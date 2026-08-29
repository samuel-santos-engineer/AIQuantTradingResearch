# Release 1.10 — Execution Plan

## Model assignment

- **GPT-5.6 Luna** — contract, architecture, policy, acceptance reconciliation, and read-only audits.
- **GPT-5.6 Terra** — implementation, tests, validation, Git/GitHub, merge, and publication mutations after acceptance.
- **GPT-5.6 Sol** — supporting analysis and non-authoritative review.

**Selected execution model for this plan: GPT-5.6 Luna for planning; GPT-5.6 Terra for later execution.**

## Dependency order

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

WP01 must select the foundational OpenTelemetry technology and policy before any package or exporter is introduced. WP02–WP04 preserve the existing .NET pipeline and boundary. WP05 consumes only the resulting governed read model. WP06 proves no-bypass and lifecycle behavior. WP07 aligns documentation. WP08 is acceptance/PR readiness and does not automatically merge or close milestone #59.

## Package controls

Every WP authority must define all three roles and its selected execution model. Every implementation authority must read the accepted definition, plan, and manifest, verify its predecessor, map each requirement to an authorized path and proof, and stop on missing or contradictory semantics. No WP may start the next WP.

## Release gates

1. Exact path and scope audit against the manifest.
2. Build and full .NET regression from the live predecessor baseline.
3. Governed Python/Streamlit compatibility and regression checks where applicable.
4. Architecture/no-bypass and schema-v4 preservation.
5. Security, redaction, dependency, documentation, and provenance truthfulness gates.
6. Deterministic lifecycle, cancellation/restart, exporter-failure, and residue matrix.
7. Dedicated release branch → acceptance → PR → verification → merge; no direct push to `main`.

## Explicit exclusions

No live provider, trading, ML, explainability, backtesting, cloud deployment, direct SQLite/UI access, parallel pipeline, generic RPC, schema migration, or unselected telemetry package/backend is authorized by this plan.

## Deterministic work-package contracts

All eight contracts inherit the release architecture: Domain remains telemetry-free; Application owns semantic pipeline facts; Infrastructure owns provider/storage/process mechanics; Worker owns process lifecycle; the existing JSON-over-stdio and atomic file handoff remain canonical; Streamlit consumes governed read models only. All use fixed synthetic/replay inputs for offline validation and must preserve the explicit non-live provenance disclosure.

### WP01 — Observability Selection, Vocabulary & Scope

- **Objective:** Select and document one governed OpenTelemetry technology/dependency/exporter policy and freeze the versioned vocabulary, correlation, timing, redaction, cardinality, degraded-mode, and reconsideration rules needed by later WPs.
- **In scope:** alternatives and compatibility evaluation; selection record; event/metric names and units; required/optional attributes; sampling/redaction/cardinality; disabled/degraded behavior; dependency/license/security policy.
- **Out of scope:** package installation, instrumentation, exporter deployment, schema migration, System Health UI, live telemetry backend, and changes to pipeline semantics.
- **Direct dependencies:** Release 1.9 accepted boundary only. **Selected model:** GPT-5.6 Luna.
- **Architecture/provenance:** technology-neutral Application vocabulary; no Domain dependency; no claim that telemetry proves live market data.
- **Owned paths:** expected add `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`; validation-only existing observability/compatibility docs; no source/package changes.
- **Acceptance:** one decision is selected with alternatives/trade-offs; every foundational dependency has rationale/version policy/boundaries/triggers; vocabulary and redaction rules are deterministic; unresolved choices are explicitly assigned to a later Luna authority.
- **Validation/security:** documentation/link review, compatibility/license/security review, secret scan; no installation or runtime mutation.
- **Completion boundary:** authorizes later implementation WPs to use only the accepted selection; does not authorize implementation itself.

### WP02 — Application Pipeline Observability Contract

- **Objective:** Expose additive, technology-neutral semantic observations for the existing five-stage pipeline and correlation identity.
- **In scope:** immutable observation/event contract; stage start/finish/outcome/duration/count semantics; stable run/correlation identity; success/empty/failed/cancelled/degraded states; backward-compatible optionality.
- **Out of scope:** OpenTelemetry API references in Domain/Application contracts, provider instrumentation, persistence, exporter configuration, UI, and schema changes.
- **Direct dependencies:** WP01. **Selected model:** GPT-5.6 Terra.
- **Architecture/provenance:** Application is semantic owner; existing stage order/result identity/replay semantics unchanged; provenance is carried factually, never upgraded to live.
- **Owned paths:** exactly `src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs` (add), `src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionUseCase.cs` (modify), `src/AIQuantTradingResearch.Application/Datasets/MaterializeDatasetUseCase.cs` (modify), and `tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs` (add). No Domain, Infrastructure, Worker, UI, project/package, schema, or configuration paths are owned.
- **Acceptance:** all defined outcomes and timing units are representable; old consumers remain valid; no exporter/provider/SQLite type leaks; repeated fixed input yields identical semantic observations.
- **Validation/security:** focused Application/architecture suites, API-surface/no-bypass checks, deterministic repeatability, secret/cardinality review.
- **Completion boundary:** supplies the contract to WP03/WP04; does not instrument concrete systems.

#### WP02 deterministic execution contract

WP02 uses the .NET BCL `System.Diagnostics.ActivitySource`, `Activity`, `Meter`, and meter instruments only. The Application project receives **no OpenTelemetry package reference**: SDK/provider/exporter composition remains outside Application and is deferred to later authorized Infrastructure/Worker work. Existing centrally listed OpenTelemetry packages do not authorize a new reference, exporter, or hosting configuration.

