# Codex Execution Prompt — Release 0.9 / WP11 Infrastructure Tests

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 11 — Infrastructure Tests |
| Type | Tests |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Prerequisite | WP10 — Application Tests = `APPLICATION TESTS COMPLETE` |
| Primary Authorities | Release 0.9 execution plan/file manifest, WP02 research model, WP04 source contract, WP06 deterministic adapter |
| Execution Mode | Narrowly scoped Infrastructure behavioral testing |
| Expected Outcome | Convert `AIQuantTradingResearch.Infrastructure.Tests` from an intentional skeleton into a deterministic behavioral suite for `DeterministicObservationSource`, proving exact fixture values, supported counts, unsupported/insufficient outcomes, repeatability, and offline behavior without testing Application orchestration, Worker, DI composition, or architecture rules |

---

# 1. Purpose

Execute:

```text
Phase 2 — Release 0.9
WP11 — Infrastructure Tests
```

WP11 creates the authoritative behavioral suite for the Release 0.9 Infrastructure adapter:

```text
DeterministicObservationSource
```

The suite must prove that the adapter correctly implements:

```text
IObservationSource
```

for the approved canonical fixture and source-result semantics.

WP11 does not test `ResearchUseCase`.

WP11 does not test Worker.

WP11 does not evolve Architecture.Tests.

Do not begin WP12.

---

# 2. Authoritative Sources

Read completely before mutation:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/11-infrastructure-tests-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Inspect current Application-owned source contract:

```text
src/AIQuantTradingResearch.Application/Research/IObservationSource.cs
src/AIQuantTradingResearch.Application/Research/ObservationSourceResult.cs
src/AIQuantTradingResearch.Application/Research/ObservationSourceFailure.cs
src/AIQuantTradingResearch.Application/Research/ResearchRequest.cs
```

Inspect current Infrastructure adapter:

```text
src/AIQuantTradingResearch.Infrastructure/Research/DeterministicObservationSource.cs
```

Inspect current Infrastructure test project and guidance:

```text
tests/AIQuantTradingResearch.Infrastructure.Tests/**
docs/architecture/implementation/TESTING_STRATEGY.md
docs/architecture/implementation/CODING_PRINCIPLES.md
docs/architecture/implementation/NAMING_CONVENTIONS.md
```

Use existing xUnit/test packages.

Do not add a new framework.

---

# 3. Accepted WP06 Adapter Semantics

The canonical adapter:

```text
DeterministicObservationSource
```

implements:

```text
IObservationSource
```

Canonical target:

```text
SAMPLE-USD
```

Canonical observations:

```text
2024-01-01T00:00:00+00:00 -> 100.00
2024-01-02T00:00:00+00:00 -> 110.00
2024-01-03T00:00:00+00:00 -> 120.00
```

Target policy:

```text
ordinal exact match
sample-usd is unsupported
no normalization
```

Count behavior:

```text
count 1 -> first observation
count 2 -> first two observations
count 3 -> all three observations
count > 3 -> InsufficientObservations
zero/negative direct call -> InsufficientObservations
```

Unsupported target:

```text
UnsupportedTarget
```

Expected source failures use explicit `ObservationSourceResult`/`ObservationSourceFailure`, not exceptions.

Determinism:

```text
no network
no clock
no randomness
no credentials
no environment variables
no filesystem
no database
```

---

# 4. Testing Principles

## 4.1 Test the Concrete Adapter Directly

WP11 is specifically the Infrastructure behavioral suite.

Instantiate the concrete:

```text
DeterministicObservationSource
```

directly if accessible under the authorized testability boundary.

Do not go through `ResearchUseCase`.

Do not go through Worker.

Do not use the DI container unless a narrow testability blocker requires separate authorization.

## 4.2 Verify Contract Behavior

Assert:

```text
success/failure result
failure category
returned observation count
returned sequence values
returned sequence ordering
repeatability
```

Do not assert private fixture-storage implementation.

## 4.3 Deterministic / Offline

Tests must remain:

```text
offline
fixed-data
clock-independent
randomness-free
culture-safe
```

