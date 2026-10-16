# C2B-13 — E4 supervised local proof contract (C2B-14 revision)

Date: 2026-10-16. **Owner-directed documentation revision; no creation or run authority.**
C2B-14 supersedes this proposal's earlier Job Object preference and mandatory native
containment/resource-bound requirements. The original C2B-13 authority remains historical.
`PUBLIC_RUNTIME_VERIFIED=False`; dataset/candles/indicators unavailable; Azure deployment/
public activation NO-GO; advanced WP07 ON HOLD.

## Proportionate local safeguards

Recommend the existing Infrastructure.Tests infrastructure and one test-only CPython
oracle, run sequentially by a developer supervising a finite local batch. No Job Object,
native helper, launch-time association, exhaustive descendant accounting, independent
watchdog or kernel CPU/RSS/commit cap is required for this first local proof proposal.
Do not introduce a complex runner to implement these ordinary safeguards without new authority.

Future test code should use an explicit subprocess timeout/cancellation token, redirected
stdout/stderr drained concurrently with byte caps, exact executable and argument lists,
and no shell interpolation or PATH fallback. On timeout, request cancellation, then
`Kill(entireProcessTree: true)` when supported, followed by a finite wait. Record whether
exit was observed, drain tasks completed and handles closed. The operator supervises the
outer build/test command, stops a hung command and inspects surviving related processes.
No process-name-wide kill or unrelated-process cleanup is permitted.

These are best-effort safeguards, not OS isolation or a hard process-tree guarantee.
Descendants can survive; RSS sampling can miss peaks; cancellation, elapsed deadlines
and kill requests can be delayed or fail. If exit/termination is failed or uncertain,
mark the session blocked, stop further runs and require manual inspection/disposition.
Never report verified cleanup without supporting evidence; no automatic retries.
Advisory CPU/memory observations inform future limits rather than blocking on a missing
kernel-enforced ceiling. Operator concern about resource pressure triggers abort.

## Existing infrastructure and future exact scope

The first proof still proposes only these two test-source paths, neither created now:

- `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`

Existing generator friendship and Python fixture-copy rule support these candidates;
use public calculator interfaces, without reflection/new production APIs. Existing
reference code provides subprocess patterns, not a ready complete B1 oracle. Its
character caps must not be mislabeled byte caps and its unbounded final wait must not
be copied into the future finite cleanup path. No production Python port or exporter.
If an additional script/helper is necessary, STOP and request its exact separate scope;
no runner/harness path is implicitly selected by this revision.

Repository observations remain: global.json requests SDK10.0.103 with latestFeature
roll-forward/prereleases; net10.0; Microsoft.NET.Test.Sdk17.14.1, xunit2.9.3 and adapter3.1.4
are declarations, not installed executable identities. Optional local build overrides
and signing targets require reviewed build inputs and no credential/signing action.
Future authority must resolve exact installed .NET and CPython executable paths/versions,
SDK selection, test filter, build configuration and offline cache availability. No PATH
fallback or assumed executable identity. Windows scripts must support PowerShell
5.1.26100.9444; use explicit arguments, no shell-built command strings.

`--no-restore` does not prohibit builds or incidental writes. Future creation/run plan
must explicitly allow the required build phase and then a bounded no-build/no-restore
test phase, with safe output patterns and no package installation/download or provider
calls. Use offline/no-network intent: no restore, pip, external requests or unreviewed
private inputs; minimal reviewed environment and no secret dumps. Python `-I -B` avoids
repository imports/bytecode but is not a filesystem/network sandbox. This local proof
need not establish production egress isolation or add firewall/cloud/native controls.
Unexpected network/package activity is STOP, not permission to continue or modify config.

## Controlled outputs and non-destructive cleanup

Candidate external root, not created/verified/authorized:
`C:\Users\sabsf\AppData\Local\Temp\aiq-c2b12-e4-proof-candidate`.
Proposed sanitized evidence: `proof-summary.json`, `comparison-summary.json`, `runner.log`.
A later creation/run authority should choose either this controlled external build/temp
root (reviewed per-project obj/bin layout) or a reviewed safe existing local test-output
pattern. Do not demand exhaustive generated-file enumeration before ordinary development;
review bounded directories/patterns, ownership, sizes and what the build legitimately creates.
No implicit new repository paths or broad cleanup rights follow from a pattern proposal.

Before execution, verify root absence/emptiness or approved existing contents, absolute
resolved paths, ownership and no overlap with protected/private material. Inventory actual
owned outputs afterwards; abort on unexpected locations, excessive bytes or data dumps.
Keep logs/summaries to revisions, case IDs, counts and sanitized equality/failure categories;
no candle/indicator rows, raw vectors, base64 datasets, snapshots, private inputs or secrets.
Cleanup only verified owned outputs under the accepted root/pattern after process inspection;
no unchecked recursive deletion, overwriting unrelated files, or global temp/cache cleanup.
Retain bounded sanitized failure evidence for owner review. Ambiguous ownership/termination
blocks cleanup/repeat pending manual inspection.

## Small bounded transport and independent comparisons

Prefer direct bounded JSON stdout from one oracle invocation, parsed in memory only after
successful exit. No custom binary/chunk protocol required initially. Stream byte-counting
caps both stdout and stderr before unbounded accumulation; drain concurrently and cancel
on overrun. Strict UTF-8, JSON types/keys/counts, explicit nulls, exact decimal/scaled-integer
strings; reject malformed/extra/duplicate fields, missing selections and trailing content.
No float tolerance, compression, spill files or persistent expected-vector fixtures.
If a complete independent result cannot fit the reviewed cap, STOP and revise the transport
proposal separately; never truncate comparison or expand limits during a run.

Oracle receives frozen selectors/specification inputs, never production-generated rows or
expected results. Derive all three datasets, aggregation, canonical streams and indicators
independently from the frozen specification, with standard-library integer/rational arithmetic.
No production imports, copied implementations/output goldens or changes to frozen constants.
Review oracle derivation/serialization before claiming parity; retain authorship/spec mapping
and exact revision. Identify an independent numerical/spec reviewer before evidence acceptance;
missing assignment does not block documentation planning. No reviewer is assigned or parity
accepted now. Self-consistent cross-runtime output alone is insufficient independent evidence.

Retain complete datasets8160/2040/340; all twelve calculation/visible windows; full canonical
bytes and existing dataset/window/visible fingerprints; all aligned indicator values/nulls/
identity; 250 hidden warmup. Exact SMA20, SMA-seeded EMA20, Wilder RSI14, MACD12/26/9,
population Bollinger20×2, scale10^12, symmetric ties-to-even and sqrt midpoint comparison
`4A` against `400(2k+1)^2`. No warmup publication or production Python port.
Frozen generator aiq-synthetic-ohlcv-v1, seed114, UTC
`[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, intervals1h/4h/1d and ranges1/7/30/90 days,
label **BTC-like Market Simulation — Synthetic Historical OHLCV** remain unchanged.
See the unchanged [C2B-12 coverage inventory](release-1.14-chart-mvp-c2b-12-e4-proof-coverage-and-resource-worksheet.md)
and revised [worksheet](release-1.14-chart-mvp-c2b-13-e4-resource-and-owner-decision-worksheet.md).
Later explicit C2B-14 authority changes the local safeguards proposal only; historical
C2B-12 prerequisites are not silently edited into current instructions.
