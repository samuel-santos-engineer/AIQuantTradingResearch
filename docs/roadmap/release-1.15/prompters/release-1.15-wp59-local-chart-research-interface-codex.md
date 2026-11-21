# Codex execution prompt — Release 1.15 / WP59: Local Historical Chart and Research Interface

You are working in `samuel-santos-engineer/AIQuantTradingResearch`. The owner has explicitly chosen to **continue locally** while existing Azure Linux F1 staging is quota-blocked. Prioritize a working local BTC/USD and ETH/USD historical chart/research interface, not cloud diagnostics or deployment.

## Established baseline and boundaries

- Previously accepted local pair: Python 3.13 Streamlit/Plotly UI and .NET 10 research API, separate Docker containers, API-owned ephemeral SQLite state, authentic retained BTC/USD and ETH/USD historical observations. Historical local browser BTC→ETH→BTC passed with 1,000 candles per instrument and SMA20 (19 warmup nulls, 981 computed values); unavailable volume remains null.
- Accepted historical local image IDs: UI `sha256:3270ce6ec33ba8cb7da27738ea6d9dc27a811a3270e7ecdef83f32e757a83ae1`; API `sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028`. These are **local IDs**, not assumed OCI registry manifest digests. Verify current local availability and image contents before use; do not assume they persist.
- PRs #445 (Azure qualification), #446 (unmerged initializer), #447 (partial Azure deployment evidence) remain open/unmerged. Do not merge, rebase, overwrite, or silently incorporate #446's implementation. Confirm live repository state first.
- Azure research app has accepted API image configured but is `QuotaExceeded`; UI was not promoted. Do **not** run Azure CLI, Azure HTTP probes, registry pushes, cloud actions, quota monitoring, or cloud changes in WP59. No production action.
- No new paid resources, providers, market-data acquisition, licensing prerequisite, persistent cloud storage, or fabricated candles/volume. No credentials, private observations, or DB content in Git, PR, images, logs, or test fixtures. Do not bake SQLite/data into images.
- Preserve Docker-first local execution, with dual-process local development supported where appropriate. Keep the research API as the data/indicator authority and UI as presentation; avoid unnecessary infrastructure redesign.

## Goal

Deliver a small, demonstrably usable local historical chart/research increment on top of the accepted pair, with repeatable startup, improved historical navigation, and testable behavior. **Local acceptance—not Azure readiness—is the WP59 gate.**

## Execution plan

### 1. Inventory and establish baseline

- Inspect `main`, current branches, existing roadmap/governance, relevant Dockerfiles, bootstrap scripts, UI and API source, tests, local compose/run helpers, and current PR state. Identify the exact canonical WP59 work-package naming and governance conventions; do not assume the suggested WP number conflicts with existing roadmap work.
- Check local Docker availability and accepted image IDs, ports, expected environment keys (names only), and safe access to retained local historical observations. Do not print secret values or dataset contents.
- Run the smallest existing local smoke checks where feasible. Report `PASS`, `FAIL`, `BLOCKED`, or `UNKNOWN` accurately; distinguish historical evidence from tests executed now. If images/data are absent, document that and proceed with code/tests using safe fakes, without inventing data or reaching external providers.

### 2. Implement a minimal local-first increment

Prefer existing application code and small changes over new layers. Implement, subject to code inspection:

- A repeatable local two-container startup path with explicit API/UI connection configuration, predictable port mapping, documented prerequisites, readiness ordering, and clear failure messages. Keep any private dataset initialization as a **separate, local-only** operator step; do not imply a missing data-transfer mechanism exists.
- Historical chart date-range selection or an equivalent bounded navigation control that works with the API's actual available snapshot contract. Do not claim arbitrary ranges if API currently supports only fixed snapshots; if necessary, add the smallest compatible, validated read-only API change and tests. Prevent misleading partial ranges, out-of-order candles, or indicator misalignment.
- Preserve BTC/USD and ETH/USD switching, genuine OHLC, SMA20 warmup/null semantics, null volume behavior, and clear empty/unavailable-data states. Do not add indicators, instruments, streaming, trading actions, or authentication features outside this scope.
- Keep UI responsive and understandable with concise instrument, range, candle-count and data-status cues. Prefer a single useful end-to-end feature over a broad refactor.

### 3. Validate locally

- Add targeted tests for any API range handling, date validation, candle ordering/count, SMA20 alignment, and empty/missing-data states; use fake/nonprivate test data.
- Build replacement **local** images only if changes require them. Preserve accepted historical images as rollback/regression baseline. No push or Azure deployment.
- If Docker and retained authentic data are available, perform a bounded local end-to-end run: API start, UI start, browser BTC→ETH→BTC, date-range change, 1,000-candle baseline when full range selected, SMA20 19-null/981-computed full-range invariant, null volume, and error/empty-state check. Record actual evidence without exposing dataset or secrets.
- Stop on unexpected mutation, missing safe private-data access, failed test, or environment problem; diagnose narrowly rather than running repeated restart loops.

### 4. Deliver reviewable change

- Create a focused WP59 branch and PR for owner review, following existing repository conventions. Do not merge.
- Include a short local runbook, precise changed-file inventory, test commands/results, screenshots only if safe and relevant, limitations, and a clear separation of historical WP57 acceptance versus new WP59 evidence.
- Record Azure staging as **deferred and quota-blocked**, not a local release blocker. Keep `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` until independently proven; do not falsely update public readiness.
- If implementation cannot safely proceed due to missing prerequisites, produce a small reviewable plan/PR with exact blockers and the single next local action; never invent PASS results.

## Acceptance criteria

1. Existing accepted image pair remains identifiable and untouched.
2. Local two-service launch is reproducible and documented.
3. BTC/USD and ETH/USD historical chart works locally with a useful, validated range/navigation improvement.
4. Authentic OHLC and SMA20 behavior remain correct; unavailable volume is not fabricated.
5. Targeted tests pass, and actual browser/container verification is recorded when feasible.
6. No Azure or production operations, no private dataset/secret leakage, no silent incorporation of unmerged PRs.
7. Owner receives a PR link, commit SHA, changed-file list, actual verification matrix, and remaining local limitations.

Begin with the repository inventory and the smallest safe local implementation. The owner has authorized local milestone work, **not** Azure activity, merging PRs, or exposure of private observations.