No external service is permitted.

## 4.4 No Application-Orchestration Testing

`ResearchRequest` may be used because it is part of the Application-owned source contract.

Do not test Application request validation here.

For example, zero/negative direct-call behavior is adapter behavior only; do not reinterpret it as Application `InvalidRequest`.

---

# 5. Required Test Areas

WP11 must cover at least:

## 5.1 Count 1 Success

Request:

```text
SAMPLE-USD / 1
```

Expected exact observation:

```text
2024-01-01T00:00:00+00:00 -> 100.00
```

## 5.2 Count 2 Success

Expected exact ordered values:

```text
2024-01-01T00:00:00+00:00 -> 100.00
2024-01-02T00:00:00+00:00 -> 110.00
```

## 5.3 Count 3 Success

Expected exact ordered values:

```text
100.00
110.00
120.00
```

with canonical timestamps.

## 5.4 Count Above Capacity

Request:

```text
SAMPLE-USD / 4
```

Expected:

```text
ObservationSourceFailure.InsufficientObservations
```

No observations should be exposed as success.

## 5.5 Unsupported Target

Request:

```text
UNKNOWN / 1
```

Expected:

```text
ObservationSourceFailure.UnsupportedTarget
```

## 5.6 Case Sensitivity / No Normalization

Request:

```text
sample-usd / 1
```

Expected:

```text
UnsupportedTarget
```

Protect the intentional exact-match behavior.

## 5.7 Zero Direct Count

Direct adapter call with:

```text
SAMPLE-USD / 0
```

Expected adapter behavior:

```text
InsufficientObservations
```

This is not Application request-validation behavior.

## 5.8 Negative Direct Count

Direct adapter call with negative count.

Expected:

```text
InsufficientObservations
```

## 5.9 Repeatability

Call the same valid request more than once.

Assert equivalent:

```text
result kind
observation count
timestamps
prices
order
```

## 5.10 Exact Canonical Values

Protect the canonical fixture itself.

Do not merely test count.

At least one test must explicitly verify all three exact timestamps and prices.

## 5.11 No Over-Return

For supported counts 1 and 2, prove exactly the requested prefix is returned.

The adapter must not return all three and rely on Application truncation.

---

# 6. Optional Additional Tests

Only add tests for already-approved adapter behavior.

Potential examples:

```text
result collection cannot be externally mutated if that is publicly observable
same request returns value-equivalent but safely isolated result collections if applicable
```

Do not freeze implementation details such as:

```text
ReadOnlyCollection concrete type
static field name
private fixture layout
```

unless those details are part of the public contract—which they are not.

---

# 7. Testability Boundary

Before coding, determine whether `DeterministicObservationSource` is accessible to Infrastructure.Tests.

If it is internal and there is no authorized friend-assembly access:

1. stop;
2. report the blocker;
3. do not use reflection;
4. do not use DI as a workaround;
5. do not make the implementation public;
6. do not silently change production visibility.

A narrow explicit friend-assembly unblock may be proposed, analogous to the authorized WP10 unblock, but must be separately authorized.

Do not contaminate WP11 with an unapproved production testability change.

---

# 8. Test Naming / Organization

Follow repository conventions.

Prefer:

```text
DeterministicObservationSourceTests.cs
```

Use descriptive analyzer-clean test names.

Avoid generic base test classes, fixtures, and mock frameworks.

---

# 9. Authorized Scope

WP11 may:

- read all relevant authority;
- add Infrastructure behavioral test source files;
- use existing xUnit infrastructure;
- directly instantiate the concrete adapter if authorized/accessibile;
- use `ResearchRequest`/source result contracts;
- run Infrastructure.Tests directly;
- run Domain.Tests and Application.Tests regression;
- run solution build;
- run Architecture.Tests;
- run canonical verification;
- inspect project/package references;
- report complete evidence.

Primary mutation boundary:

```text
tests/AIQuantTradingResearch.Infrastructure.Tests/**
```

No production mutation is expected.

---

# 10. Prohibited Scope

Do not:

- modify Infrastructure production code unless a real blocking defect is discovered; stop/report rather than silently fixing;
- modify Application production code;
- modify Domain;
- modify Worker;
- test `ResearchUseCase`;
- use Worker/Host;
- use DI container for core adapter tests;
- modify Architecture.Tests;
- add packages;
- add Worker project reference;
- add new mocking/fixture/coverage frameworks;
- add HTTP/database/file/network resources;
- modify GitHub planning;
- stage, commit, push, or create PR unless separately authorized;
- begin WP12.

---

# 11. Dependency Rules

Infrastructure.Tests should depend only on the minimum existing projects required to test Infrastructure.

Inspect the actual current project references before changing anything.

Expected acceptable shape may include:

```text
Infrastructure
Application
```

depending on the existing project setup and transitive contract accessibility.

Do not add Worker.

Do not add unnecessary direct Domain references if current transitive access is sufficient.

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
WP01–WP11 prompt/chat pairs
RESEARCH_DOMAIN_MODEL.md
WP03–WP08 production changes
WP09 Domain tests
WP10 testability-unblock AssemblyInfo.cs
WP10 Application tests
```

Preserve all pre-existing artifacts.

Do not clean, delete, or stage unrelated/pre-existing work.

---

# 13. Test Design Procedure

## Step 1 — Read Authority

Read plan, manifest, WP11 prompt, research model, source contracts, adapter implementation, and testing strategy.

## Step 2 — Verify Starting State

Confirm:

```text
WP10 = APPLICATION TESTS COMPLETE
Infrastructure.Tests currently discovers zero tests
DeterministicObservationSource implementation matches WP06 authority
```

## Step 3 — Verify Testability

Confirm Infrastructure.Tests can directly access the adapter.

If not, stop with `INFRASTRUCTURE TESTS BLOCKED`.

## Step 4 — Build Infrastructure Behavior Matrix

Before coding:

| Scenario | Request | Expected Result | Exact Observations |
| --- | --- | --- | --- |

## Step 5 — Implement Supported-Count Tests

Cover counts 1, 2, and 3.

## Step 6 — Implement Unsupported / Insufficient Tests

Cover:

```text
count > capacity
unknown target
case-mismatch target
zero direct count
negative direct count
```

## Step 7 — Implement Exact Fixture Test

Assert all canonical timestamps/prices/order.

## Step 8 — Implement Repeatability Test

Execute equivalent requests multiple times and compare observable values.

## Step 9 — Review for Application Leakage

Confirm tests do not assert Application request-validation semantics or use `ResearchUseCase`.

## Step 10 — Run Infrastructure.Tests Directly

Require:

```text
discovered > 0
failed = 0
```

Record exact counts.

## Step 11 — Run Domain.Tests Regression

Require all WP09 tests pass.

## Step 12 — Run Application.Tests Regression

Require all WP10 tests pass.

## Step 13 — Format / Build

Run formatting verification and full solution build.

Require zero build errors.

## Step 14 — Architecture Tests

Run Architecture.Tests.

Require all accepted rules pass.

## Step 15 — Canonical Verification

Run:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Require exit status 0.

Canonical discovery should now include:

```text
Domain.Tests
Application.Tests
Infrastructure.Tests
Architecture.Tests
```

## Step 16 — Dependency Inspection

Confirm no Worker reference/package expansion.

## Step 17 — Final Git Inspection

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
git diff --check
```

Confirm exact WP11 scope.

---

# 14. Validation Evidence Required

Record:

```text
test file(s)
tests discovered/passed/failed/skipped
count 1 behavior
count 2 behavior
count 3 behavior
count > 3 behavior
unsupported target
case sensitivity
zero direct count
negative direct count
exact fixture values
repeatability
Infrastructure.Tests references
package changes
Domain.Tests regression
Application.Tests regression
Architecture.Tests
eng/verify.ps1
final Git scope
```

---

# 15. Quality Review

Before completion answer:

```text
Do tests instantiate ResearchUseCase?
Do tests use Worker/Host?
Do tests use DI container?
Do tests assert private fixture implementation?
Do tests use network/time/randomness?
Are exact canonical values protected?
Are count prefix semantics protected?
Are unsupported/insufficient outcomes protected?
Is repeatability protected?
```

Expected:

```text
No
No
No
No
No
Yes
Yes
Yes
Yes
```

---

# 16. Decision Model

Return exactly one:

```text
INFRASTRUCTURE TESTS COMPLETE
INFRASTRUCTURE TESTS COMPLETE WITH ACTIONS
INFRASTRUCTURE TESTS BLOCKED
```

Use `INFRASTRUCTURE TESTS COMPLETE` when:

```text
Infrastructure.Tests contains meaningful deterministic tests
all approved adapter behaviors are protected
tests directly exercise the concrete adapter
all tests pass
no production mutation occurred
no dependency expansion occurred
Domain/Application regressions pass
Architecture.Tests/verify pass
WP12 can safely proceed
```

Use `INFRASTRUCTURE TESTS COMPLETE WITH ACTIONS` only for non-blocking observations.

Use `INFRASTRUCTURE TESTS BLOCKED` for mandatory defects or testability conflicts.

---

# 17. Acceptance Criteria

WP11 passes only when:

- [ ] WP11 prompt read completely.
- [ ] Release 0.9 plan/manifest read completely.
- [ ] `RESEARCH_DOMAIN_MODEL.md` read completely.
- [ ] Source contracts inspected.
- [ ] Deterministic adapter inspected.
- [ ] Testing strategy inspected.
- [ ] WP10 `APPLICATION TESTS COMPLETE` baseline confirmed.
- [ ] Initial Git state classified.
- [ ] Infrastructure testability confirmed before coding.
- [ ] Infrastructure behavior matrix established.
- [ ] Infrastructure.Tests discovers more than zero tests.
- [ ] Count 1 exact observation tested.
- [ ] Count 2 exact prefix tested.
- [ ] Count 3 exact fixture tested.
- [ ] Count > 3 returns `InsufficientObservations`.
- [ ] Unknown target returns `UnsupportedTarget`.
- [ ] Case-mismatch target returns `UnsupportedTarget`.
- [ ] Zero direct count behavior tested.
- [ ] Negative direct count behavior tested.
- [ ] No over-return verified.
- [ ] Exact canonical timestamps verified.
- [ ] Exact canonical prices verified.
- [ ] Ordering verified.
- [ ] Repeatability verified.
- [ ] Expected failures do not use exceptions.
- [ ] No Application orchestration tested.
- [ ] No Worker/Host used.
- [ ] No DI container used for core tests.
- [ ] No Infrastructure production code modified.
- [ ] No other production code modified.
- [ ] No new package/framework added.
- [ ] No Worker project reference added.
- [ ] Tests deterministic/offline.
- [ ] Domain.Tests remain passing.
- [ ] Application.Tests remain passing.
- [ ] Architecture.Tests unchanged.
- [ ] Build succeeds with zero errors.
- [ ] Architecture.Tests pass.
- [ ] `eng/verify.ps1` passes.
- [ ] `git diff --check` passes.
- [ ] Final Git state inspected.
- [ ] No GitHub mutation.
- [ ] WP12 not started.

---

# 18. Expected Output Contract

Return one complete:

```text
Release 0.9 WP11 Infrastructure Tests Execution Report
```

Use this structure.

