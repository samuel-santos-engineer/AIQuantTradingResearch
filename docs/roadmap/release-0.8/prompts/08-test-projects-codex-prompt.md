# Codex Execution Prompt — Release 0.8 / 08 Test Projects

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.8 — Solution Skeleton |
| Work Package | 08 — Test Projects |
| Issue Type | Tests |
| Execution Mode | Controlled repository modification |
| Primary Agent | Codex |
| Prerequisite | 07 — Dependency Registration accepted as `COMPLETE` |
| Primary Area | `tests/` |
| Expected Outcome | Minimal test-project skeleton for Domain, Application, Infrastructure, and Architecture boundaries, with valid references and no substantive test implementation |

---

## Purpose

Establish the test-project boundaries required by the Release 0.8 Solution Skeleton.

WP01–WP07 established the repository baseline, production projects, dependency graph, common build configuration, Worker host, and dependency-registration seams.

WP08 now creates the minimum test project structure that later work packages can use without prematurely implementing test suites.

The goal is:

```text
Production Architecture
        ↓
Explicit Test Project Boundaries
        ↓
Correct Project References
        ↓
Test SDK + Framework Baseline
        ↓
Buildable / Discoverable Empty Test Projects
```

WP08 owns test-project creation and their minimum test infrastructure only.

It does not own architecture rules, real unit tests, integration tests, fixtures, mocks, test data, CI execution, coverage policy, or production feature behavior.

---

## Objective

Create the Release 0.8 test project skeleton under:

```text
tests/
```

Expected projects:

```text
tests/AIQuantTradingResearch.Domain.Tests/
tests/AIQuantTradingResearch.Application.Tests/
tests/AIQuantTradingResearch.Infrastructure.Tests/
tests/AIQuantTradingResearch.Architecture.Tests/
```

Expected project files:

```text
tests/AIQuantTradingResearch.Domain.Tests/AIQuantTradingResearch.Domain.Tests.csproj
tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj
tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj
tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj
```

Create only the minimum files required for valid test projects.

Do not add substantive tests.

Do not add the projects to `AIQuantTradingResearch.slnx` unless the authoritative Release 0.8 plan explicitly assigns solution membership to WP08. The accepted baseline through WP07 has intentionally kept the root solution at zero projects, so preserve that state unless repository authority clearly says otherwise.

---

# 1. Authority and Preconditions

Before modifying anything, read completely:

```text
docs/roadmap/release-0.8/prompts/08-test-projects-codex-prompt.md
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Read test and architecture guidance:

```text
docs/architecture/implementation/TESTING_STRATEGY.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
Directory.Build.props
Directory.Packages.props
global.json
```

Read all current production project files:

```text
src/AIQuantTradingResearch.Domain/AIQuantTradingResearch.Domain.csproj
src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Consult materially relevant Toolkit guidance:

```text
AI-Engineering-Toolkit/docs/AI_ASSISTED_ENGINEERING_WORKFLOW.md
AI-Engineering-Toolkit/playbooks/dotnet/02-project-structure.md
AI-Engineering-Toolkit/playbooks/dotnet/04-dependency-management.md
AI-Engineering-Toolkit/playbooks/dotnet/08-testing.md
AI-Engineering-Toolkit/playbooks/dotnet/12-project-review.md
```

If an exact listed guidance file does not exist, do not create or rename documentation. Record the absence and continue using available authority unless the missing source prevents a mandatory decision.

Release-specific repository authority takes precedence over generic Toolkit guidance.

---

# 2. Accepted Baseline from WP07

Expected production graph:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

Expected production build state:

```text
Domain          = PASS
Application     = PASS
Infrastructure  = PASS
Worker          = PASS
Warnings        = 0
Errors          = 0
```

Expected composition state:

```text
Worker
  → AddApplication()
  → AddInfrastructure()
  → Build()
  → RunAsync()
```

Expected registration state:

```text
AddApplication       = empty boundary
AddInfrastructure    = empty boundary
Hosted services      = none
Feature behavior     = none
```

Expected root solution state:

```text
AIQuantTradingResearch.slnx exists
Project count = 0
```

Expected current test state:

```text
No test projects
```

Verify actual current state before mutation.

If the baseline materially differs, record the difference and do not silently repair earlier work packages.

---

# 3. Scope

## In Scope

You may:

- Inspect repository and Git state.
- Inspect Release 0.8 testing authority.
- Determine the approved .NET test framework from repository guidance.
- Determine required test SDK/framework packages.
- Create exactly the authorized test projects.
- Remove template-generated sample tests when they are not part of the Release 0.8 manifest.
- Add only the required production `ProjectReference` entries to test projects.
- Add required centrally managed test package versions when missing.
- Build each test project.
- Run test discovery/execution to prove the empty test projects are valid.
- Revalidate the production dependency graph.
- Inspect final Git state.
- Produce an evidence-based report.

## Out of Scope

Do not:

- Implement real unit tests.
- Implement architecture rules/tests.
- Implement integration tests.
- Implement end-to-end tests.
- Create fixtures, builders, mocks, fakes, stubs, or test data.
- Add coverage tooling unless explicitly required by the Release 0.8 authority for project creation.
- Add assertion/helper libraries unless explicitly required.
- Add test containers.
- Add database/provider test dependencies.
- Add feature-specific packages.
- Modify production source code.
- Modify production `ProjectReference` relationships.
- Modify Worker composition.
- Add hosted services.
- Add production features.
- Modify root build policy.
- Modify `global.json`.
- Modify `.editorconfig`.
- Modify documentation.
- Modify engineering scripts.
- Modify CI/GitHub workflows.
- Modify Docker assets.
- Stage, commit, push, or open a pull request.
- Begin WP09.

---

# 4. Authorized Test Project Set

Create exactly these projects unless the Release 0.8 execution plan/manifest explicitly defines a different set:

```text
AIQuantTradingResearch.Domain.Tests
AIQuantTradingResearch.Application.Tests
AIQuantTradingResearch.Infrastructure.Tests
AIQuantTradingResearch.Architecture.Tests
```

Do not create:

```text
AIQuantTradingResearch.Worker.Tests
AIQuantTradingResearch.IntegrationTests
AIQuantTradingResearch.EndToEndTests
AIQuantTradingResearch.FunctionalTests
AIQuantTradingResearch.PerformanceTests
```

unless the authoritative Release 0.8 manifest explicitly requires them in WP08.

If the manifest conflicts with the expected four-project set above, follow the manifest and clearly report the difference.

---

# 5. Test Framework Resolution Contract

Do not assume a test framework solely from generic preference.

Resolve the test framework from repository authority.

Inspect:

```text
docs/architecture/implementation/TESTING_STRATEGY.md
Directory.Packages.props
existing repository conventions
Release 0.8 execution plan
Release 0.8 file manifest
```

Determine:

```text
Test framework:
Test SDK:
Runner/adapter package:
Coverage package required now: YES/NO
Additional assertion library required now: YES/NO
```

Prefer the smallest supported baseline.

If repository authority specifies xUnit, use xUnit.

If it specifies NUnit or MSTest, follow that authority.

If no authoritative test framework can be resolved safely, return `BLOCKED` rather than choosing arbitrarily.

---

# 6. Template Strategy

Prefer supported .NET SDK tooling to create test projects.

Before creation:

```text
dotnet new list
```

and inspect the appropriate test template/help if necessary.

Use `--no-restore` when useful to keep creation deterministic, followed by explicit restore/build validation.

Do not retain template-generated sample tests such as:

```text
UnitTest1.cs
Test1.cs
```

unless the Release manifest explicitly requires a smoke test.

The target WP08 state is a test-project skeleton, not artificial passing tests.

---

# 7. Expected Test Dependency Graph

Resolve exact references from repository authority, but the expected minimal graph is:

```text
Domain.Tests
    → Domain

Application.Tests
    → Application

Infrastructure.Tests
    → Infrastructure

Architecture.Tests
    → production assemblies required for future architecture validation
```

For Architecture.Tests, do not add architecture-test implementation or an architecture-testing library in WP08 unless the manifest explicitly requires the package as part of the project skeleton.

Determine its project references deliberately from the testing strategy and dependency rules.

A likely architecture-test reference set may include:

```text
Domain
Application
Infrastructure
Worker
```

but this is not automatic authorization. Use repository authority.

Test-project references do not change the production dependency graph.

---

# 8. Package Governance

The repository uses Central Package Management.

When required test packages are missing from `Directory.Packages.props`:

- Add only the package versions required by the approved test-project baseline.
- Keep versions centralized.
- Keep project-level `PackageReference` entries versionless.
- Do not modify unrelated package versions.
- Do not upgrade existing production packages.
- Do not add convenience libraries without explicit need.

