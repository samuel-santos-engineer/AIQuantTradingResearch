# Release 1.1 WP10 --- Storage Validation & Failure Mapping --- Codex Execution Authority

## 1. Authority

You are executing **Release 1.1 --- WP10: Storage Validation & Failure Mapping** for the `AIQuantTradingResearch` repository.

This file is the authoritative execution contract for WP10. Execute it literally and conservatively. Do not expand scope by inference.

Accepted predecessor chain:

- WP01 --- Release & Repository Preflight --- COMPLETE
- WP02 --- Persistence Technology Discovery --- COMPLETE
- WP03 --- Historical Observation Persistence Semantics --- COMPLETE
- WP04 --- Application Persistence Contracts --- COMPLETE
- WP05 --- Persistence Use-Case Integration --- COMPLETE
- WP06 --- Storage Physical Model --- COMPLETE
- WP07 --- Storage Engine & Connection Boundary --- COMPLETE
- WP08 --- Observation Persistence --- COMPLETE
- WP09 --- Historical Observation Retrieval --- COMPLETE
- WP10 --- Storage Validation & Failure Mapping --- CURRENT
- WP11+ --- NOT AUTHORIZED

GitHub planning identity:

- Release: `1.1`
- Milestone: `#52 — Phase 3 - Release 1.1: Market Data Persistence Foundation`
- WP10 issue: `#112 — Storage Validation & Failure Mapping`
- Required predecessor: `#111 — Historical Observation Retrieval`
- Next issue: `#113 — Dependency Registration & Configuration`

The Release 1.1 execution plan and file manifest remain governing authorities.

## 2. Mission

Complete the minimum Infrastructure-owned validation and failure-mapping boundary required to make the accepted SQLite persistence/retrieval implementation return the already-defined Application persistence vocabulary deterministically.

WP10 must **classify, map, and contain** storage failures. It must not redesign the physical model, persistence semantics, Application contracts, or orchestration.

The completed WP06--WP10 chain must remain:

`physical model → connection/bootstrap → atomic persistence → deterministic retrieval → validation/failure mapping`

WP10 owns:

- reconciliation of the storage failure points deliberately left for WP10 by WP08 and WP09;
- deterministic mapping of authorized SQLite/storage failures into the accepted `PersistenceFailure` vocabulary;
- deterministic treatment of malformed/corrupt persisted rows at the Infrastructure/Application boundary;
- validation necessary to prevent raw SQLite/storage exceptions from leaking through normal persistence/retrieval operations where the accepted contracts can represent the failure;
- preservation of semantic conflict as `ObservationPersistenceOutcome.Conflict`, not infrastructure failure;
- preservation of successful empty retrieval;
- preservation of WP08 atomic/idempotent/conflict write behavior;
- preservation of WP09 exact-target/ascending/fidelity retrieval behavior.

WP10 does **not** own:

- new persistence semantics;
- new public Application failure categories unless the existing accepted contracts prove incapable of representing an explicitly required WP10 case;
- generic exception frameworks;
- retries, circuit breakers, resilience policy, or transient retry orchestration;
- schema redesign, migration framework, repair, destructive reset, or data cleanup;
- DI/configuration registration assigned to WP11;
- Worker orchestration assigned to WP12;
- comprehensive Domain/Application tests assigned to WP13;
- comprehensive Infrastructure/persistence tests assigned to WP14;
- architecture/documentation alignment assigned to WP15;
- Release integration/acceptance assigned to WP16.

## 3. Mandatory Inputs

Before any mutation, read and reconcile completely:

1. `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`
3. WP01--WP09 authoritative prompts and accepted execution evidence available in the repository/current execution context.
4. WP02 persistence technology decision artifacts.
5. WP03 persistence semantics.
6. WP04 Application persistence contracts, especially:
   - `IHistoricalObservationStore`
   - `ObservationPersistenceOutcome`
   - `PersistenceFailure`
   - `ObservationPersistenceResult`
   - `HistoricalObservationResult`
7. WP05 persistence use case and its validation/failure behavior.
8. WP06 SQLite schema, record, and mapper.
9. WP07 connection factory, storage configuration, and schema bootstrapper.
10. WP08 `SqliteHistoricalObservationStore` write implementation.
11. WP09 retrieval implementation and handoff.
12. Current Domain/Application/Infrastructure/Worker project references.
13. Current permanent tests and architecture tests.
14. GitHub issue #112, predecessor #111, next issue #113, milestone #52, and Project #2 planning state.

