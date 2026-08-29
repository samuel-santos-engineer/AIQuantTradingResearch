# Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority V2 — Resumption 2

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, path ownership, and governance authority.
- **GPT-5.6 Terra** — PRIMARY execution authority for continuing the SAME WP03 V2 implementation through completion, validation, exact mutation accounting, and approved post-acceptance GitHub lifecycle completion.
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

This is **Resumption 2 of the SAME WP03 V2 authority**.

It does not create a new WP, redefine the Luna contract, authorize WP04, or authorize unrelated cleanup.

The objective is to finish the already-started, already-in-scope WP03 implementation.

---

# Accepted prior state

The preceding V2 and first resumption attempts established:

- Luna reconciliation remains authoritative and Terra-ready.
- Infrastructure baseline previously passed 23/23 focused tests.
- Application previously passed 131/131.
- Architecture previously passed 21/21.
- Infrastructure build previously passed with 0 warnings / 0 errors.
- current Infrastructure build remains clean.
- BCL-only telemetry identity is in-scope.
- additional partial WP03 work now exists only in the three frozen production methods.
- no forbidden production target was changed.
- no package/project/schema/migration mutation occurred.
- Git mutations remain ZERO.
- GitHub mutations remain ZERO.
- #244 remains Open / Backlog.
- WP04 has not started.

Do not redo completed contract reconciliation.

Do not discard valid partial implementation.

---

# Frozen production scope

Only these three production methods are authorized:

1. `SqliteHistoricalObservationStore.Retrieve(string target)`
2. `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
3. `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Exact paths are those frozen in the reconciled Release 1.10 file manifest.

Existing in-scope telemetry identity members in the authorized production class/path remain allowed exactly as reconciled.

---

# Frozen forbidden scope

Do NOT modify or instrument:

- `SqliteDatasetCatalog`
- `SqliteHistoricalObservationStore.Persist(...)`

Do NOT add:

- helper files;
- new test files;
- project/package changes;
- OpenTelemetry packages;
- exporter/SDK composition;
- SQLite schema changes;
- migrations;
- Python dependency changes.

---

# Frozen test scope

Only these existing test files may receive WP03 test changes:

- `SqlitePersistenceTests.cs`
- `SqliteDatasetTests.cs`

Use exact paths from `RELEASE_1.10_FILE_MANIFEST.md`.

---

# Frozen observability contract

BCL only:

- `System.Diagnostics`
- `System.Diagnostics.Metrics`

ActivitySource:

`AIQuantTradingResearch.Infrastructure`

Meter:

`AIQuantTradingResearch.Infrastructure`

Activities:

- `provider.operation`
- `persistence.operation`

Metrics:

Use the **exact names, instrument types, units, attributes, operation kinds, outcomes, and failure categories frozen in the reconciled execution plan/manifest**.

Duration unit:

`ms`

Do not invent alternate names or semantic values.

---

# Frozen topology

Infrastructure activities inherit ambient `Activity.Current`.

Canonical historical retrieval topology:

`pipeline.execute`
→ WP02 `HistoricalObservationRetrieval`
→ WP03 `provider.operation`

WP03 must not duplicate the WP02 stage interval.

Snapshot store/retrieve activities must reflect their truthful Infrastructure ownership and ambient parent.

No `SqliteDatasetCatalog` activity.

---

# Primary remaining work

This resumption is specifically required to finish:

1. consistent frozen outcome/failure metric recording on **all relevant return/exit paths** of the three authorized methods;
2. focused deterministic `ActivityListener` / `MeterListener` tests in the two authorized existing test files;
3. topology proof;
4. metric/cardinality proof;
5. failure/security proof;
6. full affected validation;
7. functional preservation;
8. exact path/hunk ownership;
9. WP03 acceptance;
10. only after acceptance, #244 closure + Project #2 Status Done.

Do not stop after merely compiling.

---

# Phase 0 — Resumption 2 entry audit

Before mutation:

- read the original V2 authority;
- read the first resumption authority;
- read reconciled execution plan and file manifest;
- inspect current `git status --short`;
- inspect all current WP03 hunks;
- verify current partial production work exists only in authorized production paths/methods;
- inspect the two authorized test files;
- inspect #244 read-only;
- inspect milestone #59 and #245–#249 read-only.

Classify current WP03 hunks:

- `KEEP — CONTRACT-COMPLIANT`
- `REPAIR — IN-SCOPE`
- `REMOVE — CONTRACT-VIOLATING`

Do not remove unrelated/pre-existing Release 1.10 residue.

Emit:

`RELEASE 1.10 WP03 V2 RESUMPTION 2 ENTRY AUDIT: PASS`

---

# Phase 1 — Return/exit-path matrix

Before editing further, enumerate every relevant exit path for each authorized method.

For each exit path identify:

- successful or failed;
- activity status/outcome;
- operation metric recording;
- duration recording;
- failure metric recording or not;
- bounded outcome attribute;
- bounded failure category if failed;
- exception propagation behavior;
- whether the method returns normally, returns a not-found/empty result, or throws.

Methods:

## Historical provider retrieval

`SqliteHistoricalObservationStore.Retrieve(string target)`

## Snapshot store

`SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`

## Snapshot retrieve

`SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

The matrix must prove no legitimate exit path silently misses the frozen operation/outcome/duration contract.

Emit:

`RELEASE 1.10 WP03 EXIT-PATH OBSERVABILITY MATRIX: FROZEN`

---

# Phase 2 — Complete all production exit paths

Repair/complete only the three authorized methods.

Requirements:

- operation metric exactly as frozen;
- duration exactly as frozen in milliseconds;
- failure metric only for frozen failure semantics;
- bounded success/not-found/failure outcomes exactly as reconciled;
- activity status exactly as reconciled;
- no double recording;
- no metrics omitted because of an early return;
- no exception swallowed or transformed;
- original return values unchanged;
- original DB/provider behavior unchanged.

Prefer structurally reliable recording (`try`/`catch`/`finally` or equivalent) only where it preserves the frozen semantics and avoids duplicate recording.

Do not refactor unrelated logic.

Emit:

`RELEASE 1.10 WP03 ALL AUTHORIZED EXIT PATHS: PASS`

---

# Phase 3 — Provider focused tests

In the authorized existing test file only, add deterministic BCL listener tests for:

`SqliteHistoricalObservationStore.Retrieve(string target)`

Prove:

- ActivitySource identity;
- Meter identity;
- `provider.operation`;
- correct operation attributes;
- successful retrieval outcome;
- applicable empty/not-found semantics if part of the frozen method contract;
- deterministic failure outcome;
- exact provider operation metric;
- exact provider duration metric and `ms` unit;
- exact provider failure metric;
- bounded failure category;
- exception propagation;
- no uncontrolled target/symbol metric dimension;
- no raw exception-message metric dimension;
- no-listener behavior;
- `Persist(...)` is not instrumented by WP03.

Emit:

`RELEASE 1.10 WP03 PROVIDER LISTENER TESTS: PASS`

---

# Phase 4 — Persistence focused tests

In the authorized existing test file only, add deterministic BCL listener tests for:

- `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
- `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Prove:

- `persistence.operation`;
- exact bounded operation kind for store;
- exact bounded operation kind for retrieve;
- successful store;
- successful retrieve;
- frozen not-found behavior;
- deterministic failure behavior where supported;
- exact persistence operation metric;
- exact persistence duration metric and `ms` unit;
- exact persistence failure metric;
- bounded failure category;
- exception propagation;
- no duplicate catalog instrumentation;
- no-listener behavior;
- schema/functional behavior unchanged.

Emit:

`RELEASE 1.10 WP03 PERSISTENCE LISTENER TESTS: PASS`

---

# Phase 5 — Focused test gate

Run the focused tests from the two authorized files.

Report exact:

- test count;
- pass/fail/skip count;
- warnings/errors if build occurs.

All must pass.

Emit:

`RELEASE 1.10 WP03 FOCUSED LISTENER TESTS: PASS`

---

# Phase 6 — Topology proof

Use deterministic ActivityListener evidence.

Prove the canonical path:

`pipeline.execute`
→ WP02 `HistoricalObservationRetrieval`
→ WP03 `provider.operation`

Verify:

- WP03 activity parent is ambient WP02 activity;
- trace relationship is preserved;
- WP03 does not create a competing root;
- WP03 does not measure the entire WP02 stage as a duplicate owner;
- snapshot activities use truthful ambient parenting;
- `SqliteDatasetCatalog` does not emit duplicate WP03 activity.

If the topology proof requires an unauthorized test-path mutation, BLOCK and report the exact conflict rather than expanding scope.

Emit:

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

---

# Phase 7 — Metric/cardinality proof

Use deterministic MeterListener evidence.

For every frozen WP03 instrument prove:

- exact name;
- exact type;
- exact unit;
- correct operation count;
- correct duration recording;
- correct failure count;
- exact bounded attributes;
- exact bounded operation kinds;
- exact bounded outcomes;
- exact bounded failure categories.

Explicitly prove absence of metric dimensions containing:

- arbitrary target/symbol;
- GUID/request IDs;
- timestamps;
- filesystem paths;
- connection strings;
- raw exception messages;
- raw SQL;
- payload data.

Emit:

`RELEASE 1.10 WP03 METRIC/CARDINALITY CONTRACT: PASS`

---

# Phase 8 — Failure semantics

For each deterministic failure fixture available within authorized scope prove:

- activity records frozen failure status/outcome;
- operation metric remains semantically correct;
- failure metric records exactly once;
- duration records consistently;
- bounded sanitized failure category only;
- original exception propagates;
- no false success;
- no duplicate failure metric from a delegating layer.

Emit:

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

---

# Phase 9 — Dependency and forbidden-target audit

Inspect diff.

Require:

- BCL only;
- no package/project mutation;
- no helper file;
- no schema/migration change;
- no OpenTelemetry SDK/exporter implementation;
- `SqliteDatasetCatalog` unchanged by WP03;
- `SqliteHistoricalObservationStore.Persist(...)` unchanged by WP03;
- no unrelated production symbol mutation.

Emit:

`RELEASE 1.10 WP03 DEPENDENCY BOUNDARY: PASS — BCL ONLY`

`RELEASE 1.10 WP03 FORBIDDEN TARGET AUDIT: PASS`

---

# Phase 10 — Security gate

Run Gitleaks/security scanning against every combined WP03 V2/resumption-mutated path.

Manually inspect telemetry.

Require no:

- secrets;
- credentials;
- connection strings;
- raw provider payload;
- raw SQL/business payload;
- arbitrary exception labels;
- high-cardinality identifiers.

Emit:

`RELEASE 1.10 WP03 TELEMETRY SECURITY: PASS`

---

# Phase 11 — Architecture/no-bypass gate

Run relevant architecture/no-bypass tests.

Require:

- WP02 Application observability unchanged;
- no Application→Infrastructure dependency violation;
- no direct Streamlit SQLite/provider access;
- Worker ownership unchanged;
- canonical JSON handoff unchanged;
- no parallel pipeline;
- SQLite schema remains v4.

Emit:

`RELEASE 1.10 WP03 ARCHITECTURE/NO-BYPASS: PASS`

---

# Phase 12 — Full affected validation

Do not stop at focused tests.

Run the full WP03 affected validation required by the current repository and reconciled plan.

At minimum:

1. affected Infrastructure build;
2. focused listener tests;
3. full relevant Infrastructure tests;
4. Application tests;
5. architecture/no-bypass tests;
6. security scan;
7. process/listener residue check.

Report exact counts.

All failures must be attributed.

WP03-caused failure => repair within scope and rerun.

Unrelated/pre-existing failure => document evidence and apply the governing acceptance rule; do not conceal it.

Emit:

`RELEASE 1.10 WP03 FULL AFFECTED VALIDATION: PASS`

---

# Phase 13 — Functional preservation

Prove unchanged behavior for:

- historical observation retrieval;
- snapshot store;
- snapshot retrieve;
- snapshot not-found semantics;
- DB contents;
- transaction behavior;
- exception propagation;
- deterministic/replay/simulated provenance;
- SQLite schema v4;
- canonical JSON handoff;
- Worker/Streamlit independence.

Emit:

`RELEASE 1.10 WP03 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

