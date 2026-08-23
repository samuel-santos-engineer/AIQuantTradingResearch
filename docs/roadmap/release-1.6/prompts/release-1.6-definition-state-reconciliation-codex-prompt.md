# Release 1.6 Definition-State Reconciliation Authority

## 1. Purpose

This prompt is the sole corrective authority for reconciling the already-completed Release 1.6 definition against the repository and GitHub state that legitimately advanced after the original planning-definition execution.

The historical authority:

`release-1.6-planning-definition-codex-prompt.md`

must not be re-executed against its original starting-state assumptions because Release 1.6 planning has already progressed.

This corrective authority explicitly supersedes only those historical starting-state requirements that required:

- no Release 1.6 definition artifact;
- no Release 1.6 execution plan;
- no Release 1.6 file manifest;
- no Release 1.6 GitHub-planning authorities;
- no Release 1.6 issues;
- no Release 1.6 Project configuration;
- no Release 1.6 untracked governance artifacts.

It does not supersede the accepted semantic decisions of the Release 1.6 definition.

The purpose is to validate the existing Release 1.6 definition in place against the completed planning state and preserve all already-authorized Release 1.6 governance.

No implementation work is authorized.

## 2. Authoritative Predecessor Baseline

The immutable Release 1.5 baseline remains:

`18dfb01bf3503d91415b081b11fcdd7249094373`

Verify:

- branch `main`;
- `HEAD == origin/main`;
- ahead/behind `0/0`;
- Release 1.5 PR #181 merged;
- Release 1.5 milestone #46 closed;
- Release 1.5 repository content unchanged by this reconciliation.

Do not mutate Release 1.5.

## 3. Accepted Release 1.6 Governance State

The following existing Release 1.6 artifacts and GitHub objects are now accepted planning state and must not be treated as premature work merely because they violate the historical definition prompt's original starting gate.

Expected repository governance/planning artifacts include:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- Release 1.6 GitHub-planning authority pair
- Release 1.6 Project Release-field restoration/reconciliation authority pair
- historical Release 1.6 planning-definition authority pair

Expected GitHub state includes:

- milestone #47 reconciled to Release 1.6;
- milestone #47 OPEN;
- issues #182–#195 existing;
- issues #182–#195 OPEN;
- Project #2 membership for #182–#195;
- Status Backlog for #182–#195;
- Priority P1 for #182–#195;
- Release 1.6 for #182–#195;
- authoritative Areas for #182–#195;
- linear dependencies WP01→WP14;
- predecessor Project Release fields restored;
- WP01 not started.

These are accepted planning outcomes, not blockers.

## 4. Reconciliation Objective

Validate that the existing:

`docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`

remains semantically coherent with:

- the accepted Release 1.5 baseline;
- the accepted Release 1.6 execution plan;
- the accepted Release 1.6 file manifest;
- the completed GitHub planning state;
- milestone #47;
- issues #182–#195;
- Project #2 Release 1.6 configuration.

The preferred outcome is:

**validate and preserve the existing definition unchanged.**

Replacement or modification of the definition is authorized only if a material contradiction is proven between the existing definition and the subsequently accepted execution plan/manifest/planning state.

Do not rewrite the definition for stylistic reasons.

## 5. Definition Semantic Baseline

The existing definition is expected to define:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

Selected capability:

- persist accepted Release 1.5 Experiment Result evidence only.

Expected semantic decisions:

- reuse `aiq-experiment-identity-v1`;
- no new persistence identity;
- exact lookup by Experiment Result Identity;
- `NewlyAccepted`;
- `EquivalentExisting`;
- contradictory same-identity evidence → `IntegrityConflict`;
- exact empty/non-empty fidelity;
- atomic persistence;
- restart-safe exact retrieval;
- separate explicit durable-experiment boundary;
- no Feature Set persistence;
- no generalized experiment registry/history/search;
- no update/delete;
- no provider acquisition/fallback;
- no strategy/backtesting;
- no retry/scheduling framework;
- no Release 1.7 implementation.

Expected schema decision:

- implemented predecessor remains schema v2;
- Release 1.6 proposes atomic non-destructive v2→v3 evolution.

