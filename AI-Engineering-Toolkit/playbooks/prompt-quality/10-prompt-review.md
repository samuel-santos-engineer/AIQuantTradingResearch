
# Prompt Review

## Purpose

The Prompt Review guideline defines the engineering principles and practices for evaluating the quality, safety, maintainability, and execution readiness of prompts within the AI Engineering Toolkit.

Its purpose is to provide a consistent review methodology that verifies whether a prompt is sufficiently clear, contextualized, bounded, structured, verifiable, secure, and maintainable before it is used in repeatable AI-assisted engineering workflows.

Prompt Review is the final quality gate of the Prompt Quality Guidelines.

---

# Objectives

The Prompt Review guideline aims to:

* Standardize prompt quality assessment.
* Verify engineering intent.
* Detect ambiguity.
* Evaluate context quality.
* Verify execution boundaries.
* Assess instruction quality.
* Validate output contracts.
* Review acceptance criteria.
* Evaluate failure handling.
* Assess security and safety.
* Improve maintainability.
* Support reusable prompt governance.
* Enable continuous prompt improvement.

---

# Scope

This guideline applies to prompts used for:

* Software implementation.
* Refactoring.
* Testing.
* Documentation.
* Architecture analysis.
* Repository automation.
* DevOps operations.
* GitHub workflows.
* Security engineering.
* Performance engineering.
* Validation.
* Review.
* Multi-agent workflows.
* AI-assisted engineering automation.

Review depth should remain proportional to prompt reuse, execution authority, and engineering risk.

---

# Engineering Philosophy

A prompt should not be considered production-ready merely because it appears clear to its author.

Prompt quality should be evaluated through structured review.

A useful model is:

```text
Prompt

↓

Quality Review

↓

Findings

↓

Correction

↓

Validation

↓

Approval

↓

Execution
```

Review reduces the gap between author intent and agent interpretation.

---

# Prompt Review as Engineering Governance

Prompt Review should evaluate prompts as engineering artifacts.

Review should consider:

* Intent.
* Context.
* Boundaries.
* Instructions.
* Outputs.
* Validation.
* Error handling.
* Security.
* Maintainability.
* Traceability.

The goal is not stylistic perfection.

The goal is reliable engineering execution.

---

# Relationship to Prompt Validation

Prompt Review and Prompt Validation serve different purposes.

```text
Prompt Review
    ↓
Evaluates design quality

Prompt Validation
    ↓
Evaluates execution behavior
```

A prompt may look well designed but behave poorly in execution.

Likewise, one successful execution does not prove long-term prompt quality.

Both activities are required for important reusable prompts.

---

# Review Lifecycle

A mature Prompt Review follows:

```text
Prepare

↓

Inspect Metadata

↓

Review Intent

↓

Review Context

↓

Review Scope

↓

Review Instructions

↓

Review Outputs

↓

Review Validation

↓

Review Failure Handling

↓

Review Security

↓

Review Maintainability

↓

Record Findings

↓

Approve or Revise
```

Each stage contributes to execution confidence.

---

# Review Scope

Before review begins, identify:

* Prompt purpose.
* Intended users.
* Target agent type.
* Expected execution authority.
* Repository context.
* Risk level.
* Reuse level.

Review expectations should reflect the prompt's actual use.

---

# Review Levels

Prompt reviews may use different levels.

```text
Lightweight Review

Standard Review

High-Risk Review

Critical Review
```

The review model should remain proportional rather than uniform.

---

# Lightweight Review

Suitable for:

* Low-risk advisory prompts.
* One-time documentation tasks.
* Non-destructive analysis.

Review focuses on:

* Intent.
* Clarity.
* Output expectations.

---

# Standard Review

Suitable for:

* Reusable engineering prompts.
* Repository-aware coding tasks.
* Test generation.
* Documentation generation.

Review should cover all Prompt Quality Guidelines.

---

# High-Risk Review

Suitable for:

* Security-sensitive code.
* Infrastructure changes.
* Data migrations.
* Dependency changes.
* Public contract changes.

High-risk review should include:

* Strong boundary assessment.
* Security review.
* Validation review.
* Human approval points.

---

# Critical Review

Suitable for prompts capable of:

* Production modification.
* Destructive operations.
* Privilege changes.
* Secret handling.
* Large-scale autonomous execution.

