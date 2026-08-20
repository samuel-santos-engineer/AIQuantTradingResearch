# Release 1.2 Execution Plan

## 1. Release Identity

**Release:** 1.2\
**Phase:** Phase 3\
**Title:** Research Dataset Foundation

Authoritative milestone title:

``` text
Phase 3 - Release 1.2: Research Dataset Foundation
```

Release 1.2 begins only after `RELEASE 1.1 CLOSED`. The accepted Release
1.1 merged-main state is the predecessor platform baseline. Legacy
milestone `#43 — Phase 3 - Release 1.2: Storage` is historical only and
must remain closed and empty.

## 2. Release Objective

Transform the durable historical-observation foundation established by
Release 1.1 into deterministic, versioned, reproducible, and
discoverable research datasets with provider-independent metadata,
provenance, lineage, coverage, immutable snapshot semantics, and a
minimal catalog boundary.

``` text
Release 1.0: Acquire
→ Release 1.1: Persist
→ Release 1.2: Dataset
→ Release 1.3: Pipeline
→ Release 1.4: Features
```

Release 1.2 proves a reusable research-data asset. It does not implement
the general pipeline engine.

## 3. Accepted Predecessor Baseline

WP01 must reconcile the formally accepted Release 1.1 closure baseline:

``` text
Release 1.1 terminal = RELEASE 1.1 CLOSED
PR #120 = MERGED
Milestone #52 = CLOSED
Issues #103–#118 = 16/16 Closed / Done
Permanent tests = 145/145
Architecture.Tests = 13/13
Build warnings/errors = 0/0
Canonical verification = PASS
Active Release 1.2 planning = 0
```

If repository truth has legitimately advanced since closure, WP01
records the current accepted baseline rather than forcing historical
counts, provided no unauthorized Release 1.2 implementation exists.

## 4. Architectural Invariants

