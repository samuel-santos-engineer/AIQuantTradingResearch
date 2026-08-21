# Release 1.4 WP01 — Release & Repository Preflight — Codex Authority

## Mission

Execute **WP01 — Release & Repository Preflight** for **Phase 4 — Release 1.4: Deterministic Feature Engineering Foundation**.

WP01 is evidence-first and repository-mutation-free. Its purpose is to prove that Release 1.4 begins from a coherent, formally closed Release 1.3 baseline and from correctly established Release 1.4 governance before semantic or implementation work starts.

WP01 authorizes no Release 1.4 production, test, semantic-artifact, DI, Worker, documentation, integration, or Release 1.5 mutation.

## Governing Authorities

Read completely:

1. `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
2. `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
4. accepted Release 1.4 GitHub-planning authority/result;
5. Release 1.3 post-merge closure authority/result;
6. current repository, tests, architecture, schema, documentation, and GitHub state.

If authorities and repository truth materially conflict, stop without repository mutation.

## Expected Starting State

Verify rather than assume:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- local `main` synchronized with `origin/main`;
- Release 1.3 PR #152 merged;
- Release 1.3 milestone #54 closed;
- issues #138–#151 Closed/Done;
- Release 1.3 accepted merged baseline descends from `0c981bb5765bb519bca3542c745f9282beb7b0d5`;
- Release 1.4 authoritative milestone is Open;
- exactly WP01–WP14 Release 1.4 issues exist;
- WP01 is Open/Backlog;
- WP02–WP14 are Open/Backlog;
- no WP15+;
- no Release 1.5 implementation/planning issue;
- staged paths: 0.

If main has legitimately advanced after Release 1.3 closure, report the exact SHA and prove that the advance does not represent unauthorized Release 1.4 implementation.

## Repository Working-Tree Classification

Inspect all staged, tracked-modified, and untracked paths.

Expected pre-execution Release 1.4 planning/governance artifacts may include:

- `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`;
- `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`;
- `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`;
- intentionally governed Release 1.4 GitHub-planning prompt pair;
- WP01 prompt pair.

Classify each path as:

- accepted Release 1.4 planning/governance;
- explicitly out-of-band authority;
- unexpected;
- generated/residue.

Do not stage or commit anything.

Unexpected production/test/schema/package/reference changes are a blocker.

## Release 1.3 Closure Reconciliation

Independently verify:

- PR #152 state is MERGED;
- accepted Release 1.3 head/merge relationship remains coherent;
- milestone #54 is CLOSED;
- issues #138–#151 are Closed/Done;
- Release 1.3 integration/closure did not leave an open lifecycle requirement;
- Release 1.3 fixed pipeline remains current and is not being modified by Release 1.4 planning.

## Release 1.4 GitHub Planning Reconciliation

Verify:

- exactly one authoritative Release 1.4 milestone;
- accepted milestone title:
  `Phase 4 - Release 1.4: Deterministic Feature Engineering Foundation`;
- milestone is Open;
- exactly fourteen issues WP01–WP14;
- all issues are Open;
- all are Project #2 members;
- all start Backlog/P1/Release 1.4;
- Areas use existing Project values;
- dependency graph exactly matches the execution plan;
- no WP15+, closure issue, lifecycle-gate issue, or Release 1.5 issue;
- WP02 remains untouched while WP01 executes.

## Baseline Architecture Reconciliation

Verify production project references and effective graph remain:

```text
Domain → none
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

Verify:

- cycles: 0;
- no unexpected production reference;
- provider/HTTP concerns remain outside Domain/Application;
- Release 1.3 pipeline semantics remain Application-owned;
- no feature-specific production architecture has already been introduced.

## Persistence and Schema Reconciliation

Verify:

- SQLite schema version remains exactly 2;
- no feature table;
- no feature catalog;
- no feature cache;
- no feature run-history table;
- no scheduler/checkpoint/pipeline-state table;
- Release 1.1 historical persistence remains intact;
- Release 1.2 immutable snapshot/catalog persistence remains intact.

Do not migrate or create a repository database as part of WP01.

## Release 1.4 Premature-Implementation Audit

Search the repository for evidence that Release 1.4 implementation has already started.

In particular inspect for:

- `simple-return-lag-1-v1`;
- `aiq-feature-identity-v1`;
- new feature production namespaces/types;
- feature persistence/schema;
- feature Worker execution;
- feature tests that imply implementation;
- Release 1.5 feature frameworks/plugins/DAGs.

The accepted definition/planning documents and governance prompts do not count as premature implementation.

Any unexplained production/test implementation is a blocker.

## Package, Project, and Reference Baseline

Verify no Release 1.4 planning action changed:

- packages;
- projects;
- project references;
- SDK/global build configuration.

Record the current package/project/reference baseline sufficient for later WP14 comparison.

## Permanent Test Baseline

Run the canonical Release verification:

```powershell
./eng/verify.ps1 -Configuration Release
```

Expected historical baseline from Release 1.3 closure:

- Domain.Tests: 11;
- Application.Tests: 77;
- Infrastructure.Tests: 96;
- Architecture.Tests: 13;
- total: 197;
- skipped: 0;
- build warnings/errors: 0/0;
- Gitleaks: PASS.

Treat these counts as expectations, not permission to ignore repository truth.

If counts changed, identify why. A legitimate non-Release-1.4 mainline change must be reconciled explicitly; unexplained drift blocks WP01 completion.

## Security and Offline Baseline

Verify:

- Gitleaks passes;
- no real credentials are required;
- no live provider call is needed;
- no network-dependent test is required;
- no repository database residue remains after validation;
- no SQLite WAL/SHM/journal residue remains.

GitHub CLI/API reads needed for lifecycle verification are governance operations and do not count as feature-provider/network execution.

## Whitespace and Cleanliness

Run:

- `git diff --check`;
- `git diff --cached --check`;
- direct trailing-whitespace inspection of relevant untracked Release 1.4 governance/planning files;
- generated/database residue inspection.

Do not mechanically fix findings under WP01. Report and stop if correction is required.

## WP01 Mutation Boundary

Repository-content mutations authorized:

`0`

Git transport mutations authorized:

`0`

Do not:

- edit files;
- create semantic artifacts;
- create feature code/tests;
- stage;
- commit;
- push;
- branch;
- create PR;
- merge;
- tag;
- release.

GitHub lifecycle mutation is limited to WP01 itself after all acceptance gates pass:

1. move WP01 from Backlog to In Progress only after starting gates pass;
2. post concise completion evidence;
3. close WP01;
4. set WP01 Project status Done.

Do not mutate WP02 or any successor issue.

## WP01 Acceptance Matrix

WP01 passes only if all are true:

- Release 1.3 formally closed;
- repository/default branch correct;
- local/upstream state coherent;
- Release 1.4 planning objects exact;
- WP dependency graph exact;
- working tree fully classified;
- no unexpected implementation;
- schema = 2;
- production graph unchanged and acyclic;
- packages/projects/references unchanged by Release 1.4 planning;
- canonical verification PASS;
- all permanent tests PASS;
- build warnings/errors 0/0;
- Gitleaks PASS;
- diff/whitespace checks PASS;
- residue = 0;
- no provider execution;
- no Release 1.5 work;
- WP02 remains Open/Backlog and unstarted.

If any mandatory gate fails, do not close WP01.

## Required Final Report

Report at minimum:

1. executive summary;
2. authorities reviewed;
3. repository/authentication state;
4. branch/HEAD/origin/ahead-behind;
5. working-tree classification;
6. Release 1.3 closure proof;
7. Release 1.4 milestone state;
8. WP01–WP14 issue/lifecycle reconciliation;
9. dependency graph reconciliation;
10. Project field reconciliation;
11. architecture graph/cycle result;
12. schema/persistence result;
13. premature-implementation audit;
14. package/project/reference baseline;
15. restore/build result;
16. warnings/errors;
17. Domain/Application/Infrastructure/Architecture test counts;
18. permanent total/skipped;
19. canonical verification;
20. Gitleaks;
21. diff/whitespace checks;
22. database/generated residue;
23. provider/network activity;
24. repository mutation count;
25. Git/GitHub mutation accounting;
26. WP01 final state;
27. WP02 final state;
28. findings/blockers;
29. final decision.

Successful terminal marker:

`RELEASE 1.4 WP01 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP02 — Feature Engineering Semantic Discovery`

If blocked, use:

`RELEASE 1.4 WP01 BLOCKED`

and state the smallest corrective authority required.
