# Release 1.9 — WP03 Minimal Schema Evolution Definition — Codex Authority

## Authority

This document grants a **narrow, definition-only schema-evolution authority** for Release 1.9 WP03, canonical GitHub issue **#228**.

The governing provenance decision is now established:

- `source_authority = 0`
  = `AcceptedRelease11HistoricalObservations`
  = historical persisted evidence;
- `source_authority = 1`
  = `Release19SimulatedLiveReplay`
  = truthful Release 1.9 Replay provenance;
- SQLite snapshot and experiment-result persistence currently constrain `source_authority = 0`;
- Release 1.9 requires persisted Replay snapshot/catalog evidence;
- ephemeral Replay would violate the accepted Release 1.9 requirement;
- no alternate governed Replay persistence path exists;
- therefore the architecture requires a **minimal schema evolution** that permits truthful Replay provenance.

This authority exists only to define that schema evolution precisely.

It does **not** authorize implementation.

It does **not** authorize migration-file creation.

It does **not** authorize changing `PRAGMA user_version`.

It does **not** authorize production code, test, GitHub, or planning mutation.

WP04 remains unstarted.

---

# Objective

Define the smallest coherent schema evolution required for WP03 to persist Replay-derived evidence truthfully while preserving all historical semantics.

The definition must specify:

1. the target schema version;
2. the exact affected tables;
3. the exact constraint changes;
4. the authoritative allowed `source_authority` domain;
5. migration behavior for existing v3 databases;
6. preservation of all existing rows;
7. read/query compatibility;
8. insertion/update compatibility;
9. downgrade/rollback expectations under repository convention;
10. migration atomicity requirements;
11. migration idempotence/version-gating rules;
12. required schema tests;
13. required persistence tests;
14. required Worker/WP03 production-flow revalidation after implementation;
15. explicit non-goals.

The resulting contract must be specific enough for a later Terra implementation authority to implement without inventing schema semantics.

---

# Fixed Architectural and Provenance Invariants

These are authoritative and must not be reopened.

## Historical authority

`0 = AcceptedRelease11HistoricalObservations`

Historical persisted evidence remains valid and unchanged.

## Replay authority

`1 = Release19SimulatedLiveReplay`

Replay persisted evidence must retain authority `1`.

Replay must never be rewritten as authority `0` merely to satisfy legacy schema constraints.

## Persistence requirement

Release 1.9 Replay requires persisted snapshot/catalog evidence.

Non-persistent Replay is not an acceptable substitute.

## Pipeline

Historical and Replay use the same canonical five-stage `ExecuteCanonical` pipeline.

The schema evolution must not redesign pipeline stages.

## Worker/configuration

The previously defined Worker Replay configuration and Dataset-boundary contracts remain fixed.

Do not redefine them.

---

# Known Current Schema State

Current accepted schema:

`SQLite schema v3`

Known affected persistence surfaces include at least:

- persisted snapshots;
- experiment-result persistence.

Current relevant constraints require:

`source_authority = 0`

The definition pass must inspect the exact DDL and determine every schema object that encodes this restriction.

Do not assume only two objects are affected until proven.

---

# Permitted Scope

This authority may read:

- #228;
- Release 1.9 WP03 manifest/definition;
- all schema v1/v2/v3 DDL/migrations;
- schema bootstrap logic;
- schema versioning code;
- snapshot table DDL;
- experiment-result table DDL;
- indexes/triggers/views/check constraints involving `source_authority`;
- repository/domain source-authority enums/constants;
- persistence repositories;
- catalog registration code;
- queries filtering or assuming authority `0`;
- migration tests;
- schema tests;
- persistence tests;
- backup/transaction/migration conventions;
- release-governance rules for schema versioning.

It may produce one normative schema-evolution definition.

If no dedicated artifact path is authorized, return the contract in the completion report only.

---

# Explicitly Forbidden

Do not:

- edit schema files;
- create migration files;
- update schema bootstrap code;
- change `PRAGMA user_version`;
- modify production code;
- modify tests;
- alter runtime source-authority assignments;
- change Worker configuration;
- change Dataset semantics;
- change WP02 contracts;
- modify GitHub;
- close #228;
- alter Release 1.9 planning;
- start WP04.

This authority is specification-only.

---

# Phase 0 — Read Current Migration Architecture

Before defining the target:

1. Read schema v1, v2, and v3 definitions/migrations.
2. Identify the repository's exact schema-versioning convention.
3. Identify how `PRAGMA user_version` is advanced.
4. Identify whether migrations are:
   - transactional;
   - forward-only;
   - bootstrap-from-zero;
   - sequential;
   - rebuild-table based.
5. Identify rollback/downgrade conventions, if any.
6. Identify all tables/views/triggers/indexes/check constraints that reference `source_authority`.
7. Identify all application queries that assume only authority `0`.

Do not mutate anything.

---

# Phase 1 — Determine Target Schema Version

Define the next schema version according to repository convention.

Expected candidate:

`schema v4`

Do not select v4 merely by arithmetic if repository governance uses another versioning rule.

The contract must state:

- exact target version;
- predecessor version;
- permitted upgrade path;
- behavior when database is already at target version;
- behavior when database version is unexpected/newer.

