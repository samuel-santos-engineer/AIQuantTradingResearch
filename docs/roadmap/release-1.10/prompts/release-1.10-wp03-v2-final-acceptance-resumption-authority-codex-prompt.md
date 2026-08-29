# Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority V2 — Final Acceptance Resumption

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, path ownership, and governance authority.
- **GPT-5.6 Terra** — PRIMARY execution authority for completing the SAME WP03 V2 authority through final acceptance, exact mutation accounting, GitHub lifecycle completion, and WP04 handoff.
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

This is the **same WP03 V2 authority**, resumed after successful local environment unblock.

It is NOT:

- a new WP;
- a new architecture contract;
- a Luna reconciliation;
- an environment-unblock authority;
- a WP04 execution authority.

The only remaining objective is to finish WP03 acceptance and lifecycle completion using already-passing implementation/test evidence.

---

# Accepted entry evidence

Treat the following as accepted unless current verification contradicts it:

## Production/test state

- WP03 exit-path observability matrix is frozen.
- All normal return paths of the three authorized production methods route through the frozen observability logic.
- Focused WP03 listener validation: **25/25 PASS**.
- Full Infrastructure validation after environment unblock: **184/184 PASS**.
- Application: **131/131 PASS**.
- Architecture: **21/21 PASS** using the locally signed Worker artifact with `--no-build`.
- Infrastructure build: **0 warnings / 0 errors**.
- Worker local development signing completed using documented mechanism with subject `CN=AIQuantTradingDev`.
- The regular architecture test build replaces the local Worker signature, therefore architecture validation must preserve the accepted `--no-build` execution pattern against the signed artifact.

## Mutation state

- no tracked repository-contract mutations came from the environment-unblock authority;
- Git mutations: ZERO;
- GitHub mutations: ZERO;
- #244 remains Open / Backlog;
- milestone #59 remains Open;
- WP04 has not started.

Emit after read-back:

`RELEASE 1.10 WP03 V2 FINAL RESUMPTION ENTRY: PASS`

---

# Frozen implementation scope

Production authorization remains limited to:

1. `SqliteHistoricalObservationStore.Retrieve(string target)`
2. `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
3. `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Existing exact telemetry identity members previously authorized by Luna remain in scope.

Focused test mutation remains limited to existing:

- `SqlitePersistenceTests.cs`
- `SqliteDatasetTests.cs`

No new implementation work is expected unless a final acceptance proof exposes a genuine defect.

---

# Explicitly forbidden scope

Do NOT:

- modify `SqliteDatasetCatalog`;
- modify/instrument `SqliteHistoricalObservationStore.Persist(...)`;
- add helper files;
- add test files;
- modify `.csproj`;
- modify package versions;
- add OpenTelemetry packages;
- modify schema/migrations;
- modify Python dependencies;
- alter the documented local development signing mechanism;
- weaken Windows security controls;
- stage/commit/push/branch/merge/rebase/tag;
- start WP04 before WP03 completion.

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

Use the exact metric names, types, units, bounded attributes, operation kinds, outcomes, and failure categories frozen by the reconciled Release 1.10 plan/manifest.

Duration unit:

`ms`

Canonical topology:

`pipeline.execute`
→ WP02 `HistoricalObservationRetrieval`
→ WP03 `provider.operation`

---

# Phase 0 — Read-only state reconciliation

Read:

1. Release 1.10 definition;
2. reconciled execution plan;
3. reconciled file manifest;
4. `OPEN_TELEMETRY_SELECTION.md`;
5. issue #244;
6. Project #2 item for #244;
7. milestone #59;
8. current worktree status/diff;
9. current WP03 implementation/tests;
10. environment-unblock result/evidence.

Verify all accepted entry evidence is still true.

Report any drift explicitly.

Emit:

`RELEASE 1.10 WP03 V2 FINAL STATE RECONCILIATION: PASS`

---

# Phase 1 — Focused proof reconciliation

Do not rewrite passing tests.

Re-read the existing focused listener test results and, if needed for current evidence, rerun them.

Require:

- 25/25 PASS or current equivalent count if the test suite has legitimately changed within frozen scope;
- provider ActivityListener proof;
- provider MeterListener proof;
- persistence ActivityListener proof;
- persistence MeterListener proof;
- bounded attributes;
- exact units/types;
- failure semantics;
- no duplicate catalog activity;
- no historical Persist instrumentation.

