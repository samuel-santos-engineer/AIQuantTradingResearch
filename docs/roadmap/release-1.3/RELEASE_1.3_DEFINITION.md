# Release 1.3 Definition

## 1. Release title

**Phase 3 — Release 1.3: Research Pipeline Foundation**

## 2. Purpose

Release 1.3 establishes one deterministic, bounded research pipeline that
reuses the accepted historical-observation and research-dataset foundations.
It proves that the platform can execute, explain, and repeat a fixed research
workflow without introducing a general workflow engine, scheduler, or feature
pipeline.

## 3. Problem statement

Release 1.1 can acquire and persist historical observations, and Release 1.2
can materialize, persist, and catalog immutable research datasets. The platform
does not yet expose those accepted capabilities as an explicit pipeline with a
stable definition, deterministic execution evidence, stage-aware outcomes, and
operationally useful correlation.

The smallest coherent next step is to make one fixed research workflow an
Application-owned capability while preserving the existing architecture and
offline reproducibility.

## 4. Accepted predecessor foundations

Release 1.3 consumes rather than redesigns:

- Release 1.1 immutable, idempotent, conflict-aware historical observations;
- deterministic target-isolated retrieval with timestamp-offset and decimal
  fidelity;
- Release 1.2 `DatasetDefinition`, `[from, to)` selection, materialization,
  four dataset identity concepts, `aiq-dataset-identity-v1`, immutable snapshot
  persistence, catalog registration, exact lookup, and bounded integration;
- SQLite schema version 2 and its non-destructive v1-to-v2 upgrade;
- the production dependency graph Domain → none, Application → Domain,
  Infrastructure → Application, Worker → Application and Infrastructure;
- the accepted 171-test and 13-rule architecture baseline.

## 5. User and research capability unlocked

A researcher can submit one explicit pipeline request against already accepted
historical observations and receive a deterministic, stage-aware result that
identifies the pipeline definition, the resulting immutable dataset snapshot,
the terminal outcome, and safe execution evidence. An equivalent rerun produces
the same semantic identity and an equivalent-existing outcome.

## 6. Pipeline concept classification

| Concept | Release 1.3 classification | Rationale |
| --- | --- | --- |
| Fixed research pipeline abstraction | Required | Establishes the new capability without a general engine. |
| Explicit pipeline definition | Required | Makes semantic inputs and behavior reviewable and reproducible. |
| Ordered stage evidence | Required | Explains the fixed execution path and fail-stop point. |
| Dataset materialization, snapshot persistence, catalog registration | Required | Reuses the accepted Release 1.2 vertical slice. |
| Deterministic pipeline-definition and semantic-run identities | Required | Makes equivalent execution independently recognizable. |
| Pipeline result and stage outcomes | Required | Provides bounded success/failure evidence. |
| Manual/on-demand one-shot execution | Required | Is the smallest demonstrable host boundary. |
| Structured local logs with safe correlation | Required | Provides useful observability without paid infrastructure. |
| Cancellation propagation | Permitted but not required | Add only if existing seams support it without redesign. |
| Duration/timing metadata | Permitted but not required | Operational only and excluded from semantic identity. |
| Metrics and traces | Explicitly deferred | Structured events are sufficient for this foundation. |
| Persisted run history or checkpoints | Explicitly deferred | Existing dataset evidence is sufficient for acceptance. |
| Automatic retry, circuit breaker, fallback | Explicitly deferred | No hidden recovery; retry needs a later bounded policy. |
| Resume from partial execution | Explicitly deferred | No durable partial state exists or is required. |
| Live acquisition inside the pipeline | Explicitly deferred | It introduces network/provider nondeterminism into the foundation. |
| Recurring schedules, cron, refresh loops | Explicitly deferred | They are operational automation, not the pipeline semantic core. |
| Parallel stages, DAGs, arbitrary stage plugins | Explicitly deferred | They would create a generalized workflow engine prematurely. |
| Streaming and distributed coordination | Explicitly deferred | They exceed the local bounded release and cost posture. |
| Enrichment and feature generation | Explicitly deferred | They belong after the pipeline foundation, beginning with Release 1.4 planning. |
| Mutable “latest run” semantics | Rejected | Conflicts with deterministic identity and immutable evidence. |
| Swallowed failures or infinite retries | Rejected | Conflicts with fail-stop correctness and resilience guidance. |