---

# Phase 14 — Residue check

Verify no WP03-owned process/listener/testhost residue remains.

Emit:

`RELEASE 1.10 WP03 PROCESS/LISTENER RESIDUE: CLEAN`

---

# Phase 15 — Exact combined path/hunk audit

Account for the complete WP03 delta across:

- original V2 partial work;
- first resumption partial work;
- Resumption 2 work.

Separate it from:

- Release 1.10 planning;
- WP01;
- WP02;
- Luna reconciliation.

Combined WP03 repository mutation must be limited to the exact frozen production/test paths.

Production hunks must be limited to:

- exact telemetry identity members authorized by Luna;
- the three frozen methods.

Test hunks must be limited to:

- the two frozen existing test files.

No unexpected paths.

Emit:

`RELEASE 1.10 WP03 PATH OWNERSHIP: PASS`

---

# Phase 16 — WP04 handoff

Report exact WP03 outputs inherited by WP04:

- ActivitySource `AIQuantTradingResearch.Infrastructure`;
- Meter `AIQuantTradingResearch.Infrastructure`;
- `provider.operation`;
- `persistence.operation`;
- exact metric names/types/units;
- exact attributes;
- exact operation kinds/outcomes;
- exact failure categories;
- ambient Activity.Current topology;
- BCL-only WP03 state.

WP04 must not redesign these contracts.

Emit:

`RELEASE 1.10 WP03 DOWNSTREAM HANDOFF: PASS — WP04 READY`

---

# Phase 17 — Acceptance matrix

Evaluate every WP03 acceptance criterion from:

- issue #244;
- definition;
- execution plan;
- file manifest;
- Luna reconciliation;
- original V2 authority;
- first resumption;
- this Resumption 2.

Every criterion must include evidence.

Do not emit acceptance merely because tests compile.

Only when all gates pass emit:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

---

# Phase 18 — GitHub work-package completion

This phase is forbidden unless the exact acceptance marker above has been emitted.

