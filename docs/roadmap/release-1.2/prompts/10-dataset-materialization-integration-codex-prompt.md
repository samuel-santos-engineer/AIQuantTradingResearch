# Release 1.2 WP10 --- Dataset Materialization Integration --- Codex Execution Authority

## 1. Authority

You are authorized to execute **Release 1.2 WP10 --- Dataset
Materialization Integration** for:

-   Repository: `samuel-santos-engineer/AIQuantTradingResearch`
-   GitHub issue: `#130`
-   Milestone:
    `#53 — Phase 3 - Release 1.2: Research Dataset Foundation`
-   Project: `#2 — AIQuantTradingResearch Engineering Roadmap`

This prompt is the execution authority for WP10. Interpret it together
with:

-   `RELEASE_1.2_EXECUTION_PLAN.md`
-   `RELEASE_1.2_FILE_MANIFEST.md`
-   accepted WP01--WP09 artifacts and execution evidence
-   `docs/architecture/data/RESEARCH_DATASET_DEFINITION.md`
-   `docs/architecture/data/DATASET_IDENTITY_VERSION_PROVENANCE.md`
-   current Application dataset contracts
-   accepted WP05 materialization use case
-   accepted WP06 catalog model
-   accepted WP07 SQLite schema-v2 physical model
-   accepted WP08 snapshot persistence
-   accepted WP09 catalog persistence and exact lookup
-   accepted Release 1.1 historical-observation persistence/retrieval
    foundation

If these authorities materially conflict, stop before mutation and
report the conflict plus the smallest corrective authority required.

## 2. Objective

Implement the **minimum bounded Application integration required to
execute one complete dataset materialization workflow** using the
already accepted Release 1.2 seams.

WP10 must integrate, without redesigning:

1.  deterministic dataset materialization from accepted Release 1.1
    historical observations;
2.  immutable dataset snapshot persistence;
3.  immutable catalog registration;
4.  exact persisted/catalog evidence consistency;
5.  bounded success/equivalence/conflict/failure results.

The integration must orchestrate existing contracts and implementations.
It must not create a second identity model, persistence model, catalog
model, storage schema, or Worker execution flow.

## 3. Required Starting-State Gates

Before changing anything, verify and report:

1.  Repository is exactly
    `samuel-santos-engineer/AIQuantTradingResearch`.
2.  Branch is `main`.
3.  Local `main` equals `origin/main`; report both SHAs and
    ahead/behind.
4.  Staged paths are zero.
5.  Every working-tree path is classified as accepted cumulative Release
    1.2 work or explicitly authorized WP10 work.
6.  Unexpected/ambiguous paths are zero.
7.  Release 1.1 remains closed and accepted.
8.  Milestone #53 is open.
9.  WP05 issue #125 is Closed/Done.
10. WP08 issue #128 is Closed/Done.
11. WP09 issue #129 is Closed/Done.
12. WP10 issue #130 is Open/Backlog.
13. WP11 issue #131 is Open/Backlog.
14. WP10 dependencies are exactly the authoritative dependencies: WP05,
    WP08, WP09.
15. No Release 1.3 implementation has started.
16. Restore, format, build, permanent tests, architecture tests,
    canonical verification, and Git whitespace checks pass before
    mutation.

Do not move #130 to In Progress until every starting-state gate passes.

If a gate fails, stop without repository/GitHub mutation and end with:

`RELEASE 1.2 WP10 BLOCKED`

## 4. Mandatory Reconciliation

### WP02 --- Dataset semantics

Preserve:

-   exact target;
-   `[from, to)` selection;
-   deterministic ascending semantic-instant ordering;
-   valid successful empty materialization;
-   immutable snapshot semantics;
-   exact timestamp-offset and decimal fidelity.

### WP03 --- Identity/version/provenance

Preserve the four typed identities:

-   Dataset Definition Identity;
-   Research Dataset Identity;
-   Source State Identity;
-   Dataset Snapshot Identity.

