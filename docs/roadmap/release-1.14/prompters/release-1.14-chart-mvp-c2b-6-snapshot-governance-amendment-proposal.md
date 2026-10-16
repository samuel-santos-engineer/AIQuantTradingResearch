# C2B-6 — Snapshot governance amendment proposal

**NOT APPROVED / NOT EFFECTIVE.** Date: 2026-10-15 (owner timezone).
This is proposed wording for separate owner approval, not an edit to governing policy.
Merging a planning PR does not enact it. No code, schema file, dataset, snapshot,
export, Docker, test, runtime, credential or Azure change is authorized here.
`PUBLIC_RUNTIME_VERIFIED=False`; public activation and Azure deployment remain **NO-GO**.

## 1. Proposed decisions and exact amendment scope

The recommendation is **B1: independently produced immutable candles and per-window
analytics**, conditional on every gate in the companion evidence document. B0 (candles
only, indicators unavailable) remains an owner alternative. No variant is selected
for implementation. Current request-time Worker behavior stays unchanged and disabled.

### P1 — Narrow offline-production exception (C2B-5 D1/D2)

Proposed addition to the definition's complete-generation rule and execution-plan WP02:

> Only within an explicitly accepted immutable-synthetic-release context, an owner-controlled
> offline producer MAY invoke the unchanged frozen C# generator at a pinned reviewed
> repository revision for a separately authorized release operation. This exception
> permits neither anonymous-request nor deployment/startup/background generation. It
> SHALL NOT change existing Worker/cold-request behavior or activate that path.
> Snapshot production/export/promotion require their own literal-path authority.

Proposed exception to definition supplemental criteria and strategy Section 10
cache-publication/export prohibition:

> Only independently qualified immutable synthetic release artifacts MAY be promoted
> under the separate release context. Public requests, invalid requests, misses,
> corruption, denial, startup and restart SHALL NOT trigger generation, export, repair,
> provider calls or fallback. Existing request-cache rules remain otherwise unchanged.

The proposed location is an owner-controlled offline build/qualification environment,
not the public app container or an unreviewed CI job. It must have no effective private
data/credential inputs or provider-acquisition capability. Exact producer filesystem,
network restrictions, command, output path and promotion role remain TBD; none is
authorized by this proposal. Offline test generation is not production export authority.

### P2 — Provenance, manifest and trust (D2/D9)

> Artifacts SHALL be derived only from the unchanged independent synthetic algorithm.
> Source, version, seed, complete coverage, canonical candle/grid/precision and accepted
> dataset/window/visible fingerprints SHALL match the frozen contract. Observed or
> private data, provider configuration and credentials SHALL never enter public artifacts.
> A manifest SHALL bind immutable release identity, all permitted members and exact
> canonical/derived identities. Trust SHALL originate in a separately reviewed release
> anchor, not a digest supplied solely by the same artifact. The public UID SHALL NOT
> modify artifact bytes, trust anchors, executable code, promotion state or their ancestors.

Proposed manifest **semantic shape only**, not a shipped schema or format selection:

| Field group | Proposed type/constraint |
| --- | --- |
| Schema/release identity | Explicit supported schema version and opaque content-bound release ID; neither accepted from query paths |
| Producer provenance | Reviewed generator/calculator source revision, owner production/review authority references, producer identity/evidence reference; no secrets or private paths |
| Frozen source | Independent synthetic context, instrument `btc-like-simulation`, algorithm `aiq-synthetic-ohlcv-v1`, seed 114, exact exclusive UTC coverage |
| Canonical identity | Existing candle version and units; fixed interval and approved window identity; no observed venue/provider substitution |
| Complete datasets | Three entries: interval, expected count, canonical byte count, trusted existing SHA-256; complete grid/invariants verified |
| Calculation/visible windows | Twelve entries: interval, anchored from/to, calculation start, visible/calculation counts, exactly 250 warmup, existing window and visible-row fingerprints |
| B1 analytics | Twelve selection-bound entries with `aiq-ta-v1`, fixed parameters, aligned visible rows/null semantics, byte count and independently verified derived hash; hashes not yet established |
| Member binding | Canonical non-traversing member identifiers, byte counts and digests; reject unknown/duplicate members and links; format/compression/aggregate bounds TBD |
| Operational evidence | Production/promotion/verification timestamps distinct from coverage and freshness; release eligibility/revocation reference; deterministic candle hashes exclude operational timestamps |

