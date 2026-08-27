# Release 1.9 — WP04 Historical Producer Integration / Acceptance Completion — Codex Authority

## Authority

This document grants a **narrow implementation-and-acceptance completion authority** for the remaining Historical WP04 producer gap discovered after the canonical presentation-feature projection was partially implemented.

Canonical lifecycle state at entry:

- WP04 #229: **Closed / Done**
- WP05 #230: **Open / Backlog**
- WP06 #231 and later: Open / untouched
- milestone #58: Open
- schema: SQLite v4

Current proven implementation state:

- immutable `HistoricalPresentationObservation` exists;
- immutable `HistoricalPresentationFeature` exists;
- immutable `HistoricalPresentationInputs` exists;
- additive `PipelineExecutionResult.HistoricalPresentationInputs` exists;
- canonical `SimpleReturnFeatureComputer` wiring exists;
- 0/1-observation WarmUp projection exists;
- latest-value projection exists;
- existing callers remain compatible;
- build passed with **0 errors / 0 warnings**;
- existing Application/Domain/Architecture regression passed;
- no WP05 implementation exists;
- no WP06 implementation exists;
- no GitHub lifecycle mutation occurred.

Known remaining blockers:

1. no Historical WP04 producer integration exists;
2. no focused Historical producer tests exist;
3. truthful Ready / WarmUp / Empty / Failed production composition is unproven;
4. the Infrastructure test command did not yield a definitive final result in the prior run.

This authority exists only to close those gaps.

It does **not** authorize feature-contract redesign.

It does **not** authorize Replay redesign.

It does **not** authorize schema/persistence changes.

It does **not** authorize WP05 or WP06 implementation.

It does **not** reopen #229 by default.

---

# Objective

Complete and prove the real Historical WP04 producer path using the already-implemented canonical `HistoricalPresentationInputs`.

The completed production path must be:

`Worker Historical`
→ canonical Historical acquisition/materialization
→ canonical five-stage pipeline
→ canonical `HistoricalPresentationInputs`
→ WP04 Historical presentation producer
→ truthful immutable Model-C envelope

The pass must:

1. inspect and preserve the existing partial implementation;
2. wire `HistoricalPresentationInputs` into the existing WP04 producer;
3. prove truthful Ready / WarmUp / Empty / Failed behavior;
4. preserve Stale semantics;
5. add focused Historical producer tests;
6. obtain a definitive Infrastructure suite result;
7. rerun predecessor-sensitive and full regression suites;
8. keep #229 Closed / Done;
9. keep #230 Open / Backlog;
10. keep WP06 unstarted.

---

# Fixed Contracts

Do not redesign any of these.

## Feature contract

Identity:

`simple-return-lag-1-v1`

Formula:

`(current.Price / prior.Price) - 1m`

Numeric type:

`decimal`

Minimum observations:

`2`

Canonical implementation:

`SimpleReturnFeatureComputer`

WarmUp:

- 0 observations => WarmUp
- 1 observation => WarmUp
- required count = 2
- no value

Available:

- 2+ observations
- latest canonical value exposed

Invalid numeric:

- existing governed invalid-numeric evidence behavior

## Observation projection

`HistoricalPresentationObservation`:

- source timestamp
- decimal price

Ordered, immutable, truthful.

## Feature projection

`HistoricalPresentationFeature`:

- feature identity
- state
- latest value/timestamp when available
- current observation count
- required observation count = 2
- canonical failure classification when applicable

## Inputs projection

`HistoricalPresentationInputs` contains:

- ordered observations
- feature projection
- snapshot identity/version
- pipeline evidence

Exposed additively through:

`PipelineExecutionResult.HistoricalPresentationInputs`

Do not move these semantics elsewhere unless a concrete implementation defect requires only a mechanical compatibility adjustment.

---

# Fixed WP04 Envelope Contract

Model C remains:

- immutable versioned snapshot
- bounded 64-row accumulated window
- oldest→newest
- duplicate source-tick replacement
- out-of-order older rows ignored
- oldest eviction on overflow
- session-lifetime only

States:

- Ready
- Empty
- WarmUp
- Stale
- Failed

Historical revision:

`HistoricalPresentationRevision`

Rules remain:

- first published Historical envelope = 1
- Ready increments
- Empty increments
- WarmUp increments
- Failed increments
- Stale does not increment
- session-local reset
- overflow fails, never wraps

Source mode:

Historical

