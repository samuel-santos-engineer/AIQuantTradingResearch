"""Candidate-only finite local handoff. No network, subprocess or indicator arithmetic."""
import hashlib
import logging
from itertools import islice
import json
import os
from pathlib import Path
import re
import stat
import threading
import time

from historical_market_bridge import BridgeError
from local_synthetic_chart import parse

ROOT = Path(__file__).resolve().parent / "_immutable_chart_candidate"
# Candidate provenance pin, not an independently qualified production release anchor.
MANIFEST_SHA256 = "2af51eff5f1d96eff63d1ad96905f6d4f1a4c112b08f3baffe0ad25c2fc66588"
NAMES = tuple(f"{i}-{r}.json" for i in ("1h", "4h", "1d") for r in ("1D", "7D", "30D", "90D"))
_LOCK = threading.BoundedSemaphore(1)


def enabled(server_address, *, environment=None):
    env = os.environ if environment is None else environment
    forbidden = any(k.upper().startswith(("WEBSITE", "TWELVEDATA", "VIKE", "PERSISTENCE__", "WORKER__REPLAY__", "VISUALIZATION__")) for k in env)
    if env.get("AIQ_RUNTIME_MODE") == "public-synthetic":
        private = any(k.upper().startswith(("TWELVEDATA", "VIKE", "PERSISTENCE__", "WORKER__REPLAY__", "VISUALIZATION__")) for k in env)
        return (os.name == "posix" and server_address == "0.0.0.0"
                and not private and env.get("AIQ_LOCAL_CHART") != "1"
                and env.get("AIQ_IMMUTABLE_CHART_CANDIDATE") != "1"
                and env.get("AIQ_LOCAL_IMAGE_QUALIFICATION", "0") == "0")
    if env.get("AIQ_RUNTIME_MODE") == "local-candidate":
        return (os.name == "posix" and server_address == "0.0.0.0"
                and env.get("AIQ_LOCAL_IMAGE_QUALIFICATION") == "1"
                and env.get("AIQ_LOCAL_CHART") != "1"
                and env.get("AIQ_IMMUTABLE_CHART_CANDIDATE") != "1"
                and all(env.get(k, "").lower() not in ("production", "staging")
                        for k in ("DOTNET_ENVIRONMENT", "ASPNETCORE_ENVIRONMENT", "ENVIRONMENT"))
                and not forbidden)
    return (server_address == "127.0.0.1" and env.get("DOTNET_ENVIRONMENT") == "Development"
            and env.get("AIQ_IMMUTABLE_CHART_CANDIDATE") == "1" and env.get("AIQ_LOCAL_CHART") != "1"
            and not any(k.upper().startswith(("WEBSITE", "TWELVEDATA", "VIKE", "PERSISTENCE__", "WORKER__REPLAY__", "VISUALIZATION__")) for k in env))


def _pairs(items):
    result = {}
    for k, v in items:
        if k in result:
            raise ValueError("duplicate")
        result[k] = v
    return result


def _json(raw):
    if not raw.endswith(b"\n") or b"\r" in raw or raw.startswith(b"\xef\xbb\xbf"):
        raise ValueError("serialization")
    def reject(_):
        raise ValueError("nonfinite")
    return json.loads(raw.decode("utf-8", errors="strict"), object_pairs_hook=_pairs, parse_constant=reject)


def _check_path(path, directory=False, *, image_owned=False):
    info = path.lstat()
    if stat.S_ISLNK(info.st_mode) or getattr(info, "st_file_attributes", 0) & 0x400:
        raise ValueError("link")
    if not (stat.S_ISDIR(info.st_mode) if directory else stat.S_ISREG(info.st_mode)):
        raise ValueError("type")
    if os.name == "posix" and (info.st_uid != (0 if image_owned else os.getuid()) or info.st_mode & (0o022 if directory or not image_owned else 0o222)):
        if image_owned:
            category = path.name if path.name in {"app", "python", "presentation", "_immutable_chart_candidate"} else ("filesystem-root" if path == path.parent else "member")
            logging.getLogger(__name__).warning("Immutable image ownership check: category=%s uid=%s mode=%o", category, info.st_uid, stat.S_IMODE(info.st_mode))
        raise ValueError("ownership")
    return info


def _read(path, limit, *, image_owned=False):
    before = _check_path(path, image_owned=image_owned)
    if not 0 < before.st_size <= limit:
        raise ValueError("size")
    fd = os.open(path, os.O_RDONLY | getattr(os, "O_NOFOLLOW", 0) | getattr(os, "O_BINARY", 0))
    with os.fdopen(fd, "rb") as stream:
        opened = os.fstat(stream.fileno())
        if (before.st_dev, before.st_ino, before.st_size, before.st_mtime_ns) != (opened.st_dev, opened.st_ino, opened.st_size, opened.st_mtime_ns):
            raise ValueError("replacement")
        raw = stream.read(limit + 1)
        after = os.fstat(stream.fileno())
    if ((opened.st_dev, opened.st_ino, opened.st_size, opened.st_mtime_ns, opened.st_ctime_ns)
            != (after.st_dev, after.st_ino, after.st_size, after.st_mtime_ns, after.st_ctime_ns)
            or len(raw) != before.st_size or len(raw) > limit):
        raise ValueError("mutation")
    return raw


