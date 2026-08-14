# Codex Execution Prompt — Release 0.9 GitHub Planning

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Activity | GitHub Planning |
| Execution Mode | Governed GitHub planning mutation |
| Primary Agent | Codex |
| Prerequisite | `RELEASE_0.9_EXECUTION_PLAN.md` and `RELEASE_0.9_FILE_MANIFEST.md` committed or present in the repository |
| Primary Outcome | Create the authoritative Release 0.9 GitHub milestone and one GitHub issue for each of the 14 approved work packages, derived strictly from Release 0.9 authority |

---

# 1. Purpose

Create the GitHub planning structure for:

```text
Phase 2 — Release 0.9: Research Platform
```

using the two authoritative Release 0.9 governance documents as the source of truth:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
```

The GitHub milestone and issues must be **derived from these documents**.

Do not redesign the release.

Do not invent additional work packages.

Do not remove or merge work packages.

Do not reinterpret future scope as Release 0.9 scope.

---

# 2. Authoritative Sources

Read completely before performing any GitHub mutation:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
```

Also inspect relevant repository GitHub governance when present:

```text
.github/ISSUE_TEMPLATE/**
.github/PULL_REQUEST_TEMPLATE.md
.github/**
docs/project/ROADMAP.md
docs/roadmap/**
CONTRIBUTING.md
```

Inspect existing labels, milestones, issues, and project-board conventions.

Repository Release 0.9 authority takes precedence over generic GitHub conventions.

---

# 3. Required Release 0.9 Milestone

Create or reuse exactly one authoritative milestone for Release 0.9.

Expected title:

```text
Phase 2 - Release 0.9: Research Platform
```

Before creating it:

1. search existing open and closed milestones;
2. detect exact or semantically equivalent duplicates;
3. reuse the existing milestone if one already represents this release;
4. do not create a duplicate.

Use the repository's existing milestone-description style.

The milestone description must summarize the authoritative release mission, including:

```text
first executable vertical research capability
minimum research domain model
Application-owned contracts and orchestration
deterministic offline Infrastructure adapter
Worker execution
behavioral tests
architecture governance
offline/reproducible execution
no real providers/persistence/plugins/AI/ML/cloud
```

Do not invent a due date unless one is already defined by repository authority.

---

# 4. Required Work Packages

Create or reuse exactly these 14 work-package issues:

```text
01 — Repository & Release Preflight
02 — Research Domain Discovery
03 — Research Domain Model
04 — Research Application Contracts
05 — Research Execution Use Case
06 — Research Infrastructure Adapter
07 — Dependency Registration
08 — Worker Research Execution
09 — Domain Tests
10 — Application Tests
11 — Infrastructure Tests
12 — Architecture Evolution
13 — Documentation Alignment
14 — Full Validation, Integration & Acceptance
```

These names are authoritative.

Do not rename them.

Do not create WP15.

Do not create a Release 0.9 closure issue at this stage.

A post-acceptance closure activity may be authorized later, but it is not part of the 14-work-package issue set.

---

# 5. Issue Types

Use repository conventions and the approved Release 0.9 work-package types:

| WP | Type |
|---:|---|
| 01 | Research |
| 02 | Research |
| 03 | Feature |
| 04 | Feature |
| 05 | Feature |
| 06 | Feature |
| 07 | Feature |
| 08 | Feature |
| 09 | Tests |
| 10 | Tests |
| 11 | Tests |
| 12 | Tests |
| 13 | Documentation |
| 14 | Research |

Map these to the repository's existing issue-title/label conventions.

Expected title style, when consistent with existing repository practice:

```text
[Research]: 01 — Repository & Release Preflight
[Research]: 02 — Research Domain Discovery
[Feature]: 03 — Research Domain Model
...
[Tests]: 12 — Architecture Evolution
[Documentation]: 13 — Documentation Alignment
[Research]: 14 — Full Validation, Integration & Acceptance
```

If the repository uses a slightly different casing/label syntax, preserve repository convention while retaining the exact work-package number and name.

---

# 6. Issue Content Contract

Each issue must be derived from the corresponding work-package contract in:

```text
RELEASE_0.9_EXECUTION_PLAN.md
```

Each issue body must contain these sections:

```text
## Objective

## Authorized Scope

## Prohibited Scope

## Dependencies

## Expected Artifacts

## Validation Evidence

## Exit Criteria

## Release Constraints
```

Do not omit these sections.

Do not replace them with a generic task summary.

---

# 7. Objective

Copy or faithfully summarize the authoritative objective for that WP.

Do not broaden it.

Do not reinterpret it.

---

# 8. Authorized Scope

Include the approved authorized scope from the execution plan.

Preserve important boundaries and ownership.