Preserve:

-   Dataset Version = Snapshot Identity;
-   `aiq-dataset-identity-v1`;
-   deterministic identity computation;
-   provenance and narrow lineage;
-   immutable identities;
-   equivalent rematerialization semantics;
-   integrity-conflict semantics.

WP10 must not recompute identities independently of the accepted WP05
materialization behavior.

### WP04 --- Application contracts

Reuse the accepted dataset contracts and bounded result/failure
vocabulary.

Do not introduce Infrastructure types into Application.

If integration requires an Application-owned orchestration contract,
keep it minimal and aligned with the file manifest.

### WP05 --- Materialization

`IMaterializeDatasetUseCase` / `MaterializeDatasetUseCase` remain
authoritative for producing a deterministic `DatasetSnapshotCandidate`.

WP10 must not duplicate:

-   historical observation retrieval;
-   `[from,to)` filtering;
-   ordering;
-   identity computation;
-   coverage construction;
-   provenance construction;
-   lineage construction.

### WP06 --- Catalog model

`DatasetCatalogEntry` remains the authoritative catalog descriptor.

Do not add mutable latest/status/operational metadata.

### WP07 --- Physical storage

SQLite schema v2 is fixed.

WP10 must not change schema version, tables, columns, constraints,
indexes, records, or physical encoding unless a proven authority
contradiction requires stopping.

### WP08 --- Snapshot persistence

`IDatasetSnapshotStore` and `SqliteDatasetSnapshotStore` own immutable
snapshot persistence.

Preserve:

-   `NewlyAccepted`;
-   `EquivalentExisting`;
-   `IntegrityConflict`;
-   atomicity;
-   immutability;
-   empty snapshots;
-   multiple-version coexistence;
-   fidelity.

### WP09 --- Catalog persistence

`IDatasetCatalog` and `SqliteDatasetCatalog` own catalog registration
and exact Snapshot Identity lookup.

Preserve:

-   `NewlyRegistered`;
-   `EquivalentExisting`;
-   `IntegrityConflict`;
-   exact lookup;
-   `NotFound`;
-   immutable evidence.

WP10 must not add new catalog SQL or duplicate catalog persistence.

## 5. Integration Boundary

Implement one focused Application-owned integration/use-case boundary
whose responsibility is:

1.  accept an existing `DatasetDefinition`;
2.  invoke the accepted materialization use case;
3.  if materialization fails, stop and propagate/map only according to
    existing authorized Application semantics;
4.  persist the resulting immutable snapshot candidate through
    `IDatasetSnapshotStore`;
5.  register the corresponding `DatasetCatalogEntry` through
    `IDatasetCatalog`;
6.  reconcile persistence and catalog outcomes deterministically;
7.  return one bounded integration result that distinguishes successful
    new acceptance, equivalent existing evidence, integrity conflict,
    and authorized failure conditions.

Prefer composition of existing contracts over new abstractions.

Do not perform DI registration or Worker invocation; WP12 owns
composition/execution.

## 6. Consistency Model

The integration must explicitly define and implement the consistency
relationship between snapshot persistence and catalog registration.

Required invariants:

-   catalog registration must never describe evidence contradictory to
    the accepted snapshot candidate;
-   a catalog entry must be constructed from the exact accepted
    materialization candidate;
-   same Snapshot Identity with contradictory evidence must never be
    overwritten;
-   equivalent existing snapshot/catalog evidence must be accepted
    deterministically;
-   multiple versions of one Research Dataset must coexist;
-   no mutable latest pointer is introduced;
-   successful integration must leave snapshot evidence and catalog
    evidence semantically consistent.

First reconcile the actual WP08/WP09 implementation. Because WP09
delegates registration to the accepted snapshot store, do not invent a
distributed-transaction problem if repository truth shows both seams
converge on the same immutable evidence.

Document the observed consistency model and use the smallest
orchestration compatible with it.

