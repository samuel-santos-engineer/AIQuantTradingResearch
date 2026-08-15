# Release 1.0 WP01 --- Release & Repository Preflight --- Codex Prompt

## Role

Act as the **WP01 Release & Repository Preflight Executor** for Release
1.0 of `AIQuantTradingResearch`.

This is a bounded verification and readiness work package. Its purpose
is to establish a trustworthy execution baseline before any Release 1.0
Market Data implementation or provider discovery begins.

Do not start WP02.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before acting:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md
docs/roadmap/release-1.0/prompts/release-1.0-github-planning-codex-prompt.md
docs/roadmap/release-1.0/prompts/release-1.0-github-planning-reconciliation-codex-prompt.md
```

Read GitHub issue:

``` text
#86 — Release 1.0 WP01 — Release & Repository Preflight
```

Inspect the Release 0.9 closure/governance evidence required to prove
the predecessor release is closed and the repository baseline is valid.

Inspect existing repository engineering authorities and scripts only as
needed to execute WP01, including the canonical build/test/format/verify
path.

Authority precedence:

``` text
1. RELEASE_1.0_EXECUTION_PLAN.md
2. RELEASE_1.0_FILE_MANIFEST.md
3. GitHub issue #86 as the GitHub projection of those authorities
4. Existing repository engineering/governance conventions
5. This execution prompt
```

If a lower authority materially conflicts with a higher one, stop and
report the conflict rather than guessing.

------------------------------------------------------------------------

## 2. Objective

Prove that the repository and governance state are ready for Release 1.0
execution.

WP01 must establish, with evidence:

``` text
Release 0.9 predecessor closure is satisfied
Release 1.0 authorities are present and internally usable
Release 1.0 GitHub planning is established
milestone #41 is the authoritative open Release 1.0 milestone
issues #86–#101 represent exactly WP01–WP16
repository branch/baseline is understood
local and remote main are synchronized unless authority explicitly permits otherwise
working-tree state is classified
toolchain/SDK state is known
restore/build/format/test/verify baseline is healthy
current architecture/test baseline is known
file-manifest assumptions are checked against repository reality
no Release 1.0 implementation has begun
no WP02 provider discovery has begun
```

WP01 is a **preflight and evidence package**, not a feature
implementation package.

------------------------------------------------------------------------

## 3. Authorized Scope

You are authorized to:

1.  Read repository files and GitHub planning state.
2.  Inspect Git status, branches, remotes, HEAD, `origin/main`, and
    ahead/behind state.
3.  Inspect the Release 0.9 closure state and relevant merged history.
4.  Inspect Release 1.0 authorities and governance artifacts.
5.  Inspect milestone #41 and issues #86--#101 read-only.
6.  Inspect the existing GitHub Project read-only where needed to
    confirm planning representation.
7.  Inspect SDK/tool versions and repository configuration.
8.  Run repository-safe diagnostic and validation commands.
9.  Run restore, formatting verification, build, tests, canonical
    verification, and other existing non-mutating verification paths
    authorized by repository engineering conventions.
10. Inspect project references, solution membership, current test
    inventory, and current architecture baseline where needed to
    establish evidence.
11. Classify existing untracked/modified files as expected governance
    state or unexpected state.
12. Report blockers, observations, baseline counts, and readiness
    evidence.

If a validation command produces ordinary generated build artifacts
under ignored paths, that is acceptable. Do not treat normal ignored
build output as an implementation mutation.

------------------------------------------------------------------------

## 4. Prohibited Scope

Do not:

``` text
perform provider research
select a market-data provider
contact external provider APIs for WP02 research
create MARKET_DATA_PROVIDER_DECISION.md
start WP02
modify Domain code
modify Application code
modify Infrastructure code
modify Worker code
modify tests
modify architecture documentation
modify engineering scripts
modify project files
modify package references
modify solution membership
modify build policy
modify .editorconfig
modify .gitattributes
modify Release 1.0 authorities
modify Release 1.0 prompts
create new implementation artifacts
create branches
stage files
commit
push
create or merge PRs
create tags
create GitHub Releases
close issue #86
close milestone #41
edit issues #86–#101
change labels
change GitHub Project fields/items
create Release 1.1 planning
```

Do not "fix" a preflight defect inside WP01 unless the Release 1.0
execution plan explicitly authorizes that exact WP01 mutation.

If correction is required but not authorized, classify it as a blocker
and stop.

------------------------------------------------------------------------

## 5. Initial Repository Preflight

Capture before running substantive validation:

``` text
repository identity
current branch
HEAD
origin/main
ahead/behind
upstream
working-tree status
staged changes
tracked modifications
untracked files/directories
ignored build artifacts if relevant
recent merge/history evidence for Release 0.9
```

The expected starting branch is `main`.

If not on `main`, do not switch automatically unless the governing WP01
authority explicitly instructs you to do so. Report the actual state and
determine whether execution can safely continue.

Do not discard, stash, stage, commit, or rewrite existing local work.

------------------------------------------------------------------------

## 6. Release 0.9 Predecessor Gate

Prove the predecessor gate required by WP01.

At minimum inspect evidence that:

``` text
Release 0.9 implementation was merged
Release 0.9 closure unblock was merged
Release 0.9 final closure gate completed
Release 0.9 milestone #40 is CLOSED
Release 0.9 issues #69–#82 are CLOSED
Release 1.0 governance design was authorized by the Release 0.9 closure outcome
```

Use repository/GitHub evidence actually available.

Do not manufacture a closure conclusion from assumptions.

If the authoritative Release 0.9 closure state cannot be proven, WP01 is
blocked.

------------------------------------------------------------------------

## 7. Release 1.0 Governance Gate

Verify the authoritative Release 1.0 governance set.

At minimum:

``` text
RELEASE_1.0_EXECUTION_PLAN.md exists
RELEASE_1.0_FILE_MANIFEST.md exists
canonical prompts/ directory exists
planning prompt exists
planning prompt-chat exists
planning reconciliation prompt exists
planning reconciliation prompt-chat exists
WP01 prompt exists
WP01 prompt-chat exists
```

Read and reconcile the execution plan and manifest sufficiently to
validate WP01's scope, dependencies, artifact expectations, protected
paths, and downstream boundary.

Do not create missing governance files in this execution. Missing
required authority is a blocker unless explicitly classified otherwise
by the governing files.

------------------------------------------------------------------------

## 8. GitHub Planning Gate

Inspect GitHub read-only and prove:

``` text
milestone #41 title:
  Phase 3 - Release 1.0: Market Data Foundation

