# Release 1.10 OpenTelemetry Selection Record

## Decision status and model assignment

**Release:** 1.10 — OpenTelemetry & Pipeline Observability
**Work package:** WP01 — Observability Selection, Vocabulary & Scope
**Decision status:** Selected family; implementation package versions remain a later Terra resolution.
**Selected execution model:** GPT-5.6 Luna.

- **GPT-5.6 Luna** owns this contract, architecture, vocabulary, dependency policy, acceptance, and reconciliation.
- **GPT-5.6 Terra** is reserved for later implementation, package resolution/install, tests, validation, and approved Git/GitHub execution.
- **GPT-5.6 Sol** is reserved for supporting alternatives, synthesis, and non-authoritative review.

This record is planning authority only. It installs no package, changes no project file, enables no exporter, and does not authorize instrumentation.

## Selected technology boundary

Select the **OpenTelemetry API and SDK family**, consumed through technology-neutral Application contracts and concrete adapters at the existing Infrastructure/Worker/Streamlit ownership boundaries.

The minimum later implementation surface is:

1. an API surface for creating activities/spans, meters, instruments, and bounded attributes;
2. an SDK/provider surface for in-process recording and safe disabled/degraded behavior;
3. no exporter or telemetry backend for the planning baseline;
4. no hosting, collector, cloud, vendor, or deployment dependency;
5. no test-framework dependency. Tests use the repository's existing governed test surfaces and fakes.

The selected family is an observation mechanism, not a business-data source, persistence store, provider client, scheduler, RPC mechanism, or Streamlit control channel. Application contracts must not reference exporter APIs or concrete vendor packages. Domain remains telemetry-free.

## Alternatives and trade-offs

| Alternative | Decision | Rationale / trade-off |
|---|---|---|
| OpenTelemetry API + SDK family | **SELECTED** | Open standard, supports traces and metrics, permits in-process disabled/degraded operation, and keeps Application semantics separable from exporters. Concrete package versions still require fresh official compatibility/security evidence. |
| .NET/Python vendor-specific telemetry SDK | Rejected | Creates vendor coupling before a backend is accepted and would make the cross-process boundary less portable. |
| Custom logging-only convention | Rejected as the Release 1.10 selection | Existing `ILogger` remains authoritative for logs, but logging alone does not provide the bounded activity/metric vocabulary required by the accepted scope. |
| Direct exporter/backend integration | Deferred/rejected for this release baseline | No collector, hosted backend, credentials, endpoint, or operational requirement is accepted. Selecting one now would enlarge scope and dependency risk. |
| Custom telemetry bus or parallel pipeline | Rejected | Violates the existing five-stage pipeline, JSON handoff, Worker/Streamlit independence, and no-bypass architecture. |

## Version, package, license, and security policy

Exact package identities and versions are **not fixed by WP01**. A later implementation authority must use current official upstream package metadata and compatibility evidence for the repository's target frameworks and governed Python 3.13/.venv environment, select one mutually compatible set, record exact versions before installation, and run license, vulnerability, and transitive-dependency review.

No package version may be inferred from a common convention, installed globally, added to a project file, added to `requirements.txt`, or introduced transitively without that later authority. Package resolution must be reproducible and limited to the selected API/SDK components. An exporter, collector, backend, hosting extension, or vendor adapter requires a separate explicit selection decision; it is not implied by this record.

Reconsideration is required if official compatibility evidence cannot support the repository target, if the API/SDK requires an exporter for safe local operation, if a package introduces a prohibited runtime dependency, if security/license review fails, or if instrumentation would alter deterministic business results, schema-v4 persistence, protocol output, or shutdown bounds.

## Canonical vocabulary

