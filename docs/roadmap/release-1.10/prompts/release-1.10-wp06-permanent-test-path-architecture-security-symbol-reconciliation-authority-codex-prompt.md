# Release 1.10 WP06 — Permanent Test Path & Architecture/Security Symbol Reconciliation Authority

## Model assignment

- **GPT-5.6 Luna** — PRIMARY authority for contract, policy, architecture, test ownership, path/symbol reconciliation, invariant allocation, acceptance criteria, governance, and planning-only mutations.
- **GPT-5.6 Terra** — downstream implementation, validation execution, approved repository/GitHub lifecycle mutations only after this authority produces deterministic test ownership.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and exploratory/non-authoritative review; never silently replaces Luna or Terra.

**Selected execution model: GPT-5.6 Luna.**

---

# Authority identity

Release: **1.10**

WP: **WP06 — Permanent Observability and No-Bypass Tests**

Issue: **#247**

Milestone: **#59**

Project: **#2**

Predecessor: **WP05 #246 — Closed / Done**

Dependency:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

This is a narrow planning/reconciliation authority created because the first WP06 Terra authority correctly blocked before repository mutation.

---

# Accepted blocker

The binding Release 1.10 manifest requires WP06 to use only:

- exact dedicated WP06 test paths; and
- explicitly named architecture/security symbols.

However, the current:

- file manifest;
- execution plan; and
- WP06 implementation authority

do not enumerate those paths/symbols.

Therefore Terra cannot create or modify a test path without inventing ownership.

Accepted live state:

- #242–#246 Closed/Done.
- #247 Open/Backlog.
- #248–#249 Open/Backlog.
- milestone #59 Open: **3 open / 5 closed**.
- repository mutations from blocked WP06 run: ZERO.
- Git mutations: ZERO.
- GitHub mutations: ZERO.

Emit:

`RELEASE 1.10 WP06 TEST-PATH RECONCILIATION ENTRY: PASS`

---

# Purpose

Freeze a complete WP06 test ownership contract such that Terra can implement all permanent observability/no-bypass enforcement without choosing:

- a test project;
- a test file;
- a new-file name;
- an existing symbol to extend;
- a helper/fixture path;
- an invariant allocation;
- an assertion technique;
- a security/architecture ownership boundary.

This authority does **not** implement tests.

---

# Already-frozen production architecture

Do not reopen WP02–WP05 production contracts.

## WP02
- Application owns BCL `System.Diagnostics` pipeline observability.
- root `pipeline.execute`.
- five truthful governed stages.
- retrieval/materialization boundary frozen.

## WP03
Instrumentation owners:
- `SqliteHistoricalObservationStore.Retrieve(string target)`
- `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
- `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Non-owners:
- `SqliteDatasetCatalog`
- `SqliteHistoricalObservationStore.Persist(...)`

Activities:
- `provider.operation`
- `persistence.operation`

Bounded instruments/failure categories and parent topology frozen.

## WP04
- no external exporter;
- no exporter package/project mutation;
- BCL-only Worker/interop observations;
- Worker owns lifecycle;
- Streamlit independent;
- canonical handoff remains boundary.

## WP05
- .NET owns System Health.
- visualization states and System Health remain separate.
- `aiq-visualization-read-model-v1`.
- nested optional `systemHealth`.
- health states exactly:
  - `ready`
  - `warmup`
  - `empty`
  - `failed`
  - `stale`
  - `unavailable`
- `degraded` excluded.
- predicates, precedence, reason tokens, JSON shape, absent/malformed behavior, Streamlit mapping frozen.
- schema v4.
- no independent health freshness threshold.

---

# Mutation boundary

Allowed repository mutations: minimum planning/architecture artifacts required to make WP06 deterministic, expected among:

- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
- `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`

Modify Release 1.10 definition only for a genuine definition-level contradiction.

Forbidden:

- production code;
- test code;
- test helper implementation;
- `.csproj`;
- packages;
- schema/migrations;
- runtime artifacts;
- Git mutations;
- GitHub mutations;
- #247 closure;
- Project Status mutation;
- WP07 implementation.

