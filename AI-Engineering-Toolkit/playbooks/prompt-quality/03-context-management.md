
# Context Management

## Purpose

The Context Management guideline defines the engineering principles and practices for identifying, selecting, prioritizing, providing, validating, and maintaining context used by AI systems within the AI Engineering Toolkit.

Its purpose is to ensure that prompts provide sufficient authoritative information for reliable execution while avoiding irrelevant, stale, contradictory, or excessive context.

Context should improve engineering understanding without becoming noise.

---

# Objectives

The Context Management guideline aims to:

* Improve AI understanding of engineering tasks.
* Reduce unsupported assumptions.
* Establish authoritative context sources.
* Improve repository-aware execution.
* Minimize irrelevant context.
* Prevent stale context from influencing decisions.
* Reduce contradictory instructions.
* Support repeatable execution.
* Improve prompt maintainability.
* Strengthen traceability.
* Enable reliable AI-assisted engineering.

---

# Scope

This guideline applies to prompts used for:

* Architecture analysis.
* Repository inspection.
* Software implementation.
* Refactoring.
* Testing.
* Documentation.
* Security analysis.
* Performance engineering.
* DevOps automation.
* GitHub operations.
* Code review.
* Project review.
* Validation.
* AI-assisted engineering workflows.

Context requirements should remain proportional to task complexity, risk, and execution authority.

---

# Engineering Philosophy

AI systems reason from the context available to them.

Poor context creates poor assumptions.

A useful model is:

```text
Engineering Task

↓

Relevant Context

↓

Correct Interpretation

↓

Controlled Execution

↓

Validated Outcome
```

Prompt quality therefore depends not only on instructions, but also on the quality of the information surrounding those instructions.

---

# Context as an Engineering Dependency

Context should be treated as an engineering dependency.

A prompt may depend on:

* Architecture documents.
* Domain definitions.
* Engineering standards.
* Playbooks.
* Existing source code.
* Configuration.
* Tests.
* GitHub issues.
* Repository state.
* Operational evidence.

Like any dependency, context should be:

* Necessary.
* Current.
* Traceable.
* Understandable.
* Governed.

---

# Context Categories

Engineering context may be divided into several categories.

```text
Strategic Context

Architecture Context

Domain Context

Repository Context

Task Context

Execution Context

Operational Context
```

Each category answers different engineering questions.

---

# Strategic Context

Strategic context explains why the engineering work exists.

It may include:

* Product goals.
* Roadmap objectives.
* Release goals.
* Business priorities.
* Engineering constraints.

Strategic context should be included only when it materially affects implementation decisions.

---

# Architecture Context

Architecture context defines structural and engineering boundaries.

It may include:

* Solution architecture.
* Module boundaries.
* Dependency rules.
* Public contracts.
* Integration patterns.
* Resilience principles.
* Security architecture.

Architecture context should guide implementation decisions without being duplicated unnecessarily inside prompts.

---

# Domain Context

Domain context defines business meaning.

It may include:

* Ubiquitous language.
* Bounded contexts.
* Entities.
* Value objects.
* Business rules.
* Domain events.
* Business invariants.

Domain terminology used in prompts should align with authoritative domain documentation.

---

# Repository Context

Repository context describes the current implementation environment.

It may include:

* Project structure.
* Existing source code.
* Tests.
* Build scripts.
* Dependency configuration.
* Naming conventions.
* Repository documentation.
* GitHub configuration.

Repository-aware agents should inspect current repository state before significant changes.

---

# Task Context

Task context defines the immediate engineering work.

It may include:

* GitHub issue.
* Acceptance criteria.
* Requested behavior.
* Known defects.
* Constraints.
* Related implementation.

Task context should remain focused on the current objective.

---

# Execution Context

Execution context describes the environment in which work will be performed.

It may include:

* Current branch.
* SDK version.
* Operating system.
* Available tools.
* Build commands.
* Test commands.
* Agent permissions.

Execution context becomes especially important when prompts invoke tools or modify repositories.

---

# Operational Context

Operational context describes real system behavior.

It may include:

* Logs.
* Metrics.
* Traces.
* Incidents.
* Performance results.
* Failure reports.
* Production constraints.

Operational evidence should be used when implementation decisions depend on actual system behavior.

---

# Authoritative Context

Prompts should distinguish authoritative information from supplementary information.

A recommended hierarchy is:

```text
Explicit Task Requirements

↓

Approved Architecture

↓

Engineering Standards

↓

Engineering Playbooks

↓

Domain Documentation

↓

Repository Documentation

↓

Current Implementation

↓

Historical Discussion

↓

Agent Assumption
```

Higher-authority sources should take precedence when conflicts occur.

---

# Repository Authority

Durable engineering knowledge should live in repository artifacts whenever practical.

