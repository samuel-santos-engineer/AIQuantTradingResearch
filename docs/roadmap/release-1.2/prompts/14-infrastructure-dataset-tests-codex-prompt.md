# Release 1.2 WP14 --- Infrastructure & Dataset Tests --- Codex Execution Authority

## Purpose

Execute **Release 1.2 WP14 --- Infrastructure & Dataset Tests** for
`samuel-santos-engineer/AIQuantTradingResearch`.

WP14 owns the permanent, isolated, offline Infrastructure test coverage
required to prove the accepted Release 1.2 dataset implementation
through WP12, while preserving Release 1.1 persistence behavior and the
permanent Application proof established by WP13.

This is a **test work package**, not a redesign work package.

## Model Recommendation

**Recommended Codex model: GPT-5.6 Terra.**

Rationale: WP14 is broad and implementation-aware, but the architecture,
schema, contracts, persistence semantics, failure vocabulary, and
execution boundaries are already established. Terra is the
cost-effective choice for systematic test construction, reconciliation,
and regression validation. Escalate to Sol only if a genuine
architectural contradiction is discovered; do not redesign merely to
satisfy a test.

## Authoritative Inputs

Before mutation, read and reconcile completely:

1.  `RELEASE_1.2_EXECUTION_PLAN.md`
2.  `RELEASE_1.2_FILE_MANIFEST.md`
3.  Accepted WP02--WP13 authority/result state
4.  `docs/architecture/data/RESEARCH_DATASET_DEFINITION.md`
5.  `docs/architecture/data/DATASET_IDENTITY_VERSION_PROVENANCE.md`
6.  Current Application dataset contracts and use cases
7.  Current Infrastructure SQLite implementation
8.  Existing Release 1.1 SQLite tests and behavior
9.  Existing WP13 Application dataset tests
10. GitHub issue #134 and milestone #53
11. Current repository/GitHub state

Repository truth takes precedence over assumptions. Do not silently
repair contradictions. Stop and report the smallest corrective authority
if an authoritative conflict prevents safe execution.

## Starting-State Gates

Before changing any test:

-   Confirm repository is
    `samuel-santos-engineer/AIQuantTradingResearch`.
-   Confirm current branch and local/remote relationship.
-   Confirm staged paths are zero.
-   Classify every existing working-tree path.
-   Preserve all accepted cumulative Release 1.2 artifacts.
-   Confirm issue #133 is **Closed / Done**.
-   Confirm issue #134 is **Open / Backlog** before execution.
-   Confirm issue #135 remains **Open / Backlog**.
-   Confirm milestone #53 remains open.
-   Confirm WP14 dependencies exactly match authoritative planning.
-   Confirm no Release 1.3 implementation has started.
-   Run the canonical baseline before mutation.

Expected permanent baseline after WP13:

-   Domain.Tests: **11**
-   Application.Tests: **60**
-   Infrastructure.Tests: **79**
-   Architecture.Tests: **13**
-   Total: **163**

If baseline truth differs, reconcile and report before proceeding.

Only after all starting gates pass may issue #134 move **Backlog → In
Progress**.

## Scope

WP14 may add or minimally refine permanent tests under:

`tests/AIQuantTradingResearch.Infrastructure.Tests/`

Prefer the smallest coherent test surface. Reuse existing test
infrastructure where appropriate.

WP14 may make a minimal production correction **only if a permanent
Infrastructure test exposes a clear defect in already-authorized
WP07--WP12 behavior and the correction is necessary to satisfy the
accepted authority**. Any such production change must be narrowly
justified and reported separately. Do not use WP14 to redesign
contracts, schema, identity semantics, orchestration, or Worker
behavior.

No new package or project reference is expected.

## Required Permanent Coverage

### 1. Schema v2 and Upgrade

Permanently prove:

-   New/empty database bootstraps to the accepted **schema version 2**.
-   Release 1.1 schema v1 upgrades deterministically to v2.
-   Existing `historical_observations` data survives v1→v2 upgrade
    unchanged.
-   Repeated bootstrap at v2 is idempotent/non-destructive.
-   Future unsupported schema version is rejected deterministically.
-   Incompatible/corrupt schema evidence is rejected rather than
    silently repaired.
-   Accepted dataset tables, keys, constraints, collations, strictness,
    and ordering representation match WP07.
-   Release 1.1 historical storage remains valid after upgrade.

Align any Release 1.1 bootstrap tests already corrected by the accepted
WP07 resume authority; do not create contradictory version-one
expectations.

### 2. Connection Lifecycle and Bootstrap Boundary

Prove:

-   Factory calls return distinct operation-owned connections.
-   Connections are open when returned as designed.
-   Caller/store disposal closes/disposes operation-owned resources.
-   DI resolution does not create or mutate the database.
-   Bootstrap occurs only at the accepted storage-operation boundary.
-   No shared live SQLite connection is captured by singleton services.

### 3. Dataset Mapper Fidelity

Permanently prove round-trip fidelity for the accepted WP07
mapper/model:

-   all four typed identities;
-   identity scheme;
-   Dataset Version = Snapshot Identity;
-   exact target text;
-   requested `[from,to)` boundaries;
-   UTC semantic instants plus original offsets;
-   exact decimal values without floating-point conversion;
-   selected observation count;
-   first/last actual instants for non-empty snapshots;
-   null actual bounds for valid empty snapshots;
-   provenance;
-   lineage;
-   deterministic observation ordinal/order.

Include high-precision decimal and non-zero-offset cases.

### 4. Snapshot Persistence --- New Evidence

Using real isolated file-backed SQLite:

-   Persist a new non-empty snapshot.
-   Verify descriptor and membership evidence physically/durably.
-   Retrieve/reconstruct exact semantic evidence.
-   Persist a valid empty snapshot.
-   Verify empty snapshot has descriptor evidence and zero membership
    rows.
-   Verify multiple immutable versions for the same logical Research
    Dataset can coexist.

### 5. Equivalent Existing Semantics

Prove:

-   Re-persisting semantically equivalent evidence returns the accepted
    equivalent outcome.
-   Equivalent persistence performs no destructive mutation.
-   Equivalent evidence remains retrievable with exact fidelity.
-   Equivalent behavior is deterministic across separate store
    instances/connections.

### 6. Integrity Conflict and Immutability

Prove contradictory evidence under the same Snapshot Identity:

-   returns `IntegrityConflict`;
-   does not overwrite accepted descriptor evidence;
-   does not overwrite accepted membership evidence;
-   leaves previously accepted snapshot retrievable unchanged.

Exercise meaningful contradictions such as selected
price/offset/membership or descriptor inconsistency where safely
representable.

### 7. Atomicity / Rollback

Permanently prove transaction atomicity.

Create a controlled failure after partial write opportunity and verify:

-   descriptor + membership are all committed or all rolled back;
-   no partial accepted snapshot remains;
-   existing accepted history remains unchanged;
-   no repair/upsert behavior occurs.

Do not weaken production constraints merely to make this test
convenient.

### 8. Catalog Registration

Permanently prove `IDatasetCatalog`:

-   new registration → `NewlyRegistered`;
-   equivalent registration → `EquivalentExisting`;
-   contradictory same-identity registration → `IntegrityConflict`;
-   registration preserves immutable snapshot evidence;
-   registration does not introduce a second persistence model.

### 9. Exact Catalog Lookup

Prove:

-   exact Snapshot Identity hit returns the correct
    `DatasetCatalogEntry`;
-   missing identity returns `NotFound`;
-   lookup of one version never returns another version;
-   reconstructed entry preserves identities/version, target,
    boundaries, coverage, provenance, lineage, empty-state semantics,
    offsets, and decimals.

