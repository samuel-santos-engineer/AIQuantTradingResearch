"""Bounded read-only ASGI contract. No storage, generation, provider or Worker path."""
from __future__ import annotations

import asyncio
from collections import deque
from datetime import datetime, timedelta, timezone
import json
import re
import time
from urllib.parse import parse_qsl

LABEL = "BTC-like Market Simulation — Synthetic Historical OHLCV"
INSTRUMENT = "btc-like-simulation"
VERSION = "chart-preview-v1"
COVERAGE_START = "2025-10-26T00:00:00Z"
COVERAGE_END = "2026-10-01T00:00:00Z"
INTERVALS = {"1h": 3600, "4h": 14400, "1d": 86400}
INDICATORS = ("sma20", "ema20", "rsi14", "macd12_26_9", "bollinger20_2")
ROUTES = frozenset(("health", "status", "dataset", "candles", "indicators", "diagnostics"))
MAX_INPUT_BYTES = 4096
MAX_RESPONSE_BYTES = 4096
FIXED_END = datetime(2026, 10, 1, tzinfo=timezone.utc)
VISIBLE_START = FIXED_END - timedelta(days=90)
IDENTITY = {"instrument": INSTRUMENT, "source": "independent-synthetic", "status": "simulated",
            "marketType": "simulation", "priceUnit": "illustrative-price-unit", "volumeUnit": "illustrative-activity-unit"}


class InvalidQuery(ValueError):
    pass


class WindowLimit:
    """Single event-loop instance, bounded history; not deployment-global qualification."""
    def __init__(self, per_minute=30, per_second=2, clock=time.monotonic):
        self.minute = per_minute
        self.second = per_second
        self.clock = clock
        self.history = deque()
        self.last = None
        self.failed = False

    def accept(self):
        now = self.clock()
        if self.failed or (self.last is not None and now < self.last):
            self.failed = True
            return False
        self.last = now
        while self.history and now - self.history[0] >= 60:
            self.history.popleft()
        if len(self.history) >= self.minute or sum(now - stamp < 1 for stamp in self.history) >= self.second:
            return False
        self.history.append(now)
        return True


def parse_query(route, raw):
    if len(raw) > MAX_INPUT_BYTES or re.search(rb"%(?![0-9a-fA-F]{2})", raw):
        raise InvalidQuery("INVALID_QUERY")
    try:
        pairs = parse_qsl(raw.decode("ascii"), keep_blank_values=True, strict_parsing=True,
                          encoding="utf-8", errors="strict", max_num_fields=8)
    except (UnicodeError, ValueError):
        raise InvalidQuery("INVALID_QUERY") from None
    values = dict(pairs)
    allowed = {"instrument", "interval", "from", "to", "limit"} if route in ("candles", "indicators") else set()
    if route == "indicators":
        allowed.add("indicator")
    if len(values) != len(pairs) or set(values) - allowed or any(not value for value in values.values()):
        raise InvalidQuery("INVALID_QUERY")
    if not allowed:
        return values
    if values.get("instrument", INSTRUMENT) != INSTRUMENT:
        raise InvalidQuery("INVALID_INSTRUMENT")
    interval = values.get("interval", "1h")
    if interval not in INTERVALS:
        raise InvalidQuery("INVALID_INTERVAL")
    if route == "indicators" and values.get("indicator", "sma20") not in INDICATORS:
        raise InvalidQuery("INVALID_INDICATOR")
    bounds = []
    for key, default in (("from", "2026-09-01T00:00:00Z"), ("to", COVERAGE_END)):
        value = values.get(key, default)
        if not re.fullmatch(r"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z", value):
            raise InvalidQuery("INVALID_RANGE")
        try:
            stamp = datetime.strptime(value, "%Y-%m-%dT%H:%M:%SZ").replace(tzinfo=timezone.utc)
        except ValueError:
            raise InvalidQuery("INVALID_RANGE") from None
        if int((stamp - VISIBLE_START).total_seconds()) % INTERVALS[interval]:
            raise InvalidQuery("INVALID_RANGE")
        bounds.append(stamp)
    start, end = bounds
    if not VISIBLE_START <= start < end <= FIXED_END or end - start > timedelta(days=90):
        raise InvalidQuery("INVALID_RANGE")
    limit = values.get("limit", "2160")
    if not re.fullmatch(r"[1-9]\d{0,3}", limit) or not 1 <= int(limit) <= 2160:
        raise InvalidQuery("INVALID_LIMIT")
    if int((end - start).total_seconds()) // INTERVALS[interval] > int(limit):
        raise InvalidQuery("ROW_LIMIT_EXCEEDED")
    return values


