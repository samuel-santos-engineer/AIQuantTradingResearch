# Experiment Persistence Identity, Provenance, and Fidelity

## Purpose

This document freezes the Release 1.6 persistence-boundary invariants for an
accepted Release 1.5 Experiment Result. It defines identity preservation,
evidence equivalence, contradiction detection, round-trip reconstruction,
provenance, lineage, decimal fidelity, and validation requirements without
choosing Application contract shapes or a physical SQLite representation.

`DURABLE_EXPERIMENT_EVIDENCE.md` remains authoritative for the durable object,
outcomes, failures, ownership, and scope. Release 1.5 remains authoritative for
Experiment Result construction and `aiq-experiment-identity-v1` canonical
encoding.

## Semantic boundary

The persistence boundary accepts exactly one complete, valid, immutable
`simple-return-descriptive-summary-v1` Experiment Result and later returns
equivalent immutable durable evidence by exact identity. Persistence records an
accepted conclusion; it does not generate, recompute, reinterpret, repair, or
extend that conclusion.

Generation and durability remain distinct:

```text
accepted Release 1.5 Experiment Result
  -> validate durable evidence
  -> atomically accept or recognize equivalent evidence
  -> exact identity retrieval
  -> validate and reconstruct equivalent immutable evidence
```

## Identity preservation

The persisted semantic identity is the existing typed Experiment Result
Identity. Its scheme remains exactly `aiq-experiment-identity-v1`, with the
Release 1.5 SHA-256 fingerprint rendered as 64 lowercase hexadecimal
characters.

Release 1.6 creates no persistence, row, invocation, registry, or storage
identity. A database key, timestamp, sequence, path, connection, process,
machine, or persistence disposition cannot enter identity or equivalence.

Acceptance must preserve the identity supplied by the valid in-memory result.
Retrieval must validate evidence under the requested typed identity and return
that exact identity. Neither side may recompute identity with another scheme,
alter canonical inputs, assign a replacement identity, or use storage metadata
to resolve a contradiction.

The Release 1.5 canonical component framing, field order, domain separation,
decimal canonicalization, and hash computation remain unchanged and are not
restated or redefined here.

## Experiment Definition binding

Durable evidence preserves both:

- the exact Experiment Definition Identity; and
- the built-in `simple-return-descriptive-summary-v1` definition reference.

Definition substitution, aliasing, latest-version resolution, storage-owned
definition identity, and registry lookup are prohibited. Reconstruction must
establish that the definition reference and identity agree under the accepted
Release 1.5 identity rules.

Release 1.6 introduces no additional experiment definition.

## Feature Set binding

The durable result remains bound to the exact accepted Feature Set Identity
represented by the source Experiment Result. Two results with numerically equal
summaries but different Feature Set identities are distinct.

Durable acceptance and retrieval cannot:

- substitute another Feature Set identity;
- infer identity from aggregates;
- select a latest Feature Set;
- reacquire provider data;
- require Experiment or Feature recomputation; or
- create Feature Set persistence.

Feature Values are not part of the Release 1.6 persisted result-only artifact.
The durable evidence preserves the exact Feature Set identity and required
identity-bearing provenance and lineage references. Downstream contracts must
not fabricate a current in-memory `ExperimentResult` or `FeatureSet` object
whose constructor invariants cannot be established from durable evidence.
Instead, they must expose complete immutable durable result evidence capable of
being validated as semantically equivalent to the accepted result. WP04 owns
the concrete storage-independent contract shape.

## Required durable evidence

Persistence equivalence and round-trip validation cover every semantic field
required to identify and explain the accepted result:

- Experiment Result Identity scheme and fingerprint;
- Experiment Definition Identity scheme and fingerprint;
- the built-in Experiment Definition reference;
- Feature Set Identity scheme and fingerprint;
- Feature Definition Identity represented by Feature Set provenance and
  lineage;
- exact Dataset Snapshot Identity;
- exact Dataset Version and its snapshot binding;
- Dataset Definition and Source State identity references carried through
  accepted provenance and lineage;
- count;
- aggregate-presence state;
- arithmetic mean when present;
- minimum when present; and
- maximum when present.

The exact WP04 contract may organize these facts differently, but it cannot
omit a fact needed to validate identity, provenance, lineage, presence, or
summary equivalence. Raw observations, Feature Values, provider payloads,
credentials, operational timestamps, logs, and database details are excluded.

## Identity equality and evidence equivalence

Identity equality is necessary but not sufficient for persistence equivalence.
Two representations are semantically equivalent only when:

1. their typed Experiment Result identities are equal;
2. their definition identities and references agree;
3. their exact Feature Set identities agree;
4. their required snapshot, dataset, source, provenance, and lineage references
   agree;
