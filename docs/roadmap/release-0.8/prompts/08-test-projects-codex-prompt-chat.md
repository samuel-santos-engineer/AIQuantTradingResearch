Execute Phase 2 — Release 0.8, Work Package 08 — Test Projects.

Use the following repository file as the authoritative execution prompt:

docs/roadmap/release-0.8/prompts/08-test-projects-codex-prompt.md

Read that prompt completely before taking any action.

Execute it against the current AIQuantTradingResearch repository state.

Important:

- Follow the scope, authority hierarchy, safety rules, test-framework resolution process, package-governance rules, validation requirements, acceptance criteria, and completion rules defined in the execution prompt.
- Resolve the test framework and exact test-project manifest from authoritative repository guidance before creating anything.
- Create only the authorized test projects:
  - AIQuantTradingResearch.Domain.Tests
  - AIQuantTradingResearch.Application.Tests
  - AIQuantTradingResearch.Infrastructure.Tests
  - AIQuantTradingResearch.Architecture.Tests
- Respect Central Package Management and add only the minimum required test SDK/framework packages.
- Remove meaningless template-generated sample tests unless the authoritative WP08 contract explicitly requires them.
- Add only the production ProjectReference relationships authorized for each test boundary.
- Do not implement substantive unit tests.
- Do not implement architecture rules/tests.
- Do not implement integration or end-to-end tests.
- Do not create mocks, fixtures, builders, test data, or helper frameworks.
- Preserve the accepted production dependency graph exactly:
  - Domain → none
  - Application → Domain
  - Infrastructure → Application
  - Worker → Application, Infrastructure
- Do not modify production source behavior.
- Do not add projects to AIQuantTradingResearch.slnx unless the authoritative WP08 execution contract explicitly requires it.
- Do not modify root build policy, engineering scripts, documentation, CI workflows, or Docker assets.
- Preserve all existing repository state outside the authorized WP08 change set.
- Do not stage, commit, push, or open a pull request.
- Restore, build, and execute/discover each test project.
- Treat zero discovered tests as acceptable when the empty test project is valid and the command exits successfully; do not add fake tests just to produce passing-test counts.
- Revalidate all production projects and confirm the production dependency graph remains unchanged.
- Produce the complete Test Projects Execution Report required by the output contract.
- Finish with exactly one evidence-based decision:
  COMPLETE
  COMPLETE WITH ACTIONS
  BLOCKED

Do not begin the next work package.