Critical prompts should receive explicit human review and independent validation before use.

---

# Review Area 1 — Prompt Intent

Verify that the prompt clearly communicates:

* Problem.
* Objective.
* Expected engineering outcome.

Ask:

> Can another engineer understand what success means without reconstructing hidden context?

If not, the prompt should be revised.

---

# Review Area 2 — Prompt Clarity

Evaluate whether instructions are:

* Clear.
* Precise.
* Consistent.
* Understandable.
* Free from contradictory language.

Look for:

* Vague verbs.
* Undefined terminology.
* Ambiguous references.
* Subjective quality terms.

Clarity should reduce materially different interpretations.

---

# Review Area 3 — Context Quality

Evaluate whether required context is:

* Available.
* Relevant.
* Authoritative.
* Current.
* Minimal enough to remain focused.

Reviewers should identify:

* Missing context.
* Stale context.
* Conflicting context.
* Excessive context.

---

# Context Authority Review

Verify that the prompt distinguishes authoritative engineering sources from lower-authority context.

A useful hierarchy is:

```text
Task Requirements

↓

Architecture

↓

Standards

↓

Playbooks

↓

Repository Documentation

↓

Implementation Patterns

↓

Agent Preference
```

Agent preference should never silently override authoritative engineering guidance.

---

# Review Area 4 — Scope

Verify that the prompt defines:

* What is in scope.
* What is out of scope.
* What may be modified.
* What must remain unchanged.

Ask:

> Could the agent reasonably interpret this task more broadly than intended?

If yes, scope requires strengthening.

---

# Boundary Review

Evaluate boundaries involving:

* Architecture.
* Public contracts.
* Dependencies.
* Security.
* Data.
* Infrastructure.
* Production systems.

Significant boundaries should be explicit.

---

# Review Area 5 — Instruction Design

Evaluate whether instructions are:

* Structured.
* Sequenced where necessary.
* Decomposed appropriately.
* Consistent.
* Proportional to task complexity.

Look for:

* Hidden workflow.
* Overly broad instructions.
* Micromanagement.
* Contradictory sequencing.
* Missing decision points.

---

# Instruction Authority Review

Verify that the prompt distinguishes:

* Agent decisions.
* Human approval decisions.

The agent should not silently receive authority for:

* Architecture changes.
* Security exceptions.
* Production operations.
* Destructive actions.

---

# Review Area 6 — Output Contracts

Verify that expected outputs are explicit.

Review:

* Required files.
* Required behaviors.
* Structured outputs.
* Reports.
* Completion status.
* Evidence.

Ask:

> Can a reviewer determine objectively what successful execution should leave behind?

---

# Output Scope Review

Ensure outputs do not conflict with defined scope.

Example conflict:

```text
Scope:
Do not modify API contracts.

Output:
Create a new incompatible API response schema.
```

Output contracts should reinforce boundaries.

---

# Review Area 7 — Validation

Evaluate whether the prompt defines sufficient validation.

Validation may include:

* Build.
* Tests.
* Static analysis.
* Architecture checks.
* Security checks.
* Documentation validation.
* Performance checks.

Validation should reflect task risk.

---

# Acceptance Criteria Review

Verify that acceptance criteria are:

* Specific.
* Observable.
* Verifiable.
* Relevant.

Avoid criteria such as:

```text
Looks good.
```

or:

```text
Works correctly.
```

without evidence.

---

# Evidence Review

Review whether completion claims can be supported through objective evidence.

Prompt design should distinguish:

```text
Generated

Executed

Validated

Accepted
```

These states should not be conflated.

---

# Review Area 8 — Error Handling

Evaluate whether the prompt defines behavior for:

* Missing context.
* Tool failure.
* Validation failure.
* Environment failure.
* Partial completion.
* Blocked execution.

Errors should remain visible.

---

# Ambiguity Handling Review

Verify that ambiguity is classified appropriately.

Low-risk ambiguity may follow conventions.

High-risk ambiguity should trigger escalation.

Reviewers should verify that prompts do not encourage silent assumptions involving:

* Architecture.
* Security.
* Business behavior.
* Public contracts.
* Persistent data.
* Production systems.

---

# Stop Condition Review

High-risk prompts should define stop conditions.

Examples include:

* Security failure.
* Required architecture missing.
* Destructive operation required.
* Production access required without authorization.
* Scope expansion required.

Stop conditions improve safe autonomy.

---

# Review Area 9 — Security

Evaluate whether the prompt protects:

* Secrets.
* Credentials.
* Sensitive data.
* Security controls.
* Permissions.
* Trust boundaries.

Prompt design should not request unnecessary sensitive access.

---

# Safety Review

Evaluate operational safety.

Consider:

* Destructive actions.
* Reversibility.
* Environment selection.
* Production impact.
* Rollback.
* Scope expansion.
* Privilege escalation.

Safety should be proportional to execution authority.

---

# Prompt Injection Review

Review whether untrusted content could be interpreted as authoritative instructions.

Potential sources include:

* Repository files.
* Issues.
* Logs.
* Web content.
* Generated documents.

The prompt should preserve instruction authority.

---

# Dependency Review

If the prompt may introduce dependencies, verify that it includes appropriate controls.

Review:

* Necessity.
* Security.
* Maintenance.
* Existing alternatives.
* Approval requirements.

Dependency introduction should not be incidental.

---

# Review Area 10 — Maintainability

Evaluate whether the prompt is understandable and maintainable over time.

Look for:

* Repeated standards.
* Hardcoded obsolete paths.
* Hidden conversation dependencies.
* Tool-specific assumptions.
* Large accumulated exceptions.

Reusable prompts should remain easy to evolve.

---

# Duplication Review

Prompt review should identify duplicated engineering guidance.

Prefer:

```text
Reference:
playbooks/dotnet/08-testing.md
```

over copying the entire playbook into the prompt.

Authoritative knowledge should have one primary source.

---

# Tool Independence Review

Verify that tool-specific instructions are necessary.

Core engineering behavior should remain understandable even if:

* IDE changes.
* AI model changes.
* Coding agent changes.

Tool-specific behavior should be isolated when possible.

---

# Model Independence Review

Prompt quality should not rely unnecessarily on undocumented model behavior.

Reliability should come from:

* Context.
* Constraints.
* Structured instructions.
* Validation.
* Evidence.

---

# Idempotency Review

For repeatable automation, verify whether repeated execution is safe.

Look for risks such as:

* Duplicate files.
* Duplicate configuration.
* Duplicate project assets.
* Repeated resource creation.

Idempotency should be explicit where relevant.

---

# Traceability Review

Significant prompts should support traceability.

A useful chain is:

```text
Issue

↓

Prompt

↓

Playbook

↓

Implementation

↓

Validation

↓

Pull Request
```

Reviewers should confirm that prompt execution can be connected to engineering work.

---

# Prompt Metadata Review

If prompt metadata is required, verify:

* Prompt ID.
* Version.
* Status.
* Owner.
* Category.
* Dependencies.
* Lifecycle status.

Metadata should reflect the actual prompt state.

---

# Prompt Lifecycle Review

Verify whether the prompt is appropriately classified as:

* Draft.
* Review.
* Validated.
* Approved.
* Published.
* Deprecated.

Lifecycle status should match evidence.

---

# Review Evidence

Prompt review findings should be evidence-based.

A useful finding structure is:

```text
Area

Observation

Evidence

Impact

Recommendation
```

Avoid vague review feedback.

---

# Finding Severity

Findings may be classified as:

```text
Critical

High

Medium

Low

Recommendation
```

Severity should reflect execution risk and impact.

---

# Critical Findings

Critical findings may include:

* Secret exposure.
* Unbounded destructive authority.
* Production execution ambiguity.
* Security-control bypass.
* Missing approval for critical operations.

Critical findings should block prompt approval.

---

# High Findings

High findings may include:

* Architecture ambiguity.
* Undefined scope for broad repository modification.
* Missing validation for significant implementation.
* Public contract ambiguity.
* Unsafe dependency behavior.

High findings should normally block approval.

---

# Medium Findings

Medium findings may include:

* Weak output structure.
* Missing non-critical context.
* Maintainability concerns.
* Insufficient reporting.

These should generally be corrected before publication of reusable prompts.

---

# Low Findings

Low findings may include:

* Minor terminology inconsistencies.
* Redundant wording.
* Small structural improvements.

These may not block approval.

---

