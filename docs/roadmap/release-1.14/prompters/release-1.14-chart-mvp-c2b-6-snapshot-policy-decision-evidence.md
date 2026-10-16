# C2B-6 — Snapshot policy decision evidence

Date: 2026-10-15 (America/Sao_Paulo). **Proposed decisions for owner review only.**
Merged C2B-5 documents are planning evidence, not effective policy. This packet enacts
no exception and qualifies no snapshot architecture. `PUBLIC_RUNTIME_VERIFIED=False`.

## 1. Merge evidence, baseline and exact scope

PR #335: <https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/335>.
Reviewed head `96fb734d90ab5630332d08f36e05f67737fa8699`, base main, exactly three
C2B-5 Markdown files; no code/data/runtime/workflow changes. GitHub reported CLEAN and
MERGEABLE, zero required approving reviews, no reviews, no configured required status
checks, no rulesets, zero unresolved conversations. Enforced administrator protection
and required conversation resolution were respected. Permitted merge commit used with
exact-head guard; no bypass, force, protection edit, branch deletion or issue closure.

Final PR state: **MERGED**. Merge SHA `b7eec6376c4a761adc4f547d65ae403608ce35b1`.
Merge time `2026-10-15T23:55:05-03:00` (`2026-10-16T02:55:05Z`). Canonical active
checkout `C:\projects\github\AIQuantTradingResearch` was safely synchronized to main:
empty porcelain status, main == origin/main == merge SHA, ahead/behind **0/0**, and
merge tree identical to the reviewed PR tree. No protected WP07 checkout/archive operation.

The sole untracked file was the expected C2B-6 owner prompt. The attachment repeated
`#335#335` once; the local file had `#335`, with otherwise equivalent content after
whitespace normalization. The local authority text was preserved, temporarily parked
outside the checkout for clean-main verification and restored on the planning branch.
Three trailing Markdown hard breaks were normalized to blank lines for whitespace checks;
authority wording was not changed.
This was not an unrelated dirty change or implicit permission for other work.

Branch: `planning/release-1.14-chart-mvp-c2b-6`, created from the verified merge SHA.
Exactly three new Markdown paths, all in `docs/roadmap/release-1.14/prompters/`:

1. `release-1.14-chart-mvp-c2b-6-merge-pr335-snapshot-policy-decision-codex.md` — preserve existing owner authority.
2. `release-1.14-chart-mvp-c2b-6-snapshot-policy-decision-evidence.md` — this record.
3. `release-1.14-chart-mvp-c2b-6-snapshot-governance-amendment-proposal.md` — NOT APPROVED / NOT EFFECTIVE wording.

No current policy, source, tests, CI, packages, schema files, snapshot bytes, databases,
generator constants/fingerprints, REST contract, Docker/startup, credentials or Azure changed.

## 2. Sources and authority reconciliation

References are inspected at the merge baseline; line numbers identify existing clauses,
not proposals inserted by this work:

| Source | Governing fact / status |
| --- | --- |
| `PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`, Section 10 and C2 reconciliation | Synthetic-only public/private separation; cached-read/export restrictions; G-B0-G-B7. Historical proposed headings do not enact all older Option B wording |
| Same strategy, Section 11, especially failure/bounds/hosting clauses | Six frozen GET endpoints, safe truthful readiness, no mutation/admin/provider path; ready schemas/bounds need design and qualification |
| `RELEASE_1.14_DEFINITION.md:106` and `:422` | Full cold Worker generation; no startup/background/exported fixture/production Python port authority; equivalence requires separate approval |
| `RELEASE_1.14_EXECUTION_PLAN.md`, WP02/WP03/WP04/WP07 | Cold generation, canonical/cache semantics, fixed analytical semantics and runtime qualification dependency |
| `release-1.14-wp04-implementation-authority.md:28` | Full 250-warmup validation, visible-only projection, calculate per response; no analytics-cache modification |
| C2A and C2A-2 authority records | Startup/read-only runtime investigation scopes, historical evidence limitations, no independent provider/admin activation authority; not current Azure proof |
| C2B shell evidence (PR #333), C2B-4 dossier (PR #334) | Locally tested unavailable routes and shell, frozen coverage, no target/browser/native qualification |
| C2B-5 evidence and D1-D10 proposal (merged PR #335) | Alternatives and non-effective deviations, not approved policy or implementation acceptance |
| Current API, bridge, Worker, read service, calculator contracts, Dockerfile | False gate, 4096-byte unavailable response cap, selection validation, per-response analytics, current ownership and admission limitations |

Where older policy publication/effectiveness is ambiguous, retain restrictive current
behavior. The C2B-6 owner authority explicitly withholds approval of the proposed
exceptions. No historical uncertainty is resolved by inventing permission.

## 3. Decision and conflict matrix

Every recommendation below remains **PROPOSED**, including exact P1-P7 wording in
the companion file. Frozen numerical/data semantics stay frozen; new operational
thresholds stay TBD unless measured and separately accepted.

| Decision | Recommendation / reason | Existing conflict | Owner decision and gate |
| --- | --- | --- | --- |
| S1 Offline production | Bounded owner-controlled offline release producer at reviewed source revision, no private/provider inputs; never public/startup generation | Cold/export restrictions, C2B-5 D1/D2 | Accept narrow P1 exception and later literal-path production authority; G-B0/G-B2/G-B5 |
| S2 Trust/manifest | Reviewed external trust anchor binds complete immutable release and provenance; semantic manifest groups specified in P2 | Artifact cannot self-authorize; current `/app` is public-owned, D9 | Select format/anchor/ownership/promotion model and prove public cannot alter any trust component; G-B2/G-B4 |
| S3 Indicators | Conditional B1: offline frozen C# calculator, all twelve complete outputs independently verified; B0 remains candles-only alternative | WP04 per-response/no-cache rule, D4 | Explicit P3 exception, complete parity and derived hashes before export acceptance; G-B5 |
| S4 Windows | Twelve fixed end-anchored selections; other valid queries remain safe unavailable; no new parameters, rebasing or pagination | Arbitrary aligned API ranges vs twelve seed windows, D8 | Approve exact ready selection semantics and frozen response identity; C2B-API-03/G-B5 |
| S5 API bounds | Retain existing input/error/metadata caps; ready candle/indicator byte cap TBD from measured full outputs; reject rather than truncate | 4096 error cap cannot prove full ready capacity, D7 | Approve schema, byte budget and ready validation rules; C2B-API-01/06/G-B3 |
| S6 Isolation/read admission | Immutable data plus separately qualified confidentiality and whole-response bounded admission, authoritative durable abuse state | No spawn is not a blanket gate waiver, D5/D6 | Explicit gate applicability and instance/restart domain; G-B0/G-B1/G-B2/G-B7 |
| S7 Retention/recovery | Operator-owned bounded release lifetime and whole-version promotion/rollback; missing/corrupt -> unavailable; no repair on request | 24h cache is not permanent asset storage, D3/D10 | Accept separate retention/member/aggregate limits and atomicity model; G-B3/G-B4/G-B6 |
| S8 Operations | Local reproducibility/security/browser and separately authorized F1 qualification before activation | No target isolation/headroom/storage proof | Decide measured thresholds, qualification scope and explicit later activation; G-B2/G-B3/G-B6/G-B7 |

## 4. Alternatives and recommendation

| Option | Benefit | Cost/risk and disposition |
| --- | --- | --- |
| Immutable file release B1 | Potentially no request-time generator or analytic Worker; version-bound reproducibility and bounded read-only inputs | Extra WP04 timing exception, trust/ownership/atomicity/derived parity/read-budget proof; **conditional recommended complete-chart candidate**, not selected for implementation |
| Immutable B0, candles only | Smaller publication exception, no offline derived cache | Indicators unavailable; incomplete chart+indicator objective; owner may choose as intermediate |
| Synthetic-only SQLite | Potential transactions and indexed reads | Writable state, sidecars/locks, durability/mount/recovery/ownership proof; does not solve provenance or analytics authority; **not recommended without demonstrated need** |
| Existing request-time Worker | Frozen implementation and strong offline algorithm tests | Held process lifecycle/admission/confidentiality/resource qualification still needed; **retain disabled, do not revive WP07** |

Immutable serving can remove request-time generation only if the absence of any
generator/Worker/provider fallback is proven. It does not eliminate ingress cost,
confidentiality, read accounting or recovery requirements. No option is proven feasible
on Azure F1 and none is accepted just because it could avoid another paid service.

Additional source-confirmed gaps: the API's process-local active slot is released before
ASGI response sending, so it is not proof of one complete in-flight response. Full-send
memory/deadline/concurrency and slow-client behavior need independent qualification.
Dockerfile recursively gives `aiq` ownership of `/app`; placing a file and pin there
does not establish immutable trust, and chmod alone under the same owner is insufficient.
No remediation of either source behavior is authorized in C2B-6.

## 5. Bounds and numerical decision register

Frozen data: `[2025-10-26, 2026-10-01)`, version/seed unchanged; complete rows
8160/2040/340; visible maxima 2160/540/90 at 1h/4h/1d; exactly 250 hidden warmup rows;
1D/7D/30D/90D matrix; final reference visible range begins 2026-07-03. No rolling claim.
P3 enumerates all fixed indicator parameters and preserves null/gap/precision semantics.

| Quantity | Evidence class / disposition |
| --- | --- |
| 4096 path+query and current response bytes; 16384 header accounting; zero request body | Existing API implementation, not transport ingress qualification; preserve |
| Existing row limits, intervals, UTC alignment and parameter allowlists | Frozen/current contract; preserve without new pagination or hidden range mapping |
| Process-local reads 2/second and 30/minute, health 10/second and 120/minute, receive 250ms | Current implementation only; not accepted durable/global F1 budgets |
| Ready candle/indicator encoded maximum, manifest/member/aggregate/temp bytes | **TBD**: measure exact all-window/worst-decimal outputs and resource envelope, owner approves before implementation |
| Artifact lifetime, retained eligible versions/count, validation deadline/RSS, full send deadline/in-flight allocations | **TBD**: choose bounded design and qualify restart/promotion/slow-client cases; no guessed thresholds |
| Durable global/session read budget, health breaker/freshness/hysteresis, F1 CPU/memory/latency/availability limits | Existing candidate criteria remain candidates; target and abuse evidence plus owner acceptance required; no fabricated measurement |

TBD is an implementation/acceptance blocker, not a deferred safety check after activation.
Existing bridge 2 MiB capture and cache 12 entries/32 MiB/24h cannot silently become
new artifact or REST limits. Fixed dataset age alone cannot justify stale/live claims.

## 6. Measurable acceptance matrix

| Gate | Required pass evidence | Current verdict |
| --- | --- | --- |
| Q0 Policy/authority | Owner explicitly accepts exact applicable P1-P7 and S1-S8; reconciled definition/plan/strategy/manifest/WP04/WP07 record plus literal implementation scope | **BLOCK**, proposal only |
| Q1 Independent producer/provenance | Reviewed pinned source/producer environment; no private/observed/provider inputs or credentials; complete auditable production/review chain | **GAP**, no export authorized |
| Q2 Determinism/analytics | Exact canonical byte equality for all 3 datasets and 12 calculation/visible windows, unchanged hashes; B1 independent complete derived byte/hash parity for all 12, no hidden rows or seed substitution | Candle regressions inherited; B1 artifact parity **GAP** |
| Q3 Immutable trust | All tamper/traversal/link/unknown-member/duplicate/precision/grid/race cases reject; public UID cannot write anchor/code/artifact/ancestors; bounded verification | **BLOCK**, current ownership and format unqualified |
| Q4 Promotion/recovery | No mixed/partial release under interrupted promotion/concurrent readers/restart; explicit accepted rollback only; all unknown/corrupt/missing cases deny | **GAP**, no artifact mechanism |
| Q5 API semantics | Each endpoint normal/unavailable and relevant negative HTTP cases; exact statuses/schema/identity/selection/bytes; no mutation/admin/provider fallback; health independent | Unavailable HTTP inherited pass; ready data **BLOCK** |
| Q6 Read abuse/resources | Full operation including send bounded, measured CPU/RSS/latency, counter rollback/restart/client rotation/slow-reader cases, independent health; no silent budget reset | **GAP**, process-local tests are not full qualification |
| Q7 Private isolation | Target negatives for caches, credentials, paths/mounts, environment/FDs/proc, private-derived outputs and outbound providers | **BLOCK/UNTESTED** |
| Q8 Browser | Actual browser candlesticks/volume/indicators/nulls/gaps, truthful exact synthetic/fixed coverage label, accessibility and responsive behavior | **UNTESTED**, no real chart/browser run |
| Q9 Azure F1 | Separately authorized target image/filesystem/ownership, cold/restart/promotion, bounded load/health/headroom and zero incremental recurring cost evidence | **NO-GO/UNTESTED** |
| Q10 Activation | Separate owner acceptance after all applicable gates and explicit deployment/public activation authority | **NO-GO**, false runtime gate retained |

Qualifying cases must have zero unauthorized private/provider exposure, zero silent
fingerprint/numerical/window changes, zero ready responses from invalid required state,
and no partial/mixed release publication. Performance sample sizes and numerical pass
limits must be set before active qualification; this record supplies no invented samples.

## 7. Validation evidence and non-execution

Fresh checks: PR head/base/three-file diff; protections/review/conversations; exact-head
merge; main status/0-0/hash/tree equality; source/authority cross-reference inspection;
new three-path Markdown-only inventory, required proposal stamps and whitespace check.
These are documentation/Git checks, not new runtime or target qualification.

Inherited: C2B-5 fresh Python run **173 total, 162 passed, 11 platform skips** including
16 shell/API cases and actual localhost HTTP/restart; C2B-4 **620 .NET passed**.
No Python/.NET rerun is represented as fresh in C2B-6: PR #335 and this packet change
Markdown only. No tests, artifacts, manifests, schemas or production code were added.

Not performed: generation/export/promotion, offline producer experiments, SQLite,
new HTTP/browser/load/fault/native tests, Docker, Azure inspection/configuration,
credentials/RBAC/provider calls, implementation, runtime gate edits, deployment or
public activation. No access to the preserved WP07 repository or archive.

## 8. Remaining owner decisions and next authority

The recommendation is resolved into explicit proposed S1-S8/P1-P7 wording, not into
enacted decisions. Owner must choose B1/B0/Worker/SQLite, accept exact exceptions,
resolve trust/ownership/promotion/retention and all operational TBD values, then grant
literal-path bounded implementation/independent qualification authority if desired.
Current policy sources with historical proposed-status ambiguity must be reconciled
explicitly; no proposal merge substitutes for that acceptance.

Declining/revoking this packet leaves existing unavailable behavior unchanged. Any later
documentation rollback is a reviewed revert. Proposed data revocation means unavailable
until operator-approved whole-release recovery, never request regeneration/private fallback.

**C2B-6 planning PR stays unmerged under this authority. Policy approval and implementation
remain pending. Public activation and Azure deployment remain NO-GO.**
