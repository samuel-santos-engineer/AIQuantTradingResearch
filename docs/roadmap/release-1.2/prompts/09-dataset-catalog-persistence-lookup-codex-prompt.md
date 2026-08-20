# Release 1.2 WP09 --- Dataset Catalog Persistence & Lookup --- Codex Execution Authority

## 1. Authority

You are authorized to execute **Release 1.2 WP09 --- Dataset Catalog
Persistence & Lookup** for:

-   Repository: `samuel-santos-engineer/AIQuantTradingResearch`
-   GitHub issue: `#129`
-   Milestone:
    `#53 — Phase 3 - Release 1.2: Research Dataset Foundation`
-   Project: `#2 — AIQuantTradingResearch Engineering Roadmap`

This prompt is the execution authority for WP09. It must be interpreted
together with:

-   `RELEASE_1.2_EXECUTION_PLAN.md`
-   `RELEASE_1.2_FILE_MANIFEST.md`
-   accepted WP01--WP08 artifacts and execution evidence
-   `docs/architecture/data/RESEARCH_DATASET_DEFINITION.md`
-   `docs/architecture/data/DATASET_IDENTITY_VERSION_PROVENANCE.md`
-   current Application dataset contracts
-   current Release 1.1 SQLite persistence foundation
-   accepted WP07 SQLite schema-v2 physical model
-   accepted WP08 dataset snapshot persistence implementation

If any authority conflicts materially, stop before mutation and report
the conflict and the smallest corrective authority required.

## 2. Objective

Implement the **minimum bounded Infrastructure persistence required for
`IDatasetCatalog` registration and exact lookup** using the accepted
Release 1.2 Application catalog model and WP07 SQLite schema v2.

WP09 must make accepted dataset snapshot evidence discoverable through
the existing Application-owned catalog abstraction without redefining
dataset semantics, identities, snapshot persistence, or materialization.

The implementation must preserve:

-   immutable catalog evidence;
-   exact Snapshot Identity lookup;
-   deterministic reconstruction of `DatasetCatalogEntry`;
-   `NewlyRegistered`, `EquivalentExisting`, and `IntegrityConflict`
    semantics;
-   Dataset Version = Snapshot Identity semantics;
-   exact target and requested-boundary fidelity;
-   coverage fidelity;
-   provenance and lineage fidelity;
-   successful empty-snapshot representation;
-   timestamp/offset/decimal fidelity;
-   coexistence of multiple immutable versions of one logical Research
    Dataset;
-   storage independence of Domain and Application.

## 3. Required Starting-State Gates

Before changing anything, verify and report:

1.  Repository identity is exactly
    `samuel-santos-engineer/AIQuantTradingResearch`.
2.  Current branch is `main`.
3.  Local `main` is synchronized with `origin/main`; report HEAD, remote
    SHA, and ahead/behind.
4.  Staged paths are zero.
5.  Every existing working-tree path is classified as an accepted
    cumulative Release 1.2 artifact or an explicitly authorized WP09
    artifact.
6.  Unexpected or ambiguous paths are zero.
7.  Release 1.1 remains closed and accepted.
8.  Release 1.2 milestone #53 is open.
9.  WP08 issue #128 is `Closed / Done`.
10. WP09 issue #129 is `Open / Backlog`.
11. WP10 issue #130 is `Open / Backlog`.
12. WP09 dependency state matches the authoritative planning graph.
13. No Release 1.3 implementation has started.
14. Restore, format verification, build, permanent tests, architecture
    tests, canonical verification, and Git whitespace checks pass before
    mutation.

Do **not** move #129 to In Progress until all starting-state gates pass.

If a starting-state gate fails, stop without repository or GitHub
mutation and emit `RELEASE 1.2 WP09 BLOCKED`.

## 4. Mandatory Reconciliation Before Implementation

Reconcile the current repository against WP02--WP08 and explicitly
report the result.

### WP02 --- Dataset semantics

Preserve:

-   one exact target;
-   explicit `[from, to)` boundaries;
-   deterministic semantic-instant ordering;
-   valid successful empty materialization;
-   immutable snapshots;
-   exact offset and decimal fidelity.

### WP03 --- Identity/version/provenance

Preserve:

-   Dataset Definition Identity;
-   Research Dataset Identity;
-   Source State Identity;
-   Dataset Snapshot Identity;
-   Dataset Version as the immutable Snapshot Identity;
-   `aiq-dataset-identity-v1`;
-   provenance and narrow lineage;
-   identity immutability and collision/integrity-conflict semantics.

Do not redesign canonicalization or recompute semantic identities in
Infrastructure.

### WP04 --- Application contracts

Implement the existing `IDatasetCatalog` seam as currently accepted.

Do not add SQLite, SQL, filesystem, or provider mechanics to
Application.

Do not create a competing catalog abstraction.

### WP05 --- Materialization

Materialization remains Application-owned and independent of catalog
persistence.

WP09 must not invoke historical acquisition or rematerialize a dataset
to satisfy lookup.

### WP06 --- Catalog model

`DatasetCatalogEntry` is the authoritative logical catalog descriptor.

Preserve the distinction between:

-   identity-bearing semantic metadata;
-   deterministic descriptive metadata;
-   operational metadata, which is excluded;
-   physical-storage metadata, which is excluded from the Application
    model.

Do not add mutable `latest`, status, timestamp, path, database ID, or
similar operational concepts.

### WP07 --- Physical storage

Reuse schema v2 exactly as accepted.

Do not introduce schema v3, migrations, replacement tables, generic
repository infrastructure, ORM behavior, or a second physical catalog
model unless an unavoidable contradiction is discovered. If such a
contradiction exists, stop and report it.

### WP08 --- Snapshot persistence

`SqliteDatasetSnapshotStore` owns snapshot persistence semantics.

WP09 must not redesign or duplicate snapshot write behavior.

Catalog registration may rely on already persisted accepted snapshot
evidence according to the existing contract/schema design, but must not
overwrite or mutate contradictory accepted evidence.

## 5. Implementation Scope

Implement only the minimum Infrastructure behavior needed to satisfy
`IDatasetCatalog`.

Prefer extending the accepted SQLite persistence organization and
existing connection boundary.

Expected bounded behavior:

### Registration

For an accepted `DatasetCatalogEntry`:

-   register new immutable catalog evidence and return
    `NewlyRegistered`;
-   recognize semantically equivalent existing evidence and return
    `EquivalentExisting`;
-   detect contradictory evidence under the same exact Snapshot Identity
    and return `IntegrityConflict`;
-   never overwrite, replace, repair, delete, or silently alias existing
    evidence.

Registration must preserve the accepted snapshot/catalog relationship
established by WP07/WP08.

### Exact lookup

Lookup is by exact `DatasetSnapshotIdentity`.

A successful hit must reconstruct the accepted `DatasetCatalogEntry`
with all required semantic/descriptive facts.

A valid miss must be represented according to the existing Application
contract. Do not invent a mutable latest/search/listing API.

Lookup must not:

-   normalize identity text;
-   perform prefix/fuzzy/case-insensitive matching;
-   search by target;
-   return an arbitrary version;
-   materialize missing data;
-   call a provider.

### Multiple versions

Distinct Snapshot Identities for the same Research Dataset Identity must
coexist without replacement.

Exact lookup must return only the requested immutable version.

## 6. Fidelity Requirements

Registration and lookup must preserve exactly the accepted
representation semantics for:

-   all four typed dataset identities;
-   Dataset Version;
-   identity scheme;
-   target text;
-   requested boundary instants and original offsets where represented;
-   selected observation count;
-   first/last actual observation instant semantics;
-   empty-snapshot state;
-   provenance;
-   lineage;
-   source authority;
-   timestamp semantic instant and original offset;
-   exact decimal values.

Use the accepted WP07 records/mappers and WP08 persisted evidence where
applicable.

Do not introduce floating-point conversion, culture-sensitive
parsing/formatting, local-time conversion, truncation, or rounding.

## 7. SQL and Persistence Rules

Any SQL introduced by WP09 must be:

-   minimal;
-   explicit;
-   parameterized;
-   deterministic;
-   scoped to schema v2;
-   compatible with immutable evidence.

Use explicit ordering whenever reconstruction depends on ordered rows.
Never depend on SQLite natural row order.

Do not add:

-   destructive upserts;
-   `INSERT OR REPLACE`;
-   update/delete repair paths;
-   generic data-access frameworks;
-   caching;
-   background refresh;
-   retries;
-   resilience policies.

## 8. Connection and Transaction Boundary

