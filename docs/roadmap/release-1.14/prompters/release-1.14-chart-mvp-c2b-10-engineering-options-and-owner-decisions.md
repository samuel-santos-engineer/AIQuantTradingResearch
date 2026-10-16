# C2B-10 — Engineering options and owner decisions

Date: 2026-10-16 (America/Sao_Paulo).
**DESIGN RECOMMENDATIONS ONLY — NO MECHANISM APPROVED — NO IMPLEMENTATION AUTHORITY.**
The owner approved P1-P7/S/B1, retained T as TBD and accepted exact PR #339. That does not select the
engineering mechanisms below or authorize code, tests, generation/export/promotion,
platform changes or activation. `PUBLIC_RUNTIME_VERIFIED=False`; public data unavailable,
advanced WP07 ON HOLD and public activation/Azure deployment **NO-GO**.

## E1 — Reviewed release pin first, signatures later if needed

**Recommendation for review:** a separately reviewed first-party release digest/pin,
bound to a qualified artifact manifest and reviewed source revision, shipped with
non-public-writable reader code in an immutable image. The pin is not accepted from a
request, environment override, writable manifest or the artifact itself. No pin/hash
value, key or schema file is created in this phase.

Alternative signed manifests support independent producer/promotion channels and key
rotation but add signer/key custody, signature library/algorithm, revocation and rollback
management. A digest needs no signing secret and is the smallest first release choice
when the entire image/review chain is trusted. It does **not** protect against compromise
of trusted reviewers/build infrastructure or an attacker who can replace both code and
pin. Neither approach solves public-writable ownership, private isolation or downgrade
eligibility on its own. Dependencies: E2/E6/E7 and owner-accepted trusted producer/reviewer.

Proposed manifest design, not an accepted schema:

- Canonical manifest core: supported format version, pinned producer/calculator source
  revision, frozen public identity/version/seed/coverage, exact member inventory and
  lengths/digests, three dataset records and twelve selection records with calculation/
  visible counts, starts, 250 warmup and frozen dataset/window/visible fingerprints.
- B1 members bind `aiq-ta-v1`, fixed parameters, visible timestamps/null semantics,
  complete derived byte length and independently verified derived digest. Reviewer/
  producer authority references belong in provenance; no credentials/private paths.
- Recommend UTF-8 without BOM, LF, sorted ASCII object keys, compact JSON, no duplicate
  or unknown fields, no floats/NaN, integer counts and exact decimal **strings**. Arrays
  have fixed interval/range order. Dataset member bytes keep their existing canonical
  encoding; this proposed wrapper cannot replace semantic candle fingerprints.
- Recommend `releaseId = "sha256-" + digest(canonical manifest core)`, with the ID outside
  that core to avoid self-reference. Operational promotion/verification timestamps and
  signatures are outside the deterministic core. The trusted reviewed pin binds the
  exact core; member digests bind content. No digest is computed here.
- Proposed members: three complete canonical candle streams plus twelve visible-only
  analytic streams and a manifest. Calculation windows are reconstructed/verified offline
  from the complete streams, not exported as public downloads. Logical names are fixed
  allowlisted IDs, never query-selected paths; proposed literal examples are in the plan.
- Reject unsupported version, extra/missing/duplicate member, invalid encoding, size,
  identity, grid, precision, count, fingerprint or derived binding. Bound parsing before
  allocation; no archives/compression/recursive includes for the first candidate.

Anti-TOCTOU recommendation: immutable image-owned bytes, validated handles/buffers pinned
for a single release; never verify a path then reopen changed content. Lexical path
validation alone cannot reject links or races. Exact format, maximum sizes and handle/
ownership proof remain unapproved; zero trusted pin matches means unavailable.

**Owner prompt:** approve/revise/defer digest-pin-first, the proposed deterministic core
and no-compression layout? Name the trusted reviewed revision/producer/reviewer and
accept the threat assumptions before later implementation. Signatures remain an option,
not a selected effective mechanism.

## E2 — Prefer image-bundled root-owned assets for the first candidate

