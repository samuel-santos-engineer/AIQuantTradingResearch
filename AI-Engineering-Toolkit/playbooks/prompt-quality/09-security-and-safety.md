
# Security and Safety

## Purpose

The Security and Safety guideline defines the engineering principles and practices for designing prompts that protect systems, repositories, data, credentials, users, and operational environments during AI-assisted engineering workflows.

Its purpose is to ensure that prompts constrain AI execution according to security requirements, authorization boundaries, risk level, and operational impact.

Security and safety should be treated as explicit prompt design concerns rather than implicit assumptions.

---

# Objectives

The Security and Safety guideline aims to:

* Protect sensitive information.
* Prevent unauthorized operations.
* Limit unnecessary permissions.
* Reduce destructive execution risk.
* Preserve security boundaries.
* Protect production environments.
* Control dependency introduction.
* Improve auditability.
* Support safe AI autonomy.
* Strengthen human approval boundaries.
* Improve secure failure behavior.
* Reduce operational risk.

---

# Scope

This guideline applies to prompts used for:

* Software implementation.
* Refactoring.
* Repository automation.
* Infrastructure changes.
* DevOps workflows.
* GitHub operations.
* Security engineering.
* Data processing.
* Deployment.
* Validation.
* Review.
* Tool execution.
* AI-assisted engineering workflows.

Security controls should remain proportional to task sensitivity, agent authority, and potential impact.

---

# Engineering Philosophy

AI systems should operate under the same security principles applied to other engineering systems.

A useful model is:

```text
Engineering Intent

↓

Explicit Authority

↓

Least Necessary Access

↓

Controlled Execution

↓

Validation

↓

Auditability
```

Capability does not imply authorization.

An AI agent may technically be able to perform an action without being permitted to perform it.

---

# Security by Design

Security requirements should be included when prompts are authored rather than added after execution.

Prompt design should consider:

* Assets at risk.
* Required permissions.
* Trust boundaries.
* Sensitive information.
* Destructive operations.
* External systems.
* Production impact.
* Validation requirements.

Security should influence how the task is executed from the beginning.

---

# Safety by Design

Safety focuses on preventing unintended engineering harm.

Potential harms include:

* Data loss.
* Repository corruption.
* Production outage.
* Security-control bypass.
* Secret exposure.
* Destructive infrastructure changes.
* Uncontrolled dependency installation.
* Irreversible operations.

Prompt instructions should limit both intentional and accidental high-impact behavior.

---

# Asset Identification

High-risk prompts should identify important assets.

Examples include:

* Source code.
* Production data.
* Credentials.
* Repository history.
* Build infrastructure.
* Deployment systems.
* Cloud resources.
* Customer information.
* Security configuration.

Knowing what requires protection improves boundary design.

---

# Trust Boundaries

Prompts should identify relevant trust boundaries where operations cross between:

* Local and remote systems.
* Application and infrastructure.
* Repository and external service.
* Development and production.
* User input and privileged execution.
* AI agent and sensitive resources.

Crossing a trust boundary should be deliberate.

---

# Authorization

Prompts should distinguish technical capability from explicit authorization.

Agents should not infer permission to:

* Deploy.
* Delete.
* Publish.
* Modify permissions.
* Access secrets.
* Change production systems.
* Rewrite Git history.

Authorization should be explicit for high-impact operations.

---

# Least Necessary Authority

AI agents should operate with the minimum authority required to complete the task.

Where tools permit, limit:

* File write access.
* Repository permissions.
* Network access.
* Cloud permissions.
* Production access.
* Secret access.
* Administrative privileges.

Greater capability should not automatically result in broader authority.

---

# Permission Boundaries

Prompts should define allowed operations where relevant.

Example:

```text
Allowed:
- Read repository files.
- Modify src/MarketData/.
- Run local build and tests.

Not allowed:
- Push directly to protected branches.
- Deploy to production.
- Modify cloud resources.
```

Permission boundaries reduce ambiguity.

---

# Read vs Write Authority

Read and write permissions should be distinguished.

Example:

```text
Read:
- Entire repository.

Write:
- src/MarketData/
- tests/MarketData.Tests/
```

Broader inspection does not imply broader modification authority.

---

# Production Boundaries

Production operations require explicit authorization.

