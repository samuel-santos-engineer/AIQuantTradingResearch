

# Dependency Injection

**Status:** Draft
**Version:** 1.0
**Last Updated:** 2026-08-09
**Owners:** AIQuantTradingResearch Team

---

# Purpose

The Dependency Injection document defines how AIQuantTradingResearch composes architectural modules into a functioning application while preserving modularity, testability, and architectural integrity.

Dependency Injection is the mechanism through which independently developed capabilities become a cohesive platform.

The dependency injection framework is an implementation detail; the composition model is an architectural concern.

---

# Vision

Application composition should remain simple, explicit, deterministic, and aligned with the platform's architectural boundaries.

Dependencies should be declared rather than discovered, and object creation should remain centralized and predictable.

The composition model should support independent evolution without introducing hidden coupling.

---

# Composition Philosophy

Dependency Injection is responsible for assembling the platform—not implementing business behavior.

Composition should:

* Respect architectural boundaries.
* Preserve module independence.
* Enable extensibility.
* Simplify testing.
* Avoid hidden dependencies.
* Support future evolution.

The dependency injection container serves the architecture rather than defining it.

---

# Guiding Principles

Application composition follows these principles:

* Explicit dependencies.
* Centralized composition.
* Module ownership.
* Constructor injection.
* Minimal coupling.
* Deterministic registration.
* Replaceable implementations.

These principles apply across every architectural module.

---

# Composition Root

The platform should expose a single Composition Root responsible for assembling the application.

Illustrative responsibility:

```text
Application Startup
        │
        ▼
Composition Root
        │
        ▼
Module Registration
        │
        ▼
Application Execution
```

No other module should create the application dependency graph.

Centralized composition improves predictability and maintainability.

---

# Implemented Release 1.3 and 1.4 Composition

`AIQuantTradingResearch.Worker` is the current composition root. Its one-shot execution lifecycle is:

```text
Create generic host builder
        ↓
Read TwelveData:ApiKey, Persistence:DatabasePath, and Dataset inputs
        ↓
AddApplication()
        ↓
AddInfrastructure(TwelveDataConfiguration)
        ↓
Build host
        ↓
Resolve PipelineExecution
        ↓
Create and execute one IPipelineExecutionUseCase request
        ↓
Present bounded semantic evidence and exit
```

`AddApplication` registers the research/persistence and dataset seams, the
transient `IPipelineExecutionUseCase`, and singleton stateless
`IPipelineRequestFactory`. The configured `AddInfrastructure` overload preserves
the provider graph, singleton SQLite configuration/connection factory, and
transient historical, snapshot-store, and catalog implementations. Worker
resolves the bounded pipeline through DI and does not construct implementations.

`TwelveData:ApiKey`, `Persistence:DatabasePath`, `Dataset:Target`,
`Dataset:From`, and `Dataset:To` are mandatory external configuration. Release
1.3 adds no `Pipeline:*` semantic configuration: topology, identity scheme,
stage order, and failure policy are fixed Application semantics. Worker reports
deterministic failures and exits non-zero for invalid configuration. SQLite
connections are operation-owned and resolution alone creates no database. No
hosted loop, pipeline-managed acquisition, scheduler, retry framework, or
durable pipeline run history is implemented.

Release 1.4 adds transient `IFeatureGenerationUseCase`, `IFeatureComputer`,
and `IFeatureGenerationValidator` registrations in Application. They reuse the
existing transient `IDatasetSnapshotStore`; resolving the graph neither looks
up a snapshot nor executes feature generation or creates a database. The Worker
uses `Feature:SnapshotIdentity` and `Feature:SnapshotVersion` to select one
bounded feature execution; it owns no feature-computation semantics. Feature
output remains in-memory evidence only, with no feature persistence, cache, or
run history.

Release 1.5 adds transient `IExperimentGenerationUseCase`,
`IExperimentSummaryComputer`, and `IExperimentGenerationValidator`
registrations in Application. They reuse the existing transient Feature
generation graph and snapshot store. Resolution remains side-effect-free: it
does not look up a snapshot, generate a feature or experiment, create a
database, call a provider, or persist experiment evidence. Worker binds only
`Experiment:SnapshotIdentity` and `Experiment:SnapshotVersion`, then invokes
one code-owned `simple-return-descriptive-summary-v1` request when Experiment
mode is explicitly selected.

---

# Architectural Composition

Application composition mirrors the implemented production graph:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

Dependency Injection should preserve this architecture rather than bypass it.

---

# Constructor Injection

Constructor injection is the preferred mechanism for expressing dependencies.

Constructor injection:

* Makes dependencies explicit.
* Encourages immutable design.
* Simplifies testing.
* Prevents hidden runtime requirements.

Alternative injection mechanisms should be introduced only when architectural justification exists.

---

# Module Registration

Each architectural module should own its service registrations as real services are introduced. Current organization:

```text
Application
        AddApplication()

Infrastructure
        AddInfrastructure()
```

The Worker composition root invokes both boundaries without owning implementation details. Additional module or plugin registration remains planned for later releases.

---

# Dependency Ownership

Modules own the registrations associated with their capabilities.

Ownership includes:

* Service registrations.
* Configuration bindings.
* Internal implementations.
* Public contracts.

Ownership should remain localized to preserve modularity.

---

# Service Lifetimes

Service lifetime should reflect architectural responsibility rather than implementation convenience.

Illustrative guidance:

| Lifetime  | Typical Responsibility                                   |
| --------- | -------------------------------------------------------- |
| Singleton | Configuration, immutable services, shared infrastructure |
| Scoped    | Execution context, request or pipeline state             |
| Transient | Independent computations, lightweight services           |

Lifetime decisions should prioritize correctness and predictability.

---

# Configuration Integration

Configuration objects should be composed through Dependency Injection.

Configuration should be:

* Strongly typed.
* Validated.
* Immutable after startup whenever practical.

Configuration composition should align with the Configuration Model document.

---

# Plugin Composition

Plugins should extend the platform through published contracts.

Plugins may:

* Register new services.
* Provide new capabilities.
* Contribute implementations.

Plugins should not replace or modify existing architectural behavior unless explicitly designed to do so.

Extension is preferred over mutation.

---

# Testing Support

Dependency Injection should simplify automated testing.

Testing should support:

* Dependency substitution.
* Mock implementations.
* Test-specific configuration.
* Isolated execution.

Well-designed composition reduces testing complexity.

---

# Discoverability

Dependency registration should remain predictable.

Engineers should be able to determine:

* Where a service is registered.
* Which module owns it.
* Which contract it implements.
* How it participates in application composition.

Discoverability improves maintainability and onboarding.

---

# Extensibility

The composition model should support future architectural evolution.

Illustrative capabilities include:

* Plugin discovery.
* Module auto-registration.
* Feature flags.
* Conditional composition.
* Environment-specific services.

Extensibility should preserve architectural clarity.

---

# Anti-Patterns

The following practices should be avoided:

* Service Locator.
* Static dependency containers.
* Hidden dependency resolution.
* Property injection as the primary mechanism.
* Circular registrations.
* Runtime mutation of unrelated modules.
* Excessive conditional registration.
* Registering implementation details outside their owning module.

These practices reduce transparency and increase architectural complexity.

---

# Governance

Dependency Injection should preserve the documented architecture.

Composition changes should remain consistent with:

* Module boundaries.
* Dependency Rules.
* Public Contracts.
* Extensibility Model.
* Plugin Architecture.

Significant composition changes should be documented through Engineering Decision Records.

---

# Relationship to Other Documents

This document complements:

* Implementation Guidelines
* Coding Principles
* Project Structure
* Dependency Rules
* Public Contracts
* Module Catalog
* Plugin Architecture
* Configuration Model
* Extensibility Model
* Solution Architecture

Together these documents define how AIQuantTradingResearch assembles independent architectural capabilities into a cohesive, maintainable, and extensible platform.

---

# Future Evolution

Future enhancements may include:

* Automatic module discovery.
* Keyed service registration.
* Decorator pipelines.
* Cross-cutting interception.
* Dynamic plugin loading.
* Feature-based composition.
* Cloud-native service composition.
* Compile-time dependency validation.

These capabilities should extend the composition model while preserving architectural simplicity and explicit dependency management.

---

# Guiding Statement

Dependency Injection is the architecture's assembly mechanism.

AIQuantTradingResearch composes software through explicit, centralized, and modular dependency management that preserves architectural boundaries, enables extensibility, and simplifies long-term evolution.

Well-designed composition makes a complex platform feel simple.

## Release 1.6 Durable Experiment Composition

`AddApplication` registers `IDurableExperimentUseCase` as `DurableExperimentUseCase`; `AddInfrastructure` registers `IDurableExperimentEvidenceStore` as `SqliteExperimentResultStore`. Each is registered exactly once using the accepted transient lifetime. Resolution is side-effect free: it does not open or migrate SQLite, generate an Experiment, persist evidence, or contact a provider. The Worker reuses `Persistence:DatabasePath` and selects Durable Experiment before Experiment, Feature, and the fixed pipeline when both durable selectors are present.
