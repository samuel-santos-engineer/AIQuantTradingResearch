"""WP05 consumer and WP06 deterministic Streamlit visualization frame."""
from __future__ import annotations

from dataclasses import dataclass
from datetime import datetime
from decimal import Decimal
from typing import Any, Mapping

import streamlit as st

from visualization_read_model import Envelope, ReadModelCache, resolve_handoff_path, refresh_interval_seconds

WINDOW_CAPACITY = 64
_REVISION_KINDS = ("HistoricalPresentation", "ReplayLogicalTick")
_STATES = ("Ready", "WarmUp", "Empty", "Stale", "Failed")
_SOURCE_MODES = ("Historical", "Replay")
_IDEMPOTENCY_STATUSES = ("NewlyPersisted", "EquivalentExisting", "Unavailable")
_DATA_QUALITY_STATUSES = ("Valid", "Invalid", "Unavailable")


class FrameIntegrityError(ValueError):
    """An accepted transport envelope cannot be projected truthfully."""


@dataclass(frozen=True)
class PriceTimePoint:
    source_time: str
    price: Decimal


@dataclass(frozen=True)
class VisualizationFrame:
    """Immutable WP06 projection of one accepted WP05 envelope."""

    contract_version: str
    revision_kind: str
    revision_value: int
    revision_identity: str
    source_mode: str
    source_authority: int
    target: str
    state: str
    points: tuple[PriceTimePoint, ...]
    latest: PriceTimePoint | None
    observation_count: int
    window_count: int
    window_capacity: int
    feature_identity: str | None
    feature_value: Decimal | None
    feature_observation_count: int | None
    feature_required_observation_count: int | None
    pipeline_success: bool | None
    idempotency_status: str
    data_quality_status: str
    failure_category: str | None
    failure_message: str | None
    failure_recoverable: bool | None
    stale_reason: str | None
    transport_warning: str | None


def _mapping(value: Any, name: str) -> Mapping[str, Any]:
    if not isinstance(value, Mapping):
        raise FrameIntegrityError(f"{name} must be an object")
    return value


def _string(value: Any, name: str) -> str:
    if not isinstance(value, str) or not value:
        raise FrameIntegrityError(f"{name} must be a non-empty string")
    return value


def _non_negative_integer(value: Any, name: str) -> int:
    if isinstance(value, bool) or not isinstance(value, int) or value < 0:
        raise FrameIntegrityError(f"{name} must be a non-negative integer")
    return value


def _decimal(value: Any, name: str) -> Decimal:
    if isinstance(value, bool) or not isinstance(value, (int, Decimal)):
        raise FrameIntegrityError(f"{name} must be a decimal")
    return Decimal(value)


def _point(value: Any, name: str) -> PriceTimePoint:
    item = _mapping(value, name)
    source_time = _string(item.get("sourceTime"), f"{name}.sourceTime")
    try:
        datetime.fromisoformat(source_time.replace("Z", "+00:00"))
    except ValueError as exc:
        raise FrameIntegrityError(f"{name}.sourceTime must be ISO-8601") from exc
    return PriceTimePoint(source_time, _decimal(item.get("price"), f"{name}.price"))


