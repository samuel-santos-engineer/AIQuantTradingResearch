# Durable Experiment Evidence Semantics

## Purpose

This document freezes the Release 1.6 semantic boundary for making one
accepted Release 1.5 Experiment Result durable. It defines the evidence,
outcomes, invariants, ownership, and failure distinctions that later work must
preserve. It does not define Application contract shapes, SQLite records,
schema-v3 tables or migration statements, dependency registration, Worker
configuration, or implementation mechanics.

## Authoritative predecessor

Release 1.5 remains authoritative for:

- `simple-return-descriptive-summary-v1`;
- `aiq-experiment-identity-v1`;
- immutable Experiment Definition and Experiment Result identities;
- exact Feature Set binding;
- count and optional mean, minimum, and maximum evidence;
- successful empty-result semantics;
- decimal determinism;
- provenance and acyclic lineage;
- validation precedence and unknown-defect propagation; and
- explicit one-shot, in-memory Experiment execution.

Release 1.6 adds durability as a separate explicit boundary. It does not
redefine or implicitly persist ordinary Release 1.5 Experiment generation.

## Durable semantic object

The only eligible durable object is a complete, successful, already-valid
Release 1.5 Experiment Result for
`simple-return-descriptive-summary-v1`. Durability receives accepted evidence;
it does not generate, recompute, repair, normalize, or reinterpret a result.

The durable semantic evidence must preserve enough accepted information to
reconstruct an equivalent immutable result:

- the typed Experiment Result Identity;
- the typed Experiment Definition Identity and built-in definition;
- the exact Feature Set Identity;
- the Feature Definition Identity represented by Feature Set provenance;
- the exact Dataset Snapshot Identity and Dataset Version;
- the accepted predecessor provenance and lineage identity references required
  to validate the result's acyclic evidence chain;
- summary count;
- aggregate-presence state; and
- exact arithmetic mean, minimum, and maximum when aggregates are present.

This boundary does not persist Feature Values, raw observations, provider
payloads, credentials, operational run records, or invocation metadata.
Feature Set values remain deterministic, immutable predecessor evidence bound
by their accepted identity.

## Generation and durability

Experiment generation and durability are distinct operations:

```text
Release 1.5 generation
  -> complete accepted Experiment Result
  -> explicit Release 1.6 durable acceptance
  -> NewlyAccepted | EquivalentExisting | bounded failure
```

Ordinary Experiment generation remains in memory and side-effect free. A
result becomes durable only through the separately governed durable boundary.
Durability never becomes a hidden consequence of the existing Experiment,
Feature, or pipeline modes.

## Identity

`aiq-experiment-identity-v1` remains the sole semantic identity authority.
Release 1.6 introduces no persistence identity, storage identity, run identity,
or replacement identity.

The exact lookup key is the typed Experiment Result Identity. The durable and
retrieved identities must equal the accepted in-memory identity. Database row
keys, insertion times, paths, connections, processes, machines, and persistence
dispositions are operational facts and cannot affect identity or equivalence.

Persistence cannot assign another identity to contradictory evidence or repair
evidence until it happens to match an identity. WP03 remains authoritative for
the exact persistence fidelity and equivalence mechanics and must reuse, not
redefine, the Release 1.5 canonical encoding.

## Evidence fidelity

Durable acceptance and retrieval must round-trip every required semantic fact
without loss. Equality of summary statistics alone is insufficient: exact
identity, definition, Feature Set binding, snapshot/version binding,
provenance, lineage, count, presence, and aggregate evidence all participate
in durable equivalence as applicable.

The durable representation may omit recomputable predecessor value collections
only when their accepted typed identities and required provenance/lineage
references preserve the complete durable Experiment Result meaning. It must
never fabricate missing semantic evidence. WP03 freezes the exact required
field set; later contracts and storage implement it.

## Empty-result fidelity

An accepted empty Experiment Result is successful durable evidence:

- count is exactly zero;
- mean, minimum, and maximum are absent;
- absence remains absence after restart and retrieval;
- absence cannot become zero, an empty string, a sentinel, or a fabricated
  numeric value;
- Experiment Result and Feature Set identities remain exact; and
- empty success is distinct from `NotFound`.

No storage-specific sentinel representation is authorized here.

## Non-empty fidelity

For a non-empty accepted result:

- count is preserved exactly;
- mean, minimum, and maximum are all present and preserved exactly;
- decimal semantics remain exact;
- binary floating-point conversion is semantically prohibited;
- culture, locale, timezone, process, and machine cannot change evidence; and
- retrieval reconstructs evidence equivalent to the accepted result under the
  same Experiment Result Identity.

Physical decimal encoding is deferred to the persistence fidelity and schema
authorities.

## Durable acceptance outcomes

### NewlyAccepted

`NewlyAccepted` is successful when no durable evidence exists under the exact
Experiment Result Identity and one complete accepted result is atomically made
durable.

- no partial result is observable;
- identity remains unchanged;
- the result is retrievable after restart;
- no Feature Set persistence is created; and
- no registry, history, search, or lifecycle behavior is implied.

### EquivalentExisting

`EquivalentExisting` is successful when durable evidence already exists under
the exact identity and is semantically equivalent to the candidate accepted
result.

- no duplicate logical result is created;
- no overwrite or update is required;
- no new identity is generated;
- equivalent repeat acceptance is idempotent and restart-safe; and
- equivalence covers all required evidence, not aggregates alone.

