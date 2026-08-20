# Research Dataset Definition and Reproducibility Model

**Status:** Accepted for Release 1.2 WP02

**Scope:** Research Dataset Foundation

**Technology boundary:** Provider-, storage-, and implementation-independent

## Purpose

This document defines the semantic foundation for Release 1.2 research
datasets. It reuses the accepted Release 1.1 historical-observation truth and
settles what a dataset, definition, materialization, and snapshot mean before
later work packages choose identifiers, contracts, catalog models, or physical
storage.

## Authoritative source foundation

Release 1.1 persisted `PriceObservation` values are the authoritative source
material for Release 1.2 datasets. Each source observation belongs to one exact
target and carries:

- a semantic instant represented by `DateTimeOffset`, including its original
  offset representation;
- a positive `decimal` price with exact decimal fidelity; and
- an identity defined by exact target plus absolute semantic instant.

Source history is immutable and append-oriented. An exact repeat is
idempotent; a different price or offset representation for an existing target
and semantic instant is a conflict. Retrieval returns observations in strictly
ascending semantic-instant order and may succeed with an empty result.

Provider DTOs, HTTP payloads, database rows, row identifiers, insertion order,
and natural database order are not dataset source truth. Dataset
materialization neither rewrites nor repairs accepted source history.

## Vocabulary

### Research Dataset

A research-owned, deterministic materialization of explicitly selected,
accepted historical market observations together with the semantic metadata
needed to explain how the materialization was produced. It is a durable
research artifact, not a live query, provider response, mutable working set, or
implicit view of "latest" data.

### Dataset Definition

The declarative semantic intent that determines a dataset: one exact target,
explicit time-selection boundaries, their inclusion rules, deterministic
ordering, and any explicitly authorized deterministic selection parameters.
Release 1.2 introduces no transformation, enrichment, feature, label, missing-
value repair, or provider-specific rule into this definition.

### Dataset Materialization

The deterministic resolution of a valid dataset definition against a fixed,
authoritative state of accepted source observations. The term also describes
the semantic result of that resolution before a physical representation is
chosen.

### Dataset Snapshot

The durable, immutable representation of one successful dataset
materialization. A snapshot preserves research evidence even if source history
later changes. WP08 owns its persistence mechanics.

### Source Observation

An accepted Release 1.1 historical `PriceObservation` associated with its exact
target. It is normalized platform data, not provider transport data.

### Selection Boundary

An explicit semantic-instant limit used to select source observations. Release
1.2 uses an inclusive lower bound and an exclusive upper bound: `[from, to)`.

### Determinism

The property that fixed semantic inputs produce the same ordered semantic
content and semantic metadata, without influence from execution environment or
incidental ordering.

### Reproducibility

The ability to materialize semantically equivalent dataset content and
explanatory semantic metadata when given the same dataset definition and the
same authoritative source-observation state relevant to that definition.

### Equivalent Materialization

Two successful materializations are equivalent when their definition
semantics, relevant source-state semantics, ordered observations, and semantic
metadata are equal as defined below. Operational execution facts do not affect
equivalence.

### Dataset Metadata

Descriptive semantic information required to understand a materialization,
including its target, selection coverage, ordering rule, content cardinality,
and the definition and source-state facts needed for explanation.

### Provenance

Evidence identifying the authoritative dataset definition and accepted source-
observation state from which a materialization arose. WP03 owns its detailed
semantics and representation.

### Lineage

The relationship from a materialized dataset to the exact selected source
observations and semantic inputs that contributed to it. WP03 owns its detailed
semantics and representation.

### Catalog

The discoverable record or index of materialized datasets and their metadata;
it describes assets and does not define or contain their observation content.
WP06 owns the catalog model and WP09 owns persistence and lookup.

## Dataset definition model

A Release 1.2 dataset definition has these semantic inputs:

| Input | Requirement |
| --- | --- |
| Target | Exactly one non-ambiguous target, matched using the accepted exact target semantics |
| Lower boundary | Required semantic instant; inclusive |
| Upper boundary | Required semantic instant; exclusive and strictly later than the lower boundary |
| Ordering | Strictly ascending by source observation semantic instant |
| Parameters | Only deterministic parameters explicitly authorized by Release 1.2; none are added by WP02 |

Both time boundaries are required. Unbounded sides and implicit wall-clock
values are excluded because they would leave the selected source state
ambiguous. Multiple targets are outside the Release 1.2 minimum slice; a
separate single-target definition is required for each target.

