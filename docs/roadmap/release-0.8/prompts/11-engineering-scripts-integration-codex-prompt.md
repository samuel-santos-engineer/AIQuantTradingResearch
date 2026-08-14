# Codex Execution Prompt — Release 0.8 / 11 Engineering Scripts Integration

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.8 — Solution Skeleton |
| Work Package | 11 — Engineering Scripts Integration |
| Issue Type | Engineering / Automation |
| Execution Mode | Controlled repository modification |
| Primary Agent | Codex |
| Prerequisite | 10 — Solution Organization accepted as `COMPLETE` |
| Primary Area | `eng/` |
| Expected Outcome | Existing engineering scripts correctly target the Release 0.8 `.slnx`, execute consistently from repository root, and provide reliable restore/build/test/format/verify orchestration without CI implementation |

---

## Purpose

Integrate the repository's existing engineering scripts with the completed Release 0.8 solution skeleton.

WP01–WP10 established the repository baseline, root `.slnx`, production projects, production dependency graph, centralized build configuration, minimal Worker host, dependency-registration boundaries, test projects, executable architecture tests, and final solution organization.

WP11 now ensures the existing `eng/` automation can operate against that real solution.

The goal is not to redesign the engineering-tooling system.

The goal is to connect the scripts already present in the repository to the authoritative Release 0.8 solution and prove that they provide a coherent local engineering workflow.

---

## Objective

Inspect and integrate the existing engineering scripts so that the repository supports a predictable command flow equivalent to:

```text
restore
   ↓
build
   ↓
test
   ↓
format
   ↓
verify
```

The exact script names and orchestration must be derived from the repository.

Likely existing files may include:

```text
eng/restore.ps1
eng/build.ps1
eng/test.ps1
eng/format.ps1
eng/verify.ps1
eng/clean.ps1
eng/build.sh
```

Do not assume every listed file exists.

Use actual repository evidence.

At completion:

- scripts must target `AIQuantTradingResearch.slnx` correctly;
- no script may reference the obsolete Api/SharedKernel skeleton;
- scripts should work when invoked from the repository root;
- scripts should fail clearly on command failures;
- `verify` should orchestrate only the validation steps explicitly owned by existing repository design;
- script behavior should preserve the production graph and solution structure;
- no CI workflow should be created.

---

# 1. Authority and Preconditions

Before modifying anything, read completely:

```text
docs/roadmap/release-0.8/prompts/11-engineering-scripts-integration-codex-prompt.md
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Read engineering automation guidance:

```text
docs/handbook/ENGINEERING.md
docs/handbook/ENGINEERING_PLAYBOOK.md
docs/architecture/implementation/IMPLEMENTATION_GUIDELINES.md
docs/architecture/implementation/TESTING_STRATEGY.md
```

Read all existing files under:

```text
eng/
```

Inspect:

```text
AIQuantTradingResearch.slnx
Directory.Build.props
Directory.Packages.props
global.json
.editorconfig
```

Consult materially relevant Toolkit guidance:

```text
AI-Engineering-Toolkit/docs/AI_ASSISTED_ENGINEERING_WORKFLOW.md
AI-Engineering-Toolkit/playbooks/powershell/README.md
AI-Engineering-Toolkit/playbooks/powershell/01-script-architecture.md
AI-Engineering-Toolkit/playbooks/powershell/02-script-structure.md
AI-Engineering-Toolkit/playbooks/powershell/04-error-handling.md
AI-Engineering-Toolkit/playbooks/powershell/05-logging.md
AI-Engineering-Toolkit/playbooks/powershell/06-validation.md
AI-Engineering-Toolkit/playbooks/powershell/07-testing.md
AI-Engineering-Toolkit/playbooks/powershell/10-script-review.md
AI-Engineering-Toolkit/playbooks/dotnet/14-build-and-ci.md
```

If an exact listed file does not exist, do not create or rename documentation during WP11. Record the absence and continue using available authority unless it blocks a mandatory integration decision.

Repository-specific Release 0.8 guidance takes precedence over generic Toolkit guidance.

---

# 2. Accepted Baseline from WP10

Expected root solution:

```text
AIQuantTradingResearch.slnx
```

Expected solution inventory:

```text
8 projects total

/src/
  4 production projects

