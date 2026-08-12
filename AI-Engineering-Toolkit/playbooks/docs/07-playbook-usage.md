# Playbook Usage

## Purpose

Explains how engineers and AI agents discover, select, combine, execute,
and review Toolkit playbooks.

This document belongs to the **Toolkit Documentation** milestone of the
AI Engineering Toolkit. Its role is explanatory and navigational: it
helps engineers understand how to use the Toolkit as a coherent system
without creating a competing layer of standards.

------------------------------------------------------------------------

## Documentation Contract

Toolkit Documentation should:

-   Explain how Toolkit assets fit together.
-   Help engineers and AI agents find the correct source of authority.
-   Provide practical adoption and usage guidance.
-   Link engineering intent to playbooks, standards, prompts, templates,
    and references.
-   Make validation and review expectations understandable.
-   Avoid duplicating detailed rules already owned by authoritative
    assets.
-   Remain useful independently of any single AI product or IDE.

The documentation follows this authority model:

``` text
Explicit Repository / Task Requirements
              ↓
Architecture and Engineering Standards
              ↓
Applicable Playbooks
              ↓
Prompt Framework and Quality Guidance
              ↓
Reference Implementations
              ↓
Toolkit Documentation and Navigation
              ↓
Tool or Agent Preference
```

Documentation explains the system; it does not silently override it.

------------------------------------------------------------------------

## Core Usage Model

The Toolkit supports a repeatable engineering lifecycle:

``` text
Engineering Goal
      ↓
Discover Context
      ↓
Resolve Authority
      ↓
Select Guidance
      ↓
Plan
      ↓
Construct Execution Instructions
      ↓
Implement
      ↓
Validate
      ↓
Review
      ↓
Integrate and Learn
```

Each document in this milestone explains one part of that lifecycle or
the system that supports it.

------------------------------------------------------------------------

## What a Playbook Is

### Purpose

This area exists to make **what a playbook is** explicit, discoverable,
and repeatable across Toolkit-assisted engineering work. It should help
both human engineers and AI agents understand what information is
authoritative, what actions are expected, and what evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## When to Use Playbooks

### Purpose

This area exists to make **when to use playbooks** explicit,
discoverable, and repeatable across Toolkit-assisted engineering work.
It should help both human engineers and AI agents understand what
information is authoritative, what actions are expected, and what
evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## Playbook Discovery

### Purpose

This area exists to make **playbook discovery** explicit, discoverable,
and repeatable across Toolkit-assisted engineering work. It should help
both human engineers and AI agents understand what information is
authoritative, what actions are expected, and what evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## Selecting the Correct Playbook

### Purpose

This area exists to make **selecting the correct playbook** explicit,
discoverable, and repeatable across Toolkit-assisted engineering work.
It should help both human engineers and AI agents understand what
information is authoritative, what actions are expected, and what
evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## Reading Sequence

### Purpose

This area exists to make **reading sequence** explicit, discoverable,
and repeatable across Toolkit-assisted engineering work. It should help
both human engineers and AI agents understand what information is
authoritative, what actions are expected, and what evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## Combining Playbooks

### Purpose

This area exists to make **combining playbooks** explicit, discoverable,
and repeatable across Toolkit-assisted engineering work. It should help
both human engineers and AI agents understand what information is
authoritative, what actions are expected, and what evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## Applying Playbooks to Existing Repositories

### Purpose

This area exists to make **applying playbooks to existing repositories**
explicit, discoverable, and repeatable across Toolkit-assisted
engineering work. It should help both human engineers and AI agents
understand what information is authoritative, what actions are expected,
and what evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## Playbooks in AI Prompts

### Purpose

This area exists to make **playbooks in ai prompts** explicit,
discoverable, and repeatable across Toolkit-assisted engineering work.
It should help both human engineers and AI agents understand what
information is authoritative, what actions are expected, and what
evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## Execution Boundaries

### Purpose

This area exists to make **execution boundaries** explicit,
discoverable, and repeatable across Toolkit-assisted engineering work.
It should help both human engineers and AI agents understand what
information is authoritative, what actions are expected, and what
evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## Validation

### Purpose

This area exists to make **validation** explicit, discoverable, and
repeatable across Toolkit-assisted engineering work. It should help both
human engineers and AI agents understand what information is
authoritative, what actions are expected, and what evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## Common Misuse

### Purpose

This area exists to make **common misuse** explicit, discoverable, and
repeatable across Toolkit-assisted engineering work. It should help both
human engineers and AI agents understand what information is
authoritative, what actions are expected, and what evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## Playbook Evolution

### Purpose

