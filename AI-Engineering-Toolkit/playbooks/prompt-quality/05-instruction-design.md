
# Instruction Design

## Purpose

The Instruction Design guideline defines the engineering principles and practices for structuring prompt instructions so AI systems can execute engineering tasks reliably, predictably, and within established constraints.

Its purpose is to convert engineering intent into clear, ordered, testable, and maintainable execution guidance.

Well-designed instructions should reduce ambiguity, control execution flow, and make AI behavior easier to review and validate.

---

# Objectives

The Instruction Design guideline aims to:

* Improve execution reliability.
* Standardize instruction structure.
* Reduce ambiguity.
* Improve task decomposition.
* Clarify execution order.
* Make constraints visible.
* Improve failure handling.
* Support validation.
* Improve maintainability.
* Enable safe AI autonomy.
* Support reusable engineering workflows.

---

# Scope

This guideline applies to prompts used for:

* Repository modification.
* Software implementation.
* Refactoring.
* Testing.
* Documentation.
* Architecture analysis.
* DevOps automation.
* GitHub operations.
* Security work.
* Performance engineering.
* Validation.
* Reviews.
* AI-assisted engineering workflows.

Instruction rigor should remain proportional to task complexity, risk, and execution authority.

---

# Engineering Philosophy

Prompt instructions are executable engineering guidance.

A useful model is:

```text
Intent

↓

Structured Instructions

↓

Controlled Execution

↓

Validation

↓

Engineering Outcome
```

Weak instructions force the AI system to infer workflow.

Strong instructions define enough execution structure to preserve engineering intent without unnecessarily prescribing every implementation detail.

---

# Instruction Hierarchy

Instructions should follow a clear hierarchy.

A useful model is:

```text
Objective

↓

Preconditions

↓

Context

↓

Scope

↓

Constraints

↓

Execution Steps

↓

Validation

↓

Completion
```

Higher-level engineering intent should guide lower-level implementation actions.

---

# One Instruction, One Intent

Individual instructions should normally represent one primary action.

Prefer:

```text
1. Inspect the existing validation implementation.
2. Add timestamp validation.
3. Add regression tests.
4. Run the affected tests.
```

Avoid:

```text
Inspect everything, improve validation, clean up the code,
update tests, and fix anything else you notice.
```

Atomic instructions improve traceability and failure diagnosis.

---

# Sequential Instructions

When order matters, execution sequence should be explicit.

Example:

```text
1. Read the architecture documentation.
2. Inspect the existing implementation.
3. Produce an implementation plan.
4. Implement the requested change.
5. Add or update tests.
6. Run validation.
7. Report the outcome.
```

Do not rely on paragraph ordering when sequence is significant.

---

# Non-Sequential Instructions

Not every task requires strict ordering.

Independent requirements may be grouped under:

```text
Requirements:
- Preserve public API compatibility.
- Follow existing naming conventions.
- Do not introduce new dependencies.
```

The prompt should distinguish mandatory order from unordered constraints.

---

# Preconditions

Instructions should identify prerequisites when execution depends on them.

Examples include:

* Required project exists.
* Architecture document is approved.
* SDK is available.
* Current repository state is valid.
* Previous migration has completed.

Preconditions should be checked before destructive or expensive work begins.

---

# Inspection Before Modification

Repository-aware engineering tasks should generally begin with inspection.

A useful pattern is:

```text
Inspect

↓

Understand

↓

Plan

↓

Modify
```

Agents should avoid changing files before understanding:

* Architecture.
* Existing implementation.
* Tests.
* Dependencies.
* Applicable standards.

---

# Planning Before Execution

Significant tasks should require an implementation plan before modification.

The plan may identify:

* Files affected.
* Components affected.
* Dependencies.
* Tests required.
* Risks.
* Validation steps.

Planning is especially valuable for high-risk or multi-file changes.

---

# Plan Proportionality

Planning effort should match task complexity.

A simple change may require only:

```text
Plan:
- Update validator.
- Add tests.
- Run test project.
```

A system-level change may require architecture, dependency, migration, and rollout considerations.

Planning should improve execution rather than become unnecessary ceremony.

---

# Task Decomposition

Large tasks should be decomposed into smaller engineering units.

A useful decomposition model is:

```text
Discover

↓

Design

↓

Implement

↓

Verify

↓

Integrate
```

Each step should have a clear outcome.

---

# Decomposition Boundaries

Task decomposition should align with engineering boundaries.

Good decomposition may follow:

* Feature.
* Module.
* Layer.
* Component.
* Validation stage.

