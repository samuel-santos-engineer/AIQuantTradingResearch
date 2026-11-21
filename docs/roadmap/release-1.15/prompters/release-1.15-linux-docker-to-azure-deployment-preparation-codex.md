# Codex prompt — Release 1.15: Linux-first Docker-to-Azure deployment preparation

You are working in `samuel-santos-engineer/AIQuantTradingResearch`. The owner wants to move from offline WP62 helper work toward getting the **currently working Docker images** running in Azure, with minimal new governance. Do not conflate local success, registry publication, Azure configuration, and public runtime acceptance.

## Current baseline

- `main` includes merged PR #455 (`2ab325e9e0a1fbdb8b3a8a4266de6ee30f0a6fa0`) and merged PR #456 (`7ddcbe8f4bff4ea2fbfac8b9728b32051c5c690d`). Refresh `origin/main` and verify actual state before editing.
- Research API and Streamlit historical chart for BTC/USD and ETH/USD have a locally accepted Docker-first / dual-process architecture. Preserve both.
- Existing Azure research app: `aiqr115researchstagingwus27f5eabb5`; UI app: `aiqr112wp05wus27f5eabb5`; F1 plan: `asp-aiq-r112-wp05-wus2-7f5eabb5`; resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`; subscription: `7f5eabb5-80f5-4369-88be-b14a6de763f1`; region West US 2. Treat these as historical references to verify, not authorization to contact Azure.
- A prior research diagnostic candidate was published to a registry as OCI index `sha256:153d2b1ae5e2b856c1ca502849c05fe04ec09950b453b7b8b7d4b47f1dc7205f` and linux/amd64 child `sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c`. Earlier configured research digest was `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`. **Do not assume any of these is the currently locally accepted image or the right deployment target.** Resolve actual image repository and digest from authoritative project evidence.
- F1 stop quota has previously been exhausted and later reported 0/15; quota status is not currently established. Offset-free reset timestamps have caused ambiguous results. Do not infer safety from old readings.
- WP62 helper in `eng/azure-cli/r1.15-qualification/wp62_staging_observation.py` is Windows-specific and **live blocked** (`LIVE_ADAPTER_QUALIFIED=False`). Do not enable it, port it blindly, or require Windows ACL qualification for Linux deployment. On Linux, if private local files are necessary, use restrictive ownership and `0700` directories / `0600` files, verified before writing sensitive content.
- Preserve `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` unless a separately authorized and genuinely completed public acceptance justifies changing them.

## Mission and limits

Produce an implementation-ready, evidence-based **Linux-first deployment plan and bounded operator procedure** for the current working research API and chart UI Docker images. Where possible, prepare safe offline scripts/configuration/tests and a reviewable PR. Focus on getting the existing images into Azure, not redesigning the application or adding approval bureaucracy.

**This task does NOT authorize Azure CLI/ARM/SCM/network calls, registry pushes/pulls, deployment, configuration writes, restarts, resource creation, secrets access, production changes, or public endpoint probes.** Do not perform any of those. No merge without explicit owner instruction. Offline local Docker inspection is allowed if images and Docker are already available; do not rebuild, run migrations, or alter retained volumes. Do not expose secret values or private paths in output. If an essential step requires live access, document its exact command and effects as a proposed, not executed, operator action.

## Required work

1. **Establish baseline and provenance.** Inspect repository manifests, Dockerfiles, compose files, CI publication workflows, release evidence, ports, startup commands, environment-variable names (never values), storage mounts, TLS/authentication boundaries and network connectivity assumptions. Distinguish locally accepted research API and chart images from diagnostic candidates and previously configured Azure images. Record immutable digest, architecture and source commit only where verified; otherwise label `UNKNOWN` with a precise verification method. Do not fabricate image tags, registry URLs, credentials or current Azure state.
2. **Resolve deployment topology.** Determine whether the existing Azure Linux App Service arrangement supports the API and UI as separate existing web apps on the F1 plan. Preserve existing app names and resources where supported. Explain HTTP ports, startup, container registry authentication, API-to-UI routing and TLS implications. Prefer the smallest change set compatible with Docker-first policy; avoid rebuilding unless demonstrably necessary.
3. **Prepare a Linux-first, fail-closed operator runbook.** Explicitly separate: (A) local/registry image provenance verification; (B) read-only Azure preflight (resource identity, F1 quota, currently configured image/startup, app health, registry access, settings *names* only); (C) exact research image configuration change; (D) bounded research startup verification; (E) UI deployment only if research acceptance passes; (F) authenticated BTC/USD and ETH/USD chart acceptance; (G) conditional rollback/recovery. Each live action must have an exact target, expected response, bounded time/attempts, and stop condition. Never treat configuration readback or generic log markers as proof that a specific image is running. Do not assume a restart is harmless or free under F1.
4. **Protect state and secrets.** No new data import, volume deletion, secret disclosure, TLS/key replacement, database mutation or changes to existing retention. Specify private baseline capture before any proposed write; on Linux use restrictive permissions, verified before private content is stored. Include an uncertainty ledger and manual recovery when quota, drift, readback, attribution or restoration is uncertain. Avoid using the Windows-only helper as the Linux execution path.
5. **Define bounded acceptance.** Distinguish registry digest verification, Azure config readback, container start identity, authenticated API readiness, UI connectivity and browser-observed historical candles/navigation. Require direct evidence for BTC/USD and ETH/USD. Keep public runtime status false until qualified end-to-end checks genuinely pass.
6. **Offline validation.** Add narrowly scoped tests for any new scripts: plan-only default, no network/CLI calls in default mode, allowlisted targets/operations, no secret echo, quota/drift fail-closed, bounded subprocess cleanup, Linux private-permission checks, image digest matching, and rollback uncertainty. Run applicable local checks; report pass/skip/fail precisely. If unable to run Linux or Docker tests, state so rather than claiming success.
7. **GitHub delivery.** Create a focused branch/PR with only necessary runbook, helper and tests. Preserve prior Release 1.15 evidence and chronology. Avoid changing existing merged evidence unless necessary and justified. PR must remain open/unmerged for owner review. Include exact changed paths, test commands/results, verified vs unknown facts, and explicit list of live actions still awaiting authorization.

## Key decision points to surface (do not silently choose)

- Which immutable digest corresponds to the latest locally accepted research API image? Which to the chart UI?
- Are the existing two Azure web apps configured for separate Linux containers, and what are their actual configured ports/startup commands?
- Is registry access already configured for both apps, and can the image be fetched without new credentials?
- Is F1 quota currently safe for a single bounded startup, and what is the recovery option if quota is exhausted after a write?
- Can a running image be independently bound to the expected digest without relying on generic log text?

## Completion response

Provide: (1) concise findings and unknowns; (2) chosen minimal deployment topology; (3) PR link/head/changed paths; (4) offline validation results; (5) proposed exact first live operation and stop conditions **without executing it**; (6) remaining risks and required owner decision. State explicitly **ZERO AZURE OPERATIONS** and preserve `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.
