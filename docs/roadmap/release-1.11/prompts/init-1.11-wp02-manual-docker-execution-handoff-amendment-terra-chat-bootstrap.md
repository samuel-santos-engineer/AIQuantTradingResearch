Apply `init-1.11-wp02-manual-docker-execution-handoff-amendment-terra-codex-prompt.md` using **GPT-5.6 Terra**, then resume the pending Interactive Tooling Runtime Verification Authority.

Use the manual Docker/WSL handoff because the VS Code Codex sandbox cannot access Docker.

First give me one copy/paste-ready PowerShell block for interactive `sabsf` containing `whoami`, `az version`, `docker version`, `docker info`, `wsl --status`, and `wsl -l -v`, with labeled exit codes. Tell me exactly what output to return, then STOP.

After I return evidence, evaluate it. If it passes, give me the exact temporary Linux Docker build/run/cleanup PowerShell block, then STOP again for evidence.

Do not run `az login`, Azure resource operations, registry pushes, repository/GitHub mutations, or close #253.

Count my Docker mutations in the audit. After sufficient evidence, emit the tooling PASS markers and `AZURE F1 WP02 — EXECUTION AUTHORITY RERUN: READY`.
