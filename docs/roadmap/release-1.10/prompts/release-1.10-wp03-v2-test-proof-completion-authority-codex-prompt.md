# Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority V2 — Test/Proof Completion Resumption

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, path ownership, and governance authority.
- **GPT-5.6 Terra** — PRIMARY execution authority for continuing the SAME WP03 V2 authority through the remaining test/proof work, full validation, acceptance, mutation accounting, and approved GitHub lifecycle completion.
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

This is another continuation of the **same WP03 V2 authority**.

Do not create a new architecture contract.

Do not reopen Luna reconciliation.

Do not begin WP04.

---

# Accepted current state

Treat the following as accepted entry evidence unless current repository inspection contradicts it:

`RELEASE 1.10 WP03 EXIT-PATH OBSERVABILITY MATRIX: FROZEN`

Current production state:

- all normal return paths of the three authorized methods route through the in-scope observability completion logic;
- BCL-only Infrastructure build passes with **0 warnings / 0 errors**;
- existing focused Infrastructure baseline remains **23/23 passing**;
- current WP03 production changes remain confined to the frozen production scope;
- no forbidden target was changed;
- no package/project/schema/migration mutation occurred;
- Git mutations are ZERO;
- GitHub mutations are ZERO;
- #244 remains Open / Backlog;
- WP04 has not started.

The outstanding work is the deterministic test/proof and final acceptance layer.

Do not rewrite working production instrumentation merely to create activity.

---

# Frozen production targets

Production mutations remain restricted to:

1. `SqliteHistoricalObservationStore.Retrieve(string target)`
2. `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
3. `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Existing exact telemetry identity members already authorized by Luna remain in scope.

Production changes should now be made only if a focused proof exposes a genuine defect in the frozen implementation contract.

---

# Forbidden production targets

Do NOT modify/instrument:

- `SqliteDatasetCatalog`
- `SqliteHistoricalObservationStore.Persist(...)`

No unrelated Infrastructure production symbols.

---

# Frozen test scope

The remaining focused test work is authorized only in the existing exact repository paths for:

- `SqlitePersistenceTests.cs`
- `SqliteDatasetTests.cs`

No new test file.

If the reconciled manifest assigns which of these files owns provider vs persistence proof, obey that exact assignment.

---

# Frozen telemetry contract

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

Metrics, instrument types, units, operation kinds, outcomes, attributes, and failure categories:

**Use the exact frozen values in the reconciled Release 1.10 execution plan/file manifest.**

Duration unit:

`ms`

No package/API redesign.

---

# Frozen topology

Infrastructure activities inherit ambient `Activity.Current`.

Canonical provider path:

`pipeline.execute`
→ WP02 `HistoricalObservationRetrieval`
→ WP03 `provider.operation`

Do not duplicate the WP02 semantic interval.

Snapshot persistence/retrieval uses truthful ambient parenting.

No `SqliteDatasetCatalog` activity.

---

# Execution principle

The remaining unimplemented listener tests and validation gates are the assigned work of this authority.

Do **not** BLOCK merely because:

- tests still need to be written;
- listener plumbing must be implemented in the authorized test files;
- multiple deterministic success/failure cases must be added;
- full affected validation remains to be run.

Perform that work.

BLOCK only for a genuine contract/scope contradiction or an unresolved failure that cannot be repaired inside frozen authority.

---

# Phase 0 — Entry verification

Read:

1. reconciled Release 1.10 execution plan;
2. reconciled file manifest;
3. `OPEN_TELEMETRY_SELECTION.md`;
4. #244 read-only;
5. current three production method implementations;
6. current two authorized test files;
7. current worktree diff/status.

Verify the previously frozen exit-path matrix remains satisfied.

Verify:

- current build is attributable;
- no forbidden target mutation;
- no unauthorized package/project/schema mutation;
- #244 Open/Backlog;
- Git/GitHub mutation state remains zero.

Emit:

`RELEASE 1.10 WP03 V2 TEST/PROOF RESUMPTION ENTRY: PASS`

---

# Phase 1 — Listener test design read-back

Before editing tests, print the exact deterministic proof matrix.

For every frozen activity/instrument list:

