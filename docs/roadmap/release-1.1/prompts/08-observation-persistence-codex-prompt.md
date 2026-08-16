# Release 1.1 WP08 — Observation Persistence — Codex Execution Authority

## 1. Authority

You are executing **Release 1.1 — WP08: Observation Persistence** for the `AIQuantTradingResearch` repository.

This file is the authoritative execution contract for WP08. Execute it literally and conservatively. Do not expand scope by inference.

Accepted predecessor chain:

- WP01 — Release & Repository Preflight — COMPLETE
- WP02 — Persistence Technology Discovery — COMPLETE
- WP03 — Historical Observation Persistence Semantics — COMPLETE
- WP04 — Application Persistence Contracts — COMPLETE
- WP05 — Persistence Use-Case Integration — COMPLETE
- WP06 — Storage Physical Model — COMPLETE
- WP07 — Storage Engine & Connection Boundary — COMPLETE
- WP08 — Observation Persistence — CURRENT
- WP09+ — NOT AUTHORIZED

GitHub planning identity:

- Release: `1.1`
- Milestone: `#52 — Phase 3 - Release 1.1: Market Data Persistence Foundation`
- WP08 issue: `#110 — Observation Persistence`
- Required predecessor: `#109 — Storage Engine & Connection Boundary`

The Release 1.1 execution plan and file manifest remain governing authorities.

## 2. Mission

Implement the minimum Infrastructure-owned SQLite observation-write behavior required to satisfy the accepted Application persistence contract and WP03 persistence semantics.

WP08 owns:

- the Infrastructure implementation of the observation persistence/write capability;
- exact use of the accepted WP06 physical model;
- use of the WP07 connection boundary;
- atomic persistence of an authorized observation batch;
- deterministic classification of newly accepted observations;
- idempotent handling of equivalent duplicates;
- deterministic detection of conflicting duplicates;
- immutable-history enforcement;
- exact target, timestamp, offset, and decimal fidelity through the accepted mapper;
- write-side transaction behavior necessary to prevent partial semantic acceptance.

WP08 does **not** own:

- historical retrieval implementation;
- final storage validation/failure mapping assigned to WP10;
- DI/composition registration assigned to WP11;
- Worker persistence orchestration assigned to WP12;
- comprehensive Application tests assigned to WP13;
- comprehensive Infrastructure/persistence tests assigned to WP14;
- architecture/documentation alignment assigned to WP15;
- Release integration or acceptance assigned to WP16.

## 3. Mandatory Inputs

Before mutation, read and reconcile at minimum:

1. `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`
3. WP01–WP07 authoritative prompts and accepted results available in repository/current execution context.
4. WP02 persistence technology assessment/decision artifacts.
5. WP03 historical-observation persistence semantics.
6. WP04 Application persistence contracts.
7. WP05 persistence use-case implementation.
8. WP06 SQLite physical-model implementation.
9. WP07 SQLite connection/configuration/bootstrap implementation.
10. Current Domain `PriceObservation` semantics.
11. Existing Infrastructure conventions and tests.
12. Current architecture/dependency documentation.
13. GitHub issue #110, milestone #52, and Project #2 state.

Accepted predecessor decisions are authoritative. Do not redesign them because another SQLite write strategy is possible.

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

- issues #103–#109 are Closed/Done;
- issue #110 is Open/Backlog;
- issues #111–#118 remain Open/Backlog;
- milestone #52 remains Open;
- active Release 1.2 planning remains zero.

The repository may contain cumulative authorized Release 1.1 artifacts from prior WPs. Preserve and classify them. Do not delete, stage, commit, or rewrite predecessor work merely to obtain a clean tree.

Stop on unexplained drift that makes WP08 ownership ambiguous.

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

Do not repair unrelated baseline failures under WP08 authority.

## 6. Issue Lifecycle

After all starting gates pass and immediately before substantive WP08 mutation, move issue #110 / its Project item:

`Backlog` → `In Progress`

No other issue may progress.

Close #110 and verify/allow `Done` only after every WP08 acceptance gate passes.

## 7. Accepted Application Contract

Use the actual WP04 source as repository truth.

The accepted capability is:

`IHistoricalObservationStore`

with persistence conceptually equivalent to:

`Persist(string target, IReadOnlyList<PriceObservation> observations)`

Accepted result vocabulary includes:

- `ObservationPersistenceOutcome.NewlyAccepted`
- `ObservationPersistenceOutcome.Idempotent`
- `ObservationPersistenceOutcome.Conflict`

Accepted persistence failures include:

- `PersistenceFailure.Unavailable`
- `PersistenceFailure.InvalidData`

Do not change the public Application contract merely to simplify SQLite implementation.

Do not introduce SQLite/provider types into Application.

## 8. Accepted WP03 Persistence Semantics

Preserve the accepted semantics:

- target identity is exact and opaque;
- observation identity is `(target, semantic instant)`;
- timestamp fidelity is exact;
- decimal fidelity is exact;
- history is immutable;
- equivalent duplicate persistence is idempotent;
- a duplicate at the same identity with different substantive observation data is a deterministic conflict;
- conflicts must not overwrite prior accepted history;
- successful historical retrieval is ascending and may be empty, but retrieval implementation belongs to WP09.

WP08 must implement the write-side semantics without weakening them.

## 9. Accepted WP05 Use-Case Boundary

WP05 established a dedicated Application persistence use case receiving already normalized observations.

WP08 must not:

- acquire market data;
- normalize provider transport;
- combine acquisition and persistence orchestration;
- modify Worker behavior.

The store implementation receives already valid Application/Domain inputs subject to the existing contract.

Do not duplicate WP05 request validation as a new Application policy inside Infrastructure.

Infrastructure may still defensively reject impossible/corrupt data where required for storage integrity.

## 10. Accepted WP06 Physical Model

Treat the actual WP06 source as authoritative.

Expected accepted table:

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

Accepted mapping:

- target preserved exactly;
- absolute instant represented by UTC ticks;
- original `DateTimeOffset` offset represented by offset minutes;
- decimal represented losslessly using invariant `G29` text;
- schema version `1`;
- no update/delete/upsert semantic policy.

Reuse the WP06 mapper/schema artifacts. Do not create a second competing physical representation.

## 11. Accepted WP07 Connection Boundary

Use the actual WP07 implementation as repository truth.

Accepted handoff includes:

- `Microsoft.Data.Sqlite` `10.0.11`, centrally managed and Infrastructure-owned;
- `ISqliteConnectionFactory`;
- `SqliteConnectionFactory`;
- `SqliteSchemaBootstrapper`;
- `SqliteStorageConfiguration`;
- a fresh connection per factory call;
- returned connection already open;
- schema bootstrap completed before the connection is returned;
- caller owns disposal;
- schema version represented through `PRAGMA user_version`;
- accepted schema version `1`;
- bootstrap is idempotent and rejects incompatible schema;
- no final Application failure mapping was implemented.

WP08 must consume this boundary rather than opening/configuring/bootstraping SQLite independently.

Do not redesign connection ownership.

## 12. Implementation Shape

Implement the smallest focused Infrastructure component that satisfies the accepted `IHistoricalObservationStore` write contract.

Prefer a capability-specific type such as an Infrastructure historical-observation store over:

- generic repositories;
- generic CRUD abstractions;
- unit-of-work frameworks;
- ORM abstractions;
- speculative persistence frameworks.

If `IHistoricalObservationStore` also contains retrieval because of WP04 contract shape, WP08 must not implement WP09 behavior prematurely.

If the language/compiler requires all interface members to exist, use only the narrowest manifest/architecture-compatible strategy. Do not fabricate real retrieval behavior. If a correct implementation cannot compile without crossing WP09 scope, stop and report the exact contract conflict rather than silently implementing WP09.