Prompts should default to development or non-production environments unless production execution is clearly specified.

Do not infer permission to:

* Deploy production releases.
* Run production migrations.
* Modify production configuration.
* Rotate production secrets.
* Restart production services.

Production should be treated as a protected boundary.

---

# Environment Isolation

Prompts should clearly distinguish:

* Local.
* Development.
* Test.
* Staging.
* Production.

Environment ambiguity is unacceptable for potentially destructive or externally visible operations.

---

# Secret Protection

Prompts should never request unnecessary exposure of:

* Passwords.
* API keys.
* Access tokens.
* Private keys.
* Certificates.
* Connection credentials.
* Signing secrets.

Secrets should be accessed only through approved mechanisms.

---

# Secret Handling

When secrets are necessary for execution, prompts should instruct agents to:

* Avoid displaying them.
* Avoid logging them.
* Avoid writing them to source files.
* Avoid including them in reports.
* Avoid persisting them unnecessarily.

Secret access should remain minimal and task-specific.

---

# Secret Redaction

Outputs should redact sensitive values.

Example:

```text
API_KEY=<redacted>
```

rather than exposing real credentials.

Diagnostic usefulness should not compromise security.

---

# Source Control Protection

Prompts should prevent accidental introduction of secrets into version control.

Agents should inspect suspicious generated content before committing:

* Credentials.
* Tokens.
* Connection strings.
* Private keys.
* Environment files containing secrets.

Security-sensitive configuration should remain externalized.

---

# Sensitive Data

Prompts should minimize access to sensitive data.

Examples include:

* Personal information.
* Financial records.
* Customer data.
* Authentication information.
* Confidential business data.

Only data required for the engineering task should be processed.

---

# Data Minimization

AI workflows should follow the principle:

```text
Use only the minimum data required for the task.
```

Avoid supplying full production datasets when synthetic or reduced examples are sufficient.

---

# Data Masking

When realistic data shape is needed, prompts should prefer:

* Synthetic data.
* Masked data.
* Redacted data.
* Representative test fixtures.

Production-sensitive values should not be exposed unnecessarily.

---

# Destructive Operations

Destructive operations require explicit authorization.

Examples include:

* Delete files.
* Drop databases.
* Remove infrastructure.
* Purge queues.
* Rewrite history.
* Force reset branches.
* Remove repositories.
* Overwrite production configuration.

Destructive operations should never be inferred from broad language such as:

```text
Clean everything up.
```

---

# Destructive Operation Gate

A high-risk workflow should follow:

```text
Identify Destructive Action

↓

Confirm Explicit Authorization

↓

Validate Target

↓

Validate Backup / Recovery Path

↓

Execute

↓

Verify Outcome
```

Where explicit authorization is absent, execution should stop.

---

# Reversible Changes

Prefer reversible operations where practical.

Examples include:

* Additive migrations before destructive cleanup.
* Feature flags.
* New configuration before removing old configuration.
* Branch-based changes rather than direct destructive edits.

Reversibility reduces operational risk.

---

# Backup and Recovery

Prompts involving destructive or irreversible changes should consider:

* Backup.
* Snapshot.
* Rollback.
* Recovery.
* Restore validation.

A destructive action without a recovery model may require explicit engineering review.

---

# Security Controls

AI agents should never weaken security controls merely to complete a task.

Do not:

* Disable authentication.
* Relax authorization.
* Disable certificate validation.
* Remove encryption.
* Disable security scanning.
* Suppress security warnings.
* Bypass access controls.

Security controls are boundaries, not obstacles.

---

# Validation Controls

Agents should not disable required validation to achieve successful execution.

Avoid:

* Skipping tests.
* Removing assertions.
* Suppressing analyzers.
* Ignoring policy checks.
* Disabling dependency scanning.

A failing security or safety validation result should remain visible.

---

# Dependency Security

Prompts should constrain dependency introduction.

Before adding a dependency, agents should consider:

* Necessity.
* Trusted source.
* Maintenance health.
* Security history.
* License.
* Transitive dependencies.
* Existing alternatives.

Dependencies expand the software supply chain.

---

# Dependency Installation

Agents should not install arbitrary packages solely because they simplify implementation.

Example:

```text
Do not introduce a new package unless:
- Existing capabilities are insufficient.
- The dependency is justified.
- Repository dependency policy is satisfied.
```

High-risk dependency changes may require approval.

---

# External Code

Prompts should be cautious when using external scripts, packages, or copied code.

Agents should avoid blindly executing or importing:

* Unknown scripts.
* Unverified binaries.
* Untrusted actions.
* Arbitrary remote commands.

External code should be treated as untrusted until reviewed.

---

# Network Access

Network access should be limited to task requirements.

Prompts should avoid unnecessary:

* External API calls.
* Downloads.
* Package installation.
* Remote command execution.

Network operations may increase supply-chain and data-exposure risk.

---

# Remote Execution

Remote execution should require explicit context and authority.

Examples include:

* SSH.
* Cloud shell.
* Remote PowerShell.
* Production deployment tools.

Agents should not infer remote execution permission from local implementation tasks.

---

# Shell Commands

Prompts allowing shell execution should discourage unsafe commands.

Potentially dangerous patterns include:

* Recursive deletion.
* Force operations.
* Shell pipelines executing downloaded content.
* Privilege escalation.
* History rewriting.

Commands should remain reviewable and proportional to the task.

---

# Privilege Escalation

Agents should not elevate privileges automatically.

Examples include:

* Administrator execution.
* sudo.
* Cloud role escalation.
* Repository-admin permissions.

If elevated privileges are necessary, the requirement should be surfaced.

---

# Git Safety

Prompts involving Git should protect repository history.

High-risk commands include:

* force push.
* hard reset.
* rebase of shared branches.
* branch deletion.
* history rewrite.

These operations should require explicit authorization where they could affect shared work.

---

# Protected Branches

Agents should respect protected branch workflows.

Prefer:

```text
Branch

↓

Commit

↓

Pull Request

↓

Review

↓

Merge
```

over direct modification of protected branches.

---

# Release Safety

Publishing artifacts is an externally visible operation.

Prompts should distinguish:

* Build.
* Package.
* Stage.
* Publish.
* Release.

Permission to build does not imply permission to publish.

---

# Deployment Safety

Deployment workflows should separate:

```text
Prepare

Validate

Approve

Deploy

Verify
```

High-risk environments may require human approval between validation and deployment.

---

# Infrastructure Safety

Infrastructure changes may affect shared or production resources.

Prompts should define:

* Target environment.
* Allowed resources.
* Change scope.
* Validation.
* Approval boundary.
* Rollback strategy.

Infrastructure automation should never rely on implicit target selection.

---

# Database Safety

Database operations require special care.

High-risk operations include:

* Schema deletion.
* Data migration.
* Bulk updates.
* Production queries.
* Data cleanup.

Prompts should preserve:

* Data integrity.
* Backup strategy.
* Transaction safety.
* Rollback where practical.

---

# Migration Safety

Data migrations should define:

* Source state.
* Target state.
* Compatibility expectations.
* Validation.
* Rollback or recovery.
* Production approval.

Migration execution should not be inferred from migration generation.

---

# Security-Sensitive Code

Changes involving the following should receive stronger controls:

* Authentication.
* Authorization.
* Cryptography.
* Secret handling.
* Identity.
* Permissions.
* Data protection.
* Security configuration.

AI-generated security code should receive appropriate independent review.

---

# Cryptography

Prompts should discourage custom cryptographic implementations.

Prefer established platform capabilities and approved libraries.

Agents should not invent:

* Encryption algorithms.
* Key derivation schemes.
* Signing protocols.
* Authentication protocols.

unless the task explicitly concerns reviewed cryptographic research.

---

# Authentication

Authentication changes should preserve established identity flows.

Agents should not:

* Disable verification.
* Accept unsigned tokens.
* Remove credential checks.
* Bypass identity providers.

Authentication changes should be validated explicitly.

---

# Authorization

Authorization changes should protect least privilege.

Agents should not infer that:

```text
Authenticated
```

means:

```text
Authorized for every operation
```

Authorization should remain resource- and operation-specific.

---

# Secure Defaults

Prompts should prefer secure defaults.

Examples include:

* Deny by default.
* Validate by default.
* Encrypt where required.
* Restrict permissions.
* Avoid exposing internal details.

Safe behavior should not require extra effort from users.