#247 remains Open/Backlog.

---

# Phase 0 — Repository test-topology inventory

Read:

1. Release 1.10 definition;
2. execution plan;
3. file manifest;
4. `OPEN_TELEMETRY_SELECTION.md`;
5. #247 read-only;
6. #248 read-only;
7. all test projects;
8. all existing Architecture tests;
9. all relevant Application tests;
10. all relevant Infrastructure tests;
11. all Worker lifecycle tests;
12. all WP05 read-model tests;
13. all Python parser/presentation/architecture tests;
14. solution/project files read-only;
15. existing security/no-bypass test conventions;
16. current worktree status/diff.

Enumerate actual test paths and exact test class/function symbols.

Do not create hypothetical paths before inventory.

Emit:

`RELEASE 1.10 WP06 TEST TOPOLOGY INVENTORY: COMPLETE`

---

# Phase 1 — Freeze WP06 test-file strategy

Decide and freeze whether each WP06 invariant is enforced by:

A. a new dedicated WP06 test file;
B. an existing test file/symbol;
C. both, where responsibilities are non-duplicative.

For every new file freeze:

- exact repository-relative path;
- exact project/directory;
- exact class/module name;
- exact purpose.

For every existing file freeze:

- exact path;
- exact class/module;
- exact method/function symbols WP06 may extend;
- exact permitted additions.

No generic “Architecture tests” or “existing observability tests” wording.

Emit:

`RELEASE 1.10 WP06 DEDICATED TEST PATH STRATEGY: FROZEN`

---

# Phase 2 — WP02 invariant allocation

Allocate every permanent WP02 invariant to exact test path/symbol ownership:

- root activity `pipeline.execute`;
- exact stage set/order;
- retrieval boundary;
- materialization boundary;
- no duplicate opaque interval;
- `Activity.Current` child topology capability;
- bounded tags.

For each invariant freeze:

- exact test file;
- exact existing/new class;
- exact existing/new test method naming pattern or explicit method name;
- behavioral listener vs architecture/source assertion;
- why that layer owns the assertion.

Avoid duplicate testing where existing WP02 tests already permanently cover an invariant; explicitly identify existing coverage that WP06 merely relies on versus coverage WP06 must add.

Emit:

`RELEASE 1.10 WP06 WP02 INVARIANT/TEST ALLOCATION: FROZEN`

---

# Phase 3 — WP03 invariant allocation

Allocate:

- exact three instrumentation owners;
- catalog non-owner;
- Persist non-owner;
- ActivitySource/Meter ownership;
- exact activity names;
- bounded operation tokens;
- duration/failure instruments;
- finite/sanitized failure categories;
- parent topology;
- listener scoping.

Freeze exact files/classes/methods.

Distinguish:

- behavioral Infrastructure tests;
- architecture ownership tests;
- negative source/symbol tests.

Emit:

`RELEASE 1.10 WP06 WP03 INVARIANT/TEST ALLOCATION: FROZEN`

---

# Phase 4 — WP04 invariant allocation

Allocate:

- Worker lifecycle ownership;
- Python invoker no-supervision;
- no external exporter;
- no exporter package/project dependency;
- no exporter lifecycle/flush/dispose path;
- canonical handoff independence;
- Streamlit independence.

Freeze exact .NET Architecture/Infrastructure test paths and any Python ownership test path.

Do not require an exporter to test exporter absence.

Emit:

`RELEASE 1.10 WP06 WP04 INVARIANT/TEST ALLOCATION: FROZEN`

---

# Phase 5 — WP05 invariant allocation

Allocate permanent tests for:

- exact System Health state set;
- `degraded` exclusion;
- source predicates;
- precedence;
- finite reason tokens;
- state/reason mapping;
- nested JSON shape;
- v1 compatibility;
- pre-WP05 absent health;
- malformed health;
- timestamp/freshness semantics;
- no independent freshness threshold;
- provenance;
- exact Streamlit mapping.

Freeze exact .NET and Python test paths/symbols.

