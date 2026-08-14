# Codex Execution Prompt — Release 0.9 / WP12 Architecture Evolution

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 12 — Architecture Evolution |
| Type | Architecture Tests / Governance |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Prerequisite | WP11 — Infrastructure Tests = `INFRASTRUCTURE TESTS COMPLETE` |
| Primary Authorities | Release 0.9 execution plan/file manifest, Release 0.8 dependency rules, WP02 research model, WP03–WP11 implemented boundaries |
| Execution Mode | Narrowly scoped architecture-rule evolution and validation |
| Expected Outcome | Evolve `AIQuantTradingResearch.Architecture.Tests` only as needed to make the Release 0.9 architectural boundaries executable, preserving the accepted production graph and preventing cross-layer leakage without changing production behavior |

---

# 1. Purpose

Execute:

```text
Phase 2 — Release 0.9
WP12 — Architecture Evolution
```

WP12 converts the architectural decisions proven by WP03–WP11 into executable architecture safeguards.

The existing Release 0.8 architecture suite already protects the baseline dependency graph.

WP12 must determine which **new Release 0.9 rules** are materially valuable and add only those rules.

This is not a production implementation work package.

Do not modify Domain, Application, Infrastructure, or Worker behavior.

Do not begin WP13.

---

# 2. Authoritative Sources

Read completely before mutation:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/12-architecture-evolution-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Inspect current architecture authority:

```text
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/PUBLIC_CONTRACTS.md
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/TESTING_STRATEGY.md
```

Inspect all Release 0.9 production/test implementation relevant to boundaries:

```text
src/AIQuantTradingResearch.Domain/**
src/AIQuantTradingResearch.Application/**
src/AIQuantTradingResearch.Infrastructure/**
src/AIQuantTradingResearch.Worker/**

tests/AIQuantTradingResearch.Domain.Tests/**
tests/AIQuantTradingResearch.Application.Tests/**
tests/AIQuantTradingResearch.Infrastructure.Tests/**
tests/AIQuantTradingResearch.Architecture.Tests/**
```

The actual implemented Release 0.9 architecture is evidence.
Do not invent rules for future scope.

---

# 3. Accepted Production Dependency Graph

Preserve exactly:

```text
Domain          -> none
Application     -> Domain
Infrastructure  -> Application
Worker          -> Application + Infrastructure
```

Required:

```text
Cycles = 0
No new production project
No new production project reference
No direct Worker -> Domain
No Infrastructure -> Worker
No Application -> Infrastructure
No Domain -> Application/Infrastructure/Worker
```

Existing Release 0.8 tests may already enforce some or all of these rules.

Do not duplicate rules merely to increase test count.

---

# 4. Release 0.9 Architectural Decisions to Evaluate

WP12 must evaluate whether executable architecture tests should protect the following implemented decisions.

## 4.1 Domain Independence

Potential rule:

```text
Domain must not depend on Application, Infrastructure, or Worker.
```

Add only if not already adequately protected.

## 4.2 Application Independence from Infrastructure

Potential rule:

```text
Application must not depend on Infrastructure or Worker.
```

This protects the Application-owned `IObservationSource` abstraction and use-case boundary.

## 4.3 Infrastructure Must Not Depend on Worker

Potential rule:

```text
Infrastructure may depend on Application but not Worker.
```

## 4.4 Worker Must Not Depend Directly on Domain

Potential rule:

```text
Worker may compose Application + Infrastructure,
but must not take a direct Domain project dependency.
```

This is a material Release 0.9 boundary.

## 4.5 Internal Implementation Visibility

Evaluate whether architecture tests should protect implementation types such as:

```text
ResearchUseCase
DeterministicObservationSource
```

remaining non-public.

Do not encode brittle exact type lists unless the rule is clearly valuable and stable.

A preferred semantic rule may be:

```text
implementation types behind public abstractions should remain internal
```

only if it can be expressed robustly.

If not, record as observation rather than forcing a brittle test.

## 4.6 Application-Owned Abstraction

Evaluate whether architecture tests can meaningfully protect:

```text
IObservationSource belongs to Application
```

without hard-coding fragile file paths.

If the rule cannot be expressed robustly with the existing architecture-test tooling, do not force it.

## 4.7 Worker Thinness

Evaluate whether architecture tests can robustly prove Worker does not reference:

```text
ObservationSeries
MeanPrice
DeterministicObservationSource
IObservationSource
```

Do not add source-text grep tests masquerading as architecture tests unless repository conventions already authorize them.

Prefer dependency/type-level rules.

## 4.8 Test Project Isolation

Evaluate whether architecture tests should protect:

```text
Domain.Tests -> Domain only
Application.Tests -> Application (and accepted transitive Domain access)
Infrastructure.Tests -> Infrastructure
Architecture.Tests -> architecture inspection dependencies
```

