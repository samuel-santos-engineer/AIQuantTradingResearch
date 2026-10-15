# Release 1.14 WP07 — Enabled Shutdown Host Qualification Script Hardening Authority

## Owner decision and purpose

AUTHORIZE **script-only hardening and Docker-free verification**, not execution of the enabled-mode container qualification. This is a narrow follow-up to the owner's review of the prepared host-executed qualification script and runbook. **STOP for owner review** after changes and evidence; do not run the owner qualification.

## Baseline and preflight

- Repository: `AIQuantTradingResearch`; required branch `draft/release-1.14-wp07-partial-public-disabled`; required HEAD `67893f51931ee183f71050a83b81127e6e648b87`.
- Preserve the pre-existing dirty worktree. Record before/after Git status and hashes for all pre-existing files; never reset, clean, stash, commit, push or overwrite unrelated changes.
- Inspect the actual script and runbook before editing; cite precise lines for every change. If branch/HEAD differ, either file is missing, or scope cannot be met, **STOP**.

## Exact editable allowlist — two files only

1. `eng/azure-cli/r1.14-qualification/wp07-enabled-shutdown/release-1.14-wp07-enabled-shutdown-qualification.ps1`
2. `eng/azure-cli/r1.14-qualification/wp07-enabled-shutdown/release-1.14-wp07-enabled-shutdown-qualification-RUNBOOK.md`

No application code, tests, Dockerfile, dependencies, Azure configuration, project metadata or other paths may change. Do not create additional repository artifacts. Temporary files for permitted Docker-free checks must be outside the repository and removed afterward.

## Required targeted corrections

### A. Exception-resilient cleanup and reporting

- Review the `finally` flow for exceptions from filesystem cleanup, Docker resource cleanup *code paths* (inspection only; no Docker execution), manifest verification and JSON/report writing.
- Use narrow error handling so an exception in one cleanup/reporting action does not suppress later safe reporting or hide known resource identifiers. Never treat an unverified cleanup as success.
- Preserve ownership-label and exact-path checks. No wildcard deletion, force removal, prune, broad catch-and-ignore or privilege escalation. When uncertain, report exact known resources and mark `FAIL`, `NOT_QUALIFIED` or `NOT_OBSERVED` as appropriate.
- Ensure the report can still record failure if final report serialization fails; provide a safe console fallback without secrets or raw logs.

### B. Observation failures versus actual violations

- Review readiness-loop router/agent identity observations. Expected permission denial, timeout or unavailable metadata must be recorded as `NOT_QUALIFIED` and must not automatically be mislabeled an HTTP readiness failure.
- **Do not downgrade an observed wrong UID/GID, capability violation, unexpected privileged process or other positive security failure.** Those remain `FAIL` and trigger safe STOP/cleanup.
- Keep probes finite and do not add cross-UID `/proc` inspection, additional Docker privileges or retries. Preserve 30-second readiness and five-second stop grace.

### C. Final result after preservation and cleanup

- Compute/settle the authoritative overall verdict **after** shutdown, cleanup, Git preservation checks and elapsed-budget accounting, rather than relying on an early provisional PASS.
- Ensure `report.json`, console summary and individual status fields agree. A missing report, leftover resource, failed preservation check, observed budget overrun or unresolved cleanup must never produce overall PASS.
- Preserve explicit `NOT_RUN`, `NOT_OBSERVED` and `NOT_QUALIFIED` semantics; do not convert lack of evidence into PASS.

### D. Windows PowerShell compatibility and Docker-free validation

- **Mandatory compatibility target: Windows PowerShell 5.1.26100.9444 (Desktop edition).** Do not introduce PowerShell 7-only syntax, operators or dependencies. Keep `[CmdletBinding()]`/`param()` in legal positions.
- Perform static parse of the edited script and targeted inspection of embedded C# helper compatibility with .NET Framework available in Windows PowerShell 5.1.
- Where safely possible, run **Docker-free, nonprivileged helper self-tests** for fixed HTTP response, refusal and timeout classification, report finalization and failure-handling behavior. Self-tests must not run the qualification entrypoint, Docker, external network requests or any container operation. If such tests cannot be isolated without invoking Docker, mark them `NOT_RUN` and explain; do not bypass the gate.
- Verify `git diff --check` for authorized changes. Preserve original one-build/one-start limits and fixed resource boundaries.

## Security invariants — unchanged

- Manual execution only from the owner's existing Docker-authorized Windows account; Codex's sandbox must **not** access Docker or request elevation, pipe ACL/group changes, sandbox bypass or execution-policy bypass.
- One disposable image build and one enabled container start **in a later separately approved owner run only**; readiness at most 30 seconds, Docker stop grace five seconds, total ten-minute envelope.
- Synthetic key generated inside transient container process only; no key in host files, Docker configured environment/arguments, logs, reports or CLI output. No real credentials, authenticated administrative collections or credential contents/digests.
- Loopback-only publication, fixed CPU/memory/PID limits, no privileged mode, mounts, added capabilities, host networking, Worker, database or Azure actions.
- Option A disabled mode remains direct Streamlit; no changes to TLS trust policy, admin API availability or public activation gates.
- WebSocket qualification remains `NOT_RUN` unless a separately authorized test exists; do not inflate claims.

## Required deliverable and STOP gate

Return a concise owner-review report with:

1. Branch/HEAD and before/after worktree preservation evidence.
2. Exact changed lines in the **two allowlisted files** and rationale mapped to A–D.
3. Windows PowerShell 5.1 parse result and Docker-free test results (or explicit `NOT_RUN`).
4. Safety review of cleanup, security-failure classification, final verdict, credential handling and resource limits.
5. Residual risks and **GO/NO-GO recommendation for owner review**, **not** an authorization to execute.

No Docker calls, image builds, container starts, Azure access, Git commits/pushes or qualification workload. If an unexpected condition needs broader edits or privileged testing, **STOP and request new owner authority**.

`PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability, Azure passive qualification and public Worker activation remain **NO-GO**.
