# Release 1.1 WP11 --- Dependency Registration & Configuration --- Codex Execution Authority

## 1. Authority

You are executing **Release 1.1 --- WP11: Dependency Registration & Configuration** for the `AIQuantTradingResearch` repository.

This file is the authoritative execution contract for WP11. Execute it literally and conservatively. Do not expand scope by inference.

Accepted predecessor chain:

- WP01 --- Release & Repository Preflight --- COMPLETE
- WP02 --- Persistence Technology Discovery --- COMPLETE
- WP03 --- Historical Observation Persistence Semantics --- COMPLETE
- WP04 --- Application Persistence Contracts --- COMPLETE
- WP05 --- Persistence Use-Case Integration --- COMPLETE
- WP06 --- Storage Physical Model --- COMPLETE
- WP07 --- Storage Engine & Connection Boundary --- COMPLETE
- WP08 --- Observation Persistence --- COMPLETE
- WP09 --- Historical Observation Retrieval --- COMPLETE
- WP10 --- Storage Validation & Failure Mapping --- COMPLETE
- WP11 --- Dependency Registration & Configuration --- CURRENT
- WP12+ --- NOT AUTHORIZED

GitHub planning identity:

- Release: `1.1`
- Milestone: `#52 — Phase 3 - Release 1.1: Market Data Persistence Foundation`
- WP11 issue: `#113 — Dependency Registration & Configuration`
- Required predecessor: `#112 — Storage Validation & Failure Mapping`
- Next issue: `#114 — Worker Persistent Market-Data Execution`

The Release 1.1 execution plan and file manifest remain governing authorities.

## 2. Mission

Wire the accepted Release 1.1 persistence slice into the repository's existing Microsoft dependency-injection and configuration composition model without beginning Worker persistence execution.

WP11 must make the already-accepted persistence contracts and Infrastructure implementation **resolvable through the real composition mechanism**, with explicit configuration and correct lifetime/ownership semantics.

The completed persistence chain must remain:

`Application contract → Infrastructure SQLite store → operation-owned SQLite connection factory → schema bootstrap → SQLite database`

WP11 owns:

- registration of the accepted Application persistence contract to the accepted Infrastructure implementation;
- registration/composition of the SQLite connection boundary required by that implementation;
- binding or handoff of the operationally required `Persistence:DatabasePath` configuration;
- configuration validation at an appropriate composition boundary;
- lifetime decisions that preserve WP07 connection ownership and WP08--WP10 operation semantics;
- offline proof using the concrete Microsoft DI container that the persistence graph can be constructed and resolved;
- preservation of Release 1.0 DI/composition behavior;
- minimal Infrastructure and, only when necessary, Worker configuration-handoff changes explicitly allowed by the Release 1.1 file manifest.

WP11 does **not** own:

- acquisition-to-persistence Worker execution;
- changes to Worker lifecycle behavior;
- invocation of the persistence use case from Worker;
- provider acquisition changes;
- new persistence semantics;
- new Application contracts;
- physical schema changes;
- persistence/retrieval algorithm changes;
- failure-vocabulary changes;
- retries, resilience policy, caching, batching, scheduling, background-service redesign, or startup orchestration;
- hidden in-memory persistence fallback;
- comprehensive Domain/Application tests assigned to WP13;
- comprehensive Infrastructure/persistence tests assigned to WP14;
- architecture/documentation alignment assigned to WP15;
- Release integration/acceptance assigned to WP16.

## 3. Mandatory Inputs

Before any mutation, read and reconcile completely:

1. `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`
3. WP01--WP10 authoritative prompts and accepted execution evidence available in the repository/current execution context.
4. WP02 persistence technology decision artifacts.
5. WP03 persistence semantics.
6. WP04 Application persistence contracts, especially:
   - `IHistoricalObservationStore`
   - `ObservationPersistenceOutcome`
   - `PersistenceFailure`
   - `ObservationPersistenceResult`
   - `HistoricalObservationResult`
7. WP05 persistence use-case contracts and implementation.
8. WP06 SQLite physical model, schema metadata, record, and mapper.
9. WP07:
   - `SqliteStorageConfiguration`
   - `ISqliteConnectionFactory`
   - `SqliteConnectionFactory`
   - `SqliteSchemaBootstrapper`
   - accepted connection ownership/lifetime semantics.
