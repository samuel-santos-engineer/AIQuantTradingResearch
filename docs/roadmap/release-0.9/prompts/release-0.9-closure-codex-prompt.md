# Codex Execution Prompt --- Release 0.9 Post-Merge Closure

## Purpose

Execute the final post-merge closure gate for **Release 0.9 --- Research
Platform**.

Prerequisites:

``` text
WP14 = RELEASE 0.9 ACCEPTED
Git/GitHub integration = READY FOR MERGE AUTHORIZATION
PR #84 — Release 0.9 — Research Platform = merged by the human operator
```

This is not another implementation work package. Prove that the accepted
candidate landed correctly on `main`, remains technically healthy after
merge, and has a coherent final GitHub governance state.

Do not add features, fix product behavior, redesign Release 0.9, delete
branches, create tags/releases, or begin Release 1.0.

## Authorities

Read completely:

``` text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/release-0.9-github-integration-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-closure-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Review the complete Release 0.9 prompt/governance set under:

``` text
docs/roadmap/release-0.9/prompts/
```

Inspect current GitHub truth for:

``` text
PR #84
Milestone #40 — Phase 2 - Release 0.9: Research Platform
Issues #69–#82
main
release/0.9-research-platform
```

Observe current state independently. Do not assume it from earlier
reports.

## Established Pre-Merge Evidence

Immediately before the human merge:

``` text
Integration branch:
  release/0.9-research-platform

Accepted integration commit:
  6e09759a75da6e690272dc1746c36ee10a2140fc

Commit:
  feat: implement Release 0.9 research platform

PR:
  #84 — Release 0.9 — Research Platform

Candidate:
  64 reconciled files
  62 WP14-accepted files
  2 integration governance files
  0 unexpected files

Domain.Tests           11/11
Application.Tests      12/12
Infrastructure.Tests    9/9
Architecture.Tests      9/9
Total                  41/41

eng/verify.ps1         PASS
Worker                 SAMPLE-USD / 3 / mean 110.00 / exit 0
git diff --check       PASS
```

Previously observed GitHub planning:

``` text
Milestone #40 = CLOSED
Issues #69–#82 = CLOSED
```

These are observations to verify, not states to recreate blindly.

## Closure Safety Rule

Closure must prove what happened; it must not manufacture a clean
result.

If merged state differs from accepted state:

1.  investigate;
2.  distinguish merge metadata/strategy from technical drift;
3.  do not silently modify production, tests, architecture, or
    documentation;
4.  block closure if a mandatory Release 0.9 invariant is violated.

Never use:

``` text
git reset --hard
git clean
force push
```

## Authentication Preflight

Run:

``` text
gh auth status
```

Confirm authenticated repository/PR/issue/milestone access.

Never print tokens.

If authentication prevents authoritative closure inspection:

``` text
RELEASE 0.9 CLOSURE BLOCKED
```

## Initial Local State

Before switching branches capture:

``` text
git rev-parse --show-toplevel
git remote -v
git branch --show-current
git rev-parse HEAD
git status --short
git diff --cached -- .
```

Expected starting branch may still be:

``` text
release/0.9-research-platform
```

Expected working tree:

``` text
clean
```

If not clean, classify every change. Do not discard anything.

## Fetch Remote Truth

Run:

``` text
git fetch origin --prune
git branch -vv
git rev-parse origin/main
git log --oneline --decorate -n 10 origin/main
```

`--prune` may update remote-tracking references for branches GitHub
already deleted. That is acceptable synchronization behavior.

Do not delete local branches.

## Verify PR #84

Inspect PR #84 with GitHub CLI.

Capture:

``` text
number
title
state
mergedAt
merge commit
base
head
head commit
author
URL
```

Require:

``` text
state = MERGED
base = main
head = release/0.9-research-platform
```

If PR #84 is not actually merged:

``` text
RELEASE 0.9 CLOSURE BLOCKED
```

Do not merge it under this prompt.

## Verify Accepted Candidate Landed

Verify the accepted integration commit exists:

``` text
git cat-file -e 6e09759a75da6e690272dc1746c36ee10a2140fc^{commit}
```

Test ancestry:

``` text
git merge-base --is-ancestor 6e09759a75da6e690272dc1746c36ee10a2140fc origin/main
```

If exit `0`, record direct reachability.

If GitHub used squash/rebase or another strategy and the original
integration commit is not an ancestor, do not automatically fail.
Instead:

1.  inspect the PR merge strategy and merge commit;
2.  compare merged tree/content with the accepted integration commit;
3.  prove whether the accepted candidate content landed without
    technical drift;
4.  report the strategy.

The requirement is **accepted candidate content landed**, not a specific
merge strategy.

## Synchronize Local Main

Only with a safe working tree and verified merged PR:

``` text
git switch main
git pull --ff-only origin main
```

Then capture:

``` text
git branch --show-current
git rev-parse HEAD
git rev-parse origin/main
git rev-list --left-right --count origin/main...HEAD
git status --short
```

Require:

``` text
branch = main
HEAD = origin/main
ahead/behind = 0/0
working tree = clean
```

If `--ff-only` cannot synchronize safely, block closure. Do not reset
main.

## Artifact Reconciliation

Reconcile merged `main` against:

``` text
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
```

Confirm the accepted Release 0.9 areas are present:

``` text
research Domain model/implementation
Application contracts/use case
Infrastructure deterministic adapter
dependency registration
Worker execution
Domain tests
Application tests
Infrastructure tests
Architecture evolution
WP13 documentation alignment
WP10/WP11 friend-assembly boundaries
Release 0.9 prompt/chat governance
integration prompt/chat governance
```

Account for explicitly authorized prompt-chat and unblock artifacts.

No unexplained missing accepted artifact is allowed.

## Production Architecture

Reconfirm from actual project files:

``` text
Domain          -> none
Application     -> Domain
Infrastructure  -> Application
Worker          -> Application + Infrastructure
Cycles          -> 0
```

Worker must have no direct Domain project reference.

Confirm no unexpected project/package/solution/build/workflow change
entered through merge.

## Testability Boundaries

Confirm exactly:

``` text
Application
  InternalsVisibleTo("AIQuantTradingResearch.Application.Tests")

