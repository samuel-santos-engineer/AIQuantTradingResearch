# Release 1.3 File Manifest

## Phase 3 --- Release 1.3: Research Pipeline Foundation

**Purpose:** Govern the exact file ownership and candidate boundaries
for Release 1.3.\
**Companion authority:** `RELEASE_1.3_EXECUTION_PLAN.md`\
**Definition authority:**
`docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`

------------------------------------------------------------------------

## 1. Manifest rules

This manifest defines authorized Release 1.3 file ownership by work
package.

It is intentionally conservative. A listed path is **authorized to be
created or modified only when its owning WP requires it**; listing does
not require mutation.

Rules:

1.  WP authorities may narrow their mutation set below this manifest.
2.  They may not broaden it silently.
3.  Existing files must be reused when they already provide the required
    boundary.
4.  New files should be introduced only when the accepted design
    requires a distinct responsibility.
5.  WP14 must derive the exact final candidate from repository truth and
    reconcile it against this manifest and all accepted WP reports.
6.  Generated build outputs, temporary databases, WAL/SHM/journal files,
    test-result artifacts, editor files, and local secrets are never
    candidate files.
7.  Out-of-band planning, corrective, resume, or post-merge authorities
    are not candidate files unless explicitly incorporated by a later
    authority.
8.  Standard governed WP prompt pairs are candidate governance
    artifacts.
9.  Every standard `-codex-prompt-chat.md` companion must contain
    exactly five non-empty logical lines.
10. Release 1.3 must not modify SQLite schema version 2 or add durable
    pipeline-run persistence.

## 2. Pre-implementation governance artifacts

These files establish Release 1.3 authority before implementation:

``` text
docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md
RELEASE_1.3_EXECUTION_PLAN.md
RELEASE_1.3_FILE_MANIFEST.md
release-1.3-github-planning-codex-prompt.md
release-1.3-github-planning-codex-prompt-chat.md
```

The definition artifact is already accepted. The execution plan and
manifest become authoritative after human acceptance. GitHub-planning
prompts are created under separate authorization.

The out-of-band planning-definition authority pair is **not** part of
the final governed candidate unless later explicitly incorporated:

``` text
release-1.3-planning-definition-codex-prompt.md
release-1.3-planning-definition-codex-prompt-chat.md
```

## 3. Standard WP governance prompt pairs

The final candidate is expected to contain one full prompt and one
exactly-five-line bootstrap for each WP:

``` text
01-release-repository-preflight-codex-prompt.md
01-release-repository-preflight-codex-prompt-chat.md

02-research-pipeline-semantic-discovery-codex-prompt.md
02-research-pipeline-semantic-discovery-codex-prompt-chat.md

03-pipeline-identity-provenance-evidence-semantics-codex-prompt.md
03-pipeline-identity-provenance-evidence-semantics-codex-prompt-chat.md

04-application-pipeline-contracts-codex-prompt.md
04-application-pipeline-contracts-codex-prompt-chat.md

05-fixed-pipeline-orchestration-codex-prompt.md
05-fixed-pipeline-orchestration-codex-prompt-chat.md

06-pipeline-validation-failure-semantics-codex-prompt.md
06-pipeline-validation-failure-semantics-codex-prompt-chat.md

07-structured-execution-evidence-codex-prompt.md
07-structured-execution-evidence-codex-prompt-chat.md

08-dependency-registration-configuration-codex-prompt.md
08-dependency-registration-configuration-codex-prompt-chat.md

09-one-shot-worker-pipeline-execution-codex-prompt.md
09-one-shot-worker-pipeline-execution-codex-prompt-chat.md

10-application-pipeline-tests-codex-prompt.md
10-application-pipeline-tests-codex-prompt-chat.md

11-composition-worker-validation-codex-prompt.md
11-composition-worker-validation-codex-prompt-chat.md

12-architecture-evolution-codex-prompt.md
12-architecture-evolution-codex-prompt-chat.md

13-documentation-alignment-codex-prompt.md
13-documentation-alignment-codex-prompt-chat.md

14-full-validation-integration-acceptance-codex-prompt.md
14-full-validation-integration-acceptance-codex-prompt-chat.md
```

If naming is intentionally changed before a WP is created, the accepted
WP authority becomes the exact naming source and WP14 must reconcile
that change explicitly.

## 4. WP01 --- Release & Repository Preflight

### Authorized production/test mutation

None.

### Authorized governance artifact

``` text
01-release-repository-preflight-codex-prompt.md
01-release-repository-preflight-codex-prompt-chat.md
```

WP01 must not create pipeline implementation files.

## 5. WP02 --- Research Pipeline Semantic Discovery

