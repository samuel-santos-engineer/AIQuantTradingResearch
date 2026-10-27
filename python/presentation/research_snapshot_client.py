"""Server-side D4 client. Standard synchronous HTTPS; native DNS is not hard-cancellable.

No provider acquisition, indicator calculation, UI, credentials logging or retry loop.
"""
import copy
import datetime as dt
import decimal
import http.client
import json
import re
import ssl
import threading
import time
import types
import urllib.parse
import uuid

LIMIT = 4_194_304
COLLECTIONS = ("candles", "analytics", "classifications", "events",
               "researchUnavailableIntervals", "marketDataGapIntervals")
INDICATORS = ("sma20", "ema20", "bollingerLower", "bollingerMiddle", "bollingerUpper",
              "rsi14", "macd", "macdSignal", "macdHistogram", "atr14", "normalizedAtr14",
              "bollingerWidth", "emaSlope5")
TRENDS = ("Bullish", "Bearish", "Neutral")
REGIMES = ("Volatile", "Trending", "Ranging", "Undetermined")
D = decimal.Decimal


class SnapshotUnavailable(Exception):
    def __init__(self):
        super().__init__("Research snapshot unavailable.")


def failure_reason(data, status, instrument, epoch):
    """Only validated, bounded API error DTOs become user-visible fixed states."""
    require(len(data) <= 4096)
    value = parse(data)
    fields(value, ("schemaVersion", "type", "instrument", "requestEpoch", "observedAt", "error"), True)
    require(value["schemaVersion"] == "1.0" and value["type"] == "error")
    timestamp(value["observedAt"], observation=True)
    matched = value["instrument"] == instrument and value["requestEpoch"] == str(epoch)
    unidentified = value["instrument"] is None and value["requestEpoch"] is None and status in (401, 429, 504)
    require(matched or unidentified)
    error = value["error"]
    fields(error, ("code", "message", "retryAfterSeconds"), True)
    require(error["message"] == "Request unavailable.")
    retry = error["retryAfterSeconds"]
    require(retry is None or type(retry) is int and retry == 1)
    reasons = {(401, "UNAUTHORIZED"): "AuthenticationRejected", (503, "DATASET_NOT_READY"): "DatasetMissing",
               (503, "DATA_UNAVAILABLE"): "DatasetUnavailable", (429, "RATE_LIMITED"): "RateLimited",
               (504, "DEADLINE_EXCEEDED"): "TimedOut"}
    require((status, error["code"]) in reasons)
    return reasons[status, error["code"]]


def require(test):
    if not test:
        raise SnapshotUnavailable()


def counter(value, positive=False):
    require(type(value) is str and re.fullmatch(r"0|[1-9][0-9]*", value) is not None)
    require(int(value) <= 9223372036854775807 and (not positive or int(value) > 0))
    return int(value)


def identifier(value):
    require(type(value) is str)
    parsed = uuid.UUID(value)
    require(parsed.int != 0 and str(parsed) == value)


def timestamp(value, observation=False):
    require(type(value) is str)
    pattern = r"[0-9]{4}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}\.[0-9]{6}Z" if observation else r"[0-9]{4}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:00Z"
    require(re.fullmatch(pattern, value) is not None)
    t = dt.datetime.fromisoformat(value.replace("Z", "+00:00"))
    require(2000 <= t.year <= 2099)
    return t


def quantity(value, bound, positive=False, nonnegative=False):
    require(type(value) is str and len(value) <= 64 and
            re.fullmatch(r"-?(0|[1-9][0-9]*)\.[0-9]{12}", value) is not None)
    require(value != "-0.000000000000")
    n = D(value)
    require(n.copy_abs() <= D(bound) and (not positive or n > 0) and (not nonnegative or n >= 0))
    return n


def fields(obj, required, exact=False):
    require(type(obj) is dict and set(required) <= obj.keys())
    if exact:
        require(obj.keys() == set(required))


def identity(obj):
    fields(obj, ("generation", "version", "displayDate", "transformEpoch"), True)
    identifier(obj["generation"])
    counter(obj["version"])
    counter(obj["transformEpoch"], True)
    require(type(obj["displayDate"]) is str and re.fullmatch(r"[0-9]{4}-[0-9]{2}-[0-9]{2}", obj["displayDate"]) is not None)
    day = dt.date.fromisoformat(obj["displayDate"])
    require(2000 <= day.year <= 2099)


