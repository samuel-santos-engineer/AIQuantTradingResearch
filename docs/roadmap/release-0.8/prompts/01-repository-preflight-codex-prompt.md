# Codex Execution Prompt — Release 0.8 / 01 Repository Preflight

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.8 — Solution Skeleton |
| Work Package | 01 — Repository Preflight |
| Issue Type | Research |
| Execution Mode | Read-only repository inspection |
| Primary Agent | Codex |
| Change Authorization | No repository modifications |
| Expected Outcome | Evidence-based preflight report and readiness decision |

---

## Purpose

Perform the Repository Preflight for **Phase 2 — Release 0.8: Solution Skeleton**.

This task establishes the verified repository state before any Release 0.8 implementation begins.

The preflight must inspect the actual repository, resolve the authoritative engineering guidance relevant to the Solution Skeleton, identify conflicts or gaps, execute safe non-destructive validation where appropriate, and produce an evidence-based readiness decision for the next work package.

This is a research and validation task.

**Do not bootstrap, redesign, refactor, repair, or otherwise modify the repository during this work package.**

---

## Objective

Determine whether the repository is ready to begin:

**02 — Root Solution**

The final decision must be exactly one of:

- `READY`
- `READY WITH ACTIONS`
- `BLOCKED`

The decision must be supported by repository evidence.

---

## Operating Principles

Apply the repository's Prompt Quality Guidelines, .NET Engineering Playbooks, architecture documentation, Release 0.8 planning documents, and AI-assisted engineering workflow.

During execution:

1. Inspect before reasoning.
2. Prefer repository evidence over assumptions.
3. Prefer current authoritative files over remembered or conversational context.
4. Preserve architecture and repository boundaries.
5. Do not silently resolve significant ambiguity.
6. Do not expand scope.
7. Do not make repository changes.
8. Distinguish observed facts from interpretations and recommendations.
9. Distinguish successful command execution from inferred correctness.
10. Report failures and incomplete validation explicitly.
11. Do not claim readiness without evidence.
12. Do not introduce implementation decisions belonging to later work packages.

---

# 1. Authority and Context Resolution

Before performing detailed analysis, locate and inspect the repository guidance relevant to this task.

## 1.1 Release 0.8 Sources

At minimum, locate and read:

```text
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Treat these documents as the primary Release 0.8 execution contract unless a higher-authority repository policy explicitly overrides them.

Confirm that both files exist.

Record any inconsistency between them.

---

## 1.2 Architecture Sources

Inspect the architecture documentation relevant to:

- Solution structure.
- Project structure.
- Dependency direction.
- Layer/module boundaries.
- Application host/composition root.
- Dependency injection.
- Testing.
- Build configuration.
- Naming.
- Implementation constraints.

Locate the applicable documents rather than assuming exact filenames beyond known repository structure.

Relevant areas may include:

```text
docs/architecture/
docs/architecture/solution/
docs/architecture/design/
docs/architecture/implementation/
docs/architecture/resilience/
```

Do not read unrelated documentation merely to maximize context.

Identify the specific architecture documents actually used in the preflight report.

---

## 1.3 Repository Engineering Sources

Inspect applicable repository-level engineering guidance, including relevant files such as:

```text
README.md
ENGINEERING.md
ARCHITECTURE.md
PROJECT_CONSTITUTION.md
CODE_STANDARDS.md
CONTRIBUTING.md
DEPENDENCY_GUIDELINES.md
```

Only report files that actually exist.

Do not treat a filename listed in this prompt as proof that the file exists.

---

## 1.4 Root Configuration Sources

Inspect, when present:

```text
global.json
Directory.Build.props
Directory.Packages.props
.editorconfig
.gitattributes
```

Also identify any other root-level .NET/build configuration that materially affects Release 0.8.

---

## 1.5 Engineering Automation Sources

Inspect the current `eng/` directory and identify existing engineering scripts.

Pay particular attention to capabilities for:

- Restore.
- Build.
- Test.
- Format.
- Verify.
- Clean.

Determine what each relevant script currently expects from the repository.

Do not modify or normalize the scripts.

---

## 1.6 Applicable AI Engineering Guidance

If the AI Engineering Toolkit or its guidance is available in the current workspace/repository context, apply the relevant principles from:

```text
Prompt Quality Guidelines
.NET Engineering Playbooks
AI-Assisted Engineering Workflow
```

Relevant Prompt Quality areas include:

```text
01 — Prompt Quality Principles
02 — Prompt Clarity
03 — Context Management
04 — Scope and Boundaries
05 — Instruction Design
06 — Output Contracts
07 — Validation and Acceptance
08 — Error and Ambiguity Handling
09 — Security and Safety
10 — Prompt Review
```

Relevant .NET Engineering Playbooks include:

```text
01 — Solution Architecture
02 — Project Structure
03 — Domain-Driven Design
04 — Dependency Management
05 — Coding Standards
06 — Error Handling
07 — Logging
08 — Testing
09 — Security
10 — Performance
11 — Documentation
12 — Project Review
```

Do not invent Toolkit content if it is not accessible.

If Toolkit guidance is unavailable to Codex, state that explicitly and continue using authoritative guidance present in the AIQuantTradingResearch repository.

---

# 2. Scope

## In Scope

This work package may:

- Read repository files.
- Enumerate repository structure.
- Inspect Git status.
- Inspect .NET SDK/toolchain information.
- Inspect solution/project files.
- Inspect test assets.
- Inspect engineering scripts.
- Inspect build configuration.
- Compare documentation with physical repository state.
- Execute safe, non-destructive diagnostic commands.
- Execute existing validation commands when they are safe and meaningful in the current repository state.
- Identify gaps, conflicts, risks, and blockers.
- Produce a structured preflight report in the Codex response.

---

## Out of Scope

Do **not**:

- Create `.sln` or `.slnx` files.
- Create projects.
- Create directories for the future skeleton.
- Add project references.
- Modify `global.json`.
- Modify `Directory.Build.props`.
- Modify `Directory.Packages.props`.
- Modify `.editorconfig`.
- Modify `.gitattributes`.
- Modify `eng/` scripts.
- Add packages.
- Restore packages solely to mutate repository state unless an existing validation workflow requires it and the effect is understood.
- Implement the Worker host.
- Implement dependency registration.
- Create tests.
- Implement architecture tests.
- Update documentation.
- Create commits.
- Create branches.
- Push changes.
- Open pull requests.
- Delete files.
- Reformat files.
- Fix discovered problems.
- Implement work belonging to work packages 02–15.

If a problem is found, **report it; do not repair it**.

---

# 3. Safety and Repository Preservation

This is a read-only work package.

Before running commands, consider whether they can alter:

- Tracked files.
- Generated files.
- Lock files.
- Package state.
- Repository configuration.
- Git state.
- External resources.

Prefer inspection commands over mutation commands.

Do not use destructive Git commands.

Do not use commands equivalent to:

```text
git reset --hard
git clean -fd
git checkout -- .
git restore .
```

Do not install global tools.

Do not change machine configuration.

Do not authenticate to external services.

Do not expose credentials, tokens, connection strings, secrets, or private environment values in the report.

If a diagnostic command could expose sensitive values, do not execute it in that form.

---

# 4. Preflight Execution Procedure

Execute the following sequence.

---

## Step 1 — Establish Repository Identity and Git State

Determine:

- Repository root.
- Current branch.
- Current commit SHA.
- Whether tracked/untracked working-tree changes exist.
- Whether the repository state is clean enough to perform reliable inspection.

Use safe commands such as:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Do not modify Git state.

### Evidence Required

Record:

- Repository root.
- Branch.
- Commit.
- Working-tree state.

If unrelated local changes exist, do not alter them.

Explain whether they affect preflight confidence.

---

## Step 2 — Inspect Top-Level Repository Structure

Enumerate the repository root and important first-level directories.

Determine whether the expected engineering areas exist, such as:

```text
docs/
eng/
src/
tests/
.github/
```

Do not assume they must all exist before Release 0.8.

Their absence may be intentional.

Identify:

- Existing solution files.
- Existing project files.
- Existing source directories.
- Existing test directories.
- Existing bootstrap/build assets.

### Evidence Required

Provide a concise repository structure summary.

Avoid dumping the entire repository tree if it is large.

---

## Step 3 — Resolve Release 0.8 Contract

Read:

```text
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Extract the constraints relevant to Repository Preflight and the upcoming Solution Skeleton.