Infrastructure
  InternalsVisibleTo("AIQuantTradingResearch.Infrastructure.Tests")
```

Confirm:

``` text
no unauthorized friend assembly
ResearchUseCase remains non-public
DeterministicObservationSource remains non-public
```

## Post-Merge Validation

On synchronized `main` run:

``` text
dotnet restore AIQuantTradingResearch.slnx
dotnet build AIQuantTradingResearch.slnx --no-restore
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Require:

``` text
restore succeeds
build errors = 0
verification exit = 0
all discovered tests pass
```

Expected evidence:

``` text
Domain.Tests           11/11
Application.Tests      12/12
Infrastructure.Tests    9/9
Architecture.Tests      9/9
Total                  41/41
```

Investigate a legitimate count difference rather than treating test
count as permanent architecture.

Known `NU1900` vulnerability-feed connectivity warnings remain
non-blocking when validation succeeds.

## Worker Acceptance

Run:

``` text
dotnet run --project src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --no-build
```

Require behavior consistent with:

``` text
Target: SAMPLE-USD
Observation count: 3
Mean price: 110.00
Exit: 0
```

If canonical behavior fails, block closure. Do not fix it here.

## Repository Hygiene

Run:

``` text
git status --short
git diff --check
```

Require:

``` text
working tree = clean
staged = none
unexpected persistent artifacts = none
```

Ignored build outputs are acceptable.

## GitHub Governance Reconciliation

Inspect current state of:

``` text
PR #84
Milestone #40
Issues #69–#82
```

For Milestone #40 capture:

``` text
number
title
state
open issue count
closed issue count
```

For each issue #69--#82 capture:

``` text
number
title
state
milestone
```

For PR #84 capture:

``` text
MERGED
mergedAt
merge commit
base
head
```

Expected from previous observation:

``` text
PR #84 = MERGED
Milestone #40 = CLOSED
Issues #69–#82 = CLOSED
```

Current observed state is authoritative.

## Narrow Governance Mutation Policy

Do not blindly mutate already-correct governance.

A governance correction is authorized only when all are true:

1.  final state is unambiguous from Release 0.9 authority;
2.  implementation is merged and post-merge validation is green;
3.  mutation is limited to Release 0.9 issue/milestone closure state;
4.  no new planning semantics must be invented;
5.  product code and Git history are unaffected.

Potentially authorized minimal corrections:

``` text
close a proven-complete Release 0.9 issue unexpectedly still OPEN
close Milestone #40 after all authoritative Release 0.9 issues are closed
```

Before mutation verify the target belongs to Release 0.9 and no
authoritative work remains open. After mutation re-read the state.

Do not reopen already closed objects merely to recreate workflow
history.

If intent is ambiguous, do not mutate.

## Integration Branch Assessment

Inspect:

``` text
release/0.9-research-platform
```

Determine:

``` text
local exists?
remote exists?
merged/equivalent to main?
safe to delete?
```

Report exactly one cleanup assessment:

``` text
SAFE TO DELETE
KEEP — REASON
ALREADY DELETED REMOTELY
NOT SAFE TO DELETE
```

