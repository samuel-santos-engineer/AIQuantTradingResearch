
# Error and Ambiguity Handling

## Purpose

The Error and Ambiguity Handling guideline defines the engineering principles and practices for detecting, classifying, reporting, containing, and resolving errors, uncertainty, missing information, conflicting instructions, and ambiguous engineering requirements within AI-assisted workflows.

Its purpose is to prevent AI systems from silently converting uncertainty into unsupported engineering decisions.

Errors should remain visible.

Ambiguity should be resolved according to risk.

---

# Objectives

The Error and Ambiguity Handling guideline aims to:

* Prevent unsupported assumptions.
* Detect ambiguous requirements early.
* Standardize error classification.
* Improve failure transparency.
* Preserve engineering intent.
* Prevent silent scope expansion.
* Protect architecture and security boundaries.
* Support safe partial execution.
* Improve recovery.
* Strengthen traceability.
* Enable reliable AI autonomy.

---

# Scope

This guideline applies to prompts used for:

* Software implementation.
* Refactoring.
* Testing.
* Documentation.
* Architecture analysis.
* Repository automation.
* DevOps activities.
* GitHub operations.
* Security engineering.
* Performance engineering.
* Validation.
* Review.
* AI-assisted engineering workflows.

Handling rigor should remain proportional to task complexity, uncertainty, and risk.

---

# Engineering Philosophy

AI systems operate under incomplete information.

The objective is not to eliminate uncertainty.

The objective is to manage uncertainty explicitly.

A useful model is:

```text
Input

↓

Interpretation

↓

Uncertainty Detection

↓

Risk Classification

↓

Resolve / Escalate / Stop

↓

Controlled Execution
```

The dangerous condition is not uncertainty itself.

The dangerous condition is hidden uncertainty.

---

# Error vs Ambiguity

Errors and ambiguity are related but different.

```text
Error
    ↓
A known execution or validation problem occurred.

Ambiguity
    ↓
More than one materially different interpretation is possible.
```

Both require explicit handling.

---

# Error Categories

Engineering errors may be classified as:

```text
Input Error

Context Error

Execution Error

Validation Error

Environment Error

Dependency Error

Permission Error

Security Error

Contract Error

Tool Error
```

Classification helps determine appropriate recovery behavior.

---

# Input Errors

Input errors occur when required task information is invalid or incomplete.

Examples include:

* Invalid file path.
* Missing identifier.
* Unsupported parameter.
* Malformed configuration.
* Invalid version.

Input errors should be surfaced before significant execution.

---

# Context Errors

Context errors occur when required engineering information cannot be used reliably.

Examples include:

* Referenced document does not exist.
* Architecture documentation conflicts.
* Required standard is missing.
* Repository structure differs from expected state.
* Documentation appears stale.

Context errors should not be silently replaced with assumptions.

---

# Execution Errors

Execution errors occur while performing the requested task.

Examples include:

* Compilation failure.
* Script failure.
* File operation failure.
* Command failure.
* Generation failure.

Execution errors should preserve evidence and current state.

---

# Validation Errors

Validation errors occur when generated work fails required checks.

Examples include:

* Test failure.
* Analyzer failure.
* Architecture rule violation.
* Security scan failure.
* Schema validation failure.

Validation failure means the task has not yet satisfied its engineering contract.

---

# Environment Errors

Environment errors arise from execution conditions rather than the implementation itself.

Examples include:

* Missing SDK.
* Missing CLI.
* Unsupported operating system.
* Unavailable local service.
* Incorrect runtime configuration.

Environment errors should be distinguished from implementation defects.

---

# Dependency Errors

Dependency errors include:

* Package restore failure.
* Missing package.
* Version conflict.
* Unavailable external service.
* Missing repository dependency.

Agents should avoid changing dependency policy merely to bypass these errors.

---

# Permission Errors

Permission errors occur when the AI system lacks authority to perform an operation.

Examples include:

* File write denied.
* Repository permission denied.
* Deployment permission denied.
* External service access denied.

Permission failures should not be bypassed through unauthorized alternatives.

---

# Security Errors

Security errors include conditions such as:

* Secret exposure.
* Unauthorized access attempt.
* Security policy violation.
* Unsafe configuration.
* Invalid permission boundary.

Security errors should generally receive high escalation priority.

---

# Contract Errors

Contract errors occur when task requirements conflict with established engineering contracts.

Examples include:

```text
Task:
Modify the public API.

Constraint:
Public API changes are prohibited.
```

or:

```text
Task:
Add direct database access.

Architecture:
Application layer must not depend on Infrastructure.
```

Contract conflicts require engineering resolution.

---

# Tool Errors

Tool errors occur when required tooling fails or behaves unexpectedly.

Examples include:

* CLI crash.
* Agent tool unavailable.
* Invalid tool response.
* Timeout.
* Unsupported operation.

Tool failures should be distinguished from task failure.

---

# Ambiguity Categories

Ambiguity may occur in:

```text
Intent

Terminology

Context

Scope

Architecture

Domain Behavior

Implementation

Validation

Security

Output
```

Different categories require different escalation thresholds.

---

# Intent Ambiguity

Intent ambiguity occurs when the objective itself is unclear.

Example:

```text
Improve market-data handling.
```

Possible interpretations may include:

* Performance.
* Validation.
* Reliability.
* Architecture.
* Logging.

Execution should not begin until significant intent ambiguity is resolved.

---

# Terminology Ambiguity

Terminology ambiguity occurs when words may refer to multiple engineering concepts.

Example:

```text
Update the market-data service.
```

when several services match that description.

The agent should use repository terminology and resolve unclear references before modification.

---

# Context Ambiguity

Context ambiguity occurs when multiple sources provide different guidance.

Example:

```text
Architecture document:
Use event-driven processing.

Current implementation:
Uses synchronous orchestration.
```

The conflict should be surfaced according to context authority rules.

---

# Scope Ambiguity

Scope ambiguity occurs when it is unclear what may be modified.

Example:

```text
Fix the validation problem everywhere.
```

The agent should not interpret broad language as unlimited modification authority.

---

# Architecture Ambiguity

Architecture ambiguity involves structural decisions such as:

* Layer ownership.
* Dependency direction.
* Module boundaries.
* Integration style.
* New abstractions.

Architecture ambiguity should normally require explicit engineering resolution.

---

# Domain Ambiguity

Domain ambiguity affects business meaning.

Examples include:

* Undefined business rule.
* Conflicting domain terminology.
* Unclear validation semantics.
* Missing invariant.

AI systems should not invent business rules to complete implementation.

---

# Implementation Ambiguity

Implementation ambiguity involves low-level technical choices.

Examples include:

* Private method decomposition.
* Local naming.
* Test-data organization.
* Internal helper structure.

These decisions may usually follow existing repository conventions.

---

# Validation Ambiguity

Validation ambiguity occurs when success cannot be measured clearly.

Example:

```text
Make sure performance is better.
```

without defining:

* Metric.
* Workload.
* Baseline.
* Threshold.

The acceptance contract should be clarified before claiming success.

---

# Security Ambiguity

Security ambiguity should receive conservative handling.

Examples include uncertainty about:

* Authorization.
* Credentials.
* Data exposure.
* Permissions.
* Trust boundaries.

Security-sensitive assumptions should not be made silently.

---

# Output Ambiguity

Output ambiguity occurs when expected results are unclear.

Example:

```text
Document the architecture.
```

without identifying:

* Artifact.
* Location.
* Scope.
* Required content.

Significant output ambiguity should be resolved before execution.

---

# Ambiguity Severity

Ambiguity should be classified by engineering impact.

```text
Low

Medium

High

Critical
```

The classification should guide whether the agent may infer, report, or stop.

---

# Low-Severity Ambiguity

