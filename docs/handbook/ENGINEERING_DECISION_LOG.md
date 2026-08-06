# AIQuantTradingResearch Engineering Decision Log

**Status:** Active
**Version:** 1.0
**Last Updated:** 2026-08-06
**Maintainers:** AIQuantTradingResearch Team

---

# Purpose

Engineering is a continuous process of making informed decisions under technical, operational, financial, and organizational constraints.

This document captures the major engineering decisions made throughout the lifetime of AIQuantTradingResearch, together with the reasoning behind each choice.

Its purpose is not to prove that a decision is universally correct, but to preserve the context in which it was made.

Future contributors should understand not only *what* was chosen, but *why* it was chosen.

When assumptions change, decisions should be revisited and documented.

---

# Decision-Making Principles

Engineering decisions should be guided by the following priorities:

1. Simplicity over unnecessary complexity.
2. Maintainability over short-term optimization.
3. Open standards over proprietary solutions.
4. Automation over manual processes.
5. Readability over clever implementations.
6. Production readiness over prototypes.
7. Long-term sustainability over trend adoption.
8. Cost-effectiveness whenever technically reasonable.

When these principles conflict, the trade-offs should be documented explicitly.

---

# Decision Matrix

| Area             | Selected                 | Alternatives Considered  | Decision Rationale                                                                                                                                                                                                  | Revisit When                                                                                  | Status  |
| ---------------- | ------------------------ | ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------- | ------- |
| Backend          | ASP.NET Core (.NET)      | FastAPI, Spring Boot     | Aligns with primary expertise, enterprise ecosystem, strong performance, and long-term maintainability.                                                                                                             | Cross-language services become a primary architectural requirement.                           | Active  |
| Unit Tests       | xUnit                    | NUnit                    | Current Microsoft Standard that uses xUnit to test the .NET runtime and ASP.NET Core source code.<br />Thread-safe parallel execution and strict instance isolation keep your suite fast and reliable from day one. | If it is required an extensive native constraint-based assertion syntax (Assert.That(...))    | Active  |
| Database         | PostgreSQL + TimescaleDB | SQL Server, MongoDB      | Mature relational database with native time-series capabilities and an excellent open-source ecosystem.                                                                                                             | Data volume or storage patterns exceed current architecture.                                  | Planned |
| Machine Learning | Python                   | ML.NET                   | Rich ecosystem, extensive community support, and compatibility with modern AI frameworks.                                                                                                                           | ML.NET evolves to satisfy project requirements without sacrificing flexibility.               | Planned |
| Explainable AI   | Ollama                   | Azure OpenAI, OpenAI API | Enables local execution, zero API costs during development, privacy, and reproducibility.                                                                                                                           | Production requirements justify managed LLM services.                                         | Planned |
| Messaging        | RabbitMQ                 | Kafka, Azure Service Bus | Simpler operational model appropriate for the expected workload and project scope.                                                                                                                                  | Event throughput or streaming requirements significantly increase.                            | Future  |
| Observability    | OpenTelemetry            | Vendor-specific SDKs     | Open standard that promotes vendor neutrality and broad ecosystem compatibility.                                                                                                                                    | Managed observability requirements introduce capabilities unavailable through open standards. | Future  |
| CI/CD            | GitHub Actions           | Azure DevOps, Jenkins    | Native GitHub integration, low maintenance, and cost-effective automation.                                                                                                                                          | Enterprise deployment constraints require a different platform.                               | Planned |
| Frontend         | Blazor                   | React, Angular           | Strong integration with the .NET ecosystem and rapid development for internal dashboards.                                                                                                                           | Frontend complexity or user experience requirements significantly evolve.                     | Future  |

---

# Decision Template

Every significant engineering decision should answer the following questions.

## Context

What problem or opportunity led to this decision?

## Decision

What was selected?

## Alternatives

Which alternatives were evaluated?

## Rationale

Why was this option chosen?

## Consequences

What benefits and trade-offs does this decision introduce?

## Review Criteria

Under which circumstances should this decision be reconsidered?

---

# Decision Categories

Engineering decisions typically fall into one or more of the following categories:

- Architecture
- Infrastructure
- Security
- Data Platform
- Artificial Intelligence
- Machine Learning
- DevOps
- Cloud
- Observability
- Testing
- Performance
- Development Experience
- Cost Optimization

---

# Relationship with ADRs

This document provides a high-level summary of engineering decisions.

When a decision has significant architectural impact, a dedicated Architecture Decision Record (ADR) should also be created.

The Engineering Decision Log answers:

> **What did we decide?**

The ADR answers:

> **Why did we decide it?**

---

# Review Process

Engineering decisions are not permanent.

They should be reviewed whenever:

- Project goals change.
- New technical constraints emerge.
- Better technologies mature.
- Operational experience reveals shortcomings.
- Costs become unacceptable.
- Performance expectations evolve.

Revisiting a decision is considered a sign of engineering maturity—not inconsistency.

---

# Decision Lifecycle

Every engineering decision follows the same lifecycle:

1. Identify the problem.
2. Gather requirements and constraints.
3. Evaluate alternatives.
4. Select the preferred solution.
5. Document the rationale.
6. Implement the decision.
7. Monitor outcomes.
8. Reassess when assumptions change.

---

# Living Document

This Engineering Decision Log evolves alongside the project.

Entries should remain concise, factual, and traceable to milestone deliverables or Architecture Decision Records.

Historical decisions should be preserved to provide context for future contributors and maintainers.

The objective is not to build a record of technologies, but a history of engineering judgment.
