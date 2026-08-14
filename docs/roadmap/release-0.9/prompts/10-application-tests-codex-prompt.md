# Codex Execution Prompt — Release 0.9 / WP10 Application Tests

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 10 — Application Tests |
| Type | Tests |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Prerequisite | WP09 — Domain Tests = `DOMAIN TESTS COMPLETE` |
| Primary Authorities | Release 0.9 execution plan/file manifest, WP02 research model, WP04 contracts, WP05 use case |
| Execution Mode | Narrowly scoped Application behavioral testing |
| Expected Outcome | Convert `AIQuantTradingResearch.Application.Tests` from an intentional skeleton into a meaningful deterministic behavioral suite for `ResearchUseCase`, using Application-owned test doubles and without depending on concrete Infrastructure, Worker, or architecture-test changes |

---

# 1. Purpose

Execute:

```text
Phase 2 — Release 0.9
WP10 — Application Tests
```

WP10 creates the authoritative behavioral suite for the Release 0.9 Application orchestration.

The suite must prove the behavior implemented in WP05:

```text
request validation
source invocation through IObservationSource
expected source-failure mapping
exact-count enforcement
Domain delegation
ResearchResult / ResearchOutcome construction
```

WP10 must test Application independently from concrete Infrastructure.

Do not begin WP11.

---

# 2. Authoritative Sources

Read completely before mutation:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/10-application-tests-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Inspect current Application contracts and implementation:

```text
src/AIQuantTradingResearch.Application/Research/ResearchRequest.cs
src/AIQuantTradingResearch.Application/Research/ResearchResult.cs
src/AIQuantTradingResearch.Application/Research/ResearchFailure.cs
src/AIQuantTradingResearch.Application/Research/ResearchOutcome.cs
src/AIQuantTradingResearch.Application/Research/IResearchUseCase.cs
src/AIQuantTradingResearch.Application/Research/IObservationSource.cs
src/AIQuantTradingResearch.Application/Research/ObservationSourceResult.cs
src/AIQuantTradingResearch.Application/Research/ObservationSourceFailure.cs
src/AIQuantTradingResearch.Application/Research/ResearchUseCase.cs
```

Inspect relevant Domain types only as collaborators exposed through Application contracts:

```text
src/AIQuantTradingResearch.Domain/PriceObservation.cs
src/AIQuantTradingResearch.Domain/ObservationSeries.cs
src/AIQuantTradingResearch.Domain/MeanPrice.cs
```

Inspect current Application test project and testing guidance:

```text
tests/AIQuantTradingResearch.Application.Tests/**
docs/architecture/implementation/TESTING_STRATEGY.md
docs/architecture/implementation/CODING_PRINCIPLES.md
docs/architecture/implementation/NAMING_CONVENTIONS.md
```

Use the existing test framework/packages.

Do not add a mocking library unless already present and required by repository authority.

---

# 3. Accepted WP05 Application Behavior

The concrete use case:

```text
ResearchUseCase
```

implements:

```text
IResearchUseCase
```

and depends only on:

```text
IObservationSource
```

Approved orchestration:

```text
validate request
        ↓
invoke IObservationSource
        ↓
map expected source failures
        ↓
require exact requested count
        ↓
construct ObservationSeries
        ↓
invoke CalculateMeanPrice()
        ↓
construct ResearchResult
        ↓
return ResearchOutcome
```

Expected request failures:

```text
blank target -> InvalidRequest
count <= 0 -> InvalidRequest
```

Expected source failures:

```text
UnsupportedTarget -> ResearchFailure.UnsupportedTarget
InsufficientObservations -> ResearchFailure.InsufficientObservations
```

Count mismatch policy:

```text
under-count -> InsufficientObservations
over-count -> InsufficientObservations
```

Canonical success:

```text
SAMPLE-USD / 3
100.00 / 110.00 / 120.00
-> Success / 3 / 110.00
```

---

# 4. Testing Principles

## 4.1 Test Application Behavior, Not Infrastructure

Use an Application-test-owned deterministic test double for:

```text
IObservationSource
```

Do not instantiate:

```text
DeterministicObservationSource
Infrastructure.DependencyInjection
Worker
Host
ServiceCollection
```

unless an individual test is explicitly authorized by Application scope—which this WP does not require.

