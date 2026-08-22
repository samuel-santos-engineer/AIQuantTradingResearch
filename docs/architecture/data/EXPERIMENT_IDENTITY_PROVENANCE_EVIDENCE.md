# Experiment Identity, Provenance, and Evidence Semantics

## Purpose

This document freezes the Release 1.5 semantic identity, canonical
representation, provenance, lineage, equivalence, and evidence rules for
`simple-return-descriptive-summary-v1`. It implements no C# type, hashing code,
persistence model, Worker behavior, or operational run identity.

## Authority and predecessor identities

`EXPERIMENT_SEMANTICS.md` remains authoritative for input, summary,
empty-result, decimal, deterministic, failure, ownership, and persistence
semantics.

Release 1.2 remains authoritative for `aiq-dataset-identity-v1`, immutable
Dataset Definition, Research Dataset, Source State, Dataset Snapshot, and
Dataset Version identities, exact decimal and offset fidelity, semantic
equivalence, integrity contradiction, provenance, and acyclic lineage.

Release 1.3 remains authoritative for `aiq-pipeline-identity-v1`, distinct
pipeline definition and semantic execution identities, the fixed five-stage
pipeline, first-failure evidence limits, rerun equivalence, and operational
metadata exclusion.

Release 1.4 remains authoritative for `aiq-feature-identity-v1`, distinct
Feature Definition and Feature Set identities, exact snapshot/version binding,
ordered Feature Values, successful empty Feature Set identity, decimal
canonicalization, provenance, lineage, and equivalent recomputation.

Release 1.5 references these identities as established evidence. It neither
recomputes nor aliases them.

## Identity vocabulary

- **Experiment Identity Scheme:** the canonicalization domain
  `aiq-experiment-identity-v1`.
- **Experiment Definition Identity:** immutable semantic identity of the one
  built-in experiment definition, independent of any Feature Set or result.
- **Experiment Result Identity:** immutable semantic identity of one complete
  successful result for one exact Feature Set under one exact definition.
- **Fingerprint:** SHA-256 digest of canonical semantic bytes, externally
  rendered as exactly 64 lowercase hexadecimal characters.
- **Semantic evidence:** identity-bearing facts that determine or explain
  meaning and reproducibility.
- **Operational evidence:** invocation-specific facts that never participate
  in semantic identity or equivalence.
- **Equivalent recomputation:** another computation from equivalent exact
  semantic inputs that produces identical canonical result evidence.
- **Integrity contradiction:** an assertion that one semantic identity refers
  to contradictory canonical content.

## Identity scheme and fingerprint

Every Release 1.5 experiment identity uses exactly:

`aiq-experiment-identity-v1`

The digest algorithm is SHA-256. A fingerprint is exactly 32 digest bytes and
is rendered externally as exactly 64 lowercase hexadecimal characters. No
uppercase, base64, culture-dependent, platform-dependent, or abbreviated form
is equivalent under this scheme.

Conceptual external forms are:

```text
experiment-definition:aiq-experiment-identity-v1:sha256:<64-lowercase-hex>
experiment-result:aiq-experiment-identity-v1:sha256:<64-lowercase-hex>
```

The type domain and scheme are required to interpret a fingerprint. A digest
is deterministic evidence, not authentication, authorization, encryption, a
signature, or proof that collisions are impossible.

## Distinct identity types and domains

Release 1.5 defines exactly two experiment identity types:

1. Experiment Definition Identity, canonical domain `experiment-definition`;
2. Experiment Result Identity, canonical domain `experiment-result`.

The ordinal domain string is part of the hashed canonical stream. Definition
and result identities are not interchangeable, even if their remaining field
values could otherwise coincide. Release 1.5 defines no experiment-run,
invocation, persistence-record, registry-entry, workspace, or scheduler
identity.

## Canonical component framing

Canonical content is BOM-free UTF-8. Each component is appended without a
separator using this exact framing:

```text
<invariant UTF-8 byte count>:<UTF-8 content>
```