### Authorized semantic architecture artifact

Preferred path:

``` text
docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md
```

If repository conventions make a different existing architecture
subfolder clearly more authoritative, WP02 may select one equivalent
single semantic document, but must report the exact rationale and must
not create competing pipeline definitions.

### Prohibited

-   production code;
-   test code;
-   schema/SQL;
-   DI/Worker changes.

## 6. WP03 --- Pipeline Identity, Provenance & Evidence Semantics

### Authorized semantic architecture artifact

Preferred path:

``` text
docs/architecture/data/PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md
```

One semantic document is preferred. It must build on WP02 rather than
duplicate it.

### Prohibited

-   production implementation;
-   SQLite schema changes;
-   durable run-history model;
-   test expansion.

## 7. WP04 --- Application Pipeline Contracts

### Authorized Application area

Preferred namespace/directory:

``` text
src/AIQuantTradingResearch.Application/Pipelines/
```

Authorized files may include, if required:

``` text
src/AIQuantTradingResearch.Application/Pipelines/PipelineDefinition.cs
src/AIQuantTradingResearch.Application/Pipelines/PipelineIdentity.cs
src/AIQuantTradingResearch.Application/Pipelines/PipelineContracts.cs
src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionResult.cs
src/AIQuantTradingResearch.Application/Pipelines/IPipelineExecutionUseCase.cs
```

The exact minimum set must be derived from WP03. Fewer files are
preferred when responsibilities remain clear.

### Existing Application files conditionally modifiable

Only if required to expose/reuse accepted Release 1.2 seams without
semantic redesign:

``` text
src/AIQuantTradingResearch.Application/Datasets/*
```

Any modification to Dataset files must be narrowly justified and must
preserve Release 1.2 behavior.

### Prohibited

-   Infrastructure;
-   Worker;
-   Domain unless an authority conflict is raised;
-   packages/references;
-   schema.

## 8. WP05 --- Fixed Pipeline Orchestration

### Authorized Application area

``` text
src/AIQuantTradingResearch.Application/Pipelines/
```

Preferred implementation:

``` text
src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionUseCase.cs
```

Additional narrowly scoped helper files in the same directory are
allowed only if necessary for deterministic orchestration.

### Existing files conditionally modifiable

WP04 pipeline contract files may be refined only where implementation
exposes a genuine contract defect consistent with WP03.

### Prohibited

-   Infrastructure implementation;
-   Worker execution;
-   retries/scheduling/DAGs/plugins;
-   schema;
-   durable run history.

## 9. WP06 --- Pipeline Validation & Failure Semantics

### Authorized Application files

Existing WP04/WP05 files under:

``` text
src/AIQuantTradingResearch.Application/Pipelines/
```

A dedicated validation/failure file may be added if justified:

``` text
src/AIQuantTradingResearch.Application/Pipelines/PipelineValidation.cs
```

or

``` text
src/AIQuantTradingResearch.Application/Pipelines/PipelineFailure.cs
```

Prefer the smallest coherent surface.

### Infrastructure modification

Not expected. If a previously unknown Infrastructure failure leaks
through an existing seam and cannot be classified without an
Infrastructure change, WP06 must stop and request corrective authority
rather than silently broadening scope.

### Prohibited

-   retry/resilience implementation;
-   catch-all exception swallowing;
-   schema changes.

## 10. WP07 --- Structured Execution Evidence

### Authorized Application area

``` text
src/AIQuantTradingResearch.Application/Pipelines/
```

Potential files:

``` text
src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionEvidence.cs
```

Existing pipeline result/contracts may be refined if the evidence model
belongs there.

### Worker changes

Not authorized in WP07.

### Persistence changes

Not authorized. Structured evidence is local execution evidence, not
durable run-history storage.

## 11. WP08 --- Dependency Registration & Configuration

### Authorized existing files

``` text
src/AIQuantTradingResearch.Application/DependencyInjection.cs
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
```

Only the files actually requiring registration changes should be
modified.

### Authorized configuration model

Prefer Application/Worker-owned pipeline configuration consistent with
current repository conventions. A new file is allowed only if the
existing configuration approach cannot cleanly represent explicit
pipeline inputs.

Potential path:

``` text
src/AIQuantTradingResearch.Worker/PipelineExecutionConfiguration.cs
```

### Prohibited

-   database creation during DI resolution;
-   provider calls during DI resolution;
-   background/scheduler registrations;
-   schema evolution.

## 12. WP09 --- One-Shot Worker Pipeline Execution

### Authorized Worker files

Existing:

``` text
src/AIQuantTradingResearch.Worker/Program.cs
```

