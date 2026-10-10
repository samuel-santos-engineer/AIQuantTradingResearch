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
import selectors
import signal
import stat
import sys
import time
import uuid
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


# Frozen WP02 server qualification values; never learned from child output.
QUALIFIED_DATASETS = MappingProxyType({'1h': '5b187d5bea1fd202077c33713855f9c0518446ce6a8864a5249daa0e7e45dc29', '4h': 'd713767c7afeb5d3f535e8276e1cecb905b3f2d6c096aac5a62214f62ccc22ec', '1d': '0fc878f11193073aa88f71b026825c9f7cbb4cb18d5c87fec2d66a9e44291c81'})
QUALIFIED_WINDOWS = MappingProxyType({('1h', '1D'): '52008a41d0581292bc72b29e6fd1273075b08fedc95b9c82bf7570784cb92cfb', ('1h', '7D'): '1da0ac6ac728048f1751f2a0c0f48b5db8fa0c1942174f51afcadb1e29157a04', ('1h', '30D'): '2c14463a88aa3cfd452cfd5d6539b78f12b1f8110cd03791c85da15e82a564c4', ('1h', '90D'): '0346dcb017ad45a4867d34d458db777d02215843f1a555ef7038bf9b80140d9e', ('4h', '1D'): '513ee57961faa5bb633f6f9bfd36128b8af366d1ba276fa79dab662889e2d8f8', ('4h', '7D'): 'e16132d5648cc79f086a254b933dd08b9ea6afc4dec7c370089cc389c88c7bc2', ('4h', '30D'): '9bd4498592a669f394173ca46f4a20a2bf79e3e5fea306d72ef208f038e37a25', ('4h', '90D'): 'b2cf082d689ab93e38ed879f5268aff6f1cac84769044a894b6c4cd25c99397f', ('1d', '1D'): 'a5e98a46b5aaa62af444368ff269855886f5f84dd8e82d33d1087067ed2237dd', ('1d', '7D'): '735aee58440fcf4f4ba50c3000c9d54378abce696cc464c6f9518be5d1ed7e38', ('1d', '30D'): '43451b12a772287c65536556f400e4bd0676505c169a8d5096761787a6ad3f38', ('1d', '90D'): '85e6e5952eb35fc37e741eed3d0b4169fe933954cd4eb7dc5531ef5013e35cdf'})
QUALIFIED_VISIBLE = MappingProxyType({('1h', '1D'): '36ac833f75709662a319833cc4b76eec2644912e9e81213145d52b3b923afbc6', ('1h', '7D'): '91aaf2d564ad150d321d8e94587661f1c1b9df5fd2d9cfc2e756d7188febe71f', ('1h', '30D'): '36b544790c2b90161cfd729a654c9314e2b477eade8cb0bb109162ce9bb934ac', ('1h', '90D'): 'b71c04ace336fccd77f8a317372a0c13626b371aa32700cfe0ecf4918e75eb12', ('4h', '1D'): 'c77eabc3699e29bb29a0581f8baa84f3c14761fb7173bcb67cd48b19283d21d1', ('4h', '7D'): 'f59bff85e6c3fc0401138f901e2df85e05f7068b2e1a743b19ecdb745189cb03', ('4h', '30D'): '75d8030c3714d7e18fedfe9bb2a622321e82ab377ddc7bacab14e3f8a63b0814', ('4h', '90D'): '40795cf048985613d3379503114a29178b1b8ee49da5fd3dc47e5eee91367d38', ('1d', '1D'): 'e32e09e717b1aff555e8cc620d1ff40cc64c232c866f1391b89bb5c0cc732873', ('1d', '7D'): '2339827d95c7469e890c842fcf58c5d862c8c66199df0770e294eeddff9d4373', ('1d', '30D'): '4d9e7cb57dc6834ad7ef35265191d7c465c1bfad51fafcaef2871ee831124d14', ('1d', '90D'): '27bbc5f787b71b064986a457da0ccae673cf1b41641f87df162b5bdbf2be61b8'})

