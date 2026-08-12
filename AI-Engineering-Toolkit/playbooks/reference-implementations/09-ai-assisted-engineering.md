# AI-Assisted Engineering Reference Implementation

## Purpose

Demonstrates how engineers and coding agents collaborate through
authoritative context, prompts, scoped execution, reference patterns,
validation evidence, review, and controlled iteration.

This document is a concrete reference implementation within the AI
Engineering Toolkit. It translates authoritative engineering guidance
into an implementation model that engineers and AI coding agents can
inspect, adapt, validate, and review.

A reference implementation demonstrates one strong solution. It does not
replace the standards, architecture, playbooks, prompt guidance, or
repository-specific requirements that govern real work.

------------------------------------------------------------------------

## Objectives

This reference implementation aims to:

-   Convert engineering guidance into a concrete and reviewable example.
-   Make important boundaries and responsibilities explicit.
-   Demonstrate production-oriented quality without unnecessary
    framework building.
-   Provide repeatable validation and acceptance criteria.
-   Support both human engineering and AI-assisted execution.
-   Keep security, maintainability, documentation, and testing part of
    the implementation contract.
-   Produce evidence that the demonstrated outcome is correct.

------------------------------------------------------------------------

## Scope

The scope is intentionally focused on the engineering concerns
represented by this reference. Application-specific choices should be
introduced only when they are necessary to demonstrate those concerns.

The reference should be:

-   Self-contained enough to study independently.
-   Small enough to understand.
-   Realistic enough to matter.
-   Buildable or executable where applicable.
-   Testable where behavior can be verified.
-   Safe to run in a local development environment.
-   Traceable to applicable Toolkit guidance.

------------------------------------------------------------------------

## Authority Model

When guidance conflicts, use the following conceptual priority:

``` text
Explicit Task Requirements
        ↓
Repository Architecture
        ↓
Engineering Standards
        ↓
Applicable Playbooks
        ↓
Prompt Quality Guidelines
        ↓
Reference Implementations
        ↓
Existing Local Patterns
        ↓
Agent Preference
```

Reference implementations therefore provide context and examples without
silently becoming policy.

------------------------------------------------------------------------

## Operating Model

Operating Model is part of the reference contract because it contributes
directly to the implementation's ability to be understood, executed,
reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Keep the engineer accountable for intent, boundaries, and
    acceptance.
-   Use AI agents as constrained engineering executors rather than
    sources of authority.
-   Require validation evidence before accepting generated changes.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Authority Hierarchy

Authority Hierarchy is part of the reference contract because it
contributes directly to the implementation's ability to be understood,
executed, reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of authority hierarchy explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Context Discovery

Context Discovery is part of the reference contract because it
contributes directly to the implementation's ability to be understood,
executed, reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of context discovery explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Prompt Contract

Prompt Contract is part of the reference contract because it contributes
directly to the implementation's ability to be understood, executed,
reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of prompt contract explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Planning

Planning is part of the reference contract because it contributes
directly to the implementation's ability to be understood, executed,
reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of planning explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Execution Boundaries

Execution Boundaries is part of the reference contract because it
contributes directly to the implementation's ability to be understood,
executed, reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of execution boundaries explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Reference Usage

Reference Usage is part of the reference contract because it contributes
directly to the implementation's ability to be understood, executed,
reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of reference usage explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Validation Loop

Validation Loop is part of the reference contract because it contributes
directly to the implementation's ability to be understood, executed,
reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of validation loop explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Human Review

Human Review is part of the reference contract because it contributes
directly to the implementation's ability to be understood, executed,
reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of human review explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Failure and Ambiguity Handling

Failure and Ambiguity Handling is part of the reference contract because
it contributes directly to the implementation's ability to be
understood, executed, reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of failure and ambiguity handling
    explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Security and Safety

Security and Safety is part of the reference contract because it
contributes directly to the implementation's ability to be understood,
executed, reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of security and safety explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Traceability

Traceability is part of the reference contract because it contributes
directly to the implementation's ability to be understood, executed,
reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of traceability explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Agent Handoff

Agent Handoff is part of the reference contract because it contributes
directly to the implementation's ability to be understood, executed,
reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of agent handoff explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Continuous Improvement

Continuous Improvement is part of the reference contract because it
contributes directly to the implementation's ability to be understood,
executed, reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of continuous improvement explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## Validation and Acceptance

Validation and Acceptance is part of the reference contract because it
contributes directly to the implementation's ability to be understood,
executed, reviewed, and maintained.

The reference should demonstrate the following expectations:

-   Define the responsibility of validation and acceptance explicitly.
-   Prefer deterministic, reviewable behavior over hidden conventions.
-   Keep implementation choices traceable to applicable Toolkit
    guidance.
-   Document important assumptions and tradeoffs.
-   Provide automated evidence where the requirement can be verified
    mechanically.
-   Avoid introducing complexity that is unrelated to the concept being
    demonstrated.

A useful engineering flow is:

``` text
Intent
  ↓
Applicable Guidance
  ↓
Implementation Decision
  ↓
Executable Change
  ↓
Validation Evidence
  ↓
Review
```

The implementation remains an example. Standards, architecture,
playbooks, and explicit task requirements retain authority over the
example when conflicts occur.