Source authority:

`0`

No UI reconstruction.

---

# Scope

## Permitted

May:

- inspect current partial feature/projection implementation;
- inspect current WP04 producer;
- add the minimum Historical producer wiring;
- add narrow helper/mapping code inside WP04 producer scope;
- add focused producer tests;
- add production-composition tests;
- make minimal defect fixes only when a focused test proves them;
- rerun Infrastructure/Application/Domain/Architecture/full suites;
- add a concise evidence comment to #229 only if repository convention explicitly requires it.

## Forbidden

Do not:

- redesign `HistoricalPresentationInputs`;
- recompute `simple-return-lag-1-v1`;
- duplicate `SimpleReturnFeatureComputer`;
- change five-stage pipeline architecture;
- change schema v4;
- change persistence;
- read SQLite for presentation reconstruction;
- refetch providers;
- change Replay logic/revisions;
- implement WP05 transport/Streamlit;
- implement WP06;
- reopen #229;
- change #230 lifecycle;
- alter planning/dependencies;
- add packages.

---

# Phase 0 — Fresh State Proof

Before mutation:

1. Read #229 and #230.
2. Read the prior feature-implementation authority and current diff.
3. Confirm the partial types/wiring actually exist.
4. Read current Historical Worker flow.
5. Read current WP04 producer.
6. Read current Replay producer only for structural comparison.
7. Record:
   - branch;
   - HEAD;
   - origin/main;
   - ahead/behind;
   - staged/tracked/relevant untracked state.
8. Prove:
   - #229 Closed / Done;
   - #230 Open / Backlog;
   - #231–#237 open/untouched;
   - no partial WP05 implementation exists.
9. Run/confirm build baseline:
   - 0 errors
   - 0 warnings

Do not mutate until the existing partial state is understood.

---

# Phase 1 — Producer Input Mapping

For every Historical WP04 envelope field, map the exact source from `HistoricalPresentationInputs`.

At minimum:

| Envelope field | Canonical source |
|---|---|
| sourceMode | Historical |
| sourceAuthority | 0 |
| target | existing Historical dataset/request context |
| snapshot identity/version | `HistoricalPresentationInputs` |
| observation window | ordered observations |
| latest observation | last ordered observation |
| count | observation count |
| feature identity | feature projection |
| feature value | feature projection when Available |
| warm-up count | feature projection |
| pipeline/status | existing pipeline evidence |
| validation/quality | existing pipeline evidence/validation |
| revision | existing HistoricalPresentationRevision |

Do not invent missing values.

If any required field still lacks a truthful canonical source, stop.

---

# Phase 2 — Historical Producer Wiring

Integrate `HistoricalPresentationInputs` at the smallest existing WP04 producer boundary.

Requirements:

- consume the projection directly;
- do not re-query storage;
- do not re-run feature computation;
- do not re-materialize observations;
- preserve existing bounded-window logic;
- preserve atomic publication semantics.

Prefer a narrow mapping function into the existing producer.

Do not create a second producer architecture.

---

# Phase 3 — Ready Composition

Prove and implement truthful Ready behavior.

Ready requires:

- pipeline success;
- non-empty ordered observations;
- feature state Available;
- real feature value;
- real latest observation;
- real snapshot identity/version.

Envelope must contain:

- bounded truthful observations;
- latest observation;
- count;
- feature identity/value;
- pipeline/status evidence;
- Historical source authority 0;
- Historical revision.

No placeholder values.

---

# Phase 4 — WarmUp Composition

WarmUp requires the canonical feature projection to be WarmUp.

For 0/1 observations as defined:

- preserve truthful observations;
- current observation count exact;
- required count = 2;
- no feature value;
- not Failed;
- Historical revision increments for new publication.

Important:

If the overall pipeline has genuinely zero accepted observations and WP04's fixed semantics dictate Empty instead of WarmUp at envelope level, follow the already-accepted WP04 state contract and current producer conventions.

Do not guess; prove exact 0-observation mapping from accepted WP04 tests/contracts.

---

# Phase 5 — Empty Composition

Empty is allowed only for genuine no-data/no-accepted-result semantics.

Do not use Empty for:

- missing `HistoricalPresentationInputs`;
- producer wiring gaps;
- missing feature exposure;
- errors.

Add a focused test proving genuine Empty only.

---

# Phase 6 — Failed Composition

Failed must be driven by actual canonical pipeline/materialization/feature failure evidence.