## 7. Acquisition boundary decision

Acquisition is outside the Release 1.3 semantic pipeline boundary. The pipeline
operates only on historical observations already accepted through the Release
1.1 persistence seam.

This decision keeps permanent acceptance offline, avoids coupling pipeline
identity to provider availability or credentials, and preserves a clean future
extension: acquisition may later be an explicitly governed upstream trigger,
but it is not a stage of the first reproducible research pipeline.

## 8. Fixed pipeline boundary

The authoritative logical flow is:

```text
explicit ResearchPipelineRequest
    → retrieve accepted historical observations
    → deterministic dataset materialization
    → immutable snapshot persistence
    → deterministic catalog registration
    → ResearchPipelineResult and stage evidence
```

The implementation should delegate to the accepted Release 1.2 use cases and
contracts. It must not create a second dataset orchestration model or expose a
public collection of arbitrary executable stages.

## 9. Execution model

- Trigger: explicit manual/on-demand Worker invocation.
- Host lifecycle: one pipeline operation, then terminal process exit.
- Orchestration: synchronous and sequential unless an existing contract already
  requires asynchronous execution.
- Failure behavior: fail-stop at the first failed stage.
- Partial state: no accepted partial pipeline state; existing snapshot/catalog
  consistency rules remain authoritative.
- Resume: not supported.
- Rerun: safe and idempotent through existing equivalent evidence semantics.
- Success: every required stage completes with newly accepted or equivalent
  accepted evidence.
- Failure: a classified expected failure is returned, while programming defects
  remain visible and are not translated into false business outcomes.

## 10. Core vocabulary

- **Research Pipeline Definition**: immutable semantic description of the fixed
  pipeline contract and its dataset definition.
- **Pipeline Definition Identity**: deterministic identity of that semantic
  definition.
- **Semantic Run Identity**: deterministic identity derived from pipeline
  definition identity and accepted source/dataset outcome evidence.
- **Pipeline Invocation**: one operational attempt; it may have an ephemeral
  correlation identifier that is never semantic identity.
- **Stage Evidence**: ordered result evidence for the fixed stages.
- **Pipeline Result**: terminal success/failure plus safe semantic evidence.

## 11. Determinism and reproducibility

Equivalent accepted historical observations plus an equivalent pipeline
definition must produce the same dataset snapshot and semantic-run identity.

Canonical identity inputs include only semantic values:

- pipeline contract/version marker;
- dataset definition identity;
- accepted source-state identity;
- resulting dataset snapshot identity/version;
- any explicitly approved configuration that changes semantics.

Wall-clock time, duration, machine/process identity, filesystem paths,
connection strings, API keys, culture, local timezone, random values, log
format, and mutable operational status are excluded.

The existing `aiq-dataset-identity-v1` scheme remains unchanged. If a separate
pipeline scheme is necessary, use a distinct versioned marker such as
`aiq-research-pipeline-identity-v1`; its exact canonical representation must be
settled before implementation and must not reinterpret dataset identity.

## 12. Identity, provenance, and lineage relationship

Pipeline identity references accepted dataset identities rather than replacing
them. The pipeline result must preserve the relationship among pipeline
definition, source-state identity, dataset definition, immutable snapshot, and
dataset version. Narrow lineage answers which accepted historical source state
and fixed pipeline definition produced the snapshot.

Operational invocation identifiers and timestamps may correlate logs but are
not provenance inputs and do not create a new dataset version.

## 13. Failure and resilience semantics