Do **not** delete local or remote branches under this prompt.

## Tag / GitHub Release Policy

Do not create:

``` text
Git tag
GitHub Release
version bump
release notes artifact
```

unless an existing Release 0.9 authority explicitly requires it for
closure.

If none does, report:

``` text
No tag/GitHub Release required by current Release 0.9 closure authority.
```

Do not invent a release-management policy.

## Release 1.0 Protection

Do not:

``` text
create Release 1.0 milestone/issues
modify Release 1.0 roadmap
create Release 1.0 prompts/branch
implement Release 1.0 code
```

Release 1.0 starts only after Release 0.9 closure is accepted under
separate authority.

## Prohibited Operations

Do not:

-   modify production code;
-   modify tests;
-   modify WP13 documentation;
-   add packages/projects;
-   modify solution/build/engineering/workflow assets;
-   amend/rewrite merged history;
-   force-push;
-   reset main;
-   use `git reset --hard`;
-   use `git clean`;
-   revert the merge;
-   reopen closed governance without explicit authority;
-   delete branches;
-   create tags/GitHub Releases;
-   start Release 1.0.

## Findings Model

Use:

``` text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

Examples:

``` text
BLOCKER:
  PR #84 not actually merged
  merged main fails verification
  accepted candidate content missing
  unexplained technical drift

REQUIRED ACTION:
  authoritative Release 0.9 issue remains open despite proven completion
  milestone state contradicts completed authoritative work

OBSERVATION:
  NU1900 warning
  remote integration branch already deleted
  no hosted CI configured
```

## Decision Model

Finish with exactly one:

``` text
RELEASE 0.9 CLOSED
RELEASE 0.9 CLOSED WITH ACTIONS
RELEASE 0.9 CLOSURE BLOCKED
```

Use `RELEASE 0.9 CLOSED` only when:

``` text
PR #84 verified MERGED
accepted Release 0.9 content proven on main
local main == origin/main
ahead/behind = 0/0
working tree clean
architecture intact
testability boundaries intact
restore/build/verify green
all tests pass
Worker canonical acceptance passes
manifest/artifacts reconciled
issues #69–#82 reconciled
milestone #40 reconciled
no mandatory governance inconsistency remains
no unauthorized mutation occurred
Release 1.0 not started
```

Use `RELEASE 0.9 CLOSED WITH ACTIONS` only for genuinely non-blocking
cleanup/follow-up such as optional branch deletion.

Use `RELEASE 0.9 CLOSURE BLOCKED` for a mandatory technical/governance
defect.

## Required Report

Return:

``` text
Release 0.9 Post-Merge Closure Report
```

with:

### 1. Executive Summary

``` text
Release:
Merged PR:
Post-merge main:
Technical validation:
Governance:
Branch cleanup assessment:
Final decision:
```

### 2. Authorities Reviewed

List exact authorities.

### 3. Authentication Preflight

``` text
authenticated:
active account:
required access:
assessment:
```

Never print secrets.

### 4. Initial Local Repository State

``` text
repository:
starting branch:
HEAD:
working tree:
staged:
```

### 5. Remote Synchronization

``` text
fetch:
origin/main:
remote integration branch:
assessment:
```

### 6. PR #84 Merge Verification

``` text
number:
title:
state:
mergedAt:
merge commit:
base:
head:
head commit:
URL:
assessment:
```

### 7. Integration Commit / Content Reachability

``` text
integration commit:
commit exists:
ancestor of main:
merge strategy:
tree/content equivalence if required:
assessment:
```

### 8. Main Synchronization

``` text
branch:
local HEAD:
origin/main:
ahead/behind:
working tree:
assessment:
```

### 9. Release 0.9 Artifact Reconciliation

  Artifact / Area   Expected   Observed   Result
  ----------------- ---------- ---------- --------

### 10. Production Architecture

``` text
Domain:
Application:
Infrastructure:
Worker:
cycles:
unexpected references:
assessment:
```

### 11. Testability Boundaries

``` text
Application friend assembly:
Infrastructure friend assembly:
unexpected friend assemblies:
implementation visibility:
assessment:
```

### 12. Post-Merge Validation

  Validation             Result   Evidence
  ---------------------- -------- ----------
  Restore
  Build
  Domain.Tests
  Application.Tests
  Infrastructure.Tests
  Architecture.Tests
  Total tests
  eng/verify.ps1
  git diff --check

### 13. Worker Acceptance

``` text
command:
exit:
target:
observation count:
mean:
assessment:
```

### 14. Repository Hygiene

``` text
working tree:
staged:
unexpected artifacts:
assessment:
```

### 15. GitHub Governance Matrix

  --------------------------------------------------------------------------
  Object         Expected Final Observed State Mutation       Result
                 State                         Performed
  -------------- -------------- -------------- -------------- --------------
  PR #84         MERGED

  Milestone #40  CLOSED

  Issues         CLOSED
  #69--#82
  --------------------------------------------------------------------------

Also enumerate individual issue states #69 through #82.

### 16. Governance Mutations

``` text
issues changed:
milestone changed:
labels/project changed:
reason:
assessment:
```

If none:

``` text
No GitHub governance mutation was necessary.
```

### 17. Integration Branch Assessment

``` text
local branch:
remote branch:
contained/equivalent in main:
deletion safety:
branch deleted: NO
```

### 18. Tag / GitHub Release Assessment

``` text
tag created:
GitHub Release created:
required by authority:
assessment:
```

### 19. Release 1.0 Protection

``` text
Release 1.0 milestone created:
Release 1.0 issues created:
Release 1.0 branch created:
Release 1.0 implementation started:
assessment:
```

Expected all `NO`.

### 20. Findings

  ID   Classification   Finding   Evidence   Required Action
  ---- ---------------- --------- ---------- -----------------

### 21. Closure Acceptance Matrix

Evaluate mandatory criteria as `PASS`, `FAIL`, or `N/A`.

### 22. Final Repository / GitHub State

``` text
local branch:
local HEAD:
origin/main:
ahead/behind:
working tree:
PR #84:
milestone #40:
issues #69–#82:
integration branch:
```

### 23. Final Decision

State exactly one:

``` text
RELEASE 0.9 CLOSED
RELEASE 0.9 CLOSED WITH ACTIONS
RELEASE 0.9 CLOSURE BLOCKED
```

Explain the evidence.

### 24. Next Authorized Action

If progression is permitted:

``` text
Release 0.9 is formally closed.