Avoid arbitrary decomposition that fragments one coherent engineering change into disconnected actions.

---

# Incremental Execution

Complex tasks should be executed incrementally.

Prefer:

```text
Implement one coherent change

↓

Build

↓

Test

↓

Inspect result

↓

Continue
```

Incremental execution limits the cost of incorrect assumptions.

---

# Explicit Constraints

Constraints should be separated from execution steps.

Example:

```text
Constraints:
- Do not change public contracts.
- Do not add dependencies.
- Preserve current persistence behavior.
```

This reduces the risk that constraints become hidden inside long procedural instructions.

---

# Mandatory vs Recommended Instructions

Prompts should distinguish mandatory requirements from recommendations.

Example:

```text
Required:
- Preserve public API compatibility.
- Add regression tests.

Preferred:
- Reuse the existing validation helper where practical.
```

This helps agents understand which requirements may be traded off.

---

# Conditional Instructions

Conditional behavior should be explicit.

Example:

```text
If an existing abstraction supports the requirement:
- Extend it.

If it does not:
- Do not create a new abstraction automatically.
- Report the architectural constraint.
```

Conditional instructions reduce silent assumptions.

---

# Decision Points

Prompts should identify significant decision points.

Examples include:

* Whether a new dependency is required.
* Whether a public contract must change.
* Whether persistence changes are needed.
* Whether architecture conflicts exist.

Decision points should define whether the agent may proceed or must escalate.

---

# Decision Authority

Instruction design should distinguish between:

```text
Agent Decision

and

Engineering Approval
```

Agents may usually decide:

* Private implementation details.
* Local naming.
* Test arrangement.

Agents should not independently decide:

* New architecture.
* Security boundary changes.
* Breaking public contracts.
* Production deployment strategy.

---

# Negative Instructions

Important prohibitions should be explicit.

Examples include:

```text
Do not:
- Refactor unrelated code.
- Add new packages.
- Modify architecture documents.
- Disable tests.
```

Negative instructions should be used where the prohibited action is plausible.

---

# Positive Instructions

Positive instructions should explain the desired engineering path.

Example:

```text
Use the existing IMarketDataValidator abstraction.
```

Positive guidance is generally easier to execute than prohibition alone.

---

# Positive and Negative Pairing

For critical boundaries, pair positive and negative instructions.

Example:

```text
Use the existing repository abstraction.

Do not access the database directly from the application layer.
```

This defines both the expected path and the prohibited shortcut.

---

# Avoid Hidden Workflow

Prompts should not assume the AI system knows the team's workflow.

Instead of:

```text
Implement this normally.
```

prefer:

```text
Inspect the implementation, update the code, add tests,
run validation, and report the changed files.
```

Reusable prompts should encode workflow explicitly or reference the relevant playbook.

---

# Reference Playbooks

Reusable engineering methodology should be referenced instead of duplicated.

Example:

```text
Follow:
playbooks/dotnet/08-testing.md
```

Prompt instructions should focus on the current task while playbooks define reusable engineering practice.

---

# Instruction Granularity

Instructions should be detailed enough to control meaningful engineering outcomes.

Too broad:

```text
Implement authentication.
```

Too narrow:

```text
Create a private variable named tokenValidator on line 42.
```

Better:

```text
Implement token validation using the existing authentication abstraction.
Preserve current public contracts and add tests for invalid and expired tokens.
```

---

# Avoid Implementation Micromanagement

Prompts should avoid controlling low-risk internal details unnecessarily.

Overly specific instructions:

* Increase maintenance cost.
* Reduce adaptability.
* May conflict with repository conventions.

Prescribe implementation details only when they are part of the engineering contract.

---

# Instruction Consistency

Instructions should use consistent terminology and structure.

If one section says:

```text
customer identifier
```

and another says:

```text
client ID
```

the agent may infer separate concepts.

Terminology should align with authoritative repository language.

---

# Instruction Priority

When requirements may conflict, priority should be explicit.

Example:

```text
Priority:
1. Security.
2. Data integrity.
3. Public contract compatibility.
4. Architecture compliance.
5. Minimal change.
```

Priority prevents silent conflict resolution.

---

# Conflict Avoidance

Prompts should be reviewed for contradictions.

Examples include:

```text
Do not modify public contracts.

Change the existing API response structure.
```

or:

```text
Make the smallest possible change.

Refactor the entire module.
```

Contradictory instructions reduce execution reliability.

---

# Command Instructions

When commands are required, specify:

* Command.
* Purpose.
* Expected context.
* Success condition.

Example:

```text
Run:
dotnet test tests/MarketData.Tests

Purpose:
Verify timestamp validation behavior.

Success:
All tests pass.
```

This provides both action and interpretation.

---

# Tool Instructions

When prompts allow tools, instructions should clarify their intended use.

Examples include:

* Read files before editing.
* Use build tools for validation.
* Avoid destructive Git operations.
* Do not publish artifacts unless requested.

Tool capability should remain bounded by engineering intent.

---

# File Modification Instructions

When modifying repositories, prompts should define expected file behavior.

Examples include:

```text
Create:
- MarketDataValidationOptions.cs

Modify:
- MarketDataValidator.cs
- MarketDataValidatorTests.cs

Do not modify:
- public API contracts
```

File expectations improve change traceability.

---

# Idempotent Instructions

Automation-oriented instructions should support safe re-execution where practical.

Example:

```text
If the configuration already exists and is compliant, preserve it.
Do not create duplicate entries.
```

Idempotency should be built into execution guidance where repeated runs are expected.

---

# Validation Instructions

Validation should be part of the instruction flow.

Example:

```text
After implementation:
1. Build the affected solution.
2. Run the affected tests.
3. Verify no new analyzer warnings were introduced.
```

Validation should not be treated as optional follow-up work.

---

# Acceptance Instructions

Prompts should connect execution instructions to acceptance criteria.

Example:

```text
Do not report completion until:
- Build succeeds.
- Tests pass.
- No public contract changed.
```

This prevents generation from being confused with completion.

---

# Failure Instructions

Instructions should define failure behavior.

Example:

```text
If the build fails:
- Stop further implementation.
- Identify the failure.
- Determine whether it was introduced by the change.
- Report the result.
```

Failure handling should preserve evidence.

---

# Partial Completion

Prompts should define behavior when only part of the task can be completed.

Example:

```text
If one requirement cannot be completed:
- Complete only independent safe work.
- Report the blocked requirement.
- Do not claim full completion.
```

Partial completion should remain transparent.

---

# Recovery Instructions

For recoverable workflows, define how execution may resume.

Example:

```text
If validation fails due to a missing local dependency:
- Preserve completed code changes.
- Report the missing dependency.
- Do not modify repository standards to bypass the failure.
```

Recovery should preserve repository integrity.

---

# Reporting Instructions

Agents should be instructed to summarize meaningful execution results.

A completion report may include:

```text
Changed files

Implementation summary

Validation executed

Validation results

Known risks

Unresolved assumptions
```

Reporting should be concise but sufficient for review.

---

# Observation vs Action

Prompts should distinguish findings from authorized changes.

Example:

```text
If unrelated defects are discovered:
- Report them as observations.
- Do not modify them.
```

This prevents inspection from becoming uncontrolled implementation.

---

# Review Instructions

Review prompts should not behave like implementation prompts.

A review instruction may specify:

```text
Inspect the complete solution.

Do not modify files.

Produce:
- Findings.
- Evidence.
- Severity.
- Recommendations.
```

Instruction design should reflect the task type.

---

# Validation-Only Instructions

Validation prompts should explicitly prohibit redesign or remediation unless requested.

Example:

```text
Validate compliance only.

Do not modify the implementation.

Report failed criteria and supporting evidence.
```

This preserves separation between validation and correction.

---

# Documentation Instructions

Documentation tasks should define:

* Target audience.
* Authoritative source.
* Scope.
* Expected artifact.
* Required terminology.

Documentation prompts should not infer implementation changes unless explicitly requested.

---

# Security-Sensitive Instructions

High-risk prompts should include stronger controls.

Example:

```text
Do not:
- Expose secrets.
- Disable authentication.
- Modify authorization rules without approval.
- Execute against production.
```

Security-sensitive instructions should be explicit and testable.

---

# Destructive Instructions

Destructive operations require explicit authorization.

Examples include:

* Delete.
* Drop.
* Purge.
* Rewrite history.
* Destroy.
* Remove production resources.

Prompts should never imply destructive authority indirectly.

---

# Human Approval Instructions

Some workflows should include explicit approval gates.

Example:

```text
Prepare the migration plan and validation strategy.

Do not execute the production migration.
```

Instruction design should separate preparation from execution when risk requires human approval.

---

# Risk-Based Instruction Design

Instruction rigor should increase with engineering risk.

```text
Low Risk
    ↓
Simple Action + Validation

Medium Risk
    ↓
Inspect + Plan + Execute + Validate

High Risk
    ↓
Inspect
+ Context Verification
+ Plan
+ Approval Boundary
+ Controlled Execution
+ Strong Validation
+ Independent Review
```

