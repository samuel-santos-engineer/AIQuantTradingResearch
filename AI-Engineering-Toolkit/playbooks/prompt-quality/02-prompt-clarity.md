
# Prompt Clarity

## Purpose

The Prompt Clarity guideline defines the engineering principles and practices for writing clear, precise, understandable, and unambiguous prompts within the AI Engineering Toolkit.

Its purpose is to reduce interpretation variance, unsupported assumptions, execution mistakes, and unnecessary clarification by ensuring that engineering intent is communicated explicitly.

Clarity should make the desired outcome understandable before execution begins.

---

# Objectives

The Prompt Clarity guideline aims to:

* Improve prompt readability.
* Reduce ambiguity.
* Minimize interpretation variance.
* Improve execution reliability.
* Make objectives explicit.
* Standardize terminology.
* Reduce hidden assumptions.
* Improve reviewability.
* Support maintainability.
* Enable reliable AI-assisted engineering.

---

# Scope

This guideline applies to prompts used for:

* Architecture analysis.
* Repository operations.
* Software implementation.
* Refactoring.
* Testing.
* Documentation.
* Security analysis.
* Performance engineering.
* DevOps automation.
* Code review.
* Validation.
* AI-assisted engineering workflows.

Clarity expectations should remain proportional to task complexity and risk.

---

# Engineering Philosophy

A prompt should communicate engineering intent with the least ambiguity necessary for reliable execution.

The objective is not maximum verbosity.

The objective is sufficient precision.

A useful model is:

```text
Clear Intent

↓

Clear Context

↓

Clear Instructions

↓

Clear Expectations

↓

Reduced Interpretation

↓

More Reliable Execution
```

Poor clarity forces the AI system to infer missing engineering decisions.

---

# Explicit Objective

Every significant prompt should define a clear objective.

The objective should communicate:

* What must be accomplished.
* What engineering outcome is expected.
* Why the task exists when that context influences execution.

Prefer:

```text
Objective:
Add repository validation that verifies all required build assets
exist before CI execution begins.
```

Avoid:

```text
Improve repository validation.
```

The first statement provides a measurable engineering direction.

---

# One Primary Objective

A prompt should normally have one primary objective.

Multiple supporting activities may exist, but they should contribute to the same outcome.

Avoid combining unrelated goals such as:

* Refactoring.
* Adding features.
* Updating dependencies.
* Reorganizing documentation.

within one prompt unless they are part of a deliberately coordinated change.

Focused objectives improve execution and reviewability.

---

# Intent Before Implementation

Prompts should explain intent before prescribing implementation when the distinction matters.

Prefer:

```text
Objective:
Prevent invalid market-data records from entering the processing pipeline.

Implementation constraints:
Use the existing validation abstraction.
Do not introduce a new validation package.
```

This allows implementation decisions to remain aligned with the engineering goal.

---

# Explicit Terminology

Prompt terminology should be consistent with the repository and domain.

Use established names for:

* Modules.
* Projects.
* Services.
* Domain concepts.
* Files.
* Architecture layers.
* Engineering processes.

Do not introduce alternative terminology for concepts that already have authoritative names.

Terminology inconsistency increases interpretation risk.

---

# Ubiquitous Language

Where domain terminology exists, prompts should use the same ubiquitous language as the software.

For example, if the system uses:

```text
MarketDataSnapshot
```

do not casually refer to the same concept as:

```text
QuoteRecord
PriceObject
MarketEntry
```

unless those represent different concepts.

Prompt language should reinforce domain consistency.

---

# Avoid Ambiguous Verbs

Some verbs communicate insufficient engineering intent.

Examples include:

* Improve.
* Optimize.
* Fix.
* Clean.
* Enhance.
* Modernize.
* Refactor.

These verbs should be accompanied by measurable intent.

Instead of:

```text
Optimize this service.
```

prefer:

```text
Reduce unnecessary allocations in the parsing path while preserving
current public behavior. Use profiling or benchmark evidence to justify changes.
```

