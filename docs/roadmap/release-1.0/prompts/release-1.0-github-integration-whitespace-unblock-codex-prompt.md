# Release 1.0 Git/GitHub Integration Whitespace Unblock — Authoritative Codex Prompt

## 0. Prompt Identity

**Release:** 1.0 — Market Data Foundation
**Lifecycle step:** Git/GitHub Integration whitespace unblock
**Blocked integration issue:** `GI10-01`
**Blocked integration terminal:** `RELEASE 1.0 GITHUB INTEGRATION BLOCKED`
**Purpose:** Remove only the exact staged Markdown whitespace violations preventing `git diff --cached --check` from passing
**Success terminal:** `RELEASE 1.0 GITHUB INTEGRATION WHITESPACE UNBLOCK COMPLETE`
**Failure terminal:** `RELEASE 1.0 GITHUB INTEGRATION WHITESPACE UNBLOCK BLOCKED`

This prompt is a narrow human authorization. It does not authorize semantic edits, product/test changes, candidate redesign, additional files, commit, push, PR creation, merge, release closure, planning mutation, or Release 1.1 work.

---

## 1. Mission

Resolve only `GI10-01` from the blocked Release 1.0 Git/GitHub integration.

The accepted integration state is:

```text
branch: release/1.0-market-data-foundation
HEAD: 138582919d220427d2e87434533b82bf8801f8db
WP16 accepted candidate: 78/78
integration governance: 2/2
integration candidate: 80/80
staged files: 80
unstaged files: 0
untracked files: 0
unexpected files: 0
commit: none
remote integration branch: absent
PR: absent
```

The only blocker is:

```text
git diff --cached --check
→ FAIL
→ 31 whitespace violations
→ 7 previously untracked Markdown files
```

This unblock must remove only those exact whitespace defects while preserving substantive content byte-for-byte except for the authorized whitespace normalization.

---

## 2. Governing Authorities

Read completely before mutation:

1. `docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md`
3. `docs/roadmap/release-1.0/prompts/16-full-validation-integration-acceptance-codex-prompt.md`
4. `docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt.md`
5. the blocked Release 1.0 Git/GitHub Integration Report from the current context;
6. this whitespace-unblock prompt and its five-line companion.

Authority precedence:

1. this explicit whitespace-only human authorization;
2. Release 1.0 integration prompt;
3. WP16 accepted candidate authority;
4. Release 1.0 execution plan/file manifest;
5. earlier Release 1.0 prompt authorities.

Do not reinterpret this prompt as broad permission to repair Markdown.

---

## 3. Authorized Files

This unblock authorizes whitespace-only edits to exactly these seven Markdown files:

```text
docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
docs/roadmap/release-1.0/prompts/09-market-data-validation-failure-mapping-codex-prompt.md
docs/roadmap/release-1.0/prompts/14-architecture-evolution-codex-prompt.md
docs/roadmap/release-1.0/prompts/15-documentation-alignment-codex-prompt.md
docs/roadmap/release-1.0/prompts/16-full-validation-integration-acceptance-codex-prompt.md
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt.md
```

Expected violations from the blocked report:

| File | Violations |
|---|---:|
| `MARKET_DATA_PROVIDER_ASSESSMENT.md` | 1 |
| `MARKET_DATA_PROVIDER_DECISION.md` | 1 |
| WP09 prompt | 4 |
| WP14 prompt | 11 |
| WP15 prompt | 2 |
| WP16 prompt | 7 |
| Release 1.0 integration prompt | 5 |
| **Total** | **31** |

Do not edit any eighth file.

If the current cached-diff report names a different file set or violation count, stop and report repository drift before changing anything.

---

## 4. Authorized Whitespace Changes

Only these transformations are authorized:

```text
remove trailing spaces/tabs reported by git diff --cached --check
remove terminal blank-line / EOF whitespace violation reported by git diff --cached --check
```

No other normalization is authorized.

Allowed examples:

```text
"some text···"     → "some text"
"table cell |··"   → "table cell |"
extra blank EOF    → canonical single terminal newline
```

Do not alter internal spacing that Git did not report.

Do not reflow tables.

Do not change indentation unless the exact indentation contains trailing whitespace reported by Git.

Do not reorder lines.

Do not wrap or unwrap prose.

Do not alter Markdown structure.

---

## 5. Explicitly Prohibited Semantic Changes

Do not change:

```text
words
punctuation
headings
table columns
table cell text
code blocks
URLs
paths
filenames
release numbers
issue numbers
counts
hashes
commands
terminal strings
authority precedence
scope statements
acceptance criteria
next-action wording
```

Do not “fix” typos.

Do not revise wording for clarity.

Do not update stale content.

Do not modify the 5-line integration prompt-chat companion.

Do not modify the WP16 accepted 78-file candidate beyond the exact whitespace-only corrections in these seven files.

