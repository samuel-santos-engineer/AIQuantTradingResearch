# Release 1.15 WP01 - Engineering contract freeze proposal

Status: OWNER REVIEW REQUIRED. This is a proposed implementation contract, not a new effective governance amendment or evidence of feasibility. Read with the [baseline reconciliation](RELEASE_1.15_WP01_BASELINE_RECONCILIATION.md) and controlling historical crypto architecture. No source changes are authorized by this document.

## 1. Process and trust boundary

Preserve the proposed one-image Linux/amd64 topology: minimal PID1 supervisor, private non-root `research` C# process and distinct non-root `streamlit` Python process. The private HTTP API runs inside research, binds loopback only and is read-only. Only Streamlit serves public ingress. No Redis, external database, extra API process, paid resource or Windows runtime certification is introduced.

Research alone reads provider credentials, HMAC key, raw SQLite, journal/WAL, checkpoints and watermark. Streamlit receives a sanitized environment and transformed presentation DTOs only. Raw values must never reach public caches, errors, logs or file mounts. Loopback binding limits network reachability but does not itself enforce file, process, environment or secret isolation. The supervisor's access to inherited secrets is part of the trusted bootstrap boundary and requires explicit review.

Open mechanism decisions: numeric UID/GID ownership; supervisor privilege after bootstrap; cross-UID signaling/reaping; child-tree closure and restart overlap prevention; Azure mount ownership; startup health and shutdown deadlines. A distinct unprivileged supervisor cannot be assumed able to signal arbitrary other-UID children. Do not silently add capabilities or revive the old guardian architecture. Independent bounded restart backoff, roughly 30-second checks/three failures/90-second startup grace remain qualification hypotheses. Supervision does not guarantee recovery from container OOM or F1 suspension.

## 2. Private data and atomic publication

- Instruments: BTC/USD and ETH/USD. Use actual completed, UTC-aligned one-minute candle-opening timestamps. Reject incomplete, duplicate/conflicting, malformed and impossible OHLCV observations; never fill gaps with synthetic candles.
- Retain at most 1,000 actual candles per instrument; presets count observations, not elapsed minutes. Partial startup history is visible only with truthful readiness. The exact initial API behavior with zero retained rows requires the decision in section 6.
- Sole SQLite writer atomically commits candle changes, indicator/classification values and validity, transition events, continuity segment, correction/recovery intent and per-instrument version. Publish in-memory snapshots only after commit.
- Shared dataset generation UUID persists across normal restarts. Versions are compared only within that generation. New dataset/verified rollback changes generation; correction changes affected committed version and validity, not fabricated history.
- Checkpoint after five successful instrument commits is the configurable initial proposal. Replay must reproduce independently specified results. Protect active recovery evidence even when ordinary correction history is compacted (suggested 20 per instrument).
- Outside-SQLite watermark is atomically replaced after snapshot commit. It cannot be in the same transaction as SQLite. DB ahead requires validation of unwitnessed commits; watermark ahead/contradictory lineage requires investigation and Recovery. A failed watermark write means Degraded protection and bounded retry, not a false claim that a committed transaction failed. Coordinated rollback can remain undetected: this is best-effort rollback evidence.
- Recover only from sufficient evidence; otherwise restart the affected continuity segment and rewarm. Quarantine uncertain research publication, retain independently validated history and avoid pruning recovery evidence.

The existing factory's DELETE journal policy is the starting point for investigation, not a proven Azure storage selection. WAL on an unknown/shared filesystem is not an automatic improvement. The eventual decision must name mount type, locking, flush, rename, journal permissions and crash behavior. No migration of the existing dataset database is authorized here.

## 3. Research, continuity and transformed display

Private research uses raw private OHLCV. Preserve SMA20, EMA20, Bollinger20x2, RSI14, MACD12/26/9 and ATR14 semantics; warmup values remain null. Existing fixed-point arithmetic is a reuse candidate, but its synthetic identity/hour-grid validation is not the minute-input contract.

Trend sums the close-versus-EMA, MACD-histogram and RSI votes. Bullish >=2, Bearish <=-2, otherwise Neutral. Proposed equality closure: equal close/EMA, zero histogram and RSI at exactly 45 or 55 contribute zero. This closure and all numerical details below need explicit owner acceptance before golden vectors are frozen.

Regime: 100 consecutive warmup observations followed by 300 prior consecutive valid calibration observations; first eligible evaluation is candle 401. Exclude the current observation from calibration. Precedence is readiness, Volatile, Trending, Ranging, Undetermined. Use the architecture's strict percentile comparisons and direction alignment. Gap/correction invalidation must restart/reconstruct continuity honestly; pruning must not accidentally reseed recursive state from only the visible window.

