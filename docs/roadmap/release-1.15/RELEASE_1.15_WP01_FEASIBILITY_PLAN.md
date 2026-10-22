# Release 1.15 WP01 - Exact-path implementation and feasibility proposal

Status: NOT EXECUTED; SOURCE AUTHORITY REQUIRED. Issue #367. Baseline and decisions are in the [reconciliation](RELEASE_1.15_WP01_BASELINE_RECONCILIATION.md) and [contract proposal](RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md). Every future path below is a proposal, not a mutation grant. No directory wildcard authorizes additional files.

All future source, tests, scripts and evidence must be UTF-8 with LF only. Independently scan changed text for CRLF/lone CR and run `git diff --check` before commit/PR; preserve unrelated historical endings. Any Windows host orchestration script must support Windows PowerShell 5.1.26100.9444.

## Sequence and stop rules

1. Review the four WP01 planning documents and record dispositions for D1-D7. Authorize S1 and S2 separately with literal paths, installed dependency versions, bounded commands and evidence destinations.
2. S1 proves whether supported chart interaction can carry viewport state. If native capture fails, report its exact limitation and request a separate component disposition. Do not patch Streamlit or quietly waive viewport behavior.
3. S2 screens the proposed combined process topology using deterministic fixtures and isolated test storage. Resolve identity, privilege and mount questions before WP02 implementation. A local Docker pass cannot prove Azure mount durability or F1 capacity.
4. Authorize the small contract slice below only after its schema/numerical decisions are accepted. Then approve later WP-specific path inventories in dependency order. This plan is not an allowlist for the entire release.
5. Preserve Release 1.14 source/tests and default runtime until an explicit integration PR lists them. Stop if a package, project, CI file, generated asset, additional path or architecture amendment is needed. Do not install dependencies, execute Docker or contact a provider under the current planning authority.

## Proposed S1: native viewport feasibility

All files new; create only after separate source authority:

| Exact proposed path | Purpose |
| --- | --- |
| `tests/feasibility/release_1_15/viewport/fixture_snapshots.py` | Deterministic fake 1,000-row BTC/USD and ETH/USD presentation snapshots, gaps/corrections/generation/date variants |
| `tests/feasibility/release_1_15/viewport/viewport_state.py` | Minimal state reconciliation model; no research or provider access |
| `tests/feasibility/release_1_15/viewport/test_viewport_state.py` | Independent expected UTC ranges, command/request ordering and session isolation negatives |
| `tests/feasibility/release_1_15/viewport/app.py` | Native supported Streamlit/Plotly rendering and state evidence |
| `tests/feasibility/release_1_15/viewport/Dockerfile` | Isolated Linux candidate using repository dependency pins, no production target change |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp01-viewport-feasibility-evidence.md` | Sanitized commands/version/digest/screenshots/results and native limitation report |

Read-only implementation references: `python/presentation/realtime_financial_visualization.py`, `python/presentation/historical_market_bridge.py`, `requirements.txt`, `requirements-linux.lock`, `Dockerfile`. Verify pins and available browser tooling before execution. No new browser automation package is authorized by this list; use an existing supported browser or owner-assisted verification if automation is unavailable.

The installed 1.61.1 `st.plotly_chart` supports selection events; no supported viewport callback was established by static inspection. [Streamlit chart documentation](https://docs.streamlit.io/develop/api-reference/charts/st.plotly_chart) must be checked against the pinned installed implementation, not treated as a version upgrade. [Plotly relayout events](https://plotly.com/javascript/plotlyjs-events/) are supported for a chart one owns, but that alone does not demonstrate access through native Streamlit. [Streamlit components](https://docs.streamlit.io/develop/concepts/custom-components/intro) are an alternative supported boundary, not permission for DOM injection. If a component is needed, stop and present its literal files/dependency impact for separate approval; none is preauthorized here.

Bounded procedure proposed for later execution:

- One loopback-published Linux container, fake data only, no credentials, provider calls, public/LAN listener or production mount. Record image digest and actual Streamlit/Plotly/browser versions. Observe two independent browser sessions, at most 15 minutes per case set.
- Capture actual Plotly x/y ranges and visible trace state before/after; screenshots alone do not prove exact ranges. No private payloads in evidence. Confirm hover, UTC labels, candles/volume, modebar, overlay off/on/off and selectors with visible state changes.
- Test default 300 and presets 100/1,000; manual pan/zoom persists across changed refresh, unchanged response (no redraw), overlay toggle and instrument round trip. Return to Live/preset resets are intentional. Check both axes where applicable, not only the x-axis.
- Test partial-overlap pruning, fully expired viewport, disconnection/reconnect, UTC transformation-date rollover, generation replacement, correction/events/gaps and remount. Preserve real gaps and eligible absolute UTC ranges; never merge generations or retain stale annotations.
- Inject deterministic old request epochs, late browser events, programmatic range echoes and remount tokens in the offline state model. A stale event must not overwrite a newer manual range; one request plus one coalesced pending tick is the upper bound. Zoom/pan/toggle makes no fetch. Session A preferences cannot affect session B.
- Record each case PASS/FAIL/INCOMPLETE with exact before/after ranges, selected instrument/preset, identity, trace state and command/interaction revision. A failed automated click is INCOMPLETE. No inference from a click alone.

Candidate focused command after authority: `python -B -m unittest discover -s tests/feasibility/release_1_15/viewport -p test_viewport_state.py -v`. This path does not exist yet; the command was NOT run. Container build/start/browser commands must be frozen after S1 source review and before execution; no invented test count or performance result.

Success means a supported, proportionate viewport channel passes the above. A native limitation can close the investigation with a BLOCKED result and a proposed component alternative, not a false functional pass.

## Proposed S2: combined Docker memory/startup and identity feasibility

All files new; independent authority required:

| Exact proposed path | Purpose |
| --- | --- |
| `tests/feasibility/release_1_15/runtime/ResearchStub.csproj` | Isolated .NET 10 fixture host using existing central package pins only |
| `tests/feasibility/release_1_15/runtime/Program.cs` | Fixture-only localhost snapshots and isolated SQLite transaction probes; never a real provider |
| `tests/feasibility/release_1_15/runtime/supervisor.py` | Minimal proposed startup/restart/reaping policy, not production bootstrap |
| `tests/feasibility/release_1_15/runtime/test_supervisor.py` | Deadline/backoff/no-overlap policy cases using doubles |
| `tests/feasibility/release_1_15/runtime/public_app.py` | Streamlit reader with sanitized fixture-only environment |
| `tests/feasibility/release_1_15/runtime/Dockerfile` | Separate Linux/amd64 feasibility image with distinct test identities |
| `tests/feasibility/release_1_15/runtime/qualify_runtime.sh` | Bounded container-only observations and confirmed-owned cleanup |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp01-runtime-feasibility-evidence.md` | Sanitized identity/resource/durability limitations and measurements |

Read-only references: `Dockerfile`, `container/entrypoint.sh`, `Directory.Packages.props`, `Directory.Build.props`, `src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj`, `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs`. Verify exact filename casing and existing package availability before authority. The stub is not added to the production solution. Any unavailable SDK/package/browser dependency blocks execution rather than permitting installation or a project-file edit.

Proposed bounded screening profile, subject to owner execution approval: Linux/amd64, one CPU, 512 MiB hard memory limit, 64 PIDs, read-only root, 32 MiB temporary filesystem, owned private test volume, no privileged mode/host PID namespace/capability additions, loopback host publication only. These are local screening limits, not asserted Azure F1 specifications. If the selected trust model cannot start under them, stop; do not relax isolation to manufacture a pass.

Use fake credentials/canary secrets only. Bootstrap must assign distinct fixed non-root child identities and private directory permissions before starting either child. Record UID/GID, supplementary groups, capabilities, environment inheritance and process ownership without secret values. Verify public identity cannot open SQLite, journal, checkpoint, watermark, canary secrets or another process's sensitive environment/descriptors. Check private listener is unreachable through public HTTP and no raw fixture-only fields enter public DTOs/errors. Root/supervisor trust and cross-UID shutdown must be explicitly explained, not inferred from successful startup.

