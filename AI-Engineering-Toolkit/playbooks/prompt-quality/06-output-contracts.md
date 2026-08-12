
# Output Contracts

## Purpose

The Output Contracts guideline defines the engineering principles and practices for specifying the expected results of AI-assisted engineering tasks within the AI Engineering Toolkit.

Its purpose is to make AI outputs predictable, reviewable, verifiable, traceable, and suitable for downstream engineering workflows.

An output contract defines what successful execution must produce.

---

# Objectives

The Output Contracts guideline aims to:

* Standardize expected AI outputs.
* Improve execution predictability.
* Reduce ambiguous completion.
* Improve reviewability.
* Support automation.
* Enable downstream processing.
* Improve traceability.
* Clarify artifact ownership.
* Strengthen validation.
* Support machine-readable and human-readable outputs.
* Improve maintainability.

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
* Security analysis.
* Performance engineering.
* Validation.
* Review.
* AI-assisted engineering workflows.

Output requirements should remain proportional to task complexity and risk.

---

# Engineering Philosophy

A prompt should not stop at describing what the AI should do.

It should also define what the AI must produce.

A useful model is:

```text
Engineering Intent

↓

Execution

↓

Defined Outputs

↓

Validation

↓

Acceptance
```

Without explicit output contracts, AI systems may produce results that are technically relevant but operationally incomplete.

---

# Output Contract as an Engineering Interface

An output contract is the interface between execution and downstream engineering work.

It should answer:

* What artifacts must exist?
* What information must be returned?
* What format is expected?
* Which files may be created or modified?
* What evidence must accompany completion?
* What must not be produced?

The output contract should be understandable before execution begins.

---

# Output Categories

Outputs generally fall into one or more of the following categories:

```text
Repository Artifacts

Documentation Artifacts

Execution Results

Validation Evidence

Review Findings

Structured Data

Operational Reports

Decision Records
```

Each category has different quality and validation requirements.

---

# Repository Artifacts

Repository artifacts include changes to version-controlled assets.

Examples include:

* Source code.
* Tests.
* Configuration.
* Build assets.
* Infrastructure definitions.
* Documentation.
* Scripts.
* Templates.

Prompts should identify expected repository artifacts explicitly.

---

# Created Artifacts

If a task requires new files, the prompt should define them when practical.

Example:

```text
Create:
- src/MarketData/Validation/MarketDataValidationOptions.cs
- tests/MarketData.Tests/MarketDataValidationOptionsTests.cs
```

Explicit creation expectations improve traceability.

---

# Modified Artifacts

Prompts should identify expected modifications where scope is sufficiently known.

Example:

```text
Modify:
- MarketDataValidator.cs
- MarketDataValidatorTests.cs
```

When exact files cannot be predicted safely, define the project or component boundary instead.

---

# Deleted Artifacts

Deletion should always be explicit when it is part of the expected output.

Example:

```text
Delete:
- legacy/OldMarketDataParser.cs

Only after:
- replacement behavior is validated.
- no references remain.
```

Deletion should never be treated as an incidental output.

---

# Documentation Outputs

Documentation tasks should define the expected artifact and purpose.

Example:

```text
Output:
docs/architecture/MARKET_DATA_PIPELINE.md

The document must include:
- Purpose.
- Data flow.
- Component responsibilities.
- Failure handling.
- Validation points.
```

Documentation output contracts should define content expectations without unnecessarily prescribing wording.

---

# Code Outputs

Code-generation prompts should define behavioral expectations, not only file names.

Example:

```text
Expected code output:
- Reject records with invalid timestamps.
- Preserve existing public contracts.
- Reuse the current validation abstraction.
- Include automated tests.
```

The output contract should describe engineering behavior.

---

# Test Outputs

When tests are required, specify what they should prove.

Example:

```text
Tests must cover:
- Valid timestamp.
- Missing timestamp.
- Invalid timestamp format.
- Existing behavior remains unchanged.
```

Avoid output contracts that merely say:

```text
Add tests.
```

without defining meaningful verification.

---

# Configuration Outputs

Configuration outputs should define both artifact and semantic expectation.

Example:

```text
Update:
Directory.Packages.props

Expected result:
- New package version is centrally managed.
- No project-level version duplication is introduced.
```

Configuration output contracts should preserve repository consistency.

---

# Structured Outputs

Some prompts should produce structured data.

Examples include:

* JSON.
* YAML.
* CSV.
* Markdown tables.
* Machine-readable status.
* Findings lists.

When structure matters, define the schema or required fields.

---

# Machine-Readable Output

Machine-readable outputs should be deterministic enough for downstream tooling.

Example:

```text
Return JSON with:
- status
- filesChanged
- validationResults
- warnings
- errors
```

Field names and value expectations should be explicit.

---

# Human-Readable Output

Human-readable outputs should prioritize clarity and reviewability.

Useful content may include:

* Summary.
* Changes made.
* Validation results.
* Known limitations.
* Risks.
* Next actions.

Human-readable output should avoid unnecessary verbosity.

---

# Dual-Mode Outputs

Some workflows benefit from both machine-readable and human-readable outputs.

Example:

```text
Produce:
1. Structured validation result for automation.
2. Concise human summary for review.
```

Each output should have a clear consumer.

---

# Output Audience

Output design should consider who consumes the result.

Possible consumers include:

* Engineer.
* Reviewer.
* CI pipeline.
* Release workflow.
* Another AI agent.
* Governance process.

The same task may require different output forms depending on audience.

---

# Output Purpose

Every output should serve a purpose.

Avoid producing artifacts that do not contribute to:

* Implementation.
* Validation.
* Review.
* Traceability.
* Operations.
* Governance.

Unnecessary outputs increase maintenance cost.

---

# Output Minimalism

Prompts should require the smallest sufficient output set.

Avoid generating:

* Redundant reports.
* Duplicate documentation.
* Unused summaries.
* Repeated structured data.
* Unnecessary explanatory prose.

Output quantity should not be confused with output quality.

---

# Output Completeness

Outputs should be complete enough to satisfy the task contract.

A completion report should not omit important information such as:

* Validation failures.
* Unresolved risks.
* Missing artifacts.
* Partial completion.

Output completeness is part of execution transparency.

---

# Output Precision

Expected outputs should use precise terminology.

Prefer:

```text
Produce a Markdown architecture document at:
docs/architecture/MARKET_DATA_PIPELINE.md
```

over:

```text
Create some documentation about the pipeline.
```

Precision improves reproducibility.

---

# Output Location

When outputs belong in the repository, define their expected location.

Location may be specified by:

* File path.
* Folder.
* Project.
* Module.
* Artifact category.

Output location should align with repository structure standards.

---

# Output Naming

Output names should follow repository naming conventions.

Prompts should not invent naming styles that conflict with established standards.

Naming requirements should reference authoritative naming guidance where available.

---

# Output Ownership

Artifacts should have clear ownership.

Ownership may belong to:

* Module.
* Project.
* Team.
* Repository area.
* Engineering discipline.

AI systems should not place outputs in arbitrary shared locations when ownership is defined elsewhere.

---

# Output Format

Format expectations should be explicit when format matters.

Examples include:

```text
Markdown
JSON
YAML
C#
PowerShell
CSV
```

Formatting requirements should serve downstream use rather than stylistic preference.

---

# Output Schema

For structured outputs, define required fields.

Example:

```text
ValidationResult:
- status
- checks
- failures
- warnings
- evidence
```

Schemas improve automation and consistency.

---

# Output Ordering

If output sections must appear in a particular order, define that order explicitly.

Example:

```text
Report sections:
1. Summary
2. Files Changed
3. Validation
4. Warnings
5. Remaining Risks
```

Do not rely on AI-generated organization when order matters to downstream consumers.

---

# Output Stability

Reusable prompts should aim for stable output structures.

Stable outputs improve:

* Automation.
* Parsing.
* Review.
* Version comparison.
* Multi-agent workflows.

Content may vary while the contract remains stable.

---

# Output Versioning

Machine-readable or externally consumed output contracts may require versioning.

Versioning is useful when changes affect:

* Field names.
* Required fields.
* Semantics.
* Compatibility.
* Parsing behavior.

Output contract changes should be treated as interface changes.

---

# Behavioral Outputs

Some outputs are changes in behavior rather than standalone artifacts.

Example:

```text
Expected behavioral output:
Requests with invalid timestamps return validation failure
without reaching persistence.
```

Behavioral outputs should be validated through tests or observable execution.

---

# Operational Outputs

Operational prompts may produce:

* Logs.
* Metrics.
* Reports.
* Health results.
* Deployment artifacts.
* Diagnostic evidence.

Operational outputs should define retention, visibility, and sensitivity expectations where relevant.

---

# Review Outputs

Review prompts should define findings structure explicitly.

Example:

```text
For each finding provide:
- Area
- Severity
- Evidence
- Impact
- Recommendation
```

Review outputs should separate evidence from opinion.

---

# Validation Outputs

Validation prompts should define pass/fail evidence.

Example:

```text
For each validation check report:
- Check name
- Status
- Evidence
- Failure reason
```

Validation output should support objective decision-making.

---

# Architecture Outputs