Do not rely on memory when repository truth is available.

## 4. Starting-State Gate

Before implementation, prove all of the following.

Repository:

- current branch is `main`;
- `main` equals `origin/main`;
- ahead/behind is `0/0`;
- staged tracked changes are `0`;
- no unexpected tracked or untracked path exists;
- cumulative accepted Release 1.1 artifacts may remain untracked/modified only if they are exactly attributable to accepted WP01--WP09 work and current WP10 authority artifacts.

Planning:

- Release 1.0 remains closed;
- milestone #52 is OPEN;
- issues #103--#111 are Closed/Done;
- issue #112 is Open/Backlog;
- issue #113 remains Open/Backlog;
- no WP11 implementation has started;
- active Release 1.2 planning is `0`.

If a legacy Release 1.2 milestone or any other planning drift violates an explicit gate, **STOP before mutation**. Do not repair unrelated GitHub planning unless this authority explicitly permits it.

Record the exact blocker and smallest corrective authority required.

## 5. Working-Tree Classification

Classify every non-HEAD path before mutation into one of:

1. accepted cumulative Release 1.1 governance/research artifact;
2. accepted cumulative WP04--WP09 implementation artifact;
3. current WP10 authority artifact;
4. unexpected/ambiguous artifact.

Unexpected or ambiguous paths are a blocker unless they can be proven harmless without mutation.

Do not stage, commit, delete, relocate, normalize, or rewrite cumulative artifacts merely to make the tree look clean.

## 6. Initial Baseline

After the starting-state gate passes and before moving #112 to In Progress, run:

- `dotnet restore AIQuantTradingResearch.slnx --nologo`
- repository format verification using the canonical engineering script/convention;
- solution build;
- all permanent test suites;
- Architecture.Tests;
- `eng/verify.ps1`;
- `git diff --check`;
- `git diff --cached --check`.

Expected current permanent baseline from accepted WP09 evidence:

- Domain.Tests: 11/11
- Application.Tests: 16/16
- Infrastructure.Tests: 65/65
- Architecture.Tests: 13/13
- Total permanent tests: 105/105
- Build warnings: 0
- Build errors: 0

If repository truth has legitimately changed through accepted predecessor work, reconcile rather than fabricating the expected count. Any unexplained regression is a blocker.

## 7. Issue Lifecycle

Only after all pre-mutation gates and initial baseline pass:

- move issue #112 from `Backlog` to `In Progress`;
- do not modify #113--#118;
- do not modify milestone identity or Project schema.

After every WP10 acceptance gate passes:

- post concise evidence to #112;
- close #112;
- allow/ensure its Project status is `Done` according to existing workflow;
- verify #113 remains Open/Backlog.

Do not start WP11.

## 8. Accepted Failure Vocabulary

WP04 already established the public persistence failure vocabulary:

- `PersistenceFailure.Unavailable`
- `PersistenceFailure.InvalidData`

WP10 must prefer these existing categories.

Interpretation for WP10:

### `Unavailable`

Use for storage-engine/environment/connection/query execution conditions in which the persistence capability cannot successfully complete because the SQLite/storage boundary is unavailable or operationally failed.

Examples may include, subject to actual exception inspection:

- inability to open/use the configured SQLite database;
- SQLite engine operational failures;
- command execution failures caused by storage availability/state;
- bootstrap/schema access failures that represent an unusable storage capability.

### `InvalidData`

Use for data that cannot validly cross the persistence boundary.

Examples may include:

- malformed persisted row representation;
- invalid stored offset/timestamp/decimal representation;
- input rejected by the accepted storage contract where the existing implementation already maps it as invalid data.

Do not invent a third public failure category merely because SQLite exposes more detailed error codes.

If a concrete case cannot truthfully map to either accepted category without semantic distortion, stop and report the contract gap rather than silently broadening WP04.

## 9. Conflict Is Not Failure

Preserve this invariant absolutely:

`ObservationPersistenceOutcome.Conflict` is a semantic persistence outcome, not `PersistenceFailure.Unavailable` and not `PersistenceFailure.InvalidData`.

