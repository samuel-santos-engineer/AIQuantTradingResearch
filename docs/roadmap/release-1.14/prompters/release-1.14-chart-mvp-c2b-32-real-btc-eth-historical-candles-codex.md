# C2B-32 — Real BTC/USD and ETH/USD Historical Candles: Source Rights and Docker-First Local Vertical Slice

**Authority type:** Bounded research and conditional local implementation. **Owner decision:** prioritize real historical market data; C2B-31 Azure staging is deferred. **No Azure deployment, public activation, registry push, merge, or production-data publication is authorized.**

## Mission

Establish a demonstrably lawful, reproducible path to **real historical BTC/USD and ETH/USD OHLCV candles** in the existing Streamlit/Plotly chart. Keep the existing single Linux Docker image model, minimal dependencies, and C#-derived technical analysis. **Source licensing/provenance is the first gate; do not implement or display real candles publicly until rights are evidenced and accepted.** Synthetic mode remains available as a test fixture, clearly labeled.

## Baseline and preservation

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`; merged Docker-first Linux governance from PR #348 governs acceptance.
- C2B-29 PR #350 is the locally qualified, self-contained synthetic Linux candidate, still open/unmerged at last report; C2B-30 PR #351 documents verified GHCR publication of **that exact synthetic image**. Do not repurpose or overwrite its immutable digest, tag, or archive.
- Historical C2B-24–30 evidence and owner files are immutable review inputs. PRs #346, #347, #349, #350, #351 may be open; inspect their **live** state and branch ancestry before changes. Avoid duplicate cherry-picks and unrelated merges.
- Preserve original Streamlit/Plotly candlesticks, volume, selectors, hover, zoom/pan, toolbar and native Technical Analysis popover. Preserve accepted SMA-toggle zoom reset limitation unless separately approved.
- `PUBLIC_RUNTIME_VERIFIED=False`; public API endpoints must remain `503 JSON DATASET_NOT_READY` unless a **separate** owner-approved public-readiness and deployment authority changes that. Advanced WP07 remains ON HOLD; protected G-B0–G-B7 gates are unchanged.

## Phase A — First-party source, provenance and redistribution-rights investigation (mandatory first)

Investigate potential **first-party exchange / venue** sources for BTC/USD and ETH/USD historical OHLCV, alongside any reputable aggregators, but do **not** assume an unauthenticated/free API implies redistribution rights. A source must be examined at the exact relevant product/endpoint and terms version. At minimum assess:

1. Is the pair **actually BTC/USD and ETH/USD spot** (not BTC/USDT, ETH/USDT, perpetuals, composite indices, synthetic USD, or derivatives)? If only substitutes exist, report and request owner decision; do not silently relabel.
2. Who owns/controls the data and what is its upstream provenance? If an aggregator, identify upstream exchange data and sublicensing chain. Prefer direct venue provenance where terms permit.
3. Do **written, applicable terms** explicitly allow (a) programmatic historical retrieval, (b) storing/caching, (c) derived SMA computation, (d) server-side transformation, (e) **display to unauthenticated third-party website visitors**, and (f) redistribution or public access to stored OHLCV via API or Docker image? Distinguish each right; permission for display does not automatically authorize raw-data redistribution.
4. Attribution, branding, copyright/database rights, geographic/consumer restrictions, commercial-use conditions, retention limits, request-rate limits, access credentials, and revocation/change obligations. Note that authentication/login to our app is **not** a substitute for a data license.
5. Endpoint specifics: intervals, candle boundaries, UTC convention, volume unit, final vs incomplete candle, pagination, maximum lookback, gaps, outages, corrections, and costs/free-tier limitations.
6. Cite **official primary-source URLs**, exact quoted clauses (brief excerpts), access dates, and preserved nonsecret evidence. Flag ambiguities explicitly. Do not assert legal clearance from inference or generic API docs; unresolved rights are **NO-GO** for real-data publication.

Deliver a comparison matrix with at least two plausible candidates **if evidence supports them**. If no provider unambiguously permits our precise use, stop after research and produce an owner decision memo proposing options (license negotiation, narrower authorized display-only model, or continued synthetic-only chart). Do not make a live provider integration just to demonstrate progress.

## Phase B — Design and conditional local implementation

**Only if Phase A identifies a source with sufficiently clear applicable rights and a documented owner approval of the selected source/use model**, implement a minimal local-only vertical slice. If no such approval exists, stop at design/PR evidence, leaving source integration unimplemented.

- Prefer offline/batch historical acquisition with a finite, checksummed dataset, explicit source and retrieval timestamp, provenance manifest, retention rules and deterministic build inputs. Avoid a live provider service, paid Azure resources, background jobs, new databases, additional containers or runtime API keys where not required.
- Keep BTC/USD and ETH/USD separate, explicitly identified, with source/venue, timezone UTC, interval, start/end, last-complete-candle policy and a visible **real market data** label. Never combine exchange candles under one symbol without clear disclosure.
- Validate open/high/low/close invariants, timestamp uniqueness/order, regular grids and gaps, volume semantics, numeric bounds, pagination overlaps, incomplete candles, provider corrections and integrity hashes. Fail closed on invalid data; do not fabricate candles to fill gaps.
- Reuse existing chart and existing C# SMA(20) offline calculation path; maintain warmup/visible separation and verify values independently. No Python/JavaScript indicator reimplementation unless separately authorized.
- Keep raw real data out of Git, public GHCR, web assets and API responses until specific publication rights and artifact promotion are approved. If display is permitted but redistribution is not, document and enforce the distinct boundary; **do not embed redistributable raw data in a public image** by default.
- Keep development/test fixtures explicitly synthetic and deterministic. Avoid dependence on live provider access in unit tests. Any local real-data preview must be access-restricted and nonpublic with an explicit rights gate.
- Use Linux Docker as the only runtime acceptance target. Windows may orchestrate development, but native Windows parity is not a release gate. No change to the public API's default 503/404 contracts.

## Validation if Phase B is authorized

Run tests against the **actual intended local Linux image**, not a host-only script or temporary proxy substitute. Demonstrate both instruments, available intervals and range controls, correct candle count/boundaries, C# SMA20 parity, volume and tooltip labeling, toggling, zoom/pan and toolbar, graceful missing/invalid-source behavior, restart, non-root/read-only startup, resource/concurrency limits and no secret/source leak. Distinguish provider sampling evidence from deterministic local test results. Do not certify F1 capacity from local screening. Re-run changed-path security and public-route negative checks.

## Deliverables and PR discipline

Create **one narrowly scoped PR, OPEN/UNMERGED**, with:

- A source-rights/provenance matrix including first-party citations and explicit allowed/unknown/prohibited use distinctions.
- An owner decision gate for the proposed data source and **separate** gates for public chart display, raw API publication, Docker asset inclusion and Azure deployment.
- Exact code/test inventory **only if Phase B was expressly cleared**, reproducible Linux commands, bounded resource evidence, failures/limitations, and cleanup record.
- A plain-language summary of whether real BTC/USD and ETH/USD candles are locally available, merely technically feasible, or still blocked by licensing.

Do not touch owner-authored files, overwrite C2B-29/C2B-30 image identities, silently import unmerged PRs, create new Azure resources, publish provider data, push a new image, modify Azure, or merge any PR. Record branch ancestry and dependencies; stop on conflicts or unclear rights rather than bypassing gates.

## Stop/go criteria

- **GO for source selection proposal:** official rights evidence covers the intended use and technical feasibility is documented; owner still decides.
- **GO for local real-data implementation:** selected source and exact local-only use explicitly approved, with rights/provenance verified.
- **NO-GO for public real-data display, raw API activation, public image publication, Azure staging/production:** all require their own approvals and applicable rights. C2B-31 stays deferred.

**End-state:** Prefer a credible, licensed source decision and a safe, minimal local implementation plan over an unlicensed but working chart. Report any unresolved legal or provenance question clearly and do not infer authorization from free API access.