Architecture prompts should specify expected decision artifacts.

Possible outputs include:

* Architecture document.
* ADR.
* Context diagram.
* Dependency model.
* Decision summary.

Architecture outputs should preserve rationale, not only conclusions.

---

# Planning Outputs

Planning prompts may produce:

* Implementation plan.
* Risk assessment.
* File impact list.
* Validation plan.
* Dependency analysis.

Plans should distinguish proposed work from executed work.

---

# Completion Reports

Executable prompts should normally produce a concise completion report.

A useful structure is:

```text
Execution Summary

Files Created

Files Modified

Files Deleted

Validation Performed

Validation Results

Warnings

Remaining Risks

Overall Status
```

The report should reflect actual execution, not intended execution.

---

# Completion Status

Output contracts should define valid status values when automation consumes them.

Example:

```text
Completed
Partially Completed
Failed
Blocked
```

Avoid vague states such as:

```text
Mostly Done
Seems Fine
Probably Successful
```

Status semantics should be explicit.

---

# Partial Completion Output

When work is incomplete, outputs should clearly identify:

* Completed items.
* Incomplete items.
* Blocking condition.
* Current validation state.
* Recommended next action.

Partial execution should not resemble full completion.

---

# Failure Output

Failure reports should include:

```text
Failure:
- Stage
- Cause
- Evidence
- Work completed
- Repository state
- Recommended next action
```

Failure output should support recovery.

---

# Warning Output

Warnings should represent non-blocking concerns.

Examples include:

* Deprecated dependency.
* Existing architecture drift.
* Unrelated defect discovered.
* Optional validation unavailable.

Warnings should not be mixed with blocking failures.

---

# Observation Output

Prompts may allow observations outside execution scope.

Example:

```text
Observations:
Report unrelated engineering concerns separately.
Do not modify them.
```

Observations preserve useful insight without expanding the task.

---

# Evidence Output

Significant completion claims should be supported by evidence.

Evidence may include:

* Build results.
* Test results.
* Static analysis.
* File inspection.
* Runtime output.
* Security scan.
* Performance measurement.

Evidence should correspond to acceptance criteria.

---

# Output and Validation Alignment

Expected outputs and validation should align directly.

For example:

```text
Output:
Timestamp validation implementation.

Validation:
Unit tests prove valid and invalid behavior.

Output:
Updated package configuration.

Validation:
Restore and build succeed without version conflicts.
```

Every critical output should have a verification path.

---

# Output and Acceptance Alignment

Acceptance criteria should be observable through output or validation evidence.

Avoid acceptance criteria that cannot be demonstrated by the defined outputs.

The contract should form a complete chain:

```text
Requirement

↓

Output

↓

Validation

↓

Evidence

↓

Acceptance
```

---

# Output and Scope Alignment

Outputs must remain within approved scope.

A prompt should not request outputs that require unauthorized changes.

Example conflict:

```text
Scope:
Do not modify public APIs.

Output:
Redesign the public API contract.
```

Output contracts should be reviewed against boundaries.

---

# Output and Context Alignment

Output expectations should reflect authoritative repository context.

Do not request:

* Wrong file locations.
* Obsolete architecture.
* Unsupported formats.
* Deprecated project conventions.

Current repository context should guide output design.

---

# Output and Security

Outputs should not expose sensitive information.

Avoid including:

* Secrets.
* Credentials.
* Private keys.
* Tokens.
* Sensitive production data.
* Internal security information without need.

Sensitive outputs should be minimized, redacted, or omitted.

---

# Output and Privacy

Where personal or regulated data may be involved, output contracts should define appropriate handling.

Generated reports should avoid unnecessary inclusion of sensitive records.

Only required data should be surfaced.

---

# Output and Destructive Work

Destructive outputs should require explicit authorization.

Examples include:

* Deleted files.
* Removed infrastructure.
* Dropped data.
* Rewritten history.

Destructive artifact changes should never be implied by broad outcome language.

---

# Output and Idempotency

Repeated prompt execution should not create duplicate outputs where idempotency is expected.

Examples include:

* Duplicate configuration entries.
* Duplicate files.
* Duplicate GitHub assets.
* Repeated documentation sections.

Output contracts should define preservation behavior when artifacts already exist.

---

# Output and Generated Assets

Generated assets should identify their source of truth.

Example:

```text
Do not manually edit generated client files.

Modify the API specification and regenerate the clients.
```

Output contracts should preserve generation workflows.

---

# Output and Multi-Agent Workflows

Multi-agent workflows require clear handoff contracts.

An implementation agent may output:

```text
- Changed files
- Validation status
- Decisions made
- Remaining risks
```

