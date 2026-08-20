# Release 1.2 WP11 --- Dataset Validation & Failure Mapping --- Codex Execution Authority

## 1. Authority

You are authorized to execute **Release 1.2 WP11 --- Dataset Validation
& Failure Mapping** for:

-   Repository: `samuel-santos-engineer/AIQuantTradingResearch`
-   GitHub issue: `#131`
-   Milestone:
    `#53 — Phase 3 - Release 1.2: Research Dataset Foundation`
-   Project: `#2 — AIQuantTradingResearch Engineering Roadmap`

This prompt is the execution authority for WP11. Interpret it together
with:

-   `RELEASE_1.2_EXECUTION_PLAN.md`
-   `RELEASE_1.2_FILE_MANIFEST.md`
-   accepted WP01--WP10 artifacts and execution evidence
-   `docs/architecture/data/RESEARCH_DATASET_DEFINITION.md`
-   `docs/architecture/data/DATASET_IDENTITY_VERSION_PROVENANCE.md`
-   current Application dataset contracts and materialization
    integration
-   accepted Release 1.1 persistence/failure semantics
-   accepted WP07 SQLite schema v2
-   accepted WP08 snapshot persistence
-   accepted WP09 catalog persistence/lookup
-   accepted WP10 dataset materialization integration

If these authorities materially conflict, stop before mutation and
report the conflict and the smallest corrective authority required.

## 2. Objective

Implement the **minimum bounded dataset validation and failure-mapping
behavior** required to make the accepted Release 1.2 dataset workflow
deterministic and contained at its established
Application/Infrastructure boundaries.

WP11 must refine validation and failure handling without redesigning:

-   dataset definition semantics;
-   identity/version/provenance semantics;
-   Application dataset contracts except where the accepted failure
    vocabulary explicitly requires minimal completion;
-   materialization;
-   SQLite schema v2;
-   snapshot persistence;
-   catalog persistence;
-   WP10 integration orchestration.

The goal is to ensure invalid semantic inputs/evidence and known storage
failures are represented by the accepted bounded contracts rather than
leaking raw provider/storage mechanics across architectural boundaries.

## 3. Required Starting-State Gates

Before changing anything, verify and report:

1.  Repository identity is exactly
    `samuel-santos-engineer/AIQuantTradingResearch`.
2.  Current branch is `main`.
3.  Local `main` equals `origin/main`; report both SHAs and
    ahead/behind.
4.  Staged paths are zero.
5.  Every working-tree path is classified as accepted cumulative Release
    1.2 work or explicitly authorized WP11 work.
6.  Unexpected/ambiguous paths are zero.
7.  Release 1.1 remains closed and accepted.
8.  Milestone #53 is open.
9.  WP10 issue #130 is Closed/Done.
10. WP11 issue #131 is Open/Backlog.
11. WP12 issue #132 is Open/Backlog.
12. WP11 dependency is exactly WP10 according to authoritative planning.
13. No Release 1.3 implementation has started.
14. Restore, format verification, build, permanent tests, architecture
    tests, canonical verification, secret scan, and Git whitespace
    checks pass before mutation.

Do not move #131 to In Progress until every starting-state gate passes.

If any starting-state gate fails, stop without repository or GitHub
mutation and end with:

`RELEASE 1.2 WP11 BLOCKED`

## 4. Mandatory Reconciliation Before Mutation

### WP02 --- Dataset semantics

Preserve:

-   exact target identity;
-   explicit valid `[from, to)` boundaries;
-   deterministic ascending semantic-instant ordering;
-   valid successful empty materialization;
-   immutable snapshot semantics;
-   exact timestamp/offset and decimal fidelity.

Validation must reject invalid data; it must not normalize invalid data
into another valid semantic value.

### WP03 --- Identity/version/provenance

Preserve:

-   Dataset Definition Identity;
-   Research Dataset Identity;
-   Source State Identity;
-   Dataset Snapshot Identity;
-   Dataset Version = Snapshot Identity;
-   `aiq-dataset-identity-v1`;
-   canonical identity representation;
-   immutable identity assignment;
-   integrity-conflict semantics;
-   provenance and narrow lineage.

