# Release 1.2 WP06 — Dataset Metadata & Catalog Model — Codex Execution Authority

## 1. Authority

You are executing **Release 1.2 WP06 — Dataset Metadata & Catalog Model** for:

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`
- Release: `Phase 3 - Release 1.2: Research Dataset Foundation`
- GitHub issue: `#126`
- Predecessors: WP03 `#123` and WP04 `#124`, both must be Closed / Done.
- WP05 `#125` is accepted implementation context and must be reconciled, but do not change its semantics.
- Next package: WP07 — Dataset Physical Storage Model, issue `#127`.

This file is the authoritative WP06 execution instruction. Read it completely before mutation.

Also read and reconcile, at minimum:

- `RELEASE_1.2_EXECUTION_PLAN.md`
- `RELEASE_1.2_FILE_MANIFEST.md`
- accepted Release 1.2 WP01–WP05 prompt/result artifacts
- `docs/architecture/data/RESEARCH_DATASET_DEFINITION.md`
- `docs/architecture/data/DATASET_IDENTITY_VERSION_PROVENANCE.md`
- current Application dataset contracts and materialization implementation
- existing Release 1.1 persistence/catalog/data architecture documentation
- current Domain/Application/Infrastructure/Worker source and tests
- GitHub issue #126, its dependencies, milestone #53, and Project #2 state

Repository truth wins over assumptions. If authority, repository state, manifest, or accepted predecessor semantics conflict materially, stop and report the smallest corrective authority required.

## 2. Objective

Define the **logical dataset metadata and catalog model** required to describe, register, and later discover immutable Release 1.2 dataset snapshots.

WP06 must establish a precise, storage-independent model that answers:

- What metadata belongs to a dataset snapshot's catalog representation?
- Which facts are identity-bearing versus descriptive/catalog-visible?
- How is an immutable snapshot represented as a catalog entry?
- Which metadata is required for exact lookup, explanation, filtering, and future persistence?
- How are definition, version, coverage, provenance, lineage, and source-state facts exposed without duplicating or contradicting WP03/WP05 semantics?
- What invariants make a catalog entry internally consistent?
- What is intentionally deferred to WP07–WP09?

WP06 is a **modeling package**, not a persistence package.

## 3. Required starting-state gates

Before implementation:

1. Confirm repository identity and authentication without exposing credentials.
2. Confirm branch is `main`.
3. Fetch/reconcile `origin/main`; report HEAD, origin SHA, ahead/behind, staged, tracked, and untracked state.
4. Classify every existing working-tree path. Preserve accepted cumulative Release 1.2 artifacts byte-for-byte unless WP06 explicitly owns them.
5. Confirm:
   - Release 1.1 is closed.
   - milestone #53 is authoritative and Open.
   - #123 and #124 are Closed / Done.
   - #126 is Open / Backlog.
   - #127 remains Open / Backlog.
   - dependency graph for #126 matches the Release 1.2 plan.
   - no Release 1.3 implementation is active.
6. Confirm WP05 materialization artifacts exist and match the accepted handoff.
7. Run the unchanged baseline:
   - restore
   - format verification
   - build
   - all permanent tests
   - architecture tests
   - `eng/verify.ps1`
   - `git diff --check`
   - `git diff --cached --check`
8. Do not move #126 to In Progress until all starting gates pass.

If a starting gate fails, do not mutate production/test/documentation code and do not progress #126.

## 4. Accepted semantic authorities that MUST NOT be redesigned

### WP02

Preserve:

- immutable Research Dataset semantics
- exact single target
- explicit `[from, to)` selection
- deterministic ascending semantic-instant ordering
- successful empty materialization
- original timestamp offset and decimal fidelity
- immutable snapshot evidence
- deterministic reproducibility

### WP03

Preserve:

- four distinct identities:
  - Dataset Definition Identity
  - Research Dataset Identity
  - Source State Identity
  - Dataset Snapshot Identity
- Dataset Version = immutable deterministic Snapshot Identity
- identity scheme `aiq-dataset-identity-v1`
- SHA-256 / 64 lowercase hexadecimal representation
- canonical semantic representation
- provenance and narrow lineage semantics
- identity reassignment prohibited
- collision/integrity-conflict semantics
- operational metadata excluded from semantic identity

