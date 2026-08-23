# Release 1.7 WP11 --- Application & Infrastructure Discovery Tests --- Codex Authority

## 1. Mission

Execute Release 1.7 WP11 --- **Application & Infrastructure Discovery
Tests** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#207`

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
-   WP07 #203: CLOSED / Done;
-   WP08 #204: CLOSED / Done;
-   WP09 #205: CLOSED / Done;
-   WP10 #206: CLOSED / Done;
-   WP11 #207: OPEN / Backlog;
-   #208--#209: OPEN / Backlog;
-   milestone #55: OPEN, 3 open / 10 closed;
-   canonical permanent baseline: 250/250;
-   schema: v3.

Accepted Release 1.7 behavior before WP11:

-   exact Snapshot Identity + Experiment Definition Identity discovery;
-   caller-supplied positive maximum;
-   Experiment Result Identity ascending binary ordering;
-   successful immutable empty collection for zero matches;
-   no discovery/query identity;
-   exact Release 1.6 evidence fidelity and provenance;
-   read-only schema-v3 SQLite implementation;
-   existing five-value failure vocabulary;
-   exactly one effective discovery use-case/store DI registration;
-   Worker precedence:
    `Discovery → Durable Experiment → Experiment → Feature → pipeline`;
-   malformed/partial discovery intent cannot fall back;
-   Worker invokes the Application use case exactly once;
-   no direct Worker SQL/store implementation.

WP11 owns the permanent regression coverage for the accepted Application
and Infrastructure discovery behavior. It must convert the removable
proofs used by WP05--WP10 into focused repository-native permanent tests
without changing production behavior.

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
-   `src/AIQuantTradingResearch.Application/DependencyInjection.cs`
-   `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs`
-   `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteExperimentResultStore.cs`
-   `src/AIQuantTradingResearch.Worker/DurableExperimentDiscoveryConfiguration.cs`
-   `src/AIQuantTradingResearch.Worker/DurableExperimentDiscoveryExecution.cs`
-   `src/AIQuantTradingResearch.Worker/Program.cs`
-   existing Release 1.6 Experiment persistence Application tests;
-   existing Release 1.6 SQLite Experiment persistence tests;
-   existing Worker process-test conventions;
-   `docs/handbook/ENGINEERING_PLAYBOOK.md`, especially Process-Level
    Validation Prerequisites;
-   GitHub issue #207.

Treat WP02--WP10 production semantics as frozen.

------------------------------------------------------------------------

## 3. Scope

WP11 is a **permanent regression-test WP**.

Primary ownership:

1.  Application discovery orchestration tests;
2.  Infrastructure SQLite discovery tests;
3.  repository-native Worker process regression tests if and only if the
    Release 1.7 file manifest assigns them to WP11.

Do not redesign production code to make tests easier.

Production delta target:

`0`

If a test reveals an actual production defect, stop and report the
smallest corrective authority unless the manifest explicitly authorizes
WP11 to correct that exact defect.

------------------------------------------------------------------------

## 4. Flow Preservation

Do not perform the deferred Release 1.8 Architecture & Design Review.

Do not refactor:

-   discovery contracts;
-   use case;
-   SQLite store;
-   DI;
-   Worker routing;
-   predecessor modes.

WP11 tests the accepted design; it does not reopen it.

------------------------------------------------------------------------

## 5. Execution-Authority Lifecycle

The WP11 prompt pair follows the established execution-only lifecycle.

Do not stage it merely because it exists.

Reconcile expected prompt/planning artifacts rather than treating them
as unexplained repository state.

Do not remove governed artifacts without authority.

------------------------------------------------------------------------

## 6. Mandatory Starting Gate

Before permanent mutation verify:

-   branch: `main`;
-   HEAD: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main`: same SHA;
-   ahead/behind: `0/0`;
-   staged paths: 0;
-   tracked mutations: 0;
-   no merge/rebase/cherry-pick/revert;
-   conflict markers: 0;
-   #197--#206: CLOSED / Done;
-   #207: OPEN / Backlog;
-   #208--#209: OPEN / Backlog;
-   milestone #55: OPEN, 3 open / 10 closed;
-   schema: v3;
-   permanent baseline: 250/250;
-   WP10 Worker integration exists;
-   no WP12/WP13 implementation exists.

Unexpected tracked/staged state blocks execution.

------------------------------------------------------------------------

## 7. Test Design Principles

Permanent tests must be:

-   deterministic;
-   offline;
-   isolated;
-   behavior-oriented;
-   explicit about identity/provenance/fidelity;
-   independent of real provider credentials;
-   independent of external network;
-   independent of test execution order;
-   residue-free;
-   narrow enough to diagnose regression.

Prefer extending existing Release 1.6 persistence test files/classes
when the manifest and test organization make that the repository-native
location.

Avoid duplicate tests that merely restate predecessor coverage.

------------------------------------------------------------------------

## 8. Application Test Coverage

Add focused permanent Application tests for
`DurableExperimentDiscoveryUseCase`.

Cover at minimum the distinct behaviors below, consolidating cases when
one test can prove multiple invariants clearly.

### A1 --- Valid Request Invokes Store Exactly Once

For a valid request:

-   store invocation count = 1;
-   exact Snapshot Identity forwarded;
-   exact Experiment Definition Identity forwarded;
-   exact maximum forwarded.

### A2 --- Null Request

-   result: `InvalidRequest`;
-   store invocation count = 0.

### A3 --- Non-Positive Maximum

Cover the contract boundary required by the accepted implementation:

-   zero and/or negative as appropriate;
-   result: `InvalidRequest`;
-   store invocation count = 0.

Do not invent a numeric ceiling.

### A4 --- Empty Success Pass-Through

Store success with zero evidence:

-   remains success;
-   immutable empty collection;
-   never becomes `NotFound`.

### A5 --- Non-Empty Success Pass-Through

Prove exact pass-through of:

-   result ordering;
-   Experiment Result identities;
-   Snapshot identity/version;
-   Experiment Definition identity;
-   Feature Set identity;
-   provenance/lineage;
-   count;
-   canonical decimals;
-   aggregate presence/absence.

Do not recompute or normalize evidence in Application.

### A6 --- Bounded Failure Pass-Through

Prove representative existing classified failures pass through
unchanged.

Do not force every vocabulary value to be directly generated by the use
case.

### A7 --- Unknown Defect Propagation

An unknown store defect propagates and is not normalized.

### A8 --- No Retry / No Fallback

On failure:

-   store call count remains 1 for valid request;
-   no second invocation;
-   no alternate path.

------------------------------------------------------------------------

## 9. Application Test Doubles

Use repository-native deterministic fakes/stubs already present in
Application tests where practical.

Do not add mocking packages.

A minimal local test double may be introduced inside the test file if
that is the existing convention.

Test doubles must expose enough state to prove:

-   invocation count;
-   received request;
-   configured success/failure;
-   unknown exception propagation.

No production test hooks.

------------------------------------------------------------------------

## 10. Infrastructure Fixture Mechanism

Use the repository-native fixture path frozen by the engineering
playbook and Release 1.7 planning:

-   `TemporaryDatabase`;
-   deterministic `DatasetSnapshotCandidate`;
-   `SqliteDatasetSnapshotStore.Store(...)`;
-   production durable Experiment acceptance;
-   existing friend-assembly/test-host boundary;
-   deterministic evidence;
-   complete database/process cleanup.

Do not create an external probe project.

Do not use direct SQL seeding when a production acceptance path exists.

Direct read-only SQL may be used only where existing Infrastructure
tests already use it to assert durable row counts/state and the manifest
permits it.

------------------------------------------------------------------------

## 11. Infrastructure Discovery Coverage

Add focused permanent Infrastructure tests for
`SqliteExperimentResultStore` discovery.

### I1 --- Exact Dual-Identity Filtering

Seed:

-   matching Snapshot + Experiment Definition results;
-   at least one row differing by Snapshot identity;
-   at least one row differing by Experiment Definition identity where
    practical.

Discovery returns only exact dual-identity matches.

### I2 --- Deterministic Identity Ordering

Seed multiple matching results whose acceptance order does not trivially
equal Experiment Result Identity order.

Assert returned Experiment Result identities are binary ascending.

Do not rely on insertion order.

### I3 --- Caller-Supplied Maximum

With more matches than maximum:

-   returned count equals maximum;
-   returned evidence is the correct ordered prefix.

### I4 --- Zero Match

A valid query with no matching rows returns successful empty evidence.

Never `NotFound`.

### I5 --- Exact Evidence Reconstruction

For returned evidence assert exact fidelity of all accepted persisted
fields represented by `DurableExperimentEvidence`, including:

-   Experiment Result Identity;
-   Snapshot Identity;
-   Snapshot Version;
-   Experiment Definition Identity;
-   Feature Set Identity;
-   provenance/lineage fields;
-   count;
-   aggregate presence;
-   mean/minimum/maximum when present;
-   canonical decimal semantics.

Use the existing 19-column mapper as production behavior; tests assert,
not duplicate, its implementation.

### I6 --- Empty Experiment Result Fidelity

If an accepted Experiment Result has count 0:

-   evidence remains a returned Experiment Result;
-   its aggregates remain absent;
-   this is distinct from zero matching Experiment Results.

### I7 --- Read-Only Discovery

Before/after discovery prove:

-   Experiment Result row count unchanged;
-   identities unchanged;
-   schema unchanged;
-   no new tables/indexes;
-   no updates/deletes.

### I8 --- DependencyUnavailable

Use the existing repository-native unavailable-storage construction.

Assert:

-   `DependencyUnavailable`;
-   no retry;
-   no fallback;
-   no database repair/creation.

### I9 --- InvalidEvidence

Use a safe existing schema/evidence-validation boundary if
repository-native construction exists without unauthorized durable
corruption.

If permanent direct construction would require corruption, do not add a
corruption-based test merely for coverage. Reuse the strongest safe
repository-native condition assigned by the manifest.

### I10 --- Unknown Defect Boundary

Only add if the Infrastructure test boundary can prove it without
production hooks or fragile platform-specific behavior.

Unknown defects must not be broadly converted into a classified failure.

Do not manufacture an OS-specific failure just to satisfy I10.

------------------------------------------------------------------------

## 12. IntegrityConflict Coverage

Do not corrupt persisted durable state solely to create discovery-time
`IntegrityConflict`.

WP08 established:

-   `IntegrityConflict` is a preserved lower-layer acceptance invariant;
-   there is no legitimate direct read-only discovery trigger.

Permanent WP11 tests should not contradict that decision.

Existing Release 1.6 acceptance-conflict tests remain the authoritative
permanent contradiction coverage.

Document the reconciliation in the WP11 report.

------------------------------------------------------------------------

## 13. DI Permanent Coverage

Inspect existing Infrastructure DI tests.

If WP09 DI cardinality/resolution behavior is not yet permanently
protected and the Release 1.7 manifest assigns that coverage to WP11,
add focused assertions for:

-   exactly one `IDurableExperimentDiscoveryUseCase`;
-   exactly one `IDurableExperimentEvidenceDiscoveryStore`;
-   expected implementations;
-   shared `SqliteExperimentResultStore` forwarding behavior;
-   side-effect-free resolution.

Do not duplicate a permanent test already covering the exact behavior.

Do not change DI production code.

------------------------------------------------------------------------

## 14. Worker Process Permanent Coverage

Only if the Release 1.7 manifest assigns Worker process tests to WP11,
convert the most valuable WP10 removable proofs into permanent
repository-native Infrastructure/process tests.

Prioritize:

### P1 --- Valid Non-Empty Discovery

-   process success;
-   observed discovery mode;
-   expected count;
-   expected ordered identities.

### P2 --- Bounded Maximum

-   process success;
-   maximum enforced;
-   deterministic ordered prefix.

### P3 --- Empty Discovery

-   process success;
-   count 0;
-   no `NotFound`.

### P4 --- Partial/Malformed Discovery No-Fallback

-   process failure;
-   lower modes not executed.

### P5 --- Conflicting Selector Precedence

-   discovery wins;
-   lower modes not executed.

Do not duplicate all WP10 process cases if lower-layer permanent tests
already protect the semantics. Permanent process tests should focus on
integration boundaries.

Use the existing `--no-build` Worker runner and repository-native
fixture.

------------------------------------------------------------------------

## 15. Predecessor Regression Preservation

Do not delete, weaken, or rewrite predecessor tests merely to
accommodate Release 1.7.

Preserve permanent coverage for:

-   Release 1.6 durable Experiment persistence;
-   Release 1.5 Experiment;
-   Release 1.4 Feature;
-   Release 1.3 pipeline;
-   schema v3;
-   DI;
-   architecture boundaries.

If a predecessor test fails because of WP11 test state leakage, fix the
test isolation rather than production behavior.

------------------------------------------------------------------------

## 16. Test Count Discipline

Do not target a predetermined number of new tests.

Target complete nonredundant behavior coverage.

Report:

-   Domain before/after/delta;
-   Application before/after/delta;
-   Infrastructure before/after/delta;
-   Architecture before/after/delta;
-   permanent total before/after/delta.

Expected:

-   Domain delta: 0;
-   Architecture delta: 0;
-   Application delta: positive;
-   Infrastructure delta: positive;
-   production delta: 0.

A smaller high-value test set is preferable to redundant count
inflation.

------------------------------------------------------------------------

## 17. No Production Mutation

After tests are written, production diff must remain exactly the
accepted WP02--WP10 implementation.

WP11 must not modify:

-   Domain;
-   Application production;
-   Infrastructure production;
-   Worker production;
-   DI production;
-   schema;
-   SQL;
-   configuration;
-   packages/projects/references.

If a permanent test exposes a real production defect, stop with WP11
blocked and identify it precisely rather than silently correcting it
under test authority.

------------------------------------------------------------------------

## 18. Schema and Dependency Preservation

Require:

-   schema: v3;
-   table delta: 0;
-   column delta: 0;
-   index delta: 0;
-   migration delta: 0;
-   package delta: 0;
-   project delta: 0;
-   reference delta: 0;
-   dependency graph unchanged and acyclic;
-   Architecture.Tests remain 13/13.

------------------------------------------------------------------------

## 19. Targeted Test Validation

Run the changed test projects first.

Require all new and predecessor tests in those projects to pass.

Validate:

-   deterministic repeated execution where practical;
-   no order dependence;
-   no external provider/network;
-   no real credentials;
-   no retained SQLite handles;
-   no temporary process/database residue.

If Worker process tests are permanent, ensure they do not depend on
machine-specific paths or current working directory accidents.

------------------------------------------------------------------------

## 20. Canonical Validation

After targeted validation run canonical repository verification.

Baseline before WP11:

-   Domain: 11;
-   Application: 111;
-   Infrastructure: 117;
-   Architecture: 13;
-   total: 250.

Require after WP11:

-   every permanent test passes;
-   skipped: 0;
-   Architecture: 13/13;
-   Release build warnings/errors: 0/0;
-   formatting: PASS;
-   Gitleaks: PASS;
-   `git diff --check`: PASS;
-   `git diff --cached --check`: PASS;
-   direct whitespace/final-newline checks: PASS;
-   conflict markers: 0;
-   schema v3 preserved;
-   package/project/reference delta: 0/0/0;
-   provider/network product activity: 0;
-   real credentials: 0;
-   temporary database/process/probe residue: 0.

Report exact new permanent baseline.

Do not assume a target total in advance.

------------------------------------------------------------------------

## 21. Test Quality Review

Before completion inspect every new test for:

-   meaningful behavioral name;
-   one coherent reason to fail;
-   deterministic expected values;
-   no excessive implementation coupling;
-   no duplicated production algorithm;
-   no sleeps/timing races;
-   no environment-specific assumptions;
-   cleanup in failure paths;
-   clear distinction between empty discovery and empty Experiment
    Result;
-   explicit identity/provenance assertions where relevant.

Remove redundant tests before final acceptance.

------------------------------------------------------------------------

## 22. Manifest-Bounded Mutation

Read `RELEASE_1.7_FILE_MANIFEST.md` before editing.

Only modify/create WP11-authorized test paths.

Expected categories:

-   Application experiment persistence/discovery test file(s);
-   Infrastructure experiment persistence/discovery test file(s);
-   Worker process test file(s) only if manifest-owned by WP11.

For every changed path report:

-   new or modified;
-   why WP11 owns it;
-   behaviors covered;
-   test count delta.

If required coverage cannot be added within manifest-authorized paths,
stop.

------------------------------------------------------------------------

## 23. Cleanup

Remove all non-permanent validation artifacts:

-   disposable databases;
-   WAL/SHM files;
-   temporary probes/scripts;
-   temporary process fixtures not part of permanent tests;
-   logs;
-   retained Worker processes;
-   temporary directories.

Permanent test fixtures/helpers explicitly authorized by the manifest
remain.

Final disposable residue:

`0`

------------------------------------------------------------------------

## 24. GitHub Lifecycle

Only after every WP11 acceptance gate passes:

1.  move #207 Backlog → In Progress if necessary;
2.  post concise completion evidence to #207 including permanent test
    deltas and principal coverage;
3.  close #207;
4.  set #207 Project Status to Done.

Final required state:

-   #197--#207: CLOSED / Done;
-   #208--#209: OPEN / Backlog;
-   milestone #55: OPEN, 2 open / 11 closed;
-   Project membership: 13/13;
-   dependencies unchanged;
-   Priority/Release/Area unchanged.

Do not transition #208 automatically.

------------------------------------------------------------------------

## 25. Mutation Budget

### Production code

`0`

### Application tests

Manifest-authorized permanent discovery coverage.

### Infrastructure tests

Manifest-authorized permanent discovery/persistence/process coverage.

### Domain tests

`0`

### Architecture tests

`0`

### Schema/table/column/index/migration

`0`

### Packages/projects/references

`0`

### Documentation

`0` unless explicitly assigned to WP11 by the manifest.

### Disposable validation

Authorized; residue 0.

### Git transport

`0`

### GitHub

Only #207 lifecycle mutations.

------------------------------------------------------------------------

## 26. Stop Conditions

Stop with:

`RELEASE 1.7 WP11 BLOCKED`

if:

-   starting state differs materially;
-   production behavior must change for tests to pass;
-   Application/Infrastructure contracts require redesign;
-   schema/index/migration change is required;
-   a package/project/reference must be added;
-   architecture rules would need weakening;
-   safe Infrastructure fixtures cannot construct required accepted
    states;
-   permanent process testing requires unauthorized production
    visibility changes;
-   direct durable corruption would be required for new coverage;
-   required test paths lie outside the manifest;
-   tests expose nondeterministic production behavior;
-   canonical verification fails;
-   provider/network execution or real credentials would be required.

Report the exact blocker and smallest corrective authority required.

------------------------------------------------------------------------

## 27. Required Execution Report

Report:

1.  baseline and starting state;
2.  authoritative inputs read;
3.  manifest-authorized test paths;
4.  Application coverage added;
5.  Infrastructure coverage added;
6.  DI coverage added/reused;
7.  Worker process coverage added/reused;
8.  fixture/seeding mechanism;
9.  `IntegrityConflict` reconciliation;
10. production delta;
11. test counts before/after/delta by project;
12. exact new permanent total;
13. targeted test results;
14. canonical verification;
15. schema/package/project/reference preservation;
16. dependency/Architecture validation;
17. provider/network/credential isolation;
18. cleanup/residue;
19. changed paths;
20. GitHub lifecycle;
21. final milestone counts;
22. next authorized action.

------------------------------------------------------------------------

## 28. Completion Markers

On success end exactly:

`RELEASE 1.7 WP11 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP12 — Architecture & Documentation Alignment — GitHub issue #208`

Do not execute WP12 automatically.

If blocked end exactly:

`RELEASE 1.7 WP11 BLOCKED`

and identify the smallest corrective authority required.
