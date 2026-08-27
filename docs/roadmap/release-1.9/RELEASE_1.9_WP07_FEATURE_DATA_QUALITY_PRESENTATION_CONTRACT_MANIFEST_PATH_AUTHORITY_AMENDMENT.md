# Release 1.9 WP07 — Feature/Data-Quality Presentation Contract and Path Authority

Status: normative definition-only authority. Implementation requires a fresh WP07 authority.

## Binding predecessor

This contract consumes `VisualizationFrame` from the completed WP06 implementation and the canonical `PresentationIdempotencyStatus` and `PresentationDataQualityStatus` exposure. It adds no upstream semantics.

The existing WP06 chart, latest observation, observation counts/window, and transport warning are rendered first and remain unchanged. WP07 adds the following ordered sections after that existing content:

1. `Feature`
2. `Snapshot`
3. `Data Quality`
4. `Pipeline`
5. `Idempotency`

Each section is a deterministic sequence of key/value rows. No CSS, icons, cards, extra controls, screenshots, or layout framework is authorized.

## Exact factual rows

Rows are emitted in the following order, using the exact labels shown. Every value is copied from the frame; no value is recomputed.

### Feature

| Label | Frame source | Representation |
|---|---|---|
| `Feature identity` | `feature_identity` | text, or `Unavailable` when absent |
| `Feature value` | `feature_value` | invariant decimal with `.`; `Unavailable` when absent |
| `Observed / required` | `feature_observation_count`, `feature_required_observation_count` | invariant integers as `<observed> / <required>`, or `Unavailable` when either is absent |

For WarmUp, the identity and `Observed / required` rows remain factual and `Feature value` is `Unavailable`. No feature formula or validity inference is permitted.

### Snapshot

| Label | Frame source | Representation |
|---|---|---|
| `Snapshot identity` | additive frame snapshot identity metadata, when exposed by the accepted predecessor | full lowercase fingerprint text; `Unavailable` when absent |
| `Snapshot version` | additive frame snapshot version metadata, when exposed by the accepted predecessor | full lowercase fingerprint text; `Unavailable` when absent |

No abbreviation, SQLite record, provider payload, file path, credential, or arbitrary provenance is displayed. If the accepted frame does not carry either value, the row is `Unavailable`; no fallback is inferred from revision identity.

### Data Quality

| Label | Frame source | Representation |
|---|---|---|
| `Validation status` | `data_quality_status` | exact canonical value: `Valid`, `Invalid`, or `Unavailable` |

This is categorical only. No score, percentage, confidence, severity, threshold, freshness, completeness, provider-accuracy, or UI warning is displayed as quality.

### Pipeline

| Label | Frame source | Representation |
|---|---|---|
| `Pipeline status` | `pipeline_success` | `Success`, `Failure`, or `Unavailable`; `Success`/`Failure` are the direct boolean values and `Unavailable` means absent |
| `Backend state` | `state` | exact WP04 state value: `Ready`, `WarmUp`, `Empty`, `Failed`, or `Stale` |
| `Failure category` | `failure_category` | exact safe category, or `Unavailable` when absent |

`Failure message` is not part of the WP07 rows; existing WP06 safe failure behavior remains authoritative. No pipeline stages are displayed and pipeline success is not converted into data quality.

### Idempotency

| Label | Frame source | Representation |
|---|---|---|
| `Persistence disposition` | `idempotency_status` | exact canonical value: `NewlyPersisted`, `EquivalentExisting`, or `Unavailable` |

`NewlyPersisted` is not “non-idempotent”; `EquivalentExisting` is not a cache hit, duplicate tick, stale result, or retry. No derived indicator is authorized.

## Formatting and unavailable token

The exact unavailable token for absent optional values and canonical `Unavailable` statuses is `Unavailable`. It is never rendered blank or as null. Status strings are case-sensitive and invariant. Decimal values use invariant `.` notation with the frame’s exact decimal value and no rounding or locale conversion. Counts are base-10 invariant integers. Timestamps continue to use the frame’s exact ISO-8601 source-time text; WP07 adds no timestamp row.

## Backend-state behavior

| State | Required behavior |
|---|---|
| `Ready` | Render all five sections; show available feature, snapshot, pipeline, data-quality, and idempotency facts directly. |
| `WarmUp` | Render all five sections; feature value is `Unavailable` when absent; data quality is not changed to `Invalid`; other facts follow the frame or `Unavailable`. |
| `Empty` | Render all five sections with absent optional values as `Unavailable`; do not fabricate feature, snapshot, pipeline, quality, or idempotency facts. |
| `Failed` | Render all five section headings; show `Unavailable` for idempotency and data quality unless the frame carries the canonical failure-defined value; preserve the existing safe failure output and do not reinterpret unrelated failure as `Invalid`. |
| `Stale` | Render the retained last-good factual rows exactly, including statuses; the state row remains `Stale`. No new fact is inferred. |

The semantic-definition matrix governs the exact status availability: stale retains the last complete facts; transport warnings never alter them.

## Transport-warning boundary

The existing transport warning is shown before the WP07 sections using the existing WP06 warning mechanism. It remains transport-local and separate from all rows. `ProducerUnavailable` is not a backend state and is not substituted for any WP07 row. A last-good frame retains its factual rows while the warning is shown separately. No refresh, retry, cache, revision, or lifecycle behavior changes.

## Deterministic assertion surface

The later implementation may add one pure function in `python/presentation/realtime_financial_visualization.py`:

`project_wp07_presentation_sections(frame: VisualizationFrame) -> tuple[tuple[str, tuple[tuple[str, str], ...]], ...]`

It must return exactly five section tuples in the order `Feature`, `Snapshot`, `Data Quality`, `Pipeline`, `Idempotency`, with the exact row labels/order above and invariant string values. It must not call Streamlit, read files, access SQLite/providers, derive values, or mutate the frame. Existing `render_visualization_frame` behavior before the WP07 sections is unchanged; a later implementation may render these returned rows only after existing WP06 content.

## Shared-file path exception

Only `python/presentation/realtime_financial_visualization.py` is affected. Existing WP05/WP06 ownership remains intact. WP07 may add only `project_wp07_presentation_sections` and the narrow render integration for the five defined sections/rows. It may not change the chart, price/time, latest, counts/window, feature computation, frame fields, backend state, transport warning, cache, retry, or revision semantics.

## Dedicated WP07 presentation test path

The reserved path is now activated exclusively for this contract:

`python/presentation/test_realtime_financial_visualization_wp07.py`

It may assert exact section order, labels, row order, formatting, all five backend states, all canonical status values, feature/snapshot/pipeline availability, stale retention, and transport-warning separation through the pure projection. It must not test lifecycle/demonstration (WP08), permanent integration/architecture concerns (WP09), screenshots, browser automation, or the semantic-exposure test path.

## Future acceptance gates

The later implementation must run the dedicated WP07 presentation tests, semantic-exposure tests 2/2, WP06 6/6, WP05 3/3, compilation, Streamlit 1.61.1 smoke, `pip check`, build 0/0, and full .NET regression from the 309/309 predecessor baseline. No package, schema, persistence, validation, transport, state, revision, chart, WP08, or WP09 changes are allowed. #232 remains Open / Backlog until a later implementation authority completes acceptance.

`WP07 FEATURE/DATA-QUALITY PRESENTATION CONTRACT/PATH AMENDMENT MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

`WP07 FEATURE/DATA-QUALITY PRESENTATION CONTRACT/PATH AUTHORITY DEFINED — CONSOLIDATED WP07 IMPLEMENTATION REQUIRES FRESH AUTHORITY`
