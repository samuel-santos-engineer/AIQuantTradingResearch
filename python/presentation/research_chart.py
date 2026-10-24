"""Release 1.15 presentation only: validated WP05 snapshots, no research computation.

AIQ_RESEARCH_CHART=1 opts into this UI; credentials stay in the Streamlit process.
"""
from dataclasses import dataclass, field
from datetime import datetime, timedelta, timezone
from functools import lru_cache
import json
import math
import os
from pathlib import Path
import re
import time
import uuid

from research_snapshot_client import ResearchSnapshotClient, SnapshotUnavailable

DISCLOSURE = "Transformed educational values - not actual BTC/USD or ETH/USD prices."
PRESETS = (100, 300, 1000)
OVERLAYS = ("SMA20", "EMA20", "Bollinger bands")


def _date(value):
    if not isinstance(value, str) or len(value) > 32 or not re.fullmatch(r"20[0-9]{2}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}(?:\.[0-9]{1,6})?Z", value):
        raise ValueError("Invalid UTC viewport.")
    return datetime.fromisoformat(value.replace("Z", "+00:00"))


@dataclass
class Viewport:
    x: tuple | None = None
    y: tuple | None = None
    y2: tuple | None = None
    y3: tuple | None = None
    revision: int = 0
    mount: int = 1
    command_epoch: int = 0
    reset_epoch: int = 0
    live: bool = True
    expired: bool = False
    partial: bool = False

    def command(self, rebuild=False):
        self.command_epoch += 1
        if rebuild:
            self.mount += 1

    def return_live(self):
        self.x = self.y = self.y2 = self.y3 = None
        self.live, self.expired, self.partial = True, False, False
        self.reset_epoch += 1
        self.revision += 1
        self.command()

    def reconcile(self, rows):
        if not rows or not self.x:
            self.expired = self.partial = False
            return
        start, end = _date(rows[0]["time"]), _date(rows[-1]["time"]) + timedelta(minutes=1)
        left, right = map(_date, self.x)
        self.expired = right <= start or left >= end
        self.partial = not self.expired and (left < start or right > end)

    def accept(self, event, instrument, identity):
        try:
            if not isinstance(event, dict) or set(event) != {
                    "kind", "instrument", "identity", "mount", "revision", "commandEpoch", "x", "y", "y2", "y3"}:
                raise ValueError()
            if event["instrument"] != instrument or event["identity"] != dict(identity) or event["kind"] not in ("manual", "ack"):
                raise ValueError()
            if any(type(event[k]) is not int or not 0 <= event[k] < 2**31 for k in ("mount", "revision", "commandEpoch")):
                raise ValueError()
            if event["mount"] != self.mount or event["commandEpoch"] != self.command_epoch:
                raise ValueError()
            if event["kind"] == "manual" and event["revision"] <= self.revision or event["kind"] == "ack" and event["revision"] != self.revision:
                raise ValueError()
            if not isinstance(event["x"], list) or len(event["x"]) != 2 or _date(event["x"][0]) >= _date(event["x"][1]):
                raise ValueError()
            for key in ("y", "y2", "y3"):
                values = event[key]
                if key == "y3" and values is None:
                    continue
                if not isinstance(values, list) or len(values) != 2 or any(type(v) not in (int, float) or not math.isfinite(v) or abs(v) > 1e25 for v in values) or values[0] >= values[1]:
                    raise ValueError()
            if len(json.dumps(event, allow_nan=False).encode("utf-8")) > 4096:
                raise ValueError()
            if event["kind"] == "ack":
                return False
            self.x, self.y, self.y2 = tuple(event["x"]), tuple(event["y"]), tuple(event["y2"])
            self.y3 = tuple(event["y3"]) if event["y3"] is not None else None
            self.revision, self.live = event["revision"], False
            return True
        except (TypeError, KeyError, OverflowError, ValueError) as error:
            raise ValueError("Obsolete or invalid chart interaction ignored.") from error


@dataclass
class InstrumentView:
    view: Viewport = field(default_factory=Viewport)
    preset: int = 300
    overlays: tuple = ("SMA20",)
    panel: str = "None"
    events: bool = False
    identity: tuple | None = None
    notice: str | None = None
    last_event: str | None = None
    last_attempt: float | None = None

    def reconcile(self, snapshot):
        current = tuple(snapshot["identity"][k] for k in ("generation", "version", "displayDate", "transformEpoch"))
        if self.identity is not None and current != self.identity:
            generation = current[0] != self.identity[0]
            rebuild = generation or current[2:] != self.identity[2:]
            self.view.command(rebuild=rebuild)
            self.notice = "Dataset generation changed; history was replaced." if generation else "Transformation date or epoch changed; the chart was rebuilt." if rebuild else None
        self.identity = current
        self.view.reconcile(snapshot["candles"])