`PipelineObservability.cs` is the single Application-owned internal instrumentation helper. It owns the fixed source and meter names `AIQuantTradingResearch.Pipeline`, constants for `pipeline.execute` and `pipeline.stage`, and the three WP02 instruments: `pipeline.operations` (`Counter<long>`, `{operation}`), `pipeline.failures` (`Counter<long>`, `{operation}`), and `pipeline.duration` (`Histogram<double>`, `ms`). It may expose only internal start/record helpers required by `PipelineExecutionUseCase`; it must not expose an Application-to-Infrastructure dependency or an exporter/SDK abstraction.

`PipelineExecutionUseCase.Execute(...)` owns the root `pipeline.execute` activity and records final execution outcome/duration. `MaterializeDatasetUseCase.Execute(DatasetDefinition)` owns the distinct child stage intervals `HistoricalObservationRetrieval` (exactly `IHistoricalObservationStore.Retrieve(...)`) and `DatasetMaterialization` (the subsequent successful observation filtering and existing snapshot construction); its explicit-observation overload owns only `DatasetMaterialization`. `PipelineExecutionUseCase.ExecuteCanonical(...)` owns child `pipeline.stage` activities for `SnapshotPersistence`, `CatalogRegistration`, and `StructuredResultEvidence`; each stage activity ends at its existing completion/failure point. No two activities may measure the same opaque interval under different stage names. The semantic pipeline order, input/output identities, exception propagation, result identity, and functional return values remain unchanged.

Every WP02 activity/metric uses only the WP01 subset `aiq.release=1.10`, `aiq.component=application`, `aiq.operation`, `aiq.stage` (stage observations only), `aiq.outcome`, `aiq.provenance` only when already available from canonical result evidence, `aiq.error_class` only as an existing finite `PipelineFailureCategory` name on failure, and `aiq.cancellation` when true. Correlation/run identities may flow through `Activity.Current`, never as metric tags. No symbols, paths, payloads, exception messages, stack traces, GUIDs, timestamps, credentials, or connection strings may be emitted.

`PipelineObservabilityTests.cs` is the sole dedicated WP02 test path. It covers source/name emission with a local listener, root/child topology, outcome/status/error-class mapping, operation/failure/duration instruments and units, listener-absent behavioral equivalence, attribute allowlist/cardinality, and exception propagation. It must not test provider/persistence mechanics, exporter composition, Worker lifecycle, System Health rendering, or cross-cutting release acceptance.

WP03 consumes the ambient parent activity/correlation through standard BCL `Activity.Current` while it performs its own Infrastructure-owned provider/persistence work. It may create child activities beneath `HistoricalObservationRetrieval` only for actual Infrastructure retrieval mechanics and beneath `SnapshotPersistence`/`CatalogRegistration` only for actual persistence mechanics; it may use its own Infrastructure meter. It must not duplicate a WP02 timing interval, rename/relocate WP02 sources, meters, or stages, alter business outputs, or change Application package ownership.

### WP03 — Infrastructure Provider, Persistence & Failure Instrumentation

- **Objective:** Observe existing provider/storage mechanics, latency, throughput, and bounded failure categories without changing their business behavior.
- **In scope:** Infrastructure adapters for accepted WP02 observations; provider-call and persistence timing/outcome; bounded failure mapping; telemetry-disabled and exporter-unavailable isolation.
- **Out of scope:** new providers, live connectivity, schema/migration, durable telemetry storage, retries/schedulers, or Application semantic redesign.
- **Direct dependencies:** WP02. **Selected model:** GPT-5.6 Terra.
- **Architecture/provenance:** Infrastructure owns mechanics; schema v4 and canonical persistence remain authoritative; provider observations identify simulated/replay/delayed/live provenance without fabricating it.
- **Owned paths:** `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationStore.cs` (`Retrieve(string target)` only) and `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteDatasetSnapshotStore.cs` (`Store(DatasetSnapshotCandidate)` and `Retrieve(DatasetSnapshotIdentity)` only); modify `tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs` and `tests/AIQuantTradingResearch.Infrastructure.Tests/SqliteDatasetTests.cs` for focused deterministic telemetry coverage. No new helper, project/package, schema, or migration path is authorized.
- **Acceptance:** provider/storage success, empty, failure, latency, and cancellation observations map deterministically; telemetry failure cannot alter canonical result; no SQLite/WAL/SHM residue.
- **Validation/security:** focused Infrastructure and full .NET regression, schema-v4 proof, offline fixtures, secret/redaction/cardinality scan, residue audit.
- **Completion boundary:** makes mechanics observable for WP04/WP05; does not create a telemetry backend.

#### WP03 deterministic infrastructure instrumentation contract

WP03 uses BCL `System.Diagnostics.ActivitySource`, `Activity`, `Meter`, and instruments only. It adds no package or project reference; SDK/provider/exporter composition remains WP04-owned. The single Infrastructure helper is an internal type in `SqliteHistoricalObservationStore.cs`, with static/shared source `AIQuantTradingResearch.Infrastructure` and meter of the same name. No additional helper file is authorized.

`SqliteHistoricalObservationStore.Retrieve(string target)` emits `provider.operation` around the actual `connectionFactory.OpenConnection()` plus command execution and row materialization, because that method owns the complete Infrastructure retrieval interval. When called by WP02, its activity is a child of ambient `HistoricalObservationRetrieval`; it never replaces or duplicates that Application activity. Invalid input is recorded as `failed` at the method boundary without inventing provider work; returned empty results are `empty`; successful non-empty results are `success`; mapped unavailable/invalid-data failures are `failed`; cancellation is `cancelled` only if an existing cancellation exception reaches this boundary. Existing result mapping and exception behavior remain unchanged.

`SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)` emits `persistence.operation` around the actual connection, transaction, read-existing/equivalence decision, inserts, and commit. `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)` emits `persistence.operation` around the actual connection, snapshot query, observation query, and mapping. Their ambient parent is the WP02 `SnapshotPersistence` activity when invoked from canonical execution; otherwise they use the ambient activity, or no parent when called independently. `SqliteDatasetCatalog` is `WP03 NOT AUTHORIZED` because `Register` and `Find` delegate to the snapshot store and a second activity would duplicate the same persistence interval. `SqliteHistoricalObservationStore.Persist` is `WP03 NOT AUTHORIZED` in this contract because WP03 scope requires retrieval/persistence mechanics already reached by the accepted WP02 canonical flow; it may be revisited only by a later narrow authority.

WP03 metrics are `provider.operations` (`Counter<long>`, `{operation}`), `provider.duration` (`Histogram<double>`, `ms`), `provider.failures` (`Counter<long>`, `{operation}`), `persistence.operations` (`Counter<long>`, `{operation}`), `persistence.duration` (`Histogram<double>`, `ms`), and `persistence.failures` (`Counter<long>`, `{operation}`). Activity names are exactly `provider.operation` and `persistence.operation`. Allowed attributes are the WP01 finite set: fixed `aiq.release=1.10`, `aiq.component=infrastructure`, finite `aiq.operation`, finite `aiq.outcome`, existing finite failure category, `aiq.cancellation=true` when applicable, and `aiq.schema_version=4` only where already available without new lookup. No symbol, SQL, path, payload, row count, timestamp, GUID, exception message, stack trace, credential, or connection string is emitted. Failure categories are only `invalid-data`, `unavailable`, `conflict`, and `cancelled`, mapped from existing result categories; original exceptions/results propagate unchanged.

Focused tests modify only `SqlitePersistenceTests.cs` and `SqliteDatasetTests.cs` to prove source/meter identity, truthful retrieval/store intervals, ambient WP02 parenting where the existing fixture can provide it, success/empty/failure mapping, metric names/units, bounded attributes, no-listener equivalence, exception propagation, and schema-v4/functional preservation. No dedicated test file or project/package mutation is authorized. WP04 inherits the source/meter names, activity names, metric names, finite attributes/failures, BCL-only decision, and ambient-parent rule without renaming or redesign.

### WP04 — Worker/Interop Lifecycle and Exporter Isolation

- **Objective:** Observe the Worker/process and .NET↔Python boundary while preserving lifecycle, cancellation, restart, and protocol safety.
- **In scope:** bounded process/boundary timing; correlation propagation; cancellation/restart outcomes; exporter isolation and bounded shutdown; stderr/diagnostic separation; safe degraded behavior.
- **Out of scope:** new IPC/RPC, replacement of JSON-over-stdio, Worker supervision of Streamlit, persistent service, arbitrary environment controls, or Python semantic changes.
- **Direct dependencies:** WP03. **Selected model:** GPT-5.6 Terra.
- **Architecture/provenance:** Worker owns process lifecycle; existing handoff remains canonical; Streamlit remains independent; no telemetry on protocol stdout; provenance remains explicit.
- **Owned paths:** existing Worker/interop symbols named by the WP04 authority; exact lifecycle tests only in its allowlist; no new generic bridge or package outside WP01 selection.
- **Acceptance:** normal, timeout, cancellation, restart, exporter failure, and shutdown paths are bounded and deterministic; owned processes/listeners terminate; protocol payload remains valid and backward-compatible.
- **Validation/security:** Worker/Infrastructure lifecycle suites, standalone and restart checks, architecture/no-bypass, redaction/secret scan, process/listener/residue matrix.
- **Completion boundary:** exposes boundary health to WP05; does not authorize Streamlit orchestration or live deployment.

#### WP04 reconciled implementation contract

WP04 selects **no external exporter** for the Release 1.10 baseline. OTLP, console, collector, hosted, vendor, and other exporter families are rejected or deferred; no exporter package, SDK provider, collector, endpoint, credential, or project-file change is authorized. Worker and interop observations use the already available BCL `System.Diagnostics.ActivitySource`, `Activity`, `Meter`, and instruments only. This is an in-process observation surface, not an exporter backend.

Exact production ownership: add `src/AIQuantTradingResearch.Worker/WorkerObservabilityLifecycle.cs` containing the internal `WorkerObservabilityLifecycle` disposable coordinator; modify only top-level composition in `src/AIQuantTradingResearch.Worker/Program.cs`; and modify only the existing bounded invocation method in `src/AIQuantTradingResearch.Infrastructure/PythonIntegration/PythonCapabilityInvoker.cs` to create the `interop.invoke` child observation. No Python source, Streamlit source, protocol stdout, or generic bridge is authorized.

The coordinator owns Worker source/meter `AIQuantTradingResearch.Worker`, activity `worker.lifecycle`, and `worker.lifecycle.events` (`Counter<long>`, `{event}`), with finite events `startup`, `ready`, `cancelled`, `restart`, `shutdown`, `disabled`, and `failed`. It initializes once per Worker process, is disposed idempotently, observes the existing `WorkerLifetimeCancellation` token without replacing it, and never recreates pipeline telemetry per operation. The Python invoker owns `interop.invoke` timing only around its real bounded invocation and preserves protocol stdout.

No exporter is enabled by default because none exists in this baseline. There is no WP04 exporter configuration key. No exporter initialization, export, destination, flush, or disposal operation can therefore block core execution. A later exporter requires a separate explicit contract. Absent/unavailable exporter conditions are bounded `disabled` or `degraded` evidence only and do not alter pipeline, persistence, JSON, Python, Streamlit, cancellation, or exit results; no retries are authorized.

Initialization occurs once after `host.Build()` and before selected Worker execution. Shutdown observes existing cancellation, records bounded terminal state, performs no force flush, and completes synchronously. Disposal never throws into core execution and creates no background task, listener registration, socket, file, or database resource. The coordinator does not supervise Streamlit.

