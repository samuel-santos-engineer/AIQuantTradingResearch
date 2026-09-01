![AIQuantTradingResearch Logo](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/blob/main/imgs/AIQuantTradingResearchBanner-01.jpg?raw=true)

# AIQuantTradingResearch

> A production-oriented quantitative research and platform-engineering project built around deterministic market-data pipelines, durable evidence, governed .NET/Python interoperability, observability, and reproducible public-reference deployment.

[![Release progression](https://img.shields.io/badge/Release%201.12-WP03%20accepted%20%7C%20WP04%20next-blue)](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/63)
[![.NET](https://img.shields.io/badge/.NET-C%23-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Python](https://img.shields.io/badge/Python-3.13-3776AB?logo=python)](docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md)
[![Docker](https://img.shields.io/badge/Docker-containerized-2496ED?logo=docker)](Dockerfile)
[![License: MIT](https://img.shields.io/badge/license-MIT-yellow.svg)](LICENSE)

AIQuantTradingResearch demonstrates how quantitative research capability can evolve as a maintainable software platform. Architecture, executable quality gates, incremental delivery, transparent technical decisions, and disciplined AI-assisted engineering are treated as part of the product.

**Current closed milestone:** **Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification — FEASIBLE**

Initiative-1.11 is a completed non-release feasibility initiative. **Initiative-1.11 ≠ Product Release 1.11.**

**Current accepted milestone:** **[Phase 4 - Release 1.12: Public Reference Deployment Implementation & Stabilization — IN PROGRESS](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/63)**

WP01, WP02, and WP03 are Closed/Done. WP04 is the next Open/Todo work package. Release 1.12 remains in progress with five open and three closed work packages.

[What Works Today](#what-works-today) · [Architecture](#architecture) · [Run & Verify](#run--verify) · [Implemented Technology](#implemented-technology) · [Roadmap](#engineering-capability-journey) · [Engineering Handbook](#engineering-handbook)

---

## What Works Today

- **Historical market data:** bounded Twelve Data acquisition through an Infrastructure-owned provider adapter.
- **Durable persistence:** SQLite-backed accepted history with deterministic retrieval, atomic writes, fidelity guarantees, idempotency, and explicit conflicts.
- **Immutable research evidence:** deterministic dataset definitions, snapshots, versions, catalogs, identities, provenance, and lineage.
- **Research pipeline:** a fixed Application-owned pipeline from persisted observations through immutable dataset evidence.
- **Features and experiments:** deterministic feature generation, experiment evidence, durable acceptance, exact retrieval, and bounded discovery.
- **Python interoperability:** Python 3.13 and a governed one-shot JSON-over-stdio boundary owned by the .NET adapter.
- **Governed visualization:** a distinct canonical JSON visualization handoff consumed by read-only Python/Streamlit presentation code.
- **Truthful provenance:** deterministic, replay, and simulated flows are disclosed as such and are not represented as live trading.
- **Observability:** OpenTelemetry-based pipeline and cross-language boundary telemetry with truthful Streamlit System Health states.
- **Container runtime:** one Docker image composes the required .NET Worker and Streamlit processes with fail-closed configuration, required-child supervision, graceful shutdown, and non-root execution.
- **Public image distribution:** Release 1.12 images can be published to public/free GHCR with immutable digest verification.
- **Azure reference deployment:** parameterized automation provisions Azure App Service Linux F1 in West Central US using public GHCR, HTTPS, persistent `/home`, and an SQLite-compatible deployment boundary.

Release 1.12 WP03 establishes publication and deployment automation only. Persistent SQLite initialization/update/recovery, runtime secret automation, deployed public System Health, stability qualification, and final release acceptance remain WP04–WP08 work.

> Azure hosts a bounded public reference/demo deployment. F1 has shared capacity, cold starts, throttling, a 60 CPU-minute daily allowance, 1 GB storage, and no SLA. This is not production hosting or production trading architecture.

---

## Architecture

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

| Layer | Responsibility |
|---|---|
| **Domain** | Quantitative concepts and invariants without provider, storage, deployment, or presentation dependencies. |
| **Application** | Use cases, contracts, identities, orchestration, and semantic evidence. |
| **Infrastructure** | Twelve Data, SQLite, telemetry adapters, and the governed local Python process boundary. |
| **Worker** | Composition and bounded execution of the canonical .NET pipeline. |
| **Python/Streamlit** | Read-only consumption and presentation of governed handoff evidence. |

.NET remains the canonical pipeline owner. Streamlit does not directly own provider access, SQLite access, or Worker supervision. The Release 1.8 JSON-over-stdio interoperability boundary and Release 1.9 visualization handoff remain distinct governed contracts. Azure is deployment-only and introduces no Domain or Application dependency.

---

## Run & Verify

For provider-backed local execution, start with [Local Platform Execution](docs/guides/LOCAL_PLATFORM_EXECUTION.md). For deeper evidence and troubleshooting, use the [Platform Execution & Verification Guides](docs/guides/README.md).

```powershell
./eng/restore.ps1
./eng/format.ps1
./eng/build.ps1
./eng/test.ps1
./eng/verify.ps1
```

The container is defined by [Dockerfile](Dockerfile). Release 1.12 deployment automation is under [`eng/azure-cli/r1.12-deployment/`](eng/azure-cli/r1.12-deployment/). Credentials, API keys, auth profiles, and machine-local paths must remain outside source control.

The repository validates Domain, Application, Infrastructure, Architecture, and Python behavior through automated suites and canonical engineering scripts. Exact test counts are intentionally omitted because they are volatile; current validation output is authoritative.

---

## Engineering Evidence

The platform makes its engineering state inspectable through immutable accepted evidence, deterministic identities and provenance, explicit idempotent/conflict/failure semantics, executable architecture rules, governed process boundaries, OpenTelemetry telemetry, secret-safe deployment automation, and exact Git/lifecycle governance.

AI assists implementation and analysis inside explicit contracts, mutation boundaries, validation gates, and reviewable pull requests. It does not replace architecture, testing, or acceptance evidence.

---

## Implemented Technology

- C# and .NET
- Python 3.13 interoperability
- Streamlit
- Twelve Data historical acquisition
- SQLite durable evidence
- JSON-over-stdio interoperability
- canonical JSON visualization handoff
- OpenTelemetry-based observability and System Health
- Dockerized runtime composition
- public GHCR container publication
- Azure App Service Linux F1 reference-deployment automation
- PowerShell engineering and deployment automation
- automated Domain, Application, Infrastructure, Architecture, and Python validation
- GitHub issues, milestones, Project workflow, and documented AI-assisted governance

Implemented deployment capability does not include Azure SQL, Azure Files, Container Apps, ACR, paid monitoring, Kubernetes, ML, backtesting, live trading, or a production SLA.

---

## Planned Technology & Platform Direction

Release 1.12 remaining stabilization covers:

- persistent `/home` SQLite initialization, data update, integrity, and recovery;
- Twelve Data runtime configuration, secret handling, and bounded automation;
- deployed public Streamlit/System Health behavior;
- restart, recycle, redeployment, recovery, cost, and no-bypass validation;
- operational runbooks and final release acceptance.

Later governed direction includes:

- an independent Azure SQL Database Free Offer investigation and architecture decision—it is not adopted by Release 1.12;
- Phase 5 Release 2.0 lightweight machine-learning evaluation;
- Release 2.1 Machine Learning;
- Release 2.2 Explainable AI;
- Release 2.3 Backtesting;
- later resilience, scheduling, and analytics evolution where justified.

Python interoperability, Docker deployment, and OpenTelemetry observability are implemented foundations rather than future-only claims.

---

## Engineering Capability Journey

| Release / initiative | Accepted engineering capability |
|---|---|
| **0.1–1.0** | Architecture, governance, executable solution, and provider-backed acquisition foundations |
| **1.1** | Durable market-data persistence and deterministic retrieval |
| **1.2** | Immutable datasets, snapshots, versions, and catalog evidence |
| **1.3** | Fixed deterministic research pipeline |
| **1.4** | Deterministic feature generation |
| **1.5** | Deterministic research experiment evidence |
| **1.6** | Durable experiment evidence |
| **1.7** | Bounded durable experiment evidence discovery |
| **1.8** | Python interoperability and governed JSON-over-stdio boundary |
| **1.9** | Governed real-time-style visualization and canonical JSON handoff |
| **1.10** | OpenTelemetry pipeline/boundary observability and truthful System Health |
| **Initiative-1.11** | Azure App Service F1 public-reference feasibility qualification — **FEASIBLE** |
| **1.12** | Public Reference Deployment Implementation & Stabilization — **IN PROGRESS; WP01–WP03 accepted, WP04 next** |
| **2.0** | Lightweight Machine Learning Evaluation — planned |
| **2.1** | Machine Learning — planned |
| **2.2** | Explainable AI — planned |
| **2.3** | Backtesting — planned |

Product sequence: `1.10 → 1.12 → 2.0 → 2.1 → 2.2 → 2.3`.

**Initiative-1.11 ≠ Product Release 1.11.**

- [Engineering Roadmap Project](https://github.com/users/samuel-santos-engineer/projects/2)
- [Repository Issues](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues)
- [Repository Milestones](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestones)

---

## Why This Project Exists

Many quantitative repositories emphasize strategies, notebooks, or prediction accuracy. AIQuantTradingResearch explores a complementary question:

> How can a long-lived quantitative research platform evolve data sources, evidence models, analytics, and operations without sacrificing architecture, reproducibility, or maintainability?

The goal is not production trading. It is a transparent reference implementation of disciplined platform engineering for quantitative research.

---

## Engineering Handbook

The [`docs/`](docs/) tree contains the project constitution, product vision, engineering guide, playbook, decision log, architecture records, testing and observability guidance, contribution workflow, roadmap, and operational guides.

Contributions are welcome. Before contributing, review [CONTRIBUTING.md](docs/project/CONTRIBUTING.md) and the relevant architecture and release governance.

---

## Disclaimer

AIQuantTradingResearch is an engineering and quantitative research project. It is **not financial advice**, does not guarantee investment performance, and must not be interpreted as a recommendation to buy, sell, or trade any financial instrument.

## License

This project is released under the [MIT License](LICENSE).