The byte count is non-negative invariant base-10 ASCII with no leading zero,
except the value zero itself. It counts UTF-8 bytes, not characters. An empty
component is exactly `0:`. Length framing makes colons, commas, and arbitrary
UTF-8 content inside a component unambiguous.

For each identity, the canonical stream consists of these envelope components
in exact order:

1. identity domain;
2. `aiq-experiment-identity-v1`;
3. semantic field count as invariant base-10 ASCII;
4. every semantic field in the exact identity-specific order below.

The field count counts only the semantic fields after the envelope. Every
field is framed with the same component rule. There are no newlines, byte-order
mark, optional whitespace, field names, JSON properties, or terminator bytes.
Hash input is exactly the resulting concatenated UTF-8 byte stream.

Ordinal field order is authority. Reflection order, dictionary enumeration,
serializer behavior, display formatting, runtime object identity, and
platform newline conventions are excluded.

## Experiment Definition Identity

Experiment Definition Identity answers only: “which governed experiment
semantics?” It excludes Feature Set identity and values, snapshot identity,
result aggregates, invocation facts, configuration source, and persistence
disposition.

The `experiment-definition` canonical payload contains exactly 11 semantic
fields in this order:

1. scheme: `aiq-experiment-identity-v1`;
2. definition: `simple-return-descriptive-summary-v1`;
3. accepted input definition: `simple-return-lag-1-v1`;
4. cardinality rule: `complete-feature-set-cardinality`;
5. mean rule: `(x[0] + ... + x[N-1]) / N`;
6. minimum rule: `min(x[0], ..., x[N-1])`;
7. maximum rule: `max(x[0], ..., x[N-1])`;
8. empty rule: `count-zero-aggregates-absent`;
9. non-empty rule: `count-positive-all-aggregates-present`;
10. numeric rule: `decimal-exact-no-rounding`;
11. input-evidence rule: `exact-feature-set-no-reorder-no-filter`.

These strings are fixed semantic tokens, not configurable values. The scheme
appears in both the canonical envelope and semantic field 1 to match the
accepted repository identity-computation convention. Aliases, casing changes,
or alternative formula text are not equivalent canonical representations.

A future semantic change to the accepted input definition, aggregate set,
formula, empty behavior, decimal behavior, or evidence-consumption rule
requires separate governance and must not retain this definition identity.

## Experiment Result Identity

Experiment Result Identity answers: “which complete deterministic summary was
established for which exact Feature Set under which exact experiment
definition?” It binds predecessor identities rather than reconstructing them.

The `experiment-result` canonical payload contains exactly 10 semantic fields
in this order:

1. scheme: `aiq-experiment-identity-v1`;
2. Experiment Definition Identity scheme;
3. Experiment Definition Identity fingerprint;
4. exact Feature Set Identity scheme;
5. exact Feature Set Identity fingerprint;
6. result count;
7. aggregate-presence marker;
8. canonical arithmetic mean or the canonical absent component;
9. canonical minimum or the canonical absent component;
10. canonical maximum or the canonical absent component.

Identity schemes and fingerprints are separate framed components. Their
pairing preserves typed predecessor identity evidence. Experiment Result
Identity is computed only after all ten fields are coherent and complete.

## Exact Feature Set binding

Result identity binds the exact accepted Release 1.4 Feature Set Identity,
including its own `aiq-feature-identity-v1` scheme and fingerprint. It does not
derive or substitute Feature Set identity from raw values or summary values.

The Feature Set Identity already binds Feature Definition Identity, exact
Dataset Snapshot Identity and Version, cardinality, ordering, timestamps,
offsets, and decimal values. Result identity references that accepted evidence
without duplicating or reinterpreting its canonical payload.

Different Feature Set identities produce distinct Experiment Result canonical
content even if count, mean, minimum, and maximum are numerically identical.
Experiment identity never feeds back into feature, snapshot, dataset, or
pipeline identity.

## Count encoding

Count is encoded as invariant non-negative base-10 ASCII with no leading zero,
except zero itself. Examples are `0`, `1`, and `27`. A plus sign, whitespace,
group separator, culture-specific digits, and alternate leading-zero form are
invalid.

