# Release 1.6 GitHub Planning Authority

## Mission

Create and reconcile the GitHub planning objects for:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

This authority is planning-only. It authorizes bounded GitHub lifecycle mutations required to establish the accepted Release 1.6 work-package plan. It does **not** authorize WP01 execution, repository implementation, staging, commits, branches, pushes, pull requests, tags, releases, schema changes, or Release 1.7 work.

## Authoritative Inputs

Treat the following accepted artifacts as controlling Release 1.6 planning authority:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- this full authority prompt
- its five-line chat companion

Authoritative predecessor baseline:

`18dfb01bf3503d91415b081b11fcdd7249094373`

Release 1.5 is closed and immutable unless an actual defect is separately discovered and authorized.

## Mandatory Starting-State Verification

Before any GitHub mutation, verify and report:

1. Repository is `samuel-santos-engineer/AIQuantTradingResearch`.
2. Current branch is `main`.
3. Local `main` and `origin/main` both equal:
   `18dfb01bf3503d91415b081b11fcdd7249094373`
4. Ahead/behind is `0/0`.
5. Working tree has no staged tracked changes.
6. PR #181 is merged.
7. Release 1.5 milestone #46 is CLOSED with issues #168–#180 closed.
8. Release 1.6 implementation has not started.
9. No Release 1.6 branch or PR exists.
10. No Release 1.7 planning/implementation has started.
11. Implemented SQLite schema remains v2.
12. Canonical predecessor verification remains 238/238 with 13 Architecture tests and zero build warnings/errors.

If any material baseline fact conflicts with the accepted authority, stop before mutation and report the conflict.

Expected untracked Release 1.6 governance/planning artifacts are not implementation and must not be deleted, staged, committed, or rewritten by this authority.

## Legacy Milestone #47 — Mandatory Reconciliation Gate

Milestone #47 is known from the accepted definition as:

- OPEN
- EMPTY
- historically titled `Strategy Framework`

Do **not** silently repurpose it.

Read back its current title, description, due date, state, and issue associations before mutation.

### Preferred reconciliation

If milestone #47 is still OPEN, EMPTY, and contains no issue associations or other evidence that it has active independent meaning, it may be **reconciled in place** for Release 1.6 rather than creating a duplicate milestone.

The authorized target title is:

`Phase 4 - Release 1.6: Durable Experiment Evidence Foundation`

Its description should concisely state that Release 1.6 makes accepted deterministic Experiment Result evidence durably persistent and exactly retrievable while preserving Release 1.5 semantics, using atomic SQLite schema v2→v3 evolution, with no Feature Set persistence, generalized registry/history, provider acquisition, strategy/backtesting, or Release 1.7 scope.

Preserve its due date unless the accepted planning artifacts explicitly define one. Do not invent a due date.

### Stop condition

If milestone #47 is not still empty, has issue associations, has materially changed lifecycle meaning, or cannot be safely reconciled without destroying active historical planning, stop and report the smallest corrective authority required. Do not create a replacement milestone automatically.

## Project #2 Release Option

Inspect the Project #2 `Release` field before mutation.

Required final state:

- exactly one option representing Release 1.6;
- no duplicate semantic Release 1.6 option.

Preferred option label:

`1.6`

If exactly one correct option already exists, reuse it.
If none exists, add exactly one.
If duplicates or conflicting Release 1.6 options exist, stop rather than silently deleting or merging them.

Do not alter existing Release options for predecessor releases.

## Work-Package Inventory

Create exactly fourteen Release 1.6 work-package issues, WP01–WP14, and no WP15+ issue.

Use these authoritative identities:

| WP | Title | Label | Area | Dependency |
| --- | --- | --- | --- | --- |
| WP01 | Release & Repository Preflight | `devops` | Engineering | Accepted Release 1.6 planning and closed Release 1.5 |
| WP02 | Durable Experiment Evidence Discovery | `research` | Data | WP01 |
| WP03 | Persistence Identity, Provenance & Fidelity | `architecture` | Architecture | WP02 |
| WP04 | Application Persistence Contracts | `feature` | Architecture | WP03 |
| WP05 | Durable Experiment Use-Case Integration | `feature` | Architecture | WP04 |
| WP06 | Schema-v3 Physical Model | `architecture` | Data | WP05 |
| WP07 | Experiment Result Persistence | `feature` | Data | WP06 |
| WP08 | Exact Experiment Result Retrieval | `feature` | Data | WP07 |
| WP09 | Storage Validation & Failure Mapping | `feature` | Architecture | WP08 |
| WP10 | Dependency Registration & Configuration | `infra` | Configuration | WP09 |
| WP11 | One-Shot Durable Experiment Worker | `feature` | Host | WP10 |
| WP12 | Application & Infrastructure Persistence Tests | `tests` | Testing | WP11 |
| WP13 | Architecture & Documentation Alignment | `documentation` | Documentation | WP12 |
| WP14 | Full Validation, Integration & Acceptance | `devops` | Engineering | WP13 |

Use the repository's established Release 1.4/1.5 issue-title convention. Prefer:

`Release 1.6 WP01 — Release & Repository Preflight`

and correspondingly through WP14.

Do not create separate lifecycle-gate, post-merge, reconciliation, or Release 1.7 issues.

## Issue Bodies

Each issue body must be bounded and derived from the accepted Release 1.6 definition/execution plan.

Every issue should identify:

- objective;
- predecessor/dependency;
- authorized scope;
- explicit exclusions;
- acceptance evidence;
- lifecycle boundary.

Keep issue bodies concise enough for execution tracking. Do not copy entire authority documents into GitHub.

Important semantic boundaries that must remain visible across the issue set:

- persisted artifact is Experiment Result only;
- `aiq-experiment-identity-v1` remains authoritative;
- exact retrieval by Experiment Result Identity;
- `NewlyAccepted` / `EquivalentExisting`;
- contradictory same-identity evidence → `IntegrityConflict`;
- atomic non-destructive SQLite v2→v3 evolution;
- Release 1.5 in-memory Experiment execution remains unchanged;
- no Feature Set persistence;
- no experiment registry/history/search;
- no updates/deletes;
- no provider acquisition/fallback;
- no strategy/backtesting;
- no retry/scheduling framework;
- no Release 1.7 implementation.

## Labels

Use existing repository labels only.

Expected labels are the labels listed in the WP inventory:

- `devops`
- `research`
- `architecture`
- `feature`
- `infra`
- `tests`
- `documentation`

Before issue creation, verify every required label exists.

If a required label is absent, stop and report the missing label. This authority does not authorize creating or redefining labels.

Do not modify existing label definitions.

## Assignee

Assign every Release 1.6 WP issue to:

`samuel-santos-engineer`

If assignment cannot be performed, stop before leaving a partially configured issue inventory if practical; otherwise report the exact partial state and perform no unrelated correction.

## Milestone

Every WP01–WP14 issue must belong to the reconciled Release 1.6 milestone #47.

Required final milestone state:

- OPEN;
- exactly 14 Release 1.6 WP issues;
- 14 open / 0 closed immediately after planning;
- no unrelated issue associations.

Do not close the milestone.

## Project #2 Membership

Every WP01–WP14 issue must have exactly one Project #2 item.

If project automation automatically adds an issue:

- reuse that item;
- do not create a duplicate.

Final duplicate Project items for these issues: `0`.

## Project #2 Fields

For all fourteen issues, set:

- **Status:** `Backlog`
- **Priority:** `P1`
- **Release:** `1.6`
- **Area:** the authoritative Area from the WP inventory

Use the repository's actual field names/options as read back from Project #2.

Do not alter unrelated Project fields or predecessor issue values.

## Dependencies

