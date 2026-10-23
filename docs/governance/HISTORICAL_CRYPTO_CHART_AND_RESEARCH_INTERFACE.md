# Release 1.15 — Historical Crypto Chart and Research Interface

**Status:** PROPOSED — owner review required; not effective or implementation-authorizing until separately approved through applicable governance.
**Scope:** Architecture and release-acceptance proposal only.
**Repository path:** `docs/governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md`
**Decision basis:** Owner-approved Release 1.15 architecture discussion, with remaining architecture options resolved by owner instruction to apply the assistant's recommended options. These latter resolutions are **proposals for review**, not claims of independently measured behavior.

## 1. Governance alignment: Docker-first and dual-process

**The governance conflict identified when this document was first drafted has been resolved by merged PR #362** (squash commit `7247eda440f26c0fb973c604b4466eb944b5343a`). The amended `docs/governance/SYNTHETIC_PUBLIC_PRODUCT_SCOPE.md` establishes the **Release 1.15 historical BTC/USD and ETH/USD research/chart product direction** in place of the former synthetic-only future objective. The amended `docs/governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md` explicitly permits a **release-specific supervised two-process runtime** in **one immutable Linux Docker image**. Docker-first governs packaging, qualification and deployment; dual-process governs separation of private research from public presentation. They are complementary, not alternatives.

The agreed Release 1.15 architecture is one Linux/amd64 Docker image with a minimal PID1 supervisor managing a private non-root C# ingestion/research service and a public non-root Streamlit/Plotly service. The private read-only presentation API is localhost-only; Streamlit alone serves public ingress. The amended Docker-first policy preserves exact-image qualification, Linux security isolation and resource testing. The historical Release 1.14 synthetic artifacts remain historical; C2B-32 remains ABORTED and C2B-31 remains DEFERRED. **PR #362 resolves product-direction and runtime-policy alignment, not provider rights or deployment readiness.** This architecture document remains for owner review and does not itself authorize provider activation, deployment, image promotion or waiver of existing G-B0–G-B7, WP07, security, provenance, dataset-readiness or `PUBLIC_RUNTIME_VERIFIED=False` controls.

**Implementation planning boundary:** Release 1.15 engineering milestones focus on building and testing the agreed chart and research architecture. This document does not add a provider-licensing review milestone or licensing-specific implementation approval gate. It does not assert that transformed data, authentication or caching grants redistribution rights; existing repository-wide governance and applicable provider terms remain independent of this architecture specification.

## 2. Product objective, limits, and non-goals

Future candidate: educational, read-only historical candlestick and technical-research UI for **BTC/USD and ETH/USD**, with truthful source, transformed-price, continuity, readiness and availability labels. Not an execution platform, investment-grade feed, trading signal service, financial advice, user-account system or historical archive. Initial deployment target is Azure App Service **Linux F1**, best effort with possible idling/restarts; no paid add-on, Redis, external database, extra API process, or Windows runtime certification.

Only the latest **1,000 actual retained completed one-minute candles per instrument** are authoritative in SQLite; no fabricated candles, unlimited archive, or higher-resolution provider history. The chart's 100/300/1,000 presets count **retained candles**, not elapsed minutes.

## 3. Runtime, security and network

- **Docker-first and dual-process together:** one immutable Linux/amd64 Docker image with a minimal PID1 supervisor and two supervised processes: private C# ingestion/research service and public Streamlit/Plotly UI. This release-specific runtime is **already permitted by the Docker-first policy as amended in merged PR #362**; it is not a second container or a replacement for Docker-first qualification.
- Run as distinct non-root identities `research` and `streamlit`. Private SQLite files (including WAL/journal), checkpoints, high-watermark, API credentials and HMAC secret are readable only by research. Public process receives a sanitized environment and cannot access private storage/secrets.
- C# serves a **loopback-only**, read-only transformed-snapshot presentation API; no external exposure of the private API. Public ingress is Streamlit only. Validate binding and permissions in the actual Linux container.
- Supervisor checks local health roughly every 30 seconds, uses three-failure detection, independent bounded exponential process-restart backoff and roughly 90-second startup grace; actual values configurable and subject to qualification. A Streamlit outage need not stop research. Azure F1 availability is not guaranteed.

