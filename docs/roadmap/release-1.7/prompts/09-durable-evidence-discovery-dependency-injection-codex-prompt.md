# Release 1.7 WP09 --- Durable Evidence Discovery Dependency Injection --- Codex Authority

## 1. Mission

Execute Release 1.7 WP09 --- **Durable Evidence Discovery Dependency
Injection** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#205`

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
-   WP09 #205: OPEN / Backlog;
-   #206--#209: OPEN / Backlog;
-   milestone #55: OPEN, 5 open / 8 closed;
-   canonical permanent baseline: 250/250;
-   schema: v3.

Accepted Release 1.7 implementation state:

-   Application owns `IDurableExperimentDiscoveryUseCase`;
-   Application owns `DurableExperimentDiscoveryUseCase`;
-   Application owns `IDurableExperimentEvidenceDiscoveryStore`;
-   Infrastructure `SqliteExperimentResultStore` implements the
    discovery store;
-   WP07 froze successful SQLite discovery behavior;
-   WP08 froze failure semantics;
-   no discovery DI wiring has yet been authorized;
-   Worker discovery routing remains later-WP authority.

WP09 must add only the smallest composition-root registrations required
to make the accepted discovery use case resolvable through dependency
injection.

Do not execute discovery through the Worker.

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
-   `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteExperimentResultStore.cs`
-   current Application/Infrastructure service-registration extensions;
-   current Worker composition root;
-   existing DI registration/cardinality tests and conventions;
-   GitHub issue #205.

Treat WP02--WP08 semantics and implementations as frozen predecessor
authority.

------------------------------------------------------------------------

## 3. Flow Preservation

Preserve the prepared Release 1.7 flow.

WP09 is composition work only.

Do not:

-   redesign contracts;
-   change discovery semantics;
-   alter SQLite query behavior;
-   change failure semantics;
-   refactor unrelated DI;
-   change Worker routing/configuration/output;
-   begin the deferred Release 1.8 Architecture & Design Review.

Non-blocking design observations are deferred.

------------------------------------------------------------------------

## 4. Execution-Authority Lifecycle

The WP09 prompt pair follows the established Release 1.7
execution-authority lifecycle.

Do not stage it merely because it exists.

Do not count expected execution-only authority files as production/test
mutation.

Do not remove prior governed artifacts without authority.

Reconcile expected untracked authority/planning files rather than
treating them as unexplained state.

------------------------------------------------------------------------

## 5. Mandatory Starting Gate

Before mutation verify:

-   branch: `main`;
-   HEAD: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main`: same SHA;
-   ahead/behind: `0/0`;
-   staged paths: 0;
-   tracked mutations: 0;
-   no merge/rebase/cherry-pick/revert;
-   conflict markers: 0;
-   #197--#204: CLOSED / Done;
-   #205: OPEN / Backlog;
-   #206--#209: OPEN / Backlog;
-   milestone #55: OPEN, 5 open / 8 closed;
-   Project #2 membership/configuration reconciled;
-   schema: v3;
-   permanent baseline: 250/250;
-   WP05 use case exists;
-   WP07 Infrastructure store exists;
-   WP08 failure semantics are unchanged;
-   no premature discovery Worker mode exists.

Unexpected tracked/staged state blocks execution.

------------------------------------------------------------------------

## 6. Existing DI Pattern Inspection

Before editing, inspect the repository-native DI model.

Determine:

-   where Application services are registered;
-   where Infrastructure persistence services are registered;
-   lifetimes used for comparable Release 1.5/1.6 services;
-   whether one concrete SQLite store already implements multiple
    Application persistence interfaces;
-   whether registrations use concrete-to-interface forwarding or
    separate construction;
-   how configuration/connection factory dependencies are supplied;
-   how registration cardinality is tested;
-   how side-effect-free resolution is tested.

Follow the established pattern exactly unless doing so would create
duplicate instances or violate accepted ownership.

Do not introduce a new DI style.

------------------------------------------------------------------------

## 7. Required Registrations

Register exactly the services necessary for discovery composition.

At minimum, resolve:

`IDurableExperimentDiscoveryUseCase`

to the accepted Application implementation:

`DurableExperimentDiscoveryUseCase`

and resolve:

`IDurableExperimentEvidenceDiscoveryStore`

to the accepted SQLite implementation:

`SqliteExperimentResultStore`

using the repository-native lifetime and forwarding pattern.

If `SqliteExperimentResultStore` already has an existing concrete
registration because it serves Release 1.6 interfaces, prefer forwarding
the discovery abstraction to that existing concrete service where
repository conventions support it.

Do not create duplicate concrete store instances accidentally.

------------------------------------------------------------------------

## 8. Registration Cardinality

After WP09, each discovery abstraction must have exactly one effective
registration.

Prove:

-   `IDurableExperimentDiscoveryUseCase`: exactly 1;
-   `IDurableExperimentEvidenceDiscoveryStore`: exactly 1.

Also verify WP09 did not duplicate predecessor registrations for:

-   durable Experiment use case/store;
-   Feature services;
-   pipeline services;
-   connection factory;
-   other SQLite persistence services.

Do not use `TryAdd` or duplicate-tolerant behavior to hide an incorrect
registration graph unless that is already the explicit repository
convention.

------------------------------------------------------------------------

## 9. Lifetime Consistency

Use the lifetime established by comparable repository services.

Do not invent singleton/scoped/transient policy.

The chosen lifetime must:

-   preserve existing connection ownership;
-   not retain open SQLite connections;
-   not create global mutable state;
-   remain compatible with Worker one-shot composition;
-   preserve side-effect-free service resolution.

Report the lifetime and predecessor pattern used as authority.

------------------------------------------------------------------------

## 10. Side-Effect-Free Resolution

Resolving discovery services from the DI container must not:

-   open SQLite;
-   create a database;
-   create schema;
-   run migrations;
-   execute discovery;
-   access provider/network;
-   require real credentials;
-   mutate durable state.

Construction/resolution must remain side-effect free.

Actual storage access occurs only when the use case/store operation is
invoked.

------------------------------------------------------------------------

## 11. Application Ownership

The composition root may reference Application
abstractions/implementations according to existing architecture rules.

Do not move DI concerns into Domain.

Do not make Application depend on Infrastructure.

Do not add SQLite types to Application contracts.

Do not alter:

-   `IDurableExperimentDiscoveryUseCase`;
-   `DurableExperimentDiscoveryUseCase`;
-   `IDurableExperimentEvidenceDiscoveryStore`;
-   discovery request/result types.

------------------------------------------------------------------------

## 12. Infrastructure Ownership

Infrastructure continues to own:

-   `SqliteExperimentResultStore`;
-   SQLite mechanics;
-   schema-v3 access;
-   row mapping;
-   failure classification.

WP09 must not modify the WP07 SQL unless an actual DI-blocking defect
proves unavoidable.

A preference for another constructor or registration layout is not
authority for persistence redesign.

------------------------------------------------------------------------

## 13. Worker Boundary Protection

WP09 may touch the existing composition root only to the extent the
manifest assigns DI registration there.

WP09 must not add:

-   discovery command-line selectors;
-   discovery environment variables;
-   discovery configuration parsing;
-   discovery precedence;
-   discovery execution branch;
-   discovery output;
-   discovery exit-code behavior.

Those belong to later work.

A DI resolution proof is not Worker execution authority.

------------------------------------------------------------------------

## 14. No Premature Invocation

Do not invoke the discovery use case from production startup merely to
prove DI.

Do not add startup health checks that execute SQLite discovery.

Validation should resolve services through a test/probe/service provider
without triggering product behavior.

------------------------------------------------------------------------

## 15. Manifest-Bounded Mutation

Read `RELEASE_1.7_FILE_MANIFEST.md` before editing.

Only modify/create WP09-authorized paths.

Expected production changes are limited to existing DI
registration/composition files.

For each changed path report:

-   why WP09 owns it;
-   registration added;
-   lifetime;
-   whether it forwards to an existing concrete registration;
-   why no later-WP behavior is consumed.

If a required path lies outside the manifest, stop.

------------------------------------------------------------------------

## 16. Permanent Test Ownership

Respect the Release 1.7 manifest.

WP11 retains permanent discovery persistence/process regression
ownership unless the manifest explicitly assigns DI tests to WP09.

Expected permanent-test delta:

`0`

unless WP09 explicitly owns DI regression tests.

If permanent tests are not WP09-owned, use a removable DI probe to
establish:

-   cardinality;
-   resolution;
-   implementation type;
-   side-effect-free construction;
-   predecessor registration preservation.

Remove the probe completely.

Architecture.Tests delta:

`0`

------------------------------------------------------------------------

## 17. DI Validation Matrix

Prove at minimum:

### D1 --- Use Case Registration

`IDurableExperimentDiscoveryUseCase` resolves successfully.

### D2 --- Store Registration

`IDurableExperimentEvidenceDiscoveryStore` resolves successfully.

### D3 --- Correct Implementations

Resolved abstractions correspond to the accepted WP05/WP07
implementations.

### D4 --- Cardinality

Exactly one effective registration exists for each discovery
abstraction.

### D5 --- Lifetime

Discovery registrations use the repository-native comparable lifetime.

### D6 --- Shared Concrete Store Integrity

If `SqliteExperimentResultStore` serves multiple interfaces,
registration does not accidentally create conflicting duplicate concrete
instances contrary to repository convention.

### D7 --- Side-Effect-Free Resolution

Resolution creates no database/schema/network/provider/durable mutation.

### D8 --- Predecessor DI Preservation

Release 1.1--1.6 service registrations remain intact and nonduplicated.

### D9 --- Dependency Direction

Application remains independent of Infrastructure.

### D10 --- No Worker Execution

Discovery is not invoked during composition or startup.

------------------------------------------------------------------------

## 18. No Schema or Persistence Change

WP09 must leave unchanged:

-   schema version v3;
-   tables;
-   columns;
-   indexes;
-   migrations;
-   WP07 SQL;
-   19-column evidence mapper;
-   failure classification.

If DI cannot be completed without changing persistence/schema behavior,
stop.

------------------------------------------------------------------------

## 19. No Package/Project/Reference Change

WP09 must not add:

-   NuGet packages;
-   projects;
-   project references;
-   test-framework dependencies.

Use existing Microsoft DI facilities and repository dependencies.

Required deltas:

-   package: 0;
-   project: 0;
-   reference: 0.

------------------------------------------------------------------------

## 20. Targeted Validation

Run the smallest safe DI validation boundary first.

Require D1--D10 PASS.

If using a removable probe:

-   keep it outside governed production content unless
    manifest-authorized;
-   do not stage it;
-   remove it after validation;
-   verify residue 0.

No provider/network calls.

No real credentials.

------------------------------------------------------------------------

## 21. Canonical Validation

After WP09 mutation and targeted DI proof run canonical verification.

Require:

-   Domain.Tests: 11/11;
-   Application.Tests: 111/111;
-   Infrastructure.Tests: 117/117;
-   Architecture.Tests: 13/13;
-   permanent total: 250/250 unless explicitly authorized WP09 test
    delta exists;
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
-   temporary probe/process/database residue: 0.

Report exact permanent-test delta from 250.

------------------------------------------------------------------------

## 22. Architecture and Dependency Validation

Require:

-   Architecture.Tests remain 13/13;
-   production dependency graph remains acyclic;
-   Domain depends on no outer layer;
-   Application → Infrastructure dependency remains absent;
-   Infrastructure implements Application abstractions;
-   Worker/composition remains the outer wiring boundary;
-   no duplicate service registrations introduced;
-   no service-locator pattern introduced;
-   no static global container introduced.

Do not weaken an architecture rule to accommodate WP09.

------------------------------------------------------------------------

## 23. Predecessor Preservation

Verify composition still supports existing predecessor behavior without
executing unrelated product paths unnecessarily.

At minimum reconcile:

-   durable Experiment registrations;
-   Feature registrations;
-   pipeline registrations;
-   SQLite connection factory;
-   existing persistence interfaces;
-   provider registrations.

WP09 must be additive only at the intended discovery composition seam.

------------------------------------------------------------------------

## 24. Cleanup

Remove all disposable WP09 artifacts:

-   DI probes;
-   temporary projects/scripts;
-   temporary databases;
-   logs;
-   retained processes/handles.

Final disposable residue:

`0`

Do not remove governed Release 1.7 artifacts.

------------------------------------------------------------------------

## 25. GitHub Lifecycle

Only after every WP09 acceptance gate passes:

1.  move #205 Backlog → In Progress if necessary;
2.  post concise completion evidence to #205, including D1--D10;
3.  close #205;
4.  set #205 Project Status to Done.

Final required state:

-   #197--#205: CLOSED / Done;
-   #206--#209: OPEN / Backlog;
-   milestone #55: OPEN, 4 open / 9 closed;
-   Project membership: 13/13;
-   dependencies unchanged;
-   Priority/Release/Area unchanged.

Do not transition #206 automatically.

------------------------------------------------------------------------

## 26. Mutation Budget

### Domain

`0`

### Application production

`0` except an explicitly manifest-owned Application registration
extension if that is the established repository pattern.

### Infrastructure production

Only manifest-authorized DI registration/composition changes.

### Worker production

Only manifest-authorized registration call wiring; no discovery
routing/configuration/execution.

### Schema/persistence SQL

`0`

### Packages/projects/references

`0`

### Permanent tests

`0` by default; respect manifest ownership.

### Architecture rules

`0`

### Disposable validation

Authorized; residue 0.

### Git transport

`0`

### GitHub

Only #205 lifecycle mutations.

------------------------------------------------------------------------

## 27. Stop Conditions

Stop with:

`RELEASE 1.7 WP09 BLOCKED`

if:

-   starting state differs materially;
-   accepted WP05/WP07 implementations are absent or materially
    different;
-   DI requires Application → Infrastructure dependency;
-   registration requires schema/persistence redesign;
-   a new package/project/reference is required;
-   Worker discovery routing/execution is required;
-   duplicate concrete store instances cannot be avoided under existing
    composition conventions;
-   side-effect-free resolution cannot be preserved;
-   architecture rules would need weakening;
-   required permanent paths lie outside the manifest;
-   permanent tests would consume later manifest ownership;
-   canonical validation fails;
-   provider/network execution or real credentials would be required.

Report the exact blocker and smallest corrective authority required.

------------------------------------------------------------------------

## 28. Required Execution Report

Report:

1.  baseline and starting state;
2.  authoritative inputs read;
3.  existing DI conventions inspected;
4.  manifest-authorized paths;
5.  registrations added;
6.  lifetimes;
7.  concrete forwarding/shared-store behavior;
8.  D1--D10 matrix;
9.  cardinality proof;
10. side-effect-free resolution proof;
11. predecessor DI preservation;
12. Worker boundary preservation;
13. Application/Infrastructure dependency preservation;
14. changed permanent paths;
15. permanent-test delta;
16. schema/persistence/package/project/reference deltas;
17. targeted validation;
18. canonical validation;
19. offline/security/residue evidence;
20. GitHub lifecycle;
21. final milestone counts;
22. next authorized action.

------------------------------------------------------------------------

## 29. Completion Markers

On success end exactly:

`RELEASE 1.7 WP09 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP10 — Durable Evidence Discovery Worker Integration — GitHub issue #206`

Do not execute WP10 automatically.

If blocked end exactly:

`RELEASE 1.7 WP09 BLOCKED`

and identify the smallest corrective authority required.