PUBLIC_CACHE = Path('/home/aiq-public-synthetic-cache')
PUBLIC_CGROUP = Path('/sys/fs/cgroup/aiq-public')
MAX_STDERR_BYTES, READ_CHUNK = 16384, 8192
CPU_RESERVATION_US = 16000000  # conservative one-core 15-second lifecycle plus quota-period margin
# Parent-crash guardian and delegated-kernel verification are required before activation.
# This gate is deliberately not configurable through a request or inherited environment.
PUBLIC_RUNTIME_VERIFIED = False

def public_child_environment():
    # No values are copied from os.environ, including PATH, proxy or runtime overrides.
    return {'PATH': '/usr/share/dotnet:/usr/bin:/bin', 'DOTNET_ROOT': '/usr/share/dotnet',
            'HOME': '/home/aiq', 'TMPDIR': '/tmp/aiq-public', 'LANG': 'C.UTF-8', 'LC_ALL': 'C.UTF-8', 'TZ': 'UTC',
            'DOTNET_CLI_TELEMETRY_OPTOUT': '1', 'DOTNET_NOLOGO': '1', 'DOTNET_PROCESSOR_COUNT': '1',
            'Worker__Mode': 'HistoricalQuery', 'HistoricalMarketQuery__CacheDirectory': str(PUBLIC_CACHE)}

def _safe_directory(path):
    if not path.is_absolute() or '..' in path.parts:
        raise BridgeError('BridgeUnavailable')
    for item in (path, *path.parents):
        if item.is_symlink(): raise BridgeError('BridgeUnavailable')
    if not path.is_dir() or path.stat().st_uid != os.geteuid() or path.stat().st_mode & 0o022:
        raise BridgeError('BridgeUnavailable')

def _verify_public_filesystem():
    if sys.platform != 'linux': raise BridgeError('BridgeUnavailable')
    # A dedicated public deployment must not contain even readable legacy/private runtime stores.
    for root in ('/home/aiq-private-research-cache', '/home/aiq-market-cache', '/runtime'):
        if os.path.lexists(root): raise BridgeError('BridgeUnavailable')
    _safe_directory(PUBLIC_CACHE)
    _safe_directory(Path('/tmp/aiq-public'))
    # Reject extra application-data mounts, including Azure's broad /home mount. Fixed runtime
    # mounts are inspected, not accepted based on a caller's claimed public-mode flag.
    allowed = {'/', '/proc', '/sys', '/sys/fs/cgroup', '/dev', '/dev/pts', '/dev/shm',
               '/proc/acpi', '/proc/scsi', '/proc/interrupts', '/proc/kcore', '/proc/keys', '/proc/latency_stats',
               '/proc/timer_list', '/proc/sysrq-trigger', '/proc/bus', '/proc/fs', '/proc/irq', '/proc/sys',
               '/etc/resolv.conf', '/etc/hostname', '/etc/hosts', '/home/aiq-public-synthetic-cache'}
    with open('/proc/self/mountinfo', 'rb') as source:
        data = source.read(65537)
    if len(data) > 65536: raise BridgeError('BridgeUnavailable')
    for line in data.decode('ascii', errors='strict').splitlines():
        fields = line.split()
        if len(fields) < 7 or fields[4] not in allowed: raise BridgeError('BridgeUnavailable')

def _read_small(path, limit=65536):
    fd = os.open(path, os.O_RDONLY | os.O_NOFOLLOW)
    try:
        if not stat.S_ISREG(os.fstat(fd).st_mode): raise BridgeError('BridgeUnavailable')
        data = os.read(fd, limit + 1)
        if len(data) > limit: raise BridgeError('BridgeUnavailable')
        return data.decode('ascii', errors='strict')
    finally: os.close(fd)