## 4. Ingestion, source and continuity

- Implement the Twelve Data Basic candidate adapter for completed BTC/USD and ETH/USD one-minute OHLCV, using configured credentials and measured plan capabilities; keep provider integration separately configurable and testable without live calls. Use a fixed configurable post-minute UTC grace (initial hypothesis ~15 seconds, to verify), completed/aligned candles and canonical UTC candle-opening timestamps.
- Respect measured/verified provider request credits, prioritize live completed candles, use spare credits for bounded backfill, and fair round-robin between instruments. No burst retry strategy. Never assume quota, symbols or coverage from the proposed design.
- Validate OHLCV, timestamps and completeness before commit. Recover provider gaps when feasible; irrecoverable discontinuities start a new research continuity segment. Never synthesize missing market observations.
- Per-instrument freshness becomes **Stale** after >2 minutes without a newly completed candle (initial policy), suspending new research decisions while allowing independently validated history to remain visible.

## 5. Authoritative storage, atomicity and recovery

- SQLite is the **sole authoritative writer**. Each candle commit atomically records OHLCV, indicators, classification and validity, continuity metadata, transition events and monotonic per-instrument snapshot version. In-memory state advances **only after** successful commit.
- Retain 1,000 latest candles per instrument. Incremental indicators use a private checkpoint after every five successful commits per instrument (configurable), atomically replaced; replay is deterministic with numeric tolerances. Protect required evidence from pruning.
- Persist a dataset **generation UUID** shared by instruments. It survives normal restart, changing only for a genuinely new dataset or verified rollback. Version comparisons are meaningful only inside a generation.
- Maintain an atomically replaced **outside-SQLite high-watermark** after each successful snapshot commit, with generation and per-instrument commit identity/checksum. SQLite ahead of watermark may reflect a crash or failed watermark write; validate unwitnessed commits. Watermark ahead of SQLite may indicate rollback; investigate. A watermark write failure degrades protection and permits valid SQLite commits with bounded retry; contradictory evidence enters Recovery. Coordinated rollback of DB and watermark may be undetectable: protection is best effort.
- Track Protected/Degraded/Recovery integrity conditions; on uncertain lineage quarantine research publication, retain only independently validated history and safe ingestion if possible, and pause affected ingestion if pruning would destroy recovery evidence.
- Corrections discovered through ordinary polling (no extra correction-polling budget) compare OHLCV; commit correction/invalidation and recovery intent atomically, reconstruct deterministically and increment version. If replay evidence is insufficient, accept corrected candle but restart research continuity, never approximate. Keep compact private correction history (suggested 20/instrument), with active evidence protected.
- Before restoring publication, validate SQLite integrity, continuity, checkpoint, lineage, watermark and correction/recovery intent. If gaps cannot be repaired, rewarm the affected research segment.

## 6. Private research algorithms

All calculations use **real private OHLCV**, never transformed public prices.

Indicators: SMA(20), EMA(20), Bollinger Bands(20,2), RSI(14), MACD(12,26,9), ATR(14). Missing/warmup indicators are explicit unavailable/null, never invented.

Trend score = sum of three directional votes (close relative to EMA20, MACD histogram sign, RSI >55 / <45), in [-3,+3]. **Bullish** score >=2; **Bearish** <=-2; otherwise **Neutral**. Boundary/equality behavior and null-readiness must be deterministic in tests.

Regime readiness: initial **100 consecutive observations warmup**, then **300 prior consecutive valid observations** for percentile calibration; first eligible regime evaluation at the **401st** consecutive candle. Precedence: readiness -> Volatile -> Trending -> Ranging -> Undetermined.
- Volatile: normalized ATR14 > prior P80 **and** Bollinger width > prior P80.
- Trending: absolute trend score >=2, absolute five-candle EMA slope > prior P70, and direction aligned.
- Ranging: absolute score <=1 **and** absolute EMA slope < prior P40.
- Otherwise Undetermined. Any discontinuity/correction invalidating calibration triggers appropriate recalibration or reconstruction.

