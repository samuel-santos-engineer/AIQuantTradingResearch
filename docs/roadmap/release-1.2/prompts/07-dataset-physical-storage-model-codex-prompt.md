# Release 1.2 WP07 --- Dataset Physical Storage Model --- Codex Execution Authority

## 1. Authority

You are executing **Release 1.2 WP07 --- Dataset Physical Storage
Model** for:

-   Repository: `samuel-santos-engineer/AIQuantTradingResearch`
-   Release: `Phase 3 - Release 1.2: Research Dataset Foundation`
-   GitHub issue: `#127`
-   Required predecessors: WP05 `#125` and WP06 `#126`, both must be
    Closed / Done.
-   Next package: WP08 --- Dataset Snapshot Persistence, issue `#128`.

This file is the authoritative WP07 execution instruction. Read it
completely before mutation.

Also read and reconcile, at minimum:

-   `RELEASE_1.2_EXECUTION_PLAN.md`
-   `RELEASE_1.2_FILE_MANIFEST.md`
-   accepted Release 1.2 WP01--WP06 prompt/result artifacts
-   `docs/architecture/data/RESEARCH_DATASET_DEFINITION.md`
-   `docs/architecture/data/DATASET_IDENTITY_VERSION_PROVENANCE.md`
-   current Application dataset contracts, materialization
    implementation, and catalog metadata model
-   existing Release 1.1 SQLite schema/bootstrap/connection/store
    implementation
-   current Domain/Application/Infrastructure/Worker source and tests
-   existing data-storage architecture documentation
-   GitHub issue #127, its dependencies, milestone #53, and Project #2
    state

Repository truth wins over assumptions. If authority, repository state,
manifest, accepted predecessor semantics, or the existing Release 1.1
SQLite boundary conflict materially, stop and report the smallest
corrective authority required.

## 2. Objective

Define the **minimum Infrastructure-owned physical storage model**
capable of durably representing accepted Release 1.2 dataset snapshots
and their exact catalog metadata while preserving Release 1.1
persistence behavior.

WP07 must settle the physical representation sufficiently that WP08 and
WP09 can implement persistence and lookup without inventing schema
semantics.

WP07 must answer:

-   Which SQLite tables/records represent an immutable dataset snapshot?
-   Which table/record represents its catalog-visible descriptor?
-   How are Snapshot Identity / Dataset Version and the other WP03
    identities stored losslessly?
-   How are Dataset Definition, coverage, provenance, lineage, source
    authority, and empty snapshots represented?
-   How are selected `PriceObservation` values associated with a
    snapshot while preserving exact timestamp/offset/decimal fidelity
    and deterministic ordering?
-   Which keys, uniqueness constraints, checks, and indexes enforce the
    accepted immutable identity model?
-   How does schema evolution coexist with Release 1.1 schema version 1
    and existing `historical_observations` data?
-   What remains explicitly deferred to WP08 and WP09?

WP07 is a **physical-model and schema-evolution package**, not a
persistence-behavior package.

## 3. Required starting-state gates

Before implementation:

1.  Confirm repository identity and GitHub authentication without
    exposing credentials.
2.  Confirm branch is `main`.
3.  Fetch/reconcile `origin/main`; report HEAD, origin SHA,
    ahead/behind, staged, tracked, and untracked state.
4.  Classify every existing working-tree path. Preserve accepted
    cumulative Release 1.2 artifacts byte-for-byte unless WP07
    explicitly owns them.
5.  Confirm:
    -   Release 1.1 is closed.
    -   milestone #53 is authoritative and Open.
    -   #125 and #126 are Closed / Done.
    -   #127 is Open / Backlog.
    -   #128 remains Open / Backlog.
    -   #127 depends exactly on #125 and #126.
    -   no Release 1.3 implementation is active.
6.  Confirm accepted WP05 materialization artifacts and WP06 catalog
    model exist and match their handoffs.
7.  Reconcile the existing Release 1.1 SQLite boundary, including schema
    versioning, bootstrap, connection factory, mapper, store, and
    `historical_observations`.
8.  Run the unchanged baseline:
    -   restore
    -   format verification
    -   build
    -   all permanent tests
    -   architecture tests
    -   `eng/verify.ps1`
    -   `git diff --check`
    -   `git diff --cached --check`
9.  Do not move #127 to In Progress until every starting gate passes.

