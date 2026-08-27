# Release 1.9 — WP06 Visualization-Frame Contract + Manifest/Path-Authority Amendment — Codex Prompt

## Authority

This is a **narrow, definition-only authority** for Release 1.9 WP06, canonical issue **#231**.

Use **GPT-5.6 Luna**.

WP06 is currently blocked before mutation because:

1. #231 requires deterministic sequential visualization frames for evolving price/time, latest observation, count/window, and supported metadata, but the accepted artifacts do not define the exact frame/render/assertion contract; and
2. the manifest gives WP06 no dedicated test path, while:
   - `python/presentation/realtime_financial_visualization.py` is shared across WP05–WP07;
   - the existing WP05 test path is WP05-exclusive;
   - the Streamlit test path identified in the repository is WP09-owned.

This authority must resolve **only** those two gaps:

- define the deterministic WP06 visualization-frame contract;
- amend WP06 manifest/path authority narrowly enough to implement and test that contract later.

No production implementation is authorized.

No test implementation is authorized.

No GitHub lifecycle mutation is authorized.

Do not start WP07.

---

# Entry State

Expected:

- WP05 #230: Closed / Done
- WP06 #231: Open / Backlog
- WP07 #232: Open / Backlog
- milestone #58: Open
- immediate predecessor full regression: **305/305**
- build predecessor: **0 errors / 0 warnings**
- WP05 local atomic JSON handoff accepted
- WP05 bounded read-only Streamlit consumer accepted
- WP04 immutable presentation read model remains canonical

Preserve all predecessor contracts.

---

# Fixed Predecessor Inputs

Do not redefine these.

## WP04 envelope

The consumer receives the immutable versioned read model with:

- `contractVersion = aiq-visualization-read-model-v1`;
- revision;
- source mode;
- source authority;
- target;
- dataset snapshot identity/version when available;
- presentation state;
- bounded ordered observation window;
- latest observation;
- observation count;
- feature identity/value or WarmUp metadata;
- pipeline/status;
- validation/quality;
- safe failure;
- stale metadata.

Observation window capacity is fixed at **64**.

Ordering is oldest → newest.

WP06 must not create a second data model.

## WP05 consumer

Preserve:

- atomic JSON transport;
- one last-good envelope;
- revision acceptance/rejection;
- ProducerUnavailable semantics;
- transport warnings separate from backend state;
- automatic/manual bounded refresh;
- no SQLite;
- no provider;
- no feature recomputation;
- no Worker process control.

WP06 frames are presentation projections of accepted WP05 consumer state.

---

# Objective

Define one deterministic, testable WP06 frame contract sufficient for #231.

The contract must answer, without implementation ambiguity:

1. What is one visualization frame?
2. When is a new frame produced?
3. What exact price/time series does the frame contain?
4. What exact latest-observation fields does it contain?
5. What exact count/window fields does it contain?
6. Which metadata is WP06-owned?
7. Which metadata is explicitly reserved for WP07 or later?
8. How are backend non-Ready states represented?
9. How are transport warnings represented?
10. How are sequential frames compared/asserted?
11. What happens for unchanged/equivalent refreshes?
12. What is the exact deterministic test surface?
13. Which symbols in the shared Streamlit file may WP06 modify?
14. What dedicated WP06 test path is authorized?

Do not define styling beyond what is necessary for deterministic functional rendering.

---

# Phase 0 — Read-Only Evidence

Before defining the contract:

1. Read #231 completely.
2. Read the accepted Release 1.9 WP06 definition.
3. Read the Release 1.9 implementation manifest.
4. Read WP05 and WP07 ownership for:
   - `python/presentation/realtime_financial_visualization.py`;
   - `python/presentation/visualization_read_model.py`;
   - Python test paths.
5. Read WP09 ownership of the existing Streamlit test path.
6. Inspect current WP05 Python consumer and Streamlit entry point.
7. Inspect current Python testing conventions.
8. Inspect #232/WP07 to identify metadata/features explicitly reserved for WP07.
9. Record all relevant existing functions/symbols in the shared Streamlit file.

