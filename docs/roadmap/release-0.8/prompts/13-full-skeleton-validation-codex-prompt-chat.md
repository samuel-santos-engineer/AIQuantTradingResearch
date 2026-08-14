Execute Phase 2 — Release 0.8, Work Package 13 — Full Skeleton Validation.

Use the following repository file as the authoritative execution prompt:

docs/roadmap/release-0.8/prompts/13-full-skeleton-validation-codex-prompt.md

Read that prompt completely before taking any action.

Execute it against the current AIQuantTradingResearch repository state.

Important:

- This is a validation-first work package. Do not modify the repository implementation to make validation pass.
- Follow the authority hierarchy, no-change contract, validation sequence, acceptance criteria, decision model, and output contract defined in the authoritative prompt.
- Record the initial repository identity, branch, commit, Git state, configured SDK, effective SDK, and available shells before validation.
- Preserve every pre-existing user change. Do not use destructive Git cleanup, reset, restore, or checkout operations.
- Verify the accepted WP12 baseline before performing the integrated validation.
- Validate the Release 0.8 file manifest against the actual repository.
- Validate AIQuantTradingResearch.slnx:
  - parses successfully
  - exactly 8 projects
  - exactly 4 production projects
  - exactly 4 test projects
  - /src/ and /tests/ solution organization
  - no missing, unexpected, or duplicate projects
- Validate the production dependency graph exactly:
  - Domain → none
  - Application → Domain
  - Infrastructure → Application
  - Worker → Application, Infrastructure
- Confirm the production dependency graph contains zero cycles.
- Validate the accepted root build configuration:
  - global.json
  - Directory.Build.props
  - Directory.Packages.props
  - .editorconfig
- Confirm the effective SDK remains compatible with the repository configuration.
- Validate the Worker remains the minimal composition root:
  create builder
  → AddApplication
  → AddInfrastructure
  → build
  → run
- Validate the Application and Infrastructure dependency-registration boundaries without adding concrete functionality.
- Validate all four test projects:
  - AIQuantTradingResearch.Domain.Tests
  - AIQuantTradingResearch.Application.Tests
  - AIQuantTradingResearch.Infrastructure.Tests
  - AIQuantTradingResearch.Architecture.Tests
- Empty Domain/Application/Infrastructure test skeletons are acceptable when they remain consistent with Release 0.8 authority.
- Execute Architecture.Tests directly.
- Require:
  - 7 tests discovered
  - 7 tests passed
  - 0 tests failed
- Confirm those tests represent the six forbidden production dependency rules plus the acyclic production graph rule.
- Validate all WP11 engineering scripts:
  - eng/restore.ps1
  - eng/build.ps1
  - eng/build.sh
  - eng/clean.ps1
  - eng/format.ps1
  - eng/test.ps1
  - eng/verify.ps1
- Confirm mandatory scripts contain no obsolete Api, SharedKernel, old solution, or machine-specific repository targets.
- Confirm format validation is non-mutating.
- Confirm verify orchestration remains:
  restore
  → format verification
  → build
  → test

Perform the integrated validation sequence exactly as authorized:

1. Run the canonical verify workflow against the current WP12 state.
2. Run the approved eng/clean.ps1 workflow.
3. Confirm clean removed only generated build outputs and preserved repository/user files.
4. Inspect Git state after clean.
5. Run eng/restore.ps1.
6. Run eng/format.ps1 and confirm it introduces no tracked changes.
7. Run eng/build.ps1 and require zero build errors.
8. Run eng/test.ps1 and require Architecture.Tests 7/7.
9. Run eng/verify.ps1 and require the complete workflow to pass after clean reconstruction.
10. Run eng/build.sh if the current environment safely supports it. If not, report the environment limitation accurately rather than modifying the repository.

Also:

- Search for obsolete current-state AIQuantTradingResearch.Api, AIQuantTradingResearch.SharedKernel, obsolete .sln, and obsolete project-path references.
- Distinguish CURRENT-STATE violations from HISTORICAL, PLANNED, and UNRELATED references.
- Do not delete or rewrite legitimate historical artifacts.
- Validate that current-state documentation remains consistent with:
  - AIQuantTradingResearch.slnx
  - 8 projects
  - /src/ and /tests/
  - accepted production dependency graph
  - minimal Worker
  - dependency-registration boundaries
  - four test projects
  - seven architecture tests
  - WP11 engineering workflow
  - future capabilities remaining explicitly planned rather than implemented
- Treat known environmental warnings such as NU1900, PowerShell execution-policy restrictions, or line-ending notices separately from repository defects.
- Do not disable NuGet auditing.
- Do not permanently change PowerShell execution policy.
- Do not normalize repository line endings broadly.

WP13 must introduce no repository implementation changes.

Do not:

- modify production code
- modify test code
- modify architecture tests
- modify .csproj files
- modify AIQuantTradingResearch.slnx
- modify eng/ scripts
- modify documentation
- modify Directory.Build.props
- modify Directory.Packages.props
- modify global.json
- modify .editorconfig
- add packages
- create or modify CI
- modify Docker assets
- implement future-release functionality
- stage
- commit
- push
- open a pull request
- mark GitHub issues or milestones complete

At the end:

- Run git status --short.
- Inspect tracked and staged diffs.
- Compare the final Git state directly with the initial Git state.
- Prove that WP13 introduced zero tracked repository changes and zero unexpected untracked repository artifacts.
- Produce the complete Full Skeleton Validation Execution Report required by the authoritative prompt.
- Include the Release 0.8 technical skeleton readiness assessment.

Finish with exactly one evidence-based decision:

COMPLETE
COMPLETE WITH ACTIONS
BLOCKED

Do not use COMPLETE WITH ACTIONS to hide a failed mandatory Release 0.8 criterion.

If validation discovers a mandatory failure that would require implementation changes, preserve the evidence and return BLOCKED rather than repairing it.

Finally, identify the next authoritative step exactly as defined by:

docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md

The next step may be another work package, a Release 0.8 closure action, or transition to another release.

Do not infer it from memory.

Do not begin it.
