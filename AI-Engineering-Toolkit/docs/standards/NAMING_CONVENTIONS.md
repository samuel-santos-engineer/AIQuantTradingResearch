
# NAMING_CONVENTIONS.md

# Naming Conventions

## Purpose

This document defines the naming conventions for all engineering assets within the AI Engineering Toolkit.

Consistent naming improves discoverability, readability, automation, and long-term maintainability. Every repository artifact—including Engineering Playbooks, templates, standards, examples, and reference implementations—must follow these conventions.

---

# Naming Principles

All names should follow these principles:

* Clear and descriptive
* Consistent across the repository
* Technology independent where practical
* Human readable
* Machine friendly
* Stable over time
* Predictable
* Easy to search

Names should describe **what an artifact represents**, not how it is implemented.

---

# General Conventions

Unless otherwise specified:

* Use uppercase file names for standards, architectural documents, and governance assets.
* Use kebab-case (`-`) for folders.
* Avoid spaces in file and folder names.
* Avoid abbreviations unless they are widely recognized (for example, AI, API, SDK, CI, CD).
* Use singular nouns whenever possible.

Examples:

```text
PROMPT_ARCHITECTURE.md
QUALITY_GUIDELINES.md
PLAYBOOK_TEMPLATE.md

playbooks/
reference-implementations/
code-review/
```

---

# Repository Structure

Repository folders should describe engineering responsibilities rather than technologies whenever practical.

Recommended examples:

```text
docs/
playbooks/
templates/
examples/
reference-implementations/
assets/
```

Subfolders should represent engineering domains.

Examples:

```text
architecture/
bootstrap/
documentation/
dotnet/
github/
powershell/
devops/
testing/
security/
ai/
```

---

# Documentation

Documentation files should use uppercase snake case.

Format:

```text
DESCRIPTIVE_NAME.md
```

Examples:

```text
PROMPT_ARCHITECTURE.md
PROMPT_METADATA.md
PROMPT_LIFECYCLE.md
QUALITY_GUIDELINES.md
PLAYBOOK_TEMPLATE.md
```

Documentation names should describe the engineering concept they define.

---

# Engineering Playbooks

Playbook names should begin with an ordered identifier followed by a concise description.

Format:

```text
NN-description.md
```

Examples:

```text
01-create-solution.md
02-create-projects.md
03-create-build-assets.md
04-create-github-assets.md
```

The numeric prefix communicates execution order within a collection.

Names should describe the engineering objective.

---

# Templates

Templates should end with `_TEMPLATE`.

Examples:

```text
PLAYBOOK_TEMPLATE.md
REVIEW_TEMPLATE.md
VALIDATION_TEMPLATE.md
CHECKLIST_TEMPLATE.md
```

Template names should represent reusable engineering structures rather than specific implementations.

---

# Standards

Standards define repository-wide engineering guidance.

Naming format:

```text
DESCRIPTIVE_STANDARD.md
```

Examples:

```text
QUALITY_GUIDELINES.md
PROMPT_METADATA.md
NAMING_CONVENTIONS.md
```

Standards should be stable and rarely renamed.

---

# Examples

Examples should clearly identify the engineering scenario they demonstrate.

Examples:

```text
bootstrap-example
architecture-example
review-example
documentation-example
```

Example names should emphasize the engineering activity rather than a specific AI assistant.

---

# Reference Implementations

Reference implementations should use the official project name.

Examples:

```text
AIQuantTradingResearch/
SampleWebApi/
PluginFrameworkDemo/
```

Reference implementations should remain independent of playbook names.

---

# Metadata Identifiers

Playbook identifiers follow the convention:

```text
<category>-<number>
```

Examples:

```text
bootstrap-001
architecture-003
testing-002
github-004
```

Identifiers are permanent and never reused.

---

# Version Names

Engineering Playbooks follow Semantic Versioning.

Examples:

```text
1.0.0
1.2.0
2.0.0
```

Version numbers communicate evolution, not execution order.

---

# Tags

Tags should be:

* lowercase
* concise
* singular
* technology neutral where practical

Examples:

```text
architecture
bootstrap
automation
testing
documentation
security
github
powershell
dotnet
```

Avoid redundant tags.

---

# AI Assistant Names

Use official product names when referencing AI assistants.

Examples:

```text
ChatGPT
GitHub Copilot
Claude Code
Cursor
```

Do not invent abbreviations or unofficial names.

---

# Consistency Rules

Engineering assets should:

* Use consistent terminology.
* Avoid multiple names for the same concept.
* Preserve names once published unless a compelling reason exists.
* Update references when renaming becomes necessary.

Consistency takes precedence over personal preference.

---

# Reserved Terms

The following repository terms have specific meanings.

| Term                               | Meaning                                                                                                          |
| ---------------------------------- | ---------------------------------------------------------------------------------------------------------------- |
| **Engineering Playbook**     | A reusable engineering specification executed with AI assistance.                                                |
| **Template**                 | A reusable structure used to author Engineering Playbooks or related assets.                                     |
| **Standard**                 | Repository-wide engineering guidance that defines expected practices.                                            |
| **Reference Implementation** | A real project validating one or more Engineering Playbooks.                                                     |
| **Framework**                | The collection of standards, templates, governance, and supporting assets that enable the Engineering Playbooks. |

These terms should be used consistently throughout the repository.

---

# Naming Checklist

Before creating a new engineering asset, verify that:

* The name clearly describes its purpose.
* The correct naming pattern is used.
* Existing names are reused where appropriate.
* Folder placement follows the repository structure.
* The name supports long-term maintainability.

---

# Conclusion

Consistent naming is fundamental to the usability and longevity of the AI Engineering Toolkit.

By following these conventions, Engineering Playbooks and supporting assets remain easy to discover, understand, automate, and maintain, enabling the toolkit to scale without sacrificing clarity or consistency.