Do **not** invent another identity scheme, version model, digest, or canonicalization.

### WP04

Reuse the existing Application dataset contracts. Inspect their exact names and signatures before design.

In particular reconcile:

- `DatasetIdentity.cs`
- `DatasetDefinition.cs`
- `DatasetSnapshotCandidate.cs`
- `DatasetContracts.cs`
- `IDatasetCatalog`

Do not casually break or replace accepted public contracts.

### WP05

Treat `DatasetSnapshotCandidate` emitted by `IMaterializeDatasetUseCase` as the deterministic semantic input.

Preserve:

- exact selected observations
- coverage
- provenance
- lineage
- identities
- version
- source authority
- deterministic empty candidate
- bounded materialization failures

WP06 must not recompute identities differently or introduce a second materialization model.

## 5. Core design principle

Separate three concepts rigorously:

### A. Semantic identity/provenance facts

Already defined by WP03/WP05. They explain **what the snapshot is and where it came from**.

### B. Catalog metadata

Logical, storage-independent facts required to represent and discover an immutable accepted snapshot in a catalog.

### C. Physical catalog persistence

Tables, columns, indexes, SQL, SQLite layout, serialization, filesystem layout, and persistence mechanics.

**WP06 owns B. WP03/WP05 own A. WP07–WP09 own C and its persistence behavior.**

Do not blur these boundaries.

## 6. Required WP06 model

Inspect the existing WP04 `IDatasetCatalog` seam and determine the smallest Application-owned model needed to make that seam semantically complete for later implementation.

The expected direction is a capability-focused model such as a dataset catalog entry/descriptor plus query/lookup semantics, but exact type names must follow repository conventions and the manifest.

The model MUST expose enough information to explain an accepted snapshot without accessing SQLite or re-materializing it.

At minimum, reconcile representation of:

- Dataset Definition Identity
- Research Dataset Identity
- Source State Identity
- Dataset Snapshot Identity
- Dataset Version
- exact target
- requested `[from, to)` boundaries
- selected observation count
- first/last selected semantic instant when non-empty
- explicit empty-state semantics
- identity scheme
- source authority
- provenance facts already carried by the candidate
- narrow lineage facts already carried by the candidate

Prefer composition/reuse of existing WP04 types over copying the same facts into parallel primitive fields.

## 7. Metadata taxonomy

For every proposed field/property, classify it explicitly in the execution report as one of:

1. **Identity-bearing semantic input** — already governed by WP03; catalog merely exposes/reuses it.
2. **Deterministic descriptive metadata** — derived from accepted semantic content and safe to catalog.
3. **Operational metadata** — e.g. wall-clock creation time, machine, process, path. Exclude from semantic identity; add only if explicitly required by accepted authority.
4. **Physical-storage metadata** — table IDs, row IDs, database paths, filenames, offsets, SQL keys. Prohibited in WP06.

Default to excluding operational metadata unless the Release 1.2 authorities explicitly require it.

Do not introduce `CreatedAt`, `UpdatedAt`, random IDs, database-generated IDs, mutable status, "latest" pointers, or machine-specific metadata merely because catalogs commonly have them.

## 8. Catalog entry invariants

The logical catalog model must enforce or make explicit, as appropriate to repository conventions:

- snapshot identity and Dataset Version agree exactly
- definition identity agrees with the represented definition
- research dataset identity is the correct logical identity concept
- source-state identity is present
- exact target is preserved
- requested boundaries are valid `[from, to)`
- count is non-negative
- empty snapshot:
  - count = 0
  - first/last actual observation instant absent
- non-empty snapshot:
  - count > 0
  - first and last actual observation instants present
  - first <= last
  - actual instants lie within requested boundaries
- coverage/provenance/lineage cannot contradict the entry
- identity scheme remains `aiq-dataset-identity-v1`
- immutable accepted snapshot metadata cannot be mutated into a different snapshot

Reuse existing validated value objects/contracts wherever possible rather than duplicating validation.

## 9. Catalog semantics

Define the logical behavior expected from the catalog seam sufficiently for WP09 to implement it without semantic invention.