def parse(data):
    """Preflight bounded lexical values before building the full JSON object graph."""
    require(type(data) is bytes and 0 < len(data) <= LIMIT and not data.startswith(b"\xef\xbb\xbf"))
    text = data.decode("utf-8", "strict")
    stack = []
    tokens = 0
    pos = 0
    while pos < len(text):
        c = text[pos]
        if c in " \t\r\n,:":
            pos += 1
            continue
        tokens += 1
        require(tokens <= 250000)
        if c in "{[":
            if stack and stack[-1][0] == "[":
                stack[-1][1] += 1
                require(stack[-1][1] <= 2048)
            stack.append([c, 0])
            require(len(stack) <= 8)
            pos += 1
        elif c in "}]":
            require(bool(stack) and stack[-1][0] == ("{" if c == "}" else "["))
            stack.pop()
            pos += 1
        else:
            if c == '"':
                end = pos + 1
                while end < len(text):
                    if text[end] == "\\":
                        end += 2
                    elif text[end] == '"':
                        break
                    else:
                        end += 1
                require(end < len(text) and end - pos <= 1540)
                value = json.loads(text[pos:end + 1])
                encoded = value.encode("utf-8", "strict")
                look = end + 1
                while look < len(text) and text[look] in " \r\n\t":
                    look += 1
                is_key = look < len(text) and text[look] == ":"
                require(len(encoded) <= (64 if is_key else 256))
                if is_key:
                    require(bool(stack) and stack[-1][0] == "{")
                    stack[-1][1] += 1
                    require(stack[-1][1] <= 32)
                pos = end + 1
            else:
                match = re.match(r"(?:-?(?:0|[1-9][0-9]*)(?:\.[0-9]+)?(?:[eE][+-]?[0-9]+)?|true|false|null)", text[pos:])
                require(match is not None)
                lexeme = match.group(0)
                if lexeme[0] not in "tfn":
                    require(len(lexeme) <= 64)
                    exp = int(re.split("[eE]", lexeme)[1]) if "e" in lexeme.lower() else 0
                    require(-24 <= exp <= 24 and D(lexeme).copy_abs() <= D("1e24"))
                pos += len(lexeme)
                is_key = False
            if stack and stack[-1][0] == "[" and not is_key:
                stack[-1][1] += 1
                require(stack[-1][1] <= 2048)
    require(not stack)

    def pairs(items):
        out = {}
        for key, value in items:
            require(key not in out)
            out[key] = value
        return out
    return json.loads(text, object_pairs_hook=pairs, parse_float=D, parse_int=lambda value: D(value) if value == "-0" else int(value),
                      parse_constant=lambda _: (_ for _ in ()).throw(SnapshotUnavailable()))


def intervals(spans):
    result = []
    for start, end in spans:
        if result and result[-1][1] == start:
            result[-1] = (result[-1][0], end)
        else:
            result.append((start, end))
    return result


