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
