# Release 1.9 — WP12 Closure / PR-Readiness / Git-GitHub Lifecycle Contract Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow documentation-only governance contract authority** for:

`WP12 — GitHub issue #237`

Its sole purpose is to define the exact Release 1.9 closure / PR-readiness / Git / GitHub lifecycle semantics that are currently missing.

No repository implementation mutation.
No staging.
No commit.
No branch creation.
No push.
No PR creation/update/merge.
No #237 lifecycle mutation.
No milestone mutation.
No tag.
No GitHub Release.
No WP13+ work.

This pass may create only one authority artifact.

---

# Verified entry state

Treat as accepted unless current read-back disproves it:

## Git
- branch: `main`
- `main == origin/main`
- HEAD:
  `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`
- ahead/behind: `0/0`
- staged files: `0`
- dirty worktree: **269 entries**
  - 25 tracked modifications
  - 244 untracked paths
- `Directory.Build.local.props` is ignored and local-only.

## GitHub
- #233 Closed / Done
- #234 Closed / Done
- #235 Closed / Done
- #236 Closed / Done
- #237 Open / Backlog
- unique #237 Project item:
  `PVTI_lAHOCAzBgs4BfsiAzg33jmA`
- #237 metadata:
  - Release 1.9
  - Priority P1
  - Area Engineering
- milestone #58 Open
- latest accepted milestone count:
  **1 open / 12 closed**

## Technical predecessor
WP11 completed as validation-only:
- repository mutation zero
- build 0 warnings / 0 errors
- .NET 339/339
- Python 17/17
- Streamlit 1.61.1
- `pip check` clean
- SQLite persistence schema v4
- architecture/security/docs/residue accepted.

WP12 must not reopen these semantics.

---

# Canonical sources to read completely

Before creating the artifact, read:

1. issue #237;
2. Release 1.9 definition;
3. Release 1.9 execution plan;
4. Release 1.9 file manifest;
5. WP11 full-integration/release-acceptance contract;
6. WP10 documentation contract;
7. WP09 permanent integration contract;
8. current repository branch/PR/contribution workflow docs;
9. current roadmap;
10. any prior accepted Release 1.9 prompt/authority that mentions commit, PR, merge, closure, or release readiness;
11. existing open/closed PRs for Release 1.9 only as factual precedent, not as authority;
12. `.gitignore` and local signing documentation;
13. current full Git status/diff inventory.

Do not infer lifecycle from generic GitHub practice.

---

# Objective

Create one binding WP12 contract that defines exactly:

1. the Release 1.9 intended change set;
2. the exclusion set;
3. classification rules for all dirty paths;
4. handling of mixed/ambiguous files;
5. whether WP12 authorizes staging;
6. exact staging boundary;
7. whether WP12 authorizes commit;
8. commit grouping/message rules;
9. whether branch creation is required;
10. exact branch/base/head semantics;
11. whether push is authorized;
12. whether PR readiness is evidence-only or actual PR creation;
13. exact PR title/body/linkage if PR creation is authorized;
14. whether PR update is authorized;
15. whether PR merge is authorized;
16. required validation/security/residue before each mutation stage;
17. exact #237 Done/Closed timing;
18. milestone #58 boundary;
19. tag/GitHub Release boundary;
20. final expected Git/GitHub state;
21. exact mutation accounting.

The artifact must be executable by a later Terra pass without improvisation.

---

# Canonical output artifact

Create exactly:

`docs/roadmap/release-1.9/RELEASE_1.9_WP12_CLOSURE_PR_READINESS_GIT_GITHUB_LIFECYCLE_CONTRACT_AUTHORITY.md`

No other file may change under this Luna pass.

---

# Section 1 — WP12 role

Determine from canonical evidence whether WP12 is:

## A. Readiness-only
WP12 proves the Release 1.9 change set is ready for PR, but does not stage/commit/push/create PR.

## B. Git-preparation
WP12 stages/commits and possibly branches/pushes, but does not create/merge PR.

## C. PR-creation
WP12 stages/commits/branches/pushes and creates or updates a PR, but does not merge.

## D. Full PR lifecycle
WP12 includes merge and/or release closure actions.

Choose exactly one.

If canonical authority does not support one unambiguously:
BLOCK.

Do not choose a more permissive role than required.

---

# Section 2 — Change-set inventory contract

