# Dataset Identity, Version, and Provenance Semantics

**Status:** Accepted for Release 1.2 WP03

**Scope:** Research Dataset Foundation

**Technology boundary:** Provider-, storage-, and implementation-independent

## Purpose and authority

This document converts the accepted
`RESEARCH_DATASET_DEFINITION.md` model into deterministic identity, version,
provenance, and lineage semantics. It specifies meaning and normative identity
inputs without defining Application contracts, catalog objects, physical
storage, or executable hashing code.

Release 1.1 accepted historical observations remain the source authority.
Release 1.2 does not change their exact-target identity, absolute-instant
identity, original-offset fidelity, exact decimal fidelity, immutable-history
behavior, or ascending retrieval semantics.

## Vocabulary

### Dataset Definition Identity

The deterministic semantic identity of one declarative dataset definition. It
changes exactly when the definition's meaning changes and is independent of
materialization or source-history state.

### Research Dataset Identity

The stable logical identity of the research dataset family defined by one
dataset definition. It remains stable across distinguishable snapshots of that
definition. It is a separately typed identity derived from the same definition
semantics and is not an alias for a mutable "latest" snapshot.

### Source State

The exact ordered sequence of accepted Release 1.1 observations selected by a
dataset definition at materialization. The empty ordered sequence is a valid
source state.

### Source-State Identity

The deterministic semantic identity of a relevant source state, including its
exact target and ordered observation content.

### Dataset Snapshot Identity

The deterministic identity of one immutable semantic materialization result,
derived from its definition identity and source-state identity under an
explicit identity scheme.

### Dataset Version

The immutable semantic version of a research dataset snapshot. In Release 1.2,
the authoritative version is the snapshot's deterministic identity, not a
timestamp or mutable sequence number.

### Semantic Identity

Identity determined only by the meaning of an artifact under an explicit
identity scheme, excluding incidental execution or storage facts.

### Semantic Equivalence

Equality of all normative semantic inputs for the relevant identity layer
under the same identity scheme.

### Provenance

Evidence explaining the authoritative definition and relevant accepted source
state from which a snapshot was materialized.

### Lineage

The derivation relationship connecting a snapshot to one definition, one
relevant source state, and the zero or more accepted observations composing
that state.

### Canonical Semantic Representation

The unambiguous, culture-independent, versioned representation of normative
semantic values used as digest input. It is not a storage serialization.

### Digest / Fingerprint

A content-derived cryptographic digest used as compact deterministic identity
evidence. It is not authentication, authorization, encryption, or proof that
collisions are mathematically impossible.

### Algorithm/Representation Version

The explicit identity-scheme identifier that fixes the canonicalization rules,
type domain, digest algorithm, and textual interpretation for an identity.

## Identity layers

| Layer | Stable across | Changes when | Normative basis |
| --- | --- | --- | --- |
| Dataset Definition Identity | Repeated use of an equivalent definition | Any definition semantic changes | Exact target, `[from, to)` instants, inclusion/order rules, parameters, definition-model version |
| Research Dataset Identity | All snapshots of one equivalent definition | The dataset definition changes | Separately typed digest over definition semantics |
| Source-State Identity | Equivalent relevant accepted history | Selected target, membership, order, instant, offset, or decimal value changes | Exact target plus ordered selected observations |
| Dataset Snapshot Identity / Version | Equivalent re-materializations | Definition identity or source-state identity changes | Typed digest over definition and source-state identities |

Identity types are not interchangeable even when they use the same digest
algorithm. Type-specific domain separators prevent a definition, logical
dataset, source state, and snapshot from sharing an identity accidentally.

## Definition and logical dataset identity

Two definitions are semantically equivalent only when all these facts match:

1. exact target using ordinal, case-sensitive target semantics;
2. lower and upper boundary semantic instants;
3. inclusive-lower/exclusive-upper `[from, to)` interpretation;
4. strict ascending semantic-instant ordering rule;
5. every authorized deterministic definition parameter; and
6. the definition semantic-model version.

Boundary identity uses the absolute semantic instant. Different display offsets
that denote the same boundary instant do not change selection meaning and do
not create a different definition. Source-observation offsets remain distinct
normative content as required below.

