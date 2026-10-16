# C2B-10 — Finite proof and qualification plan

Date: 2026-10-16. **PROPOSAL ONLY. Commands below MUST NOT RUN under this authority.**
`PUBLIC_RUNTIME_VERIFIED=False`; implementation, snapshot production/export/promotion,
deployment and public activation remain **NO-GO**. Advanced WP07 remains ON HOLD.

Read with [engineering choices](release-1.14-chart-mvp-c2b-10-engineering-options-and-owner-decisions.md).
Recommendations, candidate paths and examples grant no mutation or execution rights.
Owner P1-P7, S and B1 decisions are preserved; unresolved T values stay TBD.

## Candidate literal paths, separated by future authority

The first recommended proof uses existing Infrastructure.Tests friendship. Production
generator/calculator/contracts remain read-only; no reflection, copied generator or
new public API is necessary for this in-memory proposal. The oracle must be independently
authored from the frozen specification before comparison with production results.

| Future stage | Candidate literal mutation path | Purpose and boundary |
| --- | --- | --- |
| In-memory proof | `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs` | New complete byte/row/window/analytics comparisons in existing friend test assembly |
| In-memory proof | `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py` | New independent integer/rational CPython oracle; no production-output goldens |
| Offline reader contract, separate approval | `python/presentation/synthetic_snapshot_contract.py` | Pure bounded manifest/identity validation; no production wiring |
| Offline reader contract, separate approval | `python/presentation/test_synthetic_snapshot_contract.py` | Unknown/duplicate fields, integrity, identity and bounds negatives |
| Later exporter, unresolved seam | `tools/AIQuantTradingResearch.SyntheticSnapshot.Qualification/AIQuantTradingResearch.SyntheticSnapshot.Qualification.csproj` | Candidate standalone tool only if separately approved; no new dependency assumed |
| Later exporter, unresolved seam | `tools/AIQuantTradingResearch.SyntheticSnapshot.Qualification/Program.cs` | Explicit operator-only export, never public request generation |
| Later exporter, unresolved seam | `src/AIQuantTradingResearch.Infrastructure/Properties/AssemblyInfo.cs` | Possible additional friend assembly, versus separately reviewed API alternative; neither selected |
| Later runtime reader | `python/presentation/synthetic_snapshot_reader.py` | Verify and pin immutable release bytes, no provider/generator fallback |
| Later runtime reader tests | `python/presentation/test_synthetic_snapshot_reader.py` | Integrity, TOCTOU, complete-send and fail-closed cases |
| Later trust anchor | `python/presentation/synthetic_snapshot_release_pin.py` | Reviewed release identity only after independent qualification; no pin created now |
| Later API integration | `python/presentation/public_preview_api.py` | Full-response admission and qualified visible projection |
| Later API tests | `python/presentation/test_public_preview_api.py` | Preserved error semantics plus separately accepted ready contract |
| Later UI integration | `python/presentation/public_preview_app.py` | Qualified version-consistent rendering |
| Later UI tests | `python/presentation/test_public_preview_app.py` | No hidden warmup, mixed release or private fallback |
| Later image qualification | `Dockerfile` | Non-public-writable code, anchor, assets and ancestors; exact patch separately reviewed |
| Later startup qualification | `container/entrypoint.sh` | Only if necessary under separately reviewed runtime authority |

These are candidate filenames, not claims that they all exist or an accepted release
manifest. Keep the first authorization to its first two paths if the owner chooses that
proposal. Additional project/fixture-copy changes, package needs or access seams trigger
STOP and a revised literal-path proposal. No backend schema path is authorized here.

## Candidate output inventory

Proposed external root: `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b-offline-proof-candidate`.
It has NOT been created or tested. Future authority must verify the resolved root is
absent/empty, outside protected worktrees and private stores, with approved ownership
and resource ceilings. No wildcard output or recursive deletion authority follows.

First in-memory proof may emit only these proposed sanitized reports, after separate
authority: `qualification-report.json`, `qualification.log`, `oracle-comparison.json`
directly beneath that root. No candle, snapshot or analytics export in that phase.
Report fields should include trusted revisions, oracle method, exact comparison outcomes,
all twelve selection identities, resource observations, aborts and limitations.

Only a distinct materialization/export authority could permit the following proposed
literal relative members beneath that same verified root:

| Member class | Exact proposed relative paths |
| --- | --- |
| Manifest | `release\manifest.json` |
| Complete candles | `release\candles\1h.canonical`, `release\candles\4h.canonical`, `release\candles\1d.canonical` |
| Hourly visible analytics | `release\analytics\1h\1D.json`, `release\analytics\1h\7D.json`, `release\analytics\1h\30D.json`, `release\analytics\1h\90D.json` |
| Four-hour visible analytics | `release\analytics\4h\1D.json`, `release\analytics\4h\7D.json`, `release\analytics\4h\30D.json`, `release\analytics\4h\90D.json` |
| Daily visible analytics | `release\analytics\1d\1D.json`, `release\analytics\1d\7D.json`, `release\analytics\1d\30D.json`, `release\analytics\1d\90D.json` |

