# Release 1.1 WP14 — Infrastructure & Persistence Tests — Codex Execution Authority

## 1. Authority

You are executing **Release 1.1 — WP14: Infrastructure & Persistence Tests** for the `AIQuantTradingResearch` repository.

This file is the authoritative execution contract for WP14. Execute it literally and conservatively. Do not expand scope by inference.

Accepted predecessor chain:

- WP01 — Release & Repository Preflight — COMPLETE
- WP02 — Persistence Technology Discovery — COMPLETE
- WP03 — Historical Observation Persistence Semantics — COMPLETE
- WP04 — Application Persistence Contracts — COMPLETE
- WP05 — Persistence Use-Case Integration — COMPLETE
- WP06 — Storage Physical Model — COMPLETE
- WP07 — Storage Engine & Connection Boundary — COMPLETE
- WP08 — Observation Persistence — COMPLETE
- WP09 — Historical Observation Retrieval — COMPLETE
- WP10 — Storage Validation & Failure Mapping — COMPLETE
- WP11 — Dependency Registration & Configuration — COMPLETE
- WP12 — Worker Persistent Market-Data Execution — COMPLETE
- WP13 — Domain & Application Tests — COMPLETE
- WP14 — Infrastructure & Persistence Tests — CURRENT
- WP15+ — NOT AUTHORIZED

GitHub planning identity:

- Release: `1.1`
- Milestone: `#52 — Phase 3 - Release 1.1: Market Data Persistence Foundation`
- WP14 issue: `#116 — Infrastructure & Persistence Tests`
- Required dependencies: `#108–#114`
- Immediate lifecycle predecessor: `#115 — Domain & Application Tests` is Closed/Done
- Next issue: `#117 — Architecture & Documentation Alignment`

The Release 1.1 execution plan and file manifest remain governing authorities.

## 2. Mission

Create the permanent **Infrastructure and persistence regression suite** for the Release 1.1 storage slice.

WP14 owns permanent tests that prove the accepted WP06–WP12 Infrastructure/runtime behavior, including:

- SQLite physical schema and bootstrap;
- physical mapping fidelity;
- connection ownership/lifecycle;
- persistent write behavior;
- idempotent duplicates;
- deterministic conflicting duplicates;
- atomic batch rollback;
- immutable history;
- historical retrieval;
- target isolation;
- timestamp/offset fidelity;
- decimal fidelity;
- successful empty retrieval through real SQLite;
- storage validation/failure mapping;
- Infrastructure DI/configuration behavior;
- database-file lifecycle and cleanup;
- selected WP12 production composition behavior only where the behavior is Infrastructure-owned and can be proven without duplicating Worker end-to-end semantics.

WP14 does **not** own:

- new production persistence behavior;
- Domain/Application contract tests already completed by WP13;
- new Worker orchestration behavior;
- architecture-rule expansion or documentation alignment assigned to WP15;
- full Release acceptance/integration assigned to WP16;
- Git integration.

## 3. Mandatory Inputs

Before mutation, read and reconcile completely:

1. `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`
3. WP01–WP13 authoritative prompts and accepted execution evidence available in repository/current execution context.
4. WP13 handoff and its exact coverage boundaries.
5. WP06:
   - `SqliteHistoricalObservationRecord`
   - `SqliteHistoricalObservationMapper`
   - `SqliteHistoricalObservationSchema`
6. WP07:
   - `SqliteStorageConfiguration`
   - `ISqliteConnectionFactory`
   - `SqliteConnectionFactory`
   - `SqliteSchemaBootstrapper`
7. WP08–WP10:
   - `SqliteHistoricalObservationStore`
   - write, retrieval, and failure-mapping behavior
8. WP11:
   - Infrastructure DI registration
   - `Persistence:DatabasePath` handoff
9. WP12:
   - Application persistence-use-case registration
   - Worker composition changes, only to understand integration boundaries
10. Current `AIQuantTradingResearch.Infrastructure.Tests` project and all existing tests.
11. Current Architecture.Tests, only for regression awareness.
12. Current package-management and project-reference files.
13. GitHub issue #116, dependencies, milestone #52, and Project state.

