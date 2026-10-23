# Release 1.15 WP01 - D1-D7 decision register

Date: 2026-10-22. Owner-review assessment at main `3880f5afee59e3d970ceacd6c3e9abfd796a0105`. See [closeout](RELEASE_1.15_WP01_CONTRACT_CLOSEOUT.md) and [qualification matrix](RELEASE_1.15_WP01_DOWNSTREAM_QUALIFICATION_MATRIX.md). `PUBLIC_RUNTIME_VERIFIED=False`.

## Status semantics

FROZEN means an exact accepted production implementation contract, not a passed platform test. PROVISIONAL means inherited architecture and/or local feasibility exists but the complete decision needs a scoped disposition. OPEN means required semantics remain unspecified or unapproved. The 2026-10-23 [foundational proposals](RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md) now specify exact candidates for D1/D3/D4/D5; their unapproved status is unchanged. These are proposed register dispositions for review, not newly granted implementation authority. No complete production D-decision is labeled FROZEN merely because a fixture passed. Existing architecture invariants remain binding.

| Decision | Status | Primary downstream owner | Production contract frozen here |
| --- | --- | --- | --- |
| D1 | PROVISIONAL | WP02 / #368 | No |
| D2 | PROVISIONAL | WP03 / #369 | No |
| D3 | OPEN | WP04 / #370 | No |
| D4 | OPEN | WP05 / #371 | No |
| D5 | OPEN | WP05 / #371 | No |
| D6 | PROVISIONAL | WP06 / #372 | No |
| D7 | PROVISIONAL | WP07 / #373 | No |

## D1 - Runtime trust and lifecycle

