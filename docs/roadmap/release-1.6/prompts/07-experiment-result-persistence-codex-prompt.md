# Release 1.6 WP07 — Experiment Result Persistence — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP07 — Experiment Result Persistence — GitHub issue #188**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP07 implements the SQLite schema-v3 migration/bootstrap and the Infrastructure-owned Experiment Result acceptance/persistence behavior required by the accepted Release 1.6 authorities.

The accepted successful outcomes are:

- `NewlyAccepted`
- `EquivalentExisting`

Contradictory same-identity durable evidence must produce:

- `IntegrityConflict`

WP07 must preserve exact semantic evidence, atomicity, schema migration safety, predecessor data, and the unchanged production dependency graph.

WP07 must not implement the full WP08 exact retrieval boundary, WP09 storage-validation/failure-mapping layer, DI, Worker behavior, or permanent Release 1.6 tests beyond the narrowly authorized schema-version fixture adjustment explicitly required by WP06.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_SCHEMA_V3.md`
- accepted WP01–WP06 execution evidence
- current WP04 `ExperimentPersistenceContracts.cs`
- current WP05 `DurableExperimentUseCase.cs`
- current Releases 1.1–1.5 SQLite schema/migration/persistence code and tests
- current schema-version fixture that still treats v3 as unsupported
- this WP07 authority and its five-line companion

Treat WP02/WP03 as semantic authority and WP06 as physical-model authority.

Do not reinterpret the accepted schema-v3 design during implementation.

---

## 3. Starting Gate

Before mutation verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main == 18dfb01bf3503d91415b081b11fcdd7249094373`;
- ahead/behind: `0/0`;
- staged paths: 0;
- unexpected tracked modifications: 0;
- #182–#187: CLOSED / Done;
- #188: OPEN / Backlog;
- #189–#195: OPEN / Backlog;
- milestone #47: OPEN, 8 open / 6 closed;
- Project #2 fields remain correct;
- predecessor Release restoration remains 89/89 exact;
- WP06 physical-model document exists and is coherent;
- implemented schema is still v2;
- no premature WP08+ implementation exists;
- no Release 1.7 work exists.

Expected untracked Release 1.6 candidate/governance artifacts are not blockers.

If a mandatory gate fails, stop before moving #188 to In Progress.

---

## 4. Authorized Lifecycle

After all starting gates pass:

1. move #188 `Backlog → In Progress`;
2. implement only WP07;
3. validate;
4. post concise completion evidence to #188;
5. close #188;
6. set #188 `In Progress → Done`.

Required final lifecycle:

- #182–#188: CLOSED / Done;
- #189–#195: OPEN / Backlog;
- milestone #47: OPEN, 7 open / 7 closed.

No other GitHub lifecycle mutation is authorized.

---

## 5. Manifest Is Binding

Use `RELEASE_1.6_FILE_MANIFEST.md` as exact path authority.

WP07 may modify only:

- the existing Infrastructure schema/migration paths explicitly reconciled by WP06;
- the manifest-authorized Experiment Result persistence implementation path(s);
- the narrowly required existing unsupported-future-version fixture/test path identified by WP06, only to move the unsupported version beyond implemented v3.

Do not add convenience helpers, generic repositories, ORM layers, new projects, packages, or extra test files.

If required implementation cannot fit the manifest-authorized surface, stop.

---

## 6. Schema-v3 Implementation

Implement exactly the accepted WP06 physical model.

Target:

`PRAGMA user_version = 3`

The schema must add exactly the accepted durable Experiment Result physical object inventory.

Do not introduce deferred tables or schema-v4 concepts.

Schema-v3 creation must match WP06 in:

- table name;
- STRICT / WITHOUT ROWID choice;
- columns;
- affinities;
- nullability;
- key/unique constraints;
- checks;
- foreign-key policy;
- index policy;
- identity representation;
- decimal representation;
- aggregate-presence representation;
- provenance/reference fields.

Any material deviation from WP06 is a blocker.

---

## 7. Fresh Database Bootstrap

A fresh database initialized by Release 1.6 must atomically create the complete accepted schema through v3.

Require:

- all predecessor v1/v2 objects;
- `experiment_results` exactly as WP06 defines;
- schema version 3;
- no deferred tables;
- no Experiment Result seed/backfill;
- no Feature Set persistence.

Fresh initialization must leave a structurally valid v3 database.

---

## 8. v2 → v3 Migration

Implement one atomic, non-destructive migration from valid schema v2 to v3.

Require:

- existing observations unchanged;
- existing dataset/snapshot rows unchanged;
- no Feature Set backfill;
- no Experiment Result backfill;
- no provider/network activity;
- no research recomputation;
- new physical objects created;
- structural validation performed according to existing schema architecture;
- `user_version = 3` established within the migration unit.

Migration must reuse existing repository schema-management patterns.

Do not invent a parallel migration framework.

---

## 9. Migration Failure Atomicity

Migration must not expose a partially accepted v3 schema state.

If migration fails:

- no partially created durable Experiment Result capability should remain committed;
- predecessor v2 evidence must remain intact according to SQLite transaction behavior and existing repository conventions;
- schema version must not falsely report 3.

Do not add retry/repair logic.

Unknown defects propagate unless existing bounded Infrastructure schema handling already defines otherwise.

---

## 10. Existing v3 Validation

If the current schema bootstrap validates already-current databases, extend it so existing v3 databases require the accepted v3 structure.

Do not treat `user_version = 3` alone as sufficient if repository conventions validate required objects.

Missing/contradictory v3 structure must fail according to existing schema integrity behavior.

Do not broaden into WP09 public storage failure design.

---

## 11. Unsupported Future Version Fixture

WP06 identified that a predecessor negative test/fixture currently uses schema version 3 as unsupported.

Once v3 becomes implemented, update only that existing fixture/test value to a future version greater than 3, following repository convention.

Preferred minimal value:

`4`

unless the fixture convention requires a different next unsupported version.

This is a compatibility maintenance adjustment, not a new Release 1.6 permanent semantic test family.

Do not change unrelated tests.

---

## 12. Persistence Implementation Boundary

Implement the Infrastructure-owned `IDurableExperimentEvidenceStore` acceptance behavior necessary for WP07.

WP07 acceptance must consume complete WP04 durable evidence and return the accepted store result semantics.

Do not leak SQLite types/exceptions into Application.

Do not modify Application semantic vocabulary unless the manifest explicitly authorizes a minimal compatibility fix; if such a fix is required, stop unless clearly within WP07 authority.

---

## 13. NewlyAccepted Behavior

When no row exists for the exact Experiment Result Identity:

- validate the candidate enough to safely persist according to accepted WP03/WP06 invariants;
- atomically insert the complete evidence;
- return `NewlyAccepted`.

Require:

- one logical durable result;
- no partial row;
- exact identity;
- exact provenance/reference evidence;
- exact count/presence/decimal fidelity;
- no Feature Values;
- no provider data.

Do not create storage-generated semantic identity.

---

## 14. EquivalentExisting Behavior

When a row already exists for the exact Experiment Result Identity:

- compare the existing durable evidence to the candidate using the complete WP03 semantic equivalence rule;
- if equivalent, return `EquivalentExisting`;
- do not write a duplicate row;
- do not update the existing row;
- do not overwrite;
- do not mutate semantic evidence.

Equal identity alone is not enough.

Equal aggregates alone are not enough.

EquivalentExisting is successful idempotence.

---

## 15. IntegrityConflict Behavior

When the same Experiment Result Identity already exists but durable evidence is materially contradictory:

- return/map `IntegrityConflict` through the accepted contract boundary;
- do not overwrite;
- do not delete;
- do not merge;
- do not assign another identity;
- do not return `EquivalentExisting`;
- do not partially mutate the row.

This must be deterministic.

---

## 16. Concurrency / Uniqueness

Use the WP06 primary-key/uniqueness model to guarantee one logical row per Experiment Result Identity.

Implementation must behave safely when concurrent acceptance races occur.

Do not implement application retry loops.

A uniqueness race must resolve to the accepted semantic distinction by re-evaluating existing evidence as needed within bounded implementation logic, without producing duplicate rows.

Keep this behavior minimal and deterministic.

---

## 17. Exact Decimal Persistence

Implement the WP06 canonical decimal physical representation exactly.

Requirements:

- .NET `decimal` values round-trip without semantic loss;
- no SQLite REAL conversion;
- no culture dependence;
- no rounding;
- signed zero preserved if WP06 requires it;
- representation compatible with accepted Release 1.5 canonical decimal semantics.

Do not create an alternate decimal encoding.

---

## 18. Aggregate Presence / Empty Fidelity

Implement WP06 constraints and mapping so:

### Empty
- count 0;
- aggregate-presence absent;
- mean/min/max absent/NULL physically as defined;
- valid successful durable evidence.