Reuse the accepted `ISqliteConnectionFactory`.

Every operation must own and deterministically dispose its connection,
commands, readers, and transaction if one is required.

Do not register or retain live SQLite connections.

Use a transaction only where needed to preserve atomic catalog
registration semantics. Explain the decision.

## 9. Failure Boundary

Do not perform WP11's generalized Dataset Validation & Failure Mapping
work.

WP09 may use only failure behavior already required by the existing
`IDatasetCatalog` contract and accepted SQLite boundaries.

Do not:

-   invent new public failure categories;
-   redesign WP10/WP11 failure semantics;
-   add retryability classifications;
-   catch arbitrary `Exception`;
-   hide programming defects.

If a failure cannot be represented without prematurely deciding WP11
policy, preserve the existing boundary and document the WP11 handoff.

## 10. Permanent Tests and Temporary Validation

Permanent test expansion belongs to WP13/WP14 unless the Release 1.2
manifest explicitly says otherwise.

For WP09:

-   permanent test delta should remain zero unless the manifest
    explicitly authorizes otherwise;
-   temporary focused offline probes are allowed;
-   probes must be removed before completion;
-   temporary SQLite databases/WAL/SHM/journal files must be removed.

At minimum validate, using temporary focused offline evidence if
permanent coverage does not yet exist:

1.  new catalog registration;
2.  exact lookup of a non-empty entry;
3.  equivalent repeated registration;
4.  contradictory same-Snapshot-Identity registration is
    non-destructive;
5.  valid empty snapshot registration and lookup;
6.  multiple versions for one Research Dataset Identity coexist;
7.  exact lookup returns only the requested version;
8.  identity/version fidelity;
9.  target/boundary/coverage fidelity;
10. provenance/lineage fidelity;
11. timestamp/offset/decimal fidelity where reconstructed from persisted
    snapshot evidence;
12. no provider/network calls;
13. no temporary database residue.

## 11. Explicitly Prohibited Scope

WP09 must **not** start or implement:

-   WP10 --- Dataset Materialization Integration;
-   WP11 --- Dataset Validation & Failure Mapping;
-   WP12 --- Dependency Registration & Bounded Dataset Execution;
-   WP13/WP14 permanent test expansion beyond manifest authority;
-   WP15 documentation alignment;
-   WP16 integration/acceptance;
-   Release 1.3 pipelines, scheduling, streaming, refresh, DAGs,
    monitoring, or resilience.

Also prohibited:

-   Domain changes unless an unavoidable authority contradiction is
    found;
-   Application contract redesign unless required to resolve a proven
    contradiction;
-   Worker changes;
-   package additions;
-   project-reference additions;
-   solution/build-script changes;
-   schema version changes;
-   Git staging/commit/push/branch/PR/merge/tag/release actions.

## 12. Architecture Protection

The production dependency graph must remain:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

Required leakage counts after WP09:

-   Domain SQLite/SQL/filesystem references: 0
-   Application SQLite/SQL/filesystem references: 0
-   Domain provider/HTTP mechanics: 0
-   Application provider/HTTP mechanics: 0

No new production dependency cycle is permitted.

## 13. Security and Offline Constraints

Execution and validation must be offline except for required GitHub
planning-state inspection/mutation.

Do not use:

-   real provider calls;
-   live market-data acquisition;
-   real API keys;
-   committed credentials;
-   personal database paths;
-   sensitive connection strings.

Run the repository's canonical secret scan.

## 14. GitHub Issue Lifecycle

After all starting-state gates and the initial baseline pass:

1.  move issue #129 from Backlog to In Progress;
2.  perform WP09;
3.  run all final gates;
4.  post concise completion evidence to #129;
5.  close #129 and set Project status to Done only if every WP09
    acceptance gate passes.

Issue #130 must remain `Open / Backlog`.

Do not mutate unrelated issues, milestone fields, Project
schema/options, labels, dependencies, priorities, releases, areas, or
assignees.

## 15. Required Final Validation

Before declaring completion, run and report:

-   restore;
-   format verification;
-   build;
-   Domain.Tests;
-   Application.Tests;
-   Infrastructure.Tests;
-   Architecture.Tests;
-   complete permanent-test total;
-   `eng/verify.ps1`;
-   canonical secret scan result;
-   `git diff --check`;
-   `git diff --cached --check`;
-   direct whitespace validation for relevant untracked WP09 files if
    necessary;
