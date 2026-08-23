# Experiment Discovery Identity, Provenance, and Fidelity

## Purpose

This document freezes the Release 1.7 identity, provenance, and fidelity
invariants for bounded discovery of already accepted durable Experiment Result
evidence. It extends neither the Release 1.5 identity algorithm nor Release
1.6 durable evidence semantics.

Discovery selects existing immutable evidence. It does not create, recompute,
reinterpret, repair, or persist evidence.

## Identity preservation

Each discovered item retains its existing typed Experiment Result Identity
under `aiq-experiment-identity-v1`.

- discovery creates no `aiq-discovery-*` scheme or equivalent identity;
- it does not hash the query, collection, ordering, or maximum into an item
  identity;
- collection membership, sorting, and truncation do not alter item identity;
- repeated discovery of unchanged durable evidence returns the same Experiment
  Result Identity; and
- the accepted Release 1.5 canonical encoding, decimal canonicalization,
  domain separation, framing, and SHA-256 computation remain unchanged.

No registry key, history key, cursor, page identity, or stored discovery
identity is introduced.

## Query dimensions are not evidence identity

The discovery predicate contains exactly two mandatory dimensions:

1. exact Dataset Snapshot Identity; and
2. exact Experiment Definition Identity.

The pair is a request predicate only. It is not a new durable identity, a
collection identity, a replacement for Experiment Result Identity, a registry
key, or a persisted search token. It selects pre-existing result identities
whose stored provenance binds to both exact values.

The returned collection is ordered by existing Experiment Result Identity
ascending. Ordering and the caller's bounded maximum select which complete
items are returned; they do not change item identity or evidence.

## Snapshot provenance and version binding

Every discovered item preserves the exact Snapshot provenance accepted by
Release 1.6:

- its typed Snapshot Identity;
- its Dataset Version bound to that same Snapshot Identity;
- Dataset Definition, Research Dataset, Source State, source authority, and
  predecessor observation-count evidence where represented by the durable
  model; and
- the accepted one-way lineage from source state through Dataset Snapshot to
  Feature Set and Experiment Result.

The predicate matches the stored Snapshot Identity exactly. Discovery cannot
substitute a similar snapshot, resolve a latest version, infer provenance from
external state, recompute the Snapshot identity, or reacquire provider data.

## Experiment Definition and Feature Set provenance

Each result preserves its exact Experiment Definition Identity and accepted
built-in definition binding. Discovery cannot alias, substitute, regenerate,
or resolve a latest definition.

The result also preserves its exact Feature Set Identity, Feature Definition
Identity, and accepted Feature Set provenance and lineage. Feature Set
provenance is returned evidence fidelity, not an additional query dimension.
Numerically similar summaries with different Feature Set identities remain
semantically distinct. Discovery never regenerates or invents a replacement
Feature Set.

## Canonical numeric and aggregate fidelity

Discovered evidence preserves the accepted Release 1.6 summary exactly:

- count remains the same non-negative semantic integer;
- aggregate presence remains explicit;
- present mean, minimum, and maximum retain their exact `decimal` values;
- Release 1.5 canonical decimal representation and signed-zero behavior remain
  authoritative; and
- no value is converted through floating point, rounded, localized, clamped,
  recomputed, or inferred.

Discovery does not choose physical decimal storage, parsing, canonicalization,
or validation mechanics. Those remain accepted predecessor behavior and later
Infrastructure authority.

## Empty-result distinctions

Two different successful states must remain distinguishable:

1. An **empty Experiment Result** is one durable result whose summary count is
   zero. Its Experiment Result, Snapshot, Definition, Feature Set, provenance,
   and lineage identities remain present and exact; mean, minimum, and maximum
   are all absent.
2. An **empty discovery collection** is a valid exact Snapshot-and-Definition
   query matching no durable Experiment Results.

Neither state is `NotFound`. The first is one complete immutable durable item;
the second contains no item. Absent aggregates must never become zero-valued
aggregates, and an empty collection must never fabricate an empty Experiment
Result.

## Immutable reconstruction and equivalence

Every returned item represents complete immutable durable evidence. Fidelity
requires the exact agreement of:

- Experiment Result Identity;
- Experiment Definition identity and binding;
- Feature Set and Feature Definition identities;
- Snapshot identity/version and required Dataset/Source provenance;
- required lineage references;
- count and aggregate-presence state; and
- every present canonical decimal aggregate.

Identity equality alone, aggregate equality alone, or storage-row equality
alone is insufficient. Discovery must not expose partial evidence, rewrite
provenance, normalize values, overwrite data, accept a result, delete a row,
or repair malformed durable evidence.

If a later boundary establishes malformed reconstructed evidence, the existing
`InvalidEvidence` meaning applies. Contradictory accepted evidence remains
`IntegrityConflict`; storage unavailability remains `DependencyUnavailable`;
invalid discovery intent remains `InvalidRequest`; unknown defects propagate.
A valid zero-match collection is success, not `NotFound`.

## Deterministic collection fidelity

The Release 1.7 collection rule is Experiment Result Identity ascending with a
positive bounded maximum. Truncation is applied after that deterministic order
and never mutates or weakens the selected items' identity, provenance, lineage,
numeric representation, or empty/non-empty semantics.

Repeated reads over unchanged durable state are semantically stable. Discovery
introduces no cursor, pagination identity, search history, registry, or
operational evidence.

## Compatibility and boundaries

The existing Release 1.6 durable model is semantically sufficient: it already
contains the identity-bearing and summary-preserving evidence required above.
Schema v3 remains unchanged at this semantic boundary.

WP03 creates no Application request/result contract, interface, use case,
validation implementation, SQL, query plan, index decision, SQLite API,
schema migration, DI registration, Worker configuration, or permanent test.

- WP04 owns storage-independent Application discovery contracts.
- WP06 remains the physical access-pattern and structural-schema stop gate.
- WP07 owns later SQLite read-only discovery implementation.

No Release 1.8 capability, provider access, generation, mutation, registry,
history, pagination, scheduling, or backtesting behavior is implied.
