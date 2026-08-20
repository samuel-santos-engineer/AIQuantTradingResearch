# Release 1.2 File Manifest

## 1. Purpose

This manifest defines authoritative file-ownership and mutation
boundaries for:

``` text
Phase 3 - Release 1.2: Research Dataset Foundation
```

It prevents uncontrolled file scope while allowing concrete
implementation filenames to emerge from evidence-backed design. The
execution plan is authoritative for behavior/sequencing; this manifest
is authoritative for artifact classes, allowed mutation surfaces, and
prohibited file categories.

## 2. Manifest Principles

1.  Do not invent implementation filenames before the owning WP
    establishes design.
2.  Modify only surfaces owned here or by a later narrow
    unblock/reconciliation authority.
3.  Existing files may evolve only when their responsibility
    legitimately evolves under the owning WP.
4.  New files remain inside the owning layer/module/documentation
    surface.
5.  Application owns provider/storage-independent
    dataset/materialization/catalog contracts and orchestration.
6.  Infrastructure owns SQLite physical dataset/catalog representation
    and persistence mechanics.
7.  Release 1.1 historical-observation behavior is preserved unless an
    owning WP explicitly proves compatible evolution.
8.  Domain changes are allowed only if WP03 proves genuinely
    Domain-owned dataset semantics are necessary.
9.  Worker changes are owned only by WP12 bounded composition/execution.
10. Permanent test changes belong to WP13/WP14; architecture-test
    changes belong to WP15.
11. Governance prompts belong under `docs/roadmap/release-1.2/prompts/`.
12. No Release 1.3 implementation artifact is authorized.
13. Final candidate counts are derived after reconciliation, never
    hardcoded now.
14. Temporary SQLite/probe/generated artifacts never silently become
    candidate files.
15. Legacy milestone #43 is historical GitHub state and does not
    authorize obsolete repository naming.

## 3. Authoritative Governance Paths

``` text
docs/roadmap/release-1.2/
    RELEASE_1.2_EXECUTION_PLAN.md
    RELEASE_1.2_FILE_MANIFEST.md
    prompts/
```

Standard WP prompt naming:

``` text
NN-<work-package-slug>-codex-prompt.md
NN-<work-package-slug>-codex-prompt-chat.md
```

Lifecycle prompt naming:

``` text
release-1.2-<lifecycle-step>-codex-prompt.md
release-1.2-<lifecycle-step>-codex-prompt-chat.md
```

Every standard `-chat` companion is exactly five lines unless explicit
later authority changes the convention.

## 4. Required Pre-Implementation Governance

Before WP01:

``` text
docs/roadmap/release-1.2/RELEASE_1.2_EXECUTION_PLAN.md
docs/roadmap/release-1.2/RELEASE_1.2_FILE_MANIFEST.md
docs/roadmap/release-1.2/prompts/release-1.2-github-planning-codex-prompt.md
docs/roadmap/release-1.2/prompts/release-1.2-github-planning-codex-prompt-chat.md
```

WP01 prompt artifacts are created only after GitHub planning is
accepted.

# 5. WP01 --- Release & Repository Preflight

**Prompt pair**

``` text
01-release-repository-preflight-codex-prompt.md
01-release-repository-preflight-codex-prompt-chat.md
```

under the Release 1.2 prompt root.

**Authorized mutation:** normally governance/report artifacts only.

**Prohibited unless narrow unblock:** `src/**`, `tests/**`,
package/build/solution files, `eng/**`, `.github/**`.

# 6. WP02 --- Research Dataset Definition & Reproducibility Model

**Prompt pair**

``` text
02-research-dataset-definition-reproducibility-model-codex-prompt.md
02-research-dataset-definition-reproducibility-model-codex-prompt-chat.md
```

**Authorized surface**

``` text
docs/architecture/data/**
```

Prefer existing data-architecture authorities where natural. A dedicated
definition/decision artifact may be created under that folder. Concrete
filename(s) are evidence-driven.

**Prohibited:** `src/**`, `tests/**`,
packages/projects/solution/build/scripts/GitHub config. WP02 is
definition/decision-only.

