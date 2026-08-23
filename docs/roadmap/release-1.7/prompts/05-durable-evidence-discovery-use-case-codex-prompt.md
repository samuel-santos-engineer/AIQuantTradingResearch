# Release 1.7 WP05 --- Durable Evidence Discovery Use Case --- Codex Authority

## 1. Mission

Execute Release 1.7 WP05 --- **Durable Evidence Discovery Use Case**
for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#201`

Frozen predecessor baseline:

`95745fc2289ea855af39ba5e7bc0236a67f1c48b`

Authoritative milestone:

`#55 — Phase 4 - Release 1.7: Durable Experiment Evidence Discovery`

Accepted predecessor state:

-   WP01 #197: CLOSED / Done;
-   WP02 #198: CLOSED / Done;
-   WP03 #199: CLOSED / Done;
-   WP04 #200: CLOSED / Done;
-   WP05 #201: OPEN / Backlog;
-   #202--#209: OPEN / Backlog;
-   milestone #55: OPEN, 9 open / 4 closed;
-   canonical permanent baseline: 250/250;
-   schema: v3.

Accepted WP04 Application contract surface:

-   `DurableExperimentDiscoveryRequest`;
-   `DurableExperimentDiscoveryResult`;
-   `IDurableExperimentEvidenceDiscoveryStore`;
-   reused `DurableExperimentEvidence`;
-   existing five-value failure vocabulary.

WP05 implements the minimal **Application orchestration/use-case
boundary** over those contracts.

WP05 must not implement SQLite/SQL, physical query planning,
schema/index/migration changes, DI registration, Worker
configuration/routing/output, or later process-level integration.

------------------------------------------------------------------------

## 2. Authoritative Inputs

Read completely before mutation:

-   `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md`
-   `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE_DISCOVERY.md`
-   `docs/architecture/data/EXPERIMENT_DISCOVERY_IDENTITY_PROVENANCE_FIDELITY.md`
-   `src/AIQuantTradingResearch.Application/Experiments/ExperimentPersistenceContracts.cs`
-   GitHub issue #201
-   existing Release 1.6 Application orchestration/use-case
    implementations and tests relevant to durable Experiment evidence.

Treat WP02--WP04 as frozen predecessor authority.

Do not redesign their contracts or semantics unless an actual
contradiction blocks WP05.

------------------------------------------------------------------------

## 3. Execution-Authority Lifecycle

The WP05 prompt pair follows the established Release 1.7
execution-authority lifecycle.

Do not:

-   stage it merely because it exists;
-   count authority files as production/test/schema mutations;
-   remove prior authority pairs without separate authority;
-   allow expected untracked authority files to create a false blocker.

Report their classification in the final execution report.

------------------------------------------------------------------------

## 4. Mandatory Starting Gate

Before any WP05 mutation verify:

-   branch: `main`;
-   HEAD: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main`: same SHA;
-   ahead/behind: `0/0`;
-   staged paths: 0;
-   tracked mutations: 0;
-   no active merge/rebase/cherry-pick/revert;
-   no conflict markers;
-   #197--#200: CLOSED / Done;
-   #201: OPEN / Backlog;
-   #202--#209: OPEN / Backlog;
-   milestone #55: OPEN, 9 open / 4 closed;
-   Project #2 planning state reconciled;
-   schema remains v3;
-   canonical baseline remains 250 tests;
-   WP04 contract surface exists exactly as accepted;
-   no WP05+ implementation already exists.

Reconcile expected untracked planning/documentation/authority content
against the manifest and completed WPs.

Unexpected tracked/staged state blocks execution.

------------------------------------------------------------------------

## 5. Repository-Native Orchestration Pattern

Before implementation inspect the current Application layer for
established patterns, especially:

-   `DurableExperimentUseCase`;
-   Experiment generation/application orchestration;
-   Feature-generation orchestration if relevant;
-   store invocation conventions;
-   request validation conventions;
-   success/failure result propagation;
-   cancellation conventions;
-   unknown-exception propagation;
-   test structure and naming.

Reuse repository-native patterns.

Do not introduce a generic mediator, command bus, pipeline framework,
result monad, retry abstraction, or other new orchestration
architecture.

------------------------------------------------------------------------

## 6. WP05 Use-Case Objective

Implement the smallest Application use case that:

1.  receives `DurableExperimentDiscoveryRequest`;
2.  enforces Application-owned request semantics assigned to the
    use-case boundary;
3.  invokes `IDurableExperimentEvidenceDiscoveryStore` exactly once for
    a valid request;
4.  returns the store's successful discovery evidence through the
    accepted Application result contract;
5.  preserves successful empty collections;
6.  preserves deterministic identity ordering and evidence fidelity;
7.  propagates the accepted bounded failure semantics without fallback
    or repair;
8.  allows unknown defects to propagate.

The use case must remain provider-agnostic and storage-agnostic.

------------------------------------------------------------------------

## 7. Use-Case Contract

Follow the repository's established naming and public-contract
conventions.

If the Release 1.7 manifest assigns a new use-case interface to WP05,
introduce the minimal interface required for later DI/Worker
composition.

Prefer a repository-native shape analogous to predecessor use cases.

Do not expose:

-   SQLite types;
-   SQL;
-   table/index/schema concepts;
-   Worker configuration;
-   provider/network concepts.

Do not add speculative overloads or future query variants.

------------------------------------------------------------------------

## 8. Request Validation

WP04 established the typed request and positive bounded maximum
semantics.

WP05 must enforce only the Application-owned validation that remains
required by the accepted contracts and repository conventions.

Requirements:

-   exact Snapshot Identity remains mandatory;
-   exact Experiment Definition Identity remains mandatory;
-   maximum must be valid and positive;
-   no zero-as-unbounded behavior;
-   no negative maximum;
-   no implicit default;
-   no wildcard or fallback semantics;
-   no numeric upper ceiling may be invented.

If WP04's contract construction already makes an invalid state
impossible, do not duplicate validation merely for ceremony.

If invalid request state remains representable by design, map it
deterministically to the existing `InvalidRequest` semantics before
store invocation.

Invalid request must invoke the discovery store:

`0 times`

------------------------------------------------------------------------

## 9. Exactly-Once Store Invocation

For each valid request:

`IDurableExperimentEvidenceDiscoveryStore` must be invoked exactly once.

WP05 must not:

-   retry;
-   query twice to verify results;
-   preflight storage;
-   fall back to exact retrieval;
-   call durable acceptance;
-   generate Experiment evidence;
-   generate Feature Sets;
-   call providers;
-   perform a second query for ordering.

The Infrastructure implementation later owns the physical query.

The Application use case owns orchestration only.

------------------------------------------------------------------------

## 10. Successful Result Propagation

On store success:

-   return a successful `DurableExperimentDiscoveryResult`;
-   preserve zero-or-more cardinality;
-   preserve successful empty collection;
-   preserve Experiment Result Identity ascending ordering;
-   preserve every item's identity/provenance/fidelity;
-   do not recompute evidence;
-   do not rewrite collection membership;
-   do not mutate returned evidence.

If the store contract already guarantees ordering, the use case must not
perform redundant sorting unless the accepted Application contract
explicitly assigns ordering enforcement to the Application boundary.

Inspect WP04 contract semantics before deciding.

Avoid duplicated responsibility.

------------------------------------------------------------------------

## 11. Evidence Fidelity

WP05 must preserve WP03 invariants exactly:

-   `aiq-experiment-identity-v1` unchanged;
-   no discovery/query identity;
-   Snapshot Identity/Version provenance unchanged;
-   Experiment Definition provenance unchanged;
-   Feature Set lineage unchanged;
-   count unchanged;
-   canonical decimals unchanged;
-   signed-zero semantics unchanged;
-   aggregate presence/absence unchanged;
-   empty Experiment Result remains distinct from empty discovery
    collection.

No mapping may lose information.

Prefer passing/reusing the accepted `DurableExperimentEvidence`
representation rather than creating a new DTO.

------------------------------------------------------------------------

## 12. Failure Propagation

Preserve the existing bounded failure vocabulary:

-   `InvalidRequest`;
-   `NotFound`;
-   `DependencyUnavailable`;
-   `InvalidEvidence`;
-   `IntegrityConflict`.

Discovery-specific rule:

A valid zero-match discovery is successful empty evidence and must never
be transformed into `NotFound`.

For failures returned by the discovery store:

-   propagate according to the established Application result/failure
    convention;
-   do not retry;
-   do not recover;
-   do not repair;
-   do not substitute provider data;
-   do not transform one accepted failure into another without existing
    repository authority.

Unknown defects/exceptions must propagate.

Do not catch `Exception` broadly merely to normalize it.

------------------------------------------------------------------------

## 13. No Mutation Semantics

WP05 discovery is read-only.

The use case must not invoke:

-   durable acceptance/store-write methods;
-   update/delete methods;
-   generation use cases;
-   provider acquisition;
-   schema initialization beyond whatever later Infrastructure naturally
    requires to read;
-   repair/reconciliation operations.

WP05 must not mutate durable state.

------------------------------------------------------------------------

## 14. Dependency Direction

Preserve:

`Worker → Application → Domain`

and Infrastructure implementation behind Application abstractions.

WP05 may depend on:

-   WP04 Application contracts;
-   existing Domain/Application identity/evidence types;
-   `IDurableExperimentEvidenceDiscoveryStore`.

WP05 must not depend on Infrastructure concrete types.

No project/reference changes are expected.

Architecture.Tests must pass unchanged.

------------------------------------------------------------------------

## 15. WP06 Boundary Protection

WP06 owns the **schema-v3 bounded query-plan proof**.

WP05 must not:

-   inspect or optimize SQL;
-   choose indexes;
-   assume row ordering;
-   decide scan strategy;
-   alter schema;
-   benchmark SQLite;
-   encode physical storage constraints into Application orchestration.

If correct Application behavior appears to require a structural
persistence change, stop.

------------------------------------------------------------------------

## 16. WP07 Boundary Protection

WP07 owns Infrastructure persistence implementation.

WP05 must not implement:

-   SQLite query methods;
-   row mapping;
-   connection handling;
-   storage failure classification;
-   schema validation;
-   physical ordering.

Only the Application use case is authorized here.

------------------------------------------------------------------------

## 17. Later Composition Boundary Protection

Do not perform work reserved for later composition/Worker WPs:

-   DI registration;
-   duplicate-registration checks;
-   Worker configuration;
-   Worker selector precedence;
-   Worker one-shot execution;
-   output formatting;
-   process exit codes;
-   process-level probes.

No `Program.cs` change is authorized unless the Release 1.7 file
manifest explicitly assigns one to WP05; if it unexpectedly does,
reconcile against the execution plan before proceeding.

------------------------------------------------------------------------

## 18. Manifest-Bounded Mutation

Read `RELEASE_1.7_FILE_MANIFEST.md` before editing.

Only create or modify WP05-authorized paths.

Expected work is confined to Application orchestration/use-case code and
any explicitly authorized focused Application tests.

For every changed path report:

-   new/modified;
-   why WP05 owns it;
-   whether production or test;
-   why it does not consume a later WP.

Do not opportunistically refactor predecessor code.

------------------------------------------------------------------------

## 19. Focused Permanent Application Tests

If the manifest/execution plan assigns Application use-case regression
tests to WP05, add the smallest permanent offline test set necessary to
prove the new orchestration.

Coverage should include, where representable:

-   valid request invokes discovery store exactly once;
-   successful non-empty result propagates exact evidence;
-   successful empty result remains successful empty;
-   identity ordering is preserved according to contract ownership;
-   `InvalidRequest` short-circuits before store invocation when invalid
    state is representable;
-   each store-returned bounded failure propagates correctly;
-   first failure short-circuits;
-   unknown defect propagates;
-   no generation/acceptance/provider fallback occurs.

Use deterministic in-memory/fake/stub Application test doubles
consistent with repository conventions.

Do not add Infrastructure or Worker process tests.

If the plan reserves all permanent tests for a later WP, do not add
tests here; instead perform only authorized validation and report test
delta 0.

------------------------------------------------------------------------

## 20. Test Quality Constraints

Tests must be:

-   deterministic;
-   offline;
-   isolated;
-   permanent only if manifest-authorized;
-   focused on Application behavior;
-   free of SQLite implementation dependence;
-   free of network/provider calls;
-   free of timing/sleep/retry behavior.

Do not overfit tests to private implementation details.

Prefer contract-observable assertions.

------------------------------------------------------------------------

## 21. Expected Deltas

Expected production delta:

-   minimal Application use-case/interface paths authorized by WP05.

Expected Infrastructure delta:

`0`

Expected Worker delta:

`0`

Expected schema delta:

`0`

Expected package/project/reference delta:

`0`

Expected Architecture.Tests delta:

`0`

Permanent Application test delta:

-   exactly what the Release 1.7 manifest/execution plan assigns to
    WP05;
-   otherwise `0`.

Provider/network activity:

`0`

------------------------------------------------------------------------

## 22. Canonical Validation

After implementation run targeted tests first if useful, then canonical
verification.

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
-   temporary database/process/probe residue: 0.

Report exact test counts and delta from the WP04 baseline of 250.

------------------------------------------------------------------------

## 23. Schema/Dependency/Behavior Validation

Require:

-   schema remains v3;
-   no table/column/index/migration delta;
-   package delta: 0;
-   project delta: 0;
-   reference delta: 0;
-   dependency graph remains acyclic;
-   Application → Infrastructure dependency: absent;
-   DI state unchanged;
-   Worker routing unchanged;
-   predecessor Release 1.1--1.6 behavior preserved.

------------------------------------------------------------------------

## 24. GitHub Lifecycle

Only after every WP05 acceptance gate passes:

1.  move #201 Backlog → In Progress if necessary;
2.  post concise completion evidence to #201;
3.  close #201;
4.  set #201 Project Status to Done.

Final required state:

-   #197--#201: CLOSED / Done;
-   #202--#209: OPEN / Backlog;
-   milestone #55: OPEN, 8 open / 5 closed;
-   Project membership remains 13/13;
-   dependencies unchanged;
-   Priority/Release/Area unchanged.

Do not transition #202 automatically.

------------------------------------------------------------------------

## 25. Mutation Budget

### Application production

Only manifest-authorized WP05 use-case/interface paths.

### Application tests

Only if explicitly WP05-authorized.

### Infrastructure

`0`

### Worker

`0`

### Schema

`0`

### Packages/projects/references

`0`

### Git transport

`0`

### GitHub

Only #201 lifecycle mutations.

------------------------------------------------------------------------

## 26. Stop Conditions

Stop with:

`RELEASE 1.7 WP05 BLOCKED`

if:

-   starting state differs materially;
-   WP04 contract surface differs materially from accepted state;
-   WP02/WP03 semantics cannot be preserved;
-   a numeric maximum ceiling would need invention;
-   store invocation semantics cannot be expressed without changing WP04
    contracts outside WP05 authority;
-   Infrastructure/SQL implementation is required;
-   schema/index/migration mutation is required;
-   DI/Worker changes are required;
-   package/project/reference changes are unexpectedly required;
-   architecture rules would need weakening;
-   required paths lie outside the manifest;
-   canonical validation fails;
-   provider/network execution or real credentials would be required.

Report the exact blocker and smallest corrective authority required.

------------------------------------------------------------------------

## 27. Required Execution Report

Report:

1.  baseline and starting state;
2.  authoritative inputs read;
3.  repository-native orchestration patterns inspected;
4.  WP05 manifest-authorized paths;
5.  use-case/interface shape;
6.  request validation ownership;
7.  exactly-once store invocation;
8.  successful non-empty propagation;
9.  successful empty propagation;
10. identity/order/provenance/fidelity preservation;
11. bounded failure propagation;
12. unknown-defect propagation;
13. read-only/no-fallback proof;
14. WP06/WP07/composition boundary preservation;
15. changed paths;
16. focused test coverage and exact delta;
17. schema/package/project/reference deltas;
18. canonical validation counts;
19. offline/security/residue evidence;
20. GitHub lifecycle;
21. final milestone counts;
22. next authorized action.

------------------------------------------------------------------------

## 28. Completion Markers

On success end exactly:

`RELEASE 1.7 WP05 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP06 — Schema-v3 Discovery Query Plan — GitHub issue #202`

Do not execute WP06 automatically.

If blocked end exactly:

`RELEASE 1.7 WP05 BLOCKED`

and identify the smallest corrective authority required.