Determine:

- Expected solution format.
- Expected production projects.
- Expected test projects.
- Expected host.
- Expected dependency direction.
- Expected engineering script integration.
- Explicit exclusions.
- Release acceptance expectations.

### Evidence Required

Summarize the Release 0.8 contract without rewriting the full documents.

Report conflicts between plan and manifest, if any.

---

## Step 4 — Inspect Architecture Guidance

Identify and read only the architecture documents necessary to validate the intended skeleton.

At minimum, establish:

- Intended project/layer responsibilities.
- Allowed dependency direction.
- Forbidden dependencies.
- Host/composition-root responsibility.
- Test architecture expectations.
- Dependency injection expectations.
- Naming and physical structure constraints.

### Evidence Required

Create an **Authoritative Sources Reviewed** section containing the exact repository paths used.

For every material architecture conclusion, identify its source.

If architecture documents contradict one another, report the contradiction.

Do not decide a new architecture.

---

## Step 5 — Inspect .NET Toolchain State

Inspect the repository's SDK requirements and the available environment.

When safe, use commands such as:

```text
dotnet --version
dotnet --info
dotnet --list-sdks
```

Inspect `global.json` if present.

Determine:

- Requested SDK version.
- Roll-forward behavior if configured.
- Installed SDK compatibility.
- Target framework expectations found in existing configuration.
- Whether the environment appears capable of executing upcoming Release 0.8 work.

Do not modify or install SDKs.

### Evidence Required

Record:

- Repository SDK requirement.
- Effective/available SDK information.
- Compatibility assessment.
- Any blocker or uncertainty.

---

## Step 6 — Inspect Root Build Configuration

Inspect:

```text
Directory.Build.props
Directory.Packages.props
.editorconfig
.gitattributes
```

when present.

Determine relevant policies for:

- Target framework, if centralized.
- Nullable reference types.
- Implicit usings.
- Warnings.
- Warnings-as-errors.
- Analyzers.
- Language version.
- Package version management.
- Formatting.
- Source control normalization.

Do not propose replacement configuration merely because another convention is possible.

### Evidence Required

Summarize the existing policies that future Release 0.8 projects must inherit or respect.

Identify contradictions with the Release 0.8 plan.

---

## Step 7 — Discover Existing Solution and Project Assets

Search for:

```text
*.sln
*.slnx
*.csproj
*.fsproj
*.vbproj
```

Determine:

- Whether a solution already exists.
- Whether production projects already exist.
- Whether test projects already exist.
- Whether any existing projects overlap with the Release 0.8 manifest.
- Whether stale/template/sample project artifacts could create ambiguity.

Do not delete or modify anything.

### Evidence Required

Provide a concise inventory with paths and observed purpose where determinable.

---

## Step 8 — Inspect Existing Dependency Graph

If project files exist, inspect their project references.

Determine whether the current graph already contains relationships relevant to Release 0.8.

Compare observed relationships with approved architecture.

Do not add or remove references.

### Evidence Required

Report:

```text
Observed dependency
Expected status
Source of architectural rule
Assessment
```

If no projects exist, state that dependency validation is not yet applicable.

---

## Step 9 — Inspect Test Assets

Inspect existing test-related files and directories.

Determine:

- Existing test projects.
- Test frameworks.
- Shared test configuration.
- Architecture-test infrastructure.
- Naming conventions.
- Package dependencies relevant to tests.

Do not create tests.

### Evidence Required

