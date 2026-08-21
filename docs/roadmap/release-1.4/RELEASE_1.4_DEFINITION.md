# Release 1.4 Definition

## 1. Executive Summary

**Phase 4 — Release 1.4: Deterministic Feature Engineering Foundation**

Release 1.4 establishes one provider-independent, storage-independent, deterministic feature transformation over an accepted immutable research-dataset snapshot. The bounded capability computes a versioned lag-1 simple-return feature set and returns canonical identity, provenance, lineage, and structured result evidence.

This release does not modify the fixed Release 1.3 research pipeline. Feature generation is a separate explicit Application use case that consumes an already accepted Release 1.2 snapshot. It introduces no live acquisition, feature-plugin framework, persistence, scheduling, retry orchestration, model training, or Release 1.5 behavior.

## 2. Verified Baseline

The definition was derived from the following verified state:

- Release 1.3 PR #152 is merged.
- Merged `main` is `0c981bb5765bb519bca3542c745f9282beb7b0d5` and matches `origin/main`.
- Release 1.3 milestone #54 is closed with 14 closed issues and zero open issues.
- Issues #138–#151 are 14/14 Closed/Done.
- Permanent tests pass 197/197: Domain 11, Application 77, Infrastructure 96, Architecture 13.
- Build warnings and errors are 0/0; canonical verification and Gitleaks pass.
- SQLite schema remains version 2.
- The production graph remains Domain → none, Application → Domain, Infrastructure → Application, Worker → Application and Infrastructure, with zero cycles.

## 3. Repository Truth Examined

The decision reconciles:

- Release 1.1 historical-observation persistence and retrieval;
- Release 1.2 immutable dataset definition, identity, snapshot, and catalog semantics;
- Release 1.3 fixed pipeline, semantic identity, fail-stop behavior, and structured evidence;
- the product vision for reproducible quantitative research;
- the data lifecycle transition from curated/feature-ready data to experiment-ready data;
- the data glossary's Feature, Feature Set, and Feature Pipeline vocabulary;
- the current Application dataset and pipeline seams;
- SQLite schema-v2 implementation and tests;
- architecture guidance for public contracts, module ownership, observability, resilience, and testing;
- current GitHub milestones, including open and empty legacy milestone #45, `Phase 4 - Release 1.4: Feature Engineering`.

## 4. Candidate Capability Comparison

| Candidate | Satisfied prerequisites | Missing prerequisites | Product value | Risk/coupling | Offline determinism | Schema impact | Decision |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Deterministic feature engineering | Immutable ordered datasets, exact decimal/timestamp fidelity, dataset identities, catalog lookup, one-shot host, Application orchestration | Feature vocabulary, canonical transform, feature identities, result evidence | Directly converts trusted datasets into analytical inputs | Bounded if limited to one built-in transform; broad plugin pipelines must remain deferred | Strong: pure calculation over immutable input | None when output is reproducible in memory | **Selected** |
| Controlled acquisition orchestration | Twelve Data adapter, normalized observations, durable observation storage | Explicit acquisition policy, credential/network boundaries, freshness semantics, provider failure policy | Automates source refresh | Couples provider availability and credentials to orchestration; weakens offline acceptance | Limited because transport is external | None necessarily | Deferred until acquisition policy is separately justified |
| Durable pipeline execution history | Stable pipeline identities and structured evidence | Retention/query semantics, immutable run-record contract, storage model, privacy/operational boundaries | Improves audit and operations | Requires schema evolution for primarily operational value | Strong after capture | Likely schema v3 | Deferred until an audit/retrieval requirement exists |
| Scheduling and resilience execution | One-shot Worker, failure categories, architectural retry guidance | Schedule ownership, overlap policy, retry eligibility/budget, cancellation, recovery, operational state | Enables unattended operation | High coupling among time, concurrency, failure recovery, and provider behavior | Lower; clock and external dependencies dominate | Potential operational-state schema | Deferred; not prerequisite for feature semantics |
| Metrics/tracing observability | Structured semantic and local operational evidence | Stable telemetry signals, backend/export policy, retention and cost decisions | Improves operations | Adds infrastructure before a demonstrated operational need | Testable but not research-semantic value | None or external | Deferred; current evidence is sufficient |

## 5. Release Boundary Decision

The smallest coherent next increment is a deterministic feature-engineering foundation, not a general feature pipeline.

The repository already provides immutable, canonically ordered datasets and reproducible identity evidence. A single built-in transformation proves the next data-lifecycle transition without introducing storage redesign, arbitrary stages, provider access, or model behavior. It also follows the product roadmap and legacy milestone direction while narrowing that broad historical description to an executable foundation.

