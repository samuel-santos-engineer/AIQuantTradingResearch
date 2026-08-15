# Release 1.0 Git/GitHub Integration Governance Finalization — Authoritative Codex Prompt

## 0. Prompt Identity

**Release:** 1.0 — Market Data Foundation
**Lifecycle step:** Final governance reconciliation before resuming the Git/GitHub integration whitespace unblock
**Purpose:** Terminate governance-file recursion by explicitly authorizing the complete Release 1.0 integration-lifecycle governance chain, including this prompt pair itself, and derive the final governed integration-candidate count from repository truth
**Success terminal:** `RELEASE 1.0 GITHUB INTEGRATION GOVERNANCE FINALIZATION COMPLETE`
**Failure terminal:** `RELEASE 1.0 GITHUB INTEGRATION GOVERNANCE FINALIZATION BLOCKED`

This prompt is a final governance authority for the integration-lifecycle artifact set.

It does not authorize product changes, test changes, architecture changes, documentation-semantic changes, whitespace correction, package/project/build changes, Git-history mutation, push, PR creation, merge, release closure, planning mutation, or Release 1.1 work.

---

## 1. Background

The Release 1.0 integration process encountered a governance recursion:

1. WP16 accepted an exact 78-file technical candidate.
2. The Git/GitHub integration prompt and companion added two governance artifacts.
3. The whitespace-unblock prompt and companion added two more governance artifacts.
4. The whitespace-unblock governance-reconciliation prompt and companion added two more governance artifacts.
5. Each fixed-count reconciliation prompt excluded its own prompt pair from the count, creating another untracked pair and another blocker.

This prompt terminates that recursion.

The governing principle is now:

```text
Final governed integration candidate
=
immutable 78-file WP16 accepted candidate
+
every explicitly recognized Release 1.0 integration-lifecycle governance artifact
under docs/roadmap/release-1.0/prompts/
including this governance-finalization prompt pair itself
```

The final count must be **derived after enumeration**, not hardcoded before repository inspection.

---

## 2. Governing Authorities

Read completely before mutation:

1. `docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md`
3. `docs/roadmap/release-1.0/prompts/16-full-validation-integration-acceptance-codex-prompt.md`
4. `docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt.md`
5. `docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-codex-prompt.md`
6. `docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-governance-reconciliation-codex-prompt.md`
7. the blocked Release 1.0 Git/GitHub Integration Report;
8. the blocked whitespace-unblock report;
9. the blocked whitespace-unblock governance-reconciliation report;
10. this governance-finalization prompt and its 5-line companion.

Authority precedence:

1. this final governance-finalization authority;
2. earlier whitespace-unblock reconciliation authority;
3. whitespace-unblock authority;
4. original integration authority;
5. WP16 acceptance authority;
6. Release 1.0 execution plan/file manifest;
7. earlier Release 1.0 authorities.

Where candidate-count/state gates conflict, this prompt supersedes them only for integration-governance reconciliation and derived candidate count.

---

## 3. Immutable Technical Baseline

The underlying WP16-accepted technical candidate remains immutable:

```text
WP16 accepted candidate = 78 files
```

This 78-file set must remain:

```text
present = 78/78
missing = 0
extra technical files = 0
semantic changes caused by governance finalization = 0
```

This prompt does not authorize modifying any of those 78 files.

---

## 4. Explicitly Recognized Integration-Lifecycle Governance Artifacts

The following governance artifacts are recognized as part of the Release 1.0 integration chain and are authorized to join the final governed integration candidate exactly once if present at the governed paths:

```text
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt.md
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt-chat.md

docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-codex-prompt.md
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-codex-prompt-chat.md

docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-governance-reconciliation-codex-prompt.md
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-governance-reconciliation-codex-prompt-chat.md

docs/roadmap/release-1.0/prompts/release-1.0-github-integration-governance-finalization-codex-prompt.md
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-governance-finalization-codex-prompt-chat.md
```

This prompt pair is explicitly self-authorized for inclusion.

No additional integration-lifecycle governance filename is implicitly authorized.

If another similarly named governance artifact exists, stop and report it rather than including it automatically.

---

## 5. Derived Final Candidate Count

The count must be derived from repository truth.