def _atomic_ledger(path, value):
    data = json.dumps(value, separators=(',', ':')).encode('ascii')
    if len(data) > 32768: raise BridgeError('BridgeUnavailable')
    temporary = path.with_name(path.name + '.tmp')
    fd = os.open(temporary, os.O_WRONLY | os.O_CREAT | os.O_EXCL | os.O_NOFOLLOW, 0o600)
    try:
        with os.fdopen(fd, 'wb') as destination:
            destination.write(data); destination.flush(); os.fsync(destination.fileno())
        os.replace(temporary, path)
        directory = os.open(path.parent, os.O_RDONLY | os.O_DIRECTORY)
        try: os.fsync(directory)
        finally: os.close(directory)
    finally:
        if temporary.exists(): temporary.unlink()

class _Cgroup:
    def __init__(self, root=PUBLIC_CGROUP):
        self.root, self.path = root, root / 'request'
        if root.is_symlink() or not (root / 'cgroup.controllers').exists(): raise BridgeError('BridgeUnavailable')
        if 'cpu' not in _read_small(root / 'cgroup.subtree_control', 4096).split(): raise BridgeError('BridgeUnavailable')

    def create(self):
        self.path.mkdir(mode=0o700)
        (self.path / 'cpu.max').write_text('10000 10000', encoding='ascii')
        if _read_small(self.path / 'cpu.max', 4096).strip() != '10000 10000': raise BridgeError('BridgeUnavailable')
        if not (self.path / 'cgroup.kill').exists(): raise BridgeError('BridgeUnavailable')

    def attach(self, pid): (self.path / 'cgroup.procs').write_text(str(pid), encoding='ascii')
    def usage(self):
        values = dict(line.split() for line in _read_small(self.path / 'cpu.stat', 4096).splitlines())
        usage = int(values['usage_usec'])
        if usage < 0: raise BridgeError('BridgeUnavailable')
        return usage
    def populated(self): return 'populated 1' in _read_small(self.path / 'cgroup.events', 4096).splitlines()
    def kill(self): (self.path / 'cgroup.kill').write_text('1', encoding='ascii')
    def remove(self): self.path.rmdir()

