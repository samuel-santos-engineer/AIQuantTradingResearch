Execute Phase 2 — Release 0.8, Work Package 06 — Minimal Worker Host.

Use the following repository file as the authoritative execution prompt:

docs/roadmap/release-0.8/prompts/06-minimal-worker-host-codex-prompt.md

Read that prompt completely before taking any action.

Execute it against the current AIQuantTradingResearch repository state.

Important:

- Follow the scope, authority hierarchy, safety rules, hosting-dependency resolution process, validation requirements, acceptance criteria, and completion rules defined in the execution prompt.
- Resolve the WP05 Worker build failure using the minimum evidence-backed .NET hosting dependency.
- Implement only the minimal Worker host lifecycle in Program.cs.
- Preserve the accepted WP04 dependency graph exactly:
  - Domain → none
  - Application → Domain
  - Infrastructure → Application
  - Worker → Application, Infrastructure
- Do not add AddApplication.
- Do not add AddInfrastructure.
- Do not create DependencyInjection.cs.
- Do not register hosted/background services.
- Do not add feature behavior, tests, architecture tests, engineering scripts, CI workflows, or other later Release 0.8 assets.
- Do not add projects to AIQuantTradingResearch.slnx.
- Preserve all existing repository state outside the authorized WP06 change set.
- Do not stage, commit, push, or open a pull request.
- Build the Worker successfully and perform bounded runtime smoke validation where safe.
- Produce the complete Minimal Worker Host Execution Report required by the output contract.
- Finish with exactly one evidence-based decision:
  COMPLETE
  COMPLETE WITH ACTIONS
  BLOCKED

Do not begin Work Package 07.
