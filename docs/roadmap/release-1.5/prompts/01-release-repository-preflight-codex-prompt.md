# Release 1.5 WP01 — Release & Repository Preflight

## GitHub Issue
`#168 — Release 1.5 WP01 — Release & Repository Preflight`

## 1. Authority
This is the authoritative WP01 execution instruction for `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is **Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**. The selected capability is `simple-return-descriptive-summary-v1`; the planned identity scheme is `aiq-experiment-identity-v1`.

Read completely before mutation:
- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `prompts/release-1.5-github-planning-codex-prompt.md`
- this WP01 authority and its five-line companion.

WP01 is preflight/lifecycle only. It MUST NOT implement experiment functionality.

## 2. Objective
Prove Release 1.5 begins from the formally closed Release 1.4 baseline and exact GitHub planning state. Validate repository identity/synchronization, predecessor closure, milestone/issues/Project state, accepted governance artifacts, unchanged architecture, SQLite schema v2, unchanged package/project/reference baseline, absence of premature Release 1.5 or Release 1.6 work, and the canonical 214-test baseline.

## 3. Git Baseline
Reconcile rather than assume:
- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- expected `HEAD == origin/main == 2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- ahead/behind: `0/0`;
- staged paths: `0`;
- tracked modifications: `0`.

Do not reset, clean, stash, overwrite, or manufacture the expected state.

## 4. Accepted Working-Tree Governance State
The completed GitHub-planning run reported seven expected untracked Release 1.5 governance artifacts. Reconcile their exact paths against repository truth and the manifest. Expected categories are the definition, execution plan, file manifest, GitHub-planning pair, and WP01 pair.

Accepted planning/governance files are not premature implementation. Do not delete, edit, or stage them. Do not add files merely to match a count. Unexpected production/test/schema/package/reference changes are blockers.

Historical planning-definition execution inputs must be classified from the accepted manifest/current state rather than guessed.

## 5. Predecessor Closure
Verify without mutation:
- PR #152 MERGED;
- issues #138–#151: 14/14 Closed/Done;
- milestone #54 CLOSED;
- legacy milestone #44 CLOSED and empty;
- PR #167 MERGED;
- issues #153–#166: 14/14 Closed/Done;
- milestone #45 CLOSED;
- Release 1.4 formally closed.

## 6. Release 1.5 GitHub Planning State
Verify milestone #46 is titled `Phase 4 - Release 1.5: Deterministic Research Experiment Foundation`, is OPEN, and begins WP01 with 13 open / 0 closed issues.

Verify exact inventory:
- WP01 #168 — Release & Repository Preflight
- WP02 #169 — Experiment Semantic Discovery
- WP03 #170 — Experiment Identity, Provenance & Evidence
- WP04 #171 — Experiment Model & Contracts
- WP05 #172 — Deterministic Summary Computation
- WP06 #173 — Experiment Validation & Failure Semantics
- WP07 #174 — Feature-to-Experiment Integration
- WP08 #175 — Dependency Registration & Configuration
- WP09 #176 — One-Shot Worker Experiment Execution
- WP10 #177 — Application Experiment Tests
- WP11 #178 — Composition & Worker Validation
- WP12 #179 — Architecture & Documentation Alignment
- WP13 #180 — Full Validation, Integration & Acceptance

All 13 must belong to milestone #46 and Project #2, start Backlog, have Priority P1 and Release 1.5, preserve accepted Areas/assignee, and follow:
`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08 → WP09 → WP10 → WP11 → WP12 → WP13`.

Verify WP14+ = 0, Release 1.6 work = 0, and no Release 1.5 implementation branch/PR exists.

## 7. WP01 Lifecycle Start
Only after all starting-state gates pass, transition #168 Project #2 Status from Backlog to In Progress and read it back.

If #168 is already In Progress solely because this exact WP01 run partially started, continue idempotently only if no unauthorized mutation occurred. If #168 is Closed/Done before validation, stop.

#169 must remain Open/Backlog throughout WP01.

## 8. Architecture Baseline
Verify unchanged acyclic production graph:
- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Unexpected edges = 0; cycles = 0. Do not add Architecture.Tests. Expected Architecture.Tests baseline: 13/13.

## 9. Persistence Baseline
Verify SQLite schema is exactly v2. Existing governed historical-observation and immutable dataset snapshot/catalog persistence remains intact.

Release 1.5 must not yet introduce experiment tables, experiment registry/history, feature persistence expansion, scheduler/checkpoint state, pipeline/experiment run history, or schema v3.

## 10. Package / Project / Reference Baseline
Verify central package management, SDK configuration, solution project inventory, and project references remain at the accepted Release 1.4 baseline.

Expected WP01 package/project/reference delta: `0/0/0`. Do not update packages or SDK versions.

## 11. Premature Implementation Audit
Exclude accepted planning/governance artifacts. Verify no production/test implementation exists yet for:
- `simple-return-descriptive-summary-v1`;
- `aiq-experiment-identity-v1`;
- experiment Definition/Result model;
- experiment computation or validation;
- feature-to-experiment orchestration;
- experiment DI registration;
- experiment Worker mode;
- experiment persistence;
- Release 1.5 permanent tests.

