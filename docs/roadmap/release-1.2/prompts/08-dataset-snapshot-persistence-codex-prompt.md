# Release 1.2 WP08 --- Dataset Snapshot Persistence --- Codex Execution Authority

## 1. Purpose

Execute **Release 1.2 WP08 --- Dataset Snapshot Persistence** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue: **#128**

This work package implements the Infrastructure-owned persistence
boundary for immutable Release 1.2 dataset snapshots using the
**accepted WP07 SQLite schema version 2 and physical model**.

WP08 must consume the accepted WP02--WP07 semantics and physical
representation. It must **not redesign dataset identity, catalog
semantics, schema structure, schema versioning, materialization, lookup,
failure classification, dependency registration, Worker execution, or
Release 1.3 behavior**.

This prompt is execution authority for WP08 only.

------------------------------------------------------------------------

## 2. Required Model

Execute this work package using **GPT-5.6 Terra**.

WP07 already resolved the architecture-heavy physical-storage decisions.
WP08 is a bounded implementation work package and should prefer direct,
minimal implementation over architectural reconsideration.

Do not switch models merely to broaden or redesign the scope.

------------------------------------------------------------------------

## 3. Controlling Authorities

Before mutation, read and reconcile completely:

1.  `RELEASE_1.2_EXECUTION_PLAN.md`
2.  `RELEASE_1.2_FILE_MANIFEST.md`
3.  Release 1.2 GitHub-planning authority and accepted planning result
4.  WP01--WP07 prompt pairs and accepted execution results
5.  `docs/architecture/data/RESEARCH_DATASET_DEFINITION.md`
6.  `docs/architecture/data/DATASET_IDENTITY_VERSION_PROVENANCE.md`
7.  Current Application dataset contracts and materialization
    implementation
8.  Current Release 1.1 SQLite persistence implementation
9.  Current WP07 SQLite dataset physical model
10. Existing permanent tests and architecture rules
11. GitHub issue #128, milestone #53, and Project #2 state

If repository truth and this prompt appear inconsistent, stop before
mutation and report the smallest corrective authority required.

Do not silently reinterpret accepted WP02--WP07 decisions.

------------------------------------------------------------------------

## 4. Mandatory Starting-State Gates

Before changing any repository file:

### 4.1 Repository identity

Verify:

-   repository is `samuel-santos-engineer/AIQuantTradingResearch`;
-   current branch is `main`;
-   local `main` and `origin/main` are synchronized;
-   ahead/behind is `0/0`;
-   staged paths are `0`.

### 4.2 Working-tree classification

The repository may contain accepted cumulative uncommitted/untracked
Release 1.2 artifacts.

Classify every existing path before mutation as:

-   accepted cumulative Release 1.2 governance/implementation work;
-   authorized WP08 authority artifacts;
-   unexpected/ambiguous.

Do not delete, normalize, stage, or rewrite accepted cumulative
artifacts.

If any unexpected or ambiguous path could overlap WP08, stop.

### 4.3 Lifecycle

Verify:

-   WP07 issue #127 is **Closed / Done**;
-   WP08 issue #128 is **Open / Backlog**;
-   WP09 issue #129 is **Open / Backlog**;
-   milestone #53 is open;
-   WP08 dependencies match the authoritative Release 1.2 dependency
    graph;
-   no Release 1.3 implementation has started.

Do not move #128 to In Progress until all starting-state and baseline
gates pass.

### 4.4 WP07 physical-model gate

Verify the accepted WP07 state exists:

-   SQLite schema version = **2**;
-   Release 1.1 `historical_observations` remains intact;
-   `dataset_snapshots` exists in the accepted physical model;
-   `dataset_snapshot_observations` exists in the accepted physical
    model;
-   `SqliteDatasetSnapshotRecord` exists;
-   `SqliteDatasetObservationRecord` exists;
-   `SqliteDatasetMapper` exists;
-   clean bootstrap supports version 2;
-   v1 → v2 upgrade is non-destructive;
-   version 2 bootstrap is idempotent;
-   version 3 is unsupported;
-   permanent baseline remains 145 tests unless repository truth has
    legitimately advanced.

