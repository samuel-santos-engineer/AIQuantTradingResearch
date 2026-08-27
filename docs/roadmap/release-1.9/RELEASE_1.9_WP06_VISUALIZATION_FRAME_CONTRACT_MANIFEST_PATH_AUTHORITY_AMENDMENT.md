# Release 1.9 WP06 - Visualization Frame Contract and Manifest/Path Authority Amendment

Status: normative, definition-only supplement for WP06 issue #231. This
document authorizes no implementation, test execution, GitHub mutation, issue
closure, or WP07 work.

## Predecessor evidence and preserved ownership

WP05 (#230) is Closed/Done and WP06 (#231) and WP07 (#232) are Open/Backlog.
The accepted WP04 envelope and WP05 consumer remain authoritative. The
envelope contract is `aiq-visualization-read-model-v1`, its observation window
is ordered oldest-to-newest with capacity 64, and WP05 remains the owner of
atomic transport, parsing, revision acceptance, retry, cache, refresh, and
transport-warning semantics. `visualization_read_model.py` and
`test_visualization_read_model.py` remain WP05-exclusive. The existing
`test_realtime_financial_visualization.py` remains WP09-owned. The current
shared Streamlit file contains `render()` and its module-entry invocation;
those existing WP05 refresh/transport symbols remain intact.

WP07 (#232) owns only the later feature/data-quality presentation described by
its accepted objective: `simple-return-lag-1-v1` values/warm-up and supported
snapshot, validation, pipeline, and idempotency state. WP06 must not implement
or semantically specify those WP07-owned additions beyond reserving them.

## Deterministic `VisualizationFrame` contract

One frame is an immutable presentation projection of one currently accepted
WP05 consumer snapshot plus the current transport warning, if any. It does
not create a revision, sequence number, history, clock value, query, or second
data model. The frame is derived only from the accepted envelope and transport
status.

### Identity and backend state

The frame preserves, exactly, the envelope contract/revision kind, revision
value and identity, source mode, source authority, target, and the exact WP04
presentation state. Historical and Replay revision kinds remain distinct.

### Price/time series

The frame contains the envelope observation window one-for-one: source
timestamp and decimal price only, in the already-governed oldest-to-newest
order. It contains at most 64 points and performs no sorting, interpolation,
resampling, synthetic-point creation, or price transformation.

### Latest and count/window

`latestObservation` is the exact envelope latest timestamp and decimal price,
or absent when the envelope has no latest observation. When the series is
non-empty it must equal the final series point. A mismatch is an integrity
error, not a repair opportunity.

The frame exposes the canonical envelope `observationCount`, derived
`windowCount` equal to the series length, and `windowCapacity` equal to 64.
The invariant is `0 <= windowCount <= min(observationCount, 64)`; under normal
accepted-envelope semantics `windowCount == min(observationCount, 64)`.

### WP06-supported metadata

WP06 may expose only existing envelope metadata needed to describe the current
read model: feature identity/availability and an already-supplied latest
feature value or WarmUp required-observation count; high-level pipeline/status
summary; and validation/quality status. No internal evidence structure or
derived value may be added.

### Production and transport semantics

Ready renders the available series, latest/count/window, and allowed metadata.
WarmUp renders existing observations and feature identity/state plus the
already-supplied required count, without fabricating a value. Empty renders no
series and no latest while preserving the supplied count. Failed renders only
the envelope's safe failure category/message/recoverability. Stale renders
the retained payload exactly with backend state Stale; no wall-clock threshold
is invented.

`ProducerUnavailable` and read warnings are transport-local and remain
separate from backend state. With no accepted envelope there is no backend
payload. With a last-good envelope, its payload remains the frame while the
transport warning is separately present.

For a newer accepted envelope, project a new frame. An equivalent envelope
produces an equivalent frame with no synthetic animation or sequence number.
An older envelope does not replace the current frame. A revision conflict
retains the last-good frame and surfaces the existing consumer integrity
warning. No new backend Failed state is created.

## Integrity and deterministic assertion surface

The future WP06 projector must reject, without reinterpretation, a series over
64 points, envelope-order divergence, latest/series mismatch, invalid revision,
invalid state, or an invalid source-authority value. It preserves the last
valid frame where one exists and surfaces a safe frame-integrity error.

The dedicated tests must assert exact timestamps, decimal prices, point count,
latest, observation count, window count/capacity, revision identity, state,
source mode/authority/target, and allowed metadata. They must cover one and two
observations, sequential append, more-than-64 canonical windows, equivalent,
older, and conflicting revisions, distinct Historical/Replay kinds, all
Ready/WarmUp/Empty/Failed/Stale states, ProducerUnavailable with and without
last-good payload, and integrity rejection. Assertions are functional data
assertions; no screenshot, pixel, timing, animation, or browser harness is
authorized.

## Minimum rendering contract

The shared Streamlit entry point may feed the frame's ordered price/time points
directly to one existing Streamlit-native line-chart or equivalent primitive,
and may display deterministic latest timestamp/price, canonical observation
count, bounded window count/capacity, allowed metadata, backend state, and
separate transport warnings. No styling, theme, layout expansion, controls
beyond the existing WP05 refresh, chart interaction, or visual reconstruction
is defined.

## Narrow WP06 path authority

Exactly these paths are authorized for later WP06 implementation:

| Path | Ownership | WP06-authorized concern | Forbidden adjacent concern |
| --- | --- | --- | --- |
| `python/presentation/realtime_financial_visualization.py` | Shared WP05-WP07; symbol-level WP06 exception | Add only pure frame data/projection symbols and the render symbols needed for price/time, latest, count/window, allowed metadata, truthful backend state, and separate transport warning; preserve WP05 cache, refresh, transport, and entry-point behavior | WP07 metadata/panels, new controls, transport, persistence/provider access, feature recomputation, styling expansion, or Worker control |
| `python/presentation/test_realtime_financial_visualization_wp06.py` | Exclusive WP06 | Executable `unittest`-based deterministic frame/render-input tests using the repository's no-pytest convention | WP05 consumer tests, WP09 Streamlit tests, screenshots/pixels, browser automation, or broad shared test coverage |

No additional helper path is authorized. In particular,
`python/presentation/visualization_read_model.py` remains outside WP06
mutation authority.

## Explicit exclusions

WP06 may not change Worker, Application, Domain, Infrastructure, schema,
migrations, persistence, providers, pipeline execution, feature computers,
the WP04 envelope, WP05 atomic JSON transport or cache/retry semantics,
requirements, Streamlit pins, JSON-over-stdio, WP05/WP09 test paths, or any
WP07+ path. No dependency, package, protocol, or GitHub lifecycle mutation is
authorized. The next implementation authority must stop if this allowlist is
insufficient.

`WP06 VISUALIZATION-FRAME/MANIFEST AMENDMENT MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

`WP06 VISUALIZATION-FRAME CONTRACT AND PATH AUTHORITY DEFINED - IMPLEMENTATION REQUIRES FRESH AUTHORITY`
