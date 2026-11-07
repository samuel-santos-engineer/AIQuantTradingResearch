# Codex execution prompt — Release 1.15 WP06 historical crypto chart

## Objective and baseline

Implement a usable Streamlit/Plotly historical candlestick interface for **BTC/USD** and **ETH/USD** using the merged WP05 authenticated Python snapshot client. Start from fresh `main` containing PR #392 squash merge `e7c102e4bf756db1ed18aa26776521c100dc263c`. Deliver a **functional implementation PR**, left OPEN for owner review; do not merge it.

The existing C# WP03 committed candles, WP04 research calculations and WP05 D4/D5 authenticated HTTPS endpoint and strict Python client are the data authority. Do not introduce a second data source, client-side provider calls, fake candles, browser-exposed API key, extra server, or redundant cache/transform logic.

## First: reconcile actual repository interfaces and scope

1. Read Release 1.15 governance, WP06 issue #372, WP05 client `python/presentation/research_snapshot_client.py`, current Streamlit entrypoint, existing chart conventions and tests, file manifest, and any pending PR #386/#387 overlap. Treat unmerged branches as unmerged; do not import them silently.
2. Inventory the original working checkout (HEAD, status, every dirty/untracked path, byte size and SHA-256) before writing. Preserve all owner files and all Codex prompt files byte-for-byte. Work only in an isolated worktree/branch from verified `main`.
3. Determine the **minimum exact paths** required for WP06, preferring existing Streamlit application files and narrowly scoped tests. Follow existing path-authority rules: if a required path is not authorized, request one concise explicit path approval before editing it. Do not create extra planning-only PRs or approval gates for settled design decisions.
4. Confirm the actual WP05 Python response schema and client method signatures. Use real fields; never guess an API shape. Do not silently change WP05 wire contracts.

## Functional requirements

- Provide an obvious **BTC/USD / ETH/USD selector** and a real **historical 1-minute OHLC candlestick chart** from the selected instrument's validated D4 full snapshot. Maximum 1,000 completed candles. Plot accurate UTC timestamps; do not invent missing minutes or draw misleading candles across gaps.
- Keep the public D5-transformed OHLC/price indicators together in the same display scale. Never show private untransformed prices, secret values, configuration, checkpoints, or private metadata. Display volume only if supplied and validated by the client.
- Offer a small, useful selection of research overlays/indicators (e.g. SMA20, EMA20, Bollinger bands, RSI14, MACD) from the **already calculated snapshot**, not locally recomputed values. Make classification/regime and meaningful events discoverable without overwhelming the chart. Avoid dozens of toggles or an elaborate dashboard.
- Respect null/warmup values, first valid 401st-candle research readiness, gaps, unavailable intervals, stale/partial status, and empty history. Explain these honestly in the UI; do not treat missing data as zeros.
- Keep server-side HTTPS and `X-API-Key` entirely in the Streamlit process, through the existing Python client. Use existing environment/config conventions, no browser-side calls or hardcoded credentials. Preserve TLS verification and fail-closed behavior.
- Correctly use WP05 `full` and `unchanged` responses, instrument-specific last-good state, and identity/date/epoch changes. Prevent old instrument responses from overwriting the selected chart. On transient failures, show a clear stale/unavailable indication rather than claiming current data. Use simple Streamlit refresh controls; do not add autonomous background loops.
- Make the interface useful at typical desktop width and readable on narrower screens. Keep plotting bounded and efficient for F1 hosting. Do not make external provider requests, activate ingestion, rotate D5 keys, or modify backend storage.

## Proof and acceptance evidence

- Offline deterministic tests for BTC and ETH selection, candle/UTC mapping, transformed indicator values, null warmup, gaps, readiness, empty/unavailable/stale/full/unchanged behavior, and no secret leakage. Prefer pure transformation/view-model functions to make tests cheap and repeatable.
- Exercise a local UI smoke path when available, using the real strict Python client with an offline fixture; never substitute invented production candles. If browser automation or dependencies are unavailable, document the exact limitation instead of claiming UI verification.
- Run relevant existing WP05 Python and WP06 tests, plus targeted regressions, with no unnecessary installs/upgrades. Record exact commands, exit codes, test totals, environment and any warnings. Do not claim Linux/Azure verification from Windows/offline tests.
- Validate only authorized changed files, UTF-8 without BOM, LF, final newline, `git diff --check`, and protected owner-file hashes. Update existing approved evidence paths only when authorized; otherwise put evidence in the PR body.

## Boundaries and deliverable

No production inspection or changes; no Azure, registry, staging deployment, provider activation, credential acquisition, package upgrades, Docker execution or resource mutation unless separately authorized. Preserve the agreed two-process/two-image Docker-first staging architecture; WP07 will qualify Linux and staging after WP06. PR #386/#387 are separate and must not be merged or copied without authority.

Open one focused WP06 implementation PR against current `main`, leave it **OPEN/UNMERGED**, and report exact changed files, tests actually run, functional demo status, remaining WP07 obligations and head SHA. Do not close WP06 issue or claim release acceptance merely because a PR exists. Keep `PUBLIC_RUNTIME_VERIFIED=False` until separately authorized staging verification succeeds.

**Priority: working historical chart, minimal changes, honest evidence.**
