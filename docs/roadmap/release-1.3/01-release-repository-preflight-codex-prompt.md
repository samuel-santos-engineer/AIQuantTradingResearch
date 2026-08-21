# Release 1.3 WP01 — Release & Repository Preflight — Codex Prompt

## Role

Act as the Release 1.3 WP01 preflight executor for `AIQuantTradingResearch`.
Use GPT-5.6 Luna. This is a bounded governance and verification package; do
not implement Release 1.3 behavior and do not begin WP02.

## Mandatory authority

Read completely before acting:

```text
docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md
docs/roadmap/release-1.3/RELEASE_1.3_EXECUTION_PLAN.md
docs/roadmap/release-1.3/RELEASE_1.3_FILE_MANIFEST.md
docs/roadmap/release-1.3/release-1.3-github-planning-codex-prompt.md
docs/roadmap/release-1.3/release-1.3-github-planning-codex-prompt-chat.md
docs/roadmap/release-1.3/01-release-repository-preflight-codex-prompt.md
docs/roadmap/release-1.3/01-release-repository-preflight-codex-prompt-chat.md
```

Also inspect the accepted Release 1.2 post-merge closure and current GitHub
planning state. The execution plan and file manifest control scope; accepted
GitHub state and repository truth control readiness. Stop on material conflict.

## Preconditions

Before any lifecycle mutation, independently verify:

- Release 1.2 is formally closed: PR #137 merged, milestone #53 closed, and
  issues #121–#136 closed/Done.
- The authoritative Release 1.3 planning milestone and exactly the planned
  WP01–WP14 issues exist, with dependencies and statuses matching the plan.
- WP01 is Open/Backlog and WP02 remains Open/Backlog.
- `main` equals `origin/main`, with no staged or unexplained working-tree
  changes; classify pre-existing out-of-band authorities explicitly.
- SDK/toolchain, solution membership, four production projects, four test
  projects, and the accepted production dependency graph are unchanged.
- SQLite schema version remains 2 and no Release 1.3 implementation exists.
- The accepted permanent baseline is 171 tests, including 13 architecture
  tests, unless repository truth supplies an accepted predecessor update.

Do not create or modify GitHub objects in this WP except the authorized WP01
issue lifecycle transition after all gates pass. Do not modify the definition,
execution plan, manifest, planning prompt, or any production/test/package file.

## Required validation

Run the canonical repository verification workflow:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1 -Configuration Release
```

Require restore, format, secret scanning, zero build errors, 171/171 tests,
13/13 architecture tests, zero provider/network use, schema v2, no generated
database residue, no stale Release 1.3 implementation artifacts, and passing
`git diff --check`. Inspect the final Git status and exact scope before any
issue update.

## Completion and report

If every gate passes, post concise evidence to the authoritative WP01 issue,
close it as Done, and leave WP02 untouched. Otherwise do not mutate GitHub and
report the precise blocker. Return a complete report covering authorities,
Git/GitHub state, repository/toolchain baseline, dependency/schema checks,
test and verification results, security/residue checks, scope classification,
issue lifecycle, unexpected changes, and next authorized work package.

Finish with exactly one terminal:

```text
RELEASE 1.3 WP01 COMPLETE
```

or

```text
RELEASE 1.3 WP01 BLOCKED
```

Do not begin WP02 — Research Pipeline Semantic Discovery.
