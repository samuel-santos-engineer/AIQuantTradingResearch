# Durable Experiment Evidence Discovery Semantics

## Purpose

This document freezes the Release 1.7 semantic contract for discovering
already-accepted durable Experiment Result evidence. Discovery answers one
bounded question for one exact research context without regenerating,
accepting, modifying, or repairing evidence.

This is a semantic authority only. It does not define Application contract
shapes, a use case, SQL, physical access, dependency registration, Worker
routing, or permanent tests.

## Authoritative predecessor

Release 1.6 remains authoritative for durable Experiment Result identity,
evidence, provenance, lineage, exact retrieval, acceptance outcomes,
immutability, decimal fidelity, and bounded failures. In particular:

- `aiq-experiment-identity-v1` remains the sole Experiment identity scheme;
- durable evidence remains bound to its exact Dataset Snapshot, Dataset
  Version, Experiment Definition, Feature Set, provenance, and lineage;
- successful empty and non-empty Experiment Results retain their accepted
  meaning; and
- discovery introduces no new durable, query, search, registry, cursor, or
  history identity.

## Discovery question

The only accepted question is:

> Which accepted durable Experiment Results exist for this exact Dataset
> Snapshot Identity and this exact Experiment Definition Identity, up to the
> caller-supplied positive bounded maximum?

Both identities are mandatory and both participate in the semantic predicate.
Matching is exact. Neither identity may be omitted, wildcarded,
prefix-matched, broadened, inferred from another field, or normalized into a
different identity.

Discovery does not redefine Experiment Result identity. Each returned item
retains its existing typed identity under `aiq-experiment-identity-v1`.

## Bounded cardinality

Every request supplies a maximum result count. The maximum is mandatory,
finite, positive, and subject to a deterministic supported upper bound.

- `maximum > 0` is required;
- returned cardinality is from zero through `maximum`, inclusive;
- no unbounded all-results operation exists;
- no implicit default is part of these semantics; and
- a value above the supported upper bound is invalid rather than silently
  becoming unbounded.

Release 1.7 planning intentionally does not freeze a numeric ceiling. A later
authorized Application boundary may place the deterministic supported upper
bound, but it must not reinterpret the semantics above.

## Deterministic ordering and truncation

Returned durable evidence is ordered by Experiment Result Identity fingerprint
in ascending ordinal order. This is a total deterministic order for the
bounded result.

The maximum is applied consistently to that order. Storage enumeration order,
row identifier, insertion or acceptance time, Snapshot order, Feature Set
identity, aggregate values, process, and database path are not discovery-order
semantics.

This ordering rule does not authorize an index or other physical change.
WP06 must prove the access pattern over schema v3, and WP07 owns any later
authorized read-only implementation.

## Successful empty collection

A valid discovery request with no matching durable Experiment Results succeeds
with an empty immutable collection.

Empty discovery:

- is not `NotFound`;
- does not call a provider or network dependency;
- does not generate an Experiment Result or Feature Set;
- does not accept or persist evidence;
- does not fabricate a placeholder item; and
- does not fall back to a broader query.

This differs from Release 1.6 exact single-result retrieval. That operation
requires one known Experiment Result Identity, so absence may be `NotFound`.
Collection discovery asks which matching results exist; zero is a complete
successful answer.

## Evidence fidelity

Every returned item is existing immutable Release 1.6 durable Experiment
Result evidence. Discovery preserves, without recomputation or reinterpretation:

- Experiment Result Identity;
- Snapshot Identity and Snapshot Version binding;
- Experiment Definition identity, version, and accepted definition evidence;
- exact Feature Set identity and its provenance;
- required Dataset, Source State, Feature, Experiment, provenance, and lineage
  references;
- count and aggregate-presence state;
- arithmetic mean, minimum, and maximum when present;
- canonical decimal value semantics, including signed-zero behavior; and
- successful empty versus non-empty evidence fidelity.

Discovery cannot collapse semantically distinct results, substitute a latest
identity, regenerate predecessor evidence, repair malformed evidence, or expose
partial evidence. Detailed identity framing, canonical encoding, provenance,
and reconstruction invariants remain owned by the accepted predecessor
authorities and WP03.

## Failure semantics

Discovery reuses the existing bounded Release 1.6 vocabulary:

1. `InvalidRequest`: mandatory query intent is missing or incoherent, or the
   maximum is non-positive or outside the supported bound.
2. `DependencyUnavailable`: the persistence dependency cannot serve the
   read-only query.
3. `InvalidEvidence`: matched durable evidence cannot be reconstructed as
   complete accepted evidence.
4. `IntegrityConflict`: matched evidence contradicts its accepted canonical
   identity or evidence invariants.

`NotFound` is not the outcome for a valid discovery query with zero matches.
It remains available to exact single-object operations such as Release 1.6
exact retrieval.

The first established governed failure stops the operation. Unknown defects
propagate. There is no retry, recovery, repair, fallback, provider
substitution, partial success, or broad exception normalization.

## Read-only guarantee

Discovery performs no semantic write or generation. It must not insert,
update, delete, accept, overwrite, migrate, create an index, create a registry
or history record, generate an Experiment or Feature Set, call a market-data
provider, call a product network dependency, or mutate Worker state.

The observable semantic result is only the bounded immutable collection or one
of the governed failures.

## Ownership and downstream boundaries

- WP02 freezes behavior only; it adds no production contract or code.
- WP03 owns detailed identity, provenance, lineage, equivalence, and fidelity
  formalization without creating a discovery identity.
- WP04 and WP05 own storage-independent Application contracts and orchestration.
- WP06 proves the schema-v3 physical access pattern. If a table, column, index,
  migration, or other structural mutation is required, WP06 must stop for
  separate authority.
- WP07 may implement the accepted bounded read-only SQLite query only after
  WP06 accepts schema-v3 access.
- Later WPs own failure mapping, composition, Worker execution, permanent
  tests, documentation alignment, and final acceptance.

WP02 chooses no SQL, index, query plan, numeric ceiling, configuration name,
public type, DI lifetime, Worker output, or process behavior.

## Process-validation prerequisite preservation

Later process validation must use the already-resolved repository-native
fixture: Infrastructure test-host `TemporaryDatabase`, deterministic
`DatasetSnapshotCandidate`, `SqliteDatasetSnapshotStore.Store(...)`, production
durable acceptance for Experiment Result seeding, the existing `--no-build`
Worker runner, current friend-assembly visibility, deterministic expected
evidence, and complete process/database/WAL/SHM/journal/output/temp cleanup.

WP02 neither executes nor changes this mechanism.

## Explicit exclusions

Release 1.7 discovery is not generalized search, registry, history, list-all,
pagination, cursoring, comparison, tagging, retention, mutation, deletion,
repair, scheduling, background processing, provider acquisition, network
fallback, Experiment generation, Feature generation, Feature Set persistence,
strategy, signal, backtesting, portfolio analysis, or Release 1.8 work.

Schema remains v3. WP02 authorizes no table, column, index, migration, package,
project, reference, production dependency edge, Worker change, or permanent
test.
