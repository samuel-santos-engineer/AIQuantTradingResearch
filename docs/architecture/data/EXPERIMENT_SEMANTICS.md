# Experiment Semantics

## Purpose

This document freezes the Release 1.5 semantics of the single built-in
experiment, `simple-return-descriptive-summary-v1`. The experiment consumes one
accepted immutable Release 1.4 Feature Set and produces deterministic,
immutable, in-memory descriptive evidence. It defines semantic behavior, not
concrete contracts, canonical identity encoding, persistence, hosting, or a
general experiment framework.

## Release boundary

Release 1.5 supports one explicit, bounded, one-shot experiment flow:

```text
explicit experiment request
  -> obtain one exact accepted simple-return-lag-1-v1 Feature Set
  -> validate definition and Feature Set evidence
  -> compute deterministic descriptive summary evidence
  -> establish immutable experiment result evidence
```

The input is accepted Application evidence. The experiment does not acquire
market data, reconstruct a dataset, persist a Feature Set or result, modify the
Release 1.3 pipeline, or create an operational experiment history.

## Predecessor foundations

Release 1.1 remains authoritative for immutable historical-observation truth,
positive decimal price fidelity, `DateTimeOffset` semantic-instant and original
offset fidelity, deterministic ordering, successful empty retrieval, and
bounded source failures.

Release 1.2 remains authoritative for dataset definitions, exact immutable
snapshot identity and version, ordered snapshot evidence, exact lookup,
`NotFound` versus an existing empty snapshot, equivalence, integrity conflict,
provenance, and acyclic lineage.

Release 1.3 remains authoritative for the fixed five-stage sequential one-shot
research pipeline, `aiq-pipeline-identity-v1`, structured pipeline evidence,
first-failure termination, and unknown-defect propagation. Experiment
execution is separate from that pipeline.

Release 1.4 remains authoritative for `simple-return-lag-1-v1`,
`aiq-feature-identity-v1`, exact snapshot/version binding, current-observation
timestamp and offset ownership, ordered immutable Feature Set evidence,
decimal-only feature computation, equivalent recomputation, bounded feature
failures, and successful empty Feature Sets.

## Vocabulary

- **Experiment Definition:** the semantic calculation selected by the request.
  Release 1.5 defines exactly `simple-return-descriptive-summary-v1`.
- **Experiment Request:** the explicit instruction to evaluate the built-in
  definition over one exact Feature Set derived through the accepted feature
  boundary.
- **Experiment Result:** immutable successful semantic evidence containing the
  summary and its required definition, input, provenance, and lineage
  references.
- **Descriptive Summary:** count and, when count is positive, arithmetic mean,
  minimum, and maximum of the complete accepted Feature Set values.
- **Semantic evidence:** identity-bearing facts that determine or explain the
  meaning and reproducibility of a result.
- **Operational evidence:** invocation time, duration, process, machine,
  correlation, logging, and presentation facts that do not determine result
  meaning or equivalence.
- **Equivalent recomputation:** evaluation of the same definition over
  equivalent exact Feature Set evidence yielding equivalent summary evidence.
- **Integrity contradiction:** an assertion that equal semantic identity refers
  to contradictory accepted semantic content.

## Exact experiment definition

The only built-in definition is:

`simple-return-descriptive-summary-v1`

It consumes exactly one accepted immutable Feature Set produced under
`simple-return-lag-1-v1`. It produces descriptive evidence only. It is not a
strategy, signal, forecast, prediction, backtest, risk or portfolio model,
optimization, statistical-inference engine, or machine-learning model.

The formula and evidence set are fixed. Release 1.5 introduces no configurable
statistics, aliases, plugins, formula language, or experiment registry.

## Accepted Feature Set input

The semantic input is the complete accepted Feature Set supplied through the
provider- and storage-independent Application boundary. The input preserves:

- Feature Definition Identity;
- Feature Set Identity;
- the exact Dataset Snapshot Identity and Dataset Version;
- ordered Feature Values;
- each value's current-observation timestamp and original offset;
- exact decimal values;
- accepted provenance and acyclic lineage.

The experiment does not independently perform provider acquisition, dataset
materialization, snapshot discovery, or physical storage access. WP07 owns the
later orchestration that obtains exact Feature Set evidence through the
existing Release 1.4 use case.

