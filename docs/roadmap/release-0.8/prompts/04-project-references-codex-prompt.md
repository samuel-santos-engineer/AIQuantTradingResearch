# Codex Execution Prompt — Release 0.8 / 04 Project References

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.8 — Solution Skeleton |
| Work Package | 04 — Project References |
| Issue Type | Feature |
| Execution Mode | Controlled repository modification |
| Primary Agent | Codex |
| Prerequisite | 03 — Production Projects accepted as `COMPLETE` |
| Authorized Changes | Production-project `ProjectReference` relationships only |
| Expected Outcome | Approved dependency graph encoded in the four production `.csproj` files and objectively validated |

---

## Purpose

Encode the approved Release 0.8 production-project dependency graph.

Work Package 03 created the four physical production boundaries without connecting them. Work Package 04 now establishes only the compile-time relationships explicitly permitted by the architecture.

This work package must not add projects to the root solution, implement dependency injection, create runtime behavior, add packages, create tests, reorganize the solution, or perform later Release 0.8 work.

The objective is to make dependency direction explicit and machine-verifiable while preserving the projects themselves as minimal skeletons.

---

## Objective

Establish the approved dependency graph among:

```text
AIQuantTradingResearch.Domain
AIQuantTradingResearch.Application
AIQuantTradingResearch.Infrastructure
AIQuantTradingResearch.Worker
```

The intended graph is:

```text
Domain
  ↑
Application
  ↑
Infrastructure
  ↑
Worker
```

with the concrete direct-reference contract defined by the authoritative Release 0.8 and architecture documents.

Before editing project files, derive the exact direct references from those authoritative sources.

Do not infer additional references merely because they may be convenient.

At minimum, preserve these architectural principles:

- Domain depends on no production project.
- Application may depend inward on Domain.
- Infrastructure may depend only on approved inner-layer contracts.
- Worker is the outer composition boundary and may depend only on projects required for composition.
- No inner project may reference Worker.
- No Domain reference may point outward.
- No circular project references are permitted.

If the authoritative documents disagree about the exact direct-reference set, stop and report the conflict rather than choosing one silently.

---

# 1. Authority and Preconditions

Before modifying the repository, read completely:

```text
docs/roadmap/release-0.8/prompts/04-project-references-codex-prompt.md
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Read the dependency-defining architecture sources completely:

```text
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/solution/SOLUTION_STRUCTURE.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
```

Consult applicable engineering guidance where materially needed:

```text
docs/handbook/PROJECT_CONSTITUTION.md
docs/handbook/ENGINEERING.md
AI-Engineering-Toolkit/docs/AI_ASSISTED_ENGINEERING_WORKFLOW.md
AI-Engineering-Toolkit/playbooks/dotnet/01-solution-architecture.md
AI-Engineering-Toolkit/playbooks/dotnet/02-project-structure.md
AI-Engineering-Toolkit/playbooks/dotnet/04-dependency-management.md
AI-Engineering-Toolkit/playbooks/dotnet/12-project-review.md
```

Repository-specific Release 0.8 instructions take precedence over generic examples.

Do not invent a dependency rule that cannot be supported by the authoritative sources.

---

# 2. Prerequisite Verification

Verify the accepted WP03 baseline before making changes.

Expected production projects:

```text
src/AIQuantTradingResearch.Domain/AIQuantTradingResearch.Domain.csproj
src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Expected baseline:

```text
All four projects exist
All four projects parse
Effective TargetFramework = net10.0
ProjectReference count = 0 for every project
Root solution exists and parses
Root solution project count = 0
No test projects exist
```

If this baseline materially changed, do not guess or normalize unrelated state.

If project references already exist, determine whether they are pre-existing authorized changes. If ownership is ambiguous, stop and report `BLOCKED`.

---

# 3. Scope

## In Scope

You may:

- Inspect repository and Git state.
- Read dependency/architecture contracts.
- Inspect the four production project files.
- Determine the exact approved direct-reference matrix.
- Add approved `ProjectReference` entries to production `.csproj` files.
- Validate the resulting dependency graph.
- Validate absence of forbidden reverse/circular references.
- Inspect final Git state.
- Produce an evidence-based execution report.

## Out of Scope

Do not:

- Add projects to `AIQuantTradingResearch.slnx`.
- Create or delete production projects.
- Create tests or test projects.
- Create architecture tests.
- Add package references.
- Modify `Directory.Packages.props`.
- Modify `Directory.Build.props`.
- Modify `global.json`.
- Modify Worker runtime behavior.
- Implement dependency registration.
- Add `DependencyInjection.cs`.
- Add application/domain/infrastructure feature code.
- Add host configuration.
- Add solution folders.
- Modify engineering scripts.
- Modify documentation.
- Modify CI/GitHub workflows.
- Modify Docker assets.
- Reconcile placeholder directories.
- Stage, commit, push, or open a pull request.
- Begin WP05 or any later work package.

---

# 4. Authorized Change Set

Only these existing project files may be modified if required by the approved graph:

```text
src/AIQuantTradingResearch.Domain/AIQuantTradingResearch.Domain.csproj
src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

`Domain.csproj` is expected to remain without production `ProjectReference` entries.

No other tracked file is authorized for modification.

If tooling changes another file:

1. Stop.
2. Inspect the side effect.
3. Preserve pre-existing user state.
4. Revert only unquestionably task-owned unauthorized side effects when safe.
5. If ownership is uncertain, preserve the state and report `BLOCKED`.

---

# 5. Dependency Resolution Contract

## 5.1 Derive Before Editing

Before adding any reference, create an evidence-backed direct-reference matrix from the authoritative documents.

Use this conceptual form:

| From | To | Direct Reference? | Evidence |
| --- | --- | --- | --- |
| Domain | Application | NO | ... |
| Domain | Infrastructure | NO | ... |
| Domain | Worker | NO | ... |
| Application | Domain | YES/NO | ... |
| Application | Infrastructure | NO | ... |
| Application | Worker | NO | ... |
| Infrastructure | Domain | YES/NO | ... |
| Infrastructure | Application | YES/NO | ... |
| Infrastructure | Worker | NO | ... |
| Worker | Domain | YES/NO | ... |
| Worker | Application | YES/NO | ... |
| Worker | Infrastructure | YES/NO | ... |

The table above is a decision framework, not authorization for every possible `YES`.

Populate it from repository evidence.

## 5.2 Direct vs. Transitive Dependencies

Do not add a direct project reference merely because the dependency would be available transitively.

A direct reference must represent an intentional compile-time dependency owned by the referencing project.

Prefer the smallest graph that satisfies the documented architecture.

## 5.3 Forbidden Directions

Regardless of the final approved direct-reference set, these directions are forbidden unless an authoritative Release 0.8 document explicitly supersedes the rule:

```text
Domain → Application
Domain → Infrastructure
Domain → Worker

Application → Infrastructure
Application → Worker