Numerical decisions still open: normalized ATR and Bollinger width definitions, five-candle EMA slope normalization, percentile interpolation/rank, rounding/precision/bounds, equality and zero-denominator behavior, persisted checkpoint state and replay tolerance. Proposed direction is to reuse independently verified deterministic arithmetic where applicable; no copied Release 1.14 vectors establish new regime correctness. Do not choose these details merely to make a test pass.

After research, transform OHLC and every price-valued indicator using one positive daily UTC HMAC-SHA256-derived factor shared by both instruments, approximately 0.90-1.10. RSI remains unchanged. Never return the factor/key or imply transformed prices are actual quotes or an anonymity/rights guarantee. Exact HMAC message/domain, integer mapping, rounding and key-rotation identity remain unresolved. Volume units and treatment require an explicit schema statement. Rotation must not allow different transformed arrays under the same cache identity; choose a documented identity amendment or generation policy before implementation.

## 4. Proposed language-neutral schema 1.0

This defines required semantics for review. Wire spelling, decimal encoding and limits are proposed closures, not assertions that an existing schema file implements them.

| Envelope/collection | Proposed rule |
| --- | --- |
| `schemaVersion`, `type`, `instrument` | Version `1.0`; `full` or `unchanged`; exact requested BTC/USD or ETH/USD |
| `requestEpoch` | Echo the bounded per-instrument session epoch; client accepts only its current outstanding request |
| `generation`, `version`, `displayDate` | Canonical UUID, monotonically increasing instrument commit identity within generation, UTC transformation day; exact identity required before accepting unchanged |
| Full candle arrays | All retained actual rows up to 1,000, ordered unique minute UTC openings; finite OHLCV and valid OHLC relationships, no fabricated gap rows |
| Full analytics | Same length/timestamp alignment as candles; nullable warmup/invalid values with explicit validity, fixed indicator parameters and algorithm identity |
| Full annotations | Complete authoritative transition events, research-unavailable intervals, market-data gaps and per-candle classification validity |
| Status envelope | Fresh per-request status and observation timestamps; independent research/freshness/integrity dimensions; connectivity belongs to the client session |
| Unchanged | No candle/analytics/annotation arrays; only exact accepted identity may update status; otherwise obtain full replacement |

Proposal: encode versions as canonical unsigned decimal strings to avoid JavaScript integer precision loss; specify public decimal precision and encoding with cross-language fixtures before source work. Retain UTC `Z` timestamps without local-time ambiguity. Proposed bounded transport: 4,096 request bytes and 2 MiB decoded response bytes, maximum 1,000 candle rows, with separately frozen event/interval counts and string limits before acceptance. These are review values, not measured adequate limits or production enforcement. Oversized payloads fail whole-response validation; do not truncate authoritative collections. Timeout starts with the architecture's configurable five seconds and no immediate retry.

Compatible unknown optional fields are ignored by the consumer after bounded parsing; missing required fields, unsupported breaking versions and malformed known fields reject the entire response. Server projection is a strict public DTO whitelist. Accepting unknown optional fields must never become permission to serialize raw private extension data, render unknown HTML, or log arbitrary payloads. Reject duplicate object keys, invalid UTF-8, non-finite numbers, invalid null placement, identity mismatch, bad ordering/alignment or inconsistent annotations. No partial mixing with the previous snapshot.

An invalid response/timeout preserves the last accepted complete snapshot, marks connectivity honestly and does not invent freshness. `Current` is not synonymous with `Connected`. Research states are Ready/Recalibrating/Recovering/Unavailable; freshness Current/Stale/Unavailable; connectivity Connected/Reconnecting/Disconnected; classification validity Valid/Invalidated/Unavailable. Integrity Protected/Degraded/Recovery remains independent. The initial stale policy is >2 minutes without a newly completed candle, not elapsed time since the UI fetched a response.

Events are bounded transitions, not markers for every row. Identity includes generation/instrument/segment/UTC timestamp/type; retain an event while its own candle remains even if its predecessor was pruned. Full responses replace event collections. Research-unavailable and gap intervals are merged half-open UTC ranges within retained history. Gaps come from missing actual timestamps, never inferred beyond retained bounds. No rangebreak compression of real UTC gaps.

## 5. Cache and viewport ownership

Private transformed cache: instrument/generation/version/display date with an initial 12-entry LRU hypothesis plus byte bound. Public shared cache additionally distinguishes schema, contains immutable validated presentation values and never shared session health/preferences. Exact caps, reference accounting and overload behavior require measurement. A cache failure must not compromise ingestion/commit safety.