10. WP08/WP09/WP10 `SqliteHistoricalObservationStore` implementation and accepted write/retrieval/failure semantics.
11. Current Infrastructure DI/composition extension methods or registration conventions.
12. Current Worker composition root, configuration conventions, `Program`/host setup, and configuration files.
13. Release 1.0 DI/composition implementation and documentation.
14. Current `Directory.Packages.props` and all relevant project files.
15. Current permanent tests and architecture tests.
16. GitHub issue #113, predecessor #112, next issue #114, milestone #52, and Project #2 planning state.

Do not rely on memory when repository truth is available.

## 4. Starting-State Gate

Before implementation, prove all of the following.

Repository:

- current branch is `main`;
- local `main` equals `origin/main`;
- ahead/behind is `0/0`;
- staged files are `0`;
- every existing tracked modification/untracked path can be classified as authorized cumulative Release 1.1 work;
- unexpected paths are `0`;
- no existing WP11 implementation conflicts with this authority.

Planning:

- issues #103--#112 are Closed/Done;
- issue #113 is Open/Backlog;
- issue #113 depends exactly on #112 according to the authoritative planning model;
- issue #114 remains Open/Backlog;
- milestone #52 is Open;
- legacy milestones #42 and #43 are Closed/empty;
- active Release 1.2 planning is `0`;
- WP12 has not started.

Implementation:

- WP04 persistence contracts exist unchanged;
- WP05 persistence use case exists unchanged;
- WP06 physical model exists unchanged;
- WP07 connection/configuration/bootstrap boundary exists;
- WP08 write implementation exists;
- WP09 retrieval implementation exists;
- WP10 failure mapping exists;
- `Microsoft.Data.Sqlite` remains Infrastructure-owned at the accepted version;
- no hidden in-memory persistence implementation exists.

If any mandatory state is false, stop before mutation and report the smallest corrective authority required.

Do not silently repair planning or historical governance drift.

## 5. Initial Technical Baseline

Before moving issue #113 to `In Progress`, run the repository's normal baseline gates:

1. restore;
2. format verification;
3. build;
4. all permanent test projects;
5. Architecture.Tests;
6. `eng/verify.ps1`;
7. `git diff --check`;
8. `git diff --cached --check`.

Record:

- build warnings/errors;
- test totals by suite;
- architecture-test count;
- diff-check result.

If the accepted baseline fails for reasons unrelated to WP11, stop and report the blocker. Do not use WP11 to repair unrelated defects.

Only after the starting-state and technical-baseline gates pass may issue #113 move from `Backlog` to `In Progress`.

## 6. Accepted Persistence Composition Inputs

WP11 must preserve the accepted predecessor design rather than recreating it.

The required conceptual graph is:

```text
Application
  IHistoricalObservationStore
           │
           ▼
Infrastructure
  SqliteHistoricalObservationStore
           │
           ▼
  ISqliteConnectionFactory
           │
           ▼
  SqliteConnectionFactory
           │
           ├── SqliteStorageConfiguration
           │
           └── SqliteSchemaBootstrapper / accepted WP06 schema
```

The exact constructor signatures and concrete types must be read from repository truth.

Do not change constructor signatures merely to make registration easier unless an actual correctness blocker is proved and the smallest compatible change remains within WP11 scope.

## 7. Application Persistence Registration

Register the accepted Application persistence contract:

```text
IHistoricalObservationStore
```

to the accepted Infrastructure implementation:

```text
SqliteHistoricalObservationStore
```

Rules:

- exactly one intended production registration;
- no competing production store;
- no in-memory fallback;
- no service-locator access;
- no static mutable singleton;
- no storage-specific type added to Application;
- no Worker-owned implementation;
- no duplicate registration whose behavior depends on registration order.

If the repository already centralizes Infrastructure registrations in an extension method, extend that existing composition model rather than introducing a parallel registration mechanism.

## 8. Persistence Use-Case Registration Reconciliation

Inspect how Application use cases are currently registered.

WP05 introduced the dedicated persistence use case. WP11 may register the accepted Application persistence use-case interface/implementation **only if** that registration belongs to the repository's existing composition convention and is necessary for the real WP12 composition root to resolve it.

Rules:

- do not redesign WP05;
- do not combine acquisition and persistence use cases;
- do not invoke the use case;
- do not create Worker orchestration;
- do not change public contracts;
- do not introduce a generic mediator/container abstraction.

The execution report must state explicitly whether the WP05 use case required a new registration and why.

## 9. SQLite Connection Boundary Registration

Register the accepted WP07 connection boundary required by the store.

At minimum reconcile:

```text
ISqliteConnectionFactory
SqliteConnectionFactory
SqliteStorageConfiguration
```

and any schema-bootstrap collaborator actually required by current constructor structure.

Preserve WP07 ownership:

- each storage operation obtains a fresh open connection;
- the store/operation owns disposal;
- the DI container must not own a live `SqliteConnection`;
- do not register `SqliteConnection` itself as Singleton/Scoped/Transient service unless repository truth proves an explicit accepted design requiring it;
- do not introduce a shared global connection;
- do not hold an open connection for host lifetime.

The DI graph should resolve factories/services, not pre-opened database resources.

## 10. Lifetime Design

Choose lifetimes from actual object state and ownership, not convenience.

Mandatory principles:

- no lifetime may cause a live SQLite connection to be retained across operations;
- stateless registration helpers/factories may use the narrowest existing repository convention compatible with their dependencies;
- the persistence store lifetime must not create hidden state or connection sharing;
- configuration values may be immutable after binding;
- schema bootstrap behavior must remain tied to connection creation as accepted by WP07;
- no database mutation may occur merely because the DI container registers services.

If existing Release 1.0 Infrastructure services establish a clear convention, prefer that convention unless it conflicts with SQLite ownership correctness.

Document the selected lifetime for every new production registration and the reason.

## 11. Configuration Contract

The accepted operational configuration key is:

```text
Persistence:DatabasePath
```

WP11 must make this value available to the Infrastructure connection boundary through the repository's established configuration model.

Rules:

- preserve the exact key unless repository truth shows an already-accepted equivalent;
- do not introduce credentials;
- do not embed a machine-specific path;
- do not embed a developer-specific absolute path;
- do not silently choose a production database path;
- do not derive persistence location from the Twelve Data API key or provider configuration;
- do not overload unrelated configuration sections;
- do not add tuning knobs not required by the accepted design.

The configuration surface must remain minimal and operationally necessary.

## 12. Configuration Validation

Blank or unusable required configuration must fail deterministically.

Reconcile the existing WP07 `SqliteStorageConfiguration` validation behavior and the repository's existing host/configuration validation conventions.

Preferred outcome:

- missing/blank `Persistence:DatabasePath` is rejected before meaningful persistence execution;
- the failure is clear and deterministic;
- no provider call is needed to discover the missing persistence configuration;
- no fallback database is silently selected.

However, WP11 must not begin WP12 Worker execution just to prove this.

Validation may be proved through direct DI/container construction and service resolution using an offline configuration source.

Do not invent a second validation model if WP07 already provides sufficient deterministic validation.

## 13. Service-Resolution Side-Effect Rule

The Release 1.1 execution plan prohibits storage mutation during service resolution unless explicit initialization semantics require it.

For WP11, the default required behavior is:

```text
Build DI container
→ resolve persistence service/factory
→ NO database file creation solely from resolution
→ NO schema creation solely from resolution
→ NO provider/network call
```

Actual database opening/bootstrap remains operation-driven through the accepted WP07 factory behavior.

If current constructor behavior would open/create storage during mere resolution, treat that as a design issue and make the smallest scope-compatible correction that preserves WP07 semantics.

Do not add eager hosted initialization.

## 14. Worker Configuration Handoff Boundary

The Release 1.1 file manifest allows minimal Worker changes **only when required** for configuration handoff/composition.

Authorized Worker-side changes may include only:

- passing the existing host `IConfiguration` into an Infrastructure registration extension;
- adding the minimal `Persistence` configuration section to an existing configuration file if repository conventions require a documented placeholder;
- preserving existing Release 1.0 provider configuration;
- wiring registration calls without invoking persistence behavior.

Prohibited Worker-side changes include:

- acquiring market data and then persisting it;
- resolving and invoking `IPersistHistoricalObservationsUseCase`;
- changing Worker loop/lifecycle;
- adding new execution modes;
- changing scheduling;
- creating database directories/files as Worker startup behavior;
- adding persistence logging flows;
- changing provider calls;
- implementing WP12's target flow.

