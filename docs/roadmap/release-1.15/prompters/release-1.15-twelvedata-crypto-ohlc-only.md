# Twelve Data crypto OHLC-only fix and live API diagnosis

Date: 2026-11-08. Baseline main `0fcb75560e3841674316f1c666066e494550857a`. Owner authorized Twelve Data diagnostic queries up to50, then explicitly approved: **Approve explicit OHLC-only crypto mode**. This amends mandatory volume for that opt-in chart mode; Twelve Data remains the sole provider and exact instruments remain BTC/USD and ETH/USD.

## What the API actually returned

Twelve bounded diagnostic queries, not50 repeated calls. Private Authorization header, TLS verification, redirects/proxies disabled,10-second timeout,1MiB bound,3 sampled candles per minute query,10-second pacing. No prices, raw bodies, provider messages, key values/fingerprints or historical bundles disclosed/persisted. Key came from the already authorized exact Windows User setting, held only in memory; no key in URLs/arguments/files. Charges not inferred from counts.

| Queries | Request | Observed result |
| --- | --- | --- |
| 1-2 | time_series, BTC/USD and ETH/USD,1min,outputsize3,UTC,ASC | HTTP200/ok,3 rows each, volume absent in all6 sampled rows; symbols match. |
| 3-6 | Same pairs with exchange=Kraken or exchange=Coinbase | HTTP404 for each; no valid candles/volume inference. |
| 7-8 | cryptocurrencies catalog, exact symbol,outputsize1 | HTTP200, exact match. BTC venues Binance/Bitfinex/Coinbase Pro; ETH Binance/Bitfinex/Huobi. Names come from Twelve Data, not direct exchange integration. |
| 9-12 | Minute candles for both exact USD pairs at common listed Bitfinex and Binance filters | HTTP200/ok,3 rows each, volume absent in all12 sampled rows; symbols match. |

Thus6 successful candle responses/18 sampled rows omitted volume. Four404 filter responses and2 catalogs complete the12-call count. No subscription/account/credit request, provider switch or direct exchange call. These results establish the sampled responses, not universal absence on every Twelve Data product/date/plan. Repeating identical queries to50 would not add a field; after the owner chose the explicit OHLC fallback, no more calls were made in this implementation phase.