---

# Input Safety

External input should be treated as untrusted.

Prompts should require appropriate validation for:

* User input.
* File paths.
* URLs.
* Shell input.
* API data.
* Configuration.
* External service responses.

Untrusted input should not become executable instructions.

---

# Injection Safety

Prompt-generated implementations should protect against injection risks such as:

* SQL injection.
* Command injection.
* Path traversal.
* Template injection.
* Query manipulation.

Agents should prefer safe abstractions and parameterized APIs.

---

# Path Safety

File-system operations should validate targets.

Agents should avoid:

* Uncontrolled relative paths.
* Traversal outside allowed directories.
* Overwriting protected files.
* Following unsafe links unintentionally.

Repository scope should constrain file operations.

---

# Logging Safety

Logs must not expose:

* Secrets.
* Credentials.
* Tokens.
* Sensitive personal data.
* Confidential payloads.

Security-sensitive diagnostics should remain useful without leaking protected information.

---

# Error Safety

Error messages should avoid exposing:

* Internal paths.
* Stack traces to untrusted consumers.
* Database internals.
* Credentials.
* Security configuration.

Detailed diagnostics should remain within protected operational channels.

---

# Output Safety

Prompt outputs should be reviewed for sensitive information.

This includes:

* Completion reports.
* Validation logs.
* Generated documentation.
* Structured data.
* Agent handoffs.

Security requirements apply to output contracts.

---

# Prompt Injection Awareness

AI systems may encounter untrusted text in:

* Repository files.
* Issues.
* Documentation.
* Logs.
* External webpages.
* Generated artifacts.

Untrusted content should not automatically gain instruction authority.

---

# Instruction Authority

Agents should distinguish:

```text
Authoritative Prompt Instructions

from

Untrusted Content Being Processed
```

Text inside a file, issue, or webpage should not override higher-authority engineering instructions merely because it contains imperative language.

---

# Untrusted Repository Content

Repositories may contain:

* Generated files.
* Third-party documentation.
* User-provided content.
* Historical instructions.

Agents should use context authority rules before treating embedded text as execution guidance.

---

# External Content Safety

External sources should be considered untrusted unless explicitly selected as authoritative.

External content should not automatically:

* Change scope.
* Authorize tool usage.
* Override security rules.
* Request secrets.
* Trigger destructive operations.

---

# Human Approval Boundaries

High-risk operations should require explicit human approval.

Examples include:

* Production deployment.
* Destructive migration.
* Security-policy changes.
* Permission elevation.
* Secret rotation.
* Public breaking changes.
* Significant infrastructure deletion.

Prompt design should identify these boundaries before execution.

---

# Approval vs Confirmation

Approval should be meaningful.

A generic instruction to:

```text
Handle everything.
```

should not be interpreted as approval for unrelated high-risk operations.

Approval should match the operation's scope and impact.

---

# Stop Conditions

Security-sensitive prompts should define stop conditions.

Examples include:

```text
Stop if:
- A secret would need to be exposed.
- Production access is required but not authorized.
- Security validation fails.
- Destructive action becomes necessary.
- Privilege escalation is required.
```

Stop conditions prevent uncontrolled escalation.

---

# Safe Failure

If execution cannot continue securely, the agent should fail safely.

Safe failure means:

* No security control is bypassed.
* No sensitive data is exposed.
* No destructive workaround is attempted.
* Failure evidence is preserved.
* Required next action is reported.

Security should take precedence over completion.

---

# Partial Completion

Safe independent work may continue when a security-sensitive step is blocked, provided:

* No unsafe assumptions are made.
* Blocked work is clearly reported.
* Partial work does not create an insecure intermediate state.

Partial completion should remain transparent.

---

# Security Validation

Security-sensitive prompts should define relevant validation.

Examples include:

* Authentication tests.
* Authorization tests.
* Secret scanning.
* Dependency scanning.
* Static analysis.
* Permission validation.
* Configuration validation.

Security validation should correspond to the risk introduced.

---

# Safety Validation

Safety validation may include:

* Scope verification.
* Dry-run results.
* Backup verification.
* File change inventory.
* Environment verification.
* Rollback readiness.

Safety validation helps verify that execution remained controlled.

---

# Dry Run

