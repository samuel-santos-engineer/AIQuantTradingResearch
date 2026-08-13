Execute Phase 2 — Release 0.8, Work Package 07 — Dependency Registration.

Use the following repository file as the authoritative execution prompt:

docs/roadmap/release-0.8/prompts/07-dependency-registration-codex-prompt.md

Read that prompt completely before taking any action.

Execute it against the current AIQuantTradingResearch repository state.

Important:

- Follow the scope, authority hierarchy, safety rules, DI dependency-resolution process, validation requirements, acceptance criteria, and completion rules defined in the execution prompt.
- Create only the Application and Infrastructure dependency-registration boundaries required by the execution prompt.
- Implement AddApplication with no real service registrations.
- Implement AddInfrastructure with no real service registrations.
- Resolve only the minimum compile-time dependency required for IServiceCollection and, only if justified by repository authority, IConfiguration.
- Update Worker Program.cs only to invoke AddApplication and AddInfrastructure between host-builder creation and host build.
- Preserve the accepted WP04 dependency graph exactly:
  - Domain → none
  - Application → Domain
  - Infrastructure → Application
  - Worker → Application, Infrastructure
- Do not add new ProjectReference relationships.
- Do not add projects to AIQuantTradingResearch.slnx.
- Do not add hosted/background services.
- Do not add application, infrastructure, market-data, storage, trading, AI/ML, plugin, API, telemetry, HTTP client, database, or provider behavior.
- Do not create tests or architecture tests.
- Do not modify root build policy, engineering scripts, documentation, CI workflows, or Docker assets.
- Preserve all existing repository state outside the authorized WP07 change set.
- Do not stage, commit, push, or open a pull request.
- Build all production projects and perform bounded Worker runtime smoke validation where safe.
- Produce the complete Dependency Registration Execution Report required by the output contract.
- Finish with exactly one evidence-based decision:
  COMPLETE
  COMPLETE WITH ACTIONS
  BLOCKED

Do not begin Work Package 08.
