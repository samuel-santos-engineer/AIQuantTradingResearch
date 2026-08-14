# Codex Execution Prompt — Release 0.9 / WP09 Domain Tests

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 09 — Domain Tests |
| Type | Tests |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Prerequisite | WP08 — Worker Research Execution = `WORKER EXECUTION COMPLETE` |
| Primary Authorities | Release 0.9 execution plan/file manifest, WP02 research model, WP03 Domain implementation |
| Execution Mode | Narrowly scoped Domain behavioral testing |
| Expected Outcome | Convert `AIQuantTradingResearch.Domain.Tests` from an intentional skeleton into a meaningful deterministic behavioral suite covering the approved Release 0.9 Domain concepts and invariants without testing Application, Infrastructure, Worker, or architecture rules |

---

# 1. Purpose

Execute:

```text
Phase 2 — Release 0.9
WP09 — Domain Tests
```

WP09 creates the authoritative behavioral test suite for the Release 0.9 Domain model.

The test project must validate the behavior introduced in WP03:

```text
PriceObservation
ObservationSeries
ObservationSeries.CalculateMeanPrice()
MeanPrice
```

WP09 does not test Application orchestration.

WP09 does not test Infrastructure.

WP09 does not test Worker.

WP09 does not evolve Architecture.Tests.

Do not begin WP10.

---

# 2. Authoritative Sources

Read completely before mutation:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/09-domain-tests-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Inspect current Domain implementation:

```text
src/AIQuantTradingResearch.Domain/PriceObservation.cs
src/AIQuantTradingResearch.Domain/ObservationSeries.cs
src/AIQuantTradingResearch.Domain/MeanPrice.cs
```

Inspect current Domain test project:

```text
tests/AIQuantTradingResearch.Domain.Tests/**
```

Inspect testing/coding guidance:

```text
docs/architecture/implementation/TESTING_STRATEGY.md
docs/architecture/implementation/CODING_PRINCIPLES.md
docs/architecture/implementation/NAMING_CONVENTIONS.md
Directory.Build.props
Directory.Packages.props
```

Use existing test framework/packages already configured.

Do not add a new test framework.

---

# 3. Accepted WP03 Domain Semantics

The approved Domain behavior is:

## PriceObservation

```text
DateTimeOffset absolute instant
decimal price
price > 0
```

Expected invalid cases:

```text
price = 0
price < 0
```

## ObservationSeries

Required invariants:

```text
at least one observation
timestamps unique
timestamps strictly increasing
input is not silently sorted
```

Expected invalid cases:

```text
empty sequence
duplicate timestamp
decreasing/out-of-order timestamp
```

## Arithmetic Mean

```text
mean = decimal sum(all prices) / observation count
```

Must:

```text
use every observation
be deterministic
delegate to Domain implementation
```

Canonical scenario:

```text
100.00
110.00
120.00
mean = 110.00
```

## MeanPrice

Represents the Domain outcome of the arithmetic mean.

No Application metadata belongs here.

---

# 4. Testing Principles

## 4.1 Test Behavior, Not Implementation Detail

Prefer assertions on:

```text
observable public behavior
invariants
value semantics
calculated results
```

Do not assert:

```text
private fields
internal collection implementation
exact local variable names
constructor implementation details
reflection internals
```

unless repository conventions explicitly require it.

## 4.2 Deterministic Tests

All tests must be:

```text
offline
repeatable
clock-independent
randomness-free
culture-safe
order-stable
```

## 4.3 Meaningful Coverage, Not Arbitrary Count

Do not create tests merely to inflate test count.

No coverage percentage target is authorized.

Each test must protect an actual Release 0.9 Domain behavior or invariant.

## 4.4 No Cross-Layer Testing

Do not reference or instantiate:

```text
Application
Infrastructure
Worker
DI container
IResearchUseCase
IObservationSource
DeterministicObservationSource
```

Domain.Tests should remain a pure Domain behavioral suite.

---

# 5. Required Test Areas

WP09 must cover at least the following behaviors.

## 5.1 PriceObservation — Valid Construction

Test that a positive decimal price and valid `DateTimeOffset` produce a usable observation with expected values.

## 5.2 PriceObservation — Zero Price Rejected

Verify the approved exception/invalid behavior.

Do not weaken the Domain invariant to make the test pass.

## 5.3 PriceObservation — Negative Price Rejected

Verify the approved exception/invalid behavior.

## 5.4 ObservationSeries — Single Observation Valid

A one-item series is valid.

Verify the exposed series contains the approved observation.

## 5.5 ObservationSeries — Empty Rejected

Verify an empty sequence is rejected.