## 7. Transformed public data and truthful disclosure

- Private service transforms OHLC and all **price-valued** indicators after research calculation using a shared, positive, UTC-day HMAC-SHA256-derived factor in approximately **0.90–1.10**; RSI remains unchanged. Same daily factor for both instruments, and date is part of snapshot identity.
- Label values clearly as **transformed educational values, not actual BTC/USD or ETH/USD prices**. Transformation is a presentation design, not a representation of actual quotes or a grant of redistribution rights.
- UTC date rollover requires complete re-transformation/full snapshot replacement. A disconnected session preserves the transformation date of its last accepted snapshot until a valid replacement arrives.
- Never expose raw private candles, provider key, HMAC secret, checkpoint, SQLite or research-only metadata through the public process.

## 8. Private API, snapshots and compatibility

- Read-only localhost JSON contract, **canonical language-neutral schema** (conceptual initial `1.0`), validated on C# output and Python input. Compatible unknown optional fields are accepted; missing required fields, unsupported breaking versions, mismatched lengths/identities or malformed data are rejected.
- Full snapshot includes all **1,000 retained candles**, aligned indicators, validated transition events, research-unavailable intervals, market-data-gap intervals, classification validity and independent health/status metadata. Streamlit slices locally to 100/300/1,000 (default 300).
- Conditional request identifies generation UUID, per-instrument version and UTC display date. `type:"unchanged"` omits arrays but updates status only when the session's accepted identity exactly matches; otherwise require `type:"full"`.
- Private transformed-array cache is bounded by per-instrument/generation/version/UTC-date identity, initial LRU hypothesis 12 entries plus configurable byte cap; this is separate from the Streamlit shared cache. Health is computed fresh per request.
- Strict whole-response validation checks envelope/schema, requested instrument and request epoch, generation/version/date, ordered unique UTC candle openings, aligned arrays, finite numbers or allowed JSON nulls, event identity/ordering and interval bounds. Reject the entire invalid response; never partially mix arrays, annotations or generations.
- Maintain current plus bounded older-compatible and negative fixtures, with cross-language contract tests; no runtime schema registry.

## 9. Status semantics and public historical annotations

- Independent dimensions: research **Ready / Recalibrating / Recovering / Unavailable**; freshness **Current / Stale / Unavailable**; session connectivity **Connected / Reconnecting / Disconnected**; classification validity **Valid / Invalidated / Unavailable**. These are not interchangeable. Disconnection alone does not invalidate history or establish freshness.
- Optional historical classification overlay **off by default**; summary displays latest classification. Draw only trend **transition** markers (Bullish/Bearish/Neutral) and SegmentStart, not a marker on every candle; regime in tooltip.
- Persist bounded validated transition events atomically with classifications. Event identity includes generation/instrument/segment/UTC timestamp/type; retain an event while its own candle remains in the 1,000-candle window, even after predecessor pruning. Corrections invalidate/reconstruct affected events. Full snapshots replace event collections; never append blindly.
- Derive bounded, merged research-unavailable intervals from committed per-candle validity (not a second interval table), using half-open UTC candle-opening ranges `[start,end)` inside retained history. Independently derive market-data-gap intervals from missing retained UTC timestamps; never fabricate candles or infer gaps outside the retained boundary. Plotly must preserve real UTC axis gaps, without rangebreak compression.
- In Recovery, retain independently validated historical events and suppress invalidated ones with truthful status.

## 10. Chart interaction and session reconciliation

