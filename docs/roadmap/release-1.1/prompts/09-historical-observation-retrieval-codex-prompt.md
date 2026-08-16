# Release 1.1 WP09 --- Historical Observation Retrieval --- Codex Execution Authority

## 1. Authority

You are executing **Release 1.1 --- WP09: Historical Observation
Retrieval** for the `AIQuantTradingResearch` repository.

This file is the authoritative execution contract for WP09. Execute it
literally and conservatively. Do not expand scope by inference.

Accepted predecessor chain:

-   WP01 --- Release & Repository Preflight --- COMPLETE
-   WP02 --- Persistence Technology Discovery --- COMPLETE
-   WP03 --- Historical Observation Persistence Semantics --- COMPLETE
-   WP04 --- Application Persistence Contracts --- COMPLETE
-   WP05 --- Persistence Use-Case Integration --- COMPLETE
-   WP06 --- Storage Physical Model --- COMPLETE
-   WP07 --- Storage Engine & Connection Boundary --- COMPLETE
-   WP08 --- Observation Persistence --- COMPLETE
-   WP09 --- Historical Observation Retrieval --- CURRENT
-   WP10+ --- NOT AUTHORIZED

GitHub planning identity:

-   Release: `1.1`
-   Milestone:
    `#52 — Phase 3 - Release 1.1: Market Data Persistence Foundation`
-   WP09 issue: `#111 — Historical Observation Retrieval`
-   Required predecessor: `#110 — Observation Persistence`

The Release 1.1 execution plan and file manifest remain governing
authorities.

## 2. Mission

Implement the minimum Infrastructure-owned SQLite historical-observation
retrieval behavior required by the accepted Application persistence
contract and WP03 semantics.

WP09 owns:

-   the real retrieval implementation for the existing
    `IHistoricalObservationStore`;
-   replacement of WP08's deliberate retrieval `NotSupportedException`;
-   exact target-scoped historical lookup;
-   deterministic ascending ordering by semantic instant;
-   lossless reconstruction of persisted `PriceObservation` values;
-   successful empty retrieval;
-   use of the WP06 physical model/mapper;
-   use of the WP07 connection boundary;
-   preservation of WP08 write semantics unchanged.

WP09 does **not** own:

-   redesign of persistence semantics;
-   write behavior changes except a strictly necessary compatibility
    correction;
-   comprehensive storage validation/failure mapping assigned to WP10;
-   DI/configuration registration assigned to WP11;
-   Worker orchestration assigned to WP12;
-   comprehensive Domain/Application tests assigned to WP13;
-   comprehensive Infrastructure/persistence tests assigned to WP14;
-   architecture/documentation alignment assigned to WP15;
-   Release integration/acceptance assigned to WP16.

## 3. Mandatory Inputs

Before mutation, read and reconcile at minimum:

1.  `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
2.  `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`
3.  WP01--WP08 authoritative prompts and accepted results available in
    repository/current execution context.
4.  WP02 persistence technology decision artifacts.
5.  WP03 historical-observation persistence semantics.
6.  WP04 `IHistoricalObservationStore`, `HistoricalObservationResult`,
    `PersistenceFailure`, and related contracts.
7.  WP05 Application persistence use case.
8.  WP06 SQLite schema, record, and mapper.
9.  WP07 connection/configuration/bootstrap boundary.
10. WP08 `SqliteHistoricalObservationStore` implementation and accepted
    write semantics.
11. Current Domain `PriceObservation` / observation-series invariants.
12. Existing source/tests/architecture documentation.
13. GitHub issue #111, milestone #52, and Project #2 state.

Accepted predecessor decisions are authoritative. Do not redesign them
because another query/API shape is possible.

## 4. Starting-State Gate

Before mutation, verify and report:

-   repository identity;
-   authenticated GitHub identity without exposing credentials;
-   current branch;
-   `HEAD`;
-   `origin/main`;
-   ahead/behind;
-   staged files;
-   tracked modifications;
-   untracked files;
-   unexpected files.

Expected lifecycle state:

-   issues #103--#110 are Closed/Done;
-   issue #111 is Open/Backlog;
-   issues #112--#118 remain Open/Backlog;
-   milestone #52 remains Open;
-   active Release 1.2 planning remains zero.

The repository may contain cumulative authorized Release 1.1 artifacts
from prior WPs. Preserve and classify them. Do not delete, stage,
commit, or rewrite predecessor work merely to obtain a clean tree.

Stop on unexplained drift that makes WP09 ownership ambiguous.

## 5. Baseline Validation

Before implementation execute:

-   restore;
-   format verification;
-   build;
-   all permanent tests;
-   Architecture.Tests;
-   `eng/verify.ps1`;
-   `git diff --check`;
-   `git diff --cached --check`.

Record exact results.

Do not repair unrelated baseline failures under WP09 authority.

## 6. Issue Lifecycle

After all starting gates pass and immediately before substantive WP09
mutation, move issue #111 / its Project item:

`Backlog` → `In Progress`

No other issue may progress.

Close #111 and verify/allow `Done` only after every WP09 acceptance gate
passes.

## 7. Accepted Retrieval Contract

Use the actual WP04 source as repository truth.

The existing `IHistoricalObservationStore` retrieval member and
`HistoricalObservationResult` are authoritative.

Do not:

-   change the public retrieval signature merely to simplify SQLite;
-   add SQLite types to Application;
-   introduce a second retrieval interface;
-   introduce pagination/range/filter contracts not already authorized;
-   return `null` for a successful empty result.

If the exact current contract differs from assumptions in this prompt,
follow repository authority and report the reconciliation.

## 8. Accepted Retrieval Semantics

Preserve WP03/WP04 semantics:

-   target identity is exact and opaque;
-   retrieval is scoped to exactly one target;
-   historical observations are returned in semantic-instant ascending
    order;
-   timestamp fidelity is exact;
-   decimal fidelity is exact;
-   persisted history is immutable;
-   no observations is a **successful empty retrieval**, not a failure;
-   successful empty retrieval must not fabricate an invalid Domain
    aggregate;
-   storage/provider mechanics remain outside Domain/Application.

Do not reinterpret successful emptiness as `NotFound`, `Unavailable`,
`InvalidData`, or `null`.

## 9. Accepted WP06 Physical Model

Reuse the actual WP06 source.

Expected table:

`historical_observations`

Expected columns:

-   `target`
-   `instant_utc_ticks`
-   `offset_minutes`
-   `price_text`

Expected identity:

`PRIMARY KEY (target, instant_utc_ticks)`

Expected mapping:

-   target preserved exactly;
-   UTC ticks represent semantic instant;
-   offset minutes reconstruct original `DateTimeOffset`;
-   price text reconstructs exact `decimal`;
-   binary target collation;
-   schema version 1.

Do not create a second row model or alternate timestamp/price encoding
without explicit predecessor authority.

## 10. Accepted WP07 Connection Boundary

Use `ISqliteConnectionFactory` and the actual WP07 implementation.

For retrieval:

-   obtain a fresh already-open, schema-validated connection from the
    factory;
-   caller owns disposal;
-   dispose deterministically after the operation;
-   do not independently bootstrap;
-   do not construct a new connection string;
-   do not choose a database path;
-   do not retain a global/shared connection.

WP09 must not redesign WP07 lifecycle semantics.

## 11. Accepted WP08 Store and Write Semantics

Extend the existing `SqliteHistoricalObservationStore`; do not replace
it with a competing store.

WP08 established:

-   Infrastructure ownership;
-   parameterized SQL;
-   transactionally atomic writes;
-   exact `(target, instant)` identity;
-   equivalent duplicate idempotency;
-   mixed new + equivalent batch → `NewlyAccepted`;
-   all-equivalent batch → `Idempotent`;
-   any conflict → `Conflict` with rollback;
-   different original offset at the same semantic instant → conflict;
-   immutable history;
-   no update/delete/replace/destructive upsert;
-   no comprehensive WP10 failure mapping.

WP09 must preserve these write semantics exactly.

The WP08 private duplicate-classification lookup is write-specific and
must not silently become the public historical retrieval design.

## 12. Implementation Shape

Implement the smallest real retrieval behavior in
`SqliteHistoricalObservationStore` consistent with the file manifest.

The existing WP08 `NotSupportedException` for retrieval must be
removed/replaced by the real WP09 implementation.

Prefer a direct, focused SQLite query over:

-   generic repositories;
-   query frameworks;
-   ORM abstractions;
-   speculative read models;
-   caching layers;
-   pagination infrastructure.

Do not add a second store implementation.

## 13. Exact Target Scoping

The retrieval query must match the target exactly.

Requirements:

-   no trimming;
-   no case folding;
-   no symbol/provider parsing;
-   no wildcard matching;
-   no `LIKE`;
-   no prefix matching;
-   no culture-sensitive comparison;
-   no normalization of embedded/leading/trailing whitespace.

Use parameterized equality against the accepted binary-collated target
column.

A query for one target must never return observations belonging to
another target.

## 14. Deterministic Ordering

The result must be explicitly ordered by semantic instant ascending.

Use an explicit SQL ordering equivalent to:

`ORDER BY instant_utc_ticks ASC`

Do not rely solely on:

-   physical row layout;
-   primary-key implementation details;
-   insertion order;
-   SQLite's incidental output order.

Ordering must remain deterministic even if observations were persisted
in different batches.

## 15. Row Reconstruction

Use the accepted WP06 mapper wherever it already owns row-to-Domain
reconstruction.

Do not duplicate conversion logic unless the existing mapper lacks a
necessary direction and the manifest authorizes the minimal extension.

For every row preserve:

-   exact target;
-   exact semantic instant;
-   original offset representation;
-   exact decimal value.

Do not convert price through `double`, `float`, or SQLite `REAL`.

Do not convert timestamp through locale-formatted text.

## 16. Timestamp / Offset Fidelity

Reconstruct the original accepted `DateTimeOffset` representation using:

-   stored UTC ticks;
-   stored offset minutes;
-   the accepted WP06 mapping/invariants.

Two rows cannot share one `(target, semantic instant)` identity because
of the primary key.

Do not normalize all returned timestamps to UTC if predecessor semantics
require original offset reconstruction.

Report proof that an observation persisted with a non-zero offset is
retrieved with the same offset representation.

## 17. Decimal Fidelity

Reconstruct `decimal` from the accepted invariant text representation.

Requirements:

-   no floating-point intermediate;
-   no rounding;
-   no scale truncation that changes the numeric value;
-   no current-culture dependency.

Validate at least one high-precision value and, if compatible with
Domain semantics, `decimal.MaxValue`.

## 18. Successful Empty Retrieval

For a valid target with no rows:

-   return the exact WP04 successful result shape;
-   observations collection must be empty;
-   do not return `null`;
-   do not fabricate a placeholder observation;
-   do not construct a Domain aggregate whose invariants prohibit
    emptiness;
-   do not classify the result as a storage failure.

Use the current `HistoricalObservationResult` contract exactly.

## 19. Result Collection Semantics

The returned collection must:

-   contain exactly the rows for the requested target;
-   be ordered ascending by semantic instant;
-   preserve Domain values;
-   satisfy the read-only/immutable exposure expected by the Application
    contract.

Do not expose mutable Infrastructure records or SQLite reader objects.

Do not leak connection lifetime beyond the method return.

## 20. Retrieval Transaction Policy

Do not introduce a write transaction merely because WP08 writes use one.

For a single deterministic target-scoped SELECT, use the minimum
transaction behavior justified by repository requirements.

If a read transaction is unnecessary for one statement, do not add one
speculatively.

If the implementation does use a transaction, justify why and ensure it
does not change WP07/WP08 behavior.

Do not introduce snapshot/versioning policy beyond Release 1.1
authority.

## 21. SQL Strategy

Use parameterized SQL.

The public retrieval query should select only fields required to
reconstruct the accepted Domain observation.

Do not use `SELECT *` if repository conventions favor explicit columns.

The query must be target-scoped and explicitly ordered.

Do not introduce:

-   joins;
-   aggregation;
-   window functions;
-   dynamic SQL;
-   provider-specific query abstractions;
-   range/pagination logic not authorized by WP04.

## 22. Failure Boundary

WP10 --- Storage Validation & Failure Mapping --- owns comprehensive
storage validation/failure mapping.

WP09 must not create a broad SQLite exception taxonomy.

Where the existing retrieval result requires a failure value, use only
already-defined vocabulary and only where predecessor authority makes
mapping unambiguous.

Do not add:

-   SQLite error-code enums to Application;
-   retryability categories;
-   transient/permanent classifiers;
-   broad exception translation;
-   new logging policy.

If an engine/corruption failure cannot yet be truthfully mapped,
preserve/report it for WP10 rather than inventing policy.

## 23. Corrupt Stored Data

Do not silently repair, overwrite, skip, or normalize corrupt persisted
rows.

If a stored row cannot be reconstructed losslessly under WP06
invariants:

-   do not mutate history;
-   do not fabricate a valid observation;
-   do not silently omit the row.

Use only existing contract behavior if already unambiguous; otherwise
leave final classification to WP10 and report the boundary.

WP09 is retrieval implementation, not corruption-repair authority.

## 24. Input Validation

Follow the actual WP04 contract and existing Application conventions.

Do not create a competing validation policy.

If blank target handling is already determined by predecessor authority,
preserve it.

If direct store invocation receives an invalid target and the existing
contract has an unambiguous failure result, use it.

Do not normalize invalid input into a different valid target.

## 25. WP08 Write Protection

Do not change WP08 behavior unless a compile/runtime defect directly
prevents WP09 and the correction is:

-   minimal;
-   semantics-preserving;
-   manifest-compatible;
-   explicitly reported.

Specifically preserve:

-   transaction/atomicity;
-   duplicate classification;
-   conflict behavior;
-   offset conflict rule;
-   immutable history;
-   parameterized writes;
-   connection ownership.

Run focused regression validation for WP08 after adding retrieval.

## 26. WP10 Protection

Do not implement WP10's comprehensive validation/failure mapping.

No new:

-   SQLite exception classifier;
-   corruption taxonomy;
-   retry policy;
-   storage availability policy;
-   generalized validation layer.

Only retrieval behavior necessary for WP09 is authorized.

## 27. WP11 Protection

Do not:

-   register the store in DI;
-   modify Worker composition;
-   bind persistence configuration in the host;
-   modify startup.

WP11 owns Dependency Registration & Configuration.

## 28. WP12 Protection

Do not:

-   invoke retrieval from Worker;
-   combine acquisition, persistence, and retrieval;
-   create runtime research workflows using stored history;
-   modify hosted execution.

WP12 owns Worker Persistent Market-Data Execution.

## 29. WP13/WP14 Test Protection

Follow `RELEASE_1.1_FILE_MANIFEST.md` exactly.

WP13 owns comprehensive Domain/Application tests.

WP14 owns comprehensive Infrastructure/persistence tests.

If WP09 has no permanent test ownership, use temporary focused probes
only, then remove them completely.

Useful focused scenarios:

1.  no rows → successful empty retrieval;
2.  one observation;
3.  multiple observations;
4.  observations persisted across multiple calls;
5.  explicit ascending order;
6.  exact target case sensitivity;
7.  exact target whitespace preservation;
8.  target isolation;
9.  non-zero offset round trip;
10. high-precision decimal round trip;
11. `decimal.MaxValue` if valid;
12. WP08 equivalent duplicate remains idempotent;
13. WP08 conflicting duplicate remains non-destructive;
14. retrieval after conflict returns the originally accepted row;
15. no temporary database residue.

Do not preempt WP14's comprehensive suite.

## 30. Expected File Scope

`RELEASE_1.1_FILE_MANIFEST.md` is the source of truth.

Expected primary WP09 mutation is the existing:

`src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationStore.cs`

If the accepted WP06 mapper requires a minimal row-to-Domain extension
and the manifest authorizes it, make only that required change.

Do not invent new production files merely for stylistic decomposition.

If manifest authority and repository truth conflict, stop.

## 31. Package / Reference Accounting

Expected WP09 delta:

-   packages: `0`;
-   project references: `0`.

Do not add:

-   EF Core;
-   Dapper;
-   another SQLite package;
-   query/caching packages;
-   serialization packages.

Report exact deltas.

## 32. Architecture

Preserve:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

SQLite remains Infrastructure-only.

No SQLite type may appear in Domain/Application public contracts.

No dependency cycle is allowed.

## 33. Security

Do not expose:

-   credentials;
-   API keys;
-   secret-bearing connection strings;
-   personal machine paths;
-   sensitive database contents in logs.

Temporary SQLite data must be synthetic and removed.

No external provider access is needed for WP09.

## 34. Logging / Observability

Do not introduce a new logging framework or query logging policy.

Do not log full persisted histories or connection strings.

Broader persistence observability/failure alignment is outside WP09
unless explicitly present in predecessor authority.

## 35. Whitespace Handling

Before acceptance run:

-   format verification;
-   `git diff --check`;
-   `git diff --cached --check`.

If whitespace findings occur in files authorized for WP09 modification,
remove only those reported whitespace defects while preserving
semantics.

This authority handles zero or more whitespace findings in the
authorized WP09 file set without another recursive authority.

Do not normalize unrelated predecessor files.

## 36. Mutation Accounting

Report exactly:

-   files added;
-   files modified;
-   files deleted;
-   packages changed;
-   project references changed;
-   permanent tests changed;
-   temporary probes created/removed;
-   temporary databases created/removed;
-   unexpected paths.

Do not stage or commit WP09 work.

## 37. Final Validation

After implementation execute:

1.  `dotnet restore AIQuantTradingResearch.slnx --nologo`
2.  repository format verification
3.  solution build
4.  Domain.Tests
5.  Application.Tests
6.  Infrastructure.Tests
7.  Architecture.Tests
8.  `eng/verify.ps1`
9.  `git diff --check`
10. `git diff --cached --check`

Report exact warnings, errors, totals, failures, and skipped tests.

No red gate may be waived.

## 38. Retrieval Validation Matrix

Report PASS/FAIL for:

  Requirement                                      Required
  ------------------------------------------------ ----------
  Existing Infrastructure store extended           PASS
  WP08 retrieval `NotSupportedException` removed   PASS
  WP07 connection factory reused                   PASS
  WP06 schema/mapper reused                        PASS
  Exact target equality                            PASS
  Target case preserved                            PASS
  Target whitespace preserved                      PASS
  Cross-target leakage                             0
  Explicit semantic-instant ascending order        PASS
  Timestamp instant fidelity                       PASS
  Original offset fidelity                         PASS
  Decimal fidelity                                 PASS
  Successful empty retrieval                       PASS
  Empty result is not failure                      PASS
  Empty result is not null                         PASS
  One-row retrieval                                PASS
  Multi-row retrieval                              PASS
  Multi-batch historical retrieval                 PASS
  Parameterized SQL                                PASS
  Infrastructure records leaked                    0
  SQLite types leaked to Domain/Application        0
  WP08 write semantics preserved                   PASS
  Equivalent duplicate regression                  PASS
  Conflict/immutability regression                 PASS
  WP10 comprehensive mapping implemented           NO
  WP11 DI registration implemented                 NO
  WP12 Worker orchestration implemented            NO
  Temporary SQLite residue                         0

## 39. Git / GitHub Protection

WP09 must not:

-   create a branch;
-   stage;
-   commit;
-   amend;
-   reset;
-   rebase;
-   push;
-   create a PR;
-   merge;
-   tag;
-   create a GitHub Release;
-   rewrite history.

The only GitHub lifecycle mutation authorized is issue #111 / its
existing Project item.

## 40. Planning Protection

Do not:

-   modify WP01--WP08 completed planning;
-   progress WP10--WP16;
-   create WP17+;
-   create lifecycle-gate issues;
-   modify labels;
-   modify Project schema/options;
-   activate Release 1.2;
-   close milestone #52.

Normal milestone counts and normal Project automation caused by closing
#111 are allowed.

## 41. WP10 Handoff

The final report must provide an implementation-ready WP10 handoff
covering:

-   concrete retrieval method now implemented;
-   exact SQL/query shape;
-   target equality semantics;
-   ordering semantics;
-   row reconstruction path;
-   successful empty result behavior;
-   timestamp/offset fidelity;
-   decimal fidelity;
-   connection ownership;
-   WP08 write semantics preserved;
-   current behavior of engine exceptions;
-   current behavior of malformed/corrupt stored rows;
-   any intentionally unmapped failures requiring WP10 classification;
-   any validation behavior already fixed by predecessor contracts.

WP10 must be able to add final validation/failure mapping without
redesigning persistence or retrieval.

## 42. Stop Conditions

Stop without claiming completion if:

-   WP08 is not accepted;
-   issue #111 dependency/lifecycle is incorrect;
-   manifest does not authorize the required mutation;
-   the current retrieval contract contradicts WP03 semantics;
-   successful empty retrieval cannot be represented by the existing
    contract;
-   WP06 mapping cannot reconstruct values losslessly;
-   implementing retrieval requires changing WP08 write semantics;
-   implementing retrieval requires WP10+ scope;
-   baseline/final validation fails and repair is outside WP09;
-   unexpected repository drift makes ownership ambiguous;
-   architecture boundaries would be violated.

Report the smallest corrective authority required.

## 43. Acceptance Criteria

WP09 is complete only if:

-   starting/lifecycle gates pass;
-   #111 is the only progressed issue;
-   accepted WP03 semantics remain unchanged;
-   WP04 retrieval contract remains unchanged unless manifest explicitly
    requires otherwise;
-   WP05 use-case remains unchanged;
-   WP06 schema/mapping is reused;
-   WP07 connection boundary is reused;
-   WP08 write semantics are preserved;
-   `SqliteHistoricalObservationStore` has real retrieval behavior;
-   deliberate WP08 `NotSupportedException` is gone;
-   retrieval is exact-target scoped;
-   SQL is parameterized;
-   ordering is explicitly semantic-instant ascending;
-   timestamp/offset fidelity is preserved;
-   decimal fidelity is preserved;
-   empty history is successful and empty;
-   no cross-target leakage occurs;
-   no Infrastructure/SQLite type leaks to Application/Domain;
-   no comprehensive WP10 mapping is introduced;
-   no WP11 DI is introduced;
-   no WP12 Worker orchestration is introduced;
-   package/reference accounting is exact;
-   security gates pass;
-   all validation passes;
-   both diff checks pass;
-   no temporary database/probe residue remains;
-   no Git integration action occurs;
-   issue #112 remains Open/Backlog until WP09 completion.

## 44. Successful Completion Actions

Only after every acceptance criterion passes:

1.  add concise completion evidence to issue #111 if repository
    convention requires it;
2.  close issue #111;
3.  verify/allow its Project item to become `Done`;
4.  verify issue #112 remains Open/Backlog;
5.  verify milestone #52 remains Open;
6.  verify Release 1.2 remains inactive.

Do not begin WP10.

## 45. Required Execution Report

Return a detailed **Release 1.1 WP09 Execution Report** covering at
minimum:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Predecessor/Lifecycle Gates
7.  Issue Lifecycle
8.  Initial Baseline
9.  WP03 Retrieval-Semantic Reconciliation
10. WP04 Contract Reconciliation
11. WP06 Physical-Model Reconciliation
12. WP07 Connection-Boundary Reconciliation
13. WP08 Store/Write Reconciliation
14. Retrieval Implementation Design
15. Exact Target Scoping
16. Ordering Strategy
17. Row Reconstruction
18. Timestamp / Offset Fidelity
19. Decimal Fidelity
20. Successful Empty Retrieval
21. Result Collection Semantics
22. Retrieval Transaction Policy
23. SQL / Parameterization Strategy
24. Input Validation
25. Storage Failure Boundary
26. Corrupt-Row Boundary
27. Connection Ownership / Disposal
28. WP08 Regression Protection
29. Exact Files Added/Modified
30. Package/Reference Delta
31. Test/Probe Delta
32. WP10/WP11/WP12 Protection
33. Security
34. Whitespace/Diff Evidence
35. Restore/Build Evidence
36. Permanent Test Evidence
37. Canonical Verification
38. Architecture Validation
39. Retrieval Validation Matrix
40. Mutation Accounting
41. Git/GitHub Protection
42. Planning Protection
43. Findings/Blockers
44. Final Repository/GitHub State
45. WP10 Handoff
46. Final Decision
47. Next Authorized Work Package

## 46. Success Terminal

On complete success, end with:

`RELEASE 1.1 WP09 COMPLETE`

and identify:

`NEXT AUTHORIZED WORK PACKAGE: WP10 — Storage Validation & Failure Mapping — GitHub issue #112`

Do not start WP10.
