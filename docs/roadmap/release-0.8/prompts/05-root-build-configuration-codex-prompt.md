# Codex Execution Prompt --- Release 0.8 / 05 Root Build Configuration

## Metadata

  -----------------------------------------------------------------------
  Field                               Value
  ----------------------------------- -----------------------------------
  Project                             AIQuantTradingResearch

  Phase                               Phase 2

  Release                             0.8 --- Solution Skeleton

  Work Package                        05 --- Root Build Configuration

  Issue Type                          Feature

  Execution Mode                      Controlled repository modification

  Primary Agent                       Codex

  Prerequisite                        04 --- Project References accepted
                                      as `COMPLETE`

  Primary Artifact                    `Directory.Build.props`

  Expected Outcome                    Evidence-backed, centralized .NET
                                      build defaults validated across all
                                      four production projects
  -----------------------------------------------------------------------

## Purpose

Inspect, reconcile, and minimally correct the existing repository-wide
.NET build configuration. The repository already contains root build
assets, so WP05 must validate and preserve correct policy rather than
replace files automatically.

WP05 owns common MSBuild policy only. It does not own solution
membership, project creation, dependency changes, package-version
changes, Worker hosting, dependency injection, tests, engineering
scripts, CI, or feature implementation.

## Objective

Establish the minimum approved common build policy in:

``` text
Directory.Build.props
```

Review properties including, when applicable:

``` text
TargetFramework
Nullable
ImplicitUsings
AnalysisLevel
TreatWarningsAsErrors
Deterministic
ContinuousIntegrationBuild
```

This is a review set, not authorization to add every property. Every
addition or change must be supported by repository authority.

# 1. Authority and Preconditions

Before modifying anything, read completely:

``` text
docs/roadmap/release-0.8/prompts/05-root-build-configuration-codex-prompt.md
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
global.json
Directory.Build.props
Directory.Packages.props
.editorconfig
```

Read applicable implementation guidance:

``` text
docs/architecture/implementation/IMPLEMENTATION_GUIDELINES.md
docs/architecture/implementation/CODING_PRINCIPLES.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
docs/architecture/implementation/NAMING_CONVENTIONS.md
docs/architecture/implementation/TESTING_STRATEGY.md
docs/handbook/PROJECT_CONSTITUTION.md
docs/handbook/ENGINEERING.md
```

Consult materially relevant toolkit guidance:

``` text
AI-Engineering-Toolkit/docs/AI_ASSISTED_ENGINEERING_WORKFLOW.md
AI-Engineering-Toolkit/playbooks/dotnet/02-project-structure.md
AI-Engineering-Toolkit/playbooks/dotnet/04-dependency-management.md
AI-Engineering-Toolkit/playbooks/dotnet/05-coding-standards.md
AI-Engineering-Toolkit/playbooks/dotnet/10-performance.md
AI-Engineering-Toolkit/playbooks/dotnet/12-project-review.md
```

If a listed guidance file does not exist, do not create or rename
documentation. Record the absence and continue unless it prevents a
mandatory policy decision.

Release-specific repository authority takes precedence over generic
guidance.

# 2. Prerequisite Verification

Verify the accepted WP04 baseline:

``` text
src/AIQuantTradingResearch.Domain/AIQuantTradingResearch.Domain.csproj
src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Expected graph:

``` text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

Expected state:

``` text
Effective SDK = 10.0.103
Effective TargetFramework = net10.0
Root solution exists and parses
Root solution project count = 0
No test projects exist
```

If the accepted dependency graph materially differs, do not repair it in
WP05. Report the changed baseline and return `BLOCKED` when it prevents
reliable validation.

# 3. Scope

## In Scope

You may:

-   Inspect repository/Git state and all four production projects.
-   Inspect effective MSBuild properties.
-   Inspect `global.json`, `Directory.Packages.props`, and
    `.editorconfig`.
-   Modify `Directory.Build.props` only when required by the resolved
    WP05 contract.
-   Remove redundant project-level common properties only when the same
    policy is authoritatively centralized and effective semantics remain
    identical.
