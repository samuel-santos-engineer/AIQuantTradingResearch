![AIQuantTradingResearch Logo](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/blob/main/imgs/AIQuantTradingResearchBanner-01.jpg?raw=true)
# AIQuantTradingResearch

> **Engineering AI-powered quantitative research with transparency, discipline, and production-grade software architecture.**

AIQuantTradingResearch is an open-source engineering platform that demonstrates how to design, build, and evolve an AI-assisted quantitative trading research system using modern software engineering practices.

Rather than focusing solely on trading algorithms or machine learning models, this project places equal emphasis on architecture, engineering governance, documentation, observability, testing, and long-term maintainability.

The goal is to demonstrate that great software is the result of disciplined engineering—not isolated technical solutions.

---

# Why This Project Exists

Many quantitative trading repositories focus on prediction accuracy.

Many AI repositories focus on model experimentation.

Few demonstrate **how an enterprise-grade engineering team would build and evolve such a platform from the ground up.**

AIQuantTradingResearch exists to bridge that gap.

This repository documents not only the software being built, but also the engineering decisions, architectural trade-offs, governance, and development practices behind it.

Every significant decision is transparent.

Every architectural choice is documented.

Every milestone strengthens both the platform and the engineering practices that support it.

---

# Project Objectives

The platform is being designed to:

* Build a production-grade quantitative research platform.
* Explore AI-assisted market analysis and prediction.
* Demonstrate modern software architecture.
* Showcase cloud-native engineering practices.
* Apply DevOps and Site Reliability Engineering principles.
* Promote documentation-driven development.
* Preserve engineering decisions through transparent documentation.
* Serve as a long-term reference implementation for professional software engineering.

---

# What Makes This Project Different?

| Traditional AI Projects    | AIQuantTradingResearch               |
| -------------------------- | ------------------------------------ |
| Prototype-oriented         | Engineering-oriented                 |
| Code-first                 | Architecture-first                   |
| Documentation added later  | Documentation-driven development     |
| Hidden design decisions    | Transparent engineering decision log |
| Focus on implementation    | Focus on engineering excellence      |
| Technology-driven          | Principle-driven                     |
| Short-term experimentation | Long-term maintainability            |

---

# Engineering Principles

The project is guided by a small set of enduring engineering principles:

* Architecture before implementation
* Documentation is part of the deliverable
* Incremental delivery
* Engineering excellence through transparency
* Automation by default
* Simplicity over unnecessary complexity
* Security and observability by design
* Continuous learning and continuous improvement

---

# Engineering Capability Journey

Each release expands both the platform and the engineering capabilities it demonstrates.

| Release | Engineering Capability                  |
| ------- | --------------------------------------- |
| 0.1–0.6 | Architecture and engineering foundation |
| 0.7     | AI Engineering Toolkit                  |
| 0.8     | Executable .NET solution skeleton       |
| 0.9+    | Later platform capabilities             |

---

# Engineering Handbook

The repository is supported by a structured Engineering Handbook that documents the project's architecture, governance, and engineering practices.

## Foundation

* Project Constitution
* Product Vision
* Engineering Guide
* Engineering Playbook
* Engineering Decision Log

## Architecture

* Architecture Overview
* Architecture Decision Records (ADRs)

## Engineering Practices

* Coding Standards
* Contributing Guide
* Code of Conduct
* Changelog
* Roadmap
* Engineer Growth Path

---

# Technology Direction

The platform is currently evolving around the following technologies:

* ASP.NET Core (.NET)
* Python
* PostgreSQL + TimescaleDB
* Docker
* GitHub Actions
* OpenTelemetry
* Ollama
* Twelve Data historical market-data API

Technology choices are continuously evaluated and documented through the Engineering Decision Log.

---

# Current Status

**Current Release:** 1.1 – Market Data Persistence Foundation

The repository contains the first provider-backed historical market-data vertical slice:

```text
AIQuantTradingResearch.slnx
├── /src/
│   ├── AIQuantTradingResearch.Domain
│   ├── AIQuantTradingResearch.Application
│   ├── AIQuantTradingResearch.Infrastructure
│   └── AIQuantTradingResearch.Worker
└── /tests/
    ├── AIQuantTradingResearch.Domain.Tests
    ├── AIQuantTradingResearch.Application.Tests
    ├── AIQuantTradingResearch.Infrastructure.Tests
    └── AIQuantTradingResearch.Architecture.Tests
```

The Worker is a one-shot composition and execution root. `AddApplication` registers the research and persistence use cases, while `AddInfrastructure` composes the Application-owned observation-source and persistence contracts with the Infrastructure-owned Twelve Data and SQLite implementations. The API key is supplied externally through `TwelveData:ApiKey`; the persistence path is supplied through `Persistence:DatabasePath`.

Release 1.1 implements durable historical observations through one provider and local SQLite persistence behind provider-independent contracts. Streaming/live feeds, provider failover, trading, plugins, AI/ML, APIs, scheduling, retry/resilience policy, and production deployment remain planned for later releases.

The production dependency graph is:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

# Local Verification

From the repository root:

```powershell
./eng/restore.ps1
./eng/format.ps1
./eng/build.ps1
./eng/test.ps1
./eng/verify.ps1
./eng/clean.ps1
```

`format.ps1` verifies formatting without changing files. `verify.ps1` delegates restore, format verification, build, and test. The cross-platform build counterpart is `eng/build.sh`.

The architecture test project currently executes 13 dependency, ownership, visibility, provider-confinement, and acyclicity checks.

---

# Repository Philosophy

AIQuantTradingResearch is built on the belief that engineering quality is as important as functional correctness.

The repository values:

* Clear architecture
* Thoughtful engineering decisions
* Transparent trade-offs
* High-quality documentation
* Sustainable software design
* Incremental evolution

The software should not only solve problems—it should demonstrate how professional engineering teams solve them.

---

# Contributing

Contributions are welcome.

Whether you are improving documentation, reviewing architecture, fixing defects, or implementing new features, every contribution helps strengthen both the platform and the engineering practices behind it.

Please review the Engineering Handbook before contributing.

---

# Roadmap

The project evolves through incremental engineering releases.

Each release introduces new technical capabilities while reinforcing architecture, testing, documentation, and operational excellence.

See the Roadmap for detailed release planning and long-term objectives.

---

# License

This project is released under the MIT License.

---

> **AIQuantTradingResearch is more than a quantitative trading platform. It is a living demonstration of how modern software engineering, artificial intelligence, and transparent technical leadership can come together to build software that is designed to evolve.**