/tests/
  4 test projects
```

Expected production graph:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

Expected test state:

```text
Domain.Tests           = valid empty test project
Application.Tests      = valid empty test project
Infrastructure.Tests   = valid empty test project
Architecture.Tests     = 7 passing tests
```

Expected solution-level validation:

```text
restore = PASS
build   = PASS
test    = PASS
```

Expected known environmental condition:

```text
NU1900 vulnerability-feed connectivity warning may occur
```

Verify the current repository state before mutation.

Do not silently repair earlier work-package defects.

---

# 3. Scope

## In Scope

You may:

- Inspect repository and Git state.
- Inspect every existing `eng/` script.
- Determine script responsibilities and current orchestration.
- Replace obsolete solution/project paths with the authoritative `.slnx`.
- Normalize repository-root path resolution where needed.
- Fix script parameter/default handling when required for Release 0.8 integration.
- Fix script error propagation and exit-code handling when broken.
- Ensure restore/build/test/format/verify scripts invoke the correct .NET commands.
- Ensure `verify` delegates to existing responsibility-specific scripts rather than duplicating logic when repository conventions support that model.
- Adjust PowerShell and shell counterparts consistently when both are authoritative.
- Validate scripts by executing them.
- Revalidate solution restore/build/test and architecture tests through the scripts.
- Inspect final Git state.
- Produce an evidence-based execution report.

## Out of Scope

Do not:

- Redesign the entire `eng/` architecture.
- Create CI/GitHub Actions workflows.
- Add package dependencies.
- Modify production code.
- Modify test code.
- Modify production/test project references.
- Modify solution membership.
- Modify `Directory.Build.props`.
- Modify `Directory.Packages.props`.
- Modify `global.json`.
- Modify `.editorconfig`.
- Add coverage tooling unless already explicitly part of existing `eng/` contract.
- Add security scanners.
- Add deployment scripts.
- Add release scripts.
- Add Docker orchestration.
- Add benchmark orchestration.
- Modify documentation.
- Stage, commit, push, or open a pull request.
- Begin WP12.

---

# 4. Authorized Change Set

Primary authorized area:

```text
eng/**
```

No changes outside `eng/` are expected.

If script execution generates ignored build outputs under `bin/` or `obj/`, those are not repository changes.

Do not modify root configuration, project files, source files, test files, solution content, or documentation.

If solving an integration failure appears to require changing something outside `eng/`, stop and determine whether the problem belongs to another work package.

---

# 5. Script Inventory Contract

Before mutation, enumerate every file in `eng/`.

For each script record:

```text
Path
Platform
Responsibility
Inputs
Default target
Referenced solution/project path
Delegates to
Exit behavior
Obsolete references
```

Classify each script as:

```text
KEEP
MODIFY
DEFER
OBSOLETE
UNKNOWN
```

Do not delete a script merely because it is unused.

Deletion requires explicit repository authority.

---

# 6. Responsibility Boundaries

## Restore

Responsible only for dependency restore.

Expected target:

```text
AIQuantTradingResearch.slnx
```

Typical behavior:

```text
dotnet restore AIQuantTradingResearch.slnx
```

## Build

Responsible only for compilation.

Prefer:

```text
dotnet build AIQuantTradingResearch.slnx --no-restore
```

when restore is intentionally handled separately.

## Test

Responsible for test execution.

Expected target:

```text
AIQuantTradingResearch.slnx
```

or explicit test-project execution when repository design requires it.

The script must execute Architecture.Tests.

## Format

Responsible for repository-supported .NET formatting.

Resolve the existing intended command from script/repository authority.

Likely form:

```text
dotnet format AIQuantTradingResearch.slnx
```

or equivalent check mode.

Do not silently change formatting policy.

## Verify

Responsible for orchestrating approved local quality gates.

Prefer delegation to responsibility-specific scripts when repository conventions support that model.

Do not add CI-only or future-release checks.

## Clean

If present, clean only generated build artifacts according to existing repository conventions.

Do not delete repository source or user files.

---

# 7. Path Resolution Contract

Scripts must work reliably from repository-root invocation.

Avoid dependence on the caller's arbitrary working directory.

Where appropriate, resolve the repository root from the script location.

PowerShell concept:

```text
$RepositoryRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
```

Shell equivalent should resolve its own directory robustly.

Do not hardcode machine-specific paths.

The authoritative solution path is repository-relative:

```text
AIQuantTradingResearch.slnx
```

---

# 8. Obsolete Reference Removal

Search `eng/` for obsolete references including:

```text
AIQuantTradingResearch.Api
AIQuantTradingResearch.SharedKernel
old .sln paths
old solution filenames
obsolete src/test project paths
```

Required after WP11:

```text
Obsolete Release 0.8 predecessor references = 0
```

Do not search/replace blindly.

Interpret each match in context.

---

# 9. Error Handling Contract

Scripts must propagate command failure.

A failed restore/build/test/format step must produce a non-zero script exit.

Do not swallow failures.

Do not continue to later verification stages after a mandatory earlier stage fails unless repository authority explicitly defines aggregate behavior.

Use robust native-command exit handling consistent with repository conventions.

Do not add elaborate logging frameworks.

---

# 10. Output and Logging Contract

Script output should be concise, understandable, stage-oriented, free of secrets, and useful in local development and future CI.

Preserve existing logging conventions.

Do not suppress meaningful .NET errors/warnings.

Known environmental `NU1900` warnings should remain visible.

Do not disable vulnerability auditing merely to silence them.

---

# 11. Cross-Platform Contract

If both PowerShell and shell entry points exist and are authoritative:

- preserve equivalent responsibilities;
- preserve equivalent solution targets;
- preserve equivalent exit behavior;
- avoid platform-specific divergence without justification.

Do not create missing shell equivalents merely for symmetry unless Release 0.8 explicitly requires them.

Do not rewrite working shell scripts into PowerShell or vice versa.

---

# 12. Execution Procedure

## Step 1 — Read Authority

Read this prompt, Release plan, file manifest, engineering guidance, all `eng/` scripts, and applicable Toolkit guidance completely.

## Step 2 — Record Initial Git State

Run:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Record all pre-existing changes.

Do not clean, reset, restore, stage, or delete user work.

## Step 3 — Verify WP10 Baseline

Confirm:

```text
AIQuantTradingResearch.slnx exists
Solution project count = 8
4 production projects
4 test projects
Architecture.Tests = 7 passing tests
Production graph matches WP04
```

Do not modify this state.

## Step 4 — Inventory eng/

Enumerate every `eng/` file.

Create the script inventory described in Section 5.

## Step 5 — Search for Obsolete References

Search all `eng/` content for:

```text
Api
SharedKernel
.sln
.slnx
src/
tests/
```

Identify only material outdated references.

## Step 6 — Resolve Engineering Script Contract

From repository authority, determine:

```text
Restore script:
Build script:
Test script:
Format script:
Verify script:
Clean script:
Cross-platform counterparts:
```

## Step 7 — Update Solution Targets

Modify scripts that still target old/nonexistent solution or project paths.

Target:

```text
AIQuantTradingResearch.slnx
```

unless a responsibility-specific script intentionally targets a narrower artifact.

## Step 8 — Normalize Root Resolution

Fix scripts that depend incorrectly on caller working directory.

Prefer repository-root resolution based on script location.

Keep changes minimal.

## Step 9 — Validate Restore Script

Execute the authoritative restore script.

Required:

```text
Exit Status = 0
```

## Step 10 — Validate Build Script

Execute the authoritative build script.

Required:

```text
Exit Status = 0
Errors = 0
```

## Step 11 — Validate Test Script

Execute the authoritative test script.

Required:

```text
Exit Status = 0
Architecture.Tests executed
Architecture failures = 0
```

Empty unit-test skeleton projects remain acceptable.

## Step 12 — Validate Format Script

Determine whether the script formats or checks formatting according to repository contract.

Execute it.

If it mutates files by design, capture the changes and avoid broad unrelated formatting changes outside WP11.

Prefer check mode when repository authority supports it.

## Step 13 — Validate Verify Script

Execute the authoritative verify script.

Required:

```text
Exit Status = 0
```

when all mandatory local gates pass.

Record which substeps it actually executed.

## Step 14 — Validate Failure Propagation

Safely verify that at least one script correctly propagates a failing native command.

Preferred non-destructive methods include a script-supported invalid target or temporary/throwaway invocation.

Do not corrupt repository files to force failure.

If safe negative validation is not possible, explain why.

## Step 15 — Validate Clean Script if Present

If `clean` is part of Release 0.8 integration:

- execute it safely,
- confirm only generated build outputs are removed,
- rerun restore/build/test as necessary afterward.

Do not delete repository content.

If clean is not part of WP11 authority, inspect only.

## Step 16 — Revalidate Solution Directly

Run:

```text
dotnet sln AIQuantTradingResearch.slnx list
```

Expected:

```text
8 projects
```

## Step 17 — Revalidate Production Graph

Required:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure

Cycles = 0
Production ProjectReference changes = 0
```

## Step 18 — Revalidate Architecture Tests

Required:

```text
Discovered = 7
Passed = 7
Failed = 0
```

unless accepted baseline changed before WP11.

## Step 19 — Inspect Final Git State

Run:

```text
git status --short
git diff -- eng
git diff -- AIQuantTradingResearch.slnx src tests Directory.Build.props Directory.Packages.props global.json
git diff --cached -- .
```

Expected WP11-owned changes:

```text
eng/**
```

only.

Nothing staged.

## Step 20 — Final Scope Validation

Confirm:

```text
Engineering scripts target authoritative solution
Obsolete references removed
Root resolution reliable
Restore script passes
Build script passes
Test script passes
Format script behavior validated
Verify script passes
Failure propagation validated or justified
Architecture tests preserved
Production graph unchanged
Solution membership unchanged
No project/source/package/config changes
No docs/CI/Docker changes
No staged changes
```

---

# 13. Script Integration Invariants

At completion:

## Correct Target

Scripts reference:

```text
AIQuantTradingResearch.slnx
```

where solution-level targeting is appropriate.

## No Obsolete Skeleton

No engineering script references:

```text
AIQuantTradingResearch.Api
AIQuantTradingResearch.SharedKernel
```

## Root-Safe

Scripts work from repository-root invocation without machine-specific paths.

## Failure-Aware

Mandatory .NET command failures propagate as non-zero script failure.

## Responsibility-Focused

Restore/build/test/format/verify responsibilities remain distinct.

## Architecture-Preserving

Scripts do not modify dependency structure, solution membership, or project definitions.

---

# 14. Failure and Ambiguity Handling

## Script Missing

If the Release plan expects a responsibility-specific script that does not exist:

- confirm whether creation is authorized by WP11;
- if explicitly required, create the smallest script consistent with existing `eng/` architecture;
- if authority is unclear, return `BLOCKED` or `COMPLETE WITH ACTIONS` depending on whether mandatory acceptance is prevented.

Do not invent a broad new script framework.

## Format Produces Broad Changes

Do not keep repository-wide formatting mutations merely to make `format` pass.

Determine whether the script should operate in check mode or whether formatting debt belongs elsewhere.

## Verify Duplicates Logic

If `verify` duplicates restore/build/test implementation and repository standards require delegation, refactor minimally.

Do not over-engineer orchestration.

## NU1900

Known NuGet vulnerability metadata connectivity warnings are non-blocking when commands exit successfully.

Do not disable auditing.

## Cross-Platform Drift

If PowerShell and shell scripts differ materially, align only responsibilities explicitly covered by WP11.

If one platform cannot be tested in the current environment, report validation coverage accurately.

---

# 15. Validation and Acceptance

WP11 is accepted only when:

- [ ] Prompt, Release plan, manifest, engineering guidance, and all `eng/` scripts were reviewed.
- [ ] Initial Git state was recorded.
- [ ] WP10 solution baseline was verified.
- [ ] Complete `eng/` inventory was produced.
- [ ] Script responsibilities were classified.
- [ ] Obsolete Api/SharedKernel/old-solution references were identified.
- [ ] All mandatory solution-level script targets point to `AIQuantTradingResearch.slnx`.
- [ ] No machine-specific repository path remains in mandatory scripts.
- [ ] Restore script succeeds.
- [ ] Build script succeeds.
- [ ] Test script succeeds.
- [ ] Architecture.Tests executes successfully through test automation or is explicitly verified alongside it.
- [ ] Format script behavior is validated.
- [ ] Verify script succeeds.
- [ ] Verify script performs only approved local quality gates.
- [ ] Native-command failures propagate correctly or safe negative validation is explicitly justified.
- [ ] Clean script behavior is validated if in scope.
- [ ] Cross-platform counterparts are aligned when applicable.
- [ ] Production project graph remains unchanged.
- [ ] Production graph remains acyclic.
- [ ] Solution membership remains exactly 8 projects.
- [ ] No production/test project file was modified.
- [ ] No production/test source behavior was modified.
- [ ] No package or root configuration was modified.
- [ ] No documentation, CI, or Docker asset was modified.
- [ ] Nothing was staged, committed, or pushed.
- [ ] Final Git state and exact diff were inspected.
- [ ] Validation evidence and final decision were recorded.

Any failed mandatory criterion must affect the final decision.

---

# 16. Expected Output Contract

Return one complete **Engineering Scripts Integration Execution Report** in the Codex response.

Do not create a report file unless separately authorized.

Use this structure.

# Engineering Scripts Integration Execution Report

## 1. Executive Summary

State what WP11 authorized, scripts inspected, scripts modified, workflow result, and final decision.

## 2. Execution Context

```text
Repository:
Branch:
Starting Commit:
Initial Working Tree:
Configured SDK:
Effective SDK:
Shells Available:
```

## 3. Authoritative Sources Reviewed

List exact paths materially used.

## 4. WP10 Baseline Verification

```text
Solution project count:
Production projects:
Test projects:
Architecture test result:
Production graph:
Material pre-existing changes:
```

## 5. Engineering Script Inventory

| Path | Platform | Responsibility | Initial Target | Delegates To | Classification |
| --- | --- | --- | --- | --- | --- |

## 6. Obsolete Reference Assessment

```text
Api references:
SharedKernel references:
Old .sln references:
Old solution paths:
Other obsolete paths:
```

## 7. Integration Decisions

| Script | Problem | Decision | Reason | Authority |
| --- | --- | --- | --- | --- |

## 8. Changes Applied

| File | Change | Reason | Authority |
| --- | --- | --- | --- |

## 9. Restore Validation

```text
Script:
Command:
Exit Status:
Warnings:
Errors:
Assessment:
```

## 10. Build Validation

```text
Script:
Command:
Exit Status:
Warnings:
Errors:
Assessment:
```

## 11. Test Validation

```text
Script:
Command:
Exit Status:
Architecture tests discovered:
Architecture tests passed:
Architecture tests failed:
Assessment:
```

## 12. Format Validation

```text
Script:
Mode:
Exit Status:
Files changed:
Assessment:
```

## 13. Verify Validation

```text
Script:
Substeps:
Exit Status:
Assessment:
```

## 14. Failure Propagation Validation

```text
Method:
Script:
Expected non-zero observed:
Repository preserved:
Assessment:
```

If not performed, explain why.

## 15. Clean Validation

If applicable:

```text
Script:
Exit Status:
Artifacts removed:
Repository files removed:
Assessment:
```

Otherwise:

```text
Not in mandatory WP11 validation scope.
```

## 16. Cross-Platform Assessment

```text
PowerShell coverage:
Shell coverage:
Behavior parity:
Unvalidated platform:
Reason:
```

## 17. Architecture and Solution Preservation

```text
Solution project count:
Production graph:
Cycles:
Production ProjectReferences changed:
Architecture tests:
```

## 18. Validation Evidence

| Command | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 19. Scope Compliance

| Scope Check | Result | Evidence |
| --- | --- | --- |
| Scripts target authoritative solution | PASS/FAIL | ... |
| Obsolete references removed | PASS/FAIL | ... |
| Root/path resolution reliable | PASS/FAIL | ... |
| Restore works | PASS/FAIL | ... |
| Build works | PASS/FAIL | ... |
| Test works | PASS/FAIL | ... |
| Format behavior validated | PASS/FAIL | ... |
| Verify works | PASS/FAIL | ... |
| Failure propagation valid | PASS/FAIL | ... |
| Production graph unchanged | PASS/FAIL | ... |
| Solution membership unchanged | PASS/FAIL | ... |
| No project/source/package/config changes | PASS/FAIL | ... |
| No docs/CI/Docker changes | PASS/FAIL | ... |
| No staging/commit/push | PASS/FAIL | ... |

## 20. Final Git State

Report `git status --short` and distinguish pre-existing, WP11-owned, generated/ignored, and unexpected changes.

## 21. Findings

When necessary:

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed classifications:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 22. Acceptance Criteria

Reproduce applicable WP11 criteria with PASS/FAIL.

## 23. Final Decision

State exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

Use `COMPLETE` when mandatory engineering scripts are integrated and validated with no unresolved WP11-specific action.

Use `COMPLETE WITH ACTIONS` only when the core workflow is valid but a non-blocking later-owned or platform-specific finding remains.

Use `BLOCKED` when the engineering workflow cannot be established safely within WP11 scope.

## 24. Next Action

If complete, identify the next work package exactly as defined by:

```text
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
```

Do not infer, rename, or begin it.

---

# 17. Prohibited Behaviors

Do not:

- Create CI workflows.
- Modify GitHub Actions.
- Redesign the entire `eng/` architecture.
- Add unrelated scripts.
- Modify production/test code.
- Modify project references.
- Modify solution membership.
- Modify package versions.
- Modify root build policy.
- Modify SDK configuration.
- Add coverage/security/deployment/release tooling unless explicitly required.
- Disable NuGet auditing.
- Reformat the repository broadly.
- Modify documentation.
- Modify Docker.
- Stage.
- Commit.
- Push.
- Open a pull request.
- Begin the next work package.

---

# 18. Completion Model

```text
Inspect
   ↓
Verify WP10 Baseline
   ↓
Inventory eng/
   ↓
Resolve Script Responsibilities
   ↓
Remove Obsolete Targets
   ↓
Normalize Root/Solution Resolution
   ↓
Validate Restore
   ↓
Validate Build
   ↓
Validate Test
   ↓
Validate Format
   ↓
Validate Verify
   ↓
Validate Failure Propagation
   ↓
Revalidate Architecture + Solution
   ↓
Inspect Git Diff
   ↓
Report Evidence
   ↓
COMPLETE | COMPLETE WITH ACTIONS | BLOCKED
```

---

# 19. Final Instruction

Execute **Phase 2 — Release 0.8 / Work Package 11 — Engineering Scripts Integration** against the actual current `AIQuantTradingResearch` repository.

Read all authoritative sources and every existing `eng/` script before mutation.

Verify the accepted WP10 solution baseline.

Inventory and classify all engineering scripts.

Integrate the existing scripts with:

```text
AIQuantTradingResearch.slnx
```

where solution-level targeting is appropriate.

Remove obsolete Api/SharedKernel or old-solution references from mandatory engineering workflows.

Ensure scripts resolve repository paths reliably and do not depend on machine-specific locations.

Preserve distinct restore/build/test/format/verify responsibilities.

Validate every mandatory script by executing it.

Confirm command failures propagate correctly.

Do not create CI.

Do not modify production/test code, project references, solution membership, packages, or root configuration.

Revalidate:

```text
8 solution projects
7 passing architecture tests
production graph unchanged
cycles = 0
```

Inspect final Git state and prove WP11 changes are restricted to `eng/`.

Return the complete **Engineering Scripts Integration Execution Report**.

Finish with exactly one evidence-based decision:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

If complete, identify the next work package exactly as defined by the Release 0.8 execution plan.

Do not begin it.

---

# Conclusion

Work Package 11 connects the completed solution skeleton to the repository's engineering workflow.

```text
Validated Solution Skeleton
        ↓
Inspect Existing Engineering Scripts
        ↓
Remove Obsolete Targets
        ↓
Bind Scripts to Authoritative .slnx
        ↓
Restore
        ↓
Build
        ↓
Test
        ↓
Format
        ↓
Verify
        ↓
Architecture + Solution Revalidation
        ↓
Controlled Handoff
```

The objective is not to create a sophisticated automation platform.

It is to ensure that the repository has a small, predictable, trustworthy set of engineering entry points that humans, Codex, and future CI can call consistently.

The scripts should remain thin orchestration layers over authoritative .NET tooling.

> **Engineering automation should make the correct workflow easy and repeatable: keep scripts thin, targets authoritative, failure behavior explicit, and leave CI orchestration to the work package that owns CI.**
