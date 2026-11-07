# Codex execution prompt — Release 1.15 WP17

## Objective
Execute **one** carefully bounded, research-only staging redeployment to determine whether the already-published .NET research API responds to HTTPS `/health` after Azure F1 cold startup. Use merged WP16 readiness logic. Do not revisit the separate interactive-container attestation workstream.

## Repository and baseline
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Start from `main` including merged PR #407, squash commit `509e1f37d7414cfbd5fe6d1309110e625f4d7114`.
- Preserve all owner dirty/untracked files and local working state. Use an isolated worktree if needed.
- Existing Azure F1 resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`; research app: `aiqr115researchstagingwus27f5eabb5`; UI app: `aiqr112wp05wus27f5eabb5`.
- Existing F1 plan only. No paid infrastructure.
- The WP15 original research configuration and Running state were restored and independently verified; treat that restored state as the starting baseline, but independently recheck before any new attempt.

## Authorization boundary — mandatory
This prompt **does not itself authorize a live Azure mutation**. Prepare and validate the exact one-attempt procedure offline, then request explicit owner authorization for the identified immutable research image digest, precise staging mutations, health probes, authenticated snapshots, observation budget and rollback. Do not infer authorization from previous WP15 or WP16 approvals; those were consumed or read-only. Until approval, do not publish, redeploy, restart, stop, change configuration, make live application probes, access credentials or run provider/import operations.

If the owner subsequently grants the exact live scope, perform only the approved research-app attempt, following the gates below. Never modify or probe the UI app in WP17. No rebuild/push: reuse the **already published, registry-verified research image** only after privately confirming its exact digest and linux/amd64 identity. Do not expose secret values or sensitive settings in PRs, logs or output.

## Before requesting approval
1. Read the merged WP14–WP16 implementation, the existing deployment/rollback runbook and the WP16 readiness collector. Avoid new governance or planning-only PRs.
2. Confirm current Azure target identity and restored research-app state with read-only management queries, if those queries are separately authorized; otherwise specify exactly which observations are needed. Do not assume WP15 settings scratch files reflect the attempted environment.
3. Define a restricted-access baseline capture covering the exact image/startup command, all relevant app settings (values stored privately), HTTPS-only, logging, slot metadata and running/stopped state. Define independent comparison after rollback.
4. Confirm the reviewed research image digest, startup/entrypoint, port `52731`, all-interface binding, `WEBSITES_PORT` and platform HTTPS termination settings. Keep the exact-site ephemeral storage guards, UID/mount/role restrictions and authentication unchanged. Do not weaken a guard merely to obtain a successful health response.
5. Prepare a precise operator checklist for a **single** research-only deployment and failure recovery. Specify a rollback owner and an independent stop/rollback mechanism. A control-plane Stop acknowledgement is not a kernel termination guarantee.
6. Run focused offline tests for the readiness collector, PowerShell 5.1 syntax and any touched code. If a concrete code defect is found, prepare a focused PR for review rather than silently changing the live attempt.

## One live attempt — only after explicit owner approval
1. Capture and privately preserve the verified baseline before any mutation.
2. Apply only the approved research image and strictly required protected research configuration; preserve unrelated settings. No new image publication or provider access.
3. Begin WP16 bounded anonymous HTTPS `GET /health` observation immediately. Maximum **three** attempts, at most **60 seconds each**, within **180 seconds** overall; the owned local probe child has a **190-second** parent limit. Accept only exact HTTP 200 and `Alive`. Do not count an Azure management Running status as process health.
4. On health failure, stop the attempt and restore the exact original research configuration and original Running state. Independently verify restoration. No second deployment attempt, no changed timeout, no guard bypass.
5. **Only if health succeeds**, execute exactly one unauthenticated `POST /v1/research/snapshot` expecting a validated 401, and exactly one authenticated snapshot request each for BTC/USD and ETH/USD, using the existing schema and strict HTTPS client. Handle API keys only in protected server-side/private channels, never command-line arguments, browser requests, screenshots or public logs. If no safe private credential delivery exists, stop before authenticated checks and report the limitation.
6. Interpret authenticated `503 DATASET_NOT_READY` as successful HTTP communication with absent private dataset, **not** chart acceptance. Invalid envelope, guard rejection, authentication failure or timeout is a failure. Do not auto-create SQLite or seed fixtures.
7. On any failure or incomplete rollback, preserve sanitized evidence, independently restore/compare baseline, and report exact residual differences. On success, report the approved end-state and whether it is intentionally left deployed; do not assume permission to leave changes in place if approval specifies rollback.

## Explicit exclusions
- No UI image/configuration changes or browser acceptance.
- No Twelve Data/provider requests, credentials discovery, historical import, synthetic candles or automatic initialization.
- No production changes, paid resources, repeated deployment attempts or diagnostic-controller bypass.
- No merging PRs without owner instruction.
- Do not claim `PUBLIC_RUNTIME_VERIFIED=True` from `/health`, empty datasets or local tests.

## Deliverables
- A concise WP17 operator decision record: verified baseline, exact artifact/digest, approval scope, observed timestamps and outcomes, and rollback verification (or a precise reason the attempt remains blocked).
- Sanitized, attributable evidence distinguishing process health, authentication, dataset readiness and public chart acceptance.
- If a concrete implementation defect is discovered, a focused tested PR left open for review; otherwise do not create a documentation-only PR just to record a blocked attempt.
- Explicit final status: `RESEARCH_PROCESS_ALIVE`, `AUTHENTIC_DATA_NOT_IMPORTED`, `PUBLIC_RUNTIME_VERIFIED=False` as applicable. Never claim process alive without a real valid HTTPS response.

## Success criterion
A single safely authorized attempt establishes whether the research service answers authenticated HTTPS correctly on the existing Azure F1 staging service after cold startup, without risking the restored UI or introducing false chart acceptance claims.