Count is the complete Feature Set cardinality frozen by WP02. It must agree
with aggregate presence:

- count `0` requires absent mean, minimum, and maximum;
- count greater than `0` requires present mean, minimum, and maximum.

A disagreement is invalid or contradictory evidence, not another canonical
representation.

## Aggregate presence encoding

Semantic field 7 is exactly one ordinal ASCII token:

- `0` means all three aggregates are absent;
- `1` means all three aggregates are present.

When the marker is `0`, fields 8, 9, and 10 are each the empty component
`0:`. When the marker is `1`, each of fields 8, 9, and 10 contains one
canonical decimal string and is framed normally. No aggregate field is ever
omitted from the ten-field record.

Absence is therefore distinct from decimal zero. NaN, infinity, sentinel
decimals, the literal word `null`, omitted fields, and mixed presence are not
valid representations.

## Canonical decimal representation

Mean, minimum, and maximum reuse the accepted Release 1.4 feature decimal
canonicalization. A present decimal is one invariant string:

```text
<sign>,<coefficient>,<scale>
```

The whole string is then byte-length framed as one canonical component.

- sign is `0` for non-negative and `1` for negative;
- coefficient is the non-negative base-10 integer magnitude with no leading
  zero except zero itself;
- scale is a non-negative invariant base-10 integer with no leading zero except
  zero itself;
- redundant trailing decimal zeros are removed from the coefficient while
  reducing scale;
- zero has sign `0`, coefficient `0`, and scale `0`;
- binary floating-point conversion, scientific notation, locale separators,
  display formatting, and convenience rounding are prohibited.

Thus numerically equal accepted decimal values have one canonical
representation. Commas inside the decimal component are unambiguous because
the complete decimal string is byte-length framed.

## Successful empty-result identity

An accepted empty Feature Set produces a successful identity-bearing result.
Its result fields contain:

- the exact Experiment Definition Identity;
- the exact empty Feature Set Identity;
- count `0`;
- presence marker `0`;
- three empty aggregate components.

There is no global empty-result sentinel identity. Empty results bound to
different Feature Set identities remain distinct. Empty success requires no
fabricated decimal and is neither `NotFound` nor failure.

## Successful non-empty-result identity

For count greater than zero, presence marker `1` and all three canonical
decimal aggregates are required. The identity binds count, mean, minimum, and
maximum in the fixed order above, together with exact definition and Feature
Set identities.

No Experiment Result Identity exists for partial aggregates, a count/presence
contradiction, or unrepresentable numeric evidence.

## Hashing and external form

The canonical component stream is hashed exactly once with SHA-256. The 32
digest bytes are rendered with two lowercase hexadecimal characters per byte,
in digest byte order, producing exactly 64 characters. No additional hashing,
text encoding, salt, secret, random input, timestamp, or environment data is
added.

The external identity always retains its semantic type and
`aiq-experiment-identity-v1` scheme. A bare fingerprint cannot be substituted
for a typed identity in semantic evidence.

## Equivalence and distinctness

Equivalent governed definition semantics produce the same Experiment
Definition Identity. The same Experiment Definition Identity, exact Feature
Set Identity, and equivalent canonical count/presence/aggregate evidence
produce the same Experiment Result Identity.

Recomputation in another process, machine, timezone, culture, or time remains
semantically equivalent. Persistence or presentation differences do not create
a new result identity.

These changes are identity-distinct:

- governed experiment definition semantics;
- exact Feature Set Identity;
- count;
- aggregate presence;
- mean;
- minimum; or
- maximum.

Operational differences alone do not require distinct identity.

## Provenance

Successful Experiment Result provenance references:

- Experiment Definition Identity;
- Experiment Result Identity;
- exact Feature Set Identity;
- the Feature Set's Feature Definition Identity;
- exact Dataset Snapshot Identity and Dataset Version reachable through
  accepted Feature Set provenance; and
- predecessor dataset definition, research dataset, source-state, and
  historical-observation provenance already established upstream.