High-risk workflows may benefit from a dry-run mode.

A dry run may produce:

* Planned changes.
* Target resources.
* Expected commands.
* Risk assessment.
* Validation plan.

Dry run should not perform the high-impact action itself.

---

# Preview Before Apply

A useful safety pattern is:

```text
Inspect

↓

Plan

↓

Preview

↓

Approve

↓

Apply

↓

Validate
```

This pattern is particularly valuable for infrastructure, migration, and deployment tasks.

---

# Rollback Strategy

Prompts should define rollback when failure could leave a harmful state.

Rollback should be:

* Feasible.
* Tested where critical.
* Proportional to risk.
* Documented.

Not every task requires rollback, but irreversible work requires stronger consideration.

---

# Auditability

High-impact AI actions should be traceable.

Audit information may include:

* Task objective.
* Agent actions.
* Commands executed.
* Files changed.
* Resources affected.
* Validation results.
* Approval points.
* Outcome.

Auditability supports incident investigation and governance.

---

# Change Inventory

Security-sensitive completion reports should include an accurate change inventory.

Examples include:

* Files modified.
* Permissions changed.
* Dependencies introduced.
* Infrastructure affected.
* Configuration changed.
* Secrets referenced.

Unexpected changes should be easy to identify.

---

# Security Observations

Agents may identify unrelated security concerns.

Recommended behavior:

```text
Report security concern.

Do not remediate outside task scope unless explicitly authorized.
```

Critical immediate risks may require escalation, but discovery should not silently expand scope.

---

# Vulnerability Discovery

When a likely vulnerability is discovered, the agent should:

* Preserve evidence.
* Avoid exploitation beyond what is required for validation.
* Report impact.
* Avoid exposing sensitive details unnecessarily.
* Follow the authorized remediation scope.

Security investigation should remain controlled.

---

# Third-Party Risk

Prompts should consider risk introduced by:

* Packages.
* GitHub Actions.
* Container images.
* External scripts.
* SaaS integrations.
* Cloud services.

Third-party convenience does not eliminate security responsibility.

---

# Supply Chain Safety

Software supply chain controls may include:

* Trusted package sources.
* Version pinning.
* Dependency scanning.
* Artifact verification.
* Signed releases.
* Provenance.

Prompt workflows should preserve existing supply chain controls.

---

# AI Autonomy and Security

Security requirements should increase as AI autonomy increases.

```text
Advisory AI
    ↓
Information Security

Coding Agent
    ↓
Repository + Dependency Controls

Tool-Executing Agent
    ↓
Permission + Command + Environment Controls

Autonomous Workflow
    ↓
Policy Gates
+ Approval Boundaries
+ Continuous Validation
+ Auditability
```

Greater autonomy requires stronger governance.

---

# Risk Classification

Prompt authors may classify tasks according to risk.

Example:

```text
Low Risk
- Documentation updates.
- Local test generation.

Medium Risk
- Repository implementation changes.
- Dependency updates.

High Risk
- Authentication changes.
- Infrastructure modifications.
- Database migrations.

Critical Risk
- Production destructive operations.
- Credential exposure.
- Security-control bypass.
```

Risk classification helps determine required controls.

---

# Risk-Based Controls

Controls should scale with risk.

```text
Low Risk
    ↓
Scope + Validation

Medium Risk
    ↓
Scope + Validation + Review

High Risk
    ↓
Strong Boundaries
+ Independent Validation
+ Approval

Critical Risk
    ↓
Explicit Authorization
+ Human Control
+ Recovery Plan
+ Audit Trail
```

Security governance should remain proportional.

---

# Separation of Duties

High-risk workflows may separate responsibilities.

Example:

```text
Agent A
Prepares change

Agent B
Reviews security impact

Automation
Validates

Human
Approves execution
```

Separation of duties reduces correlated error.

---

# Multi-Agent Safety

Multiple agents should share consistent security boundaries.

One agent should not expand authority granted to another.

Handoffs should include:

* Scope.
* Permission limits.
* Security constraints.
* Validation state.
* Approval requirements.

Security context must survive orchestration.

---

# Agent Handoff Security

Handoffs should avoid including:

* Secrets.
* Sensitive tokens.
* Production credentials.
* Unnecessary confidential data.