Authorized tests: add `tests/AIQuantTradingResearch.Infrastructure.Tests/WorkerObservabilityLifecycleTests.cs` for exactly-once initialization, idempotent disposal, bounded events, disabled/no-exporter behavior, cancellation/restart/shutdown isolation, safe interop observation, protocol separation, bounded attributes, and zero residue; focused assertions only may be added to `PythonCapabilityInvokerTests.cs`. WP08 remains release-level validation and is not the WP04 implementation test surface.

WP05 receives only bounded lifecycle evidence through the existing governed observation/read-model chain. It must not inspect coordinator/exporter/process/invoker internals, infer health from missing telemetry, or supervise Worker. Canonical JSON/file handoff, schema v4, and Worker/Streamlit independence are unchanged.

### WP05 — System Health Read Model and Streamlit Presentation

- **Objective:** Present truthful bounded System Health from governed observations/read models through the existing Streamlit adapter.
- **In scope:** additive health projection; stage/provider/persistence/boundary health, latency, failure, provenance, stale/missing/unavailable states; deterministic rendering and safe refresh. `degraded` is not a WP05 state without an actual persisted optional-loss fact.
- **Out of scope:** direct SQLite/provider access, UI-side business logic, Streamlit Worker supervision, new framework, live-data claims, and schema redesign.
- **Direct dependencies:** WP04. **Selected model:** GPT-5.6 Terra.
- **Architecture/provenance:** Streamlit is an outer consumer; canonical .NET/read-model truth wins; simulated/replay data is labeled non-live; missing telemetry is not inferred as healthy.
- **Owned paths:** existing Streamlit entry point and additive presentation/read-model symbols only when named by WP05 authority; exact presentation tests only in its allowlist.
- **Acceptance:** Ready/WarmUp/Empty/Failed/Stale/Unavailable states render deterministically; unchanged revisions do not fabricate updates; health display cannot alter pipeline result or access persistence directly.
- **Validation/security:** focused Python/Streamlit tests, manual bounded local check, no-bypass architecture tests, provenance/security/link checks, listener/process residue audit.
- **Completion boundary:** provides the System Health surface to WP06/WP07; does not authorize deployment or production supervision.

#### WP05 reconciled implementation contract

WP05 retains the existing `VisualizationPresentationState` values (`Ready`, `WarmUp`, `Empty`, `Stale`, and `Failed`) as visualization lifecycle state. It does not redefine those values as infrastructure health. The .NET-owned `VisualizationReadModel` remains the source of truth and gains one additive nullable `SystemHealthSnapshot` property; no second file, channel, persistence table, schema migration, or exporter is introduced.

`SystemHealthSnapshot` is an immutable application read-model value with exactly `State`, `Provenance`, and `Reason` fields. The WP05 health state is the bounded serialized token `ready`, `warmup`, `empty`, `failed`, `stale`, or `unavailable`; `Provenance` is one of the already governed `historical`, `replay`, or `simulated` values for this release; `Reason` is nullable and contains only a finite sanitized category, never raw exception text. `ready`, `warmup`, `empty`, `failed`, and `stale` are mapped from the corresponding canonical visualization state. Current WP03/WP04 implementations expose no persisted optional-observation-loss fact, so `degraded` is excluded from the WP05 health vocabulary rather than inferred. `unavailable` is used when required health evidence is absent or cannot be safely read. No health state implies live provider, broker, trading, or exporter availability.

The existing `aiq-visualization-read-model-v1` JSON document is extended with optional `systemHealth: { state, provenance, reason }`. After WP05 producers are implemented, canonical producers emit it on every new model; pre-WP05 documents and documents without the property remain valid visualization documents, and consumers expose health as `unavailable` without rejecting the otherwise valid visualization. Unknown properties remain ignored by the existing parser. A malformed present `systemHealth` object is a health-integrity error for the health projection while the existing envelope integrity rules remain authoritative; it must not be guessed from unrelated fields.

No independent health timestamp, age field, freshness threshold, or wall-clock comparison is authorized. `stale` is emitted only from the existing canonical visualization `Stale` state and its existing structural reason. The health projection does not create or alter revisions, windows, features, pipeline outcomes, or provenance. Existing source-time values remain data timestamps, not health timestamps.

Exact WP05 production ownership is limited to: `src/AIQuantTradingResearch.Application/Visualization/VisualizationReadModelContracts.cs` (`SystemHealthState`, `SystemHealthSnapshot`, and `VisualizationReadModel.SystemHealth`); `src/AIQuantTradingResearch.Application/Visualization/VisualizationReadModelUseCase.cs` (health composition at the existing historical/replay publication boundary only); `src/AIQuantTradingResearch.Infrastructure/Visualization/VisualizationReadModelFilePublisher.cs` (additive `systemHealth` serialization only); `python/presentation/visualization_read_model.py` (optional health parsing); and `python/presentation/realtime_financial_visualization.py` (`VisualizationFrame` health projection and presentation-only mapping). No other production path is owned.

Exact WP05 test ownership is limited to existing `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelStoreTests.cs`, `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelFilePublisherTests.cs`, `python/presentation/test_visualization_read_model.py`, and `python/presentation/test_realtime_financial_visualization.py`; no new framework or test project is authorized. WP06 permanently rechecks the health shape, optional-v1 compatibility, finite tokens/reasons, provenance, no-bypass rules, schema v4, and residue.

#### WP05 V2 semantic and presentation completion

