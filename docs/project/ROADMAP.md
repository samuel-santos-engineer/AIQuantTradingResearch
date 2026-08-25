# AIQuantTradingResearch Roadmap

**Status:** Active
**Version:** 1.1
**Last Updated:** 2026-08-24
**Maintainers:** AIQuantTradingResearch Team

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
| 0.1–0.8 | Engineering governance, architecture, AI engineering toolkit, and executable .NET solution foundation. |
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
1.9 Visualization → 1.10 Observability → 2.0 Lightweight ML Evaluation
→ 2.1 Machine Learning → 2.2 Explainable AI → 2.3 Backtesting
```

The corresponding capability narrative is:

```text
Acquire → Persist → Validate → Transform → Stream → Visualize → Observe → Learn → Explain → Backtest
```

## Release 1.9 — Real-Time Financial Data Visualization

**Status:** NEXT — milestone placeholder only; no definition, work packages, or
implementation exist.

The future release is intended to use deterministic simulated/live-mock provider
ticker behavior and the existing pipeline with a Streamlit presentation adapter.
It may expose evolving financial charts, existing feature output such as
`simple-return-lag-1-v1`, and dataset snapshot/data-quality states. It excludes
ML training and a broad observability platform.

## Release 1.10 — OpenTelemetry & Pipeline Observability

**Status:** PLANNED — milestone placeholder only.

The future release is intended to introduce governed OpenTelemetry for pipeline
and stage timing, throughput, provider behavior, persistence latency, failures,
and appropriate Python-boundary telemetry, including a Streamlit System Health
view. An explicit OpenTelemetry selection record is required before
implementation. ML training is excluded.

## Release 2.0 — Lightweight Machine Learning Evaluation

**Status:** PLANNED — milestone placeholder only.

The future release is intended to test one narrow deterministic ML hypothesis,
with Logistic Regression through governed scikit-learn as the preferred initial
candidate unless a later definition finds a blocker. It requires temporal—not
random—evaluation, baseline comparison, reproducible experiment identity and
metrics, and experiment visualization. It is not a broad reusable ML platform
or strategy backtesting.

## Release 2.1 — Machine Learning

**Status:** RESEQUENCED — existing milestone identity and broader scope
preserved; no work packages are defined here.

## Release 2.2 — Explainable AI

**Status:** RESEQUENCED — existing milestone identity and broader scope
preserved; no work packages are defined here.

## Release 2.3 — Backtesting

**Status:** PLANNED — milestone placeholder only.

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
and contributor experience—not feature count alone.

---

# Living Roadmap

This roadmap is evolutionary. Changes must preserve the project’s long-term
vision while remaining consistent with the Project Constitution, Engineering
Handbook, and release-specific authorities.
