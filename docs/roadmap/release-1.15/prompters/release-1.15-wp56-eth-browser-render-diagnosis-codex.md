# Codex prompt — Release 1.15 WP56: ETH browser render diagnosis and narrow correction

## Context and authority

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Current review PR: https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/443, expected OPEN/UNMERGED head `470561539372adde0ec0452cfc8a930f06b6e755`. Baseline main after PR #442 squash: `61a1f804526b145e8a3d47f1daf4795f3fbec375`. Inspect actual branch/head before work; if changed, reconcile before edits. Do not merge PR #443.

Release 1.15 remains exclusively BTC/USD and ETH/USD, with Twelve Data as the only authoritative market-data provider. Approved explicit OHLC-only mode carries genuine provider OHLC and `volume: null` when unavailable; retain the volume field, numeric zero and positive-volume compatibility, strict OHLCV default, C# indicators, and educational/transformation disclosures. No synthetic data may be presented as authentic. No additional instruments/providers, licensing gates, schema migrations, or architecture expansion.

Verified WP55 state: one separately authorized fresh preparation, one successful bounded authentic acquisition (1000 BTC and 1000 ETH candles, no gaps, null volume), Import, Start, and authenticated Verify completed. Both-symbol API snapshots passed. BTC browser acceptance passed (candles, SMA20, no volume bars, disclosures). ETH selector radio changes successfully but ETH Plotly graph remains absent after a 30-second wait (`ETH_RENDER_WAIT`); no page/JS exception was captured. The root cause is unknown. Existing local chart was reported at `http://127.0.0.1:18555`; do not assume it is still running. PR #443 contains evidence only. `AUTHENTIC_DATA_NOT_IMPORTED=False`; `LOCAL_CHART_VERIFIED=False`; `PUBLIC_RUNTIME_VERIFIED=False`.

## First task: obtain a precise, bounded diagnosis

Before doing anything operational, inspect source and existing tests offline, including `python/presentation/research_chart.py`, `research_chart_component.js`, `research_chart_component.html`, `research_snapshot_client.py`, relevant Streamlit/Plotly tests, and WP55 evidence. Develop hypotheses without asserting a root cause. Instrument switching crosses Streamlit session-state, fragment rerun, per-instrument client cache, component identity/key, JS component mount and Plotly graph lifecycle; do not assume which failed.

**Operational permission boundary:** This prompt authorizes offline source inspection, reasoning, local code/test edits and offline synthetic tests only. It does **not** authorize browser access to the running local chart, Chrome/CDP, API requests, container inspection/restart/build, private files or secrets, Docker, Azure, Twelve Data requests, or any new Acquire/Import/Start/Verify. Before any browser-only diagnostic, stop and ask the owner for explicit approval of **one bounded browser-only session** against the existing loopback chart, with no application/container changes. If unavailable, do not fabricate browser evidence; continue with offline tests and report the exact blocked next action.

Once separately authorized, use an already available project browser/venv and an owned temporary Chrome profile, with no installs. A single controlled visit may navigate BTC then click ETH, observing at most 30 seconds after selection. Capture **sanitized** state only:

1. Checked instrument radio before/after; visible Streamlit instrument-specific status, candle-count, research and availability captions (presence/absence and coarse category, not raw prices/timestamps).
2. ETH component host presence, expected custom-element registration, component root/iframe or shadow DOM if applicable, Plotly graph div and number of candlestick traces; compare to BTC without dumping figure JSON or data.
3. Component initialization/mount and Streamlit rerun state; capture sanitized console errors, `Runtime.exceptionThrown`, `pageerror`, and failed same-origin asset requests (resource type/status only, no tokens/URLs containing secrets).
4. Distinguish: ETH snapshot absent; Streamlit content not rerendered; component present but JS not initialized; Plotly render absent/failing; or an as-yet-unclassified condition. A 30-second timeout is evidence of absence within that window, not proof of a root cause.

Close only the Chrome/profile resources created by this run. Retain all preexisting preparation, images, volume, bundle, database and chart process. No automatic retries, provider requests, or fallback to synthetic data.

## Narrow fix only after evidence

If diagnosis identifies a reproducible application defect, implement the smallest correction in the existing presentation/component path. Preserve per-instrument state, client fetch rules (no extra automatic provider calls), explicit refresh semantics, event/viewport validation, Plotly indicators from C# snapshots, null-volume disclosure, and authentic provenance. Avoid generic timeouts, blind retries, forced app restarts, relaxing validation, or suppressing errors as a substitute for a fix.

Add targeted regression tests that reproduce the specific ETH switching failure using **offline synthetic snapshots** for both instruments, and prove BTC→ETH→BTC selection and correct instrument-specific graph mounting. If browser integration tests cannot run without separate operational approval, add unit/component lifecycle tests and mark real browser acceptance NOT_RUN; do not mark local chart verified. Run existing chart/client and Python workflow tests, plus relevant C# tests only if touched. Document exact commands, pass/fail/skip, and test limitations. Preserve UTF-8/LF/no BOM and run `git diff --check`.

**Deployment/verification boundary:** Any rebuild/restart of the retained local chart, browser rerun, or access to real imported data requires separately requested explicit owner authority; do not infer it from approval to edit code. If a fix requires a new matching preparation due to identity guards, explain that dependency and stop rather than bypass guards or mutate retained artifacts. Do not attempt Azure/public deployment.

## PR and reporting

Prefer a focused follow-up PR against current `main` for any production fix, referencing open PR #443, rather than rewriting its completed evidence; leave both OPEN/UNMERGED. If only offline diagnosis is possible, report findings without opening a documentation-only PR unless the owner specifically asks. If a code PR is created, provide exact branch, head SHA, changed paths, focused tests, evidence, and the next owner approval needed. Do not merge anything.

Report a concise acceptance matrix: BTC browser, ETH browser, both-symbol API, local chart verified, public runtime verified. Maintain `AUTHENTIC_DATA_NOT_IMPORTED=False` (already imported), `LOCAL_CHART_VERIFIED=False` until *both* instruments are verified in an actual authorized browser session, and `PUBLIC_RUNTIME_VERIFIED=False`. Explicitly distinguish observed evidence, hypotheses, and unperformed work.

## Completion target

Root-cause the ETH graph absence and produce a narrowly tested correction, then seek separate permission for any required live local runtime/browser requalification. Do not redo the successful Twelve Data acquisition/import. The milestone is complete locally only when authentic BTC and ETH candlesticks and price indicators are both visible in the browser with correct null-volume behavior and disclosures.