The Dataset Definition Identity and Research Dataset Identity are separately
typed deterministic fingerprints over the same definition semantics. The
logical Research Dataset Identity groups immutable snapshot versions without
creating mutable latest-state semantics.

Creation or execution time, display formatting, JSON ordering or whitespace,
machine/process identity, file or database paths, connection strings, culture,
and local timezone do not participate.

## Source-state identity

The relevant source state is the exact sequence selected by the definition,
strictly ordered by ascending absolute semantic instant. Its identity covers:

- the exact target;
- the explicit observation count; and
- for every ordered observation:
  - absolute semantic instant;
  - original offset representation; and
  - exact positive decimal price value.

Target and count make the source state independently interpretable. Count also
distinguishes structural boundaries in the ordered sequence. The empty state is
encoded with the exact target and an explicit zero count, so it has a stable,
unambiguous identity.

SQLite row identifiers, insertion order, natural database order, database file
location, provider DTO identity, provider response order, and execution time
are excluded.

## Snapshot identity and dataset version

Snapshot identity is derived from:

1. the explicit snapshot identity-scheme version;
2. the Dataset Definition Identity; and
3. the Source-State Identity.

The ordered content need not be duplicated in snapshot digest input because it
is already committed by the source-state identity. Stored or returned snapshot
content must nevertheless agree with both identities; disagreement is an
integrity conflict.

The resulting Dataset Snapshot Identity is also the authoritative Release 1.2
Dataset Version within the logical Research Dataset Identity. Dataset versions
are therefore immutable semantic/content-derived versions. A catalog may later
display a human-friendly sequence or creation timestamp, but neither is an
authoritative version and neither affects identity.

Equivalent re-materialization creates a new operational execution event but
resolves to the same logical dataset identity, source-state identity, snapshot
identity, and dataset version. It does not create new semantic research
evidence solely because it ran again.

## Relevant and irrelevant change semantics

The following changes produce a distinguishable source state and snapshot when
they affect the selected interval:

- an observation is added inside `[from, to)`;
- selected membership or deterministic order changes;
- a selected observation's decimal value differs in a legitimately distinct
  authoritative source state; or
- a selected observation's original offset representation differs.

The following do not change the snapshot identity:

- an observation is added outside `[from, to)`;
- history for another target changes;
- insertion or physical row order changes while semantic order does not;
- a database file moves or a connection string changes;
- execution time, duration, machine, or process changes; or
- equivalent boundary display offsets denote the same semantic instants.

A definition-semantic change always creates a distinguishable Definition
Identity, Research Dataset Identity, and Snapshot Identity. Existing identities
are never reassigned and old snapshots are never overwritten.

## Canonical semantic representation

Release 1.2 selects a normative, versioned canonical semantic representation
as the input to deterministic fingerprints. It is independent of database,
provider, JSON, and platform-native binary layouts.

### Identity scheme

The initial scheme is named `aiq-dataset-identity-v1`. Every carried identity
must retain its scheme name. The scheme fixes SHA-256 and the following
canonicalization rules.

### Encoding rules

- Values are encoded as UTF-8 bytes without a byte-order mark.
- Each identity structure begins with its exact ASCII type-domain tag and
  scheme name.
- Fields occur only in the normative order defined below.
- Every variable-length byte value is preceded by its non-negative decimal byte
  length and a colon; structures also carry explicit field and sequence counts.
- Integers use invariant base-10 ASCII with no leading zero except zero itself.
- Enumeration/rule values use fixed lowercase ASCII tokens defined by the
  scheme.
- No locale-sensitive formatting, insignificant whitespace, property ordering,
  provider JSON, or platform-native binary layout participates.

### Value canonicalization

- **Target:** exact UTF-8 value; no trimming, case folding, normalization, or
  symbol rewriting.
- **Boundary semantic instant:** signed UTC ticks in invariant base-10 form;
  its display offset is excluded because the boundary denotes an instant.
- **Source observation instant:** signed UTC ticks plus signed offset minutes;
  both are required to preserve absolute instant and original offset.