milestone #41 state:
  OPEN

authoritative WP issues:
  exactly #86–#101 / WP01–WP16

issue states:
  OPEN

milestone assignment:
  16/16 assigned to #41

WP01:
  #86

WP02:
  #87

WP17+:
  absent

lifecycle-gate issues:
  absent

active replacement Release 1.1 planning:
  absent
```

Validate the issue dependency projection at least enough to establish
that WP01 has no Release 1.0 WP predecessor and WP02 depends on WP01.

The existing Project automation observation that items may appear as
`In Review` is non-blocking unless current state shows a materially
different planning defect.

Do not mutate Project status during WP01.

------------------------------------------------------------------------

## 9. Toolchain Baseline

Capture the actual local toolchain used by the repository.

At minimum inspect as applicable:

``` text
dotnet --info
dotnet --version
global.json
target framework(s)
solution file
PowerShell version used for repository scripts
Git version
GitHub CLI version/authentication if GitHub inspection uses gh
```

Verify the installed SDK satisfies repository pinning/roll-forward
policy.

Do not install or upgrade tools in WP01.

A missing required tool is a blocker unless the repository has an
already-authorized alternate validation path.

------------------------------------------------------------------------

## 10. Solution and Dependency Baseline

Inspect without modification:

``` text
solution membership
production projects
test projects
project references
package/project manifest state
```

Confirm the current accepted production dependency graph remains
consistent with the post-Release-0.9 baseline unless the Release 1.0
authority explicitly says otherwise.

Record the actual graph and cycles.

Do not introduce Release 1.0 dependencies during WP01.

------------------------------------------------------------------------

## 11. Canonical Validation Baseline

Use the repository's existing canonical commands and scripts.

Run the WP01-required validation from the authorities. Where consistent
with repository conventions, collect evidence for:

``` text
restore
format verification
build
Domain.Tests
Application.Tests
Infrastructure.Tests
Architecture.Tests
eng/verify.ps1
git diff --check
```

If the accepted baseline includes a canonical Worker smoke execution,
run it exactly as the repository authority defines and capture its
output/exit code.

Do not weaken verification to make it pass.

Do not suppress meaningful failures.

Known external connectivity warnings such as NuGet vulnerability-feed
`NU1900` may be classified as non-blocking only when the underlying
restore/build/verification succeeds and the condition matches
established repository evidence.

Record exact passed/failed/skipped test counts from this run rather than
assuming previous counts.

------------------------------------------------------------------------

## 12. Manifest and Scope Readiness

Reconcile the Release 1.0 file manifest against the current repository.

Establish:

``` text
authorized Release 1.0 mutation areas are identifiable
protected areas are identifiable
mandatory governance paths exist
conditional artifact rules are understood
unexpected-file rule is understood
no implementation artifact required by WP02+ has already been introduced accidentally
```

Do not create downstream artifacts to make the manifest "complete." The
manifest describes authorized release evolution; WP01 must distinguish
future expected files from files that must already exist.

Report any premature downstream implementation artifact as a finding and
classify according to the authorities.

------------------------------------------------------------------------

## 13. Working-Tree Classification

Classify every non-clean item visible at the end of WP01 as one of:

``` text
EXPECTED GOVERNANCE
EXPECTED GENERATED/IGNORED
PRE-EXISTING AUTHORIZED
UNEXPECTED
```

The Release 1.0 governance directory may be intentionally untracked at
this stage if that is the inherited state. Do not stage or commit it.

For every `UNEXPECTED` item, identify:

``` text
path
status
why unexpected
whether it predates WP01
impact on readiness
minimum corrective authority required
```

Do not delete unexpected files.

------------------------------------------------------------------------

## 14. WP02 Boundary

WP01 must end before provider discovery.

Specifically, do not:

``` text
compare providers
search provider pricing
evaluate API schemas
evaluate rate limits
choose symbols/endpoints
obtain API keys
design provider transport DTOs
design HTTP clients
design normalization
write provider decision records
```

Those belong to WP02 or later work packages.

WP01 may only verify that the repository is ready for those activities.

------------------------------------------------------------------------

## 15. Acceptance / Exit Criteria

WP01 may be declared complete only if the execution-plan exit criteria
are satisfied and evidence proves, at minimum:

``` text
Release 0.9 predecessor gate = PASS
Release 1.0 authorities = PRESENT / CONSISTENT
Release 1.0 GitHub planning = ESTABLISHED
repository baseline = KNOWN
main/origin relationship = ACCEPTABLE
working-tree state = FULLY CLASSIFIED
toolchain = COMPATIBLE
restore = PASS
format verification = PASS
build = PASS with zero errors
required tests = PASS
canonical verification = PASS
diff validation = PASS
manifest readiness = PASS
Release 1.0 implementation started = NO
WP02 started = NO
unauthorized mutations by WP01 = 0
```

If a mandatory criterion fails, return `BLOCKED`, not partial success.

Non-blocking observations must not conceal failed authority
requirements.

------------------------------------------------------------------------

## 16. GitHub Issue State

WP01 execution does **not** authorize closing issue #86.

Even if all technical exit criteria pass:

``` text
issue #86 mutation = NO
milestone #41 mutation = NO
```

Issue closure belongs to a later explicit Git/GitHub integration or
governance action if defined by the release authorities.

------------------------------------------------------------------------

## 17. Required Execution Report

Return:

``` text
# Release 1.0 WP01 — Release & Repository Preflight Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. Release 0.9 Predecessor Gate
## 5. Release 1.0 Governance Gate
## 6. GitHub Planning Gate
## 7. Toolchain Baseline
## 8. Solution / Project Baseline
## 9. Dependency Baseline
## 10. Validation Evidence
## 11. Test Evidence
## 12. Worker / Runtime Smoke Evidence
## 13. File-Manifest Readiness
## 14. Working-Tree Classification
## 15. Scope Protection
## 16. Findings
## 17. Exit-Criteria Assessment
## 18. Final Repository State
## 19. Final Decision
## 20. Next Authorized Action
```

Use tables where they make evidence clearer.

Report exact commands/results, actual test counts, actual Git
identifiers, and actual GitHub identifiers where available.

Do not claim checks you did not perform.

------------------------------------------------------------------------

## 18. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP01 PREFLIGHT COMPLETE
RELEASE 1.0 WP01 PREFLIGHT COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP01 PREFLIGHT BLOCKED
```

`COMPLETE WITH OBSERVATIONS` is allowed only when every mandatory exit
criterion passes and the observations are genuinely non-blocking.

------------------------------------------------------------------------

## 19. Next Authorized Action

If and only if WP01 completes successfully, the next work package
authorized by the Release 1.0 dependency sequence is:

``` text
WP02 — Market Data Provider Discovery
GitHub issue #87
```

Do not execute WP02.

Do not create or modify WP02 artifacts.

Stop after reporting WP01's final decision.

------------------------------------------------------------------------

## Execution Instruction

Read all authorities, establish the
predecessor/governance/GitHub/toolchain/repository baseline, run the
authorized canonical validation, reconcile the file-manifest
assumptions, classify the working tree, prove scope protection, return
the required WP01 execution report, and stop before WP02.