If this physical model is absent or materially different, stop.

------------------------------------------------------------------------

## 5. Baseline Validation

Before implementation run the repository's normal validation path.

At minimum:

-   restore;
-   format verification;
-   build;
-   Domain tests;
-   Application tests;
-   Infrastructure tests;
-   Architecture tests;
-   `eng/verify.ps1`;
-   `git diff --check`;
-   `git diff --cached --check`.

Expected accepted baseline at WP07 handoff:

-   Domain.Tests: 11
-   Application.Tests: 42
-   Infrastructure.Tests: 79
-   Architecture.Tests: 13
-   permanent total: 145
-   warnings/errors: 0/0

If repository truth has legitimately advanced, reconcile and report it.
Do not manufacture old counts.

After the baseline passes, move issue #128 from Backlog to In Progress.

------------------------------------------------------------------------

## 6. Accepted Semantic Model --- Do Not Redesign

WP08 must preserve the following accepted semantics.

### 6.1 Dataset semantics

A Research Dataset Snapshot is immutable evidence of one deterministic
materialization.

The definition is:

-   one exact target;
-   explicit `[from, to)` semantic-instant boundaries;
-   deterministic ascending semantic-instant ordering;
-   valid even when zero observations are selected.

### 6.2 Identity semantics

Keep distinct:

-   Dataset Definition Identity;
-   Research Dataset Identity;
-   Source State Identity;
-   Dataset Snapshot Identity.

Dataset Version remains the immutable Snapshot Identity representation.

Identity scheme remains:

`aiq-dataset-identity-v1`

Identity values remain validated 64-character lowercase hexadecimal
SHA-256 fingerprints.

WP08 must **not recompute identities**. It persists the already accepted
candidate.

### 6.3 Fidelity

Persist and recoverable evidence must preserve:

-   exact target text;
-   semantic instant;
-   original `DateTimeOffset` offset representation;
-   exact decimal price;
-   requested boundaries;
-   coverage;
-   observation count;
-   deterministic observation ordering;
-   provenance;
-   narrow lineage.

No floating-point conversion is permitted.

### 6.4 Immutability and equivalence

Previously accepted snapshot evidence must never be overwritten.

An equivalent re-materialization may be recognized as already existing.

Contradictory evidence under the same Snapshot Identity is an integrity
conflict.

No update/replace/delete/destructive upsert behavior is allowed.

------------------------------------------------------------------------

## 7. Accepted WP07 Physical Model --- Consume As-Is

WP08 must use schema version **2**.

### 7.1 Tables

Use:

-   `dataset_snapshots`
-   `dataset_snapshot_observations`

Do not introduce an alternative dataset persistence table.

### 7.2 Snapshot descriptor

The accepted descriptor representation includes, at minimum, the
WP07-defined physical fields for:

-   snapshot identity;
-   definition identity;
-   research dataset identity;
-   source-state identity;
-   identity scheme;
-   exact target;
-   requested boundary UTC ticks and offsets;
-   ordering;
-   observation count;
-   source authority;
-   nullable first/last actual observation UTC ticks and offsets.

Dataset Version is represented by Snapshot Identity and must not be
stored as an independent drifting value.

### 7.3 Observation membership

Persist ordered membership using the accepted fields:

-   snapshot identity;
-   zero-based ordinal;
-   UTC ticks;
-   original offset minutes;
-   invariant decimal price text.

The physical model already provides:

-   primary key `(snapshot_identity, ordinal)`;
-   unique `(snapshot_identity, instant_utc_ticks)`;
-   restrictive foreign-key relationship to the snapshot descriptor.

### 7.4 Representation rules

Preserve:

-   target as `TEXT COLLATE BINARY`;
-   UTC ticks + offset minutes for timestamps;
-   offset range `[-840, 840]`;
-   invariant decimal `G29` text;
-   no SQLite `REAL`;
-   zero-observation snapshot as one descriptor row and zero membership
    rows;