-   Validate effective properties across all four production projects.
-   Revalidate the WP04 dependency graph.
-   Perform safe project-level build validation when appropriate.
-   Produce an evidence-based report.

## Out of Scope

Do not:

-   Add projects to `AIQuantTradingResearch.slnx`.
-   Create, delete, or rename projects.
-   Change `ProjectReference` relationships.
-   Add package references or change central package versions.
-   Modify `Directory.Packages.props`, `global.json`, or
    `.editorconfig`.
-   Modify `Program.cs` or implement Worker hosting.
-   Add dependency registration, features, tests, or architecture tests.
-   Modify documentation, `eng/`, `.github/`, or Docker assets.
-   Stage, commit, push, or open a pull request.
-   Begin WP06.

# 4. Authorized Change Set

Primary authorized file:

``` text
Directory.Build.props
```

These project files may be modified only for proven redundant-property
cleanup directly caused by WP05 centralization:

``` text
src/AIQuantTradingResearch.Domain/AIQuantTradingResearch.Domain.csproj
src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Inspection-only files:

``` text
global.json
Directory.Packages.props
.editorconfig
```

If `Directory.Build.props` already satisfies the authoritative contract,
do not manufacture a change. WP05 may complete through validation alone.

# 5. Build Policy Resolution Contract

Before editing, create this property-resolution matrix:

  ----------------------------------------------------------------------------------------
  Property                     Current     Effective   Required    Authority   Action
                               Root Value  Project     Value                   
                                           Value                               
  ---------------------------- ----------- ----------- ----------- ----------- -----------
  TargetFramework              ...         ...         ...         ...         ...

  Nullable                     ...         ...         ...         ...         ...

  ImplicitUsings               ...         ...         ...         ...         ...

  AnalysisLevel                ...         ...         ...         ...         ...

  TreatWarningsAsErrors        ...         ...         ...         ...         ...

  Deterministic                ...         ...         ...         ...         ...

  ContinuousIntegrationBuild   ...         ...         ...         ...         ...
  ----------------------------------------------------------------------------------------

Allowed actions:

``` text
KEEP
ADD
CHANGE
REMOVE-DUPLICATE
NO-ACTION
BLOCKED
```

Add other properties only when materially present or explicitly
required.

Common repository policy belongs in `Directory.Build.props`;
project-specific semantics remain local.

Validate effective behavior, not only raw XML. Use MSBuild evaluation
such as:

``` text
dotnet msbuild <project> -getProperty:TargetFramework
dotnet msbuild <project> -getProperty:Nullable
dotnet msbuild <project> -getProperty:ImplicitUsings
dotnet msbuild <project> -getProperty:TreatWarningsAsErrors
```

Do not automatically enable warnings-as-errors, analyzers, or
CI-specific behavior because they are general best practices. Resolve
values from repository authority.

Do not add analyzer packages in WP05.

# 6. SDK, Framework, and Package Boundaries

`global.json` is inspection-only.

Expected:

``` text
SDK = 10.0.103
TargetFramework = net10.0
```

If configured and effective SDKs disagree, do not modify `global.json`;
report the mismatch.

`Directory.Packages.props` is inspection-only. Do not add/remove package
versions, toggle central package management, or introduce package
references.

# 7. Project File Normalization

A project-local property may be removed only if all are true:

``` text
1. Directory.Build.props defines the authoritative value.
2. The local value is semantically identical.
3. The property is intended to be repository-wide.
4. Effective MSBuild behavior remains unchanged.
5. Removal does not alter project type or runtime semantics.
```

Do not remove Worker-specific properties for visual uniformity. Do not
rewrite project files unnecessarily.

# 8. Execution Procedure

## Step 1 --- Read Authority

Read this prompt, Release plan, manifest, root configuration, and
applicable engineering guidance completely before mutation.

## Step 2 --- Record Initial State

Run:

``` text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Preserve all pre-existing changes. Do not clean/reset/restore user work.

## Step 3 --- Verify Toolchain

Run:

``` text
dotnet --version
dotnet --info
```

Inspect `global.json` and confirm the effective SDK.

## Step 4 --- Verify WP04 Baseline

