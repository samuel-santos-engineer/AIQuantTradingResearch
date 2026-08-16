# Release 1.1 WP13 — Domain & Application Tests — Codex Execution Authority

## 1. Authority

You are executing **Release 1.1 — WP13: Domain & Application Tests** for the `AIQuantTradingResearch` repository.

This file is the authoritative execution contract for WP13. Execute it literally and conservatively. Do not expand scope by inference.

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
- WP13 — Domain & Application Tests — CURRENT
- WP14+ — NOT AUTHORIZED

GitHub planning identity:

- Release: `1.1`
- Milestone: `#52 — Phase 3 - Release 1.1: Market Data Persistence Foundation`
- WP13 issue: `#115 — Domain & Application Tests`
- Required dependencies: `#105`, `#106`, `#107`
- Immediate lifecycle predecessor: `#114 — Worker Persistent Market-Data Execution` is already Closed/Done
- Next issue: `#116 — Infrastructure & Persistence Tests`

The Release 1.1 execution plan and file manifest remain governing authorities.

## 2. Mission

Build the permanent **Domain and Application test coverage** required to prove the accepted Release 1.1 persistence semantics and Application orchestration contracts.

WP13 owns permanent tests for:

- Domain semantics already established by WP03;
- Application persistence contracts established by WP04;
- Application persistence use-case behavior established by WP05;
- Application registration/composition behavior introduced where Application-owned in WP12, if and only if the Release 1.1 manifest assigns such coverage to WP13;
- regression protection for existing Release 1.0 Domain/Application behavior where the new persistence slice could have affected it.

WP13 does **not** own:

- SQLite schema/bootstrap tests;
- SQLite store behavior tests;
- connection factory tests;
- persistence/retrieval Infrastructure tests;
- SQLite exception mapping tests;
- Worker end-to-end tests;
- architecture-test expansion unless explicitly assigned by the manifest;
- documentation alignment;
- Git integration/acceptance.

Those belong to WP14, WP15, and WP16.

## 3. Mandatory Inputs

Before mutation, read and reconcile completely:

1. `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`
3. WP01–WP12 authoritative prompts and accepted execution evidence available in repository/current execution context.
4. WP03 accepted semantic decisions.
5. WP04 Application persistence contracts.
6. WP05 persistence use-case implementation.
7. WP12 Application DI registration change.
8. Current Domain source and Domain.Tests.
9. Current Application source and Application.Tests.
10. Current Architecture.Tests, only to understand boundaries; do not modify unless the manifest explicitly assigns it.
11. GitHub issue #115, dependencies, milestone #52, and Project state.

Do not infer exact API shape from this prompt when repository truth is available.

## 4. Starting-State Gate

Before mutation, verify and report:

Repository:

- repository identity is `samuel-santos-engineer/AIQuantTradingResearch`;
- current branch is `main`;
- `main` equals `origin/main`;
- ahead/behind is `0/0`;
- staged files are `0`;
- all existing modified/untracked files classify as accepted cumulative Release 1.1 work plus the WP13 prompt pair;
- unexpected paths are `0`.

Planning:

- issues #103–#114 are Closed/Done;
- issue #115 is Open/Backlog;
- issue #116 is Open/Backlog;
- milestone #52 is Open;
- active Release 1.2 planning is `0`;
- WP14 implementation has not begun.

Implementation:

- WP03 Domain semantics remain accepted;
- WP04 Application persistence contracts are present;
- WP05 persistence use case is present;
- WP12 Application registration is present;
- no contradictory Domain/Application implementation drift exists.

If any mandatory state is false, stop before mutation and report the smallest corrective authority required.

Do not automatically reconcile unrelated GitHub governance drift.

## 5. Initial Technical Baseline

Before moving issue #115 to `In Progress`, run:

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

Expected predecessor baseline from WP12:

- Domain.Tests: 11/11
- Application.Tests: 16/16
- Infrastructure.Tests: 65/65
- Architecture.Tests: 13/13
- Total: 105/105
- Build warnings: 0
- Build errors: 0

