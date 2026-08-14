# Codex Execution Prompt — Release 0.9 Governance Integration

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Activity | Governance Integration |
| Execution Mode | Narrowly scoped Git/GitHub integration |
| Primary Agent | Codex |
| Prerequisite | Release 0.9 GitHub Planning = `COMPLETE` |
| Integration Scope | `docs/roadmap/release-0.9/**` governance authority only |
| Expected Outcome | Integrate the authoritative Release 0.9 governance artifacts and their intentional `-codex-prompt-chat.md` companion files through a controlled branch, exact staged delta, commit, push, and pull request without starting WP01 or modifying implementation |

---

# 1. Purpose

Integrate the Release 0.9 governance authority into the repository before WP01 begins.

The Release 0.9 GitHub planning activity is complete.

GitHub now contains:

```text
Milestone #40 — Phase 2 - Release 0.9: Research Platform
Issues #69–#82 — WP01–WP14
All 14 issues = Open / Todo
No WP15
No Release 0.9 implementation started
```

However, the Release 0.9 governance authority currently exists locally under:

```text
docs/roadmap/release-0.9/
```

and has not yet been integrated into `main`.

This activity exists solely to integrate that governance authority safely.

It is **not WP01**.

Do not begin WP01.

---

# 2. Authoritative Governance Files

Before taking any mutation, read completely:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/release-0.9-github-planning-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-github-planning-codex-prompt-chat.md
```

Also inspect every file currently present under:

```text
docs/roadmap/release-0.9/
```

Do not assume only four files exist.

Classify every file before staging.

---

# 3. Prompt / Prompt-Chat Repository Convention

The repository intentionally preserves two governance artifacts for each Codex execution prompt:

```text
<name>-codex-prompt.md
<name>-codex-prompt-chat.md
```

The authoritative `*-codex-prompt.md` file contains the complete execution contract.

The companion `*-codex-prompt-chat.md` file contains the concise bootstrap/chat prompt used to invoke that authoritative prompt in Codex.

Both files are intentionally retained in the repository for:

```text
clarity
repeatability
future reference
execution-history context
AI-assisted engineering traceability
```

Therefore:

- `*-codex-prompt-chat.md` files are **not accidental artifacts**;
- do not classify them as unrelated merely because they are chat/bootstrap prompts;
- when both the prompt and its chat companion belong to the current Release 0.9 governance activity, both are valid governance artifacts;
- do not delete or exclude a valid companion file solely because of the `-chat` suffix.

This convention applies to future Release 0.9 Codex work-package prompts as they are created.

---

# 4. Expected Current Release 0.9 Governance Set

The minimum expected governance set at this point is:

```text
docs/roadmap/release-0.9/
├── RELEASE_0.9_EXECUTION_PLAN.md
├── RELEASE_0.9_FILE_MANIFEST.md
└── prompts/
    ├── release-0.9-github-planning-codex-prompt.md
    └── release-0.9-github-planning-codex-prompt-chat.md
