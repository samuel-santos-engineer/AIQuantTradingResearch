# WP45 - Twelve Data metadata contract compatibility (offline)

## Baseline and authority

Verified PR #431 MERGED, current origin/main exactly `37aeb51a181fe28fa08d293acbe274c38e357f66`. New clean isolated branch `fix/release-1.15-wp45-metadata-compatibility`; owner dirty/untracked checkout and all private resources preserved. Effective AGENTS/LF policy, current adapter, Worker mapping, Python strict validator, fake tests and WP42/WP44 evidence reviewed. No provider/DNS/HEAD/account probe, credential value/presence inspection, historical private response/artifact access, Azure or Docker operation. Documentation browsing only accessed public documentation/search pages, never an API example link.

Historical WP44 remains BTC/USD HTTP 200 / response_parse / INVALID_PROVIDER_METADATA, requestsStarted=1, responsesReceived=1, outcomeUnknown=false, bundlePublished=false, exit 1; ETH/USD not requested, no bundle/import/database/chart. Specific missing/wrong-typed field and credit usage remain unknown. Approval consumed. New diagnostics cannot retroactively identify that field.

## Authoritative documentation reviewed

Accessed **2026-11-07**, without credentials or endpoint execution:

- [Official time-series documentation](https://twelvedata.com/docs/market-data/time-series): response types/example, metadata meanings and intraday output timezone parameter. The success example includes status and exchange_timezone; it does not explicitly guarantee optional omission for crypto.
- [Official timezone support guide](https://support.twelvedata.com/en/articles/5745849-timezones): cryptocurrency default timezone is UTC; request timezone can be selected.
- [Official errors documentation](https://twelvedata.com/docs/introduction/errors): standardized code/message/status error structure. We recognize fixed structural status=error only; never expose message/code or infer account state.

No external data fixtures, actual response bodies or documentation candle values were copied into tests. All fixtures remain synthetic. General documentation is evidence of intended contract, not evidence of the previous response or current provider availability.

## Field contract and handling decision

| Field/context | Documentation evidence | Local correctness requirement / safe handling |
| --- | --- | --- |
| top-level object | Response example is an object | Required; reject non-object/duplicate keys |
| status | Documented string; example ok | Retain required string ok; reject error/unknown/null/missing/wrong type |
| meta | Documented object | Retain required object; no fabricated metadata |
| meta.symbol | Documented string | Required exact requested BTC/USD or ETH/USD; never infer identity from arbitrary rows |
| meta.interval | Documented string | Required exact 1min; never resample/invent interval |
| meta.exchange_timezone | Documented string describing exchange timezone | Retain required string and existing UTC check; omission not established as supported |
| timezone request parameter | Controls intraday output datetime | Keep explicit UTC; offset-free timestamps still require strict UTC interpretation/minute alignment/window validation |
| values | Documented object array | Require bounded collection and strict existing per-row validation |
| datetime / OHLC / volume | Documented strings | Preserve timestamp/decimal/volume requirements; no missing-data synthesis |
| currency/exchange/mic_code/type | Documented ancillary metadata | Not consumed as identity/time/volume proof; no new requirement added |

The documentation does **not** explicitly designate the core fields above optional. No absence relaxation is justified. Likewise, exchange timezone is not an output-timezone echo: the current exchange_timezone==UTC rule is a conservative application restriction, **not a documented universal provider requirement**. The stock example cannot establish the actual crypto response schema. Removing or interpreting that check differently requires defensible crypto-specific contract evidence and a reviewed equivalent validation rule; this task does not guess.

BTC/ETH UTC defaults plus explicit UTC request support timestamp interpretation, but do not independently prove which field was omitted or make missing symbol/interval safe. Base-asset volume units remain the existing owner confirmation; the documentation review does not independently verify actual BTC/ETH volume capability.

## Small implementation delivered

No accepted/rejected payload behavior changed. Following the prompt's conservative fallback, replace the combined required-metadata check with four fixed category signals:

| New failure code | Reliably distinguishes |
| --- | --- |
| INVALID_METADATA_OBJECT | meta missing, null or non-object |
| INVALID_METADATA_SYMBOL_FIELD | symbol missing, null or non-string |
| INVALID_METADATA_INTERVAL_FIELD | interval missing, null or non-string |
| INVALID_METADATA_TIMEZONE_FIELD | exchange_timezone missing, null or non-string |

All emit response_parse. Present contradictory strings retain existing symbol/interval/timezone mismatch codes. Missing status remains INVALID_PROVIDER_STATUS. Existing INVALID_PROVIDER_METADATA remains accepted for compatibility/older producers. No code includes provider field values, arbitrary text, headers, body, keys, exception messages or paths.

Version-1 ten-field envelope, size/input bounds, send/response/status/unknown semantics unchanged. Update C# producer and Python allowlist together; older consumer rejects unfamiliar codes conservatively. No schema evolution. Tests exercise actual Worker envelopes through actual Python parser, not copied expected messages. New code has not been packaged into existing prepared images.

## Offline commands and results

Existing cached assets/packages only, empty-feed restore, no downloads. Run from isolated checkout:

```powershell
$feed = Join-Path $env:TEMP 'aiq-wp45-empty-feed'
# Create this empty local directory if absent; do not use a network feed.
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source $feed -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
# Actual execution used the existing project virtual-environment interpreter:
python -B -m unittest test_research_chart test_research_snapshot_client -q
git diff --check
```

Final results: **116 C# passed**, 0 failed/skipped, exit 0; **23 Python workflow passed**, exit 0; **61 chart/client passed**, exit 0; total **200 passed**. First focused run passed 112 tests; four additional ETH second-symbol category cases were then added and final 116 passed. No test failure or skipped tool execution. Existing CA1711, SYSLIB1045, CA1861, CA2219 warnings and Streamlit bare-mode notices persist. Existing installed tools only; no PowerShell execution-policy bypass.

Coverage: valid synthetic BTC/ETH with required metadata and unrelated fields omitted; each metadata category missing/null/wrong-typed; missing/wrong status and structural error envelope; contradictory symbol/interval/timezone; strict UTC/alignment/order/duplicate/OHLCV/volume/window/body bounds; exact HTTP status and cancellation/transport semantics. Actual new C# failure envelopes pass through Python, which also round-trips all new codes using owned fake children and rejects forged/canary-bearing diagnostics. Four ETH second-symbol missing-field cases retain counts2/2 and publish no partial bundle. Synthetic successes remain SyntheticFixture and operational import rejects them. No authentic-data qualification.

## Exact changed paths

1. `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs` - split required-field diagnostic boundaries; documentation comment; acceptance unchanged.
2. `src/AIQuantTradingResearch.Worker/ResearchLocalHistory.cs` - four version-1 allowlisted mappings.
3. `eng/docker/release-1.15/wp39_local_history.py` - matching strict allowlist.
4. `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs` - precise synthetic field cases, cross-runtime envelopes, second-symbol rejection.
5. `tests/runtime/release_1_15/test_wp39_local_history.py` - new fixed-code round-trip/forgery cases.
6. This new runbook.
7. `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` - append only.

All changed text UTF-8/LF/no BOM, separate CRLF scan and git diff --check passed. No historical evidence rewritten or private artifact staged. Endpoint/query, max two sequential calls, UTC/ASC/minute/1000/latest24h/grace15s, no retry/pagination/fallback, atomic publication, private input/import/provenance, UI/API and architecture unchanged.

## Review decision and remaining engineering option

Return one new WP45 PR OPEN/UNMERGED. A relaxation is **not supported** by the reviewed documentation; this source change provides diagnostic precision only. Concrete next option: obtain authoritative crypto-specific clarification of core field presence and exchange/output-timezone semantics before changing requiredness. Alternatively, an independently authorized future acquisition can identify a fixed field category with this build, without raw response disclosure; no new live approval is requested or implied now. The historical cause and credits remain unknown. No new governance/licensing gate.

Any later provider invocation requires separate explicit owner approval and fresh build/manifest/TLS/ACL/destination checks. No authentic import, local/public chart or Azure readiness established.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
