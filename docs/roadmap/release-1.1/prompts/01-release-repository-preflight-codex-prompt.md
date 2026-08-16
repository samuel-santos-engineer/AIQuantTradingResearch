# Release 1.1 WP01 --- Release & Repository Preflight --- Authoritative Codex Execution Prompt

## 1. Authority

You are executing **Release 1.1 --- WP01: Release & Repository Preflight** for:

``` text
Repository: samuel-santos-engineer/AIQuantTradingResearch
Release:    Phase 3 - Release 1.1: Market Data Persistence Foundation
Work item:  GitHub issue #103
WP:         WP01
```

This file is the authoritative WP01 execution contract.

Read this file completely before taking any action.

The standard five-line companion:

``` text
docs/roadmap/release-1.1/prompts/01-release-repository-preflight-codex-prompt-chat.md
```

is only a bootstrap. It does not duplicate or supersede this contract.

------------------------------------------------------------------------

## 2. Governing Authority Precedence

Apply authority in this order:

1.  Explicit human instructions in the current execution conversation.
2.  This authoritative WP01 prompt.
3.  `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`.
4.  `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`.
5.  The accepted Release 1.1 governance-baseline post-merge closure result.
6.  GitHub issue #103 and the accepted Release 1.1 planning state.
7.  Current repository and GitHub truth.
8.  Existing repository engineering conventions where they do not conflict with higher authority.

Do not infer authority to expand scope.

If two authorities cannot be reconciled without mutation outside this prompt, stop and report `WP01 BLOCKED`.

------------------------------------------------------------------------

## 3. Purpose

WP01 establishes the exact Release 1.1 starting state and proves that the repository is safe to enter persistence technology discovery.

This is a **preflight and evidence work package**, not an implementation package.

WP01 must answer, with evidence:

-   Is Release 1.0 fully closed?
-   Is the accepted Release 1.1 governance baseline merged and synchronized?
-   Is the working repository technically green?
-   What is the permanent test baseline?
-   What is the current production dependency graph?
-   What persistence-related surfaces already exist in Domain, Application, Infrastructure, and Worker?
-   Does any unauthorized Release 1.1 persistence implementation already exist?
-   Does any existing implementation conflict with the Release 1.1 plan?
-   Is GitHub planning still coherent and is WP01 the only work package being executed?
-   Can WP02 safely begin without repairing or redesigning the repository first?

WP01 must not choose a persistence technology.

------------------------------------------------------------------------

## 4. Accepted Starting Lifecycle State

The accepted governance-baseline closure established the following starting state:

``` text
Release 1.0:                    CLOSED
Release 1.1 governance baseline: CLOSED
Release 1.1 milestone #52:      OPEN
Release 1.1 WP issues:          #103–#118
WP01 / issue #103:              OPEN / Backlog
WP02–WP16:                      OPEN / Backlog
Legacy milestone #42:           CLOSED / empty
Legacy milestone #43:           CLOSED / empty
Active Release 1.2 planning:    0
```

Accepted merged governance baseline:

``` text
PR #119:      MERGED
main HEAD:    9ce7af388b9818bf4374897fc4615e17ccc1615a
origin/main:  9ce7af388b9818bf4374897fc4615e17ccc1615a
```

The accepted four-file governance baseline is:

``` text
docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md
docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md
docs/roadmap/release-1.1/prompts/release-1.1-github-planning-codex-prompt.md
docs/roadmap/release-1.1/prompts/release-1.1-github-planning-codex-prompt-chat.md
```

Treat these values as expected evidence, not as permission to fabricate current state. Re-query Git/GitHub and reconcile them against repository truth.

------------------------------------------------------------------------

## 5. WP01 Governance Pair and Recursion-Safe Handling

The file manifest explicitly authorizes exactly these new WP01 governance files:

``` text
docs/roadmap/release-1.1/prompts/01-release-repository-preflight-codex-prompt.md
docs/roadmap/release-1.1/prompts/01-release-repository-preflight-codex-prompt-chat.md
```

These two files are **EXPECTED GOVERNANCE** for WP01.

Their presence as untracked files when execution starts is expected and must **not** be classified as implementation drift, an unexpected mutation, or a dirty-tree blocker.

This rule intentionally prevents governance-artifact recursion.

For this WP:

-   do not stage them;
-   do not commit them;
-   do not push them;
-   do not delete them;
-   do not relocate them;
-   do not rewrite them;
-   do not normalize their whitespace;
-   do not create another authority merely because they are untracked.

