# Release 1.9 — WP04 Historical Production-Composition Acceptance-Test — Codex Authority

## Authority

This document grants a **narrow acceptance-test authority** for the sole remaining WP04 Historical predecessor gate required before WP05 can be retried.

Canonical lifecycle state at entry:

- WP04 #229: **Closed / Done**
- WP05 #230: **Open / Backlog**
- WP06 #231 and later: Open / untouched
- milestone #58: Open
- canonical milestone counts: **8 open / 4 closed**
- raw GitHub closed count also includes historical duplicate #225
- schema: SQLite v4

Current proven implementation/evidence:

- Historical presentation projections are implemented.
- Canonical `SimpleReturnFeatureComputer` wiring is implemented.
- Historical `PipelineExecution` invokes the WP04 producer through `PipelineExecutionResult.HistoricalPresentationInputs`.
- Successful canonical Historical results map to truthful Ready, WarmUp, or genuine Empty.
- Canonical failures map to Failed through existing safe failure categories.
- No SQLite reconstruction, provider refetch, duplicate feature computation, Replay redesign, WP05, or WP06 work exists.
- Infrastructure: **151/151 passed**, 0 failed, 0 skipped, exit 0.
- Application: **122/122 passed**.
- Domain: **11/11 passed**.
- Architecture: **13/13 passed**.
- Focused WP04 read-model tests: **7/7 passed**.
- Build: **0 errors / 0 warnings**.
- Full regression: **297/297 passed**, 0 failed, 0 skipped, exit 0.
- `INFRASTRUCTURE EVIDENCE MUTATIONS: ZERO`.

The sole remaining blocker is:

> No focused acceptance test invokes the actual Historical `PipelineExecution` production composition and asserts the resulting published WP04 envelope.

The unchanged full-suite count of 297 confirms that missing acceptance coverage has not yet been added.

This authority exists only to add that missing production-composition acceptance coverage and complete the predecessor evidence.

---

# Objective

Add the minimum focused acceptance test(s) that exercise the real Historical production composition:

`Historical PipelineExecution`
→ canonical pipeline
→ `PipelineExecutionResult.HistoricalPresentationInputs`
→ WP04 Historical producer
→ published immutable envelope

The test must prove actual production wiring rather than only isolated DTO/producer behavior.

Production-code changes are forbidden by default.

A production change is allowed only if the new acceptance test proves a concrete defect in the existing WP04 Historical composition.

---

# Model Recommendation

Use **GPT-5.6 Terra**.

This pass is test implementation plus targeted regression, not contract design.

---

# Fixed Contracts

Do not redesign:

- `HistoricalPresentationObservation`
- `HistoricalPresentationFeature`
- `HistoricalPresentationInputs`
- additive `PipelineExecutionResult.HistoricalPresentationInputs`
- canonical `SimpleReturnFeatureComputer`
- Model-C WP04 envelope
- 64-row bounded window
- HistoricalPresentationRevision
- Replay logical-tick revision
- Ready / Empty / WarmUp / Stale / Failed
- atomic publication semantics

No schema/persistence/Replay/WP05/WP06 changes are authorized.

---

# Permitted Scope

May:

- inspect current Historical `PipelineExecution`;
- inspect the WP04 producer/store;
- inspect existing test fixtures/builders;
- add focused production-composition test(s);
- add test-only fixtures/helpers;
- use existing fakes/mocks for external dependencies only if they do not bypass the actual Historical pipeline-result→producer composition;
- make a minimal production fix only when a focused test proves a concrete defect;
- rerun focused and full validation.

---

# Explicitly Forbidden

Do not:

- add a parallel test-only producer path;
- manually construct the final envelope and call that production composition;
- bypass `PipelineExecution`;
- mock away `PipelineExecutionResult.HistoricalPresentationInputs` creation;
- recompute the simple-return feature in the test;
- read SQLite for presentation reconstruction;
- refetch providers for presentation;
- redesign producer contracts;
- change Replay behavior;
- alter schema/persistence;
- implement WP05 transport/Streamlit;
- implement WP06;
- add packages;
- reopen #229;
- mutate #230 lifecycle.

---

# Phase 0 — Fresh State Proof

Before mutation:

1. Read #229 and #230.
2. Read the current Historical `PipelineExecution`.
3. Read the WP04 Historical producer integration.
4. Read existing tests around:
   - pipeline execution;
   - WP04 producer;
   - Replay production composition.
5. Record Git state:
   - branch;
   - HEAD;
   - origin/main;
   - ahead/behind;
   - staged/tracked/relevant untracked state.
6. Confirm:
   - #229 Closed / Done;
   - #230 Open / Backlog;
   - no WP05/WP06 implementation exists.
7. Confirm current baseline where practical:
   - Infrastructure 151/151;
   - Application 122/122;
   - Domain 11/11;
   - Architecture 13/13;
   - full regression 297/297.

Do not alter production code in this phase.

---

# Phase 1 — Identify the Real Composition Boundary

Document the exact production call path used by Historical mode.

At minimum identify:

- Historical Worker/use-case entry point;
- `PipelineExecution` invocation;
- canonical materialization/pipeline path;
- creation of `HistoricalPresentationInputs`;
- invocation of WP04 Historical producer;
- publication target/store;
- how the final envelope can be observed in the test.

The acceptance test must traverse this real path.

If production composition occurs behind DI, use the actual production registrations where repository test conventions permit.

---

# Phase 2 — Test Fixture Design

Use the minimum deterministic fixture that produces known Historical observations and expected feature/state.

Preferred:

- deterministic in-memory/fake Historical observation source/store;
- no external provider/network dependency;
- no SQLite reconstruction for presentation;
- same canonical pipeline path as production;
- actual producer/store instance.

External boundaries may be faked, but the following must remain real:

- `PipelineExecution`;
- canonical feature wiring;
- `HistoricalPresentationInputs`;
- WP04 producer;
- published envelope/store.

Do not hand-build `HistoricalPresentationInputs` for the main production-composition acceptance test.

---

# Phase 3 — Required Ready Production-Composition Test

Add at least one test proving a real Historical Ready path.

The test must:

1. arrange at least two deterministic Historical observations;
2. invoke the actual Historical `PipelineExecution` composition;
3. allow canonical `SimpleReturnFeatureComputer` execution;
4. allow `HistoricalPresentationInputs` creation;
5. allow the WP04 producer to publish;
6. read the published envelope;
7. assert:
   - state = Ready;
   - source mode = Historical;
   - source authority = 0;
   - observation count truthful;
   - latest observation truthful;
   - feature identity = `simple-return-lag-1-v1`;
   - feature value equals canonical expected decimal;
   - snapshot identity/version propagated;
   - pipeline evidence/status propagated;
   - Historical revision exists and is valid;
   - bounded window contains expected ordered observations.

The expected feature value may be calculated in the test assertion from fixed literal values, but do not call a duplicate production feature implementation.

---

# Phase 4 — WarmUp Production-Composition Test

If not already directly proven through actual composition, add a test with one Historical observation.

Must prove:

- actual `PipelineExecution` path;
- canonical Historical input projection;
- WP04 producer publication;
- state = WarmUp;
- current count = 1;
- required count = 2;
- no feature value;
- Historical source authority 0;
- Historical revision publication semantics preserved.

Do not manually invoke only the producer.

---

# Phase 5 — Zero-Observation / Empty Composition

Determine accepted current WP04 semantics for zero observations.

Add a production-composition test if required to prove the real path.

Must verify one exact accepted outcome:

- genuine Empty; or
- another already-governed zero-observation mapping.

Do not invent new semantics.

The test must traverse the real Historical composition path.

---

# Phase 6 — Failed Composition

If current test coverage does not exercise actual Historical production failure composition, add one deterministic failure case.

Permitted failure source:

- an existing canonical validation/materialization/feature failure that can be triggered safely in test.

Prove:

- real `PipelineExecution` path;
- producer receives canonical failure;
- envelope = Failed;
- safe category/message;
- no raw stack trace;
- no fabricated observations/feature;
- Historical revision behavior correct.

Do not add artificial production hooks merely to force failure.

---

# Phase 7 — Production Publication Assertion

The acceptance fact is not complete until the test observes the actual published envelope.

Use the real WP04 publication store/abstraction.

Prove:

- producer was actually invoked;
- publication occurred;
- envelope is immutable/complete;
- the asserted envelope was not manually constructed by the test.

This is the core gate.

---

# Phase 8 — No-Reconstruction / No-Recomputation Assertions

For the new test path, prove:

- no SQLite presentation reconstruction;
- no provider refetch;
- no feature recomputation outside `SimpleReturnFeatureComputer`;
- no second pipeline;
- no test-only shortcut around `HistoricalPresentationInputs`.

Use code inspection plus test fixture structure.

---

# Phase 9 — Mutation Gate

Default:

`HISTORICAL PRODUCTION-COMPOSITION PRODUCTION MUTATIONS: ZERO`

If the new acceptance test fails because of a concrete production defect:

1. identify exact defect;
2. prove it lies within existing Historical producer integration;
3. make the smallest fix;
4. rerun the test;
5. rerun neighboring WP04 tests.

Do not redesign contracts.

If a new contract decision is required, stop.

---

# Phase 10 — Focused Test Result

Run the new Historical production-composition acceptance test(s) directly.

Capture:

- exact command;
- passed;
- failed;
- skipped;
- total.

All new focused tests must pass.

---

# Phase 11 — Definitive Infrastructure Result

Rerun the full governed Infrastructure suite.

Expected predecessor evidence:

**151/151 passed**

After adding new Infrastructure test(s), the count should increase if those tests live in Infrastructure.

Capture definitive:

- command;
- exit code;
- passed;
- failed;
- skipped;
- total;
- duration if available.

No output/session ambiguity is acceptable.

---

# Phase 12 — Other Predecessor Suites

Rerun and capture:

- Application — predecessor evidence 122/122;
- Domain — 11/11;
- Architecture — 13/13;
- relevant WP02 Replay suite;
- relevant WP03 Worker/schema/persistence suite;
- WP04 read-model/revision/concurrency tests.

All must remain green.

