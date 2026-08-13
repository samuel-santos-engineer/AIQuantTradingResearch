Execute Phase 2 — Release 0.8, Work Package 09 — Architecture Tests.

Use the following repository file as the authoritative execution prompt:

docs/roadmap/release-0.8/prompts/09-architecture-tests-codex-prompt.md

Read that prompt completely before taking any action.

Execute it against the current AIQuantTradingResearch repository state.

Important:

- Follow the scope, authority hierarchy, safety rules, architecture-testing approach resolution process, validation requirements, acceptance criteria, and completion rules defined in the execution prompt.
- Resolve the smallest reliable architecture-testing approach from repository evidence before adding any package or test code.
- Implement executable architecture tests for these forbidden production dependencies:
  - Domain !→ Application
  - Domain !→ Infrastructure
  - Domain !→ Worker
  - Application !→ Infrastructure
  - Application !→ Worker
  - Infrastructure !→ Worker
- Implement production-graph acyclicity validation only when it is reliable and proportionate to the selected approach.
- Keep the architecture suite minimal and focused on Release 0.8 dependency boundaries.
- Do not implement business/unit tests.
- Do not implement integration or end-to-end tests.
- Do not add feature-specific, naming, namespace, or folder-convention rules unless explicitly required by the authoritative WP09 contract.
- Do not modify production source code.
- Do not modify production ProjectReference relationships.
- Preserve the accepted production graph exactly:
  - Domain → none
  - Application → Domain
  - Infrastructure → Application
  - Worker → Application, Infrastructure
- Do not add projects to AIQuantTradingResearch.slnx.
- Respect Central Package Management and add only the minimum architecture-testing dependency if one is required.
- Do not modify root build policy, Worker composition, engineering scripts, documentation, CI workflows, or Docker assets.
- Preserve all existing repository state outside the authorized WP09 change set.
- Do not stage, commit, push, or open a pull request.
- Build and execute Architecture.Tests.
- Perform safe negative validation when possible without leaving any forbidden dependency or temporary repository contamination behind.
- Revalidate all production projects and confirm the production dependency graph remains unchanged.
- Produce the complete Architecture Tests Execution Report required by the output contract.
- Finish with exactly one evidence-based decision:
  COMPLETE
  COMPLETE WITH ACTIONS
  BLOCKED

If complete, identify the next work package exactly as defined by RELEASE_0.8_EXECUTION_PLAN.md.

Do not begin the next work package.
