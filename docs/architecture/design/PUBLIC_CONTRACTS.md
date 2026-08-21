
# Public Contracts

**Status:** Draft
**Version:** 1.0
**Last Updated:** 2026-08-09
**Owners:** AIQuantTradingResearch Team

---

# Purpose

The Public Contracts document defines the principles, responsibilities, and governance of the contracts that enable collaboration between architectural modules in AIQuantTradingResearch.

Public contracts are the only supported mechanism through which modules expose capabilities to one another.

They protect module boundaries, promote independent evolution, and preserve architectural integrity.

---

# Vision

Public contracts should provide stable, intentional, and business-oriented capabilities.

Consumers depend on contracts rather than implementations, allowing providers to evolve internally while maintaining compatibility and trust.

A well-designed contract is a long-term architectural commitment.

---

# Contract Philosophy

A public contract is a promise between architectural modules.

It describes **what** capability is offered, not **how** it is implemented.

Contracts should express business intent, remain technology independent, and evolve more slowly than the implementations behind them.

---

# Contract-First Design

Public contracts should be designed before implementation begins.

The recommended lifecycle is:

```text
Business Capability
        │
        ▼
Contract Design
        │
        ▼
Architecture Review
        │
        ▼
Implementation
        │
        ▼
Verification
        │
        ▼
Evolution
```

Implementation should realize a reviewed contract rather than define one implicitly.

---

# Contract Categories

## Capability Contracts

Expose a business capability owned by a module.

Examples include:

* Market data retrieval
* Feature generation
* Strategy evaluation
* Portfolio analysis

---

## Command Contracts

Request that another module perform an action.

Examples include:

* Import dataset
* Execute backtest
* Train model

Commands communicate intent and may modify system state.

---

## Query Contracts

Retrieve information without changing state.

Examples include:

* Retrieve experiment history
* Load feature metadata
* Obtain portfolio metrics

Queries should remain deterministic and free of side effects.

---

## Event Contracts

Publish significant business events.

Examples include:

* Dataset Imported
* Features Generated
* Model Trained
* Backtest Completed

Events enable asynchronous collaboration while preserving module autonomy.

---

## Configuration Contracts

Describe supported configuration options without exposing implementation details.

Configuration contracts define behavior, not infrastructure.

---

## Extension Contracts

Enable new providers, analytical models, plugins, or integrations to extend the platform without modifying existing modules.

Extension contracts are fundamental to the platform's long-term extensibility.

---

# Contract Characteristics

Every public contract should be:

* Stable
* Explicit
* Cohesive
* Discoverable
* Technology independent
* Testable
* Version-aware

Contracts should remain focused on a single business capability.

---

# Contract Ownership

Every contract has one owning module.

The owning module is responsible for:

* Design
* Documentation
* Versioning
* Compatibility
* Evolution
* Deprecation strategy

Consumers may rely on contracts but should not redefine them.

## Implemented Release 1.0 Research Contracts

Application owns the public, provider-independent boundary for the current research operation:

* `IResearchUseCase` exposes execution of a `ResearchRequest` and returns a `ResearchOutcome`.
* `IObservationSource` is the Application-owned external observation port.
* `ObservationSourceResult` and `ObservationSourceFailure` describe expected source outcomes without exposing Infrastructure details.
* `ResearchResult` associates the requested target and observation count with the Domain-owned `MeanPrice`.
* `ResearchFailure` distinguishes invalid requests, unsupported targets, insufficient observations, source unavailability, access denial, usage limits, and invalid source responses.

`ResearchUseCase` and `TwelveDataObservationSource` are internal implementations behind these contracts. Twelve Data transport DTOs, the client, normalizer, and provider-local failure evidence are also internal Infrastructure details. `TwelveDataConfiguration` is the intentional public composition surface. Friend access is limited to Application.Tests and Infrastructure.Tests and does not make internal types supported public API; Architecture.Tests has no friend access.

---