Examples:

```text
WP02 performs discovery, not implementation.
WP03 modifies Domain only as justified by WP02.
WP06 is deterministic/offline and cannot use a real provider.
WP14 validates/integrates/accepts and is not a repair package.
```

---

# 9. Prohibited Scope

Every issue must explicitly carry its prohibited scope.

This is mandatory.

Release 0.9 scope protection is part of the issue contract.

Common release-wide prohibited scope includes:

```text
real market-data providers
HTTP acquisition
persistence/database implementation
plugin framework
strategy/backtesting engine
AI/ML
MLOps
cloud deployment
future-release functionality
```

However, use each WP's exact prohibited scope from the execution plan rather than blindly copying one generic list.

---

# 10. Dependencies

Each issue must list its authoritative work-package dependencies.

Use WP numbers and names.

Examples:

```text
WP03 depends on WP02.
WP07 depends on WP05 and WP06.
WP12 depends on WP03–WP11.
WP14 depends on WP01–WP13.
```

Do not create GitHub dependency relationships unless the repository already uses a supported parent/sub-issue/dependency mechanism and authority permits it.

At minimum, record the dependencies textually in the issue body.

---

# 11. Expected Artifacts

Derive expected artifact boundaries from both:

```text
RELEASE_0.9_EXECUTION_PLAN.md
RELEASE_0.9_FILE_MANIFEST.md
```

Do not invent exact source filenames before WP02 has established authoritative domain naming.

Where the manifest authorizes a bounded directory, preserve that form.

Examples:

```text
src/AIQuantTradingResearch.Domain/**
src/AIQuantTradingResearch.Application/**
tests/AIQuantTradingResearch.Domain.Tests/**
```

For WP02, use the authoritative documentation artifact:

```text
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

For WP01, make clear that no implementation/documentation mutation is expected.

---

# 12. Validation Evidence

Every issue must state the objective evidence required before completion.

Examples include:

```text
build result
test discovery/pass counts
dependency graph validation
zero cycles
deterministic repeatability
offline/no-network proof
architecture rules
Git state inspection
manifest compliance
```

Do not use vague completion statements such as:

```text
works as expected
looks correct
done
```

---

# 13. Exit Criteria

Each issue must contain the exact or faithful authoritative exit criteria.

The exit criterion is the condition that allows the next work package to begin.

Do not replace it with GitHub administrative state such as:

```text
PR merged
```

unless the corresponding work package actually owns GitHub integration.

---

# 14. Release Constraints Section

Every issue must include:

```text
## Release Constraints
```

with a concise reminder that:

```text
Release 0.9 must remain offline and deterministic.
No real providers.
No persistence.
No plugin framework.
No AI/ML.
No unauthorized later-release scope.
Repository authority takes precedence over assumptions.
```

Add WP-specific constraints where necessary.

---

# 15. Priority

Determine priority from repository conventions.

If Release 0.9 authority does not define per-WP priorities, use a consistent execution priority that reflects the dependency chain rather than inventing business severity.

Preferred default:

```text
P1
```

for all Release 0.9 execution work packages if that matches existing repository convention.

Do not mix arbitrary priorities without authority.

---

# 16. Release Field

Every issue must be associated with:

```text
Release 0.9
```

using the repository's existing project/roadmap field conventions when available.

Do not assign Release 0.8.

Do not assign a future release.

---

# 17. Area

Use the repository's existing Area taxonomy.

Suggested mappings only if they match existing repository conventions:

```text
WP01  governance / engineering
WP02  research / domain
WP03  domain
WP04  application
WP05  application
WP06  infrastructure
WP07  architecture / dependency-injection
WP08  worker / application-host
WP09  testing / domain
WP10  testing / application
WP11  testing / infrastructure
WP12  architecture
WP13  documentation
WP14  governance / validation
```

Do not invent new Area values if repository project fields already define accepted choices.

Use the nearest existing authoritative value.

---

# 18. Labels

Use existing labels only unless repository governance explicitly authorizes label creation.

At minimum map issue type to existing labels such as:

```text
research
feature
tests
documentation
```

Use exact existing label names/casing.

Do not create duplicate semantic labels.

Do not create one label per WP.

If a required type label does not exist, report it before creating a new label unless repository governance explicitly authorizes label management.

---

# 19. Roadmap / Project Board

If the repository uses a GitHub Project for roadmap tracking, add all 14 issues to the authoritative project.

Populate existing project fields when available:

```text
Roadmap Step
Summary
Priority
Release
Area
Label
Status
```

Expected initial status:

```text
Todo
```

or the repository's equivalent initial state.

Do not mark any Release 0.9 work package `In Progress` merely because the issues are being created.

Implementation has not begun.

---

# 20. Roadmap Step

If the GitHub Project contains a `Roadmap Step` field, use the exact work-package number/name:

```text
01 — Repository & Release Preflight
02 — Research Domain Discovery
...
14 — Full Validation, Integration & Acceptance
```

Do not renumber.

---

# 21. Summary Field

Create a concise summary derived from the objective.

The summary should explain the outcome, not repeat the title mechanically.

Examples of appropriate style:

```text
Establish the trusted Release 0.9 starting baseline and classify all pre-existing repository conditions.
```

```text
Discover the minimum research vocabulary, invariants, ownership, and deterministic reference scenario before implementation.
```

Do not add scope not present in the execution plan.

---

# 22. Duplicate Detection

Before creating any milestone or issue:

1. query existing open issues;
2. query closed issues when necessary;
3. identify exact/semantic Release 0.9 duplicates;
4. reuse/update only when clearly the same work package;
5. do not create duplicates.

Report:

```text
Existing reused
Newly created
Conflicts
Skipped
```

for milestone and issue planning objects.

---

# 23. GitHub Authentication and Safety

Before remote mutation, verify:

```text
gh auth status
repository identity
remote identity
```

Do not expose token values.

Do not:

```text
delete issues
delete milestones
delete labels
close unrelated issues
change repository visibility
change permissions
change branch protection
change secrets
change collaborators
```

This prompt authorizes Release 0.9 planning only.

---

# 24. No Repository Implementation Mutation

This GitHub-planning activity must not modify:

```text
src/**
tests/**
eng/**
AIQuantTradingResearch.slnx
Directory.Build.props
Directory.Packages.props
global.json
.github/workflows/**
```

Do not implement Release 0.9.

Do not create WP prompts beyond this planning activity.

Do not create source files.

Do not create CI.

---

# 25. Execution Procedure

## Step 1 — Read Release 0.9 Authority

Read both authoritative Release 0.9 governance files completely.

## Step 2 — Inspect Repository GitHub Governance

Inspect issue templates, labels, milestones, project fields, existing Release conventions, and roadmap structure.

## Step 3 — Verify GitHub Authentication

Verify authenticated access without exposing credentials.

## Step 4 — Search for Existing Release 0.9 Milestone

Reuse if authoritative equivalent exists.

Otherwise create exactly one Release 0.9 milestone.

## Step 5 — Search for Existing WP01–WP14 Issues

Detect duplicates across open/closed issues.

## Step 6 — Build Planning Matrix Before Mutation

Create an internal matrix with:

```text
WP
Type
Title
Objective/Summary
Priority
Release
Area
Labels
Milestone
Existing/New
```

Do not mutate yet.

## Step 7 — Validate Matrix Against Authority

Confirm exactly 14 WPs.

Confirm names/types match the execution plan.

Confirm no WP15.

Confirm no future-release scope.

## Step 8 — Create/Reuse Issues

Create only missing authoritative WP issues.

Associate every issue with Release 0.9 milestone.

## Step 9 — Add to Roadmap Project

When supported and part of existing repository governance, add/reuse each issue in the authoritative project and populate fields.

## Step 10 — Verify Remote State

Confirm:

```text
1 authoritative Release 0.9 milestone
14 authoritative Release 0.9 WP issues
all assigned to correct milestone
all initial statuses correct
all project fields correct when applicable
no duplicates
```

## Step 11 — Produce Planning Report

Return the complete GitHub Planning Execution Report.

Do not start WP01.

---

# 26. Acceptance Criteria

Planning is accepted only when:

- [ ] Both Release 0.9 authoritative governance files were read completely.
- [ ] Repository GitHub conventions were inspected.
- [ ] GitHub authentication was verified.
- [ ] Repository identity was verified.
- [ ] Existing milestones were searched before creation.
- [ ] Existing issues were searched before creation.
- [ ] Exactly one authoritative Release 0.9 milestone exists.
- [ ] Exactly 14 authoritative WP issues exist.
- [ ] WP numbers are exactly 01–14.
- [ ] Work-package names exactly match Release authority.
- [ ] Issue types match Release authority.
- [ ] Every issue includes Objective.
- [ ] Every issue includes Authorized Scope.
- [ ] Every issue includes Prohibited Scope.
- [ ] Every issue includes Dependencies.
- [ ] Every issue includes Expected Artifacts.
- [ ] Every issue includes Validation Evidence.
- [ ] Every issue includes Exit Criteria.
- [ ] Every issue includes Release Constraints.
- [ ] Every issue is assigned to milestone Release 0.9.
- [ ] Existing labels/conventions are reused.
- [ ] No duplicate semantic issues or milestones were created.
- [ ] Project-board fields are populated when the repository uses them.
- [ ] Initial status remains Todo/equivalent.
- [ ] No issue is marked In Progress.
- [ ] No implementation files were changed.
- [ ] No CI/workflow was created.
- [ ] No Release 0.9 implementation began.
- [ ] Final remote state was verified.
- [ ] Complete evidence was reported.

---

# 27. Expected Output Contract

Return one complete:

```text
Release 0.9 GitHub Planning Execution Report
```

Use this structure.

# Release 0.9 GitHub Planning Execution Report

## 1. Executive Summary

```text
Release:
Milestone:
Issues expected:
Issues created:
Issues reused:
Duplicates:
Project-board integration:
Final decision:
```

## 2. Execution Context

```text
Repository:
GitHub account:
Authentication:
Default branch:
Remote:
```

Do not expose credentials.

## 3. Authoritative Sources Reviewed

List exact repository paths.

## 4. GitHub Governance Assessment

```text
Issue-title convention:
Issue-type labels:
Priority convention:
Release field:
Area field:
Roadmap Step field:
Status field:
Project:
Milestone convention:
```

## 5. Milestone Result

```text
Title:
Number:
Created/Reused:
State:
Description:
Due date:
Assessment:
```

## 6. Work Package Planning Matrix

| WP | Type | Title | Priority | Release | Area | Label | Milestone | Result |
|---:|---|---|---|---|---|---|---|---|

## 7. Issue Results

For each WP:

```text
WP:
Issue:
Created/Reused:
Title:
Milestone:
Labels:
Project status:
Assessment:
```

## 8. Duplicate Assessment

```text
Duplicate milestones detected:
Duplicate issues detected:
Conflicts:
Actions:
```

## 9. Project Board Integration

Report field values for all 14 WPs when applicable.

## 10. Scope Compliance

| Check | Result | Evidence |
|---|---|---|
| Authority used without redesign | PASS/FAIL | |
| Exactly 14 WPs | PASS/FAIL | |
| No WP15 | PASS/FAIL | |
| No future scope added | PASS/FAIL | |
| No duplicate issues | PASS/FAIL | |
| No duplicate milestone | PASS/FAIL | |
| No implementation mutation | PASS/FAIL | |
| No CI created | PASS/FAIL | |
| WP01 not started | PASS/FAIL | |

## 11. Final GitHub State

```text
Release 0.9 milestone:
Open WP issues:
Closed WP issues:
Todo:
In Progress:
Done:
```

Expected initial work-package state:

```text
14 open
14 Todo
0 In Progress
0 Done
```

unless pre-existing equivalent planning objects legitimately alter those counts.

## 12. Findings

| ID | Classification | Finding | Evidence | Required Action | Owner |
|---|---|---|---|---|---|

Allowed classifications:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 13. Acceptance Criteria

Reproduce applicable criteria with PASS/FAIL/N/A.

## 14. Final Decision

State exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

## 15. Next Action

If planning is complete, identify:

```text
WP01 — Repository & Release Preflight
```

as the next authorized work package.

Do not begin WP01.

---

# 28. Decision Model

Return `COMPLETE` when the milestone, all 14 issues, and applicable project-board metadata are correct and no planning action remains.

Return `COMPLETE WITH ACTIONS` only when the planning structure is valid but a non-blocking manual GitHub/project-field action remains.

Return `BLOCKED` when authoritative planning cannot be safely represented without resolving a material governance conflict.

---

# 29. Final Instruction

Execute the GitHub planning setup for:

```text
Phase 2 — Release 0.9: Research Platform
```

using:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
```

as immutable Release 0.9 authority.

Create or reuse exactly one Release 0.9 milestone.

Create or reuse exactly 14 issues corresponding to WP01–WP14.

Derive every issue's objective, authorized scope, prohibited scope, dependencies, expected artifacts, validation evidence, and exit criteria from Release authority.

Use repository GitHub conventions for labels, priority, area, release, roadmap step, status, and milestone association.

Do not create WP15.

Do not create implementation.

Do not create CI.

Do not start WP01.

Return the complete **Release 0.9 GitHub Planning Execution Report**.

Finish with exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

If complete, identify WP01 as next.

Do not execute it.

---

# Conclusion

This planning activity translates already-approved Release 0.9 engineering authority into GitHub execution objects.

GitHub issues are not allowed to redefine the release. They are operational representations of the execution plan.

The correct flow is:

```text
Authoritative Execution Plan + File Manifest
                ↓
        Release 0.9 Milestone
                ↓
          14 WP Issues
                ↓
      Roadmap / Project Metadata
                ↓
       Verified Planning State
                ↓
      WP01 Authorized to Begin
```

> **The execution plan defines the work; GitHub only makes that work traceable and executable.**