Likely categories include:

```text
Microsoft.NET.Test.Sdk
test framework
test runner/adapter
```

Actual package identities and versions must be resolved from repository authority and compatible SDK/tooling evidence.

Do not infer that every template-generated package must remain. Keep only packages needed for the selected test framework and repository policy.

---

# 9. Test Project Configuration Contract

Each test project should inherit common repository build policy from:

```text
Directory.Build.props
```

Avoid repeating centrally owned properties unnecessarily.

Test-specific properties may remain local when appropriate, such as:

```text
IsPackable
IsTestProject
```

Use the generated/template structure only when consistent with repository conventions.

Do not add unrelated build properties.

Target framework must resolve to:

```text
net10.0
```

unless authoritative repository policy states otherwise.

---

# 10. Execution Procedure

## Step 1 — Read Authority

Read this prompt, Release plan, manifest, testing strategy, project structure, dependency rules, package governance, and applicable Toolkit testing guidance completely.

## Step 2 — Record Initial Git State

Run:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Record all pre-existing changes.

Do not clean/reset/restore user work.

## Step 3 — Verify WP07 Baseline

Confirm:

```text
Four production projects exist
All production projects build
AddApplication exists
AddInfrastructure exists
Worker invokes both
No hosted services
No test projects exist
```

Do not repair earlier work-package defects unless explicitly authorized.

## Step 4 — Verify Production Graph

Confirm:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

Required:

```text
Cycles = 0
```

## Step 5 — Verify Root Solution State

Run:

```text
dotnet sln AIQuantTradingResearch.slnx list
```

Record the current project count.

Expected accepted baseline:

```text
Project count = 0
```

## Step 6 — Resolve Test Framework

Produce:

```text
Selected test framework:
Test SDK package:
Runner/adapter:
Coverage package required:
Assertion library required:
Authority:
```

If unresolved, stop with `BLOCKED`.

## Step 7 — Resolve Test Project Manifest

Produce:

| Test Project | Required | Production Reference(s) | Purpose |
| --- | --- | --- | --- |
| Domain.Tests | YES/NO | ... | Domain test boundary |
| Application.Tests | YES/NO | ... | Application test boundary |
| Infrastructure.Tests | YES/NO | ... | Infrastructure test boundary |
| Architecture.Tests | YES/NO | ... | Architecture validation boundary |

Do not create projects outside the authoritative manifest.

## Step 8 — Resolve Package Versions

Inspect `Directory.Packages.props`.

Determine which required test packages are already centrally versioned.

For missing packages, derive compatible versions from repository/tooling authority.

Do not modify production package versions.

## Step 9 — Create Test Directories and Projects

Use supported .NET tooling.

Create projects directly at their final paths.

Avoid creating temporary solutions or unrelated files.

Do not add projects to the root solution unless WP08 authority explicitly requires it.

## Step 10 — Remove Template Sample Tests

Remove template-generated sample test source files unless explicitly required.

Required final skeleton should not contain meaningless tests created only to make `dotnet test` report a passing test.

## Step 11 — Normalize Test Project Files

Ensure:

```text
TargetFramework effective value = net10.0
Central package management respected
Required test packages only
IsTestProject appropriate
IsPackable appropriate
No redundant common policy unless justified
```

## Step 12 — Add Production Project References

Use supported .NET CLI tooling.

Expected minimum:

```text
Domain.Tests          → Domain
Application.Tests     → Application
Infrastructure.Tests  → Infrastructure
```

Resolve Architecture.Tests references from repository authority.

Do not add test-to-test project references unless explicitly required.

## Step 13 — Restore Test Projects

Run explicit restore for each test project or an equivalent bounded restore operation.

Required:

```text
Exit Status = 0
```

Record dependency resolution.

## Step 14 — Build Test Projects

Build each test project individually.

Required:

```text
Exit Status = 0
Errors = 0
```

Record warnings.

## Step 15 — Test Discovery / Empty Execution Validation

Run:

```text
dotnet test <test-project> --no-build
```

for each created test project, or the repository-approved equivalent.

An empty test project may report zero tests.

That is acceptable for WP08 if the command exits successfully.

Do not add fake tests merely to force a non-zero test count.

Record:

```text
Discovered tests:
Executed tests:
Exit status:
Assessment:
```

## Step 16 — Revalidate Production Builds

