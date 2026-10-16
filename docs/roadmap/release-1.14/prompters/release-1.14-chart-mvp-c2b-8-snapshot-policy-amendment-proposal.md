# C2B-8 — Snapshot policy amendment reconciliation

Date: 2026-10-16 (America/Sao_Paulo).
**OWNER SELECTIONS RECORDED — RECONCILIATION DRAFT NOT EFFECTIVE — NO IMPLEMENTATION AUTHORITY.**
P1-P7, the narrow security-state exception S and retaining unmeasured thresholds T
are approved owner choices; B1 is selected. This document does not reclassify those
choices as unapproved. Its proposed canonical insertions, implementation details and
operational qualification still require explicit reviewed governance reconciliation.
No existing binding policy or historical authority is edited by this packet.

Source of approval: the owner-supplied C2B-8 Owner Decision Register dated 2026-10-16,
preserved verbatim beside this document. No hidden earlier selection log is invented.
`PUBLIC_RUNTIME_VERIFIED=False`. Implementation, snapshot production/export/promotion,
public activation and Azure deployment are **NOT AUTHORIZED / NO-GO**.

## 1. Clause-level insertion/replacement map

The following C1-C8 are concrete text for a later approved reconciliation, not effective
instructions. Unchanged cold-Worker/private paths remain governed by existing contracts.

| Clause | Governing target and conflict | Owner decision / proposed resolution |
| --- | --- | --- |
| C1 | `RELEASE_1.14_DEFINITION.md` complete-generation rule; execution-plan WP02 cold-request/startup prohibitions; strategy Section 10 exported-fixture restriction | Approved P1 permits a narrowly separate offline release context; no actual producer/export permission |
| C2 | Strategy provenance/publication requirements, G-3/G-B2/G-B5; definition immutable fingerprint contracts | Approved P2 requires independent trusted anchor and immutable release ownership; mechanism unselected |
| C3 | WP04 authority Phase B per-response/no-analytics-cache rule; execution-plan WP04 | Approved P3 and B1 select offline C# analytics for a separate immutable context; numerical/candle-cache semantics unchanged |
| C4 | Strategy Section 11; twelve window identities versus arbitrary validated API ranges and ready response bytes | Approved P4 preserves fixed selections and errors; schemas/size limits still require proof/acceptance |
| C5 | C2B-7 no-mutable-shared-state wording vs strategy durable abuse requirements/C2B-6 P5 | Approved S narrows immutability to market/private data; separately controlled durable security state permitted conceptually |
| C6 | G-B0-G-B7 and existing invocation gate/admission/health rules | Approved P5/P7 retain confinement, full-read lifetime, abuse and gate proof; no blanket Worker/WP07 waiver |
| C7 | 24h request-cache retention vs immutable release lifetime; promotion/restart/rollback duties | Approved P6 separates operator-owned release lifecycle from unchanged request-cache rules; budgets TBD |
| C8 | Governance effectiveness, traceability/file manifest and implementation authorities | Approved P7/T separate exact policy reconciliation, measured limits, mutation allowlist, export, qualification and activation |

Historical C2B-5/6/7 records remain historical. A later approved reconciliation should
append precise current status and cross-references rather than rewrite earlier approvals
or claim all historical Option B provisions are effective. No new source path is added
to an implementation allowlist by mentioning it here.

## 2. C1 — Approved offline exception, not execution permission

Proposed addition after the existing full-cold-request rule and corresponding export ban:

> A separately owner-authorized immutable-synthetic-release operation MAY invoke the
> unchanged frozen C# generator offline at a reviewed source revision. This exception
> applies only to independently generated BTC-like synthetic data, algorithm
> `aiq-synthetic-ohlcv-v1`, seed 114, coverage
> `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)` and unchanged canonical bytes,
> dataset/window/visible fingerprints. Production/export/promotion each require their
> own scoped authority, finite accepted budgets and literal paths.
> Anonymous requests, deployment, startup, background refresh, repair, restart or
> recovery SHALL NOT generate/export data or invoke a generator/Worker/provider fallback.
> The exception SHALL NOT alter existing Worker behavior or permit observed/private inputs.

Producer location/principal, network/input restrictions, command/output paths and reviewer
are still unselected. They must exclude private credentials, provider acquisitions,
observations and private derived outputs. Existing offline tests are not export permission.