-   no sentinel observation.

Do not change schema version or bootstrap design in WP08 unless a proven
blocker requires new authority.

------------------------------------------------------------------------

## 8. Authorized Implementation Scope

Implement the **minimum Infrastructure-owned snapshot persistence
behavior** necessary to fulfill the existing Application
`IDatasetSnapshotStore` contract.

Prefer extending the existing SQLite persistence area rather than
introducing generalized repositories/frameworks.

WP08 may add or modify only files authorized by the Release 1.2 file
manifest for WP08.

The implementation may include a focused SQLite dataset snapshot store
and private helpers strictly necessary for:

-   mapping the accepted candidate through the WP07 mapper;
-   inserting snapshot descriptor evidence;
-   inserting ordered snapshot-observation membership;
-   recognizing equivalent existing evidence;
-   detecting contradictory existing evidence;
-   returning the existing Application-owned persistence outcome/failure
    model;
-   transaction ownership and deterministic disposal.

Do not modify Application contracts unless the current accepted contract
is literally impossible to implement. If so, stop and report the
blocker.

------------------------------------------------------------------------

## 9. Persistence Algorithm

Implement a deterministic insert-only algorithm.

For one `DatasetSnapshotCandidate`:

1.  Validate only what is necessary at the Infrastructure boundary
    without changing Application semantics.
2.  Obtain a fresh open connection through the accepted SQLite
    connection boundary.
3.  Begin one transaction covering the complete snapshot.
4.  Determine whether the Snapshot Identity already exists.
5.  If it does not exist:
    -   insert exactly one snapshot descriptor;
    -   insert all observation-membership rows in accepted ordinal
        order;
    -   commit;
    -   return the existing outcome representing newly
        accepted/registered snapshot evidence.
6.  If it exists:
    -   reconstruct/compare the complete existing persisted evidence
        required to establish semantic equivalence;
    -   if equivalent, perform no mutation and return the existing
        equivalent/idempotent outcome;
    -   if contradictory, perform no mutation and return the existing
        integrity-conflict outcome.
7.  On any unsuccessful write before commit, the transaction must leave
    no partial snapshot evidence.

Do not implement a destructive repair path.

------------------------------------------------------------------------

## 10. Equivalence Requirements

Do not treat equal Snapshot Identity alone as sufficient evidence of
equivalence.

For an existing Snapshot Identity, compare enough persisted evidence to
prove that the stored snapshot is the same accepted semantic snapshot.

At minimum reconcile:

-   all four identities;
-   identity scheme;
-   target;
-   requested boundaries and offsets;
-   ordering;
-   observation count;
-   source authority;
-   first/last actual coverage values;
-   complete ordered observation membership;
-   each observation instant;
-   each original offset;
-   each exact decimal value.

If persisted content contradicts the candidate under the same Snapshot
Identity, return the accepted integrity-conflict outcome.

Do not overwrite it.

------------------------------------------------------------------------

## 11. Empty Snapshot Requirements

A valid empty candidate must persist successfully.

It must result in:

-   exactly one `dataset_snapshots` descriptor;
-   observation count = 0;
-   first actual observation = null;
-   last actual observation = null;
-   zero `dataset_snapshot_observations` rows.

Repeated persistence of the equivalent empty snapshot must be
idempotent/equivalent.

No sentinel row is permitted.

------------------------------------------------------------------------

## 12. Atomicity

The descriptor and all observation membership rows form one immutable
snapshot.

They must commit atomically.

Mandatory cases:

-   descriptor succeeds + observation fails → rollback all;
-   N observations succeed + later observation fails → rollback all;
-   conflict detected before insertion → no mutation;
-   conflict detected against existing evidence → no mutation;
-   equivalent existing evidence → no mutation.

Do not leave partial descriptor or membership rows.

------------------------------------------------------------------------

## 13. Connection and Transaction Ownership

Reuse the accepted Release 1.1/WP07 SQLite connection boundary.