| Term | Canonical meaning and owner | Allowed use / prohibited ambiguity |
|---|---|---|
| Trace | A bounded logical observation context for one execution or validation flow; lifecycle owner is the boundary that starts it. | May correlate owned activities; must not claim distributed propagation where none exists. |
| Activity/span | One bounded operation observation with parent, timing, outcome, and allowlisted attributes; Application names semantic operations and concrete adapters supply telemetry mechanics. | `span` and `.NET Activity` are equivalent only as observation terms; neither is business state. |
| Metric | An aggregate numeric observation emitted by a bounded instrument. | No raw symbols, request IDs, paths, timestamps, payloads, or unbounded dimensions. |
| Log | Existing structured `ILogger` event describing an operational fact or failure. | Remains distinct from a metric or span; no duplicate uncontrolled emission. |
| Pipeline operation | One invocation of the existing canonical five-stage Application pipeline. | Must not mean a second pipeline, scheduler, or provider call sequence. |
| Pipeline stage | One existing ordered stage owned by the Application pipeline contract. | Stage names are finite and stable; no dynamically generated stage names. |
| Boundary | An existing ownership transition observed without changing its contract. | Application, Infrastructure, Worker/interop, persistence, handoff, and presentation boundaries only. |
| Provider operation | An Infrastructure-owned interaction with an already accepted provider abstraction or deterministic fixture. | Does not prove live connectivity or trading. |
| Persistence operation | An Infrastructure-owned read/write against canonical schema-v4 persistence. | Telemetry is not persisted business state and does not authorize schema migration. |
| Handoff/publication operation | Publication or consumption of the existing governed read model/JSON handoff. | Does not authorize direct SQLite access, protocol redesign, or UI reconstruction. |
| Worker lifecycle | Startup, readiness, execution, cancellation, restart, and shutdown observations owned by Worker/process infrastructure. | Does not make Worker a Streamlit supervisor. |
| Interop operation | One bounded .NET-to-Python boundary invocation or lifecycle observation. | Existing versioned JSON-over-stdio remains canonical; no generic RPC claim. |
| Health state | A finite, evidence-derived presentation state. | Never shorthand for live, connected, profitable, secure, or trading-capable. |
| Failure/error | A bounded classified unsuccessful outcome with safe diagnostic metadata. | Raw exception text and secrets are not telemetry attributes. |
| Duration/latency | Monotonic elapsed time for one bounded operation, represented in milliseconds where recorded. | Never a metric dimension and never a business timestamp. |
| Success/failure outcome | The operation's factual terminal outcome: `success`, `empty`, `failed`, `cancelled`, `degraded`, or `unavailable` where applicable. | Must not be upgraded from missing telemetry or simulated data to live health. |

## Scope and naming contract

Later instrumentation may observe, at the existing owner only: Application pipeline entry/exit and stages; Infrastructure provider and persistence boundaries; canonical read-model publication and JSON/file handoff; Worker startup/readiness/cancellation/restart/shutdown; bounded interop lifecycle; and the inputs consumed by the WP05 System Health projection. Each observation is additive and must preserve stage order, result identity, replay semantics, schema v4, and protocol stdout discipline.

Canonical source names are stable, lowercase, dot-separated identifiers with no provider/vendor name and no dynamic suffix:

- Activity source: `AIQuantTradingResearch.Pipeline` for Application semantics and `AIQuantTradingResearch.Infrastructure` for mechanics.
- Activity names: `pipeline.execute`, `pipeline.stage`, `provider.operation`, `persistence.operation`, `read_model.publish`, `handoff.consume`, `worker.lifecycle`, and `interop.invoke`.
- Meter names: `AIQuantTradingResearch.Pipeline` and `AIQuantTradingResearch.Infrastructure`.
- Metric names: `pipeline.operations`, `pipeline.duration`, `pipeline.failures`, `provider.operations`, `persistence.operations`, `handoff.operations`, and `worker.lifecycle.events`.
- Units: count instruments use `{operation}` or `{event}`; durations use `ms`.
- Outcomes use the finite lowercase values `success`, `empty`, `failed`, `cancelled`, `degraded`, and `unavailable`.

Names are semantic and may be mapped to OpenTelemetry conventions later where an exact standard applies; this record does not claim unsupported semantic-convention compliance.

## Attribute and cardinality contract

Only these attributes may be emitted after later implementation review:

| Attribute | Type / bounded values | Cardinality | Trace | Metric | Health |
|---|---|---|---:|---:|---:|
| `aiq.release` | string, fixed `1.10` | STATIC | yes | yes | no |
| `aiq.component` | string, finite `application`, `infrastructure`, `worker`, `interop`, `presentation` | LOW | yes | yes | yes |
| `aiq.operation` | string, finite names from this record | BOUNDED | yes | yes | yes |
| `aiq.stage` | string, finite existing stage names | BOUNDED | yes | yes | yes |
| `aiq.outcome` | finite outcome domain above | BOUNDED | yes | yes | yes |
| `aiq.provenance` | finite `historical`, `replay`, `simulated`, `delayed-provider`, `real-time-provider`, `live-broker` | BOUNDED | yes | yes | yes |
| `aiq.health_state` | finite `ready`, `warmup`, `empty`, `failed`, `stale`, `degraded`, `unavailable` | BOUNDED | yes | yes | yes |
| `aiq.schema_version` | fixed bounded string, currently `4` | STATIC | yes | yes | yes |
| `aiq.protocol_version` | fixed bounded string, currently `1` where applicable | STATIC | yes | yes | yes |
| `aiq.error_class` | finite safe classification, never raw text | BOUNDED | yes | yes | yes |
| `aiq.cancellation` | boolean | LOW | yes | yes | yes |

