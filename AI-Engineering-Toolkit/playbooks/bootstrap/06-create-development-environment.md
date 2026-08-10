
# 06 – Create Development Environment

## Metadata

| Property                     | Value                           |
| ---------------------------- | ------------------------------- |
| **Playbook ID**        | BOOTSTRAP-006                   |
| **Name**               | Create Development Environment  |
| **Category**           | Bootstrap                       |
| **Version**            | 1.0                             |
| **Author**             | AI Engineering Toolkit          |
| **Status**             | Stable                          |
| **Output Type**        | Developer Experience Foundation |
| **Estimated Duration** | 15–30 minutes                  |

---

# Purpose

This Engineering Playbook establishes the repository's development environment by configuring the assets required to deliver a consistent and productive engineering experience.

Rather than installing software, this playbook prepares the repository for development by defining editor settings, recommended tooling, debugging configuration, development tasks, and workspace conventions.

The objective is to minimize onboarding effort while ensuring every contributor works within the same engineering environment.

---

# Objective

Create a standardized development environment that:

* Improves developer productivity.
* Supports consistent engineering practices.
* Reduces onboarding time.
* Integrates with repository standards.
* Provides a predictable development experience.
* Enables AI-assisted engineering workflows.

---

# Scope

This playbook is responsible for:

* Creating workspace configuration.
* Defining editor settings.
* Configuring recommended extensions.
* Creating development tasks.
* Configuring debugging support.
* Establishing repository workspace conventions.

This playbook is **not** responsible for:

* Installing software.
* Installing SDKs.
* Installing extensions.
* Creating application code.
* Creating CI/CD pipelines.
* Managing operating system configuration.

Those responsibilities remain outside the repository.

---

# Prerequisites

Before executing this playbook:

* BOOTSTRAP-001 through BOOTSTRAP-005 have completed successfully.
* Repository structure exists.
* Build assets are available.
* Documentation foundation exists.
* GitHub assets have been configured.

---

# Required Inputs

| Input               | Description                                              |
| ------------------- | -------------------------------------------------------- |
| Primary IDE         | Visual Studio Code, Visual Studio, JetBrains Rider, etc. |
| Supported Platforms | Windows, Linux, macOS.                                   |
| Technology Stack    | Primary implementation platform.                         |
| Build Commands      | Repository build commands.                               |
| Test Commands       | Repository validation commands.                          |

---

# Development Environment Assets

The repository should generate development environment assets similar to:

```text
.vscode/
│
├── settings.json
├── extensions.json
├── launch.json
├── tasks.json
└── README.md
```

Additional IDE-specific configurations may be created when appropriate.

---

# Asset Responsibilities

| Asset                      | Responsibility                                                     |
| -------------------------- | ------------------------------------------------------------------ |
| **settings.json**    | Repository-specific editor configuration.                          |
| **extensions.json**  | Recommended editor extensions.                                     |
| **launch.json**      | Standard debugging configurations.                                 |
| **tasks.json**       | Common engineering tasks such as build, test, restore, and format. |
| **Workspace README** | Guidance for configuring the local development environment.        |

Each asset should improve the developer experience without introducing machine-specific dependencies.

---

# Engineering Principles

The development environment should:

* Be reproducible.
* Remain platform-independent whenever practical.
* Avoid user-specific configuration.
* Support automation.
* Minimize manual setup.
* Promote engineering consistency.
* Integrate naturally with AI-assisted development tools.

Developer productivity should improve without sacrificing maintainability.

---

# Recommended Workspace Capabilities

A standard engineering workspace should support:

* Build execution.
* Test execution.
* Code formatting.
* Repository validation.
* Static analysis.
* Debugging.
* Task automation.
* AI-assisted development.

The exact capabilities may vary according to the technology stack.

---

# Execution Workflow

```text
Verify Repository
        │
        ▼
Validate Inputs
        │
        ▼
Create Workspace Assets
        │
        ▼
Configure Development Tasks
        │
        ▼
Configure Debugging
        │
        ▼
Verify Environment
        │
        ▼
Complete
```

Each stage should complete successfully before continuing.

---

# Engineering Instructions

The AI assistant should:

1. Verify all prerequisite playbooks.
2. Generate repository workspace configuration.
3. Configure recommended editor settings.
4. Create reusable engineering tasks.
5. Configure debugging support where appropriate.
6. Avoid user-specific or machine-specific settings.
7. Validate the resulting workspace configuration.

The generated environment should support both human developers and AI assistants.

---

# Deliverables

Successful execution produces:

* Workspace configuration.
* Recommended editor extensions.
* Development tasks.
* Debugging configuration.
* Developer onboarding assets.

The repository becomes ready for productive software development.

---

# Acceptance Criteria

Execution is successful when:

* Workspace configuration exists.
* Editor settings follow repository standards.
* Recommended extensions are documented.
* Development tasks execute successfully.
* Debugging configuration is available.
* No machine-specific configuration has been introduced.

---

# Validation Checklist

Verify that:

* Workspace configuration exists.
* Development tasks execute successfully.
* Repository build commands are available.
* Repository test commands are available.
* Formatting commands are available.
* Debugging configuration is valid.
* AI development tools can operate without additional repository configuration.

Validation confirms the repository provides a consistent development experience.

---

# Common Pitfalls

Avoid:

* Hardcoding local file paths.
* Including personal editor preferences.
* Requiring proprietary tools unnecessarily.
* Duplicating build configuration.
* Mixing IDE configuration with repository governance.
* Creating platform-specific assumptions without justification.

The development environment should remain portable and maintainable.

---

# Dependencies

This playbook depends on:

* BOOTSTRAP-001 – Create Solution
* BOOTSTRAP-002 – Create Directory Structure
* BOOTSTRAP-003 – Create Build Assets
* BOOTSTRAP-004 – Create GitHub Assets
* BOOTSTRAP-005 – Create Documentation
* Engineering Standards
* Naming Conventions
* Quality Guidelines

The development environment builds upon the engineering foundation established by previous bootstrap activities.

---

# Next Playbook

After completing this playbook, continue with:

**07 – Validate Bootstrap**

The final bootstrap playbook verifies that all repository assets have been created successfully and that the repository complies with the engineering standards defined by the AI Engineering Toolkit.

---

# References

Applicable repository assets include:

* Bootstrap Collection README
* Engineering Standards
* Build Guidelines
* Documentation Standards
* Quality Guidelines
* Playbook Template
* Review Template
* Validation Template

---

# Conclusion

This playbook establishes the repository's development environment by creating a consistent workspace configuration, reusable engineering tasks, editor settings, and debugging support.

By focusing on developer experience rather than software installation, the AI Engineering Toolkit ensures that repositories remain portable, reproducible, and easy to onboard while providing a reliable foundation for both human engineers and AI-assisted development workflows.
