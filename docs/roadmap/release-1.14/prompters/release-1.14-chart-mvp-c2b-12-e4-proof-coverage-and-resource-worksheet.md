# C2B-12 — E4 coverage and finite-resource worksheet

Date: 2026-10-16. **Proposal; no tests/generator/oracle execution authorized or performed.**
Read the [readiness plan](release-1.14-chart-mvp-c2b-12-e4-finite-offline-proof-readiness-plan.md).
`PUBLIC_RUNTIME_VERIFIED=False`; public data unavailable; deployment/activation NO-GO;
advanced WP07 ON HOLD. T remains TBD, not replaced by proof-run safety proposals.

## Complete dataset and window comparison inventory

Every comparison is exact; no floating tolerance, sample-only substitute or production-output
golden. Existing expected values reside in generator maps and the existing independent
generator test tables. These are source observations, not fresh verification. No new hash
is computed here. Compare every canonical byte and every OHLCV field, not only digests.

| Complete dataset | Rows | Existing canonical byte count, inspected test source | Additional obligations |
| --- | --- | --- | --- |
| 1h | 8160 | 596926 | Entire independent hourly sequence, SHA-256 counter derivation/seed 114, source independence |
| 4h | 2040 | 151407 | Every four-hour aggregation: first open, max high, min low, final close, summed volume |
| 1d | 340 | 25957 | Every UTC day aggregation and exact boundary/coverage continuity |

Preserve `aiq-synthetic-ohlcv-v1`, `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`;
UTF-8 without BOM, LF, frozen field order/header, fixed price/activity scales and terminal
newline. Compare all three `DatasetFingerprints`, all twelve `WindowFingerprints` and
all twelve `VisibleRowFingerprints` against independent derivation and existing anchors.
The following counts/bytes are transcribed from existing `Windows` expectations, not
newly generated evidence. Visible bytes must be independently reconstructed and compared
in full; no invented visible-byte count is needed.

| Interval / fixed range | Calculation rows | Visible rows | Warmup hidden | Existing calculation canonical bytes |
| --- | --- | --- | --- | --- |
| 1h / 1D | 274 | 24 | 250 | 20578 |
| 1h / 7D | 418 | 168 | 250 | 31107 |
| 1h / 30D | 970 | 720 | 250 | 71459 |
| 1h / 90D | 2410 | 2160 | 250 | 176720 |
| 4h / 1D | 256 | 6 | 250 | 19493 |
| 4h / 7D | 292 | 42 | 250 | 22157 |
| 4h / 30D | 430 | 180 | 250 | 32369 |
| 4h / 90D | 790 | 540 | 250 | 59006 |
| 1d / 1D | 251 | 1 | 250 | 19373 |
| 1d / 7D | 257 | 7 | 250 | 19823 |
| 1d / 30D | 280 | 30 | 250 | 21549 |
| 1d / 90D | 340 | 90 | 250 | 26049 |

For each of twelve: exact calculation start/visible start/exclusive end and UTC grid;
all calculation rows including warmup; visible rows exactly calculation[250..]; identity,
canonical/window/visible streams and fingerprints; exact analytics on the full calculation
window followed by visible-only projection. Compare overlapping candle equality and
request/clock/culture independence without assuming recursively seeded analytics equality
between distinct calculation windows. Maximum calculator input is 2410; do not feed an
8160-row dataset into a calculator intended for bounded windows.

## Indicator and negative matrix — every selection, every aligned row

Let x be exact close scaled by 10^12; R denotes symmetric signed rational ties-to-even
to an integer, at each specified recurrence step. Zero-based index is sample number minus
one. Derivations must come independently from the frozen contract, with public C# output
used only as the comparison subject. Input close must be positive, at most 10^9, with
at most 12 decimal places. No float intermediary or platform default rounding assumption.

| Subject | Independently derived expectation | First valid one-based sample / negatives |
| --- | --- | --- |
| SMA20 | R(sum of last 20 x / 20) | 20; preceding 19 null; constant/rising/falling/mixed and signed midpoint cases |
| EMA20 | SMA20 seed at 20; then R((2x + 19 previous)/21) | 20; no lookahead or alternative initial-close seed |
| Wilder RSI14 | First 14 deltas, separately rounded average gains/losses; R((13 previous + current)/14); R(100*scale*gain/(gain+loss)) | 15; flat 50, rising 100, falling 0; no divide-by-zero workaround altering contract |
| MACD12/26 | SMA-seeded EMA12 minus SMA-seeded EMA26, each exact recurrence | 26; EMA12/EMA26 first valid at 12/26; signed MACD allowed |
| MACD signal9 / histogram | Signal seeded by mean of first 9 valid MACD values (samples 26–34), then exact EMA9; histogram = MACD minus signal | 34 for both; null alignment preserved, no premature zero fill |
| Population Bollinger20×2 | A=20 sum(x²)−sum(x)²; sigma=R(sqrt(A)/20); middle=SMA20, lower/upper=middle∓2sigma | 20; lower may be negative, no clamp; compare `4A` with `400(2k+1)^2` at integer sqrt midpoint, parity ties-to-even |
| Complete output | Timestamp, every nullable indicator, fixed aiq-ta-v1 parameters, exact scaled decimal conversion and identity | No normalization of null to zero or hidden warmup leak; compare all nine indicator fields |
| Arithmetic edge derivations | Constant/rising/falling/alternating/mixed closes, signed ties, sqrt boundaries, maximal intermediates, minimum/maximum valid precision | Hand-derived expectations reviewed before production comparison; public-result test seam required if internals inaccessible |
| Malformed input | Missing/null row/identity/provenance, zero/negative/too-large close, precision overflow, invalid interval/symbol/range, >2410 rows, empty list | Expected explicit rejection, never helper exception, skip or accepted truncation |
| Grid | Gap, duplicate, reorder, non-UTC/off-grid time, calculation-start/count mismatch | Reject rather than fill/reorder; exact first failing predicate recorded |
| Provenance | Wrong generator/seed/source/context/instrument/units/coverage/fingerprint, stale/nonclosed data, non-null candle CalculationVersion, synthetic/private mixture | Fail closed through actual validator; no provider/private fixture input |
| Execution behavior | Pre-cancel and bounded in-progress cancellation, deterministic repeats, malicious/malformed oracle frame and unexpected output/process | Finite abort and termination evidence required; no unbounded retries or output dumps |