Official [crypto page](https://twelvedata.com/exchanges/digital_currency) describes time-series OHLC and distinguishes volume for non-currency instruments. [REST time series](https://twelvedata.com/docs/market-data/time-series) describes volume for applicable instruments. [Crypto catalog](https://twelvedata.com/docs/asset-catalogs/cryptocurrencies-list) documents available_exchanges. Reviewed2026-11-08. These support a coverage distinction, not a parser conversion fix. No missing volume is zero and no quote-volume conversion is justified.

## Small explicit fix

Existing operator CLI adds **--ohlc-only**. Default remains strict OHLCV, including all six WP50 subtypes; there is no automatic failover. In OHLC-only mode the adapter deliberately does not consume volume, carries null volume/unit and validates the same actual OHLC, UTC, symbol/interval, closed/grace/window/order/size rules. An incidental upstream volume field is ignored in this explicitly price-only mode, rather than being given unverified base units. This is not a numeric-volume acceptance relaxation. No missing candle or synthetic price generated.

Worker's bounded private stdin accepts optional boolean ohlcOnly; invalid mode types/unknown fields still fail. Base-asset-volume confirmation remains mandatory for strict mode, not for a mode that uses no volume. Python's existing operator privately forwards the explicit option. Same BTC-first/ETH-second execution, no per-invocation retry/pagination, two-pair atomic publication and sanitized envelope.

Bundle volume fields are explicit JSON null, with per-symbol volumeAvailable=false evidence; unavailable volume remains null in import, SQLite JSON payload/restart, transformed snapshot and strict Python client. No SQL schema migration or rewrite of old databases. Snapshot retains required field name volume; only value null is added by the owner-approved contract amendment. Old numeric observations remain numeric; genuine reported zero is distinct from null. Updated API and Python consumers must ship together; old consumers may fail closed on null.

Chart hides volume bars/panel when the retained window lacks complete volume and shows **Volume unavailable - Twelve Data OHLC-only**. It retains Plotly candles, selectors, interactions, supplied C# price indicators and RSI/MACD panels; no indicator recomputation in Python/JavaScript. No current research indicator consumes volume. Price transformation/disclosure remains unchanged: transformed educational values are not actual quotes.

After a separately approved fresh preparation, the command form is:

```text
python -B eng/docker/release-1.15/wp39_local_history.py --action Acquire --directory <fresh-matching-preparation> --execute-provider-approved --ohlc-only
```

No base-volume claim is needed in this mode. **Do not use the retained WP53 preparation:** source/workflow/Worker identities changed, and the guard intentionally rejects stale preparations before key access. No guard override, manifest editing, rebuild, Docker, deployment or real import/start was performed for this code change. New preparation/resources need their own applicable authority. Existing diagnostic query authorization has used12 of its50 ceiling; no fresh acquisition is claimed by the tests.

## Verification (actual, scoped and honest)

Passing final focused runs:

- Full Application:290 passed, exit0.
- Exact affected Infrastructure classes ResearchLocalHistoryTests, TwelveDataMinuteCandleSourceTests, ResearchStateInitializationTests, SqliteResearchStateStoreTests and ResearchSnapshotEndpointTests:228 passed, exit0, owner context for synthetic certificate import.
- Chart/client:62 passed, exit0.
- Python workflow:39 run,38 passed,1 skipped (Windows unprivileged symlink), exit0.

**619 focused tests run:618 passed,1 skipped.** Includes opt-in adapter acceptance/missing/null/ignored-volume and malformed-price negatives; unchanged strict-volume matrix; atomic synthetic bundle -> initializer -> reopened SQLite null round-trip/authentic-only rejection; null C# transformed API -> real local HTTPS -> strict Python consumer for both symbols with401 candles and price analytics; renderer null-volume/no-bar/SMA/RSI checks. No actual browser or authentic acquisition/import qualification claimed.

Commands used empty local feed/NuGetAudit=false restore, then:

```text
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-restore -c Release --verbosity quiet
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter "FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests|FullyQualifiedName~SqliteResearchStateStoreTests|FullyQualifiedName~ResearchSnapshotEndpointTests" --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
<existing-venv-python> -B -m unittest test_research_chart test_research_snapshot_client -q
```

Chart uses PYTHONPATH=python/presentation. Existing analyzer and bare-mode Streamlit warnings remain. A new chart test initially tried to mutate frozen validated data and then used a string epoch where validator expects an integer; its synthetic setup was corrected. Production validation not weakened to make tests pass.

**Full Infrastructure is not green in this checkout.** An initial overbroad filter matched the repository namespace and ran650 tests (614 pass,36 fail) in the restricted context. Final full owner-context run:656 run,625 pass,31 fail,exit1. Restricted HTTPS PFX-import failure was removed by running tests under owner context (no certificate/security code change). Baseline comparisons at unchanged main also failed: VisualizationPermanentIntegrationTests2 fail/2 pass (missing Debug Worker executable), and WP08LifecycleDemonstrationTests/PythonCapabilityInvokerTests/SyntheticSnapshotQualificationTests27 fail/24 pass. These document existing setup/legacy qualification failures; not every one of the31 full-run failures is individually diagnosed here, and no clean full-suite claim is made. No unrelated lifecycle/interop/E4 fix attempted. Passing focused contracts provide the OHLC-specific proof; remaining full-suite qualification must not be hidden.

## Changed paths

- src/AIQuantTradingResearch.Application/MarketData/ResearchStateContracts.cs
- src/AIQuantTradingResearch.Application/MarketData/ResearchSnapshotTransformer.cs
- src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs
- src/AIQuantTradingResearch.Worker/ResearchLocalHistory.cs
- src/AIQuantTradingResearch.Worker/ResearchStateInitialization.cs
- eng/docker/release-1.15/wp39_local_history.py
- python/presentation/research_snapshot_client.py
- python/presentation/research_chart.py
- python/presentation/test_research_chart.py
- tests/AIQuantTradingResearch.Application.Tests/ResearchSnapshotTransformerTests.cs
- tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataMinuteCandleSourceTests.cs
- tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs
- tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchSnapshotEndpointTests.cs
- tests/runtime/release_1_15/test_wp39_local_history.py
- docs/roadmap/release-1.15/RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md (append owner amendment)
- docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md (append evidence)
- docs/roadmap/release-1.15/prompters/release-1.15-twelvedata-crypto-ohlc-only.md (this report)

All changed text UTF-8/LF/no BOM, git diff --check plus independent CRLF check before review. No dependencies, SQL migration, provider/credentials store, public route, startup/DI, Dockerfile, Azure/registry or deployment change. Private artifacts/owner checkout/untracked key generator preserved. No merge or production activation. `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## PR #442 verification-gate closure - 2026-11-08

### Exact baseline and scope

GitHub main and PR base independently read as `0fcb75560e3841674316f1c666066e494550857a`; initial reviewed PR head `6a1e5df32db5829a26fb6e4a63d8c51973c39cdd`, branch `feat/release-1.15-crypto-ohlc-mode`, OPEN/UNMERGED. Its isolated worktree was clean and the GitHub inventory matched the 17 paths above. No comments/checks were listed at preflight. A new detached baseline worktree at the exact base was used; the owner checkout and retained private preparations were not changed. No fetching, rebasing or baseline guessing was needed.

Continuation changes are test/evidence only: strengthen the existing `ResearchLocalHistoryTests.cs`, `ResearchSnapshotEndpointTests.cs`, `TwelveDataMinuteCandleSourceTests.cs`, `test_research_chart.py`, this runbook and the existing execution-plan entry. No production code change, new path, dependency or schema migration was necessary. Twelve Data remains the sole upstream. Historical venue-filter evidence above is historical only and authorizes no new call or alternate provider.

### Identical full-suite comparison

Windows owner execution context; .NET SDK 10.0.103, .NET/ASP.NET runtime 10.0.3, Python 3.13.15. Both worktrees used their repository root, identical Release/no-restore command and shared pre-existing package cache. The fresh baseline needed `dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source <empty-local-feed> -p:NuGetAudit=false --verbosity quiet`, exit 0; no remote feed or package installation. No credential/environment-channel inspection was performed. No checkout-local `.venv`, Debug build, or E4 interpreter override was added to either checkout. Existing HTTPS tests ran in owner context rather than changing certificate validation.

```text
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --logger "trx;LogFileName=full.trx" --results-directory <owned-output>/<baseline-or-pr> --verbosity quiet
```

Only checkout and result destinations differ. No filter or `--no-build` was used; each invocation built its own current source. Structured TRX results were compared by stable test identifier, not log position or count alone. Local logs contain synthetic fixture diagnostics and are not published; the sanitized matrix below preserves exact identifiers and fixed failure categories without local/private paths or payloads.

| Full Infrastructure run | Total | Pass | Fail | Skip | Exit |
| --- | ---: | ---: | ---: | ---: | ---: |
| Exact baseline | 650 | 621 | 29 | 0 | 1 |
| Original PR head, before added coverage | 656 | 627 | 29 | 0 | 1 |
| Final PR test correction/strengthening | 663 | 634 | 29 | 0 | 1 |

**PR-only failures: 0; baseline-only failures: 0; shared failures: 29.** All 29 failures are matched individually below. The historical 31/36 failures were not reproduced as such in these runs; no claim is made to retrospectively classify unavailable historical failures. The prior count is superseded for this comparison, not forced to match. Neither full suite is green.

Test identifiers below share prefix `AIQuantTradingResearch.Infrastructure.Tests.`. ?Setup? describes the actual stage of failure even where xUnit reports an assertion after a configuration failure.

| Stable test identifier | Baseline | Final PR | Sanitized diagnosis |
| --- | --- | --- | --- |
| `PythonCapabilityInvokerTests.CancellationTerminatesOnlyTheOwnedChildAndPreservesAnUnrelatedPythonProcess` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Configured `.venv` interpreter missing; configuration failure precedes expected protocol/activity outcome. |
| `PythonCapabilityInvokerTests.ConcurrentStdoutAndStderrIsReadWithoutDeadlock` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Configured `.venv` interpreter missing; configuration failure precedes expected protocol/activity outcome. |
| `PythonCapabilityInvokerTests.GovernedEndpointSupportsDeterministicHealthAndEchoRoundTrips` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Configured `.venv` interpreter missing; configuration failure precedes expected protocol/activity outcome. |
| `PythonCapabilityInvokerTests.GovernedInvocationEmitsBoundedInteropActivityWithoutProtocolContamination` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Configured `.venv` interpreter missing; configuration failure precedes expected protocol/activity outcome. |
| `PythonCapabilityInvokerTests.MalformedOrInvalidStdoutFailsClosed(fixture: "empty.py")` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Configured `.venv` interpreter missing; configuration failure precedes expected protocol/activity outcome. |
| `PythonCapabilityInvokerTests.MalformedOrInvalidStdoutFailsClosed(fixture: "invalid_json.py")` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Configured `.venv` interpreter missing; configuration failure precedes expected protocol/activity outcome. |
| `PythonCapabilityInvokerTests.MalformedOrInvalidStdoutFailsClosed(fixture: "multiple_frames.py")` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Configured `.venv` interpreter missing; configuration failure precedes expected protocol/activity outcome. |
| `PythonCapabilityInvokerTests.MalformedOrInvalidStdoutFailsClosed(fixture: "wrong_version.py")` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Configured `.venv` interpreter missing; configuration failure precedes expected protocol/activity outcome. |
| `PythonCapabilityInvokerTests.NonzeroExitWithStderrMapsToDependencyFailure` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Configured `.venv` interpreter missing; configuration failure precedes expected protocol/activity outcome. |
| `PythonCapabilityInvokerTests.TimeoutTerminatesOnlyTheOwnedChildAndLeavesNoOrphan` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Configured `.venv` interpreter missing; configuration failure precedes expected protocol/activity outcome. |
| `PythonCapabilityInvokerTests.UnsupportedContractVersionIsRejectedWithoutDowngrade` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Configured `.venv` interpreter missing; configuration failure precedes expected protocol/activity outcome. |
| `SyntheticSnapshotQualificationTests.CompleteIndependentBytesAndEveryAlignedIndicatorMatchOnTwoPasses` | FAIL | FAIL | Setup: Explicit oracle authority unset. `AIQ_E4_REFERENCE_PYTHON` required; test throws before oracle execution. |
| `VisualizationPermanentIntegrationTests.PiReadyRealReplayPipelinePublishesCanonicalReadyEnvelope` | FAIL | FAIL | Setup: Debug Worker absent. `dotnet run --no-build` selects Debug; only Release artifacts were built. |
| `VisualizationPermanentIntegrationTests.PiWarmUpRealReplayPipelinePublishesCanonicalWarmUpEnvelope` | FAIL | FAIL | Setup: Debug Worker absent. `dotnet run --no-build` selects Debug; only Release artifacts were built. |
| `WP08LifecycleDemonstrationTests.DiagnosticAWorkerOnly` | FAIL | FAIL | Setup: Debug Worker absent. `WindowsIsolatedProcessGroup.Launch` rejects missing Debug Worker executable. |
| `WP08LifecycleDemonstrationTests.DiagnosticBWorkerAndStreamlit` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Legacy probe/Streamlit process creation fails with missing interpreter (Win32 setup failure). |
| `WP08LifecycleDemonstrationTests.DiagnosticCWorkerAndProbe` | FAIL | FAIL | Setup: Debug Worker absent. `WindowsIsolatedProcessGroup.Launch` rejects missing Debug Worker executable. |
| `WP08LifecycleDemonstrationTests.DiagnosticDWorkerStreamlitAndProbe` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Legacy probe/Streamlit process creation fails with missing interpreter (Win32 setup failure). |
| `WP08LifecycleDemonstrationTests.RestartR0FreshSingleWorker` | FAIL | FAIL | Setup: Debug Worker absent. `WindowsIsolatedProcessGroup.Launch` rejects missing Debug Worker executable. |
| `WP08LifecycleDemonstrationTests.RestartR1FFreshRuntimeSplit` | FAIL | FAIL | Setup: Debug Worker absent. `WindowsIsolatedProcessGroup.Launch` rejects missing Debug Worker executable. |
| `WP08LifecycleDemonstrationTests.RestartR1SecondWorkerOnly` | FAIL | FAIL | Setup: Debug Worker absent. `WindowsIsolatedProcessGroup.Launch` rejects missing Debug Worker executable. |
| `WP08LifecycleDemonstrationTests.RestartR2SecondWorkerWithStreamlit` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Legacy probe/Streamlit process creation fails with missing interpreter (Win32 setup failure). |
| `WP08LifecycleDemonstrationTests.RestartR3SecondWorkerWithProbe` | FAIL | FAIL | Setup: Debug Worker absent. `WindowsIsolatedProcessGroup.Launch` rejects missing Debug Worker executable. |
| `WP08LifecycleDemonstrationTests.RestartR4SecondWorkerFullTopology` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Legacy probe/Streamlit process creation fails with missing interpreter (Win32 setup failure). |
| `WP08LifecycleDemonstrationTests.RestartRFDatabaseReuseSplit` | FAIL | FAIL | Setup: Debug Worker absent. `WindowsIsolatedProcessGroup.Launch` rejects missing Debug Worker executable. |
| `WP08LifecycleDemonstrationTests.RestartRFHandoffReuseSplit` | FAIL | FAIL | Setup: Debug Worker absent. `WindowsIsolatedProcessGroup.Launch` rejects missing Debug Worker executable. |
| `WP08LifecycleDemonstrationTests.RestartRFRuntimeResidueSplit` | FAIL | FAIL | Setup: Debug Worker absent. `WindowsIsolatedProcessGroup.Launch` rejects missing Debug Worker executable. |
| `WP08LifecycleDemonstrationTests.WindowsFiniteDemonstrationProvesStreamlitProbeRestartAndOwnedResidue` | FAIL | FAIL | Setup: Checkout-local interpreter absent. Legacy probe/Streamlit process creation fails with missing interpreter (Win32 setup failure). |
| `WP08LifecycleDemonstrationTests.WindowsWorkerPublishesP1ThenNewerP2BeforeGracefulTargetedCtrlBreak` | FAIL | FAIL | Setup: Debug Worker absent. `WindowsIsolatedProcessGroup.Launch` rejects missing Debug Worker executable. |

Root-cause cross-check: legacy WP08 `WorkerExecutable()` hardcodes `bin/Debug/net10.0`, and visualization invokes `dotnet run --no-build` without a Release argument. Python interop configuration uses checkout-local `.venv`; the isolated checkouts have no such interpreter. E4 `RunOracleAsync()` explicitly requires a separately approved absolute `AIQ_E4_REFERENCE_PYTHON` and rejects an unset value. These are reproducible harness prerequisites, not nullable-volume failures. Category counts: Checkout-local interpreter absent: 16, Debug Worker absent: 12, Explicit oracle authority unset: 1.

Minimal follow-up, kept open rather than silently broadening execution: qualify the existing interpreter for each disposable checkout, provide Debug Worker artifacts by a cache-only Debug build (or separately review Release-aware harness selection), and obtain the exact E4 oracle run authority before setting its selector. Then rerun the same full suite on both commits. No junction, package install, harness rewrite, oracle override or unrelated production fix was made here. Missing prerequisites do not become waived passing gates.

### Null/numeric compatibility and fail-closed proof

- The existing two-symbol adapter/atomic-bundle/import/reopened-SQLite test now covers upstream field missing, explicit JSON null, strict numeric zero and strict positive numeric volume. Both instruments preserve exact values and unit presence/absence. Synthetic fixtures are still rejected by authentic-only verification.
- The real local TLS endpoint/strict Python consumer test now covers null, numeric zero and positive numeric values for both instruments, with required `volume` retained, fixed decimal representation for numeric data, and non-null C# SMA after calculation. No synthetic fixture is represented as authentic imported history.
- Plotly tests prove zero/positive volume bars remain visible and exact for both instruments; null hides bars and discloses unavailable volume while preserving candles and supplied C# analytics. Renderer tests are offline figure tests, not browser acceptance.
- Explicit OHLC-only adapter negatives cover malformed prices, missing timestamp and wrong symbol. Existing six strict-volume diagnostic classes, first-failure/no-retry, BTC-first ordering, atomic publication and stale/missing/swapped preparation identity tests continue passing. No manifest edits or identity bypass.
- `CompletedResearchCandle.Volume` remains nullable and `VolumeUnit` absent only for null volume; actual zero retains BTC/ETH units. Strict OHLCV remains the default; explicit price-only mode does not consume incidental volume or infer units. Current price indicator algorithms and transformation labels are unchanged.

### Actual final verification and merge conclusion

| Check | Total | Pass | Fail | Skip | Exit |
| --- | ---: | ---: | ---: | ---: | ---: |
| Full Application | 290 | 290 | 0 | 0 | 0 |
| Full Infrastructure (final PR) | 663 | 634 | 29 | 0 | 1 |
| Python workflow (`test_wp39_local_history.py`) | 39 | 38 | 0 | 1 | 0 |
| Chart/client | 63 | 63 | 0 | 0 | 0 |

Application command: `dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-restore -c Release --logger "trx;LogFileName=application.trx" --results-directory <owned-output>/application --verbosity quiet`, exit 0. Python workflow command remains `python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q`. Chart/client uses the existing installed venv interpreter, `PYTHONPATH=python/presentation`, `-B -m unittest test_research_chart test_research_snapshot_client -q`. No dependencies installed. The single Windows unprivileged symlink skip remains honest; prior Linux 38/38 evidence is recorded in the existing WP53 report, not rerun here. Existing analyzer/bare-mode Streamlit warnings do not imply additional passing tests. UTF-8/LF/no BOM and `git diff --check` are checked before commit.

**Recommendation:** OHLC-only correctness has no unaddressed regression in the matched comparison and is suitable for owner review. Unconditional verification-gate closure/merge readiness is **not established**, because 29 full-suite harness prerequisites remain open. The owner has a precise baseline comparison and minimal follow-up instead of a claimed clean run. PR #442 remains OPEN/UNMERGED. A fresh matching preparation and controlled Twelve Data acquisition remain separate future actions.

No live provider/DNS/account/credit request, credential-presence/value inspection, private historical response read, actual Acquire/Import/Start/Verify workflow, browser operation, Docker/resource operation, Azure/registry/deployment or retained-resource deletion occurred. Tests used local synthetic fixtures and their owned subprocesses only. `liveAttemptReady=false`; `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.
