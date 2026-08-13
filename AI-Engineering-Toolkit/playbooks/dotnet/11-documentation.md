# .NET Documentation

## Purpose

This playbook defines how documentation should be designed, maintained, validated, and reviewed for .NET engineering assets.

Documentation is part of the engineering system. It should explain intent, contracts, architecture, operational behavior, and important decisions without becoming a duplicate implementation that immediately drifts from the code.

---

## Objectives

This playbook aims to ensure that .NET documentation is:

- Accurate.
- Discoverable.
- Maintained with the code it describes.
- Appropriate to its audience.
- Explicit about contracts and constraints.
- Useful to both human engineers and AI coding agents.
- Validated during engineering changes.
- Free from unnecessary duplication.

---

## Documentation Principles

Documentation should follow these principles:

1. Document intent before incidental implementation detail.
2. Keep authoritative information in one clear location.
3. Place documentation close to the asset it governs when practical.
4. Update documentation in the same change that modifies documented behavior.
5. Prefer examples that can be validated.
6. Avoid documentation that merely restates obvious code.
7. Record architectural and operational decisions that cannot be inferred safely.
8. Treat outdated documentation as a defect.

---

## Documentation Layers

A .NET repository commonly needs several documentation layers.

```text
Repository Documentation
        ↓
Architecture Documentation
        ↓
Engineering Documentation
        ↓
Project / Module Documentation
        ↓
Public API Documentation
        ↓
Operational Documentation
```

Each layer should have a distinct responsibility.

---

## Repository Documentation

Repository-level documentation should explain:

- Project purpose.
- Repository structure.
- Prerequisites.
- Restore/build/test commands.
- Contribution workflow.
- Important navigation links.
- Current architectural entry points.

`README.md` should normally act as the primary entry point rather than contain every detail.

---

## Architecture Documentation

Architecture documentation should capture:

- System boundaries.
- Architectural style.
- Dependency direction.
- Module responsibilities.
- Public contracts.
- Integration boundaries.
- Important constraints.
- Significant design decisions.

Architecture documentation should describe the intended system and remain synchronized with executable architecture rules where those rules can be automated.

---

## Engineering Documentation

Engineering documentation should describe how contributors work with the codebase.

Typical topics include:

- Coding standards.
- Dependency management.
- Testing.
- Logging and observability.
- Error handling.
- Security.
- Performance.
- Build and validation workflows.

The repository should avoid duplicating the same rule across many documents.

---

## Project and Module Documentation

A project or module should receive dedicated documentation when its responsibility cannot be understood safely from its name, public contracts, and architecture documentation.

Useful project documentation may explain:

- Responsibility.
- Allowed dependencies.
- Public entry points.
- Configuration.
- Extension points.
- Known constraints.

Do not create empty `README.md` files merely to make every directory look documented.

---

## Code Documentation

Code documentation should add information that the code itself cannot communicate clearly.

Useful examples include:

- Non-obvious invariants.
- Important algorithmic decisions.
- Security-sensitive assumptions.
- Performance constraints.
- Interoperability requirements.
- Reasons for unusual implementation choices.

Avoid comments such as:

```csharp
// Increment counter.
counter++;
```

Prefer comments that explain why a non-obvious choice exists.

---

## XML Documentation

XML documentation comments are appropriate for public APIs when consumers require contract information that is not sufficiently clear from names and types.

Example:

```csharp
/// <summary>
/// Loads market observations for the requested interval.
/// </summary>
/// <param name="request">The validated market-data request.</param>
/// <param name="cancellationToken">Cancellation token for the operation.</param>
/// <returns>The available observations for the requested interval.</returns>
public Task<IReadOnlyCollection<MarketObservation>> LoadAsync(
    MarketDataRequest request,
    CancellationToken cancellationToken);
```

XML documentation should describe the contract rather than narrate implementation.

---

## Public API Documentation

Public APIs should document information such as:

- Required inputs.
- Returned values.
- Failure behavior.
- Cancellation semantics.
- Thread-safety expectations where relevant.
- Side effects.
- Compatibility constraints.

The more broadly an API is consumed, the stronger its documentation contract should be.

---

## Configuration Documentation

Configuration should document:

- Configuration keys.
- Purpose.
- Data type.
- Required or optional status.
- Safe default.
- Environment-specific behavior.
- Security classification.

Secrets must never be placed directly in documentation examples.

Use placeholders such as:

```text
<API_KEY>
<CONNECTION_STRING>
```

---

## Error Documentation

Important public or operational workflows should document expected failure categories.

Documentation should help consumers distinguish:

```text
Validation Failure
Business Failure
Dependency Failure
Transient Failure
Configuration Failure
Unexpected Failure
```

Do not expose internal sensitive details simply to make an error example realistic.

---

## Logging and Observability Documentation

Operational documentation should explain:

- Important log events.
- Correlation model.
- Metrics.
- Traces.
- Health checks.
- Diagnostic procedures.

The documentation should focus on how engineers understand system behavior rather than listing every possible log message.

---

## Testing Documentation

Testing documentation should explain:

- Test project organization.
- Test categories.
- How to execute tests.
- Integration-test prerequisites.
- Test data rules.
- Expected local and CI behavior.

Example:

```text
dotnet test
```

Repository scripts should remain the preferred entry point when they encapsulate additional validation.

---

## Build Documentation

The repository should document the canonical build workflow.

For example:

```text
Restore
  ↓
Build
  ↓
Test
  ↓
Format Validation
  ↓
Architecture Validation
  ↓
Verify
```