The selection includes precisely the accepted observations for the exact target
whose semantic instants satisfy `lower <= instant < upper`. Selection never
depends on insertion order, provider order, database row order, locale, or the
time at which materialization runs.

## Materialization and snapshot model

Materialization evaluates a definition against a fixed view of the relevant
accepted source history, selects matching observations, and orders them by
semantic instant. It performs no acquisition, normalization, correction,
aggregation, enrichment, feature engineering, or imputation.

A successful selection containing zero observations is a valid empty
materialization. This preserves Release 1.1 successful-empty retrieval
semantics and distinguishes "no matching accepted observations" from an invalid
definition or operational failure.

Once accepted, the resulting snapshot is immutable. It may not be overwritten
or amended. Re-materialization may recognize an equivalent result, but must not
rewrite existing research evidence. A materially different result remains a
distinct snapshot; older snapshots remain valid historical evidence.

## Reproducibility model

Given the same authoritative dataset definition and the same authoritative
source-observation state relevant to that definition, materialization must
produce semantically equivalent ordered content and explanatory semantic
metadata independent of:

- execution time or wall-clock `now`;
- process identity, machine identity, machine path, or connection string;
- random values;
- provider response order or provider current state;
- database natural row-return order or insertion order;
- unordered collection enumeration;
- locale or current culture;
- local machine timezone or implicit timezone conversion;
- floating-point conversion; and
- environment-specific operational configuration.

Source `DateTimeOffset` values and their original offsets are preserved without
conversion or truncation. Source decimal values remain decimal and are
preserved without binary floating-point conversion or rounding.

### Input classification

| Candidate input | Classification |
| --- | --- |
| Dataset definition and its explicit parameters | Required semantic input |
| Exact target | Required semantic input |
| Inclusive lower and exclusive upper boundaries | Required semantic input |
| Relevant accepted source observations | Required semantic input |
| Observation semantic instants and original offsets | Required semantic input |
| Observation decimal values | Required semantic input |
| Strict ascending semantic-instant ordering rule | Required semantic input |
| Explicit deterministic filters or transformations | Deferred; none are authorized by WP02 |
| Content count and actual selected coverage | Explanatory semantic metadata |
| Definition/source-state provenance and lineage | Required to be knowable; detailed representation deferred to WP03 |
| Execution timestamp and duration | Operational metadata only |
| Process/machine identity, path, row ID, or connection string | Operational metadata only; excluded from semantics |
| Provider DTO, HTTP details, and provider ordering | Out of scope and excluded from semantics |
| Canonical bytes, identifier, version, or digest | Deferred to WP03 |

Operational metadata may be recorded later for diagnostics, but must be kept
separate from semantic content and must not determine semantic identity or
equivalence.

## Ordering and semantic content

The canonical semantic content is the selected single-target observation
sequence in strictly ascending absolute semantic-instant order. Each element
preserves the exact instant, original offset representation, and decimal price
from source truth. No duplicate target-plus-semantic-instant identities may be
introduced. Physical record order and serialization are not authoritative.

Because the Release 1.2 slice is single-target, no cross-target ordering rule is
needed. Multi-target composition is not implied and remains outside this model.

## Equivalent materialization semantics

Two materializations are semantically equivalent only when all of these are
equal:

1. the complete dataset-definition semantics, including exact target,
   boundaries, inclusion rules, ordering, and authorized parameters;
2. the relevant authoritative source-state semantics;
3. the ordered sequence of source observation identities;
4. each observation's exact semantic instant, original offset, and decimal
   value; and
5. explanatory semantic metadata derived from those inputs and outputs.

Execution timestamp, duration, process or machine identity, machine paths,
connection strings, diagnostic correlation values, physical row identifiers,
and storage layout are excluded from semantic equivalence.

WP03 must choose how equivalence and distinction are represented. WP02 does not
select canonical bytes, identifier shapes, version shapes, serialization, or a
digest algorithm.

## Source-history change model

The dataset definition is stable independently of source-history state. A
materialization resolves that definition against the authoritative source state
available to that materialization. The resulting snapshot permanently records
the semantic result and must remain explainable from provenance and lineage.

If accepted source history later gains observations relevant to the same
definition, a later materialization may be materially different. It must not
replace the earlier snapshot. If the relevant source state and definition are
semantically unchanged, a later materialization is equivalent even when its
operational execution metadata differs.

