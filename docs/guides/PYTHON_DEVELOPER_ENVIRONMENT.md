# Python Developer Environment

## Purpose

This guide establishes the portable local workflow for the delivered Release
1.8 Python engineering foundation and the bounded Release 1.9 Streamlit
presentation adapter. It does not introduce a product ML workflow, model
training, provider access, or a live-market-data service.

## Runtime and ownership

The current governed machine runtime is CPython **3.13.15 x64**. Machine Python
is the bootstrap/base runtime only. The repository owns its dependencies in the
ignored, disposable root `.venv`; do not install project dependencies globally.
Use the official Microsoft VS Code Python extension (`ms-python.python`) for
editor discovery, and select the workspace `.venv` when it exists. Do not
commit user-specific or absolute interpreter paths.

`requirements.txt` is the authoritative direct declaration:

- `numpy==2.5.1`
- `pandas==3.0.5`
- `scikit-learn==1.9.0`
- `streamlit==1.61.1`

Transitive packages are resolver output, not direct product selections. Use the
interpreter-qualified `pip` command below; version changes and removals follow
the dependency governance record rather than ad hoc upgrades.

## Create or restore the environment

From the repository root in PowerShell:

```powershell
py -3.13 --version
py -3.13 -m venv .venv
.\.venv\Scripts\python.exe -m pip install -r requirements.txt
.\.venv\Scripts\python.exe -m pip check
```

The expected runtime is Python 3.13.15 and the exact direct pins above. If the
launcher does not resolve the governed minor line, correct the machine runtime
selection before proceeding; do not fall back to the WindowsApps alias or a
global package installation.

The environment is reproducible and disposable. To recreate it, remove only
the repository-root `.venv`, then repeat the commands above. Never remove a
machine Python installation as part of this workflow.

## Validate the Python foundation

Run the deterministic, offline WP08 evidence with the qualified environment
interpreter:

```powershell
.\.venv\Scripts\python.exe python\validation\scientific_stack_validation.py
```

It validates meaningful NumPy, pandas, scikit-learn, and Streamlit behavior
without a persistent server, real/provider data, or a third-party Python test
framework. `python/validation/` is non-production evidence and is separate from
the production JSON-over-stdio endpoint in `python/integration/`.

The .NET boundary is a local, one-shot, out-of-process capability invocation.
It resolves the repository-local `.venv` interpreter and
`python/integration/protocol_endpoint.py`, uses one versioned JSON request and
response over standard I/O, reserves stdout for protocol output, captures only
bounded stderr diagnostics, and terminates only processes it owns on timeout or
cancellation. See [the interoperability boundary](../architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md)
for contracts, failure mapping, security, and portability rules.

Windows developers whose Smart App Control blocks locally built test binaries
may need [local-development Authenticode signing](../development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md).

## Release 1.9 and 1.10 presentation checks

Release 1.9 uses the same governed `.venv`. Streamlit 1.61.1 renders only the
Worker-published local visualization read model; it does not read SQLite, call
a provider, or compute the pipeline/feature evidence. Current visualization
and demo flows use deterministic simulated/replay data for local testing and
demonstration. They are not a live market-data feed or a statement of live
trading suitability.

Run the governed presentation tests from the repository root:

```powershell
Push-Location .\python\presentation
..\..\.venv\Scripts\python.exe -m unittest discover -p "test_*.py"
Pop-Location
```

The current suite is 25 tests. It includes parser, visualization-frame,
factual-section, permanent integration, and Release 1.10 no-bypass coverage.
The Streamlit adapter is
an independently launched read-only consumer; the finite WP08 harness and its
probe are acceptance-only and are not a production supervisor or generic Python
bridge. See [the interoperability boundary](../architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md)
for the distinct Release 1.8 JSON-over-stdio boundary and the Release 1.9
handoff/lifecycle rules.

If a local test assembly is blocked by Windows App Control, use only the
documented local-development Authenticode signing setup above. It keeps App
Control enabled, uses uncommitted `Directory.Build.local.props`, and is not
production trust or a Smart App Control bypass.

## Release 1.10 System Health and observability