---

# Phase 2 — Define Source-Authority Domain

Define the persisted domain exactly.

Required semantic values:

- `0 = AcceptedRelease11HistoricalObservations`
- `1 = Release19SimulatedLiveReplay`

Determine whether the database constraint should be:

- `source_authority IN (0, 1)`;
- or another equivalent form consistent with repository DDL conventions.

Do not broaden to arbitrary integers.

Do not reserve speculative future values unless repository schema convention explicitly does so.

The database contract must reject invalid source-authority values outside the governed domain.

---

# Phase 3 — Identify Exact Schema Objects to Change

For every object containing or validating `source_authority`, document:

- object/table name;
- current column definition;
- current constraint;
- target constraint;
- whether indexes/triggers/views must change;
- whether dependent foreign keys or uniqueness rules are affected.

At minimum inspect:

- snapshot persistence;
- experiment-result persistence.

Do not assume a constraint is isolated if table recreation would affect indexes/foreign keys.

---

# Phase 4 — Define Migration Mechanics

Given SQLite's constraint-alteration limitations and the repository's existing migration style, define the exact migration pattern required.

If table rebuild is required, specify the conceptual sequence:

1. begin governed transaction;
2. create replacement table with target constraints;
3. copy existing rows without data transformation except structural necessity;
4. verify row preservation;
5. replace old table;
6. recreate indexes/triggers/views;
7. restore foreign-key relationships/validation as required;
8. advance `PRAGMA user_version`;
9. commit.

Use repository conventions rather than inventing a new migration framework.

The definition must state whether foreign-key enforcement must be temporarily managed and how integrity is verified.

---

# Phase 5 — Existing Data Preservation Contract

Existing v3 rows use historical authority `0`.

The migration definition must require:

- all existing rows preserved exactly;
- `source_authority = 0` remains `0`;
- no historical row reclassification;
- row counts preserved;
- primary keys preserved;
- foreign-key references preserved;
- timestamps/content preserved;
- catalog relationships preserved.

No data backfill to authority `1` is allowed unless pre-existing Replay rows are proven to exist, which current constraints make unlikely.

Do not invent Replay history.

---

# Phase 6 — Read/Query Compatibility

Inspect all reads/queries involving source authority.

Define whether they should:

- remain authority-agnostic and return both governed authorities;
- continue explicitly selecting Historical only where historical-only semantics are intended;
- add explicit Replay-aware behavior in later implementation where #228 requires it.

The schema contract must distinguish:

## Historical-only query semantics

Queries intentionally requiring historical evidence may continue to constrain authority `0`.

## General persisted-evidence semantics

Queries/catalog paths meant to support Release 1.9 Replay must accept authority `1`.

Do not silently change business semantics in the migration itself.

If code changes are needed after schema evolution, list them as later implementation requirements rather than performing them here.

---

# Phase 7 — Insert/Update Compatibility

Define post-migration persistence rules:

- Historical insert with authority `0` succeeds.
- Replay insert with authority `1` succeeds where WP03 persistence requires it.
- Invalid authority values fail.
- Existing required columns/constraints remain enforced.
- No insert path may silently coerce authority `1` to `0`.

If update semantics permit changing source authority, determine whether that is already governed.

Do not authorize provenance mutation between historical and Replay unless existing repository semantics explicitly permit it.

Prefer immutable provenance if that matches current design.

---

# Phase 8 — Migration Atomicity and Failure Semantics

Define:

- whether migration must be transactional;
- what happens on partial failure;
- when `user_version` changes;
- how to prove no half-migrated schema survives;
- whether backup/restore behavior exists in repository conventions;
- how migration failure surfaces to application startup.

Required principle:

> `PRAGMA user_version` must advance only after all required schema transformations and integrity checks succeed.

No partially applied v4 state may be reported as v4.

---

# Phase 9 — Idempotence and Version-Gating Contract

Define expected behavior for:

## v3 database

Upgrade once to target version.

## target-version database

No-op / normal startup under repository convention.

## older-than-v3 database

Follow existing sequential migration path if supported.

Do not invent skip-level behavior inconsistent with repository convention.

## newer-than-target database

Fail or follow existing forward-compatibility behavior.

Define from repository evidence.

---

# Phase 10 — Rollback / Downgrade Expectations

Determine actual repository governance.

If migrations are forward-only:

- state clearly that no automatic downgrade migration is required;
- require transactional rollback only for a failed in-progress migration.

If explicit downgrade migrations exist:

- define the required v4→v3 behavior;
- address Replay rows with authority `1`, which cannot fit v3.

Do not invent destructive downgrade behavior.

If downgrade would lose Replay evidence, state that as a reason not to support automatic downgrade unless governance requires it.

---

# Phase 11 — Required Schema Tests

Define the future implementation test contract.

At minimum:

## Migration/versioning

- pristine/bootstrap database reaches target schema;
- v3 database upgrades to target version;
- already-target database remains valid;
- unexpected/newer version follows governed failure behavior;
- `PRAGMA user_version` exact value verified.

## Data preservation

