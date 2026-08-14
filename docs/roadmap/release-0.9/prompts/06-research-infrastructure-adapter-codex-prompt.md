# Codex Execution Prompt — Release 0.9 / WP06 Research Infrastructure Adapter

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 06 — Research Infrastructure Adapter |
| Type | Implementation |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Prerequisite | WP05 — Research Execution Use Case = `USE CASE COMPLETE` |
| Primary Authorities | `RESEARCH_DOMAIN_MODEL.md`, WP04 Application contracts, WP05 use case |
| Execution Mode | Narrowly scoped deterministic Infrastructure implementation |
| Expected Outcome | Implement one offline deterministic `IObservationSource` adapter that supports the canonical `SAMPLE-USD` fixture and explicit source outcomes without adding network, persistence, provider frameworks, DI registration, Worker behavior, or tests |

---

# 1. Purpose

Execute:

```text
Phase 2 — Release 0.9
WP06 — Research Infrastructure Adapter
```

WP06 provides the first concrete Infrastructure implementation required by the Release 0.9 research vertical slice.

It must implement the existing Application-owned:

```text
IObservationSource
```

using a deterministic, immutable, offline fixture.

WP06 does **not** own DI registration.

WP06 does **not** own Worker execution.

WP06 does **not** own Infrastructure behavioral tests.

Do not begin WP07.

---

# 2. Authoritative Sources

Read completely before mutation:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/06-research-infrastructure-adapter-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Inspect current Domain implementation:

```text
src/AIQuantTradingResearch.Domain/PriceObservation.cs
src/AIQuantTradingResearch.Domain/ObservationSeries.cs
src/AIQuantTradingResearch.Domain/MeanPrice.cs
```

Inspect current Application contracts and use case:

```text
src/AIQuantTradingResearch.Application/Research/IObservationSource.cs
src/AIQuantTradingResearch.Application/Research/ObservationSourceResult.cs
src/AIQuantTradingResearch.Application/Research/ObservationSourceFailure.cs
src/AIQuantTradingResearch.Application/Research/ResearchRequest.cs
src/AIQuantTradingResearch.Application/Research/ResearchUseCase.cs
```

Inspect current Infrastructure boundary and guidance:

```text
src/AIQuantTradingResearch.Infrastructure/**
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/PUBLIC_CONTRACTS.md
docs/architecture/implementation/NAMING_CONVENTIONS.md
docs/architecture/implementation/IMPLEMENTATION_GUIDELINES.md
```

WP06 must implement the existing Application contract rather than redesign it.

---

# 3. Accepted WP05 Baseline

Expected:

```text
ResearchUseCase exists
ResearchUseCase implements IResearchUseCase
ResearchUseCase depends only on IObservationSource
request validation implemented
source failures mapped
exact-count enforcement implemented
Domain arithmetic delegated
canonical orchestration harness passed
```

The Infrastructure adapter must satisfy those semantics.

---

# 4. Required Adapter Responsibility

Implement one concrete Infrastructure type that:

```text
implements IObservationSource
accepts ResearchRequest
supports the canonical SAMPLE-USD target
returns deterministic PriceObservation values
returns explicit ObservationSourceResult outcomes
requires no external resource
```

The adapter must be:

```text
offline
repeatable
immutable in behavior
credential-free
network-free
database-free
filesystem-persistence-free
clock-independent
randomness-free
provider-neutral
```

---

# 5. Canonical Deterministic Fixture

WP06 must support exactly the canonical Release 0.9 fixture:

```text
Target:
SAMPLE-USD

Available observations:
2024-01-01T00:00:00+00:00 -> 100.00
2024-01-02T00:00:00+00:00 -> 110.00
2024-01-03T00:00:00+00:00 -> 120.00
```

Expected behavior:

```text
SAMPLE-USD / count 1
  -> first approved deterministic observation set of size 1

SAMPLE-USD / count 2
  -> first approved deterministic observation set of size 2

SAMPLE-USD / count 3
  -> all three observations

SAMPLE-USD / count > 3
  -> InsufficientObservations

unsupported target
  -> UnsupportedTarget
```

The exact selection semantics must align with WP02/WP04/WP05 authority.

Do not introduce a time-range API.

Do not introduce a provider concept.

---

# 6. Target Matching Semantics

Use the smallest target-matching policy consistent with existing authority.

Expected canonical target:

```text
SAMPLE-USD
```

Do not silently add:

```text
aliases
case-insensitive provider mapping
symbol normalization framework
instrument lookup
exchange suffix parsing
```

If existing contract/guidance does not specify normalization, use the simplest deterministic exact-match policy and record it.

---

# 7. Observation Selection Semantics

The adapter must return observations in the canonical valid order.

It must not:

```text
randomize
sort malformed data at runtime
query external state
depend on current time
truncate an invalid internal fixture
return more observations than requested
```

For valid counts 1..3, return exactly the requested count.

For count > available observations, return:

```text
ObservationSourceFailure.InsufficientObservations
```

The Application use case owns invalid request validation; the adapter may assume positive count if the contract path guarantees it, but it must behave safely if called directly.

Record chosen direct-call behavior.

---

# 8. Fixture Ownership

Fixture data belongs in Infrastructure.

Do not place fixture data in:

```text
Domain
Application
Worker
tests
configuration
JSON files
database
filesystem
```

unless existing authority explicitly requires otherwise.

Prefer a small in-code immutable fixture if that is the simplest implementation.

Do not create a general fixture framework.

---

# 9. Contract Result Semantics

Use the existing:

```text
ObservationSourceResult
ObservationSourceFailure
```

Do not create parallel result types.

Do not throw exceptions for normal expected source outcomes such as:

```text
unsupported target
insufficient observations
```

Unexpected internal programming defects may still fail according to normal repository conventions.

---

# 10. Infrastructure Naming Discipline

Choose the smallest truthful name for the adapter.

Do not use a real provider/vendor name.

Avoid names implying broader capability than exists.

Examples of acceptable semantic direction:

```text
DeterministicObservationSource
InMemoryObservationSource
ReferenceObservationSource
```

The exact name must be justified against repository naming conventions and the WP02 model.

Record the mapping in the execution report.

---

# 11. Dependency Rules

Infrastructure must remain:

```text
Infrastructure -> Application
```

No new production dependency edge is authorized.

Do not add:

```text
Domain direct reference
Worker reference
HTTP package
database package
serialization package
file-system package
randomness/time abstraction package
```

Use existing references only.

If the Application contract exposes Domain types transitively, consume them through the existing Application contract without adding a direct Domain project reference unless already present and authoritative.

Inspect actual project references before and after.

---

# 12. Authorized Scope

WP06 may:

- read all relevant authority;
- add one minimal Infrastructure adapter implementation;
- add minimal private/static fixture representation inside Infrastructure;
- use existing Application contracts;
- use existing Domain value types only through the accepted dependency model;
- use a temporary validation harness removed afterward;
- build/format/test/verify;
- inspect project/package references;
- report complete evidence.

Primary mutation boundary:

```text
src/AIQuantTradingResearch.Infrastructure/**
```

Expected implementation area should follow existing project organization.

---

# 13. Prohibited Scope

Do not:

- modify Application contracts;
- modify ResearchUseCase;
- modify Domain;
- modify Worker;
- register DI;
- modify `DependencyInjection.cs` except if merely inspected;
- create Infrastructure behavioral tests;
- modify Architecture.Tests;
- add NuGet packages;
- add project references beyond accepted Infrastructure -> Application;
- create HTTP clients;
- call real APIs;
- use filesystem persistence;
- create database/repository abstractions;
- create provider selection/factory frameworks;
- implement cache/message bus;
- introduce randomness/current-time dependence;
- create plugin/strategy/backtest/AI abstractions;
- modify CI;
- modify GitHub planning;
- stage, commit, push, or create PR unless separately authorized;
- begin WP07.

