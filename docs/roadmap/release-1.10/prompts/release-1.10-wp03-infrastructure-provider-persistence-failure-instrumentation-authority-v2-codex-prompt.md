# Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority v2

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, path ownership, and governance authority.
- **GPT-5.6 Terra** — PRIMARY implementation/execution authority for WP03 implementation, validation, exact mutation accounting, and approved post-acceptance GitHub lifecycle completion.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and non-authoritative review; Sol does not replace Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

---

# Authority identity

Release: **1.10**

Work package:

**WP03 — Infrastructure Provider, Persistence & Failure Instrumentation**

Issue: **#244**

Milestone: **#59**

Project: **#2**

This v2 authority supersedes the blocked first Terra WP03 authority.

The intervening GPT-5.6 Luna reconciliation passed:

`RELEASE 1.10 WP03 CONTRACT/PATH RECONCILIATION: PASS`

`RELEASE 1.10 WP03 MATERIALIZATION SIMULATION: PASS — TERRA-READY`

Terra MUST implement the reconciled contract exactly and MUST NOT reopen architectural/path/package decisions.

---

# Canonical inputs

Read before mutation:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. issue #244
6. accepted WP02 implementation and tests
7. the exact WP03 production/test files frozen by the reconciled manifest.

If this prompt conflicts with a more precise reconciled execution-plan/manifest detail, use the reconciled planning artifact and report the discrepancy before mutation.

No discretionary scope expansion.

---

# Reconciled production contract

The following exact Infrastructure symbols are WP03-authorized:

## Provider/retrieval

`SqliteHistoricalObservationStore.Retrieve(string target)`

Responsibility:

- truthful provider/retrieval Infrastructure instrumentation.

## Snapshot persistence

`SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`

Responsibility:

- truthful snapshot persistence instrumentation.

## Snapshot retrieval

`SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Responsibility:

- truthful snapshot persistence-retrieval instrumentation.

---

# Explicitly forbidden production symbols

Do NOT instrument or modify for WP03:

- `SqliteDatasetCatalog`
- `SqliteHistoricalObservationStore.Persist(...)`

Reason:

- catalog delegates to the snapshot store and would duplicate semantic ownership;
- historical observation persistence was explicitly excluded by Luna reconciliation.

No other Infrastructure production symbol is writable unless the reconciled manifest explicitly names it.

---

# Reconciled test ownership

Focused WP03 tests are limited to the existing files:

- `SqlitePersistenceTests.cs`
- `SqliteDatasetTests.cs`

Use the exact repository paths frozen in `RELEASE_1.10_FILE_MANIFEST.md`.

No new test file.

No broad test-directory ownership.

---

# Reconciled API/dependency contract

WP03 is:

**BCL-only**

Allowed APIs:

- `System.Diagnostics`
- `System.Diagnostics.Metrics`

Prohibited:

- OpenTelemetry package additions;
- `.csproj` mutation;
- package-version mutation;
- exporter packages;
- SDK/provider composition;
- schema/migration packages;
- Python package mutation.

Required marker:

`RELEASE 1.10 WP03 INFRASTRUCTURE OBSERVABILITY API: BCL-ONLY ENFORCED`

---

# Reconciled Infrastructure telemetry identity

Infrastructure ActivitySource:

`AIQuantTradingResearch.Infrastructure`

Infrastructure Meter:

`AIQuantTradingResearch.Infrastructure`

No new helper file.

Implement ownership in the exact existing production path/symbol location frozen by the reconciled manifest.

Do not introduce a generic telemetry framework.

Do not change WP02 Application source/meter.

---

# Reconciled activity contract

Canonical WP03 activity names:

- `provider.operation`
- `persistence.operation`

## `provider.operation`

Owner:

`SqliteHistoricalObservationStore.Retrieve(string target)`

Measured interval:

Only the actual Infrastructure provider/retrieval operation owned by that method.

Parent:

Ambient `Activity.Current`, which during canonical WP02 execution must be the WP02 `HistoricalObservationRetrieval` activity.

Do not time the full WP02 stage separately.

## `persistence.operation`

Owners:

- `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
- `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Measured interval:

Only the actual Infrastructure snapshot persistence/retrieval operation performed by the owning method.

Parent:

Ambient `Activity.Current` when present.

Do not add instrumentation to `SqliteDatasetCatalog` merely to create an additional parent/child layer.

---

# Reconciled metric contract

Implement only the exact metric names/types/units frozen in the reconciled execution plan/manifest.

Semantic categories are:

## Provider

- bounded provider operation count;
- provider duration in `ms`;
- provider failure count.

## Persistence

- bounded persistence operation count;
- persistence duration in `ms`;
- persistence failure count.

Do not invent alternate names.

Do not invent histogram/counter types if the reconciled contract already freezes them.

Do not record duplicate metrics at delegating layers.

---

# Bounded attributes/failures

Use only the finite attributes and failure categories frozen by Luna.

Prohibited telemetry data includes:

- connection strings;
- credentials/tokens/secrets;
- raw provider payloads;
- SQL containing business/user data;
- arbitrary exception messages as metric labels;
- stack traces as metric dimensions;
- GUID/request IDs as metric dimensions;
- timestamps as metric dimensions;
- raw filesystem paths;
- uncontrolled symbols/tickers;
- arbitrary dynamic operation names.

Failure observations must be sanitized and finite.

Original exceptions must continue to propagate unchanged.

---

# Architecture invariants

Preserve:

- .NET Application pipeline ownership;
- Infrastructure implementation ownership;
- WP02 observability semantics;
- SQLite schema v4;
- canonical JSON handoff;
- deterministic/replay/simulated provenance;
- Worker/Streamlit independence;
- Release 1.8 JSON-over-stdio boundary;
- no direct Streamlit SQLite/provider access;
- no parallel pipeline.

Do not implement WP04 exporter/lifecycle concerns.

---

# Existing worktree preservation

The worktree already contains accepted Release 1.10 planning/WP01/WP02/reconciliation residue.

Terra MUST:

1. inventory it before mutation;
2. preserve it;
3. distinguish it from WP03 v2 mutations;
4. not stage/commit/reset/clean it;
5. not treat it as WP03 scope drift.

---

# Git/GitHub policy

## During implementation

Git mutations: ZERO.

Do not stage, commit, push, branch, merge, rebase, tag, reset, or clean.

GitHub mutations: ZERO until WP03 acceptance PASS.

## After acceptance

Only after:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

Terra is authorized to:

1. close #244 if Open;
2. set its unique Project #2 item Status to `Done` if not already Done.

Preserve:

- Release=1.10;
- milestone #59;
- milestone #59 remains Open;
- #245–#249 unchanged.

Maximum GitHub mutations: 2.

---

# Phase 0 — Entry audit

Record:

- repo/branch/HEAD;
- remote main if available read-only;
- `git status --short`;
- staged/untracked state;
- pre-existing Release 1.10 residue;
- exact WP03 production/test paths from reconciled manifest;
- #244 state/milestone/Release/Project Status;
- milestone #59 state/counts;
- #245–#249 state/status.

Expected #244:

- Open;
- Backlog;
- Release=1.10;
- milestone #59;
- one unique Project #2 item.

Emit:

`RELEASE 1.10 WP03 V2 ENTRY BASELINE: ACCEPTED`

---

# Phase 1 — Deterministic contract read-back

Before mutation, print the exact resolved contract from the current planning artifacts.

Require deterministic answers for:

- three authorized production methods;
- two forbidden production targets;
- exact two test files;
- source name;
- meter name;
- two activity names;
- exact metric names/types/units;
- bounded attributes;
- bounded failure categories;
- BCL-only decision;
- zero package/project/schema/helper-file authority;
- WP02 parent topology;
- WP04 handoff.

Emit:

`RELEASE 1.10 WP03 V2 CONTRACT READ-BACK: PASS`

If any current artifact is inconsistent:
BLOCK before mutation.

---

# Phase 2 — Baseline validation

Run an attributable baseline before WP03 production mutation.

At minimum:

- affected Infrastructure build;
- existing `SqlitePersistenceTests`;
- existing `SqliteDatasetTests`;
- relevant broader Infrastructure test project(s);
- Application tests needed to preserve WP02→WP03 integration;
- architecture/no-bypass tests;
- process/listener residue check.

Record exact counts/warnings/errors.

If a pre-existing failure prevents attribution:
BLOCK.

Emit:

`RELEASE 1.10 WP03 V2 BASELINE: PASS`

---

# Phase 3 — Implement Infrastructure telemetry identity

Within the exact reconciled authorized production path(s):

- expose/use ActivitySource `AIQuantTradingResearch.Infrastructure`;
- expose/use Meter `AIQuantTradingResearch.Infrastructure`;
- use BCL only;
- do not add a helper file;
- do not alter project/package files.

Ensure no-listener/no-meter-consumer execution preserves behavior.

Emit:

`RELEASE 1.10 WP03 INFRASTRUCTURE SOURCE/METER: PASS`

---

# Phase 4 — Implement provider/retrieval instrumentation

Modify only:

`SqliteHistoricalObservationStore.Retrieve(string target)`

Implement:

- `provider.operation`;
- exact provider metrics from reconciled contract;
- exact bounded attributes;
- exact bounded failure category;
- duration in milliseconds as frozen;
- success/failure semantics;
- exception propagation unchanged.

Parenting:

Use normal ActivitySource semantics so the activity inherits ambient `Activity.Current`.

Under canonical WP02 pipeline execution prove:

WP02 `HistoricalObservationRetrieval`
→ WP03 `provider.operation`

Do not modify/instrument `SqliteHistoricalObservationStore.Persist(...)`.

Emit:

`RELEASE 1.10 WP03 PROVIDER/RETRIEVAL INSTRUMENTATION: PASS`

---

# Phase 5 — Implement snapshot persistence instrumentation

Modify only:

`SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`

Implement:

- `persistence.operation`;
- exact persistence metrics;
- exact bounded operation attribute identifying the frozen operation kind;
- exact bounded failure category;
- truthful store interval;
- original transaction/database behavior;
- original exception behavior.

Schema v4 unchanged.

Emit:

`RELEASE 1.10 WP03 SNAPSHOT STORE INSTRUMENTATION: PASS`

---

# Phase 6 — Implement snapshot retrieval instrumentation

Modify only:

`SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Implement:

- `persistence.operation`;
- exact persistence metrics;
- exact bounded retrieval operation kind;
- exact bounded failure category;
- truthful retrieval interval;
- original result/not-found semantics;
- original exception behavior.

Do not instrument `SqliteDatasetCatalog`.

Emit:

`RELEASE 1.10 WP03 SNAPSHOT RETRIEVAL INSTRUMENTATION: PASS`

---

# Phase 7 — Failure semantics

Test real failure paths already supported by deterministic fixtures.

Require:

- activity failure state/status as frozen;
- failure metric increments exactly once at the owning boundary;
- sanitized bounded failure category;
- no raw exception message as metric dimension;
- original exception propagates;
- no false success;
- no duplicate catalog/store failure telemetry.

Emit:

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

---

# Phase 8 — Focused tests

Modify only the exact reconciled paths for:

- `SqlitePersistenceTests.cs`
- `SqliteDatasetTests.cs`

Add focused deterministic coverage for the frozen contract.

Cover as applicable:

- source identity;
- meter identity;
- `provider.operation`;
- `persistence.operation`;
- ActivityListener observation;
- MeterListener observation;
- correct ambient parent;
- success;
- failure;
- metrics;
- durations;
- bounded attributes;
- sanitized failure category;
- exception propagation;
- no-listener behavior;
- schema/functional preservation;
- forbidden candidate non-duplication.

No new test file.

Emit exact focused test count.

---

# Phase 9 — Topology validation

Prove the canonical topology.

At minimum:

`pipeline.execute`
→ WP02 `HistoricalObservationRetrieval`
→ WP03 `provider.operation`

For snapshot store/retrieve, prove the actual ambient parent behavior dictated by the current call graph and reconciled contract.

Require:

- Infrastructure activities do not replace WP02 stages;
- no two spans claim the same semantic owner interval;
- no `SqliteDatasetCatalog` duplicate activity.

Emit:

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

---

# Phase 10 — Metric/cardinality validation

Use MeterListener or equivalent deterministic BCL test observation.

Prove:

- exact metric names;
- exact instrument types;
- exact units;
- success recording;
- failure recording;
- duration recording;
- bounded attributes only;
- no symbol/ticker cardinality leakage;
- no exception-message cardinality;
- no duplicate delegating-layer metrics.

Emit:

`RELEASE 1.10 WP03 METRIC/CARDINALITY CONTRACT: PASS`

---

# Phase 11 — Dependency/package audit

