# Release 1.9 — WP07 Canonical Idempotency + Data-Quality Semantic-Definition Authority

## Authority

This is a **narrow semantic-definition authority** for Release 1.9 WP07, canonical issue **#232**.

Use **GPT-5.6 Luna**.

Prior read-only authorities proved that no existing presentation-level contract canonically defines the “idempotency” or “validation/data-quality” facts required by #232. This authority is therefore explicitly empowered to **select and introduce the minimum new canonical semantic primitives** needed to satisfy those two requirements.

This is definition-only.

It may define new semantic contracts and their ownership/exposure boundaries, but it may not implement them.

No production code.
No test code.
No schema/persistence mutation.
No package change.
No GitHub lifecycle mutation.
No WP07 UI implementation.
No WP08/WP09.

---

# Proven Entry Evidence

Treat these findings as the starting evidence, but verify them read-only:

- `ObservationPersistenceOutcome.Idempotent` exists for observation persistence.
- pipeline persistence exposes `EquivalentExisting`.
- neither is currently governed as the WP07 presentation-level idempotency fact.
- WP04 exposes pipeline success/failure and snapshot identity/version.
- WP04 exposes no canonical validation/data-quality field/category suitable for deterministic WP07 display.
- previous definition attempts made zero implementation/repository/GitHub mutations.
- #232 remains Open / Backlog.
- #233 remains Open / Backlog.
- predecessor full .NET baseline is 305/305.
- WP06 Python baseline is 6/6.
- WP05 Python baseline is 3/3.

---

# Objective

Normatively define exactly two new Release 1.9 semantic primitives:

1. **Canonical Presentation Idempotency**
2. **Canonical Presentation Data Quality**

The primitives must be:

- factual;
- deterministic;
- minimal;
- upstream-owned rather than UI-invented;
- directly projectable into WP07;
- compatible with WP02–WP06;
- independent of transport/cache/rendering behavior.

Also define the minimum additive exposure chain needed later:

canonical pipeline/persistence evidence
→ application execution result/evidence
→ WP04 envelope
→ WP05 JSON/parser
→ WP06 additive factual metadata
→ WP07 presentation.

Do not implement that chain here.

---

# Phase 0 — Read-Only Semantic Evidence

Before selecting semantics, inspect:

1. #232 and accepted Release 1.9 WP07 wording.
2. `ObservationPersistenceOutcome.Idempotent`.
3. pipeline persistence `EquivalentExisting` and all sibling outcomes.
4. snapshot/catalog persistence semantics.
5. canonical five-stage pipeline evidence/result contracts.
6. validation/acceptance/rejection/failure contracts.
7. invalid-numeric behavior.
8. WP04 state/failure contract.
9. WP05 envelope serialization/parser.
10. WP06 `VisualizationFrame`.
11. tests proving persistence equivalence and validation behavior.

Build an evidence matrix before selecting either primitive.

---

# Part A — Canonical Presentation Idempotency

## A1 — Required semantic question

Define what #232 means when it presents **idempotency state**.

The chosen scope must correspond to one meaningful canonical operation and must not combine unrelated notions.

Evaluate at minimum:

### Candidate A — Pipeline-result persistence equivalence

The canonical pipeline attempts to persist/register its resulting evidence. The presentation fact reports whether that operation:

- created/accepted a new canonical persisted result; or
- resolved to an already-existing semantically equivalent canonical result.

This candidate may map directly from pipeline persistence outcomes such as `EquivalentExisting`.

### Candidate B — Observation persistence idempotency

The fact reflects `ObservationPersistenceOutcome.Idempotent`.

### Candidate C — Composite persistence idempotency

The fact combines observation and pipeline-result persistence.

### Candidate D — New execution-level semantic

A new primitive summarizes whether re-executing equivalent canonical inputs has externally equivalent canonical persistence effects.

Do not choose by convenience. Choose the narrowest model that truthfully satisfies #232 and can be supported deterministically by the canonical production path.

---

# A2 — Preferred semantic constraint

Unless repository evidence contradicts it, prefer **pipeline-result persistence equivalence** as the presentation scope because it is closest to the completed pipeline result that WP04 presents.

If selected, define a canonical primitive conceptually equivalent to:

`PresentationIdempotencyStatus`

with an exact scope such as:

> The disposition of the canonical pipeline result persistence operation for the current accepted pipeline execution.

Do not use this wording blindly; align it with actual repository types.

---

# A3 — Required value domain

Define the smallest exact value domain.

A preferred shape, only if supported after inspection:

- `NewlyPersisted`
- `EquivalentExisting`
- `Unavailable`

If the existing persistence outcomes require another truthful value, include it only when needed.

Do **not** use a vague boolean if it would erase the distinction between “new” and “equivalent existing”.

Do not label `NewlyPersisted` as “not idempotent”. A first successful write is not evidence of non-idempotent behavior.

