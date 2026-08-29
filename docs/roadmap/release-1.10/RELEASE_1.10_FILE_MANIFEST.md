# Release 1.10 — File Manifest

## Model assignment

- **GPT-5.6 Luna** — manifest and governance authority.
- **GPT-5.6 Terra** — later implementation/validation and Git/GitHub execution authority.
- **GPT-5.6 Sol** — supporting analysis only.

**Selected execution model for this manifest: GPT-5.6 Luna.**

The following ownership is planned only; this manifest does not authorize implementation.

| Path/area | Action | Owner | Constraint |
|---|---|---|---|
| `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md` | VERIFY/MODIFY by planning authority | Governance | Canonical scope and boundaries. |
| `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md` | VERIFY/MODIFY by planning authority | Governance | WP order and gates. |
| `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md` | VERIFY/MODIFY by planning authority | Governance | Exact future ownership. |
| `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md` | CREATE in WP01 only | Architecture | Required selection record; exact technology/version/backend remains undecided until WP01. |
| `src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs` | CREATE in WP02 only | Application | Internal BCL `ActivitySource`/`Meter` helper; owns only fixed Application source, meter, activity, metric, and bounded-attribute constants/helpers. No SDK, exporter, provider, or business state. |
| `src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionUseCase.cs` | MODIFY in WP02 only | Application | `Execute(...)` and `ExecuteCanonical(...)` may add root/stage observation calls around existing five-stage orchestration. Preserve stage order, outputs, exception propagation, and dependency direction. |
| `src/AIQuantTradingResearch.Application/Datasets/MaterializeDatasetUseCase.cs` | MODIFY in WP02 only | Application | The two `Execute(...)` overloads only may add BCL child activities: `HistoricalObservationRetrieval` exactly around `IHistoricalObservationStore.Retrieve(...)`, and `DatasetMaterialization` around existing filtering/snapshot construction. Do not modify `CreateSnapshot(...)`, interfaces, data contracts, source authority, or Infrastructure mechanics. |
| `tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs` | CREATE in WP02 only | Tests | Dedicated BCL listener/meter tests for WP02 source, activities, metrics, bounded attributes, no-listener equivalence, and exception propagation only. |
| Existing Infrastructure provider/storage adapter symbols | MODIFY only if WP03 authority names them | Infrastructure | Observe mechanics; no schema or behavior change. |
| Existing Worker/interop symbols | MODIFY only if WP04 authority names them | Infrastructure/Worker | Bounded lifecycle/exporter isolation; preserve JSON-over-stdio. |
| Existing Streamlit presentation entry point | MODIFY only if WP05 authority names it | Presentation | System Health only; no direct SQLite/provider access. |
| Dedicated WP06 test paths | CREATE only if WP06 authority names exact paths | Tests | Permanent deterministic no-bypass/lifecycle/security coverage. |
| Documentation paths | MODIFY only in the literal WP07 allowlist below, or another WP row that names an exact path | Documentation | Truthful setup/provenance/troubleshooting only. |

## Forbidden paths/actions

No ungoverned new source tree, generic bridge, parallel pipeline, provider credentials, live data fixtures, direct UI database access, schema/migration files, package changes before selection, generated/runtime artifacts, secrets, local signing configuration, or Release 1.9 historical rewrites. WP08 may stage/commit only an exact accepted release manifest on a dedicated branch under its own later authority.

## Path governance

Any implementation authority must stop before mutation if a required symbol/path is absent or shared ownership is ambiguous. Exact test counts and additional paths are selected by the relevant WP authority, not inferred from this planning manifest. No files are staged, committed, branched, pushed, or published by this Luna planning authority.

## WP-specific path enforcement

The execution plan is the semantic source; this table is the path gate. A Terra authority may modify or add only paths explicitly named by its accepted WP authority and within the listed concern. A missing exact symbol/path is a blocker, not permission to broaden scope.

