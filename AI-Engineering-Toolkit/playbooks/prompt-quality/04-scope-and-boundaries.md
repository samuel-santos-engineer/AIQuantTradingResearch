
# Scope and Boundaries

## Purpose

The Scope and Boundaries guideline defines the engineering principles and practices for constraining AI-assisted work within explicit task, repository, architectural, security, and operational boundaries.

Its purpose is to reduce unintended changes, uncontrolled task expansion, architecture drift, security risk, and ambiguous execution by clearly defining what an AI system is authorized to inspect, modify, create, and leave unchanged.

Boundaries should preserve engineering intent while allowing enough flexibility to complete the task effectively.

---

# Objectives

The Scope and Boundaries guideline aims to:

* Define explicit task scope.
* Prevent unintended repository changes.
* Preserve architectural boundaries.
* Protect security-sensitive areas.
* Reduce uncontrolled refactoring.
* Limit unnecessary dependency introduction.
* Improve reviewability.
* Improve execution predictability.
* Support safe AI autonomy.
* Preserve traceability.
* Reduce engineering risk.

---

# Scope

This guideline applies to prompts used for:

* Repository modification.
* Software implementation.
* Refactoring.
* Testing.
* Documentation.
* Architecture changes.
* DevOps automation.
* GitHub operations.
* Security engineering.
* Performance work.
* Validation.
* AI-assisted engineering workflows.

Boundary rigor should increase with task impact, execution authority, and engineering risk.

---

# Engineering Philosophy

Every engineering task operates within boundaries.

A useful model is:

```text
Objective

↓

Authorized Scope

↓

Protected Boundaries

↓

Controlled Execution

↓

Validated Change
```

Without explicit boundaries, AI systems may interpret the task more broadly than intended.

Scope defines where work belongs.

Boundaries define where work must stop.

---

# Scope as an Engineering Contract

Scope should define the authorized area of change.

A prompt may specify:

* Components in scope.
* Files in scope.
* Projects in scope.
* Behaviors in scope.
* Outputs in scope.
* Activities explicitly out of scope.

The clearer the scope, the easier the resulting work is to review and validate.

---

# Scope Dimensions

Engineering scope may exist across multiple dimensions.

```text
Functional Scope

Repository Scope

Architectural Scope

Data Scope

Dependency Scope

Security Scope

Operational Scope

Temporal Scope
```

High-risk tasks may require several dimensions to be defined explicitly.

---

# Functional Scope

Functional scope defines which behavior may change.

Examples include:

* Add validation.
* Fix a defect.
* Introduce a new API operation.
* Improve logging.
* Add tests.

Functional scope should distinguish the requested capability from related but unrequested improvements.

---

# Repository Scope

Repository scope defines where changes may occur.

Example:

```text
In scope:
- src/MarketData/
- tests/MarketData.Tests/

Out of scope:
- infrastructure/
- docs/architecture/
- unrelated projects
```

Repository scope is especially useful for coding agents with broad workspace access.

---

# File-Level Scope

For narrowly targeted work, specific files may be identified.

Example:

```text
Allowed modifications:
- src/MarketData/MarketDataValidator.cs
- tests/MarketData.Tests/MarketDataValidatorTests.cs
```

File-level scope improves predictability but should not be used when legitimate implementation may require nearby changes.

---

# Project-Level Scope

Larger tasks may define project boundaries instead of individual files.

Example:

```text
Changes may be made within:
- MarketData.Domain
- MarketData.Application
- MarketData.Tests

Do not modify:
- Storage.Infrastructure
- Trading.Api
```

Project boundaries should reflect architectural ownership.

---

# Architectural Scope

Architectural scope defines which architectural responsibilities may be affected.

For example:

```text
This task may modify application-layer validation.

Do not:
- Change domain boundaries.
- Introduce new infrastructure dependencies.
- Redesign public contracts.
```

Architectural scope prevents implementation tasks from becoming unapproved design changes.

---

