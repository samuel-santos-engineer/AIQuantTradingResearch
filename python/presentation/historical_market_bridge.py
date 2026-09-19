"""Bounded server-side consumer for the WP05 Worker historical-query protocol."""
from __future__ import annotations

from dataclasses import dataclass
from decimal import Decimal, InvalidOperation
import json
import os
from pathlib import Path
import subprocess
from typing import Callable, Mapping

CONTRACT_VERSION = "aiq-historical-query-v1"
SYMBOLS = ("BTC/USD", "ETH/USD")
INTERVALS = ("1h", "4h", "1d")
RANGES = ("1D", "7D", "30D", "90D")
DEFAULT_SELECTION = ("BTC/USD", "1h", "30D")
PUBLIC_UNAVAILABLE = "Historical market data is temporarily unavailable. Please try again later."
MAX_RESPONSE_BYTES = 2_000_000


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


def build_request(symbol: str, interval: str, range_value: str, deadline_seconds: int = 10) -> str:
    if symbol not in SYMBOLS or interval not in INTERVALS or range_value not in RANGES or not 1 <= deadline_seconds <= 30:
        raise BridgeError("InvalidSelection")
    return json.dumps({"contractVersion": CONTRACT_VERSION, "symbol": symbol, "interval": interval, "range": range_value, "deadlineSeconds": deadline_seconds}, separators=(",", ":"))


def _decimal(value: object) -> Decimal:
    if not isinstance(value, str):
        raise BridgeError("MalformedResponse")
    try:
        return Decimal(value)
    except InvalidOperation as exc:
        raise BridgeError("MalformedResponse") from exc


def parse_response(text: str) -> MarketResponse:
    if len(text.encode("utf-8")) > MAX_RESPONSE_BYTES:
        raise BridgeError("ResponseTooLarge")
    try:
        value = json.loads(text)
    except json.JSONDecodeError as exc:
        raise BridgeError("MalformedResponse") from exc
    if not isinstance(value, dict) or value.get("contractVersion") != CONTRACT_VERSION or value.get("state") not in ("Fresh", "Stale", "Unavailable"):
        raise BridgeError("MalformedResponse")
    state = value["state"]
    symbol, interval, range_value = value.get("symbol"), value.get("interval"), value.get("range")
    if state != "Unavailable" and (symbol not in SYMBOLS or interval not in INTERVALS or range_value not in RANGES):
        raise BridgeError("MalformedResponse")
    candles_value = value.get("candles")
    if state == "Unavailable":
        candles_value = [] if candles_value is None else candles_value
    if not isinstance(candles_value, list):
        raise BridgeError("MalformedResponse")
    candles: list[Candle] = []
    for item in candles_value:
        if not isinstance(item, dict) or not isinstance(item.get("openTimeUtc"), str):
            raise BridgeError("MalformedResponse")
        candle = Candle(item["openTimeUtc"], _decimal(item.get("open")), _decimal(item.get("high")), _decimal(item.get("low")), _decimal(item.get("close")), _decimal(item.get("volume")))
        if min(candle.open, candle.high, candle.low, candle.close) <= 0 or candle.volume < 0 or candle.low > min(candle.open, candle.close) or candle.high < max(candle.open, candle.close):
            raise BridgeError("MalformedResponse")
        candles.append(candle)
    return MarketResponse(state, symbol, interval, range_value, tuple(candles), value.get("provider") if isinstance(value.get("provider"), str) else None, value.get("lastUpdatedUtc") if isinstance(value.get("lastUpdatedUtc"), str) else None, value.get("lastValidatedUtc") if isinstance(value.get("lastValidatedUtc"), str) else None, value.get("isStale") is True, value.get("failure") if isinstance(value.get("failure"), str) else None)


def invoke(symbol: str, interval: str, range_value: str, environ: Mapping[str, str] | None = None, runner: Callable[..., subprocess.CompletedProcess[str]] = subprocess.run) -> MarketResponse:
    env = dict(os.environ if environ is None else environ)
    worker = env.get("HistoricalMarketQuery__WorkerAssemblyPath", "/app/worker/AIQuantTradingResearch.Worker.dll")
    if not Path(worker).is_absolute() and not worker.startswith("/"):
        raise BridgeError("BridgeUnavailable")
    env["Worker__Mode"] = "HistoricalQuery"
    request = build_request(symbol, interval, range_value)
    try:
        completed = runner(["dotnet", worker], input=request, text=True, capture_output=True, timeout=15, shell=False, env=env, check=False)
    except (OSError, subprocess.TimeoutExpired) as exc:
        raise BridgeError("BridgeUnavailable") from exc
    if completed.returncode != 0:
        raise BridgeError("BridgeUnavailable")
    return parse_response(completed.stdout)