def validate(obj, instrument, epoch, previous=None):
    fields(obj, ("schemaVersion", "type", "instrument", "requestEpoch", "observedAt", "identity", "contract", "status"))
    require(type(obj["schemaVersion"]) is str and re.fullmatch(r"1\.(?:0|[1-9][0-9]?)", obj["schemaVersion"]) is not None)
    require(instrument in ("BTC/USD", "ETH/USD") and obj["instrument"] == instrument)
    require(counter(obj["requestEpoch"]) == epoch)
    observed = timestamp(obj["observedAt"], True)
    ident = obj["identity"]
    identity(ident)
    require(ident["displayDate"] == observed.date().isoformat())
    contract = obj["contract"]
    fields(contract, ("researchAlgorithm", "transformAlgorithm", "intervalSeconds"))
    require(contract["researchAlgorithm"] == "aiq-r115-research-v1" and
            contract["transformAlgorithm"] == "aiq-r115-transform-v1" and
            type(contract["intervalSeconds"]) is int and contract["intervalSeconds"] == 60)
    kind = obj["type"]
    require(kind in ("full", "unchanged"))
    if previous is not None and previous["identity"]["generation"] == ident["generation"]:
        old = previous["identity"]
        require(int(ident["version"]) >= int(old["version"]) and int(ident["transformEpoch"]) >= int(old["transformEpoch"]) and ident["displayDate"] >= old["displayDate"])
        require(observed >= timestamp(previous["observedAt"], True))
    if kind == "unchanged":
        require(not any(k in obj for k in COLLECTIONS) and previous is not None and previous["identity"] == ident)
        require(previous["schemaVersion"] == obj["schemaVersion"] and previous["contract"] == contract)
        data = {k: previous[k] for k in COLLECTIONS}
    else:
        fields(obj, COLLECTIONS)
        data = {k: obj[k] for k in COLLECTIONS}
        if previous is not None and previous["identity"] == ident:
            require(previous["schemaVersion"] == obj["schemaVersion"] and previous["contract"] == contract and all(previous[k] == data[k] for k in COLLECTIONS))
    for k, cap in zip(COLLECTIONS, (1000, 1000, 1000, 2000, 1000, 999)):
        require(type(data[k]) is list and len(data[k]) <= cap)
    candles, analytics, classes = (data[k] for k in COLLECTIONS[:3])
    require(len(candles) == len(analytics) == len(classes))
    times, gaps, unavailable = [], [], []
    by_time = {}
    previous_row = None
    previous_time = None
    seen_segments = set()
    latest_valid = None
    for row, a, cl in zip(candles, analytics, classes):
        fields(row, ("time", "segment", "segmentIndex", "open", "high", "low", "close", "volume"))
        t = timestamp(row["time"])
        require(t + dt.timedelta(minutes=1) <= observed)
        identifier(row["segment"])
        index = counter(row["segmentIndex"], True)
        if previous_row is not None:
            require(t > previous_time)
            if row["segment"] == previous_row["segment"]:
                require(index == int(previous_row["segmentIndex"]) + 1 and t == previous_time + dt.timedelta(minutes=1))
            else:
                require(index == 1 and row["segment"] not in seen_segments)
            if t > previous_time + dt.timedelta(minutes=1):
                gaps.append((previous_time + dt.timedelta(minutes=1), t))
                require(row["segment"] != previous_row["segment"])
        seen_segments.add(row["segment"])
        o, h, lo, c = (quantity(row[k], "4e9", positive=True) for k in ("open", "high", "low", "close"))
        require(lo <= min(o, c) <= max(o, c) <= h)
        quantity(row["volume"], "1e12", nonnegative=True)
        fields(a, ("time",) + INDICATORS)
        require(a["time"] == row["time"])
        first_positions = (20, 20, 20, 20, 20, 15, 26, 34, 34, 14, 14, 20, 25)
        for name, first in zip(INDICATORS, first_positions):
            v = a[name]
            require(v is None or index >= first)
            if v is not None:
                quantity(v, "100" if name == "rsi14" else "1e24" if name in INDICATORS[10:] else "4e9",
                         nonnegative=name in ("rsi14", "atr14", "normalizedAtr14", "bollingerWidth"))
        fields(cl, ("time", "segment", "validity", "trendScore", "trend", "regime"))
        require(cl["time"] == row["time"] and cl["segment"] == row["segment"])
        require(cl["validity"] in ("Valid", "Invalidated", "Unavailable"))
        if cl["validity"] == "Valid":
            require(index >= 401 and all(a[k] is not None for k in INDICATORS))
            score = cl["trendScore"]
            require(type(score) is int and -3 <= score <= 3)
            require(cl["trend"] == ("Bullish" if score >= 2 else "Bearish" if score <= -2 else "Neutral"))
            require(cl["regime"] in REGIMES)
            latest_valid = row["time"]
        else:
            require(all(cl[k] is None for k in ("trendScore", "trend", "regime")))
            unavailable.append((t, t + dt.timedelta(minutes=1)))
        times.append(t)
        by_time[row["time"]] = (row, cl)
        previous_row, previous_time = row, t
    seen_events, last_event = set(), None
    for event in data["events"]:
        fields(event, ("time", "segment", "type", "previousTrend", "trend", "regime"))
        require(event["time"] in by_time and event["type"] in ("SegmentStart", "TrendTransition"))
        row, cl = by_time[event["time"]]
        require(event["segment"] == row["segment"])
        order = (timestamp(event["time"]), 0 if event["type"] == "SegmentStart" else 1)
        require(order not in seen_events and (last_event is None or order > last_event))
        seen_events.add(order)
        last_event = order
        if event["type"] == "SegmentStart":
            require(int(row["segmentIndex"]) == 1 and all(event[k] is None for k in ("previousTrend", "trend", "regime")))
        else:
            require(cl["validity"] == "Valid" and event["trend"] == cl["trend"] and event["regime"] == cl["regime"])
            require(event["previousTrend"] in TRENDS or event["previousTrend"] is None)
            require(event["previousTrend"] is None or event["previousTrend"] != event["trend"])
            earlier = [r[1] for r in by_time.values() if r[0]["segment"] == event["segment"] and r[0]["time"] < event["time"] and r[1]["validity"] == "Valid"]
            if earlier:
                require(event["previousTrend"] == earlier[-1]["trend"])
            elif int(row["segmentIndex"]) == 401:
                require(event["previousTrend"] is None)
            else:
                require(event["previousTrend"] in TRENDS)
    for name, expected in (("researchUnavailableIntervals", intervals(unavailable)), ("marketDataGapIntervals", gaps)):
        actual = []
        for item in data[name]:
            fields(item, ("start", "end"))
            start, end = timestamp(item["start"]), timestamp(item["end"])
            require(start < end)
            actual.append((start, end))
        require(actual == expected)
    status = obj["status"]
    fields(status, ("observedAt", "research", "freshness", "integrity", "classificationValidity",
                    "lastCompletedCandleAt", "lastSuccessfulProviderPollAt", "lastCommitAt", "researchAsOfCandleAt"))
    require(status["observedAt"] == obj["observedAt"])
    require(status["research"] in ("Ready", "Recalibrating", "Recovering", "Unavailable"))
    require(status["integrity"] in ("Protected", "Degraded", "Recovery"))
    validity = classes[-1]["validity"] if classes else "Unavailable"
    require(status["classificationValidity"] == validity and status["researchAsOfCandleAt"] == latest_valid)
    require(status["lastCompletedCandleAt"] == (candles[-1]["time"] if candles else None))
    freshness = "Unavailable" if not candles else "Current" if observed - (times[-1] + dt.timedelta(minutes=1)) <= dt.timedelta(seconds=120) else "Stale"
    require(status["freshness"] == freshness)
    for k in ("lastSuccessfulProviderPollAt", "lastCommitAt"):
        if status[k] is not None:
            require(timestamp(status[k], True) <= observed)
    if candles:
        require(int(ident["version"]) > 0 and status["lastCommitAt"] is not None and status["lastSuccessfulProviderPollAt"] is not None)
        require(times[-1] + dt.timedelta(minutes=1) <= timestamp(status["lastSuccessfulProviderPollAt"], True))
    else:
        require(ident["version"] == "0" and status["research"] == "Unavailable" and status["integrity"] == "Protected" and status["lastCommitAt"] is None)
    if status["research"] == "Ready":
        require(validity == "Valid" and status["integrity"] != "Recovery")
    if validity == "Valid":
        require(status["research"] == "Ready")
    if status["integrity"] == "Recovery":
        require(status["research"] == "Recovering" and validity != "Valid")
    result = copy.deepcopy(obj)
    result.update(copy.deepcopy(data))
    return result


