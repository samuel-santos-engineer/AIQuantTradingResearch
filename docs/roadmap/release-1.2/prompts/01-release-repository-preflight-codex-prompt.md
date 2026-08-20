# Release 1.2 WP01 --- Release & Repository Preflight --- Codex Prompt

## Role

Act as the **WP01 Release & Repository Preflight Executor** for Release
1.2 of `AIQuantTradingResearch`.

This is a bounded governance, verification, and readiness work package.
Its purpose is to establish a trustworthy execution baseline for:

`Phase 3 - Release 1.2: Research Dataset Foundation`

Do not implement dataset behavior. Do not start WP02.

Use **GPT-5.6 Sol** for this work package.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before acting:

``` text
docs/roadmap/release-1.2/RELEASE_1.2_EXECUTION_PLAN.md
docs/roadmap/release-1.2/RELEASE_1.2_FILE_MANIFEST.md
docs/roadmap/release-1.2/prompts/release-1.2-github-planning-codex-prompt.md
docs/roadmap/release-1.2/prompts/release-1.2-github-planning-codex-prompt-chat.md
docs/roadmap/release-1.2/prompts/01-release-repository-preflight-codex-prompt.md
docs/roadmap/release-1.2/prompts/01-release-repository-preflight-codex-prompt-chat.md
```

Read GitHub planning state for:

``` text
Milestone #53 — Phase 3 - Release 1.2: Research Dataset Foundation
Issue #121 — Release 1.2 WP01 — Release & Repository Preflight
Issues #122–#136 — WP02–WP16
Project #2 — AIQuantTradingResearch Engineering Roadmap
```

Inspect the accepted Release 1.1 closure evidence needed to prove the
predecessor release is terminal, including PR #120, milestone #52,
issues #103--#118, and merged repository state.

Inspect repository engineering authorities and scripts only as needed to
execute WP01.

Authority precedence:

``` text
1. RELEASE_1.2_EXECUTION_PLAN.md
2. RELEASE_1.2_FILE_MANIFEST.md
3. Accepted Release 1.2 GitHub planning state
4. Accepted Release 1.1 closure/repository truth
5. Existing repository engineering/governance conventions
6. This execution prompt
```

If a lower authority materially conflicts with a higher authority, stop
and report the smallest precise blocker. Do not invent a reconciliation
authority and do not silently repair drift.

------------------------------------------------------------------------

## 2. Accepted Starting Planning Baseline

Treat the following as the accepted Release 1.2 planning baseline unless
current GitHub evidence proves drift:

``` text
Release 1.1:
  PR #120: MERGED
  milestone #52: CLOSED
  issues #103–#118: CLOSED / DONE
  terminal state: RELEASE 1.1 CLOSED

Release 1.2:
  authoritative milestone: #53
  milestone #53 state: OPEN
  milestone #53 issues: #121–#136
  WP issues: exactly 16
  WP01–WP16: OPEN / BACKLOG
  assignee: samuel-santos-engineer on 16/16
  Priority: P1 on 16/16
  Release: 1.2 on 16/16
  Area: populated on 16/16
  dependency drift: 0
  WP17+: 0
  lifecycle-gate issues: 0

Legacy:
  milestone #43 — Phase 3 - Release 1.2: Storage: CLOSED / EMPTY

Protection:
  Release 1.3 implementation: NOT STARTED
  WP02 implementation: NOT STARTED
```

Do not recreate planning objects merely because they already exist.

------------------------------------------------------------------------

## 3. Objective

Prove, with current repository and GitHub evidence, that Release 1.2 may
safely proceed from planning into execution.

WP01 must establish:

``` text
Release 1.1 terminal closure remains valid
PR #120 is merged into main
milestone #52 remains closed
issues #103–#118 remain closed/done
local main and origin/main identity are understood
repository truth is synchronized or any divergence is precisely classified
working-tree state is fully classified
the four accepted Release 1.2 pre-implementation governance artifacts are present or their exact state is understood
Release 1.2 GitHub planning still matches the accepted baseline
milestone #53 is the sole authoritative open Release 1.2 milestone
issues #121–#136 represent exactly WP01–WP16
dependency graph has zero drift
Project #2 fields remain Backlog / P1 / Release 1.2 / authoritative Area
toolchain and SDK baseline are known
restore/build/format/test/canonical verification baseline is healthy
current permanent test counts are established from execution, not assumed
current production dependency graph is established
Release 1.1 persistence/retrieval baseline remains healthy
file-manifest assumptions are reconciled against repository reality
no Release 1.2 dataset implementation has begun
no Release 1.3 implementation has begun
WP02 has not begun
```

