# Release 1.4 File Manifest

## 1. Purpose

This manifest defines the authorized Release 1.4 repository mutation envelope for:

**Phase 4 — Release 1.4: Deterministic Feature Engineering Foundation**

It is subordinate to `RELEASE_1.4_DEFINITION.md` and paired with `RELEASE_1.4_EXECUTION_PLAN.md`.

The manifest is intentionally restrictive. A path listed here is eligible for mutation only when the active work-package prompt authorizes that mutation. Listing a path does not grant blanket permission to change it.

If implementation truth requires a path outside this manifest, stop and obtain corrective authority before mutation.

## 2. Global Mutation Rules

Release 1.4 expects:

- no new project;
- no new package;
- no new project reference;
- no SQLite schema change;
- no feature persistence;
- no provider implementation change;
- no Release 1.3 pipeline semantic change;
- no Release 1.5 implementation.

Unless explicitly authorized by a later corrective authority, these files are protected from Release 1.4 mutation:

- `Directory.Build.props`
- `Directory.Packages.props`
- `global.json`
- solution/project files
- SQLite schema/migration files
- Twelve Data provider implementation
- Release 1.1 historical persistence implementation except existing seams consumed unchanged
- Release 1.2 dataset persistence/catalog implementation except existing seams consumed unchanged
- Release 1.3 pipeline semantic/orchestration implementation
- engineering scripts under `eng/`

## 3. Authoritative Planning Artifacts

These two artifacts are governed Release 1.4 planning inputs:

```text
docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md
docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md
docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md
```

The definition is created before this manifest/plan pair. The execution plan and manifest are created together under human authority.

## 4. Governance Prompt Directory

Canonical Release 1.4 work-package governance location:

```text
docs/roadmap/release-1.4/
```

Expected WP prompt pairs:

```text
01-release-repository-preflight-codex-prompt.md
01-release-repository-preflight-codex-prompt-chat.md

02-feature-engineering-semantic-discovery-codex-prompt.md
02-feature-engineering-semantic-discovery-codex-prompt-chat.md

03-feature-identity-provenance-evidence-semantics-codex-prompt.md
03-feature-identity-provenance-evidence-semantics-codex-prompt-chat.md

04-feature-domain-application-model-codex-prompt.md
04-feature-domain-application-model-codex-prompt-chat.md

05-feature-generation-contracts-codex-prompt.md
05-feature-generation-contracts-codex-prompt-chat.md

06-deterministic-feature-computation-codex-prompt.md
06-deterministic-feature-computation-codex-prompt-chat.md

07-feature-validation-failure-mapping-codex-prompt.md
07-feature-validation-failure-mapping-codex-prompt-chat.md

08-feature-generation-integration-codex-prompt.md
08-feature-generation-integration-codex-prompt-chat.md

09-dependency-registration-configuration-codex-prompt.md
09-dependency-registration-configuration-codex-prompt-chat.md

10-one-shot-worker-feature-execution-codex-prompt.md
10-one-shot-worker-feature-execution-codex-prompt-chat.md

11-feature-semantic-tests-codex-prompt.md
11-feature-semantic-tests-codex-prompt-chat.md

12-composition-worker-validation-codex-prompt.md
12-composition-worker-validation-codex-prompt-chat.md

13-architecture-documentation-evolution-codex-prompt.md
13-architecture-documentation-evolution-codex-prompt-chat.md

14-full-validation-integration-acceptance-codex-prompt.md
14-full-validation-integration-acceptance-codex-prompt-chat.md
```

Separate GitHub-planning and post-merge-closure authorities may also be intentionally governed if human-approved and incorporated into the candidate. Temporary normalization, resume, correction, or out-of-band planning-definition authorities are excluded by default and must not silently enter the final candidate.

Every governed `*-codex-prompt-chat.md` companion must contain exactly five non-empty logical lines.

## 5. Semantic Architecture Artifacts

### WP02

Authorized new file:

```text
docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md
```

No other semantic architecture file is expected to change in WP02.

### WP03

Authorized new file:

```text
docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md
```

No other semantic architecture file is expected to change in WP03.

## 6. Production Feature Area

