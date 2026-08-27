# Release 1.9 WP07 — Canonical Idempotency and Data-Quality Semantics

Status: definition-only authority. Implementation requires a fresh WP07 authority.

## Selected idempotency primitive

`PresentationIdempotencyStatus` is the disposition of the canonical pipeline-result persistence operation for the current pipeline execution and the result presented by WP04. Its scope is the current canonical execution/result envelope only; it makes no cross-session or replay-wide claim.

The source is `PipelineExecutionResult.Disposition`, populated by the canonical `PipelineExecutionUseCase.ExecuteCanonical` path from the snapshot-persistence outcome. The immutable application projection is optional when no successful persistence disposition exists.

Allowed values are:

- `NewlyPersisted`: the canonical pipeline result established a new persisted snapshot/result.
- `EquivalentExisting`: persistence resolved to an already-existing semantically equivalent canonical result.
- `Unavailable`: no successful pipeline-result persistence disposition exists for the presented state.

`NewlyPersisted` is not “not idempotent”; it describes first acceptance. This primitive is not observation-level `ObservationPersistenceOutcome.Idempotent`, duplicate observation replacement, replay logical-tick equality, historical revision equality, snapshot identity equality alone, pipeline success alone, cache hit, unchanged JSON, transport retry, or Streamlit refresh equivalence.

## Selected data-quality primitive

`PresentationDataQualityStatus` reports whether the canonical validation contract for the pipeline result accepted the data, rejected it, or produced no applicable validation result. It is categorical and factual; it is not an assessment of market quality.

The source is the canonical pipeline validation/failure evidence: a governed validation rejection maps to `Invalid`, an accepted complete pipeline validation maps to `Valid`, and no applicable validation evidence maps to `Unavailable`. Existing `PipelineFailureCategory.InvalidInput` and `InvalidEvidence` are validation-related only when the failing operation is the canonical validation/data acceptance operation; unrelated dependency or persistence failures do not become `Invalid`.

Allowed values are `Valid`, `Invalid`, and `Unavailable`. No score, percentage, confidence, severity, threshold, freshness, completeness inference, provider-accuracy claim, profitability claim, or UI-generated warning is permitted.

## Backend-state matrix

| WP04 state | Idempotency | Data quality |
|---|---|---|
| Ready | `NewlyPersisted` or `EquivalentExisting`, directly from the current successful persistence disposition | `Valid` only when canonical validation accepted; otherwise `Unavailable` |
| WarmUp | `NewlyPersisted` or `EquivalentExisting` if the current result was successfully persisted; otherwise `Unavailable` | `Valid` if the canonical validation accepted the result; never `Invalid` merely because the feature is warming up; otherwise `Unavailable` |
| Empty | `Unavailable` unless a current canonical operation actually produced both facts | `Unavailable` unless a canonical validation result exists |
| Failed | `Unavailable` | `Invalid` only when canonical validation itself rejected/failed; `Unavailable` for dependency, persistence, transport, or other unrelated failure |
| Stale | Retain the last complete payload’s fact under WP04 last-good retention; no new fact is inferred | Retain the last complete payload’s fact; stale status does not change quality |

Transport warnings do not alter either primitive.

## Minimum additive exposure

The future implementation shall add immutable, nullable/tagged fields without changing existing fields or state/revision semantics:

1. Application: expose `PresentationIdempotencyStatus?` and `PresentationDataQualityStatus?` on `PipelineExecutionResult`/`PipelineExecutionEvidence`, sourced directly from canonical persistence and validation evidence.
2. WP04: carry the two optional facts on the immutable `VisualizationReadModel`; do not add a state, revision kind, persistence requirement, or schema field.
3. JSON: serialize optional `idempotencyStatus` and `dataQualityStatus` values in the existing `aiq-visualization-read-model-v1` envelope. These are additive optional members; absent members remain `Unavailable`. This is backward-compatible provided the accepted v1 policy continues to ignore unknown members, and the parser must be updated under a later implementation authority to preserve exact values.
4. WP05: parse the two members directly, with no derivation and no cache/retry/transport changes.
5. WP06: add only optional immutable factual metadata to the existing `VisualizationFrame`; preserve revision, price/time, latest, count, window, feature, and existing state semantics. No second frame or side channel.
6. WP07: map each value 1:1 to stable factual terminology in the Snapshot/Data Quality/Pipeline/Idempotency presentation sections; no inference.

The two primitives do not require schema or persistence-behavior changes. A later implementation authority must confirm the accepted v1 additive-member policy before changing serialization; if that policy does not permit additive members, a separate envelope-version authority is required.

## Future implementation surface

The narrow surface is `PipelineExecutionResult`, `PipelineExecutionEvidence`, `VisualizationReadModel`, `VisualizationReadModelFilePublisher`, the WP05 parser/read model, `VisualizationFrame`/its projection, and one dedicated WP07 test path. Exact manifest ownership and path additions require a separate path amendment before implementation. No existing WP02–WP06 semantics are redesigned.

## Required future tests

Tests must cover both persistence dispositions and `Unavailable`; distinguish observation idempotency; prove no inference from revision, cache, retry, or transport; map accepted validation to `Valid`, canonical validation rejection to `Invalid`, unrelated failure to `Unavailable`, WarmUp not to `Invalid`, and Stale retention; and verify exact Application → WP04 → JSON → Python → WP06 → WP07 preservation. Existing WP02/WP03/WP04/WP05/WP06 regressions, schema v4, and the 305/305 baseline remain required.

## Non-goals

No composite system idempotency, new persistence behavior, schema v5, replay semantics, transport/cache semantics, new WP04 state/revision, quality scoring, UI implementation, WP08, or WP09 is defined here.