Correlation/run identity may be present on traces and logs only when generated by an existing boundary and must not be a metric dimension or health grouping. File paths, database paths, symbols/tickers, GUIDs, timestamps, exception messages, stack traces, arbitrary payloads, credentials, tokens, connection strings, environment values, and raw provider data are `PROHIBITED-HIGH` or prohibited. No user/provider identifier may become an unbounded dynamic name.

## Metrics and traces

Required metrics are limited to operation count, failure count, bounded duration, lifecycle-event count, and publication/handoff outcome count at the owners above. No financial or market metric is introduced. A metric records observation only; it cannot change the canonical result.

The Application operation is the root semantic activity when a trace exists. Existing stage activities are children. Provider and persistence activities are children of the stage that invokes them. Publication/handoff and Worker/interop activities follow the existing ownership boundary. Cross-process context propagation is not selected; the Worker/Streamlit relationship remains independently observable through governed handoff/revision evidence. Activity status reflects factual outcome; exceptions are recorded only as safe bounded classifications, never unrestricted exception payloads.

Existing `ILogger` behavior remains. Logs may be correlated with a trace/activity identity when safe, but log export is not selected and duplicate log-plus-metric emission is not required. Operational failures require the existing log plus an appropriate bounded outcome; metrics and activities are additive evidence.

## System Health semantics

WP05 may project only evidence from the governed observation/read-model chain:

- `ready`: required canonical evidence is present, current, and successful; it proves neither live connectivity nor trading capability.
- `warmup`: lifecycle or observation evidence is incomplete but bounded startup is progressing; it proves neither failure nor readiness.
- `empty`: the canonical operation completed with no data; it is not a provider-health failure.
- `failed`: a required canonical operation completed unsuccessfully with a classified failure.
- `stale`: the last evidence is older than the governed freshness rule; it is not proof of current failure or current health.
- `degraded`: the business result remains available but an optional observation/export path is unavailable or incomplete.
- `unavailable`: required evidence is absent or cannot be safely read; absence is not success.

Historical, replay, and simulated provenance must remain visible where applicable. `real-time-provider` and `live-broker` are vocabulary values only; Release 1.10 does not authorize those sources. No health state implies broker/exchange connectivity, trading success, or distributed tracing.

## Exporter isolation and failure policy

No external exporter is required for Release 1.10 acceptance. Development console/debug export may be considered only by a later authority with safe opt-in defaults; it is not selected here. OTLP, collector, hosted backend, credentials, endpoints, and vendor exporters are deferred and require a new explicit decision.

If no approved exporter is configured, in-process observations are disabled or safely degraded without changing the pipeline result. If an approved later exporter fails, the failure is logged/classified and bounded; it cannot block or alter persistence, canonical JSON publication, protocol stdout, Streamlit consumption, cancellation, or shutdown. Exporter creation/disposal belongs to WP04, and Streamlit never owns or supervises exporter infrastructure.

### WP04 exporter/lifecycle reconciliation

Release 1.10 WP04 adopts the stricter baseline **no external exporter and zero exporter-package mutations**. No OTLP, console, collector, hosted, vendor, or other exporter is selected; each is rejected or deferred pending a separate explicit decision. Worker owns only BCL in-process observations and a bounded lifecycle coordinator. No exporter object, destination, endpoint, credential, socket, background export task, persistent telemetry file, or force-flush operation exists in the baseline.

