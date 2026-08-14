# Codex Execution Prompt — Release 0.9 / WP05 Research Execution Use Case

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 05 — Research Execution Use Case |
| Type | Implementation |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Prerequisite | WP04 — Research Application Contracts = `CONTRACTS COMPLETE` |
| Primary Authorities | `RESEARCH_DOMAIN_MODEL.md`, WP03 Domain implementation, WP04 Application contracts |
| Execution Mode | Narrowly scoped Application orchestration implementation |
| Expected Outcome | Implement the concrete Application use case behind `IResearchUseCase`, validating requests, invoking `IObservationSource`, constructing the approved Domain series, calculating the mean, and mapping explicit outcomes without implementing Infrastructure, DI, Worker, or behavioral tests |

---

# 1. Purpose

Execute:

```text
Phase 2 — Release 0.9
WP05 — Research Execution Use Case
```

WP05 turns the WP04 Application contracts into executable Application orchestration.

The use case must:

```text
validate request
        ↓
obtain observations through IObservationSource
        ↓
translate source failures
        ↓
construct valid ObservationSeries
        ↓
invoke Domain mean behavior
        ↓
construct ResearchResult
        ↓
return ResearchOutcome
```

WP05 owns orchestration only.

Do not implement Infrastructure.

Do not modify Worker.

Do not perform DI registration.

Do not create behavioral tests.

Do not begin WP06.

---

# 2. Authoritative Sources

Read completely before mutation:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/05-research-execution-use-case-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Inspect current Domain implementation:

```text
src/AIQuantTradingResearch.Domain/PriceObservation.cs
src/AIQuantTradingResearch.Domain/ObservationSeries.cs
src/AIQuantTradingResearch.Domain/MeanPrice.cs
```

Inspect current Application contracts:

```text
src/AIQuantTradingResearch.Application/Research/ResearchRequest.cs
src/AIQuantTradingResearch.Application/Research/ResearchResult.cs
src/AIQuantTradingResearch.Application/Research/ResearchFailure.cs
src/AIQuantTradingResearch.Application/Research/ResearchOutcome.cs
src/AIQuantTradingResearch.Application/Research/IResearchUseCase.cs
src/AIQuantTradingResearch.Application/Research/IObservationSource.cs
src/AIQuantTradingResearch.Application/Research/ObservationSourceResult.cs
src/AIQuantTradingResearch.Application/Research/ObservationSourceFailure.cs
```

Inspect Application architecture/error-handling guidance:

```text
docs/architecture/design/ERROR_HANDLING.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/PUBLIC_CONTRACTS.md
docs/architecture/implementation/CODING_PRINCIPLES.md
docs/architecture/implementation/IMPLEMENTATION_GUIDELINES.md
```

WP05 must use the established contracts as authority rather than redesigning them.

---

# 3. Accepted WP04 Contracts

Expected Application contracts:

```text
ResearchRequest
ResearchResult
ResearchFailure
ResearchOutcome
IResearchUseCase
IObservationSource
ObservationSourceResult
ObservationSourceFailure
```

Expected ownership:

```text
Application owns:
  target
  requested count
  request/result
  use-case orchestration
  observation-source abstraction
  explicit expected use-case outcomes

Domain owns:
  PriceObservation
  ObservationSeries
  MeanPrice
  arithmetic mean behavior

Infrastructure owns later:
  deterministic observation-source implementation

Worker owns later:
  reference invocation / composition
```

Do not change this ownership model unless a blocker is proven.

---

# 4. Required Orchestration Behavior

## 4.1 Validate Request

WP05 must validate Application-owned request semantics before invoking the source.

Expected invalid request conditions include:

```text
blank/whitespace target
requested observation count <= 0
```

Return the explicit Application invalid-request outcome.

Do not use exceptions as normal control flow for these expected cases.

Do not move this validation into Domain.

---

## 4.2 Invoke Observation Source

Call the Application-owned:

```text
IObservationSource
```

using the approved request semantics.

The use case must not know:

```text
fixture details
provider details
network
HTTP
database
filesystem
Infrastructure type
```

---

## 4.3 Map Observation Source Failures

Translate source-level expected failures into use-case failures.

At minimum:

```text
ObservationSourceFailure.UnsupportedTarget
    -> ResearchFailure.UnsupportedTarget

ObservationSourceFailure.InsufficientObservations
    -> ResearchFailure.InsufficientObservations
```