Required behavior:

- preserve existing source-history unavailable, storage unavailable,
  invalid-data, integrity-conflict, and configuration-validation distinctions;
- identify the failed fixed stage without leaking SQLite, HTTP, or host types;
- stop immediately after failure and do not execute downstream stages;
- preserve accepted snapshot/catalog evidence without destructive repair;
- treat equivalent reruns as success;
- propagate unknown programming defects visibly;
- never log credentials or connection strings.

Release 1.3 introduces no automatic retry. Retry, timeout policy, circuit
breaking, provider fallback, checkpoint recovery, and resume remain future
operational capabilities because the fixed local pipeline can be correct and
demonstrable without them.

## 14. Observability and execution evidence

Semantic evidence is returned through Application-owned result contracts and
contains deterministic identities, ordered stage outcomes, and terminal result.

Operational observability is Worker-owned structured logging using existing
.NET host facilities. Minimum safe events are pipeline started, stage completed,
pipeline completed, and pipeline failed. Events include safe pipeline/dataset
identities and outcome, but never secrets or database paths. Duration is
permitted as operational metadata only.

No OpenTelemetry backend, dashboard, persisted event store, metrics platform,
or distributed trace system is required.

## 15. Persistence and schema decision

**Schema evolution is not required. SQLite remains at version 2.**

The durable reproducibility evidence already exists in immutable dataset
snapshot and catalog records. Release 1.3 pipeline results and structured logs
can prove the bounded capability without durable run history, checkpoints, or
mutable status tables. A future release may justify separate immutable pipeline
run evidence, but must first define a retrieval/audit requirement that schema v2
cannot satisfy.

## 16. Architecture ownership

- **Domain**: expected delta zero; no provider, storage, pipeline-host, or
  operational mechanics.
- **Application**: pipeline definition/request/result contracts, deterministic
  pipeline identity, fixed orchestration, stage evidence, and failure vocabulary.
- **Infrastructure**: reuses accepted historical and dataset implementations;
  expected production delta zero unless composition needs a compile-level
  adapter that remains provider/storage specific.
- **Worker**: configuration binding, DI composition, one-shot trigger, structured
  operational logs, and process exit mapping.
- **Tests**: deterministic Application behavior, composition/Worker boundaries,
  existing storage regression, and architecture confinement.

The production project/reference graph remains unchanged.

## 17. Security, offline, and cost constraints

- Permanent tests use no live provider, real credential, paid cloud service,
  paid orchestration platform, or paid telemetry backend.
- Secrets remain externally configured and are never semantic identity or logs.
- Test-owned fakes provide historical observations at Application boundaries.
- Any bounded Worker proof uses dummy configuration and must demonstrate zero
  provider calls and zero database residue.
- No new package or project is assumed; later planning must justify any delta.

## 18. Acceptance criteria

Release 1.3 is acceptable only when:

1. one fixed, explicit pipeline request executes the accepted Release 1.2
   materialize/persist/catalog capability;
2. pipeline and semantic-run identities are canonical and deterministic;
3. equivalent runs produce equivalent semantic evidence and no duplicate
   accepted state;
4. source-state changes remain distinguishable through dataset versions;
5. ordered stage evidence and fail-stop behavior are executable;
6. expected failures remain classified and programming defects remain visible;
7. structured local events provide safe correlation without secrets;
8. Worker execution is manual, one-shot, offline-testable, and terminates;
9. SQLite remains schema v2 and Release 1.1/1.2 behavior regresses zero;
10. architecture boundaries and cycles remain unchanged;
11. permanent tests are deterministic and network-free;
12. canonical verification, security scan, documentation, and fresh-checkout
    validation pass;
13. scheduling, retries, resume, DAGs, streaming, distributed execution,
    enrichment, and features remain absent.

## 19. Proposed work packages

