# Release 1.10 WP02 — Application Pipeline Observability Contract Authority v3

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, architecture, vocabulary, scope, reconciliation, acceptance criteria, and governance authority.
- **GPT-5.6 Terra** — PRIMARY implementation/execution authority for WP02 source/test mutations and focused validation.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and non-authoritative review; Sol does not replace Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

---

# Authority identity

Release: **1.10 — OpenTelemetry / Pipeline Observability**

Work package:

**WP02 — Application Pipeline Observability Contract**

Canonical issue:

**#243**

Predecessor:

**WP01 / #242 — Observability Selection, Vocabulary & Scope**

This v3 authority supersedes the earlier blocked WP02 Terra attempts by incorporating both accepted GPT-5.6 Luna reconciliations:

1. WP02 Path Contract Reconciliation
2. WP02 Stage-Boundary Reconciliation

---

# Mandatory canonical inputs

Read in full before mutation:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. GitHub issue #243
6. current `PipelineExecutionUseCase`
7. current `MaterializeDatasetUseCase`
8. relevant interfaces and Application tests.

The current planning artifacts—not this prompt—are authoritative if wording differs.

If either Luna reconciliation is missing or inconsistent:
BLOCK before implementation.

---

# Frozen WP02 implementation contract

## BCL-only

Application observability uses BCL `System.Diagnostics` / `System.Diagnostics.Metrics` only.

No Application OpenTelemetry package reference.
No SDK/provider/exporter/configuration in WP02.

Required marker:

`RELEASE 1.10 WP02 APPLICATION OBSERVABILITY API: BCL-ONLY ENFORCED`

## Authorized add

`src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs`

## Authorized PipelineExecutionUseCase modifications

Modify only:

- `PipelineExecutionUseCase.Execute(...)`
- `PipelineExecutionUseCase.ExecuteCanonical(...)`

No unrelated symbol/hunk changes.

## Authorized MaterializeDatasetUseCase modifications

Modify only the two accepted:

- `MaterializeDatasetUseCase.Execute(...)` overloads

The exact existing file path must be derived from the reconciled manifest and reported before mutation.

No other symbol/member in that file may change.

## Authorized test add

`tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs`

No other test file is writable unless the reconciled manifest explicitly names it.

---

# Frozen truthful stage semantics

The five accepted Application stages remain separate only where real intervals exist.

Critically:

## HistoricalObservationRetrieval

Measures **only** the real interval of:

`IHistoricalObservationStore.Retrieve(...)`

No filtering, transformation, or snapshot construction belongs in this duration.

## DatasetMaterialization

Measures **only** the subsequent Application-owned filtering and snapshot construction after retrieval completes.

It must not include the retrieval interval.

## Truthfulness invariant

No two activities may time the same opaque interval under different semantic names.

No invented duration partitioning.

No duplicated wrapper spans around `IMaterializeDatasetUseCase.Execute(...)` pretending to represent both retrieval and materialization.

Required marker:

`RELEASE 1.10 WP02 STAGE TRUTHFULNESS: PASS — NO DUPLICATED INTERVALS`

---

# WP03 handoff

WP03 may later create actual Infrastructure retrieval/persistence child activities beneath the relevant ambient Application activity via `Activity.Current`.

WP03 MUST NOT need to change:

- WP02 ActivitySource identity;
- WP02 Meter identity;
- WP02 activity names;
- WP02 metrics;
- WP02 Application business semantics;
- WP02 stage timing ownership.

WP02 must leave a truthful ambient parent context where the accepted call topology naturally permits it.

---

# Out of scope

Do NOT implement:

- Infrastructure/provider instrumentation;
- persistence instrumentation;
- OpenTelemetry SDK/provider composition;
- exporter installation/configuration;
- Worker/interop lifecycle;
- exporter isolation;
- System Health read model;
- Streamlit UI;
- schema changes;
- Python telemetry ownership;
- live provider/broker/exchange connectivity;
- trading;
- ML/backtesting;
- WP06 permanent cross-cutting suite beyond focused WP02 tests;
- WP07 runbook;
- WP08 full-release validation.

---

# Git / GitHub boundary

## Git

ZERO mutations.

Do not stage, commit, push, branch, merge, rebase, tag, amend, or alter index.

## GitHub

ZERO mutations.

Do not edit/close #243 or #242.
Do not change milestone #59.
Do not change Project #2.
Do not create a PR.