Requirements:

-   no global/shared live connection;
-   store operation owns its connection lifetime;
-   transaction is operation-local;
-   command objects are disposed deterministically;
-   connection is disposed deterministically;
-   DI does not own a live SQLite connection.

Do not redesign connection ownership.

------------------------------------------------------------------------

## 14. Failure Boundary

WP11 owns comprehensive **Dataset Validation & Failure Mapping**.

Therefore WP08 must not build a new generalized SQLite
error-classification framework.

Preserve existing Application result/failure vocabulary and existing
Release 1.1 storage behavior.

WP08 may perform the minimum mapping already required by the existing
snapshot-store contract for:

-   invalid candidate data already representable by accepted contracts;
-   equivalent existing snapshot;
-   integrity conflict.

Raw SQLite operational failure behavior that is explicitly assigned to
WP11 should remain available for WP11 unless an existing shared Release
1.1 boundary already maps it naturally without redesign.

Do not add retry, resilience, repair, or availability policy.

------------------------------------------------------------------------

## 15. WP09 Protection

WP09 owns **Dataset Catalog Persistence & Lookup**.

WP08 must not implement `IDatasetCatalog`.

Do not add:

-   generalized catalog registration;
-   public catalog lookup;
-   catalog search;
-   latest-version lookup;
-   target/date-range search;
-   list-all datasets;
-   mutable catalog status;
-   catalog indexes beyond the accepted WP07 physical model.

WP08 may privately read persisted snapshot evidence only as required to
classify its own persistence attempt as new, equivalent, or conflicting.

That private equivalence check is not WP09 catalog lookup behavior.

------------------------------------------------------------------------

## 16. WP10 Protection

WP10 owns **Dataset Materialization Integration**.

Do not make WP05 materialization automatically persist snapshots.

Do not orchestrate:

materialize → snapshot store → catalog

Do not modify the materialization use case to call Infrastructure.

------------------------------------------------------------------------

## 17. WP11 Protection

WP11 owns **Dataset Validation & Failure Mapping**.

Do not introduce:

-   generalized dataset storage exception classifier;
-   repair semantics;
-   corruption recovery;
-   retryability classification;
-   new public failure vocabulary.

Only implement what WP08 needs to satisfy the existing snapshot
persistence contract.

------------------------------------------------------------------------

## 18. WP12 Protection

WP12 owns **Dependency Registration & Bounded Dataset Execution**.

Do not register the snapshot store with DI unless the authoritative
manifest explicitly assigns that file/change to WP08.

Do not modify Worker execution.

Do not add configuration keys.

Do not introduce dataset execution scheduling.

------------------------------------------------------------------------

## 19. WP13/WP14 Test Ownership

WP13 and WP14 own the permanent Release 1.2 dataset test expansion.

Therefore:

-   permanent test count delta in WP08 should be **0** unless the
    Release 1.2 manifest explicitly says otherwise;
-   do not add broad permanent dataset tests;
-   do not consume WP14 test scope early.

Temporary focused offline probes are allowed when needed to prove WP08
behavior.

Any temporary probe must be removed before completion.

The existing WP07 test-count-neutral schema alignment must remain
intact.

------------------------------------------------------------------------

## 20. Required Focused Validation

Use temporary offline validation if necessary to prove the
implementation before removing the probe.

At minimum prove:

### 20.1 New non-empty snapshot

-   descriptor inserted once;
-   all observations inserted;
-   ordinal order correct;
-   count correct;
-   exact identities preserved;
-   exact target preserved;
-   boundaries preserved;
-   coverage preserved;
-   source authority preserved;
-   offsets preserved;
-   decimal values preserved.

### 20.2 Equivalent repeated persistence

Persist the same candidate again.

Prove:

-   equivalent/idempotent outcome;
-   no new descriptor;
-   no new membership rows;
-   no mutation of accepted evidence.

### 20.3 Integrity conflict

Create contradictory persisted evidence under the same Snapshot Identity
in a controlled offline test.