Member names/serialization remain design proposals, not frozen schema. The frozen
canonical candle/window encodings and existing fingerprints must not change. No actual
release hash, pin, key, manifest or dataset bytes are produced by this document.

## Sequential finite stages

Every stage requires a separately approved exact scope, numeric safety ceilings and
abort/cleanup procedure before execution. A planning approval does not authorize tests.
Any mismatch stops progression; never adjust approved constants to fit observed output.

| Stage | Preconditions and example procedure, NOT TO RUN | Expected evidence and independent check | Abort, cleanup and rollback |
| --- | --- | --- | --- |
| 0 — Owner decisions | Approve/revise/defer E1-E8; reconcile exact policy, domain, candidate paths, budgets and proof authority | Decision record distinguishing accepted policy from mechanism and feasibility | Missing decision/ceiling: STOP; safe unavailable remains |
| 1 — In-memory offline proof | Separately authorize first two candidate files and finite execution; review independent oracle before production comparison; example commands below | All three complete datasets (8160/2040/340 rows), exact canonical byte counts/digests; all twelve calculation/visible windows; exact B1 parity on every aligned row | Any byte/hash/rounding/null/provenance discrepancy: STOP, preserve sanitized comparison; no export or golden replacement |
| 2 — Independent review | Different review of spec, oracle authorship, comparisons and bounded runner; no active experiment implied | Trace each expectation to spec, not production; challenge ties, signed values, seeding, gaps, malformed identities, cancellation and repeatability | Insufficient independence or missing negative evidence: reject proof; no runtime effects to roll back |
| 3 — Materialization/export | New authority for exporter seam, exact output members, trust encoding and bounded production | Compare complete members against independently accepted proof; verify inventory, lengths, hashes, unknown-field rejection and whole-release identity | Partial/extra/mixed/corrupt output: quarantine exact owned candidate release, never promote; retain approved reports only |
| 4 — Local integration | New source/test/image authority and accepted ready shapes/limits; no private material in test context | Effective ownership/ancestor negatives, manifest/pin TOCTOU, all twelve projections, no spawn/provider, full-send admission, slow reader/cancel, resources and independent health | Any confidentiality/admission/resource uncertainty: return safe unavailable; do not repair/regenerate/reset accounting |
| 5 — Target/browser qualification | Explicit Azure F1/browser authority with exact target, zero-cost constraints and bounded procedures | Actual UID/mount/FS durability/multiplicity, restart/clock/unknown-state accounting, network/FD/proc/env negatives, cold/warm and browser behavior | Missing capability or unacceptable health/cost impact: stop, operator rollback under that authority, public data unavailable |
| 6 — Owner acceptance and activation | Complete evidence for applicable gates, resolved numerical ceilings and explicit activation/deployment authority | Owner accepts residual risk and release identity; independently verified rollback/revocation procedure | No implied activation from proof, PR merge, image build or accepted documents |

Example commands for proposed stage 1, **NOT TO RUN**, compatible with Windows
PowerShell 5.1; paths/classes are proposals and require future reviewed implementation:

```powershell
# NOT TO RUN: only after separate finite offline proof authority.
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj -c Release --no-restore --filter 'FullyQualifiedName~SyntheticSnapshotQualificationTests'
& '.venv\Scripts\python.exe' -I -B 'tests\AIQuantTradingResearch.Infrastructure.Tests\PythonIntegrationFixtures\synthetic_snapshot_reference.py' --mode verify-in-memory
```

Neither command supplies a resource bound by itself. Future authority must specify the
bounded runner and cancellation/termination evidence before execution. Restore/build,
oracle transport and report interfaces require explicit review; no implicit network access.
Static tests do not prove native descriptor closure, kernel waits, target isolation or
Azure durability. Advanced WP07 mechanisms are not resumed by this sequence.

## Exact independent parity obligations

