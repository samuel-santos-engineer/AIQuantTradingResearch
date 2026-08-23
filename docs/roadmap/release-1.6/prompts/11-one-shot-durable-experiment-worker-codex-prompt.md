# Release 1.6 WP11 — One-Shot Durable Experiment Worker — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP11 — One-Shot Durable Experiment Worker — GitHub issue #192**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP11 establishes the explicit, bounded, one-shot Worker path for Durable Experiment execution.

Following the accepted WP10 composition-scope reconciliation, WP11 owns the complete Worker boundary for this capability:

- Durable Experiment configuration model;
- configuration binding and validation;
- explicit intent detection;
- routing precedence;
- one-shot `IDurableExperimentUseCase` invocation;
- bounded semantic output;
- deterministic exit behavior.

The durable path must reuse the completed WP05/WP07–WP10 stack and must not redesign Application or Infrastructure semantics.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_SCHEMA_V3.md`
- Release 1.5 Experiment Worker/configuration implementation and authorities
- current `ExperimentPersistenceContracts.cs`
- current `DurableExperimentUseCase.cs`
- WP07 persistence implementation
- WP08 exact retrieval implementation
- WP09 failure mapping
- WP10 DI registrations
- `release-1.6-wp10-composition-scope-reconciliation-codex-prompt.md`
- current Worker `Program.cs`
- current Feature/Experiment Worker configuration types and routing conventions
- existing Worker/process validation conventions
- this WP11 authority and its five-line companion

The WP10 reconciliation explicitly transfers all Durable Experiment Worker configuration/binding/intent/routing ownership to WP11 and is authoritative where the original WP10/WP11 division differs.

---

## 3. Starting Gate

Before mutation verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- Release 1.5 authoritative baseline remains `18dfb01bf3503d91415b081b11fcdd7249094373`;
- cumulative Release 1.6 candidate work remains expected and uncommitted/un-staged;
- staged paths: 0;
- unexpected tracked modifications: 0;
- #182–#191: CLOSED / Done;
- #192: OPEN / Backlog;
- #193–#195: OPEN / Backlog;
- milestone #47: OPEN, 4 open / 10 closed;
- Project #2 fields remain correct;
- schema v3 is implemented;
- `IDurableExperimentUseCase` resolves through WP10 DI;
- `IDurableExperimentEvidenceStore` resolves through WP10 DI;
- WP07 acceptance, WP08 retrieval, and WP09 failure semantics are intact;
- Worker remains unchanged by WP10;
- permanent baseline remains 238/238;
- no premature WP12+ implementation exists;
- no Release 1.7 work exists.

Expected Release 1.6 governance/candidate paths are not blockers.

If a mandatory gate fails, stop before moving #192 to In Progress.

---

## 4. Authorized Lifecycle

After all starting gates pass:

1. move #192 `Backlog → In Progress`;
2. implement only WP11;
3. validate;
4. post concise completion evidence to #192;
5. close #192;
6. set #192 `In Progress → Done`.

Required final lifecycle:

- #182–#192: CLOSED / Done;
- #193–#195: OPEN / Backlog;
- milestone #47: OPEN, 3 open / 11 closed.

No other GitHub mutation is authorized.

---

## 5. Manifest and Reconciliation Are Binding

Use `RELEASE_1.6_FILE_MANIFEST.md` as exact path authority, interpreted together with the accepted WP10 composition-scope reconciliation.

WP11 is explicitly authorized to own the Worker configuration artifact(s) that were deferred from WP10.

Modify/create only the resulting WP11-authorized Worker paths.

Expected surface is the minimum required combination of:

- Worker Durable Experiment configuration model;
- Worker one-shot durable execution helper if consistent with existing conventions;
- `Program.cs` routing/composition change.

Do not modify Application/Infrastructure production code unless the manifest explicitly assigns an unavoidable WP11 compatibility path. If required, stop first unless authority is unambiguous.

---

## 6. Durable Experiment Mode

Introduce one explicit Worker mode:

**Durable Experiment**

Its semantic operation is:

`exact Experiment request → generate Release 1.5 Experiment Result → reduce durable evidence → accept into schema-v3 store → report bounded acceptance evidence → exit`

Use the existing `IDurableExperimentUseCase`.

Do not reimplement orchestration in Worker.

---

## 7. Configuration Ownership

Durable Experiment configuration is Worker-owned.

Define/bind only the minimum configuration needed to create the existing durable Experiment request.

Reuse Release 1.5 Experiment semantics:

- exact Dataset Snapshot identity;
- exact Snapshot version;
- code-owned built-in `simple-return-descriptive-summary-v1`.

Do not make the experiment definition free-form.

Do not add provider-specific durable configuration.

Reuse the existing SQLite storage configuration.

---

## 8. Configuration Namespace

Use the exact namespace/key names already frozen by the Release 1.6 plan/manifest if present.

If not explicitly frozen, use the smallest unambiguous Worker namespace consistent with repository conventions, preferably:

- `DurableExperiment:SnapshotIdentity`
- `DurableExperiment:SnapshotVersion`

Do not reuse `Experiment:*` in a way that makes Durable Experiment intent indistinguishable from Release 1.5 in-memory Experiment intent.

Record the exact chosen keys in the execution report.

---

## 9. Explicit Intent Detection

Any explicit Durable Experiment selector must establish Durable Experiment intent.

If either mandatory Durable Experiment selector is supplied, the Worker must treat the request as Durable Experiment intent and validate the complete Durable Experiment configuration.

Partial intent must not fall through to another mode.

Example:

- Durable identity present, version absent → invalid durable configuration;
- Durable version present, identity absent → invalid durable configuration.

Do not silently borrow missing values from `Experiment:*` or `Feature:*`.

---

## 10. Routing Precedence

Preserve deterministic routing with Durable Experiment as the most explicit/newest mode.

Required precedence:

1. explicit Durable Experiment intent;
2. existing Release 1.5 Experiment intent;
3. existing Release 1.4 Feature intent;
4. existing Release 1.3 five-stage pipeline default.

Malformed/partial Durable Experiment intent must fail within Durable Experiment mode.

It must not fall back to Experiment, Feature, or pipeline.

Existing lower-mode precedence remains unchanged when Durable Experiment selectors are absent.

---

## 11. Configuration Validation

Before invoking `IDurableExperimentUseCase`, reject invalid mandatory Durable Experiment configuration.

At minimum validate:

- missing identity;
- malformed identity;
- missing version;
- invalid version;
- incoherent identity/version where the existing typed model establishes coherence;
- partial intent.

Use invariant parsing.

Do not access SQLite merely to validate configuration.

Do not call providers.

Do not invoke the durable use case on invalid configuration.

---

## 12. Invalid Configuration Presentation

Use one bounded, stable Worker-facing error for invalid mandatory Durable Experiment configuration, consistent with predecessor Worker conventions.

Prefer wording parallel to Release 1.5, for example:

`Invalid mandatory durable experiment configuration.`

Do not expose stack traces for governed invalid configuration.

Exit:

`1`

Do not fabricate an Experiment Result identity.

---

## 13. Exactly-Once Invocation

For a valid Durable Experiment request:

- resolve `IDurableExperimentUseCase`;
- invoke it exactly once;
- do not separately invoke `IExperimentGenerationUseCase`;
- do not separately invoke the store;
- do not separately recompute identity/summary.

The Application use case owns orchestration.

Worker owns only request construction, invocation, presentation, and exit.

---

## 14. Successful NewlyAccepted Behavior

For `NewlyAccepted`:

- exit `0`;
- emit bounded semantic evidence sufficient to prove durable acceptance.

At minimum include, using stable invariant representation:

- durable mode/result status;
- Experiment Definition identity/name where available;
- Experiment Result identity;
- Feature Set identity;
- Dataset Snapshot identity;
- Snapshot version;
- count;
- aggregate presence;
- exact mean/minimum/maximum when present;
- explicit aggregate absence for empty result;
- acceptance disposition `NewlyAccepted`.

Do not print Feature Values.

Do not print secrets, database internals, or SQL.

---

## 15. Successful EquivalentExisting Behavior

For `EquivalentExisting`:

- exit `0`;
- emit the same bounded semantic evidence shape as successful new acceptance;
- disposition must explicitly be `EquivalentExisting`;
- Experiment Result identity must be the exact accepted identity.

Do not treat equivalence as an error.

Do not insert a duplicate logical row.

---

## 16. Empty Evidence Presentation

For a successful empty Experiment Result:

- count = 0;
- aggregate presence is explicitly absent;
- mean/minimum/maximum are not fabricated;
- result identity is still emitted;
- provenance-identifying evidence remains bounded and available as authorized.

Use deterministic invariant text.

---

## 17. Non-Empty Evidence Presentation

For non-empty evidence:

- count > 0;
- mean/minimum/maximum are emitted exactly;
- use invariant decimal formatting consistent with existing Experiment Worker presentation;
- no culture-dependent formatting;
- do not round.

---

## 18. Governed Failure Behavior

Map existing bounded durable-use-case failures to deterministic Worker failure presentation and exit `1`.

Preserve the existing Release 1.6 vocabulary:

- `InvalidRequest`
- `NotFound`
- `DependencyUnavailable`
- `InvalidEvidence`
- `IntegrityConflict`

Do not add Worker-specific semantic failure categories.

Do not convert a governed failure into success.

Do not emit a fabricated result identity on failure.

---

## 19. NotFound Behavior

If the exact snapshot/evidence prerequisite results in bounded `NotFound` through the existing Application flow:

- exit `1`;
- emit the bounded failure name;
- no provider fallback;
- no alternate snapshot lookup;
- no durable result identity.

Do not reinterpret it as an empty Experiment Result.

---

## 20. DependencyUnavailable Behavior

For bounded storage dependency unavailability:

- exit `1`;
- emit `DependencyUnavailable`;
- no retry;
- no fallback database;
- no provider fallback;
- no partial success.

Preserve WP09 semantics.

---

## 21. InvalidEvidence / IntegrityConflict

For `InvalidEvidence`:

- exit `1`;
- do not normalize malformed evidence into success.

For `IntegrityConflict`:

- exit `1`;
- do not overwrite/delete/repair existing evidence;
- do not present `EquivalentExisting`.

No result identity should be fabricated beyond evidence legitimately available from the bounded failure contract.

---

## 22. Unknown Defects

Unknown defects remain unhandled and propagate according to existing Worker/process conventions.

Do not add broad:

`catch (Exception)`

normalization around Durable Experiment execution.

Do not translate programming defects to `DependencyUnavailable` or `InvalidEvidence`.

---

## 23. No Retry / Recovery / Repair

WP11 must not implement:

- retry;
- backoff;
- circuit breaker;
- repair;
- overwrite;
- deletion;
- recovery queue;
- alternate database;
- provider fallback.

One-shot means exactly one governed execution attempt.

---

## 24. Provider / Network Boundary

Durable Experiment mode consumes the existing Release 1.5 Experiment-generation path, which derives from already persisted snapshot evidence.

The WP11 acceptance proof must use isolated local schema-v3 SQLite evidence and must not require provider/network acquisition.

Require in validation:

- provider calls: 0;
- network calls: 0;
- real credentials: 0.

If existing composition requires a syntactically present provider key, use a dummy value only and prove no provider call occurs.

---

## 25. Persistence Semantics Preservation

Worker must not implement SQL or storage logic.

Preserve WP07:

- `NewlyAccepted`;
- `EquivalentExisting`;
- `IntegrityConflict`;
- schema v3;
- exact Experiment Result identity;
- no duplicate logical row.

Do not call the store directly from Worker if `IDurableExperimentUseCase` already owns it.

---

## 26. Retrieval Semantics Preservation

WP08 exact retrieval remains unchanged.

WP11 does not need to expose a separate retrieval CLI mode unless explicitly authorized by the manifest.

Do not add durable search/history/list/read commands.

The purpose of WP11 is one-shot durable Experiment execution/acceptance.

---

## 27. Existing Experiment Mode Preservation

When no Durable Experiment selectors are present and Release 1.5 `Experiment:*` selectors are present:

- existing Experiment mode behavior remains unchanged;
- it remains in-memory/non-durable unless its established semantics say otherwise;
- it must not persist Experiment Result evidence implicitly.

Durability must remain explicit.

---

## 28. Existing Feature Mode Preservation

When Durable Experiment and Experiment selectors are absent but Feature selectors are present:

- existing Release 1.4 Feature mode runs unchanged.

Do not persist Experiment evidence.

---

## 29. Existing Pipeline Preservation

When Durable Experiment, Experiment, and Feature selectors are absent:

- existing Release 1.3 five-stage pipeline runs unchanged.

Do not make Durable Experiment the default.

---

## 30. Conflicting Selectors

If Durable Experiment selectors coexist with lower-priority Experiment/Feature selectors:

- Durable Experiment intent wins by routing precedence;
- only Durable Experiment mandatory configuration determines whether that mode is valid;
- lower-priority selectors must not cause a second execution;
- no fallback occurs if Durable Experiment configuration is malformed.

Prove this in the removable process probe.

---

## 31. One-Shot Process Discipline

For one valid Durable Experiment invocation:

- one Worker process;
- one durable use-case invocation;
- one Release 1.5 Experiment-generation invocation through the use case;
- one acceptance attempt;
- one final success/failure exit.

No loop.

No scheduler.

No background service.

No repeated polling.

---

## 32. Temporary Offline Process Probe

Use a removable process-level probe against the Release Worker, preferably with `--no-build`, isolated temporary schema-v3 SQLite files, dummy credentials if required, output capture, timeout, and deterministic cleanup.

At minimum prove:

1. valid non-empty Durable Experiment → success / exit `0`;
2. disposition `NewlyAccepted`;
3. exact Experiment Result identity emitted;
4. count and exact aggregates emitted;
5. second equivalent process → success / exit `0`;
6. disposition `EquivalentExisting`;
7. same Experiment Result identity;
8. row count remains one for that identity;
9. empty durable Experiment → success / exit `0`, count zero, aggregates absent;
10. malformed Durable Experiment identity → exit `1`, no durable invocation/result identity;
11. partial Durable Experiment intent → exit `1`;
12. partial durable intent plus valid lower-mode selectors → exit `1`, no fallback;
13. exact NotFound prerequisite → exit `1`, bounded failure, no result identity;
14. unavailable storage → exit `1`, `DependencyUnavailable`;
15. lower Release 1.5 Experiment mode remains unchanged when Durable selectors are absent;
16. Feature routing remains unchanged when higher selectors are absent;
17. pipeline routing remains unchanged when all selectors are absent;
18. no provider/network activity;
19. no real credentials;
20. no temporary database/WAL/SHM/journal/probe residue.

Where practical prove an `IntegrityConflict` process case without violating storage invariants; otherwise rely on lower-layer permanent/temporary evidence and state why.

Remove all probe artifacts.

---

## 33. Restart / Durability Proof

The second equivalent Worker process must operate against the committed durable evidence from the first process.

This proves:

- process restart;
- persisted row survival;
- semantic equivalence recognition;
- `EquivalentExisting`;
- stable Experiment Result identity.

Do not satisfy this with a single-process in-memory reuse.

---

## 34. Output Stability

Worker output must be:

- bounded;
- deterministic;
- invariant-culture;
- semantic rather than storage-internal;
- free of secrets;
- suitable for later permanent process tests in WP12.

Do not emit timestamps, random IDs, connection strings, SQL, stack traces for governed failures, or environment-dependent noise as required semantic evidence.

Existing predecessor logging may remain unchanged.

---

## 35. Exit Codes

Required Durable Experiment exit behavior:

- valid `NewlyAccepted` → `0`;
- valid `EquivalentExisting` → `0`;
- invalid mandatory configuration → `1`;
- bounded use-case failure → `1`.

Unknown unhandled defects follow existing process/runtime behavior and must not be normalized merely to force `1`.

---

## 36. Schema Boundary

Require:

- schema remains v3;
- no schema migration beyond existing v3 bootstrap;
- no new table/column/index;
- no Feature Set persistence;
- no registry/history.

WP11 may cause normal existing v3 bootstrap by actually executing against a fresh configured database, but must not modify bootstrap logic.

---

## 37. DI Boundary

Use WP10 registrations.

Do not duplicate service registrations in `Program.cs`.

Do not manually instantiate Application/Infrastructure implementations when the production graph already provides them.

The Worker should resolve the durable use-case seam.

---

## 38. Permanent Test Boundary

WP12 owns permanent Application & Infrastructure persistence tests/process validation.

WP11 must not add a new permanent test suite unless explicitly manifest-authorized.

Expected permanent count remains 238.

Use removable process probes only.

If an existing permanent test requires an unavoidable routing expectation update and the manifest does not authorize it, stop and request the smallest corrective authority.

---

## 39. Architecture / Package / Reference Preservation

Production graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Require:

- cycles: 0;
- unexpected edges: 0;
- package delta: 0;
- project delta: 0;
- reference delta: 0;
- solution project count unchanged.

---

## 40. Canonical Validation

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

## 41. Structural Acceptance

Require:

- exact manifest/reconciliation-authorized WP11 Worker paths only;
- Durable Experiment configuration and binding implemented;
- explicit intent detection;
- precedence Durable Experiment → Experiment → Feature → pipeline;
- partial/malformed durable intent cannot fall back;
- exactly-once `IDurableExperimentUseCase` invocation;
- deterministic success/failure output;
- `NewlyAccepted` and `EquivalentExisting` process behavior;
- restart-safe equivalent second process;
- schema v3 unchanged;
- WP07/WP08/WP09 semantics unchanged;
- no provider/network;
- no permanent test expansion unless explicitly authorized;
- no package/project/reference delta;
- no WP12+ implementation;
- no Release 1.7 work.

---

## 42. Mutation Budget

Authorized repository mutations:

- exact WP11 manifest-authorized Worker paths, including the Durable Experiment configuration ownership transferred from WP10 by the accepted reconciliation.

Authorized GitHub mutations:

1. #192 Backlog → In Progress;
2. completion evidence comment;
3. close #192;
4. #192 In Progress → Done.

Not authorized:

- staging;
- commit;
- branch;
- push;
- PR;
- tag/release;
- milestone closure;
- #193–#195 mutation;
- Application/Infrastructure semantic redesign;
- schema change;
- packages/references;
- Release 1.7 work.

---

## 43. Stop Conditions

Stop with #192 OPEN / In Progress if:

- manifest/reconciliation path ownership is ambiguous;
- Worker execution requires Application/Infrastructure semantic change;
- routing cannot preserve predecessor modes;
- Durable configuration cannot be distinguished from lower-mode intent;
- a new failure value is required;
- schema change is required;
- provider/network fallback is required;
- package/project/reference change is required;
- permanent test changes are required but not authorized;
- canonical verification fails;
- Release 1.7 work is detected.

Report the smallest corrective authority required.

---

## 44. Completion Evidence

Post concise #192 evidence including:

- exact changed paths;
- Durable Experiment configuration keys;
- configuration validation;
- routing precedence;
- partial-intent no-fallback behavior;
- exactly-once durable invocation;
- `NewlyAccepted`;
- `EquivalentExisting`;
- stable result identity across processes;
- empty/non-empty presentation;
- bounded failures/exit codes;
- predecessor Experiment/Feature/pipeline preservation;
- schema remains v3;
- no provider/network;
- no DI duplication;
- canonical 238/238;
- next WP12/#193.

---

## 45. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting state;
4. exact changed paths;
5. configuration model/keys;
6. explicit intent;
7. routing precedence;
8. configuration validation;
9. exactly-once invocation;
10. NewlyAccepted behavior;
11. EquivalentExisting behavior;
12. empty/non-empty evidence;
13. governed failure behavior;
14. unknown-defect propagation;
15. no retry/fallback;
16. persistence/retrieval/failure semantic preservation;
17. Experiment mode preservation;
18. Feature mode preservation;
19. pipeline preservation;
20. conflicting-selector behavior;
21. process-probe evidence;
22. restart/durability evidence;
23. output stability;
24. exit codes;
25. provider/network isolation;
26. schema/DI preservation;
27. architecture/package/reference preservation;
28. canonical validation;
29. whitespace/security/residue;
30. repository mutation accounting;
31. GitHub lifecycle;
32. findings/blockers;
33. next authorized WP.

---

## 46. Completion Marker

On success end exactly:

`RELEASE 1.6 WP11 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP12 — Application & Infrastructure Persistence Tests — GitHub issue #193`

Required final lifecycle:

- #182–#192: CLOSED / Done
- #193–#195: OPEN / Backlog
- milestone #47: OPEN

If blocked end:

`RELEASE 1.6 WP11 BLOCKED`

and identify the smallest corrective authority required.