Pass references to approved secret mechanisms rather than raw secret values.

---

# Tool Independence

Security principles should not depend on one AI product.

The same concepts should apply across:

* Coding agents.
* IDE assistants.
* CI agents.
* Cloud agents.
* Future autonomous systems.

Specific permission mechanisms may vary, but the security model should remain stable.

---

# Security Policy Authority

Prompts should reference authoritative security policies rather than invent local alternatives.

Possible sources include:

* Repository security standards.
* Organizational policy.
* Architecture security documentation.
* Secure coding playbooks.

Higher-authority policy should override agent preference.

---

# Policy Conflict

If requested work conflicts with security policy, the agent should surface the conflict.

Example:

```text
Requested change requires storing a production token in source control,
which conflicts with repository security policy.
```

Execution should not silently violate policy.

---

# Security Exceptions

Security exceptions require explicit engineering authority.

AI agents should not approve exceptions for themselves.

Exceptions should document:

* Requirement.
* Risk.
* Mitigation.
* Duration.
* Approval.

Temporary exceptions should not silently become permanent architecture.

---

# Common Security Anti-Patterns

Avoid:

## Secret Exposure

Including credentials in prompts, logs, or output.

## Capability Equals Authorization

Assuming tool access means permission.

## Security Bypass

Disabling controls to complete execution.

## Production Assumption

Operating against production without explicit authorization.

## Untrusted Execution

Executing unknown remote scripts or binaries.

## Dependency Convenience

Adding arbitrary packages without review.

## Unlimited Permissions

Granting broad privileges for simple tasks.

## Hidden Destructive Actions

Deleting or overwriting resources as incidental implementation.

---

# Common Safety Anti-Patterns

Avoid:

## No Rollback

Performing irreversible high-risk changes without recovery consideration.

## Ambiguous Environment

Running changes without confirming target environment.

## Scope Expansion

Modifying unrelated security or infrastructure assets.

## Validation Suppression

Removing failing safety checks.

## Blind Automation

Executing high-impact operations without preview or approval.

## Unsafe Partial State

Leaving systems in inconsistent or insecure intermediate conditions.

---

# Engineering Recommendations

Prompt authors should:

* Identify security-sensitive assets.
* Define execution authority explicitly.
* Apply least necessary authority.
* Separate read and write permissions.
* Protect production boundaries.
* Prevent unnecessary secret exposure.
* Require explicit authorization for destructive operations.
* Prefer reversible changes.
* Define stop conditions.
* Preserve security controls.
* Control dependency introduction.
* Treat external content as untrusted.
* Protect against prompt-injection-style instruction authority confusion.
* Use independent validation for high-risk changes.
* Define human approval gates.
* Preserve auditability.
* Scale controls with AI autonomy and risk.

---

# Success Criteria

A prompt satisfies this guideline when:

* Agent authority is clear.
* Permissions are proportional to the task.
* Sensitive information is protected.
* Production access is controlled explicitly.
* Destructive operations require explicit authorization.
* Security controls cannot be silently bypassed.
* Dependency changes are governed.
* External content cannot override authoritative instructions.
* Security-sensitive ambiguity causes escalation.
* High-risk operations have appropriate validation.
* Human approval boundaries are explicit where required.
* Failure leaves the system in a safe state.
* Significant actions remain traceable.
* Another engineer can determine what authority was granted and whether it was respected.

---

# Related Guidelines

This guideline should be used together with:

* 01-prompt-quality-principles.md
* 02-prompt-clarity.md
* 03-context-management.md
* 04-scope-and-boundaries.md
* 05-instruction-design.md
* 06-output-contracts.md
* 07-validation-and-acceptance.md
* 08-error-and-ambiguity-handling.md
* 10-prompt-review.md

---

# Conclusion

Security and safety define the limits within which AI-assisted engineering may operate.

A strong execution model follows:

```text
Explicit Intent

↓

Explicit Authority

↓

Least Necessary Access

↓

Protected Boundaries

↓

Controlled Execution

↓

Security Validation

↓

Auditable Outcome
```

AI systems should never interpret technical capability as unlimited engineering authority.

The central principle is:

> **AI-assisted engineering is safe only when the system clearly understands what it may do, what it must protect, what requires approval, and when it must stop.**