class _Admission:
    def __init__(self, started, clock=time.monotonic, wall=time.time_ns, root=PUBLIC_CACHE, group=None):
        self.started, self.clock, self.wall, self.root = started, clock, wall, root
        self.group = group
        self.fd = None
        self.state = None
        self.token = uuid.uuid4().hex
    def __enter__(self):
        import fcntl
        self.fd = os.open(self.root / '.public-runtime.lock', os.O_RDWR | os.O_CREAT | os.O_NOFOLLOW, 0o600)
        try:
            if not stat.S_ISREG(os.fstat(self.fd).st_mode): raise BridgeError('BridgeUnavailable')
            while True:
                try: fcntl.flock(self.fd, fcntl.LOCK_EX | fcntl.LOCK_NB); break
                except BlockingIOError:
                    if self.clock() >= self.started + 1: raise BridgeError('Busy')
                    time.sleep(0.01)
            self._load()
            return self
        except BaseException:
            os.close(self.fd); self.fd = None; raise
    def _load(self):
        path, marker = self.root / '.public-runtime-ledger.json', self.root / '.public-runtime-initialized'
        now = self.wall()
        if path.exists():
            self.state = _load(_read_small(path, 32768), 32768)
        elif marker.exists(): raise BridgeError('BridgeUnavailable')
        else:
            self.state = {'last': now, 'entries': [], 'pending': None}
            _atomic_ledger(path, self.state)
            fd = os.open(marker, os.O_WRONLY | os.O_CREAT | os.O_EXCL | os.O_NOFOLLOW, 0o600); os.close(fd)
        state = self.state
        if not isinstance(state, dict) or set(state) != {'last', 'entries', 'pending'} or type(state['last']) is not int or now < state['last']:
            raise BridgeError('BridgeUnavailable')
        if not isinstance(state['entries'], list) or len(state['entries']) > 256: raise BridgeError('BridgeUnavailable')
        for entry in state['entries']:
            if not isinstance(entry, list) or len(entry) != 2 or any(type(v) is not int or v < 0 for v in entry) or entry[0] > now:
                raise BridgeError('BridgeUnavailable')
        if state['pending'] is not None:
            if not isinstance(state['pending'], dict) or set(state['pending']) != {'token', 'reserved'} or not re.fullmatch('[0-9a-f]{32}', str(state['pending']['token'])) or state['pending']['reserved'] != CPU_RESERVATION_US:
                raise BridgeError('BridgeUnavailable')
            # Do not expire a reservation by timestamp. Kernel containment must be emptied first.
            if self.group.path.exists():
                self.group.kill()
                if self.group.populated(): raise BridgeError('Busy')
                usage = self.group.usage()
                if usage > CPU_RESERVATION_US: raise BridgeError('BridgeUnavailable')
                self.group.remove()
            # Conservatively charge the whole abandoned reservation, even when usage is unknown.
            state['entries'].append([now, CPU_RESERVATION_US]); state['pending'] = None
        elif self.group.path.exists(): raise BridgeError('BridgeUnavailable')
        state['entries'] = [entry for entry in state['entries'] if now - entry[0] <= 86400 * 1000000000]
        short = sum(cost for at, cost in state['entries'] if now - at <= 900 * 1000000000)
        day = sum(cost for _, cost in state['entries'])
        if short + CPU_RESERVATION_US > 60000000 or day + CPU_RESERVATION_US > 240000000: raise BridgeError('Busy')
        state['last'] = now
        state['pending'] = {'token': self.token, 'reserved': CPU_RESERVATION_US}
        _atomic_ledger(path, state)
    def settle(self):
        if self.group.populated(): raise BridgeError('BridgeUnavailable')
        usage = self.group.usage()
        if usage > CPU_RESERVATION_US: raise BridgeError('BridgeUnavailable')
        self.group.remove()
        if self.state['pending']['token'] != self.token: raise BridgeError('BridgeUnavailable')
        now = self.wall()
        if now < self.state['last']: raise BridgeError('BridgeUnavailable')
        # Charge the complete conservative reservation, including the single-threaded
        # Python launch gate before cgroup attachment. Do not reclaim unmeasured launch CPU.
        self.state['entries'].append([now, CPU_RESERVATION_US]); self.state['last'] = now; self.state['pending'] = None
        _atomic_ledger(self.root / '.public-runtime-ledger.json', self.state)
    def __exit__(self, *_):
        if self.fd is not None: os.close(self.fd); self.fd = None

def _capture(process, request, operation_end, total_end, group, cancel=None, clock=time.monotonic):
    stdout, stderr_count = bytearray(), 0
    with selectors.DefaultSelector() as selector:
        for pipe, name in ((process.stdout, 'stdout'), (process.stderr, 'stderr'), (process.stdin, 'stdin')):
            os.set_blocking(pipe.fileno(), False)
            selector.register(pipe, selectors.EVENT_WRITE if name == 'stdin' else selectors.EVENT_READ, name)
        sent = 0
        while selector.get_map():
            if clock() >= min(operation_end, total_end - 2) or (cancel is not None and cancel.is_set()): raise BridgeError('Timeout')
            if group.usage() > 10000000: raise BridgeError('Timeout')
            for key, _ in selector.select(0.01):
                if key.data == 'stdin':
                    written = os.write(key.fd, request[sent:sent + READ_CHUNK]); sent += written
                    if sent == len(request): selector.unregister(key.fileobj); process.stdin.close()
                else:
                    block = os.read(key.fd, READ_CHUNK)
                    if not block: selector.unregister(key.fileobj); continue
                    if key.data == 'stdout':
                        if len(stdout) + len(block) > MAX_RESPONSE_BYTES: raise BridgeError('PayloadTooLarge')
                        stdout.extend(block)
                    else:
                        stderr_count += len(block)
                        if stderr_count > MAX_STDERR_BYTES: raise BridgeError('PayloadTooLarge')
        process.wait(timeout=max(0.001, min(operation_end, total_end - 2) - clock()))
        if process.returncode != 0: raise BridgeError('BridgeUnavailable')
    return bytes(stdout)