## 5.6 ObservationSeries — Duplicate Timestamp Rejected

Two observations at the same instant must be rejected even when prices differ.

## 5.7 ObservationSeries — Out-of-Order Rejected

A decreasing timestamp sequence must be rejected.

The Domain must not silently sort it.

## 5.8 ObservationSeries — Ordered Sequence Preserved

A valid ordered sequence should remain observably ordered.

Do not assert private storage type.

## 5.9 Arithmetic Mean — Canonical Scenario

Test:

```text
100.00
110.00
120.00
```

Expected:

```text
110.00
```

This is mandatory.

## 5.10 Arithmetic Mean — Uses Complete Series

Choose a deterministic case where using only a subset would produce a different result.

Prove all observations participate.

## 5.11 Arithmetic Mean — Single Observation

A one-item series mean equals that price.

## 5.12 MeanPrice — Exposes Calculated Value

Verify the Domain outcome exposes the expected decimal value.

Do not test Application metadata.

---

# 6. Optional Additional Tests

Additional tests are authorized only when they protect actual implementation semantics already approved by WP02/WP03.

Potential examples:

```text
decimal precision behavior without arbitrary rounding
input enumeration snapshot semantics if publicly observable and intentional
null sequence handling if the constructor explicitly defines it
null observation handling if explicitly supported/rejected by implementation
```

Do not invent new Domain requirements.

If implementation behavior is undefined by authority, do not create a test that accidentally freezes an incidental behavior.

Record any optional test and its justification.

---

# 7. Test Naming

Follow repository naming conventions.

Test names should communicate:

```text
subject
condition
expected behavior
```

Examples of semantic style:

```text
Constructor_WhenPriceIsZero_Throws...
Constructor_WhenTimestampsAreOutOfOrder_Throws...
CalculateMeanPrice_WhenSeriesHasThreeObservations_ReturnsArithmeticMean
```

Use the repository's actual preferred naming style when defined.

---

# 8. Test Organization

Prefer small focused test files aligned to Domain concepts, for example:

```text
PriceObservationTests.cs
ObservationSeriesTests.cs
```

A separate `MeanPriceTests.cs` is optional only if it has meaningful independent behavior worth testing.

Do not create excessive test-class fragmentation.

Do not create generalized fixture/base-test frameworks.

---

# 9. Exception Assertions

Use the existing test framework's standard exception assertions.

Assert:

```text
exception type
```

and message/parameter only when message/parameter is part of an intentional contract already established by repository conventions.

Avoid brittle full-message assertions unless justified.

---

# 10. Authorized Scope

WP09 may:

- read all relevant authority;
- add Domain behavioral test source files;
- minimally adjust Domain.Tests project-local organization if required;
- use existing xUnit/test packages already configured;
- run Domain.Tests directly;
- run solution build;
- run Architecture.Tests;
- run canonical verification;
- inspect project/package references;
- report complete evidence.

Primary mutation boundary:

```text
tests/AIQuantTradingResearch.Domain.Tests/**
```

No production code mutation is expected.

---

# 11. Prohibited Scope

Do not:

- modify Domain production code unless a real blocking defect is discovered; if so, stop/report rather than silently fix under WP09;
- modify Application;
- modify Infrastructure;
- modify Worker;
- test Application orchestration;
- test Infrastructure adapter;
- test Worker output;
- modify Architecture.Tests;
- add packages;
- add project references beyond the existing Domain.Tests -> Domain test relationship;
- create mocks for nonexistent Domain dependencies;
- introduce AutoFixture/Faker/property-testing packages;
- introduce coverage tooling;
- create generic test frameworks/base classes;
- modify DI;
- modify GitHub planning;
- stage, commit, push, or create PR unless separately authorized;
- begin WP10.

---

# 12. Dependency Rules

Domain.Tests should depend only on what it requires to test Domain plus its existing test framework infrastructure.

Confirm no dependency on:

```text
Application
Infrastructure
Worker
```

Do not add cross-layer project references.

---

