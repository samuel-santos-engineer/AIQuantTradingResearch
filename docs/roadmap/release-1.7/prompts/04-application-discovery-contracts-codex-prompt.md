# Release 1.7 WP04 --- Application Discovery Contracts --- Codex Authority

## 1. Mission

Execute Release 1.7 WP04 --- **Application Discovery Contracts** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#200`

Frozen predecessor baseline:

`95745fc2289ea855af39ba5e7bc0236a67f1c48b`

Authoritative milestone:

`#55 — Phase 4 - Release 1.7: Durable Experiment Evidence Discovery`

Accepted predecessor state:

-   WP01 #197: CLOSED / Done;
-   WP02 #198: CLOSED / Done;
-   WP03 #199: CLOSED / Done;
-   WP04 #200: OPEN / Backlog;
-   #201--#209: OPEN / Backlog;
-   milestone #55: OPEN, 10 open / 3 closed;
-   canonical permanent baseline: 250/250;
-   schema: v3;
-   production implementation delta through WP03: 0.

WP04 is the first Release 1.7 production-contract work package.

Its purpose is to express the already-frozen WP02/WP03 discovery
semantics as a minimal, strongly typed **Application boundary**.

WP04 must not implement orchestration, SQLite access, SQL, physical
query planning, DI registration, Worker routing/configuration/output,
schema/index/migration changes, or permanent tests reserved for later
WPs.

------------------------------------------------------------------------

## 2. Authoritative Inputs

Read completely before mutation:

-   `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md`
-   `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE_DISCOVERY.md`
-   `docs/architecture/data/EXPERIMENT_DISCOVERY_IDENTITY_PROVENANCE_FIDELITY.md`
-   GitHub issue #200
-   existing Release 1.6 Application contracts for durable Experiment
    evidence;
-   existing identity/value types in Domain/Application required to
    reuse accepted semantics.

Treat WP02 and WP03 as frozen predecessor authority.

Do not reopen their decisions.

------------------------------------------------------------------------

## 3. Execution-Authority Lifecycle

The WP04 prompt pair follows the established Release 1.7
execution-authority lifecycle.

Do not:

-   stage it merely because it exists;
-   count it as production/test/schema mutation;
-   remove prior WP prompt pairs without separate authority;
-   allow expected untracked authority files to create a false blocker.

Report authority-file classification in the execution report.

------------------------------------------------------------------------

## 4. Mandatory Starting Gate

Before any WP04 mutation verify:

-   branch: `main`;
-   HEAD: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main`: same SHA;
-   ahead/behind: `0/0`;
-   staged paths: 0;
-   tracked mutations: 0;
-   no active merge/rebase/cherry-pick/revert;
-   no conflict markers;
-   #197--#199: CLOSED / Done;
-   #200: OPEN / Backlog;
-   #201--#209: OPEN / Backlog;
-   milestone #55: OPEN, 10 open / 3 closed;
-   Project #2 planning state reconciled;
-   schema: v3;
-   canonical baseline: 250 tests;
-   WP02/WP03 semantic documents present;
-   no WP04+ production implementation already exists.

Reconcile expected untracked planning, semantic-documentation, and
authority files against the Release 1.7 manifest and completed WPs.

Unexpected tracked/staged state blocks execution.

------------------------------------------------------------------------

## 5. WP04 Contract Objective

Create the smallest Application contract surface needed for later
Release 1.7 discovery orchestration.

The Application boundary must represent:

1.  a typed discovery request;
2.  exact Snapshot Identity;
3.  exact Experiment Definition Identity;
4.  mandatory positive bounded maximum;
5.  a typed successful discovery result containing zero or more
    immutable durable Experiment Result evidence items;
6.  the existing bounded Application failure vocabulary/shape as
    appropriate to current repository conventions;
7.  a persistence-facing abstraction sufficient for WP05 orchestration
    and WP07 Infrastructure implementation, if and only if the Release
    1.7 execution plan/file manifest assigns that abstraction to WP04.

Do not implement the use case.

------------------------------------------------------------------------

## 6. Repository-Native Contract Design First

Before creating types, inspect the current Application project for
established patterns from Releases 1.1--1.6.

At minimum inspect:

-   durable Experiment use-case contracts;
-   exact Experiment Result retrieval/store abstractions;
-   Application result/failure representations;
-   request/value validation patterns;
-   immutable collection patterns;
-   namespace/file naming conventions;
-   cancellation-token conventions, if any;
-   Domain identity types already exposed through Application contracts.

Reuse established repository conventions unless they conflict with the
authoritative Release 1.7 semantics.

Do not create a parallel contract style.

------------------------------------------------------------------------

## 7. Discovery Request Contract

Represent the discovery request as a strongly typed immutable
Application contract.

It must carry exactly the semantic query inputs:

-   Snapshot Identity;
-   Experiment Definition Identity;
-   maximum result count.

Requirements:

-   both identities are mandatory;
-   maximum is mandatory;
-   maximum must semantically be positive;
-   no wildcard/null/optional identity semantics;
-   no free-text criteria;
-   no Feature Set filter;
-   no Experiment Result Identity filter;
-   no date/time filter;
-   no registry/history selector;
-   no sort selector;
-   no pagination/cursor;
-   no provider selector.

Do not add fields for anticipated future use.

------------------------------------------------------------------------

## 8. Maximum Cardinality Contract

Do not invent a numeric maximum ceiling.

The Application contract must make invalid non-positive intent
representable only in a way consistent with repository validation
conventions.

WP04 may:

-   use an existing validated value type if one already fits exactly; or
-   define the minimal Application-owned representation required by the
    manifest/execution plan.

WP04 must not:

-   choose a storage-dependent ceiling;
-   silently clamp values;
-   silently substitute a default;
-   allow zero to mean "all";
-   allow negative values;
-   introduce unbounded discovery.

If the concrete supported upper ceiling is intentionally deferred by
planning, preserve that deferral.

------------------------------------------------------------------------

## 9. Discovery Result Contract

Represent successful discovery as a typed immutable collection of
durable Experiment Result evidence.

Requirements:

-   cardinality may be zero;
-   successful empty collection is first-class;
-   empty collection is not `NotFound`;
-   each item is existing immutable durable Experiment Result evidence;
-   item identity/provenance/fidelity is preserved;
-   result ordering semantics remain Experiment Result Identity
    ascending;
-   contract must not expose mutable collection state;
-   contract must not expose persistence implementation details.

Prefer repository-native immutable/read-only collection conventions.

Do not invent paging metadata.

------------------------------------------------------------------------

## 10. Evidence Representation Reuse

Inspect the accepted Release 1.6 Application/domain evidence
representation.

Prefer reuse over duplication.

WP04 must not create a second structurally equivalent Experiment Result
evidence DTO merely for discovery unless the authoritative plan
explicitly requires one.

Preserve:

-   Experiment Result Identity;
-   Snapshot identity/version provenance;
-   Experiment Definition provenance;
-   Feature Set lineage;
-   count;
-   aggregate presence/absence;
-   canonical decimal fidelity.

If existing evidence contracts cannot safely represent discovery items
without semantic loss, stop and report the missing contract requirement
rather than silently duplicating or weakening evidence.

------------------------------------------------------------------------

## 11. Application-Owned InvalidRequest Boundary

Preserve `InvalidRequest` as Application-owned.

WP04 must establish the contract boundary needed for later validation
of:

-   malformed/invalid mandatory identity input where applicable;
-   invalid maximum cardinality intent.

Do not implement WP05 orchestration.

Do not normalize Infrastructure failures in WP04.

Do not add discovery-specific failure values.

The accepted five-value vocabulary remains:

-   `InvalidRequest`
-   `NotFound`
-   `DependencyUnavailable`
-   `InvalidEvidence`
-   `IntegrityConflict`

For discovery:

-   valid zero-match success is not `NotFound`.

Unknown defects remain outside broad normalization.

------------------------------------------------------------------------

## 12. Persistence-Facing Abstraction Boundary

Read the Release 1.7 execution plan and file manifest carefully.

If WP04 owns the Application-side persistence abstraction, define only
the minimal discovery operation required by the frozen semantics.

It must express:

-   exact Snapshot Identity;
-   exact Experiment Definition Identity;
-   bounded maximum;
-   asynchronous/cancellation semantics only if consistent with
    repository conventions;
-   successful ordered collection;
-   existing bounded failure representation as required by current
    architecture.

It must not expose:

-   SQL;
-   SQLite connection types;
-   table names;
-   columns;
-   indexes;
-   query plans;
-   row IDs;
-   schema version;
-   provider types.

If the manifest assigns the persistence abstraction to a later WP, do
not create it here.

------------------------------------------------------------------------

## 13. Dependency Direction

Preserve the established dependency architecture.

Application contracts may depend only on layers/types already permitted
by the architecture.

Infrastructure must remain the implementation side of persistence.

Worker must remain a composition/execution boundary.

Do not add:

-   Application → Infrastructure references;
-   Domain → Application references;
-   Worker-owned contract types used by Application;
-   persistence-library types in Application public contracts.

Architecture.Tests must continue to pass without weakening rules.

------------------------------------------------------------------------

## 14. WP05 Boundary Protection

WP05 owns **Durable Evidence Discovery Use Case**.

WP04 must not implement:

-   orchestration;
-   validation flow beyond contract/value construction semantics already
    assigned here;
-   store invocation;
-   failure propagation logic;
-   collection post-processing;
-   use-case implementation;
-   logging/telemetry for execution.

WP04 creates the boundary WP05 will implement against.

Do not instantiate Infrastructure dependencies.

------------------------------------------------------------------------

## 15. WP06/WP07 Boundary Protection

WP04 is storage-agnostic.

Do not:

-   write SQL;
-   select a query plan;
-   decide index requirements;
-   add schema artifacts;
-   modify schema version;
-   create migrations;
-   add SQLite-specific options;
-   optimize physical access.

WP06 remains the structural query-plan gate.

WP07 owns SQLite durable discovery implementation.

Schema remains v3.

------------------------------------------------------------------------

## 16. WP09/WP10 Boundary Protection

Do not perform:

-   DI registration;
-   configuration binding;
-   Worker configuration;
-   Worker routing;
-   Worker output;
-   process-level execution.

Those are later work packages.

------------------------------------------------------------------------

## 17. Manifest-Bounded Mutation

Read `RELEASE_1.7_FILE_MANIFEST.md` before editing.

Only create or modify paths explicitly authorized for WP04.

For each changed path report:

-   why WP04 owns it;
-   whether it is new or modified;
-   whether it is production Application contract code or authorized
    documentation alignment.

Do not opportunistically refactor predecessor code.

Do not rename unrelated types.

Do not reformat unrelated files.

If a required path is outside the manifest, stop.

------------------------------------------------------------------------

## 18. Production Mutation Constraints

WP04 may introduce only the minimal Application contract production code
authorized by the manifest.

Expected production behavior delta:

`0`

Contracts may compile into production assemblies, but no executable
discovery behavior may become reachable.

Expected Infrastructure implementation delta:

`0`

Expected Worker behavior delta:

`0`

Expected schema delta:

`0`

Expected DI delta:

`0`

Expected provider/network behavior delta:

`0`

------------------------------------------------------------------------

## 19. Permanent Test Policy

Follow the Release 1.7 execution plan/file manifest.

WP04 must not consume WP11's persistence/process regression-test
authority.

If the repository convention requires focused contract/value tests for a
new validated Application value type and the manifest explicitly
authorizes them in WP04, add only those narrowly required tests.

Otherwise expected permanent-test delta is:

`0`

Do not add Infrastructure or Worker process tests.

Architecture.Tests delta is expected to remain:

`0`

Do not weaken architecture rules to make WP04 compile.

------------------------------------------------------------------------

## 20. Contract Quality Gates

Verify the new contract surface is:

-   minimal;
-   immutable;
-   strongly typed;
-   nullable-safe;
-   deterministic;
-   storage-agnostic;
-   provider-agnostic;
-   free of speculative fields;
-   consistent with naming conventions;
-   consistent with current async/cancellation conventions;
-   consistent with current failure/result conventions;
-   documented sufficiently for public/internal contract expectations
    already established by repository standards.

No broad abstractions "for future extensibility" are authorized.

------------------------------------------------------------------------

## 21. Canonical Validation

Run canonical repository verification after WP04 implementation.

Require:

-   Domain.Tests: baseline preserved unless manifest-authorized focused
    delta exists;
-   Application.Tests: baseline preserved unless manifest-authorized
    focused delta exists;
-   Infrastructure.Tests: 117/117 unless authorized predecessor-neutral
    delta exists;
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

Explicitly report test deltas from the 250-test baseline.

------------------------------------------------------------------------

## 22. Schema and Dependency Validation

After WP04 verify:

-   schema remains v3;
-   no table/column/index/migration change;
-   package delta: 0;
-   project delta: 0;
-   project-reference delta: 0 unless explicitly manifest-authorized;
-   dependency graph remains acyclic;
-   no Application → Infrastructure dependency;
-   Worker routing unchanged;
-   DI registrations unchanged.

------------------------------------------------------------------------

## 23. GitHub Lifecycle

Only after every WP04 acceptance gate passes:

1.  move #200 Backlog → In Progress if necessary;
2.  post concise completion evidence to #200;
3.  close #200;
4.  set #200 Project Status to Done.

Final required state:

-   #197--#200: CLOSED / Done;
-   #201--#209: OPEN / Backlog;
-   milestone #55: OPEN, 9 open / 4 closed;
-   Project membership: 13/13;
-   dependencies unchanged;
-   Priority/Release/Area unchanged.

Do not transition #201 automatically.

------------------------------------------------------------------------

## 24. Mutation Budget

### Application production contracts

Only manifest-authorized WP04 paths.

### Executable orchestration

`0`

### Infrastructure

`0`

### Worker

`0`

### Schema

`0`

### Packages/projects/references

`0` unless the manifest explicitly proves otherwise.

### Permanent tests

`0` by default; only narrowly manifest-authorized contract/value tests
if explicitly required.

### Git transport

`0`

### GitHub

Only #200 lifecycle mutations.

------------------------------------------------------------------------

## 25. Stop Conditions

Stop with:

`RELEASE 1.7 WP04 BLOCKED`

if:

-   starting state differs materially;
-   WP02/WP03 semantics conflict;
-   existing evidence representation cannot preserve required fidelity;
-   a new discovery identity appears necessary;
-   a numeric maximum ceiling would need to be invented;
-   Application contracts would need persistence-specific types;
-   orchestration/use-case implementation is required;
-   SQL/SQLite work is required;
-   schema/index/migration mutation is required;
-   DI/Worker changes are required;
-   package/project/reference changes are unexpectedly required;
-   a required path lies outside the manifest;
-   architecture rules would need weakening;
-   canonical validation fails;
-   provider/network execution or real credentials would be required.

Report the exact blocker and smallest corrective authority required.

------------------------------------------------------------------------

## 26. Required Execution Report

Report:

1.  baseline and starting state;
2.  authoritative inputs read;
3.  repository-native contract patterns inspected;
4.  WP04 manifest-authorized paths;
5.  request contract;
6.  maximum-cardinality representation;
7.  successful result/collection contract;
8.  evidence representation reuse;
9.  `InvalidRequest` ownership;
10. persistence-facing abstraction, if WP04-authorized;
11. dependency-direction proof;
12. WP05/WP06/WP07/WP09/WP10 boundary preservation;
13. changed paths;
14. production behavior delta;
15. permanent-test delta;
16. schema/package/project/reference deltas;
17. canonical validation;
18. offline/security/residue evidence;
19. GitHub lifecycle;
20. final milestone counts;
21. next authorized action.

------------------------------------------------------------------------

## 27. Completion Markers

On success end exactly:

`RELEASE 1.7 WP04 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP05 — Durable Evidence Discovery Use Case — GitHub issue #201`

Do not execute WP05 automatically.

If blocked end exactly:

`RELEASE 1.7 WP04 BLOCKED`

and identify the smallest corrective authority required.
