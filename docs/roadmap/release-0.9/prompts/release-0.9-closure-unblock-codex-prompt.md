# Codex Execution Prompt --- Release 0.9 Closure Unblock

## Purpose

Resolve **only** the two blockers discovered by the authoritative
Release 0.9 post-merge closure gate:

``` text
C09-01 — Canonical verification fails after checkout from merged main
C09-02 — Closure governance prompt/chat artifacts remain untracked
```

This is a narrowly scoped, forward-only Release 0.9 closure correction.

The objective is:

``` text
inspect repository line-ending policy
      ↓
prove root cause
      ↓
apply minimum repository-owned policy correction
      ↓
integrate closure governance artifacts
      ↓
prove fresh-checkout reproducibility
      ↓
run full Release 0.9 acceptance
      ↓
create corrective branch + commit + PR
      ↓
inspect PR
      ↓
STOP FOR HUMAN MERGE
```

Do not implement new product capability.

Do not weaken verification.

Do not begin Release 1.0.

------------------------------------------------------------------------

# 1. Authoritative Blocker Evidence

The Release 0.9 closure report established:

``` text
PR #84 = MERGED
main == origin/main
accepted integration commit landed directly
production architecture = PASS
testability boundaries = PASS
restore = PASS
build = PASS
Domain.Tests = 11/11
Application.Tests = 12/12
Infrastructure.Tests = 9/9
Architecture.Tests = 9/9
total tests = 41/41
Worker acceptance = PASS
GitHub governance = PASS
```

Closure was blocked by exactly:

``` text
C09-01 BLOCKER
Canonical verification fails after checkout from merged main.

Observed:
  eng/verify.ps1 -> FAIL
  dotnet format --verify-no-changes -> ENDOFLINE errors

Root-cause evidence:
  system Git core.autocrlf = true
  repository .editorconfig end_of_line = lf

Observed behavior:
  pre-integration untracked files retained LF
  fresh checkout materialized tracked Release 0.9 C# files as CRLF
  repository format verification then rejected those line endings
```

and:

``` text
C09-02 REQUIRED ACTION
Final working tree is not clean.

Observed:
  release-0.9-closure-codex-prompt.md
  release-0.9-closure-codex-prompt-chat.md

These are intentional Release 0.9 governance artifacts but were still
untracked during the closure run.
```

The previous final decision was:

``` text
RELEASE 0.9 CLOSURE BLOCKED
```

This prompt exists only to remove those blockers safely.

------------------------------------------------------------------------

# 2. Authorities

Read completely before changing anything:

``` text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/release-0.9-github-integration-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-closure-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
.editorconfig
.gitattributes
eng/verify.ps1
```

Review the complete Release 0.9 prompt/chat/unblock governance set
under:

``` text
docs/roadmap/release-0.9/prompts/
```

Also inspect the actual merged files reported by
`dotnet format --verify-no-changes` as having `ENDOFLINE` violations.

Do not assume the desired `.gitattributes` rules before inspecting the
existing repository policy.

------------------------------------------------------------------------

# 3. Authorized Scope

Authorized changes are limited to the minimum required to resolve C09-01
and C09-02.

Potentially authorized:

``` text
.gitattributes
```

only if evidence proves the repository lacks or has insufficient
line-ending policy for the affected tracked files.

Authorized governance artifacts:

``` text
docs/roadmap/release-0.9/prompts/release-0.9-closure-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-closure-codex-prompt-chat.md
docs/roadmap/release-0.9/prompts/release-0.9-closure-unblock-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-closure-unblock-codex-prompt-chat.md
```

The two closure artifacts may currently exist at repository root as
untracked files because they were saved there before the closure run. If
so, move them into the authoritative Release 0.9 prompt directory rather
than duplicating them.

The two closure-unblock artifacts may likewise initially be supplied as
local/untracked inputs. Their governed destination is the same prompt
directory.

Authorized line-ending normalization is limited to files proven
necessary to make repository-authorized checkout and verification
consistent with repository policy.

Do not broadly rewrite the repository merely because normalization is
possible.

------------------------------------------------------------------------

# 4. Prohibited Scope

Do not change:

``` text
research product behavior
Domain model semantics
Application contracts
ResearchUseCase behavior
Infrastructure adapter behavior
dependency registration
Worker behavior/output
test assertions or test semantics
architecture rules
WP13 architecture documentation content
project references
package references
solution membership
SDK version
CI/workflows
GitHub issue bodies
GitHub labels
GitHub milestone semantics
Release 1.0 artifacts
```

Do not solve C09-01 by:

``` text
disabling format verification
removing dotnet format
weakening eng/verify.ps1
changing .editorconfig from LF merely to accommodate one workstation
setting/changing global or system core.autocrlf as the repository solution
requiring each developer to manually configure Git
adding a machine-specific workaround
ignoring ENDOFLINE diagnostics
```

Do not use:

``` text
git reset --hard
git clean
git push --force
git push --force-with-lease
```

------------------------------------------------------------------------

# 5. Core Engineering Principle

The repository must be reproducible independently of a developer's
global Git configuration.

The repository already declares:

``` text
.editorconfig:
  end_of_line = lf
```

The correction should make Git checkout behavior and repository
formatting policy agree.

The expected mechanism may be `.gitattributes`, but that conclusion must
be confirmed from repository evidence.

Prefer repository-owned policy over workstation-owned configuration.

------------------------------------------------------------------------

# 6. Authentication and Repository Preflight

Run:

``` text
gh auth status
git rev-parse --show-toplevel
git remote -v
git branch --show-current
git rev-parse HEAD
git rev-parse origin/main
git rev-list --left-right --count origin/main...HEAD
git status --short
```

Require an authenticated GitHub account with repository/PR access.

Expected baseline:

``` text
branch = main
HEAD = origin/main
ahead/behind = 0/0
```

The only expected local untracked inputs are the authorized
closure/unblock governance artifacts.

If other unexplained changes exist:

``` text
RELEASE 0.9 CLOSURE UNBLOCK BLOCKED
```

Do not discard them.

------------------------------------------------------------------------

# 7. Reproduce C09-01 Before Fixing It

Capture:

``` text
git config --show-origin --get core.autocrlf
git check-attr -a -- <representative affected files>
```

Inspect:

``` text
.editorconfig
.gitattributes
eng/verify.ps1
```

Run the exact failing verification path:

``` text
dotnet format AIQuantTradingResearch.slnx --verify-no-changes
```

or the exact command invoked by `eng/verify.ps1` if different.

Then run:

``` text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Capture the affected paths and `ENDOFLINE` evidence.

The purpose is to prove the blocker still exists on merged `main` before
applying the correction.

If the blocker no longer reproduces, investigate why. Do not create a
speculative policy change.

------------------------------------------------------------------------

# 8. Analyze Existing Line-Ending Policy

Determine:

``` text
What does .editorconfig require?
What does .gitattributes currently require?
Which file types are affected?
What attributes does Git currently apply to those files?
What line endings are stored in the Git index/blob?
What line endings are materialized in the working tree?
How does core.autocrlf=true interact with current repository attributes?
```

Use appropriate Git inspection commands, for example:

``` text
git check-attr text eol -- <path>
git ls-files --eol <path>
```

Use representative affected C# files and any other file types actually
implicated by verification.

Do not infer a repository-wide pattern from one file without checking
the affected set.

------------------------------------------------------------------------

# 9. Design the Minimum Repository-Owned Correction

Choose the smallest `.gitattributes` correction that:

1.  makes checkout line endings deterministic for the affected tracked
    text files;
2.  agrees with `.editorconfig`;
3.  works even when the developer has `core.autocrlf=true`;
4.  does not alter binary files;
5.  does not unnecessarily normalize unrelated file categories;
6.  supports fresh-clone/fresh-checkout verification.

A likely form may involve explicit `text eol=lf` attributes, but do not
blindly use this example:

``` gitattributes
*.cs text eol=lf
```

Determine the exact required patterns from evidence.

If existing repository policy already covers the affected files
correctly, do not add redundant rules; find the actual root cause.

Do not make `.editorconfig` less strict merely to make verification
pass.

------------------------------------------------------------------------

# 10. Normalization Policy

After updating repository attributes, determine whether affected tracked
files require index/worktree renormalization.

If necessary, use a controlled Git-native normalization operation such
as:

``` text
git add --renormalize <narrow-authorized-paths>
```

Do not blindly run repository-wide renormalization unless evidence
proves repository-wide normalization is necessary and safe.

Before staging normalization, enumerate the exact paths that would
change.

After normalization inspect:

``` text
git diff --numstat
git diff --stat
git diff --name-status
git diff --word-diff=porcelain
```

For files whose only authorized change is line ending normalization,
prove there is no semantic/textual content change.

Any unexpected semantic delta:

``` text
RELEASE 0.9 CLOSURE UNBLOCK BLOCKED
```

------------------------------------------------------------------------

# 11. Governance Artifact Disposition

Ensure these files exist exactly once at:

``` text
docs/roadmap/release-0.9/prompts/release-0.9-closure-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-closure-codex-prompt-chat.md
docs/roadmap/release-0.9/prompts/release-0.9-closure-unblock-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-closure-unblock-codex-prompt-chat.md
```

If closure prompt/chat currently exist at repository root:

``` text
move them to the governed prompt directory
```

Do not leave duplicate root copies.

Preserve their contents except for line-ending normalization required by
the repository policy.

These files are governance/history artifacts, not product
implementation.

------------------------------------------------------------------------

# 12. Working-Tree Validation Before Branching

After the minimum correction is prepared, inspect:

``` text
git status --short
git diff --stat
git diff --name-status
git diff --check
```

Classify every changed/untracked path as:

``` text
LINE-ENDING POLICY
PROVEN LINE-ENDING NORMALIZATION
CLOSURE GOVERNANCE
CLOSURE-UNBLOCK GOVERNANCE
UNEXPECTED
```

Require:

``` text
UNEXPECTED = 0
```

Do not branch/commit until the delta is fully understood.

------------------------------------------------------------------------

# 13. Validate the Corrected Working Tree

Run:

``` text
dotnet restore AIQuantTradingResearch.slnx
dotnet format AIQuantTradingResearch.slnx --verify-no-changes
dotnet build AIQuantTradingResearch.slnx --no-restore
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff --check
```

Expected:

``` text
format verification = PASS
build errors = 0
eng/verify.ps1 = PASS
git diff --check = PASS
```

Expected test baseline:

``` text
Domain.Tests           11/11
Application.Tests      12/12
Infrastructure.Tests    9/9
Architecture.Tests      9/9
Total                  41/41
```

Run Worker:

``` text
dotnet run --project src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --no-build
```

Require:

``` text
Target: SAMPLE-USD
Observation count: 3
Mean price: 110.00
Exit: 0
```

No test or Worker behavior may change as part of this unblock.

------------------------------------------------------------------------

# 14. Fresh-Checkout Reproducibility Proof

Passing verification in the already-modified working tree is
insufficient.

You must prove the repository-owned line-ending policy works from a
clean checkout while the machine's existing Git configuration remains
unchanged.

Do not modify global/system `core.autocrlf`.

Use a safe isolated validation approach that does not destroy the
current working tree, such as:

``` text
a temporary Git worktree
or
a temporary local clone
```

The isolated checkout must include the proposed correction.

Do not depend on unstaged local state that would not exist for another
developer.

A valid approach is to create the correction commit on the dedicated
branch first, then create an isolated worktree/clone from that commit
for reproducibility validation before push/PR.

In the isolated checkout capture:

``` text
git config --show-origin --get core.autocrlf
git status --short
git ls-files --eol <representative affected files>
git check-attr text eol -- <representative affected files>
```

Then run:

``` text
dotnet restore AIQuantTradingResearch.slnx
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
dotnet run --project src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --no-build
git status --short
```

Require:

``` text
core.autocrlf remains unchanged
fresh checkout is clean before validation
eng/verify.ps1 = PASS
all 41 tests pass
Worker canonical acceptance = PASS
checkout remains free of unexpected tracked modifications
```

Remove only the temporary worktree/clone created by this execution after
evidence is captured.

Do not delete user work.

------------------------------------------------------------------------

# 15. Corrective Branch

Use a dedicated forward-only branch.

Preferred:

``` text
release/0.9-closure-unblock
```

Before creating it, check local and remote existence.

If absent:

``` text
git switch -c release/0.9-closure-unblock
```

The branch must originate from current synchronized `main`.

If the branch already exists, inspect it. Never overwrite/reset an
ambiguous branch.

------------------------------------------------------------------------

# 16. Exact Staging

Stage only authorized unblock files.

Expected categories:

``` text
.gitattributes policy correction, if proven necessary
narrow line-ending normalization, if proven necessary
closure prompt/chat governance
closure-unblock prompt/chat governance
```

Inspect:

``` text
git status --short
git diff --cached --stat
git diff --cached --name-status
git diff --cached --check
```

Require:

``` text
all required authorized files staged
no unauthorized file staged
no semantic product/test change
cached diff check = PASS
```

------------------------------------------------------------------------

# 17. Corrective Commit

Create one coherent forward-only commit.

Preferred message:

``` text
fix: unblock Release 0.9 closure verification
```

Do not amend PR #84 history.

Do not rewrite existing Release 0.9 commits.

After commit inspect:

``` text
git status --short
git log -1 --oneline
git show --stat --oneline HEAD
```

------------------------------------------------------------------------

# 18. Post-Commit Validation

Run from the corrective branch:

``` text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff HEAD^ HEAD --check
dotnet run --project src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --no-build
```

Require:

``` text
verification = PASS
41/41 tests = PASS
commit diff check = PASS
Worker = canonical result
working tree = clean
```

Then perform the isolated fresh-checkout proof described above from the
committed corrective branch.

If post-commit or fresh-checkout validation fails:

``` text
do not push
do not silently amend/fix
return RELEASE 0.9 CLOSURE UNBLOCK BLOCKED
```

------------------------------------------------------------------------

# 19. Push

After all local and isolated validation succeeds:

``` text
git push -u origin release/0.9-closure-unblock
```

Never force-push.

Confirm local and remote branch commits match.

------------------------------------------------------------------------

# 20. Pull Request Preflight

Inspect existing PRs:

``` text
gh pr list --state open
gh pr list --state all --head release/0.9-closure-unblock
```

Do not create a duplicate PR.

------------------------------------------------------------------------

# 21. Corrective Pull Request

If no equivalent PR exists, create:

``` text
base: main
head: release/0.9-closure-unblock
title: Release 0.9 — Closure Verification Unblock
```

PR body must clearly state that this is a Release 0.9 closure
correction, not new functionality.

Include evidence equivalent to:

``` text
## Purpose
Resolve the two blockers discovered by the Release 0.9 post-merge closure gate.