These are historical expectations only. Report current truth.

Only after the baseline passes may issue #115 move:

`Backlog → In Progress`

## 6. Test Ownership Boundary

WP13 is **not** a general persistence-test package.

The permanent test ownership split is:

### WP13 — Domain & Application Tests

Owns tests of:

- Domain value invariants relevant to persistence semantics;
- Application persistence result/value contracts;
- Application persistence use-case validation and orchestration;
- Application persistence use-case outcome propagation;
- Application failure/result distinction;
- Application registration if the manifest assigns it.

### WP14 — Infrastructure & Persistence Tests

Owns tests of:

- SQLite schema;
- mapper physical round trip;
- connection factory/bootstrap;
- SQLite store persistence;
- duplicate/conflict physical behavior;
- retrieval SQL/order/fidelity;
- SQLite failure mapping;
- database residue/lifecycle;
- concrete Infrastructure DI behavior where Infrastructure-specific.

Do not move WP14 scenarios into WP13 merely because they are easy to test here.

## 7. Domain Test Scope

WP03 accepted **Domain delta = 0**.

Therefore WP13 must not invent new Domain production behavior solely to create tests.

Permanent Domain tests should prove only relevant existing invariants that support persistence semantics and are not already sufficiently covered.

At minimum inspect whether current tests already prove:

- valid positive `PriceObservation` price;
- invalid/non-positive price rejection;
- `DateTimeOffset` preservation/value semantics;
- observation-series invariants;
- ordering or uniqueness semantics already owned by Domain;
- any target-independent behavior used by persistence semantics.

Do not duplicate existing test cases without a clear regression purpose.

If WP03 persistence semantics are primarily Application policy rather than Domain code, state that Domain test delta may legitimately be zero.

A zero Domain test delta is acceptable when evidence proves existing Domain tests already cover all Domain-owned semantics.

## 8. Application Contract Test Scope

WP13 must permanently test the WP04 persistence contract value/result semantics where behavior exists.

Inspect and cover as applicable:

- `ObservationPersistenceOutcome`;
- `PersistenceFailure`;
- `ObservationPersistenceResult`;
- `HistoricalObservationResult`;
- any constructor/factory invariants;
- mutual exclusivity between success/failure representations;
- empty retrieval representation;
- retrieval ordering/uniqueness validation enforced by Application result construction;
- invalid result-shape rejection, if implemented;
- read-only result exposure, if behaviorally significant.

Do not test SQLite or Infrastructure implementation through these tests.

Use pure Application-level construction and fakes/stubs.

## 9. Application Persistence Use-Case Test Scope

WP13 must permanently cover the accepted WP05 `PersistHistoricalObservationsUseCase` behavior.

At minimum inspect and test, where repository behavior supports them:

### Request validation

- null request handling;
- blank target;
- null observations collection;
- empty observations collection;
- null observation element, where representable;
- duplicate semantic instants;
- non-ascending semantic instants.

### Forwarding / fidelity

- exact target forwarded unchanged;
- exact observation instances or values forwarded unchanged;
- no target normalization;
- no timestamp conversion;
- no price conversion;
- no sorting hidden inside the use case.

### Outcome propagation

- `NewlyAccepted`;
- `Idempotent`;
- `Conflict`;
- `PersistenceFailure.Unavailable`;
- `PersistenceFailure.InvalidData`.

The use case must preserve the accepted store result semantics without flattening or reclassifying them incorrectly.

Use an Application-layer fake/double for `IHistoricalObservationStore`.

Do not use SQLite.

## 10. Acquisition/Persistence Separation Tests

WP05 and WP12 preserved acquisition/persistence separation.

WP13 may test Application behavior that proves the persistence use case itself:

- does not depend on `IObservationSource`;
- does not perform market-data acquisition;
- does not reference provider-specific types;
- does not introduce provider semantics.

Prefer compile-time/reference inspection and focused Application tests.

Do not write Worker integration tests here.