Prove:

-   conflict result;
-   existing evidence remains unchanged;
-   candidate does not overwrite existing evidence.

### 20.4 Empty snapshot

Prove:

-   descriptor exists;
-   count = 0;
-   actual first/last values are null;
-   membership rows = 0;
-   repeated persistence is equivalent/idempotent.

### 20.5 Atomic rollback

Force a failure after partial insertion would otherwise be possible.

Prove:

-   descriptor residue = 0 for the failed new snapshot;
-   membership residue = 0;
-   previously accepted unrelated snapshots remain intact.

### 20.6 Multiple versions

Persist two valid snapshots for the same logical Research Dataset
Identity with different Source State/Snapshot identities.

Prove coexistence without overwrite.

### 20.7 Release 1.1 regression

Historical observation persistence/retrieval remains unchanged.

### 20.8 Residue

Remove all temporary databases and probe files.

Repository SQLite/WAL/SHM/journal residue must be zero.

------------------------------------------------------------------------

## 21. Security and Offline Requirements

WP08 validation must be fully offline.

Prohibited:

-   live Twelve Data calls;
-   provider/network access;
-   real credentials;
-   secrets;
-   machine-specific persistent database paths;
-   logging connection strings or database contents unnecessarily.

Run canonical secret scanning.

------------------------------------------------------------------------

## 22. Architecture Requirements

The production dependency graph must remain:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

Requirements:

-   Domain SQLite/SQL leakage: 0
-   Application SQLite/SQL leakage: 0
-   Domain provider/HTTP leakage: 0
-   Application provider/HTTP leakage: 0
-   new project-reference edges: 0 unless explicitly authorized
-   dependency cycles: 0

SQLite dataset persistence belongs to Infrastructure.

------------------------------------------------------------------------

## 23. Package and Reference Policy

Expected WP08 package/reference delta:

`0/0`

Use the existing `Microsoft.Data.Sqlite` dependency.

Do not add:

-   ORM;
-   migration framework;
-   serialization package;
-   hashing package;
-   repository framework;
-   database abstraction framework.

If a new package is genuinely unavoidable, stop and request authority
before adding it.

------------------------------------------------------------------------

## 24. Documentation Policy

WP15 owns Architecture & Documentation Alignment.

Do not broadly update documentation in WP08.

Only modify documentation if the Release 1.2 file manifest explicitly
assigns a WP08 artifact.

Do not rewrite accepted WP02/WP03/WP07 semantic documentation.

------------------------------------------------------------------------

## 25. Git and GitHub Protection

During WP08:

Do not:

-   stage;
-   commit;
-   push;
-   create a branch;
-   create a PR;
-   merge;
-   tag;
-   create a release;
-   rewrite history.

Only the authorized issue #128 lifecycle may change.

Do not mutate issue #129 or later WP issues.

Do not modify milestone #53 except through normal issue count effects.

------------------------------------------------------------------------

## 26. Completion Validation

After implementation and after all temporary probes are removed, run:

1.  restore;
2.  format verification;
3.  build;
4.  Domain.Tests;
5.  Application.Tests;
6.  Infrastructure.Tests;
7.  Architecture.Tests;
8.  `eng/verify.ps1`;
9.  `git diff --check`;
10. `git diff --cached --check`;
11. direct whitespace checks for authorized untracked WP08 files when
    Git diff cannot see them;
12. secret scan through canonical verification;
13. temporary SQLite residue check;
14. architecture/leakage reconciliation;
15. mutation accounting.

Expected permanent-test baseline remains 145 unless repository truth
legitimately differs.

------------------------------------------------------------------------

## 27. Mandatory Acceptance Matrix