Do not leak source-result implementation details to Worker.

Do not create generalized error-mapping infrastructure.

---

## 4.4 Enforce Complete Requested Count

The use case must not calculate a partial result.

If the source success payload does not contain the exact requested number of observations, return the explicit insufficient-observations outcome.

Do not silently truncate.

Do not silently continue with fewer observations.

If the source unexpectedly returns more than requested and the WP04 contract permits it, determine from existing authority whether exact-count enforcement requires rejection or a minimal approved selection. Prefer the stricter interpretation consistent with WP02: the use case requires the complete requested count and no partial calculation.

Record the chosen interpretation in the report.

---

## 4.5 Construct Domain Series

Use the returned observations to construct:

```text
ObservationSeries
```

Domain remains responsible for:

```text
non-empty series
unique timestamps
strict ordering
positive prices
```

The Application should not duplicate Domain invariant logic.

If Domain construction rejects invalid observations, treat this according to the repository's unexpected/internal-failure conventions rather than converting every Domain invariant violation into an expected use-case failure unless authority explicitly requires that mapping.

Do not invent a broad exception framework.

---

## 4.6 Calculate Mean Price

Invoke the existing Domain behavior:

```text
ObservationSeries.CalculateMeanPrice()
```

WP05 must not reimplement arithmetic.

---

## 4.7 Construct Success Result

Build the existing `ResearchResult` with:

```text
target
actual/approved observation count
MeanPrice
```

Then return the existing successful `ResearchOutcome`.

Do not add provider metadata or future fields.

---

# 5. Canonical Reference Scenario

The WP05 implementation must make this orchestration possible when paired with a compatible observation-source test double/harness:

```text
Request:
  Target = SAMPLE-USD
  RequestedObservationCount = 3

Source success:
  2024-01-01T00:00:00+00:00 -> 100.00
  2024-01-02T00:00:00+00:00 -> 110.00
  2024-01-03T00:00:00+00:00 -> 120.00

Expected use-case result:
  Success
  Target = SAMPLE-USD
  ObservationCount = 3
  MeanPrice = 110.00
```

Expected failure scenarios:

```text
blank target
count = 0
unsupported target
insufficient observations
```

Infrastructure fixture implementation remains WP06.

---

# 6. Implementation Shape

Create the smallest concrete Application type required to implement:

```text
IResearchUseCase
```

Do not create:

```text
handler framework
mediator layer
pipeline behaviors
command/query hierarchy
application service base class
factory framework
generic orchestration abstraction
```

One concrete use-case implementation is expected unless repository conventions demand otherwise.

---

# 7. Constructor / Dependency Design

The concrete use case should receive only the dependencies it genuinely needs.

Expected dependency:

```text
IObservationSource
```

Do not inject:

```text
IServiceProvider
ILogger unless repository authority specifically requires logging here
configuration
clock
random
HTTP client
Infrastructure type
```

Do not introduce unnecessary dependencies merely for future needs.

---

# 8. Synchronous / Asynchronous Semantics

WP04 established the current contract shape.

Do not redesign synchronous/asynchronous semantics during WP05 unless a blocking contract defect is proven.

If `IResearchUseCase` and `IObservationSource` are synchronous, implement synchronously.

Do not introduce `Task`, cancellation tokens, or asynchronous abstractions merely because future providers may need them.

Future I/O concerns belong to later evolution when real external systems exist.

---

# 9. Error Semantics

Expected failures remain explicit:

```text
InvalidRequest
UnsupportedTarget
InsufficientObservations
```

Do not use expected exceptions as normal flow.

Unexpected programming/domain invariant failures should not be silently converted into an expected business outcome unless repository authority explicitly says so.

Keep expected vs unexpected failure semantics distinct.

---

# 10. Authorized Scope

WP05 may:

- read all relevant authority;
- add the minimum concrete use-case implementation under Application;
- minimally add internal helper code under Application only if strictly required and justified;
- use existing WP04 contracts;
- use existing WP03 Domain types/behavior;
- use the existing `IObservationSource` abstraction;
- build/format/test/verify;
- use temporary local validation harnesses that are removed afterward;
- inspect project/package/dependency state;
- report complete evidence.

Primary mutation boundary:

```text
src/AIQuantTradingResearch.Application/**
```

