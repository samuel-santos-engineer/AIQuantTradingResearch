"""Opt-in Windows loopback adapter. No public bridge activation or indicator arithmetic."""
from datetime import datetime, timedelta, timezone
from decimal import Decimal
import hashlib
import json
import os
from types import MappingProxyType
from urllib.request import build_opener, ProxyHandler, HTTPRedirectHandler

from historical_market_bridge import (Analytics, BridgeError, Candle, IndicatorRow, MarketResponse,
                                     QUALIFIED_DATASETS, QUALIFIED_WINDOWS, QUALIFIED_VISIBLE,
                                     _object, _decimal, _utc_timestamp)
from public_preview_api import LABEL


def enabled(server_address, *, environment=None, platform=None):
    env = os.environ if environment is None else environment
    return ((os.name if platform is None else platform) == "nt" and server_address == "127.0.0.1"
            and env.get("AIQ_LOCAL_CHART") == "1" and env.get("DOTNET_ENVIRONMENT") == "Development"
            and not any(k.upper().startswith("WEBSITE") for k in env))


class NoRedirect(HTTPRedirectHandler):
    def redirect_request(self, *args, **kwargs):
        raise BridgeError("LocalSourceUnavailable")


def load(symbol, interval, range_value, *, server_address):
    if not enabled(server_address):
        raise BridgeError("LocalSourceDisabled")
    if symbol != "btc-like-simulation" or interval not in QUALIFIED_DATASETS or (interval, range_value) not in QUALIFIED_WINDOWS:
        raise BridgeError("InvalidLocalSelection")
    # Fixed numeric loopback address, no proxy/redirect or user-supplied URL.
    try:
        with build_opener(ProxyHandler({}), NoRedirect()).open(
                f"http://127.0.0.1:8766/local-chart/data?interval={interval}&range={range_value}", timeout=12) as response:
            raw = response.read(2 * 1024 * 1024 + 1)
        return parse(raw, interval, range_value)
    except (OSError, ValueError, KeyError, TypeError, ArithmeticError) as exc:
        raise BridgeError("LocalSourceUnavailable") from exc


def parse(raw, interval, range_value):
    # Local ASP.NET JSON carries the Unicode simulation label; the held Worker's
    # ASCII wire decoder is intentionally not changed or reused for this envelope.
    if not isinstance(raw, bytes) or len(raw) > 2 * 1024 * 1024:
        raise BridgeError("MalformedLocalSource")
    def reject_constant(_):
        raise BridgeError("MalformedLocalSource")
    try:
        value = json.loads(raw.decode("utf-8", errors="strict"), object_pairs_hook=_object, parse_constant=reject_constant)
    except (UnicodeError, ValueError, RecursionError) as exc:
        raise BridgeError("MalformedLocalSource") from exc
    fields = {"label", "instrument", "interval", "range", "coverageStartUtc", "endExclusiveUtc", "datasetFingerprint",
              "windowFingerprint", "calculationVersion", "calculationStartUtc", "warmup", "calculationCount", "publicRuntimeVerified", "candles"}
    if not isinstance(value, dict) or set(value) != fields or type(value["publicRuntimeVerified"]) is not bool or value["publicRuntimeVerified"]:
        raise BridgeError("MalformedLocalSource")
    hours = {"1h": 1, "4h": 4, "1d": 24}[interval]
    count = 24 * int(range_value[:-1]) // hours
    end = datetime(2026, 10, 1, tzinfo=timezone.utc)
    start = end - timedelta(days=int(range_value[:-1]))
    expected = {"label": LABEL, "instrument": "btc-like-simulation", "interval": interval, "range": range_value,
                "coverageStartUtc": "2025-10-26T00:00:00Z", "endExclusiveUtc": "2026-10-01T00:00:00Z",
                "datasetFingerprint": QUALIFIED_DATASETS[interval], "windowFingerprint": QUALIFIED_WINDOWS[(interval, range_value)],
                "calculationVersion": "aiq-ta-v1", "warmup": 250, "calculationCount": count + 250,
                "calculationStartUtc": (start - timedelta(hours=250 * hours)).strftime("%Y-%m-%dT%H:%M:%SZ")}
    if any(type(value[k]) is not type(v) or value[k] != v for k, v in expected.items()) or not isinstance(value["candles"], list) or len(value["candles"]) != count:
        raise BridgeError("MalformedLocalSource")
    candles, indicators = [], []
    for i, row in enumerate(value["candles"]):
        if not isinstance(row, dict) or set(row) != {"timeUtc", "open", "high", "low", "close", "volume", "sma20"}:
            raise BridgeError("MalformedLocalSource")
        stamp = row["timeUtc"]
        if _utc_timestamp(stamp) != start + timedelta(hours=i * hours) or stamp != (start + timedelta(hours=i * hours)).strftime("%Y-%m-%dT%H:%M:%SZ"):
            raise BridgeError("MalformedLocalSource")
        candle = Candle(stamp, *(_decimal(row[k], 4) for k in ("open", "high", "low", "close")), _decimal(row["volume"], 3))
        if not Decimal(0) < candle.low <= min(candle.open, candle.close) <= max(candle.open, candle.close) <= candle.high <= Decimal(10**9) or candle.volume < 0:
            raise BridgeError("MalformedLocalSource")
        sma = _decimal(row["sma20"], 12)
        if not 0 < sma <= Decimal(10**9):
            raise BridgeError("MalformedLocalSource")
        candles.append(candle)
        # Only SMA is transported. No Python/JS recomputation or invented other indicators.
        indicators.append(IndicatorRow(stamp, sma, None, None, None, None, None, None, None, None))
    canonical = "".join(",".join((c.open_time_utc, format(c.open, ".4f"), format(c.high, ".4f"), format(c.low, ".4f"),
                                  format(c.close, ".4f"), format(c.volume, ".3f"))) + "\n" for c in candles).encode("ascii")
    if hashlib.sha256(canonical).hexdigest() != QUALIFIED_VISIBLE[(interval, range_value)]:
        raise BridgeError("UnqualifiedLocalCandles")
    provenance = MappingProxyType({"source": "independent-synthetic", "status": "simulated", "calculationVersion": "aiq-ta-v1",
                                   "datasetFingerprint": value["datasetFingerprint"], "windowFingerprint": value["windowFingerprint"]})
    analytics = Analytics(MappingProxyType({"context": "public-synthetic", "availableIndicators": ("sma20",)}), tuple(indicators))
    return MarketResponse("Available", "btc-like-simulation", interval, range_value, tuple(candles), "independent-synthetic",
                          None, None, False, None, provenance, analytics)
