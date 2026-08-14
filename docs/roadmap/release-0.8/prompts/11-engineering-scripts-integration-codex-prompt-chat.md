Execute Phase 2 — Release 0.8, Work Package 11 — Engineering Scripts Integration.

Use the following repository file as the authoritative execution prompt:

docs/roadmap/release-0.8/prompts/11-engineering-scripts-integration-codex-prompt.md

Read that prompt completely before taking any action.

Execute it against the current AIQuantTradingResearch repository state.

Important:

- Follow the scope, authority hierarchy, safety rules, script-inventory process, responsibility boundaries, path-resolution rules, validation requirements, acceptance criteria, and completion rules defined in the execution prompt.
- Verify the accepted WP10 baseline before modifying anything.
- Inventory and classify every existing file under eng/.
- Determine the actual restore, build, test, format, verify, clean, and cross-platform script responsibilities from repository authority.
- Integrate existing mandatory engineering scripts with:
  AIQuantTradingResearch.slnx
  wherever solution-level targeting is appropriate.
- Remove obsolete Api, SharedKernel, old .sln, or predecessor solution/project references from mandatory engineering workflows.
- Ensure mandatory scripts resolve the repository root reliably and do not depend on machine-specific paths.
- Preserve distinct responsibilities for restore, build, test, format, and verify.
- Prefer thin orchestration over duplicated build logic.
- Ensure mandatory native-command failures propagate as non-zero script failures.
- Validate restore, build, test, format, and verify scripts by executing them.
- Confirm Architecture.Tests still executes successfully with the existing 7 architecture tests preserved.
- Preserve the accepted production dependency graph exactly:
  - Domain → none
  - Application → Domain
  - Infrastructure → Application
  - Worker → Application, Infrastructure
- Preserve zero production dependency cycles.
- Preserve exactly 8 projects in AIQuantTradingResearch.slnx.
- Do not modify production or test source code.
- Do not modify any production or test ProjectReference.
- Do not modify solution membership or solution organization.
- Do not modify Directory.Build.props, Directory.Packages.props, global.json, or .editorconfig.
- Do not add package dependencies.
- Do not create CI or GitHub Actions workflows.
- Do not add coverage, security, deployment, release, Docker, or unrelated engineering tooling.
- Do not broadly redesign the eng/ architecture.
- Do not broadly reformat the repository.
- Preserve all existing repository state outside the authorized WP11 eng/ change set.
- Do not stage, commit, push, or open a pull request.
- Perform safe failure-propagation validation where possible without corrupting repository state.
- Revalidate the final solution inventory, architecture tests, and production dependency graph.
- Produce the complete Engineering Scripts Integration Execution Report required by the output contract.
- Finish with exactly one evidence-based decision:
  COMPLETE
  COMPLETE WITH ACTIONS
  BLOCKED

If complete, identify the next work package exactly as defined by RELEASE_0.8_EXECUTION_PLAN.md.

Do not begin the next work package.
