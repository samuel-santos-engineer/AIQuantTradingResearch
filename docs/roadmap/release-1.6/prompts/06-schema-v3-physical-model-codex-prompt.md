# Release 1.6 WP06 — Schema-v3 Physical Model — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP06 — Schema-v3 Physical Model — GitHub issue #187**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP06 defines and freezes the physical SQLite schema-v3 model required to persist accepted durable Experiment Result evidence while preserving Releases 1.1–1.5 and all existing schema-v2 data.

WP06 is design/documentation-only unless the accepted Release 1.6 file manifest explicitly assigns a narrowly scoped schema-definition code path to WP06. It must not implement Experiment Result persistence behavior, exact retrieval behavior, storage failure mapping, DI, Worker behavior, or permanent tests.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- accepted WP01–WP05 execution evidence
- current SQLite schema/migration implementation and tests from Releases 1.1–1.5
- current Release 1.2 snapshot/catalog persistence schema
- current schema version detection/migration conventions
- current SQLite connection/transaction ownership conventions
- this WP06 authority and its five-line companion

Treat WP02/WP03 as semantic authority and WP04/WP05 as Application contract/orchestration authority.

Do not redefine semantic evidence in WP06.

---

## 3. Starting Gate

Before mutation verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main == 18dfb01bf3503d91415b081b11fcdd7249094373`;
- ahead/behind: `0/0`;
- staged paths: 0;
- unexpected tracked modifications: 0;
- #182–#186: CLOSED / Done;
- #187: OPEN / Backlog;
- #188–#195: OPEN / Backlog;
- milestone #47: OPEN, 9 open / 5 closed;
- Project #2 fields remain correct;
- predecessor Release restoration remains 89/89 exact;
- implemented SQLite schema remains v2;
- no schema-v3 implementation exists yet;
- no premature WP07+ implementation exists;
- no Release 1.7 work exists.

Expected untracked Release 1.6 governance/candidate artifacts are not blockers.

If any mandatory gate fails, stop before moving #187 to In Progress.

---

## 4. Authorized Lifecycle

After all starting gates pass:

1. move #187 `Backlog → In Progress`;
2. perform WP06 physical-model discovery and definition;
3. create only the manifest-authorized WP06 schema-v3 physical-model artifact;
4. validate;
5. post concise completion evidence to #187;
6. close #187;
7. set #187 `In Progress → Done`.

Required final lifecycle:

- #182–#187: CLOSED / Done;
- #188–#195: OPEN / Backlog;
- milestone #47: OPEN, 8 open / 6 closed.

No other GitHub lifecycle mutation is authorized.

---

## 5. Sole Authorized Artifact

Create exactly the WP06 artifact identified by:

`docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`

Expected logical role:

**Experiment Persistence Schema v3**

Use the manifest path and filename exactly.

Expected repository-content delta:

- schema/architecture documentation: +1;
- production persistence behavior: 0;
- permanent tests: 0;
- package/project/reference: 0/0/0.

If the manifest requires naming exact existing schema/migration implementation files as future WP07-owned paths, identify them in the document/report without modifying them.

---

## 6. Physical-Model Objective

Freeze the complete SQLite schema-v3 design needed for WP07/WP08 without implementing persistence/retrieval behavior.

The document must define:

- new table(s);
- exact columns;
- exact SQL affinities/types;
- nullability;
- primary key;
- unique constraints;
- check constraints;
- foreign-key policy if any;
- indexes if justified;
- canonical storage representation for identities and decimals;
- aggregate-presence representation;
- provenance/lineage storage requirements;
- migration v2→v3;
- fresh database v3 creation;
- migration atomicity;
- predecessor data preservation;
- failure/rollback expectations;
- schema-version transition;
- explicit exclusions.

Do not write repository implementation code in WP06 unless manifest authority explicitly says otherwise.

---

## 7. Minimal Table Principle

Prefer the smallest physical model that can preserve the complete WP03 durable semantic evidence.

Do not create:

- Feature Set persistence tables;
- Feature Value tables;
- generalized experiment definitions registry;
- experiment history table;
- experiment run table;
- comparison/search table;
- audit/event table;
- retry/checkpoint table;
- provider metadata table;
- mutable status table.

Release 1.6 persists Experiment Result evidence only.

---

## 8. Experiment Result Primary Key

The durable Experiment Result identity must be the semantic lookup key.

Preferred physical rule:

- exact Experiment Result fingerprint as primary key or uniquely constrained key;
- no independent storage-generated semantic identity.

If an internal SQLite rowid exists implicitly, it must have no semantic meaning and must not participate in Application contracts.

Do not introduce UUID/sequence identity.

---

## 9. Identity Storage Representation

Freeze exact storage representation for:

- Experiment Result Identity;
- Experiment Definition Identity;
- Feature Set Identity;
- snapshot identity if persisted as part of reduced evidence;
- dataset/source identity/reference fields required by WP03.

Prefer the existing repository representation conventions where they preserve exact typed identity semantics.

Requirements:

- no case-folding ambiguity;
- no culture dependence;
- no whitespace normalization;
- exact 64-lowercase-hex preservation where the identity type is a SHA-256 fingerprint;
- constraints should reject malformed persisted fingerprints where practical.

Do not invent a new encoding scheme.

---

## 10. Version / Definition Fields

Persist only definition/version evidence required by WP03/WP04.

If the reduced evidence contains the built-in experiment definition identifier/version, define its physical representation explicitly.

Do not create a generalized definitions table or registry.

Do not allow “latest version” semantics.

The persisted result remains bound to the exact accepted Release 1.5 definition evidence.

---

## 11. Count Physical Representation

Define an exact physical representation for count.

Requirements:

- non-negative;
- sufficient range for accepted Application semantics;
- no floating-point storage;
- no text-format ambiguity unless repository conventions require text;
- check constraint where practical.

Do not silently narrow the Application semantic range.

---

## 12. Aggregate Presence Representation

Freeze a physical representation that preserves the semantic distinction:

- aggregates absent;
- aggregates present.

For empty successful evidence:

- count = 0;
- mean/minimum/maximum absent.

For non-empty evidence:

- aggregate values present according to Release 1.5 invariants.

Prefer schema-level coherence constraints where practical.

Do not represent absence with numeric zero or magic sentinel values.

---

## 13. Decimal Storage Representation

Choose and document a deterministic SQLite physical representation that round-trips .NET `decimal` values exactly.

Evaluate repository precedent first.

The chosen representation must preserve:

- sign;
- coefficient/value;
- scale/value equivalence required by Application semantics;
- exact decimal value;
- culture independence;
- no binary floating-point loss;
- no rounding.

Do not use SQLite `REAL` if it cannot prove exact .NET decimal fidelity.

If textual canonical decimal storage is selected, define its canonical form precisely enough for WP07/WP08.

If decomposed sign/coefficient/scale storage is selected, justify it against simplicity and existing repository conventions.

Do not change Release 1.5 identity canonicalization; physical storage representation is separate.

---

## 14. Empty / Non-Empty Coherence Constraints

Define schema-level constraints where appropriate to reject impossible durable states.

At minimum reason about:

### Empty
- count = 0;
- aggregate-presence = absent;
- aggregate columns null/absent.

### Non-empty
- count > 0;
- aggregate-presence = present;
- mean/minimum/maximum present.

Do not rely entirely on Application validation if simple database constraints can prevent persistent corruption.

Do not overcomplicate with business logic triggers unless repository precedent strongly justifies them.

---

## 15. Provenance Storage

Persist only provenance/lineage evidence required by WP03 reduced durable evidence.

Define whether each required provenance field is:

- inline in the Experiment Result table;
- referenced to an existing durable predecessor object/table;
- represented as exact immutable identity/reference evidence.

Prefer reuse of existing snapshot/dataset durable identities rather than duplicating full predecessor data.

Do not persist Feature Values.

Do not create a generalized provenance graph table unless WP03 semantics absolutely require it.

---

## 16. Existing Snapshot / Dataset Relationships

Inspect current schema-v2 snapshot/catalog tables.

If a foreign-key relationship from durable Experiment Result to an existing snapshot/dataset row can be established without changing semantic behavior, evaluate it.

However:

- do not require a foreign key merely because it is possible;
- do not create Feature Set persistence;
- do not force deletion/update cascade semantics that conflict with immutable predecessor data;
- do not introduce cyclic storage dependencies.

Document the selected relationship policy and rationale.

---

## 17. Foreign-Key Policy

If foreign keys are used, freeze:

- referenced table/key;
- `ON UPDATE`;
- `ON DELETE`;
- migration implications.

Prefer immutable/restrictive semantics.

Do not introduce cascading deletion of durable research evidence.

If no FK is selected for a semantic identity reference, explain how integrity is instead validated by Application/Infrastructure boundaries.

---

## 18. Index Policy

Create only indexes required for:

- exact Experiment Result identity retrieval;
- foreign-key enforcement/performance where needed;
- deterministic known access patterns explicitly in Release 1.6 scope.

Do not add:

- history indexes;
- aggregate search indexes;
- definition search indexes;
- timestamp indexes;
- generic query optimization for deferred capabilities.

Release 1.6 lookup is exact identity only.

---

## 19. No Update/Delete Physical Semantics

Freeze absence of update/delete behavior.

Do not design mutable columns or lifecycle fields for in-place mutation.

The physical model should support:

- insert first acceptance;
- validate equivalent existing evidence;
- exact retrieval.

Contradictory existing rows must not be overwritten.

---

## 20. Schema Version

Release 1.6 target schema version is:

`3`

Freeze exactly one schema-version transition:

`v2 → v3`

Preserve support for:

- fresh database creation directly at v3;
- migration of valid v2 database to v3;
- predecessor data preservation;
- existing v1→v2 historical migration semantics as required by current architecture.

Do not create v4 concepts.

---

## 21. v2 → v3 Migration

Define exact migration behavior.

Requirements:

- atomic;
- non-destructive;
- idempotence according to existing schema-management conventions;
- no loss or rewrite of existing observation/snapshot data;
- no Feature Set backfill;
- no Experiment Result backfill unless accepted evidence already exists durably, which Release 1.5 explicitly did not provide;
- no provider access;
- no network;
- no synthetic experiment generation during migration.

Migration creates the physical capability only.

---

## 22. Migration Atomicity

Freeze:

- schema-version increment and new physical objects must succeed as one migration unit according to existing repository transaction conventions;
- failure leaves no partially accepted v3 schema state;
- predecessor v2 data remains usable/unmodified after failed migration where SQLite transaction semantics permit.

Document expected rollback/failure state.

Do not implement transaction code in WP06.

---

## 23. Fresh Database v3

Define fresh-database creation requirements.

A fresh database initialized under Release 1.6 must contain:

- all accepted predecessor schema objects;
- new durable Experiment Result physical objects;
- schema version 3;
- no deferred Release 1.7+ tables.

Fresh creation and migrated v2→v3 databases must be semantically equivalent in schema capabilities.

---

## 24. Unsupported Future Version Behavior

Preserve existing unsupported-future-version behavior.

Do not weaken guards that reject unknown schema versions.

The pre-existing schema-v3 negative test fixture used while v2 is current must be reconciled carefully once v3 becomes implemented in WP07: future unsupported-version tests should move to a version beyond current only under later authorized implementation/test work.

WP06 documents this implication but does not modify tests.

---

## 25. Schema Fingerprint / Validation

If the repository has a schema-validation or schema-version verification mechanism, define how v3 will be recognized and validated.

Do not invent a parallel schema framework.

Reuse existing schema-management architecture.

WP07 will implement.

---

## 26. Failure Boundaries

Physical-model design must support later distinction between:

- schema/migration unavailable;
- invalid stored evidence;
- integrity conflict;
- exact NotFound;
- dependency unavailable.

Do not create new public Application failures in WP06.

Storage-specific exceptions remain WP09 responsibility.

---

## 27. Concurrency and Uniqueness

Reason about concurrent acceptance of the same Experiment Result identity.

The physical model must allow WP07 to guarantee one logical durable result.

Use uniqueness/primary-key constraints sufficient to prevent duplicate same-identity rows.

Do not define application retry loops.

Equivalent/concurrent insert resolution behavior belongs WP07/WP09 implementation semantics, but schema must make duplicate logical storage impossible.

---

## 28. Connection and Transaction Ownership

Document how the schema design fits existing Infrastructure ownership conventions.

Do not change connection ownership architecture.

Do not embed connection lifecycle into Application contracts.

WP07 implements using existing Infrastructure patterns.

---

## 29. Security / Data Minimization

Persist only semantic evidence required for durable fidelity.

Do not persist:

- API keys;
- credentials;
- secrets;
- connection strings;
- provider payloads not required by the accepted result;
- raw Feature Values;
- raw source observations duplicated solely for experiment evidence;
- process/machine identifiers;
- logs.

---

## 30. Architecture Preservation

Physical model must not require a project/reference graph change.

Preserve:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

No new project/package/reference is authorized by WP06.

---

## 31. Explicit Deferrals

Do not design physical schema for:

- Feature Set persistence;
- feature catalog;
- generalized experiment definitions;
- registry/history;
- list/search/comparison;
- update/delete/retention;
- strategies/signals/backtesting;
- scheduling/retries/checkpoints;
- provider acquisition;
- workspace/UI/API;
- AI/ML;
- Release 1.7 work.

---

## 32. Downstream Authority Protection

WP06 must leave implementation to:

- WP07 — Experiment Result persistence and migration implementation;
- WP08 — exact retrieval;
- WP09 — storage validation/failure mapping;
- WP10 — DI/configuration;
- WP11 — Worker;
- WP12 — permanent tests;
- WP13 — documentation/current-state alignment;
- WP14 — integration.

Do not write production persistence/retrieval code.

---

## 33. Documentation Requirements

The WP06 artifact must contain:

- current schema-v2 baseline summary;
- target schema-v3 summary;
- exact new physical object inventory;
- columns/types/nullability;
- keys/constraints/indexes;
- identity representation;
- decimal representation;
- aggregate-presence representation;
- provenance/reference representation;
- empty/non-empty row invariants;
- v2→v3 migration;
- fresh v3 creation;
- migration atomicity/failure state;
- predecessor preservation;
- concurrency/uniqueness considerations;
- unsupported-version implications;
- explicit exclusions;
- WP07/WP08 implementation handoff.

Use repository-relative links if used.

No broken links, trailing whitespace, or missing final newline.

---

## 34. Validation

After creating the sole authorized artifact, run:

`eng/verify.ps1 -Configuration Release`

Expected permanent counts remain:

- Domain.Tests: 11/11
- Application.Tests: 102/102
- Infrastructure.Tests: 112/112
- Architecture.Tests: 13/13
- Total: 238/238
- Skipped: 0

Require:

- warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- direct expected-untracked whitespace/final-newline checks: PASS;
- staged paths: 0;
- production implementation delta: 0;
- permanent-test delta: 0;
- package/project/reference delta: 0/0/0;
- implemented schema remains v2;
- dependency graph unchanged;
- database/WAL/SHM/journal residue: 0;
- provider/network activity: 0;
- real credentials: 0;
- Release 1.7 work: 0.

---

## 35. Mutation Budget

Authorized repository mutation:

- exactly one manifest-authorized WP06 schema-v3 physical-model document.

Authorized GitHub mutations:

1. #187 Backlog → In Progress;
2. completion evidence comment;
3. close #187;
4. #187 In Progress → Done.

Not authorized:

- schema implementation;
- SQL migration code;
- persistence/retrieval code;
- tests;
- DI;
- Worker;
- packages/projects/references;
- staging;
- commits;
- branches;
- pushes;
- PRs;
- milestone closure;
- #188–#195 mutation.

---

## 36. Stop Conditions

Stop with #187 OPEN / In Progress if:

- manifest artifact path is ambiguous;
- WP02/WP03/WP04/WP05 semantics materially conflict;
- exact durable evidence requirements cannot map to a coherent SQLite model;
- exact decimal fidelity cannot be achieved without unresolved design contradiction;
- schema requires Feature Set persistence;
- new package/project/reference appears necessary;
- production code must change to complete WP06;
- canonical verification fails;
- implemented schema changes from v2;
- unexpected provider/network/database residue appears;
- Release 1.7 work is detected.

Report the smallest corrective authority required.

---

## 37. Completion Evidence

Post concise #187 evidence covering:

- artifact path;
- target schema v3;
- physical object inventory;
- identity storage representation;
- decimal storage representation;
- empty/non-empty constraints;
- provenance/reference policy;
- exact identity lookup key;
- uniqueness/concurrency protection;
- v2→v3 migration;
- fresh-v3 behavior;
- atomicity/rollback;
- predecessor preservation;
- no Feature Set persistence/registry/history;
- implemented schema still v2;
- canonical 238/238;
- next WP07/#188.

---

## 38. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting state;
4. exact artifact;
5. current v2 baseline;
6. target v3 object inventory;
7. table design;
8. identity representation;
9. definition/version representation;
10. count representation;
11. aggregate presence;
12. decimal representation;
13. empty/non-empty constraints;
14. provenance/reference model;
15. FK policy;
16. index policy;
17. update/delete exclusion;
18. v2→v3 migration;
19. migration atomicity;
20. fresh-v3 creation;
21. predecessor preservation;
22. unsupported-version handling;
23. concurrency/uniqueness;
24. connection/transaction ownership fit;
25. security/data minimization;
26. architecture/package/reference preservation;
27. downstream authority preservation;
28. validation;
29. whitespace/security/residue;
30. repository mutation accounting;
31. GitHub lifecycle;
32. findings/blockers;
33. next WP.

---

## 39. Completion Marker

On success end exactly:

`RELEASE 1.6 WP06 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP07 — Experiment Result Persistence — GitHub issue #188`

Required final lifecycle:

- #182–#187: CLOSED / Done
- #188–#195: OPEN / Backlog
- milestone #47: OPEN

If blocked end:

`RELEASE 1.6 WP06 BLOCKED`

and identify the smallest corrective authority required.
