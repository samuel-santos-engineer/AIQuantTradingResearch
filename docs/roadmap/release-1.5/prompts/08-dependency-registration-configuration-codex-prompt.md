# Release 1.5 WP08 — Dependency Registration & Configuration

## GitHub Issue
`#175 — Release 1.5 WP08 — Dependency Registration & Configuration`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP08 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Selected experiment:

`simple-return-descriptive-summary-v1`

Identity scheme:

`aiq-experiment-identity-v1`

WP08 registers the accepted Release 1.5 Application experiment graph and introduces only the minimum explicit Worker-side configuration needed to construct a canonical experiment request. It must preserve side-effect-free composition.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `docs/architecture/data/EXPERIMENT_SEMANTICS.md`
- `docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md`
- WP04 experiment model/contracts and identity implementation
- WP05 summary computer
- WP06 validator
- WP07 `ExperimentGenerationUseCase.cs`
- Release 1.4 Application DI registrations
- Release 1.4 FeatureExecutionConfiguration and feature Worker composition patterns
- existing Release 1.2/1.3/1.4 persistence registrations
- current Application/Worker configuration and DI conventions
- WP01–WP07 completion evidence
- this WP08 authority and its five-line companion

Repository truth and accepted authorities take precedence over assumptions.

If composition cannot be established without starting WP09 Worker execution or introducing new persistence/provider behavior, stop and request the smallest corrective authority.

---

## 2. Objective

Implement exactly the manifest-authorized dependency registration and configuration surface for Release 1.5.

WP08 must:

- register the accepted experiment use case;
- register the accepted summary computer;
- register the accepted validator;
- reuse Release 1.4 `IFeatureGenerationUseCase` and its dependency graph;
- define the minimal explicit experiment execution configuration;
- construct the accepted experiment request inputs deterministically;
- preserve code-owned experiment semantics;
- prove DI graph resolution is side-effect-free.

WP08 must not execute the experiment. One-shot Worker execution belongs to WP09.

---

## 3. Expected Starting State

Reconcile rather than assume:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main`;
- expected baseline SHA: `2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- ahead/behind: `0/0`;
- staged paths: `0`;
- tracked modifications: `0`.

Expected lifecycle:

- #168–#174: CLOSED / Done;
- #175 WP08: OPEN / Backlog;
- #176 WP09: OPEN / Backlog;
- #177–#180: OPEN / Backlog;
- milestone #46: OPEN with 6 open / 7 closed;
- Release 1.5 integration branch/PR: none;
- Release 1.6 implementation: none.

Expected technical baseline:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- SQLite schema: v2.

If #174 is not Closed/Done or #176 has started, stop before mutation.

---

## 4. WP08 Lifecycle Start

After starting-state gates pass:

- move only #175 Project #2 Status from Backlog to In Progress.

Read back the state.

If #175 is already In Progress solely because this exact WP08 execution partially started, continue idempotently if no unauthorized mutation occurred.

Do not mutate #176.

#176 must remain OPEN / Backlog throughout WP08.

---

## 5. Mandatory Composition Inventory

Before editing code, inspect the actual accepted registrations and contracts.

Record:

- current `AIQuantTradingResearch.Application.DependencyInjection` registrations;
- current Release 1.4 feature registrations;
- actual interface and implementation types for:
  - `IExperimentGenerationUseCase`
  - `ExperimentGenerationUseCase`
  - `IExperimentSummaryComputer`
  - `SimpleReturnDescriptiveSummaryComputer`
  - WP06 validator interface and implementation
  - `IFeatureGenerationUseCase`
- current lifetimes of related Release 1.4 services;
- current Worker configuration conventions;
- existing dataset/feature execution configuration keys;
- exact experiment request constructor/factory requirements.

Do not invent registration names or lifetimes.

---

## 6. Application DI Registration

Register exactly the Release 1.5 services required by the accepted graph.

Expected logical registrations, subject to actual type names:

- `IExperimentGenerationUseCase → ExperimentGenerationUseCase`
- `IExperimentSummaryComputer → SimpleReturnDescriptiveSummaryComputer`
- accepted experiment-validator interface → `ExperimentGenerationValidator`

Use lifetimes consistent with accepted Release 1.5/Release 1.4 composition semantics.

Preferred lifetime is transient for stateless per-execution Application services unless repository truth clearly establishes another accepted pattern.

Do not duplicate existing Release 1.4 feature registrations.

Do not register concrete implementation types unnecessarily when interfaces already govern the boundary.

---

## 7. Release 1.4 Dependency Reuse

WP07 depends on Release 1.4 feature generation.

WP08 must reuse the already-accepted Release 1.4 graph for:

- `IFeatureGenerationUseCase`;
- feature validator/computer;
- snapshot store;
- any accepted upstream dependencies.

Do not create a second feature stack.

