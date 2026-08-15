# Release 1.0 Git/GitHub Integration Whitespace-Unblock Governance Reconciliation — Authoritative Codex Prompt

## 0. Prompt Identity

**Release:** 1.0 — Market Data Foundation
**Lifecycle step:** Git/GitHub integration governance reconciliation for the whitespace unblock
**Blocked condition:** `WU10-01`
**Purpose:** Reconcile the two whitespace-unblock governance artifacts into the governed integration candidate and supersede the obsolete 80-file count with an authoritative 82-file count
**Success terminal:** `RELEASE 1.0 GITHUB INTEGRATION WHITESPACE UNBLOCK GOVERNANCE RECONCILIATION COMPLETE`
**Failure terminal:** `RELEASE 1.0 GITHUB INTEGRATION WHITESPACE UNBLOCK GOVERNANCE RECONCILIATION BLOCKED`

This prompt is a narrow human governance authorization.

It does **not** authorize product, test, architecture, package, project, documentation-semantic, Git-history, push, PR, merge, planning-closure, tag, GitHub Release, or Release 1.1 work.

---

## 1. Background

The Release 1.0 integration process is currently blocked by a governance-recursion issue.

The accepted sequence is:

```text
WP16 accepted candidate                      78 files
Git/GitHub integration governance             2 files
Initial governed integration candidate        80 files
Whitespace-unblock governance                 2 files
                                              ───────
New governed integration candidate            82 files
```

The prior whitespace-unblock execution correctly stopped because:

```text
staged files = 80
untracked files = 2
```

while its starting-state gate still required:

```text
staged files = 80
untracked files = 0
integration candidate = 80
```

The two untracked files are the whitespace-unblock governance artifacts themselves.

This prompt resolves that conflict explicitly.

---

## 2. Governing Authorities

Read completely before mutation:

1. `docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md`
3. `docs/roadmap/release-1.0/prompts/16-full-validation-integration-acceptance-codex-prompt.md`
4. `docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt.md`
5. `docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-codex-prompt.md`
6. the blocked Release 1.0 Git/GitHub Integration Report;
7. the blocked Release 1.0 Git/GitHub Integration Whitespace Unblock Execution Report;
8. this reconciliation prompt and its five-line companion.

Authority precedence:

1. this later explicit governance reconciliation authority;
2. whitespace-unblock prompt;
3. original integration prompt;
4. WP16 acceptance authority;
5. Release 1.0 execution plan/file manifest;
6. earlier Release 1.0 authorities.

Where file-count/state gates conflict, this prompt supersedes them only as explicitly described below.

---

## 3. Narrow Human Authorization

This prompt explicitly authorizes the following two governance files to become part of the governed Release 1.0 integration candidate:

```text
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-codex-prompt.md
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-codex-prompt-chat.md
```

They must be included exactly once and preserved unchanged.

This authorization changes the integration-candidate governance count from:

```text
80
```

to:

```text
82
```

No other new file is authorized.

---

## 4. Superseded Count Gates

The following prior integration/whitespace-unblock gates are superseded:

### Old

```text
WP16 accepted candidate = 78
integration governance = 2
integration candidate = 80
staged = 80
untracked = 0
```

### New authoritative model

```text
WP16 accepted candidate = 78
original integration governance = 2
whitespace-unblock governance = 2
governed integration candidate = 82
```

Starting state before reconciliation is expected to be:

```text
staged = 80
untracked governance = 2
unstaged tracked files = 0
unexpected = 0
```

Required state after reconciliation:

```text
staged = 82
untracked = 0
unstaged = 0
unexpected = 0
```

The underlying WP16-accepted 78-file candidate remains immutable.

---

## 5. Starting-State Gate

Before any mutation, prove:

```text
branch = release/1.0-market-data-foundation
HEAD = 138582919d220427d2e87434533b82bf8801f8db
commit created = NO
remote integration branch = absent
PR = absent

staged files = 80
unstaged files = 0
untracked files = 2
```

The two untracked files must be exactly:

```text
release-1.0-github-integration-whitespace-unblock-codex-prompt.md
release-1.0-github-integration-whitespace-unblock-codex-prompt-chat.md
```

No third untracked file is allowed.

No unexpected staged file is allowed.

If this exact state does not reproduce, stop.

---

## 6. Governance Artifact Preservation

Before staging the two untracked governance files, record for each:

```text
path
byte length
SHA-256
line count
```

The chat companion must remain exactly five physical lines.

Do not edit either file.

Do not normalize their whitespace.

Do not reformat them.

Do not rewrite content.

These two files are governance-history artifacts and must enter the integration candidate byte-preserved.

---