Inspect diff and project/package state.

Require:

- no `.csproj` mutation;
- no package mutation;
- no OpenTelemetry package;
- no exporter/SDK/provider composition;
- no helper file;
- no Python dependency change.

Emit:

`RELEASE 1.10 WP03 DEPENDENCY BOUNDARY: PASS — BCL ONLY`

---

# Phase 12 — Architecture/no-bypass validation

Run relevant architecture tests.

Require:

- WP02 Application contract unchanged;
- no dependency inversion violation;
- no Streamlit database/provider access;
- no Worker ownership change;
- no parallel pipeline;
- schema v4 unchanged;
- JSON handoff unchanged.

Emit:

`RELEASE 1.10 WP03 ARCHITECTURE/NO-BYPASS: PASS`

---

# Phase 13 — Security validation

Run Gitleaks/security scanning on every WP03-mutated production/test path.

Inspect telemetry manually for forbidden data.

Require:

- no secret leakage;
- no connection-string telemetry;
- no raw provider payload;
- no raw SQL/business payload;
- no uncontrolled exception labels;
- no high-cardinality dimensions.

Emit:

`RELEASE 1.10 WP03 TELEMETRY SECURITY: PASS`

---

# Phase 14 — Affected validation

Run all required affected suites after implementation.

At minimum:

1. affected Infrastructure build;
2. focused WP03 tests;
3. full relevant Infrastructure tests;
4. Application tests needed for WP02 integration;
5. architecture/no-bypass tests;
6. Gitleaks/security;
7. process/listener residue.

Report exact test counts, warnings, and errors.

Do not claim WP08 full-release validation.

---

# Phase 15 — Functional preservation

Prove WP03 observability changes do not alter:

- historical retrieval result;
- snapshot store result;
- snapshot retrieval/not-found result;
- database contents;
- transaction behavior;
- exception propagation;
- deterministic/replay provenance;
- schema v4;
- canonical JSON handoff;
- Worker/Streamlit behavior.

Emit:

`RELEASE 1.10 WP03 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

---

# Phase 16 — Forbidden-target audit

Explicitly inspect diff/call sites and prove:

- `SqliteDatasetCatalog`: not modified/instrumented;
- `SqliteHistoricalObservationStore.Persist(...)`: not modified/instrumented;
- no new helper file;
- no unrelated Infrastructure symbol mutation.

Emit:

`RELEASE 1.10 WP03 FORBIDDEN TARGET AUDIT: PASS`

---

# Phase 17 — Exact path/hunk ownership

Compare final worktree against Phase 0.

Separate:

## Pre-existing preserved residue
Planning/WP01/WP02/Luna reconciliation.

## WP03 v2 mutations
Only exact production/test paths from the reconciled manifest.

For production files, verify only the three authorized methods plus exact existing-class telemetry identity members if explicitly frozen by manifest.

For test files, verify only WP03-focused additions.

Zero unexpected paths.

Emit:

`RELEASE 1.10 WP03 PATH OWNERSHIP: PASS`

---

# Phase 18 — WP04 handoff

Report exact frozen outputs WP04 inherits:

- ActivitySource `AIQuantTradingResearch.Infrastructure`;
- Meter `AIQuantTradingResearch.Infrastructure`;
- activities `provider.operation`, `persistence.operation`;
- exact metrics;
- exact attributes;
- exact failure categories;
- ambient parenting;
- BCL-only WP03 dependency state.

WP04 owns lifecycle/exporter isolation as planned and must not redesign WP03 telemetry.

Emit:

`RELEASE 1.10 WP03 DOWNSTREAM HANDOFF: PASS — WP04 READY`

---

# Phase 19 — Acceptance matrix

Evaluate every WP03 criterion from:

- issue #244;
- execution plan;
- file manifest;
- Luna reconciliation.

For each report evidence and PASS/BLOCK.

All must PASS.

Emit:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

No GitHub completion before this exact marker.

---

# Phase 20 — GitHub lifecycle completion

After acceptance only:

Re-read #244.

Require:

- correct issue identity;
- milestone #59;
- Release=1.10;
- unique Project #2 item;
- Open/Backlog or idempotently already Closed/Done.

Then:

1. close #244 if Open;
2. set unique Project #2 Status to `Done` if needed.

Do not change:

- issue body/title/labels unless platform closure itself requires metadata;
- Release field;
- milestone assignment;
- milestone #59 state;
- #245–#249.

Maximum GitHub mutations: 2.

Emit:

`RELEASE 1.10 WP03 GITHUB WORK-PACKAGE COMPLETION: PASS`

---

# Phase 21 — Post-completion verification

Re-read GitHub.

Require:

- #242 Closed/Done;
- #243 Closed/Done;
- #244 Closed/Done;
- #244 Release=1.10;
- #244 milestone #59;
- milestone #59 remains Open;
- #245–#249 unchanged Open/Backlog unless an independent authority has legitimately changed them.

Report current milestone counts from GitHub; do not invent them.

Emit:

`RELEASE 1.10 WP03 GITHUB COMPLETION POST-VERIFY: PASS`

---

# Phase 22 — Mutation accounting

Report exact mutation ledger.

## Repository
Only accepted WP03 production/test paths.

## Git
ZERO.

## GitHub
Only:
- #244 close;
- #244 Project Status→Done.

Maximum 2.

Required markers:

`RELEASE 1.10 WP03 REPOSITORY MUTATIONS: ACCEPTED WP03 PATHS ONLY`

`RELEASE 1.10 WP03 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