### Non-empty
- count > 0;
- aggregate-presence present;
- all mean/min/max values present in canonical form.

Partial aggregate states must not be durably accepted.

---

## 19. Identity and Reference Fidelity

Persist exactly the WP06 identity/reference fields.

At minimum preserve all accepted fields represented by the physical model, including:

- Experiment Result fingerprint;
- Experiment Definition identity/name as specified;
- Feature Set identity;
- Feature Definition identity if specified;
- Dataset Snapshot identity;
- Dataset Definition identity;
- Research Dataset identity;
- Source State identity;
- source authority/reference;
- dataset observation count where specified;
- summary count/presence/aggregates.

Do not add operational metadata.

---

## 20. Foreign-Key Behavior

Implement the accepted restrictive relationship to `dataset_snapshots` exactly as WP06 defines.

Preserve:

- `ON UPDATE RESTRICT`;
- `ON DELETE RESTRICT`.

Do not add cascading behavior.

Do not require Feature Set persistence.

---

## 21. No Update/Delete Semantics

Persistence code must not expose update/delete operations for Experiment Results.

Do not implement generic CRUD.

Only:

- accept first complete evidence;
- recognize equivalent existing evidence;
- detect contradiction.

Exact retrieval is WP08.

---

## 22. WP08 Retrieval Boundary

WP07 may read an existing row internally only as needed to decide `EquivalentExisting` versus `IntegrityConflict`.

That internal comparison is not authorization to implement the public/exact WP08 retrieval use case.

Do not add:

- external exact retrieval orchestration;
- NotFound retrieval API behavior beyond existing WP04 contract stubs;
- list/search/history;
- restart retrieval proof.

WP08 owns exact retrieval implementation.

---

## 23. WP09 Failure-Mapping Boundary

Implement only the minimum storage exception handling required to keep WP07 acceptance safe and to honor existing contract boundaries.

Do not complete the broader WP09 storage validation/failure mapping design.

In particular:

- do not introduce new public failure values;
- do not broadly normalize unknown exceptions;
- do not add retry/fallback/repair;
- do not hide corruption.

If a storage-specific classification question cannot be resolved from existing authority, preserve the defect/exception and defer to WP09.

---

## 24. Connection / Transaction Ownership

Reuse existing Infrastructure connection ownership patterns.

Require:

- no leaked connection;
- no Application-owned connection;
- no Worker-owned transaction;
- acceptance atomicity scoped correctly;
- migration transaction separate from normal acceptance where existing architecture dictates.

Do not redesign connection architecture.

---

## 25. Security / Offline Boundary

No provider/network activity.

No credentials required beyond dummy/predecessor configuration if an existing composition path needs one for unrelated validation.

Do not persist:

- API keys;
- connection strings;
- secrets;
- provider payloads;
- logs;
- process/machine identity.

---

## 26. No DI / Worker Changes

WP10 owns DI/configuration.

WP11 owns durable Worker execution.

WP07 must not modify:

- Application DI;
- Infrastructure DI unless the manifest explicitly assigns the concrete store registration later to WP10;
- Worker configuration;
- Worker routing;
- `Program.cs`;
- Worker output/exit behavior.

If the store cannot be constructed in tests without DI, use direct construction in temporary probes; do not register it prematurely.

---

## 27. Permanent Test Boundary

WP12 owns Release 1.6 permanent persistence tests.

WP07 must not add a new permanent Release 1.6 test suite.

The only permanent test mutation authorized is the existing unsupported-future-schema fixture/version adjustment required because v3 is now implemented.

All semantic/storage acceptance proof during WP07 should use removable offline probes unless existing predecessor tests automatically validate the new schema paths.

---

## 28. Temporary Offline Proof

Use a removable offline probe if necessary to prove:

- fresh v3 bootstrap;
- v2→v3 migration;
- predecessor row preservation;
- `NewlyAccepted`;
- `EquivalentExisting`;
- `IntegrityConflict`;
- empty fidelity;
- non-empty decimal fidelity;
- no duplicate rows;
- migration rollback/failure behavior where practical;
- no provider/network activity;
- no residue.

Remove probe completely before final validation.

No package/project/reference changes may remain.

---

## 29. Predecessor Preservation

All existing Releases 1.1–1.5 permanent tests must remain green.

Preserve:

- observation persistence;
- dataset/snapshot persistence;
- schema-v2 predecessor migration semantics;
- Release 1.3 pipeline;
- Release 1.4 feature behavior;
- Release 1.5 experiment generation;
- Release 1.6 WP04/WP05 Application behavior.