Exact implementation paths are `src/AIQuantTradingResearch.Worker/WorkerObservabilityLifecycle.cs`, the top-level composition in `src/AIQuantTradingResearch.Worker/Program.cs`, and the existing bounded invocation method in `src/AIQuantTradingResearch.Infrastructure/PythonIntegration/PythonCapabilityInvoker.cs`. The coordinator uses source/meter `AIQuantTradingResearch.Worker`, activity `worker.lifecycle`, and counter `worker.lifecycle.events` (`Counter<long>`, `{event}`), with finite events `startup`, `ready`, `cancelled`, `restart`, `shutdown`, `disabled`, and `failed`. The Python invoker may emit `interop.invoke` only around its real invocation and must preserve protocol stdout.

The coordinator is created once after Worker host construction, observes the existing `WorkerLifetimeCancellation` token, and is disposed idempotently during process shutdown. It never owns or recreates pipeline telemetry, replaces cancellation, supervises Streamlit, or performs force flush. Since no exporter is configured, disabled/unavailable/initialization/export/flush/disposal cases cannot block core execution; bounded in-process state is `disabled` or `degraded`. A later exporter requires a new explicit decision and may not change this default.

WP04 test ownership is `tests/AIQuantTradingResearch.Infrastructure.Tests/WorkerObservabilityLifecycleTests.cs` plus focused assertions in existing `PythonCapabilityInvokerTests.cs`; no new project or package is allowed. Tests prove exactly-once initialization, idempotent shutdown, cancellation/restart isolation, finite attributes, protocol-stdout separation, and zero process/listener/resource residue. WP05 receives only governed bounded observation/read-model evidence and must not inspect coordinator/exporter/invoker internals or directly supervise Worker.

## Performance, security, and reconsideration constraints

Telemetry is non-authoritative, bounded in memory/cardinality, and must not change deterministic outcomes, replay semantics, schema-v4 persistence, or handoff bytes. No arbitrary numeric performance budget is invented by WP01; later authorities must preserve any accepted bounded lifecycle/shutdown budgets and prove no material behavioral regression.

Telemetry must exclude secrets, API keys, tokens, credentials, connection strings, sensitive configuration, raw provider payloads, unrestricted exception data, and high-cardinality dimensions. Health JSON/read models and Streamlit may show only safe bounded classifications, states, durations, outcomes, and provenance. Telemetry presence is never a security proof. WP06/WP08 own executable security, compatibility, and residue validation.

Any need for a package outside the API/SDK family, exporter/backend, schema or persistence change, protocol change, cross-process propagation, live provider, or new path/symbol requires a separate narrow Luna authority before implementation.

## Downstream immutable handoffs

- **WP02:** use this finite vocabulary, outcome domain, timing unit, correlation rule, optional/additive versioning, and technology-neutral Application contract; do not reference OpenTelemetry packages.
- **WP03:** instrument only existing provider/storage mechanics; use bounded failure classes and preserve schema v4 and persistence truth.
- **WP04:** own Worker/interop lifecycle, exporter isolation, bounded shutdown, and diagnostic separation; preserve JSON-over-stdio and Worker/Streamlit independence.
- **WP05:** consume only governed observations/read models and use the exact health states and provenance truthfulness above; no direct SQLite/provider access.

### WP05 System Health reconciliation

WP05 keeps the five existing visualization lifecycle states separate from a nested `SystemHealthSnapshot` in the same `aiq-visualization-read-model-v1` document. The snapshot is .NET-owned and contains only the bounded WP05 `state` token (`ready`, `warmup`, `empty`, `failed`, `stale`, or `unavailable`), governed `provenance` (`historical`, `replay`, or `simulated` for Release 1.10), and an optional finite sanitized `reason`. It is not a telemetry subsystem or a second health channel.

The canonical mapping is visualization `Ready`→`ready`, `WarmUp`→`warmup`, `Empty`→`empty`, `Failed`→`failed`, and existing `Stale`→`stale`; no current WP03/WP04 fact truthfully supports a WP05 `degraded` state, so it is excluded rather than inferred. `unavailable` is the deterministic result of absent/unreadable required health evidence. Neither token claims live-provider, broker, trading, or exporter availability. Historical/replay/simulated disclosure remains independent and visible.

The extension is optional for compatibility. A pre-WP05 v1 payload without `systemHealth` continues to render its existing visualization and exposes health as `unavailable`; unknown extra properties remain ignored. A present malformed health object is rejected by health projection as an integrity error and is never inferred from unrelated fields. No new read-model identifier, JSON channel, SQLite state, or schema version is introduced.