Required:

- safe category;
- safe message;
- failed revision where contract requires;
- recoverability flag;
- last-good payload retention according to fixed WP04 behavior;
- no raw stack trace;
- no fabricated observations/features.

Invalid numeric feature behavior must map through existing canonical failure evidence.

---

# Phase 7 — Stale Preservation

Do not redesign Stale.

Prove:

- Historical Stale retains prior complete payload;
- no new Historical revision merely because no newer accepted result exists;
- no wall-clock staleness introduced;
- producer integration does not accidentally publish Stale as Ready/Failed.

---

# Phase 8 — Bounded Window Compatibility

Feed actual Historical observations through the existing 64-row bounded-window logic.

Re-prove:

- oldest→newest;
- duplicate source-tick replacement;
- older out-of-order ignore;
- max 64;
- oldest eviction.

Do not create a second History buffer.

---

# Phase 9 — Focused Producer Tests

Add focused tests that exercise the actual producer mapping.

Required:

## Ready
- actual `HistoricalPresentationInputs`
- Available feature
- envelope = Ready
- feature value exact
- latest/count exact
- snapshot identity/version exact
- source authority 0

## WarmUp
- one-observation canonical input
- envelope = WarmUp
- required=2
- no feature value
- revision increments

## Zero-observation path
- prove exact accepted envelope state
- no fabricated feature/data

## Empty
- genuine no-data path only

## Failed
- actual canonical failure input
- safe failure payload
- no fake data

## Invalid numeric
- canonical feature failure maps truthfully

## Stale
- existing Stale semantics preserved

## Bounded window
- actual projection feeds existing 64-row behavior

---

# Phase 10 — Production Composition Test

Add or run a test that exercises the actual production path as directly as repository conventions allow:

`Historical Worker/use-case`
→ `PipelineExecutionResult.HistoricalPresentationInputs`
→ WP04 Historical producer
→ immutable envelope

Do not satisfy this gate only with a manually constructed DTO if the production wiring itself is the acceptance fact.

Mocks may isolate external provider/storage dependencies, but must not bypass the pipeline result-to-producer link.

---

# Phase 11 — No-Reconstruction Proof

Prove by diff/code search/tests:

- no SQLite readback added;
- no provider refetch added;
- no duplicate feature formula;
- `SimpleReturnFeatureComputer` remains sole formula implementation;
- no second pipeline;
- no WP05/Streamlit code.

Hard gate.

---

# Phase 12 — Replay Compatibility

Replay must remain unchanged.

Run existing Replay-focused tests.

If any shared producer helper was touched, prove:

- Replay logical ticks unchanged;
- Replay source authority unchanged;
- Replay envelope semantics unchanged;
- Replay acquisition/persistence unchanged.

No Replay feature redesign.

---

# Phase 13 — Infrastructure Suite Definitive Result

The prior run did not produce a final Infrastructure result.

This pass must run the governed Infrastructure test command to completion and capture:

- exact command;
- exit code;
- passed;
- failed;
- skipped;
- warnings.

Do not claim acceptance if the Infrastructure suite hangs, terminates without result, or cannot be interpreted.

If it cannot complete due environment limitations, report the exact blocker and stop.

---

# Phase 14 — Other Predecessor Suites

Rerun relevant:

- Application;
- Domain;
- Architecture;
- WP02 Replay-focused;
- WP03 Worker/schema/persistence;
- WP04 read-model/revision/concurrency.

Capture exact counts where suites expose them.

Do not weaken tests.

---

# Phase 15 — Build and Full Regression

Run established build.

Require:

- 0 errors;
- report warnings exactly.

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate historical baseline before this partial implementation began:

**297/297 passed**

If the current partial implementation has already increased total test count, record the fresh pre-completion count before adding new tests and use that as the local comparison baseline.

Final result must have:

- 0 failed;
- explained test-count increase;
- no unexplained missing tests.

Capture exact exit code/passed/failed/skipped.

---

# Phase 16 — Diff / Scope Audit

Classify every changed file as:

- pre-existing partial Historical feature/projection implementation;
- Historical producer integration;
- focused Historical producer test;
- production-composition test;
- minimal defect fix proven by focused test.

Prove:

- no WP05 implementation;
- no Streamlit;
- no transport/runtime-location/refresh implementation;
- no schema change;
- no persistence redesign;
- no SQLite/provider reconstruction;
- no formula duplication;
- no pipeline redesign;
- no Replay redesign;
- no WP06;
- no package changes;
- no unrelated refactor;
- authority/control files preserved.

