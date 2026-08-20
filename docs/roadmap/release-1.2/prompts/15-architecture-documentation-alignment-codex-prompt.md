# Release 1.2 WP15 --- Architecture & Documentation Alignment --- Codex Execution Authority

## Purpose

Execute **Release 1.2 WP15 --- Architecture & Documentation Alignment**
for:

`samuel-santos-engineer/AIQuantTradingResearch`

WP15 reconciles the accepted Release 1.2 implementation through WP14
with the repository's authoritative current-state architecture and
engineering documentation.

This is a **documentation and architecture-alignment work package**. It
must describe what the repository actually implements, preserve
architectural boundaries, distinguish implemented behavior from future
plans, and introduce no new product behavior.

## Recommended Model

**GPT-5.6 Terra**

WP15 is reconciliation-heavy rather than invention-heavy. The
implementation, schema, contracts, persistence behavior, execution
model, and permanent proof already exist. Terra is the cost-effective
model for systematic repository/documentation reconciliation.

Do not escalate to Sol merely for documentation breadth. If a genuine
architecture contradiction is discovered that cannot be resolved without
changing accepted production behavior, stop and report the smallest
corrective authority instead of redesigning the system.

## Authoritative Inputs

Before any mutation, read and reconcile completely:

1.  `RELEASE_1.2_EXECUTION_PLAN.md`
2.  `RELEASE_1.2_FILE_MANIFEST.md`
3.  Accepted WP02--WP14 authority/result state
4.  `docs/architecture/data/RESEARCH_DATASET_DEFINITION.md`
5.  `docs/architecture/data/DATASET_IDENTITY_VERSION_PROVENANCE.md`
6.  Current Release 1.1 persistence architecture documentation
7.  Current solution/design/implementation/testing documentation
    relevant to the implemented Release 1.2 surface
8.  Current README/navigation where Release 1.2 current-state claims are
    exposed
9.  Current Domain/Application/Infrastructure/Worker source
10. Current permanent tests, including WP13 and WP14
11. GitHub issue #135, milestone #53, and Project #2 state
12. Current repository/Git state

Repository implementation and accepted Release 1.2 semantics are the
source of truth for current-state documentation.

Do not silently reinterpret accepted WP02/WP03 semantics.

## Starting-State Gates

Before modifying documentation:

-   Confirm repository identity.
-   Confirm current branch and local/remote relationship.
-   Confirm staged paths = 0.
-   Classify every existing working-tree path.
-   Preserve accepted cumulative Release 1.2 artifacts.
-   Confirm issue #134 is **Closed / Done**.
-   Confirm issue #135 is **Open / Backlog**.
-   Confirm issue #136 remains **Open / Backlog**.
-   Confirm milestone #53 remains open.
-   Confirm WP15 dependencies match authoritative planning.
-   Confirm no Release 1.3 implementation is active.
-   Run the canonical baseline before mutation.

Expected permanent test baseline:

-   Domain.Tests: **11**
-   Application.Tests: **60**
-   Infrastructure.Tests: **87**
-   Architecture.Tests: **13**
-   Total: **171**

If repository truth differs, reconcile before proceeding.

Only after all starting gates pass may #135 move **Backlog → In
Progress**.

## Core Alignment Objective

Documentation must accurately represent the accepted Release 1.2
architecture now implemented through WP14:

**Release 1.2 --- Research Dataset Foundation**

The repository now supports deterministic, immutable, versioned research
datasets materialized from accepted Release 1.1 historical observations,
with semantic identity, reproducibility, provenance, lineage,
SQLite-backed immutable snapshot/catalog evidence, bounded execution,
validation/failure mapping, and permanent Application/Infrastructure
proof.

Do not describe Release 1.3 pipeline capabilities as implemented.

## Required Architecture Reconciliation

### 1. Production Dependency Graph

Verify and document the actual graph:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

Confirm:

-   cycles = 0;
-   Domain remains provider/storage independent;
-   Application remains provider/storage independent;
-   SQLite remains Infrastructure-owned;
-   provider/HTTP mechanics remain Infrastructure-owned;
-   Worker remains composition/execution host.

Do not alter production references in WP15.

### 2. Release 1.1 Foundation Relationship

Document clearly that Release 1.2 **builds on**, rather than replaces,
Release 1.1.

Release 1.1 remains responsible for accepted historical market
observations and their SQLite persistence/retrieval foundation.

Release 1.2 uses those accepted observations as dataset source truth.

Preserve Release 1.1 guarantees including:

-   exact target identity;
-   immutable/idempotent historical observations;
-   deterministic conflict behavior;
-   chronological retrieval;
-   timestamp/offset fidelity;
-   decimal fidelity;
-   explicit persistence configuration;
-   Infrastructure-owned SQLite.