The artifact MUST define a classification model for every one of the current 269 dirty entries.

Required classes:

## R1 — Release 1.9 intended change
A path whose diff/new file is directly attributable to accepted Release 1.9 WP01–WP12 work and belongs in the eventual Release 1.9 change set.

## R2 — Pre-existing unrelated user work
Must remain untouched and excluded.

## R3 — Local-only development configuration
Examples:
- `Directory.Build.local.props`
- local signing configuration
- certificates/private-key material
- machine-specific files.

Must never be staged/committed.

## R4 — Generated/test/runtime evidence
Examples:
- `bin/`
- `obj/`
- TRX/test results
- temp runtime roots
- handoff files
- SQLite test DBs/sidecars
- generated logs.

Must be excluded and cleaned only if factually harness-owned and safe.

## R5 — Ambiguous/mixed
Path contains both intended Release 1.9 changes and unrelated user changes, or origin cannot be proven.

Must block whole-file staging unless exact hunk-level inclusion authority is provided.

The artifact must define how to establish R1 attribution:
- accepted authority path;
- originating WP;
- diff content;
- manifest/path ownership;
- predecessor evidence.

Title/name similarity alone is insufficient.

---

# Section 3 — Exact Release 1.9 inclusion manifest

The Luna pass must inspect the actual dirty worktree and produce a concrete path-level table:

`path → class → originating WP → include/exclude → evidence`

This table must cover **all tracked modifications** and all untracked top-level/relevant paths needed to classify the 244 untracked entries.

It may group clearly generated descendants by governed directory/pattern if exact expansion is impractical, but:
- every included R1 path must be named exactly;
- every ambiguous path must be named exactly;
- local-only exclusions must be explicit.

The contract must define the **exact set permitted for later staging**.

No wildcard staging like:
`git add .`
`git add -A`

unless the final include manifest and exclusions make it provably safe; preferred rule is exact-path staging.

---

# Section 4 — Mixed-file rule

If an authorized Release 1.9 file contains unrelated user hunks:

Preferred contract:
- do not stage the whole file;
- use hunk-level staging only if:
  1. canonical tooling/workflow permits it;
  2. intended hunks are independently attributable;
  3. no later edit/rewrite is required.

If hunk-level staging authority is not accepted:
- BLOCK and require local diff reconciliation.

Never overwrite or discard unrelated hunks.

---

# Section 5 — Local signing/security exclusions

Must explicitly exclude:

- `Directory.Build.local.props`
- PFX/P12/PEM private-key files
- certificate exports containing private keys
- passwords/secrets
- machine-specific thumbprint/config files if local-only
- build outputs signed with local certificate
- SDK-local caches.

The committed script/documentation supporting local signing may be included only if already accepted Release 1.9 work and part of R1.

Use terminology:
`local-development Authenticode signing for Windows Smart App Control compatibility`

Never call it a bypass.

---

# Section 6 — Security preflight contract

Before any later staging/commit/push/PR action, require:

1. tracked/non-ignored secret scan using existing approved tooling;
2. inspect all intended new files;
3. verify local signing secrets excluded;
4. verify generated binaries excluded;
5. inspect staged diff after staging;
6. run secret scan against staged content if supported;
7. no credentials/tokens/private keys.

Define exact accepted command/tool from repository evidence.

Do not introduce a new package/tool.

A security failure blocks all Git mutation.

---

# Section 7 — Technical readiness contract

Define whether WP12 re-runs technical acceptance or inherits WP11.

If re-run is required, exact expected gates:

## Build
0 warnings / 0 errors.

## .NET
- Domain 11/11
- Application 125/125
- Infrastructure 182/182
- Architecture 21/21
- total 339/339.

## Python
17/17.

## Environment
- Streamlit 1.61.1
- `pip check` clean.

## Schema
v4 preserved.

If inherited evidence is acceptable, define freshness/read-back rules.

No test-count changes under WP12 unless #237 explicitly requires tests.

---

# Section 8 — Residue contract

Before staging/commit/PR readiness, require:

Zero owned:
- Worker;
- testhost;
- Python;
- Streamlit;
- listeners;
- harness runtime roots;
- handoff temp siblings;
- test DB/WAL/SHM/journal residue.

Allowed retained:
- standard test-result artifacts only if repository-ignore and PR rules permit them.

Define exact cleanup ownership.
No global cleanup.