Do not close WP08 until all applicable rows pass.

  Requirement                                       Required result
  ------------------------------------------------- ----------------------------------
  WP07 predecessor                                  PASS
  Schema version                                    2
  Existing WP07 physical model reused               PASS
  `IDatasetSnapshotStore` implemented               PASS
  New snapshot persistence                          PASS
  Descriptor persistence                            PASS
  Ordered observation membership persistence        PASS
  Exact four identities preserved                   PASS
  Dataset Version / Snapshot Identity consistency   PASS
  Exact target preserved                            PASS
  Requested boundaries preserved                    PASS
  Coverage preserved                                PASS
  Provenance/source authority preserved             PASS
  Lineage evidence preserved                        PASS
  Timestamp/offset fidelity                         PASS
  Decimal fidelity                                  PASS
  Successful empty snapshot                         PASS
  Equivalent repeated persistence                   PASS
  Integrity conflict non-destructive                PASS
  Atomic rollback                                   PASS
  Immutable accepted evidence                       PASS
  Multiple logical-dataset versions coexist         PASS
  Destructive update/delete/upsert                  NO
  WP09 catalog implementation                       NO
  WP10 integration started                          NO
  WP11 generalized failure mapping started          NO
  WP12 DI/Worker execution started                  NO
  Release 1.3 implementation started                NO
  Provider/network calls                            0
  Domain SQLite/SQL leakage                         0
  Application SQLite/SQL leakage                    0
  Package/reference delta                           0/0
  Permanent test delta                              0 unless manifest says otherwise
  Temporary probe residue                           0
  Temporary SQLite residue                          0
  Build warnings/errors                             0/0
  Canonical verification                            PASS

------------------------------------------------------------------------

## 28. Issue Completion

After every required gate passes:

1.  post concise evidence to GitHub issue #128;
2.  close issue #128;
3.  set its Project #2 status to Done;
4.  verify issue #129 remains Open / Backlog;
5.  verify milestone #53 remains open;
6.  do not start WP09.

If any mandatory gate fails, leave #128 open and report WP08 as blocked.

------------------------------------------------------------------------

## 29. Required Execution Report

Return a structured report covering at least:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Predecessor/Lifecycle Gates
7.  Issue Lifecycle
8.  Initial Baseline
9.  WP02 Reconciliation
10. WP03 Reconciliation
11. WP04 Contract Reconciliation
12. WP05 Materialization Reconciliation
13. WP06 Catalog Reconciliation
14. WP07 Physical-Model Reconciliation
15. Snapshot Store Design
16. Persistence Algorithm
17. Transaction Strategy
18. New Snapshot Behavior
19. Equivalent Existing Behavior
20. Integrity Conflict Behavior
21. Empty Snapshot Behavior
22. Observation Membership / Ordering
23. Identity / Version Fidelity
24. Target / Boundary / Coverage Fidelity
25. Provenance / Lineage Fidelity
26. Timestamp / Offset Fidelity
27. Decimal Fidelity
28. Atomicity / Immutability
29. Connection Ownership / Disposal
30. Failure-Boundary Decision
31. Exact Files Added/Modified
32. Domain Delta
33. Application Delta
34. Infrastructure Delta
35. Worker Delta
36. Package/Reference Delta
37. Permanent Test Delta
38. Temporary Probe Evidence
39. WP09 Protection
40. WP10--WP12 / Release 1.3 Protection
41. Security / Offline Evidence
42. Whitespace / Diff Evidence
43. Restore / Build Evidence
44. Permanent Test Evidence
45. Canonical Verification
46. Architecture Validation
47. Release 1.1 Persistence Regression
48. Snapshot Persistence Validation Matrix
49. Mutation Accounting
50. Git / GitHub Protection
51. Planning Protection
52. Findings / Blockers
53. Final Repository / GitHub State
54. WP09 Handoff
55. Final Decision
56. Next Authorized Work Package

------------------------------------------------------------------------

## 30. Required Terminal Marker

If successful, end exactly with:

`RELEASE 1.2 WP08 COMPLETE`

Then include:

`NEXT AUTHORIZED WORK PACKAGE: WP09 — Dataset Catalog Persistence & Lookup — GitHub issue #129`

If blocked, end with:

`RELEASE 1.2 WP08 BLOCKED`

and state the smallest corrective authority required.

Do not start WP09.