## 11. Empty Retrieval Contract Tests

The accepted semantic is:

`valid target + no persisted observations → successful empty result`

WP13 must permanently prove the **Application result contract** can represent this correctly.

Required:

- result is successful;
- observations collection is non-null;
- observations collection may be empty;
- no invalid `ObservationSeries` is fabricated;
- no failure is implied by emptiness.

This must be proven without SQLite.

## 12. Ordering and Uniqueness Contract Tests

WP04 established that successful historical results require unique, strictly ascending semantic instants.

Where Application result construction enforces this, test:

- valid ascending observations accepted;
- duplicate instant rejected;
- descending/out-of-order observations rejected;
- offset differences representing the same semantic instant are treated according to the accepted semantic-instant rule;
- empty collection remains valid if the contract allows it.

Do not reproduce WP09 SQL-ordering tests here.

WP13 tests Application-level ordering guarantees only.

## 13. Timestamp Fidelity Test Boundary

WP13 must prove Application contracts/use cases do not alter `DateTimeOffset`.

Use Domain/Application-level values with:

- non-zero offset;
- at least one DST-independent arbitrary offset allowed by `DateTimeOffset`;
- differing representations where appropriate.

Do not test SQLite `instant_utc_ticks` or `offset_minutes` columns. That belongs to WP14.

The Application test proves that values pass through unchanged.

## 14. Decimal Fidelity Test Boundary

WP13 must prove Application contracts/use cases do not alter `decimal`.

Use high-precision Domain values permitted by `PriceObservation`.

Consider:

- many decimal digits;
- very small positive values;
- `decimal.MaxValue` only if valid under current Domain construction.

Do not test SQLite `price_text` physical encoding. That belongs to WP14.

## 15. Failure Vocabulary Tests

The accepted Application failure vocabulary remains:

- `PersistenceFailure.Unavailable`;
- `PersistenceFailure.InvalidData`.

WP13 should prove that Application-level result types and use-case propagation keep these failures distinct.

Do not test SQLite exception codes or their mapping. That belongs to WP14.

Do not add new failure values.

## 16. Conflict Semantic Tests

`ObservationPersistenceOutcome.Conflict` is not an infrastructure failure.

WP13 must prove at the Application level:

- conflict is a persistence outcome;
- conflict is not `Unavailable`;
- conflict is not `InvalidData`;
- the WP05 use case preserves conflict unchanged from the store abstraction;
- no retry/overwrite behavior exists in the Application use case.

Do not verify SQLite row immutability here; WP14 owns that.

## 17. Idempotency Semantic Tests

WP13 must prove at the Application level:

- `Idempotent` is distinct from `NewlyAccepted`;
- `Idempotent` is not a failure;
- WP05 preserves the idempotent result unchanged.

Do not test actual SQLite duplicate row counts here.

## 18. Application DI Registration Test Boundary

WP12 added production registration:

`IPersistHistoricalObservationsUseCase → PersistHistoricalObservationsUseCase`

Reconcile the Release 1.1 file manifest.

If WP13 owns permanent Application DI tests, prove:

- exactly one registration exists;
- implementation is correct;
- lifetime matches Application conventions;
- no Infrastructure reference is required to resolve the Application use-case registration itself when using an Application-level fake store.

If permanent DI composition testing is assigned to WP14 or already sufficiently covered elsewhere, do not duplicate it. Report the ownership decision.

## 19. Test Style and Conventions

Follow existing Domain.Tests and Application.Tests conventions exactly.

Inspect:

- test framework;
- assertion style;
- naming style;
- file organization;
- namespace style;
- fake/stub conventions;
- deterministic-data conventions.

Do not add a new test framework.

Do not add mocking libraries unless already present and used by the repository.

Prefer small hand-written fakes/stubs if that matches current conventions.

## 20. No Package Expansion

Expected WP13 package delta:

`0`

Expected project-reference delta:

`0`

Do not add:

- Moq;
- NSubstitute;
- FluentAssertions;
- AutoFixture;
- FsCheck;
- SQLite packages to Domain/Application test projects;
- testcontainers;
- snapshot-testing libraries.

If existing project tooling is insufficient for required tests, stop and report the exact blocker rather than expanding dependencies without authority.

## 21. Authorized Mutation Surface

Use `RELEASE_1.1_FILE_MANIFEST.md` as the exact source of truth.

Expected WP13 production delta:

`0`

Expected WP13 test mutations are limited to:

- `tests/AIQuantTradingResearch.Domain.Tests/**`
- `tests/AIQuantTradingResearch.Application.Tests/**`

Only change Domain/Application production code if a test exposes a true defect in already-accepted behavior and the smallest correction is explicitly within WP13 authority according to the execution plan/manifest.

If a defect requires redesign of WP03–WP05 semantics, stop and report it instead.

Do not modify Infrastructure.Tests, Architecture.Tests, Infrastructure production, Worker production, packages, project references, or docs under normal WP13 execution.

## 22. Test Case Inventory Before Mutation

Before writing tests:

1. list every existing relevant Domain test;
2. list every existing relevant Application test;
3. map each required WP13 semantic to existing coverage;
4. identify actual coverage gaps;
5. design the minimum permanent test delta.

The execution report must include a coverage matrix:

| Semantic / behavior | Existing coverage | WP13 action |
|---|---|---|
| Domain price invariant | | |
| Timestamp value preservation | | |
| Empty historical result | | |
| Ordering validation | | |
| Duplicate instant validation | | |
| Use-case invalid target | | |
| Use-case empty observations | | |
| Exact forwarding | | |
| NewlyAccepted propagation | | |
| Idempotent propagation | | |
| Conflict propagation | | |
| Unavailable propagation | | |
| InvalidData propagation | | |
| Acquisition/persistence separation | | |

Do not create tests for already well-covered semantics unless they protect a new Release 1.1 regression surface.

## 23. Test Determinism

All WP13 tests must be:

- offline;
- deterministic;
- independent of system timezone;
- independent of current culture;
- independent of external provider access;
- independent of SQLite/database files;
- independent of test execution order;
- independent of machine-specific paths;
- free of sleeps/timeouts as correctness mechanisms.

Set culture explicitly only where needed to prove invariance; restore it deterministically.

Do not use `DateTimeOffset.Now`, `DateTime.Now`, or current time when fixed values suffice.

## 24. No Infrastructure Test Leakage

WP13 tests must not instantiate:

- `SqliteHistoricalObservationStore`;
- `SqliteConnectionFactory`;
- `SqliteSchemaBootstrapper`;
- `SqliteConnection`;
- SQLite physical records;
- actual database files.

If a test needs persistence behavior, fake `IHistoricalObservationStore`.

Any need for concrete SQLite is a WP14 handoff, not a reason to broaden WP13.

## 25. No Worker Test Leakage

WP13 must not test:

- `PersistentMarketDataExecution`;
- Worker exit codes;
- Worker console output;
- end-to-end acquisition-to-persistence;
- missing Worker configuration;
- Worker DI graph.

Those are not Domain/Application tests and are reserved for other work-package coverage.

## 26. Regression Protection

WP13 must preserve all existing Domain/Application tests.

Do not weaken assertions or delete pre-existing tests to make new behavior pass.

At final validation, compare before/after counts.

Any test-count increase must be explained exactly.

Expected Infrastructure.Tests and Architecture.Tests counts should remain unchanged under normal WP13 execution.

## 27. Whitespace Handling

This authority handles ordinary WP13 whitespace findings without a recursive authority.

Run:

- `git diff --check`
- `git diff --cached --check`

If a finding exists in a WP13-authorized test file, correct only the reported whitespace.

Do not normalize unrelated cumulative files.

If a finding occurs outside the WP13 mutation surface and was not pre-existing/accepted, stop and report it.

## 28. Security

WP13 must not introduce:

- credentials;
- API keys;
- connection strings;
- personal file-system paths;
- real market/provider data requiring secrecy;
- live network calls.

Use synthetic test targets and observations.

Do not log secrets.

## 29. Implementation Sequence

Execute in this order:

1. reconcile repository/GitHub starting state;
2. run initial technical baseline;
3. move issue #115 to In Progress;
4. inspect Domain.Tests and Application.Tests fully;
5. build the existing-coverage matrix;
6. identify minimal gaps;
7. implement Domain tests only where genuinely needed;
8. implement Application contract tests;
9. implement WP05 use-case tests;
10. optionally implement Application registration test only if WP13 owns it;
11. run targeted Domain.Tests;
12. run targeted Application.Tests;
13. run all permanent test suites;
14. run format verification;
15. run canonical verification;
16. run both diff checks;
17. inspect mutation accounting;
18. verify Infrastructure/Worker production untouched;
19. post concise evidence to issue #115;
20. close #115 / reach Done only if every gate passes;
21. verify #116 remains Open/Backlog;
22. stop.

Do not begin WP14.

## 30. Required Targeted Validation

Before the full canonical run, execute the changed suites directly.

Record:

- Domain.Tests total/passed/failed/skipped;
- Application.Tests total/passed/failed/skipped.

If test filtering is used during development, final evidence must come from complete project execution, not only filtered tests.

## 31. Full Validation

After WP13 changes run at minimum:

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

Use repository-equivalent commands if paths differ.

Required final state:

- restore PASS;
- format PASS;
- build PASS;
- warnings 0;
- errors 0;
- every permanent test PASS;
- Architecture.Tests PASS;
- canonical verification PASS;
- both diff checks PASS.

## 32. Architecture Protection

WP13 must not change the production dependency graph:

```text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

Architecture.Tests must remain green.

No new project reference may be introduced.

No Infrastructure dependency may be added to Application.Tests solely to reach concrete SQLite behavior.

## 33. Git / GitHub Protection

Repository Git operations are prohibited:

- no branch;
- no staging;
- no commit;
- no push;
- no PR;
- no merge;
- no tag;
- no GitHub Release;
- no history rewrite.

GitHub mutation is limited to issue #115 lifecycle/evidence after gates pass.

Do not modify issue #116 except read-only verification.

Do not close milestone #52.

Do not activate Release 1.2 planning.

## 34. WP14 Handoff

The final WP13 report must give WP14 a precise handoff of what remains Infrastructure-owned and therefore intentionally untested by WP13.

At minimum list:

- SQLite physical schema;
- mapper physical round trips;
- connection factory;
- schema bootstrap;
- store persistence;
- new/idempotent/conflict physical behavior;
- atomic batch rollback;
- retrieval SQL/order;
- timestamp/offset physical round trip;
- decimal physical round trip;
- successful empty retrieval through SQLite;
- operational failure → `Unavailable`;
- malformed row → `InvalidData`;
- DI/configuration integration where Infrastructure-specific;
- temporary database cleanup.

WP14 must not need to infer which scenarios WP13 deliberately excluded.

## 35. Acceptance Matrix

The final report must include a matrix with at least:

| Requirement | Required Result |
|---|---|
| WP12 predecessor | PASS |
| Issue #115 lifecycle | Closed / Done |
| Issue #116 | Open / Backlog |
| Existing Domain tests preserved | PASS |
| Existing Application tests preserved | PASS |
| Domain production delta | 0 unless accepted defect |
| Application production delta | 0 unless accepted defect |
| Persistence contract tests | PASS |
| Empty retrieval contract test | PASS |
| Ordering/duplicate validation tests | PASS where contract owns them |
| Exact target forwarding | PASS |
| Timestamp fidelity forwarding | PASS |
| Decimal fidelity forwarding | PASS |
| NewlyAccepted propagation | PASS |
| Idempotent propagation | PASS |
| Conflict propagation | PASS |
| Unavailable propagation | PASS |
| InvalidData propagation | PASS |
| Acquisition/provider calls in WP13 tests | 0 |
| SQLite/database use in WP13 tests | 0 |
| New packages | 0 |
| New project references | 0 |
| Infrastructure.Tests changed | NO |
| Worker production changed | NO |
| Architecture tests | PASS |
| Canonical verification | PASS |
| Both diff checks | PASS |
| WP14 started | NO |

If a requirement is genuinely not applicable because repository truth places it elsewhere, explain why rather than fabricating a test.

## 36. Blocker Policy

Stop and report `RELEASE 1.1 WP13 BLOCKED` if:

- starting-state gate fails;
- issue #115 planning state/dependencies are incorrect;
- active Release 1.2 planning is nonzero;
- baseline tests/build are red for unrelated reasons;
- a required Application behavior cannot be tested without concrete Infrastructure and the behavior is actually Infrastructure-owned;
- the accepted contracts lack deterministic semantics needed for a test;
- a new package/reference is required;
- production redesign is required;
- fixing a discovered defect exceeds WP13 authority;
- unexpected repository drift makes scope ambiguous.

Report the smallest corrective authority required.

Do not begin WP14.

## 37. Required Execution Report

Return a complete **Release 1.1 WP13 Execution Report** with at least:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. Domain Test Inventory
10. Application Test Inventory
11. Existing Coverage Matrix
12. WP03 Semantic Reconciliation
13. WP04 Contract Reconciliation
14. WP05 Use-Case Reconciliation
15. WP12 Application Registration Reconciliation
16. Domain Test Design
17. Application Contract Test Design
18. Persistence Use-Case Test Design
19. Empty Retrieval Tests
20. Ordering / Duplicate Validation Tests
21. Target/Fidelity Forwarding Tests
22. Persistence Outcome Propagation Tests
23. Failure Propagation Tests
24. Acquisition/Persistence Separation Tests
25. Application Registration Test Decision
26. Exact Files Added/Modified
27. Domain Production Delta
28. Application Production Delta
29. Package/Reference Delta
30. Permanent Test Count Delta
31. Targeted Domain Test Evidence
32. Targeted Application Test Evidence
33. Full Permanent Test Evidence
34. Canonical Verification
35. Architecture Validation
36. Security / Offline Determinism
37. Whitespace / Diff Evidence
38. Mutation Accounting
39. Git/GitHub Protection
40. Planning Protection
41. Findings / Blockers
42. Acceptance Matrix
43. Final Repository / GitHub State
44. WP14 Handoff
45. Final Decision
46. Next Authorized Work Package

Use exact paths, names, and test counts from repository truth.

## 38. Success Terminal

Only if every mandatory WP13 gate passes, end exactly with:

```text
RELEASE 1.1 WP13 COMPLETE

DOMAIN & APPLICATION TESTS:
Domain persistence-semantics coverage: PASS
Application persistence-contract coverage: PASS
Persistence use-case coverage: PASS
Empty retrieval contract: PASS
Ordering/duplicate validation: PASS
Exact target/timestamp/decimal forwarding: PASS
NewlyAccepted propagation: PASS
Idempotent propagation: PASS
Conflict propagation: PASS
Unavailable propagation: PASS
InvalidData propagation: PASS
SQLite/database use in WP13 tests: 0
Provider/network calls in WP13 tests: 0
Package/reference delta: 0/0
WP14 started: NO

NEXT AUTHORIZED WORK PACKAGE:
WP14 — Infrastructure & Persistence Tests
GitHub issue #116
```

If any mandatory gate fails, end with:

```text
RELEASE 1.1 WP13 BLOCKED
```

and identify the exact blocker, evidence, repository/GitHub state, and minimum corrective authority.

## 39. Final Instruction

Execute WP13 as the permanent **Domain and Application regression suite** for the Release 1.1 persistence semantics already accepted.

Do not retest Infrastructure through SQLite.

Do not invent new semantics.

Do not weaken existing tests.

Prefer the smallest permanent test delta that closes real coverage gaps.

Do not begin WP14.