# 7. WP03 --- Dataset Identity, Version & Provenance Semantics

**Prompt pair**

``` text
03-dataset-identity-version-provenance-semantics-codex-prompt.md
03-dataset-identity-version-provenance-semantics-codex-prompt-chat.md
```

**Primary expected production outcome:** Domain delta may be `0`.

If required:

``` text
src/AIQuantTradingResearch.Domain/**
```

may change only for provider/storage-independent dataset
identity/version/provenance/lineage/coverage semantics.

Narrow semantic documentation under `docs/architecture/data/**` may be
updated only when explicitly authorized.

**Prohibited:** Infrastructure physical types, SQLite/schema, Worker,
packages/projects, tests, pipeline/feature types. Application contracts
remain WP04-owned.

# 8. WP04 --- Application Dataset Contracts

**Prompt pair**

``` text
04-application-dataset-contracts-codex-prompt.md
04-application-dataset-contracts-codex-prompt-chat.md
```

**Authorized production surface**

``` text
src/AIQuantTradingResearch.Application/**
```

Allowed classes: dataset definition/request; snapshot/descriptor;
identity/version supporting values where Domain does not own them;
provenance/lineage/coverage; materialization result/failure;
snapshot-store abstraction; catalog registration/lookup abstraction;
minimal supporting Application values.

**Prohibited:** Infrastructure/Worker, SQLite/SQL/ORM/filesystem types
in public contracts, provider DTOs, pipeline runtime types, tests, new
packages.

# 9. WP05 --- Dataset Materialization Use Case

**Prompt pair**

``` text
05-dataset-materialization-use-case-codex-prompt.md
05-dataset-materialization-use-case-codex-prompt-chat.md
```

**Authorized**

``` text
src/AIQuantTradingResearch.Application/**
```

for deterministic materialization orchestration, request validation,
Release 1.1 historical retrieval reuse, and snapshot-candidate
construction.

**Prohibited:** Infrastructure, Worker, SQLite/schema, catalog
persistence, permanent tests, packages/projects, scheduler/DAG, feature
generation.

# 10. WP06 --- Dataset Metadata & Catalog Model

**Prompt pair**

``` text
06-dataset-metadata-catalog-model-codex-prompt.md
06-dataset-metadata-catalog-model-codex-prompt-chat.md
```

**Authorized primary surface**

``` text
src/AIQuantTradingResearch.Application/**
```

Narrow `docs/architecture/data/**` refinement is allowed when explicitly
authorized. Domain may change only as a compile-level continuation of
WP03's already accepted Domain semantic decision.

Allowed classes: catalogable descriptor values/contracts; coverage;
provenance/lineage; deterministic lookup criteria; minimal catalog-model
refinements.

**Prohibited:** Infrastructure persistence, SQLite, Worker, tests,
semantic/AI/full-text search platform, generalized indexing, pipeline
types.

# 11. WP07 --- Dataset Physical Storage Model

**Prompt pair**

``` text
07-dataset-physical-storage-model-codex-prompt.md
07-dataset-physical-storage-model-codex-prompt-chat.md
```

**Authorized**

``` text
src/AIQuantTradingResearch.Infrastructure/**
```

Allowed classes: snapshot records; catalog records; mappings; schema
representation/version evolution; SQLite constraints/indexes required by
approved lookup semantics.

Existing Release 1.1 SQLite bootstrap/schema files may change only to
add Release 1.2 schema while preserving `historical_observations`
behavior/data.

Default production package delta is `0`. Additional packages require
exact separate authority unless the WP07 prompt explicitly grants them
after evidence.

**Prohibited:** Domain physical records, Application SQLite types,
Worker, permanent tests, new storage engine, generalized migration
framework, destructive Release 1.1 reset.

# 12. WP08 --- Dataset Snapshot Persistence

**Prompt pair**

``` text
08-dataset-snapshot-persistence-codex-prompt.md
08-dataset-snapshot-persistence-codex-prompt-chat.md
```

**Authorized**

``` text
src/AIQuantTradingResearch.Infrastructure/**
```

for snapshot persistence, immutable writes, identity/version
enforcement, idempotency/conflict behavior, transactions/atomicity, and
mapping.