# Domain Boundaries

Domain boundaries should be respected explicitly when domain models exist.

AI systems should not casually:

* Move business rules across bounded contexts.
* Share domain entities between contexts.
* Introduce cross-context dependencies.
* Redefine ubiquitous language.

Domain boundary changes should be treated as architectural decisions.

---

# Public Contract Boundaries

Public contracts require strong protection.

Examples include:

* APIs.
* Public interfaces.
* Events.
* Message schemas.
* Package contracts.
* Configuration contracts.

Prompts should explicitly state whether these contracts may change.

For example:

```text
Preserve all existing public API signatures.
```

---

# Data Boundaries

Tasks involving persistence or data processing should define whether data contracts may change.

Potential boundaries include:

* Database schema.
* Migration behavior.
* Serialization formats.
* Event schemas.
* Data retention rules.

Changes to persistent data often have broader consequences than local code changes.

---

# Dependency Boundaries

Prompts should define whether dependency changes are allowed.

Example:

```text
Do not introduce new NuGet packages.

Use existing repository dependencies unless the task cannot be completed
without a new dependency, in which case report the constraint.
```

Dependency boundaries reduce unnecessary architectural coupling.

---

# Security Boundaries

Security-sensitive areas should be explicitly protected.

Examples include:

* Authentication.
* Authorization.
* Secrets.
* Permissions.
* Cryptography.
* Security policies.
* Production credentials.

A prompt should not allow incidental changes to these areas unless security changes are part of the task.

---

# Infrastructure Boundaries

Application-focused prompts should normally avoid modifying infrastructure unless required.

Potential infrastructure boundaries include:

* CI/CD.
* Cloud configuration.
* Deployment scripts.
* Networking.
* Kubernetes.
* Infrastructure-as-Code.

Crossing infrastructure boundaries should be intentional.

---

# Operational Boundaries

Operational behavior may require protection.

Examples include:

* Logging contracts.
* Health checks.
* Monitoring.
* Alerting.
* Retry behavior.
* Timeout behavior.

Prompts should identify when operational semantics must remain unchanged.

---

# Scope Inclusion

A good prompt should make included work explicit.

Example:

```text
In scope:
- Add timestamp validation.
- Add unit tests for valid and invalid timestamps.
- Reuse the existing validation abstraction.
```

Inclusion reduces interpretation variance.

---

# Scope Exclusion

Out-of-scope items should be explicit when they are plausible extensions of the task.

Example:

```text
Out of scope:
- Refactoring unrelated validators.
- Changing API contracts.
- Introducing new validation libraries.
- Updating deployment infrastructure.
```

Out-of-scope statements are particularly useful for preventing agent overreach.

---

# Minimal Change Principle

AI systems should prefer the smallest change that satisfies the engineering objective.

The preferred pattern is:

```text
Required Behavior

↓

Smallest Valid Change

↓

Validation

↓

Stop
```

The agent should not continue improving unrelated areas after the objective is satisfied.

---

# Change Isolation

Changes should remain isolated to the engineering concern whenever practical.

Benefits include:

* Easier review.
* Easier rollback.
* Better traceability.
* Reduced regression risk.
* Cleaner commits.

Change isolation is especially important for AI-generated modifications.

---

# Unrelated Refactoring

Prompts should explicitly control unrelated refactoring.

For example:

```text
Do not refactor unrelated code, even if improvement opportunities are found.

Report significant observations separately.
```

This allows useful findings without expanding execution scope.

---

# Opportunistic Improvements

AI systems may discover adjacent issues during execution.

Possible handling:

```text
If you identify unrelated improvements:
- Do not implement them.
- Record them as observations.
- Continue with the requested task.
```

This preserves task focus without discarding useful engineering insight.

---

# Scope Expansion

Sometimes legitimate completion requires expanding scope.

The prompt should define how this is handled.

Example:

```text
If completing the task requires modifying an out-of-scope component,
stop and report:
- Required change.
- Reason.
- Impact.
- Recommended next action.
```

