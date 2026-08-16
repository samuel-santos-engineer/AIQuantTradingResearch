# Release 1.1 WP06 — Storage Physical Model — Codex Execution Authority

## 1. Authority

You are executing **Release 1.1 — WP06: Storage Physical Model** for the `AIQuantTradingResearch` repository.

This file is the authoritative execution contract for WP06. Execute it literally and conservatively. Do not expand scope by inference.

The accepted predecessor chain is:

- WP01 — Release & Repository Preflight — COMPLETE
- WP02 — Persistence Technology Discovery — COMPLETE
- WP03 — Historical Observation Persistence Semantics — COMPLETE
- WP04 — Application Persistence Contracts — COMPLETE
- WP05 — Persistence Use-Case Integration — COMPLETE
- WP06 — Storage Physical Model — CURRENT
- WP07+ — NOT AUTHORIZED

GitHub planning identity:

- Release: `1.1`
- Milestone: `#52 — Phase 3 - Release 1.1: Market Data Persistence Foundation`
- WP06 issue: `#108 — Storage Physical Model`
- Required predecessor: `#107 — Persistence Use-Case Integration`

The Release 1.1 execution plan and file manifest remain governing authorities.

## 2. Mission

Design and implement the minimum physical storage model required to persist historical market-data observations while preserving the already accepted Release 1.1 semantics.

WP06 owns the **physical representation and schema contract**.

WP06 does **not** own:

- database connection lifecycle;
- storage-engine connection abstractions;
- runtime database initialization orchestration;
- repository/store implementation;
- SQL command execution for persistence or retrieval;
- Worker integration;
- dependency injection registration;
- comprehensive persistence tests;
- architecture/documentation alignment;
- Release integration.

Those belong to later work packages.

## 3. Mandatory Inputs

Before mutation, read and reconcile at minimum:

1. `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`
3. WP01–WP05 authoritative prompts and accepted execution results available in the repository/current execution context.
4. WP02 persistence-technology decision/assessment artifacts.
5. WP03 persistence semantics.
6. WP04 Application persistence contracts.
7. WP05 persistence use-case implementation.
8. Current solution/project structure.
9. Current package-management files.
10. Existing architecture and persistence-related documentation.
11. GitHub issue #108 and its exact dependencies.
12. Current milestone and Project state.

Do not replace accepted predecessor decisions merely because another design is possible.

## 4. Starting-State Gate

Before changing files, verify and report:

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

- WP01–WP05 issues are Closed/Done;
- WP06 issue #108 is Open/Backlog before execution;
- WP07–WP16 remain Open/Backlog;
- milestone #52 remains Open;
- active Release 1.2 planning remains zero.

The repository may contain cumulative, authorized, uncommitted Release 1.1 artifacts from prior WPs. Classify them; do not delete, overwrite, stage, commit, or otherwise disturb them merely to obtain a clean working tree.

Stop on unexplained repository drift that makes WP06 scope ambiguous.

## 5. Baseline Validation

Before implementation, run the repository's canonical baseline validation, including:

- restore;
- format verification;
- build;
- permanent tests;
- architecture tests;
- canonical `eng/verify.ps1`;
- `git diff --check`;
- `git diff --cached --check`.

Record exact results.

Do not repair unrelated baseline failures under WP06 authority.

## 6. Issue Lifecycle

After all starting gates pass and immediately before substantive WP06 implementation, move issue #108 / its Project item from:

`Backlog` → `In Progress`

Do not progress any other Release 1.1 issue.

Close #108 and allow/ensure its Project state becomes `Done` only after every WP06 acceptance gate passes.

## 7. Accepted Semantic Baseline

WP06 must preserve the accepted semantics from WP03–WP05.

At minimum:

- persistence is scoped by the exact opaque target;
- observation identity is the target plus the Domain observation's semantic instant;
- timestamp fidelity must preserve the represented instant exactly;
- price fidelity must preserve the Domain `decimal` value exactly;
- historical observations are immutable after acceptance;
- equivalent duplicates are idempotent;
- conflicting duplicates are deterministic conflicts;
- retrieval ordering is ascending by semantic instant;
- successful empty retrieval remains distinguishable from failure;
- storage/provider failures remain distinct from semantic conflicts;
- Application and Domain remain storage-engine independent.

Do not weaken these semantics to fit a convenient schema.

## 8. Technology Constraint

Use the persistence technology accepted by WP02.

If WP02 selected SQLite, WP06 may introduce only the minimum SQLite-specific package/configuration dependency necessary to express or validate the physical schema **if and only if the Release 1.1 manifest assigns that dependency to WP06**.

Before adding any package:

1. prove it is required now;
2. prove the manifest permits it now;
3. use central package management if the repository already does so;
4. avoid packages belonging to WP07+ implementation concerns.

Do not substitute another database technology.

## 9. Physical Model Ownership

WP06 must establish an explicit physical model for historical observations.

The model must define, at minimum:

- storage object/table identity;
- column identities;
- physical data types;
- nullability;
- primary key and/or unique constraint strategy;
- target representation;
- semantic-instant representation;
- price representation;
- deterministic uniqueness needed for duplicate/conflict handling;
- ordering/index support needed for historical retrieval;
- schema/version ownership if required by the accepted design;
- creation/DDL representation appropriate to the selected technology.

Prefer the smallest schema that satisfies accepted Release 1.1 requirements.

Do not design speculative analytics, aggregation, portfolio, trading, ML, or multi-provider storage.

## 10. Target Representation

The target is an opaque Application value.

The physical model must:

- preserve the target exactly;
- avoid case folding unless already explicitly required;
- avoid trimming or normalization that changes identity;
- avoid provider-specific parsing;
- avoid encoding business meaning into storage columns.

If the selected storage engine has collation behavior that could alter exact target identity, explicitly choose or document a representation/constraint that preserves the accepted identity semantics.

## 11. Semantic Instant Representation

The physical representation must preserve the exact semantic instant represented by `DateTimeOffset`.

Do not silently lose:

- UTC instant;
- offset information if retaining it is required to reconstruct the accepted Domain value exactly;
- sub-second precision supported/required by the accepted model.

Select a deterministic representation that:

1. preserves identity;
2. supports uniqueness;
3. supports chronological ordering;
4. can reconstruct the accepted Domain value without semantic loss.

If multiple physical columns are required, keep them minimal and explain why.

Do not rely on locale-dependent date/time text.

## 12. Decimal Price Representation

SQLite does not provide a native arbitrary-precision decimal storage class equivalent to .NET `decimal`.

Therefore, if SQLite is the accepted engine, do **not** casually map Domain prices to binary floating point.

Choose and document a deterministic lossless representation for all accepted Domain `decimal` values.

The representation must:

- round-trip exactly;
- not use `REAL` if that can lose decimal fidelity;
- be culture invariant;
- be deterministic;
- remain compatible with later persistence/retrieval implementation.

Do not introduce an arbitrary fixed scale unless the accepted Domain semantics guarantee that scale.

## 13. Identity and Duplicate Enforcement

The physical model must support the accepted identity:

`exact target + semantic instant`

The database must be capable of enforcing uniqueness for that identity.

This uniqueness is necessary but does not by itself implement WP08 conflict/idempotency behavior.

WP06 defines the constraint; WP08 will interpret an existing row as equivalent or conflicting according to the accepted persistence semantics.

Do not implement overwrite/upsert behavior in WP06.

## 14. Immutability

The physical design must be compatible with immutable accepted history.

Do not introduce schema behavior whose normal persistence path implies replacement or destructive mutation of an accepted observation.

No `INSERT OR REPLACE`-style semantic policy belongs in WP06.

## 15. Retrieval Ordering and Indexes

WP09 owns retrieval implementation, but WP06 must ensure the physical model supports efficient deterministic retrieval by:

1. exact target;
2. ascending semantic instant.

Add only indexes/keys justified by this Release 1.1 retrieval shape.

Avoid speculative indexes.

If the primary/unique key already provides the required access pattern, do not add redundant indexes without evidence.

## 16. Schema Definition Strategy

Use the repository's accepted architecture and WP02 decision to choose the minimum schema-definition mechanism.

Acceptable examples may include:

- explicit SQL DDL embedded as an Infrastructure-owned schema definition;
- an Infrastructure-owned schema/version artifact;
- another mechanism explicitly accepted by WP02.

Do not introduce a migration framework merely because one exists in the ecosystem unless WP02/manifest explicitly requires it.

Do not introduce EF Core or an ORM unless already authorized.

## 17. Layer Ownership

All storage-engine-specific physical model artifacts belong in **Infrastructure** unless the accepted repository architecture explicitly establishes another physical-storage owner.

Mandatory dependency rules remain:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

WP06 must not add storage references to Domain or Application.

## 18. Application Contract Preservation

Do not modify WP04/WP05 public semantics simply to accommodate physical storage.

In particular, do not change:

- `IHistoricalObservationStore`;
- persistence outcome vocabulary;
- persistence failure vocabulary;
- historical result semantics;
- persistence-use-case request/result semantics;

unless a demonstrable contradiction makes WP06 impossible.

