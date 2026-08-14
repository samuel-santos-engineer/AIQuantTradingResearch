# Codex Execution Prompt — Release 0.9 Git/GitHub Integration

## Purpose

Integrate the already accepted Release 0.9 candidate into Git/GitHub without changing its technical contents.

Prerequisite:

```text
WP14 = RELEASE 0.9 ACCEPTED
```

WP14 proved the cumulative WP01–WP13 candidate is coherent, scope/architecture compliant, fully tested, documented, and ready for Git/GitHub integration.

This execution is limited to:

```text
candidate reconciliation
→ acceptance revalidation
→ integration branch
→ exact staging
→ commit
→ post-commit validation
→ push
→ pull request
→ PR/check inspection
→ STOP before merge
```

Do not implement or fix product behavior during integration.

## Authorities

Read completely:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/release-0.9-github-integration-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Review all Release 0.9 prompt/chat and unblock authorities in:

```text
docs/roadmap/release-0.9/prompts/
```

Inspect current Release 0.9 GitHub milestone/issues and existing PRs read-only.

## Accepted WP14 Baseline

```text
main == origin/main
ahead/behind = 0/0
staged = none

62 cumulative changed/untracked files understood
unexpected = 0

Domain.Tests           11/11
Application.Tests      12/12
Infrastructure.Tests    9/9
Architecture.Tests      9/9
Total                  41/41

restore                PASS
format                 PASS
build                  PASS — 0 errors
eng/verify.ps1         PASS
Worker acceptance      PASS
git diff --check       PASS
```

Canonical Worker result:

```text
Target: SAMPLE-USD
Observation count: 3
Mean price: 110.00
Exit: 0
```

The current tree may additionally contain this integration prompt and its intentional `-chat` companion. Those are authorized governance artifacts.

No other unexplained post-WP14 delta is authorized.

## Safety Rule

The accepted candidate must not drift.

Before Git mutation:

1. capture complete repository state;
2. reconcile every changed/untracked file;
3. compare against the Release 0.9 manifest and WP14 baseline;
4. permit the two new integration governance files;
5. re-run acceptance-critical validation;
6. stop on any unexplained or substantive technical change.

Never "fix while integrating."

## Authentication

Run:

```text
gh auth status
```

Require an authenticated active account with sufficient repository/PR/check access.

Never print tokens.

Authentication failure or insufficient access =>

```text
GITHUB INTEGRATION BLOCKED
```

## Repository Preflight

Run:

```text
git rev-parse --show-toplevel
git remote -v
git branch --show-current
git rev-parse HEAD
git rev-parse origin/main
git rev-list --left-right --count origin/main...HEAD
git status --short
git diff --stat
git diff --name-status
git diff --cached -- .
```

Require:

```text
branch = main
HEAD = origin/main
ahead/behind = 0/0
staged = none
```

The cumulative Release 0.9 working tree is intentionally uncommitted; do not require it to be clean.

## Candidate Classification

Classify every changed/untracked file as:

```text
AUTHORIZED RELEASE 0.9
AUTHORIZED PROMPT/CHAT GOVERNANCE
AUTHORIZED TESTABILITY UNBLOCK
AUTHORIZED INTEGRATION PROMPT/CHAT
UNEXPECTED
```

Expected:

```text
UNEXPECTED = 0
```

`*-codex-prompt-chat.md` artifacts are intentionally preserved reference/governance files.

Do not delete them because of their suffix.

Reconcile the actual delta against:

```text
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
```

No unexplained file is allowed.

Confirm no unauthorized change exists to protected infrastructure such as:

```text
global.json
Directory.Build.props
Directory.Packages.props
AIQuantTradingResearch.slnx
eng/**
.github/**
```

## Pre-Integration Revalidation

Before branch creation run:

```text
dotnet restore AIQuantTradingResearch.slnx
dotnet build AIQuantTradingResearch.slnx --no-restore
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff --check
dotnet run --project src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --no-build
```

Require zero build errors, all tests passing, verification exit 0, diff check PASS, and canonical Worker result:

```text
SAMPLE-USD
3 observations
110.00 mean
exit 0
```

Known NU1900 connectivity warnings are non-blocking if validation succeeds.

Any acceptance-critical failure => stop before branch/commit/push and return `GITHUB INTEGRATION BLOCKED`.

## Integration Branch

Preferred branch:

```text
release/0.9-research-platform
```

