# Codex prompt — Release 1.15 WP61: Local historical candle comparison

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Baseline: `main` after squash merge of PR #449, commit `0cfb4886db626aab7487ce3ac7794e6d818e5fac`.

## Objective

Extend the locally qualified BTC/USD and ETH/USD historical chart with a small, read-only **two-candle comparison** workflow. Users should select two available UTC candles within one instrument's validated 1,000-candle snapshot and inspect their exact API-supplied OHLC and SMA20 values side by side, plus explicitly labeled price differences and percentage changes computed only for presentation. Do not introduce provider requests, new backend indicators, trading signals, or claims that transformed educational values are actual quotes.

## Start by verifying

1. Fetch `main`, confirm PR #449 is merged and the baseline SHA, and inspect repository work-package numbering and existing governance. If WP61 conflicts with an existing work package, stop and report; do not overwrite it.
2. Inspect `python/presentation/research_chart.py`, `test_research_chart.py`, existing snapshot client, WP59/WP60 evidence and the supported local Docker launcher. Use the merged code, not unmerged PR #445/#446/#447.
3. Confirm the retained local API image, authentic BTC/ETH data volume, service key and current TLS validity before any container replacement. The seven-day TLS certificate is temporary; never regenerate credentials or replace imported state as a shortcut.

## Implementation scope

- Add a native Streamlit comparison control near WP60 candle inspection, scoped to the currently validated snapshot and current instrument.
- Provide independent A and B UTC timestamp selectors. Default to two available candles, handle one/zero candles truthfully, and retain independent selections for BTC and ETH across switching and refresh.
- Show exact supplied values for each candle (UTC, open, high, low, close, volume, SMA20). Nulls must remain explicitly `Unavailable`; do not fabricate volume or SMA warmup.
- Calculate *presentation-only* close-to-close absolute and percent difference using `Decimal` from validated close strings. Clearly label direction as **B minus A**, define the denominator as A close, and show `Unavailable` when inputs are absent or A close is zero. Avoid float rounding or modifying API DTOs. Preserve exact input strings; display computed deltas with a documented, consistent decimal formatting policy.
- Reject or reset stale/pruned selections with visible explanation; never compare candles from different instruments, snapshots, or incompatible identities. Keep the comparison independent of viewport zoom and WP60 single-candle inspection.
- Keep existing instrument switching, bounded/single/full range navigation, presets, pan/zoom, Return to Live, inspection, gaps and refresh behavior unchanged.
- Prefer small changes to existing presentation and test files. Do not modify API, schema, provider, Dockerfile, dependency pins, or architecture without a demonstrated necessity; stop and explain if such a change appears required.

## Tests and acceptance

1. Add focused fake-only tests for both instruments, first/last/same-candle/reversed selections, missing SMA20, null volume, zero denominator, precise decimal arithmetic, empty/pruned refresh, instrument independence, no DTO mutation and no viewport mutation.
2. Run existing chart/client regressions and report exact test counts, exit codes and warnings. Preserve all WP59/WP60 cases.
3. Build **only** the new UI image locally using the supported Dockerfile, cached pinned dependencies, audited public build context, Linux/amd64 and no network/pulls where feasible. Do not overwrite the accepted historical, WP59 or WP60 images.
4. Preserve the accepted API image, imported volume, service key and valid TLS. The supported launcher may recreate both containers; distinguish container identity from unchanged image/volume identity. Capture a private rollback baseline and avoid publishing private paths, credentials, certificate material, DTO values or raw screenshots.
5. Perform a real browser test at the loopback UI: BTC -> ETH -> BTC; compare A/B candles and verify side-by-side exact DTO fields and computed differences against independently computed in-memory expectations; exercise WP59 range navigation and WP60 inspection concurrently. Test null-volume/SMA warmup disclosure and refresh. Use real controls, not injected Streamlit state.
6. Preserve the retained authentic dataset; use fake-only tests for destructive empty/pruned scenarios. Report NOT_RUN rather than inventing runtime evidence.

## GitHub deliverable

Create a focused **draft PR** for WP61 against the verified merged `main`, with only implementation, tests and `docs/roadmap/release-1.15/prompters/release-1.15-wp61-local-candle-comparison.md` evidence. Record exact source SHA, new local UI image ID, test outcomes, browser acceptance and limitations. If local browser acceptance passes, mark ready for owner review; **do not merge**. Ensure UTF-8/LF/no BOM and `git diff --check`.

## Boundaries

**Local-only.** No Azure calls, quota investigation, registry publication, production changes, paid infrastructure, provider acquisition, new market-data source, private-data transfer, reimport or unrelated refactoring. Leave PRs #445/#446/#447 unchanged and open. Keep `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` unless separately proven by actual public deployment (not in scope).

If a prerequisite fails, stop safely, preserve the existing working chart and report the smallest actionable blocker. Do not restart-loop or erase existing state.
