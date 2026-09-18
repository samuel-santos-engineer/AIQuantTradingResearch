# GPT-5.6 Luna — Release 1.12 73-File Relocation Governance Amendment

**Selected execution model: GPT-5.6 Luna**

## User-authorized governance correction

The repository owner has explicitly clarified that the previously observed 73 tracked deletions are intentional because those scripts were refactored/moved to another repository folder.

This supersedes the prior reconciliation classification:

`RESTORE_TRACKED_PATH: 73`

for those exact scripts only.

The new intended semantic operation is:

**tracked relocation/refactor: 73 old tracked paths deleted + 73 corresponding relocated paths added**

No other local untracked artifacts are included.

## Canonical base

Expected canonical `main`:

`0ffca425f485fdea23e4a2f88ee2c5968b6046f0`

Expected local branch may still be:

`feat/release-1.12-wp02-container-runtime`

The authority is read-only.

## Scope

Reconcile exactly these four historical script groups on canonical `main`:

- WP02 App Service qualification scripts: 26 files
  - `check-docker.ps1`
  - `check-docker-2.ps1` through `check-docker-26.ps1`
- WP03 SQLite qualification scripts: 26 files
  - `check-sqlite-01.ps1` through `check-sqlite-26.ps1`
- WP04 Twelve Data qualification scripts: 17 files
  - `check-conn-01.ps1` through `check-conn-17.ps1`
- WP05 qualification scripts: 4 files
  - `check-qual-01.ps1` through `check-qual-04.ps1`

Total old paths: 73.

Expected relocated root:

`eng/azure-cli/r1.11-qualification/`

The relocated set must contain exactly 73 files corresponding one-to-one with the 73 deleted tracked files.

## Required one-to-one relocation proof

For every old tracked path and candidate relocated path, output:

- exact old repository-relative path;
- exact new repository-relative path;
- old path exists in canonical `main`;
- new path exists locally as untracked;
- basename relationship;
- content hash old;
- content hash new;
- content equality result.

Default authorization is **content-preserving move only**.

If any relocated file content differs from the canonical tracked source, classify it `CONTENT_CHANGED` and BLOCK Terra publication until Luna explicitly approves the changed content.

## Excluded local artifacts

The following are not part of this relocation and remain local/unpublished:

- `eng/azure-cli/r1.12-deployment/wp02-deployment/a1.ps1` through `a10.ps1`;
- root `a10.ps1`;
- all `docs/roadmap/release-1.12/prompters/*.md`;
- any other untracked file not one of the exact 73 relocation targets.

## Literal path contract

Produce two exact sorted blocks:

`R111_RELOCATION_DELETE_SET`

Exactly 73 literal old paths.

`R111_RELOCATION_CREATE_SET`

Exactly 73 literal new paths.

Then produce:

`R111_RELOCATION_MUTATION_SET`

Exactly 146 literal paths total.

No wildcard/glob/directory-only authorization is valid.

Report:

- `DELETE_PATH_COUNT=73`
- `CREATE_PATH_COUNT=73`
- `TOTAL_MUTATION_PATH_COUNT=146`
- `ONE_TO_ONE_MAPPING_COUNT=73`
- `CONTENT_EQUALITY_PASS_COUNT=73`
- `CONTENT_CHANGED_COUNT=0`
- `UNRELATED_UNTRACKED_INCLUDED_COUNT=0`

## Governance effect

If all 73 mappings are content-equal and exact:

`73-FILE RELOCATION — USER INTENT OVERRIDE: ACCEPTED`

`73-FILE RELOCATION — PRIOR RESTORE CLASSIFICATION: SUPERSEDED`

`73-FILE RELOCATION — DELETE 73 / CREATE 73: AUTHORIZED`

This amendment does not mutate the repository.

## Publication requirement

If exact relocation closure passes:

`73-FILE RELOCATION PUBLICATION: REQUIRED`

The relocation should be committed and merged as a dedicated refactor PR before WP03 implementation, restoring a coherent tracked repository layout.

## Required markers

`73-FILE RELOCATION — CANONICAL BASE RECONCILIATION: PASS`

`73-FILE RELOCATION — OLD TRACKED SET 73/73: PASS`

`73-FILE RELOCATION — NEW RELOCATED SET 73/73: PASS`

`73-FILE RELOCATION — ONE-TO-ONE PATH MAPPING 73/73: PASS`

`73-FILE RELOCATION — CONTENT EQUALITY 73/73: PASS`

`73-FILE RELOCATION — UNRELATED LOCAL ARTIFACT EXCLUSION: PASS`

`73-FILE RELOCATION — LITERAL MUTATION SET 146/146: PASS`

`73-FILE RELOCATION — GOVERNANCE AMENDMENT: PASS`

Acceptance:

`73-FILE RELOCATION — RECONCILIATION: PASS`

Terra handoff:

`GPT-5.6 TERRA RELOCATION CONTRACT: ONLY R111_RELOCATION_MUTATION_SET MAY BE MUTATED`

`73-FILE RELOCATION — TERRA PUBLICATION/MERGE AUTHORITY: READY`

Terminal:

`73-FILE RELOCATION — LUNA GOVERNANCE AMENDMENT COMPLETE`

Blocked:

`73-FILE RELOCATION — LUNA GOVERNANCE AMENDMENT BLOCKED`
