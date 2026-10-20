# Public product objective — Release 1.15 historical crypto chart and research interface

**Status:** Proposed owner-approved direction change, **effective only after this governance amendment is reviewed and merged**.  
**Supersedes for Release 1.15 and subsequent explicitly authorized work:** the synthetic-only public-product objective approved 2026-10-16.  
**Architecture companion:** [HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md](HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md) (proposed in separate draft PR #361; review and approve independently).  
**Change class:** Product-scope governance only; no implementation, provider access, publication, or deployment authority.

> **New target:** A publicly accessible, educational **historical BTC/USD and ETH/USD chart and private research interface**, using completed real market observations in a protected C# service and **clearly labeled transformed, non-actual price presentation** in Streamlit/Plotly, **only when provider rights and all release gates are independently satisfied**.

## Binding Release 1.15 scope

1. **Instruments and provenance.** The target instruments are BTC/USD and ETH/USD, using completed one-minute real OHLCV observations obtained from a separately approved, lawful provider. Twelve Data Basic is a **candidate**, not an approved entitlement, license, quota, availability guarantee or implementation authority. Preserve true source/provenance internally.
2. **Public representation.** Public charts may show only explicitly licensed/authorized provider-derived information. The proposed interface displays HMAC-derived daily transformed OHLC and price-valued indicators, clearly and prominently labeled **transformed educational values—not actual BTC/USD or ETH/USD quotes**. Real observations and research calculations remain private. No real quote/price claims, investment-grade or real-time guarantee, trading advice or order execution.
3. **License gate before provider use/public display.** Verify applicable provider terms, symbols, plan, acquisition, retention, derived/transformed public chart presentation, redistribution and attribution requirements, with durable evidence and owner approval. **Transformation, login, caching, attribution or absence of raw prices does not itself grant redistribution rights.** If rights are not established, public real-derived charts are **NO-GO**; do not substitute a source or fall back to a provider without separate approval.
4. **Architecture direction.** Subject to separate implementation authority, target one Linux/amd64 immutable Docker image on Azure App Service Linux F1, containing a minimal PID1 supervisor, private non-root C# ingestion/research service, and public non-root Streamlit/Plotly UI. The private API binds only to loopback; private SQLite, credentials, HMAC material and recovery state are not accessible to Streamlit. No Redis, separate paid services or public private-API endpoint.
5. **Bounded data.** Proposed retention is the latest 1,000 completed one-minute candles per instrument in private SQLite, with no fabricated candles, unbounded archive, or guaranteed always-on freshness. Historical research validity, gaps, corrections, recovery, staleness and connection state must be shown truthfully.
6. **Qualification.** Linux container, isolation, schema/contract, browser, resource, integrity/recovery and per-site Azure F1 verification remain mandatory before any future public activation. The canonical detailed architecture and acceptance criteria are subject to owner review in the companion document.

## Transition from the October 16 synthetic objective

The prior document's **synthetic-only** restriction is superseded **as a future product objective** when this amendment is merged; it is **not** a statement that a real-data release is ready or permitted today. The Release 1.14 deterministic synthetic BTC-like showcase, immutable synthetic dataset and associated checksums, CI evidence, historical image and governance decisions remain preserved and historically valid. Do not relabel synthetic candles as historical observations or silently replace an already qualified immutable image.

- **C2B-32 remains ABORTED.** The former workstream and its authorization do not resume. Any Release 1.15 real-data research/implementation requires **new, scoped owner authority**, after license and governance prerequisites.
- **C2B-31 remains DEFERRED.** This scope amendment does not authorize staging or Azure publication.
- C2B-29/C2B-30 historical artifacts and digest evidence remain historical; no new promotion is implied.
- The previous synthetic-only restriction on researching provider terms is lifted **for planning and lawful licensing due diligence only**; this does not authorize API integration, live polling, credential use, provider redistribution or data publication.
- Until the new architecture is separately implemented, qualified and activated, existing synthetic/unavailable public-runtime contracts remain in force. No silent cutover or activation.

## Release gates and non-authorizations

Preserve gates G-B0–G-B7, dataset/public readiness, provenance, licensing, security, owner approval and advanced WP07 controls. Unless independently verified and separately authorized, `PUBLIC_RUNTIME_VERIFIED=False`, Azure public deployment/activation **NO-GO**, and advanced WP07 **ON HOLD**. The existing public API unavailable contract remains effective where applicable.

**Approval of this governance amendment authorizes the change in intended product direction only.** It does **not** authorize implementation, restarting an aborted workstream, real feed activation, registry push, Azure deployment, public data/API enablement, GitHub merge of other PRs, or any gate waiver.

## Review and change control

- [ ] Owner approves replacement of the future synthetic-only objective with the gated historical BTC/USD + ETH/USD research/chart objective.
- [ ] Owner accepts that provider-derived public output is blocked pending explicit license evidence and approval.
- [ ] Owner confirms Release 1.14 synthetic evidence and C2B-32/C2B-31 statuses are preserved.
- [ ] Owner reviews the companion Release 1.15 architecture PR independently.
- [ ] Any future implementation, image qualification, provider use and deployment receive separately scoped approval.

This is a documentation-only governance change; it does not modify runtime behavior, data, APIs, tests, infrastructure or published artifacts.