No mutation.

---

# Phase 1 — Select the Minimal Frame Model

Prefer a **pure immutable/deterministic presentation projection** built from one accepted WP05 consumer snapshot.

The frame must not:

- accumulate its own history beyond the envelope's bounded window;
- query data;
- recompute features;
- depend on wall-clock time;
- depend on Streamlit session timing;
- assign a new revision;
- reinterpret backend state.

The frame should be fully derivable from:

- current accepted envelope, if any;
- current transport warning, if any.

If repository evidence makes this model incompatible with #231, stop and explain why.

---

# Phase 2 — Define `VisualizationFrame`

Define the exact conceptual fields.

Unless #231/repository evidence requires a narrower naming convention, the normative semantic content should be:

## Identity

- contract/envelope revision kind;
- revision value;
- deterministic revision identity/tie-breaker;
- source mode;
- source authority;
- target.

The frame does not create a new identity.

## Backend state

- exact WP04 presentation state.

## Price/time series

An ordered sequence projected **1:1** from the accepted envelope observation window.

Each point contains only:

- source timestamp;
- decimal price.

Rules:

- oldest → newest;
- maximum 64;
- no interpolation;
- no resampling;
- no synthetic point;
- no inferred missing point;
- no sorting beyond preserving the already-governed envelope order;
- no price transformation.

## Latest observation

Either:

- exact latest observation timestamp + price from the envelope; or
- absent when the envelope has no latest observation.

It must agree with the final price/time point whenever the series is non-empty.

A disagreement is a consumer/frame integrity error, not silently repaired.

## Count/window

Expose separately:

- `observationCount`: the canonical envelope observation count;
- `windowCount`: number of points in the bounded presentation series;
- `windowCapacity`: 64.

Do not redefine total count as window length.

Normative invariant:

`0 <= windowCount <= min(observationCount, 64)`

For a non-empty bounded current window under normal accepted envelope semantics:

`windowCount = min(observationCount, 64)`

If the actual predecessor envelope semantics require an exception, document it explicitly from repository evidence rather than inventing one.

## Feature metadata allowed in WP06

WP06 may display existing canonical feature status only to the minimum extent needed to support #231's "supported metadata":

- feature identity;
- availability state;
- latest feature value when already supplied;
- required observation count when WarmUp.

No recomputation.

## Pipeline/quality metadata allowed in WP06

Only existing high-level, already-present envelope metadata needed to identify current presentation health, such as:

- pipeline/status summary;
- validation/quality status.

Do not expose new internal evidence structures.

---

# Phase 3 — Define WP06 vs WP07 Metadata Boundary

Read #232 and define a precise boundary.

WP06 owns only **current factual frame metadata** necessary to describe the currently rendered read model.

WP07-reserved material must not be implemented or semantically specified by WP06 beyond identifying it as reserved.

At minimum, reserve for WP07 if consistent with #232:

- additional dashboard organization;
- richer explanatory labels;
- strategy/decision metadata;
- derived presentation summaries;
- user controls beyond existing WP05 refresh;
- styling/theme/layout enhancements;
- annotations not directly present in the envelope;
- additional charts/panels not required by #231.

Use the actual #232 definition to make this list exact.

If #231 and #232 overlap materially and ownership cannot be separated, stop and report the conflict.

---

# Phase 4 — Define Frame Production Semantics

A WP06 frame is produced from each **accepted consumer state evaluation**, but frame identity remains the underlying envelope revision.

Define:

## Newer accepted envelope

Produce a new frame from that envelope.

## Equivalent envelope

The deterministic frame is equivalent to the current frame.

No synthetic sequence number.

No artificial animation step.

A rerender may occur due to Streamlit, but functional frame content must be identical.

## Older envelope

WP05 ignores it; WP06 receives/retains the current accepted frame.