---

# A4 — Explicit non-equivalences

The canonical idempotency fact must explicitly **not** mean:

- observation duplicate replacement;
- `ObservationPersistenceOutcome.Idempotent`, unless that candidate is explicitly selected;
- Replay logical-tick equality;
- Historical presentation revision equality;
- WP05 cache hit;
- unchanged JSON file;
- transport retry;
- Streamlit refresh equivalence;
- pipeline success alone;
- snapshot identity equality alone.

Document these separations normatively.

---

# A5 — Scope and lifetime

Define exactly whether the fact belongs to:

- the current canonical pipeline execution;
- its resulting persistence disposition;
- the current envelope only.

It must not claim cross-session historical idempotency beyond evidence actually available.

---

# Part B — Canonical Presentation Data Quality

## B1 — Required semantic question

Define what #232 means by **validation/data-quality state**.

This authority may introduce a new categorical projection, but it must be grounded in canonical validation evidence rather than subjective assessment.

The primitive must answer a narrow factual question, such as:

> Did the canonical data accepted for this pipeline result satisfy the governed validation rules required for presentation, fail those rules, or provide no applicable validation result?

Select exact wording from repository evidence.

---

# B2 — Source semantics

Trace the actual canonical validation points.

Potential evidence may include:

- accepted observation validation;
- dataset/snapshot validation;
- invalid numeric evidence;
- pipeline validation result;
- structured-result validation;
- canonical failure category.

Select one coherent validation scope.

Do not aggregate unrelated validators into an invented “quality score”.

---

# B3 — Preferred categorical model

Unless repository evidence requires more granularity, prefer a minimal primitive conceptually equivalent to:

`PresentationDataQualityStatus`

with:

- `Valid`
- `Invalid`
- `Unavailable`

or repository-aligned names with equivalent factual semantics.

`Valid` must mean only that the selected governed validation contract passed.

It must not mean:

- profitable;
- complete market coverage;
- fresh;
- high confidence;
- accurate provider data;
- good trading quality.

`Invalid` must correspond to a canonical validation rejection/failure, not generic pipeline failure.

`Unavailable` must cover states where no applicable validation fact exists.

---

# B4 — No invented scoring

Explicitly forbid:

- quality percentages;
- confidence;
- severity levels;
- thresholds;
- Good/Fair/Poor;
- inferred completeness;
- inferred freshness;
- UI-generated warnings presented as data quality.

---

# Part C — Backend-State Matrix

Define both primitives for:

## Ready
Specify exact idempotency and data-quality availability.

## WarmUp
Feature WarmUp must not automatically become Invalid. Define whether canonical validation remains Valid and whether idempotency exists.

## Empty
Define both facts explicitly. Prefer `Unavailable` unless canonical operations actually produce the fact.

## Failed
Distinguish:
- validation-caused failure;
- unrelated pipeline failure;
- persistence failure.

Define when DataQuality is `Invalid` versus `Unavailable`.
Define when Idempotency is `Unavailable`.

## Stale
Retain the last complete payload's facts if that is consistent with WP04 retention semantics. Stale itself does not alter either primitive.

Transport warnings never alter either fact.

---

# Part D — Ownership

Define the semantic owner upstream.

Preferred architecture:

- canonical persistence/validation operations remain the source facts;
- Application exposes immutable semantic projections;
- WP04 transports those facts;
- Python/WP06/WP07 only preserve/display them.

Presentation must not decide whether an operation was idempotent or data was valid.

Name the exact proposed immutable types/properties using repository naming conventions.

---

# Part E — Minimum Additive Exposure Contract

Define the future additive chain.

## E1 — Application

Specify exact additive fields/types for `PipelineExecutionResult` and/or `PipelineExecutionEvidence`.

They must be immutable and optional/tagged where the fact is unavailable.

Do not alter existing fields.

## E2 — WP04 envelope

Specify exact additive fields.

Requirements:

- no state/revision change;
- no arbitrary dictionary;
- no schema persistence change;
- no feature contract change.

## E3 — JSON contract/version

Determine whether adding these fields is backward-compatible under `aiq-visualization-read-model-v1`.

If accepted versioning rules permit additive fields, state that explicitly.

If they do not, stop and require a narrow envelope-version authority.

Do not silently assume.

## E4 — WP05 parser

Specify direct parsing only.

No derivation.

No cache/retry changes.

## E5 — WP06 frame

Authorize conceptually only a narrowly additive immutable metadata projection if needed by WP07.

Existing WP06 semantics remain identical.

No second frame.

---

# Part F — WP07 Display Semantics

This authority defines only canonical values that a later presentation authority may label.

Define deterministic display mapping for the primitive values, preferably 1:1:

Idempotency:
- `NewlyPersisted` → exact factual label/value
- `EquivalentExisting` → exact factual label/value
- `Unavailable` → exact unavailable value

Data Quality:
- `Valid`
- `Invalid`
- `Unavailable`

Do not define layout/styling beyond stable terminology.

---

# Part G — Required Future Tests

A later implementation authority must prove:

## Idempotency

- new pipeline result maps to `NewlyPersisted`;
- equivalent existing pipeline result maps to `EquivalentExisting`;
- unavailable path maps to `Unavailable`;
- first persistence is never mislabeled “non-idempotent”;
- observation idempotency does not overwrite the selected scope;
- revision/cache/transport do not affect the fact.

## Data Quality

- canonical accepted validation maps to `Valid`;
- canonical validation rejection maps to `Invalid`;
- unrelated pipeline failure does not become `Invalid` unless validation itself failed;
- unavailable states map to `Unavailable`;
- WarmUp is not Invalid merely because the feature is unavailable;
- Stale retains last-good quality fact;
- transport warning does not alter quality.

## Exposure

- source operation/evidence;
- Application result/evidence;
- WP04 envelope;
- JSON;
- Python parser;
- WP06 additive metadata;
- WP07 direct mapping.

## Compatibility

- WP02 replay/determinism unchanged;
- WP03 persistence/schema unchanged;
- WP04 state/revision unchanged;
- WP05 transport/cache unchanged;
- WP06 chart/frame semantics unchanged;
- schema remains v4;
- full predecessor regressions remain green.

---

# Part H — Required Future Implementation Scope

Define the exact classes/symbols and likely paths that a later implementation authority must inspect/change.

Do not grant broad path ownership.

Classify each as:

- semantic source wiring;
- Application projection;
- WP04 envelope exposure;
- Worker JSON serialization;
- Python parsing;
- WP06 additive projection;
- focused tests.

State whether a separate manifest/path amendment is required before implementation.

---

# Part I — Non-Goals

Do not define:

- observation-level idempotency as WP07 idempotency unless explicitly selected;
- composite “system idempotency” without a single governed scope;
- quality score;
- confidence;
- freshness;
- profitability;
- trading recommendation;
- provider accuracy;
- new database fields;
- schema v5;
- new persistence behavior;
- new Replay semantics;
- new transport;
- new WP04 state;
- new revision kind;
- WP07 UI implementation;
- WP08/WP09.

---

# Part J — Stop Conditions

Stop if:

- no candidate idempotency scope can be made precise without redesigning persistence;
- no coherent validation scope can be selected without new pipeline validation behavior;
- the primitives require schema changes;
- the primitives require changing persistence outcomes rather than projecting them;
- additive envelope fields require an undefined version evolution;
- multiple materially different semantic models remain equally valid after repository evidence.

If blocked, report the exact new architectural decision required.

---

# Mutation Policy

Definition-only.

If Release 1.9 governance permits one documentation artifact, create only:

`docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_IDEMPOTENCY_DATA_QUALITY_SEMANTIC_DEFINITION.md`

No other repository file may change.

No GitHub mutation.

If that artifact path is not governed/allowed, return the normative definition in chat with zero repository mutations.

---

# Required Completion Report

## Selected idempotency primitive
State:
- name;
- scope;
- canonical source;
- values;
- exact semantics;
- non-equivalences;
- state behavior.

## Selected data-quality primitive
State:
- name;
- validation scope;
- canonical source;
- values;
- exact semantics;
- failure/state behavior;
- explicit exclusions.

## Backend-state matrix
Ready / WarmUp / Empty / Failed / Stale.

## Additive exposure contract
Application → WP04 → JSON → WP05 parser → WP06 → WP07.

## Versioning conclusion
State whether v1 supports the additive fields or a separate authority is required.

## Future implementation surface
Exact classes/symbols/paths as far as repository evidence permits.

## Required test matrix
List all mandatory tests.

## Mutation proof
If one documentation artifact is created:

`WP07 CANONICAL IDEMPOTENCY/DATA-QUALITY SEMANTIC DEFINITION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

Otherwise:

`WP07 CANONICAL IDEMPOTENCY/DATA-QUALITY SEMANTIC DEFINITION MUTATIONS: ZERO`

## Next step
On success:

`WP07 CANONICAL IDEMPOTENCY/DATA-QUALITY SEMANTICS DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

Do not implement.

---

# Terminal Markers

Success:

`RELEASE 1.9 WP07 CANONICAL IDEMPOTENCY/DATA-QUALITY SEMANTIC DEFINITION COMPLETE`

Blocked:

`RELEASE 1.9 WP07 CANONICAL IDEMPOTENCY/DATA-QUALITY SEMANTIC DEFINITION BLOCKED`

Do not emit success unless both new primitives have one precise canonical scope, source, value domain, state behavior, and unambiguous additive exposure contract.