---

# Phase 23 — Next authority

On complete PASS:

**Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Authority — GPT-5.6 Terra**

Do not execute WP04 here.

---

# Required success markers

`RELEASE 1.10 WP03 V2 ENTRY BASELINE: ACCEPTED`

`RELEASE 1.10 WP03 V2 CONTRACT READ-BACK: PASS`

`RELEASE 1.10 WP03 V2 BASELINE: PASS`

`RELEASE 1.10 WP03 INFRASTRUCTURE OBSERVABILITY API: BCL-ONLY ENFORCED`

`RELEASE 1.10 WP03 INFRASTRUCTURE SOURCE/METER: PASS`

`RELEASE 1.10 WP03 PROVIDER/RETRIEVAL INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 SNAPSHOT STORE INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 SNAPSHOT RETRIEVAL INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

`RELEASE 1.10 WP03 METRIC/CARDINALITY CONTRACT: PASS`

`RELEASE 1.10 WP03 DEPENDENCY BOUNDARY: PASS — BCL ONLY`

`RELEASE 1.10 WP03 ARCHITECTURE/NO-BYPASS: PASS`

`RELEASE 1.10 WP03 TELEMETRY SECURITY: PASS`

`RELEASE 1.10 WP03 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

`RELEASE 1.10 WP03 FORBIDDEN TARGET AUDIT: PASS`

`RELEASE 1.10 WP03 PATH OWNERSHIP: PASS`

`RELEASE 1.10 WP03 DOWNSTREAM HANDOFF: PASS — WP04 READY`

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

`RELEASE 1.10 WP03 GITHUB WORK-PACKAGE COMPLETION: PASS`

`RELEASE 1.10 WP03 GITHUB COMPLETION POST-VERIFY: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

`RELEASE 1.10 WP03 REPOSITORY MUTATIONS: ACCEPTED WP03 PATHS ONLY`

`RELEASE 1.10 WP03 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

Terminal:

`RELEASE 1.10 WP03 — INFRASTRUCTURE PROVIDER, PERSISTENCE & FAILURE INSTRUMENTATION AUTHORITY V2 COMPLETE`

---

# Blocked outcome

BLOCK before mutation if the reconciled planning artifacts no longer provide deterministic names/types/units/attributes/failure categories/path ownership.

BLOCK after mutation if:

- any forbidden production target requires modification;
- a package/project/helper-file mutation becomes necessary;
- BCL-only implementation is not possible under the frozen contract;
- WP02 must be redesigned;
- telemetry duplicates semantic ownership;
- exception/business behavior changes;
- schema v4 changes;
- security/cardinality validation fails;
- affected tests fail due to WP03;
- unexpected path/hunk mutations occur.

If blocked before:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

then GitHub completion mutations MUST be ZERO and #244 remains Open/Backlog.

If a failure occurs after authorized GitHub completion, preserve correct completed state and report exact partial accounting; do not invent rollback authority.

Terminal:

`RELEASE 1.10 WP03 — INFRASTRUCTURE PROVIDER, PERSISTENCE & FAILURE INSTRUMENTATION AUTHORITY V2 BLOCKED`
