# Codex Execution Prompt — Release 0.9 / WP08 Worker Research Execution

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 08 — Worker Research Execution |
| Type | Implementation |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Prerequisite | WP07 — Dependency Registration = `REGISTRATION COMPLETE` |
| Primary Authorities | Release 0.9 execution plan/file manifest, WP02 research model, WP04 contracts, WP05 use case, WP06 adapter, WP07 registrations |
| Execution Mode | Narrowly scoped Worker composition and reference execution |
| Expected Outcome | Make Worker execute the canonical Release 0.9 research flow through DI using `IResearchUseCase`, surface the deterministic result/failure appropriately, and remain free of Domain, Infrastructure, and research-orchestration logic |

---

# 1. Purpose

Execute:

```text
Phase 2 — Release 0.9
WP08 — Worker Research Execution
```

WP08 completes the first executable vertical slice of Release 0.9.

Worker must:

```text
compose Application + Infrastructure
        ↓
resolve IResearchUseCase
        ↓
construct canonical ResearchRequest
        ↓
invoke use case
        ↓
surface success or expected failure
```

Worker must remain thin.

It must not calculate the mean.

It must not construct fixture data.

It must not know the concrete Infrastructure adapter.

It must not duplicate Application validation/orchestration.

Do not begin WP09.

---

# 2. Authoritative Sources

Read completely before mutation:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/08-worker-research-execution-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Inspect current Worker composition/runtime code:

```text
src/AIQuantTradingResearch.Worker/**
```

Inspect current module registration boundaries:

```text
src/AIQuantTradingResearch.Application/DependencyInjection.cs
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
```

Inspect current Application contracts/use case:

```text
src/AIQuantTradingResearch.Application/Research/ResearchRequest.cs
src/AIQuantTradingResearch.Application/Research/ResearchOutcome.cs
src/AIQuantTradingResearch.Application/Research/ResearchResult.cs
src/AIQuantTradingResearch.Application/Research/ResearchFailure.cs
src/AIQuantTradingResearch.Application/Research/IResearchUseCase.cs
```

Inspect existing hosting/configuration guidance:

```text
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/LOGGING_STRATEGY.md
docs/architecture/implementation/OBSERVABILITY_MODEL.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/ERROR_HANDLING.md
```

Do not redesign prior work packages.

---

# 3. Accepted WP07 Baseline

Expected current state:

```text
IResearchUseCase -> ResearchUseCase
registered in Application

IObservationSource -> DeterministicObservationSource
registered in Infrastructure

IResearchUseCase resolves
IObservationSource resolves

Worker references Application + Infrastructure
Worker does not yet execute research
```

WP08 should use these existing boundaries exactly.

---

# 4. Canonical Reference Request

Worker must construct the approved Release 0.9 reference request:

```text
Target = SAMPLE-USD
RequestedObservationCount = 3
```

Do not introduce:

```text
CLI arguments
configuration-driven target selection
environment variables
provider selection
interactive prompts
time ranges
strategy parameters
```

unless existing authority explicitly requires them.

The canonical request is intentionally fixed and deterministic for Release 0.9.

---

# 5. Expected Successful Result

When the approved modules are composed, Worker should receive:

```text
Success
Target = SAMPLE-USD
ObservationCount = 3
MeanPrice = 110.00
```

Worker may surface this using the smallest existing console/logging mechanism consistent with repository guidance.

Do not add a presentation framework.

Do not add structured telemetry infrastructure beyond existing repository conventions.

---

# 6. Expected Failure Handling

Worker must handle the existing `ResearchOutcome` contract.

Expected use-case failures are:

```text
InvalidRequest
UnsupportedTarget
InsufficientObservations
```

For the canonical request, none should occur.

If an expected failure occurs, Worker should surface it clearly and terminate/complete according to the existing host model.

Do not duplicate validation logic.

Do not catch and reinterpret every exception as a research failure.

Unexpected exceptions remain unexpected.

