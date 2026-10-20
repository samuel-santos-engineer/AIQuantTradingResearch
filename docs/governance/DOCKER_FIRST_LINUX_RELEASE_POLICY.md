# Docker-first Linux release qualification policy

**Status:** Proposed Release 1.15 clarification — effective only after owner review and merge of this amendment.  
**Scope:** AIQuantTradingResearch public application; preserve Release 1.14 synthetic qualification history and support a separately authorized Release 1.15 historical crypto-chart candidate.  
**Target:** Azure App Service **Linux F1** custom container.  
**Authority:** Qualification policy only, not implementation, provider, release or deployment authorization.

## Decision and intent

The canonical release-qualification environment is the **actual Linux Docker image intended for deployment**. Windows may be a development workstation but is not a production/certification runtime. Build and qualify one immutable image and, only after separate approval, promote its exact digest.

## Binding rules

1. **Container-only release acceptance.** Functional, integration, browser, security, filesystem and startup evidence MUST exercise a locally executed Linux image. Windows host-only evidence does not certify the public runtime.
2. **One immutable deployable image.** Record source/build provenance and image digest; do not rebuild or substitute bits between qualification, registry promotion and Azure deployment. Promotion and deployment each require separate authorization.
3. **Runtime profiles are release-specific.** For the historical Release 1.14 synthetic showcase, prefer the existing single-process Streamlit/Plotly runtime with approved immutable synthetic handoff and offline C# calculations. For a **separately authorized Release 1.15 historical candidate**, a **single image with a minimal PID1 supervisor and two isolated non-root processes**—private C# ingestion/research and public Streamlit/Plotly—is an allowed architecture, subject to the Release 1.15 governance and qualification gates. This exception does not authorize any provider access or deployment.
4. **Dataset and source policy.** The Release 1.14 synthetic handoff remains checksummed, immutable and clearly synthetic. The Release 1.15 proposal instead uses bounded, private SQLite real historical data with transformed public presentation **only after verified provider rights**. Never package provider credentials, private replay, raw restricted candles or secrets in publicly readable image assets or public routes. Do not treat HMAC transformation as a license.
5. **Linux runtime security.** Verify correct PID1/supervisor behavior, restart limits, health/startup, public port 8501, separate non-root identities, strict file/secret permissions, SQLite/WAL/checkpoint isolation, loopback-only private API, public process environment sanitization, bounded memory/CPU/PID use and concurrent access. Test that the public identity cannot read research-only files/secrets or reach private services from external ingress. No unplanned outbound network: any future provider egress requires specific approval.
6. **Browser acceptance on container.** Use a real browser against the running Linux image. Release 1.14 tests cover synthetic candles, volume, range/interval, technical analysis, hover, zoom, pan and toolbar. Release 1.15 tests additionally cover BTC/USD and ETH/USD selection, indicators, historical viewport retention, reconnect/disconnect and last-known data, corrections/intervals, truthful transformed labels and status. Use deterministic isolated fixtures; no live provider dependency in CI.
7. **No Windows parity mandate.** Native Windows runtime or browser parity is not a release gate; historical Windows evidence may inform debugging.
8. **Provenance and repeatability.** Record source commit, Docker inputs, immutable digest, approved dataset provenance/manifest where applicable, commands, results, observed limits, failures and cleanup. Distinguish environment configuration from image bits.
9. **Per-site Azure verification.** After separately authorized deployment, verify exact digest, configuration, health, resource use, browser UX, API status and rollback at each Azure F1 site. Local F1-like limits are screening evidence, not Azure F1 capacity proof.
10. **Minimalism and change control.** No extra paid service, Windows compatibility program, public private API, provider integration or security exception merely to satisfy qualification. Deviations require explicit owner approval and scoped PR.

## Release gates and non-authorizations

This policy governs **where and how** qualification is performed, not who authorizes release. Preserve G-B0–G-B7, WP07, provider licensing, provenance, dataset readiness, security, immutable image promotion and owner approvals. On conflict, stop and request owner disposition; do not bypass gates.

Release 1.14's existing synthetic image and evidence remain historical and must not be silently mutated. C2B-32 remains ABORTED; C2B-31 remains DEFERRED. The proposed Release 1.15 dual-process runtime is a **candidate**, not a deployment authority. Until separately authorized and independently qualified, `PUBLIC_RUNTIME_VERIFIED=False`, public deployment/activation **NO-GO**, and advanced WP07 **ON HOLD**. Preserve existing unavailable public-API behavior where applicable.

## Review checklist

- [ ] Owner accepts Linux Docker image as exclusive public-runtime certification target.
- [ ] Owner accepts a narrowly scoped two-process exception for Release 1.15 only after separate implementation authority.
- [ ] Owner confirms the Release 1.14 synthetic baseline and all protected gates remain intact.
- [ ] Owner requires independent provider-rights evidence before any public real-derived display.
- [ ] CI/release implementation changes, if any, are handled in separately scoped PRs.
- [ ] No image build, provider call, registry push, Azure deployment, merge or data activation occurs through this governance PR.