No "latest", fuzzy, range, search, or listing semantics may be
introduced.

### 10. WP11 Failure Mapping

Permanently prove bounded Infrastructure classification for dataset
storage:

-   known SQLite unavailable conditions →
    `DatasetStoreFailure.Unavailable`;
-   malformed/corrupt persisted dataset evidence →
    `DatasetStoreFailure.InvalidData`;
-   incompatible/malformed schema evidence is contained at the
    Infrastructure boundary as authorized;
-   constraint/type/data corruption classified by WP11 does not leak
    SQLite types to Application;
-   unknown/unclassified SQLite failures are not falsely normalized;
-   unrelated unknown exceptions are not swallowed.

Do not introduce `catch (Exception)` normalization.

### 11. DI / Configuration Composition

At the Infrastructure-owned boundary prove:

-   exactly one `IDatasetSnapshotStore` registration resolves to the
    accepted SQLite implementation;
-   exactly one `IDatasetCatalog` registration resolves to the accepted
    SQLite implementation;
-   existing `IHistoricalObservationStore` registration remains valid;
-   existing `ISqliteConnectionFactory` ownership/lifetime remains
    valid;
-   configured `Persistence:DatabasePath` is handed through exactly;
-   graph construction/resolution creates no database file;
-   existing Release 1.0/Twelve Data registrations remain intact without
    provider calls.

WP14 does **not** own Worker execution/configuration tests; those remain
outside this Infrastructure test boundary.

### 12. Release 1.1 Regression

The final permanent suite must continue proving Release 1.1 behavior:

-   schema/bootstrap compatibility;
-   historical observation persistence;
-   retrieval;
-   idempotency;
-   conflicts;
-   atomicity;
-   target isolation;
-   ordering;
-   timestamp/offset/decimal fidelity;
-   failure mapping;
-   DI/configuration.

Do not delete or weaken Release 1.1 tests to make Release 1.2 pass.

## Test Isolation Requirements

All WP14 tests must be:

-   offline;
-   deterministic;
-   provider/network independent;
-   credential independent;
-   isolated from user databases;
-   file-backed where durable SQLite behavior is being proven;
-   unique per test/scenario where necessary;
-   fully cleaned after execution.

Use repository-appropriate temporary-directory/database helpers. Clear
SQLite pools when necessary before deletion.

At completion, verify zero residue for temporary database, WAL, SHM, and
journal files.

## Explicit Non-Goals

WP14 must not:

-   change Domain behavior;
-   redesign Application contracts;
-   redesign identity/canonicalization;
-   redesign schema v2;
-   introduce schema v3;
-   add migrations beyond accepted v1→v2 behavior;
-   redesign WP08 snapshot persistence;
-   redesign WP09 catalog semantics;
-   redesign WP10 orchestration;
-   redesign WP11 failure vocabulary/classification;
-   change WP12 Worker execution;
-   add scheduling, streaming, refresh, DAG, retry policy, monitoring,
    or Release 1.3 behavior;
-   add provider/network integration tests;
-   add live Twelve Data tests;
-   stage, commit, push, create branches, create PRs, merge, tag, or
    release.

## Validation Gates

After implementation run, at minimum:

1.  Targeted Infrastructure tests.
2.  Domain tests.
3.  Application tests.
4.  Full Infrastructure tests.
5.  Architecture tests.
6.  Restore.
7.  Format verification.
8.  Build with zero warnings/errors.
9.  Canonical `eng/verify.ps1 -Configuration Release`.
10. Gitleaks through the canonical verification path.
11. `git diff --check`.
12. `git diff --cached --check`.
13. Direct whitespace checks for untracked WP14 files where normal Git
    diff does not cover them.
14. Architecture/dependency reconciliation.
15. Temporary SQLite residue scan.

Expected permanent total is **at least 163** and should increase only by
justified WP14 Infrastructure tests. Report exact before/after counts.

## Acceptance Matrix

