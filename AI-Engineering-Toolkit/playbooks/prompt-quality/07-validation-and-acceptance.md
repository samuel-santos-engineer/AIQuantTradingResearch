
# Validation and Acceptance

## Purpose

The Validation and Acceptance guideline defines the engineering principles and practices for verifying AI-assisted engineering work and determining whether it satisfies its intended objective within the AI Engineering Toolkit.

Its purpose is to ensure that generated outputs are not treated as complete merely because they exist, but are verified against explicit requirements, acceptance criteria, engineering standards, and observable evidence.

Validation establishes confidence.

Acceptance determines whether the result is fit to progress.

---

# Objectives

The Validation and Acceptance guideline aims to:

* Standardize validation practices.
* Define objective completion criteria.
* Prevent false completion.
* Improve engineering confidence.
* Strengthen traceability.
* Support automated quality gates.
* Distinguish execution from acceptance.
* Improve failure transparency.
* Enable repeatable verification.
* Support human and AI-assisted review.
* Reduce regression risk.

---

# Scope

This guideline applies to prompts used for:

* Software implementation.
* Refactoring.
* Testing.
* Documentation.
* Repository automation.
* Architecture work.
* DevOps activities.
* GitHub operations.
* Security engineering.
* Performance engineering.
* Validation.
* Review.
* AI-assisted engineering workflows.

Validation rigor should remain proportional to task complexity, impact, and risk.

---

# Engineering Philosophy

AI-generated work should not be accepted on appearance alone.

A useful model is:

```text
Generated

↓

Executed

↓

Validated

↓

Evidence Collected

↓

Acceptance Evaluated

↓

Accepted
```

Each stage represents a different engineering state.

Generation is not validation.

Validation is not acceptance.

Acceptance requires evidence that the engineering contract has been satisfied.

---

# Validation vs Acceptance

Validation and acceptance serve different purposes.

```text
Validation
    ↓
Does the result satisfy defined checks?

Acceptance
    ↓
Is the result fit to meet the engineering objective?
```

A result may pass individual checks while still fail overall acceptance because:

* Scope was violated.
* Required behavior is missing.
* Security constraints were broken.
* Documentation is incomplete.
* The wrong problem was solved.

Acceptance should evaluate the complete engineering outcome.

---

# Validation as Evidence

Validation should produce observable evidence.

Evidence may include:

* Build output.
* Test results.
* Static analysis.
* File inspection.
* Runtime behavior.
* Security scans.
* Performance measurements.
* Schema validation.
* Documentation checks.

Statements such as:

```text
This should work.
```

do not constitute engineering evidence.

---

# Acceptance Criteria

Acceptance criteria define the observable conditions required for successful completion.

Criteria should be:

* Specific.
* Relevant.
* Observable.
* Verifiable.
* Aligned with the objective.

A prompt should define acceptance criteria before execution whenever practical.

---

# Acceptance Criteria Example

Prefer:

```text
Acceptance criteria:
- Invalid timestamps are rejected.
- Valid records continue through the pipeline.
- Public API behavior remains unchanged.
- All affected tests pass.
- No new analyzer warnings are introduced.
```

Avoid:

```text
Make sure validation works correctly.
```

The first version creates an objective engineering contract.

---

# Validation Categories

Validation may occur across several categories.

```text
Structural Validation

Behavioral Validation

Build Validation

Test Validation

Architecture Validation

Security Validation

Performance Validation

Documentation Validation

Operational Validation
```

Not every task requires every category.

The validation strategy should reflect the work performed.

---

# Structural Validation

Structural validation verifies that expected artifacts and organization exist.

Examples include:

* Required files exist.
* Expected directories exist.
* Generated assets are in correct locations.
* Project references are correct.
* Configuration is present.

Structural validation is particularly useful for repository bootstrap and configuration tasks.

---

# Behavioral Validation

Behavioral validation verifies that software performs the required behavior.

Examples include:

* Input is accepted or rejected correctly.
* Business rules execute correctly.
* Error behavior is preserved.
* Public contracts remain compatible.

Behavioral validation should focus on observable outcomes.

---

# Build Validation

Build validation verifies that implementation integrates successfully with the codebase.

Typical checks include:

* Dependency restore.
* Compilation.
* Analyzer execution.
* Generated code.
* Build warnings.

A successful build is necessary for many code changes but rarely sufficient for acceptance.

---

# Test Validation

Tests provide executable evidence.

Validation may include:

* Unit tests.
* Integration tests.
* Architecture tests.
* Contract tests.
* End-to-end tests.
* Regression tests.

The selected test set should reflect the change and associated risk.

---

# Architecture Validation

Architecture validation verifies that implementation respects established design rules.

Examples include:

* Dependency direction.
* Layer boundaries.
* Module isolation.
* Naming conventions.
* Forbidden references.

Architecture validation reduces architecture drift.

---

# Security Validation

Security-sensitive work should include appropriate verification.

Examples include:

* Authentication behavior.
* Authorization behavior.
* Secret scanning.
* Dependency scanning.
* Input validation.
* Permission boundaries.

Security validation should be explicit for high-risk changes.

---

# Performance Validation

Performance-sensitive changes should be validated through measurement.

Examples include:

* Benchmarks.
* Load tests.
* Latency comparisons.
* Allocation measurements.
* Throughput measurements.

Performance acceptance should rely on evidence rather than intuition.

---

# Documentation Validation

Documentation outputs should be validated for:

* Required sections.
* Correct paths.
* Link integrity.
* Alignment with implementation.
* Terminology consistency.

Documentation should not be accepted merely because a file was generated.

---

# Operational Validation

Operational changes may require validation of:

* Startup.
* Shutdown.
* Health checks.
* Logging.
* Configuration.
* Deployment behavior.
* Recovery behavior.

Operational readiness should reflect the actual execution environment.

---

# Validation Depth

Validation depth should match task risk.

```text
Low Risk
    ↓
Focused Validation

Medium Risk
    ↓
Build + Tests + Scope Verification

High Risk
    ↓
Build
+ Tests
+ Architecture
+ Security
+ Operational Checks
+ Review
```

Validation should be sufficient without creating unnecessary process overhead.

---

# Validation Planning

Significant tasks should define validation before implementation begins.

A validation plan may answer:

* What must be verified?
* Which commands will run?
* Which tests are relevant?
* What evidence is required?
* What constitutes failure?

Defining validation early improves implementation quality.

---

# Validation Sequence

Validation should follow a logical progression.

A common sequence is:

```text
Inspect Outputs

↓

Build

↓

Static Analysis

↓

Tests

↓

Architecture Checks

↓

Security Checks

↓

Performance / Operational Checks

↓

Acceptance Evaluation
```

Early failures should prevent unnecessary later work where appropriate.

---

# Local Validation

Agents should perform local validation before reporting completion whenever tool access permits.

Local validation may include:

* Build.
* Tests.
* Formatting.
* Static analysis.
* File inspection.

Completion should not be based solely on generated content.

---

# Independent Validation

Important work should use validation independent of the generation process where practical.

Examples include:

* Automated test suites.
* Static analyzers.
* Security scanners.
* Architecture tests.
* Separate review agents.
* Human review.

Independent validation reduces correlated mistakes.

---

# Validation Evidence Record

Completion reports should preserve validation evidence.

A useful format is:

```text
Validation:
- dotnet build: Passed
- unit tests: Passed (42/42)
- architecture tests: Passed
- security scan: Not required
```

Evidence should be concise and factual.

---

# Validation Failure

When validation fails, the result should not be reported as complete.

The agent should identify:

* Failed check.
* Failure reason.
* Evidence.
* Whether the failure was introduced by the current work.
* Current repository state.
* Recommended next action.

Failure should remain visible.

---

# Existing Validation Failures

A repository may contain pre-existing failures.

Agents should distinguish:

```text
Failure introduced by current change

from

Pre-existing repository failure
```

Existing failures should not automatically block all work, but they should be reported clearly.

---

# Validation Bypass

Agents should not bypass validation merely to achieve a successful status.

Do not:

* Disable tests.
* Remove assertions.
* Suppress analyzers without justification.
* Skip required checks.
* Weaken security rules.

Validation controls are part of the engineering contract.

---

# Validation Scope

Validation should remain aligned with change scope.

A small focused change may not require the entire system test suite.

A cross-cutting change may require broader validation.

Validation breadth should be intentional.

---

# Acceptance Evaluation

Acceptance should compare the final result against:

* Objective.
* Scope.
* Constraints.
* Output contract.
* Acceptance criteria.
* Validation evidence.

A useful model is:

```text
Objective

+

Scope Compliance

+

Required Outputs

+

Validation Evidence

+

Acceptance Criteria

↓

Acceptance Decision
```

---

# Acceptance Status

Reusable workflows may standardize acceptance status.

Recommended values include:

```text
Accepted

Accepted with Observations

Rejected

Blocked
```

