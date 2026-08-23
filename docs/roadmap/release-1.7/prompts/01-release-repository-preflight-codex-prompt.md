# Release 1.7 WP01 --- Release & Repository Preflight --- Codex Authority

## 1. Mission

Execute Release 1.7 WP01 --- **Release & Repository Preflight** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#197`

Authoritative predecessor baseline:

`95745fc2289ea855af39ba5e7bc0236a67f1c48b`

Authoritative Release 1.7 milestone:

`#55 — Phase 4 - Release 1.7: Durable Experiment Evidence Discovery`

WP01 is a **read-only/preflight work package**.

Its purpose is to prove that Release 1.7 begins from the accepted
Release 1.6 baseline, that the reconciled Release 1.7 planning state is
intact, and that no implementation or repository drift exists before
WP02 begins.

No production, test, schema, package, project, reference, current-state
documentation, or governed planning-artifact mutation is authorized.

------------------------------------------------------------------------

## 2. Authoritative Release 1.7 Planning Inputs

Read completely:

-   `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md`

Treat these three files as the governing Release 1.7 planning
authorities.

Do not modify them.

Confirm they agree on:

-   objective: Durable Experiment Evidence Discovery;
-   query: exact Snapshot Identity + Experiment Definition Identity;
-   positive bounded maximum cardinality;
-   ordering by Experiment Result Identity ascending;
-   successful empty collection semantics;
-   reuse of `aiq-experiment-identity-v1`;
-   no discovery identity;
-   schema v3 preservation;
-   explicit one-shot discovery Worker mode;
-   Release 1.6 failure vocabulary;
-   zero-delta-first Architecture.Tests policy;
-   WP01--WP13 linear execution plan;
-   registry/history/search/mutation/scheduling/provider/Release 1.8
    exclusions.

If the three planning authorities materially disagree, stop.

------------------------------------------------------------------------

## 3. Execution-Only Prompt Classification

This WP01 prompt pair is execution-only out-of-band input:

-   `docs/roadmap/release-1.7/prompts/01-release-repository-preflight-codex-prompt.md`
-   `docs/roadmap/release-1.7/prompts/01-release-repository-preflight-codex-prompt-chat.md`

If materialized as untracked repository content, it is expected during
execution.

It must:

-   remain unstaged;
-   remain uncommitted;
-   not be counted as a governed Release 1.7 implementation path;
-   not be treated as part of the three accepted planning artifacts.

Follow the established repository prompt-lifecycle convention. Do not
mechanically remove this WP authority if Release work-package prompts
are intended to remain governed Release candidate content under the
accepted manifest/conventions. If repository evidence shows this pair is
execution-only rather than governed, report that classification before
cleanup.

Do not let self-reference create a false preflight blocker.

------------------------------------------------------------------------

## 4. Mandatory Repository Starting Gate

Verify:

-   repository identity is correct;
-   current branch is `main`;
-   local HEAD is exactly: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main` resolves to the same SHA;
-   ahead/behind is `0/0`;
-   staged paths = 0;
-   tracked modifications = 0;
-   no merge/rebase/cherry-pick/revert operation is active;
-   no conflict markers exist;
-   no unexpected repository lock/state exists.

Expected untracked governed planning files:

1.  `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md`
2.  `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md`
3.  `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md`

The WP01 prompt pair may additionally be present according to Section 3.

Any other unexplained path must be reported and must block WP01
completion unless repository convention clearly classifies it.

------------------------------------------------------------------------

## 5. Release 1.6 Closure Proof

Verify the accepted predecessor lifecycle:

-   PR #196 is MERGED;
-   merge commit is: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   Release 1.6 milestone #47 is CLOSED;
-   Release 1.6 issues #182--#195 are 14/14 CLOSED / Done;
-   predecessor Release restoration is 89/89 exact;
-   Release 1.6 implementation branch state does not affect `main`;
-   no unresolved Release 1.6 planning or implementation issue remains.

Do not mutate Release 1.6 state.

------------------------------------------------------------------------

## 6. Release 1.7 GitHub Planning Proof

Read back milestone #55.

Require:

-   title:
    `Phase 4 - Release 1.7: Durable Experiment Evidence Discovery`;
-   state: OPEN;
-   open issues: 13;
-   closed issues: 0;
-   issues exactly #197--#209.

Read back WP issues:

-   #197 --- WP01
-   #198 --- WP02
-   #199 --- WP03
-   #200 --- WP04
-   #201 --- WP05
-   #202 --- WP06
-   #203 --- WP07
-   #204 --- WP08
-   #205 --- WP09
-   #206 --- WP10
-   #207 --- WP11
-   #208 --- WP12
-   #209 --- WP13

Require before WP01 lifecycle mutation:

-   #197: OPEN / Backlog;
-   #198--#209: OPEN / Backlog;
-   all 13 assigned to `samuel-santos-engineer`;
-   milestone coverage: 13/13;
-   Project #2 membership: 13/13;
-   duplicates: 0;
-   Status Backlog: 13/13;
-   Priority P1: 13/13;
-   Release 1.7: 13/13;
-   Area populated according to authoritative planning: 13/13;
-   dependency graph: 12 accepted linear edges;
-   dependency drift: 0.

If this state materially differs before WP01 begins, stop.

------------------------------------------------------------------------

## 7. Legacy Planning Read-Back

Verify historical milestones remain reconciled:

-   milestone #48: CLOSED and empty, historical title unchanged;
-   milestone #49: CLOSED and empty, historical title/metadata
    unchanged.

Treat closure of these milestones as planning reconciliation only.

Do not infer that the historical capabilities themselves are cancelled.

Do not reopen, rename, repurpose, or mutate either milestone under WP01.

------------------------------------------------------------------------

## 8. Release 1.7 Absence-of-Implementation Proof

Prove that Release 1.7 implementation has not started.

At minimum verify:

-   Release 1.7 implementation branches: 0;
-   Release 1.7 PRs: 0;
-   WP02+ implementation changes: 0;
-   schema remains v3;
-   no new production project;
-   no new package;
-   no new project reference;
-   no new production dependency edge;
-   no Release 1.7 Worker implementation;
-   no discovery contract/use-case/store implementation;
-   no discovery schema/index/migration;
-   no Release 1.8 implementation.

Do not infer absence only from branch names; inspect repository state
sufficiently to prove no implementation delta exists from the frozen
baseline.

------------------------------------------------------------------------

## 9. Schema-v3 Baseline Proof

Inspect the accepted Release 1.6 physical persistence state.

Confirm:

-   SQLite schema version is 3;
-   `experiment_results` exists as accepted;
-   no Release 1.7 discovery table exists;
-   no Release 1.7 discovery index exists;
-   no schema-v4 migration exists;
-   existing Release 1.6 durable acceptance/retrieval semantics remain
    present.

WP01 must not benchmark or redesign the Release 1.7 query.

That belongs to WP06.

WP01 only freezes the starting physical baseline.

------------------------------------------------------------------------

## 10. Production Dependency and Package Baseline

Record and verify:

-   project set;
-   project-reference graph;
-   package set;
-   production dependency graph;
-   DI baseline relevant to durable Experiment evidence;
-   Worker routing baseline.

Require no drift from accepted Release 1.6 state.

Do not modify dependency registration or Worker configuration.

------------------------------------------------------------------------

## 11. Test Baseline

Canonical permanent baseline must be:

-   Domain.Tests: 11/11;
-   Application.Tests: 111/111;
-   Infrastructure.Tests: 117/117;
-   Architecture.Tests: 13/13;
-   total: 250/250;
-   skipped: 0.

Run canonical verification:

`eng/verify.ps1 -Configuration Release`

Require:

-   restore: PASS;
-   formatting: PASS;
-   Gitleaks: PASS;
-   Release build: PASS;
-   warnings: 0;
-   errors: 0;
-   all permanent tests PASS.

If test counts differ before any authorized WP01 mutation, stop and
report the discrepancy.

WP01 authorizes no test changes.

------------------------------------------------------------------------

## 12. Security, Offline, and Residue Baseline

Verify:

-   Gitleaks: PASS;
-   provider calls: 0;
-   network product activity: 0;
-   real credentials used: 0;
-   temporary database residue: 0;
-   WAL/SHM/journal residue: 0;
-   temporary validation projects/probes: 0;
-   orphan Worker processes created by WP01: 0.

Ordinary GitHub/Git metadata reads needed for lifecycle verification are
not product/provider execution.

Do not invoke market-data providers.

------------------------------------------------------------------------

## 13. Process-Level Validation Prerequisite Read-Back

WP01 does not execute the future Worker discovery process matrix.

It must only confirm that planning has already resolved the future
prerequisite mechanism.

Read back and record the accepted mechanism:

-   Infrastructure test-host `TemporaryDatabase`;
-   deterministic `DatasetSnapshotCandidate`;
-   `SqliteDatasetSnapshotStore.Store(...)`;
-   production durable acceptance path for Experiment Result seeding;
-   existing `--no-build` Worker runner;
-   current friend-assembly boundary;
-   deterministic expected evidence;
-   complete process/database residue cleanup.

Confirm the mechanism is represented in the Release 1.7 planning
authorities.

Do not construct synthetic durable state during WP01.

Do not rediscover or redesign the mechanism.

------------------------------------------------------------------------

## 14. WP06 Structural-Schema Stop Gate Read-Back

Confirm the accepted Release 1.7 governance rule:

-   schema v3 is the planned Release 1.7 baseline;
-   WP06 must prove the bounded physical query plan;
-   any requirement for table/column/index/migration structural mutation
    blocks execution and requires separate authority;
-   WP07 cannot silently introduce schema mutation.

WP01 does not decide the WP06 query plan.

It only proves this governance gate is present before execution begins.

------------------------------------------------------------------------

## 15. Architecture-Test Baseline

Confirm:

-   Architecture.Tests baseline: 13;
-   accepted policy: zero-delta-first;
-   no Release 1.7 architecture test has been added;
-   current rules continue protecting the Release 1.6 dependency
    boundaries.

Do not add Architecture.Tests under WP01.

------------------------------------------------------------------------

## 16. WP01 GitHub Lifecycle

Only after all mandatory preflight gates pass:

1.  move issue #197 from Backlog to In Progress;
2.  perform the WP01 read-only verification;
3.  post concise completion evidence to #197;
4.  close #197;
5.  set #197 Project Status to Done.

After completion require:

-   #197: CLOSED / Done;
-   #198--#209: OPEN / Backlog;
-   milestone #55: OPEN, 12 open / 1 closed;
-   Project membership remains 13/13;
-   Priority/Release/Area fields remain unchanged;
-   dependency graph remains unchanged.

Do not transition #198 automatically.

------------------------------------------------------------------------

## 17. Authorized Mutations

### Repository content

`0`

### Git transport

`0`

### GitHub

Only WP01 lifecycle mutations for issue #197:

-   Backlog → In Progress;
-   completion evidence comment;
-   close issue;
-   Project Status → Done.

No other GitHub mutation is authorized.

Do not mutate:

-   milestone #55 metadata;
-   #198--#209;
-   #47/#48/#49;
-   predecessor issues;
-   labels;
-   assignees;
-   dependencies;
-   Project Release/Area/Priority fields.

------------------------------------------------------------------------

## 18. Diff and Working-Tree Validation

Require before completion:

-   `git diff --check`: PASS;
-   `git diff --cached --check`: PASS;
-   staged paths: 0;
-   tracked modifications: 0;
-   no implementation files created;
-   no database/probe residue;
-   no unexpected untracked paths.

The three governed Release 1.7 planning artifacts remain untracked
unless separately integrated later.

Do not stage them.

------------------------------------------------------------------------

## 19. Stop Conditions

Stop with:

`RELEASE 1.7 WP01 BLOCKED`

if:

-   frozen baseline differs;
-   `main != origin/main`;
-   repository tracked/staged state is dirty;
-   unexplained untracked paths exist;
-   Release 1.6 closure state differs materially;
-   milestone #55 / #197--#209 planning state differs materially;
-   Project #2 configuration has unexplained drift;
-   predecessor restoration is not 89/89;
-   schema baseline is not v3;
-   Release 1.7 implementation already exists;
-   canonical baseline is not 250/250;
-   package/project/reference graph drift exists;
-   process-validation prerequisite is absent from planning;
-   WP06 structural-schema stop gate is absent;
-   completing WP01 would require
    production/test/schema/planning-artifact mutation.

Report the exact blocker and smallest corrective authority required.

------------------------------------------------------------------------

## 20. Required Execution Report

Report:

1.  repository/branch/SHA/ahead-behind;
2.  working-tree classification;
3.  Release 1.6 closure proof;
4.  Release 1.7 planning-artifact consistency;
5.  milestone #55 read-back;
6.  #197--#209 mapping/state;
7.  Project #2 field/dependency coverage;
8.  legacy #48/#49 read-back;
9.  Release 1.7 implementation absence;
10. schema-v3 baseline;
11. package/project/reference/dependency baseline;
12. Worker/DI baseline;
13. process-validation prerequisite read-back;
14. WP06 schema-stop-gate read-back;
15. canonical verification;
16. security/offline/residue proof;
17. repository mutation accounting;
18. GitHub lifecycle mutation accounting;
19. final #197/#198 state;
20. milestone #55 final counts;
21. next authorized action.

------------------------------------------------------------------------

## 21. Completion Markers

On success end exactly:

`RELEASE 1.7 WP01 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP02 — Durable Evidence Discovery Semantics — GitHub issue #198`

Do not execute WP02 automatically.

If blocked end exactly:

`RELEASE 1.7 WP01 BLOCKED`

and identify the smallest corrective authority required.