Do not silently recompute or repair contradictory persisted identity
evidence.

### WP04 --- Application contracts

Inventory the currently accepted dataset result/failure vocabularies
before changing anything.

Reuse existing public failure categories whenever they can represent
WP11 semantics correctly.

Do not create storage-specific public failures or expose SQLite
exceptions/types.

Any public contract refinement must be the smallest change required by
the accepted execution plan/file manifest and must remain
Application-owned and provider/storage independent.

### WP05 --- Materialization

Preserve deterministic materialization and existing source-history
mapping.

WP11 must not duplicate materialization or identity computation.

### WP06 --- Catalog model

Preserve immutable `DatasetCatalogEntry` semantics.

Invalid catalog evidence must not be normalized, repaired, or assigned
mutable operational metadata.

### WP07 --- Physical model

Schema v2 is fixed.

No schema version, table, column, index, constraint, record-layout,
migration, or mapper redesign is authorized unless a contradiction
requires stopping.

### WP08 --- Snapshot persistence

Preserve immutable, atomic, insert-only snapshot semantics.

Inventory its current failure behavior and identify which known
SQLite/row-validation failures require bounded mapping.

### WP09 --- Catalog persistence

Preserve exact lookup, immutable registration, equivalent-existing
behavior, integrity conflicts, and lookup misses.

Do not create search/list/latest semantics.

### WP10 --- Integration

Preserve the accepted integration flow and outcome matrix.

WP11 may refine failure propagation/mapping where explicitly required,
but must not redesign orchestration.

The accepted high-level sequence remains:

`materialize → snapshot persistence → catalog registration`

## 5. Validation Taxonomy

Establish the smallest explicit validation taxonomy consistent with
existing contracts.

At minimum distinguish conceptually:

### Semantic invalidity

Examples include, where reachable at the relevant boundary:

-   invalid/blank target;
-   invalid `[from, to)` boundaries;
-   malformed identity fingerprint/scheme;
-   Dataset Version inconsistent with Snapshot Identity;
-   inconsistent count/coverage;
-   inconsistent provenance or lineage;
-   duplicate or non-ascending semantic instants;
-   contradictory target/definition/candidate/catalog facts;
-   invalid timestamp/offset representation;
-   invalid decimal representation/value;
-   malformed persisted dataset evidence.

Use existing constructors/contracts as the first validation boundary. Do
not duplicate checks merely to create another validation layer.

### Availability/storage failure

Known transient/operational SQLite failures that prevent required
persistence/lookup should map to the existing bounded
unavailable/storage-unavailable semantics where authorized.

Do not infer retryability or add retry policy.

### Integrity conflict

Contradictory immutable evidence under the same semantic identity
remains an integrity conflict, not availability failure and not success.

### Not found

An exact catalog miss remains the accepted `NotFound` behavior. Do not
classify an ordinary miss as corruption or unavailability.

### Programming/unknown failure

Programming defects and unknown/unclassified failures must not be
swallowed or falsely converted into a public failure.

## 6. Failure-Mapping Boundary

Keep provider/storage mechanics inside Infrastructure.

Where known SQLite failures arise in dataset snapshot/catalog
persistence:

-   map only explicitly recognized SQLite failure classes/codes;
-   use the accepted dataset failure vocabulary;
-   preserve semantic conflicts as semantic outcomes;
-   preserve exact lookup misses;
-   map malformed persisted dataset rows/evidence to the accepted
    invalid/integrity failure only if the current contracts authorize
    that distinction;
-   allow unknown SQLite codes to propagate rather than guessing;
-   do not catch arbitrary `Exception`.

Follow the established Release 1.1 failure-mapping discipline where
semantically applicable, but do not mechanically copy
observation-specific behavior if dataset contracts differ.

The final report must state the exact SQLite/error categories mapped and
the reason for each mapping.