**Status: PROVISIONAL. Primary owner: WP02 (#368).**

Sources: [architecture](../../governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md), section 3 Runtime, security and network; [engineering proposal](RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md), section 1 Process and trust boundary; [runtime evidence](prompters/release-1.15-wp01-runtime-feasibility-evidence.md), Executed profile and boundary observations and Remaining recovery and security obligations.

Rationale: separate non-root children, tested file denials and bounded fixed-role restart support feasibility. Fixture PID1 was root with default capabilities; owner acceptance of that trust model is absent. Preserve single Linux image, two application roles, private loopback API, secret/environment separation and wait-before-replace intent as inherited requirements. No normative mechanism is newly frozen.

Owner choice required: review [D1-A](RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md#d1-proposed-runtime-contract), the explicit trusted retained-root PID1 candidate with a four-capability runtime set, empty-capability research/UI roles, inherited namespace restrictions, closed FD/environment boundaries, process-group plus role-UID/pidfd closure and fail-closed deadlines/backoff. This is distinct from the fixture's default capabilities. Alternative unprivileged PID1 requires a separately reviewed privileged role launcher. Accept/amend/reject the trust and availability tradeoff before WP02 implementation; do not infer approval from feasibility. Exact capability/seccomp/kernel/Azure conformance remains downstream.

Acceptance: real final-image UID/GID/group/capability/environment/FD/file/socket negatives; adversarial cross-role access and independently witnessed tree termination/reaping before replacement; bounded peer-independent restart and shutdown under fault schedules. Non-claims: no hostile descendant containment, root least-privilege acceptance, OOM recovery or Azure process proof from S2.

## D2 - Authoritative storage and continuity

**Status: PROVISIONAL. Primary owner: WP03 (#369).**

Sources: [architecture](../../governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md), sections 4 Ingestion and 5 Authoritative storage; [engineering proposal](RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md), section 2 Private data and atomic publication; [runtime evidence](prompters/release-1.15-wp01-runtime-feasibility-evidence.md), Remaining recovery and security obligations.

Rationale: local SQLite integrity and denial probes support a fixture boundary, not production lineage or durability. Inherited contract: at most 1,000 actual completed aligned minute candles per instrument, single atomic SQLite writer, publish memory only after commit, persistent generation/version, honest gaps/corrections and protection of recovery evidence. No new journal/mount/protocol is frozen.

Unresolved decision: journal/mount/flush/rename guarantees, atomic transaction contents, outside-DB watermark validation and Degraded/Recovery transitions, zero-history ingestion behavior, sufficient replay/checkpoint state, evidence-aware pruning and unrecoverable continuity reset. Checkpoint cadence five and correction-history twenty remain proposals, not accepted constants. WP04 supplies algorithm state requirements; WP07 qualifies actual Azure semantics.

Acceptance: offline completed/future/misaligned/impossible/duplicate candle vectors, quota fairness; crash at commit/checkpoint/watermark/prune boundaries; DB-ahead/watermark-ahead/corruption/correction matrix, replay after pruning, denied sidecar access. Then separately authorized real mount persistence/locking/flush tests owned by WP07. Non-claims: no real Twelve Data capability measurement, WAL suitability, Azure fsync/rename or coordinated-rollback detection from S2.

## D3 - Deterministic private research

**Status: OPEN. Primary owner: WP04 (#370).**

Sources: [architecture](../../governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md), section 6 Private research algorithms; [engineering proposal](RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md), section 3 Research, continuity and transformed display; [acceptance risks](RELEASE_1.15_WP01_ACCEPTANCE_RISKS.md), Technical risk and decision matrix.

Inherited requirements: raw private OHLCV SMA20, EMA20, Bollinger20x2, RSI14, MACD12/26/9 and ATR14; explicit null warmup; trend sum of close/EMA, histogram sign and RSI votes, Bullish >=2/Bearish <=-2/otherwise Neutral. Regime uses 100 warmup plus 300 prior valid consecutive observations; first eligibility candle 401, current observation excluded from calibration. Preserve readiness -> Volatile -> Trending -> Ranging -> Undetermined precedence, strict percentile comparisons and direction alignment.

Owner choice required: review [D3-A](RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md#d3-proposed-deterministic-research-contract): scale10^12 signed ties-even integer recurrences, exact seed/null/update order, population Bollinger, Wilder zero cases/first-bar ATR, ratio normalization, type-7 prior300 percentiles, equality and 401st-candle rules, transition/replay/checkpoint state. Hand-derived anchors and independent C# vector requirements are proposals, not production passes. Accept/amend/reject these numerical conventions; historical Release1.14 arithmetic does not substitute for this decision.

Acceptance: separately derived vectors for all indicators, seed/rounding/equality/zero/overflow cases; 400 not ready versus 401 eligible; no lookahead; discontinuity/correction/replay and pruning equivalence. Non-claims: no algorithm execution or regime proof from viewport fixture traces. These decisions block the affected foundational algorithm interface, not merely final benchmarking.

## D4 - Canonical transformed snapshot interface

**Status: OPEN. Primary owner: WP05 (#371).**

Sources: [architecture](../../governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md), sections 8 Private API and 9 Status semantics; [engineering proposal](RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md), section 4 Proposed language-neutral schema 1.0; [runtime evidence](prompters/release-1.15-wp01-runtime-feasibility-evidence.md), Remaining recovery and security obligations.

Inherited requirements: read-only private loopback interface; bounded whole-response validation; requested BTC/USD or ETH/USD; UTC completed minute candle-opening timestamps; full replaces aligned arrays/events/intervals atomically, unchanged omits arrays and requires exact accepted identity; generation/version/display-date identity and current request epoch. Preserve independent research/freshness/connectivity/classification/integrity states and status observation times; connectivity is session-local. Compatible unknown optional fields never permit server raw-field serialization or unbounded data. Invalid/timeout replies preserve last valid complete snapshot with truthful status.

Owner choice required: review [D4-A](RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md#d4-proposed-canonical-wire-contract), exact request/full/unchanged/zero/error shapes, decimal/counter strings, timestamps, status/collection/error consistency, compatible optional-field policy and bounded streaming/atomic validation. Proposed request limit4,096 bytes and success response4MiB replace the earlier unapproved 2MiB response candidate; no adequacy measurement is claimed. Identity includes D5's public positive monotonic transformEpoch counter string, independent of private keyEpoch UUID. Accept/amend/reject the canonical interface and illustrative vectors before dependent implementations; no spelling/type/value becomes FROZEN here.

Acceptance: one reviewed language-neutral specification and independently authored C#/Python full/unchanged/empty/error vectors; malformed UTF-8, duplicate keys, every bound, NaN/null/order/alignment/identity/status negative, all-or-nothing replacement and last-known preservation. WP06 consumes the accepted contract; WP03/WP04 supply fields only after their contracts. Non-claims: fake endpoint and component event validation are not the production wire contract or stream-size enforcement. This missing foundational interface blocks closeout.

## D5 - Transform and rotation identity

**Status: OPEN. Primary owner: WP05 (#371).**

Sources: [architecture](../../governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md), section 7 Transformed public data; [engineering proposal](RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md), sections 3 Research, continuity and transformed display and 5 Cache and viewport ownership.

Inherited requirements: positive HMAC-SHA256-derived daily UTC factor approximately 0.90-1.10, shared across both instruments and OHLC/all price-valued indicators after private calculation; RSI unscaled. No raw quotes, key or factor in public DTOs/logs. Date rollover replaces complete transformed collections. No normative derivation or rotation scheme is newly frozen.

Owner choice required: review [D5-A](RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md#d5-proposed-daily-transform-and-rotation-contract), exact LF-delimited HMAC-SHA256 message/private keyEpoch UUID, full-digest integer mapping to inclusive0.90..1.10, ties-even output, unchanged RSI/dimensionless quantities/base-asset volume and durable public transformEpoch counter. Same-day key rotation changes that counter and forces full replacement/cache eviction without inventing new market-data lineage. D4 uses the same tuple. Accept/amend/reject this candidate and independent test-only vector specification; no transform/rotation proof is claimed.

Acceptance: independent HMAC vectors for both instruments/day boundaries/extreme digest mappings; scale every price-valued output consistently, RSI unchanged, approved volume/dimensionless rules; rounding boundaries, key rotation on same UTC day, cold restart and unchanged/cache collision negatives; no raw/key/factor disclosure. Non-claims: S1 fake date multiplier is not HMAC qualification, anonymity or rights evidence. Missing identity semantics block the foundational transformed contract.

## D6 - Viewport mechanism and interaction

**Status: PROVISIONAL. Primary owner: WP06 (#372).**

Sources: [architecture](../../governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md), section 10 Chart interaction; [engineering proposal](RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md), section 5 Cache and viewport ownership; [component evidence](prompters/release-1.15-wp01-component-feasibility-evidence.md), Loading proof before implementation, Pending-intent correction - final review, 2026-10-22, Final bounded resource screen and Authorized cold-load correction and regression - 2026-10-22.

Rationale: owner selected supported components-v2 with locally loaded installed Plotly; no further mechanism search. Preserve native chart controls and fixture-proven manual intent semantics: absolute UTC x and price/volume y, session/instrument independence, ordered revision/mount/command/reset epochs, no programmatic gesture invention, newest pending intent survives ordinary refresh/remount and is committed once, Return to Live/preset authoritative reset, partial retention preservation and truthful full expiry. No production event transport/storage contract is newly frozen.

Unresolved decision: adapt to D4/D5 identities and real scheduler/cache/status/disconnect semantics; qualify reconnect and all-or-nothing publication, sessionStorage denial/corruption/abandoned-session reclamation, production payload/read limits, cleanup across actual lifecycle and acceptable pinned-asset loading/resource cost. Scope provisional status to production integration, not reconsideration of owner-selected technology.

Acceptance: actual browser on final Linux image records x/y/y2, identity/revision/epochs before/pending/after refresh/ack/remount/correction/rollover/rotation, stale callbacks, retention, resets and reciprocal sessions. Include cold starts, disconnect/reconnect, cleanup, query/redraw counts and bounded resource screen. Non-claims: 36 tests, 26 earlier browser cases, nine later smoke cases and six cold pairs are distinct historical evidence, not production acceptance or arbitrary network reordering proof.

## D7 - Linux and Azure F1 qualification

**Status: PROVISIONAL. Primary owner: WP07 (#373).**

Sources: [Docker policy](../../governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md), Binding rules and Release gates and non-authorizations; [architecture](../../governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md), sections 11 Performance and 13 Qualification; [runtime evidence](prompters/release-1.15-wp01-runtime-feasibility-evidence.md), Startup, interruptions and resource samples.

Rationale: single immutable Linux/amd64 image and Azure App Service Linux F1 are selected targets. Local 1 CPU/512 MiB/64-PID screening observations do not specify F1 guarantees. No numerical resource budget or availability SLA is frozen.

Unresolved decision: measured memory/CPU/cache/startup budgets, Normal/Elevated/Critical thresholds and truthful load shedding; 1/3/5-session capacity with authoritative private-service load; actual F1 filesystem/identity/restart/quota behavior and best-effort availability. Insufficient evidence means NO-GO or explicit reduced capacity, not weakened integrity. These platform proofs are downstream obligations, not a demand to deploy during WP01.

Acceptance: exact-image Linux security/contract/browser and combined recovery/load evidence; separately authorized actual per-site Azure durability/resource/rollback observations, no paid resource assumption. Non-claims: no F1 capacity, fsync support, image promotion or deployment authority from a local fixture pass. WP08 later records final acceptance/runbook handoff and cannot waive these proofs.

## Owner decision checklist - 2026-10-23

- [ ] D1-A accepted/amended/rejected explicitly, including retained-root trust, capability/filter policy and fail-closed closure.
- [ ] D3-A accepted/amended/rejected explicitly, including seeds/rounding/percentiles, flat-series result and checkpoint/replay state.
- [ ] D4-A accepted/amended/rejected explicitly, including exact schema, bounds, errors and zero/unchanged semantics.
- [ ] D5-A accepted/amended/rejected explicitly, including test-only vector plan, unchanged-volume policy and durable rotation identity.
- [ ] Confirm D4/D5 public identity exactly agrees: generation/version/displayDate/transformEpoch; transformEpoch is a positive counter string, not private keyEpoch UUID.

Until those choices are recorded, every status/primary owner above is unchanged and WP01 remains BLOCKED. D2 storage durability stays WP03, D6 accepted components-v2/local Plotly integration WP06 and D7 actual Linux/F1 qualification WP07. No alternative chart search, licensing gate, implementation or new test pass is introduced.

## Review corrections awaiting owner disposition

The [revised proposal](RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md) independently derives and retains the correct Bollinger midpoint inequality and ATR14 ramp seed; adds true integer-window ties, flat/ramp EMA/MACD and 400/401 calculations; proves minimum transformed positive OHLC remains one unit; and reconciles empty/unchanged status and explicit commit/rotation/midnight/stale identity cases. Documentation arithmetic is not an implementation test pass.

- [ ] D1: explicitly accept/amend/reject retained-root trust and the four capability purposes. Two UID-empty scans are insufficient closure proof; WP02 must establish a complete witness or keep replacement disabled. Alternative privileged helper/cgroup designs are not silently authorized. WP07 must verify actual F1 facilities with no privilege fallback.
- [ ] D3: accept/amend/reject population variance, retained midpoint formula, exact seeds/rounding and 100+300 prior-only readiness. Alternative sample variance/seeding changes results; WP04 independent vectors and replay proof remain downstream.
- [ ] D4: accept/amend/reject empty-initial versus successful-empty-poll semantics, literal unchanged envelope, all bounds/null/error rules and fresh status consistent with cached arrays. WP05 independently verifies both runtimes; WP06 integrates only the accepted contract.
- [ ] D5: accept/amend/reject minimum-positive proof, unchanged volume and generation/version/displayDate/transformEpoch transitions. Explicit rotation advances epoch even if factor coincides; already committed replies may arrive late. WP03 durability and WP05 independent HMAC/race vectors are still unproven.

D1/D2/D6/D7 remain PROVISIONAL; D3/D4/D5 remain OPEN. WP01 BLOCKED; #367 OPEN; WP02 unstarted; `PUBLIC_RUNTIME_VERIFIED=False`. Owner review concerns proposed contract choices, not a demand to complete downstream production qualification inside WP01.