**Recommendation for review:** operator-built immutable image containing code, reviewed
pin and release assets not writable by the serving UID or through writable ancestors.
Promote/rollback a complete qualified image, not a live public-writable directory.
Readers keep one release identity/byte set for the whole response. UI must reject mixed
release identities across separate metadata/candle/indicator responses rather than
silently overlay old/new data. This is not a new query parameter or accepted REST rule.

Operator-controlled versioned storage permits replacing data without rebuilding an
image but adds atomic pointer/same-filesystem semantics, rollback eligibility, path/link
races, writable promotion credentials and shared-mount consistency. Prefer image bundling
to remove that initial promotion surface. Deployment itself remains separately authorized.

Observed: Dockerfile currently recursively gives `/app` to `aiq`; that does not establish
immutable code/pin/assets. Later proof must examine **effective** UID, mount/read-only
flags, ancestors, file handles and inherited permissions. Root ownership alone is not
kernel isolation; no Docker edit or privileged deployment is authorized here. Unknown
Azure image/filesystem lifetime, replicas, restart routing or deployment atomicity is
**UNRESOLVED**, not an assumed F1 feature. No deployment-slot mechanism is presumed.

**Owner prompt:** approve/revise/defer image-bundled first release, subject to actual
non-writability and complete image promotion/revocation evidence? E1/E6/E7 must pass.

## E3 — Conditional SQLite control-state proof, not a backend selection

**Recommendation for review:** compare a **security-only SQLite** transactional candidate
in the first finite local backend proof, using the already referenced SQLite dependency,
but accept no backend until persistence/locking/ownership/domain evidence passes.
`Microsoft.Data.Sqlite` is already referenced; this is repository evidence, not a current
Azure storage or package-install qualification. No database or SQL schema is created.

| Alternative | Benefit | Failure/feasibility obligation |
| --- | --- | --- |
| Protected append-only journal | Small conceptual surface; no DB query engine | Replay/compaction/atomic durable append, corruption/partial record, concurrent writer, fsync/rename and rollback logic become custom security code |
| Security-only SQLite | Existing transaction implementation; bounded indexed ledger/counters, atomic multi-field updates | Correct target locking/durability/sidecars and protected mount/ownership, crash/recycle/clock behavior and finite busy handling must be proven |
| Already available transactional resource | Could cover actual multiple instances | No qualified existing resource identified by this repository inspection; actual availability, least-privilege auth, consistency, latency and permanent zero incremental cost need separate inventory/proof; no new service implied |

Candidate admission domain: one explicitly verified serving instance, persistent
authoritative state across its restarts; **not a verified deployment topology**. If
multiplicity/recycle overlap is possible or cannot be disproved, a local store is not
deployment-global: deny activation until a shared consistent domain is qualified or
the owner explicitly approves another proven topology. Do not infer SQLite viability
on Azure mounts or silently select a paid resource when local durability fails.

Security-only semantic records (conceptual, not SQL): domain/schema identity, persisted
UTC high-water/epoch evidence, bounded admission/debit events, active/resolved status,
operation ID for idempotence and audit references. Global authoritative checks/debit
precede work in one bounded transaction; per-client counters are supplementary and
cannot bypass global limits. Client keys need purpose-minimized pseudonymization and
proxy trust review; no raw request bodies/market rows/private credentials or unnecessary
IP/UA retention. Actual limits and data lifetimes remain TBD.

Do not reuse Worker 16-second reservations as a fabricated no-spawn read CPU budget.
Charge types/units need measurement and acceptance. Crash/unknown outcome keeps required
protection conservative; no automatic reset/refund/eviction through restart, timeout,
clock/client/release rotation or corrupt store. Clock rollback, missing/inconsistent/
unreadable state and unverified recovery deny affected work. Operator recovery needs
auditable evidence, never a public reset command. Mutable security-state permission S
does not permit mutable market data or private-store reuse.