## 13. Batch Semantics

Treat one `Persist` invocation as one semantic batch.

The implementation must not report a successful batch if only part of the requested batch was durably accepted.

For a batch containing only new observations:

- persist all observations atomically;
- return the accepted `NewlyAccepted` outcome according to the exact WP04 result contract.

For a batch whose observations are all already stored equivalently:

- perform no semantic mutation;
- return `Idempotent`.

For a batch containing a mixture of new observations and equivalent duplicates, reconcile the exact WP03/WP04 accepted semantics and implement the deterministic result required by those authorities.

Do not invent a new partial-count/result vocabulary unless it already exists in accepted contracts.

If predecessor authority does not determine the externally visible outcome for a mixed new/idempotent batch and the existing result type cannot express it unambiguously, stop and report the ambiguity rather than guessing.

## 14. Atomicity

All write-side decisions for one persistence batch must be transactionally safe.

Use one SQLite transaction when needed so that:

- no partial batch is accepted if a conflict invalidates the batch under accepted semantics;
- no partial batch remains after a write failure;
- duplicate classification and insertion are protected against ordinary race windows as far as SQLite transaction semantics allow;
- existing history is never rewritten to force success.

Do not use WP07 bootstrap transactions as a substitute for WP08 write transactions.

Keep transaction scope limited to the persistence operation.

## 15. Exact Identity

For every observation, identity is exactly:

`(target, semantic instant)`

Use the WP06 mapping for semantic instant identity.

Requirements:

- do not trim target;
- do not case-fold target;
- do not parse target into provider/symbol components;
- do not normalize target whitespace;
- do not use the original offset as part of identity;
- do not use price as part of identity;
- do not invent a surrogate observation ID.

The SQLite primary key is the physical enforcement mechanism for this identity.

## 16. Equivalent Duplicate

A persistence request is idempotent for an already-existing identity only when the stored substantive observation is equivalent under the accepted WP03 semantics.

At minimum, reconcile:

- exact target identity;
- same semantic instant;
- timestamp representation/fidelity requirements;
- exact decimal value.

Use the accepted physical mapper to compare representations where appropriate.

Do not treat every primary-key collision as automatically idempotent.

Do not rewrite the stored row to make it equivalent.

## 17. Offset Fidelity and Duplicate Equivalence

WP06 stores both UTC ticks and original offset minutes.

The semantic instant determines identity, but the accepted predecessor semantics also require timestamp fidelity.

Therefore, explicitly reconcile whether an incoming observation at the same semantic instant but with a different original offset representation is:

- equivalent under WP03 semantics; or
- conflicting because persistence must preserve the originally accepted representation exactly.

Follow WP03/WP06 authority, not intuition.

Report the resolved rule and evidence.

If the predecessor authorities are genuinely contradictory or insufficient on this point, stop rather than silently choosing a policy.

## 18. Decimal Fidelity

Never persist prices using SQLite `REAL`.

Use the accepted WP06 mapping.

Requirements:

- exact `decimal` numeric value must round-trip;
- invariant representation must be preserved;
- duplicate comparison must not use floating-point conversion;
- no rounding or scale truncation;
- no locale-sensitive parsing/formatting.

Do not alter the Domain price value.

## 19. Conflicting Duplicate

For an existing `(target, semantic instant)` whose stored substantive value is not equivalent to the incoming observation:

- return the exact accepted `Conflict` outcome;
- preserve the existing row unchanged;
- do not update;
- do not replace;
- do not delete/reinsert;
- do not use `INSERT OR REPLACE`;
- do not convert the conflict into `Idempotent`;
- do not retry until it appears successful.

If a batch contains a conflict, preserve the accepted batch atomicity semantics and ensure no unauthorized partial acceptance remains.

## 20. Immutable History

The WP08 implementation must have no normal code path that mutates or removes an already accepted historical observation.