Only add rules if test-project references are in scope for the existing architecture suite and the Release 0.9 execution plan expects them.

Do not over-expand WP12.

---

# 5. Architecture-Test Design Principles

## 5.1 Protect Stable Boundaries

Good architecture tests protect:

```text
dependency direction
project/module isolation
public/internal boundary where robust
cycles
```

Avoid tests for:

```text
specific filenames
line counts
exact number of classes
incidental namespaces
temporary implementation names
```

unless those are explicit authority.

## 5.2 No Duplicate Assertions

Inspect all existing Architecture.Tests first.

Build a matrix:

| Architectural Rule | Already Covered? | Existing Test | Gap? | Action |
| --- | --- | --- | --- | --- |

Only add tests for real gaps.

## 5.3 Fail for Real Violations

Every new test must have a clear hypothetical violation it would catch.

Document:

```text
Rule
Example violation
Why harmful
How test detects it
```

If this cannot be articulated, do not add the test.

## 5.4 Avoid Source-Text Tests

Do not use simple file-content matching unless the architecture suite already intentionally uses it and no type/dependency alternative exists.

Prefer structural inspection.

---

# 6. Required WP12 Analysis

Before coding, answer:

```text
Which Release 0.8 architecture rules already protect Release 0.9?
Which Release 0.9 boundaries are not yet executable?
Which missing rules are stable enough to automate?
Which potential rules would be brittle and should remain documentation-only?
```

The analysis itself is required evidence.

---

# 7. Minimum Expected Outcome

WP12 does **not** require a specific number of new tests.

Possible valid outcomes include:

```text
0 new tests, if all Release 0.9 boundaries are already adequately protected
1–N new tests, if specific material gaps exist
```

A zero-change outcome is acceptable only if objectively justified.

Do not add tests merely because the work package is named Architecture Evolution.

---

# 8. Authorized Scope

WP12 may:

- read all relevant authority;
- inspect existing Architecture.Tests;
- add or modify Architecture.Tests only where a Release 0.9 boundary gap is proven;
- minimally refactor architecture-test helpers if necessary to express a stable rule;
- run Architecture.Tests directly;
- run Domain/Application/Infrastructure behavioral regressions;
- run full build;
- run canonical verification;
- inspect production/test project references;
- report complete evidence.

Primary mutation boundary:

```text
tests/AIQuantTradingResearch.Architecture.Tests/**
```

No production mutation is expected.

---

# 9. Prohibited Scope

Do not:

- modify production code;
- modify Domain/Application/Infrastructure/Worker behavior;
- add project references to production projects;
- add packages unless explicitly required by existing architecture-test authority and separately justified;
- change WP09–WP11 behavioral tests except for a proven blocker that requires stop/report;
- create source-text grep tests without strong justification;
- create future-scope architecture rules;
- enforce naming/file-layout trivia;
- modify GitHub planning;
- stage, commit, push, or create PR unless separately authorized;
- begin WP13.

---

# 10. Working-Tree Protection

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
WP01–WP12 prompt/chat pairs
RESEARCH_DOMAIN_MODEL.md
WP03–WP08 production changes
WP09 Domain tests
WP10 Application testability boundary + tests
WP11 Infrastructure testability boundary + tests
```

Preserve all pre-existing artifacts.

Do not clean, delete, or stage unrelated/pre-existing work.

---

# 11. Architecture Evolution Procedure

## Step 1 — Read Authority

Read plan, manifest, WP12 prompt, research model, dependency rules, boundaries, DI guidance, and current architecture tests.

## Step 2 — Verify Starting State

Confirm:

```text
WP11 = INFRASTRUCTURE TESTS COMPLETE
Domain.Tests = 11/11
Application.Tests = 12/12
Infrastructure.Tests = 9/9
Architecture.Tests = current accepted baseline
eng/verify.ps1 = PASS
```

## Step 3 — Inventory Existing Architecture Tests

For each existing test record:

```text
test name
rule protected
projects/types involved
Release 0.9 relevance
```

## Step 4 — Build Gap Matrix

Create:

| Rule | Existing Coverage | Gap | Stable to Automate? | Decision |
| --- | --- | --- | --- | --- |

Evaluate at minimum:

```text
Domain independence
Application independence from Infrastructure/Worker
Infrastructure independence from Worker
Worker no direct Domain dependency
cycles
implementation visibility
Application-owned observation abstraction
Worker thinness
test project isolation
```

## Step 5 — Select Only Material New Rules

Add only rules with:

```text
clear Release 0.9 value
stable structural expression
non-duplicative coverage
clear violation scenario
```

## Step 6 — Implement Architecture Tests

Use existing architecture-test patterns/tooling.

Do not introduce a new architecture-testing framework.

## Step 7 — Negative Reasoning Review

For every new test, describe a hypothetical violating code/reference change that would cause failure.

## Step 8 — Direct Architecture Test Run

Run Architecture.Tests directly.

Require:

```text
failed = 0
```

Record exact discovered/passed count.

## Step 9 — Behavioral Regression

Run:

```text
Domain.Tests
Application.Tests
Infrastructure.Tests
```

Require all pass.

## Step 10 — Full Build

Require zero errors.

## Step 11 — Canonical Verification

Run:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Require exit status 0.

## Step 12 — Production/Test Reference Inspection

Confirm accepted production graph and no unauthorized test-project dependencies.

## Step 13 — Final Git Inspection

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
git diff --check
```

