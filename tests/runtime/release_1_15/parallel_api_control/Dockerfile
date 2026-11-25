FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f
WORKDIR /app
COPY publish/ ./
USER 1654:1654
EXPOSE 52731
ENTRYPOINT ["/bin/sh", "-c", "unset AIQ_RESEARCH_API_KEY; printf 'Parallel API control. stage=BOOTSTRAP_ENTRY\\n' >&2; exec dotnet /app/ParallelApiControl.dll"]
