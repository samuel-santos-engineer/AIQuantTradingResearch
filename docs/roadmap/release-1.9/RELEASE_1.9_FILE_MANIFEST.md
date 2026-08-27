# Release 1.9 — Real-Time Financial Data Visualization — File Manifest

## Rules

This manifest governs future implementation only after separate acceptance and
GitHub planning. Its predecessor is `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`.
`CREATE` paths are intentional; no unlisted structural root is allowed without
corrective authority. The canonical persistence schema remains v4. `requirements.txt`, `.csproj` files,
package references, `.venv`, and machine Python are VERIFY-only.

## Planning

| Path | Action | Owner | Purpose |
| --- | --- | --- | --- |
| `docs/roadmap/release-1.9/RELEASE_1.9_DEFINITION.md` | CREATE/VERIFY | Planning | Scope and boundaries. |
| `docs/roadmap/release-1.9/RELEASE_1.9_EXECUTION_PLAN.md` | CREATE/VERIFY | Planning | WP graph. |
| `docs/roadmap/release-1.9/RELEASE_1.9_FILE_MANIFEST.md` | CREATE/VERIFY | Planning | Ownership freeze. |
| `docs/roadmap/release-1.9/prompts/` | VERIFY | Planning | Authorities, not implementation. |

## Replay and existing pipeline

| Path | Action | Owner | Purpose |
| --- | --- | --- | --- |
| `src/AIQuantTradingResearch.Infrastructure/Research/SimulatedLiveObservationSource.cs` | CREATE | WP02 | Deterministic incremental `IObservationSource`. |
| `src/AIQuantTradingResearch.Infrastructure/Research/SimulatedLiveReplayConfiguration.cs` | CREATE | WP02 | Validated replay settings. |
| `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs` | MODIFY | WP02 | Register adapter without layer change. |
| `src/AIQuantTradingResearch.Application/Pipelines/IncrementalPipelineExecutionUseCase.cs` | CREATE | WP03 | Orchestrate existing pipeline contracts only. |
| `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs` | CREATE | WP03/WP08 | Owned replay/pipeline/presentation lifecycle. |
| `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationConfiguration.cs` | CREATE | WP03/WP08 | Validated Worker mode configuration. |
| `src/AIQuantTradingResearch.Worker/Program.cs` | MODIFY | WP03/WP05 | Explicit composition/mode selection only. |

## Presentation boundary

| Path | Action | Owner | Purpose |
| --- | --- | --- | --- |
| `src/AIQuantTradingResearch.Application/Visualization/VisualizationReadModelContracts.cs` | CREATE | WP04 | Bounded technology-neutral display contract. |
| `src/AIQuantTradingResearch.Application/Visualization/VisualizationReadModelUseCase.cs` | CREATE | WP04 | Map established evidence to display state. |
| `src/AIQuantTradingResearch.Infrastructure/Visualization/AtomicVisualizationReadModelStore.cs` | CREATE | WP04 | Atomic disposable handoff; never UI SQLite. |
| `src/AIQuantTradingResearch.Infrastructure/Visualization/VisualizationReadModelStorageConfiguration.cs` | CREATE | WP04 | Validated local runtime location. |
| `python/presentation/realtime_financial_visualization.py` | CREATE | WP05–WP07 | Minimal Streamlit adapter using only read model. |
| `python/presentation/visualization_read_model.py` | CREATE | WP05 | Read-model parsing/validation; no provider/SQLite/features. |

## Tests and documentation

| Path | Action | Owner | Purpose |
| --- | --- | --- | --- |
| `tests/AIQuantTradingResearch.Application.Tests/VisualizationReadModelTests.cs` | CREATE | WP04/WP09 | Contracts, warm-up, and failures. |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/SimulatedLiveObservationSourceTests.cs` | CREATE | WP02/WP09 | Ordering, restart, bounds, cancellation. |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelStoreTests.cs` | CREATE | WP04/WP09 | Atomic handoff/cleanup. |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/SimulatedLiveVisualizationExecutionTests.cs` | CREATE | WP03/WP08/WP09 | Finite end-to-end lifecycle. |
| `tests/AIQuantTradingResearch.Architecture.Tests/VisualizationBoundaryRulesTests.cs` | CREATE | WP09 | No Streamlit/provider/SQLite leakage. |
| `python/presentation/test_realtime_financial_visualization.py` | CREATE | WP09 | Governed deterministic Streamlit test; no pytest. |
| `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md` | VERIFY/MODIFY | WP10 | Change only for delivered truth. |
| `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md` | VERIFY/MODIFY | WP10 | Portable execution/isolation truth. |
| `README.md`, `docs/project/ROADMAP.md` | VERIFY/MODIFY | WP10 | Delivered capability only after acceptance. |

## Prohibited or verify-only content

- SQLite bootstrap/schema/store files remain VERIFY-only at schema v4; any
  migration stops work for separate authority.
- `python/validation/` remains Release 1.8 non-production validation and is
  never the product presentation root.
- No model/prediction artifacts, real-provider fixtures, credentials, browser
  automation, OpenTelemetry exporter/backend, backtesting code, or generated
  handoff/database/runtime files may be committed.
