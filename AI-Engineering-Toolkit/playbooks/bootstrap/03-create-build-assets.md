
# 03 – Create Build Assets

## Metadata

| Property                     | Value                        |
| ---------------------------- | ---------------------------- |
| **Playbook ID**        | BOOTSTRAP-003                |
| **Name**               | Create Build Assets          |
| **Category**           | Bootstrap                    |
| **Version**            | 1.0                          |
| **Author**             | AI Engineering Toolkit       |
| **Status**             | Stable                       |
| **Output Type**        | Engineering Build Foundation |
| **Estimated Duration** | 10–20 minutes               |

---

# Purpose

This Engineering Playbook establishes the shared engineering assets required to build, validate, and maintain a software solution consistently across local development environments and automated build systems.

Rather than creating application code, this playbook creates the engineering infrastructure that supports repeatable builds, centralized configuration, and future automation.

---

# Objective

Create a standardized build foundation that:

* Centralizes build configuration.
* Promotes deterministic builds.
* Supports reproducible development environments.
* Enables future CI/CD integration.
* Reduces configuration duplication across projects.

---

# Scope

This playbook is responsible for:

* Creating shared build configuration.
* Creating engineering automation scripts.
* Defining SDK and tooling versions.
* Establishing repository-wide build settings.
* Preparing the repository for automated builds.

This playbook is **not** responsible for:

* Creating source projects.
* Creating GitHub workflows.
* Creating documentation.
* Creating IDE settings.
* Creating deployment pipelines.
* Creating application code.

Those responsibilities belong to later playbooks.

---

# Prerequisites

Before executing this playbook:

* BOOTSTRAP-001 has completed successfully.
* BOOTSTRAP-002 has completed successfully.
* Repository directory structure exists.
* Solution file exists.

---

# Required Inputs

| Input               | Description                          |
| ------------------- | ------------------------------------ |
| Technology Stack    | Primary implementation platform.     |
| SDK Version         | Required SDK version.                |
| Build Tool          | Preferred build system.              |
| Package Strategy    | Central package management approach. |
| Formatting Standard | Repository formatting rules.         |

---

# Build Assets

The following engineering assets should be created.

```text
Repository/
│
├── global.json
├── Directory.Build.props
├── Directory.Build.targets
├── Directory.Packages.props
├── .editorconfig
│
└── eng/
    │
    ├── build.ps1
    ├── build.sh
    ├── clean.ps1
    ├── restore.ps1
    ├── test.ps1
    ├── format.ps1
    ├── verify.ps1
    └── package.ps1
```

Additional assets may be introduced when justified by project requirements.

---

# Asset Responsibilities

| Asset                              | Responsibility                                                             |
| ---------------------------------- | -------------------------------------------------------------------------- |
| **global.json**              | Pins the SDK version to ensure consistent builds.                          |
| **Directory.Build.props**    | Defines repository-wide MSBuild properties.                                |
| **Directory.Build.targets**  | Centralizes shared build targets.                                          |
| **Directory.Packages.props** | Manages package versions from a single location.                           |
| **.editorconfig**            | Establishes repository-wide formatting conventions.                        |
| **eng/**                     | Contains engineering automation scripts used by developers and CI systems. |

Each asset should have a clearly defined responsibility.

---

# Engineering Principles

Build assets should:

* Be deterministic.
* Be centralized.
* Avoid duplicated configuration.
* Be easy to maintain.
* Be compatible with automated build systems.
* Minimize manual developer setup.

The engineering platform should behave consistently across environments.

---

# Execution Workflow

```text
Verify Repository
        │
        ▼
Validate Inputs
        │
        ▼
Generate Build Assets
        │
        ▼
Create Engineering Scripts
        │
        ▼
Validate Build Configuration
        │
        ▼
Complete
```

Each stage should complete successfully before continuing.

---

# Engineering Instructions

The AI assistant should:

1. Verify prerequisite playbooks have completed.
2. Generate repository-wide build configuration.
3. Create centralized package management assets.
4. Create engineering automation scripts.
5. Avoid technology-specific customization beyond the selected stack.
6. Verify all generated assets.

The implementation should prioritize maintainability over complexity.

---

# Deliverables

Successful execution produces:

* Repository build configuration.
* Engineering automation scripts.
* SDK version management.
* Centralized package management.
* Repository formatting configuration.

These assets establish the engineering platform for future development.

---

# Acceptance Criteria

Execution is successful when:

* All required build assets exist.
* SDK version is defined.
* Shared build configuration is centralized.
* Engineering scripts are available.
* Repository formatting configuration exists.
* Build assets conform to repository standards.

---

# Validation Checklist

Verify that:

* `global.json` exists.
* Shared build properties are centralized.
* Package management configuration exists.
* Engineering scripts execute without errors.
* Formatting configuration is present.
* Repository builds successfully using the generated assets.

Validation confirms readiness for the next bootstrap stage.

---

# Common Pitfalls

Avoid:

* Duplicating configuration across projects.
* Hardcoding machine-specific paths.
* Embedding environment-specific values.
* Mixing CI/CD configuration with build assets.
* Creating application projects prematurely.
* Ignoring repository-wide package management.

A clean engineering foundation reduces maintenance costs over time.

---

# Dependencies

This playbook depends on:

* BOOTSTRAP-001 – Create Solution
* BOOTSTRAP-002 – Create Directory Structure
* Repository Naming Conventions
* Engineering Standards
* Quality Guidelines

Subsequent bootstrap activities assume these build assets are available.

---

# Next Playbook

After completing this playbook, continue with:

**04 – Create GitHub Assets**

The next playbook establishes repository governance, collaboration workflows, issue templates, pull request templates, labels, and automation assets for GitHub.

---

# References

Applicable repository assets include:

* Bootstrap Collection README
* Engineering Build Guidelines
* PROJECT_STRUCTURE
* DEPENDENCY_INJECTION
* Naming Conventions
* Quality Guidelines
* Playbook Template
* Validation Template

---

# Conclusion

This playbook establishes the engineering build foundation of the repository by creating centralized build configuration, version management, formatting standards, and reusable automation scripts.

By separating build infrastructure from application implementation, the AI Engineering Toolkit ensures deterministic builds, consistent developer experiences, and a maintainable engineering platform that supports future automation, continuous integration, and long-term project evolution.
