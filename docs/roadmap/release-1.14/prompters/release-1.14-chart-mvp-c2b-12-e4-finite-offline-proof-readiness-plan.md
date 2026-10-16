# C2B-12 — E4 finite offline proof readiness plan

Date: 2026-10-16. **Planning only; no executable proof authorized.**
`PUBLIC_RUNTIME_VERIFIED=False`. Dataset/candles/indicators remain safely unavailable;
activation and Azure deployment **NO-GO**; advanced WP07 **ON HOLD**.
Public identity: **BTC-like Market Simulation — Synthetic Historical OHLCV**.

## Purpose and decisions preserved

E4/E8 conditionally approve a direction: the existing Infrastructure.Tests friend assembly
and an independently authored test-only CPython integer/rational oracle, initially entirely
in memory. They do not approve test creation/execution, qualified parity, export or
resource ceilings. E1-E8 retained conditions, P1-P7/S/B1 and T TBD remain unchanged.
See the [C2B-11 register](release-1.14-chart-mvp-c2b-11-engineering-owner-decision-register.md),
[C2B-10 options](release-1.14-chart-mvp-c2b-10-engineering-options-and-owner-decisions.md)
and [coverage/resource worksheet](release-1.14-chart-mvp-c2b-12-e4-proof-coverage-and-resource-worksheet.md).

Recommend a future exact two-file proof, not the broader C2B-10 exporter/reader proposal:

1. `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs`
2. `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`

Neither file is created now. No additional necessary production/project/package path was
identified by static inspection: generator friendship already exists; the test project
copies `PythonIntegrationFixtures\*.py`. If implementation exposes a missing test seam,
runner need or fixture-copy conflict, STOP and request its exact literal path separately.
Application arithmetic internals are not exposed to Infrastructure.Tests; use public
calculator results to compare externally observable exact behavior. Do not change friendship
or use reflection to test internal rounding helpers. Existing Application tests are
read-only evidence, not new permission to edit or run them.

## Inspected interfaces and proposed use

Paths below are read-only references, never mutation permissions:

| Source | Interface observed | Future proof use |
| --- | --- | --- |
| `src/AIQuantTradingResearch.Infrastructure/Properties/AssemblyInfo.cs` | `InternalsVisibleTo("AIQuantTradingResearch.Infrastructure.Tests")` | Access generator's internal complete dataset/serialization methods in existing assembly |
| `src/AIQuantTradingResearch.Infrastructure/MarketData/Synthetic/SyntheticHistoricalMarketDataProvider.cs` | `GenerateCompleteDataset(GenerationBudget)` returns `CompleteDataset`; `For(HistoricalCandleInterval)` returns `IReadOnlyList<HistoricalCandle>` | Independent full three-dataset comparison; no public request/runtime composition |
| Same generator | `GenerationBudget(TimeProvider, TimeSpan, CancellationToken)`; `SelectCalculationWindow(HistoricalMarketDataRequest, IReadOnlyList<HistoricalCandle>)` | Bounded future generation and exact twelve calculation windows |
| Same generator | `SerializeDataset(interval, rows, budget)`, `SerializeWindow(request, calculation, budget)`, `SerializeVisibleRows(request, calculation, budget)` return `byte[]` | Full canonical bytes versus independently derived bytes; no file export |
| Same generator | `DatasetFingerprints`, `WindowFingerprints`, `VisibleRowFingerprints` | Compare existing frozen anchors, never replace them with observed production hashes |
| `src/AIQuantTradingResearch.Application/MarketData/TechnicalIndicatorCalculator.cs` | `CalculatePublic(request, candles, provenance, CancellationToken = default)` and `Calculate(candles, identity, provenance, CancellationToken = default)` return `TechnicalIndicatorResult` | Full calculation-window exact aligned results and visible projection; invalid identity/grid cases through public entry points |
| `src/AIQuantTradingResearch.Application/MarketData/TechnicalIndicatorContracts.cs` | Immutable `TechnicalIndicatorInputIdentity`, `TechnicalIndicatorParameters.Fixed`, `TechnicalIndicatorResult.Rows` and nullable `TechnicalIndicatorRow` fields | Compare parameters, identity, each nullable value and timestamp; no new runtime schema |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticHistoricalMarketDataProviderTests.cs` | Existing independent embedded `ReferenceScript`, `RunReferenceAsync()`, `ReadBoundedAsync(...)`, dataset byte expectations and `Windows` table | Historical generator/reference design evidence, not a fresh B1 analytics oracle or permission to run existing tests |

The existing reference runner redirects stdout/stderr, uses `-I -B`, a 20-second timeout,
4 MiB stdout and 16 KiB stderr character bounds, and tree-kill cleanup. These are inspected
implementation choices, not new proof safety ceilings, byte limits or verified crash-safe
containment. Its final wait is not itself a bounded kill-grace guarantee. Do not inherit
those choices as accepted protection for this proof.

## Independence before comparison

The oracle author derives generator counters, aggregation, canonical serialization and
indicator recurrences directly from the frozen Release 1.14 specification, records source
sections and derivations, and uses standard-library exact integers/rationals. No production
code imports, production-output input, copied implementation, floating tolerance or copied
goldens. The embedded prior reference may corroborate established candle anchors but cannot
substitute for independently reviewed complete B1 indicator derivation.

Before any production comparison, an independent reviewer must inspect the oracle's
authorship history/spec mapping, signed ties, rational recurrence quantization, variance
midpoints, hand-derived edge expectations and null/seed semantics. Record reviewed oracle
revision and reviewer identity in sanitized evidence. No independent reviewer or successful
review is claimed now. If independence cannot be demonstrated, STOP before comparison.

Run the independent oracle without receiving production-generated bytes. The C# side
compares separately produced rows/bytes and immutable anchors only after both are ready.
Never update frozen expectations after a mismatch merely to match the implementation.
Proposed derived comparison representation is test-only, not an accepted release/API schema:
fixed field order, UTF-8/LF, exact signed scaled-integer decimal strings and explicit nulls,
selection identity plus calculation/visible row identity. Review this representation before
execution; no derived hashes are invented here. Future in-memory hashes of both independent
streams can be compared, but are not release pins or accepted golden artifacts.

## Transport and permitted future evidence proposal

Prefer one C# test-host coordinator and one oracle child, sequential framed stdin/stdout,
with no candle/indicator files. Frames carry selection selectors and independent results,
not production rows into the oracle. Each frame and aggregate stream require byte bounds,
strict field/type/count validation and cancellation before allocating. Drain stdout/stderr
concurrently within separate finite bounds; excess output is a hard failure, not truncation
with success. No shell command interpolation, arbitrary executable argument or network lookup.

Future executable/interpreter paths and .NET SDK/runtime must be pinned and inspected before
authority. `-I -B` avoids Python repository imports/bytecode; it is not filesystem/network
sandboxing. No pip/NuGet install, implicit restore, network, provider/credential use or new
dependency. Building needed test code and its incidental obj/bin outputs would require
explicit future authority and a reviewed external-output mapping; `--no-restore` alone
does not prevent build or qualify incidental outputs. No executable command is supplied
as ready to run because the bounded runner and output mapping remain unresolved.

Proposed exact external evidence root, not created:
`C:\Users\sabsf\AppData\Local\Temp\aiq-c2b12-e4-proof-candidate`.
Only future approved sanitized files directly under it: `proof-summary.json`,
`comparison-summary.json`, `runner.log`. Summaries contain revisions, case IDs, counts,
existing qualification identifiers, exact equality/failure categories and resource observations;
never row dumps, base64 datasets, raw indicator vectors, secrets or private paths/content.
Mismatch evidence uses selection/row/field index and safe category; no persistent market-data
fixtures or snapshot output. Exact build intermediates/output paths are **TBD and not
authorized**; their necessity must be resolved in the later literal-output request.

## STOP, cleanup and containment prerequisites

Hard STOP on inconsistent frozen anchors/specs, any numerical/byte/null/identity mismatch,
extra/missing result, unreviewed oracle, exceeded/unknown resource ceiling, unexpected process,
file or network output, runaway child, cancellation/kill uncertainty or ambiguous cleanup.
Do not silently retry, weaken assertions, reseed, approximate, skip or replace expectations.
An unsafe environment is a blocked proof, not a parity failure to fix by changing contracts.

Before future execution, validate that the resolved absolute output root is absent/empty
and outside source/protected/private trees; record exact owned created files. Cleanup only
those verified files within approved bounds; no unverified recursive delete, broad temp wipe,
counter reset or unrelated work deletion. Preserve bounded sanitized failure evidence for
owner review. If a descendant cannot be independently confirmed terminated, stop all further
runs and require owner recovery; do not claim successful cleanup from a kill request alone.
Windows PowerShell 5.1 has no assumed built-in hard descendant CPU/RSS boundary: the
runaway/process ownership and bounded termination mechanism is a prerequisite, not proven
by polling or `Process.Kill`. Any harness/native/job-object work requires separate exact scope.

## Stage separation and next owner decisions

| Stage | Required distinct acceptance/authority | Does not authorize |
| --- | --- | --- |
| Plan review | Approve/revise/defer this E4 matrix, two candidate files and outstanding worksheet | Test creation, execution, measurements or accepted budgets |
| Narrow local E4 authority | Exact source/output paths, independently reviewed oracle, toolchain, finite safety ceilings, owned process boundary, abort/cleanup/build/no-network scope | Exporter, persistent rows, runtime wiring or Azure |
| Evidence acceptance | Independent complete byte/value/negative review; resource and preservation evidence | Public readiness or release artifacts |
| Other engineering | E1/E2/E3/E5-E8 actual trust, ownership, accounting, schemas, lifecycle and isolation qualification | Waiver from E4 success; other proof obligations remain |
| Materialization/export | Separate operator-only scope and qualified complete immutable release | Integration, target tests, deployment or activation |
| Integration and target/browser | Separate literal code and bounded local/target/browser authority | Automatic deployment/public activation |
| Deployment/activation | Separate owner authority after every applicable gate and residual-risk acceptance | Future regeneration, private fallback or held WP07 resumption |

Owner must choose or require further design for resource ceilings/containment, build-output
mapping, exact test transport and oracle independence before executable authority is drafted
as ready. No stage inherits permission from a preceding plan, proof or PR merge.
