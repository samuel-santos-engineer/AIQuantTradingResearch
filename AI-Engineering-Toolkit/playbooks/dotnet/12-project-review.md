
# Project Review

## Purpose

The Project Review playbook defines the engineering principles and practices for evaluating the architecture, implementation quality, reliability, security, performance, testability, observability, documentation, and maintainability of .NET solutions developed as part of the AI Engineering Toolkit.

Its purpose is to establish a consistent, evidence-based assessment process that verifies alignment with the .NET Engineering Playbooks, identifies engineering risks, exposes technical debt, and produces actionable improvement plans.

A project review evaluates the software system as a complete engineering product rather than reviewing individual code changes in isolation.

---

# Objectives

The Project Review playbook aims to:

- Standardize .NET project assessments.
- Verify alignment with engineering standards.
- Evaluate architectural integrity.
- Identify technical risks.
- Detect maintainability concerns.
- Assess software quality.
- Evaluate test effectiveness.
- Verify security practices.
- Assess performance readiness.
- Review operational observability.
- Validate documentation quality.
- Produce actionable improvement plans.
- Support continuous engineering improvement.

---

# Scope

This playbook applies to .NET solutions within the AI Engineering Toolkit, including:

- Web applications.
- Web APIs.
- Background services.
- Worker services.
- Shared libraries.
- Modular monoliths.
- Microservices.
- Distributed systems.
- Data processing platforms.
- AI-enabled applications.
- Cloud-native solutions.

The depth of the review should reflect the complexity, business criticality, security exposure, operational risk, and maturity of the system.

---

# Design Principles

Project reviews should be:

- Objective.
- Evidence-based.
- Repeatable.
- Risk-oriented.
- Architecture-aware.
- Collaborative.
- Traceable.
- Actionable.
- Proportional.
- Improvement-focused.

The objective is not to achieve theoretical perfection.

The objective is to understand the current engineering state of the solution and determine where improvement provides meaningful value.

---

# Engineering Philosophy

A successful build does not necessarily represent a well-engineered system.

A project may compile and function correctly while still containing:

- Architectural erosion.
- Excessive coupling.
- Weak domain boundaries.
- Dependency risk.
- Poor failure handling.
- Insufficient testing.
- Security weaknesses.
- Performance bottlenecks.
- Limited observability.
- Outdated documentation.
- Accumulated technical debt.

Project reviews therefore evaluate engineering quality beyond functional correctness.

---

# Relationship to Code Review

Project review and code review serve different purposes.

```text
Code Review
    ↓
Evaluates a specific change

Project Review
    ↓
Evaluates the engineering health of the system
```