Prohibited:

- `UPDATE` of accepted observation values;
- `DELETE` as conflict handling;
- `REPLACE`;
- destructive upsert;
- overwrite-on-duplicate;
- last-write-wins behavior.

An equivalent duplicate is a no-op.

A conflicting duplicate is a deterministic conflict.

## 21. SQL Strategy

Use parameterized SQLite commands.

Do not concatenate target, timestamp, offset, or price values into SQL text.

Keep SQL focused on the accepted table and WP08 behavior.

Acceptable strategies may include:

- deterministic existence/read-for-comparison plus insert;
- insert with conflict-aware follow-up;
- another minimal transactionally correct SQLite strategy.

Whichever strategy is chosen must prove the accepted idempotency/conflict/atomicity semantics.

Do not introduce stored procedures, triggers, ORM mappings, or a generic SQL layer.

## 22. Concurrency and Race Handling

Do not claim stronger concurrency guarantees than SQLite and the implemented transaction actually provide.

The implementation must handle a uniqueness race deterministically enough that a concurrent equivalent insert does not become a false semantic conflict merely because the row appeared after the initial check.

Likewise, a concurrent conflicting row must not be overwritten.

Use SQLite transaction/constraint behavior deliberately.

Do not introduce distributed locks, process-global locks, or speculative retry infrastructure.

If a bounded SQLite-specific retry is truly necessary for correctness, it must not preempt WP10 failure policy and must be explicitly justified. Prefer avoiding new retry policy in WP08.

## 23. Input Ordering

WP05 validates that its persistence request contains unique, strictly increasing semantic instants.

WP08 must preserve incoming observation identity and values.

Do not sort/reorder merely to hide invalid input unless predecessor authority explicitly allows it.

If direct calls to the store can bypass WP05, implement only the minimum defensive behavior required by the accepted WP04 contract and storage integrity.

Do not create a second competing validation policy.

## 24. Empty Input

WP05 defines empty persistence input as an invalid Application request.

WP08 should not invent an alternate business meaning such as "empty batch = idempotent success."

Follow the actual WP04/WP05 contract.

If the Infrastructure store can be called directly with an empty collection and the contract requires a deterministic response, use the already accepted failure/result vocabulary only.

Do not expand the public contract.

## 25. Storage Failure Boundary

WP10 — Storage Validation & Failure Mapping — owns final storage validation and failure mapping.

WP08 must not redesign the failure taxonomy.

Where the existing WP04 contract requires WP08 to return an `ObservationPersistenceResult`, use only already-defined result/failure values and only where predecessor authority makes the mapping unambiguous.

Do not create a broad SQLite-exception classifier.

Do not add retryability, error codes, diagnostics categories, or provider-specific failure types to Application.

If an SQLite failure cannot yet be truthfully mapped under accepted authority, preserve it for WP10 according to the existing architecture rather than inventing policy.

Report intentionally unmapped failure behavior.

## 26. Constraint and Corruption Handling

A primary-key collision is not by itself an infrastructure outage.

Distinguish semantic duplicate reconciliation from unrelated SQLite failures.

Do not treat malformed/incompatible stored rows as equivalent duplicates.

If an existing row cannot be losslessly interpreted using the accepted WP06 mapping, do not overwrite it.

Final classification of corrupt/invalid stored data belongs to WP10 unless already fixed by predecessor contracts.

## 27. Connection Use

For each persistence operation:

- obtain the connection through `ISqliteConnectionFactory`;
- rely on WP07's guarantee that it is open and schema-validated;
- dispose it according to WP07 ownership rules;
- do not bootstrap the schema independently;
- do not construct a second connection string;
- do not hard-code a database path.

Do not keep a live connection in a static/global field.

## 28. Retrieval Protection

WP09 — Historical Observation Retrieval — is not authorized.

WP08 may read an existing row only as narrowly required to classify a duplicate during persistence.