No new older frame.

## Revision conflict

Retain the last-good accepted envelope/frame and surface the existing consumer integrity warning.

Do not fabricate a conflicting data frame.

## Missing producer / transport failure

If no accepted envelope exists:

- frame has no backend payload;
- transport status is ProducerUnavailable/read warning as governed by WP05.

If last-good exists:

- frame continues to project last-good backend payload;
- transport warning is separately present.

No wall-clock staleness is invented.

---

# Phase 5 — Define Sequential Evolution Contract

WP06 acceptance requires deterministic sequential frames.

Define a test sequence using accepted envelope revisions.

For a sequence `E1, E2, ... En` accepted by WP05:

`Fi = Project(Ei, transport_status_i)`

Required properties:

- frame revision equals envelope revision;
- price/time series equals envelope window exactly;
- latest equals envelope latest exactly;
- observationCount equals envelope count exactly;
- windowCount equals series length;
- windowCapacity = 64;
- allowed metadata equals envelope metadata exactly;
- no value is derived except structural counts such as `windowCount`;
- newer frames replace earlier current frames;
- equivalent refresh produces an equivalent frame;
- older/conflicting input does not replace last-good frame.

Do not create frame numbers 1,2,3 independent of source revision.

---

# Phase 6 — Define Deterministic Price/Time Assertions

Future WP06 tests must assert functional data, not pixels.

For each frame assert:

- exact ordered timestamp sequence;
- exact ordered decimal price sequence;
- exact point count;
- latest timestamp;
- latest price;
- canonical observation count;
- bounded window count;
- revision;
- state;
- source mode/authority/target;
- allowed metadata.

For evolving frames assert exact before/after sequence.

Examples of required semantic scenarios:

1. one observation → one point;
2. second accepted observation → two ordered points;
3. sequential append → latest moves to final point;
4. >64 total observations → frame contains only canonical 64-point envelope window;
5. equivalent revision → identical functional frame;
6. older revision → current frame unchanged;
7. revision conflict → current frame unchanged + integrity warning;
8. Historical and Replay frames retain their distinct revision kinds;
9. Ready/WarmUp/Empty/Failed/Stale remain truthful;
10. ProducerUnavailable remains transport-local.

Do not require screenshot/pixel assertions for WP06.

---

# Phase 7 — Define Rendering Contract

WP06 may define only the minimum deterministic rendering surface required by #231.

Prefer:

## Price/time

One Streamlit-native evolving line chart or equivalent existing supported primitive fed directly by the frame's ordered price/time points.

The contract is about the **data supplied to the rendering primitive**, not pixel geometry.

## Latest

A deterministic latest-observation display containing:

- timestamp;
- price.

## Count/window

Display:

- canonical observation count;
- current bounded window count;
- capacity 64 where useful/required by #231.

## Supported metadata

Display only the WP06-owned metadata defined above.

Do not specify colors, theme, decorative layout, advanced chart controls, or WP07-owned panels.

If the current Streamlit file already uses a specific primitive and it satisfies #231, prefer preserving it.

---

# Phase 8 — Backend State Rendering

Define frame behavior for each WP04 state.

## Ready

- render price/time series if present;
- latest/count/window;
- available canonical feature metadata;
- allowed pipeline/quality metadata.

## WarmUp

- render existing observations;
- latest/count/window;
- feature identity/state;
- required observation count 2;
- no fabricated feature value.

## Empty

- empty price/time series;
- no latest;
- canonical count as supplied;
- backend state Empty.

## Failed

- retain only payload exactly as supplied by the accepted envelope;
- show safe canonical failure category/message/recoverability;
- do not synthesize data.

## Stale

- render retained backend payload exactly as supplied;
- backend state remains Stale;
- no wall-clock threshold.

Transport warnings remain separate in all cases.

---

# Phase 9 — Integrity Rules

Define deterministic frame validation.

At minimum:

- series length <= 64;
- series order matches accepted envelope order;
- latest equals final series point when series non-empty;
- no latest when series empty unless predecessor contract explicitly allows it;
- `windowCount == len(series)`;
- `windowCapacity == 64`;
- source authority is accepted predecessor value;
- revision representation is valid;
- frame state equals envelope state.

On violation:

- surface a safe consumer/frame integrity error;
- preserve last-good valid frame if available;
- do not repair or reinterpret malformed data.

Do not create new backend Failed state.

---

# Phase 10 — Shared Streamlit Path Amendment

The shared production path remains:

`python/presentation/realtime_financial_visualization.py`

Define a **symbol/concern-level WP06 shared-path exception**.

WP06 may modify only the minimum symbols needed to:

- project accepted WP05 consumer state into the deterministic WP06 frame;
- render the price/time series;
- render latest observation;
- render count/window;
- render WP06-owned supported metadata;
- render existing backend/transport state truthfully.

WP06 may not use this shared path to implement:

- WP07-reserved metadata/panels;
- new controls except existing WP05 refresh;
- new transport;
- persistence/provider access;
- feature recomputation;
- theme/styling expansion.

If cleaner repository architecture requires a new production helper file, it must already be manifest-authorized or this definition must explicitly decide whether one narrow WP06-exclusive helper path is necessary. Prefer avoiding a new production file if a pure helper in the shared file is sufficient and testable.

---

# Phase 11 — Dedicated WP06 Test Path Amendment

Authorize exactly one dedicated WP06 Python test path, using the repository's actual Python test convention.

Determine the exact path from repository evidence.

It should be semantically equivalent to a WP06-specific file such as:

`python/tests/presentation/test_realtime_financial_visualization_wp06.py`

but **do not use this example if it conflicts with the repository's real test layout**.

The chosen path must be:

- exclusive to WP06;
- dedicated to deterministic visualization-frame/render-input behavior;
- not WP05-owned;
- not WP09-owned;
- not a broad directory grant.

Tests may import pure functions/symbols from the shared Streamlit file or the already-authorized WP05 read-model consumer as appropriate.

No screenshot/pixel harness is required.

---

# Phase 12 — Optional Existing Consumer Path

Determine whether WP06 needs any change to:

`python/presentation/visualization_read_model.py`

Default: **no**.

WP06 should consume the already-accepted WP05 consumer output.

Only authorize a narrow shared exception here if repository evidence proves that deterministic frame projection cannot be implemented/tested without a presentation-neutral accessor that belongs naturally in this file.

Do not change WP05 revision/cache/retry semantics.

If no change is required, explicitly keep this path outside WP06 mutation authority.

---

# Phase 13 — Exact Manifest Amendment

Produce an exact amended WP06 path allowlist.

For every path include:

- exact path;
- exclusive/shared;
- authorized symbols/concerns;
- forbidden adjacent concerns.

The amendment should normally contain:

1. shared `realtime_financial_visualization.py` symbol-level exception;
2. one dedicated WP06 test file;
3. only if strictly necessary, one additional narrow shared/exclusive helper path proven by repository architecture.

No wildcard directory ownership.

---

# Phase 14 — Explicit Forbidden Paths

WP06 remains forbidden from changing:

- Worker production code;
- .NET Application/Domain/Infrastructure production contracts unless already explicitly WP06-owned by the accepted manifest;
- schema/migrations;
- persistence repositories;
- providers;
- canonical feature computer/formula;
- WP04 envelope contract;
- WP05 atomic transport;
- WP05 retry/cache semantics;
- Python package requirements;
- Streamlit pin;
- JSON-over-stdio;
- WP07-exclusive files/symbols;
- WP09 test paths;
- WP05-exclusive test paths.

Refine this list using the actual manifest.

---

# Phase 15 — Required Future Tests

The later implementation authority must prove at least:

## Frame projection