Release 1.10 adds bounded in-process observations to the existing .NET pipeline
and its governed boundaries. It does not add live providers, trading, ML,
backtesting, an external telemetry exporter, or a telemetry backend. The
canonical handoff remains `aiq-visualization-read-model-v1` and SQLite schema
remains v4. The optional nested `systemHealth` extension is .NET-owned; Python
and Streamlit are read-only consumers and never inspect SQLite, providers,
Worker processes, listeners, or exporter internals.

Visualization state (`Ready`, `WarmUp`, `Empty`, `Stale`, `Failed`) is separate
from System Health. System Health is exactly `ready`, `warmup`, `empty`,
`failed`, `stale`, or `unavailable`; `degraded` is not a Release 1.10 health
state. The finite reason vocabulary is `pipeline-failed`,
`structural-staleness`, and `required-health-evidence-unavailable`, with no
reason for ready, warmup, or empty. There is no health age or additional
freshness threshold: `stale` retains the structural visualization meaning.

In Streamlit, **System Health** appears immediately after the target/state
subheader. `ready`, `warmup`, and `empty` use informational messages; `failed`
uses an error; `stale` and `unavailable` use warnings. Missing health in a
legacy v1 document deterministically becomes unavailable. Malformed health is
an integrity warning that retains safe last-good presentation data rather than
deriving health from unrelated fields.

## Bounded local verification and troubleshooting

From the repository root, use only the governed commands and do not introduce a
second service, SQLite inspection path, or Streamlit-to-Worker supervision:

```powershell
dotnet build
dotnet test tests\AIQuantTradingResearch.Application.Tests\AIQuantTradingResearch.Application.Tests.csproj --no-restore
dotnet test tests\AIQuantTradingResearch.Infrastructure.Tests\AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore
dotnet test tests\AIQuantTradingResearch.Architecture.Tests\AIQuantTradingResearch.Architecture.Tests.csproj --no-restore
dotnet test tests\AIQuantTradingResearch.Domain.Tests\AIQuantTradingResearch.Domain.Tests.csproj --no-restore
Push-Location .\python\presentation
..\..\.venv\Scripts\python.exe -m unittest discover -p "test_*.py"
Pop-Location
.\.venv\Scripts\python.exe -m pip check
.\.venv\Scripts\python.exe -m streamlit --version
gitleaks git . --redact --verbose
```

The expected Streamlit version is 1.61.1. The dedicated Release 1.10 permanent
coverage is in `Release110ObservabilityPermanentTests.cs` in the Application
and Infrastructure test projects, `Release110ObservabilityNoBypassTests.cs` in
the Architecture test project, and
`python/presentation/test_release_1_10_observability_no_bypass.py`. Existing
WP02–WP05 tests remain their predecessor owners.

If the handoff is absent, confirm the Worker has published the governed file
under its existing runtime configuration; do not substitute direct SQLite or
provider access. If it is malformed, retain the last-good presentation and
address the producing boundary. If Streamlit cannot start, verify the `.venv`,
exact pins, Streamlit version, and `pip check`. If a built assembly is blocked
by App Control, use only the local signing guide below; it is an environment
remediation, not an observability or lifecycle repair. After local validation,
stop only processes and remove only temporary artifacts owned by that run.

## Verify the repository

The current governed .NET baseline is 365 passing tests: 11 Domain, 136
Application, 191 Infrastructure, and 27 Architecture. Run the repository's
canonical verification from the root:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\eng\verify.ps1
```

The process-scoped bypass permits the repository-owned verification scripts to
run on Windows workstations whose normal policy blocks direct `.ps1` execution;
it does not change the user or machine execution policy.

The Python scientific validation complements, but does not replace, those
permanent tests. Keep credentials, external provider configuration, generated
runtime data, `.venv`, and local interpreter paths out of commits.

## Release boundary

Release 1.8 delivers runtime, dependency, validation, and interoperability
foundation. Release 1.9 adds the governed deterministic simulated/replay
visualization presentation flow. Release 1.10 adds bounded in-process pipeline
and boundary observations plus the compatible System Health projection described
above. All displayed market evidence remains deterministic/replay/simulated and
non-live. Model training, prediction, real-provider streaming, persistent
services, remote Python execution, external telemetry backends, and Backtesting
remain outside the delivered scope.