Silent scope expansion should be avoided for significant changes.

---

# Allowed Flexibility

Not every implementation detail requires pre-approval.

Agents may generally use existing repository conventions for:

* Naming.
* Private method organization.
* Formatting.
* Local helper functions.
* Test arrangement.

when these decisions do not affect architecture, contracts, or security.

---

# Decision Boundaries

Prompts should distinguish decisions the agent may make independently from those requiring engineering approval.

Example:

```text
Agent may decide:
- Private method decomposition.
- Local variable naming.
- Test data organization.

Agent must not decide:
- New architectural layer.
- New public API.
- New dependency.
- Domain model redesign.
```

Decision boundaries improve safe autonomy.

---

# Authority Boundaries

Scope should reflect the authority granted to the AI system.

An agent may have technical capability to:

* Modify many files.
* Execute commands.
* Delete assets.
* Install dependencies.

Capability does not imply authorization.

Prompts should define necessary authority explicitly.

---

# Least Necessary Authority

AI execution should follow the principle of least necessary authority.

The agent should receive access only to resources necessary for the task where tool controls permit.

This may limit:

* File access.
* Network access.
* Production access.
* Secrets.
* Administrative permissions.

Least authority reduces operational risk.

---

# Destructive Boundaries

Destructive operations require explicit control.

Examples include:

* Deleting files.
* Dropping databases.
* Removing branches.
* Overwriting configuration.
* Rewriting Git history.
* Destroying infrastructure.

Prompts should not imply authorization for destructive actions merely because they simplify task completion.

---

# Protected Assets

Repositories may define protected assets.

Examples include:

```text
Protected:
- docs/architecture/
- SECURITY.md
- production infrastructure
- release workflows
```

Protected assets should not be modified unless the task explicitly includes them.

---

# Generated Files

Generated files may have different modification rules.

Agents should determine whether files are:

* Authoritative source files.
* Generated artifacts.
* Build outputs.
* Vendor-managed assets.

Generated artifacts should generally be updated through their source process rather than edited manually.

---

# Test Boundaries

Tests should remain aligned with task scope.

Agents should:

* Add tests for requested behavior.
* Update tests broken legitimately by approved behavior changes.
* Avoid rewriting unrelated test suites.

Tests should not be modified merely to make incorrect implementation pass.

---

# Validation Boundaries

Agents should not weaken validation to satisfy completion.

Do not:

* Disable tests.
* Suppress analyzers without justification.
* Remove quality gates.
* Relax assertions unnecessarily.
* Bypass security validation.

Validation rules are boundaries, not obstacles.

---

# Documentation Boundaries

Implementation prompts should update documentation only when the implementation changes documented behavior or contracts.

Avoid broad documentation rewrites unrelated to the task.

Documentation changes should remain proportional to engineering impact.

---

# Scope and Architecture Drift

Architecture drift often begins with small boundary violations.

Examples include:

* Adding infrastructure dependencies for convenience.
* Placing business logic in controllers.
* Sharing internal models across modules.
* Introducing generic shared libraries without governance.

Prompts should reinforce boundaries before these patterns accumulate.

---

# Scope and Technical Debt

A scoped task may expose existing technical debt.

The agent should distinguish:

```text
Required to Complete Task
    ↓
May be addressed

Existing but Unrelated Debt
    ↓
Report separately
```

This prevents technical-debt cleanup from silently expanding delivery scope.

---

# Scope and Bug Fixes

Bug fixes should focus on:

* Reproducing the defect.
* Correcting root cause.
* Adding regression protection.

Avoid using bug-fix tasks as justification for broad redesign unless the defect reveals an architectural flaw requiring explicit approval.

---

# Scope and Refactoring

Refactoring prompts should define intended boundaries carefully.

Example:

```text
Refactor only the parsing subsystem.

Preserve:
- Public behavior.
- Public interfaces.
- Serialization format.
- Existing domain semantics.
```