Low-severity ambiguity affects local implementation details.

Examples include:

* Private variable naming.
* Test fixture naming.
* Internal helper decomposition.

Recommended behavior:

```text
Follow existing repository convention.
```

No escalation is normally required.

---

# Medium-Severity Ambiguity

Medium-severity ambiguity may affect maintainability or implementation direction but not major contracts.

Examples include:

* Choice between two existing internal abstractions.
* Location of a new internal helper.
* Test organization.

Recommended behavior:

```text
Inspect existing conventions.

Choose the most consistent option.

Document the assumption if materially relevant.
```

---

# High-Severity Ambiguity

High-severity ambiguity may affect:

* Architecture.
* Public contracts.
* Persistent data.
* Business behavior.
* Significant dependencies.

Recommended behavior:

```text
Do not silently decide.

Surface the ambiguity.

Request or identify authoritative resolution.
```

---

# Critical Ambiguity

Critical ambiguity may affect:

* Security boundaries.
* Production systems.
* Data loss.
* Destructive operations.
* Regulatory requirements.

Recommended behavior:

```text
Stop.

Do not execute the risky operation.

Require explicit resolution or authorization.
```

---

# Ambiguity Decision Model

A useful decision model is:

```text
Ambiguity Detected

↓

Does It Affect a Significant Engineering Decision?

No
    ↓
Follow Established Convention

Yes
    ↓
Is an Authoritative Source Available?

Yes
    ↓
Use Authoritative Source

No
    ↓
Surface / Clarify / Stop
```

This model reduces unnecessary interruptions while protecting important decisions.

---

# Assumption Classification

Assumptions may be classified as:

```text
Safe Assumption

Documented Assumption

Approval-Requiring Assumption

Prohibited Assumption
```

This improves consistent agent behavior.

---

# Safe Assumptions

Safe assumptions generally involve low-risk implementation details governed by existing conventions.

Examples include:

* Formatting.
* Private naming.
* Test arrangement.

Safe assumptions do not normally require reporting.

---

# Documented Assumptions

Some assumptions may be reasonable but materially influence implementation.

Example:

```text
Assumption:
The existing validation abstraction remains the intended extension point
because no conflicting architecture guidance was found.
```

These assumptions should be included in completion reporting when relevant.

---

# Approval-Requiring Assumptions

Some decisions should not proceed without engineering approval.

Examples include:

* New architecture.
* Breaking API changes.
* Persistent schema changes.
* New production dependencies.
* Security policy changes.

These should be escalated.

---

# Prohibited Assumptions

AI systems should not assume:

* Production authorization.
* Permission to expose secrets.
* Permission to delete data.
* Permission to weaken security.
* Permission to bypass validation.
* Permission to change business rules.

These require explicit authorization.

---

# Clarification Strategy

Clarification should be targeted.

A good clarification request should identify:

* What is ambiguous.
* Why it matters.
* Available options.
* Consequences when useful.

Avoid vague clarification such as:

```text
Can you provide more information?
```

when the missing decision can be stated precisely.

---

# Clarification Minimalism

Do not ask for clarification when the ambiguity is safely resolved through authoritative context.

Prefer:

```text
Inspect repository standard

↓

Resolve locally
```

over unnecessary interruption.

Clarification should protect meaningful decisions, not replace repository inspection.

---

# Clarification Timing

Significant ambiguity should be resolved before irreversible or high-impact execution.

Minor ambiguity may be resolved during implementation using established conventions.

Timing should reflect risk.

---

# Error Detection

Prompts should encourage early error detection.

A useful workflow is:

```text
Validate Inputs

↓

Validate Context

↓

Validate Preconditions

↓

Execute

↓

Validate Output
```

Earlier detection reduces wasted execution.

---

# Fail Fast

For blocking conditions, agents should fail early.

Examples include:

* Missing required architecture.
* Invalid repository state.
* Required dependency unavailable.
* Security constraint violation.

