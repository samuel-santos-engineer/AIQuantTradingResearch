# Release 1.15 WP51 - Twelve Data acquisition readiness and diagnostic rehearsal

Date: 2026-11-08. Baseline #437 squash/main `2e6ff99f04ba68b3ade68093dd41931d9194b077`, verified merged before creating a clean isolated worktree. **Offline rehearsal complete; actual attempt NOT authorized or executed.** Twelve Data remains the sole provider for BTC/USD and ETH/USD. All changed/new text is UTF-8/LF/no BOM; historical evidence and owner checkout/private preparations are preserved.

## Demonstrated gap and narrow change

WP50 already exercises real C# volume classification at both symbols and sends actual Worker ten-field envelopes through Python child_failure. Repeating those cases would add no evidence. WP51 extends that existing bridge to exercise the parent workflow as well:

```text
synthetic HttpMessageHandler -> real TwelveDataMinuteCandleSource
 -> real ResearchLocalHistory.RunAsync / AcquisitionTrace JSON
 -> owned local Python interpreter
 -> real wp39_local_history.worker -> run -> run_acquisition -> child_failure
```

Only the subprocess boundary inside Python is fake: its owned in-memory stdin/stderr/wait/poll expose actual C# diagnostic bytes and the synthetic exit status. No dotnet acquisition process or network transport is launched by that fake boundary. The real parent reader thread, bounded stderr capture, first error propagation and cleanup execute. Assertions require exactly one child launch with the exact published-Worker command, minimal fake environment, stdout discarded, fake stdin only, no kill of an already finished child, no output and exact diagnostic round-trip. This is a test-only improvement, not a new production entry point or a live endpoint rehearsal.

The same bridge now checks the existing real fake-Worker success path: parent discards its output and returns no authentic claim. Existing bundle verification still marks it SyntheticFixture and rejects authentic-only verification. Each fake Worker invocation asserts no import receipt under its owned synthetic destination. No import is run.

## Evidence map (reuse, not duplicate tests)

| Requirement | Actual evidence |
| --- | --- |
| All six WP50 subtypes, BTC-first/ETH-second | Existing representation matrix; every plain case now traverses worker/run_acquisition/strict parser, in addition to exact C# stage/code/instrument/HTTP/counter checks. Prefix and forming cases retain real producer validation. |
| No second request after BTC failure, no retries/partial bundle | Handler Calls equals1 at first failure or2 at second; absent destination, fixed bundlePublished=false. Reader bridge adds exactly one process boundary and cannot retry it. |
| Valid-prefix failure, timestamp/OHLC precedence | Existing volume matrix and precedence/partial-row tests, unchanged expected acceptance. Partial/empty-result cases now traverse the parent reader too. |
| Response without valid data / HTTP / transport | Existing malformed response classification, second-symbol metadata/provider-error/429/transport tests and first-symbol HTTP/timeout/transport tests now use the parent-reader bridge. Transport-without-response preserves null status and unknown outcome; received HTTP facts remain exact. |
| Cancellation / genuine deadline mechanics | Existing TwelveDataMinuteCandleSourceTests cancellation, deadline and stream-failure tests plus ResearchLocalHistory cancellation-before-read test. Parent timeout/oversize/owned-child cleanup covered by existing Python fake/owned-local-process tests; no live cancellation experiment. |
| Existing destination unchanged | Existing ExistingDestinationFailsBeforeOutboundBoundaryAndPreservesSentinel proves zero sends and sentinel preservation; no real private destination read. |
| Receipt/bundle safety | Fake diagnostic calls assert no import receipt; failure paths assert no bundle. Receipts are produced only in import_bundle, which rehearsal never calls. No claim of testing real import/recovery here. |
| Legacy/unknown/malformed/extra/oversized envelopes | Existing Python subtype/legacy round-trip and strict rejection tests retained, plus real C# envelopes through the reader. No allowlist widening. |
| Fake success cannot become authentic | Existing successful fake bundle/verification assertions retained; parent-reader success bridge discards outputs instead of turning them into provenance. |