``` text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

-   Domain remains independent of SQLite, SQL, ORM, provider transport,
    filesystem mechanics, catalog storage, and operational
    configuration.
-   Application owns provider/storage-independent dataset contracts,
    materialization orchestration, catalog abstractions, and approved
    result/failure vocabulary.
-   Infrastructure owns physical dataset/catalog representation, SQLite
    mechanics, durable persistence, mapping, queries, and concrete
    failure handling.
-   Worker remains a bounded composition/execution boundary.
-   Release 1.1 historical-observation persistence/retrieval is reused,
    not redesigned without evidence.
-   Dataset identity/version/provenance/lineage must not depend on
    physical row identifiers.
-   No new production project is authorized by default.
-   Production cycles remain zero.
-   Release 1.3 pipeline responsibilities must not leak into Release
    1.2.

## 5. In Scope

Release 1.2 includes repository preflight; dataset/reproducibility
definition; dataset identity/version/provenance/lineage/coverage
semantics; deterministic observation selection; Application
dataset/materialization/catalog contracts; materialization use case;
minimal catalog model; SQLite physical dataset/catalog model; durable
immutable snapshot persistence; catalog registration/lookup;
materialization integration; bounded failure mapping; DI/configuration;
one bounded production execution; permanent Domain/Application and
Infrastructure tests; architecture/documentation alignment; technical
acceptance; governed integration; human merge; and post-merge closure.

## 6. Explicitly Out of Scope

No streaming/WebSockets, event buses, generalized ingestion pipelines,
pipeline orchestration, DAG/workflow engine, scheduler, pipeline
monitoring, generalized validation/transformation/enrichment stages,
feature generation, technical indicators, ML feature store, backtesting,
strategy/portfolio/trading execution, generalized caching, data lake,
cloud database/object storage, TimescaleDB/PostgreSQL migration,
distributed storage, retention automation, generalized migration
platform, semantic/AI catalog search, notebooks, UI/API product surface,
AI/ML, MLOps, plugin framework, Release 1.3 implementation, or Release
1.4 implementation.

## 7. Design Principles

1.  Semantics before storage mechanics.
2.  Equivalent accepted inputs plus equivalent materialization
    definition produce deterministic logical dataset identity/content.
3.  Accepted snapshots are immutable.
4.  Versioning is semantic, never an accidental database row ID.
5.  Provenance/lineage is bounded to what is needed for reproducibility.
6.  Cataloging is minimal deterministic registration/lookup, not
    generalized search.
7.  Release 1.1 persistence/retrieval is reused.
8.  Domain/Application remain storage independent.
9.  Canonical tests are offline and deterministic.
10. A bounded materialization use case must not become a pipeline
    runtime.
11. Implement the smallest credible vertical slice.
12. WP16 acceptance, merge, and post-merge closure remain distinct
    gates.

## 8. Authoritative Work-Package Sequence

Exactly sixteen work packages:

``` text
WP01 — Release & Repository Preflight
WP02 — Research Dataset Definition & Reproducibility Model
WP03 — Dataset Identity, Version & Provenance Semantics
WP04 — Application Dataset Contracts
WP05 — Dataset Materialization Use Case
WP06 — Dataset Metadata & Catalog Model
WP07 — Dataset Physical Storage Model
WP08 — Dataset Snapshot Persistence
WP09 — Dataset Catalog Persistence & Lookup
WP10 — Dataset Materialization Integration
WP11 — Dataset Validation & Failure Mapping
WP12 — Dependency Registration & Bounded Dataset Execution
WP13 — Domain & Application Dataset Tests
WP14 — Infrastructure & Dataset Tests
WP15 — Architecture & Documentation Alignment
WP16 — Full Validation, Integration & Acceptance
```

No WP17+ implementation package is authorized.

## 9. Authoritative Dependency Graph

  WP     Depends On
  ------ ------------------------------------
  WP01   Release 1.1 CLOSED
  WP02   WP01
  WP03   WP02
  WP04   WP03
  WP05   WP04
  WP06   WP03, WP04
  WP07   WP05, WP06
  WP08   WP07
  WP09   WP08
  WP10   WP05, WP08, WP09
  WP11   WP10
  WP12   WP11
  WP13   WP03, WP04, WP05, WP06
  WP14   WP07, WP08, WP09, WP10, WP11, WP12
  WP15   WP13, WP14
  WP16   WP15

GitHub issue dependencies must match exactly. Missing or artificial
edges are planning drift.

## 10. WP01 --- Release & Repository Preflight

**Objective:** establish the exact Release 1.2 starting state.

Verify Release 1.1 closure; PR #120 merged; local `main` synchronized;
milestone #52 terminal; legacy #43 closed/empty; canonical
verification/test baseline; production graph; Release 1.1 persistence
surfaces; existing dataset/catalog concepts; no unauthorized 1.2
implementation; no 1.3 leakage.

**Mutation:** governance/report artifacts only if the file manifest and
WP01 prompt authorize them.

**Prohibited:** production/test code, contracts, schema, packages, DI,
Worker behavior, Git transport, WP02 implementation.

**Exit:** baseline reconciled with zero unexplained implementation
drift.

## 11. WP02 --- Research Dataset Definition & Reproducibility Model

**Objective:** define a Release 1.2 research dataset and reproducibility
before contracts/storage.

Decide dataset vs observation series; snapshot meaning; source-selection
boundary; deterministic materialization definition; reproducibility
rule; minimum metadata; provenance/lineage; coverage; immutability;
logical identity vs snapshot/version identity; and the boundary with
Release 1.3.

Produce at least one evidence-backed data-architecture
decision/definition artifact.

**Prohibited:** source/test implementation, contracts, SQLite, packages,
DI, Worker, WP03+ implementation.

**Exit:** one coherent model is precise enough for later WPs.

## 12. WP03 --- Dataset Identity, Version & Provenance Semantics

**Objective:** define provider/storage-independent identity,
snapshot/version, provenance, lineage, coverage, determinism,
immutability, and empty-selection semantics.

WP03 must determine whether Domain changes are necessary. Domain delta
`0` is valid and preferred when existing values suffice.

**Prohibited:** SQLite, physical records, Infrastructure, DI, Worker,
tests, pipeline runtime, feature engineering.

**Exit:** all later semantic decisions are explicit and technology
independent.

## 13. WP04 --- Application Dataset Contracts

**Objective:** create the minimum provider/storage-independent
Application seam.

Contracts may include dataset definition/request, snapshot/descriptor,
identity/version, provenance/lineage/coverage, materialization
result/failure, snapshot store abstraction, catalog registration/lookup
abstraction, and deterministic lookup criteria.

Application must not expose SQLite/SQL/ORM, filesystem paths,
database-generated semantic IDs, storage exceptions, provider DTOs, or
pipeline types.

**Exit:** minimal technology-independent contract surface exists.

## 14. WP05 --- Dataset Materialization Use Case

**Objective:** deterministically turn a materialization request plus
persisted historical observations into a provider/storage-independent
dataset snapshot candidate.

``` text
request
→ historical retrieval
→ deterministic selection/materialization
→ snapshot/descriptor candidate
→ explicit result
```

Reuse Release 1.1 retrieval and preserve observation fidelity.

**Prohibited:** physical storage, catalog persistence, Worker,
scheduler/DAG, feature generation.

**Exit:** deterministic Application materialization works without
storage leakage.

## 15. WP06 --- Dataset Metadata & Catalog Model

**Objective:** define the minimum catalogable metadata model.

Reconcile logical identity, snapshot/version, target/instrument
coverage, temporal coverage, bounded count/coverage, provenance,
lineage/materialization-definition reference, and only necessary
lifecycle metadata.

The catalog is not semantic search, a marketplace, enterprise metadata
platform, AI discovery, or generalized indexing framework.

**Exit:** a minimal discoverable descriptor is compatible with
WP03--WP05.

## 16. WP07 --- Dataset Physical Storage Model

**Objective:** define the minimum Infrastructure-owned durable
dataset/catalog representation.

Reuse Release 1.1 SQLite by default. Any schema evolution must preserve
`historical_observations`, existing data, clean-checkout bootstrap, and
avoid a generalized migration framework.

Physical records/SQLite remain Infrastructure-only.

**Exit:** durable model represents accepted semantics without changing
Release 1.1 observation meaning.

## 17. WP08 --- Dataset Snapshot Persistence

**Objective:** implement durable immutable snapshot persistence.

Persist identity/descriptor, preserve
version/provenance/lineage/coverage fidelity, implement approved
idempotency/conflict behavior, prevent silent overwrite, preserve
atomicity, and survive component reconstruction.

**Prohibited:** cache/pipeline/feature/catalog-search frameworks or new
storage engine.

**Exit:** snapshots are durably and immutably stored.

## 18. WP09 --- Dataset Catalog Persistence & Lookup

**Objective:** implement durable minimal catalog registration and
deterministic lookup.

Preserve metadata/provenance/lineage/coverage fidelity and approved
empty/not-found behavior.

**Prohibited:** semantic/full-text/AI search, ranking, generalized
indexing, pipelines.

**Exit:** catalog descriptors can be registered and retrieved
deterministically.

## 19. WP10 --- Dataset Materialization Integration

**Objective:** integrate materialization, snapshot persistence, and
catalog registration.

``` text
request
→ historical retrieval
→ deterministic materialization
→ durable snapshot
→ catalog registration
→ provider-independent result
```

Define an explicit consistency/atomicity boundary and prevent partial
accepted state.

**Prohibited:** scheduler, background queue, pipeline runtime,
transformation/enrichment framework, feature generation.

**Exit:** full dataset-foundation use case works through approved
abstractions.

## 20. WP11 --- Dataset Validation & Failure Mapping

**Objective:** map concrete dataset/catalog storage conditions into
approved provider-independent failures.

Cover unavailable storage, incompatible schema, malformed persisted
metadata, identity/content conflict, invalid lineage/coverage
representation, corruption, and partial-state prevention. Do not swallow
programming defects or invent a generalized data-quality/retry
framework.

**Exit:** failure behavior is deterministic and storage-independent at
the Application boundary.

## 21. WP12 --- Dependency Registration & Bounded Dataset Execution

**Objective:** wire the dataset graph into the existing composition
model and prove one bounded production path.

Allowed:

``` text
one explicit materialization
→ retrieve persisted observations
→ materialize
→ persist snapshot
→ register catalog
→ report result
```

Not allowed: scheduler, continuous loop, DAG/workflow engine, background
orchestration, generic stage framework, feature pipeline.

**Exit:** the real composition root executes the vertical slice while
Release 1.3 remains absent.

## 22. WP13 --- Domain & Application Dataset Tests

Permanently prove technology-independent semantics/contracts/use cases:
identity/version, reproducibility, provenance/lineage, coverage, empty
selection, request validation, deterministic materialization,
observation fidelity, result/failure propagation, catalog contracts, and
separation from provider/SQLite/pipeline mechanics.

Domain test delta may be `0` if WP03 changes no Domain behavior.

Tests are offline and deterministic.

## 23. WP14 --- Infrastructure & Dataset Tests

Permanently prove SQLite schema evolution and Release 1.1 preservation;
snapshot mapping/persistence; idempotency/conflict; immutability;
atomicity; catalog registration/lookup;
metadata/provenance/lineage/coverage fidelity; materialization
integration; failure mapping; DI/configuration; reconstruction
durability; and database cleanup.

All tests are isolated/offline and leave zero repository database
residue.

## 24. WP15 --- Architecture & Documentation Alignment

Align executable architecture rules and current-state documentation with
accepted implementation. Reconcile data platform
vision/lifecycle/catalog/storage/pipeline docs plus
solution/design/DI/testing/project-structure/README where repository
truth requires.

Documentation must clearly show:

``` text
historical observations
→ reproducible research dataset snapshot
→ cataloged reusable asset
→ future Release 1.3 pipelines
→ future Release 1.4 features
```

No production/package/project/build/script changes.

## 25. WP16 --- Full Validation, Integration & Acceptance

Reconcile the exact candidate and validate restore, format, build, all
tests, architecture tests, canonical verification, whitespace,
docs/links, security/package state, dataset reproducibility,
immutability, catalog behavior, Release 1.1 regression, Release 1.3
leakage=0, temporary residue=0, and fresh-checkout reproducibility.

The separately authored WP16 authority may grant exact integration
branch/stage/commit/push/PR mutations after candidate reconciliation.
WP16 must not merge or self-approve.

If an earlier-WP defect is found, return `BLOCKED` and request a narrow
correction.

## 26. Reproducibility Contract

``` text
same accepted historical observation set
+ same materialization definition
+ same dataset semantics
= same logical dataset content/identity outcome
```

Reproducibility must not depend on row insertion order, SQLite-generated
IDs, machine paths, locale, timezone, network availability, developer
state, or untracked files.

## 27. Dataset Immutability Contract

Accepted snapshot content is immutable. Equivalent rematerialization
follows the approved idempotency rule. Changed source
selection/definition produces an explicit new or conflicting semantic
outcome. Infrastructure must not invent update-in-place behavior.

## 28. Provenance / Lineage Boundary

Release 1.2 must be able to explain what logical dataset/snapshot this
is, what source target/coverage contributed, what materialization
definition produced it, what historical-observation boundary it derives
from, and whether it can be reproduced.

No enterprise lineage graph/ontology/cross-system metadata platform is
required.

## 29. Catalog Boundary

The catalog describes/references dataset assets and supports
deterministic registration/lookup. It does not replace Release 1.1
historical storage. No generalized search technology is selected without
separate evidence/authority.

## 30. Release 1.1 Regression Contract

Preserve exact target identity, timestamp/offset/decimal fidelity,
immutable historical observations, duplicate idempotency, deterministic
conflict, atomic writes, ascending retrieval, successful empty
retrieval, failure behavior, `Persistence:DatabasePath`, operation-owned
connections, provider/storage independence, and bounded Worker
composition.

Any public Release 1.1 contract evolution must be explicitly
proven/governed by WP03/WP04 or treated as a blocker.

## 31. Release 1.3 Protection

Release 1.2 must not introduce abstractions primarily for arbitrary
stage composition, stage graphs, scheduling, recurring execution,
pipeline lifecycle/monitoring, generalized
validation/transformation/enrichment stages, or feature-generation
orchestration.

## 32. Security / Data Safety

No real credentials, secret-bearing connection strings, production
market-data fixtures without authority, committed SQLite/WAL/SHM/journal
files, personal machine paths, destructive historical-data reset, silent
dataset overwrite, or secret-bearing catalog metadata.

## 33. Working-Tree Discipline

Until WP16 explicitly owns integration: no commits, pushes,
implementation PRs, branch proliferation, or destructive reset/stash. No
staging unless a WP authority explicitly requires it. Preserve
cumulative accepted work. Unexpected mutations are blockers.

## 34. GitHub Planning Model

Before WP01, planning must represent one new authoritative milestone
titled `Phase 3 - Release 1.2: Research Dataset Foundation`, exactly 16
WP issues, the exact dependency graph, no WP17+, existing labels/Project
conventions, Project Release=`1.2` reconciliation if required, legacy
#43 closed/empty, and no Release 1.3 implementation.

Project #2 field truth must be inspected, not assumed.

## 35. Standard Issue Body Contract

``` text
Objective
Scope
Dependencies
Deliverables
Validation Evidence
Exit Criteria
Out of Scope
Authority
```

## 36. Required Governance Before WP01

``` text
docs/roadmap/release-1.2/RELEASE_1.2_EXECUTION_PLAN.md
docs/roadmap/release-1.2/RELEASE_1.2_FILE_MANIFEST.md
docs/roadmap/release-1.2/prompts/release-1.2-github-planning-codex-prompt.md
docs/roadmap/release-1.2/prompts/release-1.2-github-planning-codex-prompt-chat.md
```

Standard `-chat` companions are exactly five lines. WP01 prompt
artifacts are created only after GitHub planning is accepted.

## 37. Validation Rules

Every implementation WP must classify Git state, verify
predecessors/issue lifecycle, run relevant baseline and targeted
validation, run canonical verification before completion, pass
`git diff --check`, prove unexpected paths=0, remove temporary residue,
report package/reference/test deltas, and preserve later-WP scope.

## 38. Temporary Probe Policy

Temporary probes must be minimal, offline unless explicitly authorized
otherwise, uncommitted, removed before completion, residue-free, and
never substitute for required WP13/WP14 permanent tests.

## 39. Package / Project Policy

Default:

``` text
new production packages = 0
new production projects = 0
new project references = 0
```

Expected references remain Application→Domain,
Infrastructure→Application, Worker→Application+Infrastructure. Any
package addition requires explicit evidence and authority.

## 40. Candidate Accounting

Do not hardcode the final candidate count. At WP16 derive the accepted
governance + WP01--WP15 artifacts + explicitly in-band WP16 artifacts,
then freeze candidate `N`. Later lifecycle authorities must state
whether their files are in-band or out-of-band.

## 41. Integration / Merge Gate

Under explicit authority WP16 may create one governed integration
branch/commit/push/review-ready PR. No self-approval, auto-merge, merge,
force-push, or history rewrite.

## 42. Post-Merge Closure

A separate closure authority must prove the accepted candidate on
`main`, local/remote synchronization, canonical validation,
fresh-checkout reproducibility, all WP issues Closed/Done, authoritative
milestone with zero open issues then closed, clean working tree, and
Release 1.3 not started.

Only then:

``` text
RELEASE 1.2 CLOSED
```

## 43. Release-Level Exit Criteria

Technical acceptance requires deterministic
identity/version/reproducibility; explicit provenance/lineage/coverage;
storage-independent contracts; deterministic materialization; durable
immutable snapshots; explicit equivalent-rerun behavior; durable catalog
registration; deterministic lookup; fidelity; Release 1.1 regression
PASS; SQLite confined to Infrastructure; Domain/Application SQLite
leakage=0; pipeline/feature implementation=0; permanent/architecture
tests PASS; canonical verification PASS; documentation aligned;
residue=0; fresh checkout PASS; candidate reconciled; review-ready PR
under authority.

## 44. Authority Transition

``` text
Release 1.2 Definition
→ Execution Plan + File Manifest
→ GitHub Planning
→ WP01 ... WP16
→ RELEASE 1.2 ACCEPTED
→ Human Review / Merge
→ Post-Merge Closure
→ RELEASE 1.2 CLOSED
→ Release 1.3 governance may be separately authorized
```

## 45. Final Governance Decision

This plan defines exactly one capability:
`Phase 3 - Release 1.2: Research Dataset Foundation`.

It authorizes preparation of Release 1.2 GitHub planning and, only after
that planning is accepted, WP01--WP16 execution under separately
authored prompts. It does not itself mutate GitHub planning, implement
Release 1.2, authorize early Git transport, authorize merge, or
authorize Release 1.3 implementation.