The accepted model should support **exact immutable snapshot registration and lookup**, not a general search engine.

At minimum determine semantics for:

### Registration

- registering a previously unknown snapshot catalog entry
- registering semantically equivalent evidence again
- attempting to register contradictory metadata for the same Snapshot Identity
- prohibition on overwrite/reassignment
- distinction between idempotent equivalent registration and integrity conflict

Do not implement registration persistence in WP06.

### Exact lookup

The catalog must be capable of exact lookup by the strongest accepted immutable identity: Dataset Snapshot Identity / Dataset Version.

If WP04 already fixed the exact lookup signature, preserve it.

Define:

- found semantics
- not-found semantics
- invalid input semantics
- integrity failure boundary as representable by existing/authorized vocabulary

Do not add broad filtering/search APIs unless the execution plan explicitly requires them for WP06.

### Logical-dataset/version relationship

A Research Dataset Identity may have multiple immutable Snapshot Identities/Versions when relevant source state changes.

Do not model a mutable "current version" or overwrite prior versions.

If version enumeration is not explicitly required by the WP04 seam/manifest, document it as later capability rather than expanding scope.

## 10. Metadata vs provenance

Avoid a second provenance model.

Where `DatasetSnapshotCandidate`, `DatasetProvenance`, `DatasetCoverage`, or `DatasetLineage` already contain the required facts:

- reuse them directly when appropriate; or
- define a deliberately minimal immutable catalog descriptor that composes them.

Any duplication must have a clear contract reason and must be protected by consistency invariants.

Do not create competing definitions of:

- source authority
- source state
- coverage
- lineage
- identity scheme
- definition identity
- snapshot version

## 11. Application-layer ownership

WP06 logical catalog types belong in the **Application dataset boundary** unless the accepted manifest says otherwise.

Requirements:

- provider-independent
- storage-independent
- no SQLite types
- no SQL
- no ORM annotations
- no filesystem paths
- no connection strings
- no provider DTOs
- no HTTP types
- no Infrastructure references
- no Worker references

Domain delta should remain zero unless a genuine Domain-owned invariant is impossible to express with current Domain values. If a Domain delta appears necessary, stop and justify before mutation.

## 12. Scope expected from the manifest

Read `RELEASE_1.2_FILE_MANIFEST.md` and obey its exact WP06 path authorization.

Do not create files merely because this prompt suggests conceptual names.

If the manifest authorizes Application files for WP06, use the smallest set necessary. Keep naming consistent with the current `AIQuantTradingResearch.Application.Datasets` namespace and repository conventions.

Do not modify WP02/WP03 architecture artifacts unless the manifest explicitly assigns such alignment to WP06. WP15 owns broad documentation alignment.

## 13. Explicit non-goals

WP06 MUST NOT implement:

- SQLite schema or DDL
- physical dataset storage
- snapshot persistence
- catalog persistence
- SQL queries
- catalog lookup implementation
- connection/configuration behavior
- migrations
- filesystem layout
- serialization format for persistence
- ORM entities
- Worker execution
- DI registration
- materialization orchestration changes
- source-history retrieval changes
- identity canonicalization redesign
- identity digest redesign
- permanent tests assigned to WP13/WP14
- Release 1.3 pipelines, scheduling, refresh, streaming, DAGs, monitoring, or resilience

WP07 owns physical storage modeling.
WP08 owns snapshot persistence.
WP09 owns catalog persistence and lookup.
WP10 owns materialization integration.
WP11 owns validation/failure mapping.
WP12 owns DI/bounded execution.
WP13/WP14 own permanent tests.
WP15 owns architecture/documentation alignment.
WP16 owns full integration.

## 14. Implementation approach

After starting gates pass:

1. Move issue #126 from Backlog to In Progress.
2. Inspect exact WP04 catalog contract and candidate model.
3. Produce a concise design matrix before coding:
   - required catalog fact
   - existing owner/type
   - reuse vs new representation
   - identity-bearing?
   - catalog-visible?
   - physical?
