# Experiment Persistence Schema v3

## Purpose and authority

This document freezes the Release 1.6 SQLite physical model for durable
Experiment Result evidence. It translates the accepted
[`DURABLE_EXPERIMENT_EVIDENCE.md`](DURABLE_EXPERIMENT_EVIDENCE.md) and
[`EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`](EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md)
semantics into a storage design. It does not implement schema v3, migration,
persistence, retrieval, failure mapping, dependency registration, or Worker
behavior.

Release 1.5 remains authoritative for `aiq-experiment-identity-v1` and the
meaning of the immutable Experiment Result. This physical model creates no new
semantic identity.

## Current schema-v2 baseline

The implemented repository remains at `PRAGMA user_version = 2`. Its existing
Infrastructure-owned bootstrapper:

- enables SQLite foreign keys for each opened connection;
- begins one transaction before reading or changing schema state;
- creates or validates `historical_observations` for schema v1;
- creates or validates `dataset_snapshots` and
  `dataset_snapshot_observations` for schema v2;
- uses strict `WITHOUT ROWID` tables and binary-collated text identifiers;
- stores exact decimal observation prices as invariant text;
- advances `user_version` inside the schema transaction;
- commits only after structural validation succeeds; and
- rejects unsupported versions and incompatible structures.

Schema v2 owns durable observations, Dataset Snapshot evidence, and exact
snapshot observations. It contains no Feature Set or Experiment Result table.

## Target schema-v3 inventory

Schema v3 retains every schema-v2 object unchanged and adds exactly one table:

`experiment_results`

No auxiliary table, trigger, view, sequence, or new semantic identifier is
required. The table is `STRICT, WITHOUT ROWID`; its primary key is the exact
Release 1.5 Experiment Result fingerprint.

## Experiment Result table

The target definition is:

```sql
CREATE TABLE experiment_results (
    experiment_result_identity TEXT COLLATE BINARY NOT NULL
        CHECK (
            length(experiment_result_identity) = 64
            AND experiment_result_identity NOT GLOB '*[^0-9a-f]*'),
    experiment_identity_scheme TEXT COLLATE BINARY NOT NULL
        CHECK (experiment_identity_scheme = 'aiq-experiment-identity-v1'),
    experiment_definition_name TEXT COLLATE BINARY NOT NULL
        CHECK (experiment_definition_name = 'simple-return-descriptive-summary-v1'),
    experiment_definition_identity TEXT COLLATE BINARY NOT NULL
        CHECK (
            length(experiment_definition_identity) = 64
            AND experiment_definition_identity NOT GLOB '*[^0-9a-f]*'),
    feature_identity_scheme TEXT COLLATE BINARY NOT NULL
        CHECK (feature_identity_scheme = 'aiq-feature-identity-v1'),
    feature_set_identity TEXT COLLATE BINARY NOT NULL
        CHECK (
            length(feature_set_identity) = 64
            AND feature_set_identity NOT GLOB '*[^0-9a-f]*'),
    feature_definition_identity TEXT COLLATE BINARY NOT NULL
        CHECK (
            length(feature_definition_identity) = 64
            AND feature_definition_identity NOT GLOB '*[^0-9a-f]*'),
    dataset_identity_scheme TEXT COLLATE BINARY NOT NULL
        CHECK (dataset_identity_scheme = 'aiq-dataset-identity-v1'),
    snapshot_identity TEXT COLLATE BINARY NOT NULL
        CHECK (
            length(snapshot_identity) = 64
            AND snapshot_identity NOT GLOB '*[^0-9a-f]*'),
    dataset_definition_identity TEXT COLLATE BINARY NOT NULL
        CHECK (
            length(dataset_definition_identity) = 64
            AND dataset_definition_identity NOT GLOB '*[^0-9a-f]*'),
    research_dataset_identity TEXT COLLATE BINARY NOT NULL
        CHECK (
            length(research_dataset_identity) = 64
            AND research_dataset_identity NOT GLOB '*[^0-9a-f]*'),
    source_state_identity TEXT COLLATE BINARY NOT NULL
        CHECK (
            length(source_state_identity) = 64
            AND source_state_identity NOT GLOB '*[^0-9a-f]*'),
    source_authority INTEGER NOT NULL
        CHECK (source_authority = 0),
    dataset_observation_count INTEGER NOT NULL
        CHECK (dataset_observation_count >= 0),
    summary_count INTEGER NOT NULL
        CHECK (summary_count >= 0),
    aggregates_present INTEGER NOT NULL
        CHECK (aggregates_present IN (0, 1)),
    arithmetic_mean_canonical TEXT COLLATE BINARY,
    minimum_canonical TEXT COLLATE BINARY,
    maximum_canonical TEXT COLLATE BINARY,
    PRIMARY KEY (experiment_result_identity),
    FOREIGN KEY (snapshot_identity) REFERENCES dataset_snapshots(snapshot_identity)
        ON UPDATE RESTRICT ON DELETE RESTRICT,
    CHECK (
        (summary_count = 0
            AND aggregates_present = 0
            AND arithmetic_mean_canonical IS NULL
            AND minimum_canonical IS NULL
            AND maximum_canonical IS NULL)
        OR
        (summary_count > 0
            AND aggregates_present = 1
            AND arithmetic_mean_canonical IS NOT NULL
            AND length(arithmetic_mean_canonical) >= 5
            AND arithmetic_mean_canonical NOT GLOB '*[^0-9,]*'
            AND minimum_canonical IS NOT NULL
            AND length(minimum_canonical) >= 5
            AND minimum_canonical NOT GLOB '*[^0-9,]*'
            AND maximum_canonical IS NOT NULL
            AND length(maximum_canonical) >= 5
            AND maximum_canonical NOT GLOB '*[^0-9,]*'))
) STRICT, WITHOUT ROWID;
```

