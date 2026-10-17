"""WP05 consumer and WP06 deterministic Streamlit visualization frame."""
from __future__ import annotations

from dataclasses import dataclass
from datetime import datetime, timedelta
from decimal import Decimal
from typing import Any, Mapping
from pathlib import Path
import os
import time

import streamlit as st
import plotly.graph_objects as go
from plotly.subplots import make_subplots

from visualization_read_model import Envelope, ReadModelCache, resolve_handoff_path, refresh_interval_seconds
from historical_market_bridge import DEFAULT_SELECTION, PUBLIC_UNAVAILABLE, BridgeError, MarketResponse, invoke, QUALIFIED_DATASETS, QUALIFIED_WINDOWS
from historical_market_bridge import PUBLIC_RUNTIME_VERIFIED
from local_synthetic_chart import enabled as local_chart_enabled, load as load_local_chart
from immutable_synthetic_chart import enabled as immutable_chart_enabled, load as load_immutable_chart
from public_preview_api import LABEL as SYNTHETIC_LABEL

FAVICON_PATH = Path(__file__).resolve().parents[2] / "favicon.ico"

st.set_page_config(
    page_title="AI Quant Trading Research",
    page_icon=str(FAVICON_PATH),
)

VIKE_PUBLIC_MESSAGE = "BTC-like Market Simulation — Synthetic Historical OHLCV"
TWELVE_DATA_BOUNDARY_MESSAGE = "Twelve Data is retained for private/internal research, including real-time market-data studies. Twelve Data values from that research are not exposed through this public interface under the project's current data-access/licensing boundary."
SYNTHETIC_DISCLOSURE = "SYNTHETIC / SIMULATED - NOT REAL MARKET DATA"

RESEARCH_FOOTER = "Research and demonstration application \u2022 No trade execution"
STALE_HISTORICAL_MESSAGE = "Showing the most recently validated historical data."
EMPTY_HISTORICAL_MESSAGE = "No historical market data is available for this selection."
SYSTEM_HEALTH_UNAVAILABLE_MESSAGE = "System Health data is temporarily unavailable."
NAVIGATION_OPTIONS = ("Market Research", "Architecture & Data Governance", "ML & Automation Studies", "System Health")
ML_AUTOMATION_STUDIES_CONTENT = (
    "AI Quant Trading Research is evolving from historical market-data visualization toward reproducible quantitative research and machine-learning evaluation.",
    "Historical Market Data \u2192 Feature Engineering \u2192 Machine Learning Evaluation \u2014 Release 2.0+ \u2192 Strategy / Signal Research \u2192 Governed Automation Studies",
    "Historical visualization \u2014 Release 1.13",
    "ML evaluation \u2014 Begins Release 2.0",
    "Automated trading \u2014 Not enabled",
    "This environment does not execute trades.",
)

WINDOW_CAPACITY = 64
_REVISION_KINDS = ("HistoricalPresentation", "ReplayLogicalTick")
_STATES = ("Ready", "WarmUp", "Empty", "Stale", "Failed")
_SOURCE_MODES = ("Historical", "Replay")
_IDEMPOTENCY_STATUSES = ("NewlyPersisted", "EquivalentExisting", "Unavailable")
_DATA_QUALITY_STATUSES = ("Valid", "Invalid", "Unavailable")
_SYSTEM_HEALTH_STATES = ("ready", "warmup", "empty", "failed", "stale", "unavailable")
_SYSTEM_HEALTH_PROVENANCES = ("historical", "replay", "simulated")
_SYSTEM_HEALTH_REASONS = {
    "ready": None,
    "warmup": None,
    "empty": None,
    "failed": "pipeline-failed",
    "stale": "structural-staleness",
    "unavailable": "required-health-evidence-unavailable",
}


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
    system_health_state: str
    system_health_provenance: str
    system_health_reason: str | None
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
    health = (
        {"state": "unavailable", "provenance": "historical" if source_authority == 0 else "simulated", "reason": "required-health-evidence-unavailable"}
        if "systemHealth" not in raw
        else _mapping(raw.get("systemHealth"), "systemHealth")
    )
    health_state, health_provenance, health_reason = health.get("state"), health.get("provenance"), health.get("reason")
    if health_state not in _SYSTEM_HEALTH_STATES or health_provenance not in _SYSTEM_HEALTH_PROVENANCES or health_reason != _SYSTEM_HEALTH_REASONS.get(health_state):
        raise FrameIntegrityError("systemHealth is invalid")
    if transport_warning == "HealthIntegrity":
        health_state, health_reason = "unavailable", "required-health-evidence-unavailable"

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
        source_mode, source_authority, _string(raw.get("target"), "target"), state, health_state, health_provenance, health_reason, points, latest,
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