Examples include:

* Architecture.
* Standards.
* Playbooks.
* ADRs.
* Domain documentation.
* Tests.
* Configuration.

Temporary AI conversation history should not become the only source of important engineering truth.

---

# Current State as Context

For implementation tasks, current repository state should normally be inspected before execution.

The agent should not assume that:

* Files still exist.
* Structure remains unchanged.
* Dependencies remain the same.
* Previous implementation decisions are still current.

Repository state evolves.

Context should reflect that evolution.

---

# Context Freshness

Context has a lifecycle.

Information may become stale when:

* Architecture changes.
* Files move.
* Dependencies change.
* APIs evolve.
* Standards are updated.
* Releases modify behavior.

Prompts should favor current authoritative sources over remembered information.

---

# Context Validation

Important context should be validated when practical.

Examples include:

```text
Prompt expects:
docs/architecture/SOLUTION_ARCHITECTURE.md

Agent:
Verify the file exists and inspect its current contents.
```

Do not silently proceed using assumptions when required context cannot be found.

---

# Context Discovery

Repository-aware prompts may instruct the agent to discover relevant context before implementation.

A useful sequence is:

```text
Inspect Repository

↓

Locate Authoritative Documents

↓

Read Applicable Standards

↓

Inspect Existing Implementation

↓

Inspect Tests

↓

Build Task Understanding

↓

Execute
```

Context discovery should occur before significant modification.

---

# Context Selection

Not every available artifact should be loaded or referenced.

Context should be selected according to:

* Relevance.
* Authority.
* Freshness.
* Scope.
* Risk.

The objective is the smallest sufficient context required for reliable execution.

---

# Context Minimization

Excessive context may reduce execution quality.

Too much context can introduce:

* Noise.
* Contradictory guidance.
* Distracting historical information.
* Increased processing cost.
* Reduced task focus.

Prefer targeted context over repository-wide information dumps.

---

# Context Relevance

Every context source should answer an engineering question relevant to the task.

For example:

```text
Task:
Add a new domain validation rule.

Relevant:
- Domain model.
- Validation playbook.
- Existing validation code.
- Related tests.

Probably irrelevant:
- Deployment documentation.
- GitHub release workflow.
- Unrelated infrastructure code.
```

Relevance should guide context selection.

---

# Context Proximity

Prefer context closest to the engineering concern.

For example:

```text
Specific module architecture
    ↓ preferred over
General solution description
```

when implementing inside that module.

Broader context remains useful when local decisions affect system-wide boundaries.

---

# Context Granularity

Context should be provided at the level necessary for the task.

Possible levels include:

```text
Repository

↓

Solution

↓

Module

↓

Project

↓

Component

↓

File

↓

Method
```

Using context that is too broad increases noise.

Using context that is too narrow may hide important dependencies.

---

# Explicit Context References

Prompts should identify important context sources explicitly.

Prefer:

```text
Read:
- docs/architecture/DEPENDENCY_RULES.md
- playbooks/dotnet/04-dependency-management.md
- src/MarketData/
```

over:

```text
Use the relevant architecture and standards.
```

Explicit references improve repeatability.

---

# Context Bundles

Frequently reused context may be grouped conceptually.

Example:

```text
.NET Implementation Context:
- Solution Architecture
- Project Structure
- Coding Standards
- Testing
- Security
```

Context bundles can reduce repeated authoring while preserving consistency.

---

# Context and Playbooks

Playbooks should be treated as reusable engineering context.

Prompts should reference playbooks rather than restating their contents.

Example:

```text
Follow:
playbooks/dotnet/08-testing.md
```

instead of copying the entire testing methodology into every implementation prompt.

---

# Context and Standards

Engineering standards should remain authoritative.

Prompts should avoid duplicating standards unless the task requires a small explicit constraint.

Duplication increases the risk of inconsistency when standards evolve.

---

# Context and Existing Code

Existing implementation is an important source of local conventions.

Agents should inspect:

* Naming.
* Patterns.
* Error handling.
* Dependency usage.
* Test style.
* Logging style.

However, existing code should not override explicit architecture or standards when implementation has drifted.

---

# Context Conflict

Conflicts between context sources should be surfaced rather than silently resolved.

Example:

```text
Architecture:
Application must not reference Infrastructure.

Existing implementation:
Application references Infrastructure directly.
```

The agent should report the conflict rather than assume the implementation is authoritative.

---

# Context Conflict Resolution

A useful resolution hierarchy is:

```text
Higher-Authority Source Wins

↓

If Authority Is Equal
Evaluate Freshness

↓

If Still Ambiguous
Surface Conflict

↓

Do Not Invent Resolution
```

Significant conflicts should become engineering decisions.

---

# Missing Context

