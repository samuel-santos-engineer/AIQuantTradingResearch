# Release 1.9 — PR #240 Review / Checks / Merge Authority

## Model
Use **GPT-5.6 Terra**.

## Purpose
Review, validate, and—only if every gate passes—merge:

**PR #240 — Docs: Showcase completed Release 1.9**

This is a narrow documentation-only merge authority.

The PR is expected to contain exactly two accepted paths:

1. `README.md`
2. `docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

Expected PR head SHA:

`77fcbc59b01b12626e0b49c09a9fa30bc872116f`

Expected branch:

`docs/release-1.9-showcase-readme`

Expected base:

`main`

Expected commit count:

`1`

Do not merge if any of these frozen expectations drift materially.

---

# Release state to preserve

Treat as binding:

- Release 1.9 remains completed/accepted.
- tag `v1.9.0` must remain anchored to:
  `e4958721c9a581efbb2552134c00bc146c73f047`
- GitHub Release `v1.9.0` remains published, non-draft, non-prerelease.
- milestone #58 remains Closed, 0 open / 13 closed.
- milestone #59 remains Open.
- #233–#237 remain Closed / Done.
- PR #238 remains merged.
- PR #239 remains merged.
- PR #240 is documentation-only.

The merge of PR #240 must NOT retarget or recreate `v1.9.0`.

---

# Phase 0 — Read-only entry verification

Before any mutation, read back PR #240 and record:

- number;
- title;
- URL;
- state;
- draft status;
- base branch;
- head branch;
- head SHA;
- commit count;
- changed-file count;
- mergeability / merge state;
- review decision if available;
- required checks / status checks;
- labels if relevant;
- exact changed paths.

Use paginated PR files API or equivalent authoritative source.

Expected:

- state = Open;
- draft = false;
- base = `main`;
- head = `docs/release-1.9-showcase-readme`;
- head SHA = `77fcbc59b01b12626e0b49c09a9fa30bc872116f`;
- commit count = `1`;
- changed-file count = `2`.

If head SHA changed:
BLOCK.

If commit count is no longer 1:
BLOCK unless the additional commit is proven to be an authorized no-op metadata artifact, which normally should not occur.

---

# Phase 1 — Frozen payload gate

Require exact path set:

```text
README.md
docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md
```

No third path is allowed.

Use paginated files enumeration and compare exact set equality.

Report:

`RELEASE 1.9 PR #240 FROZEN PAYLOAD: PASS — 2/2 PATHS`

If any unexpected path exists:
BLOCK and do not merge.

---

# Phase 2 — Diff review

Review the PR diff for both files.

## README must preserve accepted baseline

Require:

- Current closed milestone = Release 1.9 / #58.
- Current accepted milestone = Release 1.10 / #59.
- 1.8 completed.
- 1.9 completed.
- 1.10 remains planned/accepted as next milestone.
- truthful Python badge/tag preserved.
- completed 1.8 and 1.9 descriptions preserved.
- Release 1.9 showcase-guide front-door link valid.
- accepted table-formatting changes preserved.
- no stale current-state `1.9 Planned`.
- no contradiction implying Release 1.9 is unfinished.

## Showcase guide must preserve

- deterministic simulated/replay disclosure;
- governed .NET → canonical JSON → Python/Streamlit boundary;
- no direct Streamlit → SQLite/provider bypass;
- no live broker/provider overclaim;
- valid repository-local instructions and links.

## Technical scope

Require:
- no source code changes;
- no test changes;
- no package/dependency changes;
- no schema changes;
- no CI/config/runtime changes;
- no secrets/local machine material.

Technical impact must remain:

`NONE — DOCUMENTATION-ONLY`

If diff semantics drift from the accepted documentation baseline:
BLOCK.

---

# Phase 3 — Checks / review gate

Inspect all checks/statuses currently associated with PR #240.

Require all repository-required checks to be successful or otherwise satisfy repository merge policy.

If checks are:
- pending: STOP/BLOCK;
- failing: STOP/BLOCK;
- cancelled/skipped where repository policy requires success: STOP/BLOCK;
- absent and repository policy does not require checks: record that fact explicitly.

Inspect review state.

If branch protection requires approval:
require it.

If no approval is required:
record that approval is not a merge gate.

Do not self-approve unless explicitly authorized by repository policy and current account role, and even then this authority prefers read-only review inspection.

---

# Phase 4 — Mergeability gate

Require PR #240 to be mergeable against current `main`.

Verify:
- no merge conflict;
- no base-branch protection blocker;
- no stale-head requirement;
- no unresolved required conversation if enforced;
- no unexpected required status.

If branch must be updated/rebased before merge:
BLOCK.
Do not rewrite the branch under this authority.

---

# Phase 5 — Pre-merge release integrity check

Immediately before merge, read-only verify:

- `v1.9.0` still targets:
  `e4958721c9a581efbb2552134c00bc146c73f047`
- GitHub Release `v1.9.0` remains published;
- milestone #58 remains Closed 0/13;
- milestone #59 remains Open;
- #233–#237 remain Closed / Done.

If any release lifecycle drift is discovered:
BLOCK.

---

# Phase 6 — Merge

Only if every prior gate passes, merge PR #240.

Use the repository's allowed/preferred merge method.

Do not:
- force merge;
- bypass required checks;
- bypass branch protection;
- retarget base;
- alter PR payload;
- amend PR head;
- push tags;
- mutate Release/milestone/issues/Project state.

Record:
- merge method;
- merge timestamp;
- merge commit SHA;
- PR final state.

If the repository allows multiple methods and no explicit policy is discoverable, prefer the method consistent with recent repository practice.

---

# Phase 7 — Post-merge verification

After merge, perform idempotent read-only verification.

## Main branch

Verify:
- PR #240 state = Merged;
- merged head SHA = expected frozen head;
- current `origin/main` contains the merge;
- record resulting `origin/main` SHA.

## Payload

Reconfirm PR #240 still reports exactly two changed paths:

```text
README.md
docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md
```

## Release integrity

Verify again:
- `v1.9.0` still targets `e4958721c9a581efbb2552134c00bc146c73f047`;
- GitHub Release unchanged and published;
- milestone #58 remains Closed 0/13;
- milestone #59 remains Open;
- #233–#237 remain Closed / Done.

## Documentation state

Verify merged `main` now contains:
- accepted README baseline;
- Release 1.9 showcase guide;
- valid README → guide link.

No post-merge correction is authorized here.

---

# Branch deletion

Branch deletion is NOT authorized under this authority.

Leave:

`docs/release-1.9-showcase-readme`

unchanged after merge unless repository automation removes it automatically.

If GitHub auto-deletes it:
record that as platform behavior, not an explicit mutation requested by this authority.

---

# Allowed mutations

## Repository content
None.

## Git
None locally required by this authority.

## GitHub
Exactly one possible mutation:
- merge PR #240.

No other GitHub mutation is authorized.

---

# Acceptance criteria

PASS only if:

1. PR #240 head SHA exactly matches `77fcbc59b01b12626e0b49c09a9fa30bc872116f`;
2. PR remains one commit before merge;
3. exact frozen payload = 2/2 paths;
4. documentation diff matches accepted baseline;
5. technical impact = none;
6. all required checks pass;
7. all required review gates pass;
8. PR is mergeable;
9. release integrity passes before merge;
10. merge succeeds without bypass;
11. post-merge PR state = Merged;
12. post-merge payload still exact 2/2;
13. release tag/Release/milestones/issues remain unchanged.

---

# Required success report

## PR identity
- PR #240
- title
- URL
- base/head
- frozen head SHA
- commit count

## Frozen payload

```text
README.md
docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md
```

`RELEASE 1.9 PR #240 FROZEN PAYLOAD: PASS — 2/2 PATHS`

## Review/checks
- required checks: PASS
- review gate: PASS / NOT REQUIRED
- mergeability: PASS

## Merge
- merge method
- merge commit SHA
- merged timestamp
- final PR state

## Post-merge
- resulting `origin/main` SHA
- README baseline present: PASS
- showcase guide present: PASS
- README → guide link: PASS

## Release integrity
- `v1.9.0` target unchanged
- GitHub Release unchanged
- milestone #58 Closed 0/13
- milestone #59 Open
- #233–#237 unchanged

## Technical impact

`NONE — DOCUMENTATION-ONLY`

## Mutation markers

`RELEASE 1.9 PR #240 REVIEW/CHECKS/MERGE REPOSITORY MUTATIONS: ZERO`

`RELEASE 1.9 PR #240 REVIEW/CHECKS/MERGE GIT MUTATIONS: ZERO`

`RELEASE 1.9 PR #240 REVIEW/CHECKS/MERGE GITHUB MUTATIONS: PR #240 MERGE ONLY`

## Completion

`RELEASE 1.9 PR #240 MERGED — DOCUMENTATION PAYLOAD ACCEPTED ON MAIN`

Terminal:

`RELEASE 1.9 PR #240 REVIEW/CHECKS/MERGE AUTHORITY COMPLETE`

---

# Required blocked report

If any gate fails, report:

- exact failing phase;
- current PR state;
- current head SHA;
- check/review status;
- changed-file count and path set;
- mergeability state;
- whether any mutation occurred;
- minimum next authority needed.

Terminal:

`RELEASE 1.9 PR #240 REVIEW/CHECKS/MERGE AUTHORITY BLOCKED`

Do not emit COMPLETE unless PR #240 is merged and all post-merge integrity checks pass.