Confirm all four production projects and the exact accepted dependency
graph. Do not change it.

## Step 5 --- Verify Solution State

Run:

``` text
dotnet sln AIQuantTradingResearch.slnx list
```

Required:

``` text
Project count = 0
```

## Step 6 --- Inspect Root Build Assets

Read `Directory.Build.props`, `Directory.Packages.props`,
`.editorconfig`, and `global.json` completely.

## Step 7 --- Measure Effective Properties

For all four projects, evaluate at minimum:

``` text
TargetFramework
Nullable
ImplicitUsings
AnalysisLevel
TreatWarningsAsErrors
Deterministic
ContinuousIntegrationBuild
```

Inspect other materially relevant common properties found in root
policy.

## Step 8 --- Resolve Property Matrix

Classify each property as `KEEP`, `ADD`, `CHANGE`, `REMOVE-DUPLICATE`,
`NO-ACTION`, or `BLOCKED`.

Every `ADD` or `CHANGE` requires repository evidence. If authoritative
sources materially conflict, return `BLOCKED`.

## Step 9 --- Apply Minimal Root Changes

Modify `Directory.Build.props` only as resolved. Preserve valid comments
and existing policy. Prefer a minimal diff. Do not add speculative
properties.

## Step 10 --- Remove Proven Redundancy

If project files duplicate newly centralized common policy and satisfy
all normalization criteria, remove only those duplicates. Do not touch
`ProjectReference` entries.

## Step 11 --- Re-evaluate Effective Configuration

Repeat MSBuild evaluation for all four projects.

Required:

``` text
All projects parse
TargetFramework = net10.0
Required common policies resolve consistently
Project-specific semantics remain intact
```

## Step 12 --- Revalidate Dependency Graph

Required graph:

``` text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

Required:

``` text
Graph changed by WP05 = No
Cycles = 0
```

## Step 13 --- Validate Package Boundary

Confirm:

``` text
0 new PackageReference entries
0 Directory.Packages.props changes
```

## Step 14 --- Build Validation

Project-level compilation is useful evidence once root policy is
resolved.

When safe, run:

``` text
dotnet build src/AIQuantTradingResearch.Domain/AIQuantTradingResearch.Domain.csproj
dotnet build src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
dotnet build src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
dotnet build src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Normal ignored `bin/` and `obj/` outputs are not repository artifacts.

Do not change package policy, source code, or future WP06 behavior
merely to make a build pass.

If Worker compilation fails solely because intentionally deferred WP06
host work is absent, classify that accurately rather than expanding
WP05.

## Step 15 --- Revalidate Empty Solution

Run `dotnet sln AIQuantTradingResearch.slnx list` again. Project count
must remain zero.

## Step 16 --- Inspect Final Diff

Run:

``` text
git status --short
git diff -- .
git diff --cached -- .
```

Distinguish pre-existing, WP05-owned, generated/ignored, and unexpected
changes. Nothing may be staged.

## Step 17 --- Final Scope Check

Confirm:

``` text
Root build policy resolved from repository authority
Directory.Build.props valid
Effective common properties validated across all four projects
WP04 graph unchanged
0 package-management changes
0 solution-membership changes
0 tests created
0 runtime/DI/feature implementation
0 docs/eng/CI/Docker changes attributable to WP05
0 staged changes
```

# 9. Failure and Ambiguity Handling

If existing configuration is already correct, do not edit it merely to
produce a diff.

If authoritative sources conflict on a mandatory property:

``` text
Stop
→ Cite conflicting sources
→ Identify affected property
→ Explain impact
→ Request human decision
→ BLOCKED
```

If a required property/value is unsupported by the effective SDK, do not
change SDK or silently substitute another setting.

A build failure is not authorization to repair Worker behavior, future
dependencies, tests, CI, or unrelated source.

Unexpected tooling side effects must not be adopted. Preserve
pre-existing user state.

# 10. Validation and Acceptance

WP05 is accepted only when:

-   [ ] Prompt, Release plan, manifest, and applicable build guidance
    were reviewed.
