"""Fake-data resource reader. No raw SQLite access or provider integration."""
import json
from urllib.request import urlopen
import streamlit as st
import plotly.graph_objects as go

st.title('Dual-process fixture - not market data')
instrument=st.selectbox('Instrument',['BTC/USD','ETH/USD'])
@st.fragment(run_every='60s')
def render():
    with urlopen('http://127.0.0.1:52731/snapshot?instrument='+instrument,timeout=5) as response:
        payload=json.load(response)
    assert payload['fixtureOnly'] is True and len(payload['rows'])==1000
    rows=payload['rows']
    figure=go.Figure(go.Candlestick(x=[r['time'] for r in rows],open=[r['open'] for r in rows],high=[r['high'] for r in rows],low=[r['low'] for r in rows],close=[r['close'] for r in rows]))
    figure.update_layout(title='FAKE DATA - S2 resource fixture')
    st.plotly_chart(figure,key='fixture_chart')
    st.caption('Research classification unavailable; no real analytics.')
render()