4. Select the smallest model consistent with the manifest.
5. Implement only WP06-owned logical metadata/catalog types or contract refinements.
6. Preserve public API compatibility where possible.
7. If WP04's seam is insufficient and the manifest permits refinement, make only the minimum change necessary and explain why.
8. Do not implement any Infrastructure behavior.
9. Do not add permanent tests unless the manifest explicitly assigns them to WP06.
10. Temporary offline probes are allowed only when necessary to prove invariants; remove them completely before final accounting.

## 15. Required semantic scenarios to validate

Even if permanent tests are deferred, reason through and, where useful, prove temporarily:

1. Non-empty catalog entry from a valid WP05 candidate.
2. Empty candidate produces a valid catalog representation with count zero and absent actual first/last instant.
3. Snapshot Identity equals Dataset Version.
4. Exact target including case/whitespace remains unchanged.
5. Requested boundaries remain exact.
6. Coverage count and actual bounds remain consistent.
7. Original observation offset/decimal semantics are not rewritten by catalog modeling.
8. Equivalent re-materialization can map to equivalent catalog metadata.
9. Same Research Dataset Identity may coexist conceptually with a distinguishable Snapshot Identity after relevant source-state change.
10. Contradictory metadata cannot silently claim the same Snapshot Identity.
11. No wall-clock/random/path/database value is required to construct semantic catalog metadata.
12. No mutable "latest" semantics are introduced.

## 16. Failure and outcome vocabulary

Do not casually create a large new public failure taxonomy.

First inspect WP04's existing `DatasetMaterializationResult`, `DatasetMaterializationFailure`, `IDatasetSnapshotStore`, and `IDatasetCatalog` contracts.

For catalog semantics, introduce/refine outcome vocabulary only if the manifest and existing seam require it.

Any catalog registration model must be able to distinguish conceptually:

- newly registered
- equivalent/idempotent existing entry
- integrity conflict

Any lookup model must distinguish conceptually:

- found
- not found
- invalid/integrity failure as appropriate

But **WP06 defines logical representation; WP09 implements persistence behavior**.

Do not reuse Release 1.1 persistence outcomes blindly if dataset-specific semantics warrant bounded dataset vocabulary. Conversely, do not proliferate types when existing WP04 types already represent the required states.

## 17. Immutability

Catalog entries represent immutable snapshot evidence.

Prohibit semantics equivalent to:

- update snapshot metadata in place
- change version of an existing snapshot
- reassign Snapshot Identity
- replace provenance/lineage under the same identity
- delete-and-reinsert to hide conflict
- mutable "current snapshot" state

WP06 does not need to design retention/deletion policy.

## 18. Security and privacy

No secrets, API keys, connection strings, credentials, tokens, personal paths, machine identifiers, or database contents may enter catalog metadata.

Do not log sensitive values.

Run the repository's canonical security verification unchanged.

## 19. Architecture protection

Final production graph must remain:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Expected WP06:

- Domain production delta: 0
- Infrastructure production delta: 0
- Worker production delta: 0
- package delta: 0
- project-reference delta: 0

If any of those become non-zero, treat it as a scope alarm and justify against explicit authority before proceeding.

## 20. Validation gates

After implementation, run at minimum:

- restore
- format verification
- build
- Domain.Tests
- Application.Tests
- Infrastructure.Tests
- Architecture.Tests
- `eng/verify.ps1`
- `git diff --check`
- `git diff --cached --check`

For untracked authorized WP06 files, perform direct whitespace validation because ordinary `git diff --check` does not inspect untracked files.

Also verify:

- build warnings = 0
- build errors = 0
- skipped tests = 0 unless pre-existing and explicitly explained
- provider/network calls = 0
- temporary database residue = 0
- temporary probe residue = 0
- SQLite/SQL/filesystem leakage into Domain/Application = 0
- Release 1.3 implementation = 0

## 21. Git and GitHub protection

WP06 may mutate only the authorized issue lifecycle and repository files allowed by the manifest.

Do NOT:

- stage files
- commit
- push
- create/switch branches for integration
- open a PR
- merge
- tag
- release
- rewrite history

Do not modify #127 or later issues.

After all acceptance gates pass:

1. post concise evidence to #126;
2. close #126;
3. set its Project status to Done;
4. leave #127 Open / Backlog.