Preserve the linear accepted dependency model:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08 → WP09 → WP10 → WP11 → WP12 → WP13 → WP14`

Use the repository's established dependency representation from Release 1.5.

Do not invent a different dependency mechanism.

After mutation, verify dependency drift is zero.

## Duplicate and Existing-Object Reconciliation

Before creating issues, search for existing Release 1.6 WP identities.

Required final state:

- exactly one issue per WP01–WP14;
- no duplicate WP identity;
- no WP15+;
- no unauthorized lifecycle-gate issue.

If an exact intended issue already exists and is safely reusable, reconcile it rather than duplicating it.

If existing objects materially conflict with this authority, stop and report the conflict rather than deleting historical data.

## Authorized GitHub Mutations

This authority permits only the GitHub mutations necessary to establish the planning state:

1. reconcile milestone #47 in place if and only if its mandatory reconciliation gate passes;
2. add exactly one Project #2 `Release = 1.6` option if absent;
3. create/reuse and configure exactly WP01–WP14 issues;
4. assign each issue to `samuel-santos-engineer`;
5. associate each issue with milestone #47;
6. establish accepted dependency metadata;
7. ensure exactly one Project #2 item per issue;
8. set Status/priority/release/area fields.

No issue is to be moved to In Progress or closed during GitHub planning.

## Explicitly Unauthorized Mutations

Do not:

- modify repository content;
- stage files;
- commit;
- create or switch to an integration/implementation branch;
- push;
- create a PR;
- merge anything;
- create tags or GitHub Releases;
- close milestone #47;
- modify milestones #44, #45, #46, or #54;
- reopen predecessor issues;
- alter Release 1.5;
- create Release 1.7 objects;
- execute WP01;
- implement schema v3;
- perform production/test code changes;
- delete historical GitHub objects;
- force-push or rewrite history.

## Repository Integrity Verification

Because this is a GitHub-planning authority, repository content must remain unchanged.

After GitHub planning, verify:

- no tracked repository modification caused by this execution;
- staged paths: 0;
- no commits;
- no branches created;
- no pushes;
- no PRs;
- schema implementation still v2;
- production graph unchanged;
- no package/project/reference mutation;
- no Release 1.6 implementation.

Expected planning/governance files may remain untracked and unchanged.

## Technical Verification

Run canonical Release verification against the predecessor baseline:

`eng/verify.ps1 -Configuration Release`

Required baseline:

- Domain.Tests: 11/11
- Application.Tests: 102/102
- Infrastructure.Tests: 112/112
- Architecture.Tests: 13/13
- Total: 238/238
- warnings: 0
- errors: 0
- formatting: PASS
- Gitleaks: PASS

Also verify:

- `git diff --check`: PASS
- `git diff --cached --check`: PASS
- direct whitespace/final-newline checks for expected untracked governed Release 1.6 artifacts: PASS
- database/WAL/SHM/journal/generated residue: 0
- provider/network execution: 0

Do not treat GitHub planning mutations as repository-content changes.

## Mandatory Final Read-Back

Read back GitHub after all authorized mutations.

Report:

### Milestone
- #47 title
- state
- due date
- open/closed counts
- issue associations

### Issues
- exact WP01–WP14 issue numbers/titles
- Open state
- assignee
- label
- milestone
- dependency
- no duplicate WP identities
- no WP15+

### Project #2
For all 14:
- membership exactly once
- Status = Backlog
- Priority = P1
- Release = 1.6
- correct Area
- duplicate items = 0

### Preservation
- predecessor milestones unchanged
- Release 1.5 unchanged
- no Release 1.7 objects
- no Release 1.6 branch/PR
- WP01 not started

## Stop-on-Conflict Rule

This authority favors correctness over completing a partially ambiguous GitHub plan.

Stop before the conflicting mutation when encountering:

- non-empty or actively meaningful milestone #47;
- duplicate/conflicting Release 1.6 Project options;
- missing required existing labels;
- duplicate/conflicting WP identities;
- unexpected pre-existing Release 1.6 implementation/lifecycle objects;
- repository baseline drift;
- predecessor closure regression.

Report the smallest corrective authority needed.

Do not improvise destructive reconciliation.

## Completion Marker

If every gate passes, end the execution report with exactly:

`RELEASE 1.6 GITHUB PLANNING COMPLETE`

Then state:

`NEXT AUTHORIZED WORK PACKAGE: WP01 — Release & Repository Preflight`

WP01 must remain OPEN / Backlog at the end of this planning authority.