Envelope stays version1/exactly ten fields. Six volume codes and legacy INVALID_VOLUME remain recognized. Producer first failure and Python strict scalar reconstruction are unchanged. No payloads, invalid volume values, lengths, headers, provider/exception text or credentials enter diagnostics. No acceptance, source, request, unit, schema, analytics or UI change.

## Packaging / stale executable review

Actual paths:

- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs`: HTTP parser and finite volume taxonomy.
- `src/AIQuantTradingResearch.Worker/ResearchLocalHistory.cs`: fake-handler seam, failure producer, atomic bundle publication.
- `eng/docker/release-1.15/wp39_local_history.py`: prepare, worker, run_acquisition, child_failure and later import path.
- `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs`: enhanced real-producer/parent-consumer bridge.
- `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataMinuteCandleSourceTests.cs`, `tests/runtime/release_1_15/test_wp39_local_history.py`: retained timeout/cancellation/schema/owned-child regressions.

Prepare publishes the current ROOT Worker into directory/publish, copies that publication into the Docker build context, and uses the current workflow source. worker then executes exactly `dotnet <directory>/publish/AIQuantTradingResearch.Worker.dll --research-acquire-local`, with runtime/bundle/private paths rooted in the selected directory. The bridge asserts that command shape without executing it.

**Residual readiness limitation:** Acquire accepts an arbitrary supplied prepared directory; local.json identifies images/session/TLS, not a cryptographic workflow/Worker version binding. Reusing old preparations or invoking an older checkout can mix versions. New Python accepts legacy INVALID_VOLUME; old Python rejects new WP50 codes as CHILD_DIAGNOSTIC_INVALID. No evidence supports calling an old prepared directory WP50-ready merely because current source tests pass.

There is no demonstrated build rule that intentionally mixes versions in a fresh Prepare. No production packaging change is made: a new embedded compatibility protocol or build-manifest format would exceed the needed test improvement. Minimal next remedy is a **fresh preparation from one reviewed checkout**, recorded source/Worker/workflow identity checks and a no-mixing operator checklist below. The current workflow does not automatically enforce that identity; this remains an explicit pre-attempt verification obligation, not a proven guard. No old preparation, image, manifest, executable hash or TLS/private artifact was inspected in WP51. A new Prepare involves Docker and is NOT authorized here.

## Executed fake-only commands and results

From the isolated checkout, existing cached packages only:

```powershell
$feed = Join-Path $env:TEMP 'aiq-wp51-empty-feed'
New-Item -ItemType Directory -Force -Path $feed | Out-Null
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source $feed -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
# Use the already installed chart virtualenv interpreter; no installation.
& '<existing-venv-python.exe>' -B -m unittest test_research_chart test_research_snapshot_client -q
```

Executed interpreter: existing owner-checkout `.venv/Scripts/python.exe` for chart/client; standard installed python for workflow/bridge. No secret channel read or presence check. Restore's sole source was the empty local feed, NuGetAudit=false; no dependency download. First run passed before success/receipt assertions; final focused C# rerun includes those assertions.

Fresh results: **248 passed =163 C# +24 workflow +61 chart/client**, exit0/no skips. Test count unchanged deliberately: existing cases now exercise a deeper boundary. Existing CA1711, SYSLIB1045, CA1861, CA2219 analyzer warnings and bare-mode Streamlit ScriptRunContext warnings remain. No Docker/Linux packaging, TLS deployment, provider, browser or real Acquire/Import/Start/Verify was qualified. UTF-8/LF/no-BOM and git diff --check are checked separately before commit/PR.

## Single-attempt operator checklist - documentation only, NOT EXECUTED

1. Review this PR and use one identified clean source checkout containing merged WP50 diagnostics and matching Python consumer. Run the offline commands above; record source head, commands, exits and warnings. A source test pass is not proof that a separate publication matches it.
2. Under separate authorization for required preparation/resources, create a new owned temporary preparation from this same checkout. Do not reuse WP47/private history. The existing command is `python -B eng/docker/release-1.15/wp39_local_history.py --action Prepare --directory <NEW-OWNED-TEMP-DIRECTORY>`; it uses Docker/cached inputs and is **not executed/authorized by WP51**. Stop if cached prerequisites unavailable, never pull/substitute silently.
3. Verify that source, actual published Worker assembly/dependencies, workflow and prepared image/context identities all match the reviewed build. Record only nonsecret build/file identifiers. Confirm exact Worker path used by worker; do not combine old publish directories and new Python or vice versa. Stop on identity uncertainty. Existing code does not do this for the operator.
4. Verify owned path resolution and ACL, destination absent (bundle and import receipt), no existing private-state/database/session conflict, expected image/manifest ownership and valid unexpired TLS identities. Check only resources covered by the later authority; do not clean/reset/delete unrelated resources. None of these live preparation checks ran here.
5. Obtain a **new explicit single-attempt owner authorization** before any provider credential/presence read or send. WP47 approval is consumed. Target remains Twelve Data time_series, BTC/USD first then ETH/USD; interval1min/outputsize1000/timezoneUTC/orderASC, no pagination or retry, <=24h and15s grace. Unresolved minute-volume availability/base-asset semantics must remain disclosed; fake tests cannot resolve them. No new licensing or provider-switch gate.
6. Only inside that separately approved operation, use the existing transient private process channel/authorized retention and cleanup procedure; never place keys in command arguments, logs, URL, files, Git, Docker or browser. This document neither reads nor prepares that channel.
7. Existing live entry, shown for identification only: `python -B eng/docker/release-1.15/wp39_local_history.py --action Acquire --directory <VERIFIED-FRESH-PREPARATION> --execute-provider-approved --base-asset-volume-confirmed`. Do not run it based on this checklist, tests, PR creation or merge. Lower-level dotnet invocation bypasses Python prechecks and is not the operator recommendation.
8. Stop after the first failure; no repeat invocation, retry, fallback or second symbol after BTC failure. Report the sanitized version1 fields only: version/event/failedStage/instrument/httpStatus/requestsStarted/responsesReceived/outcomeUnknown/failureCode/bundlePublished. BTC volume failure expects1/1 and HTTP200; ETH failure2/2 only if BTC completed. Transport may have null HTTP and started>received. Do not invent receipt counts, subtype or status.
9. Treat bundlePublished=false as no usable two-pair history, not as proof of no provider charge. Credits unknown unless separately evidenced; outcomeUnknown reflects response accounting, not billing. Preserve owned failure evidence privately without raw provider excerpts. Do not read past private responses for diagnosis.
10. Even success means AcquiredNotImported only; separate authentic provenance/import/readiness/snapshot/browser evidence is required before claiming a working chart. Do not label fixtures authentic or silently proceed to Import/Start/Verify. Return the sanitized outcome to the owner and keep all flags truthful.

## Changed paths and operational exclusions

Only three paths change: the existing `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs`, this new `docs/roadmap/release-1.15/prompters/release-1.15-wp51-twelvedata-acquisition-readiness.md`, and append-only `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`. No production implementation changes or repeated general provider research.

No market-data/DNS/HEAD/account/credit/capability requests, support contact, messages to others, credentials/presence checks, private response/bundle reads, Docker/Azure/registry operations, deployment or actual Acquire/Import/Start/Verify/browser operation. Owner changes/untracked files/private artifacts/resources preserved. WP47 subtype/credits and exact Twelve Data minute volume availability/base-unit/feed behavior remain unknown. No public/production qualification implied. PR remains OPEN/UNMERGED.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
