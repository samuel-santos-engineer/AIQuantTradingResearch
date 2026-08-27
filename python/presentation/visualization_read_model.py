"""Read-only WP05 local atomic-JSON visualization consumer."""
from __future__ import annotations
from dataclasses import dataclass
from decimal import Decimal
import json, os, time
from pathlib import Path
from typing import Callable

CONTRACT_VERSION = "aiq-visualization-read-model-v1"
DEFAULT_REFRESH_SECONDS, MIN_REFRESH_SECONDS, MAX_REFRESH_SECONDS = 2, 1, 60
_IDEMPOTENCY_STATUSES = ("NewlyPersisted", "EquivalentExisting", "Unavailable")
_DATA_QUALITY_STATUSES = ("Valid", "Invalid", "Unavailable")

class ConfigurationError(ValueError): pass
class ReadModelError(ValueError): pass

def resolve_handoff_path(environ: dict[str, str] | None = None) -> Path:
    env = os.environ if environ is None else environ
    value = env.get("Visualization__HandoffPath")
    if value:
        path = Path(value)
        if not path.is_absolute(): raise ConfigurationError("Visualization__HandoffPath must be absolute")
        return path.resolve()
    base = env.get("LOCALAPPDATA")
    if not base: raise ConfigurationError("LOCALAPPDATA is required for the default handoff path")
    return (Path(base) / "AIQuantTradingResearch" / "Release1.9" / "runtime" / "visualization-read-model.json").resolve()

def refresh_interval_seconds(environ: dict[str, str] | None = None) -> int:
    env = os.environ if environ is None else environ; value = env.get("Visualization__RefreshIntervalSeconds")
    if value is None or value == "": return DEFAULT_REFRESH_SECONDS
    try: interval = int(value)
    except ValueError as exc: raise ConfigurationError("Visualization__RefreshIntervalSeconds must be an integer from 1 to 60") from exc
    if str(interval) != value or not MIN_REFRESH_SECONDS <= interval <= MAX_REFRESH_SECONDS: raise ConfigurationError("Visualization__RefreshIntervalSeconds must be an integer from 1 to 60")
    return interval

@dataclass(frozen=True)
class Envelope:
    raw: dict
    @property
    def revision(self) -> dict: return self.raw["revision"]

def parse_envelope(text: str) -> Envelope:
    try: payload = json.loads(text, parse_float=Decimal)
    except (json.JSONDecodeError, UnicodeDecodeError) as exc: raise ReadModelError("ReadIntegrity") from exc
    if not isinstance(payload, dict) or payload.get("contractVersion") != CONTRACT_VERSION: raise ReadModelError("UnsupportedVersion")
    revision = payload.get("revision")
    if not isinstance(revision, dict) or revision.get("kind") not in ("HistoricalPresentation", "ReplayLogicalTick") or not isinstance(revision.get("value"), int) or revision["value"] < 0 or not isinstance(revision.get("identity"), str) or not revision["identity"]: raise ReadModelError("ReadIntegrity")
    if payload.get("state") not in ("Ready", "WarmUp", "Empty", "Stale", "Failed") or payload.get("sourceMode") not in ("Historical", "Replay") or payload.get("sourceAuthority") not in (0, 1): raise ReadModelError("ReadIntegrity")
    payload["idempotencyStatus"] = payload.get("idempotencyStatus", "Unavailable")
    payload["dataQualityStatus"] = payload.get("dataQualityStatus", "Unavailable")
    if payload["idempotencyStatus"] not in _IDEMPOTENCY_STATUSES or payload["dataQualityStatus"] not in _DATA_QUALITY_STATUSES: raise ReadModelError("ReadIntegrity")
    return Envelope(payload)

def compare_revision(candidate: Envelope, current: Envelope) -> str:
    a, b = candidate.revision, current.revision
    if a["kind"] != b["kind"] or candidate.raw["sourceMode"] != current.raw["sourceMode"]: return "context"
    if a["value"] > b["value"]: return "newer"
    if a["value"] < b["value"]: return "older"
    return "equivalent" if a["identity"] == b["identity"] else "conflict"

class ReadModelCache:
    def __init__(self) -> None: self.last_good: Envelope | None = None
    def refresh(self, path: Path, read: Callable[[Path], str] | None = None, sleep: Callable[[float], None] = time.sleep) -> str | None:
        if not path.exists(): return "ProducerUnavailable"
        reader = read or (lambda item: item.read_text(encoding="utf-8"))
        error: ReadModelError | None = None
        for attempt in range(2):
            try:
                candidate = parse_envelope(reader(path)); error = None; break
            except (OSError, ReadModelError) as exc:
                error = exc if isinstance(exc, ReadModelError) else ReadModelError("TransportRead")
                if attempt == 0: sleep(0.05)
        if error is not None: return str(error)
        if self.last_good is None: self.last_good = candidate; return None
        relation = compare_revision(candidate, self.last_good)
        if relation in ("newer", "context"): self.last_good = candidate; return None
        return None if relation == "equivalent" else ("OlderRevision" if relation == "older" else "RevisionConflict")
