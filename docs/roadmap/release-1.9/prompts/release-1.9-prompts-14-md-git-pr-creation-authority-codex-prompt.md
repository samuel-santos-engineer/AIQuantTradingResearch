# Release 1.9 — Prompts Folder 14-Markdown Git/PR Creation Authority

## Model

Use **GPT-5.6 Terra**.

## Purpose

Create one narrow documentation/governance pull request containing the **exact 14 currently unmerged Markdown files** under:

`docs/roadmap/release-1.9/prompts`

This authority must discover and freeze the exact 14-file manifest before any Git mutation, then package only those files into one commit and one PR targeting `main`.

STOP before merge.

## Scope boundary

Only Markdown files under:

`docs/roadmap/release-1.9/prompts`

Expected unmerged count: `14`.

No file outside this folder is authorized. No non-Markdown file is authorized.

If the exact unmerged count is not 14, BLOCK before staging.

## Current main baseline

Expected current `origin/main`:

`35ec644576275570aee522872c770e6c06e7879d`

Verify at entry. If `origin/main` advanced, record it and continue only if the 14-file prompts-only scope remains provable and safe.

Do not reset or overwrite user work.

## Published state to preserve

- Release 1.9 completed/accepted.
- `v1.9.0` remains anchored to `e4958721c9a581efbb2552134c00bc146c73f047`.
- GitHub Release `v1.9.0` remains published.
- milestone #58 remains Closed 0/13.
- milestone #59 remains Open.
- #233–#237 remain Closed / Done.
- PR #240 remains merged.

## Phase 0 — Entry-state audit

Record:

- current branch;
- current HEAD;
- `origin/main`;
- `git status --short`;
- staged paths;
- untracked paths;
- all working-tree changes;
- all files under `docs/roadmap/release-1.9/prompts`.

Do not reset, clean, stash, or discard unrelated work.

If unrelated staged changes cannot be safely isolated, BLOCK.

## Phase 1 — Discover exact unmerged manifest

Determine the exact Markdown files under `docs/roadmap/release-1.9/prompts` that are not represented on current `origin/main`.

Include as applicable:

- untracked Markdown files;
- modified Markdown files;
- locally committed-but-unmerged Markdown files.

Use authoritative Git comparisons, not filename assumptions.

Require:

1. every path is under `docs/roadmap/release-1.9/prompts/`;
2. every path ends in `.md`;
3. exact count = `14`;
4. none is already identical to `origin/main`;
5. no out-of-folder dependency is required.

Sort and print the manifest before staging.

State:

`RELEASE 1.9 PROMPTS PR CANDIDATE MANIFEST: 14/14 MD PATHS DISCOVERED`

If count != 14, BLOCK.

## Phase 2 — Content classification

Read/review all 14 Markdown files.

Require each to be Release 1.9 prompt/authority/governance documentation.

Require:

- no source/test/package/schema/config/runtime changes;
- no secrets;
- no binary files;
- no generated runtime artifacts.

Prompt artifacts may contain model assignments, governance authorities, validation instructions, Git/GitHub execution instructions, terminal markers, and historical Release 1.9 evidence.

Do not rewrite prompt contents merely for style.

If any file is not clearly in scope, BLOCK and identify it.

## Phase 3 — Freeze manifest

Freeze the exact 14-path set discovered above.

No thirteenth path may enter afterward.

All staging, commit, push, and PR verification must use exact set equality against this manifest.

## Phase 4 — Branch

Create or safely reuse:

`docs/release-1.9-prompts`

from current `origin/main`.

If it already exists, inspect it and require no unrelated commits. Do not force-reset or overwrite user work.

Record branch name and starting SHA.

## Phase 5 — Staging

Stage exactly the frozen 14 Markdown paths.

After staging, verify:

`git diff --cached --name-only`

Require exact staged set = frozen manifest and staged count = 14.

If any unexpected path appears, BLOCK unless it was staged by this authority and can be safely unstaged without disturbing user work.

## Phase 6 — Commit

Create exactly one commit.

Preferred message:

`docs: add remaining Release 1.9 prompt authorities`

Verify:

- one commit created;
- changed-file count = 14;
- every committed path is under `docs/roadmap/release-1.9/prompts/`;
- every path ends in `.md`;
- committed set = frozen manifest.

Record commit SHA and parent SHA.

## Phase 7 — Push

Push only the dedicated branch.

Allowed: set upstream.

Forbidden:

- force push;
- push tags;
- push `main`;
- delete branches;
- mutate unrelated refs.

## Phase 8 — Pull request creation

Create exactly one PR targeting `main`.

Preferred title:

`Docs: Add remaining Release 1.9 prompt authorities`

Preferred body:

```markdown
## Summary

Adds the remaining Release 1.9 prompt/authority Markdown artifacts under:

`docs/roadmap/release-1.9/prompts`

## Scope

Documentation/governance only.

- 14 Markdown files
- no source code
- no tests
- no packages
- no schema/config changes
- no Release/tag/milestone/issue/Project lifecycle changes

## Validation

- exact 14-file manifest frozen before staging;
- staged set = committed set = PR payload;
- every path is under `docs/roadmap/release-1.9/prompts/`;
- every path is Markdown;
- Release 1.9 published state remains unchanged.

## Technical impact

None.
```

Do not add unrelated issue-closing keywords.

## Phase 9 — PR frozen-payload verification

Immediately read back:

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

Require:

- state = Open;
- base = `main`;
- commit count = `1`;
- changed-file count = `14`;
- exact PR path set = frozen manifest;
- every path under `docs/roadmap/release-1.9/prompts/`;
- every path ends in `.md`.

State:

`RELEASE 1.9 PROMPTS PR FROZEN PAYLOAD: PASS — 14/14 MD PATHS`

If any mismatch exists, BLOCK and do not merge.

## Phase 10 — Release integrity

Read-only verify:

- `v1.9.0` still targets `e4958721c9a581efbb2552134c00bc146c73f047`;
- GitHub Release remains published;
- milestone #58 remains Closed 0/13;
- milestone #59 remains Open;
- #233–#237 remain Closed / Done;
- PR #240 remains merged.

## Stop boundary

STOP after successful PR creation + 14/14 frozen-payload verification.

Not authorized:

- PR merge;
- review approval;
- branch deletion;
- tag mutation;
- GitHub Release mutation;
- milestone mutation;
- issue mutation;
- Project mutation.

A separate review/checks/merge authority is required.

## Allowed mutations

### Repository content

Zero new content edits. Package the existing 14 prompt Markdown files as-is.

### Git

Allowed:

- dedicated branch creation/switch;
- stage exact 14 frozen paths;
- one commit;
- branch push.

### GitHub

Allowed:

- exactly one PR creation.

## Acceptance criteria

PASS only if:

1. exactly 14 unmerged `.md` files are discovered;
2. all 14 are under `docs/roadmap/release-1.9/prompts/`;
3. all 14 are Release 1.9 prompt/governance artifacts;
4. manifest frozen before staging;
5. staged set = manifest;
6. commit set = manifest;
7. PR set = manifest;
8. changed-file count = 14;
9. no other path enters PR;
10. Release lifecycle state unchanged;
11. PR remains open/unmerged.

## Required success report

### Entry

- current branch
- current HEAD
- `origin/main`

### Frozen manifest

Print all 14 paths in sorted order.

`RELEASE 1.9 PROMPTS PR CANDIDATE MANIFEST: 14/14 MD PATHS DISCOVERED`

### Branch

- branch name
- starting SHA

### Commit

- commit SHA
- parent SHA
- message
- changed-file count = 14

### Push

- remote branch
- pushed SHA

### PR

- PR number
- URL
- title
- base/head
- head SHA
- state
- commit count
- changed-file count = 14

### Frozen payload

`RELEASE 1.9 PROMPTS PR FROZEN PAYLOAD: PASS — 14/14 MD PATHS`

### Release integrity

- `v1.9.0`: unchanged
- GitHub Release: unchanged
- milestone #58: Closed 0/13
- milestone #59: Open
- #233–#237: unchanged
- PR #240: merged

### Technical impact

`NONE — PROMPT/GOVERNANCE DOCUMENTATION ONLY`

### Mutation markers

`RELEASE 1.9 PROMPTS PR AUTHORITY REPOSITORY CONTENT MUTATIONS: ZERO — EXISTING 14 MD FILES PACKAGED AS-IS`

`RELEASE 1.9 PROMPTS PR AUTHORITY GIT MUTATIONS: BRANCH + 14-PATH STAGE + ONE COMMIT + PUSH ONLY`

`RELEASE 1.9 PROMPTS PR AUTHORITY GITHUB MUTATIONS: ONE PR CREATED ONLY`

### Next step

`RELEASE 1.9 PROMPTS PR CREATED — SEPARATE REVIEW/CHECKS/MERGE AUTHORITY REQUIRED`

Terminal:

`RELEASE 1.9 PROMPTS 14-MD GIT/PR CREATION AUTHORITY COMPLETE`

## Required blocked report

Report exact discovered count, complete candidate manifest, unexpected/out-of-scope paths, staged conflicts, content-classification failures, mutations already performed, and PR state if already created.

Terminal:

`RELEASE 1.9 PROMPTS 14-MD GIT/PR CREATION AUTHORITY BLOCKED`

Do not emit COMPLETE unless the PR is open/unmerged and frozen to exactly the discovered 14 Markdown paths.