## 7. Outcome Reconciliation

Define the minimum deterministic outcome matrix using the existing
WP08/WP09 vocabularies.

At minimum reason about:

### New candidate

If snapshot persistence accepts new evidence and catalog registration
registers/equates the same evidence, return the appropriate successful
integrated outcome.

### Equivalent rematerialization

If the same deterministic candidate already exists and catalog evidence
is equivalent, return an equivalent/idempotent successful outcome. Do
not create another semantic version.

### Integrity conflict

Any contradiction under the same Snapshot Identity must produce an
integrity-conflict result and must not overwrite evidence.

### Materialization failure

A materialization failure must prevent snapshot/catalog mutation.

### Snapshot persistence failure

If snapshot persistence returns an authorized failure, catalog
registration must not proceed unless existing contract semantics make
doing so provably safe and required. Prefer fail-stop behavior.

### Catalog registration failure

Handle only according to currently authorized contracts. Do not redesign
generalized validation/failure mapping owned by WP11.

The final report must include the exact integration outcome matrix
derived from repository truth.

## 8. Idempotency and Re-execution

Equivalent execution of the same definition against the same relevant
source state must:

-   produce the same semantic identities and Dataset Version;
-   preserve the same snapshot evidence;
-   preserve the same catalog evidence;
-   never overwrite;
-   never create a new semantic version;
-   return deterministic equivalent-success semantics.

Re-execution after a relevant source-state change may produce a distinct
Snapshot Identity/version while retaining earlier immutable evidence.

## 9. Empty Materialization

A valid empty materialization must flow through the complete
integration:

-   materialization succeeds with zero selected observations;
-   immutable snapshot evidence is accepted/equated;
-   catalog metadata represents count zero and absent actual first/last
    observation instants;
-   exact catalog lookup can represent the accepted empty snapshot;
-   equivalent re-execution remains deterministic.

No sentinel observation or fake timestamp may be introduced.

## 10. Fidelity

WP10 must preserve without transformation:

-   exact target;
-   requested boundaries;
-   selected observation membership;
-   semantic instant;
-   original `DateTimeOffset` offset;
-   exact decimal value;
-   all four identities;
-   Dataset Version;
-   coverage;
-   provenance;
-   lineage;
-   source authority.

No culture-sensitive formatting, local-time conversion, floating-point
intermediary, truncation, or rounding.

## 11. Failure Boundary

WP11 owns generalized Dataset Validation & Failure Mapping.

WP10 may only define/map the minimum integration-level outcomes
necessary to compose existing Application contracts.

Do not:

-   create a comprehensive SQLite failure classifier;
-   add retries;
-   add resilience policies;
-   add arbitrary exception swallowing;
-   catch `Exception`;
-   invent provider retry semantics;
-   convert integrity conflicts into success;
-   collapse distinct existing failures without explicit semantic
    justification.

Unknown/unowned failures should remain visible for WP11 rather than
being falsely classified.

## 12. Files and Layer Ownership

Follow `RELEASE_1.2_FILE_MANIFEST.md` exactly.

Expected WP10 changes should be concentrated in Application
integration/use-case files.

Do not change Domain, Infrastructure, or Worker merely for convenience.

Infrastructure changes are prohibited unless the accepted contracts
cannot be integrated due to a proven defect. If such a defect is
discovered, stop and report the authority conflict rather than silently
expanding WP10.

Do not add packages or project references.

## 13. Permanent Tests and Temporary Probes

Permanent test expansion remains owned by WP13/WP14 unless the manifest
explicitly authorizes otherwise.

Expected WP10 permanent-test delta: `0`.

Temporary offline probes are allowed and must be removed before
completion.

Focused validation must prove at least:

1.  new non-empty materialization integrates successfully;
2.  equivalent re-execution is deterministic/idempotent;
3.  relevant source-state change can produce a distinct immutable
    version;