Fail-fast behavior should preserve useful diagnostic information.

---

# Fail Safe

When failure occurs, the repository or system should remain in a safe state.

Avoid leaving:

* Partially destructive changes.
* Corrupted configuration.
* Disabled validation.
* Exposed secrets.
* Inconsistent generated assets.

Safety is more important than forced completion.

---

# Error Containment

Errors should be contained to the smallest possible execution area.

Incremental execution helps achieve:

```text
Small Change

↓

Validation

↓

Continue or Stop
```

This reduces the blast radius of incorrect assumptions.

---

# Partial Execution

Some workflows can safely continue after a non-blocking failure.

Example:

```text
Documentation update succeeded.

Optional link validation unavailable.
```

The agent may complete independent work while reporting incomplete validation.

---

# Blocking Errors

Blocking errors prevent safe continuation.

Examples include:

* Architecture conflict.
* Security violation.
* Required build failure caused by the change.
* Missing required input.
* Unauthorized operation.

Blocking errors should prevent completion claims.

---

# Non-Blocking Errors

Non-blocking errors may reduce confidence without invalidating the core task.

Examples include:

* Optional tooling unavailable.
* Unrelated pre-existing warning.
* Non-critical documentation link issue.

These should be reported as warnings or observations.

---

# Error Ownership

Agents should attempt to determine whether an error is:

```text
Introduced by Current Work

Pre-Existing

Environmental

External

Unknown
```

Error ownership helps reviewers decide the next action.

---

# Pre-Existing Errors

Pre-existing repository failures should not be hidden.

Example:

```text
Validation:
dotnet test failed.

Assessment:
The same test fails before the current change.

Status:
Pre-existing failure.
```

Evidence should support the classification where practical.

---

# Recovery Strategy

Recoverable errors should define a controlled recovery path.

A useful model is:

```text
Failure

↓

Preserve Evidence

↓

Assess State

↓

Determine Recoverability

↓

Recover or Stop

↓

Revalidate
```

Recovery should not weaken engineering controls.

---

# Retry Behavior

Retries should be appropriate to the failure type.

Reasonable retry candidates include:

* Temporary network failure.
* External service timeout.
* Transient tool error.

Do not repeatedly retry deterministic failures such as:

* Compilation errors.
* Invalid configuration.
* Architecture violations.

---

# Retry Limits

Automated workflows should limit retries.

Unbounded retries may:

* Waste resources.
* Hide persistent failures.
* Delay escalation.

Retry policy should be explicit for autonomous execution.

---

# Rollback

High-risk workflows may require rollback behavior.

Rollback may include:

* Restoring modified files.
* Reverting configuration.
* Returning infrastructure to prior state.

Rollback strategy should be designed before destructive execution where practical.

---

# Preserve Evidence

Failure handling should preserve evidence needed for diagnosis.

Evidence may include:

* Command.
* Error output.
* Validation result.
* Files changed.
* Execution stage.
* Environment information.

Do not remove useful evidence merely to produce a cleaner report.

---

# Error Reporting

A useful error report structure is:

```text
Error Category

Execution Stage

Description

Evidence

Impact

Current State

Recovery Attempted

Recommended Next Action
```

Reports should be concise but actionable.

---

# Error Message Quality

Error messages should explain what failed and why it matters.

Prefer:

```text
Architecture validation failed because MarketData.Application
references MarketData.Infrastructure, violating DEPENDENCY_RULES.md.
```

over:

```text
Architecture check failed.
```

Useful errors accelerate engineering resolution.

---

# Uncertainty Reporting

When certainty is limited, the agent should communicate it explicitly.

Examples include:

```text
Confirmed

Likely

Uncertain

Unable to Verify
```

Do not present inference as verified fact.

---

# Confidence vs Evidence

Agent confidence should not replace engineering evidence.

```text
High Confidence
    ≠
Validated
```

Completion decisions should rely on observable verification.

---