Derived indicator comparison encoding and byte counts/hashes are not existing frozen
release constants. Future test-only encoding must be independently reviewed, compared
exactly across runtimes, and kept in memory; no hash is promoted to a release qualification
or manifest merely because both implementations agree. Missing independent expected vectors
or a golden disagreement is a STOP, not permission to amend approved arithmetic.

## Safety ceilings versus acceptance targets

These are proposals for a later authority, not measurements, approved limits or performance
claims. Static row/count bounds establish workload size but cannot justify CPU/RSS/time
ceilings for .NET/CPython/BigInteger or the host. Therefore critical numeric ceilings remain
**TBD — owner selection required before execution**, not fabricated from existing timeouts.
No future run may proceed while a mandatory cell is unresolved.

| Dimension | Candidate finite safety proposal | Static rationale / remaining acceptance |
| --- | --- | --- |
| Repetitions | One complete comparison pass, plus one explicit determinism repeat; no retry | Proposed small finite workload, not owner-approved; exact negative/cancellation case count must be enumerated before run |
| Concurrency | One proof batch/selection at a time | Avoid parallel test runner amplification; separately disable/review discovery and parallelism |
| Subprocess count | One oracle child at a time; no child-created descendants | Proposed logical protocol; .NET CLI/testhost/helper process inventory must be pinned; total process-tree ceiling TBD |
| Wall time | TBD per build, oracle, batch, repeat and total session | Prior 20-second oracle timeout is not a justified all-B1 limit |
| CPU time | TBD aggregate for coordinator, testhost, oracle and all descendants | Polling has overshoot; actual attributable accounting and hard abort mechanism unresolved |
| RSS / committed memory | TBD per process and tree, plus polling/abort overshoot | 2410 inputs do not bound runtime/BigInteger/JSON/testhost overhead; no asserted Windows kernel cap |
| Pipe/frame output | TBD frame, total stdout, stderr and buffered bytes | Byte-count before decoding/allocation; existing character caps not reused as byte budgets |
| Logs/evidence/temp/disk | TBD individual and aggregate bytes including obj/bin/build outputs | No raw dataset/indicator fixture; external exact output mapping and bounded retention required |
| Requests/batches | Twelve fixed selections per pass; edge/negative count TBD | Twelve is frozen coverage, not public traffic authority; no public requests in E4 proof |
| Cancellation/kill grace | TBD cooperative deadline, forced termination, final wait and verification | Must prove process ownership and no surviving descendants; unbounded final wait forbidden |
| Cleanup/retention | TBD deadline, exact owned-file bytes/count and failure-evidence retention | Root absence/ownership checks, no protected/private/repo cleanup; unknown child state blocks repeats |
| Ready response / public headroom | TBD under separate E5/E7/target authority | E4 test pipe is not a ready API size limit; 4096 unavailable cap and 2 MiB bridge cap remain distinct |

The Windows host launcher must remain compatible with Windows PowerShell 5.1.26100.9444,
quote exact executable paths/arguments, avoid shell-built commands, prevent implicit
restore/network, account for all build/test processes and enforce finite capture and
termination. No harness script or job-object/native experiment is authorized here.
If those needs cannot fit the two proposed test files and a separately accepted existing
runner, request additional literal harness/output scope first. No unsupported platform
capability is assumed. Numerical run-safety ceilings do not set release T acceptance targets.

## Mandatory evidence before later owner acceptance

Require reviewed oracle provenance/spec derivations; exact dataset/window/visible byte and
hash comparison; complete twelve-window indicator/null/identity comparisons; negative-case
validator rejection evidence; measured total resources and bounded cancellation/cleanup;
source/output inventory and unrelated-file preservation; no network/provider/private data;
and independent review of mismatch-free evidence. Every field remains **NOT RUN / UNPROVEN**
now. Public isolation, storage durability, immutable ownership and Azure F1 feasibility
are outside this local proof and cannot be inferred from a future E4 pass.