## 7. Application Validation Boundary

Application validation should remain focused on semantic contracts and
orchestration.

Do not add Infrastructure-aware validation.

Where accepted constructors already make invalid states unrepresentable,
document that as the validation mechanism instead of adding redundant
validators.

Where WP10 integration can receive an authorized failure from a lower
seam, map/propagate it deterministically according to the existing
integration contract.

Do not collapse:

-   integrity conflict into unavailable;
-   unavailable into integrity conflict;
-   not-found into invalid data;
-   successful equivalence into conflict.

## 8. Persisted-Evidence Validation

When reconstructing persisted snapshot/catalog evidence, malformed or
contradictory stored data must never be:

-   skipped;
-   repaired;
-   normalized;
-   overwritten;
-   partially returned as successful evidence.

If reconstruction cannot produce a valid accepted Application model,
return/map the authorized bounded invalid/integrity failure where the
contract supports it; otherwise preserve the failure for the correct
boundary and report the handoff.

No partial snapshot/catalog result is permitted after a classified
reconstruction failure.

## 9. Atomicity and Immutability Protection

WP11 must preserve:

-   WP08 transaction atomicity;
-   insert-only immutable snapshot evidence;
-   equivalent-existing behavior;
-   non-destructive integrity conflicts;
-   coexistence of multiple versions;
-   no update/delete/replace/repair path.

Failure mapping must not turn a failed transaction into partial success.

## 10. Fidelity Protection

Validation/failure handling must not alter:

-   target text;
-   requested boundaries;
-   observation membership/order;
-   UTC semantic instant;
-   original `DateTimeOffset` offset;
-   exact decimal value;
-   four dataset identities;
-   Dataset Version;
-   coverage;
-   provenance;
-   lineage;
-   source authority.

No culture-sensitive conversion, local-time conversion, floating-point
intermediary, rounding, or truncation.

## 11. Connection and Disposal Protection

Reuse the accepted `ISqliteConnectionFactory` and operation-owned
connection model.

No live SQLite connection may be retained in DI or shared as mutable
state.

Connections, transactions, commands, and readers must remain
deterministically disposed.

WP11 must not introduce connection pooling policy, retry policy, or
resilience behavior.

## 12. Expected File Scope

Follow `RELEASE_1.2_FILE_MANIFEST.md` exactly.

Prefer the smallest changes to existing dataset persistence/integration
files needed to complete validation/failure mapping.

Likely authorized areas are:

-   Application dataset failure/result contracts only if the manifest
    and current vocabulary require minimal refinement;
-   Infrastructure SQLite dataset snapshot/catalog implementation for
    bounded failure mapping;
-   WP10 Application integration only if necessary to propagate an
    already-authorized failure correctly.

Do not change Domain or Worker.

Do not add packages, project references, solution changes, build-script
changes, schema changes, or configuration.

If correct implementation requires any prohibited expansion, stop and
report the authority conflict.

## 13. Permanent Tests and Temporary Validation

Permanent test expansion remains owned by WP13/WP14 unless the manifest
explicitly authorizes WP11 test changes.

Expected permanent-test delta: `0`.

Temporary focused offline probes are allowed and must be removed before
completion.

At minimum prove, as applicable to repository truth:

1.  invalid semantic input is rejected deterministically;
2.  malformed identity/version relationship cannot be accepted;
3.  contradictory immutable evidence remains `IntegrityConflict`;
4.  known SQLite operational failure maps to the authorized unavailable
    failure;
5.  malformed persisted dataset evidence maps to the authorized
    invalid/integrity failure if supported;
6.  unknown SQLite failure is not falsely classified;
7.  exact catalog `NotFound` remains distinct;
8.  equivalent existing evidence remains successful equivalence;
9.  failed snapshot persistence prevents catalog registration;
10. no partial evidence after failure;
11. atomicity remains intact;
12. valid empty snapshot remains successful;
13. exact target/boundary/coverage fidelity remains intact;
14. provenance/lineage remains intact;
15. timestamp/offset/decimal fidelity remains intact;
16. no provider/network calls;
17. temporary database residue is zero.

