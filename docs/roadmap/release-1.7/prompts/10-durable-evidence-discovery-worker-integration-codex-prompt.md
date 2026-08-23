# Release 1.7 WP10 --- Durable Evidence Discovery Worker Integration --- Codex Authority

## 1. Mission

Execute Release 1.7 WP10 --- **Durable Evidence Discovery Worker
Integration** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#206`

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
-   WP10 #206: OPEN / Backlog;
-   #207--#209: OPEN / Backlog;
-   milestone #55: OPEN, 4 open / 9 closed;
-   canonical permanent baseline: 250/250;
-   schema: v3.

Accepted Release 1.7 capability before WP10:

-   `DurableExperimentDiscoveryRequest` and immutable result contracts
    exist;
-   `IDurableExperimentDiscoveryUseCase` /
    `DurableExperimentDiscoveryUseCase` exist;
-   `IDurableExperimentEvidenceDiscoveryStore` exists;
-   `SqliteExperimentResultStore` implements discovery using exact
    Snapshot Identity + Experiment Definition Identity filtering;
-   caller-supplied positive maximum bounds results;
-   ordering is binary Experiment Result Identity ascending;
-   zero matches are successful empty discovery;
-   Release 1.6 five-value failure vocabulary is preserved;
-   WP09 provides exactly one effective DI registration for the
    discovery use case and discovery store;
-   DI resolution is side-effect free;
-   no Worker discovery mode exists yet.

WP10 must expose this already-accepted Application capability through
one explicit one-shot Worker mode while preserving every predecessor
Worker mode and its routing semantics.

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
-   current Worker `Program.cs`;
-   current Durable Experiment Worker execution/configuration files;
-   current Release 1.5 Experiment Worker mode;
-   current Release 1.4 Feature Worker mode;
-   current Release 1.3 pipeline mode;
-   existing Worker configuration conventions;
-   existing process-level validation helpers/runners;
-   `docs/handbook/ENGINEERING_PLAYBOOK.md`, especially Process-Level
    Validation Prerequisites;
-   GitHub issue #206.

Treat WP02--WP09 semantics as frozen.

------------------------------------------------------------------------

## 3. Flow Preservation

Preserve the current Release 1.7 execution plan.

WP10 is **Worker integration only**.

Do not perform:

-   architecture/design review;
-   Application contract redesign;
-   persistence redesign;
-   schema/index optimization;
-   permanent discovery regression suite work owned by later WPs;
-   broad Worker refactoring.

Non-blocking architectural observations remain deferred to Release 1.8.

------------------------------------------------------------------------

## 4. Execution-Authority Lifecycle

The WP10 authority pair is execution-only input under the established
Release 1.7 lifecycle.

Do not stage it merely because it exists.

Do not classify expected prompt/planning artifacts as unexplained
mutations.

Do not remove governed artifacts without explicit authority.

Report the authority-file classification in the final report.

------------------------------------------------------------------------

## 5. Mandatory Starting Gate

Before mutation verify:

-   branch: `main`;
-   HEAD: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main`: same SHA;
-   ahead/behind: `0/0`;
-   staged paths: 0;
-   tracked mutations: 0;
-   no active merge/rebase/cherry-pick/revert;
-   conflict markers: 0;
-   #197--#205: CLOSED / Done;
-   #206: OPEN / Backlog;
-   #207--#209: OPEN / Backlog;
-   milestone #55: OPEN, 4 open / 9 closed;
-   Project #2 membership/configuration reconciled;
-   schema: v3;
-   permanent baseline: 250/250;
-   WP09 discovery DI registrations exist exactly once;
-   no premature discovery Worker implementation exists.

Reconcile expected cumulative Release 1.7 untracked content against the
manifest.

Unexpected tracked/staged state blocks execution.

------------------------------------------------------------------------

## 6. Repository-Native Worker Pattern Inspection

Before editing, inspect how the Worker currently represents:

1.  Durable Experiment mode;
2.  Release 1.5 Experiment mode;
3.  Release 1.4 Feature mode;
4.  Release 1.3 pipeline mode.

Determine:

-   selector/configuration conventions;
-   validation conventions;
-   configuration object pattern;
-   execution helper pattern;
-   DI resolution pattern;
-   console evidence style;
-   failure/exit-code behavior;
-   routing precedence;
-   process-level test runner conventions.

Follow those conventions rather than introducing a parallel Worker
framework.

------------------------------------------------------------------------

## 7. Discovery Mode

Add one explicit one-shot **Durable Experiment Evidence Discovery**
mode.

It must accept exactly the dimensions frozen by Release 1.7:

-   Snapshot Identity;
-   Experiment Definition Identity;
-   positive caller-supplied maximum.

Do not add:

-   discovery identity;
-   result identity as query selector;
-   date ranges;
-   registry/history filters;
-   paging cursor;
-   offset;
-   sort selector;
-   provider selector;
-   mutation flags;
-   retry configuration.

The Worker is an adapter over the existing Application use case, not a
second discovery API.

------------------------------------------------------------------------

## 8. Configuration Semantics

Use repository-native configuration conventions and names consistent
with the authoritative Release 1.7 definition/execution plan.

A valid discovery intent requires all mandatory discovery selectors to
be present and valid.

At minimum:

-   Snapshot Identity must be present;
-   Experiment Definition Identity must be present;
-   maximum must be present and positive.

Do not silently:

-   infer a default maximum;
-   clamp the maximum;
-   normalize malformed identity into another value;
-   substitute another mode's selector;
-   use a lower-priority mode when discovery intent is
    partial/malformed.

If the authoritative planning files already freeze exact configuration
keys, use those exact keys.

Do not invent alternatives.

------------------------------------------------------------------------

## 9. Partial/Malformed Intent

Once discovery intent is present, partial or malformed discovery
configuration must fail deterministically.

It must not fall back to:

-   Durable Experiment execution;
-   Experiment execution;
-   Feature execution;
-   pipeline execution;
-   provider acquisition.

No product operation should execute for invalid discovery configuration.

Follow the existing Worker error/exit convention.

Do not introduce a new failure vocabulary value.

------------------------------------------------------------------------

## 10. Routing Precedence

Extend the existing routing chain minimally.

Required precedence:

**Durable Experiment Evidence Discovery → Durable Experiment →
Experiment → Feature → pipeline**

Discovery wins whenever a valid discovery intent conflicts with lower
selectors.

Partial/malformed discovery intent also blocks fallback.

Do not reorder predecessor modes relative to each other.

------------------------------------------------------------------------

## 11. Application Invocation

For valid discovery configuration:

-   resolve `IDurableExperimentDiscoveryUseCase` from DI;
-   construct exactly one `DurableExperimentDiscoveryRequest`;
-   invoke the use case exactly once;
-   do not resolve/use `SqliteExperimentResultStore` directly from
    Worker;
-   do not execute SQL in Worker;
-   do not create persistence objects manually;
-   do not bypass Application.

The Worker must remain an outer adapter/composition boundary.

------------------------------------------------------------------------

## 12. Successful Output Semantics

Output must provide deterministic, reviewable evidence without inventing
a new durable identity.

At minimum report:

-   observed mode: Durable Experiment Evidence Discovery;
-   Snapshot Identity query dimension;
-   Experiment Definition Identity query dimension;
-   requested maximum;
-   returned count.

For each returned Experiment Result, emit sufficient accepted evidence
to prove:

-   Experiment Result Identity;
-   Snapshot identity/version binding;
-   Experiment Definition Identity;
-   Feature Set Identity;
-   evidence fidelity/provenance already frozen by WP03;
-   deterministic returned ordering.

Use the repository's existing console style where practical.

Do not create:

-   discovery identity;
-   query fingerprint;
-   synthetic aggregate over the collection;
-   new persisted record.

------------------------------------------------------------------------

## 13. Empty Discovery Output

A valid zero-match discovery must:

-   exit successfully;
-   identify the mode;
-   report returned count `0`;
-   emit no fabricated Experiment Result;
-   not report `NotFound`;
-   not fall back;
-   not mutate durable state.

This distinction is mandatory.

------------------------------------------------------------------------

## 14. Failure Semantics

Preserve WP08 exactly.

### Invalid configuration

Use the existing Worker invalid-configuration behavior and non-success
exit.

### `InvalidRequest`

Application-prevalidated; Worker should prevent malformed process
configuration from becoming fallback behavior.

### `NotFound`

Never use it for valid zero-match discovery.

### `DependencyUnavailable`

Propagate/report using the existing bounded failure behavior; no
retry/fallback.

### `InvalidEvidence`

Propagate/report existing bounded semantics; no partial-success
invention.

### `IntegrityConflict`

Preserve the lower-layer invariant; do not manufacture corruption to
make it Worker-reachable.

### Unknown defects

Propagate according to existing Worker behavior; do not broadly
normalize them.

Do not add retries, repair, recovery, or alternate paths.

------------------------------------------------------------------------

## 15. Read-Only Guarantee

Discovery Worker mode is read-only.

It must not:

-   accept Experiment Results;
-   update Experiment Results;
-   delete Experiment Results;
-   create dataset snapshots;
-   invoke providers;
-   create schema/migrations;
-   mutate registry/history;
-   persist discovery metadata.

Representative process proof must verify durable row counts/state remain
unchanged.

------------------------------------------------------------------------

## 16. Predecessor Worker Preservation

WP10 must preserve:

### P1 --- Durable Experiment

Existing Release 1.6 Durable Experiment mode remains behaviorally
unchanged.

### P2 --- Experiment

Existing Release 1.5 Experiment mode remains behaviorally unchanged.

### P3 --- Feature

Existing Release 1.4 Feature mode remains behaviorally unchanged.

### P4 --- Pipeline

Existing Release 1.3 five-stage pipeline remains behaviorally unchanged.

### P5 --- Conflicting Selectors

Valid discovery selectors override all lower modes.

### P6 --- Partial Discovery + Lower Selectors

Malformed/partial discovery intent fails and does not execute lower
modes.

Do not change predecessor output except where unavoidable to insert the
new top-level route.

------------------------------------------------------------------------

## 17. Process-Level Validation Prerequisite

Before process validation, identify and reuse the repository-native
fixture/seeding path established in WP12 of Release 1.6 and confirmed in
Release 1.7 planning:

-   test-host-local `TemporaryDatabase`;
-   deterministic `DatasetSnapshotCandidate`;
-   `SqliteDatasetSnapshotStore.Store(...)`;
-   production durable Experiment acceptance;
-   existing `--no-build` Worker runner;
-   current friend-assembly boundary;
-   deterministic evidence;
-   complete process/database cleanup.

Do not repeat the WP11 Release 1.6 mistake of inventing an external
seeding project before checking repository-native fixtures.

If the existing fixture cannot construct a required state, stop and
report the smallest missing validation authority rather than improvising
production visibility changes.

------------------------------------------------------------------------

## 18. Process Validation Dataset

Construct only the smallest deterministic disposable schema-v3 state
necessary to prove Worker discovery.

Prefer a fixture containing:

-   at least two Experiment Results matching one exact Snapshot +
    Experiment Definition pair;
-   at least one nonmatching Experiment Result where useful to prove
    filtering;
-   deterministic identities whose expected binary ascending order is
    known.

Use production acceptance paths to create durable Experiment Result
evidence.

Do not corrupt data.

No real provider/network/credentials.

------------------------------------------------------------------------

## 19. Required Worker Validation Matrix

Prove at minimum:

### W1 --- Valid Non-Empty Discovery

-   process exit: success;
-   observed mode: Durable Experiment Evidence Discovery;
-   exact query dimensions;
-   returned count correct;
-   identities/evidence correct;
-   order correct.

### W2 --- Maximum Bound

With more matches than requested maximum:

-   process exit: success;
-   count equals maximum;
-   returned prefix follows Experiment Result Identity ascending;
-   no partial-failure semantics.

### W3 --- Empty Discovery

-   process exit: success;
-   returned count 0;
-   no `NotFound`;
-   no fallback.

### W4 --- Partial Discovery Intent

-   process exit: failure;
-   no use-case/product execution;
-   no lower-mode fallback.

### W5 --- Malformed Maximum

-   zero/non-positive/malformed according to configuration
    representation;
-   process exit: failure;
-   no fallback;
-   no product execution.

### W6 --- DependencyUnavailable

-   process exit: failure;
-   existing bounded failure observed;
-   no retry/fallback/result fabrication.

### W7 --- Conflicting Valid Selectors

-   discovery mode wins;
-   Durable Experiment execution: NO;
-   Experiment execution: NO;
-   Feature execution: NO;
-   pipeline execution: NO.

### W8 --- Durable Experiment Preservation

Existing mode remains successful and unchanged.

### W9 --- Experiment Preservation

Existing mode remains successful and unchanged.

### W10 --- Feature Preservation

Existing mode remains successful and unchanged.

### W11 --- Pipeline Preservation

Existing mode remains successful and unchanged.

### W12 --- Read-Only State

Discovery leaves Experiment Result row count and durable identities
unchanged.

### W13 --- Isolation

-   provider calls: 0;
-   network product activity: 0;
-   real credentials: 0;
-   residue: 0.

------------------------------------------------------------------------

## 20. IntegrityConflict Validation

Do not corrupt durable state merely to produce Worker-level
`IntegrityConflict`.

If safe Worker-level construction is not applicable, reuse accepted
WP08/WP07/Release 1.6 lower-layer evidence and state explicitly:

`Worker-level IntegrityConflict construction: NOT APPLICABLE`

Explain that discovery is read-only and contradiction is preserved as a
lower-layer acceptance invariant.

No overwrite/delete/repair is authorized.

------------------------------------------------------------------------

## 21. Manifest-Bounded Mutation

Read `RELEASE_1.7_FILE_MANIFEST.md` before editing.

Only modify/create WP10-authorized paths.

Expected areas may include:

-   Worker `Program.cs`;
-   discovery Worker execution helper;
-   discovery Worker configuration helper;

only if those exact paths/categories are authorized by the manifest.

Do not edit Application or Infrastructure production code unless a
genuine integration defect exists and the manifest explicitly grants
WP10 ownership.

For each changed path report:

-   why WP10 owns it;
-   exact behavior added;
-   why it does not consume WP11+ authority.

If a required path lies outside the manifest, stop.

------------------------------------------------------------------------

## 22. Minimal Worker Design

Prefer the existing Durable Experiment Worker pattern.

If repository conventions support it, use narrowly scoped types
analogous to:

-   discovery execution configuration;
-   discovery execution helper.

Do not create a generalized command framework, router abstraction,
mediator, CLI package, or new host.

WP10 should be the smallest extension of the existing Worker structure.

------------------------------------------------------------------------

## 23. Permanent Test Ownership

Respect the Release 1.7 execution plan/file manifest.

If WP11 owns permanent Application/Infrastructure/process discovery
tests, WP10 must not preempt that ownership.

Expected permanent-test delta:

`0`

unless the manifest explicitly assigns permanent Worker tests to WP10.

Use removable repository-native process probes for WP10 validation.

Architecture.Tests delta:

`0`

------------------------------------------------------------------------

## 24. DI Preservation

WP09 registrations are frozen.

WP10 must not:

-   duplicate discovery registrations;
-   alter their lifetime;
-   register concrete stores again;
-   introduce service locator behavior;
-   bypass DI.

Process validation must prove the real container resolves and executes
the accepted use case.

------------------------------------------------------------------------

## 25. Schema and Query Preservation

WP10 must leave unchanged:

-   schema v3;
-   `experiment_results` shape;
-   tables;
-   columns;
-   indexes;
-   migrations;
-   WP07 SQL;
-   binary ordering;
-   parameterized maximum;
-   19-column mapper.

No query optimization belongs here.

------------------------------------------------------------------------

## 26. Package/Project/Reference Preservation

Required deltas:

-   packages: 0;
-   projects: 0;
-   project references: 0.

Do not add a command-line parsing library or testing package.

Use existing platform/repository facilities.

------------------------------------------------------------------------

## 27. Targeted Validation

Run targeted Worker/process validation first.

Require W1--W13 PASS or accepted reconciliation where explicitly
allowed.

Capture deterministic evidence sufficient to prove:

-   routing;
-   invocation count;
-   output fidelity;
-   maximum;
-   empty success;
-   failure/no-fallback;
-   predecessor preservation;
-   read-only state;
-   isolation.

Remove all disposable state afterward.

------------------------------------------------------------------------

## 28. Canonical Validation

After WP10 implementation and targeted validation run canonical
verification.

Require:

-   Domain.Tests: 11/11;
-   Application.Tests: 111/111;
-   Infrastructure.Tests: 117/117;
-   Architecture.Tests: 13/13;
-   permanent total: 250/250 unless manifest explicitly authorizes a
    WP10 permanent-test delta;
-   skipped: 0;
-   Release build warnings/errors: 0/0;
-   formatting: PASS;
-   Gitleaks: PASS;
-   `git diff --check`: PASS;
-   `git diff --cached --check`: PASS;
-   direct whitespace/final-newline checks: PASS;
-   conflict markers: 0;
-   schema: v3;
-   package/project/reference delta: 0/0/0;
-   provider/network product activity: 0;
-   real credentials: 0;
-   temporary database/process/probe residue: 0.

Report exact permanent-test delta from 250.

------------------------------------------------------------------------

## 29. Architecture Validation

Require:

-   Architecture.Tests: 13/13;
-   dependency graph unchanged and acyclic;
-   Worker remains outermost adapter/composition root;
-   Application remains Infrastructure-independent;
-   no SQLite/store implementation leaks into Worker beyond composition
    already established;
-   Worker resolves Application use case, not Infrastructure store;
-   no new architecture rule is needed unless an actual unprotected
    dependency violation is discovered.

Architecture.Tests are zero-delta-first.

Do not weaken rules.

------------------------------------------------------------------------

## 30. Cleanup

Remove all disposable WP10 validation artifacts:

-   temporary databases;
-   WAL/SHM files;
-   test-host state;
-   removable probes;
-   scripts/logs;
-   retained Worker processes;
-   temporary directories.

Final residue:

`0`

Do not remove governed Release 1.7 artifacts.

------------------------------------------------------------------------

## 31. GitHub Lifecycle

Only after every WP10 gate passes:

1.  move #206 Backlog → In Progress if necessary;
2.  post concise completion evidence to #206 including W1--W13;
3.  close #206;
4.  set #206 Project Status to Done.

Final required state:

-   #197--#206: CLOSED / Done;
-   #207--#209: OPEN / Backlog;
-   milestone #55: OPEN, 3 open / 10 closed;
-   Project membership: 13/13;
-   dependencies unchanged;
-   Priority/Release/Area unchanged.

Do not transition #207 automatically.

------------------------------------------------------------------------

## 32. Mutation Budget

### Domain

`0`

### Application production

`0`

### Infrastructure production

`0`

### Worker production

Only manifest-authorized discovery configuration/execution/routing
changes.

### DI

`0`

### Schema/table/column/index/migration

`0`

### Packages/projects/references

`0`

### Permanent tests

`0` by default; preserve later ownership.

### Architecture rules

`0` by default.

### Disposable process validation

Authorized; residue 0.

### Git transport

`0`

### GitHub

Only #206 lifecycle mutations.

------------------------------------------------------------------------

## 33. Stop Conditions

Stop with:

`RELEASE 1.7 WP10 BLOCKED`

if:

-   starting state differs materially;
-   WP09 DI state is absent/duplicated;
-   planning files do not define enough configuration authority to
    implement discovery without inventing selectors;
-   valid discovery requires changing Application contracts;
-   valid discovery requires changing WP07 SQL/schema/index/migrations;
-   Worker would need direct SQL/store access;
-   routing cannot preserve predecessor precedence;
-   partial discovery intent would fall back;
-   zero matches cannot remain successful;
-   unknown defects require broad normalization;
-   Worker integration requires retry/repair/fallback;
-   process validation requires unauthorized production visibility
    changes or durable corruption;
-   required paths lie outside the WP10 manifest;
-   package/project/reference changes are required;
-   architecture rules would need weakening;
-   canonical validation fails;
-   provider/network execution or real credentials would be required.

Report the exact blocker and smallest corrective authority required.

------------------------------------------------------------------------

## 34. Required Execution Report

Report:

1.  baseline and starting state;
2.  authoritative inputs read;
3.  existing Worker patterns inspected;
4.  manifest-authorized changed paths;
5.  discovery configuration keys/validation;
6.  routing precedence before/after;
7.  Application use-case resolution/invocation;
8.  successful output contract;
9.  empty discovery behavior;
10. failure behavior;
11. W1--W13 matrix;
12. IntegrityConflict reconciliation;
13. predecessor Durable Experiment preservation;
14. predecessor Experiment preservation;
15. predecessor Feature preservation;
16. predecessor pipeline preservation;
17. conflicting-selector precedence;
18. partial/malformed no-fallback proof;
19. read-only durable-state proof;
20. process-level fixture mechanism;
21. provider/network/credential isolation;
22. permanent-test delta;
23. schema/package/project/reference deltas;
24. targeted validation;
25. canonical validation;
26. cleanup/residue;
27. GitHub lifecycle;
28. final milestone counts;
29. next authorized action.

------------------------------------------------------------------------

## 35. Completion Markers

On success end exactly:

`RELEASE 1.7 WP10 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP11 — Application & Infrastructure Discovery Tests — GitHub issue #207`

Do not execute WP11 automatically.

If blocked end exactly:

`RELEASE 1.7 WP10 BLOCKED`

and identify the smallest corrective authority required.