That narrow duplicate-comparison read does **not** authorize:

- target history queries;
- public retrieval implementation;
- historical result construction;
- ascending-history materialization;
- empty retrieval behavior;
- query filters/ranges;
- pagination.

Do not implement `Retrieve` behavior assigned to WP09.

## 29. WP10 Protection

Do not implement WP10's full storage validation/failure mapping.

Specifically do not introduce:

- comprehensive SQLite exception → `PersistenceFailure` mapping;
- corruption taxonomy;
- unavailable/retry policy;
- validation framework for all persistence/retrieval operations;
- final diagnostics/logging policy.

Only behavior strictly necessary for WP08's accepted write outcomes is authorized.

## 30. WP11 Protection

WP11 — Dependency Registration & Configuration — is not authorized.

Do not:

- register the store in DI;
- modify Worker service registration;
- bind `Persistence:DatabasePath` in the composition root;
- change host startup.

Infrastructure constructors may receive the WP07 connection abstraction needed for later registration.

## 31. WP12 Protection

WP12 — Worker Persistent Market-Data Execution — is not authorized.

Do not:

- modify Worker execution;
- call the persistence use case from Worker;
- combine market-data acquisition with persistence;
- add runtime end-to-end persistence orchestration.

## 32. WP13/WP14 Test Protection

Follow `RELEASE_1.1_FILE_MANIFEST.md` exactly.

WP13 owns Domain/Application persistence tests.

WP14 owns comprehensive Infrastructure/persistence tests.

If the WP08 manifest assigns focused permanent tests to WP08, create exactly those tests.

Otherwise, temporary focused probes are allowed to prove the implementation provided they:

- are not staged/committed;
- are removed before completion;
- leave no SQLite residue;
- do not alter permanent test counts.

Useful WP08 validation scenarios include:

- one new observation;
- multiple new observations in one batch;
- equivalent duplicate;
- repeated equivalent batch;
- conflicting duplicate;
- conflict leaves existing row unchanged;
- conflict does not partially persist earlier new rows from the same batch when atomic semantics require rollback;
- mixed new/equivalent batch according to accepted semantics;
- exact target case/whitespace preservation;
- same semantic instant duplicate handling;
- differing offsets according to the accepted equivalence rule;
- high-precision decimal persistence;
- `decimal.MaxValue` if compatible with accepted Domain semantics;
- no generated database residue after validation.

Do not preempt WP14's comprehensive suite.

## 33. Expected File Scope

`RELEASE_1.1_FILE_MANIFEST.md` is the source of truth for WP08 file ownership.

Use the exact authorized file set where specified.

Do not invent helper files merely for stylistic decomposition.

Any deviation must be minimal, unavoidable, explicitly reported, and impossible to defer to WP09+.

If manifest authority and repository truth conflict, stop.

## 34. Package / Project Reference Accounting

WP08 should normally require:

- package delta `0`;
- project-reference delta `0`.

`Microsoft.Data.Sqlite` is already owned by Infrastructure through WP07.

Do not add:

- EF Core;
- Dapper;
- another SQLite provider;
- resilience packages;
- generic repository packages.

Report all package/reference deltas exactly.

## 35. Architecture

Mandatory dependency graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

WP08 must preserve this graph.

SQLite-specific implementation remains in Infrastructure.

No SQLite/provider type may leak into Domain or Application public contracts.

No new dependency cycle is allowed.

## 36. Security

Do not introduce or expose:

- credentials;
- passwords;
- API keys;
- secret-bearing connection strings;
- machine-specific personal paths;
- sensitive database content in logs.

No real external provider credential is required for WP08.

Temporary SQLite databases must contain only synthetic validation data and must be removed.

## 37. Logging and Diagnostics

Do not introduce a new logging framework or persistence logging policy.

If existing Infrastructure conventions require logging, do not log:

- secrets;
- full connection strings;
- unnecessary persisted values.

