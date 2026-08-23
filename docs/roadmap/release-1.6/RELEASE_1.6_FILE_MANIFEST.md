# Release 1.6 File Manifest

## Phase 4 — Release 1.6: Durable Experiment Evidence Foundation

## 1. Purpose

This manifest defines the governed Release 1.6 candidate surface and file-ownership rules before implementation begins.

Authoritative predecessor baseline:

`18dfb01bf3503d91415b081b11fcdd7249094373`

This manifest works with:

- `RELEASE_1.6_DEFINITION.md`
- `RELEASE_1.6_EXECUTION_PLAN.md`

The manifest is intentionally conservative. Existing files are modified only when a work package requires them. New implementation filenames below are authorized logical targets; if repository truth requires a narrowly different filename in the same ownership area, the relevant WP must explicitly reconcile that difference rather than silently creating aliases or duplicates.

## 2. Candidate Classes

The final Release 1.6 candidate may contain only:

1. Release planning artifacts
2. Governed WP/GitHub-planning prompt pairs
3. Release 1.6 semantic/physical-model documentation
4. Application production changes
5. Infrastructure production changes
6. Worker production changes
7. Application tests
8. Infrastructure tests
9. Architecture tests only if WP13 proves a non-redundant rule is required
10. Manifest-authorized current-state documentation

Anything else is unexpected unless separately reconciled by explicit corrective authority before WP14 staging.

## 3. Planning Artifacts — Governed

Expected governed planning artifacts:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`

Count: 3.

## 4. Planning-Definition Authority — Out of Band

The pair:

- `release-1.6-planning-definition-codex-prompt.md`
- `release-1.6-planning-definition-codex-prompt-chat.md`

is execution-only planning input and is excluded from the governed Release 1.6 candidate unless a later explicit reconciliation changes this classification.

WP14 must exclude it.

## 5. GitHub-Planning Authority Pair — Governed

Once separately created and accepted:

- `prompts/release-1.6-github-planning-codex-prompt.md`
- `prompts/release-1.6-github-planning-codex-prompt-chat.md`

The companion must contain exactly five non-empty logical lines and satisfy repository final-newline/whitespace rules.

## 6. WP Prompt Pairs — Governed

Expected governed WP prompt pairs:

- `prompts/release-1.6/01-release-repository-preflight-codex-prompt.md`
- `prompts/release-1.6/01-release-repository-preflight-codex-prompt-chat.md`
- `prompts/release-1.6/02-durable-experiment-evidence-discovery-codex-prompt.md`
- `prompts/release-1.6/02-durable-experiment-evidence-discovery-codex-prompt-chat.md`
- `prompts/release-1.6/03-persistence-identity-provenance-fidelity-codex-prompt.md`
- `prompts/release-1.6/03-persistence-identity-provenance-fidelity-codex-prompt-chat.md`
- `prompts/release-1.6/04-application-persistence-contracts-codex-prompt.md`
- `prompts/release-1.6/04-application-persistence-contracts-codex-prompt-chat.md`
- `prompts/release-1.6/05-durable-experiment-use-case-integration-codex-prompt.md`
- `prompts/release-1.6/05-durable-experiment-use-case-integration-codex-prompt-chat.md`
- `prompts/release-1.6/06-schema-v3-physical-model-codex-prompt.md`
- `prompts/release-1.6/06-schema-v3-physical-model-codex-prompt-chat.md`
- `prompts/release-1.6/07-experiment-result-persistence-codex-prompt.md`
- `prompts/release-1.6/07-experiment-result-persistence-codex-prompt-chat.md`
- `prompts/release-1.6/08-exact-experiment-result-retrieval-codex-prompt.md`
- `prompts/release-1.6/08-exact-experiment-result-retrieval-codex-prompt-chat.md`
- `prompts/release-1.6/09-storage-validation-failure-mapping-codex-prompt.md`
- `prompts/release-1.6/09-storage-validation-failure-mapping-codex-prompt-chat.md`
- `prompts/release-1.6/10-dependency-registration-configuration-codex-prompt.md`
- `prompts/release-1.6/10-dependency-registration-configuration-codex-prompt-chat.md`
- `prompts/release-1.6/11-one-shot-durable-experiment-worker-codex-prompt.md`
- `prompts/release-1.6/11-one-shot-durable-experiment-worker-codex-prompt-chat.md`
- `prompts/release-1.6/12-application-infrastructure-persistence-tests-codex-prompt.md`
- `prompts/release-1.6/12-application-infrastructure-persistence-tests-codex-prompt-chat.md`
- `prompts/release-1.6/13-architecture-documentation-alignment-codex-prompt.md`
- `prompts/release-1.6/13-architecture-documentation-alignment-codex-prompt-chat.md`
- `prompts/release-1.6/14-full-validation-integration-acceptance-codex-prompt.md`
- `prompts/release-1.6/14-full-validation-integration-acceptance-codex-prompt-chat.md`

Expected WP prompt files: 28.

Every chat companion must contain exactly five non-empty logical lines.

## 7. Semantic / Physical-Model Documentation — Governed

Expected new Release 1.6 architecture artifacts:

- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_SCHEMA_V3.md`