| WP | Allowed concern-level paths | Forbidden |
|---|---|---|
| WP01 | `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md` and selection evidence | packages, exporters, source, runtime config |
| WP02 | `src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs` (ADD; internal BCL instrumentation helper); `src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionUseCase.cs` (MODIFY; `Execute(...)` and `ExecuteCanonical(...)` observation calls only); `src/AIQuantTradingResearch.Application/Datasets/MaterializeDatasetUseCase.cs` (MODIFY; two `Execute(...)` overloads only, exact retrieval/materialization stage timing); `tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs` (ADD; focused BCL activity/meter tests only) | Domain telemetry; any other Application file; interfaces/data contracts/private `CreateSnapshot(...)`; provider/storage types or instrumentation; Infrastructure/Worker/UI/Python paths; project/package files; exporter/SDK/hosting configuration; schema/migrations |
| WP03 | `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationStore.cs` — MODIFY `Retrieve(string target)` only; `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteDatasetSnapshotStore.cs` — MODIFY `Store(DatasetSnapshotCandidate)` and `Retrieve(DatasetSnapshotIdentity)` only; `tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs` and `tests/AIQuantTradingResearch.Infrastructure.Tests/SqliteDatasetTests.cs` — MODIFY focused telemetry tests only | `SqliteDatasetCatalog.cs`, `SqliteHistoricalObservationStore.Persist(...)`, schema/migrations, new providers, live endpoints, new helper files, project/package/configuration files |
| WP04 | `src/AIQuantTradingResearch.Worker/WorkerObservabilityLifecycle.cs` (ADD; internal BCL-only coordinator); `src/AIQuantTradingResearch.Worker/Program.cs` (MODIFY top-level composition only); `src/AIQuantTradingResearch.Infrastructure/PythonIntegration/PythonCapabilityInvoker.cs` (MODIFY existing bounded invocation method only); `tests/AIQuantTradingResearch.Infrastructure.Tests/WorkerObservabilityLifecycleTests.cs` (ADD); `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonCapabilityInvokerTests.cs` (MODIFY focused assertions only) | exporter packages/project files, generic bridge, protocol stdout, Streamlit supervision, Python/Streamlit production paths, schema/migrations, persistent telemetry backend |
| WP05 | `src/AIQuantTradingResearch.Application/Visualization/VisualizationReadModelContracts.cs` (`SystemHealthState`, `SystemHealthSnapshot`, `VisualizationReadModel.SystemHealth`); `src/AIQuantTradingResearch.Application/Visualization/VisualizationReadModelUseCase.cs` (existing publication boundaries only); `src/AIQuantTradingResearch.Infrastructure/Visualization/VisualizationReadModelFilePublisher.cs` (additive lowercase `systemHealth.state/provenance/reason` JSON only); `python/presentation/visualization_read_model.py` (optional health parsing and `HealthIntegrity`); `python/presentation/realtime_financial_visualization.py` (`VisualizationFrame` health projection and `render_visualization_frame` System Health message immediately after target/state subheader); existing `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelStoreTests.cs`, `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelFilePublisherTests.cs`, `python/presentation/test_visualization_read_model.py`, and `python/presentation/test_realtime_financial_visualization.py` | SQLite/provider access, new UI framework, Worker control, second handoff, schema/migration, independent clock/threshold, new test framework, direct telemetry/process inspection |
| WP06 | ADD `tests/AIQuantTradingResearch.Application.Tests/Release110ObservabilityPermanentTests.cs` (`Release110ObservabilityPermanentTests`); ADD `tests/AIQuantTradingResearch.Infrastructure.Tests/Release110ObservabilityPermanentTests.cs` (`Release110ObservabilityPermanentTests`); ADD `tests/AIQuantTradingResearch.Architecture.Tests/Release110ObservabilityNoBypassTests.cs` (`Release110ObservabilityNoBypassTests`); ADD `python/presentation/test_release_1_10_observability_no_bypass.py` (`Release110NoBypassTests`); existing WP02–WP05 tests are read-only corroborating evidence | all production files, all `.csproj`/packages, schema/migrations, runtime config, existing predecessor test edits, WP07/WP08 paths, generic helpers, network/live fixtures, exporter/backend |
| WP07 | MODIFY only `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md` (architecture/boundary sections); `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md` (setup, bounded local operations, troubleshooting, validation sections); and `docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md` (local security/signing cross-reference sections). All three are EXISTING. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`, `README.md`, Release 1.9 documents, and all other Markdown are read-only predecessor evidence. No NEW WP07 documentation path is authorized. | source/tests/packages/schema/Release 1.9 docs, README, OpenTelemetry selection, and all other docs; no new operations tree, no production claims, no executable test paths |
| WP08 | acceptance evidence and exact release manifest paths named by its authority | implementation, dependency/schema changes, lifecycle publication |

## Reconciled Release 1.10 publication candidate (literal)

The canonical publication candidate contains exactly 103 paths: 21 tracked changes and 82 untracked additions. It includes the two Git-publication authority prompt files as Release 1.10 governance documentation. The remote-base reconciliation pair and the later Terra publication-resumption pair are execution-control inputs and are explicitly excluded from publication; they remain preserved locally. The candidate is the following exhaustive literal set:

1. `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
2. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
3. `docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md`
4. `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`
5. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
6. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
7. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
8. `docs/roadmap/release-1.10/prompts/release-1.10-definition-planning-authority-chat-bootstrap.md`
9. `docs/roadmap/release-1.10/prompts/release-1.10-definition-planning-authority-codex-prompt.md`
10. `docs/roadmap/release-1.10/prompts/release-1.10-git-candidate-publication-pull-request-authority-chat-bootstrap.md`
11. `docs/roadmap/release-1.10/prompts/release-1.10-git-candidate-publication-pull-request-authority-codex-prompt.md`
12. `docs/roadmap/release-1.10/prompts/release-1.10-github-planning-materialization-authority-chat-bootstrap.md`
13. `docs/roadmap/release-1.10/prompts/release-1.10-github-planning-materialization-authority-codex-prompt.md`
14. `docs/roadmap/release-1.10/prompts/release-1.10-github-planning-materialization-authority-v2-chat-bootstrap.md`
15. `docs/roadmap/release-1.10/prompts/release-1.10-github-planning-materialization-authority-v2-codex-prompt.md`
16. `docs/roadmap/release-1.10/prompts/release-1.10-work-package-contract-manifest-completion-authority-chat-bootstrap.md`
17. `docs/roadmap/release-1.10/prompts/release-1.10-work-package-contract-manifest-completion-authority-codex-prompt.md`
18. `docs/roadmap/release-1.10/prompts/release-1.10-wp01-observability-selection-vocabulary-scope-authority-chat-bootstrap.md`
19. `docs/roadmap/release-1.10/prompts/release-1.10-wp01-observability-selection-vocabulary-scope-authority-codex-prompt.md`
20. `docs/roadmap/release-1.10/prompts/release-1.10-wp01-wp02-github-completion-authority-chat-bootstrap.md`
21. `docs/roadmap/release-1.10/prompts/release-1.10-wp01-wp02-github-completion-authority-codex-prompt.md`
22. `docs/roadmap/release-1.10/prompts/release-1.10-wp02-application-pipeline-observability-contract-authority-chat-bootstrap.md`
23. `docs/roadmap/release-1.10/prompts/release-1.10-wp02-application-pipeline-observability-contract-authority-codex-prompt.md`
24. `docs/roadmap/release-1.10/prompts/release-1.10-wp02-application-pipeline-observability-contract-authority-v2-chat-bootstrap.md`
25. `docs/roadmap/release-1.10/prompts/release-1.10-wp02-application-pipeline-observability-contract-authority-v2-codex-prompt.md`
26. `docs/roadmap/release-1.10/prompts/release-1.10-wp02-application-pipeline-observability-contract-authority-v3-chat-bootstrap.md`
27. `docs/roadmap/release-1.10/prompts/release-1.10-wp02-application-pipeline-observability-contract-authority-v3-codex-prompt.md`
28. `docs/roadmap/release-1.10/prompts/release-1.10-wp02-path-contract-reconciliation-authority-chat-bootstrap.md`
29. `docs/roadmap/release-1.10/prompts/release-1.10-wp02-path-contract-reconciliation-authority-codex-prompt.md`
30. `docs/roadmap/release-1.10/prompts/release-1.10-wp02-stage-boundary-reconciliation-authority-chat-bootstrap.md`
31. `docs/roadmap/release-1.10/prompts/release-1.10-wp02-stage-boundary-reconciliation-authority-codex-prompt.md`
32. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-environment-unblock-validation-authority-chat-bootstrap.md`
33. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-environment-unblock-validation-authority-codex-prompt.md`
34. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-gitleaks-environment-unblock-security-validation-authority-chat-bootstrap.md`
35. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-gitleaks-environment-unblock-security-validation-authority-codex-prompt.md`
36. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-infrastructure-instrumentation-contract-path-reconciliation-authority-chat-bootstrap.md`
37. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-infrastructure-instrumentation-contract-path-reconciliation-authority-codex-prompt.md`
38. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-infrastructure-provider-persistence-failure-instrumentation-authority-chat-bootstrap.md`
39. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-infrastructure-provider-persistence-failure-instrumentation-authority-codex-prompt.md`
40. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-infrastructure-provider-persistence-failure-instrumentation-authority-v2-chat-bootstrap.md`
41. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-infrastructure-provider-persistence-failure-instrumentation-authority-v2-codex-prompt.md`
42. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-v2-final-acceptance-resumption-2-authority-chat-bootstrap.md`
43. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-v2-final-acceptance-resumption-2-authority-codex-prompt.md`
44. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-v2-final-acceptance-resumption-authority-chat-bootstrap.md`
45. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-v2-final-acceptance-resumption-authority-codex-prompt.md`
46. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-v2-resumption-2-authority-chat-bootstrap.md`
47. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-v2-resumption-2-authority-codex-prompt.md`
48. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-v2-resumption-authority-chat-bootstrap.md`
49. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-v2-resumption-authority-codex-prompt.md`
50. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-v2-test-proof-completion-authority-chat-bootstrap.md`
51. `docs/roadmap/release-1.10/prompts/release-1.10-wp03-v2-test-proof-completion-authority-codex-prompt.md`
52. `docs/roadmap/release-1.10/prompts/release-1.10-wp04-worker-interop-lifecycle-exporter-isolation-authority-chat-bootstrap.md`
53. `docs/roadmap/release-1.10/prompts/release-1.10-wp04-worker-interop-lifecycle-exporter-isolation-authority-codex-prompt.md`
54. `docs/roadmap/release-1.10/prompts/release-1.10-wp04-worker-interop-lifecycle-exporter-isolation-authority-v2-chat-bootstrap.md`
55. `docs/roadmap/release-1.10/prompts/release-1.10-wp04-worker-interop-lifecycle-exporter-isolation-authority-v2-codex-prompt.md`
56. `docs/roadmap/release-1.10/prompts/release-1.10-wp04-worker-interop-lifecycle-exporter-isolation-contract-path-reconciliation-authority-chat-bootstrap.md`
57. `docs/roadmap/release-1.10/prompts/release-1.10-wp04-worker-interop-lifecycle-exporter-isolation-contract-path-reconciliation-authority-codex-prompt.md`
58. `docs/roadmap/release-1.10/prompts/release-1.10-wp05-system-health-read-model-streamlit-presentation-authority-chat-bootstrap.md`
59. `docs/roadmap/release-1.10/prompts/release-1.10-wp05-system-health-read-model-streamlit-presentation-authority-codex-prompt.md`
60. `docs/roadmap/release-1.10/prompts/release-1.10-wp05-system-health-read-model-streamlit-presentation-authority-v2-chat-bootstrap.md`
61. `docs/roadmap/release-1.10/prompts/release-1.10-wp05-system-health-read-model-streamlit-presentation-authority-v2-codex-prompt.md`
62. `docs/roadmap/release-1.10/prompts/release-1.10-wp05-system-health-read-model-streamlit-presentation-contract-path-reconciliation-authority-chat-bootstrap.md`
63. `docs/roadmap/release-1.10/prompts/release-1.10-wp05-system-health-read-model-streamlit-presentation-contract-path-reconciliation-authority-codex-prompt.md`
64. `docs/roadmap/release-1.10/prompts/release-1.10-wp05-system-health-semantic-presentation-contract-reconciliation-authority-v2-chat-bootstrap.md`
65. `docs/roadmap/release-1.10/prompts/release-1.10-wp05-system-health-semantic-presentation-contract-reconciliation-authority-v2-codex-prompt.md`
66. `docs/roadmap/release-1.10/prompts/release-1.10-wp06-permanent-observability-no-bypass-tests-authority-chat-bootstrap.md`
67. `docs/roadmap/release-1.10/prompts/release-1.10-wp06-permanent-observability-no-bypass-tests-authority-codex-prompt.md`
68. `docs/roadmap/release-1.10/prompts/release-1.10-wp06-permanent-test-path-architecture-security-symbol-reconciliation-authority-chat-bootstrap.md`
69. `docs/roadmap/release-1.10/prompts/release-1.10-wp06-permanent-test-path-architecture-security-symbol-reconciliation-authority-codex-prompt.md`
70. `docs/roadmap/release-1.10/prompts/release-1.10-wp07-documentation-developer-setup-operational-runbook-authority-chat-bootstrap.md`
71. `docs/roadmap/release-1.10/prompts/release-1.10-wp07-documentation-developer-setup-operational-runbook-authority-codex-prompt.md`
72. `docs/roadmap/release-1.10/prompts/release-1.10-wp07-documentation-developer-setup-operational-runbook-authority-resumption-chat-bootstrap.md`
73. `docs/roadmap/release-1.10/prompts/release-1.10-wp07-documentation-developer-setup-operational-runbook-authority-resumption-codex-prompt.md`
74. `docs/roadmap/release-1.10/prompts/release-1.10-wp07-documentation-path-allowlist-content-ownership-reconciliation-authority-chat-bootstrap.md`
75. `docs/roadmap/release-1.10/prompts/release-1.10-wp07-documentation-path-allowlist-content-ownership-reconciliation-authority-codex-prompt.md`
76. `docs/roadmap/release-1.10/prompts/release-1.10-wp08-full-validation-acceptance-pr-readiness-authority-chat-bootstrap.md`
77. `docs/roadmap/release-1.10/prompts/release-1.10-wp08-full-validation-acceptance-pr-readiness-authority-codex-prompt.md`
78. `python/presentation/realtime_financial_visualization.py`
79. `python/presentation/test_realtime_financial_visualization.py`
80. `python/presentation/test_release_1_10_observability_no_bypass.py`
81. `python/presentation/test_visualization_read_model.py`
82. `python/presentation/visualization_read_model.py`
83. `src/AIQuantTradingResearch.Application/Datasets/MaterializeDatasetUseCase.cs`
84. `src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionUseCase.cs`
85. `src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs`
86. `src/AIQuantTradingResearch.Application/Visualization/VisualizationReadModelContracts.cs`
87. `src/AIQuantTradingResearch.Application/Visualization/VisualizationReadModelUseCase.cs`
88. `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteDatasetSnapshotStore.cs`
89. `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationStore.cs`
90. `src/AIQuantTradingResearch.Infrastructure/PythonIntegration/PythonCapabilityInvoker.cs`
91. `src/AIQuantTradingResearch.Infrastructure/Visualization/VisualizationReadModelFilePublisher.cs`
92. `src/AIQuantTradingResearch.Worker/Program.cs`
93. `src/AIQuantTradingResearch.Worker/WorkerObservabilityLifecycle.cs`
94. `tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs`
95. `tests/AIQuantTradingResearch.Application.Tests/Release110ObservabilityPermanentTests.cs`
96. `tests/AIQuantTradingResearch.Architecture.Tests/Release110ObservabilityNoBypassTests.cs`
97. `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonCapabilityInvokerTests.cs`
98. `tests/AIQuantTradingResearch.Infrastructure.Tests/Release110ObservabilityPermanentTests.cs`
99. `tests/AIQuantTradingResearch.Infrastructure.Tests/SqliteDatasetTests.cs`
100. `tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs`
101. `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelFilePublisherTests.cs`
102. `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelStoreTests.cs`
103. `tests/AIQuantTradingResearch.Infrastructure.Tests/WorkerObservabilityLifecycleTests.cs`

The required staging boundary is exactly this 103-path list. Exclude ignored local configuration (including Directory.Build.local.props), signing secrets/private keys, generated binaries, runtime/test residue, and these four preserved execution-control inputs:

- `docs/roadmap/release-1.10/prompts/release-1.10-remote-base-publication-authority-artifact-reconciliation-authority-codex-prompt.md`
- `docs/roadmap/release-1.10/prompts/release-1.10-remote-base-publication-authority-artifact-reconciliation-authority-chat-bootstrap.md`
- `docs/roadmap/release-1.10/prompts/release-1.10-git-candidate-publication-pull-request-authority-resumption-codex-prompt.md`
- `docs/roadmap/release-1.10/prompts/release-1.10-git-candidate-publication-pull-request-authority-resumption-chat-bootstrap.md`

The two Git-publication authority files are included; the two remote-base reconciliation files, two Terra resumption files, and this repair authority's two prompt files are excluded as paired execution-control artifacts. Every numbered entry above is a repository path only. No wildcard or indiscriminate staging is allowed.

## Materialization contract fields

Every generated GitHub issue must reproduce from the execution plan: exact WP title/objective; in/out scope; direct dependencies; architecture/provenance rules; bounded path ownership; measurable acceptance; validation categories; security requirements; Luna/Terra/Sol assignments; selected execution model; and completion boundary. The plan's eight-WP order is the only dependency topology. No issue body may add a package, schema, provider, endpoint, test count, or filename not supported by a later narrow authority.