Repository truth governs exact file names, signatures, test style, and current behavior.

## 4. Starting-State Gate

Before mutation, verify and report:

Repository:

- repository identity is `samuel-santos-engineer/AIQuantTradingResearch`;
- current branch is `main`;
- `main` equals `origin/main`;
- ahead/behind is `0/0`;
- staged files are `0`;
- all current changes classify as accepted cumulative Release 1.1 work plus the WP14 prompt pair;
- unexpected paths are `0`.

Planning:

- issues #103–#115 are Closed/Done;
- issue #116 is Open/Backlog;
- issue #117 is Open/Backlog;
- milestone #52 is Open;
- active Release 1.2 planning is `0`;
- WP15 implementation has not begun.

Implementation:

- WP06–WP12 accepted production behavior is present;
- WP13 permanent Domain/Application tests are present;
- no conflicting Infrastructure/persistence implementation drift exists.

If any mandatory state is false, stop before mutation and report the smallest corrective authority required.

Do not automatically reconcile unrelated GitHub governance drift.

## 5. Initial Technical Baseline

Before moving issue #116 to `In Progress`, run:

1. restore;
2. format verification;
3. build;
4. Domain.Tests;
5. Application.Tests;
6. Infrastructure.Tests;
7. Architecture.Tests;
8. `eng/verify.ps1`;
9. `git diff --check`;
10. `git diff --cached --check`.

Expected predecessor baseline from WP13:

- Domain.Tests: 11/11
- Application.Tests: 42/42
- Infrastructure.Tests: 65/65
- Architecture.Tests: 13/13
- Total: 131/131
- Build warnings: 0
- Build errors: 0

These are historical expectations only. Report repository truth.

Only after the baseline passes may issue #116 move:

`Backlog → In Progress`

## 6. Test Ownership Boundary

WP14 is the permanent **Infrastructure & Persistence** suite.

### WP14 owns

- SQLite physical model verification;
- schema bootstrap/version behavior;
- connection factory behavior;
- physical mapper round trips;
- store persistence;
- duplicate/conflict physical semantics;
- batch atomicity;
- immutable-history persistence behavior;
- retrieval SQL/order/fidelity;
- successful empty retrieval through SQLite;
- WP10 error/failure mapping;
- concrete Infrastructure DI registration/configuration;
- disposable database lifecycle and cleanup.

### WP14 does not own

- Domain invariants already covered by Domain.Tests;
- Application contract/result semantics already covered by WP13;
- WP05 pure use-case validation/forwarding tests already covered by WP13;
- Worker end-to-end acquisition/persistence behavior already proved in WP12;
- new architecture-rule tests unless the manifest explicitly assigns them to WP14;
- documentation alignment.

Do not duplicate WP13 tests through SQLite unless physical behavior itself requires proof.

## 7. Existing Infrastructure Test Inventory

Before adding tests:

1. read every current Infrastructure.Tests file;
2. classify each existing test by responsibility;
3. identify Release 1.0 provider coverage;
4. identify any existing WP06–WP12 persistence coverage;
5. map required WP14 scenarios against existing coverage;
6. create the minimum permanent delta.

The report must include an existing-coverage matrix before mutation.

Do not rewrite established provider tests merely to standardize style.

## 8. Test Project / Dependency Reconciliation

Inspect the current Infrastructure.Tests project.

Use existing packages/references where possible.

Expected package delta:

`0`

Expected project-reference delta:

`0`

If the concrete Microsoft DI package is already present from prior work, reuse it.

Do not add:

- EF Core;
- Dapper;
- mocking libraries;
- testcontainers;
- embedded migration frameworks;
- another SQLite provider;
- filesystem abstraction packages;
- assertion libraries not already used.

If a genuinely missing test-only dependency is required and the Release 1.1 manifest does not authorize it, stop and report the blocker rather than adding it.

## 9. SQLite Test Isolation

Every SQLite-backed permanent test must be isolated and deterministic.

Preferred approach:

- unique temporary database per test or per tightly controlled test fixture;
- test-owned temporary directory/path;
- no repository-local persistent database;
- no shared machine-global database;
- no reliance on execution order;
- cleanup in `finally`/disposal;
- zero `.db`, `.sqlite`, `-wal`, `-shm`, or journal residue after test completion.

Do not use a production database path.

Do not require network access.

If in-memory SQLite cannot faithfully test the production connection lifecycle, use disposable file-backed databases instead.

## 10. WP06 Physical Schema Tests

Permanently prove the accepted physical model.

At minimum:

- table `historical_observations` exists after bootstrap;
- schema is `STRICT`;
- schema is `WITHOUT ROWID`;
- exact columns exist:
  - `target`
  - `instant_utc_ticks`
  - `offset_minutes`
  - `price_text`
- expected SQLite types are correct;
- nullability is correct;
- `target` uses exact/binary comparison semantics as accepted;
- composite primary key is `(target, instant_utc_ticks)`;
- schema version is `1`;
- no unexpected persistence column is introduced.

Avoid brittle string-only DDL assertions when SQLite metadata can prove behavior more reliably.

## 11. Schema Bootstrap Tests

Permanently prove WP07 bootstrap behavior.

At minimum:

1. version-0 empty database bootstraps to version 1;
2. required schema is created;
3. repeated bootstrap is idempotent;
4. unrelated accepted data is preserved;
5. unsupported schema version is rejected;
6. incompatible version-0 schema is rejected;
7. failed bootstrap does not destructively replace existing state;
8. bootstrap leaves no partial schema/version stamp under ordinary failure;
9. no migration beyond version 1 is performed.

Do not add migration behavior to make tests pass.

## 12. Connection Factory Tests

Permanently prove:

- blank path/configuration is rejected according to accepted semantics;
- configured path is honored;
- factory returns an open connection;
- separate calls return separate connection instances;
- caller disposal closes the connection;
- no live connection is retained by the DI container/factory;
- schema is ready when returned connection is usable;
- failed open/bootstrap does not leak a live connection;
- no hidden in-memory fallback occurs.

Do not assert implementation details unrelated to accepted semantics.

## 13. Mapper Round-Trip Tests

Permanently prove WP06 mapper fidelity independently of the store where practical.

At minimum:

- exact target preservation;
- semantic instant preservation;
- original non-zero offset preservation;
- positive high-precision decimal round trip;
- very small positive decimal where valid;
- `decimal.MaxValue` where valid;
- invariant-culture behavior;
- invalid offset rejected;
- malformed decimal rejected;
- invalid reconstructed Domain value rejected.

Do not use floating-point intermediates.

## 14. Persistence — New Observation Tests

Through real `SqliteHistoricalObservationStore` and real SQLite:

- one new observation → `NewlyAccepted`;
- multiple new observations → `NewlyAccepted`;
- all rows are durably present afterward;
- exact target stored;
- timestamp/offset/decimal fidelity preserved.

Use the production connection factory/mapper/store path rather than bypassing it with ad hoc SQL, except for independent verification queries where necessary.

## 15. Persistence — Idempotency Tests

Permanently prove:

- repeat of one equivalent observation → `Idempotent`;
- repeat of equivalent batch → `Idempotent`;
- row count does not increase;
- stored values remain unchanged;
- no update/delete/replace occurs as a semantic effect.

Where mixed new + equivalent batch behavior is accepted as `NewlyAccepted`, prove it permanently.

Do not redefine the outcome.

## 16. Persistence — Conflict Tests

Permanently prove:

- same target + same semantic instant + different decimal → `Conflict`;
- same target + same semantic instant + different original offset representation → `Conflict`, according to accepted WP08 semantics;
- conflicting operation is non-destructive;
- previously accepted row remains byte/semantic-equivalent;
- conflict is not `Unavailable`;
- conflict is not `InvalidData`;
- no overwrite or replacement occurs.

## 17. Batch Atomicity Tests

Permanently prove all-or-nothing behavior.

At minimum:

- batch with early new row and later conflict leaves no partial new row if accepted semantics require rollback;
- batch write failure leaves no partial acceptance;
- original history remains unchanged after rollback;
- successful multi-row batch persists all rows.

Use deterministic induced failure mechanisms.

Do not rely on timing or thread races for basic atomicity proof.

## 18. Immutable History Tests

Prove through public store behavior that accepted history is immutable.

Required evidence:

- equivalent duplicate does not mutate;
- conflict does not mutate;
- no update/delete/replace public behavior exists;
- retrieval after conflict returns the original observation.

Do not test source-text absence only; behavior is primary.

## 19. Retrieval — Successful Empty Tests

Through real SQLite:

- valid exact target with no rows returns success;
- observations collection is non-null;
- observations collection is empty;
- failure is absent;
- no placeholder Domain object is fabricated.

This complements, but does not duplicate, WP13's pure Application result-contract test: WP14 proves real SQLite/store behavior.

## 20. Retrieval — Exact Target Isolation

Permanently prove:

- exact case-sensitive target matching;
- leading/trailing/internal whitespace preserved when target permits it;
- target A never returns target B rows;
- no wildcard behavior;
- no prefix behavior;
- no normalization/case folding.

Use at least two targets whose values would expose accidental collation/normalization errors.

## 21. Retrieval — Ordering Tests

Persist observations in an order different from chronological order across one or more batches where the write API permits it, then prove retrieval returns:

`semantic instant ascending`

Do not rely on insertion order or physical primary-key assumptions alone.

Where WP05 prevents unsorted direct use-case input, WP14 may seed via direct store calls or safe SQL only if necessary and still within Infrastructure test scope.

## 22. Retrieval — Fidelity Tests

Through real persisted retrieval, permanently prove:

- semantic instant exact;
- original offset exact;
- high-precision decimal exact;
- `decimal.MaxValue` exact where valid;
- multiple offsets and dates round-trip;
- no current-culture influence.

Use fixed deterministic dates/offsets.

## 23. Retrieval After Multiple Batches

Persist history through multiple calls and prove the retrieved history:

- contains all accepted observations exactly once;
- remains ascending;
- preserves target isolation;
- preserves fidelity;
- remains stable after idempotent re-execution.

## 24. WP10 Failure Mapping Tests

Permanent Infrastructure tests must cover the accepted failure boundary without expanding it.

At minimum validate, where deterministically inducible:

### `Unavailable`

- connection/open failure maps to `PersistenceFailure.Unavailable`;
- operational SQLite failure classified as `Unavailable` does not leak a covered `SqliteException`;
- failed persistence returns no partial acceptance;
- failed retrieval returns no partial history.

### `InvalidData`

- malformed `price_text` row → `InvalidData`;
- invalid offset/timestamp representation → `InvalidData`;
- other explicitly accepted malformed-row reconstruction failures → `InvalidData`.

### Unknown SQLite errors

If the accepted WP10 design intentionally allows unknown/unclassified SQLite errors to propagate rather than fabricate a mapping, test that behavior only if it can be induced deterministically without relying on unstable provider internals.

Do not broaden the production classifier merely to create a test.

## 25. Corrupt Row Test Technique

To validate malformed stored rows, direct SQL seeding is allowed inside Infrastructure tests because the purpose is to simulate persisted corruption that the public store cannot generate.

Requirements:

- seed only the minimum invalid state;
- document the reason;
- never use direct SQL as a substitute for testing normal public persistence behavior;
- ensure the corrupted database is disposable;
- prove retrieval does not repair/delete/skip the row silently.

## 26. Concrete DI / Configuration Tests

WP11 established production DI/configuration behavior.

WP14 must permanently test the Infrastructure-specific composition contract where permitted by the file manifest.

At minimum consider:

- production registration resolves `IHistoricalObservationStore` to `SqliteHistoricalObservationStore`;
- `ISqliteConnectionFactory` resolves correctly;
- exactly one intended production store registration exists;
- configured `Persistence:DatabasePath` is used;
- missing/blank path is deterministic;
- service resolution alone creates no database file;
- no hidden in-memory fallback;
- operation-owned connections remain intact.