Refactoring scope should be explicit because behavior is intended to remain stable.

---

# Scope and Performance Work

Performance tasks should define which performance characteristic is being optimized.

Example:

```text
Scope:
Reduce allocation rate in MarketDataParser.

Out of scope:
- API redesign.
- Persistence changes.
- Parallelization.
```

Performance optimization should not become unrestricted redesign.

---

# Scope and Security Work

Security tasks may legitimately cross multiple layers.

In such cases, boundaries should still identify:

* Assets affected.
* Trust boundaries.
* Public contracts.
* Deployment implications.
* Approval requirements.

Broad security scope should remain explicit rather than implicit.

---

# Scope and Documentation Tasks

Documentation prompts should identify which documentation authority is affected.

Example:

```text
Update:
docs/architecture/MARKET_DATA.md

Do not modify:
- Runtime implementation.
- API contracts.
- Other architecture documents.
```

Documentation tasks should not silently become implementation tasks.

---

# Scope and Review Tasks

Review prompts may inspect broader scope than they are authorized to modify.

For example:

```text
Review scope:
Entire solution.

Modification authority:
None.

Output:
Findings and recommendations only.
```

Inspection scope and modification scope should be distinguished.

---

# Read Scope vs Write Scope

Prompts may explicitly separate what an agent may inspect from what it may modify.

Example:

```text
Read scope:
- Entire repository.

Write scope:
- src/MarketData/
- tests/MarketData.Tests/
```

This pattern is useful for repository-aware tasks requiring broader understanding.

---

# Execution Scope

Execution scope defines which commands or tools may be used.

For example:

```text
Allowed:
- dotnet restore
- dotnet build
- dotnet test

Do not:
- Publish packages.
- Deploy infrastructure.
- Modify remote repositories.
```

Execution scope becomes important as coding agents gain tool access.

---

# Network Boundaries

Prompts should define whether external network access is necessary.

Tasks should avoid unnecessary external calls, especially when working with:

* Private repositories.
* Sensitive code.
* Regulated data.

Network access should be justified by task requirements.

---

# Environment Boundaries

Prompts should distinguish local, test, staging, and production environments.

Agents should not infer permission to operate against production systems from a development task.

Environment boundaries should remain explicit.

---

# Production Boundary

Production operations should normally require explicit authorization.

Examples include:

* Deployment.
* Configuration changes.
* Data migrations.
* Secret rotation.
* Infrastructure changes.

Development prompts should default to non-production execution.

---

# Temporal Scope

Some tasks may be limited to a release, migration phase, or temporary compatibility period.

Example:

```text
This compatibility behavior applies only to Release 1.x.

Do not redesign the Release 2.0 contract.
```

Temporal boundaries should be documented when they influence engineering decisions.

---

# Scope Traceability

Scope should remain traceable to engineering work.

A useful chain is:

```text
Issue Scope

↓

Prompt Scope

↓

Files Changed

↓

Validation

↓

Pull Request Scope
```

Unexpected divergence should be visible during review.

---

# Scope Verification

After execution, the agent should verify scope compliance.

A completion report may include:

```text
Scope verification:
- Modified only approved projects.
- No new dependencies introduced.
- No public contracts changed.
- No unrelated files modified.
```

Scope validation provides evidence that boundaries were respected.

---

# Change Inventory

Agents should report modified assets.

Useful information includes:

* Files created.
* Files modified.
* Files deleted.
* Dependencies added or removed.
* Public contracts changed.

A change inventory makes scope review easier.

---

# Boundary Violations

If the agent detects that a requested task conflicts with an established boundary, it should report the conflict.

Example:

```text
Requested behavior requires Domain to depend on Infrastructure,
which conflicts with DEPENDENCY_RULES.md.

Execution should not silently violate the architecture.
```

Boundary conflicts are engineering decisions.

---

# Boundary Hierarchy

When boundaries conflict, a useful priority model is:

```text
Safety

↓

Security

↓

Data Integrity

↓

Approved Architecture

↓

Public Contracts

↓

Task Scope

↓

Implementation Convenience
```

Convenience should never override higher-order boundaries.

---

# Scope and Human Approval

Some boundary crossings should require human approval.

Examples include:

* Architecture changes.
* Public API breaking changes.
* Security boundary changes.
* New production dependencies.
* Persistent data migrations.
* Destructive operations.

Approval requirements should be explicit for high-risk prompts.

---

# Scope and AI Autonomy

Boundary rigor should increase as agent autonomy increases.

```text
Advisory AI
    ↓
Mostly informational boundaries

Coding Agent
    ↓
Explicit write boundaries

Tool-Executing Agent
    ↓
File + Command + Permission boundaries

Autonomous Workflow
    ↓
Policy + Approval + Validation boundaries
```

Greater capability requires stronger scope governance.

---

# Scope Profiles

Reusable workflows may define standard scope profiles.

Examples include:

```text
Review Only

Documentation Only

Test Modification

Implementation

Repository Bootstrap

Release Preparation

Production Operation
```

Profiles can standardize permissions and boundaries across prompt collections.

---

# Boundary Documentation

Long-lived boundaries should be documented in authoritative repository assets rather than repeated in every prompt.

Examples include:

* Dependency rules.
* Security policies.
* Module boundaries.
* Data ownership.
* Public API governance.

Prompts should reference these authoritative sources.

---

# Common Scope Anti-Patterns

Avoid:

## Unlimited Scope

```text
Fix everything that looks wrong.
```

## Implicit Write Authority

```text
Review the repository and make any improvements you find.
```

## Silent Architecture Expansion

Adding new modules, abstractions, or dependencies without approval.

## Unrelated Refactoring

Cleaning nearby code because the agent encountered it.

## Validation Weakening

Changing tests or analyzers merely to make execution succeed.

## Production Ambiguity

Allowing a prompt to execute against production without explicit authorization.

## Capability Equals Permission

Assuming that because a tool can perform an operation, the agent is allowed to perform it.

---

# Engineering Recommendations

Prompt authors should:

* Define task scope explicitly.
* Identify plausible out-of-scope work.
* Preserve architectural boundaries.
* Protect public contracts.
* Define dependency permissions.
* Separate read and write scope where useful.
* Limit destructive operations.
* Protect production environments.
* Prefer minimal changes.
* Prevent unrelated refactoring.
* Require reporting before significant scope expansion.
* Define agent decision boundaries.
* Apply least necessary authority.
* Verify scope compliance after execution.
* Scale boundary controls with task risk and agent autonomy.

---

# Success Criteria

A prompt satisfies this guideline when:

* The authorized task scope is clear.
* Relevant repository boundaries are defined.
* Architectural boundaries are preserved.
* Public contracts are protected appropriately.
* Dependency permissions are understood.
* Security-sensitive areas are controlled.
* Unrelated modifications are avoided.
* Significant scope expansion is surfaced.
* Destructive operations require explicit authority.
* Execution results remain easy to review.
* The final change set can be traced back to the original objective.

---

# Related Guidelines

This guideline should be used together with:

* 01-prompt-quality-principles.md
* 02-prompt-clarity.md
* 03-context-management.md
* 05-instruction-design.md
* 06-output-contracts.md
* 07-validation-and-acceptance.md
* 08-error-and-ambiguity-handling.md
* 09-security-and-safety.md
* 10-prompt-review.md

---

# Conclusion

Scope and boundaries define the safe operating area for AI-assisted engineering.

A strong boundary model follows:

```text
Objective

↓

Authorized Scope

↓

Protected Boundaries

↓

Least Necessary Authority

↓

Focused Change

↓

Scope Validation
```

AI systems should not interpret broad technical capability as permission to make broad engineering changes.

The central principle is:

> **An AI agent should change everything necessary to satisfy the approved objective—and nothing else without explicit engineering justification or authorization.**