Not every prompt requires the same level of procedural detail.

---

# Instruction Reuse

Repeated instruction patterns should become reusable prompt components or playbooks.

Examples include:

* Repository inspection.
* Build validation.
* Test execution.
* Completion reporting.
* Security checks.

Reuse improves consistency and reduces prompt duplication.

---

# Instruction Maintainability

Reusable instructions should remain easy to update.

Avoid:

* Large duplicated procedure blocks.
* Hardcoded obsolete paths.
* Tool-specific assumptions without necessity.
* Hidden dependencies on conversation history.

Instructions should reference authoritative assets wherever practical.

---

# Instruction Versioning

Significant reusable instruction changes may require versioning when they affect:

* Execution behavior.
* Output expectations.
* Validation.
* Tool use.
* Compatibility.
* Safety.

Versioning improves reproducibility.

---

# Instruction Testing

Important reusable prompts should be tested against representative scenarios.

Instruction testing may evaluate:

* Step interpretation.
* Scope compliance.
* Conditional behavior.
* Failure handling.
* Output quality.
* Validation behavior.

The objective is consistent engineering execution, not identical generated text.

---

# Multi-Agent Instructions

When multiple agents participate, responsibilities should be separated explicitly.

Example:

```text
Agent A:
Implement the change.

Agent B:
Review architecture and test coverage.

Validation Agent:
Run required checks and report evidence.
```

Agents should not assume responsibilities that belong to another role.

---

# Handoff Instructions

Agent handoffs should define the information required for continuation.

Example:

```text
Provide:
- Work completed.
- Files changed.
- Decisions made.
- Validation status.
- Remaining work.
```

Handoffs should not rely on hidden conversation state.

---

# Instruction and AI Autonomy

As autonomy increases, instructions should become more explicit about:

* Authority.
* Scope.
* Decision boundaries.
* Validation.
* Failure handling.
* Approval gates.

A coding agent that can execute commands requires stronger instruction design than an advisory assistant.

---

# Common Instruction Anti-Patterns

Avoid:

## Broad Command

```text
Improve the system.
```

## Hidden Sequence

```text
Implement this correctly.
```

## Unlimited Agency

```text
Do whatever is necessary.
```

## Contradictory Requirements

```text
Preserve all behavior but change the public contract.
```

## Validation Omission

```text
Implement and finish.
```

without defining verification.

## Micromanagement

Controlling low-risk internal implementation details unnecessarily.

## Workflow Duplication

Repeating entire playbooks inside every prompt.

## Silent Decision Delegation

Allowing agents to make architecture or security decisions implicitly.

---

# Engineering Recommendations

Prompt authors should:

* Begin with the objective.
* Verify prerequisites.
* Separate constraints from execution steps.
* Structure complex work sequentially.
* Decompose large tasks.
* Require inspection before significant modification.
* Require planning for high-impact changes.
* Define decision boundaries.
* Use conditional instructions explicitly.
* Preserve scope.
* Integrate validation into execution.
* Define failure behavior.
* Distinguish observations from authorized changes.
* Use approval gates for high-risk operations.
* Reuse playbooks and standardized instruction patterns.
* Keep instructions proportional to task complexity.

---

# Success Criteria

A prompt satisfies this guideline when:

* Instructions follow a clear structure.
* Execution order is explicit where required.
* Tasks are decomposed appropriately.
* Preconditions are identifiable.
* Constraints are visible.
* Decision authority is understood.
* Conditional behavior is explicit.
* Validation is part of the workflow.
* Failure and partial completion behavior are defined.
* Unrelated work is not implicitly authorized.
* Instructions remain understandable and maintainable.
* Another engineer or agent could follow the same execution model consistently.

---

# Related Guidelines

This guideline should be used together with:

* 01-prompt-quality-principles.md
* 02-prompt-clarity.md
* 03-context-management.md
* 04-scope-and-boundaries.md
* 06-output-contracts.md
* 07-validation-and-acceptance.md
* 08-error-and-ambiguity-handling.md
* 09-security-and-safety.md
* 10-prompt-review.md

---

# Conclusion

Instruction design converts engineering intent into controlled execution.

A strong instruction model follows:

```text
Understand

↓

Inspect

↓

Plan

↓

Execute

↓

Validate

↓

Report
```

The objective is not to tell an AI system every implementation detail.

The objective is to define enough structure, authority, sequencing, constraints, and validation that the system can act reliably without inventing significant engineering decisions.

The central principle is:

> **A good instruction tells the AI what must happen, in what order when necessary, under which constraints, and how to know when the work is truly complete.**
