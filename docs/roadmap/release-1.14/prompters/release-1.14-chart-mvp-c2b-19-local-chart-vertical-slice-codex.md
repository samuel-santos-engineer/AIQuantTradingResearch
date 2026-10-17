# C2B-19 — Local-only synthetic candlestick chart vertical slice

**Owner authority:** Implement the smallest useful local visual demonstration for AIQuantTradingResearch. This is a **showcase lab, not a production trading service**. Prefer existing components, short paths to visible functionality, and proportionate checks. Date: 2026-10-16.

## Accepted baseline and boundaries

- E4 is **ACCEPTABLE FOR SHOWCASE** at `3a4e6ef0e8a0ddf77776801cf87907927d8abc31`: C2B-17 reported 21/21 focused tests passing; C2B-18 specification-based review found no material discrepancies. This is not a third-party audit.
- `PUBLIC_RUNTIME_VERIFIED=False`. Public `/api/v1/dataset`, `/candles`, `/indicators` remain disabled/503 `DATASET_NOT_READY`; Azure deployment and public activation **NO-GO**. Advanced WP07 **ON HOLD**. Preserve protected WP07/archive/private material and unrelated owner files.
- The visual content is **BTC-like Market Simulation — Synthetic Historical OHLCV**, not real BTC/USD market data. No Twelve Data, exchange/provider calls, credentials, or private research datasets.
- Before editing, inspect current `main`, branch state, existing webapp/UI architecture, synthetic generator, E4 tests, and available chart packages. Do not assume Streamlit, JS charting or other implementation is present until verified. If baseline differs, report the delta and ensure no conflicting changes.

## Authorized implementation — narrow local vertical slice

1. On a new focused branch, use the **existing frontend and backend architecture** to display a locally served candlestick chart for the frozen **1h interval / 1-day visible window** (24 visible candles) from the accepted synthetic dataset. Include the required 250 hidden warmup rows for calculation, but never show them as visible candles.
2. Render recognizable OHLC candlesticks on a UTC time axis, with a clearly readable chart title and conspicuous synthetic/simulation label. Include one useful indicator overlay, preferably **SMA(20)**, calculated through existing verified C# logic. If the existing chart library supports a second overlay cheaply, EMA(20) is optional; do not add scope for it.
3. Use a **local-only, explicitly gated data path** that cannot expose the frozen data through currently disabled public routes. Favor an existing internal development/demo mechanism or an in-process test/dev host. If a new local endpoint is unavoidable, bind to loopback, gate it explicitly for local development, and ensure it cannot be enabled by the deployed Azure configuration. Preserve public route failure behavior.
4. Reuse existing synthetic generation, window selection, publication policy, and indicator calculation rather than duplicating algorithms or creating a new snapshot/export pipeline. No persistent candle files, no public artifact materialization, no Python runtime dependency, no new database, no auth system, no production telemetry stack.
5. Make the smallest necessary source/config/UI edits. Do not add a new frontend framework, charting dependency, or build subsystem without first explaining why existing facilities cannot render candles. Prefer already installed libraries; do not run package installation or network downloads without a separate owner decision.
6. Add or adjust only lightweight tests needed to verify the local route/gate and chart payload shape; keep existing E4 correctness proof untouched. Use the simplest safe mechanism available to validate the rendered chart locally.

## Execution permission and limits

- Authorized: local inspection, focused implementation, offline build, focused tests, and a **local-only browser or HTTP smoke check** if available with installed tools. Commands must be finite, supervised, and confined to reviewed project outputs. No Job Objects, native containment, elaborate harness, new CI workflows, or exhaustive process accounting.
- Do not run restore/install/download/provider access, create paid cloud resources, deploy Azure, enable public routes, generate persistent snapshots, or perform production activation. If existing dependencies are missing, **STOP and report** the smallest needed decision.
- Do not change frozen generator constants, E4 expected fingerprints, numerical semantics, dataset provenance, or private/public separation to make the chart work. On unexpected side effects, stop and report.
- Preserve unrelated tracked/untracked files. No broad cleanup or branch deletion.

## Acceptance checks

- Local webapp opens and visibly renders **24 synthetic 1h candlesticks** for the fixed 1-day end-anchored window, with a UTC axis and an SMA(20) overlay sourced from verified calculation logic.
- Clearly shows **BTC-like Market Simulation — Synthetic Historical OHLCV** and does not imply real exchange quotes or live trading.
- Local development gate is demonstrably separate from the disabled public data API. Public routes still return their existing unavailable behavior in nonlocal/default mode.
- Focused build/tests and smoke check results are recorded as actual PASS/FAIL/BLOCKED, not inferred. If graphical verification is unavailable, report the limitation honestly; do not claim a chart was seen.
- No new infrastructure, provider dependencies, or unauthorized persistent data artifacts.

## Deliverable and stop conditions

Open **one small implementation PR**, leave it **OPEN and UNMERGED**, and report: base/head SHAs, exact changed files, local launch steps, screenshots only if actually captured, observed chart behavior, focused test results, public-route gate verification, known limitations, and the next smallest action. If blocked by architecture or dependencies, stop before expanding scope and propose the least-complex alternative for owner approval.

**Success is a visible local chart, not a production qualification program.** Deployment/activation remains NO-GO; WP07 remains ON HOLD unless separately authorized.