def project_visualization_frame(envelope: Envelope, transport_warning: str | None = None) -> VisualizationFrame:
    """Project one accepted WP05 envelope without changing its semantics."""
    raw = _mapping(envelope.raw, "envelope")
    revision = _mapping(raw.get("revision"), "revision")
    revision_kind = revision.get("kind")
    if revision_kind not in _REVISION_KINDS:
        raise FrameIntegrityError("revision.kind is invalid")
    revision_value = _non_negative_integer(revision.get("value"), "revision.value")
    revision_identity = _string(revision.get("identity"), "revision.identity")
    source_mode, source_authority, state = raw.get("sourceMode"), raw.get("sourceAuthority"), raw.get("state")
    if source_mode not in _SOURCE_MODES or source_authority not in (0, 1) or state not in _STATES:
        raise FrameIntegrityError("envelope source or state is invalid")
    if revision_kind == "HistoricalPresentation" and source_mode != "Historical":
        raise FrameIntegrityError("Historical revision requires Historical source mode")
    if revision_kind == "ReplayLogicalTick" and source_mode != "Replay":
        raise FrameIntegrityError("Replay revision requires Replay source mode")

    window = raw.get("window")
    if not isinstance(window, list) or len(window) > WINDOW_CAPACITY:
        raise FrameIntegrityError("window must contain at most 64 points")
    points = tuple(_point(value, f"window[{index}]") for index, value in enumerate(window))
    for earlier, later in zip(points, points[1:]):
        if datetime.fromisoformat(earlier.source_time.replace("Z", "+00:00")) >= datetime.fromisoformat(later.source_time.replace("Z", "+00:00")):
            raise FrameIntegrityError("window must be ordered oldest to newest")
    latest_value = raw.get("latest")
    latest = None if latest_value is None else _point(latest_value, "latest")
    if points and latest != points[-1]:
        raise FrameIntegrityError("latest must equal the final window point")
    if not points and latest is not None:
        raise FrameIntegrityError("empty window must not have latest")
    observation_count = _non_negative_integer(raw.get("observationCount"), "observationCount")
    window_count = len(points)
    if window_count != min(observation_count, WINDOW_CAPACITY):
        raise FrameIntegrityError("window count is inconsistent with observation count")

    feature = raw.get("feature")
    feature_identity = feature_value = feature_observation_count = feature_required_observation_count = None
    if feature is not None:
        item = _mapping(feature, "feature")
        feature_identity = _string(item.get("identity"), "feature.identity")
        value = item.get("value")
        feature_value = None if value is None else _decimal(value, "feature.value")
        feature_observation_count = _non_negative_integer(item.get("observationCount"), "feature.observationCount")
        feature_required_observation_count = _non_negative_integer(item.get("requiredObservationCount"), "feature.requiredObservationCount")
    pipeline = raw.get("pipeline")
    pipeline_success = None
    if pipeline is not None:
        pipeline_success = _mapping(pipeline, "pipeline").get("isSuccess")
        if not isinstance(pipeline_success, bool):
            raise FrameIntegrityError("pipeline.isSuccess must be boolean")
    idempotency_status, data_quality_status = raw.get("idempotencyStatus", "Unavailable"), raw.get("dataQualityStatus", "Unavailable")
    if idempotency_status not in _IDEMPOTENCY_STATUSES or data_quality_status not in _DATA_QUALITY_STATUSES:
        raise FrameIntegrityError("semantic status is invalid")

    failure = raw.get("failure")
    failure_category = failure_message = None
    failure_recoverable = None
    if state == "Failed":
        item = _mapping(failure, "failure")
        failure_category, failure_message = _string(item.get("category"), "failure.category"), _string(item.get("message"), "failure.message")
        failure_recoverable = item.get("recoverable")
        if not isinstance(failure_recoverable, bool):
            raise FrameIntegrityError("failure.recoverable must be boolean")
    elif failure is not None:
        raise FrameIntegrityError("only Failed may carry failure metadata")
    stale_reason = raw.get("staleReason")
    if state == "Stale":
        stale_reason = _string(stale_reason, "staleReason")
    elif stale_reason is not None:
        raise FrameIntegrityError("only Stale may carry staleReason")
    if transport_warning is not None and (not isinstance(transport_warning, str) or not transport_warning):
        raise FrameIntegrityError("transport warning must be a non-empty string")

    return VisualizationFrame(
        _string(raw.get("contractVersion"), "contractVersion"), revision_kind, revision_value, revision_identity,
        source_mode, source_authority, _string(raw.get("target"), "target"), state, points, latest,
        observation_count, window_count, WINDOW_CAPACITY, feature_identity, feature_value, feature_observation_count,
        feature_required_observation_count, pipeline_success, idempotency_status, data_quality_status, failure_category, failure_message, failure_recoverable,
        stale_reason, transport_warning,
    )


