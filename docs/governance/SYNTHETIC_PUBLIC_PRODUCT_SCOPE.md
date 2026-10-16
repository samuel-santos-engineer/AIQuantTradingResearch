# Public product objective — synthetic crypto-chart education

**Status:** Authoritative product scope, owner-approved 2026-10-16.

> **A publicly accessible, educational synthetic crypto-chart application—not a live or real historical market-data platform.**

## Binding scope

- Public chart outputs are **synthetic only**: deterministic generated OHLCV candles, simulated prices/volumes and derived technical indicators. Clearly and prominently label charts and data as synthetic/simulated; do not represent them as real BTC/USD or ETH/USD prices, quotes, or historical market observations.
- Preserve the existing Streamlit/Plotly candlestick, volume, timeframe/range and technical-analysis UI, with offline C# SMA(20) and the finite checksummed immutable synthetic handoff.
- Docker-first Linux remains the sole release-certification target. Prefer a single image with approved immutable synthetic assets; no live C# worker, provider service or extra paid infrastructure.
- Real market-data feeds, exchange APIs, Twelve Data or other quote-provider research, real historical candle ingestion, provider-derived public outputs, and redistribution/licensing investigations for those integrations are **out of scope** unless the owner explicitly revises this objective in a later governance decision.
- The existing synthetic BTC-like educational showcase is in scope. A distinct synthetic ETH-like instrument is **not automatically authorized**; define its simulation and labeling separately before implementation.
- The synthetic dataset is not investment-grade market data. No live trading, order execution, real-time quote claims or production-trading claims.

## Release and workstream boundaries

- **C2B-32 (real historical BTC/USD and ETH/USD candles): ABORTED.** Do not resume its provider research or integration under the former authority. Preserve any preexisting work/evidence; do not delete branches or files without authorization.
- **C2B-31 (Azure F1 staging qualification): DEFERRED**, not waived. Public accessibility remains a product objective, not evidence of deployment readiness.
- C2B-29's locally qualified synthetic Linux image and C2B-30's exact-digest GHCR publication remain historical evidence, not permission to deploy or activate.
- No new Azure deployment, registry push, public data/API activation, GitHub merge of other PRs, data-source substitution or promotion is authorized by this document.
- Preserve governance gates G-B0–G-B7, dataset/public readiness gates, provenance and owner approvals. Until independently qualified and authorized: `PUBLIC_RUNTIME_VERIFIED=False`, public deployment/activation **NO-GO**, advanced WP07 **ON HOLD**. The existing public API unavailable contract remains in force where applicable.

## Change control

Any future proposal to use real historical or live market data must first obtain an **explicit owner-approved change to this product objective**. A task title, exploratory search, or Codex prompt cannot silently expand the scope.

This is a documentation-only scope decision. It does not alter runtime behavior, APIs, code, tests, infrastructure or published artifacts.
