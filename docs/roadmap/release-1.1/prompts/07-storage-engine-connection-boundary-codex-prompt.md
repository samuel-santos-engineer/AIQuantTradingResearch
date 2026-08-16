# Release 1.1 WP07 — Storage Engine & Connection Boundary — Codex Execution Authority

## 1. Authority

You are executing **Release 1.1 — WP07: Storage Engine & Connection Boundary** for the `AIQuantTradingResearch` repository.

This file is the authoritative execution contract for WP07. Execute it literally and conservatively. Do not expand scope by inference.

Accepted predecessor chain:

- WP01 — Release & Repository Preflight — COMPLETE
- WP02 — Persistence Technology Discovery — COMPLETE
- WP03 — Historical Observation Persistence Semantics — COMPLETE
- WP04 — Application Persistence Contracts — COMPLETE
- WP05 — Persistence Use-Case Integration — COMPLETE
- WP06 — Storage Physical Model — COMPLETE
- WP07 — Storage Engine & Connection Boundary — CURRENT
- WP08+ — NOT AUTHORIZED

GitHub planning identity:

- Release: `1.1`
- Milestone: `#52 — Phase 3 - Release 1.1: Market Data Persistence Foundation`
- WP07 issue: `#109 — Storage Engine & Connection Boundary`
- Required predecessor: `#108 — Storage Physical Model`

The Release 1.1 execution plan and file manifest remain governing authorities.

## 2. Mission

Implement the minimum Infrastructure-owned SQLite engine/connection boundary needed by later persistence work.

WP07 owns:

- the authorized SQLite package dependency;
- database-location/configuration representation assigned to WP07;
- SQLite connection creation boundary;
- connection ownership and disposal semantics;
- safe opening/configuration of SQLite connections;
- schema-version-1 bootstrap execution using the WP06 physical schema;
- deterministic bootstrap/version validation necessary to make the storage boundary usable.

WP07 does **not** own:

- observation persistence behavior;
- duplicate/idempotency/conflict implementation;
- historical retrieval;
- storage failure mapping beyond connection/bootstrap concerns explicitly required here;
- Application orchestration changes;
- Worker persistent market-data execution;
- final DI/composition registration assigned to WP11;
- comprehensive persistence tests assigned to WP14;
- Release integration.

## 3. Mandatory Inputs

Before mutation, read and reconcile at minimum:

1. `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`
3. WP01–WP06 authoritative prompts and accepted results available in repository/current execution context.
4. WP02 persistence technology assessment/decision artifacts.
5. WP03 persistence semantics.
6. WP04 persistence contracts.
7. WP05 persistence use case.
8. WP06 physical-model implementation and report.
9. Current central package-management files.
10. Current Infrastructure project configuration.
11. Current configuration/DI architecture documentation.
12. Existing source/test conventions.
13. GitHub issue #109, milestone #52, and Project #2 state.

Accepted predecessor decisions are authoritative. Do not redesign them merely because another SQLite approach is possible.

## 4. Starting-State Gate

Before mutation, verify and report:

- repository identity;
- authenticated GitHub identity without exposing credentials;
- current branch;
- `HEAD`;
- `origin/main`;
- ahead/behind;
- staged files;
- tracked modifications;
- untracked files;
- unexpected files.

Expected lifecycle state:

- issues #103–#108 are Closed/Done;
- issue #109 is Open/Backlog;
- issues #110–#118 remain Open/Backlog;
- milestone #52 remains Open;
- active Release 1.2 planning remains zero.

The repository may contain cumulative authorized Release 1.1 artifacts from prior WPs. Preserve and classify them. Do not delete, stage, commit, or rewrite predecessor work merely to obtain a clean tree.

Stop on unexplained drift that makes WP07 ownership ambiguous.

## 5. Baseline Validation

Before implementation, execute the canonical baseline:

- restore;
- format verification;
- build;
- all permanent tests;
- Architecture.Tests;
- `eng/verify.ps1`;
- `git diff --check`;
- `git diff --cached --check`.

Record exact results.

Do not repair unrelated baseline failures under WP07 authority.

## 6. Issue Lifecycle

After all starting gates pass and immediately before substantive WP07 mutation, move issue #109 / its Project item:

`Backlog` → `In Progress`

No other issue may progress.

Close #109 and verify/allow `Done` only after all WP07 acceptance gates pass.

## 7. Accepted WP06 Physical Model

Treat the following WP06 model as fixed unless repository truth proves the accepted report inaccurate:

Table:

`historical_observations`

SQLite declaration:

`STRICT, WITHOUT ROWID`

Columns:

- `target` — `TEXT COLLATE BINARY NOT NULL`
- `instant_utc_ticks` — `INTEGER NOT NULL`
- `offset_minutes` — `INTEGER NOT NULL`
- `price_text` — `TEXT NOT NULL`

Identity:

`PRIMARY KEY (target, instant_utc_ticks)`

Physical semantics:

- target preserved exactly;
- absolute instant represented by UTC ticks;
- original `DateTimeOffset` offset represented by offset minutes;
- offset constrained to the valid `-840..840` minute range;
- decimal represented losslessly using invariant `G29` text;
- schema version is `1`;
- no redundant retrieval index is required;
- no update/delete/upsert semantic policy exists.

Use the actual WP06 source files as final repository truth.

Do not modify the physical model merely to simplify connection/bootstrap code.

## 8. SQLite Dependency

WP07 is the authorized owner for introducing `Microsoft.Data.Sqlite` if it is not already present.

Requirements:

1. use central package management if the repository uses it;
2. add the package only to the Infrastructure project;
3. select a version consistent with repository/.NET compatibility and existing dependency governance;
4. do not add EF Core;
5. do not add an ORM;
6. do not add a migration framework;
7. do not add unrelated SQLite helper packages.

Record the exact package/version delta.

If the Release 1.1 manifest specifies an exact package/version or file location, follow it.

## 9. Connection Boundary Design

Create the smallest Infrastructure-owned boundary that later WP08/WP09 implementations can use without knowing database-location/configuration mechanics.

The boundary must make connection ownership explicit.

Prefer a focused abstraction over a generic database framework.

It must be clear:

- who creates a connection;
- whether returned connections are already open;
- who owns disposal;
- whether each operation receives a fresh connection;
- how bootstrap is guaranteed before use.

Do not expose SQLite types into Domain or Application.

SQLite-specific types may remain internal to Infrastructure unless the accepted architecture requires otherwise.

## 10. Connection Lifetime

Choose deterministic, simple connection semantics suitable for the Release 1.1 workload.

Unless predecessor authority explicitly requires another model, prefer:

- short-lived connections;
- explicit disposal by the Infrastructure caller that receives them;
- no global mutable `SqliteConnection`;
- no static live connection;
- no hidden process-wide connection ownership.

Do not implement connection pooling policy beyond the behavior naturally provided by the selected SQLite provider unless explicitly required.

## 11. Database Location / Configuration Boundary

Follow the Release 1.1 file manifest and existing configuration conventions.

The database location must:

- be configurable through the appropriate host/configuration boundary;
- not be hard-coded to a developer machine;
- not contain credentials;
- not require a committed database file;
- have deterministic validation;
- remain Infrastructure/host configuration rather than Domain/Application business data.

Do not invent a production deployment path when the execution plan does not define one.

If WP11 owns final DI/configuration binding, WP07 may define only the Infrastructure-side configuration/options contract needed for later registration.

Do not implement WP11 composition-root registration.

## 12. Connection String Handling

If a connection string is constructed:

- construct it using provider-supported APIs where practical;
- avoid hand-built unsafe string concatenation;
- preserve the configured database location exactly where semantically appropriate;
- do not log secrets or sensitive connection data;
- do not introduce credentials.

SQLite connection configuration should be minimal and justified.

Do not enable provider features unrelated to Release 1.1.

## 13. Opening Connections

The connection boundary must fail deterministically when it cannot create/open a usable SQLite connection.

Do not translate connection failures into WP10's final Application persistence failure vocabulary unless WP10 explicitly owns that mapping.

WP07 may surface Infrastructure-specific exceptions/failures appropriate to its boundary, but avoid speculative taxonomy.

Do not swallow SQLite exceptions.

## 14. Schema Bootstrap

WP07 owns execution of the accepted WP06 schema bootstrap.

Bootstrap must:

- use WP06's schema definition as the source of truth;
- create the required schema safely when absent;
- be idempotent when schema version 1 already exists correctly;
- not destroy existing accepted data;
- not drop/recreate the table as a normal startup path;
- not overwrite conflicting schema silently;
- validate enough schema/version state to prevent operating against a known incompatible database;
- execute before later persistence/retrieval operations receive a usable connection.

Do not duplicate the WP06 DDL in multiple independent locations.

## 15. Schema Version

Use WP06's schema version `1`.

If schema-version metadata is required for deterministic bootstrap/version validation, introduce only the minimum mechanism authorized by the manifest and architecture.

Requirements:

- version identity must be deterministic;
- existing supported version 1 must be accepted;
- unsupported/newer/incompatible versions must not be silently treated as version 1;
- no migration framework;
- no speculative version 2 migration.

If SQLite `PRAGMA user_version` is sufficient and compatible with WP06 authority, it may be used. If repository authority establishes another mechanism, use that instead.

Explain the selected strategy in the report.

## 16. Bootstrap Atomicity

Schema creation/version initialization must not leave a partially initialized database under ordinary failure conditions.

Use an appropriate transaction if required to make bootstrap atomic.

Transaction use here is authorized only for schema bootstrap integrity.

This does not authorize WP08 observation-persistence transaction behavior.

## 17. SQLite Pragmas

Do not introduce a broad tuning policy.

Any PRAGMA configured by WP07 must be:

- necessary for correctness or accepted bootstrap/version behavior;
- scoped and documented;
- compatible with the selected connection lifetime.

Do not introduce performance tuning, WAL policy, busy-timeout policy, synchronous-mode changes, foreign-key policy, or other operational tuning without explicit need/authority.

## 18. File-System Behavior

If SQLite uses a file-backed database:

- parent-directory handling must be deterministic;
- do not create arbitrary directories unless the accepted configuration model requires it;
- do not delete existing database files;
- do not replace existing databases to recover from errors;
- do not commit generated `.db`, `.sqlite`, journal, WAL, or SHM files.

Any temporary database used for focused validation must be isolated and removed before completion.

## 19. In-Memory SQLite

An in-memory SQLite database may be used for narrow WP07 validation if useful.

Do not make in-memory storage the production default unless the accepted Release 1.1 configuration explicitly says so.

Be aware that SQLite in-memory database lifetime is connection-sensitive; do not design production connection semantics around a test-only shortcut.

## 20. WP08 Protection

WP08 — Observation Persistence — is not authorized.

Do not implement:

- `IHistoricalObservationStore`;
- insert observation commands;
- duplicate lookup;
- idempotency comparison;
- conflicting-duplicate detection;
- observation write transactions;
- update/upsert/replace behavior.

WP07 may execute only bootstrap/schema SQL necessary to establish a usable storage engine.

## 21. WP09 Protection

WP09 — Historical Observation Retrieval — is not authorized.

Do not implement:

- historical observation SELECT queries;
- row-to-Domain materialization for retrieval;
- ordering/result construction;
- empty-history result behavior.

## 22. WP10 Protection

WP10 — Storage Validation & Failure Mapping — is not authorized.

Do not implement the final mapping of SQLite failures into:

- `PersistenceFailure.Unavailable`;
- `PersistenceFailure.InvalidData`;
- or other Application-level persistence semantics.

WP07 may validate its own configuration and bootstrap preconditions.

## 23. WP11 Protection

WP11 — Dependency Registration & Configuration — is not authorized.

Do not:

- register the storage boundary in Worker DI;
- modify Worker composition to consume it;
- create final configuration binding/wiring;
- change startup orchestration for persistence.

Infrastructure-side types needed for later WP11 registration are allowed only if the manifest assigns them to WP07.

## 24. WP12 Protection

WP12 — Worker Persistent Market-Data Execution — is not authorized.

Do not modify the Worker acquisition lifecycle to persist observations.

No end-to-end acquisition → persistence flow belongs here.

## 25. Tests

Follow `RELEASE_1.1_FILE_MANIFEST.md` exactly.

Comprehensive Infrastructure/persistence testing belongs to WP14.

If WP07's manifest permits focused permanent tests, create only those assigned to WP07.