## 4.2 Prefer Hand-Written Test Doubles

For this small use case, prefer a tiny deterministic fake/stub implemented inside the Application test project.

Do not add Moq/NSubstitute/FakeItEasy merely for convenience unless already present.

The double should allow controlled:

```text
source result
call count
captured request
```

and nothing more.

## 4.3 Test Observable Orchestration

Assert:

```text
outcome
failure category
source call count
captured request
result target/count/mean
```

Do not assert private implementation details.

## 4.4 Deterministic / Offline

All tests must be:

```text
offline
fixed-data
clock-independent
randomness-free
culture-safe
```

---

# 5. Required Test Areas

WP10 must cover at least the following.

## 5.1 Valid Request Invokes Source Once

Given:

```text
SAMPLE-USD / 3
```

verify:

```text
source invoked exactly once
captured target = SAMPLE-USD
captured count = 3
```

## 5.2 Blank Target Returns InvalidRequest

Verify:

```text
failure = InvalidRequest
source call count = 0
```

## 5.3 Whitespace Target Returns InvalidRequest

If blank/whitespace semantics are explicit in implementation, test whitespace separately when meaningful.

## 5.4 Zero Count Returns InvalidRequest

Verify source is not invoked.

## 5.5 Negative Count Returns InvalidRequest

Verify source is not invoked.

## 5.6 Unsupported Target Maps Correctly

Configure test double:

```text
ObservationSourceFailure.UnsupportedTarget
```

Expected:

```text
ResearchFailure.UnsupportedTarget
```

## 5.7 Insufficient Observations Maps Correctly

Configure:

```text
ObservationSourceFailure.InsufficientObservations
```

Expected:

```text
ResearchFailure.InsufficientObservations
```

## 5.8 Under-Count Is Rejected

For request count 3, source returns 2 valid observations.

Expected:

```text
ResearchFailure.InsufficientObservations
```

No mean should be returned.

## 5.9 Over-Count Is Rejected

For request count 3, source returns 4 valid observations.

Expected:

```text
ResearchFailure.InsufficientObservations
```

No truncation.

## 5.10 Canonical Success

Source returns:

```text
100.00
110.00
120.00
```

Expected:

```text
success
target = SAMPLE-USD
observation count = 3
mean = 110.00
```

This is mandatory.

## 5.11 Domain Behavior Is Delegated

Choose data that makes the Domain calculation observable and proves Application did not hard-code the canonical mean.

Example:

```text
10.00
20.00
90.00
-> 40.00
```

Do not inspect implementation internals; assert the result only.

## 5.12 Unexpected Invalid Source Data Is Not Hidden

If the source returns a successful payload that violates Domain invariants, such as out-of-order observations, confirm the current Application behavior matches authority:

```text
unexpected Domain invariant failure remains unexpected
not mapped to InvalidRequest/UnsupportedTarget/InsufficientObservations
```

Assert only the agreed exception category/type when stable.

Do not broaden expected failure semantics.

---

# 6. Optional Additional Tests

Only add tests that protect actual WP04/WP05 semantics.

Potential examples:

```text
null ResearchRequest remains programming-contract violation
null/structurally invalid source result remains unexpected failure
source is not invoked after request validation failure
result observation count equals actual approved count
```

Do not freeze incidental behavior unless clearly intentional.

Record optional tests and justification.

---

# 7. Test Double Design

Create the smallest test-only implementation of:

```text
IObservationSource
```

It may expose test-only state such as:

```text
CallCount
LastRequest
ConfiguredResult
```

Keep it in the Application test project.

Do not place test doubles in production projects.

Do not create a generic fake framework.

---

# 8. Test Naming / Organization

Follow repository conventions.

Prefer focused organization such as:

```text
ResearchUseCaseTests.cs
```

A separate helper file for the test double is acceptable only if it improves clarity.

Avoid unnecessary test-base classes or fixtures.

Test names should communicate:

```text
subject
condition
expected result
```

without analyzer violations.

---

# 9. Authorized Scope

WP10 may:

- read all relevant authority;
- add Application behavioral test files;
- add small Application-test-owned test doubles/helpers;
- use existing xUnit infrastructure;
- run Application.Tests directly;
- run Domain.Tests to ensure lower-level behavior remains healthy;
- run solution build;
- run Architecture.Tests;
- run canonical verification;
- inspect project/package references;
- report complete evidence.

