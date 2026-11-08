# WP46 - crypto response contract and narrow compatibility

## Baseline and boundaries

Verified PR #432 MERGED and origin/main exactly `819298b4f02258e4345e2ebd301cadff5bacbffa`. New clean isolated branch; owner checkout/untracked files untouched. Effective AGENTS/LF governance, current adapter, Worker/version-1 mapping, Python strict validator, tests and prior WP39-WP45 runbook decisions reviewed. All authored text UTF-8/LF/no BOM.

Public documentation research and fake tests only. Zero provider/DNS/HEAD/account/credit probes, Acquire invocations, credential value/presence inspection, historical private artifact access, Azure, Docker, registry publication, authentic import or browser qualification. No package downloads or external fixture data. API example links were not executed.

Historical WP44 remains BTC/USD HTTP 200 / response_parse / INVALID_PROVIDER_METADATA, one send/one response, no ETH/USD request or bundle/import. The exact field/content and credit use remain unknown. New evidence below does not identify that response.

## Authoritative source matrix

Retrieved **2026-11-07**. Public SDK documentation is new evidence beyond WP45's source set; generated models are engineering contract evidence, not proof of all deployed responses.

| Source | Establishes | Does not establish |
| --- | --- | --- |
| [Official time-series docs](https://twelvedata.com/docs/market-data/time-series) | Typed metadata/candle fields; output timezone parameter applies intraday; example success includes status | Stock example is not a BTC/ETH response guarantee or evidence of historical content |
| [Official crypto timezone guide](https://support.twelvedata.com/en/articles/5745849-timezones) | Cryptocurrency default UTC; configurable output timezone | No promise that exchange metadata echoes output timezone or is always present |
| [Official SDK response model](https://github.com/twelvedata/twelvedata-java/blob/main/docs/GetTimeSeries200Response.md) | meta, values, status listed without optional markers | Does not prove current account/data availability |
| [Official SDK metadata model](https://github.com/twelvedata/twelvedata-java/blob/main/docs/GetTimeSeries200ResponseMeta.md) | symbol/interval required; exchangeTimezone optional | No crypto-specific payload observed; optional is not permission to accept malformed fields |
| [Official generated model source](https://github.com/twelvedata/twelvedata-java/blob/main/src/main/java/com/twelvedata/client/model/GetTimeSeries200ResponseMeta.java) | JSON name exchange_timezone maps to exchangeTimezone, required=false; symbol/interval required=true | Client annotations do not independently prove timestamp correctness of a response |
| [Official candle model](https://github.com/twelvedata/twelvedata-java/blob/main/docs/TimeSeriesItem.md) | datetime/OHLC strings; volume optional generally | Not a base-volume unit/capability guarantee for this workflow |
| [Official errors docs](https://twelvedata.com/docs/introduction/errors) | Structural error envelope with code/message/status | HTTP success alone does not prove valid observations or credits |

Raw public documentation counterparts were read at raw.githubusercontent.com/twelvedata/twelvedata-java/main/docs/{GetTimeSeries200Response,GetTimeSeries200ResponseMeta,TimeSeriesItem}.md and the matching generated Java source. Initial GitHub Raw redirect returned cache miss; direct raw documentation URLs succeeded. No dependency installation or source import. No vendor candle values/provider messages copied into fixtures.

## Necessity and handling matrix

This is the local adapter decision, informed by the sources above, not a claim of complete provider schema conformance.

| Field or guarantee | Necessity | WP46 handling |
| --- | --- | --- |
| status | Distinguish admitted success from error/ambiguity | Required string ok; error/unknown/null/wrong type/missing rejected; no absence evidence supports relaxation |
| meta | Bind identity/interval | Required object; absent/null/wrong type rejected |
| meta.symbol | Identity | Required exact BTC/USD or ETH/USD for selected request; never inferred from rows |
| meta.interval | Minute identity | Required exact 1min; no resampling |
| meta.exchange_timezone absent | Ancillary exchange metadata, documented optional | Newly accepted for existing crypto-only UTC request; no metadata field fabricated |
| meta.exchange_timezone present | Conservatively screen malformed/contradictory metadata | String UTC accepted; null/number/non-UTC/empty rejected as before |
| request timezone=UTC | Output timestamp interpretation | Fixed in existing intraday request; crypto-only allowed instruments and strict UTC parsing remain |
| datetime | UTC/minute/window correctness | Exact offset-free format interpreted under explicit UTC request; reject invalid/offset strings, misalignment, future/order/duplicates/window violations |
| OHLC / volume | Valid observations | Existing decimal precision/bounds/OHLC rules; volume still mandatory locally with owner-confirmed base units despite generic SDK optionality |
| source/provenance | Safe publication | Unchanged authentic-only acquisition path, complete two-symbol validation, atomic bundle/hash/receipt binding; fake handler always SyntheticFixture |
| currency/exchange/mic/type | Unused descriptive fields | Existing handling unchanged; not used to invent identity, timezone or volume proof |

**Status absence remains rejected. Exchange-timezone absence is now supported. Exchange timezone need not generally equal output timezone:** their meanings differ. We retain non-UTC-present rejection as a conservative local restriction, not a universal provider guarantee. This may reject otherwise legitimate data; it is not necessary to remove that restriction to support documented field absence. Null is deliberately stricter than the SDK nullable annotation; no null relaxation is part of this increment.

## Exact compatibility change and safety rationale

Only the presence check changes: TryGetProperty records whether exchange_timezone exists. If absent, skip that ancillary check; if present, retain strict string/UTC checks. No new field, default metadata value, accepted symbol or endpoint. Timestamp UTC comes from the fixed timezone=UTC request semantics and crypto UTC default, not the exchange_timezone string. The existing request-instrument validation allows only BTC/USD and ETH/USD. Identity and minute interval remain response-validated even when timezone metadata is absent.

Previously all missing exchange_timezone responses were rejected before candle validation. Now only otherwise-valid responses missing that field continue through unchanged row/completion/window/publication checks. Removing it cannot make malformed status, missing symbol, wrong interval, offset timestamp, invalid OHLC, missing volume or a provider error envelope pass; combined negative tests establish this. This source change is evidence-backed compatibility, not a historical root-cause fix claim.

No Worker/Python code changes: version-1 ten-field bounded envelope and synchronized allowlists remain unchanged, including precise diagnostics for present wrong-typed fields. No diagnostics added merely for observation. Existing actual C# failure envelopes still round-trip through actual Python parser in the focused suite.

## Executed offline verification

Commands from isolated checkout using existing toolchain/cached packages only, Windows PowerShell 5.1 compatible:

```powershell
$feed = Join-Path $env:TEMP 'aiq-wp46-empty-feed'
# Created empty local feed if absent; no network feed.
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source $feed -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
# Actual chart/client execution used existing project virtual-environment Python:
python -B -m unittest test_research_chart test_research_snapshot_client -q
git diff --check
```

| Suite | Final actual result |
| --- | --- |
| C# acquisition/adapter/initializer | **126 passed**, 0 failed/skipped, exit 0 |
| Python workflow/strict diagnostic | **23 passed**, exit 0 |
| Chart/client | **61 passed**, exit 0 |
| Total | **210 passed** |

Baseline 116 C# tests passed before edits. Intermediate compatibility suite passed 119; seven combined invalid-data cases added and final 126 passed. No failures/skips. Python suites passed on unchanged Python/UI source. Existing analyzer CA1711, SYSLIB1045, CA1861, CA2219 and bare-mode Streamlit notices remain. No policy bypass, broad requalification or prepared-image/native/browser claim.

New successes: direct BTC/ETH adapter requests without exchange_timezone preserve UTC/instrument/query identity; complete fake Worker runs with the field absent on BTC or ETH remain SyntheticFixture and rejected by authentic verifier. Replaced obsolete missing-timezone negative expectation with success coverage; second-symbol wrong-type negative retained. Seven absence-plus-invalid-core cases reject. Existing present wrong types/null/contradictory timezone/status/error envelopes/forged diagnostics, transport/HTTP/cancellation, size bounds and second-symbol no-partial-publication regressions remain.

## Exact changed paths and review

1. `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs` - optional absence only, evidence comment.
2. `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataMinuteCandleSourceTests.cs` - both-symbol success and combined rejection fixtures.
3. `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs` - synthetic Worker success and preserved second-symbol negative coverage.
4. This new `docs/roadmap/release-1.15/prompters/release-1.15-wp46-crypto-response-contract.md`.
5. `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` - append only.

UTF-8/LF/no BOM, separate CRLF scan and git diff --check passed. No private files staged; historical WP45/WP44 text unchanged. No architecture, query/limits/grace, retry, importer, public schema/UI or provenance change. Existing old images do not contain this new source; no new image built here.

## Outstanding uncertainties and next action

Optional metadata now has credible official SDK evidence and equivalent UTC/identity validation, so this limited change is justified. No authoritative example establishes every actual BTC/ETH 1min success variant, status omission is not supported, present non-UTC exchange metadata remains a conservative rejection, and actual base-volume availability remains unverified. None explains WP44's specific failed field or credits.

Next implementation action: review/merge this narrow PR if accepted, then freshly build/qualify this source before any future operational attempt. To resolve remaining schema questions without provider credits, the owner can request written Twelve Data support clarification or a public SDK documentation issue: (1) status presence for BTC/USD and ETH/USD 1min; (2) omission versus null for exchange_timezone; (3) whether it describes venue timezone when output timezone=UTC; (4) base-asset volume availability/units. Draft questions here only; no message/issue submitted or new governance gate.

Any future real Acquire requires **new explicit single-attempt owner approval** and fresh source/build/manifest/TLS/ACL/destination checks. No approval inferred from this task or PR. New WP46 PR remains OPEN/UNMERGED. `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
