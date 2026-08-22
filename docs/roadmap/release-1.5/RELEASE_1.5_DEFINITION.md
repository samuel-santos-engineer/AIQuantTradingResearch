# Release 1.5 Definition

## 1. Release title and placement

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Release 1.5 is the next governed increment after the formally closed Release
1.4 Deterministic Feature Engineering Foundation. It advances the data
lifecycle from reproducible feature evidence to one bounded, experiment-ready
quantitative result.

## 2. Executive summary

Release 1.5 establishes one provider-independent, storage-independent,
deterministic research experiment over the accepted Release 1.4
`simple-return-lag-1-v1` feature set. The built-in experiment,
`simple-return-descriptive-summary-v1`, produces an immutable in-memory summary
containing count, arithmetic mean, minimum, and maximum simple return, together
with canonical identity, provenance, lineage, and structured result evidence.

The release proves the first reproducible transition from feature evidence to
a quantitative research outcome. It does not build a notebook environment,
experiment registry, backtester, feature store, generalized statistics
library, model-training system, scheduler, or live-acquisition workflow.

## 3. Verified starting baseline

The definition was derived from this independently verified baseline:

- local `main` and `origin/main` are synchronized at
  `2fa88ff70e8a772b2d10bfab0f550f4cd66dd504` with ahead/behind `0/0`;
- Release 1.3 PR #152 and Release 1.4 PR #167 are merged;
- authoritative milestones #54 and #45 are closed;
- legacy milestone #44 is closed and empty;
- Release 1.4 issues #153–#166 are 14/14 Closed/Done;
- milestone #46 is open, empty, and unchanged;
- no Release 1.5 issues or implementation exist;
- SQLite remains schema version 2;
- the production dependency graph remains unchanged and acyclic;
- build warnings/errors are 0/0;
- permanent tests pass 214/214: Domain 11, Application 86,
  Infrastructure 104, Architecture 13;
- formatting, canonical verification, and Gitleaks pass.

The planning prompt pair is out-of-band execution authority and is not part of
the proposed Release 1.5 governed candidate.

## 4. Problem statement

The platform can now acquire and persist historical observations, materialize
immutable dataset snapshots, execute a fixed research pipeline, and compute a
canonically identified simple-return feature set. It cannot yet express or
reproduce a quantitative research conclusion over that feature evidence.

This is the immediate research-value bottleneck. Additional infrastructure
would improve storage or operations, and additional features would broaden
inputs, but neither proves that the platform can turn its existing trusted
evidence into a governed research result. Release 1.5 should cross that boundary
once, narrowly and deterministically.

## 5. Release 1.1–1.4 capability map

| Release | Implemented capability | Durable or semantic evidence |
| --- | --- | --- |
| 1.1 | Historical-observation persistence and exact retrieval | SQLite-backed immutable ordering, fidelity, conflict, atomicity, and failure semantics |
| 1.2 | Dataset materialization, immutable snapshots, catalog, versioning, identity, provenance, and lineage | `aiq-dataset-identity-v1` and SQLite schema v2 |
| 1.3 | Fixed deterministic five-stage one-shot research pipeline | `aiq-pipeline-identity-v1`, fail-stop stage evidence, no durable run history |
| 1.4 | Exact-snapshot deterministic feature generation | `simple-return-lag-1-v1`, `aiq-feature-identity-v1`, immutable in-memory feature evidence |

Together these releases can prove exactly which persisted observations became
which immutable snapshot and which deterministic feature values. The missing
step is an explicit quantitative interpretation whose inputs, semantics, and
result are equally reproducible.

## 6. Candidate comparison