Expected implementation area:

```text
src/AIQuantTradingResearch.Application/Research/**
```

---

# 11. Prohibited Scope

Do not:

- modify Infrastructure;
- implement deterministic adapter;
- modify Worker;
- register DI;
- modify `DependencyInjection.cs` unless explicitly authorized by a separate WP;
- create behavioral tests;
- modify Domain except for a proven blocking defect requiring stop/report;
- add packages;
- add project references;
- implement provider/network/storage concerns;
- create HTTP clients;
- create repositories;
- create plugin/strategy/backtest/AI abstractions;
- create generic Result/Error/Mediator frameworks;
- modify CI;
- modify GitHub planning;
- stage, commit, push, or create PR unless separately authorized;
- begin WP06.

---

# 12. Dependency Rules

Application must remain:

```text
Application -> Domain
```

No new production dependency edge is authorized.

The concrete use case depends on the Application-owned abstraction:

```text
IObservationSource
```

not on Infrastructure.

Confirm final:

```text
Infrastructure dependency = none
Worker dependency = none
ProjectReference changes = none
Package additions = none
```

---

# 13. Working-Tree Protection

Before mutation record:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Classify all pre-existing Release 0.9 governance, discovery, Domain, and Application-contract artifacts.

Expected cumulative untracked files may include WP01–WP05 prompt/chat pairs, `RESEARCH_DOMAIN_MODEL.md`, WP03 Domain source, and WP04 Application contracts.

Preserve them.

Do not use destructive cleanup.

Do not stage pre-existing files during WP05.

---

# 14. Implementation Procedure

## Step 1 — Read Authority

Read Release 0.9 plan, manifest, WP05 prompt, WP02 research model, WP03 Domain implementation, and WP04 contracts.

## Step 2 — Verify Starting State

Confirm:

```text
WP04 = CONTRACTS COMPLETE
IResearchUseCase has no implementation
IObservationSource exists
Domain implementation exists
Application dependency graph is valid
```

## Step 3 — Build Orchestration Mapping

Before coding, record internally:

| Orchestration Step | Existing Contract/Type | Intended Behavior |
| --- | --- | --- |

## Step 4 — Implement Concrete Use Case

Create the minimal implementation of `IResearchUseCase`.

## Step 5 — Implement Request Validation

Blank target and non-positive count return `InvalidRequest`.

## Step 6 — Invoke Source

Call `IObservationSource`.

## Step 7 — Map Source Failures

Map unsupported/insufficient failures explicitly.

## Step 8 — Enforce Complete Count

No partial research result is allowed.

## Step 9 — Construct Domain Series

Use `ObservationSeries`; do not duplicate invariants.

## Step 10 — Invoke Domain Mean

Use `CalculateMeanPrice()`.

## Step 11 — Construct Application Success Outcome

Use existing WP04 result/outcome contracts.

## Step 12 — Review Public Surface

The implementation should be public only if later DI/Worker boundaries require it. Prefer minimal visibility consistent with WP07 registration needs.

Record rationale.

## Step 13 — Temporary Orchestration Harness

If needed, use a temporary in-memory test double outside committed repository scope to verify:

```text
SAMPLE-USD / 3 -> 110.00
Invalid request -> InvalidRequest
Unsupported -> UnsupportedTarget
Insufficient -> InsufficientObservations
```

Remove the harness afterward.

Do not create WP10 tests.

## Step 14 — Format / Build

Run formatting verification and full solution build.

Require zero build errors.

## Step 15 — Architecture Tests

Run Architecture.Tests.

Require all existing tests pass.

## Step 16 — Canonical Verification

Run:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Require exit status 0.

## Step 17 — Dependency / Leakage Inspection

Inspect project/package references and search semantics for forbidden Infrastructure/Worker/future leakage.

