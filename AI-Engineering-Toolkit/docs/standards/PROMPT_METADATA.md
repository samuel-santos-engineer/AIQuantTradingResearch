
# PROMPT_METADATA.md

# Prompt Metadata Standard

## Purpose

This document defines the metadata standard for all Engineering Playbooks within the AI Engineering Toolkit.

Metadata provides a consistent mechanism for identifying, classifying, versioning, validating, and governing Engineering Playbooks. By standardizing metadata, the toolkit enables automation, discoverability, traceability, and long-term maintainability.

Metadata is mandatory for every Engineering Playbook.

---

# Design Principles

The metadata model is designed around the following principles:

* Simplicity
* Human readability
* Machine readability
* Versionability
* Extensibility
* Technology independence
* Consistency
* Automation friendly

---

# Metadata Format

Every Engineering Playbook begins with a YAML front matter block.

```yaml
---
id: bootstrap-001
title: Create Solution Skeleton
description: Creates the initial .NET solution and repository bootstrap assets.
version: 1.0.0
status: Production Ready

category: Bootstrap
type: Generation

owner: AI Engineering Toolkit

authors:
  - Samuel Santos

created: 2026-08-09
last-reviewed: 2026-08-09

ai-models:
  - ChatGPT
  - GitHub Copilot

languages:
  - PowerShell

outputs:
  - PowerShell Script

tags:
  - bootstrap
  - dotnet
  - powershell

related-playbooks:
  - bootstrap-002

references:
  - PROMPT_ARCHITECTURE.md

license: MIT
---
```

---

# Required Metadata

The following properties are mandatory.

| Property                | Description                         |
| ----------------------- | ----------------------------------- |
| **id**            | Unique identifier for the playbook. |
| **title**         | Human-readable title.               |
| **description**   | Brief summary of the playbook.      |
| **version**       | Semantic version.                   |
| **status**        | Current maturity level.             |
| **category**      | Engineering domain.                 |
| **type**          | Playbook classification.            |
| **created**       | Initial publication date.           |
| **last-reviewed** | Most recent review date.            |

---

# Optional Metadata

Additional metadata may be included when applicable.

| Property          | Description                            |
| ----------------- | -------------------------------------- |
| owner             | Team or repository owner.              |
| authors           | Contributors.                          |
| ai-models         | Validated AI assistants.               |
| languages         | Target technologies.                   |
| outputs           | Generated artifact types.              |
| tags              | Search keywords.                       |
| related-playbooks | Related Engineering Playbooks.         |
| references        | Supporting standards or documentation. |
| license           | Licensing information.                 |

---

# Playbook Categories

Categories identify the engineering domain.

Examples include:

* Architecture
* Bootstrap
* Documentation
* DevOps
* GitHub
* PowerShell
* .NET
* Testing
* Code Review
* Refactoring
* Security
* AI

Future categories may be introduced without affecting existing playbooks.

---

# Playbook Types

Each Engineering Playbook belongs to exactly one type.

| Type        | Purpose                                           |
| ----------- | ------------------------------------------------- |
| Generation  | Creates new engineering artifacts.                |
| Review      | Evaluates existing artifacts.                     |
| Refactoring | Improves existing assets while preserving intent. |
| Analysis    | Produces engineering insights or assessments.     |

---

# Status Model

Playbooks evolve through defined maturity levels.

| Status           | Description                                           |
| ---------------- | ----------------------------------------------------- |
| Draft            | Initial proposal under development.                   |
| Experimental     | Functional but still evolving.                        |
| Validated        | Successfully exercised and considered reliable.       |
| Production Ready | Stable, reusable, and recommended for general use.    |
| Deprecated       | Retained for compatibility but no longer recommended. |
| Archived         | Preserved for historical reference only.              |

Status should be updated as the playbook matures.

---

# Identifier Convention

Every Engineering Playbook receives a permanent identifier.

Format:

```text
<category>-<number>
```

Examples:

```text
bootstrap-001
bootstrap-002
architecture-001
powershell-003
testing-004
```

Identifiers are immutable.

Titles may change.

Identifiers never change.

---

# Versioning

Engineering Playbooks follow Semantic Versioning.

Examples:

```text
1.0.0
1.1.0
2.0.0
```

Version increments should follow these rules:

* Major → Breaking structural changes.
* Minor → New capabilities.
* Patch → Corrections and clarifications.

---

# Tags

Tags improve discoverability.

Recommended tags:

* architecture
* bootstrap
* dotnet
* powershell
* github
* devops
* documentation
* testing
* security
* automation
* ai
* prompt-engineering

Tags should remain concise and technology-neutral whenever possible.

---

# Relationships

Metadata may reference other Engineering Playbooks.

Typical relationships include:

* Prerequisite playbooks
* Follow-up playbooks
* Supporting standards
* Framework templates
* Reference implementations

These relationships create a navigable engineering knowledge graph.

---

# Validation Rules

Every playbook should satisfy the following metadata requirements:

* Required fields are present.
* Identifier is unique.
* Metadata is valid YAML.
* Semantic version is valid.
* Status is recognized.
* Category exists.
* Type is valid.
* References resolve correctly.

Future automation may validate metadata during repository workflows.

---

# Extensibility

The metadata model is intentionally extensible.

New properties may be introduced without breaking existing playbooks, provided backward compatibility is maintained.

Deprecated properties should remain documented until fully removed.

---

# Governance

Metadata is part of the Engineering Playbook.

Changes to metadata should follow the same review and approval process as changes to playbook content.

Metadata accuracy is essential for maintaining repository consistency and enabling future automation.

---

# Conclusion

Prompt metadata transforms Engineering Playbooks into governed, searchable, versioned engineering assets.

By establishing a standardized metadata model, the AI Engineering Toolkit supports consistent authoring, automated validation, repository organization, and long-term evolution while remaining independent of specific AI models, programming languages, or implementation technologies.