WP01 is a **preflight/evidence package**, not a dataset design or
implementation package.

------------------------------------------------------------------------

## 4. Authorized Scope

You are authorized to:

1.  Read repository files and GitHub planning state.
2.  Inspect Git status, branches, remotes, upstreams, HEAD,
    `origin/main`, and ahead/behind state.
3.  Fetch remote metadata when required to establish current truth,
    provided no repository content or history is rewritten.
4.  Inspect PR #120 and Release 1.1 closure evidence.
5.  Inspect milestone #52, milestone #53, legacy milestone #43, issues
    #103--#136, and Project #2.
6.  Inspect the Release 1.2 authority files and prompt companions.
7.  Inspect SDK/tool versions and repository configuration.
8.  Run repository-safe diagnostic commands.
9.  Run restore, formatting verification, build, permanent tests,
    architecture tests, canonical verification, and existing
    non-mutating security/package checks.
10. Inspect solution membership, project references, package state,
    production dependency direction, current persistence implementation,
    and test inventory.
11. Classify every staged, tracked-modified, and untracked path.
12. Compare current repository truth with the accepted Release 1.1
    merged baseline and Release 1.2 manifest expectations.
13. Move issue #121 / WP01 from `Backlog` to `In Progress` **only after
    all mandatory starting-state gates and the initial technical
    baseline pass**.
14. After every WP01 acceptance gate passes, post concise evidence to
    issue #121, close it, and set its Project #2 Status to `Done`.
15. Produce the complete execution report required by this authority.

Ordinary ignored build output produced by validation is acceptable. It
must not be mistaken for governed implementation content.

------------------------------------------------------------------------

## 5. WP01 Mutation Boundary

WP01 is normally non-implementation work.

Repository mutations authorized by WP01:

``` text
normally: 0 production mutations
normally: 0 test mutations
normally: 0 documentation-semantic mutations
normally: 0 package/project/solution/build/script mutations
```

The Release 1.2 execution plan and manifest allow governance/report
artifacts only when specifically authorized. This prompt does **not**
authorize creation of an additional repository report file.

The WP01 prompt pair itself is an accepted governance input and must not
be rewritten during execution.

GitHub mutations authorized by this prompt are limited to the lifecycle
of issue #121:

``` text
Backlog → In Progress
Open → Closed
Project Status → Done
one concise evidence comment if useful
```

No other GitHub planning mutation is authorized.

------------------------------------------------------------------------

## 6. Prohibited Scope

Do not:

``` text
start WP02
define the research dataset model
decide dataset reproducibility semantics
define identity/version/provenance semantics
create dataset contracts
create dataset materialization behavior
create catalog models
create or evolve SQLite dataset schema
implement snapshot persistence
implement catalog persistence or lookup
implement dataset failure mapping
modify Worker dataset execution
modify Domain/Application/Infrastructure/Worker production code
modify permanent tests
modify architecture tests
modify architecture/data documentation
modify engineering scripts
modify package references
modify project references
modify solution membership
modify build policy
modify .editorconfig
modify .gitattributes
rewrite Release 1.2 authorities
rewrite prompt files
stage files
commit
push
create branches
create PRs
merge PRs
create tags
create GitHub Releases
close milestone #53
edit issues #122–#136
change dependencies
change labels
change assignees
change Priority/Release/Area fields
create WP17+
create lifecycle-gate issues
reopen or repurpose milestone #43
create or modify Release 1.3 planning/implementation
discard, stash, reset, clean, or overwrite accepted local work
```

Do not fix a discovered defect unless this prompt explicitly authorizes
that exact mutation.

If a correction is required but unauthorized, stop and report the
smallest corrective authority required.

------------------------------------------------------------------------

## 7. Initial Git and Repository Gate

Before substantive validation, capture:

``` text
repository identity
current branch
HEAD
origin/main
upstream
ahead/behind
working-tree status
staged paths
tracked modifications
untracked paths
recent main history
PR #120 merge commit presence
```

Expected execution branch:

``` text
main
```

Expected baseline from accepted Release 1.1 closure:

``` text
merged Release 1.1 main includes PR #120
```

