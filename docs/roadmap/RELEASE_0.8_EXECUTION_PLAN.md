# Release 0.8 — Solution Skeleton Execution Plan

## Purpose

This document defines the exact execution plan for **Phase 2 — Release 0.8: Solution Skeleton** of AIQuantTradingResearch.

The goal of this release is to transform the approved architecture and engineering standards into a minimal, buildable, testable, validated .NET solution skeleton without implementing business capabilities that belong to later releases.

Release 0.8 is the first major implementation milestone executed through the AI-first engineering lifecycle established by the AI Engineering Toolkit.

---

## Release Objective

Create the physical .NET solution foundation required for later platform implementation while preserving the architecture, dependency rules, testing strategy, engineering automation, and repository governance already defined.

The release should prove that the repository can support:

- Domain-first architecture.
- Explicit dependency direction.
- Centralized build configuration.
- Centralized package management.
- Application hosting and composition.
- Automated testing.
- Executable architecture validation.
- Repeatable local verification.
- AI-assisted implementation through repository-aware coding agents.

---

## Release Boundaries

### In Scope

- Root solution creation.
- Production project creation.
- Test project creation.
- Project references.
- Centralized build configuration verification.
- Centralized package management verification.
- Minimal worker host.
- Dependency injection composition root.
- Basic application configuration.
- Architecture tests.
- Build/test/format/verify integration.
- Repository-level validation.
- Documentation updates required to reflect the created skeleton.

### Out of Scope

- Market data ingestion.
- Exchange or provider integrations.
- Storage implementation.
- Database schemas.
- Trading strategies.
- Backtesting engine.
- Machine learning.
- AI inference.
- Portfolio management.
- Order execution.
- Public REST APIs.
- Authentication or authorization features.
- Production deployment infrastructure.
- Cloud resource provisioning.
- Messaging infrastructure.
- Plugin framework implementation.

---

## Authority Model

Implementation must follow repository guidance in this order:

```text
Release 0.8 Requirements

↓

Approved Architecture Documents

↓

Implementation and Dependency Rules

↓

AI Engineering Toolkit Playbooks

↓

Repository Conventions

↓

Reference Implementations

↓

Coding Agent Preference
```

The coding agent must not silently override higher-authority guidance.

---

## AI-First Execution Model

```text
Human Engineering Intent

↓

Approved Release Plan

↓

Repository Architecture Context

↓

AI Engineering Toolkit

↓

Coding Agent Plan

↓

Incremental Repository Changes

↓

Automated Validation

↓

Human Review

↓

GitHub Pull Request

↓

Release Acceptance
```

The coding agent is an implementation executor, not the source of architectural authority.

---

## Recommended Tool Responsibilities

### ChatGPT

Use ChatGPT for:

- Release planning.
- Architectural interpretation.
- Engineering trade-offs.
- Prompt preparation.
- Review of execution results.
- Review of architecture implications.

### Codex in VS Code

Use Codex for:

- Repository inspection.
- File and project creation.
- Project references.
- Configuration changes.
- Test skeleton implementation.
- Architecture test implementation.
- Build and test execution.
- Local validation.
- Change summaries.

### GitHub Copilot

Use Copilot optionally for:

- Inline completion.
- Small local edits.
- Repetitive syntax.
- Focused code suggestions.

### GitHub

Use GitHub for:

- Issue tracking.
- Branching.
- Pull request review.
- Validation evidence.
- Merge history.
- Release traceability.

---

## Work Package 01 — Repository Preflight

### Objective

Verify the repository is ready for Solution Skeleton implementation before any new project files are created.

### Activities

1. Inspect the repository root.
2. Locate existing architecture documentation.
3. Locate implementation guidelines.
4. Inspect root build assets.
5. Inspect `eng/` scripts.
6. Inspect GitHub workflows.
7. Confirm the .NET SDK configuration.
8. Identify pre-existing solution or project files.

### Required Evidence

Report:

- Current repository root structure.
- Existing solution files.
- Existing project files.
- Existing build configuration.
- Existing package management configuration.
- Existing validation scripts.
- Conflicts between this execution plan and repository state.

### Stop Conditions

Stop and report if:

- Existing structure materially conflicts with approved architecture.
- A different solution skeleton already exists.
- Required SDK cannot be resolved.
- Architecture documents define a materially different project model.

---

## Work Package 02 — Root Solution

### Objective

Create the root solution container.

### Expected Artifact

Preferred:

```text
AIQuantTradingResearch.slnx
```

If repository or SDK compatibility requires `.sln`, use that format and document the deviation.

### Validation

Verify:

- Solution can be opened by the installed .NET SDK.
- Expected projects are discoverable.
- No unrelated projects are added.

---

## Work Package 03 — Production Projects

### Objective

Create the minimal production project set representing the approved architecture.

### Projects

```text
src/
├── AIQuantTradingResearch.Domain/
├── AIQuantTradingResearch.Application/
├── AIQuantTradingResearch.Infrastructure/
└── AIQuantTradingResearch.Worker/
```

### Domain

Responsibilities:

- Core business abstractions.
- Domain concepts introduced in later releases.

