# C2B-7 — Snapshot implementation specification

Date: 2026-10-16 (America/Sao_Paulo).
**PROPOSED DESIGN — NOT IMPLEMENTATION AUTHORIZATION — NOT EFFECTIVE POLICY.**
No artifact, format, producer, loader, trust mechanism or runtime integration is approved.
`PUBLIC_RUNTIME_VERIFIED=False`; existing unavailable responses remain unchanged.
Public activation, Azure deployment and advanced WP07 work remain **NO-GO / ON HOLD**.

## 1. Preconditions and scope

This specification refines C2B-6 P1-P7/S1-S8 for review, not implementation. Before any
code/export step: explicit owner acceptance of exact final amendment text, resolution of
the shared-state conflict below, reconciled governing records and a literal mutation/test
allowlist. Merging C2B-5/6/7 planning evidence does not satisfy these conditions.

Frozen source: independent `aiq-synthetic-ohlcv-v1`, seed 114, coverage
`[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, unchanged canonical bytes and all
accepted dataset/window/visible fingerprints. Three intervals 1h/4h/1d; twelve
1D/7D/30D/90D selections; exactly 250 preceding calculation-only candles. No rolling
window, observed data, Twelve Data/Vike public access or automatic fallback.

**State conflict, implementation STOP:** C2B-7 prohibits mutable shared state while
existing governance and C2B-6 P5 require authoritative durable abuse accounting across
restart. This specification adopts neither an exception nor a weakened interpretation.
Owner must either explicitly distinguish immutable public datasets/private state from
a separately authorized control-state mechanism, or approve an equivalent stateless
protection contract with evidence. No such equivalent mechanism is established here.
Publicly mutable artifact/data state and private shared state remain prohibited either way.

## 2. Logical responsibilities and trust boundary

These are design responsibilities, not new processes, UIDs, dependencies or authorized files.

| Responsibility | Proposed input/output | Required restriction |
| --- | --- | --- |
| Offline producer | Reviewed revision + frozen constants -> complete canonical candles and selected B1 analytics | Separate owner authority; no public runtime or private/provider inputs/capabilities; bounded local operation |
| Independent verifier | Canonical/derived bytes + frozen reference maps -> qualification evidence | Full byte/hash/numerical comparison, no copying production output into expected values |
| Release promoter | Accepted evidence + immutable release -> operator-controlled whole-version promotion | Cannot be invoked by anonymous request; trust anchor and parents not public-writable |
| Pinned reader | Accepted release identity -> bounded verified bytes/state | No generation/export/repair, provider clients, private paths or Worker fallback; one coherent version |
| Query projection | Approved fixed selection + verified rows -> bounded public response | Full identity/window binding; visible rows only; no reseeding, synthetic gap filling or unapproved pagination |
| Presentation | Validated public response -> truthful Streamlit/Plotly view | No private handoff; fixed synthetic label; no retained unqualified chart on failure |

An immutable file does not prove confinement. Target negatives must cover effective
mount/file permissions, credentials/environment, inherited descriptors, `/proc`, path/link
attacks, private derived artifacts and outbound provider capability. Existing public
ownership of `/app` is insufficient to protect code, manifest pins or assets from that UID.
Any future ownership/image/mount change needs separate scope and proof; no privileged
deployment, broker/guardian revival or new service is selected.

## 3. Candidate release and manifest contract

Proposed immutable naming is a release-specific namespace such as
`releases/<opaque-release-id>/<fixed-member-id>`; this is a logical layout, not an
approved filesystem path or schema. Public parameters never select release IDs or paths.
The same release identifier must never be reused for different bytes. A separately
reviewed anchor binds that identifier to exact manifest/member digests; a manifest
cannot establish its own trust merely by including a self-consistent hash.

Proposed semantic manifest fields:

| Group | Required meaning / rejection |
| --- | --- |
| Envelope | Explicit supported version, immutable release ID, reviewed producer/reviewer and source revision references; reject unsupported/duplicate/unknown fields |
| Frozen identity | Independent synthetic context/instrument/units, canonical version, generator version, seed, exact exclusive UTC coverage; no provider/venue substitution |
| Dataset entries | Exactly three qualified interval identities with canonical byte count, row count and existing trusted hash; reject incomplete grids, bad precision/OHLCV invariants or altered bytes |
| Window entries | Exactly twelve approved selection identities: calculation/visible start, end, counts, 250 warmup, dataset/window/visible hashes; reject mixed identity or seed context |
| B1 analytics entries | All twelve full-window-derived visible outputs, `aiq-ta-v1`, fixed parameters, alignment/null semantics, exact byte count and independently qualified output hash; new hashes remain unestablished |
| Member binding | Fixed allowlisted logical IDs and exact lengths/digests; no arbitrary path, traversal, links, duplicate/extra member, unbounded decompression or recursive includes |
| Operations | Production/promotion/verification evidence separate from historical coverage; accepted eligibility/revocation/retention policy; no credentials/private paths |

Concrete serialization/schema version, manifest/member/aggregate/temp byte limits,
digest pin/signature mechanism, path ownership and release-ID derivation remain
**TBD — measurement and owner approval required**. No schema file or hash constant is
created here. Existing canonical UTF-8/LF candle serialization must be retained exactly;
new envelope hashes cannot replace the frozen semantic fingerprints.

## 4. B1 analytics specification, still conditional

Preferred complete-chart candidate B1 precomputes with the unchanged C# calculator;
B0 publishes candles only and keeps indicators unavailable. Request-time C# calculation
would require a separately qualified runtime path; no production Python port is proposed.
B1 requires explicit amendment of WP04's per-response/no-analytics-cache restriction.

Parameters remain SMA20, SMA-seeded EMA20, Wilder RSI14, MACD12/26/9 and population
Bollinger20x2, scale `10^12`, exact frozen rounding. First-valid positions remain
20/20/15/26/34/20 for SMA/EMA/RSI/MACD/signal/Bollinger. Preserve signed lower bands,
nullable timestamp alignment, no lookahead and canonical/display precision. A null is
not zero or a carried-forward sample. Do not fabricate a missing candle or partial bucket.

Every exported selection must derive from its own complete 250-warmup calculation
window and bind its original dataset/window identity. Different range seeds need not
produce identical overlapping EMA/RSI/MACD. Verify full derived bytes against a separately
authored oracle for all twelve selections; existing sample vectors alone do not qualify
an exported result. Do not replace an expected hash to force equivalence. No hidden
warmup candle/analytic row may enter a ready response or downloadable public artifact.
Release members are server-internal, not public static assets or direct downloads;
only the qualified visible projection is public. Whether calculation history itself
is packaged must be owner-reviewed; hidden proof prehistory stays nonpublic.

## 5. API and UI projection contract

All six GET routes and current failure envelopes/parameter names remain unchanged now.
Future ready schemas are proposals requiring acceptance before integration. Health is
cheap and independent; status separates web/dataset/indicator readiness; diagnostics
contains safe categories only. No mutation, recovery, export or administrative endpoints.

| Surface | Proposed ready behavior | Bound / current behavior retained |
| --- | --- | --- |
| health/status/dataset/diagnostics | Minimal metadata; actual fixed coverage only after qualification, no private provenance details | Existing 4096 response-byte cap; no query/body; ready metadata schema pending |
| candles | One approved end-anchored window, visible rows only, illustrative units and full public identity | At most 90 visible days; max 2160/540/90 rows by interval; ready byte cap **TBD — measurement required** |
| indicators | Selected fixed indicator as defined by current enum, aligned to the same approved window and visible timestamps | Exact twelve seed contexts; ready byte cap/schema **TBD — measurement required** |

Current `instrument`, `interval`, `from`, `to`, `limit` and indicator selector semantics
must not silently change. Recommend no pagination or new parameter initially. Limit
smaller than requested window retains 400 `ROW_LIMIT_EXCEEDED`; valid but unqualified
date/window selection remains 503 `DATASET_NOT_READY`, not a silently rebased result.
Defaults still identify the 30D reference window. All successful window ends are the
fixed exclusive end, not current wall time. Arbitrary aligned date ranges are not
independently qualified indicator windows. Any other design needs explicit amendment.

Retain existing malformed/unknown/duplicate/oversized validation and stable safe
400/404/405/414/429/431/503 behavior, with no sensitive input echo. Current path+query
4096-byte and header-accounting 16384-byte checks are application bounds, not transport
ingress memory proof. A ready response too large for its accepted bound is unavailable,
never silently truncated or granted the bridge's unrelated 2 MiB capture allowance.
No ready byte threshold is inferred from raw canonical file size.

UI must show exactly **BTC-like Market Simulation — Synthetic Historical OHLCV**,
illustrative price/activity units, research-lab/no-investment-advice context and fixed
coverage. Plotly candlesticks, volume and approved indicator rows must preserve UTC,
gaps/nulls and provenance. Failure clears retained unqualified data; no observed/live
market labels or private data. Existing session retention is not permission for a new
shared cache or indefinite artifact retention.

## 6. Verification, promotion and recovery states

Proposed future states: DISABLED -> VERIFYING -> QUALIFIED_READ_ONLY; failures or
revocation -> UNAVAILABLE. **No transition is currently authorized**: the false runtime
gate stays in force, and a manifest/environment/request cannot promote readiness.

Before a future transition, pin accepted release identity and bytes, enforce bounded
schema/member/hash/semantic checks and required isolation/control-state predicates.
Do not validate a pathname then reopen mutable/replaced content. One reader must not
mix old/new members. Startup/restart may only validate accepted existing bytes under
separate authority, never generate, export or repair. Health protection during validation
requires measured evidence, not an assumption about a small file or background task.

Missing/corrupt/inaccessible/unsupported/unqualified content -> data null and 503
`DATASET_NOT_READY`; readiness/diagnostics remain sanitized. Integrity/staleness category
extensions require the separately accepted schema and actual checks. Fixed historical
age alone does not establish data staleness. No automatic retry/rebuild/older-file fallback.

Promotion/revocation/retention cleanup belong only to an authorized release operator.
Prove whole-release atomicity and concurrent reader pinning on the actual storage/image
model. On revocation, deny new reads; disposition of an already streaming response must
be explicitly specified and tested (already delivered bytes cannot be recalled). A known-good
rollback requires an explicitly eligible complete trusted release, not a digest found
inside a corrupt store. Version/member/retention/temp-space budgets remain TBD.

## 7. Admission and resource qualification

No public request may invoke generator, Worker, export, repair, provider or private-cache
access. Absence of a spawn path needs code and capability proof, not a diagram. Generation
specific gate applicability must be explicitly accepted; confidentiality/read abuse and
recovery are not waived. Advanced WP07 remains on hold.

Subject to resolution of the shared-state conflict, read admission must cover validation,
projection, serialization, bytes held and full response send/cancellation. The current
API releases its slot before sending, so it is not whole-response concurrency evidence.
Keep no unbounded queue, retry loop, cardinality registry or counter eviction granting
admission. Client/tab/IP/release changes and restart must not replenish durable protection.
Authoritative domain/backend, cold/recycle behavior, clock rollback, send timeout and
limiter failure behavior require owner decisions; no state-store/service is selected.

Before any future active measurement batch, approve finite request count, duration,
parallelism, CPU/RSS/disk/log and shutdown ceilings, sample plan and abort criteria.
If any necessary ceiling remains **TBD — measurement required**, do not start that batch.
No active load/fault/Azure qualification is authorized by this plan.

| Reproducible future procedure | Measurements to record | Acceptance prerequisite |
| --- | --- | --- |
| Pinned offline repeatability across all 12 selections | Source/runtime versions, full bytes/hashes, derived parity, UTC grid and independent-oracle evidence | Zero fingerprint/numerical/identity discrepancies; artifact production authority first |
| Cold validation and warm reads of accepted release | Whole-process/container CPU, peak/current RSS, latency, bytes, failures and environment evidence | Limits/sample count **TBD — measurement required**; no hidden warmup/first-run exclusion |
| Clean restart and interrupted promotion/read | Identity before/after, no mixed release, no generation/fallback, recovery/readiness/health timeline | Unknown state unavailable; accepted rollback only; time/resource ceilings TBD |
| Bounded concurrent/slow-reader/invalid/denied traffic | Full-send in-flight allocations, request/response bytes, CPU/RSS, rate decisions, health latency, client/release rotation | Finite owner-approved batch; global/durable-state choice resolved; no unbounded queue |
| Target Azure F1, separately authorized | Image/mount/ownership/restart behavior, cold and warm latency, CPU/RSS, disk/quota usage, log bounds, cost/resource/plan evidence | No inferred cgroup/mount/quota support; thresholds and sample sizes TBD before tests |
| Actual browser, separately scoped | Visible candles/volume/indicators, UTC selection, no fake gaps, exact label, accessibility/mobile behavior | No chart claim from AppTest or localhost JSON alone |

No operational performance limits or F1 measurements are invented. Existing candidate
quotas are not qualification results. If zero-cost durable abuse/isolation cannot be
proven under the accepted policy, activation remains NO-GO rather than adding paid
resources or weakening the contract.

## 8. Staged delivery and rollback plan — not authorized execution

1. **Policy gate:** accept exact final C2B-6 P1-P7 plus any explicit shared-state/gate
   reconciliation; select B0/B1 and settle R1-R10 in companion evidence. No export/code yet.
2. **Offline proof authority:** literal source/test/output path allowlist, independent
   oracle and finite budgets; implement exporter/validator only if separately authorized.
   Verify all three datasets/twelve windows and B1 outputs; publish nothing.
3. **Independent security review:** reject malformed manifests, tampering, extra/missing
   members, private-source identity, traversal/links/TOCTOU, precision/gap/seed mismatch,
   oversize and unsupported versions; review producer and public read capabilities.
4. **Local integration authority:** exact ready API/UI/ownership/gate scope and accepted
   byte/in-flight/control-state limits; missing/corrupt state stays unavailable. No new
   provider or held native-lifecycle path. Existing errors and health regressions retained.
5. **Target/browser qualification authority:** separate bounded Azure/browser/load/restart
   plan, immutable deployment atomicity and rollback evidence; no production activation.
6. **Acceptance and activation:** owner review of all applicable gates, separate explicit
   deployment/public activation authority. No planning merge auto-enables this stage.

Documentation rollback: decline/revert the planning PR through normal review; current
unavailable behavior is unchanged. Proposed runtime rollback: stop new reads, revoke
unqualified release, operator-select whole eligible release or remain unavailable;
never reset protection, weaken hashes, generate/repair or reach a private fallback.
