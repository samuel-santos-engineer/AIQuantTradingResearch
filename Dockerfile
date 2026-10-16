FROM mcr.microsoft.com/dotnet/sdk:10.0@sha256:3a48280a9a7c6276c3eeaba1efa6a08820f18c84fb9e6274c187719ecfaf9fbc AS build
WORKDIR /src

COPY Directory.Build.props Directory.Build.targets Directory.Packages.props global.json ./
COPY src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj src/AIQuantTradingResearch.Application/
COPY src/AIQuantTradingResearch.Domain/AIQuantTradingResearch.Domain.csproj src/AIQuantTradingResearch.Domain/
COPY src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj src/AIQuantTradingResearch.Infrastructure/
COPY src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj src/AIQuantTradingResearch.Worker/
RUN dotnet restore src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj

COPY src/ src/
RUN dotnet publish src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --configuration Release --no-restore --output /out/worker

FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f AS dotnet-runtime

FROM python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024 AS private-runtime
ENV DOTNET_ROOT=/usr/share/dotnet \
    PATH=/usr/share/dotnet:/opt/venv/bin:$PATH \
    PYTHONDONTWRITEBYTECODE=1 \
    PYTHONUNBUFFERED=1 \
    STREAMLIT_SERVER_ADDRESS=0.0.0.0 \
    STREAMLIT_SERVER_PORT=8501 \
    Visualization__HandoffPath=/runtime/visualization-read-model.json \
    Persistence__DatabasePath=/runtime/aiq.sqlite

COPY --from=dotnet-runtime /usr/share/dotnet /usr/share/dotnet
WORKDIR /app

RUN apt-get update \
    && apt-get install --no-install-recommends --yes gosu libicu76 \
    && rm -rf /var/lib/apt/lists/*

COPY requirements.txt ./
RUN python -m venv /opt/venv \
    && /opt/venv/bin/pip install --no-cache-dir --requirement requirements.txt

COPY python/presentation/ /app/python/presentation/
COPY favicon.ico imgs/icon.png /app/
COPY --from=build /out/worker/ /app/worker/
COPY container/entrypoint.sh /usr/local/bin/aiq-entrypoint

RUN chmod 0555 /usr/local/bin/aiq-entrypoint \
    && mkdir -p /runtime \
    && useradd --create-home --shell /usr/sbin/nologin aiq \
    && chown -R aiq:aiq /app /runtime

# The entrypoint prepares only the configured persistence parent, then drops
# permanently to this account before starting the Worker or Streamlit.
USER root
EXPOSE 8501
ENTRYPOINT ["/usr/local/bin/aiq-entrypoint"]

# Offline local candidate generation is never part of startup or a request.
FROM build AS immutable-handoff
RUN --network=none DOTNET_ENVIRONMENT=Development AIQ_IMMUTABLE_EXPORT=1 dotnet /out/worker/AIQuantTradingResearch.Worker.dll --write-immutable-chart-candidate /out/candidate

FROM python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024 AS synthetic-candidate
ENV PATH=/opt/venv/bin:$PATH PYTHONDONTWRITEBYTECODE=1 PYTHONUNBUFFERED=1
WORKDIR /app
COPY requirements.txt requirements-linux.lock ./
RUN python -m venv /opt/venv && /opt/venv/bin/pip install --no-cache-dir --requirement requirements.txt --constraint requirements-linux.lock --report /app/linux-install-report.json
COPY python/presentation/ /app/python/presentation/
COPY favicon.ico imgs/icon.png /app/
COPY container/entrypoint.sh /usr/local/bin/aiq-entrypoint
COPY --from=immutable-handoff /out/candidate/ /app/python/presentation/_immutable_chart_candidate/
RUN useradd --uid 1000 --create-home --shell /usr/sbin/nologin aiq \
    && chmod 0555 /usr/local/bin/aiq-entrypoint \
    && find /app -type d -exec chmod 0555 {} + \
    && find /app -type f -exec chmod 0444 {} +
USER 1000:1000
EXPOSE 8501
ENTRYPOINT ["/usr/local/bin/aiq-entrypoint"]