Primary mutation boundary:

```text
tests/AIQuantTradingResearch.Application.Tests/**
```

No production code mutation is expected.

---

# 10. Prohibited Scope

Do not:

- modify Application production code unless a blocking defect is discovered; stop/report rather than silently fixing;
- modify Domain production code;
- modify Infrastructure;
- modify Worker;
- instantiate concrete Infrastructure adapter in tests;
- use DI container for core use-case tests;
- create Infrastructure tests;
- create Worker tests;
- modify Architecture.Tests;
- add packages;
- add cross-layer project references to Infrastructure/Worker;
- add mocking frameworks merely for convenience;
- add coverage tooling;
- modify GitHub planning;
- stage, commit, push, or create PR unless separately authorized;
- begin WP11.

---

# 11. Dependency Rules

Application.Tests may depend on:

```text
Application
Domain as already transitively/explicitly required by current test project configuration
test framework packages already configured
```

It must not gain:

```text
Infrastructure project reference
Worker project reference
```

Inspect actual current project references before changing anything.

No dependency expansion is expected.

---

# 12. Working-Tree Protection

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
WP01–WP10 prompt/chat pairs
RESEARCH_DOMAIN_MODEL.md
WP03–WP08 production changes
WP09 Domain tests
```

Preserve all pre-existing artifacts.

Do not clean, delete, or stage unrelated/pre-existing work.

---

# 13. Test Design Procedure

## Step 1 — Read Authority

Read plan, manifest, WP10 prompt, research model, Application contracts/use case, Domain types, and testing strategy.

## Step 2 — Verify Starting State

Confirm:

```text
WP09 = DOMAIN TESTS COMPLETE
Application.Tests currently discovers zero tests
ResearchUseCase implementation unchanged from WP05
```

## Step 3 — Build Application Behavior Matrix

Before coding:

| Scenario | Source Behavior | Expected Outcome | Source Call Count |
| --- | --- | --- | --- |

## Step 4 — Implement Minimal Test Double

Create a deterministic test-owned `IObservationSource` fake/stub.

## Step 5 — Implement Request Validation Tests

Cover blank/whitespace target and zero/negative count.

## Step 6 — Implement Source Failure Mapping Tests

Cover unsupported and insufficient.

## Step 7 — Implement Exact-Count Tests

Cover under-count and over-count.

## Step 8 — Implement Canonical Success Test

Cover:

```text
SAMPLE-USD / 3 -> 110.00
```

## Step 9 — Implement Non-Hardcoded Domain Delegation Test

Use a different valid observation set, such as:

```text
10 / 20 / 90 -> 40
```

## Step 10 — Implement Unexpected Invalid-Source-Data Test

Only if behavior is stable and clearly part of current Application semantics.

## Step 11 — Review for Infrastructure Coupling

Confirm no concrete Infrastructure type/reference exists.

## Step 12 — Run Application.Tests Directly

Require:

```text
discovered > 0
failed = 0
```

Record exact counts.

## Step 13 — Run Domain.Tests

Confirm lower-level behavioral suite remains passing.

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

Canonical test discovery should now include:

```text
Domain.Tests
Application.Tests
Architecture.Tests
```

Infrastructure.Tests may remain intentionally empty until WP11.

## Step 17 — Dependency Inspection

Verify no Infrastructure/Worker project references or package changes.

## Step 18 — Final Git Inspection

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
git diff --check
```

Confirm exact WP10 scope.

---

# 14. Validation Evidence Required

Record:

```text
test files created
test double/helper files
tests discovered/passed/failed/skipped
request-validation behavior
source-call count behavior
source failure mapping
under-count/over-count handling
canonical success
non-hardcoded mean behavior
Application.Tests references
package references
Domain.Tests status
Architecture.Tests status
eng/verify.ps1 status
final Git scope
```

---

# 15. Quality Review

Before completion answer:

```text
Do tests use concrete Infrastructure?
Do tests require DI container?
Do tests assert private implementation?
Do tests use network/time/randomness?
Does invalid request avoid source invocation?
Are unsupported/insufficient mappings protected?
Are under-count and over-count protected?
Is canonical 110.00 automated?
Is another mean value automated to avoid hard-coded orchestration?
```