| Candidate | Research value | Determinism and reuse | Cost and coupling | Decision |
| --- | --- | --- | --- | --- |
| Feature persistence/catalog | Reuses feature evidence efficiently but does not create a new research conclusion | Strong offline reproducibility | Requires schema evolution, retrieval/retention semantics, and integrity policy | Deferred |
| Additional deterministic features | Broadens analytical inputs | Strong and testable | Risks prematurely creating a registry, parameter system, or generic feature engine before one feature is consumed by research | Deferred |
| Controlled acquisition orchestration | Improves freshness and automation | Depends on network/provider variability | Introduces credentials, rate limits, retry policy, and provider coupling | Deferred |
| Durable pipeline/feature execution history | Adds operational auditability | Captures occurrences rather than new semantic research value | Requires operational identity, retention, and schema evolution | Deferred |
| Scheduling/resilience automation | Enables unattended execution | Clock, concurrency, recovery, and network behavior reduce deterministic isolation | High operational complexity relative to current research value | Deferred |
| Research workspace/notebooks | Improves exploration ergonomics | Can consume current assets | Broad UI/runtime/dependency surface and weak governance of reproducible outputs | Deferred |
| Experiment registry | Makes experiments discoverable and reusable | Valuable after stable experiment semantics exist | Persistence and lifecycle semantics are premature before one experiment artifact is proven | Deferred |
| **One deterministic descriptive experiment** | **Produces the first governed quantitative result from existing feature evidence** | **Fully offline; reuses exact snapshot and feature identities** | **Small Application/Worker/test surface; no schema or package change required** | **Selected** |

## 7. Selected capability and rationale

Release 1.5 selects a **Deterministic Research Experiment Foundation** with
exactly one built-in experiment:

`simple-return-descriptive-summary-v1`

It is the strongest next increment because it:

- directly advances quantitative-research value rather than operational
  infrastructure;
- consumes the complete Release 1.1–1.4 evidence chain;
- produces a meaningful, reviewable result without strategy or prediction;
- remains deterministic, offline, local, and inexpensive;
- requires no schema, package, project, or reference change;
- establishes experiment semantics before any registry or workspace is built;
- leaves generalized analytics and multiple-experiment frameworks unjustified.

## 8. Core capability

For one accepted immutable Feature Set generated by
`simple-return-lag-1-v1`, the built-in experiment produces:

- `Count`: the exact number of feature values;
- `ArithmeticMean`: the exact decimal arithmetic mean when count is positive;
- `Minimum`: the exact minimum feature value when count is positive;
- `Maximum`: the exact maximum feature value when count is positive.

An empty feature set is a successful experiment result with count zero and no
mean, minimum, or maximum. A non-empty result requires all three statistics.
The result preserves the exact input Feature Set identity and does not alter or
round feature values.

## 9. End-to-end boundary

```text
explicit experiment request
  → validate built-in experiment definition and exact snapshot request
  → invoke existing exact-snapshot feature-generation use case once
  → validate returned feature identity/provenance/evidence
  → compute deterministic descriptive summary
  → construct immutable experiment identity/provenance/lineage
  → return structured experiment result/evidence
```

The boundary begins with an explicit exact snapshot identity/version and ends
with an immutable in-memory experiment result. It does not persist the feature
set or experiment result.

## 10. In scope

- one built-in `simple-return-descriptive-summary-v1` definition;
- provider- and storage-independent experiment request/result contracts;
- immutable descriptive-summary value and structured semantic evidence;
- deterministic count, decimal mean, minimum, and maximum;
- explicit empty-result semantics;
- canonical Experiment Definition and Experiment Result identities;
- provenance and lineage through the exact Feature Set and Dataset Snapshot;
- validation and bounded expected-failure mapping;
- one Application-owned orchestration path over `IFeatureGenerationUseCase`;
- DI registration and explicit configuration/request construction;
- one bounded offline Worker experiment mode;
- deterministic permanent semantic and composition tests;
- architecture and current-state documentation alignment;
- final validation and separately governed integration.

## 11. Out of scope

- experiment persistence, registry, catalog, search, comparison, or history;
- feature persistence, feature catalog, feature cache, or schema evolution;
- notebooks, kernels, interactive workspace, UI, REST API, or visualization;
- additional statistics, indicators, features, formulas, or configurable
  aggregations;
