# Release 1.9 — WP04 Historical Presentation-Producer Completion — Codex Authority

## Authority

This document grants a **narrow predecessor-completion authority** for Release 1.9 WP04, canonical GitHub issue **#229**, solely to complete the Historical producer surface required by WP05.

WP04 itself is already closed/completed, but later WP05 production-composition proof exposed one narrow asymmetry:

- Replay Worker flow can publish a complete truthful WP04 presentation envelope.
- Historical Worker flow currently produces `PipelineExecutionEvidence` but does not expose the observation/feature inputs needed to construct the same complete truthful WP04 envelope.
- Publishing fabricated `Empty`, `WarmUp`, placeholder feature values, or synthetic observations would violate the fixed WP04 state/read-model contract.
- Reconstructing presentation data from SQLite or UI-side persistence access would violate the producer/consumer boundary.
- Recomputing features in the UI or in a second presentation path is forbidden.

Current proven state:

- WP04 #229 remains Closed / Done.
- WP05 #230 remains Open / Backlog.
- WP06 remains unstarted.
- No repository, Python, configuration, runtime, Project, or GitHub mutation occurred in the blocked WP05 pass.
- WP04 Model-C contract remains fixed.
- WP04 HistoricalPresentationRevision remains fixed.
- Replay presentation production remains valid and must not be redesigned.
- schema v4 and source-authority semantics remain fixed.
- full predecessor regression baseline remains **297/297** unless fresh validation proves otherwise.

This authority is intentionally narrow.

Its purpose is to complete the **Historical presentation producer** truthfully so that WP05 can later consume the same governed envelope for both Historical and Replay modes.

It does not authorize WP05 implementation.

It does not authorize WP06.

---

# Objective

Establish and implement the minimum Historical producer path that emits a complete, truthful WP04 presentation envelope using already-computed Historical pipeline/materialization data.

The completed Historical path must provide the same presentation contract shape as Replay without:

- fabricating observations;
- fabricating feature values;
- mislabeling state;
- reading presentation inputs back from SQLite merely for UI construction;
- recomputing features in a second path;
- redesigning Replay;
- creating a second pipeline.

The authority should prefer the narrowest producer-side extension that exposes already-existing Historical observations/features/evidence at a boundary where they truthfully coexist.

---

# Fixed WP04 Presentation Contract

Do not redesign.

## Model

Model C:

- immutable versioned snapshot;
- bounded accumulated window;
- capacity 64;
- oldest→newest;
- duplicate source-tick replacement;
- older out-of-order row ignored;
- bounded session-lifetime retention.

## Envelope

Contract version:

`aiq-visualization-read-model-v1`

The producer must truthfully populate, as applicable:

- revision kind/value;
- source mode;
- source authority;
- target;
- dataset snapshot identity/version;
- state;
- bounded observation window;
- latest observation/count;
- feature identity;
- feature value or warm-up metadata;
- pipeline/status evidence;
- validation/quality status;
- safe failure/stale metadata.

## States

Mutually exclusive:

- Ready
- Empty
- WarmUp
- Stale
- Failed

Do not use Empty/WarmUp as placeholders when required Historical inputs are merely unavailable to the current contract.

## Historical revision

Historical uses:

`RevisionKind = HistoricalPresentation`

with:

`HistoricalPresentationRevision : ulong`

Rules remain fixed:
- first newly published Historical envelope = 1;
- Ready / Empty / WarmUp / Failed increment;
- Stale does not increment;
- session-local only;
- no synthetic source tick.

## Replay

Replay remains:

`RevisionKind = ReplayLogicalTick`

using the real WP02 logical tick.

Do not alter Replay semantics.

---

# Fixed Producer / Consumer Boundary

WP04 producer owns:

- read-model construction;
- feature-state interpretation from already-computed pipeline outputs;
- bounded window;
- state transitions;
- revision assignment;
- immutable publication.

WP05 consumer must later remain read-only.

Therefore this authority must solve the missing Historical inputs **upstream of WP05**, not in Streamlit.

---

# Permitted Scope

This authority may:

- inspect current Historical Worker flow;
- inspect `PipelineExecutionResult`;
- inspect `PipelineExecutionEvidence`;
- inspect materialization outputs;
- inspect canonical stage outputs;
- inspect already-computed observation collections;
- inspect already-computed feature results;
- inspect existing WP04 producer integration;
- inspect Replay producer integration for structural comparison;
- add the minimum read-only Application/Worker contract exposure required;
- move or extend the Historical producer integration point to a boundary where required data already coexist;
- add focused WP04 Historical producer tests;
- make minimal behavior-preserving predecessor contract extensions where necessary;
- rerun predecessor suites and full regression.