def payload(route):
    result = {"apiVersion": VERSION, "state": "unavailable", "code": "DATASET_NOT_READY", "data": None}
    if route == "health":
        result.update(state="alive", code="OK", data={"service": "public-preview"})
        return 200, result
    if route == "status":
        result.update(state="degraded", data={"web": "ready", "dataset": "unavailable", "indicators": "unavailable",
                                             "generationEnabled": False})
        return 200, result
    if route == "diagnostics":
        result.update(state="degraded", data={"categories": ["DATASET_NOT_READY"], "retryAutomatically": False})
        return 200, result
    if route == "dataset":
        result["contract"] = {"label": LABEL, "identity": dict(IDENTITY), "generatorVersion": "aiq-synthetic-ohlcv-v1",
                              "seed": 114, "qualifiedReferenceStartUtc": COVERAGE_START,
                              "qualifiedReferenceEndExclusiveUtc": COVERAGE_END, "maximumVisibleDays": 90,
                              "intervals": list(INTERVALS), "ranges": ["1D", "7D", "30D", "90D"],
                              "hiddenWarmupCandles": 250, "actualCoverage": None, "datasetFingerprint": None,
                              "rollingRefreshEnabled": False}
    return 503, result


async def respond(send, status, body):
    encoded = json.dumps(body, separators=(",", ":"), ensure_ascii=True, allow_nan=False).encode("ascii")
    if len(encoded) > MAX_RESPONSE_BYTES:
        status = 503
        encoded = b'{"apiVersion":"chart-preview-v1","state":"unavailable","code":"SERVICE_UNAVAILABLE","data":null}'
    headers = [(b"content-type", b"application/json; charset=utf-8"), (b"cache-control", b"no-store"),
               (b"x-content-type-options", b"nosniff"), (b"content-length", str(len(encoded)).encode("ascii"))]
    if status == 429:
        headers.append((b"retry-after", b"1"))
    await send({"type": "http.response.start", "status": status, "headers": headers})
    await send({"type": "http.response.body", "body": encoded})


def failure(code):
    return {"apiVersion": VERSION, "state": "unavailable", "code": code, "data": None}


class PublicReadOnlyAPI:
    """Fixed unavailable implementation: no data-store argument or activation option."""
    def __init__(self, clock=time.monotonic):
        self.reads = WindowLimit(clock=clock)
        self.health = WindowLimit(120, 10, clock)
        self.active = 0

    async def __call__(self, scope, receive, send):
        if scope["type"] == "websocket":
            await send({"type": "websocket.close", "code": 1008})
            return
        path = scope.get("path", "")
        route = path.rsplit("/", 1)[-1]
        if route not in ROUTES or path not in ("/" + route, "/api/v1/" + route):
            await respond(send, 404, failure("NOT_FOUND"))
            return
        limiter = self.health if route == "health" else self.reads
        if not limiter.accept():
            await respond(send, 429, failure("RATE_LIMITED"))
            return
        if scope.get("method") != "GET":
            await respond(send, 405, failure("METHOD_NOT_ALLOWED"))
            return
        query = scope.get("query_string", b"")
        if len(query) + len(path.encode("utf-8")) > MAX_INPUT_BYTES:
            await respond(send, 414, failure("REQUEST_TOO_LARGE"))
            return
        headers = scope.get("headers", [])
        if sum(len(k) + len(v) for k, v in headers) > 16384:
            await respond(send, 431, failure("REQUEST_TOO_LARGE"))
            return
        if sum(k.lower() == b"content-length" for k, _ in headers) > 1 or any(
                k.lower() == b"transfer-encoding" or (k.lower() == b"content-length" and v != b"0") for k, v in headers):
            await respond(send, 400, failure("INVALID_REQUEST"))
            return
        # Liveness is constant work and does not wait on the data/read admission slot.
        if route == "health":
            try:
                parse_query(route, query)
            except InvalidQuery as error:
                await respond(send, 400, failure(str(error)))
                return
            await respond(send, *payload(route))
            return
        if self.active:
            await respond(send, 503, failure("SERVICE_BUSY"))
            return
        self.active += 1
        try:
            event = await asyncio.wait_for(receive(), timeout=0.25)
            if event.get("type") != "http.request" or event.get("body") or event.get("more_body", False):
                raise ValueError("invalid request event")
            parse_query(route, query)
            status, body = payload(route)
        except InvalidQuery as error:
            status, body = 400, failure(str(error))
        except (TimeoutError, ValueError, UnicodeError):
            status, body = 400, failure("INVALID_REQUEST")
        except Exception:
            status, body = 503, failure("SERVICE_UNAVAILABLE")
        finally:
            self.active -= 1
        # Transport failure after headers must not trigger a second response.
        await respond(send, status, body)


class PublicBoundary:
    """Reserve API/admin paths instead of falling through to the Streamlit SPA."""
    def __init__(self, app):
        self.app = app
        self.api = PublicReadOnlyAPI()

    async def __call__(self, scope, receive, send):
        path = scope.get("path", "").lower()
        if scope["type"] in ("http", "websocket") and (path == "/admin" or path.startswith("/admin/") or
                path == "/api" or path.startswith("/api/")):
            if scope["type"] == "websocket":
                await send({"type": "websocket.close", "code": 1008})
            elif path.startswith("/api/v1/") and scope["path"] == path:
                await self.api(scope, receive, send)
            else:
                await respond(send, 404, failure("NOT_FOUND"))
            return
        await self.app(scope, receive, send)
