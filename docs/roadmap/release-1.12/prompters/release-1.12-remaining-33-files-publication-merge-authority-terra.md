# GPT-5.6 Terra — Release 1.12 Remaining Files Publication + Merge Authority

**Selected execution model: GPT-5.6 Terra**

## Prerequisite

Do not mutate unless GPT-5.6 Luna has emitted:

`REMAINING LOCAL FILES — PUBLICATION DESIGNATION: PASS`

and:

`GPT-5.6 TERRA REMAINING-FILES CONTRACT: ONLY REMAINING_33_PUBLICATION_SET MAY BE MUTATED`

The exact Luna-approved literal set must be available in full.

If Luna approved fewer than 33 paths, use the exact approved count and set; never add files merely to reach 33.

## Mission

Publish and merge the exact Luna-approved remaining local files as one dedicated PR, with no unrelated paths.

## Canonical base

Expected `main`:

`e931e714258028b2a6aa942a2b5cda1fc3f6866f`

Fresh reconciliation controls.

## Exact mutation closure

Authorized:
- `CREATE` only for every path in Luna's exact publication set.

Not authorized:
- modification/deletion of any already-tracked path;
- any path outside the Luna set;
- application/source/config/package/schema changes unless literally included and approved by Luna.

## Pre-staging gate

Prove:
- canonical base;
- staging empty;
- every approved path exists locally;
- every approved path is untracked/unpublished;
- no approved path is already present on `main`;
- sensitive-content screening still passes;
- no local file has drifted since Luna designation.

Required:

`REMAINING LOCAL FILES — TERRA PRE-STAGING: PASS`

## Staging

Use literal selective staging only.

Never use:
- `git add .`
- `git add -A`

Stage only the exact approved paths.

Then prove:
- staged path set equals Luna set exactly;
- staged count equals Luna count;
- all staged entries are creates;
- zero unrelated paths;
- `git diff --cached --check` passes.

If Luna approved all 33:

`REMAINING 33 FILES — STAGED PAYLOAD 33/33: PASS`

## Validation

Run:
- `git diff --cached --check`;
- Gitleaks/secret screening on candidate/staged content;
- syntax/readability checks appropriate to PowerShell and Markdown;
- duplicate/root-helper sanity check if root `a10.ps1` is included;
- no Azure/Docker/GHCR/provider execution unless a separate authority explicitly requires it.

Record exact validation evidence.

## Commit and PR

Create a dedicated branch from reconciled base.

Preferred branch:

`chore/release-1.12-publish-local-controls`

Preferred commit:

`Publish Release 1.12 deployment controls and prompters`

Preferred PR title:

`Publish Release 1.12 deployment controls and prompters`

Push and create one non-draft PR to `main`.

Verify PR payload using authoritative Git comparison, not a possibly truncated UI enumeration.

Required:

`REMAINING LOCAL FILES — PR PAYLOAD EXACT: PASS`

If 33 approved:

`REMAINING 33 FILES — PR PAYLOAD 33/33: PASS`

## Merge authority

This authority authorizes merge only after:
- exact PR path-set verification;
- no new commits/drift;
- mergeability confirmed;
- validation still passes.

Merge once and record:
- PR number;
- head SHA;
- merge SHA;
- merge parents;
- merged timestamp.

Required:

`REMAINING LOCAL FILES — PR MERGE: PASS`

## Post-merge verification

After merge:
- fetch;
- synchronize local `main`;
- prove local `main` = `origin/main`, 0/0;
- authoritative parent..merge path comparison;
- exact merged path-set equality;
- every approved path tracked/present;
- zero unauthorized paths;
- staging empty;
- report any unrelated local files still preserved.

If 33 approved:

`REMAINING 33 FILES — MERGED PAYLOAD 33/33: PASS`

## Governance/lifecycle boundary

This publication PR is not itself WP03 implementation unless Luna explicitly classified it as such.

Do not:
- close #262;
- set #262 Done;
- close milestone #63;
- alter Release assignments;
- create tags/releases.

WP03 lifecycle changes remain prohibited until WP03 exact acceptance.

## Mutation accounting

Report exact:
- repository creates;
- branches;
- commits;
- pushes;
- PRs;
- merges;
- fetches;
- local-main synchronization;
- issue mutations;
- Project mutations;
- milestone mutations;
- Azure/Docker/GHCR/provider mutations;
- package/schema mutations.

## Required markers

`REMAINING LOCAL FILES — TERRA BASE RECONCILIATION: PASS`

`REMAINING LOCAL FILES — TERRA PRE-STAGING: PASS`

`REMAINING LOCAL FILES — STAGED PAYLOAD EXACT: PASS`

`REMAINING LOCAL FILES — VALIDATION: PASS`

`REMAINING LOCAL FILES — COMMIT: PASS`

`REMAINING LOCAL FILES — PUSH: PASS`

`REMAINING LOCAL FILES — PR PAYLOAD EXACT: PASS`

`REMAINING LOCAL FILES — PR MERGE: PASS`

`REMAINING LOCAL FILES — MERGED PAYLOAD EXACT: PASS`

`REMAINING LOCAL FILES — POST-MERGE CLEANLINESS: PASS`

`REMAINING LOCAL FILES — MUTATION AUDIT: PASS`

If all 33 were approved:

`REMAINING 33 FILES — PUBLICATION & MERGE: PASS`

Terminal:

`REMAINING LOCAL FILES — TERRA PUBLICATION + MERGE AUTHORITY COMPLETE`

Blocked:

`REMAINING LOCAL FILES — TERRA PUBLICATION + MERGE AUTHORITY BLOCKED`
