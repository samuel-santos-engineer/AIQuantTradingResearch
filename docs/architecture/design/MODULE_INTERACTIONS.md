
# Module Interactions

**Status:** Draft
**Version:** 1.0
**Last Updated:** 2026-08-09
**Owners:** AIQuantTradingResearch Team

---

# Purpose

The Module Interactions document defines how architectural modules collaborate within AIQuantTradingResearch.

Its purpose is to establish consistent communication patterns, protect module boundaries, and ensure that collaboration occurs through explicit, stable contracts rather than implementation details.

While the Dependency Rules define **who may depend on whom**, this document defines **how those dependencies are exercised**.

---

# Vision

Modules should collaborate as independent capabilities.

Every interaction should represent a business intention, be governed by a public contract, and preserve the autonomy of both the consumer and the provider.

Interactions should strengthen modularity rather than increase coupling.

---

# Interaction Philosophy

Interactions represent capability collaboration.

A module should request **what it needs**, never dictate **how another module fulfills the request**.

Communication should remain intentional, explicit, and technology independent.

---

# Interaction Model

All module interactions follow the same conceptual model.

```text
Consumer Module
        │
        ▼
Public Contract
        │
        ▼
Provider Module
```

Consumers depend on contracts rather than implementations.

Provider modules remain free to evolve internally without affecting consumers.

The implemented Release 1.0 research interaction follows this model:

```text
Worker
  -> IResearchUseCase (Application)
  -> ResearchUseCase (internal Application implementation)
  -> IObservationSource (Application-owned port)
  -> TwelveDataObservationSource (internal Infrastructure adapter)
  -> TwelveDataClient / /time_series (Infrastructure transport)
  -> TwelveDataTimeSeriesNormalizer (Infrastructure mapping)
  -> PriceObservation / ObservationSeries / MeanPrice (Domain)
  -> ResearchOutcome / ResearchResult (Application)
  -> Worker presentation
```

Worker supplies configuration and initiates this interaction; its Worker-owned coordinator invokes the Application acquisition port and then the persistence use case, while Worker owns no provider mechanics, SQL, or persistence semantics. Infrastructure calls `/time_series` with `interval=1day` and `adjust=splits`, carrying the configured key only in the authentication header. It validates transport/provider evidence, normalizes the daily close at exchange-local midnight with the resolved offset, orders observations by absolute instant, rejects malformed/non-positive closes and duplicate instants, and maps failures to the provider-independent Application vocabulary before returning across the port.

Release 1.1 adds a separate persistence interaction after successful acquisition:

```text
Worker -> IObservationSource -> normalized PriceObservation values

## Release 1.2 Dataset Materialization

Release 1.2 reuses accepted Release 1.1 historical observations rather than
provider transport as dataset source truth. The Worker provides one explicit
`DatasetDefinition` (`Dataset:Target`, `Dataset:From`, `Dataset:To`) to the
Application integration seam. Application selects exact-target observations in
the `[from,to)` interval, orders by semantic instant, constructs deterministic
identity/version/provenance/lineage evidence, and asks Infrastructure to persist
an immutable SQLite schema-v2 snapshot and register the same evidence for exact
Snapshot Identity lookup. `NewlyAccepted`, equivalent existing evidence,
integrity conflict, unavailable storage, and invalid persisted evidence retain
their existing result meanings. This is a one-shot bounded action: no polling,
scheduling, refresh, retries, or pipeline orchestration is implemented.
      -> IPersistHistoricalObservationsUseCase
      -> IHistoricalObservationStore
      -> SqliteHistoricalObservationStore -> historical_observations
```

New, idempotent, and conflicting duplicates remain distinct; conflicts are not storage failures. Retrieval is exact-target, ascending, fidelity-preserving, and successfully empty when no history exists.

## Release 1.3 Fixed Research Pipeline

Release 1.3 composes the Release 1.1 and Release 1.2 interactions. Worker
supplies explicit dataset configuration and invokes one Application-owned fixed
pipeline over persisted historical observations: retrieval, deterministic
materialization, immutable snapshot persistence, catalog registration, and
structured result/evidence. Pipeline definition and semantic execution
identities use `aiq-pipeline-identity-v1` and remain distinct from dataset
identities. Equivalent reruns preserve execution identity; failure stops at the
first failed stage and does not fabricate downstream evidence. This is not live
acquisition, a scheduler, retry loop, configurable DAG, or durable run history.

## Release 1.4 Feature Generation

Release 1.4 adds a separate Application-owned request/response interaction:
an exact snapshot identity/version request is validated, looked up through the
existing snapshot store, validated as accepted evidence, and computed as the
single built-in `simple-return-lag-1-v1` feature. The immutable result retains
`aiq-feature-identity-v1` definition/set identities, snapshot-bound provenance,
lineage, ordered decimal values, and the current observation timestamp/offset.
Worker selects this bounded mode from `Feature:SnapshotIdentity` and
`Feature:SnapshotVersion`, invokes it once, presents safe evidence, and exits.

Infrastructure contributes only existing SQLite snapshot lookup. Feature
generation does not call Twelve Data or HTTP, does not persist features, and is
not a sixth pipeline stage.

## Release 1.5 Experiment Generation

Release 1.5 adds a separate Application-owned request/response interaction over
one exact successful Feature Set. An exact snapshot identity/version request is
forwarded through existing feature generation, validated as accepted Feature Set
evidence, summarized deterministically, and returned as immutable Experiment
Result evidence. `simple-return-descriptive-summary-v1` is the sole built-in
definition. `aiq-experiment-identity-v1` binds distinct definition/result
identities to the exact Feature Set, provenance, and acyclic predecessor lineage.