| WP | Title | Objective and principal area | Depends on | Major allowed artifacts | Explicit exclusions | Acceptance outcome | Model |
| --- | --- | --- | --- | --- | --- | --- | --- |
| WP01 | Release & Repository Preflight | Reconcile Release 1.2 closure, governance, GitHub, architecture, and 171-test baseline. Governance/validation. | Release 1.2 CLOSED | Evidence/report only | Definition changes, implementation, Git transport | Exact clean starting gate | Luna |
| WP02 | Research Pipeline Semantic Discovery | Settle fixed pipeline, acquisition exclusion, vocabulary, execution, and deferrals. Architecture/research. | WP01 | One bounded semantic decision artifact if manifest-authorized | Contracts, code, schema, GitHub planning | Unambiguous pipeline boundary | Sol |
| WP03 | Pipeline Identity, Provenance & Evidence Semantics | Freeze canonical definition/run identities and semantic versus operational evidence. Application architecture. | WP02 | Narrow semantic specification; Domain delta only if proven necessary | Dataset identity changes, persistence, host behavior | Deterministic identity contract | Sol |
| WP04 | Application Pipeline Contracts | Define provider/storage/host-independent request, result, stage evidence, and use-case seams. Application. | WP03 | Application contracts | Infrastructure, Worker, arbitrary stage/plugin API | Minimal compile-valid contract surface | Terra |
| WP05 | Fixed Pipeline Orchestration | Compose accepted Release 1.2 materialization integration into one fail-stop pipeline use case. Application. | WP04 | Application orchestration | Acquisition, scheduler, parallel/DAG engine, storage redesign | Deterministic new/equivalent execution | Terra |
| WP06 | Pipeline Validation & Failure Semantics | Validate requests/evidence and preserve classified failures, fail-stop, and defect visibility. Application. | WP03, WP05 | Application validation/failure mapping | Retry, timeout framework, exception swallowing | Complete bounded failure matrix | Sol |
| WP07 | Structured Execution Evidence | Define and implement safe stage/run evidence and Worker-owned structured operational events. Application/Host. | WP03, WP05, WP06 | Result evidence and logging boundary | Metrics backend, traces, persisted run history, secrets | Reviewable correlated execution evidence | Terra |
| WP08 | Dependency Registration & Configuration | Register the pipeline use case and bind only semantic/host inputs required for one run. Composition. | WP04, WP05, WP06, WP07 | Existing DI/configuration surfaces | New project, scheduler, dynamic plugins, schema | Real graph resolves offline with accepted lifetimes | Terra |
| WP09 | One-Shot Worker Pipeline Execution | Replace the bounded dataset trigger with one explicit pipeline trigger while preserving termination semantics. Host. | WP08 | Worker composition/execution files | Background loop, acquisition stage, retries, service/API redesign | New and equivalent runs exit predictably | Terra |
| WP10 | Application Pipeline Tests | Permanently prove identities, ordering, stages, equivalence, validation, and failure propagation using test fakes. Testing. | WP03, WP04, WP05, WP06 | Domain/Application test surfaces as justified | SQLite, provider/network, production changes | Deterministic semantic suite | Luna |
| WP11 | Composition & Worker Validation | Prove DI, configuration, safe logs, zero provider calls, bounded exit behavior, and Release 1.1/1.2 regression. Testing/Host. | WP07, WP08, WP09 | Existing composition tests and bounded smoke evidence | New test project/package without authority, live credentials | Offline production-boundary proof | Terra |
| WP12 | Architecture Evolution | Add only stable pipeline ownership/confinement rules while preserving all accepted rules. Architecture tests. | WP09, WP10, WP11 | Architecture.Tests | Incidental namespace/count rules, project/reference changes | Executable ownership and dependency protection | Terra |
| WP13 | Documentation Alignment | Align current-state pipeline, execution, resilience, observability, and future boundaries. Documentation. | WP12 | Manifest-authorized current-state docs | Production/tests, broad roadmap redesign, Release 1.4 implementation | Documentation matches executable truth | Terra |
| WP14 | Full Validation, Integration & Acceptance | Reconcile the complete candidate, validate/fresh-checkout prove it, and perform separately authorized Git integration. Validation/integration. | WP10, WP11, WP12, WP13 | Validation evidence and explicitly authorized transport only | Corrective implementation, merge, tags/releases, Release 1.4 | Review-ready accepted release candidate | Sol |

