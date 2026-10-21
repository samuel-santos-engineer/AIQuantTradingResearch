# Release 1.15 Definition — Historical Crypto Chart and Research Interface

## Status, authority and predecessor
**Status:** Planning baseline for owner review; not evidence of implementation, provider activation or deployment.
**Predecessor:** Release 1.14 synthetic public preview remains immutable historical evidence.
**Product authority:** [Historical Crypto Chart and Research Interface](../../governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md), [Public Product Scope](../../governance/SYNTHETIC_PUBLIC_PRODUCT_SCOPE.md), and [Docker-first Policy](../../governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md).
**Engineering convention:** [Repository-wide LF Policy](../../governance/REPOSITORY_LF_LINE_ENDINGS_POLICY.md) and root `AGENTS.md`.

Read alongside [Execution Plan](RELEASE_1.15_EXECUTION_PLAN.md), [File Manifest](RELEASE_1.15_FILE_MANIFEST.md), [Acceptance](RELEASE_1.15_ACCEPTANCE.md), and [Issue Register](RELEASE_1.15_ISSUE_REGISTER.md). The architecture governance document controls technical decisions if a planning summary differs.

## Product objective
Deliver a publicly accessible educational **historical BTC/USD and ETH/USD** candlestick research interface. The private C# service ingests and researches real completed one-minute OHLCV; the public Streamlit/Plotly process receives **transformed educational values**, never actual quotes or private research storage. No trading execution, investment-grade price claims, brokerage, backtesting, ML production, paid add-ons or availability SLA.

## Binding design
| Dimension | Release 1.15 contract |
| --- | --- |
| Instruments | BTC/USD and ETH/USD, separately selectable |
| Source | Configurable adapter; Twelve Data Basic initial candidate, actual capabilities measured |
| Ingestion | Completed 1-minute UTC OHLCV, quota-aware, fair, gap/correction-safe, no fabricated candles |
| Retention | Latest 1,000 completed 1m candles per instrument in private SQLite |
| Private research | SMA20, EMA20, Bollinger20, RSI14, MACD12/26/9, ATR14, trend score, regime, events |
| Regime | 100 consecutive warmup plus 300 prior valid calibration observations; first eligible at candle 401 |
| Presentation | Shared UTC-day positive HMAC factor for OHLC and price-valued indicators; truthful labels |
| API | Read-only loopback JSON, full/unchanged conditional snapshots, generation/version/date identity |
| UI | Plotly candles/volume/indicators; 100/300/1,000 retained-candle presets (default 300) |
| Interaction | Live follow and Historical Exploration; preserve absolute UTC viewport across 60s refresh |
| Runtime | One immutable Linux/amd64 Docker image; minimal PID1 supervisor; isolated non-root C# and Streamlit processes |
| Hosting | Azure App Service Linux F1 candidate; local Docker qualification first |
| State | Explicit research/freshness/connectivity/classification validity; safe last-known display |
| Security | No public raw candles, research files, provider credentials, HMAC keys or private API ingress |

## Non-goals and preserved boundaries
Release 1.14 synthetic datasets and qualifications are not overwritten or relabeled. C2B-32 remains ABORTED; C2B-31 remains DEFERRED. No new licensing-specific engineering gate is created; applicable terms remain independent obligations. No automatic image promotion, Azure activation, provider entitlement claims, or bypass of G-B0–G-B7/WP07/security/provenance controls. `PUBLIC_RUNTIME_VERIFIED=False` until independently qualified and authorized.

## Work-package sequence
WP01 contract/feasibility → WP02 Linux runtime; WP03 ingestion/storage after runtime contracts; WP04 research after WP03; WP05 transformed API after WP02–WP04; WP06 public UI after WP05; WP07 integration/qualification after WP02–WP06; WP08 release acceptance after WP07. Issues #367–#374 are tracked in the issue register.

## Quality and evidence
All WPs require deterministic tests, explicit negative cases, Linux-container evidence where applicable, no fabricated benchmark results, and owner review. Every changed text file MUST be UTF-8 LF, including PowerShell. Preserve exact file allowlists per implementation PR, not inferred from this planning document.