The chat companion must contain exactly five lines.

If either file is missing, duplicated, or located outside the manifest-authorized prompt directory, stop with `WP01 BLOCKED`.

At WP01 completion, the expected local candidate is allowed to contain exactly this untracked pair and no other WP01-created repository mutation.

------------------------------------------------------------------------

## 6. Required Initial Inspection

Before any GitHub planning mutation, inspect and record:

``` text
current branch
HEAD
origin/main
upstream
ahead/behind
staged paths
unstaged paths
untracked paths
recent relevant commits
open PRs relevant to Release 1.1
```

Fetch remote state safely before relying on remote references.

Do not:

``` text
reset
clean
stash
rebase
force checkout
discard user work
rewrite history
```

If unrelated user work or an ambiguous repository mutation is present, stop rather than altering it.

------------------------------------------------------------------------

## 7. Working-Tree Classification

Classify every visible non-HEAD path into exactly one category:

``` text
EXPECTED GOVERNANCE
WP01 AUTHORIZED
EXPECTED GENERATED / IGNORED
UNEXPECTED
```

For this execution, the normal expected classification is:

``` text
EXPECTED GOVERNANCE
  01-release-repository-preflight-codex-prompt.md
  01-release-repository-preflight-codex-prompt-chat.md

WP01 AUTHORIZED
  none

UNEXPECTED
  none
```

The four merged Release 1.1 governance-baseline files are tracked baseline content and are not working-tree changes.

If any additional staged, unstaged, or untracked path exists and cannot be proven generated/ignored and harmless, stop with `WP01 BLOCKED`.

Do not delete unexpected files to make the gate pass.

------------------------------------------------------------------------

## 8. Release 1.0 Closure Reconciliation

Verify current GitHub/repository evidence that Release 1.0 remains terminal.

At minimum inspect:

``` text
PR #102
Milestone #41
Issues #86–#101
main history containing the Release 1.0 integration
Release 1.0 closure state
```

Required result:

``` text
PR #102 = MERGED
Milestone #41 = CLOSED
Issues #86–#101 = CLOSED
Release 1.0 terminal = RELEASE 1.0 CLOSED
```

Do not modify Release 1.0 objects.

If terminal state has regressed, stop with `WP01 BLOCKED`.

------------------------------------------------------------------------

## 9. Release 1.1 Governance-Baseline Reconciliation

Verify:

``` text
PR #119 = MERGED
accepted PR head = e6f24b860e5d3112e6bf52488e52b54152003018
accepted merge commit = 9ce7af388b9818bf4374897fc4615e17ccc1615a
```

Verify the accepted governance commit is represented on `main`.

Verify the four merged governance files are present.

Verify the planning chat companion remains exactly five lines.

Do not repair these files in WP01.

Any substantive drift is a blocker.

------------------------------------------------------------------------

## 10. Main Synchronization Gate

WP01 requires a trustworthy current `main`.

If the working tree contains only the recognized WP01 governance pair and no unrelated work, safely synchronize local `main` with `origin/main` using non-destructive operations.

Required final Git baseline:

``` text
branch = main
main = origin/main
ahead/behind = 0/0
staged = 0
unstaged tracked = 0
untracked = exactly the recognized WP01 governance pair
```

If remote `main` has advanced beyond the accepted governance merge, inspect the intervening commits before proceeding.

Proceed only if those commits are clearly compatible with the Release 1.1 planning/implementation gate and introduce no unauthorized Release 1.1 persistence implementation.

Otherwise stop with `WP01 BLOCKED`.

------------------------------------------------------------------------

## 11. Release 1.1 GitHub Planning Reconciliation

Inspect current planning truth.

Required authoritative state:

``` text
Milestone #52
  title = Phase 3 - Release 1.1: Market Data Persistence Foundation
  state = OPEN
  issues = #103–#118

Issues #103–#118
  present exactly once
  no WP17+
  no lifecycle-gate issue
  assigned to samuel-santos-engineer
  Release = 1.1
  Priority = P1
  Area populated
```

The authoritative dependency graph must remain:

``` text
WP01 ← Release 1.0 CLOSED
WP02 ← WP01
WP03 ← WP02
WP04 ← WP03
WP05 ← WP04
WP06 ← WP05
WP07 ← WP06
WP08 ← WP07
WP09 ← WP08
WP10 ← WP09
WP11 ← WP10
WP12 ← WP11
WP13 ← WP03, WP04, WP05
WP14 ← WP06–WP12
WP15 ← WP13, WP14
WP16 ← WP15
```