- Streamlit/Plotly candles, volume, selected indicators and status. Fixed 100/300/1,000 retained-candle presets, default 300; show count, elapsed UTC range and gaps. Chart zoom/pan explores only the loaded 1,000; no additional provider/API requests from interaction.
- Default **Live follow**. Manual zoom/pan changes to **Historical Exploration** and preserves absolute UTC viewport across 60-second refresh. Explicit Return to Live or preset selection resets. Viewport and preferences are per instrument/per session.
- When retention prunes a selected viewport, preserve UTC coordinates and show surviving overlap; if no overlap, explain expiry and offer Return to Live. While disconnected, do not infer pruning from absent updates.
- On generation change, clear all lineage-dependent data and annotations and accept only a new-generation validated full snapshot; preserve eligible absolute viewport/preferences, explain dataset reset and never merge generations.
- Per-instrument session request epochs and generation-aware acceptance reject out-of-order responses. One request in flight per instrument per session; coalesce refresh ticks to at most one pending follow-up. Zoom/pan itself makes no request.
- Initial configurable localhost HTTP timeout **5 seconds**, no immediate retry; normal scheduled reconciliation resumes. On timeout, label Disconnected and preserve last accepted snapshot for the session lifetime with last sync UTC, sync age, and last completed candle UTC. Connectivity alone does not establish Current freshness.
- During reconnection keep the last-known chart visibly labeled until a complete validated authoritative replacement is accepted; atomically replace candles, indicators, markers and both interval collections. A truly matching `unchanged` may restore connectivity and update status without redrawing.
- Feasibility spike on pinned Streamlit/Plotly must verify event capture, `uirevision`, Return to Live, instrument switching, retention and remount behavior. Native chart interaction is preferred; a narrowly scoped component is allowed only if necessary, and only as an interaction controller (no provider fetching/research).
- Browser handles immediate local zoom/pan; report settled absolute UTC viewport after ~200–300 ms debounce. Explicit Return to Live is immediate. Per-instrument interaction revisions, command epochs, last acknowledgements and mount-instance tokens prevent stale events/programmatic relayout from overriding user input. Streamlit session state is authoritative for accepted mode, viewport, preset, commands and snapshot; browser temporarily owns unacknowledged interaction. Session state is not persisted to SQLite.
- Stable logical chart identity per instrument across reruns; do not assume a Plotly key alone guarantees viewport persistence. Reuse figure when safe but treat every accepted snapshot as complete authoritative state. `unchanged` means no redraw; correction/annotation changes replace affected collections. Mandatory full figure rebuild for generation/date change, unsupported structural change or uncertain consistency, preserving eligible viewport and suppressing false interaction events. Presentation-only changes do not call the API.

## 11. Performance, resource protection and multi-session policy

- Bounded performance acceptance measures initial chart, instrument switch, changed/unchanged 60-second refresh and local viewport responsiveness with deterministic **1,000-candle** fixtures. Measure separately C# snapshot preparation, Streamlit reconciliation/figure preparation and browser rendering. Provisional warm targets: initial usable chart <=5 s; instrument switch with session snapshot <=2 s; changed refresh rendering <=2 s; local visible viewport feedback <=200 ms (separate from settled-event debounce). **Targets are unverified hypotheses, not F1 SLAs or CI hard gates until baselined.**
- **Recommended closure of capacity decision:** design for small measured multi-user use; exercise 1, 3 and 5 simultaneous sessions. Declare supported capacity from actual Azure F1 evidence, not an assumed guarantee. No admission-control service in this release.
- Protect in order: SQLite integrity/recovery evidence, completed-candle ingestion, private research continuity, public API, chart presentation. Under pressure shed nonessential caches/figures and coalesce or defer presentation work before compromising ingestion or transaction safety. Do not mislabel historical research as invalid merely because rendering is delayed. Actual container OOM cannot be guaranteed recoverable.
- Shared Streamlit immutable validated presentation snapshot cache keyed by instrument/schema/generation/version/UTC transformation date; independent session-local viewport, instrument, preset, connectivity, reconciliation and interaction state. No cross-session mutation, health sharing or user-state leakage.
- **Memory-budgeted LRU with session-reference awareness**: conservative estimated byte accounting for arrays/events/intervals and overhead; evict unreferenced least-recently-used entries first. Cache eviction does not guarantee immediate memory reclamation if sessions retain references. If admission exceeds budget, bypass shared cache instead of duplicating data. Account for Plotly figures and private C# cache separately; no unbounded historical snapshots.
- One accepted immutable snapshot **per instrument per active session**. Keep disconnected last-known validated chart for the life of that session. Rely on Streamlit's supported session lifecycle for termination and cleanup; remove redundant optional figures/objects during idle periods where supported. No custom forced-expiration manager or cross-session session enumeration assumptions. Ended sessions have no restoration guarantee.
- **Recommended closure of overload behavior:** define Normal/Elevated/Critical resource modes from measured, configurable thresholds; on Elevated evict optional caches and suppress unnecessary redraw; on Critical defer costly presentation and expose truthful degraded/unavailable status while retaining core safe persistence if possible. No arbitrary CPU throttling that risks missed ingestion, no uncontrolled request queues, no promise of recovery after OOM. Verify thresholds empirically on F1.
- **Recommended closure of performance regression gating:** deterministic correctness, security, contract and browser assertions are hard CI gates. Collect repeated timing/memory baselines on pinned CI environments with generous, calibrated regression alerts rather than unmeasured absolute hard thresholds. Azure F1 warm/cold and 1/3/5-session acceptance measured separately; if capacity/performance insufficient, document NO-GO or reduced supported capacity rather than weakening integrity.

