# Codex Execution Prompt — Release 0.8 Closure

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.8 — Solution Skeleton |
| Activity | Formal Release Closure |
| Work Package | None — this is a post-WP15 release-governance activity |
| Primary Agent | Codex |
| Prerequisite | WP15 — Release Acceptance Review = `ACCEPTED WITH ACTIONS` |
| Acceptance Issue | #66 — WP15 Release Acceptance Review |
| Milestone | #39 — Phase 2 - Release 0.8: Solution Skeleton |
| Integration Baseline | `main` synchronized with `origin/main` |
| Expected Outcome | Version the remaining WP15 governance artifacts, record the accepted WP15 result, integrate the closure delta through the governed GitHub workflow, close the WP15 issue and Release 0.8 milestone only after successful integration, and record Release 0.8 as formally closed without beginning Release 0.9 |

---

## Purpose

Formally close **Phase 2 — Release 0.8: Solution Skeleton** after the successful WP15 acceptance review.

WP15 concluded:

```text
ACCEPTED WITH ACTIONS
```

The remaining actions are administrative and governance-related, not technical remediation.

The accepted Release 0.8 technical state has already passed:

```text
8 solution projects
4 production projects
4 test projects
accepted production dependency graph
0 dependency cycles
Architecture.Tests = 7/7
eng/verify.ps1 = PASS
eng/build.sh = PASS
clean reconstruction = PASS
documentation alignment = PASS
GitHub integration = PASS
no premature Release 0.9 implementation
```

This closure activity must preserve that accepted state.

It is not WP16.

It must not extend the Release 0.8 execution plan.

---

# 1. Objective

Complete the formal governance closure of Release 0.8.

The closure sequence is:

```text
WP15 accepted
      ↓
preserve/version WP15 authority artifacts
      ↓
record WP15 acceptance outcome
      ↓
create governed closure branch
      ↓
validate exact closure delta
      ↓
run Release 0.8 verification
      ↓
commit
      ↓
push
      ↓
create closure PR
      ↓
merge only through authorized governance path
      ↓
synchronize main
      ↓
close/update issue #66
      ↓
close milestone #39
      ↓
record final Release 0.8 status
      ↓
Release 0.8 CLOSED
```

Do not begin Release 0.9.

---

# 2. Authority

Read completely before taking action:

```text
docs/roadmap/release-0.8/prompts/release-0.8-closure-codex-prompt.md
docs/roadmap/release-0.8/prompts/15-release-acceptance-review-codex-prompt.md
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Review repository Git/GitHub governance that actually exists, including relevant:

```text
.github/**
CONTRIBUTING.md
docs/**
eng/**
```

Use authenticated GitHub state as evidence.

Do not expose tokens, credentials, secrets, or sensitive authentication details.

---

# 3. Accepted WP15 Baseline

WP15 reported:

```text
Technical result: PASS
Governance result: PASS with closure actions
Release decision: ACCEPTED WITH ACTIONS
Blockers: none
Technical risks: none
```

Expected current local state before closure:

```text
Branch: main
main = origin/main
Tracked changes: none
Staged changes: none
```

Expected untracked WP15 governance artifacts:

```text
docs/roadmap/release-0.8/prompts/15-release-acceptance-review-codex-prompt.md
docs/roadmap/release-0.8/prompts/15-release-acceptance-review-codex-prompt-chat.md
```

These files are intended Release 0.8 governance artifacts and should be considered for inclusion in the closure delta.

Verify actual state rather than assuming it.

---

# 4. Required Closure Actions

The WP15 acceptance report identified the remaining closure actions:

```text
1. Record the WP15 acceptance result.
2. Version or otherwise disposition the two WP15 authority files.
3. Close issue #66 and mark its roadmap item Done.
4. Close milestone #39.
5. Record the final Release 0.8 status.
```

This prompt authorizes Codex to execute these actions only through the controlled sequence defined here.

Do not perform milestone closure before the closure integration is safely complete.

---

# 5. Initial State Verification

Before mutation, record:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
git remote -v
git rev-list --left-right --count main...origin/main
dotnet --version
gh auth status
```

Verify:

```text
repository identity
current branch
local/remote synchronization
working tree
GitHub authentication
milestone #39
issue #66
current roadmap/project state for #66 when accessible
existing PRs relevant to closure
```

Do not expose the authentication token.

If `main` is not synchronized with `origin/main`, stop and reconcile safely before creating the closure branch.

Do not overwrite user work.

---

# 6. WP15 Result Verification

Before closing anything, confirm the accepted WP15 facts from repository and GitHub evidence:

```text
WP01–WP14 accepted
WP14 PR #67 merged
main synchronized
8 projects
accepted production graph
0 cycles
Architecture.Tests 7/7
verify PASS
no Release 0.9 scope leakage
```

Confirm issue #66 represents WP15.

Confirm milestone #39 represents Release 0.8.

If these identities do not match, return `BLOCKED`.

---

# 7. Closure Artifact Plan

The closure delta should remain minimal.

Expected files to version:

```text
docs/roadmap/release-0.8/prompts/15-release-acceptance-review-codex-prompt.md
docs/roadmap/release-0.8/prompts/15-release-acceptance-review-codex-prompt-chat.md
docs/roadmap/release-0.8/prompts/release-0.8-closure-codex-prompt.md
```

The closure prompt itself is part of Release 0.8 governance history and is authorized for versioning.

If an additional small release-status artifact is already required by repository authority, update only that authoritative artifact.

Do not invent a new status file merely to satisfy this prompt.

Before staging, classify every working-tree item as:

```text
RELEASE-0.8 CLOSURE
UNRELATED USER WORK
GENERATED
AMBIGUOUS
```

Do not stage anything outside the approved closure delta.

---

# 8. Recording the WP15 Acceptance Result

Record the WP15 result using the repository's existing governance model.

The result to record is:

```text
WP15 — Release Acceptance Review
Decision: ACCEPTED WITH ACTIONS
Technical blockers: none
Technical risks: none
Closure readiness: ready after administrative closure actions
```

After this closure activity completes, the final release status becomes:

```text
Phase 2 — Release 0.8: Solution Skeleton
Status: COMPLETE / CLOSED
```

Use existing repository conventions for status recording.

Do not create a new release-management architecture.

Do not alter the technical meaning of the WP15 report.

---

# 9. Branch Strategy

Use repository-defined branch conventions.

Create a dedicated forward-only closure branch from synchronized `main`.

Preferred semantic intent:

```text
release-0.8 closure governance
```

Do not use the previous WP14 branch.

Do not rewrite existing Release 0.8 history.

Record:

```text
base branch
base commit
closure branch
branch creation command
```

If repository governance defines the exact branch naming pattern, follow it.

---

# 10. No Technical Implementation Changes

The closure branch must not change:

```text
production source
test source
architecture tests
project files
solution membership
project references
Worker behavior
DI boundaries
engineering scripts
root build configuration
package configuration
SDK configuration
CI
Docker
future release implementation
```

If a technical change appears necessary, stop.

Release closure is not authorized to repair implementation.

Return `BLOCKED` and identify the required follow-up.

---

# 11. Pre-Staging Release Verification

Before staging the closure delta, run:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Require:

```text
Exit Status = 0
Build errors = 0
Architecture.Tests = 7/7
```

Run:

```text
eng/build.sh
```

through the supported shell when safely available.

Expected:

```text
PASS
```

If an environment limitation prevents the shell build, report it accurately.

Do not modify repository implementation to work around an environmental limitation.

---

# 12. Staging Contract

Build an explicit inclusion/exclusion plan first.

Then stage only authorized closure files.

Inspect:

```text
git status --short
git diff --cached --stat
git diff --cached
```

Require:

```text
no production changes
no test changes
no build/script changes
no generated outputs
no secrets
no credentials
no unrelated files
no unexpected binaries
```

If the staged delta is not exact, correct it safely before proceeding.

Do not use destructive Git cleanup.

---

# 13. Commit Contract

Use repository commit conventions.

The commit must represent only Release 0.8 closure governance.

A suitable semantic intent is:

```text
docs: record Release 0.8 acceptance and closure
```

Use the actual repository convention if it differs.

Before commit, confirm:

```text
verification PASS
staged scope exact
no unrelated changes
```

After commit, record:

```text
commit hash
subject
files committed
```

Do not amend or rewrite existing history.

---

# 14. Push Contract

Before push, verify:

```text
remote identity
closure branch
commit
upstream
```

Push the closure branch.

Do not force push.

Do not push the closure commit directly to `main`.

Record the remote branch state.

---

# 15. Closure Pull Request

Create a PR from the closure branch to `main` using repository conventions and the existing PR template.

The PR must clearly state:

```text
Purpose:
  Formal Release 0.8 governance closure

Technical changes:
  None

Governance artifacts:
  WP15 authoritative prompt
  WP15 Codex chat/bootstrap prompt
  Release 0.8 closure prompt
  any explicitly authorized existing status update

WP15 decision:
  ACCEPTED WITH ACTIONS

Release 0.8 technical state:
  8 projects
  accepted graph
  0 cycles
  Architecture.Tests 7/7
  verify PASS
  shell build PASS when supported

Out of scope:
  Release 0.9
  CI
  plugin framework
  product features
  release tagging/publishing unless separately authorized
```

Do not fabricate hosted checks or review approvals.

---

# 16. PR Review and Merge Gate

Do not close issue #66 or milestone #39 while the closure PR is still unmerged.

Inspect actual PR state.

If repository governance allows Codex to merge and the PR is mergeable with all required checks/reviews satisfied, Codex may merge only when explicitly permitted by the repository's normal governance rules.

If human review/approval is required:

```text
do not bypass it
do not self-approve
do not fabricate it
```

Return `COMPLETE WITH ACTIONS` with the exact remaining human action.

If Codex cannot safely merge, stop before issue/milestone closure.

---

# 17. Post-Merge Synchronization

After the closure PR is actually merged:

```text
switch to main
pull origin main
verify main = origin/main
```

Then run:

```text
git status --short
```

Require no unexpected working-tree changes.

Re-run the canonical verification when practical:

```text
eng/verify.ps1
```

Confirm the merged closure did not affect the accepted technical baseline.

---

# 18. Close WP15 Issue #66

Only after the closure PR is merged and `main` is synchronized:

1. Record the WP15 acceptance result on issue #66 using repository conventions.
2. Mark the roadmap/project item Done when the project model supports it.
3. Close issue #66.

The closure note should communicate:

```text
WP15 decision: ACCEPTED WITH ACTIONS
all mandatory technical/governance acceptance criteria passed
closure governance artifacts integrated
remaining closure administration completed by this closure activity
Release 0.8 ready for milestone closure
```

Do not alter unrelated issue metadata.

Verify issue #66 is actually closed.

---

# 19. Close Milestone #39

Only after:

```text
closure PR merged
main synchronized
issue #66 closed
no other substantive Release 0.8 issue remains open
```

close:

```text
Milestone #39
Phase 2 - Release 0.8: Solution Skeleton
```

Before closing, inspect the milestone one final time.

Require:

```text
Open substantive Release 0.8 issues = 0
```

If another substantive issue remains open, do not close the milestone.

Return `BLOCKED` or `COMPLETE WITH ACTIONS` according to whether the remaining issue requires engineering remediation or administrative action.

Verify milestone #39 is actually closed after mutation.

---

# 20. Record Final Release 0.8 Status

Use the repository's existing roadmap/status mechanism when one exists.

Record:

```text
Phase 2 — Release 0.8: Solution Skeleton
Final Status: COMPLETE
Acceptance: ACCEPTED
Milestone: #39 CLOSED
Technical blockers: none
Technical risks: none
```

Important semantic rule:

WP15's historical decision remains:

```text
ACCEPTED WITH ACTIONS
```

because actions were outstanding at the time of review.

After those actions are completed by this closure activity, the final Release 0.8 closure status may be recorded as:

```text
COMPLETE / CLOSED
```

Do not rewrite WP15 history to pretend its original decision was `ACCEPTED`.

---

# 21. Tag and GitHub Release Boundary

This closure prompt does not automatically authorize:

```text
Git tag
GitHub Release
package publishing
deployment
artifact publishing
release notes publication
```

If the execution plan explicitly requires one of these for Release 0.8 closure, report the requirement and follow only explicit authority.

Otherwise do not create them.

---

# 22. Release 0.9 Boundary

Do not begin Release 0.9.

Specifically, do not:

```text
create CI workflows
implement quality gates owned by Release 0.9
create plugin framework code
change architecture for future work
create Release 0.9 projects
implement product features
```

After Release 0.8 closes, merely identify the next authoritative transition.

Do not execute it.

---

# 23. Failure and Stop Conditions

Stop and return `BLOCKED` when:

```text
WP15 acceptance evidence cannot be verified
issue #66 identity is wrong
milestone #39 identity is wrong
mandatory Release 0.8 validation fails
technical repository mutation is required
closure delta contains unexplained implementation changes
main cannot be safely synchronized
GitHub state conflicts materially with repository authority
```

Return `COMPLETE WITH ACTIONS` when:

```text
technical and closure preparation are valid
but a required external/human governance action remains
```

Examples:

```text
human PR approval required
PR merge requires manual action
project-item state cannot be changed by available tooling
```

Do not use `COMPLETE WITH ACTIONS` to hide a technical failure.

---

# 24. Closure Decision Model

Return exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

## COMPLETE

Use when:

```text
closure artifacts integrated
closure PR merged
main synchronized
issue #66 closed
roadmap item Done where applicable
milestone #39 closed
final Release 0.8 status recorded
technical baseline preserved
Release 0.9 not started
```

## COMPLETE WITH ACTIONS

Use when no blocker exists but a clearly identified external/manual governance action remains.

## BLOCKED

Use when a mandatory closure criterion cannot be satisfied safely.

---

# 25. Acceptance Criteria

Release 0.8 closure is complete only when all applicable mandatory criteria pass:

- [ ] Closure prompt and Release 0.8 authority reviewed.
- [ ] Initial local Git state recorded.
- [ ] Authenticated GitHub state recorded.
- [ ] `main` synchronized with `origin/main` before branch creation.
- [ ] WP15 acceptance result verified.
- [ ] WP14 PR #67 merge verified.
- [ ] Issue #66 identity verified.
- [ ] Milestone #39 identity verified.
- [ ] WP15 authority files classified as closure governance artifacts.
- [ ] Closure prompt classified as a closure governance artifact.
- [ ] No unrelated user work included.
- [ ] Dedicated forward-only closure branch used.
- [ ] No existing history rewritten.
- [ ] No technical implementation change introduced.
- [ ] Pre-staging `eng/verify.ps1` passed.
- [ ] Architecture.Tests remained 7/7.
- [ ] Shell build passed when supported.
- [ ] Staged delta contained only authorized closure governance changes.
- [ ] No generated files, secrets, credentials, or unrelated artifacts staged.
- [ ] Closure commit followed repository conventions.
- [ ] Closure branch pushed without force.
- [ ] Closure PR created using repository conventions.
- [ ] PR accurately stated zero technical changes.
- [ ] No fabricated checks/reviews were reported.
- [ ] Required human review was not bypassed.
- [ ] Closure PR merged before issue/milestone closure.
- [ ] Post-merge `main` synchronized with `origin/main`.
- [ ] Accepted technical baseline remained valid after merge.
- [ ] WP15 result recorded.
- [ ] Issue #66 closed.
- [ ] Roadmap/project item marked Done when applicable and accessible.
- [ ] No other substantive Release 0.8 issue remained open.
- [ ] Milestone #39 closed.
- [ ] Final Release 0.8 status recorded as COMPLETE/CLOSED.
- [ ] WP15 historical decision remained accurately recorded as `ACCEPTED WITH ACTIONS`.
- [ ] No unauthorized tag or GitHub Release created.
- [ ] No Release 0.9 work started.
- [ ] Final local and remote state recorded.
- [ ] Complete closure report produced.

---

# 26. Expected Output Contract

Return one complete **Release 0.8 Closure Execution Report**.

Do not create a report file unless separately authorized.

Use this structure.

# Release 0.8 Closure Execution Report

## 1. Executive Summary

State:

```text
Release:
WP15 decision:
Closure objective:
Closure integration result:
Issue #66 result:
Milestone #39 result:
Final Release 0.8 status:
Final decision:
```

## 2. Execution Context

```text
Repository:
Starting Branch:
Starting HEAD:
Initial Working Tree:
Remote:
Configured SDK:
Effective SDK:
GitHub Authentication:
```

Do not expose credentials.

## 3. Authoritative Sources Reviewed

List exact repository paths and GitHub objects used.

## 4. WP15 Acceptance Verification

```text
Decision:
Technical blockers:
Technical risks:
Closure actions:
Assessment:
```

## 5. Initial Git / GitHub State

```text
main:
origin/main:
ahead/behind:
issue #66:
milestone #39:
existing closure PR:
```

## 6. Closure Artifact Classification

| Path | Classification | Included | Reason |
| --- | --- | --- | --- |

## 7. Closure Branch

```text
Base:
Base commit:
Branch:
Creation result:
Assessment:
```

## 8. Pre-Staging Validation

```text
Verify:
Architecture.Tests:
Build errors:
Shell build:
Assessment:
```

## 9. Staged Delta

```text
Files staged:
Technical files changed:
Generated files:
Unrelated files:
Secrets/credentials:
Assessment:
```

## 10. Closure Commit

```text
Created:
Hash:
Subject:
Files:
Assessment:
```

## 11. Push

```text
Performed:
Remote:
Branch:
Force push:
Result:
Assessment:
```

## 12. Closure Pull Request

```text
Created:
Number:
URL/reference:
Base:
Head:
Title:
Technical changes:
Governance changes:
Checks:
Review state:
Mergeability:
Assessment:
```

## 13. Merge

```text
Merged:
Merge method:
Merge commit:
Required review satisfied:
Assessment:
```

If not merged, explain exact remaining action.

## 14. Post-Merge Synchronization

```text
Local main:
origin/main:
ahead/behind:
Working tree:
Assessment:
```

## 15. Post-Merge Technical Verification

```text
Solution projects:
Production graph:
Cycles:
Architecture.Tests:
Verify:
Assessment:
```

## 16. WP15 Issue Closure

```text
Issue:
Acceptance result recorded:
Roadmap/project state:
Closed:
Assessment:
```

## 17. Milestone Closure

```text
Milestone:
Open substantive issues before closure:
Closed:
Assessment:
```

## 18. Final Release Status

```text
Release:
WP15 historical decision:
Final closure status:
Technical blockers:
Technical risks:
Assessment:
```

## 19. Tag / GitHub Release

```text
Tag created:
GitHub Release created:
Authority:
Assessment:
```

## 20. Release 0.9 Boundary

```text
Release 0.9 work started:
CI created:
Plugin framework created:
Future implementation created:
Assessment:
```

## 21. Final Git State

```text
Branch:
HEAD:
origin/main:
git status --short:
Staged changes:
Uncommitted changes:
```

## 22. Final GitHub State

```text
Issue #66:
Milestone #39:
Closure PR:
Default branch:
Release 0.8 status:
```

## 23. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 24. Scope Compliance

| Scope Check | Result | Evidence |
| --- | --- | --- |
| Governance-only closure delta | PASS/FAIL | |
| No technical implementation changes | PASS/FAIL | |
| WP15 result preserved accurately | PASS/FAIL | |
| No history rewrite | PASS/FAIL | |
| No force push | PASS/FAIL | |
| Verification passed | PASS/FAIL | |
| Architecture.Tests 7/7 | PASS/FAIL | |
| Closure PR governed | PASS/FAIL | |
| Issue #66 handled after merge | PASS/FAIL/N/A | |
| Milestone #39 handled after issue closure | PASS/FAIL/N/A | |
| No unauthorized tag/release | PASS/FAIL | |
| Release 0.9 not started | PASS/FAIL | |

## 25. Findings

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 26. Closure Criteria Matrix

Reproduce applicable closure acceptance criteria with:

```text
PASS
FAIL
N/A
```

## 27. Final Decision

State exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

Explain why.

## 28. Release 0.8 Final State

State explicitly whether:

```text
Phase 2 — Release 0.8: Solution Skeleton
```

is now formally:

```text
COMPLETE / CLOSED
```

## 29. Next Authoritative Step

Identify the next authoritative transition from repository roadmap authority.

It is expected to be preparation for the next planned release after 0.8.

Confirm rather than assume.

Do not begin it.

---

# 27. Prohibited Behaviors

Do not:

- call this WP16;
- extend the Release 0.8 execution plan;
- modify production implementation;
- modify tests;
- modify architecture tests;
- modify project references;
- modify the solution structure;
- modify Worker/DI behavior;
- modify engineering scripts;
- modify root build/package/SDK configuration;
- create CI;
- implement Release 0.9;
- stage unrelated work;
- include generated outputs;
- expose credentials;
- rewrite history;
- force push;
- bypass required review;
- fabricate checks;
- close issue #66 before the closure integration is merged;
- close milestone #39 while substantive Release 0.8 issues remain open;
- rewrite WP15's historical `ACCEPTED WITH ACTIONS` decision;
- create a tag or GitHub Release without explicit authority;
- begin the next release.

---

# 28. Completion Model

```text
Read Closure Authority
        ↓
Verify WP15 Acceptance
        ↓
Verify Local + GitHub Baseline
        ↓
Classify Closure Artifacts
        ↓
Create Forward-Only Closure Branch
        ↓
Run Release 0.8 Verification
        ↓
Stage Exact Governance Delta
        ↓
Inspect Staged Diff
        ↓
Commit
        ↓
Push
        ↓
Create Closure PR
        ↓
Review / Merge Gate
        ↓
Synchronize Main
        ↓
Revalidate Technical Baseline
        ↓
Record WP15 Result
        ↓
Close Issue #66
        ↓
Close Milestone #39
        ↓
Record Release 0.8 COMPLETE
        ↓
COMPLETE | COMPLETE WITH ACTIONS | BLOCKED
```

---

# 29. Final Instruction

Execute the formal closure of:

```text
Phase 2 — Release 0.8: Solution Skeleton
```

against the actual current `AIQuantTradingResearch` repository and GitHub state.

This is a release-governance closure activity, not WP16.

Read the closure prompt, WP15 prompt, Release 0.8 execution plan, manifest, and relevant repository governance completely.

Verify the WP15 result:

```text
ACCEPTED WITH ACTIONS
```

and confirm no technical blocker or technical risk exists.

Verify `main` is synchronized with `origin/main`.

Version the authorized WP15 governance artifacts and this closure prompt through a dedicated forward-only closure branch.

Run the canonical Release 0.8 verification before integration.

Stage only the exact governance closure delta.

Commit using repository conventions.

Push without force.

Create a closure PR to `main`.

Do not claim reviews or checks that did not occur.

Do not bypass human review when required.

Do not close issue #66 or milestone #39 until the closure PR is actually merged.

After merge:

```text
synchronize main
revalidate Release 0.8
record WP15 outcome
close/update issue #66
mark roadmap item Done when applicable
verify no substantive Release 0.8 issue remains open
close milestone #39
record Release 0.8 as COMPLETE / CLOSED
```

Preserve WP15's historical decision as:

```text
ACCEPTED WITH ACTIONS
```

Do not rewrite it to `ACCEPTED`.

Do not create CI.

Do not create a tag or GitHub Release unless explicitly required by repository authority.

Do not begin Release 0.9.

Return the complete **Release 0.8 Closure Execution Report**.

Finish with exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

State explicitly whether Release 0.8 is formally closed.

Identify the next authoritative transition.

Do not begin it.

---

# Conclusion

Release 0.8 closure is the final governance action that converts a successfully accepted engineering baseline into a formally completed release.

The release has already proven:

```text
structure
architecture
buildability
test enforcement
reconstructibility
documentation alignment
GitHub integration
release acceptance
```

The remaining responsibility is to preserve the acceptance evidence, integrate the final governance artifacts, close the authoritative planning objects, and record the release's final state without changing the engineering baseline.

The closure path is therefore:

```text
ACCEPTED WITH ACTIONS
        ↓
Governance Artifacts Versioned
        ↓
Closure PR Integrated
        ↓
WP15 Closed
        ↓
Milestone Closed
        ↓
Release 0.8 COMPLETE
```

Only after this boundary is complete should the project transition to its next release.

> **A release is complete when its engineering state and its governance state reach closure together.**
