"""Test-only E4 oracle. NOT a runtime provider, exporter or accepted parity result.

Derived for C2B-15 from RELEASE_1.14_DEFINITION.md's approved generator,
canonical serialization and shared indicator contracts. No repository imports, C#
translation, embedded WP02 script or production output is used as an expectation.
Independence is an authorship/design claim pending independent numerical review.

Derivations: price/volume use the specification's integer ticks; aggregation is a
partition fold. Fraction's round implements exact rational ties-to-even; EMA is a
rounded rational recurrence (not rounded alpha). Population variance is A/400 at
scale 10^24: floor(sqrt(A)/20) = isqrt(A//400), followed by exact midpoint comparison.
No binary floating point, files, provider/network inputs or persistent vectors.
"""

import base64
from datetime import datetime, timedelta, timezone
from fractions import Fraction
import hashlib
import json
from math import isqrt
import sys

SCALE = 10**12
ORIGIN = datetime(2025, 10, 26, tzinfo=timezone.utc)
END = datetime(2026, 10, 1, tzinfo=timezone.utc)
VERSION = "aiq-synthetic-ohlcv-v1"
INTERVALS = (("1h", 1), ("4h", 4), ("1d", 24))
RANGES = (1, 7, 30, 90)


def quantize(numerator, denominator):
    return round(Fraction(numerator, denominator))


def timestamp(hour):
    return (ORIGIN + timedelta(hours=hour)).strftime("%Y-%m-%dT%H:%M:%SZ")


def counter(index, lane, modulus):
    message = (VERSION.encode("ascii") + b"\0" + (114).to_bytes(8, "big")
               + index.to_bytes(8, "big") + bytes((lane,)))
    word = int.from_bytes(hashlib.sha256(message).digest()[:4], "big")
    return word * modulus // (2**32)


def boundary(index):
    phase = index % 480
    triangle = phase - 120 if phase <= 240 else 360 - phase
    return 500000000 + 2500 * triangle + counter(index, 0, 200001) - 100000


def hourly():
    prices = [boundary(i) for i in range(8161)]
    return [(i, prices[i], max(prices[i:i+2]) + 1 + counter(i, 1, 50000),
             min(prices[i:i+2]) - 1 - counter(i, 2, 50000), prices[i+1],
             100000 + counter(i, 3, 1000000)) for i in range(8160)]


def aggregate(rows, hours):
    result = []
    for start in range(0, len(rows), hours):
        group = rows[start:start+hours]
        result.append((group[0][0], group[0][1], max(r[2] for r in group),
                       min(r[3] for r in group), group[-1][4], sum(r[5] for r in group)))
    return result


def fixed(integer, digits):
    sign = "-" if integer < 0 else ""
    whole, fraction = divmod(abs(integer), 10**digits)
    return f"{sign}{whole}.{fraction:0{digits}d}"


def row_bytes(rows):
    return "".join(timestamp(r[0]) + "," + ",".join(
        fixed(value, 3 if i == 4 else 4) for i, value in enumerate(r[1:]))
        + "\n" for r in rows).encode("utf-8")


def canonical(interval, rows, days=None):
    # Header order and spellings transcribed from the frozen serialization contract.
    lines = ["aiq-synthetic-dataset-v1" if days is None else "aiq-synthetic-window-v1",
             "canonical=aiq-candles-v2", "source=independent-synthetic", "status=simulated",
             "instrument=btc-like-simulation", "marketType=simulation", "venue=none",
             "base=none", "quote=none", "settlement=none", "priceUnit=illustrative-price-unit",
             "volumeUnit=illustrative-activity-unit", "generator=" + VERSION, "seed=114",
             "start=2025-10-26T00:00:00Z", "endExclusive=2026-10-01T00:00:00Z",
             "interval=" + interval, "priceScale=4", "volumeScale=3"]
    if days is not None:
        lines += [f"range={days}D", "visibleStart=" + (END-timedelta(days=days)).strftime(
            "%Y-%m-%dT%H:%M:%SZ"), "calculationStart=" + timestamp(rows[0][0]), "warmup=250"]
    return ("\n".join(lines + ["columns=openTimeUtc,open,high,low,close,volume"]) + "\n").encode() + row_bytes(rows)


def digest(data):
    return hashlib.sha256(data).hexdigest()


def envelope(data):
    return {"bytes": base64.b64encode(data).decode("ascii"),
            "length": len(data), "sha256": digest(data)}


def mean_path(values, period):
    """SMA seed, then round the complete rational recurrence once per step."""
    answer = [None] * len(values)
    if len(values) >= period:
        answer[period-1] = quantize(sum(values[:period]), period)
        for end in range(period, len(values)):
            answer[end] = quantize(2*values[end] + (period-1)*answer[end-1], period+1)
    return answer