No independent health timestamp, age field, wall-clock comparison, or freshness threshold is selected. `stale` is emitted only when the existing canonical visualization state is `Stale`, using its existing structural reason. Existing source timestamps remain data timestamps. The exact implementation and test paths are frozen in the Release 1.10 execution plan and file manifest; Streamlit remains a read-only consumer of the parsed canonical handoff and never reads SQLite/providers or inspects Worker/listener/exporter state.

#### WP05 V2 semantic/presentation reconciliation

The only normal .NET source predicates are the existing `VisualizationReadModel.State`, `SourceMode`, `SourceAuthority`, `Pipeline`, `Failure`, and `StaleReason` at `VisualizationReadModelUseCase` publication. Precedence is: unreadable/absent required health evidence → `unavailable`; `Failed` → `failed`; `Stale` → `stale`; `WarmUp` → `warmup`; `Empty` → `empty`; `Ready` → `ready`. Current WP03/WP04 implementations expose no persisted optional-loss fact, so WP05 excludes `degraded` rather than inferring it.

The finite reason set is `null`, `pipeline-failed`, `structural-staleness`, and `required-health-evidence-unavailable`, with the latter three restricted to `failed`, `stale`, and `unavailable` respectively. The compatible v1 property is exactly `systemHealth.state/provenance/reason`; no timestamp, age, second channel, schema migration, or independent stale clock is permitted.

In `python/presentation/realtime_financial_visualization.py`, `render_visualization_frame` places an always-visible `System Health` subheader and fixed `st.info`/`st.warning`/`st.error` wording immediately after the existing target/state subheader and before all charts and metadata. The exact text is frozen in the execution plan. Absent health keeps the visualization usable but displays `System Health: Health evidence is unavailable; visualization data may still be available.`; malformed health produces the existing safe last-good behavior plus that same warning. Streamlit never reads SQLite/providers or inspects Worker/listener/exporter state.
- **WP06:** permanently test vocabulary, no-bypass, redaction, cardinality, compatibility, disabled/degraded exporter behavior, deterministic outcomes, and residue.
- **WP07:** document these terms, setup defaults, non-live simulated/replay disclosure, safe configuration, and troubleshooting without adding operational claims.
- **WP08:** validate the complete release matrix, dependency/security evidence, lifecycle/residue behavior, documentation, and exact path scope; no automatic merge, milestone closure, tag, or release publication.

These handoffs are immutable inputs for the later WPs unless a new Luna authority explicitly reopens this record. WP02 can proceed without inventing WP01-owned semantics.

### WP06 permanent observability and no-bypass test ownership

WP06 is test-only and adds no production, package, exporter, schema, runtime, or helper path. Its exact dedicated files are `tests/AIQuantTradingResearch.Application.Tests/Release110ObservabilityPermanentTests.cs`, `tests/AIQuantTradingResearch.Infrastructure.Tests/Release110ObservabilityPermanentTests.cs`, `tests/AIQuantTradingResearch.Architecture.Tests/Release110ObservabilityNoBypassTests.cs`, and `python/presentation/test_release_1_10_observability_no_bypass.py`. Existing WP02–WP05 test files remain predecessor-owned and are read-only corroborating evidence. The Application file owns behavioral Activity/Meter and health-contract assertions; the Infrastructure file owns provider/persistence and Worker/no-exporter behavior; the Architecture file owns resolved project/source no-bypass checks; the Python module owns AST/import and deterministic presentation checks. No shared helper or fixture is authorized.

The permanent assertions use behavior for activity/meter topology and health, serialization for canonical v1 compatibility, resolved source/project inspection for ownership and no-bypass, and Gitleaks 8.30.1 for secrets. They permanently enforce the finite WP05 health states `ready`, `warmup`, `empty`, `failed`, `stale`, `unavailable` and reasons `pipeline-failed`, `structural-staleness`, and `required-health-evidence-unavailable`; `degraded` is not a WP05 health state. They also enforce schema v4, the canonical JSON handoff, Release 1.8 separation, Worker/Streamlit independence, no exporter package/configuration, no direct SQLite/provider/process/listener access from presentation, and no sensitive or unbounded telemetry dimensions.

## Completion boundary

This record authorizes later WPs to evaluate and implement only the selected OpenTelemetry API/SDK family under the policy above. It does not authorize package installation, project-file edits, instrumentation, exporter/backend deployment, schema migration, runtime configuration, Git/GitHub mutations, issue closure, or WP02 execution.