## 3. C2 — Provenance, ownership and trust

> A release SHALL have immutable content-bound identity, accepted member inventory,
> exact schema/source/seed/window/row/byte/fingerprint checks and an auditable producer,
> independent verifier, reviewer and promotion chain. Trust SHALL be anchored outside
> the artifact's self-reported hashes. The public principal SHALL NOT modify assets,
> trust anchors, executable code, promotion state or their ancestor directories.
> Unknown/duplicate/extra members, traversal/links, partial/mixed versions, malformed
> canonical values, unsupported identity or any trust/parity mismatch SHALL fail closed.
> Public artifacts/evidence SHALL contain no private provider data, credentials or paths.

Manifest serialization/schema/version, authenticated pin or signature/key lifecycle,
source/build identity, immutable naming, owning principals and promotion paths remain
unselected. Checked-in trusted pins and authenticated signatures are alternatives,
not approvals; signature private keys and owner credentials never belong in artifacts.
Current public ownership of `/app` does not satisfy immutable trust. No ownership,
image, mount, key, dependency or schema change is authorized here.

## 4. C3 — B1 selected; exact analytics parity required

Proposed narrow WP04 exception for the separate immutable context:

> B1 SHALL use candles plus independently verified offline-precomputed analytics from
> the unchanged shared C# calculator. It MAY replace per-response calculation only in
> the accepted immutable context; existing Worker and candle/cache
> `CalculationVersion == null` semantics remain unchanged. No production Python port.
> All twelve derived outputs SHALL be independently reproduced and bound to complete
> calculation windows, exact fixed parameters and dataset/window/visible identities.
> Exactly 250 preceding candles remain calculation-only. No hidden warmup values,
> reseeding, zero/forward-filled nulls or fake gap candles SHALL be published.

SMA20, SMA-seeded EMA20, Wilder RSI14, MACD12/26/9, population Bollinger20x2, scale
`10^12`, frozen rounding, first-valid positions, signed bands and nullable alignment
remain unchanged. Exact deterministic integer/decimal parity admits no invented
floating tolerance. Complete derived fingerprints/oracle remain unqualified; existing
sample vectors are insufficient for all twelve exported outputs. Indicator readiness
stays unavailable until complete parity and integrity pass. B0 is not the selected variant.

## 5. C4 — Fixed window and API compatibility

> Public Preview SHALL remain anonymous, free and labeled exactly
> **BTC-like Market Simulation — Synthetic Historical OHLCV**, with illustrative units,
> research/no-investment-advice context and truthful fixed coverage. Twelve Data stays
> private, Vike inactive; no public observed data or cross-context fallback.
> Preserve six GET-only paths, existing allowlisted fields/defaults and safe errors.
> Only twelve qualified end-anchored 1D/7D/30D/90D windows at 1h/4h/1d MAY become ready.
> Otherwise valid unqualified ranges remain 503 `DATASET_NOT_READY`, never silently
> truncated/rebased/reseeded. No rolling window or algorithm/fingerprint migration.

Visible maxima remain 2160/540/90 by interval, with 250 hidden calculation rows. Retain
current 4096-byte request path/query and unavailable/error/metadata response behavior,
16384-byte application header accounting, no request bodies, rate/concurrency controls
and health independence. These are existing application bounds, not measured ingress
or full-response guarantees. Ready candle/indicator schema/maximum bytes and any pagination
decision require explicit acceptance. No new parameter, schema or REST behavior is enacted.

## 6. C5 — Approved narrow durable security-state exception

Proposed replacement of the broad no-mutable-shared-state statement in future-context
governance, preserving market/private immutability:

> Public market-data artifacts SHALL remain immutable. Mutable shared market-data state
> and sharing private research state with public execution remain prohibited. Separately
> controlled persistent security/abuse-accounting state MAY exist solely for authoritative
> durable admission/protection across the approved instance/restart domain. This narrow
> exception selects no backend, principal, service, filesystem, topology or paid resource.
> Updates SHALL be bounded, atomic, concurrency-safe and purpose-limited. Unknown,
> corrupt, inconsistent, inaccessible or rollback-ambiguous required state SHALL deny
> affected work; restart, release/client rotation, timeout or uncertainty SHALL NOT reset
> counters, grant fresh budget or silently reopen admission.

Proposed constraints on that selected principle:

- Minimize stored data to necessary domain/counter/reservation/status/time/high-water
  and audit references. Client identifiers, if necessary, need separately reviewed
  minimization/pseudonymization and proxy-trust semantics; anonymous identity is not trust.
  No market candles, private features, provider credentials, raw request bodies or
  unnecessary raw IP/UA details in the security store or public logs.
- Separate control-state authority/paths/permissions from immutable assets and private
  research. Public requests may trigger only mediated bounded accounting transitions,
  never arbitrary writes, resets, backend administration or recovery endpoints.
- Choose retention/cleanup from accepted security windows and evidence obligations;
  never expire unresolved occupation/charges or delete counters to grant capacity.
  Retention, size, clock/recycle semantics and log ceilings remain
  **TBD — measurement required**.
- Recovery is an auditable authorized operator action after independently verified
  state/cleanup evidence. No time-only recovery, fail-open fallback, automatic reset
  or use of private stores. Store authentication must be least-privilege and must not
  embed owner credentials; specific identities/keys remain unselected.

This resolves the conceptual R2 prohibition, not backend or durability qualification.
An in-memory per-process limiter is not durable authoritative protection. Filesystem
journal, SQLite or an existing transactional resource remain engineering alternatives;
none is selected or demonstrated zero-cost/feasible by this exception.

## 7. C6 — Isolation, full-response admission and gates

> No public generator, Worker, provider, export, repair or private fallback path SHALL
> exist in the qualified immutable reader. Confidentiality, path/mount/environment/FD/
> `/proc`/network negatives and shared-contract publication validation remain mandatory.
> Bounded admission SHALL cover verification, loading, projection, serialization,
> retained allocations and complete response send/cancellation. No unbounded queues,
> automatic retry storms or identity/release rotation bypass. Health remains independent.
> Generation-specific gate applicability requires an explicit owner-approved mapping;
> immutable data SHALL NOT supply a blanket G-B0-G-B7 waiver or revive advanced WP07.

Current active-slot release before ASGI sending and process-local counters are retained
source gaps, not remediated here. Full-send/cancellation/concurrency, memory/CPU/read
budgets, durable domain/backend and health-breaker criteria remain unqualified. No
request, environment variable, manifest or planning merge can flip the existing false gate.

## 8. C7 — Retention, promotion and recovery

> Immutable release retention SHALL be explicitly bounded and distinct from unchanged
> 24h request-cache retention. An authorized release operator controls whole-version
> promotion, revocation, retirement and eligible rollback; no public control endpoint.
> Readers SHALL pin one verified release and bytes, never validate a pathname and reopen
> replaced content or mix versions. Startup/restart MAY only validate existing accepted
> bytes under separate authority, not generate/export/repair. Missing, corrupt,
> inaccessible, unsupported or unqualified data SHALL yield null data and
> 503 `DATASET_NOT_READY`, with safe readiness/diagnostics and cheap independent health.
> Rollback SHALL name an explicitly eligible whole trusted release; no automatic older-file
> or provider fallback after integrity failure. Fixed historical age is not live-market staleness.

Member/aggregate/temp bytes, versions retained, lifetime, promotion atomicity, active-read
revocation, response deadlines and recovery ceilings remain TBD. No budget is guessed or
borrowed silently from the bridge/cache. Unknown mandatory evidence keeps data unavailable.

## 9. C8 — Effectiveness, thresholds and authorization ledger

P1-P7/S/B1/T owner choices are recorded now; this exact reconciled canonical wording and
its target-file traceability still need explicit reviewed acceptance. Existing canonical
records are unchanged. Unmeasured response, CPU/RSS, send/cancellation, admission,
durability, retention, disk/temp, recovery, target and test ceilings remain TBD and
must be accepted before their affected activity. Numerical semantics are frozen, not TBD.

No implementation, artifact production/export/promotion, local active proof, Azure
qualification, deployment or public activation follows from these selections. Each needs
separate finite authority and literal mutation/test/output paths as applicable. Historical
WP07 work remains on hold; `PUBLIC_RUNTIME_VERIFIED=False` remains unchanged.
If a required predicate is unknown, STOP the affected step. Rollback/revocation returns
to unavailable behavior; no reset, widened fingerprint tolerance, private fallback or
implicit generation exception. A planning PR merge alone does not enact policy.