If a starting gate fails, do not mutate production/test/documentation
code and do not progress #127.

## 4. Manifest authority and mutation boundary

`RELEASE_1.2_FILE_MANIFEST.md` authorizes WP07 changes only under:

`src/AIQuantTradingResearch.Infrastructure/**`

Allowed WP07 concerns include:

-   snapshot physical records
-   catalog physical records
-   mappings
-   schema representation
-   minimal schema-version evolution
-   SQLite constraints/indexes required by approved exact lookup
    semantics

Existing Release 1.1 SQLite bootstrap/schema files may change **only**
to add Release 1.2 schema while preserving `historical_observations`
behavior and existing data.

Default production package delta is **0**.

Do not add a package merely for convenience. If a package appears
genuinely required, stop and report the exact evidence and smallest
separate authority required rather than adding it.

## 5. Accepted semantic authorities that MUST NOT be redesigned

### WP02 --- Dataset definition/reproducibility

Preserve:

-   immutable deterministic research datasets
-   exact single target
-   explicit `[from, to)` boundaries
-   deterministic ascending semantic-instant ordering
-   successful empty materialization
-   original `DateTimeOffset` offset representation
-   exact decimal fidelity
-   immutable snapshot evidence
-   deterministic reproducibility

### WP03 --- Identity/version/provenance

Preserve exactly:

-   Dataset Definition Identity
-   Research Dataset Identity
-   Source State Identity
-   Dataset Snapshot Identity
-   Dataset Version = immutable Snapshot Identity
-   identity scheme `aiq-dataset-identity-v1`
-   SHA-256 represented as exactly 64 lowercase hexadecimal characters
-   canonical semantic representation already implemented/accepted
-   provenance and narrow lineage semantics
-   identity reassignment prohibited
-   contradictory content under the same fingerprint is integrity
    conflict
-   operational metadata excluded from semantic identity

WP07 stores accepted identity values. It does **not** recompute,
reinterpret, shorten, re-hash, or redesign them.

### WP04 --- Application contracts

Application contracts remain storage-independent.

No SQLite type, SQL term, row identifier, storage path, ORM annotation,
or physical schema concept may leak into Domain or Application.

### WP05 --- Materialization

`DatasetSnapshotCandidate` is the accepted semantic input for later
persistence.

WP07 must make every required candidate fact physically representable
without changing materialization behavior.

### WP06 --- Catalog metadata

`DatasetCatalogEntry` is the accepted logical catalog representation.

Preserve:

-   immutable exact snapshot catalog evidence
-   identity-bearing versus deterministic descriptive metadata
    separation
-   Research Dataset Identity distinct from Snapshot Identity / Dataset
    Version
-   exact target/boundaries
-   coverage
-   provenance
-   lineage
-   successful empty snapshot metadata
-   `NewlyRegistered`, `EquivalentExisting`, `IntegrityConflict`
    semantics
-   exact lookup by Snapshot Identity
-   no mutable `latest`
-   no operational metadata
-   no physical metadata in Application contracts

## 6. Existing Release 1.1 SQLite boundary

SQLite is already selected and integrated. WP07 does not reselect the
storage engine.

Inspect repository truth before designing.

Preserve at minimum:

-   existing `historical_observations` table semantics and data
-   existing Release 1.1 identity `(target, instant_utc_ticks)`
-   exact target behavior
-   timestamp/offset/decimal fidelity
-   existing connection factory ownership/lifetime
-   existing schema bootstrap behavior
-   clean-checkout database bootstrap
-   existing persistence/retrieval behavior
-   existing permanent tests

Do not destructively reset an existing Release 1.1 database.

Do not replace the current schema strategy with a generalized migration
framework.

## 7. Core physical-model principle

The physical model must separate:

1.  **Dataset snapshot descriptor/catalog evidence** --- one immutable
    accepted snapshot and its semantic metadata.
2.  **Snapshot observation membership/content** --- zero or more
    selected observations belonging to that exact snapshot.
3.  **Release 1.1 source observations** --- existing accepted
    `historical_observations`, which remain independently authoritative
    source history.

Do not collapse these into one mutable table.

Do not make dataset snapshot existence depend on at least one
observation: **empty snapshots are valid and must be physically
representable**.

## 8. Required physical facts

The model must be capable of losslessly representing, at minimum:

### Snapshot/catalog facts

