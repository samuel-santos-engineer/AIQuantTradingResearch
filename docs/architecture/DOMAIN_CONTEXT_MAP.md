
# Domain Context Map

**Status:** Draft
**Version:** 1.0
**Last Updated:** 2026-08-08
**Maintainers:** AIQuantTradingResearch Team

---

# Purpose

The Domain Context Map defines the major business domains of AIQuantTradingResearch and the relationships between them.

It establishes clear boundaries, responsibilities, and interactions, enabling the platform to evolve through well-defined architectural contexts.

This document complements the Business Domain Model by focusing on collaboration between domains rather than individual business concepts.

---

# Architectural Vision

AIQuantTradingResearch is organized as a collection of collaborating business domains.

Each domain owns a specific responsibility and communicates through explicit contracts.

Business capabilities should remain cohesive while minimizing coupling between domains.

---

# High-Level Context Map

```text
                         ┌─────────────────────────┐
                         │     Market Providers    │
                         │ Binance • Coinbase •    │
                         │ Kraken • NASDAQ • CME   │
                         └────────────┬────────────┘
                                      │
                                      ▼
                    ┌────────────────────────────────┐
                    │      Market Data Domain        │
                    └────────────────┬───────────────┘
                                     │
                                     ▼
                    ┌────────────────────────────────┐
                    │      Data Management Domain    │
                    └────────────────┬───────────────┘
                                     │
                                     ▼
                    ┌────────────────────────────────┐
                    │    Feature Engineering Domain  │
                    └────────────────┬───────────────┘
                                     │
                    ┌────────────────┴────────────────┐
                    ▼                                 ▼
        ┌────────────────────────┐      ┌────────────────────────┐
        │ Machine Learning Domain│      │ Quant Research Domain  │
        └──────────────┬─────────┘      └──────────────┬─────────┘
                       └──────────────┬────────────────┘
                                      ▼
                    ┌────────────────────────────────┐
                    │     Strategy Evaluation Domain │
                    └────────────────┬───────────────┘
                                     ▼
                    ┌────────────────────────────────┐
                    │      Backtesting Domain        │
                    └────────────────┬───────────────┘
                                     ▼
                    ┌────────────────────────────────┐
                    │ Risk & Portfolio Analytics      │
                    └────────────────┬───────────────┘
                                     ▼
                    ┌────────────────────────────────┐
                    │ Visualization & Reporting       │
                    └────────────────────────────────┘
```

---

# Domain Responsibilities

## Market Data Domain

Responsible for acquiring market information from external providers.

### Responsibilities

* Exchange connectivity
* Data ingestion
* Quote collection
* Candle collection
* Symbol discovery
* Time synchronization

Produces validated raw market observations.

---

## Data Management Domain

Responsible for transforming raw market observations into trusted datasets.

### Responsibilities

* Data validation
* Normalization
* Storage
* Versioning
* Dataset creation
* Metadata management

Produces reproducible datasets.

---

## Feature Engineering Domain

Responsible for transforming datasets into analytical features.

### Responsibilities

* Technical indicators
* Statistical features
* Feature pipelines
* Feature catalog
* Feature versioning

Produces reusable feature sets.

---

## Machine Learning Domain

Responsible for model development and prediction.

### Responsibilities

* Training
* Validation
* Evaluation
* Model registry
* Prediction
* Explainability

Produces predictions and model metadata.

---

## Quantitative Research Domain

Responsible for experimentation and hypothesis validation.

### Responsibilities

* Research workflows
* Experiment tracking
* Benchmarking
* Comparative analysis
* Statistical validation

Produces validated research outcomes.

---

## Strategy Evaluation Domain

Responsible for converting analytical outputs into investment strategies.

### Responsibilities

* Signal generation
* Rule evaluation
* Strategy configuration
* Strategy comparison

Produces executable strategy definitions.

---

## Backtesting Domain

Responsible for historical simulation.

### Responsibilities

* Historical replay
* Performance evaluation
* Transaction cost simulation
* Benchmark comparison

Produces reproducible simulation results.

---

## Risk & Portfolio Analytics Domain

Responsible for evaluating portfolio quality.

### Responsibilities

* Risk metrics
* Position sizing
* Portfolio construction
* Exposure analysis
* Performance attribution

Produces portfolio analytics and risk reports.

---

## Visualization & Reporting Domain

Responsible for communicating analytical results.

### Responsibilities

* Dashboards
* Reports
* Charts
* Experiment summaries
* Executive views

Produces human-readable insights.

---

# Domain Relationships

| Domain                     | Depends On                                             |
| -------------------------- | ------------------------------------------------------ |
| Market Data                | External Providers                                     |
| Data Management            | Market Data                                            |
| Feature Engineering        | Data Management                                        |
| Machine Learning           | Feature Engineering                                    |
| Quantitative Research      | Data Management, Feature Engineering, Machine Learning |
| Strategy Evaluation        | Machine Learning, Quantitative Research                |
| Backtesting                | Strategy Evaluation                                    |
| Risk & Portfolio Analytics | Backtesting                                            |
| Visualization & Reporting  | All domains                                            |

Dependencies should flow in one direction to preserve modularity and reduce coupling.

---

# Architectural Principles

Each domain should:

* Own its data.
* Expose explicit contracts.
* Hide implementation details.
* Be independently testable.
* Minimize coupling.
* Maximize cohesion.
* Support reproducible workflows.

---

# Future Evolution

As the platform grows, additional contexts may emerge, including:

* MLOps
* Experiment Orchestration
* Model Monitoring
* Data Quality
* Cloud Infrastructure
* Authentication & Authorization
* API Gateway

New contexts should be introduced only when justified by evolving business capabilities.

---

# Relationship to Other Architecture Documents

| Document                                | Purpose                                |
| --------------------------------------- | -------------------------------------- |
| Product Vision                          | Why the project exists                 |
| Project Constitution                    | Engineering principles                 |
| Business Domain Model                   | Business vocabulary and concepts       |
| Domain Context Map                      | Relationships between business domains |
| Software Architecture                   | Technical implementation               |
| Engineering Infrastructure Architecture | Engineering platform and governance    |

Together, these documents provide a complete architectural view of AIQuantTradingResearch.

---

# Guiding Statement

Well-defined domain boundaries enable scalable software.

The Domain Context Map ensures that business capabilities evolve through clear responsibilities, explicit collaboration, and sustainable architectural decisions.