Release 0.8 should keep this project intentionally minimal.

It must not depend on Application, Infrastructure, or Worker.

### Application

Responsibilities:

- Application orchestration abstractions.
- Use-case contracts.
- Application-level ports.

It may depend on Domain only.

### Infrastructure

Responsibilities:

- Infrastructure implementations.
- External technology adapters introduced later.

For Release 0.8, it should contain only minimal composition support needed to prove the dependency model.

It may depend on Application and Domain.

### Worker

Acts as executable host and composition root.

It may depend on Application and Infrastructure.

It should contain host bootstrap, configuration, and dependency registration only.

---

## Work Package 04 — Project References

### Objective

Make architecture dependency direction explicit in the project graph.

### Required Direction

```text
Application → Domain
Infrastructure → Application
Infrastructure → Domain (only if actually required)
Worker → Application
Worker → Infrastructure
```

### Forbidden References

```text
Domain → Application
Domain → Infrastructure
Domain → Worker
Application → Infrastructure
Application → Worker
Infrastructure → Worker
```

These rules must be protected by architecture tests.

---

## Work Package 05 — Root Build Configuration

### Objective

Verify and align repository-wide .NET configuration.

### Existing Assets to Inspect

```text
global.json
Directory.Build.props
Directory.Packages.props
.editorconfig
.gitattributes
.gitignore
```

### Rules

- Do not recreate or replace existing assets blindly.
- Preserve existing repository intent.
- Add only missing settings required for the new projects.
- Avoid project-level package version duplication.
- Respect repository SDK governance.
- Preserve nullable and analyzer policies already defined.

### Validation

Run from repository root:

```text
dotnet restore
dotnet build
```

---

## Work Package 06 — Minimal Worker Host

### Objective

Create a minimal executable composition root without introducing application features.

### Required Responsibilities

`Program.cs` should:

1. Create the generic host.
2. Load normal .NET configuration.
3. Register application services through an extension point.
4. Register infrastructure services through an extension point.
5. Build the host.
6. Run the host.

### Recommended Pattern

```text
Worker
  ↓
AddApplication(...)
  ↓
AddInfrastructure(...)
  ↓
Build
  ↓
Run
```

### Boundaries

Do not introduce:

- Market data services.
- Trading workflows.
- Database providers.
- HTTP endpoints.
- Cloud dependencies.
- Messaging providers.

---

## Work Package 07 — Dependency Registration Extensions

### Objective

Establish explicit composition boundaries for Application and Infrastructure.

### Suggested Artifacts

```text
src/AIQuantTradingResearch.Application/DependencyInjection.cs
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
```

`AddApplication` should expose application registration without infrastructure knowledge.

`AddInfrastructure` should expose infrastructure registration without leaking implementation decisions into Worker.

For Release 0.8, registrations may be intentionally empty or minimal.

---

## Work Package 08 — Test Projects

### Objective

Create the baseline test structure.

### Required Projects

```text
tests/
├── AIQuantTradingResearch.Domain.Tests/
├── AIQuantTradingResearch.Application.Tests/
├── AIQuantTradingResearch.Infrastructure.Tests/
└── AIQuantTradingResearch.Architecture.Tests/
```

### Responsibilities

- `Domain.Tests`: future domain rule verification.
- `Application.Tests`: future use-case verification.
- `Infrastructure.Tests`: future adapter/integration verification.
- `Architecture.Tests`: immediate executable dependency validation.

Release 0.8 should avoid artificial tests that exist only to increase test count.

---

## Work Package 09 — Architecture Tests

### Objective

Convert dependency rules into executable checks.

### Required Rules

Architecture tests must verify at minimum:

- Domain does not depend on Application.
- Domain does not depend on Infrastructure.
- Domain does not depend on Worker.
- Application does not depend on Infrastructure.
- Application does not depend on Worker.
- Infrastructure does not depend on Worker.

### Dependency Choice

Prefer a minimal implementation.

If a new architecture-test package is proposed, justify it according to dependency-management guidance and centralize its version.

### Validation

Architecture tests must run under:

```text
dotnet test
```

---

## Work Package 10 — Solution Organization

### Objective

Add all production and test projects to the root solution.

### Expected Membership

```text
AIQuantTradingResearch.Domain
AIQuantTradingResearch.Application
AIQuantTradingResearch.Infrastructure
AIQuantTradingResearch.Worker
AIQuantTradingResearch.Domain.Tests
AIQuantTradingResearch.Application.Tests
AIQuantTradingResearch.Infrastructure.Tests
AIQuantTradingResearch.Architecture.Tests
```

Solution organization should improve discoverability without becoming an architectural dependency.

---

## Work Package 11 — Engineering Scripts Integration

### Objective

Ensure existing engineering scripts work against the new solution.

### Scripts to Validate

```text
eng/build.ps1
eng/build.sh
eng/clean.ps1
eng/format.ps1
eng/test.ps1
eng/verify.ps1
```

### Required Behavior

- Build: restore/build complete solution.
- Test: execute all test projects.
- Format: verify or apply repository formatting.
- Verify: provide the primary local quality gate.

### Preferred Verify Flow