Confirm exact WP12 scope.

---

# 12. Validation Evidence Required

Record:

```text
existing architecture-test inventory
gap matrix
new/modified architecture tests
rule-to-test mapping
hypothetical violation examples
Architecture.Tests counts
Domain.Tests regression
Application.Tests regression
Infrastructure.Tests regression
production graph
test-project references
full build
eng/verify.ps1
final Git scope
```

---

# 13. Quality Review

Before completion answer:

```text
Does each new test protect a stable architecture boundary?
Does each new test catch a concrete harmful violation?
Does any new test duplicate an existing rule?
Does any new test encode file-layout trivia?
Does any new test depend on current implementation names unnecessarily?
Does any new test inspect source text instead of structure?
Did WP12 change production behavior?
```

Expected:

```text
Yes
Yes
No
No
No
No
No
```

If a robust rule cannot be expressed, prefer documenting it as a finding rather than implementing a brittle test.

---

# 14. Decision Model

Return exactly one:

```text
ARCHITECTURE EVOLUTION COMPLETE
ARCHITECTURE EVOLUTION COMPLETE WITH ACTIONS
ARCHITECTURE EVOLUTION BLOCKED
```

Use `ARCHITECTURE EVOLUTION COMPLETE` when:

```text
existing coverage was inventoried
Release 0.9 gaps were analyzed
all material stable gaps were automated
no brittle/duplicate rules added
all architecture/behavior/build/verify validation passes
WP13 can safely proceed
```

A zero-test-change result may still be `ARCHITECTURE EVOLUTION COMPLETE` when the gap analysis proves no material executable gap exists.

Use `ARCHITECTURE EVOLUTION COMPLETE WITH ACTIONS` only for non-blocking documentation/future observations.

Use `ARCHITECTURE EVOLUTION BLOCKED` for mandatory defects or tooling/authority conflicts.

---

# 15. Acceptance Criteria

WP12 passes only when:

- [ ] WP12 prompt read completely.
- [ ] Release 0.9 plan/manifest read completely.
- [ ] Research Domain Model read completely.
- [ ] Dependency/boundary/DI authority inspected.
- [ ] WP11 `INFRASTRUCTURE TESTS COMPLETE` baseline confirmed.
- [ ] Initial Git state classified.
- [ ] Existing Architecture.Tests inventoried.
- [ ] Existing Release 0.8 coverage mapped to Release 0.9.
- [ ] Gap matrix created.
- [ ] Domain independence evaluated.
- [ ] Application independence from Infrastructure/Worker evaluated.
- [ ] Infrastructure independence from Worker evaluated.
- [ ] Worker direct-Domain dependency evaluated.
- [ ] Cycle protection evaluated.
- [ ] Internal implementation visibility evaluated.
- [ ] Application-owned observation abstraction evaluated.
- [ ] Worker thinness evaluated.
- [ ] Test-project isolation evaluated.
- [ ] Every new test has a concrete violation scenario.
- [ ] No duplicate architecture rule added.
- [ ] No brittle file-layout/naming test added.
- [ ] No source-text grep test added without explicit justification.
- [ ] No new architecture framework/package added.
- [ ] Architecture.Tests pass.
- [ ] Domain.Tests remain passing.
- [ ] Application.Tests remain passing.
- [ ] Infrastructure.Tests remain passing.
- [ ] Production graph remains exact.
- [ ] Cycles remain zero.
- [ ] Build succeeds with zero errors.
- [ ] `eng/verify.ps1` passes.
- [ ] `git diff --check` passes.
- [ ] No production code modified.
- [ ] Final Git state inspected.
- [ ] No GitHub mutation.
- [ ] WP13 not started.

---

# 16. Expected Output Contract

Return one complete:

```text
Release 0.9 WP12 Architecture Evolution Execution Report
```

Use this structure.

# Release 0.9 WP12 Architecture Evolution Execution Report

## 1. Executive Summary

```text
Release:
Work Package:
Objective:
Existing architecture tests:
New/modified architecture tests:
Material gaps closed:
Technical validation:
Final decision:
```

## 2. Execution Context

```text
Repository:
Branch:
HEAD:
Initial Git state:
WP11 baseline:
```

