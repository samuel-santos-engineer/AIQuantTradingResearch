# Release 1.9 — Finalization / PR-Git / Milestone-Tag-Release Contract Authority

## Model
Use **GPT-5.6 Luna**.

## Purpose
Create the binding post-WP12 contract for Release 1.9 finalization.

All Release 1.9 work packages WP01–WP12 are complete. This authority does NOT implement, stage, commit, push, create/merge a PR, close a milestone, create a tag, or publish a GitHub Release.

Its only permitted repository mutation is creation of the single contract artifact named below.

---

# Frozen accepted state

Treat these as inherited unless current read-only evidence disproves them.

## Work packages / GitHub
- #233–#237: Closed.
- #237 Project item `PVTI_lAHOCAzBgs4BfsiAzg33jmA`: Done.
- #237 Release 1.9 / P1 / Engineering preserved.
- milestone #58: **Open**, 0 open / 13 closed.

## WP12 PR readiness
- `WP12 PR READINESS: PASS`.
- hypothetical R1 PR include set: **286 non-ignored paths**.
- inventory at readiness: 28 tracked + 258 untracked.
- R2 none.
- R3 includes ignored local-only `Directory.Build.local.props`.
- R4 generated/runtime/test outputs excluded.
- R5 none.
- exact-path staging required by the WP12 contract.
- staging/commit/branch/push/PR/merge/tag/Release/milestone closure were not authorized by WP12.

## Technical release baseline
- build: 0 warnings / 0 errors.
- .NET: 339/339.
  - Domain 11/11.
  - Application 125/125.
  - Infrastructure 182/182.
  - Architecture 21/21.
- Python: 17/17.
- Streamlit 1.61.1.
- `pip check` clean.
- WP08 lifecycle: 18/18.
- WP09 integration: 4/4.
- WP09 architecture/no-bypass: 8/8.
- SQLite persistence schema v4.
- schema suites: 23/23.
- Git-aware Gitleaks: clean.
- zero owned process/listener residue.

## Late accepted fix
Release 1.9 includes the revalidated Windows atomic-replacement fix:
- `VisualizationReadModelFilePublisher.cs`
- `VisualizationReadModelFilePublisherTests.cs`

The full predecessor surface was revalidated after that fix.

## Git
Latest accepted evidence:
- `main == origin/main`
- predecessor HEAD begins `3a02f035`
- ahead/behind 0/0
- staged paths 0
- dirty worktree intentionally contains the Release 1.9 R1 set plus ignored/local/generated exclusions.

---

# Binding sources to read

Read completely before writing the contract:

1. Release 1.9 definition.
2. Release 1.9 execution plan.
3. Release 1.9 file manifest.
4. WP12 closure/PR-readiness/Git-GitHub lifecycle contract.
5. completed WP12 PR-readiness evidence.
6. completed WP12 #237 lifecycle evidence.
7. WP11 acceptance contract.
8. WP10 documentation/developer-alignment contract.
9. repository contribution/branch/PR workflow documentation.
10. README and roadmap.
11. `.gitignore`.
12. local Smart App Control development-signing documentation.
13. current Git status and full R1 inventory.
14. relevant historical repository PRs/merge conventions only as factual precedent.
15. GitHub milestone #58.
16. current tags/releases.
17. any canonical source specifying version/tag naming or GitHub Release conventions.

Do not treat historical convention as authority when canonical Release 1.9 documents conflict or remain silent.

---

# Output artifact

Create exactly:

`docs/roadmap/release-1.9/RELEASE_1.9_FINALIZATION_PR_GIT_MILESTONE_TAG_RELEASE_CONTRACT_AUTHORITY.md`

No other repository file may change under this Luna pass.

---

# Primary objective

The contract must turn the already-proven PR-ready state into an executable, mutation-safe release-finalization sequence.

It must resolve independently:

1. staging;
2. branch creation;
3. commit structure;
4. push;
5. PR creation;
6. PR update;
7. PR review/check requirements;
8. PR merge;
9. merge method;
10. post-merge synchronization;
11. milestone #58 closure;
12. Git tag creation;
13. tag push;
14. GitHub Release publication;
15. release notes/changelog requirements;
16. final repository/Git/GitHub state.

No later Terra pass may need to invent any material lifecycle semantic.

---

# 1 — Finalization model

