
# PROMPT_TEMPLATE.md

# Prompt Template

## Purpose

The Prompt Template defines the canonical structure for all prompts created within the AI Engineering Toolkit.

It establishes a consistent engineering standard that promotes clarity, maintainability, traceability, reproducibility, and high-quality AI interactions.

Every prompt should follow this template unless a documented architectural decision justifies a deviation.

---

# Objectives

The Prompt Template aims to:

* Standardize prompt construction.
* Improve prompt quality.
* Increase prompt reusability.
* Reduce ambiguity.
* Simplify prompt reviews.
* Support prompt lifecycle management.
* Enable prompt automation.
* Facilitate AI-assisted engineering.

---

# Design Principles

Every prompt should be:

* Clear
* Concise
* Deterministic
* Modular
* Reusable
* Technology-aware
* Context-driven
* Testable
* Versioned
* Maintainable

Prompts should communicate engineering intent rather than simply instruct an AI model.

---

# Canonical Prompt Structure

A standard prompt should contain the following sections.

```text
Metadata

Purpose

Objective

Context

Prerequisites

Inputs

Instructions

Constraints

Expected Outputs

Acceptance Criteria

Failure Handling

Validation

References

Version History
```

Individual prompt types may extend this structure while preserving the overall organization.

---

# Prompt Sections

## Metadata

Provides engineering information about the prompt.

Typical metadata includes:

* Prompt ID
* Name
* Category
* Version
* Author
* Status
* Prompt Type
* Estimated Execution Time

Metadata enables lifecycle management and traceability.

---

## Purpose

Describes why the prompt exists.

The purpose should explain the engineering problem the prompt solves rather than the specific implementation approach.

---

## Objective

Defines the expected engineering outcome.

Objectives should be measurable, specific, and focused on the desired result.

---

## Context

Provides the background information required for successful execution.

Context may include:

* Repository information
* Architecture
* Technology stack
* Engineering standards
* Previous work
* Project goals

Well-defined context reduces ambiguity and improves output quality.

---

## Prerequisites

Lists the conditions that must be satisfied before executing the prompt.

Examples include:

* Existing repository assets
* Required documentation
* Prior playbooks
* Software dependencies
* Engineering approvals

---

## Inputs

Defines the information required from the user.

Typical inputs include:

* Repository name
* Technology stack
* Configuration values
* Architectural decisions
* Engineering preferences

Every required input should be explicitly documented.

---

## Instructions

Contains the engineering tasks the AI should perform.

Instructions should:

* Be sequential.
* Be explicit.
* Avoid ambiguity.
* Focus on engineering intent.
* Minimize assumptions.

Each instruction should contribute directly to the prompt objective.

---

## Constraints

Defines the engineering rules that limit execution.

Examples include:

* Follow repository standards.
* Preserve backward compatibility.
* Avoid modifying unrelated artifacts.
* Respect architectural boundaries.
* Produce deterministic results.

Constraints help ensure consistent outcomes.

---

## Expected Outputs

Describes the artifacts that should be produced.

Outputs may include:

* Documents
* Source code
* Configuration
* Diagrams
* Scripts
* Reports
* Validation results

Expected outputs should be clearly identified.

---

## Acceptance Criteria

Defines the conditions required for successful completion.

Acceptance criteria should be:

* Objective
* Verifiable
* Measurable

These criteria establish the prompt's Definition of Done.

---

## Failure Handling

Defines expected behavior when execution cannot continue.

Examples include:

* Missing prerequisites.
* Invalid inputs.
* Repository conflicts.
* Unsupported technology.
* Incomplete information.

The prompt should fail gracefully while providing actionable guidance.

---

## Validation

Defines how the generated outputs should be verified.

Validation may include:

* Structural validation.
* Standards compliance.
* Repository consistency.
* Naming verification.
* Build verification.
* Documentation verification.

Validation ensures engineering quality.

---

## References

Lists related repository assets.

Examples include:

* Engineering Standards
* Architecture Documents
* Playbooks
* Templates
* Naming Conventions
* Quality Guidelines

References reduce duplication while improving maintainability.

---

## Version History

Tracks prompt evolution.

Typical information includes:

* Version
* Date
* Author
* Summary of changes

Version history supports continuous improvement.

---

# Prompt Lifecycle

Every prompt should follow a managed lifecycle.

```text
Draft

↓

Review

↓

Validate

↓

Approved

↓

Published

↓

Maintain

↓

Retire
```

Each lifecycle stage should be documented.

---

# Prompt Quality Standards

Every prompt should satisfy the following characteristics:

* Clear objective
* Complete context
* Explicit instructions
* Defined constraints
* Deterministic behavior
* Actionable outputs
* Validation guidance
* Maintainable structure

Prompt quality should be evaluated independently of the generated output.

---

# Engineering Guidelines

Prompt authors should:

* Prefer explicit instructions over assumptions.
* Minimize unnecessary complexity.
* Keep prompts modular.
* Separate concerns.
* Avoid technology-specific implementation unless required.
* Reference standards rather than duplicating them.
* Write prompts that remain understandable months after their creation.

Prompts are long-lived engineering assets and should be maintained accordingly.

---

# Common Pitfalls

Avoid:

* Mixing multiple responsibilities.
* Omitting context.
* Providing vague objectives.
* Embedding hidden assumptions.
* Producing non-deterministic instructions.
* Ignoring validation.
* Duplicating repository standards.

A high-quality prompt should be predictable, maintainable, and reusable.

---

# Extensibility

Specialized prompt templates may extend this standard.

Examples include:

* System Prompt Template
* Task Prompt Template
* Review Prompt Template
* Validation Prompt Template
* Generator Prompt Template
* Refactoring Prompt Template
* Documentation Prompt Template

Extensions should preserve the canonical structure while introducing additional sections only when justified.

---

# Success Criteria

The Prompt Template is successful when it enables prompts that:

* Produce consistent engineering outcomes.
* Require minimal clarification.
* Are easy to review and maintain.
* Integrate naturally with Engineering Playbooks.
* Support future automation and orchestration.

Success is measured by repeatability, quality, and long-term maintainability.

---

# Conclusion

The Prompt Template establishes the engineering contract for prompt development within the AI Engineering Toolkit.

By defining a consistent structure for metadata, context, instructions, validation, and lifecycle management, it transforms prompts from ad hoc AI interactions into governed engineering artifacts. This foundation promotes clarity, consistency, traceability, and reuse while supporting the toolkit's long-term vision of an AI-assisted engineering methodology.