# Implemented Release 1.4 Feature Contracts

Application owns `FeatureDefinition`, typed Feature Definition/Feature Set
identities, immutable feature evidence, `FeatureGenerationRequest`,
`FeatureGenerationResult`, and `IFeatureGenerationUseCase`. The only built-in
definition is `simple-return-lag-1-v1`; its code-owned semantics use decimal
`(p[i] / p[i-1]) - 1` over accepted snapshot order. The result belongs to the
current observation and preserves its original timestamp/offset. Empty and
single-observation snapshots are successful empty results.

`aiq-feature-identity-v1` uses canonical SHA-256 fingerprints represented as
64 lowercase hexadecimal characters. Definition and set identities are distinct
and bind feature evidence to the exact dataset snapshot/version, provenance, and
lineage. Snapshot NotFound, dependency unavailable, invalid evidence/numeric
input, and integrity conflict remain bounded result failures; unexpected defects
propagate. No provider DTO, HTTP contract, SQLite record, feature persistence,
or configurable formula/lag belongs to this Application contract surface.

# Contract Evolution

Contracts should evolve conservatively.

Preferred evolution includes:

* Adding optional capabilities.
* Introducing new operations.
* Publishing additional events.
* Extending metadata.

Breaking changes should be avoided whenever practical.

When unavoidable, they should be documented, versioned, and communicated through established governance processes.

---

# Contract Compatibility

Public contracts should strive for backward compatibility.

Changes should preserve existing consumer behavior whenever possible.

When compatibility cannot be maintained:

* Provide migration guidance.
* Define transition periods.
* Document rationale through Engineering Decision Records.

Contract stability is essential for long-term maintainability.

---

# Contract Naming

Contract names should communicate business intent.

Preferred examples include:

* RetrieveHistoricalPrices
* GenerateFeatures
* ExecuteBacktest
* EvaluatePortfolio

Contracts should avoid exposing implementation technologies, storage mechanisms, or transport protocols.

---

# Contract Validation

Public contracts should be verified through automated testing.

Validation should include:

* Behavioral compatibility
* Expected inputs and outputs
* Error scenarios
* Version compatibility
* Consumer expectations

Contract validation strengthens confidence during continuous evolution.

---

# Anti-Patterns

The following practices should be avoided:

* Leaking implementation details
* Exposing infrastructure concerns
* Large, unfocused contracts
* Shared mutable state
* Technology-specific contracts
* Frequent breaking changes
* Hidden behavioral assumptions

These anti-patterns increase coupling and reduce architectural flexibility.

---

# Relationship to Other Design Documents

This document complements:

* Design Principles
* Module Interactions
* Extensibility Model
* Configuration Model
* Versioning Strategy
* Module Catalog
* Boundary Definitions

Together these documents define how architectural capabilities are exposed, consumed, and governed.

---

# Release 1.1 Persistence Contracts

Application owns `IHistoricalObservationStore`, `IPersistHistoricalObservationsUseCase`, and the provider-independent persistence result vocabulary. `NewlyAccepted`, `Idempotent`, and `Conflict` are distinct outcomes; `Unavailable` and `InvalidData` are the bounded persistence failures. Historical retrieval is successful with a non-null empty collection when no observations exist.

These contracts preserve exact opaque targets, `PriceObservation` timestamp/offset and decimal fidelity, and do not expose SQLite records, SQL, connection factories, or provider DTOs. SQLite remains an Infrastructure implementation detail.

---

# Future Evolution

As the platform grows, public contracts may include additional capabilities, extension points, and interaction patterns.

Their evolution should always prioritize stability, discoverability, and backward compatibility while preserving the architectural independence of every module.

---

# Guiding Statement

Public contracts are the architectural language of AIQuantTradingResearch.

They define how capabilities are shared, how modules collaborate, and how the platform evolves without sacrificing clarity, stability, or modularity.

A carefully designed contract is an investment in the long-term sustainability of the entire solution.
