
# PROMPT_METADATA.md

# Prompt Metadata

## Purpose

The Prompt Metadata specification defines the canonical metadata model for engineering prompts within the AI Engineering Toolkit.

Metadata provides the descriptive information required to identify, classify, govern, version, discover, review, validate, orchestrate, and maintain prompts throughout their lifecycle.

Every engineering prompt should include standardized metadata to ensure consistency and long-term maintainability.

---

# Objectives

The Prompt Metadata specification aims to:

* Standardize prompt identification.
* Improve prompt discoverability.
* Enable prompt governance.
* Support lifecycle management.
* Facilitate orchestration.
* Improve traceability.
* Support automation.
* Enable enterprise-scale prompt management.

---

# Scope

This specification applies to every prompt within the AI Engineering Toolkit, including prompts for:

* Bootstrap
* Documentation
* Architecture
* Testing
* GitHub
* DevOps
* Cloud
* Security
* AI Engineering
* Validation
* Review
* Future prompt collections

Metadata requirements are independent of prompt implementation.

---

# Design Principles

Prompt metadata should be:

* Explicit
* Consistent
* Complete
* Human-readable
* Machine-readable
* Versioned
* Extensible
* Stable

Metadata should describe the prompt without duplicating its implementation.

---

# Metadata Categories

Prompt metadata is organized into the following categories:

* Identification
* Classification
* Ownership
* Lifecycle
* Execution
* Dependencies
* Quality
* Governance

Each category serves a distinct engineering purpose.

---

# Identification Metadata

Every prompt should define:

| Property   | Description                                    |
| ---------- | ---------------------------------------------- |
| Prompt ID  | Globally unique prompt identifier.             |
| Name       | Human-readable prompt name.                    |
| Collection | Prompt collection to which the prompt belongs. |
| Category   | Functional classification.                     |
| Version    | Current prompt version.                        |
| Status     | Lifecycle status.                              |

Identification metadata uniquely distinguishes every prompt.

---

# Classification Metadata

Classification provides organizational context.

Typical properties include:

* Domain
* Technology
* Repository Type
* Engineering Discipline
* Prompt Type
* Execution Mode

Classification supports prompt discovery and reuse.

---

# Ownership Metadata

Ownership identifies responsibility.

Typical properties include:

* Author
* Maintainer
* Reviewer
* Validator
* Organization

Ownership enables governance and accountability.

---

# Lifecycle Metadata

Lifecycle metadata describes the current engineering maturity.

Typical values include:

* Draft
* Review
* Validation
* Approved
* Published
* Deprecated
* Retired

Lifecycle metadata supports controlled evolution.

---

# Execution Metadata

Execution metadata describes operational characteristics.

Typical properties include:

* Estimated Duration
* Required Inputs
* Expected Outputs
* Execution Mode
* Idempotent
* Resume Supported
* Validation Required

Execution metadata supports orchestration.

---

# Dependency Metadata

Dependencies identify relationships with other engineering assets.

Examples include:

* Playbooks
* Standards
* Templates
* Architecture Documents
* Repository Assets
* Related Prompts

Dependencies improve maintainability and change impact analysis.

---

# Quality Metadata

Quality metadata provides engineering confidence.

Typical properties include:

* Review Status
* Validation Status
* Quality Score
* Test Coverage
* Compliance Status

Quality metadata supports engineering governance.

---

# Governance Metadata

Governance metadata documents organizational compliance.

Examples include:

* Approval Date
* Approved By
* Review Cycle
* Policy Version
* Compliance Level

Governance metadata enables enterprise adoption.

---

# Canonical Metadata Structure

A typical metadata block may include:

```yaml
PromptId:
Name:
Collection:
Category:
Version:
Status:
Author:
Maintainer:
Reviewer:
Validator:
Technology:
ExecutionMode:
EstimatedDuration:
Dependencies:
ReviewStatus:
ValidationStatus:
LastUpdated:
```

The exact representation may vary provided the required information is preserved.

---

# Metadata Lifecycle

Metadata evolves alongside the prompt.

```text
Created

↓

Reviewed

↓

Validated

↓

Published

↓

Updated

↓

Deprecated

↓

Archived
```

Metadata should always reflect the current state of the prompt.

---

# Metadata Validation

Metadata should be validated for:

* Completeness
* Consistency
* Correct formatting
* Version accuracy
* Dependency integrity
* Lifecycle validity

Invalid metadata reduces prompt reliability.

---

# Metadata Usage

Metadata supports numerous engineering activities, including:

* Prompt discovery.
* Repository navigation.
* Collection management.
* Prompt orchestration.
* Lifecycle reporting.
* Change management.
* Automated validation.
* Compliance auditing.

Metadata should be treated as operational information rather than documentation.

---

# Extensibility

Prompt collections may extend the metadata model with domain-specific properties.

Examples include:

* Cloud Provider
* Programming Language
* Target Framework
* Infrastructure Platform
* AI Model
* Repository Profile

Extensions should preserve compatibility with the canonical metadata model.

---

# Engineering Guidelines

Metadata authors should:

* Use consistent terminology.
* Avoid unnecessary duplication.
* Keep metadata current.
* Prefer explicit values.
* Record meaningful ownership.
* Update metadata whenever the prompt changes.

Metadata should evolve together with the prompt.

---

# Common Pitfalls

Avoid:

* Missing identifiers.
* Inconsistent versioning.
* Obsolete ownership information.
* Undocumented dependencies.
* Duplicate metadata.
* Incorrect lifecycle status.

Metadata quality directly affects prompt governance.

---

# Success Criteria

The Prompt Metadata specification is successful when:

* Every prompt can be uniquely identified.
* Prompt collections are easy to navigate.
* Lifecycle status is always clear.
* Dependencies are traceable.
* Governance is simplified.
* Automation can consume metadata reliably.

Success is measured through discoverability, consistency, and operational value.

---

# Dependencies

This specification depends on:

* Prompt Architecture
* Prompt Lifecycle
* Prompt Template
* Naming Conventions
* Quality Guidelines
* Review Template
* Validation Template

These assets collectively define the governance model for engineering prompts.

---

# Future Evolution

The Prompt Metadata model is designed to support future capabilities, including:

* Metadata-driven orchestration.
* Prompt catalogs.
* Prompt search indexes.
* Dependency visualization.
* AI-assisted prompt discovery.
* Enterprise governance dashboards.
* Automated compliance reporting.

Future enhancements should preserve backward compatibility.

---

# Conclusion

The Prompt Metadata specification establishes the canonical model for describing engineering prompts within the AI Engineering Toolkit.

By standardizing identification, classification, ownership, lifecycle, execution, dependencies, quality, and governance metadata, it transforms prompts into fully governed engineering assets. This metadata foundation enables discoverability, traceability, automation, and enterprise-scale management while supporting the toolkit's long-term vision of structured AI-assisted software engineering.