Otherwise, narrow temporary probes are allowed to validate connection/bootstrap behavior provided they:

- are not committed/staged;
- leave no database residue;
- are removed before completion;
- do not alter permanent test counts.

Useful WP07-focused validation may prove:

- SQLite provider can open the configured database;
- bootstrap creates schema version 1;
- second bootstrap is idempotent;
- `historical_observations` has the WP06 shape;
- unsupported schema version is rejected;
- connection disposal works as designed;
- temporary database artifacts are cleaned.

Do not preempt WP14's comprehensive suite.

## 26. Expected File Scope

`RELEASE_1.1_FILE_MANIFEST.md` is the source of truth for WP07 file ownership.

Use the exact authorized file set where specified.

Do not invent helper files merely for stylistic decomposition.

Any deviation must be minimal, unavoidable, explicitly reported, and impossible to defer to WP08+.

If manifest authority and repository truth conflict, stop.

## 27. Package / Project Reference Accounting

Report:

- central package file modifications;
- Infrastructure project modifications;
- exact `Microsoft.Data.Sqlite` version;
- all package deltas;
- all project-reference deltas.

Expected project-reference delta is `0` unless the manifest explicitly says otherwise.

No new dependency edge may violate the established architecture.

## 28. Architecture

Mandatory dependency graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

WP07 must preserve this graph.

No SQLite/provider type may appear in Domain or Application public contracts.

No Worker change is permitted unless explicitly required by the manifest, and WP11/WP12 responsibilities must remain protected.

## 29. Security

Do not introduce or expose:

- credentials;
- passwords;
- API keys;
- secret-bearing connection strings;
- machine-specific personal paths;
- sensitive logs.

Never print full secret-bearing configuration.

No real external provider credential is required for WP07.

## 30. Whitespace Handling

Before acceptance run:

- format verification;
- `git diff --check`;
- `git diff --cached --check`.

If either diff check reports whitespace in a file authorized for WP07 modification, you may remove only the reported whitespace from that authorized file, preserving semantics.

This authority intentionally handles zero or more WP07 whitespace findings without requiring a recursive unblock authority.

Do not normalize unrelated predecessor files.

## 31. Mutation Accounting

At completion report exactly:

- files added;
- files modified;
- files deleted;
- packages added/changed;
- project references changed;
- permanent tests changed;
- temporary probes created/removed;
- generated SQLite artifacts created/removed;
- unexpected paths.

Do not stage or commit WP07 work.

## 32. Final Validation

After implementation execute:

1. `dotnet restore AIQuantTradingResearch.slnx --nologo`
2. repository format verification
3. solution build
4. Domain.Tests
5. Application.Tests
6. Infrastructure.Tests
7. Architecture.Tests
8. `eng/verify.ps1`
9. `git diff --check`
10. `git diff --cached --check`

Report exact warnings, errors, test totals, failures, and skipped counts.

No red gate may be waived.

## 33. Connection/Bootstrap Validation Matrix

Report PASS/FAIL for:

| Requirement | Required |
|---|---|
| SQLite dependency Infrastructure-owned | PASS |
| Database location configurable | PASS |
| No machine-specific hard-coded path | PASS |
| Connection creation deterministic | PASS |
| Connection ownership explicit | PASS |
| Connection disposal explicit | PASS |
| WP06 schema reused | PASS |
| Schema version 1 bootstrap | PASS |
| First bootstrap safe | PASS |
| Repeated bootstrap idempotent | PASS |
| Existing data not destructively reset | PASS |
| Incompatible version not silently accepted | PASS |
| Bootstrap atomicity | PASS |
| WP06 table/column identity preserved | PASS |
| Domain SQLite leakage | 0 |
| Application SQLite leakage | 0 |
| WP08 persistence behavior implemented | NO |
| WP09 retrieval behavior implemented | NO |
| WP11 DI registration implemented | NO |
| WP12 Worker orchestration implemented | NO |

## 34. Git / GitHub Protection

WP07 must not:

- create a branch;
- stage;
- commit;
- amend;
- reset;
- rebase;
- push;
- create a PR;
- merge;
- tag;
- create a GitHub Release;
- rewrite history.

The only GitHub lifecycle mutation authorized is issue #109 / its existing Project item.

