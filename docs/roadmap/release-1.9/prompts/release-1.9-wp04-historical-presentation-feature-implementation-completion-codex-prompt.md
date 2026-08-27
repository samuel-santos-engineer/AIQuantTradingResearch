# Release 1.9 — WP04 Historical Presentation-Feature Implementation / Completion — Codex Authority

## Authority

This document grants a **fresh narrow implementation/completion authority** for the Historical presentation-feature predecessor gap discovered while preparing WP05.

Canonical lifecycle state at entry:

- WP04 #229: **Closed / Done**
- WP05 #230: **Open / Backlog**
- WP06 #231 and later: Open / untouched
- milestone #58: Open
- canonical milestone counts: **8 open / 4 closed**
- schema: SQLite v4
- full regression predecessor baseline: **297/297 passed**
- build predecessor baseline: **0 errors / 0 warnings**

This authority implements the already-defined Historical presentation-feature contract.

It does **not** redesign that contract.

It does **not** authorize WP05 implementation.

It does **not** authorize WP06.

It does **not** reopen #229 by default.

---

# Fixed Historical Presentation-Feature Contract

## Feature identity

`simple-return-lag-1-v1`

## Minimum observations

`2`

## Formula

`(current.Price / prior.Price) - 1m`

Numeric type:

`decimal`

## Canonical implementation

The existing:

`SimpleReturnFeatureComputer`

remains the **sole implementation** of the formula.

Do not duplicate the formula elsewhere.

## Feature output

Canonical output is an ordered feature series keyed by each current observation timestamp.

WP04 consumes only the **latest** available value.

## Invalid numeric behavior

- zero predecessor price;
- decimal overflow;

must use the existing governed invalid-numeric evidence/failure behavior.

Do not invent a new failure taxonomy.

## Ordering

Observations must remain strictly ordered and deterministic.

---

# Fixed Canonical Computation Location

Use the existing canonical feature computation path as an **additive output of the existing `StructuredResultEvidence` stage**.

Do not:

- add a sixth stage;
- create a parallel presentation pipeline;
- compute the feature in WP04;
- compute it in WP05;
- compute it in Streamlit.

The five-stage architecture remains fixed.

---

# Fixed Projection Types

The implementation may add these narrow immutable projections.

## `HistoricalPresentationObservation`

Contains only presentation-safe observation fields required by the accepted contract:

- source timestamp;
- decimal price.

No provider/persistence types.

## `HistoricalPresentationFeature`

Contains:

- feature identity;
- state:
  - `Available`
  - `WarmUp`
  - canonical failure representation when applicable;
- latest value when available;
- latest feature timestamp when available;
- current observation count;
- required observation count = `2`;
- existing governed failure classification when applicable.

## `HistoricalPresentationInputs`

Contains:

- canonical ordered immutable observation projection;
- canonical feature projection;
- existing snapshot identity/version;
- existing pipeline evidence.

This is additive presentation-producer input, not a second presentation model.

---

# Fixed Feature Semantics

## 0 observations

Feature state:

`WarmUp`

- no value;
- current count = 0;
- required count = 2.

If the overall pipeline genuinely has no accepted observations/result, WP04 may later map the envelope to genuine Empty according to its fixed contract.

Do not conflate the feature projection's WarmUp semantics with fabricated backend state.

## 1 observation

Feature state:

`WarmUp`

- no value;
- current count = 1;
- required count = 2.

## 2 or more observations

Feature state:

`Available`

- latest canonical lag-1 feature value exposed;
- latest feature timestamp exposed;
- current count truthful;
- required count = 2.

## Invalid numeric computation

Use canonical existing invalid-numeric behavior.

WarmUp is not a pipeline failure.

---

# Fixed Observation Projection

Expose an immutable ordered projection from the canonical materialized snapshot.

Projection fields:

- source timestamp;
- price.

Requirements:

- preserve canonical ordering;
- immutable;
- no SQLite/provider type leakage;
- no UI formatting;
- no artificial 64-row bound inside the pipeline.

WP04 continues to apply its existing bounded 64-row presentation-window rules.

---