def project_system_health_presentation(frame: VisualizationFrame) -> tuple[str, str]:
    mapping = {
        "ready": ("info", "System Health: Canonical evidence available."),
        "warmup": ("info", "System Health: Waiting for bounded canonical observations."),
        "empty": ("info", "System Health: Canonical pipeline completed with no observations."),
        "failed": ("error", "System Health: Canonical pipeline failed."),
        "stale": ("warning", "System Health: Canonical visualization evidence is structurally stale."),
        "unavailable": ("warning", "System Health: Health evidence is unavailable; visualization data may still be available."),
    }
    return mapping[frame.system_health_state]


def render_visualization_frame(frame: VisualizationFrame) -> None:
    """Render only deterministic WP06 frame inputs; pixel layout is not semantic."""
    st.subheader(f"{frame.target} - {frame.state}")
    st.subheader("System Health")
    health_component, health_message = project_system_health_presentation(frame)
    getattr(st, health_component)(health_message)
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


def build_market_figure(response: MarketResponse, show_sma: bool = False, show_ema: bool = False, show_bollinger: bool = False, show_rsi: bool = False, show_macd: bool = False) -> go.Figure:
    """Build the bounded client-side candlestick/volume figure from canonical bridge data."""
    candles = response.candles
    panel_count = int(show_rsi) + int(show_macd)
    figure = make_subplots(rows=2 + panel_count, cols=1, shared_xaxes=True, vertical_spacing=0.03,
                           row_heights=[0.72, 0.28] if not panel_count else [0.52, 0.18] + [0.30 / panel_count] * panel_count)
    figure.add_trace(go.Candlestick(x=[c.open_time_utc for c in candles], open=[float(c.open) for c in candles], high=[float(c.high) for c in candles], low=[float(c.low) for c in candles], close=[float(c.close) for c in candles], name="OHLC", hovertemplate=SYNTHETIC_DISCLOSURE + "<br>Simulation time (UTC): %{x}<br>Open: %{open}<br>High: %{high}<br>Low: %{low}<br>Close: %{close}<extra></extra>"), row=1, col=1)
    figure.add_trace(go.Bar(x=[c.open_time_utc for c in candles], y=[float(c.volume) for c in candles], name="Volume", hovertemplate=SYNTHETIC_DISCLOSURE + "<br>Simulation time (UTC): %{x}<br>Simulated activity: %{y}<extra></extra>"), row=2, col=1)
    figure.update_layout(title=dict(text="BTC-like simulation<br>" + SYNTHETIC_DISCLOSURE, x=0.5, xanchor="center", font=dict(size=12)),
                         height=620, margin=dict(l=16, r=16, t=78, b=16), xaxis_rangeslider_visible=False, hovermode="x unified", dragmode="pan")
    figure.update_yaxes(title_text="Price", row=1, col=1); figure.update_yaxes(title_text="Volume", row=2, col=1)
    figure.data[0].uid = "ohlc"
    figure.data[1].uid = "volume"
    if response.analytics is not None:
        figure.add_trace(go.Scatter(x=[r.open_time_utc for r in response.analytics.rows],
                                   y=[None if r.sma20 is None else float(r.sma20) for r in response.analytics.rows],
                                   name="SMA (20)", uid="sma20", mode="lines", line=dict(color="#d9a400", width=2),
                                   visible=show_sma, hovertemplate=SYNTHETIC_DISCLOSURE + "<br>Simulation time (UTC): %{x}<br>SMA (20): %{y:.4f}<extra></extra>"), row=1, col=1)
        for field, label, color, visible, dash in (
                ("ema20", "EMA (20)", "#1874d1", show_ema, "solid"),
                ("bollinger_lower", "Bollinger lower (20, 2)", "#9254b5", show_bollinger, "solid"),
                ("bollinger_middle", "Bollinger middle (20)", "#9254b5", show_bollinger, "dot"),
                ("bollinger_upper", "Bollinger upper (20, 2)", "#9254b5", show_bollinger, "solid")):
            figure.add_trace(go.Scatter(x=[r.open_time_utc for r in response.analytics.rows],
                                       y=[None if getattr(r, field) is None else float(getattr(r, field)) for r in response.analytics.rows],
                                       name=label, uid=field, mode="lines", line=dict(color=color, width=1.5, dash=dash), visible=visible,
                                       hovertemplate=SYNTHETIC_DISCLOSURE + "<br>Simulation time (UTC): %{x}<br>" + label + ": %{y:.4f}<extra></extra>"), row=1, col=1)
        panel = 3
        def lower_trace(field, label, color, row):
            figure.add_trace(go.Scatter(x=[r.open_time_utc for r in response.analytics.rows],
                                       y=[None if getattr(r, field) is None else float(getattr(r, field)) for r in response.analytics.rows],
                                       name=label, uid=field, visible=True, mode="lines", line=dict(color=color, width=1.5),
                                       hovertemplate=SYNTHETIC_DISCLOSURE + "<br>Simulation time (UTC): %{x}<br>" + label + ": %{y:.4f}<extra></extra>"), row=row, col=1)
        if show_rsi:
            lower_trace("rsi14", "RSI (14)", "#9254b5", panel)
            figure.update_yaxes(title_text="RSI (14)", range=[0, 100], fixedrange=True, row=panel, col=1)
            panel += 1
        if show_macd:
            lower_trace("macd", "MACD (12, 26)", "#1874d1", panel)
            lower_trace("macd_signal", "MACD signal (9)", "#d97a00", panel)
            figure.add_trace(go.Bar(x=[r.open_time_utc for r in response.analytics.rows],
                                   y=[None if r.macd_histogram is None else float(r.macd_histogram) for r in response.analytics.rows],
                                   name="MACD histogram", uid="macd_histogram", visible=True,
                                   marker_color=["#aaa" if r.macd_histogram is None else "#00aa88" if r.macd_histogram >= 0 else "#ee4444" for r in response.analytics.rows],
                                   hovertemplate=SYNTHETIC_DISCLOSURE + "<br>Simulation time (UTC): %{x}<br>MACD histogram: %{y:.4f}<extra></extra>"), row=panel, col=1)
            figure.update_yaxes(title_text="MACD (12, 26, 9)", zeroline=True, row=panel, col=1)
    figure.update_layout(height=620 + 200 * panel_count)
    figure.update_xaxes(title_text="Simulation time (UTC)", row=2 + panel_count, col=1)
    figure.update_layout(uirevision="|".join((response.symbol or "", response.interval or "", response.range or "")))
    return figure