def acquire(client, instrument, state, now=None, force=False):
    """One synchronous refresh per instrument, never triggered by viewport/presentation changes."""
    now = time.monotonic() if now is None else now
    if not force and state.last_attempt is not None and now - state.last_attempt < 60:
        return client.snapshot(instrument)
    state.last_attempt = now
    try:
        snapshot = client.fetch(instrument)
        state.reconcile(snapshot)
        return snapshot
    except SnapshotUnavailable:
        # WP05 owns complete last-good state; no second snapshot cache or fallback source.
        return client.snapshot(instrument)


def _number(value):
    return None if value is None else float(value)  # Rendering only; never an indicator calculation.


def build_figure(snapshot, state):
    # Eagerly finish the existing optional import before Plotly's non-loading lookup.
    import pandas  # noqa: F401 - same cold-load barrier qualified in WP01
    import plotly.graph_objects as go
    from plotly.subplots import make_subplots
    rows = snapshot["candles"]
    oscillator = state.panel != "None"
    figure = make_subplots(rows=3 if oscillator else 2, cols=1, shared_xaxes=True,
                           row_heights=[0.58, 0.18, 0.24] if oscillator else [0.76, 0.24], vertical_spacing=0.05)
    # Full retained data remains loaded; presets change the initial viewport, not source acquisition.
    groups = [list(range(len(rows)))] if rows else []
    for group in groups:
        subset = [rows[i] for i in group]
        text = ["Transformed educational OHLC - not actual quotes<br>" + r["time"] + " UTC<br>" +
                "<br>".join(k.title() + ": " + r[k] for k in ("open", "high", "low", "close")) for r in subset]
        figure.add_trace(go.Candlestick(x=[r["time"] for r in subset], open=[_number(r["open"]) for r in subset],
                                       high=[_number(r["high"]) for r in subset], low=[_number(r["low"]) for r in subset],
                                       close=[_number(r["close"]) for r in subset], name="Transformed OHLC", text=text,
                                       hoverinfo="text", showlegend=group is groups[0]), row=1, col=1)
    figure.add_trace(go.Bar(x=[r["time"] for r in rows], y=[_number(r["volume"]) for r in rows],
                            name="Volume (BTC)" if snapshot["instrument"] == "BTC/USD" else "Volume (ETH)",
                            marker_color="#75bfff", hovertemplate="%{x} UTC<br>Base-asset volume: %{y}<extra></extra>"), row=2, col=1)
    fields = []
    if "SMA20" in state.overlays:
        fields.append(("sma20", "SMA20 (transformed)"))
    if "EMA20" in state.overlays:
        fields.append(("ema20", "EMA20 (transformed)"))
    if "Bollinger bands" in state.overlays:
        fields.extend((name, label + " (transformed)") for name, label in
                      (("bollingerLower", "Bollinger lower"), ("bollingerMiddle", "Bollinger middle"), ("bollingerUpper", "Bollinger upper")))
    for name, label in fields:
        x, y, text = [], [], []
        for index, (row, analytic) in enumerate(zip(rows, snapshot["analytics"])):
            if index and row["segment"] != rows[index - 1]["segment"]:
                x.append(row["time"]); y.append(None); text.append("")
            x.append(row["time"]); y.append(_number(analytic[name])); text.append(analytic[name])
        figure.add_trace(go.Scatter(x=x, y=y, text=text, mode="lines", connectgaps=False, name=label,
                                    hovertemplate="%{x} UTC<br>Transformed educational value: %{text}<extra>%{fullData.name}</extra>"), row=1, col=1)
    if oscillator:
        names = [("rsi14", "RSI14 (0-100)")] if state.panel == "RSI14" else [("macd", "MACD (transformed)"), ("macdSignal", "MACD signal (transformed)"), ("macdHistogram", "MACD histogram (transformed)")]
        for name, label in names:
            x, y = [], []
            for i, (row, analytic) in enumerate(zip(rows, snapshot["analytics"])):
                if i and row["segment"] != rows[i - 1]["segment"]:
                    x.append(row["time"]); y.append(None)
                x.append(row["time"]); y.append(_number(analytic[name]))
            figure.add_trace(go.Scatter(x=x, y=y, name=label, mode="lines", connectgaps=False), row=3, col=1)
        if state.panel == "RSI14":
            figure.update_yaxes(range=[0, 100], row=3, col=1)
    figure.update_layout(shapes=[{"type": "rect", "xref": "x", "yref": "y domain", "x0": gap["start"], "x1": gap["end"],
                                  "y0": 0, "y1": 1, "fillcolor": "#f5a623", "opacity": 0.15, "line": {"width": 0}, "layer": "below"}
                                 for gap in snapshot["marketDataGapIntervals"]])
    if state.events:
        lookup = {r["time"]: r for r in rows}
        for event_type, label in (("SegmentStart", "Continuity start"), ("TrendTransition", "Trend transition")):
            items = [e for e in snapshot["events"] if e["type"] == event_type]
            figure.add_trace(go.Scatter(x=[e["time"] for e in items], y=[_number(lookup[e["time"]]["high"]) for e in items],
                                        mode="markers", name=label, marker={"symbol": "diamond", "size": 9},
                                        text=[(e["trend"] or "New continuity segment") + " / " + (e["regime"] or "Recalibrating") for e in items],
                                        hovertemplate="%{x} UTC<br>%{text}<extra></extra>"), row=1, col=1)
    if rows:
        start = rows[-min(state.preset, len(rows))]["time"]
        end = (_date(rows[-1]["time"]) + timedelta(minutes=1)).isoformat().replace("+00:00", "Z")
        figure.update_xaxes(type="date", range=[start, end], autorange=False, title_text="UTC")
    figure.update_layout(height=620, title=snapshot["instrument"] + " - transformed educational OHLC (not actual quotes)",
                         dragmode="zoom", uirevision=snapshot["instrument"] + str(state.view.mount),
                         margin={"l": 45, "r": 20, "t": 70, "b": 40}, legend={"orientation": "h", "y": 1.06},
                         xaxis_rangeslider_visible=False)
    figure.update_yaxes(title_text="Transformed price", row=1, col=1)
    figure.update_yaxes(title_text="Volume", row=2, col=1)
    return json.loads(figure.to_json())