This authority produces validated working-tree implementation only.

---

# Phase 0 — Entry audit

Record:

- repository identity;
- branch;
- local HEAD;
- authoritative remote `main` if available without prohibited mutation;
- `git status --short`;
- staged paths;
- untracked paths;
- WP01 selection record;
- both Luna reconciliation planning edits;
- absence/presence of prior WP02 implementation residue;
- issue #243 state if available.

The prior Terra attempts reported no WP02 implementation mutations. Verify rather than assume.

Preserve all pre-existing planning artifacts.

Emit:

`RELEASE 1.10 WP02 ENTRY BASELINE: ACCEPTED`

---

# Phase 1 — Dual reconciliation read-back

Prove the current planning artifacts freeze all of:

- `PipelineObservability.cs` add path;
- `PipelineExecutionUseCase.Execute(...)`;
- `PipelineExecutionUseCase.ExecuteCanonical(...)`;
- both `MaterializeDatasetUseCase.Execute(...)` overloads;
- `PipelineObservabilityTests.cs` add path;
- BCL-only API;
- exact five-stage topology;
- truthful retrieval/materialization split;
- WP03 `Activity.Current` handoff.

Print exact source file paths and symbol signatures.

Emit:

`RELEASE 1.10 WP02 CONTRACT RECONCILIATION: PASS`

---

# Phase 2 — Baseline validation

Before mutation run:

- Application build;
- Application tests;
- relevant architecture/no-bypass tests;
- residue/process/listener check if applicable.

Known prior baseline evidence was:

- Application build: 0 warnings / 0 errors
- Application tests: 125/125 passed.

Re-run; do not rely on stale counts.

Record exact results.

If baseline fails materially:
BLOCK unless clearly unrelated and accepted workflow permits continuation.

---

# Phase 3 — Implement PipelineObservability

Add exactly:

`src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs`

Implement the frozen WP01/Luna Application observability contract using BCL only.

Own exactly the accepted:

- ActivitySource name/instance;
- Meter name/instance;
- canonical activity names;
- WP02 metric instruments;
- stable attribute/outcome constants/helpers if assigned.

Requirements:

- deterministic static names;
- no dynamic source/meter/span names;
- no exporter/provider/SDK ownership;
- no Infrastructure dependency;
- no telemetry-owned business state;
- bounded attributes;
- no secrets;
- no raw provider payload;
- no general-purpose telemetry framework.

---

# Phase 4 — Instrument PipelineExecutionUseCase

Modify only:

- `Execute(...)`
- `ExecuteCanonical(...)`

Implement only the accepted stage/root boundaries owned by these methods.

Preserve:

- execution order;
- outputs;
- exception propagation;
- deterministic behavior;
- business ownership.

Use exact canonical names/metrics/attributes from the reconciled contracts.

Do not manufacture retrieval/materialization spans around the opaque materialization call at this layer.

Those two truthful boundaries are owned inside the authorized `MaterializeDatasetUseCase.Execute(...)` overloads.

No unrelated refactoring.

---

# Phase 5 — Instrument HistoricalObservationRetrieval truthfully

In each authorized `MaterializeDatasetUseCase.Execute(...)` overload:

Create/record the `HistoricalObservationRetrieval` Application activity/metric interval around **only**:

`IHistoricalObservationStore.Retrieve(...)`

Requirements:

- start immediately before the real retrieval invocation;
- end immediately after retrieval returns or throws;
- no subsequent filtering/snapshot construction included;
- accepted success/failure semantics;
- accepted bounded attributes only;
- exception propagation unchanged;
- no Infrastructure span implemented here;
- ambient activity remains available for WP03 to nest actual Infrastructure retrieval instrumentation later.

If the concrete code structure prevents this exact truthful interval without modifying another symbol:
BLOCK.

---

# Phase 6 — Instrument DatasetMaterialization truthfully

In each authorized `MaterializeDatasetUseCase.Execute(...)` overload:

Create/record the `DatasetMaterialization` interval only after retrieval has completed.

It covers only the reconciled Application-owned:

- filtering;
- accepted transformation/materialization logic;
- snapshot construction.

It MUST NOT include `IHistoricalObservationStore.Retrieve(...)`.

Requirements:

- distinct start/end from retrieval;
- no overlap that falsely duplicates the retrieval duration unless the reconciled contract explicitly requires a truthful parent relationship;
- success/failure semantics match actual materialization;
- exception behavior unchanged;
- metrics record only the correct interval.

Emit:

`RELEASE 1.10 WP02 STAGE TRUTHFULNESS: PASS — NO DUPLICATED INTERVALS`

---

# Phase 7 — Verify all five Application stages

For each accepted stage report:

- canonical activity name;
- exact owning symbol;
- exact measured interval;
- parent/child relationship;
- success/failure rule;
- metric;
- attributes;
- downstream WP owner if deeper instrumentation exists.

Require every stage to correspond to a real interval.

Emit:

`RELEASE 1.10 WP02 APPLICATION PIPELINE BOUNDARIES: PASS`

---

# Phase 8 — Add focused tests

Add exactly:

`tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs`

Test the frozen WP02 contract.

At minimum, as applicable:

- ActivitySource identity;
- Meter identity;
- all five canonical activity names;
- activity emission with BCL listener;
- unchanged behavior with no listener;
- `Execute(...)`;
- `ExecuteCanonical(...)`;
- both `MaterializeDatasetUseCase.Execute(...)` overloads;
- retrieval span covers retrieval only;
- materialization span begins after retrieval;
- retrieval/materialization do not duplicate the same opaque interval;
- parent/child relationships;
- success outcome;
- failure outcome;
- exception propagation;
- metric names/types/units;
- success/failure metric recording;
- allowlisted bounded attributes;
- absence of prohibited high-cardinality dimensions;
- no secret/raw payload tags;
- no Infrastructure/provider/persistence spans introduced by WP02.

Tests must require no OpenTelemetry SDK, collector, exporter, network, Streamlit, or Worker lifecycle.

---

# Phase 9 — Package/BCL audit

Prove:

- no `.csproj` changed;
- no central package file changed;
- no OpenTelemetry package added;
- no SDK/provider/exporter reference;
- Application observability uses BCL diagnostics only.

Emit:

`RELEASE 1.10 WP02 APPLICATION OBSERVABILITY API: BCL-ONLY ENFORCED`

---

# Phase 10 — Architecture/no-bypass audit

Require:

- no Application → Infrastructure dependency;
- no Application → Python/Streamlit dependency;
- no telemetry canonical business state;
- no parallel pipeline;
- no schema v4 change;
- no exporter/provider composition in Application;
- no presentation SQLite access;
- no provider call from Python/UI.

Run relevant architecture tests.

Emit:

`RELEASE 1.10 WP02 ARCHITECTURE/NO-BYPASS: PASS`

---

# Phase 11 — Security/cardinality audit

Verify WP01 rules:

- no secrets/tokens/credentials;
- no connection strings;
- no raw provider payload;
- no uncontrolled exception message metric labels;
- no GUID/request IDs as metric dimensions;
- no timestamps as dimensions;
- no raw file paths as dimensions;
- no uncontrolled ticker/symbol metric dimensions;
- no arbitrary dynamic telemetry names.

Run appropriate repository security scan over working-tree changes without staging.

Emit:

`RELEASE 1.10 WP02 TELEMETRY SECURITY: PASS`

---

# Phase 12 — Focused validation

Run all WP02 acceptance validation required by issue #243/current plan.

At minimum:

1. affected .NET build;
2. full Application tests;
3. focused `PipelineObservabilityTests`;
4. relevant architecture/no-bypass tests;
5. security scan;
6. process/listener residue check.

Report exact commands/counts/warnings/errors.

No full Release 1.10 acceptance claim.

---

# Phase 13 — Functional preservation

Prove:

- outputs unchanged;
- exceptions unchanged;
- retrieval/materialization semantics unchanged;
- deterministic/replay provenance unchanged;
- persistence behavior unchanged;
- JSON handoff unchanged;
- schema v4 unchanged;
- Streamlit unchanged;
- Worker lifecycle unchanged.

Emit:

`RELEASE 1.10 WP02 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

---

# Phase 14 — WP03 handoff verification

Verify that during the real retrieval call the relevant Application activity is ambient so WP03 can later nest Infrastructure retrieval instrumentation via `Activity.Current`.

Verify WP03 can add persistence/provider child activities without:

- changing WP02 files/contracts;
- renaming activities/meters;
- adding Application package dependencies;
- duplicating WP02 timing ownership.

Emit:

`RELEASE 1.10 WP02 DOWNSTREAM HANDOFF: PASS — WP03 READY`

---

# Phase 15 — Exact path and hunk audit

Compare against Phase 0.

WP02 implementation delta may include only:

1. ADD `src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs`
2. MODIFY only `PipelineExecutionUseCase.Execute(...)`
3. MODIFY only `PipelineExecutionUseCase.ExecuteCanonical(...)`
4. MODIFY only the two authorized `MaterializeDatasetUseCase.Execute(...)` overloads
5. ADD `tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs`

Determine exact existing source paths for the two modified classes from the reconciled manifest.

At hunk/symbol level prove no unrelated member changed.

Pre-existing WP01 and Luna planning artifacts must remain preserved and are not counted as WP02 implementation changes.

Require zero unexpected paths/hunks.

Emit:

`RELEASE 1.10 WP02 PATH OWNERSHIP: PASS`

---

# Phase 16 — Acceptance matrix

Evaluate every issue #243/current execution-plan criterion individually.

For each report:

- criterion;
- evidence;
- validation;
- PASS/BLOCK.

Require all PASS.

Emit:

`RELEASE 1.10 WP02 ACCEPTANCE: PASS`

---

# Phase 17 — Mutation accounting

Separate:

## Pre-existing preserved planning/contract artifacts
List them exactly.

## WP02 implementation mutations
List exact added/modified paths and symbols.

Required:

`RELEASE 1.10 WP02 REPOSITORY MUTATIONS: ACCEPTED WP02 PATHS ONLY`

`RELEASE 1.10 WP02 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP02 GITHUB MUTATIONS: ZERO`

Do not stage, commit, push, close #243, or update Project status.

---

# Phase 18 — Next authority

On PASS:

**Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority — GPT-5.6 Terra**

Do not execute WP03 here.

WP03 must explicitly define Luna/Terra/Sol roles and consume the WP02 ambient Application activity contract as immutable input.

---

# Required final report

Report:

1. model assignment;
2. entry baseline/residue;
3. dual Luna reconciliation read-back;
4. baseline validation;
5. exact source/test mutations;
6. all five truthful stage intervals;
7. tests;
8. BCL/package audit;
9. architecture/no-bypass;
10. security/cardinality;
11. focused validation counts;
12. functional preservation;
13. WP03 handoff;
14. path/hunk audit;
15. acceptance matrix;
16. mutation accounting;
17. exact next authority.

---

# Success markers

`RELEASE 1.10 WP02 CONTRACT RECONCILIATION: PASS`

`RELEASE 1.10 WP02 APPLICATION OBSERVABILITY API: BCL-ONLY ENFORCED`

`RELEASE 1.10 WP02 STAGE TRUTHFULNESS: PASS — NO DUPLICATED INTERVALS`

`RELEASE 1.10 WP02 APPLICATION PIPELINE BOUNDARIES: PASS`

`RELEASE 1.10 WP02 ARCHITECTURE/NO-BYPASS: PASS`

`RELEASE 1.10 WP02 TELEMETRY SECURITY: PASS`

`RELEASE 1.10 WP02 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

`RELEASE 1.10 WP02 PATH OWNERSHIP: PASS`

`RELEASE 1.10 WP02 DOWNSTREAM HANDOFF: PASS — WP03 READY`

`RELEASE 1.10 WP02 ACCEPTANCE: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

`RELEASE 1.10 WP02 REPOSITORY MUTATIONS: ACCEPTED WP02 PATHS ONLY`

`RELEASE 1.10 WP02 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP02 GITHUB MUTATIONS: ZERO`

Terminal:

`RELEASE 1.10 WP02 — APPLICATION PIPELINE OBSERVABILITY CONTRACT AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- either Luna reconciliation is absent/inconsistent;
- truthful retrieval/materialization intervals cannot be implemented inside the newly authorized symbols;
- any required path/symbol is outside the frozen allowlist;
- an Application package becomes necessary;
- two stage activities would measure the same interval;
- WP02 would absorb WP03+ scope;
- architecture direction changes;
- business behavior changes;
- tests/security/cardinality fail;
- unexpected path/hunk appears;
- Git/GitHub mutation occurs.

Report exact evidence and smallest required reconciliation.

Terminal:

`RELEASE 1.10 WP02 — APPLICATION PIPELINE OBSERVABILITY CONTRACT AUTHORITY BLOCKED`
