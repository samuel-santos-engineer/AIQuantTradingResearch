# Release 1.9 — WP05 Worker Handoff Test-Isolation / Deterministic-Regression — Codex Authority

## Authority

This document grants a **narrow predecessor-stabilization authority** for the accepted WP05 Worker handoff and its Infrastructure regression behavior.

Use **GPT-5.6 Sol**.

WP05 #230 is already **Closed / Done** and its production semantics are accepted.

WP06 #231 is currently **Open / Backlog** but blocked before mutation because the immediate predecessor full regression is nondeterministic:

- expected predecessor baseline: **305/305**;
- first clean full run: **304/305**, Infrastructure failure involving a locked WP05 handoff temp file;
- the affected test passed in isolation;
- second clean full rerun: **304/305**, a different Infrastructure Worker test failed with non-zero Worker exit code;
- WP05 Python checks passed;
- build passed with **0 warnings / 0 errors**;
- no WP06 production/test mutation occurred.

This authority exists only to diagnose and eliminate proven **test/runtime interference around the existing WP05 Worker handoff**, restoring a deterministic predecessor baseline for WP06.

It does **not** authorize redesign of WP05 production behavior.

It does **not** authorize WP06 implementation.

It does **not** authorize WP07.

---

# Objective

Identify the exact source of nondeterministic Infrastructure interference affecting the accepted WP05 handoff and Worker tests, then make only the minimum proven fix needed to restore deterministic full-suite execution.

Primary suspects to investigate include:

- shared canonical handoff path collisions;
- shared temp-file names/patterns;
- incomplete Worker/process disposal;
- lingering file handles;
- overlapping Worker lifetimes;
- stale temp artifacts;
- test fixture reuse;
- parallel test execution against a shared runtime path;
- environment-variable/configuration leakage between tests;
- process cleanup ordering.

Do not assume the cause. Prove it.

---

# Accepted WP05 Production Contract

Do not redesign:

- local atomic JSON handoff;
- canonical runtime path semantics;
- `Visualization:HandoffPath`;
- Worker-owned parent directory creation;
- Worker startup canonical-file cleanup;
- owned temp sibling pattern;
- atomic write/flush/close/replace;
- independently launched Worker/Streamlit;
- no bidirectional IPC;
- source authority semantics;
- Historical/Replay envelope semantics.

Production behavior must remain compatible with the accepted WP05 authority.

---

# Fixed Lifecycle State

At entry:

- #230 remains Closed / Done;
- #231 remains Open / Backlog;
- #232 and later remain untouched;
- milestone #58 remains open.

Do not reopen #230 by default.

Do not change #231 lifecycle.

Do not start WP06/WP07.

---

# Phase 0 — Fresh Evidence

Before mutation:

1. Read #230 and the accepted WP05 implementation authority.
2. Read current WP05 handoff implementation.
3. Read all Infrastructure tests that:
   - exercise Worker startup;
   - exercise atomic handoff;
   - manipulate `Visualization:HandoffPath`;
   - launch Worker/apphost/in-process Worker composition;
   - clean runtime files.
4. Record Git state:
   - branch;
   - HEAD;
   - origin/main;
   - ahead/behind;
   - staged/tracked/relevant untracked.
5. Confirm no WP06 implementation exists.
6. Reproduce the nondeterminism with at least one full-suite run if practical.

Do not mutate until interference candidates are mapped.

---

# Phase 1 — Test Resource Map

For every relevant Infrastructure test/fixture, record:

- handoff path used;
- whether path is default or overridden;
- whether directory/file is shared across tests;
- temp sibling behavior;
- process/Worker lifetime;
- disposal semantics;
- environment/configuration mutation;
- static/shared state;
- parallelization eligibility;
- cleanup behavior.

Identify any resource that is shared unintentionally.

---

# Phase 2 — Process / File Handle Diagnosis

Investigate:

- lingering Worker processes;
- undisposed file streams;
- open temp files;
- canonical file handles;
- process exit ordering;
- file cleanup before child/process termination;
- test host child-process lifetime.

Use standard .NET/process/file diagnostics available in the repository/toolchain.

Do not add packages.

---

# Phase 3 — Parallelism Diagnosis

Determine whether failing tests can overlap in a way that violates test isolation.