def sigma(window):
    # Independent exact integer root, not the calculator's binary-search implementation.
    a = 20*sum(x*x for x in window) - sum(window)**2
    lower = isqrt(a // 400)
    midpoint_side = 4*a - 400*(2*lower+1)**2
    return lower + int(midpoint_side > 0 or (midpoint_side == 0 and lower % 2 == 1))


def indicators(values):
    fast, slow, ema = (mean_path(values, p) for p in (12, 26, 20))
    macd = [None if slow[i] is None else fast[i]-slow[i] for i in range(len(values))]
    signal = [None]*min(25, len(values)) + mean_path(macd[25:], 9)
    gain = loss = None
    result = []
    for end in range(len(values)):
        if end == 14:
            changes = [values[k]-values[k-1] for k in range(1, 15)]
            gain = quantize(sum(max(d, 0) for d in changes), 14)
            loss = quantize(sum(max(-d, 0) for d in changes), 14)
        elif end > 14:
            change = values[end]-values[end-1]
            gain = quantize(13*gain+max(change, 0), 14)
            loss = quantize(13*loss+max(-change, 0), 14)
        rsi = None if end < 14 else (50*SCALE if gain+loss == 0 else
                                    quantize(100*SCALE*gain, gain+loss))
        middle = deviation = None
        if end >= 19:
            window = values[end-19:end+1]
            middle, deviation = quantize(sum(window), 20), sigma(window)
        row = [middle, ema[end], rsi, macd[end], signal[end],
               None if signal[end] is None else macd[end]-signal[end],
               None if middle is None else middle-2*deviation, middle,
               None if middle is None else middle+2*deviation]
        result.append([None if x is None else fixed(x, 12) for x in row])
    return result


def edge_series():
    # Explicit small specification-derived series, not sampled production rows.
    series = {
        "constant": [100*SCALE]*40,
        "rising": [i*SCALE for i in range(1, 41)],
        "falling": [i*SCALE for i in range(40, 0, -1)],
        "alternating": [(100+i % 2)*SCALE for i in range(40)],
        "mixed": [(89+(i*17) % 23)*SCALE+(i % 3)*(SCALE//4) for i in range(40)],
        "large": [(10**9)*SCALE-i for i in range(40)],
        "minimum": [1+(i % 3) for i in range(40)],
    }
    # +/- half a scaled unit around an even/odd integer SMA, all closes positive.
    for offset in (0, 1):
        for direction in (-1, 1):
            series[f"tie-{offset}-{direction}"] = [100*SCALE+offset]*19 + [100*SCALE+offset+direction*10]
    return [{"id": name, "closes": [fixed(x, 12) for x in values],
             "indicators": indicators(values)} for name, values in series.items()]


def proof():
    # Frozen rational reference checks, not production-derived goldens.
    if [quantize(n, 2) for n in (1, 3, -1, -3)] != [0, 2, 0, -2]:
        raise ArithmeticError("rational ties-to-even contract")
    base = hourly()
    datasets, windows = [], []
    for literal, hours in INTERVALS:
        rows = base if hours == 1 else aggregate(base, hours)
        datasets.append({"interval": literal, "count": len(rows), "canonical": envelope(canonical(literal, rows))})
        for days in RANGES:
            visible_count = 24*days//hours
            calculation = rows[-visible_count-250:]
            visible = calculation[250:]
            values = [r[4]*(SCALE//10000) for r in calculation]
            analytics = indicators(values)
            windows.append({"interval": literal, "range": f"{days}D", "count": len(calculation),
                            "visibleCount": visible_count, "calculationStart": timestamp(calculation[0][0]),
                            "visibleStart": timestamp(visible[0][0]), "canonical": envelope(canonical(literal, calculation, days)),
                            "visible": envelope(row_bytes(visible)), "indicators": analytics,
                            "visibleIndicators": analytics[250:]})
    return {"version": "e4-test-only-v1", "generator": VERSION, "seed": 114,
            "coverageStart": "2025-10-26T00:00:00Z", "coverageEnd": "2026-10-01T00:00:00Z",
            "parameters": ["aiq-ta-v1", 12, 20, 20, 14, 12, 26, 9, 20, 2],
            "datasets": datasets, "windows": windows, "edges": edge_series()}


if __name__ == "__main__":
    # Exact selector only. No input candle stream, paths, arbitrary mode or output file.
    if sys.argv[1:] != ["--proof", "aiq-synthetic-ohlcv-v1", "114"]:
        sys.stderr.write("invalid E4 selector\n")
        raise SystemExit(2)
    payload = json.dumps(proof(), ensure_ascii=True, allow_nan=False, separators=(",", ":")).encode("utf-8")
    if len(payload) > 16*1024*1024:
        raise RuntimeError("E4 output exceeds proposed capture bound")
    sys.stdout.buffer.write(payload)
