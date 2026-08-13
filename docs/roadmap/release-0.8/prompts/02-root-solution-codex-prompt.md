# Codex Execution Prompt — Release 0.8 / 02 Root Solution

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.8 — Solution Skeleton |
| Work Package | 02 — Root Solution |
| Issue Type | Feature |
| Execution Mode | Controlled repository modification |
| Primary Agent | Codex |
| Authorized Primary Artifact | `AIQuantTradingResearch.slnx` |
| Prerequisite | 01 — Repository Preflight accepted as `READY WITH ACTIONS` |
| Expected Outcome | Valid empty root `.slnx` solution with validation evidence |

---

## Purpose

Create the authoritative root solution artifact for **Phase 2 — Release 0.8: Solution Skeleton**.

This is the first controlled implementation change after Repository Preflight. The task is intentionally narrow: create and validate the root solution only.

Do not create production projects, test projects, project references, solution folders, source code, Worker implementation, dependency registration, engineering scripts, CI workflows, or other future Release 0.8 assets.

---

## Objective

Create exactly:

```text
AIQuantTradingResearch.slnx
```

at the repository root using supported .NET SDK tooling.

The solution must:

- Use `.slnx`.
- Be valid with the repository's configured SDK.
- Contain zero projects.
- Contain no speculative solution folders.
- Contain no obsolete Api/SharedKernel references.
- Introduce no unrelated repository changes.
- Preserve all pre-existing repository state.
- Be validated with objective evidence.

---

# 1. Authority and Preconditions

Before modifying the repository, read completely:

```text
docs/roadmap/release-0.8/prompts/02-root-solution-codex-prompt.md
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Consult applicable architecture and engineering guidance when needed, especially:

```text
docs/architecture/solution/SOLUTION_STRUCTURE.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
docs/architecture/implementation/NAMING_CONVENTIONS.md
docs/handbook/PROJECT_CONSTITUTION.md
docs/handbook/ENGINEERING.md
AI-Engineering-Toolkit/docs/AI_ASSISTED_ENGINEERING_WORKFLOW.md
AI-Engineering-Toolkit/playbooks/dotnet/01-solution-architecture.md
AI-Engineering-Toolkit/playbooks/dotnet/02-project-structure.md
AI-Engineering-Toolkit/playbooks/dotnet/12-project-review.md
```

The accepted Repository Preflight established that:

- No solution files exist.
- No project files exist.
- The obsolete Api/SharedKernel skeleton was removed.
- .NET SDK `10.0.103` is available.
- `.slnx` is supported.
- A dry run confirmed that the target solution can be generated.
- Remaining preflight findings belong to later work packages.

Re-check the actual current repository state before writing. Repository evidence is authoritative.

---

# 2. Scope

## In Scope

You may:

- Inspect repository and Git state.
- Inspect the Release 0.8 contract.
- Inspect relevant architecture/configuration.
- Verify the effective .NET SDK.
- Verify `.slnx` support.
- Create exactly `AIQuantTradingResearch.slnx`.
- Validate the solution.
- Inspect final Git state and diff.
- Produce an evidence-based execution report.

## Out of Scope

Do not create or modify anything under:

```text
src/**
tests/**
eng/**
.github/**
docker/**
samples/**
benchmarks/**
tools/**
scripts/**
```

Do not create:

- `AIQuantTradingResearch.Domain`
- `AIQuantTradingResearch.Application`
- `AIQuantTradingResearch.Infrastructure`
- `AIQuantTradingResearch.Worker`
- Any test project
- Any `.csproj`
- Any project reference
- Any package reference
- Any source/test file
- Any solution folder
- Any architecture-test infrastructure
- Any build/validation script
- Any CI workflow

Do not modify:

```text
global.json
Directory.Build.props
Directory.Packages.props
.editorconfig
.gitattributes
.gitignore
README.md
docker-compose.yml
```

Do not address warnings-as-errors, placeholder directory taxonomy, documentation drift, engineering scripts, or CI. Those belong to later work packages.

---

# 3. Authorized Change Set

The intended change attributable to this work package is exactly:

```text
A  AIQuantTradingResearch.slnx
```

No other tracked file may be intentionally added, modified, renamed, or deleted.

If tooling creates unexpected artifacts:

1. Stop and inspect them.
2. Do not silently adopt them.
3. Preserve all pre-existing user state.
4. Remove only unquestionably task-generated side effects when safe.
5. If ownership or cleanup is uncertain, preserve them and report `BLOCKED`.

Do not stage, commit, push, or open a pull request.

---

# 4. Safety and Repository Preservation

Before writing:

- Establish repository root.
- Record branch.
- Record commit SHA.
- Record `git status --short`.
- Confirm target path availability.
- Search for existing `.sln`, `.slnx`, and project files.
- Confirm the effective SDK.

If the repository materially differs from the accepted preflight baseline, do not guess.

Examples requiring reassessment:

- A solution already exists.
- Project files now exist.
- The target path is occupied.
- Significant unrelated changes make task attribution unsafe.
- Required SDK is unavailable.
- Release 0.8 contract materially changed.

Do not overwrite or delete unexpected assets.

---

# 5. Execution Procedure

## Step 1 — Read the Execution Contract

Read this prompt and the Release 0.8 Execution Plan/File Manifest completely.

Extract only the requirements belonging to Work Package 02.

Do not execute later work packages.

## Step 2 — Record Initial State

Use safe commands such as:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Record repository, branch, commit, and initial working-tree state.

Preserve any pre-existing changes.

## Step 3 — Confirm Target Availability

Verify that:

```text
AIQuantTradingResearch.slnx
```

does not exist.

Search for:

```text
*.sln
*.slnx
*.csproj
*.fsproj
*.vbproj
```

Expected baseline:

```text
Solutions: none
Projects: none
```

Do not overwrite an existing solution or absorb unexpected projects.

## Step 4 — Verify Toolchain

Inspect `global.json` and run safe commands such as:

```text
dotnet --version
dotnet --info
dotnet new sln --help
```

Confirm the configured/effective SDK and `.slnx` support.

Expected baseline is SDK `10.0.103`, but actual current repository/environment evidence is authoritative.

Do not install SDKs or modify `global.json`.

## Step 5 — Optional Dry Run

If useful, execute:

```text
dotnet new sln --name AIQuantTradingResearch --format slnx --dry-run --no-update-check
```

Confirm it proposes:

```text
AIQuantTradingResearch.slnx
```

and writes nothing.

## Step 6 — Create the Root Solution

From repository root, use supported SDK tooling.

Preferred command:

```text
dotnet new sln --name AIQuantTradingResearch --format slnx --no-update-check
```

Do not hand-author `.slnx`.

If the supported command fails:

- Capture the error.
- Diagnose without unrelated modifications.
- Do not fall back to `.sln`.
- Do not invent XML.
- Report `BLOCKED` if `.slnx` cannot be created safely.

## Step 7 — Inspect the Artifact

Inspect `AIQuantTradingResearch.slnx`.

Confirm:

- Correct root path.
- Valid text solution representation.
- Zero projects.
- Zero speculative solution folders.
- No Api/SharedKernel references.
- No future project entries.

Prefer the SDK-generated representation; do not rewrite it for stylistic preference.

## Step 8 — Validate Parsing

Use supported non-mutating solution tooling, preferably:

```text
dotnet sln AIQuantTradingResearch.slnx list
```

Required semantic result:

```text
Solution parses successfully
Project count = 0
```

Exact CLI wording may differ.

Do not create a temporary project for validation.

## Step 9 — Avoid Meaningless Future Validation

Do not force:

```text
dotnet build
dotnet test
dotnet format
```

to claim health for projects that do not exist.

Restore/build/test/format validation belongs to later work packages when meaningful.

## Step 10 — Inspect Change Set

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
```

Inspect the new file directly because an untracked file may not appear in a normal diff.

Expected task-owned status:

```text
?? AIQuantTradingResearch.slnx
```

There must be no Codex-created staged changes.

## Step 11 — Verify Scope Compliance

Confirm:

```text
No .csproj created
No source/test code created
No project references added
No package references added
No solution folders added
No root configuration modified
No documentation modified
No eng scripts modified
No GitHub workflow modified
No placeholder directories deleted
```

## Step 12 — Final Preservation Check

Compare final repository state with the initial state.

The only intentional task-owned change must be:

```text
AIQuantTradingResearch.slnx
```

Do not stage, commit, push, clean, reset, or restore user changes.

---

# 6. Root Solution Requirements

| Requirement | Required Value |
| --- | --- |
| Path | Repository root |
| Filename | `AIQuantTradingResearch.slnx` |
| Format | `.slnx` |
| Generation | Supported installed .NET SDK tooling |
| Project count | 0 |
| Solution folder count | 0 |
| Obsolete Api/SharedKernel references | 0 |
| Future project references | 0 |

Do not substitute legacy `.sln`.

---

# 7. Architectural Boundary

The future production project set is:

```text
AIQuantTradingResearch.Domain
AIQuantTradingResearch.Application
AIQuantTradingResearch.Infrastructure
AIQuantTradingResearch.Worker
```

Future test projects are:

```text
AIQuantTradingResearch.Domain.Tests
AIQuantTradingResearch.Application.Tests
AIQuantTradingResearch.Infrastructure.Tests
AIQuantTradingResearch.Architecture.Tests
```

These names provide context only.

**Do not create or register them during Work Package 02.**

The desired transition is:

```text
Before WP02
───────────
No solution
No projects

        ↓

After WP02
──────────
AIQuantTradingResearch.slnx
No projects
```

Nothing more.

---

# 8. Failure and Ambiguity Handling

## Existing Target

If `AIQuantTradingResearch.slnx` already exists:

- Do not overwrite it.
- Inspect and report the changed baseline.
- Default to `BLOCKED` unless explicit current authorization says otherwise.

## Unexpected Solution or Projects

If other solution/project assets now exist:

- Do not delete, merge, or register them.
- Report the changed state.
- If ownership or architecture is ambiguous, return `BLOCKED`.

## SDK Mismatch

If the required SDK is unavailable:

- Do not install one.
- Do not modify `global.json`.
- Do not silently use an incompatible major version.
- Report evidence and return `BLOCKED` if the approved artifact cannot be safely generated/validated.

## `.slnx` Unsupported

If `.slnx` is unexpectedly unsupported:

- Do not use `.sln`.
- Do not handwrite `.slnx`.
- Capture evidence.
- Return `BLOCKED`.

## Unexpected Side Effects

If tooling creates additional files:

- Stop.
- Inspect.
- Preserve pre-existing user work.
- Clean only unquestionably task-owned generated side effects when safe.
- Report uncertainty.

## Significant Ambiguity

Do not make new architecture decisions.

Use:

```text
Stop
→ Identify ambiguity
→ Provide evidence
→ Explain impact
→ Request human decision
```

---

# 9. Validation and Acceptance

Work Package 02 is complete only when:

- [ ] Execution prompt was read completely.
- [ ] Release 0.8 Execution Plan was reviewed.
- [ ] Release 0.8 File Manifest was reviewed.
- [ ] Initial repository root, branch, commit, and Git state were recorded.
- [ ] Target path was available before creation.
- [ ] No pre-existing solution/project collision blocked execution.
- [ ] Effective SDK was verified.
- [ ] `.slnx` support was verified.
- [ ] `AIQuantTradingResearch.slnx` was created at repository root.
- [ ] Supported .NET tooling created the solution.
- [ ] Solution parses successfully.
- [ ] Solution contains zero projects.
- [ ] Solution contains zero obsolete Api/SharedKernel references.
- [ ] No production/test project was created.
- [ ] No source/test code was created.
- [ ] No project/package reference was introduced.
- [ ] No root build configuration was modified.
- [ ] No documentation was modified.
- [ ] No engineering script was modified.
- [ ] No CI/GitHub workflow was modified.
- [ ] No unrelated tracked change was introduced.
- [ ] Codex did not stage, commit, or push.
- [ ] Final Git state was inspected.
- [ ] Validation evidence was recorded.
- [ ] Final decision was recorded.

Any failed mandatory criterion must affect the final decision.

---

# 10. Expected Output Contract

Return one complete **Root Solution Execution Report** in the Codex response.

Do not create a report file unless separately authorized.

Use this structure:

# Root Solution Execution Report

## 1. Executive Summary

State what was authorized, what was created, whether scope was preserved, and the final decision.

## 2. Execution Context

```text
Repository:
Branch:
Starting Commit:
Initial Working Tree:
Effective .NET SDK:
```

## 3. Authoritative Sources Reviewed

List exact paths actually used, including at minimum:

```text
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
docs/roadmap/release-0.8/prompts/02-root-solution-codex-prompt.md
```

## 4. Initial State Verification

```text
Existing .sln:
Existing .slnx:
Existing project files:
Target path available:
Material pre-existing changes:
```

## 5. Toolchain Verification

```text
Configured SDK:
Effective SDK:
.slnx support:
Assessment:
```

## 6. Execution

Record the actual command executed.

```text
Command:
Exit Status:
Result:
Created Artifact:
```

## 7. Artifact Assessment

```text
Path:
Format:
Parses Successfully:
Project Count:
Solution Folder Count:
Obsolete References:
```

## 8. Validation Evidence

| Command | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

Include only commands actually executed.

## 9. Scope Compliance

| Scope Check | Result | Evidence |
| --- | --- | --- |
| Only authorized artifact created | PASS/FAIL | ... |
| No projects created | PASS/FAIL | ... |
| No source/test code created | PASS/FAIL | ... |
| No root configuration changed | PASS/FAIL | ... |
| No documentation changed | PASS/FAIL | ... |
| No engineering scripts changed | PASS/FAIL | ... |
| No CI changes | PASS/FAIL | ... |
| No staging/commit/push | PASS/FAIL | ... |

## 10. Final Git State

Report `git status --short`.

Distinguish pre-existing, WP02, and unexpected changes.

## 11. Findings

Only if needed:

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed classifications:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 12. Acceptance Criteria

Reproduce the applicable checklist with PASS/FAIL results.

## 13. Final Decision

State exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

Use `COMPLETE` when the root solution was created and validated with no unresolved WP02-specific action.

Use `COMPLETE WITH ACTIONS` only when the root solution is valid but a non-blocking WP02 observation needs later handling. Do not use it merely because future work packages remain.

Use `BLOCKED` when mandatory acceptance criteria cannot be safely satisfied.

## 14. Next Action

If complete:

```text
Proceed to:
03 — Production Projects
```

If blocked, identify the minimum action or human decision needed before retrying WP02.

Do not begin WP03.

---

# 11. Prohibited Behaviors

Do not:

- Create future production/test projects.
- Add nonexistent projects to the solution.
- Create solution folders.
- Modify project architecture.
- Modify central package versions.
- Change warning policy.
- Add dependencies.
- Modify `global.json`.
- Modify repository documentation.
- Modify `eng/`.
- Add GitHub Actions.
- Reconcile placeholder directories.
- Delete `.gitkeep` files.
- Remove `docker-compose.yml`.
- Format unrelated files.
- Run repository-wide cleanup.
- Stage changes.
- Commit changes.
- Push changes.
- Open a pull request.
- Claim build/test health for projects that do not exist.
- Execute any later Release 0.8 work package.

---

# 12. Completion Model

```text
Inspect
   ↓
Confirm Preflight Baseline
   ↓
Verify SDK + .slnx
   ↓
Create One Root Solution
   ↓
Parse and Inspect
   ↓
Verify Zero Projects
   ↓
Inspect Git State
   ↓
Validate Scope
   ↓
Report Evidence
   ↓
COMPLETE | COMPLETE WITH ACTIONS | BLOCKED
```

---

# 13. Final Instruction

Execute **Phase 2 — Release 0.8 / Work Package 02 — Root Solution** against the actual current `AIQuantTradingResearch` repository.

Read the execution contract first.

Verify that the accepted preflight baseline still holds.

Create only:

```text
AIQuantTradingResearch.slnx
```

Use supported .NET SDK tooling.

Validate that the solution parses and contains zero projects.

Do not implement any later Release 0.8 work package.

Inspect final Git state and prove scope preservation.

Return the complete **Root Solution Execution Report**.

Finish with exactly one evidence-based decision:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

If complete, identify `03 — Production Projects` as the next work package, but do not begin it.

---

# Conclusion

Work Package 02 establishes the first authoritative implementation artifact of Release 0.8.

Its responsibility is deliberately small:

```text
Verified Pre-Skeleton Repository
            ↓
Create Root Solution
            ↓
Validate Root Solution
            ↓
Preserve Empty Project Membership
            ↓
Evidence
            ↓
Controlled Handoff to WP03
```

The value of this work package is not the complexity of the artifact. It is the controlled transition from an inspected repository baseline to an implementation state whose origin, scope, and validity are explicit.

By creating the solution independently from the projects that will later populate it, Release 0.8 preserves incremental validation, traceability, narrow AI authorization, and clear responsibility boundaries.

The central principle is:

> **Create only the structural artifact authorized by the current work package, validate it objectively, and leave every future responsibility to the work package that owns it.**