---

# Avoid Ambiguous References

Prompts should minimize references such as:

* This.
* That.
* It.
* The thing.
* The current implementation.

when multiple possible referents exist.

Prefer explicit references:

```text
Update MarketDataParser.ParseAsync(...)
```

rather than:

```text
Update this method.
```

unless the execution environment makes the reference unambiguous.

---

# Explicit Subjects

Instructions should clearly identify what performs or receives the action.

For example:

```text
Update MarketDataValidator to reject records with missing timestamps.
```

is clearer than:

```text
Reject missing timestamps.
```

The subject reduces uncertainty about implementation location.

---

# Concrete Constraints

Constraints should be expressed directly.

Prefer:

```text
Do not modify public API signatures.
```

rather than:

```text
Try to preserve compatibility.
```

Prefer:

```text
Do not introduce new NuGet packages.
```

rather than:

```text
Avoid adding dependencies if possible.
```

Use flexible language only when flexibility is intentional.

---

# Positive and Negative Instructions

Prompts may need both positive and negative instructions.

Positive instruction:

```text
Use the existing IMarketDataRepository abstraction.
```

Negative instruction:

```text
Do not access the database directly from the application layer.
```

Together, these establish clearer execution boundaries.

---

# Avoid Contradictory Instructions

Prompts should be reviewed for conflicts.

Examples include:

```text
Make the smallest possible change.

Refactor the entire subsystem to improve maintainability.
```

or:

```text
Do not introduce dependencies.

Use library X.
```

Conflicting instructions force the AI system to determine priority implicitly.

Priority should be explicit.

---

# Instruction Priority

When multiple instructions exist, establish priority where conflicts are possible.

For example:

```text
Priority:
1. Preserve security requirements.
2. Preserve public contracts.
3. Follow architecture standards.
4. Minimize implementation changes.
```

Priority helps resolve legitimate engineering trade-offs.

---

# Specificity

Prompt specificity should match the task.

Too little specificity produces ambiguity.

Too much specificity may unnecessarily constrain implementation.

A prompt should specify:

* What matters.
* What must remain stable.
* What must be produced.

and leave low-risk implementation details flexible when repository conventions already provide guidance.

---

# Avoid Over-Specification

Do not prescribe implementation details that do not affect the engineering contract.

For example, avoid specifying:

* Local variable names.
* Exact private method decomposition.
* Internal ordering with no behavioral significance.

unless such details are part of an established standard.

Over-specification reduces flexibility and increases prompt maintenance.

---

# Structured Language

Complex prompts should use clear structural sections.

Example:

```text
Objective

Context

Scope

Constraints

Instructions

Expected Output

Validation
```

Structure reduces cognitive load for both humans and AI systems.

---

# Sequential Instructions

When execution order matters, instructions should be numbered or explicitly sequenced.

Example:

```text
1. Inspect the existing project structure.
2. Read applicable architecture documentation.
3. Identify affected components.
4. Implement the requested behavior.
5. Add or update tests.
6. Run validation.
7. Report the outcome.
```

Do not rely on paragraph order alone when sequence is significant.

---

# Atomic Instructions

Individual instructions should express one primary action.

Prefer:

```text
1. Add validation for missing timestamps.
2. Add tests for invalid timestamp scenarios.
3. Run the affected test project.
```

rather than:

```text
Add validation, update everything related to it, test it,
and clean up anything else that seems necessary.
```

Atomic instructions improve execution traceability.

---

# Conditional Instructions

Conditional behavior should be explicit.

Example:

```text
If an existing validation abstraction supports this requirement,
extend it.

If not, report the architectural limitation before introducing
a new abstraction.
```

This is clearer than leaving the agent to decide silently.

---

# Clear Preconditions

Execution assumptions should be stated where relevant.

Examples include:

* Required project exists.
* Architecture document has been approved.
* Current branch is correct.
* Required SDK is available.
* Previous migration has completed.

