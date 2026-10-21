# Public product objective — Release 1.15 historical crypto chart and research interface

**Status:** Effective Release 1.15 product-direction amendment (merged PR #362).  
**Supersedes for Release 1.15 and subsequent explicitly authorized work:** the synthetic-only public-product objective approved 2026-10-16.  
**Architecture companion:** [HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md](HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md) (merged in PR #361).  
**Change class:** Product-scope governance only; no implementation, provider access, publication, or deployment authority.

> **New target:** A publicly accessible, educational **historical BTC/USD and ETH/USD chart and private research interface**, using completed real market observations in a protected C# service and **clearly labeled transformed, non-actual price presentation** in Streamlit/Plotly, **subject to existing security, readiness and deployment qualification controls**.

## Binding Release 1.15 scope

1. **Instruments and provenance.** The target instruments are BTC/USD and ETH/USD, using completed one-minute real OHLCV observations from a configurable source adapter. Twelve Data Basic is the initial candidate; its quota, coverage and availability must be measured rather than assumed. Preserve true source/provenance internally.
2. **Public representation.** Public charts present clearly labeled transformed educational values rather than actual quotes. The proposed interface displays HMAC-derived daily transformed OHLC and price-valued indicators, clearly and prominently labeled **transformed educational values—not actual BTC/USD or ETH/USD quotes**. Real observations and research calculations remain private. No real quote/price claims, investment-grade or real-time guarantee, trading advice or order execution.
3. **Implementation focus.** Build and test the agreed ingestion, research, transformed-presentation and recovery architecture using deterministic fixtures and measured provider capabilities. No licensing-specific investigation, evidence package or approval gate is required to begin or complete Release 1.15 engineering milestones. Transformation is a presentation technique, not a legal grant; applicable obligations remain independent of this implementation plan.
4. **Architecture direction.** Subject to separate implementation authority, target one Linux/amd64 immutable Docker image on Azure App Service Linux F1, containing a minimal PID1 supervisor, private non-root C# ingestion/research service, and public non-root Streamlit/Plotly UI. The private API binds only to loopback; private SQLite, credentials, HMAC material and recovery state are not accessible to Streamlit. No Redis, separate paid services or public private-API endpoint.
5. **Bounded data.** Proposed retention is the latest 1,000 completed one-minute candles per instrument in private SQLite, with no fabricated candles, unbounded archive, or guaranteed always-on freshness. Historical research validity, gaps, corrections, recovery, staleness and connection state must be shown truthfully.
6. **Qualification.** Linux container, isolation, schema/contract, browser, resource, integrity/recovery and per-site Azure F1 verification remain mandatory before any future public activation. The canonical detailed architecture and acceptance criteria are defined in the merged companion document.

## Transition from the October 16 synthetic objective

The prior document's **synthetic-only** restriction is superseded **as a future product objective** when this amendment is merged; it is **not** a statement that a real-data release is ready or permitted today. The Release 1.14 deterministic synthetic BTC-like showcase, immutable synthetic dataset and associated checksums, CI evidence, historical image and governance decisions remain preserved and historically valid. Do not relabel synthetic candles as historical observations or silently replace an already qualified immutable image.

- **C2B-32 remains ABORTED.** The former workstream and its authorization do not resume. Release 1.15 engineering proceeds under its own scoped implementation authority, without a licensing prerequisite.
- **C2B-31 remains DEFERRED.** This scope amendment does not authorize staging or Azure publication.
- C2B-29/C2B-30 historical artifacts and digest evidence remain historical; no new promotion is implied.
- The previous synthetic-only product restriction no longer blocks Release 1.15 ingestion and chart implementation planning. Provider integration and runtime activation remain governed by scoped engineering and deployment decisions, not a new licensing-review gate.
- Until the new architecture is separately implemented, qualified and activated, existing synthetic/unavailable public-runtime contracts remain in force. No silent cutover or activation.

## Release gates and non-authorizations

Preserve gates G-B0–G-B7, dataset/public readiness, provenance, security, owner approval and advanced WP07 controls. Do not add licensing-specific Release 1.15 approval gates. Unless independently verified and separately authorized, `PUBLIC_RUNTIME_VERIFIED=False`, Azure public deployment/activation **NO-GO**, and advanced WP07 **ON HOLD**. The existing public API unavailable contract remains effective where applicable.

**Approval of this governance amendment authorizes the change in intended product direction only.** It does **not** authorize implementation, restarting an aborted workstream, real feed activation, registry push, Azure deployment, public data/API enablement, GitHub merge of other PRs, or any gate waiver.

## Review and change control

- [ ] Owner approves replacement of the future synthetic-only objective with the gated historical BTC/USD + ETH/USD research/chart objective.
- [ ] Owner confirms that licensing review is not a Release 1.15 engineering prerequisite.
- [ ] Owner confirms Release 1.14 synthetic evidence and C2B-32/C2B-31 statuses are preserved.
- [x] Companion Release 1.15 architecture PR #361 merged.
- [ ] Implementation follows scoped Release 1.15 authority; image promotion and deployment remain separately controlled.

This is a documentation-only governance change; it does not modify runtime behavior, data, APIs, tests, infrastructure or published artifacts.
