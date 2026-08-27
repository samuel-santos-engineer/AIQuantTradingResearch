# Release 1.9 — PR #238 Review / Checks / Merge Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This is a **narrow PR-review/checks/merge authority** for:

`PR #238 — Release 1.9 — Real-Time Financial Data Visualization`

It assumes the PR already exists and contains the frozen Release 1.9 executable/documentation payload.

This authority may:
- read/review PR #238;
- verify changed-file and commit identity;
- inspect required checks/reviews;
- merge PR #238 only if all binding gates pass;
- read back post-merge Git/GitHub state.

This authority does **not** authorize:
- adding new commits to PR #238;
- implementation/test/doc fixes;
- milestone #58 closure;
- tag creation;
- GitHub Release publication;
- issue/Project mutation;
- follow-up governance PR creation;
- branch deletion unless explicitly required by the canonical merge workflow.

If a defect is found that requires changing the PR contents, STOP and request a separate fix/revalidation authority.

---

# Frozen accepted PR state

Treat as binding unless current read-back contradicts it:

## PR
- PR number: **#238**
- title:
  `Release 1.9 — Real-Time Financial Data Visualization`
- base:
  `main`
- head:
  `release/1.9-real-time-financial-data-visualization`
- state before merge:
  Open / ready for review
- one commit:
  `6b7c2cac8c20e6033666e1dfaf160f629fb7894b`
- commit message:
  `feat: finalize Release 1.9 real-time financial visualization (#233-#237)`
- changed-file set:
  exactly **286 paths**
- changed-file verification:
  paginated files API = 286 paths
  commit paths = 286 paths
  unexpected = 0
  missing = 0

## Accepted technical/security evidence
- build 0 warnings / 0 errors
- .NET 339/339
- Python 17/17
- Streamlit 1.61.1
- `pip check` clean
- WP08/WP09 focused preservation accepted
- architecture/no-bypass accepted
- SQLite schema v4 preserved
- Git-aware + staged-content Gitleaks clean
- residue clean

## Lifecycle
- #233–#237 Closed / Done
- milestone #58 Open, 0 open / 13 closed

Do not reopen technical scope unless current PR state contradicts the frozen payload.

---

# Binding sources

Read completely:

1. `docs/roadmap/release-1.9/RELEASE_1.9_FINALIZATION_PR_GIT_MILESTONE_TAG_RELEASE_CONTRACT_AUTHORITY.md`
2. PR #238.
3. PR #238 commits.
4. PR #238 changed-files list.
5. current branch/PR workflow documentation.
6. current repository merge policy, if explicitly documented.
7. current required GitHub checks/review state.
8. milestone #58 state.
9. current tags/releases only to prove they remain untouched.

Historical PR merge style is evidence only unless the finalization contract explicitly permits using it to resolve merge method.

---

# Phase 0 — Idempotency check

Before anything else, determine PR #238 current state:

## If OPEN
Continue with review/checks/merge.

## If MERGED
Do not attempt to merge again.
Read back:
- merge commit SHA;
- merged timestamp;
- base/head;
- final changed-file count;
- checks/reviews at merge time if available;
- origin/main contains merge result.

Then classify execution as:
`IDEMPOTENT POST-MERGE VERIFICATION`

Do not mutate anything.

## If CLOSED UNMERGED
STOP.

Do not reopen without separate authority.

---

# Phase 1 — PR identity proof

Require exact:

- PR #238;
- repository correct;
- title matches Release 1.9;
- base = `main`;
- head = `release/1.9-real-time-financial-data-visualization`;
- commit `6b7c2cac8c20e6033666e1dfaf160f629fb7894b` present;
- changed files exactly 286 unless current GitHub normalization produces a documented equivalent count.

If commit or payload differs materially:
STOP.

No merge of a drifted PR.

---

# Phase 2 — Changed-file invariant

Use authoritative PR files API or equivalent.

Require:

`PR changed paths == frozen 286-path R1 payload`

Verify:
- no unexpected path;
- no missing path;
- no local-only signing config;
- no `Directory.Build.local.props`;
- no generated binaries/test results/runtime artifacts;
- no secrets/private keys.

If any path mismatch:
STOP.

---

# Phase 3 — Security read-back

Use existing accepted security evidence and current PR content.

If canonical workflow requires a fresh scan, run only approved existing tooling.

Require:
- no secret finding;
- no private-key/certificate secret;
- local signing remains dev-only;
- no executable drift after accepted scan.

Do not install new tools.

---

# Phase 4 — Review/checks discovery

Read actual PR #238 review/check state.

Determine:

- required checks, if any;
- completed checks;
- failed/pending checks;
- required approvals, if any;
- existing reviews;
- branch protection requirements;
- mergeability state;
- merge conflict state.

Do not invent required checks.

If repository has no configured required checks/reviews, state that fact.

---

# Phase 5 — Review gate

If approvals are required:
- require them.

If self-review is disallowed:
- do not manufacture approval.

If no approval requirement exists:
- do not block on an invented review policy.

Any explicit CHANGES_REQUESTED blocks merge.

---

# Phase 6 — Checks gate

Every configured required check must pass.

If any:
- pending;
- failing;
- cancelled;
- stale in a way branch protection rejects;