Rebuild all production projects or otherwise prove WP08 introduced no production regression.

Required:

```text
Domain          PASS
Application     PASS
Infrastructure  PASS
Worker          PASS
```

## Step 17 — Revalidate Production Dependency Graph

Required:

```text
Production graph matches WP04 = Yes
Cycles = 0
Production ProjectReferences changed = No
```

## Step 18 — Revalidate Root Solution Membership

Run:

```text
dotnet sln AIQuantTradingResearch.slnx list
```

Unless WP08 explicitly owns solution membership:

```text
Project count = 0
```

## Step 19 — Inspect Test Inventory

Confirm:

```text
Expected test project count = actual test project count
Unexpected test projects = 0
Template sample tests = 0
Substantive tests = 0
```

## Step 20 — Inspect Final Git State

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
```

Distinguish:

- Pre-existing changes.
- WP08-owned changes.
- Generated/ignored build outputs.
- Unexpected changes.

Nothing staged.

## Step 21 — Final Scope Validation

Confirm:

```text
Authorized test projects created
Correct production references
Minimum test packages only
Central package management preserved
All test projects build
Test discovery/execution succeeds
No substantive tests
Production graph unchanged
Production projects still build
Solution membership preserved unless explicitly authorized
No production source changes
No docs/eng/CI/Docker changes
No staged changes
```

---

# 11. Architecture Test Project Boundary

`AIQuantTradingResearch.Architecture.Tests` is created in WP08 as a project boundary only.

Do not yet implement rules such as:

```text
Domain must not depend on Application
Application must not depend on Infrastructure
Infrastructure must not depend on Worker
Layer naming rules
Namespace rules
Dependency cycle rules
```

Do not add an architecture-testing package unless the Release 0.8 manifest explicitly assigns that dependency to WP08.

Architecture rule implementation belongs to the later architecture-validation work package.

WP08 only prepares the project that will host those rules.

---

# 12. Failure and Ambiguity Handling

## Test Framework Ambiguity

If repository authority does not identify the test framework:

```text
Stop
→ Record sources inspected
→ Explain ambiguity
→ Do not create projects
→ BLOCKED
```

## Package Version Ambiguity

If required package versions cannot be confidently resolved:

- Do not choose arbitrary latest versions.
- Do not change the SDK.
- Do not disable Central Package Management.
- Return `BLOCKED`.

## Template Produces Extra Assets

If the SDK template creates files outside the approved manifest:

- Remove template-only sample tests when safe.
- Do not adopt unrelated assets.
- Record what was removed.

## dotnet test Reports Zero Tests

Zero tests is not a WP08 failure when:

```text
test project builds
test host executes successfully
no substantive tests are authorized yet
```

Do not add placeholder tests solely to change the count.

## Production Regression

If creating test projects causes a production build or dependency regression:

- Capture evidence.
- Fix only WP08-owned dependency/configuration mistakes.
- Do not modify production behavior.
- Return `BLOCKED` if resolution requires broader scope.

---

# 13. Validation and Acceptance

WP08 is accepted only when:

- [ ] Prompt, Release plan, manifest, testing strategy, and dependency guidance were reviewed.
- [ ] Initial Git state was recorded.
- [ ] WP07 baseline was verified.
- [ ] Production dependency graph was verified before mutation.
- [ ] Root solution state was recorded.
- [ ] Test framework was resolved from repository authority.
- [ ] Test SDK/runner packages were resolved deliberately.
- [ ] Test project manifest was resolved.
- [ ] Only authorized test projects were created.
- [ ] Domain.Tests references Domain as required.
- [ ] Application.Tests references Application as required.
- [ ] Infrastructure.Tests references Infrastructure as required.
- [ ] Architecture.Tests references only authoritative production targets.
- [ ] No test-to-test references were added unless explicitly required.
- [ ] Central Package Management remains valid.
- [ ] Only minimum required test packages were introduced.
- [ ] No unrelated package version was changed.
- [ ] Template-generated sample tests were removed unless explicitly required.
- [ ] No substantive unit tests were implemented.
- [ ] No architecture rules/tests were implemented.
- [ ] No integration/end-to-end tests were implemented.
- [ ] All test projects restore successfully.
- [ ] All test projects build successfully.
- [ ] Test discovery/execution completes successfully.
- [ ] Zero discovered tests, if applicable, is explicitly recorded rather than hidden.
- [ ] All production projects remain buildable.
- [ ] Production project-reference graph remains unchanged.
- [ ] Dependency cycles remain zero.
- [ ] Root solution membership remains unchanged unless explicitly authorized.
- [ ] No production source behavior was modified.
- [ ] No root build policy was modified.
- [ ] No documentation, engineering script, CI, or Docker asset was modified.
- [ ] Nothing was staged, committed, or pushed.
- [ ] Final Git state and exact diff were inspected.
- [ ] Validation evidence and final decision were recorded.

Any failed mandatory criterion must affect the final decision.

---

# 14. Expected Output Contract

Return one complete **Test Projects Execution Report** in the Codex response.

Do not create a report file unless separately authorized.

Use this structure.

# Test Projects Execution Report

## 1. Executive Summary

State:

- What WP08 authorized.
- Test framework selected.
- Test projects created.
- Package/reference decisions.
- Build/test validation result.
- Final decision.

## 2. Execution Context

```text
Repository:
Branch:
Starting Commit:
Initial Working Tree:
Configured SDK:
Effective SDK:
```

## 3. Authoritative Sources Reviewed

List exact paths materially used.

## 4. WP07 Baseline Verification

```text
Production projects:
Production graph:
Production build state:
Worker composition:
Existing test projects:
Root solution project count:
Material pre-existing changes:
```

## 5. Test Framework Decision

```text
Framework:
Microsoft.NET.Test.Sdk:
Runner/adapter:
Coverage package:
Assertion library:
Authority:
```

## 6. Test Project Manifest

| Test Project | Created | Production References | Purpose |
| --- | --- | --- | --- |
| Domain.Tests | YES/NO | ... | ... |
| Application.Tests | YES/NO | ... | ... |
| Infrastructure.Tests | YES/NO | ... | ... |
| Architecture.Tests | YES/NO | ... | ... |

## 7. Package Decision

| Package | Version | Projects | Reason | Existing/New |
| --- | --- | --- | --- | --- |

## 8. Changes Applied

| File/Path | Change | Reason | Authority |
| --- | --- | --- | --- |

## 9. Test Project Configuration

| Project | Effective TFM | IsTestProject | IsPackable | Package Count | Project References |
| --- | --- | --- | --- | ---: | --- |

## 10. Template Cleanup

```text
Sample test files generated:
Sample test files removed:
Unexpected template assets:
Substantive tests remaining:
```

## 11. Restore Validation

| Project | Command | Exit Status | Assessment |
| --- | --- | ---: | --- |

## 12. Build Validation

| Project | Exit Status | Warnings | Errors | Assessment |
| --- | ---: | ---: | ---: | --- |

## 13. Test Discovery / Execution

| Project | Exit Status | Discovered | Executed | Passed | Failed | Assessment |
| --- | ---: | ---: | ---: | ---: | ---: | --- |

If the framework does not expose all counts for an empty project, record the available evidence accurately.

## 14. Production Regression Validation

| Project | Exit Status | Warnings | Errors | Assessment |
| --- | ---: | ---: | ---: | --- |

## 15. Dependency and Solution Preservation

```text
Production graph matches WP04:
Production cycles:
Production ProjectReferences changed:
Root solution project count:
Solution membership changed:
```

## 16. Validation Evidence

| Command | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 17. Scope Compliance

| Scope Check | Result | Evidence |
| --- | --- | --- |
| Only authorized test projects created | PASS/FAIL | ... |
| Minimum test dependencies only | PASS/FAIL | ... |
| Correct production references | PASS/FAIL | ... |
| No substantive tests | PASS/FAIL | ... |
| No architecture rules implemented | PASS/FAIL | ... |
| No integration/E2E tests | PASS/FAIL | ... |
| Production graph unchanged | PASS/FAIL | ... |
| Production builds preserved | PASS/FAIL | ... |
| Solution membership preserved | PASS/FAIL | ... |
| No production source behavior changed | PASS/FAIL | ... |
| No docs/eng/CI/Docker changes | PASS/FAIL | ... |
| No staging/commit/push | PASS/FAIL | ... |

## 18. Final Git State

Report:

```text
git status --short
```

Distinguish:

- Pre-existing changes.
- WP08-owned changes.
- Ignored/generated build outputs.
- Unexpected changes.

## 19. Findings

When necessary:

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 20. Acceptance Criteria

Reproduce applicable WP08 criteria with PASS/FAIL.

## 21. Final Decision

State exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

Use `COMPLETE` when all authorized test projects are valid and no unresolved WP08-specific action remains.

Use `COMPLETE WITH ACTIONS` only when the skeleton is valid but a non-blocking finding clearly belongs to a later work package.

Use `BLOCKED` when the test-project skeleton cannot be established safely within WP08 scope.

## 22. Next Action

If complete, identify the next work package from:

```text
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
```

Do not infer or rename the next work package if the authoritative plan defines it.

Do not begin the next work package.

---

# 15. Prohibited Behaviors

Do not:

- Add placeholder tests merely to make test counts non-zero.
- Implement real unit tests.
- Implement architecture tests.
- Implement integration tests.
- Implement end-to-end tests.
- Add mocks/fakes/builders/fixtures.
- Add architecture-testing libraries without explicit WP08 authority.
- Add assertion libraries without explicit need.
- Add coverage tooling without explicit WP08 authority.
- Modify production code.
- Modify production project references.
- Add projects to the root solution unless explicitly authorized.
- Modify root build policy.
- Modify `global.json`.
- Modify `.editorconfig`.
- Modify documentation.
- Modify engineering scripts.
- Modify CI.
- Modify Docker.
- Reformat unrelated files.
- Stage.
- Commit.
- Push.
- Open a pull request.
- Begin the next work package.

---

# 16. Completion Model

```text
Inspect
   ↓