**Prohibited:** Application redesign, Worker, Domain, permanent tests,
new packages unless separately authorized, catalog-search framework,
pipeline behavior.

# 13. WP09 --- Dataset Catalog Persistence & Lookup

**Prompt pair**

``` text
09-dataset-catalog-persistence-lookup-codex-prompt.md
09-dataset-catalog-persistence-lookup-codex-prompt-chat.md
```

**Authorized**

``` text
src/AIQuantTradingResearch.Infrastructure/**
```

for catalog registration, deterministic lookup, record mapping,
metadata/provenance/lineage/coverage reconstruction, and minimal
approved indexes.

**Prohibited:** Application storage knowledge, Worker, Domain, permanent
tests, semantic/AI search, ranking/recommendation, generalized indexing
platform, pipeline behavior.

# 14. WP10 --- Dataset Materialization Integration

**Prompt pair**

``` text
10-dataset-materialization-integration-codex-prompt.md
10-dataset-materialization-integration-codex-prompt-chat.md
```

**Authorized primary surfaces**

``` text
src/AIQuantTradingResearch.Application/**
src/AIQuantTradingResearch.Infrastructure/**
```

The WP10 authority must enumerate which layer/file responsibilities
require mutation before changing them.

Allowed: Application orchestration over approved abstractions;
Infrastructure consistency/transaction mechanics; no-partial-state
behavior; deterministic rerun semantics.

**Prohibited:** Worker, Domain redesign, permanent tests,
packages/projects, scheduler/background queue/pipeline runtime/feature
generation.

# 15. WP11 --- Dataset Validation & Failure Mapping

**Prompt pair**

``` text
11-dataset-validation-failure-mapping-codex-prompt.md
11-dataset-validation-failure-mapping-codex-prompt-chat.md
```

**Authorized**

``` text
src/AIQuantTradingResearch.Infrastructure/**
```

Minimal Application changes only when an already-approved WP04 contract
requires compile-level reconciliation and the WP11 prompt names the
exact surface.

Allowed: SQLite failure classification; malformed dataset/catalog record
handling; incompatible schema; identity/content conflict preservation;
partial-state validation; approved failure mapping.

**Prohibited:** Worker, Domain, permanent tests, unrelated exception
handling, retry framework, generalized data-quality pipeline.

# 16. WP12 --- Dependency Registration & Bounded Dataset Execution

**Prompt pair**

``` text
12-dependency-registration-bounded-dataset-execution-codex-prompt.md
12-dependency-registration-bounded-dataset-execution-codex-prompt-chat.md
```

**Authorized surfaces**

``` text
src/AIQuantTradingResearch.Application/**
src/AIQuantTradingResearch.Infrastructure/**
src/AIQuantTradingResearch.Worker/**
```

Only minimal composition changes justified by current repository
conventions: service registration, snapshot/catalog implementation
registration, configuration handoff, one bounded Worker materialization
invocation, and result reporting.

Existing configuration files may change only when explicitly named by
the WP12 authority.

**Prohibited:** new project/reference/package unless separately
authorized, CLI/service/API redesign, scheduler, continuous loop,
background framework, pipeline DAG/runtime, feature pipeline.

# 17. WP13 --- Domain & Application Dataset Tests

**Prompt pair**

``` text
13-domain-application-dataset-tests-codex-prompt.md
13-domain-application-dataset-tests-codex-prompt-chat.md
```

**Authorized test surface**

``` text
tests/AIQuantTradingResearch.Domain.Tests/**
tests/AIQuantTradingResearch.Application.Tests/**
```

No production visibility/reference/package change is automatically
authorized. If needed, stop for narrow testability authority.

**Prohibited:** Infrastructure tests, production behavior changes,
SQLite/database use in Application tests unless separately authorized,
provider/network calls.

# 18. WP14 --- Infrastructure & Dataset Tests

**Prompt pair**

``` text
14-infrastructure-dataset-tests-codex-prompt.md
14-infrastructure-dataset-tests-codex-prompt-chat.md
```

**Authorized**

``` text
tests/AIQuantTradingResearch.Infrastructure.Tests/**
```