# Fixed Pipeline Exposure

Add one additive immutable:

`HistoricalPresentationInputs`

to:

`PipelineExecutionResult`

Existing callers must remain valid through an optional/additive property or equivalent backward-compatible repository convention.

The projection includes:

- observations;
- feature;
- snapshot identity/version;
- pipeline evidence.

`PipelineExecutionEvidence` may expose the same projection only if required by current repository conventions.

Do not duplicate data unnecessarily across both surfaces.

Preferred outcome:

- runtime downstream data belongs on `PipelineExecutionResult`;
- existing evidence fields remain unchanged.

No schema/persistence change is authorized.

---

# Fixed WP04 Historical Producer Mapping

The Historical producer must later consume:

- observation window input:
  canonical materialization observation projection;
- latest/count:
  ordered observation projection;
- feature identity/value:
  canonical `SimpleReturnFeatureComputer` output;
- WarmUp:
  canonical observation count < 2;
- snapshot identity/version:
  canonical pipeline provenance;
- pipeline/status:
  existing `PipelineExecutionEvidence`;
- source mode:
  Historical;
- source authority:
  `0`;
- revision:
  existing `HistoricalPresentationRevision`.

Do not reconstruct any of this from persistence.

---

# Fixed Replay Compatibility

Replay may consume the same additive canonical feature output when it already passes through the shared pipeline.

But:

- Replay logical ticks remain unchanged;
- Replay revision semantics remain unchanged;
- Replay acquisition remains unchanged;
- no Replay redesign is authorized.

Any shared code change must be behavior-preserving for Replay.

---

# Objective

Implement the fixed canonical Historical observation/feature projection and wire it through the existing five-stage pipeline so that the already-accepted WP04 Historical producer can construct truthful Ready/WarmUp/Empty/Failed envelopes.

The pass must:

1. implement the immutable projection types;
2. expose canonical ordered observations;
3. wire `SimpleReturnFeatureComputer` into the existing structured-evidence stage output;
4. expose `HistoricalPresentationInputs` additively through `PipelineExecutionResult`;
5. update the WP04 Historical producer to consume these canonical inputs;
6. preserve Replay;
7. add focused tests;
8. run predecessor regression;
9. keep #229 Closed / Done;
10. keep #230 Open / Backlog.

---

# Phase 0 — Fresh Pre-Mutation Proof

Before mutation:

1. Read #229 and #230.
2. Read accepted WP04/WP05 definitions.
3. Read:
   - five canonical stages;
   - `StructuredResultEvidence` stage;
   - `SimpleReturnFeatureComputer`;
   - `PipelineExecutionResult`;
   - `PipelineExecutionEvidence`;
   - materialized snapshot/result types;
   - current WP04 producer;
   - Replay producer path.
4. Record Git state:
   - branch;
   - HEAD;
   - origin/main;
   - ahead/behind;
   - staged paths;
   - tracked changes;
   - relevant untracked authority/control files.
5. Prove:
   - #229 remains Closed / Done;
   - #230 remains Open / Backlog;
   - #231–#237 remain Open / untouched;
   - milestone #58 remains Open;
   - no partial WP05 implementation exists from the blocked pass.
6. Run or confirm the immediate accepted baseline:
   - build 0 errors / 0 warnings;
   - full regression 297/297.

If unexpected repository state conflicts with the fixed contract, stop.

---

# Phase 1 — Exact Data-Flow Map

Before coding, map actual canonical Historical flow:

`historical acquisition`
→ materialized snapshot
→ ordered observations
→ five-stage pipeline
→ structured evidence stage
→ feature computation
→ `PipelineExecutionResult`
→ WP04 Historical producer

For each required field, identify the exact existing source.

Do not code until this map proves all inputs are already available or can be computed canonically with `SimpleReturnFeatureComputer`.

If any required input is genuinely absent from the canonical pipeline inputs, stop.

---

# Phase 2 — Implement Immutable Projection Types

Implement only the fixed narrow types:

- `HistoricalPresentationObservation`
- `HistoricalPresentationFeature`
- `HistoricalPresentationInputs`

Requirements:

- immutable;
- Application/pipeline-safe;
- no UI/Streamlit naming where repository conventions prefer neutral names;
- no provider objects;
- no persistence records;
- no mutable collections exposed.

Use existing record/value-object conventions.

---

# Phase 3 — Observation Projection Wiring

From the canonical materialized snapshot:

- create ordered immutable observation projection;
- preserve exact canonical source timestamp and decimal price;
- preserve strict deterministic ordering;
- do not query SQLite;
- do not refetch providers;
- do not sort by an invented secondary key.

Add tests for ordering and immutability.

---

# Phase 4 — Canonical Feature Wiring

Wire `SimpleReturnFeatureComputer` into the existing canonical `StructuredResultEvidence` feature-computation path.

Requirements:

- no formula duplication;
- no sixth stage;
- no presentation-only calculation;
- deterministic ordered feature output;
- use existing invalid-numeric evidence behavior.

For 0/1 observations:

- feature projection = WarmUp;
- no feature value.

For >=2:

- compute canonical series;
- expose latest value + latest current-observation timestamp.

If `SimpleReturnFeatureComputer` currently assumes >=2 and cannot accept 0/1, gate its invocation canonically rather than modifying its mathematics unnecessarily.

---

# Phase 5 — Exact Formula / Feature Identity Tests

Add focused tests proving:

- identity exactly `simple-return-lag-1-v1`;
- two-observation result exactly follows:
  `(current.Price / prior.Price) - 1m`;
- decimal type preserved;
- multiple observations produce deterministic latest value;
- timestamp key corresponds to current observation;
- ordered inputs produce ordered outputs.

Use exact decimal expectations.

---

# Phase 6 — WarmUp Tests

Prove:

## 0 observations
- feature state WarmUp;
- current=0;
- required=2;
- no value.

## 1 observation
- feature state WarmUp;
- current=1;
- required=2;
- no value.

WarmUp must not become pipeline failure.

---

# Phase 7 — Invalid-Numeric Tests

Using existing governed behavior, prove:

- zero predecessor price produces canonical invalid-numeric classification;
- decimal overflow produces canonical invalid-numeric classification;
- no unhandled arithmetic behavior leaks into WP04 projection;
- no fabricated numeric value is emitted.

Do not create a new error category.

---

# Phase 8 — `HistoricalPresentationInputs` Exposure

Expose the additive projection through `PipelineExecutionResult`.

Requirements:

- existing constructors/callers remain compatible using repository-standard additive approach;
- existing result semantics unchanged;
- projection contains:
  - observations;
  - feature;
  - snapshot identity/version;
  - pipeline evidence.
- no schema/persistence changes.

If adding the property requires touching many callers, prefer repository-supported optional/default-compatible construction.

Do not break existing APIs unnecessarily.

---

# Phase 9 — WP04 Historical Producer Integration

Update the existing Historical WP04 producer to consume `HistoricalPresentationInputs`.

Required truthful mapping:

## Ready
Use when:
- observations exist;
- feature is Available;
- pipeline succeeded.

Use actual:
- observations;
- latest;
- count;
- feature identity/value;
- snapshot identity/version;
- pipeline evidence.

## WarmUp
Use when:
- canonical feature projection is WarmUp;
- no fabricated feature value;
- observation count truthful;
- required count=2.

## Empty
Use only for genuine canonical no-data/no-accepted-result semantics.

Do not map missing projection to Empty.

## Failed
Use actual canonical failure/invalid-numeric/pipeline evidence according to fixed WP04 failure mapping.

No persistence reconstruction.

---

# Phase 10 — Bounded Window Compatibility

Feed the truthful Historical observations into the already-existing WP04 64-row bounded-window logic.

Do not:

- add another history buffer;
- pre-truncate canonically unless WP04 already owns the truncation;
- change duplicate/order semantics.

Re-run WP04 boundedness tests.

---

# Phase 11 — Historical Revision Preservation

Use existing `HistoricalPresentationRevision`.

Do not alter:

- initial revision;
- Ready increment;
- Empty increment;
- WarmUp increment;
- Failed increment;
- Stale non-increment;
- restart/reset;
- overflow behavior.

