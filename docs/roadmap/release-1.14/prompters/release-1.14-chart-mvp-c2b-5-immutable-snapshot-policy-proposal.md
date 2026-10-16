# C2B-5 — Immutable synthetic snapshot policy proposal

Date: 2026-10-15, owner timezone. **PROPOSED ONLY — NOT APPROVED OR EFFECTIVE.**
This document neither changes controlling policy nor authorizes export, implementation,
publication, deployment, activation or a change to `PUBLIC_RUNTIME_VERIFIED=False`.
The companion architecture evidence packet records the current blockers and source references.

## 1. Decision requested

Consider a separately qualified read-only publication context using artifacts generated
offline by the existing independent C# generator. Compare two variants explicitly:

- **B0, candles only:** retain immutable canonical candles; indicators remain unavailable.
  This cannot claim the full candle-and-indicator objective is complete.
- **B1, candles plus per-selection analytics:** export independently verified derived
  results for each of the twelve frozen windows using the unchanged shared calculator.
  A public reader performs bounded validation/projection only and launches no Worker.
  This also requires an explicit exception to WP04's per-response calculation rule.

B1 is the recommended *conditional complete-chart candidate*, not an accepted architecture.
It avoids adding a production Python analytics implementation or a new request-time
.NET process solely for calculation. B0 is a smaller partial alternative. Retaining
per-response C# analytics would require separately qualified runtime integration and
does not automatically avoid the held WP07 lifecycle boundary.

No option is approved by merging this proposal. The owner must accept exact reconciled
policy wording and an implementation allowlist before export/loader work begins.

## 2. Proposed narrowly scoped normative exception

The following text is offered for a later approved amendment, not inserted into policy:

> A separately accepted immutable-synthetic-publication context MAY serve explicitly
> qualified, versioned fixed-window artifacts without anonymous request-time generation.
> The exception SHALL apply only to the independent synthetic source, fixed coverage
> `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, algorithm
> `aiq-synthetic-ohlcv-v1`, seed 114, unchanged canonical serialization and accepted
> dataset/window/visible-row fingerprints. It SHALL NOT authorize observed-market
> inputs, private derivation, provider calls, rolling data, daily refresh, startup
> generation, on-denial generation, or an automatic fallback to the Worker path.
>
> Artifact production SHALL be a separately authorized, bounded offline release operation
> using the unchanged generator and, if B1 is chosen, `aiq-ta-v1` calculator. The
> producer SHALL have no effective private credential/data/provider acquisition path.
> Existing isolated test execution is not authorization to export production assets.
> Artifact bytes and complete provenance SHALL be independently reproduced and reviewed
> before promotion. Production MUST NOT trust hashes supplied only by the artifact itself.
>
> Public reads SHALL bind accepted release identity to trusted, separately reviewed
> hashes and immutable bytes. Verification SHALL include bounds, canonical encoding,
> identity, precision, ordering, complete grids, coverage, exact fingerprints, and
> approved calculation windows. Hash equality alone SHALL NOT establish source independence
> or access isolation. The public identity SHALL remain BTC-like simulation with
> illustrative price/activity units and honest fixed coverage, not live BTC/USD.
>
> Publication SHALL select exactly one complete qualified release. Readers SHALL NOT
> mix versions or validate one file and serve a later pathname replacement. Byte ownership,
> open-handle/version pinning, symlink/hardlink/path rejection and atomic promotion
> semantics SHALL be proven under the actual storage model. The public UID SHALL NOT
> modify artifacts, parent directories, trust anchors, executable code or promotion state.
> Recovery SHALL replace an entire accepted release through an authorized operator
> process; there SHALL be no public repair, export, regenerate or gate-control endpoint.
>
> Missing, oversized, corrupt, unsupported, inaccessible or unqualified releases SHALL
> keep data endpoints unavailable. Restart SHALL re-establish verified read state and
> SHALL NOT generate, repair, weaken trust or choose unapproved older content. Rollback
> SHALL use an explicitly accepted complete release and preserve auditable identity;
> it SHALL NOT be an automatic integrity-failure fallback.

### Optional B1 analytics clause — requires separate explicit acceptance

> Only for accepted immutable B1 releases, shared analytics MAY be calculated during
> the offline authorized release operation rather than per public response. This is a
> specific amendment to WP04's per-response/no-analytics-cache restriction, not a change
> to candle-cache `CalculationVersion == null` or to existing Worker behavior. Each
> derived artifact SHALL bind `aiq-ta-v1`, fixed parameters, dataset/window identity,
> calculation start, exactly 250 warmup candles and timestamp-aligned visible rows.
> All twelve complete derived results SHALL pass independent numerical/serialization
> verification and trusted output-hash review. Hidden warmup values SHALL NOT be served.
> Distinct range seeds SHALL NOT be conflated; nulls SHALL NOT become zero or fills.

The new artifact envelope, trust manifest, filenames, producer, byte budgets and ready
response schemas remain unselected. No new fingerprint constants are proposed here.
Existing complete candle fingerprints must not be regenerated merely to fit a new format.

## 3. Governing deviation register

All dispositions below are **proposal pending owner decision and proof**, not waivers.

| ID | Existing governing provision | Proposed change / exact scope | Gate and owner decision | Rollback |
| --- | --- | --- | --- | --- |
| D1 | Definition complete-generation rule: full dataset once per cold Worker request; execution plan WP02 same rule | Permit a separately authorized offline release producer; do not alter existing on-demand implementation or generator bytes | G-1/G-2/G-3, G-B0/G-B5; accept artifact-production authority and equivalence proof | Reject/unpublish new context; retain disabled Worker path |
| D2 | Definition excludes deployment/startup/background generation; strategy Section 10 abuse item 5 and definition supplemental criteria exclude exported fixtures | Permit only reviewed immutable release assets; retain all startup/background/request regeneration prohibitions | G-B0/G-B2/G-B5; accept exact export/promotion/trust model | Return to unavailable; never regenerate on demand |
| D3 | WP03 cache: 12 selections, 2 MiB per entry, 32 MiB including temporary data, 24h retention, validation on hits | Separate release-asset lifetime/limits from request cache; preserve existing cache rules and approve any new asset store independently | G-B0/G-B3/G-B4; decide total bytes/member count, storage/lifetime, validation and temporary-data budget | Stop reader; no TTL extension or cache migration |
| D4 | WP04 authority Phase B: calculate per response; no analytics cache modification | B1 only: offline per-window derived artifacts, complete independent proof and trusted hashes; B0 leaves analytics unavailable | G-B0/G-B5, AC119/120/122; explicitly accept or reject B1 exception | Withdraw derived assets; indicators unavailable |
| D5 | Current Worker/bridge invocation and frozen fail-closed publication/runtime gate; held WP07 topology | A future no-spawn reader would be a distinct authorized integration; current gate remains false and existing invocation blocked until separately accepted gate design | G-B0/G-B1/G-B2/G-B4; approve role/threat/gate mapping, not blanket removal of WP07 requirements | Fail closed; no fallback Worker or second route |
| D6 | G-B1/G-B3/G-B4 generation admission/reservation/cleanup and C2 account predicates | Only an explicitly proven no-generation/no-Worker reader can request a documented applicability decision for spawn-specific predicates; common confidentiality/read abuse/recovery remain mandatory | G-B0 and owner security acceptance; prove absence of fallback/spawn, independent offline production controls | Deny reads on unknown mandatory state; no hidden budget reset |
| D7 | Section 11 ready-data response schema/bounds are pending; current unavailable REST body cap 4096 bytes | Design a separately approved bounded ready payload large enough for frozen windows; preserve all error bounds and cheap health; bridge's 2 MiB cap is not a REST default | C2B-API-01/03/05/06, G-B3/G-B7; approve schemas, decimals, maximum bytes and limits | Current unavailable responses unchanged |
| D8 | API accepts UTC-aligned ranges within reference visibility; numerical qualification is twelve end-anchored windows | Ready publication initially binds only the twelve approved windows; any unqualified selection remains a stable safe unavailable response. No silently rebased indicators or arbitrary window reuse | G-B5, AC119/122; approve ready-selection mapping/failure semantics before implementation | Keep all selections unavailable |
| D9 | Current Dockerfile makes `/app` public-UID-owned; no accepted immutable artifact/trust protection mechanism | Future separately scoped image/mount/ownership design must prevent public writes to asset, manifest, code and ancestors; chmod on an owner-writable tree is insufficient | G-B2/G-B6; target proof and exact Docker/startup allowlist required | No deployment; withhold qualified release |
| D10 | Current 24h cache retention and historical fixed end do not mean live market freshness | Explicitly authorize release-asset retention and retired-release policy; preserve actual fixed coverage and operational verification timestamp distinction | AC122, G-B0/G-B4/G-B6; approve retention/rollback allowlist without rolling claim | Unavailable on unsupported/retired release |

No governing document is edited by this packet. D1-D10 must be reconciled into the
definition, execution plan, governance strategy, file manifest and applicable WP04/WP07
authority records in a later approved documentation scope; implementation permission
does not follow from a proposal or a planning PR merge.

## 4. Mandatory invariants and gates retained

- Confidentiality, no Twelve Data/Vike public access, source independence and no
  cross-context fallback remain non-negotiable; preserve private provider code/behavior.
- Frozen candle contracts, output scales, seed, complete coverage and all fingerprints
  remain unchanged. Do not create a production Python port of the generator/calculator.
- Numerical indicator semantics and the 12 calculation windows remain unchanged even
  if the *timing* of analytics production is later amended. Exactly 250 warmup rows
  remain calculation-only and never become public candle/analytics rows.
- No snapshot/DB marker can assert `PUBLIC_RUNTIME_VERIFIED=True`. Any future gate
  split or replacement requires an explicit owner-approved contract and negative tests.
- Anonymous reads still need bounded input/output/verification/allocations, authoritative
  concurrency and abuse controls, cheap independent health and measured headroom.
  Process-local counters are not durable deployment-global evidence. No unbounded queue,
  automatic retry, silent fallback, or repair triggered by public requests.
- Azure filesystem, startup/restart, cold latency, memory, request floods and F1 quota
  exhaustion remain evidence obligations. Zero added resources is a design objective,
  not a measured cost or availability guarantee; paid upgrades are not an escape route.
- Later implementation, local adversarial qualification, target-runtime diagnostics,
  deployment and activation each need their own explicit authority. Held WP07 work is
  not resumed, accepted or merged by this proposal.

## 5. Before any implementation GO

Owner must decide B0/B1/on-demand, accept exact reconciled D1-D10 wording as applicable,
set literal-path scope, approve artifact/ready-response formats and size/member budgets,
select trust anchors/ownership/promotion and recovery model, and resolve gate applicability.
A separate offline-only exporter/validator proof could then be authorized with no public
wiring. Any output would remain unpublished until independent equivalence/security
review. Until then: **implementation BLOCK; export/publication/activation/deployment NO-GO**.