Prompts should define behavior when required context is unavailable.

For example:

```text
If the architecture document referenced by this task does not exist,
do not infer a new architecture.

Report the missing dependency and continue only with work that does not
depend on that decision.
```

Missing context should not automatically become agent-generated architecture.

---

# Optional Context

Not all context is mandatory.

Prompts may classify context as:

```text
Required

Recommended

Optional
```

Required context blocks execution when unavailable.

Recommended context improves quality but may not block execution.

Optional context provides supporting information.

---

# Context and Assumptions

Context gaps create assumptions.

A prompt should control which assumptions are acceptable.

Safe assumptions may include:

* Existing naming conventions.
* Existing formatting.
* Established folder patterns.

Unsafe assumptions may include:

* New architecture.
* Business rules.
* Security policy.
* Public contract changes.
* Data migration behavior.

---

# Context and Task Scope

Context should remain aligned with task scope.

If the prompt limits work to:

```text
src/MarketData/
```

then unrelated modules should not be inspected extensively unless necessary to understand dependencies.

Context discovery should not become uncontrolled repository exploration.

---

# Context and Security

Context may contain sensitive information.

AI systems should avoid unnecessary access to:

* Secrets.
* Credentials.
* Production data.
* Private keys.
* Sensitive customer information.
* Restricted infrastructure configuration.

Only necessary context should be exposed to the execution process.

---

# Context Sanitization

When context includes sensitive values, prompts should prefer sanitized forms.

Example:

```text
ConnectionString=<redacted>
```

rather than exposing real credentials.

Engineering context should communicate structure without unnecessarily exposing secrets.

---

# Context and External Sources

External documentation may be required for:

* Framework behavior.
* API contracts.
* Dependency capabilities.
* Standards.

External context should be:

* Relevant.
* Trustworthy.
* Current.
* Clearly distinguished from repository authority.

Repository-specific decisions should not be overridden casually by generic external guidance.

---

# Context and Documentation Drift

Documentation may become inconsistent with implementation.

Agents should look for evidence of drift when:

* Paths no longer exist.
* Commands fail.
* APIs differ.
* Architecture statements conflict with project references.

Potential documentation drift should be reported.

---

# Context and Tests

Tests provide executable context.

Tests may reveal:

* Expected behavior.
* Public contracts.
* Edge cases.
* Regression expectations.

However, tests should not automatically override explicit requirements when they are outdated or incomplete.

---

# Context and Build Configuration

Build assets provide important repository context.

Examples include:

* global.json.
* Directory.Build.props.
* Directory.Packages.props.
* .editorconfig.
* CI configuration.

Agents should inspect applicable configuration rather than inventing build conventions.

---

# Context and GitHub Issues

Issues often provide task-specific engineering context.

Useful information includes:

* Objective.
* Acceptance criteria.
* Priority.
* Dependencies.
* Discussion.
* Milestone.

The prompt should distinguish issue requirements from implementation suggestions when both exist.

---

# Context and Pull Requests

Previous pull requests may provide historical context, but they are usually lower authority than current architecture and standards.

Historical discussions should be used to understand decisions, not blindly reproduce old implementation.

---

# Context Persistence

Important context discovered during execution should become durable repository knowledge when it represents a lasting engineering decision.

A useful progression is:

```text
Repeated Context Need

↓

Engineering Decision Identified

↓

Document Authoritative Source

↓

Future Prompts Reference It
```

This reduces dependence on conversational memory.

---

# Context Compression

Large context sources may need summarization.

Compression should preserve:

* Constraints.
* Decisions.
* Definitions.
* Interfaces.
* Dependencies.

It should avoid reducing important engineering rules into vague summaries.

---

# Context Indexing

Large repositories may benefit from explicit documentation indexes.

Examples include:

```text
docs/README.md
docs/architecture/README.md
playbooks/README.md
```

Indexes improve context discovery for humans and AI systems.

---

# Context Ordering

When providing multiple context sources, order them deliberately.

A useful sequence is:

```text
Task Requirements

↓

Architecture

↓

Standards

↓

Playbooks

↓

Local Implementation

↓

Tests

↓

Supporting References
```

This reinforces authority and execution focus.

---

# Context Duplication

Avoid repeating the same engineering rule across:

* Prompt.
* Standard.
* Playbook.
* Architecture document.

Prefer one authoritative source with references from dependent artifacts.

Duplication increases maintenance cost and inconsistency risk.

---

# Context Isolation

Independent tasks should avoid unnecessary context contamination.

For example, a prompt implementing logging should not include extensive unrelated security or deployment context unless those concerns materially affect the task.

Context isolation improves reasoning focus.

---

# Context for Review Tasks

Review prompts require broader context than focused implementation prompts.

A review may need:

* Standards.
* Architecture.
* Implementation.
* Tests.
* Documentation.
* Operational evidence.

Context depth should reflect review scope.

---

# Context for Validation Tasks

Validation prompts require:

* Acceptance criteria.
* Expected outputs.
* Validation standards.
* Current artifacts.
* Execution evidence.

Validation context should focus on proving compliance rather than redesigning implementation.

---

# Context for Architecture Tasks

Architecture tasks may require broader strategic context.

Examples include:

* Business goals.
* Constraints.
* Existing architecture.
* Operational requirements.
* Domain boundaries.
* Future roadmap.

Architecture prompts should avoid becoming implementation-first.

---

# Context for Coding Agents

Coding agents should normally receive or discover:

```text
Task

+

Architecture

+

Applicable Standards

+

Relevant Playbook

+

Repository State

+

Existing Tests

+

Validation Requirements
```

This forms the minimum engineering context for controlled repository modification.

---

# Context for Conversational AI

Conversational AI may work with broader reasoning context than coding agents.

It may explore:

* Alternatives.
* Trade-offs.
* Architectural options.
* Strategy.
* Future evolution.

Decisions resulting from those conversations should be persisted when they become authoritative.

---

# Context and Multi-Agent Workflows

When multiple agents participate, shared context should remain consistent.

Agents should reference the same:

* Architecture.
* Standards.
* Task requirements.
* Acceptance criteria.

Each agent may receive specialized local context while sharing authoritative global constraints.

---

# Context Handoffs

Agent handoffs should preserve essential information.

A handoff may include:

```text
Objective

Current State

Decisions Made

Files Changed

Validation Status

Known Risks

Next Action
```

Handoffs should not rely on hidden conversational state.

---

# Context and AI Autonomy

Context quality becomes more important as AI autonomy increases.

```text
Higher Autonomy

↓

More Independent Decisions

↓

Greater Dependence on Context

↓

Greater Need for Context Governance
```

Autonomous workflows require stronger context discovery, authority, freshness, and validation controls.

---

# Context Quality Checks

Before execution, prompts or agents should consider:

* Is required context available?
* Is it authoritative?
* Is it current?
* Is it relevant?
* Are sources contradictory?
* Is sensitive context unnecessarily exposed?
* Is important task context missing?

These checks reduce avoidable execution errors.

---

# Common Context Anti-Patterns

Avoid:

## Context Dumping

Providing large amounts of unrelated repository content.

## Hidden Context

Relying on information not available to the executing agent.

## Stale Context

Using outdated architecture or implementation assumptions.

## Conflicting Context

Providing contradictory instructions without priority.

## Context Duplication

Copying the same standards into multiple prompts.

## Context Overreach

Inspecting unrelated repository areas without need.

## Conversation Dependency

Relying on previous chat history for durable engineering decisions.

## Implementation Authority Drift

Treating existing code as more authoritative than approved architecture when they conflict.

---

# Engineering Recommendations

Prompt authors should:

* Identify required context explicitly.
* Prefer authoritative repository sources.
* Keep context current.
* Minimize irrelevant information.
* Reference playbooks and standards rather than duplicating them.
* Inspect current repository state.
* Establish context authority.
* Detect conflicts.
* Surface missing critical context.
* Protect sensitive information.
* Keep context proportional to task scope.
* Persist important decisions as repository artifacts.
* Use operational evidence when runtime behavior matters.

---

# Success Criteria

A prompt satisfies this guideline when:

* Required context is identifiable.
* Context sources are authoritative.
* Context is current.
* Irrelevant information is minimized.
* Repository state is inspected where necessary.
* Conflicts are surfaced.
* Missing critical context does not result in unsupported assumptions.
* Sensitive context is protected.
* Playbooks and standards are referenced consistently.
* Another engineer or agent can reproduce the task understanding from available artifacts.

---

# Related Guidelines

This guideline should be used together with:

* 01-prompt-quality-principles.md
* 02-prompt-clarity.md
* 04-scope-and-boundaries.md
* 05-instruction-design.md
* 06-output-contracts.md
* 07-validation-and-acceptance.md
* 08-error-and-ambiguity-handling.md
* 09-security-and-safety.md
* 10-prompt-review.md

---

# Conclusion

Context determines the engineering environment in which an AI system interprets a task.

A reliable context model follows:

```text
Authoritative Sources

↓

Relevant Selection

↓

Freshness Verification

↓

Conflict Detection

↓

Task-Focused Context

↓

Controlled Execution
```

Effective context management does not attempt to provide every available piece of information.

It provides the **right information, from the right source, at the right level of detail, at the right time**.

The central principle is:

> **AI systems should not be expected to infer engineering truth when authoritative context can be discovered, referenced, or preserved explicitly.**