- hypothesis testing, confidence intervals, distributions, regression, or
  optimization;
- labels, targets, leakage policy, strategies, signals, backtesting, portfolio,
  risk, or trading;
- model training, evaluation, prediction, explainability, AI/ML, or MLOps;
- live acquisition, provider calls, credentials, or provider fallback;
- scheduling, retries, circuit breakers, recovery, checkpoint, or resume;
- plugins, expression languages, registries, generalized analytics, or DAGs;
- durable pipeline, feature, or experiment execution history;
- Release 1.6 implementation or planning.

## 12. Release 1.1 preservation

Historical-observation persistence and retrieval remain authoritative for
target/time fidelity, decimal/timestamp fidelity, ordering, idempotency,
equivalence, conflicts, atomicity, isolation, and failure mapping. The
experiment does not read historical storage directly.

## 13. Release 1.2 preservation

Dataset identity, materialization, immutable snapshot/version semantics,
catalog lookup, source-state evidence, provenance, lineage, equivalence, and
schema v2 remain unchanged. The experiment request identifies the exact
snapshot/version through existing Feature Generation contracts.

## 14. Release 1.3 preservation

The fixed pipeline remains exactly five stages. Experiment execution is not a
sixth stage and does not change `aiq-pipeline-identity-v1`, fail-stop behavior,
one-shot execution, or structured pipeline evidence.

## 15. Release 1.4 preservation

`simple-return-lag-1-v1`, `aiq-feature-identity-v1`, exact snapshot lookup,
feature validation, feature identity/provenance/lineage, empty/single-input
success, and one-shot feature Worker behavior remain unchanged. Release 1.5
consumes a successful Feature Set without redefining its identity or values.

## 16. Ownership by layer

- **Domain:** expected zero delta. Experiment orchestration and evidence do not
  introduce a domain-wide market invariant.
- **Application:** owns experiment definition, value model, identities,
  provenance, lineage, contracts, deterministic computation, validation,
  failure mapping, and orchestration.
- **Infrastructure:** expected production delta zero. Existing snapshot storage
  remains behind Application contracts; there is no experiment persistence.
- **Worker:** binds one explicit experiment request, resolves the Application
  use case, executes once, projects bounded evidence, and exits.
- **Tests:** Application tests own semantics and determinism; Infrastructure
  tests own real composition and black-box Worker proof; Architecture tests add
  only stable non-redundant rules.

The production dependency graph remains unchanged and acyclic.

## 17. Identity semantics

Release 1.5 introduces one genuinely new immutable semantic namespace:

`aiq-experiment-identity-v1`

It contains two distinct identities:

- **Experiment Definition Identity:** identifies the fixed definition name,
  version, required feature definition, summary fields, decimal rules, and
  empty-result semantics.
- **Experiment Result Identity:** identifies the exact Experiment Definition,
  exact Feature Set identity, exact snapshot/version binding, and canonical
  summary evidence.

Fingerprints use SHA-256 rendered as exactly 64 lowercase hexadecimal
characters. Canonical content is UTF-8, unambiguous, length-delimited,
culture-invariant, and deterministically ordered. Operational invocation data,
paths, logs, machine information, timing, and presentation are excluded.

Dataset, pipeline, and feature identities are referenced, never overloaded or
recomputed as experiment identities.

## 18. Provenance and lineage semantics

Successful experiment provenance references:

- Experiment Definition Identity;
- Experiment Result Identity;
- exact Feature Definition and Feature Set identities;
- exact Dataset Snapshot identity and version;
- existing dataset/source-state provenance reachable through Feature Set
  evidence;
- exact summary cardinality and values.

Lineage is one-way and acyclic:

```text
Source State
  → Dataset Definition / Research Dataset
  → Dataset Snapshot / Version
  → Feature Definition / Feature Set
  → Experiment Definition / Experiment Result
```

