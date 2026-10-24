FROM mcr.microsoft.com/dotnet/sdk:10.0@sha256:3a48280a9a7c6276c3eeaba1efa6a08820f18c84fb9e6274c187719ecfaf9fbc AS build
WORKDIR /fixture
COPY tests/runtime/release_1_15/dual_docker/ResearchFixture.csproj ./
RUN --network=none dotnet restore ResearchFixture.csproj --ignore-failed-sources
COPY tests/runtime/release_1_15/dual_docker/Program.cs ./
RUN --network=none dotnet publish ResearchFixture.csproj -c Release --no-restore -o /out
FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f
WORKDIR /app
COPY --from=build --chown=0:0 /out/ ./
RUN mkdir /private && chown 12001:12001 /private && chmod 0700 /private
ENV DOTNET_EnableDiagnostics=0 DOTNET_PROCESSOR_COUNT=1
USER 12001:12001
EXPOSE 52731
STOPSIGNAL SIGTERM
ENTRYPOINT ["dotnet", "/app/ResearchFixture.dll"]
