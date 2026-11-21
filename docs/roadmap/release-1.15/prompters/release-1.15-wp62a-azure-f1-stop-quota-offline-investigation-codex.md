# Codex task — Release 1.15 WP62-A: Azure F1 stop-quota recurrence, offline investigation

## Authority and current facts

Owner approved an **offline-first, read-only investigation only**. Do not treat this prompt as authority to run Azure commands, make HTTP requests to staging, access Azure logs or streaming, restart/redeploy, change configuration, rotate keys, import private data, query providers, pull images, or operate retained local runtime/data.

On 2026-11-15 at 01:30:33 São Paulo time (04:30:33 UTC), staging F1 **stop requests were 57/15**, after a temporary recovery at 00:05 São Paulo time (0/15). The earlier recovery was not durable. No health request, log stream, restart, configuration change or baseline preparation was performed during the stopped WP62 attempt. This is a **platform quota blocker**, not proof of application failure or of a key-related problem. Keep `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false`.

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Release 1.15 WP58–WP61 are merged. PR #446 merged as `fcbb54201e951a8dec5fe71a798aece8e5e22aa8`. PR #451 merged as `ca874bb7afb4a79011f621cbf5ca314af0f8018a`. Preserve the accepted historical BTC/USD + ETH/USD chart behavior, governance, and all newer evidence.

## Goal

Identify **what can and cannot be established from existing repository and previously recorded evidence** about the unexpectedly rapid accumulation of Azure F1 stop requests. Produce a ranked, falsifiable cause matrix and the smallest separately approvable future observation plan. Do not assert a root cause without evidence. No Azure operations are authorized in this task.

## Work sequence

1. Verify clean local checkout, current `main`, relevant branch/PR state, and latest Release 1.15 evidence without changing runtime. Use repository files and already available, nonsecret local evidence only. If remote GitHub metadata is needed, ordinary repository reads are allowed; do not trigger workflows or alter PRs.
2. Inspect only relevant tracked deployment/startup material: App Service configuration represented in source, Dockerfile(s), container entrypoints/bootstrap, worker/API startup/exit handling, deployment workflows/scripts, health-check/restart behavior, Azure staging runbooks, and WP58–WP62 qualification notes. Trace the sequence from process start to exit/stop and distinguish App Service platform stop counters from application-level restarts or process failures.
3. Build a **timeline** with explicit timestamps/time zones and source references: 00:05 quota 0/15 and 01:30:33 quota 57/15, plus only previously documented relevant events. Do not invent intervening events or assume 57 actual application crashes.
4. Build a **hypothesis/evidence matrix** covering at least: app process termination/startup failure, container entrypoint mode or missing private input, platform health/startup recycling, deployment/restart automation, scale or platform maintenance, misinterpreted quota semantics/time window, and configuration/key-related failures. For each, record supporting evidence, contrary evidence, missing observations, confidence, and one minimal falsification check. Treat keys as an unproven possibility, never a conclusion.
5. Search tracked CI/CD workflows and scheduled automation definitions for actions that could issue stop/restart/deploy calls; report exact trigger and path if present, but do not run or disable them. Distinguish repository automation from Azure-managed behavior and external automation not visible in the repo.
6. Propose a **staged, quota-conserving future diagnostic** requiring explicit separate approval: begin with metadata-only, non-mutating observations when quota permits, prefer a single bounded snapshot of existing quota/counter and relevant existing event metadata, no retries, no log stream, no HTTP health probes by default. Specify exact candidate commands/endpoints, maximum calls, timeout, redaction, abort conditions, and how each result would distinguish hypotheses. Do not execute it.
7. If the evidence supports a code/config defect, describe the smallest proposed fix separately, without editing or deploying application code. Do not invent new governance/licensing approval gates.

## Boundaries

- **Zero Azure CLI/ARM/portal/API operations**, including read-only ones, in this task. **Zero staging HTTP requests**, browser checks, log-stream sessions, restarts, stop/start, redeploys, key or app-setting reads, registry calls, provider requests, image pulls, container starts, private mounts, and retained-data operations.
- No changes to application source, tests, Docker files, workflows, Azure settings, secrets, credentials, local chart runtime, or historical evidence.
- Do not print or store secret values, tokens, private paths, dataset contents, connection strings, full environment dumps, or raw sensitive logs. Use names of configuration variables only where necessary.
- Preserve distinction between quota status, process health, authenticated API readiness, historical chart readiness and public readiness.
- Do not claim the F1 quota has recovered or that any application endpoint is healthy.

## Deliverable

Create or update a narrowly scoped Release 1.15 WP62-A investigation document under `docs/roadmap/release-1.15/` (prefer the existing prompters/evidence convention). Include:

- Timestamped known-facts timeline with source file/section references.
- Repository startup/stop and automation inventory, with precise paths.
- Hypothesis matrix, evidence gaps, and confidence levels.
- Quota-safe, **not executed** future diagnostic proposal with explicit limits and stop conditions.
- A clear `NO_AZURE_OPERATIONS_PERFORMED` statement and `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.

Validate Markdown formatting, UTF-8/LF/no BOM, `git diff --check`, and narrow changed-path inventory. Documentation-only commit/PR is acceptable if the checkout and repository workflow permit; **leave PR open and unmerged for owner review**. Do not rewrite or overwrite newer Release 1.15 evidence. If permissions prevent publication, report the exact local artifact and recommended manual next action.

## Final report format

Report: facts established; top hypotheses with confidence; what remains unknown; files changed; validation actually performed; whether a PR was opened/updated; exact smallest future operation requiring owner approval. Explicitly state that **no staging probe, restart, redeploy, Azure read, log stream, or configuration change occurred**.