---

# 7. Composition Root Discipline

Worker owns:

```text
host/bootstrap
module registration invocation
service provider composition
canonical request construction
use-case invocation
result/failure surfacing
process lifecycle
```

Worker does not own:

```text
Domain invariants
observation fixture
mean arithmetic
request validation
source failure mapping
Application orchestration
Infrastructure implementation
```

---

# 8. Host Execution Model

Inspect the Worker implementation established in Release 0.8.

Use the smallest change consistent with that host design.

Do not introduce `BackgroundService`, scheduler, long-running loop, hosted queue consumer, or periodic timer unless the existing Worker architecture already requires it.

For Release 0.9, a single deterministic execution is sufficient.

The process may:

```text
build host/provider
resolve IResearchUseCase
execute once
surface result
exit cleanly
```

if consistent with the current host design.

Record the chosen lifecycle.

---

# 9. Service Resolution

Worker should resolve only the public abstraction:

```text
IResearchUseCase
```

Do not resolve:

```text
ResearchUseCase
DeterministicObservationSource
IObservationSource
ObservationSeries
MeanPrice
```

unless strictly necessary for already-authorized composition mechanics.

Worker must not manually construct the implementation graph.

No service locator pattern may be introduced beyond normal composition-root resolution.

---

# 10. Output / Logging

Prefer existing logging conventions if already configured.

If the minimal Worker currently has no meaningful logging pipeline and console output is already acceptable by repository guidance, use the smallest compliant mechanism.

Success output must make the canonical execution objectively visible.

At minimum, surface:

```text
target
observation count
mean price
```

Expected failure output must surface the failure category.

Do not add:

```text
JSON serialization framework
logging package
telemetry backend
OpenTelemetry exporters
metrics backend
distributed tracing
```

in WP08.

---

# 11. Exit Semantics

Choose a minimal deterministic process-exit policy.

Expected direction:

```text
success -> normal successful completion
expected research failure -> clearly surfaced non-success condition according to host conventions
unexpected exception -> not silently swallowed
```

Do not invent a generalized exit-code framework unless existing repository guidance already defines one.

Record the actual chosen behavior.

---

# 12. Authorized Scope

WP08 may:

- read all relevant authority;
- modify the minimal Worker source required to invoke the reference research flow;
- call existing `AddApplication(...)`;
- call existing `AddInfrastructure(...)`;
- resolve `IResearchUseCase`;
- construct the fixed `ResearchRequest`;
- invoke the use case once;
- surface the existing `ResearchOutcome`;
- minimally adjust Worker-local code organization if required;
- run Worker directly;
- run build/format/tests/verify;
- inspect dependency state;
- report complete evidence.

Primary mutation boundary:

```text
src/AIQuantTradingResearch.Worker/**
```

No other production mutation is expected.

---

# 13. Prohibited Scope

Do not:

- modify Domain;
- modify Application contracts;
- modify `ResearchUseCase`;
- modify Infrastructure adapter;
- modify DI registrations except to stop/report a proven blocker;
- create tests;
- modify Architecture.Tests;
- add packages;
- add project references;
- add real providers;
- add HTTP/network access;
- add persistence/database;
- add CLI framework;
- add configuration-driven research selection;
- add scheduler/background loop;
- add plugin/strategy/backtest/AI functionality;
- add API/UI;
- add new logging/telemetry infrastructure;
- modify GitHub planning;
- stage, commit, push, or create PR unless separately authorized;
- begin WP09.

---

# 14. Dependency Rules

Preserve:

```text
Domain          -> none
Application     -> Domain
Infrastructure  -> Application
Worker          -> Application + Infrastructure
```

Worker may depend on Application abstractions and Infrastructure registration extension boundaries already authorized.

Worker must not gain a direct Domain project reference.

No new project reference is authorized.

---

# 15. Working-Tree Protection

Before mutation record:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Classify all cumulative Release 0.9 artifacts.