-   [ ] Initial Git state was recorded.
-   [ ] Effective SDK was verified.
-   [ ] WP04 project inventory and dependency graph were verified.
-   [ ] Root solution contains zero projects.
-   [ ] Existing root build assets were inspected.
-   [ ] Effective common properties were measured before mutation.
-   [ ] Property-resolution matrix was created.
-   [ ] Every `ADD`/`CHANGE` has repository authority.
-   [ ] `Directory.Build.props` contains the resolved common policy.
-   [ ] No speculative build property was introduced.
-   [ ] Project-level duplicates were removed only when proven safe.
-   [ ] All four projects parse after WP05.
-   [ ] Effective target framework remains `net10.0`.
-   [ ] Required effective properties are consistent.
-   [ ] WP04 graph remains unchanged and acyclic.
-   [ ] No package reference was added.
-   [ ] `Directory.Packages.props`, `global.json`, and `.editorconfig`
    were not modified.
-   [ ] Root solution membership remains unchanged.
-   [ ] No tests, runtime, feature, or DI implementation was introduced.
-   [ ] No documentation, engineering script, CI, or Docker asset was
    modified.
-   [ ] Nothing was staged, committed, or pushed.
-   [ ] Final Git state and exact diff were inspected.
-   [ ] Validation evidence and final decision were recorded.

# 11. Expected Output Contract

Return one complete **Root Build Configuration Execution Report** in the
Codex response. Do not create a report file unless separately
authorized.

Use this structure:

# Root Build Configuration Execution Report

## 1. Executive Summary

State authorization, initial configuration condition, changes (if any),
effective result, and final decision.

## 2. Execution Context

``` text
Repository:
Branch:
Starting Commit:
Initial Working Tree:
Configured SDK:
Effective SDK:
```

## 3. Authoritative Sources Reviewed

List exact paths materially used.

## 4. WP04 Baseline Verification

``` text
Production projects:
Dependency graph:
Root solution project count:
Effective target frameworks:
Material pre-existing changes:
```

## 5. Initial Root Build Configuration

Summarize `Directory.Build.props`, `global.json`,
`Directory.Packages.props`, and `.editorconfig`.

## 6. Property Resolution Matrix

  -----------------------------------------------------------------------
  Property    Initial     Initial     Required    Authority   Action
              Root Value  Effective   Value                   
                          Value                               
  ----------- ----------- ----------- ----------- ----------- -----------

  -----------------------------------------------------------------------

## 7. Changes Applied

  File   Change   Reason   Authority
  ------ -------- -------- -----------

If none:

``` text
No repository modification required by WP05.
```

## 8. Final Effective Configuration

  Property                     Domain   Application   Infrastructure   Worker
  ---------------------------- -------- ------------- ---------------- --------
  TargetFramework              ...      ...           ...              ...
  Nullable                     ...      ...           ...              ...
  ImplicitUsings               ...      ...           ...              ...
  AnalysisLevel                ...      ...           ...              ...
  TreatWarningsAsErrors        ...      ...           ...              ...
  Deterministic                ...      ...           ...              ...
  ContinuousIntegrationBuild   ...      ...           ...              ...

## 9. Project File Normalization

``` text
Project-level properties removed:
Effective semantics preserved:
ProjectReference graph changed:
```

## 10. Build Validation

  Project   Command     Exit Status   Warnings   Errors Assessment
  --------- --------- ------------- ---------- -------- ------------

If not executed, state why.

## 11. Dependency and Solution Preservation

``` text
Dependency graph matches WP04:
Cycles:
Root solution project count:
Solution membership changed:
```

## 12. Validation Evidence

  Command     Exit Status Result   Interpretation
  --------- ------------- -------- ----------------

## 13. Scope Compliance

  Scope Check                                 Result      Evidence
  ------------------------------------------- ----------- ----------
  Root build policy resolved from authority   PASS/FAIL   ...
  Only authorized files changed               PASS/FAIL   ...
  No dependency graph changes                 PASS/FAIL   ...
  No package-management changes               PASS/FAIL   ...
  No solution-membership changes              PASS/FAIL   ...
  No runtime/feature/DI changes               PASS/FAIL   ...
  No tests created                            PASS/FAIL   ...
  No docs/eng/CI/Docker changes               PASS/FAIL   ...
  No staging/commit/push                      PASS/FAIL   ...

