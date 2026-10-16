# C2B-5 — Immutable synthetic snapshot architecture evidence

Date: 2026-10-15 (America/Sao_Paulo). **Documentation-only recommendation; not architecture acceptance.**
`PUBLIC_RUNTIME_VERIFIED=False`. Public activation and Azure deployment remain **NO-GO**.

## 1. PR #334 merge and clean baseline

- Reviewed PR: <https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/334>.
- Reviewed head: `863183854e14fbd9cf5908f1922274d3f9b12b47`; base `main`; exactly
  the C2B-4 authority, evidence dossier and 41-line HTTP test addition. No production change.
- GitHub preflight: MERGEABLE/CLEAN, no required status checks configured, zero required
  approving reviews, no reviews and no unresolved conversations; no repository rulesets.
  Enforced administrator protection and conversation-resolution rule remained intact.
- Fresh full Python run at that head: 173 total, 162 passed, 11 platform skips, exit 0.
  This includes the 16 focused shell/API cases and actual localhost HTTP/restart checks.
  Inherited C2B-4 .NET evidence: 570 solution tests plus 50 packaged offline protocol
  tests passed. .NET was **not rerun** for this documentation-only stage; the reviewed
  PR did not change .NET implementation/tests or dependency files.
- Merge method: permitted merge commit, matching the exact reviewed head; no admin,
  force, bypass, branch deletion, protection change or issue closure.
- Merge commit: `fc86f4318b75ba0eb5c62dc0171ede7454148290`.
  GitHub merge time: `2026-10-16T02:43:56Z`, or `2026-10-15T23:43:56-03:00`.
- Canonical checkout: `C:\projects\github\AIQuantTradingResearch`. Switched safely to
  `main`, fast-forwarded, verified empty porcelain status and `HEAD == origin/main`
  at the merge SHA. Merge tree equals the reviewed PR head tree.
- The sole initial untracked C2B-5 owner prompt matched the supplied authority after
  whitespace normalization. It was preserved temporarily outside the checkout to verify
  clean main, then restored on the dedicated planning branch; no user content discarded.
- Planning branch: `planning/release-1.14-chart-mvp-c2b-5`, created from that clean main.

No commands inspect, alter or move the preserved original WP07 checkout or archive in
this stage. Their historical verification is not represented as freshly rerun evidence.

## 2. Exact documentation-only inventory

1. `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-5-merge-pr334-snapshot-architecture-codex.md` — owner prompt preserved.
2. `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-5-immutable-snapshot-architecture-evidence.md` — this dossier.
3. `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-5-immutable-snapshot-policy-proposal.md` — non-effective amendment text and D1-D10 mapping.

No changes to application/runtime/test code, snapshots/fixtures, source hash maps,
Dockerfile, entrypoint, schemas, dependencies, credentials, Azure or current policy.
The new planning PR must remain unmerged for owner review.

## 3. Governing sources and policy status

Current frozen requirements come from the owner's C2B-5 authority, the release
definition/execution plan and applicable accepted implementation contracts. Older
Section 10 headers and earlier Option B packets include proposed/pending wording;
existence or merge alone does not prove every historical amendment effective.
Where authority is unresolved, this assessment retains the stricter current disabled
behavior and proposes an explicit reconciliation; it does not activate an older option.

Read-only source anchors:

- `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`: complete-generation rule,
  request/cache bounds, retention, C2 supplemental resource/read criteria and no
  startup/background generation, exported fixture or production Python port.
- `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`: WP02 cold request,
  WP03 canonical/cache and WP04 analytic semantics; WP07 prerequisite to activation.
- `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`:
  Section 10 synthetic/private split, cache publication restriction, G-B0-G-B7 and C2
  obligations; Section 11 six-route, honest readiness and no-privilege REST contract.
- `prompters/release-1.14-wp04-implementation-authority.md`, Phase B: calculate per
  response, no analytics cache modification, candle/cache CalculationVersion null.
- `SyntheticHistoricalMarketDataProvider.cs` and its independent CPython/C# tests;
  `HistoricalMarketDataReadService.cs`, `HistoricalMarketDataQueryExecution.cs`,
  `TechnicalIndicatorCalculator.cs`, `historical_market_bridge.py`, public API/app,
  Streamlit UI, Dockerfile and public entrypoint: inspected as implementation evidence.

