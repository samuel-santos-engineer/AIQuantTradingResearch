"""Synthetic v2 protocol validation. Runtime environment/capture/admission wiring belongs to WP07."""
from __future__ import annotations
from dataclasses import dataclass
from datetime import datetime, timedelta, timezone
from decimal import Decimal, InvalidOperation, localcontext
import json
import hashlib
import os
from pathlib import Path
import re
import subprocess
from typing import Callable, Mapping
from types import MappingProxyType

CONTRACT_VERSION = "aiq-historical-query-v2"
SYMBOLS = ("btc-like-simulation",)
INTERVALS = ("1h", "4h", "1d")
RANGES = ("1D", "7D", "30D", "90D")
DEFAULT_SELECTION = (SYMBOLS[0], "1h", "30D")
PUBLIC_UNAVAILABLE = "Historical market data is temporarily unavailable. Please try again later."
MAX_RESPONSE_BYTES = 2 * 1024 * 1024
MAX_REQUEST_BYTES = 4096
MAX_CANDLES = 2160
_START = datetime(2025, 10, 26, tzinfo=timezone.utc)
_END = datetime(2026, 10, 1, tzinfo=timezone.utc)
_RESPONSE_FIELDS = {"contractVersion", "state", "instrument", "interval", "range", "candles", "provenance", "generatedAtUtc", "failure", "analytics"}
_IDENTITY_FIELDS = {"canonicalVersion", "instrument", "marketType", "source", "status", "venue", "base", "quote", "settlement", "priceUnit", "volumeUnit", "generatorVersion", "seed", "coverageStartUtc", "coverageEndExclusiveUtc", "datasetFingerprint", "windowFingerprint", "calculationVersion"}
_PARAMETERS = {"version": "aiq-ta-v1", "scale": 12, "smaPeriod": 20, "emaPeriod": 20, "rsiPeriod": 14,
               "macdFastPeriod": 12, "macdSlowPeriod": 26, "macdSignalPeriod": 9, "bollingerPeriod": 20, "bollingerMultiplier": 2}
_ANALYTICS_FIELDS = {"calculationVersion", "parameters", "context", "source", "instrument", "interval", "range",
                     "canonicalVersion", "datasetFingerprint", "windowFingerprint", "calculationStartUtc", "visibleStartUtc",
                     "warmup", "calculationCount", "rows"}
_INDICATOR_FIELDS = ("sma20", "ema20", "rsi14", "macd", "macdSignal", "macdHistogram", "bollingerLower", "bollingerMiddle", "bollingerUpper")

class BridgeError(ValueError):
    pass

@dataclass(frozen=True)
class Candle:
    open_time_utc: str
    open: Decimal
    high: Decimal
    low: Decimal
    close: Decimal
    volume: Decimal

@dataclass(frozen=True)
class IndicatorRow:
    open_time_utc: str
    sma20: Decimal | None
    ema20: Decimal | None
    rsi14: Decimal | None
    macd: Decimal | None
    macd_signal: Decimal | None
    macd_histogram: Decimal | None
    bollinger_lower: Decimal | None
    bollinger_middle: Decimal | None
    bollinger_upper: Decimal | None

@dataclass(frozen=True)
class Analytics:
    metadata: Mapping[str, object]
    rows: tuple[IndicatorRow, ...]

@dataclass(frozen=True)
class MarketResponse:
    state: str
    symbol: str | None
    interval: str | None
    range: str | None
    candles: tuple[Candle, ...]
    provider: str | None
    last_updated_utc: str | None
    last_validated_utc: str | None
    is_stale: bool
    failure: str | None
    provenance: Mapping[str, object] | None = None
    analytics: Analytics | None = None