WP14 is complete only if evidence supports all applicable rows:

-   schema v2 bootstrap: PASS
-   v1→v2 upgrade: PASS
-   Release 1.1 data preservation across upgrade: PASS
-   unsupported future version rejection: PASS
-   schema validation/non-destructive behavior: PASS
-   connection lifecycle: PASS
-   mapper fidelity: PASS
-   non-empty snapshot persistence: PASS
-   empty snapshot persistence: PASS
-   equivalent existing behavior: PASS
-   integrity conflict/non-overwrite: PASS
-   atomic rollback: PASS
-   multiple versions coexist: PASS
-   catalog new/equivalent/conflict registration: PASS
-   exact catalog hit/miss: PASS
-   exact version lookup isolation: PASS
-   identity/version fidelity: PASS
-   target/boundary/coverage fidelity: PASS
-   provenance/lineage fidelity: PASS
-   timestamp/offset fidelity: PASS
-   decimal fidelity: PASS
-   unavailable failure mapping: PASS
-   invalid-data failure mapping: PASS
-   unknown failure propagation: PASS
-   Infrastructure DI/configuration: PASS
-   resolution-time database mutation: NO
-   provider/network calls: 0
-   temporary SQLite residue: 0
-   Release 1.1 persistence regression: PASS
-   production architecture regression: PASS
-   package/reference delta: 0/0 unless separately justified by
    authority
-   WP15 started: NO
-   Release 1.3 implementation started: NO

## GitHub Lifecycle

When and only when all WP14 acceptance gates pass:

1.  Post concise completion evidence to issue #134.
2.  Close issue #134.
3.  Set its Project #2 status to Done.
4.  Verify issue #135 remains Open / Backlog.
5.  Leave milestone #53 open.

Do not mutate later WP issues except to inspect their state.

If a blocker remains, leave #134 open/in progress and report the blocker
precisely.

## Required Execution Report

Return a structured report covering:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Predecessor/Lifecycle Gates
7.  Issue Lifecycle
8.  Initial Baseline
9.  Existing Infrastructure Test Inventory
10. WP07 Schema/Upgrade Reconciliation
11. WP08 Snapshot-Persistence Reconciliation
12. WP09 Catalog Reconciliation
13. WP11 Failure-Mapping Reconciliation
14. WP12 Composition Reconciliation
15. Test Isolation Strategy
16. Schema v2 Coverage
17. v1→v2 Upgrade Coverage
18. Connection Lifecycle Coverage
19. Mapper Fidelity Coverage
20. Snapshot Persistence Coverage
21. Empty Snapshot Coverage
22. Equivalent Existing Coverage
23. Integrity Conflict / Immutability Coverage
24. Atomic Rollback Coverage
25. Multiple-Version Coverage
26. Catalog Registration Coverage
27. Exact Lookup Coverage
28. Failure-Mapping Coverage
29. DI/Configuration Coverage
30. Release 1.1 Regression Protection
31. Exact Files Added/Modified
32. Production Code Delta
33. Package/Reference Delta
34. Permanent Test Count Delta
35. Targeted Infrastructure Evidence
36. Full Permanent Test Evidence
37. Canonical Verification
38. Architecture Validation
39. Security/Offline Determinism
40. Database Cleanup Evidence
41. Whitespace/Diff Evidence
42. Mutation Accounting
43. Git/GitHub Protection
44. Planning Protection
45. Findings/Blockers
46. Acceptance Matrix
47. Final Repository/GitHub State
48. WP15 Handoff
49. Final Decision
50. Next Authorized Work Package

End a successful execution exactly with:

`RELEASE 1.2 WP14 COMPLETE`

and:

`NEXT AUTHORIZED WORK PACKAGE: WP15 — Architecture & Documentation Alignment — GitHub issue #135`

If blocked, end with:

`RELEASE 1.2 WP14 BLOCKED`

and do not authorize WP15.
