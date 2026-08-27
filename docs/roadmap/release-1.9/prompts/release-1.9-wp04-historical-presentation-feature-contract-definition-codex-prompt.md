# Release 1.9 — WP04 Historical Presentation-Feature Contract Definition — Codex Authority

## Authority

This document grants a **narrow, definition-only authority** for the Historical presentation-feature gap discovered while preparing WP05, with WP04 canonical issue **#229** remaining Closed / Done and WP05 canonical issue **#230** remaining Open / Backlog.

Fresh read-only verification established:

- Historical materialization creates observations and snapshot data.
- `PipelineExecutionResult` currently exposes provenance/evidence only.
- The canonical five-stage pipeline does **not** currently compute or expose `simple-return-lag-1-v1`.
- The Historical Worker therefore lacks:
  - the observation projection required by the WP04 presentation model;
  - the canonical feature output required for truthful Ready / WarmUp semantics.
- UI-side feature computation, SQLite reconstruction, provider refetch, or fabricated presentation values are forbidden.

No repository, Python, configuration, runtime, Project, or GitHub mutation occurred in the blocked predecessor pass.

This authority exists only to define the missing **canonical Historical presentation-feature contract**.

It does **not** authorize implementation.

It does **not** authorize WP05 implementation.

It does **not** authorize WP06.

It does **not** authorize GitHub lifecycle mutation.

---

# Objective

Define one unambiguous architecture for producing and exposing the Historical presentation inputs needed by the fixed WP04 Model-C envelope, especially:

1. the observation projection;
2. the `simple-return-lag-1-v1` feature definition;
3. the canonical feature computation location;
4. the canonical feature output representation;
5. WarmUp semantics;
6. Ready semantics;
7. additive pipeline/result/evidence exposure;
8. compatibility with Replay;
9. compatibility with existing five-stage pipeline semantics;
10. required future implementation tests.

The definition must ensure that the feature is computed exactly once in a governed canonical path and then consumed by WP04, rather than recomputed in the presentation layer.

---

# Fixed Architectural Invariants

Do not reopen these.

## WP04 presentation model

Model C remains fixed:

- immutable versioned snapshot;
- bounded 64-row observation window;
- Ready / Empty / WarmUp / Stale / Failed;
- HistoricalPresentationRevision;
- Replay logical-tick revision;
- no UI-side business logic.

## WP04 producer/consumer boundary

WP04 producer owns truthful read-model construction.

WP05 must later be read-only.

## Historical acquisition

Historical observations remain acquired through the canonical Historical path.

Do not replace it with Replay or persistence readback.

## Replay

Replay producer semantics remain valid and must not be redesigned unless a shared canonical feature extension requires only behavior-preserving compatibility.

## Pipeline

There is one canonical five-stage pipeline.

Do not create a separate presentation feature pipeline.

---

# Core Problem

The accepted WP04 Ready/WarmUp semantics require a governed feature:

`simple-return-lag-1-v1`

But the canonical five-stage pipeline currently does not compute/expose it.

Therefore this authority must decide:

- whether the feature belongs as a canonical pipeline output;
- which existing stage owns the computation;
- whether a new sub-result inside an existing stage is sufficient;
- whether the pipeline result/evidence contract must expose it additively;
- how the Historical observation projection is exposed alongside it.

Do not choose based on presentation convenience alone.

---

# Permitted Scope

This authority may read:

- #229 and #230;
- Release 1.9 WP04/WP05 definitions;
- canonical five-stage pipeline implementation;
- stage result types;
- `PipelineExecutionResult`;
- `PipelineExecutionEvidence`;
- historical materialization outputs;
- observation domain types;
- any existing feature-definition/catalog abstractions;
- Replay feature/observation handling if any;
- tests around stage outputs and deterministic calculations;
- any prior definition of `simple-return-lag-1-v1`.

It may define one normative Historical presentation-feature contract.

If governance permits one WP04-owned definition artifact, create only that artifact.

Otherwise return the normative contract in the completion report.

---

# Explicitly Forbidden

Do not:

- modify production code;
- modify tests;
- modify schema;
- add SQLite queries for presentation;
- add provider refetch;
- compute feature in Streamlit;
- compute feature in WP04 outside the canonical pipeline if the feature is meant to be canonical;
- add a parallel feature pipeline;
- alter WP04 state semantics;
- alter Replay revision semantics;
- start WP05 implementation;
- start WP06;
- modify GitHub.

This is definition-only authority.

---

# Phase 0 — Read Canonical Pipeline Semantics

Before defining anything:

1. Read the exact five canonical stages.
2. Identify what each stage receives and emits.
3. Identify where ordered observations exist.
4. Identify where transformed/derived values are already computed.
5. Search for any existing simple-return, lag-1, return, feature, or feature-definition logic.
6. Read `PipelineExecutionResult`.
7. Read `PipelineExecutionEvidence`.
8. Read historical materialization result types.
9. Read Replay path usage of observations/features.
10. Determine whether `simple-return-lag-1-v1` is already normatively defined elsewhere.