Do not create a parallel snapshot-store registration.

Do not bypass Release 1.4 composition.

---

## 8. Service Lifetime Reconciliation

For each Release 1.5 registration, record and justify the actual lifetime.

The lifetime must not create:

- semantic state retention;
- execution-history retention;
- mutable cross-run state;
- eager provider/network activity;
- eager database creation.

If services are stateless, prefer existing transient patterns.

Do not change predecessor lifetimes unless Release 1.5 cannot compose otherwise and the manifest explicitly authorizes the shared DI refinement.

---

## 9. Experiment Configuration Boundary

Introduce only the minimal Worker-side configuration needed to construct an exact Release 1.5 experiment request.

Use the exact manifest-authorized configuration file path.

The configuration must identify only the upstream semantic evidence required by the accepted request contract.

Expected logical inputs are the exact upstream snapshot/feature-generation selectors already established by Release 1.4, such as:

- snapshot identity;
- snapshot version.

Reconcile actual WP04/WP07 request requirements before choosing keys.

Do not guess or create unnecessary fields.

---

## 10. Configuration Key Design

Configuration keys must be explicit, deterministic, and minimal.

Use an `Experiment:` namespace unless the accepted execution plan/file manifest or repository convention specifies another exact prefix.

Expected logical examples, subject to actual request contract:

- `Experiment:SnapshotIdentity`
- `Experiment:SnapshotVersion`

Do not expose semantic configuration for:

- experiment formula;
- statistic list;
- mean algorithm;
- rounding;
- identity scheme;
- identity domain;
- canonical encoding;
- retry;
- scheduling;
- persistence;
- provider.

The built-in experiment definition remains code-owned.

---

## 11. Built-In Definition Construction

Experiment configuration must always construct exactly:

`simple-return-descriptive-summary-v1`

Do not allow user configuration to select another experiment definition.

Do not add registries/plugins.

Do not expose future experiment IDs via configuration.

---

## 12. Snapshot / Feature Input Reuse

Configuration must preserve exact Release 1.4 input semantics.

If the accepted experiment request is based on exact:

- `DatasetSnapshotIdentity`
- `DatasetVersion`

then parse and construct those exact typed values.

Do not accept looser substitutes.

Do not convert typed semantic identity to unvalidated arbitrary strings after parsing.

Use accepted constructors/factories.

---

## 13. Parsing and Validation

Configuration parsing must be deterministic and fail before experiment execution when inputs are:

- missing;
- malformed;
- semantically incoherent.

Use culture-independent parsing.

Preserve exact typed identity format requirements.

If version parsing has an accepted invariant format, use it.

Do not silently default required semantic values.

Do not normalize malformed values into another valid identity.

---

## 14. Configuration Failure Boundary

WP08 may represent configuration-construction failure in the minimal manner required for WP09 to produce deterministic bounded process exit behavior.

Do not execute the experiment merely to validate configuration.

Do not catch unrelated system defects.

Do not introduce a generalized configuration framework.

If current Worker patterns use a static factory/creator that throws `ArgumentException` or another bounded construction exception, preserve the accepted convention.

---

## 15. Side-Effect-Free DI Resolution

This is a mandatory WP08 acceptance requirement.

Building and resolving the production service graph must not:

- invoke `IExperimentGenerationUseCase`;
- invoke `IFeatureGenerationUseCase`;
- invoke `IExperimentSummaryComputer`;
- query a snapshot;
- create a SQLite database solely from resolution;
- open a database connection eagerly;
- call a provider;
- make HTTP/network requests;
- write experiment/feature evidence;
- mutate catalog/snapshot state.

Resolution is composition only.

---

## 16. Database Creation Proof

Using an isolated disposable path, prove that production DI graph construction and service resolution do not create the configured SQLite database merely as a side effect of resolution.

Do not alter existing Release 1.2/1.4 connection-factory semantics.

If an existing dependency necessarily creates a database during constructor resolution contrary to predecessor accepted evidence, stop and report the contradiction.

---

## 17. Provider / Network Isolation

Use only dummy/non-production credential values where composition requires a key to exist.

No provider call may occur.

No live HTTP.

No credential validation against the network.

No real API key.

The dummy value exists solely to satisfy side-effect-free composition.

---

## 18. Experiment-Not-Executed Proof

A temporary offline probe must demonstrate:

- `IExperimentGenerationUseCase` resolves;
- the experiment execution method is never invoked;
- no Feature Set generation occurs;
- no summary computation occurs;
- no experiment identity/result is produced;
- no persistence occurs.

Remove the probe before final validation.

---

## 19. Production Graph Protection

The production graph must remain:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure.

WP08 does not authorize:

- Application → Infrastructure;
- Application → Worker;
- new project references;
- new packages.

No cycle.

---

## 20. Infrastructure Registration Protection

