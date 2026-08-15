# Release 1.0 GitHub Planning Reconciliation --- Codex Prompt

## Role

Act as the **Release 1.0 GitHub Planning Reconciliation Executor** for
`AIQuantTradingResearch`.

This is a narrowly scoped governance-unblock task. Its sole purpose is
to reconcile the stale GitHub roadmap identities and the local Release
1.0 prompt-directory discrepancy reported by the blocked Release 1.0
GitHub-planning execution.

Do not perform Release 1.0 planning itself. Do not start WP01.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before acting:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md
```

Also read the existing Release 1.0 GitHub-planning prompt and its
companion from their actual current location, whether that is:

``` text
docs/roadmap/release-1.0/prompt/
```

or:

``` text
docs/roadmap/release-1.0/prompts/
```

Read the blocked Release 1.0 GitHub Planning Execution Report available
in the current Codex context if present.

Inspect the existing GitHub milestone state, especially:

``` text
Milestone #41 — Phase 2 - Release 1.0: Plugin Framework
Milestone #42 — Phase 3 - Release 1.1: Market Data Platform
```

The Release 1.0 execution plan and file manifest remain authoritative
for Release 1.0.

------------------------------------------------------------------------

## 2. Human Reconciliation Authorization

The human explicitly authorizes the following semantic reconciliation:

### R10-01 --- Reconcile milestone #41

Milestone #41 is an unused legacy planning placeholder and may be
repurposed for the authoritative Release 1.0.

Change milestone #41 from:

``` text
Phase 2 - Release 1.0: Plugin Framework
```

to:

``` text
Phase 3 - Release 1.0: Market Data Foundation
```

Replace its legacy description with a concise description derived
strictly from the Release 1.0 execution plan.

The description must communicate that Release 1.0 establishes the first
real external historical market-data vertical slice through one
evidence-selected provider while preserving provider-independent
Domain/Application boundaries and Infrastructure ownership of provider
mechanics.

Keep milestone #41 **OPEN**.

Do not create a second Release 1.0 milestone.

### R10-02 --- Retire legacy milestone #42

Milestone #42:

``` text
Phase 3 - Release 1.1: Market Data Platform
```

is superseded legacy roadmap planning.

Close milestone #42.

Do **not** delete it. Do **not** rename it. Do **not** repurpose it. Do
**not** create a replacement Release 1.1 milestone. Do **not** create
Release 1.1 issues.

Its historical identity must remain visible as superseded legacy
planning.

### R10-03 --- Normalize the Release 1.0 prompt directory

The authoritative Release 1.0 governance convention is:

``` text
docs/roadmap/release-1.0/prompts/
```

If the current local directory is:

``` text
docs/roadmap/release-1.0/prompt/
```

rename/move it to:

``` text
docs/roadmap/release-1.0/prompts/
```

Preserve every contained governance artifact
byte-for-byte/content-equivalently except for path relocation required
by the rename.

Do not edit the contents merely to normalize formatting, whitespace,
links, wording, or line endings.

If both `prompt/` and `prompts/` already exist, stop and report the
exact contents/conflict rather than merging or overwriting.

------------------------------------------------------------------------

## 3. Exact Authorized Mutation Set

This task authorizes only:

``` text
GitHub:
  milestone #41 title
  milestone #41 description
  milestone #41 remains OPEN
  milestone #42 state -> CLOSED

Repository working tree:
  docs/roadmap/release-1.0/prompt/
    ->
  docs/roadmap/release-1.0/prompts/
```

The directory relocation is authorized only if required by the actual
current state.

No other mutation is authorized.

------------------------------------------------------------------------

## 4. Prohibited Scope

Do not:

``` text
create Release 1.0 WP issues
edit existing issues
assign issues to milestones
change labels
change GitHub Project items or fields
create/delete milestones
rename milestone #42
edit milestone #42 description
create Release 1.1 planning
modify Release 0.9 planning
start WP01
modify production code
modify tests
modify architecture documentation
modify Release 1.0 execution plan
modify Release 1.0 file manifest
modify prompt contents
stage files
commit
push
create a branch
create a PR
merge anything
create a tag
create a GitHub Release
change repository settings/workflows/templates
```

This task is reconciliation only.

------------------------------------------------------------------------

## 5. Preflight

Before mutation, capture and report:

``` text
current branch
HEAD
origin/main
ahead/behind
working-tree state
staged state
actual Release 1.0 directory tree
GitHub authentication
milestone #41 title/state/description/open+closed issue counts
milestone #42 title/state/description/open+closed issue counts
```

Confirm whether milestone #41 and #42 still have zero issues as reported
by the blocked planning execution.

If either milestone now contains issues, stop before mutation and report
a blocker because the human authorization was based on unused legacy
placeholders.

Confirm no equivalent authoritative
`Phase 3 - Release 1.0: Market Data Foundation` milestone already exists
under another milestone number.

If one exists, stop before mutation.

------------------------------------------------------------------------

## 6. Repository Path Reconciliation

If only `docs/roadmap/release-1.0/prompt/` exists:

1.  Inventory every file beneath it.
2.  Rename the directory to `prompts/`.
3.  Prove the file set is identical before/after.
4.  Prove file contents are unchanged.
5.  Do not stage the rename.

If only `prompts/` already exists, no repository mutation is required.

If neither exists, stop and report a blocker.

If both exist, stop and report a blocker.

The expected planning artifacts include at least:

``` text
release-1.0-github-planning-codex-prompt.md
release-1.0-github-planning-codex-prompt-chat.md
release-1.0-github-planning-reconciliation-codex-prompt.md
release-1.0-github-planning-reconciliation-codex-prompt-chat.md
```

Do not infer that missing files should be created during this task.

------------------------------------------------------------------------

## 7. GitHub Milestone Reconciliation

After all preflight gates pass:

### Milestone #41

Set:

``` text
title:
Phase 3 - Release 1.0: Market Data Foundation

