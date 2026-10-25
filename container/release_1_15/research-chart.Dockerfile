# Build context is the runner's allowlisted public source copy; no fixtures or secrets.
FROM qualified_python AS dependencies
FROM python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024
COPY --from=dependencies /opt/venv/ /opt/venv/
WORKDIR /app
COPY favicon.ico ./favicon.ico
COPY python/presentation/ ./python/presentation/
RUN mkdir /ui && chown 12002:12002 /ui && chmod 0700 /ui
ENV PATH=/opt/venv/bin:/usr/local/bin:/usr/bin:/bin HOME=/ui XDG_CACHE_HOME=/tmp/cache PYTHONDONTWRITEBYTECODE=1 PYTHONUNBUFFERED=1 AIQ_RESEARCH_CHART=1 OPENBLAS_NUM_THREADS=1
USER 12002:12002
EXPOSE 8501
STOPSIGNAL SIGTERM
ENTRYPOINT ["/opt/venv/bin/python", "-B", "-m", "streamlit", "run", "/app/python/presentation/realtime_financial_visualization.py", "--server.address=0.0.0.0", "--server.port=8501", "--server.headless=true", "--server.fileWatcherType=none", "--browser.gatherUsageStats=false", "--logger.hideWelcomeMessage=true", "--client.showErrorDetails=none", "--secrets.files=[]"]