Expected model, if all eight recognized governance artifacts are present exactly once:

```text
WP16 accepted candidate                         78
recognized integration-lifecycle governance      8
                                                ──
final governed integration candidate             86
```

However, do not use `86` merely because this prompt says it is expected.

Prove:

```text
recognized governance artifacts present = actual
recognized governance artifacts missing = actual
duplicates = actual
```

Then calculate:

```text
final candidate count = 78 + recognized governance artifacts present
```

Success requires all eight recognized governance artifacts to be present exactly once, which should derive:

```text
final candidate count = 86
```

If the derived count differs, stop.

---

## 6. Starting-State Gate

Before staging anything, record and prove:

```text
branch = release/1.0-market-data-foundation
HEAD = 138582919d220427d2e87434533b82bf8801f8db
commit created = NO
remote integration branch = absent
PR = absent
```

Expected repository state based on the latest blocked report plus this newly created finalization pair:

```text
staged files = 80
unstaged tracked files = 0
untracked integration-governance artifacts = 6
unexpected files = 0
```

The six expected untracked files are:

```text
release-1.0-github-integration-whitespace-unblock-codex-prompt.md
release-1.0-github-integration-whitespace-unblock-codex-prompt-chat.md
release-1.0-github-integration-whitespace-unblock-governance-reconciliation-codex-prompt.md
release-1.0-github-integration-whitespace-unblock-governance-reconciliation-codex-prompt-chat.md
release-1.0-github-integration-governance-finalization-codex-prompt.md
release-1.0-github-integration-governance-finalization-codex-prompt-chat.md
```

The original integration prompt/chat pair should already be among the 80 staged files.

If the actual state differs, classify the difference.

A harmless difference is only acceptable if it still reconciles exactly to the same eight recognized governance artifacts and no unexpected file exists.

Any additional unrecognized file is a blocker.

---

## 7. Governance Artifact Inventory

Enumerate all eight recognized integration-lifecycle governance artifacts.

For each record:

```text
path
tracked/staged/untracked state
byte length
SHA-256
line count
```

For each `*-prompt-chat.md`, verify exactly five physical lines.

Required:

```text
recognized governance artifacts = 8/8
duplicate recognized artifacts = 0
missing recognized artifacts = 0
chat companions = 4
all chat companions = 5 lines each
```

Do not edit any governance artifact.

---

## 8. Governance Content Preservation

This finalization step must preserve all eight governance files unchanged.

Before staging any currently untracked governance file, capture SHA-256 and byte length.

After staging, recompute and prove:

```text
hash preserved = PASS
byte length preserved = PASS
line count preserved = PASS
```

Do not normalize Markdown.

Do not fix whitespace.

Do not re-save files through a formatter.

Do not alter line endings deliberately.

---

## 9. Stage All Recognized Untracked Governance Artifacts

Stage every recognized integration-lifecycle governance artifact that is currently untracked.

Do not stage any unrecognized file.

Do not unstage any existing candidate file.

Do not reset.

Do not discard accepted content.

After staging, require:

```text
WP16 accepted candidate = 78/78
recognized integration-lifecycle governance = 8/8
final governed integration candidate = 86/86
staged files = 86
unstaged files = 0
untracked files = 0
unexpected files = 0
```

This 86-file count supersedes the earlier 80/82/84 fixed-count gates.

---

## 10. Candidate Integrity After Governance Staging

Prove:

```text
technical WP16 candidate content changed = NO
original integration governance changed = NO
whitespace-unblock governance changed = NO
reconciliation governance changed = NO
finalization governance changed = NO
product changed = NO
tests changed = NO
architecture changed = NO
documentation semantics changed = NO
packages/projects/build changed = NO
```

Governance membership changed; candidate semantics did not.

---

## 11. Cached Whitespace Discovery

After all 86 files are staged, run:

```powershell
git diff --cached --check
```

This is now the authoritative discovery point for whitespace scope.

Do **not** assume the violation count remains 31.

Record:

```text
total cached whitespace violations
affected files
violation type per finding
```

Classify each affected file as:

```text
already-authorized whitespace target
recognized governance artifact
unexpected file
```

Do not correct anything in this finalization step.

---

## 12. Expected Whitespace Outcomes

### Outcome A — no new governance findings

If the cached findings remain exactly:

```text
31 violations
7 affected files
```

and those are the same seven previously authorized files, record that the existing whitespace-unblock target set remains sufficient.

### Outcome B — new findings in recognized governance artifacts

If staging the reconciliation/finalization governance files exposes additional whitespace findings, record the exact expanded set.

Because this finalization prompt does not authorize whitespace correction, stop the whitespace phase and report that a later whitespace authority must include the newly discovered governance files/findings.

### Outcome C — finding in an unexpected file

This is a blocker.

Do not expand authority automatically.

---

## 13. Finalization Does Not Perform Whitespace Correction

Do not edit any of the seven original whitespace target files.

Do not edit newly staged governance artifacts.

Do not remove trailing whitespace.

Do not remove blank EOF lines.

Do not change Git whitespace policy.

Do not modify `.gitattributes`, `.editorconfig`, or Git configuration.

This step only finalizes governance membership and discovers the complete staged whitespace scope.

---

## 14. No Technical Revalidation Required Yet

Do not rerun the complete 105-test suite merely for staging governance files unless an unexpected executable file change is observed.

The next whitespace-unblock or integration-resume step will rerun canonical verification as required.

If you do run validation, it must be read-only with respect to candidate content.

No live provider call is authorized.

---

## 15. Git / GitHub Protection

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
mutate issues #86–#101
mutate Project fields/items
mutate labels
create tag
create GitHub Release
create Release 1.1 planning
begin Release 1.1 implementation
```

No GitHub mutation is required.

---

## 16. Working-Tree Final State

Successful governance finalization must end with:

```text
branch = release/1.0-market-data-foundation
HEAD = 138582919d220427d2e87434533b82bf8801f8db

WP16 candidate = 78/78 staged
recognized integration governance = 8/8 staged
final governed integration candidate = 86/86 staged

staged files = 86
unstaged files = 0
untracked files = 0
unexpected files = 0

