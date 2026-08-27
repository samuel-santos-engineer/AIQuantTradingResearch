# Release 1.9 — WP04 Infrastructure-Test Completion / Evidence — Codex Authority

## Authority

This is a **narrow validation/evidence authority** for the sole remaining acceptance blocker in the WP04 Historical presentation-producer amendment.

Preserve the current repository state as a **valid partial implementation**.

Known current state:

- #229 remains **Closed / Done**.
- #230 remains **Open / Backlog**.
- WP06 and later work remain unstarted.
- Historical presentation projections and canonical `SimpleReturnFeatureComputer` wiring are implemented.
- Historical `PipelineExecution` invokes the WP04 producer through `PipelineExecutionResult.HistoricalPresentationInputs`.
- Successful canonical Historical results map to truthful Ready, WarmUp, or genuine Empty.
- Canonical failures map to Failed through existing safe failure categories.
- No SQLite reconstruction, provider refetch, feature recomputation, Replay redesign, WP05, or WP06 work exists.
- Build passed with **0 errors / 0 warnings**.
- Application tests passed **122/122**.
- The sole known blocker is that the governed Infrastructure test command started but did not produce a definitive final result.

Do **not** clean, revert, or reimplement the valid partial state.

This authority exists to diagnose the Infrastructure test execution, obtain a definitive complete-suite result, finish remaining regression evidence, and determine whether the predecessor amendment is accepted.

## Model

Use **GPT-5.6 Sol** for this pass.

The task is diagnostic/evidence-heavy. Prefer test-runner/process diagnosis over production mutation.

---

## Fixed Scope

### Permitted

- inspect the current partial implementation;
- identify the exact governed Infrastructure test command;
- run the complete Infrastructure suite;
- use standard .NET diagnostic/test-runner options;
- list/filter tests **for diagnosis only**;
- inspect process/test-host behavior;
- capture diagnostic logs;
- make a narrowly proven WP04-owned fix only if a concrete repository defect is identified;
- rerun focused/predecessor/full suites;
- perform final diff/scope audit.

### Forbidden

Do not:

- revert valid WP04 work;
- redesign Historical projections or feature semantics;
- duplicate `SimpleReturnFeatureComputer`;
- change the five-stage pipeline architecture;
- change Replay semantics;
- change schema v4 or persistence;
- add SQLite/provider presentation reconstruction;
- implement WP05 or WP06;
- add packages;
- skip, ignore, delete, weaken, or quarantine tests to get green;
- substitute a filtered subset for the complete Infrastructure acceptance suite;
- broadly refactor test infrastructure;
- reopen #229;
- mutate #230 lifecycle.

---

# Phase 0 — Fresh State Proof

Before mutation:

1. Read #229 and #230.
2. Read the current tracked diff and preceding WP04 amendment work.
3. Record:
   - branch;
   - HEAD;
   - origin/main;
   - ahead/behind;
   - staged paths;
   - tracked changes;
   - relevant untracked authority/control files.
4. Prove the current changes are the valid partial WP04 implementation.
5. Prove no WP05/WP06 implementation exists.
6. Confirm #229 Closed / Done and #230 Open / Backlog.
7. Reconfirm build and Application evidence if practical.

If the partial implementation is missing or corrupted, stop for reconciliation. Do not revert it.

---

# Phase 1 — Identify the Governed Infrastructure Suite

Determine the exact repository-standard command for the **complete Infrastructure test project/suite**, preferably the same command used in the prior blocked run.

Record:

- exact command;
- project path;
- target framework/configuration;
- test runner;
- material environment variables;
- restore/no-restore behavior.

Do not silently replace it with a different suite.

---

# Phase 2 — Controlled Diagnostic Run

Run the complete governed Infrastructure suite and capture enough evidence to determine whether the problem is:

A. output/result capture only;

B. one or more hanging tests;

C. test-host/child-process hang;

D. external environment/policy/resource blocker;

E. normal test failure.

Permitted standard diagnostics include:

- normal/detailed console logger;
- .NET test diagnostic logging;
- supported blame/hang diagnostics;
- outer process timeout for diagnosis;
- process-tree inspection;
- test discovery/listing;
- filtered class/test runs for isolation only.

Do not add dependencies.

---

# Phase 3 — Isolate a Stall if Necessary

If the full suite does not complete:

1. list discovered Infrastructure tests;
2. run logical groups/classes;
3. bisect/filter to locate the non-completing test or host boundary;
4. rerun the suspect scope with diagnostics;
5. identify the exact blocking test/process/resource.

Filtered runs are **diagnostic only**.

Final acceptance still requires the complete governed Infrastructure suite.

Do not rely on arbitrary sleeps.

---

# Phase 4 — Mutation Gate

Default:

`INFRASTRUCTURE EVIDENCE MUTATIONS: ZERO`

A mutation is allowed only when all are true:

1. diagnostics prove a concrete repository-owned defect;
2. it is introduced/exposed by the current WP04 amendment or its focused test support;
3. the fix is narrow and within WP04 acceptance scope;
4. the fix does not weaken acceptance;
5. no new semantics are required.

Potentially valid examples:

- deterministic disposal of a newly introduced test resource;
- correction of missing cancellation/disposal in WP04 test support;
- replacing an accidental infinite wait with deterministic synchronization;
- a narrow production defect directly proven by the Infrastructure test.

Forbidden fixes include:

- merely increasing timeout;
- skipping/ignoring tests;
- removing assertions;
- global unrelated test-runner changes;
- broad concurrency redesign.

If broader authority is required, stop.

---

# Phase 5 — Definitive Infrastructure Run

After diagnosis and any narrowly authorized fix, run the **complete governed Infrastructure suite** to completion.

Capture:

- exact command;
- exit code;
- passed;
- failed;
- skipped;
- total;
- warnings/errors;
- duration if available.

Acceptance requires:

- exit code 0;
- 0 failed;
- no unexplained skipped tests;
- interpretable final counts.

A run that only starts or ceases output without a final result is not acceptance evidence.

---

# Phase 6 — WP04 Focused Evidence

Prove the current implementation satisfies:

- actual Historical producer consumes `HistoricalPresentationInputs`;
- Ready is truthful;
- WarmUp is truthful;
- zero-observation mapping follows accepted WP04 semantics;
- Empty is genuine only;
- Failed uses canonical safe failure evidence;
- invalid-numeric behavior remains canonical;
- Stale semantics are preserved;
- bounded 64-row window semantics are preserved;
- Historical revision semantics are preserved;
- snapshot identity/version propagate;
- pipeline evidence propagates;
- production pipeline-result → producer composition is exercised.

If these tests live outside Infrastructure, run their governed project too.

---

# Phase 7 — No-Reconstruction Proof

By diff/search/test evidence prove:

- no SQLite presentation readback;
- no provider refetch;
- no duplicate simple-return formula;
- `SimpleReturnFeatureComputer` remains the sole formula implementation;
- no second pipeline;
- no WP05/Streamlit/transport work;
- no WP06 work.

This is a hard gate.

---

# Phase 8 — Predecessor Suites

Run the governed predecessor-sensitive evidence needed for:

## WP02 / Replay

- replay identity;
- logical ticks;
- restart/resume;
- duplicate determinism;
- cancellation;
- bounds;
- finite completion.

## WP03

- Historical/Replay dispatch;
- schema v4;
- source authorities 0/1;
- Replay persistence;
- canonical five-stage pipeline.

## WP04

- Model C;
- states;
- revision behavior;
- boundedness;
- atomic/concurrency behavior.

Also run Application, Domain, and Architecture suites to definitive completion.

Expected current Application evidence is **122/122**; capture fresh counts.

---

# Phase 9 — Build

Run the established repository build.

Require:

- exit code 0;
- 0 errors;
- report warnings exactly.

Expected current evidence: **0 errors / 0 warnings**.

---

# Phase 10 — Full Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Historical pre-amendment baseline:

**297/297 passed**

The current partial implementation may have increased the count.

Acceptance requires:

- definitive completion;
- exit code 0;
- 0 failed;
- exact passed/failed/skipped/total;
- every count increase explained by focused tests;
- no unexplained disappearance of predecessor tests.

The final passing total becomes the immediate WP05 predecessor baseline.

---

# Phase 11 — Final Diff / Scope Audit

Classify every tracked change as:

- pre-existing Historical feature/projection implementation;
- pre-existing Historical producer integration;
- focused WP04 producer/acceptance test;
- narrowly proven WP04-owned execution defect fix, if required.

Prove:

- no WP05;
- no Streamlit;
- no atomic-file handoff;
- no refresh/retry implementation;
- no WP06;
- no schema/persistence change;
- no SQLite/provider reconstruction;
- no duplicate feature computation;
- no Replay semantic change;
- no package/foundation change;
- no unrelated refactor.

Anything unexplained blocks acceptance.

---

# Phase 12 — Acceptance Matrix

Report PASS/FAIL for:

- valid partial implementation preserved;
- definitive Infrastructure result;
- producer integration;
- Ready;
- WarmUp;
- zero-observation mapping;
- Empty;
- Failed;
- invalid numeric;
- Stale;
- bounded window;
- Historical revision;
- snapshot identity/version;
- pipeline evidence;
- no reconstruction/recomputation;
- Replay unchanged;
- WP03/schema compatibility;
- Application;
- Domain;
- Architecture;
- build;
- full regression;
- final scope audit.

Any FAIL means BLOCKED.

---

# GitHub Lifecycle

Under this authority:

- #229 remains **Closed / Done**;
- do not reopen #229;
- preserve its Project fields and milestone;
- #230 remains **Open / Backlog**;
- do not mutate #230;
- do not mutate #231 or later.

A closed-issue evidence comment on #229 is allowed only if established repository convention explicitly requires amendment evidence after all technical gates pass. Otherwise GitHub mutations are zero.

Canonical milestone counts remain **8 open / 4 closed**.

---

# Stop Conditions

Stop if:

- the valid partial implementation is corrupted/missing;
- complete Infrastructure evidence cannot be obtained;
- the blocker requires broad test-infrastructure or production redesign;
- tests must be weakened/skipped;
- schema/persistence/Replay/WP05/WP06 changes become necessary;
- any predecessor suite fails;
- build fails;
- full regression fails;
- final diff contains unexplained scope.

On stop:

- preserve the valid partial implementation;
- preserve useful diagnostic evidence where repository policy permits;
- report the exact test/process/environment blocker;
- keep #229 Closed / Done;
- keep #230 Open / Backlog.

---

# Required Completion Report

Return:

## Initial state
- Git/repository state;
- valid partial implementation proof;
- #229/#230 lifecycle.

## Infrastructure diagnosis
- exact governed command;
- diagnostic method;
- classification A/B/C/D/E;
- exact blocking test/process/resource, if any.

## Mutations
State either:

`INFRASTRUCTURE EVIDENCE MUTATIONS: ZERO`

or list every narrowly authorized mutation and its proof.

## Definitive Infrastructure result
- command;
- exit code;
- passed;
- failed;
- skipped;
- total;
- warnings;
- duration if available.

## WP04 acceptance evidence
PASS/FAIL for producer integration and all required states/revision/boundedness/production-composition gates.

## Predecessor evidence
- WP02/Replay;
- WP03/schema;
- Application;
- Domain;
- Architecture.

## Build
- command;
- errors;
- warnings.

## Full regression
- command;
- exit code;
- passed;
- failed;
- skipped;
- total;
- comparison with historical 297/297 baseline.

## Scope audit
- diff classification;
- no WP05;
- no WP06;
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

`RELEASE 1.9 WP04 INFRASTRUCTURE-TEST COMPLETION AND EVIDENCE COMPLETE`

On blocker:

`RELEASE 1.9 WP04 INFRASTRUCTURE-TEST COMPLETION AND EVIDENCE BLOCKED`

Do not emit success unless the complete governed Infrastructure suite and full regression both produce definitive passing results.