Re-read #244 and Project #2.

Require:

- #244 correct identity;
- milestone #59;
- Release=1.10;
- unique Project #2 item.

Then perform only if needed:

1. close #244;
2. set its Project #2 Status to `Done`.

Maximum GitHub mutations: 2.

Do NOT:

- close milestone #59;
- alter Release field;
- modify #245–#249;
- start WP04.

Emit:

`RELEASE 1.10 WP03 GITHUB WORK-PACKAGE COMPLETION: PASS`

---

# Phase 19 — GitHub post-verification

Re-read state.

Require:

- #242 Closed/Done;
- #243 Closed/Done;
- #244 Closed/Done;
- #244 Release=1.10;
- #244 milestone #59;
- milestone #59 remains Open;
- #245–#249 remain unchanged unless independently changed by another legitimate authority.

Report actual milestone counts.

Emit:

`RELEASE 1.10 WP03 GITHUB COMPLETION POST-VERIFY: PASS`

---

# Phase 20 — Mutation accounting

Report exact combined mutation ledger.

## Repository

Accepted WP03 frozen paths only.

## Git

ZERO.

## GitHub

At most:

- close #244;
- #244 Project Status → Done.

Required markers:

`RELEASE 1.10 WP03 REPOSITORY MUTATIONS: ACCEPTED WP03 PATHS ONLY`

`RELEASE 1.10 WP03 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

Model marker:

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Phase 21 — Next authority

Only after full WP03 completion:

**Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Authority — GPT-5.6 Terra**

Do not execute WP04 here.

---

# Required completion markers

`RELEASE 1.10 WP03 V2 RESUMPTION 2 ENTRY AUDIT: PASS`

`RELEASE 1.10 WP03 EXIT-PATH OBSERVABILITY MATRIX: FROZEN`

`RELEASE 1.10 WP03 ALL AUTHORIZED EXIT PATHS: PASS`

`RELEASE 1.10 WP03 PROVIDER LISTENER TESTS: PASS`

`RELEASE 1.10 WP03 PERSISTENCE LISTENER TESTS: PASS`

`RELEASE 1.10 WP03 FOCUSED LISTENER TESTS: PASS`

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

`RELEASE 1.10 WP03 METRIC/CARDINALITY CONTRACT: PASS`

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 DEPENDENCY BOUNDARY: PASS — BCL ONLY`

`RELEASE 1.10 WP03 FORBIDDEN TARGET AUDIT: PASS`

`RELEASE 1.10 WP03 TELEMETRY SECURITY: PASS`

`RELEASE 1.10 WP03 ARCHITECTURE/NO-BYPASS: PASS`

`RELEASE 1.10 WP03 FULL AFFECTED VALIDATION: PASS`

`RELEASE 1.10 WP03 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

`RELEASE 1.10 WP03 PROCESS/LISTENER RESIDUE: CLEAN`

`RELEASE 1.10 WP03 PATH OWNERSHIP: PASS`

`RELEASE 1.10 WP03 DOWNSTREAM HANDOFF: PASS — WP04 READY`

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

`RELEASE 1.10 WP03 GITHUB WORK-PACKAGE COMPLETION: PASS`

`RELEASE 1.10 WP03 GITHUB COMPLETION POST-VERIFY: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

`RELEASE 1.10 WP03 REPOSITORY MUTATIONS: ACCEPTED WP03 PATHS ONLY`

`RELEASE 1.10 WP03 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

Exact terminal marker:

`RELEASE 1.10 WP03 — INFRASTRUCTURE PROVIDER, PERSISTENCE & FAILURE INSTRUMENTATION AUTHORITY V2 COMPLETE`

---

# Blocked outcome

BLOCK only when a real acceptance blocker remains after performing all work possible inside the frozen authority.

Do NOT block merely because:

- work is lengthy;
- focused listener tests still need to be written;
- multiple authorized return paths require completion;
- broad validation remains to be run.

Those are the assigned tasks of this authority.

BLOCK when, for example:

- the frozen contract is internally contradictory;
- required implementation needs an unauthorized path/symbol;
- BCL-only cannot satisfy the frozen contract;
- a forbidden target must be changed;
- listener/topology proof cannot be achieved within authorized test paths;
- security/cardinality cannot pass without contract change;
- WP03 causes an unresolved affected-test failure;
- path ownership is violated.

If blocked before:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

then:

- GitHub mutations MUST remain ZERO;
- #244 MUST remain Open/Backlog;
- WP04 MUST NOT start.

Preserve all valid in-scope partial work.

Report the exact blocker and the minimum next authority only if a genuinely new authority is required.

Exact blocked terminal:

`RELEASE 1.10 WP03 — INFRASTRUCTURE PROVIDER, PERSISTENCE & FAILURE INSTRUMENTATION AUTHORITY V2 BLOCKED`
