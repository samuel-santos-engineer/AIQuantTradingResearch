FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Directory.Build.props Directory.Build.targets Directory.Packages.props global.json ./
COPY src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj src/AIQuantTradingResearch.Application/
COPY src/AIQuantTradingResearch.Domain/AIQuantTradingResearch.Domain.csproj src/AIQuantTradingResearch.Domain/
COPY src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj src/AIQuantTradingResearch.Infrastructure/
COPY src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj src/AIQuantTradingResearch.Worker/
RUN dotnet restore src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj

COPY src/ src/
RUN dotnet publish src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --configuration Release --no-restore --output /out/worker

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS dotnet-runtime

FROM python:3.13-slim AS runtime
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
    && apt-get install --no-install-recommends --yes libicu76 \
    && rm -rf /var/lib/apt/lists/*

COPY requirements.txt ./
RUN python -m venv /opt/venv \
    && /opt/venv/bin/pip install --no-cache-dir --requirement requirements.txt

COPY python/presentation/ /app/python/presentation/
COPY --from=build /out/worker/ /app/worker/
COPY container/entrypoint.sh /usr/local/bin/aiq-entrypoint

RUN chmod 0555 /usr/local/bin/aiq-entrypoint \
    && mkdir -p /runtime \
    && useradd --create-home --shell /usr/sbin/nologin aiq \
    && chown -R aiq:aiq /app /runtime

USER aiq
EXPOSE 8501
ENTRYPOINT ["/usr/local/bin/aiq-entrypoint"]