### Model-cost rationale

- **Luna** is recommended for WP01 and WP10 because their authority should be
  precise and their work is checklist-driven or repetitive deterministic test
  implementation. Escalate to Sol only if unexplained baseline drift or a
  semantic contradiction appears.
- **Terra** is recommended for WP04, WP05, WP07–WP09, and WP11–WP13 because
  they are bounded contract, implementation, composition, validation, or
  documentation tasks. Escalate to Sol if authority conflicts or cross-layer
  redesign becomes necessary.
- **Sol** is recommended for WP02, WP03, WP06, and WP14 because scope,
  canonical identity, failure semantics, and final acceptance are ambiguous or
  cross-cutting and have high architectural risk.

These recommendations use the model roles stated by the planning authority.
Actual availability, pricing, and account access must be verified when each WP
is executed.

## 20. Dependency graph

```text
Release 1.2 CLOSED
  → WP01
    → WP02
      → WP03
        → WP04
          → WP05
            ├─→ WP06
            │    └─→ WP07
            │          └─→ WP08
            │                └─→ WP09
            └─→ WP10 (also depends on WP03, WP04, WP06)

WP07 + WP08 + WP09 → WP11
WP09 + WP10 + WP11 → WP12
WP12 → WP13
WP10 + WP11 + WP12 + WP13 → WP14
```

Exact dependency sets:

```text
WP01: Release 1.2 CLOSED
WP02: WP01
WP03: WP02
WP04: WP03
WP05: WP04
WP06: WP03, WP05
WP07: WP03, WP05, WP06
WP08: WP04, WP05, WP06, WP07
WP09: WP08
WP10: WP03, WP04, WP05, WP06
WP11: WP07, WP08, WP09
WP12: WP09, WP10, WP11
WP13: WP12
WP14: WP10, WP11, WP12, WP13
```

## 21. GitHub planning recommendation

Legacy milestone #44 currently exists as open and empty with title
`Phase 3 - Release 1.3: Pipelines`. Its description includes ingestion,
validation, transformation, enrichment, feature generation, scheduling,
monitoring, and execution, which materially exceeds this definition.

Later GitHub-planning authority should preserve #44 as legacy history by closing
it empty without repurposing it, then create one authoritative milestone titled
`Phase 3 - Release 1.3: Research Pipeline Foundation`. It should add Project #2
Release option `1.3` only if still absent and create exactly the accepted WP
set after execution-plan/file-manifest approval. No existing issue or PR claims
Release 1.3 scope at definition time.

## 22. Release 1.4 and later deferrals

Release 1.4 planning may evaluate deterministic transformations and feature
engineering over cataloged dataset snapshots. It must remain separate from
Release 1.3 pipeline execution semantics.

Later releases may separately justify scheduling, autonomous refresh,
streaming acquisition, generalized DAGs/plugins, persisted run history,
recovery/checkpoints, automatic retry/circuit breaking, distributed execution,
cloud deployment, advanced telemetry/alerting, ML training/serving,
backtesting, and portfolio/risk capabilities.

## 23. Handoff

This definition does not authorize implementation, GitHub planning, an
execution plan, a file manifest, WP prompts, schema changes, packages, Git
transport, or Release 1.4 work.

After human acceptance, separately design:

- `RELEASE_1.3_EXECUTION_PLAN.md`
- `RELEASE_1.3_FILE_MANIFEST.md`

Those artifacts must reconcile the accepted 14-WP graph and exact ownership
before any GitHub planning or WP01 execution.