Infrastructure → Worker
```

No project may directly or indirectly depend on itself.

No cycle is permitted.

## 5.4 Outer-Layer Discipline

Infrastructure and Worker are outer layers.

Do not use their position as justification for adding every possible inward reference.

Add only references required by the documented architecture.

---

# 6. Reference Creation Method

Prefer supported .NET CLI tooling rather than hand-editing XML when adding project references.

Typical form:

```text
dotnet add <referencing-project> reference <referenced-project>
```

If the installed SDK recommends a newer equivalent command, use the supported syntax appropriate to SDK `10.0.103`.

Do not add references using package references.

Do not introduce aliases or custom MSBuild metadata unless explicitly required by the repository contract.

After CLI modification, inspect the exact XML produced.

---

# 7. Execution Procedure

## Step 1 — Read the Contract

Read this prompt completely.

Read the Release plan, manifest, and dependency-defining architecture documents completely.

Do not execute later work packages.

## Step 2 — Record Initial Repository State

Run safe commands such as:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Record all pre-existing changes.

Do not stage, clean, reset, restore, or overwrite user work.

## Step 3 — Verify WP03 Project Inventory

Confirm exactly the four expected production `.csproj` files exist.

Search for unexpected production/test project files.

Record findings.

## Step 4 — Verify Root Solution Preservation

Run:

```text
dotnet sln AIQuantTradingResearch.slnx list
```

Expected:

```text
Project count = 0
```

Do not add projects to the solution.

## Step 5 — Verify Initial Reference State

Inspect every production `.csproj`.

Required baseline:

```text
Domain ProjectReference count = 0
Application ProjectReference count = 0
Infrastructure ProjectReference count = 0
Worker ProjectReference count = 0
```

If not true, classify the changed state before proceeding.

## Step 6 — Resolve the Authoritative Dependency Matrix

From the Release 0.8 plan/manifest and dependency architecture documents, determine:

- Required direct references.
- Allowed but unnecessary references.
- Forbidden references.

Record file/path evidence for every direct reference you intend to create.

If two authoritative sources materially conflict, do not choose silently.

Return:

```text
BLOCKED
```

with the conflict and minimum human decision required.

## Step 7 — Present the Resolved Graph Internally Before Mutation

Before editing, formulate the intended graph in a compact form such as:

```text
Domain          → none
Application     → ...
Infrastructure  → ...
Worker          → ...
```

Verify:

- Every edge is documented.
- No forbidden edge exists.
- No cycle exists.
- No convenience-only edge exists.

Only then mutate project files.

## Step 8 — Add Approved References

Use supported .NET CLI commands.

Execute one logical reference addition at a time so failures are attributable.

Do not add any reference not present in the resolved graph.

## Step 9 — Inspect Modified Project XML

Inspect all four `.csproj` files.

Confirm:

- Only approved `ProjectReference` entries were added.
- Relative paths are correct.
- No package references were introduced.
- No unrelated properties changed.
- Domain remains dependency-free.
- Formatting remains consistent with SDK/MSBuild conventions.

## Step 10 — Validate Reference Inventory

For each project, enumerate direct `ProjectReference` entries.

Compare actual graph with resolved expected graph.

Required:

```text
Actual graph = Approved graph
```

No extra edge is acceptable.

## Step 11 — Validate Forbidden References

Explicitly search for forbidden outward dependencies.

Confirm:

```text
Domain has no production references
Application does not reference Infrastructure or Worker
Infrastructure does not reference Worker
No inner layer references Worker
```

Also validate any stricter rules found in the authoritative documentation.

## Step 12 — Validate Acyclicity

Construct the actual project dependency graph and prove it is acyclic.

A simple topological/dependency traversal is sufficient.

Required:

```text
Cycles = 0
```

Do not rely only on visual inspection if a deterministic graph check can be performed safely.

## Step 13 — Validate Project Parsing

Use non-mutating MSBuild inspection for all four projects.

Examples:

```text
dotnet msbuild <project> -getProperty:TargetFramework
```

Confirm all projects still parse and resolve `net10.0`.

If a safe graph-oriented MSBuild inspection is available, use it.

## Step 14 — Build Validation Decision

A build may become meaningful once references exist, but do not broaden WP04 into build-system remediation.

If required restore assets already exist and a build can run without unauthorized changes, an individual or project-graph build may be used as supplementary evidence.

If restore is required or build failures relate to later WP05/WP06 configuration, report them without modifying unrelated configuration.

Project-reference structure and parsing are mandatory WP04 evidence.

Do not modify root build policy to make a build green.

## Step 15 — Confirm Root Solution Still Unchanged

Run again:

```text
dotnet sln AIQuantTradingResearch.slnx list
```

Required:

```text
Project count = 0
```

WP04 does not own solution membership.

## Step 16 — Inspect Git Diff

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
```

Inspect exact diffs for the four authorized `.csproj` files.

Expected tracked changes are limited to project-reference additions in the subset of project files requiring references.

No staged changes should exist.

## Step 17 — Final Scope Validation

Confirm:

```text
4 production projects still exist
Approved ProjectReference graph encoded
0 forbidden references
0 dependency cycles
0 test projects created
0 package references added by WP04
0 solution membership changes
0 runtime/feature implementation
0 root configuration changes
0 documentation changes attributable to WP04
0 eng changes
0 CI changes
0 staged changes
```

---

# 8. Expected Architectural Invariants

At completion, the following invariants must hold.

## Domain Independence

```text
Domain → none
```

Domain must remain independently compilable from other production projects.

## Inward Dependency Direction

Dependencies point toward more stable inner boundaries according to the repository architecture.

## No Reverse Dependency

Inner projects must not reference outer implementation/composition projects.

## No Cycles

The production graph must be a directed acyclic graph.

## Minimal Direct Graph

No redundant or speculative direct references.

## Solution Independence

The project dependency graph is encoded in `.csproj` files independently of solution membership.

The root `.slnx` remains empty until the work package that owns solution organization/membership.

---

# 9. Failure and Ambiguity Handling

## Architecture Conflict

If authoritative documents disagree about a direct reference:

- Do not guess.
- Do not choose based on generic Clean Architecture conventions alone.
- Cite the conflicting repository sources.
- Explain the concrete edge affected.
- Return `BLOCKED`.

## Existing Unauthorized Reference

If a pre-existing reference is found:

- Do not silently remove it.
- Determine whether it belongs to uncommitted human work.
- Preserve user state.
- Report ambiguity if ownership cannot be established.

## CLI Side Effect

If `dotnet add ... reference` changes more than the intended `.csproj`:

- Stop.
- Inspect.
- Preserve unrelated state.
- Revert only safe task-owned side effects.

## Invalid Reference Path

If the CLI cannot resolve an approved project:

- Verify paths and project existence.
- Do not move projects.
- Do not recreate projects.
- Return `BLOCKED` if the approved edge cannot be safely encoded.

## Build Failure

A build failure is not authorization to change:

```text
Directory.Build.props
Directory.Packages.props
global.json
Program.cs
```

Classify the failure and defer it to the owning work package unless it proves the reference graph itself is invalid.

---

# 10. Validation and Acceptance

WP04 is accepted only when:

- [ ] Prompt, Release plan, manifest, and dependency architecture were reviewed.
- [ ] Initial repository/Git state was recorded.
- [ ] All four WP03 production projects exist.
- [ ] Root solution exists and parses.
- [ ] Root solution contains zero projects before WP04.
- [ ] Initial project-reference state was recorded.
- [ ] Exact direct-reference matrix was derived from authoritative sources.
- [ ] Every created edge has repository evidence.
- [ ] Only approved direct references were added.
- [ ] Domain contains zero production `ProjectReference`s.
- [ ] No forbidden outward reference exists.
- [ ] No dependency cycle exists.
- [ ] No convenience-only speculative reference was added.
- [ ] No package reference was added by WP04.
- [ ] No project was added to the solution.
- [ ] All production projects still parse.
- [ ] Effective target framework remains `net10.0`.
- [ ] No test project was created.
- [ ] No source/feature/DI implementation was introduced.
- [ ] Root build/package/SDK configuration was not modified.
- [ ] Documentation, engineering scripts, CI, and Docker assets were not modified.
- [ ] Nothing was staged, committed, or pushed.
- [ ] Final Git state and exact diff were inspected.
- [ ] Validation evidence was recorded.
- [ ] Final decision was recorded.

Any failed mandatory criterion must affect the final decision.

---

# 11. Expected Output Contract

Return one complete **Project References Execution Report** in the Codex response.

Do not create a report file unless separately authorized.

Use this structure.

# Project References Execution Report

## 1. Executive Summary

State:

- What was authorized.
- What dependency graph was resolved.
- Which references were created.
- Whether architectural invariants hold.
- Final decision.

## 2. Execution Context

```text
Repository:
Branch:
Starting Commit:
Initial Working Tree:
Effective SDK:
```

## 3. Authoritative Sources Reviewed

List exact paths materially used to resolve dependencies.

## 4. Initial State Verification

```text
Production projects:
Initial ProjectReference counts:
Root solution project count:
Existing test projects:
Material pre-existing changes:
```

## 5. Resolved Dependency Matrix

Provide:

| From | To | Direct Reference? | Evidence |
| --- | --- | --- | --- |

Include all meaningful production-project pairings.

## 6. Resolved Direct Graph

Report exactly:

```text
Domain          → ...
Application     → ...
Infrastructure  → ...
Worker          → ...
```

## 7. Execution

For each reference actually added:

| Referencing Project | Referenced Project | Command | Exit Status | Result |
| --- | --- | --- | ---: | --- |

Do not list an edge as created unless it was actually created.

## 8. Final Project Reference Inventory

For each production project:

```text
Project:
Direct references:
Reference count:
Assessment:
```

## 9. Architectural Invariant Validation

| Invariant | Result | Evidence |
| --- | --- | --- |
| Domain independent | PASS/FAIL | ... |
| Dependencies point inward | PASS/FAIL | ... |
| No forbidden reverse edge | PASS/FAIL | ... |
| No cycles | PASS/FAIL | ... |
| Direct graph is minimal | PASS/FAIL | ... |
| Solution membership unchanged | PASS/FAIL | ... |

## 10. Validation Evidence

| Command | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

Include only commands actually executed.

## 11. Scope Compliance