If Worker changes are not required, do not touch Worker.

## 15. Release 1.0 Composition Preservation

Inspect the existing Release 1.0 DI graph before mutation.

WP11 must preserve:

- `IObservationSource` registration;
- Twelve Data provider ownership in Infrastructure;
- existing provider configuration behavior;
- Worker → Application/Infrastructure composition boundaries;
- existing host construction;
- existing missing-provider-configuration behavior unless a directly required WP11 configuration handoff makes a minimal compatible adjustment necessary.

No Release 1.0 service may be removed, replaced, or accidentally shadowed.

## 16. No Hidden In-Memory Fallback

A missing SQLite path, unavailable database, or persistence resolution failure must **not** cause selection of an in-memory persistence implementation.

Prohibited examples:

```text
if path missing → use in-memory store
if SQLite fails → use list/dictionary store
development environment → bypass SQLite automatically
tests → production registration silently changes storage engine
```

Offline validation may use a temporary SQLite database path explicitly supplied by the test/probe.

## 17. Registration API Design

Prefer a focused Infrastructure composition entry point consistent with repository conventions, for example conceptually:

```text
services.AddInfrastructure(configuration)
```

or an existing equivalent.

Do not create multiple overlapping public registration APIs unless the repository already requires them.

If an existing `AddInfrastructure` method exists, extend it minimally.

If the repository separates provider and persistence registration intentionally, preserve that convention rather than forcing consolidation.

The execution report must identify the exact registration entry point(s) before and after WP11.

## 18. Configuration Binding Strategy

Use the smallest approach compatible with existing repository conventions.

Acceptable patterns may include:

- direct construction of immutable `SqliteStorageConfiguration` from `IConfiguration`;
- options binding if options are already the repository standard and it does not add unnecessary complexity;
- a focused factory delegate when that is the established style.

Do not add a package solely for configuration binding if existing framework references already suffice or direct construction is simpler.

Do not add package dependencies unless strictly necessary and explicitly justified.

Expected package delta is `0`.

## 19. Offline Concrete-DI Proof

The Release 1.1 plan requires concrete Microsoft DI container construction/resolution to be testable offline.

Create a focused temporary validation probe or use an existing appropriate test surface without permanently expanding WP13/WP14 test scope.

At minimum prove:

1. a `ServiceCollection`/real Microsoft DI container can be constructed with the production WP11 registration path;
2. a supplied temporary `Persistence:DatabasePath` is consumed correctly;
3. `IHistoricalObservationStore` resolves to `SqliteHistoricalObservationStore`;
4. the required connection factory graph resolves;
5. any WP05 persistence use-case registration required by production composition resolves;
6. resolution itself does not create the database file or schema;
7. a real persistence operation can obtain the configured SQLite connection through the accepted boundary if needed for proof;
8. the temporary database can be removed afterward;
9. no network/provider call is required;
10. no hidden fallback is selected.

Temporary validation artifacts must be removed before final acceptance.

## 20. Missing-Configuration Proof

Using the real production registration path, prove the behavior when `Persistence:DatabasePath` is absent or blank.

Required properties:

- deterministic failure;
- no fallback;
- no database file;
- no provider call;
- no secret disclosure;
- failure location is consistent with accepted configuration semantics.

Do not require the full Worker runtime for this proof if direct container resolution is sufficient.

If configuration validation occurs when the persistence service/factory is resolved rather than when the container is built, report that precisely. Do not fabricate eager validation.

## 21. Lifetime/Identity Proof

Use focused offline validation to inspect service identity where meaningful.

Prove that selected lifetimes match the intended design.

Examples:

- if the store is transient, separate resolutions produce separate store instances;
- if the connection factory is singleton/stateless, repeated resolutions may share only the factory object, never a live SQLite connection;
- separate persistence operations receive separate SQLite connections;
- disposal of one operation's connection cannot invalidate another service instance.

Do not force a specific lifetime solely to satisfy a test. Validate the lifetime selected from design reasoning.

## 22. Database-Path Fidelity

Prove that an explicitly supplied temporary database path reaches SQLite unchanged except for transformations inherently performed by the accepted connection-string builder.

Do not:

- trim into a different path;
- substitute current directory;
- replace with a default filename;
- normalize to a repository-owned hidden path;
- silently switch to `:memory:`.

The configured path remains operational input, not Domain/Application data.

## 23. Existing Persistence Semantics Regression Protection

WP11 must not alter WP08--WP10 behavior.

At minimum reconcile and, where useful in the focused probe, prove:

- newly accepted observation persistence still succeeds;
- equivalent duplicate remains idempotent;
- conflicting duplicate remains deterministic and non-destructive;
- retrieval remains exact-target;
- retrieval remains ascending by semantic instant;
- timestamp/offset fidelity remains intact;
- decimal fidelity remains intact;
- valid no-row retrieval remains successful empty;
- covered storage failures remain mapped using WP10 behavior.

Do not redesign the store to make DI validation easier.

## 24. Security Requirements

WP11 must not introduce or expose:

- API keys;
- credentials;
- database passwords;
- personal absolute paths in committed production configuration;
- secrets in logs;
- connection-string secrets;
- provider response payloads.

SQLite database path is operational configuration, not a secret, but temporary/local paths used by validation must not be committed as machine-specific production defaults.

Do not print environment secrets during diagnostics.

## 25. Package and Reference Protection

Expected WP11 deltas:

```text
new NuGet packages:       0
new project references:   0
SQLite version changes:   0
```

Do not add a DI/configuration package unless repository compilation proves a missing dependency and the addition is strictly necessary.

If any package/reference change becomes necessary, stop and justify it before broadening scope.

Production dependency graph must remain:

```text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

No cycle is permitted.

## 26. Authorized Production Surface

Per the Release 1.1 file manifest, WP11 production mutations are limited to:

```text
src/AIQuantTradingResearch.Infrastructure/**
```

and, **only when required for configuration handoff/composition**:

```text
src/AIQuantTradingResearch.Worker/**
```

Potential Worker configuration files may be modified only when they are already part of repository configuration conventions and the change is directly required by WP11.

The WP11 governance prompt pair itself is authorized under:

```text
docs/roadmap/release-1.1/prompts/
```

Do not modify Domain or Application production code.

Do not modify permanent tests unless a pre-existing test must be minimally adjusted solely because an accepted composition API signature changes; prefer a temporary focused probe because WP13/WP14 own comprehensive tests.

## 27. Prohibited Mutations

WP11 must not:

- change WP03 semantics;
- change WP04 public persistence contracts;
- redesign WP05 persistence orchestration;
- change WP06 schema/record/mapper semantics;
- redesign WP07 connection ownership/bootstrap;
- change WP08 write semantics;
- change WP09 retrieval semantics;
- broaden WP10 failure vocabulary;
- add in-memory storage;
- add caching;
- add retries;
- add resilience policies;
- add migration frameworks;
- add repository/generic CRUD abstractions;
- add a second persistence store;
- add ORM technology;
- start WP12 Worker persistence execution;
- start WP13/WP14 comprehensive test implementation;
- perform WP15 documentation alignment;
- perform WP16 integration/acceptance;
- stage, commit, push, branch, or create a PR;
- create tags/releases;
- mutate Release 1.2 planning.

## 28. Implementation Sequence

Execute in this order:

1. inspect repository/GitHub starting state;
2. reconcile WP10 completion and WP11 planning identity;
3. run the initial technical baseline;
4. move issue #113 to `In Progress`;
5. inspect existing Release 1.0 DI/configuration conventions;
6. inspect constructors for WP05/WP07/WP08--WP10 services;
7. decide exact production registration graph and lifetimes;
8. implement the minimum Infrastructure registration/configuration composition;
9. make only the minimum Worker configuration-handoff change if actually required;
10. run focused offline concrete-DI validation;
11. validate missing/blank configuration behavior;
12. validate no resolution-time storage mutation;
13. validate lifetime/connection ownership;
14. regression-check accepted persistence/retrieval/failure semantics;
15. remove temporary probe/database artifacts;
16. run formatting/diff checks;
17. run restore/build/tests/architecture/canonical verification;
18. inspect final mutation accounting;
19. post concise WP11 evidence to issue #113;
20. close #113 / move to `Done` only if every acceptance gate passes;
21. leave #114 Open/Backlog;
22. emit the required execution report.

## 29. Whitespace and Line-Ending Policy

Do not normalize unrelated files.

Run:

```text
git diff --check
git diff --cached --check
```

Both must pass.

If Git reports line-ending conversion notices without actual whitespace errors, record them as informational only.

If authorized WP11 files contain whitespace findings caused by WP11 edits, correct only those findings.

Do not create another governance authority merely for ordinary whitespace introduced within the authorized WP11 mutation surface.

Do not rewrite predecessor governance files for formatting.

## 30. Permanent Validation Gates

After implementation, all of the following must pass:

### Restore

```text
dotnet restore AIQuantTradingResearch.slnx --nologo
```

or the repository's canonical equivalent.

### Format verification

Use the repository's established format-verification command.

### Build

Require:

```text
warnings = 0
errors   = 0
```

unless repository baseline explicitly proves an accepted warning state; do not create new warnings.

### Permanent tests

Run every permanent test project.

Expected predecessor baseline from WP10:

```text
Domain.Tests          11/11
Application.Tests     16/16
Infrastructure.Tests  65/65
Architecture.Tests    13/13
Total                 105/105
```

If counts legitimately change due to repository truth, report exact before/after counts and explain why. WP11 itself should normally add no permanent tests.

### Canonical verification

```text
eng/verify.ps1
```

must pass.

### Diff validation

```text
git diff --check
git diff --cached --check
```

must pass.

## 31. Architecture Validation

Prove after WP11:

```text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

Also prove:

- Domain SQLite references: `0`;
- Application SQLite references: `0`;
- Application Microsoft DI composition references introduced by WP11: `0` unless already accepted by repository architecture;
- Worker does not implement persistence storage;
- Infrastructure owns SQLite registration/implementation;
- no production dependency cycle;
- no new project-reference edge.

Architecture.Tests must remain green.

## 32. Composition Acceptance Matrix

The final WP11 report must explicitly record at least:

| Requirement | Required Result |
|---|---|
| WP10 predecessor | PASS |
| Issue #113 lifecycle | Backlog → In Progress → Closed/Done |
| `IHistoricalObservationStore` production registration | PASS |
| Concrete implementation | `SqliteHistoricalObservationStore` |
| Connection factory registration | PASS |
| Configuration key | `Persistence:DatabasePath` |
| Missing/blank path | deterministic rejection |
| Hidden in-memory fallback | NO |
| Database created on mere service resolution | NO |
| Provider/network call during DI proof | NO |
| Operation-owned connections preserved | PASS |
| Shared live SQLite connection | NO |
| Release 1.0 DI preserved | PASS |
| WP05 use-case registration reconciled | PASS / not required, with reason |
| Concrete Microsoft DI offline resolution | PASS |
| Configured path fidelity | PASS |
| WP08 write regression | PASS |
| WP09 retrieval regression | PASS |
| WP10 failure mapping regression | PASS |
| Domain/Application SQLite leakage | 0 |
| New packages | 0 |
| New project references | 0 |
| WP12 execution started | NO |
| Temporary residue | 0 |
| Build warnings/errors | 0/0 |
| Canonical verification | PASS |
| Both diff checks | PASS |

## 33. Git/GitHub Mutation Policy

Authorized GitHub mutation is limited to WP11 lifecycle:

- move issue #113 `Backlog → In Progress` after starting gates pass;
- add concise execution evidence after technical acceptance;
- close issue #113 and reach Project status `Done` only after every WP11 gate passes.

Do not mutate:

- issue #114 or later issues;
- milestone identity/description;
- Project schema;
- labels;
- dependencies;
- Release 1.2 planning.

Repository Git operations prohibited:

- `git add`;
- commit;
- push;
- branch creation;
- PR creation;
- merge;
- rebase/reset/history rewrite;
- tag;
- GitHub Release.

The cumulative Release 1.1 working tree remains intentionally unintegrated until the later authorized integration stage.

## 34. Failure / Blocker Policy

Stop and report instead of improvising if:

- WP10 is not actually Closed/Done;
- issue #113 planning identity/dependency is wrong;
- active Release 1.2 planning reappears;
- baseline validation fails for unrelated reasons;
- accepted constructor/configuration contracts cannot be composed without changing Domain/Application public contracts;
- a new package/project reference appears necessary;
- correct DI requires redesign of WP07 connection ownership;
- the only way to pass is to begin WP12 orchestration;
- hidden fallback appears necessary;
- an unrelated file must be modified;
- unexpected repository paths cannot be classified.

The blocker report must state:

1. exact blocker;
2. evidence;
3. why WP11 authority is insufficient;
4. smallest corrective authority required;
5. confirmation that WP12 was not started.

## 35. Required Execution Report

Return a structured **Release 1.1 WP11 Execution Report** covering at least:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. Existing Release 1.0 Composition Reconciliation
10. WP04/WP05 Contract and Use-Case Reconciliation
11. WP07 Connection-Boundary Reconciliation
12. WP08--WP10 Store Reconciliation
13. Registration Design
14. `IHistoricalObservationStore` Registration
15. Persistence Use-Case Registration Decision
16. Connection Factory Registration
17. Configuration Binding/Handoff
18. `Persistence:DatabasePath` Validation
19. Lifetime Decisions
20. Service-Resolution Side-Effect Evidence
21. Offline Concrete-DI Validation
22. Missing-Configuration Validation
23. Connection Ownership/Disposal Validation
24. Configured Path Fidelity
25. Release 1.0 DI Regression Protection
26. Persistence/Write Regression Protection
27. Retrieval Regression Protection
28. Failure-Mapping Regression Protection
29. Exact Files Added/Modified
30. Package/Reference Delta
31. Test/Probe Delta
32. WP12 Protection
33. Security
34. Whitespace/Diff Evidence
35. Restore/Build Evidence
36. Permanent Test Evidence
37. Canonical Verification
38. Architecture Validation
39. Composition Acceptance Matrix
40. Mutation Accounting
41. Git/GitHub Protection
42. Planning Protection
43. Findings/Blockers
44. Final Repository/GitHub State
45. WP12 Handoff
46. Final Decision
47. Next Authorized Work Package

If implementation is blocked before mutation, still use this report structure and mark implementation-dependent sections `NOT RUN` or `NOT APPLICABLE` truthfully.

## 36. WP12 Handoff Contract

If WP11 succeeds, the handoff to WP12 must be explicit and minimal.

WP12 may rely on:

- a production DI registration path that resolves the persistence use case/store graph;
- `IHistoricalObservationStore → SqliteHistoricalObservationStore`;
- the accepted SQLite connection factory/bootstrap boundary;
- `Persistence:DatabasePath` as the operational database-location input;
- deterministic missing/blank persistence configuration behavior;
- operation-owned SQLite connections;
- accepted WP08 persistence semantics;
- accepted WP09 retrieval semantics;
- accepted WP10 failure mapping.

WP12 remains responsible for the real Worker target flow:

```text
Worker
→ Application
→ IObservationSource
→ Release 1.0 provider boundary
→ normalized observations
→ persistence use case
→ IHistoricalObservationStore
→ SQLite Infrastructure
```

WP11 must not implement that flow.

## 37. Success Terminal

WP11 succeeds only if every mandatory gate passes.

Required terminal:

```text
RELEASE 1.1 WP11 COMPLETE

DEPENDENCY REGISTRATION & CONFIGURATION:
Application persistence contract registration: PASS
Infrastructure SQLite implementation registration: PASS
Connection boundary registration: PASS
Persistence:DatabasePath configuration: PASS
Missing/blank configuration rejection: PASS
Concrete Microsoft DI offline resolution: PASS
Resolution-time database mutation: NO
Hidden in-memory fallback: NO
Operation-owned connections preserved: PASS
Release 1.0 DI regression: PASS
WP08 persistence regression: PASS
WP09 retrieval regression: PASS
WP10 failure-mapping regression: PASS
Domain/Application SQLite leakage: 0
Package/reference delta: 0/0
WP12 started: NO

NEXT AUTHORIZED WORK PACKAGE:
WP12 — Worker Persistent Market-Data Execution
GitHub issue #114
```

Do not emit this terminal unless the evidence supports every statement.

## 38. Final Instruction

Execute **only WP11**.

The purpose of this work package is not to make the Worker persist market data yet. Its purpose is to make the already-accepted persistence slice **correctly composable** through the repository's real DI/configuration model so WP12 can exercise it without redesign.

Prefer the smallest registration/configuration change that preserves all accepted predecessor behavior.

Do not begin WP12.