Release 1.4 production feature code must be concentrated under:

```text
src/AIQuantTradingResearch.Application/Features/
```

The exact file decomposition is intentionally left to WP04–WP08 after semantic discovery. Authorized files may be created or modified only under this directory for feature semantics, contracts, computation, validation, identity, evidence, and integration.

Expected cohesive responsibilities include:

- feature typed identities;
- built-in feature definition/model;
- feature observations and immutable feature set;
- provenance/lineage;
- request/result/failure/evidence contracts;
- canonical identity computation;
- deterministic simple-return computation;
- validation;
- feature-generation use-case integration.

### Production area restriction

WP04–WP08 may not create a general-purpose plugin/registry/DAG framework merely because the directory is open as an authorized feature area.

No Infrastructure production feature directory is authorized by default.

## 7. Domain Mutation

Preferred Domain delta:

```text
0 files
```

WP04 may modify/add Domain code only if its authoritative execution proves that a small technology-independent numeric invariant must reside there and the WP04 prompt explicitly permits the exact path after reconciliation.

Because the accepted definition is zero-delta-first, any Domain mutation must be treated as exceptional and reported explicitly. WP14 must reject unexplained Domain feature changes.

## 8. Existing Application Composition

### WP09 authorized existing file

```text
src/AIQuantTradingResearch.Application/DependencyInjection.cs
```

Mutation is limited to the minimum Release 1.4 feature registration required by accepted WP04–WP08 contracts.

Do not duplicate existing dataset/pipeline registrations or change unrelated lifetimes.

## 9. Worker Surface

WP09/WP10 may use the existing Worker composition root and a narrowly bounded feature configuration/execution surface.

Authorized existing file:

```text
src/AIQuantTradingResearch.Worker/Program.cs
```

Authorized Release 1.4 Worker files, if required:

```text
src/AIQuantTradingResearch.Worker/FeatureExecutionConfiguration.cs
src/AIQuantTradingResearch.Worker/FeatureExecution.cs
```

If repository conventions discovered at execution time make a different feature-specific filename clearly canonical, WP09/WP10 must stop before creating it unless the active prompt explicitly contains bounded manifest-reconciliation authority.

Worker mutation must not rewrite Release 1.2 dataset or Release 1.3 pipeline semantics.

## 10. Infrastructure Production Surface

Expected Release 1.4 Infrastructure production delta:

```text
0 files
```

Existing snapshot/catalog persistence and DI registrations are reused.

The following are specifically not authorized:

- feature persistence store;
- feature catalog;
- feature cache;
- schema migration;
- provider feature adapter;
- run-history repository;
- scheduler/retry infrastructure.

If WP08/WP09 discovers that an Infrastructure production change is genuinely necessary, execution must stop for corrective authority.

## 11. Permanent Test Surface

### WP11

Authorized test projects:

```text
tests/AIQuantTradingResearch.Domain.Tests/
tests/AIQuantTradingResearch.Application.Tests/
```

Expected Domain test delta is zero unless WP04 introduced an explicitly authorized Domain invariant.

Preferred new Application test file:

```text
tests/AIQuantTradingResearch.Application.Tests/FeatureApplicationTests.cs
```

If existing naming conventions strongly justify a different single cohesive feature test filename, WP11 may use it only under explicit prompt-level bounded reconciliation.

### WP12

Preferred new Infrastructure composition/Worker test file:

```text
tests/AIQuantTradingResearch.Infrastructure.Tests/FeatureCompositionTests.cs
```

This file owns real DI/configuration/black-box Worker proof using temporary isolated SQLite state and dummy non-production provider configuration only where existing host construction requires it.

### Architecture tests

Existing architecture project:

```text
tests/AIQuantTradingResearch.Architecture.Tests/
```

WP13 may modify existing architecture tests or add one narrowly scoped architecture test file only when a stable non-redundant Release 1.4 boundary is not already enforced.

Zero architecture-test delta is explicitly valid.

## 12. Documentation Alignment Surface

WP13 may modify only current-state documentation proven stale by Release 1.4.

Pre-authorized candidate documents for inspection and mutation when justified:

```text
README.md
docs/architecture/data/DATA_LIFECYCLE.md
docs/architecture/data/DATA_PIPELINE_ARCHITECTURE.md
docs/architecture/data/DATA_GLOSSARY.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/PUBLIC_CONTRACTS.md
docs/architecture/design/CONFIGURATION_MODEL.md
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/OBSERVABILITY_MODEL.md
docs/architecture/implementation/TESTING_STRATEGY.md
```

This is an upper bound, not a requirement to touch every file.

WP13 must minimize documentation mutation and leave documents unchanged when already accurate.

Do not modify roadmap documents for Release 1.5+, resilience strategy, MLOps, backtesting, or future feature frameworks merely to restate deferrals.

## 13. WP-by-WP Mutation Matrix

| WP | New/modified repository content authorized |
| --- | --- |
| WP01 | None; evidence/lifecycle only |
| WP02 | `docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md` |
| WP03 | `docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md` |
| WP04 | `src/AIQuantTradingResearch.Application/Features/**`; Domain only by explicit exceptional authority |
| WP05 | `src/AIQuantTradingResearch.Application/Features/**` |
| WP06 | `src/AIQuantTradingResearch.Application/Features/**` |
| WP07 | `src/AIQuantTradingResearch.Application/Features/**` |
| WP08 | `src/AIQuantTradingResearch.Application/Features/**` |
| WP09 | `src/AIQuantTradingResearch.Application/DependencyInjection.cs`; `src/AIQuantTradingResearch.Worker/FeatureExecutionConfiguration.cs` if required |
| WP10 | `src/AIQuantTradingResearch.Worker/Program.cs`; `src/AIQuantTradingResearch.Worker/FeatureExecution.cs`; bounded refinement of feature configuration |
| WP11 | Domain/Application feature tests only |
| WP12 | Infrastructure composition/Worker tests only |
| WP13 | Architecture tests only if justified; bounded current-state documentation list |
| WP14 | Mechanical corrections explicitly allowed by WP14 authority; Git integration objects after all gates pass |

Governance prompt files are created under separate human prompt-generation actions and are cumulative candidate governance artifacts; ordinary implementation WPs must not rewrite predecessor full prompts.

## 14. Protected Schema and Persistence Paths

Release 1.4 does not authorize modification of SQLite schema or migration behavior.

Any path implementing:

- schema bootstrap/versioning;
- migration v1→v2;
- historical observation tables;
- dataset snapshot tables;
- dataset catalog tables;

is protected unless a later corrective authority explicitly proves that a non-semantic mechanical correction is required.

WP14 must verify SQLite schema remains exactly version 2.

## 15. Package and Reference Protection

Expected deltas:

```text
Packages: 0
Projects: 0
Project references: 0
```

Changes to `.csproj`, central package management, solution files, SDK pinning, analyzers, or build properties are outside the Release 1.4 candidate.

Any such required change is a stop condition.

## 16. Generated and Temporary Files

Never include:

- `bin/`
- `obj/`
- `.vs/`
- test-result output
- coverage output
- temporary probe projects/files
- temporary worktrees
- SQLite test databases
- `-wal`
- `-shm`
- `-journal`
- logs containing execution residue
- local secrets/configuration overrides

Temporary probes are permitted only when an active prompt authorizes them. They must be removed before WP completion.

## 17. Out-of-Band Authority Exclusion

Planning-definition prompt pairs used only to create the accepted definition are not automatically part of the governed integration candidate.

Likewise, temporary:

- normalization authorities;
- resume authorities;
- corrective authorities;
- investigation notes;
- copied execution reports;

must remain outside the final candidate unless a later explicit authority names them for inclusion.

WP14 must reconcile these paths directly rather than assuming they are excluded.

## 18. Candidate Classification Rules

WP14 must classify every Release 1.4 candidate path into exactly one category:

1. planning/semantic documentation;
2. governance prompt;
3. Application production;
4. Worker production;
5. Domain production, only if exceptionally authorized;
6. permanent test;
7. architecture test;
8. current-state documentation.

Expected Infrastructure production category count is zero.

A path that cannot be classified from repository truth and governing authority is unexpected and blocks integration.

## 19. Governance Pair Reconciliation