def _parse_analytics(value, identity, instrument, interval, range_value, candles):
    if not isinstance(value, dict) or set(value) != _ANALYTICS_FIELDS:
        raise BridgeError("MalformedResponse")
    parameters = value["parameters"]
    if not isinstance(parameters, dict) or set(parameters) != set(_PARAMETERS) or any(
            type(parameters[k]) is not type(expected) or parameters[k] != expected for k, expected in _PARAMETERS.items()):
        raise BridgeError("MalformedResponse")
    hours = {"1h": 1, "4h": 4, "1d": 24}[interval]
    visible_start = _END - timedelta(days=int(range_value[:-1]))
    expected = {"calculationVersion": "aiq-ta-v1", "context": "public-synthetic", "source": identity["source"],
                "instrument": instrument, "interval": interval, "range": range_value, "canonicalVersion": identity["canonicalVersion"],
                "datasetFingerprint": identity["datasetFingerprint"], "windowFingerprint": identity["windowFingerprint"],
                "calculationStartUtc": (visible_start - timedelta(hours=250 * hours)).strftime("%Y-%m-%dT%H:%M:%SZ"),
                "visibleStartUtc": visible_start.strftime("%Y-%m-%dT%H:%M:%SZ"), "warmup": 250, "calculationCount": len(candles) + 250}
    if any(type(value[k]) is not type(v) or value[k] != v for k, v in expected.items()):
        raise BridgeError("MalformedResponse")
    items = value["rows"]
    if not isinstance(items, list) or len(items) != len(candles):
        raise BridgeError("MalformedResponse")
    rows = []
    for candle, item in zip(candles, items):
        if not isinstance(item, dict) or set(item) != {"openTimeUtc", *_INDICATOR_FIELDS} or item["openTimeUtc"] != candle.open_time_utc:
            raise BridgeError("MalformedResponse")
        numbers = []
        for field in _INDICATOR_FIELDS:
            text = item[field]
            # All visible positions follow 250 warmup rows and therefore every first-valid sample.
            # Preserve Decimal/null types in the model, but reject null at these mature positions.
            if not isinstance(text, str) or not re.fullmatch(r"-?(?:0|[1-9][0-9]{0,9})\.[0-9]{12}", text):
                raise BridgeError("MalformedResponse")
            number = Decimal(text)
            if number.copy_abs() > Decimal("3000000000") or (number == 0 and text.startswith("-")):
                raise BridgeError("MalformedResponse")
            numbers.append(number)
        sma, ema, rsi, macd, signal, histogram, lower, middle, upper = numbers
        with localcontext() as context:
            context.prec = 50
            if not (0 < sma <= Decimal("1000000000") and 0 < ema <= Decimal("1000000000") and 0 <= rsi <= 100 and
                    middle == sma and lower <= middle <= upper and upper - middle == middle - lower and histogram == macd - signal):
                raise BridgeError("MalformedResponse")
        rows.append(IndicatorRow(candle.open_time_utc, *numbers))
    metadata = {k: v for k, v in value.items() if k != "rows"}
    metadata["parameters"] = MappingProxyType(dict(parameters))
    return Analytics(MappingProxyType(metadata), tuple(rows))

def build_request(symbol: str, interval: str, range_value: str, deadline_seconds: int = 10) -> str:
    if symbol not in SYMBOLS or interval not in INTERVALS or range_value not in RANGES or type(deadline_seconds) is not int or not 1 <= deadline_seconds <= 30:
        raise BridgeError("InvalidRequest")
    return json.dumps({"contractVersion": CONTRACT_VERSION, "instrument": symbol, "interval": interval, "range": range_value, "deadlineSeconds": deadline_seconds}, separators=(",", ":"))

def _object(pairs):
    result = {}
    for name, value in pairs:
        if name in result:
            raise BridgeError("InvalidRequest")
        result[name] = value
    return result

def _load(payload: str | bytes, maximum: int):
    try:
        encoded = payload.encode("utf-8", errors="strict") if isinstance(payload, str) else payload
        if not isinstance(encoded, bytes) or len(encoded) > maximum:
            raise BridgeError("PayloadTooLarge")
        text = encoded.decode("utf-8", errors="strict")
        if any(ord(c) > 127 for c in text):
            raise BridgeError("MalformedResponse")
        return json.loads(text, object_pairs_hook=_object, parse_constant=lambda _: (_ for _ in ()).throw(BridgeError("MalformedResponse")))
    except (UnicodeError, ValueError, RecursionError) as exc:
        raise BridgeError("MalformedResponse") from exc