The next lifecycle activity may be a separately authorized Release 1.0 scope and governance design exercise.
```

Do not start Release 1.0.

## Closure Checklist

Before returning `RELEASE 0.9 CLOSED`, confirm:

-   [ ] Authorities read.
-   [ ] Authentication verified safely.
-   [ ] Initial repository state captured.
-   [ ] Remote state fetched.
-   [ ] PR #84 independently verified MERGED.
-   [ ] Merge commit captured.
-   [ ] Accepted integration commit exists.
-   [ ] Reachability or equivalent merged content proven.
-   [ ] Main switched safely.
-   [ ] `git pull --ff-only origin main` succeeds.
-   [ ] Local main equals origin/main.
-   [ ] Ahead/behind = 0/0.
-   [ ] Working tree clean.
-   [ ] Release 0.9 artifacts reconciled.
-   [ ] Production graph correct.
-   [ ] Cycles = 0.
-   [ ] Worker has no direct Domain project reference.
-   [ ] Exactly two authorized friend-assembly relationships remain.
-   [ ] Concrete implementations remain non-public.
-   [ ] Restore succeeds.
-   [ ] Build succeeds with zero errors.
-   [ ] Domain.Tests pass.
-   [ ] Application.Tests pass.
-   [ ] Infrastructure.Tests pass.
-   [ ] Architecture.Tests pass.
-   [ ] `eng/verify.ps1` passes.
-   [ ] Worker canonical acceptance passes.
-   [ ] `git diff --check` passes.
-   [ ] Post-validation tree clean.
-   [ ] Milestone #40 inspected.
-   [ ] Issues #69--#82 inspected.
-   [ ] Any governance correction minimal and authorized.
-   [ ] Integration branch deletion safety assessed.
-   [ ] Integration branch not deleted.
-   [ ] No tag/GitHub Release created.
-   [ ] Release 1.0 not started.
-   [ ] No product/test/documentation correction made.
-   [ ] Final decision evidence-based.

# Final Instruction

Execute the Release 0.9 post-merge closure gate.

Verify the merged PR and accepted candidate, synchronize `main` safely,
re-run full technical acceptance, reconcile Release 0.9 GitHub
governance, assess but do not delete the integration branch, and return
the complete closure report.

Do not manufacture closure through product changes or destructive Git
operations.

Finish with exactly one:

``` text
RELEASE 0.9 CLOSED
RELEASE 0.9 CLOSED WITH ACTIONS
RELEASE 0.9 CLOSURE BLOCKED
```

If closed, state that the next lifecycle activity may be a **separately
authorized Release 1.0 scope and governance design exercise**.

> Release closure proves that the accepted candidate landed, remains
> healthy on `main`, and has a coherent final governance state.
