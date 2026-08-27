# Release 1.9 — WP03 Production-Flow Evidence Substitution — Codex Authority

## Authority

This document grants a **narrow evidence-substitution authority** for Release 1.9 WP03, canonical GitHub issue **#228**.

WP03 implementation is complete enough to satisfy all code-level, unit/integration, and regression gates, but the final production-flow gate remains unproven because the current environment blocks the real Worker process launch.

Proven current state:

- fixed Worker configuration types and mode parsing implemented;
- Historical default behavior implemented;
- Replay configuration validation implemented;
- Dataset target equality implemented;
- `[From,To)` replay timestamp-bound validation implemented;
- Replay dispatch implemented through the WP02 replay source and WP03 observation seam;
- existing canonical executor/seam preserved;
- build: **0 errors / 0 warnings**;
- full regression: **288/288 passed**;
- Application suite: **122/122 passed**;
- WP02 replay suite: **142/142 passed**;
- no GitHub lifecycle mutation occurred;
- #228 remains Open / Backlog;
- WP04 has not started.

Exact remaining blocker:

- apphost launch is blocked by Application Control;
- managed DLL launches exit with code 1 without usable runtime output;
- therefore the authority-required **real Worker Replay process composition proof** could not be demonstrated in this environment.

This authority does **not** authorize implementation changes.

This authority does **not** authorize weakening #228.

This authority exists only to determine whether equivalent, repository-controlled production-composition evidence can substitute for the unavailable external process-launch proof.

---

# Objective

Determine whether WP03's production-flow requirement can be satisfied through **equivalent in-process evidence** when process launch is externally blocked by the execution environment.

The substitute evidence must prove the same material facts that a successful Worker Replay process launch was intended to prove:

1. Worker configuration binds exactly as defined.
2. `Worker:Mode=Replay` selects the Replay production branch.
3. Replay configuration maps exactly to WP02 request/configuration.
4. Dataset configuration is validated exactly as defined.
5. The real production DI/composition root resolves the real WP02 replay source.
6. Replay observations pass through the real WP03 observation-input seam.
7. The seam invokes the shared `ExecuteCanonical`.
8. All five canonical stages execute.
9. Replay does not use `IHistoricalObservationStore`.
10. Finite completion, cancellation, restart/resume, duplicate determinism, logical ticks, and bounds remain governed as already proven.
11. Historical mode still selects the historical path.
12. The code path exercised is the **actual production composition path**, not a test-only substitute architecture.

If all of these can be proven with equivalent repository-controlled evidence, WP03 may proceed to lifecycle finalization.

If not, remain blocked.

---

# Core Principle

> Evidence may substitute for an unavailable launch mechanism only if it proves the same production composition semantics through the actual production code path.

This authority does not permit lowering the acceptance bar from "production flow proven" to merely "tests pass."

The issue is **how** the production path is invoked, not whether the production path itself exists.

---

# Fixed Accepted Implementation State

Do not change the implementation under this authority.

Treat the following as preserved:

- canonical `ExecuteCanonical` executor;
- additive observation seam;
- Worker mode/configuration implementation;
- Dataset-boundary validation;
- real Replay dispatch;
- WP02 replay source integration;
- existing tests;
- package/Python/schema/protocol/planning state.

Any implementation mutation is outside this authority unless a later separate authority is issued.

---

# Permitted Activities

This authority may:

- inspect current Worker entry point;
- inspect composition root;
- inspect DI registrations;
- inspect Worker configuration binding/validation;
- inspect Replay dispatch;
- inspect WP02 replay source resolution;
- inspect WP03 seam and `ExecuteCanonical`;
- inspect existing tests;
- add **evidence-only test coverage** if and only if it exercises existing production code without changing production implementation;
- run in-process tests that instantiate the actual production service graph;
- run DI validation;
- run configuration binding/validation tests;
- invoke production Worker execution classes/methods directly in-process where repository structure permits;
- use reflection only if repository conventions require it and no public/internal invocation path exists, but do not alter production visibility solely for testing;
- prove the external launch failure is environment-specific;
- perform read-only GitHub verification;
- finalize #228 only after equivalence is proven.

---

# Explicitly Forbidden

Do not:

- change production Worker code;
- change production Application/Infrastructure code;
- change configuration semantics;
- change Dataset semantics;
- change WP02 contracts;
- add a separate test-only replay pipeline;
- replace production DI with hand-constructed fake wiring and call that equivalent;
- mock away the production branch being accepted;
- change package pins;
- change Python;
- change Streamlit;
- change schema;
- change JSON-over-stdio protocol;
- modify planning/dependencies;
- start WP04;
- close #228 merely because 288/288 passes.