Identify which WP05 existing tests already provide permanent behavioral coverage and which WP06 architecture/security tests are necessary to prevent ownership/bypass/vocabulary drift.

Emit:

`RELEASE 1.10 WP06 WP05 INVARIANT/TEST ALLOCATION: FROZEN`

---

# Phase 6 — Python/Streamlit no-bypass ownership

Freeze exact Python test path/module/function ownership for:

- no SQLite access;
- no provider calls;
- no Worker supervision/process inspection;
- no ActivityListener/MeterListener authority;
- canonical-handoff-only health consumption;
- no second health channel;
- presentation-only Streamlit ownership.

Choose exact assertion method based on repository conventions:

- AST;
- import graph;
- source inspection;
- monkeypatch behavioral guard;
- another existing deterministic technique.

For each invariant freeze the technique and symbol.

Do not leave technique choice to Terra.

Emit:

`RELEASE 1.10 WP06 PYTHON/STREAMLIT NO-BYPASS SYMBOL OWNERSHIP: FROZEN`

---

# Phase 7 — Architecture test symbols

Freeze the exact Architecture test project paths/classes/methods responsible for cross-project ownership.

At minimum decide ownership for:

- Application observability ownership;
- Infrastructure instrumentation ownership;
- Worker lifecycle ownership;
- no external exporter/package dependency;
- canonical handoff ownership;
- schema v4/no migration;
- Release 1.8 boundary separation;
- Streamlit/Python no-bypass where .NET architecture tests can truthfully inspect it.

If an existing architecture class is extended, name it.

If a dedicated new class is needed, freeze exact path/class.

Emit:

`RELEASE 1.10 WP06 ARCHITECTURE TEST SYMBOLS: FROZEN`

---

# Phase 8 — Security test symbols

Freeze exact test ownership for security/cardinality invariants:

- finite health reason tokens;
- finite health states;
- no free-form exception messages;
- no secrets/tokens/connection strings/endpoints;
- no uncontrolled high-cardinality identifiers;
- bounded observability tags;
- no external exporter configuration/dependency.

Separate what is enforced by:

- unit/architecture tests;
- Gitleaks;
- package/project inspection.

Do not pretend Gitleaks proves cardinality.

Name exact test paths/classes/functions.

Emit:

`RELEASE 1.10 WP06 SECURITY TEST SYMBOLS: FROZEN`

---

# Phase 9 — Helper/fixture contract

Determine whether WP06 requires any new test helper/fixture.

If none, explicitly freeze:

`WP06 NEW TEST HELPERS/FIXTURES: NONE`

If required, freeze:

- exact path;
- exact symbol;
- exact owning test project;
- exact bounded purpose;
- consumers;
- forbidden expansion.

No generic shared helper creation.

Emit:

`RELEASE 1.10 WP06 TEST HELPER/FIXTURE CONTRACT: FROZEN`

---

# Phase 10 — Forbidden adjacent test paths

Explicitly list test files/projects WP06 must not modify, including adjacent tests that are tempting but outside ownership.

For each forbidden area state why:

- already sufficient;
- belongs to WP07/WP08;
- production behavior rather than permanent architecture;
- unrelated release capability.

Emit:

`RELEASE 1.10 WP06 FORBIDDEN TEST TARGETS: FROZEN`

---

# Phase 11 — Assertion-strength contract

For every invariant classify the required test strength:

1. behavioral;
2. architecture/symbol ownership;
3. negative dependency/import;
4. serialization contract;
5. presentation contract;
6. security/static inspection.

Prevent brittle tests:

- no comment matching;
- no formatting dependence;
- no broad string grep where AST/symbol/behavior is available;
- no nondeterministic listener capture;
- no network dependency.

Freeze where source/AST inspection is explicitly justified.

Emit:

`RELEASE 1.10 WP06 ASSERTION-STRENGTH CONTRACT: FROZEN`

---

# Phase 12 — Complete invariant allocation matrix

Produce a canonical matrix:

| Invariant ID | WP origin | Exact invariant | Test layer | Exact path | Exact class/module | Exact test symbol | Assertion technique | Existing/New | Helper |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |

Every WP06 requirement must appear exactly once as primary ownership.

Additional corroborating coverage may be listed separately, but no invariant may have ambiguous primary ownership.

Emit:

`RELEASE 1.10 WP06 COMPLETE INVARIANT/TEST OWNERSHIP MATRIX: FROZEN`

---

# Phase 13 — Validation command allocation

Freeze exact commands/suite filters Terra must run for:

- WP06 focused tests;
- Infrastructure;
- Application;
- Architecture;
- Domain;
- Python WP06 tests;
- full Python presentation/architecture;
- relevant build;
- `pip check`;
- Gitleaks.

Do not hard-code final post-WP06 test counts; Terra reports actual counts.

Security scanner remains:

**Gitleaks 8.30.1**

Canonical command:

`gitleaks git . --redact --verbose`

Emit:

`RELEASE 1.10 WP06 VALIDATION COMMAND MATRIX: FROZEN`

---

# Phase 14 — WP07 handoff references

Freeze exact permanent-test locations WP07 must reference in documentation/runbook.

WP07 must be able to state:

- where pipeline observability is permanently tested;
- where Infrastructure observability is permanently tested;
- where Worker/no-exporter is tested;
- where System Health is tested;
- where no-bypass/security architecture is tested;
- exact validation commands.

Do not implement WP07.

Emit:

`RELEASE 1.10 WP06 → WP07 TEST/RUNBOOK HANDOFF: FROZEN`

---

# Phase 15 — Planning artifact reconciliation

Update only minimum authorized planning/architecture artifacts.

The file manifest MUST now enumerate:

- every dedicated WP06 test path;
- every allowed existing test path;
- any helper path;
- exact mutation classification.

The execution plan MUST contain:

- invariant allocation;
- test-layer ownership;
- exact validation commands;
- WP07 handoff.

`OPEN_TELEMETRY_SELECTION.md` should be changed only where permanent enforcement ownership belongs in architecture documentation.

Emit:

`RELEASE 1.10 WP06 TEST-PATH PLANNING RECONCILIATION: PASS`

---

# Phase 16 — Cross-document consistency

Re-read all modified artifacts and prove:

- every path exists or is explicitly authorized as a new WP06 test path;
- every named existing symbol exists;
- every invariant has one primary owner;
- helper ownership is deterministic;
- no production path is accidentally authorized;
- no package/schema/project mutation is introduced;
- WP07 handoff matches test ownership.

Emit:

`RELEASE 1.10 WP06 TEST-PATH CROSS-CONTRACT CONSISTENCY: PASS`

---

# Phase 17 — Terra materialization simulation

Simulate WP06 implementation file-by-file.

For every authorized test path state:

- exact symbol to create/extend;
- exact invariant IDs implemented;
- exact production symbols inspected/invoked;
- exact assertion method;
- exact fixture/helper;
- exact focused command that executes it.

Ask:

**Could two competent Terra implementers choose different test files, symbols, helpers, or assertion layers while both claiming compliance?**

If YES, reconciliation is incomplete.

Only if NO emit:

`RELEASE 1.10 WP06 TEST-PATH MATERIALIZATION SIMULATION: PASS — TERRA-READY`

---

# Phase 18 — Mutation audit

Report exact mutations.

Require:

`RELEASE 1.10 WP06 RECONCILIATION REPOSITORY MUTATIONS: PLANNING/ARCHITECTURE ONLY`

`RELEASE 1.10 WP06 RECONCILIATION PRODUCTION/TEST MUTATIONS: ZERO`

`RELEASE 1.10 WP06 RECONCILIATION PROJECT/PACKAGE/SCHEMA MUTATIONS: ZERO`

`RELEASE 1.10 WP06 RECONCILIATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP06 RECONCILIATION GITHUB MUTATIONS: ZERO`

#247 remains Open/Backlog.

Milestone #59 remains Open.

WP07 does not start.

---

# Phase 19 — Terra resumption handoff

Only after deterministic materialization simulation authorize resumption of:

**Release 1.10 WP06 — Permanent Observability and No-Bypass Tests Authority — GPT-5.6 Terra**