The feature projection does not become a new revision source.

---

# Phase 12 — Replay Compatibility

If Replay traverses the same canonical pipeline:

- it may receive the same feature projection;
- Replay producer may consume it only where already compatible;
- no logical-tick changes;
- no source-authority changes;
- no acquisition changes.

If no Replay producer change is required, prefer zero Replay production changes.

Run existing Replay-focused tests unchanged.

---

# Phase 13 — No-Reconstruction / No-Recomputation Proof

Prove by code inspection/tests:

- no SQLite read was added for presentation inputs;
- no provider refetch was added;
- no feature formula duplication exists;
- only `SimpleReturnFeatureComputer` computes the formula;
- no second feature/presentation pipeline exists;
- no WP05/Streamlit code was added.

This is a hard acceptance gate.

---

# Phase 14 — Focused Historical Producer Tests

Add focused tests for:

- Ready from actual canonical feature;
- WarmUp with 0/1 observations;
- genuine Empty path;
- canonical failure path;
- invalid numeric path;
- ordered observation projection;
- latest/count truthfulness;
- snapshot identity/version propagation;
- pipeline evidence propagation;
- Historical source authority 0;
- Historical revision preservation;
- 64-row bounded producer behavior.

Avoid mock-only tests that bypass the actual canonical result exposure when the acceptance fact is production wiring.

---

# Phase 15 — Contract Compatibility Tests

Prove:

- existing `PipelineExecutionResult` callers remain valid;
- existing `PipelineExecutionEvidence` semantics unchanged;
- existing Historical pipeline behavior unchanged except additive outputs;
- Replay behavior unchanged;
- schema v4 unchanged;
- source authorities unchanged.

---

# Phase 16 — Real Historical Production Composition

Prove actual production code path:

`Worker Historical`
→ historical acquisition
→ canonical materialization
→ ordered observation projection
→ canonical five-stage pipeline
→ `SimpleReturnFeatureComputer`
→ structured evidence stage output
→ `PipelineExecutionResult.HistoricalPresentationInputs`
→ WP04 Historical producer
→ truthful immutable envelope

Required production-state proof:

- Ready works;
- WarmUp works;
- Empty is genuine;
- Failed is genuine;
- no reconstruction/recomputation.

If actual production composition cannot be proven, stop.

---

# Phase 17 — Predecessor Regression Gates

Revalidate:

## WP02
- Replay identity;
- logical ticks;
- restart/resume;
- duplicate determinism;
- cancellation;
- bounds;
- finite completion.

## WP03
- Historical/Replay dispatch;
- Dataset boundary;
- schema v4;
- authorities 0/1;
- Replay persistence;
- canonical pipeline.

## WP04
- Model C;
- 64-row bound;
- all five states;
- HistoricalPresentationRevision;
- Replay revision;
- atomic publication;
- concurrency;
- overflow/reset.

Do not weaken predecessor tests.

---

# Phase 18 — Build and Full Regression

Run established build.

Require:

- 0 errors;
- report warnings exactly.

Then run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate predecessor baseline:

**297/297 passed**

A higher count is expected due to focused tests.

An unexplained lower count is a blocker.

Capture exact:

- command;
- exit status;
- passed;
- failed;
- skipped;
- material warnings.

---

# Phase 19 — Diff / Scope Audit

Classify every changed file as:

- immutable Historical presentation projection type;
- structured-evidence feature wiring;
- additive `PipelineExecutionResult` exposure;
- Historical WP04 producer integration;
- focused Historical feature/producer test;
- directly required compatibility test.

Prove:

- no WP05 implementation;
- no Streamlit code;
- no transport/runtime-location/refresh implementation;
- no schema change;
- no persistence redesign;
- no provider refetch;
- no SQLite presentation readback;
- no formula duplication;
- no sixth stage;
- no Replay semantic redesign;
- no WP06 work;
- no new dependency;
- no unrelated refactor;
- authority/control files preserved.

Anything unexplained blocks completion.

---

# Phase 20 — Technical Acceptance Gate

Require explicit PASS for:

- feature identity;
- exact formula;
- 0-observation WarmUp;
- 1-observation WarmUp;
- 2-observation Available;
- multi-observation deterministic latest value;
- invalid predecessor price;
- decimal overflow behavior;
- immutable ordered observation projection;
- additive result compatibility;
- truthful Ready;
- truthful WarmUp;
- genuine Empty;
- genuine Failed;
- snapshot identity/version propagation;
- pipeline evidence propagation;
- Historical revision preservation;
- bounded window compatibility;
- no SQLite reconstruction;
- no provider refetch;
- no formula recomputation;
- no second pipeline;
- Replay unchanged;
- build;
- full regression;
- scope audit.

Any FAIL stops the pass.

---

# GitHub Lifecycle

WP04 #229 is already Closed / Done.

Default lifecycle rule:

- do not reopen #229;
- do not change Project Status;
- do not modify Priority/Release/Area;
- do not alter milestone membership.

WP05 #230 remains Open / Backlog.

Do not change #230 lifecycle.

Do not modify #231.

A concise evidence comment on closed #229 may be added only if established repository convention explicitly requires it. Otherwise make zero GitHub mutation.

---

# Expected Success State

After success:

- #229 remains Closed / Done;
- #230 remains Open / Backlog;
- #231–#237 remain Open / untouched;
- milestone #58 remains Open;
- canonical milestone counts remain **8 open / 4 closed**;
- schema remains v4;
- new successful .NET regression count becomes the immediate predecessor baseline for the retried WP05 pass;
- WP05 becomes technically eligible for a fresh consolidated implementation/completion retry;
- WP06 remains unstarted.

---

# Stop Conditions

Stop immediately if:

- the fixed formula/feature contract conflicts with `SimpleReturnFeatureComputer`;
- canonical feature output requires a sixth stage;
- required observations cannot be projected without reconstruction;
- additive result exposure requires broad API redesign;
- schema/persistence change becomes necessary;
- SQLite/provider reconstruction becomes necessary;
- feature recomputation becomes necessary;
- Replay redesign becomes necessary;
- production composition cannot be proven;
- focused/predecessor tests fail;
- build/full regression fails;
- diff scope is unexplained.

On stop:

- preserve valid work;
- do not broaden authority;
- keep #229 Closed / Done;
- keep #230 Open / Backlog;
- report exact blocker and whether local state is valid partial or requires reconciliation.

---

# Required Completion Report

Return:

## Canonical feature implementation
- exact `SimpleReturnFeatureComputer` wiring;
- feature identity;
- formula evidence;
- stage/boundary.

## Projection types
- `HistoricalPresentationObservation`;
- `HistoricalPresentationFeature`;
- `HistoricalPresentationInputs`;
- exact immutable fields.

## Pipeline exposure
- exact additive `PipelineExecutionResult` change;
- compatibility evidence.

## Historical producer
Report PASS/FAIL for:
- Ready;
- WarmUp;
- Empty;
- Failed;
- bounded window;
- revision.

## No-reconstruction proof
- no SQLite;
- no provider refetch;
- no duplicate feature computation;
- no second pipeline.

## Replay compatibility
State exact impact and test evidence.

## Validation
- focused feature tests;
- focused Historical producer tests;
- predecessor-sensitive suites;
- build errors/warnings;
- full regression exact counts.

## Scope proof
- final diff classification;
- no WP05 implementation;
- no schema/persistence change;
- no WP06;
- no unauthorized foundation/planning changes.

## Lifecycle
State:
- #229 remains Closed / Done;
- #230 remains Open / Backlog;
- milestone counts unchanged.

## Next step

On success state exactly:

`WP05 MAY BE RETRIED UNDER A FRESH CONSOLIDATED IMPLEMENTATION/COMPLETION AUTHORITY`

Do not execute WP05 here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP04 HISTORICAL PRESENTATION-FEATURE IMPLEMENTATION AND COMPLETION COMPLETE`

On blocker:

`RELEASE 1.9 WP04 HISTORICAL PRESENTATION-FEATURE IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit success unless every technical gate is freshly proven.