Cold-generation/export restrictions are not solved merely by declaring a new cache.
No current artifact format, trusted exporter, immutable loader, promotion mechanism,
complete analytic artifact goldens or ready-data REST implementation is qualified.

## 4. Alternatives and recommendation

| Concern | A: Existing request-time Worker | B: Immutable offline synthetic release | C: Synthetic-only SQLite |
| --- | --- | --- | --- |
| Data/source confidentiality | Offline algorithm is independent; public runtime/private mount/environment/FD/network boundary remains unqualified | Trusted independent producer plus public read-only release could reduce runtime capabilities; still must exclude private inputs/paths/credentials | Separate DB does not establish isolation; effective file/query/credential permissions must be proven |
| Determinism/integrity | Existing full C#/CPython bytes/hashes, twelve windows and numerical vectors retained | Preserve exact canonical bytes and pin a reviewed trust manifest; validate complete grids, hashes and derived bindings; no self-qualification | Canonical row serialization/order/precision and atomic metadata binding needed; a DB file hash alone is insufficient |
| Admission/cleanup | Durable exclusive spawn, reservations, independent descendant cleanup and unknown-state denial remain blockers | No public generation/Worker is a possible different applicability domain, requiring owner-approved proof and gate mapping; offline producer remains separately bounded | Does not remove Worker/analytics or publication constraints; adds write transactions, lock/recovery concerns |
| Resource/abuse | Existing generation plus capture/cleanup and warm-cache child cost; health headroom unqualified | Removes request generation only if no fallback exists; hashing/loading/serialization/UI/ingress still cost CPU/RAM and require authoritative bounded reads | Connection/query/scan/lock costs and corruption recovery need extra bounds; no headroom evidence |
| Analytics/browser | Existing C# calculator runs per response; UI/REST currently unavailable behind false gate | B0 candles only leaves indicators unavailable; B1 precomputed per-window analytics needs explicit WP04 amendment; no production Python calculator | Storing candles alone leaves the same analytic integration question; copying derived rows requires B1-style authority |
| Startup/restart | No startup generation; unknown cleanup/journal state cannot reopen admission | Bounded startup/restart validation, one pinned accepted version; missing/corrupt release stays unavailable, no automatic rebuild | Requires supported locking/transaction/durability semantics and fail-closed interrupted writes/restart |
| Atomicity/rollback | Existing validated cache behavior plus unqualified runtime lifecycle | Candidate versioned whole-release promotion; same-volume semantics and reader pinning must be proven, or immutable image deployment qualified | Transaction commit/version snapshot, sidecars and recovery must be proven on target mount; avoid mixed row/metadata versions |
| Azure F1/storage | Existing constraints unresolved; no target measurement in this stage | Could package read-only release without persistent writable state/additional service; image lifetime, cold start, ownership and resource constraints unverified | No storage backend/mount qualification; journal/WAL/local-vs-persistent behavior and restart loss remain questions |
| Zero recurring cost | No new paid resource authorized; not an availability proof | Strongest candidate for no added recurring service, but build/artifact retention and actual plan usage still need owner evidence | Could use existing storage only if supported and cost-free; no such verified selection made |
| Current decision | Keep disabled; do not revive WP07 | Recommended conditional candidate, not approved/implemented | Not selected; extra mutable-state obligations without demonstrated benefit |

**Recommendation:** request explicit consideration of B1 for a complete fixed-window
chart without a request-time Worker, or B0 if the owner prefers a candles-only intermediate
milestone. Do not export or integrate either before the proposed amendments are accepted.
Immutable files can eliminate request-time generation as a mechanism, but cannot by
themselves waive confidentiality, provenance, read abuse, recovery or target-runtime gates.

### Important implementation seams discovered

1. **Derived analytics are a separate amendment.** Frozen WP04 calculates per response.
   Packaging indicator results is an additional publication/cache decision, not a
   free consequence of exporting candles. Retaining C# calculation per request would
   need runtime integration and may retain the WP07 process lifecycle requirements.
