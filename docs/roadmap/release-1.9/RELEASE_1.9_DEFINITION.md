# Release 1.9 — Real-Time Financial Data Visualization

## Status and predecessor

**Status:** Definition candidate; planning only. Implementation, work-package
issues, Project items, and package changes are not authorized.

**Immutable predecessor:** Roadmap Reconciliation / Release Sequencing merge
commit `3a02f035a253e4e16f479e1866c9a5195f5cfbdb` (PR #224; accepted branch head
`8e1e9deed02c583bff2298a55ab1758dc33fdcf9`). Release 1.8 remains closed at
schema v3 with its accepted Python and interoperability foundation.

## Objective

Make the existing platform visibly demonstrable: a deterministic simulated-live
observation sequence is processed by the existing fixed pipeline and rendered
as truthful financial, feature, and data-quality state in a minimal local
Streamlit UI.

```text
simulated observation → existing pipeline → persisted snapshot / feature / validation evidence → UI update
```

The UI must explicitly label the source as simulated/replayed; it must never
claim real live market data.

## Scope and non-goals

Scope is a deterministic incremental replay adapter behind the existing
`IObservationSource` boundary; reuse of the five-stage pipeline; an
Application-owned bounded presentation read model; Worker-owned lifecycle; and
a Streamlit outer adapter showing price/time, latest/count/window,
`simple-return-lag-1-v1` value or warm-up, snapshot identity/version, and
supported validation/quality state.

Excluded are real provider streaming, paid services, credentials, cloud,
trading, ML prediction/training, OpenTelemetry/System Health, backtesting,
Explainable AI, schema migration, package changes, a frontend framework,
direct SQLite UI access, Streamlit business logic, and a second pipeline.

## Architecture and reuse

Domain remains independent of UI, Python, processes, files, and provider
mechanics. Application owns read-contract semantics and orchestration;
Infrastructure owns replay/provider/persistence/handoff mechanics; Worker owns
validated composition and lifecycle; Streamlit renders only presentation state.

The governing seams are existing `IObservationSource`,
`PipelineExecutionUseCase`, `PipelineExecutionEvidence`, snapshot store/catalog,
and `IFeatureGenerationUseCase`. Pipeline success already establishes the fixed
five ordered stages, source state, snapshot identity/version, and newly-
accepted/equivalent-existing evidence. The feature is only
`FeatureDefinition.SimpleReturnLag1V1` over that same snapshot identity/version.

Streamlit consumes a bounded, versioned, non-authoritative presentation
read-model snapshot atomically emitted by Worker after Application
orchestration. It never opens SQLite, calls a provider, computes features, or
changes accepted evidence. The handoff format, atomic-write rules, retention,
and stale/failure display are WP04/05 deliverables using standard .NET/Python
facilities—no new external technology.

Release 1.8's local one-shot JSON-over-stdio capability boundary remains
governed for bounded Python capabilities and regressions. A persistent
Streamlit process is not misrepresented as that one-shot invocation. Any new
service, framework, or dependency requires separate foundational authority.

## Determinism, lifecycle, and safety

Replay has a fixed repository-owned ordered fixture, target, UTC instants,
decimal values, replay identity, finite length, logical ticks, and accelerated
test mode. Tests do not rely on wall-clock sleeps. Restart is explicit;
duplicates use existing idempotency/equivalent-existing behavior and conflicts
remain failures. Worker owns only children it starts, cancellation, ports,
temporary handoff cleanup, and local database residue. It never kills unrelated
Python, VS Code, or Jedi processes.

Schema v4 is unchanged. It is the accepted Release 1.9 persistence boundary,
established by the accepted WP03 schema-evolution authority and preserved by
WP09. Existing SQLite persistence remains the accepted source
of historical/snapshot evidence; no UI table, migration, model, or durable
presentation history is authorized. `.venv`, CPython 3.13.15, exact pins, and
the local one-shot JSON boundary remain unchanged. No secret, global package,
absolute user path, arbitrary script, or real provider call is permitted.

## Presentation and acceptance

The one-page UI contains: simulated-stream overview; evolving price/time and
latest/count/window; `simple-return-lag-1-v1` with explicit warm-up/unavailable
state; and truthful snapshot/pipeline/quality/validation state. Missing, stale,
malformed, or failed read models render safe empty/failure state. System Health
belongs to Release 1.10.

Acceptance repeatedly proves finite replay → existing pipeline → persistence /
feature / validation → UI update; idempotent replay; no owned residue; schema
v4; governed Python cleanliness; WP08/WP11 regressions; full .NET verification;
and no ML/OpenTelemetry/backtesting leakage. Application, Infrastructure,
Architecture, and governed Streamlit tests each own their appropriate layer.

## Risks and integration

The atomic local read-model handoff avoids a new service or direct SQLite UI
bypass but requires explicit staleness/atomicity handling. If its implementation
requires schema change, HTTP/service transport, direct database UI access, or a
new dependency, stop for narrow authority.

Implementation later requires a dedicated release branch → acceptance → PR →
verification → merge. This definition does not authorize WP01, GitHub planning,
or implementation.