# Ambiguity and Scope

Ambiguity should never be used to justify scope expansion.

If a task could mean:

```text
Fix one validator
```

or:

```text
Refactor the entire validation subsystem
```

the agent should choose the narrower interpretation or seek clarification according to risk.

---

# Ambiguity and Architecture

When architecture intent is unclear, agents should prefer preservation over invention.

A useful rule is:

```text
Preserve Existing Approved Boundary

↓

Surface Need for Architecture Decision
```

Do not create architectural policy implicitly through implementation.

---

# Ambiguity and Domain Behavior

Undefined business behavior should not be guessed.

If requirements do not define whether a condition is valid, invalid, or exceptional, the agent should seek authoritative domain context.

Business semantics belong to the domain, not the agent.

---

# Ambiguity and Security

Security ambiguity should default toward the safer interpretation without silently redesigning security behavior.

When explicit authorization is required, execution should stop.

---

# Ambiguity and Destructive Operations

Destructive actions require explicit intent.

Do not infer permission to:

* Delete.
* Drop.
* Purge.
* Rewrite.
* Destroy.
* Force overwrite.

Ambiguous destructive requests should be clarified before execution.

---

# Ambiguity and Production

Production execution should never be inferred from a generic engineering prompt.

Example:

```text
Deploy the fix.
```

If environment is unspecified and production is possible, the execution target must be resolved before high-impact deployment.

---

# Ambiguity and Dependencies

If a task may require a new dependency, the agent should first inspect:

* Existing capabilities.
* Repository dependency policy.
* Applicable playbooks.

A new dependency should not be introduced solely because it simplifies implementation.

---

# Ambiguity and Validation

If acceptance criteria are ambiguous, the agent should not invent success criteria that materially change the task.

Validation expectations should be derived from:

* Requirements.
* Standards.
* Existing tests.
* Applicable playbooks.

Significant gaps should be surfaced.

---

# Error and Output Contracts

Error behavior should be part of output contracts.

A task may produce:

```text
Completed

Partially Completed

Blocked

Failed
```

Each status should reflect actual execution state.

---

# Error and Acceptance

A task with blocking errors should not be accepted.

A useful model is:

```text
Blocking Error
    ↓
Acceptance = Rejected or Blocked

Non-Blocking Observation
    ↓
Acceptance may still be possible
```

Error severity should influence acceptance explicitly.

---

# Error and Traceability

Errors should remain traceable through:

```text
Task

↓

Execution Stage

↓

Failure

↓

Evidence

↓

Resolution

↓

Revalidation
```

This supports continuous improvement.

---

# Error Learning

Repeated errors should improve engineering assets.

A useful cycle is:

```text
Failure

↓

Root Cause

↓

Missing Rule / Context / Validation Identified

↓

Update Prompt / Playbook / Standard / Test

↓

Prevent Recurrence
```

AI-assisted workflows should learn from execution failures.

---

# Ambiguity Learning

Repeated clarification needs often indicate missing durable documentation.

For example:

```text
Same Architecture Question Repeatedly Appears

↓

Document Architecture Decision

↓

Future Prompts Reference It
```

Repeated ambiguity is a signal of missing engineering knowledge.

---

# Multi-Agent Error Handling

Multi-agent workflows should propagate failures accurately.

An implementation agent should not hide failure from a validation agent.

A validation agent should not convert incomplete evidence into success.

Error state should remain consistent across handoffs.

---

# Error Handoffs

A handoff containing failure should include:

```text
Objective

Work Completed

Failure

Evidence

Current State

Validation Status

Recommended Next Action
```

This allows another agent or engineer to continue safely.

---

# Autonomous Error Handling

Autonomous workflows require stronger error policies.

They should define:

* Retry limits.
* Escalation conditions.
* Stop conditions.
* Rollback behavior.
* Evidence preservation.
* Notification behavior.

Autonomy should increase error governance, not reduce it.

---

# Stop Conditions