## 22. Required final report

Produce a structured execution report containing at least:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. WP02 Reconciliation
10. WP03 Reconciliation
11. WP04 Contract Reconciliation
12. WP05 Candidate Reconciliation
13. Catalog Model Design
14. Metadata Taxonomy
15. Catalog Entry Invariants
16. Registration Semantics
17. Exact Lookup Semantics
18. Logical Dataset / Version Relationship
19. Empty Snapshot Semantics
20. Coverage Semantics
21. Provenance/Lineage Reuse
22. Identity Consistency
23. Operational-Metadata Exclusions
24. Application Ownership
25. Exact Files Added/Modified
26. Domain Delta
27. Infrastructure Delta
28. Worker Delta
29. Package/Reference Delta
30. Permanent Test Delta
31. Temporary Probe Evidence
32. WP07/WP08/WP09 Protection
33. Release 1.3 Protection
34. Security/Offline Evidence
35. Whitespace/Diff Evidence
36. Restore/Build Evidence
37. Permanent Test Evidence
38. Canonical Verification
39. Architecture Validation
40. Semantic Acceptance Matrix
41. Mutation Accounting
42. Git/GitHub Protection
43. Planning Protection
44. Findings/Blockers
45. Final Repository/GitHub State
46. WP07 Handoff
47. Final Decision
48. Next Authorized Work Package

## 23. Acceptance criteria

WP06 is complete only if all are true:

- #126 predecessor and lifecycle gates pass.
- WP02/WP03 semantics remain unchanged.
- WP04 contracts are reused/refined minimally.
- WP05 deterministic candidate is treated as source semantic input.
- logical catalog metadata is explicitly defined.
- identity-bearing facts are distinguished from descriptive metadata.
- operational/physical metadata does not contaminate semantic identity.
- exact immutable Snapshot Identity / Dataset Version is central to catalog representation.
- Research Dataset Identity and Snapshot Identity remain distinct.
- empty snapshot metadata is valid and deterministic.
- coverage/provenance/lineage are reused without competing models.
- equivalent re-materialization remains representable as equivalent evidence.
- contradictory same-identity metadata is an integrity conflict, never overwrite.
- no mutable "latest" semantics are introduced.
- Domain/Application remain SQLite/provider/filesystem independent.
- no physical storage/catalog persistence is implemented.
- no WP07+ work is started.
- production graph remains valid.
- package/reference delta is zero unless explicit authority proves otherwise.
- all validation gates pass.
- issue #126 ends Closed / Done.
- issue #127 remains Open / Backlog.
- working tree remains uncommitted and unstaged.

## 24. Terminal marker

On success, end exactly with:

RELEASE 1.2 WP06 COMPLETE

DATASET METADATA & CATALOG MODEL:
WP02 dataset semantics preserved: PASS
WP03 identity/version/provenance semantics preserved: PASS
WP04 dataset contracts reconciled: PASS
WP05 materialization candidate reconciled: PASS
Logical catalog metadata model: PASS
Identity-bearing vs descriptive metadata separation: PASS
Research Dataset / Snapshot identity separation: PASS
Dataset Version consistency: PASS
Exact target/boundary metadata: PASS
Coverage metadata: PASS
Successful empty snapshot metadata: PASS
Provenance reuse: PASS
Lineage reuse: PASS
Equivalent re-materialization semantics: PASS
Integrity-conflict semantics: PASS
Immutable catalog evidence: PASS
Mutable latest semantics introduced: NO
Operational metadata in semantic identity: 0
Physical-storage metadata leakage: 0
SQLite/SQL/filesystem leakage: 0
Provider/HTTP leakage: 0
Domain delta: 0
Infrastructure delta: 0
Worker delta: 0
Permanent test delta: <report>
Package/reference delta: 0/0
WP07 started: NO
Release 1.3 implementation started: NO
Issue #126: CLOSED / DONE

NEXT AUTHORIZED WORK PACKAGE:
WP07 — Dataset Physical Storage Model
GitHub issue #127

If blocked, do not emit the success marker. Emit `RELEASE 1.2 WP06 BLOCKED` and state the smallest corrective authority required.