State what can be reused, what does not yet exist, and what Release 0.8 expects later.

Do not treat expected future assets as preflight defects unless their absence blocks the next work package.

---

## Step 10 — Inspect Engineering Scripts

Inspect `eng/`.

For each relevant script, determine:

- Responsibility.
- Inputs.
- Expected solution/project paths.
- Whether it currently works without a solution.
- Whether it assumes a particular solution filename or format.
- Whether it delegates to other scripts.
- Exit/failure behavior where reasonably discoverable.

Relevant scripts may include:

```text
restore
build
test
format
verify
clean
```

### Evidence Required

Produce a compact capability matrix:

| Capability | Script/Path | Current Expectation | Release 0.8 Impact |
| --- | --- | --- | --- |
| Restore | ... | ... | ... |
| Build | ... | ... | ... |
| Test | ... | ... | ... |
| Format | ... | ... | ... |
| Verify | ... | ... | ... |
| Clean | ... | ... | ... |

Use only actual repository evidence.

---

## Step 11 — Execute Safe Existing Validation

Determine which existing validation commands are meaningful before the solution skeleton exists.

Do not force commands that are guaranteed to fail solely because future Release 0.8 assets have not yet been created.

Where safe and useful, execute existing diagnostic/validation commands.

For each command, record:

```text
Command
Exit status
Result
Interpretation
```

A failing command is evidence.

Do not hide or repair it.

Do not interpret an expected pre-skeleton failure as a blocker unless it prevents the next work package or contradicts the documented baseline.

---

## Step 12 — Compare Documentation with Physical Repository State

Identify material discrepancies such as:

- Documented path does not exist.
- Existing path differs from documented structure.
- Script references obsolete solution name.
- SDK documentation conflicts with `global.json`.
- Package guidance conflicts with central package configuration.
- Architecture describes a different project boundary than Release 0.8.
- Documentation assumes assets that have not yet been implemented.

Classify each discrepancy as:

- Expected future-state difference.
- Documentation drift.
- Implementation drift.
- Ambiguous.
- Potential blocker.

Do not update documentation.

---

## Step 13 — Identify Risks, Gaps, and Blockers

Classify findings using:

### Blocker

Prevents safe execution of Work Package 02.

### Required Action

Should be addressed during a defined Release 0.8 work package but does not prevent Work Package 02.

### Risk

May affect later work and should be tracked.

### Observation

Useful information with no immediate action required.

Do not classify normal absence of future Release 0.8 artifacts as blockers.

For example, absence of the root solution before Work Package 02 is expected.

---

## Step 14 — Map Findings to Release 0.8 Work Packages

When possible, assign non-blocking actions to the work package that should own them.

Use the Release 0.8 Execution Plan as the authority for work-package ownership.

Examples:

```text
Root solution creation
→ 02 — Root Solution

Production projects
→ 03 — Production Projects

Project references
→ 04 — Project References

Root configuration alignment
→ 05 — Root Build Configuration

Worker
→ 06 — Minimal Worker Host

Dependency registration
→ 07 — Dependency Registration

Test projects
→ 08 — Test Projects

Architecture tests
→ 09 — Architecture Tests

Solution organization
→ 10 — Solution Organization

Engineering scripts
→ 11 — Engineering Scripts Integration

Documentation drift
→ 12 — Documentation Alignment

End-to-end validation
→ 13 — Full Skeleton Validation
```

Do not perform those actions during Preflight.

---

# 5. Readiness Decision Rules

Use the following decision model.

## READY

Use `READY` when:

- No blocker prevents Work Package 02.
- Authoritative Release 0.8 guidance is sufficiently consistent.
- Required toolchain is available or clearly compatible.
- Repository state is sufficiently understood.
- Existing assets do not create unresolved conflicts requiring prior intervention.

Non-blocking observations may still exist.

---

## READY WITH ACTIONS

Use `READY WITH ACTIONS` when:

- Work Package 02 can begin safely.
- One or more non-blocking issues must be addressed by later Release 0.8 work packages.
- The actions have clear ownership.
- No unresolved architectural decision is required before Work Package 02.

