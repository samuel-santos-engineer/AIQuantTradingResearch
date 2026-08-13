# .NET Project Review

## Purpose

This playbook defines a systematic review process for .NET projects.

A project review evaluates whether a .NET solution remains aligned with its architecture, engineering standards, quality expectations, security requirements, operational needs, and maintainability goals.

The review is evidence-based. It combines repository inspection, automated validation, engineering judgment, and explicit findings rather than relying on code aesthetics alone.

---

## Objectives

A .NET project review should determine whether:

- Architecture remains coherent.
- Project boundaries remain meaningful.
- Dependency direction is respected.
- Domain modeling is appropriate.
- Code follows repository standards.
- Error handling is predictable.
- Logging and observability are useful.
- Tests provide meaningful confidence.
- Security expectations are satisfied.
- Performance risks are understood.
- Documentation matches reality.
- Build and validation workflows remain reproducible.
- AI-generated changes remain reviewable and governed.

---

## Review Philosophy

The review should answer:

```text
Is the system structured correctly?
        ↓
Does it behave correctly?
        ↓
Can failures be understood?
        ↓
Can changes be made safely?
        ↓
Can quality be demonstrated?
        ↓
Can another engineer understand why the system looks this way?
```

A successful build alone does not answer these questions.

---

## Review Scope

The review scope should be explicit before work begins.

Possible scopes include:

- Entire solution.
- Individual project.
- Module or bounded context.
- Release.
- Pull request.
- Architecture slice.
- Engineering infrastructure.

Avoid accidentally turning a focused review into an unrestricted redesign.

---

## Review Inputs

Useful inputs include:

- Solution files.
- Project files.
- Architecture documentation.
- Engineering standards.
- Dependency rules.
- Source code.
- Tests.
- Build configuration.
- Package configuration.
- Engineering scripts.
- CI configuration.
- Security guidance.
- Operational documentation.
- Relevant decision records.

Repository-local authoritative sources should be preferred over assumptions.

---

## Review Order

A recommended sequence is:

```text
Context
   ↓
Architecture
   ↓
Project Structure
   ↓
Dependencies
   ↓
Domain Design
   ↓
Implementation Quality
   ↓
Error Handling
   ↓
Logging / Observability
   ↓
Testing
   ↓
Security
   ↓
Performance
   ↓
Documentation
   ↓
Build / Validation
   ↓
Findings and Decision
```

Reviewing higher-level constraints first prevents local implementation preferences from dominating architectural concerns.

---

## Architecture Review

Verify:

- Architectural style is still recognizable.
- Responsibilities are separated appropriately.
- Dependency direction matches documented rules.
- Infrastructure concerns do not leak into inner layers.
- Composition roots remain explicit.
- New capabilities have not bypassed intended boundaries.

Questions:

- Can each project responsibility be explained clearly?
- Are boundaries protecting change?
- Has convenience created architectural coupling?
- Are documented rules executable where practical?

---

## Project Structure Review

Inspect:

- Solution organization.
- Project naming.
- Folder responsibilities.
- Test project alignment.
- Host projects.
- Shared assets.

Look for:

- Speculative folders.
- Generic `Common` or `Helpers` dumping grounds.
- Projects with no meaningful boundary.
- Deep structures that do not represent domain or technical responsibilities.
- Template/sample files left in production repositories.

Structure should emerge from responsibility.

---

## Dependency Review

Review:

- `ProjectReference` relationships.
- NuGet dependencies.
- Central package management.
- Version consistency.
- Transitive dependencies.
- Unused dependencies.
- Technology leakage across boundaries.

The dependency graph should match architecture rather than gradually redefine it.

---

## Domain Design Review

Where domain behavior exists, review:

- Ubiquitous language.
- Entities.
- Value objects.
- Aggregates.
- Invariants.
- Domain events.
- Domain services.
- Repository abstractions.
- Bounded context boundaries.

Avoid judging domain quality solely by class count or pattern usage.

The important question is whether the model expresses business behavior correctly.

---

## Coding Standards Review

Review implementation for:

- Naming.
- Readability.
- Nullable correctness.
- Async patterns.
- Cancellation.
- Immutability where appropriate.
- API clarity.
- Complexity.
- Duplication.
- Language feature usage.

Repository standards should govern style decisions.

Do not introduce reviewer-specific preferences as undocumented mandatory rules.

---

## Error Handling Review

Verify:

- Expected failures are modeled appropriately.
- Exceptions are used intentionally.
- Exceptions are not swallowed.
- Error context is preserved.
- External dependency failures are translated at suitable boundaries.
- Cancellation is not incorrectly treated as an unexpected error.
- User-facing errors do not expose sensitive internals.

Error behavior is part of the contract.

---

## Logging and Observability Review

Review:

- Structured logging.
- Event usefulness.
- Log levels.
- Correlation.
- Metrics.
- Tracing.
- Health signals.
- Diagnostic context.

Look for:

- Sensitive data in logs.
- Excessive informational noise.
- Missing failure context.
- String interpolation that defeats structured logging.
- Telemetry that cannot answer operational questions.

