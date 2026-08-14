# Codex Execution Prompt — Release 0.9 / WP03 Research Domain Model

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 03 — Research Domain Model |
| Type | Implementation |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Prerequisite | WP02 — Research Domain Discovery = `DISCOVERY COMPLETE` |
| Primary Authority | `docs/architecture/research/RESEARCH_DOMAIN_MODEL.md` |
| Execution Mode | Narrowly scoped Domain implementation |
| Expected Outcome | Implement only the approved Release 0.9 Domain concepts, invariants, and arithmetic-mean behavior without starting Application, Infrastructure, Worker, or behavioral-test work |

---

# 1. Purpose

Execute:

```text
Phase 2 — Release 0.9
WP03 — Research Domain Model
```

WP03 is the first Release 0.9 production-code work package.

Its purpose is to translate the approved WP02 research-domain authority into the smallest possible implementation inside:

```text
src/AIQuantTradingResearch.Domain/
```

WP03 must implement only the Domain concepts approved by WP02:

```text
price observation
observation series
arithmetic-mean behavior
mean-price outcome
```

Do not redesign the domain while coding it.

Do not begin WP04.

---

# 2. Authoritative Sources

Read completely before mutation:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/03-research-domain-model-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Also inspect relevant current repository guidance:

```text
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/design/DESIGN_PRINCIPLES.md
docs/architecture/design/ERROR_HANDLING.md
docs/architecture/implementation/**
Directory.Build.props
Directory.Packages.props
src/AIQuantTradingResearch.Domain/**
tests/AIQuantTradingResearch.Architecture.Tests/**
```

The WP02 artifact is authoritative for Release 0.9 domain semantics.

If broader planned documentation conflicts with the narrower WP02 decision, do not silently broaden WP03.

---

# 3. Accepted WP02 Domain Decisions

The approved reference operation is:

```text
Calculate the arithmetic mean of a valid ordered series
of positive timestamped price observations.
```

Approved Domain ownership:

```text
Price observation
Observation series
Arithmetic-mean behavior
Mean-price outcome
```

Application-owned concepts are not Domain concepts:

```text
research target key
requested count
research request/result
use-case orchestration
observation-source abstraction
unsupported-target outcome
insufficient-observations outcome
```

Do not move these into Domain.

---

# 4. Required Domain Semantics

## 4.1 Price Observation

Represent one observation with:

```text
absolute offset-aware instant
positive decimal price
```

Required invariants:

```text
instant must represent an unambiguous absolute point in time
price > 0
```

Invalid construction must be rejected according to repository error-handling conventions.

Do not add:

```text
symbol
provider
exchange
OHLC
volume
currency hierarchy
metadata
database identity
```

unless the WP02 authority explicitly requires it.

## 4.2 Observation Series

Represent a valid series of observations.

Required invariants:

```text
at least one observation
timestamps unique
timestamps strictly increasing
```

The series owns these sequence invariants.

Do not silently sort invalid input if doing so would hide an invariant violation.

Do not permit partial/invalid series state.

## 4.3 Arithmetic Mean

Implement the approved Domain behavior:

```text
mean = sum(all prices) / observation count
```

Requirements:

```text
use every item in the valid series
use decimal arithmetic
deterministic
no rounding policy beyond what the approved model requires
no external dependencies
```

Do not implement indicators, moving averages, strategies, backtests, or generalized analytics frameworks.

## 4.4 Mean-Price Outcome

Represent the Domain outcome of the arithmetic-mean operation.

It must contain only Domain meaning.

Do not add Application metadata such as:

```text
target
requested count
provider
status
error code
execution metadata
```

---

# 5. Canonical Reference Scenario

The implementation must be capable of representing and calculating:

```text
2024-01-01T00:00:00+00:00 -> 100.00
2024-01-02T00:00:00+00:00 -> 110.00
2024-01-03T00:00:00+00:00 -> 120.00

Expected mean:
110.00
```

This scenario is evidence for semantics, not authorization to implement Application orchestration or Infrastructure fixtures.

---

# 6. Invalid Domain Cases

The Domain implementation must make these states invalid:

```text
zero price
negative price
non-absolute / ambiguous timestamp where applicable to the selected .NET type
empty observation series
duplicate timestamp
out-of-order timestamp
```

Use the smallest implementation consistent with existing repository conventions.

