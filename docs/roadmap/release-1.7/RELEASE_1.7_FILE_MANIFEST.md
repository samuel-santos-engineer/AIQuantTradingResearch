# Release 1.7 File Manifest

## Governance

Release: **Phase 4 - Release 1.7: Durable Experiment Evidence Discovery**

Baseline: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`

Status: conservative planning inventory; no implementation authority.

Schema remains v3. Package, project, reference, solution-membership, and production dependency-edge deltas are zero.

## CREATE

| Path | Owner | Allowed change |
| --- | --- | --- |
| `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md` | Definition | Objective, scope, decisions, exclusions |
| `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md` | Definition | WP01-WP13 plan and GitHub proposal |
| `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md` | Definition | Conservative mutation inventory |
| `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE_DISCOVERY.md` | WP02 | Exact bounded discovery semantics |
| `docs/architecture/data/EXPERIMENT_DISCOVERY_IDENTITY_PROVENANCE_FIDELITY.md` | WP03 | Identity/provenance/fidelity authority |
| `docs/architecture/data/EXPERIMENT_DISCOVERY_PHYSICAL_ACCESS.md` | WP06 | Schema-v3 predicate/order/query-plan decision |
| `src/AIQuantTradingResearch.Application/Experiments/DurableExperimentDiscoveryUseCase.cs` | WP05 | Application discovery orchestration |
| `src/AIQuantTradingResearch.Worker/DurableExperimentDiscoveryConfiguration.cs` | WP09 | Worker configuration only |
| `src/AIQuantTradingResearch.Worker/DurableExperimentDiscoveryExecution.cs` | WP10 | One-shot execution/presentation |
| `tests/AIQuantTradingResearch.Application.Tests/ExperimentDiscoveryApplicationTests.cs` | WP11 | Application semantic coverage |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/ExperimentDiscoveryTests.cs` | WP11 | SQLite/DI/Worker-process coverage |

## MODIFY

| Path | Owner | Allowed change |
| --- | --- | --- |
| `src/AIQuantTradingResearch.Application/Experiments/ExperimentPersistenceContracts.cs` | WP04 | Storage-independent discovery contracts only |
| `src/AIQuantTradingResearch.Application/DependencyInjection.cs` | WP09 | Exactly-once use-case registration |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteExperimentResultStore.cs` | WP07 | Bounded read-only query/storage classification |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteExperimentResultMapper.cs` | WP07 | Immutable reconstruction validation only if required |
| `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs` | WP09 | Exactly-once discovery-store registration |
| `src/AIQuantTradingResearch.Worker/Program.cs` | WP10 | Explicit discovery intent/routing |
| `README.md` | WP12 | Current-state capability/test count |
| `docs/architecture/data/DATA_PIPELINE_ARCHITECTURE.md` | WP12 | Durable discovery flow/exclusions |
| `docs/architecture/design/CONFIGURATION_MODEL.md` | WP12 | Discovery configuration/routing |
| `docs/architecture/design/MODULE_INTERACTIONS.md` | WP12 | Interaction/ownership |
| `docs/architecture/design/PUBLIC_CONTRACTS.md` | WP12 | Application contracts |
| `docs/architecture/implementation/DEPENDENCY_INJECTION.md` | WP12 | Registration/lifetimes |
| `docs/architecture/implementation/OBSERVABILITY_MODEL.md` | WP12 | Bounded evidence/output |
| `docs/architecture/implementation/TESTING_STRATEGY.md` | WP12 | Coverage/final counts |

WP08 is a zero-production-delta validation gate. A gap found there requires narrow corrective authority rather than giving a second WP ownership of WP07 files.

## CONDITIONAL

| Path | Owner | Condition |
| --- | --- | --- |
| `tests/AIQuantTradingResearch.Architecture.Tests/ResearchBoundaryRulesTests.cs` | WP12 | Only a genuinely new stable non-redundant repository-wide rule |

No schema/bootstrap/migration path is conditional: WP06 freezes schema v3 unchanged. A structural need blocks and requires separate corrective authority.

## READ-ONLY

- Release 1.1-1.6 authorities and closure evidence;
- `docs/handbook/PRODUCT_VISION.md`, `docs/project/ROADMAP.md`, solution/data architecture, and `ENGINEERING_PLAYBOOK.md`;
- Domain production code and predecessor Dataset/Feature/Experiment/durable-evidence code;
- `SqliteExperimentResultSchema.cs` and `SqliteSchemaBootstrapper.cs`;
- existing test helpers/process conventions outside the WP11 file;
- solution/build/security and package/project/reference files.

## FORBIDDEN

- Domain or schema/table/column/index/bootstrap/migration changes;
- Feature Value/Feature Set persistence;
- Experiment acceptance/exact-retrieval redesign;
- generalized registry/history/search/list-all/comparison/tagging;
- provider/network orchestration or real credentials;
- retry/recovery/repair/scheduling;
- packages, projects, references, solution membership, or dependency edges;
- unrelated current-state docs or WP implementation prompts during definition;
- Release 1.8 artifacts or Git transport before acceptance.

## Process-level validation fixture contract

The permanent host is `AIQuantTradingResearch.Infrastructure.Tests`, using its existing friend assembly and established patterns: `TemporaryDatabase` plus `SqliteConnectionFactory`; `DatasetSnapshotCandidate` plus `SqliteDatasetSnapshotStore.Store(...)`; production Release 1.6 durable acceptance for `experiment_results`; existing bounded Worker runner with `--no-build`; deterministic identities/order/bounds/empty/no-fallback/exit assertions; fixture cleanup of database, WAL/SHM/journal, output, temporary directories, and processes. External probes, provider calls, real credentials, and production visibility changes are forbidden.

## Accounting rules

- Every actual path belongs to exactly one category.
- CREATE/MODIFY requires the individual WP authority.
- Existing canonical files are extended, not duplicated.
- The planning-definition prompt pair is out of band.
- Final acceptance requires zero missing, unexpected, duplicate, or residue paths.
