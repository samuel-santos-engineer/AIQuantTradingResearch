# Release 1.14 WP07 — Administrative REST Enabled-Mode Shutdown: Host-Executed Container Requalification Script Preparation Authority

## Authority and decision

Owner authorizes **script preparation only** for a subsequent owner-executed, bounded local Docker qualification of enabled-mode shutdown. **Do not execute Docker, the generated script, or any container workload in Codex.** Stop for owner review after producing the artifacts. This authority does not authorize deployment, commits, pushes, or source repairs.

Baseline: `draft/release-1.14-wp07-partial-public-disabled` at `67893f51931ee183f71050a83b81127e6e648b87`. The working tree is intentionally dirty. Preserve all existing tracked/untracked changes; do not reset, clean, checkout, stash, or overwrite them.

Current evidence: targeted shutdown changes were made in five authorized files; 59 .NET and 26 Python offline tests passed. Exit 0 now requires host shutdown, `STOP_UI`/`STOPPED`, channel EOF, agent exit 0 and successful disposal, with a shared four-second deadline. Actual container shutdown remains unqualified. Disabled-mode Option A is retained (direct Streamlit on 8501, generic UI fallback allowed). Docker Desktop is accessible to the owner's Windows account but not to `codexsandboxoffline`.

## Allowed deliverables and files

Prepare exactly two **new**, self-contained artifacts under an appropriate `eng/qualification/` or existing equivalent tooling directory, provided their target paths are absent:

1. `release-1.14-wp07-enabled-shutdown-qualification.ps1` — owner-reviewed, owner-executed script.
2. `release-1.14-wp07-enabled-shutdown-qualification-RUNBOOK.md` — prerequisites, inspection steps, exact invocation, evidence interpretation and cleanup procedure.

If either filename already exists, STOP and report without overwriting. Do not modify application source, tests, Dockerfile, entrypoint, dependencies, CI, docs outside this new runbook, or other repository files. No generated artifacts should be staged or committed. If creating these files conflicts with existing worktree preservation or project policy, STOP and request owner direction.

## Mandatory PowerShell compatibility

Target **Windows PowerShell 5.1.26100.9444** (Desktop edition), not PowerShell 7. Use compatible cmdlets, syntax, error handling and native-process invocation. Avoid `&&`, `||`, `??`, ternary operators, PowerShell 7-only features and reliance on newer modules. Place `#Requires`, `[CmdletBinding()]` and `param()` in valid positions. Ensure external-command exit codes are checked explicitly; avoid relying on `$?` alone. Keep output encoding compatible with Windows PowerShell 5.1. The runbook must state this requirement.

## Script security and preflight

- Explicitly require owner invocation; do not auto-run during generation or validation.
- Confirm current Git branch/HEAD and record pre/post `git status --porcelain` without requiring a clean tree. Do not mutate the tree. Fail closed on baseline mismatch.
- Verify Windows identity is the owner's authorized Docker-capable session, Docker context is exactly `desktop-linux`, and `docker info` succeeds; do not hard-code or disclose the owner's username in reports. Do not elevate, change ACLs, switch contexts, grant pipe access or disable sandbox protections.
- Check Docker build context, candidate paths and required runtime settings against repository implementation before generating the script. If they cannot be established from source, STOP rather than inventing flags or image entrypoints.
- Use unique, clearly labeled disposable image/container identifiers; inspect existence before use; refuse collisions. Do not prune shared cache or delete unrelated resources.
- No privileged containers, `--cap-add`, host network, mounts, Docker socket mounts, production secrets, Azure calls, SQLite/provider access or Worker activation. Bind published ports to `127.0.0.1` only; set `--cpus 0.5`, `--memory 512m`, `--pids-limit 64`, and no restart policy.
- Generate a canonical synthetic 32-byte key locally in process memory; pass it into enabled-mode startup using the implementation's required configuration mechanism **without embedding its value in command arguments, Docker inspect-visible environment, host files, logs or the evidence report**. If this cannot be achieved with the available Docker CLI and implementation, STOP at preparation and explain the unresolved credential-delivery design. Do not weaken authentication or TLS checks to make the test pass.
- Never print or persist secrets, credential digests, full environment dumps, arbitrary `/proc` contents, descriptor contents, private configuration, or exception stacks. Scrub any captured logs to an explicit allowlist of fixed outcome/stage codes and benign Streamlit startup/shutdown events; do not simply dump raw logs.

## Qualification procedure for later owner execution

The generated script must implement bounded, fail-closed phases:

1. One image build maximum from the unchanged candidate. One enabled-mode container start maximum; one application container concurrently. No retry loops that create additional containers.
2. Startup-relative monotonic timestamps; bounded readiness (up to 30 seconds), local listener/health/root HTTP status and categorized transport failures; preserve UI markup check. Avoid interpreting pre-readiness transport failures as definitive application defects.
3. Observe process roles/identities and security restrictions through nonprivileged, fixed checks where permitted: router PID 1 UID/GID 1100:1100, presentation agent and Streamlit UID/GID 1000:1000, no supplementary groups, zero effective/permitted/inheritable capabilities, `NoNewPrivs=1`. Do not bypass cross-UID `/proc` denial. Report inaccessible observations as NOT_QUALIFIED.
4. Perform a minimal, bounded WebSocket upgrade/close check only if a safe PowerShell 5.1-compatible implementation is available; otherwise explicitly mark NOT_RUN. No authenticated administrative collections; unauthenticated `/admin` requests must not disclose diagnostics. TLS policy remains unchanged; an HTTP 503 under local non-TLS conditions is not proof of production TLS correctness.
5. Issue exactly one normal Docker stop with five-second grace, record host elapsed time, container exit code, OOM status, and sanitized fixed shutdown stage outcomes. **PASS for enabled graceful shutdown requires exit 0, no OOM, and affirmative evidence of the complete stop protocol**; exit 0 alone is insufficient. If protocol evidence cannot be observed safely, report NOT_QUALIFIED rather than PASS.
6. Always attempt narrowly targeted cleanup of only the script-created container/image/temp context. No broad prune, no deletion of existing repository artifacts. Record cleanup success/failure, before/after Git status and a bounded total wall-clock budget of at most ten minutes. Stop on exceeded budgets or security anomalies.

No attempt to inspect credential bytes, perform authenticated collections, validate Azure F1 capacity, qualify complete dependency immutability or prove comprehensive key confinement. FD metadata qualification remains a separate gate.

## Preparation-only validation permitted to Codex

Codex may inspect repository files, use Git read-only commands and perform static script review. **Do not run the script**, even with `-WhatIf`; do not call Docker or perform container-based smoke tests. If a safe, already-available PowerShell 5.1 parser-only check exists, it may be used without running script logic; otherwise document that parser/runtime compatibility is unverified. Do not install tools or modules.

## Required report and STOP

Report exact created paths, pre/post Git status, static checks, any unresolved assumptions, credential-handling feasibility, phase/budget summary, and explicit instructions for owner inspection before execution. Clearly separate script readiness from any actual runtime qualification. STOP for owner review. Do not imply that the script was executed or that shutdown passed.

Release gates remain: `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability **NO-GO**, Azure passive qualification **NO-GO**, public Worker activation **NO-GO**. Do not change these values or infer Azure readiness from local evidence.