Do not mutate anything.

---

# Phase 1 — Define `simple-return-lag-1-v1`

If an accepted definition already exists, adopt it exactly.

If not, determine whether Release 1.9 artifacts define it sufficiently.

The contract must specify:

- exact feature identity:
  `simple-return-lag-1-v1`;
- required input observations;
- minimum observation count;
- exact formula/semantic;
- output type;
- whether output applies only to latest observation or to a series;
- treatment of zero/invalid prior value;
- deterministic ordering requirement;
- any invalid/missing-data behavior.

### Hard stop

If the mathematical/semantic definition itself is not already governed and materially ambiguous, stop.

Do not invent the feature formula silently under this authority.

---

# Phase 2 — Choose Canonical Computation Location

Evaluate narrowly:

## Model A — Existing canonical stage owns the feature

Add `simple-return-lag-1-v1` computation as an additive output of the stage that already owns derived/feature computation.

Preferred if such a stage clearly exists.

## Model B — Existing stage result is extended with a feature projection

Computation remains in canonical pipeline, but output type gains a narrow feature result.

## Model C — Pipeline orchestration computes feature after canonical stage outputs

Use only if no existing stage semantically owns feature calculation and orchestration is already the canonical derived-output boundary.

## Model D — New sixth stage

Use only if accepted Release 1.9 architecture explicitly permits it.

### Preferred discipline

Preserve the accepted five-stage architecture if possible.

A new sixth stage is a material architecture change and should be avoided unless unavoidable and explicitly justified.

### Hard stop

If multiple materially different computation locations remain equally valid, stop rather than guessing.

---

# Phase 3 — Define Feature Result Contract

Define one immutable feature result shape.

At minimum specify:

- feature identity;
- state:
  - Available
  - WarmUp / NotReady
  - Failed, only if canonical feature computation can fail independently;
- latest value if available;
- required observation count;
- current observation count;
- optional source observation identity/tick linkage if already governed.

Prefer the smallest contract required by WP04.

Do not create a generalized feature framework unless one already exists.

---

# Phase 4 — WarmUp Semantics

WP04 fixed contract says lag-1 feature requires two observations.

Define exact canonical semantics:

- observation count < 2 => WarmUp / NotReady;
- required count = 2;
- no feature value is produced;
- existing observations remain truthful and may be exposed to WP04;
- this is not a pipeline failure.

Define behavior exactly at count = 2.

Do not fabricate zero/NaN feature as a substitute for WarmUp unless existing feature conventions require it.

---

# Phase 5 — Ready Semantics

Define Ready feature output when sufficient observations exist.

Specify:

- which ordered observations are used;
- whether only the latest lag-1 value is exposed;
- whether the full derived feature series is retained or discarded;
- exact link to latest observation;
- deterministic behavior for duplicates/order.

Prefer only the latest value if WP04 requires only current presentation value, unless pipeline/repository conventions already expose full series.

Do not add unnecessary feature history.

---

# Phase 6 — Historical Observation Projection

Define the minimum observation projection that must be exposed from canonical Historical execution to WP04.

Specify:

- whether the full accepted materialized observation set is exposed;
- whether a bounded/immutable projection is sufficient;
- whether WP04 applies its own existing 64-row bound;
- exact observation fields needed by presentation;
- no persistence/provider types leaked.

Preferred:

> expose an immutable ordered projection of the canonical accepted observations, then let WP04 apply its already-governed 64-row bounded-window rules.

Do not pre-bound inside pipeline unless that is already canonical behavior.

---

# Phase 7 — Pipeline Result / Evidence Exposure

Determine where the Historical observation projection and feature result should be surfaced.

Evaluate:

## Option A — `PipelineExecutionResult`

Use for canonical runtime outputs needed by downstream producer.

## Option B — `PipelineExecutionEvidence`

Use if repository semantics classify these as evidence.

## Option C — Additive nested presentation-input projection

A narrow immutable sub-result attached to `PipelineExecutionResult`.

### Selection rules

Choose the contract that:

- minimizes leakage;
- is additive;
- preserves existing callers;
- exposes already-computed data;
- avoids UI-specific naming in core domain types.

Do not overload `Evidence` with runtime values if repository semantics distinguish them.

---

# Phase 8 — Historical Producer Consumption

Define exactly how WP04 Historical producer will later consume the new contract:

`Historical Worker`
→ canonical pipeline
→ additive observation/feature output
→ WP04 producer
→ Ready / WarmUp / Empty / Failed envelope

No extra fetch or calculation.

Map each WP04 field to its canonical source.

---

# Phase 9 — Replay Compatibility

Determine whether Replay should consume the same canonical feature result if Replay passes through the same pipeline.

Preferred if technically true:

- Replay and Historical both receive the same canonical `simple-return-lag-1-v1` result from the shared pipeline;
- source-specific revision semantics remain distinct;
- no Replay feature recomputation.

If Replay currently bypasses this output path, define only the minimum compatibility requirement.

Do not redesign Replay acquisition.

---

# Phase 10 — Failure Semantics

Define where feature computation failures belong.

Distinguish:

- insufficient observations => WarmUp, not failure;
- invalid observation input => existing validation/pipeline failure if applicable;
- arithmetic/domain invalidity => exact existing repository convention;
- feature result failure vs whole pipeline failure.

Do not invent a new failure taxonomy unless needed.

WP04 Failed should reflect actual canonical failure, not missing exposure.

---

# Phase 11 — Minimum Future Contract Extension

Define exact categories of additive change a later implementation authority may make.

Potentially:

- immutable feature result type;
- additive observation projection on `PipelineExecutionResult`;
- additive feature result on `PipelineExecutionResult`;
- minimal stage output extension;
- WP04 Historical producer wiring;
- focused tests.

Explicitly state whether:

- `PipelineExecutionEvidence` changes are required;
- any schema/persistence changes are required;
- any Replay changes are required.

Preferred outcome:

- no schema change;
- no persistence change;
- additive Application contract only;
- shared canonical computation.

---

# Phase 12 — Required Future Tests

Define the later implementation test contract.

At minimum:

## Feature semantics
- 0 observations => WarmUp;
- 1 observation => WarmUp;
- 2 observations => first valid feature;
- >2 observations => deterministic latest feature;
- exact feature identity;
- deterministic formula result;
- invalid input behavior.

## Observation projection
- ordered;
- immutable;
- truthful;
- no SQLite/provider reconstruction.

## Pipeline result exposure
- additive compatibility;
- existing callers unchanged;
- feature/observations available to Historical producer.

## Historical producer
- Ready from real feature;
- WarmUp from real insufficient count;
- Empty only genuine no-data;
- Failed only genuine canonical failure.

## Replay
- shared feature semantics if applicable;
- existing Replay tests unchanged.

## Regression
- WP02/WP03/WP04 predecessor suites;
- full build;
- full regression.

Do not implement tests here.

---

# Non-Goals

This definition must not authorize:

- feature computation in Streamlit;
- feature computation in a presentation-only duplicate path;
- SQLite reconstruction;
- provider refetch;
- generalized feature registry unless already existing;
- a sixth pipeline stage unless separately justified;
- schema changes;
- persistence changes;
- WP05 implementation;
- WP06 work.

---

# Stop Conditions

Stop immediately if:

- `simple-return-lag-1-v1` semantic/formula definition is not governed;
- no existing pipeline boundary can own canonical feature computation without material redesign;
- observation projection would require persistence/provider reconstruction;
- a sixth stage becomes necessary without explicit architecture authority;
- Replay compatibility would require redesign;
- multiple materially different minimal contracts remain equally valid.

On stop:

- make zero production/test/GitHub changes;
- report exact unresolved feature/pipeline contract choice;
- identify minimum additional governance authority required.

---

# Success Criteria

This definition authority succeeds only when one unambiguous Historical presentation-feature contract is established that specifies:

- exact `simple-return-lag-1-v1` semantics;
- canonical computation location;
- feature result shape;
- WarmUp semantics;
- Ready semantics;
- observation projection;
- additive pipeline/result exposure;
- WP04 producer consumption;
- Replay compatibility;
- failure semantics;
- minimum future implementation surface;
- required tests;
- non-goals.

No implementation occurs.

No GitHub mutation occurs.

#229 remains Closed / Done.

#230 remains Open / Backlog.

WP06 remains unstarted.

---

# Required Completion Report

Return:

## Feature definition
- identity;
- exact semantic/formula;
- minimum observations;
- output.

## Canonical computation location
- selected stage/boundary;
- rationale.

## Feature result contract
- fields;
- WarmUp/Available/failure semantics.

## Observation projection
- exact source;
- shape;
- ordering;
- immutability.

## Pipeline exposure
- exact result/evidence surface;
- additive compatibility.

## WP04 Historical producer mapping
Map each required envelope input to canonical source.

## Replay compatibility
State whether shared feature output applies and what remains unchanged.

## Required future implementation scope
List exact categories of changes.

## Required future tests
List exact scenarios.

## Mutation proof

Expected:

`WP04 HISTORICAL PRESENTATION-FEATURE DEFINITION MUTATIONS: ZERO`

## Next step

On success state exactly:

`WP04 HISTORICAL PRESENTATION-FEATURE CONTRACT DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

Do not implement here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP04 HISTORICAL PRESENTATION-FEATURE DEFINITION COMPLETE`

On blocker:

`RELEASE 1.9 WP04 HISTORICAL PRESENTATION-FEATURE DEFINITION BLOCKED`

Emit success only if the canonical feature/output contract is fully unambiguous.
