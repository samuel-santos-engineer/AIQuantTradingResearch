# C2B-13 — Local proof resource/decision worksheet (C2B-14 revision)

Date: 2026-10-16. **Every numerical safeguard is candidate / not measured / not approved.**
Owner-directed simplification supersedes the earlier native-bound table and 660-second
session proposal. No test creation/run, build, restore, generation or export authority.
`PUBLIC_RUNTIME_VERIFIED=False`; public data unavailable; activation/deployment NO-GO;
advanced WP07 ON HOLD. Release T/REST/F1 production acceptance budgets remain TBD.

## Modest local safeguards and advisory observations

| Dimension | Candidate, unmeasured and unapproved | Rationale / future action |
| --- | --- | --- |
| Build elapsed | 120 seconds | Provisional supervised stop point, not a prediction; missing offline assets STOP, no restore |
| Test batch elapsed | 120 seconds total | Finite complete comparisons/repeat/negative batch; operator watches outer command |
| Oracle subprocess | 30 seconds per invocation, at most two sequential invocations | One initial independent result plus deterministic repeat; timeout cancels, no automatic retry |
| Total elapsed | 300 seconds including 60-second cleanup allowance | Simpler finite operator-supervised envelope than 660 seconds; no independent watchdog or hard OS claim |
| Concurrency | One session/test batch and one oracle child at a time | No parallel selection batches or automatic retries; ordinary build/test helpers may exist |
| Repetitions | Two complete positive passes; each supplemental case once | Complete exact proof plus deterministic repeat, not statistical performance qualification |
| Oracle stdout | 16 MiB/invocation, 32 MiB/session | Byte-counted before unbounded capture, no disk spill; complete-result size unmeasured, revise before run if insufficient |
| Oracle stderr | 16 KiB/invocation, 32 KiB/session | Concurrent drain; overflow fails, sanitized diagnostic only persisted |
| Logs/evidence | Two summaries up to64 KiB each, runner.log up to256 KiB; aggregate384 KiB | No raw rows/vectors or secret exception dumps; bound outer build/test logging too |
| Build/temp/disk | Candidate1 GiB aggregate under reviewed owned output pattern | Provisional operator-observed stop, not a disk quota; actual pattern and expected build needs resolved in later authority |
| Cancellation/final wait | Cooperative2 seconds then best-effort tree kill and finite5-second wait | No unbounded final wait; failed/uncertain termination blocks more runs until manual inspection |
| Cleanup/retention | Cleanup allowance60 seconds; bounded failure evidence retained up to7 days for disposition | Exact owned files/pattern only; no automatic deletion of ambiguous state or unrelated files |
| CPU | Observe elapsed process CPU where available and record its coverage | Advisory, no hard aggregate CPU ceiling or exhaustive descendant reconciliation required; report missing coverage |
| Memory | Observe working set/commit where available and record sample method | Advisory; peaks/descendants can be missed, no hard RSS/commit cap; operator may abort resource pressure |
| Process inventory | Record known build/testhost/oracle and related helpers where visible | No exhaustive kernel inventory prerequisite; unexpected/surviving process needs operator inspection |

Oracle deadlines fit inside test-batch/total deadlines, not additive extensions. Timeouts,
RSS sampling and process-tree kill are best-effort, not OS isolation; descendants may
survive. A missed deadline, output overrun, unexpected write/network or failed cancellation
is STOP. No further run after uncertain termination until manually inspected. Adjusting
candidate values requires owner review, not an unbounded retry loop.

## Supplemental correctness cases: simpler organization, unchanged coverage

Recommend 18 named groups instead of 40 artificial case labels. Each group contains the
finite concrete variants listed here; all variants run once, with separately visible
assertions. Grouping does not remove negatives, arithmetic edges or frozen full comparisons.
No native fault-injection tests or special containment harness is required. Exact literals
and expected validator failures are reviewed in the later test-file creation plan; a helper
exception is not evidence of intended rejection.

| Group | Explicit finite variants |
| --- | --- |
| G01 | Constant, rising, falling, alternating and mixed close sequences |
| G02 | Positive even/odd and negative even/odd signed midpoint ties |
| G03 | Square-root below/at/above midpoint, including even/odd tie outcomes |
| G04 | Large intermediates/max valid close; minimum valid precision |
| G05 | Empty input, null row, zero close, negative close, close above10^9 |
| G06 | Excess precision and >2410 inputs |
| G07 | Gap, duplicate timestamp and reversed order |
| G08 | Non-UTC and off-grid time; calculation-start and row-count mismatch |
| G09 | Invalid interval, symbol and range |
| G10 | Stale and nonclosed provenance |
| G11 | Wrong source and context; generator and seed; units, coverage and instrument |
| G12 | Dataset and window fingerprint mismatch; synthetic/private mixture; non-null candle CalculationVersion |
| G13 | Oversize stdout and stderr; aggregate-byte overrun |
| G14 | Malformed/truncated UTF-8/JSON; duplicate/unknown key; trailing content |
| G15 | Missing/extra selection, wrong order/count/identity in result; missing/null field mismatch |
| G16 | Pre-cancel and bounded in-progress cancellation |
| G17 | Oracle nonzero exit and subprocess timeout |
| G18 | Ordinary cleanup failure/uncertain termination result blocks further runs; review/manual inspection path, no native fault injection |

Positive comparisons remain EVERY byte/OHLCV row of all three datasets and EVERY aligned
indicator/null/identity in all twelve calculation/visible selections, including250 hidden
warmup rows. First-valid SMA/EMA/Bollinger20, RSI15, MACD26, signal/histogram34 unchanged.
Full frozen canonical dataset/window/visible hashes and exact recurrence/rounding semantics
remain mandatory. Overlapping candles must match; recursively seeded indicators need not
match between distinct windows. No float tolerance, copied-output goldens, skipped cases,
truncation or provider/private fallback. No persistent datasets or raw expected fixtures.

## Actual prerequisites and distinct owner gates

| Step | Required decision / evidence | Boundary |
| --- | --- | --- |
| Review this revision | Approve/revise candidate ordinary safeguards, output caps and finite coverage | No merge or executable authority implied |
| Future creation plan | Separately authorize exact two candidate test files; define literals, assertions, oracle derivation and any genuinely necessary extra path | No runner/native helper by implication |
| Future local run plan | Resolve exact installed dotnet/CPython paths/versions, SDK/build/test commands/filter, offline cache/no-restore constraints, reviewed minimal env and safe external or existing output pattern | No exhaustive generated-file inventory required, but no unreviewed writes/network/private inputs |
| Supervised execution | Separate explicit authority accepts timeouts/caps, operator presence, advisory observations, abort/finite wait/manual inspection and non-destructive cleanup | No hard CPU/RSS/descendant guarantee; uncertain termination denies repeats |
| Independent acceptance | Identify independent reviewer; review specification-derived oracle and complete exact comparisons/negatives before claiming parity | No named reviewer needed for documentation planning; no evidence accepted now |
| Later stages | Separate E1/E2/E3/E5-E8 qualification, materialization/export, integration, Azure/browser, deployment and activation authorities | Local proof cannot establish production isolation/durability/F1 readiness or resume WP07 |

No command is authorized now. The two future candidate test paths remain
`tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs`
and `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`.
See [local proof design](release-1.14-chart-mvp-c2b-13-e4-bounded-execution-contract-design.md).