This area exists to make **playbook evolution** explicit, discoverable,
and repeatable across Toolkit-assisted engineering work. It should help
both human engineers and AI agents understand what information is
authoritative, what actions are expected, and what evidence is required.

### Guidance

Apply the following principles:

-   Start from the engineering objective rather than from a preferred
    tool.
-   Inspect repository-local guidance before introducing new
    conventions.
-   Keep responsibilities, scope, assumptions, and boundaries explicit.
-   Prefer small, reviewable changes over broad speculative
    modifications.
-   Use automation when it produces reproducible evidence.
-   Preserve security, maintainability, documentation, and testability
    as part of the delivery contract.
-   Record important deviations when the repository intentionally
    differs from a Toolkit example.

A useful operating pattern is:

``` text
Intent
  ↓
Context
  ↓
Applicable Toolkit Guidance
  ↓
Decision
  ↓
Execution
  ↓
Validation Evidence
  ↓
Review
```

### Human and AI Responsibilities

The engineer remains accountable for intent, acceptance, risk, and
architectural decisions. AI tools may accelerate discovery, planning,
implementation, documentation, and validation, but they should operate
inside explicit boundaries.

AI-generated claims such as "implemented", "tested", or "validated"
should be supported by observable evidence whenever the repository
permits that evidence to be produced.

### Validation

Before treating this area as complete, confirm that:

-   The relevant guidance can be located without hidden conversational
    context.
-   Terminology is consistent with the rest of the Toolkit.
-   The described workflow does not contradict a higher-authority
    standard or architecture decision.
-   Commands, paths, and examples are safe and understandable.
-   Another engineer could reproduce the intended process.
-   AI agents can distinguish required behavior from optional examples.

------------------------------------------------------------------------

## Practical Adoption Checklist

Use this checklist when applying the guidance in this document:

-   [ ] The engineering objective is explicit.
-   [ ] Repository-local instructions have been inspected.
-   [ ] Applicable Toolkit assets have been identified.
-   [ ] Authority conflicts have been resolved before implementation.
-   [ ] Scope and modification boundaries are clear.
-   [ ] AI tools have only the context and permissions required for the
    task.
-   [ ] Implementation is incremental and reviewable.
-   [ ] Validation commands and acceptance criteria are known.
-   [ ] Results include evidence rather than unsupported completion
    claims.
-   [ ] Documentation is updated when behavior or workflow changes.
-   [ ] Human review remains the final acceptance boundary for material
    changes.

------------------------------------------------------------------------

## Relationship to Other Toolkit Assets

This document should be read together with the relevant subset of:

-   Engineering Standards.
-   Architecture documentation.
-   Bootstrap Playbooks.
-   PowerShell Playbooks.
-   GitHub Playbooks.
-   .NET Engineering Playbooks.
-   Prompt Framework.
-   Prompt Quality Guidelines.
-   Templates.
-   Reference Implementations.
-   AI-assisted engineering workflow guidance.
-   Roadmap and project status documentation.

The correct subset depends on the task. Engineers should avoid loading
every Toolkit asset into an AI context when a smaller authoritative set
is sufficient.

------------------------------------------------------------------------

## Maintenance

Update this document when:

-   Toolkit structure changes.
-   New asset families are introduced.
-   Authority relationships change.
-   Engineering workflows materially change.
-   AI-assisted development practices evolve.
-   Validation or review expectations change.
-   Examples become misleading or obsolete.

Documentation changes should be reviewed for consistency with the assets
they describe.

------------------------------------------------------------------------

## Success Criteria

This document succeeds when an engineer or AI agent can use it to:

-   Understand the responsibility of **Playbook Usage**.
-   Navigate to the correct authoritative Toolkit assets.
-   Apply the guidance without relying on hidden context.
-   Avoid confusing explanatory documentation with engineering
    standards.
-   Produce a scoped, reviewable engineering outcome.
-   Identify how that outcome should be validated.
-   Explain what evidence is required before accepting the work.

------------------------------------------------------------------------

# Conclusion

Explains how engineers and AI agents discover, select, combine, execute,
and review Toolkit playbooks.

The Toolkit Documentation layer exists to reduce navigation and adoption
friction across an increasingly capable engineering system.

The governing model is:

``` text
Understand
   ↓
Navigate
   ↓
Select Authority
   ↓
Execute Deliberately
   ↓
Validate Objectively
   ↓
Review
   ↓
Improve
```

The central principle is:

> **Toolkit documentation is successful when it helps humans and AI
> agents find the right guidance, understand how the pieces cooperate,
> and move from engineering intent to validated implementation without
> creating another competing source of truth.**