Infrastructure production registration should remain unchanged unless the accepted manifest explicitly assigns a Release 1.5 change there.

Release 1.5 experiment generation reuses:

- existing snapshot storage;
- existing provider-independent feature-generation boundary.

Do not add:

- experiment repository;
- experiment store;
- experiment catalog;
- feature store/catalog;
- new provider adapter.

Infrastructure production delta expected: 0.

---

## 21. Persistence / Schema Protection

Do not add:

- experiment tables;
- experiment registry/history/cache;
- run-history state;
- scheduler/checkpoints;
- feature persistence expansion;
- schema migration.

SQLite remains version 2.

Schema delta: 0.

---

## 22. WP09 Protection

WP08 must not execute the Worker experiment path.

Do not:

- modify `Program.cs` for experiment-mode invocation unless the manifest explicitly assigns only a non-executing configuration hook to WP08;
- invoke experiment use case from Worker;
- emit experiment results;
- establish process exit policy;
- run a real Worker experiment process as permanent behavior.

WP09 owns one-shot Worker experiment execution.

A temporary composition probe may resolve Worker-related types only when it does not invoke execution.

---

## 23. Release 1.3 / 1.4 Preservation

Preserve Release 1.3 pipeline mode and registrations.

Preserve Release 1.4 feature mode and registrations.

Do not change configuration semantics for existing modes merely to simplify Release 1.5.

Release 1.5 experiment mode must coexist without breaking predecessor composition.

---

## 24. Explicit Deferrals

Do not implement:

- experiment persistence;
- registries/history;
- additional experiments/statistics;
- configurable formulas/statistics;
- provider acquisition;
- scheduling/retries/recovery;
- generalized command dispatch;
- plugins/DAGs;
- notebooks/backtesting/ML;
- Release 1.6 work.

---

## 25. Authorized File Mutation

Use `RELEASE_1.5_FILE_MANIFEST.md` as hard path authority.

Expected logical WP08 paths are:

- `src/AIQuantTradingResearch.Application/DependencyInjection.cs`
- `src/AIQuantTradingResearch.Worker/ExperimentExecutionConfiguration.cs`

Use exact manifest paths.

Before mutation:

1. enumerate exact WP08-authorized paths;
2. verify no WP09 execution path is included;
3. verify no unexpected WP08 implementation already exists.

Expected deltas:

- Application production: 1 shared DI file;
- Worker production/configuration: 1 configuration-only file;
- Domain: 0;
- Infrastructure: 0;
- permanent tests: 0;
- packages/projects/references/schema: 0/0/0/0.

Do not stage or commit.

---

## 26. No Permanent Tests in WP08

Permanent composition/Worker validation belongs to WP11.

Do not add permanent tests.

Use a removable offline composition/configuration probe if needed.

The probe must:

- build the actual production graph;
- use an isolated temporary database path;
- use a dummy API key only if required;
- resolve experiment services;
- validate configuration parsing;
- prove no database/provider/execution side effect;
- be removed before completion.

Permanent test baseline remains 214.

---

## 27. Required Temporary Acceptance Matrix

Prove at minimum:

1. production DI container builds successfully;
2. exactly one effective `IExperimentGenerationUseCase` registration;
3. exactly one effective `IExperimentSummaryComputer` registration;
4. exactly one effective validator registration;
5. accepted service lifetimes are correct;
6. Release 1.4 `IFeatureGenerationUseCase` is reused rather than duplicated;
7. resolving experiment use case does not execute it;
8. resolution does not create the database;
9. resolution causes zero provider/network calls;
10. valid experiment configuration constructs the exact accepted request inputs;
11. missing required configuration fails before execution;
12. malformed identity fails before execution;
13. malformed/incoherent version fails before execution;
14. parsing is culture-independent;
15. built-in experiment definition cannot be changed through configuration;
16. no persistence/schema mutation occurs.

If an item is structurally guaranteed by accepted constructors, report the invariant and still prove composition behavior as practical.

---

## 28. Culture / Timezone Determinism

Where parsing includes version/timestamp-like predecessor values, validate under at least one alternate culture such as `pt-BR`.

Configuration parsing must preserve exact semantic values independent of culture.

Do not convert offsets/timezones unless the accepted predecessor contract requires it.

If WP08 inputs contain no timestamp-like values, report that this check is not applicable.

---

## 29. Technical Validation

Run:

`eng/verify.ps1 -Configuration Release`

Expected final baseline:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- skipped: 0;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS.

Also run:

- `git diff --check`;
- `git diff --cached --check`;
- direct whitespace inspection of WP08 files and relevant untracked governance artifacts.

Require:

- temporary probe removed;
- database/WAL/SHM/journal residue: 0;
- generated residue: 0;
- provider/network calls: 0;
- real credentials: 0.

---

## 30. Architecture Validation

Confirm:

- dependency graph unchanged;
- cycles 0;
- no Application → Infrastructure/Worker;
- no new package/reference;
- no schema change;
- Infrastructure production delta 0.

Architecture.Tests remain 13/13.

Do not add Architecture.Tests.

---

## 31. Repository and Git Protection

Do not:

- stage;
- commit;
- create/switch integration branch;
- push;
- create/merge PR;
- tag;
- release;
- mutate packages/projects/references/schema;
- begin WP09;
- begin Release 1.6.

Git transport mutation budget:

`0`

Repository mutation budget:

only exact manifest-authorized WP08 paths.

---

## 32. Authorized GitHub Mutation Budget

At WP08 start after gates pass:

1. #175 Project Status: Backlog → In Progress.

At successful completion only:

2. post one concise WP08 completion-evidence comment to #175;
3. close #175 as completed;
4. set #175 Project Status to Done.

Do not mutate #176.

Milestone #46 remains OPEN.

---

## 33. Completion Gate

WP08 may close only if:

- #174 is Closed/Done;
- #175 was In Progress during execution;
- #176 remains Open/Backlog;
- exact manifest path accounting passes;
- experiment use case/computer/validator registrations are correct;
- Release 1.4 feature generation is reused;
- accepted service lifetimes are documented;
- minimal deterministic configuration is implemented;
- built-in experiment definition remains code-owned;
- invalid configuration fails before execution;
- DI graph resolution is side-effect-free;
- resolution does not create the database;
- provider/network execution is zero;
- experiment execution is zero during resolution;
- WP09 Worker execution was not implemented;
- Domain/Infrastructure/test deltas are zero;
- package/project/reference/schema deltas are zero;
- SQLite remains v2;
- permanent tests remain 214/214;
- Architecture.Tests remain 13/13;
- warnings/errors 0/0;
- canonical verification passes;
- formatting/Gitleaks/whitespace pass;
- residue 0;
- Release 1.6 work 0.

If any gate fails, do not close #175 or mark Done.

---

## 34. Completion Evidence Comment

On success, post concise evidence to #175 covering:

- exact files modified/added;
- experiment registrations and lifetimes;
- Release 1.4 feature-generation reuse;
- exact configuration keys and typed request construction;
- built-in definition remains code-owned;
- invalid configuration behavior;
- side-effect-free resolution;
- no database creation during resolution;
- no provider/network call;
- no experiment execution during resolution;
- no WP09 Worker execution;
- zero Domain/Infrastructure/test/package/reference/schema delta;
- SQLite v2;
- 214/214 tests and 13/13 Architecture.Tests;
- canonical verification/Gitleaks/whitespace PASS;
- #176 preserved Open/Backlog.

---

## 35. Final Read-Back

After successful closure verify:

- #175: CLOSED / Done;
- #176: OPEN / Backlog;
- #177–#180: unchanged Open / Backlog;
- milestone #46: OPEN;
- milestone counts: 5 open / 8 closed;
- staged paths: 0;
- commits/branches/pushes/PRs: 0;
- Release 1.6 work: 0.

Report cumulative accepted Release 1.5 artifacts accurately.

---

## 36. Stop Conditions

Stop without unauthorized repair if:

- repository/account is wrong;
- #174 is not Closed/Done;
- #176+ started unexpectedly;
- WP08 manifest ownership is ambiguous;
- actual contracts cannot be registered/configured without semantic redesign;
- satisfying WP08 requires WP09 Worker execution;
- side-effect-free resolution cannot be preserved;
- database/provider execution occurs during resolution contrary to accepted predecessor behavior;
- premature later-WP implementation exists;
- Release 1.6 implementation exists;
- architecture/schema baseline drifted;
- canonical verification fails;
- security/whitespace/residue gates fail;
- package/project/reference/schema mutation is required.

Report the smallest corrective authority required.

---

## 37. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. lifecycle reconciliation;
5. exact WP08 manifest paths;
6. existing composition inventory;
7. experiment registrations;
8. service lifetimes;
9. Release 1.4 dependency reuse;
10. configuration keys;
11. typed request construction;
12. parsing/culture determinism;
13. invalid configuration behavior;
14. side-effect-free resolution;
15. database-creation proof;
16. provider/network isolation;
17. experiment-not-executed proof;
18. WP09 protection;
19. predecessor/schema protection;
20. repository delta;
21. temporary acceptance matrix;
22. permanent validation/test counts;
23. architecture/security/whitespace/residue;
24. GitHub lifecycle mutations;
25. final #175/#176/milestone state;
26. findings/blockers;
27. next authorized WP.

---

## 38. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP08 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP09 — One-Shot Worker Experiment Execution — GitHub issue #176`

Do not begin WP09.

If blocked, end:

`RELEASE 1.5 WP08 BLOCKED`

and identify the smallest corrective authority required.
