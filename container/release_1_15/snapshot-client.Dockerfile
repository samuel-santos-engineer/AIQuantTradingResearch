# Test-only stdlib driver, not a Streamlit image or deployment candidate.
FROM python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024
WORKDIR /app
COPY client/research_snapshot_client.py ./research_snapshot_client.py
ENV PYTHONDONTWRITEBYTECODE=1
USER 1655:1655
ENTRYPOINT ["python", "-B", "research_snapshot_client.py"]