Verify no Release 1.6 capability work exists. Generic planning/documentation references to experiments are not implementation by themselves.

## 12. Release Boundary
Confirm planning remains consistent with:
- input: accepted `simple-return-lag-1-v1` Feature Set evidence;
- result: immutable count/mean/min/max evidence;
- identity: `aiq-experiment-identity-v1`;
- semantic ownership: Application;
- persistence: none;
- provider/network dependency: none;
- SQLite schema v2;
- production graph unchanged.

Confirm deferred work remains absent: experiment persistence/registry/workspace, broader feature libraries, notebooks/APIs, strategies/backtesting, portfolio/risk, AI/ML/MLOps, acquisition orchestration, scheduling/retries/recovery/checkpoints, generalized plugins/DAGs/distributed execution, and durable history/telemetry.

## 13. Canonical Validation
Run `eng/verify.ps1 -Configuration Release` using the established platform-equivalent command only if required.

Expected:
- Restore PASS
- Formatting PASS
- Gitleaks PASS
- Release build PASS
- warnings/errors 0/0
- Domain.Tests 11/11
- Application.Tests 86/86
- Infrastructure.Tests 104/104
- Architecture.Tests 13/13
- permanent total 214/214
- skipped 0 unless accepted repository truth explicitly says otherwise

A count mismatch is a blocker unless explained by accepted pre-WP01 history. Do not edit tests to force the count. No provider execution or real credentials are required.

## 14. Whitespace / Security / Residue
Run `git diff --check` and `git diff --cached --check`. Directly inspect relevant untracked governance files for trailing whitespace.

Require trailing whitespace 0, Gitleaks PASS, real credentials 0, provider calls 0, database/WAL/SHM/journal/generated residue 0, and temporary validation artifacts remaining 0. Do not stage files for validation.

## 15. Repository Mutation Protection
Repository-content mutation budget: `0`.
Git transport mutation budget: `0`.

Do not edit code/tests/docs/governance, alter schema/packages/projects/references, stage, commit, create/switch integration branches, push, create/merge PRs, tag, release, or delete accepted untracked artifacts.

## 16. Authorized GitHub Mutations
Only:
1. after starting gates pass, #168 Backlog → In Progress;
2. on successful completion, post one concise completion-evidence comment to #168;
3. close #168 as completed;
4. set #168 Project #2 Status to Done.

Do not mutate #169+, milestone #46 state, predecessor lifecycle, PRs/tags/releases, or Release 1.6.

## 17. Completion Gate
Close WP01 only if all required evidence passes: correct repository/account; synchronized accepted main; working-tree classification reconciled; predecessor closure correct; milestone #46 and #168–#180 exact; #168 In Progress during execution; #169 Open/Backlog; graph unchanged; schema v2; package/project/reference delta 0; premature Release 1.5 implementation 0; Release 1.6 work 0; canonical verification PASS; 214/214 permanent tests; 13/13 Architecture.Tests; warnings/errors 0/0; Gitleaks/whitespace PASS; residue 0; repository mutation 0.

If any gate fails, do not close #168 or mark Done. Report the smallest corrective authority required.

## 18. Completion Evidence
On success, comment on #168 with concise evidence covering synchronized SHA, predecessor closure, exact Release 1.5 planning inventory, schema v2, unchanged graph, zero premature implementation, 214/214 tests, 13/13 Architecture.Tests, canonical verification/Gitleaks/whitespace PASS, zero repository mutation, and #169 preserved Open/Backlog.

## 19. Final Read-Back
Verify:
- #168 CLOSED / Done;
- #169 OPEN / Backlog;
- milestone #46 OPEN with 12 open / 1 closed;
- WP03–WP13 unchanged;
- Release 1.6 work 0;
- staged paths 0;
- tracked modifications caused by WP01 0;
- commits/branches/pushes/PRs 0.

Report accepted untracked Release 1.5 governance artifacts accurately.

## 20. Stop Conditions
Stop without unauthorized repair if repository/account is wrong, accepted main cannot be reconciled, predecessor closure differs, planning inventory is not exact, milestone #46 has unrelated issues, #169+ started unexpectedly, premature implementation exists, Release 1.6 work exists, schema is not v2, architecture/package/project/reference baseline drifted, canonical verification fails, test baseline cannot be reconciled, or security/whitespace/residue gates fail.

## 21. Required Report
Report executive summary; authorities; repository/authentication; Git/working tree; predecessor closure; milestone/WP inventory; WP01 lifecycle; architecture; schema; package/project/reference baseline; premature implementation/Release 1.6 audit; canonical validation/test counts; security/whitespace/residue; repository/Git mutation accounting; GitHub mutations; final #168/#169/milestone state; findings/blockers; next WP.

## 22. Terminal Marker
On success end exactly:

`RELEASE 1.5 WP01 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP02 — Experiment Semantic Discovery — GitHub issue #169`

Do not begin WP02.

If blocked end:

`RELEASE 1.5 WP01 BLOCKED`

and identify the smallest corrective authority required.