---

# Section 9 — Staging authority

The contract MUST say either:

## STAGING-NOT-AUTHORIZED
WP12 ends at readiness evidence.

or

## STAGING-AUTHORIZED
Later Terra may stage only the exact R1 include manifest.

If authorized:
- exact path staging only;
- no `git add .`;
- no `git add -A`;
- verify staged path list against manifest;
- inspect staged diff completely;
- re-run secrets check.

If any staged path is outside R1:
STOP and unstage only that path if safe; do not alter working copy.

---

# Section 10 — Commit authority

The contract MUST state:

- whether commit is authorized;
- exact commit grouping;
- exact message or required message pattern;
- whether one commit or multiple commits;
- whether docs/tests/source are split or consolidated;
- whether commit must reference #237/Release 1.9.

If canonical sources do not define commit structure, prefer the **minimum single Release 1.9 commit** only if repository workflow supports it; otherwise BLOCK rather than inventing style.

No amend/rebase/squash unless explicitly authorized.

---

# Section 11 — Branch authority

Define exactly:

- whether later Terra remains on `main` for staging/commit;
- whether a fresh branch must be created before commit;
- exact branch name or naming pattern;
- base commit;
- whether branch must be created from current `main`;
- how dirty unrelated work is preserved.

If branch creation with current dirty worktree is unsafe:
- the contract must specify a safe sequencing rule or block.

Do not assume feature-branch practice.

---

# Section 12 — Push authority

State explicitly:

- push authorized or not;
- exact remote;
- exact branch;
- upstream tracking;
- force-push prohibited unless separately explicit.

If PR creation is required, push authority must be explicit.

---

# Section 13 — PR readiness vs creation

Choose one exact state:

## PR-READY-ONLY
Later Terra produces:
- final change-set inventory;
- staged/commit evidence if authorized;
- tests/security/residue evidence;
- proposed PR title/body;
but creates no PR.

## PR-CREATE
Later Terra creates one exact PR.

If PR creation is authorized define:

### Base
Exact branch, likely `main` only if canonical.

### Head
Exact branch.

### Title
Exact title or pattern.

### Body
Must include:
- Release 1.9 summary;
- included WPs;
- test evidence;
- schema v4;
- simulated-data/non-live warning where appropriate;
- security/no-bypass;
- residue;
- issue linkage/closing syntax only if canonical.

### Labels/reviewers
Only if explicitly required.

### Draft
Specify draft vs ready-for-review.

No merge unless separately authorized.

---

# Section 14 — PR update/merge authority

State independently:

- existing PR update authorized? yes/no
- PR merge authorized? yes/no
- merge method if authorized
- required checks/reviews if known
- post-merge read-back.

If repository lacks explicit merge authority, default:
**merge NOT authorized**.

Do not infer from #237 title.

---

# Section 15 — #237 lifecycle timing

The artifact must define exactly when #237 transitions to Done/Closed.

Possible valid models:

## L1 — Readiness complete
#237 closes when readiness evidence is complete, even before PR creation.

## L2 — PR created
#237 closes only after authorized PR exists.

## L3 — PR merged
#237 closes only after merge.

Select exactly one from canonical evidence.

Then define ordering:

1. technical/security/residue passes;
2. Git/PR action as required;
3. #237 Project Status → Done;
4. read back;
5. #237 issue close;
6. read back.

If Project automation closes issue, explicit close is idempotent.

No #237 mutation before its completion condition.

---

# Section 16 — Milestone #58 authority

State exactly one:

## M0 — milestone closure NOT authorized
Expected final WP12 state:
- #237 Closed/Done if WP12 completion condition met;
- milestone #58 remains Open with 0 open issues;
- milestone closure requires separate release-finalization authority.

## M1 — milestone closure authorized by WP12
Only if canonical #237/release authority explicitly says so.

If M1:
- verify all milestone issues closed;
- verify counts;
- close milestone;
- read back.

Do not choose M1 merely because #237 is the last open issue.

---

# Section 17 — Tag / GitHub Release authority

State independently:

- Git tag authorized? yes/no
- tag name/version if yes
- annotated/lightweight if yes
- GitHub Release authorized? yes/no
- title/body/assets if yes.

Preferred default without explicit authority:
- tag NOT authorized
- GitHub Release NOT authorized.

---

# Section 18 — Release notes/changelog authority