Do not retest WP12 Worker orchestration here.

If a production `AddInfrastructure` overload also configures Twelve Data, provide safe placeholder provider configuration and prove no provider call occurs.

## 27. Release 1.0 Provider Regression

Infrastructure.Tests already contain Release 1.0 provider coverage.

WP14 must preserve all existing provider tests unchanged unless a genuine regression requires a minimal fix.

Do not mix SQLite test setup into Twelve Data tests.

No live Twelve Data call is permitted.

## 28. Concurrency / Race Coverage Decision

WP08 required deterministic uniqueness-race behavior, but WP14 should not introduce flaky multithreaded tests without evidence that such tests can be deterministic.

Inspect current implementation.

If a deterministic concurrency test can be built with controlled synchronization and SQLite semantics, add it only if the Release 1.1 manifest expects this coverage.

Otherwise document that primary-key/transaction behavior is covered through deterministic duplicate/conflict/atomicity tests and reserve stress testing for later releases.

Do not add sleeps as race-control mechanisms.

## 29. Test Style

Follow existing Infrastructure.Tests conventions exactly.

Inspect:

- framework;
- assertion style;
- helper patterns;
- fixture/disposal style;
- file naming;
- namespaces;
- temporary-resource handling.

Prefer focused helper methods inside test files or an already-authorized test helper location.

Do not introduce a test framework or mocking package.

## 30. Test Helper Discipline

Shared test helpers are allowed only when they reduce meaningful duplication across multiple WP14 tests.

Do not create a test infrastructure framework.

Helpers must remain test-only.

Potential helpers may provide:

- unique temporary SQLite path;
- disposable database cleanup;
- synthetic `PriceObservation`;
- real store/factory construction;
- schema metadata inspection.

Every helper must be deterministic and leave zero residue.

## 31. Culture / Timezone Determinism

Tests involving decimal/time must be independent of host culture and timezone.

Requirements:

- fixed `DateTimeOffset` values;
- invariant expected values;
- no machine-local timezone dependence;
- no `DateTime.Now`;
- no `DateTimeOffset.Now`;
- no current-date dependence.

If changing current culture in a test, restore it in `finally`.

Avoid global parallel-test interference.

## 32. File-System Determinism

Temporary database paths must:

- be unique;
- be writable in normal test environments;
- not depend on repository location;
- not use a user-specific path;
- be cleaned after test;
- be compatible with Windows and repository-supported environments.

Do not assert path formatting beyond accepted semantics.

## 33. No Production Redesign

Expected WP14 production delta:

`0`

If a permanent test reveals a true production defect:

1. determine which earlier WP owns the behavior;
2. do not silently redesign production under WP14;
3. if the correction is trivially local, semantics-preserving, and explicitly allowed by the Release 1.1 execution plan/file manifest, document it before mutation;
4. otherwise stop and report a blocker/unblock need.

WP14's purpose is to make accepted behavior executable, not change accepted behavior.

## 34. Authorized Mutation Surface

Normal WP14 mutations are limited to:

`tests/AIQuantTradingResearch.Infrastructure.Tests/**`

and only manifest-authorized supporting test project files when necessary.

Do not modify:

- Domain production/tests;
- Application production/tests;
- Worker production;
- Architecture.Tests;
- production Infrastructure under normal execution;
- docs;
- package files;
- project references.

Any required exception must be explicitly justified and authorized by the manifest.

## 35. Package / Reference Protection

Expected delta:

- packages: `0`;
- project references: `0`;
- SQLite version: unchanged.

Use the current test project dependencies.

If a concrete DI package already exists, reuse it.

Do not add packages for convenience.

## 36. Security / Offline Requirement

All WP14 tests must be offline.

Required:

- provider/network calls: `0`;
- real credentials: `0`;
- committed secrets: `0`;
- machine-specific production database paths: `0`.

Use placeholder provider keys only where DI construction requires a non-empty value.

Do not log secrets.

## 37. Whitespace Handling