First check whether it exists locally/remotely.

If it exists, inspect it. Never overwrite/reset/delete it automatically. If its ownership/state is ambiguous, stop.

If absent:

```text
git switch -c release/0.9-research-platform
```

Confirm it was created from the accepted `main` HEAD and the working-tree candidate was preserved.

## Exact Staging

Stage only authorized Release 0.9 files, including the intentional prompt/chat governance artifacts.

Inspect:

```text
git status --short
git diff --cached --stat
git diff --cached --name-status
git diff --cached --check
```

Require:

```text
all authorized candidate files staged
no authorized candidate file omitted
no unauthorized file staged
cached diff check PASS
```

Do not commit until staging is fully reconciled.

## Commit

Create one coherent integration commit unless repository authority explicitly requires another structure.

Preferred message:

```text
feat: implement Release 0.9 research platform
```

Commit only the accepted candidate.

Then inspect:

```text
git status --short
git log -1 --oneline
git show --stat --oneline HEAD
```

All Release 0.9 prompt/chat artifacts are expected to be committed.

## Post-Commit Validation

Run:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff HEAD^ HEAD --check
```

Confirm the committed Worker still reproduces the canonical result when needed.

If committed validation fails:

```text
do not silently amend/fix
do not push
return GITHUB INTEGRATION BLOCKED
```

## Push

After successful validation:

```text
git push -u origin release/0.9-research-platform
```

Confirm local and remote branch commits match and tracking is configured.

Never force-push.

A conflicting pre-existing remote branch must be inspected rather than overwritten.

## Pull Request Preflight

Inspect:

```text
gh pr list --state open
gh pr list --state all --head release/0.9-research-platform
```

Do not create a duplicate PR.

Reuse an existing PR only if it clearly represents this exact integration branch/candidate.

## Pull Request

If needed create:

```text
base: main
head: release/0.9-research-platform
title: Release 0.9 — Research Platform
```

Use an evidence-based body containing:

```text
## Summary
- Implements the bounded Release 0.9 research vertical slice.
- Adds Domain, Application, Infrastructure, Worker, testing, architecture, and documentation alignment.
- Preserves deterministic offline execution.

## Architecture
- Domain -> none
- Application -> Domain
- Infrastructure -> Application
- Worker -> Application + Infrastructure
- Cycles: 0

## Validation
- Domain.Tests: 11/11
- Application.Tests: 12/12
- Infrastructure.Tests: 9/9
- Architecture.Tests: 9/9
- Total: 41/41
- eng/verify.ps1: PASS
- Worker: SAMPLE-USD / 3 observations / mean 110.00
- git diff --check: PASS

