# Codex Execution Prompt — Release 0.9 / WP01 Repository & Release Preflight

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 01 — Repository & Release Preflight |
| Type | Research |
| GitHub Issue | #69 |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Execution Mode | Read-only repository/GitHub research and validation |
| Expected Outcome | Establish an evidence-based Release 0.9 starting baseline without mutating repository or GitHub planning state |

## 1. Purpose

Execute Release 0.9 / WP01 against the actual repository and GitHub state.

WP01 proves that Release 0.8 is formally closed, Release 0.9 governance is integrated, the technical baseline is healthy, GitHub planning matches authority, and no Release 0.9 implementation has started.

This is research-only.

Default repository mutation: `NONE`.

Default GitHub mutation: `NONE`.

Do not begin WP02.

## 2. Authoritative Sources

Read completely:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/01-repository-release-preflight-codex-prompt.md
```

Inspect relevant current-state authority when present:

```text
README.md
AIQuantTradingResearch.slnx
Directory.Build.props
Directory.Packages.props
global.json
.editorconfig
eng/**
src/**
tests/**
docs/architecture/**
docs/design/**
docs/implementation/**
docs/project/ROADMAP.md
.github/**
```

Inspect GitHub state for milestone #39, milestone #40, issues #69–#82, project-board status, default branch, and relevant open PRs.

## 3. Expected Release 0.8 Baseline

Verify independently:

```text
Release 0.8 = COMPLETE / CLOSED
Milestone #39 = CLOSED
Solution projects = 8
Production projects = 4
Test projects = 4
Domain -> none
Application -> Domain
Infrastructure -> Application
Worker -> Application + Infrastructure
Cycles = 0
Architecture.Tests = 7/7
eng/verify.ps1 = PASS
```

## 4. Expected Release 0.9 Governance Baseline

Verify Release 0.9 authority is present on `main`, including:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/release-0.9-github-planning-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-github-planning-codex-prompt-chat.md
docs/roadmap/release-0.9/prompts/release-0.9-governance-integration-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-governance-integration-codex-prompt-chat.md
```

Repository convention:

```text
*-codex-prompt.md
*-codex-prompt-chat.md
```

Valid prompt-chat companions are intentional governance artifacts.

## 5. Expected GitHub Planning Baseline

Verify:

```text
Milestone #40 = Phase 2 - Release 0.9: Research Platform
Issues #69–#82 exist
Exactly 14 WPs
WP01 #69 = Open / Todo / not started
14 Todo
0 In Progress
0 Done
No WP15
```

Do not mutate planning.

## 6. Research Questions

WP01 must answer with evidence:

1. Repository identity, branch, HEAD, remotes, local/remote divergence, and working-tree state.
2. Whether Release 0.8 remains formally closed.
3. Whether Release 0.9 authority is fully integrated on `main`.
4. Whether milestone #40 and issues #69–#82 match authority.
5. Whether `AIQuantTradingResearch.slnx` parses and contains exactly 8 projects, 4 production and 4 test projects.
6. Whether the production dependency graph is exact and acyclic.
7. Configured/effective SDK, target framework, central package management, nullable, implicit usings, warning/analyzer policy.
8. Whether all existing engineering scripts remain suitable as the Release 0.9 starting workflow.
9. Whether Architecture.Tests still discover/pass 7/7.
10. Whether Domain.Tests, Application.Tests, and Infrastructure.Tests remain intentionally empty at the starting baseline.
11. Whether any Release 0.9 implementation already exists unexpectedly.
12. Whether any unauthorized future-scope implementation exists.

## 7. Release 0.9 Implementation Leakage Search

Inspect for pre-existing implementation of:

```text
research Domain model
research Application contracts
research execution use case
deterministic Infrastructure adapter
Release 0.9 DI registrations
Worker research execution
new Domain/Application/Infrastructure behavioral tests
new Release 0.9 architecture rules
```

Classify findings as:

```text
EXPECTED BASELINE
PRE-EXISTING RELEASE-0.9 IMPLEMENTATION
HISTORICAL
PLANNED DOCUMENTATION
UNRELATED
AMBIGUOUS
```

Do not change anything.

## 8. Future-Scope Leakage Search

Inspect implementation for:

```text
real market-data providers
HTTP acquisition
database/persistence
plugin framework/loading
strategy/backtesting engine
AI/ML
MLOps
cloud deployment
REST API
web UI
message brokers
```

Planned documentation is not implementation leakage.

## 9. Authorized Scope

WP01 may read files, inspect Git/GitHub state, run non-mutating validation, run Architecture.Tests, run `eng/verify.ps1`, inspect references/packages/docs, search for scope leakage, classify findings, and return the preflight report.

## 10. Prohibited Scope

Do not modify source, tests, projects, solution, references, packages, build configuration, scripts, documentation, Release 0.8/0.9 authority, GitHub planning, or issue status.

Do not create branches, stage, commit, push, create PRs, create CI, create `RESEARCH_DOMAIN_MODEL.md`, or begin WP02.

## 11. Working-Tree Protection

Record `git status --short` before validation.

Classify non-clean entries as:

```text
EXPECTED GOVERNANCE
UNRELATED USER WORK
GENERATED
AMBIGUOUS
UNEXPECTED TECHNICAL CHANGE
```

Do not use destructive Git cleanup/reset/restore commands and do not delete user work.

## 12. Validation Procedure

1. Read all authority.
2. Record repository root, branch, HEAD, Git status, remotes, and main/origin-main divergence.
3. Verify GitHub authentication without exposing tokens.
4. Verify Release 0.8 milestone #39 and closure state.
5. Verify Release 0.9 milestone #40, issues #69–#82, and WP01 Todo state.
6. Verify Release 0.9 authority files on `main`.
7. Validate solution inventory.
8. Inspect actual production `ProjectReference` graph and cycles.
9. Run `dotnet --version` and inspect root build/package configuration.
10. Execute Architecture.Tests directly; expected baseline 7 discovered / 7 passed / 0 failed.
11. Run:
   `powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1`
12. Confirm the three behavioral test projects are intentionally empty unless unexpected 0.9 implementation is found.
13. Search for Release 0.9 implementation leakage.
14. Search for unauthorized future-scope implementation.
15. Record documentation starting-state observations for later WP13 without editing.
16. Run final `git status --short`, `git diff -- .`, and `git diff --cached -- .`.

WP01 itself must introduce no repository changes.

Known NU1900 vulnerability-feed connectivity warnings may be observations when commands succeed.

## 13. Severity Model

Classify findings:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

A blocker prevents WP02.

Examples: Release 0.8 not actually closed, Release 0.9 authority missing, broken architecture baseline, repository-caused verify failure, or pre-existing Release 0.9 implementation materially conflicting with authority.

## 14. Decision Model

Return exactly one:

```text
READY
READY WITH ACTIONS
NOT READY
```

Use `READY` when all mandatory starting criteria pass and no required action remains.

Use `READY WITH ACTIONS` only for non-blocking explicit actions.

Use `NOT READY` when a mandatory prerequisite fails.

## 15. Acceptance Criteria

- Release 0.9 plan, manifest, and WP01 prompt read completely.
- Repository identity, branch, HEAD, remotes, divergence, and initial Git state recorded.
- GitHub authentication verified.
- Release 0.8 milestone #39 verified closed.
- Release 0.9 authority verified on `main`.
- Milestone #40 and issues #69–#82 verified.
- Exactly 14 Release 0.9 WPs and no WP15.
- WP01 begins from Todo/not-started state.
- `.slnx` parses.
- Exactly 8 projects: 4 production + 4 tests.
- Production graph matches accepted baseline and cycles=0.
- SDK/toolchain/build configuration recorded.
- Architecture.Tests direct execution passes 7/7.
- `eng/verify.ps1` passes.
- Empty behavioral suites correctly classified.
- Release 0.9 implementation leakage search completed.
- Future-scope leakage search completed.
- Documentation starting-state observations recorded.
- No repository or GitHub mutation introduced.
- Final Git state inspected.
- Findings classified.
- Decision recorded.
- WP02 not started.

## 16. Expected Output Contract

Return one complete **Release 0.9 WP01 Repository & Release Preflight Execution Report** containing:

1. Executive Summary
2. Execution Context
3. Authoritative Sources Reviewed
4. Release 0.8 Closure Verification
5. Release 0.9 Governance Verification
6. Initial Git State Classification
7. Solution Inventory
8. Production Dependency Graph
9. Toolchain / Root Configuration
10. Engineering Workflow Baseline
11. Architecture Test Baseline
12. Canonical Verification
13. Behavioral Test Baseline
14. Release 0.9 Implementation Leakage Assessment
15. Future-Scope Leakage Assessment
16. Documentation Starting-State Assessment
17. Validation Evidence
18. Scope Compliance
19. Findings
20. Acceptance Criteria Matrix
21. Final Decision
22. Next Authorized Work Package

The final decision must be exactly one:

```text
READY
READY WITH ACTIONS
NOT READY
```

If progression is allowed, identify `WP02 — Research Domain Discovery` as next but do not begin it.

## 17. Final Instruction

Execute Release 0.9 / WP01 as research-only.

Verify Release 0.8 closure, Release 0.9 authority, GitHub planning, solution structure, dependency graph, SDK/build baseline, Architecture.Tests 7/7, and `eng/verify.ps1` PASS.

Search for pre-existing Release 0.9 implementation and unauthorized future-scope implementation.

Do not mutate repository or GitHub state.

Do not stage, commit, push, create a branch, change WP01 status, or start WP02.

Return the complete WP01 preflight report and finish with exactly `READY`, `READY WITH ACTIONS`, or `NOT READY`.

# Conclusion

WP01 is the trust boundary between the closed Release 0.8 Solution Skeleton and the new Release 0.9 Research Platform.

It does not design the research domain and does not write code. It proves exactly what state the repository is starting from so WP02 can perform domain discovery from a controlled, evidence-based baseline.

> **Before AI-assisted implementation begins, the project must first prove exactly what state it is starting from.**