-   architecture dependency/leakage validation;
-   Release 1.1 persistence regression;
-   Release 1.2 snapshot-persistence regression;
-   temporary SQLite residue check;
-   package/reference delta;
-   working-tree classification.

Expected baseline before permanent-test work remains:

-   Domain.Tests: 11
-   Application.Tests: 42
-   Infrastructure.Tests: 79
-   Architecture.Tests: 13
-   Total: 145

If repository truth has legitimately changed under accepted authority,
report the actual counts and reconcile them rather than forcing these
numbers.

## 16. WP09 Acceptance Matrix

WP09 is complete only if all applicable rows pass:

-   WP08 predecessor accepted;
-   issue #129 lifecycle valid;
-   existing `IDatasetCatalog` implemented;
-   `DatasetCatalogEntry` reused;
-   schema v2 reused unchanged;
-   WP08 snapshot persistence semantics preserved;
-   new immutable catalog registration works;
-   equivalent existing registration works;
-   same-identity contradictory evidence returns integrity conflict;
-   conflict is non-destructive;
-   exact Snapshot Identity lookup works;
-   valid lookup miss follows existing contract;
-   multiple immutable versions coexist;
-   no mutable latest semantics;
-   identity/version fidelity passes;
-   target/boundary/coverage fidelity passes;
-   provenance/lineage fidelity passes;
-   valid empty snapshot works;
-   timestamp/offset/decimal fidelity passes where applicable;
-   deterministic SQL/ordering passes;
-   operation-owned connections preserved;
-   no destructive update/delete/replace behavior;
-   provider/network calls = 0;
-   Domain/Application SQLite leakage = 0;
-   production graph unchanged;
-   permanent-test delta = 0 unless manifest-authorized;
-   package/reference delta = 0/0;
-   temporary residue = 0;
-   WP10 not started;
-   Release 1.3 not started;
-   all canonical validation passes.

## 17. Required Execution Report

Return a numbered execution report covering at least:

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
15. WP08 Snapshot-Persistence Reconciliation
16. Catalog Persistence Design
17. Registration Algorithm
18. New Registration Behavior
19. Equivalent Existing Behavior
20. Integrity Conflict Behavior
21. Exact Lookup Behavior
22. Lookup-Miss Behavior
23. Multiple-Version Coexistence
24. Identity / Version Fidelity
25. Target / Boundary / Coverage Fidelity
26. Provenance / Lineage Fidelity
27. Empty Snapshot Semantics
28. Timestamp / Offset / Decimal Fidelity
29. SQL / Ordering Strategy
30. Transaction Strategy
31. Connection Ownership / Disposal
32. Failure-Boundary Decision
33. Exact Files Added/Modified
34. Domain Delta
35. Application Delta
36. Infrastructure Delta
37. Worker Delta
38. Package/Reference Delta
39. Permanent Test Delta
40. Temporary Probe Evidence
41. WP10/WP11/WP12 Protection
42. Release 1.3 Protection
43. Security / Offline Evidence
44. Whitespace / Diff Evidence
45. Restore / Build Evidence
46. Permanent Test Evidence
47. Canonical Verification
48. Architecture Validation
49. Release 1.1 Persistence Regression
50. WP08 Snapshot-Persistence Regression
51. Catalog Validation Matrix
52. Mutation Accounting
53. Git / GitHub Protection
54. Planning Protection
55. Findings / Blockers
56. Final Repository / GitHub State
57. WP10 Handoff
58. Final Decision
59. Next Authorized Work Package

## 18. Terminal Markers

On success, end exactly with:

`RELEASE 1.2 WP09 COMPLETE`

Then include:

`NEXT AUTHORIZED WORK PACKAGE: WP10 — Dataset Materialization Integration — GitHub issue #130`

On a blocking authority/state contradiction, end with:

`RELEASE 1.2 WP09 BLOCKED`

and identify the smallest corrective authority required.

## 19. Execution Principle

Prefer the **smallest implementation that makes the accepted Application
catalog contract real over the accepted SQLite v2 model**.

Do not redesign what WP02--WP08 have already decided.

Correctness, immutability, deterministic reconstruction, exact identity
lookup, and strict work-package boundaries take precedence over
abstraction or convenience.