Expected cumulative files may include:

```text
WP01–WP08 prompt/chat pairs
RESEARCH_DOMAIN_MODEL.md
WP03 Domain source
WP04 contracts
WP05 ResearchUseCase
WP06 DeterministicObservationSource
WP07 registration changes
```

Preserve all pre-existing artifacts.

Do not clean, delete, or stage unrelated/pre-existing work.

---

# 16. Implementation Procedure

## Step 1 — Read Authority

Read Release 0.9 plan, manifest, WP08 prompt, research model, current Worker, registrations, and Application contracts.

## Step 2 — Verify WP07 Baseline

Confirm:

```text
WP07 = REGISTRATION COMPLETE
AddApplication registers IResearchUseCase
AddInfrastructure registers IObservationSource
Worker currently has no research execution
```

## Step 3 — Define Worker Execution Mapping

Before coding, record internally:

| Worker Step | Existing Boundary | Intended Behavior |
| --- | --- | --- |

## Step 4 — Compose Modules

Ensure Worker invokes the existing Application and Infrastructure registration boundaries exactly once according to current host conventions.

## Step 5 — Build Provider / Host

Use current Release 0.8 Worker composition pattern.

Do not introduce a new host architecture.

## Step 6 — Resolve IResearchUseCase

Resolve the public Application abstraction.

## Step 7 — Construct Canonical Request

Use exactly:

```text
SAMPLE-USD
3
```

## Step 8 — Execute Once

Invoke the existing use case exactly once.

## Step 9 — Surface Success

For success, expose:

```text
Target
ObservationCount
MeanPrice
```

## Step 10 — Surface Expected Failure

For expected failure, expose the failure category without duplicating Application logic.

## Step 11 — Review Worker Thinness

Confirm no Domain arithmetic, fixture construction, source implementation, validation, or orchestration exists in Worker.

## Step 12 — Execute Worker Directly

Run the Worker in the approved local configuration.

Expected canonical result:

```text
SAMPLE-USD
3
110.00
```

Record actual output/exit status.

## Step 13 — Repeatability Check

Run the Worker at least twice.

Equivalent canonical output must be observed.

No network or external resource should be required.

## Step 14 — Format / Build

Run formatting verification and full solution build.

Require zero build errors.

## Step 15 — Architecture Tests

Run Architecture.Tests.

Require all accepted rules pass.

## Step 16 — Canonical Verification

Run:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Require exit status 0.

## Step 17 — Dependency / Leakage Inspection

Confirm:

```text
Worker refs unchanged
no Domain direct ref
no concrete Infrastructure type usage
no manual implementation construction
no fixture data
no arithmetic
no provider/network/storage code
```