Actual encoding, complete schema, member layout, signature/pin mechanism, release-ID
derivation, aggregate limits and trusted ownership model require separate acceptance.
A self-consistent malicious manifest is not trusted. Existing candle hashes cannot be
changed to match a new container format. No private provenance evidence is published.

### P3 — Indicator timing exception, not numerical change (D4)

Proposed addition to WP04 Phase B, only if B1 is separately accepted:

> Qualified B1 releases MAY calculate shared analytics offline rather than per public
> response. This narrow exception SHALL NOT alter the existing Worker or candle/cache
> `CalculationVersion == null` contract. Production Python reimplementation is prohibited.
> All twelve derived windows SHALL be independently reproduced and bound to the accepted
> full calculation windows, fixed parameters, source/context and dataset/window identities.
> Only visible rows SHALL be served; 250 warmup rows remain calculation-only.

Parameters remain SMA20; SMA-seeded EMA20; Wilder RSI14; MACD12/26/9; population
Bollinger20x2; scale `10^12`, frozen exact rounding and canonical/display distinctions.
First-valid input positions remain 20/20/15/26/34/20 for SMA/EMA/RSI/MACD/signal/Bollinger.
Null alignment, no lookahead, signed bands, gap rejection and range-specific seeds are
unchanged. Existing sample vectors are not independent proof of complete exported
analytics. If complete B1 parity fails or is unavailable, indicators stay unavailable;
do not copy calculator outputs into expectations or weaken hashes to force a pass.

### P4 — Fixed windows and REST compatibility (D7/D8)

> Fixed coverage SHALL remain `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`.
> Ready results MAY initially be published only for the twelve qualified selections:
> intervals 1h/4h/1d and 1/7/30/90 days ending at the fixed exclusive end. Other currently
> valid UTC-aligned queries SHALL remain explicitly unavailable, not be silently rounded,
> rebased, re-seeded, filled or mapped to a different window. No rolling or live claim.

This proposal retains the six GET paths and current parameter names/defaults and
validation errors. It introduces no range/pagination parameter or mutable endpoint.
No pagination is recommended for the initial ready contract: return a complete qualified
visible window only within accepted byte/row bounds, or fail closed. Limit below requested
count retains `ROW_LIMIT_EXCEEDED`; an otherwise valid but unqualified selection retains
503 `DATASET_NOT_READY`. No silent truncation of an analytic calculation context.

| Endpoint | Parameter/date/row semantics | Byte limit disposition |
| --- | --- | --- |
| health | No query/body; cheap liveness, independent of artifact verification | Retain existing 4096 response bytes |
| status | No query/body; web/dataset/indicator readiness separately reported | Retain existing 4096 response bytes |
| dataset | No query/body; fixed actual coverage/identity only when qualified; metadata, no candle rows | Retain existing 4096 response bytes; ready metadata schema pending |
| diagnostics | No query/body; stable safe category only, no internals/admin controls | Retain existing 4096 response bytes |
| candles | Current instrument/interval/from/to/limit; max 90 visible days and 2160/540/90 rows at 1h/4h/1d | Ready payload bound **TBD**, measured complete maximum-width output and owner acceptance required; current 4096 unavailable cap unchanged |
| indicators | Same window fields plus fixed approved indicator enum; timestamp-aligned visible rows; no custom periods | Ready payload bound **TBD** independently; current unavailable/error cap unchanged |

Current request path+query cap is 4096 bytes, header accounting 16384 bytes, body
disallowed; global `limit` is 1..2160, not an instruction to fabricate interval rows.
These existing application numbers are not newly measured transport guarantees.
Ready schemas, decimal serialization, parameter metadata and byte limits require separate
acceptance; the bridge's 2 MiB cap is not an approved REST budget. If an existing cap
cannot accommodate qualified output, remain unavailable rather than silently increasing it.