Proposed test envelope: at most three cold starts plus 1/3/5 simultaneous sessions, each session-load stage at most ten minutes. Include one independently interrupted private process and one UI interruption, under later explicit fault-test authority. Verify bounded backoff, no old/new child overlap, authoritative private storage survives UI restart, PID1 reaps children and confirmed-owned shutdown terminates the tree. Stop on uncertain process ownership; no host-wide kill or cleanup. No advanced WP07 guardian experiments are included.

Record cold/warm startup, C# snapshot/commit time, Python reconciliation, browser render and interaction latency separately; process RSS plus container current/peak memory where available; CPU, PID/thread counts, cache bytes, restarts, timeouts and variability. Do not sum shared RSS as if it were container memory. If peak accounting is unavailable, report that measurement gap. Compare observed values against configurable budgets only after an owner-approved baseline, not invented throughput.

SQLite probes use disposable owned storage and fake market rows. Test atomic commit visibility and post-commit memory publication, checkpoint/watermark mismatch classification, denied public reads including sidecars, and deterministic recovery. Local volume results do not establish App Service mount locking, fsync/rename/persistence semantics. Actual Azure filesystem and restart evidence belongs to separately authorized WP07, not S2.

Candidate future offline command: `python -B -m unittest discover -s tests/feasibility/release_1_15/runtime -p test_supervisor.py -v`. Build/start/fault commands require review of the stub and harness before explicit execution authority. They have NOT run. No image is to be pushed or promoted.

## Proposed first contract implementation slice

After decisions D3-D5 and schema approval, the following is the proposed initial source/test boundary, not current authority. All paths are new; existing synthetic contracts stay intact.

| Exact proposed path | Responsibility |
| --- | --- |
| `src/AIQuantTradingResearch.Application/Research/ResearchSnapshotContracts.cs` | Immutable public presentation envelope/identity/status definitions |
| `src/AIQuantTradingResearch.Application/Research/ResearchSnapshotValidator.cs` | Bounded pure full/unchanged validation; no provider or persistence calls |
| `tests/AIQuantTradingResearch.Application.Tests/ResearchSnapshotContractTests.cs` | Independent positive/negative wire and state vectors |
| `python/presentation/research_snapshot_contract.py` | Matching bounded consumer validation and accepted-identity rules |
| `python/presentation/test_research_snapshot_contract.py` | Cross-language parity, malformed inputs and session/request negatives |

Fixtures should be literal independently reviewed vectors inside these authorized test files initially; external fixtures/generated datasets need an added explicit allowlist. Future commands: `dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-restore --filter FullyQualifiedName~ResearchSnapshotContractTests` and `python -B -m unittest python.presentation.test_research_snapshot_contract -v`, only after source/test authority and offline assets are verified. No expected pass count exists yet.

WP02-WP08 need subsequent literal implementation manifests. Candidate existing integration points are identified in reconciliation; they are not implicitly writable. In particular, no current edit to `Dockerfile`, `container/entrypoint.sh`, Worker `Program.cs`, Twelve Data adapters, SQLite factory, indicator calculators or the existing chart is proposed for this planning PR.

## Run-only preservation inventory

These existing files may supply later regression evidence; they are not mutation paths and were not executed under this planning authority:

- `tests/AIQuantTradingResearch.Application.Tests/HistoricalMarketDataContractsTests.cs`
- `tests/AIQuantTradingResearch.Application.Tests/TechnicalIndicatorCalculatorTests.cs`
- `tests/AIQuantTradingResearch.Application.Tests/WilderAverageTrueRangeCalculatorTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataClientTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataTimeSeriesNormalizerTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/LocalSyntheticChartTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/ImmutableSyntheticChartHandoffTests.cs`
- `python/presentation/test_historical_market_bridge.py`
- `python/presentation/test_local_synthetic_chart.py`
- `python/presentation/test_immutable_synthetic_chart.py`
- `python/presentation/test_public_preview_api.py`

Before later implementation authority, resolve test-project coupling, central package availability and any restored build assets. Application/Infrastructure existing suites and synthetic Python tests protect Release 1.14; they cannot substitute for minute ingestion, regime, browser or Azure qualification.