WP07 may use `CREATE TABLE IF NOT EXISTS` within the existing bootstrap flow,
provided structural validation proves the table exactly matches this model
before `user_version` advances or an existing v3 database is accepted.

## Column contract

| Column | SQLite type | Null | Meaning |
| --- | --- | --- | --- |
| `experiment_result_identity` | `TEXT` | No | Exact Experiment Result fingerprint and primary lookup key |
| `experiment_identity_scheme` | `TEXT` | No | Constant `aiq-experiment-identity-v1` |
| `experiment_definition_name` | `TEXT` | No | Constant `simple-return-descriptive-summary-v1` |
| `experiment_definition_identity` | `TEXT` | No | Exact Experiment Definition fingerprint |
| `feature_identity_scheme` | `TEXT` | No | Constant `aiq-feature-identity-v1` |
| `feature_set_identity` | `TEXT` | No | Exact accepted Feature Set fingerprint |
| `feature_definition_identity` | `TEXT` | No | Exact built-in Feature Definition fingerprint |
| `dataset_identity_scheme` | `TEXT` | No | Constant `aiq-dataset-identity-v1` |
| `snapshot_identity` | `TEXT` | No | Exact Dataset Snapshot fingerprint and restrictive foreign key |
| `dataset_definition_identity` | `TEXT` | No | Exact Dataset Definition fingerprint |
| `research_dataset_identity` | `TEXT` | No | Exact Research Dataset fingerprint |
| `source_state_identity` | `TEXT` | No | Exact Source State fingerprint |
| `source_authority` | `INTEGER` | No | Accepted source-authority enum value `0` |
| `dataset_observation_count` | `INTEGER` | No | Non-negative predecessor observation count |
| `summary_count` | `INTEGER` | No | Non-negative Experiment summary count |
| `aggregates_present` | `INTEGER` | No | Boolean `0` or `1` controlling aggregate presence |
| `arithmetic_mean_canonical` | `TEXT` | Yes | Exact canonical decimal mean when present |
| `minimum_canonical` | `TEXT` | Yes | Exact canonical decimal minimum when present |
| `maximum_canonical` | `TEXT` | Yes | Exact canonical decimal maximum when present |

SQLite `INTEGER` is a signed 64-bit physical type. Infrastructure mapping must
use checked conversion to the accepted Application `int` count range on both
write and read, so the database does not silently narrow or overflow the
Application contract.

## Identity and definition representation

Every SHA-256 identity fingerprint is stored as exactly 64 lowercase
hexadecimal characters in `TEXT COLLATE BINARY`. Length and character-set
checks prevent case folding, whitespace, or alternative encodings. The scheme
columns remain explicit so retrieval can validate typed identity domains rather
than treating equal fingerprint text from different schemes as interchangeable.

The one built-in experiment definition is stored by its exact code-owned name
and exact definition fingerprint. There is no definitions table, alias,
configuration-selected version, or latest-version lookup.

`DatasetVersion` currently contains exactly its bound `DatasetSnapshotIdentity`.
The row therefore stores one `snapshot_identity`; retrieval reconstructs the
version from that same typed identity. A duplicate version column would add no
semantic evidence and could create an impossible disagreement.