This authority may modify #229 code even though #229 is closed, but only as a narrow compatibility/completion amendment supporting the already-accepted WP04 contract and only if the changes are strictly producer-side.

GitHub lifecycle for #229 should remain Closed / Done unless repository governance explicitly requires an evidence comment for the amendment.

Do not reopen #229 unless a repository/GitHub convention explicitly requires it; prefer preserving lifecycle state.

---

# Explicitly Forbidden

Do not:

- implement WP05 Streamlit/UI code;
- implement WP05 transport;
- implement WP06;
- read SQLite solely to reconstruct presentation observations/features;
- call providers solely for presentation reconstruction;
- recompute feature calculations outside the canonical pipeline;
- create a second Historical presentation pipeline;
- fabricate feature values;
- fabricate observations;
- map unavailable-data contract gaps to Empty/WarmUp dishonestly;
- redesign `ExecuteCanonical`;
- alter schema v4;
- alter source authorities;
- change WP02 Replay contracts;
- change Replay presentation producer;
- add package dependencies;
- change Python/Streamlit versions;
- alter planning/dependency chain.

---

# Phase 0 — Fresh State Proof

Before mutation:

1. Read #229 and #230.
2. Read accepted WP04 and WP05 definitions.
3. Read current Historical Worker execution path.
4. Read current Replay presentation producer path.
5. Read `PipelineExecutionResult`.
6. Read `PipelineExecutionEvidence`.
7. Read canonical materialization/stage output types.
8. Identify exactly where Historical:
   - observations exist;
   - feature definition exists;
   - feature value/warm-up status exists;
   - snapshot identity exists;
   - pipeline evidence exists.
9. Record Git state:
   - branch;
   - HEAD;
   - origin/main;
   - ahead/behind;
   - staged/tracked/relevant untracked.
10. Prove no partial WP05 implementation exists from the blocked pass.
11. Confirm #229 Closed / Done and #230 Open / Backlog.

Do not mutate until the data-flow map is explicit.

---

# Phase 1 — Historical Data-Flow Map

Produce a concrete Historical path:

`Worker`
→ historical acquisition
→ materialization
→ canonical five-stage pipeline
→ existing outputs/evidence
→ missing presentation-producer inputs

For each required WP04 envelope field, identify:

- current source;
- whether source is already computed;
- whether source is currently exposed;
- exact boundary where it becomes unavailable;
- minimum read-only exposure needed.

At minimum map:

- observation window inputs;
- latest observation;
- observation count;
- feature definition;
- feature value;
- warm-up condition/count;
- snapshot identity/version;
- source mode/authority;
- pipeline status/evidence;
- validation/quality state.

If any required presentation value is genuinely not computed by the Historical pipeline, stop. Do not invent it.

---

# Phase 2 — Candidate Producer Completion Designs

Evaluate the minimum valid options.

## Model A — Extend `PipelineExecutionResult`

Expose already-computed Historical presentation inputs through a narrow read-only result payload.

Use only if these values naturally belong to execution result semantics.

## Model B — Extend `PipelineExecutionEvidence`

Expose already-computed presentation evidence through a narrow read-only evidence structure.

Use only if these are evidence rather than runtime result data.

## Model C — Produce WP04 envelope at an earlier Worker/Application boundary

Move Historical presentation construction to the point where observations/features/snapshot/evidence already coexist, without altering canonical pipeline behavior.

## Model D — Narrow dedicated producer input record

Introduce one Application-internal read-only presentation-producer input assembled from existing canonical outputs, then feed the existing WP04 producer.

Use only if it reduces predecessor contract pollution.

### Selection rules

Choose the narrowest model that:

- uses already-computed data;
- does not create a second calculation path;
- preserves existing pipeline contracts where possible;
- keeps presentation ownership in WP04/Application/Worker;
- does not leak UI concerns into domain/persistence.

### Hard stop

If two materially different models remain equally valid after repository inspection, stop for a definition amendment rather than guessing.

---

# Phase 3 — Minimum Contract Extension

If a predecessor contract extension is required, define and implement only the minimum read-only fields.

Requirements:

- additive only;
- behavior-preserving;
- no removal/renaming of existing fields;
- no schema/persistence changes;
- no change to historical acquisition semantics;
- no change to canonical stage ordering/algorithms;
- no Replay change.

Do not expose raw internal objects if a narrow immutable projection is sufficient.

Avoid passing provider/persistence types into presentation layer.

---

# Phase 4 — Historical Producer Construction

Construct the WP04 envelope from truthful Historical data.

Required behavior:

## Ready
Use when:
- accepted Historical observations exist;
- lag-1 feature value is valid;
- pipeline execution succeeded.

Populate actual:
- bounded observations;
- latest observation;
- count;
- feature identity/value;
- snapshot identity/version;
- pipeline evidence;
- validation/quality data.

## Empty
Use only when:
- Historical execution legitimately has no observations/accepted result;
- not merely because presentation inputs were not exposed.

## WarmUp
Use only when:
- the actual lag-1 feature is not yet available due to insufficient observations;
- current count and required count=2 are truthful.

## Failed
Use actual Historical pipeline/materialization failure evidence.

Do not fabricate safe message/category beyond existing governed error mapping.

## Stale
Remain governed by the accepted WP04 structural publication semantics.

Do not invent wall-clock stale logic.

---

# Phase 5 — Historical Bounded Window Inputs

Use actual Historical observations from the canonical Historical path.

Rules:

- no SQLite re-query solely for UI;
- no provider re-fetch;
- no synthetic rows;
- preserve source-time order;
- feed the existing WP04 64-row bounded-window logic;
- do not create a second history buffer outside WP04 producer semantics.

---

# Phase 6 — Feature Inputs

Use already-computed feature outputs from the canonical five-stage pipeline.

Prove:

- feature definition identity is the same governed identity used by pipeline;
- feature value is not recomputed;
- warm-up status is derived from existing feature result semantics;
- required count=2 remains fixed by accepted WP04 contract.

If current pipeline throws away feature values before the Worker boundary, expose them minimally rather than recomputing them.

---

# Phase 7 — Historical Revision Integration

Use the existing `HistoricalPresentationRevision` producer counter.

Do not derive revision from observations, timestamps, snapshot hash, or persistence.

Publication rules remain:

- newly published Ready increments;
- Empty increments;
- WarmUp increments;
- Failed increments;
- Stale non-increment.

Producer session semantics remain unchanged.

---

# Phase 8 — Preserve Replay Producer

Do not modify Replay producer unless a shared refactor is mechanically necessary and behavior-preserving.

If shared extraction is required:

- Historical and Replay may share envelope-building helpers;
- source-specific revision semantics must remain distinct;
- Replay logical tick remains authoritative;
- Replay tests must remain unchanged/passing.

No Replay redesign.

---

# Phase 9 — Focused Historical Producer Tests

Add direct tests for:

## Ready
- real Historical observations;
- real feature value;
- real snapshot identity;
- real pipeline evidence;
- correct Historical revision.

## WarmUp
- actual insufficient-observation condition;
- current count truthful;
- required count=2;
- no fabricated feature.

## Empty
- only genuine no-data path maps to Empty.

## Failed
- actual pipeline/materialization failure maps to Failed;
- safe failure payload;
- no fabricated data.

## Bounded window
- actual Historical observations feed producer;
- capacity/order/replacement semantics preserved.

## No reconstruction
Prove:
- no SQLite readback in presentation producer;
- no provider refetch;
- no feature recomputation.

---

# Phase 10 — Contract Compatibility Tests

If `PipelineExecutionResult` / `PipelineExecutionEvidence` / another Application contract is extended:

- existing callers remain valid;
- existing tests compile/pass;
- old semantics unchanged;
- new fields are read-only/additive;
- Replay unaffected.

Do not weaken predecessor tests.

---

# Phase 11 — Real Historical Production Composition

Prove actual production path:

`Worker Historical`
→ historical acquisition
→ canonical materialization
→ canonical stages 1–5
→ existing truthful observations/features/evidence
→ WP04 Historical presentation producer
→ complete immutable Model-C envelope

Required proof:

- Ready path can produce a complete truthful envelope;
- WarmUp path can produce truthful warm-up envelope;
- Empty path is genuine;
- Failed path is genuine;
- no SQLite reconstruction;
- no feature recomputation.

This is the main acceptance gate.

---

# Phase 12 — Predecessor Regression

Revalidate:

## WP02
- Replay semantics unchanged.

## WP03
- Historical/Replay Worker paths;
- schema v4;
- source authority;
- Replay persistence;
- canonical pipeline.

## WP04
- Model C;
- HistoricalPresentationRevision;
- Replay logical ticks;
- bounded window;
- atomic in-memory publication;
- Ready/Empty/WarmUp/Stale/Failed;
- concurrency/overflow/reset tests.

Do not weaken any predecessor evidence.

---

# Phase 13 — Build and Full Regression

Run established build.

Require:
- 0 errors;
- report warnings exactly.

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate accepted predecessor baseline:

**297/297 passed**

A higher count is expected if focused Historical producer tests are added.

An unexplained lower count is a blocker.