The experiment cannot mutate or become an ancestor of predecessor evidence.

## 19. Persistence and schema decision

**SQLite remains schema version 2.**

The selected experiment is a pure deterministic projection of immutable,
canonically identified feature evidence. Its result can be reproduced exactly,
so durable state is not necessary to prove the capability.

Release 1.5 adds no experiment table, registry, catalog, run history, feature
table, cache, checkpoint, or migration. A future persistence release must first
define lookup, retention, coexistence, integrity, and migration semantics.

## 20. Failure taxonomy and behavior

Expected failures remain provider-independent and distinguish:

- invalid experiment request;
- unsupported experiment definition;
- unsupported or contradictory feature definition evidence;
- exact snapshot `NotFound` inherited from feature generation;
- unavailable snapshot dependency;
- invalid snapshot, feature, provenance, or lineage evidence;
- invalid numeric evidence or decimal overflow;
- integrity contradiction.

Execution is fail-stop and returns only evidence established before failure.
There is no partial summary. Unknown programming defects propagate and are not
normalized into an expected business failure. No retry, fallback, repair,
overwrite, or compensation is introduced.

## 21. Determinism and equivalence

- Inputs are the exact experiment definition and exact successful Feature Set
  evidence produced for one snapshot/version.
- Count follows immutable feature order and cardinality.
- Mean uses exact .NET `decimal` addition and division with no binary
  floating-point conversion or convenience rounding.
- Minimum and maximum compare exact decimals.
- Empty input succeeds with count zero and absent aggregate values.
- Culture, timezone, wall clock, randomness, process, machine, and filesystem
  state cannot affect semantic results.
- Equivalent feature evidence produces equivalent summary values and
  Experiment Result Identity.
- Different Feature Set identities remain experiment-result distinct even when
  their numeric summaries coincide.

## 22. Configuration and DI implications

Application registration adds exactly one effective experiment use case,
validator, and deterministic computer using established lifetimes. The use case
reuses `IFeatureGenerationUseCase`; no new Infrastructure service is required.

Worker configuration supplies only the exact snapshot identity/version and an
explicit experiment mode. Definition, formula, summary fields, numeric rules,
and empty semantics are built-in and are not configurable. DI resolution must
remain side-effect free.

## 23. Worker and hosting implications

Worker executes at most one experiment per process and terminates. It presents
safe bounded evidence: definition/result identities, input feature/snapshot
identities, count, and summary values or one bounded failure. It does not print
credentials, database paths, full input observations, or machine-specific
state.

Absent experiment configuration preserves existing Release 1.3 pipeline and
Release 1.4 feature modes. No daemon, loop, schedule, retry, or hosted
background service is introduced.

## 24. Security, offline, and provider decision

Experiment execution is strictly offline after predecessor data exists. It
must not call Twelve Data or any provider, require a real credential, use HTTP,
or fall back to acquisition. Permanent tests use synthetic immutable evidence
and isolated temporary SQLite only where black-box Worker composition requires
existing snapshot storage.

No secret, raw connection string, provider payload, or sensitive local path is
part of experiment identity, evidence, logging, or committed configuration.

## 25. Observability and evidence decision

Application returns deterministic semantic evidence sufficient to explain and
reproduce the experiment. Worker may emit bounded start, success, and failure
presentation using safe semantic identifiers.

Release 1.5 does not add metrics, distributed tracing, dashboards, persisted
operational history, correlation storage, or an observability backend.

## 26. Architecture impact

- production graph unchanged: Domain → none, Application → Domain,
  Infrastructure → Application, Worker → Application and Infrastructure;
- cycles remain zero;
- provider/HTTP confinement remains in Infrastructure;
- semantic ownership remains in Application;
- expected Infrastructure and Domain production deltas are zero;
- expected package/project/reference deltas are zero;
- schema remains v2;
- no generic experiment, statistics, plugin, or DAG framework is introduced.

