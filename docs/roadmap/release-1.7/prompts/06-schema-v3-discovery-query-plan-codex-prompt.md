# Release 1.7 WP06 --- Schema-v3 Discovery Query Plan --- Codex Authority

## 1. Mission

Execute Release 1.7 WP06 --- **Schema-v3 Discovery Query Plan** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#202`

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
-   WP06 #202: OPEN / Backlog;
-   #203--#209: OPEN / Backlog;
-   milestone #55: OPEN, 8 open / 5 closed;
-   canonical permanent baseline: 250/250;
-   schema: v3.

Accepted Release 1.7 semantics:

-   exact Snapshot Identity + Experiment Definition Identity discovery;
-   caller-supplied mandatory positive bounded maximum;
-   deterministic ordering by Experiment Result Identity ascending;
-   successful zero-match discovery returns an empty collection;
-   `aiq-experiment-identity-v1` remains authoritative;
-   no discovery/query identity;
-   immutable durable Experiment evidence fidelity is preserved;
-   discovery is read-only;
-   schema v3 is the planned target;
-   structural schema mutation is a mandatory stop condition requiring
    separate authority.

WP06 is a **physical-access feasibility and query-plan proof**.

It must determine whether the existing schema-v3 physical model can
support the frozen discovery semantics correctly and boundedly **without
structural schema mutation**.

WP06 must not implement WP07 Infrastructure discovery behavior unless
the authoritative execution plan/file manifest explicitly assigns a
narrowly bounded production artifact to WP06.

------------------------------------------------------------------------

## 2. Flow Preservation

Release 1.7 execution flow is frozen for the remainder of the release.

Do not initiate a broad architecture/design review, redesign existing
Release 1.7 contracts, or opportunistically refactor predecessor
implementation.

Architecture/design observations that do not constitute an actual
blocking defect are deferred until the separately governed
post-Release-1.7 review planned for Release 1.8.

WP06 must therefore answer only the bounded question authorized here:

> Can the accepted schema-v3 physical model support the frozen Release
> 1.7 discovery access pattern with deterministic semantics and an
> acceptable bounded query plan, without schema/index/migration
> mutation?

Do not expand WP06 into a general architecture review.

------------------------------------------------------------------------

## 3. Authoritative Inputs

Read completely before mutation or physical proof:

-   `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md`
-   `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE_DISCOVERY.md`
-   `docs/architecture/data/EXPERIMENT_DISCOVERY_IDENTITY_PROVENANCE_FIDELITY.md`
-   `src/AIQuantTradingResearch.Application/Experiments/ExperimentPersistenceContracts.cs`
-   `src/AIQuantTradingResearch.Application/Experiments/DurableExperimentDiscoveryUseCase.cs`
-   current schema-v3 SQLite persistence implementation and schema
    initialization/validation;
-   current `experiment_results` table definition and accepted
    indexes/keys;
-   existing Infrastructure persistence tests/helpers relevant to
    schema-v3 Experiment Result evidence;
-   GitHub issue #202.

Treat WP02--WP05 semantics/contracts/orchestration as frozen predecessor
authority.

------------------------------------------------------------------------

## 4. Execution-Authority Lifecycle

The WP06 prompt pair follows the established Release 1.7
execution-authority lifecycle.

Do not:

-   stage it merely because it exists;
-   count it as production/test/schema mutation;
-   remove prior WP prompt pairs without separate authority;
-   allow expected untracked authority files to create a false blocker.

Report authority-file classification in the execution report.

------------------------------------------------------------------------

## 5. Mandatory Starting Gate

Before WP06 mutation or disposable database construction verify:

-   branch: `main`;
-   HEAD: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main`: same SHA;
-   ahead/behind: `0/0`;
-   staged paths: 0;
-   tracked mutations: 0;
-   no active merge/rebase/cherry-pick/revert;
-   no conflict markers;
-   #197--#201: CLOSED / Done;
-   #202: OPEN / Backlog;
-   #203--#209: OPEN / Backlog;
-   milestone #55: OPEN, 8 open / 5 closed;
-   Project #2 planning state reconciled;
-   schema: v3;
-   canonical baseline: 250 tests;
-   accepted WP04/WP05 Application contracts/use case present;
-   no WP06+ Infrastructure discovery implementation already exists.

Reconcile expected untracked planning, documentation, and authority
files against the Release 1.7 manifest and completed WPs.

Unexpected tracked/staged state blocks execution.

------------------------------------------------------------------------

## 6. Process-Level Validation Prerequisite

Apply the engineering-playbook rule established after Release 1.6 WP11.

Before constructing any process/database proof, identify and report the
repository-native fixture/seeding mechanism.

The planned Release 1.7 prerequisite is:

-   Infrastructure test-host `TemporaryDatabase`;
-   deterministic `DatasetSnapshotCandidate` construction;
-   `SqliteDatasetSnapshotStore.Store(...)` for schema-v3 predecessor
    state;
-   production durable Experiment acceptance/persistence for Experiment
    Result evidence;
-   existing friend-assembly/test-host boundary where required;
-   disposable databases only;
-   complete cleanup of database/process/probe residue.

Prefer existing permanent test helpers or a removable test-host probe.

Do not create a permanent validation utility merely for WP06.

Do not repeat the Release 1.6 WP11 failure mode of inventing an external
seeding path before identifying the repository-native mechanism.

------------------------------------------------------------------------

## 7. Frozen Logical Access Pattern

The query plan must implement exactly this logical predicate:

-   Snapshot Identity = requested Snapshot Identity;
-   Experiment Definition Identity = requested Experiment Definition
    Identity.

It must then:

-   order by Experiment Result Identity ascending;
-   return at most the caller-supplied positive maximum;
-   permit zero matching rows as successful empty discovery.

No other query dimension is authorized.

Do not add:

-   Feature Set filtering;
-   Snapshot Version as an independent search dimension;
-   result-identity range filtering;
-   timestamps;
-   date ranges;
-   status;
-   registry/history concepts;
-   provider;
-   pagination/cursor;
-   free text;
-   secondary sort choices.

------------------------------------------------------------------------

## 8. Schema-v3 Physical Inspection

Inspect the actual accepted schema-v3 definition rather than reasoning
from assumptions.

Document the relevant physical facts for `experiment_results`, including
only what actually exists:

-   primary key;
-   columns used for Snapshot identity/provenance;
-   columns used for Experiment Definition identity/provenance;
-   existing indexes;
-   uniqueness constraints;
-   ordering-relevant identity storage;
-   relevant SQLite affinities/collations if they affect correctness;
-   schema initialization/version validation.

Do not alter any of them in WP06.

------------------------------------------------------------------------

## 9. Query Shape Proof

Derive the smallest SQL query shape that can satisfy the frozen
semantics against schema v3.

The proof must establish:

1.  exact equality on Snapshot Identity;
2.  exact equality on Experiment Definition Identity;
3.  explicit `ORDER BY` on Experiment Result Identity ascending;
4.  explicit bounded result count;
5.  deterministic successful empty result;
6.  no reliance on implicit SQLite row order;
7.  no mutation statement;
8.  no provider/network dependency.

The query shape may be demonstrated in disposable validation
code/scripts/probes, but do not place temporary proof artifacts into
governed production paths.

------------------------------------------------------------------------

## 10. Bounded Maximum

The Application contract intentionally does not invent a numeric upper
ceiling.

WP06 must not invent one.

The physical proof must demonstrate that the caller-supplied positive
maximum can be bound safely using repository-native SQLite
parameterization.

Do not:

-   concatenate the maximum into SQL text if parameter binding is
    available;
-   silently clamp it;
-   substitute a default;
-   interpret zero as unbounded;
-   introduce an Infrastructure-specific public maximum contract.

If SQLite or the current data-access mechanism imposes a relevant hard
technical limitation, report the exact observed limitation and determine
whether it actually affects the accepted Application contract. Do not
manufacture a policy ceiling from it.

------------------------------------------------------------------------

## 11. Deterministic Ordering

SQLite row order without explicit ordering is not authoritative.

The query-plan proof must require:

`Experiment Result Identity ASC`

using the actual persisted representation of that identity.

Verify that the stored identity representation and comparison semantics
produce the accepted deterministic ordering.

Do not introduce an additional sequence column or discovery ordinal.

Do not rely on insertion order, `rowid`, timestamp, or primary-key
coincidence unless Experiment Result Identity itself is the accepted
ordering key.

------------------------------------------------------------------------

## 12. Query-Plan Inspection

Use SQLite's repository-compatible query-plan inspection mechanism, such
as `EXPLAIN QUERY PLAN`, against a deterministic disposable schema-v3
database.

Capture enough evidence to determine:

-   which existing key/index/access path is used;
-   whether filtering requires scanning `experiment_results`;
-   whether ordering requires a temporary B-tree/sort;
-   whether the bounded limit can stop work meaningfully;
-   whether the plan remains semantically correct.

Do not treat the mere presence of `SCAN` as an automatic defect.

Do not treat the mere absence of a composite index as automatic
authority to add one.

WP06 is a proof and decision gate, not an optimization reflex.

------------------------------------------------------------------------

## 13. Representative Deterministic Data

Construct a disposable dataset sufficient to distinguish query
correctness from accidental success.

The proof should include multiple durable Experiment Result rows such
that:

-   more than one Snapshot Identity exists;
-   more than one Experiment Definition Identity exists;
-   multiple results match the target pair if the accepted identity
    model permits this;
-   nonmatching rows exist;
-   result identities are not inserted in already-sorted order where
    practical;
-   the requested maximum can be smaller than the number of matches;
-   a valid zero-match query can be demonstrated.

Use production persistence/acceptance paths where required to construct
valid durable evidence.

Do not corrupt durable data.

Do not manufacture logically impossible rows merely to make the query
test convenient.

If the accepted identity model constrains how many valid rows can share
the query dimensions, report that fact and construct the strongest valid
proof possible.

------------------------------------------------------------------------

## 14. Correctness Matrix

At minimum prove, using valid schema-v3 state:

### Q1 --- Exact Match

Only rows matching both exact query identities are returned.

### Q2 --- Snapshot Isolation

Same Experiment Definition with a different Snapshot does not leak into
results.

### Q3 --- Experiment Definition Isolation

Same Snapshot with a different Experiment Definition does not leak into
results.

### Q4 --- Deterministic Ordering

Returned Experiment Result identities are ascending exactly as frozen.

### Q5 --- Bounded Maximum

When valid matches exceed the requested maximum, no more than the
maximum are returned.

### Q6 --- Empty Discovery

A valid pair with no matches returns zero rows successfully.

### Q7 --- Read Only

Query execution creates, updates, deletes, or repairs zero durable rows.

### Q8 --- Evidence Fidelity Feasibility

The selected physical columns are sufficient for WP07 to reconstruct the
accepted `DurableExperimentEvidence` without information loss.

Do not implement WP07 row mapping beyond what is needed to prove
feasibility.

------------------------------------------------------------------------

## 15. Structural Stop Gate

This is mandatory.

If the proof establishes that correct Release 1.7 discovery **requires**
any of the following:

-   a new table;
-   a new column;
-   a new index;
-   a changed primary/unique key;
-   a migration;
-   schema version 4;
-   altered persisted identity representation;
-   changed Release 1.6 evidence semantics;

then stop.

Do not perform the structural mutation.

End:

`RELEASE 1.7 WP06 BLOCKED`

Report:

-   the exact observed schema-v3 limitation;
-   the exact query-plan/correctness evidence;
-   why the limitation is correctness-critical or acceptance-critical
    rather than merely an optimization opportunity;
-   the smallest corrective authority required.

A potentially faster index is not by itself proof that schema v3 is
unacceptable.

------------------------------------------------------------------------

## 16. Acceptance Standard for Existing Schema v3

Schema v3 may be accepted for Release 1.7 if the proof demonstrates:

-   semantic correctness;
-   deterministic ordering;
-   bounded cardinality;
-   exact filtering;
-   successful empty discovery;
-   full evidence reconstruction feasibility;
-   read-only behavior;
-   a query plan acceptable for the bounded Release 1.7 scope.

Do not impose an invented enterprise-scale throughput requirement that
is absent from the Release 1.7 definition.

Document performance implications honestly.

A later scale-oriented release may add indexes under separate authority
if justified by measured workload.

------------------------------------------------------------------------

## 17. WP07 Boundary Protection

WP07 owns the permanent Infrastructure discovery implementation.

WP06 must not:

-   add `IDurableExperimentEvidenceDiscoveryStore` SQLite
    implementation;
-   wire production row mapping;
-   add production SQL to the permanent Infrastructure implementation
    unless explicitly assigned by the manifest;
-   add DI registration;
-   change production failure classification;
-   create permanent Infrastructure behavior reserved for WP07.

Disposable proof SQL is permitted only as validation evidence and must
leave zero residue.

------------------------------------------------------------------------

## 18. Application Boundary Protection

Do not change:

-   `DurableExperimentDiscoveryRequest`;
-   `DurableExperimentDiscoveryResult`;
-   `IDurableExperimentEvidenceDiscoveryStore`;
-   `IDurableExperimentDiscoveryUseCase`;
-   `DurableExperimentDiscoveryUseCase`;
-   `DurableExperimentEvidence`;
-   accepted failure vocabulary;

unless an actual correctness blocker is discovered.

A preference for a different contract shape is not WP06 authority.

Architecture/design review is deferred until Release 1.8.

------------------------------------------------------------------------

## 19. Worker/DI Boundary Protection

WP06 must not change:

-   Worker selectors;
-   Worker configuration;
-   Worker precedence;
-   Worker output;
-   process exit codes;
-   DI registration;
-   composition root.

No process-level Worker discovery execution is required here unless the
authoritative Release 1.7 plan explicitly assigns a WP06 validation
step. The query-plan proof should remain at the smallest physical
boundary capable of establishing the required facts.

------------------------------------------------------------------------

## 20. Manifest-Bounded Mutation

Read `RELEASE_1.7_FILE_MANIFEST.md` before editing.

Only modify/create paths explicitly assigned to WP06.

If WP06 is designed as a validation-only work package, permanent
production/test mutation may be zero.

If the manifest authorizes a durable architecture/data document
recording the query-plan decision, update/create only that authorized
document.

For every permanent changed path report:

-   why WP06 owns it;
-   whether it is new or modified;
-   whether it records proof/decision rather than implementing WP07.

If a required permanent path is outside the manifest, stop.

------------------------------------------------------------------------

## 21. Permanent Test Policy

Do not consume WP07 or later persistence-test authority.

Expected permanent test delta:

`0`

unless the Release 1.7 execution plan/file manifest explicitly assigns a
WP06 query-plan regression artifact.

Prefer disposable deterministic proof for physical-plan inspection.

Do not add Worker process tests.

Architecture.Tests delta:

`0`

Do not weaken architecture rules.

------------------------------------------------------------------------

## 22. Validation Cleanup

Every WP06 disposable artifact must be removed before completion,
including:

-   temporary SQLite databases;
-   WAL/SHM files;
-   temporary projects;
-   scripts;
-   probe source;
-   build output created outside normal repository verification;
-   logs;
-   retained handles/processes.

Final residue:

`0`

Repository-native permanent test fixtures remain untouched unless
manifest-authorized.

------------------------------------------------------------------------

## 23. Canonical Validation

After the query-plan proof and any manifest-authorized documentation
mutation, run canonical verification.

Require:

-   Domain.Tests: 11/11;
-   Application.Tests: 111/111;
-   Infrastructure.Tests: 117/117;
-   Architecture.Tests: 13/13;
-   permanent total: 250/250 unless an explicitly authorized WP06 delta
    exists;
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
-   disposable validation residue: 0.

Report exact permanent-test delta from 250.

------------------------------------------------------------------------

## 24. Schema and Dependency Validation

At successful WP06 completion require:

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
-   DI state unchanged;
-   Worker behavior unchanged.

------------------------------------------------------------------------

## 25. GitHub Lifecycle

Only after every WP06 acceptance gate passes:

1.  move #202 Backlog → In Progress if necessary;
2.  post concise completion evidence to #202, including the schema-v3
    query-plan conclusion;
3.  close #202;
4.  set #202 Project Status to Done.

Final required state:

-   #197--#202: CLOSED / Done;
-   #203--#209: OPEN / Backlog;
-   milestone #55: OPEN, 7 open / 6 closed;
-   Project membership: 13/13;
-   dependencies unchanged;
-   Priority/Release/Area unchanged.

Do not transition #203 automatically.

------------------------------------------------------------------------

## 26. Mutation Budget

### Production Application

`0`

### Production Infrastructure

`0` unless an explicit WP06 manifest artifact proves otherwise; never
implement WP07 behavior.

### Worker/DI

`0`

### Schema/index/migration

`0`

### Packages/projects/references

`0`

### Permanent tests

`0` by default.

### Disposable validation

Authorized only for deterministic schema-v3 query-plan proof; final
residue must be 0.

### Git transport

`0`

### GitHub

Only #202 lifecycle mutations.

------------------------------------------------------------------------

## 27. Stop Conditions

Stop with:

`RELEASE 1.7 WP06 BLOCKED`

if:

-   starting state differs materially;
-   repository-native prerequisite construction cannot be identified;
-   WP02--WP05 frozen semantics conflict materially;
-   exact filtering cannot be represented by schema v3;
-   deterministic identity ordering cannot be represented correctly;
-   bounded maximum cannot be safely expressed;
-   full evidence reconstruction is impossible from existing schema-v3
    state;
-   a table/column/index/migration/schema-version mutation is required
    for acceptance;
-   Application contract changes are required;
-   WP07 production implementation is required to complete the proof;
-   DI/Worker changes are required;
-   package/project/reference changes are required;
-   architecture rules would need weakening;
-   required permanent paths lie outside the manifest;
-   canonical validation fails;
-   provider/network execution or real credentials would be required.

Report the exact blocker and smallest corrective authority required.

------------------------------------------------------------------------

## 28. Required Execution Report

Report:

1.  baseline and starting state;
2.  authoritative inputs read;
3.  repository-native prerequisite/seeding mechanism;
4.  relevant schema-v3 physical facts;
5.  exact logical query;
6.  parameterization/bounded maximum proof;
7.  deterministic ordering proof;
8.  representative valid data construction;
9.  Q1--Q8 correctness matrix;
10. `EXPLAIN QUERY PLAN` evidence;
11. scan/index/sort/limit interpretation;
12. evidence-fidelity reconstruction feasibility;
13. structural stop-gate conclusion;
14. schema-v3 acceptance decision;
15. WP07/Application/Worker/DI boundary preservation;
16. permanent changed paths;
17. permanent-test delta;
18. schema/index/package/project/reference deltas;
19. cleanup/residue proof;
20. canonical validation;
21. offline/security evidence;
22. GitHub lifecycle;
23. final milestone counts;
24. next authorized action.

------------------------------------------------------------------------

## 29. Completion Markers

On success end exactly:

`RELEASE 1.7 WP06 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP07 — SQLite Durable Evidence Discovery — GitHub issue #203`

Do not execute WP07 automatically.

If blocked end exactly:

`RELEASE 1.7 WP06 BLOCKED`

and identify the smallest corrective authority required.