The authoritative .NET source is the existing `VisualizationReadModelUseCase` publication result and its `VisualizationReadModel.State`, `SourceMode`, `SourceAuthority`, `Pipeline`, `Failure`, and `StaleReason` values. WP03/WP04 activity and lifecycle facts are not independently inspected by Python/Streamlit and are not persisted health facts. `SystemHealthSnapshot` is composed at the same publication boundary and contains `State`, `Provenance`, and nullable `Reason`; no timestamp or age field exists.

The health state evaluation is total and mutually exclusive, in this precedence order: (1) an unreadable required health source or a pre-WP05 payload without health at the consumer is `unavailable`; (2) visualization `Failed` is `failed`; (3) visualization `Stale` is `stale`; (4) `WarmUp` is `warmup`; (5) `Empty` is `empty`; (6) `Ready` is `ready`. The first applicable row wins. No health state changes visualization state.

The exhaustive reason tokens are: `null` for `ready`, `warmup`, and `empty`; `pipeline-failed` for `failed`; `structural-staleness` for `stale`; and `required-health-evidence-unavailable` for `unavailable`. No other reason string is valid. Reasons are never shown as raw machine tokens; presentation uses the fixed text table below.

The canonical v1 extension is exactly `systemHealth: { "state": <lowercase token>, "provenance": <historical|replay|simulated>, "reason": <string|null> }`. It is emitted by every WP05-created model and is optional for older v1 documents. No independent timestamp/freshness comparison is performed; `stale` is only the existing visualization `Stale` state and retains its structural reason internally.

Streamlit ownership is `python/presentation/realtime_financial_visualization.py`, function `render_visualization_frame`. Immediately after the existing target/state subheader and before chart/latest/feature rendering, it renders an always-visible `System Health` subheader and one `st.info`, `st.warning`, or `st.error` message from the fixed table. The visualization state subheader and all existing sections remain unchanged and follow the health message. No controls or process inspection are added.

| Health | Reason | Component | Exact text |
|---|---|---|---|
| ready | null | `st.info` | `System Health: Canonical evidence available.` |
| warmup | null | `st.info` | `System Health: Waiting for bounded canonical observations.` |
| empty | null | `st.info` | `System Health: Canonical pipeline completed with no observations.` |
| failed | `pipeline-failed` | `st.error` | `System Health: Canonical pipeline failed.` |
| stale | `structural-staleness` | `st.warning` | `System Health: Canonical visualization evidence is structurally stale.` |
| unavailable | `required-health-evidence-unavailable` | `st.warning` | `System Health: Health evidence is unavailable; visualization data may still be available.` |

For a valid pre-WP05 v1 envelope with absent `systemHealth`, the parser returns the normal envelope plus health `unavailable` with `required-health-evidence-unavailable`; the base visualization remains usable. A present non-object, missing required field, unknown state/provenance/reason, invalid type, or malformed health value returns `ReadModelError("HealthIntegrity")`; `ReadModelCache` retains the last valid envelope, and the page displays the exact unavailable text above when a prior envelope exists. A malformed entire document follows existing `ReadIntegrity` behavior and retains the last valid envelope; with no prior envelope, the existing producer-unavailable message remains the transport message and the page does not claim health readiness. Unknown top-level properties remain ignored.

| .NET source condition | Health | Reason | Serialized health | Python/frame | Streamlit assertion |
|---|---|---|---|---|---|
| canonical `State=Ready`, historical authority | ready | null | `{state:ready,provenance:historical,reason:null}` | state=`ready`, provenance=`historical` | `System Health: Canonical evidence available.` via `st.info` |
| canonical `State=WarmUp`, historical authority | warmup | null | `{state:warmup,provenance:historical,reason:null}` | state=`warmup` | `System Health: Waiting for bounded canonical observations.` |
| canonical `State=Empty`, historical authority | empty | null | `{state:empty,provenance:historical,reason:null}` | state=`empty` | `System Health: Canonical pipeline completed with no observations.` |
| canonical `State=Failed` | failed | pipeline-failed | `{state:failed,provenance:<source>,reason:pipeline-failed}` | state=`failed` | `System Health: Canonical pipeline failed.` via `st.error` |
| canonical `State=Stale` | stale | structural-staleness | `{state:stale,provenance:<source>,reason:structural-staleness}` | state=`stale` | structural-stale warning; no clock calculation |
| absent/unreadable required health evidence | unavailable | required-health-evidence-unavailable | `{state:unavailable,provenance:<source>,reason:required-health-evidence-unavailable}` or absent for pre-WP05 | state=`unavailable` | health-unavailable warning; base visualization preserved |

The exact test assertions are: `VisualizationReadModelStoreTests.cs` proves each .NET state/provenance/reason and independence from visualization state; `VisualizationReadModelFilePublisherTests.cs` proves the nested property names, lowercase tokens, null reason, and v1 atomic publication; `test_visualization_read_model.py` proves optional absence, valid shape, unknown-token/type rejection, and last-good retention; `test_realtime_financial_visualization.py` proves frame fields and the fixed section/text mapping without I/O. WP06 adds permanent no-bypass, finite-token, schema-v4, provenance, and residue assertions.

### WP06 — Permanent Observability and No-Bypass Tests

- **Objective:** Permanently prove the observability contract, architecture boundaries, compatibility, failure behavior, and cleanup.
- **In scope:** deterministic layer-appropriate tests for semantic observations, exporter/degraded behavior, System Health states, schema/protocol compatibility, lifecycle/restart/cancellation, security and residue.
- **Out of scope:** new production capability, test-framework/package introduction, live providers, broad UI automation, or duplicating unrelated predecessor tests.
- **Direct dependencies:** WP05. **Selected model:** GPT-5.6 Terra.
- **Architecture/provenance:** tests may use owned fakes/fixtures and harnesses, never bypass canonical contracts; evidence distinguishes simulated/replay from live.
- **Owned paths:** dedicated WP06 test paths only when exact paths are named by the WP06 authority; existing architecture/security test surfaces may be amended only by symbol-level authority.
- **Acceptance:** all release gates have deterministic executable proof; no Domain/provider/UI/SQLite bypass; repeated runs are stable; no owned residue remains.
- **Validation/security:** focused .NET/Python/Streamlit and architecture suites, full regressions, build, Gitleaks/secret scan, dependency health, schema/protocol checks, process/listener/database/temp-file audit.
- **Completion boundary:** establishes permanent proof; does not alter production semantics merely to satisfy a test.

