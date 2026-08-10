
# 04 – Create GitHub Assets

## Metadata

| Property                     | Value                  |
| ---------------------------- | ---------------------- |
| **Playbook ID**        | BOOTSTRAP-004          |
| **Name**               | Create GitHub Assets   |
| **Category**           | Bootstrap              |
| **Version**            | 1.0                    |
| **Author**             | AI Engineering Toolkit |
| **Status**             | Stable                 |
| **Output Type**        | Repository Governance  |
| **Estimated Duration** | 20–30 minutes         |

---

# Purpose

This Engineering Playbook establishes the GitHub foundation of a software repository.

It creates the governance, collaboration, automation, and community assets required to support professional software development throughout the project's lifecycle.

Rather than focusing solely on automation workflows, this playbook defines how contributors interact with the repository and how engineering activities are managed.

---

# Objective

Create a GitHub repository that:

* Encourages collaboration.
* Standardizes contribution workflows.
* Establishes engineering governance.
* Supports automation.
* Improves maintainability.
* Scales as the project evolves.

---

# Scope

This playbook is responsible for:

* Creating GitHub repository assets.
* Establishing repository governance.
* Configuring issue management.
* Configuring pull request workflows.
* Creating GitHub Actions.
* Preparing the repository for collaborative development.

This playbook is **not** responsible for:

* Creating source code.
* Creating build configuration.
* Creating documentation.
* Creating development environment settings.
* Creating deployment infrastructure.

Those responsibilities belong to other playbooks.

---

# Prerequisites

Before executing this playbook:

* BOOTSTRAP-001 has completed successfully.
* BOOTSTRAP-002 has completed successfully.
* BOOTSTRAP-003 has completed successfully.
* The repository structure exists.
* Engineering standards have been established.

---

# Required Inputs

| Input                 | Description                       |
| --------------------- | --------------------------------- |
| Repository Name       | GitHub repository name.           |
| Repository Visibility | Public or Private.                |
| Default Branch        | Primary development branch.       |
| License               | Repository license.               |
| Community Features    | Discussions, Wiki, Projects, etc. |

---

# GitHub Assets

The following repository assets should be generated.

```text
.github/
│
├── ISSUE_TEMPLATE/
│
├── workflows/
│
├── PULL_REQUEST_TEMPLATE.md
│
├── CODEOWNERS
│
├── FUNDING.yml
│
├── dependabot.yml
│
└── labels/
```

Repository root assets:

```text
CODE_OF_CONDUCT.md

CONTRIBUTING.md

SECURITY.md

SUPPORT.md

LICENSE

README.md
```

---

# Asset Responsibilities

| Asset                           | Responsibility                   |
| ------------------------------- | -------------------------------- |
| **Issue Templates**       | Standardize issue reporting.     |
| **Pull Request Template** | Standardize code reviews.        |
| **GitHub Workflows**      | Automate engineering activities. |
| **CODEOWNERS**            | Define review ownership.         |
| **Dependabot**            | Manage dependency updates.       |
| **Security Policy**       | Define vulnerability reporting.  |
| **Contributing Guide**    | Explain contribution workflow.   |
| **Code of Conduct**       | Define community expectations.   |

Each asset contributes to repository governance.

---

# Engineering Principles

GitHub assets should:

* Encourage collaboration.
* Reduce manual administration.
* Promote engineering consistency.
* Support automation.
* Improve repository discoverability.
* Scale with project growth.

Repository governance should evolve without disrupting contributors.

---

# GitHub Features

Recommended repository features include:

* Issues
* Discussions
* Pull Requests
* Actions
* Projects
* Releases
* Security Advisories
* Dependabot
* Branch Protection
* Code Owners

These features establish a complete engineering collaboration platform.

---

# Recommended Labels

Repositories should include labels that support engineering workflows.

Examples include:

* architecture
* documentation
* enhancement
* feature
* refactoring
* bug
* tests
* research
* ai
* api
* infrastructure
* devops
* help wanted
* good first issue
* duplicate
* invalid
* question
* wontfix
* P0
* P1
* P2

Additional labels may be introduced to support project-specific workflows.

---

# Execution Workflow

```text
Verify Repository
        │
        ▼
Validate Inputs
        │
        ▼
Create Governance Assets
        │
        ▼
Configure Templates
        │
        ▼
Configure Automation
        │
        ▼
Verify Repository
        │
        ▼
Complete
```

---

# Engineering Instructions

The AI assistant should:

1. Verify prerequisite playbooks.
2. Generate repository governance assets.
3. Create issue and pull request templates.
4. Configure GitHub workflows.
5. Configure repository labels.
6. Configure CODEOWNERS.
7. Configure dependency management.
8. Validate repository readiness.

The generated repository should be suitable for professional collaborative development.

---

# Deliverables

Successful execution produces:

* GitHub configuration.
* Repository governance.
* Community documentation.
* Issue templates.
* Pull request templates.
* GitHub Actions.
* Repository labels.
* Dependency automation.

The repository becomes collaboration-ready.

---

# Acceptance Criteria

Execution is successful when:

* GitHub configuration exists.
* Governance documents are present.
* Issue templates exist.
* Pull request template exists.
* CODEOWNERS exists.
* GitHub workflows are configured.
* Repository labels have been defined.
* Repository governance is complete.

---

# Validation Checklist

Verify that:

* Required GitHub assets exist.
* Governance documents are complete.
* Templates follow repository standards.
* GitHub workflows validate successfully.
* Labels support engineering processes.
* Repository is ready for collaborative development.

Validation confirms repository governance has been successfully established.

---

# Common Pitfalls

Avoid:

* Mixing documentation with GitHub configuration.
* Creating unnecessary workflows.
* Duplicating repository standards.
* Ignoring repository security.
* Omitting contribution guidance.
* Creating technology-specific automation unnecessarily.

Repository governance should remain clear, maintainable, and scalable.

---

# Dependencies

This playbook depends on:

* BOOTSTRAP-001 – Create Solution
* BOOTSTRAP-002 – Create Directory Structure
* BOOTSTRAP-003 – Create Build Assets
* Engineering Standards
* Naming Conventions
* Quality Guidelines

Subsequent playbooks assume repository governance is in place.

---

# Next Playbook

After completing this playbook, continue with:

**05 – Create Documentation**

The next playbook establishes the documentation foundation, including architectural documentation, engineering guides, project references, and onboarding materials.

---

# References

Applicable repository assets include:

* Bootstrap Collection README
* Repository Governance Standards
* CONTRIBUTING.md
* CODE_OF_CONDUCT.md
* QUALITY_GUIDELINES.md
* REVIEW_TEMPLATE.md
* VALIDATION_TEMPLATE.md

---

# Conclusion

This playbook transforms a newly created repository into a governed engineering workspace by establishing GitHub-based collaboration, automation, and community standards.

By separating repository governance from implementation and infrastructure concerns, the AI Engineering Toolkit ensures every project begins with a professional collaboration model that supports maintainability, transparency, automation, and long-term growth.
