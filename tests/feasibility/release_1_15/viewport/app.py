"""Native-only experiment: no injected JS, DOM access or custom component."""
import inspect
import streamlit as st
import plotly.graph_objects as go
from plotly.subplots import make_subplots
from fixture_snapshots import snapshot

st.title("Release 1.15 native viewport feasibility - fake data only")
st.warning("No native relayout capture is claimed. Browser acceptance remains required.")
instrument = st.selectbox("Instrument", ["BTC/USD", "ETH/USD"])
preset = st.selectbox("Retained candles", [100, 300, 1000], index=1)
mode = st.selectbox("Fixture", ["baseline", "changed", "correction", "gap", "pruned", "expired", "generation", "date"])
show = st.checkbox("SMA fixture overlay (not calculated analytics)")
st.button("Rerun / refresh")
data = snapshot(instrument, version=2 if mode != "baseline" else 1,
                generation="fixture-generation-b" if mode == "generation" else "fixture-generation-a",
                day="2026-10-23" if mode == "date" else "2026-10-22",
                start=2000 if mode == "expired" else 50 if mode == "pruned" else 0,
                gap=700 if mode == "gap" else None)
rows = data["rows"][-preset:]
if mode == "correction":
    rows[-10] = dict(rows[-10], close=rows[-10]["close"] + 0.5)
fig = make_subplots(rows=2, cols=1, shared_xaxes=True, row_heights=[0.75, 0.25])
x = [r["time"] for r in rows]
fig.add_trace(go.Candlestick(x=x, open=[r["open"] for r in rows], high=[r["high"] for r in rows],
                            low=[r["low"] for r in rows], close=[r["close"] for r in rows], name="Fake OHLC"), row=1, col=1)
fig.add_trace(go.Bar(x=x, y=[r["volume"] for r in rows], name="Fake volume"), row=2, col=1)
if show:
    fig.add_trace(go.Scatter(x=x, y=[r["close"] for r in rows], name="Fixture overlay"), row=1, col=1)
fig.update_layout(uirevision=instrument, xaxis_rangeslider_visible=False, title="FAKE DATA - viewport experiment")
fig.update_xaxes(title_text="UTC")
event = st.plotly_chart(fig, key="native_viewport_" + instrument, on_select="rerun", config={"scrollZoom": True})
st.write({"instrument": instrument, "preset": preset, "overlay": show, "identity": {k:v for k,v in data.items() if k != "rows"},
          "native_event": dict(event), "on_relayout_parameter": "on_relayout" in inspect.signature(st.plotly_chart).parameters})
st.caption("Selection payload is not pan/zoom state. Record x/y ranges externally in browser evidence; do not infer them here.")
