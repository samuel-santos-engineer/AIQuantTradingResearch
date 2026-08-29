# Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority V2 — Resumption Authority

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, path ownership, and governance authority.
- **GPT-5.6 Terra** — PRIMARY execution authority for resuming the already-authorized WP03 V2 implementation, validation, mutation accounting, and approved post-acceptance GitHub lifecycle completion.
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

This is a continuation/resumption of the SAME WP03 V2 implementation authority.

It is NOT:

- a new work package;
- a new architecture decision;
- a Luna reconciliation;
- a rollback authority;
- a WP04 authority.

The frozen WP03 Luna contract remains authoritative.

---

# Prior V2 blocked state

The previous WP03 V2 execution ended with:

`RELEASE 1.10 WP03 — INFRASTRUCTURE PROVIDER, PERSISTENCE & FAILURE INSTRUMENTATION AUTHORITY V2 BLOCKED`

Accepted evidence from that attempt:

- focused Infrastructure baseline: **23/23 passed**
- Application: **131/131 passed**
- Architecture: **21/21 passed**
- Infrastructure build: **0 warnings / 0 errors**
- a partial in-scope BCL-only telemetry identity was added in `SqliteHistoricalObservationStore.cs`
- the partial implementation compiled cleanly
- the three authorized method bodies were not all completed
- the two required test files were not completed/validated
- `RELEASE 1.10 WP03 ACCEPTANCE: PASS` was not reached
- Git mutations: ZERO
- GitHub mutations: ZERO
- #244 remains Open / Backlog.

This resumption MUST preserve valid partial in-scope work and continue from it.

Do NOT discard, reset, clean, or recreate accepted partial work merely because the prior run blocked.

---

# Canonical contract

Read before mutation:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. issue #244
6. accepted WP02 implementation/tests
7. current partial WP03 worktree implementation
8. exact production/test files frozen by Luna.

The reconciled planning artifacts outrank any older ambiguous WP03 text.

---

# Frozen production targets

Only these production methods are authorized:

1. `SqliteHistoricalObservationStore.Retrieve(string target)`
2. `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
3. `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Exact paths must come from the reconciled manifest.

---

# Explicitly forbidden production targets

Do NOT modify/instrument:

- `SqliteDatasetCatalog`
- `SqliteHistoricalObservationStore.Persist(...)`

Do not expand WP03 to other Infrastructure symbols.

---

# Frozen tests

Only the existing exact paths for:

- `SqlitePersistenceTests.cs`
- `SqliteDatasetTests.cs`

may receive WP03-focused test mutations.

No new test file.

---

# Frozen API/dependency contract

WP03 remains BCL-only:

- `System.Diagnostics`
- `System.Diagnostics.Metrics`

Infrastructure source:

`AIQuantTradingResearch.Infrastructure`

Infrastructure meter:

`AIQuantTradingResearch.Infrastructure`

Activities:

- `provider.operation`
- `persistence.operation`

No:

- helper file;
- `.csproj` mutation;
- package mutation;
- OpenTelemetry SDK/exporter package;
- schema/migration mutation;
- Python dependency mutation.

Use the exact metric names/types/units, bounded attributes, and bounded failure categories already frozen in the reconciled execution plan/manifest.

Do not invent replacements.

---

# WP02 topology contract

Preserve WP02 unchanged.

Canonical historical topology must be:

`pipeline.execute`
→ WP02 `HistoricalObservationRetrieval`
→ WP03 `provider.operation`

WP03 Infrastructure activities inherit ambient `Activity.Current`.

Infrastructure instrumentation measures only its real Infrastructure operation.

No duplicated WP02 stage interval.

---

# Resumption mutation policy

## Preserve

Preserve:

- all pre-existing Release 1.10 planning/WP01/WP02 worktree changes;
- Luna WP03 reconciliation changes;
- valid partial WP03 V2 changes from the blocked run.

## Do not

Do not:

- reset;
- clean;
- checkout away;
- revert valid in-scope partial work;
- stage;
- commit;
- push;
- branch;
- merge;
- rebase;
- tag.

Git mutations remain ZERO.

GitHub mutations remain ZERO until WP03 acceptance PASS.

---

# Phase 0 — Resumption entry audit

Before new mutation:

1. inspect `git status --short`;
2. identify all pre-existing Release 1.10 residue;
3. isolate current partial WP03 V2 delta;
4. inspect `SqliteHistoricalObservationStore.cs`;
5. prove the partial telemetry identity is within the Luna allowlist;
6. inspect whether either other production file or either focused test file already has partial WP03 work;
7. inspect #244 state without mutation;
8. inspect milestone #59 and #245–#249 without mutation.