STOP.

Do not rerun external CI unless canonical tooling/authority explicitly permits it.

Local accepted test evidence does not override a configured failing required check.

---

# Phase 7 — Mergeability gate

Require:
- mergeable;
- no conflict;
- base/head still correct;
- PR not superseded;
- no new commit after accepted frozen SHA unless separately revalidated.

If head SHA changes:
STOP and require a new post-change validation authority.

---

# Phase 8 — Merge-method resolution

Use the binding finalization contract.

The authority MUST NOT invent merge method.

Resolve one exact method from canonical policy:

- merge commit;
- squash;
- rebase.

If finalization contract deliberately leaves merge method unresolved and no canonical repository policy resolves it:
STOP and request a narrow merge-method authority.

Do not choose based solely on personal preference or common GitHub defaults.

---

# Phase 9 — Merge mutation

Only if all gates pass and PR is still Open:

Perform exactly one authorized merge using the resolved method.

Do not:
- edit PR content;
- add commits;
- close issues;
- close milestone;
- tag;
- publish release;
- delete branch unless the canonical merge workflow explicitly authorizes branch deletion in this same action.

Record:
- merge method;
- resulting merge/squash/rebase commit SHA;
- merge timestamp.

If merge fails:
STOP and report GitHub error/state.

---

# Phase 10 — Post-merge read-back

After merge, verify:

- PR #238 = Merged;
- base = main;
- head branch identity preserved/readable;
- merged commit SHA known;
- origin/main contains the Release 1.9 change set;
- #233–#237 remain Closed/Done;
- milestone #58 remains Open, 0 open / 13 closed;
- no tag created;
- no GitHub Release created;
- no follow-up governance PR created by this authority.

Do not close milestone.

---

# Phase 11 — Optional local synchronization

Only if the finalization contract explicitly authorizes read-only fetch/pull synchronization after merge.

If authorized:
- fetch;
- update local main safely only if no unrelated local work is endangered;
- verify local main vs origin/main.

If dirty worktree makes synchronization unsafe:
- skip local sync and report remote authoritative merge state.

Do not reset/stash/clean.

---

# Explicitly forbidden

This authority never authorizes:

- implementation/source/test/doc change;
- staging;
- new commit on PR branch;
- push of new PR content;
- PR body/title mutation unless required solely for a GitHub merge prerequisite explicitly authorized;
- milestone #58 closure;
- tag;
- GitHub Release;
- #233–#237 mutation;
- Project item mutation;
- branch deletion unless merge policy explicitly includes it;
- creation of governance follow-up PR.

---

# Required success report — active merge path

## PR identity
#238, title, base/head, frozen SHA, 286 paths.

## Reviews/checks
Exact required and actual states.

## Mergeability
Result.

## Merge method
Exact canonical method and authority source.

## Merge
Resulting SHA/timestamp.

## Post-merge
PR Merged; origin/main contains result.

## Preservation
#233–#237 unchanged; milestone #58 Open; no tag/Release.

## Mutation markers

`RELEASE 1.9 PR #238 REVIEW/CHECKS REPOSITORY MUTATIONS: ZERO`

`RELEASE 1.9 PR #238 REVIEW/CHECKS GIT MUTATIONS: ZERO`

`RELEASE 1.9 PR #238 REVIEW/CHECKS GITHUB MUTATIONS: PR #238 MERGED; ALL OTHER GITHUB MUTATIONS ZERO`

## Next authority
`PR #238 MERGED — SEPARATE MILESTONE / TAG / RELEASE FINALIZATION AUTHORITY REQUIRED`

Terminal:

`RELEASE 1.9 PR #238 REVIEW / CHECKS / MERGE COMPLETE`

---

# Required success report — idempotent already-merged path

If PR #238 was already merged before this authority ran:

State:

`PR #238 STATE: ALREADY MERGED — IDEMPOTENT POST-MERGE VERIFICATION`

Verify:
- merged SHA/timestamp;
- 286-path payload identity;
- origin/main contains result;
- #233–#237 unchanged;
- milestone #58 Open;
- no tag/Release.

Mutation markers:

`RELEASE 1.9 PR #238 REVIEW/CHECKS REPOSITORY MUTATIONS: ZERO`

`RELEASE 1.9 PR #238 REVIEW/CHECKS GIT MUTATIONS: ZERO`

`RELEASE 1.9 PR #238 REVIEW/CHECKS GITHUB MUTATIONS: ZERO — PR #238 WAS ALREADY MERGED`

Next:
`PR #238 MERGE VERIFIED — SEPARATE MILESTONE / TAG / RELEASE FINALIZATION AUTHORITY REQUIRED`

Terminal:

`RELEASE 1.9 PR #238 REVIEW / CHECKS / MERGE COMPLETE`

---

# Required blocked report

Include:
- PR state;
- identity verification;
- checks/reviews;
- exact blocker;
- whether merge-method authority is missing;
- zero mutation accounting if no merge occurred.

Terminal:

`RELEASE 1.9 PR #238 REVIEW / CHECKS / MERGE BLOCKED`

Do not emit COMPLETE unless PR #238 is authoritatively verified as merged with the frozen Release 1.9 payload and no forbidden lifecycle mutation occurred.