Migration to v3 must not rewrite predecessor data.

---

## 30. Architecture / Package / Reference Preservation

Production graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Require:

- cycles 0;
- unexpected edges 0;
- Domain delta 0;
- Worker delta 0;
- package/project/reference delta 0/0/0.

No new package.

No new project.

---

## 31. Canonical Validation

Run targeted schema/persistence proof first.

Then run:

`eng/verify.ps1 -Configuration Release`

Expected permanent counts remain 238 unless the narrowly authorized future-version fixture modification changes no count:

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
- real credentials: 0.

After WP07, implemented schema must be v3.

---

## 32. Structural Acceptance

Require exact WP07 deltas only:

- Infrastructure schema/migration implementation: authorized;
- Infrastructure Experiment Result persistence implementation: authorized;
- existing unsupported-version fixture value: minimal authorized adjustment;
- Application semantic production delta: 0 unless manifest explicitly allows a necessary compatibility edit;
- Worker delta: 0;
- new permanent test file delta: 0;
- package/project/reference delta: 0/0/0.

No WP08+ implementation.

No Release 1.7 work.

---

## 33. Mutation Budget

Authorized repository mutations:

- exact manifest-authorized Infrastructure schema/migration path(s);
- exact manifest-authorized Experiment Result persistence implementation path(s);
- existing unsupported-future-schema fixture/test path, version value only as necessary.

Authorized GitHub mutations:

1. #188 Backlog → In Progress;
2. completion evidence comment;
3. close #188;
4. #188 In Progress → Done.

Not authorized:

- staging;
- commit;
- branch;
- push;
- PR;
- tag/release;
- milestone closure;
- #189–#195 mutation;
- DI/Worker;
- new permanent test suite;
- Release 1.7 work.

---

## 34. Stop Conditions

Stop with #188 OPEN / In Progress if:

- manifest path authority is ambiguous;
- implementation requires deviation from WP06 physical model;
- migration cannot preserve predecessor data atomically;
- exact decimal fidelity cannot be implemented;
- Feature Set persistence becomes required;
- public WP08 retrieval behavior becomes necessary to complete WP07;
- unresolved storage classification requires WP09 authority;
- new package/project/reference is required;
- permanent Release 1.6 tests beyond the authorized fixture adjustment become necessary;
- canonical verification fails;
- unexpected provider/network activity occurs;
- Release 1.7 work is detected.

Report the smallest corrective authority required.

---

## 35. Completion Evidence

Post concise #188 evidence including:

- exact changed paths;
- schema v3 implemented;
- fresh bootstrap behavior;
- v2→v3 migration;
- predecessor preservation;
- `experiment_results` implementation;
- `NewlyAccepted`;
- `EquivalentExisting`;
- `IntegrityConflict`;
- uniqueness/concurrency behavior;
- decimal and empty/non-empty fidelity;
- restrictive snapshot FK;
- unsupported-version fixture moved beyond v3;
- no WP08 public retrieval, WP09 broad mapping, DI, Worker, or new permanent test suite;
- canonical 238/238;
- next WP08/#189.

---

## 36. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting state;
4. exact changed paths;
5. schema-v3 implementation;
6. fresh bootstrap;
7. v2→v3 migration;
8. migration atomicity/failure behavior;
9. existing-v3 validation;
10. unsupported-version fixture adjustment;
11. Experiment Result persistence implementation;
12. NewlyAccepted;
13. EquivalentExisting;
14. IntegrityConflict;
15. uniqueness/concurrency;
16. decimal representation;
17. empty/non-empty fidelity;
18. identity/reference fidelity;
19. FK behavior;
20. no update/delete;
21. WP08 retrieval boundary preservation;
22. WP09 failure-mapping boundary preservation;
23. connection/transaction ownership;
24. provider/security isolation;
25. predecessor regression;
26. temporary probe evidence;
27. canonical validation;
28. whitespace/security/residue;
29. architecture/package/reference checks;
30. repository mutation accounting;
31. GitHub lifecycle;
32. findings/blockers;
33. next authorized WP.

---

## 37. Completion Marker

On success end exactly:

`RELEASE 1.6 WP07 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP08 — Exact Experiment Result Retrieval — GitHub issue #189`

Required final lifecycle:

- #182–#188: CLOSED / Done
- #189–#195: OPEN / Backlog
- milestone #47: OPEN

If blocked end:

`RELEASE 1.6 WP07 BLOCKED`

and identify the smallest corrective authority required.