Preferred new bounded execution file if separation is needed:

``` text
src/AIQuantTradingResearch.Worker/PipelineExecution.cs
```

or a repository-convention-equivalent name.

Existing Release 1.2 Worker dataset execution code may be
modified/reused only if doing so produces a clearer single bounded entry
path without removing accepted Release 1.2 behavior.

### Configuration files

Repository configuration files may be modified only if they already
contain non-secret sample/default execution configuration and the WP
authority explicitly requires it.

No real API keys or local database paths may be committed.

### Prohibited

-   hosted recurring service;
-   scheduler;
-   polling/refresh loop;
-   provider acquisition orchestration.

## 13. WP10 --- Application Pipeline Tests

### Authorized test file

Preferred:

``` text
tests/AIQuantTradingResearch.Application.Tests/PipelineApplicationTests.cs
```

If existing test organization favors multiple files, WP10 may split
tests by responsibility, but should prefer one focused suite unless
size/readability clearly requires separation.

### Production delta

0.

### Prohibited

-   SQLite;
-   Worker process tests;
-   provider/network;
-   package/reference changes unless a blocker is reported.

## 14. WP11 --- Composition & Worker Validation

### Authorized test areas

Preferred Infrastructure/composition test file:

``` text
tests/AIQuantTradingResearch.Infrastructure.Tests/PipelineCompositionTests.cs
```

Worker validation may be placed in an existing suitable test project if
the current solution already provides an authorized reference path.

If no existing test project can reference Worker without adding a new
project/reference, WP11 must first determine whether process-level
validation through existing test infrastructure is sufficient. It must
not create a new test project or project reference unless explicitly
authorized by a corrective authority.

### Production delta

Expected 0.

### Temporary assets

Temporary SQLite databases and process configuration are allowed during
tests/probes but must not remain in the repository.

## 15. WP12 --- Architecture Evolution

### Authorized architecture test file

Existing architecture-test files may be modified if a new stable rule is
justified:

``` text
tests/AIQuantTradingResearch.Architecture.Tests/*
```

A new focused file is allowed only when needed.

### Zero-delta rule

`0` architecture files changed and `0` architecture tests added is an
explicitly valid WP12 outcome when the existing 13 rules already enforce
all Release 1.3 architectural boundaries.

### Production delta

0.

## 16. WP13 --- Documentation Alignment

WP13 must inspect current documentation and modify only
stale/current-state files.

### Authorized candidate documentation set

The following are candidates, not mandatory changes:

``` text
README.md
docs/architecture/data/DATA_PIPELINE_ARCHITECTURE.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/TESTING_STRATEGY.md
docs/architecture/implementation/OBSERVABILITY_MODEL.md
docs/architecture/implementation/CONFIGURATION_MODEL.md
docs/architecture/resilience/RESILIENCE_MODEL.md
```

If exact repository paths differ, WP13 must use existing canonical
documents rather than create duplicates.

The two accepted Release 1.3 semantic documents from WP02/WP03 may be
refined for terminology consistency, but their accepted semantics must
not be redesigned.

### Production/test delta

0/0.

## 17. WP14 --- Full Validation, Integration & Acceptance

### Authorized governance files

``` text
14-full-validation-integration-acceptance-codex-prompt.md
14-full-validation-integration-acceptance-codex-prompt-chat.md
```

### Repository candidate

WP14 may stage and commit only:

1.  accepted Release 1.3 governance artifacts intended for the
    candidate;
2.  accepted WP02--WP13 semantic, production, test, and documentation
    changes;
3.  narrowly authorized mechanical corrections required to make the
    exact candidate valid, only when the WP14 authority explicitly
    permits them and semantic equivalence is demonstrated.

### Integration branch

Preferred:

``` text
release/1.3-research-pipeline-foundation
```

### Preferred integration commit

``` text
feat: establish Release 1.3 research pipeline foundation
```

Exact commit wording may be adjusted to repository convention without
changing scope.

### Preferred PR

``` text
Release 1.3 — Research Pipeline Foundation
```

Base/head:

``` text
main ← release/1.3-research-pipeline-foundation
```

WP14 must leave the PR open and review-ready.

## 18. Files explicitly protected from Release 1.3 mutation

Unless a separately authorized corrective action proves mutation
unavoidable, Release 1.3 must not modify:

``` text
Directory.Build.props
Directory.Packages.props
global.json
*.sln
*.slnx
eng/*
.gitleaks.toml
```

Release 1.3 also must not introduce package/reference changes.

Existing Release 1.1/1.2 persistence schema/bootstrap files are
protected from schema evolution. In particular, Release 1.3 must
preserve schema version 2.