---

## 6. Starting State Gate

Before mutation prove:

```text
branch = release/1.0-market-data-foundation
HEAD = 138582919d220427d2e87434533b82bf8801f8db
commit created = NO
remote integration branch = absent
PR = absent
staged files = 80
unstaged files = 0
untracked files = 0
unexpected files = 0
```

Run and record:

```powershell
git status --short
git diff --cached --name-only
git diff --cached --check
```

Required reproduction:

```text
cached diff check = FAIL
violations = 31
affected files = exactly the seven authorized files
```

If this exact state does not reproduce, stop before mutation.

---

## 7. Preservation Snapshot

Before editing the seven files, capture a preservation snapshot sufficient to prove substantive equivalence.

For each file record:

```text
byte length
SHA-256
line count
```

Also create a logical comparison baseline where each line is normalized only by:

```text
removing trailing spaces/tabs
```

and the file ending is normalized only to:

```text
exactly one final newline
```

This normalized baseline is the semantic-preservation reference.

Do not store permanent snapshot artifacts in the repository.

Temporary local comparison files are permitted only outside the repository or in an ignored temporary location and must be deleted before completion.

---

## 8. Whitespace Normalization Procedure

For each authorized file:

1. inspect the exact `git diff --cached --check` findings;
2. edit only the reported trailing whitespace / EOF blank-line defect;
3. preserve UTF-8 encoding;
4. preserve all non-whitespace characters;
5. preserve line ordering;
6. preserve all Markdown content;
7. ensure exactly one terminal newline;
8. do not normalize unrelated whitespace.

Use an encoding-safe mechanism.

If an edit attempt alters non-whitespace bytes, immediately restore that file from the staged original and retry safely.

Do not use a formatter that may reflow Markdown.

---

## 9. Semantic Equivalence Proof

After normalization, prove for each of the seven files:

```text
normalized-before line count = normalized-after line count
normalized-before content = normalized-after content
non-whitespace textual differences = 0
```

Where terminal blank-line removal changes physical line count, compare after applying the authorized EOF normalization rule.

The proof must demonstrate that the only differences are:

```text
trailing spaces/tabs removed
terminal blank-line whitespace removed
```

No substantive character may differ.

Report:

```text
file
pre-normalization violations
post-normalization violations
semantic/content differences
```

Required:

```text
semantic/content differences = 0 for all seven files
```

---

## 10. Re-Stage Corrected Files

The candidate is already staged.

After correcting each file, update the index only for those seven paths so the staged version contains the authorized whitespace-only correction.

Do not unstage the other 73 files.

Do not use reset.

Do not discard accepted candidate content.

After re-staging, require:

```text
staged files = 80
unstaged files = 0
untracked files = 0
unexpected files = 0
```

Reconcile the staged candidate remains:

```text
78 WP16 accepted files
+ 2 integration governance files
= 80
```

No path additions/removals are authorized.

---

## 11. Mandatory Diff Checks

Run:

```powershell
git diff --check
git diff --cached --check
```

Required:

```text
git diff --check = PASS
git diff --cached --check = PASS
```

Also prove:

```text
reported whitespace violations after normalization = 0
```

Do not weaken Git whitespace rules.

Do not add `.gitattributes` or `.editorconfig` exceptions.

Do not change Git configuration.

---

## 12. Staged Semantic Drift Review

After the cached check passes, inspect staged diffs for the seven files.

Required:

```text
only authorized whitespace-only differences
semantic changes = 0
file additions/removals = 0
candidate count = 80
```

Also confirm all non-authorized staged files are byte-identical to their pre-unblock staged content.

Where practical, compare staged blob hashes for the other 73 files before/after the unblock.

Do not alter the integration prompt-chat companion.

---

## 13. Candidate Technical Preservation

This unblock does not require repeating the entire 105-test suite unless a repository command or edit unexpectedly touches executable content.

However, because the integration candidate must remain technically accepted, run at minimum:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Required:

```text
PASS
105/105 permanent tests
13/13 Architecture.Tests
build errors = 0
```

If canonical verification fails, stop.

Do not repair code under this prompt.

---

## 14. Git / GitHub Protection

Do not:

```text
commit
push
create remote branch
create PR
merge
enable auto-merge
close issue #101
close milestone #41
mutate issues/project/labels
create tag
create GitHub Release
begin Release 1.1
```

The unblock ends with a corrected staged index ready for the original integration prompt to resume.

---

## 15. Working-Tree Integrity

Final expected state:

```text
branch = release/1.0-market-data-foundation
HEAD = accepted base commit
staged files = 80
unstaged files = 0
untracked files = 0
unexpected files = 0
commit = none
remote integration branch = absent
PR = absent
```

No temporary comparison artifact may remain.

---

## 16. Blocker Policy

