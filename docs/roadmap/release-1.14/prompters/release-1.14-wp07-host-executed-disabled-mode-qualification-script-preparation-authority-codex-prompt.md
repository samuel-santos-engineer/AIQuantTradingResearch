# Release 1.14 WP07 — Host-Executed Disabled-Mode Qualification Script Preparation Authority

## Owner decision and purpose

Authorize **preparation only** of a narrowly scoped, reviewable PowerShell script for the owner to execute later from their existing authorized Windows PowerShell identity. **Do not execute the script or any Docker operation under this authority.** The purpose is to collect reproducible, sanitized evidence for disabled-mode Streamlit startup/readiness and graceful shutdown without granting Docker access to the Codex sandbox.

Confirmed access evidence:
- Owner PowerShell: `sam-laptop-hp\sabsf`; `docker context show` = `desktop-linux`; Docker Engine version `29.7.2`; access succeeds.
- Codex environment: `sam-laptop-hp\codexsandboxoffline`; context `desktop-linux`; Docker named pipe `npipe:////./pipe/dockerDesktopLinuxEngine` returns permission denied.
- This establishes an execution-identity mismatch, **not** the precise Windows ACL/sandbox cause. Do not modify accounts, named-pipe ACLs, Docker Desktop configuration, or sandbox restrictions.

Repository baseline expected: branch `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`. The worktree is already dirty; preserve all existing changes. `PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability, Azure passive qualification and public Worker activation remain **NO-GO**.

## Stage 0 — Read-only preflight and design

1. Verify branch/HEAD and capture Git status without modifying files. If the baseline differs, STOP and report.
2. Inspect the current `Dockerfile`, `container/entrypoint.sh`, relevant Streamlit startup and existing qualification conventions to determine the exact disabled-mode launch contract and safe build context. Do not guess image arguments or health endpoints.
3. Determine a fixed, conservative set of host-executed Docker commands and their required evidence. Preserve Option A: disabled mode runs direct Streamlit as PID 1, UID/GID 1000:1000; generic UI fallback (including HTTP 200) is acceptable; no administrative router/agent/collector runs.
4. If any necessary detail cannot be established without execution, mark it for owner review; do not improvise a broader permission.

## Stage 1 — Authorized deliverables: script and usage notes ONLY

Prepare **one** PowerShell 7-compatible `.ps1` script and **one** short Markdown runbook/report template, preferably as new files under an existing appropriate `eng/` qualification directory if repository conventions support it. Before writing, list the exact intended paths. Do not edit application source, tests, Dockerfile, entrypoint, dependencies, GitHub metadata, or roadmap authority files. Do not commit, push, open PRs, or create GitHub issues.

The script must be **inert until the owner explicitly runs it** and must implement all these controls:

### Hard safety gates

- Validate execution identity is the owner's approved account (`sam-laptop-hp\sabsf`) unless the owner explicitly edits an expected-identity parameter before running; do not attempt impersonation/elevation. Fail closed on mismatch.
- Require Docker context exactly `desktop-linux` and successful read-only engine access; do not select/switch context automatically.
- Verify expected repository branch and HEAD and preserve the dirty worktree; reject unexpected repo root or missing required files.
- Never use `--privileged`, `--cap-add`, host networking, host PID namespace, volume/bind mounts, secret mounts, arbitrary environment dumps, credential files, or production credentials.
- Publish the application only to `127.0.0.1` using an ephemeral host port; no public exposure. Use `--cpus 0.5 --memory 512m --pids-limit 64` and disable restarts.
- Force diagnostics disabled by exact environment setting; never provide an administrative key or enable diagnostics.
- Use a unique, clearly prefixed disposable image/container name. Record identifiers and delete **only** the resources created by this script. Never run global prune, remove shared images/containers, or alter shared Docker build cache.
- Reject an existing resource-name collision; do not delete it.
- No Azure, cloud deployment, databases, Worker operations, API authenticated collections, or network access beyond loopback qualification and necessary local image build dependencies.

### Strict budgets

- At most **one** image build and **one** container start; no retry or restart.
- Readiness observation up to **30 seconds from container start**; health/root probes must have explicit short per-request timeouts and a bounded polling interval.
- Shutdown with **five-second** grace and bounded client-side command timeout; do not let a hung Docker CLI exceed the overall budget.
- End-to-end script wall-clock cap **10 minutes**, including preflight, build, runtime, cleanup and reporting. If safe cleanup cannot complete within the cap, report exact remaining resource IDs without expanding authority.
- At most one application container running at any time. Zero authenticated diagnostic collections.

### Evidence and confidentiality

- Emit a sanitized, timestamped local report (UTC plus seconds since Docker start), with structured results `PASS`, `FAIL`, `NOT_RUN`, `NOT_QUALIFIED` and specific error categories (connection refused, timeout, HTTP non-200, unexpected response, other), rather than generic `OTHER` when a reliable category is available.
- Record first listener-absent and listener-present observations, health/root HTTP status, bounded response durations and presence of Streamlit markup **without writing full HTML**.
- Capture a bounded, sanitized excerpt of startup/shutdown logs only if an allowlist-based filter can prevent sensitive text; otherwise mark logs `NOT_OBSERVED`. Do not print raw Docker inspect JSON, environment variables, credential values, private paths, headers, cookies, or exception dumps.
- Record PID 1 identity, groups, capabilities and `NoNewPrivs` only through safe fixed checks; if unavailable without extra permissions, mark `NOT_QUALIFIED` rather than escalating.
- Record requested stop time, configured stop signal if safely observable, completion time, exit status, OOM state and whether force-kill occurred. Do not infer signal handling from exit code alone.
- Record image/container IDs, counts, exact budgets, cleanup result, and before/after Git status (avoid full unrelated diff or sensitive filenames in published summary).
- Write reports only to a clearly identified local output directory outside tracked application paths; no automatic upload or transmission. The runbook must instruct the owner to review/redact before sharing.
- Prefer a single script with explicit cleanup in `finally` and conservative error handling; no dynamic code download, `Invoke-Expression`, credential prompts, permission changes, or destructive fallback.

## Stage 2 — Non-execution validation

Allowed: source review, PowerShell syntax/static analysis **only if it cannot execute the qualification script or Docker**, review of command quoting, safety gates, timeout handling and cleanup behavior, and `git diff --check` where applicable. **Forbidden:** Docker `info`, `build`, `run`, `stop`, `rm`, image deletion, host HTTP probes, executing the qualification script, or requesting elevation. Do not treat successful static validation as runtime qualification.

## Required final report

Provide:
1. Exact files created and any unavoidable deviations; before/after branch, HEAD and Git status.
2. Concise explanation of script invocation from the owner's already-authorized PowerShell session, with an explicit **owner review and manual execution gate**.
3. Checklist mapping every safety limit and evidence requirement to the script's implementation.
4. Static validation performed and remaining risks/unknowns.
5. A separate future owner-controlled run recommendation; do not claim a qualification result.

**STOP** if the task requires application code edits, execution of Docker, privilege/sandbox/ACL changes, broader network access, secrets, additional dependencies, or exceeds the specified files and budgets. Preserve `PUBLIC_RUNTIME_VERIFIED=False` and all NO-GO gates.