Preconditions prevent execution against an invalid starting state.

---

# Clear Scope

Prompt clarity depends on knowing where the task applies.

Example:

```text
In scope:
- src/MarketData/
- tests/MarketData.Tests/

Out of scope:
- API contracts
- Storage schema
- Deployment configuration
```

Scope clarity reduces accidental expansion.

---

# Clear File References

When file locations are important, use explicit repository paths.

Prefer:

```text
docs/architecture/DEPENDENCY_RULES.md
```

over:

```text
the dependency document
```

Explicit paths improve repository-aware execution.

---

# Clear Authority References

If a prompt depends on engineering standards, identify authoritative sources.

Example:

```text
Follow:
- docs/architecture/SOLUTION_ARCHITECTURE.md
- docs/architecture/DEPENDENCY_RULES.md
- playbooks/dotnet/05-coding-standards.md
```

This is clearer than:

```text
Follow our standards.
```

---

# Expected Output Clarity

Prompts should state what the AI system should produce.

Example:

```text
Expected outputs:
- Updated MarketDataValidator implementation.
- Unit tests covering valid and invalid timestamps.
- No changes to public contracts.
- Validation summary.
```

Undefined outputs lead to inconsistent completion behavior.

---

# Completion Clarity

Prompts should define when execution is complete.

Example:

```text
Complete when:
- The solution builds.
- All affected tests pass.
- No new analyzer warnings are introduced.
- Required files are updated.
```

Completion criteria reduce false success reporting.

---

# Validation Clarity

Validation commands or expectations should be explicit where practical.

Example:

```text
Validate using:
dotnet build
dotnet test tests/MarketData.Tests
```

When exact commands are not appropriate, define the validation category instead.

---

# Failure Clarity

Prompts should specify how failures should be reported.

Example:

```text
If validation fails:
- Do not report completion.
- Identify the failing command.
- Summarize the failure.
- Report whether the failure was introduced by this change.
```

Failure clarity prevents misleading outcomes.

---

# Assumption Clarity

If assumptions are permitted, define their boundaries.

Example:

```text
You may follow existing naming and folder conventions.

Do not assume:
- New architectural abstractions.
- New dependencies.
- Changes to domain behavior.
```

This distinguishes safe implementation inference from significant engineering decisions.

---

# Clarification Threshold

Prompts should distinguish between ambiguity that can be resolved locally and ambiguity requiring escalation.

```text
Minor ambiguity
    ↓
Follow existing repository convention

Significant ambiguity
    ↓
Surface the ambiguity before implementation
```

Significant ambiguity includes decisions affecting:

* Architecture.
* Business rules.
* Security.
* Public contracts.
* Persistent data.
* Production infrastructure.

---

# Conciseness

Clarity benefits from concise language.

Remove content that does not affect:

* Intent.
* Context.
* Scope.
* Constraints.
* Execution.
* Validation.

Repeated instructions can reduce clarity by making priority harder to infer.

---

# Avoid Decorative Language

Engineering prompts should minimize language that adds emphasis without changing execution.

Avoid excessive phrases such as:

* Be extremely careful.
* Make this perfect.
* Use world-class engineering.
* Do your absolute best.

Replace them with observable requirements.

For example:

```text
Preserve existing public behavior and pass all architecture tests.
```

is more useful than:

```text
Be extremely careful not to break anything.
```

---

# Avoid Subjective Quality Terms

Terms such as:

* Clean.
* Elegant.
* Professional.
* Enterprise-grade.
* Best practice.

should be supported by explicit criteria.

Instead of:

```text
Create an enterprise-grade implementation.
```

prefer:

```text
Follow repository architecture, add automated tests,
preserve public contracts, use structured logging,
and pass all required validation.
```

---

# Example Clarity Pattern

A clear engineering prompt may use:

```text
Objective:
Add timestamp validation to market-data ingestion.

Context:
Follow the existing validation pattern in src/MarketData/.

Scope:
Modify only the MarketData ingestion and its tests.

Constraints:
- Do not change public API contracts.
- Do not introduce new packages.
- Preserve current logging behavior.

Instructions:
1. Inspect the existing validation implementation.
2. Add rejection for missing or invalid timestamps.
3. Add tests for valid and invalid scenarios.
4. Run affected tests.

Expected output:
- Updated implementation.
- Updated tests.
- Validation summary.

Complete when:
- Build succeeds.
- Tests pass.
- No unrelated files are modified.
```

The value comes from explicit engineering intent rather than prompt length.

---

# Clarity and Maintainability

Reusable prompts should remain clear when read months after creation.

Avoid relying on temporary context such as:

* As discussed earlier.
* Use what we decided yesterday.
* Follow the approach from the previous chat.

Durable prompts should reference durable engineering artifacts.

---

# Clarity and Tool Independence

Prompt meaning should remain understandable even when tools change.

Avoid relying on unexplained tool-specific behavior unless the prompt intentionally targets that tool.

Core engineering requirements should remain explicit.

---

# Clarity and AI Autonomy

Clarity becomes more important as AI execution authority increases.

```text
Advisory AI
    ↓
Misinterpretation affects advice

Coding Agent
    ↓
Misinterpretation affects repository changes

Autonomous Workflow
    ↓
Misinterpretation may affect multiple systems
```

Higher autonomy requires stronger clarity.

---

# Common Clarity Anti-Patterns

Avoid:

## Vague Objective

```text
Improve this.
```

## Undefined Scope

```text
Fix all related problems.
```

## Hidden Context

```text
Use the architecture we agreed on.
```

## Ambiguous Completion

```text
Make sure it works.
```

## Subjective Requirement

```text
Make the code cleaner.
```

## Contradictory Direction

```text
Make no structural changes but redesign the module.
```

## Unlimited Authority

```text
Change whatever you think is necessary.
```

These patterns force unnecessary AI interpretation.

---

# Engineering Recommendations

Prompt authors should:

* State the objective explicitly.
* Use repository terminology consistently.
* Avoid ambiguous verbs.
* Identify subjects and targets.
* Make important constraints explicit.
* Structure complex prompts.
* Use sequential instructions where order matters.
* Define scope.
* Identify authoritative references.
* Define expected outputs.
* Define completion criteria.
* Define validation.
* Define failure reporting.
* Minimize unnecessary wording.
* Replace subjective terms with observable requirements.
* Review reusable prompts for contradictions.

---

# Success Criteria

A prompt satisfies this guideline when:

* Its objective is immediately understandable.
* Important terminology is unambiguous.
* Instructions can be interpreted consistently.
* Scope is clear.
* Constraints are explicit.
* Expected outputs are defined.
* Completion criteria are understandable.
* Significant assumptions are minimized.
* Failure behavior is clear.
* Another engineer can understand the prompt without reconstructing hidden context.

---

# Related Guidelines

This guideline should be used together with:

* 01-prompt-quality-principles.md
* 03-context-management.md
* 04-scope-and-boundaries.md
* 05-instruction-design.md
* 06-output-contracts.md
* 07-validation-and-acceptance.md
* 08-error-and-ambiguity-handling.md
* 09-security-and-safety.md
* 10-prompt-review.md

---

# Conclusion

Prompt clarity is the foundation of reliable AI interpretation.

A clear prompt communicates:

```text
Intent

↓

Meaning

↓

Boundaries

↓

Expected Action

↓

Expected Outcome
```

Clarity does not require excessive detail.

It requires deliberate language.

By using explicit objectives, consistent terminology, visible constraints, structured instructions, clear outputs, and measurable completion criteria, engineering prompts reduce uncertainty and enable AI systems to operate more reliably within established engineering intent.

The central principle is:

> **If an important engineering decision can be interpreted in multiple materially different ways, the prompt is not yet clear enough.**