-   Dataset Snapshot Identity
-   Dataset Version
-   Dataset Definition Identity
-   Research Dataset Identity
-   Source State Identity
-   identity scheme
-   exact target
-   requested `from` instant
-   requested `to` instant
-   selected observation count
-   first selected semantic instant when non-empty
-   last selected semantic instant when non-empty
-   source authority
-   provenance facts required by accepted Application contracts
-   lineage facts required by accepted Application contracts

### Definition facts

The physical representation must preserve the accepted definition
semantics sufficiently to reconstruct the accepted Application
representation, including:

-   exact target
-   exact `[from, to)` semantic boundaries
-   accepted ordering semantics
-   semantic-model/identity-scheme facts that are part of the existing
    contracts

Do not invent provider or operational fields.

### Snapshot observation facts

For every selected observation:

-   snapshot identity association
-   deterministic ordinal or another explicit physical ordering
    mechanism
-   semantic instant identity
-   original offset representation
-   exact decimal price representation

The model must permit exact reconstruction of the ordered
`PriceObservation` sequence.

## 9. Identity representation

All accepted WP03 digest identities are fixed semantic values.

Requirements:

-   store them losslessly
-   preserve lowercase 64-hex validation semantics
-   do not truncate
-   do not reinterpret as database-generated IDs
-   do not replace them with integer surrogate identities
-   do not use wall-clock timestamps as versions
-   do not create mutable version sequences

A physical surrogate may be introduced only if repository evidence
proves it necessary and it cannot become semantic identity. Prefer the
accepted immutable digest identity directly when practical.

Dataset Version and Snapshot Identity are semantically one-to-one. Avoid
storing contradictory independent values unless the physical model has a
constraint that makes disagreement impossible. Prefer a representation
that cannot drift.

## 10. Timestamp and offset representation

Follow the proven Release 1.1 fidelity model unless concrete evidence
requires another compatible representation.

The model must preserve:

-   absolute semantic instant identity/order
-   original `DateTimeOffset` offset representation
-   deterministic reconstruction
-   minute-granular offset validity
-   no local-time or culture dependence

For requested boundaries, preserve their semantic instant and any
Application-contract representation needed for exact reconstruction.

For observation values, reuse the accepted Release 1.1
representation/mapping strategy where appropriate rather than inventing
a second incompatible timestamp model.

## 11. Decimal representation

Do not use SQLite `REAL` for `decimal` values.

Preserve exact .NET decimal semantics without floating-point
intermediates, rounding, truncation, or culture dependence.

Prefer reuse of the accepted Release 1.1 invariant decimal
representation/mapping strategy.

The physical model must support high-precision decimal values and
`decimal.MaxValue` to the extent already supported by accepted Release
1.1 semantics.

## 12. Target representation

Target remains an opaque exact value.

Requirements:

-   no trimming
-   no case folding
-   no parsing
-   no normalization
-   no locale-sensitive comparison
-   exact binary comparison semantics compatible with Release 1.1

If target is duplicated between descriptor and observation-related
physical structures, define how consistency is guaranteed or avoid
unnecessary duplication.

## 13. Empty snapshot representation

A valid successful empty snapshot must be physically representable
without synthetic observations.

Required semantics:

-   snapshot/catalog row exists
-   selected count = 0
-   first actual observation instant absent
-   last actual observation instant absent
-   zero snapshot-observation rows
-   definition/source-state/snapshot identities remain present and
    deterministic
-   provenance and lineage remain reconstructable

Do not use sentinel timestamps, sentinel prices, fake rows, or null
identity values to represent emptiness.

## 14. Snapshot observation ordering and identity

The physical model must make deterministic ascending reconstruction
explicit.

Choose the smallest robust strategy consistent with accepted semantics.

Possible mechanisms include:

-   explicit ordinal constrained to the snapshot; or
-   semantic-instant key/order if it completely captures accepted
    sequence semantics.

Whatever strategy is selected, prove:

-   one snapshot cannot contain duplicate semantic instants
-   retrieval can deterministically reconstruct ascending
    semantic-instant order
-   ordering does not depend on SQLite natural row order
-   cross-snapshot membership cannot leak
-   observation content cannot silently mutate under the same snapshot
    identity

Do not implement WP08 insertion/query behavior.

## 15. Immutability and uniqueness constraints

Design physical constraints that support later immutable persistence.