4.  valid empty materialization integrates successfully;
5.  materialization failure causes no snapshot/catalog mutation;
6.  snapshot integrity conflict is non-destructive;
7.  catalog integrity conflict is non-destructive if representable
    independently;
8.  exact catalog lookup after success returns equivalent accepted
    evidence;
9.  identities/version remain unchanged across boundaries;
10. target/boundary/coverage fidelity;
11. provenance/lineage fidelity;
12. timestamp/offset/decimal fidelity;
13. multiple versions coexist;
14. no provider/network call;
15. no temporary SQLite residue.

Use hand-written stubs/fakes at Application boundaries where that
provides clearer deterministic integration proof. Use real offline
SQLite only if needed to prove the existing concrete seams work
together, without turning WP10 into WP14.

## 14. Explicitly Prohibited Scope

Do not start:

-   WP11 --- Dataset Validation & Failure Mapping;
-   WP12 --- Dependency Registration & Bounded Dataset Execution;
-   WP13 --- Domain & Application Dataset Tests;
-   WP14 --- Infrastructure & Dataset Tests;
-   WP15 --- Architecture & Documentation Alignment;
-   WP16 --- Full Validation, Integration & Acceptance;
-   Release 1.3.

Do not add:

-   Worker execution;
-   DI registration;
-   configuration keys;
-   scheduling;
-   loops/background services;
-   automatic refresh;
-   streaming;
-   pipelines/DAGs;
-   monitoring;
-   retries/circuit breakers;
-   schema v3;
-   mutable latest pointers;
-   new catalog search/list APIs;
-   provider acquisition behavior.

Do not stage, commit, push, branch, create a PR, merge, tag, or release.

## 15. Architecture Protection

Production graph must remain:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

Required leakage after WP10:

-   Domain SQLite/SQL/filesystem: 0
-   Application SQLite/SQL/filesystem: 0
-   Domain provider/HTTP: 0
-   Application provider/HTTP: 0

Production dependency cycles: 0.

## 16. Security and Offline Execution

Validation must remain offline except for GitHub lifecycle
inspection/mutation.

No:

-   real provider calls;
-   real API keys;
-   secrets;
-   personal paths committed to source;
-   external market-data acquisition.

Run canonical secret scanning.

## 17. GitHub Lifecycle

After all starting gates and initial validation pass:

1.  move #130 Backlog → In Progress;
2.  execute WP10;
3.  complete all validation;
4.  post concise evidence to #130;
5.  close #130 and set Project status Done only after all acceptance
    gates pass.

Issue #131 must remain Open/Backlog.

Do not mutate unrelated planning objects.

## 18. Required Final Validation

Before completion, run and report:

-   restore;
-   format verification;
-   build;
-   Domain.Tests;
-   Application.Tests;
-   Infrastructure.Tests;
-   Architecture.Tests;
-   total permanent tests;
-   `eng/verify.ps1`;
-   secret scan;
-   `git diff --check`;
-   `git diff --cached --check`;
-   direct whitespace checks for relevant untracked WP10 files;
-   architecture/leakage validation;
-   Release 1.1 persistence regression;
-   WP08 snapshot-persistence regression;
-   WP09 catalog regression;
-   integration validation matrix;
-   temporary SQLite residue;
-   package/reference delta;
-   working-tree classification.

Expected permanent baseline if unchanged:

-   Domain.Tests: 11
-   Application.Tests: 42
-   Infrastructure.Tests: 79
-   Architecture.Tests: 13
-   Total: 145

Reconcile legitimate accepted changes rather than forcing stale counts.

## 19. Acceptance Matrix

WP10 completes only if all applicable rows pass:

-   WP05/WP08/WP09 dependencies accepted;
-   issue #130 lifecycle valid;
-   existing materialization use case reused;
-   existing snapshot store reused;
-   existing catalog reused;
-   no duplicate identity computation;
-   no duplicate snapshot persistence;
-   no duplicate catalog persistence;
-   bounded integration contract/use case implemented;
-   new candidate integration passes;
-   equivalent rematerialization passes;
-   deterministic idempotency passes;
-   relevant source-state change/version coexistence passes;
-   empty materialization passes;
-   materialization failure causes no persistence/catalog mutation;
-   snapshot conflict remains non-destructive;
-   catalog conflict remains non-destructive;
-   exact catalog evidence after success passes;
-   identities/version fidelity passes;
-   target/boundary/coverage fidelity passes;
-   provenance/lineage fidelity passes;
-   timestamp/offset/decimal fidelity passes;
-   consistency model explicitly reconciled;
-   Domain/Application storage leakage = 0;
-   provider/network calls = 0;
-   production graph unchanged;
-   permanent-test delta = 0 unless manifest-authorized;
-   package/reference delta = 0/0;
-   temporary residue = 0;
-   WP11 not started;
-   Release 1.3 not started;
-   canonical validation passes.

## 20. Required Execution Report

Return a numbered report covering at least:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Predecessor/Lifecycle Gates
7.  Issue Lifecycle
8.  Initial Baseline
9.  WP02 Semantic Reconciliation
10. WP03 Identity Reconciliation
11. WP04 Contract Reconciliation
12. WP05 Materialization Reconciliation
13. WP06 Catalog-Model Reconciliation
14. WP07 Physical-Model Reconciliation
15. WP08 Snapshot-Persistence Reconciliation
16. WP09 Catalog-Persistence Reconciliation
17. Integration Boundary Design
18. Integration Contract/Use-Case Design
19. Consistency Model
20. New Candidate Flow
21. Equivalent Re-materialization Flow
22. Relevant Source-State Change Flow
23. Empty Materialization Flow
24. Materialization Failure Flow
25. Snapshot Persistence Failure/Conflict Flow
26. Catalog Registration Failure/Conflict Flow
27. Outcome Reconciliation Matrix
28. Exact Catalog Evidence Validation
29. Identity / Version Fidelity
30. Target / Boundary / Coverage Fidelity
31. Provenance / Lineage Fidelity
32. Timestamp / Offset / Decimal Fidelity
33. Idempotency / Re-execution Evidence
34. Multiple-Version Coexistence
35. Failure-Boundary Decision
36. Exact Files Added/Modified
37. Domain Delta
38. Application Delta
39. Infrastructure Delta
40. Worker Delta
41. Package/Reference Delta
42. Permanent Test Delta
43. Temporary Probe Evidence
44. WP11/WP12 Protection
45. Release 1.3 Protection
46. Security / Offline Evidence
47. Whitespace / Diff Evidence
48. Restore / Build Evidence
49. Permanent Test Evidence
50. Canonical Verification
51. Architecture Validation
52. Release 1.1 Persistence Regression
53. WP08 Snapshot-Persistence Regression
54. WP09 Catalog Regression
55. Integration Validation Matrix
56. Mutation Accounting
57. Git / GitHub Protection
58. Planning Protection
59. Findings / Blockers
60. Final Repository / GitHub State
61. WP11 Handoff
62. Final Decision
63. Next Authorized Work Package

## 21. Terminal Markers

On success end exactly with:

`RELEASE 1.2 WP10 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP11 — Dataset Validation & Failure Mapping — GitHub issue #131`

If blocked, end with:

`RELEASE 1.2 WP10 BLOCKED`

and state the smallest corrective authority required.

## 22. Execution Principle

WP10 is an **integration work package, not a redesign work package**.

Use GPT-5.6 Sol's deeper reasoning to reconcile the already accepted
boundaries before mutation, especially the relationship among
materialization, snapshot persistence, and catalog registration.

Prefer the smallest deterministic orchestration that composes WP05 +
WP08 + WP09 while preserving all accepted semantics and leaving
generalized validation/failure mapping, DI, Worker execution, and
permanent coverage to their assigned later work packages.