Expected architecture:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure.

## 6. Execution Plan Reconciliation

Read the full existing:

`RELEASE_1.6_EXECUTION_PLAN.md`

Verify it operationalizes rather than contradicts the definition.

Expected:

- WP01–WP14;
- no WP15+;
- semantic discovery before implementation;
- schema-v3 physical-model definition before SQLite implementation;
- explicit persistence/retrieval boundaries;
- DI and Worker later in sequence;
- permanent tests after core implementation;
- architecture/documentation alignment;
- final integration/acceptance last;
- PR remains unmerged at WP14 completion.

If the execution plan materially changes the Release 1.6 semantic boundary defined by the definition, stop and report the contradiction.

## 7. File Manifest Reconciliation

Read the full existing:

`RELEASE_1.6_FILE_MANIFEST.md`

Verify it preserves the definition boundary.

Expected:

- planning artifacts governed;
- historical planning-definition pair out-of-band;
- GitHub-planning pair governed;
- WP01–WP14 prompt pairs governed;
- Release 1.6 semantic/schema documentation governed;
- Application/Infrastructure/Worker authorized surfaces bounded;
- Feature Set persistence excluded;
- registry/history excluded;
- package/project/reference zero-delta-first;
- Architecture test zero-delta-first;
- WP14 exact candidate reconciliation required;
- Release 1.7 excluded.

If manifest ownership materially expands Release 1.6 beyond the definition, stop.

## 8. GitHub Planning Reconciliation

Read back milestone #47 and issues #182–#195.

Require:

- #47 OPEN;
- 14 open / 0 closed;
- WP01–WP14 exact;
- #182–#195 OPEN;
- WP01 #182 Backlog;
- none started;
- Project membership exactly once;
- Status Backlog 14/14;
- Priority P1 14/14;
- Release 1.6 14/14;
- authoritative Areas 14/14;
- dependency drift 0;
- WP15+ 0;
- Release 1.7 work 0.

This state is accepted and must not be undone merely to satisfy the obsolete historical definition starting gate.

## 9. Historical-Authority Supersession Rule

The original Release 1.6 planning-definition authority remains historical execution evidence.

It must not be used to force rollback of later authorized planning artifacts or GitHub objects.

This corrective authority explicitly establishes:

- original definition starting-state gate = historical only after first successful definition completion;
- accepted definition semantics = still authoritative;
- later accepted plan/manifest/GitHub planning = valid downstream governance;
- no rollback to a pre-definition state is required or authorized.

Do not delete historical authority files solely because their starting-state assumptions are obsolete.

## 10. Definition Replacement Gate

Default:

`Definition modification count = 0`

Modification/replacement is authorized only if all of the following are true:

1. a material contradiction exists;
2. the contradiction affects Release 1.6 semantic scope, identity, persistence, schema, architecture, Worker boundary, or failure model;
3. the contradiction cannot be resolved by interpreting the plan/manifest as downstream detail;
4. the exact corrective text can preserve the already-completed GitHub planning state.

If any change is necessary:

- modify only `RELEASE_1.6_DEFINITION.md`;
- preserve the selected release title unless the contradiction proves it invalid;
- do not modify plan/manifest/GitHub objects in the same run;
- report exact changed sections.

If no material contradiction exists, leave the definition byte-for-byte unchanged.

## 11. Repository Mutation Budget

Preferred repository-content mutation:

`0`

Conditionally authorized:

- `RELEASE_1.6_DEFINITION.md` only, and only under the Definition Replacement Gate.

Not authorized:

- execution plan edits;
- file manifest edits;
- production code;
- tests;
- schema implementation;
- packages/projects/references;
- current-state documentation;
- WP prompts;
- staging;
- commits;
- branches;
- pushes;
- PRs.

## 12. GitHub Mutation Budget

GitHub mutation budget:

`0`

Do not:

- rename milestone #47;
- close milestone #47;
- change issue state;
- change Project fields;
- change dependencies;
- create issues;
- start WP01;
- create Release 1.7 objects.

This reconciliation is read-only with respect to GitHub.