```

If additional files are present, inspect and classify them. Do not stage ambiguous or unrelated files automatically.

---

# 5. Scope Classification

Before staging anything, classify every changed/untracked file as exactly one:

```text
RELEASE-0.9 GOVERNANCE
UNRELATED USER WORK
GENERATED
AMBIGUOUS
PROTECTED
```

Valid `*-codex-prompt-chat.md` companions belong to `RELEASE-0.9 GOVERNANCE` when they match an approved Codex prompt in this release.

---

# 6. Authorized Repository Change Set

The only authorized repository mutation area is:

```text
docs/roadmap/release-0.9/**
```

and only for files that already exist locally as intentional Release 0.9 governance artifacts.

No implementation, test, build, CI, or architecture mutation is authorized.

---

# 7. Protected Areas

Do not modify:

```text
src/**
tests/**
eng/**
.github/**
AIQuantTradingResearch.slnx
Directory.Build.props
Directory.Packages.props
global.json
docker-compose.yml
README.md
docs/architecture/**
docs/design/**
docs/implementation/**
docs/roadmap/release-0.8/**
```

unless inspection only.

---

# 8. Initial State Verification

Before Git mutation, record:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
git remote -v
git rev-list --left-right --count main...origin/main
gh auth status
```

Also verify:

```text
milestone #40 exists
milestone #40 = Phase 2 - Release 0.9: Research Platform
issues #69–#82 exist
all 14 WP issues are Open/Todo
WP01 has not started
```

Do not expose token values.

---

# 9. Preconditions

Require:

```text
current base branch = main
main synchronized with origin/main
Release 0.8 remains COMPLETE / CLOSED
Release 0.9 milestone #40 exists
Release 0.9 WP01–WP14 issues exist
WP01 remains Todo
no Release 0.9 implementation has started
```

If implementation has already begun unexpectedly, stop and report the conflict.

---

# 10. Governance File Inspection

Inspect:

```text
git status --short
git diff -- .
```

For untracked files, inspect contents directly.

Build an explicit inclusion/exclusion matrix:

| Path | Classification | Include | Reason |
| --- | --- | --- | --- |

Do not stage by directory wildcard until this matrix is complete.

---

# 11. Required Inclusion

When present and matching their expected purpose, include:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/release-0.9-github-planning-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-github-planning-codex-prompt-chat.md
```

The prompt-chat file is explicitly authorized and intentional.

---

# 12. Branch Strategy

Use a dedicated forward-only branch from synchronized `main`.

Follow repository branch naming conventions when defined.

Do not commit directly to `main`.

Do not reuse a Release 0.8 branch.

Do not rewrite history.

Record base branch, base commit, integration branch, and creation result.

---

# 13. Pre-Staging Validation

Run:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Require:

```text
Exit Status = 0
Build errors = 0
Architecture.Tests = 7/7 baseline or current accepted count
solution project count = 8
production graph unchanged
cycles = 0
```

Do not modify technical files if validation fails.

---

# 14. Staging Contract

Stage only approved Release 0.9 governance artifacts.

Inspect:

```text
git status --short
git diff --cached --stat
git diff --cached
git diff --cached --check
```

Require:

```text
only docs/roadmap/release-0.9/** staged
no source/test/script/build/.github/Release 0.8 changes
no generated outputs
no secrets/credentials
```

Confirm the prompt/chat companion convention is represented correctly.

---

# 15. Commit Contract

Use repository commit conventions.

A suitable semantic intent is:

```text
docs: add Release 0.9 governance authority
```

if consistent with repository practice.

Record commit hash, subject, and files.

Do not amend or rewrite accepted history.

---

# 16. Push Contract

Verify remote identity, integration branch, commit, and upstream state.

Push only the integration branch.

Never force push.

Do not push the governance commit directly to `main`.

---

# 17. Pull Request Contract

Create a PR targeting `main` using repository conventions.

The PR must state:

```text
Purpose:
Integrate the authoritative Release 0.9 governance contract before WP01.

Included:
- Release 0.9 execution plan
- Release 0.9 file manifest
- GitHub planning authoritative Codex prompt
- GitHub planning Codex prompt-chat companion
- any other explicitly classified Release 0.9 governance artifacts, if applicable

Technical changes:
None

Implementation started:
No

WP01 status:
Todo / Not started

GitHub planning:
Milestone #40
Issues #69–#82
14 WPs
No WP15
```

Also explain:

```text
Every authoritative Codex prompt is paired with a
*-codex-prompt-chat.md companion file for clarity,
repeatability, and future reference.
```

Do not fabricate hosted checks or reviews.

---

# 18. Merge Boundary

Do not bypass required human review.

Do not self-approve, fabricate review state, bypass branch protection, or force merge.

If the PR is valid and awaits required human review/merge, return:

```text
COMPLETE WITH ACTIONS
```

Do not start WP01 while the governance authority remains only on the integration branch.

WP01 begins only after governance authority is integrated into `main`.

---

# 19. Post-Merge Validation

If the PR is actually merged during the authorized flow:

```text
git switch main
git pull origin main
git status --short
```

Verify `main = origin/main`, governance authority is present on `main`, and rerun `eng/verify.ps1` when practical.

---

# 20. No WP01 Execution

Do not:

```text
change issue #69 to In Progress
create WP01 implementation
execute WP01 research
modify source/test files
```

WP01 remains the next authorized action.

---

# 21. No Additional GitHub Planning Mutation

The milestone and issues already exist.

Do not unnecessarily mutate milestone #40, issues #69–#82, labels, areas, priorities, releases, or statuses unless a material discrepancy with Release 0.9 authority is discovered.

---

# 22. Failure Handling

Return `BLOCKED` when safe integration cannot proceed because of synchronization, technical-baseline, implementation-start, artifact-scope, staged-delta, or repository-identity conflicts.

Return `COMPLETE WITH ACTIONS` when branch/commit/PR are correct but human review/merge remains.

Return `COMPLETE` only after all applicable integration requirements are satisfied.

---

# 23. Acceptance Criteria

- [ ] Execution plan read completely.
- [ ] File manifest read completely.
- [ ] GitHub planning prompt read completely.
- [ ] Prompt-chat companion recognized as intentional governance.
- [ ] Every `docs/roadmap/release-0.9/**` file classified.
- [ ] Prompt/prompt-chat convention preserved.
- [ ] Initial Git/GitHub state recorded.
- [ ] Authentication verified without exposing credentials.
- [ ] Milestone #40 verified.
- [ ] Issues #69–#82 verified.
- [ ] All WPs remain Open/Todo before integration.
- [ ] WP01 remains not started.
- [ ] `main` synchronized before branch creation.
- [ ] Dedicated forward-only branch used.
- [ ] No Release 0.8 history modified.
- [ ] No technical files modified.
- [ ] Canonical verification passed before staging.
- [ ] Solution remains 8 projects.
- [ ] Production graph remains accepted.
- [ ] Cycles remain zero.
- [ ] Staged delta contains only approved Release 0.9 governance.
- [ ] Valid prompt-chat companion included.
- [ ] No generated/unrelated/ambiguous files staged.
- [ ] No secrets/credentials staged.
- [ ] `git diff --cached --check` passes.
- [ ] Commit follows repository convention.
- [ ] Push performed without force.
- [ ] PR targets `main`.
- [ ] PR states zero technical changes.
- [ ] No fabricated review/check state.
- [ ] Required human review not bypassed.
- [ ] If merged, `main` synchronized and authority present.
- [ ] If merged, technical baseline remains passing.
- [ ] WP01 remains Todo/not started.
- [ ] Final state recorded.

---

# 24. Expected Output Contract

Return one complete **Release 0.9 Governance Integration Execution Report** with:

1. Executive Summary
2. Execution Context
3. Authoritative Sources Reviewed
4. GitHub Planning Baseline
5. Governance Artifact Classification
6. Prompt Companion Convention Validation
7. Integration Branch
8. Pre-Staging Technical Validation
9. Staged Delta
10. Commit
11. Push
12. Pull Request
13. Merge / Review Gate
14. Post-Merge Synchronization
15. Post-Merge Technical Validation
16. WP01 Protection
17. Final Git State
18. Final GitHub State
19. Scope Compliance
20. Findings
21. Acceptance Criteria Matrix
22. Final Decision
23. Next Action

Final decision must be exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

If fully integrated, identify `WP01 — Repository & Release Preflight` as next but do not begin it.

---

# 25. Final Instruction

Integrate the existing Release 0.9 governance authority through a narrowly scoped governed Git/GitHub flow.

Treat:

```text
*-codex-prompt.md
*-codex-prompt-chat.md
```

as intentional companion governance artifacts when they belong to the same approved workflow.

Do not classify a valid `-codex-prompt-chat.md` file as accidental.

Read and classify every file under `docs/roadmap/release-0.9/` before staging.

Integrate only approved Release 0.9 governance artifacts.

Do not modify implementation, Release 0.8 history, or CI.

Do not start WP01.

Validate the technical baseline.

Create a dedicated forward-only branch.

Stage and inspect the exact governance delta.

Commit using repository conventions.

Push without force.

Create a PR targeting `main`.

Respect required human review.

If human merge remains, return `COMPLETE WITH ACTIONS`.

If merged, synchronize `main`, revalidate the baseline, and return `COMPLETE`.

Identify `WP01 — Repository & Release Preflight` as the next authorized action.

Do not execute it.

---

# Conclusion

Release 0.9 governance must be integrated before implementation begins so that Codex, humans, and GitHub operate from the same committed authority.

The repository intentionally preserves both the full execution prompt and its concise Codex chat/bootstrap companion:

```text
authoritative prompt
        +
prompt-chat companion
        ↓
repeatable AI-assisted execution history
```

The integration flow is:

```text
Approved Release 0.9 Authority
          ↓
Classify Governance Artifacts
          ↓
Preserve Prompt + Prompt-Chat Companions
          ↓
Validate Technical Baseline
          ↓
Controlled Branch
          ↓
Exact Governance Commit
          ↓
Pull Request
          ↓
Governed Merge
          ↓
Authority Present on main
          ↓
WP01 Authorized to Begin
```

> **Implementation should begin only after the release authority—and the prompts used to execute that authority—are themselves safely governed and versioned.**
