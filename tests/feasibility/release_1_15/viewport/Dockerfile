# Local existing image only; verify its ID and installed versions in evidence.
FROM aiq-c2b36:candidate
USER root
WORKDIR /spike
COPY tests/feasibility/release_1_15/viewport/ /spike/
RUN /opt/venv/bin/python -c "import streamlit,plotly; from pathlib import Path; assert streamlit.__version__ == '1.61.1' and plotly.__version__ == '7.1.0'; assert (Path(plotly.__file__).parent/'package_data/plotly.min.js').is_file()"
ENV HOME=/tmp PYTHONDONTWRITEBYTECODE=1 STREAMLIT_BROWSER_GATHER_USAGE_STATS=false
USER 1000:1000
ENTRYPOINT ["/opt/venv/bin/python", "-B", "-m", "streamlit", "run", "/spike/app.py", "--server.address=0.0.0.0", "--server.port=8501", "--browser.gatherUsageStats=false", "--browser.serverAddress=127.0.0.1"]
