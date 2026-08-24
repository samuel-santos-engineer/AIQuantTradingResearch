# Release 1.8 — Planning Baseline Reconciliation Codex Authority

## Authority

You are authorized to perform one narrowly scoped Release 1.8 planning-baseline reconciliation for `samuel-santos-engineer/AIQuantTradingResearch`.

This authority exists because the Release 1.8 planning artifacts were legitimately committed by the human after Release 1.7 closure.

The prior Release 1.8 GitHub Planning and partial-planning reconciliation authorities still reference the historical Release 1.7 merge SHA as their mandatory repository HEAD. That HEAD requirement is now stale for ongoing Release 1.8 planning.

This authority corrects only the planning baseline expectation. It does not authorize Release 1.8 GitHub Planning mutations, implementation, Python installation, Git integration, or Release 1.9 work.

## Historical Predecessor

Release 1.7 remains the authoritative closed predecessor:

- commit: `f8e521af2c5262d6cc173d0731b5e915dbceac0a`
- tree: `880f7fff6a9b946a310d32e17c1c803ca6c1a286`
- schema: v3
- permanent tests: 268/268
- Release 1.7: CLOSED

Do not reset, rewrite, or otherwise return `main` to the Release 1.7 SHA.

## New Release 1.8 Planning Baseline

The human explicitly confirms that the following synchronized `main` commit is legitimate Release 1.8 planning content:

`651c45bd0df0b717b2bb5ad272ec8c890612fb6d`

This commit is the authoritative **Release 1.8 planning baseline** for ongoing Release 1.8 GitHub planning and WP execution.

Baseline progression:

```text
Release 1.7 closed baseline
f8e521af2c5262d6cc173d0731b5e915dbceac0a
        ↓
Release 1.8 planning baseline
651c45bd0df0b717b2bb5ad272ec8c890612fb6d
```

## Governed Planning Content

Read completely:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`

Verify these artifacts are contained in or consistent with commit `651c45bd0df0b717b2bb5ad272ec8c890612fb6d`.

Do not change their substantive content under this authority.

## Authorities Affected by Baseline Reconciliation

This reconciliation applies prospectively to the existing Release 1.8 governance authorities whose only stale assumption is that repository HEAD must still equal the Release 1.7 merge SHA.

At minimum this includes:

- `release-1.8-github-planning-codex-prompt.md`
- `release-1.8-partial-github-planning-reconciliation-codex-prompt.md`

For resumed execution, reinterpret only their repository-baseline gate as:

- branch: `main`
- HEAD: `651c45bd0df0b717b2bb5ad272ec8c890612fb6d`
- `origin/main`: same SHA
- ahead/behind: `0/0`

All other authority constraints remain unchanged.

Do not rewrite those full authority files merely to update the SHA. This authority is sufficient governance to supersede the stale HEAD gate during the resumed Codex session.

## Mandatory Starting-State Gate

Before declaring reconciliation complete, verify:

1. repository is `samuel-santos-engineer/AIQuantTradingResearch`;
2. branch is `main`;
3. HEAD is `651c45bd0df0b717b2bb5ad272ec8c890612fb6d`;
4. `origin/main` is the same SHA;
5. ahead/behind is `0/0`;
6. no staged paths;
7. no unexpected tracked repository mutation;
8. Release 1.8 planning artifacts exist and are readable;
9. their lifecycle state is human-accepted;
10. Release 1.7 remains the closed historical predecessor;
11. Release 1.8 GitHub partial-planning state remains preserved;
12. milestone #56 and issues #211–#216 are not modified by this authority;
13. Release 1.9 implementation has not begun.

If any condition fails, stop and report the smallest corrective authority required.

## Known GitHub Partial State to Preserve

Do not mutate this state under this authority.

Latest known Release 1.8 planning state:

- milestone #56: OPEN;
- #211 WP01: created/reconciled;
- #212 WP02: created/reconciled;
- #213 WP03: created/reconciled;
- #214 WP04: created/reconciled;
- #215 WP05: created/reconciled;
- #216 WP06: created and assigned to milestone #56;
- WP01→WP02→WP03→WP04→WP05 dependencies were verified;
- WP05→WP06 Project/dependency state remains unproven because GitHub GraphQL rate limiting interrupted read-back;
- no WP07–WP13 planning is proven complete;
- no Release 1.8 implementation has begun.

The next resumed planning authority must read this live state before mutation.

## Authorized Reconciliation

Authorize exactly this governance correction:

**For ongoing Release 1.8 planning and WP execution, use `651c45bd0df0b717b2bb5ad272ec8c890612fb6d` as the mandatory synchronized repository baseline instead of `f8e521af2c5262d6cc173d0731b5e915dbceac0a`.**

No other governance rule changes.

## Explicit Prohibitions

Do not:

- reset `main`;
- revert the Release 1.8 planning commit;
- force-push;
- amend the planning commit;
- edit Release 1.8 planning files;
- edit existing GitHub Planning authority files;
- create or modify milestones/issues/Project items/dependencies;
- create WP07–WP13;
- install Python;
- create a virtual environment;
- modify source/tests/docs beyond the already committed planning content;
- stage files;
- create commits;
- push;
- create branches/PRs;
- merge;
- tag;
- create GitHub Releases;
- begin Release 1.9.

## Validation

Verify:

- `git rev-parse HEAD` = `651c45bd0df0b717b2bb5ad272ec8c890612fb6d`;
- `git rev-parse origin/main` = same;
- ahead/behind = `0/0`;
- branch = `main`;
- staged paths = 0;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- Release 1.8 governed planning files are present;
- no repository mutation is introduced by this reconciliation;
- no GitHub mutation is introduced;
- no Python/environment mutation is introduced.

## Execution-Only Authority Lifecycle

This authority pair is execution-only input.

It must not be staged or committed unless separately governed.

If repository conventions allow cleanup, remove only this authority pair after successful reconciliation. Otherwise leave it untracked and report it.

Do not remove governed Release 1.8 planning artifacts or existing GitHub Planning/reconciliation authority files.

## Required Report

Report:

- historical Release 1.7 predecessor SHA;
- accepted Release 1.8 planning baseline SHA;
- branch/local/remote synchronization;
- planning artifact presence;
- confirmation that the stale HEAD gate is superseded only for Release 1.8 planning;
- confirmation that all other authority constraints remain unchanged;
- preserved GitHub partial state;
- repository/Git/GitHub/Python mutation counts;
- authority-file cleanup state;
- exact next authorized action.

On success end exactly:

`RELEASE 1.8 PLANNING BASELINE RECONCILIATION COMPLETE`

`RELEASE 1.8 PLANNING BASELINE: 651c45bd0df0b717b2bb5ad272ec8c890612fb6d`

`NEXT AUTHORIZED ACTION: Resume Release 1.8 partial GitHub Planning reconciliation from the live #56/#211–#216 state, using the existing corrective authority with this reconciled baseline.`

Do not resume GitHub Planning automatically.
