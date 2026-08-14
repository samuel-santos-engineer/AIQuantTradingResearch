Execute Phase 2 — Release 0.8, Work Package 10 — Solution Organization.

Use the following repository file as the authoritative execution prompt:

docs/roadmap/release-0.8/prompts/10-solution-organization-codex-prompt.md

Read that prompt completely before taking any action.

Execute it against the current AIQuantTradingResearch repository state.

Important:

- Follow the scope, authority hierarchy, safety rules, project-inventory contract, solution-folder resolution process, tooling requirements, validation requirements, acceptance criteria, and completion rules defined in the execution prompt.
- Verify the accepted WP09 baseline before making any change.
- Discover the actual repository project inventory and compare it with the authoritative Release 0.8 file manifest.
- Expect exactly these 8 projects unless repository authority explicitly states otherwise:
  - AIQuantTradingResearch.Domain
  - AIQuantTradingResearch.Application
  - AIQuantTradingResearch.Infrastructure
  - AIQuantTradingResearch.Worker
  - AIQuantTradingResearch.Domain.Tests
  - AIQuantTradingResearch.Application.Tests
  - AIQuantTradingResearch.Infrastructure.Tests
  - AIQuantTradingResearch.Architecture.Tests
- Resolve the smallest authoritative solution-folder model from repository documentation before modifying the solution.
- Add exactly the authoritative production and test projects to:
  AIQuantTradingResearch.slnx
- Organize every production project into the selected production solution folder.
- Organize every test project into the selected test solution folder.
- Ensure every project appears exactly once.
- Do not add unexpected, future, or speculative projects.
- Do not create empty or speculative solution folders.
- Prefer supported .NET SDK `.slnx` tooling for all solution modifications.
- Do not convert the `.slnx` solution to legacy `.sln`.
- Do not create, delete, rename, or physically move projects.
- Do not modify any production or test `.csproj` file.
- Do not modify production source code or test source code.
- Do not modify any ProjectReference relationship.
- Preserve the accepted production dependency graph exactly:
  - Domain → none
  - Application → Domain
  - Infrastructure → Application
  - Worker → Application, Infrastructure
- Preserve zero production dependency cycles.
- Do not modify package dependencies or package versions.
- Do not modify Directory.Build.props, Directory.Packages.props, global.json, or .editorconfig.
- Do not modify dependency registration or Worker behavior.
- Do not modify engineering scripts, documentation, CI workflows, or Docker assets.
- Preserve all existing repository state outside the authorized WP10 change set.
- Do not stage, commit, push, or open a pull request.
- After solution organization, validate the exact solution membership and solution-folder membership.
- Restore the organized solution.
- Build the organized solution.
- Execute tests through the organized solution.
- Verify that the WP09 architecture suite still executes successfully, with the existing architecture rules preserved.
- Revalidate the production dependency graph after solution organization.
- Inspect the final Git diff and prove that WP10 changed only the authorized solution artifact.
- Produce the complete Solution Organization Execution Report required by the output contract.
- Finish with exactly one evidence-based decision:
  COMPLETE
  COMPLETE WITH ACTIONS
  BLOCKED

If complete, identify the next work package exactly as defined by RELEASE_0.8_EXECUTION_PLAN.md.

Do not begin the next work package.