## 6. Core Capability

Release 1.4 defines and executes exactly one built-in feature:

`simple-return-lag-1-v1`

For canonically ordered input prices `p[i-1]` and `p[i]`, the feature value at the current observation timestamp is:

```text
(p[i] / p[i-1]) - 1
```

Required semantics:

- use exact .NET `decimal` arithmetic;
- preserve the current observation's exact `DateTimeOffset` value and offset;
- consume input observations in their accepted canonical order;
- emit one feature observation for each adjacent pair;
- an empty or single-observation snapshot produces a valid empty feature set;
- reject non-positive or otherwise invalid price evidence rather than inventing a value;
- never use local culture, local timezone, wall-clock time, randomness, machine state, or filesystem paths;
- repeated evaluation of equivalent snapshot evidence produces equivalent feature identity and values.

## 7. End-to-End Boundary

```text
explicit feature-generation request
  → exact immutable Dataset Snapshot Identity lookup
  → validate accepted snapshot evidence
  → apply built-in simple-return-lag-1-v1 in canonical order
  → construct immutable feature-set identity/provenance/lineage
  → return structured feature-generation result/evidence
```

The boundary starts with an existing snapshot identity and ends with an immutable in-memory feature set and result evidence. It neither invokes Release 1.3 pipeline execution nor persists feature output.

## 8. In Scope

- provider- and storage-independent feature definition and request contracts;
- one built-in lag-1 simple-return definition with an explicit version marker;
- immutable feature observations and feature-set result values;
- exact timestamp/offset and decimal fidelity;
- deterministic feature-definition and feature-set identities;
- provenance linking the feature set to the accepted input snapshot and feature definition;
- narrow lineage from input snapshot to output feature set;
- exact snapshot lookup through existing Application contracts;
- validation and classified expected failures;
- structured semantic result evidence;
- DI registration and one bounded offline Worker execution path;
- deterministic permanent tests and stable architecture enforcement;
- minimal current-state documentation alignment;
- final integration and acceptance under separate authority.

## 9. Out of Scope

- adding a feature stage to the fixed Release 1.3 pipeline;
- arbitrary formulas, user-authored expressions, plugins, or dynamic feature graphs;
- rolling indicators, windows, joins, resampling, aggregation, labels, or target leakage policy;
- feature selection, scaling, normalization, imputation, or enrichment libraries;
- feature-set persistence, catalog storage, cache, or schema evolution;
- live acquisition, provider calls, or credentials;
- scheduling, refresh loops, retries, circuit breakers, fallback, compensation, checkpoint, or resume;
- parallel, streaming, distributed, or GPU execution;
- notebooks, research workspace, visualization, or experiment tracking;
- strategy, signal, backtest, portfolio, model training, evaluation, inference, explainability, or MLOps;
- Release 1.5 implementation or planning.

## 10. Release 1.1–1.3 Preservation

Release 1.1 remains authoritative for immutable historical-observation persistence, exact-target retrieval, ordering, fidelity, idempotency, conflicts, atomicity, and failure mapping.

Release 1.2 remains authoritative for dataset definitions, `[from,to)` selection, source-state identity, dataset-definition identity, snapshot identity, version, provenance, lineage, immutable snapshot persistence, and catalog lookup.

Release 1.3 remains authoritative for the fixed five-stage one-shot pipeline, `aiq-pipeline-identity-v1`, fail-stop execution, semantic evidence, and Worker termination. Release 1.4 does not alter that topology or reinterpret its identities.

## 11. Identity, Provenance & Reproducibility

Release 1.4 requires a distinct scheme:

`aiq-feature-identity-v1`

Two identities are required:

- **Feature Definition Identity:** canonical identity of the built-in algorithm marker, algorithm version, lag, timestamp-association rule, input ordering rule, and numeric semantics.
- **Feature Set Identity:** canonical identity derived from Feature Definition Identity, accepted Dataset Snapshot Identity, and ordered feature observations.

Fingerprints use SHA-256 encoded as exactly 64 lowercase hexadecimal characters. Canonical encoding must be length-delimited or otherwise unambiguous and culture invariant.

The feature set's provenance references existing dataset identities; it does not duplicate or replace them. Operational invocation IDs, timestamps, duration, machine/process data, paths, logs, and success dispositions are excluded from semantic identity.

`NewlyComputed` and any future cache-related disposition are operational/result classifications only. Release 1.4 does not authorize a cache or persistence layer.

## 12. Persistence & Schema Decision

**SQLite remains schema version 2.**

The selected feature is a pure deterministic projection of an immutable snapshot and a versioned definition. Its result can be reproduced exactly from accepted evidence, so durable feature persistence is not required to prove the foundation.