## 27. Testing strategy

Permanent acceptance remains deterministic and offline. Tests should prove:

- exact count, decimal mean, minimum, and maximum;
- empty and non-empty success;
- canonical definition/result identities and 64-character lowercase SHA-256;
- equivalent recomputation stability;
- identity distinction for different Feature Set identities with equal summary;
- provenance and acyclic lineage consistency;
- invalid request, unsupported definition, NotFound, dependency unavailable,
  invalid evidence, invalid numeric input, overflow, and integrity conflict;
- first-failure and no-partial-result behavior;
- unknown-exception propagation;
- DI resolution without execution or database/provider side effects;
- one bounded offline Worker process and deterministic exit behavior;
- Release 1.1–1.4 regression, schema-v2 preservation, architecture, formatting,
  Gitleaks, whitespace, residue, canonical verification, and fresh-checkout
  reproducibility.

No test requires network access, a real credential, nondeterministic time,
random input, or persistent repository state.

## 28. Documentation impact

Later execution may minimally align current-state architecture documents for
the experiment boundary, identity/provenance, Application ownership,
configuration, Worker behavior, evidence, testing, and explicit deferrals.
Historical and clearly future documents remain preserved. This definition does
not authorize broad documentation rewriting.

## 29. Release 1.6+ deferrals

Explicitly deferred:

- experiment persistence, registry, comparison, history, and workspace;
- feature persistence/catalog/cache and schema evolution;
- additional features, statistics, indicators, labels, and configurable
  formulas;
- notebooks, visualization, interactive kernels, and APIs;
- strategies, signals, backtesting, portfolio, and risk;
- model training, evaluation, prediction, explainability, AI/ML, and MLOps;
- live acquisition orchestration and multi-provider policy;
- scheduling, retries, circuit breakers, recovery, checkpoints, and resume;
- plugins, expression languages, dynamic DAGs, parallel/distributed/streaming
  execution;
- durable pipeline, feature, experiment, or operational run history;
- metrics/tracing backends and dashboards.

## 30. Proposed work-package decomposition

| WP | Title | Purpose | Layer(s) | Depends on | Major exclusions | Model |
| --- | --- | --- | --- | --- | --- | --- |
| WP01 | Release & Repository Preflight | Reconcile closure, governance, GitHub, repository, schema-v2, architecture, and 214-test baseline | Evidence only | Release 1.4 closed | No implementation or planning mutation | Luna |
| WP02 | Experiment Semantic Discovery | Freeze descriptive-summary vocabulary, aggregates, empty behavior, and exclusions | Architecture documentation | WP01 | No identities, contracts, or code | Sol |
| WP03 | Experiment Identity, Provenance & Evidence | Freeze `aiq-experiment-identity-v1`, canonical representation, equivalence, provenance, and lineage | Architecture documentation | WP02 | No implementation or persistence | Sol |
| WP04 | Experiment Model & Contracts | Define immutable Application values, request/result/failure/evidence contracts | Application | WP03 | No computation or orchestration | Terra |
| WP05 | Deterministic Summary Computation | Implement exact count/mean/minimum/maximum behavior | Application | WP04 | No feature lookup, DI, or Worker | Terra |
| WP06 | Experiment Validation & Failure Semantics | Enforce evidence-established-only and first-failure rules | Application | WP03, WP05 | No retries or generalized validation framework | Sol |
| WP07 | Feature-to-Experiment Integration | Compose existing feature generation and summary computation once | Application | WP04, WP05, WP06 | No provider, persistence, or pipeline change | Terra |
| WP08 | Dependency Registration & Configuration | Register the graph and construct explicit semantic input side-effect free | Application/Worker composition | WP07 | No execution or database mutation during resolution | Terra |
| WP09 | One-Shot Worker Experiment Execution | Execute one experiment, present bounded evidence, and terminate | Worker | WP08 | No loops, scheduling, retries, or API | Terra |
| WP10 | Application Experiment Tests | Prove semantics, identity, determinism, failures, and orchestration with hand-written doubles | Application.Tests | WP03–WP07 | No SQLite, provider, or process tests | Luna |
| WP11 | Composition & Worker Validation | Prove real DI/configuration and black-box offline Worker behavior | Infrastructure.Tests | WP08, WP09 | No production changes or network | Terra |
| WP12 | Architecture & Documentation Alignment | Preserve stable boundaries and align only stale current-state documentation | Architecture.Tests/docs | WP10, WP11 | No redundant rules or feature redesign | Terra |
| WP13 | Full Validation, Integration & Acceptance | Reconcile candidate, run full/fresh-checkout validation, and separately govern integration | Validation/integration | WP10, WP11, WP12 | No merge, tag, release, or Release 1.6 work | Sol |