Session-local instrument/preset/viewport/request epochs are isolated. Default is 300 retained candles and Live follow. Manual pan/zoom enters Historical Exploration with absolute UTC ranges preserved across 60-second refresh. Return to Live and explicit preset selection may reset. At most one request in flight and one coalesced pending refresh per instrument; presentation interactions do not fetch.

Unchanged means no redraw. Changed/corrected snapshots replace affected authoritative collections. Generation/date/unsupported structural changes require full rebuild while restoring eligible absolute viewport and suppressing programmatic interaction echoes. Preserve partial-overlap ranges; if fully expired, explain expiry and offer Return to Live. Disconnection does not infer pruning. Instrument switches preserve each instrument's session state.

Browser interaction revisions, command epochs/acknowledgments and mount identity must prevent stale/remount events from overwriting a newer manual viewport. Settled-event debounce around 200-300 ms is distinct from immediate local visual feedback. Supported native APIs first; a narrow component only if necessary and separately authorized. No library fork, page DOM manipulation or injected workaround.

## 6. Decisions required before dependent source work

| Decision | Proposed disposition / owner |
| --- | --- |
| D1: cross-UID supervisor and secret inheritance | WP02 trust/lifecycle decision after bounded feasibility; no silent capabilities |
| D2: storage/mount/journal/checkpoint semantics | WP03 design, WP07 actual Azure qualification; no WAL assumption |
| D3: exact numerical/regime definitions | WP04 independent specification and golden vectors before implementation |
| D4: wire encoding/bounds/zero-row full behavior | WP05: propose empty full with truthful unavailable states; approve required names, limits and extension policy |
| D5: HMAC/key-rotation identity | WP05 explicit factor/rounding/rotation contract and independent vectors |
| D6: supported browser interaction channel | WP06 feasibility first; component scope requires a separate decision if native capture is insufficient |
| D7: resource budgets and F1 availability | WP07 measured limits; provisional timing targets are not SLAs or passed gates |

Owners here identify work-package responsibility, not invented approvals. Unresolved decisions block the affected implementation slice, not the publication of this planning PR. `PUBLIC_RUNTIME_VERIFIED=False`.


## WP09 narrow exception: non-durable existing F1 staging research

Owner-authorized implementation exception, effective on merge: the exact existing `aiqr115researchstagingwus27f5eabb5` App Service may opt into `AIQ_RESEARCH_STORAGE_SCOPE=azure-f1-staging-ephemeral` together with `AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING=1`. It is not a durable-storage acceptance or an extension to production/other apps. Defaults still reject App Service. Exact site and default hostname, nonempty instance identity, role instance0, App Service storage disabled, Linux UID1654 in all kernel UID fields and expected mount `/` are required. State is fixed at `/tmp/aiq-research`; only a writable container-root overlay with root `/`, major device0 and no shared/master propagation or nested database mounts is accepted. Unknown evidence, persistent/network/tmpfs mounts, alternative paths, symlinks and nonprivate/unwritable directories reject before SQLite opens.

This is explicitly disposable state. Instance replacement/restart may lose the complete SQLite/WAL/witness/D5 state. Missing or invalid database must remain unavailable; no fixture or automatic provider/bootstrap fallback. Controlled reinitialization uses the existing initializer with an authorized authentic private export/config, after stopping and verifying the old writer. No concurrent instance, deployment slot overlap, automatic failover or cross-instance ownership guarantee is added. Filesystem ownership is local-process only. The operator must stop/verify/start; role0 is not a global fence.

WAL, integrity witness, D5 HMAC, single-process lock, provenance, completed UTC semantics, C# analytics, API authentication and existing HTTPS/transport checks are unchanged. Durable storage qualification remains deferred with its prior requirements. The existing explicit App Service frontend-HTTPS/backend-HTTP transport is not end-to-end TLS and requires its existing exact-site HTTPS/bind assertions; strict UI-to-research public HTTPS remains mandatory. No silent transport downgrade is introduced.

Mountinfo plus platform settings are fail-closed checks, not cryptographic Azure identity or proof of physical backing/lifetime. Trusted platform/operator metadata and a non-malicious container administrator remain assumptions; symlink checks do not eliminate hostile concurrent rename/mount TOCTOU. No privileged/mount-capable workload or private-directory co-tenant is permitted. If the actual F1 root is not the accepted private writable overlay layout, startup rejects; do not broaden the allowlist without evidence/authority. Actual staging layout, bootstrap delivery, idle/cold-start and shared-plan capacity remain NOT_RUN. UTF-8/LF applies to this amendment. PUBLIC_RUNTIME_VERIFIED=False until separately authorized staging acceptance.