This authority handles zero or more ordinary whitespace findings in WP14-authorized files.

Run:

- `git diff --check`;
- `git diff --cached --check`.

Correct only Git-reported whitespace in WP14-modified test files.

Do not normalize predecessor artifacts or broad line endings.

Whitespace outside the WP14 mutation surface is a blocker unless clearly pre-existing and irrelevant to the current diff.

## 38. Implementation Sequence

Execute in this order:

1. reconcile repository/GitHub starting state;
2. run initial baseline;
3. move issue #116 to In Progress;
4. inventory all existing Infrastructure tests;
5. build required-coverage matrix against WP06–WP12;
6. design minimal permanent test delta;
7. add schema/bootstrap tests;
8. add mapper/fidelity tests;
9. add connection factory tests;
10. add persistence/idempotency/conflict/atomicity tests;
11. add retrieval/empty/order/isolation/fidelity tests;
12. add failure-mapping tests;
13. add concrete Infrastructure DI/configuration tests if owned by WP14;
14. verify cleanup/residue behavior;
15. run targeted Infrastructure.Tests;
16. run all permanent suites;
17. run format/build/canonical validation;
18. run diff checks;
19. inspect mutation accounting;
20. post concise evidence to issue #116;
21. close #116 / reach Done only if every gate passes;
22. verify #117 remains Open/Backlog;
23. stop.

Do not begin WP15.

## 39. Existing Coverage Matrix

Before mutation, the execution report must include a matrix like:

| Infrastructure behavior | Existing coverage | WP14 action |
|---|---|---|
| WP06 schema shape | | |
| Mapper target fidelity | | |
| Mapper timestamp/offset fidelity | | |
| Mapper decimal fidelity | | |
| Bootstrap version 0→1 | | |
| Bootstrap idempotency | | |
| Incompatible schema rejection | | |
| Connection open/disposal | | |
| Path fidelity | | |
| New persistence | | |
| Multi-row persistence | | |
| Idempotent duplicate | | |
| Mixed new+equivalent | | |
| Conflict | | |
| Offset conflict | | |
| Batch rollback | | |
| Immutable history | | |
| Empty retrieval | | |
| Target isolation | | |
| Ascending retrieval | | |
| Multi-batch retrieval | | |
| Retrieval fidelity | | |
| Operational failure → Unavailable | | |
| Malformed row → InvalidData | | |
| Concrete Infrastructure DI | | |
| Resolution-time DB mutation | | |
| Cleanup/residue | | |

Only add tests for actual gaps.

## 40. Targeted Validation

Before full validation, run the complete Infrastructure test project.

Record exact:

- total;
- passed;
- failed;
- skipped;
- duration if useful but do not use time as an acceptance criterion.

During development, filters may be used, but final targeted evidence must come from the whole Infrastructure.Tests project.

## 41. Full Validation

After WP14 changes, run at minimum:

```powershell
dotnet restore AIQuantTradingResearch.slnx --nologo
dotnet format AIQuantTradingResearch.slnx --verify-no-changes --no-restore
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
dotnet test tests/AIQuantTradingResearch.Domain.Tests/AIQuantTradingResearch.Domain.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo
.\eng\verify.ps1
git diff --check
git diff --cached --check
```

Required final state:

- restore PASS;
- format PASS;
- build PASS;
- warnings 0;
- errors 0;
- Domain.Tests PASS;
- Application.Tests PASS;
- Infrastructure.Tests PASS;
- Architecture.Tests PASS;
- canonical verification PASS;
- both diff checks PASS.

## 42. Permanent Test Delta Accounting

Report exact before/after counts:

- Domain.Tests;
- Application.Tests;
- Infrastructure.Tests;
- Architecture.Tests;
- total.

Expected normal WP14 behavior:

- Domain delta: `0`;
- Application delta: `0`;
- Infrastructure delta: positive;
- Architecture delta: `0`.

Do not invent a target number of WP14 tests. Coverage quality and authority matter more than arbitrary count.

## 43. Database Cleanup Gate

After all Infrastructure tests finish:

- no test database file remains;
- no SQLite journal remains;
- no WAL file remains;
- no SHM file remains;
- no temporary test directory remains if test-owned;
- no repository path contains generated SQLite artifacts.

Report cleanup evidence.

A residue failure blocks WP14 completion.

## 44. Architecture Protection

Production graph remains:

```text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

Architecture.Tests must remain green.

WP14 test references must not create a production dependency edge.

No SQLite type may leak into Domain/Application production contracts.

## 45. Git / GitHub Protection

Repository Git operations are prohibited:

- no branch;
- no staging;
- no commit;
- no push;
- no PR;
- no merge;
- no tag;
- no GitHub Release;
- no reset/rebase/history rewrite.

GitHub mutation is limited to issue #116 lifecycle/evidence after gates pass.

Do not modify issue #117 except read-only verification.

Do not close milestone #52.

Do not activate Release 1.2 planning.

## 46. WP15 Protection

WP15 is:

**Architecture & Documentation Alignment — issue #117**

WP14 must not begin WP15.

Do not:

- modify Architecture.Tests to add new rules unless the manifest explicitly assigns that to WP14;
- update architecture docs;
- update README/docs;
- perform final architecture reconciliation;
- perform Release acceptance.

At final acceptance verify:

```text
Issue #117: OPEN / Backlog
WP15 implementation started: NO
```

## 47. WP15 Handoff

The final WP14 report must give WP15 a precise evidence handoff.

At minimum summarize:

- final permanent test totals;
- exact Infrastructure test delta;
- SQLite schema behavior now permanently proved;
- connection/bootstrap behavior permanently proved;
- persistence/idempotency/conflict/rollback behavior permanently proved;
- retrieval/order/fidelity/empty behavior permanently proved;
- failure mapping permanently proved;
- Infrastructure DI/configuration behavior permanently proved;
- provider/network calls = 0;
- database residue = 0;
- package/reference delta;
- production code delta;
- any architecture/documentation observations WP15 should reconcile.

Do not perform the alignment yourself.

## 48. Acceptance Matrix

The final report must include at least:

| Requirement | Required Result |
|---|---|
| WP13 predecessor | PASS |
| Issue #116 lifecycle | Closed / Done |
| Issue #117 | Open / Backlog |
| Existing 65 Infrastructure tests preserved | PASS |
| WP06 schema tests | PASS |
| WP07 bootstrap tests | PASS |
| WP07 connection lifecycle tests | PASS |
| Mapper target fidelity | PASS |
| Mapper timestamp/offset fidelity | PASS |
| Mapper decimal fidelity | PASS |
| New persistence | PASS |
| Multi-row persistence | PASS |
| Equivalent duplicate idempotency | PASS |
| Mixed new+equivalent behavior | PASS |
| Conflict behavior | PASS |
| Offset conflict behavior | PASS |
| Conflict non-destructive | PASS |
| Batch atomic rollback | PASS |
| Immutable history | PASS |
| Empty retrieval | PASS |
| Exact target isolation | PASS |
| Ascending retrieval | PASS |
| Multi-batch retrieval | PASS |
| Retrieval fidelity | PASS |
| Operational failure → Unavailable | PASS |
| Malformed row → InvalidData | PASS |
| Concrete Infrastructure DI | PASS if owned by manifest |
| Resolution-time DB creation | NO |
| Hidden in-memory fallback | NO |
| Provider/network calls | 0 |
| Temporary SQLite residue | 0 |
| Production code delta | 0 unless authorized defect correction |
| New packages | 0 |
| New project references | 0 |
| Domain/Application test delta | 0 |
| Architecture test delta | 0 |
| Build warnings/errors | 0/0 |
| Canonical verification | PASS |
| Both diff checks | PASS |
| WP15 started | NO |

If a row is not applicable due to manifest ownership, explain the ownership decision.

## 49. Blocker Policy

Stop and emit `RELEASE 1.1 WP14 BLOCKED` if:

- starting-state gate fails;
- issue #116 planning/dependency state is incorrect;
- active Release 1.2 planning is nonzero;
- baseline is red for unrelated reasons;
- a required permanent test needs a new package/reference not authorized;
- accepted WP06–WP12 behavior is internally contradictory;
- a discovered production defect requires redesign outside WP14 authority;
- reliable test isolation/cleanup cannot be achieved;
- SQLite failures cannot be induced deterministically without unstable hacks;
- unexpected repository drift makes scope ambiguous;
- WP15 work becomes necessary to continue.

Report the smallest corrective authority required.

Do not begin WP15.

## 50. Required Execution Report

Return a complete **Release 1.1 WP14 Execution Report** with at least:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. Existing Infrastructure Test Inventory
10. Existing Coverage Matrix
11. WP06 Physical-Model Reconciliation
12. WP07 Connection/Bootstrap Reconciliation
13. WP08 Persistence Reconciliation
14. WP09 Retrieval Reconciliation
15. WP10 Failure-Mapping Reconciliation
16. WP11 DI/Configuration Reconciliation
17. WP12 Runtime Boundary Reconciliation
18. Test Isolation / Temporary Database Strategy
19. Schema Test Design
20. Bootstrap Test Design
21. Connection Factory Test Design
22. Mapper Round-Trip Test Design
23. New Persistence Tests
24. Idempotency Tests
25. Conflict Tests
26. Atomicity / Rollback Tests
27. Immutable-History Tests
28. Empty Retrieval Tests
29. Target Isolation Tests
30. Ordering / Multi-Batch Retrieval Tests
31. Retrieval Fidelity Tests
32. Failure-Mapping Tests
33. Concrete DI / Configuration Test Decision
34. Release 1.0 Provider Regression Protection
35. Exact Files Added/Modified
36. Production Code Delta
37. Package / Reference Delta
38. Permanent Test Count Delta
39. Targeted Infrastructure Test Evidence
40. Full Permanent Test Evidence
41. Canonical Verification
42. Architecture Validation
43. Security / Offline Determinism
44. Database Cleanup Evidence
45. Whitespace / Diff Evidence
46. Mutation Accounting
47. Git / GitHub Protection
48. Planning Protection
49. Findings / Blockers
50. Acceptance Matrix
51. Final Repository / GitHub State
52. WP15 Handoff
53. Final Decision
54. Next Authorized Work Package

Use exact paths, test names, counts, SQLite versions, and observed states from repository truth.

## 51. Success Terminal

Only if every mandatory WP14 gate passes, end exactly with:

```text
RELEASE 1.1 WP14 COMPLETE

