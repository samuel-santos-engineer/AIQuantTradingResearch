# Release 1.6 WP09 — Storage Validation & Failure Mapping — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP09 — Storage Validation & Failure Mapping — GitHub issue #190**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP09 hardens the completed WP07/WP08 durable Experiment storage boundary by defining and implementing the minimum deterministic validation and SQLite/storage failure classification required by the accepted Release 1.6 failure vocabulary.

The governing principle is:

`known bounded storage condition → existing Release 1.6 failure`
`unknown programming/infrastructure defect → propagate`

WP09 must not redesign persistence, retrieval, schema, identity, DI, Worker behavior, or introduce retry/recovery/repair/fallback semantics.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_SCHEMA_V3.md`
- Release 1.5 Experiment validation/failure authorities and implementation
- current `ExperimentPersistenceContracts.cs`
- current `DurableExperimentUseCase.cs`
- current WP07 schema-v3/persistence implementation
- current WP08 exact retrieval implementation
- existing predecessor SQLite failure-classification conventions
- accepted WP07/WP08 execution evidence
- this WP09 authority and its five-line companion

WP02/WP03 own semantic durable evidence.
WP06 owns physical schema.
WP07 owns acceptance/persistence.
WP08 owns exact retrieval.
WP09 owns only the bounded storage validation/failure mapping needed around those established behaviors.

---

## 3. Starting Gate

Before mutation verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- Release 1.5 authoritative baseline remains `18dfb01bf3503d91415b081b11fcdd7249094373`;
- cumulative Release 1.6 candidate work remains expected and uncommitted/un-staged;
- staged paths: 0;
- unexpected tracked modifications: 0;
- #182–#189: CLOSED / Done;
- #190: OPEN / Backlog;
- #191–#195: OPEN / Backlog;
- milestone #47: OPEN, 6 open / 8 closed;
- Project #2 fields remain correct;
- schema v3 is implemented;
- WP07 acceptance semantics are intact;
- WP08 exact retrieval semantics are intact;
- permanent baseline is 238/238;
- no premature WP10+ implementation exists;
- no Release 1.7 work exists.

Expected Release 1.6 governance/candidate paths are not blockers.

If a mandatory gate fails, stop before moving #190 to In Progress.

---

## 4. Authorized Lifecycle

After all starting gates pass:

1. move #190 `Backlog → In Progress`;
2. implement only WP09;
3. validate;
4. post concise completion evidence to #190;
5. close #190;
6. set #190 `In Progress → Done`.

Required final lifecycle:

- #182–#190: CLOSED / Done;
- #191–#195: OPEN / Backlog;
- milestone #47: OPEN, 5 open / 9 closed.

No other GitHub mutation is authorized.

---

## 5. Manifest Is Binding

Use `RELEASE_1.6_FILE_MANIFEST.md` as exact path authority.

Modify/create only WP09-authorized paths.

Prefer the smallest Infrastructure-owned validation/classification surface around the existing WP07/WP08 store.

Do not introduce:

- generic storage exception frameworks;
- new projects;
- new packages;
- new public failure vocabularies;
- convenience repositories;
- retry policies;
- recovery services;
- telemetry backends.

If required work cannot fit the manifest, stop.

---

## 6. Existing Failure Vocabulary Is Closed

Release 1.6 bounded expected failures remain exactly:

1. `InvalidRequest`
2. `NotFound`
3. `DependencyUnavailable`
4. `InvalidEvidence`
5. `IntegrityConflict`

WP09 must not add a sixth semantic failure.

Storage classification must map only into the applicable existing values.

Do not rename predecessor failures.

Do not alter successful `NewlyAccepted` / `EquivalentExisting` outcomes.

---

## 7. Classification Principle

Classify by semantic meaning, not by raw SQLite error code alone.

A storage exception is bounded only when the repository can deterministically establish that it represents an accepted Release 1.6 condition.

Do not catch all `SqliteException`.

Do not convert arbitrary exceptions to `DependencyUnavailable`.

Do not convert programming defects to `InvalidEvidence`.

Do not hide integrity contradictions.

Unknown/unclassified defects propagate.

---

## 8. DependencyUnavailable

Map to `DependencyUnavailable` only for bounded storage-access conditions where the durable SQLite dependency cannot be used to complete the requested operation.

Examples may include, where deterministically identifiable under existing repository conventions:

- database cannot be opened/accessed;
- required storage path is unavailable;
- connection-level storage dependency is unavailable;
- bounded SQLite busy/locked/unavailable conditions already classified by predecessor infrastructure policy.

Do not classify semantic evidence corruption as dependency unavailability.

Do not add retries.

Do not add fallback databases.

---

## 9. InvalidEvidence

Map to `InvalidEvidence` only when durable evidence itself is malformed, incomplete, non-canonical, or violates accepted evidence invariants without representing a same-identity contradiction.

Applicable examples may include:

- malformed persisted identity/reference representation;
- invalid count/presence relationship;
- malformed canonical decimal representation;
- incomplete aggregate state;
- invalid provenance/reference evidence;
- structurally invalid durable row evidence detected during reconstruction.

Do not silently normalize malformed evidence.

Do not repair the database.

Do not fabricate a valid result.

---

## 10. IntegrityConflict

Preserve/map `IntegrityConflict` for contradictions where accepted identity/evidence integrity is violated.

At minimum preserve:

- same Experiment Result identity with materially contradictory candidate evidence during acceptance;
- persisted Experiment Result identity inconsistent with reconstructed complete evidence where the accepted identity algorithm proves contradiction;
- other exact same-identity contradictions explicitly supported by existing Release 1.6 contracts.

Do not downgrade integrity contradiction to `InvalidEvidence` merely because the contradiction is stored.

Do not overwrite or delete conflicting evidence.

---

## 11. NotFound Preservation

WP08 exact lookup `NotFound` semantics remain unchanged.

NotFound means:

- valid exact identity;
- no matching persisted Experiment Result.

Do not use `NotFound` for:

- malformed rows;
- unavailable database;
- schema corruption;
- identity contradiction;
- invalid request.

No fallback generation is permitted.

---

## 12. InvalidRequest Preservation

Application-owned request validation remains authoritative.

WP09 must not move normal request validation into Infrastructure.

Invalid typed input rejected before storage access should remain `InvalidRequest` according to existing contracts.

Do not broaden Infrastructure validation into duplicate Application validation.

---

## 13. Validation Precedence

Preserve deterministic first-failure behavior.

Where multiple conditions are theoretically present, honor the established boundary order rather than depending on SQLite incidental behavior.

At minimum preserve:

1. Application request validity before storage invocation where already established;
2. bounded dependency availability;
3. durable row/evidence validation;
4. identity/evidence integrity contradiction where complete evidence permits the determination.

Do not reorder existing WP07/WP08 public semantics without explicit authority.

If exact precedence is already frozen elsewhere, that authority wins.

---

## 14. Schema Validation Boundary

Schema v3 remains authoritative.

WP09 may reuse existing schema bootstrap/structural validation but must not redesign or migrate schema.

A structurally invalid current database must not be treated as valid Experiment Result evidence.

Classify only where existing contracts and predecessor schema policy clearly support a bounded result.

Otherwise propagate the established schema/storage exception.

No schema repair is authorized.

---

## 15. SQLite Constraint Handling

Do not map SQLite constraints mechanically.

For example:

- a uniqueness/primary-key race during acceptance must continue to resolve through WP07's semantic equivalence/conflict logic;
- accepted FK/check constraints remain defensive physical invariants;
- a constraint failure caused by a programming defect must not automatically become user-facing `InvalidEvidence`.

Use semantic context plus existing repository conventions.

---

## 16. Unknown Defect Propagation

Unknown defects must remain observable.

Do not catch or normalize:

- `NullReferenceException`;
- `InvalidOperationException` representing programming defects unless already explicitly bounded by contract;
- arbitrary parsing/logic defects not established as stored-evidence invalidity;
- unrelated I/O/runtime defects without accepted classification;
- arbitrary SQLite exceptions outside the bounded classification set.

No broad `catch (Exception)` normalization.

If cleanup requires `finally`, preserve the original exception.

---

## 17. No Retry / Recovery / Repair

WP09 must not implement:

- retry loops;
- exponential backoff;
- circuit breakers;
- checkpointing;
- database repair;
- row deletion;
- row rewrite;
- schema recreation;
- fallback provider access;
- fallback database paths;
- recovery queues.

Those capabilities remain deferred.

Failure mapping is classification, not resilience automation.

---

## 18. Acceptance Path Preservation

WP07 acceptance behavior remains:

- no existing identity → `NewlyAccepted`;
- equivalent existing evidence → `EquivalentExisting`;
- contradictory same-identity evidence → `IntegrityConflict`.

WP09 may harden validation/classification around this path but must not change these semantics.

Exactly-once store invocation from WP05 remains preserved.

---

## 19. Retrieval Path Preservation

WP08 retrieval remains:

`exact ExperimentResultIdentity → Found durable evidence / NotFound`

WP09 may harden invalid stored-evidence and dependency classification around this path but must not:

- add search/list/history;
- regenerate evidence;
- mutate rows;
- change exact lookup key;
- change schema.

---

## 20. Decimal Validation

Preserve WP06/WP07/WP08 canonical decimal representation.

Malformed persisted canonical decimal evidence must map only according to the accepted invalid-evidence boundary when deterministically identified.

Require:

- invariant semantics;
- no SQLite REAL;
- no rounding;
- no culture normalization;
- no silent signed-zero loss where identity fidelity depends on it.

Do not create another decimal parser/format unless the manifest explicitly requires a shared bounded validator.

---

## 21. Provenance / Lineage Validation

Persisted durable evidence must preserve the accepted acyclic provenance references.

WP09 validation may reject malformed/incomplete durable provenance as `InvalidEvidence` where deterministically established.

Do not:

- traverse providers;
- reconstruct source observations;
- persist Feature Values;
- invent missing lineage;
- create generalized graph validation.

Keep validation bounded to the stored Release 1.6 evidence representation.

---

## 22. Database Mutation Boundary

WP09 introduces no new semantic database mutation.

Do not change:

- schema version;
- tables;
- columns;
- indexes;
- migrations;
- existing rows;
- acceptance write semantics.

Any temporary test/probe database must be disposable and removed.

---

## 23. No DI / Configuration / Worker Changes

WP10 owns Dependency Registration & Configuration.
WP11 owns the Durable Experiment Worker.

WP09 must not modify:

- DI registrations;
- Worker configuration;
- Worker routing;
- `Program.cs`;
- Worker output/exit behavior.

Use direct construction in removable probes if needed.

---

## 24. Permanent Test Boundary

WP12 owns comprehensive permanent Release 1.6 persistence tests.

WP09 must not add a new permanent test suite unless the Release 1.6 manifest explicitly assigns a specific WP09 test path.

Default expected permanent count remains 238.

Use removable offline probes for failure-classification proof.

If a permanent test change is required but not manifest-authorized, stop.

---

## 25. Temporary Offline Failure Matrix

Use a removable offline probe to exercise the applicable bounded classification matrix.

At minimum prove, where constructible without violating scope:

- valid acceptance still yields `NewlyAccepted`;
- equivalent acceptance still yields `EquivalentExisting`;
- contradictory same-identity acceptance yields `IntegrityConflict`;
- valid exact missing identity yields `NotFound`;
- unavailable storage maps to `DependencyUnavailable`;
- malformed durable evidence maps to `InvalidEvidence`;
- persisted identity/evidence contradiction maps to `IntegrityConflict` if supported by the existing contract;
- unknown injected/programming defect propagates;
- no retry occurs;
- no provider/network fallback occurs;
- no partial semantic mutation occurs.

Remove the probe completely.

---

## 26. Failure Injection Discipline

Failure probes must be deterministic and offline.

Prefer:

- isolated temporary SQLite paths;
- controlled malformed rows only where constraints permit;
- direct construction/fakes already available in the repository;
- deterministic unavailable-path/connection scenarios.

Do not:

- corrupt user data;
- depend on timing races;
- use real network outages;
- require real credentials;
- alter machine-wide settings.

---

## 27. Side-Effect Proof

For every failure case verify as applicable:

- no fabricated Experiment Result identity;
- no partial row;
- no overwrite;
- no delete;
- no provider call;
- no Feature/Experiment regeneration;
- no retry;
- no schema mutation;
- no residue.

Failure mapping must not change state unexpectedly.

---

## 28. Predecessor Preservation

All existing Releases 1.1–1.5 behavior remains unchanged.

Preserve:

- observation persistence;
- dataset/snapshot persistence/retrieval;
- schema-v3 predecessor compatibility;
- Release 1.3 pipeline;
- Release 1.4 Feature behavior;
- Release 1.5 Experiment behavior;
- WP07 persistence;
- WP08 retrieval.

Do not rewrite predecessor data.

---

## 29. Architecture / Package / Reference Preservation

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
- reference delta: 0.

No new dependency.

---

## 30. Targeted Validation

Run the removable WP09 classification probe first.

Record the exact matrix:

| Condition | Expected |
| --- | --- |
| valid new acceptance | `NewlyAccepted` |
| equivalent existing | `EquivalentExisting` |
| same-identity contradiction | `IntegrityConflict` |
| exact missing identity | `NotFound` |
| bounded storage unavailable | `DependencyUnavailable` |
| malformed stored evidence | `InvalidEvidence` |
| identity/evidence contradiction | `IntegrityConflict` where contractually representable |
| unknown defect | propagates |

If any row cannot be proven because the accepted contract/schema prevents safe construction, explain it rather than weakening production invariants.

---

## 31. Canonical Validation

After removing temporary probes run:

`eng/verify.ps1 -Configuration Release`

Expected permanent counts remain:

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

## 32. Structural Acceptance

Require:

- exact manifest-authorized WP09 production delta only;
- existing failure vocabulary unchanged;
- bounded storage classification implemented;
- unknown defects propagate;
- WP07 acceptance unchanged;
- WP08 retrieval unchanged;
- schema delta: 0;
- DI/configuration delta: 0;
- Worker delta: 0;
- package/project/reference delta: 0/0/0;
- no retry/recovery/repair/fallback;
- no WP10+ implementation;
- no Release 1.7 work.

---

## 33. Mutation Budget

Authorized repository mutations:

- exact WP09 manifest-authorized validation/failure-classification path(s).

Authorized GitHub mutations:

1. #190 Backlog → In Progress;
2. completion evidence comment;
3. close #190;
4. #190 In Progress → Done.

Not authorized:

- staging;
- commit;
- branch;
- push;
- PR;
- tag/release;
- milestone closure;
- #191–#195 mutation;
- schema changes;
- DI/Worker;
- new packages/references;
- broad permanent test expansion;
- Release 1.7 work.

---

## 34. Stop Conditions

Stop with #190 OPEN / In Progress if:

- manifest path authority is ambiguous;
- a new failure value is required;
- deterministic classification cannot be established;
- implementation requires broad `catch (Exception)` normalization;
- WP07 acceptance semantics would change;
- WP08 retrieval semantics would change;
- schema change is required;
- retry/recovery/repair is required;
- DI/Worker work is required;
- package/project/reference change is required;
- permanent test mutation is required but not manifest-authorized;
- canonical verification fails;
- provider/network fallback occurs;
- Release 1.7 work is detected.

Report the smallest corrective authority required.

---

## 35. Completion Evidence

Post concise #190 evidence including:

- exact changed paths;
- bounded classification rules;
- `DependencyUnavailable`;
- `InvalidEvidence`;
- `IntegrityConflict`;
- `NotFound` preservation;
- `InvalidRequest` preservation;
- unknown-defect propagation;
- validation precedence;
- WP07 acceptance preservation;
- WP08 retrieval preservation;
- no retry/recovery/repair/fallback;
- schema remains v3;
- no DI/Worker;
- canonical 238/238;
- next WP10/#191.

---

## 36. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting state;
4. exact changed paths;
5. failure vocabulary preservation;
6. classification principle;
7. DependencyUnavailable mapping;
8. InvalidEvidence mapping;
9. IntegrityConflict mapping;
10. NotFound preservation;
11. InvalidRequest preservation;
12. validation precedence;
13. schema validation boundary;
14. SQLite constraint handling;
15. unknown-defect propagation;
16. no retry/recovery/repair;
17. WP07 acceptance preservation;
18. WP08 retrieval preservation;
19. decimal/provenance validation;
20. failure-injection matrix;
21. side-effect proof;
22. predecessor regression;
23. canonical validation;
24. whitespace/security/residue;
25. architecture/package/reference checks;
26. repository mutation accounting;
27. GitHub lifecycle;
28. findings/blockers;
29. next authorized WP.

---

## 37. Completion Marker

On success end exactly:

`RELEASE 1.6 WP09 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP10 — Dependency Registration & Configuration — GitHub issue #191`

Required final lifecycle:

- #182–#190: CLOSED / Done
- #191–#195: OPEN / Backlog
- milestone #47: OPEN

If blocked end:

`RELEASE 1.6 WP09 BLOCKED`

and identify the smallest corrective authority required.