Required legacy/future state:

``` text
Milestone #42 = CLOSED / empty
Milestone #43 = CLOSED / empty
Active Release 1.2 planning = 0
Release 1.2 implementation = not started
```

Do not repair planning drift under WP01. Report it and stop.

------------------------------------------------------------------------

## 12. Issue #103 Execution-State Handling

Issue #103 is the authoritative WP01 work item.

After all pre-mutation repository/GitHub gates above pass, you may perform the narrow execution-state transition:

``` text
WP01 / issue #103 Project Status:
Backlog → In Progress
```

Do not change:

``` text
title
body
milestone
assignee
label
Priority
Release
Area
dependencies
```

Do not move any other WP to `In Progress`.

If Project automation performs an equivalent expected transition, reconcile it rather than duplicating it.

If the status cannot be changed safely, continue the technical preflight only if no mutation has occurred, then report the status blocker; do not invent success.

------------------------------------------------------------------------

## 13. Canonical Technical Baseline

Run the repository's canonical validation from synchronized `main`.

At minimum execute and record:

``` text
dotnet restore AIQuantTradingResearch.slnx --nologo
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Also record:

``` text
build warnings
build errors
Domain.Tests
Application.Tests
Infrastructure.Tests
Architecture.Tests
total permanent tests
failed tests
skipped tests
```

Release 1.0 closed with the historical baseline:

``` text
Domain.Tests          11
Application.Tests     16
Infrastructure.Tests  65
Architecture.Tests    13
Total                 105
```

These counts are evidence, not hardcoded Release 1.1 targets.

If current `main` legitimately contains more tests, record current truth.

Required gate:

``` text
restore = PASS
build = PASS
build errors = 0
eng/verify.ps1 = PASS
all permanent test suites = PASS
```

Do not repair failures under WP01.

------------------------------------------------------------------------

## 14. Diff and Whitespace Baseline

Run:

``` text
git diff --check
git diff --cached --check
```

Both must pass for tracked/staged state.

Because the WP01 governance pair is untracked and therefore invisible to normal Git diff checks, perform a direct read-only whitespace inspection of those two Markdown files and report:

``` text
trailing whitespace findings
terminal blank-line findings, if any
chat companion line count
```

Whitespace findings confined to the immutable untracked WP01 authority pair are **recorded observations**, not authorization to edit those files and not, by themselves, implementation drift.

Do not create a recursive whitespace authority during WP01.

Any future normalization of governance files belongs to later candidate reconciliation/integration authority unless the human explicitly authorizes otherwise.

------------------------------------------------------------------------

## 15. Production Dependency Graph Inventory

Inspect project references and relevant source structure.

Record the actual production dependency graph.

The accepted Release 1.0 baseline is expected to remain:

``` text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

Verify:

-   no production dependency cycle;
-   no Domain dependency on Application/Infrastructure/Worker;
-   no Application dependency on Infrastructure/Worker;
-   Infrastructure remains the concrete external-integration layer;
-   Worker remains composition/execution.

Do not modify references or architecture tests.

Any incompatible graph drift is a blocker.

------------------------------------------------------------------------

## 16. Persistence-Related Surface Inventory

Perform a bounded repository inspection of the current Release 1.0 implementation.

Inventory persistence-relevant types, contracts, configuration, composition, and data flow in:

``` text
src/AIQuantTradingResearch.Domain/**
src/AIQuantTradingResearch.Application/**
src/AIQuantTradingResearch.Infrastructure/**
src/AIQuantTradingResearch.Worker/**
```

Also inspect relevant project/package configuration read-only where needed to understand the baseline.

The inventory must distinguish:

1.  Existing market-data semantics.
2.  Existing Application contracts/use cases.
3.  Existing Infrastructure provider/client behavior.
4.  Existing Worker composition/configuration.
5.  Any existing storage/persistence abstractions.
6.  Any concrete database/storage engine dependency.
7.  Any existing serialization/file/database persistence behavior.
8.  Any persistence-related configuration.
9.  Any generated/local storage artifacts or ignore rules relevant to future work.
10. Any implementation that could constrain WP02 technology discovery.

Do not interpret generic in-memory collections, provider HTTP transport, logging, configuration binding, or test fixtures as durable persistence without evidence.

------------------------------------------------------------------------

## 17. Persistence Conflict Search

