# Release 1.10 WP02 — Application Pipeline Observability Contract Authority v2

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, architecture, scope, vocabulary, acceptance, governance, and reconciliation authority.
- **GPT-5.6 Terra** — PRIMARY implementation/execution authority for approved source/test mutations and validation.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and non-authoritative review; Sol does not replace Luna or Terra for assigned authorities.

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

This v2 authority resumes WP02 after the successful Luna path-contract reconciliation.

---

# Resolved prior block

The earlier Terra WP02 authority correctly blocked because exact writable Application symbols/test paths and the Application API surface were not frozen.

GPT-5.6 Luna has now reconciled that gap.

Accepted frozen execution boundary:

## Add
`src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs`

## Modify
Only the implementations of:

- `PipelineExecutionUseCase.Execute(...)`
- `PipelineExecutionUseCase.ExecuteCanonical(...)`

Do not modify unrelated members in that file.

## Add test
`tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs`

## API surface
**BCL `System.Diagnostics` only.**

No Application OpenTelemetry package reference is authorized.

No OpenTelemetry SDK, exporter, provider, hosting extension, or exporter configuration is authorized in WP02.

## WP03 handoff
WP03 may nest Infrastructure activities beneath the current Application activity using `Activity.Current`, but WP03 may not alter the frozen WP02 Application observability contract.

Re-read these decisions from the current planning artifacts. This prompt is not a substitute if they differ.

---

# Canonical inputs

Read in full before mutation:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. GitHub issue #243
6. current `PipelineExecutionUseCase` implementation
7. current Application test conventions.

If the updated execution plan/file manifest do not contain the successful Luna reconciliation:
BLOCK before mutation.

If issue #243 materially conflicts with the reconciled contract:
BLOCK.

---

# Architecture boundary

Preserve:

- .NET Application pipeline ownership;
- Infrastructure dependency direction;
- canonical JSON handoff;
- SQLite schema v4;
- deterministic/replay/simulated provenance;
- Worker/Streamlit independence;
- existing business outputs and exception semantics;
- no-bypass architecture.

WP02 MUST NOT implement:

- provider instrumentation;
- persistence instrumentation;
- Infrastructure activities;
- OpenTelemetry SDK/provider/exporter composition;
- Worker/interop lifecycle;
- exporter lifecycle/isolation;
- System Health read model;
- Streamlit System Health UI;
- schema migration;
- live providers;
- trading;
- ML/backtesting;
- Python telemetry ownership;
- full permanent WP06 suite;
- WP07 docs/runbook;
- WP08 release validation.

---

# Hard path boundary

The only WP02 production/test implementation paths authorized are the exact reconciled paths.

## ADD

`src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs`

## MODIFY

The file containing `PipelineExecutionUseCase`, but modifications are limited strictly to:

- `Execute(...)`
- `ExecuteCanonical(...)`

Determine and report the exact existing file path from the repository.

No other symbol in that file may be changed.

## ADD

`tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs`

No other source/test path is writable.

Planning artifacts and `OPEN_TELEMETRY_SELECTION.md` are read-only during this implementation authority.

If implementation requires another path:
BLOCK and return to Luna.

---

# Package boundary

WP02 is **BCL-only**.

Allowed namespaces/APIs include the reconciled BCL `System.Diagnostics` / `System.Diagnostics.Metrics` primitives.

Forbidden:

- adding OpenTelemetry packages;
- modifying `.csproj`;
- modifying central package management;
- changing Python dependencies;
- adding exporter packages;
- adding SDK/provider hosting packages.

Required marker:

`RELEASE 1.10 WP02 APPLICATION OBSERVABILITY API: BCL-ONLY ENFORCED`

---

# Git / GitHub boundary

## Git

ZERO mutations.

Do not:

- checkout/switch;
- create/delete branch;
- stage;
- commit;
- amend;
- merge/rebase;
- push;
- tag;
- alter index.

## GitHub

ZERO mutations.

Do not:

- edit/close #243;
- edit #242;
- change milestone #59;
- change Project #2;
- create PR.

This authority ends with validated working-tree implementation only.

---

# Phase 0 — Entry audit

Record:

- repository identity;
- branch;
- local HEAD;
- remote `main` SHA if available without prohibited mutation;
- `git status --short`;
- staged/untracked paths;
- all pre-existing Release 1.10 planning artifacts;
- WP01 untracked selection record;
- Luna reconciliation planning edits;
- issue #243 state if available.

Classify all pre-existing residue and preserve it.

Emit:

`RELEASE 1.10 WP02 ENTRY BASELINE: ACCEPTED`

---

# Phase 1 — Reconciliation read-back

Read the updated execution plan/file manifest and prove they freeze:

- exact `PipelineObservability.cs` add path;
- exact `Execute(...)` / `ExecuteCanonical(...)` modification boundary;
- exact `PipelineObservabilityTests.cs` add path;
- BCL-only Application API;
- no package reference;
- additive contract shape;
- WP03 `Activity.Current` nesting handoff.

Emit:

`RELEASE 1.10 WP02 CONTRACT RECONCILIATION: PASS`

If any is missing:
BLOCK.

---

# Phase 2 — Baseline validation

Before WP02 mutation, run the relevant baseline:

- affected .NET build;
- existing Application tests;
- architecture/no-bypass tests relevant to Application dependency direction;
- process/listener residue check if repository validation conventions require it.

Record exact counts/results.

Do not fix unrelated failures.

If baseline failure prevents attribution of WP02 correctness:
BLOCK.

---

# Phase 3 — Implement `PipelineObservability.cs`

Add exactly:

`src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs`

Implement only the additive Application-owned observability contract frozen by Luna/WP01.

Use BCL primitives only.

The file must own the exact reconciled:

- `ActivitySource` identity/name;
- `Meter` identity/name;
- canonical Application activity names;
- WP02-owned metric instruments;
- bounded tag/attribute helpers/constants if the contract assigns them here.

Requirements:

- no OpenTelemetry SDK reference;
- no exporter/provider composition;
- no Infrastructure dependency;
- no business state;
- no dynamic telemetry names;
- no high-cardinality metric dimensions;
- no secrets/payloads;
- deterministic static contract.

Do not turn this into a general-purpose telemetry framework.

---

# Phase 4 — Instrument `Execute(...)`

Modify only the existing implementation of:

`PipelineExecutionUseCase.Execute(...)`

Apply the exact reconciled WP02 operation/stage contract.

Requirements:

- create/start the accepted Application activity boundary;
- preserve current functional execution order;
- preserve return value;
- preserve exception propagation;
- record accepted success/failure semantics;
- record only WP02-owned metrics;
- apply only allowlisted bounded attributes;
- do not instrument provider/persistence internals;
- do not catch exceptions merely for telemetry unless the frozen contract requires catch-record-rethrow behavior;
- no listener/provider must result in unchanged business behavior.

No unrelated refactoring.

---

# Phase 5 — Instrument `ExecuteCanonical(...)`

Modify only:

`PipelineExecutionUseCase.ExecuteCanonical(...)`

Apply the exact reconciled canonical-operation contract.

Requirements mirror `Execute(...)`:

- canonical activity name;
- correct parent/root relationship;
- accepted metric recording;
- accepted status/outcome;
- exception propagation unchanged;
- no infrastructure instrumentation;
- no high-cardinality/sensitive tags;
- functional output unchanged.

If `ExecuteCanonical(...)` delegates to `Execute(...)`, preserve the exact Luna-defined span relationship and avoid accidental duplicate/incorrect instrumentation.

---

# Phase 6 — Application boundary verification

Verify every WP02 Application operation/stage against `OPEN_TELEMETRY_SELECTION.md` and the reconciled execution plan.

For each report:

- exact method;
- activity name;
- parent/child relationship;
- metric(s);
- allowed attributes;
- success behavior;
- failure behavior.

Emit:

`RELEASE 1.10 WP02 APPLICATION PIPELINE BOUNDARIES: PASS`

---

# Phase 7 — Add focused tests

Add exactly:

`tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs`

No other test file may be changed.

Implement focused deterministic tests for the reconciled WP02 contract.

Cover as applicable:

- canonical ActivitySource identity;
- canonical Meter identity;
- accepted activity names;
- activity emission with listener;
- unchanged behavior with no listener;
- `Execute(...)` semantics;
- `ExecuteCanonical(...)` semantics;
- parent/child relationship;
- success outcome;
- failure outcome;
- exception propagation;
- metric name/type/unit;
- success/failure metric recording;
- bounded allowlisted attributes;
- absence of prohibited high-cardinality metric attributes;
- absence of secrets/raw payload;
- no provider/persistence spans implemented in WP02.

Tests must not require:

- OpenTelemetry SDK;
- collector;
- exporter;
- network;
- Streamlit;
- Worker lifecycle.

---

# Phase 8 — BCL/package audit

Prove:

- no `.csproj` changed;
- no package file changed;
- no OpenTelemetry package added;
- no SDK/provider/exporter reference introduced;
- Application uses BCL diagnostics only.

Emit:

`RELEASE 1.10 WP02 APPLICATION OBSERVABILITY API: BCL-ONLY ENFORCED`

---

# Phase 9 — Architecture/no-bypass audit

Run relevant architecture tests and inspect dependencies.

Require:

- no Application → Infrastructure dependency;
- no Application → Python/Streamlit dependency;
- no exporter/provider composition in Application;
- no telemetry-owned canonical business state;
- no parallel pipeline;
- no schema change;
- no direct presentation SQLite access introduced;
- no provider call introduced into presentation/Python.

Emit:

`RELEASE 1.10 WP02 ARCHITECTURE/NO-BYPASS: PASS`

---

# Phase 10 — Telemetry security/cardinality audit

Verify WP01/WP02 rules.

Prohibit:

- secrets;
- API keys/tokens;
- credentials;
- connection strings;
- arbitrary provider payloads;
- raw exception text as metric labels;
- stack traces as metric dimensions;
- GUID/request IDs as metric dimensions;
- timestamps as dimensions;
- raw file paths as metric dimensions;
- uncontrolled symbols/tickers as metric dimensions;
- arbitrary dynamic activity/metric names.

Run repository security scanning appropriate to changed working-tree files without staging.

Emit:

`RELEASE 1.10 WP02 TELEMETRY SECURITY: PASS`

---

# Phase 11 — Focused validation

Run all issue #243/WP02-required validation.

At minimum:

1. restore/build as required;
2. affected .NET build;
3. Application tests including `PipelineObservabilityTests`;
4. relevant architecture/no-bypass tests;
5. security scan;
6. process/listener residue check.

Record exact command, count, pass/fail, and warnings/errors where available.

No full Release 1.10 acceptance claim.

---

# Phase 12 — Behavioral preservation

Prove instrumentation did not change canonical functional behavior.

Validate as applicable:

- same successful outputs;
- same failure propagation;
- same deterministic/replay semantics;
- no changed persistence behavior;
- no changed JSON handoff;
- no schema v4 change;
- no Streamlit behavior change;
- no Worker ownership change.

Emit:

`RELEASE 1.10 WP02 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

---

# Phase 13 — WP03 handoff verification

Prove the implementation preserves the frozen downstream contract:

- Application activity is current while downstream Infrastructure work executes where intended;
- WP03 can use `Activity.Current` to parent Infrastructure spans;
- WP03 does not need to modify `PipelineObservability.cs`;
- WP03 does not need to rename Application activities/meters;
- WP03 does not need an Application package/SDK dependency;
- WP03 can instrument provider/persistence/failure behavior independently.

Emit:

`RELEASE 1.10 WP02 DOWNSTREAM HANDOFF: PASS — WP03 READY`

---

# Phase 14 — Hard path/diff audit

Compare current working tree to Phase 0.

WP02-created implementation delta must be exactly limited to:

1. ADD `src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs`
2. MODIFY only `Execute(...)` and `ExecuteCanonical(...)` in the existing `PipelineExecutionUseCase` file
3. ADD `tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs`

Pre-existing WP01/reconciliation documentation changes are preserved and excluded from WP02 implementation mutation count.

Inspect the `PipelineExecutionUseCase` diff at symbol/hunk level and prove no unrelated member changed.

Require zero unexpected paths/hunks.

Emit:

`RELEASE 1.10 WP02 PATH OWNERSHIP: PASS`

---

# Phase 15 — Acceptance matrix

Evaluate every acceptance criterion from issue #243 and the updated execution plan individually.

For each:

- criterion;
- evidence;
- validation result;
- PASS/BLOCK.

Require all PASS.

Emit:

`RELEASE 1.10 WP02 ACCEPTANCE: PASS`

---

# Phase 16 — Mutation accounting

Report separately:

## Pre-existing preserved artifacts
Including:

- `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
- Luna reconciliation edits to Release 1.10 planning docs
- any unrelated pre-entry residue.

## WP02 repository mutations
Exact added/modified paths.

Required markers:

`RELEASE 1.10 WP02 REPOSITORY MUTATIONS: ACCEPTED WP02 PATHS ONLY`

`RELEASE 1.10 WP02 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP02 GITHUB MUTATIONS: ZERO`

Do not stage/commit/push.

---

# Phase 17 — Next authority

On PASS, next:

**Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority — GPT-5.6 Terra**

Do not execute WP03.

Do not close #243.
Do not change Project status.
Do not commit WP02 under this authority.

---

# Required final report

Report:

1. model assignment;
2. entry baseline/residue;
3. reconciliation read-back;
4. baseline validation;
5. exact implementation;
6. activity/metric contract;
7. focused tests;
8. BCL/package audit;
9. architecture/no-bypass;
10. security/cardinality;
11. focused validation counts;
12. behavior preservation;
13. WP03 handoff;
14. exact diff/path audit;
15. acceptance matrix;
16. mutation accounting;
17. exact next authority.

---

# Success markers

`RELEASE 1.10 WP02 CONTRACT RECONCILIATION: PASS`

`RELEASE 1.10 WP02 APPLICATION PIPELINE BOUNDARIES: PASS`

`RELEASE 1.10 WP02 APPLICATION OBSERVABILITY API: BCL-ONLY ENFORCED`

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

- updated Luna reconciliation is absent/inconsistent;
- any required implementation path lies outside the exact allowlist;
- any non-authorized symbol/hunk must change;
- an Application package is required;
- WP02 would absorb WP03+ scope;
- architecture direction is violated;
- telemetry changes functional behavior;
- security/cardinality rules fail;
- focused tests/validation fail due to WP02;
- unexpected repository paths/hunks appear;
- Git/GitHub mutation occurs.

Report exact evidence and the smallest reconciliation needed.

Terminal:

`RELEASE 1.10 WP02 — APPLICATION PIPELINE OBSERVABILITY CONTRACT AUTHORITY BLOCKED`
