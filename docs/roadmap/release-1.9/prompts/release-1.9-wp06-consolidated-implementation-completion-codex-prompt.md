# Release 1.9 — WP06 Consolidated Implementation / Completion — Codex Authority

## Authority

Execute Release 1.9 **WP06**, canonical GitHub issue **#231**, under this fresh consolidated implementation/completion authority.

Use **GPT-5.6 Terra**.

The previously accepted documentation artifact is binding semantic and path authority:

`docs/roadmap/release-1.9/RELEASE_1.9_WP06_VISUALIZATION_FRAME_CONTRACT_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`

Read it completely before any mutation.

Its exact:

- visualization-frame semantics;
- sequential-frame behavior;
- deterministic assertion surface;
- WP06/WP07 ownership boundary;
- shared Streamlit symbol/concern exception;
- dedicated WP06 test path;
- forbidden paths;
- stop rules

are normative.

Do not reinterpret, broaden, or replace that amendment.

---

# Entry State

Expected lifecycle:

- WP01–WP05: complete
- #230: Closed / Done
- #231: Open / Backlog
- #232: Open / Backlog
- #233–#237: Open / untouched
- milestone #58: Open
- SQLite schema: v4

Immediate predecessor technical baseline:

- full .NET regression: **305/305 passed**
- build: **0 errors / 0 warnings**
- WP05 atomic local JSON handoff accepted
- WP05 Python consumer accepted
- WP05 Streamlit entry point accepted
- Historical Worker → file → Python evidence accepted
- Replay Worker → file → Python evidence accepted

Do not assume more-specific project-suite counts without fresh verification.

---

# Binding Authority Stack

For this execution, obey:

1. this fresh consolidated WP06 implementation/completion authority;
2. `RELEASE_1.9_WP06_VISUALIZATION_FRAME_CONTRACT_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`;
3. accepted #231 / Release 1.9 WP06 definition;
4. accepted WP05 transport/consumer contracts and manifest amendment;
5. accepted WP04 presentation contracts;
6. accepted WP03/WP02 predecessor contracts;
7. Release 1.9 manifest/roadmap.

If a material conflict exists, stop before mutation and report it.

Do not invent semantics.

---

# Objective

Complete only WP06 #231 by implementing the fixed deterministic visualization-frame/rendering behavior over the already-accepted WP05 consumer state.

WP06 must prove deterministic sequential frames for:

- evolving price/time;
- latest observation;
- canonical observation count;
- bounded presentation window;
- WP06-owned supported metadata;
- backend states;
- transport warnings;

without changing the underlying WP04/WP05 data/transport semantics.

No WP07 implementation.

---

# Hard Path Gate

The binding amendment authorizes the exact WP06 surface.

At minimum, it establishes:

- a symbol/concern-level shared exception for:
  `python/presentation/realtime_financial_visualization.py`
- dedicated WP06 test path:
  `python/presentation/test_realtime_financial_visualization_wp06.py`
- no additional helper paths.

Use the **exact artifact text** as authority if its wording is more specific.

Before editing, produce an execution map for every intended path.

If any required change falls outside the amendment:

**STOP.**

Do not create a helper.
Do not reuse the WP05-exclusive test path.
Do not use WP09's Streamlit test path.
Do not broaden the shared file exception.

---

# Fixed Frame Principle

A WP06 visualization frame is a deterministic presentation projection of one accepted WP05 consumer state.

It does not:

- create its own market-data history;
- create a new revision;
- query SQLite;
- call providers;
- recompute features;
- execute the pipeline;
- infer missing observations;
- depend on wall-clock time;
- depend on refresh timing for semantic identity.

Frame identity is inherited from the accepted envelope.

---

# Fixed Price / Time Projection

Project the accepted envelope's bounded observation window **1:1**.

Each point contains only the accepted:

- source timestamp;
- decimal price.

Preserve:

- oldest → newest order;
- maximum 64 points;
- exact values.

Do not:

- resample;
- interpolate;
- sort into a new semantic order;
- normalize timestamps;
- transform prices;
- synthesize points;
- fill gaps.

---

# Fixed Latest Observation

The frame exposes the accepted latest observation:

- timestamp;
- price.

If the series is non-empty, latest must equal the final point.

A mismatch is an integrity error.

Do not silently repair it.

If the series is empty, latest must follow the exact binding amendment semantics.

---

# Fixed Count / Window Semantics

Expose separately:

- canonical `observationCount`;
- `windowCount`;
- fixed `windowCapacity = 64`.

`windowCount` is the number of projected price/time points.

Do not substitute bounded-window length for the canonical total observation count.

Enforce the invariants fixed by the binding amendment.

---

