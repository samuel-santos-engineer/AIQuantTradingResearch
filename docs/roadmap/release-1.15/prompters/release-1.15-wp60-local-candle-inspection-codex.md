# Codex prompt — Release 1.15 WP60: Local historical candle inspection

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

## Confirmed predecessor

PR #448 was squash-merged to `main` at `322d21f717ac010136f378c437ab600cf79b7251`. WP59 provides browser-qualified inclusive UTC viewport navigation on the existing BTC/USD and ETH/USD 1,000-candle snapshots. Start by fetching/verifying current `origin/main`, work in an isolated branch, and inspect the repository's work-package numbering/conventions before finalizing the WP60 label; if WP60 is already assigned, select the next unused local work-package identifier and explain the adjustment. Do not incorporate open PRs #445, #446, or #447.

## Objective

Implement the next small, user-visible local research enhancement: a **read-only candle inspection panel** for the historical chart. Let a user select an available UTC candle and inspect its actual timestamp, OHLC prices, available volume status, and API-supplied SMA20 at that candle, without fetching additional market data or modifying any research calculations. The panel must respect the currently selected BTC/USD or ETH/USD snapshot and work alongside WP59's range navigation, presets, panning, and Return to Live.

## Implementation guidance

1. Inspect the merged chart's existing Streamlit/Plotly component contract and snapshot DTO. Prefer existing supported Streamlit controls (e.g., a UTC candle selector scoped to the validated loaded snapshot) over adding a new event channel, JavaScript package, or dependency. Preserve chart behavior and avoid destabilizing gesture reconciliation.
2. Show only data actually present in the validated snapshot: exact UTC timestamp, open/high/low/close, and the corresponding SMA20 value if supplied; explicitly say unavailable for null SMA20 warmup or null volume. Do not compute SMA20 in Python or synthesize volume. Do not invent candles across gaps.
3. Keep selection per instrument, with safe reconciliation when snapshots refresh or instrument switches. If the selected timestamp disappears, reset to a valid available candle and disclose the change. Handle empty/unavailable snapshots gracefully.
4. Do not mutate DTOs, stored candles, indicators, imported SQLite, or API state. Keep display rounding separate from numeric values. Avoid presenting this as real-time trading or advice.
5. Add focused fake-only tests for BTC and ETH, first/last candle, SMA20 warmup, null volume, missing/empty data, invalid/stale selection, snapshot refresh and instrument switching. Retain WP59 regression coverage.
6. Use the existing local two-container workflow, retained authentic imported volume, service key, and currently valid TLS pair. Do not assume the seven-day certificate is still valid; preflight validity and stop with an actionable local-only report if expired. Never expose secret or private data in logs/PR.
7. Build a new local UI image from the qualified Dockerfile, with a tightly audited public build context and cached dependencies; do not rebuild the API or overwrite the historical accepted images. Qualify with real browser interactions, BTC → ETH → BTC, candle inspection, navigation coexistence, SMA20/null-volume invariants and readiness. Preserve rollback ability and imported volume. If local runtime prerequisites are unavailable, report NOT_RUN instead of claiming browser acceptance.

## Explicit boundaries

Local-only. No Azure calls, quota checks, registry publication, production changes, provider requests, new historical acquisition, new instruments, storage migration, new dependencies, API/schema modifications, or deployment. No merge. Do not modify or merge PRs #445/#446/#447. Keep the new change narrowly scoped; don't introduce a broad analytics dashboard, new indicators, trading signals, or speculative architecture.

## Deliverables

- Focused UI implementation and regression tests.
- Concise local runbook/evidence under the existing Release 1.15 documentation conventions, with actual commands and PASS/FAIL/NOT_RUN outcomes.
- A new GitHub PR against current `main`, initially draft until actual local browser acceptance passes; then leave OPEN/UNMERGED for owner review (mark ready only if checks pass).
- Report exact changed paths, test counts, image ID, preserved runtime assets, browser evidence, known limitations, and PR URL.

## Acceptance criteria

Authentic retained BTC/USD and ETH/USD snapshots each have 1,000 candles; selected candle values match the authenticated DTO; SMA20 is displayed from the supplied series (19 null warmup / 981 populated values), null volume remains unavailable; WP59 range controls and Return to Live continue to work; local browser acceptance passes without changing API or imported data. `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` remain unchanged because Azure is deferred.

Begin with source inspection and a short implementation plan, then execute the bounded local work if the prerequisites are satisfied. Do not impose additional per-command approval gates for ordinary local development and tests; stop before any unexpected destructive action or scope expansion.