Do not hardcode an old SHA as current truth. The accepted Release 1.1
closure recorded merged main at:

``` text
465c7d2f2c1cc5f99b4aa72a8d685db18951a9ad
```

Use that SHA as historical evidence, not as permission to reset current
repository truth.

If `origin/main` has legitimately advanced after Release 1.1 closure,
inspect and classify the advancement. Continue only if it is compatible
with the Release 1.2 authorities and does not represent unauthorized
Release 1.2/1.3 implementation.

Never reset or rewrite current `main` to force it back to the historical
SHA.

If local `main` is behind `origin/main`, do not automatically mutate it
unless a fast-forward is both safe and necessary under existing
repository conventions. Prefer establishing truth first. If
synchronization would constitute an unapproved local mutation under the
observed state, report it rather than guessing.

------------------------------------------------------------------------

## 8. Working-Tree Classification Gate

Classify every non-clean path as exactly one of:

``` text
A. accepted Release 1.2 governance input
B. accepted repository content already tracked
C. ordinary ignored/generated validation output
D. unexpected or ambiguous state
```

The accepted pre-implementation Release 1.2 governance set is:

``` text
RELEASE_1.2_EXECUTION_PLAN.md
RELEASE_1.2_FILE_MANIFEST.md
release-1.2-github-planning-codex-prompt.md
release-1.2-github-planning-codex-prompt-chat.md
```

The WP01 prompt pair is also an accepted governance input for this
execution.

These files may be tracked or untracked depending on the current
integration lifecycle. Their mere presence as untracked files is **not**
a defect.

Do not stage, commit, delete, normalize, relocate, or rewrite accepted
governance artifacts.

If any other untracked or modified path exists, determine whether it is
accepted repository evolution or an unexpected mutation. If ambiguity
could affect WP01 correctness, stop.

------------------------------------------------------------------------

## 9. Release 1.1 Closure Gate

Prove current evidence for:

``` text
PR #120 is MERGED
accepted Release 1.1 candidate is present on main
issues #103–#118 are 16/16 CLOSED
Project #2 Release 1.1 items are Done
milestone #52 is CLOSED
Release 1.1 terminal state remains valid
```

Also establish that Release 1.1 historical observation behavior needed
by Release 1.2 still exists:

``` text
SQLite persistence foundation exists
historical observation persistence exists
historical observation retrieval exists
target identity semantics remain available
timestamp/offset fidelity remains available
decimal fidelity remains available
idempotent/conflict behavior remains available
successful empty retrieval remains available
Persistence:DatabasePath composition remains available
bounded Worker persistence flow remains available
```

WP01 does not redesign or re-prove every internal semantic detail. It
establishes that the accepted Release 1.1 foundation is present and that
the canonical baseline remains healthy.

If Release 1.1 terminal closure cannot be proven, stop.

------------------------------------------------------------------------

## 10. Release 1.2 Governance Gate

Verify the authoritative governance set and its current state.

Required pre-implementation authorities:

``` text
docs/roadmap/release-1.2/RELEASE_1.2_EXECUTION_PLAN.md
docs/roadmap/release-1.2/RELEASE_1.2_FILE_MANIFEST.md
docs/roadmap/release-1.2/prompts/release-1.2-github-planning-codex-prompt.md
docs/roadmap/release-1.2/prompts/release-1.2-github-planning-codex-prompt-chat.md
```

Required WP01 authority:

``` text
docs/roadmap/release-1.2/prompts/01-release-repository-preflight-codex-prompt.md
docs/roadmap/release-1.2/prompts/01-release-repository-preflight-codex-prompt-chat.md
```

Verify standard `-chat` companions are exactly five lines.

Do not require later WP02--WP16 prompt pairs to exist yet. They are
separately authored before their owning work packages.

Verify that execution-plan and file-manifest responsibilities do not
materially conflict.

If a material contradiction exists, stop.

------------------------------------------------------------------------

## 11. GitHub Planning Gate

Verify current GitHub state against the accepted planning result.

### 11.1 Milestones

Require:

``` text
#52 — Release 1.1 authoritative milestone: CLOSED
#53 — Phase 3 - Release 1.2: Research Dataset Foundation: OPEN
#43 — Phase 3 - Release 1.2: Storage: CLOSED / EMPTY
```

Require exactly one authoritative open Release 1.2 milestone: #53.