# 13. Working-Tree Protection

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
WP01–WP09 prompt/chat pairs
RESEARCH_DOMAIN_MODEL.md
WP03–WP08 source changes
WP07 DI modifications
WP08 Worker Program.cs
```

Preserve all pre-existing artifacts.

Do not clean, delete, or stage unrelated/pre-existing work.

---

# 14. Test Design Procedure

## Step 1 — Read Authority

Read plan, manifest, WP09 prompt, research model, Domain implementation, and testing strategy.

## Step 2 — Verify Starting State

Confirm:

```text
WP08 = WORKER EXECUTION COMPLETE
Domain.Tests currently discovers zero tests
Domain production implementation matches WP03 authority
```

## Step 3 — Build Behavior Matrix

Before coding, create an internal test matrix:

| Domain Concept | Behavior / Invariant | Test Case | Expected Result |
| --- | --- | --- | --- |

## Step 4 — Implement PriceObservation Tests

Cover valid positive price and zero/negative rejection.

## Step 5 — Implement ObservationSeries Invariant Tests

Cover:

```text
single valid observation
empty rejection
duplicate timestamp rejection
out-of-order rejection
ordered sequence preservation
```

## Step 6 — Implement Arithmetic Mean Tests

Cover:

```text
canonical 100/110/120 -> 110
complete-series usage
single observation
```

## Step 7 — Review for Incidental Behavior

Remove tests that merely freeze implementation details not required by authority.

## Step 8 — Run Domain.Tests Directly

Run the Domain test project directly.

Require:

```text
discovered tests > 0
failed = 0
```

Record exact discovered/passed/skipped counts.

## Step 9 — Mutation Safety Review

Confirm only Domain.Tests files were changed by WP09.

## Step 10 — Format / Build

Run formatting verification and full solution build.

Require zero build errors.

## Step 11 — Architecture Tests

Run Architecture.Tests.

Require all accepted rules pass.

## Step 12 — Canonical Verification

Run:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Require exit status 0.

At this point the canonical test orchestration should discover Domain.Tests in addition to Architecture.Tests.

## Step 13 — Dependency Inspection

Verify Domain.Tests has no Application/Infrastructure/Worker references.

## Step 14 — Final Git Inspection

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
git diff --check
```

Confirm exact WP09 scope.

---

# 15. Validation Evidence Required

Record objective evidence for:

```text
test files created
test count
passed/failed/skipped
price invariant tests
series invariant tests
canonical mean test
complete-series test
single-observation mean test
Domain.Tests project references
package references
solution build
Architecture.Tests
eng/verify.ps1
final Git scope
```

---

# 16. Quality Review

Before completion, answer:

```text
Does every test protect a real Domain requirement?
Are any tests testing Application/Infrastructure concerns?
Are any tests coupled to private implementation details?
Are any assertions unnecessarily brittle?
Do tests run without network/time/randomness?
Does the canonical scenario have a direct automated test now?
```

Expected answers:

```text
Yes
No
No
No
Yes
Yes
```

---

# 17. Decision Model

Return exactly one:

```text
DOMAIN TESTS COMPLETE
DOMAIN TESTS COMPLETE WITH ACTIONS
DOMAIN TESTS BLOCKED
```

Use `DOMAIN TESTS COMPLETE` when:

```text
Domain.Tests discovers meaningful tests
all required Domain invariants are protected
canonical mean scenario is automated
all Domain tests pass
no cross-layer testing exists
no production code mutation occurred
build/Architecture.Tests/verify pass
WP10 can safely proceed
```

Use `DOMAIN TESTS COMPLETE WITH ACTIONS` only for non-blocking observations.

Use `DOMAIN TESTS BLOCKED` for mandatory defects or authority conflicts.

---

# 18. Acceptance Criteria

WP09 passes only when:

- [ ] WP09 prompt read completely.
- [ ] Release 0.9 plan/manifest read completely.
- [ ] `RESEARCH_DOMAIN_MODEL.md` read completely.
- [ ] Domain implementation inspected.
- [ ] Testing strategy inspected.
- [ ] WP08 `WORKER EXECUTION COMPLETE` baseline confirmed.
- [ ] Initial Git state classified.
- [ ] Behavior matrix established before coding.
- [ ] Domain.Tests discovers more than zero tests.
- [ ] Valid `PriceObservation` construction tested.
- [ ] Zero price rejection tested.
- [ ] Negative price rejection tested.
- [ ] Single-observation series tested.
- [ ] Empty series rejection tested.
- [ ] Duplicate timestamp rejection tested.
- [ ] Out-of-order timestamp rejection tested.
- [ ] Valid ordering preservation tested.
- [ ] Canonical `100/110/120 -> 110.00` test exists.
- [ ] Complete-series mean behavior tested.
- [ ] Single-observation mean tested.
- [ ] MeanPrice value exposure tested where meaningful.
- [ ] All Domain.Tests pass.
- [ ] No Domain production code modified.
- [ ] No Application/Infrastructure/Worker mutation.
- [ ] No Application/Infrastructure/Worker project references added.
- [ ] No new test/package framework added.
- [ ] Tests deterministic/offline.
- [ ] No architecture tests modified.
- [ ] Build succeeds with zero errors.
- [ ] Architecture.Tests pass.
- [ ] `eng/verify.ps1` passes.
- [ ] `git diff --check` passes.
- [ ] Final Git state inspected.
- [ ] No GitHub planning mutation.
- [ ] WP10 not started.

