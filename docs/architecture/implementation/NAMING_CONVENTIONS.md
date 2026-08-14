# Naming Conventions

**Status:** Draft
**Version:** 1.0
**Last Updated:** 2026-08-09
**Owners:** AIQuantTradingResearch Team

---

# Purpose

The Naming Conventions document defines the naming standards used throughout AIQuantTradingResearch.

Consistent naming improves readability, discoverability, maintainability, and architectural clarity by ensuring that similar concepts are expressed consistently across the entire platform.

Naming should communicate intent before implementation.

---

# Vision

Every name within the platform should be understandable without requiring knowledge of its implementation.

A contributor should be able to infer the purpose and responsibility of a project, namespace, class, or method from its name alone.

Good names reduce cognitive load and simplify long-term evolution.

---

# Naming Philosophy

Names should describe **what something represents** rather than **how it works**.

Prefer names that express:

* Business concepts
* Architectural responsibilities
* Domain language
* Observable behavior

Avoid names based on:

* Temporary implementation details
* Frameworks
* Internal algorithms
* Historical decisions

Names should remain meaningful as implementations evolve.

---

# General Principles

Names throughout the platform should be:

* Clear
* Precise
* Consistent
* Concise
* Business-oriented
* Stable
* Unambiguous

When choosing between brevity and clarity, prefer clarity.

---

# Domain Language

The platform should consistently use the terminology established in the Domain Context and Data Glossary.

Examples include:

* Market
* Instrument
* Quote
* Candle
* Provider
* Pipeline
* Dataset
* Strategy
* Feature
* Portfolio

Using a shared vocabulary improves communication between engineers, researchers, and contributors.

---

# Repository Naming

Top-level repository directories should describe their responsibility.

Illustrative examples:

```text
docs/
eng/
src/
tests/
samples/
benchmarks/
tools/
assets/
```

Directory names should remain singular in purpose and avoid overlapping responsibilities.

---

# Solution Naming

Solution files should clearly identify the product.

Example:

```text
AIQuantTradingResearch.slnx
```

Multiple solution files introduced in the future should communicate their purpose explicitly.

Illustrative examples:

```text
AIQuantTradingResearch.Samples.slnx
AIQuantTradingResearch.Benchmarks.slnx
```

---

# Project Naming

Project names should reflect architectural responsibility.

Examples:

```text
AIQuantTradingResearch.Domain
AIQuantTradingResearch.Application
AIQuantTradingResearch.Infrastructure
AIQuantTradingResearch.Worker
```

Projects should avoid technology-specific or implementation-specific names whenever possible.

---

# Namespace Naming

Namespaces should mirror project organization.

Illustrative examples:

```text
AIQuantTradingResearch.Data
AIQuantTradingResearch.Data.Providers
AIQuantTradingResearch.Data.Pipelines
AIQuantTradingResearch.Domain.Instruments
```

Namespace hierarchy should communicate increasing specialization.

---

# Interface Naming

Interfaces should describe capabilities.

Examples:

```text
IMarketDataProvider
IDataPipeline
IFeatureExtractor
IStrategyExecutor
```

Interface names should represent behavior rather than implementation.

---

# Class Naming

Classes should represent concrete responsibilities.

Examples:

```text
YahooFinanceProvider
HistoricalDataPipeline
FeatureCatalog
PortfolioAnalyzer
```

Avoid generic names such as:

* Manager
* Helper
* Utility
* Processor
* Handler

unless the responsibility genuinely matches the term.

---

# Record and Value Object Naming

Records should represent domain concepts.

Examples:

```text
MarketQuote
Candlestick
TradeSignal
FeatureVector
PortfolioSnapshot
```

Names should correspond directly to business terminology.

---

# Enumeration Naming

Enumerations should represent well-defined classifications.

Examples:

```text
MarketType
OrderSide
FailureCategory
ProviderCapability
```

Enumeration members should be self-explanatory.

---

# Method Naming

Methods should describe observable behavior.

Examples:

```text
LoadHistoricalData()
ImportDataset()
CalculateFeatures()
GenerateSignals()
```

Method names should communicate outcomes rather than implementation details.

---

# Property Naming

Properties should describe state.

Examples:

```text
Symbol
Timestamp
Volume
OpenPrice
ClosePrice
ConfidenceScore
```

Properties should avoid redundant prefixes or suffixes.

---

# Event Naming

Events should describe completed business occurrences.

Examples:

```text
MarketDataImported
DatasetValidated
PipelineCompleted
ProviderUnavailable
```

Event names should be expressed in the past tense.

---

# Exception Naming

Exceptions should clearly describe exceptional conditions.

Examples:

```text
ProviderUnavailableException
InvalidDatasetException
ConfigurationException
```

Exception names should communicate the problem rather than the implementation.

---

# Configuration Naming

Configuration objects should end with **Options**.

Examples:

```text
RetryOptions
StorageOptions
YahooFinanceOptions
PipelineOptions
```

Configuration sections should align naturally with these names.

---

# File Naming

Source files should match the primary type they contain.

Examples:

```text
MarketQuote.cs
YahooFinanceProvider.cs
RetryOptions.cs
FeatureCatalog.cs
```

Documentation files should use uppercase snake case.

Examples:

```text
PRODUCT_VISION.md
DATA_PLATFORM_VISION.md
RESILIENCE_MODEL.md
```

Consistency across documentation improves navigation.

---

# Test Naming

Test project names should mirror production projects.

Examples:

```text
AIQuantTradingResearch.Domain.Tests
AIQuantTradingResearch.Application.Tests
AIQuantTradingResearch.Infrastructure.Tests
AIQuantTradingResearch.Architecture.Tests
```

Test methods should clearly describe expected behavior.

Illustrative pattern:

```text
Should_Load_Historical_Data_When_Source_Is_Available
```

Alternative naming conventions may be adopted if they remain consistent.

---

# Plugin Naming

Plugins should communicate the capability they provide.

Examples:

```text
YahooFinancePlugin
BinancePlugin
CsvImportPlugin
FeatureEngineeringPlugin
```

Plugin names should remain independent of loading mechanisms.

---

# Package Naming

Published packages should remain aligned with project names.

Examples:

```text
AIQuantTradingResearch.Core
AIQuantTradingResearch.Data
AIQuantTradingResearch.Plugins
```

Package names should remain stable to support long-term compatibility.

---

# Documentation Naming

Documentation titles should describe architectural concepts rather than implementation artifacts.

Examples:

```text
MODULE_CATALOG.md
PUBLIC_CONTRACTS.md
IMPLEMENTATION_GUIDELINES.md
```

Document names should remain concise and discoverable.

---

# Abbreviations

Abbreviations should be minimized.

Prefer:

```text
Configuration
Identifier
Provider
```

Instead of:

```text
Config
Id
Prov
```

Widely recognized industry abbreviations may be used where they improve readability.

Examples include:

* API
* HTTP
* JSON
* CSV
* URI

---

# Anti-Patterns

The following naming practices should be avoided:

* Generic names such as Manager, Helper, Utility, or Miscellaneous.
* Technology-specific names embedded in business concepts.
* Inconsistent abbreviations.
* Ambiguous terminology.
* Historical implementation references.
* Excessively long names.
* Single-letter identifiers outside small local scopes.

Good names should reduce explanation rather than require it.

---

# Governance

Naming consistency should be reviewed during code reviews and architectural discussions.

When introducing new business terminology, contributors should ensure consistency with the Domain Context, Data Glossary, and Module Catalog.

Significant terminology changes should be documented to preserve architectural consistency.

---

# Relationship to Other Documents

This document complements:

* Domain Context Map
* Data Glossary
* Module Catalog
* Project Structure
* Coding Principles
* Implementation Guidelines
* Dependency Injection
* Public Contracts

Together these documents establish a shared language across architecture, implementation, documentation, and engineering governance.

---

# Future Evolution

Future guidance may expand to include:

* Plugin naming standards
* API resource naming
* Database naming conventions
* Telemetry naming
* Metric naming
* Event taxonomy
* AI model naming

These additions should reinforce consistency while preserving the principles established in this document.

---

# Guiding Statement

Names are part of the architecture.

AIQuantTradingResearch uses a shared language that expresses business concepts, architectural intent, and engineering responsibilities consistently across the entire platform.

Well-chosen names make software easier to understand, easier to evolve, and easier to trust.