## 13. Existing Untracked Governance Artifacts

Existing untracked Release 1.6 governance artifacts are expected.

Classify them into:

- governed future candidate;
- out-of-band historical execution authority;
- corrective execution authority.

Do not treat expected governance artifacts as implementation.

Do not delete them.

Do not stage them.

Do not commit them.

Report exact count and unexpected count.

Required:

`Unexpected paths = 0`

## 14. No Implementation Gate

Verify no Release 1.6 production implementation has started.

Require no new Release 1.6:

- Application production implementation;
- Infrastructure persistence implementation;
- Worker durable-experiment implementation;
- schema-v3 implementation;
- permanent tests;
- branch/PR.

Planning/governance/semantic definition artifacts are not implementation.

If implementation exists unexpectedly before WP01, stop.

## 15. Technical Baseline Validation

Run:

`eng/verify.ps1 -Configuration Release`

Expected predecessor counts:

- Domain.Tests: 11/11
- Application.Tests: 102/102
- Infrastructure.Tests: 112/112
- Architecture.Tests: 13/13
- Permanent total: 238/238
- warnings/errors: 0/0
- formatting: PASS
- Gitleaks: PASS

Also require:

- `git diff --check`: PASS
- `git diff --cached --check`: PASS
- staged paths: 0
- implemented schema remains v2
- database/WAL/SHM/journal residue: 0
- provider/network execution: 0
- real credentials: 0

## 16. Definition Quality Revalidation

Verify the existing definition still provides enough authority for downstream execution without semantic guessing.

Require explicit coverage of:

- selected Release 1.6 capability;
- why durable Experiment Evidence was selected;
- alternatives deferred;
- exact in/out scope;
- Release 1.1–1.5 preservation;
- identity/equivalence;
- provenance/lineage;
- persistence;
- schema;
- failure model;
- architecture ownership;
- Worker decision;
- testing strategy;
- WP-level guidance;
- Release 1.7+ deferrals;
- next-step boundary.

If this remains true, preserve the artifact unchanged.

## 17. Final Reconciled Authority State

On success, establish explicitly:

1. Release 1.6 definition is accepted and current.
2. Execution plan is accepted downstream operational detail.
3. File manifest is accepted downstream candidate governance.
4. GitHub planning state is accepted.
5. Historical definition starting-state requirements no longer apply after successful first execution.
6. WP01 is the next authorized execution action.
7. WP01 remains Open / Backlog until separately executed.

## 18. Stop Conditions

Stop without mutation if:

- Release 1.5 baseline drifted;
- definition/plan/manifest materially contradict one another;
- GitHub planning state conflicts with accepted WP01–WP14 plan;
- unexpected Release 1.6 implementation exists;
- WP01 already started;
- Release 1.7 work exists;
- technical baseline no longer passes;
- resolving the contradiction would require plan/manifest/GitHub mutation.

Report the smallest corrective authority required.

## 19. Required Execution Report

Report:

1. executive summary;
2. historical starting-state supersession applied;
3. Release 1.5 baseline verification;
4. existing Release 1.6 governance inventory;
5. definition semantic validation;
6. execution-plan reconciliation;
7. file-manifest reconciliation;
8. GitHub milestone/issue reconciliation;
9. Project #2 planning reconciliation;
10. implementation absence;
11. untracked governance classification;
12. definition contradiction findings;
13. definition modification count;
14. exact modification if any;
15. canonical validation/test counts;
16. schema/graph/package/reference baseline;
17. repository mutation accounting;
18. GitHub mutation accounting;
19. findings/blockers;
20. final reconciled authority state;
21. next authorized work package.

## 20. Completion Marker

If successful with no material contradiction, end exactly:

`RELEASE 1.6 DEFINITION STATE RECONCILIATION COMPLETE`

Then:

`RELEASE 1.6 DEFINITION ACCEPTED IN CURRENT PLANNING STATE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP01 — Release & Repository Preflight — GitHub issue #182`

WP01 must remain OPEN / Backlog.

If blocked, end:

`RELEASE 1.6 DEFINITION STATE RECONCILIATION BLOCKED`

and identify the smallest corrective authority required.