## Acceptance
WP14 result: RELEASE 0.9 ACCEPTED
```

Use actual reconciled values if they legitimately differ.

Do not claim remote CI passed until observed.

## PR Inspection

After creating/reusing the PR, capture:

```text
PR number
PR URL
title
base
head
state
mergeability if available
review state
checks/status
```

Use appropriate `gh pr view` and `gh pr checks` commands.

Pending checks must be reported as pending.

Do not bypass required checks.

## Mandatory Merge Gate

STOP BEFORE MERGE.

Even if every check passes and the PR is mergeable, do not:

```text
gh pr merge
git merge
enable auto-merge
```

Explicit human authorization is required for the merge.

## GitHub Planning Protection

Do not:

```text
close WP01–WP14 issues
edit issue bodies
change labels
change project state
close/edit Release 0.9 milestone
create tags
create GitHub Release
start Release 1.0 planning
```

Read-only inspection is allowed.

Issue/milestone closure belongs to a later post-merge closure step.

## Prohibited Operations

Do not:

- modify production behavior;
- modify tests;
- modify WP13 documentation;
- add packages/projects;
- modify protected build/engineering assets;
- reformat unrelated files;
- delete prompt-chat artifacts;
- use `git reset --hard`;
- use `git clean`;
- force-push;
- silently amend/fix a failed committed candidate;
- merge the PR;
- enable auto-merge;
- mutate issues/milestone/project;
- tag/create a release;
- start Release 1.0.

## Stop Conditions

Return:

```text
GITHUB INTEGRATION BLOCKED
```

for:

```text
unexpected main/origin divergence
unexplained file
manifest mismatch
validation failure
Worker acceptance failure
unauthorized scope/dependency change
GitHub authentication failure
ambiguous branch collision
staging mismatch
post-commit validation failure
push failure
unsafe PR conflict
authority conflict
```

Do not resolve substantive conflicts by changing the accepted candidate.

## Decision Model

Return exactly one:

```text
GITHUB INTEGRATION READY FOR MERGE AUTHORIZATION
GITHUB INTEGRATION READY WITH ACTIONS
GITHUB INTEGRATION BLOCKED
```

Use `GITHUB INTEGRATION READY FOR MERGE AUTHORIZATION` when the exact candidate is reconciled, revalidated, branched, staged, committed, revalidated, pushed, represented by a PR, and inspected without candidate drift or prohibited GitHub mutation.

Use `GITHUB INTEGRATION READY WITH ACTIONS` when only non-blocking external actions such as pending required checks/reviews remain before merge authorization.

Use `GITHUB INTEGRATION BLOCKED` for mandatory defects or unsafe state.

## Required Report

Return:

```text
Release 0.9 Git/GitHub Integration Report
```

with:

1. Executive Summary
2. Authority Review
3. Authentication Preflight
4. Initial Repository State
5. Candidate Reconciliation
6. Manifest Reconciliation
7. Pre-Integration Validation
8. Branch Creation
9. Staging Review
10. Commit
11. Post-Commit Validation
12. Push
13. Pull Request
14. PR Checks / Review State
15. GitHub Planning Protection
16. Candidate Integrity
17. Final Local/Remote Git State
18. Findings
19. Merge Authorization Gate
20. Final Decision
21. Next Authorized Action

Candidate Integrity must explicitly answer:

```text
Did integration change accepted production behavior?
Did integration change tests?
Did integration change architecture?
Did integration change WP13 documentation?
Did integration introduce unauthorized files?
Is the pushed commit the accepted Release 0.9 candidate plus authorized integration governance artifacts only?
```

The Merge Authorization Gate must explicitly state:

```text
PR merge performed: NO
Auto-merge enabled: NO
Merge authorization requested from user: YES
```

If checks/reviews remain, identify them exactly.

## Acceptance Checklist

Before a ready decision confirm:

- [ ] WP14 `RELEASE 0.9 ACCEPTED` prerequisite verified.
- [ ] GitHub authentication verified without exposing secrets.
- [ ] Main/HEAD/origin baseline captured.
- [ ] Every changed/untracked file classified.
- [ ] Prompt-chat artifacts preserved.
- [ ] Integration prompt/chat artifacts authorized.
- [ ] Manifest reconciled.
- [ ] No unexplained file exists.
- [ ] Pre-integration validation passes.
- [ ] Expected 41 tests pass or any difference is reconciled.
- [ ] Worker canonical acceptance passes.
- [ ] `git diff --check` passes.
- [ ] Dedicated integration branch created/reused safely.
- [ ] Exact candidate staged.
- [ ] No unauthorized file staged.
- [ ] Cached diff check passes.
- [ ] One coherent commit created.
- [ ] Post-commit validation passes.
- [ ] No force-push used.
- [ ] Remote branch pushed.
- [ ] Duplicate PR check performed.
- [ ] PR created/reused against main.
- [ ] PR evidence is accurate.
- [ ] PR checks/review state inspected.
- [ ] No issue/milestone/project mutation performed.
- [ ] No tag/GitHub Release created.
- [ ] Release 1.0 not started.
- [ ] PR not merged.
- [ ] Auto-merge not enabled.
- [ ] Final local/remote state captured.
- [ ] Candidate integrity confirmed.
- [ ] Merge authorization gate reported.

# Final Instruction

Transport the exact WP14-accepted Release 0.9 candidate into a reviewable GitHub PR.

The authorized sequence is:

```text
accepted candidate
      ↓
reconcile + revalidate
      ↓
release/0.9-research-platform
      ↓
exact stage + commit
      ↓
post-commit validation
      ↓
push
      ↓
PR to main
      ↓
inspect checks/review
      ↓
STOP — HUMAN MERGE AUTHORIZATION REQUIRED
```

Do not merge.

Do not close issues or the milestone.

Do not start Release 1.0.

Finish with exactly one:

```text
GITHUB INTEGRATION READY FOR MERGE AUTHORIZATION
GITHUB INTEGRATION READY WITH ACTIONS
GITHUB INTEGRATION BLOCKED
```

> Integration must transport what WP14 accepted into Git history without changing what WP14 accepted.