- source/meter name;
- activity/instrument name;
- instrument type;
- unit;
- owning method;
- success case;
- not-found/empty case if applicable;
- failure case;
- expected bounded attributes;
- expected outcome;
- expected failure category;
- expected parent behavior.

Do not invent contract values.

Emit:

`RELEASE 1.10 WP03 LISTENER PROOF MATRIX: FROZEN`

---

# Phase 2 — Add provider ActivityListener tests

In the authorized existing test file only, implement deterministic BCL `ActivityListener` coverage for:

`SqliteHistoricalObservationStore.Retrieve(string target)`

Prove:

- exact source identity;
- exact `provider.operation` name;
- activity is observable when listener requests it;
- success status/outcome;
- applicable empty/not-found behavior;
- deterministic failure status/outcome;
- exact bounded attributes;
- exact bounded failure category;
- original exception propagation;
- no uncontrolled target/symbol data;
- no raw exception message;
- no instrumentation of `Persist(...)`.

Emit:

`RELEASE 1.10 WP03 PROVIDER ACTIVITYLISTENER TESTS: PASS`

---

# Phase 3 — Add provider MeterListener tests

In the authorized existing test file only, implement deterministic BCL `MeterListener` coverage.

Prove exact frozen provider instruments:

- operation;
- duration;
- failure.

For each prove:

- exact name;
- exact instrument type;
- exact unit;
- correct count/measurement;
- duration is non-negative and uses `ms`;
- failure increments exactly as frozen;
- bounded tags only;
- no high-cardinality dimensions.

Emit:

`RELEASE 1.10 WP03 PROVIDER METERLISTENER TESTS: PASS`

---

# Phase 4 — Add persistence ActivityListener tests

In the authorized existing test file only, implement deterministic `ActivityListener` coverage for:

- `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
- `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Prove:

- exact source;
- exact `persistence.operation`;
- exact bounded operation kind for store;
- exact bounded operation kind for retrieve;
- success;
- frozen not-found behavior;
- deterministic failure where fixture permits;
- bounded outcome;
- bounded failure category;
- exception propagation;
- no duplicate `SqliteDatasetCatalog` activity.

Emit:

`RELEASE 1.10 WP03 PERSISTENCE ACTIVITYLISTENER TESTS: PASS`

---

# Phase 5 — Add persistence MeterListener tests

Implement deterministic BCL `MeterListener` proof for exact persistence instruments.

Prove:

- exact operation instrument;
- exact duration instrument;
- exact failure instrument;
- exact types;
- exact units;
- correct store operation observation;
- correct retrieve operation observation;
- correct not-found semantics;
- correct failure semantics;
- bounded tags only;
- no duplicate catalog measurement.

Emit:

`RELEASE 1.10 WP03 PERSISTENCE METERLISTENER TESTS: PASS`

---

# Phase 6 — Focused listener validation

Run all newly added WP03-focused tests in the two authorized files.

Report exact counts.

Require all pass.

If a test exposes a production defect, repair only within the three authorized methods/existing authorized telemetry identity members and rerun.

Emit:

`RELEASE 1.10 WP03 FOCUSED LISTENER TESTS: PASS`

---

# Phase 7 — Topology proof

Using deterministic ActivityListener evidence, prove:

`pipeline.execute`
→ WP02 `HistoricalObservationRetrieval`
→ WP03 `provider.operation`

Prove:

- same trace where applicable;
- WP03 parent span/activity is the ambient WP02 retrieval activity;
- WP03 does not become an independent competing root;
- no duplicate semantic ownership;
- snapshot activities preserve truthful ambient parenting;
- no catalog duplicate activity.

Use existing authorized test scope and existing test infrastructure. If a proof can be achieved without repository mutation via an execution harness/filtered existing tests, prefer that.

Do not expand test path ownership.

Emit:

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

---

# Phase 8 — Cardinality proof

Using MeterListener observations across representative deterministic inputs, prove metric tag values remain bounded.

Explicitly prove metrics do not use as dimensions:

- raw target/symbol;
- GUID/request IDs;
- timestamps;
- raw filesystem paths;
- connection strings;
- SQL text;
- payload contents;
- arbitrary exception messages.

Emit:

`RELEASE 1.10 WP03 METRIC/CARDINALITY CONTRACT: PASS`

---

# Phase 9 — Failure semantics proof