def _terminate(process, group, total_end, clock=time.monotonic):
    if process is not None:
        try: os.killpg(process.pid, signal.SIGTERM)
        except ProcessLookupError: pass
    grace = min(clock() + 1, total_end - 1)
    while group.path.exists() and group.populated() and clock() < grace: time.sleep(0.01)
    if group.path.exists(): group.kill()
    if process is not None:
        if process.poll() is None: process.kill()
        process.wait(timeout=max(0.001, total_end - clock()))
    while group.path.exists() and group.populated():
        if clock() >= total_end: raise BridgeError('BridgeUnavailable')
        time.sleep(0.01)

def _run_public(request, *, cancel=None):
    if not PUBLIC_RUNTIME_VERIFIED: raise BridgeError('BridgeUnavailable')
    _verify_public_filesystem()
    started = time.monotonic()
    group = _Cgroup()
    with _Admission(started, group=group) as admission:
        process = None
        read_gate, write_gate = os.pipe()
        try:
            group.create()
            # Spawn gate prevents exec before kernel containment is attached. If parent dies,
            # EOF exits the launcher; no private or uncontained Worker can start.
            launcher = 'import os,sys; fd=int(sys.argv[1]); ok=os.read(fd,1)==b"G"; os.close(fd); os.execve(sys.argv[2],sys.argv[2:],dict(os.environ)) if ok else sys.exit(1)'
            process = subprocess.Popen(['/opt/venv/bin/python', '-I', '-S', '-B', '-c', launcher, str(read_gate),
                                        '/usr/share/dotnet/dotnet', '/app/worker/AIQuantTradingResearch.Worker.dll'],
                                       stdin=subprocess.PIPE, stdout=subprocess.PIPE, stderr=subprocess.PIPE,
                                       env=public_child_environment(), cwd='/app', start_new_session=True,
                                       pass_fds=(read_gate,), close_fds=True)
            group.attach(process.pid)
            os.close(read_gate); read_gate = -1
            os.write(write_gate, b'G'); os.close(write_gate); write_gate = -1
            return _capture(process, request, time.monotonic() + 10, started + 15, group, cancel)
        finally:
            for fd in (read_gate, write_gate):
                if fd >= 0: os.close(fd)
            try:
                _terminate(process, group, started + 15)
                if group.path.exists(): admission.settle()
            finally:
                if process is not None:
                    for pipe in (process.stdin, process.stdout, process.stderr): pipe.close()

def invoke(symbol: str, interval: str, range_value: str, *, qualified_dataset_fingerprints: Mapping[str, str] | None = None, qualified_window_fingerprints: Mapping[tuple[str, str], str] | None = None, qualified_visible_fingerprints: Mapping[tuple[str, str], str] | None = None, cancel=None) -> MarketResponse:
    request = build_request(symbol, interval, range_value)
    validate_request(request)  # before any subprocess selection
    try:
        raw = _run_public(request.encode('ascii'), cancel=cancel)
    except (OSError, UnicodeError, ValueError, KeyError, subprocess.TimeoutExpired) as exc:
        raise BridgeError("BridgeUnavailable") from exc
    response = parse_response(raw, qualified_dataset_fingerprints=QUALIFIED_DATASETS if qualified_dataset_fingerprints is None else qualified_dataset_fingerprints, qualified_window_fingerprints=QUALIFIED_WINDOWS if qualified_window_fingerprints is None else qualified_window_fingerprints, qualified_visible_fingerprints=QUALIFIED_VISIBLE if qualified_visible_fingerprints is None else qualified_visible_fingerprints)
    if response.state == "Available" and (response.symbol, response.interval, response.range) != (symbol, interval, range_value):
        raise BridgeError("MalformedResponse")
    return response