**Owner prompt:** approve/revise/defer SQLite as a *local proof candidate only*, the
single-instance assumption to verify and a protected-journal comparator? Store/domain/
principals/retention remain unselected until evidence; E6/E7/E8 prerequisites apply.

## E4 — Independent complete B1 oracle, exact arithmetic only

**Recommendation:** use the existing friend Infrastructure test assembly for first
in-memory qualification. Source inspection confirms complete-generation/serialization
APIs are internal and the assembly already has `InternalsVisibleTo`; no new production
API or reflection seam is needed for that first proof. A production exporter seam is
a separate later decision, not a reason to copy or modify the generator.

Have a separately authored test-only CPython oracle derive the frozen generator and
analytic formulas independently using integer/rational arithmetic, not production
output or translated calculator source. Preserve existing independent reference tests.
Compare all three complete datasets, all twelve calculation/visible windows and all
visible B1 analytic rows/nulls/identity/bytes. Freeze derived goldens only after independent
full equality and owner review; never take production hashes as expected values.

Cover SMA20, SMA-seeded EMA20, Wilder RSI14, MACD12/26/9, population Bollinger20x2,
scale `10^12`, exact ties-to-even and square-root midpoint behavior, signed bands,
first-valid positions, gaps/order/precision/identity negatives and no lookahead. Exactly
250 prehistory rows remain calculation-only. Different ranges retain their own recursive
seeds; overlapping rows need not have identical analytics. No floating tolerance is
invented for exact deterministic parity. Oracle is test-only, not a production Python port.

**Owner prompt:** approve/revise/defer the friend-test first proof and independent
complete oracle approach, with later separate output/golden acceptance authority?

## E5 — Full fixed-window reads; ready payloads remain illustrative

**Recommendation:** keep existing parameter/default/error semantics and twelve full
fixed-end selections; no initial pagination, arbitrary analytic reseeding or truncation.
Valid unqualified windows return 503 `DATASET_NOT_READY`; `limit` below the selected
window count retains 400 `ROW_LIMIT_EXCEEDED`. Maximum visible rows remain 2160/540/90,
not a measured byte/RSS budget. No rolling/current/live claim or provider fallback.

Illustrative **not accepted contracts** for the six existing GET routes:

| Route | Illustrative payload concept, not new behavior |
| --- | --- |
| health | Existing version/state/code/data liveness envelope, independent of data verification |
| status | Existing web/dataset/indicator readiness; future release eligibility reference only after all predicates pass |
| dataset | Future data object with trusted release ID, fixed actual coverage, generator/canonical identity, permitted intervals/windows and verified public provenance; never private paths |
| candles | Future data object with release/selection identity and visible `{time, open, high, low, close, volume}` rows using frozen exact decimal strings |
| indicators | Future data object with same release/window/visible timestamps, `aiq-ta-v1`/fixed parameters and selected indicator values/nulls; no hidden warmup rows |
| diagnostics | Existing stable safe failure-category concept; no manifest contents, stack traces, storage paths, process IDs, secrets or recovery controls |

Actual field names, ready state/code spelling and extra-field rules require contract
review; tables do not amend current schemas. Current 4096-byte unavailable/error cap
and 2 MiB bridge capture cap are distinct. Ready bytes, serialization allocations,
load/CPU/RSS, in-flight/send/cancellation and total HTTP/health limits remain
**TBD — measurement required**. Oversize ready output must deny, not enlarge a cap silently.

Reserve read admission through verification/load/projection/serialization **and send or
cancellation**, including retained bytes and slow readers. Current slot release before
ASGI send is a source gap, not a fix. Bound all waiting/state cardinality and health
overhead; no unbounded queues or retry storms. Future measurement must include maximum
decimal widths, all twelve windows, malformed/denied traffic, slow/disconnected readers,
client/release rotation, restart and cold/warm health, plus actual browser Plotly labels/
gaps/nulls/volume/indicator rendering. No such measurement is executed now.

**Owner prompt:** approve/revise/defer full-window/no-pagination and version-bound
illustrative response direction, subject to exact schemas and measured ceilings?

## E6 — Operator whole-release lifecycle and minimal control records