Developers should not need undocumented machine-specific steps to produce a valid build.

---

## Examples

Examples should:

- Demonstrate supported behavior.
- Remain small.
- Avoid secrets.
- Avoid obsolete APIs.
- Match current naming and architecture.
- Be testable where practical.

Examples that cannot be maintained should be removed rather than allowed to become misleading.

---

## Diagrams

Diagrams are useful when relationships are easier to understand visually.

Good candidates include:

- System context.
- Dependency direction.
- Data flow.
- Request lifecycle.
- Deployment topology.

Prefer diagrams that can be maintained in source form.

A diagram should complement explanatory text rather than replace it.

---

## Decision Documentation

Important engineering decisions should record:

- Context.
- Decision.
- Alternatives considered.
- Consequences.
- Status.

Decision records are especially useful when future engineers might otherwise "simplify" a deliberate architectural constraint.

---

## Documentation and Source Control

Documentation belongs under source control when it describes repository engineering behavior.

Documentation changes should be reviewed through the same pull-request process as code changes.

A behavior change that invalidates documentation is incomplete until the relevant documentation is updated.

---

## Documentation Review

Reviewers should verify:

- Accuracy.
- Clarity.
- Correct audience.
- Consistency with implementation.
- Consistency with architecture.
- Valid commands and paths.
- Security of examples.
- Absence of unnecessary duplication.

Documentation review should be part of normal engineering review.

---

## Documentation Validation

Where practical, automate validation for:

- Broken internal links.
- Invalid code examples.
- Formatting.
- Generated API documentation.
- Documentation references to removed assets.

Automation cannot determine whether every explanation is conceptually correct, so human review remains necessary.

---

## Documentation Drift

Documentation drift occurs when documentation and implementation describe different realities.

Common causes include:

- Renamed projects.
- Changed commands.
- Moved files.
- Modified APIs.
- Changed architecture.
- Deprecated dependencies.

Documentation should be inspected whenever these changes occur.

---

## Documentation for AI-Assisted Engineering

AI coding agents require reliable repository context.

Documentation intended to guide AI-assisted work should:

- State authority clearly.
- Use stable terminology.
- Define boundaries explicitly.
- Avoid contradictory instructions.
- Keep examples distinct from mandatory rules.
- Identify validation requirements.
- Avoid depending on hidden conversational history.

Good documentation reduces prompt size because authoritative repository context can be referenced rather than recreated repeatedly.

---

## AI Documentation Boundaries

An AI agent may help:

- Draft documentation.
- Update changed paths and commands.
- Produce examples.
- Identify potential drift.
- Check terminology.
- Summarize implementation behavior.

It should not independently redefine:

- Architecture.
- Security policy.
- Public contracts.
- Engineering standards.

without explicit approval.

---

## Documentation Change Workflow

Use the following workflow:

```text
Engineering Change
      ↓
Identify Documentation Impact
      ↓
Update Authoritative Documents
      ↓
Update Examples / Navigation
      ↓
Validate
      ↓
Review Code + Documentation Together
```

Documentation should not be postponed indefinitely to a separate cleanup milestone.

---

## Documentation Checklist

Before accepting a .NET engineering change, ask:

- [ ] Does the change alter documented behavior?
- [ ] Are commands and paths still correct?
- [ ] Are public contracts documented where necessary?
- [ ] Are configuration changes documented?
- [ ] Are architecture diagrams still accurate?
- [ ] Are examples current?
- [ ] Are security-sensitive examples safe?
- [ ] Are links and navigation valid?
- [ ] Is duplicated documentation being introduced?
- [ ] Can an engineer understand the change without relying on chat history?
- [ ] Can an AI coding agent locate the authoritative guidance?

---

## Anti-Patterns

Avoid:

- Documentation generated only to satisfy a checklist.
- Large documents that duplicate source code.
- Comments explaining obvious syntax.
- Multiple conflicting sources of truth.
- Examples containing real credentials.
- Architecture documentation that no longer matches dependency rules.
- README files with no meaningful responsibility.
- Undocumented build prerequisites.
- Documentation that exists only in chat conversations.
- AI-generated documentation accepted without technical review.

---

## Acceptance Criteria

Documentation is acceptable when:

- Its purpose and audience are clear.
- It accurately represents current behavior.
- Important contracts and constraints are explicit.
- It is discoverable from normal repository navigation.
- It does not unnecessarily duplicate authoritative guidance.
- Examples are safe and current.
- Relevant commands are reproducible.
- Documentation changes accompany behavior changes.
- Human reviewers can verify its correctness.
- AI coding agents can use it without confusing examples with authority.

---

## Related Playbooks

This playbook should be used together with:

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
12-project-review.md
```

Documentation is a cross-cutting engineering responsibility and should reflect the decisions governed by these playbooks.

---

# Conclusion

Documentation is part of the .NET engineering contract, not a final publishing step.

The recommended model is:

```text
Engineering Intent
      ↓
Implementation
      ↓
Documentation Impact
      ↓
Authoritative Update
      ↓
Validation
      ↓
Review
      ↓
Shared Engineering Knowledge
```

High-quality documentation makes architecture, contracts, operational behavior, and engineering expectations discoverable without forcing future contributors to reconstruct intent from code or conversation history.

For AI-first engineering, this becomes even more important: coding agents can only act reliably when the repository contains accurate, explicit, and appropriately scoped context.

The central principle is:

> **Document the knowledge required to understand, operate, validate, and safely evolve the system, and maintain that documentation as part of the same engineering lifecycle as the code it describes.**