## Decimal representation

Aggregate decimals follow the repository's invariant-`TEXT` precedent and
never use SQLite `REAL`. Their canonical physical form is exactly the Release
1.5 identity component `sign,coefficient,scale`:

- `sign` is `0` for non-negative and `1` for negative;
- `coefficient` is the unsigned 96-bit decimal coefficient rendered in base 10
  without leading zeros, except zero is `0`;
- `scale` is an invariant base-10 integer from `0` through `28`;
- trailing coefficient zeros are removed while scale is positive; and
- the sign is retained even when the coefficient is zero.

Examples are `0,125,2` for `1.25m` and `1,0,0` for a negative decimal zero.
Preserving signed zero is necessary because the accepted Release 1.5 identity
encoding includes the decimal sign bit even when numeric decimal equality would
consider both zeros equal.

WP07 write mapping must derive this triple from `decimal.GetBits` using the
same normalization as `ExperimentIdentityComputer`. WP08 read mapping must
parse only this grammar, reject coefficient overflow or scale outside `0..28`,
reconstruct the exact decimal bits, then regenerate the triple and require
ordinal equality with stored text. This rejects non-canonical spellings,
locale-specific forms, whitespace, binary floating-point approximations, and
values outside the .NET `decimal` domain without changing
`aiq-experiment-identity-v1`.

The table constrains presence, length, and the physical character alphabet.
Canonical grammar, numeric reconstruction, and semantic checks such as `minimum <= mean <=
maximum` belong to Infrastructure mapping/validation because SQLite cannot
compare canonical decimal text numerically without lossy coercion.

## Aggregate-presence invariants

The row-level check admits exactly two coherent states:

- empty success: `summary_count = 0`, `aggregates_present = 0`, and all three
  aggregate columns are `NULL`;
- non-empty success: `summary_count > 0`, `aggregates_present = 1`, and all
  three aggregate columns contain non-empty canonical decimal text.

Zero is never a sentinel for absence. Partial aggregate presence is rejected by
SQLite. An empty durable result is still a stored successful result and remains
distinct from an absent primary-key lookup.

## Provenance and lineage representation

The table stores the reduced immutable identity-bearing evidence accepted by
WP03/WP04 inline: Experiment Definition, Feature Set, Feature Definition,
Dataset Snapshot, Dataset Definition, Research Dataset, Source State, source
authority, and predecessor observation count. These values are sufficient to
reconstruct `DurableExperimentProvenance` and `DurableExperimentLineage`
without persisting Feature Values or raw observations.

The same definition/source identities serve both provenance and lineage because
the Application contract requires them to agree. Duplicate lineage columns
would create contradictory representations without adding information.

The snapshot identity also references the existing immutable
`dataset_snapshots` row. The foreign key uses `ON UPDATE RESTRICT ON DELETE
RESTRICT`: durable evidence cannot outlive, retarget, cascade-update, or
cascade-delete its accepted predecessor. Inline references remain necessary
because they are semantic evidence used for equivalence and contradiction
checks, while the foreign key proves the referenced durable snapshot exists.

Application and Infrastructure validation must compare the inline dataset
identities, source authority, and observation count with the referenced
snapshot evidence during acceptance and retrieval. SQLite checks row shape and
referential existence; it does not fabricate or repair semantic agreement.

## Keys, uniqueness, indexes, and concurrency

`experiment_result_identity` is the sole primary key. `WITHOUT ROWID` makes
that key the table's physical lookup key and prevents a separate row identity.
Its uniqueness guarantees at most one logical row for an Experiment Result
identity, including concurrent insert attempts.

No additional index is authorized. Exact lookup is served by the primary key;
Release 1.6 defines no snapshot, definition, aggregate, time, history, list,
search, or comparison query. WP07 resolves a uniqueness race by reading and
validating the winning row as equivalent or contradictory; it must not retry
generation, overwrite evidence, or create a second logical result.

## Immutability

The schema defines insert and exact read only. It contains no mutable status,
updated timestamp, revision, soft-delete marker, retention field, or cascading
action. Production persistence must not issue `UPDATE`, `DELETE`, or replacement
statements. An equivalent row is recognized without mutation; contradictory
evidence is an `IntegrityConflict`, never an overwrite.

## Schema-v2 to schema-v3 migration

The existing `SqliteSchemaBootstrapper` remains the single schema-management
owner. WP07 must extend it and the existing schema-definition convention rather
than create a parallel migration framework.

