# GPT-5.6 Terra — Release 1.12 73-File Relocation Publication + Merge Authority

**Selected execution model: GPT-5.6 Terra**

## Authority prerequisite

Do not mutate unless the Luna relocation amendment has emitted all of:

`73-FILE RELOCATION — RECONCILIATION: PASS`

`73-FILE RELOCATION — CONTENT EQUALITY 73/73: PASS`

`73-FILE RELOCATION — LITERAL MUTATION SET 146/146: PASS`

`GPT-5.6 TERRA RELOCATION CONTRACT: ONLY R111_RELOCATION_MUTATION_SET MAY BE MUTATED`

The complete literal Luna-approved `R111_RELOCATION_DELETE_SET` and `R111_RELOCATION_CREATE_SET` must be available and must be embedded/used exactly.

## Mission

Publish and merge the intentional repository refactor consisting only of:

- 73 tracked historical qualification-script deletions;
- 73 corresponding relocated script additions under the Luna-approved destination paths.

This is a path-only relocation. No content changes are authorized.

## Canonical base

Expected pre-publication `main`:

`0ffca425f485fdea23e4a2f88ee2c5968b6046f0`

Fresh Git reconciliation controls.

## Absolute mutation closure

Authorized repository mutations:

- every path in `R111_RELOCATION_DELETE_SET`: `DELETE`;
- every path in `R111_RELOCATION_CREATE_SET`: `CREATE`.

Nothing else.

Expected:

`DELETE_PATH_COUNT=73`

`CREATE_PATH_COUNT=73`

`TOTAL_MUTATION_PATH_COUNT=146`

No modification of file contents is authorized.

If Git detects a content change rather than a pure move/equivalent delete+add, BLOCK.

## Excluded state

Preserve and do not stage:

- `eng/azure-cli/r1.12-deployment/wp02-deployment/a1.ps1` through `a10.ps1`;
- root `a10.ps1`;
- `docs/roadmap/release-1.12/prompters/*.md`;
- any unrelated untracked/local artifact;
- any Release 1.12 WP02 files;
- any application/source/config/test file.

## Pre-staging verification

Before staging:

1. verify `main`/`origin/main`;
2. verify Luna-approved 73 delete paths are exactly the tracked deletions;
3. verify Luna-approved 73 create paths are exactly the intended relocated untracked files;
4. independently re-check old/new content equality;
5. prove unrelated local artifacts are excluded;
6. run secret screening/Gitleaks over candidate content.

Required:

`73-FILE RELOCATION — TERRA PRE-STAGING 146/146: PASS`

## Staging

Use literal selective staging only.

Never use:

`git add .`

`git add -A`

Stage only the Luna-approved 146 paths.

After staging prove:

- staged deleted paths = exact 73 delete set;
- staged added paths = exact 73 create set;
- total staged paths = 146;
- unrelated local state remains unstaged;
- `git diff --cached --check` passes.

Use Git rename detection as informational evidence only; exact path-set closure is authoritative.

Required:

`73-FILE RELOCATION — STAGED PAYLOAD 146/146: PASS`

## Validation

Because this is content-preserving script relocation:

- `git diff --cached --check`;
- Gitleaks on staged/candidate content;
- verify PowerShell files remain byte/content equivalent to canonical originals;
- verify no application/build/package/schema content changed.

Run broader build/tests only if repository governance requires them for publication; record exact results if run.

No Azure, Docker, GHCR, or provider execution is required or authorized.

## Commit and PR

Create a dedicated branch from reconciled base.

Preferred branch:

`refactor/r1.11-qualification-script-layout`

Preferred commit:

`Refactor Initiative-1.11 qualification script layout`

Preferred PR title:

`Refactor Initiative-1.11 qualification script layout`

Push branch and create one non-draft PR to `main`.

Verify PR payload using authoritative Git comparison:

- 146 total paths;
- 73 deletes;
- 73 adds;
- exact Luna-approved path sets;
- zero unrelated paths.

Required:

`73-FILE RELOCATION — PR PAYLOAD 146/146: PASS`

## Merge authority

This authority authorizes merge after exact PR verification.

Before merge verify:
- base `main`;
- expected head commit;
- mergeable;
- exact 146-path payload;
- no new commits/drift.

Merge once.

Record merge SHA and parents.

Required:

`73-FILE RELOCATION — PR MERGE: PASS`

## Post-merge verification

After merge:

- fetch;
- safely synchronize local `main`;
- local `main` = `origin/main`, 0/0;
- authoritative parent..merge Git comparison;
- exact merged 146-path set;
- 73 old paths absent;
- 73 new paths tracked/present;
- content equality with historical sources remains provable;
- unrelated local artifacts remain unstaged/preserved;
- no authority-owned residue.

Required:

`73-FILE RELOCATION — MERGED PAYLOAD 146/146: PASS`

`73-FILE RELOCATION — OLD PATHS REMOVED 73/73: PASS`

`73-FILE RELOCATION — NEW PATHS TRACKED 73/73: PASS`

`73-FILE RELOCATION — POST-MERGE CONTENT PRESERVATION 73/73: PASS`

`73-FILE RELOCATION — POST-MERGE CLEANLINESS: PASS`

## Governance

This refactor does not:
- reopen/alter completed WP02;
- mutate #261/#262 lifecycle;
- alter milestone #63;
- change Product Release assignments;
- alter Release 1.10 or Initiative-1.11 historical release objects;
- authorize WP03 implementation.

After successful merge, WP03 remains next-ready from a repository state with the intentional relocation published.

## Mutation accounting

Report exact counts:

- repository delete paths: 73;
- repository create paths: 73;
- total published paths: 146;
- branches: 1;
- commits: 1;
- pushes: 1;
- PRs: 1;
- PR merges: 1;
- fetches;
- local-main synchronizations;
- issue mutations: 0;
- Project mutations: 0;
- milestone mutations: 0;
- tag/release mutations: 0;
- Azure: 0;
- Docker: 0;
- GHCR: 0;
- provider: 0;
- package: 0;
- schema: 0.

## Required markers

`73-FILE RELOCATION — TERRA BASE RECONCILIATION: PASS`

`73-FILE RELOCATION — TERRA PRE-STAGING 146/146: PASS`

`73-FILE RELOCATION — STAGED PAYLOAD 146/146: PASS`

`73-FILE RELOCATION — CONTENT-PRESERVING REFACTOR VALIDATION: PASS`

`73-FILE RELOCATION — COMMIT: PASS`

`73-FILE RELOCATION — PUSH: PASS`

`73-FILE RELOCATION — PR PAYLOAD 146/146: PASS`

`73-FILE RELOCATION — PR MERGE: PASS`

`73-FILE RELOCATION — MERGED PAYLOAD 146/146: PASS`

`73-FILE RELOCATION — OLD PATHS REMOVED 73/73: PASS`

`73-FILE RELOCATION — NEW PATHS TRACKED 73/73: PASS`

`73-FILE RELOCATION — POST-MERGE CONTENT PRESERVATION 73/73: PASS`

`73-FILE RELOCATION — POST-MERGE CLEANLINESS: PASS`

`73-FILE RELOCATION — MUTATION AUDIT: PASS`

Acceptance:

`73-FILE RELOCATION — PUBLICATION & MERGE: PASS`

Next:

`RELEASE 1.12 WP03 — EXECUTION AUTHORITY: READY`

Terminal:

`73-FILE RELOCATION — TERRA PUBLICATION + MERGE AUTHORITY COMPLETE`

Blocked:

`73-FILE RELOCATION — TERRA PUBLICATION + MERGE AUTHORITY BLOCKED`