commit = none
remote integration branch = absent
PR = absent
```

Cached whitespace may still fail. That does not block governance finalization unless the failure reveals an unexpected file or an authority conflict.

---

## 17. Permanent Recursion-Break Rule

After this prompt succeeds, no later integration-lifecycle prompt should create another in-repository governance artifact unless that later prompt explicitly authorizes its own pair into the governed candidate or explicitly declares itself out-of-band by human authority.

For the current Release 1.0 integration chain, treat the eight recognized governance artifacts listed in Section 4 as the finalized governance set.

Do not create another governance-reconciliation prompt merely because this prompt exists.

---

## 18. Superseding Candidate-Count Authority

This prompt supersedes all earlier integration candidate counts:

```text
80
82
84
```

with the final governed count:

```text
86
```

provided all eight recognized integration-lifecycle governance files are present exactly once.

Subsequent whitespace-unblock and integration-resume executions must use:

```text
candidate = 86
staged = 86
```

not the earlier counts.

---

## 19. Blocker Policy

Return `RELEASE 1.0 GITHUB INTEGRATION GOVERNANCE FINALIZATION BLOCKED` if:

- the 78-file WP16 candidate no longer reconciles;
- any recognized governance artifact is missing;
- any recognized governance artifact is duplicated;
- any chat companion is not five lines;
- an unrecognized file exists;
- an existing staged candidate file is lost;
- a governance artifact changes during staging;
- final staged count is not exactly 86;
- unstaged or untracked files remain after staging;
- candidate semantics changed;
- a commit/push/PR already exists unexpectedly;
- Release 1.1 artifacts exist;
- cached whitespace reveals an unexpected non-authorized file.

New whitespace findings inside recognized governance artifacts do not invalidate governance membership, but they must be reported and must block any whitespace correction until a later authority explicitly covers them.

---

## 20. Required Execution Report

Return:

```text
# Release 1.0 Git/GitHub Integration Governance Finalization Execution Report
```

with:

1. Executive Summary
2. Authorities Reviewed
3. Initial Repository State
4. Governance Recursion Reconciliation
5. WP16 Candidate Preservation
6. Recognized Governance Artifact Inventory
7. Pre-Staging Hash / Line Evidence
8. Governance Staging Action
9. Final Candidate Count Derivation
10. Post-Staging Hash Preservation
11. Candidate Integrity
12. Cached Whitespace Discovery
13. Whitespace Scope Classification
14. Working-Tree Classification
15. Git / GitHub Protection
16. Scope Protection
17. Findings / Observations
18. Acceptance Matrix
19. Final Repository State
20. Final Decision
21. Next Authorized Action

---

## 21. Acceptance Matrix

Report:

| Requirement | Result |
|---|---|
| WP16 candidate | 78/78 |
| Recognized integration governance artifacts | 8/8 |
| Missing recognized governance artifacts | 0 |
| Duplicate recognized governance artifacts | 0 |
| Chat companions | 4/4 |
| Each chat companion line count | 5 |
| Governance hashes preserved | PASS/FAIL |
| Final governed candidate | 86/86 |
| Staged files | 86 |
| Unstaged files | 0 |
| Untracked files | 0 |
| Unexpected files | 0 |
| Product/test/architecture semantic drift | 0 |
| Cached whitespace check | PASS/FAIL |
| Cached whitespace violations | actual count |
| Cached affected files | actual count |
| Unexpected whitespace-affected files | count |
| Commit created | NO |
| Push performed | NO |
| PR created | NO |
| Planning mutated | NO |
| Release 1.1 started | NO |

---

## 22. Success Criteria

Governance finalization succeeds only if:

- the technical WP16 candidate remains 78/78;
- all eight recognized integration-lifecycle governance artifacts are present exactly once;
- all four chat companions are exactly five lines;
- all governance artifacts are byte-preserved through staging;
- all recognized governance artifacts are staged;
- final governed candidate derives to 86/86;
- staged = 86;
- unstaged = 0;
- untracked = 0;
- unexpected = 0;
- candidate semantics are unchanged;
- no commit/push/PR/planning/closure/Release 1.1 action occurs;
- cached whitespace scope is fully enumerated;
- no cached whitespace finding belongs to an unexpected file.

The exact success terminal is:

```text
RELEASE 1.0 GITHUB INTEGRATION GOVERNANCE FINALIZATION COMPLETE
```

---

## 23. Failure Criteria

If any mandatory governance-finalization condition fails, use exactly:

```text
RELEASE 1.0 GITHUB INTEGRATION GOVERNANCE FINALIZATION BLOCKED
```

State the minimum additional human authority required.

---

## 24. Next Authorized Action After Success

After successful governance finalization, inspect the cached whitespace discovery result.

### If cached findings remain exactly the original 31 across the original seven files

Resume the existing whitespace-unblock prompt:

```text
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-whitespace-unblock-codex-prompt.md
```

under this final authority with superseded state:

```text
candidate = 86
staged = 86
unstaged = 0
untracked = 0
```

and with the same original seven-file whitespace whitelist.

### If new cached whitespace findings appear in recognized governance artifacts

Do **not** run another governance reconciliation.

Instead create one separately authorized **whitespace-scope expansion** prompt that:

- keeps the candidate count fixed at 86;
- does not add any new governance artifact to the repository unless that prompt explicitly self-authorizes or is declared out-of-band by human authority;
- authorizes only the exact newly discovered whitespace findings;
- preserves all substantive content.

### After whitespace validation passes

Resume the original integration prompt:

```text
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt.md
```

under this final 86-file authority.

The final integration commit must contain exactly 86 files unless a later explicit human authority changes the finalized governance set.

Do not merge.

Do not close Release 1.0.

Do not begin Release 1.1.

---

## Execution Instruction

Read all Release 1.0 integration authorities and blocked reports; prove the current repository still contains the immutable 78-file WP16 candidate and the complete integration-governance chain; enumerate exactly the eight recognized integration-lifecycle governance artifacts including this finalization prompt pair; verify all four companions are five lines; hash and byte-count every governance artifact; stage every recognized untracked governance artifact unchanged; derive the final governed candidate count from repository truth and require it to equal exactly 86; leave zero unstaged, untracked, or unexpected files; prove no candidate semantics changed; run `git diff --cached --check` only to discover the complete whitespace scope after all governance is staged; do not correct whitespace, commit, push, create a PR, merge, mutate planning, close Release 1.0, or begin Release 1.1; return the complete governance-finalization report; and stop so the next whitespace or integration step can proceed under this final 86-file authority.