def project_wp07_presentation_sections(frame: VisualizationFrame) -> tuple[tuple[str, tuple[tuple[str, str], ...]], ...]:
    """Return the fixed WP07 factual rows without I/O or Streamlit calls."""
    unavailable = "Unavailable"
    feature_value = unavailable if frame.feature_value is None else format(frame.feature_value, "f")
    observed_required = unavailable if frame.feature_observation_count is None or frame.feature_required_observation_count is None else f"{frame.feature_observation_count} / {frame.feature_required_observation_count}"
    pipeline_status = unavailable if frame.pipeline_success is None else ("Success" if frame.pipeline_success else "Failure")
    return (
        ("Feature", (("Feature identity", frame.feature_identity or unavailable), ("Feature value", feature_value), ("Observed / required", observed_required))),
        ("Snapshot", (("Snapshot identity", unavailable), ("Snapshot version", unavailable))),
        ("Data Quality", (("Validation status", frame.data_quality_status),)),
        ("Pipeline", (("Pipeline status", pipeline_status), ("Backend state", frame.state), ("Failure category", frame.failure_category or unavailable))),
        ("Idempotency", (("Persistence disposition", frame.idempotency_status),)),
    )


def render_visualization_frame(frame: VisualizationFrame) -> None:
    """Render only deterministic WP06 frame inputs; pixel layout is not semantic."""
    st.subheader(f"{frame.target} - {frame.state}")
    if frame.transport_warning:
        st.warning(frame.transport_warning)
    if frame.points:
        st.line_chart([{"sourceTime": point.source_time, "price": point.price} for point in frame.points], x="sourceTime", y="price")
    if frame.latest:
        st.write({"latestSourceTime": frame.latest.source_time, "latestPrice": frame.latest.price})
    st.write({"observationCount": frame.observation_count, "windowCount": frame.window_count, "windowCapacity": frame.window_capacity})
    if frame.feature_identity:
        feature: dict[str, Any] = {"featureIdentity": frame.feature_identity}
        if frame.feature_value is not None:
            feature["featureValue"] = frame.feature_value
        elif frame.feature_required_observation_count is not None:
            feature["requiredObservationCount"] = frame.feature_required_observation_count
        st.write(feature)
    if frame.pipeline_success is not None:
        st.write({"pipelineSuccess": frame.pipeline_success})
    if frame.failure_category:
        st.error({"failureCategory": frame.failure_category, "failureMessage": frame.failure_message, "recoverable": frame.failure_recoverable})
    if frame.stale_reason:
        st.info({"staleReason": frame.stale_reason})
    for section, rows in project_wp07_presentation_sections(frame):
        st.subheader(section)
        for label, value in rows:
            st.write({label: value})


def render() -> None:
    path, interval = resolve_handoff_path(), refresh_interval_seconds()
    cache = st.session_state.setdefault("wp05_cache", ReadModelCache())
    warning = cache.refresh(path)
    st.title("Simulated / replayed financial visualization")
    st.caption("Read-only local presentation of the Worker-published bounded envelope; not live market data.")
    if st.button("Refresh now"):
        warning = cache.refresh(path)
    st.autorefresh(interval=interval * 1000, key="wp05_refresh")
    if cache.last_good is None:
        if warning:
            st.warning(warning)
        st.info("ProducerUnavailable - awaiting the first Worker publication.")
        return
    try:
        render_visualization_frame(project_visualization_frame(cache.last_good, warning))
    except FrameIntegrityError as exc:
        if warning:
            st.warning(warning)
        st.error(f"FrameIntegrity: {exc}")


if __name__ == "__main__":
    render()
