Execute Phase 2 — Release 0.8, Work Package 05 — Root Build Configuration.

Use the following repository file as the authoritative execution prompt:

docs/roadmap/release-0.8/prompts/05-root-build-configuration-codex-prompt.md

Read that prompt completely before taking any action.

Execute it against the current AIQuantTradingResearch repository state.

Important:

- Follow the scope, authority hierarchy, safety rules, build-policy resolution process, validation requirements, acceptance criteria, and completion rules defined in the execution prompt.
- Treat the existing Directory.Build.props as an artifact to inspect and reconcile, not something to replace automatically.
- Measure the effective MSBuild properties of all four production projects before changing configuration.
- Derive every build-policy change from authoritative repository guidance.
- Apply only the minimum required changes.
- Preserve the accepted WP04 dependency graph exactly:
  - Domain → none
  - Application → Domain
  - Infrastructure → Application
  - Worker → Application, Infrastructure
- Do not add projects to AIQuantTradingResearch.slnx.
- Do not modify global.json, Directory.Packages.props, or .editorconfig.
- Do not add package references or change central package versions.
- Do not implement Worker hosting, dependency injection, tests, feature code, engineering scripts, CI, or other later Release 0.8 work.
- Preserve all existing repository state outside the authorized WP05 change set.
- Do not stage, commit, push, or open a pull request.
- Produce the complete Root Build Configuration Execution Report required by the output contract.
- Finish with exactly one evidence-based decision:
  COMPLETE
  COMPLETE WITH ACTIONS
  BLOCKED

Do not begin Work Package 06.
