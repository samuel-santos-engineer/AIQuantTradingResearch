# Codex execution prompt — Release 1.15 WP14: HTTPS-first research API and chart integration

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Baseline: `main` includes merged PR #405, merge commit `835891b6ca762698c2ea79d96bf4078dd6f62280`.

## Decision and objective

The owner has approved changing implementation direction: ordinary authenticated HTTPS/JSON between the existing Streamlit UI and .NET 10 research service is the product architecture. Stop treating interactive Azure terminal access, immutable image execution attestation, and a guaranteed diagnostic hard-stop as prerequisites to building or testing the HTTPS application path. Preserve existing fail-closed diagnostics, but do not extend them for this work package.

Deliver the smallest useful **implementation PR** toward a public, authentic historical BTC/USD and ETH/USD 1-minute candlestick chart with Plotly and the existing indicators. Do not produce a planning-only PR. Inspect the repository first: reuse existing endpoints, contracts, clients, authentication, charts, data integrity checks, tests, deployment scripts and documentation. Do not duplicate implemented functionality or weaken controls merely to satisfy this prompt.

## Implementation work

1. Trace the actual Streamlit-to-research-service call path and identify concrete blockers to HTTPS/JSON integration on the two existing Azure F1 App Services. Distinguish reachability, authentication, readiness, dataset initialization, transport, and UI rendering. Do not conflate `/health` with dataset readiness.
2. Implement or complete the smallest authenticated, bounded read-only HTTPS API contract for dataset status and historical candles, and the Streamlit HTTP client/chart integration as necessary. Prefer existing routes and schemas. Support only BTC/USD and ETH/USD, completed 1-minute candles, max 1,000 per instrument; preserve established integrity checks and fail-closed handling. No synthetic/demo fallback in public acceptance.
3. Ensure configuration of the research service URL and authentication is environment-based, with no secrets in repository, responses, logs, tests or screenshots. Use timeouts, bounded response sizes, explicit errors and no unbounded retry behavior. Never expose provider credentials to the browser. Keep server-side Streamlit-to-research authentication where possible.
4. Make runtime behavior explicit: HTTP process alive, dataset missing, dataset ready, data stale or unavailable, and research service unreachable must be distinguishable without misleading success messages. Preserve exact-site F1 non-durable SQLite exception and all existing WAL, single-writer, D5/HMAC, authorization and storage guards. Do not silently relax guards to make the deployment appear healthy.
5. Add focused tests for API authentication, bounded queries, malformed/missing data, client errors/timeouts, and chart behavior using deterministic fixtures clearly labeled as test-only. Verify relevant .NET, Python and script tests and report actual results. Preserve unrelated owner changes; do not reset, stash, clean or overwrite them.
6. If deployment scripts require improvements, make them minimal, reversible, auditable and opt-in. Provide an exact manual deployment and HTTPS smoke-test sequence for the **existing** UI and research F1 staging App Services, including a rollback plan and a truthful public-browser acceptance checklist. Prefer functional HTTPS evidence over the WP11–WP13 terminal qualification path.

## Existing environment and constraints

- UI staging app: `aiqr112wp05wus27f5eabb5`; public acceptance URL: `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/`.
- Research staging app: `aiqr115researchstagingwus27f5eabb5`.
- Existing F1 plan: `asp-aiq-r112-wp05-wus2-7f5eabb5`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, West US 2. **No new paid infrastructure.**
- .NET 10 research backend, Streamlit 1.61.1 / Plotly 7.1.0 UI, BTC/USD and ETH/USD, completed 1-minute candles, max 1,000 per symbol, existing SMA20/EMA20/BB20/RSI14/MACD12-26-9/ATR14 indicators.
- Authentic Twelve Data acquisition remains separately bounded by the owner's prior approval: at most **two total provider requests**, one per symbol, max 1,000 completed 1-minute candles per symbol, no retries, max 16 credits total. **Do not make provider calls in WP14**; credentials and available credits have not been verified. Do not seek or reveal secrets.
- Research storage is non-durable on the designated F1 staging site. Do not claim durability or auto-seed fabricated data. Missing database must be reported accurately.
- The previous diagnostic experiment produced no attributable output. WP13 was blocked; no changes or PR. PR #405 is merged. Do not remove the diagnostic execution block as a shortcut; simply keep this application-integration work independent of that diagnostic controller.

## Authority boundary — mandatory

This prompt authorizes **repository implementation, offline tests, documentation, and opening a PR for owner review only**. It does **not** authorize Azure configuration changes, deployments, image publication, live HTTPS probes against staging, app restart/stop, provider requests, data import, secrets access, PR merge, or modification of paid resources. If actual staging evidence is necessary, state the exact narrow approval needed and stop at that boundary. Do not label offline tests as public runtime verification.

## Deliverables

- A focused implementation PR against `main`, with code and tests (unless repository inspection proves the entire HTTPS path already exists; in that case report exact evidence and do not create a no-op PR).
- PR description: before/after request path, actual changed files, security/storage invariants, tests and results, deployment steps, rollback, unresolved external prerequisites, and exact next authorization request.
- Clear status flags: `AZURE_DEPLOYMENT_NOT_RUN`, `AUTHENTIC_DATA_NOT_IMPORTED`, `PUBLIC_RUNTIME_VERIFIED=False` until independently demonstrated otherwise.

Prioritize delivering a working HTTPS research application over adding another governance or diagnostic gate.