Choose exactly one canonical finalization model:

## F1 — PR only
Stage/commit/branch/push/create PR; merge/milestone/tag/Release deferred.

## F2 — PR through merge
Stage/commit/branch/push/create PR/merge; milestone/tag/Release deferred.

## F3 — PR + merge + milestone closure
Tag/Release deferred.

## F4 — Full release finalization
Stage/commit/branch/push/PR/merge/milestone/tag/GitHub Release as explicitly governed.

## F-SPLIT
Canonical evidence requires multiple separate authorities.

## F-UNRESOLVED
Canonical evidence is insufficient or contradictory.

Select the least permissive model that satisfies canonical requirements.

---

# 2 — R1 change-set freeze

The contract must inherit the WP12 exact R1 manifest rather than casually reclassifying it.

Read current worktree and verify whether the 286-path R1 set is still exact.

Produce:

`exact path → originating WP/authority → tracked/untracked → final include`

Rules:
- every included path exact;
- no `git add .`;
- no `git add -A`;
- no wildcard that can capture R2/R3/R4;
- R5 must remain empty;
- any new unexplained non-ignored path after WP12 blocks finalization;
- `Directory.Build.local.props` excluded;
- local certificate/private-key material excluded;
- generated binaries/test results/runtime files excluded.

If current R1 differs only because this Luna contract artifact itself is newly created, define explicitly whether that contract artifact is:
- included in the Release 1.9 finalization commit; or
- governance-only and excluded.

Do not leave this ambiguous.

---

# 3 — Staging contract

Define exact staging procedure.

Required default:
- stage only exact R1 paths authorized for final release inclusion;
- inspect staged path list;
- inspect staged diff;
- prove no R2/R3/R4/R5;
- run staged-content security check if supported.

Explicitly prohibit:
- `git add .`
- `git add -A`
unless canonical evidence proves a safer equivalent and all exclusions are enforced.

Define rollback procedure for accidental staging:
- unstage only unintended paths;
- never discard working-copy changes.

---

# 4 — Dirty-worktree preservation

Because Release 1.9 developed in a large dirty worktree, the contract must define what happens to any excluded local/user state during branch/commit operations.

Must preserve:
- ignored `Directory.Build.local.props`;
- unrelated/local-only files;
- generated evidence that is allowed to remain ignored;
- user work not in R1.

No `git clean`, destructive reset, checkout overwrite, or blanket stash unless explicitly and safely authorized.

---

# 5 — Branch contract

Resolve exactly:
- whether commit occurs on `main` or a release/feature branch;
- exact branch name or canonical pattern;
- base commit;
- whether branch is created before or after staging;
- remote tracking;
- expected local `main` state.

If repository workflow requires a PR into `main`, prefer a dedicated branch unless canonical evidence explicitly allows direct-main commits.

Do not invent branch names if naming rules exist.

---

# 6 — Commit contract

Define:
- one commit vs multiple commits;
- exact grouping;
- exact message(s) or message pattern(s);
- issue/milestone/release references;
- whether the 286-path release change set is one cohesive commit or separated by source/tests/docs/governance.

No amend/rebase/squash unless explicitly authorized.

If historical Release PRs use squash merge, distinguish local commit structure from final merge commit.

---

# 7 — Pre-commit acceptance gate

Determine whether full technical validation must be rerun immediately before commit or whether the recent WP12/post-fix evidence is fresh enough.

If rerun required, exact gates remain:
- build 0/0;
- .NET 339/339;
- Python 17/17;
- Streamlit 1.61.1;
- `pip check` clean;
- schema v4;
- security clean;
- residue clean.

Define freshness criteria.

Any post-readiness code/test change requires full revalidation.

---

# 8 — Security gate

Before commit/push/PR:
- Git-aware Gitleaks or repository-approved equivalent;
- intended/staged content only where possible;
- no credentials/tokens/private keys;
- no PFX/P12/PEM private key;
- local signing configuration excluded;
- no locally signed binaries;
- inspect all untracked R1 additions.

Do not install or upgrade security tooling.

---

# 9 — Push contract

Resolve:
- push authorized?
- exact remote;
- exact branch;
- upstream creation;
- normal push only;
- force push prohibited unless explicitly canonical.

Define expected local/remote branch relationship after push.

---

# 10 — PR creation contract

If authorized, define exactly:

## Base
Exact base branch.

## Head
Exact branch.

## Title
Exact title or canonical pattern.

## Body
Must define required content, including as applicable:
- Release 1.9 summary;
- WP01–WP12 completion;
- technical evidence;
- .NET 339/339;
- Python 17/17;
- schema v4;
- Windows atomic-replacement fix;
- simulated/replay/non-live warning;
- security/no-bypass;
- residue;
- documentation alignment;
- issue/milestone linkage.

Do not use closing syntax for already-closed #233–#237 unless canonical workflow requires references.

## State
Draft or ready-for-review.

## Labels/reviewers
Only if canonical.

---

# 11 — PR checks and review contract

Define:
- required CI checks;
- local evidence vs GitHub checks;
- required approvals;
- self-approval constraints if relevant;
- whether administrator merge is permitted;
- what failure blocks merge.

If repository has no automated checks, state that fact rather than inventing them.

---

# 12 — PR update contract

If PR checks reveal a defect:
- define whether fixes are authorized under the same Terra finalization pass;
- preferred default: implementation fixes are NOT authorized and require a new narrow fix/revalidation authority.

Define whether metadata-only PR edits are allowed.

---

# 13 — Merge contract

If merge is authorized, define:
- exact merge method: merge commit / squash / rebase;
- required checks/reviews;
- expected resulting `main`;
- whether source branch deletion is authorized;
- post-merge fetch/pull synchronization;
- verification that `origin/main` contains the Release 1.9 change set.

No merge-method improvisation.

---

# 14 — Milestone #58 closure contract

Milestone currently:
- Open;
- 0 open / 13 closed.

Determine whether milestone closure occurs:
- before PR merge;
- after PR merge;
- after tag;
- after GitHub Release;
- or under a separate authority.

If authorized:
- verify all 13 issues closed;
- verify correct milestone;
- close #58;
- read back Closed.

Do not infer closure solely from 0 open issues.

---

# 15 — Version/tag contract

Determine from canonical versioning:
- whether Release 1.9 requires a Git tag;
- exact tag string, e.g. `v1.9.0` only if canonical;
- commit to tag;
- annotated vs lightweight;
- tag message;
- whether tag is signed;
- whether tag push is authorized.

Never invent version/tag format.

Tag must point to the canonical post-merge release commit if that is repository policy.

---

# 16 — GitHub Release contract

Determine:
- whether a GitHub Release is required;
- whether it is created from the tag;
- exact title;
- release notes source;
- draft/prerelease status;
- assets, if any.

Do not attach local build artifacts unless canonical.

If no GitHub Release is required, state `NOT AUTHORIZED / NOT REQUIRED`.

---

# 17 — Release notes/changelog/roadmap

Resolve whether finalization requires any repository documentation mutation after the already PR-ready R1 set.

Possible paths:
- changelog;
- roadmap;
- release notes;
- README.

If required but not already in R1:
BLOCK rather than silently expanding scope, unless this Luna contract explicitly establishes the exact new path/content authority.

Prefer no new content if accepted WP10/WP12 documentation already satisfies release closure.

---

# 18 — Post-merge technical verification

Define whether post-merge validation is required.

If required:
- exact subset/full matrix;
- whether it runs on clean `main`;
- residue/security expectations.

Do not change test counts.

---

# 19 — Final Git state

Define exact expected state after the authorized finalization model:
- current branch;
- local `main`;
- origin/main;
- ahead/behind;
- source branch existence/deletion;
- staged files;
- working tree expectations;
- treatment of ignored local signing configuration.

Distinguish committed R1 changes from intentionally retained ignored/local state.

---

# 20 — Final GitHub state

Define exact expected state:
- #233–#237 Closed/Done;
- milestone #58 Open or Closed according to model;
- PR state;
- tag state;
- GitHub Release state;
- no Project item creation/deletion;
- Release/Priority/Area metadata preserved.

---

# 21 — Mutation sequencing

Provide a numbered executable sequence with hard gates.

Example structure, only if canonical:
1. read-back/safety snapshot;
2. R1 manifest verification;
3. technical/security/residue freshness gate;
4. branch;
5. exact-path staging;
6. staged audit;
7. commit;
8. push;
9. PR create;
10. checks/review;
11. merge;
12. sync main;
13. milestone close;
14. tag;
15. push tag;
16. GitHub Release;
17. final read-back.