## Blockers
- C09-01: fresh-checkout line endings caused canonical format verification failure.
- C09-02: closure governance prompt/chat artifacts were not yet integrated.

## Scope
- Repository-owned line-ending policy correction.
- Only proven-required line-ending normalization.
- Release 0.9 closure and unblock governance artifacts.
- No product behavior, architecture, package, project, or test-semantic changes.

## Validation
- dotnet format --verify-no-changes: PASS
- eng/verify.ps1: PASS
- Domain.Tests: 11/11
- Application.Tests: 12/12
- Infrastructure.Tests: 9/9
- Architecture.Tests: 9/9
- Total: 41/41
- Worker: SAMPLE-USD / 3 / mean 110.00 / exit 0
- git diff --check: PASS
- isolated fresh checkout with existing core.autocrlf configuration: PASS
```

Use actual observed evidence.

------------------------------------------------------------------------

# 22. PR Inspection

After creating/reusing the PR capture:

``` text
PR number
URL
title
base
head
state
mergeability
review state
checks/status
```

Do not claim hosted checks passed unless observed.

Pending checks/reviews are reported accurately.

------------------------------------------------------------------------

# 23. Mandatory Human Merge Gate

STOP BEFORE MERGE.

Do not:

``` text
gh pr merge
git merge
enable auto-merge
```

Even if the corrective PR is clean and mergeable.

The user must explicitly merge/authorize merge.

After the human merge, the existing authoritative:

``` text
docs/roadmap/release-0.9/prompts/release-0.9-closure-codex-prompt.md
```

must be rerun.

Do not replace or weaken that closure gate.

------------------------------------------------------------------------

# 24. GitHub Governance Protection

Do not mutate:

``` text
Milestone #40
Issues #69–#82
labels
project state
tags
GitHub Releases
Release 1.0 planning
```

They were already reconciled by the closure report.

This unblock concerns C09-01 and C09-02 only.

------------------------------------------------------------------------

# 25. Stop Conditions

Return:

``` text
RELEASE 0.9 CLOSURE UNBLOCK BLOCKED
```

for:

``` text
unexpected repository delta
root cause cannot be reproduced
existing .gitattributes already proves another cause
correction requires weakening verification
semantic product/test change appears
broad unexplained normalization occurs
fresh-checkout proof fails
eng/verify.ps1 fails
any test fails
Worker acceptance changes
branch collision is ambiguous
push fails
PR conflict is unsafe
authority conflict exists
```

Do not expand scope to solve a blocker.

------------------------------------------------------------------------

# 26. Decision Model

Finish with exactly one:

``` text
RELEASE 0.9 CLOSURE UNBLOCK READY FOR MERGE AUTHORIZATION
RELEASE 0.9 CLOSURE UNBLOCK READY WITH ACTIONS
RELEASE 0.9 CLOSURE UNBLOCK BLOCKED
```

Use `READY FOR MERGE AUTHORIZATION` when:

``` text
C09-01 reproduced and root cause proven
minimum repository-owned correction implemented
C09-02 governance artifacts integrated
no unexpected delta exists
format verification passes
eng/verify.ps1 passes
41/41 tests pass
Worker canonical behavior passes
fresh-checkout reproducibility passes without changing global/system Git configuration
corrective commit created
branch pushed
corrective PR created/reused and inspected
no prohibited GitHub mutation occurred
PR not merged
```

Use `READY WITH ACTIONS` only for non-blocking external PR actions such
as pending review/checks.

------------------------------------------------------------------------

# 27. Required Execution Report

Return:

``` text
Release 0.9 Closure Unblock Execution Report
```

with:

## 1. Executive Summary

``` text
C09-01:
C09-02:
Correction:
Fresh-checkout proof:
PR:
Final decision:
```

## 2. Authorities Reviewed

List exact authorities.

## 3. Initial Repository State

``` text
branch:
HEAD:
origin/main:
ahead/behind:
working tree:
authorized untracked governance:
unexpected:
```

## 4. C09-01 Reproduction

``` text
core.autocrlf:
.editorconfig:
.gitattributes:
git attributes on affected files:
working-tree line endings:
format result:
eng/verify result:
assessment:
```

## 5. Root-Cause Analysis

Explain the proven interaction between Git checkout policy and
repository formatting policy.

## 6. Correction Design

``` text
files changed:
attribute rules added/changed:
why each rule is required:
why .editorconfig was not weakened:
why workstation Git configuration was not changed:
```

## 7. Normalization Review

  File/Pattern   Reason   Semantic Change   Result
  -------------- -------- ----------------- --------

Explicitly prove any normalization-only delta has no semantic change.

## 8. C09-02 Governance Integration

``` text
closure prompt:
closure prompt-chat:
unblock prompt:
unblock prompt-chat:
duplicates:
final governed location:
assessment:
```

## 9. Corrected Working-Tree Validation

  Validation             Result   Evidence
  ---------------------- -------- ----------
  dotnet format verify
  Build
  Domain.Tests
  Application.Tests
  Infrastructure.Tests
  Architecture.Tests
  Total tests
  eng/verify.ps1
  Worker
  git diff --check

## 10. Branch and Commit

``` text
branch:
base:
commit:
message:
working tree after commit:
```

## 11. Fresh-Checkout Reproducibility

``` text
method:
source commit:
core.autocrlf unchanged:
checkout initial status:
representative git ls-files --eol:
representative git check-attr:
eng/verify.ps1:
tests:
Worker:
final checkout status:
assessment:
```

## 12. Push

``` text
remote branch:
local commit:
remote commit:
force push:
assessment:
```

## 13. Pull Request

``` text
number:
URL:
title:
base:
head:
state:
mergeability:
reviews:
checks:
```

## 14. Scope Integrity

Explicitly answer:

``` text
Production behavior changed?
Test semantics changed?
Architecture changed?
Packages/projects changed?
Verification weakened?
.editorconfig weakened?
Global/system Git configuration changed?
Broad unrelated normalization performed?
Release 1.0 started?
```

Expected all `NO`.

## 15. GitHub Governance Protection

``` text
milestone #40 mutated:
issues #69–#82 mutated:
labels/project mutated:
tag created:
GitHub Release created:
```

Expected all `NO`.

## 16. Findings

  ID   Classification   Finding   Evidence   Required Action
  ---- ---------------- --------- ---------- -----------------

## 17. Merge Authorization Gate

Explicitly state:

``` text
PR merge performed: NO
Auto-merge enabled: NO
Human merge authorization required: YES
```

## 18. Final Decision

Exactly one:

``` text
RELEASE 0.9 CLOSURE UNBLOCK READY FOR MERGE AUTHORIZATION
RELEASE 0.9 CLOSURE UNBLOCK READY WITH ACTIONS
RELEASE 0.9 CLOSURE UNBLOCK BLOCKED
```

## 19. Next Authorized Action

If ready:

``` text
Human review and merge of the corrective PR.

