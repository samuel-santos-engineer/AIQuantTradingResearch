FROM mcr.microsoft.com/dotnet/sdk:10.0@sha256:3a48280a9a7c6276c3eeaba1efa6a08820f18c84fb9e6274c187719ecfaf9fbc AS build
WORKDIR /src
COPY Directory.Build.props Directory.Build.targets Directory.Packages.props global.json ./
COPY tests/feasibility/release_1_15/runtime/ResearchStub.csproj tests/feasibility/release_1_15/runtime/
COPY --from=offline / /offline/
RUN --network=none dotnet restore tests/feasibility/release_1_15/runtime/ResearchStub.csproj --source /offline --ignore-failed-sources
COPY tests/feasibility/release_1_15/runtime/Program.cs tests/feasibility/release_1_15/runtime/
RUN --network=none dotnet publish tests/feasibility/release_1_15/runtime/ResearchStub.csproj -c Release --no-restore -o /out
FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f AS dotnet
FROM aiq-c2b36:candidate
USER root
COPY --from=dotnet /usr/share/dotnet /usr/share/dotnet
COPY --from=build /out /stub
WORKDIR /spike
COPY tests/feasibility/release_1_15/runtime/ /spike/
RUN /opt/venv/bin/python -c "import streamlit,plotly; assert streamlit.__version__=='1.61.1' and plotly.__version__=='7.1.0'"     && printf 'research:x:12001:12001::/tmp:/usr/sbin/nologin\nstreamlit:x:12002:12002::/tmp:/usr/sbin/nologin\n' >> /etc/passwd     && printf 'research:x:12001:\nstreamlit:x:12002:\n' >> /etc/group     && mkdir /private && chmod 0700 /private
ENV PYTHONDONTWRITEBYTECODE=1 PYTHONUNBUFFERED=1
ENTRYPOINT ["/usr/local/bin/python", "-B", "/spike/supervisor.py"]