# Release 0.9 WP11 Infrastructure Tests Execution Report

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
WP10 baseline:
```

## 3. Authoritative Sources Reviewed

List exact paths.

## 4. Testability Assessment

```text
Adapter type:
Visibility:
Directly accessible from Infrastructure.Tests:
Friend-assembly boundary:
Assessment:
```

## 5. Infrastructure Behavior Matrix

| Scenario | Request | Expected Result | Exact Observations |
| --- | --- | --- | --- |

## 6. Test Source Changes

| Path | Change | Purpose |
| --- | --- | --- |

## 7. Supported Count Tests

```text
Count 1:
Count 2:
Count 3:
Exact prefix behavior:
Assessment:
```

## 8. Failure Outcome Tests

```text
Count > capacity:
Unknown target:
Case mismatch:
Zero direct count:
Negative direct count:
Exception-based expected flow:
Assessment:
```

## 9. Canonical Fixture Tests

```text
Timestamps:
Prices:
Ordering:
Mutable/private implementation asserted:
Assessment:
```

## 10. Repeatability Test

```text
Request:
Runs:
Equivalent result kind:
Equivalent counts:
Equivalent timestamps/prices/order:
Assessment:
```

## 11. Test Quality Assessment

```text
ResearchUseCase:
Worker/Host:
DI container:
Private implementation assertions:
Network/time/randomness:
Cross-layer concerns:
Assessment:
```

## 12. Infrastructure.Tests Direct Execution

```text
Command:
Discovered:
Passed:
Failed:
Skipped:
Assessment:
```

## 13. Domain.Tests Regression

```text
Discovered:
Passed:
Failed:
Assessment:
```

## 14. Application.Tests Regression

```text
Discovered:
Passed:
Failed:
Assessment:
```

## 15. Dependency Assessment

```text
Infrastructure.Tests project references:
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
Infrastructure production modified:
Other production modified:
Assessment:
```

## 19. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 20. Final Git State

```text
WP11-owned changes:
Pre-existing changes:
Staged:
Tracked modifications:
Unexpected:
```

## 21. Scope Compliance

| Check | Result | Evidence |
| --- | --- | --- |
| Infrastructure-tests-only scope | PASS/FAIL | |
| Concrete adapter tested directly | PASS/FAIL | |
| No Application orchestration | PASS/FAIL | |
| No Worker/Host | PASS/FAIL | |
| No DI container for core tests | PASS/FAIL | |
| No production mutation | PASS/FAIL | |
| No dependency expansion | PASS/FAIL | |
| Deterministic/offline | PASS/FAIL | |
| WP12 not started | PASS/FAIL | |

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
INFRASTRUCTURE TESTS COMPLETE
INFRASTRUCTURE TESTS COMPLETE WITH ACTIONS
INFRASTRUCTURE TESTS BLOCKED
```

Explain why.

## 25. Next Authorized Work Package

If and only if progression is permitted:

```text
WP12 — Architecture Evolution
```

Do not begin it.

---

# 19. Final Instruction

Execute Release 0.9 / WP11 — Infrastructure Tests.

Create meaningful deterministic behavioral tests for:

```text
DeterministicObservationSource
```

Protect:

```text
SAMPLE-USD counts 1/2/3
exact canonical timestamps/prices/order
count > 3 -> InsufficientObservations
unknown target -> UnsupportedTarget
case mismatch -> UnsupportedTarget
zero/negative direct count behavior
no over-return
repeatability
```

Do not test `ResearchUseCase`.

Do not use Worker/Host.

Do not use DI container for core adapter tests.

If the internal adapter is not directly accessible, stop and report the testability blocker rather than using reflection, DI, or making it public.

Do not modify production code without separate authorization.

Run Infrastructure.Tests directly, Domain.Tests regression, Application.Tests regression, full build, Architecture.Tests, `eng/verify.ps1`, inspect dependencies/scope/Git state, and return the complete WP11 report.

Finish with exactly one:

```text
INFRASTRUCTURE TESTS COMPLETE
INFRASTRUCTURE TESTS COMPLETE WITH ACTIONS
INFRASTRUCTURE TESTS BLOCKED
```

If complete, identify:

```text
WP12 — Architecture Evolution
```

as next.

Do not execute WP12.

# Conclusion

WP11 turns the deterministic Infrastructure adapter into an executable contract:

```text
DeterministicObservationSource
        ↓
Exact Fixture Tests
        ↓
Count Semantics
        ↓
Failure Outcomes
        ↓
Repeatability
        ↓
INFRASTRUCTURE TESTS COMPLETE
        ↓
WP12 Architecture Evolution
```

> **Infrastructure tests should prove that the adapter honors the Application-owned contract deterministically without dragging Application orchestration into the test boundary.**