Check:

- xUnit collection behavior;
- assembly-level parallelization configuration;
- shared fixtures;
- test collections;
- static configuration;
- same handoff path across parallel tests.

Do not globally disable parallelism merely because it makes failures disappear.

If parallel execution exposes a real shared-resource bug in test design, prefer isolating the resource or narrowly grouping only the conflicting tests.

---

# Phase 4 — Configuration Isolation Diagnosis

Check whether tests mutate:

- `Visualization:HandoffPath`;
- environment variables;
- current directory;
- LocalApplicationData assumptions;
- Worker configuration.

Prove whether any mutation leaks across tests.

Environment-variable tests must restore prior state deterministically.

Avoid process-global mutable configuration where a test-local configuration source can be used instead.

---

# Phase 5 — Path Isolation Candidate

If tests currently share the canonical WP05 runtime path, evaluate a **test-only unique absolute handoff path per test/fixture** using the already-accepted `Visualization:HandoffPath` override.

Preferred properties:

- unique temp directory per test;
- absolute path;
- deterministic ownership;
- cleanup after Worker/process disposal;
- no production semantic change.

This is preferred over altering production path semantics.

Do not modify production defaults just for tests.

---

# Phase 6 — Temp-File Ownership / Cleanup

Verify the implementation/test behavior around:

`.visualization-read-model.json.<owned-random-suffix>.tmp`

Check whether:

- temp files remain open during cleanup;
- one test can delete another test's temp file;
- startup cleanup can race another active Worker;
- broad wildcard cleanup crosses test ownership.

If test isolation makes each handoff directory unique, cleanup should remain local.

If production cleanup is overly broad and that is a concrete defect, a narrow production fix may be authorized only with proof.

---

# Phase 7 — Worker Disposal

Prove whether all tests wait for Worker/process shutdown before:

- deleting handoff directories;
- starting another Worker using the same resource;
- ending the test fixture.

If not, fix the test/fixture lifecycle narrowly.

Potential allowed fixes:

- `await using` / deterministic disposal;
- explicit cancellation then awaited completion;
- process exit wait;
- stream disposal before directory cleanup;
- finally-block cleanup.

Do not add arbitrary sleeps.

---

# Phase 8 — Mutation Gate

Default preference:

**test-only isolation fix**

Production mutation is allowed only if diagnostics prove a concrete WP05 production defect that affects correct resource ownership/cleanup outside tests too.

Any mutation must be:

- narrow;
- directly proven;
- within WP05 handoff ownership;
- behavior-preserving relative to accepted contract.

Forbidden:

- weakening assertions;
- skipping tests;
- marking tests non-parallel without justification if unique-resource isolation is viable;
- global test-runner serialization;
- broad timeouts;
- retrying failed tests until green;
- production path semantic changes for test convenience.

---

# Phase 9 — Focused Reproduction Tests

Before full regression, run the previously failing tests:

- individually;
- together;
- repeatedly as a focused group where tooling supports repeat execution.

Prove they no longer interfere.

If one fails deterministically after isolation, treat it as a real defect and investigate.

Do not use pass-in-isolation as sufficient acceptance.

---

# Phase 10 — Infrastructure Determinism Gate

Run the complete governed Infrastructure suite **multiple clean times**.

Minimum required:

**3 consecutive complete passing Infrastructure runs**

Each run must produce:

- exit code 0;
- exact passed/failed/skipped/total;
- no locked-file errors;
- no Worker non-zero exit caused by cross-test interference.

Expected test count before any added stabilization tests:

use the fresh repository count; do not assume 159 or another value without proof.

If stabilization adds tests, explain count changes.

---

# Phase 11 — Full Regression Determinism Gate

After Infrastructure is stable, run the full solution suite:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Minimum required:

**3 consecutive clean full-suite passes**

Target predecessor behavior:

- 0 failed;
- 0 skipped unless already governed;
- total should match the accepted current test inventory.

The historical WP06 predecessor baseline was **305/305**.

If the total changes only because authorized stabilization tests were added, record and explain the new baseline.

Do not accept 2/3 passes.

---

# Phase 12 — Build and Python Guards

Run:

- repository-standard build;
- WP05 Python consumer tests/compile checks affected by shared handoff configuration.

