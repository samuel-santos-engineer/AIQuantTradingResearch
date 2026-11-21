# Codex execution prompt — Release 1.15 WP61 / PR #450 reconciliation

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
PR: https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/450

## Objective

Prepare PR #450 for owner approval by correcting Streamlit A/B comparison selector synchronization after snapshot pruning or identity changes, adding focused regression tests, reconciling with the latest `origin/main`, and recording accurate validation evidence. **Do not merge PR #450.** Do not touch PR #446.

## Known checkpoint (verify, do not assume still current)

- PR #450 head: `20c9cb9b4f9f67534c63283946e15bf0565c2736`, open and unmerged; original base `0cfb4886db626aab7487ce3ac7794e6d818e5fac`.
- Last observed `main`: `92aff046f24b1353670f57a79576e495f4dd0394` (merged PR #447); GitHub showed PR #450 mergeable but 2 commits behind.
- Local WP61 acceptance reported 84 passing tests and full browser A/B acceptance; evidence: `docs/roadmap/release-1.15/prompters/release-1.15-wp61-local-candle-comparison.md`.
- Scope currently limited to `python/presentation/research_chart.py`, `python/presentation/test_research_chart.py`, and the WP61 evidence document.
- PR #446 strict initializer remains separate and Linux-unqualified.

## Required work

1. **Preflight:** fetch `origin/main` and PR #450, inspect current statuses and full diff, confirm working-tree cleanliness and preserve operator-owned work. If worktree is dirty, use a safe isolated worktree/branch rather than resetting, deleting, stashing, or overwriting user changes. Identify any new upstream changes before editing. Do not assume a local running browser or Docker engine is available.
2. **Reproduce the edge case with fakes:** the comparison model (`compare_candles`) may reset an unavailable selected UTC timestamp to the first/last available candle, while the Streamlit selectbox's keyed session value can remain another still-valid timestamp. Inspect the current `research_chart.py` render flow, `InstrumentView` state, and Streamlit widget lifecycle. Verify the mismatch with a focused regression test. Cover pruning, incompatible identity (generation/displayDate/transformEpoch), compatible version refresh, both symbols, and user-initiated A/B changes as appropriate. Tests must demonstrate the pre-fix failure and post-fix pass where feasible.
3. **Fix narrowly:** establish one authoritative reconciliation of model selections and widget values *before* selectbox creation on each rerun. Preserve user selections when valid and intentional; reset only unavailable selections, or both for incompatible identity, with truthful notice. Do not overwrite a new user selection with stale model state on a normal rerun. Keep per-instrument and per-session independence; ensure both displayed rows come from one current validated snapshot. Avoid mutating DTOs, viewport, WP60 inspection, or API state. Handle empty/single-candle snapshots without invalid selectbox options or comparison indexing. Avoid duplicate reconciliation calls that clear notices or obscure reset causes.
4. **Tests:** add focused fake-only unit/integration coverage for the widget/model state handshake, including stale-but-valid widget state after model reset, removed selections, identity resets, ordinary user interaction, and refreshed candle values. Use a lightweight mocked Streamlit interface if necessary; do not require a real browser for the deterministic regression. Maintain existing decimal precision, half-even rounding, null semantics, and no-gap fabrication. Run the existing focused suite: `PYTHONPATH=python/presentation python -B -m unittest test_research_chart test_research_snapshot_client -q`. Report actual counts and failures; do not claim the previous 84 count for the new run without verifying.
5. **Reconcile branch:** integrate the latest `origin/main` into PR #450 safely, preserving merged PR #445/#447 and the latest Release 1.15 evidence. Prefer a controlled rebase or merge consistent with repository policy and safe remote branch handling; never force-push without establishing that it is permitted and safe. If branch ownership, policy, or conflicts prevent safe update, stop and report a precise proposed resolution. Confirm PR #450's changed-file scope and check for accidental changes. Keep #446 untouched and unmerged.
6. **Evidence and validation:** append a concise, truthful section to the existing WP61 evidence document with original vs current base/head, the reproduced failure, fix, new tests, actual commands/results, reconciliation details, and explicit limitations. Run `git diff --check` and any available safe local checks. If an owned local Docker/browser qualification is possible without touching retained assets, follow the existing documented supported launcher and preflight; otherwise report browser/runtime qualification as NOT_RUN for the revised code. Never represent earlier browser acceptance as a fresh post-fix browser pass.
7. **PR handoff:** push only the intended PR #450 branch update if safe and permitted, confirm its new head, mergeability, CI/check status, and changed paths. Update PR description if needed to reflect new evidence. Leave **OPEN and UNMERGED** for owner review. Provide the PR URL, commit SHAs, tests, any unresolved concerns, and an explicit merge recommendation.

## Non-negotiable boundaries

- No Azure or public runtime deployment, quota-consuming operations, production changes, provider calls, acquisitions, imports, data transfer, registry publication, or network-dependent dependency changes.
- No touching/reinitializing the retained SQLite historical volume, private source observations, API key, TLS materials, or rollback images. Do not print secrets or private paths.
- No .NET API/schema/Docker architecture changes; do not broaden WP61 scope or introduce trading indicators/signals.
- Preserve BTC/USD and ETH/USD exact transformed educational values; do not describe them as live/raw provider quotes.
- Preserve WP59 navigation, WP60 inspection, WP61 A/B comparison and refresh coexistence.
- `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` remain unchanged.
- Do not modify, close, or merge PR #446. Do not merge PR #450; owner approval is required.

## Completion criteria

A focused regression fails on the old behavior and passes after the correction; the full focused test suite passes; branch is reconciled with the verified current main; evidence is updated without erasing prior failed browser attempts or overstating new qualification; PR #450 remains open and ready for owner review. Stop and clearly report any blocker rather than inventing a pass or taking unauthorized actions.