---

## Testing Review

Evaluate:

- Unit tests.
- Domain tests.
- Integration tests.
- Architecture tests.
- Contract tests where applicable.
- End-to-end tests where justified.
- Regression coverage.

Tests should provide confidence at appropriate boundaries.

Review for:

- Determinism.
- Isolation.
- Meaningful assertions.
- Excessive mocking.
- Duplicate coverage.
- Missing failure scenarios.
- Tests coupled to implementation details.

Coverage percentage alone is not sufficient evidence of test quality.

---

## Architecture Test Review

Architecture rules that matter should be executable where practical.

Examples:

```text
Domain !→ Infrastructure

Application !→ Infrastructure

Application !→ Host

Infrastructure !→ Host
```

Review architecture tests whenever project boundaries change.

A passing architecture test suite should confirm actual relationships, not merely assert hard-coded expected values.

---

## Security Review

Review:

- Input validation.
- Authentication and authorization where applicable.
- Secrets handling.
- Configuration security.
- Dependency risk.
- Data protection.
- Logging privacy.
- Least privilege.
- External command execution.
- File and path handling.

Security findings should be prioritized by risk rather than by stylistic preference.

---

## Performance Review

Review performance where the workload makes it relevant.

Consider:

- Allocation patterns.
- I/O.
- Concurrency.
- Database access.
- Network calls.
- Serialization.
- Collection behavior.
- Hot paths.
- Caching.
- Resource lifetime.

Do not optimize speculative bottlenecks.

Performance findings should be supported by workload understanding, measurement, or credible risk.

---

## Documentation Review

Verify:

- README and navigation remain accurate.
- Architecture documents match implementation.
- Build/test commands work.
- Configuration is documented.
- Public contracts are documented where required.
- Examples remain current.
- Decision records explain important constraints.

Documentation drift should be recorded as a project quality finding.

---

## Build Configuration Review

Inspect:

```text
global.json

Directory.Build.props

Directory.Packages.props

.editorconfig
```

and relevant project files.

Verify:

- SDK expectations are explicit.
- Shared properties are centralized.
- Package versions are centralized where required.
- Nullable/analyzer/warning policies remain active.
- Individual projects do not bypass repository standards without justification.

---

## Engineering Automation Review

Review repository scripts and automation for:

- Restore.
- Build.
- Test.
- Format.
- Clean.
- Verify.

The preferred local validation path should be easy to discover.

Automation should avoid:

- Hidden machine assumptions.
- Duplicate logic.
- Unsafe destructive operations.
- Silent failures.
- Inconsistent exit behavior.

---

## Reproducibility Review

A healthy project should be reproducible from a clean repository state.

Where practical, verify:

```text
Clean Checkout
    ↓
Resolve SDK
    ↓
Restore
    ↓
Build
    ↓
Test
    ↓
Quality Validation
    ↓
Success
```

Undocumented manual setup is a maintainability risk.

---

## AI-Assisted Engineering Review

For AI-generated or AI-modified work, review:

- Whether the agent used authoritative repository context.
- Whether scope was respected.
- Whether unrelated files were modified.
- Whether dependencies were introduced without justification.
- Whether validation claims have evidence.
- Whether generated abstractions are actually needed.
- Whether documentation was updated.
- Whether security boundaries were preserved.

AI-generated code should meet the same engineering bar as human-generated code.

---

## Finding Classification

Findings should be classified consistently.

### Critical

Immediate security, data integrity, production safety, or fundamental architecture risk.

### High

Material correctness, maintainability, reliability, or architecture problem that should block acceptance.

### Medium

Important quality problem that should be addressed but may not block all progress.

### Low

Localized improvement with limited risk.

### Observation

Non-blocking information, future consideration, or optional improvement.

Severity should describe engineering impact, not reviewer preference.

---

## Finding Structure

Each finding should include:

```text
ID
Title
Severity
Area
Evidence
Impact
Recommendation
Acceptance Requirement
```

Example:

```text
Finding: ARCH-001
Severity: High
Area: Architecture

Evidence:
Application references Infrastructure.

Impact:
The dependency direction violates the documented architecture
and couples use cases to implementation technology.

Recommendation:
Move the required abstraction into Application and inject an
Infrastructure implementation through the composition root.

Acceptance:
Architecture test passes and the forbidden project reference is removed.
```

---

## Evidence

Review findings should point to observable evidence such as:

- File path.
- Project reference.
- Test result.
- Build output.
- Configuration entry.
- Source location.
- Architecture rule.
- Reproducible command.

Evidence makes findings actionable.

---

## Review Outcomes

A project review may conclude with:

```text
Approved

Approved with Observations

Changes Required

Blocked
```

### Approved

No blocking findings remain.

### Approved with Observations

The project is acceptable, with documented non-blocking improvements.

### Changes Required

One or more findings must be resolved before acceptance.

### Blocked

The review cannot be completed because required evidence, environment, access, or foundational decisions are missing.

---

## Review Report

A project review report should summarize:

- Scope.
- Repository/revision reviewed.
- Applicable standards.
- Validation performed.
- Findings by severity.
- Positive observations where useful.
- Known limitations.
- Required actions.
- Final outcome.

Keep the report factual and evidence-based.

---

## Automated Review

Automation should be used for objective checks such as:

- Restore.
- Build.
- Tests.
- Formatting.
- Static analysis.
- Architecture rules.
- Dependency checks.
- Security scanning where configured.

Automated checks reduce review effort but do not replace architectural or domain judgment.

---

## Human Review

Human review remains essential for:

- Architectural fit.
- Domain correctness.
- Trade-offs.
- Maintainability.
- Security context.
- Operational appropriateness.
- Scope decisions.

The strongest review model combines automated evidence with engineering judgment.

---

## Review Cadence

Project reviews are useful:

- Before major releases.
- After significant architecture changes.
- After introducing important dependencies.
- Before major platform expansion.
- When quality drift is suspected.
- Periodically for long-lived systems.

Not every review needs to inspect the entire repository.

---

## Review Boundaries

A reviewer should not:

- Redesign unrelated areas.
- Add requirements after implementation without identifying them as new requirements.
- Treat personal style preferences as standards.
- Demand abstraction without a concrete responsibility.
- Introduce dependencies solely to satisfy review aesthetics.
- Ignore successful automated evidence without explaining why it is insufficient.

Review should improve engineering quality while respecting scope.

---

## Project Review Checklist

### Architecture

- [ ] Responsibilities are explicit.
- [ ] Dependency direction is correct.
- [ ] Composition boundaries are clear.
- [ ] Architecture rules are executable where practical.

### Structure

- [ ] Projects have meaningful responsibilities.
- [ ] Naming is consistent.
- [ ] Speculative structure is avoided.

### Dependencies

- [ ] Dependencies are justified.
- [ ] Versions follow repository policy.
- [ ] Forbidden dependencies do not exist.

### Implementation

- [ ] Code is readable and maintainable.
- [ ] Nullable/async/cancellation policies are respected.
- [ ] Complexity is appropriate.

### Errors and Observability

- [ ] Failure behavior is predictable.
- [ ] Exceptions retain context.
- [ ] Logging is structured and safe.
- [ ] Operational signals are sufficient.

### Testing

- [ ] Important behavior is tested.
- [ ] Tests are deterministic.
- [ ] Architecture rules are protected.
- [ ] Failure paths are covered appropriately.

### Security

- [ ] Inputs are validated.
- [ ] Secrets are protected.
- [ ] Least privilege is respected.
- [ ] Sensitive information is not exposed.

### Performance

- [ ] Known performance-sensitive paths are understood.
- [ ] No obvious unnecessary bottleneck is introduced.
- [ ] Optimization claims are evidence-based.

### Documentation

- [ ] Documentation matches implementation.
- [ ] Build and test commands are current.
- [ ] Important decisions are discoverable.

### Validation

- [ ] Restore succeeds.
- [ ] Build succeeds.
- [ ] Tests succeed.
- [ ] Formatting/static validation succeeds.
- [ ] Repository verification succeeds.

---

## Acceptance Criteria

A .NET project review is complete when:

- Scope is documented.
- Applicable guidance is identified.
- Architecture has been evaluated.
- Project structure and dependencies have been evaluated.
- Implementation quality has been evaluated.
- Error handling and observability have been evaluated.
- Testing has been evaluated.
- Security has been evaluated.
- Relevant performance concerns have been evaluated.
- Documentation has been evaluated.
- Build and engineering automation have been evaluated.
- Validation evidence has been captured.
- Findings include severity and evidence.
- Blocking findings are explicit.
- A final review outcome is recorded.

---

## Related Playbooks

This project review consolidates the review perspective of:

```text
01-solution-architecture.md
02-project-structure.md
03-domain-driven-design.md
04-dependency-management.md
05-coding-standards.md
06-error-handling.md
07-logging.md
08-testing.md
09-security.md
10-performance.md
11-documentation.md
```

Those documents contain the detailed engineering guidance.

This playbook provides the integrated review process.

---

# Conclusion

A .NET project review is a structured engineering assessment, not a stylistic inspection.

The review model is:

```text
Define Scope
    ↓
Resolve Authority
    ↓
Inspect Architecture
    ↓
Inspect Implementation
    ↓
Execute Validation
    ↓
Collect Evidence
    ↓
Classify Findings
    ↓
Resolve Blocking Risk
    ↓
Record Decision
```

The review should make it possible to explain not only whether the project builds, but whether its architecture, dependencies, behavior, tests, security, documentation, and engineering workflows remain healthy enough to support continued evolution.

In an AI-first engineering environment, project review also becomes the boundary that converts rapid AI-assisted implementation into trusted engineering output. Speed is useful only when generated changes remain explainable, testable, secure, and reviewable.

The central principle is:

> **A project should be accepted because its engineering quality can be demonstrated through architecture, behavior, validation evidence, and informed review—not merely because the code compiles or was produced quickly.**