Release 1.1 conflict rules prohibit silently changing an accepted observation.
If a future governed correction mechanism creates a changed authoritative
source state, both the old and new materializations must remain distinguishable
and the older snapshot must remain immutable research evidence.

## Metadata, provenance, lineage, and catalog boundary

For every materialization it must be possible to know:

- the complete dataset-definition semantics;
- the exact target and requested selection interval;
- the actual selected coverage, including an explicit empty result;
- the deterministic ordering rule and observation count;
- the exact selected source identities and their preserved values;
- the authoritative source-state facts needed to reproduce the result;
- whether a later materialization is equivalent or materially different; and
- the relationship between the snapshot, its definition, and its sources.

WP03 defines identity, version, provenance, and lineage semantics and chooses
their representations. WP06 defines catalog metadata objects. WP07 and WP08 own
physical schema and snapshot persistence. WP09 owns catalog persistence and
lookup. These later work packages may represent this required knowledge but may
not weaken it.

## Validation and failure concept boundary

These concepts are distinct:

- **Invalid definition:** target or boundaries are ambiguous, missing, or
  unsupported; lower is not earlier than upper; or an input is inherently
  non-deterministic.
- **Valid empty materialization:** the definition is valid and source access
  succeeds, but no accepted observations satisfy the selection.
- **Source-data semantic failure:** required source fidelity or uniqueness
  cannot be established without altering source truth.
- **Operational materialization failure:** an execution dependency prevents a
  result from being produced.

WP02 creates no public failure types. WP11 owns validation and failure mapping.

## Decisions and alternatives

| Decision | Alternatives considered | Selected option and rationale | Consequence / deferred work |
| --- | --- | --- | --- |
| Dataset form | Live query; durable artifact | Durable materialization because research evidence must survive source evolution | Persistence mechanics deferred to WP08 |
| Mutability | Mutable working set; immutable snapshot | Immutable snapshot to preserve reproducibility and auditability | Re-materialization cannot overwrite evidence |
| Source inputs | Implicit current state; fixed relevant source state | Fixed semantic inputs avoid time-dependent results | WP03 represents source-state provenance |
| Equivalence | Include execution metadata; semantic inputs/content only | Exclude operational metadata because machines and execution times must not alter meaning | Operational facts remain diagnostic only |
| Re-materialization | Overwrite; preserve snapshots | Preserve historical snapshots to explain prior research | WP03 distinguishes materially different results |
| Empty selection | Invalid; valid empty | Valid empty, consistent with Release 1.1 retrieval and deterministic selection | Empty is distinct from failure |
| Source truth | Provider transport; accepted platform observations | Accepted Release 1.1 observations preserve provider independence and validated fidelity | Provider data remains outside dataset semantics |
| Time boundaries | Unbounded/implicit; explicit closed interval; explicit half-open interval | Required `[from, to)` interval is deterministic and composes without boundary overlap | Concrete contract representation deferred to WP04 |
| Target scope | Multiple targets; one exact target | Single target is the minimum authorized slice and avoids speculative cross-target semantics | Future expansion requires separate authority |

## Release 1.3 exclusions

This model does not authorize continuous ingestion, scheduled refresh,
event-driven re-materialization, automatic recomputation, stream processing,
pipeline DAG orchestration, background monitoring, production scheduling, or
pipeline retry/resilience. Release 1.2 WP12 may later demonstrate one bounded
execution path; it does not establish pipeline infrastructure.

## WP03 handoff

WP03 receives these settled semantic requirements:

- the dataset definition, a materialization, and its immutable snapshot require
  distinguishable identity concepts;
- equivalence is determined by definition semantics, relevant source-state
  semantics, ordered observations, preserved offset/decimal fidelity, and
  semantic metadata;
- any material change to the definition or relevant source state must remain
  distinguishable;
- provenance must explain the authoritative definition and source state;
- lineage must relate the snapshot to its exact selected source observations;
- accepted snapshots and prior research evidence remain immutable; and
- operational metadata must not affect semantic identity.

WP03 must leave representations provider- and storage-independent. It owns the
identifier and version shapes, digest/hash choice if any, canonical
serialization if any, provenance encoding, and lineage identifier encoding.
Physical persistence remains deferred to WP07+.

## Acceptance statement

This model defines a small, deterministic Release 1.2 research dataset without
changing Release 1.1 observation truth. It settles semantic meaning while
deliberately leaving representation, contracts, catalog structures, storage,
execution, and pipelines to their owning work packages.