state:
OPEN
```

Set its description strictly from Release 1.0 authority. Keep it concise
and do not introduce implementation detail not present in the execution
plan.

### Milestone #42

Set:

``` text
state:
CLOSED
```

Preserve its existing title and description.

Do not attach new issues to either milestone during this task.

------------------------------------------------------------------------

## 8. Validation

After reconciliation, prove:

### GitHub

``` text
#41 title = Phase 3 - Release 1.0: Market Data Foundation
#41 state = OPEN
#41 description = aligned with Release 1.0 authority
#41 issue count = unchanged

#42 title = Phase 3 - Release 1.1: Market Data Platform
#42 state = CLOSED
#42 description = unchanged
#42 issue count = unchanged

other milestones mutated = 0
issues mutated = 0
labels mutated = 0
Project mutations = 0
Release 1.1 replacement planning created = NO
```

Also search again and prove there is exactly one open milestone
representing authoritative Release 1.0.

### Repository

Prove:

``` text
docs/roadmap/release-1.0/prompts/ exists
docs/roadmap/release-1.0/prompt/ does not exist
file inventory preserved
prompt contents unchanged
staged changes = none
commit created = NO
push performed = NO
PR created = NO
```

The authorized directory relocation may remain as uncommitted/untracked
repository state for later governance integration. Do not commit it in
this task.

### Scope

Prove:

``` text
Release 1.0 WP issues created = 0
WP01 started = NO
Release 1.0 implementation changes = 0
Release 0.9 planning mutations = 0
```

------------------------------------------------------------------------

## 9. Blocker Policy

Stop rather than guess if:

``` text
milestone #41 or #42 contains issues
another authoritative Release 1.0 milestone already exists
both prompt/ and prompts/ exist
neither prompt/ nor prompts/ exists
directory relocation would overwrite files
GitHub permissions are insufficient
authority files are missing or contradictory
an unexpected mutation is required
```

Do not broaden this authorization.

Report the blocker, evidence, mutations already performed, and the
minimum human decision required.

------------------------------------------------------------------------

## 10. Required Execution Report

Return:

``` text
# Release 1.0 GitHub Planning Reconciliation Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. Initial GitHub Planning State
## 5. Preflight Assessment
## 6. Milestone #41 Reconciliation
## 7. Milestone #42 Retirement
## 8. Prompt Directory Reconciliation
## 9. Content-Preservation Evidence
## 10. Final GitHub Planning State
## 11. Repository Mutation Check
## 12. Scope Protection
## 13. Findings
## 14. Final Decision
## 15. Next Authorized Action
```

Report actual milestone URLs/identifiers and actual paths where
available.

Never claim validation not performed.

------------------------------------------------------------------------

## 11. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 GITHUB PLANNING RECONCILIATION COMPLETE
RELEASE 1.0 GITHUB PLANNING RECONCILIATION COMPLETE WITH OBSERVATIONS
RELEASE 1.0 GITHUB PLANNING RECONCILIATION BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only for non-blocking observations that
do not compromise the authorized final state.

------------------------------------------------------------------------

## 12. Next Authorized Action After Success

After successful reconciliation, the next authorized action is to
**rerun the existing authoritative Release 1.0 GitHub-planning prompt**:

``` text
docs/roadmap/release-1.0/prompts/release-1.0-github-planning-codex-prompt.md
```

That subsequent execution may create/reconcile the Release 1.0 milestone
planning issues WP01--WP16 under its own authority.

This reconciliation task must stop before doing so.

Do not start WP01.

------------------------------------------------------------------------

## Execution Instruction

Read the authorities, perform the preflight, reconcile only milestone
#41, milestone #42, and the singular-to-plural Release 1.0 prompt
directory as explicitly authorized, validate the resulting state, return
the required report, and stop.