Search the repository for evidence of pre-existing durable persistence technology or implementation.

Use evidence-driven searches for concepts such as:

``` text
database
storage
persistence
repository
DbContext
SQLite
SQL
connection string
file-backed storage
serialization used as durable state
schema
migration
```

Also inspect package references for storage/database/ORM dependencies.

Do not assume a technology merely from a name.

Classify every meaningful finding as:

``` text
existing Release 1.0 behavior
documentation-only future concept
test-only artifact
generated/ignored state
potential Release 1.1 implementation drift
actual persistence conflict
```

Required success result:

``` text
unauthorized Release 1.1 implementation = 0
existing persistence conflict = 0
```

If either is nonzero, do not delete or repair it. Stop with evidence.

------------------------------------------------------------------------

## 18. Security / Secret Safety

Do not expose credentials or secret values.

When inspecting configuration:

-   report key names and whether values are present when necessary;
-   redact actual secret values;
-   do not print API keys;
-   do not print credential-bearing connection strings;
-   do not create `.env` or secret files;
-   do not create a database;
-   do not call live providers merely to prove preflight.

WP01 requires no live Twelve Data request.

------------------------------------------------------------------------

## 19. Generated / Runtime Artifact Protection

Do not create or commit:

``` text
database files
test databases
local persistence files
production market-data fixtures
.env files
secret files
IDE state
build outputs as candidate files
package caches
temporary repository reports
probe files
```

Normal build/test output may be generated by validation but must remain ignored/non-candidate state.

If validation leaves unexpected repository-visible artifacts, classify them and remove them only when they are provably execution-generated disposable artifacts. Never remove pre-existing user files.

------------------------------------------------------------------------

## 20. Explicit Mutation Boundary

WP01 authorizes **no production, test, package, project, solution, build, script, workflow, architecture-documentation, or persistence-design mutation**.

Specifically prohibited:

``` text
src/**
tests/**
Directory.Packages.props
Directory.Build.props
*.csproj
*.slnx
eng/**
.github/**
README.md
docs/architecture/**
```

No persistence package may be added.

No storage technology may be selected.

No schema may be created.

No Application persistence contract may be created.

No Domain persistence semantics may be changed.

No Infrastructure persistence implementation may be created.

No Worker persistence behavior may be created.

No permanent test may be added.

No WP02 artifact may be created.

------------------------------------------------------------------------

## 21. Git Transport Prohibition

WP01 must not perform:

``` text
git add
git commit
git commit --amend
git push
git tag
PR creation
PR merge
branch publication
force push
history rewrite
```

Do not create a WP01 implementation branch.

The cumulative Release 1.1 candidate remains uncommitted until the dedicated integration lifecycle after WP16 unless later explicit authority changes that strategy.

------------------------------------------------------------------------

## 22. No WP02 Execution

WP02 --- Persistence Technology Discovery is a separate work package.

WP01 may identify facts WP02 must consider, but must not:

-   compare technology candidates;
-   rank databases;
-   select SQLite or any alternative;
-   create `MARKET_DATA_PERSISTENCE_ASSESSMENT.md`;
-   create `MARKET_DATA_PERSISTENCE_DECISION.md`;
-   add packages;
-   design a physical schema.

WP01's terminal recommendation is only whether WP02 is safe to begin.

------------------------------------------------------------------------

## 23. WP01 Acceptance Gates

WP01 may succeed only if all of the following are true:

``` text
Release 1.0 terminal                              PASS
PR #102 merged                                    PASS
Release 1.1 governance baseline merged            PASS
PR #119 merged                                    PASS
main synchronized with origin/main                PASS
tracked working tree clean                        PASS
only recognized WP01 governance pair untracked    PASS
unexpected repository paths                       0
Release 1.1 milestone #52                         OPEN
WP issues #103–#118                               present
dependency drift                                  0
WP17+                                             0
active Release 1.2 planning                       0
restore                                            PASS
build                                              PASS
build errors                                       0
canonical verification                            PASS
all permanent test suites                          PASS
permanent test baseline                            recorded
Architecture.Tests baseline                        recorded
git diff --check                                   PASS
git diff --cached --check                          PASS
production dependency graph                        recorded
unauthorized Release 1.1 implementation            0
existing persistence conflict                      0
production/test/package/project mutations by WP01  0
WP02 implementation                                0
```

The WP01 governance pair itself is expected governance and does not count as implementation mutation.

------------------------------------------------------------------------

