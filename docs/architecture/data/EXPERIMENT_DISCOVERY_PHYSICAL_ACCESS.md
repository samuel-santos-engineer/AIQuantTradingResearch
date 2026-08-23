# Experiment Discovery Physical Access

## Decision

Release 1.7 durable Experiment evidence discovery is accepted on the existing
SQLite schema v3 without a table, column, index, key, collation, or migration
change. The existing `experiment_results` representation supports the frozen
exact predicate, deterministic result-identity order, parameterized positive
maximum, successful empty result, and complete evidence reconstruction.

The observed plan scans `experiment_results` in its primary-key order. That
plan is semantically correct and acceptable for the bounded Release 1.7 scope.
It is not a claim of optimal performance for a future large registry or
history workload.

## Frozen logical access

Discovery has exactly three inputs:

1. exact Dataset Snapshot Identity;
2. exact Experiment Definition Identity; and
3. a caller-supplied positive maximum result count.

The storage-independent Application boundary owns request validity. Physical
access adds no default, clamp, numeric ceiling, wildcard, pagination, or
additional predicate.

The proved query shape is:

```sql
SELECT
    experiment_result_identity,
    experiment_identity_scheme,
    experiment_definition_name,
    experiment_definition_identity,
    feature_identity_scheme,
    feature_set_identity,
    feature_definition_identity,
    dataset_identity_scheme,
    snapshot_identity,
    dataset_definition_identity,
    research_dataset_identity,
    source_state_identity,
    source_authority,
    dataset_observation_count,
    summary_count,
    aggregates_present,
    arithmetic_mean_canonical,
    minimum_canonical,
    maximum_canonical
FROM experiment_results
WHERE snapshot_identity = $snapshotIdentity
  AND experiment_definition_identity = $definitionIdentity
ORDER BY experiment_result_identity COLLATE BINARY ASC
LIMIT $maximumResultCount;
```

All three inputs are bound SQLite parameters. No request value is concatenated
into SQL. `LIMIT` bounds returned cardinality directly and does not reinterpret
zero as unbounded; invalid non-positive intent remains an Application failure.

## Existing schema-v3 facts

`experiment_results` is a `STRICT`, `WITHOUT ROWID` table. Its
`experiment_result_identity` is a 64-character lowercase hexadecimal `TEXT`
value with `BINARY` collation and is the primary key. SQLite exposes the
existing primary-key autoindex as `sqlite_autoindex_experiment_results_1`;
there is no secondary discovery index.

The exact predicate columns already exist as binary-collated text:

- `snapshot_identity` retains the Dataset Snapshot provenance reference and
  has a restrictive foreign key to `dataset_snapshots(snapshot_identity)`;
- `experiment_definition_identity` retains the exact Experiment Definition
  identity; and
- `experiment_result_identity` is the accepted ordering key.

The remaining selected columns retain the accepted identity schemes,
definition name, Feature Set and Feature Definition identities, Dataset and
Source provenance, observation count, summary count, aggregate-presence bit,
and canonical decimal aggregates. These are the complete inputs already used
by the schema-v3 mapper to reconstruct `DurableExperimentEvidence`.

Schema initialization validates the table columns, required constraints,
foreign key, and schema version before use. `PRAGMA user_version` remained
`3` throughout the proof.

## Repository-native proof fixture

The disposable proof ran inside `AIQuantTradingResearch.Infrastructure.Tests`
through the existing friend-assembly boundary. It used:

- an isolated temporary database and `SqliteConnectionFactory`;
- valid `DatasetSnapshotCandidate` instances persisted through
  `SqliteDatasetSnapshotStore.Store(...)`;
- the existing Application Experiment generation path to create valid
  Experiment evidence; and
- production `SqliteExperimentResultStore.Accept(...)` to persist durable
  Experiment Result evidence.

Three snapshots with different identities and evidence were accepted in
descending Experiment Result Identity order. Their physical ascending order
was then observed as:

1. `316871df77db18d7cee308f24234e8ca90a00e8eb624f9ac1009a7c7b04877a2`;
2. `48d59f551f13f22e9b3749a5917a96486945e08ccbfd39dae1033191200569ad`;
3. `5bf36d300e7bd73fa8b462c56d60adf76654ad8b19ecc222575c832280089732`.

This distinguished explicit binary identity ordering from insertion order.
The target proof used Snapshot Identity
`aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa`, the
accepted built-in Experiment Definition Identity
`8c0a7d0cf813ca6a01d91f73d38dd2934f2f4d237ebe2c8794eb9d4d81e9c7c7`,
and maximum `1`.

The current built-in Experiment Definition and deterministic Feature Set
model yield one valid Experiment Result for one exact Snapshot-and-Definition
pair. The proof therefore did not fabricate logically impossible duplicate
matching results. The parameterized query remains cardinality-safe if later
accepted evidence permits more than one match, but no future semantic
expansion is authorized here.

## Correctness matrix

| Gate | Evidence | Result |
| --- | --- | --- |
| Q1 Exact match | The target pair returned only its accepted Result Identity. | PASS |
| Q2 Snapshot isolation | Rows for two other Snapshot Identities were excluded. | PASS |
| Q3 Definition isolation | The same Snapshot queried with a distinct valid-shaped Definition fingerprint returned zero rows. The current model permits only the one built-in accepted definition, so no invalid durable row was fabricated. | PASS |
| Q4 Deterministic ordering | Descending insertion produced ascending binary Result Identity enumeration; the frozen query uses explicit `ORDER BY ... COLLATE BINARY ASC`. | PASS |
| Q5 Bounded maximum | `$maximumResultCount = 1` returned at most one row. The limit was parameterized with no clamp, default, or invented ceiling. | PASS |
| Q6 Empty discovery | A valid-shaped exact pair with no durable match returned zero rows without failure. | PASS |
| Q7 Read only | Query-only mode was enabled and snapshot, observation, result counts, and the ordinal serialization of complete persisted row evidence were identical before and after discovery. | PASS |
| Q8 Evidence fidelity | All 19 durable columns were selected and the existing mapper reconstructed evidence equal to the accepted source evidence. | PASS |

## Query-plan interpretation

`EXPLAIN QUERY PLAN` reported:

```text
SCAN experiment_results
```

Because `experiment_results` is `WITHOUT ROWID` and keyed by binary
`experiment_result_identity`, the scan supplies the required identity order;
SQLite reported no temporary B-tree sort. The Snapshot and Definition
predicates are evaluated during the scan. The parameterized limit can stop
after the requested number of matches, while a sparse or empty query may scan
through the table.

The absence of a composite predicate index is a performance characteristic,
not a correctness defect for this bounded foundation. Release 1.7 defines no
enterprise-scale throughput target, registry, history, or list-all workload.
Measured future scale needs may justify a separately governed index change;
they do not authorize one here.

## Structural stop-gate conclusion

No correctness or acceptance requirement needs a new table, column, index,
key, migration, schema version, or identity representation. Schema v3 is
accepted for Release 1.7 discovery.

WP07 may implement only the proved read-only query and existing evidence
reconstruction boundary. This decision does not implement Infrastructure
discovery, failure mapping, DI, Worker behavior, provider access, retry,
repair, fallback, registry/history, or Release 1.8 behavior.