Potential test-only dependency surface, only when explicitly authorized:

``` text
Directory.Packages.props
tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj
```

Temporary databases must be isolated, cleaned, ignored/outside tracked
state, and leave zero WAL/SHM/journal residue.

**Prohibited:** production behavior changes.

# 19. WP15 --- Architecture & Documentation Alignment

**Prompt pair**

``` text
15-architecture-documentation-alignment-codex-prompt.md
15-architecture-documentation-alignment-codex-prompt-chat.md
```

**Authorized architecture-test surface**

``` text
tests/AIQuantTradingResearch.Architecture.Tests/**
```

only for accepted Release 1.2 boundaries not already executable.

**Potential current-state documentation surface, after exact
reconciliation**

``` text
README.md
docs/architecture/data/DATA_PLATFORM_VISION.md
docs/architecture/data/DATA_LIFECYCLE.md
docs/architecture/data/DATA_CATALOG.md
docs/architecture/data/DATA_STORAGE_ARCHITECTURE.md
docs/architecture/data/DATA_PIPELINE_ARCHITECTURE.md
docs/architecture/solution/SOLUTION_ARCHITECTURE.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/PUBLIC_CONTRACTS.md
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/TESTING_STRATEGY.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
```

Only existing documents materially requiring alignment may change.

New dataset architecture documents may be created only under
`docs/architecture/data/` or another established architecture subfolder
proven more consistent by the WP15 authority. Exact files must be
enumerated before mutation.

**Prohibited:** production code/packages/project
references/solution/build/scripts/workflows and Release 1.3
implementation.

# 20. WP16 --- Full Validation, Integration & Acceptance

**Prompt pair**

``` text
16-full-validation-integration-acceptance-codex-prompt.md
16-full-validation-integration-acceptance-codex-prompt-chat.md
```

WP16 is primarily validation/integration. Before candidate freeze,
expected correction delta is zero for
production/tests/docs/packages/projects. Earlier-WP defects cause
`BLOCKED` and narrow corrective authority.

Only the separately authored WP16 authority may grant staging of the
exact candidate, one integration branch, governed commit strategy, push
without force, and one review-ready PR. It must not grant
merge/self-approval.

Temporary validation artifacts must be uncommitted and removed.

# 21. Lifecycle Governance After WP16

After `RELEASE 1.2 ACCEPTED`, lifecycle governance may be created under
the Release 1.2 prompt root.

Expected closure pair:

``` text
release-1.2-post-merge-closure-codex-prompt.md
release-1.2-post-merge-closure-codex-prompt-chat.md
```

A separate integration pair is allowed only if the eventual WP16
authority does not own integration transport. Do not create duplicate
authorities for one lifecycle transition.

Unblock/reconciliation prompts are created only for actual blockers.

# 22. Integration Candidate Accounting

Never predefine final candidate file count.

At integration:

``` text
accepted Release 1.2 governance
+ accepted WP01–WP15 implementation/test/docs
+ explicitly in-band WP16 artifacts
= reconciled candidate N
```

Freeze `N`. Later authorities must state whether their own files are
in-band (changing `N`) or out-of-band (never staged).

# 23. Project / Package Boundaries

Normally unchanged unless exact WP authority says otherwise:

``` text
AIQuantTradingResearch.slnx
Directory.Build.props
global.json
eng/**
.github/**
```

Default production package delta: `0`. Release 1.1 SQLite remains the
default storage technology.

Expected references remain:

``` text
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

No new project/reference is authorized by default.

# 24. Test Project Ownership

``` text
Domain.Tests         → WP13
Application.Tests    → WP13
Infrastructure.Tests → WP14
Architecture.Tests   → WP15
```

Temporary probes in earlier WPs are removed before completion unless an
owning test WP explicitly promotes them.

# 25. Documentation Ownership

WP02 initially owns dataset/reproducibility definition under
`docs/architecture/data/**`. WP03/WP06 may refine only their explicitly
owned semantic/model portions when authorized. WP15 owns broad
current-state convergence and README alignment.

Earlier WPs must not opportunistically rewrite broad architecture
documentation.

# 26. Release 1.1 Assets Must Be Preserved

Release 1.2 extends but does not delete accepted Release 1.1 assets
under:

``` text
src/AIQuantTradingResearch.Domain/**
src/AIQuantTradingResearch.Application/**
src/AIQuantTradingResearch.Infrastructure/**
src/AIQuantTradingResearch.Worker/**
tests/AIQuantTradingResearch.Domain.Tests/**
tests/AIQuantTradingResearch.Application.Tests/**
tests/AIQuantTradingResearch.Infrastructure.Tests/**
tests/AIQuantTradingResearch.Architecture.Tests/**
```

# 27. Existing SQLite Boundary

SQLite is already selected/integrated. WP07 owns Release 1.2 schema
evolution, not engine reselection. Reuse the existing
connection/bootstrap boundary where sufficient; preserve
`historical_observations`; use minimal deterministic schema evolution;
no destructive reset; no new database technology under this manifest.

# 28. Namespace / Folder Policy

Concrete source folders/namespaces must follow repository conventions.
This manifest intentionally does not predeclare speculative paths such
as `Application/Datasets/` or `Infrastructure/Datasets/`. The owning WP
establishes them from repository truth.

No new top-level production project/layer is authorized for folder
aesthetics.

# 29. Generated / Runtime Artifacts

Must not become candidate files:

``` text
*.db
*.sqlite
*.sqlite3
*-wal
*-shm
*-journal
TestResults/**
bin/**
obj/**
temporary probes
temporary worktrees
local secret/config files
```

All temporary artifacts are removed before WP completion.

# 30. Security-Sensitive Files

No WP may commit API keys/tokens/credentials, secret-bearing connection
strings, personal database paths, local secrets, or production market
datasets as fixtures without explicit authority. Existing
secret-scanning/verification must remain operational.

# 31. Work-Package Ownership Summary

  -----------------------------------------------------------------------
  WP                                  Primary Ownership
  ----------------------------------- -----------------------------------
  WP01                                Governance/preflight only

  WP02                                Data-architecture
                                      dataset/reproducibility definition

  WP03                                Domain semantics if necessary +
                                      narrow semantic docs

  WP04                                Application
                                      dataset/materialization/catalog
                                      contracts

  WP05                                Application materialization use
                                      case

  WP06                                Application metadata/catalog
                                      model + narrow data docs

  WP07                                Infrastructure physical
                                      schema/model

  WP08                                Infrastructure snapshot persistence

  WP09                                Infrastructure catalog
                                      persistence/lookup

  WP10                                Application/Infrastructure
                                      materialization integration

  WP11                                Infrastructure validation/failure
                                      mapping

  WP12                                Minimal
                                      Application/Infrastructure/Worker
                                      composition/execution

  WP13                                Domain.Tests + Application.Tests

  WP14                                Infrastructure.Tests + explicitly
                                      authorized test dependencies

  WP15                                Architecture.Tests + authorized
                                      current-state documentation

  WP16                                Validation + explicitly authorized
                                      integration transport
  -----------------------------------------------------------------------

Any deviation requires explicit authority.

# 32. Candidate Reconciliation

Every WP report classifies visible changes:

``` text
EXPECTED GOVERNANCE
WP01 AUTHORIZED
WP02 AUTHORIZED
...
WP16 AUTHORIZED
UNBLOCK / RECONCILIATION AUTHORIZED
EXPECTED GENERATED / IGNORED
UNEXPECTED
```

Unexpected changes must be zero. Preserve prior accepted work.

# 33. Staging / Commit Policy

WP01--WP15 normally prohibit:

``` text
git add
git commit
git push
PR creation
```

Cumulative work remains uncommitted until WP16 or a dedicated
integration authority owns Git transport.

# 34. Whitespace / Diff Policy

Before integration:

``` text
git diff --check = PASS
```

After staging:

``` text
git diff --cached --check = PASS
```

The controlling integration authority should handle zero or more
strictly mechanical whitespace findings without unnecessary recursive
authority chains when exact findings are bounded and semantic
equivalence can be proven. Corrections must name affected
files/findings, remove only the defect, prove no semantic change, and
define whether current authority artifacts are in-band or out-of-band.

# 35. Clean-Checkout Requirement

Acceptance/closure must prove no dependence on untracked databases,
developer-machine state, secrets, pre-existing schema, machine-global
data, network availability for canonical tests, or prior local Release
1.2 execution. Fresh checkout restore/build/test/verify must pass.

# 36. GitHub Planning Boundary

The later GitHub-planning authority may create planning state but no
implementation. It should establish one new authoritative Release 1.2
milestone, 16 WP issues, Project #2 items/fields, and the exact
dependency graph.

Preserve:

``` text
legacy #43 = CLOSED / EMPTY
Release 1.1 #52 = CLOSED
Release 1.1 issues = terminal
Release 1.3 implementation = NOT STARTED
```

Project field truth must be inspected rather than assumed.

# 37. Standard Prompt-Pair Inventory

``` text
01-release-repository-preflight-codex-prompt.md
01-release-repository-preflight-codex-prompt-chat.md
02-research-dataset-definition-reproducibility-model-codex-prompt.md
02-research-dataset-definition-reproducibility-model-codex-prompt-chat.md
03-dataset-identity-version-provenance-semantics-codex-prompt.md
03-dataset-identity-version-provenance-semantics-codex-prompt-chat.md
04-application-dataset-contracts-codex-prompt.md
04-application-dataset-contracts-codex-prompt-chat.md
05-dataset-materialization-use-case-codex-prompt.md
05-dataset-materialization-use-case-codex-prompt-chat.md
06-dataset-metadata-catalog-model-codex-prompt.md
06-dataset-metadata-catalog-model-codex-prompt-chat.md
07-dataset-physical-storage-model-codex-prompt.md
07-dataset-physical-storage-model-codex-prompt-chat.md
08-dataset-snapshot-persistence-codex-prompt.md
08-dataset-snapshot-persistence-codex-prompt-chat.md
09-dataset-catalog-persistence-lookup-codex-prompt.md
09-dataset-catalog-persistence-lookup-codex-prompt-chat.md
10-dataset-materialization-integration-codex-prompt.md
10-dataset-materialization-integration-codex-prompt-chat.md
11-dataset-validation-failure-mapping-codex-prompt.md
11-dataset-validation-failure-mapping-codex-prompt-chat.md
12-dependency-registration-bounded-dataset-execution-codex-prompt.md
12-dependency-registration-bounded-dataset-execution-codex-prompt-chat.md
13-domain-application-dataset-tests-codex-prompt.md
13-domain-application-dataset-tests-codex-prompt-chat.md
14-infrastructure-dataset-tests-codex-prompt.md
14-infrastructure-dataset-tests-codex-prompt-chat.md
15-architecture-documentation-alignment-codex-prompt.md
15-architecture-documentation-alignment-codex-prompt-chat.md
16-full-validation-integration-acceptance-codex-prompt.md
16-full-validation-integration-acceptance-codex-prompt-chat.md
```

All `-chat` companions are exactly five lines.

# 38. Lifecycle Artifact Boundary

Expected planning and closure artifacts:

``` text
release-1.2-github-planning-codex-prompt.md
release-1.2-github-planning-codex-prompt-chat.md
release-1.2-post-merge-closure-codex-prompt.md
release-1.2-post-merge-closure-codex-prompt-chat.md
```

A separate GitHub-integration pair exists only if WP16 does not own
integration. Never duplicate authority.

# 39. Release 1.3 Protection

No Release 1.2 WP owns artifacts primarily responsible for pipeline
orchestration, pipeline stages, DAG/workflow execution, scheduling,
recurring execution, pipeline monitoring, generic
validation/transformation/enrichment stages, or feature generation. If
such an artifact becomes necessary, return `BLOCKED`.

# 40. Final Manifest Decision

This manifest freezes Release 1.2 ownership/mutation boundaries while
leaving concrete dataset/catalog filenames evidence-driven.

``` text
Release 1.2 governance
→ GitHub planning
→ WP01–WP16
→ technical acceptance
→ governed integration
→ human merge
→ post-merge closure
→ RELEASE 1.2 CLOSED
```

Anything outside these boundaries requires explicit later authority.