At minimum evaluate and explicitly decide:

-   primary key for snapshot/catalog evidence
-   uniqueness of Dataset Snapshot Identity
-   relationship between Snapshot Identity and Dataset Version
-   uniqueness of observation membership within a snapshot
-   duplicate semantic-instant prevention within a snapshot
-   count/empty coverage consistency that can reasonably be enforced
    physically
-   foreign-key relationship, if appropriate, between snapshot content
    and descriptor
-   delete/update policy compatibility with immutable evidence

Do not add triggers or complex database logic unless simpler schema
constraints cannot represent the accepted invariants.

WP07 defines compatibility with immutability. WP08 implements write
behavior.

## 16. Catalog exact-lookup support

WP09 will implement exact catalog lookup by Dataset Snapshot Identity.

WP07 must ensure the physical model supports this efficiently and
deterministically.

Add only indexes required by accepted lookup semantics.

Do not add:

-   full-text indexes
-   semantic/vector indexes
-   generalized search indexes
-   ranking
-   mutable latest-version indexes
-   speculative target/time-range discovery indexes unless explicitly
    required by accepted WP06 contracts

If the primary key already supplies exact Snapshot Identity lookup, do
not add a redundant index.

## 17. Provenance and lineage physical representation

Prefer a minimal representation that can reconstruct the accepted
Application `DatasetProvenance` and `DatasetLineage`.

Do not create a generic provenance graph, DAG, event log, or pipeline
lineage system.

The accepted lineage is narrow:

-   one snapshot
-   one definition
-   one relevant source state
-   zero or more accepted selected observations

If some provenance/lineage facts are derivable exactly from other stored
immutable fields, avoid gratuitous duplication. If stored redundantly,
define consistency constraints or mapper validation.

## 18. Physical records and mappings

Infrastructure-owned records/mappers may be introduced as authorized by
the manifest.

Requirements:

-   internal unless repository conventions strongly require otherwise
-   no Infrastructure record leakage into Application/Domain public APIs
-   mapping must be lossless
-   mapping must not recompute semantic identities
-   mapping must not normalize target/timestamps/decimals
-   mapping must reject physically malformed values rather than silently
    repair them
-   physical records should be purpose-specific rather than a
    generalized persistence abstraction

WP07 may use temporary focused probes to prove round-trip fidelity and
schema invariants. Remove all temporary probes before final accounting.

Permanent tests remain WP14-owned unless the manifest explicitly says
otherwise.

## 19. Schema evolution strategy

Release 1.1 currently owns an existing SQLite schema/version bootstrap.
Reconcile its exact implementation before mutation.

WP07 must implement the **smallest deterministic schema evolution**
needed to add Release 1.2 physical structures.

Requirements:

-   preserve `historical_observations`
-   preserve existing Release 1.1 data
-   preserve clean empty-database bootstrap
-   support an existing accepted Release 1.1 schema upgrading to the
    Release 1.2 schema
-   repeated bootstrap at the new version is idempotent
-   reject unsupported future/unknown versions
-   reject incompatible/corrupt expected schema rather than destructive
    reset
-   perform schema transition atomically where the current bootstrap
    design permits
-   no generalized migration framework
-   no downgrade logic
-   no destructive drop/recreate of Release 1.1 data

Choose the next schema version according to existing repository
convention. Do not invent an independent dataset-only version marker if
the repository already has one authoritative SQLite schema marker.

## 20. Bootstrap scenarios that MUST be designed/proved

At minimum validate, preferably with a temporary offline probe if
permanent tests are deferred:

1.  Brand-new database bootstraps with Release 1.1 + Release 1.2 schema.
2.  Existing valid Release 1.1 schema upgrades without losing
    `historical_observations`.
3.  Existing Release 1.1 observation rows survive
    byte/semantic-equivalent retrieval after upgrade.
4.  Repeated bootstrap at the new schema version is idempotent.
5.  Unsupported schema version is rejected.
6.  Incompatible prior schema is not destructively replaced.
7.  Release 1.2 tables/constraints match the selected physical model.
8.  Empty snapshot representation is structurally possible.
9.  Non-empty snapshot records can round-trip through physical mappers
    without SQLite write orchestration.
10. Exact target, identity, timestamp/offset, decimal, coverage,
    provenance, and lineage representation is lossless.
11. Temporary database residue is zero after validation.