## 12. Operational observability and failure semantics (recommended closure)

- Structured bounded logs with UTC timestamps, generation/instrument/version/segment and correlation identifiers; no provider keys, HMAC material, private raw candle payloads or secret-bearing URLs. Bound disk usage/rotation on F1.
- Track per instrument last completed candle, last successful provider poll, consecutive valid observations, readiness, freshness, ingestion gaps, request credits and backfill attempts, correction/recovery state, commit/checkpoint/watermark outcome, private API health and latency.
- Track Streamlit API timeouts/validation rejects, `unchanged` vs `full`, cache hits/evictions/estimated bytes, active-session observations only through supported APIs, figure preparation and browser performance; track process restarts and container memory/CPU when measurable.
- Public-facing diagnostics are sanitized, status-driven and actionable (stale, disconnected, recalibrating, recovering, generation reset, retention expiry); internal logs retain detail. No public debug endpoints, credentials or sensitive internal paths.
- **Recommended closure of alerting:** no additional paid monitoring service or promise of 24/7 alerting. Use existing Azure/container and CI logs and bounded health diagnostics; investigate sustained critical memory, repeated restarts, integrity contradictions or stale ingestion. Future external alerting requires separate authorization.

## 13. Qualification, release gates and decision completion

Mandatory future PR CI (subject to actual repository protection configuration):
- Deterministic C#/Python canonical JSON contract tests, current/compatible/negative fixtures, algorithm/warmup/continuity/correction/recovery tests and cross-language numeric alignment.
- Real Linux/amd64 Docker smoke test of PID1 and both non-root processes, loopback-only private API, public Streamlit HTTP, inaccessible SQLite/secrets from public identity, interrupted-private-process recovery and synthetic isolated test SQLite; test mode never deployed as production.
- Minimal headless Chromium test against actual container verifying BTC/ETH candles, indicators, disconnection/last-known retention and core chart controls. Avoid pixel-perfect brittleness and live-provider dependency. Capture bounded diagnostics on failures.
- Bounded Plotly interaction feasibility spike before committing to a custom component; validate settled viewport, command epochs, remount, pruning, date rollover, corrections and return-to-live.
- Resource/performance test 1/3/5 concurrent sessions with worst-case retained 1,000 candles, memory accounting, no cross-session interference and protected ingestion. Record warm vs cold measurements and variability; CI is not F1 capacity proof.
- Before any future deployment: independently authorize and qualify the **exact immutable Linux image digest**; verify each Azure F1 site, configured provider connectivity and rate-limit behavior, network isolation, permissions, UTC/date rollover, stale/recovery statuses, browser usability and rollback. Preserve all existing governance gates. **No live feed, registry push, Azure deployment, API activation, merge or promotion is authorized by this documentation PR.**

### Final proposed release acceptance / NO-GO rules

A future Release 1.15 implementation is **NO-GO** if any of the following hold: private credentials/raw prices are exposed; SQLite lineage/recovery is unsafe; fabricated or incomplete candles are shown as valid; classifications are published without sufficient continuity; generation/date/version snapshots mix; browser viewport/reconnection invariants fail; Linux image/security/contract/browser tests fail; actual F1 resource evidence is insufficient; or required owner release gates remain unmet.