This is a valid successful preflight outcome.

---

## BLOCKED

Use `BLOCKED` when a material unresolved condition prevents safe execution of Work Package 02.

Examples may include:

- Release plan and authoritative architecture fundamentally conflict.
- Required SDK/toolchain cannot support the planned solution.
- Existing solution/project state creates an unresolved collision.
- Critical authoritative documents are missing or contradictory enough that the intended skeleton cannot be determined.
- Repository state cannot be inspected reliably.

A blocker must include evidence and the decision required to unblock it.

---

# 6. Significant Ambiguity Handling

If significant ambiguity is discovered:

1. Do not guess.
2. Identify the conflicting or missing sources.
3. Explain why the ambiguity matters.
4. Determine whether Work Package 02 can proceed without resolving it.
5. If not, classify it as a blocker.
6. Ask for a human decision only when repository evidence cannot resolve the issue.

Do not stop for minor implementation details that belong to later work packages.

---

# 7. Expected Output Contract

Return one complete **Repository Preflight Report** in the Codex response.

Do not create a repository file unless the issue or a human explicitly authorizes that separately.

Use the following structure.

---

# Repository Preflight Report

## 1. Executive Summary

Briefly state:

- What was inspected.
- Overall repository condition.
- Final readiness decision.

---

## 2. Execution Context

Include:

```text
Repository:
Branch:
Commit:
Working Tree:
Execution Environment:
```

Do not expose secrets or sensitive environment data.

---

## 3. Authoritative Sources Reviewed

List the exact paths of the documents/configuration used to reach material conclusions.

Group by:

- Release 0.8.
- Architecture.
- Engineering.
- Build/configuration.
- AI engineering guidance, if accessible.

---

## 4. Repository Structure

Summarize relevant current structure.

Identify existing:

- Solutions.
- Projects.
- Source directories.
- Tests.
- Engineering automation.

---

## 5. Release 0.8 Contract Assessment

Summarize:

- Expected skeleton.
- Expected project boundaries.
- Expected test structure.
- Expected host.
- Expected dependency rules.
- Explicit exclusions.

Report inconsistencies if present.

---

## 6. .NET SDK and Toolchain

Report:

- `global.json` state.
- Available/effective SDK.
- Compatibility.
- Relevant target-framework/build expectations.
- Assessment.

---

## 7. Root Build Configuration

Report relevant settings from:

- `Directory.Build.props`.
- `Directory.Packages.props`.
- `.editorconfig`.
- `.gitattributes`.
- Other material root configuration.

---

## 8. Existing Solution and Project Assets

Provide a concise inventory.

If none exist, say so explicitly.

---

## 9. Dependency State

Report current project-reference state and architecture alignment.

If not applicable yet, say so.

---

## 10. Test State

Report existing test infrastructure and its relevance to Release 0.8.

---

## 11. Engineering Scripts

Include the capability matrix:

| Capability | Script/Path | Current Expectation | Release 0.8 Impact |
| --- | --- | --- | --- |

---

## 12. Validation Evidence

For every command actually executed:

| Command | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

Do not claim commands were executed if they were not.

---

## 13. Documentation vs Repository Differences

For each material difference:

| Evidence | Classification | Impact | Owner / Work Package |
| --- | --- | --- | --- |

---

## 14. Findings

Use:

| ID | Classification | Area | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- | --- |

Classification must be one of:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

---

## 15. Release 0.8 Work-Package Readiness

Provide a concise mapping of relevant findings to future work packages.

Do not expand or redesign the approved execution plan.

---

## 16. Preflight Decision

State exactly one:

```text
READY
```

or:

```text
READY WITH ACTIONS
```

or:

```text
BLOCKED
```

Then provide the evidence-based rationale.

---

## 17. Next Action

If `READY` or `READY WITH ACTIONS`, state:

```text
Proceed to:
02 — Root Solution
```

If `BLOCKED`, state the minimum human/engineering action required before Work Package 02 can begin.