Anything unexplained blocks completion.

---

# Phase 17 — Technical Acceptance Gate

Require explicit PASS for:

- `HistoricalPresentationInputs` consumed by actual WP04 producer;
- Ready truthful;
- WarmUp truthful;
- zero-observation mapping truthful;
- Empty genuine only;
- Failed truthful;
- invalid numeric truthful;
- Stale preserved;
- bounded window preserved;
- Historical revision preserved;
- snapshot identity/version propagated;
- pipeline evidence propagated;
- no SQLite reconstruction;
- no provider refetch;
- no formula duplication;
- no second pipeline;
- Replay unchanged;
- focused producer tests;
- production-composition test;
- definitive Infrastructure suite result;
- predecessor suites;
- build;
- full regression;
- scope audit.

Any FAIL stops the pass.

---

# GitHub Lifecycle

WP04 #229 is already Closed / Done.

Default:

- do not reopen;
- do not change Project Status;
- do not change Priority/Release/Area;
- do not alter milestone.

WP05 #230 remains Open / Backlog.

Do not modify #230 lifecycle.

Do not modify #231.

A closed-issue evidence comment on #229 is optional only if established repository convention requires it after predecessor amendments.

---

# Expected Success State

After success:

- #229 remains Closed / Done;
- #230 remains Open / Backlog;
- #231–#237 remain Open / untouched;
- milestone #58 remains Open;
- canonical milestone counts remain 8 open / 4 closed;
- schema remains v4;
- final successful regression count becomes the new immediate predecessor baseline for WP05 retry;
- WP05 is technically unblocked for a fresh consolidated implementation/completion retry;
- WP06 remains unstarted.

---

# Stop Conditions

Stop immediately if:

- actual WP04 Historical producer requires data outside `HistoricalPresentationInputs`;
- truthful Ready/WarmUp/Empty/Failed cannot be produced without new contract semantics;
- producer integration requires SQLite/provider reconstruction;
- feature recomputation is necessary;
- Replay redesign becomes necessary;
- schema/persistence changes become necessary;
- Infrastructure suite cannot produce a definitive result;
- focused/predecessor tests fail;
- build/full regression fails;
- diff scope is unexplained.

On stop:

- preserve valid current partial implementation;
- do not broaden authority;
- keep #229 Closed / Done;
- keep #230 Open / Backlog;
- report whether local state is valid partial or requires reconciliation.

---

# Success Criteria

This authority succeeds only when:

- Historical WP04 producer consumes the canonical projection;
- truthful Ready/WarmUp/Empty/Failed production composition is proven;
- Stale semantics remain intact;
- bounded window/revision semantics remain intact;
- no reconstruction/recomputation exists;
- Replay remains unchanged;
- focused producer tests pass;
- Infrastructure suite completes successfully with definitive result;
- predecessor suites pass;
- build passes;
- full regression passes;
- final diff remains narrowly predecessor-producer scoped;
- #229 lifecycle remains Closed / Done;
- #230 remains Open / Backlog.

---

# Required Completion Report

Return:

## Producer integration
- exact integration point;
- mapping from `HistoricalPresentationInputs`;
- files changed.

## State evidence
PASS/FAIL for:
- Ready;
- WarmUp;
- zero-observation path;
- Empty;
- Failed;
- invalid numeric;
- Stale.

## Bounded/revision evidence
- window;
- revision;
- snapshot identity/version;
- pipeline evidence.

## No-reconstruction proof
- no SQLite;
- no provider;
- no duplicate feature formula;
- no second pipeline.

## Replay compatibility
- exact test evidence.

## Validation
- focused producer tests;
- production-composition tests;
- definitive Infrastructure command/result;
- Application/Domain/Architecture and other predecessor results;
- build errors/warnings;
- full regression exact counts.

## Scope proof
- final diff classification;
- no WP05;
- no Streamlit/transport/refresh;
- no schema/persistence changes;
- no WP06.

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

`RELEASE 1.9 WP04 HISTORICAL PRODUCER-INTEGRATION ACCEPTANCE COMPLETION COMPLETE`

On blocker:

`RELEASE 1.9 WP04 HISTORICAL PRODUCER-INTEGRATION ACCEPTANCE COMPLETION BLOCKED`

Do not emit success unless every technical gate is freshly proven.
