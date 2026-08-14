# Codex Execution Prompt — Release 0.8 / 15 Release Acceptance Review

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.8 — Solution Skeleton |
| Work Package | 15 — Release Acceptance Review |
| Execution Mode | Final release-governance review / no implementation changes by default |
| Primary Agent | Codex |
| Prerequisite | 14 — GitHub Integration completed and merged to `main` |
| Primary Area | Release 0.8 acceptance and closure readiness |
| Expected Outcome | Perform the final evidence-based acceptance review for Release 0.8, confirm that all work packages and governance conditions are satisfied, identify any remaining blockers or actions, and determine whether Release 0.8 is ready for formal closure without beginning Release 0.9 |

---

## Purpose

Perform the final acceptance review for **Phase 2 — Release 0.8: Solution Skeleton**.

WP01–WP14 established and validated the entire Release 0.8 lifecycle:

```text
Repository Preflight
        ↓
Root Solution
        ↓
Production Projects
        ↓
Project References
        ↓
Root Build Configuration
        ↓
Minimal Worker Host
        ↓
Dependency Registration
        ↓
Test Projects
        ↓
Architecture Tests
        ↓
Solution Organization
        ↓
Engineering Scripts Integration
        ↓
Documentation Alignment
        ↓
Full Skeleton Validation
        ↓
GitHub Integration
        ↓
Release Acceptance Review
```

WP15 does not create new product functionality.

WP15 determines whether the completed Release 0.8 implementation, documentation, validation evidence, GitHub traceability, and governance state together satisfy the release acceptance contract.

This work package is review-first.

The default repository change set is:

```text
NONE
```

Do not alter implementation merely to produce a `COMPLETE` decision.

---

# 1. Objective

Determine whether Release 0.8 is ready for formal closure.

The review must evaluate:

```text
technical completeness
architectural correctness
solution completeness
test/architecture enforcement
engineering workflow
documentation alignment
clean reconstruction
GitHub traceability
branch/PR integration
remaining open issues
milestone state
scope compliance
Release 0.9 boundary protection
```

At completion, return exactly one evidence-based decision:

```text
ACCEPTED
ACCEPTED WITH ACTIONS
REJECTED
```

This WP15 decision model is release-level and intentionally distinct from the work-package `COMPLETE` model.

---

# 2. Authority and Preconditions

Before reviewing anything, read completely:

```text
docs/roadmap/release-0.8/prompts/15-release-acceptance-review-codex-prompt.md
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Review relevant current-state repository artifacts:

```text
AIQuantTradingResearch.slnx
Directory.Build.props
Directory.Packages.props
global.json
.editorconfig
src/**
tests/**
eng/**
README.md
docs/**
.github/**
```

Review Release 0.8 GitHub governance and evidence:

```text
milestone 39
Release 0.8 issues
WP14 issue #65
PR #67
current main branch
origin/main
```

Use authenticated GitHub inspection when available.

Do not expose credentials or tokens.

---

# 3. Accepted WP14 Baseline

Expected Git/GitHub state:

```text
main synchronized with origin/main
WP14 governance artifacts merged
working tree clean
```

Expected GitHub traceability:

```text
Milestone #39
  Phase 2 - Release 0.8: Solution Skeleton

Issue #65
  [Feature]: 14 — GitHub Integration

PR #67
  merged into main
```

Expected technical baseline:

```text
8 solution projects
4 production projects
4 test projects
accepted production graph
0 cycles
Architecture.Tests = 7/7
eng/verify.ps1 = PASS
eng/build.sh = PASS
```

Expected documentation state:

```text
current-state Release 0.8 documentation aligned
future capabilities clearly marked as planned
```

Verify actual current state rather than assuming it.

---

# 4. Release Acceptance Principles

## 4.1 Evidence Over Assumption

Every acceptance claim must be supported by:

```text
repository state
test output
script validation
Git history
GitHub state
documented authority
```

Do not accept based on prior conversation memory alone.

## 4.2 No Repair During Review

Do not modify implementation, documentation, scripts, project structure, or GitHub governance simply to make the release acceptable.

If something fails acceptance:

```text
record it
classify it
identify owner/action
do not silently repair
```

## 4.3 Release Boundary Protection

Release 0.8 is the Solution Skeleton release.

Do not require Release 0.9 capabilities for Release 0.8 acceptance.

Examples of later work include:

```text
Build, CI & Quality Gates
Plugin Framework
Market Data
Storage
Pipelines
feature-level tests
production observability implementation
cloud deployment
```

Do not reject Release 0.8 because later-release functionality does not exist.

## 4.4 No Premature Release 0.9 Work

Do not begin Release 0.9.

Do not create CI merely because Release 0.9 owns CI.

Do not implement plugin infrastructure.

---

# 5. Work Package Completion Review

Review the Release 0.8 execution plan and confirm each WP01–WP14 is completed or otherwise accepted.

Expected sequence:

```text
01 — Repository Preflight
02 — Root Solution
03 — Production Projects
04 — Project References
05 — Root Build Configuration
06 — Minimal Worker Host
07 — Dependency Registration
08 — Test Projects
09 — Architecture Tests
10 — Solution Organization
11 — Engineering Scripts Integration
12 — Documentation Alignment
13 — Full Skeleton Validation
14 — GitHub Integration
15 — Release Acceptance Review
```

For WP01–WP14 classify:

```text
COMPLETE
COMPLETE WITH RESOLVED ACTIONS
INCOMPLETE
BLOCKED
NOT APPLICABLE
```

Use actual repository/GitHub evidence.

---

# 6. Technical Acceptance Contract

Release 0.8 technical acceptance requires:

```text
root .slnx exists and parses
8 projects exactly
4 production projects
4 test projects
/src/ and /tests/ solution organization
net10.0 effective target framework
SDK compatible with global.json
production dependency graph exact
0 production cycles
minimal Worker host valid
AddApplication boundary valid
AddInfrastructure boundary valid
4 test project skeletons valid
7 architecture tests pass
engineering scripts exist and work
clean reconstruction succeeds
documentation matches implementation
no obsolete current-state Api/SharedKernel implementation
```

Do not add criteria not defined by Release 0.8 authority.

---

# 7. Production Dependency Acceptance

Required graph:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

Required:

```text
Cycles = 0
Unexpected edges = 0
```

Architecture.Tests must continue enforcing:

```text
Domain !→ Application
Domain !→ Infrastructure
Domain !→ Worker
Application !→ Infrastructure
Application !→ Worker
Infrastructure !→ Worker
production graph acyclic
```

---

# 8. Build and Toolchain Acceptance

Validate:

```text
Configured SDK = expected Release 0.8 SDK
Effective SDK = compatible
TargetFramework = net10.0
Central Package Management valid
Root build policy valid
```

Known environmental warnings such as:

```text
NU1900 vulnerability-feed connectivity
```

are non-blocking when restore/build/test succeed and repository acceptance is unaffected.

Do not disable auditing.

---

# 9. Worker and DI Acceptance

Worker must remain a minimal executable composition root.

Expected lifecycle:

```text
Host.CreateApplicationBuilder
        ↓
AddApplication
        ↓
AddInfrastructure
        ↓
Build
        ↓
RunAsync
```

At Release 0.8:

```text
AddApplication may be empty
AddInfrastructure may be empty
```

Do not require later service registrations.

Do not require hosted services or domain functionality.

---

# 10. Test Acceptance

Expected test projects:

```text
Domain.Tests
Application.Tests
Infrastructure.Tests
Architecture.Tests
```

The first three may intentionally contain zero tests.

Release 0.8 acceptance depends on valid project/test infrastructure, not artificial placeholder tests.

Required Architecture.Tests result:

```text
Discovered = 7
Passed = 7
Failed = 0
```

---

# 11. Engineering Workflow Acceptance

Expected scripts:

```text
eng/restore.ps1
eng/build.ps1
eng/build.sh
eng/clean.ps1
eng/format.ps1
eng/test.ps1
eng/verify.ps1
```

Expected primary local quality flow:

```text
restore
→ format verification
→ build
→ test
```

Expected:

```text
eng/verify.ps1 = PASS
eng/build.sh = PASS when environment supports it
```

Failure propagation and repository-root path resolution must remain valid.

---

# 12. Clean Reconstruction Acceptance

Revalidate the clean-reconstruction proof.

Recommended sequence:

```text
verify
clean
restore
format verification
build
test
verify
```

This may reproduce WP13 validation if needed for final acceptance.

Do not use destructive Git cleanup.

Confirm:

```text
tracked source preserved
user files preserved
generated outputs reconstruct correctly
final Git state remains clean
```

---

# 13. Documentation Acceptance

Current-state documentation must accurately reflect:

```text
AIQuantTradingResearch.slnx
8 projects
/src/ and /tests/
accepted dependency graph
minimal Worker
empty DI boundaries
4 test projects
7 architecture rules/tests
WP11 engineering workflow
future capabilities remain planned
```

Do not require historical execution prompts to be rewritten.

Historical artifacts may preserve earlier states.

---

# 14. GitHub Integration Acceptance

Verify actual remote state.

At minimum inspect:

```text
repository identity
default branch
origin/main
milestone #39
issue #65
PR #67
release-related open issues
release-related closed issues
```

Confirm WP14 governance artifacts are present on `main`.

Confirm no history rewrite occurred.

Confirm PR #67 was merged through the forward-only governance strategy.

Do not fabricate review or merge information.

---

# 15. Milestone and Issue Acceptance

Inspect milestone #39.

Determine:

```text
total issues
open issues
closed issues
WP14 state
WP15 state
other Release 0.8 open issues
```

Do not close issues automatically unless WP15 authority explicitly requires it.

If acceptance review is the final remaining open issue, record that fact.

If another substantive Release 0.8 issue remains open, determine whether it blocks release closure.

Do not treat administrative issue-state lag as equivalent to a technical failure without authority.

---

# 16. Release Scope Acceptance

Confirm Release 0.8 contains only the intended Solution Skeleton scope.

Verify absence of premature implementation for:

```text
Release 0.9 CI framework
Plugin infrastructure
Market data providers
Storage engines
Pipelines
Trading/backtesting
AI/ML
MLOps
Cloud deployment
production feature implementations
```

Planned documentation is acceptable.

Premature code implementation is not.

---

# 17. Repository Cleanliness Acceptance

Record:

```text
git branch --show-current
git rev-parse HEAD
git status --short
git diff -- .
git diff --cached -- .
```

Expected final review state:

```text
main
synchronized with origin/main
no staged changes
no unintended tracked changes
```

If intentional untracked historical/governance files exist, classify them.

A clean repository is preferred for release acceptance.

---

# 18. Acceptance Severity Model

Classify findings as:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## BLOCKER

Release cannot be accepted.

Examples:

```text
solution fails build
architecture tests fail
dependency graph violated
required project missing
verify fails due to repository defect
Release 0.8 implementation not integrated
```

## REQUIRED ACTION

Action required before formal milestone closure, but technical release may otherwise be sound.

Examples:

```text
WP15 issue must be closed after review
milestone state needs final update
human governance acknowledgment required
```

## RISK

Non-blocking concern that should be tracked.

## OBSERVATION

Informational only.

---

# 19. Acceptance Decision Model

Return:

```text
ACCEPTED
```

when:

```text
all mandatory technical and governance criteria pass
no blocker remains
no required action remains before release closure
```

Return:

```text
ACCEPTED WITH ACTIONS
```

when:

```text
technical Release 0.8 acceptance passes
no blocker exists
one or more explicit administrative/governance closure actions remain
```

Examples:

```text
close WP15 issue
close milestone
record final release status
```

Return:

```text
REJECTED
```

when any mandatory acceptance criterion fails.

Do not use `ACCEPTED WITH ACTIONS` to hide a technical failure.

---

# 20. Execution Procedure

## Step 1 — Read Authority

Read WP15, execution plan, manifest, current repository state, relevant documentation, and GitHub governance.

## Step 2 — Record Local State

Record:

```text
repository
branch
HEAD
working tree
configured SDK
effective SDK
```

## Step 3 — Record Remote State

Using authenticated GitHub access, inspect:

```text
repository
default branch
milestone 39
issue 65
PR 67
Release 0.8 issues
```

## Step 4 — Verify WP14 Merge State

Confirm the forward-only WP14 integration is merged to main.

Confirm local main matches origin/main.

## Step 5 — Review WP01–WP14 Completion

Create work-package acceptance matrix.

## Step 6 — Validate Manifest

Compare actual repository against Release 0.8 manifest.

## Step 7 — Validate Solution and Project Inventory

Confirm exact eight-project skeleton.

## Step 8 — Validate Production Graph

Confirm exact graph and zero cycles.

## Step 9 — Validate Build Configuration

Confirm accepted SDK/build policy.

## Step 10 — Validate Worker and DI

Confirm minimal composition-root state.

## Step 11 — Validate Test Skeleton

Confirm four test projects.

## Step 12 — Execute Architecture.Tests

Require 7/7.

## Step 13 — Execute Canonical Verify

Require PASS.

## Step 14 — Execute Shell Build if Supported

Require PASS or accurately report environment limitation.

## Step 15 — Revalidate Clean Reconstruction if Needed

Use WP13 evidence and/or rerun approved workflow where appropriate.

## Step 16 — Validate Documentation

Confirm current-state alignment.

## Step 17 — Validate GitHub Traceability

Confirm milestone, issue, PR, branch, and merge facts.

## Step 18 — Inspect Remaining Release 0.8 Issues

Determine whether any open issue blocks closure.

## Step 19 — Inspect Release Scope

Confirm no Release 0.9/product scope leaked into Release 0.8.

## Step 20 — Inspect Final Repository State

Ensure review introduced no repository changes.

## Step 21 — Build Acceptance Matrix

Mark every mandatory criterion PASS/FAIL.

## Step 22 — Produce Final Acceptance Decision

Use:

```text
ACCEPTED
ACCEPTED WITH ACTIONS
REJECTED
```

## Step 23 — Identify Closure Actions

If accepted with actions, list exact manual/admin actions.

## Step 24 — Identify Next Authoritative Step

Read the execution plan.

The next step may be:

```text
Release 0.8 closure
milestone closure
transition to Release 0.9
```

Do not infer from memory.

Do not begin it.

---

# 21. Acceptance Criteria

Release 0.8 is accepted only when applicable mandatory criteria pass:

- [ ] WP15 authority and Release 0.8 plan/manifest reviewed.
- [ ] Local branch, HEAD, working tree, SDK state recorded.
- [ ] Authenticated GitHub state inspected.
- [ ] WP14 PR integration verified on `main`.
- [ ] Local `main` synchronized with `origin/main`.
- [ ] WP01–WP14 completion matrix produced.
- [ ] Release 0.8 manifest validated.
- [ ] Root `.slnx` exists and parses.
- [ ] Solution contains exactly 8 projects.
- [ ] Exactly 4 production projects exist.
- [ ] Exactly 4 test projects exist.
- [ ] `/src/` and `/tests/` solution organization is correct.
- [ ] All projects target the accepted framework.
- [ ] Production graph exactly matches accepted dependency graph.
- [ ] Production dependency cycles = 0.
- [ ] Root build/toolchain configuration is valid.
- [ ] Worker remains minimal and valid.
- [ ] `AddApplication` boundary is valid.
- [ ] `AddInfrastructure` boundary is valid.
- [ ] Four test skeleton projects are valid.
- [ ] Architecture.Tests discovers exactly 7 tests.
- [ ] Architecture.Tests passes all 7 tests.
- [ ] Engineering scripts exist and remain valid.
- [ ] Canonical `eng/verify.ps1` passes.
- [ ] Shell build passes when environment supports it.
- [ ] Clean reconstruction evidence remains valid.
- [ ] Current-state documentation matches implementation.
- [ ] Obsolete current-state Api/SharedKernel implementation is absent.
- [ ] Future capabilities remain clearly planned rather than implemented.
- [ ] Release 0.8 GitHub milestone is verified.
- [ ] WP14 issue is verified.
- [ ] WP14 PR is verified merged.
- [ ] No unauthorized history rewrite occurred.
- [ ] Remaining open Release 0.8 issues were reviewed.
- [ ] No substantive blocking Release 0.8 issue remains.
- [ ] No Release 0.9 capability was prematurely implemented.
- [ ] Review introduced no repository changes.
- [ ] No staging/commit/push occurred during review unless explicitly authorized.
- [ ] Acceptance findings were classified.
- [ ] Final decision is evidence-based.
- [ ] Next authoritative step is identified.
- [ ] Release 0.9 work was not started.

---

# 22. Expected Output Contract

Return one complete **Release 0.8 Acceptance Review Report**.

Do not create a report file unless separately authorized.

Use this structure.

# Release 0.8 Acceptance Review Report

## 1. Executive Summary

State:

```text
Release:
Review scope:
Technical result:
Governance result:
Final acceptance decision:
```

## 2. Execution Context

```text
Repository:
Branch:
HEAD:
Working Tree:
Configured SDK:
Effective SDK:
GitHub Authentication:
Remote:
```

Do not expose credentials.

## 3. Authoritative Sources Reviewed

List material repository paths and GitHub objects.

## 4. WP01–WP14 Completion Matrix

| Work Package | Status | Evidence | Acceptance |
| --- | --- | --- | --- |

Allowed status values:

```text
COMPLETE
COMPLETE WITH RESOLVED ACTIONS
INCOMPLETE
BLOCKED
NOT APPLICABLE
```

## 5. Manifest Acceptance

| Area | Expected | Actual | Result | Evidence |
| --- | --- | --- | --- | --- |

## 6. Solution Acceptance

```text
Solution:
Parses:
Project count:
Production projects:
Test projects:
Solution folders:
Missing:
Unexpected:
Duplicates:
Assessment:
```

## 7. Dependency Architecture Acceptance

```text
Domain:
Application:
Infrastructure:
Worker:
Cycles:
Forbidden edges:
Assessment:
```

## 8. Build / Toolchain Acceptance

```text
SDK:
Target framework:
Nullable:
Implicit usings:
Central package management:
Analyzer/warning policy:
Assessment:
```

## 9. Worker / DI Acceptance

```text
Host model:
Composition lifecycle:
Application boundary:
Infrastructure boundary:
Concrete registrations:
Unexpected runtime behavior:
Assessment:
```

## 10. Test Acceptance

```text
Domain.Tests:
Application.Tests:
Infrastructure.Tests:
Architecture.Tests:
Assessment:
```

## 11. Architecture Test Acceptance

```text
Command:
Discovered:
Passed:
Failed:
Rules:
Assessment:
```

## 12. Engineering Workflow Acceptance

```text
Restore:
Build:
Shell Build:
Clean:
Format:
Test:
Verify:
Assessment:
```

## 13. Clean Reconstruction Acceptance

```text
Evidence source:
Initial verify:
Clean:
Restore:
Format:
Build:
Test:
Final verify:
Assessment:
```

## 14. Documentation Acceptance

```text
Solution/project structure:
Dependency graph:
Worker/DI:
Tests:
Architecture enforcement:
Engineering workflow:
Current/future distinction:
Assessment:
```

## 15. GitHub Integration Acceptance

```text
Milestone:
WP14 issue:
PR:
PR merged:
Main synchronized:
History rewritten:
Assessment:
```

## 16. Remaining Release 0.8 Issues

| Issue | State | Classification | Blocks Acceptance | Required Action |
| --- | --- | --- | --- | --- |

## 17. Scope Boundary Acceptance

```text
Release 0.9 CI implemented:
Plugin framework implemented:
Product features implemented:
Premature scope detected:
Assessment:
```

## 18. Repository Cleanliness

```text
Branch:
HEAD:
git status --short:
Staged changes:
Tracked changes:
Assessment:
```

## 19. Environmental Observations

Record only relevant non-repository conditions.

## 20. Findings

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Classifications:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 21. Acceptance Criteria Matrix

| Criterion | Result | Evidence |
| --- | --- | --- |

## 22. Release Acceptance Decision

State exactly one:

```text
ACCEPTED
ACCEPTED WITH ACTIONS
REJECTED
```

Explain the evidence supporting the decision.

## 23. Required Closure Actions

If `ACCEPTED WITH ACTIONS`, list exact remaining actions.

Examples:

```text
close WP15 issue
close milestone 39
record final release status
```

If `ACCEPTED`:

```text
None
```

## 24. Release 0.8 Closure Readiness

State whether Release 0.8 may now be formally closed.

Do not perform closure unless WP15 authority explicitly allows it.

## 25. Next Authoritative Step

Read:

```text
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
```

Identify the exact next step.

Do not begin it.

---

# 23. Prohibited Behaviors

Do not:

- modify production code;
- modify test code;
- modify architecture tests;
- modify `.csproj`;
- modify `.slnx`;
- modify `eng/`;
- modify documentation;
- modify build/package/SDK configuration;
- create CI;
- add Release 0.9 functionality;
- change architecture;
- stage unrelated files;
- commit review artifacts unless explicitly authorized;
- push;
- force push;
- rewrite history;
- merge PRs during review unless explicitly authorized;
- create release tags;
- publish GitHub Releases;
- close milestone 39 unless explicitly authorized;
- begin Release 0.9.

---

# 24. Completion Model

```text
Read Authority
      ↓
Record Local + Remote State
      ↓
Verify WP14 Integration
      ↓
Review WP01–WP14
      ↓
Validate Manifest
      ↓
Validate Solution + Graph
      ↓
Validate Toolchain + Worker + DI
      ↓
Validate Tests + Architecture Rules
      ↓
Validate Engineering Workflow
      ↓
Validate Clean Reconstruction
      ↓
Validate Documentation
      ↓
Validate GitHub Traceability
      ↓
Inspect Remaining Issues
      ↓
Validate Release Boundary
      ↓
Inspect Clean Repository State
      ↓
Classify Findings
      ↓
Release Acceptance Decision
      ↓
ACCEPTED | ACCEPTED WITH ACTIONS | REJECTED
```

---

# 25. Final Instruction

Execute **Phase 2 — Release 0.8 / Work Package 15 — Release Acceptance Review** against the actual current `AIQuantTradingResearch` repository and GitHub state.

This is the final Release 0.8 review.

Do not change implementation to make the release pass.

Verify that WP14 is merged and `main` is synchronized with `origin/main`.

Review WP01–WP14 evidence.

Validate the Release 0.8 manifest and complete eight-project solution.

Revalidate:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
Cycles          → 0
Architecture.Tests → 7/7
eng/verify.ps1  → PASS
eng/build.sh    → PASS when supported
```

Validate Worker/DI boundaries, test skeleton, engineering workflow, documentation, clean reconstruction, GitHub traceability, milestone/issue/PR state, and remaining Release 0.8 issues.

Do not require Release 0.9 capabilities.

Do not implement CI.

Do not modify repository or GitHub state during the review unless the authoritative WP15 contract explicitly allows a closure action.

Return the complete **Release 0.8 Acceptance Review Report**.

Finish with exactly one:

```text
ACCEPTED
ACCEPTED WITH ACTIONS
REJECTED
```

State whether Release 0.8 is ready for formal closure.

Identify the next authoritative step from the Release 0.8 execution plan.

Do not begin it.

---

# Conclusion

Work Package 15 is the final governance boundary for Release 0.8.

Earlier work packages proved individual implementation, architecture, validation, documentation, and integration responsibilities. WP15 determines whether those proofs together constitute an acceptable release.

The final review path is:

```text
Integrated Release 0.8 State
        ↓
Work-Package Completion Review
        ↓
Manifest Validation
        ↓
Architecture + Toolchain Validation
        ↓
Test + Engineering Workflow Validation
        ↓
Documentation Validation
        ↓
GitHub Governance Validation
        ↓
Remaining-Issue Review
        ↓
Release Boundary Validation
        ↓
Acceptance Decision
```

Release acceptance is not the same as feature completeness.

Release 0.8 succeeds when the intended Solution Skeleton is complete, executable, governed, documented, and ready to support the next release without hidden architectural debt or unresolved release-level blockers.

> **Accept the release only when implementation, architecture, automation, documentation, and governance all describe the same completed boundary.**