Do not create artificial production hooks solely to force failures for
probes.

## 14. Explicitly Prohibited Scope

WP11 must not start:

-   WP12 --- Dependency Registration & Bounded Dataset Execution;
-   WP13 --- Domain & Application Dataset Tests;
-   WP14 --- Infrastructure & Dataset Tests;
-   WP15 --- Architecture & Documentation Alignment;
-   WP16 --- Full Validation, Integration & Acceptance;
-   Release 1.3.

Also prohibited:

-   DI registration;
-   Worker execution;
-   new configuration keys;
-   schema v3;
-   migrations;
-   new persistence/catalog abstraction;
-   mutable latest semantics;
-   provider acquisition;
-   retries;
-   circuit breakers;
-   timeout policy;
-   scheduling;
-   background execution;
-   streaming;
-   pipelines/DAGs;
-   monitoring;
-   caching;
-   destructive repair.

Do not stage, commit, push, branch, create a PR, merge, tag, or release.

## 15. Architecture Protection

Production dependency graph must remain:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

Required leakage counts:

-   Domain SQLite/SQL/filesystem references: 0
-   Application SQLite/SQL/filesystem references: 0
-   Domain provider/HTTP mechanics: 0
-   Application provider/HTTP mechanics: 0

Production dependency cycles: 0.

## 16. Security and Offline Constraints

Validation must remain offline except for required GitHub planning
inspection/mutation.

Do not use:

-   real market-data provider calls;
-   real API keys;
-   credentials;
-   sensitive connection strings;
-   committed personal paths.

Run the canonical Gitleaks/secret scan.

## 17. GitHub Lifecycle

After every starting-state and baseline gate passes:

1.  move issue #131 Backlog → In Progress;
2.  execute WP11;
3.  run all final gates;
4.  post concise completion evidence to #131;
5.  close #131 and set Project status Done only if all acceptance gates
    pass.

Issue #132 must remain Open/Backlog.

Do not mutate unrelated issues, milestone metadata, Project
schema/options, labels, dependencies, priorities, Release, Area, or
assignee fields.

## 18. Required Final Validation

Before declaring completion, run and report:

-   restore;
-   format verification;
-   build;
-   Domain.Tests;
-   Application.Tests;
-   Infrastructure.Tests;
-   Architecture.Tests;
-   total permanent tests;
-   `eng/verify.ps1`;
-   canonical secret scan;
-   `git diff --check`;
-   `git diff --cached --check`;
-   direct whitespace checks for relevant untracked WP11 files;
-   architecture dependency/leakage validation;
-   Release 1.1 persistence regression;
-   WP08 snapshot-persistence regression;
-   WP09 catalog regression;
-   WP10 integration regression;
-   WP11 validation/failure matrix;
-   temporary SQLite residue;
-   package/reference delta;
-   final working-tree classification.

Expected permanent baseline if unchanged:

-   Domain.Tests: 11
-   Application.Tests: 42
-   Infrastructure.Tests: 79
-   Architecture.Tests: 13
-   Total: 145

The WP10 report recorded a local Windows Application Control observation
affecting a generated Debug Architecture.Tests DLL after mutation while
the isolated Release architecture suite and full Release canonical
workflow passed. Treat environment-specific recurrence as evidence to
report, not authority to alter repository/system security policy. Do not
implement a workaround unless separately authorized.

## 19. Acceptance Matrix

WP11 is complete only if all applicable rows pass:

-   WP10 predecessor accepted;
-   issue #131 lifecycle valid;
-   existing semantic contracts reused;
-   validation remains at correct layer boundaries;
-   known invalid semantic states rejected;
-   known storage unavailability mapped to authorized failure;
-   malformed persisted evidence contained according to accepted
    contract;