Emit:

`RELEASE 1.10 WP03 FOCUSED LISTENER TESTS: PASS`

---

# Phase 2 — Topology acceptance proof

Reconcile existing ActivityListener evidence and, where needed, rerun the exact deterministic proof.

Require:

`pipeline.execute`
→ WP02 `HistoricalObservationRetrieval`
→ WP03 `provider.operation`

Prove:

- ambient `Activity.Current` parenting;
- same trace where applicable;
- no competing WP03 root;
- no duplicate semantic ownership;
- truthful ambient parenting for snapshot operations;
- no `SqliteDatasetCatalog` duplicate activity.

Emit:

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

---

# Phase 3 — Metric/cardinality acceptance proof

Reconcile existing MeterListener evidence.

Require exact proof of:

- metric names;
- instrument types;
- units;
- operation measurements;
- duration measurements in `ms`;
- failure measurements;
- bounded operation kinds/outcomes/failure categories.

Require absence of high-cardinality/sensitive dimensions:

- arbitrary target/symbol;
- GUID/request IDs;
- timestamps;
- filesystem paths;
- connection strings;
- SQL;
- raw payload;
- arbitrary exception messages.

Emit:

`RELEASE 1.10 WP03 METRIC/CARDINALITY CONTRACT: PASS`

---

# Phase 4 — Failure semantics acceptance proof

Reconcile deterministic failure test evidence.

Require:

- activity failure state correct;
- operation semantics correct;
- duration recorded consistently;
- failure metric exactly once;
- bounded sanitized failure category;
- original exception propagates unchanged;
- no false success;
- no duplicate delegating-layer failure telemetry.

Emit:

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

---

# Phase 5 — Dependency/forbidden-target audit

Inspect the final combined WP03 delta.

Require:

- BCL only;
- no package/project/helper/schema/migration mutation;
- `SqliteDatasetCatalog` untouched by WP03;
- `SqliteHistoricalObservationStore.Persist(...)` untouched by WP03;
- no unrelated production symbol.

Emit:

`RELEASE 1.10 WP03 DEPENDENCY BOUNDARY: PASS — BCL ONLY`

`RELEASE 1.10 WP03 FORBIDDEN TARGET AUDIT: PASS`

---

# Phase 6 — Security acceptance

Re-run or reconcile Gitleaks/security validation against all combined WP03-mutated paths.

Manually confirm no telemetry contains:

- secrets;
- credentials;
- connection strings;
- provider payloads;
- SQL/business payload;
- arbitrary exception messages as dimensions;
- uncontrolled high-cardinality identifiers.

Emit:

`RELEASE 1.10 WP03 TELEMETRY SECURITY: PASS`

---

# Phase 7 — Full affected validation acceptance

Use the already-successful environment-unblocked validation pattern.

Run/reconcile:

- Infrastructure: expected accepted baseline **184/184 PASS**;
- focused WP03 listener: **25/25 PASS**;
- Application: **131/131 PASS**;
- Architecture: **21/21 PASS** using the locally signed Worker artifact with `--no-build`;
- Infrastructure build: **0 warnings / 0 errors**.

If a regular build replaces the local Worker signature, restore the already-approved local development signing state using the documented mechanism only, then rerun architecture with `--no-build`.

This does not authorize tracked signing/project changes.

Report exact current counts.

Emit:

`RELEASE 1.10 WP03 FULL AFFECTED VALIDATION: PASS`

---

# Phase 8 — Functional preservation

Prove no behavior change in:

- historical retrieval;
- snapshot store;
- snapshot retrieval;
- not-found semantics;
- transaction/database contents;
- exception propagation;
- deterministic/replay/simulated provenance;
- SQLite schema v4;
- canonical JSON handoff;
- Worker/Streamlit independence.

Emit:

`RELEASE 1.10 WP03 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

---

# Phase 9 — Process/listener residue

Verify no WP03-owned process/testhost/Worker/listener residue remains.

Emit:

`RELEASE 1.10 WP03 PROCESS/LISTENER RESIDUE: CLEAN`

---

# Phase 10 — Exact combined path/hunk ownership

Separate current worktree into:

1. pre-existing Release 1.10 planning/WP01/WP02 residue;
2. Luna WP03 reconciliation planning changes;
3. combined WP03 V2 production/test implementation;
4. local environment-only signing artifacts/state.

The combined WP03 repository delta must be limited to the frozen production/test paths.

Production hunks:

- exact authorized telemetry identity members;
- three frozen methods.

Test hunks:

- two frozen existing test files.

Environment signing must contribute:

- ZERO tracked repository-contract mutations.

Emit:

`RELEASE 1.10 WP03 PATH OWNERSHIP: PASS`

---

# Phase 11 — WP04 handoff contract

Report exact inherited contract for WP04:

- ActivitySource `AIQuantTradingResearch.Infrastructure`;
- Meter `AIQuantTradingResearch.Infrastructure`;
- `provider.operation`;
- `persistence.operation`;
- exact metric names/types/units;
- exact bounded attributes;
- exact operation kinds/outcomes;
- exact failure categories;
- ambient topology;
- BCL-only WP03 dependency state;
- local dev signing remains an environment concern, not WP04 observability contract.

Emit:

`RELEASE 1.10 WP03 DOWNSTREAM HANDOFF: PASS — WP04 READY`

Do not execute WP04.

---

# Phase 12 — Final acceptance matrix

Evaluate every WP03 acceptance criterion from:

- #244;
- Release 1.10 definition;
- execution plan;
- file manifest;
- Luna reconciliation;
- original WP03 V2 authority;
- all WP03 V2 resumptions;
- environment-unblock validation evidence.

Every criterion must include concrete evidence.

Only if every criterion passes emit:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

This is the hard GitHub lifecycle gate.

---

# Phase 13 — GitHub lifecycle completion

Only after exact:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

Re-read #244 and Project #2.

Require:

- issue #244;
- milestone #59;
- Release=1.10;
- exactly one Project #2 item.

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

# Phase 14 — GitHub post-verification

Re-read GitHub.

Require:

- #242 Closed/Done;
- #243 Closed/Done;
- #244 Closed/Done;
- #244 Release=1.10;
- #244 milestone #59;
- milestone #59 remains Open;
- #245–#249 remain unchanged unless independently changed by another valid authority.

Report actual milestone open/closed counts.

Emit:

`RELEASE 1.10 WP03 GITHUB COMPLETION POST-VERIFY: PASS`

---

# Phase 15 — Mutation accounting

Report exact final mutation ledger.

## Repository

Only accepted WP03 frozen production/test paths plus pre-existing planning/reconciliation residue.

Environment unblock/signing:

ZERO tracked repository-contract mutations.

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

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Phase 16 — Next authority

Only after full completion:

**Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Authority — GPT-5.6 Terra**

Do not execute it here.

---

# Required success markers

`RELEASE 1.10 WP03 V2 FINAL RESUMPTION ENTRY: PASS`

`RELEASE 1.10 WP03 V2 FINAL STATE RECONCILIATION: PASS`

`RELEASE 1.10 WP03 FOCUSED LISTENER TESTS: PASS`

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

`RELEASE 1.10 WP03 METRIC/CARDINALITY CONTRACT: PASS`

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 DEPENDENCY BOUNDARY: PASS — BCL ONLY`

`RELEASE 1.10 WP03 FORBIDDEN TARGET AUDIT: PASS`

`RELEASE 1.10 WP03 TELEMETRY SECURITY: PASS`

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

BLOCK only for a genuine unresolved WP03 acceptance issue.

Do not block because the previously resolved environment issue required local signing; the documented local dev signing path is accepted evidence.

Valid blockers include:

- focused proof no longer passes;
- topology/cardinality/failure semantics contradict the frozen contract;
- security failure;
- affected-suite WP03 failure;
- path/hunk ownership violation;
- acceptance criterion cannot be evidenced;
- GitHub identity/project item state is inconsistent and cannot be safely reconciled within the two authorized completion mutations.

If blocked before:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

then:

- GitHub mutations remain ZERO;
- #244 remains Open/Backlog;
- WP04 remains blocked.

If blocked after acceptance but before completing GitHub lifecycle, report exact partial state and do not invent extra repair authority.

Exact blocked terminal:

`RELEASE 1.10 WP03 — INFRASTRUCTURE PROVIDER, PERSISTENCE & FAILURE INSTRUMENTATION AUTHORITY V2 BLOCKED`