## 3. Authoritative Sources Reviewed

List exact paths.

## 4. Existing Architecture Test Inventory

| Test | Rule Protected | Release 0.9 Relevance | Status |
| --- | --- | --- | --- |

## 5. Release 0.9 Architecture Gap Matrix

| Rule | Existing Coverage | Gap | Stable to Automate | Decision |
| --- | --- | --- | --- | --- |

## 6. Architecture Test Changes

| Path | Change | Rule | Reason |
| --- | --- | --- | --- |

If none:

```text
None — existing suite already covers all material stable Release 0.9 boundaries.
```

## 7. New Rule Justification

For each new/modified rule:

```text
Rule:
Hypothetical violation:
Why harmful:
Why existing tests were insufficient:
How new test detects it:
```

Use `N/A` when no new tests were required.

## 8. Brittle-Rule Rejections

List potential rules deliberately not automated and why.

Examples may include:

```text
exact internal implementation type names
source-text Worker thinness checks
file-layout rules
```

## 9. Architecture.Tests Direct Execution

```text
Command:
Discovered:
Passed:
Failed:
Skipped:
Assessment:
```

## 10. Behavioral Regression

```text
Domain.Tests:
Application.Tests:
Infrastructure.Tests:
Assessment:
```

## 11. Production Dependency Graph

```text
Domain:
Application:
Infrastructure:
Worker:
Cycles:
Assessment:
```

## 12. Test Project Dependency Assessment

```text
Domain.Tests:
Application.Tests:
Infrastructure.Tests:
Architecture.Tests:
Unexpected refs:
Assessment:
```

## 13. Build Validation

```text
Build:
Errors:
Assessment:
```

## 14. Canonical Verification

```text
Command:
Exit status:
Total tests observed:
Warnings:
Assessment:
```

## 15. Production-Code Protection

```text
Production modified:
Behavior changed:
Assessment:
```

## 16. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 17. Final Git State

```text
WP12-owned changes:
Pre-existing changes:
Staged:
Tracked modifications:
Unexpected:
```

## 18. Scope Compliance

| Check | Result | Evidence |
| --- | --- | --- |
| Architecture-tests/governance-only scope | PASS/FAIL | |
| Material gaps evaluated | PASS/FAIL | |
| Stable rules only | PASS/FAIL | |
| No duplicate rules | PASS/FAIL | |
| No brittle source/file tests | PASS/FAIL | |
| No production mutation | PASS/FAIL | |
| No dependency expansion | PASS/FAIL | |
| WP13 not started | PASS/FAIL | |

## 19. Findings

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed:

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
ARCHITECTURE EVOLUTION COMPLETE
ARCHITECTURE EVOLUTION COMPLETE WITH ACTIONS
ARCHITECTURE EVOLUTION BLOCKED
```

Explain why.

## 22. Next Authorized Work Package

If and only if progression is permitted, identify the exact WP13 title from the authoritative Release 0.9 execution plan.

Do not begin it.

---

# 17. Final Instruction

Execute Release 0.9 / WP12 — Architecture Evolution.

Inventory the existing Architecture.Tests first.

Determine which Release 0.9 boundaries are already protected and which material stable gaps remain.

Evaluate at minimum:

```text
Domain independence
Application independence from Infrastructure/Worker
Infrastructure independence from Worker
Worker no direct Domain dependency
cycles
implementation visibility
Application-owned observation abstraction
Worker thinness
test-project isolation
```

Add only non-duplicative, stable architecture tests that protect real Release 0.9 boundaries.

Do not add brittle file/name/source-text rules merely to increase coverage.

A zero-test-change result is acceptable when objectively justified.

Do not modify production code.

Run Architecture.Tests directly, regress Domain/Application/Infrastructure tests, build, run `eng/verify.ps1`, inspect dependencies and final Git scope, and return the complete WP12 report.

Finish with exactly one:

```text
ARCHITECTURE EVOLUTION COMPLETE
ARCHITECTURE EVOLUTION COMPLETE WITH ACTIONS
ARCHITECTURE EVOLUTION BLOCKED
```

If complete, identify the exact WP13 from the Release 0.9 execution plan.

Do not execute WP13.

# Conclusion

WP12 turns the architectural lessons of the Release 0.9 vertical slice into durable executable boundaries without overfitting tests to today's implementation.

The progression is:

```text
WP03–WP11 Implemented Architecture
        ↓
Existing Rule Inventory
        ↓
Release 0.9 Gap Analysis
        ↓
Stable Rule Selection
        ↓
Architecture Test Evolution
        ↓
Regression + Canonical Verification
        ↓
ARCHITECTURE EVOLUTION COMPLETE
        ↓
WP13
```

> **Architecture tests should protect stable boundaries that matter, not encode the accidental shape of today's code.**