Expected:

```text
No
No
No
No
Yes
Yes
Yes
Yes
Yes
```

---

# 16. Decision Model

Return exactly one:

```text
APPLICATION TESTS COMPLETE
APPLICATION TESTS COMPLETE WITH ACTIONS
APPLICATION TESTS BLOCKED
```

Use `APPLICATION TESTS COMPLETE` when:

```text
Application.Tests contains meaningful deterministic tests
all approved orchestration paths are protected
concrete Infrastructure is not required
all tests pass
no production mutation occurred
no dependency expansion occurred
build/Domain.Tests/Architecture.Tests/verify pass
WP11 can safely proceed
```

Use `APPLICATION TESTS COMPLETE WITH ACTIONS` only for non-blocking observations.

Use `APPLICATION TESTS BLOCKED` for mandatory defects or authority conflicts.

---

# 17. Acceptance Criteria

WP10 passes only when:

- [ ] WP10 prompt read completely.
- [ ] Release 0.9 plan/manifest read completely.
- [ ] `RESEARCH_DOMAIN_MODEL.md` read completely.
- [ ] WP04 contracts inspected.
- [ ] WP05 use case inspected.
- [ ] Testing strategy inspected.
- [ ] WP09 `DOMAIN TESTS COMPLETE` baseline confirmed.
- [ ] Initial Git state classified.
- [ ] Application behavior matrix established before coding.
- [ ] Application.Tests discovers more than zero tests.
- [ ] Test-owned deterministic `IObservationSource` double created.
- [ ] Valid request invokes source exactly once.
- [ ] Captured target/count verified.
- [ ] Blank target returns `InvalidRequest`.
- [ ] Whitespace target behavior tested where applicable.
- [ ] Zero count returns `InvalidRequest`.
- [ ] Negative count returns `InvalidRequest`.
- [ ] Invalid request does not invoke source.
- [ ] Unsupported source failure maps correctly.
- [ ] Insufficient source failure maps correctly.
- [ ] Under-count maps to `InsufficientObservations`.
- [ ] Over-count maps to `InsufficientObservations`.
- [ ] No truncation/partial success.
- [ ] Canonical `SAMPLE-USD / 3 -> 110.00` automated.
- [ ] Alternate valid observation set proves Domain delegation/non-hardcoding.
- [ ] Unexpected invalid-source data behavior protected if stable/authorized.
- [ ] All Application.Tests pass.
- [ ] Domain.Tests remain passing.
- [ ] No Application production code modified.
- [ ] No Domain/Infrastructure/Worker production code modified.
- [ ] No concrete Infrastructure used in tests.
- [ ] No DI container used for core tests.
- [ ] No new package/framework added.
- [ ] No Infrastructure/Worker project reference added.
- [ ] Tests deterministic/offline.
- [ ] Architecture.Tests unchanged.
- [ ] Build succeeds with zero errors.
- [ ] Architecture.Tests pass.
- [ ] `eng/verify.ps1` passes.
- [ ] `git diff --check` passes.
- [ ] Final Git state inspected.
- [ ] No GitHub mutation.
- [ ] WP11 not started.

---

# 18. Expected Output Contract

Return one complete:

```text
Release 0.9 WP10 Application Tests Execution Report
```

Use this structure.

# Release 0.9 WP10 Application Tests Execution Report

## 1. Executive Summary

```text
Release:
Work Package:
Objective:
Test files created:
Tests discovered:
Tests passed:
Tests failed:
Technical validation:
Final decision:
```

## 2. Execution Context

```text
Repository:
Branch:
HEAD:
Initial Git state:
WP09 baseline:
```

## 3. Authoritative Sources Reviewed

List exact paths.

## 4. Application Behavior Matrix

| Scenario | Source Behavior | Expected Outcome | Source Calls |
| --- | --- | --- | --- |

## 5. Test Source Changes

| Path | Change | Purpose |
| --- | --- | --- |

## 6. Test Double Design

```text
Type:
Implements:
Configured behavior:
Captured state:
External dependencies:
Assessment:
```

## 7. Request Validation Tests

```text
Blank target:
Whitespace target:
Zero count:
Negative count:
Source calls:
Assessment:
```

## 8. Source Failure Mapping Tests

```text
Unsupported:
Insufficient:
Assessment:
```