# Fixed Supported Metadata

Expose only metadata owned by WP06 in the binding amendment.

This may include the already-present factual frame metadata such as:

- source mode;
- source authority;
- target;
- backend presentation state;
- canonical feature identity/status/value or WarmUp requirement where explicitly authorized;
- high-level pipeline/status;
- validation/quality status.

Use the exact amendment boundary.

Do not add WP07-reserved summaries, annotations, controls, derived insights, panels, or styling semantics.

---

# Fixed Sequential Frame Semantics

For accepted envelope sequence:

`E1, E2, ... En`

the functional frames are deterministic projections:

`F1, F2, ... Fn`

with identity inherited from each accepted envelope.

Required behavior:

## Newer accepted envelope

Project the new frame.

## Equivalent revision/identity

Functional frame remains equivalent.

No synthetic frame number.
No artificial progression.

## Older revision

WP05 retains current accepted state; WP06 does not regress the frame.

## Equal revision with conflicting identity

Retain last-good accepted frame and expose the existing integrity warning.

Do not project conflicting payload as current.

## Historical / Replay

Preserve distinct revision kinds.

Never numerically compare Historical presentation revision with Replay logical tick.

---

# Fixed Backend State Semantics

Render exactly the accepted backend state.

## Ready

Render available series/latest/count/window and WP06-owned metadata.

## WarmUp

Render existing observations/latest/count/window plus canonical feature warm-up metadata.

No fabricated feature value.

## Empty

Render genuine backend Empty.

No series/latest beyond what the accepted envelope actually contains.

## Failed

Render safe canonical failure information and only retained payload already present in the accepted envelope.

Do not synthesize recovery data.

## Stale

Render the retained backend payload and keep backend state Stale.

Do not invent wall-clock staleness.

---

# Fixed Transport State Semantics

Transport state remains separate from backend state.

Examples:

- ProducerUnavailable;
- transient read warning;
- corrupt/read-integrity warning;
- unknown-version warning;
- revision-integrity warning.

If a last-good accepted envelope exists, the frame may continue projecting it while showing the transport warning.

Do not convert transport problems into backend Empty, Failed, or Stale.

---

# Phase 0 — Pre-Mutation Reconciliation

Before mutation:

1. read #231;
2. read the binding WP06 amendment;
3. read #232 sufficiently to preserve WP07 boundary;
4. read current:
   - `realtime_financial_visualization.py`;
   - WP05 consumer;
   - Python test conventions;
5. inspect Git state;
6. verify #230 Closed/Done;
7. verify #231 Open/Backlog;
8. verify #232 Open/Backlog;
9. verify no unauthorized partial WP06 implementation exists;
10. verify the binding documentation amendment exists and is unchanged.

Record:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged/tracked/untracked relevant paths.

---

# Phase 1 — Predecessor Validation

Before WP06 mutation, run the governed predecessor checks sufficient to prove a clean entry state.

At minimum:

- repository-standard build;
- full .NET regression:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Expected immediate predecessor total:

**305/305**

Also run existing WP05 Python consumer tests/compile checks relevant to the shared Streamlit surface.

If the predecessor baseline is not clean and cannot be fully explained, stop.

---

# Phase 2 — Exact Execution Map

List intended files.

Expected:

1. `python/presentation/realtime_financial_visualization.py`
   - shared path;
   - only amendment-authorized WP06 symbols/concerns.

2. `python/presentation/test_realtime_financial_visualization_wp06.py`
   - exclusive WP06 test path.

No other implementation/test path is expected.

If a third path is required, stop.

---

# Phase 3 — Implement Pure Frame Projection

Within the authorized shared-file surface, implement the smallest pure deterministic frame projection required by the amendment.

Prefer pure functions/data structures that can be tested without starting Streamlit.

The projection must derive only from the accepted WP05 consumer state.

No new persistent/session history beyond existing consumer state.

No duplicate cache.

No new transport parsing.

No new revision logic that competes with WP05.

---

# Phase 4 — Frame Integrity Validation

Implement only the integrity checks fixed by the amendment.

At minimum where normative:

- series length <= 64;
- exact ordered projection;
- latest equals final series point for non-empty series;
- `windowCount == len(series)`;
- fixed capacity 64;
- valid inherited revision;
- valid state;
- accepted source authority;
- frame state equals envelope state.

On integrity failure:

- preserve last-good valid presentation where governed;
- surface safe consumer/frame integrity information;
- do not mutate backend state;
- do not repair malformed data.

Do not create a generalized validation framework.

---

# Phase 5 — Deterministic Rendering Inputs

Make the rendering boundary testable as data.

Tests must be able to assert exact:

- ordered timestamps;
- ordered decimal prices;
- latest timestamp;
- latest price;
- observation count;
- window count;
- capacity;
- revision;
- state;
- source mode;
- source authority;
- target;
- WP06-owned metadata;
- transport warning.

Do not make pixel output the acceptance surface.

---

# Phase 6 — Price / Time Rendering

Use the minimal existing/supported Streamlit rendering primitive consistent with the binding amendment.

Feed it directly from the deterministic frame.

The implementation must not alter the semantic data for presentation convenience.

No new chart dependency.

No resampling/interpolation.

No WP07 styling.

---

# Phase 7 — Latest / Count / Window Rendering

Render deterministic current factual values:

- latest timestamp/price;
- canonical observation count;
- bounded window count;
- capacity where required by the amendment.

Do not create derived market metrics.

---

# Phase 8 — Supported Metadata Rendering

Render only the WP06-owned metadata explicitly defined in the binding amendment.

Do not cross into WP07.

If current code contains WP05 state/transport rendering, preserve it and extend only within the authorized WP06 concern.

---

# Phase 9 — Sequential Frame Tests

Add focused tests only in:

`python/presentation/test_realtime_financial_visualization_wp06.py`

Required scenarios from the binding contract include:

- exact one-point frame;
- exact two-point evolution;
- sequential multi-frame evolution;
- exact timestamp/price ordering;
- latest moves with newest accepted point;
- canonical observation count;
- exact window count;
- capacity 64;
- >64 total observations using canonical bounded envelope window;
- equivalent frame determinism;
- older revision does not regress current accepted frame;
- conflict retains last-good + warning;
- Historical revision kind;
- Replay revision kind;
- no cross-kind numeric ordering.

Use deterministic fixtures.

---

# Phase 10 — State Tests

Test:

- Ready;
- WarmUp;
- Empty;
- Failed;
- Stale;
- ProducerUnavailable with no accepted payload;
- transport warning with last-good payload.

Prove transport state remains separate from backend state.

---

# Phase 11 — Integrity Tests

Test all integrity conditions required by the binding amendment, including where applicable:

- latest/series mismatch;
- invalid/oversized frame;
- invalid inherited revision/state;
- count/window inconsistency.

Assert safe rejection/preservation behavior.

Do not add semantics not fixed by the amendment.

---

# Phase 12 — WP06 / WP07 Boundary Tests / Audit

Prove WP06 did not add:

- WP07-reserved metadata;
- WP07 panels;
- strategy/decision summaries;
- new controls;
- theme/styling expansion;
- annotations;
- later-work functionality.

Use static search/diff plus focused tests where appropriate.

---

# Phase 13 — Hard Presentation Boundary Audit

Search/diff prove no WP06 addition performs:

- SQLite access;
- provider calls;
- feature formula recomputation;
- Worker process control;
- pipeline execution;
- persistence writes;
- new IPC;
- filesystem handoff writes;
- unbounded history.

Also prove no change to:

- WP05 retry/cache/revision semantics;
- WP04 envelope;
- schema;
- packages;
- Streamlit pin;
- JSON-over-stdio.

Hard gate.

---

# Phase 14 — Focused Python Validation

Run the dedicated WP06 test file.

Run existing WP05 Python consumer tests affected by the shared presentation path.

Run repository-governed Python compile/import/syntax checks.

If there is an accepted exact Python command in the roadmap/repository, use it.

Capture exact counts/results.

All must pass.

---

# Phase 15 — Streamlit Validation

Validate the entry point under the exact installed Streamlit pin.

Use repository-governed smoke/import validation.

Do not launch uncontrolled long-running processes.

Where rendering primitives are difficult to assert directly, acceptance remains the deterministic data supplied to them, plus smoke validation that the entry point can consume/render that frame.

---

# Phase 16 — Predecessor Regression

Revalidate all affected predecessor behavior.

## WP05

Preserve:

- parser;
- path semantics;
- revision/cache/retry;
- ProducerUnavailable;
- last-good;
- atomic handoff consumption.

## WP04

Preserve:

- bounded 64-row window;
- Ready/Empty/WarmUp/Stale/Failed;
- Historical/Replay revision semantics.

No WP04 producer changes are expected.

## WP03 / WP02

No production changes are expected, but full regression must remain green.

---

# Phase 17 — Governed .NET Suites

Run definitively:

- Infrastructure;
- Application;
- Domain;
- Architecture.

Record exact fresh counts.

No .NET test count increase is expected from WP06 because the authorized dedicated test path is Python-only.

If .NET counts change unexpectedly, investigate.

---

# Phase 18 — Build

Run the repository-standard build.

Require:

- exit 0;
- 0 errors;
- 0 warnings unless a pre-existing governed warning is explicitly demonstrated.

Predecessor: 0 errors / 0 warnings.

---

# Phase 19 — Full .NET Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate predecessor baseline:

**305/305**

Expected WP06 result remains **305/305** unless repository evidence proves an authorized .NET test change, which this path authority does not normally permit.

Require:

- exit 0;
- 0 failed;
- exact passed/failed/skipped/total;
- no lost tests.

---

# Phase 20 — Final Manifest Scope Audit

For every changed/created path report:

- exact path;
- binding amendment entry;
- exclusive/shared;
- exact authorized concern;
- proof the diff stayed inside that concern.

Expected implementation/test mutations:

- shared Streamlit file;
- dedicated WP06 test file.

The accepted documentation amendment itself may already exist and must not be rewritten unless separately authorized.

Prove zero changes to:

- WP05-exclusive test path;
- WP09 test path;
- WP07-exclusive symbols/files;
- Worker/.NET production;
- schema/migrations;
- persistence;
- providers;
- pipeline;
- package files;
- Python requirements;
- protocol boundary.

Any mismatch => blocked.

---

# Phase 21 — Acceptance Matrix

Report PASS/FAIL for:

- binding amendment read and obeyed;
- exact path compliance;
- deterministic frame projection;
- exact price/time series;
- latest observation;
- count/window/capacity;
- supported metadata;
- newer evolution;
- equivalent determinism;
- older retention;
- conflict retention/warning;
- Historical revision kind;
- Replay revision kind;
- Ready;
- WarmUp;
- Empty;
- Failed;
- Stale;
- ProducerUnavailable;
- transport/backend separation;
- frame integrity;
- deterministic rendering input;
- Streamlit smoke;
- no SQLite;
- no provider;
- no feature recomputation;
- no Worker control;
- no new transport;
- no WP07;
- dedicated WP06 tests;
- WP05 Python regression;
- Infrastructure;
- Application;
- Domain;
- Architecture;
- build;
- full .NET regression;
- final scope audit.

Any FAIL blocks #231 closure.

---

# Phase 22 — GitHub Completion

Only after every gate passes:

1. add concise implementation/evidence comment to #231 if repository convention requires;
2. set Project #2 Status = Done;
3. preserve governed Priority / Release 1.9 / Area fields;
4. close #231.

Do not modify:

- #230;
- #232–#237.

Verify:

- #231 Closed / Done;
- #232 Open / Backlog;
- milestone #58 remains open;
- canonical milestone state becomes **6 open / 6 closed**;
- raw GitHub closed count remains one higher if historical duplicate #225 remains separately counted.

Do not start WP07.

---

# Stop Conditions

Stop if:

- any third implementation/test path is required;
- the binding amendment is missing/ambiguous;
- shared Streamlit changes exceed the authorized concern;
- WP07-owned metadata/functionality is required;
- WP09 test ownership is required;
- WP05-exclusive test ownership is required;
- a new helper file is required;
- the WP04 envelope must change;
- WP05 consumer semantics must change;
- a new dependency is required;
- SQLite/provider/persistence access is required;
- feature recomputation is required;
- a new transport is required;
- tests require screenshot/pixel infrastructure not authorized;
- Python validation fails;
- Streamlit validation fails;
- predecessor regression fails;
- build fails;
- full .NET regression fails;
- final path audit fails.

On blocker:

- preserve valid authorized partial WP06 changes;
- do not close #231;
- do not start WP07;
- report exact blocker and minimum fresh authority required.

---

# Required Completion Report

## Entry proof

- Git state;
- lifecycle;
- binding amendment presence;
- predecessor baseline.

## Execution map

Every intended/actual path and ownership.

## Implementation

- frame projection;
- integrity;
- price/time;
- latest;
- count/window;
- supported metadata;
- state/transport rendering.

## Focused evidence

- dedicated WP06 tests;
- WP05 Python regression;
- Streamlit validation.

## Boundary audit

Prove no forbidden leakage and no WP07.

## Regression

- Infrastructure;
- Application;
- Domain;
- Architecture;
- build;
- full regression.

## Scope audit

Every changed path mapped to the binding amendment.

## GitHub lifecycle

- #231 before/after;
- Project status;
- milestone counts;
- #232 untouched.

## Next eligible work package

On success state exactly:

`NEXT ELIGIBLE WORK PACKAGE: WP07 — #232`

Do not execute WP07.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP06 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

On blocker:

`RELEASE 1.9 WP06 CONSOLIDATED IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit success unless all semantic, Python, Streamlit, predecessor, build, regression, scope, and GitHub lifecycle gates pass.