### IntegrityConflict

`IntegrityConflict` occurs when the same exact Experiment Result Identity is
associated with contradictory semantic evidence or evidence that violates the
accepted identity/evidence invariants.

The operation must not overwrite, merge, repair, delete, partially accept,
assign a replacement identity, report `EquivalentExisting`, or invoke provider
fallback. Unknown corruption or programming defects outside the bounded model
continue to propagate.

## Exact lookup

Retrieval is an exact lookup by typed Experiment Result Identity. It supports
only exact match and returns at most one semantically equivalent immutable
result evidence object.

It does not provide fuzzy, definition-only, Feature Set-only, latest, list,
range, history, comparison, search, or registry queries. Absence of the exact
identity is `NotFound`; an existing successful empty result is not.

## Restart safety

After `NewlyAccepted` completes and the original process and connection are
disposed normally, an independent later process or context using the same
durable store can retrieve equivalent evidence by the exact identity.

Restart safety cannot depend on an in-memory cache, retained DI scope, retained
process state, provider access, network access, Experiment recomputation, or
Feature Set persistence.

## Atomicity

A durable acceptance exposes either:

- one complete accepted Experiment Result evidence unit; or
- no newly accepted Experiment Result evidence.

Failure cannot expose a partial identity, partial summary, partial provenance,
or partial lineage. Transaction APIs and SQLite statements are deferred to
WP06 and WP07.

## Provenance and lineage

Durability preserves the accepted one-way evidence chain:

```text
source state
  -> dataset definition / research dataset
  -> dataset snapshot / version
  -> feature definition / feature set
  -> experiment definition / experiment result
```

Storage is a boundary around the accepted result, not a new research
transformation or semantic ancestor. It cannot rewrite predecessor identities,
introduce a lineage cycle, or make storage metadata part of provenance.

## Immutability

Accepted durable Experiment Results are immutable. Release 1.6 defines no
update, overwrite, replacement, correction-in-place, mutable status, delete,
retention, or archival semantics. Idempotent equivalent acceptance does not
mutate semantic evidence.

## Bounded failures

The semantic durability boundary distinguishes:

1. `InvalidRequest`: the durable acceptance or lookup request is missing or
   incoherent;
2. `NotFound`: exact identity lookup has no durable result;
3. `DependencyUnavailable`: the durable store cannot serve the operation;
4. `InvalidEvidence`: evidence crossing or reconstructed from the boundary is
   malformed or incomplete; and
5. `IntegrityConflict`: the same identity is associated with contradictory
   semantic evidence.

The first established governed failure stops the operation. There is no retry,
repair, fallback, overwrite, partial success, or broad exception normalization.
Unknown programming defects propagate.

## Ownership

- **Domain:** no Release 1.6 production change is expected.
- **Application:** owns durable use-case semantics, storage-independent
  contracts, validation expectations, outcomes, failures, orchestration, and
  identity/evidence requirements.
- **Infrastructure:** later owns SQLite schema/migration, transactions,
  physical representation, exact persistence/retrieval, and storage-specific
  failure translation without redefining semantics.
- **Worker:** later owns explicit durable-mode request construction, one-shot
  invocation, bounded presentation, and deterministic exit behavior.

The production graph remains Domain to none, Application to Domain,
Infrastructure to Application, and Worker to Application and Infrastructure.

## Schema boundary

The implemented repository remains SQLite schema v2 during WP02. Release 1.6
plans an atomic, non-destructive v2-to-v3 migration that preserves all accepted
v1/v2 data. WP02 neither defines the physical schema-v3 layout nor implements
tables, columns, indexes, constraints, or migration statements. Feature Set
persistence remains absent. WP06 owns the physical model.

## Worker boundary

Release 1.6 durability is a separate explicit one-shot mode. It is not an
implicit side effect of Release 1.5 Experiment execution. Existing Experiment,
Feature, and five-stage pipeline modes remain unchanged. The future durable
mode has bounded success/failure presentation, no provider fallback, and no
scheduling, retry, recovery, or daemon behavior. WP11 owns implementation and
final configuration names.

## Release preservation

- Release 1.1 observation persistence and retrieval remain unchanged.
- Release 1.2 Dataset Snapshot identity, version, persistence, and exact lookup
  remain unchanged.
- Release 1.3 remains a fixed five-stage pipeline.
- Release 1.4 Feature generation and Feature Set identity remain unchanged and
  in-memory.
- Release 1.5 Experiment generation, identity, evidence, validation, DI, and
  one-shot Worker behavior remain unchanged.

## Explicit deferrals

Release 1.6 does not add Feature Set persistence or cataloging, generalized
experiment registry/history/comparison/search, update/delete/retention,
additional experiments, strategies, signals, backtesting, portfolio/risk,
provider acquisition or fallback, scheduling/retries/recovery/checkpoints,
workspaces/notebooks/visualization/public APIs, distributed execution,
AI/ML, explainability, MLOps, or Release 1.7 implementation.

## Downstream authority

WP03 may freeze exact persistence identity, provenance, equivalence, decimal,
and reconstruction mechanics while preserving these semantics. WP04–WP14 own
their separately authorized contracts, orchestration, physical schema,
persistence, retrieval, failure mapping, composition, Worker behavior,
permanent tests, documentation, and integration work.

No downstream work package may reinterpret durability as generalized
experiment management.
