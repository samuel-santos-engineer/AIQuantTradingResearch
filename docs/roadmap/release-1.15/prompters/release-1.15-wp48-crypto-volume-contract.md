# WP48 - crypto volume contract compatibility

Status: **OFFLINE_CONTRACT_AND_TESTS_COMPLETE; ACCEPTANCE_UNCHANGED**.

## Baseline and historical observation

Verified PR #434 MERGED, freshly fetched origin/main `05dd8ba4abc95c28d8e8abf6df39661e42dc70dc`. New clean isolated branch preserves owner checkout/untracked files, private preparations, Docker resources and credentials. Effective AGENTS/LF governance, execution plan, prior WP39-WP47 workflow evidence, actual adapter, Worker mapping, Python strict allowlist, constructor/import/provenance and fake fixtures reviewed. No old private response/artifact read.

WP47 historical fact: one approved Acquire invocation, BTC/USD HTTP200, observation_validation / INVALID_VOLUME, sends/responses1/1, exit1, outcomeUnknown=false, bundlePublished=false. ETH/USD not requested; no bundle/import/database/chart. Specific volume defect and credit usage **unknown**, approval consumed. Nothing below assigns a retrospective cause.

## Public authoritative source matrix

Retrieved 2026-11-07 America/Sao_Paulo (2026-11-08 UTC). Public documentation/search only; API example URLs were never executed.

