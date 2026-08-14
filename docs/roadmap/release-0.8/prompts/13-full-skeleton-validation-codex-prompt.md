# Codex Execution Prompt — Release 0.8 / 13 Full Skeleton Validation

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.8 — Solution Skeleton |
| Work Package | 13 — Full Skeleton Validation |
| Execution Mode | Validation-first / no implementation changes by default |
| Primary Agent | Codex |
| Prerequisite | 12 — Documentation Alignment accepted as `COMPLETE` |
| Primary Area | Entire Release 0.8 skeleton |
| Expected Outcome | Prove from a clean repository state that the complete Release 0.8 skeleton restores, formats, builds, tests, satisfies architecture constraints, matches the authoritative manifest/documentation, and is ready for Release 0.8 completion without beginning later-release work |

---

## Purpose

Perform the final integrated validation of the complete Release 0.8 Solution Skeleton.

WP01–WP12 incrementally established and aligned:

- repository prerequisites,
- root `.slnx`,
- production projects,
- production dependency graph,
- centralized build configuration,
- minimal Worker host,
- dependency registration boundaries,
- test projects,
- executable architecture tests,
- solution organization,
- engineering scripts,
- and repository documentation.

WP13 does not add another architectural capability.

WP13 proves that all Release 0.8 work functions together as one coherent, reproducible skeleton.

This work package is validation-first.

The default authorized repository change set is:

```text
NONE
```

If a mandatory Release 0.8 acceptance criterion fails, do not silently repair it unless the Release 0.8 execution plan explicitly authorizes WP13 to perform that exact correction.

Report the failure and preserve evidence.

---

## Objective

Validate the complete Release 0.8 repository from an appropriately clean generated-output state and establish objective evidence that:

```text
repository baseline
solution structure
project inventory
dependency graph
build configuration
Worker composition root
dependency registration
test skeleton
architecture tests
solution organization
engineering scripts
documentation alignment
```

are mutually consistent and operational.

The final validation should prove the repository can execute the intended engineering flow:

```text
clean
   ↓
restore
   ↓
format verification
   ↓
build
   ↓
test
   ↓
verify
```

without introducing implementation changes.

At completion, Codex must provide a complete **Full Skeleton Validation Execution Report** and exactly one evidence-based decision:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

---

# 1. Authority and Preconditions

Before taking any validation action, read completely:

```text
docs/roadmap/release-0.8/prompts/13-full-skeleton-validation-codex-prompt.md
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Inspect current implementation:

```text
AIQuantTradingResearch.slnx
Directory.Build.props
Directory.Packages.props
global.json
.editorconfig
src/**
tests/**
eng/**
```

Review current-state documentation materially defining the skeleton, including when present:

```text
README.md
docs/handbook/ENGINEERING.md
docs/handbook/CODING_STANDARDS.md
docs/architecture/implementation/NAMING_CONVENTIONS.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/TESTING_STRATEGY.md
docs/architecture/solution/SOLUTION_STRUCTURE.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/solution/SOLUTION_ARCHITECTURE.md
```

Use the actual repository and Release 0.8 authority rather than assumptions.

---

# 2. Accepted Baseline from WP12

Expected root solution:

```text
AIQuantTradingResearch.slnx
```

Expected project count:

```text
8
```

Expected solution organization:

```text
/src/
  AIQuantTradingResearch.Domain
  AIQuantTradingResearch.Application
  AIQuantTradingResearch.Infrastructure
  AIQuantTradingResearch.Worker

/tests/
  AIQuantTradingResearch.Domain.Tests
  AIQuantTradingResearch.Application.Tests
  AIQuantTradingResearch.Infrastructure.Tests
  AIQuantTradingResearch.Architecture.Tests
```

Expected production graph:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

Expected architecture enforcement:

```text
Domain !→ Application
Domain !→ Infrastructure
Domain !→ Worker
Application !→ Infrastructure
Application !→ Worker
Infrastructure !→ Worker
Production graph is acyclic
```

Expected architecture tests:

```text
Discovered = 7
Passed = 7
Failed = 0
```

Expected engineering scripts:

```text
eng/restore.ps1
eng/build.ps1
eng/build.sh
eng/clean.ps1
eng/format.ps1
eng/test.ps1
eng/verify.ps1
```

Expected `verify` orchestration:

```text
restore
→ format verification
→ build
→ test
```

Expected SDK:

```text
10.0.103
```

Verify all of these against the actual repository.

---

# 3. Validation Philosophy

WP13 must distinguish between:

```text
proof
```

and:

```text
repair
```

The purpose of this work package is proof.

Do not make implementation changes merely because a validation command fails.

A final validation that repairs the repository while testing it does not prove that the accepted WP12 baseline was independently valid.

Therefore:

```text
observe first
preserve evidence
classify failure
avoid mutation
report accurately
```

Generated `bin/` and `obj/` outputs are expected operational artifacts and are not repository implementation changes.

---

# 4. Scope

## In Scope

You may:

- Read all Release 0.8 authority.
- Inspect repository state.
- Inspect solution/project/configuration/script/documentation artifacts.
- Execute clean, restore, format verification, build, test, and verify.
- Inspect solution membership and solution folders.
- Inspect project references.
- Inspect package references and central package configuration.
- Inspect Worker host and dependency registration.
- Execute architecture tests directly.
- Search for obsolete current-state Api/SharedKernel artifacts.
- Compare implementation against the Release 0.8 file manifest.
- Compare documentation against implemented current state.
- Inspect Git state before and after validation.
- Remove generated outputs only through the approved clean workflow.
- Produce the complete validation report.

## Out of Scope

Do not:

- Add or modify production code.
- Add or modify test code.
- Add or modify architecture tests.
- Modify project files.
- Modify `AIQuantTradingResearch.slnx`.
- Modify `eng/`.
- Modify documentation.
- Modify `Directory.Build.props`.
- Modify `Directory.Packages.props`.
- Modify `global.json`.
- Modify `.editorconfig`.
- Add packages.
- Create CI.
- Modify Docker assets.
- Implement plugins.
- Implement market data.
- Implement storage.
- Implement pipelines.
- Implement analytics.
- Implement AI/ML or MLOps.
- Perform release tagging/versioning unless explicitly owned by a later work package.
- Stage, commit, push, or open a pull request.
- Begin the next work package or release.

---

# 5. No-Change Contract

WP13 expects:

```text
Tracked repository changes introduced by WP13 = 0
Untracked source/config/documentation artifacts introduced by WP13 = 0
Staged changes = 0
Commits = 0
Pushes = 0
```

Validation may create ignored/generated outputs.

Those outputs must be removable through the repository's clean workflow.

If validation itself modifies tracked files, investigate immediately.

Examples:

- format verification must not rewrite files;
- build/test must not generate tracked source;
- clean must not delete repository assets.

Any unexpected repository mutation is a validation failure until explained.

---

# 6. Repository State Contract

Before running clean or other commands:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Record all pre-existing changes.

Expected pre-existing work may include the WP13 prompt files themselves.

Do not:

```text
git clean
git reset
git restore
git checkout -- .
```

Do not remove user work.

The approved `eng/clean.ps1` may be used only according to its validated responsibility for generated build artifacts.

---

# 7. File Manifest Validation

Compare actual Release 0.8 implementation artifacts with:

```text
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Classify each relevant manifest entry as:

```text
PRESENT
MISSING
UNEXPECTED
DEFERRED
DOCUMENTATION-ONLY
```

Do not treat future-release files as missing Release 0.8 artifacts.

Identify unexpected implementation artifacts that materially violate the Release 0.8 skeleton.

Do not delete them during WP13.

---

# 8. Solution Validation

Validate:

```text
AIQuantTradingResearch.slnx exists
solution parses
project count = 8
solution folder count = 2
solution folders = /src/ and /tests/
each project appears exactly once
no unexpected project exists
no expected project is missing
```

Use supported .NET tooling and direct `.slnx` inspection where needed.

Do not modify solution XML.

---

# 9. Project Inventory Validation

Expected production projects:

```text
AIQuantTradingResearch.Domain
AIQuantTradingResearch.Application
AIQuantTradingResearch.Infrastructure
AIQuantTradingResearch.Worker
```

Expected test projects:

```text
AIQuantTradingResearch.Domain.Tests
AIQuantTradingResearch.Application.Tests
AIQuantTradingResearch.Infrastructure.Tests
AIQuantTradingResearch.Architecture.Tests
```

Validate physical paths, project names, and target framework.

Do not infer project existence from documentation alone.

---

# 10. Production Dependency Graph Validation

Inspect actual production `ProjectReference` relationships.

Required:

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

Unexpected production edges are failures.

Do not repair references during WP13.

---

# 11. Root Build Configuration Validation

Inspect:

```text
global.json
Directory.Build.props
Directory.Packages.props
.editorconfig
```

Confirm they remain compatible with the Release 0.8 implementation and accepted WP05 baseline.

Validate at minimum:

```text
effective SDK
target framework behavior
nullable policy
implicit usings policy
analyzer/warning policy where defined
central package management behavior where defined
```

Do not redesign build policy.

Do not change warnings to make validation pass.

---

# 12. Worker Host Validation

Inspect the actual Worker composition root.

Expected lifecycle:

```text
create builder
→ AddApplication
→ AddInfrastructure
→ build
→ run
```

Validate that the Worker remains minimal.

Do not claim or require:

```text
HTTP API
controllers
web endpoints
scheduler
market-data loop
trading engine
message broker
database
plugin discovery
```

unless Release 0.8 authority explicitly requires it.

Do not start the Worker indefinitely as part of validation unless a safe bounded validation method exists and is required.

Compilation plus source inspection may be sufficient for host structure.

---

# 13. Dependency Registration Validation

Validate the accepted registration boundaries.

Expected:

```text
AddApplication(...)
AddInfrastructure(...)
```

At the Release 0.8 skeleton stage, these boundaries may intentionally contain no concrete registrations.

Confirm current behavior matches accepted documentation and implementation.

Do not populate them during WP13.

---

# 14. Test Skeleton Validation

Expected test projects:

```text
Domain.Tests
Application.Tests
Infrastructure.Tests
Architecture.Tests
```

The first three may intentionally contain zero tests.

That is acceptable when consistent with Release 0.8 authority.

Do not manufacture placeholder tests merely to produce a non-zero test count.

---

# 15. Architecture Test Validation

Execute Architecture.Tests directly.

Required:

```text
Discovered = 7
Passed = 7
Failed = 0
```

Confirm the tests enforce exactly the accepted Release 0.8 architecture constraints:

```text
6 forbidden dependency rules
1 acyclic production graph rule
```

Do not overstate architecture coverage.

---

# 16. Engineering Script Validation

Validate the WP11 scripts:

```text
restore.ps1
build.ps1
build.sh
clean.ps1
format.ps1
test.ps1
verify.ps1
```

Confirm:

- expected scripts exist;
- mandatory scripts target the authoritative solution where appropriate;
- obsolete Api/SharedKernel paths are absent;
- machine-specific repository paths are absent;
- failure propagation remains explicit;
- `format.ps1` uses non-mutating verification behavior;
- `verify.ps1` orchestrates the accepted stages.

Do not rewrite scripts during WP13.

---

# 17. Clean-State Validation Sequence

The primary integrated validation sequence is:

## Step A — Initial Verification

Before cleaning, execute the canonical verification workflow once.

Expected:

```text
PASS
```

This proves the accepted WP12 working state remains valid.

## Step B — Approved Clean

Execute:

```text
eng/clean.ps1
```

using the repository-supported PowerShell invocation required by the current environment.

Confirm:

```text
generated build outputs removed
repository files preserved
tracked/untracked user files preserved
```

## Step C — Post-Clean State Inspection

Inspect generated-output state and Git state.

Do not use destructive Git cleanup.

## Step D — Restore

Execute:

```text
eng/restore.ps1
```

Expected:

```text
Exit Status = 0
```

## Step E — Format Verification

Execute:

```text
eng/format.ps1
```

Expected:

```text
Exit Status = 0
Tracked file changes = 0
```

## Step F — Build

Execute:

```text
eng/build.ps1
```

Expected:

```text
Exit Status = 0
Errors = 0
```

## Step G — Test

Execute:

```text
eng/test.ps1
```

Expected:

```text
Exit Status = 0
Architecture.Tests = 7/7
```

## Step H — Full Verify

Execute:

```text
eng/verify.ps1
```

Expected:

```text
Exit Status = 0
```

This proves orchestration works after clean-state reconstruction.

## Step I — Shell Build

If the environment can execute `eng/build.sh`, validate it.

If the current environment cannot safely execute it, report:

```text
NOT EXECUTED — environment limitation
```

Do not treat an unavailable shell as a repository failure when the script was already validated in WP11 and no script changes occurred.

---

# 18. Obsolete Artifact Validation

Search current implementation and current-state documentation for obsolete Release 0.8 predecessor artifacts, including:

```text
AIQuantTradingResearch.Api
AIQuantTradingResearch.SharedKernel
obsolete root .sln
obsolete project paths
```

Classify matches:

```text
CURRENT-STATE VIOLATION
HISTORICAL
PLANNED
UNRELATED
```

Historical execution artifacts and explicitly planned Shared Kernel concepts are not current-state violations.

Do not delete historical evidence.

---

# 19. Documentation Consistency Validation

Validate that current-state documentation remains consistent with actual implementation.

At minimum confirm documentation accurately represents:

```text
.slnx
8 projects
/src/ and /tests/
production graph
minimal Worker
empty/minimal DI boundaries
4 test projects
7 architecture tests
engineering scripts
future capabilities as planned
```

WP13 does not edit documentation.

Any material contradiction is a validation finding.

---

# 20. Environmental Warning Handling

Known environmental observations may include:

```text
NU1900 vulnerability metadata connectivity warnings
PowerShell execution-policy restrictions
line-ending conversion notices
```

Classify environmental conditions separately from repository defects.

Do not:

- disable NuGet auditing,
- modify execution policy permanently,
- normalize the whole repository's line endings,
- change configuration merely to silence warnings.

A warning is non-blocking when:

```text
the command succeeds
the repository contract remains satisfied
the warning does not invalidate evidence
```

---

# 21. Execution Procedure

## Step 1 — Read Authority

Read this prompt, execution plan, manifest, current implementation, scripts, and relevant aligned documentation completely.

## Step 2 — Record Execution Context

Record:

```text
repository root
branch
starting commit
initial Git status
configured SDK
effective SDK
available shells
```

## Step 3 — Verify No Authority Conflict

Compare implementation with Release 0.8 authority.

If a material conflict exists, do not repair it.

Record and classify it.

## Step 4 — Validate File Manifest

Compare actual Release 0.8 artifacts against the manifest.

## Step 5 — Validate Solution

Confirm exact project/folder membership.

## Step 6 — Validate Project Inventory

Confirm all eight expected projects physically exist and are correctly named.

## Step 7 — Validate Production Graph

Inspect actual ProjectReferences and prove zero cycles.

## Step 8 — Validate Build Configuration

Inspect root build/package/SDK/editor configuration.

## Step 9 — Validate Worker Host

Confirm minimal composition-root lifecycle.

## Step 10 — Validate Dependency Registration

Confirm accepted boundaries and current registration state.

## Step 11 — Validate Test Skeleton

Confirm all four test projects and intentional empty unit-test skeletons.

## Step 12 — Validate Architecture Tests Directly

Required:

```text
7/7 PASS
```

## Step 13 — Validate Engineering Script Definitions

Inspect all seven WP11 workflow scripts.

## Step 14 — Execute Initial Verify

Required:

```text
PASS
```

## Step 15 — Execute Approved Clean

Confirm only generated artifacts are removed.

## Step 16 — Inspect Git State After Clean

Confirm repository/user files are preserved.

## Step 17 — Execute Restore

Required:

```text
PASS
```

## Step 18 — Execute Format Verification

Required:

```text
PASS
No tracked mutation
```

## Step 19 — Execute Build

Required:

```text
PASS
0 errors
```

## Step 20 — Execute Test

Required:

```text
PASS
Architecture.Tests 7/7
```

## Step 21 — Execute Full Verify

Required:

```text
PASS
```

## Step 22 — Execute Shell Build if Supported

Record result or environment limitation.

## Step 23 — Search Obsolete Artifacts

Distinguish current-state violations from historical/planned references.

## Step 24 — Validate Documentation Consistency

Inspect current-state documentation against implementation.

## Step 25 — Inspect Final Git State

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
```

Compare with initial state.

Expected:

```text
WP13 repository delta = none
```

## Step 26 — Produce Final Report

Return the complete output contract below.

Do not create a report file unless separately authorized.

---

# 22. Acceptance Criteria

WP13 is accepted only when:

- [ ] Authoritative Release 0.8 sources were reviewed.
- [ ] Initial repository/Git state was recorded.
- [ ] Effective SDK was verified.
- [ ] No unresolved implementation/authority conflict exists.
- [ ] Release 0.8 file manifest was validated.
- [ ] Root `.slnx` exists and parses.
- [ ] Solution contains exactly 8 projects.
- [ ] Solution contains exactly `/src/` and `/tests/` organization required by authority.
- [ ] Each expected project appears exactly once.
- [ ] No unexpected Release 0.8 project exists.
- [ ] All expected project files physically exist.
- [ ] Production graph exactly matches the accepted graph.
- [ ] Production graph has zero cycles.
- [ ] Root build configuration remains compatible with accepted WP05 policy.
- [ ] Effective SDK is compatible with `global.json`.
- [ ] Worker remains the accepted minimal composition root.
- [ ] Application and Infrastructure registration boundaries remain correct.
- [ ] Four test projects exist.
- [ ] Empty unit-test skeletons remain acceptable and intentional.
- [ ] Architecture.Tests directly discovers 7 tests.
- [ ] Architecture.Tests directly passes 7 tests.
- [ ] Architecture.Tests has 0 failures.
- [ ] All seven WP11 workflow scripts exist.
- [ ] Engineering scripts contain no obsolete current-state Api/SharedKernel targets.
- [ ] Initial canonical verify passes.
- [ ] Approved clean succeeds without deleting repository/user assets.
- [ ] Restore succeeds from post-clean state.
- [ ] Format verification succeeds without tracked mutation.
- [ ] Build succeeds with 0 errors.
- [ ] Test succeeds.
- [ ] Full verify succeeds after clean-state reconstruction.
- [ ] Shell build is validated when supported, or limitation is accurately documented.
- [ ] Current-state obsolete project/solution references are absent.
- [ ] Historical/planned references are not misclassified as current violations.
- [ ] Current-state documentation remains aligned with implementation.
- [ ] Validation introduces no tracked repository changes.
- [ ] Validation introduces no unexpected untracked repository artifacts.
- [ ] Nothing is staged.
- [ ] Nothing is committed.
- [ ] Nothing is pushed.
- [ ] Final Git state is compared with initial Git state.
- [ ] Complete validation evidence is recorded.

Any failed mandatory criterion must affect the final decision.

---

# 23. Decision Model

Use:

```text
COMPLETE
```

only when all mandatory Release 0.8 skeleton validation criteria pass and no WP13-owned action remains.

Use:

```text
COMPLETE WITH ACTIONS
```

only when the skeleton is valid but a clearly non-blocking external/environmental/later-owned action remains.

Examples may include:

```text
NU1900 environmental connectivity warning
unsupported local shell preventing duplicate build.sh execution
non-blocking Git line-ending notice
```

Do not use `COMPLETE WITH ACTIONS` to hide a failed mandatory Release 0.8 criterion.

Use:

```text
BLOCKED
```

when a mandatory Release 0.8 criterion fails or the repository cannot be validated without unauthorized repair.

---

# 24. Expected Output Contract

Return one complete **Full Skeleton Validation Execution Report**.

Use this structure.

# Full Skeleton Validation Execution Report

## 1. Executive Summary

State:

- what WP13 validated;
- whether repository changes were made;
- integrated workflow result;
- architecture result;
- final decision.

## 2. Execution Context

```text
Repository:
Branch:
Starting Commit:
Initial Working Tree:
Configured SDK:
Effective SDK:
Available Shells:
```

## 3. Authoritative Sources Reviewed

List exact material paths.

## 4. Initial Repository State

Report:

```text
git status --short
```

Classify pre-existing changes.

## 5. Release 0.8 Manifest Validation

| Manifest Area | Expected | Actual | Result | Evidence |
| --- | --- | --- | --- | --- |

## 6. Solution Validation

```text
Solution:
Parses:
Project count:
Solution folder count:
Production projects:
Test projects:
Missing projects:
Unexpected projects:
Duplicate projects:
Assessment:
```

## 7. Project Inventory Validation

| Project | Expected Path | Exists | Target Framework | Result |
| --- | --- | --- | --- | --- |

## 8. Production Dependency Graph

```text
Domain:
Application:
Infrastructure:
Worker:
Cycles:
Unexpected edges:
Assessment:
```

## 9. Root Build Configuration

```text
Configured SDK:
Effective SDK:
Target framework:
Nullable:
Implicit usings:
Central package management:
Analyzer/warning policy:
Assessment:
```

## 10. Worker Host Validation

```text
Host model:
Composition lifecycle:
Unexpected runtime capabilities:
Assessment:
```

## 11. Dependency Registration Validation

```text
Application boundary:
Infrastructure boundary:
Concrete registrations:
Configuration dependency:
Assessment:
```

## 12. Test Skeleton Validation

```text
Domain.Tests:
Application.Tests:
Infrastructure.Tests:
Architecture.Tests:
Assessment:
```

## 13. Architecture Test Validation

```text
Command:
Discovered:
Passed:
Failed:
Rules verified:
Assessment:
```

## 14. Engineering Script Validation

| Script | Exists | Responsibility | Target/Behavior | Result |
| --- | --- | --- | --- | --- |

## 15. Initial Verify

```text
Command:
Exit Status:
Warnings:
Errors:
Assessment:
```

## 16. Clean Validation

```text
Command:
Exit Status:
Generated outputs removed:
Repository/user files preserved:
Git delta:
Assessment:
```

## 17. Post-Clean Restore

```text
Command:
Exit Status:
Warnings:
Errors:
Assessment:
```

## 18. Format Verification

```text
Command:
Exit Status:
Tracked files changed:
Assessment:
```

## 19. Build Validation

```text
Command:
Exit Status:
Warnings:
Errors:
Assessment:
```

## 20. Test Validation

```text
Command:
Exit Status:
Architecture tests:
Assessment:
```

## 21. Full Verify After Clean Reconstruction

```text
Command:
Substeps:
Exit Status:
Assessment:
```

## 22. Shell Build Validation

```text
Command:
Environment supported:
Exit Status:
Assessment:
```

If not executed, explain the environment limitation.

## 23. Obsolete Artifact Assessment

```text
Current-state Api references:
Current-state SharedKernel project references:
Obsolete solution references:
Historical references:
Planned references:
Assessment:
```

## 24. Documentation Consistency Validation

```text
Solution/project structure:
Dependency graph:
Worker/DI:
Test skeleton:
Architecture tests:
Engineering workflow:
Current vs future distinction:
Contradictions:
Assessment:
```

## 25. Environmental Observations

List only relevant environmental warnings and explain whether they affect acceptance.

## 26. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 27. Scope Compliance

| Scope Check | Result | Evidence |
| --- | --- | --- |
| Validation-only behavior preserved | PASS/FAIL | ... |
| No production/test changes | PASS/FAIL | ... |
| No project/solution changes | PASS/FAIL | ... |
| No script changes | PASS/FAIL | ... |
| No configuration/package changes | PASS/FAIL | ... |
| No documentation changes | PASS/FAIL | ... |
| No CI/Docker changes | PASS/FAIL | ... |
| No staging/commit/push | PASS/FAIL | ... |
| Final Git state equals initial state except expected generated/ignored outputs | PASS/FAIL | ... |

## 28. Findings

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

## 29. Acceptance Criteria

Reproduce the WP13 acceptance criteria with PASS/FAIL.

## 30. Final Git State

Report:

```text
git status --short
```

Compare directly with initial state.

State:

```text
WP13-owned tracked changes:
WP13-owned untracked repository artifacts:
Staged changes:
Unexpected changes:
```

## 31. Final Decision

State exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

Explain the evidence supporting the decision.

## 32. Release 0.8 Readiness

State whether the validated repository satisfies the technical skeleton acceptance requirements of Release 0.8.

Do not mark GitHub milestones/issues/releases complete unless explicitly authorized.

## 33. Next Action

Identify the next authoritative step exactly from:

```text
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
```

This may be another work package, a release-level closure step, or a transition to the next release.

Do not infer the next step from memory.

Do not begin it.

---

# 25. Prohibited Behaviors

Do not:

- repair implementation during validation without explicit WP13 authority;
- modify production code;
- modify test code;
- modify architecture tests;
- modify project references;
- modify project files;
- modify `.slnx`;
- modify `eng/`;
- modify documentation;
- modify packages;
- modify build configuration;
- modify SDK configuration;
- create CI;
- modify Docker;
- add future-release capabilities;
- use destructive Git cleanup;
- stage;
- commit;
- push;
- create a pull request;
- mark GitHub issues/milestones complete;
- begin the next work package or release.

---

# 26. Completion Model

```text
Read Authority
      ↓
Record Initial State
      ↓
Validate Manifest
      ↓
Validate Solution + Projects
      ↓
Validate Dependency Graph
      ↓
Validate Build Configuration
      ↓
Validate Worker + DI
      ↓
Validate Tests + Architecture Rules
      ↓
Validate Engineering Scripts
      ↓
Initial Verify
      ↓
Approved Clean
      ↓
Restore
      ↓
Format Verification
      ↓
Build
      ↓
Test
      ↓
Full Verify
      ↓
Shell Validation if Supported
      ↓
Validate Obsolete References
      ↓
Validate Documentation Consistency
      ↓
Compare Final Git State
      ↓
Release 0.8 Readiness Decision
      ↓
COMPLETE | COMPLETE WITH ACTIONS | BLOCKED
```

---

# 27. Final Instruction

Execute **Phase 2 — Release 0.8 / Work Package 13 — Full Skeleton Validation** against the actual current `AIQuantTradingResearch` repository.

This is a validation work package.

Do not modify the repository implementation to make validation pass.

Read all authoritative sources first.

Record the initial Git state and preserve all pre-existing user work.

Validate:

```text
Release 0.8 file manifest
AIQuantTradingResearch.slnx
8-project inventory
/src/ and /tests/ organization
production dependency graph
zero cycles
root build configuration
minimal Worker composition root
dependency registration boundaries
4 test projects
7 executable architecture tests
7 WP11 engineering scripts
current-state documentation alignment
```

Then prove clean reconstruction using the approved engineering workflow:

```text
initial verify
clean
restore
format verification
build
test
full verify
```

Validate `build.sh` when the environment supports it.

Distinguish environmental warnings from repository defects.

Search for obsolete current-state Api/SharedKernel/old-solution artifacts without deleting historical or planned references.

At the end, compare final Git state with initial Git state and prove WP13 introduced no repository changes.

Return the complete **Full Skeleton Validation Execution Report**.

Finish with exactly one evidence-based decision:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

State Release 0.8 technical skeleton readiness.

Identify the next authoritative step exactly from the Release 0.8 execution plan.

Do not begin it.

---

# Conclusion

Work Package 13 is the proof boundary for Release 0.8.

Earlier work packages created individual pieces of the solution skeleton. WP13 establishes whether those pieces function together as a reproducible engineering baseline.

The validation path is:

```text
Accepted WP12 Repository
        ↓
Manifest + Structural Validation
        ↓
Architecture Validation
        ↓
Configuration Validation
        ↓
Host + DI Validation
        ↓
Test + Script Validation
        ↓
Initial Verify
        ↓
Clean Generated State
        ↓
Restore
        ↓
Format Verification
        ↓
Build
        ↓
Test
        ↓
Full Verify
        ↓
Documentation Consistency
        ↓
Zero-Change Git Comparison
        ↓
Release 0.8 Readiness
```

The key requirement is not merely that the repository builds once.

The repository must demonstrate that its documented structure, executable architecture rules, engineering scripts, and clean reconstruction workflow all describe and validate the same system.

WP13 therefore closes the technical validation loop for the Solution Skeleton.

> **A skeleton is complete only when it can be reconstructed, validated, and explained from repository authority without hidden repair steps or undocumented assumptions.**