Every step must specify:
- authorized mutation;
- prerequisite;
- expected result;
- stop condition.

---

# 22 — Idempotency/recovery

Define safe handling for:
- branch already exists;
- paths already staged;
- commit already created;
- push already exists;
- PR already exists;
- PR already merged;
- milestone already closed;
- tag already exists;
- Release already exists.

Never duplicate lifecycle objects.

If existing object differs materially from contract:
STOP.

---

# 23 — Failure boundaries

The contract must require STOP if:
- R1 drift;
- R5 appears;
- security finding;
- test regression;
- unexpected staged path;
- branch base mismatch;
- remote divergence;
- PR base/head mismatch;
- failed required check;
- merge conflict;
- milestone identity/count mismatch;
- existing tag points elsewhere;
- existing Release metadata conflicts;
- any implementation fix becomes necessary.

Do not repair production/test behavior under finalization authority.

---

# 24 — Mutation accounting

The future Terra pass must report separately:

## Repository
Exact repository file mutations, expected normally zero during finalization beyond already-existing R1 content.

## Git
- branch;
- staging;
- commits;
- push;
- merge-related local sync;
- tag.

## GitHub
- PR;
- milestone;
- Release;
- any branch deletion if remote action.

Issue/Project mutations should be zero because #233–#237 are already complete.

---

# 25 — Acceptance matrix

Create a table with at least:

| ID | Gate |
|---|---|
| R1 | exact final change set |
| EXCL | local/generated exclusions |
| FRESH | validation freshness |
| SEC | security |
| RES | residue |
| BRANCH | branch |
| STAGE | exact staging |
| COMMIT | commit |
| PUSH | push |
| PR | PR creation |
| CHECKS | checks/review |
| MERGE | merge |
| MILESTONE | #58 |
| TAG | version tag |
| RELEASE | GitHub Release |
| FINAL-GIT | final Git |
| FINAL-GH | final GitHub |
| IDEMP | idempotency/recovery |

For each:
- proof;
- pass condition;
- stop condition.

---

# 26 — This Luna pass mutation boundary

Allowed repository mutation:
- create exactly:
  `docs/roadmap/release-1.9/RELEASE_1.9_FINALIZATION_PR_GIT_MILESTONE_TAG_RELEASE_CONTRACT_AUTHORITY.md`

Forbidden:
- all implementation/test/Python/schema/package edits;
- staging;
- commit;
- branch;
- push;
- PR mutation;
- milestone mutation;
- tag;
- GitHub Release;
- issue/Project mutation.

---

# 27 — Required completion report

Report:

## Artifact
Exact path.

## Finalization model
F1/F2/F3/F4/F-SPLIT.

## R1
Final exact count and treatment of this new contract artifact.

## Git plan
Branch/staging/commit/push.

## PR plan
Create/check/review/merge.

## Milestone
Exact closure timing/authority.

## Tag
Exact version/tag policy.

## GitHub Release
Exact publication policy.

## Validation
Freshness and rerun requirements.

## Security/residue
Exact gates.

## Final state
Git and GitHub.

## Mutation statement
`RELEASE 1.9 FINALIZATION CONTRACT AUTHORITY MUTATIONS: ZERO Git/GitHub mutations; one authorized contract artifact created`

## Next authority
If implementation-ready:
`RELEASE 1.9 FINALIZATION CONTRACT DEFINED — FRESH GPT-5.6 TERRA FINALIZATION EXECUTION AUTHORITY REQUIRED`

Terminal:
`RELEASE 1.9 FINALIZATION / PR-GIT / MILESTONE-TAG-RELEASE CONTRACT AUTHORITY COMPLETE`

---

# 28 — Required blocked report

If F-UNRESOLVED or a material contradiction prevents an executable contract:
- create no artifact unless the contradiction itself can be safely recorded under the sole artifact;
- identify exact conflicting sources;
- identify minimum reconciliation authority;
- Git/GitHub mutations zero.

Terminal:
`RELEASE 1.9 FINALIZATION / PR-GIT / MILESTONE-TAG-RELEASE CONTRACT AUTHORITY BLOCKED`

Do not emit COMPLETE unless a later Terra pass can execute finalization without inventing branch, commit, PR, merge, milestone, tag, or Release semantics.