- **Decimal price:** sign, base-10 coefficient, and scale after removing
  redundant trailing coefficient zeros; numerically equal decimal values have
  one representation. Scientific notation and floating-point conversion are
  prohibited.
- **Sequence:** explicit count followed by observations in strict ascending
  semantic-instant order. An empty sequence is count zero with no elements.

### Normative field order

- `definition`: scheme, target, lower UTC ticks, upper UTC ticks,
  `lower-inclusive`, `upper-exclusive`, `instant-ascending`, parameter count,
  then parameters in scheme-defined ordinal key order.
- `research-dataset`: scheme, canonical definition fields.
- `source-state`: scheme, target, observation count, then each observation's
  UTC ticks, offset minutes, and normalized decimal components.
- `snapshot`: scheme, typed Definition Identity, typed Source-State Identity.

WP03 defines these canonical semantic bytes because deterministic content
identity requires an unambiguous input. It does not define physical storage or
Application type shapes.

## Digest and textual identity decision

SHA-256 is selected because it is a standard, non-secret cryptographic hash
available in the .NET platform without an added package and operates fully
offline. Random identifiers, GUIDs, sequences, and timestamps cannot recognize
equivalent semantic content. Carrying raw canonical content as identity is
deterministic but unnecessarily large and awkward for later contracts and
catalog lookup.

The normative textual fingerprint is lowercase hexadecimal with exactly 64
characters. A complete identity is interpreted only with its identity type and
scheme, conceptually:

```text
<identity-type>:aiq-dataset-identity-v1:sha256:<64-lowercase-hex>
```

The digest is compact identity evidence, not a signature, credential, or
security boundary.

## Scheme evolution and collision semantics

Identity scheme, representation version, and digest algorithm are explicit.
Snapshots retain their original scheme permanently. A future scheme never
reinterprets an old identifier. Cross-scheme equivalence, if needed, requires
comparison of preserved semantics or separately authorized migration; WP03
does not define a migration framework.

Under one scheme, equal fingerprints are expected to represent semantically
equivalent content. If preserved metadata or content contradicts an equal
fingerprint, the condition is an integrity conflict. Implementations must fail
deterministically, preserve both bodies of evidence, and never overwrite or
silently alias materially different definitions, source states, or snapshots.
WP11 owns concrete validation and failure mapping.

## Provenance semantics

For every snapshot it must be possible to know:

- its Research Dataset Identity, Dataset Definition Identity, Source-State
  Identity, Snapshot Identity, and Dataset Version;
- the exact definition semantics, target, and `[from, to)` boundaries;
- source authority: accepted Release 1.1 historical observations;
- the selected observation count;
- the ordered semantic content or sufficient preserved evidence to reconstruct
  and verify the Source-State Identity;
- the identity scheme, canonicalization version, and digest algorithm; and
- that the materialized content agrees with its semantic identities.

Execution timestamp, duration, host, process, diagnostic correlation, and
storage location may later be audit metadata. They are not semantic provenance
inputs and never affect identity or version.

## Lineage semantics

Each snapshot derives from exactly one dataset definition and exactly one
relevant source state. That source state consists of zero or more accepted
Release 1.1 observations selected by the definition. The definition and source
state together fully explain the semantic snapshot.

An empty snapshot retains lineage to its definition and to an explicitly empty
source state with a deterministic Source-State Identity. Lineage does not
require one physical record per observation; WP06 through WP09 may choose a
lossless representation without weakening this relationship.

## Identity and immutability invariants

1. Same semantic definition under the same scheme yields the same Definition
   Identity and Research Dataset Identity.
2. Same exact target and relevant ordered source content under the same scheme
   yields the same Source-State Identity.
3. Same Definition Identity and Source-State Identity under the same scheme
   yields the same Snapshot Identity and Dataset Version.
4. Operational metadata changes alone never change semantic identity.
5. A relevant source semantic change yields distinguishable source-state and
   snapshot identities.
6. A definition semantic change yields distinguishable definition, logical
   dataset, and snapshot identities.
7. An accepted snapshot identity is immutable and is never reassigned to
   different semantic content.
