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

## Release 1.9 presentation checks

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

The current suite is 17 tests. It includes parser, visualization-frame,
factual-section, and permanent integration coverage. The Streamlit adapter is
an independently launched read-only consumer; the finite WP08 harness and its
probe are acceptance-only and are not a production supervisor or generic Python
bridge. See [the interoperability boundary](../architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md)
for the distinct Release 1.8 JSON-over-stdio boundary and the Release 1.9
handoff/lifecycle rules.

If a local test assembly is blocked by Windows App Control, use only the
documented local-development Authenticode signing setup above. It keeps App
Control enabled, uses uncommitted `Directory.Build.local.props`, and is not
production trust or a Smart App Control bypass.

## Verify the repository

The current governed .NET baseline is 339 passing tests: 11 Domain, 125
Application, 182 Infrastructure, and 21 Architecture. Run the repository's
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
foundation. Release 1.9 adds only the governed deterministic simulated/replay
visualization presentation flow described above. Model training, prediction,
real-provider streaming, persistent services, remote Python execution,
OpenTelemetry, and Backtesting remain outside the delivered scope.
