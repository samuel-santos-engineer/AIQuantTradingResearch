# Public product objective — Release 1.15 historical crypto chart and research interface

**Status:** Effective Release 1.15 product-direction amendment (merged PR #362).  
**Supersedes for Release 1.15 and subsequent explicitly authorized work:** the synthetic-only public-product objective approved 2026-10-16.  
**Architecture companion:** [HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md](HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md) (proposed in separate draft PR #361; review and approve independently).  
**Change class:** Product-scope governance only; no implementation, provider access, publication, or deployment authority.

> **New target:** A publicly accessible, educational **historical BTC/USD and ETH/USD chart and private research interface**, using completed real market observations in a protected C# service and **clearly labeled transformed, non-actual price presentation** in Streamlit/Plotly, **subject to existing security, readiness and deployment qualification controls**.

## Binding Release 1.15 scope

1. **Instruments and provenance.** The target instruments are BTC/USD and ETH/USD, using completed one-minute real OHLCV observations from a configurable source adapter. Twelve Data Basic is the initial candidate; its quota, coverage and availability must be measured rather than assumed. Preserve true source/provenance internally.
2. **Public representation.** Public charts present clearly labeled transformed educational values rather than actual quotes. The proposed interface displays HMAC-derived daily transformed OHLC and price-valued indicators, clearly and prominently labeled **transformed educational values—not actual BTC/USD or ETH/USD quotes**. Real observations and research calculations remain private. No real quote/price claims, investment-grade or real-time guarantee, trading advice or order execution.
3. **Implementation focus.** Build and test the agreed ingestion, research, transformed-presentation and recovery architecture using deterministic fixtures and measured provider capabilities. No licensing-specific investigation, evidence package or approval gate is required to begin or complete Release 1.15 engineering milestones. Transformation is a presentation technique, not a legal grant; applicable obligations remain independent of this implementation plan.
4. **Architecture direction.** Subject to separate implementation authority, target one Linux/amd64 immutable Docker image on Azure App Service Linux F1, containing a minimal PID1 supervisor, private non-root C# ingestion/research service, and public non-root Streamlit/Plotly UI. The private API binds only to loopback; private SQLite, credentials, HMAC material and recovery state are not accessible to Streamlit. No Redis, separate paid services or public private-API endpoint.
5. **Bounded data.** Proposed retention is the latest 1,000 completed one-minute candles per instrument in private SQLite, with no fabricated candles, unbounded archive, or guaranteed always-on freshness. Historical research validity, gaps, corrections, recovery, staleness and connection state must be shown truthfully.
6. **Qualification.** Linux container, isolation, schema/contract, browser, resource, integrity/recovery and per-site Azure F1 verification remain mandatory before any future public activation. The canonical detailed architecture and acceptance criteria are subject to owner review in the companion document.

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
- [ ] Owner reviews the companion Release 1.15 architecture PR independently.
- [ ] Implementation follows scoped Release 1.15 authority; image promotion and deployment remain separately controlled.

This is a documentation-only governance change; it does not modify runtime behavior, data, APIs, tests, infrastructure or published artifacts.

## Release 1.15 deployment-topology amendment — 2026-10-23

The owner has chosen two Docker containers and two Azure Linux F1 Web Apps **per environment**. For Release 1.15 this supersedes binding rule 4's **single image, PID1 supervisor, loopback-only research API** and “no public private-API endpoint” topology. The product objective remains the same: private real OHLCV/research, explicitly transformed educational public charts for BTC/USD and ETH/USD, bounded retention, no fabricated data and no trading execution.

**Production (West Central US):** existing UI Web App `aiqr112wp035ec325382770` plus research Web App `aiqr115researchprodwcus7f5eabb5`, sharing existing F1 Linux plan `asp-aiq-r112-wp03-wcus-5ec325382770`. **Staging (West US 2):** existing UI Web App `aiqr112wp05wus27f5eabb5` plus research Web App `aiqr115researchstagingwus27f5eabb5`, sharing existing F1 Linux plan `asp-aiq-r112-wp05-wus2-7f5eabb5`. Existing UI sites and infrastructure are preserved. New research sites are empty shells only, as evidenced in pending PR #381; they do not establish runtime qualification.

Each role now has its own immutable Linux/amd64 image and non-root container process. The research endpoint is **not** cross-app loopback and must not be deemed private just because it is a research app. Until F1-compatible HTTPS service authentication, least-privilege transformed-only endpoint authorization, ingress protection and failure handling are implemented and qualified, public research endpoint activation remains prohibited. No raw observations, provider secrets, HMAC keys, SQLite, checkpoints or research administration are exposed to UI or users. Do not assume Free F1 supports VNet/private endpoints or add paid infrastructure silently. Existing qualification/approval gates remain.

The previously selected WP01 D1-A root PID1, role-UID census, process reaping and restart mechanism is **superseded for Release 1.15's new topology only**; accepted numerical D3, snapshot semantic D4 and transformation D5 decisions remain unaffected except that D4 transport, authentication and deadlines must be qualified across Web Apps. This documentation change is architectural authorization to replan WP02 and dependent transport interfaces, **not** authorization for implementation, deployment, provider activation or release. Preserve Release 1.14 history, no licensing-specific engineering approval gate and `PUBLIC_RUNTIME_VERIFIED=False`.