### 3. Dataset Definition and Reproducibility

Align documentation with WP02:

-   Research Dataset;
-   Dataset Definition;
-   Materialization;
-   Snapshot;
-   exact single target;
-   explicit `[from,to)` selection;
-   deterministic ascending semantic-instant ordering;
-   successful empty materialization;
-   exact offset/decimal fidelity;
-   reproducibility based on definition + relevant source state;
-   immutable prior snapshots when source history changes.

### 4. Identity, Version, Provenance, and Lineage

Align with WP03 and implementation:

Distinct concepts:

-   Dataset Definition Identity
-   Research Dataset Identity
-   Source State Identity
-   Dataset Snapshot Identity
-   Dataset Version

Document:

-   Dataset Version is the immutable Snapshot Identity representation;
-   identity scheme `aiq-dataset-identity-v1`;
-   SHA-256 / 64 lowercase hexadecimal representation;
-   deterministic canonical semantic representation;
-   equivalent rematerialization semantics;
-   relevant source-state changes create distinguishable snapshot
    identities;
-   operational metadata is excluded from semantic identity;
-   identities are immutable and cannot be reassigned;
-   provenance explains definition/source/identity facts;
-   lineage connects snapshot, definition, source state, and selected
    observations.

Avoid exposing low-level implementation detail where architecture
documentation should remain semantic.

### 5. Application Dataset Contracts

Document the accepted Application-owned dataset seam:

-   `DatasetDefinition`
-   typed identities and `DatasetVersion`
-   `DatasetSnapshotCandidate`
-   coverage
-   provenance
-   lineage
-   materialization result/failure vocabulary
-   `IMaterializeDatasetUseCase`
-   `IDatasetSnapshotStore`
-   `IDatasetCatalog`
-   bounded materialization integration seam

Make clear that Application contracts contain no SQLite, SQL,
filesystem, provider, or HTTP mechanics.

### 6. Deterministic Materialization

Align with WP05:

-   exact target-scoped source history;
-   `[from,to)` selection;
-   deterministic ordering;
-   duplicate semantic-instant rejection;
-   valid empty result;
-   preservation of original offsets and decimal values;
-   deterministic identity computation;
-   coverage/provenance/lineage construction;
-   no persistence/catalog side effect inside the pure materialization
    use case.

### 7. Catalog Metadata Model

Align with WP06:

-   immutable catalog evidence;
-   identity-bearing versus deterministic descriptive metadata;
-   exact snapshot lookup;
-   Research Dataset identity distinct from Snapshot identity/version;
-   multiple immutable versions may coexist;
-   no mutable "latest" semantics;
-   no operational metadata in semantic identity;
-   no physical-storage detail in Application catalog contracts.

### 8. SQLite Physical Dataset Model

Align with accepted WP07 state:

-   SQLite schema version **2**;
-   explicit v1→v2 evolution;
-   preservation of Release 1.1 `historical_observations`;
-   dataset snapshot descriptor/catalog evidence;
-   ordered snapshot observation membership;
-   exact target representation;
-   UTC ticks + original offset representation;
-   invariant exact decimal text;
-   explicit empty-snapshot representation without sentinel
    observations;
-   accepted constraints/keys/indexes/ordering strategy;
-   non-destructive schema validation.

Do not invent schema v3 or future migration policy beyond current
accepted design.

### 9. Snapshot Persistence

Align with WP08:

-   immutable insert-only snapshot evidence;
-   descriptor + membership atomicity;
-   `NewlyAccepted`;
-   equivalent existing evidence;
-   `IntegrityConflict`;
-   no overwrite/repair/delete semantics;
-   empty snapshot persistence;
-   multiple versions coexist;
-   operation-owned connections;
-   exact reconstruction fidelity.

### 10. Catalog Persistence and Lookup

Align with WP09:

-   `NewlyRegistered`;
-   `EquivalentExisting`;
-   `IntegrityConflict`;
-   exact typed Snapshot Identity lookup;
-   `NotFound`;
-   catalog registration reuses accepted immutable snapshot evidence;
-   no second persistence model;
-   no latest/search/listing semantics.

### 11. Materialization Integration

Align with WP10:

Describe the bounded Application orchestration joining:

1.  materialization;
2.  snapshot persistence;
3.  catalog registration.

Preserve fail-stop semantics.

Document outcome behavior at an architectural level without creating new
vocabulary.

Do not describe distributed transactions, eventual consistency, retries,
or pipeline orchestration that do not exist.

### 12. Validation and Failure Mapping