def validate_request(payload: str | bytes):
    value = _load(payload, MAX_REQUEST_BYTES)
    if not isinstance(value, dict) or set(value) != {"contractVersion", "instrument", "interval", "range", "deadlineSeconds"} or value["contractVersion"] != CONTRACT_VERSION:
        raise BridgeError("InvalidRequest")
    build_request(value["instrument"], value["interval"], value["range"], value["deadlineSeconds"])
    return value

def _decimal(value: object, scale: int) -> Decimal:
    if not isinstance(value, str) or not re.fullmatch(r"[0-9]{1,26}\.[0-9]{" + str(scale) + r"}", value):
        raise BridgeError("MalformedResponse")
    try:
        number = Decimal(value)
        if not number.is_finite() or number > Decimal("79228162514264337593543950335"):
            raise BridgeError("MalformedResponse")
        return number
    except InvalidOperation as exc:
        raise BridgeError("MalformedResponse") from exc

def _utc_timestamp(value: object) -> datetime:
    if not isinstance(value, str):
        raise BridgeError("MalformedResponse")
    try:
        parsed = datetime.fromisoformat(value.replace("Z", "+00:00"))
    except ValueError as exc:
        raise BridgeError("MalformedResponse") from exc
    if parsed.tzinfo is None or parsed.utcoffset() != timedelta(0):
        raise BridgeError("MalformedResponse")
    return parsed

def parse_response(text: str | bytes, *, qualified_dataset_fingerprints: Mapping[str, str] | None = None, qualified_window_fingerprints: Mapping[tuple[str, str], str] | None = None, qualified_visible_fingerprints: Mapping[tuple[str, str], str] | None = None) -> MarketResponse:
    value = _load(text, MAX_RESPONSE_BYTES)
    if not isinstance(value, dict) or set(value) != _RESPONSE_FIELDS or value["contractVersion"] != CONTRACT_VERSION:
        raise BridgeError("MalformedResponse")
    if value["state"] == "Unavailable":
        if any(value[field] is not None for field in ("instrument", "interval", "range", "candles", "provenance", "generatedAtUtc", "analytics")) or value["failure"] not in ("InvalidRequest", "Unavailable", "Busy", "Timeout"):
            raise BridgeError("MalformedResponse")
        return MarketResponse("Unavailable", None, None, None, (), None, None, None, False, value["failure"])
    instrument, interval, range_value = value["instrument"], value["interval"], value["range"]
    if value["state"] != "Available" or instrument not in SYMBOLS or interval not in INTERVALS or range_value not in RANGES or value["failure"] is not None:
        raise BridgeError("MalformedResponse")
    identity = value["provenance"]
    if not isinstance(identity, dict) or set(identity) != _IDENTITY_FIELDS:
        raise BridgeError("MalformedResponse")
    expected = {"canonicalVersion": "aiq-candles-v2", "instrument": SYMBOLS[0], "marketType": "simulation", "source": "independent-synthetic", "status": "simulated", "venue": None, "base": None, "quote": None, "settlement": None, "priceUnit": "illustrative-price-unit", "volumeUnit": "illustrative-activity-unit", "generatorVersion": "aiq-synthetic-ohlcv-v1", "calculationVersion": "aiq-ta-v1"}
    if any(identity[k] != v for k, v in expected.items()) or type(identity["seed"]) is not int or identity["seed"] != 114 or _utc_timestamp(identity["coverageStartUtc"]) != _START or _utc_timestamp(identity["coverageEndExclusiveUtc"]) != _END:
        raise BridgeError("MalformedResponse")
    if any(not isinstance(identity[k], str) or not re.fullmatch("[0-9a-f]{64}", identity[k]) for k in ("datasetFingerprint", "windowFingerprint")):
        raise BridgeError("MalformedResponse")
    # Server configuration supplies qualified identities. Never infer them from response/provider display strings.
    if qualified_dataset_fingerprints is None or qualified_dataset_fingerprints.get(interval) != identity["datasetFingerprint"]:
        raise BridgeError("UnqualifiedDataset")
    if qualified_window_fingerprints is None or qualified_window_fingerprints.get((interval, range_value)) != identity["windowFingerprint"]:
        raise BridgeError("UnqualifiedWindow")
    _utc_timestamp(value["generatedAtUtc"])
    hours = {"1h": 1, "4h": 4, "1d": 24}[interval]
    days = int(range_value[:-1])
    items = value["candles"]
    if not isinstance(items, list) or len(items) != 24 * days // hours or len(items) > MAX_CANDLES:
        raise BridgeError("MalformedResponse")
    candles = []
    for i, item in enumerate(items):
        if not isinstance(item, dict) or set(item) != {"openTimeUtc", "open", "high", "low", "close", "volume"}:
            raise BridgeError("MalformedResponse")
        timestamp = item["openTimeUtc"]
        if timestamp != (_END - timedelta(days=days) + timedelta(hours=i * hours)).strftime("%Y-%m-%dT%H:%M:%SZ"):
            raise BridgeError("MalformedResponse")
        candle = Candle(timestamp, *(_decimal(item[k], 3 if k == "volume" else 4) for k in ("open", "high", "low", "close", "volume")))
        if not 0 < candle.low <= min(candle.open, candle.close) <= max(candle.open, candle.close) <= candle.high <= Decimal("1000000000") or candle.volume < 0:
            raise BridgeError("MalformedResponse")
        candles.append(candle)
    visible_bytes = ''.join(','.join((c.open_time_utc, format(c.open, '.4f'), format(c.high, '.4f'), format(c.low, '.4f'), format(c.close, '.4f'), format(c.volume, '.3f'))) + '\n' for c in candles).encode('ascii')
    if qualified_visible_fingerprints is None or qualified_visible_fingerprints.get((interval, range_value)) != hashlib.sha256(visible_bytes).hexdigest():
        raise BridgeError("UnqualifiedVisibleData")
    analytics = _parse_analytics(value["analytics"], identity, instrument, interval, range_value, candles)
    return MarketResponse("Available", instrument, interval, range_value, tuple(candles), "independent-synthetic", value["generatedAtUtc"], None, False, None, MappingProxyType(identity), analytics)