Determine whether WP12 may modify:
- changelog;
- release notes;
- roadmap;
- release summary docs.

If not explicitly in the four/other canonical paths:
do not authorize.

If required, list exact paths and content.

---

# Section 19 — Final Git state contract

Define exact expected end-state for later Terra, for the chosen WP12 role.

Examples:

## Readiness-only
- main unchanged;
- no staged files created by WP12;
- no commit;
- no branch;
- no push;
- no PR.

## Commit/PR creation
- exact branch;
- exact commit SHA relationship;
- main unchanged locally/remotely;
- head pushed;
- PR open;
- unrelated dirty work preserved/excluded.

The contract must be explicit.

---

# Section 20 — Final GitHub state contract

Define exact expected final state:

At minimum:
- #233–#236 unchanged Closed/Done;
- #237 state according to lifecycle model;
- #237 Project item status;
- milestone #58 state;
- PR state if any;
- tag/release state if any.

No Project item creation/deletion.

---

# Section 21 — Mutation accounting contract

Later Terra must report separately:

## Repository mutations
Exact file changes under WP12, if any.

## Git mutations
Exact:
- staging;
- commit;
- branch;
- push.

## GitHub mutations
Exact:
- PR create/update/merge;
- #237 status/close;
- milestone;
- tag/release if any.

The contract must prohibit vague summaries.

---

# Section 22 — Acceptance matrix

The authority artifact must include a concrete table:

| ID | Gate |
|---|---|
| CSET | exact change-set classification |
| EXCL | exclusions/local-only/generated |
| SEC | secrets/signing security |
| TECH | technical readiness |
| RES | residue |
| STAGE | staging rules |
| COMMIT | commit rules |
| BRANCH | branch rules |
| PUSH | push rules |
| PR | PR readiness/create rules |
| LIFE | #237 lifecycle timing |
| MILESTONE | milestone boundary |
| RELEASE | tag/release boundary |
| PRESERVE | unrelated work and #233–#236 preservation |

For each:
- exact proof;
- pass condition;
- block condition.

---

# Section 23 — Stop conditions

Create no artifact if:

- #237 scope conflicts with execution plan;
- change-set inclusion cannot be classified without modifying user work;
- canonical Git/PR workflow is contradictory;
- #237 lifecycle timing cannot be determined;
- milestone/tag/release ownership is contradictory;
- branch/commit/PR actions appear required but lack authority.

Do not invent workflow.

---

# Mutation boundary for this Luna pass

Allowed:
- create exactly:
  `docs/roadmap/release-1.9/RELEASE_1.9_WP12_CLOSURE_PR_READINESS_GIT_GITHUB_LIFECYCLE_CONTRACT_AUTHORITY.md`

Everything else:
- repository implementation mutations: ZERO
- Git mutations: ZERO
- GitHub mutations: ZERO.

---

# Required completion report

## Artifact
Exact path.

## WP12 role
A/B/C/D.

## Change-set
Exact R1 include manifest and exclusion classification.

## Mixed files
Exact policy/results.

## Security
Exact preflight.

## Technical/residue
Exact gates.

## Git
Staging/commit/branch/push authority.

## PR
Readiness/create/update/merge authority.

## #237
Exact lifecycle timing.

## Milestone
M0 or M1.

## Tag/release
Exact authority.

## Final states
Git and GitHub.

## Mutation statement

`WP12 CLOSURE/PR-READINESS CONTRACT AUTHORITY MUTATIONS: ZERO repository/Git/GitHub mutations; one authorized contract artifact created`

## Next step

`WP12 CLOSURE/PR-READINESS CONTRACT DEFINED — FRESH GPT-5.6 TERRA EXECUTION/COMPLETION AUTHORITY REQUIRED`

---

# Terminal markers

Success:

`RELEASE 1.9 WP12 CLOSURE / PR-READINESS / GIT-GITHUB LIFECYCLE CONTRACT AUTHORITY COMPLETE`

Blocked:

`RELEASE 1.9 WP12 CLOSURE / PR-READINESS / GIT-GITHUB LIFECYCLE CONTRACT AUTHORITY BLOCKED`

Do not emit COMPLETE unless the Release 1.9 change set, exclusions, Git actions, PR semantics, #237 timing, milestone boundary, and release/tag boundaries are all explicit and implementation-ready.