# Review Outcome

Prompt Review should produce one of the following outcomes:

```text
Approved

Approved with Recommendations

Changes Required

Rejected
```

Outcome should reflect unresolved findings.

---

# Approved

Use when:

* Required quality standards are satisfied.
* No blocking findings remain.
* Validation plan is appropriate.
* Security boundaries are adequate.

---

# Approved with Recommendations

Use when:

* Prompt is safe and usable.
* Only non-blocking improvements remain.
* Recommendations are documented.

---

# Changes Required

Use when:

* Significant quality gaps remain.
* Prompt can be corrected without fundamental redesign.

---

# Rejected

Use when:

* Prompt objective is fundamentally unsafe.
* Requirements are contradictory.
* Execution cannot be bounded appropriately.
* Prompt design is unsuitable for intended use.

---

# Review Checklist

A Prompt Review should answer:

```text
Intent
- Is the objective explicit?

Clarity
- Are instructions unambiguous?

Context
- Is required context authoritative and current?

Scope
- Are allowed changes bounded?

Instructions
- Is execution structured appropriately?

Outputs
- Are expected outputs defined?

Validation
- Can success be verified?

Ambiguity
- Are significant assumptions controlled?

Security
- Are sensitive assets protected?

Safety
- Are destructive and production actions governed?

Maintainability
- Is the prompt reusable and understandable?

Traceability
- Can execution be connected to engineering work?
```

---

# Prompt Review and Testing

Review should identify what prompt testing is required.

Representative execution may verify:

* Scope compliance.
* Instruction interpretation.
* Output contract.
* Failure handling.
* Validation behavior.
* Repeatability.

Review evaluates design.

Testing evaluates behavior.

---

# Prompt Review and AI-Assisted Review

AI systems may assist Prompt Review by identifying:

* Ambiguity.
* Missing context.
* Contradictions.
* Weak scope.
* Missing validation.
* Security concerns.
* Duplicated instructions.

AI review should provide evidence and recommendations rather than automatic approval.

---

# Independent Review

High-impact prompts should benefit from independent review.

A useful pattern is:

```text
Author

↓

Review Agent / Engineer

↓

Execution Validation

↓

Approval
```

Independent review reduces author blind spots.

---

# Human Review

Human review should be required when prompts control high-risk operations.

Examples include:

* Production changes.
* Security controls.
* Data migrations.
* Destructive operations.
* Infrastructure deletion.
* Privilege changes.

Human accountability should remain explicit.

---

# Review Frequency

Reusable prompts should be reviewed:

* Before publication.
* After significant modification.
* After repeated execution failure.
* When dependent architecture changes.
* When security requirements change.
* When tooling changes materially.

Review is part of prompt lifecycle maintenance.

---

# Triggered Review

Specific events should trigger review.

Examples include:

```text
Architecture Change

Dependency Policy Change

Security Incident

Repeated Prompt Failure

Scope Violation

Agent Tooling Change
```

Prompt quality should evolve with its environment.

---

# Prompt Drift

Prompt drift occurs when a prompt no longer aligns with:

* Architecture.
* Repository structure.
* Standards.
* Tooling.
* Validation.
* Business requirements.

Reviews should identify drift before it causes repeated failure.

---

# Prompt Regression

A prompt change may unintentionally degrade execution quality.

Regression may appear as:

* More clarifications.
* Scope violations.
* Missing outputs.
* Lower validation success.
* Increased rework.

Important prompts should be retested after significant changes.

---

# Review Metrics

Useful prompt-review signals may include:

* Clarification frequency.
* Execution success rate.
* Validation pass rate.
* Scope violation rate.
* Rework frequency.
* Failure rate.
* Human intervention frequency.

Metrics should inform improvement rather than create arbitrary scores.

---

# Quality Score Caution

A single prompt quality score may hide important risk.

For example:

```text
Excellent clarity

+

Excellent output contract

+

Unsafe production authority

=

Unsafe prompt
```

Critical quality dimensions should not be averaged away.

---

# Continuous Improvement

Prompt review should feed back into engineering assets.

A useful cycle is:

```text
Prompt Review

↓

Finding

↓

Correction

↓

Prompt Validation

↓

Execution Evidence

↓

Guideline / Template Improvement
```

Review should improve both the individual prompt and the broader prompt system.