Return `RELEASE 1.0 GITHUB INTEGRATION WHITESPACE UNBLOCK BLOCKED` if:

- the starting state does not match the blocked integration report;
- cached-diff findings are not exactly 31 across the seven authorized files;
- normalization requires editing an eighth file;
- any non-whitespace semantic difference appears;
- UTF-8/content corruption occurs and cannot be safely restored;
- staged candidate count changes from 80;
- an accepted candidate file disappears;
- an unauthorized path appears;
- either diff check still fails;
- canonical verification fails;
- working tree cannot be returned to staged-80 / unstaged-0 / untracked-0 without discarding accepted content.

Do not improvise a broader fix.

---

## 17. Required Execution Report

Return:

```text
# Release 1.0 Git/GitHub Integration Whitespace Unblock Execution Report
```

with these sections:

1. Executive Summary
2. Authorities Reviewed
3. Initial Repository State
4. GI10-01 Reproduction
5. Authorized File Set
6. Preservation Snapshot
7. Whitespace Normalization
8. Per-File Equivalence Evidence
9. Re-Staging Evidence
10. Candidate Count Reconciliation
11. `git diff --check`
12. `git diff --cached --check`
13. Staged Semantic Drift Review
14. Canonical Verification
15. Git / GitHub Protection
16. Working-Tree Classification
17. Scope Protection
18. Findings / Observations
19. Acceptance Matrix
20. Final Repository State
21. Final Decision
22. Next Authorized Action

Include the exact pre/post violation count for every authorized file.

---

## 18. Acceptance Matrix

The report must explicitly assess:

| Requirement | Result |
|---|---|
| GI10-01 reproduced | PASS/FAIL |
| Authorized files | 7/7 |
| Initial violations | 31 |
| Post-normalization violations | 0 |
| Semantic differences | 0 |
| Non-authorized files changed | 0 |
| Candidate paths | 80/80 |
| WP16 candidate preserved | 78/78 |
| Integration governance preserved | 2/2 |
| `git diff --check` | PASS/FAIL |
| `git diff --cached --check` | PASS/FAIL |
| Canonical verification | PASS/FAIL |
| Permanent tests | x/x |
| Architecture.Tests | x/x |
| Build errors | count |
| Staged files | 80 |
| Unstaged files | 0 |
| Untracked files | 0 |
| Unexpected files | 0 |
| Commit created | NO |
| Push performed | NO |
| PR created | NO |
| Release 1.1 started | NO |

---

## 19. Success Criteria

This unblock succeeds only when:

- the exact 31 violations are removed;
- only seven authorized files are edited;
- semantic/content differences = 0;
- candidate remains exactly 80/80;
- 78 WP16 files remain present;
- both integration governance artifacts remain present;
- both diff checks pass;
- canonical verification passes;
- all permanent tests remain passing;
- staged files = 80;
- unstaged files = 0;
- untracked files = 0;
- unexpected files = 0;
- no commit/push/PR/merge/planning mutation occurs;
- Release 1.1 remains untouched.

The exact terminal line must be:

```text
RELEASE 1.0 GITHUB INTEGRATION WHITESPACE UNBLOCK COMPLETE
```

---

## 20. Failure Criteria

If any mandatory condition fails, use exactly:

```text
RELEASE 1.0 GITHUB INTEGRATION WHITESPACE UNBLOCK BLOCKED
```

State the minimum additional human authority required.

---

## 21. Next Authorized Action After Success

After successful whitespace unblock, resume the existing authoritative integration prompt:

```text
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt.md
```

Do not create a replacement integration prompt.

Resume from the staged semantic-drift review / commit gate.

The resumed integration must:

1. recognize GI10-01 as resolved;
2. verify the exact staged 80-file candidate;
3. run `git diff --cached --check` again;
4. create the single authorized commit:
   `feat: implement Release 1.0 market data foundation`
5. perform post-commit validation;
6. push without force;
7. create and inspect the PR;
8. stop at explicit human merge authorization.

Do not merge.

Do not close Release 1.0.

Do not begin Release 1.1.

---

## Execution Instruction

Read the blocked Release 1.0 integration report and all governing authorities; prove the current branch/index exactly matches the blocked 80-file staged state; reproduce GI10-01 as exactly 31 cached whitespace findings across the seven authorized Markdown files; snapshot each file for semantic-preservation proof; remove only Git-reported trailing whitespace and terminal blank-line/EOF whitespace using UTF-8-safe editing; re-stage only those seven files; prove normalized-before and normalized-after substantive content is identical; preserve all other 73 staged paths unchanged; require the candidate to remain exactly 80/80; run both diff checks and canonical verification; leave the branch with 80 staged files, zero unstaged/untracked/unexpected files, no commit, no remote branch, and no PR; return the complete whitespace-unblock execution report; and stop so the original integration authority can resume separately.