### P5 — Isolation, read admission and resource predicates (D5/D6)

> A proposed no-generation reader SHALL have no generator/Worker/provider fallback path.
> Artifact immutability SHALL NOT be equated to public/private isolation. Private paths,
> caches, environment, inherited FDs, `/proc`, network/provider capabilities and derived
> artifacts remain subject to independent negative evidence. Private research stays separate.
> Read work SHALL be bounded before validation/loading/projection/serialization and until
> response completion/cancellation, including slow readers and memory held by denied work.

Snapshot-specific applicability of spawn admission, reservation and termination predicates
must be explicitly decided at G-B0 after proof of no public spawn. No blanket WP07 waiver.
Advanced WP07 remains ON HOLD; existing false gate is not replaced, bypassed or configured
by a manifest, environment variable or request. Durable read/abuse accounting and health
protection remain mandatory across the approved instance/restart domain; backend TBD.
Immutable data does not eliminate mutable authoritative abuse state requirements.

Current process-local read limits (2/second, 30/minute), health allowance and 250 ms receive
timeout are implementation evidence, not qualified F1 quotas. Full in-flight response,
verification CPU/RSS, send deadline, global/session rate, counter durability/rollback,
health breaker/freshness/hysteresis and ingress budgets remain **TBD** pending measured
qualification and owner approval. Do not evict counters or reset budgets on restart,
change of client, release ID or integrity failure. Unknown mandatory state denies reads.

### P6 — Retention, promotion and recovery (D3/D10)

> Immutable release lifetime SHALL be separately authorized from the unchanged 24h
> request-cache retention. Member counts, aggregate bytes, temporary space and eligible
> current/prior release count/lifetime require explicit accepted bounds. A privileged
> release operator, never a public endpoint, owns promotion, retirement, cleanup and revocation.
> Promotion SHALL expose only a complete accepted version; each reader pins one verified
> version/byte identity. Validation-to-use races, symlinks, partial writes and mixed versions
> SHALL fail closed. Startup/restart MAY validate bounded existing bytes but SHALL NOT generate.

Missing/corrupt/unsupported/inaccessible/unqualified snapshot: dataset/candles/indicators
return **503 `DATASET_NOT_READY`** with null data; status/diagnostics remain bounded
and non-sensitive, health independent. Existing safe `DATASET_INTEGRITY_FAILURE`,
`DATASET_STALE` and `SERVICE_UNAVAILABLE` taxonomy may classify independently established
conditions in a later accepted schema; no corruption detector is implemented here and
no condition invents data. Staleness is not inferred from fixed historical end alone.

Rollback is an operator-approved whole release with a trusted accepted hash/identity;
it never automatically selects an older file after integrity failure. Interrupted
promotion/restart must not silently restore trust. Revocation, trust-anchor change,
tampering, parity mismatch, private exposure, unknown required state or failed measured
budgets withdraw data readiness. No public repair/regeneration or private/provider fallback.

### P7 — Qualification, effectiveness and revocation

> This proposed exception SHALL become effective only after explicit owner acceptance
> of exact reconciled clauses and governance records. Later implementation, offline
> production/export, local adversarial tests, target Azure qualification, deployment and
> public activation each require their own scoped authority. Planning acceptance or
> merge SHALL NOT supply implementation permission or qualify any safety gate.

Definition, execution plan, strategy, manifest and applicable WP04/WP07 authority
reconciliation must identify the narrow context and retain historical records. No
current governing file is amended by this proposal. Confidentiality, synthetic identity,
fixed hashes, accurate unavailable behavior and no observed-market fallback cannot be waived.
On revocation or failed prerequisites, use the current safe unavailable shell/API; do
not reset budgets, weaken provenance or resume request-time generation. Documentation
rollback is a normal reviewed revert, not a runtime operation.