-   unknown failures not falsely classified;
-   no `catch (Exception)`;
-   integrity conflict remains distinct;
-   exact `NotFound` remains distinct;
-   equivalent existing remains successful;
-   materialization failure prevents persistence;
-   snapshot failure prevents catalog registration;
-   no partial successful evidence after failure;
-   atomicity preserved;
-   immutability preserved;
-   empty snapshot semantics preserved;
-   identity/version fidelity preserved;
-   target/boundary/coverage fidelity preserved;
-   provenance/lineage fidelity preserved;
-   timestamp/offset/decimal fidelity preserved;
-   schema remains v2;
-   Domain/Application SQLite leakage = 0;
-   provider/network calls = 0;
-   production graph unchanged;
-   permanent-test delta = 0 unless manifest-authorized;
-   package/reference delta = 0/0;
-   temporary residue = 0;
-   WP12 not started;
-   Release 1.3 not started;
-   canonical validation passes.

## 20. Required Execution Report

Return a numbered execution report covering at least:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Predecessor/Lifecycle Gates
7.  Issue Lifecycle
8.  Initial Baseline
9.  WP02 Semantic Reconciliation
10. WP03 Identity/Version/Provenance Reconciliation
11. WP04 Contract/Failure-Vocabulary Reconciliation
12. WP05 Materialization Reconciliation
13. WP06 Catalog-Model Reconciliation
14. WP07 Physical-Model Reconciliation
15. WP08 Snapshot-Persistence Reconciliation
16. WP09 Catalog-Persistence Reconciliation
17. WP10 Integration Reconciliation
18. Validation Surface Discovery
19. Validation Boundary Design
20. Failure Surface Discovery
21. Failure-Mapping Boundary Design
22. Semantic Invalidity Handling
23. Identity/Version Validation
24. Coverage/Provenance/Lineage Validation
25. SQLite Error Classification
26. Snapshot Persistence Failure Mapping
27. Catalog Persistence/Lookup Failure Mapping
28. Integration Failure Propagation
29. Integrity Conflict Preservation
30. NotFound Preservation
31. Equivalent-Existing Preservation
32. Malformed Persisted Evidence Handling
33. Unknown Failure Behavior
34. Atomicity/Immutability Protection
35. Empty Snapshot Protection
36. Fidelity Protection
37. Connection Ownership/Disposal
38. Exact Files Added/Modified
39. Domain Delta
40. Application Delta
41. Infrastructure Delta
42. Worker Delta
43. Package/Reference Delta
44. Permanent Test Delta
45. Temporary Probe Evidence
46. WP12 Protection
47. Release 1.3 Protection
48. Security/Offline Evidence
49. Whitespace/Diff Evidence
50. Restore/Build Evidence
51. Permanent Test Evidence
52. Canonical Verification
53. Architecture Validation
54. Release 1.1 Persistence Regression
55. WP08 Snapshot-Persistence Regression
56. WP09 Catalog Regression
57. WP10 Integration Regression
58. Validation/Failure-Mapping Matrix
59. Mutation Accounting
60. Git/GitHub Protection
61. Planning Protection
62. Findings/Blockers
63. Final Repository/GitHub State
64. WP12 Handoff
65. Final Decision
66. Next Authorized Work Package

## 21. Terminal Markers

On success, end exactly with:

`RELEASE 1.2 WP11 COMPLETE`

Then include:

`NEXT AUTHORIZED WORK PACKAGE: WP12 — Dependency Registration & Bounded Dataset Execution — GitHub issue #132`

If blocked, end with:

`RELEASE 1.2 WP11 BLOCKED`

and identify the smallest corrective authority required.

## 22. Execution Principle

WP11 is a **boundary-hardening work package**, not an architecture
redesign.

Prefer explicit, narrow validation and failure classification over broad
exception handling.

Reuse accepted contracts and Release 1.1 failure-boundary discipline
where semantically applicable. Preserve integrity conflicts, exact
lookup misses, equivalent evidence, immutable history, and unknown
programming/storage failures as distinct concepts.

Do not make later WP12--WP16 work easier by prematurely implementing it.