- exact one-point frame;
- exact two-point evolution;
- sequential multi-frame evolution;
- exact timestamp/price ordering;
- latest equals final point;
- canonical observation count;
- bounded window count/capacity;
- >64 behavior using canonical envelope window;
- source mode/authority/target;
- revision identity;
- supported metadata.

## Revision behavior

- newer accepted;
- equivalent deterministic;
- older ignored;
- conflict retains last-good + warning;
- Historical/Replay revision kinds distinct.

## States

- Ready;
- WarmUp;
- Empty;
- Failed;
- Stale;
- ProducerUnavailable with no payload;
- ProducerUnavailable/transport warning with last-good payload.

## Integrity

- latest/series mismatch rejected;
- oversize frame rejected if such malformed input can reach projector;
- invalid revision/state rejected according to existing consumer conventions.

## Boundary

Static proof of:

- no SQLite;
- no provider;
- no feature recomputation;
- no Worker control;
- no WP07 functionality;
- no WP09 test reuse.

## Regression

- existing WP05 Python consumer tests;
- Python compile/import;
- full governed .NET regression baseline starting at 305/305;
- build 0 errors / 0 warnings.

---

# Phase 16 — Future Implementation Stop Rule

The later Terra WP06 implementation must stop if:

- it needs any path not in this amended allowlist;
- it needs metadata not defined as WP06-owned;
- it needs a new chart/frame protocol;
- it needs screenshot/pixel infrastructure;
- it needs WP07 symbols;
- it needs WP09 test ownership;
- it needs envelope/transport redesign;
- it needs new dependencies.

No improvisation.

---

# Non-Goals

This definition does not authorize:

- implementation;
- test implementation;
- GitHub mutation;
- closing #231;
- WP07;
- visual styling/theme work;
- screenshot acceptance;
- animation timing;
- wall-clock frame timing;
- chart interaction controls;
- new backend fields;
- feature recomputation;
- new persistence;
- new transport;
- schema changes;
- package changes.

---

# Mutation Policy

This is definition-only.

If the accepted Release 1.9 governance explicitly permits a dedicated WP06 definition/amendment artifact, create only that documentation artifact in the authorized roadmap location.

Otherwise make zero repository mutations and return the normative contract in chat.

No production/test/GitHub mutations under any circumstance.

---

# Required Completion Report

## Repository evidence

- #231 requirements;
- #232 boundary;
- current shared Streamlit symbols;
- Python test convention;
- current manifest ownership.

## Selected visualization-frame contract

State exact:

- frame identity;
- price/time representation;
- latest representation;
- count/window semantics;
- supported metadata;
- backend states;
- transport warnings;
- sequential evolution;
- integrity behavior.

## WP06 / WP07 boundary

List exactly what WP06 owns and what remains reserved.

## Rendering contract

State the minimum deterministic rendering primitives and assertion surface.

## Manifest amendment

List exact:

- shared production path(s);
- symbol/concern exception(s);
- dedicated WP06 test path;
- any optional helper path if truly required;
- forbidden paths.

## Future test matrix

List exact acceptance coverage.

## Mutation proof

If documentation artifact is authorized:

`WP06 VISUALIZATION-FRAME/MANIFEST AMENDMENT MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

If no artifact is authorized:

`WP06 VISUALIZATION-FRAME/MANIFEST AMENDMENT MUTATIONS: ZERO`

## Next step

On success state exactly:

`WP06 VISUALIZATION-FRAME CONTRACT AND PATH AUTHORITY DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

---

# Terminal Markers

On success:

`RELEASE 1.9 WP06 VISUALIZATION-FRAME CONTRACT AND MANIFEST/PATH-AUTHORITY AMENDMENT COMPLETE`

On blocker:

`RELEASE 1.9 WP06 VISUALIZATION-FRAME CONTRACT AND MANIFEST/PATH-AUTHORITY AMENDMENT BLOCKED`

Emit success only when the frame contract, WP06/WP07 boundary, deterministic assertion surface, shared-path exception, and dedicated WP06 test path are all unambiguous.