---

# Phase 0 — Prove External Launch Blocker

Before relying on substitute evidence:

1. Reproduce or inspect the apphost launch failure.
2. Record the Application Control blocking evidence.
3. Reproduce or inspect managed DLL launch exit code 1.
4. Confirm absence of useful runtime output is environmental/tooling-related rather than an application exception that can be diagnosed from repository code.
5. Confirm build and tests remain green.

The required conclusion must be:

> The inability to obtain process-level proof is caused by the execution environment's launch restrictions, not by a known repository/runtime defect.

If that cannot be established, stop.

---

# Phase 1 — Identify the Actual Production Composition Path

Document the production flow from Worker entry/configuration to pipeline.

At minimum identify actual production types/methods for:

- Worker entry/bootstrap;
- configuration binding;
- mode selection;
- DI registration;
- replay source registration/resolution;
- Worker execution dispatcher;
- replay request construction;
- DatasetDefinition construction/validation;
- WP03 explicit-observation materialization seam;
- `ExecuteCanonical`;
- final execution result/termination.

Produce a concrete call graph.

No substitute test is acceptable unless it traverses this actual graph.

---

# Phase 2 — DI and Configuration Equivalence Proof

Using the actual production service-registration code:

1. build the production service collection/provider in-process;
2. apply the same configuration keys/values production would receive;
3. validate options/configuration;
4. prove Replay mode resolves the real WP02 replay source implementation;
5. prove Historical mode resolves/uses the historical path;
6. prove no replay path resolves `IHistoricalObservationStore` for replay acquisition;
7. prove Dataset validation executes before replay pipeline entry.

If production DI cannot be built in-process without changing production code, stop and report why.

---

# Phase 3 — In-Process Replay Production-Path Execution

Invoke the real Worker Replay execution path in-process using the production types and service graph.

The invocation must:

- use `Worker:Mode=Replay`;
- use valid fixed replay config;
- use valid Dataset config;
- resolve the actual WP02 source;
- produce/select replay observations;
- validate Dataset target and `[From,To)` bounds;
- call the real WP03 observation seam;
- reach `ExecuteCanonical`;
- execute all five canonical stages;
- return/complete under the real finite Replay behavior.

Do not invoke lower-level components directly if doing so skips Worker dispatch/composition.

The substitute must begin at the closest in-process equivalent of the real Worker execution boundary.

---

# Phase 4 — Historical Production-Path Guard

Using the same production service graph/configuration mechanism, prove:

- missing `Worker:Mode` defaults to Historical;
- explicit Historical selects the historical path;
- `IHistoricalObservationStore` is used for historical acquisition;
- historical processing reaches the same `ExecuteCanonical`;
- Replay-only settings do not alter Historical dispatch.

---

# Phase 5 — Required Evidence Matrix

For each intended process-level acceptance fact, provide substitute evidence.

| Production fact | Required substitute evidence |
|---|---|
| Worker binds fixed config | production configuration binder/options validation |
| Replay mode selected | real Worker dispatcher branch observed |
| WP02 source used | production DI resolution + actual invocation |
| Dataset target validated | real validation path exercised |
| Dataset interval validated | real validation path exercised |
| Replay observations enter WP03 seam | real seam invocation observed |
| `ExecuteCanonical` reached | actual canonical executor invocation observed |
| stages 1–5 execute | behavioral/interaction evidence through production path |
| no historical store for replay | DI/call-path proof |
| finite completion | real replay execution completion evidence |
| Historical remains intact | real Historical production-path execution evidence |

Every row must PASS.

---

# Phase 6 — Evidence-Only Tests

If existing tests do not expose enough of the production path, you may add narrowly scoped **test-only** evidence coverage.

Rules:

- no production code changes;
- tests must exercise production registrations/types;
- no alternate composition root;
- no fake replay pipeline;
- mocks may isolate external resources but must not replace the production branch being accepted;
- test names should clearly indicate production-composition evidence;
- added tests become part of the full regression suite.

If proving the path requires a production-code seam solely for testing, stop. That would require separate implementation authority.

---

# Phase 7 — Regression Re-Proof

After any evidence-only test additions:

Run:

- Application focused suite;
- WP02 replay suite;
- Worker/configuration/runtime focused suite;
- full build;
- full regression:
  `dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate baseline before evidence substitution:

- Application: 122/122
- WP02 replay: 142/142
- full suite: 288/288
- build: 0 errors / 0 warnings

Higher counts are acceptable when explained by evidence-only tests.

Any regression failure is a blocker.

---

# Phase 8 — Equivalence Decision Gate

Conclude exactly one:

## Outcome A — Equivalent production-flow evidence proven

Use only if all required production facts are proven through the actual production composition path in-process and the only missing evidence is the externally blocked process launch mechanism.

Then #228 may proceed to lifecycle finalization.

## Outcome B — Equivalent evidence not proven

Use if any production fact cannot be demonstrated without:

- changing production code;
- bypassing Worker dispatch;
- using a test-only composition;
- weakening acceptance semantics.

Then remain blocked.

Do not use subjective confidence.

---

# Phase 9 — GitHub Lifecycle Finalization

Only for Outcome A:

1. read #228 current state;
2. confirm established completion convention;
3. add one concise evidence comment that explicitly states:
   - external process launch was blocked by Application Control;
   - equivalent production composition was proven in-process through actual production DI/configuration/dispatch code;
   - build/regression results;
4. transition #228 Project Status to the authoritative completed state;
5. preserve Priority = P1, Release = 1.9, authoritative Area;
6. close #228;
7. keep milestone #58 open;
8. immediately read back all mutations.

Do not modify #229.

---

# Expected Success State

If Outcome A succeeds:

- #226 Closed / Done;
- #227 Closed / Done;
- #228 Closed / Done or authoritative completed state;
- #229–#237 remain Open and untouched;
- milestone #58 remains Open;
- canonical milestone counts:
  - 9 open
  - 3 closed;
- raw closed count may additionally include historical duplicate #225;
- dependency chain remains 11/11;
- WP04 #229 becomes next eligible;
- WP04 remains unstarted.

---

# Stop Conditions

Stop immediately if:

- launch failure appears to be an application defect rather than environment restriction;
- production service graph cannot be built in-process without code changes;
- Worker dispatch cannot be exercised in-process;
- substitute path skips real production composition;
- Replay source is mocked instead of actually resolved when resolution is the acceptance fact;
- production code changes are required;
- any required production fact remains unproven;
- regression fails;
- GitHub lifecycle mutation cannot be proven.

On stop:

- preserve implementation;
- do not broaden authority;
- leave #228 open;
- report the exact missing production-flow fact.

---

# Success Criteria

This authority succeeds only if:

- external launch restriction is proven environmental;
- actual production configuration binding is exercised;
- actual production DI composition is exercised;
- actual Worker Replay dispatch is exercised;
- actual WP02 replay source is resolved and invoked;
- actual Dataset validation is exercised;
- actual WP03 seam is exercised;
- actual `ExecuteCanonical` is exercised;
- all five canonical stages execute;
- Replay does not use historical storage;
- Historical production path remains intact;
- finite completion is proven;
- all relevant tests/build/regression pass;
- no production implementation changes occur;
- #228 is finalized only after evidence equivalence is established;
- WP04 remains unstarted.

---

# Required Completion Report

Return:

## Launch blocker proof
- apphost/Application Control evidence;
- managed DLL launch evidence;
- conclusion that blocker is environmental.

## Production call graph
List actual production types/methods from Worker configuration through `ExecuteCanonical`.

## Substitute evidence
For every production fact in the evidence matrix:
- evidence source;
- observed result;
- PASS/FAIL.

## Test/regression proof
Report:
- evidence-only tests added, if any;
- Application suite count;
- WP02 replay suite count;
- Worker/runtime suite count;
- build errors/warnings;
- full regression exact counts.

## Equivalence decision
State exactly one:

`OUTCOME A — EQUIVALENT PRODUCTION-FLOW EVIDENCE PROVEN`

or

`OUTCOME B — EQUIVALENT PRODUCTION-FLOW EVIDENCE NOT PROVEN`

## Mutation proof
State whether production code changed.

Expected:

`WP03 PRODUCTION IMPLEMENTATION MUTATIONS: ZERO`

Test-only evidence additions, if any, must be listed separately.

## GitHub lifecycle
If Outcome A:
- #228 before/after;
- Project Status before/after;
- completion comment;
- milestone canonical counts;
- #229–#237 untouched.

## Next eligibility
If Outcome A:

`NEXT ELIGIBLE WORK PACKAGE: WP04 — #229`

Do not authorize or execute WP04.

---

# Terminal Markers

On Outcome A success:

`RELEASE 1.9 WP03 PRODUCTION-FLOW EVIDENCE SUBSTITUTION COMPLETE`

On Outcome B/blocker:

`RELEASE 1.9 WP03 PRODUCTION-FLOW EVIDENCE SUBSTITUTION BLOCKED`

Do not emit COMPLETE unless evidence equivalence and lifecycle finalization are both proven.