Capture exact:
- command;
- exit code;
- passed;
- failed;
- skipped;
- warnings.

---

# Phase 14 — Diff / Scope Audit

Classify every changed file as:

- narrow Historical producer input exposure;
- Historical envelope producer integration;
- shared WP04 producer helper strictly required;
- focused Historical producer test;
- directly required compatibility test.

Prove:

- no WP05 Streamlit implementation;
- no transport implementation;
- no schema change;
- no persistence redesign;
- no provider/UI reconstruction;
- no feature recomputation;
- no pipeline algorithm change;
- no Replay semantic change;
- no WP06 work;
- no new dependency;
- no unrelated refactor;
- authority/control files preserved.

Anything unexplained blocks completion.

---

# Phase 15 — Technical Acceptance Gate

Before declaring the predecessor amendment complete, enumerate the Historical producer requirements and report PASS/FAIL.

Require explicit PASS for:

- truthful Historical observation inputs;
- truthful latest/count;
- truthful feature identity/value;
- truthful WarmUp semantics;
- genuine Empty semantics;
- genuine Failed semantics;
- snapshot identity/version;
- pipeline evidence;
- bounded window;
- Historical revision;
- no SQLite reconstruction;
- no provider refetch;
- no feature recomputation;
- no second pipeline;
- Replay unchanged;
- build;
- full regression;
- scope audit.

If any item fails, stop.

---

# GitHub Lifecycle

WP04 #229 is already Closed / Done.

Default rule:

- do **not** reopen #229;
- do **not** change Project Status;
- do **not** alter Priority/Release/Area;
- do **not** change milestone membership.

If repository governance supports evidence comments on closed issues, a concise amendment evidence comment may be added only after full technical acceptance, but it is not required unless established convention demands it.

WP05 #230 remains Open / Backlog.

Do not modify #230 lifecycle under this authority.

Do not modify #231.

---

# Expected Success State

After successful predecessor completion:

- #229 remains Closed / Done;
- #230 remains Open / Backlog;
- #231–#237 remain Open / untouched;
- milestone #58 remains Open;
- canonical milestone counts remain 8 open / 4 closed;
- WP05 becomes technically unblocked for a fresh implementation/completion authority;
- WP06 remains unstarted.

---

# Stop Conditions

Stop immediately if:

- Historical presentation requires data not actually computed by canonical pipeline;
- required fields cannot be exposed without broad predecessor redesign;
- a schema/persistence change becomes necessary;
- SQLite/UI reconstruction becomes necessary;
- feature recomputation becomes necessary;
- Replay redesign becomes necessary;
- pipeline algorithm changes become necessary;
- focused tests fail;
- predecessor tests regress;
- build/full regression fails;
- diff audit reveals unexplained scope.

On stop:

- preserve valid current state;
- do not broaden authority;
- report exact missing producer input or contract boundary;
- leave #229 Closed / Done and #230 Open / Backlog.

---

# Success Criteria

This authority succeeds only when:

- Historical mode can truthfully construct a complete WP04 envelope;
- required observations/features/evidence come from canonical Historical execution;
- no second computation/retrieval path exists;
- Model-C semantics remain exact;
- HistoricalPresentationRevision remains exact;
- Replay remains unchanged;
- focused Historical producer tests pass;
- predecessor behavior remains intact;
- build passes;
- full regression passes;
- final diff is narrowly predecessor-producer scoped;
- WP05 can later consume Historical and Replay envelopes through the same fixed transport contract.

---

# Required Completion Report

Return:

## Historical data-flow
- actual source of observations;
- feature identity/value source;
- snapshot identity/version source;
- pipeline evidence source;
- exact producer integration point.

## Contract extension
- exact types/fields added, if any;
- why minimal;
- compatibility proof.

## Historical envelope evidence
Report PASS/FAIL for:
- Ready;
- Empty;
- WarmUp;
- Failed;
- Stale behavior preservation;
- bounded window;
- revision assignment.

## No-reconstruction proof
- no SQLite readback;
- no provider refetch;
- no feature recomputation;
- no second pipeline.

## Validation
- focused Historical producer tests;
- predecessor-sensitive suites;
- build errors/warnings;
- full regression exact counts.

## Scope proof
- final diff classification;
- Replay unchanged;
- no WP05 implementation;
- no schema/persistence redesign;
- no WP06 work.

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

`RELEASE 1.9 WP04 HISTORICAL PRESENTATION-PRODUCER COMPLETION COMPLETE`

On blocker:

`RELEASE 1.9 WP04 HISTORICAL PRESENTATION-PRODUCER COMPLETION BLOCKED`

Do not emit success unless every technical acceptance gate is freshly proven.