Do not create a generalized validation framework.

Do not create a generalized Result/Error framework.

---

# 7. Naming Discipline

Use names that faithfully express the WP02 ubiquitous language.

Before creating files/types:

1. inspect `RESEARCH_DOMAIN_MODEL.md`;
2. inspect repository naming conventions;
3. choose the smallest clear names;
4. document any implementation-name mapping in the execution report.

Do not rename the conceptual model merely to imitate common trading libraries.

---

# 8. Design Discipline

Prefer:

```text
small immutable domain types
explicit invariants
behavior near the data/concept that owns it
minimal public API
no unnecessary inheritance
no framework dependencies
```

Avoid:

```text
base Entity
AggregateRoot
IDomainService
repository abstractions
domain events
factories without necessity
specification pattern
generic value-object frameworks
reflection-based machinery
dependency injection inside Domain
```

No forced DDD ceremony is authorized.

---

# 9. Dependency Rules

The Domain project must remain:

```text
Domain -> none
```

WP03 must not add:

```text
ProjectReference
PackageReference
framework-specific dependency
Infrastructure dependency
Application dependency
Worker dependency
```

Existing SDK/framework references required by .NET are not architectural dependencies.

---

# 10. Authorized Scope

WP03 may:

- read all relevant authority;
- create the minimum Domain source files required by the approved model;
- modify the Domain project only if strictly required for the approved implementation and already authorized by the Release 0.9 manifest;
- use .NET BCL types;
- run build/format/test/architecture verification;
- inspect generated compiler/build output;
- return the complete WP03 execution report.

Primary mutation boundary:

```text
src/AIQuantTradingResearch.Domain/**
```

Only files necessary for the four approved Domain responsibilities are authorized.

---

# 11. Prohibited Scope

Do not:

- modify Application implementation;
- modify Infrastructure implementation;
- modify Worker implementation;
- create behavioral tests;
- modify Architecture.Tests unless the authoritative WP03 scope explicitly requires it;
- create Application contracts;
- create observation-source interfaces;
- create deterministic fixture adapters;
- register DI;
- invoke the research flow from Worker;
- add project references;
- add NuGet packages;
- implement persistence;
- implement HTTP/network access;
- implement real market-data providers;
- implement plugins;
- implement strategies/backtesting;
- implement AI/ML;
- modify CI;
- modify unrelated documentation;
- modify GitHub planning;
- stage, commit, push, or create a PR unless explicitly authorized by the authoritative Release 0.9 execution plan;
- begin WP04.

---

# 12. Pre-Existing Governance / Working-Tree Protection

Before mutation record:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Classify every pre-existing change.

Expected intentional untracked governance may include WP01/WP02/WP03 prompt and prompt-chat files plus the WP02 research artifact if they have not yet been integrated.

Do not delete, overwrite, stage, or modify those files unless this work package explicitly authorizes doing so.

The repository convention remains:

```text
*-codex-prompt.md
*-codex-prompt-chat.md
```

Both are intentional artifacts.

---

# 13. Implementation Procedure

## Step 1 — Read Authority

Read the Release 0.9 execution plan, manifest, WP03 prompt, and `RESEARCH_DOMAIN_MODEL.md` completely.

## Step 2 — Verify Starting State

Confirm:

```text
WP02 = DISCOVERY COMPLETE
technical baseline healthy
Domain currently contains no Release 0.9 implementation
```

If conflicting Domain implementation already exists, stop and classify it before changing anything.

## Step 3 — Derive Exact Domain File Plan

Before writing code, produce an internal implementation mapping:

| WP02 Concept | Proposed Type | Proposed File | Responsibility |
| --- | --- | --- | --- |

Keep it minimal.

## Step 4 — Implement Price Observation

Implement the approved timestamp/price semantics and invariants.

## Step 5 — Implement Observation Series

Implement non-empty, unique, strictly increasing observation semantics.

## Step 6 — Implement Arithmetic Mean

Implement deterministic decimal mean over the complete valid series.

## Step 7 — Implement Mean-Price Outcome

Implement only the approved Domain outcome semantics.

## Step 8 — Review Public Surface

Verify no Application/Infrastructure/Future concepts leaked into Domain.

## Step 9 — Format Validation

Run the repository formatting workflow or equivalent approved check.

## Step 10 — Build

Build the solution.

Require:

```text
0 build errors
```

## Step 11 — Architecture Validation

Run Architecture.Tests.

Expected accepted baseline remains passing.

## Step 12 — Canonical Verification

Run:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Require exit status 0.

Known NU1900 vulnerability-feed connectivity warnings may be recorded as environmental observations when verification succeeds.

## Step 13 — Dependency Inspection

Confirm Domain has no project/package dependencies introduced by WP03.

## Step 14 — Final Scope Inspection

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
git diff --check
```

Distinguish pre-existing files from WP03-owned changes.

---

# 14. Validation Evidence Required

Record objective evidence for:

```text
Domain source inventory before/after
implemented type inventory
public API/surface assessment
Domain project references
Domain package references
solution build
Architecture.Tests
canonical verification
final Git delta
```

Do not claim behavior was unit-tested during WP03 if WP03 does not authorize behavioral tests.

Compile/build/architecture validation is not a substitute for later WP09 Domain behavioral tests; state this accurately.

---

# 15. Scope-Leakage Review

Search the WP03 Domain delta for concepts such as:

```text
target
provider
adapter
request
application result
repository
storage
HTTP
database
strategy
backtest
plugin
AI
ML
Worker
DI
```

A textual match is not automatically a violation; inspect semantic ownership.

Any actual leakage must be removed before WP03 can complete.

---

# 16. Decision Model

Return exactly one:

```text
IMPLEMENTATION COMPLETE
IMPLEMENTATION COMPLETE WITH ACTIONS
IMPLEMENTATION BLOCKED
```

Use `IMPLEMENTATION COMPLETE` when:

```text
all approved Domain concepts implemented
all required invariants represented
arithmetic mean behavior implemented
no prohibited scope introduced
Domain remains dependency-free
build/architecture/canonical verification pass
WP04 can safely proceed
```

Use `IMPLEMENTATION COMPLETE WITH ACTIONS` only for non-blocking observations that do not affect WP04.

Use `IMPLEMENTATION BLOCKED` when the Domain cannot be implemented safely within authority.

---

# 17. Acceptance Criteria

WP03 passes only when:

- [ ] WP03 prompt read completely.
- [ ] Execution plan and manifest read completely.
- [ ] `RESEARCH_DOMAIN_MODEL.md` read completely.
- [ ] WP02 `DISCOVERY COMPLETE` baseline confirmed.
- [ ] Initial Git state recorded/classified.
- [ ] Exact implementation mapping established before coding.
- [ ] Price-observation semantics implemented.
- [ ] Positive-price invariant enforced.
- [ ] Absolute/unambiguous timestamp semantics enforced as defined by the approved model.
- [ ] Observation-series semantics implemented.
- [ ] Empty series rejected.
- [ ] Duplicate timestamps rejected.
- [ ] Out-of-order timestamps rejected.
- [ ] Arithmetic mean uses complete valid series.
- [ ] Decimal arithmetic used.
- [ ] Mean-price Domain outcome implemented.
- [ ] Canonical 100/110/120 scenario is representable and yields 110 by the implemented behavior.
- [ ] No Application-owned target/count/request/result semantics leaked into Domain.
- [ ] No Infrastructure/Worker concerns leaked into Domain.
- [ ] No future-scope concepts introduced.
- [ ] No unnecessary DDD/framework abstractions introduced.
- [ ] Domain project references remain zero.
- [ ] No unauthorized package added.
- [ ] No Application/Infrastructure/Worker mutation.
- [ ] No behavioral tests created.
- [ ] No GitHub planning mutation.
- [ ] Build succeeds with zero errors.
- [ ] Architecture.Tests pass.
- [ ] Canonical verification passes.
- [ ] `git diff --check` passes.
- [ ] Final Git state inspected.
- [ ] WP04 not started.

---

# 18. Expected Output Contract

Return one complete:

```text
Release 0.9 WP03 Research Domain Model Execution Report
```

Use this structure.

# Release 0.9 WP03 Research Domain Model Execution Report

## 1. Executive Summary

```text
Release:
Work Package:
Objective:
Domain types created:
Domain behavior implemented:
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
WP02 baseline:
```

## 3. Authoritative Sources Reviewed

List exact paths.

## 4. Implementation Mapping

| WP02 Concept | Implemented Type | File | Responsibility |
| --- | --- | --- | --- |

## 5. Domain Source Changes

| Path | Change | Purpose |
| --- | --- | --- |

## 6. Price Observation

```text
Type:
Timestamp representation:
Price representation:
Invariants:
Invalid behavior:
Assessment:
```

## 7. Observation Series

```text
Type:
Collection semantics:
Empty behavior:
Duplicate behavior:
Ordering behavior:
Assessment:
```

## 8. Arithmetic Mean Behavior

```text
Location:
Arithmetic:
Uses complete series:
Deterministic:
Canonical expected result:
Assessment:
```

## 9. Mean-Price Outcome

```text
Type:
Domain meaning:
Application metadata present:
Assessment:
```

## 10. Public Surface Assessment

List public types/members introduced and justify necessity.

## 11. Dependency Assessment

```text
Project references:
Package references:
Framework dependencies:
Domain -> non-Domain dependencies:
Assessment:
```

## 12. Scope-Leakage Assessment

```text
Application concepts:
Infrastructure concepts:
Worker concepts:
Future concepts:
Generic framework/DDD ceremony:
Assessment:
```

## 13. Build / Architecture Validation

```text
Build:
Build errors:
Architecture.Tests:
Assessment:
```

## 14. Canonical Verification

```text
Command:
Exit status:
Warnings:
Assessment:
```

## 15. Behavioral-Test Boundary

State clearly that WP03 did not create the later Domain behavioral test suite and identify the later work package responsible according to Release 0.9 authority.

## 16. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 17. Final Git State

```text
WP03-owned changes:
Pre-existing changes:
Staged:
Tracked modifications:
Unexpected:
```

## 18. Scope Compliance

| Check | Result | Evidence |
| --- | --- | --- |
| Domain-only implementation | PASS/FAIL | |
| WP02 authority preserved | PASS/FAIL | |
| No Application implementation | PASS/FAIL | |
| No Infrastructure implementation | PASS/FAIL | |
| No Worker implementation | PASS/FAIL | |
| No behavioral tests | PASS/FAIL | |
| No dependency additions | PASS/FAIL | |
| No future-scope leakage | PASS/FAIL | |
| WP04 not started | PASS/FAIL | |

## 19. Findings

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed classifications:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 20. Acceptance Criteria Matrix

Reproduce applicable criteria with:

```text
PASS
FAIL
N/A
```

## 21. Final Decision

State exactly one:

```text
IMPLEMENTATION COMPLETE
IMPLEMENTATION COMPLETE WITH ACTIONS
IMPLEMENTATION BLOCKED
```

Explain why.

## 22. Next Authorized Work Package

If and only if progression is permitted, identify the exact WP04 title from the authoritative Release 0.9 execution plan.

Do not begin it.

---

# 19. Final Instruction

Execute Release 0.9 / WP03 — Research Domain Model.

Treat `docs/architecture/research/RESEARCH_DOMAIN_MODEL.md` as the semantic authority produced by WP02.

Implement only:

```text
price observation
observation series
arithmetic mean behavior
mean-price outcome
```

inside the Domain project.

Enforce only the approved invariants.

Keep Domain dependency-free.

Do not implement target/count request semantics, Application contracts/orchestration, Infrastructure adapters, DI, Worker execution, behavioral tests, or future research/trading capabilities.

Do not introduce generic DDD/framework infrastructure.

Build and validate the solution, run Architecture.Tests, run `eng/verify.ps1`, inspect dependencies and final Git scope, and return the complete WP03 execution report.

Finish with exactly one:

```text
IMPLEMENTATION COMPLETE
IMPLEMENTATION COMPLETE WITH ACTIONS
IMPLEMENTATION BLOCKED
```

If complete, identify the exact WP04 from the Release 0.9 execution plan as next.

Do not execute WP04.

# Conclusion

WP03 converts the discovery authority into the first executable Release 0.9 production behavior while preserving the architectural discipline established in Release 0.8.

The progression is:

```text
WP02 Discovery Authority
        ↓
Price Observation
        ↓
Observation Series
        ↓
Arithmetic Mean
        ↓
Mean-Price Outcome
        ↓
Domain Dependency Check
        ↓
Build + Architecture + Verify
        ↓
IMPLEMENTATION COMPLETE
        ↓
WP04
```

> **WP03 should make the discovered domain executable without allowing implementation convenience to expand the domain.**