def market_selection_changed(previous: tuple[str, str, str] | None, current: tuple[str, str, str]) -> bool:
    return previous != current


def market_provenance_caption(response: MarketResponse) -> str | None:
    if response.provider != "Vike" or not response.last_updated_utc:
        return None
    try:
        timestamp = datetime.fromisoformat(response.last_updated_utc.replace("Z", "+00:00"))
    except ValueError:
        return None
    if timestamp.tzinfo is None or timestamp.utcoffset() != timedelta(0):
        return None
    return f"Data source: Vike \u2022 Historical OHLCV \u2022 Last updated: {response.last_updated_utc} (UTC)"


def render_research_footer() -> None:
    st.caption(RESEARCH_FOOTER)

def market_session_request(state, selection, *, retry=False, now=None):
    now = time.monotonic() if now is None else now
    previous_time = state.get('wp07_last_interaction', now)
    if now < previous_time or now - previous_time >= 1800:
        state.pop('wp05_market_response', None)
    state['wp07_last_interaction'] = now
    changed = market_selection_changed(state.get('wp05_market_selection'), selection)
    if retry:
        previous_retry = state.get('wp07_last_retry')
        if previous_retry is not None and (now < previous_retry or now - previous_retry < 10): return False
        state['wp07_last_retry'] = now
    if changed or retry:
        state['wp05_market_selection'] = selection
        state.pop('wp05_market_response', None)
        return True
    return False

