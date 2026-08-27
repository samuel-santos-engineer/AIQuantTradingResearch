# Release 1.9 — WP07 Predecessor Idempotency + Data-Quality Definition — Codex Authority

## Authority

This document grants a **narrow, definition-only predecessor authority** to establish the two upstream canonical facts required by Release 1.9 WP07, canonical issue **#232**:

1. a governed **idempotency presentation fact**;
2. a governed **validation/data-quality presentation fact**.

Use **GPT-5.6 Luna**.

WP07 is currently blocked because the accepted WP04/WP05/WP06 presentation boundary does not expose either fact in a form that can be displayed deterministically without inventing semantics.

This authority exists only to define those facts upstream of presentation and define the minimum additive exposure needed to carry them through the existing presentation envelope/transport.

It does **not** authorize implementation.

It does **not** authorize WP07 UI work.

It does **not** authorize WP08 or WP09.

It does **not** authorize GitHub lifecycle mutation.

---

# Entry State

Expected:

- WP01–WP06 complete.
- #232 remains Open / Backlog.
- #233 remains Open / Backlog.
- Full .NET predecessor baseline: 305/305.
- WP06 Python: 6/6.
- WP05 Python: 3/3.
- Build: 0 errors / 0 warnings.
- Schema: v4.
- Existing WP04 envelope/transport contract remains accepted.
- Prior WP07 definition attempts made zero implementation mutations.

No reconciliation is required unless fresh read-only inspection proves otherwise.

---

# Fixed Predecessor Boundaries

Do not redefine:

## WP02

- Replay identity;
- logical ticks;
- duplicate determinism;
- restart/resume;
- finite completion.

## WP03

- canonical five-stage pipeline;
- Historical/Replay modes;
- schema v4;
- source authority 0/1;
- persistence/provenance semantics.

## WP04

- immutable versioned presentation read model;
- Ready / Empty / WarmUp / Stale / Failed;
- bounded 64-row window;
- HistoricalPresentationRevision;
- ReplayLogicalTick;
- feature projection;
- safe failure semantics.

## WP05

- atomic JSON transport;
- read-only Python consumer;
- one last-good envelope;
- bounded retry/cache;
- transport warnings separate from backend state.

## WP06

- deterministic `VisualizationFrame`;
- exact price/time/latest/count/window;
- inherited revision;
- factual metadata only;
- no reconstruction.

This authority may only add canonical facts and the minimum additive exposure needed for WP07.

---

# Objective

Define, without implementation ambiguity:

## A. Canonical idempotency fact

The contract must establish:

- exact semantic meaning;
- exact scope;
- canonical source;
- allowed values/states;
- unavailable/not-applicable behavior;
- relationship to execution/snapshot identity;
- relationship to duplicate handling;
- relationship to replay determinism;
- relationship to revision/cache behavior;
- whether it is per request, execution, snapshot, persisted result, or another governed unit.

It must explicitly state what idempotency **is not**.

## B. Canonical validation/data-quality fact

The contract must establish:

- exact semantic meaning;
- canonical source;
- exact categories/fields;
- allowed values/states;
- unavailable/not-applicable behavior;
- relationship to pipeline validation;
- relationship to failure;
- whether quality is categorical only or contains any already-governed factual counts/flags.

It must not invent numeric scores, confidence, severity, or thresholds unless already governed.

## C. Minimum additive exposure

Define the smallest additive path required so those facts can later reach WP07 through:

canonical execution/evidence
→ WP04 envelope
→ WP05 JSON
→ Python consumer
→ WP06/WP07 presentation projection

without changing existing semantics.

---

# Phase 0 — Read Canonical Evidence

Before defining anything:

1. Read #232.
2. Read accepted WP02–WP06 definitions relevant to:
   - determinism;
   - duplicate behavior;
   - pipeline evidence;
   - validation;
   - quality;
   - persistence;
   - snapshot identity.
3. Search repository types/fields/tests for:
   - idempotent/idempotency;
   - duplicate;
   - deterministic;
   - replay identity;
   - source-state identity;
   - snapshot identity/version;
   - validation;
   - quality;
   - accepted/rejected;
   - invalid numeric;
   - pipeline success/failure.
4. Read the actual serialized WP04 envelope model.
5. Read `PipelineExecutionResult`, `PipelineExecutionEvidence`, structured evidence, validation result types, persistence result types, and relevant tests.
6. Identify what is already computed versus what would need new semantics.

No mutation.

---

# Phase 1 — Idempotency Candidate Analysis

Evaluate only evidence-backed candidates.

Potential candidate classes may include:

## Model A — Execution idempotency

A repeated canonical execution over the same governed inputs produces the same canonical result identity/evidence.

## Model B — Persistence idempotency

Repeated persistence/registration of the same canonical result does not create semantically distinct duplicate evidence.

## Model C — Replay duplicate determinism

Repeated replay input/tick handling produces deterministic duplicate behavior.

## Model D — Snapshot identity stability