Status meanings should remain explicit.

---

# Accepted

Use when:

* Required outputs exist.
* Acceptance criteria are satisfied.
* Required validation passed.
* Scope was respected.
* No blocking issue remains.

---

# Accepted with Observations

Use when:

* Core acceptance criteria are satisfied.
* Non-blocking concerns remain.
* Those concerns are documented.

Observations should not conceal material risks.

---

# Rejected

Use when:

* Required criteria failed.
* Scope was violated.
* Validation failed materially.
* Required outputs are missing.

Rejected work requires remediation before acceptance.

---

# Blocked

Use when acceptance cannot be determined because of an external condition.

Examples include:

* Missing dependency.
* Required environment unavailable.
* Missing approval.
* External service unavailable.

Blocked does not mean failed.

---

# Mandatory Acceptance Criteria

Some criteria may be mandatory regardless of other success.

Examples include:

* Security controls preserved.
* Data integrity preserved.
* Public contract compatibility maintained.
* Required tests passed.

Mandatory criteria should be identified explicitly.

---

# Optional Acceptance Criteria

Optional criteria may improve quality without blocking acceptance.

Examples include:

* Additional documentation.
* Non-critical performance improvement.
* Minor refactoring.

Optional criteria should not be confused with mandatory quality gates.

---

# Acceptance and Scope

A technically correct implementation may still fail acceptance if it exceeds scope.

Example:

```text
Requirement satisfied:
Yes

Scope respected:
No

Acceptance:
Rejected
```

Scope compliance is part of engineering correctness.

---

# Acceptance and Security

Security requirements should be treated as acceptance gates when relevant.

A task should not be accepted if it:

* Exposes secrets.
* Weakens authorization.
* Introduces unsafe dependencies.
* Bypasses required security checks.

Functional correctness does not override security.

---

# Acceptance and Documentation

Documentation may be part of acceptance when behavior, architecture, configuration, or public contracts changed.

If documentation is required by the task contract, missing documentation means the work is incomplete.

---

# Acceptance and Performance

Performance criteria should be measurable.

Prefer:

```text
95th percentile latency must remain below 200 ms under the defined workload.
```

over:

```text
Performance should remain good.
```

Performance acceptance should use objective thresholds when applicable.

---

# Acceptance and Regression

Significant bug fixes should include regression protection where practical.

Acceptance may require:

* Reproduction test.
* Fix.
* Passing regression test.

This converts a defect into durable engineering knowledge.

---

# Acceptance and Idempotency

Automation-oriented prompts may require idempotency as an acceptance criterion.

Example:

```text
Running the prompt twice must not create duplicate configuration entries.
```

Idempotency should be tested where repeated execution is expected.

---

# Acceptance and Traceability

Acceptance evidence should be traceable to the task.

A useful chain is:

```text
Requirement

↓

Acceptance Criterion

↓

Validation Check

↓

Evidence

↓

Acceptance Decision
```

Traceability improves audits and debugging.

---

# Acceptance and Human Approval

Some tasks require human approval even after technical validation.

Examples include:

* Architecture changes.
* Production deployment.
* Security policy changes.
* Destructive operations.
* Risk acceptance.

Technical validation does not replace approval authority.

---

# Completion Reporting

An AI agent should report completion only after required validation and acceptance evaluation.

A completion report may include:

```text
Status

Objective

Files Changed

Validation Performed

Validation Results

Acceptance Criteria

Observations

Remaining Risks
```

Completion reporting should remain concise and factual.

---

# False Completion

Avoid reporting success based on:

* Code generation alone.
* Compilation alone.
* Passing one test.
* Agent confidence.
* Visual inspection only.

False completion is one of the highest-risk failure modes in AI-assisted engineering.

---

# Partial Validation

Sometimes not all validation can run.

The agent should report:

* Which checks ran.
* Which checks could not run.
* Why.
* Effect on acceptance confidence.

Missing validation should never be hidden.

---

# Deferred Validation

Some validation may require later environments.

Examples include:

* Staging.
* Production-like load testing.
* External integration testing.

Deferred validation should be identified as an explicit remaining gate.

---

# Manual Validation

Some criteria require human or manual verification.

Examples include:

* UX behavior.
* Architectural rationale.
* Business semantics.
* Visual output.

Manual validation should be documented when automation is insufficient.

---

# Automated Validation

Automation should be preferred where outcomes are objectively testable.

Benefits include:

* Repeatability.
* Speed.
* Reduced interpretation variance.
* Continuous enforcement.