If such a contradiction exists, stop and report it rather than silently rewriting accepted contracts.

## 19. WP07 Boundary Protection

WP07 — Storage Engine & Connection Boundary — is not authorized.

WP06 must not implement:

- connection factories;
- connection ownership/lifetime;
- database-path configuration;
- connection-string configuration;
- open/close behavior;
- transaction orchestration;
- connection health checks;
- runtime initialization service;
- DI registration for database connections.

A schema definition may be representable without owning connection execution.

## 20. WP08 Protection

WP08 — Observation Persistence — is not authorized.

Do not implement:

- `IHistoricalObservationStore.Persist`;
- insert commands executed against a live database;
- duplicate lookup logic;
- idempotency comparison logic;
- conflict detection implementation;
- persistence transactions;
- retry behavior.

WP06 defines the model those behaviors will use.

## 21. WP09 Protection

WP09 — Historical Observation Retrieval — is not authorized.

Do not implement:

- `IHistoricalObservationStore.Retrieve`;
- database queries executed for historical retrieval;
- row materialization;
- reconstruction of `HistoricalObservationResult`.

The physical model must merely make the later retrieval contract implementable.

## 22. WP10–WP12 Protection

Do not implement:

- storage validation/failure mapping beyond schema-level invariants;
- DI registration/configuration;
- Worker persistence orchestration;
- acquisition + persistence execution;
- runtime storage initialization.

## 23. Tests

Follow the Release 1.1 file manifest exactly.

If WP06 authorizes narrowly scoped schema/model tests, create only those necessary to prove physical-model invariants.

If the manifest reserves comprehensive Infrastructure persistence testing for WP14, do not preempt WP14.

At minimum, validation evidence must prove through compilation, inspection, or authorized focused tests that the schema expresses:

- exact target identity;
- exact semantic-instant identity;
- lossless decimal representation;
- uniqueness;
- deterministic chronological ordering support.

Do not broaden the permanent test architecture without authority.

## 24. Expected File Scope

Use `RELEASE_1.1_FILE_MANIFEST.md` as the source of truth for exact WP06 file ownership.

Do not invent additional files merely to make the implementation look complete.

Any necessary deviation must be:

- minimal;
- directly required;
- reported explicitly;
- consistent with the execution plan;
- incapable of being deferred to a later WP.

If the manifest and repository truth conflict, stop rather than silently expanding scope.

## 25. Package and Project References

Record exact deltas.

Expected architectural principle:

- Domain project references: unchanged;
- Application project references: unchanged;
- Worker project references: unchanged;
- Infrastructure remains the physical-storage owner.

Any new package must be justified under Section 8.

Do not add cross-layer project references to facilitate schema implementation.

## 26. Security

WP06 must not introduce:

- credentials;
- API keys;
- passwords;
- production connection strings;
- machine-specific database paths;
- secrets in logs;
- committed local database files.

Use no real secret during validation.

## 27. Formatting and Whitespace

Before acceptance:

- run formatting verification;
- run `git diff --check`;
- run `git diff --cached --check`.

If whitespace findings exist in files authorized for WP06 modification, you may correct only the reported whitespace in those authorized files when necessary for acceptance, provided semantic content remains unchanged.

Do not normalize unrelated predecessor artifacts.

Do not create a recursive governance authority merely to remove whitespace from an already authorized WP06 file.

## 28. Mutation Accounting

At completion, report:

- every file added;
- every file modified;
- every file deleted;
- every package delta;
- every project-reference delta;
- every test delta;
- every unexpected path;
- cumulative authorized predecessor artifacts preserved.

WP06 must not stage or commit its work.

## 29. Final Technical Validation

After implementation, run:

1. restore;
2. format verification;
3. build;
4. Domain.Tests;
5. Application.Tests;
6. Infrastructure.Tests;
7. Architecture.Tests;
8. canonical `eng/verify.ps1`;
9. `git diff --check`;
10. `git diff --cached --check`.

Report exact totals, warnings, errors, failures, and skipped tests.

No acceptance on a red gate.

## 30. Architecture Validation

Explicitly verify:

- Domain has no new dependency;
- Application has no Infrastructure/storage dependency;
- Infrastructure remains dependent only in allowed directions;
- Worker has no WP06 change unless explicitly manifest-authorized;
- no cycles were introduced;
- storage-specific physical model types do not leak into Application contracts.

## 31. Semantic Validation Matrix

Report explicit PASS/FAIL for:

| Requirement | Required result |
|---|---|
| Exact target preservation | PASS |
| Semantic instant identity preservation | PASS |
| Timestamp round-trip fidelity | PASS |
| Decimal round-trip fidelity | PASS |
| Unique target + instant enforcement | PASS |
| Immutable-history compatibility | PASS |
| Ascending retrieval support | PASS |
| Equivalent duplicate semantics remain implementable | PASS |
| Conflict semantics remain implementable | PASS |
| Successful empty retrieval remains implementable | PASS |
| Application storage independence | PASS |
| Domain storage independence | PASS |

## 32. Git / GitHub Protection

WP06 execution must not:

- create a branch;
- stage files;
- create a commit;
- amend;
- reset;
- rebase;
- push;
- create a PR;
- merge;
- create a tag;
- create a GitHub Release;
- rewrite history.

GitHub mutation is limited to the lifecycle of issue #108 / its existing Project item.

## 33. Planning Protection

Do not:

- modify milestone #52 except through normal issue-count effects;
- modify WP01–WP05 historical planning;
- progress WP07–WP16;
- create WP17+;
- create lifecycle-gate issues;
- create or activate Release 1.2 planning;
- alter Project schema/options;
- alter labels.

## 34. WP07 Handoff

The final report must provide a precise WP07 handoff describing:

- the selected storage engine confirmed from WP02;
- physical schema/table identity;
- column names/types/nullability;
- primary/unique constraints;
- index/access strategy;
- target representation;
- semantic-instant representation;
- decimal representation;
- schema creation/version representation;
- any package introduced;
- what WP07 must implement for connection ownership without changing WP06 semantics.

WP07 must be able to implement the connection boundary without rediscovering the physical model.

## 35. Stop Conditions

Stop without claiming completion if any of these occurs:

- predecessor state is not accepted;
- issue dependency does not match;
- manifest does not authorize required files/dependencies;
- accepted WP03–WP05 semantics cannot be represented losslessly;
- SQLite/selected technology cannot satisfy the accepted model under current authority;
- implementation requires WP07+ behavior;
- baseline or final validation fails for reasons requiring unauthorized repair;
- unexpected repository drift makes scope ambiguous;
- architecture boundaries would be violated.

Report the smallest required corrective authority.

## 36. Acceptance Criteria

WP06 is complete only if all are true:

- starting-state gates pass;
- issue #108 was the only progressed issue;
- physical model is explicit and minimal;
- exact target identity is preserved;
- semantic instant identity/fidelity is preserved;
- decimal fidelity is lossless;
- target + instant uniqueness is enforceable;
- schema supports immutable historical observations;
- retrieval ordering/access is supported;
- no WP07 connection boundary was implemented;
- no WP08 persistence implementation was implemented;
- no WP09 retrieval implementation was implemented;
- Domain/Application remain storage independent;
- package/reference changes are authorized and minimal;
- security constraints pass;
- restore/build/tests/architecture/canonical verification pass;
- both diff checks pass;
- no Git integration action occurred;
- WP07 was not started.

## 37. Successful Completion Actions

Only after every acceptance criterion passes:

1. add concise completion evidence to issue #108 if repository convention requires it;
2. close issue #108;
3. verify its Project status is `Done` (allowing normal automation);
4. verify WP07 issue #109 remains Open/Backlog;
5. verify milestone #52 remains Open;
6. verify Release 1.2 remains inactive.

Do not begin WP07.

## 38. Required Execution Report

Return a detailed **Release 1.1 WP06 Execution Report** covering at minimum:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. WP02 Technology Reconciliation
10. Accepted Semantic Inputs
11. Physical Model Design
12. Table/Object Definition
13. Column Definitions
14. Target Representation
15. Semantic-Instant Representation
16. Decimal Representation
17. Identity/Uniqueness
18. Immutability Compatibility
19. Retrieval/Index Strategy
20. Schema Definition/Version Strategy
21. Layer Ownership
22. Application/Domain Preservation
23. Exact Files Added/Modified
24. Package/Reference Delta
25. Test Delta
26. WP07 Protection
27. WP08/WP09 Protection
28. Security
29. Whitespace/Diff Evidence
30. Restore/Build Evidence
31. Permanent Test Evidence
32. Canonical Verification
33. Architecture Validation
34. Semantic Validation Matrix
35. Mutation Accounting
36. Git/GitHub Protection
37. Planning Protection
38. Findings/Blockers
39. Final Repository/GitHub State
40. WP07 Handoff
41. Final Decision
42. Next Authorized Work Package

## 39. Success Terminal

On complete success, end exactly with a concise terminal equivalent to:

`RELEASE 1.1 WP06 COMPLETE`

and identify:

`NEXT AUTHORIZED WORK PACKAGE: WP07 — Storage Engine & Connection Boundary — GitHub issue #109`

Do not start WP07.
