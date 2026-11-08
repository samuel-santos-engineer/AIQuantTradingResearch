# Release 1.15 WP50 - Twelve Data volume diagnostics

Baseline: PR #436 squash/main `52398b2ad31f75a4a098ca1c86bf414740e6140c`, verified 2026-11-08. Offline implementation only, owner review required. All modified/new text is UTF-8/LF/no BOM; unrelated files and historical WP39-WP49 evidence are preserved.

## Product scope and result

Twelve Data remains the sole provider for BTC/USD and ETH/USD. No alternative provider, provider-agnostic architecture, dependency, schema, chart, Docker or Azure change. The HTTP request, sequential BTC then ETH execution, no retry, UTC minute/ASC, 1000-row upper bound, 24h window, 15-second grace, mandatory OHLCV/base units, provenance and two-symbol atomic publication are unchanged.

This patch refines future failures without changing accepted observations. It does not identify WP47's past defect: its historical INVALID_VOLUME and credits remain unknown; its one-attempt approval was consumed. No private response was read and no live request was made.

## Exact finite taxonomy

All six new codes use failedStage=observation_validation. No values, lengths, fragments, headers, exception/provider text, arbitrary paths or other dynamic details are emitted.

| failureCode | Predicate and old behavior |
| --- | --- |
| VOLUME_MISSING | No volume property; previously INVALID_VOLUME. |
| VOLUME_NULL | Explicit JSON null; previously INVALID_VOLUME. |
| VOLUME_WRONG_TYPE | Non-string/non-null, including JSON numeric zero, booleans, arrays and objects; previously INVALID_VOLUME. |
| VOLUME_INVALID_FORMAT | Empty, >40 characters, disallowed lexical character (including signed negatives/exponents/whitespace), or failed decimal parsing due to multiple points/lone point; previously INVALID_VOLUME. |
| VOLUME_PRECISION_EXCEEDED | Lexically admitted text has more than 12 characters after its first decimal point; previously INVALID_VOLUME. This check retains existing precedence even for multiply malformed text. |
| VOLUME_OUT_OF_RANGE | Decimal overflow after format/precision checks, or parsed volume >10^12 after OHLC encoding validation; previously INVALID_VOLUME for overflow, INVALID_OBSERVATION for constructor-domain range. |

Only diagnostics change. String zero remains an explicit provider observation; missing/null never becomes zero. Existing decimal grammar is retained, including `.5`, `1.` and leading zeros. No numeric JSON acceptance, float conversion, rounding, quote-volume division, inferred units, provider mixing or OHLC-only downgrade. Existing decimal/instrument/OHLC/time constructor validation remains in place. Negative strings remain format failures rather than being newly parsed as signed decimals.

Precedence: HTTP/header/size/UTF-8/JSON/duplicate/status/metadata/collection checks first; row object then timestamp format; volume presence/type/lexical/precision/decimal parse; OHLC field decoding; parsed-volume upper bound; candle constructor/sequence; completed-bar filtering; Worker count/window validation and publication. Thus malformed timestamp wins over missing volume, missing volume wins over missing close, and missing close wins over an otherwise parseable excessive volume. Unrelated constructor/sequence failures remain INVALID_OBSERVATION. Encoding failures remain INVALID_OHLC_ENCODING. Invalid JSON/non-object rows do not acquire a volume label. Forming candles are validated before filtering, so invalid volume there still fails. Empty/all-valid-forming results still yield ZERO_ACCEPTED_CANDLES.

Range rejection inside the OHLC block preserves the first failure and avoids emitting a second OHLC event. Finite enum members are appended, preserving existing enum ordinals. Worker acquisition trace maps them explicitly; Worker Codes and Python CHILD_CODES accept exactly the added literals. Legacy INVALID_VOLUME remains recognized in the version1 contract for older producers, although the new normal extraction paths emit specific codes.

## Envelope compatibility and safety

Version remains 1, exactly ten fields: version, event, failedStage, instrument, httpStatus, requestsStarted, responsesReceived, outcomeUnknown, failureCode, bundlePublished. No subtype/details field added. Producer and consumer must be shipped together: an older strict Python consumer will conservatively reject unknown new codes rather than forward them. New consumer accepts old INVALID_VOLUME. No backward compatibility claim is made for an old consumer parsing a new producer.