## 9. Exact-Count Tests

```text
Under-count:
Over-count:
Partial success:
Assessment:
```

## 10. Success / Domain Delegation Tests

```text
Canonical SAMPLE-USD / 3:
Observed mean:
Alternate valid dataset:
Observed alternate mean:
Assessment:
```

## 11. Unexpected Invalid-Source-Data Behavior

```text
Scenario:
Observed behavior:
Mapped to expected failure:
Assessment:
```

Use `N/A` if intentionally omitted because behavior is not stable/authorized.

## 12. Test Quality Assessment

```text
Concrete Infrastructure:
DI container:
Private implementation assertions:
Network/time/randomness:
Cross-layer concerns:
Assessment:
```

## 13. Application.Tests Direct Execution

```text
Command:
Discovered:
Passed:
Failed:
Skipped:
Assessment:
```

## 14. Domain.Tests Regression

```text
Discovered:
Passed:
Failed:
Assessment:
```

## 15. Dependency Assessment

```text
Application.Tests project references:
Infrastructure reference:
Worker reference:
Package changes:
Assessment:
```

## 16. Build / Architecture Validation

```text
Build:
Errors:
Architecture.Tests:
Cycles:
Assessment:
```

## 17. Canonical Verification

```text
Command:
Exit status:
Total tests observed:
Warnings:
Assessment:
```

## 18. Production-Code Protection

```text
Application production modified:
Other production modified:
Assessment:
```

## 19. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 20. Final Git State

```text
WP10-owned changes:
Pre-existing changes:
Staged:
Tracked modifications:
Unexpected:
```

## 21. Scope Compliance

| Check | Result | Evidence |
| --- | --- | --- |
| Application-tests-only scope | PASS/FAIL | |
| Deterministic test double only | PASS/FAIL | |
| No concrete Infrastructure | PASS/FAIL | |
| No DI container for core tests | PASS/FAIL | |
| No production mutation | PASS/FAIL | |
| No dependency expansion | PASS/FAIL | |
| WP11 not started | PASS/FAIL | |

## 22. Findings

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 23. Acceptance Criteria Matrix

Reproduce applicable criteria with:

```text
PASS
FAIL
N/A
```

## 24. Final Decision

State exactly one:

```text
APPLICATION TESTS COMPLETE
APPLICATION TESTS COMPLETE WITH ACTIONS
APPLICATION TESTS BLOCKED
```

Explain why.

## 25. Next Authorized Work Package

If and only if progression is permitted:

```text
WP11 — Infrastructure Tests
```

Do not begin it.

---

# 19. Final Instruction

Execute Release 0.9 / WP10 — Application Tests.

Create meaningful deterministic behavioral tests for `ResearchUseCase` using only an Application-test-owned `IObservationSource` double.

Protect:

```text
valid source invocation
blank/whitespace/zero/negative request validation
unsupported failure mapping
insufficient failure mapping
under-count rejection
over-count rejection
canonical SAMPLE-USD / 3 -> 110.00
alternate valid data -> correct Domain-derived mean
```

Do not use concrete Infrastructure.

Do not use the DI container for core tests.

Do not modify production code unless a genuine blocking defect is discovered; stop/report rather than silently fixing it.

Do not add packages or cross-layer references.

Run Application.Tests directly, Domain.Tests regression, full build, Architecture.Tests, `eng/verify.ps1`, inspect dependencies/scope/Git state, and return the complete WP10 report.

Finish with exactly one:

```text
APPLICATION TESTS COMPLETE
APPLICATION TESTS COMPLETE WITH ACTIONS
APPLICATION TESTS BLOCKED
```

If complete, identify:

```text
WP11 — Infrastructure Tests
```

as next.

Do not execute WP11.

# Conclusion

WP10 converts Application orchestration into executable specifications without depending on Infrastructure:

```text
ResearchUseCase
      ↓
Test-Owned IObservationSource Double
      ↓
Request Validation
      ↓
Failure Mapping
      ↓
Exact Count Enforcement
      ↓
Domain Delegation
      ↓
ResearchOutcome Assertions
      ↓
APPLICATION TESTS COMPLETE
      ↓
WP11 Infrastructure Tests
```

> **Application tests should prove orchestration against owned abstractions, not against the concrete infrastructure that happens to satisfy them.**