Different accepted Feature Set identities remain distinct inputs even when
their ordered values or computed summaries happen to be numerically equal.

## Count semantics

Count is the exact cardinality of the accepted Feature Set values. It is a
non-negative integer:

```text
empty Feature Set: count = 0
Feature Set with N values: count = N
```

Every accepted value participates. The experiment performs no filtering,
sampling, deduplication, missing-value removal, pair skipping, or silent value
exclusion.

## Empty Feature Set

An accepted empty Feature Set is successful input. Its summary is:

```text
count = 0
arithmetic mean = absent
minimum = absent
maximum = absent
```

Absence is semantic and must not be represented by zero, NaN, infinity, a
sentinel decimal, a fabricated Feature Value, or failure. The successful empty
result remains bound to the exact Feature Set and its provenance.

## Non-empty Feature Set

For the complete accepted ordered values:

```text
x[0], x[1], ..., x[N-1]
```

where `N >= 1`, the summary is:

```text
count = N
arithmetic mean = (x[0] + x[1] + ... + x[N-1]) / N
minimum = min(x[0], x[1], ..., x[N-1])
maximum = max(x[0], x[1], ..., x[N-1])
```

For one value, count is one and mean, minimum, and maximum are that exact
value. No median, variance, standard deviation, quantile, cumulative return,
annualization, Sharpe ratio, confidence interval, or additional statistic is
part of this definition.

## Decimal and numeric semantics

All summary arithmetic uses the accepted decimal computation boundary. It must
not convert Feature Values to binary floating point, apply convenience
rounding, clamp or saturate a result, depend on culture, or substitute an
approximate representation.

If required addition or division cannot produce a representable accepted
decimal result, computation fails as invalid numeric evidence/computation. It
must not return a partial summary, rounded fallback, floating-point fallback,
NaN, infinity, skipped values, or fabricated success. WP05 and WP06 own the
later computation mechanism and bounded failure mapping.

## Ordering, timestamp, and offset rules

The Feature Set remains ordered predecessor evidence. The experiment consumes
it as a whole and does not sort, reorder, deduplicate, or canonicalize values as
a new input transformation.

Count, mean, minimum, and maximum may be numerically permutation-insensitive,
but that fact does not erase Feature Set identity, ordered evidence,
provenance, or lineage. Numerically equal summaries over distinct Feature Set
identities remain distinct experiment inputs and results.

The summary invents no market timestamp. Individual Feature Value timestamps
and original offsets remain evidence owned by the Feature Set. Invocation,
wall-clock, logging, and process timestamps are operational only and cannot
replace or collapse semantic provenance.

## Determinism and equivalence

Equivalent accepted Feature Set evidence under the same built-in definition
produces equivalent semantic summary evidence. Results are independent of:

- current culture and UI culture;
- machine timezone;
- invocation time and duration;
- process, thread, or machine identity;
- random or correlation identifiers;
- filesystem and database paths;
- connection strings;
- logging configuration;
- provider credentials;
- persistence disposition; and
- retry, scheduling, or checkpoint state.

An equivalent recomputation is not a new semantic result because it ran in a
different process or at another time. A changed Feature Set identity or changed
experiment definition is semantically distinct even when the resulting
statistics are numerically identical.

Exact `aiq-experiment-identity-v1` canonical fields, byte encoding, domain
separators, fingerprint construction, and external identity form are reserved
exclusively for WP03. This document does not freeze them.

## Immutability

A successful Experiment Result is immutable once established. Its count,
optional aggregate values, definition reference, exact Feature Set reference,
provenance, and lineage cannot be mutated after construction. It exposes no
mutable collection or semantic state and does not modify the input Feature Set.

## Provenance and lineage

A successful result remains traceable to:

- the exact `simple-return-descriptive-summary-v1` definition;
- the exact accepted Feature Set and Feature Set Identity;
- the Feature Definition Identity for `simple-return-lag-1-v1`;
- the exact Dataset Snapshot Identity and Dataset Version; and
- accepted predecessor dataset, source-state, and historical-observation
  provenance reachable through the Feature Set.

Lineage is narrow and acyclic:

```text
source state
  -> dataset definition / research dataset
  -> dataset snapshot / version
  -> feature definition
  -> feature set
  -> experiment definition
  -> experiment result
```

Release 1.5 references accepted predecessor identities and evidence; it does
not recompute, reinterpret, rewrite, or fabricate them. Experiment evidence
never feeds back into dataset, pipeline, or feature identity.

## Success semantics

Success is either:

- a valid immutable empty summary; or
- a valid immutable non-empty count/mean/minimum/maximum summary.

Equivalent recomputation is successful without requiring persistence or a
`NewlyAccepted`/`EquivalentExisting` disposition. Success requires all
applicable summary evidence and coherent identity/provenance prerequisites.

## Failure semantic distinctions

Later contracts and validation must preserve these distinct concepts:

- invalid experiment request;
- unsupported experiment definition;
- feature-generation or input dependency unavailable;
- requested predecessor evidence `NotFound`, where applicable;
- invalid Feature Set identity, provenance, lineage, ordering, or cardinality
  evidence;
- invalid numeric evidence or unrepresentable decimal computation;
- integrity contradiction; and
- unknown programming or system defects, which propagate outside bounded
  failure normalization.

A successful empty result is neither `NotFound` nor failure. Exact enum names,
contract shapes, and deterministic validation precedence belong to WP04 and
WP06.

## Fail-stop and evidence-established-only rules

The first governed failure stops downstream semantic construction. No partial
summary is successful. Mean, minimum, maximum, Experiment Result identity, or
downstream provenance must not be fabricated when their prerequisites cannot
be established.

Failure evidence contains only facts established before failure. Unknown
defects propagate and must not be converted into misleading success or a broad
catch-all governed failure. A valid empty summary is complete evidence, not a
partial result.

## Ownership and architecture

Experiment semantics are Application-owned and provider- and
storage-independent. The accepted production dependency graph remains:

```text
Domain -> none
Application -> Domain
Infrastructure -> Application
Worker -> Application, Infrastructure
```

Domain remains unchanged. Infrastructure does not define experiment meaning.
Worker later owns explicit configuration, one-shot invocation, bounded
presentation, and process exit behavior; it cannot redefine semantic evidence.
No project or reference edge is added.

## Release 1.3 pipeline protection

The fixed Release 1.3 topology remains exactly:

1. historical observation retrieval;
2. deterministic dataset materialization;
3. immutable snapshot persistence;
4. catalog registration; and
5. structured pipeline result/evidence.

The Release 1.5 experiment is a separate bounded use case. It is not a sixth
stage, configurable node, scheduler target, retry boundary, or generalized
research DAG.

## Provider, storage, and observability boundaries

Experiment semantics contain no Twelve Data, HTTP, provider response,
credential, SQL, SQLite API, database key/path, connection string, filesystem
path, or persistence timing detail. There is no provider fallback or live
acquisition.

Operational logs or bounded Worker output may later reference semantic
evidence, but logging, metrics, tracing, dashboards, timestamps, and correlation
values do not determine experiment identity or equivalence. Release 1.5 adds no
telemetry backend or durable operational evidence.

## Persistence and schema decision

Experiment results are immutable in-memory evidence only. Release 1.5 adds no
experiment persistence, registry, catalog, history, cache, feature persistence,
run-history store, checkpoint state, or scheduler state. SQLite remains schema
version 2; schema v3 is not authorized.

## Explicit deferrals

Release 1.6 or later authority is required for experiment persistence,
registries, history, research workspaces, notebooks, visualization, APIs,
additional descriptive or inferential experiments, configurable statistics,
broader feature libraries, feature persistence/catalogs, strategies, signals,
backtesting, portfolio/risk, AI/ML, explainability, MLOps, live acquisition
orchestration, scheduling, retries, recovery, checkpoints, plugins,
expressions, generalized DAGs, distributed execution, durable execution
history, and telemetry backends.

This release creates no implementation placeholder for deferred capabilities.

## Handoff

WP03 may define exact Experiment Definition and Experiment Result identity,
canonical representation, provenance, lineage, and evidence encoding under
`aiq-experiment-identity-v1` without changing these semantics. WP04 and later
work packages may implement only their separately authorized model, contracts,
computation, validation, integration, composition, Worker, and test boundaries.
