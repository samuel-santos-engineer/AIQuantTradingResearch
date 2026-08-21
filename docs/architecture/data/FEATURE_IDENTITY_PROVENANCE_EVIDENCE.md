# Feature Identity, Provenance, and Evidence Semantics

## Purpose

This document freezes the Release 1.4 semantic identity, provenance, lineage,
and evidence rules for `simple-return-lag-1-v1`. It defines how deterministic
feature evidence composes with accepted dataset evidence without changing the
Release 1.2 dataset identity system or the Release 1.3 pipeline identity system.
It defines no C# API, persistence model, or operational run identity.

## Authority and predecessor identities

Release 1.2 remains authoritative for `aiq-dataset-identity-v1`, deterministic
UTF-8 canonical representations, SHA-256 fingerprints rendered as lowercase
hexadecimal, Dataset Definition Identity, Research Dataset Identity, Source
State Identity, immutable Dataset Snapshot Identity and Version, provenance,
acyclic lineage, equivalence, and integrity-conflict semantics.

Release 1.3 remains authoritative for `aiq-pipeline-identity-v1`, distinct
Pipeline Definition and Semantic Pipeline Execution identities, deterministic
rerun equivalence, disposition-independent semantic execution identity,
first-failure evidence limits, and separation of semantic and operational
information.

Release 1.4 consumes these identities as established evidence. It neither
recomputes nor replaces them. The feature use case remains separate from the
fixed Release 1.3 pipeline.

## Identity vocabulary

- **Feature Identity Scheme:** the versioned canonicalization domain
  `aiq-feature-identity-v1`.
- **Feature Definition Identity:** the semantic identity of exactly one
  transformation definition, independent of input and execution.
- **Feature Set Identity:** the semantic identity of the immutable ordered
  feature evidence produced for one exact snapshot and definition.
- **Fingerprint:** the 32-byte SHA-256 digest of canonical semantic content,
  rendered as exactly 64 lowercase hexadecimal characters.
- **Semantic evidence:** information that determines or explains the result's
  meaning and reproducibility.
- **Operational evidence:** invocation-specific information that may describe
  execution but never determines semantic identity.
- **Equivalent recomputation:** another computation from the same exact
  semantic inputs producing the same semantic evidence and identity.
- **Integrity contradiction:** an assertion that equal semantic identity refers
  to contradictory canonical content.

## `aiq-feature-identity-v1`

Every Release 1.4 feature fingerprint uses the exact scheme discriminator
`aiq-feature-identity-v1`, SHA-256, a 32-byte digest, and a 64-character
lowercase hexadecimal rendering. Canonical content is UTF-8 without a byte
order mark, length-delimited, ordered using ordinal byte semantics, and
independent of culture, timezone, process, machine, runtime, and invocation.

JSON serialization, reflection order, C# object hash codes, dictionary order,
filesystem state, random values, and current time are not identity authority.

## Feature Definition Identity

Feature Definition Identity answers only: "which transformation semantics?"
Release 1.4 supports exactly `simple-return-lag-1-v1`. Equivalent requests for
that definition produce the same identity.

Definition identity excludes snapshot identity, source state, output values,
execution time, correlation or invocation identifiers, persistence disposition,
Worker details, and operational configuration. It is a distinct semantic type
and cannot be substituted for Feature Set Identity.

## Feature Set Identity

Feature Set Identity answers: "which deterministic feature evidence was
produced from which exact accepted snapshot under which exact definition?" It
binds:

- the feature identity scheme;
- Feature Definition Identity;
- the exact Dataset Snapshot Identity;
- the accepted Dataset Snapshot Version evidence;
- semantic cardinality; and
- every ordered feature value with its position, timestamp, offset, and exact
  decimal value.

The exact input snapshot is identity-bearing even when another snapshot happens
to yield equal numeric values. Feature Set Identity is immutable. Release 1.4
defines neither a mutable Feature Version nor an Operational Feature Run
Identity.

## Acyclic identity derivation

Identity derivation is one-way:

```text
simple-return-lag-1-v1 semantic definition
  -> Feature Definition Identity

accepted Dataset Snapshot Identity / Version
  + Feature Definition Identity
  + ordered deterministic feature evidence
  -> Feature Set Identity
```

Feature Definition Identity never depends on Feature Set Identity. Feature Set
Identity never feeds back into source-state, dataset definition, research
dataset, snapshot, version, pipeline definition, or pipeline execution identity.

## Canonical representation

Each canonical identity payload begins with an explicit type-domain label,
followed by the `aiq-feature-identity-v1` scheme discriminator, the invariant
field count, and fields in the order defined by this document. Each component
is encoded as:

```text
invariant UTF-8 byte length + ":" + UTF-8 content
```

Length prefixes prevent concatenation ambiguity. Domain labels distinguish
Feature Definition and Feature Set payloads. Absence, an empty scalar, and an
empty collection are distinct. Collections use semantic order. Scalar
representations are invariant. Canonical identity content excludes incidental
type or namespace names and all operational metadata.

## Canonical decimal semantics

Feature decimals use the same semantic principles as accepted dataset decimal
evidence: no binary floating-point conversion, culture-specific separator,
display formatting, scientific-notation variation, convenience rounding, or
loss of scale-independent numeric meaning.

The canonical decimal value is represented by invariant sign, non-negative
coefficient, and scale components. Redundant trailing decimal zeros are removed
from the coefficient while reducing scale, so semantically equal decimal values
have one representation. Zero has the non-negative canonical sign. The
representation is length-delimited like every other field.

## Canonical timestamp and offset semantics

Each feature value uses the current observation `i` timestamp frozen by WP02.
Canonical evidence contains invariant UTC ticks for the semantic instant and
the accepted offset in whole minutes as a separate component. Together they are
round-trippable to the accepted `DateTimeOffset` evidence.

Identity never uses local time, machine timezone, execution time, the prior
observation timestamp, or an offset-destroying representation.

## Feature Definition canonical content

The `feature-definition` canonical payload contains, in this fixed semantic
order:

1. `aiq-feature-identity-v1` as the scheme discriminator;
2. built-in definition name/version `simple-return-lag-1-v1`;
3. fixed lag `1`;
4. fixed formula semantics `(p[i] / p[i-1]) - 1`;
5. current-observation timestamp and offset ownership;
6. exact decimal arithmetic without binary floating point or convenience
   rounding;
7. zero predecessor classified as invalid numeric evidence;
8. accepted empty snapshot produces successful empty evidence; and
9. accepted one-observation snapshot produces successful empty evidence.

These are semantic fields, not configurable parameters. Aliases cannot create
different identities for the same definition.

## Feature Set canonical content

The `feature-set` canonical payload contains, in this fixed semantic order:

1. `aiq-feature-identity-v1` as the scheme discriminator;
2. Feature Definition Identity scheme and fingerprint;
3. Dataset Snapshot Identity scheme and fingerprint;
4. Dataset Version's bound snapshot-identity scheme and fingerprint;
5. invariant feature count; and
6. feature values in accepted order.

Each feature value contributes its zero-based feature position, current
observation UTC ticks, current observation offset minutes, and canonical decimal
value. The count and positions make empty and non-empty collections unambiguous.

Provider names, SQL records, database keys, paths, connection strings, logging
metadata, execution duration, persistence timing, and console formatting are
excluded.

## Empty feature-set identity

An accepted empty snapshot and an accepted one-observation snapshot each
produce a successful empty feature set. Empty evidence still has a deterministic
Feature Set Identity binding the exact Feature Definition Identity, exact
snapshot identity/version, and cardinality zero.

There is no global empty sentinel identity. Empty feature sets from different
snapshot identities are not automatically interchangeable.

## Equivalent recomputation

The same exact snapshot identity/version, Feature Definition Identity, and
deterministic ordered feature evidence produce the same Feature Set Identity.
Wall-clock, host, process, invocation, logging, and formatting differences do
not affect it.

Feature recomputation has no `NewlyAccepted` or `EquivalentExisting`
persistence disposition. Semantic equivalence is sufficient.

## Same values from different snapshots

Two different Dataset Snapshot identities remain different feature inputs even
if they produce identical ordered timestamps and numeric feature values. Their
Feature Set identities must differ because exact snapshot identity/version is
part of feature identity and lineage. Numeric coincidence cannot erase
provenance.

## Definition evolution and versioning rule

A change to formula, lag, timestamp ownership, decimal semantics, invalid
numeric behavior, or empty/single-observation behavior is a semantic definition
change and must not retain the `simple-return-lag-1-v1` identity semantics.

Release 1.4 defines only this version. Future definitions require separate
governed names and identities; WP03 does not create a registry, alias system,
plugin boundary, or configurable parameter model.

## Provenance

Successful immutable feature provenance is traceable to:

- Feature Definition Identity;
- exact Dataset Snapshot Identity;
- exact Dataset Snapshot Version evidence;
- the predecessor Dataset Definition, Research Dataset, and Source State
  evidence reachable through accepted snapshot provenance;