2. **Ownership currently prevents an immutability claim.** Dockerfile assigns `/app`
   recursively to `aiq`. If artifacts/trust anchors/code were simply placed there, the
   public UID would own them. Same-owner chmod and a self-reported manifest hash are
   insufficient. No Docker/UID/mount change is implemented here.
3. **Twelve seed windows versus arbitrary viewport queries.** The API currently validates
   aligned from/to ranges; accepted analytic fingerprints are selection-specific. A
   ready reader cannot silently use a 90D seed for a 7D request. Bind all reads to the
   approved full calculation window and define safe unavailable behavior for unqualified
   selections before ready integration. No query schema is changed by this packet.
4. **Error cap is not a ready payload budget.** Current public API cap is 4096 bytes;
   a full 2160-row candle/analytics response needs a separately accepted bound. The
   bridge's 2 MiB limit and old cache limits do not automatically govern a new store/API.
5. **Retention and freshness differ.** A 24h request cache cannot quietly become an
   indefinitely retained release asset. Fixed historical coverage and verification time
   must remain separate from market freshness or rolling daily updates.

## 5. Candidate lifecycle — obligations, not executable design

1. Owner approves exact policy and producer/validator scope. A bounded offline operation
   uses the existing generator and optional shared calculator in an input/network/private-data
   constrained environment. Do not acquire or perturb observed candles.
2. Independently reproduce full canonical bytes/hashes and, for B1, all twelve derived
   result byte streams. Existing analytical sample vectors are not complete exported
   analytics goldens. Keep canonical dataset/window/visible fingerprints unchanged.
3. Produce version/provenance/coverage/parameter bindings and separate trusted reviewed
   release hashes. Record generator revision, producer authority and independent review.
   No credentials/private artifacts in either release or evidence.
4. Qualify a bounded, non-writable-by-public release and trust anchor. Decide aggregate,
   member, decompression-if-any, validation CPU/RSS, startup and ready-response budgets
   before implementation. A format requiring unbounded parsing/extraction is unacceptable.
5. Promote a complete verified version atomically, pin reader bytes/version and test
   replacement races, path/link attacks and mixed-version rejection. Process-local
   validation success cannot substitute for this storage/ownership proof.
6. Restart revalidates bounded immutable state. Missing/corrupt/inaccessible/unsupported
   content remains unavailable with public-safe errors; no auto repair, fallback or generation.
   An operator-only whole-release rollback must name an accepted version and revalidate it.
7. Only separately accepted API/UI/gate integration may publish visible rows. No hidden
   warmup, invented gap filling, provider fallback, fake freshness or request-time Worker.

None of these steps was executed. Snapshot production, startup loading and publication
are prohibited in this authority; no sample/fixture export is present in the PR.

## 6. Qualification and acceptance matrix