Stage/instrument/HTTP and send/response counters remain factual. BTC-first validation failure is1/1; ETH-second is2/2. Both have outcomeUnknown=false only because responses were received; this says nothing about credits. Transport timeout/send-without-response retains unknown outcome. First classified failure wins; zero or partial validated rows cannot publish a bundle. Python still rejects unknown, malformed, extra-field, duplicate-property or oversized envelopes and reconstructs only fixed scalars. Sanitized logs disclose no volume value, even an invalid one.

## Tests and evidence

Fresh offline results: **248 passed =163 focused C# +24 Python workflow +61 chart/client**, exit0/no skips. Historical baseline was237; the new total is not inferred from it. The final C# run also feeds actual C# failure envelopes through the real strict Python child parser for every plain representation case at both symbols.

Coverage:

- Expanded 24-case representation matrix at BTC-first and ETH-second, each plain, valid-prefix-then-bad-row and forming-row context:144 fake scenarios. Includes each subtype, raw invalid JSON, decimal overflow, malformed/multiple points and excess precision including trailing zeros; exact stage/code/HTTP/counters and no bundle.
- Four precedence theory cases at both symbols: malformed timestamp with absent/excessive volume; missing OHLC with absent/excessive volume. Existing partial-row/empty tests retained.
- Six accepted boundary/grammar cases across both symbols: explicit zero, 12 decimal places, domain maximum, leading zeros, leading point and trailing point. Exact values survive publication; fixtures remain SyntheticFixture and authentic-only verification rejects them.
- Python round-trips all six new codes plus legacy INVALID_VOLUME at both symbol positions; forged/extra fields, arbitrary codes, booleans masquerading as integers and false publication facts are rejected. Existing malformed/oversized, canary non-disclosure, cancellation, transport, failure-first, initializer, chart/client regressions remain.

Commands:

```text
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source <empty-local-feed> -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter "FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests" --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
<existing-venv-python> -B -m unittest test_research_chart test_research_snapshot_client -q
```

Restore used only global cached packages and an empty local feed, NuGetAudit disabled; no dependency download. Chart/client run used PYTHONPATH=python/presentation. Existing CA1711, SYSLIB1045, CA1861, CA2219 analyzer and bare-mode Streamlit ScriptRunContext warnings appeared; no unrelated repair. C# rerun followed the added actual-envelope/Python compatibility assertions. git diff --check and separate changed-file UTF-8/LF/no-BOM inspection are required before commit/PR.

## Exact changed paths

1. `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs` - static volume event classification, same acceptance.
2. `src/AIQuantTradingResearch.Worker/ResearchLocalHistory.cs` - coordinated mappings/allowlist, legacy compatibility.
3. `eng/docker/release-1.15/wp39_local_history.py` - coordinated finite consumer allowlist only.
4. `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs` - representation/context/precedence/acceptance and actual Python compatibility proof.
5. `tests/runtime/release_1_15/test_wp39_local_history.py` - strict subtype/legacy envelope tests.
6. `docs/roadmap/release-1.15/prompters/release-1.15-wp50-twelvedata-volume-diagnostics.md` - this runbook.
7. `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` - append-only WP50 status.

## Remaining unknowns and next operation

Exact current BTC/USD/ETH/USD minute volume availability, base-unit/feed semantics and account/product behavior remain unverified. This patch cannot make an unavailable mandatory field valid or establish volume units. Earlier owner confirmation remains owner-reported. See the unchanged Twelve Data-only diagnosis runbook for official evidence limits. No new licensing or unrelated approval gate.

A future attempt requires separate, explicit, fresh single-attempt owner authorization and fresh preparation/identity/TLS/ACL/destination checks; no request is authorized by this PR or passing tests. Stop at first failure, never retry. No provider/account/credit/capability/DNS/HEAD requests, support messages, credential value/presence reads, private response/bundle inspection, Docker/Azure/registry operations, deployment, real Acquire/Import/Start/Verify or browser qualification occurred here. Owner dirty checkout/private preparations/resources preserved. Leave implementation PR OPEN/UNMERGED for review.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