This closes the remaining **architecture-policy questions** by recommended bounded choices. It does **not** claim all technical feasibility, provider quotas, Streamlit APIs, performance thresholds or F1 capacity have been verified. These are implementation validation concerns, not reopened architecture decisions.

## 14. Decision ledger and review checklist

| Area | Decision | Status |
|---|---|---|
| Instruments and history | BTC/USD + ETH/USD; 1-minute completed candles; latest 1,000 each | Owner discussion |
| Runtime | Docker-first single Linux F1 image, PID1 + private C# and public Streamlit | Aligned with Docker-first policy by merged PR #362 |
| Storage/integrity | Atomic SQLite, generation/version, checkpoints, outside-DB watermark, deterministic recovery | Owner discussion |
| Research | Private indicators, trend/regime precedence and 401-candle readiness | Owner discussion |
| Public data | Daily HMAC transformed-price presentation, clear non-actual labels | Architecture settled; implement and validate |
| Chart | Full snapshots, transition/interval history, Live/Historical UTC viewport | Owner discussion |
| Reconciliation | Strict versioned schema, atomic replace, disconnected last-known, epochs | Owner discussion |
| Validation | Cross-language CI, Docker and Chromium smoke, bounded interaction spike | Owner discussion |
| Performance | Bounded measurements, provisional targets, F1 separately measured | Explicitly selected B |
| CPU/memory | Protect authoritative research; bounded resource shedding | Explicitly selected B |
| Multi-session | Shared immutable snapshots, session-local preferences | Explicitly selected B |
| Cache eviction | Byte-budgeted LRU with session-reference awareness | Explicitly selected B |
| Idle sessions | Framework lifecycle + bounded idle safeguards | Explicitly selected B |
| Capacity | Measure 1/3/5; no unverified concurrency guarantee | **Recommended B closure for owner review** |
| Overload | Normal/Elevated/Critical, degrade presentation first | **Recommended B closure for owner review** |
| Performance gating | Baseline-calibrated CI regression, hard functional gates | **Recommended B closure for owner review** |
| Observability | Bounded structured diagnostics, no paid monitoring dependency | **Recommended B closure for owner review** |
| Release gate | Security, integrity and technical qualification criteria | **Recommended closure for owner review** |