---

# 19. Expected Output Contract

Return one complete:

```text
Release 0.9 WP09 Domain Tests Execution Report
```

Use this structure.

# Release 0.9 WP09 Domain Tests Execution Report

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
WP08 baseline:
```

## 3. Authoritative Sources Reviewed

List exact paths.

## 4. Domain Behavior Matrix

| Domain Concept | Behavior / Invariant | Test | Expected Result |
| --- | --- | --- | --- |

## 5. Test Source Changes

| Path | Change | Purpose |
| --- | --- | --- |

## 6. PriceObservation Tests

```text
Valid construction:
Zero price:
Negative price:
Assessment:
```

## 7. ObservationSeries Tests

```text
Single observation:
Empty:
Duplicate timestamp:
Out-of-order:
Ordering preserved:
Assessment:
```

## 8. Mean Calculation Tests

```text
Canonical 100/110/120:
Complete series:
Single observation:
MeanPrice value:
Assessment:
```

## 9. Test Quality Assessment

```text
Implementation-detail coupling:
Brittle message assertions:
External resources:
Clock/randomness:
Cross-layer concerns:
Assessment:
```

## 10. Domain.Tests Direct Execution

```text
Command:
Discovered:
Passed:
Failed:
Skipped:
Assessment:
```

## 11. Dependency Assessment

```text
Domain.Tests project references:
Application reference:
Infrastructure reference:
Worker reference:
Package changes:
Assessment:
```

## 12. Build / Architecture Validation

```text
Build:
Errors:
Architecture.Tests:
Cycles:
Assessment:
```

## 13. Canonical Verification

```text
Command:
Exit status:
Total tests observed:
Warnings:
Assessment:
```

## 14. Production-Code Protection

```text
Domain production modified:
Other production projects modified:
Assessment:
```

## 15. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 16. Final Git State

```text
WP09-owned changes:
Pre-existing changes:
Staged:
Tracked modifications:
Unexpected:
```

## 17. Scope Compliance

| Check | Result | Evidence |
| --- | --- | --- |
| Domain-tests-only scope | PASS/FAIL | |
| Meaningful behavioral tests | PASS/FAIL | |
| No production mutation | PASS/FAIL | |
| No cross-layer testing | PASS/FAIL | |
| No new dependencies/frameworks | PASS/FAIL | |
| Deterministic/offline | PASS/FAIL | |
| WP10 not started | PASS/FAIL | |

## 18. Findings

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 19. Acceptance Criteria Matrix

Reproduce applicable criteria with:

```text
PASS
FAIL
N/A
```

## 20. Final Decision

State exactly one:

```text
DOMAIN TESTS COMPLETE
DOMAIN TESTS COMPLETE WITH ACTIONS
DOMAIN TESTS BLOCKED
```

Explain why.

## 21. Next Authorized Work Package

If and only if progression is permitted:

```text
WP10 — Application Tests
```

Do not begin it.

---

# 20. Final Instruction

Execute Release 0.9 / WP09 — Domain Tests.

Create meaningful tests only for:

```text
PriceObservation
ObservationSeries
CalculateMeanPrice()
MeanPrice
```

Protect:

```text
positive price
empty-series rejection
duplicate timestamp rejection
strict ordering
canonical 100/110/120 -> 110.00
complete-series mean
single-observation mean
```

Do not test Application, Infrastructure, Worker, DI, or architecture rules.

Do not modify production code unless a genuine blocking defect is discovered; stop/report instead of silently fixing it.

Do not add packages or cross-layer references.

Run Domain.Tests directly, then build, run Architecture.Tests, run `eng/verify.ps1`, inspect dependencies/scope/Git state, and return the complete WP09 report.

Finish with exactly one:

```text
DOMAIN TESTS COMPLETE
DOMAIN TESTS COMPLETE WITH ACTIONS
DOMAIN TESTS BLOCKED
```

If complete, identify:

```text
WP10 — Application Tests
```

as next.

Do not execute WP10.

# Conclusion

WP09 converts the approved Domain behavior into executable specifications:

```text
WP03 Domain Model
      ↓
Price Invariant Tests
      ↓
Series Invariant Tests
      ↓
Mean Behavior Tests
      ↓
Deterministic Domain Suite
      ↓
DOMAIN TESTS COMPLETE
      ↓
WP10 Application Tests
```

> **Domain tests should protect the behavior and invariants the model promises, not the implementation details it happens to use.**