def freeze(value):
    if isinstance(value, dict):
        return types.MappingProxyType({k: freeze(v) for k, v in value.items()})
    if isinstance(value, list):
        return tuple(freeze(v) for v in value)
    return value


class ResearchSnapshotClient:
    def __init__(self, base_url, api_key, timeout=5, ca_file=None):
        url = urllib.parse.urlsplit(base_url)
        require(url.scheme == "https" and url.hostname and not url.username and not url.password and
                not url.query and not url.fragment and url.path in ("", "/"))
        require(type(api_key) is str and 16 <= len(api_key) <= 256 and all(33 <= ord(c) <= 126 for c in api_key))
        require(type(timeout) in (int, float) and 0 < timeout <= 5)
        self._host, self._port = url.hostname, url.port or 443
        self._key = api_key
        self._ssl = ssl.create_default_context(cafile=ca_file)
        self._timeout = timeout
        self._epochs = {}
        self._cache = {}
        self._connectivity = {}
        self._availability = {}
        self._syncs = {}
        self._guard = threading.RLock()
        self._flight = threading.Lock()
        self.connected = False
        self.last_successful_sync = None

    def connection_state(self, instrument):
        with self._guard:
            return ("Connected" if self._connectivity.get(instrument, False) else "Disconnected", self._syncs.get(instrument))

    def availability(self, instrument):
        with self._guard:
            return self._availability.get(instrument, "NotObserved")

    def snapshot(self, instrument):
        with self._guard:
            return freeze(copy.deepcopy(self._cache.get(instrument)))

    def begin(self, instrument):
        with self._guard:
            require(instrument in ("BTC/USD", "ETH/USD"))
            epoch = self._epochs.get(instrument, 0) + 1
            counter(str(epoch))
            self._epochs[instrument] = epoch
            previous = self._cache.get(instrument)
            return {"schemaVersion": "1.0", "instrument": instrument, "requestEpoch": str(epoch),
                    "knownIdentity": copy.deepcopy(previous["identity"]) if previous else None}

    def accept(self, data, instrument, epoch):
        with self._guard:
            try:
                require(self._epochs.get(instrument) == epoch)
                result = validate(parse(data), instrument, epoch, self._cache.get(instrument))
                self._cache[instrument] = result
                self.connected = True
                self.last_successful_sync = dt.datetime.now(dt.timezone.utc)
                self._connectivity[instrument] = True
                self._availability[instrument] = "Ready" if result["candles"] else "DatasetEmpty"
                self._syncs[instrument] = self.last_successful_sync
                return freeze(copy.deepcopy(result))
            except (ValueError, TypeError, KeyError, OverflowError, UnicodeError, SnapshotUnavailable, decimal.InvalidOperation):
                self.connected = False
                self._connectivity[instrument] = False
                raise SnapshotUnavailable() from None

    def fetch(self, instrument):
        with self._flight:
            request = self.begin(instrument)
            connection = None
            started = time.monotonic()
            reason = "InvalidResponse"
            try:
                connection = http.client.HTTPSConnection(self._host, self._port, timeout=self._timeout, context=self._ssl)
                # Direct HTTPSConnection does not inherit proxies or follow redirects.
                connection.connect()  # Native DNS may exceed timeout; accepted platform limitation.
                if time.monotonic() - started >= self._timeout:
                    raise TimeoutError()
                connection.sock.settimeout(max(0.001, self._timeout - (time.monotonic() - started)))
                connection.request("POST", "/v1/research/snapshot", body=json.dumps(request, separators=(",", ":")).encode("utf-8"),
                                   headers={"Content-Type": "application/json", "X-API-Key": self._key, "Accept-Encoding": "identity"})
                response = connection.getresponse()
                require(response.status in (200, 401, 429, 503, 504))
                limit = LIMIT if response.status == 200 else 4096
                headers = response.getheaders()
                for name in ("Content-Type", "Content-Encoding", "Content-Length"):
                    require(sum(k.lower() == name.lower() for k, _ in headers) <= 1)
                media = response.getheader("Content-Type", "").lower().replace(" ", "")
                require(media in ("application/json", "application/json;charset=utf-8"))
                require(response.getheader("Content-Encoding", "identity").lower() == "identity")
                length = response.getheader("Content-Length")
                require(length is None or re.fullmatch(r"[0-9]+", length) is not None and int(length) <= limit)
                body = bytearray()
                while True:
                    remaining = self._timeout - (time.monotonic() - started)
                    if remaining <= 0:
                        raise TimeoutError()
                    if connection.sock is not None:
                        connection.sock.settimeout(remaining)
                    chunk = response.read1(min(8192, limit + 1 - len(body)))
                    if not chunk:
                        break
                    body.extend(chunk)
                    require(len(body) <= limit)
                require(time.monotonic() - started <= self._timeout and (length is None or int(length) == len(body)))
                if response.status != 200:
                    reason = failure_reason(bytes(body), response.status, instrument, request["requestEpoch"])
                    raise SnapshotUnavailable()
                return self.accept(bytes(body), instrument, int(request["requestEpoch"]))
            except (OSError, http.client.HTTPException, ValueError, TypeError, SnapshotUnavailable) as error:
                if isinstance(error, TimeoutError):
                    reason = "TimedOut"
                elif isinstance(error, OSError):
                    reason = "Unreachable"
                with self._guard:
                    self.connected = False
                    self._connectivity[instrument] = False
                    self._availability[instrument] = reason
                raise SnapshotUnavailable() from None
            finally:
                if connection is not None:
                    connection.close()


if __name__ == "__main__":
    import os
    try:
        client = ResearchSnapshotClient(os.environ["AIQ_RESEARCH_BASE_URL"], os.environ["AIQ_RESEARCH_API_KEY"],
                                        ca_file=os.environ.get("AIQ_RESEARCH_CA_FILE"))
        for instrument in ("BTC/USD", "ETH/USD"):
            first = client.fetch(instrument)
            second = client.fetch(instrument)
            require(first["type"] == "full" and second["type"] == "unchanged")
            print(instrument + " authenticated full/unchanged PASS")
    except (KeyError, ValueError, OSError, SnapshotUnavailable):
        print("Research snapshot qualification unavailable.", file=__import__("sys").stderr)
        raise SystemExit(1)
