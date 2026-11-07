# Release 1.15 — WP41: Controlled authentic acquisition and local verification

## Baseline and mission

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Start from current `origin/main`, containing squash-merged PR #428 at `7c92eb98b8e9fe7d204375cf43c3e11f3f68a5e7`. Verify HEAD, clean isolated worktree, current governance, WP39 runbook and WP40 runbook before changing anything. Do not overwrite the owner's working tree. Build on the existing WP39 Python/PowerShell operator workflow, C# Twelve Data adapter, sanitized diagnostic contract, SQLite importer, authenticated local two-container runtime, and Streamlit historical chart. Do not invent a new architecture or licensing gate.

Objective: obtain attributable results from **at most one newly owner-approved live acquisition invocation**, then, **only if successful**, import authentic BTC/USD and ETH/USD observations and verify the existing local Docker research/chart path. Produce a reviewable PR with honest evidence. **This prompt itself is not authorization to call Twelve Data.**

## Phase A — Offline-only preflight (authorized by this prompt)

1. Confirm PR #428 is merged and inspect the current WP40 preparation evidence. Reconcile any branch changes before proceeding.
2. Validate local prerequisites without any provider/DNS/HEAD/HTTP probes: toolchain, cached dependencies/images, source/build identity, private preparation directory and ownership, session labels, absent destination bundle/receipt/SQLite/qualification, and the TLS certificate's actual validity at execution time. The WP40 certificate was recorded as expiring `2026-11-08T19:12:34Z`; never assume it remains valid. If necessary, run a **fresh offline Prepare** in a new owner-private temp directory; never overwrite an old attempt or private artifacts.
3. Re-run focused fake-only C# acquisition/initializer tests, Python WP39 diagnostic/workflow tests, and chart/client regressions; check UTF-8/LF/no BOM and `git diff --check`. Reconfirm missing-approval produces `MISSING_APPROVAL`, `requestsStarted=0`, `responsesReceived=0` through the freshly built Worker and strict Python parser. No real credential access in this phase; presence-only credential-setting check is permissible without reading the value.
4. Document the exact prepared artifact/image identity, ownership, expiry, test results, and no-request evidence in a new WP41 runbook. Never commit private absolute paths, secrets, environment dumps, provider responses, private database, TLS key or provider bundle.
5. **STOP and present an approval checkpoint**. Request explicit owner authorization for exactly one live Acquire invocation. Do not interpret this prompt, previously consumed approval, the presence of a key, an environment flag, or a CLI switch as authorization. If approval is absent, finish with status `AWAITING_NEW_OWNER_PROVIDER_APPROVAL`; do not invoke Acquire.

## Phase B — Only after a separate explicit owner approval

Scope of the approval to request: exactly one invocation of the existing WP39 Acquire action; at most two sequential HTTPS GET requests to `https://api.twelvedata.com/time_series`, first BTC/USD then ETH/USD, `interval=1min`, `outputsize=1000`, `timezone=UTC`, `order=ASC`; accept only validated completed observations within the existing latest-24-hour window and 15-second grace. No retries, pagination, fallback data, capability/account probes, additional endpoints, Azure calls, deployments or registry publication. Credits actually consumed are unknown unless independently established by authorized evidence; do not infer from started requests.

Before executing, recheck fresh build/manifest identity, private directory ownership, unexpired TLS, no existing bundle, and no conflicting session. Use the already established owner-private Windows User credential setting through the approved transient process channel only, with restoration in `finally`; never print/read into evidence, store, or put the value in arguments, images, URLs, browser, logs or source. Base-asset volume units were owner-confirmed BTC/ETH; do not silently change this assumption.

Invoke Acquire **once**. Capture only the bounded sanitized diagnostic fields: `failedStage`, `instrument`, `httpStatus`, `requestsStarted`, `responsesReceived`, `outcomeUnknown`, `failureCode`, `bundlePublished`, plus child exit and presence-only artifact checks. Do not copy raw stdout/stderr, response bodies, exception text, secret material, or private paths into PR artifacts. If failure/unknown: stop immediately; no retry, no import, no chart start. Preserve private evidence and report uncertainty precisely, including unknown HTTP status, provider receipt, credits, or request count where applicable. Do not attempt to reconstruct the previous WP39 failure.

## Phase C — Conditional success path only

If and only if Acquire succeeds and a complete `ObservedProvider` bundle passes existing provenance/hash/UTC/OHLCV/volume/count/gap validation, run the existing `Import`, `Start`, `Verify`, and `Stop` actions in their documented order. Do not alter the importer to force success, relabel fixtures, or replace incompatible private state. Verify authenticated TLS snapshots for both symbols, generation/receipt/bundle identity, first/last timestamps, row counts, research continuity/readiness, and that public presentation remains transformed/educational rather than exposing raw private candles or secrets. Perform local browser checks for symbol switching, 100/300/1000 presets, zoom, Refresh retention, provenance labels and volume. Stop only owned resources; preserve private state for review. Do not claim public Azure qualification.

Only after the actual successful observed-provider import and verified local snapshot may the **local qualification artifact** record `AUTHENTIC_DATA_NOT_IMPORTED=False` according to the existing WP39 contract. If not proven, preserve `AUTHENTIC_DATA_NOT_IMPORTED=True`. `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` remain unchanged in either case. Do not conflate local qualification with Azure/public acceptance.

## Deliverables and review

Create a narrowly scoped WP41 PR against `main` and leave it **OPEN/UNMERGED**. Prefer documentation/evidence changes; modify production code only for a concrete, reproducible, separately justified defect and cover it with offline tests. Record exactly which phases ran, actual tests, the new approval decision (if any), sanitized acquisition outcome, artifact presence, import and chart evidence (if applicable), limitations and next action. Keep credentials and private artifacts outside Git. Never run Azure operations. Do not automatically trigger a second live attempt after any failure. Preserve the owner's original checkout and unrelated workloads.

Initial checkpoint: `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
