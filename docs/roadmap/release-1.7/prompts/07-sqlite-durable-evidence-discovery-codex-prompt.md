# Release 1.7 WP07 --- SQLite Durable Evidence Discovery --- Codex Authority

## 1. Mission

Execute Release 1.7 WP07 --- **SQLite Durable Evidence Discovery** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#203`

Frozen Release 1.6 baseline:

`95745fc2289ea855af39ba5e7bc0236a67f1c48b`

Authoritative milestone:

`#55 — Phase 4 - Release 1.7: Durable Experiment Evidence Discovery`

Accepted predecessor state:

-   WP01 #197: CLOSED / Done;
-   WP02 #198: CLOSED / Done;
-   WP03 #199: CLOSED / Done;
-   WP04 #200: CLOSED / Done;
-   WP05 #201: CLOSED / Done;
-   WP06 #202: CLOSED / Done;
-   WP07 #203: OPEN / Backlog;
-   #204--#209: OPEN / Backlog;
-   milestone #55: OPEN, 7 open / 6 closed;
-   canonical permanent baseline: 250/250;
-   schema: v3.

Accepted WP06 physical-access decision:

-   exact Snapshot Identity + Experiment Definition Identity filtering
    is feasible on schema v3;
-   explicit binary Experiment Result Identity ascending ordering is
    feasible;
-   caller-supplied positive maximum is safely parameterizable;
-   zero matches are a successful empty result;
-   discovery is read-only;
-   all 19 persisted columns are sufficient for exact
    `DurableExperimentEvidence` reconstruction;
-   `EXPLAIN QUERY PLAN` reports `SCAN experiment_results`;
-   existing binary primary-key ordering supplies the accepted result
    order without a temporary B-tree;
-   the scan is accepted for the bounded Release 1.7 scope;
-   no table, column, index, migration, or schema-version change is
    required.

WP07 converts that accepted physical proof into the smallest permanent
SQLite Infrastructure implementation of
`IDurableExperimentEvidenceDiscoveryStore`.

Do not reopen the WP06 query-plan decision or perform the deferred
Release 1.8 architecture/design review.

------------------------------------------------------------------------

## 2. Authoritative Inputs

Read completely before mutation:

-   `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md`
-   `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE_DISCOVERY.md`
-   `docs/architecture/data/EXPERIMENT_DISCOVERY_IDENTITY_PROVENANCE_FIDELITY.md`
-   `docs/architecture/data/EXPERIMENT_DISCOVERY_PHYSICAL_ACCESS.md`
-   `src/AIQuantTradingResearch.Application/Experiments/ExperimentPersistenceContracts.cs`
-   `src/AIQuantTradingResearch.Application/Experiments/DurableExperimentDiscoveryUseCase.cs`
-   current SQLite Experiment persistence/retrieval implementation;
-   current schema-v3 creation/validation code;
-   current SQLite failure-classification conventions;
-   current Infrastructure tests/helpers for durable Experiment
    evidence;
-   GitHub issue #203.

Treat WP02--WP06 as frozen predecessor authority.

------------------------------------------------------------------------

## 3. Flow Preservation

Preserve the prepared Release 1.7 execution flow.

Do not initiate broad architectural redesign, contract restructuring,
source-layout refactoring, or performance redesign.

Non-blocking architecture/design observations are deferred to the
separately governed Release 1.8 Architecture & Design Review Register.

WP07 exists only to implement the already-proven schema-v3 discovery
persistence boundary.

------------------------------------------------------------------------

## 4. Execution-Authority Lifecycle

The WP07 prompt pair follows the established Release 1.7
execution-authority lifecycle.

Do not:

-   stage it merely because it exists;
-   count it as production/test/schema mutation;
-   remove prior WP prompt pairs without separate authority;
-   allow expected untracked authority files to create a false blocker.

Report authority-file classification in the final execution report.

------------------------------------------------------------------------

## 5. Mandatory Starting Gate

Before any WP07 mutation verify:

-   branch: `main`;
-   HEAD: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main`: same SHA;
-   ahead/behind: `0/0`;
-   staged paths: 0;
-   tracked mutations: 0;
-   no active merge/rebase/cherry-pick/revert;
-   no conflict markers;
-   #197--#202: CLOSED / Done;
-   #203: OPEN / Backlog;
-   #204--#209: OPEN / Backlog;
-   milestone #55: OPEN, 7 open / 6 closed;
-   Project #2 planning state reconciled;
-   schema: v3;
-   canonical baseline: 250 tests;
-   WP04 discovery contracts exist;
-   WP05 use case exists;
-   WP06 physical-access document exists;
-   no permanent WP07 discovery store implementation already exists.

Reconcile expected untracked planning, documentation, and authority
files against the Release 1.7 manifest and completed WPs.

Unexpected tracked/staged state blocks execution.

------------------------------------------------------------------------

## 6. Repository-Native Infrastructure Pattern

Before implementation inspect the accepted Infrastructure persistence
code for Release 1.6 durable Experiment evidence.

At minimum inspect:

-   SQLite connection factory usage;
-   Experiment Result acceptance/store implementation;
-   exact retrieval implementation;
-   schema-v3 validation/opening conventions;
-   SQL command construction;
-   parameter binding;
-   row-reading/mapping;
-   decimal canonicalization;
-   nullable aggregate handling;
-   identity/value-object reconstruction;
-   failure classification;
-   cancellation/disposal conventions;
-   Infrastructure naming/file organization.

Reuse these patterns.

Do not introduce a second SQLite persistence style.

------------------------------------------------------------------------

## 7. Implementation Objective

Implement the permanent SQLite Infrastructure side of:

`IDurableExperimentEvidenceDiscoveryStore`

The implementation must:

1.  receive the accepted typed discovery request inputs through the
    Application abstraction;
2.  query the existing schema-v3 `experiment_results`;
3.  filter by exact Snapshot Identity;
4.  filter by exact Experiment Definition Identity;
5.  order explicitly by Experiment Result Identity ascending;
6.  apply the caller-supplied positive bounded maximum using safe
    parameterization;
7.  reconstruct each matching row as exact `DurableExperimentEvidence`;
8.  return zero-or-more evidence items through the accepted discovery
    result/store contract;
9.  preserve the accepted bounded Infrastructure failure semantics;
10. remain read-only.

No other behavior is authorized.

------------------------------------------------------------------------

## 8. SQL Shape

Implement the WP06-proven logical query shape only.

The permanent query must semantically be equivalent to:

-   exact Snapshot Identity equality;
-   exact Experiment Definition Identity equality;
-   `ORDER BY Experiment Result Identity ASC`;
-   parameterized `LIMIT`.

Use actual accepted schema-v3 column names and repository SQL
conventions.

Do not rely on:

-   insertion order;
-   implicit primary-key scan order without explicit `ORDER BY`;
-   `rowid`;
-   timestamp ordering;
-   in-memory sorting as a substitute for SQL ordering.

Do not add optional predicates.

------------------------------------------------------------------------

## 9. Safe Parameterization

All caller-controlled query values must use the repository-native SQLite
parameter mechanism.

This includes:

-   Snapshot Identity;
-   Experiment Definition Identity;
-   maximum.

Do not concatenate caller values into SQL.

Do not silently:

-   clamp maximum;
-   substitute a default;
-   turn zero into unbounded;
-   reinterpret negative values.

Application-owned invalid request handling remains in WP05.
Infrastructure should preserve the accepted contract and not invent a
second policy.

------------------------------------------------------------------------

## 10. Deterministic Ordering

Returned evidence must preserve the frozen ordering:

`Experiment Result Identity ascending`

Use the exact persisted binary/text representation and comparison
semantics proven in WP06.

Do not:

-   introduce a discovery ordinal;
-   order by insertion sequence;
-   order by Snapshot;
-   order by Experiment Definition;
-   order by timestamps;
-   reorder after retrieval unless required by the accepted store
    contract and repository conventions.

The permanent implementation must match the WP06 physical proof.

------------------------------------------------------------------------

## 11. Exact Evidence Reconstruction

WP06 proved that all 19 persisted columns are sufficient.

WP07 must reconstruct `DurableExperimentEvidence` with no semantic loss.

Preserve exactly:

-   Experiment Result Identity;
-   identity algorithm/version semantics;
-   Snapshot Identity;
-   Snapshot Version;
-   Experiment Definition Identity/provenance;
-   Feature Set Identity/lineage;
-   result count;
-   aggregate presence/absence;
-   mean;
-   minimum;
-   maximum;
-   canonical decimal fidelity;
-   signed-zero behavior;
-   every other accepted persisted provenance/fidelity field represented
    by the existing evidence contract.

Reuse existing Release 1.6 row-mapping helpers where appropriate.

Do not duplicate canonicalization logic if a repository-native
implementation already exists.

Do not invent a discovery-specific evidence DTO.

------------------------------------------------------------------------

## 12. Successful Empty Discovery

Zero matching rows must return a successful empty discovery collection.

It must not become:

-   `NotFound`;
-   `InvalidEvidence`;
-   `DependencyUnavailable`;
-   `IntegrityConflict`;
-   an exception.

This is a central Release 1.7 semantic invariant.

------------------------------------------------------------------------

## 13. Read-Only Guarantee

WP07 discovery must perform no durable mutation.

Forbidden:

-   `INSERT`;
-   `UPDATE`;
-   `DELETE`;
-   acceptance/upsert calls;
-   repair;
-   reconciliation;
-   schema mutation;
-   index creation;
-   migration;
-   provider acquisition.

The store implementation must only read accepted schema-v3 state.

Validation must prove row/state equality before and after representative
discovery operations.

------------------------------------------------------------------------

## 14. Failure Semantics

Reuse the established Release 1.6 SQLite failure classification and the
accepted five-value vocabulary:

-   `InvalidRequest`;
-   `NotFound`;
-   `DependencyUnavailable`;
-   `InvalidEvidence`;
-   `IntegrityConflict`.

Do not add a discovery-specific failure.

For discovery:

-   valid zero-match result is success, never `NotFound`;
-   malformed persisted evidence encountered during reconstruction must
    follow the existing accepted evidence-validation/failure boundary;
-   unavailable SQLite storage must map according to the existing
    bounded Infrastructure convention;
-   unknown defects must propagate according to current repository
    rules.

Do not broadly catch and normalize all exceptions.

Do not retry.

Do not repair.

Do not fall back.

------------------------------------------------------------------------

## 15. Invalid Persisted Evidence

If the existing durable retrieval implementation already has an
authoritative path for validating/reconstructing persisted evidence,
reuse it or extract only a manifest-authorized shared helper if
necessary.

Do not weaken validation to make discovery easier.

If one selected row is invalid according to accepted Release 1.6
evidence invariants:

-   preserve the accepted bounded failure semantics;
-   do not skip the row silently;
-   do not return a partial successful collection unless the frozen
    semantics explicitly authorize that behavior;
-   do not repair the row.

If the existing architecture does not determine the correct
collection-level behavior and WP02/WP03 do not resolve it, stop and
report the semantic gap rather than inventing policy.

------------------------------------------------------------------------

## 16. Connection and Resource Ownership

Follow repository-native SQLite connection ownership and disposal
conventions.

Requirements:

-   no leaked connection;
-   no leaked command;
-   no leaked reader;
-   cancellation semantics consistent with existing Infrastructure code;
-   no long-lived static connection;
-   no global mutable state.

Do not introduce pooling/configuration policy changes.

------------------------------------------------------------------------

## 17. Schema-v3 Preservation

WP07 must not modify:

-   schema version;
-   `experiment_results` columns;
-   table definitions;
-   primary/unique keys;
-   indexes;
-   migrations.

The accepted WP06 result is that schema v3 is sufficient.

If permanent implementation unexpectedly proves otherwise, stop with a
blocker.

Do not add the tempting composite filter index during WP07.

Performance optimization is outside this authority.

------------------------------------------------------------------------

## 18. WP08+ Boundary Protection

Read the Release 1.7 execution plan to preserve later work-package
ownership.

WP07 must not consume later authority for:

-   DI registration/composition;
-   Worker configuration;
-   Worker routing/precedence;
-   Worker output;
-   process-level discovery execution;
-   broad permanent acceptance matrix;
-   architecture/documentation alignment beyond manifest-authorized WP07
    artifacts;
-   full release validation.

Only the permanent Infrastructure store implementation and explicitly
WP07-owned focused tests are authorized.

------------------------------------------------------------------------

## 19. Manifest-Bounded Mutation

Read `RELEASE_1.7_FILE_MANIFEST.md` before editing.

Only create/modify WP07-authorized paths.

For every changed path report:

-   new or modified;
-   why WP07 owns it;
-   production or test;
-   why it does not consume a later WP.

Do not opportunistically:

-   split unrelated files;
-   rename predecessor types;
-   reorganize namespaces;
-   refactor the Worker;
-   alter Application contracts;
-   change documentation outside WP07 manifest authority.

If a required path is outside the manifest, stop.

------------------------------------------------------------------------

## 20. Focused Permanent Infrastructure Tests

Add permanent tests only to the extent assigned to WP07 by the Release
1.7 execution plan/file manifest.

Use repository-native:

-   `TemporaryDatabase`;
-   deterministic valid schema-v3 state;
-   production snapshot persistence;
-   production durable Experiment acceptance;
-   existing fixture helpers.

Focused WP07 Infrastructure coverage should prove, where authorized:

1.  exact two-identity filtering;
2.  Snapshot isolation;
3.  Experiment Definition isolation;
4.  deterministic Experiment Result Identity ascending ordering;
5.  bounded maximum;
6.  successful empty result;
7.  exact evidence reconstruction/fidelity;
8.  read-only row/state preservation;
9.  bounded unavailable-storage mapping if WP07 owns it;
10. invalid persisted evidence behavior if safely constructible without
    unauthorized corruption.

Do not duplicate the entire later integration/process matrix.

No provider/network calls.

No real credentials.

------------------------------------------------------------------------

## 21. Test Data Integrity

Permanent tests must construct valid durable state through
repository-native production persistence paths wherever practical.

Do not directly corrupt database rows merely to force a failure unless
the manifest/test strategy explicitly authorizes that test boundary.

Do not add hidden test-only production hooks.

Do not weaken visibility boundaries solely for testing.

Use existing friend-assembly/test-host conventions.

------------------------------------------------------------------------

## 22. Expected Deltas

Expected production delta:

-   minimal SQLite discovery-store implementation authorized by WP07;
-   minimal shared Infrastructure mapping/helper change only if
    manifest-authorized and necessary to avoid semantic duplication.

Expected Application delta:

`0`

Expected Worker delta:

`0`

Expected DI delta:

`0`

Expected schema/index/migration delta:

`0`

Expected package/project/reference delta:

`0`

Expected Architecture.Tests delta:

`0`

Permanent Infrastructure test delta:

-   exactly the WP07-authorized focused coverage;
-   no speculative expansion.

Provider/network activity:

`0`

------------------------------------------------------------------------

## 23. Targeted Validation

Before full canonical verification, run the smallest targeted
Infrastructure suite that exercises the WP07 implementation.

Require:

-   exact filtering PASS;
-   ordering PASS;
-   bounded maximum PASS;
-   empty success PASS;
-   fidelity PASS;
-   read-only proof PASS;
-   relevant failure mapping PASS;
-   zero provider/network activity;
-   zero residue.

Then run canonical verification.

------------------------------------------------------------------------

## 24. Canonical Validation

Require:

-   all Domain tests PASS;
-   all Application tests PASS;
-   all Infrastructure tests PASS;
-   Architecture.Tests: 13/13;
-   all permanent tests PASS;
-   skipped: 0;
-   Release build warnings/errors: 0/0;
-   formatting: PASS;
-   Gitleaks: PASS;
-   `git diff --check`: PASS;
-   `git diff --cached --check`: PASS;
-   direct whitespace/final-newline checks: PASS;
-   conflict markers: 0;
-   provider/network product activity: 0;
-   real credentials: 0;
-   database/process/probe residue: 0.

Report exact permanent-test counts and delta from the WP06 baseline of
250.

------------------------------------------------------------------------

## 25. Schema and Dependency Validation

At successful completion verify:

-   schema remains v3;
-   table delta: 0;
-   column delta: 0;
-   index delta: 0;
-   migration delta: 0;
-   package delta: 0;
-   project delta: 0;
-   project-reference delta: 0;
-   production dependency graph remains acyclic;
-   Application → Infrastructure dependency remains absent;
-   Worker behavior unchanged;
-   DI registrations unchanged;
-   Release 1.1--1.6 predecessor behavior preserved.

------------------------------------------------------------------------

## 26. Process/Database Cleanup

All disposable test/probe resources must be cleaned:

-   temporary databases;
-   WAL/SHM files;
-   temporary scripts/projects;
-   retained handles;
-   spawned processes;
-   logs.

Final residue:

`0`

Permanent test fixtures/code remain only if explicitly
manifest-authorized.

------------------------------------------------------------------------

## 27. GitHub Lifecycle

Only after every WP07 acceptance gate passes:

1.  move #203 Backlog → In Progress if necessary;
2.  post concise completion evidence to #203;
3.  close #203;
4.  set #203 Project Status to Done.

Final required state:

-   #197--#203: CLOSED / Done;
-   #204--#209: OPEN / Backlog;
-   milestone #55: OPEN, 6 open / 7 closed;
-   Project membership: 13/13;
-   dependencies unchanged;
-   Priority/Release/Area unchanged.

Do not transition #204 automatically.

------------------------------------------------------------------------

## 28. Mutation Budget

### Application

`0`

### Infrastructure production

Only manifest-authorized WP07 SQLite discovery implementation and
strictly necessary authorized shared mapping support.

### Infrastructure tests

Only manifest-authorized WP07 focused permanent coverage.

### Worker/DI

`0`

### Schema/table/column/index/migration

`0`

### Packages/projects/references

`0`

### Architecture rules

`0`

### Git transport

`0`

### GitHub

Only #203 lifecycle mutations.

------------------------------------------------------------------------

## 29. Stop Conditions

Stop with:

`RELEASE 1.7 WP07 BLOCKED`

if:

-   starting state differs materially;
-   WP06 physical-access evidence differs materially;
-   existing schema v3 cannot support the permanent implementation;
-   a new table/column/index/migration/schema version is required;
-   Application contract/use-case changes are required;
-   evidence cannot be reconstructed without fidelity loss;
-   collection-level invalid-evidence semantics are undefined and must
    be invented;
-   DI/Worker changes are required;
-   package/project/reference changes are required;
-   architecture rules would need weakening;
-   required paths lie outside the manifest;
-   permanent tests require unauthorized durable corruption;
-   canonical validation fails;
-   provider/network execution or real credentials would be required.

Report the exact blocker and smallest corrective authority required.

------------------------------------------------------------------------

## 30. Required Execution Report

Report:

1.  baseline and starting state;
2.  authoritative inputs read;
3.  repository-native SQLite patterns reused;
4.  WP07 manifest-authorized paths;
5.  permanent store implementation shape;
6.  exact SQL/filter shape;
7.  parameterization proof;
8.  deterministic ordering proof;
9.  evidence reconstruction/fidelity proof;
10. successful empty semantics;
11. read-only proof;
12. failure mapping;
13. unknown-defect behavior;
14. schema-v3 preservation;
15. WP08+/Application/Worker/DI boundary preservation;
16. changed production paths;
17. focused permanent test coverage;
18. exact test delta from 250;
19. schema/index/package/project/reference deltas;
20. targeted validation;
21. canonical validation;
22. offline/security/residue evidence;
23. GitHub lifecycle;
24. final milestone counts;
25. next authorized action.

------------------------------------------------------------------------

## 31. Completion Markers

On success end exactly:

`RELEASE 1.7 WP07 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP08 — Durable Evidence Discovery Failure Semantics — GitHub issue #204`

Do not execute WP08 automatically.

If blocked end exactly:

`RELEASE 1.7 WP07 BLOCKED`

and identify the smallest corrective authority required.