| Source | Evidence | Limit |
| --- | --- | --- |
| [Official time-series docs](https://twelvedata.com/docs/market-data/time-series) | Generic volume type string; volume for applicable instruments | No BTC/USD or ETH/USD 1min base-unit/availability guarantee |
| [Official SDK candle model](https://github.com/twelvedata/twelvedata-java/blob/main/docs/TimeSeriesItem.md), read via [raw documentation](https://raw.githubusercontent.com/twelvedata/twelvedata-java/main/docs/TimeSeriesItem.md) | volume is optional String; datetime/OHLC are strings | Optional field is not mandatory OHLCV availability; no numeric JSON representation or unit guarantee |
| [Official crypto catalog page](https://twelvedata.com/exchanges/digital_currency?group=core) | Time-series overview associates volume with non-currency instruments | General catalog text does not conclusively establish absence on every crypto response |
| [Official Sheets guide](https://support.twelvedata.com/en/articles/5702399-google-sheets-add-on-documentation) | Currency time series described as OHLC for physical/digital currencies, unlike generic OHLCV function | Add-on description is not a complete REST schema or historical body evidence |

Generated TimeSeriesItem.java source was also requested through raw.githubusercontent.com and GitHub documentation views; both returned cache miss. No source annotations or nullable guarantee are invented from that unavailable file. Available official model documentation supports optional string; it does not explicitly define JSON null as valid data. No source reviewed establishes lossless numeric-JSON support, BTC/ETH base-asset volume units, or guaranteed volume for these exact minute requests. No vendor values or provider messages copied into fixtures.

## Product requirement versus representation

The existing product requires authentic, attributable BTC/USD and ETH/USD minute **OHLCV**, with defensible BTC/ETH base-volume units. Optionality in a generic SDK does not make volume optional here. Prior owner unit confirmation is retained as owner-reported, not independently verified by this research. No missing/null volume becomes zero, no quote/base conversion, no inferred unit from symbol/currency metadata, and no silent OHLC-only product change.

String encoding is potentially incidental, but there must be credible representation evidence before widening acceptance. Numeric JSON could theoretically be parsed losslessly with bounded decimal processing; that fact alone does not establish provider contract or units. No speculative numeric acceptance or new diagnostic family is added. Current fixed categories plus the explicit matrix already establish the boundaries sufficiently for owner review.

## Actual parser decision tree

1. HTTP facts/headers/body byte limit, strict UTF-8/JSON/depth/unique properties, top-level/status, identity/interval/timezone and bounded values collection run first. Invalid raw nonfinite JSON fails response_parse / INVALID_JSON_ENCODING; duplicate properties fail INVALID_PAYLOAD_SHAPE.
2. Row must be an object; otherwise INVALID_CANDLE_COLLECTION. Timestamp is read before volume; missing/wrong/unparseable datetime yields INVALID_CANDLE_TIMESTAMP, not a volume diagnosis.
3. Volume GetProperty/GetString requires a present JSON string. Missing, null or non-string (number/bool/object/array) becomes observation_validation / INVALID_VOLUME.
4. Value() rejects empty or over40-character strings, any character other than ASCII digits/dot, more than12 digits after the first dot, or failure of invariant decimal parsing. Thus whitespace, sign, exponent, comma, nonfinite text, malformed decimals, decimal overflow and excess precision become INVALID_VOLUME.
5. CompletedResearchCandle constructor enforces nonnegative volume <=10^12 and exact <=12 decimal places together with timestamp/OHLC/instrument rules. A parseable volume above10^12 throws ArgumentException and maps to INVALID_OBSERVATION, not INVALID_VOLUME. Negative strings are rejected earlier lexically. Other invalid constructor/sequence/window conditions remain INVALID_OBSERVATION.
6. Validated forming bars are excluded only later; malformed volume is not accepted merely because its candle would be filtered. Zero completed eligible observations yields ZERO_ACCEPTED_CANDLES. Two complete validated instruments are required before atomic bundle publication; no partial success.

## Representation matrix (unchanged acceptance)

| Synthetic representation | Result / fixed code |
| --- | --- |
| Missing, null | Reject INVALID_VOLUME; never fill |
| JSON number integer/decimal/negative | Reject INVALID_VOLUME; no double conversion |
| Boolean, object, array | Reject INVALID_VOLUME |
| Empty/whitespace/malformed/canary string | Reject INVALID_VOLUME |
| Signed or exponent string | Reject INVALID_VOLUME |
| String NaN/Infinity | Reject INVALID_VOLUME |
| Unquoted NaN | Reject INVALID_JSON_ENCODING before row validation |
| String >12 fractional digits, >40 characters, decimal overflow | Reject INVALID_VOLUME |
| Parseable string volume above10^12 | Reject INVALID_OBSERVATION |
| Explicit synthetic zero string | Accept exact zero; this is supplied test data, never a missing-value default |
| Synthetic12-place fraction or upper bound10^12 | Accept exact decimal and retain SyntheticFixture-only provenance |

No new payload shape accepted or previously accepted shape rejected by WP48. Production adapter, Worker, Python allowlist, request query, sequential budget, timeout, UTC/ASC/minute/1000/24h/grace15s, source identity, atomic publication, private input, importer, provenance, UI, schema and retry policy unchanged. Version1 ten-field diagnostic semantics and unknown outcomes unchanged.

## Executed offline qualification

Only tests and documentation changed. New synthetic handler option injects a representation at first or second symbol; no real input/credential access. Twenty-one invalid representation cases each run both positions (42 fake scenarios), verify exact code/stage/HTTP200/counters1/1 or2/2 and absence of bundle. Three valid-boundary cases execute both instruments, check exact serialized volume, retain SyntheticFixture and fail authentic-only verification. Existing actual C# envelope/Python, malformed/forged/oversize/canary, transport/cancellation, OHLCV, initializer and chart/client coverage retained. The representation matrix is provider-independent test data, not a statement that Twelve Data returned any case.

Commands from isolated checkout, existing cached dependencies/tools only; Windows PowerShell5.1 compatible:

```powershell
$feed = Join-Path $env:TEMP 'aiq-wp48-empty-feed'
# Create the empty local feed if absent; no network package sources.
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source $feed -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
# Actual execution used existing project virtual-environment Python:
python -B -m unittest test_research_chart test_research_snapshot_client -q
git diff --check
```

Final **234 passed**:150 C# (0 failed/skipped),23 workflow,61 chart/client; all exit0. First matrix run147 C# passed; three additional number/length cases added, final150 passed. No failures or skipped suite. Existing CA1711/SYSLIB1045/CA1861/CA2219 analyzer warnings and bare-mode Streamlit notices remain. No download, policy bypass, Docker or browser run.

## Exact changed paths

1. `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs` - synthetic representation injection and matrix/success assertions.
2. This `docs/roadmap/release-1.15/prompters/release-1.15-wp48-crypto-volume-contract.md`.
3. `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` - append only.

UTF-8/LF/no BOM and separate CRLF/diff checks passed. No source/diagnostic/schema/UI/Docker/Azure change, private artifact or credential staged. Historical records preserved.

## Source-fit risk and next owner decision

Twelve Data may not satisfy mandatory base-asset OHLCV for the currently selected crypto feed. Optional generic volume and OHLC currency descriptions justify treating this as a source-fit uncertainty, not another formatting fix or proof of the historical missing field. Repeated acquisition is not a substitute for contract evidence.

Concrete later engineering options, none executed or requiring a licensing gate:

1. Obtain written technical clarification for the exact symbols/minute feed: presence/nullability, string versus numeric format, units, venue/aggregation and availability. Owner can use public support/SDK documentation channels without consuming API credits; no message/issue submitted here.
2. If a legitimate existing Twelve Data export with mandatory base volume is available, later explicitly authorize its provenance/format review against the unchanged importer. Do not assume such an export exists or mix unrelated sources.
3. If the source cannot supply it, separately scope evaluation of an exchange-origin historical feed with documented base-asset volume, consistent OHLC/volume venue identity and bounded coverage. A provider adapter/request-contract amendment would be separate owner work; no new endpoint/integration proposed as implemented here. Do not combine another source's volume with these candles without a defensible joint contract.

Keep OHLCV requirement intact; no zero fill, quote-volume division or OHLC-only downgrade. Return one WP48 PR OPEN/UNMERGED. No future live request authorized/requested; a later attempt needs new explicit single-attempt approval and fresh source/build/manifest/TLS/ACL/destination checks. Current exact defect/credits unknown.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
