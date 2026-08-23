# Release 1.6 WP01 — Release & Repository Preflight — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP01 — Release & Repository Preflight — GitHub issue #182**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP01 is a verification and lifecycle-preflight work package. It must establish that the accepted Release 1.6 definition/planning state is safe to execute before any semantic, production, schema-v3, persistence, Worker, or permanent-test implementation begins.

Repository-content mutation budget:

`0`

WP01 must not implement Release 1.6 behavior.

---

## 2. Authoritative Baseline

The authoritative predecessor baseline is:

`18dfb01bf3503d91415b081b11fcdd7249094373`

Verify before any lifecycle mutation:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main == 18dfb01bf3503d91415b081b11fcdd7249094373`;
- ahead/behind: `0/0`;
- no staged tracked paths;
- no unexpected tracked modifications;
- Release 1.5 PR #181: MERGED;
- Release 1.5 milestone #46: CLOSED;
- Release 1.5 issues #168–#180: closed;
- implemented SQLite schema: v2;
- permanent predecessor baseline: 238 tests;
- Architecture.Tests baseline: 13;
- solution projects: 8;
- production dependency graph unchanged and acyclic.

If the authoritative predecessor baseline has materially drifted, stop before changing #182.

---

## 3. Governing Release 1.6 Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- accepted Release 1.6 GitHub planning authority
- accepted Project #2 Release-field restoration/reconciliation authority
- accepted Release 1.6 definition-state reconciliation authority
- this WP01 authority
- its five-line companion

Historical planning-definition starting-state requirements are superseded by the accepted definition-state reconciliation. Do not reapply them.

Existing accepted Release 1.6 governance artifacts and completed GitHub planning are expected state, not premature implementation.

---

## 4. WP01 Scope

WP01 must verify and freeze the execution starting point for WP02–WP14.

Required areas:

1. repository identity and synchronization;
2. predecessor Release 1.5 closure;
3. accepted Release 1.6 definition/plan/manifest coherence;
4. Release 1.6 GitHub planning state;
5. Project #2 restoration/preservation;
6. working-tree governance classification;
7. architecture/dependency baseline;
8. project/package/reference baseline;
9. SQLite schema-v2 baseline;
10. persistence/storage baseline;
11. Release 1.5 experiment baseline;
12. absence of premature Release 1.6 implementation;
13. absence of Release 1.7 work;
14. canonical verification;
15. whitespace/security/residue checks;
16. bounded GitHub lifecycle completion for issue #182.

No design or implementation decision beyond the accepted authorities is permitted.

---

## 5. Release 1.6 Semantic Boundary Verification

Confirm the accepted Release 1.6 capability remains:

**Durable Experiment Evidence Foundation**

Verify the planning authorities still establish:

- persisted artifact: accepted Release 1.5 Experiment Result only;
- identity: reuse `aiq-experiment-identity-v1`;
- no new persistence identity;
- exact lookup by Experiment Result Identity;
- `NewlyAccepted`;
- `EquivalentExisting`;
- contradictory evidence under the same identity → `IntegrityConflict`;
- exact empty/non-empty aggregate fidelity;
- atomic non-destructive SQLite v2→v3 evolution is planned but not implemented;
- restart-safe exact retrieval is planned;
- separate explicit durable-experiment Worker mode is planned;
- existing Release 1.5 in-memory experiment path remains unchanged;
- Feature Set persistence is excluded;
- registry/history/search is excluded;
- update/delete is excluded;
- provider acquisition/fallback is excluded;
- strategies/signals/backtesting are excluded;
- scheduling/retry/recovery framework is excluded;
- Release 1.7 work is excluded.

WP01 must not refine these semantics.

---

## 6. GitHub Starting Gate

Before changing issue #182, read back:

### Milestone #47

Require:

- title: `Phase 4 - Release 1.6: Durable Experiment Evidence Foundation`;
- state: OPEN;
- 14 open / 0 closed;
- issues #182–#195 associated;
- no unrelated issue associations.

### Issues

Require:

- #182–#195 exist;
- all are OPEN;
- #182 is WP01;
- #183 is WP02;
- #196 or WP15+ Release 1.6 issue: absent;
- duplicate WP identities: 0.

### Project #2

For #182–#195 require:

- membership exactly once;
- duplicate items: 0;
- Status: Backlog;
- Priority: P1;
- Release: 1.6;
- Area: authoritative;
- dependency drift: 0.

Also verify predecessor Release-field restoration remains intact:

- predecessor release-scoped rows expected: 89;
- exact restored predecessor Release values: 89/89;
- ambiguous predecessor assignments: 0;
- predecessor Status/Priority/Area drift: 0.

If Project #2 has materially regressed, stop before starting WP01.

---

## 7. Authorized GitHub Lifecycle Mutations

Only after all mandatory starting gates pass, WP01 may perform exactly this lifecycle:

1. #182 Project Status:
   `Backlog → In Progress`
2. Execute the WP01 verification.
3. Post concise completion evidence to #182.
4. Close #182.
5. Set #182 Project Status:
   `In Progress → Done`

No other Release 1.6 issue may change state or Project Status.

At completion:

- #182: CLOSED / Done;
- #183: OPEN / Backlog;
- #184–#195: OPEN / Backlog;
- milestone #47: OPEN, 13 open / 1 closed.

Do not close milestone #47.

---

## 8. Working-Tree Governance Classification

Inspect all untracked paths.

Existing Release 1.6 planning/governance artifacts are expected and must be classified according to the accepted manifest/reconciliation authorities.

At minimum distinguish:

- governed future candidate artifacts;
- historical out-of-band execution authorities;
- corrective out-of-band execution authorities;
- WP01 authority pair.

Do not:

- delete them;
- stage them;
- commit them;
- reinterpret them as production implementation.

Report:

- total untracked paths;
- expected governed candidate paths;
- expected out-of-band paths;
- unexpected paths.

Required:

`Unexpected paths = 0`

If an unexpected path exists, classify it before proceeding. If it may represent premature implementation or unrelated work, stop.

---

## 9. Premature Release 1.6 Implementation Audit

Prove Release 1.6 implementation has not started.

Search/reconcile for unauthorized new:

- Application persistence contracts;
- durable experiment use cases;
- experiment-result repositories/stores;
- SQLite experiment-result tables;
- schema-v3 migrations;
- Infrastructure experiment persistence/retrieval;
- storage failure mapping specific to Release 1.6;
- durable-experiment Worker execution;
- Release 1.6 permanent tests;
- Release 1.6 production configuration;
- packages;
- project references;
- solution projects;
- implementation branches;
- PRs.

Planning, semantic documentation, prompts, and governance authorities are not implementation.

Required premature implementation findings:

`0`

---

## 10. Release 1.7 Exclusion Audit

Verify there is no Release 1.7:

- definition;
- execution plan;
- file manifest;
- milestone repurposing;
- issues;
- Project release planning;
- production implementation;
- branch;
- PR.

Do not classify generic historical roadmap placeholders/templates as Release 1.7 work unless they constitute actual Release 1.7 planning or implementation.

Required:

`Release 1.7 work = 0`

---

## 11. Architecture Baseline

Verify the production graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Require:

- unexpected edges: 0;
- dependency cycles: 0;
- Domain production delta for Release 1.6: 0;
- new production project: 0;
- architecture rule count: 13;
- Architecture.Tests: 13/13.

WP01 must not add an architecture rule.

---

## 12. Solution / Package / Reference Baseline

Verify:

- solution projects: 8;
- package delta: 0;
- project delta: 0;
- project-reference delta: 0;
- no new external dependency for Release 1.6;
- existing package-management conventions unchanged.

WP01 authorizes no package/project/reference mutation.

---

## 13. SQLite and Persistence Baseline

Verify implemented schema remains exactly:

`v2`

Confirm:

- no schema-v3 implementation;
- no experiment-result persistence table;
- no Feature Set persistence table;
- no experiment registry/history table;
- existing Release 1.1–1.5 SQLite objects remain intact;
- existing migration behavior remains unchanged;
- no update/delete semantics added;
- no generated database migration residue exists.

The planned v2→v3 evolution belongs to later WPs.

---

## 14. Release 1.5 Experiment Baseline

Verify the accepted Release 1.5 experiment capability remains present and unchanged:

- `simple-return-descriptive-summary-v1`;
- `aiq-experiment-identity-v1`;
- immutable Experiment Result evidence;
- exact Feature Set binding;
- deterministic count/mean/minimum/maximum;
- empty-result success semantics;
- provenance and acyclic lineage;
- bounded validation/failure behavior;
- Application ownership;
- explicit one-shot in-memory Experiment Worker mode;
- no experiment persistence;
- no provider fallback.

WP01 must not alter Release 1.5 behavior.

---

## 15. Canonical Verification

Run:

`eng/verify.ps1 -Configuration Release`

Required:

- restore: PASS;
- formatting: PASS;
- Gitleaks: PASS;
- Release build: PASS;
- warnings: 0;
- errors: 0;
- Domain.Tests: 11/11;
- Application.Tests: 102/102;
- Infrastructure.Tests: 112/112;
- Architecture.Tests: 13/13;
- permanent total: 238/238;
- skipped: 0.

Do not update baselines during WP01.

Any unexplained baseline delta is a blocker.

---

## 16. Whitespace and Governance Validation

Require:

- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- staged paths: 0;
- direct whitespace check across expected untracked Release 1.6 governance artifacts: PASS;
- terminal-newline requirements: PASS;
- governed five-line companions remain exactly five non-empty logical lines where applicable.

Do not normalize unrelated files under WP01.

If an existing governance formatting defect is discovered, stop and request narrow corrective authority.

---

## 17. Security / Offline / Residue Gate

Require:

- Gitleaks: PASS;
- real credentials used: 0;
- provider/network execution: 0;
- database/WAL/SHM/journal residue: 0;
- temporary probes/projects/worktrees left behind: 0;
- generated implementation residue: 0.

WP01 should not need provider access or live market-data acquisition.

---

## 18. Repository Mutation Budget

Repository content edits:

`0`

Staged paths:

`0`

Commits:

`0`

Branches created:

`0`

Pushes:

`0`

PRs:

`0`

Tags/releases:

`0`

Do not create an integration branch.

Do not commit governance artifacts.

---

## 19. Stop Conditions

Stop before closing #182 if any of the following occurs:

- predecessor baseline SHA drift;
- local/origin divergence;
- unexpected tracked repository modification;
- unexpected untracked implementation;
- Release 1.6 definition/plan/manifest contradiction;
- Project #2 restoration regression;
- incorrect milestone/issue inventory;
- WP02 or later WP already started;
- premature schema-v3 or durable persistence implementation;
- Release 1.7 work;
- dependency graph drift;
- package/project/reference drift;
- canonical test/build failure;
- security/whitespace/residue failure.

If a blocker occurs after #182 was moved to In Progress:

- leave #182 OPEN / In Progress;
- do not close it;
- do not mutate #183–#195;
- report the smallest corrective authority required.

---

## 20. Completion Evidence for #182

If all gates pass, post concise evidence including:

- authoritative baseline SHA;
- main/origin synchronization;
- Release 1.5 closure;
- Release 1.6 planning read-back;
- Project restoration preservation;
- schema v2;
- architecture graph;
- no premature Release 1.6 implementation;
- no Release 1.7 work;
- canonical 238/238;
- 13/13 Architecture.Tests;
- 0 warnings/errors;
- Gitleaks/format/diff/residue PASS;
- repository mutation count 0;
- next authorized WP: WP02/#183.

Do not paste the entire execution report into the issue.

---

## 21. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. repository identity and SHA;
4. Git synchronization;
5. working-tree classification;
6. Release 1.5 closure verification;
7. Release 1.6 definition/plan/manifest reconciliation;
8. milestone #47 read-back;
9. issues #182–#195 read-back;
10. Project #2 Release 1.6 field state;
11. predecessor Release-field restoration preservation;
12. premature implementation audit;
13. Release 1.7 exclusion;
14. architecture graph and Architecture.Tests;
15. solution/package/reference baseline;
16. schema/persistence baseline;
17. Release 1.5 experiment preservation;
18. canonical verification;
19. whitespace/security/residue checks;
20. repository mutation accounting;
21. GitHub mutation accounting;
22. #182 lifecycle;
23. #183 preservation;
24. findings/blockers;
25. next authorized work package.

---

## 22. Completion Marker

On success, end exactly:

`RELEASE 1.6 WP01 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP02 — Durable Experiment Evidence Discovery — GitHub issue #183`

Required final lifecycle:

- #182: CLOSED / Done
- #183: OPEN / Backlog
- #184–#195: OPEN / Backlog
- milestone #47: OPEN

If blocked, end:

`RELEASE 1.6 WP01 BLOCKED`

and identify the smallest corrective authority required.
