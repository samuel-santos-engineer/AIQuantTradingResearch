# Streamlit Selection

## Decision

Select Streamlit as the initial Python-side interface and dashboard tool for rapid engineering and research visualization of governed analytical evidence. Its low-friction integration with pandas and the scientific Python stack supports bounded local foundation work.

Exact selection: `streamlit==1.61.1` (observed 2026-08-24). PyPI identifies it as the current stable release and its trusted publication metadata records CPython 3.13; it is a pure-Python wheel compatible with the selected scientific stack. Installation is project-local only.

Maintaining the interface entirely in .NET and adopting a general web framework were considered. They were not selected for this initial visualization foundation because they add broader UI or service complexity before the Python capability boundary is proven.

Accepted trade-offs include limited UI customization, process/runtime topology concerns, scaling constraints, and the need to keep interface code separate from core platform responsibilities.

## Ownership and boundaries

Streamlit is a project-local `.venv` dependency declared directly in `requirements.txt` and an interface/adapter concern. It does not own Domain or Application business rules, authoritative persistence, schema, evidence identity, or provider access. No Streamlit application or deployment is created by this record.

## Validation and reconsideration

Later work must validate isolated restoration, bounded local startup, deterministic governed-data rendering, controlled termination, and no provider/network product dependency. Version changes follow intentional WP06 upgrade and recreation governance. Reconsider if production UI needs, customization, scaling, security, deployment topology, or runtime ownership exceed Streamlit's suitability. Compatibility and deployment evidence remain in `PYTHON_RUNTIME_COMPATIBILITY.md`.

This supports Release 1.8 visualization readiness and does not implement Release 1.9 or a production dashboard.

Evidence and policy: https://pypi.org/project/streamlit/1.61.1/ and https://pypi.org/pypi/streamlit/1.61.1/json. Revisit on upstream security/support changes, Python compatibility changes, UI/deployment boundary changes, or a governed resolver conflict. Do not launch a persistent server in WP07.