Do not implement WP08 snapshot persistence to prove these scenarios.
Schema-level inserts used solely inside a disposable probe are allowed
only if necessary and must not become production behavior.

## 21. Failure-boundary protection

WP11 owns final Dataset validation/failure mapping.

WP07 may throw/reject invalid physical representation during
mapping/bootstrap according to existing Infrastructure conventions, but
must not create the final public failure classifier.

Do not redesign Application failure vocabulary.

Do not add retry/resilience policy.

Do not catch broad `Exception` merely to hide schema/mapping defects.

## 22. WP08 protection

WP08 owns **Dataset Snapshot Persistence**.

WP07 MUST NOT implement:

-   `IDatasetSnapshotStore` concrete behavior
-   snapshot insert orchestration
-   idempotent persistence classification
-   conflict lookup/classification
-   atomic snapshot write transactions as runtime store behavior
-   snapshot retrieval behavior
-   overwrite prevention logic beyond physical constraints/model
    compatibility
-   persistence result mapping

The schema may contain constraints required for those later behaviors,
but WP08 must remain clearly unstarted.

Issue #128 must remain Open / Backlog.

## 23. WP09 protection

WP09 owns **Dataset Catalog Persistence & Lookup**.

WP07 MUST NOT implement:

-   `IDatasetCatalog` concrete persistence
-   catalog registration runtime behavior
-   exact lookup runtime queries
-   found/not-found behavior
-   catalog result/failure mapping
-   generalized search/filtering

The schema may support exact Snapshot Identity lookup, but WP09 must
remain clearly unstarted.

## 24. WP10--WP12 and Release 1.3 protection

Do not implement:

-   materialization-to-persistence integration
-   dataset validation/failure mapping
-   DI registration
-   configuration binding
-   Worker execution
-   scheduling
-   automatic refresh
-   streaming
-   pipelines/DAGs
-   monitoring
-   resilience orchestration
-   Release 1.3 behavior

## 25. Architecture protection

Final production graph must remain:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

Expected WP07:

-   Domain production delta: 0
-   Application production delta: 0
-   Worker production delta: 0
-   Infrastructure production delta: authorized
-   package delta: 0
-   project-reference delta: 0

SQLite/SQL/filesystem physical concepts must remain confined to
Infrastructure.

If Domain, Application, Worker, package, or project-reference delta
becomes non-zero, treat it as a scope alarm and stop unless explicit
authority proves it necessary.

## 26. Security and privacy

No credentials, API keys, tokens, connection secrets, personal machine
paths, or sensitive database contents may be introduced.

Do not log secrets or full connection strings.

Use only synthetic data for probes.

Provider/network calls during WP07 validation must be zero.

Run the repository's canonical security verification unchanged.

## 27. Implementation approach

After all starting gates pass:

1.  Move issue #127 from Backlog to In Progress.
2.  Inspect the exact Release 1.1 SQLite schema/bootstrap
    implementation.
3.  Inspect WP05 candidate and WP06 catalog entry contracts.
4.  Produce a concise pre-code physical-model matrix:
    -   semantic fact
    -   Application owner/type
    -   physical representation
    -   key/constraint
    -   mapper strategy
    -   empty-state behavior
    -   later WP consumer
5.  Produce a schema-evolution matrix:
    -   current schema version/state
    -   target schema version/state
    -   transition
    -   preserved Release 1.1 objects/data
    -   incompatibility behavior
6.  Select the smallest schema satisfying WP05/WP06 semantics.
7.  Implement only manifest-authorized Infrastructure
    records/mappers/schema evolution.
8.  Prefer existing Release 1.1 mapping patterns for
    timestamp/offset/decimal fidelity.
9.  Add only indexes required for exact approved lookup semantics.
10. Do not implement snapshot/catalog runtime persistence.
11. Use temporary offline probes only when needed; remove them
    completely.
12. Re-run every final validation gate.
13. Only after all gates pass, post concise evidence to #127 and close
    it Done.

## 28. Required physical-model decision matrix

The execution report must explicitly state the chosen representation and
rationale for:

-   schema version
-   snapshot/catalog table(s)
-   snapshot observation table
-   primary keys
-   foreign keys
-   identity storage
-   Dataset Version representation
-   target collation
-   requested boundary representation
-   actual coverage-bound representation
-   selected count representation
-   source authority representation
-   provenance representation
-   lineage representation
-   observation ordinal/order strategy
-   semantic instant representation
-   original offset representation
-   decimal representation
-   empty snapshot representation
-   uniqueness constraints
-   exact lookup index strategy
-   update/delete/overwrite compatibility
-   mapper ownership
-   bootstrap/evolution strategy

Do not leave any of these to WP08/WP09 if they are required to interpret
the schema.

## 29. Required validation gates

After implementation, run at minimum:

-   restore
-   format verification
-   build
-   Domain.Tests
-   Application.Tests
-   Infrastructure.Tests
-   Architecture.Tests
-   `eng/verify.ps1`
-   `git diff --check`
-   `git diff --cached --check`

For untracked authorized WP07 files, perform direct whitespace
validation because ordinary `git diff --check` does not inspect
untracked files.

Also verify:

-   build warnings = 0
-   build errors = 0
-   skipped tests = 0 unless pre-existing and explicitly explained
-   package vulnerability/security verification passes
-   provider/network calls = 0
-   temporary probe residue = 0
-   temporary database residue = 0
-   Domain/Application SQLite leakage = 0
-   Release 1.1 persistence regression = PASS
-   Release 1.3 implementation = 0

## 30. Required focused physical validation

Even if permanent tests are deferred to WP14, WP07 must obtain
sufficient evidence for the selected model.

At minimum prove:

-   physical round-trip of all four identities
-   Dataset Version / Snapshot Identity consistency
-   exact target including case/whitespace
-   requested boundary fidelity
-   successful empty snapshot representation
-   non-empty coverage representation
-   source authority fidelity
-   provenance/lineage reconstruction
-   timestamp semantic instant + original offset fidelity
-   high-precision decimal fidelity
-   deterministic observation ordering representation
-   duplicate semantic instant cannot be represented as valid membership
-   same Research Dataset Identity can coexist with distinct Snapshot
    Identities
-   contradictory same-Snapshot-Identity physical evidence cannot
    silently overwrite
-   exact Snapshot Identity lookup is structurally supported
-   Release 1.1 `historical_observations` survives schema evolution
-   repeated bootstrap is idempotent

If proving any row-level property would require implementing WP08/WP09
production behavior, use a disposable schema/mapping probe rather than
crossing the work-package boundary.

## 31. Git and GitHub protection

WP07 may mutate only:

-   manifest-authorized Infrastructure files
-   issue #127 lifecycle/evidence

Do NOT:

-   stage files
-   commit
-   push
-   create/switch integration branches
-   open a PR
-   merge
-   tag
-   release
-   rewrite history

Do not modify #128 or later issues.

After every acceptance gate passes:

1.  post concise evidence to #127;
2.  close #127;
3.  set its Project status to Done;
4.  leave #128 Open / Backlog.

## 32. Required final report

Produce a structured execution report containing at least:

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
12. WP05 Candidate Reconciliation
13. WP06 Catalog Reconciliation
14. Release 1.1 SQLite Reconciliation
15. Physical Model Design
16. Schema Version Decision
17. Snapshot/Catalog Table Design
18. Snapshot Observation Table Design
19. Identity/Version Representation
20. Target Representation
21. Boundary/Coverage Representation
22. Provenance/Lineage Representation
23. Observation Ordering Strategy
24. Timestamp/Offset Representation
25. Decimal Representation
26. Empty Snapshot Representation
27. Keys/Constraints/Indexes
28. Immutability Compatibility
29. Schema Evolution / Bootstrap Strategy
30. Exact Files Added/Modified
31. Domain Delta
32. Application Delta
33. Infrastructure Delta
34. Worker Delta
35. Package/Reference Delta
36. Permanent Test Delta
37. Temporary Probe Evidence
38. WP08 Protection
39. WP09 Protection
40. WP10--WP12 / Release 1.3 Protection
41. Security/Offline Evidence
42. Whitespace/Diff Evidence
43. Restore/Build Evidence
44. Permanent Test Evidence
45. Canonical Verification
46. Architecture Validation
47. Release 1.1 Persistence Regression
48. Physical Validation Matrix
49. Mutation Accounting
50. Git/GitHub Protection
51. Planning Protection
52. Findings/Blockers
53. Final Repository/GitHub State
54. WP08 Handoff
55. Final Decision
56. Next Authorized Work Package