## Step 18 — Final Git Inspection

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
git diff --check
```

Confirm exact WP05 scope.

---

# 15. Validation Evidence Required

Record objective evidence for:

```text
concrete use-case type
constructor dependencies
request validation
source invocation
source-failure mapping
exact-count enforcement
Domain-series construction
Domain mean invocation
success-result construction
canonical success scenario
canonical expected failures
project references
package references
build
Architecture.Tests
eng/verify.ps1
final Git scope
```

Do not claim WP10 behavioral-test coverage during WP05.

---

# 16. Scope-Leakage Review

Inspect the WP05 delta for:

```text
Infrastructure concrete types
Worker references
DI registration
HTTP/network
database/repository
provider/vendor names
plugin
strategy
backtest
AI/ML
generic mediator
generic handler
generic result framework
logging/configuration dependencies not justified by authority
```

Any semantic leakage must be removed before completion.

---

# 17. Decision Model

Return exactly one:

```text
USE CASE COMPLETE
USE CASE COMPLETE WITH ACTIONS
USE CASE BLOCKED
```

Use `USE CASE COMPLETE` when:

```text
request validation implemented
source abstraction invoked
expected failures mapped
complete-count rule enforced
Domain behavior delegated correctly
success result produced
Application remains independent of Infrastructure/Worker
no DI registration or downstream implementation leaked in
build/architecture/verify pass
WP06 can safely proceed
```

Use `USE CASE COMPLETE WITH ACTIONS` only for non-blocking observations.

Use `USE CASE BLOCKED` for a mandatory defect or authority conflict.

---

# 18. Acceptance Criteria

WP05 passes only when:

- [ ] WP05 prompt read completely.
- [ ] Release 0.9 plan/manifest read completely.
- [ ] `RESEARCH_DOMAIN_MODEL.md` read completely.
- [ ] WP03 Domain implementation inspected.
- [ ] WP04 contracts inspected.
- [ ] WP04 `CONTRACTS COMPLETE` baseline confirmed.
- [ ] Initial Git state classified.
- [ ] Orchestration mapping established before coding.
- [ ] Concrete `IResearchUseCase` implementation created.
- [ ] Only necessary dependency is injected.
- [ ] Blank target returns invalid-request outcome.
- [ ] Non-positive count returns invalid-request outcome.
- [ ] Observation source invoked through `IObservationSource`.
- [ ] Unsupported target maps to `ResearchFailure.UnsupportedTarget`.
- [ ] Insufficient observations maps to `ResearchFailure.InsufficientObservations`.
- [ ] No partial calculation is returned.
- [ ] `ObservationSeries` owns sequence/domain validation.
- [ ] Arithmetic is delegated to Domain.
- [ ] Success result uses existing `ResearchResult`.
- [ ] Canonical SAMPLE-USD / 3 scenario yields 110.00 with compatible source double.
- [ ] Expected failure scenarios are representable/executable.
- [ ] No Infrastructure implementation created.
- [ ] No Worker implementation created.
- [ ] No DI registration performed.
- [ ] No behavioral tests created.
- [ ] No package added.
- [ ] No project reference added.
- [ ] Application remains dependent only on Domain.
- [ ] No future-scope leakage.
- [ ] Public surface reviewed/minimized.
- [ ] Build succeeds with zero errors.
- [ ] Architecture.Tests pass.
- [ ] `eng/verify.ps1` passes.
- [ ] `git diff --check` passes.
- [ ] Final Git state inspected.
- [ ] No GitHub planning mutation.
- [ ] WP06 not started.

---

# 19. Expected Output Contract

Return one complete:

```text
Release 0.9 WP05 Research Execution Use Case Execution Report
```

Use this structure.

# Release 0.9 WP05 Research Execution Use Case Execution Report

## 1. Executive Summary

```text
Release:
Work Package:
Objective:
Concrete use-case type:
Dependency:
Success path:
Expected failures:
Technical validation:
Final decision:
```

## 2. Execution Context

```text
Repository:
Branch:
HEAD:
Initial Git state:
WP04 baseline:
```

## 3. Authoritative Sources Reviewed

List exact paths.

## 4. Orchestration Mapping

| Step | Existing Contract / Domain Type | Implemented Behavior |
| --- | --- | --- |

## 5. Application Source Changes

| Path | Change | Purpose |
| --- | --- | --- |

## 6. Concrete Use Case

```text
Type:
Implements:
Visibility:
Constructor dependencies:
Assessment:
```

## 7. Request Validation

```text
Blank target:
Zero count:
Negative count:
Exceptions used as normal flow:
Assessment:
```

## 8. Observation Source Invocation

```text
Abstraction:
Input:
Source result handling:
Infrastructure dependency:
Assessment:
```

## 9. Failure Mapping

```text
Unsupported target:
Insufficient observations:
Unexpected source state:
Assessment:
```

## 10. Complete-Count Enforcement

```text
Requested count:
Returned count:
Partial calculation allowed:
Over-count behavior:
Assessment:
```

## 11. Domain Delegation

```text
ObservationSeries construction:
Duplicated Domain validation:
Mean calculation:
Arithmetic reimplemented:
Assessment:
```

## 12. Success Result Mapping

```text
ResearchResult:
Target:
Observation count:
MeanPrice:
Provider metadata:
Assessment:
```

## 13. Canonical Scenario Validation

```text
SAMPLE-USD / 3:
Expected mean:
Observed mean:
Invalid request:
Unsupported:
Insufficient:
Temporary harness retained:
Assessment:
```

## 14. Public Surface Assessment

List public/internal types/members introduced and justify visibility.

## 15. Dependency Assessment

```text
Application project references:
Package references:
Infrastructure dependency:
Worker dependency:
Assessment:
```

## 16. Scope-Leakage Assessment

```text
Infrastructure implementation:
DI registration:
Worker implementation:
Network/storage/provider:
Future concepts:
Generic frameworks:
Assessment:
```

## 17. Build / Architecture Validation

```text
Build:
Errors:
Architecture.Tests:
Cycles:
Assessment:
```

## 18. Canonical Verification

```text
Command:
Exit status:
Warnings:
Assessment:
```

## 19. Behavioral-Test Boundary

State that WP05 does not own WP10 Application behavioral tests and that any temporary harness was removed.

## 20. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 21. Final Git State

```text
WP05-owned changes:
Pre-existing changes:
Staged:
Tracked modifications:
Unexpected:
```

## 22. Scope Compliance

| Check | Result | Evidence |
| --- | --- | --- |
| Application orchestration only | PASS/FAIL | |
| Contracts preserved | PASS/FAIL | |
| Domain behavior delegated | PASS/FAIL | |
| No Infrastructure implementation | PASS/FAIL | |
| No DI registration | PASS/FAIL | |
| No Worker implementation | PASS/FAIL | |
| No behavioral tests | PASS/FAIL | |
| No dependency additions | PASS/FAIL | |
| WP06 not started | PASS/FAIL | |

## 23. Findings

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 24. Acceptance Criteria Matrix

Reproduce applicable criteria with:

```text
PASS
FAIL
N/A
```

## 25. Final Decision

State exactly one:

```text
USE CASE COMPLETE
USE CASE COMPLETE WITH ACTIONS
USE CASE BLOCKED
```

Explain why.

## 26. Next Authorized Work Package

If and only if progression is permitted:

```text
WP06 — Research Infrastructure Adapter
```

Do not begin it.

---

# 20. Final Instruction

Execute Release 0.9 / WP05 — Research Execution Use Case.

Implement only the concrete Application orchestration behind `IResearchUseCase`.

The use case must:

```text
validate ResearchRequest
invoke IObservationSource
map unsupported/insufficient expected failures
enforce complete requested observation count
construct ObservationSeries
delegate arithmetic to Domain
construct ResearchResult
return ResearchOutcome
```

Do not implement Infrastructure.

Do not register DI.

Do not modify Worker.

Do not create behavioral tests.

Do not add packages or production project references.

Validate the canonical success/failure scenarios using a temporary harness if useful, remove it afterward, then build, run Architecture.Tests, run `eng/verify.ps1`, inspect dependencies/scope/Git state, and return the complete WP05 execution report.

Finish with exactly one:

```text
USE CASE COMPLETE
USE CASE COMPLETE WITH ACTIONS
USE CASE BLOCKED
```

If complete, identify:

```text
WP06 — Research Infrastructure Adapter
```

as next.

Do not execute WP06.

# Conclusion

WP05 is the point where the Release 0.9 Application boundary becomes executable without knowing how Infrastructure will eventually satisfy the observation dependency.

The progression is:

```text
WP04 Contracts
      ↓
Validate Request
      ↓
Invoke IObservationSource
      ↓
Map Expected Failures
      ↓
Construct ObservationSeries
      ↓
Delegate Mean Calculation
      ↓
Return ResearchOutcome
      ↓
USE CASE COMPLETE
      ↓
WP06 Infrastructure Adapter
```

> **Application orchestration should coordinate approved behavior and dependencies without absorbing Domain logic or Infrastructure knowledge.**