## 35. Planning Protection

Do not:

- modify WP01–WP06 completed planning;
- progress WP08–WP16;
- create WP17+;
- create lifecycle-gate issues;
- change labels;
- change Project schema/options;
- activate Release 1.2;
- close milestone #52.

Normal milestone issue counts and normal Project automation caused by closing #109 are allowed.

## 36. WP08 Handoff

The final report must give WP08 an implementation-ready handoff containing:

- exact SQLite package/version;
- connection-boundary type(s);
- how a WP08 component obtains a usable connection;
- whether the connection is already open;
- disposal ownership;
- bootstrap guarantee;
- database-location/configuration contract;
- schema-version strategy;
- WP06 table/column/primary-key identity;
- any bootstrap transaction behavior relevant to connection use;
- any provider exceptions intentionally left unmapped for WP10.

WP08 must not need to redesign connection ownership.

## 37. Stop Conditions

Stop without claiming completion if:

- WP06 is not accepted;
- issue #109 dependency/lifecycle is incorrect;
- manifest does not authorize required package/files;
- accepted WP06 schema cannot be bootstrapped without changing its semantics;
- implementation requires WP08+ behavior;
- package introduction would violate dependency governance;
- baseline/final validation fails and repair is outside WP07;
- unexpected repository drift makes scope ambiguous;
- architecture boundaries would be violated.

Report the smallest corrective authority required.

## 38. Acceptance Criteria

WP07 is complete only if:

- starting-state/lifecycle gates pass;
- #109 is the only progressed issue;
- accepted SQLite technology is used;
- authorized SQLite package dependency is correctly introduced;
- connection boundary is focused and Infrastructure-owned;
- database location/configuration boundary is deterministic;
- connection lifetime and disposal are explicit;
- WP06 schema version 1 is safely bootstrapped;
- repeated bootstrap is idempotent;
- incompatible schema is not silently accepted;
- bootstrap is non-destructive and appropriately atomic;
- no WP08 persistence behavior exists;
- no WP09 retrieval behavior exists;
- no WP10 final failure mapping exists;
- no WP11 final DI registration exists;
- no WP12 Worker orchestration exists;
- Domain/Application contain no SQLite leakage;
- security gates pass;
- package/reference accounting is exact;
- all technical validation passes;
- both diff checks pass;
- no generated database residue remains;
- no Git integration action occurs;
- WP08 remains Open/Backlog until WP07 completion.

## 39. Successful Completion Actions

Only after all acceptance criteria pass:

1. add concise completion evidence to issue #109 if repository convention requires it;
2. close issue #109;
3. verify/allow its Project item to become `Done`;
4. verify issue #110 remains Open/Backlog;
5. verify milestone #52 remains Open;
6. verify Release 1.2 remains inactive.

Do not begin WP08.

## 40. Required Execution Report

Return a detailed **Release 1.1 WP07 Execution Report** covering at minimum:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. SQLite Dependency Decision
10. WP06 Schema Reconciliation
11. Connection Boundary Design
12. Connection Lifetime/Ownership
13. Database Location/Configuration
14. Connection-String Handling
15. Connection Opening Behavior
16. Schema Bootstrap Design
17. Schema Version Strategy
18. Bootstrap Atomicity
19. SQLite PRAGMA Decisions
20. File-System Behavior
21. Layer Ownership
22. Exact Files Added/Modified
23. Package/Reference Delta
24. Test/Probe Delta
25. WP08 Protection
26. WP09 Protection
27. WP10/WP11/WP12 Protection
28. Security
29. Whitespace/Diff Evidence
30. Restore/Build Evidence
31. Permanent Test Evidence
32. Canonical Verification
33. Architecture Validation
34. Connection/Bootstrap Validation Matrix
35. Mutation Accounting
36. Git/GitHub Protection
37. Planning Protection
38. Findings/Blockers
39. Final Repository/GitHub State
40. WP08 Handoff
41. Final Decision
42. Next Authorized Work Package

## 41. Success Terminal

On complete success, end with:

`RELEASE 1.1 WP07 COMPLETE`

and identify:

`NEXT AUTHORIZED WORK PACKAGE: WP08 — Observation Persistence — GitHub issue #110`

Do not start WP08.
