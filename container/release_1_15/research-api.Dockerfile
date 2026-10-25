# Prepared for later separately authorized Linux qualification; no restore, pull or provider activation.
FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f
WORKDIR /app
# Context must contain a previously built linux/amd64 framework-dependent publish in research/.
COPY research/ ./
COPY container/release_1_15/research-bootstrap.sh /app/research-bootstrap.sh
ENV AIQ_RESEARCH_BIND_ADDRESS=0.0.0.0
USER 1654:1654
EXPOSE 52731
ENTRYPOINT ["/bin/sh", "/app/research-bootstrap.sh"]
