# Feature Engineering Semantics

## Purpose

This document freezes the Release 1.4 semantics of the single built-in
feature transformation, `simple-return-lag-1-v1`. It defines a deterministic,
provider- and storage-independent transformation over one already accepted,
immutable research dataset snapshot. It does not define implementation APIs,
identity encodings, persistence, orchestration extensions, or additional
features.

## Release boundary

Release 1.4 supports one explicit, one-shot feature request with this flow:

```text
explicit feature request
  -> exact immutable dataset snapshot lookup
  -> snapshot and evidence validation
  -> deterministic simple-return-lag-1-v1 transformation
  -> immutable in-memory feature evidence
  -> structured result
```

The input is an existing accepted snapshot. Feature generation does not call a
provider, acquire observations, rematerialize a dataset, mutate source data, or
persist its output.

## Predecessor foundations

Release 1.1 supplies accepted historical observations with a target, a
`DateTimeOffset` whose semantic instant and original offset are preserved, and
a positive `decimal` price. Retrieval is deterministically ordered and may
succeed with no observations. Provider and HTTP behavior remain outside these
semantics, while SQLite mechanics remain Infrastructure-owned.

Release 1.2 supplies the provider-independent `DatasetDefinition`, target and
`[from, to)` selection semantics, deterministic materialization, dataset and
source-state identities, immutable snapshot identity and version, provenance,
lineage, strict semantic-instant ordering, empty snapshots, exact catalog
lookup, equivalence, and integrity-conflict semantics.

Release 1.3 supplies the Application-owned fixed five-stage, one-shot research
pipeline, structured semantic evidence, and first-failure behavior. It adds no
scheduler, retry loop, configurable DAG, or durable run history.

## Feature-engineering vocabulary

- **Feature definition:** the semantic transformation selected by a request.
  Release 1.4 has exactly one: `simple-return-lag-1-v1`.
- **Feature value:** one derived decimal return associated with the current
  observation of an adjacent input pair.
- **Feature set:** the immutable, ordered, in-memory feature evidence produced
  for one exact snapshot and one exact feature definition.
- **Transformation:** deterministic derivation of feature values from accepted
  snapshot observations without changing those observations.
- **Lag 1:** adjacency in the snapshot's accepted order, not a configurable
  duration or arbitrary lag.
- **Return:** the simple arithmetic price return defined below; it is not a log
  return, total return, trading result, signal, prediction, or recommendation.
- **Signal:** a trading or analytical decision concept and not an implemented
  Release 1.4 feature value.
- **Enrichment, feature-ready, and experiment-ready:** broader roadmap terms.
  They do not authorize extra transformations, lifecycle stages, persistence,
  experimentation, or model behavior in Release 1.4.

## Input snapshot boundary

A request identifies one exact accepted immutable dataset snapshot and the
single supported feature definition. Lookup is by exact accepted snapshot
identity; there is no implicit latest-snapshot selection. The snapshot's
definition, identity, version, observations, coverage, provenance, and lineage
must be mutually consistent before computation.

`NotFound` means that no accepted snapshot exists for the requested exact
identity. It is distinct from an existing accepted snapshot whose observation
collection is empty. A lookup failure triggers neither provider acquisition nor
dataset rematerialization.

## `simple-return-lag-1-v1`

For each ordered position `i > 0`, the feature value is:

```text
r[i] = (p[i] / p[i-1]) - 1
```

Here `p[i]` is the exact accepted `decimal` price of snapshot observation `i`.
The adjacent predecessor is observation `i-1` in the same snapshot. The
produced value belongs to current observation `i`. The transformation's exact
semantic name and version are `simple-return-lag-1-v1`.

No log return, rolling return, configurable lag, alternative formula, or
second built-in definition is included.

## Ordering semantics

The transformation consumes the snapshot's accepted strict ascending
semantic-instant order. It does not reorder by provider order, insertion or
database row order, culture, local timezone, hash enumeration, filesystem
state, or execution time. It introduces no new deduplication rule.

For `N >= 2`, output order follows the adjacent pairs in input order:

```text
(p[0], p[1]) -> r[1]
(p[1], p[2]) -> r[2]
...
(p[N-2], p[N-1]) -> r[N-1]
```

## Timestamp and offset semantics

Feature value `r[i]` uses the timestamp of current observation `i`, including
its accepted semantic instant and original offset representation. It never uses
the predecessor timestamp, wall-clock execution time, a synthetic timestamp,
or an offset-destroying normalization.

## Decimal and numeric semantics

All prices and returns have decimal semantics. Computation must not convert to
binary floating point, depend on culture, apply a convenience rounding rule,
clamp a result, or substitute an approximate value.

If division or subtraction cannot produce a value representable by the
platform's supported decimal semantics, the input is invalid numeric evidence.
No partial result is returned. WP07 will map this semantic distinction without
WP02 selecting implementation exception types.

