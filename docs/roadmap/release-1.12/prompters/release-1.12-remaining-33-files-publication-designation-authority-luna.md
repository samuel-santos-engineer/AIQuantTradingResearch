# GPT-5.6 Luna — Release 1.12 Remaining 33 Files Publication Designation Authority

**Selected execution model: GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — authoritative read-only reconciliation, exact path designation, governance ownership, publication grouping, acceptance criteria.
- **GPT-5.6 Terra** — later implementation/publication/merge mutations under an explicit exact-path authority.
- **GPT-5.6 Sol** — supporting analysis only; never silently replaces Luna or Terra.

## Mission

Reconcile and freeze the exact literal set of the 33 local files intentionally preserved outside PR #270, and determine whether they may be published together as one dedicated PR.

This authority is strictly read-only.

## Canonical base

Expected canonical `main` after PR #270:

`e931e714258028b2a6aa942a2b5cda1fc3f6866f`

Expected:
- local `main` = `origin/main`
- ahead/behind `0/0`
- staging empty

Fresh empirical evidence controls.

## Expected candidate universe

Exactly 33 local files are expected:

1. root operator/helper:
   - `a10.ps1`

2. Release 1.12 deployment scripts:
   - `eng/azure-cli/r1.12-deployment/wp02-deployment/a1.ps1`
   - `eng/azure-cli/r1.12-deployment/wp02-deployment/a2.ps1`
   - `eng/azure-cli/r1.12-deployment/wp02-deployment/a3.ps1`
   - `eng/azure-cli/r1.12-deployment/wp02-deployment/a4.ps1`
   - `eng/azure-cli/r1.12-deployment/wp02-deployment/a5.ps1`
   - `eng/azure-cli/r1.12-deployment/wp02-deployment/a6.ps1`
   - `eng/azure-cli/r1.12-deployment/wp02-deployment/a7.ps1`
   - `eng/azure-cli/r1.12-deployment/wp02-deployment/a8.ps1`
   - `eng/azure-cli/r1.12-deployment/wp02-deployment/a9.ps1`
   - `eng/azure-cli/r1.12-deployment/wp02-deployment/a10.ps1`

3. exactly 22 Markdown control artifacts under:
   - `docs/roadmap/release-1.12/prompters/`

The 22 Markdown files MUST be expanded to exact literal repository-relative paths.

## Required read-only reconciliation

Prove:
- candidate count is exactly 33;
- every candidate is currently untracked or otherwise unpublished;
- none is already on canonical `main`;
- none overlaps PR #270's 146-path relocation;
- no additional local file is silently included.

If the candidate count is not exactly 33, BLOCK and report the exact discrepancy.

## Content inspection and publication suitability

Inspect all 33 files.

For each file determine:
- purpose;
- governance owner;
- whether it is source-of-record, operator tooling, generated artifact, duplicate, temporary helper, or historical prompt/control artifact;
- whether public publication is appropriate;
- whether it contains secrets, credentials, tokens, auth caches, local-only user paths, transient logs, or sensitive runtime values;
- whether publication would create misleading Release 1.12 architecture or operational claims.

### Special rule for root `a10.ps1`

Determine whether root `a10.ps1` is:
- an exact duplicate of `eng/azure-cli/r1.12-deployment/wp02-deployment/a10.ps1`;
- a materially different operator helper;
- required as a repository root artifact.

If it is a duplicate or local-only helper with no repository purpose, Luna MUST NOT include it merely to reach 33 files.

If excluding it causes the publication set to be 32 or fewer, report that truthfully. Do not force a 33-path set.

## Governance ownership

Map each candidate to one primary owner:
- Release 1.12 deployment/tooling;
- Release 1.12 governance/prompter history;
- local-only operator workflow;
- duplicate/redundant;
- blocked/sensitive.

## Publication decision

Luna must select exactly one:

`REMAINING LOCAL FILES PUBLICATION: ONE PR APPROVED`

`REMAINING LOCAL FILES PUBLICATION: SPLIT REQUIRED`

`REMAINING LOCAL FILES PUBLICATION: PARTIAL ONLY`

`REMAINING LOCAL FILES PUBLICATION: BLOCKED`

Publishing all 33 together is allowed only if they form a coherent repository-purpose package and all are suitable for public source control.

Do not group files solely to clear local `git status`.

## Exact literal path contract

If one PR is approved, output:

`REMAINING_33_PUBLICATION_SET`

containing every exact literal path, sorted.

Required counts:

`TOTAL_PATH_COUNT=<actual>`

`POWERSHELL_PATH_COUNT=<actual>`

`MARKDOWN_PATH_COUNT=<actual>`

`OTHER_PATH_COUNT=<actual>`

`UNTRACKED_CREATE_COUNT=<actual>`

Expected only if all 33 are approved:

`TOTAL_PATH_COUNT=33`
`POWERSHELL_PATH_COUNT=11`
`MARKDOWN_PATH_COUNT=22`
`OTHER_PATH_COUNT=0`
`UNTRACKED_CREATE_COUNT=33`

No wildcards, globs, directory-only entries, ellipses, or inferred future paths.

## Sensitive-content gate

Run/read-only screening sufficient to establish that the approved publication set contains no unresolved secret or credential material.

Synthetic/invalid test values may be allowed only if clearly non-secret.

## WP03 relationship

Decide:

`REMAINING LOCAL FILES — WP03 PUBLICATION PREREQUISITE: REQUIRED`

or

`REMAINING LOCAL FILES — WP03 PUBLICATION PREREQUISITE: NOT REQUIRED`

Explain why.

## Mutation prohibition

Not authorized:
- staging;
- commit;
- branch creation;
- push;
- PR creation;
- merge;
- file edits;
- file deletion;
- GitHub lifecycle changes;
- Azure/Docker/GHCR/provider execution;
- package/schema changes.

## Required markers

`REMAINING LOCAL FILES — CANONICAL BASE RECONCILIATION: PASS`

`REMAINING LOCAL FILES — CANDIDATE INVENTORY: PASS`

`REMAINING LOCAL FILES — CONTENT & OWNERSHIP CLASSIFICATION: PASS`

`REMAINING LOCAL FILES — SENSITIVE CONTENT SCREENING: PASS`

`REMAINING LOCAL FILES — PUBLICATION COHERENCE: PASS`

`REMAINING LOCAL FILES — LITERAL PATH SET CLOSURE: PASS`

`REMAINING LOCAL FILES — WP03 DEPENDENCY DECISION: PASS`

`REMAINING LOCAL FILES — READ-ONLY MUTATION AUDIT: PASS`

If all 33 are approved:

`REMAINING 33 FILES — PUBLICATION SET 33/33: PASS`

Acceptance:

`REMAINING LOCAL FILES — PUBLICATION DESIGNATION: PASS`

Terra handoff:

`GPT-5.6 TERRA REMAINING-FILES CONTRACT: ONLY REMAINING_33_PUBLICATION_SET MAY BE MUTATED`

`REMAINING LOCAL FILES — TERRA PUBLICATION/MERGE AUTHORITY: READY`

Terminal:

`REMAINING LOCAL FILES — LUNA PUBLICATION DESIGNATION AUTHORITY COMPLETE`

If unresolved:

`REMAINING LOCAL FILES — LUNA PUBLICATION DESIGNATION AUTHORITY BLOCKED`