## 7. Stage the Two Governance Files

Stage exactly:

```text
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-codex-prompt.md
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-codex-prompt-chat.md
```

Do not stage any other new file because no other file may exist.

After staging, prove:

```text
staged files = 82
unstaged = 0
untracked = 0
unexpected = 0
```

Reconcile:

```text
WP16 accepted candidate = 78/78
original integration governance = 2/2
whitespace-unblock governance = 2/2
total governed integration candidate = 82/82
```

---

## 8. Governance Hash Revalidation

After staging, recompute the two governance hashes and prove they match the pre-staging hashes exactly.

Required:

```text
byte preservation = PASS
content preservation = PASS
line count preservation = PASS
chat companion line count = 5
```

If either artifact changed during staging, stop.

---

## 9. Candidate Integrity

Prove that this reconciliation changed only candidate governance membership, not candidate semantics.

Required:

```text
WP16 candidate file content changed = NO
original integration governance changed = NO
whitespace-unblock governance changed = NO
product changed = NO
tests changed = NO
architecture changed = NO
documentation semantics changed = NO
packages/projects/build changed = NO
```

Do not inspect this step as permission to correct whitespace yet.

The actual seven-file whitespace normalization remains governed by the existing whitespace-unblock authority and occurs only after this governance reconciliation succeeds.

---

## 10. Cached Whitespace State

After staging the two governance artifacts, rerun:

```powershell
git diff --cached --check
```

Expected:

```text
FAIL
```

because the original 31 authorized whitespace findings have not yet been corrected.

The failure may include additional whitespace findings inside the two newly staged governance files only if those findings already exist in the byte-preserved governance artifacts.

### Important

This reconciliation prompt does **not** itself authorize correcting any additional whitespace in the two newly staged governance artifacts.

If staging these two files introduces new `git diff --cached --check` findings beyond the previously known 31, record them exactly and stop this reconciliation as `BLOCKED` because the subsequent whitespace authority would need expansion.

If the cached findings remain exactly the original 31 across the same seven files, reconciliation may complete successfully.

---

## 11. Required Whitespace-Finding Reconciliation

Expected post-staging cached-whitespace state:

```text
violations = 31
affected files = the same 7 previously authorized Markdown files
new violations from the two newly staged governance artifacts = 0
```

If this expectation holds, the next execution may resume the existing whitespace-unblock prompt using the superseded 82-file count.

If violations exceed 31 or affect an eighth/ninth file, stop and report the exact new scope.

Do not infer authority to fix new findings.

---

## 12. No Whitespace Correction in This Reconciliation Step

Do not edit the seven whitespace-affected files during this governance reconciliation.

Do not remove any of the 31 violations yet.

Do not run a Markdown formatter.

Do not alter `.gitattributes`, `.editorconfig`, or Git config.

This step changes only governance membership.

---

## 13. No Commit / Push / PR

Do not:

```text
commit
push
create remote integration branch
create PR
merge
enable auto-merge
```

This step ends before the whitespace correction and before the original integration commit gate.

---

## 14. GitHub / Planning Protection

Do not mutate:

```text
issue #101
milestone #41
issues #86–#101
Project items/fields
labels
tags
GitHub Releases
Release 1.1 planning
```

No GitHub mutation is required.

---

## 15. Working-Tree Final State

Successful reconciliation must end with:

```text
branch = release/1.0-market-data-foundation
HEAD = 138582919d220427d2e87434533b82bf8801f8db

staged files = 82
unstaged files = 0
untracked files = 0
unexpected files = 0

commit = none
remote integration branch = absent
PR = absent
```

Cached whitespace is expected to remain blocked by exactly the known 31 findings across the same seven files.

---

## 16. Blocker Policy

Return `RELEASE 1.0 GITHUB INTEGRATION WHITESPACE UNBLOCK GOVERNANCE RECONCILIATION BLOCKED` if:

- starting state differs materially;
- either governance artifact is absent;
- an additional untracked file exists;
- either governance artifact changed unexpectedly;
- the chat companion is not five lines;
- staged count after reconciliation is not exactly 82;
- any accepted 80-file candidate path disappears;
- any unexpected path enters staging;
- staging the two governance files creates new whitespace findings beyond the known 31;
- cached whitespace affects any new file outside the seven previously authorized targets;
- a commit/push/PR already exists unexpectedly;
- Release 1.1 artifacts appear.

Do not improvise a broader reconciliation.

---

## 17. Required Execution Report

Return:

```text
# Release 1.0 Git/GitHub Integration Whitespace-Unblock Governance Reconciliation Execution Report
```

with:

1. Executive Summary
2. Authorities Reviewed
3. Initial Repository State
4. WU10-01 Reproduction
5. Governance Artifact Inventory
6. Pre-Staging Hash / Line Evidence
7. Staging Action
8. Post-Staging Count Reconciliation
9. Post-Staging Hash Preservation
10. Candidate Integrity
11. Cached Whitespace Reconciliation
12. Working-Tree Classification
13. Git / GitHub Protection
14. Scope Protection
15. Findings / Observations
16. Acceptance Matrix
17. Final Repository State
18. Final Decision
19. Next Authorized Action

---

## 18. Acceptance Matrix

Explicitly report:

| Requirement | Result |
|---|---|
| Starting staged candidate | 80/80 |
| Untracked governance artifacts | 2/2 |
| Governance artifact hashes preserved | PASS/FAIL |
| Chat companion line count | 5 |
| WP16 candidate preserved | 78/78 |
| Original integration governance preserved | 2/2 |
| Whitespace-unblock governance integrated | 2/2 |
| Governed integration candidate | 82/82 |
| Unexpected files | 0 |
| Unstaged files | 0 |
| Untracked files | 0 |
| Cached whitespace violations | expected 31 |
| Cached whitespace affected files | expected 7 |
| New whitespace findings from governance artifacts | 0 |
| Commit created | NO |
| Push performed | NO |
| PR created | NO |
| Planning mutated | NO |
| Release 1.1 started | NO |

---

## 19. Success Criteria

This reconciliation succeeds only if:

- the exact starting 80-staged + 2-untracked state is reproduced;
- the two untracked files are exactly the whitespace-unblock governance pair;
- both files are byte-preserved;
- both files are staged;
- the candidate becomes exactly 82/82;
- untracked becomes 0;
- unstaged remains 0;
- unexpected remains 0;
- WP16 78/78 remains preserved;
- original integration governance 2/2 remains preserved;
- no semantic candidate content changes;
- cached whitespace remains exactly the original 31 findings across exactly the same seven files;
- no new whitespace finding appears in the newly governed artifacts;
- no commit/push/PR/planning/Release 1.1 mutation occurs.

The exact terminal line must be:

```text
RELEASE 1.0 GITHUB INTEGRATION WHITESPACE UNBLOCK GOVERNANCE RECONCILIATION COMPLETE
```

---

## 20. Failure Criteria

If any mandatory condition fails, end with exactly:

```text
RELEASE 1.0 GITHUB INTEGRATION WHITESPACE UNBLOCK GOVERNANCE RECONCILIATION BLOCKED
```

State the minimum additional authority required.

---

## 21. Superseding Authority for Subsequent Whitespace Unblock

On successful reconciliation, the existing whitespace-unblock prompt remains authoritative except that the following state/count gates are superseded:

### Superseded

```text
integration candidate = 80
staged = 80
```

### Replacement

```text
integration candidate = 82
staged = 82
```

The existing seven authorized whitespace target files remain unchanged.

The existing expected 31 violations remain unchanged.

The two newly integrated governance artifacts are **not** authorized for whitespace editing under the existing seven-file whitelist.

---

## 22. Next Authorized Action After Success

After successful reconciliation, rerun:

```text
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-codex-prompt.md
```

under this later reconciliation authority.

The resumed whitespace-unblock execution must now expect:

```text
staged = 82
unstaged = 0
untracked = 0
candidate = 82/82
violations = 31
affected whitespace files = the same seven
```

It must then perform the already-authorized whitespace-only corrections, prove zero semantic differences, rerun both diff checks and canonical verification, and stop before commit.

After that whitespace unblock completes, rerun the original integration prompt:

```text
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt.md
```

with this reconciliation authority superseding its old 80-file count.

The final integration commit must then contain:

```text
82 files
```

not 80.

Do not merge.

Do not close Release 1.0.

Do not begin Release 1.1.

---

## Execution Instruction

Read the blocked integration and blocked whitespace-unblock reports plus all governing authorities; prove the current repository is on `release/1.0-market-data-foundation` with the exact accepted 80-file staged candidate and exactly two untracked whitespace-unblock governance artifacts; hash and line-count those two artifacts; stage exactly those two files unchanged; prove their hashes and five-line companion contract are preserved; reconcile the governed integration candidate from 80 to exactly 82 files with zero unstaged/untracked/unexpected paths; rerun `git diff --cached --check` only to confirm that the cached whitespace scope remains exactly the original 31 findings across exactly the same seven files and that the two newly staged governance artifacts introduce no new findings; do not perform whitespace correction, commit, push, PR, merge, planning mutation, closure, or Release 1.1 work; return the complete governance-reconciliation report; and stop so the existing whitespace-unblock prompt can resume under this later 82-file authority.