A review agent can then consume that contract.

Stable handoffs improve orchestration.

---

# Agent Handoff Output

A handoff should contain enough context for continuation without hidden conversation state.

Useful fields include:

* Objective.
* Completed work.
* Current state.
* Modified assets.
* Validation evidence.
* Open risks.
* Next action.

Handoffs should remain concise and authoritative.

---

# Output and AI Autonomy

As AI autonomy increases, output contracts should become stronger.

```text
Advisory AI
    ↓
Recommendation Output

Coding Agent
    ↓
Artifact + Validation Output

Tool-Executing Agent
    ↓
Artifact + Execution + Evidence Output

Autonomous Workflow
    ↓
Machine-Readable State + Evidence + Handoff Output
```

Higher autonomy requires more reliable completion contracts.

---

# Output Contracts and Automation

Automation depends on stable outputs.

Machine-consumed outputs should favor:

* Defined fields.
* Stable naming.
* Explicit status.
* Predictable structure.
* Versioned contracts where necessary.

Free-form prose should not be the only output for machine-driven workflows.

---

# Output Contracts and Human Review

Human reviewers benefit from concise summaries that answer:

* What changed?
* Why?
* What was validated?
* What remains uncertain?
* Did scope change?

Review output should reduce the effort required to understand AI-generated work.

---

# Output Contract Reuse

Common output structures should become reusable templates.

Examples include:

* Implementation completion report.
* Validation report.
* Review findings report.
* Architecture decision output.
* Agent handoff.

Reuse improves consistency across prompt collections.

---

# Output Contract Maintainability

Reusable output contracts should:

* Remain simple.
* Avoid unnecessary fields.
* Preserve backward compatibility where useful.
* Be documented.
* Be versioned when consumed programmatically.

Output contracts should evolve deliberately.

---

# Output Contract Testing

Important structured outputs should be tested where practical.

Testing may verify:

* Required fields.
* Schema validity.
* Status semantics.
* Missing values.
* Compatibility.
* Parser behavior.

Output contract validation is particularly important for automation.

---

# Common Output Contract Anti-Patterns

Avoid:

## Undefined Output

```text
Handle the task.
```

## Vague Completion

```text
Tell me when it looks good.
```

## Excessive Output

Requesting large reports unrelated to engineering value.

## Format Ambiguity

Requiring structured processing while allowing arbitrary prose.

## Hidden Artifacts

Creating files not disclosed in the completion report.

## Output-Scope Conflict

Requesting results that violate defined boundaries.

## Unsupported Success

Reporting completion without validation evidence.

## Secret Exposure

Including sensitive values in generated reports.

---

# Engineering Recommendations

Prompt authors should:

* Define expected outputs explicitly.
* Identify required artifacts.
* Specify format where it matters.
* Define output location when relevant.
* Describe behavioral expectations.
* Align outputs with scope.
* Align outputs with validation.
* Require evidence for significant completion claims.
* Define partial and failure output.
* Separate warnings from failures.
* Keep completion reports concise.
* Protect sensitive information.
* Use structured outputs for automation.
* Reuse common output contracts.
* Version externally consumed contracts when necessary.

---

# Success Criteria

A prompt satisfies this guideline when:

* Expected outputs are understandable before execution.
* Required artifacts are identifiable.
* Output locations are clear where necessary.
* Behavioral outcomes are explicit.
* Format expectations are defined where relevant.
* Outputs remain within approved scope.
* Validation can prove critical outputs.
* Completion status is unambiguous.
* Partial execution is reported accurately.
* Failures produce actionable information.
* Sensitive information is protected.
* Outputs support intended downstream consumers.
* Another engineer or agent can determine whether the output contract was satisfied.

---

# Related Guidelines

This guideline should be used together with:

* 01-prompt-quality-principles.md
* 02-prompt-clarity.md
* 03-context-management.md
* 04-scope-and-boundaries.md
* 05-instruction-design.md
* 07-validation-and-acceptance.md
* 08-error-and-ambiguity-handling.md
* 09-security-and-safety.md
* 10-prompt-review.md

---

# Conclusion

Output contracts define what AI-assisted engineering execution must leave behind.

A strong output model follows:

```text
Engineering Requirement

↓

Defined Artifact or Behavior

↓

Structured Result

↓

Validation Evidence

↓

Acceptance
```

The output should not be whatever the AI system happens to produce.

It should be the smallest complete set of artifacts, behavior, evidence, and status information required to satisfy the engineering objective.

The central principle is:

> **If successful execution cannot be distinguished objectively from incomplete or incorrect execution by inspecting the defined outputs and evidence, the output contract is not yet strong enough.**