---

# Review Findings as Engineering Work

Significant findings may become traceable engineering tasks.

Example:

```text
Finding:
Prompt lacks architecture boundary controls.

↓

Issue:
Add architecture scope section to bootstrap prompt template.
```

This turns prompt quality into governed engineering work.

---

# Review and Prompt Templates

Repeated review findings may indicate weaknesses in prompt templates.

Examples include repeated:

* Missing validation.
* Weak output contracts.
* Missing security controls.
* Undefined failure behavior.

Templates should evolve to prevent recurring quality defects.

---

# Review and Playbooks

Repeated prompt-specific engineering instructions may indicate missing playbook guidance.

Reviewers should ask:

> Does this rule belong in the prompt, or should it become reusable engineering methodology?

This preserves separation of concerns.

---

# Review and Architecture

Prompt review should verify that prompts consume architecture rather than redefine it silently.

Architecture-changing prompts should be explicitly classified and governed differently from implementation prompts.

---

# Review and AI Autonomy

Review rigor should increase with agent autonomy.

```text
Advisory Prompt
    ↓
Clarity Review

Coding Prompt
    ↓
Full Quality Review

Tool-Executing Prompt
    ↓
Quality + Security + Safety Review

Autonomous Workflow Prompt
    ↓
Full Review
+ Independent Validation
+ Approval Gates
+ Auditability
```

Higher autonomy requires stronger review.

---

# Common Review Anti-Patterns

Avoid:

## Style-Only Review

Focusing only on wording.

## Checklist Without Judgment

Marking items complete without evaluating their quality.

## Validation Blindness

Approving prompts without considering how outcomes are verified.

## Security as Afterthought

Ignoring authority and destructive operations.

## One Successful Run Equals Approval

Treating one execution as sufficient evidence.

## Author Self-Approval

Allowing high-risk prompts to bypass independent review.

## Excessive Formalism

Applying critical-workflow ceremony to trivial prompts.

---

# Engineering Recommendations

Prompt reviewers should:

* Evaluate engineering outcomes rather than prose style.
* Review intent first.
* Verify authoritative context.
* Evaluate scope carefully.
* Identify hidden decision authority.
* Verify output contracts.
* Require meaningful validation.
* Examine failure behavior.
* Review security and safety explicitly.
* Check maintainability and duplication.
* Scale review depth with risk.
* Require independent review for high-impact prompts.
* Use execution evidence to improve review quality.
* Convert recurring findings into template, guideline, or playbook improvements.

---

# Success Criteria

A prompt satisfies this guideline when:

* Its objective is clear.
* Required context is authoritative and current.
* Scope is explicit.
* Architectural and security boundaries are preserved.
* Instructions support controlled execution.
* Outputs are defined.
* Acceptance criteria are verifiable.
* Validation is appropriate to risk.
* Failure behavior is explicit.
* Significant ambiguity is escalated.
* Sensitive information is protected.
* High-risk authority requires approval.
* The prompt is maintainable and traceable.
* Review findings are resolved or explicitly accepted.
* The prompt is ready for its intended execution environment.

---

# Related Guidelines

This guideline completes the Prompt Quality Guidelines collection and should be used together with:

* 01-prompt-quality-principles.md
* 02-prompt-clarity.md
* 03-context-management.md
* 04-scope-and-boundaries.md
* 05-instruction-design.md
* 06-output-contracts.md
* 07-validation-and-acceptance.md
* 08-error-and-ambiguity-handling.md
* 09-security-and-safety.md

---

# Conclusion

Prompt Review is the governance mechanism that converts prompt quality principles into an engineering quality gate.

The complete review model is:

```text
Intent

↓

Clarity

↓

Context

↓

Scope

↓

Instructions

↓

Outputs

↓

Validation

↓

Ambiguity Handling

↓

Security & Safety

↓

Maintainability

↓

Review Evidence

↓

Approval
```

A prompt should not be approved because it sounds detailed or because an AI successfully executed it once.

It should be approved because its engineering intent is clear, its authority is bounded, its outputs are verifiable, its failure behavior is controlled, and its risks are understood.

The central principle is:

> **A production-quality prompt is not merely well written. It is reviewed, bounded, verifiable, maintainable, safe, and fit for its intended engineering authority.**