**Recommendation:** image-based complete-release promotion/revocation/eligible rollback
for market artifacts; separate security-state recovery with no budget reset. Prefer
that to online pointer mutation initially, but verify actual rollout/reader behavior.
Mark unqualified/corrupt/missing/inaccessible state unavailable, never auto-generate,
repair, select an older file or reach a private source. Restart revalidates accepted
bytes and authoritative control state; it does not manufacture trust or fresh capacity.

Security records retain only fields necessary for accepted accounting windows, unknown
outcomes and audit/recovery; expire only after validated evidence and safe window
completion. Do not clean unresolved occupation/debits to gain admission. Client metadata
minimization, operator/reader permissions and credential custody need review. Dataset
assets are not the security store and retain no private research material.

Eligible versions/counts, artifact/control retention, aggregate/member/temp/disk/log
space, audit data, response and recovery deadlines are **TBD — measurement required**.
Operator recovery must name a reviewed eligible complete release and resolve unknown
control state auditably. Already delivered bytes cannot be recalled; future in-flight
revocation/abort semantics must be owner-approved and tested. No private fallback.

**Owner prompt:** approve/revise/defer image-whole-release lifecycle with separate
fail-closed security recovery and no automatic rollback/reset? Exact values remain TBD.

## E7 — No-spawn narrows mechanics, never the security contract

**Recommendation:** qualify a dedicated public read-only/no-private-material context,
explicit no-spawn/no-provider path and effective negative isolation; retain all applicable
G-B0-G-B7 obligations until the owner accepts their precise mapping in the companion plan.
Do not revive advanced WP07 to satisfy this design by implication. Removing generation
may change spawn-specific evidence, not confidentiality, durable abuse or health needs.
Whether effective outbound-provider denial and immutable permissions are feasible under
unprivileged F1 is **UNRESOLVED** and needs separately authorized target evidence; code
review alone does not prove a kernel boundary. No namespace/firewall/cgroup support assumed.

**Owner prompt:** approve/revise/defer this conditional no-spawn applicability proposal
and negative-evidence matrix, with NO-GO if target controls cannot be proven at zero added cost?

## E8 — Friend-test proof first; production and integration are separate

**Recommendation:** start only after owner approval of the exact finite in-memory
proof scope/ceilings. Use existing test friendship and new independent oracle/contract
tests, no runtime wiring, no new production API and no materialized snapshot/export.
The companion plan lists candidate literal paths/output names and staged example
commands, all **not authorized**. A standalone production exporter/friend seam, actual
snapshot bytes, reader/UI integration, Docker/target/browser and activation need distinct
later scopes and evidence. No new package/service or guessed budget is recommended.

**Owner prompt:** approve/revise/defer the staged plan as a proposal, then separately
approve exact paths and finite safety ceilings before any execution or mutation.

## Compact owner decision table

No recommendation is recorded as owner approval; P1-P7/S/B1/T are not being reopened.

| Engineering choice | Recommendation for review | Owner response |
| --- | --- | --- |
| E1 | Reviewed digest pin + deterministic manifest core, no first-release signing secret | **PENDING: approve / revise / defer** |
| E2 | Image-bundled assets/code/anchor not public-writable, whole-image promotion | **PENDING: approve / revise / defer** |
| E3 | Conditional security-only SQLite local proof and verified single-instance domain; no effective backend selection | **PENDING: approve / revise / defer** |
| E4 | Existing friend test assembly + independent exact complete oracle | **PENDING: approve / revise / defer** |
| E5 | Full qualified window, no pagination, illustrative version-bound responses; limits TBD | **PENDING: approve / revise / defer** |
| E6 | Operator whole-release lifecycle, separate minimal fail-closed control recovery | **PENDING: approve / revise / defer** |
| E7 | Dedicated no-spawn public context; retain applicable gates and target negative proof | **PENDING: approve / revise / defer** |
| E8 | Finite in-memory offline proof first; no exporter/integration/target execution yet | **PENDING: approve / revise / defer** |