Exercise deterministic failure paths available in authorized tests.

Require:

- activity failure status/outcome correct;
- operation semantics correct;
- duration consistently recorded;
- failure metric exactly once;
- bounded failure category;
- original exception propagates;
- no false success;
- no duplicate failure from delegating layer.

Emit:

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

---

# Phase 10 — Production contract recheck

After listener tests, recheck all three authorized production methods.

Confirm:

- all frozen exit paths still route through correct observability logic;
- no double-recording introduced;
- no behavior change introduced merely to satisfy tests;
- BCL-only remains true.

Emit:

`RELEASE 1.10 WP03 ALL AUTHORIZED EXIT PATHS: PASS`

`RELEASE 1.10 WP03 DEPENDENCY BOUNDARY: PASS — BCL ONLY`

---

# Phase 11 — Forbidden-target and exact scope audit

Inspect the complete combined WP03 delta from all V2 attempts/resumptions.

Require:

- `SqliteDatasetCatalog` unchanged by WP03;
- `SqliteHistoricalObservationStore.Persist(...)` unchanged by WP03;
- no helper file;
- no new test file;
- no project/package mutation;
- no schema/migration mutation;
- no unrelated production symbol.

Emit:

`RELEASE 1.10 WP03 FORBIDDEN TARGET AUDIT: PASS`

---

# Phase 12 — Security validation

Run Gitleaks/security checks over every combined WP03-mutated path.

Manually inspect telemetry dimensions and activity attributes.

Require no secrets or uncontrolled sensitive/high-cardinality values.

Emit:

`RELEASE 1.10 WP03 TELEMETRY SECURITY: PASS`

---

# Phase 13 — Architecture/no-bypass validation

Run the relevant architecture/no-bypass tests.

Require:

- WP02 unchanged semantically;
- dependency direction preserved;
- no Streamlit SQLite/provider access;
- no Worker ownership change;
- no parallel pipeline;
- canonical JSON handoff unchanged;
- SQLite schema remains v4.

Emit:

`RELEASE 1.10 WP03 ARCHITECTURE/NO-BYPASS: PASS`

---

# Phase 14 — Full affected validation

Run the complete WP03 affected validation.

At minimum:

1. Infrastructure build;
2. all WP03 focused listener tests;
3. full relevant Infrastructure test suite;
4. Application tests;
5. architecture/no-bypass tests;
6. security scan;
7. process/listener/testhost residue.

Report exact counts, warnings, errors, failures, and skips.

All WP03-attributable failures must be repaired within scope and rerun.

Emit:

`RELEASE 1.10 WP03 FULL AFFECTED VALIDATION: PASS`

---

# Phase 15 — Functional preservation

Prove observability did not alter:

- historical retrieval;
- snapshot store;
- snapshot retrieval;
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

# Phase 16 — Residue validation

Verify no WP03-owned process, testhost, ActivityListener, MeterListener, or listener-related residue remains.

Emit:

`RELEASE 1.10 WP03 PROCESS/LISTENER RESIDUE: CLEAN`

---

# Phase 17 — Exact combined path/hunk ownership

Separate final worktree into:

1. pre-existing Release 1.10 planning/WP01/WP02 residue;
2. Luna WP03 reconciliation;
3. all combined WP03 V2 implementation/test changes.

WP03 combined mutations must remain only in the frozen production/test paths.

Production hunks:

- exact authorized telemetry identity members;
- three frozen methods only.

Test hunks:

- two frozen existing test files only.

Zero unexpected paths.

Emit:

`RELEASE 1.10 WP03 PATH OWNERSHIP: PASS`

---

# Phase 18 — WP04 handoff

Report exact frozen WP03 output:

- ActivitySource `AIQuantTradingResearch.Infrastructure`;
- Meter `AIQuantTradingResearch.Infrastructure`;
- `provider.operation`;
- `persistence.operation`;
- exact metrics/types/units;
- exact bounded attributes/operation kinds/outcomes;
- exact failure categories;
- ambient topology;
- BCL-only dependency state.

Emit:

`RELEASE 1.10 WP03 DOWNSTREAM HANDOFF: PASS — WP04 READY`

Do not execute WP04.

---

# Phase 19 — Acceptance matrix

