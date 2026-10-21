# AIQuantTradingResearch contributor instructions

Follow repository governance, including `docs/governance/REPOSITORY_LF_LINE_ENDINGS_POLICY.md`.

For every Codex or AI-authored change:
- Write all new and modified text files as UTF-8 with LF (`\n`) line endings, including PowerShell, Markdown, source code, configuration and test files.
- Respect root `.gitattributes`; do not generate CRLF or mixed line endings.
- Run `git diff --check` and separately check changed text files for CRLF before committing or opening a PR.
- Do not reformat unrelated files or change historical artifacts solely to normalize line endings.
- Preserve existing governance, release boundaries, and scoped approval requirements.