- v3 historical snapshot rows survive unchanged;
- v3 historical experiment-result rows survive unchanged;
- row counts/keys/relationships preserved.

## Constraint domain

- authority `0` snapshot insert succeeds;
- authority `1` snapshot insert succeeds;
- invalid authority snapshot insert fails;
- authority `0` experiment-result insert succeeds;
- authority `1` experiment-result insert succeeds;
- invalid authority experiment-result insert fails.

## Integrity

- foreign keys remain valid;
- indexes/triggers/views recreated correctly if affected;
- migration failure does not expose half-migrated target version.

---

# Phase 12 — Required WP03 Persistence Tests

Define tests required after the later migration implementation:

- Replay persistence writes source authority `1`;
- persisted Replay snapshot retains authority `1`;
- persisted Replay experiment result retains authority `1`;
- historical persistence remains authority `0`;
- no Replay row is mislabeled `0`;
- catalog evidence can distinguish Historical from Replay;
- relevant reads return Replay evidence where #228 requires it;
- Historical-only reads remain historical-only where intended;
- real Worker Replay production flow persists/catalogs required evidence;
- finite Replay completion succeeds;
- same canonical five-stage pipeline remains used.

---

# Phase 13 — Regression / Compatibility Requirements

The later implementation authority must rerun:

- schema/migration tests;
- persistence tests;
- Application focused suite;
- WP02 Replay suite;
- WP03 Worker/configuration tests;
- full build;
- full solution regression.

Immediate pre-schema-change full-suite baseline:

**288/288 passing**

Any higher count should be explained by migration/persistence tests.

Historical behavior must remain backward compatible.

---

# Phase 14 — Normative Schema Evolution Contract

Produce one exact contract with:

## Version

- predecessor version;
- target version.

## Affected objects

Exact table/object inventory.

## Constraint changes

Before/after constraint text or semantic equivalent.

## Migration mechanics

Exact governed sequence.

## Data preservation

Exact invariants.

## Query compatibility

Historical-only vs general Replay-aware semantics.

## Insert/update rules

Exact authority behavior.

## Atomicity/failure

Exact requirements.

## Version gating

Exact behavior by starting version.

## Rollback/downgrade

Exact governed expectation.

## Required implementation code changes

List only changes that a later authority must permit, such as:
- migration/schema definitions;
- schema bootstrap/version logic;
- persistence/query compatibility changes if required;
- tests;
- WP03 production-flow revalidation.

Do not implement them here.

---

# Decision Discipline

Prefer the smallest schema delta that truthfully supports authority `1`.

Do not redesign persistence.

Do not widen source authority beyond values `0` and `1`.

Do not change unrelated columns, indexes, or constraints.

Do not use schema evolution as an excuse for cleanup.

Every normative choice must be grounded in current migration and schema conventions.

---

# Stop Conditions

Stop if:

- migration/versioning conventions cannot be determined;
- exact affected schema objects cannot be identified;
- existing queries make authority `1` semantics ambiguous;
- Release 1.9 requires persistence behavior beyond a minimal provenance-domain expansion;
- migration would require broad schema redesign;
- rollback requirements are materially ambiguous under governance;
- more than one materially different minimal migration contract remains equally valid.

On stop:

- make zero schema/code/test/GitHub changes;
- report exact unresolved schema decision;
- identify the minimum additional governance authority required.

---

# Success Criteria

This authority succeeds only when one unambiguous minimal schema-evolution contract is established that defines:

- target schema version;
- affected schema objects;
- exact authority-domain expansion;
- migration mechanics;
- preservation of all existing historical data;
- read/query compatibility;
- insert/update compatibility;
- atomicity/failure behavior;
- version gating;
- rollback/downgrade expectations;
- required schema tests;
- required WP03 persistence tests;
- required later implementation scope.

No schema or production mutation occurs.

No GitHub mutation occurs.

WP04 remains unstarted.

---

# Required Completion Report

Return:

## Target schema
- current version;
- target version;
- version-gating behavior.

## Affected objects
List every table/view/trigger/index/constraint requiring change.

## Source-authority domain
- `0` semantics;
- `1` semantics;
- exact target constraint.

## Migration contract
- exact conceptual steps;
- transaction/atomicity behavior;
- `user_version` advancement point;
- integrity verification.

## Existing-data preservation
- row/key/reference invariants.

## Query/persistence compatibility
- historical-only behavior;
- Replay-aware behavior;
- insert/update semantics.

## Rollback/downgrade
State governed expectation.

## Required future tests
List all migration, constraint, persistence, production-flow, and regression scenarios.

## Future implementation authority
State exact categories of files/changes that must be authorized.

## Mutation proof

Expected:

`WP03 MINIMAL SCHEMA EVOLUTION DEFINITION MUTATIONS: ZERO`

## Next step

State:

`WP03 MINIMAL SCHEMA EVOLUTION CONTRACT DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

Do not implement it here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP03 MINIMAL SCHEMA EVOLUTION DEFINITION COMPLETE`

On blocker:

`RELEASE 1.9 WP03 MINIMAL SCHEMA EVOLUTION DEFINITION BLOCKED`

Emit success only if the migration contract is fully unambiguous.