Evaluate every WP03 acceptance criterion from:

- #244;
- Release 1.10 definition;
- execution plan;
- file manifest;
- Luna reconciliation;
- original WP03 V2 authority;
- all resumption authorities.

Every criterion must have concrete evidence.

Only after every criterion passes emit:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

No GitHub lifecycle mutation before this marker.

---

# Phase 20 — GitHub completion

After acceptance only, re-read #244 and its unique Project #2 item.

Require:

- #244 identity correct;
- milestone #59;
- Release=1.10;
- unique Project item.

Then perform at most two mutations:

1. close #244 if Open;
2. set Project #2 Status to `Done` if needed.

Do not:

- close milestone #59;
- alter Release field;
- modify #245–#249;
- begin WP04.

Emit:

`RELEASE 1.10 WP03 GITHUB WORK-PACKAGE COMPLETION: PASS`

---

# Phase 21 — GitHub post-verification

Re-read GitHub.

Require:

- #242 Closed/Done;
- #243 Closed/Done;
- #244 Closed/Done;
- #244 Release=1.10;
- #244 milestone #59;
- milestone #59 remains Open;
- #245–#249 unchanged unless independently changed by another valid authority.

Report actual milestone counts.

Emit:

`RELEASE 1.10 WP03 GITHUB COMPLETION POST-VERIFY: PASS`

---

# Phase 22 — Mutation accounting

Report exact combined WP03 ledger.

Repository:

WP03 frozen paths only.

Git:

ZERO.

GitHub:

at most:

- #244 close;
- #244 Project Status → Done.

Emit:

`RELEASE 1.10 WP03 REPOSITORY MUTATIONS: ACCEPTED WP03 PATHS ONLY`

`RELEASE 1.10 WP03 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Phase 23 — Next authority

Only after exact WP03 completion:

**Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Authority — GPT-5.6 Terra**

Do not execute it here.

---

# Required success markers

`RELEASE 1.10 WP03 V2 TEST/PROOF RESUMPTION ENTRY: PASS`

`RELEASE 1.10 WP03 LISTENER PROOF MATRIX: FROZEN`

`RELEASE 1.10 WP03 PROVIDER ACTIVITYLISTENER TESTS: PASS`

`RELEASE 1.10 WP03 PROVIDER METERLISTENER TESTS: PASS`

`RELEASE 1.10 WP03 PERSISTENCE ACTIVITYLISTENER TESTS: PASS`

`RELEASE 1.10 WP03 PERSISTENCE METERLISTENER TESTS: PASS`

`RELEASE 1.10 WP03 FOCUSED LISTENER TESTS: PASS`

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

`RELEASE 1.10 WP03 METRIC/CARDINALITY CONTRACT: PASS`

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 ALL AUTHORIZED EXIT PATHS: PASS`

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

`RELEASE 1.10 WP03 REPOSITORY MUTATIONS: ACCEPTED WP03 PATHS ONLY`

`RELEASE 1.10 WP03 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

Exact terminal:

`RELEASE 1.10 WP03 — INFRASTRUCTURE PROVIDER, PERSISTENCE & FAILURE INSTRUMENTATION AUTHORITY V2 COMPLETE`

---

# Blocked outcome

BLOCK only for a genuine blocker that cannot be resolved within the frozen WP03 authority.

Do not block simply because listener tests or validation remain to be performed; those are the assigned work.

Examples of valid blockers:

- frozen listener proof requires an unauthorized repository path;
- frozen metric contract contradicts actual BCL instrument semantics;
- topology cannot be proven without contract expansion;
- a required fix needs a forbidden production target;
- security/cardinality cannot pass within frozen vocabulary;
- WP03-attributable affected-suite failure cannot be fixed inside scope;
- exact path/hunk ownership is violated and cannot be repaired within WP03-owned hunks.

If blocked before `RELEASE 1.10 WP03 ACCEPTANCE: PASS`:

- GitHub mutations remain ZERO;
- #244 remains Open/Backlog;
- WP04 remains blocked.

Preserve all valid in-scope work.

Exact blocked terminal:

`RELEASE 1.10 WP03 — INFRASTRUCTURE PROVIDER, PERSISTENCE & FAILURE INSTRUMENTATION AUTHORITY V2 BLOCKED`