INFRASTRUCTURE & PERSISTENCE TESTS:
SQLite schema coverage: PASS
Schema bootstrap coverage: PASS
Connection lifecycle coverage: PASS
Mapper fidelity coverage: PASS
Persistence coverage: PASS
Idempotency coverage: PASS
Conflict coverage: PASS
Atomic rollback coverage: PASS
Immutable-history coverage: PASS
Historical retrieval coverage: PASS
Target isolation coverage: PASS
Ordering/fidelity coverage: PASS
Empty retrieval coverage: PASS
Failure mapping coverage: PASS
Infrastructure DI/configuration coverage: PASS
Provider/network calls: 0
Temporary SQLite residue: 0
Production code delta: 0
Package/reference delta: 0/0
WP15 started: NO

NEXT AUTHORIZED WORK PACKAGE:
WP15 — Architecture & Documentation Alignment
GitHub issue #117
```

If any mandatory gate fails, end with:

```text
RELEASE 1.1 WP14 BLOCKED
```

and identify the exact blocker, evidence, repository/GitHub state, and minimum corrective authority.

## 52. Final Instruction

Execute WP14 as the permanent executable proof of the accepted Release 1.1 Infrastructure persistence slice.

Prefer real SQLite and real production Infrastructure boundaries for Infrastructure-owned behavior.

Keep tests isolated, deterministic, offline, and residue-free.

Do not duplicate WP13's pure Domain/Application coverage.

Do not redesign accepted production behavior merely to improve testability.

Do not begin WP15.