Worker selects this one-shot mode when either `Experiment:SnapshotIdentity` or
`Experiment:SnapshotVersion` is explicit; Experiment takes precedence over
Feature mode, which takes precedence over the existing pipeline. Partial
Experiment intent fails without fallback. Infrastructure contributes only the
existing snapshot path: there is no provider acquisition, experiment
persistence, experiment registry/history, or schema change.

---

# Interaction Types

The platform supports several interaction patterns.

## Request / Response

Used when a module requires an immediate result.

Examples include:

* Retrieve market data
* Resolve configuration
* Query feature metadata

---

## Commands

Used to request that another module perform an action.

Examples include:

* Import dataset
* Execute backtest
* Train model

Commands communicate intent rather than implementation.

---

## Queries

Used to retrieve information without modifying system state.

Examples include:

* Retrieve experiment history
* Load portfolio metrics
* Obtain data catalog entries

Queries should remain side-effect free.

---

## Events

Used to communicate that something has occurred.

Examples include:

* Dataset Imported
* Model Trained
* Experiment Completed
* Backtest Finished

Events improve decoupling by allowing multiple consumers to react independently.

---

## Pipeline Collaboration

Used for sequential processing.

Example:

```text
Acquire Data
      │
      ▼
Validate
      │
      ▼
Transform
      │
      ▼
Store
      │
      ▼
Catalog
```

Each stage owns a single responsibility and communicates through explicit contracts.

---

# Contract Ownership

Every public interaction contract has exactly one owning module.

The owning module is responsible for:

* Contract design
* Versioning
* Documentation
* Compatibility
* Evolution

Consumers may implement extension points but should not redefine another module's contracts.

---

# Allowed Interaction Direction

Interactions should follow the established architectural dependency hierarchy.

Illustrative examples include:

```text
Feature Engineering
        │
        ▼
Data Platform
```

```text
Machine Learning
        │
        ▼
Feature Engineering
```

```text
Backtesting
        │
        ▼
Portfolio Analytics
```

Interaction direction should always respect module boundaries and dependency governance.

---

# Prohibited Interaction Patterns

The following interaction patterns should be avoided:

* Circular collaboration
* Bidirectional command chains
* Direct access to internal implementation
* Shared mutable state
* Database access across module boundaries
* Leaking infrastructure details through public contracts

Modules should collaborate through published capabilities rather than implementation shortcuts.

---

# Interaction Granularity

Interactions should exchange business intent rather than technical details.

Preferred examples include:

* Request historical market data
* Execute experiment
* Generate features
* Evaluate strategy

Interactions should avoid exposing infrastructure concepts such as storage schemas, transport protocols, or internal processing steps.

---

# Stable Contracts

Public contracts should remain stable over time.

Implementations may evolve freely provided they preserve the contract's behavioral expectations.

Breaking changes should be minimized and documented through Engineering Decision Records.

---

# Error Propagation

Interactions should communicate failures through consistent and explicit mechanisms.

Errors should:

* Preserve context.
* Be meaningful.
* Avoid leaking internal implementation details.
* Support diagnostics.

Consumers should understand *what* failed without needing to understand *how* it failed internally.

---

# Interaction Governance

Interaction design should be reviewed whenever:

* New modules are introduced.
* Public contracts change.
* Module responsibilities evolve.
* Dependency direction changes.
* Cross-cutting capabilities are added.

Significant interaction changes should be documented through architectural governance.

---

# Relationship to Other Architecture Documents

This document complements:

* Solution Architecture
* Module Catalog
* Dependency Rules
* Boundary Definitions
* Design Principles
* Public Contracts
* Extensibility Model

Together these documents define how AIQuantTradingResearch modules collaborate while preserving architectural integrity.

---

# Guiding Statement

Modules should communicate through stable, intentional, and business-oriented contracts.

Well-designed interactions enable independent evolution, reduce coupling, and preserve the autonomy of every architectural capability, allowing AIQuantTradingResearch to grow without sacrificing clarity or maintainability.

## Release 1.6 Durable Experiment Evidence

Worker Durable Experiment intent invokes `IDurableExperimentUseCase` exactly once. Application reuses existing Experiment generation, projects reduced immutable evidence, then calls `IDurableExperimentEvidenceStore.Accept`. Infrastructure owns schema-v3 `experiment_results`, atomic acceptance, exact read-only lookup, and bounded storage classification. Worker owns neither SQL nor persistence semantics. `NewlyAccepted` and `EquivalentExisting` are successes; exact absence is `NotFound` and contradictory same-identity evidence is `IntegrityConflict`.

## Release 1.7 Durable Experiment Evidence Discovery

Worker Discovery intent invokes `IDurableExperimentDiscoveryUseCase` exactly
once. Application validates the exact Snapshot Identity, exact Experiment
Definition Identity, and positive bound, then calls its storage-independent
discovery abstraction once. Infrastructure forwards that abstraction through the
SQLite Experiment Result store, which reads schema-v3 `experiment_results` with
the exact dual-identity predicate and binary Result Identity ordering. Returned
`DurableExperimentEvidence` is complete immutable predecessor evidence; zero
matches are a successful empty collection. Worker owns configuration, precedence,
bounded presentation, and exit behavior, never SQL, store mechanics, provider
fallback, retries, or writes.
