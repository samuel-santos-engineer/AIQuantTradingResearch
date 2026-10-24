"""Isolated WP02 security fixture. Never a production chart or data source."""
import os
import streamlit as st
from api_key_client_fixture import Client, Unavailable

st.title("WP02 offline security fixture")
st.caption("No market data, analytics or production snapshot service.")
try:
    client = Client(os.environ.get("AIQ_FIXTURE_RESEARCH_URL", ""),
                    os.environ.get("AIQ_FIXTURE_ACTIVE_KEY", ""),
                    os.environ.get("AIQ_FIXTURE_TRUST"))
    value = client.refresh()
    st.session_state["last_known"] = value
    st.info("Fixture connected; research data unavailable.")
except Unavailable:
    st.warning("Disconnected. Research data unavailable.")
    if "last_known" in st.session_state:
        st.caption("Last-known fixture status retained; no market data.")