| Evidence area | Required proof before acceptance | Current status / gate |
| --- | --- | --- |
| Owner/policy authority | Exact cold/export/cache/analytics/integration amendments and applicability decision; literal path allowlist | **BLOCK**, proposal only; G-1/G-2/G-B0 |
| Independent provenance | Authorized isolated producer, no observed inputs/private/provider capabilities, trusted review chain | **GAP**, existing independent algorithm tests inherited; G-3/G-B2/G-B5 |
| Reproducibility | Complete independent C#/reference bytes, unchanged 3 dataset/12 window/12 visible hashes; B1 complete analytic byte/hash proof | Candle regression evidence inherited; exported/B1 artifact proof **GAP**; G-B5 |
| Immutable validation | Bound parsing, canonical/identity/precision/grid/hash/link/race negatives; public cannot alter code/anchor/artifact/ancestors | **BLOCK**, current ownership not sufficient; G-B2/G-B5 |
| Atomic deployment | One release, no partial/mixed version, concurrent old/new reader behavior, interrupted promotion and approved rollback | **GAP/UNTESTED**; G-B4/G-B6 |
| Dataset readiness | Available only after all required release/state/authorization checks; fixed actual coverage, null on failure | Current unavailable routes fresh HTTP pass; ready implementation **BLOCK** |
| API integration | Six bounded GET routes, ready schemas/decimal semantics/limits/selection mapping, no mutation/admin exposure | Unavailable API fresh pass; ready data **BLOCK**; C2B-API-01/03/05/06 |
| UI/browser | Truthful synthetic label, fixed coverage, no stale retained chart, correct Plotly rows/indicator/null/gap semantics, accessibility/mobile checks | Existing AppTest fresh pass; real chart/browser **UNTESTED** |
| Negative isolation | Private caches/mounts/environment/FDs/proc/paths/provider network/derived-data negatives on target | **BLOCK/UNTESTED**, no new claim; G-B2 |
| Read rate/concurrency | Authoritative deployment-domain controls, pre-validation budgets, bounded allocations, restart/flood/client-rotation behavior and cheap health | Process-local API negatives fresh pass; durable/global/target proof **GAP**; G-B3/G-B7 |
| Crash/restart recovery | No public spawn/fallback; no mixed publication or silent trust restoration; accepted rollback; prove generation-domain applicability separately | Clean localhost restart fresh pass only; crash/atomic storage proof **GAP**; G-B1/G-B4 |
| Azure F1 headroom | Target cold/warm CPU/RSS/latency/ingress/health, supported image/filesystem/restart and zero added recurring cost | **NO-GO/UNTESTED**; G-B3/G-B6/G-B7 |
| Runtime gate/activation | Explicit qualified gate architecture and separate owner acceptance/activation | False gate retained; **NO-GO** |

No unmeasured number is promoted to a qualified threshold. The policy proposal maps
each deviation D1-D10 to governing requirements, owner decisions, gates and rollback.
No mandatory public/private protection is reclassified as satisfied or waived here.

## 7. Tests, checks and non-tests

Fresh merge validation command (Windows PowerShell 5.1):

```powershell
$env:PYTHONDONTWRITEBYTECODE='1'
& '.venv\Scripts\python.exe' -B -m unittest discover -s python/presentation -p 'test_*.py' -v
git diff --check
```

Python: **173 total, 162 passed, 11 platform skips, exit 0**. The actual localhost
Streamlit tests assert `/` and `/_stcore/health` 200; API health/status/diagnostics 200;
dataset/candles/indicators 503 `DATASET_NOT_READY`; an invalid query on each route 400
`INVALID_QUERY` without echoing sensitive-shaped input; `/admin` 404; dataset POST 405.
Clean server restart does not promote dataset/indicator readiness. These are ordinary
local server tests, not native Worker experiments, browser or Azure qualification.

Inherited .NET **620 passed** is recorded from C2B-4 with no new execution claim.
Current packet changes only Markdown; no additional application test run is justified
by those edits. Whitespace and exact Markdown-only path scope are checked before PR.

Not executed: snapshot generation/export/validation/promotion; new golden production
artifacts; SQLite tests; filesystem/UID/native/FD/process-tree fault experiments; Docker;
Azure diagnostics, RBAC/configuration, deployment, real-provider requests; browser tests;
load/resource/cost qualification; gate changes. No access to protected old WP07 state.

## 8. Disposition, next authority and rollback

- **GO:** owner review of this documentation-only comparison/proposal and retained local
  unavailable shell/API evidence. PR #334 consolidation is complete.
- **BLOCK:** snapshot implementation/export, ready-data publication and any policy-dependent
  integration until exact amendments and their qualification scope are separately accepted.
- **NO-GO:** target-runtime qualification under this authority, public activation and Azure deployment.

Next owner decisions: choose A/B0/B1/C; approve or reject each D1-D10 as applicable;
resolve exact artifact/trust/immutability/promotion/retention/selection/budget design and
gate mapping. A later bounded offline exporter/validator work package needs a literal-path
allowlist and independent proof requirements. Active target diagnostics and deployment
need distinct authority; planning acceptance never grants those actions.

Rollback of this packet requires no data migration or operational recovery: decline the
planning PR, or later use a normal reviewed documentation revert. Existing unavailable
behavior remains unchanged. Proposed future data rollback must be an operator-approved
whole qualified release, never an automatic corrupt-store regeneration/provider fallback.

**The new C2B-5 documentation PR must not be merged under this authority.**
