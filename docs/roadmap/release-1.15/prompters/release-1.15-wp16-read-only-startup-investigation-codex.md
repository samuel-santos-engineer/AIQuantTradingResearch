# Release 1.15 — WP16 Codex Prompt: Read-Only Azure F1 Research Startup Investigation

## Mission
Investigate the failed WP15 research staging HTTPS health smoke without changing Azure state. Identify the most likely evidenced cause of the timeout, and, only if justified, prepare a focused repository fix and PR for owner review. Keep the product architecture simple: Streamlit → authenticated HTTPS/JSON → .NET 10 Research API → validated historical data. Do not reopen interactive terminal/image-attestation diagnostic gates.

## Known baseline
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`; WP14 PR #406 merged as `d28cf8afce6d88430598d99fb39cf78d57d8d082`.
- Existing F1 research App Service: `aiqr115researchstagingwus27f5eabb5`; resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`; existing F1 plan only.
- UI App Service: `aiqr112wp05wus27f5eabb5`; UI was not modified during WP15.
- WP15: approved images published and registry-verified; research `/health` request timed out; no authenticated snapshot checks or browser validation. Research original configuration and Running state independently restored. No provider requests/imports; all 35 owner files preserved.
- Research API currently uses port 52731, with platform HTTPS termination and an explicit bind-address configuration. Existing exact-site ephemeral SQLite guard remains fail-closed.
- `AUTHENTIC_DATA_NOT_IMPORTED`; `PUBLIC_RUNTIME_VERIFIED=False`.

## Authorization and non-negotiable limits
**Read-only Azure inspection is authorized by the user's agreement to WP16.** Do not modify Azure resources or state: no deployment, image publication, restart, stop, configuration change, log setting change, remote command execution, live probe/HTTP request, credentials retrieval, or provider calls. Do not touch UI service. If read-only Azure access is unavailable, work from existing local evidence and report gaps. Do not presume existing historical diagnostic approval applies to another attempt.

Preserve the owner working tree, including dirty/untracked files, using isolated branch/worktree and verify their status/hashes before and after. No new paid resources, licensing approval gates, or synthetic fallback. Do not expose keys, tokens, full environment settings, private paths or private Azure details in PR text or logs.

## Investigation sequence
1. Confirm repository `main` baseline and review WP15 deployment record/runbook and relevant research Dockerfile, .NET startup code, `ResearchSnapshotEndpoint`, `ResearchStorageBoundary`, transport settings, and deployment tooling. Record exact image digests only in restricted evidence if operationally sensitive.
2. Collect **existing** Azure App Service startup/container logs and platform events via read-only operations only. Check whether the log records actually correspond to the WP15 attempt; identify timestamp, instance and image correlation limits. Never enable logging or stream an interactive shell as part of this task.
3. Inspect **current** read-only app metadata and, if accessible, existing historical deployment/configuration evidence. Because rollback is complete, current settings may not represent the failed attempt. Compare against the privately preserved WP15 baseline/attempt records without printing secrets. Do not treat SCM authentication as proof of process/image execution.
4. Examine startup and routing hypotheses separately: container entrypoint/architecture, process exit, ASP.NET/Kestrel binding (`0.0.0.0` vs loopback), `WEBSITES_PORT` and port 52731, App Service platform HTTPS termination, startup readiness delay, exact-site ephemeral guard, permissions/UID/mount, and missing SQLite behavior. Distinguish direct evidence from inference and unknowns.
5. If a deterministic defect is demonstrated in code or deploy configuration, implement the smallest safe fix **offline**, with focused automated tests and rollback notes. Preserve strict guard/authentication, no automatic database creation and no provider fallback. Open a narrowly scoped PR; leave it OPEN/UNMERGED. If evidence is insufficient, do not invent a fix or open a documentation-only PR just to report a blocker.
6. Report a concise root-cause matrix with supporting evidence, confidence, alternative explanations, and the single smallest proposed next action. If another live deployment or smoke would be required, draft its exact scope for owner approval but **do not execute**.

## Checks and acceptance
- Run relevant offline .NET/Python tests for any changes, PowerShell syntax checks if scripts change, shell syntax if relevant, UTF-8/LF, `git diff --check`, and verify owner-file preservation.
- Confirm whether the original research app remains Running **using read-only metadata** if accessible. Never claim the public chart works or that F1 runtime is qualified based on offline tests or `/health` alone.
- Deliver: investigation summary, evidence source/time limitations, actionable diagnosis or explicitly unresolved cause, changed-file/test inventory (if any), PR URL (if justified), and precise proposed authorization boundary for any future Azure operation.

## Stop conditions
Stop immediately if the only way to continue is an Azure mutation, remote command, unauthorised HTTP probe, secret exposure, provider request, or risk to the restored app. Preserve rollback state. Do not bypass fail-closed checks to obtain a green health result.