def invoke(symbol: str, interval: str, range_value: str, environ: Mapping[str, str] | None = None, runner: Callable[..., subprocess.CompletedProcess[str]] = subprocess.run, *, qualified_dataset_fingerprints: Mapping[str, str] | None = None, qualified_window_fingerprints: Mapping[tuple[str, str], str] | None = None, qualified_visible_fingerprints: Mapping[tuple[str, str], str] | None = None) -> MarketResponse:
    request = build_request(symbol, interval, range_value)
    validate_request(request)  # before any subprocess selection
    env = dict(os.environ if environ is None else environ)
    worker = env.get("HistoricalMarketQuery__WorkerAssemblyPath", "/app/worker/AIQuantTradingResearch.Worker.dll")
    if not Path(worker).is_absolute() and not worker.startswith("/"):
        raise BridgeError("BridgeUnavailable")
    env["Worker__Mode"] = "HistoricalQuery"
    # Environment allowlist, bounded live capture and global admission are explicitly WP07 work.
    try:
        completed = runner(["dotnet", worker], input=request, text=True, encoding="utf-8", errors="strict", capture_output=True, timeout=15, shell=False, env=env, check=False)
    except (OSError, UnicodeError, subprocess.TimeoutExpired) as exc:
        raise BridgeError("BridgeUnavailable") from exc
    if completed.returncode != 0:
        raise BridgeError("BridgeUnavailable")
    response = parse_response(completed.stdout, qualified_dataset_fingerprints=qualified_dataset_fingerprints, qualified_window_fingerprints=qualified_window_fingerprints, qualified_visible_fingerprints=qualified_visible_fingerprints)
    if response.state == "Available" and (response.symbol, response.interval, response.range) != (symbol, interval, range_value):
        raise BridgeError("MalformedResponse")
    return response