No feature table, feature catalog, feature cache, run-history table, or migration is authorized. A future release must define retrieval, retention, coexistence, and immutability requirements before proposing schema v3.

## 13. Failure & Resilience Boundary

Expected provider-independent outcomes must distinguish:

- invalid feature request or unsupported definition;
- snapshot `NotFound`;
- unavailable snapshot dependency;
- invalid or contradictory persisted snapshot evidence;
- invalid numeric input evidence;
- successful empty feature set;
- successful non-empty feature set.

Failure stops computation and returns only established upstream identity/evidence. Unknown programming defects remain visible and are not normalized into business failures.

No automatic retry, timeout policy, circuit breaker, fallback, repair, overwrite, compensation, checkpoint, or resume behavior is introduced.

## 14. Observability Boundary

Application returns deterministic structured semantic evidence containing definition identity, input snapshot identity, feature-set identity when established, observation count, and terminal outcome.

Worker may emit bounded structured local events for start, completion, and failure using safe semantic identifiers. Logs must exclude database paths, credentials, raw configuration secrets, machine-specific values, and full feature payloads.

No persisted operational history, metrics backend, tracing backend, dashboard, or telemetry package is required.

## 15. Architecture Impact

- **Domain:** expected zero delta unless semantic discovery proves a small technology-independent numeric invariant belongs in Domain. No provider, storage, host, or orchestration concept may enter Domain.
- **Application:** owns feature definitions, values, identities, provenance, contracts, deterministic computation, validation, failures, and use-case orchestration.
- **Infrastructure:** expected production delta zero; existing dataset catalog/snapshot implementations satisfy input lookup. DI changes are allowed only if composition requires them.
- **Worker:** binds one explicit snapshot/feature request, resolves the Application use case, executes once, projects safe evidence, and exits.
- **Tests:** Application tests own feature semantics and determinism; Infrastructure tests own real composition and offline Worker proof; Architecture.Tests add only stable non-redundant ownership rules.
- **Packages/projects/references:** expected delta 0/0/0. No new project is required.
- **Production graph:** unchanged.
- **Schema:** unchanged at version 2.

## 16. Testing Strategy

Permanent acceptance must remain offline and deterministic:

- exact lag-1 formula and decimal fidelity;
- timestamp/offset preservation;
- canonical ordering;
- empty and single-value inputs;
- equivalent input/definition identity stability;
- changed input or definition identity distinction;
- culture/timezone independence;
- invalid request, NotFound, dependency unavailable, and invalid evidence;
- provenance and lineage consistency;
- unknown-exception propagation;
- DI resolution without execution or database creation;
- one bounded Worker execution with no provider call;
- Release 1.1–1.3 regression and schema-v2 preservation;
- architecture ownership, dependency graph, security, whitespace, canonical verification, and fresh-checkout reproducibility.

No test may require a live provider, real credential, paid service, network access, nondeterministic clock, or persistent repository database.

## 17. Proposed Work-Package Graph

| WP | Title | Depends on | Objective | Expected area | Model |
| --- | --- | --- | --- | --- | --- |
| WP01 | Release & Repository Preflight | Release 1.3 CLOSED | Reconcile governance, GitHub, repository, schema-v2, architecture, and 197-test baseline | Evidence only | Luna |
| WP02 | Feature Engineering Semantic Discovery | WP01 | Freeze feature vocabulary, lag-1 formula, ordering, empty-result, and exclusion semantics | One semantic architecture artifact | Sol |
| WP03 | Feature Identity, Provenance & Evidence Semantics | WP02 | Freeze `aiq-feature-identity-v1`, canonical encoding, provenance, lineage, and equivalence | One semantic identity artifact | Sol |
| WP04 | Feature Domain/Application Model | WP03 | Establish the minimum provider/storage-independent value and invariant surface, preferring Domain delta zero | Domain only if proven; Application contracts | Terra |
| WP05 | Feature Generation Contracts | WP04 | Define request, result, failure, evidence, and use-case seams | Application feature area | Terra |
| WP06 | Deterministic Simple-Return Computation | WP05 | Implement the exact built-in lag-1 transformation | Application | Terra |
| WP07 | Feature Validation & Failure Mapping | WP03, WP06 | Enforce request/evidence invariants and classified fail-stop outcomes | Application | Sol |
| WP08 | Feature Generation Integration | WP05, WP06, WP07 | Compose exact snapshot lookup and deterministic computation without persistence | Application | Terra |
| WP09 | Dependency Registration & Configuration | WP08 | Register the bounded graph and explicit request factory with side-effect-free resolution | Existing DI/configuration surfaces | Terra |
| WP10 | One-Shot Worker Feature Execution | WP09 | Execute one explicit feature request, emit safe evidence, and exit | Worker | Terra |
| WP11 | Domain & Application Feature Tests | WP03, WP04, WP06, WP07, WP08 | Prove identities, formula, determinism, evidence, validation, and failures with hand-written doubles | Existing Domain/Application test projects | Luna |
| WP12 | Composition & Worker Validation | WP09, WP10 | Prove real DI, configuration, one-shot offline behavior, and zero provider calls | Existing Infrastructure tests | Terra |
| WP13 | Architecture & Documentation Alignment | WP11, WP12 | Preserve boundaries and align only current-state documentation | Architecture.Tests/docs | Terra |
| WP14 | Full Validation, Integration & Acceptance | WP11, WP12, WP13 | Reconcile, validate, fresh-checkout prove, and separately govern integration | Validation/integration | Sol |