WP10/WP15 may own broader failure/observability alignment.

Keep WP08 focused on write semantics.

## 38. Whitespace Handling

Before acceptance run:

- format verification;
- `git diff --check`;
- `git diff --cached --check`.

If either diff check reports whitespace in a file authorized for WP08 modification, you may remove only the reported whitespace from that authorized file, preserving semantics.

This authority intentionally handles zero or more WP08 whitespace findings without requiring a recursive unblock authority.

Do not normalize unrelated predecessor files.

## 39. Mutation Accounting

At completion report exactly:

- files added;
- files modified;
- files deleted;
- packages changed;
- project references changed;
- permanent tests changed;
- temporary probes created/removed;
- generated SQLite artifacts created/removed;
- unexpected paths.

Do not stage or commit WP08 work.

## 40. Final Validation

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

## 41. Observation-Persistence Validation Matrix

Report PASS/FAIL for:

| Requirement | Required |
|---|---|
| `IHistoricalObservationStore` write capability implemented in Infrastructure | PASS |
| WP07 connection factory reused | PASS |
| WP06 mapper/schema reused | PASS |
| Exact target preserved | PASS |
| Semantic-instant identity preserved | PASS |
| Timestamp fidelity preserved | PASS |
| Decimal fidelity preserved | PASS |
| New observation persistence | PASS |
| Multi-observation batch persistence | PASS |
| Equivalent duplicate idempotent | PASS |
| Repeated equivalent persistence does not add rows | PASS |
| Conflicting duplicate deterministic | PASS |
| Conflicting duplicate does not overwrite | PASS |
| Batch write atomicity | PASS |
| Conflict rollback/partial-write protection | PASS |
| Immutable history | PASS |
| Parameterized SQL | PASS |
| No `INSERT OR REPLACE` / destructive upsert | PASS |
| Duplicate race handled without overwrite | PASS |
| Connection disposal follows WP07 | PASS |
| Domain SQLite leakage | 0 |
| Application SQLite leakage | 0 |
| WP09 public retrieval implemented | NO |
| WP10 comprehensive failure mapping implemented | NO |
| WP11 DI registration implemented | NO |
| WP12 Worker orchestration implemented | NO |

Also report the accepted result for:

- mixed new + equivalent duplicate batch;
- same semantic instant with differing original offset representation.

These two outcomes must be derived from predecessor authority, not invented.

## 42. Git / GitHub Protection

WP08 must not:

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

The only GitHub lifecycle mutation authorized is issue #110 / its existing Project item.

## 43. Planning Protection

Do not:

- modify WP01–WP07 completed planning;
- progress WP09–WP16;
- create WP17+;
- create lifecycle-gate issues;
- change labels;
- change Project schema/options;
- activate Release 1.2;
- close milestone #52.

Normal milestone issue counts and normal Project automation caused by closing #110 are allowed.

## 44. WP09 Handoff

The final report must give WP09 an implementation-ready handoff containing:

- concrete Infrastructure store type(s);
- exact connection boundary used;
- exact table/schema/mapping reused;
- whether any duplicate-comparison SQL is write-private and must not become the retrieval design accidentally;
- transaction behavior;
- target and semantic-instant identity;
- timestamp/offset fidelity rule;
- decimal mapping;
- immutable-history guarantees;
- what WP08 deliberately did **not** implement for retrieval;
- provider/storage failures intentionally left for WP10.

WP09 must be free to implement historical retrieval without redesigning write semantics.

## 45. Stop Conditions

Stop without claiming completion if:

- WP07 is not accepted;
- issue #110 dependency/lifecycle is incorrect;
- manifest does not authorize the required implementation files;
- the WP04 interface cannot be implemented without prematurely implementing WP09 and no manifest-compatible narrow solution exists;
- predecessor authority does not determine mixed-batch outcome and the public result contract cannot express it unambiguously;
- predecessor authority does not determine whether differing offset representations at the same semantic instant are equivalent or conflicting;
- accepted WP06 mapping cannot preserve required fidelity;
- correct atomic persistence requires changing WP03/WP04 semantics;
- implementation requires WP09+ behavior;
- baseline/final validation fails and repair is outside WP08;
- unexpected repository drift makes scope ambiguous;
- architecture boundaries would be violated.

Report the smallest corrective authority required.

## 46. Acceptance Criteria

WP08 is complete only if:

- starting-state/lifecycle gates pass;
- #110 is the only progressed issue;
- accepted WP03 semantics remain unchanged;
- accepted WP04 contracts remain unchanged unless the manifest explicitly assigns a required WP08 change;
- accepted WP05 use-case boundary remains unchanged;
- WP06 physical model and mapper are reused;
- WP07 connection/bootstrap boundary is reused;
- observation writes are Infrastructure-owned;
- new observations persist correctly;
- equivalent duplicates are idempotent;
- conflicting duplicates are deterministic and non-destructive;
- batch semantics are explicit and conform to predecessor authority;
- write operations are atomic as required;
- history remains immutable;
- exact target identity is preserved;
- timestamp/offset fidelity is preserved;
- decimal fidelity is preserved without SQLite `REAL`;
- SQL is parameterized;
- no destructive upsert/replace behavior exists;
- no WP09 public retrieval implementation exists;
- no WP10 comprehensive failure mapping exists;
- no WP11 DI registration exists;
- no WP12 Worker orchestration exists;
- Domain/Application contain no SQLite leakage;
- package/reference accounting is exact;
- security gates pass;
- all technical validation passes;
- both diff checks pass;
- no generated SQLite residue remains;
- no Git integration action occurs;
- issue #111 remains Open/Backlog until WP08 completion.

## 47. Successful Completion Actions

Only after all acceptance criteria pass:

1. add concise completion evidence to issue #110 if repository convention requires it;
2. close issue #110;
3. verify/allow its Project item to become `Done`;
4. verify issue #111 remains Open/Backlog;
5. verify milestone #52 remains Open;
6. verify Release 1.2 remains inactive.

Do not begin WP09.

## 48. Required Execution Report

Return a detailed **Release 1.1 WP08 Execution Report** covering at minimum:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. WP03 Semantic Reconciliation
10. WP04 Contract Reconciliation
11. WP05 Use-Case Reconciliation
12. WP06 Physical-Model Reconciliation
13. WP07 Connection-Boundary Reconciliation
14. Persistence Implementation Design
15. Batch Semantics
16. Transaction / Atomicity Design
17. Identity Handling
18. Equivalent-Duplicate Handling
19. Offset-Equivalence Decision
20. Decimal Fidelity
21. Conflicting-Duplicate Handling
22. Immutable-History Enforcement
23. SQL / Parameterization Strategy
24. Concurrency / Uniqueness-Race Handling
25. Input / Empty-Batch Handling
26. Storage Failure Boundary
27. Connection Ownership / Disposal
28. Exact Files Added/Modified
29. Package/Reference Delta
30. Test/Probe Delta
31. WP09 Protection
32. WP10/WP11/WP12 Protection
33. Security
34. Whitespace/Diff Evidence
35. Restore/Build Evidence
36. Permanent Test Evidence
37. Canonical Verification
38. Architecture Validation
39. Observation-Persistence Validation Matrix
40. Mutation Accounting
41. Git/GitHub Protection
42. Planning Protection
43. Findings/Blockers
44. Final Repository/GitHub State
45. WP09 Handoff
46. Final Decision
47. Next Authorized Work Package

## 49. Success Terminal

On complete success, end with:

`RELEASE 1.1 WP08 COMPLETE`

and identify:

`NEXT AUTHORIZED WORK PACKAGE: WP09 — Historical Observation Retrieval — GitHub issue #111`

Do not start WP09.