def is_public_market_response(response, selection):
    if response is None or response.state != 'Available': return False
    identity = response.provenance
    return (response.symbol, response.interval, response.range) == selection and response.provider == 'independent-synthetic' and \
        identity is not None and identity.get('source') == 'independent-synthetic' and identity.get('status') == 'simulated' and \
        identity.get('calculationVersion') == 'aiq-ta-v1' and identity.get('datasetFingerprint') == QUALIFIED_DATASETS.get(selection[1]) and \
        identity.get('windowFingerprint') == QUALIFIED_WINDOWS.get((selection[1], selection[2])) and response.analytics is not None and \
        response.analytics.metadata.get('context') == 'public-synthetic'


def acquire_immutable_chart(selection, address):
    # A superseded Streamlit rerun may still be releasing the global reader.
    # Keep the reader nonqueuing; retry only transient busy, never invalid data.
    deadline = time.monotonic() + 2
    for attempt in range(9):
        try:
            return load_immutable_chart(*selection, server_address=address)
        except BridgeError as exc:
            if str(exc) != "ImmutableCandidateBusy" or attempt == 8 or time.monotonic() >= deadline:
                raise
            time.sleep(0.25)


def render_market_research() -> None:
    st.title("AI Quant Trading Research")
    st.subheader("Historical Market Research")
    st.caption(VIKE_PUBLIC_MESSAGE)
    st.caption("Prices and activity units are illustrative, not observed market prices or investment advice.")
    st.info(TWELVE_DATA_BOUNDARY_MESSAGE)
    st.caption("Research preview · Read-only · Independent synthetic data only")
    st.info("Selections are limited to 90 completed UTC days. The qualified reference dataset covers "
            "26 October 2025 through 30 September 2026. Rolling daily updates are not enabled.")
    local_mode = local_chart_enabled(st.get_option("server.address"))
    immutable_mode = immutable_chart_enabled(st.get_option("server.address"))
    if os.environ.get("AIQ_IMMUTABLE_CHART_CANDIDATE") == "1" and os.environ.get("AIQ_LOCAL_CHART") == "1":
        local_mode = immutable_mode = False
    if not PUBLIC_RUNTIME_VERIFIED and not (local_mode or immutable_mode):
        # Do not invoke even the disabled process bridge, retain an old chart, or
        # read a private handoff while public execution remains unqualified.
        st.session_state.pop("wp05_market_response", None)
        st.selectbox("Market", (DEFAULT_SELECTION[0],), index=0, disabled=True)
        st.selectbox("Interval", ("1h", "4h", "1d"), index=0, disabled=True)
        st.selectbox("Range", ("1D", "7D", "30D", "90D"), index=2, disabled=True)
        st.warning("Synthetic chart unavailable while runtime safety checks remain incomplete. No market data is being requested.")
        st.caption("The application shell is available. Candles and indicators are not ready; no live or observed prices are shown.")
        render_research_footer()
        return
    symbol = st.selectbox("Market", (DEFAULT_SELECTION[0],), index=0)
    interval = st.selectbox("Interval", ("1h", "4h", "1d"), index=0)
    range_value = st.selectbox("Range", ("1D", "7D", "30D", "90D"), index=2)
    selection = (symbol, interval, range_value)
    def acquire():
        if immutable_mode:
            return acquire_immutable_chart(selection, st.get_option("server.address"))
        return load_local_chart(*selection, server_address=st.get_option("server.address")) if local_mode else invoke(*selection)
    if market_session_request(st.session_state, selection):
        try:
            st.session_state["wp05_market_response"] = acquire()
            st.session_state["wp05_market_selection"] = selection
        except BridgeError:
            st.session_state["wp05_market_response"] = None
            st.session_state["wp05_market_selection"] = selection
    response = st.session_state.get("wp05_market_response")
    if response is not None and response.state != 'Unavailable' and not is_public_market_response(response, selection):
        st.session_state.pop('wp05_market_response', None)
        response = None
    if response is None or response.state == "Unavailable":
        st.warning(PUBLIC_UNAVAILABLE)
        if st.button("Retry historical data", key="wp05_market_retry"):
            if market_session_request(st.session_state, selection, retry=True):
                try: st.session_state['wp05_market_response'] = acquire()
                except BridgeError: st.session_state['wp05_market_response'] = None
            st.rerun()
        render_research_footer()
        return
    if not response.candles:
        st.info(EMPTY_HISTORICAL_MESSAGE)
        render_research_footer()
        return
    if response.state == "Stale":
        st.info(STALE_HISTORICAL_MESSAGE)
    # Native accessible popover adjacent to the toolbox; no DOM injection/custom modebar JS.
    _, toolbox = st.columns([9, 3])
    with toolbox:
        with st.popover("Technical Analysis", icon=":material/analytics:", help="Show or hide technical analysis overlays"):
            show_sma = st.checkbox("SMA (20)", value=False, key="market_sma20")
            show_ema = st.checkbox("EMA (20)", value=False, key="market_ema20")
            show_bollinger = st.checkbox("Bollinger Bands (20, 2)", value=False, key="market_bollinger")
            show_rsi = st.checkbox("RSI (14)", value=False, key="market_rsi14")
            show_macd = st.checkbox("MACD (12, 26, 9)", value=False, key="market_macd")
            st.caption("Bollinger Bands use population standard deviation. Indicators are calculated in C# from synthetic data.")
    st.plotly_chart(build_market_figure(response, show_sma, show_ema, show_bollinger, show_rsi, show_macd), use_container_width=True, key="market_chart",
                    config={"scrollZoom": True, "displaylogo": False})
    provenance = market_provenance_caption(response)
    if provenance:
        st.caption(provenance)
    render_research_footer()