### WP07 — Documentation, Developer Setup & Operational Runbook

- **Objective:** Align architecture, setup, provenance, telemetry configuration, troubleshooting, and operational guidance with delivered Release 1.10 truth.
- **In scope:** exact setup/configuration commands; simulated/replay disclosure; health-state interpretation; redaction and failure guidance; local bounded runbook; links and branch/PR workflow.
- **Out of scope:** source/test/package/schema changes, new tools, live-provider claims, cloud deployment, or undocumented operational promises.
- **Direct dependencies:** WP06. **Selected model:** GPT-5.6 Terra.
- **Architecture/provenance:** docs must state .NET→canonical JSON→Python/Streamlit ownership and must not imply UI/provider/SQLite bypass or live telemetry.
- **Owned paths:** the complete literal EXISTING allowlist is `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md` for architecture and boundary truth; `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md` for developer setup, bounded local operational runbook, troubleshooting, validation commands, and WP08 handoff; and `docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md` for local security/signing hygiene and environment-only remediation. No NEW WP07 path is authorized. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`, `README.md`, Release 1.9 documents, and every other Markdown file are read-only evidence or forbidden. The absence of a `docs/operations` tree is intentional; no operations directory may be created.
- **Acceptance:** commands and paths resolve; configuration defaults are safe; all health/provenance/security claims are truthful; links, formatting, secret scan, and stale-reference checks pass.
- **Validation/security:** Markdown/link/command checks, docs diff review, Gitleaks, local-only configuration exclusion, no executable test delta unless separately authorized.
- **Completion boundary:** documents current behavior and reproducibility; does not authorize implementation or publication.

#### WP07 documentation path and content ownership reconciliation

The three files above are all present before WP07 implementation and are the only writable WP07 paths. Ownership is deterministic: `DOTNET_PYTHON_INTEROPERABILITY.md` owns the .NET pipeline → canonical JSON → Python/Streamlit boundary, Worker/Streamlit independence, schema-v4 and no-bypass architecture statements; `PYTHON_DEVELOPER_ENVIRONMENT.md` owns Python 3.13.15/.venv setup, exact governed pins, bounded local run/validation commands, System Health interpretation, simulated/replay non-live disclosure, troubleshooting, residue expectations, and the WP08 handoff; `WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md` owns only local development signing/App Control facts, secret hygiene, and environment-remediation cross-reference. WP07 may add concise links between these files, but may not move ownership or create a second authority.

The WP01 OpenTelemetry selection record, WP02–WP06 implementation/test artifacts, Release 1.9 documentation, root README, and all other Markdown are read-only predecessor evidence. No WP07 text may claim live providers, trading, cloud deployment, an external exporter, ML/backtesting, a parallel pipeline, direct SQLite/provider access, or Streamlit Worker supervision. Permanent-test ownership remains WP06; WP08 owns release-level acceptance and residue evidence. WP07 only references the exact WP06 paths and commands already frozen in this plan.

#### WP07 documentation materialization simulation

Terra's file-by-file action is fully determined: inspect and modify only the named sections of the three existing files; add no file; use no source, test, project, package, schema, signing-configuration, or runtime path; and leave all read-only evidence unchanged. Validation is limited to Markdown/link/command resolution, factual stale-reference review, Gitleaks, local-only configuration exclusion, and diff/path ownership. This simulation leaves Terra no choice of path, owner, helper, test delta, dependency, or runtime behavior.

### WP08 — Full Validation, Acceptance & PR Readiness

- **Objective:** Independently validate the complete Release 1.10 candidate and prepare an exact reviewable payload.
- **In scope:** fresh release-level matrix; full regressions; architecture/security/provenance/docs/residue audits; exact manifest; dedicated branch and PR readiness under later Git authority.
- **Out of scope:** unapproved fixes, new scope/dependencies, automatic merge, milestone closure, tag/Release publication, or starting a later release.
- **Direct dependencies:** WP07. **Selected model:** GPT-5.6 Terra.
- **Architecture/provenance:** acceptance consumes only governed canonical outputs and preserves all prior Release 1.9 boundaries; no live-provider or ML claim.
- **Owned paths:** release acceptance evidence and exact WP08 manifest only under later authority; no implementation files by default.
- **Acceptance:** every release matrix row passes; exact intended paths are isolated; security and all residue gates are clean; candidate is review-ready without bypass.
- **Validation/security:** build, full .NET/Python/Streamlit, focused WPs, schema/protocol/no-bypass, lifecycle, security/Gitleaks, docs, dependency, and complete cleanup audits.
- **Completion boundary:** may request a separate PR/review/merge authority; it does not merge, close #59, tag, or publish.

#### Reconciled publication base and candidate handoff

Remote compare evidence proves that frozen base 35ec644576275570aee522872c770e6c06e7879d is an ancestor of authoritative remote main at 5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33, with two intervening governance-only commits (937a696f10ab4aafe55320f4ebd625635b99bf7b and 5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33) changing only Release 1.9 prompt documentation. No Release 1.10 implementation path overlaps or conflicts.

Terra must materialize from current remote main at 5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33; the candidate commit parent must be that SHA. Terra may not publish a stale-parent commit and may not rebase, merge, cherry-pick, reset, or overwrite independent work. The deterministic procedure is to preserve the current working-tree candidate, create the approved publication branch from the reconciled remote base, carry only the literal candidate list in the manifest, and stop on any conflict or missing path. The two Git-publication authority prompt files are IN; the two remote-base reconciliation prompt files, the two later Terra publication-resumption prompt files, and the two current repair-authority prompt files are OUT execution-control inputs. The canonical candidate is therefore 103 paths: 21 tracked and 82 untracked, including 70 publication-authority prompt artifacts. The initial raw inventory was 107 paths (21 tracked, 86 untracked); after creation of this repair authority pair, the observed raw inventory is 109 paths (21 tracked, 88 untracked), reconciled as 103 candidate paths plus six excluded execution-control paths.

Because the base moved across governance commits and the candidate is re-anchored, Terra must rerun focused WP06 permanent suites, full .NET, full Python, build, Streamlit/pip, Gitleaks, docs/diff, schema/package, and residue gates after re-anchoring. WP08 acceptance remains semantic evidence, but validation freshness must be re-established on the reconciled base. The existing Terra publication authority's 35ec... and 101-path literals are superseded only by this reconciled base/103-path boundary; every other safety constraint remains binding.

## Cross-WP responsibility matrix

| Concern | Primary owner | Validator |
|---|---|---|
| Selection/vocabulary | WP01 | WP06, WP08 |
| Application observation contract | WP02 | WP06 |
| Stage/provider/persistence instrumentation | WP03 | WP06, WP08 |
| Worker/interop lifecycle and exporter isolation | WP04 | WP06, WP08 |
| Health/read-model projection and Streamlit | WP05 | WP06, WP08 |
| Failure/degraded semantics | WP02/WP03/WP04 | WP05, WP06 |
| Lifecycle/restart/residue | WP04 | WP06, WP08 |
| Architecture/no-bypass | WP06 | WP08 |
| Security/redaction/dependency | WP01/WP04 | WP06, WP08 |
| Documentation/setup/runbook | WP07 | WP08 |
| Full integration and release acceptance | WP08 | Human review/merge authority |

#### WP06 permanent test-path and invariant ownership reconciliation

WP06 adds no production behavior, project/package reference, schema, migration, runtime configuration, or helper/fixture path. Its dedicated permanent test files are exactly:

1. `tests/AIQuantTradingResearch.Application.Tests/Release110ObservabilityPermanentTests.cs`, class `Release110ObservabilityPermanentTests`: BCL `ActivityListener`/`MeterListener` behavioral proof for the WP02 root/stage topology, exact stage set/order, retrieval/materialization non-overlap, bounded tags, and no-listener functional equivalence; also exact finite WP05 health state/reason/provenance and v1 compatibility assertions at the canonical Application contract boundary.
2. `tests/AIQuantTradingResearch.Infrastructure.Tests/Release110ObservabilityPermanentTests.cs`, class `Release110ObservabilityPermanentTests`: scoped listener proof for WP03 provider/persistence ownership, activity/meter identity, finite failure categories and bounded dimensions, ambient WP02→WP03 parent topology, and WP04 Worker/no-exporter/canonical-handoff isolation. It may invoke only the three WP03-owned methods and the already-governed Worker/interop symbols; it must not instrument Catalog or `Persist(...)`.
3. `tests/AIQuantTradingResearch.Architecture.Tests/Release110ObservabilityNoBypassTests.cs`, class `Release110ObservabilityNoBypassTests`: symbol/reference and project-graph assertions for Domain telemetry absence, Application BCL-only ownership, Infrastructure/Worker ownership, no exporter package/configuration, schema v4, canonical v1 handoff, Release 1.8 separation, Streamlit independence, and forbidden direct SQLite/provider/process/listener access. Assertions inspect resolved source/project/reference structure, never comments or formatting.
4. `python/presentation/test_release_1_10_observability_no_bypass.py`, module `Release110NoBypassTests`: AST/import and bounded behavioral assertions for canonical-handoff-only parsing, exact WP05 health vocabulary/reasons, malformed/absent health behavior, no SQLite/provider/process/listener imports or calls, no second health channel, and deterministic presentation mapping. It does not launch Streamlit or any network service.

`WP06 NEW TEST HELPERS/FIXTURES: NONE.` Existing `PipelineObservabilityTests.cs`, `SqlitePersistenceTests.cs`, `SqliteDatasetTests.cs`, `WorkerObservabilityLifecycleTests.cs`, `PythonCapabilityInvokerTests.cs`, `VisualizationReadModelStoreTests.cs`, `VisualizationReadModelFilePublisherTests.cs`, `test_visualization_read_model.py`, and `test_realtime_financial_visualization.py` remain predecessor-owned and are consumed read-only; WP06 does not duplicate or edit their existing primary assertions.

The complete relevant existing-test corroboration inventory is: `tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs`; `tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs`; `tests/AIQuantTradingResearch.Infrastructure.Tests/SqliteDatasetTests.cs`; `tests/AIQuantTradingResearch.Infrastructure.Tests/WorkerObservabilityLifecycleTests.cs`; `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonCapabilityInvokerTests.cs`; `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelStoreTests.cs`; `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelFilePublisherTests.cs`; `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationPermanentIntegrationTests.cs`; `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationSemanticExposureTests.cs`; `tests/AIQuantTradingResearch.Architecture.Tests/VisualizationBoundaryRulesTests.cs`; `python/presentation/test_visualization_read_model.py`; `python/presentation/test_realtime_financial_visualization.py`; `python/presentation/test_realtime_financial_visualization_wp06.py`; `python/presentation/test_realtime_financial_visualization_wp07.py`; and `python/presentation/test_visualization_semantic_exposure_wp07.py`. These are all read-only predecessor evidence; no existing file is an additional WP06 mutation target.

| Invariant ID | Primary path/class/symbol | Layer/technique |
|---|---|---|
| W06-APP-01 root `pipeline.execute`; W06-APP-02 five-stage order | `Application.Tests/Release110ObservabilityPermanentTests.cs` / `Release110ObservabilityPermanentTests.RootAndStagesHaveExactOrder` | behavioral `ActivityListener` |
| W06-APP-03 retrieval only `IHistoricalObservationStore.Retrieve`; W06-APP-04 materialization starts afterward; W06-APP-05 no duplicate opaque interval | same / `RetrievalAndMaterializationIntervalsDoNotOverlap` | fake store plus activity timing/order |
| W06-APP-06 bounded Application tags and no-listener equivalence | same / `ApplicationObservationsAreBoundedAndNonAuthoritative` | listener tag allowlist plus fixed-result comparison |
| W06-INF-01 three exact WP03 owners; W06-INF-02 source/meter/activity identity | `Infrastructure.Tests/Release110ObservabilityPermanentTests.cs` / `InfrastructureOwnersEmitExactObservations` | scoped `ActivityListener`/`MeterListener` |
| W06-INF-03 finite failure categories/dimensions; W06-INF-04 WP02 parent topology | same / `InfrastructureFailureAndParentContractsAreBounded` | behavioral listener and ambient parent assertion |
| W06-INF-05 Catalog and `Persist(...)` non-ownership | `Architecture.Tests/Release110ObservabilityNoBypassTests.cs` / `ForbiddenPersistenceSymbolsRemainUninstrumented` | resolved symbol/source inspection |
| W06-WORKER-01 lifecycle ownership; W06-WORKER-02 no exporter/package/config | Infrastructure dedicated file / `WorkerLifecycleIsBoundedAndExporterFree`; Architecture dedicated file / `WorkerHasNoExporterDependency` | behavior plus project/reference inspection |
| W06-WORKER-03 canonical handoff and Streamlit independence | Architecture dedicated file / `CanonicalHandoffAndIndependentPresentationArePreserved` | source/import graph |
| W06-HEALTH-01 exact six states and finite reasons; W06-HEALTH-02 precedence/provenance | Application dedicated file / `HealthVocabularyAndPrecedenceAreClosed` | behavioral contract assertions |
| W06-HEALTH-03 nested v1 shape; W06-HEALTH-04 absent/malformed compatibility; W06-HEALTH-05 no clock/degraded | Application dedicated file / `HealthV1CompatibilityAndNoIndependentFreshness` | serialized shape and negative assertions |
| W06-PY-01 canonical parser-only health; W06-PY-02 no bypass/second channel | `python/presentation/test_release_1_10_observability_no_bypass.py` / `test_import_and_ast_boundaries` | AST/import inspection |
| W06-PY-03 exact presentation text and malformed-safe behavior | same / `test_health_projection_and_presentation_mapping` | deterministic behavioral projection |
| W06-SEC-01 no secrets/raw exceptions/high-cardinality values | Architecture dedicated file / `TelemetrySourcesContainNoSensitiveDimensions`; Python dedicated file / `test_no_sensitive_or_unbounded_telemetry_inputs` | bounded source/AST assertions; Gitleaks remains the secret scanner |
| W06-SEC-02 no exporter dependency/configuration | Architecture dedicated file / `NoExporterPackageOrConfigurationIsIntroduced` | project/reference inspection |
| W06-SCHEMA-01 schema v4 and v1 handoff; W06-REL18-01 separation | Architecture dedicated file / `SchemaAndReleaseBoundaryRemainStable` | source/project assertions |

The assertion-strength rule is behavioral for runtime observability and health, serialization for v1 compatibility, AST/import or resolved-project inspection for no-bypass/ownership, and Gitleaks/package inspection for secrets/dependencies. No comment matching, formatting matching, network, live provider, or generic helper is authorized.

#### WP06 validation command matrix and WP07 handoff

- Focused .NET: `dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-restore --filter FullyQualifiedName~Release110ObservabilityPermanentTests`; equivalent Infrastructure and Architecture filters using their project paths.
- Full .NET: the four test projects from the repository solution, with actual totals reported by Terra; Domain remains a regression gate.
- Python: run the five governed presentation test modules from `python/presentation`; WP06 adds `test_release_1_10_observability_no_bypass.py` and reports the actual total.
- Build: `dotnet build AIQuantTradingResearch.slnx --no-restore`.
- Dependency: `.venv\Scripts\python.exe -m pip check`; Streamlit version must remain 1.61.1.
- Security: `gitleaks git . --redact --verbose` using Gitleaks 8.30.1; this does not replace cardinality tests.
- Residue: read-only owned process/listener/temp-handoff/database audit after each focused and full run.

WP07 must reference the four dedicated WP06 paths above, the predecessor-owned corroborating files, these exact commands, schema v4, canonical v1/systemHealth, no-exporter policy, and the explicit simulated/replay non-live disclosure. WP07 owns documentation only; WP08 owns release-level acceptance.

## Impact ownership and acceptance coverage

SQLite schema v4 and persistence are owned as **no-change** by WP03/WP06 validation; JSON/read-model additions, if proven necessary, are owned by WP02/WP05 and require additive versioning; JSON-over-stdio and file handoff remain no-change canonical boundaries owned/observed by WP04/WP05; .NET/Python dependency decisions belong to WP01 and must not be invented by Terra; Streamlit version remains unchanged and is validated by WP05/WP06; configuration is WP01/WP04 decision-controlled; persisted business state remains unchanged. Domain correctness, compatibility, provenance, security, lifecycle, residue, and documentation each have an owner in the matrices above. No release requirement is orphaned.