Align with WP11:

-   Application constructors enforce semantic invariants;
-   Infrastructure validates physical schema and reconstructed evidence;
-   `DatasetStoreFailure.Unavailable`;
-   `DatasetStoreFailure.InvalidData`;
-   integrity conflict remains distinct;
-   catalog miss remains `NotFound`;
-   unknown/unclassified failures are not silently normalized;
-   SQLite-specific mechanics do not leak into Domain/Application;
-   no catch-all exception normalization.

### 13. Dependency Registration and Bounded Worker Execution

Align with WP12:

-   Application dataset use cases registered through existing
    composition conventions;
-   Infrastructure snapshot store/catalog registrations;
-   existing SQLite configuration/factory ownership preserved;
-   `Persistence:DatabasePath`;
-   `Dataset:Target`;
-   `Dataset:From`;
-   `Dataset:To`;
-   deterministic configuration rejection;
-   one bounded Worker dataset execution;
-   first execution may accept new evidence;
-   equivalent rerun recognizes existing evidence;
-   no scheduling, looping, refresh, pipeline, retry, or streaming
    behavior.

Do not imply that the Worker is a Release 1.3 pipeline engine.

### 14. Testing Strategy and Current Evidence

Update current-state testing documentation where authorized to reflect:

-   Domain.Tests: 11
-   Application.Tests: 60
-   Infrastructure.Tests: 87
-   Architecture.Tests: 13
-   Permanent total: 171

Describe responsibility boundaries:

-   Domain: Domain-owned invariants;
-   Application: dataset contracts, deterministic materialization,
    identities, catalog semantics, orchestration/failure propagation;
-   Infrastructure: schema v2, v1→v2 upgrade, SQLite fidelity, snapshot
    persistence, catalog lookup, atomicity, failure containment, DI
    boundary;
-   Architecture: dependency/ownership rules.

Do not turn transient probes from earlier WPs into claims of permanent
tests.

### 15. Implemented vs Planned

Audit all touched documentation for wording that can confuse:

-   implemented/current;
-   accepted architecture;
-   future/planned.

Release 1.2 implemented scope may be described as current only where
repository evidence supports it.

Release 1.3 remains future work, including:

-   continuous/periodic pipelines;
-   scheduling;
-   automatic refresh;
-   streaming;
-   DAG orchestration;
-   retry/resilience orchestration;
-   monitoring of dataset pipelines;
-   production pipeline lifecycle.

Do not accidentally promote roadmap intent to current capability.

## Documentation Scope

Use `RELEASE_1.2_FILE_MANIFEST.md` as the strict authority for files
WP15 may modify.

Do not modify unrelated documentation merely because it could be
improved.

Prefer targeted current-state corrections over broad rewrites.

Preserve repository terminology, document organization, tone, and
navigation conventions.

Where an existing historical decision/assessment document describes an
earlier state, preserve its historical nature and add only the minimum
clarification needed to prevent it from being mistaken for current
state.

## Architecture Tests

Inspect existing Architecture.Tests against the Release 1.2
implementation.

Default expectation: **architecture-test delta = 0**.

Do not add architecture tests merely to increase counts.

If an accepted Release 1.2 architectural rule is both:

1.  important and mechanically enforceable, and
2.  currently missing from executable architecture protection,

report the gap first. Only modify Architecture.Tests if the execution
plan/file manifest explicitly authorizes it for WP15.

Never weaken an architecture rule to make the current implementation
pass.

## Production/Test Mutation Protection

Expected WP15 deltas:

-   Domain production: 0
-   Application production: 0
-   Infrastructure production: 0
-   Worker production: 0
-   Permanent functional tests: 0
-   Packages: 0
-   Project references: 0
-   Solution/build/scripts: 0

If documentation cannot be made truthful without changing accepted
production behavior, stop and report a blocker.

## Validation Gates

After documentation alignment, run:

1.  repository-relative Markdown link validation for touched/current
    navigation;
2.  restore;
3.  format verification;
4.  build;
5.  Domain.Tests;
6.  Application.Tests;
7.  Infrastructure.Tests;
8.  Architecture.Tests;
9.  canonical `eng/verify.ps1 -Configuration Release`;
10. Gitleaks through canonical verification;
11. `git diff --check`;
12. `git diff --cached --check`;
13. direct whitespace checks for untracked WP15 documentation where
    necessary;
14. production dependency graph reconciliation;
15. documentation contradiction audit;
16. implemented-vs-planned terminology audit;
17. temporary database/residue check where canonical validation creates
    any transient state.

Required final permanent baseline:

-   Domain: 11/11
-   Application: 60/60
-   Infrastructure: 87/87
-   Architecture: 13/13
-   Total: **171/171**
-   skipped: 0 unless repository baseline already proves otherwise
-   build warnings/errors: 0/0

## Documentation Acceptance Matrix

WP15 is complete only if all applicable rows pass:

-   manifest-authorized documentation scope: PASS
-   Release 1.1 → Release 1.2 relationship: PASS
-   production dependency graph alignment: PASS
-   Domain storage/provider independence: PASS
-   Application storage/provider independence: PASS
-   SQLite Infrastructure ownership: PASS
-   Research Dataset vocabulary: PASS
-   deterministic definition/selection/ordering: PASS
-   reproducibility semantics: PASS
-   four identity concepts: PASS
-   Dataset Version semantics: PASS
-   canonical identity scheme representation: PASS
-   provenance semantics: PASS
-   lineage semantics: PASS
-   catalog metadata semantics: PASS
-   schema v2 alignment: PASS
-   v1→v2 evolution alignment: PASS
-   Release 1.1 historical-data preservation: PASS
-   snapshot persistence semantics: PASS
-   catalog persistence/lookup semantics: PASS
-   materialization integration semantics: PASS
-   validation/failure mapping alignment: PASS
-   DI/configuration alignment: PASS
-   bounded Worker execution alignment: PASS
-   successful empty snapshot semantics: PASS
-   multiple immutable versions: PASS
-   timestamp/offset/decimal fidelity: PASS
-   testing strategy/count alignment: PASS
-   implemented-vs-planned distinction: PASS
-   Release 1.3 capability leakage: 0
-   cross-document contradictions: 0
-   broken repository-relative links introduced: 0
-   production code delta: 0
-   permanent functional test delta: 0
-   architecture-test delta: 0 unless separately authorized
-   package/reference delta: 0/0
-   permanent tests: 171/171
-   WP16 started: NO

## Explicit Non-Goals

WP15 must not:

-   redesign dataset semantics;
-   change identity canonicalization;
-   change schema;
-   add schema v3;
-   change persistence behavior;
-   change catalog behavior;
-   change failure mapping;
-   change DI behavior;
-   change Worker execution;
-   add product features;
-   add Release 1.3 pipelines;
-   add scheduling/streaming/refresh/retries;
-   add packages/references;
-   stage/commit/push;
-   create branches/PRs;
-   merge/tag/release;
-   start WP16.

## GitHub Lifecycle

When and only when every WP15 gate passes:

1.  Post concise completion evidence to issue #135.
2.  Close issue #135.
3.  Set Project #2 status to Done.
4.  Verify #136 remains **Open / Backlog**.
5.  Leave milestone #53 open.

Do not start or mutate WP16 beyond state inspection.

If blocked, leave #135 open/in progress and report the exact blocker.

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
9.  Manifest Documentation Scope
10. Release 1.1 Foundation Reconciliation
11. Production Dependency Graph Reconciliation
12. WP02 Dataset Definition/Reproducibility Alignment
13. WP03 Identity/Version/Provenance/Lineage Alignment
14. WP04 Application Contract Alignment
15. WP05 Materialization Alignment
16. WP06 Catalog Metadata Alignment
17. WP07 Physical Storage Alignment
18. WP08 Snapshot Persistence Alignment
19. WP09 Catalog Persistence/Lookup Alignment
20. WP10 Integration Alignment
21. WP11 Validation/Failure Mapping Alignment
22. WP12 DI/Bounded Execution Alignment
23. WP13/WP14 Testing Alignment
24. Implemented-vs-Planned Audit
25. Release 1.3 Protection
26. Architecture-Test Decision
27. Exact Documentation Files Added/Modified
28. Production Code Delta
29. Permanent Test Delta
30. Architecture-Test Delta
31. Package/Reference Delta
32. Documentation/Link Validation
33. Restore/Build Evidence
34. Permanent Test Evidence
35. Canonical Verification
36. Architecture Validation
37. Security/Offline Evidence
38. Whitespace/Diff Evidence
39. Cross-Document Contradiction Audit
40. Mutation Accounting
41. Git/GitHub Protection
42. Planning Protection
43. Findings/Blockers
44. Acceptance Matrix
45. Final Repository/GitHub State
46. WP16 Handoff
47. Final Decision
48. Next Authorized Work Package

A successful report must end exactly with:

`RELEASE 1.2 WP15 COMPLETE`

and:

`NEXT AUTHORIZED WORK PACKAGE: WP16 — Full Validation, Integration & Acceptance — GitHub issue #136`

If blocked, end with:

`RELEASE 1.2 WP15 BLOCKED`

and do not authorize WP16.
