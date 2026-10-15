"""Supported Streamlit ASGI entrypoint; one unprivileged server, no Worker."""
from pathlib import Path

import streamlit as st
from starlette.middleware import Middleware

from public_preview_api import PublicBoundary

# Streamlit 1.61's documented App API preserves its own health/static/WebSocket
# routes. PublicBoundary reserves the six JSON routes and denies admin paths.
app = st.App(str(Path(__file__).with_name("realtime_financial_visualization.py")),
             middleware=[Middleware(PublicBoundary)], debug=False)
