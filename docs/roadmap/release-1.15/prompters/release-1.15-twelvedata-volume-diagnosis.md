# Release 1.15 - Twelve Data-only volume diagnosis and operator runbook

Date: 2026-11-08. Baseline main/#435 `7c8d242105fbc5e3fbed4159b07f94a90a7a271c`; prior PR #436 head verified `9d36bbae61e8d4c35eec6e4ba4b7c17f545490f9`, OPEN/UNMERGED. Work remains isolated from the owner's dirty checkout/private preparations.

## Effective direction and result

**Twelve Data remains the sole Release 1.15 provider, BTC/USD and ETH/USD.** Kraken/Coinbase/archives and provider-agnostic migration are not selected and are out of scope. PR #436 is corrected in place; the accurate comparative research is retained as historical evidence and its migration proposal explicitly withdrawn. No merge or provider change.

Production acceptance and diagnostics are unchanged. Reliable official evidence does not justify accepting numeric JSON volume or making volume optional. Existing version-1 static codes already distinguish JSON, metadata, timestamp, volume encoding and broader observation failures. A new subcode system would change coordinated producer/consumer allowlists without establishing what WP47 actually received; this slice instead documents that limitation and extends fake-only partial-row coverage. No parser, Worker/Python allowlist, schema, provider selection or request-budget changes.

WP47 remains a consumed one-attempt approval: BTC HTTP200, observation_validation / INVALID_VOLUME, started/received1/1, outcomeUnknown=false, bundlePublished=false; ETH not requested. Specific volume defect and credits remain unknown. No private response was inspected.

## Trace and decision matrix

Exact baseline touchpoints:

- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs`: ReadCompletedAsync validates HTTP headers/size/UTF-8/JSON, uniqueness, status and metadata, then row timestamp, volume Value(), candle constructor and sequence; forming bars are filtered only after validation. Fixed request is time_series, symbol BTC/USD or ETH/USD, interval1min, outputsize1000, timezoneUTC, orderASC; no request change here.
- `src/AIQuantTradingResearch.Application/MarketData/ResearchStateContracts.cs`: CompletedResearchCandle constructor enforces authentic observation bounds, Provider=twelvedata, BTC/ETH volume units; ResearchInputValidation validates poll/batch.
- `src/AIQuantTradingResearch.Worker/ResearchLocalHistory.cs`: AcquisitionTrace.Observe maps finite events to fixed stage/code; Fail preserves first failure; FailureJson emits bounded version1 scalars. AcquireAsync validates rows <=1000 and age <=24h, assembles both symbols, then atomically moves a private staging directory. VerifyBundle checks source/endpoint/hash/kind before import. No partial instrument publication.
- `eng/docker/release-1.15/wp39_local_history.py`: CHILD_CODES/CHILD_STAGES and child_failure strictly reconstruct allowed scalars, reject arbitrary/extra/oversized fields, enforce counts <=2, preserve unknown outcome. No forwarding raw stderr/body.
- `src/AIQuantTradingResearch.Worker/ResearchStateInitialization.cs`: authentic bundle import keeps source checks; workflow receipt binds import hash/generation. `python/presentation/research_chart.py` consumes the transformed research API via snapshot client; it does not parse upstream responses, fill missing volume or calculate substitute indicators.

| Synthetic input / condition | Current acceptance and diagnostic | Why / ordering |
| --- | --- | --- |
| volume absent, null, JSON number (including numeric zero), bool, object, array | Reject INVALID_VOLUME / observation_validation | GetProperty/GetString or strict Value fails before candle construction. Optional SDK field does not make product volume optional. |
| string "0" | Accept volume when all other fields valid | Genuine reported zero allowed; absent/null is never synthesized into zero. Fixture success is not authentic acquisition. |
| digit/decimal string, <=40 characters, <=12 fractional digits, decimal-parsable, 0..10^12 | Accept volume when complete candle valid | Invariant decimal path; no float conversion. Exact grammar is the existing Value implementation, not a newly tightened regex. |
| empty/whitespace, signed negative, exponent, +sign, NaN/Infinity, malformed string | Reject INVALID_VOLUME / observation_validation | Lexical characters/length/decimal checks fail. |
| >12 fractional digits, even trailing zeros | Reject INVALID_VOLUME / observation_validation | Encoding precision check precedes decimal constructor; no rounding. |
| decimal overflow or >40-character encoding | Reject INVALID_VOLUME / observation_validation | Value cannot parse within bounded representation. |
| parseable string volume >10^12 | Reject INVALID_OBSERVATION / observation_validation | Constructor throws ArgumentException, handled by outer observation catch, not the narrower volume catch. INVALID_VOLUME is not a universal volume-domain code. |
| valid volume but malformed/missing OHLC string | Reject INVALID_OHLC_ENCODING / observation_validation | Value failure occurs in OHLC block. |
| valid decimal OHLC but inconsistent bounds, unaligned UTC minute or out-of-range volume | Reject INVALID_OBSERVATION / observation_validation | Constructor ArgumentException reaches outer catch; numeric invalidity is not necessarily OHLC encoding failure. |
| raw unquoted NaN / invalid UTF-8 / invalid JSON | Reject INVALID_JSON_ENCODING / response_parse | Never reaches row/volume validation. |
| duplicate JSON properties or non-object root | Reject INVALID_PAYLOAD_SHAPE / response_parse | Earlier structural checks. |
| missing/wrong-type values or non-object row | Reject INVALID_CANDLE_COLLECTION | Collection fails in response_parse; row fails after observation_validation begins. |
| missing/malformed timestamp, even alongside absent volume | Reject INVALID_CANDLE_TIMESTAMP / observation_validation | Timestamp precedes volume; do not diagnose the hidden later field. |
| valid first row followed by missing volume | Reject INVALID_VOLUME / observation_validation | Earlier valid row cannot make the instrument or bundle partially successful. |
| valid first row followed by missing close | Reject INVALID_OHLC_ENCODING / observation_validation | Complete OHLCV required per row. |
| empty values or only valid forming bars | ZERO_ACCEPTED_CANDLES / observation_validation at Worker | Parsing can succeed with no eligible completed observations. |
| forming row with malformed volume | Reject INVALID_VOLUME | Filtering is not a way around row integrity. |
| duplicate/descending/future row | Reject INVALID_OBSERVATION | Sequence validated before completed-bar projection. |

At first-symbol failure: started/received1/1 and BTC; at second-symbol HTTP200 validation failure:2/2 and ETH. outcomeUnknown=false means responses were received, not that nothing was charged. A send without a response preserves unknown outcome; neither absence of bundle nor parser failure proves zero credits. Failure-first diagnostics prevent later cleanup symptoms replacing the primary reason. Current code does **not** reveal which INVALID_VOLUME subtype occurred; no retrospective cause is inferred.

## Authoritative provider evidence (accessed 2026-11-08)

| Official source | Established | Not established |
| --- | --- | --- |
| [Time-series REST documentation](https://twelvedata.com/docs/market-data/time-series) | Generic volume response field is string; example is an equity. Supports minute interval and bounded outputsize; current implementation uses its stricter 1000 bound. | Exact BTC/USD and ETH/USD minute availability, base/quote units, guaranteed nullability, or account/product entitlement. Numeric types on other endpoints do not amend time_series. |
| [Official Java TimeSeriesItem model](https://github.com/twelvedata/twelvedata-java/blob/main/docs/TimeSeriesItem.md) | Volume optional String; OHLC strings. | Optional does not establish null as usable volume or permit OHLC-only authentic import; no exact crypto unit guarantee. |
| [Official MarketDataApi documentation](https://github.com/twelvedata/twelvedata-java/blob/main/docs/MarketDataApi.md) | Time-series volume applies to applicable instruments. | Which exact BTC/ETH minute feed supplies it. |
| [Provider cryptocurrency product page](https://twelvedata.com/cryptocurrency) | Historical crypto OHLC and broad multi-exchange coverage advertised. | Per-request constituents, venue weights, mandatory traded volume or units for selected symbols. |
| [Provider support overview](https://support.twelvedata.com/en/articles/5544954-twelve-data-overview) | Crypto coverage includes exchange feeds and an aggregated feed. | Which feed the current unqualified symbol selects or whether its volume matches its OHLC population. |
| [Composite Currency Feed methodology](https://support.twelvedata.com/en/articles/12528665-how-the-composite-currency-feed-works) | Describes composite midpoints and reliability/liquidity/latency weights, including forex/crypto. | Does not define the selected time_series volume field as BTC/ETH trade quantity. Liquidity input/quote frequency is not interchangeable with returned base volume. |
| [Official Sheets support documentation](https://support.twelvedata.com/en/articles/5702399-google-sheets-add-on-documentation) | Separately describes general OHLCV and currency OHLC functions. | Not proof of universal crypto-volume absence, nor proof of WP47's missing field. Different product descriptions cannot identify the historical defect. |

Owner's earlier base-unit confirmation remains owner-reported, not independently verified here. Public material is insufficient to demonstrate exact feed volume availability/units or subscription entitlement. No account, credential presence, capability, API, HEAD/DNS or credit probe occurred. No support contact or ticket submitted. No licensing or unrelated governance prerequisite added.

## Practical Twelve Data-only next action

Keep the current strict parser and all authentic OHLCV checks. Do not spend another attempt merely to rediscover INVALID_VOLUME. The precise unresolved technical question is: for the existing BTC/USD and ETH/USD time_series requests (1min, UTC, ASC, outputsize1000, no exchange selector), is volume supplied for each row; what is its JSON encoding/nullability; is it BTC/ETH base-asset traded quantity for the same OHLC source population; and does availability depend on a supported feed/plan option?

A prepared but **unsent** technical clarification can use exactly that question, with no key, account identifier or private response. The owner may independently supply authoritative documentation or separately instruct a provider support inquiry. No new provider-switch or licensing gate. If evidence identifies a supported Twelve Data-only correction, implement only that encoding/request correction with fake expectations and unchanged bounded arithmetic, then review it. Do not invent a parameter, silently select an exchange, or build a provider-agnostic abstraction. If evidence shows volume unavailable, report that conflict without downgrading OHLCV or changing provider.

Optional later implementation, if the owner wants more diagnosis before a new attempt: classify missing/null/type/format/precision/range with finite static events inside the existing version1 fields, updating the Twelve Data adapter, Worker Codes/Observe, Python CHILD_CODES and synthetic tests together. This is not implemented here and cannot explain the past response. It should not change accepted values, expose values, or expand request count.

Any later real acquisition still needs fresh explicit **single-attempt operational authorization**, fresh build/manifest/TLS/ACL/destination checks, first-failure stop and no retry. None is requested or granted by this report. Import/start/verify/browser work is not run or preauthorized here.

## Actual changes and executed tests

Production parser, diagnostic envelope/allowlists, contract, importer and chart are unchanged. Test-only correction extends DiagnosticHandler with partial-volume/partial-OHLC rows and three theory cases (each first/second symbol): valid-row prefix followed by missing volume; valid-row prefix followed by missing close; empty values. All assert actual validator rejection, bounded code/stage/HTTP200/counters, no bundle and no canary disclosure. The existing 21-case volume matrix still runs both failure positions; valid-boundary fixtures remain SyntheticFixture and authentic-only verification rejects them.

Commands (existing cached assets; no restore/download):

```text
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter "FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests" --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
<existing-venv-python> -B -m unittest test_research_chart test_research_snapshot_client -q
```

Fresh results: **153 C# +23 workflow +61 chart/client =237 passed**, exit0, no skips. Chart run used PYTHONPATH=python/presentation. Existing analyzer warnings CA1711/SYSLIB1045/CA1861/CA2219 and bare-mode Streamlit ScriptRunContext warnings remain; no unrelated repair. Tests are synthetic/offline; they establish neither current provider availability nor authentic acquisition. UTF-8/LF/no BOM and git diff --check required before commit.

Exact changed paths in corrected PR #436:

1. `docs/roadmap/release-1.15/prompters/release-1.15-wp49-exchange-origin-ohlcv-evaluation.md` - correct direction, retain historical evidence/withdrawn proposal.
2. `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` - correct unmerged WP49 entry and append this status; no earlier merged evidence altered.
3. `docs/roadmap/release-1.15/prompters/release-1.15-twelvedata-volume-diagnosis.md` - this runbook.
4. `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs` - six fake scenarios in three theory cases, test helper only.

No production source changes warrant a separate implementation PR; the test/doc follow-up remains reviewable in corrected PR #436, OPEN/UNMERGED. Owner checkout/untracked prompt/key-generator and private preparations preserved. No credential/presence/private-history access, real requests, Docker, Azure, registry, deployment, import, start, verify or browser qualification. Historical credits/defect unknown.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
