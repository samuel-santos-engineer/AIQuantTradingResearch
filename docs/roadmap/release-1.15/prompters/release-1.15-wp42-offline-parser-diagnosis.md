# WP42 - offline Twelve Data parser diagnosis

## Scope and observed history

Existing PR #429 was verified OPEN/unmerged, head `75055bb73710f34a205bda6849963269d6d10674`, base `7c92eb98b8e9fe7d204375cf43c3e11f3f68a5e7`. Its clean isolated branch was reused, preserving the owner's dirty checkout. This follow-up is offline-only. No live provider/DNS/HEAD/account probe, credential read, historical private evidence inspection, Azure, Docker operation, dependency download or merge occurred.

Historical WP41 facts remain: one approved invocation, BTC/USD HTTP 200, response_parse / INVALID_PROVIDER_PAYLOAD, requestsStarted=1, responsesReceived=1, outcomeUnknown=false, bundlePublished=false, exit 1. ETH/USD not requested; no bundle/import/database/chart. Exact historical payload defect and charged credits remain **unknown**. The prior approval is consumed. No new category is assigned retroactively.

## Existing paths and new boundaries

Before this correction, oversized streamed bodies, malformed UTF-8/JSON, duplicate JSON properties, wrong top-level type, absent/wrong/non-ok status, malformed/missing metadata fields and invalid/missing/oversized values collections could collapse into INVALID_PROVIDER_PAYLOAD at parse time (stream size failure occurred before response_parse). Well-typed metadata mismatches were INVALID_PROVIDER_METADATA. Missing/invalid volume was INVALID_VOLUME; timestamp/order/constructor/numeric defects generally became INVALID_OBSERVATION. HTTP and transport failures occurred earlier; no eligible rows were ZERO_ACCEPTED_CANDLES in acquisition; publication errors occurred later. Declared oversize was grouped with invalid headers.

The bounded version-1 ten-field envelope is unchanged. Eleven additive fixed codes are produced by typed C# events and accepted atomically by the Python allowlist:

| Code | Reliable boundary / stage |
| --- | --- |
| INVALID_JSON_ENCODING | Strict UTF-8 decoding or bounded-depth JSON parsing failed; response_parse (does not distinguish encoding from JSON/depth) |
| INVALID_PAYLOAD_SHAPE | Non-object top level or duplicate property anywhere in JSON; response_parse |
| PROVIDER_ERROR_ENVELOPE | Object with string status exactly error; response_parse. Does not inspect provider code/message or infer account/key/quota state |
| INVALID_PROVIDER_STATUS | Missing, non-string or unrecognized status; response_parse |
| PROVIDER_SYMBOL_MISMATCH | Well-typed metadata symbol differs from requested symbol; response_parse |
| PROVIDER_INTERVAL_MISMATCH | Well-typed interval differs from 1min; response_parse |
| PROVIDER_TIMEZONE_MISMATCH | Well-typed exchange_timezone differs from UTC; response_parse |
| INVALID_CANDLE_COLLECTION | Missing/non-array/over-1000 values at response_parse; non-object row at observation_validation |
| PAYLOAD_SIZE_LIMIT | Declared or actually streamed bytes exceed 1 MiB; http_response |
| INVALID_CANDLE_TIMESTAMP | Missing/non-string/unparseable timestamp; observation_validation |
| INVALID_OHLC_ENCODING | Missing/non-string/malformed/overprecision OHLC decimal representation; observation_validation |

Retained codes: INVALID_PROVIDER_METADATA for absent/wrong-typed metadata or required fields; INVALID_VOLUME for missing/invalid volume representation; INVALID_OBSERVATION for semantic constructor bounds, minute alignment, future/order/duplicate timestamps and acquisition window checks; ZERO_ACCEPTED_CANDLES for empty/no completed eligible rows. Semantic OHLC bounds and volume range failures remain under INVALID_OBSERVATION where the existing constructor enforces them; the new encoding code does not claim semantic precision it lacks. No valid data is accepted through a fallback mapping. Existing exception surface remains fixed ResearchUnavailableException without provider text; unknown CLI outcomes retain generic fixed categories.

Compatibility: version/fields, 2048-byte parser limit, 4096-byte private input bound, counters/status/unknown semantics and single event are unchanged. Old codes remain accepted; old Python rejects unfamiliar new codes conservatively as CHILD_DIAGNOSTIC_INVALID, so deploy producer and consumer together. No runtime/image was repackaged here; old prepared images are not evidence of the new capability.

## Offline verification

| Suite | Actual final result |
| --- | --- |
| C# acquisition, adapter, initializer | 101 passed, 0 failed/skipped, exit 0 |
| Python workflow/strict diagnostics | 23 passed, exit 0 |
| Existing chart/client regressions | 61 passed, exit 0 using installed project virtual environment |
| Total | 185 passed; fake-only/local fixtures |

Commands from existing isolated PR branch, cached assets only:

```powershell
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
# Invoke existing project virtual-environment Python, not a new installation:
python -B -m unittest test_research_chart test_research_snapshot_client -q
git diff --check
```

The C# fake-handler Worker tests now feed actual RunAsync failure JSON through the actual Python child_failure parser, including every new code, asserting exact envelope equality and empty subprocess stdout/stderr. This requires the existing python executable on PATH and uses a bounded owned child, no network. Python additionally round-trips every new code through owned fake children and rejects canary-suffixed forged codes. Existing malformed/oversize/duplicate/type/version rejection and canary exception/output assertions remain.

Synthetic cases cover BTC/ETH success with SyntheticFixture-only provenance, malformed JSON/UTF-8, wrong root, structural error envelope with canary message, invalid status, missing/wrong metadata, symbol/interval/timezone, missing/non-array collections/non-object row, timestamps, duplicates/order/alignment, OHLC encoding/semantic rejection, missing/invalid volume, empty/forming rows, declared and streamed oversize. Existing 401/403/429/500/301, transport/deadline/cancellation/body-read failure retain observed facts. Second-symbol error-envelope failure reports two sends/two responses, HTTP 200, ETH/USD, and no partial bundle. No retries or credential/provider text leakage.

Intermediate test runs exposed four obsolete generic-code expectations (updated to precise assertions), then one stage-mapping defect: non-object rows were incorrectly mapped to response_parse. The C# mapping was corrected to retain current stage, preserving both collection and row boundaries. Final 101-test run passed. Existing analyzer warnings CA1711, SYSLIB1045, CA1861, CA2219 and Streamlit bare-mode notices remain; no new warnings or skipped runtime tests. UTF-8/LF/no BOM and separate CRLF checks apply to all eight changed text paths; unrelated files were not normalized.

## Exact follow-up changed paths

1. `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs`
2. `src/AIQuantTradingResearch.Worker/ResearchLocalHistory.cs`
3. `eng/docker/release-1.15/wp39_local_history.py`
4. `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs`
5. `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataMinuteCandleSourceTests.cs`
6. `tests/runtime/release_1_15/test_wp39_local_history.py`
7. This new WP42 runbook.
8. `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` (append only).

Endpoint/query, max-two sequential requests, timeouts, no retries/pagination, UTC/ASC/minute/1000 bounds, completion grace, volume confirmation, atomic publication, importer/provenance, UI and fixture safeguards remain unchanged. No private artifacts staged.

## Decision and remaining uncertainty

Return PR #429 OPEN/UNMERGED for review of the offline classification capability. Historical cause cannot be recovered from these synthetic tests and was not inspected. Any future real attempt requires separate explicit owner approval and fresh build/runtime identity checks; this task does not request or perform it. No new licensing gate. No authentic import or public qualification.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