## Step 18 — Final Git Inspection

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
git diff --check
```

Confirm exact WP08 scope.

---

# 17. Thin-Worker Review

Explicitly answer:

```text
Does Worker calculate mean?
Does Worker validate price/timestamp invariants?
Does Worker know fixture observations?
Does Worker know DeterministicObservationSource?
Does Worker call IObservationSource directly?
Does Worker construct ResearchUseCase manually?
Does Worker contain provider logic?
Does Worker contain persistence/network logic?
```

Expected:

```text
No
```

Worker may construct `ResearchRequest` and inspect `ResearchOutcome`.

---

# 18. Runtime Validation

Record:

```text
Command used
Exit status
Output
Resolved use case
Canonical target
Observation count
Mean price
Failure state
External resources used
```

Run at least twice to establish repeatability.

Do not claim a benchmark or production runtime test.

---

# 19. Decision Model

Return exactly one:

```text
WORKER EXECUTION COMPLETE
WORKER EXECUTION COMPLETE WITH ACTIONS
WORKER EXECUTION BLOCKED
```

Use `WORKER EXECUTION COMPLETE` when:

```text
Worker composes existing modules
IResearchUseCase resolves
canonical request executes once
success output contains SAMPLE-USD / 3 / 110.00
repeat execution is equivalent
Worker remains thin
no Domain/Infrastructure implementation leaks in
dependency graph remains exact
build/Architecture.Tests/verify pass
WP09 can safely proceed
```

Use `WORKER EXECUTION COMPLETE WITH ACTIONS` only for non-blocking observations.

Use `WORKER EXECUTION BLOCKED` for mandatory defects or authority conflicts.

---

# 20. Acceptance Criteria

WP08 passes only when:

- [ ] WP08 prompt read completely.
- [ ] Release 0.9 plan/manifest read completely.
- [ ] Research model inspected.
- [ ] WP07 registration implementation inspected.
- [ ] WP07 `REGISTRATION COMPLETE` baseline confirmed.
- [ ] Initial Git state classified.
- [ ] Worker execution mapping established before coding.
- [ ] Worker uses existing `AddApplication(...)`.
- [ ] Worker uses existing `AddInfrastructure(...)`.
- [ ] Worker resolves `IResearchUseCase`.
- [ ] Worker does not resolve concrete `ResearchUseCase`.
- [ ] Worker does not resolve `IObservationSource` directly.
- [ ] Worker constructs canonical `ResearchRequest("SAMPLE-USD", 3)` or semantically equivalent approved contract construction.
- [ ] Worker invokes the use case exactly once per process execution.
- [ ] Success output surfaces target.
- [ ] Success output surfaces observation count.
- [ ] Success output surfaces mean price.
- [ ] Canonical mean is `110.00`.
- [ ] Expected failure contract is handled without duplicating Application logic.
- [ ] Worker does not contain Domain arithmetic.
- [ ] Worker does not contain Domain invariant validation.
- [ ] Worker does not contain fixture data.
- [ ] Worker does not reference concrete Infrastructure implementation.
- [ ] Worker does not manually construct implementation graph.
- [ ] Worker execution is offline.
- [ ] Two runs produce equivalent canonical result.
- [ ] No new package added.
- [ ] No project reference added.
- [ ] Worker retains Application + Infrastructure references only.
- [ ] No direct Worker -> Domain reference.
- [ ] Build succeeds with zero errors.
- [ ] Architecture.Tests pass.
- [ ] `eng/verify.ps1` passes.
- [ ] `git diff --check` passes.
- [ ] Final Git state inspected.
- [ ] No tests created.
- [ ] No GitHub planning mutation.
- [ ] WP09 not started.

---

# 21. Expected Output Contract

Return one complete:

```text
Release 0.9 WP08 Worker Research Execution Execution Report
```

Use this structure.

# Release 0.9 WP08 Worker Research Execution Execution Report

## 1. Executive Summary

```text
Release:
Work Package:
Objective:
Execution model:
Canonical request:
Canonical result:
Repeatability:
Technical validation:
Final decision:
```

## 2. Execution Context

```text
Repository:
Branch:
HEAD:
Initial Git state:
WP07 baseline:
```

## 3. Authoritative Sources Reviewed

List exact paths.

## 4. Worker Execution Mapping

| Step | Existing Boundary | Implemented Worker Behavior |
| --- | --- | --- |

## 5. Worker Source Changes

| Path | Change | Purpose |
| --- | --- | --- |

## 6. Composition

```text
AddApplication:
AddInfrastructure:
Host/provider model:
Manual construction:
Assessment:
```

## 7. Service Resolution

```text
Resolved abstraction:
Concrete type referenced directly:
IObservationSource resolved directly:
Assessment:
```

## 8. Canonical Request

```text
Target:
Requested count:
Configuration source:
Assessment:
```

## 9. Success Handling

```text
Target surfaced:
Observation count surfaced:
Mean price surfaced:
Observed mean:
Assessment:
```

## 10. Expected Failure Handling

```text
Failure contract:
Duplicate validation:
Exception swallowing:
Assessment:
```

## 11. Worker Thinness Assessment

```text
Domain arithmetic:
Domain validation:
Fixture data:
Concrete adapter knowledge:
Source invocation:
Manual use-case construction:
Provider/network/storage logic:
Assessment:
```

## 12. Runtime Validation

```text
Run 1 command:
Run 1 exit:
Run 1 result:

Run 2 command:
Run 2 exit:
Run 2 result:

Equivalent:
External resources:
Assessment:
```

## 13. Dependency Assessment

```text
Worker project references:
Direct Domain reference:
Package changes:
Cycles:
Assessment:
```

## 14. Scope-Leakage Assessment

```text
Infrastructure implementation:
Domain logic:
CLI/config expansion:
Scheduler/background loop:
Network/persistence:
Future scope:
Assessment:
```

## 15. Build / Architecture Validation

```text
Build:
Errors:
Architecture.Tests:
Cycles:
Assessment:
```

## 16. Canonical Verification

```text
Command:
Exit status:
Warnings:
Assessment:
```

## 17. Test Boundary

State that WP08 creates no behavioral tests and that WP09–WP11 own those suites.

## 18. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 19. Final Git State

```text
WP08-owned changes:
Pre-existing changes:
Staged:
Tracked modifications:
Unexpected:
```

## 20. Scope Compliance

| Check | Result | Evidence |
| --- | --- | --- |
| Worker execution only | PASS/FAIL | |
| Existing DI boundaries used | PASS/FAIL | |
| Abstraction-only resolution | PASS/FAIL | |
| Canonical request exact | PASS/FAIL | |
| Canonical result exact | PASS/FAIL | |
| Worker remains thin | PASS/FAIL | |
| Offline/repeatable | PASS/FAIL | |
| No tests | PASS/FAIL | |
| No dependency additions | PASS/FAIL | |
| WP09 not started | PASS/FAIL | |

## 21. Findings

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 22. Acceptance Criteria Matrix

Reproduce applicable criteria with:

```text
PASS
FAIL
N/A
```

## 23. Final Decision

State exactly one:

```text
WORKER EXECUTION COMPLETE
WORKER EXECUTION COMPLETE WITH ACTIONS
WORKER EXECUTION BLOCKED
```

Explain why.

## 24. Next Authorized Work Package

If and only if progression is permitted:

```text
WP09 — Domain Tests
```

Do not begin it.

---

# 22. Final Instruction

Execute Release 0.9 / WP08 — Worker Research Execution.

Use the existing Application and Infrastructure registration boundaries.

Resolve only `IResearchUseCase`.

Construct the fixed canonical request:

```text
SAMPLE-USD
3
```

Execute once and surface the existing `ResearchOutcome`.

For success, surface:

```text
SAMPLE-USD
3
110.00
```

Run the Worker twice and prove equivalent deterministic output.

Do not implement Domain logic, fixture data, Infrastructure behavior, direct source access, manual implementation construction, CLI/config expansion, scheduler/background processing, tests, or future scope.

Do not add packages or project references.

Build, run Architecture.Tests, run `eng/verify.ps1`, inspect dependencies/Worker thinness/final Git state, and return the full WP08 report.

Finish with exactly one:

```text
WORKER EXECUTION COMPLETE
WORKER EXECUTION COMPLETE WITH ACTIONS
WORKER EXECUTION BLOCKED
```

If complete, identify:

```text
WP09 — Domain Tests
```

as next.

Do not execute WP09.

# Conclusion

WP08 is the first complete executable Release 0.9 vertical slice:

```text
Worker
  ↓
IResearchUseCase
  ↓
ResearchUseCase
  ↓
IObservationSource
  ↓
DeterministicObservationSource
  ↓
ObservationSeries
  ↓
MeanPrice
  ↓
ResearchOutcome
  ↓
SAMPLE-USD / 3 / 110.00
```

> **Worker should prove that the architecture works end-to-end without becoming the place where the architecture's behavior lives.**