High-risk prompts should define explicit stop conditions.

Examples include:

```text
Stop if:
- Required architecture is missing.
- Security validation fails.
- Public contract change becomes necessary.
- Destructive operation is required.
- Production access would be required.
```

Stop conditions prevent uncontrolled execution.

---

# Escalation Conditions

Escalation should occur when the agent encounters decisions outside its authority.

Examples include:

* Architecture change.
* Security exception.
* Business-rule ambiguity.
* New production dependency.
* Data migration.
* Scope expansion.

Escalation preserves human accountability.

---

# Error Handling and Human Approval

Some errors require human judgment rather than technical recovery.

Examples include:

* Conflicting requirements.
* Risk acceptance.
* Architecture trade-off.
* Security exception.
* Production rollback decision.

AI systems should surface these decisions rather than resolve them silently.

---

# Common Error Handling Anti-Patterns

Avoid:

## Silent Failure

Continuing without reporting a failed step.

## Forced Completion

Changing requirements or validation until the task appears successful.

## Assumption Substitution

Inventing missing requirements.

## Infinite Retry

Repeating deterministic failures.

## Error Suppression

Removing warnings or checks to produce success.

## Architecture Guessing

Resolving architecture conflicts through implementation without approval.

## Security Guessing

Assuming permissions or security behavior.

## Failure Misclassification

Calling an implementation defect an environment issue without evidence.

---

# Common Ambiguity Anti-Patterns

Avoid:

## Broad Interpretation

Choosing the largest possible scope.

## Conversation Dependence

Assuming undocumented decisions from previous discussions.

## Undefined Terminology

Using inconsistent names for engineering concepts.

## Hidden Assumption

Making significant decisions without reporting them.

## Premature Clarification

Asking the user about details that can be resolved from authoritative repository context.

## Missing Clarification

Proceeding when multiple high-impact interpretations remain possible.

---

# Engineering Recommendations

Prompt authors should:

* Define expected error behavior.
* Classify significant ambiguity by risk.
* Allow low-risk convention-based decisions.
* Escalate architecture, domain, security, and contract ambiguity.
* Define stop conditions.
* Distinguish blocking and non-blocking failures.
* Preserve error evidence.
* Separate pre-existing and introduced failures.
* Limit retries.
* Define recovery where appropriate.
* Prevent validation bypass.
* Prevent silent scope expansion.
* Require explicit authorization for destructive actions.
* Persist recurring ambiguity resolutions as engineering documentation.
* Scale error controls with AI autonomy.

---

# Success Criteria

A prompt satisfies this guideline when:

* Significant ambiguity can be identified.
* Low-risk ambiguity has a defined resolution strategy.
* High-risk ambiguity triggers escalation.
* Errors remain visible.
* Blocking failures prevent false completion.
* Pre-existing failures can be distinguished from introduced failures.
* Recovery behavior is controlled.
* Retry behavior is bounded.
* Destructive ambiguity requires explicit authorization.
* Security ambiguity is handled conservatively.
* Error evidence is preserved.
* Completion status reflects actual execution state.
* Another engineer or agent can understand what failed, why, and what should happen next.

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
* 09-security-and-safety.md
* 10-prompt-review.md

---

# Conclusion

Errors and ambiguity are unavoidable parts of engineering execution.

Reliable AI-assisted workflows make them explicit.

The core model is:

```text
Detect

↓

Classify

↓

Assess Risk

↓

Resolve Safely

↓

Escalate When Necessary

↓

Preserve Evidence

↓

Revalidate
```

AI systems should be allowed to resolve routine implementation uncertainty using established engineering conventions.

They should not silently resolve uncertainty that changes architecture, business behavior, security boundaries, public contracts, persistent data, or production systems.

The central principle is:

> **When uncertainty affects a significant engineering decision, the correct behavior is not to guess—it is to expose the uncertainty and resolve it through authoritative context or explicit engineering judgment.**