Expected:

- #244 Open / Backlog / Release=1.10 / milestone #59;
- milestone #59 Open;
- #245–#249 unchanged;
- no Git mutation from previous run.

Emit:

`RELEASE 1.10 WP03 V2 RESUMPTION ENTRY AUDIT: PASS`

---

# Phase 1 — Partial-work reconciliation

Compare the existing partial WP03 delta against the frozen Luna contract.

For every existing WP03 hunk classify:

- `KEEP — CONTRACT-COMPLIANT`
- `REPAIR — IN-SCOPE`
- `REMOVE — CONTRACT-VIOLATING`

Removal is authorized only for a WP03 hunk created by the prior V2 attempt that demonstrably violates the frozen contract.

Do not remove unrelated/pre-existing work.

Specifically verify the partial BCL telemetry identity in `SqliteHistoricalObservationStore.cs`:

- source identity exact;
- meter identity exact;
- BCL-only;
- no helper/package/project change;
- no forbidden method instrumentation;
- no security/cardinality violation.

Emit:

`RELEASE 1.10 WP03 V2 PARTIAL-WORK RECONCILIATION: PASS`

---

# Phase 2 — Baseline reuse and delta baseline

The prior baseline evidence is accepted as historical evidence, but because the worktree now contains partial WP03 implementation, run a concise delta baseline before continuing.

At minimum:

- affected Infrastructure build;
- currently affected focused Infrastructure tests;
- process/listener residue check.

If these fail because of the partial WP03 work, repair only within authorized WP03 scope before proceeding.

Do not rerun broad suites unnecessarily at this phase; full affected validation is mandatory later.

Emit:

`RELEASE 1.10 WP03 V2 RESUMPTION DELTA BASELINE: PASS`

---

# Phase 3 — Complete telemetry identity

Finish/reconcile the existing in-scope Infrastructure source/meter identity exactly as frozen.

Require:

- ActivitySource = `AIQuantTradingResearch.Infrastructure`
- Meter = `AIQuantTradingResearch.Infrastructure`
- no new helper file
- no package/project mutation
- stable static/shared ownership exactly as reconciled
- no-listener behavior preserves functionality.

Emit:

`RELEASE 1.10 WP03 INFRASTRUCTURE OBSERVABILITY API: BCL-ONLY ENFORCED`

`RELEASE 1.10 WP03 INFRASTRUCTURE SOURCE/METER: PASS`

---

# Phase 4 — Complete provider instrumentation

Complete only:

`SqliteHistoricalObservationStore.Retrieve(string target)`

Implement exact reconciled:

- `provider.operation`;
- provider operation metric;
- provider duration metric in `ms`;
- provider failure metric;
- bounded operation/provider/outcome/failure attributes;
- success/failure activity semantics;
- sanitized failure behavior;
- unchanged exception propagation.

Do NOT modify/instrument:

`SqliteHistoricalObservationStore.Persist(...)`

Emit:

`RELEASE 1.10 WP03 PROVIDER/RETRIEVAL INSTRUMENTATION: PASS`

---

# Phase 5 — Complete snapshot store instrumentation

Complete only:

`SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`

Implement exact reconciled:

- `persistence.operation`;
- persistence operation metric;
- persistence duration metric in `ms`;
- persistence failure metric;
- exact bounded operation kind;
- exact bounded attributes/failure categories;
- unchanged SQLite transaction/write behavior;
- schema v4 unchanged;
- unchanged exception propagation.

Emit:

`RELEASE 1.10 WP03 SNAPSHOT STORE INSTRUMENTATION: PASS`

---

# Phase 6 — Complete snapshot retrieval instrumentation

Complete only:

`SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Implement exact reconciled:

- `persistence.operation`;
- persistence operation metric;
- persistence duration metric in `ms`;
- persistence failure metric;
- exact bounded retrieval operation kind;
- exact bounded attributes/failure categories;
- unchanged result/not-found semantics;
- unchanged exception propagation.

Do NOT instrument/modify `SqliteDatasetCatalog`.

Emit:

`RELEASE 1.10 WP03 SNAPSHOT RETRIEVAL INSTRUMENTATION: PASS`

---

# Phase 7 — Complete focused listener tests

Modify only:

- exact `SqlitePersistenceTests.cs`
- exact `SqliteDatasetTests.cs`

Complete deterministic BCL listener coverage.

Use `ActivityListener`, `MeterListener`, or the exact BCL observation approach compatible with the frozen contract.

Cover:

- source identity;
- meter identity;
- `provider.operation`;
- `persistence.operation`;
- correct parent;
- success;
- failure;
- operation counts;
- durations;
- failure counts;
- exact instrument types;
- exact units;
- bounded attributes;
- sanitized failure category;
- exception propagation;
- no-listener behavior;
- no duplicate catalog telemetry;
- no instrumentation of historical `Persist(...)`;
- schema/functional preservation where relevant.

Report exact focused test count.

Emit:

`RELEASE 1.10 WP03 FOCUSED LISTENER TESTS: PASS`

---

# Phase 8 — Topology proof

Prove using deterministic listener evidence:

`pipeline.execute`
→ WP02 `HistoricalObservationRetrieval`
→ WP03 `provider.operation`

For snapshot store/retrieve, prove the actual ambient parent behavior frozen by Luna/current call graph.

Require:

- correct trace/parent relationship where ambient activity exists;
- no replacement of WP02 stage;
- no duplicated semantic duration;
- no catalog duplicate span.

Emit:

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

---

# Phase 9 — Failure semantics proof

Exercise deterministic failure paths available in the frozen tests.

Require:

- failure recorded once by the owning Infrastructure boundary;
- exact bounded failure category;
- activity failure status;
- failure metric exactly once per failed owning operation;
- no arbitrary exception message metric labels;
- original exception propagates;
- no failure converted to success;
- no duplicate delegating-layer failure telemetry.

Emit:

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

---

# Phase 10 — Metric/cardinality proof

Using deterministic meter observation, prove:

- exact frozen metric names;
- exact instrument types;
- exact units;
- operation metric behavior;
- duration in `ms`;
- failure metric behavior;
- exact bounded attribute vocabulary;
- no uncontrolled target/symbol cardinality;
- no GUID/timestamp/path cardinality;
- no raw exception-message dimension;
- no duplicate metric ownership.

Emit:

`RELEASE 1.10 WP03 METRIC/CARDINALITY CONTRACT: PASS`

---

# Phase 11 — Dependency/package audit

Inspect final WP03 diff.

Require:

- BCL only;
- no `.csproj` change;
- no package change;
- no OpenTelemetry package;
- no helper file;
- no exporter/SDK/provider composition;
- no schema/migration change;
- no Python dependency change.

Emit:

`RELEASE 1.10 WP03 DEPENDENCY BOUNDARY: PASS — BCL ONLY`

---

# Phase 12 — Architecture/no-bypass validation

Run the relevant architecture/no-bypass suite.

Require:

- WP02 Application contract unchanged;
- Application does not acquire Infrastructure dependency;
- no direct Streamlit SQLite/provider access;
- no Worker ownership change;
- no parallel pipeline;
- schema v4 unchanged;
- JSON handoff unchanged.

Emit:

`RELEASE 1.10 WP03 ARCHITECTURE/NO-BYPASS: PASS`

---

# Phase 13 — Security validation

Run Gitleaks/security checks against every WP03 production/test path changed by the original V2 attempt plus this resumption.

Inspect telemetry manually.

Require no:

- secrets;
- credentials;
- connection strings;
- raw provider payload;
- raw SQL/business payload;
- uncontrolled exception labels;
- high-cardinality dimensions.

Emit:

`RELEASE 1.10 WP03 TELEMETRY SECURITY: PASS`

---

# Phase 14 — Full affected validation

After implementation is complete, run the full affected validation required by WP03.

At minimum:

1. Infrastructure build;
2. focused listener tests;
3. full relevant Infrastructure tests;
4. Application tests;
5. architecture/no-bypass tests;
6. security scan;
7. process/listener residue check.

Report exact:

- test counts;
- warnings;
- errors;
- failures/skips if any.

The prior baseline counts are not substitutes for final validation.

---

# Phase 15 — Functional preservation

Prove telemetry did not alter:

- historical retrieval result;
- snapshot store result;
- snapshot retrieval/not-found behavior;
- database contents;
- transaction semantics;
- exception propagation;
- deterministic/replay provenance;
- SQLite schema v4;
- canonical JSON handoff;
- Worker behavior;
- Streamlit behavior.

Emit:

`RELEASE 1.10 WP03 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

---

# Phase 16 — Forbidden-target audit

Explicitly prove final WP03 implementation did NOT modify/instrument:

- `SqliteDatasetCatalog`
- `SqliteHistoricalObservationStore.Persist(...)`

Also prove:

- no new helper file;
- no unrelated Infrastructure production symbol;
- no unauthorized test path;
- no project/package file.

Emit:

`RELEASE 1.10 WP03 FORBIDDEN TARGET AUDIT: PASS`

---

# Phase 17 — Exact path/hunk audit

Compare final worktree against the original pre-WP03 baseline where determinable and against the resumption Phase 0 inventory.

Separate:

1. pre-existing Release 1.10 planning/WP01/WP02 residue;
2. Luna WP03 reconciliation planning changes;
3. partial WP03 V2 changes from the blocked attempt;
4. new WP03 resumption changes.

The combined WP03 V2 + resumption delta must remain entirely within the frozen production/test allowlist.

For production files, only:

- the three authorized methods;
- exact existing-class telemetry identity members frozen by Luna.

For tests, only the two frozen existing test files.

Emit:

`RELEASE 1.10 WP03 PATH OWNERSHIP: PASS`

---

# Phase 18 — WP04 handoff

Freeze/report what WP04 inherits without redesign:

- ActivitySource `AIQuantTradingResearch.Infrastructure`;
- Meter `AIQuantTradingResearch.Infrastructure`;
- `provider.operation`;
- `persistence.operation`;
- exact frozen metrics/types/units;
- exact bounded attributes;
- exact failure categories;
- ambient topology;
- BCL-only WP03 dependency state.

WP04 owns Worker/interop lifecycle and exporter isolation.

Emit:

`RELEASE 1.10 WP03 DOWNSTREAM HANDOFF: PASS — WP04 READY`

---

# Phase 19 — Acceptance matrix

Evaluate every WP03 criterion from:

- #244;
- Release 1.10 execution plan;
- file manifest;
- Luna reconciliation;
- WP03 V2 authority;
- this resumption authority.

Report each criterion with evidence and PASS/BLOCK.

Require all PASS.

Only then emit:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

This is the hard GitHub lifecycle gate.

---

# Phase 20 — GitHub lifecycle completion

Only after `RELEASE 1.10 WP03 ACCEPTANCE: PASS`:

Re-read #244 and its Project #2 item.

Require:

- issue identity #244;
- milestone #59;
- Release=1.10;
- exactly one Project #2 item;
- current state Open/Backlog or idempotently Closed/Done.

Then:

1. close #244 if Open;
2. set its unique Project #2 item Status to `Done` if needed.

Preserve:

- Release=1.10;
- milestone #59;
- milestone #59 remains Open;
- #245–#249 unchanged.

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
- milestone #59 Open;
- #245–#249 unchanged unless a separate legitimate authority changed them.

Report actual milestone open/closed counts.

Emit:

`RELEASE 1.10 WP03 GITHUB COMPLETION POST-VERIFY: PASS`

---

# Phase 22 — Mutation accounting

Report exact combined WP03 mutation ledger, including partial V2 work and resumption work.

## Repository

Only accepted frozen WP03 production/test paths.

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

Only after full completion:

**Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Authority — GPT-5.6 Terra**

Do not execute WP04 here.

---

# Required success markers

`RELEASE 1.10 WP03 V2 RESUMPTION ENTRY AUDIT: PASS`

`RELEASE 1.10 WP03 V2 PARTIAL-WORK RECONCILIATION: PASS`

`RELEASE 1.10 WP03 V2 RESUMPTION DELTA BASELINE: PASS`

`RELEASE 1.10 WP03 INFRASTRUCTURE OBSERVABILITY API: BCL-ONLY ENFORCED`

`RELEASE 1.10 WP03 INFRASTRUCTURE SOURCE/METER: PASS`

`RELEASE 1.10 WP03 PROVIDER/RETRIEVAL INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 SNAPSHOT STORE INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 SNAPSHOT RETRIEVAL INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 FOCUSED LISTENER TESTS: PASS`

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

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

BLOCK if:

- existing partial WP03 work cannot be reconciled within the frozen contract;
- a new path/symbol/test/package/helper is required;
- forbidden targets would need mutation;
- WP02 requires redesign;
- BCL-only implementation cannot satisfy the frozen contract;
- listener/topology proof fails;
- telemetry duplicates semantic ownership;
- security/cardinality fails;
- final affected validation fails due to WP03;
- functional behavior changes;
- unexpected paths/hunks occur.

If blocked before:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

then:

- GitHub completion mutations MUST be ZERO;
- #244 remains Open/Backlog;
- WP04 MUST NOT begin.

Preserve valid in-scope partial work for another resumption unless it is itself the cause of the block.

Terminal:

`RELEASE 1.10 WP03 — INFRASTRUCTURE PROVIDER, PERSISTENCE & FAILURE INSTRUMENTATION AUTHORITY V2 BLOCKED`