After merge, rerun:
docs/roadmap/release-0.9/prompts/release-0.9-closure-codex-prompt.md

Do not start Release 1.0 until that closure gate returns RELEASE 0.9 CLOSED.
```

------------------------------------------------------------------------

# 28. Acceptance Checklist

Before a ready decision confirm:

-   [ ] C09-01 reproduced.
-   [ ] Existing `.editorconfig` inspected.
-   [ ] Existing `.gitattributes` inspected.
-   [ ] `eng/verify.ps1` inspected.
-   [ ] Affected file attributes/line endings inspected.
-   [ ] Root cause proven.
-   [ ] Repository-owned correction is minimal.
-   [ ] `.editorconfig` not weakened.
-   [ ] Global/system Git configuration unchanged.
-   [ ] Verification not weakened.
-   [ ] Binary handling not damaged.
-   [ ] Any normalization is proven necessary.
-   [ ] No semantic product/test delta introduced.
-   [ ] Closure prompt/chat integrated.
-   [ ] Unblock prompt/chat integrated.
-   [ ] No duplicate governance copies remain.
-   [ ] `dotnet format --verify-no-changes` passes.
-   [ ] Build succeeds with zero errors.
-   [ ] Domain.Tests 11/11.
-   [ ] Application.Tests 12/12.
-   [ ] Infrastructure.Tests 9/9.
-   [ ] Architecture.Tests 9/9.
-   [ ] Total 41/41.
-   [ ] `eng/verify.ps1` passes.
-   [ ] Worker canonical acceptance passes.
-   [ ] `git diff --check` passes.
-   [ ] Corrective branch created safely.
-   [ ] Exact authorized delta staged.
-   [ ] Corrective commit created.
-   [ ] Post-commit validation passes.
-   [ ] Isolated fresh-checkout validation passes.
-   [ ] Existing `core.autocrlf` configuration remained unchanged.
-   [ ] Corrective branch pushed without force.
-   [ ] Duplicate PR check performed.
-   [ ] Corrective PR created/reused.
-   [ ] PR state inspected.
-   [ ] No Release 0.9 planning governance mutated.
-   [ ] No tag/GitHub Release created.
-   [ ] Release 1.0 not started.
-   [ ] PR not merged.
-   [ ] Auto-merge not enabled.

# Final Instruction

Resolve only:

``` text
C09-01 — repository checkout/format line-ending conflict
C09-02 — untracked Release 0.9 closure governance artifacts
```

Make the repository---not the developer workstation---the authority for
reproducible line endings.

Prove the correction from an isolated clean checkout while leaving the
existing global/system Git configuration unchanged.

Preserve all accepted Release 0.9 product behavior, tests, architecture,
and governance.

Create a forward-only corrective PR and stop before merge.

After human merge, the existing Release 0.9 post-merge closure gate must
be rerun unchanged.

Finish with exactly one:

``` text
RELEASE 0.9 CLOSURE UNBLOCK READY FOR MERGE AUTHORIZATION
RELEASE 0.9 CLOSURE UNBLOCK READY WITH ACTIONS
RELEASE 0.9 CLOSURE UNBLOCK BLOCKED
```

> The unblock is complete only when a fresh checkout can reproduce the
> repository's canonical verification without relying on
> workstation-specific Git configuration.