---

# 8. Acceptance Criteria

Repository Preflight is complete only when all applicable criteria below are satisfied.

- [ ] Repository root, branch, commit, and working-tree state were inspected.
- [ ] Release 0.8 Execution Plan was reviewed.
- [ ] Release 0.8 File Manifest was reviewed.
- [ ] Applicable architecture guidance was identified and reviewed.
- [ ] Applicable repository engineering guidance was identified.
- [ ] `global.json` was inspected when present.
- [ ] Available .NET SDK/toolchain was inspected.
- [ ] Root build configuration was inspected when present.
- [ ] Existing solution/project files were inventoried.
- [ ] Existing dependency state was inspected when applicable.
- [ ] Existing test assets were inspected.
- [ ] Existing `eng/` automation was inspected.
- [ ] Safe meaningful validation was executed or explicitly identified as not applicable.
- [ ] Documentation/repository differences were assessed.
- [ ] Risks, required actions, observations, and blockers were distinguished.
- [ ] Findings were mapped to future Release 0.8 work packages where applicable.
- [ ] No repository files were intentionally modified.
- [ ] No implementation work from Work Packages 02–15 was performed.
- [ ] A final readiness decision was recorded.
- [ ] The decision is supported by evidence.
- [ ] The next action is explicit.

---

# 9. Final Repository Preservation Check

Before completing the task, inspect Git status again.

Compare it with the initial state.

Confirm whether this preflight introduced any repository changes.

Expected result:

```text
Preflight-introduced tracked changes: NONE
```

If a diagnostic command unexpectedly created or changed files:

- Do not silently delete them.
- Identify them.
- Explain which command caused them if known.
- Report the repository state accurately.
- Do not claim full read-only compliance.

Preserve pre-existing user changes.

---

# 10. Completion Rules

Do not report this task as complete merely because repository files were inspected.

Completion requires:

```text
Repository Inspection
        +
Authority Resolution
        +
Toolchain Assessment
        +
Configuration Assessment
        +
Asset Inventory
        +
Automation Assessment
        +
Safe Validation
        +
Gap Analysis
        +
Evidence
        +
Readiness Decision
```

If any material portion cannot be completed, say so explicitly.

Never fabricate missing evidence.

---

# 11. Prohibited Completion Behavior

Do not:

- Claim tests passed if they were not executed.
- Claim the build is healthy if no meaningful build exists yet.
- Treat future-state files as currently implemented.
- Convert expected future work into false preflight failures.
- Fix findings during the research task.
- Change architecture to eliminate a conflict.
- Create Release 0.8 implementation assets.
- Hide validation failures.
- infer repository state from this prompt when direct inspection is possible.
- Report `READY` merely because no obvious error was noticed.

---

# 12. Final Instruction

Perform the Repository Preflight now against the **actual current repository state**.

Inspect first.

Use authoritative repository evidence.

Remain read-only.

Do not implement Release 0.8.

Produce the complete **Repository Preflight Report** using the required output contract.

Finish with exactly one evidence-based readiness decision:

```text
READY
READY WITH ACTIONS
BLOCKED
```

If ready, identify **02 — Root Solution** as the next work package.

---

# Conclusion

Repository Preflight is the evidence boundary between Release 0.8 planning and implementation.

Its purpose is not to make the repository conform to the plan. Its purpose is to establish whether the actual repository, approved architecture, toolchain, configuration, and engineering automation provide a sufficiently coherent baseline for implementation to begin safely.

The execution model is:

```text
Inspect
   ↓
Resolve Authority
   ↓
Compare
   ↓
Validate
   ↓
Collect Evidence
   ↓
Classify Findings
   ↓
Decide Readiness
   ↓
Proceed or Escalate
```

A successful preflight gives the next coding task a verified starting point and prevents an AI coding agent from silently turning uncertainty into architecture or implementation decisions.

The central principle is:

> **Do not begin implementation from assumed repository state; begin from inspected, evidence-backed, and explicitly accepted repository state.**