8. New versions never overwrite old snapshots.
9. A valid empty materialization has deterministic identities and version.
10. Persisted or returned content that contradicts its identities is an
    integrity conflict, never an overwrite opportunity.

## Metadata and catalog boundary

WP06 must make these facts catalog-visible: Research Dataset Identity, Dataset
Definition Identity, Snapshot Identity/Dataset Version, Source-State Identity,
identity scheme, target, selection boundaries, observation count, source
authority, and provenance/lineage relationships.

WP03 does not define catalog records, indexes, lookup APIs, pagination, query
behavior, storage layout, or lifecycle presentation metadata.

## Application and physical-storage boundaries

WP04 must provide provider- and storage-independent Application values capable
of carrying the four typed identities, scheme information, exact definition
semantics, immutable snapshot/version semantics, required provenance, lineage,
coverage/count, and valid empty snapshot results. Its contracts must preserve
the equivalence and distinguishability invariants above without exposing
SQLite, provider, filesystem, or generated database identifiers.

WP05 owns materialization orchestration. WP06 owns catalog metadata objects.
WP07 owns lossless physical representation but not identity meaning. WP08 owns
immutable snapshot persistence. WP09 owns catalog persistence and lookup. WP10
owns integration consistency. WP11 owns validation/failure mapping. WP12 owns a
bounded execution path. WP13/WP14 own permanent tests.

No table, column, SQLite type, index, foreign key, DDL, migration, or file path
is specified here.

## Alternatives and decisions

| Decision | Alternatives | Selected option and rationale | Deferred implementation |
| --- | --- | --- | --- |
| Identity source | Random/GUID/sequential; semantic | Deterministic semantic identity recognizes equivalent offline materializations | Computing values: WP05 or owning later implementation |
| Identity layers | Conflate definition and snapshot; separate | Separate logical definition/dataset, source-state, and snapshot identities because definitions survive source evolution | Application shapes: WP04 |
| Version | Sequence; execution timestamp; content-derived | Snapshot fingerprint is the authoritative immutable semantic version | Optional display sequence: WP06 |
| Source-state basis | Physical rows; semantic observations | Exact ordered observation content preserves provider/storage independence | Persistence: WP07/WP08 |
| Equivalent rerun | New semantic version; same version | Same semantic identity/version; only operational event differs | Idempotent behavior: WP08/WP10 |
| Operational metadata | Include; exclude | Excluded because it does not change research meaning | Audit metadata: later bounded ownership |
| Canonicalization | Implicit; explicit versioned | Explicit scheme prevents locale/platform ambiguity and supports evolution | Executable encoder: later owning implementation |
| Identity representation | Raw semantic key; SHA-256 digest | Typed versioned SHA-256 is compact, deterministic, offline, and package-free | Concrete value types: WP04 |

## WP04 handoff

WP04 receives these settled requirements:

- four distinct typed concepts: Dataset Definition Identity, Research Dataset
  Identity, Source-State Identity, and Dataset Snapshot Identity;
- Dataset Version is the immutable Snapshot Identity within one logical
  Research Dataset Identity;
- equivalent definition and source state produce the same semantic identities
  and version, including an explicitly empty source state;
- any semantic definition or relevant selected-source change produces a
  distinguishable snapshot identity;
- identity values carry type, scheme, algorithm, and lowercase digest text;
- provenance carries enough definition/source evidence to explain and verify a
  snapshot;
- lineage relates one snapshot to one definition and one source state of zero
  or more accepted observations;
- original observation offset and decimal fidelity remain normative;
- operational metadata is excluded from semantic identity; and
- identities are provider-, storage-, and machine-independent and immutable.

WP04 owns interface and value-type shapes, not these semantics. Later work
packages retain their boundaries: WP05 orchestration, WP06 catalog model, WP07
physical schema, WP08 snapshot persistence, WP09 catalog persistence/lookup,
WP10 integration, WP11 validation, WP12 composition, and WP13/WP14 tests.

## Release 1.3 exclusion

These semantics do not authorize scheduled refresh, continuous ingestion,
event-driven re-materialization, pipeline graphs, background monitoring,
feature generation, or Release 1.3 orchestration.