## 33. Acceptance criteria

WP07 is complete only if all are true:

-   #125 and #126 predecessor gates pass.
-   issue #127 lifecycle is correctly executed.
-   accepted WP02--WP06 semantics remain unchanged.
-   Release 1.1 `historical_observations` behavior/data are preserved.
-   SQLite remains the storage engine.
-   minimum Infrastructure-owned dataset/catalog physical model is
    defined.
-   every accepted identity/version value is losslessly representable.
-   exact target is losslessly representable.
-   requested boundaries and coverage are losslessly representable.
-   provenance and narrow lineage are reconstructable without a
    competing model.
-   snapshot observations preserve semantic instant, original offset,
    decimal value, and deterministic order.
-   successful empty snapshots are physically representable without
    sentinel observations.
-   uniqueness/constraints support immutable snapshot semantics.
-   exact Snapshot Identity lookup is structurally supported without
    generalized search.
-   schema evolution supports clean bootstrap and upgrade from accepted
    Release 1.1 state.
-   schema evolution is non-destructive and idempotent.
-   unsupported/incompatible schema is rejected rather than reset.
-   no generalized migration framework is introduced.
-   no WP08 snapshot persistence behavior is implemented.
-   no WP09 catalog persistence/lookup behavior is implemented.
-   no WP10--WP12 or Release 1.3 behavior is implemented.
-   Domain/Application remain free of SQLite/SQL/filesystem physical
    concepts.
-   Domain/Application/Worker production delta is zero.
-   package/reference delta is `0/0`.
-   permanent tests remain unchanged unless explicit manifest authority
    proves otherwise.
-   all validation gates pass.
-   temporary probe/database residue is zero.
-   issue #127 ends Closed / Done.
-   issue #128 remains Open / Backlog.
-   working tree remains uncommitted and unstaged.

## 34. WP08 handoff

The final report must give WP08 an exact implementation handoff
containing:

-   target schema version
-   exact table names
-   exact column names/types/nullability
-   primary/foreign/unique/check constraints
-   exact identity representation
-   exact Dataset Version representation
-   target collation
-   timestamp/offset representation
-   decimal representation
-   observation ordering/membership representation
-   empty snapshot representation
-   mapper/record types to reuse
-   bootstrap guarantees
-   indexes available
-   explicit statement that WP08 must not redesign the physical model
    unless a proven blocker requires new authority

WP08 owns runtime snapshot persistence, transaction semantics,
idempotency/conflict classification, and durable reconstruction.

## 35. Terminal marker

On success, end exactly with:

RELEASE 1.2 WP07 COMPLETE

DATASET PHYSICAL STORAGE MODEL: WP02 dataset semantics preserved: PASS
WP03 identity/version/provenance semantics preserved: PASS WP04
Application contracts preserved: PASS WP05 materialization candidate
representable: PASS WP06 catalog metadata representable: PASS Release
1.1 historical_observations preserved: PASS SQLite storage engine
preserved: PASS Schema evolution: PASS Clean bootstrap: PASS Release 1.1
→ Release 1.2 upgrade: PASS Snapshot/catalog physical model: PASS
Snapshot observation physical model: PASS Four identity representations:
PASS Dataset Version / Snapshot Identity consistency: PASS Exact target
representation: PASS Requested boundary representation: PASS Coverage
representation: PASS Provenance representation: PASS Lineage
representation: PASS Successful empty snapshot representation: PASS
Timestamp/offset fidelity: PASS Decimal fidelity: PASS Deterministic
observation-order representation: PASS Immutable-history compatibility:
PASS Exact Snapshot Identity lookup support: PASS Generalized migration
framework introduced: NO Snapshot persistence implemented: NO Catalog
persistence/lookup implemented: NO Domain SQLite/SQL leakage: 0
Application SQLite/SQL leakage: 0 Domain delta: 0 Application delta: 0
Worker delta: 0 Package/reference delta: 0/0 Permanent test delta:
`<report>`{=html} Temporary SQLite residue: 0 WP08 started: NO Release
1.3 implementation started: NO Issue #127: CLOSED / DONE

NEXT AUTHORIZED WORK PACKAGE: WP08 --- Dataset Snapshot Persistence
GitHub issue #128

If blocked, do not emit the success marker. Emit
`RELEASE 1.2 WP07 BLOCKED` and state the smallest corrective authority
required.