Verify `aiq-synthetic-ohlcv-v1`, seed 114, UTC coverage
`[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, 1h/4h/1d and 1/7/30/90-day
selections. Compare canonical UTF-8/LF byte counts and all existing dataset/window/visible
fingerprints against independent derivation. Reconstruct all 250 calculation-only warmup
rows for each selection; expose none in public payload. Compare complete OHLCV vectors,
aggregation, overlapping candles, input ordering/gap rejection and identity binding.

Independently derive aiq-ta-v1 SMA20, EMA20, RSI14, MACD12/26/9 and Bollinger20x2
with scale 10^12, exact seeds, ties-to-even and nullable alignment. First valid samples:
20/20/15/26/34/20 respectively (MACD signal and histogram 34). Include signed midpoint
ties, square-root midpoint boundaries, large intermediates and invalid provenance.
Recursively seeded indicators need not match across distinct range windows. No tolerance,
copied calculator output or speculative derived fingerprint is acceptable.

## Resource and response acceptance worksheet

All values below are **TBD until separately measured and owner-approved**. Measurement
authority itself needs finite safety ceilings; absence of those ceilings forbids a run.

| Scope | Required values and evidence |
| --- | --- |
| Offline proof/export | CPU, peak RSS, wall time, intermediate allocation, output/member/aggregate bytes, temp/disk/log ceilings, repetitions and abort/shutdown deadline |
| Ready read | Maximum complete response bytes by selection/endpoint; verification/load/projection/serialization CPU/RSS; retained bytes; request/header/body and retry bounds |
| Complete send | Total and per-client active responses, send deadline, slow-reader/cancel cleanup, queue ceiling and admission lifetime through final send |
| Accounting | Atomic reservations, conservative unknown charges, rolling/global/client windows, restart/clock handling, durable retention/capacity and quota-share/headroom predicates |
| Health/UI | Independent health latency/error/headroom criteria, cold/warm startup and browser render/memory deadlines; bounded samples and operator abort |

4096-byte unavailable/error behavior and 2 MiB bridge capture are existing distinct
constraints, not accepted ready-response ceilings. Frozen row counts are not CPU/memory
budgets. Current process-local limits and pre-send slot release are insufficient evidence
for durable complete-read protection. No unbounded retries, counter eviction/reset or
automatic regeneration on denial. No paid resource or privileged facility is presumed.

## G-B0–G-B7 applicability proposal

Controlling requirements remain in the [strategy](../../../architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md).
This is a proposed snapshot evidence mapping, not a waiver or gate passage. Spawn-specific
requirements remain for held Worker paths; a public no-spawn alternative needs owner
acceptance of its exact applicability and proof that no such capability exists.

| Gate | Unchanged requirement | Snapshot applicability and proposed evidence | Owner acceptance condition | STOP outcome |
| --- | --- | --- | --- | --- |
| G-B0 | Exact authority, policy conflicts, numerical limits and admission domain resolved | Record P1-P7/S/B1 versus pending E/T decisions; reconcile authoritative topology and no-spawn mapping | Exact choices and ceilings explicitly accepted, separate implementation authority | No implementation if ambiguous or unresolved |
| G-B1 | Durable occupation before spawn; no overlap after failures/restart; Phase 1B regression for Worker paths | Prove public reader cannot spawn Worker/generator/provider; atomic durable read admission across its full send/domain; retain Worker obligations on held paths | Qualified no-spawn mapping plus crash-safe admission evidence; no blanket waiver | Any unproven reopening/domain: unavailable |
| G-B2 | Private data and credentials inaccessible via paths/mounts/symlinks/FD/proc/env/network | Target negatives include private SQLite/cache/runtime, inherited descriptors, private-derived artifacts, provider egress and writable trust anchor/ancestors | Effective target boundary verified; no private fallback | Any public access or uncertain boundary: NO-GO |
| G-B3 | Atomic reservations, rolling accounting, attributable resources, deadlines/capture, overshoot and overhead | Measure verification/load/projection/serialization/retained allocations/full-send CPU/RSS/cancellation; conservative unknown charges and health protection | Approved finite ceilings and accepted residual C2 availability risk; reservation never called enforcement | Unknown/uncontrolled accounting or resource risk: stop |
| G-B4 | Ordinary/combined failure, parent/supervisor/guardian death, orphan/escape, corrupt ledger/restart/operator recovery | No-spawn proof for public path; interrupted promotion/revocation, reader/control-store crashes, clock rollback and uncertain transaction tests; held lifecycle requirements retained | No unsafe auto-reopening/reset; recovery evidence independently reviewed | Corrupt/unknown state remains closed |
| G-B5 | Frozen fingerprints, twelve selections, warmup/analytics/provenance and private preservation | Exact independent complete oracle, visible-only B1 projection, no mixed release or provider fallback; preserve existing Worker transport | Complete parity and regression evidence, no claimed runtime equivalence from static tests | Mismatch/leakage/contract regression: stop |
| G-B6 | Authorized F1 cold/warm/health/memory/startup/restoration/cost qualification | Actual image ownership, UID/mount/durability/topology, health/availability and immutable rollback; Azure/browser need separate authority | Target evidence and permanent zero incremental cost accepted | Unsupported/unverified target or unacceptable impact: NO-GO |
| G-B7 | Bounded flood/rotation/tabs/cache-hit/invalid-input tests, durable budgets/restart/corruption and health headroom | All ready reads charged through complete send; invalid/denied requests cannot spawn; no queue/retry amplification; C2 local policy does not invent fresh Azure telemetry | Durable global protection, health breaker and accepted headroom/retention thresholds proven | Bypass/starvation/amplification or missing durability: unavailable |

## Cleanup, failure and authority boundary

Future cleanup must inventory only exact newly created owned outputs, verify resolved
absolute paths beneath the approved external root, and preserve unrelated work, private
stores, accepted evidence and durable unresolved charges. No recursive removal based on
unchecked computed paths. Interrupted runs retain sanitized failure evidence for owner
review within approved disk/log/retention ceilings. Never publish partial output, silently
select an older member, repair from private observations or clear accounting to pass.

UNRESOLVED: target immutable ownership, durable backend/domain, zero-cost topology,
complete independent parity, full-send/resource budgets, quota policy predicates and
target/browser proof. Each requires the corresponding narrow future authority above.
Until then safe unavailable behavior and all production/public NO-GO gates remain.