---

# 14. Working-Tree Protection

Before mutation record:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Classify all pre-existing Release 0.9 artifacts.

Expected cumulative untracked files may include:

```text
WP01–WP06 prompt/chat pairs
RESEARCH_DOMAIN_MODEL.md
WP03 Domain source
WP04 Application contracts
WP05 ResearchUseCase
```

Preserve them.

Do not clean/delete/stage unrelated or pre-existing artifacts.

---

# 15. Implementation Procedure

## Step 1 — Read Authority

Read Release 0.9 plan, manifest, WP06 prompt, research model, WP04 contracts, WP05 use case, and current Infrastructure boundary.

## Step 2 — Verify Starting State

Confirm:

```text
WP05 = USE CASE COMPLETE
IObservationSource exists
no Infrastructure implementation exists yet
Infrastructure project references Application only
```

## Step 3 — Build Adapter Mapping

Before coding, record internally:

| Application Contract | Proposed Infrastructure Type | File | Responsibility |
| --- | --- | --- | --- |

## Step 4 — Implement Canonical Fixture

Create the fixed SAMPLE-USD observation set.

## Step 5 — Implement Adapter

Implement `IObservationSource`.

## Step 6 — Implement Expected Source Outcomes

Return:

```text
UnsupportedTarget
InsufficientObservations
Success
```

using existing contracts.

## Step 7 — Enforce Determinism

Confirm no network, clock, randomness, filesystem, database, or credentials are used.

## Step 8 — Review Public Surface

Prefer internal implementation visibility where later WP07 DI can still activate it according to the chosen registration approach.

Do not export unnecessary Infrastructure API.

## Step 9 — Temporary Adapter Harness

If useful, create a temporary harness to exercise:

```text
SAMPLE-USD / 1
SAMPLE-USD / 2
SAMPLE-USD / 3
SAMPLE-USD / 4
UNKNOWN / 1
repeat same request twice
```

Expected:

```text
same request -> same result
count 1 -> exactly 1 observation
count 2 -> exactly 2 observations
count 3 -> exactly 3 observations
count 4 -> InsufficientObservations
UNKNOWN -> UnsupportedTarget
```

Remove harness afterward.

Do not create WP11 tests.

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

## Step 13 — Dependency / Scope Inspection

Inspect:

```text
Infrastructure project references
Infrastructure package references
network/file/database APIs
provider/vendor terminology
random/time dependencies
```

