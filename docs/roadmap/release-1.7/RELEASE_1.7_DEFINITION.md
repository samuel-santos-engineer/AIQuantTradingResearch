# Release 1.7 Definition

## Status and identity

Planning candidate awaiting human acceptance; not implementation or GitHub-planning authority.

- Release: **Phase 4 - Release 1.7: Durable Experiment Evidence Discovery**
- Baseline: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`
- Objective: discover already accepted durable Experiment Result evidence for one exact research context without regenerating or mutating evidence.
- Value: researchers can determine which accepted results exist without already knowing their result identities or querying SQLite directly.

## Evidence and continuity

The accepted progression is Release 1.1 historical-observation persistence, Release 1.2 immutable Dataset Snapshots/catalog evidence, Release 1.3 deterministic five-stage pipeline, Release 1.4 deterministic Feature Sets, Release 1.5 deterministic Experiment Results, and Release 1.6 durable exact Experiment Result acceptance/retrieval.

Release 1.6 explicitly deferred registry/history/search while leaving a narrower gap: exact retrieval requires the caller to know `ExperimentResultIdentity`. Bounded semantic discovery is the smallest coherent next vertical slice. It is not a registry or history platform.

## Selected discovery question

> Which accepted durable Experiment Results exist for this exact Dataset Snapshot and Experiment Definition?

The request contains a typed Dataset Snapshot Identity, typed Experiment Definition Identity, and positive maximum-result count bounded by an Application-owned constant. The store matches both identities exactly using canonical values already persisted in `experiment_results`. Results are ordered by `ExperimentResultIdentity` fingerprint ascending and truncated to the bound. A valid query with no matches succeeds with an empty immutable result. Returned items are complete Release 1.6 durable evidence, not storage identifiers or regenerated object graphs.

No pagination token, offset, free-text filter, date search, list-all operation, comparison, tagging, or arbitrary query language is introduced.

## In scope

- provider- and storage-independent discovery contracts;
- Application validation and one-call discovery orchestration;
- read-only SQLite discovery over `experiment_results`;
- exact identity/provenance/fidelity reconstruction;
- deterministic ordering, bounded cardinality, and successful empty results;
- accepted bounded failure mapping;
- exactly-once DI registration;
- one explicit one-shot Worker discovery mode;
- deterministic Application and Infrastructure/process tests;
- architecture/current-state documentation alignment;
- final validation and separately authorized Git integration.

## Explicitly out of scope

- Experiment generation or acceptance changes;
- mutation, overwrite, delete, repair, or backfill;
- Feature Value or Feature Set persistence;
- generalized Experiment registry, run/audit history, search, comparison, tags, or retention;
- new experiment or feature families;
- provider acquisition/fallback or real credentials;
- retries, recovery, scheduling, recurrence, checkpoints, or DAGs;
- strategy, signal, backtest, portfolio, risk, UI/API/workspace, AI/ML, or Release 1.8 work.

## Predecessor semantics

Release 1.7 preserves Releases 1.1-1.6 unchanged, including `aiq-dataset-identity-v1`, `aiq-feature-identity-v1`, `aiq-experiment-identity-v1`, immutable evidence, exact snapshot/version/definition/Feature Set/provenance/lineage binding, decimal canonicalization, signed-zero and count/presence fidelity, acceptance dispositions, exact retrieval, and Durable Experiment -> Experiment -> Feature -> pipeline routing.

Discovery creates no new semantic identity. Query parameters and ordering are request semantics, not identity inputs.

## Persistence and schema decision

SQLite remains **schema v3**. `experiment_results` already persists `snapshot_identity`, `experiment_definition_identity`, result identity, and complete reconstructable evidence. The bounded predicate and primary-key ordering need no new table, column, semantic key, or index. WP06 must confirm this with the actual schema and representative `EXPLAIN QUERY PLAN`. Any discovered structural need blocks and requires corrective schema authority; it is not silently absorbed.

## Failure model

Reuse exactly:

- `InvalidRequest` for malformed identity or bound;
- `DependencyUnavailable` when storage cannot serve the query;
- `InvalidEvidence` when a matched row cannot reconstruct accepted evidence;
- `IntegrityConflict` for canonical identity/evidence contradiction;
- `NotFound` only for exact single-result retrieval, not collection discovery.

Empty valid discovery succeeds. Unknown defects propagate. There is no retry, repair, fallback, partial success, or fabricated evidence.

## Ownership and routing

- Domain: no expected production change.
- Application: request/result contracts, validation, bounded abstraction, and use case.
- Infrastructure: SQLite predicate/order/reconstruction and storage classification.
- Worker: explicit configuration, intent/routing, one invocation, bounded presentation, and exit code.

The graph remains Domain -> none, Application -> Domain, Infrastructure -> Application, Worker -> Application and Infrastructure, with zero cycles.

Worker exposure is required for a demonstrable vertical slice. Complete Durable Discovery intent precedes Durable Experiment, Experiment, Feature, and pipeline. Partial/malformed discovery intent fails without fallback. Valid execution invokes the use case exactly once and terminates.

## Process-level validation prerequisites

Process WPs must use the established Infrastructure test-host pattern:

1. `TemporaryDatabase` creates an isolated path and `SqliteConnectionFactory`.
2. `DatasetSnapshotCandidate` and `SqliteDatasetSnapshotStore.Store(...)` seed the exact Snapshot prerequisite.
3. The production Release 1.6 durable use-case/store acceptance path creates `experiment_results`; direct SQL seeding is forbidden.
4. Existing Infrastructure process helpers launch the Release Worker with `--no-build` and explicit environment configuration.
5. Synthetic identities/decimal observations prove zero/one/multiple, order, bound, empty, malformed, unavailable, and no-fallback evidence.
6. Existing friend-assembly access is retained; production visibility is unchanged.
7. Processes, output, database, WAL/SHM/journal files, and temporary directories are bounded and removed by fixture disposal/finally blocks.

Provider calls, external product-network calls, and real credentials remain zero.

## Test and architecture strategy

- Domain.Tests: zero delta expected.
- Application.Tests: validation, one-call orchestration, bounds/order contract, empty/non-empty, failures, and unknown defects with hand-written doubles.
- Infrastructure.Tests: schema-v3 read-only discovery, reconstruction, order/bounds, restart, failures, DI, Worker routing/process behavior, isolation, and cleanup.
- Architecture.Tests: zero-delta-first; add only a new stable non-redundant repository-wide rule.

Exact final counts are not frozen. All 250 predecessor tests remain mandatory.

## Documentation and acceptance

Only manifest-authorized current-state documents may be aligned after implementation. Release 1.7 completes only when a clean offline process discovers bounded, ordered, already accepted evidence for the exact context without regeneration, mutation, provider activity, schema change, or residue, with all predecessor regressions passing.
