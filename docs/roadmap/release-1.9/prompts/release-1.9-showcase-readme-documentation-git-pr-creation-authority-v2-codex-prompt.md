# Release 1.9 — Showcase Guide + README Documentation Git/PR Creation Authority (Reissued)

## Model
Use **GPT-5.6 Terra**.

## Purpose
Package the already accepted Release 1.9 documentation-only payload into one narrow Git commit and one GitHub pull request.

This is the reissued authority after README drift acceptance/reclassification. The current user-reviewed `README.md` is now the canonical accepted baseline for PR packaging.

This authority may create/use a dedicated documentation branch, stage exactly the two accepted paths, create one commit, push that branch, create one PR targeting `main`, and verify the frozen PR payload. STOP before merge.

## Canonical accepted README baseline
The current `README.md` is accepted as-is, including:
- Current closed milestone = Release 1.9 / #58.
- Current accepted milestone = Release 1.10 / #59.
- 1.8 completed.
- 1.9 completed.
- 1.10 planned/accepted as next milestone.
- Python badge/tag present.
- completed 1.8 and 1.9 descriptions present.
- Release 1.9 showcase-guide front-door link present.
- deterministic/replay disclosure preserved.
- governed .NET → canonical JSON → Python/Streamlit boundary preserved.
- no direct Streamlit → SQLite/provider bypass.
- reviewed table-formatting changes accepted.

Do NOT compare README against the older snapshot where Current accepted milestone was 1.9. Do NOT revert the accepted 1.10 / #59 state.

## Exact authorized payload
Only:
1. `README.md`
2. `docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

No other path may be staged, committed, or included in the PR.

## Published state to preserve
- Release 1.9 completed/accepted.
- PR #238 merged.
- PR #239 governance follow-up merged.
- `v1.9.0` remains anchored to `e4958721c9a581efbb2552134c00bc146c73f047`.
- GitHub Release `v1.9.0` remains published.
- milestone #58 remains Closed 0/13.
- milestone #59 remains Open.
- #233–#237 remain Closed / Done.
- accepted baseline remains .NET 339/339, Python 17/17, schema v4, Streamlit 1.61.1.

## Phase 0 — Entry-state verification
Record current branch, HEAD, `origin/main`, `git status --short`, staged paths, untracked paths, working-tree diff for the two authorized files, and unrelated dirty work.

Preserve unrelated user work. Do not reset, clean, stash, or discard. If unrelated staged changes cannot be safely isolated, BLOCK.

## Phase 1 — Accepted-content gate
README must match the newly accepted baseline above. In particular, `Current accepted milestone = Release 1.10 / #59` is expected and must not be treated as drift.

Showcase guide must remain at:
`docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

and preserve simulated/replay disclosure, governed .NET → JSON → Python/Streamlit flow, no direct SQLite/provider bypass, and no live broker/provider overclaim.

If either file has new material drift after this re-baseline, BLOCK rather than rewriting.

## Phase 2 — Branch
Create or safely reuse:
`docs/release-1.9-showcase-readme`

The branch must be based on current `origin/main`. If it exists, inspect history and require no unrelated commits. Do not force-reset user work.

Record branch name and starting SHA.

## Phase 3 — Staging
Stage exactly:
- `README.md`
- `docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

Verify with:
`git diff --cached --name-only`

Expected exact set:

```text
README.md
docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md
```

If any third path is staged, BLOCK unless it was staged by this authority and can be safely unstaged without disturbing user work.

## Phase 4 — Commit
Create exactly one commit:

`docs: showcase completed Release 1.9`

Verify commit SHA, parent SHA, message, changed-file count = 2, and exact committed path set = the two authorized paths.

## Phase 5 — Push
Push only the dedicated documentation branch. Set upstream if needed.

Forbidden: force push, pushing tags, pushing `main`, deleting branches, mutating unrelated refs.

## Phase 6 — Pull request creation
Create exactly one PR targeting `main`.

Preferred title:
`Docs: Showcase completed Release 1.9`

Preferred body:

```markdown
## Summary

- adds the Release 1.9 showcase and local-run guide;
- updates the front-door README to reflect Release 1.9 as completed;
- preserves Release 1.10 / milestone #59 as the current accepted next milestone;
- keeps Python visible in the engineering stack;
- preserves the reviewed README table-formatting changes.

## Scope

Documentation only.

Changed paths:
- `README.md`
- `docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

## Validation

- current README user-reviewed baseline accepted;
- milestone links #56, #58, and #59 validated as applicable;
- Release 1.9 remains completed;
- Release 1.10 / #59 remains current accepted/next milestone;
- deterministic/replay and governed .NET → JSON → Python/Streamlit semantics preserved;
- no executable/configuration changes;
- no tag/Release/milestone/issue/Project lifecycle changes.

## Technical impact

None. Existing Release 1.9 acceptance evidence remains unchanged.
```

Do not add unrelated issue-closing keywords.

## Phase 7 — Frozen-payload verification
Immediately read back PR number, URL, title, state, base/head, head SHA, commit count, changed-file count, and exact changed paths.

Use paginated PR files API or equivalent authoritative source.

Require changed-file count = `2` and exact set:

```text
README.md
docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md
```

If payload differs, BLOCK. Do not merge.

## Phase 8 — Release integrity
Read-only verify:
- `v1.9.0` still resolves to `e4958721c9a581efbb2552134c00bc146c73f047`;
- GitHub Release remains published;
- milestone #58 remains Closed 0/13;
- milestone #59 remains Open;
- #233–#237 remain Closed / Done.

## Phase 9 — Stop boundary
STOP after successful PR creation + frozen-payload verification.

Not authorized:
- merge;
- approval;
- merge queue mutation;
- branch deletion;
- tag mutation;
- GitHub Release mutation;
- milestone mutation;
- issue/Project mutation.

A separate review/checks/merge authority is required.

## Required success report
Report baseline gate, branch, commit, push, PR metadata, and exact frozen payload.

State:

`RELEASE 1.9 DOCUMENTATION PR FROZEN PAYLOAD: PASS — 2/2 PATHS`

`NONE — DOCUMENTATION-ONLY`

`RELEASE 1.9 DOCUMENTATION PR AUTHORITY REPOSITORY CONTENT MUTATIONS: ZERO BEYOND PRE-ACCEPTED README + GUIDE`

`RELEASE 1.9 DOCUMENTATION PR AUTHORITY GIT MUTATIONS: BRANCH + TWO-PATH STAGE + ONE COMMIT + PUSH ONLY`

`RELEASE 1.9 DOCUMENTATION PR AUTHORITY GITHUB MUTATIONS: ONE PR CREATED ONLY`

`RELEASE 1.9 DOCUMENTATION PR CREATED — SEPARATE REVIEW/CHECKS/MERGE AUTHORITY REQUIRED`

Terminal:

`RELEASE 1.9 SHOWCASE + README DOCUMENTATION GIT/PR CREATION AUTHORITY V2 COMPLETE`

## Required blocked report
Report exact blocking state, current branch/HEAD, staged/untracked conflicts, any new post-rebaseline README drift, payload mismatch, mutations already performed, PR state if created, and minimum reconciliation needed.

Terminal:

`RELEASE 1.9 SHOWCASE + README DOCUMENTATION GIT/PR CREATION AUTHORITY V2 BLOCKED`

Do not emit COMPLETE unless the PR is open/unmerged and frozen to exactly the two accepted documentation paths.