## Step 14 — Final Git Inspection

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
git diff --check
```

Confirm exact WP06 scope.

---

# 16. Validation Evidence Required

Record objective evidence for:

```text
adapter type
implemented interface
fixture content
target matching policy
count selection policy
unsupported-target behavior
insufficient-observations behavior
repeatability
absence of external resources
project references
package references
build
Architecture.Tests
eng/verify.ps1
final Git scope
```

Do not claim WP11 behavioral-test coverage.

---

# 17. Determinism Review

Explicitly answer:

```text
Does the adapter read current time?
Does it use randomness?
Does it read environment variables?
Does it read files?
Does it use network?
Does it use credentials?
Does it depend on database state?
Can identical requests return different observations?
```

Expected answer:

```text
No
```

to all except the final phrasing, where identical requests must be equivalent.

---

# 18. Scope-Leakage Review

Search the WP06 delta for semantic leakage involving:

```text
HTTP
HttpClient
database
repository
EF Core
file
JSON persistence
provider factory
plugin
strategy
backtest
AI
ML
Worker
DI registration
clock
random
```

A textual occurrence in comments is not automatically a violation; inspect semantics.

Any actual leakage must be removed before completion.

---

# 19. Decision Model

Return exactly one:

```text
ADAPTER COMPLETE
ADAPTER COMPLETE WITH ACTIONS
ADAPTER BLOCKED
```

Use `ADAPTER COMPLETE` when:

```text
IObservationSource implemented
canonical fixture supported
exact requested counts returned for 1..3
unsupported target explicit
insufficient observations explicit
repeatability proven
no external resources/dependencies
Infrastructure -> Application preserved
no DI/Worker/tests leaked in
build/architecture/verify pass
WP07 can proceed
```

Use `ADAPTER COMPLETE WITH ACTIONS` only for non-blocking observations.

Use `ADAPTER BLOCKED` for mandatory defects or authority conflicts.

---

# 20. Acceptance Criteria

WP06 passes only when:

- [ ] WP06 prompt read completely.
- [ ] Release 0.9 plan/manifest read completely.
- [ ] `RESEARCH_DOMAIN_MODEL.md` read completely.
- [ ] WP04 contracts inspected.
- [ ] WP05 use case inspected.
- [ ] WP05 `USE CASE COMPLETE` baseline confirmed.
- [ ] Initial Git state classified.
- [ ] Adapter mapping established before coding.
- [ ] One concrete `IObservationSource` implementation created.
- [ ] Canonical `SAMPLE-USD` fixture created in Infrastructure.
- [ ] Fixture contains exactly approved 100/110/120 observations.
- [ ] Canonical timestamps exactly match authority.
- [ ] Supported count 1 returns exactly 1 observation.
- [ ] Supported count 2 returns exactly 2 observations.
- [ ] Supported count 3 returns exactly 3 observations.
- [ ] Count above available observations returns `InsufficientObservations`.
- [ ] Unsupported target returns `UnsupportedTarget`.
- [ ] Expected failures do not use exceptions as normal flow.
- [ ] Identical requests produce equivalent deterministic results.
- [ ] No network used.
- [ ] No credentials used.
- [ ] No database used.
- [ ] No filesystem persistence used.
- [ ] No current-time dependence.
- [ ] No randomness.
- [ ] No provider/vendor semantics.
- [ ] No generalized provider/fixture framework introduced.
- [ ] Public surface minimized.
- [ ] No Application/Domain/Worker mutation.
- [ ] No DI registration.
- [ ] No behavioral tests created.
- [ ] No package added.
- [ ] No unauthorized project reference added.
- [ ] Infrastructure dependency graph remains valid.
- [ ] Build succeeds with zero errors.
- [ ] Architecture.Tests pass.
- [ ] `eng/verify.ps1` passes.
- [ ] `git diff --check` passes.
- [ ] Final Git state inspected.
- [ ] No GitHub planning mutation.
- [ ] WP07 not started.

---

# 21. Expected Output Contract

Return one complete:

```text
Release 0.9 WP06 Research Infrastructure Adapter Execution Report
```

Use this structure.

# Release 0.9 WP06 Research Infrastructure Adapter Execution Report

## 1. Executive Summary

```text
Release:
Work Package:
Objective:
Adapter type:
Canonical target:
Available observations:
Dependencies introduced:
Technical validation:
Final decision:
```

## 2. Execution Context

```text
Repository:
Branch:
HEAD:
Initial Git state:
WP05 baseline:
```

## 3. Authoritative Sources Reviewed

List exact paths.

## 4. Adapter Mapping

| Application Contract | Implemented Infrastructure Type | File | Responsibility |
| --- | --- | --- | --- |

## 5. Infrastructure Source Changes

| Path | Change | Purpose |
| --- | --- | --- |

## 6. Adapter Design

```text
Type:
Implements:
Visibility:
State:
External dependencies:
Assessment:
```

## 7. Canonical Fixture

```text
Target:
Observations:
Storage form:
Mutable:
Assessment:
```

## 8. Target Matching

```text
Policy:
Supported target:
Unsupported behavior:
Normalization:
Assessment:
```

## 9. Observation Count Semantics

```text
Count 1:
Count 2:
Count 3:
Count > 3:
Zero/negative direct call:
Over-return possible:
Assessment:
```

## 10. Source Outcome Mapping

```text
Success:
Unsupported:
Insufficient:
Exception-based expected flow:
Assessment:
```

## 11. Determinism Assessment

```text
Network:
Clock:
Randomness:
Credentials:
Environment variables:
Filesystem:
Database:
Repeatability:
Assessment:
```

## 12. Public Surface Assessment

List public/internal types/members and justify visibility.

## 13. Dependency Assessment

```text
Infrastructure project references:
Package references:
Direct Domain dependency:
Worker dependency:
Assessment:
```

## 14. Scope-Leakage Assessment

```text
HTTP/network:
Persistence:
Provider framework:
DI registration:
Worker:
Future concepts:
Assessment:
```

## 15. Canonical Scenario Validation

```text
SAMPLE-USD / 1:
SAMPLE-USD / 2:
SAMPLE-USD / 3:
SAMPLE-USD / 4:
UNKNOWN / 1:
Repeated request equivalence:
Temporary harness retained:
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
Warnings:
Assessment:
```

## 18. Behavioral-Test Boundary

State that WP06 does not own WP11 Infrastructure behavioral tests and that temporary validation harnesses were removed.

## 19. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 20. Final Git State

```text
WP06-owned changes:
Pre-existing changes:
Staged:
Tracked modifications:
Unexpected:
```

## 21. Scope Compliance

| Check | Result | Evidence |
| --- | --- | --- |
| Infrastructure adapter only | PASS/FAIL | |
| Application contract preserved | PASS/FAIL | |
| Deterministic/offline | PASS/FAIL | |
| No DI registration | PASS/FAIL | |
| No Worker implementation | PASS/FAIL | |
| No behavioral tests | PASS/FAIL | |
| No dependency additions | PASS/FAIL | |
| No future-scope leakage | PASS/FAIL | |
| WP07 not started | PASS/FAIL | |

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
ADAPTER COMPLETE
ADAPTER COMPLETE WITH ACTIONS
ADAPTER BLOCKED
```

