# Release 1.0 GitHub Planning --- Codex Prompt

## Authority

Act only as the Release 1.0 GitHub Planning Executor for
`AIQuantTradingResearch`.

Read completely before acting:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md
```

These two files are authoritative. Inspect Release 0.9
milestones/issues, existing labels, and the existing GitHub Project only
to preserve repository conventions. If convention conflicts with Release
1.0 authority, the Release 1.0 authority controls.

This task is derivative, not creative. Translate the approved release
design into GitHub planning state without redesigning the release and
without starting implementation.

## Objective

Create or reconcile exactly one milestone for:

``` text
Phase 3 - Release 1.0: Market Data Foundation
```

and exactly one GitHub issue for each approved work package WP01--WP16.

## Exact Work Packages

``` text
WP01 — Release & Repository Preflight
WP02 — Market Data Provider Discovery
WP03 — Market Data Domain Evolution
WP04 — Market Data Application Contracts
WP05 — Historical Market Data Use-Case Integration
WP06 — Provider Transport Model
WP07 — Provider HTTP Client
WP08 — Market Data Normalization
WP09 — Market Data Validation & Failure Mapping
WP10 — Dependency Registration & Configuration
WP11 — Worker Market Data Execution
WP12 — Domain & Application Tests
WP13 — Infrastructure & Provider Tests
WP14 — Architecture Evolution
WP15 — Documentation Alignment
WP16 — Full Validation, Integration & Acceptance
```

Prefer titles `Release 1.0 WPxx — <approved WP name>`.

Do not merge, split, reorder, rename semantically, or add work packages.

## Authorized GitHub Actions

You may inspect current planning state; create/reconcile the Release 1.0
milestone; create/reconcile the 16 one-to-one WP issues; assign all 16
to the milestone; reuse only accurate existing labels; and add issues to
the existing GitHub Project/populate existing fields only when
established repository conventions make the values unambiguous.

Before creating anything, search open and closed milestones/issues for
duplicates or equivalent objects.

## Prohibited Actions

Do not modify repository files or authorities; create WP prompts; start
implementation; create WP17+; create issues for Git/GitHub Integration,
Human Merge, or Closure; create Release 1.1 planning; alter Release 0.9
planning; close Release 1.0 issues/milestone; create/rename/delete
labels; redesign Project schema; create branches, commits, pushes, PRs,
tags, or GitHub Releases; or change repository
settings/workflows/templates.

The integration, merge, and closure gates are lifecycle gates, not
work-package issues.

## Milestone Contract

Preferred title:

``` text
Phase 3 - Release 1.0: Market Data Foundation
```

Keep it OPEN. Derive its concise description from the execution plan:
first real external historical market-data vertical slice through one
evidence-selected provider, with provider-independent Domain/Application
boundaries and provider mechanics owned by Infrastructure. Do not claim
future capabilities.

## Issue Body Contract

Every issue must contain:

``` text
## Authority
## Objective
## Authorized Scope
## Prohibited Scope
## Dependencies
## Expected Artifacts
## Validation Evidence
## Exit Criteria
```

Reference both authority files in `Authority`. Preserve the exact
semantics of the corresponding WP. Reconcile artifacts with the file
manifest; do not invent requirements or paths.

## Dependency Contract

Preserve exactly:

``` text
WP01 -> WP02 -> WP03 -> WP04
WP04 -> WP05
WP04 -> WP06
WP06 -> WP07 -> WP08 -> WP09
WP05 + WP09 -> WP10 -> WP11
WP03 + WP04 + WP05 -> WP12
WP06 + WP07 + WP08 + WP09 + WP10 -> WP13
WP11 + WP12 + WP13 -> WP14 -> WP15 -> WP16
```

After issue numbers are known, use GitHub issue references in
Dependencies. Add no artificial dependency and omit none.

## Labels and Project

Inspect existing labels first. Reuse only labels whose current semantics
clearly fit. Never create, rename, delete, or repurpose labels. If no
accurate label exists, leave that dimension unlabeled and report it.

If an existing GitHub Project is established for roadmap tracking, add
the 16 issues and set only existing, unambiguous fields/options. Do not
create Projects, fields, options, views, workflows, or automation. If
faithful Project integration is impossible, leave it unchanged and
report why.

## Duplicate and Conflict Protection

Search before create. Detect equivalents despite punctuation
differences. Never create duplicates for convenience.

If an existing Release 1.0 object materially conflicts with the
authorities and cannot be narrowly reconciled as the same planning
object, stop and report a blocker rather than rewriting history or
guessing.

## Required Final State

``` text
Release 1.0 milestone = exactly one and OPEN
authoritative WP issues = exactly 16
WP01–WP16 = each represented exactly once
all 16 issues = OPEN
all 16 issues assigned to Release 1.0 milestone
WP17+ = absent
lifecycle-gate issues = absent
Release 1.1 planning = absent
implementation started = NO
```

## Repository Protection

This execution is GitHub planning only. Human-created planning
prompt/chat files may already exist locally; preserve them exactly. Do
not stage, commit, move, delete, or integrate them.

Preserve unrelated working-tree changes and report them. At completion
prove:

``` text
repository files modified by this execution = 0
branch created = NO
commit created = NO
push performed = NO
PR created = NO
```

## Validation

Verify one correct open milestone; exactly 16 open WP issues assigned to
it; zero WP/lifecycle/Release 1.1 extras; and for every issue,
objective, authorized scope, prohibited scope, dependencies, artifacts,
validation evidence, and exit criteria match the authorities.

Reconstruct the dependency graph from the issues and require drift = 0.

If Project integration is used, verify all 16 issues follow established
Project conventions.

## Blocker Policy

Stop rather than guess on release identity, duplicate/conflicting
planning objects, missing authority, permissions, Project field meaning,
authority conflict, need for new labels/schema, or need to redesign a
WP.

Report evidence, why authority is insufficient, minimum human decision
required, and mutations already performed. Do not work around governance
constraints.

## Required Execution Report

Return:

``` text
# Release 1.0 GitHub Planning Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Authentication / Repository Context
## 4. Existing Planning-State Inspection
## 5. Duplicate Reconciliation
## 6. Milestone Result
## 7. Issue Creation / Reconciliation
## 8. Work-Package Mapping
## 9. Dependency Validation
## 10. Labels
## 11. GitHub Project Integration
## 12. Scope Protection
## 13. Repository Mutation Check
## 14. Findings
## 15. Final GitHub Planning State
## 16. Final Decision
## 17. Next Authorized Action
```

Include a table containing WP, issue number, issue title, milestone,
state, dependencies, labels, and Project status. Report actual GitHub
identifiers/URLs where available. Never claim unperformed actions or
validation.

## Final Decision

Finish with exactly one:

``` text
RELEASE 1.0 GITHUB PLANNING COMPLETE
RELEASE 1.0 GITHUB PLANNING COMPLETE WITH OBSERVATIONS
RELEASE 1.0 GITHUB PLANNING BLOCKED
```

`COMPLETE WITH OBSERVATIONS` is only for non-blocking conditions that do
not alter the approved 16-WP model.

## Next Authorized Action

Success authorizes human review of the GitHub planning state only. It
does not authorize implementation.

After human acceptance, the next separately authorized action is:

``` text
WP01 — Release & Repository Preflight
```

Do not start WP01 or create its implementation prompt in this task.

## Execution Instruction

Read both authorities completely, inspect existing GitHub planning
conventions/state, create or reconcile the milestone and exactly 16 WP
issues, validate zero governance drift, return the required report, and
stop before implementation.