Do not modify milestone #43 or #53.

### 11.2 Issues

Require exactly:

``` text
#121–#136 = WP01–WP16
```

Before WP01 lifecycle mutation:

``` text
#121: OPEN / Backlog
#122–#136: OPEN / Backlog
```

No WP17+ or lifecycle-gate issue may exist as part of authoritative
Release 1.2 planning.

### 11.3 Exact Dependencies

Verify:

``` text
WP01 ← Release 1.1 CLOSED
WP02 ← WP01
WP03 ← WP02
WP04 ← WP03
WP05 ← WP04
WP06 ← WP03, WP04
WP07 ← WP05, WP06
WP08 ← WP07
WP09 ← WP08
WP10 ← WP05, WP08, WP09
WP11 ← WP10
WP12 ← WP11
WP13 ← WP03, WP04, WP05, WP06
WP14 ← WP07, WP08, WP09, WP10, WP11, WP12
WP15 ← WP13, WP14
WP16 ← WP15
```

Require:

``` text
missing edges: 0
artificial edges: 0
dependency drift: 0
```

### 11.4 Project #2

Require all 16 Release 1.2 issues to be represented exactly once with:

``` text
Status = Backlog
Priority = P1
Release = 1.2
Area = authoritative mapped value
Assignee = samuel-santos-engineer
```

At the starting gate, WP01 must still be Backlog.

Do not move WP01 to In Progress until the repository/governance/planning
starting gates and initial technical baseline pass.

------------------------------------------------------------------------

## 12. Release 1.3 Protection Gate

Inspect current state sufficiently to prove that Release 1.3 has not
been started by Release 1.2 work.

The known historical/legacy Release 1.3 milestone #44 may exist and must
not be mutated merely because it exists.

Require:

``` text
Release 1.3 implementation introduced by Release 1.2: 0
Release 1.3 planning objects created by this execution: 0
Release 1.3 repository artifacts created by this execution: 0
```

Do not attempt to reconcile legacy Release 1.3 planning in WP01.

------------------------------------------------------------------------

## 13. Toolchain and Repository Baseline

Capture the actual environment, including where applicable:

``` text
dotnet --version
dotnet --info
global.json
target frameworks
solution membership
central package management state
repository package sources
current branch and remote state
```

Do not upgrade or alter the SDK, packages, package sources, or project
files.

Record relevant versions as evidence.

------------------------------------------------------------------------

## 14. Initial Technical Baseline

Before changing issue #121 lifecycle, run the repository's canonical
non-mutating baseline.

At minimum:

``` text
dotnet restore AIQuantTradingResearch.slnx
dotnet format AIQuantTradingResearch.slnx --verify-no-changes
dotnet build AIQuantTradingResearch.slnx --no-restore
dotnet test AIQuantTradingResearch.slnx --no-build
eng/verify.ps1
git diff --check
git diff --cached --check
```

Use repository-native equivalents if the canonical scripts establish a
slightly different exact invocation.

Also run the existing architecture-test suite and any existing
repository-owned secret/package/security validation included by
canonical verification.

Do not contact Twelve Data or any other provider as part of WP01
validation.

All WP01 validation must remain offline except for Git/GitHub metadata
operations and ordinary package restore if required by the repository.

If the baseline fails, do not move issue #121 to In Progress. Report the
failure and stop unless the failure is clearly caused only by an
external transient dependency and can be safely rerun without mutation.

------------------------------------------------------------------------

## 15. Baseline Test Inventory

Derive test counts from the actual execution.

The accepted Release 1.1 closure recorded:

``` text
Domain.Tests: 11
Application.Tests: 42
Infrastructure.Tests: 79
Architecture.Tests: 13
Total: 145
```

These are historical expectations, not values to fake.

Report current actual counts and explain any legitimate delta.

A changed count is not automatically a failure if repository truth
legitimately advanced. It is a blocker if the delta is unexplained,
unauthorized, or indicates Release 1.2/1.3 implementation already began.

------------------------------------------------------------------------

## 16. Production Dependency Graph Gate

Inspect current production project references and verify the accepted
graph remains compatible with:

``` text
Domain → none
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

Require:

``` text
unexpected production dependency edges: 0
dependency cycles: 0
Domain SQLite leakage: 0
Application SQLite leakage: 0
Domain provider/HTTP mechanics: 0
Application provider/HTTP mechanics: 0
```

Do not add or remove references.

------------------------------------------------------------------------

## 17. Release 1.1 Persistence Regression Baseline

WP01 must establish that Release 1.2 begins from a healthy Release 1.1
persistence foundation.

Use existing permanent tests and canonical verification to confirm, at
minimum, continued evidence for:

``` text
SQLite schema/bootstrap baseline
connection lifecycle
observation persistence
idempotency
conflict preservation
atomicity
immutable accepted history
historical retrieval
target isolation
ascending ordering
timestamp/offset fidelity
decimal fidelity
successful empty retrieval
failure mapping
DI/configuration
bounded Worker persistence execution
```

Do not create new tests or temporary semantic probes unless an existing
repository verification mechanism already does so.

WP01 is not the place to improve coverage.

------------------------------------------------------------------------

## 18. File-Manifest Reconciliation

Reconcile current repository reality with
`RELEASE_1.2_FILE_MANIFEST.md`.

Verify:

``` text
WP01 has not mutated prohibited src/** surfaces
WP01 has not mutated prohibited tests/** surfaces
WP01 has not mutated package/build/solution files
WP01 has not mutated eng/**
WP01 has not mutated .github/**
WP02 definition artifacts have not already appeared as unauthorized implementation
WP03+ implementation artifacts have not already appeared
Release 1.3 implementation artifacts have not appeared
temporary SQLite/probe/generated artifacts are not being treated as candidate content
```

Do not hardcode a final Release 1.2 candidate path count in WP01.

------------------------------------------------------------------------

## 19. WP02 Protection

Before WP01 can complete, explicitly prove WP02 has not begun.

Require:

``` text
issue #122 remains OPEN / Backlog
no Research Dataset Definition & Reproducibility Model implementation produced by WP01
no WP02 decision/definition artifact created by WP01
no dataset identity/version/provenance design started by WP01
no source/test/schema/Worker mutation attributable to WP02
```

WP01 may inspect existing architecture/data documentation for baseline
understanding, but must not edit it.

------------------------------------------------------------------------

## 20. Issue #121 Lifecycle

Only after Sections 7--19 pass:

1.  Move issue #121 / WP01 Project Status from `Backlog` to
    `In Progress`.
2.  Perform the final WP01 evidence pass.
3.  Confirm no unauthorized repository mutation occurred.
4.  Confirm issue #122 remains Open/Backlog.
5.  Post concise evidence to #121.
6.  Close issue #121.
7.  Set Project Status to `Done`.

Do not close #121 if any acceptance gate is unresolved.

Do not modify Priority, Release, Area, assignee, dependencies, title,
body, milestone, or labels unless the accepted planning state itself
proves they have drifted; even then, WP01 does not authorize repair.
Report drift instead.

------------------------------------------------------------------------

## 21. Final Validation

After the issue lifecycle change, rerun enough non-mutating checks to
prove that WP01 itself did not alter repository correctness.

At minimum confirm:

``` text
working-tree classification unchanged except ordinary ignored validation output
staged paths = 0
tracked WP01 implementation mutations = 0
restore/build/test baseline remains healthy
architecture tests pass
canonical verification passes
git diff --check passes
git diff --cached --check passes
issue #121 = CLOSED / Done
issue #122 = OPEN / Backlog
milestone #53 remains OPEN
Release 1.3 implementation remains not started
```

Remove only temporary artifacts created by WP01 itself when safe and
explicitly identifiable. Never remove pre-existing user work or accepted
governance inputs.

------------------------------------------------------------------------

## 22. Blocker Policy

Stop without implementation if any of these occurs:

``` text
Release 1.1 closure cannot be proven
PR #120 is not merged
milestone #52 is unexpectedly open
authoritative Release 1.2 milestone #53 is missing, duplicated, or closed
legacy milestone #43 is unexpectedly active in a way that conflicts with accepted planning
issues #121–#136 do not represent exactly WP01–WP16
dependency drift is nonzero
Project #2 required fields materially drift
WP02 or later Release 1.2 implementation has already begun without accepted authority
Release 1.3 implementation has begun
working-tree state contains unexplained material mutations
authority files materially conflict
canonical baseline fails
architecture baseline fails
security/package baseline exposes an unresolved blocker
required correction exceeds WP01 mutation authority
```

For a blocker, report:

``` text
blocker ID
observed evidence
why it violates authority
what was not mutated
smallest corrective authority required
safe resume point
```

Do not create a recursive authority chain for cosmetic findings.
Whitespace or formatting findings are simply baseline failures unless an
existing authority already permits their correction.

------------------------------------------------------------------------

## 23. Required Execution Report

Produce a structured report containing at least:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Release 1.1 Closure Gate
7.  Release 1.2 Governance Gate
8.  GitHub Planning Gate
9.  Release 1.3 Protection
10. Toolchain / SDK Baseline
11. Initial Restore / Format / Build Evidence
12. Initial Permanent Test Evidence
13. Canonical Verification Evidence
14. Architecture Baseline
15. Release 1.1 Persistence Regression Baseline
16. File-Manifest Reconciliation
17. WP02 Protection
18. Issue #121 Lifecycle
19. Security / Offline Evidence
20. Whitespace / Diff Evidence
21. Mutation Accounting
22. Git / GitHub Protection
23. Findings / Blockers
24. Acceptance Matrix
25. Final Repository / GitHub State
26. WP02 Handoff
27. Final Decision
28. Next Authorized Work Package

Report actual observed values. Do not copy historical counts or SHAs as
if newly verified.

------------------------------------------------------------------------

## 24. Acceptance Matrix

WP01 passes only if all applicable rows pass:

  Requirement                          Required result
  ------------------------------------ -----------------------------------
  Release 1.1 terminal closure         PASS
  PR #120                              MERGED
  Milestone #52                        CLOSED
  Issues #103--#118                    CLOSED / DONE
  Release 1.2 governance authorities   PRESENT / CONSISTENT
  WP01 chat companion                  EXACTLY 5 LINES
  Milestone #53                        EXACTLY 1 / OPEN
  WP01--WP16 issues                    16/16
  Dependency drift                     0
  Project membership                   16/16
  Starting Project Status              Backlog 16/16
  Priority                             P1 16/16
  Release                              1.2 16/16
  Area                                 populated 16/16
  WP17+                                0
  Lifecycle-gate issues                0
  Legacy #43                           CLOSED / EMPTY
  Local/remote repository state        CLASSIFIED / SAFE
  Unexpected working-tree paths        0 or fully explained non-blocking
  Restore                              PASS
  Format verification                  PASS
  Build                                PASS / 0 errors
  Permanent tests                      PASS
  Architecture tests                   PASS
  Canonical verification               PASS
  Production dependency drift          0
  Domain/Application SQLite leakage    0
  Release 1.1 persistence regression   PASS
  Unauthorized production mutations    0
  Unauthorized test mutations          0
  Package/reference delta              0/0
  WP02 started                         NO
  Release 1.3 implementation started   NO
  Issue #121 final state               CLOSED / DONE
  Issue #122 final state               OPEN / BACKLOG
  Milestone #53 final state            OPEN
  Staged paths                         0

------------------------------------------------------------------------

## 25. Required Terminal Summary

If successful, end exactly with:

``` text
RELEASE 1.2 WP01 COMPLETE

RELEASE & REPOSITORY PREFLIGHT:
Release 1.1 closure: PASS
PR #120 merged: PASS
Milestone #52 closed: PASS
Release 1.2 governance: PASS
Milestone #53 authoritative/open: PASS
WP01–WP16 planning: PASS
Dependency drift: 0
Repository baseline: PASS
Restore/build/format: PASS
Permanent tests: PASS
Architecture validation: PASS
Canonical verification: PASS
Release 1.1 persistence regression: PASS
Unauthorized production mutations: 0
Unauthorized test mutations: 0
Package/reference delta: 0/0
WP02 started: NO
Release 1.3 implementation started: NO
Issue #121: CLOSED / DONE

NEXT AUTHORIZED WORK PACKAGE:
WP02 — Research Dataset Definition & Reproducibility Model
GitHub issue #122
```

If blocked, end exactly with:

``` text
RELEASE 1.2 WP01 BLOCKED
```

Do not print the success terminal unless every mandatory acceptance gate
passes.

------------------------------------------------------------------------

## 26. Final Constraint

WP01 establishes the trusted starting point for Release 1.2.

It must not make the repository appear ready by repairing evidence it
was only authorized to inspect.

**Verify first. Classify precisely. Mutate only issue #121 lifecycle
after the baseline passes. Preserve Release 1.1. Do not start WP02.**