For a valid v2 database, one existing bootstrap transaction must:

1. enable foreign keys before the transaction;
2. read `PRAGMA user_version` and establish version `2`;
3. validate all v1/v2 tables and their constraints before mutation;
4. create `experiment_results` empty;
5. validate its exact columns, constraints, strict/without-rowid form, and
   foreign key;
6. set `PRAGMA user_version = 3` in the same transaction; and
7. commit once.

Migration performs no observation/snapshot rewrite, Experiment Result
backfill, Feature Set backfill, experiment generation, provider call, or
network access. Release 1.5 had no durable Experiment Result evidence to
backfill.

If any create, validation, or version-write operation fails, the transaction
must roll back. The database remains a valid v2 database with predecessor rows
unchanged, no accepted partial `experiment_results` object, and
`user_version = 2`. A later explicit open may attempt the same migration again;
there is no internal retry loop.

## Fresh and predecessor database initialization

A fresh version-zero database is initialized atomically with all accepted v1,
v2, and v3 objects, validates them, sets `user_version = 3`, and commits once.
A valid schema-v1 database follows the existing non-destructive v1-to-v2 path,
then creates and validates the v3 object in the same bootstrap transaction
before setting the final version to `3`.

Fresh-v3 and migrated-v3 databases have the same validated schema capability:
the predecessor tables are present and `experiment_results` is empty until an
explicit durable acceptance succeeds. No deferred Release 1.7 table exists.

Opening an already valid v3 database validates every v1/v2/v3 object and makes
no schema or data mutation. Structural mismatch fails before the connection is
returned.

## Unsupported versions and structural validation

Versions greater than `3`, negative versions, and unsupported gaps remain
rejected by the existing fail-stop bootstrap boundary. WP07 must update the
current unsupported-version test fixture from version `3` to a version beyond
the implemented current version; WP06 does not change that test.

Schema v3 recognition must extend the existing exact column-order/type/nullability/
primary-key validation, required SQL-fragment validation, and foreign-key
inspection. It must not trust `user_version = 3` alone. A missing or malformed
Experiment Result table, altered identity constraints, different referential
actions, or incompatible predecessor object remains a schema validation
failure.

## Connection and transaction ownership

The existing Infrastructure connection factory continues to open the
configured SQLite file and invoke the bootstrapper. Schema creation and
migration remain inside the bootstrapper's transaction. Experiment acceptance
later uses a separate Infrastructure-owned immediate write transaction;
Application contracts never own SQLite connections or transactions.

Service resolution remains side-effect free. Database opening, migration, and
durable acceptance occur only during an explicit operation in later work
packages.

## Failure-model support

This model allows later boundaries to distinguish:

- exact primary-key absence as `NotFound`;
- storage/open/migration inability as `DependencyUnavailable` where governed;
- malformed or unreconstructable row evidence as `InvalidEvidence`;
- same-identity contradictory evidence as `IntegrityConflict`; and
- unknown programming defects, which continue to propagate.

WP06 creates no new public failure and performs no exception translation.
WP09 owns final storage-specific precedence and mapping.

## Security and data minimization

The table stores only reduced semantic evidence required for exact identity,
summary, provenance, lineage, equivalence, and retrieval. It excludes Feature
Values, raw observations, provider payloads, credentials, API keys, connection
strings, database paths, process/machine identifiers, operational timestamps,
logs, run history, and telemetry.

## Explicit exclusions

Schema v3 contains no Feature Set or Feature Value persistence, experiment
definition registry, experiment run/history, comparison/search, audit/event,
retry/checkpoint, provider, strategy, signal, backtest, workspace, UI/API,
AI/ML, or Release 1.7 object. It adds no update, delete, retention, repair,
fallback, or generalized repository semantics.

## WP07 and WP08 handoff

WP07 owns the future implementation under the existing Infrastructure SQLite
area. It is expected to name and reconcile the exact implementation paths for:

- one schema-definition artifact following `SqliteDatasetSchema.cs`;
- the necessary extension of `SqliteSchemaBootstrapper.cs`;
- one immutable SQLite Experiment Result record/mapping boundary; and
- one store implementing `IDurableExperimentEvidenceStore.Accept`.

WP08 owns exact primary-key retrieval and immutable reconstruction through the
same store contract. WP07/WP08 must preserve this physical model, the WP02/WP03
semantics, the WP04 contracts, and WP05's explicit orchestration without
changing packages, projects, references, connection ownership, or the
production dependency graph.