```text
Environment Check
  ↓
Restore
  ↓
Format Verification
  ↓
Build
  ↓
Tests
  ↓
Architecture Tests
  ↓
Repository-Specific Checks
  ↓
Success
```

Do not duplicate logic unnecessarily across scripts.

---

## Work Package 12 — Documentation Alignment

### Objective

Update repository documentation only where the created skeleton changes repository truth.

### Documents to Inspect

```text
README.md
ENGINEERING.md
ARCHITECTURE.md
PROJECT_STRUCTURE_V2.md
```

and relevant documents under `docs/architecture/`.

### Rules

Update only where needed to:

- Reflect actual project paths.
- Reflect actual solution format.
- Add build/test commands if missing.
- Mark the skeleton milestone implemented.
- Remove obsolete future-tense statements where necessary.

Do not redesign architecture during documentation alignment.

---

## Work Package 13 — Full Skeleton Validation

### Objective

Prove the repository is ready for subsequent platform implementation.

### Validation Sequence

```text
dotnet --info
dotnet restore
dotnet build
dotnet test
dotnet format --verify-no-changes
```

Then execute:

```text
eng/verify.ps1
```

and the repository-supported Unix equivalent where applicable.

### Required Evidence

Report:

- SDK resolved.
- Restore result.
- Build result.
- Test result.
- Architecture test result.
- Format validation result.
- Verify script result.
- Files created.
- Files modified.
- Dependencies introduced.
- Known warnings.
- Remaining risks.

---

## Work Package 14 — GitHub Integration

### Objective

Preserve engineering traceability.

### Recommended Workflow

```text
GitHub Issue
  ↓
Feature Branch
  ↓
Codex Implementation
  ↓
Local Verification
  ↓
Commit
  ↓
Push
  ↓
Pull Request
  ↓
CI
  ↓
Review
  ↓
Merge
```

Suggested issue title:

```text
Release 0.8 — Create Solution Skeleton
```

The issue should reference this execution plan and file manifest.

---

## Work Package 15 — Release Acceptance Review

### Objective

Determine whether Release 0.8 is complete.

### Review Areas

- Physical solution structure.
- Dependency direction.
- Build configuration.
- Package management.
- Worker composition root.
- Test project structure.
- Architecture tests.
- Engineering scripts.
- Documentation alignment.
- Validation evidence.

### Acceptance Status

Use one of:

```text
Accepted
Accepted with Observations
Changes Required
Blocked
```

---

## Execution Order

```text
01 Repository Preflight
  ↓
02 Root Solution
  ↓
03 Production Projects
  ↓
04 Project References
  ↓
05 Root Build Configuration
  ↓
06 Minimal Worker Host
  ↓
07 Dependency Registration Extensions
  ↓
08 Test Projects
  ↓
09 Architecture Tests
  ↓
10 Solution Organization
  ↓
11 Engineering Scripts Integration
  ↓
12 Documentation Alignment
  ↓
13 Full Skeleton Validation
  ↓
14 GitHub Integration
  ↓
15 Release Acceptance Review
```

---

## Coding Agent Prompt Contract

A Release 0.8 coding prompt should instruct the agent to:

1. Read this execution plan.
2. Read the file manifest.
3. Inspect repository architecture documents.
4. Inspect current repository state.
5. Produce a concise implementation plan.
6. Report conflicts before changing architecture.
7. Implement incrementally.
8. Avoid out-of-scope features.
9. Run required validation.
10. Report evidence.

### Required Constraints

```text
Do not invent business features.
Do not introduce infrastructure providers.
Do not create storage schemas.
Do not redesign architecture.
Do not weaken validation.
Do not modify unrelated repository assets.
Do not add dependencies without justification.
```

---

## Definition of Done

Release 0.8 is done when:

- Root solution exists.
- Four production projects exist.
- Four test projects exist.
- Project references follow approved architecture.
- Worker runs as a minimal host.
- Application and Infrastructure expose composition boundaries.
- Architecture dependency rules are executable.
- Root restore succeeds.
- Root build succeeds.
- All tests succeed.
- Formatting validation succeeds.
- Engineering verification succeeds.
- Documentation reflects the implemented skeleton.
- No later-release business capability has been introduced.
- Changes are reviewable through Git.
- Validation evidence is preserved in the pull request.

---

## Exit Criteria

The repository must be ready for the next implementation milestone without requiring structural rework.

A future engineer should be able to add market data behavior, application use cases, infrastructure adapters, additional hosts, storage, and pipelines without redesigning the physical project model first.

---

# Conclusion

Release 0.8 establishes the executable structural foundation of AIQuantTradingResearch.

The milestone is intentionally small in business behavior and strong in engineering structure.

Its lifecycle is:

```text
Approved Architecture
  ↓
Physical Solution
  ↓
Explicit Dependencies
  ↓
Minimal Composition
  ↓
Executable Architecture Rules
  ↓
Automated Validation
  ↓
Reviewed Foundation
```

The central principle is:

> **Release 0.8 succeeds when the repository proves that the approved architecture can exist as a clean, buildable, testable, and AI-operable .NET solution before business capabilities are allowed to accumulate.**