Verify WP07 Baseline
   ↓
Resolve Test Framework
   ↓
Resolve Test Project Manifest
   ↓
Resolve Minimum Test Packages
   ↓
Create Test Projects
   ↓
Remove Template Sample Tests
   ↓
Add Authorized Production References
   ↓
Restore
   ↓
Build
   ↓
Run Empty Test Discovery/Execution
   ↓
Revalidate Production Builds + Graph
   ↓
Preserve Solution State
   ↓
Inspect Git Diff
   ↓
Report Evidence
   ↓
COMPLETE | COMPLETE WITH ACTIONS | BLOCKED
```

---

# 17. Final Instruction

Execute **Phase 2 — Release 0.8 / Work Package 08 — Test Projects** against the actual current `AIQuantTradingResearch` repository.

Read all authoritative sources before mutation.

Resolve the test framework and exact test-project manifest from repository authority rather than generic preference.

Create only the authorized test projects.

Use the minimum test SDK/framework dependencies required for valid .NET test projects.

Respect Central Package Management.

Remove meaningless template-generated sample tests unless explicitly required.

Add only the production project references authorized for each test boundary.

Do not implement substantive unit tests, architecture rules, integration tests, or end-to-end tests.

Do not modify production behavior or the accepted production dependency graph.

Do not change root solution membership unless WP08 authority explicitly requires it.

Restore, build, and execute/discover each test project.

A successful empty test project is valid WP08 evidence; do not add fake tests merely to create passing-test counts.

Revalidate all production projects and the production dependency graph.

Inspect final Git state and prove scope preservation.

Return the complete **Test Projects Execution Report**.

Finish with exactly one evidence-based decision:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

If complete, identify the next work package exactly as defined by the Release 0.8 execution plan, but do not begin it.

---

# Conclusion

Work Package 08 establishes the test architecture before introducing test behavior.

The intended transition is:

```text
Production Skeleton
        ↓
Test Framework Decision
        ↓
Domain Test Boundary
        ↓
Application Test Boundary
        ↓
Infrastructure Test Boundary
        ↓
Architecture Test Boundary
        ↓
Restore + Build + Discovery Validation
        ↓
No Substantive Tests Yet
        ↓
Controlled Handoff
```

This separation matters because creating a test project and designing a test suite are different engineering decisions.

WP08 gives each testing concern a stable project boundary while keeping later work packages responsible for executable architecture rules and meaningful behavioral tests.

The resulting structure should make future testing deliberate rather than incidental:

```text
tests/
├── AIQuantTradingResearch.Domain.Tests/
├── AIQuantTradingResearch.Application.Tests/
├── AIQuantTradingResearch.Infrastructure.Tests/
└── AIQuantTradingResearch.Architecture.Tests/
```

The central principle is:

> **Build the test boundaries before filling them with tests: establish ownership, dependencies, tooling, and validation first, then introduce executable rules and behavior only when their work package explicitly owns them.**