def _load_candidate(root, pin, interval, range_value, *, image_owned=False):
    """Private proof seam; public acquisition never accepts a root or trust pin."""
    deadline = time.monotonic() + 2
    def budget():
        if time.monotonic() > deadline:
            raise ValueError("budget")
    _check_path(root, directory=True, image_owned=image_owned)
    # Reject links in ancestors; application directory ownership is checked separately.
    for parent in root.parents:
        if image_owned:
            _check_path(parent, directory=True, image_owned=True)
        if parent.is_symlink() or getattr(parent.lstat(), "st_file_attributes", 0) & 0x400:
            raise ValueError("linked ancestor")
    _check_path(root.parent, directory=True, image_owned=image_owned)
    if {p.name for p in islice(root.iterdir(), 14)} != {"manifest.json", *NAMES}:
        raise ValueError("inventory")
    raw = _read(root / "manifest.json", 8192, image_owned=image_owned)
    if hashlib.sha256(raw).hexdigest() != pin:
        raise ValueError("anchor")
    manifest = _json(raw)
    if (not isinstance(manifest, dict) or set(manifest) != {"schema", "generatorVersion", "seed", "files"}
            or manifest["schema"] != "aiq-chart-handoff-candidate-v3"
            or manifest["generatorVersion"] != "aiq-synthetic-ohlcv-v1"
            or type(manifest["seed"]) is not int or manifest["seed"] != 114
            or not isinstance(manifest["files"], list) or len(manifest["files"]) != 12):
        raise ValueError("manifest")
    selected, total = None, len(raw)
    for name, entry in zip(NAMES, manifest["files"]):
        budget()
        if (not isinstance(entry, dict) or set(entry) != {"name", "bytes", "sha256"} or entry["name"] != name
                or type(entry["bytes"]) is not int or not 0 < entry["bytes"] <= 1048576
                or not isinstance(entry["sha256"], str) or re.fullmatch(r"[a-f0-9]{64}", entry["sha256"]) is None):
            raise ValueError("member")
        raw = _read(root / name, 1048576, image_owned=image_owned)
        total += len(raw)
        if total > 4194304 or len(raw) != entry["bytes"] or hashlib.sha256(raw).hexdigest() != entry["sha256"]:
            raise ValueError("integrity")
        value = _json(raw)
        # Canonical fixed-scale strings; no Python/JS SMA calculation.
        if not isinstance(value, dict) or not isinstance(value.get("candles"), list) or len(value["candles"]) > 2160:
            raise ValueError("rows")
        for row in value["candles"]:
            if not isinstance(row, dict):
                raise ValueError("row")
            for key, digits in (("open", 4), ("high", 4), ("low", 4), ("close", 4), ("volume", 3), ("sma20", 12), ("ema20", 12), ("bollingerLower", 12), ("bollingerMiddle", 12), ("bollingerUpper", 12), ("rsi14", 12), ("macd", 12), ("macdSignal", 12), ("macdHistogram", 12)):
                text = row.get(key)
                sign = "-?" if key in ("bollingerLower", "macd", "macdSignal", "macdHistogram") else ""
                if not isinstance(text, str) or re.fullmatch(sign + r"(?:0|[1-9][0-9]{0,9})\.[0-9]{" + str(digits) + r"}", text) is None:
                    raise ValueError("decimal")
        i, r = name[:-5].split("-")
        response = parse(raw, i, r)  # Existing strict identity, counts, grid and frozen OHLCV fingerprint.
        if (i, r) == (interval, range_value):
            selected = response
        budget()
    if selected is None:
        raise ValueError("selection")
    return selected


def load(symbol, interval, range_value, *, server_address):
    if not enabled(server_address) or symbol != "btc-like-simulation" or f"{interval}-{range_value}.json" not in NAMES:
        raise BridgeError("ImmutableCandidateUnavailable")
    if not _LOCK.acquire(blocking=False):
        logging.getLogger(__name__).info("Immutable synthetic chart busy")
        raise BridgeError("ImmutableCandidateBusy")
    try:
        return _load_candidate(ROOT, MANIFEST_SHA256, interval, range_value, image_owned=os.name == "posix")
    except (OSError, ValueError, TypeError, KeyError, ArithmeticError, RecursionError) as exc:
        # Log only fixed categories; never payloads, paths, environment or credentials.
        reason = str(exc) if str(exc) in {"budget", "ownership", "linked ancestor", "inventory", "anchor", "manifest", "integrity", "selection"} else type(exc).__name__
        logging.getLogger(__name__).warning("Immutable synthetic chart unavailable: %s", reason)
        raise BridgeError("ImmutableCandidateUnavailable") from exc
    finally:
        _LOCK.release()