Dependency summary:

```text
Release 1.3 CLOSED → WP01 → WP02 → WP03 → WP04 → WP05 → WP06
WP03 + WP06 → WP07
WP05 + WP06 + WP07 → WP08 → WP09 → WP10
WP03 + WP04 + WP06 + WP07 + WP08 → WP11
WP09 + WP10 → WP12
WP11 + WP12 → WP13
WP11 + WP12 + WP13 → WP14
```

## 18. Model Recommendations

- **Sol:** WP02, WP03, WP07, and WP14 because semantic identity, failure boundaries, and final reconciliation are high-consequence.
- **Terra:** WP04–WP06, WP08–WP10, WP12, and WP13 because they are bounded contract, implementation, composition, host, architecture, or documentation tasks.
- **Luna:** WP01 and WP11 because preflight and deterministic test expansion are structured and repetitive once semantics are frozen.

These tiers are execution guidance, not claims about current pricing or availability.

## 19. Release 1.5+ Deferrals

Explicitly deferred:

- feature persistence, catalog, cache, and schema v3;
- multiple built-in indicators or configurable formulas;
- rolling-window framework, resampling, cross-target joins, labels, and leakage controls;
- dynamic feature pipelines, plugins, DAGs, and parallel/distributed computation;
- live acquisition orchestration;
- scheduling and recurring refresh;
- automatic retries, circuit breakers, fallback, compensation, checkpoint, and resume;
- durable pipeline or feature-execution history;
- metrics/tracing backends and dashboards;
- research workspace, notebooks, visualization, and experiment registry;
- strategies, signals, backtesting, portfolio analytics, ML, explainability, and MLOps.

## 20. GitHub Planning Recommendation

- Existing milestone #45 is open, empty, and directionally consistent, but its broad historical title/description must remain untouched until separate GitHub-planning authority decides whether to preserve it as legacy or reconcile it.
- Recommended authoritative milestone title: `Phase 4 - Release 1.4: Deterministic Feature Engineering Foundation`.
- Recommended issue set: exactly WP01–WP14 from this definition; no WP15+, closure issue, or lifecycle-gate issue.
- Project #2 should receive a single `Release = 1.4` option only if absent and explicitly authorized.
- Initial fields should follow established conventions: Status `Backlog`, Priority `P1`, Release `1.4`, and the closest existing Area justified per WP.
- Reuse existing labels and owner conventions; do not create schema, labels, priorities, or areas without authority.
- WP01 must remain Backlog until governance artifacts are integrated and human accepted.

No GitHub object was created or modified during definition.

## 21. Definition Acceptance Criteria

Execution-plan and file-manifest design may begin only after human acceptance that:

1. the release is limited to one built-in lag-1 simple-return feature;
2. feature generation remains separate from the Release 1.3 fixed pipeline;
3. input is an accepted immutable snapshot and output is an immutable in-memory feature set;
4. `aiq-feature-identity-v1` and its two identity roles are appropriate;
5. exact decimal, timestamp/offset, ordering, empty-result, provenance, and lineage semantics are sufficient;
6. SQLite remains schema version 2 and feature persistence is deferred;
7. retries, scheduling, acquisition, plugins, durable history, workspace, and ML remain excluded;
8. the production dependency graph and package/project surface remain unchanged;
9. the proposed 14-WP dependency graph is coherent;
10. separate future authorities will govern execution-plan, file-manifest, GitHub planning, prompts, implementation, integration, and closure.

## 22. Definition Status

This document is a proposed Release 1.4 definition produced from the formally closed Release 1.3 repository baseline. It authorizes no implementation or GitHub mutation. Human acceptance is required before execution-plan and file-manifest design.