Equivalent canonical inputs yield the same governed snapshot identity/version.

Do not select one merely because it sounds closest to “idempotency”.

For each candidate state:

- exact canonical source;
- exact scope;
- whether the repository already proves it;
- whether it is presentation-safe;
- whether it is truly idempotency versus determinism/identity/deduplication.

### Hard stop

If #232 requires an idempotency concept that is not already governed anywhere upstream, stop and report that a new semantic primitive would be required under separate authority.

Do not manufacture idempotency from revision equality or cache behavior.

---

# Phase 2 — Define Idempotency Semantics

If a canonical candidate exists, define the exact fact.

The definition must include:

## Identity

Canonical presentation field name, e.g. conceptually:

`idempotencyStatus`

Use actual repository naming conventions where possible.

## Scope

Exactly one of:

- execution;
- snapshot;
- persisted result;
- replay operation;
- another proven scope.

No ambiguity.

## Allowed values

Use the narrowest factual domain, for example only if evidence supports it:

- `Idempotent`
- `NotIdempotent`
- `Unavailable`

or a boolean + availability tag.

Do not use subjective labels.

## Source

Exact type/property/test evidence.

## Non-equivalences

Explicitly state that the fact is **not inferred from**:

- equal presentation revision;
- cache hit;
- transport retry;
- duplicate observation replacement;
- successful pipeline execution;
- replay logical tick alone.

unless canonical evidence explicitly makes one of these equivalent.

---

# Phase 3 — Validation/Data-Quality Candidate Analysis

Trace existing canonical validation and quality evidence.

Candidates may include:

- accepted/rejected validation result;
- invalid numeric evidence;
- schema/contract validation;
- pipeline validation summary;
- quality status already present in the WP04 envelope;
- row/observation acceptance facts.

For every candidate state:

- exact source;
- exact semantics;
- whether it is already computed;
- whether it is stable enough for deterministic presentation.

Do not conflate pipeline success with data quality unless the repository does.

---

# Phase 4 — Define Data-Quality Semantics

Define one narrow factual presentation contract.

The contract must specify:

## Field set

The smallest governed set necessary for WP07.

Possible shapes, only if evidence supports them:

- `validationStatus`
- `qualityStatus`
- `validationCategory`
- `qualityCategory`
- exact factual counts already governed

Prefer fewer fields.

## Allowed values

Use existing canonical categories only.

Do not invent:

- Excellent/Good/Poor;
- percentages;
- scores;
- severity bands;
- thresholds;
- confidence.

## Relationship to failure

Specify:

- whether invalid data makes pipeline Failed;
- whether quality may be available on Ready/WarmUp/Empty;
- whether failure carries a canonical validation category;
- what is unavailable.

## Source

Exact upstream type/property.

---

# Phase 5 — State Availability Matrix

Create a required matrix for both facts across:

- Ready;
- WarmUp;
- Empty;
- Failed;
- Stale.

For each state define:

- idempotency available? yes/no/conditional;
- validation/data-quality available? yes/no/conditional;
- exact value source;
- whether value is retained from prior payload on Stale;
- whether Failed exposes the fact or unavailable.

Transport warnings are not backend states and do not alter these facts.

---

# Phase 6 — Additive Exposure Location

Determine the minimum upstream place to expose the canonical facts.

Evaluate:

## Option A — existing `PipelineExecutionEvidence`

Preferred if the facts are execution/evidence semantics.

## Option B — `PipelineExecutionResult`

Use if they are runtime result facts needed downstream.

## Option C — existing WP04 envelope metadata

Use only if canonical upstream facts already exist and the envelope currently omits them.

Do not put presentation-only labels in core contracts.

The upstream fact should be domain/application-neutral.

---

# Phase 7 — WP04 Envelope Exposure

If canonical facts are established, define exact additive WP04 envelope fields.

Requirements:

- additive only;
- immutable;
- backward-compatible;
- no change to revision semantics;
- no change to Ready/Empty/WarmUp/Stale/Failed;
- no new persistence requirement;
- no schema change.

Define exact field names/types at the semantic level.

Prefer optional/tagged facts if some states legitimately lack them.

Do not introduce arbitrary metadata dictionaries.

---

# Phase 8 — WP05 JSON / Python Exposure

Define the minimum transport/parser exposure.

Rules:

- same `aiq-visualization-read-model-v1` semantic contract if additive optional fields are allowed under the accepted compatibility model;
- otherwise explicitly determine whether contract version must change.

### Hard stop

If adding these fields requires a new contract version and the accepted versioning policy does not define how to evolve it, stop and request a separate envelope-version evolution authority.

Do not silently mutate v1 semantics if versioning rules forbid it.

Python exposure must:

- preserve exact canonical values;
- not reinterpret;
- not derive.

---

# Phase 9 — WP06 Compatibility

Define whether WP06 `VisualizationFrame` remains unchanged or receives narrowly additive optional factual fields.

Preferred:

- existing WP06 fields and tests remain unchanged;
- WP07 may consume additive metadata without changing price/time/latest/count semantics.

If the cleanest path is to add optional factual fields to `VisualizationFrame`, define:

- exact fields;
- immutable/additive behavior;
- no effect on WP06 rendering;
- WP06 6/6 compatibility.

Do not create a second frame.

---

# Phase 10 — WP07 Presentation Mapping

Define only the source mapping, not UI implementation.

For future WP07:

## Idempotency section

Map exact upstream fact → exact factual display value.

## Data Quality section

Map exact upstream fact(s) → exact factual display value(s).

No derived interpretation.

No new score.

No recommendation.

---

# Phase 11 — Required Future Tests

A later implementation authority must prove:

## Idempotency

- exact canonical scope;
- repeated equivalent operation behavior;
- exact positive/negative/unavailable cases where governed;
- no inference from revision/cache/duplicate handling unless explicitly canonical.

## Data Quality

- every allowed canonical value;
- state availability;
- failure relationship;
- no invented metrics.

## Exposure

- upstream evidence/result;
- WP04 envelope;
- JSON serialization;
- Python parsing;
- WP06 compatibility;
- WP07 consumption.

## Regression

- WP02 determinism tests;
- WP03 schema/persistence;
- WP04 envelope;
- WP05 transport/parser;
- WP06 6/6;
- full .NET 305/305 baseline or explained increase;
- Python tests.

---

# Phase 12 — Implementation Authority Surface

Define exact categories of files a later implementation may need.

Do not grant broad paths here unless this definition authority is explicitly allowed to amend path ownership.

At minimum identify likely layers:

- Application evidence/result types;
- WP04 envelope mapping;
- WP05 serializer;
- Python parser;
- optional WP06 frame additive metadata;
- dedicated tests.

For each state whether a future path amendment will be required.

Do not authorize implementation implicitly.

---

# Non-Goals

This authority does not define or authorize:

- trading recommendations;
- quality scores;
- confidence;
- new persistence tables;
- schema v5;
- new replay semantics;
- new duplicate semantics;
- new cache semantics;
- new transport;
- UI layout/styling;
- WP07 implementation;
- WP08/WP09;
- GitHub mutation.

---

# Stop Conditions

Stop if:

- no canonical idempotency fact exists upstream;
- data-quality semantics are not already governed sufficiently;
- adding fields requires envelope version evolution without existing policy;
- the two facts require schema/persistence redesign;
- presentation would need derivation rather than direct factual projection;
- multiple materially different semantic models remain equally valid.

On stop:

- make zero production/test/GitHub changes;
- report the exact missing predecessor semantic primitive;
- identify the minimum next authority.

---

# Success Criteria

This definition succeeds only when:

- idempotency has an exact canonical meaning/source/scope/value domain;
- data-quality/validation has exact canonical meaning/source/value domain;
- state availability is fixed;
- minimum additive upstream exposure is fixed;
- WP04/WP05/WP06 compatibility is fixed;
- no presentation-side inference is required;
- required future tests are fixed;
- no implementation occurs.

---

# Mutation Policy

Definition-only.

If governance explicitly permits one dedicated predecessor-definition artifact, create only:

`docs/roadmap/release-1.9/RELEASE_1.9_WP07_PREDECESSOR_IDEMPOTENCY_DATA_QUALITY_DEFINITION.md`

Otherwise make zero repository mutations.

No production/test/GitHub mutation under any circumstance.

---

# Required Completion Report

Return:

## Canonical idempotency fact

- exact meaning;
- scope;
- source;
- allowed values;
- unavailable behavior;
- explicit non-equivalences.

## Canonical data-quality fact

- exact meaning;
- source;
- fields;
- allowed values;
- state/failure behavior.

## State availability matrix

Ready/WarmUp/Empty/Failed/Stale.

## Additive exposure

- upstream result/evidence location;
- WP04 envelope fields;
- JSON/versioning implication;
- Python parser;
- WP06 compatibility.

## Future implementation surface

List exact layers and whether a path amendment is still required.

## Required tests

List exact scenarios.

## Mutation proof

If one documentation artifact is authorized:

`WP07 PREDECESSOR IDEMPOTENCY/DATA-QUALITY DEFINITION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

Otherwise:

`WP07 PREDECESSOR IDEMPOTENCY/DATA-QUALITY DEFINITION MUTATIONS: ZERO`

## Next step

On success state exactly:

`WP07 PREDECESSOR IDEMPOTENCY/DATA-QUALITY CONTRACT DEFINED — WP07 PRESENTATION DEFINITION MAY RESUME`

Do not implement WP07 here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP07 PREDECESSOR IDEMPOTENCY/DATA-QUALITY DEFINITION COMPLETE`

On blocker:

`RELEASE 1.9 WP07 PREDECESSOR IDEMPOTENCY/DATA-QUALITY DEFINITION BLOCKED`

Do not emit success unless both upstream facts are canonically defined without invention and the additive exposure path is unambiguous.
