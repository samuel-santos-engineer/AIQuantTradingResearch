# Release 1.6 WP10 — Dependency Registration & Configuration — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP10 — Dependency Registration & Configuration — GitHub issue #191**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP10 composes the already-implemented Release 1.6 durable Experiment capability. It registers the existing Application and Infrastructure durable-evidence services exactly once with bounded lifetimes and establishes only the Worker-owned configuration model required by the later WP11 one-shot Durable Experiment Worker.

WP10 is composition/configuration work only.

It must not execute the durable experiment path, change Worker routing, persist/retrieve evidence as a side effect of DI resolution, alter schema v3, or redesign WP05/WP07/WP08/WP09 semantics.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_SCHEMA_V3.md`
- current `ExperimentPersistenceContracts.cs`
- current `DurableExperimentUseCase.cs`
- current WP07 persistence implementation
- current WP08 exact retrieval implementation
- current WP09 storage validation/failure mapping
- existing Application and Infrastructure `DependencyInjection.cs`
- existing Release 1.5 Experiment DI/configuration implementation
- existing Feature and pipeline composition conventions
- accepted WP08/WP09 execution evidence
- this WP10 authority and its five-line companion

Earlier WPs own semantics and storage behavior.
WP10 owns only registration and configuration composition.
WP11 owns execution/routing.

---

## 3. Starting Gate

Before mutation verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- Release 1.5 authoritative baseline remains `18dfb01bf3503d91415b081b11fcdd7249094373`;
- cumulative Release 1.6 work is expected and remains uncommitted/un-staged;
- staged paths: 0;
- unexpected tracked modifications: 0;
- #182–#190: CLOSED / Done;
- #191: OPEN / Backlog;
- #192–#195: OPEN / Backlog;
- milestone #47: OPEN, 5 open / 9 closed;
- Project #2 fields remain correct;
- schema v3 is implemented;
- WP05 durable orchestration exists;
- WP07 acceptance exists;
- WP08 retrieval exists;
- WP09 failure mapping is preserved;
- permanent baseline is 238/238;
- no premature WP11+ implementation exists;
- no Release 1.7 work exists.

Expected Release 1.6 governance/candidate paths are not blockers.

If a mandatory gate fails, stop before moving #191 to In Progress.

---

## 4. Authorized Lifecycle

After all starting gates pass:

1. move #191 `Backlog → In Progress`;
2. implement only WP10;
3. validate;
4. post concise completion evidence to #191;
5. close #191;
6. set #191 `In Progress → Done`.

Required final lifecycle:

- #182–#191: CLOSED / Done;
- #192–#195: OPEN / Backlog;
- milestone #47: OPEN, 4 open / 10 closed.

No other GitHub mutation is authorized.

---

## 5. Manifest Is Binding

Use `RELEASE_1.6_FILE_MANIFEST.md` as exact path authority.

Modify/create only WP10-authorized paths.

Expected categories are:

- existing Application DI registration surface if required;
- existing Infrastructure DI registration surface if required;
- bounded Worker-owned Durable Experiment configuration model if manifest-authorized.

Do not modify `Program.cs` unless the manifest explicitly assigns a WP10 configuration-only change and it does not alter routing/execution. Prefer no `Program.cs` change.

If composition cannot fit the manifest-authorized paths, stop.

---

## 6. Application Registration

Register the existing durable Experiment Application use case exactly once.

Expected semantic seam:

- durable Experiment use-case interface → existing `DurableExperimentUseCase`.

Reuse the actual contract/type names already implemented; do not invent aliases.

Use the same lifetime discipline as analogous Application use cases unless an authority explicitly requires otherwise.

Do not duplicate existing Release 1.5 Experiment registrations.

---

## 7. Infrastructure Store Registration

Register the existing schema-v3 durable Experiment evidence store exactly once against its existing storage-independent Application interface.

Expected semantic seam:

- `IDurableExperimentEvidenceStore` → existing SQLite durable Experiment store implementation.

Reuse existing SQLite configuration/path composition.

Do not create a second database abstraction or alternate store.

Do not introduce provider coupling.

---

## 8. Registration Cardinality

For every WP10-owned durable registration require:

- exactly one descriptor;
- exactly one intended implementation;
- no duplicate `TryAdd`/`Add*` combination that creates multiple resolutions;
- no competing registration;
- no decorator;
- no hidden service locator.

Existing predecessor registrations must remain singular.

---

## 9. Lifetime Semantics

Choose lifetimes from existing repository conventions and object ownership.

The intended default is transient for stateless Application orchestration/validation/computation services and the existing established lifetime for SQLite store infrastructure.

Do not make mutable storage connections singleton.

Do not introduce scoped semantics unless the repository already has an explicit scope boundary requiring it.

The final report must state exact lifetimes.

---

## 10. Side-Effect-Free Composition

Building and validating the DI graph must not:

- open/create a database merely because the service is registered;
- migrate schema;
- accept Experiment Result evidence;
- retrieve Experiment Result evidence;
- execute Feature generation;
- execute Experiment generation;
- invoke providers;
- perform network I/O;
- require real credentials.

Construction/resolution must remain side-effect-free except for ordinary in-memory object creation.

If an existing infrastructure constructor violates this, stop rather than hiding the side effect.

---

## 11. Predecessor Composition Preservation

Preserve all existing registrations and behavior for:

- Release 1.3 pipeline;
- Release 1.4 Feature generation;
- Release 1.5 Experiment generation;
- SQLite observation/dataset/snapshot persistence.

Do not duplicate the Feature generation stack.

Do not replace the Release 1.5 `IExperimentGenerationUseCase`.

The durable use case composes it; it does not supersede it.

---

## 12. Durable Worker Configuration Boundary

WP10 may define/bind only the configuration necessary for WP11 to select and execute the explicit durable Experiment path.

Configuration remains Worker-owned.

Use the Release 1.6 definition/plan/manifest as exact authority for key names.

Do not invent configuration keys if those authorities already specify them.

If key names are not frozen, choose the smallest coherent `DurableExperiment:*` or otherwise plan-authorized namespace consistent with existing Worker conventions and document the exact decision.

---

## 13. Configuration Semantics

The Durable Experiment configuration must identify exactly the inputs needed by the existing durable orchestration path.

Reuse the accepted Release 1.5 Experiment request semantics rather than creating a second semantic definition.

Where the durable use case requires the exact snapshot identity/version used to generate the Experiment Result, preserve:

- exact typed snapshot identity;
- exact version;
- code-owned built-in experiment definition where already frozen.

Do not accept provider configuration as part of durable mode.

---

## 14. Explicit Intent

Configuration must support an explicit Durable Experiment execution intent for WP11.

Partial durable intent must be distinguishable from:

- ordinary Release 1.5 Experiment mode;
- Release 1.4 Feature mode;
- Release 1.3 pipeline mode.

WP10 defines/binds the configuration model only.

WP11 owns routing precedence and process behavior.

Do not alter routing in WP10.

---

## 15. Configuration Validation

Reject malformed mandatory Durable Experiment configuration before execution.

At minimum, as applicable to the accepted configuration shape:

- missing mandatory value;
- malformed identity;
- invalid/non-positive/incoherent version;
- partial durable intent.

Use invariant parsing.

Do not query SQLite to validate configuration.

Do not query providers.

Do not generate evidence.

---

## 16. Configuration Coherence

Where multiple fields encode the same semantic identity/version relationship, reject incoherent combinations deterministically.

Do not silently normalize or substitute another mode's configuration.

Do not fall back from malformed Durable Experiment intent to Release 1.5 Experiment, Feature, or pipeline behavior.

The actual fallback-prevention routing is WP11; WP10 must make the configuration state expressive enough to enforce it.

---

## 17. Code-Owned Experiment Definition

Preserve the Release 1.5 built-in:

`simple-return-descriptive-summary-v1`

If the existing Experiment configuration keeps this definition code-owned, Durable Experiment configuration must do the same.

Do not make the experiment name a free-form user configuration merely for WP10.

Do not introduce additional experiment definitions.

---

## 18. Storage Configuration Reuse

Reuse the existing SQLite database/path configuration.

Do not introduce a separate durable-experiment database.

Do not duplicate connection-string/path settings.

The same schema-v3 database owns predecessor and Experiment Result evidence.

No new storage configuration hierarchy unless explicitly required by the manifest.

---

## 19. Failure Semantics Preservation

WP09's five-value vocabulary remains unchanged:

- `InvalidRequest`
- `NotFound`
- `DependencyUnavailable`
- `InvalidEvidence`
- `IntegrityConflict`

WP10 must not add failure values or storage exception handling.

Configuration parsing/validation must remain at the Worker composition boundary and must not mutate the Application persistence vocabulary.

---

## 20. No Schema / Persistence Changes

WP10 must not change:

- `PRAGMA user_version = 3`;
- `experiment_results`;
- migrations;
- SQL;
- persistence acceptance;
- retrieval;
- row mapping;
- decimal representation;
- provenance representation.

If DI composition exposes a defect requiring storage changes, stop.

---

## 21. No Worker Execution

WP11 owns:

- routing precedence;
- one-shot Durable Experiment invocation;
- output;
- exit code;
- process-level behavior.

WP10 must not:

- resolve and invoke the durable use case from `Program.cs`;
- execute a request;
- print durable Experiment evidence;
- change process exit codes;
- change existing mode precedence.

---

## 22. No Provider / Network Activity

Composition validation must be offline.

Require:

- provider calls: 0;
- network calls: 0;
- real credentials: 0.

A dummy provider key may be used only if an existing options object requires a syntactically present value to build the graph, and resolution still must not call the provider.

Prefer not to require it.

---

## 23. Permanent Test Boundary

WP12 owns comprehensive Release 1.6 persistence/composition tests.

WP10 must not add a new permanent test suite unless the manifest explicitly authorizes a WP10 permanent test path.

Expected permanent count remains 238.

Use a removable offline concrete-DI/configuration probe.

If existing permanent tests require unavoidable expectation updates due solely to authorized registration/configuration and the manifest does not authorize them, stop.

---

## 24. Temporary Concrete-DI Probe

Create a removable offline probe if needed and prove:

1. durable Application use case registration exists exactly once;
2. durable evidence store registration exists exactly once;
3. exact lifetimes are correct;
4. predecessor registrations remain singular;
5. `BuildServiceProvider` validation succeeds under representative valid configuration;
6. durable use case resolves through the real graph;
7. store resolves through the real graph;
8. resolution performs no durable acceptance/retrieval;
9. no database/schema side effect occurs merely from graph construction/resolution;
10. no provider/network activity occurs;
11. valid Durable Experiment configuration binds;
12. missing mandatory configuration is rejected;
13. malformed identity is rejected;
14. invalid/incoherent version is rejected;
15. partial durable intent is represented/rejected according to the configuration contract;
16. parsing is culture-independent.

Remove the probe completely.

---

## 25. Database Side-Effect Check

Before and after the DI probe verify that graph construction/resolution does not create:

- SQLite database files;
- WAL files;
- SHM files;
- journal files;
- Experiment Result rows.

If a temporary existing SQLite path must be supplied, ensure no file is created until an actual store operation, which WP10 must not invoke.

---

## 26. Configuration Culture Independence

Run representative configuration parsing under at least one non-default culture where practical.

Identity/version parsing must remain invariant.

Do not depend on current machine locale.

---

## 27. Architecture Preservation

Production graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Require:

- unexpected edges: 0;
- cycles: 0.

Worker may compose both Application and Infrastructure as already established.

Do not add Infrastructure → Worker or Application → Infrastructure.

---

## 28. Package / Project / Reference Preservation

Require:

- package delta: 0;
- project delta: 0;
- project-reference delta: 0;
- solution project count unchanged.

Use existing Microsoft.Extensions.DependencyInjection/configuration capabilities already present.

No new package for options validation.

---

## 29. Predecessor Regression

Preserve:

- Release 1.3 pipeline DI;
- Release 1.4 Feature DI/configuration;
- Release 1.5 Experiment DI/configuration;
- WP07 acceptance;
- WP08 retrieval;
- WP09 failure mapping.

No mode is removed or replaced.

---

## 30. Canonical Validation

After removing temporary probes run:

`eng/verify.ps1 -Configuration Release`

Expected permanent counts:

- Domain.Tests: 11/11
- Application.Tests: 102/102
- Infrastructure.Tests: 112/112
- Architecture.Tests: 13/13
- Total: 238/238
- Skipped: 0

Require:

- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- direct expected-untracked whitespace/final-newline checks: PASS;
- staged paths: 0;
- database/WAL/SHM/journal/probe residue: 0;
- provider/network activity: 0;
- real credentials: 0;
- schema remains v3.

---

## 31. Structural Acceptance

Require:

- exact manifest-authorized WP10 paths only;
- durable Application registration exactly once;
- durable Infrastructure store registration exactly once;
- correct lifetimes;
- valid real graph resolution;
- bounded Worker-owned Durable Experiment configuration;
- malformed/partial configuration rejected before execution;
- no DI-resolution side effects;
- no schema/persistence/retrieval changes;
- no Worker routing/execution;
- no permanent test expansion unless manifest-authorized;
- no package/project/reference delta;
- no WP11+ implementation;
- no Release 1.7 work.

---

## 32. Mutation Budget

Authorized repository mutations:

- exact WP10 manifest-authorized DI/configuration paths only.

Authorized GitHub mutations:

1. #191 Backlog → In Progress;
2. completion evidence comment;
3. close #191;
4. #191 In Progress → Done.

Not authorized:

- staging;
- commit;
- branch;
- push;
- PR;
- tag/release;
- milestone closure;
- #192–#195 mutation;
- schema/storage behavior changes;
- Worker execution/routing;
- packages/references;
- Release 1.7 work.

---

## 33. Stop Conditions

Stop with #191 OPEN / In Progress if:

- manifest path authority is ambiguous;
- required service cannot resolve without constructor side effects;
- registration requires a new package/reference;
- configuration semantics contradict existing Release 1.5/1.6 authorities;
- storage/schema behavior must change;
- Worker routing/execution must change;
- permanent test changes are required but not authorized;
- canonical verification fails;
- provider/network activity occurs;
- Release 1.7 work is detected.

Report the smallest corrective authority required.

---

## 34. Completion Evidence

Post concise #191 evidence including:

- exact changed paths;
- exact registrations and lifetimes;
- registration cardinality;
- predecessor DI preservation;
- durable configuration keys/shape;
- valid/missing/malformed/incoherent/partial configuration behavior;
- invariant parsing;
- real graph resolution;
- side-effect-free DI proof;
- schema remains v3;
- no Worker execution/routing;
- no provider/network;
- canonical 238/238;
- next WP11/#192.

---

## 35. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting state;
4. exact changed paths;
5. Application registration;
6. Infrastructure registration;
7. cardinality/lifetimes;
8. predecessor composition preservation;
9. Durable Experiment configuration shape;
10. explicit-intent semantics;
11. validation/coherence;
12. culture independence;
13. storage configuration reuse;
14. failure-model preservation;
15. side-effect-free DI proof;
16. temporary probe evidence;
17. database-residue proof;
18. no Worker execution;
19. provider/network isolation;
20. architecture/package/reference preservation;
21. predecessor regression;
22. canonical validation;
23. whitespace/security/residue;
24. repository mutation accounting;
25. GitHub lifecycle;
26. findings/blockers;
27. next authorized WP.

---

## 36. Completion Marker

On success end exactly:

`RELEASE 1.6 WP10 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP11 — One-Shot Durable Experiment Worker — GitHub issue #192`

Required final lifecycle:

- #182–#191: CLOSED / Done
- #192–#195: OPEN / Backlog
- milestone #47: OPEN

If blocked end:

`RELEASE 1.6 WP10 BLOCKED`

and identify the smallest corrective authority required.