| Scope Check | Result | Evidence |
| --- | --- | --- |
| Only approved project references added | PASS/FAIL | ... |
| No package references added | PASS/FAIL | ... |
| No solution membership changes | PASS/FAIL | ... |
| No tests created | PASS/FAIL | ... |
| No runtime/feature/DI implementation | PASS/FAIL | ... |
| No root configuration changes | PASS/FAIL | ... |
| No docs/eng/CI/Docker changes | PASS/FAIL | ... |
| No staging/commit/push | PASS/FAIL | ... |

## 12. Final Git State

Report:

```text
git status --short
```

Distinguish:

- Pre-existing changes.
- WP04-owned changes.
- Unexpected changes.

## 13. Findings

Only when necessary:

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed classifications:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 14. Acceptance Criteria

Reproduce applicable acceptance criteria with PASS/FAIL.

## 15. Final Decision

State exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

### COMPLETE

All mandatory WP04 criteria passed and no unresolved WP04-specific action remains.

### COMPLETE WITH ACTIONS

The approved graph is correctly encoded and valid, but a non-blocking finding has a clear later owner.

Do not use this merely because later Release 0.8 work remains.

### BLOCKED

A mandatory criterion cannot be satisfied safely, including unresolved architecture conflict.

## 16. Next Action

If complete:

```text
Proceed to:
05 — Root Build Configuration
```

If blocked, identify the minimum human decision or remediation required.

Do not begin WP05.

---

# 12. Prohibited Behaviors

Do not:

- Guess the dependency graph.
- Apply generic Clean Architecture rules in place of repository evidence.
- Add convenience references.
- Add projects to the solution.
- Create or delete projects.
- Create tests.
- Create architecture tests.
- Add packages.
- Modify central package management.
- Modify root build configuration.
- Modify `global.json`.
- Modify `Program.cs`.
- Implement DI.
- Implement host behavior.
- Implement feature/domain/application/infrastructure code.
- Modify documentation.
- Modify `eng/`.
- Modify `.github/`.
- Modify Docker assets.
- Reorganize directories.
- Format unrelated files.
- Stage.
- Commit.
- Push.
- Open a pull request.
- Begin WP05.

---

# 13. Completion Model

```text
Inspect
   ↓
Verify WP03 Baseline
   ↓
Read Dependency Authority
   ↓
Resolve Exact Direct Graph
   ↓
Check for Conflicts
   ↓
Add Approved References Only
   ↓
Inspect Project XML
   ↓
Validate Graph
   ├── Direction
   ├── Minimality
   └── Acyclicity
   ↓
Verify Solution Still Empty
   ↓
Inspect Git Diff
   ↓
Report Evidence
   ↓
COMPLETE | COMPLETE WITH ACTIONS | BLOCKED
```

---

# 14. Final Instruction

Execute **Phase 2 — Release 0.8 / Work Package 04 — Project References** against the actual current `AIQuantTradingResearch` repository.

Read this execution contract and all dependency-defining authoritative sources before changing any project file.

Derive the exact approved direct-reference graph from repository evidence.

If the sources materially conflict, stop and return `BLOCKED` rather than guessing.

Add only the approved `ProjectReference` relationships.

Keep Domain dependency-free.

Do not add projects to `AIQuantTradingResearch.slnx`.

Do not add packages, tests, DI, runtime behavior, or later Release 0.8 implementation.

Validate the graph for exactness, direction, minimality, and acyclicity.

Inspect final Git state and prove scope preservation.

Return the complete **Project References Execution Report**.

Finish with exactly one evidence-based decision:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

If complete, identify:

```text
05 — Root Build Configuration
```

as the next work package, but do not begin it.

---

# Conclusion

Work Package 04 transforms four isolated production project boundaries into an explicit compile-time architecture.

The intended transition is:

```text
Four Independent Projects
          ↓
Read Architectural Authority
          ↓
Resolve Direct Dependency Graph
          ↓
Encode ProjectReference Edges
          ↓
Validate Direction + Minimality + Acyclicity
          ↓
Preserve Empty Solution Membership
          ↓
Controlled Handoff to WP05
```

This work package deliberately separates **dependency structure** from project creation, solution organization, runtime composition, and build-policy configuration.

That separation makes each architectural edge reviewable and evidence-based. It also prevents an AI agent from introducing convenient dependencies that gradually erode the intended boundaries.

The central principle is:

> **Every project reference is an architectural decision: derive it from repository authority, encode only the minimum approved graph, and prove that dependency direction remains valid.**