### Design Expectations

The design should make responsibilities and boundaries discoverable from
repository structure, names, documentation, and tests. Important
behavior must not depend on undocumented conversation history or
developer-specific machine state.

### Validation Expectations

Validation should answer three questions:

1.  Was the intended behavior implemented?
2.  Were applicable engineering constraints preserved?
3.  Is there objective evidence that another engineer or coding agent
    can reproduce the result?

Where automated validation is not practical, the reference should
identify the required review evidence explicitly.

------------------------------------------------------------------------

## AI-Assisted Engineering Contract

When an AI coding agent uses this reference, the expected workflow is:

``` text
Inspect Repository
      ↓
Read Authoritative Guidance
      ↓
Confirm Scope and Boundaries
      ↓
Study Relevant Reference
      ↓
Plan
      ↓
Implement Incrementally
      ↓
Run Validation
      ↓
Report Evidence
      ↓
Human Review
```

The agent should not copy patterns blindly. It should first verify that
the pattern fits the current architecture, scope, technology, and
security requirements.

The agent should report:

-   Files created or modified.
-   Important design decisions.
-   Commands executed.
-   Tests and checks performed.
-   Validation results.
-   Warnings, assumptions, and unresolved risks.

------------------------------------------------------------------------

## Quality Gates

A reference implementation should not be considered complete merely
because its files exist.

Acceptance requires appropriate evidence for:

-   Structural correctness.
-   Build or execution success where applicable.
-   Automated tests where applicable.
-   Static analysis where applicable.
-   Architecture conformance.
-   Security expectations.
-   Documentation completeness.
-   Repeatability.
-   Reviewability.

The preferred model is:

``` text
Implementation
    +
Automated Validation
    +
Security Review
    +
Documentation
    +
Human Review
    =
Accepted Reference
```

------------------------------------------------------------------------

## Maintenance and Evolution

This reference should evolve when the authoritative Toolkit guidance it
demonstrates changes.

Maintenance should consider:

-   Deprecated tools or dependencies.
-   Superseded architectural practices.
-   New security requirements.
-   New validation capabilities.
-   Changes to AI-assisted engineering workflows.
-   Drift between documentation and executable behavior.

Changes should preserve backward understanding: a future maintainer
should be able to determine why the reference changed and which guidance
motivated the change.

------------------------------------------------------------------------

## Review Checklist

Before accepting changes to this reference, verify:

-   [ ] The purpose and scope remain clear.
-   [ ] The implementation still demonstrates a focused engineering
    concept.
-   [ ] Applicable standards and playbooks are followed.
-   [ ] Architecture boundaries remain explicit.
-   [ ] Dependencies are justified.
-   [ ] Error and failure behavior is understandable.
-   [ ] Security requirements are preserved.
-   [ ] Tests cover meaningful behavior.
-   [ ] Validation can be reproduced.
-   [ ] Documentation matches the implementation.
-   [ ] AI-assisted changes include evidence rather than unsupported
    claims.
-   [ ] No reference-specific choice has accidentally been promoted into
    an undocumented global standard.

------------------------------------------------------------------------

## Acceptance Criteria

This reference implementation is accepted when:

-   Its intended engineering outcome is clearly documented.
-   Its structure and responsibilities are understandable.
-   Applicable behavior can be executed or inspected deterministically.
-   Required tests and validation pass.
-   Security boundaries are preserved.
-   Important decisions are traceable to authoritative guidance.
-   Repeated execution is safe where repeatability applies.
-   Another engineer can reproduce the validation process.
-   An AI coding agent can use the reference without requiring hidden
    context.
-   Human review can determine whether the implementation is appropriate
    for reuse.

------------------------------------------------------------------------

## Related Toolkit Areas

This reference should be interpreted together with the applicable:

-   Engineering Standards.
-   Bootstrap Playbooks.
-   PowerShell Playbooks.
-   GitHub Playbooks.
-   .NET Engineering Playbooks.
-   Prompt Framework.
-   Prompt Quality Guidelines.
-   Templates.
-   Architecture documentation.
-   AI-assisted engineering workflow guidance.

The exact subset depends on the engineering concern being demonstrated.

------------------------------------------------------------------------

## Success Criteria

The reference succeeds when it reduces ambiguity between engineering
guidance and implementation without creating a second source of
authority.

An engineer or AI coding agent should be able to determine:

-   What the implementation demonstrates.
-   Why its important decisions exist.
-   Which constraints govern it.
-   How to execute or inspect it.
-   How to validate it.
-   Which parts may be adapted.
-   Which parts must not be copied without contextual review.

------------------------------------------------------------------------

# Conclusion

Demonstrates how engineers and coding agents collaborate through
authoritative context, prompts, scoped execution, reference patterns,
validation evidence, review, and controlled iteration.

The reference model is:

``` text
Engineering Intent
      ↓
Authoritative Guidance
      ↓
Concrete Reference
      ↓
Scoped Implementation
      ↓
Automated Evidence
      ↓
Human Review
      ↓
Reusable Engineering Knowledge
```

The central principle is:

> **A reference implementation is valuable when it makes good
> engineering concrete, keeps its decisions explainable, and provides
> enough validation evidence that both humans and AI coding agents can
> adapt it safely without confusing the example with the standard.**