Provenance references accepted scheme-qualified identities rather than copying
provider payloads, SQL rows, storage paths, or predecessor canonical bodies.
It makes the result reproducible without redefining predecessor identity.

## Lineage

Lineage is one-way and acyclic:

```text
Source State
  -> Dataset Definition / Research Dataset
  -> Dataset Snapshot / Version
  -> Feature Definition
  -> Feature Set
  -> Experiment Definition
  -> Experiment Result
```

Experiment Definition Identity never depends on Experiment Result Identity.
Experiment identities never become inputs to dataset, snapshot, pipeline, or
feature identities. Release 1.5 introduces no generalized lineage graph.

## Evidence-established-only rule

An Experiment Definition Identity may be established once the one governed
definition is validated. An Experiment Result Identity exists only after the
exact Feature Set identity and complete coherent summary evidence are
established successfully.

Invalid request, unsupported definition, Feature Set lookup or generation
failure, invalid Feature Set evidence, numeric failure, partial aggregate
evidence, or integrity contradiction establishes no new Experiment Result
Identity. No placeholder, sentinel, random, timestamp-derived, or failure hash
fills missing prerequisites.

Failure evidence stops at the last established semantic fact. Unknown defects
remain unknown and do not become synthetic identity-bearing success or durable
failure-history identity.

## Integrity contradictions

Equal scheme-qualified identity asserted for contradictory canonical semantic
content is an integrity contradiction, not equivalence. For Experiment Result
Identity, contradictions include different definition identity, Feature Set
identity, count, presence marker, mean, minimum, or maximum associated with the
same asserted identity.

Implementations must fail deterministically, preserve the contradictory
evidence for bounded reporting, and never overwrite, alias, normalize away, or
replace the identity with a random or timestamp-derived value. WP06 owns
concrete validation precedence and failure mapping.

## Semantic and operational evidence

Experiment identities exclude invocation/start/end timestamps, duration,
current time, process/thread/machine identity, correlation/request identifiers,
filesystem or database path, connection string, environment-specific source,
credentials or API keys, logging messages/levels, metrics/traces, retry count,
scheduling/checkpoint state, persistence disposition, Worker exit code, Git
SHA, and build number.

Operational diagnostics may reference semantic identities, but operational
facts never determine identity, equivalence, provenance, or lineage.

## Provider, storage, pipeline, and schema boundaries

Experiment identity contains no Twelve Data, HTTP, provider payload, SQL,
SQLite API, database row key, file path, credential, persistence timing, or
storage-layout semantics. Results remain immutable in-memory evidence; there
is no experiment persistence, registry, catalog, cache, or history.

The fixed Release 1.3 five-stage pipeline and `aiq-pipeline-identity-v1`
remain unchanged. Experiment identity is not appended to pipeline identity and
does not create a sixth pipeline stage.

SQLite remains schema version 2. No schema v3 or experiment identity table is
authorized.

## Immutability and scheme evolution

Identity-bearing semantic content is immutable. An established identity cannot
be reassigned to different content. Release 1.5 defines only
`aiq-experiment-identity-v1`; it provides no migration, cross-scheme aliasing,
or version-2 identity behavior.

## Explicit deferrals

Deferred capabilities include experiment persistence, registry/history,
operational run identity, workspaces/notebooks, additional experiments or
statistics, configurable formulas, strategies/signals/backtesting,
portfolio/risk, AI/ML/MLOps, live acquisition, scheduling, retries, recovery,
checkpoints, generalized plugins/DAGs, distributed execution, durable
telemetry, identity migration, and a cross-release generic identity framework.

This document creates no implementation placeholder for deferred work.

## WP04 handoff

WP04 may implement the minimum immutable Application-owned identity and
experiment model corresponding exactly to this scheme. It must use the frozen
domains, envelope, field order, framing, count/presence rules, decimal
canonicalization, SHA-256 external form, exact Feature Set binding, provenance,
lineage, evidence-established-only behavior, and integrity semantics without
introducing a competing encoding.