5. their counts agree;
6. their aggregate-presence states agree; and
7. every present decimal aggregate is exactly equal under accepted decimal
   semantics.

Same identity alone, aggregates alone, count alone, definition alone, or
storage-row equality alone cannot establish equivalence.

## Same-identity contradiction

Evidence is contradictory when it claims the same Experiment Result Identity
but materially differs in any required semantic evidence or violates an
accepted identity, presence, provenance, or lineage invariant.

The bounded outcome is `IntegrityConflict`, whether contradiction is detected
during acceptance or retrieval validation. Processing is fail-stop. The system
must not overwrite, merge, repair, normalize away the difference, assign a new
identity, apply last-write-wins behavior, return `EquivalentExisting`, delete
the stored evidence, or expose partial success.

Unknown corruption or programming defects beyond the governed contradiction
model propagate rather than being broadly normalized.

## Round-trip invariant

For any valid accepted Experiment Result evidence `R`:

```text
persist(R) -> retrieve(R.Identity) -> R'
```

`R'` must be semantically equivalent to `R` under every required identity,
definition, Feature Set, summary, provenance, and lineage invariant.

Round-trip equivalence is deterministic and independent of culture, timezone,
process, restart, object reference identity, provider availability, database
path, and physical storage encoding. Semantic equality is required; byte-for-
byte equality of a physical record is not.

## Count fidelity

Count is a non-negative semantic integer and must round-trip unchanged. It is
never floating-point, localized, truncated, inferred from persisted aggregates,
or given another meaning. Count zero is valid successful evidence.

The physical integer type and constraints belong to WP06.

## Aggregate-presence fidelity

Presence is an explicit semantic distinction:

- count zero requires mean, minimum, and maximum all absent;
- positive count requires mean, minimum, and maximum all present.

Persistence must preserve presence and absence exactly. Absence cannot become
zero, empty text, NaN, infinity, a sentinel, or a fabricated decimal. A partial
presence state is invalid evidence.

Physical nullability and encoding belong to WP06.

## Decimal fidelity

Every present mean, minimum, and maximum must round-trip as the exact accepted
`decimal` value. Persistence cannot introduce binary floating point, precision
loss, rounding, clamping, scientific-format ambiguity, culture dependence, or
a scale change that alters value.

Where identity validation requires decimal canonicalization, the existing
Release 1.5 canonical decimal semantics remain authoritative. WP03 does not
choose SQLite storage types or text encoding.

## Empty-result fidelity

A valid empty result is durable successful evidence:

- Experiment Result Identity remains exact;
- Experiment Definition and Feature Set bindings remain exact;
- count remains zero;
- all aggregates remain absent;
- provenance and lineage remain complete;
- first acceptance may return `NewlyAccepted`;
- an equivalent repeat returns `EquivalentExisting`; and
- later exact lookup succeeds.

An empty result is never `NotFound`.

## Non-empty-result fidelity

A valid non-empty result preserves the exact typed identities and references,
positive count, present decimal mean/minimum/maximum, aggregate-presence state,
and required provenance and lineage. Retrieval cannot normalize or approximate
this evidence.

## Provenance preservation

Durable evidence retains the accepted origin chain represented by the Release
1.5 result:

```text
experiment definition
  <- experiment result
  <- exact Feature Set
  <- exact Dataset Snapshot / Dataset Version
  <- dataset definition and source state
```

The notation describes evidence references, not execution direction. Every
identity-bearing provenance reference needed to establish the accepted result
must remain available after restart. Operational fields such as insertion time,
database path, process, machine, and Worker output are not provenance.

Feature Values and raw observations are not copied into the result-only durable
artifact.

## Lineage preservation and acyclicity

The accepted semantic lineage remains one-way and acyclic:

```text
source state
  -> dataset definition / research dataset
  -> dataset snapshot / version
  -> feature definition / feature set
  -> experiment definition / experiment result
```

Persistence is not a semantic parent, database rows are not lineage nodes, and
retrieval adds no lineage edge. No predecessor may point backward to the
Experiment Result, and no registry or history node is introduced.

## Immutable reconstruction

Retrieval validates all required evidence before exposing an immutable durable
result. It cannot leak a mutable storage DTO, expose a partial object, require
post-retrieval mutation, retain a storage session for semantic completeness, or
perform a lazy provider lookup.

The concrete immutable Application contract is deferred to WP04. Whatever its
shape, it must preserve the complete durable semantic evidence without
pretending that excluded Feature Values were persisted.

## Acceptance validation

Only valid accepted Experiment Result evidence may cross the durability
boundary. Before a new durable result is established, validation must reject:

- malformed or unsupported identity and definition evidence;
- inconsistent Experiment Result Identity inputs;
- mismatched Feature Set binding;
- invalid count or aggregate-presence state;
- invalid decimal evidence;
- incomplete or contradictory provenance; and
- invalid or cyclic lineage.

WP04, WP05, and WP09 own contract, orchestration, precedence, and mapping
mechanics. WP03 freezes the invariant only.

## Retrieval validation

Retrieval does not trust physical evidence blindly. It must reconstruct and
validate enough semantic evidence to prove the requested identity and accepted
invariants before returning success.

- exact identity absent: `NotFound`;
- storage inaccessible: `DependencyUnavailable`;
- present evidence malformed but not a same-identity contradiction:
  `InvalidEvidence` where the accepted boundary can establish it;
- present same-identity contradictory evidence: `IntegrityConflict`;
- unknown defect outside the bounded model: propagate.

Retrieval cannot fabricate missing evidence or silently repair a record.

## Acceptance dispositions

### NewlyAccepted

`NewlyAccepted` means one complete valid semantic unit was atomically made
durable under its unchanged identity. Subsequent restart-safe exact retrieval
must reconstruct equivalent evidence. It creates no duplicate logical result,
Feature Set persistence, registry entry, or history record.

### EquivalentExisting

`EquivalentExisting` means exact-identity evidence already exists, validates,
and is equivalent to the candidate across every required field. It is
successful idempotence without overwrite, duplicate, identity replacement, or
semantic mutation. It can never mask a contradiction.

## Atomic semantic unit

The atomic unit is the complete durable Experiment Result evidence required for
equivalent reconstruction. Success cannot require later writes to become
semantically complete. Failure exposes no newly accepted partial identity,
summary, presence, provenance, or lineage evidence.

The transaction and physical atomicity mechanism belongs to WP06 and WP07.

## Failure semantics

Release 1.6 preserves this storage-independent vocabulary:

1. `InvalidRequest`: malformed or incoherent persistence/retrieval request;
2. `NotFound`: exact identity is absent;
3. `DependencyUnavailable`: durable storage cannot serve the operation;
4. `InvalidEvidence`: candidate or reconstructed evidence is malformed or
   incomplete; and
5. `IntegrityConflict`: same-identity durable evidence is contradictory.

Unknown programming defects propagate. No public storage-specific failure
vocabulary, retry, repair, fallback, overwrite, or partial success is defined.

## Ownership

- **Domain:** zero Release 1.6 delta.
- **Application:** owns storage-independent contracts, validation,
  orchestration, identity/evidence requirements, outcomes, and failures.
- **Infrastructure:** later owns SQLite mapping, migration, transactions,
  persistence/retrieval mechanics, and storage-specific exception translation.
- **Worker:** later owns explicit one-shot durable-mode invocation and bounded
  presentation.

The production dependency graph remains unchanged and acyclic.

## Schema boundary

The implemented repository remains schema v2. Release 1.6 plans schema v3, but
WP03 introduces no table, column, index, constraint, SQL, migration, or schema
code. WP06 owns the physical schema-v3 model and must preserve accepted v1/v2
evidence. Feature Set persistence and generalized experiment registry/history
remain excluded.

## Predecessor preservation

- Release 1.1 observation persistence and retrieval remain intact.
- Release 1.2 dataset/snapshot identity, version, provenance, persistence, and
  exact lookup remain intact.
- Release 1.3 remains a fixed five-stage pipeline.
- Release 1.4 Feature Set identity and generation remain unchanged and
  unpersisted.
- Release 1.5 Experiment generation, identity encoding, provenance, evidence,
  validation, DI, and Worker behavior remain unchanged.

Release 1.6 persistence neither recomputes nor redefines predecessor evidence.

## Explicit deferrals

Deferred capabilities include Feature Set persistence/cataloging, generalized
experiment registry/history/list/search/comparison, update/delete/retention,
additional experiments, provider acquisition/fallback, strategies/signals,
backtesting, portfolio/risk, scheduling/retry/recovery, distributed execution,
workspaces/notebooks/UI/API, AI/ML, explainability/MLOps, and Release 1.7
implementation.

## Downstream authority

WP04 owns Application persistence contracts; WP05 orchestration; WP06 the
schema-v3 physical model; WP07 persistence; WP08 retrieval; WP09 storage
validation and failure mapping; WP10 composition; WP11 Worker behavior; WP12
permanent tests; WP13 architecture/documentation alignment; and WP14 release
integration.

No downstream work may weaken identity preservation, evidence equivalence,
round-trip fidelity, immutability, or same-identity conflict behavior.