## 14. Final Git State

Report `git status --short` and distinguish pre-existing, WP05-owned,
ignored/generated, and unexpected changes.

## 15. Findings

When needed:

  ID   Classification   Finding   Evidence   Required Action   Owner
  ---- ---------------- --------- ---------- ----------------- -------

Allowed classifications:

``` text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 16. Acceptance Criteria

Reproduce applicable WP05 criteria with PASS/FAIL.

## 17. Final Decision

State exactly one:

``` text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

Use `COMPLETE` when root build policy is validated with no unresolved
WP05-specific action. Use `COMPLETE WITH ACTIONS` only for a valid
configuration with a non-blocking later-owned finding. Use `BLOCKED`
when mandatory policy resolution or validation cannot be completed
safely.

## 18. Next Action

If complete:

``` text
Proceed to:
06 — Minimal Worker Host
```

Do not begin WP06.

# 12. Prohibited Behaviors

Do not:

-   Replace valid root configuration without evidence.
-   Add arbitrary best-practice properties.
-   Make warning policy stricter without authority.
-   Add analyzer packages or package references.
-   Modify central package versions, `global.json`, or `.editorconfig`.
-   Add projects to the solution.
-   Change project references.
-   Create projects/tests.
-   Implement Worker hosting, DI, or features.
-   Modify docs, `eng/`, `.github/`, or Docker assets.
-   Reformat unrelated files.
-   Stage, commit, push, or open a pull request.
-   Begin WP06.

# 13. Completion Model

``` text
Inspect
   ↓
Verify WP04 Baseline
   ↓
Read Root Build Authority
   ↓
Measure Effective Properties
   ↓
Resolve Property Matrix
   ↓
Apply Minimal Central Configuration
   ↓
Remove Only Proven Redundancy
   ↓
Re-evaluate Effective Properties
   ↓
Validate Builds Where Appropriate
   ↓
Preserve Dependency Graph + Empty Solution
   ↓
Inspect Git Diff
   ↓
Report Evidence
   ↓
COMPLETE | COMPLETE WITH ACTIONS | BLOCKED
```

# 14. Final Instruction

Execute **Phase 2 --- Release 0.8 / Work Package 05 --- Root Build
Configuration** against the actual current `AIQuantTradingResearch`
repository.

Read all applicable repository authority before modifying root build
configuration.

Treat the existing `Directory.Build.props` as an artifact to inspect and
reconcile, not something to replace automatically.

Measure effective MSBuild properties before changes. Derive required
root policy from repository evidence. Apply only minimum authorized
changes.

Do not modify `global.json`, `Directory.Packages.props`, or
`.editorconfig`.

Do not change the WP04 project-reference graph or add projects to the
root solution.

Do not implement Worker hosting, DI, tests, features, engineering
scripts, or CI.

Re-evaluate effective configuration across all four production projects,
perform safe build validation where meaningful, inspect final Git state,
and prove scope preservation.

Return the complete **Root Build Configuration Execution Report**.

Finish with exactly one evidence-based decision:

``` text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

If complete, identify `06 — Minimal Worker Host` as next, but do not
begin it.

# Conclusion

Work Package 05 establishes repository-wide build policy before runtime
composition begins.

``` text
Production Project Graph
          ↓
Inspect Existing Root Build Assets
          ↓
Measure Effective MSBuild State
          ↓
Resolve Policy from Repository Authority
          ↓
Apply Minimal Central Configuration
          ↓
Validate Effective Project Configuration
          ↓
Preserve Dependency Graph + Solution State
          ↓
Controlled Handoff to WP06
```

The important outcome is not the number of properties in
`Directory.Build.props`. Every common property must have a deliberate
owner, evidence-backed value, and consistent effective behavior.

A centralized build file should reduce duplication and environmental
ambiguity without becoming a dumping ground for speculative policy.

> **Centralize only proven common build policy, validate effective
> behavior rather than raw XML, and never turn a configuration work
> package into unrelated repository cleanup.**