An accepted `PriceObservation` requires a positive price, so a zero predecessor
cannot occur in coherent accepted snapshot evidence. If contradictory evidence
nevertheless presents `p[i-1] = 0`, the requested return is undefined and the
case is invalid numeric input/evidence. It must not yield infinity, NaN, zero, a
sentinel, a skipped pair, or partial success.

## Empty and single-observation semantics

An existing accepted empty snapshot succeeds with an empty feature set. An
existing accepted one-observation snapshot also succeeds with an empty feature
set because no adjacent pair exists. These cases are not `NotFound` and are not
failures.

## Multi-observation semantics

For `N >= 2` valid ordered observations, successful computation yields exactly
`N - 1` ordered feature values. Every value corresponds to one adjacent pair.
Any invalid pair prevents successful completion of the entire request; Release
1.4 has no partial-success result.

## Determinism

Equivalent accepted snapshot evidence and the same feature definition produce
semantically equivalent feature evidence. Results cannot depend on wall-clock
time, duration, process or machine identity, random values, correlation IDs,
paths, connection strings, logging configuration, culture, local timezone,
collection implementation, or execution environment.

WP03 owns the exact canonical representation and fingerprint encoding. WP02
does not freeze bytes, delimiters, digest composition, or identity types.

## Immutability

The source snapshot remains unchanged. The resulting feature evidence is an
immutable in-memory value for the request. There is no overwrite, mutable
feature version, cache mutation, catalog mutation, or persisted feature state.

## Provenance and lineage

A feature set must remain traceable to:

- the exact `simple-return-lag-1-v1` feature definition;
- the exact accepted dataset snapshot identity and version;
- the snapshot's accepted dataset definition and ordered observations; and
- the predecessor dataset/source-state provenance and lineage already carried
  by the snapshot.

Lineage is acyclic: feature evidence derives from one accepted snapshot and
does not become its own ancestor or redefine dataset identity. WP03 owns the
exact identity, provenance, lineage, and evidence representations.

## Success semantics

Success includes a non-empty feature set, an empty feature set from an empty or
single-observation snapshot, and semantically equivalent recomputation. Success
does not require persistence. Release 1.2 persistence dispositions such as
`NewlyAccepted` and `EquivalentExisting` are not feature-generation outcomes.

## Failure semantic inventory

Later contracts must keep these situations distinguishable:

- invalid request;
- unsupported feature definition;
- exact snapshot `NotFound`;
- unavailable dependency;
- invalid predecessor snapshot or provenance evidence;
- invalid numeric input/evidence, including zero prior price or unrepresentable
  decimal arithmetic;
- integrity contradiction; and
- unknown or unrelated programming defects, which propagate distinctly from
  expected failures.

Failures are fail-stop. There are no retries, fallback data, recovery
orchestration, pair skipping, or partial-success results.

## Provider and storage boundary

Feature semantics contain no Twelve Data, HTTP, SQL, SQLite API, database path,
credential, connection lifetime, provider ordering, or provider failure detail.
Application semantics consume provider- and storage-independent accepted
snapshot evidence; Infrastructure retains physical lookup and storage mechanics.

## Relationship to the Release 1.3 pipeline

The Release 1.3 topology remains exactly:

1. historical observation retrieval;
2. dataset materialization;
3. immutable snapshot persistence;
4. catalog registration; and
5. structured pipeline result/evidence.

Feature generation begins only after an accepted immutable snapshot exists. It
is a separate one-shot Application use case, not a sixth pipeline stage. It
does not change pipeline topology, identity, evidence, or execution behavior.

## Schema and persistence decision

SQLite remains schema version 2. Release 1.4 feature output is reproducible
in-memory evidence. No schema v3, feature table, feature catalog, feature
history, run-history table, checkpoint, scheduler state, or cache is created.

## Explicit deferrals

Release 1.5 or later may separately consider feature persistence and catalogs,
multiple indicators, configurable lag, arbitrary formulas, rolling indicators,
plugins, configurable feature DAGs, live acquisition orchestration, scheduling,
refresh loops, retries, circuit breakers, fallback, checkpoints, resume,
durable execution history, notebooks, workspaces, strategies, portfolio/risk,
backtesting, model training, ML/MLOps, distributed or streaming execution, and
metrics or tracing backends. This release creates no placeholders for them.

## Ownership and WP03+ handoff

Application owns the provider- and storage-independent feature semantics and
the later bounded use case. Domain remains unchanged unless a later authority
proves a genuinely domain-wide invariant. Infrastructure retains snapshot
lookup and physical storage mechanics; Worker remains a later composition and
one-shot execution concern.

WP03 may define exact Feature Definition and Feature Set identities,
provenance, lineage, equivalence evidence, and canonical encoding without
changing this semantic boundary. WP04 and later work packages may translate
the frozen semantics into models, contracts, computation, validation,
integration, composition, Worker behavior, and tests only under their separate
authorities.