Any requirement to modify these protected areas is a blocker requiring
human corrective authority.

## 19. Domain protection

Expected Release 1.3 Domain delta:

``` text
0
```

No Domain production file is pre-authorized.

If implementation reveals a genuine domain concept that cannot correctly
remain Application-owned, the active WP must stop and request explicit
scope correction.

## 20. Infrastructure protection

Release 1.3 is not a persistence release.

Expected Infrastructure production delta is normally limited to DI
registration, and only if required:

``` text
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
```

The following Release 1.2 SQLite areas are reuse-only and not authorized
for semantic/schema redesign:

``` text
src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/*
```

If a bug in accepted Release 1.2 persistence blocks Release 1.3, report
it as a blocker and request corrective authority.

## 21. Schema protection

No Release 1.3 file may introduce:

-   schema version 3;
-   pipeline-run table;
-   checkpoint table;
-   scheduler table;
-   pipeline-definition persistence table;
-   operational execution-history table;
-   migration for Release 1.3.

The accepted physical state remains:

``` text
PRAGMA user_version = 2
```

## 22. Package and project-reference protection

Expected delta:

``` text
Packages added: 0
Packages removed: 0
Project references added: 0
Project references removed: 0
Solution projects added: 0
Solution projects removed: 0
```

Any non-zero delta blocks the active WP unless separately authorized.

## 23. Test ownership

Release 1.3 permanent coverage is owned as follows:

  -------------------------------------------------------------------------------
  Concern                                     Owner
  ------------------------------------------- -----------------------------------
  Pure pipeline                               WP10 / Application.Tests
  semantics/contracts/orchestration/failure
  behavior

  DI/composition/configuration/offline Worker WP11
  validation

  Executable architecture rules               WP12

  Release 1.1 persistence regression          Existing tests, unchanged

  Release 1.2 dataset regression              Existing tests, unchanged

  Full candidate validation                   WP14
  -------------------------------------------------------------------------------

WP02--WP09 may use removable temporary probes only when their individual
authority permits them. They must not preempt permanent coverage
assigned to WP10--WP12.

## 24. Expected final candidate categories

WP14 should expect the final candidate to consist only of these
categories:

``` text
Release 1.3 definition/governance
Release 1.3 semantic architecture
Application pipeline contracts/implementation
Minimal DI/composition changes
Bounded Worker one-shot execution
Application pipeline tests
Composition/Worker tests
Architecture tests only if justified
Current-state documentation alignment
```

Anything outside these categories is presumptively unexpected and
requires reconciliation.

## 25. Explicitly excluded candidate categories

The following must not appear in the Release 1.3 candidate:

``` text
live acquisition pipeline implementation
scheduler/background refresh
configurable DAG engine
pipeline plugin framework
parallel/streaming/distributed execution
retry/circuit-breaker/fallback orchestration
checkpoint/resume persistence
operational run-history persistence
schema v3
metrics backend
distributed tracing backend
feature engineering
model training/evaluation
MLOps
new external packages
new solution projects
real credentials/secrets
temporary databases
bin/
obj/
TestResults/
coverage outputs
local editor/system artifacts
out-of-band corrective authorities unless explicitly incorporated
post-merge closure authorities
Release 1.4 implementation
```

## 26. Candidate reconciliation requirements

Before staging, WP14 must produce an exact inventory and report:

-   governed paths;
-   missing paths;
-   unexpected paths;
-   duplicate logical artifacts;
-   prompt count;
-   companion count;
-   invalid companion line counts;
-   production-file delta by layer;
-   permanent-test delta by project;
-   package/reference delta;
-   documentation delta;
-   generated/temp residue;
-   schema version;
-   Release 1.4+ artifacts.

Any unresolved count other than zero for
missing/unexpected/duplicate/residue blocks integration.

## 27. Post-merge artifacts

The following are intentionally **not** part of the implementation
candidate and should be created only after human merge authorization:

``` text
release-1.3-post-merge-closure-codex-prompt.md
release-1.3-post-merge-closure-codex-prompt-chat.md
```

They remain out-of-band closure authority and must not be committed into
the accepted Release 1.3 candidate unless a future governance rule
explicitly changes this policy.

## 28. Manifest acceptance rule

This manifest is a **maximum authorized envelope**, not a quota.

A successful Release 1.3 should generally modify fewer files than the
maximum paths described here.

For every WP:

``` text
minimum coherent change
+ exact authority
+ deterministic validation
+ no later-WP implementation
+ no Release 1.4+ leakage
```

is preferred over speculative framework construction.

At WP14, repository truth plus accepted WP execution evidence determines
the exact final candidate inside this envelope.