ARCHITECTURE_SVG = """<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 900 300" role="img" aria-labelledby="title desc">
<title id="title">Public synthetic preview and isolated private research</title>
<desc id="desc">Independent synthetic candles enter canonical contracts and shared analytics. Publication checks allow synthetic public outputs only. Twelve Data private research and inactive Vike remain separate; shared contracts do not share data or publication rights.</desc>
<g fill="#12243a" stroke="#58b7d5" stroke-width="2">
<rect x="20" y="20" width="260" height="70" rx="12"/><rect x="320" y="20" width="260" height="70" rx="12"/>
<rect x="620" y="20" width="260" height="70" rx="12"/><rect x="320" y="130" width="260" height="70" rx="12"/>
<rect x="620" y="130" width="260" height="70" rx="12"/></g>
<g fill="none" stroke="#58b7d5" stroke-width="3"><path d="M280 55H320M580 55H620M750 90V130M620 165H580"/></g>
<g fill="#ffffff" font-family="sans-serif" font-size="17" text-anchor="middle">
<text x="150" y="50">Independent synthetic OHLCV</text><text x="150" y="74">Public preview source</text>
<text x="450" y="50">Canonical contracts</text><text x="450" y="74">Identity and provenance</text>
<text x="750" y="50">Shared analytics</text><text x="750" y="74">SMA · EMA · RSI · MACD · BB</text>
<text x="750" y="160">Publication checks</text><text x="750" y="184">Synthetic public outputs only</text>
<text x="450" y="160">Read-only chart</text><text x="450" y="184">Runtime activation pending</text></g>
<text x="450" y="245" fill="#55758d" font-family="sans-serif" font-size="17" text-anchor="middle">Separate private context: Twelve Data research · Vike inactive · No automatic fallback</text>
<text x="450" y="277" fill="#55758d" font-family="sans-serif" font-size="15" text-anchor="middle">Logical design — shared algorithms do not imply shared datasets or publication rights</text></svg>"""


