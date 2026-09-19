# AIQuantTradingResearch Roadmap

**Status:** Active
**Version:** 1.1
**Last Updated:** 2026-08-27
**Maintainers:** AIQuantTradingResearch Team

---

## Current Release Status

**Release 1.12 — Public Reference Deployment Implementation & Stabilization** is complete and published as `v1.12.0`. WP01-WP08 are accepted and lifecycle-complete. Its bounded public-reference/demo deployment remains West US 2 on Azure App Service Linux F1/Free, using a public GHCR immutable image and persistent `/home` SQLite; it does not claim production readiness, an SLA, or high availability.

Initiative-1.11 remains historical feasibility evidence, not Product Release 1.11. Product Release 1.11 remains abandoned/nonexistent. The historical West Central US target is retained as evidence; constrained-infrastructure recovery follows the governed order West Central US → West US 2 → Central US → South Central India.

---

# Vision

AIQuantTradingResearch advances through small, verifiable releases that add
platform capability without weakening architectural boundaries, reproducibility,
or engineering governance. Completed releases are evidence; future releases are
direction until separately defined and accepted.

---

# Roadmap Principles

- Deliver value incrementally and preserve architectural consistency.
- Keep releases demonstrable, deterministic where applicable, and documented.
- Select every foundational external runtime, library, framework, or tool through
  an explicit engineering selection record before implementation.
- Do not infer implementation scope from a future milestone placeholder.
- Integrate release work through a dedicated branch, acceptance, pull request,
  verification, and merge to `main`; direct release integration into `main` is
  prohibited except under separately authorized emergency/hotfix governance.

---

# Completed Platform Foundations

| Releases | Delivered capability |
| --- | --- |
| 0.1-0.8 | Engineering governance, architecture, AI engineering toolkit, and executable .NET solution foundation. |
| 0.9 | Deterministic offline research platform. |
| 1.0 | Provider-backed historical market-data acquisition. |
| 1.1 | Durable provider-independent historical market-data persistence. |
| 1.2 | Deterministic immutable research datasets, snapshots, and catalog evidence. |
| 1.3 | Fixed deterministic one-shot research pipeline. |
| 1.4 | Deterministic simple-return feature generation. |
| 1.5 | Deterministic research experiment foundation. |
| 1.6 | Durable experiment evidence with schema v3. |
| 1.7 | Bounded durable experiment-evidence discovery. |
| 1.8 | Python and AI engineering foundation: isolated `.venv`, governed scientific stack, local JSON-over-stdio boundary, and permanent interoperability tests. |

Release 1.8 is complete. It did not implement product ML behavior, real-time
visualization, observability, explainability, or backtesting.

---

# Canonical Next Release Sequence

```text
1.9 Visualization -> 1.10 Observability -> 1.12 Public Reference Deployment
-> 1.13 Historical Market Visualization -> 1.14 Technical Analysis / Quantitative
Feature Foundation -> 2.0 Lightweight ML Evaluation -> 2.1 Machine Learning
-> 2.2 Explainable AI -> 2.3 Backtesting
```

The corresponding capability narrative is:

```text
Acquire -> Persist -> Validate -> Transform -> Stream -> Visualize -> Observe -> Learn -> Explain -> Backtest
```

## Release 1.9 - Real-Time Financial Data Visualization

**Status:** COMPLETE - the deterministic simulated/replay visualization foundation is accepted as a predecessor capability. It is not a live market-data feed or trading surface.

The delivered flow reuses the existing pipeline and publishes a bounded local
canonical JSON read model for a read-only Streamlit presentation. Current
visualization/demo flows use deterministic simulated/replay data for local
testing and demonstration, not a live market-data feed. The UI does not call a
provider, open SQLite, reconstruct features, or run a parallel pipeline. ML
training, trading, real-provider streaming, a broad observability platform,
and new Release 1.10/2.0 scope remain excluded.

## Release 1.10 - OpenTelemetry & Pipeline Observability

**Status:** COMPLETE - OpenTelemetry pipeline/boundary observability and the truthful System Health presentation are accepted predecessor capabilities.

The future release is intended to introduce governed OpenTelemetry for pipeline
and stage timing, throughput, provider behavior, persistence latency, failures,
and appropriate Python-boundary telemetry, including a Streamlit System Health
view. An explicit OpenTelemetry selection record is required before
implementation. ML training is excluded.


## Release 1.13 - Historical Market Visualization & Provider Abstraction

**Status:** PLANNED - governed planning release; implementation requires separate work-package authority.

Release 1.13 extends the bounded Release 1.12 public-reference foundation with a provider-independent, cache-first historical OHLCV path and public Market Research visualization for BTC/USD and ETH/USD. Vike is the intended server-side public historical-data provider; Twelve Data remains preserved for private/internal research and is not an automatic public-chart fallback.

The public experience is research/demo only: no trading, recommendations, ML execution, technical indicators, or backtesting. It preserves the existing canonical .NET ownership, Streamlit read-model boundary, System Health, Azure App Service Linux F1 constraints, and the `$0.00` recurring-infrastructure-cost objective.

## Release 1.14 - Technical Analysis & Quantitative Feature Foundation

**Status:** RESERVED - milestone and roadmap reservation only; detailed planning and implementation require separate authority.

Release 1.14 follows Release 1.13 and precedes Release 2.0. It reserves a canonical technical-analysis/feature layer over governed OHLCV so future UI and ML research do not create inconsistent indicator calculations. Candidate indicator families are not a frozen implementation list; trading recommendations and execution remain excluded.

## Release 2.0 - Lightweight Machine Learning Evaluation

**Status:** PLANNED - milestone placeholder only.

The future release is intended to test one narrow deterministic ML hypothesis,
with Logistic Regression through governed scikit-learn as the preferred initial
candidate unless a later definition finds a blocker. It requires temporal-not-
random evaluation, baseline comparison, reproducible experiment identity and
metrics, and experiment visualization. It is not a broad reusable ML platform
or strategy backtesting.

## Release 2.1 - Machine Learning

**Status:** RESEQUENCED - existing milestone identity and broader scope
preserved; no work packages are defined here.

## Release 2.2 - Explainable AI

**Status:** RESEQUENCED - existing milestone identity and broader scope
preserved; no work packages are defined here.

## Release 2.3 - Backtesting

**Status:** PLANNED - milestone placeholder only.

The future release is intended to evaluate decision policies and research
outputs historically with explicit temporal integrity and trading assumptions.
Detailed APIs, costs, slippage, portfolio rules, metrics, persistence, and UI
remain undefined.

---

# Long-Term Direction

Cloud/SRE, MLOps, production deployment, risk/portfolio intelligence,
multi-asset support, distributed processing, and advanced visualization remain
future directions. They must be separately justified, designed, and governed;
their mention here is not implementation authorization.

---

# Measuring Progress

Progress is measured through stable architecture, documentation quality,
automated testing, reproducibility, observability, maintainability, security,
and contributor experience-not feature count alone.

---

# Living Roadmap

This roadmap is evolutionary. Changes must preserve the project's long-term
vision while remaining consistent with the Project Constitution, Engineering
Handbook, and release-specific authorities.