@lru_cache(maxsize=1)
def component_assets():
    import pandas  # noqa: F401
    from plotly.offline import get_plotlyjs
    folder = Path(__file__).parent
    source = "const module={exports:{}}; const exports=module.exports;\n" + get_plotlyjs()
    source += "\nconst Plotly=module.exports;\n" + (folder / "research_chart_component.js").read_text(encoding="utf-8")
    return source, (folder / "research_chart_component.html").read_text(encoding="utf-8")


def component():
    import streamlit as st
    source, html = component_assets()
    # Register with this runtime through the public API; cache assets, not a runtime-bound callable.
    return st.components.v2.component("r115_research_chart", html=html, js=source, isolate_styles=False)


def component_data(snapshot, state, session):
    return {"figure": build_figure(snapshot, state), "instrument": snapshot["instrument"], "identity": dict(snapshot["identity"]),
            "mount": state.view.mount, "revision": state.view.revision, "commandEpoch": state.view.command_epoch,
            "resetEpoch": state.view.reset_epoch, "session": session,
            "viewport": {"x": state.view.x, "y": state.view.y, "y2": state.view.y2, "y3": state.view.y3},
            "renderToken": json.dumps([snapshot["instrument"], dict(snapshot["identity"]), state.preset, state.overlays, state.panel, state.events, state.view.mount, state.view.command_epoch], sort_keys=True)}