Explain why.

## 25. Next Authorized Work Package

If and only if progression is permitted:

```text
WP07 — Dependency Registration
```

Do not begin it.

---

# 22. Final Instruction

Execute Release 0.9 / WP06 — Research Infrastructure Adapter.

Implement exactly one deterministic offline Infrastructure adapter for the existing Application-owned `IObservationSource`.

Support the canonical `SAMPLE-USD` fixture:

```text
2024-01-01T00:00:00+00:00 -> 100.00
2024-01-02T00:00:00+00:00 -> 110.00
2024-01-03T00:00:00+00:00 -> 120.00
```

Return exactly requested counts 1..3.

Return `InsufficientObservations` above available count.

Return `UnsupportedTarget` for unsupported targets.

Do not use network, credentials, database, filesystem persistence, clock, randomness, or real provider semantics.

Do not register DI.

Do not modify Worker.

Do not create tests.

Do not add packages or production project references.

Validate determinism with a temporary harness if useful, remove it afterward, then build, run Architecture.Tests, run `eng/verify.ps1`, inspect dependencies/scope/Git state, and return the complete WP06 execution report.

Finish with exactly one:

```text
ADAPTER COMPLETE
ADAPTER COMPLETE WITH ACTIONS
ADAPTER BLOCKED
```

If complete, identify:

```text
WP07 — Dependency Registration
```

as next.

Do not execute WP07.

# Conclusion

WP06 supplies the first concrete external-boundary implementation while preserving Application ownership and complete offline determinism.

The progression is:

```text
WP05 Use Case
      ↓
IObservationSource
      ↓
Deterministic SAMPLE-USD Fixture
      ↓
Explicit Source Outcomes
      ↓
Repeatability + Dependency Validation
      ↓
ADAPTER COMPLETE
      ↓
WP07 Dependency Registration
```

> **Infrastructure should satisfy an Application need concretely without forcing the Application to know how that need is fulfilled.**