Require:

- build 0 errors;
- report warnings exactly;
- Python checks pass.

No Python production changes are expected.

---

# Phase 13 — Scope Audit

Classify every changed file as:

- WP05 test isolation;
- WP05 Worker test-fixture lifecycle;
- narrowly proven WP05 production cleanup/disposal fix, if required;
- focused stabilization test.

Prove:

- no WP06 implementation;
- no WP07;
- no schema/persistence change;
- no Replay semantic change;
- no Streamlit/UI behavior change;
- no package/pin change;
- no transport redesign;
- no unrelated global test infrastructure change.

Anything unexplained blocks completion.

---

# Phase 14 — Acceptance Matrix

Require PASS for:

- root cause identified;
- shared resource map complete;
- file-handle/process lifecycle understood;
- path/config isolation proven;
- no test leakage;
- focused conflict group stable;
- Infrastructure pass #1;
- Infrastructure pass #2;
- Infrastructure pass #3;
- full regression pass #1;
- full regression pass #2;
- full regression pass #3;
- build;
- WP05 Python checks;
- scope audit.

Any FAIL => BLOCKED.

---

# GitHub Lifecycle

Under this authority:

- #230 remains Closed / Done;
- do not reopen #230;
- #231 remains Open / Backlog;
- do not mutate #231;
- #232+ untouched.

A concise evidence comment on #230 is allowed only if established repository convention requires documenting a predecessor stabilization amendment.

Otherwise GitHub mutations are zero.

---

# Expected Success State

After success:

- WP05 handoff/test execution is deterministic;
- no shared temp-file/file-handle interference remains;
- 3 consecutive Infrastructure runs pass;
- 3 consecutive full regression runs pass;
- final full-suite count becomes the restored immediate predecessor baseline for WP06;
- build remains clean;
- #230 Closed / Done;
- #231 Open / Backlog;
- WP06 may be retried under its existing fresh authority or a regenerated authority using the restored baseline;
- WP07 remains unstarted.

---

# Stop Conditions

Stop if:

- root cause requires redesign of WP05 transport semantics;
- fixing requires global test-runner serialization without narrow justification;
- schema/persistence/Replay changes become necessary;
- tests must be weakened/skipped;
- nondeterminism persists after isolated resource ownership;
- 3 consecutive Infrastructure passes cannot be achieved;
- 3 consecutive full-suite passes cannot be achieved;
- build/Python guards fail;
- scope audit fails.

On blocker:

- preserve valid predecessor implementation;
- report exact interference still observed;
- report exact last deterministic evidence;
- keep #230 closed and #231 open.

---

# Required Completion Report

## Root cause

State exact cause(s) of nondeterminism with evidence.

## Resource isolation

Report:

- handoff path strategy;
- temp-file ownership;
- Worker/process disposal;
- environment/config restoration;
- parallelism treatment.

## Mutations

List every changed file and whether test-only or production.

If no production mutation:

`WP05 DETERMINISTIC-REGRESSION PRODUCTION MUTATIONS: ZERO`

## Focused evidence

Report previously conflicting tests individually/together/repeatedly.

## Infrastructure determinism

Run 1:
- command;
- counts;
- exit.

Run 2:
- command;
- counts;
- exit.

Run 3:
- command;
- counts;
- exit.

## Full regression determinism

Run 1:
- counts;
- exit.

Run 2:
- counts;
- exit.

Run 3:
- counts;
- exit.

## Build / Python

Report exact results.

## Scope audit

Prove no WP06/WP07 or architecture drift.

## Lifecycle

State:

- #230 remains Closed / Done;
- #231 remains Open / Backlog.

## Next step

On success:

`WP06 PREDECESSOR BASELINE RESTORED — WP06 MAY BE RETRIED`

---

# Terminal Markers

On success:

`RELEASE 1.9 WP05 WORKER HANDOFF TEST-ISOLATION AND DETERMINISTIC-REGRESSION COMPLETE`

On blocker:

`RELEASE 1.9 WP05 WORKER HANDOFF TEST-ISOLATION AND DETERMINISTIC-REGRESSION BLOCKED`

Do not emit success unless the root cause is controlled and both Infrastructure and full regression pass three consecutive clean runs.