Count: 3.

These documents freeze semantics before corresponding implementation.

## 8. Application Production — Authorized Surface

Expected new or modified Application paths are confined to:

- `src/AIQuantTradingResearch.Application/Experiments/`
- `src/AIQuantTradingResearch.Application/DependencyInjection.cs`

Preferred logical implementation artifacts:

- `ExperimentPersistenceContracts.cs`
- `DurableExperimentUseCase.cs`
- `ExperimentPersistenceValidator.cs`

Existing Release 1.5 experiment model/identity files may be modified only if a WP proves a minimal compatibility change is required. Reuse is preferred over semantic duplication.

Application must not reference Infrastructure.

## 9. Infrastructure Production — Authorized Surface

Expected Release 1.6 Infrastructure changes are confined to the existing SQLite persistence/migration ownership areas plus narrowly scoped Experiment persistence implementation.

Preferred logical artifacts include:

- an Experiment Result SQLite store implementation;
- schema-v3 migration/model changes within the existing schema mechanism;
- storage mapping/failure-classification changes only where required.

Exact filenames must follow repository conventions discovered by WP06/WP07.

Do not create:

- Feature Set persistence implementation;
- experiment registry/history repositories;
- generic ORM/repository frameworks;
- provider/network implementation.

## 10. Worker Production — Authorized Surface

Expected Worker changes are confined to:

- `src/AIQuantTradingResearch.Worker/Program.cs`
- one durable-experiment configuration artifact;
- one durable-experiment execution/presentation artifact.

Preferred logical names:

- `DurableExperimentExecutionConfiguration.cs`
- `DurableExperimentExecution.cs`

Existing Release 1.5 `ExperimentExecution` and Feature/Pipeline behavior must remain semantically unchanged except for explicit routing necessary to add the new mode.

## 11. Application Tests — Authorized Surface

Expected permanent Application Release 1.6 test delta:

- one focused test file under `tests/AIQuantTradingResearch.Application.Tests/`

Preferred logical name:

- `ExperimentPersistenceApplicationTests.cs`

It may cover contracts/orchestration/validation/equivalence/failure semantics.

Do not move SQLite/process tests into Application tests.

## 12. Infrastructure Tests — Authorized Surface

Expected permanent Infrastructure Release 1.6 test delta:

- one focused persistence/migration test file under `tests/AIQuantTradingResearch.Infrastructure.Tests/`

Preferred logical name:

- `ExperimentPersistenceTests.cs`

If Worker composition/process validation cannot remain coherent in the same file under established conventions, WP12 may justify one additional narrowly scoped Infrastructure test file, but this must be explicitly reconciled before WP14 and must not become an ungoverned path.

Coverage may include:

- schema v3;
- v2→v3 migration;
- predecessor preservation;
- insert/retrieve fidelity;
- idempotency;
- conflict;
- atomicity;
- restart recovery;
- unavailable storage;
- DI/Worker process proof;
- cleanup/residue.

## 13. Architecture Tests — Conditional Surface

Default Release 1.6 Architecture test delta:

`0`

Existing architecture tests remain authoritative unless WP13 proves a stable non-redundant structural rule is missing.

If such a rule is required, only the existing Architecture test project may be modified.

No new Architecture test project.

## 14. Current-State Documentation — Authorized Surface

WP13 may modify only these current-state documentation paths unless repository truth proves one additional existing document is materially stale and WP13 explicitly stops for reconciliation:

- `README.md`
- `docs/architecture/data/DATA_PIPELINE_ARCHITECTURE.md`
- `docs/architecture/design/CONFIGURATION_MODEL.md`
- `docs/architecture/design/MODULE_INTERACTIONS.md`
- `docs/architecture/design/PUBLIC_CONTRACTS.md`
- `docs/architecture/implementation/DEPENDENCY_INJECTION.md`
- `docs/architecture/implementation/OBSERVABILITY_MODEL.md`
- `docs/architecture/implementation/TESTING_STRATEGY.md`

Expected current-state documentation maximum: 8 paths.

Do not rewrite unrelated historical documents.

## 15. Project / Package / Reference Files

Default authorized delta:

- packages: 0
- projects: 0
- project references: 0
- solution projects: remains 8

Therefore these files are not candidate paths by default:

- `Directory.Packages.props`
- project `.csproj` files
- solution file

If implementation proves a change unavoidable, the active WP must stop and request explicit reconciliation rather than silently modifying them.

## 16. Schema Files

Schema v3 changes must occur only through the repository's existing schema/migration mechanism discovered in WP06.

Do not invent a parallel migration framework.

The manifest authorizes the necessary existing Infrastructure schema/migration file(s) once identified and explicitly named by WP06 before WP07 implementation.

WP14 must reconcile their exact paths against WP06 authority.

## 17. Generated / Runtime Files — Always Excluded

Never govern or commit:

- SQLite runtime databases;
- `-wal`;
- `-shm`;
- journal files;
- temporary probe projects;
- temporary worktrees;
- build output;
- test output;
- coverage output;
- logs;
- secrets;
- real credentials;
- generated local configuration.

Residue count must be zero at each WP completion.

## 18. Corrective Authorities

Any narrow corrective authority created because a WP stops at a governance gate is execution-only by default.

It must remain excluded from the Release 1.6 candidate unless a later explicit candidate-governance reconciliation declares otherwise.

WP14 must inventory and exclude all such corrective authority pairs.

## 19. Candidate Governance Rules

Before WP14 stages anything, it must prove:

- every governed expected path exists;
- no unexpected candidate path exists;
- no duplicate logical artifact exists;
- no orphan prompt companion exists;
- every governed prompt has exactly one companion;
- every companion has exactly five non-empty logical lines;
- all governed files satisfy direct whitespace/final-newline rules;
- execution-only authorities are excluded;
- generated/runtime residue is zero.

No staging may occur before this reconciliation passes.

## 20. Expected Candidate Shape

The exact final path count is intentionally not frozen before implementation because Infrastructure schema/migration ownership uses existing repository files whose exact paths must be discovered, and WP12 may justify one additional Infrastructure test file.

However, the candidate must reconcile exactly to this manifest's categories and authorized surfaces.

WP14 must report exact counts by:

- Planning
- Governance prompts/companions
- Semantic/physical-model documentation
- Application production
- Infrastructure production
- Worker production
- Application tests
- Infrastructure tests
- Architecture tests
- Current-state documentation

Unexpected: 0.

## 21. Git Integration Boundary

Only WP14 may:

- stage the governed Release 1.6 candidate;
- create the Release 1.6 integration branch;
- create the single integration commit;
- push the branch;
- create the Release 1.6 PR.

WP01–WP13 must leave cumulative accepted work unstaged and uncommitted.

## 22. Release 1.6 Integration Naming

Preferred integration branch:

`release/1.6-durable-experiment-evidence-foundation`

Preferred commit message:

`feat: establish Release 1.6 durable experiment evidence foundation`

Preferred PR title:

`Release 1.6 — Durable Experiment Evidence Foundation`

WP14 must reconcile these against repository conventions before use.

## 23. Post-Merge Boundary

WP14 must leave its PR open and unmerged.

Milestone closure, merged-main verification, and formal Release 1.6 closure require a separate post-merge closure authority after explicit human merge authorization.

## 24. Release 1.7 Exclusion

No Release 1.7 planning or implementation belongs in the Release 1.6 candidate.

Generic historical future-roadmap references do not constitute Release 1.7 work, but new Release 1.7 artifacts/issues/branches/implementation are prohibited.

## 25. Final Manifest Principle

Every Release 1.6 path must answer one question:

**Is this file necessary to make accepted Release 1.5 Experiment Result evidence durably persistent, exactly retrievable, integrity-protected, and operationally provable without generalizing the research platform beyond Release 1.6?**

If not, it does not belong in the candidate.