---

# Phase 13 — Build

Run established build.

Require:

- exit code 0;
- 0 errors;
- report warnings exactly.

Expected:

- 0 errors;
- 0 warnings.

---

# Phase 14 — Full Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Current predecessor baseline:

**297/297 passed**

Because at least one new acceptance test is expected, final total should normally be **>297**.

Acceptance requires:

- exit code 0;
- 0 failed;
- exact passed/failed/skipped/total;
- all count increase explained by the new test(s);
- no disappeared predecessor tests.

The final passing count becomes the new WP05 predecessor baseline.

---

# Phase 15 — Diff / Scope Audit

Classify every changed file as:

- Historical production-composition acceptance test;
- test-only fixture/helper;
- minimal production defect fix proven by the test, if any.

Prove:

- no WP05;
- no Streamlit;
- no atomic-file transport;
- no refresh/retry;
- no WP06;
- no schema/persistence change;
- no Replay redesign;
- no duplicate feature formula;
- no SQLite/provider reconstruction;
- no package/foundation change;
- no unrelated refactor.

Anything unexplained blocks acceptance.

---

# Phase 16 — Acceptance Gate

Require explicit PASS for:

- actual Historical `PipelineExecution` invoked;
- canonical feature computation invoked;
- `HistoricalPresentationInputs` produced;
- WP04 Historical producer invoked;
- actual envelope published;
- Ready production composition;
- WarmUp production composition if required;
- zero-observation/Empty production composition if required;
- Failed production composition if required;
- source authority 0;
- feature identity/value truthfulness;
- observation/latest/count truthfulness;
- snapshot identity/version propagation;
- pipeline evidence propagation;
- Historical revision truthfulness;
- bounded window;
- no reconstruction/recomputation;
- Replay unchanged;
- definitive Infrastructure suite;
- Application;
- Domain;
- Architecture;
- build;
- full regression;
- scope audit.

Any FAIL means BLOCKED.

---

# GitHub Lifecycle

Under this authority:

- #229 remains **Closed / Done**;
- do not reopen #229;
- preserve its Project fields and milestone;
- #230 remains **Open / Backlog**;
- do not mutate #230;
- do not modify #231 or later.

A concise evidence comment on #229 is optional only if established repository convention explicitly requires it after predecessor acceptance completion.

Otherwise GitHub mutations should be zero.

---

# Expected Success State

After success:

- missing Historical production-composition acceptance test exists and passes;
- complete Infrastructure suite passes with a new definitive count;
- Application/Domain/Architecture/predecessor suites remain green;
- build passes;
- full regression passes with a count greater than or equal to the predecessor 297 and explained by new tests;
- #229 remains Closed / Done;
- #230 remains Open / Backlog;
- milestone remains Open with canonical 8 open / 4 closed;
- WP05 becomes eligible for a fresh consolidated implementation/completion retry;
- WP06 remains unstarted.

---

# Stop Conditions

Stop if:

- real Historical composition cannot be exercised without new contract semantics;
- the test would need to bypass `PipelineExecution`;
- production defect requires broad redesign;
- schema/persistence/Replay changes become necessary;
- WP05/WP06 work becomes necessary;
- any focused or predecessor test fails;
- Infrastructure lacks a definitive result;
- build fails;
- full regression fails;
- diff scope is unexplained.

On stop:

- preserve valid current implementation;
- preserve useful test evidence;
- keep #229 Closed / Done;
- keep #230 Open / Backlog;
- report exact missing composition fact or defect.

---

# Required Completion Report

Return:

## Production composition path
- exact types/methods traversed;
- actual producer/store used.

## New acceptance tests
For each:
- scenario;
- command;
- result.

## Published envelope evidence
PASS/FAIL for:
- Ready;
- WarmUp;
- zero-observation/Empty;
- Failed;
- source authority;
- feature;
- observations;
- snapshot identity/version;
- pipeline evidence;
- Historical revision;
- bounded window.

## Mutations
State either:

`HISTORICAL PRODUCTION-COMPOSITION PRODUCTION MUTATIONS: ZERO`

or list every narrowly proven production defect fix.

## Validation
- Infrastructure exact count;
- Application exact count;
- Domain exact count;
- Architecture exact count;
- other predecessor-sensitive results;
- build errors/warnings;
- full regression exact count.

## Scope audit
- changed-file classification;
- no WP05/WP06;
- no schema/persistence/Replay redesign;
- no reconstruction/recomputation.

## Lifecycle
State:
- #229 remains Closed / Done;
- #230 remains Open / Backlog;
- milestone counts unchanged.

## Next step

On success state exactly:

`WP05 MAY BE RETRIED UNDER A FRESH CONSOLIDATED IMPLEMENTATION/COMPLETION AUTHORITY`

Do not execute WP05.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP04 HISTORICAL PRODUCTION-COMPOSITION ACCEPTANCE TEST COMPLETE`

On blocker:

`RELEASE 1.9 WP04 HISTORICAL PRODUCTION-COMPOSITION ACCEPTANCE TEST BLOCKED`

Do not emit success unless the real Historical production composition is directly exercised and the complete regression gates pass.
