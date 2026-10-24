# Existing qualified Python dependencies are supplied as an explicit named context.
# The runner verifies its immutable local image ID before building (no pip install).
FROM qualified_python AS dependencies
FROM python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024
COPY --from=dependencies /opt/venv/ /opt/venv/
WORKDIR /app
COPY --chown=0:0 tests/runtime/release_1_15/dual_docker/api_key_client_fixture.py ./
COPY --chown=0:0 tests/runtime/release_1_15/dual_docker/public_app_fixture.py ./
RUN mkdir /ui && chown 12002:12002 /ui && chmod 0700 /ui
ENV PATH=/opt/venv/bin:/usr/local/bin:/usr/bin:/bin HOME=/ui PYTHONDONTWRITEBYTECODE=1 PYTHONUNBUFFERED=1
USER 12002:12002
EXPOSE 8501
STOPSIGNAL SIGTERM
ENTRYPOINT ["/opt/venv/bin/python", "-B", "-m", "streamlit", "run", "/app/public_app_fixture.py", "--server.address=0.0.0.0", "--server.port=8501", "--server.headless=true", "--server.fileWatcherType=none", "--browser.gatherUsageStats=false", "--logger.hideWelcomeMessage=true", "--client.showErrorDetails=none", "--secrets.files=[]"]