def _render_content(auto_refresh=False):
    import streamlit as st
    st.title("Historical Crypto Research")
    st.caption(DISCLOSURE + " Completed one-minute candles; educational research, no trade execution.")
    instrument = st.radio("Instrument", ("BTC/USD", "ETH/USD"), horizontal=True, key="r115_instrument")
    if "r115_session" not in st.session_state:
        st.session_state["r115_session"] = uuid.uuid4().hex
    if "r115_views" not in st.session_state:
        st.session_state["r115_views"] = {}
    state = st.session_state["r115_views"].setdefault(instrument, InstrumentView())
    try:
        if "r115_client" not in st.session_state:
            st.session_state["r115_client"] = ResearchSnapshotClient(os.environ["AIQ_RESEARCH_BASE_URL"], os.environ["AIQ_RESEARCH_API_KEY"],
                                                                   ca_file=os.environ.get("AIQ_RESEARCH_CA_FILE"))
        client = st.session_state["r115_client"]
    except (KeyError, ValueError, OSError, SnapshotUnavailable):
        st.warning("Research service unavailable. No validated historical data is available.")
        st.caption("Public runtime verification remains disabled.")
        return
    controls = st.columns([2, 1, 1])
    preset = controls[0].selectbox("Retained candles", PRESETS, index=PRESETS.index(state.preset), key="r115_preset_" + instrument)
    if preset != state.preset:
        state.preset = preset
        state.view.return_live()
    refresh = controls[1].button("Refresh", key="r115_refresh_" + instrument)
    if controls[2].button("Return to Live", key="r115_live_" + instrument):
        state.view.return_live()
    with st.popover("Technical Analysis"):
        state.overlays = tuple(st.multiselect("Price overlays", OVERLAYS, default=list(state.overlays), key="r115_overlays_" + instrument))
        panel = st.selectbox("Research panel", ("None", "RSI14", "MACD"), index=("None", "RSI14", "MACD").index(state.panel), key="r115_panel_" + instrument)
        if panel != state.panel:
            state.panel = panel
            state.view.y3 = None
            state.view.command(rebuild=True)
        state.events = st.checkbox("Historical transition markers", value=state.events, key="r115_events_" + instrument)
    # Explicit refresh, or a due tick of the supported, user-enabled Streamlit fragment.
    # No worker thread/background loop; presentation changes create no additional requests.
    if state.last_attempt is None or refresh or auto_refresh and time.monotonic() - state.last_attempt >= 60:
        acquire(client, instrument, state, force=refresh)
    snapshot = client.snapshot(instrument)
    connected, synced = client.connection_state(instrument)
    st.caption(connected + " | " + ("Live follow" if state.view.live else "Historical Exploration"))
    if snapshot is None:
        st.warning("Historical data unavailable. Refresh when the research service is available.")
        return
    status = snapshot["status"]
    st.caption("Research: " + status["research"] + " | Freshness at observation: " + status["freshness"] + " | Integrity: " + status["integrity"] + " | Classification: " + status["classificationValidity"])
    st.caption("Transformation date: " + snapshot["identity"]["displayDate"] + " | Observed: " + snapshot["observedAt"] + " | Last successful sync: " + (synced.isoformat() if synced else "Unavailable"))
    if connected != "Connected":
        st.warning("Disconnected - showing last-known validated history, not a current observation.")
    elif status["freshness"] != "Current":
        st.warning("History is " + status["freshness"].lower() + "; these values are not current market quotes.")
    if state.notice:
        st.info(state.notice)
    rows = snapshot["candles"]
    if not rows:
        st.info("No completed historical candles have been committed for this instrument.")
        return
    state.view.reconcile(rows)
    st.caption(str(len(rows)) + " retained candles | " + rows[0]["time"] + " to " + rows[-1]["time"] + " UTC | " + str(len(snapshot["marketDataGapIntervals"])) + " market-data gaps | " + str(len(snapshot["researchUnavailableIntervals"])) + " research-unavailable intervals. Presets count candles, not elapsed minutes.")
    latest = snapshot["classifications"][-1]
    if latest["validity"] == "Valid":
        st.info("Latest research: " + latest["trend"] + " / " + latest["regime"] + " | trend score " + str(latest["trendScore"]))
    else:
        st.info("Research is recalibrating or unavailable. Classification requires 401 consecutive candles; null warmup indicators remain unavailable.")
    if state.view.expired:
        st.warning("Historical viewport expired from retained history. Choose Return to Live; the saved UTC range has not been reset.")
    elif state.view.partial:
        st.info("Part of the saved UTC viewport is outside retained history; surviving overlap remains visible.")
    result = component()(data=component_data(snapshot, state, st.session_state["r115_session"]),
                         key="research_" + instrument + "_" + str(state.view.mount), height=640,
                         default={"viewport": None}, on_viewport_change=lambda: None)
    event = result.viewport
    if event is not None:
        signature = json.dumps(event, sort_keys=True)
        if signature != state.last_event:
            state.last_event = signature
            try:
                if state.view.accept(event, instrument, snapshot["identity"]):
                    state.view.reconcile(rows)
                    st.rerun()
            except ValueError:
                pass  # Rejected browser state never changes authoritative snapshots or accepted ranges.
    if st.checkbox("Show research history", key="r115_history_" + instrument):
        st.dataframe([dict(row) for row in snapshot["classifications"][-state.preset:]], hide_index=True, width="stretch")
    st.caption("Public runtime verification remains disabled. No trading or provider acquisition is enabled by this view.")


def render():
    import streamlit as st
    auto = st.checkbox("Refresh every 60 seconds while this page is open", value=False, key="r115_auto_refresh")
    @st.fragment(run_every=60 if auto else None)
    def page():
        _render_content(auto_refresh=auto)
    page()