## 24. WP01 Completion Mutation

Only after every technical and governance acceptance gate passes, update WP01 planning state.

Required final issue state:

``` text
Issue #103 = CLOSED
Project Status = Done
```

Before closure, add a concise evidence comment to issue #103 summarizing:

-   synchronized `main` SHA;
-   canonical verification result;
-   permanent test counts;
-   architecture graph result;
-   persistence-surface inventory conclusion;
-   unauthorized Release 1.1 implementation count;
-   persistence conflict count;
-   confirmation that WP02 was not started.

Allow normal Project automation to move the closed issue to `Done` when repository convention supports it. If automation does not do so and the existing Project contract permits a direct status update, set only WP01 to `Done`.

Do not close or modify WP02--WP16.

If issue closure or `Done` reconciliation fails after the technical gates passed, report `WP01 BLOCKED` with the exact partial planning state. Do not roll back technical evidence or mutate unrelated planning.

------------------------------------------------------------------------

## 25. Required Final Repository State

On successful completion:

``` text
branch = main
HEAD = origin/main
ahead/behind = 0/0
staged = 0
unstaged tracked = 0
untracked = exactly:
  docs/roadmap/release-1.1/prompts/01-release-repository-preflight-codex-prompt.md
  docs/roadmap/release-1.1/prompts/01-release-repository-preflight-codex-prompt-chat.md
unexpected = 0
repository commit created = NO
push performed = NO
PR created = NO
```

If execution-generated ignored build/test output exists, it must not appear as candidate state.

------------------------------------------------------------------------

## 26. Required Final GitHub State

On successful completion:

``` text
Milestone #52 = OPEN
Issue #103 = CLOSED / Done
Issues #104–#118 = OPEN
WP02/#104 = Backlog
WP03–WP16 = not started
Legacy milestone #42 = CLOSED / empty
Legacy milestone #43 = CLOSED / empty
Active Release 1.2 planning = 0
```

Milestone #52 should then naturally show:

``` text
closed issues = 1
open issues = 15
```

unless GitHub presentation semantics differ; report actual truth without fabricating it.

------------------------------------------------------------------------

## 27. Execution Report Contract

Return a complete Markdown report titled:

``` text
# Release 1.1 WP01 Release & Repository Preflight Execution Report
```

At minimum include:

1.  Executive Summary
2.  Authorities Reviewed
3.  Authentication / Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Release 1.0 Closure Reconciliation
7.  Release 1.1 Governance-Baseline Reconciliation
8.  Main Synchronization
9.  Release 1.1 Planning Reconciliation
10. WP01 Issue-State Handling
11. Restore Evidence
12. Build Evidence
13. Permanent Test Baseline
14. Canonical Verification
15. Diff / Whitespace Evidence
16. Production Dependency Graph
17. Persistence-Related Surface Inventory
18. Persistence Conflict Search
19. Security / Credential Safety
20. Repository Mutation Accounting
21. Git / GitHub Protection
22. WP02 Protection
23. Findings / Observations
24. WP01 Acceptance Matrix
25. Final GitHub State
26. Final Repository State
27. Final Decision
28. Next Authorized Work Package

Use exact observed counts and SHAs.

Do not fabricate hosted checks, GitHub state, tests, file counts, or command results.

------------------------------------------------------------------------

## 28. Success Terminal

Emit the following terminal only if every WP01 acceptance gate passes and issue #103 is closed/Done:

``` text
RELEASE 1.1 WP01 COMPLETE

NEXT AUTHORIZED WORK PACKAGE:
WP02 — Persistence Technology Discovery
GitHub issue #104
```

WP02 must not be executed in this run.

------------------------------------------------------------------------

## 29. Blocked Terminal

If any mandatory gate fails, emit:

``` text
RELEASE 1.1 WP01 BLOCKED
```

The report must state:

-   exact blocker;
-   observed evidence;
-   affected authority/gate;
-   mutations already performed, if any;
-   minimum narrow corrective authority or action required;
-   exact final repository/GitHub state.

Do not repair outside WP01 authority merely to reach success.

------------------------------------------------------------------------

## 30. Final Instruction

Execute WP01 as a bounded, evidence-first preflight.

Prove the baseline before persistence design begins.

Preserve the merged Release 1.1 governance baseline, preserve all future work packages, treat this WP01 prompt pair as recursion-safe expected governance, perform no implementation or Git transport, and authorize WP02 only through a truthful successful WP01 terminal.