**Owner review checklist:**
- [ ] Confirm the document faithfully captures the agreed architecture and recommended closures.
- [x] Confirm Release 1.15 product-direction amendment merged in PR #362; preserve Release 1.14 synthetic history.
- [x] Confirm Docker-first policy expressly permits the Release 1.15 supervised dual-process architecture in one immutable image (PR #362).
- [ ] Authorize any future feasibility spike, implementation PR, image qualification and deployment separately.

**Change control:** This is a **documentation-only proposal**. Its review/merge does not itself grant implementation, provider access, public data redistribution, registry publication, Azure deployment, governance-gate waiver or release approval. No additional licensing prerequisite is established by this document.

## Release 1.15 superseding dual-container deployment decision — 2026-10-23

**Status:** Owner-selected architecture amendment for planning; publication does not itself authorize application implementation, images, deployment or public runtime activation. This section supersedes all prior Release 1.15 references within this document to **one image**, **a PID1/root supervisor running both roles**, **independent in-container role restarts**, **same-container file/UID boundary** and **localhost-only UI-to-research API**. The rest of this architecture's product, numerical, integrity, UI and bounded snapshot decisions remain in force.

### Four Web Apps, two Docker images per environment

| Environment | Existing UI app | New research app | Existing shared F1 Linux plan |
| --- | --- | --- | --- |
| Production / West Central US | `aiqr112wp035ec325382770` | `aiqr115researchprodwcus7f5eabb5` | `asp-aiq-r112-wp03-wcus-5ec325382770` |
| Staging / West US 2 | `aiqr112wp05wus27f5eabb5` | `aiqr115researchstagingwus27f5eabb5` | `asp-aiq-r112-wp05-wus2-7f5eabb5` |

The two research apps were created as **empty Linux App Service resources**, each on its environment's existing F1 plan (PR #381, pending merge). Their management state Running does not establish a working application, private ingress or sufficient workload compute. Each plan now hosts two apps sharing the same F1 capacity. Keep the existing UI app/resource intact during transition.

Research image: one main non-root C# process; only its container has raw completed candles, SQLite/WAL, watermark/checkpoints, provider credentials and HMAC key; its service computes indicators, classifications, transforms and bounded read-only snapshots. UI image: one main non-root Streamlit/Plotly process; no raw storage or secret mounts; handles only validated transformed responses and session-local historical viewport. No custom C supervisor, shell-managed dual-process restart or elevated same-container privilege-switching is required. Each app's container/platform handles lifecycle independently; outage of one role need not terminate the other.

### Explicit network trust boundary and qualifications

The research API is **not** localhost-only when called from another Azure Web App. Research's `127.0.0.1` is never the UI app's loopback. The UI-to-research channel is an untrusted network boundary and must provide HTTPS, server identity validation, service caller authentication, authorization limited to read-only transformed bounded contract operations, request limits and safe error responses. No external user may be authorized to query even transformed research snapshots directly unless independently authorized as a new public product surface. A public DNS hostname is not proof of a private Azure network. Binding research to its container interface for platform routing is not authorization to make the API anonymously available.

**F1 feasibility is deliberately open:** do not assume VNet integration, private endpoints, managed identity token support in the exact configuration, authenticated upstream proxy, access restrictions or any particular network security feature without evidence. WP02 is responsible for the minimal threat model and F1-compatible service-authentication/ingress candidate; WP05 owns cross-app authenticated transport integration, preserving D4 schemas; WP07 must prove actual behavior including anonymous and directly invoked rejection, TLS, invalid/stale credential handling, key rotation/outage cases, request bounds and sensitive-data negatives. If an acceptable mechanism cannot be validated under current F1 constraints, block research API activation and report the tradeoff rather than silently expose raw data or switch plans.

The accepted D4 `POST /v1/research/snapshot` message shape, full/unchanged identity, data bounds and response-validation semantics are retained; its old `http://127.0.0.1:52731` transport configuration and local 5-second roundtrip hypothesis are superseded, with cross-app endpoint, credential and timeout settings to be established using measured evidence. Authentication headers and network status handling are transport-level concerns, not permission to add raw fields to the public response or leak credentials through logs. Preserve disconnect/last-known chart behavior under research cold start, outage or idling.

### Storage, resource and deployment integrity

SQLite remains research-exclusive; the UI has no research filesystem, no provider calls and no database writer rights. A single main research container process is **not** sufficient proof of a sole writer when Azure replaces instances or deployments overlap. WP03/WP07 retain authoritative writer exclusion, persistent evidence, recovery and Azure mount semantics; do not declare F1 local storage durable until tested. Qualify both containers concurrently on their shared F1 plan and test resource contention, independent lifecycle, truthful stale states and 1/3/5 UI sessions without guaranteeing availability. Record and promote **two distinct immutable image digests** and their compatibility pairing, not one combined image. Existing G-B0–G-B7, Release 1.14 preservation, provider non-activation and `PUBLIC_RUNTIME_VERIFIED=False` remain unchanged.

### D1 owner disposition and remaining implementation work

Owner instruction to adopt dual Docker across two Azure Web Apps replaces accepted WP01 D1-A **only as to runtime topology, privileged supervision, restart and loopback transport**. D3 fixed-point algorithms, D4 wire semantics and D5 HMAC transformation remain accepted; D2 durability, F1 transport, and WP07 live validation are not fabricated as completed. Subsequent WP02 exact-path implementation plan must be revised to two single-process images and remove proposed C-supervisor-related paths; pending local WP02 files must be retained and reconciled, never reset or deleted. Do not preempt WP05's schema ownership or WP03's writer integrity obligations.

**Governance consequence:** Docker-first is preserved. The exception is no longer “dual process in one image” but “two Docker images with one primary process each”; cross-app API authentication becomes required because the network trust boundary has changed. Product scope and licensing prerequisite policy do not change. PR #381 is infrastructure evidence only and remains independently reviewable.
