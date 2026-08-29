# Release 1.9 — Showcase Guide + README Documentation Git/PR Creation Authority

## Model
Use **GPT-5.6 Terra**.

## Purpose
Package the already accepted Release 1.9 documentation-only payload into one narrow Git commit and one GitHub pull request, then STOP before merge.

## Exact authorized payload
Only:
1. `README.md`
2. `docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

No other path may be staged, committed, pushed as content, or included in the PR.

## Published state to preserve
- Release 1.9 is completed/accepted.
- PR #238 merged.
- PR #239 governance follow-up merged.
- tag `v1.9.0` remains anchored to `e4958721c9a581efbb2552134c00bc146c73f047`.
- GitHub Release `v1.9.0` remains published.
- milestone #58 remains Closed, 0 open / 13 closed.
- #233–#237 remain Closed / Done.
- accepted baseline remains .NET 339/339, Python 17/17, schema v4, Streamlit 1.61.1.

## Phase 0 — Entry-state verification
Record current branch, HEAD, `origin/main`, `git status --short`, staged paths, untracked paths, and diffs for the two authorized files.

Preserve unrelated user work exactly. Do not reset, clean, stash, or discard.

If unrelated staged changes cannot be safely isolated, BLOCK.

## Phase 1 — Accepted-diff verification
Verify the current accepted content is intact.

README must still contain:
- Python badge/tag;
- Current closed milestone = Release 1.9 / #58;
- Current accepted milestone = Release 1.9 / #58;
- 1.8 completed;
- 1.9 completed/accepted;
- 1.10 planned;
- concise completed descriptions for 1.8 and 1.9;
- no stale current-state `1.9 Planned`;
- showcase-guide front-door link.

Guide must remain at:
`docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

and preserve:
- deterministic simulated/replay disclosure;
- governed .NET → canonical JSON → Python/Streamlit boundary;
- no direct Streamlit → SQLite/provider bypass;
- no live broker/provider overclaim.

If material drift exists, BLOCK rather than silently rewriting.

## Phase 2 — Branch
Create or safely reuse:

`docs/release-1.9-showcase-readme`

from current `origin/main`.

Do not overwrite unrelated commits.

Record branch name and starting SHA.

## Phase 3 — Staging
Stage exactly:

- `README.md`
- `docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

Verify with `git diff --cached --name-only`.

Expected exact set:

```text
README.md
docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md
```

If any third path is staged, BLOCK unless it was staged by this authority and can be safely unstaged without disturbing user work.

## Phase 4 — Commit
Create exactly one commit:

`docs: showcase completed Release 1.9`

Verify:
- one commit created by this authority;
- changed-file count = 2;
- exact path set = the two authorized paths.

## Phase 5 — Push
Push only the dedicated documentation branch.

Allowed:
- set upstream if needed.

Forbidden:
- force push;
- pushing tags;
- pushing `main`;
- deleting branches;
- mutating unrelated refs.

## Phase 6 — Pull request creation
Create exactly one PR targeting `main`.

Preferred title:

`Docs: Showcase completed Release 1.9`

Preferred body:

```markdown
## Summary

- adds the Release 1.9 showcase and local-run guide;
- updates the front-door README to reflect Release 1.9 as completed/accepted;
- adds Python to the visible engineering stack;
- advances the current closed/accepted milestone to Release 1.9;
- preserves Release 1.10 as the next planned milestone.

## Scope

Documentation only.

Changed paths:
- `README.md`
- `docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

## Validation

- repository paths and guide links validated;
- milestone links #56 and #58 validated;
- stale current-state `1.9 Planned` wording removed;
- deterministic/replay and governed .NET → JSON → Python/Streamlit semantics preserved;
- no executable/configuration changes;
- no GitHub Release/tag/milestone/issue/Project lifecycle changes.

## Technical impact

None. Existing Release 1.9 acceptance evidence remains unchanged.
```

Do not add unrelated issue-closing keywords.

## Phase 7 — Frozen-payload verification
Immediately read back the PR and record:
- PR number;
- URL;
- title;
- state;
- base/head;
- head SHA;
- commit count;
- changed-file count;
- exact changed paths.

Use paginated PR files API or equivalent authoritative source.

Require exact payload:

```text
README.md
docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md
```

Require changed-file count = `2`.

If anything differs, BLOCK and do not merge.

## Phase 8 — Release integrity
Read-only verify:
- `v1.9.0` still resolves to `e4958721c9a581efbb2552134c00bc146c73f047`;
- GitHub Release remains published;
- milestone #58 remains Closed 0/13;
- #233–#237 remain Closed / Done.

## Phase 9 — Stop boundary
STOP after successful PR creation + frozen-payload verification.

Not authorized:
- PR merge;
- approval;
- merge queue mutation;
- branch deletion;
- tag mutation;
- GitHub Release mutation;
- milestone mutation;
- issue/Project mutation.

A separate review/checks/merge authority is required.

## Required success report

### Branch
- name
- starting SHA

### Commit
- commit SHA
- message
- exact two-file manifest

### Push
- remote branch
- pushed SHA

### PR
- number
- URL
- title
- base/head
- head SHA
- state
- commit count
- changed-file count

### Frozen payload

```text
README.md
docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md
```

`RELEASE 1.9 DOCUMENTATION PR FROZEN PAYLOAD: PASS — 2/2 PATHS`

### Release integrity
- `v1.9.0`: unchanged
- GitHub Release: unchanged
- milestone #58: Closed 0/13
- #233–#237: unchanged

### Technical impact
`NONE — DOCUMENTATION-ONLY`

### Mutation markers
`RELEASE 1.9 DOCUMENTATION PR AUTHORITY REPOSITORY CONTENT MUTATIONS: ZERO BEYOND PRE-ACCEPTED README + GUIDE`

`RELEASE 1.9 DOCUMENTATION PR AUTHORITY GIT MUTATIONS: BRANCH + TWO-PATH STAGE + ONE COMMIT + PUSH ONLY`

`RELEASE 1.9 DOCUMENTATION PR AUTHORITY GITHUB MUTATIONS: ONE PR CREATED ONLY`

`RELEASE 1.9 DOCUMENTATION PR CREATED — SEPARATE REVIEW/CHECKS/MERGE AUTHORITY REQUIRED`

Terminal:

`RELEASE 1.9 SHOWCASE + README DOCUMENTATION GIT/PR CREATION AUTHORITY COMPLETE`

## Required blocked report
Report exact blocker, current branch/HEAD, staged/untracked conflicts, payload mismatch, mutations already performed, PR state if created, and minimum reconciliation needed.

Terminal:

`RELEASE 1.9 SHOWCASE + README DOCUMENTATION GIT/PR CREATION AUTHORITY BLOCKED`

Do not emit COMPLETE unless the PR is open/unmerged and frozen to exactly the two accepted documentation paths.
