# Codex prompt — Release 1.15 WP54: Controlled authentic Twelve Data acquisition

## Authority and baseline

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Start from clean `main` including PR #440 squash merge `0fcb75560e3841674316f1c666066e494550857a`. Use an isolated branch/worktree, preserve owner checkout, private artifacts, existing containers/images/volumes and untracked files. Review the merged WP39–WP53 implementation, especially `eng/docker/release-1.15/wp39_local_history.py`, WP50 diagnostic taxonomy, WP52 preparation identity guard, and WP53 qualification evidence.

**IMPORTANT: This prompt and PR #440 merge are NOT authorization for a live Twelve Data request, provider credential access/presence inspection, or any further Docker/resource operation.** Before executing any operational step beyond offline source/test inspection, obtain the owner's explicit authorization identifying the operation and its boundaries. In particular, **stop and request a separate explicit owner authorization for exactly one live acquisition attempt**. Do not treat prior WP47 approval as reusable. Do not make DNS/HEAD/probe/account/credit/support requests or inspect historical private provider responses as a workaround. Do not start an actual Acquire command unless this new authorization is granted. Do not use the Codex prompt itself as approval.

## Objective

Make real, measurable progress toward the local historical crypto chart by executing **at most one** separately authorized Twelve Data acquisition attempt through the existing guarded workflow, and reporting its sanitized result. The two instruments remain **BTC/USD then ETH/USD**, 1-minute historical authentic OHLCV with confirmed base-asset volume; Twelve Data is the sole provider. No provider migration, licensing gate, fake/zero-filled volume, quote/base conversion, OHLC-only downgrade, retry, fallback, pagination, or partial publication. Preserve existing UTC/ASC, outputsize 1000, <=24-hour observation window and 15-second grace, as implemented. Do not alter production acceptance rules to force success.

## Phase A — offline and no operational authorization needed

1. Confirm source baseline and inspect merged WP53 runbook. Identify the exact existing `Prepare`, `Acquire`, and diagnostic entry points without invoking operational commands. Review the preflight contract, identity manifest, TLS/ACL/destination checks, transient credential channel, and first-failure stop.
2. Run only already-supported **offline, cache-only** test commands if available without network, credential access or Docker; otherwise document the limitation. Do not install dependencies or touch private directories.
3. Write a concise, precise single-attempt execution proposal specifying which preflight/resource checks would be performed, which owned fresh preparation would be used, how credentials would be passed only via the existing transient private channel, what constitutes a single request attempt, what diagnostic fields can be reported, and what will be retained/cleaned up. Identify any need for separate Docker authorization. Present it to the owner and **STOP for explicit approval before Phase B**. If approval is not granted, leave a planning/evidence-only PR open, with no claim that a live attempt occurred.

## Phase B — only after separate explicit owner approval

1. Stay within the exact approval scope. In the owner-authorized context, perform immediate identity validation of the fresh WP53 preparation and matching loaded workflow/published Worker/context. Verify owned ACL, current TLS validity, absent bundle/receipt/destination conflict and required runtime prerequisites only if approved. Do not silently rebuild, refresh images, pull, deploy, inspect old preparations, or weaken permissions. If the retained WP53 preparation is not usable or its identity is stale, **STOP**; request new resource-preparation authority rather than bypassing the guard.
2. Only after preflight succeeds, use the established ephemeral private process channel for the Twelve Data key. Never print, echo, store in Git, pass in CLI args/URLs, include in logs, or reveal key presence/values beyond the minimum sanitized failure category. No credential reads before the identity guard.
3. Invoke the existing `wp39_local_history.py --action Acquire` operator workflow with its required approval/volume-confirmation flags, **once only**, targeting the verified preparation. Do not invoke the lower-level Worker directly, retry, run a second acquisition, or send a separate capability check. The workflow itself may request BTC/USD and, only after BTC succeeds, ETH/USD. Stop at the first failure. Do not infer provider charges from request/response counters.
4. Capture **only** the existing fixed ten-field version1 diagnostic envelope (version, event, failedStage, instrument, httpStatus, requestsStarted, responsesReceived, outcomeUnknown, failureCode, bundlePublished) or an equally sanitized success status. Never paste raw payloads, volume values, HTTP headers, private paths, keys, exception strings or provider response excerpts into GitHub/PR/console transcript.
5. If both authentic pairs succeed and an atomic bundle is published, label it **acquired, not imported**. Do not automatically Import, Start, Verify, deploy, browse or claim the chart works. Those operations require their own owner authority. If failure occurs, preserve only permitted private evidence and report the exact subtype and counters; do not attempt remediation with another live request.

## Tests, evidence and PR

- Keep any changes narrowly scoped. Avoid new architecture, unrelated documentation, provider research or redundant test matrices. Do not modify code unless a concrete issue is evidenced; if a fix is needed, implement it offline and obtain renewed approval before any live attempt.
- Record baseline SHA, offline test commands/results, exact authority received and its scope, fresh preparation identity validation outcome, and the sanitized acquisition result. If no authority is given, explicitly record `LIVE_ACQUISITION_NOT_AUTHORIZED / NOT_RUN`.
- Open a focused PR against `main` and leave it **OPEN/UNMERGED** for owner review. Never merge it yourself. Do not commit private preparation artifacts, credentials, historical data, receipts, TLS material or raw provider output.
- Preserve truthful flags: `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady` must remain false until preflight and explicit live approval actually establish readiness. A successful acquisition alone does not prove import or public runtime.

## Final operator handoff

Report what was verified, what was actually executed, whether the single live approval was obtained/consumed, the sanitized result, and the exact next owner decision. If a required authorization is absent, stop rather than stretching this prompt into operational permission.