- ordered feature evidence and its cardinality; and
- Feature Set Identity.

Feature provenance references accepted predecessor identities instead of
duplicating or redefining them. No provider-specific or operational-run
provenance is introduced.

## Lineage

Feature lineage is narrow and acyclic:

```text
Source State
  -> Dataset Definition / Research Dataset
  -> Dataset Snapshot / Version
  -> Feature Definition
  -> Feature Set
```

The feature edge adds derived evidence but never mutates predecessor lineage.
There is no mutable feature-version history, feature-run history, pipeline-run
history, operational lineage, or persistence lineage.

## Successful semantic evidence

A successful result can establish:

- Feature Definition Identity;
- exact input Dataset Snapshot Identity and Version;
- Feature Set Identity;
- ordered immutable feature values;
- cardinality;
- provenance and acyclic lineage; and
- successful empty or successful non-empty outcome.

Success is semantic and requires no feature persistence.

## Failure semantic evidence

Later contracts may represent bounded failure categories already frozen by
WP02: invalid request, unsupported definition, snapshot `NotFound`, unavailable
dependency, invalid predecessor evidence, invalid numeric evidence, and
integrity contradiction.

Failure evidence includes only facts established before failure. Unknown or
unrelated defects remain outside bounded normalization and propagate according
to later contract authority. WP03 defines no exception or result types.

## Evidence-established-only rule

Identity evidence cannot be claimed before its prerequisites exist:

- A valid supported definition may establish Feature Definition Identity.
- Successful exact lookup may establish Dataset Snapshot Identity and Version.
- Feature Set Identity exists only after all deterministic feature values have
  been computed and validated successfully.
- `NotFound`, failed lookup, invalid predecessor evidence, or failed numeric
  computation establishes no Feature Set Identity.
- Validation failure before definition establishment cannot fabricate any
  downstream identity.

Evidence stops at the last established semantic fact. No placeholder or
sentinel identity fills a missing prerequisite.

## Integrity contradiction

Equal semantic identity or fingerprint asserted for contradictory canonical
semantic content is an integrity contradiction. It is neither equivalence nor
ordinary invalid input. The system must not choose one value, overwrite
evidence, or normalize the contradiction away. WP07 owns exact failure mapping.

## Semantic versus operational evidence

Semantic identity and evidence exclude wall-clock timestamps, duration,
correlation or invocation identifiers, process and machine identity, user name,
current culture or timezone, filesystem or database path, connection string,
provider credential, logging scope, console formatting, retry count, and
scheduling information.

Operational diagnostics may reference semantic identities later, but
operational values never determine them.

## Provider and storage independence

Feature identity, provenance, and evidence contain no Twelve Data, HTTP,
provider response, SQLite, SQL, database-generated key, database path,
connection ownership, or persistence timing semantics. Infrastructure remains
an implementation boundary, and Release 1.4 feature output remains in memory.

## Relationship to the Release 1.3 pipeline

`aiq-pipeline-identity-v1`, Pipeline Definition Identity, Semantic Pipeline
Execution Identity, five-stage topology, pipeline evidence, dispositions, and
Worker pipeline semantics remain unchanged. Feature Definition and Feature Set
identities are separate Release 1.4 identities and do not participate in or
alter Release 1.3 pipeline identity.

## Schema and persistence decision

SQLite remains schema version 2. WP03 requires no feature table, catalog,
run-history table, cache, identity persistence, provenance persistence,
checkpoint state, or schema v3. Identity, provenance, lineage, and evidence are
in-memory semantic contracts for Release 1.4.

## Explicit Release 1.5+ deferrals

Deferred capabilities include feature persistence and cataloging, multiple
indicators, configurable lag, arbitrary formulas, rolling indicators, plugins,
feature DAGs, acquisition orchestration, scheduling, retries, circuit breakers,
fallback, checkpoints, resume, durable execution history, observability
backends, notebooks, workspaces, strategies, backtesting, model training,
ML/MLOps, and distributed or streaming execution. WP03 creates no placeholder
for them.

## Ownership and WP04+ handoff

Application owns the provider- and storage-independent feature identity,
provenance, lineage, and evidence semantics. Dataset and pipeline identity
systems retain their existing ownership and meanings. Infrastructure owns only
later physical mechanics; Worker owns only later composition and one-shot
presentation concerns.

WP04 may translate these semantics into the minimum immutable feature model
without changing the identity scheme, canonical content, predecessor identity
systems, schema, or five-stage pipeline. Later work packages separately own
contracts, computation, validation, integration, DI, Worker behavior, and tests.
