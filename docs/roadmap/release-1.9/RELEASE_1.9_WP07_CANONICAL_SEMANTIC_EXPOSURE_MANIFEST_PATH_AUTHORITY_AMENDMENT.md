# Release 1.9 WP07 — Canonical Semantic-Exposure Manifest/Path Authority Amendment

Status: normative definition-only path amendment. It authorizes no implementation, test execution, GitHub mutation, or WP07 rendering.

## Binding semantic authority

The binding source is `RELEASE_1.9_WP07_CANONICAL_IDEMPOTENCY_DATA_QUALITY_SEMANTIC_DEFINITION.md`.

`PresentationIdempotencyStatus` has exactly `NewlyPersisted`, `EquivalentExisting`, and `Unavailable` values and is scoped to the current canonical pipeline-result persistence disposition. `PresentationDataQualityStatus` has exactly `Valid`, `Invalid`, and `Unavailable` values and reports only the canonical validation outcome. Neither may be inferred from revisions, replay ticks, cache/retry behavior, transport, observation-level idempotency, or generic pipeline success.

## Exact production exposure exceptions

The existing owner remains responsible for each path. WP07 receives only the listed symbol/concern exception; ownership is not transferred.

| Exact path | Existing owner | WP07 shared exception | Forbidden |
|---|---|---|---|
| `src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionResult.cs` | WP03 | Add immutable optional `PresentationIdempotencyStatus` and `PresentationDataQualityStatus` projections from existing canonical evidence; preserve result/failure behavior | Persistence behavior, validators, stages, Replay semantics, schema |
| `src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionEvidence.cs` | WP03 | Add direct immutable exposure of the same two facts | New evidence computation, stage changes, UI labels |
| `src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionUseCase.cs` | WP03 | Map existing pipeline persistence/validation evidence to the two statuses only | New persistence/validation operations, transaction/order changes, feature work |
| `src/AIQuantTradingResearch.Application/Visualization/VisualizationReadModelContracts.cs` | WP04 | Carry two optional immutable factual fields on `VisualizationReadModel` and preserve the binding state matrix | State/revision/window changes, schema, persistence, feature changes |
| `src/AIQuantTradingResearch.Application/Visualization/VisualizationReadModelUseCase.cs` | WP04 | Pass through the two facts without deriving or changing state retention | New state, revision, cache, replay, or feature semantics |
| `src/AIQuantTradingResearch.Infrastructure/Visualization/VisualizationReadModelFilePublisher.cs` | WP05 | Serialize only `idempotencyStatus` and `dataQualityStatus` as optional members of `aiq-visualization-read-model-v1` | Atomic-write, path, lifecycle, retry, or transport changes |
| `python/presentation/visualization_read_model.py` | WP05 | Parse and validate the two values directly; preserve absent members as `Unavailable` per binding authority | Read attempts, retry/cache/revision behavior, warnings, transport version policy |
| `python/presentation/realtime_financial_visualization.py` | WP06 | Add only optional immutable factual fields to `VisualizationFrame` and direct frame construction propagation | Chart, price/time, latest/count/window, feature, state, warning, or WP07 rendering changes |

No new production helper path is authorized. These exceptions do not authorize changing the accepted v1 contract version; if the actual compatibility policy rejects additive members, implementation must stop for a separate version authority.

## Exact focused test paths

The following dedicated paths are authorized for semantic-exposure tests only:

| Exact path | Ownership | Scope | Forbidden |
|---|---|---|---|
| `tests/AIQuantTradingResearch.Application.Tests/PresentationSemanticExposureTests.cs` | WP07 semantic exposure | Immutable Application projections, source mappings, value domains, and state availability | Persistence redesign, schema, WP08/WP09 |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationSemanticExposureTests.cs` | WP07 semantic exposure | WP04 envelope and Worker JSON production composition, including exact additive fields | Transport/lifecycle redesign, WP09 ownership |
| `python/presentation/test_visualization_semantic_exposure_wp07.py` | WP07 semantic exposure | Direct parsing, unavailable/backward behavior, frame propagation, and preservation of WP06 semantics | Sections/rendering, screenshots, WP05 consumer suite, WP06 suite, WP09 |

These are exact new files, not wildcard grants. No existing predecessor test ownership is broadened.

## Reserved later presentation test

`python/presentation/test_realtime_financial_visualization_wp07.py` remains reserved for the later WP07 feature/data-quality presentation authority. It is not authorized for this semantic-exposure implementation.

## Exposure chain

Only this chain is authorized:

`PipelineExecutionUseCase.ExecuteCanonical` existing persistence/validation evidence → `PipelineExecutionResult` / `PipelineExecutionEvidence` → `VisualizationReadModel` / WP04 use case → `VisualizationReadModelFilePublisher` JSON → WP05 parser → additive `VisualizationFrame` metadata.

All values remain direct factual projections. No new persistence behavior, schema version, pipeline stage, Replay behavior, transport behavior, WP04 state/revision, WP06 chart semantics, or UI section is authorized.

## Stop rules

Later implementation must stop for any path not listed above, any change to source outcomes or validation behavior, schema/persistence change, new envelope version, WP04 state/revision change, WP05 cache/retry/lifecycle change, WP06 chart change, WP07 rendering, or WP08/WP09 work.

`WP07 CANONICAL SEMANTIC-EXPOSURE MANIFEST/PATH AMENDMENT MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

`WP07 CANONICAL SEMANTIC-EXPOSURE PATH AUTHORITY AMENDED — EXPOSURE IMPLEMENTATION REQUIRES FRESH AUTHORITY`