The accepted WP08 behavior must remain:

- first insert → `NewlyAccepted`;
- semantically equivalent duplicate → `Idempotent`;
- same target + semantic instant with differing persisted semantics → `Conflict`;
- conflict is non-destructive;
- immutable historical value remains unchanged.

Do not catch or translate a valid conflict into an infrastructure failure.

## 10. Successful Empty Retrieval Is Not Failure

Preserve the accepted WP09 invariant:

A valid exact target with no persisted observations returns a **successful, non-null empty historical result**.

It must not become:

- `InvalidData`;
- `Unavailable`;
- null;
- exception;
- synthetic placeholder observation.

## 11. Failure-Surface Discovery

Before changing code, enumerate the actual failure surfaces in WP06--WP09.

At minimum inspect:

- `SqliteHistoricalObservationMapper`;
- `SqliteHistoricalObservationSchema`;
- `SqliteSchemaBootstrapper`;
- `ISqliteConnectionFactory`;
- `SqliteConnectionFactory`;
- `SqliteHistoricalObservationStore`;
- SQLite calls used by persistence and retrieval.

Identify where exceptions can currently escape during:

1. connection creation/open;
2. schema bootstrap/validation;
3. transaction creation/commit/rollback;
4. persistence command execution;
5. duplicate lookup/classification;
6. retrieval command execution;
7. row materialization;
8. mapper reconstruction.

Do not add catch blocks until this surface is understood.

## 12. Mapping Boundary Design

Choose the smallest design that keeps storage-specific exception knowledge in Infrastructure and returns accepted Application results from the store boundary.

Prefer a focused Infrastructure-private mapping mechanism over:

- a generic global exception mapper;
- middleware;
- cross-layer exception types;
- public SQLite-specific types;
- catch-all suppression.

A small private/internal classifier/helper is acceptable if it reduces duplication and has one clear persistence-boundary purpose.

Do not expose `SqliteException`, SQLite error codes, connection types, schema types, or physical records through Application or Domain.

## 13. Exception Specificity

Do not use `catch (Exception)` as the normal mapping strategy.

Catch only exception families that repository truth and the SQLite API demonstrate are part of the authorized storage boundary.

A broad catch is permitted only when narrowly justified for a specific mapper/materialization boundary where the set of possible conversion exceptions cannot otherwise be represented safely, and only if:

- cancellation/termination-style exceptions are not swallowed;
- programming defects are not intentionally hidden;
- the report explains the exact rationale;
- the mapped result is semantically correct.

Do not convert arbitrary bugs into `Unavailable` or `InvalidData`.

## 14. SQLite Error Classification

If `SqliteException` is mapped, inspect its actual SQLite error code/extended code behavior rather than treating every SQLite exception identically by convenience.

The authority does not require a large exhaustive error-code taxonomy.

The goal is a minimal, defensible distinction sufficient for the accepted vocabulary.

Rules:

- constraint behavior already used for WP08 duplicate classification must remain semantically intact;
- expected uniqueness/duplicate handling must not be swallowed by a generic `Unavailable` mapping;
- malformed/corrupt persisted content must not be mislabeled as successful empty retrieval;
- operational engine failures may map to `Unavailable` when that is the truthful accepted abstraction;
- do not introduce retryability semantics.

## 15. Connection / Bootstrap Failures

WP07 owns connection creation and schema bootstrap.

WP10 may add the minimum containment required so that store operations can return an accepted persistence failure when the connection/bootstrap boundary fails during normal use.

Do not:

- change database-path ownership;
- add a default production path;
- redesign `ISqliteConnectionFactory`;
- add DI;
- add retries;
- reset incompatible databases;
- silently migrate unsupported schema versions.

If an incompatible/unsupported schema cannot be truthfully represented under the accepted vocabulary, use the narrowest defensible existing mapping or stop on a proven contract gap.

## 16. Write Failure Mapping

For `Persist`, preserve WP08 semantics and atomicity.

WP10 may map authorized storage failures to `ObservationPersistenceResult` using the existing failure field/vocabulary.

Requirements:

- no partial batch success;
- no overwrite;
- no update/delete;
- duplicate semantic classification remains deterministic;
- transaction behavior remains atomic;
- connection/command/transaction disposal remains deterministic;
- raw SQLite/storage exceptions covered by the accepted mapping must not leak through the normal store contract.