def render_architecture() -> None:
    st.title("Architecture & Data Governance")
    st.caption(SYNTHETIC_LABEL)
    st.markdown("### One analytical foundation, separate publication boundaries")
    st.html(ARCHITECTURE_SVG)
    st.write("Independent synthetic OHLCV maps into canonical candle contracts and provider-independent analytics. "
             "Publication validation permits only qualified synthetic outputs in the public chart. "
             "Private research shares contract and algorithm designs, never public access to private datasets.")
    st.info("Twelve Data is private research only. Vike is retained but inactive. Neither is a public fallback; "
            "free API access or authentication does not establish display or redistribution rights.")
    st.subheader("Implemented and planned")
    st.table([{"Capability": "Public shell and read-only unavailable API", "Status": "Implemented locally; Azure unverified"},
              {"Capability": "Fixed-window generator and shared indicators", "Status": "Offline implementation; public execution disabled"},
              {"Capability": "Rolling coverage, daily refresh and storage", "Status": "Deferred; not selected or activated"},
              {"Capability": "Private features, models, forecasts and exports", "Status": "Not public; separate publication approval required"}])
    st.caption("This application executes no trades. Operational isolation, resource qualification and deployment require independent evidence.")
    render_research_footer()


def render_ml_automation_studies() -> None:
    st.title("ML & Automation Studies")
    st.info(ML_AUTOMATION_STUDIES_CONTENT[0])
    st.subheader("Research roadmap")
    st.write(ML_AUTOMATION_STUDIES_CONTENT[1])
    st.subheader("Current status")
    for message in ML_AUTOMATION_STUDIES_CONTENT[2:]:
        (st.info if message.endswith("trades.") else st.write)(message)
    render_research_footer()


def render_system_health() -> None:
    st.session_state.pop("wp05_cache", None)
    st.title("System Health")
    st.info("Application interface: responding.")
    address = st.get_option("server.address")
    if not immutable_chart_enabled(address):
        st.info("Synthetic chart: disabled for this runtime.")
    else:
        try:
            # Use the same bounded, pinned validation as the chart; never read a private handoff.
            load_immutable_chart("btc-like-simulation", "1h", "1D", server_address=address)
        except BridgeError:
            st.warning("Synthetic chart: unavailable. The bundled dataset could not be validated.")
        else:
            st.success("Synthetic chart: available. The bundled synthetic dataset passed validation.")
    st.info("Public data APIs: intentionally disabled.")
    st.caption("Checks reflect this page load. Azure infrastructure monitoring is not included.")
    render_research_footer()


def render() -> None:
    now = time.monotonic()
    previous = st.session_state.get('wp07_last_interaction', now)
    if now < previous or now - previous >= 1800: st.session_state.pop('wp05_market_response', None)
    st.session_state['wp07_last_interaction'] = now
    navigation = st.radio("Navigation", NAVIGATION_OPTIONS, horizontal=True)
    if navigation == "Market Research":
        render_market_research()
        return
    if navigation == "ML & Automation Studies":
        render_ml_automation_studies()
        return
    if navigation == "Architecture & Data Governance":
        render_architecture()
        return
    render_system_health()


if __name__ == "__main__":
    render()