Automation should verify engineering rules that should remain permanently true.

---

# Continuous Validation

Important quality gates should run continuously where practical.

Examples include:

* Build.
* Unit tests.
* Static analysis.
* Architecture tests.
* Dependency scanning.
* Documentation validation.

Continuous validation prevents known standards from becoming optional.

---

# Validation as Code

Validation rules should be executable when practical.

Examples include:

* Tests.
* Linters.
* Policy checks.
* Architecture rules.
* Schemas.

Executable rules reduce dependence on manual interpretation.

---

# Validation and Multi-Agent Workflows

Validation responsibilities may be separated from implementation.

Example:

```text
Implementation Agent
        ↓
Produces change

Validation Agent
        ↓
Executes quality checks

Review Agent
        ↓
Evaluates evidence

Human
        ↓
Approves high-risk outcome
```

Separation strengthens independence.

---

# Validation Handoffs

A handoff should include sufficient evidence for the next participant.

Example:

```text
Implementation complete.

Validation:
- Build passed.
- 42 tests passed.
- Architecture checks passed.
- Security validation pending.
```

Handoffs should preserve uncertainty.

---

# Validation and AI Autonomy

Validation rigor should increase with AI autonomy.

```text
Advisory AI
    ↓
Human Verification

Coding Agent
    ↓
Automated Build + Tests

Tool-Executing Agent
    ↓
Automated Validation + Scope Verification

Autonomous Workflow
    ↓
Policy Gates + Independent Validation + Approval
```

Greater execution capability requires stronger evidence.

---

# Validation Quality

Validation itself should be reviewed for quality.

Weak validation may produce false confidence.

Examples include:

* Tests that assert nothing meaningful.
* Build-only acceptance.
* Security scans configured incorrectly.
* Benchmarks with unrealistic workloads.

Validation evidence is valuable only when the validation method is relevant.

---

# Acceptance Drift

Acceptance criteria should remain aligned with evolving requirements.

Outdated criteria may validate the wrong behavior.

Criteria should be reviewed when:

* Requirements change.
* Architecture changes.
* Public contracts change.
* Risks change.

---

# Common Validation Anti-Patterns

Avoid:

## Generation Equals Completion

```text
The file was created, so the task is complete.
```

## Build-Only Validation

Assuming compilation proves behavioral correctness.

## Validation Bypass

Disabling checks to make the task pass.

## Vague Acceptance

```text
Everything looks correct.
```

## Missing Evidence

Claiming tests passed without reporting the executed validation.

## Over-Validation

Running excessive unrelated checks for low-risk work.

## Under-Validation

Using minimal checks for high-risk changes.

---

# Engineering Recommendations

Prompt authors should:

* Define acceptance criteria before execution.
* Align criteria with the objective.
* Choose relevant validation categories.
* Require observable evidence.
* Distinguish validation from acceptance.
* Scale validation with risk.
* Prefer independent validation where practical.
* Prevent validation bypass.
* Report pre-existing failures separately.
* Define partial validation behavior.
* Require human approval where authority demands it.
* Preserve validation evidence in completion reports.
* Continuously improve validation rules.

---

# Success Criteria

A prompt satisfies this guideline when:

* Acceptance criteria are explicit.
* Required validation is identifiable.
* Validation produces observable evidence.
* Validation scope matches task risk.
* Failures are reported accurately.
* Required checks cannot be silently bypassed.
* Scope compliance contributes to acceptance.
* Partial validation remains visible.
* Human approval requirements are explicit.
* Completion status can be justified objectively.
* Another engineer or agent can reproduce the acceptance decision from available evidence.

---

# Related Guidelines

This guideline should be used together with:

* 01-prompt-quality-principles.md
* 02-prompt-clarity.md
* 03-context-management.md
* 04-scope-and-boundaries.md
* 05-instruction-design.md
* 06-output-contracts.md
* 08-error-and-ambiguity-handling.md
* 09-security-and-safety.md
* 10-prompt-review.md

---

# Conclusion

Validation and acceptance convert AI-generated work into engineering confidence.

The core model is:

```text
Requirement

↓

Output

↓

Validation

↓

Evidence

↓

Acceptance Criteria

↓

Acceptance Decision
```

AI-assisted work should not be considered complete because the system generated code, documentation, or configuration successfully.

It is complete only when the result can be verified against its engineering contract.

The central principle is:

> **Completion is a claim. Validation provides evidence. Acceptance is the engineering decision that the evidence is sufficient.**