Input-validation behavior already accepted in WP08/WP05 must remain unchanged unless a narrowly necessary correction is proven.

## 17. Retrieval Failure Mapping

For `Retrieve`, preserve WP09 semantics.

Requirements:

- exact target equality;
- explicit ascending `instant_utc_ticks`;
- exact timestamp/offset reconstruction;
- exact decimal reconstruction;
- successful empty result;
- no cross-target leakage.

Authorized query/connection failures should map to the existing retrieval failure representation using `PersistenceFailure`.

Do not return partial history after a mid-read failure.

If any selected row is malformed, the operation must fail deterministically rather than:

- skipping the row;
- repairing it;
- returning partial history;
- substituting defaults;
- silently truncating results.

## 18. Corrupt / Malformed Persisted Rows

WP09 intentionally left malformed-row behavior for WP10.

Treat persisted representation that cannot reconstruct a valid accepted Domain observation as invalid persisted data.

The preferred public abstraction is:

`PersistenceFailure.InvalidData`

subject to the actual WP04 result shape.

Examples to validate where feasible:

- invalid `offset_minutes`;
- invalid/inconsistent tick representation;
- invalid `price_text`;
- values that cannot reconstruct a valid `PriceObservation`.

Do not mutate the database to repair the row.

Do not delete the row.

Do not log the full database content.

Do not leak the physical record.

## 19. Schema Validation Boundary

Preserve WP07's schema-version strategy:

- `PRAGMA user_version`;
- supported versions remain exactly those already accepted;
- schema version 1 remains authoritative for Release 1.1;
- incompatible schema remains rejected;
- no migration framework.

WP10 may classify schema/bootstrap failure at the store boundary. It does not own schema evolution.

## 20. Input Validation Preservation

Preserve existing accepted validation:

- blank target remains invalid;
- invalid/null persistence request content remains invalid according to WP05/WP08;
- target is not trimmed, normalized, case-folded, parsed, or rewritten;
- Domain invariants remain Domain-owned.

Do not turn caller-invalid input into `Unavailable`.

## 21. Atomicity and Immutability Protection

Revalidate WP08 atomicity after adding failure mapping.

Required invariants:

- all-or-nothing write batch;
- failed write does not leave partial rows;
- conflicting duplicate does not mutate existing row;
- equivalent duplicate remains idempotent;
- no update/delete/upsert/replace semantics;
- rollback behavior remains effective on write failure.

Failure mapping must not accidentally commit a transaction that would previously fail.

## 22. Fidelity Protection

WP10 must not alter WP06/WP09 representation:

- target: exact opaque string;
- instant identity/order: UTC ticks;
- original offset representation: offset minutes;
- decimal: invariant lossless text;
- mapping: accepted `SqliteHistoricalObservationMapper`.

No floating-point intermediate is authorized.

No timestamp normalization that loses original offset representation is authorized.

## 23. Permanent Test Scope

The Release 1.1 plan reserves comprehensive Infrastructure/persistence testing for WP14.

Therefore WP10 should **not** expand into the full WP14 permanent test suite unless the file manifest explicitly assigns a permanent WP10 test delta.

Use the narrowest validation mechanism permitted by repository conventions:

- existing permanent tests;
- temporary focused probe(s), if necessary;
- direct bounded execution against temporary SQLite databases.

Any temporary probe/project/file/database must be removed before completion.

If a tiny permanent test is strictly required to make a WP10 production change safe and the manifest permits it, justify it explicitly. Do not silently consume WP14.

## 24. Focused Failure Validation

Validate, at minimum, the cases that can be induced safely without weakening the repository:

### Persistence

- normal new write remains successful;
- equivalent duplicate remains idempotent;
- semantic conflict remains `Conflict`;
- an authorized SQLite operational failure maps to `Unavailable`;
- failure does not create partial history.

### Retrieval

- normal retrieval remains successful;
- empty target remains successful empty;
- malformed persisted row maps deterministically to `InvalidData`;
- an authorized SQLite operational/query failure maps to `Unavailable`;
- failed retrieval does not return partial history.

### Boundary

- raw covered SQLite exceptions do not escape the store contract;
- programming defects are not intentionally swallowed;
- temporary database residue is zero.