Before WP14 stages anything:

- enumerate all governed Release 1.4 full prompts;
- enumerate all governed Release 1.4 chat companions;
- require one-to-one pairing;
- require exactly five non-empty logical lines per companion;
- reject duplicate logical companions;
- reject missing companions;
- reject temporary/out-of-band authorities not intentionally governed.

Do not normalize malformed governance during WP14 unless WP14's authority explicitly permits the exact mechanical correction. Otherwise stop for narrow corrective authority.

## 20. Whitespace Reconciliation

Because cumulative Release work may remain untracked until WP14, `git diff --check` alone is insufficient.

Every WP that creates untracked Markdown/source files must directly inspect those files for trailing whitespace.

WP14 must:

1. run direct whitespace inspection across every candidate path;
2. run `git diff --check`;
3. after exact staging, run `git diff --cached --check`;
4. stop on semantic ambiguity;
5. perform only explicitly authorized mechanical whitespace correction.

## 21. Candidate Accounting

The manifest intentionally does not freeze an exact final file count before WP04–WP13 execute, because the accepted definition freezes responsibilities rather than arbitrary class decomposition.

WP14 must derive and report actual:

- total governed paths;
- planning/semantic documentation count;
- governance prompt count;
- production count by layer;
- test count by project;
- architecture-test count;
- documentation count;
- insertions/deletions;
- package/reference/schema deltas;
- missing paths;
- unexpected paths;
- duplicate paths;
- generated/database residue.

Exact candidate accounting becomes authoritative only when WP14 reconciles repository truth.

## 22. Final Integration Allowlist

Subject to actual WP execution and reconciliation, the final Release 1.4 candidate may contain only:

- the three Release 1.4 planning artifacts;
- intentionally governed Release 1.4 prompt pairs;
- the two semantic architecture artifacts;
- authorized Application `Features/**` production code;
- minimal Application DI change;
- bounded Worker feature configuration/execution changes;
- authorized permanent Domain/Application tests;
- authorized Infrastructure composition/Worker tests;
- stable architecture-test changes if justified;
- bounded current-state documentation changes.

No Infrastructure feature persistence production code is allowed.

No schema/package/project/reference mutation is allowed.

## 23. WP14 Mechanical Correction Boundary

WP14 may perform only mechanical candidate corrections explicitly authorized by its full prompt, such as:

- exact trailing-whitespace removal;
- removal of generated/temporary residue;
- removal of explicitly excluded out-of-band authority files;
- correction of a governance companion only if the authority explicitly permits normalization and semantic equivalence can be proven.

WP14 must not use mechanical-correction authority to redesign contracts, change semantics, alter tests to pass, broaden scope, or conceal candidate drift.

If a correction is not clearly mechanical, stop.

## 24. Integration Artifacts

WP14 is expected to create Git/GitHub integration state, not additional repository planning content:

- one integration branch;
- one integration commit;
- one normal push;
- one non-draft PR.

Recommended branch:

```text
release/1.4-deterministic-feature-engineering-foundation
```

Recommended commit:

```text
feat: establish Release 1.4 deterministic feature engineering foundation
```

The PR must remain unmerged pending human review.

## 25. Post-Merge Protection

Post-merge closure must not add repository files.

Its expected repository content delta is:

```text
0
```

Its only expected GitHub lifecycle mutation, after every closure gate passes, is closure of the authoritative Release 1.4 milestone.

No tag, GitHub Release, branch deletion, Release 1.5 planning, or repository-content edit is implied.

## 26. Manifest Acceptance Criteria

This manifest is accepted only if it remains consistent with all of the following:

- one built-in `simple-return-lag-1-v1` feature;
- feature generation separate from Release 1.3 pipeline;
- accepted immutable snapshot input;
- immutable in-memory feature output;
- `aiq-feature-identity-v1`;
- exact decimal/timestamp/offset/order semantics;
- no feature persistence;
- SQLite schema version 2;
- unchanged production dependency graph;
- packages/projects/references 0/0/0;
- deterministic offline testing;
- exactly WP01–WP14;
- Release 1.5+ exclusions.

Any future contradiction requires explicit human-authorized reconciliation before execution continues.
