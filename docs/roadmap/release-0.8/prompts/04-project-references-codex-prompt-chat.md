Execute Phase 2 — Release 0.8, Work Package 04 — Project References.

Use the following repository file as the authoritative execution prompt:

docs/roadmap/release-0.8/prompts/04-project-references-codex-prompt.md

Read that prompt completely before taking any action.

Execute it against the current AIQuantTradingResearch repository state.

Important:

- Follow the scope, authority hierarchy, safety rules, dependency-resolution process, validation requirements, acceptance criteria, and completion rules defined in the execution prompt.
- Derive the exact direct ProjectReference graph from the authoritative Release 0.8 and architecture documents before modifying any .csproj file.
- Do not guess dependency relationships.
- If authoritative sources materially conflict, stop and return BLOCKED with evidence.
- Add only the approved minimum ProjectReference relationships.
- Keep AIQuantTradingResearch.Domain dependency-free.
- Do not add projects to AIQuantTradingResearch.slnx.
- Do not add package references, tests, architecture tests, dependency-injection code, runtime behavior, feature code, build configuration changes, documentation changes, engineering scripts, or CI changes.
- Preserve all existing repository state outside the authorized .csproj changes.
- Do not stage, commit, push, or open a pull request.
- Produce the complete Project References Execution Report required by the output contract.
- Finish with exactly one evidence-based decision:
  COMPLETE
  COMPLETE WITH ACTIONS
  BLOCKED

Do not begin Work Package 05.