If a scenario cannot be induced without altering production design or relying on unsafe platform tricks, document the limitation and validate the closest deterministic boundary. Do not fake evidence.

## 25. WP06 Protection

Do not change the accepted physical model unless a proven defect makes WP10 impossible.

Expected unchanged model:

- table `historical_observations`;
- `STRICT, WITHOUT ROWID`;
- `target TEXT COLLATE BINARY NOT NULL`;
- `instant_utc_ticks INTEGER NOT NULL`;
- `offset_minutes INTEGER NOT NULL`;
- `price_text TEXT NOT NULL`;
- primary key `(target, instant_utc_ticks)`;
- schema version 1.

Any required physical-model change is a stop condition requiring explicit authority.

## 26. WP07 Protection

Do not expand connection/bootstrap responsibilities.

No:

- DI registration;
- Worker configuration;
- default database path;
- pooling policy;
- WAL/synchronous/cache tuning;
- migration framework;
- retry policy;
- health-check framework.

## 27. WP08 Protection

Do not redesign write behavior.

The WP08 write implementation may be edited only where necessary to add WP10 validation/failure containment while preserving its accepted semantics.

Before/after focused regression evidence is mandatory.

## 28. WP09 Protection

Do not redesign retrieval behavior.

The WP09 retrieval implementation may be edited only where necessary to add WP10 validation/failure containment while preserving:

- exact target scoping;
- explicit ascending order;
- mapper reconstruction;
- offset fidelity;
- decimal fidelity;
- successful empty retrieval.

## 29. WP11 Protection

WP11 owns Dependency Registration & Configuration.

WP10 must not:

- register `IHistoricalObservationStore`;
- register connection factories;
- bind `Persistence:DatabasePath`;
- add service-collection extensions for final composition;
- change Worker DI.

## 30. WP12 Protection

WP12 owns Worker Persistent Market-Data Execution.

WP10 must not:

- persist acquired market data from Worker;
- add Worker lifecycle orchestration;
- add host-level persistence execution;
- change acquisition/persistence sequencing in Worker.

## 31. WP13--WP16 Protection

Do not begin:

- WP13 Domain & Application Tests;
- WP14 Infrastructure & Persistence Tests;
- WP15 Architecture & Documentation Alignment;
- WP16 Full Validation, Integration & Acceptance.

Normal regression execution of existing tests is mandatory and does not count as starting those work packages.

## 32. Expected File Scope

Prefer the smallest Infrastructure-only production delta.

Likely authorized modification targets, subject to repository truth:

- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationStore.cs`
- one focused internal SQLite failure-classification/helper file if justified.

Potentially authorized WP10-local validation artifacts are temporary only unless the Release 1.1 file manifest explicitly assigns permanent test files.

Do not modify Domain or Application contracts merely for implementation convenience.

Do not modify Worker.

Do not modify package/project references unless an unavoidable, explicitly justified WP10 requirement is proven. The expected package/reference delta is `0/0`.

Before completion, report every added/modified/deleted file and why it belongs to WP10.

## 33. Package / Reference Accounting

Expected:

- new NuGet packages: `0`;
- project-reference changes: `0`;
- SQLite package version change: `0`.

`Microsoft.Data.Sqlite` remains the accepted WP07 dependency.

If a new dependency appears necessary, STOP and request authority instead of adding it.

## 34. Architecture

The production dependency graph must remain:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Required:

- cycles: `0`;
- SQLite types in Domain: `0`;
- SQLite types in Application: `0`;
- Infrastructure persistence records in public Application/Domain contracts: `0`.

Run all Architecture.Tests.

## 35. Security

Do not expose or log:

- API keys;
- credentials;
- connection secrets;
- full connection strings where sensitive information could later appear;
- arbitrary database contents;
- personal machine paths in committed code;
- raw persisted financial data as diagnostic dumps.

Temporary SQLite databases must use safe disposable locations and be removed.

No external provider calls are required for WP10.

## 36. Logging / Observability

WP10 does not authorize a new logging framework.

Do not add noisy exception logging merely because failures are now mapped.

If existing repository conventions require logging at this boundary, keep it minimal and non-sensitive. Otherwise return the accepted result without adding logging.

## 37. Whitespace Handling

Run:

- `git diff --check`
- `git diff --cached --check`

If either reports whitespace introduced by WP10, correct only the reported whitespace in WP10-touched files.

Do not normalize unrelated cumulative files.

Benign Git line-ending notices are not failures by themselves; actual diff-check findings are.

## 38. Mutation Accounting

At completion classify:

- production files added;
- production files modified;
- production files deleted;
- permanent test files added/modified;
- temporary probes created/removed;
- package changes;
- project-reference changes;
- unexpected paths;
- staged paths.

Expected staging: `0`.

This WP authority does not authorize commit/integration.

## 39. Final Validation

After implementation, run at minimum:

1. restore;
2. format verification;
3. build;
4. Domain.Tests;
5. Application.Tests;
6. Infrastructure.Tests;
7. Architecture.Tests;
8. `eng/verify.ps1`;
9. `git diff --check`;
10. `git diff --cached --check`;
11. focused WP10 failure-validation scenarios;
12. WP08 write regression scenarios;
13. WP09 retrieval regression scenarios;
14. temporary-artifact cleanup check.

Expected build:

- warnings: `0`;
- errors: `0`.

All permanent tests must pass.

## 40. Failure-Mapping Validation Matrix

The final report must explicitly give PASS/FAIL/NOT APPLICABLE for:

| Requirement | Result |
| --- | --- |
| Existing WP04 failure vocabulary reused | |
| New public failure category introduced | |
| Conflict remains semantic outcome | |
| Equivalent duplicate remains idempotent | |
| Conflict remains non-destructive | |
| Write atomicity preserved | |
| Connection/bootstrap operational failure mapping | |
| Write operational failure → `Unavailable` | |
| Retrieval operational failure → `Unavailable` | |
| Malformed stored row → `InvalidData` | |
| Raw covered SQLite exception leakage | |
| Exact target retrieval preserved | |
| Ascending retrieval preserved | |
| Timestamp/offset fidelity preserved | |
| Decimal fidelity preserved | |
| Successful empty retrieval preserved | |
| Partial history returned on failure | |
| Database repair/deletion introduced | |
| Retry/resilience policy introduced | |
| Domain/Application SQLite leakage | |
| WP11 DI/configuration started | |
| WP12 Worker orchestration started | |
| Temporary SQLite/probe residue | |

For negative invariants, report the factual count/state, e.g. `0`, `NO`, or `PASS — none`.

## 41. Git / GitHub Protection

This authority does **not** authorize:

- branch creation;
- staging;
- commit;
- push;
- PR;
- merge;
- tag;
- GitHub Release;
- history rewrite;
- force operations.

Repository implementation remains local cumulative Release 1.1 work.

Only issue #112 lifecycle/evidence mutation is authorized after gates pass.

## 42. Planning Protection

Preserve:

- milestone #52 identity and OPEN state;
- issues #103--#111 Closed/Done;
- #113--#118 Open/Backlog;
- Project Release `1.1`;
- Priority/Area mappings;
- legacy #42/#43 historical state unless a separately authorized reconciliation occurs;
- active Release 1.2 planning = `0`.

Do not create WP17+, lifecycle-gate issues, Release 1.2 objects, or Project schema options.

## 43. WP11 Handoff

If WP10 succeeds, provide a precise handoff for WP11.

At minimum identify:

- the concrete Infrastructure store implementation that should satisfy `IHistoricalObservationStore`;
- the connection factory/configuration types requiring composition;
- the exact configuration key already accepted (`Persistence:DatabasePath`);
- disposal/lifetime expectations;
- which failures are now mapped at the storage boundary;
- confirmation that WP11 does not need to redesign persistence/retrieval semantics.

Do not implement the handoff.

## 44. Stop Conditions

STOP immediately and report rather than infer authority if any of the following occurs:

- starting-state/planning gate fails;
- predecessor #111 is not Closed/Done;
- #112 is not Open/Backlog before authorized lifecycle transition;
- active Release 1.2 planning is nonzero;
- accepted WP04 failure vocabulary cannot truthfully represent a required WP10 case;
- a Domain/Application public-contract change appears necessary;
- a physical-schema change appears necessary;
- a new package/reference appears necessary;
- failure mapping would require generic suppression of arbitrary programming defects;
- write atomicity or conflict semantics cannot be preserved;
- retrieval fidelity/ordering/empty-success cannot be preserved;
- permanent test regression cannot be explained and corrected within WP10;
- unexpected repository artifacts cannot be reconciled;
- WP11/WP12 work becomes necessary.

The execution report must name the smallest additional authority required.

## 45. Acceptance Criteria

WP10 is accepted only if all applicable criteria pass:

1. Starting-state and predecessor gates pass.
2. Initial technical baseline passes.
3. #112 follows Backlog → In Progress → Closed/Done only at authorized points.
4. Existing WP04 failure vocabulary is reused unless a proven contract blocker stops execution.
5. Storage operational failures are deterministically contained/mapped.
6. Malformed persisted rows fail deterministically as invalid data.
7. Valid conflict remains `ObservationPersistenceOutcome.Conflict`.
8. Equivalent duplicate remains idempotent.
9. Write atomicity and immutable history remain intact.
10. Retrieval exact-target behavior remains intact.
11. Retrieval ascending ordering remains intact.
12. Timestamp/offset fidelity remains intact.
13. Decimal fidelity remains intact.
14. Successful empty retrieval remains intact.
15. No partial retrieval result is returned after failure.
16. No destructive repair/reset/delete behavior is introduced.
17. No retry/resilience policy is introduced.
18. Domain/Application remain SQLite-independent.
19. Package/reference delta is `0/0`.
20. WP11 and WP12 remain unstarted.
21. Restore/build/format/canonical verification pass.
22. All permanent tests and Architecture.Tests pass.
23. Both Git diff checks pass.
24. Temporary artifacts/residue are zero.
25. No Git integration operation occurs.
26. Issue #113 remains Open/Backlog.

## 46. Successful Completion Actions

Only after every acceptance criterion passes:

1. post concise WP10 evidence to issue #112;
2. close #112;
3. verify Project status is `Done`;
4. verify #113 remains Open/Backlog;
5. verify milestone #52 remains OPEN;
6. verify active Release 1.2 planning remains `0`;
7. emit the required execution report;
8. stop.

Do not begin WP11.

## 47. Required Execution Report

Return a structured **Release 1.1 WP10 Execution Report** covering, at minimum:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. WP03 Semantic Reconciliation
10. WP04 Failure-Contract Reconciliation
11. WP06 Physical-Model Reconciliation
12. WP07 Connection/Bootstrap Reconciliation
13. WP08 Write Reconciliation
14. WP09 Retrieval Reconciliation
15. Failure-Surface Discovery
16. Mapping Boundary Design
17. SQLite Error Classification
18. Connection/Bootstrap Failure Mapping
19. Write Failure Mapping
20. Retrieval Failure Mapping
21. Conflict Preservation
22. Successful Empty Retrieval Preservation
23. Malformed/Corrupt Row Handling
24. Input Validation Preservation
25. Atomicity/Immutability Protection
26. Fidelity Protection
27. Exception Specificity
28. Exact Files Added/Modified
29. Package/Reference Delta
30. Test/Probe Delta
31. WP11/WP12 Protection
32. Security
33. Whitespace/Diff Evidence
34. Restore/Build Evidence
35. Permanent Test Evidence
36. Canonical Verification
37. Architecture Validation
38. Failure-Mapping Validation Matrix
39. Mutation Accounting
40. Git/GitHub Protection
41. Planning Protection
42. Findings/Blockers
43. Final Repository/GitHub State
44. WP11 Handoff
45. Final Decision
46. Next Authorized Work Package

Do not claim evidence that was not actually executed.

## 48. Success Terminal

If and only if every WP10 acceptance gate passes, end with:

`RELEASE 1.1 WP10 COMPLETE`

Then:

`STORAGE VALIDATION & FAILURE MAPPING:`
`Accepted failure vocabulary reused: PASS`
`Semantic conflict preserved: PASS`
`Write atomicity preserved: PASS`
`Retrieval semantics preserved: PASS`
`Malformed stored data classification: PASS`
`Raw covered SQLite exception leakage: 0`
`Domain/Application SQLite leakage: 0`
`Package/reference delta: 0/0`
`WP11 started: NO`
`WP12 started: NO`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP11 — Dependency Registration & Configuration — GitHub issue #113`

If any mandatory gate fails, do **not** emit the success terminal. End with:

`RELEASE 1.1 WP10 BLOCKED`