Dependency summary:

```text
Release 1.4 CLOSED → WP01 → WP02 → WP03 → WP04 → WP05
WP03 + WP05 → WP06
WP04 + WP05 + WP06 → WP07 → WP08 → WP09
WP03 + WP04 + WP05 + WP06 + WP07 → WP10
WP08 + WP09 → WP11
WP10 + WP11 → WP12
WP10 + WP11 + WP12 → WP13
```

## 31. Model recommendations

- **GPT-5.6 Sol:** WP02, WP03, WP06, and WP13 because semantic boundaries,
  canonical identity, failure behavior, and final reconciliation are
  high-consequence reasoning tasks.
- **GPT-5.6 Terra:** WP04, WP05, WP07–WP09, WP11, and WP12 because they are
  bounded contract, implementation, composition, Worker, integration-test, or
  documentation tasks.
- **GPT-5.6 Luna:** WP01 and WP10 because preflight and deterministic test
  expansion are structured once semantics are frozen.

These recommendations are workload guidance, not pricing or availability
claims.

## 32. GitHub-planning recommendation

A later, separate GitHub-planning authority should reconcile empty milestone
#46 with the accepted Release 1.5 title and boundary. It should decide whether
the milestone may be renamed/re-described or whether a new authoritative
milestone is required while preserving #46 as legacy.

Recommended planning creates exactly WP01–WP13, no WP14+, closure issue, or
lifecycle-gate issue. Use repository conventions: one owner, existing labels,
Project #2 Status `Backlog`, Priority `P1`, a unique Release `1.5` option only
when separately authorized, and the closest existing Area justified by each
WP. WP01 remains Backlog until governance integration and human acceptance.

No GitHub object is mutated by this definition.

## 33. Acceptance criteria

The definition is ready for human acceptance only if:

1. Release 1.5 is limited to one built-in descriptive-summary experiment;
2. the experiment consumes existing exact Feature Set evidence;
3. count/mean/minimum/maximum and empty-result semantics are unambiguous;
4. `aiq-experiment-identity-v1` represents genuinely new immutable semantics;
5. dataset, pipeline, and feature identities remain unchanged;
6. provenance and lineage are deterministic and acyclic;
7. SQLite remains schema v2 and no result persistence is implied;
8. Application owns semantics, computation, validation, and orchestration;
9. Worker remains one-shot and offline;
10. the production dependency graph remains unchanged;
11. package/project/reference deltas remain zero by default;
12. experiments, features, acquisition, scheduling, backtesting, ML, and
    workspace capabilities beyond this boundary remain deferred;
13. WP01–WP13 form a complete dependency graph without implementation detail;
14. the existing 214-test baseline remains green after artifact creation;
15. no GitHub or Git-history mutation occurred.

## 34. Definition completion marker

This artifact defines Release 1.5 only. It does not authorize execution-plan,
file-manifest, prompt, GitHub-planning, implementation, integration, or closure
work.

**Definition decision: COMPLETE — pending human acceptance.**