Terra must consume the reconciled manifest/plan and must not reopen path/symbol/invariant ownership.

Emit:

`RELEASE 1.10 WP06 TEST-PATH/ARCHITECTURE-SECURITY SYMBOL RECONCILIATION: PASS`

`RELEASE 1.10 WP06 → TERRA RESUMPTION HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success markers

`RELEASE 1.10 WP06 TEST-PATH RECONCILIATION ENTRY: PASS`

`RELEASE 1.10 WP06 TEST TOPOLOGY INVENTORY: COMPLETE`

`RELEASE 1.10 WP06 DEDICATED TEST PATH STRATEGY: FROZEN`

`RELEASE 1.10 WP06 WP02 INVARIANT/TEST ALLOCATION: FROZEN`

`RELEASE 1.10 WP06 WP03 INVARIANT/TEST ALLOCATION: FROZEN`

`RELEASE 1.10 WP06 WP04 INVARIANT/TEST ALLOCATION: FROZEN`

`RELEASE 1.10 WP06 WP05 INVARIANT/TEST ALLOCATION: FROZEN`

`RELEASE 1.10 WP06 PYTHON/STREAMLIT NO-BYPASS SYMBOL OWNERSHIP: FROZEN`

`RELEASE 1.10 WP06 ARCHITECTURE TEST SYMBOLS: FROZEN`

`RELEASE 1.10 WP06 SECURITY TEST SYMBOLS: FROZEN`

`RELEASE 1.10 WP06 TEST HELPER/FIXTURE CONTRACT: FROZEN`

`RELEASE 1.10 WP06 FORBIDDEN TEST TARGETS: FROZEN`

`RELEASE 1.10 WP06 ASSERTION-STRENGTH CONTRACT: FROZEN`

`RELEASE 1.10 WP06 COMPLETE INVARIANT/TEST OWNERSHIP MATRIX: FROZEN`

`RELEASE 1.10 WP06 VALIDATION COMMAND MATRIX: FROZEN`

`RELEASE 1.10 WP06 → WP07 TEST/RUNBOOK HANDOFF: FROZEN`

`RELEASE 1.10 WP06 TEST-PATH PLANNING RECONCILIATION: PASS`

`RELEASE 1.10 WP06 TEST-PATH CROSS-CONTRACT CONSISTENCY: PASS`

`RELEASE 1.10 WP06 TEST-PATH MATERIALIZATION SIMULATION: PASS — TERRA-READY`

`RELEASE 1.10 WP06 RECONCILIATION REPOSITORY MUTATIONS: PLANNING/ARCHITECTURE ONLY`

`RELEASE 1.10 WP06 RECONCILIATION PRODUCTION/TEST MUTATIONS: ZERO`

`RELEASE 1.10 WP06 RECONCILIATION PROJECT/PACKAGE/SCHEMA MUTATIONS: ZERO`

`RELEASE 1.10 WP06 RECONCILIATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP06 RECONCILIATION GITHUB MUTATIONS: ZERO`

`RELEASE 1.10 WP06 TEST-PATH/ARCHITECTURE-SECURITY SYMBOL RECONCILIATION: PASS`

`RELEASE 1.10 WP06 → TERRA RESUMPTION HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal

`RELEASE 1.10 WP06 — PERMANENT TEST PATH & ARCHITECTURE/SECURITY SYMBOL RECONCILIATION AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if Luna cannot deterministically allocate every WP06 invariant to an exact test path/symbol without changing production architecture or exceeding Release 1.10 scope.

If blocked:

- preserve valid planning analysis;
- production/test/project/package/schema mutations ZERO;
- Git mutations ZERO;
- GitHub mutations ZERO;
- #247 remains Open/Backlog;
- milestone #59 remains Open;
- WP07 does not start;
- report the exact unresolved ownership/governance choice.

Exact blocked terminal:

`RELEASE 1.10 WP06 — PERMANENT TEST PATH & ARCHITECTURE/SECURITY SYMBOL RECONCILIATION AUTHORITY BLOCKED`
