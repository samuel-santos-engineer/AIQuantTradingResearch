# Release 1.15 WP39 - local authentic historical observation workflow

## Status and boundary

Baseline main `fd8c8e28dcf8c4897620d64909c13f19c181ec92` (merged PR #426), isolated WP39 branch. **ONE_APPROVED_ACQUISITION_ATTEMPT_FAILED_NO_BUNDLE; OFFLINE DIAGNOSTICS FOR REVIEW.** Implementation and offline qualification are progress, not authentic-data import or public qualification. One earlier approved acquisition invocation failed exit 1 without a bundle/import; its actual request count, HTTP status and credit consumption remain unknown. The diagnostic follow-up made zero live provider/DNS/HTTP probes and zero Azure operations. Historical offline checkpoints below do not authorize a repeat attempt. No authentic rows imported, registry publication or deployment. WP38 remains a separate conspicuously synthetic workflow. All authored text is UTF-8/LF/no BOM.

| Stage | Actual state |
| --- | --- |
| Local acquisition/import implementation | Implemented for owner review |
| Offline contract/fixture qualification | Passed, results below |
| Actual provider acquisition/import | One earlier approved invocation consumed, failed exit 1; no bundle/import, actual issued requests unknown; no retry |
| Local end-to-end authentic qualification | NOT RUN; do not infer from fixture smoke |
| Azure public qualification | Deferred/unresolved; NOT RUN |

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Inventory and reuse

The existing `TwelveDataMinuteCandleSource` already uses the HTTPS time_series endpoint, header authentication, exactly BTC/USD or ETH/USD, one-minute UTC metadata, ascending order, maximum 1,000 rows and 1 MiB actual response bytes, five-second per-call deadlines, no redirects/proxy, decimal/OHLCV validation, timestamp uniqueness/order and completed-candle filtering. It rejects unknown base-asset volume capability before requesting. Forming candles are validated then excluded; no missing observation is synthesized. Provider errors, invalid/missing volume, invalid timezone, invalid dates and malformed encoding fail generically without retries.

The existing `ResearchStateInitialization` imports both instruments after parsing every row, validates completed UTC/OHLCV/source, uses the unchanged single-writer SQLite/checkpoint/watermark/research pipeline, and allows exact retry without witness changes. It rejects replacement of incompatible initialized history. Continuity gaps start new segments and reset readiness; regimes are unavailable before 401 consecutive candles. The existing D4 transformed snapshot and UI retain one-minute 100/300/1,000 presets, indicators, volume, viewport retention and educational price disclosure. No raw candle API or alternate calculator was added.

Actual gaps closed here: a one-shot bridge from the existing provider source to the existing import format; associated provenance/integrity evidence; a local owner CLI; explicit fixture-versus-observation labeling; and offline contract/negative coverage. The adapter itself required no production rewrite. There is no pagination/backfill loop in this bounded WP39 workflow.

## Acquisition and provenance contract

`--research-acquire-local` is a new explicit operator mode, never normal service startup. It requires a separate approval marker and bounded stdin object containing providerKey and baseAssetVolumeConfirmed. No credential appears in an argument, response, URL, image layer or diagnostic. The private process channel is read only in the explicitly selected, approved Acquire operation; Plan/Prepare do not look for a provider key. The Python parent strips that channel from the C# child environment and sends the key over stdin; C# supplies `Authorization: apikey ...`. Original owner environment is preserved. Managed strings/process memory are not a secure enclave: the owner must protect the operator session and clear its private provider channel afterward. No Azure research key is obtained or reused.

One call per instrument, sequentially, maximum two calls total, no retries or pagination. Accepted rows must be within the 24 hours preceding the UTC acquisition anchor and completed at least 15 seconds before that anchor. The request asks for latest 1,000 rows; actual counts may be smaller because of forming bars, missing coverage or capability limits. Both instruments must have at least one validated row before a bundle is published. A failure on the second symbol publishes no partial bundle. Existing destination is rejected **before any request**, preventing accidental repeated quota consumption. No provider account capability or free-history entitlement is presumed.

A newly published private bundle contains `observations.json` in the frozen initializer format and `provenance.json`: fixed provider/endpoint/interval, original symbols, UTC observation/acquisition times, first/last opening bounds, row/gap counts, explicit SyntheticFixture versus ObservedProvider kind, AcquiredNotImported status and SHA-256 of exact import bytes. Publication uses a fresh staging directory followed by a same-parent rename; files are private on Unix. Verification binds hash, symbol identity, counts and UTC bounds before import. It is local acquisition-chain evidence, **not a provider digital signature or tamper-proof proof against a malicious owner who controls both files**. Timestamps alone never establish origin. Synthetic test bundles are rejected by the operational import workflow; tests can inspect them only with an explicit test-only option.

Import writes a separate receipt only after the unchanged initializer succeeds. Receipt binds import hash and generation, and has status ImportedAwaitingSnapshotVerification. Existing private state is never silently deleted or replaced. Start requires ObservedProvider evidence/receipt agreement and a session-owned volume labeled with the matching import identity. A failed/partial state-volume copy is preserved for review; the existing API snapshot readiness guard must still succeed, and no automatic destructive recovery occurs. Do not run Import while this session's research container is running; Stop first. Re-import is limited to the existing initializer's exact-idempotent semantics, not incremental backfill into an already populated dataset.

Verify fetches both real running transformed snapshots through the existing authenticated TLS client and compares generation, counts and first/last timestamps against receipt/acquisition evidence. Only that separately executed, successful observed-provider path writes local qualification with AUTHENTIC_DATA_NOT_IMPORTED=False. This action has **not** run in WP39; the repository/final report remain True. All public/runtime flags remain false even after a future local verification.

## Owner commands - preparation is offline

Windows PowerShell 5.1 thin wrapper delegates to the shared Python workflow. Existing Docker Desktop Linux/amd64, .NET 10, cached NuGet packages and the same pinned cached ASP.NET/Python/dependency images as WP38 are required. No package installation, pull, image push or cloud service is performed. Prepare requires a fresh directory under the operator's temporary directory; it restricts Windows ACLs to current owner/SYSTEM (Unix 0700), creates separate disposable local service/HMAC keys and one-day TLS material, and builds local-only research/UI images. No SQLite dataset or provider rows are created by Prepare. The image build context contains published research binaries and allowlisted public UI sources, not runtime secrets, provenance or private history. Re-prepare a fresh directory after TLS expiry.

```powershell
$local = Join-Path $env:TEMP 'aiq-r115-wp39-owner'
.\eng\docker\release-1.15\Invoke-Wp39LocalHistory.ps1 -Action Plan
.\eng\docker\release-1.15\Invoke-Wp39LocalHistory.ps1 -Action Prepare -Directory $local
```

Equivalent commands actually exercised by the agent (without changing the host PowerShell execution policy):

```powershell
python -B eng/docker/release-1.15/wp39_local_history.py --action Plan
python -B eng/docker/release-1.15/wp39_local_history.py --action Prepare --directory "$env:TEMP/aiq-wp39-prepared"
```

The thin wrapper parses under Windows PowerShell 5.1; runtime execution remains blocked by the agent host's existing policy, as in WP38. No bypass is needed: direct Python is the same implementation, not a different orchestration framework. The Docker launcher remains `wp07_local_historical_chart.py`; research stays private on its internal network and UI publishes only `127.0.0.1:8501` (or selected free port). The UI has no SQLite or private TLS-key mount.

## Historical provider proposal - single-attempt authority consumed

Provider: **Twelve Data**. Exactly two sequential HTTPS GET requests, one per symbol, fixed endpoint and nonsecret query:

```text
https://api.twelvedata.com/time_series?symbol=BTC%2FUSD&interval=1min&outputsize=1000&timezone=UTC&order=ASC
https://api.twelvedata.com/time_series?symbol=ETH%2FUSD&interval=1min&outputsize=1000&timezone=UTC&order=ASC
```

Date span: the **24-hour UTC interval ending at the recorded execution anchor**, retaining at most the latest 1,000 completed one-minute observations per instrument, with 15-second completion grace. No arbitrary historical paging, retries or extra capability/quote requests. Actual first/last boundaries are recorded from the response, not inferred. This is a latest-window attempt, not a promise of complete 24-hour coverage. The owner can review Plan immediately before choosing the bounded attempt.

Expected quota cost is **two API credits**, based on the provider's current [credit documentation](https://support.twelvedata.com/en/articles/5615854-credits), which describes time_series as one credit per symbol. Actual account limits, remaining daily/minute credits, history availability and base-asset volume semantics are unverified. A 401/403/429, malformed metadata, unknown volume capability or insufficient valid data stops without retry, fallback fixtures or plan changes. Header authentication follows the provider's [documented method](https://twelvedata.com/docs/markets/market-state). This is operational data/capability verification, not a new licensing milestone.

Only after separate owner approval and an already available private process provider channel, proposed bounded one-shot command:

```powershell
.\eng\docker\release-1.15\Invoke-Wp39LocalHistory.ps1 -Action Acquire -Directory $local -ExecuteProviderApproved -BaseAssetVolumeConfirmed
```

The flags must not be treated as approval granted by this document. The volume flag preserves the adapter's existing capability requirement; do not set it on an assumption. Do not type a credential literal into a shell command, clipboard, chat or screenshot. The workflow does not create a provider account, retrieve an Azure key, inspect vaults or request the key from Codex. If the already supported private process channel is absent, stop with a generic channel-unavailable result. No live attempt occurred here.

Future subsequent owner steps, only after a successful approved acquisition:

```powershell
.\eng\docker\release-1.15\Invoke-Wp39LocalHistory.ps1 -Action Import -Directory $local
.\eng\docker\release-1.15\Invoke-Wp39LocalHistory.ps1 -Action Start -Directory $local -Port 8501
# Open http://127.0.0.1:8501 ; BTC/USD / ETH/USD, retained-candle presets, zoom/Refresh.
.\eng\docker\release-1.15\Invoke-Wp39LocalHistory.ps1 -Action Verify -Directory $local
.\eng\docker\release-1.15\Invoke-Wp39LocalHistory.ps1 -Action Stop -Directory $local
```

Stop preserves state/images for the next Start. Cleanup must inspect matching session/owner labels before deleting only owned stopped containers/networks/volumes/local tags; never remove a private dataset as automatic error recovery. The qualification resources used below were separately verified and removed after stopping both principals with exit 0. Prepared manifests in those cleaned test directories are evidence, not ready-to-start sessions; use a new Prepare for an owner attempt.

## Actual offline qualification

- **51 C# tests passed**, exit 0: ResearchLocalHistoryTests, TwelveDataMinuteCandleSourceTests, ResearchStateInitializationTests. Covers both symbols, gap counts, atomic failure on second symbol, no overwrite/retry, cancellation, no approval/no secret read, fixture rejection, import/hash/summary tampering, initializer/exact retry, provider 301/401/429/500, malformed metadata/timezone/UTC, duplicate/descending/unaligned timestamps, invalid OHLC, non-completed candles/grace, input byte limits, readiness and witness integrity. All provider handlers are synthetic offline fakes; no result is described as an authentic import.
- **8 Python workflow tests passed**, exit 0: Plan/no I/O, missing approval/capability/channel never invoking acquisition, fake key over stdin instead of arguments/environment, failure preserving the owner channel, fixture rejected by authentic import, and no overwrite of an existing preparation directory.
- **61 Python chart/client tests passed**, exit 0, existing project virtual environment: includes observation/unknown/fixture provenance precedence and the retained WP38/chart/snapshot negative suite. Synthetic mode always wins over an observation label.
- Local Docker/API smoke using **WP38 synthetic fixtures only** passed: exact anonymous 200 Alive, 1,000 BTC and 1,000 ETH rows, ordered/unique UTC and OHLC invariants, four negative cases. Real installed Chrome passed all six existing categories: labeled BTC chart/volume, presets, ETH switch, pointer zoom, Refresh retention, no browser upstream/key exposure. Evidence/screenshot: `$env:TEMP/aiq-wp38-browser-1w3e8q5a`. Initial 300 range `[2026-10-01T11:46:00Z,2026-10-01T16:46:00Z]`; 100 range starts `15:06:00Z`, 1,000 starts `00:05:00Z`, both end `16:46:00Z`; manual range `[2026-10-01 13:46,2026-10-01 14:30:46.0104]` on the UTC axis.
- WP39 Prepare completed offline: published/built local images, restricted private directory, disposable independent runtime configuration/TLS, no dataset. Import with a missing acquisition bundle rejected with exit 1 and **no research.sqlite creation**. Real Acquire/Import/Start/Verify with observations NOT RUN.
- PowerShell 5.1 AST zero errors. Existing unrelated C# analyzer warnings in SyntheticSnapshotQualificationTests (CA1711/SYSLIB1045/CA1861/CA2219) and bare-mode Streamlit warnings remain; no new dependency or policy exception. UTF-8/LF/no BOM and git diff --check required before commit.

Exact commands:

```powershell
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -v
# Existing project Python environment; no installation:
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
python -B -m unittest test_research_chart test_research_snapshot_client -q
python -B eng/docker/release-1.15/wp38_prepare_local.py --output "$env:TEMP/aiq-wp39-fixture-regression"
python -B eng/docker/release-1.15/wp38_prepare_local.py --output "$env:TEMP/aiq-wp39-fixture-regression" --action Start --port 18539
python -B tests/runtime/release_1_15/wp38_api_smoke.py --directory "$env:TEMP/aiq-wp39-fixture-regression"
python -B tests/runtime/release_1_15/wp38_browser_qualification.py --directory "$env:TEMP/aiq-wp39-fixture-regression" --url http://127.0.0.1:18539
python -B eng/docker/release-1.15/wp38_prepare_local.py --output "$env:TEMP/aiq-wp39-fixture-regression" --action Stop --port 18539
```

## Exact changed paths and remaining limits

1. `src/AIQuantTradingResearch.Worker/ResearchLocalHistory.cs` - bounded operator acquisition, runtime preparation and provenance verification.
2. `src/AIQuantTradingResearch.Worker/Program.cs` - explicit prepare/acquire/verify operator modes only.
3. `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs` - offline bundle/provenance/import/cancellation tests.
4. `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataMinuteCandleSourceTests.cs` - extra sequence/OHLC rejection cases, unchanged production adapter.
5. `eng/docker/release-1.15/wp39_local_history.py` - owner Plan/Prepare/Acquire/Import/Start/Verify/Stop and existing launcher delegation.
6. `eng/docker/release-1.15/Invoke-Wp39LocalHistory.ps1` - PowerShell 5.1 thin wrapper.
7. `eng/docker/release-1.15/wp07_local_historical_chart.py` - pass fixed local provenance label; same orchestration.
8. `python/presentation/research_chart.py` - observation/unknown disclosure while preserving fixture label.
9. `python/presentation/test_research_chart.py` - truthful provenance precedence regression.
10. `tests/runtime/release_1_15/test_wp39_local_history.py` - fake-only workflow tests.
11. `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` - separate offline progress and authentic-import/public gates.
12. This runbook.

No actual response/account capability or end-to-end authentic path has been qualified. Cached-image/package preparation is reproducible on this qualified local environment, not a new clean-machine dependency installer. The private evidence chain assumes the owner protects the local directory; it is not independently signed provider evidence. Exact-idempotent import is supported; historical extension/correction uses the existing WP03 ingestion service, not destructive re-import. Partially initialized state remains private and blocked for review; no unsafe automatic reset was introduced. Azure role/storage/transport/quota causes remain deferred and unresolved, not failures of the local fixture slice. PR stays OPEN/UNMERGED.


## Owner-approved continuation - 2026-11-03

The owner explicitly approved continuing the bounded two-request provider attempt described in PR #427. That approval is retained; another approval of the same attempt is not required. A presence-only check of the supported process channel returned **PROVIDER_CHANNEL_AVAILABLE=False**. No key value was printed, read from another store, requested in chat or obtained from Azure. No provider request, import, image rebuild or Docker startup ran in this continuation.

Operational status: **PROVIDER_ATTEMPT_APPROVED_BLOCKED_PRIVATE_CHANNEL**. Earlier AWAITING_OWNER_PROVIDER_APPROVAL entries describe the implementation-review checkpoint; they do not revoke the subsequent approval. The existing requirement to confirm base-asset-volume semantics remains unverified, and no claim of actual account/history capabilities is made.

Next step: make the already-supported private provider process channel available to the process executing the documented owner command, or execute that command in an owner-controlled session where the channel is privately configured. Do not paste the key into chat, a command literal, clipboard, logs or screenshots. This continuation did not search other environment names, credential stores, private files or cloud configuration for a key. Existing cleaned qualification manifests are not runnable sessions: a new Prepare is needed before an owner acquisition.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. PR remains OPEN/UNMERGED.


## Approved attempt continuation - 2026-11-07

Current checkpoint: **ONE_APPROVED_ACQUISITION_ATTEMPT_FAILED_NO_BUNDLE**. The previous private-channel blocker is resolved. A presence-only check found the exact owner-authorized Windows User setting `TwelveData__ApiKey`; no credential value entered evidence. The owner also explicitly reported that volume is verified as BTC/ETH base-asset units. That is owner-supplied capability confirmation, not independently measured evidence from this attempt.

PR #427 was verified OPEN at head `2fe09782544421867d294e3ab5d4dd44a2cbcf97` before execution, with a clean isolated worktree. A fresh offline Prepare completed, including new one-day local TLS material and disposable service credentials, using cached image inputs and existing packages. No fixture state was seeded into this observation attempt.

The approved Acquire command was invoked **once**, with execute-provider-approved and base-asset-volume-confirmed flags. The named Windows setting was mapped privately into the acquisition process channel; the provider value was not printed, put into shell arguments, persisted, or passed to Docker/browser. The temporary process channel was restored in finally; the persistent Windows setting was not changed. No Azure key, unrelated credential, cloud configuration or owner key-generation script was read.

Observed result: generic `WP39_BLOCKED; preserve local state for review; no automatic retry`, **acquisition exit 1**. Presence-only artifact checks confirmed: preparation manifest present; **no published bundle, no import receipt, no private research.sqlite, no qualification file**. No import, chart startup or authentic snapshot/browser verification followed. The generic workflow discards the underlying child diagnostics; HTTP status, failure stage, exact cause, actual request count and quota consumption are **UNRESOLVED**. At most two provider requests were permitted by code, but the evidence does not establish whether zero, one or two were sent. Do not call this a provider rejection, network failure, missing-volume response or invalid-key result without evidence.

**No retry or additional provider call was made.** A new live attempt requires separate owner approval; first improve bounded sanitized stage/status reporting offline and test it with fakes so a further attempt can be attributable without leaking credentials or private observations. This continuation changes evidence only, not the reviewed acquisition implementation.

Cleanup removed only the two preparation image tags after exact image-ID checks. No runtime containers had been started. Restricted private attempt artifacts remain for owner review; the cleaned manifest must not be treated as a ready-to-start session. Existing Windows settings, cached input images and unrelated workloads were preserved. No Azure requests, registry publication, deployment, PR merge or tests beyond the operational preflight/artifact checks ran in this continuation. UTF-8/LF/no BOM and git diff --check checked before the evidence commit.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. PR remains OPEN/UNMERGED.


## Offline-only sanitized acquisition diagnostics - 2026-11-07

Authority: continue existing OPEN/UNMERGED PR #427 from verified head `248704a408443237893a6d973b193f0445a6632c`. This follow-up accessed **no real credential or secret setting**, made **zero provider requests or DNS/HTTP probes**, and performed **zero Azure, registry publication or deployment operations**. Tests use fake keys, injected HTTP handlers and owned local Python subprocess stubs. No live Acquire was invoked. Existing prepared worker binaries were not modified; any future separately authorized attempt needs a fresh Prepare of the reviewed code. No historical private attempt directory was read or modified. No architecture, API/snapshot, provider source/query, storage, indicator or UX change was required.

**Historical fact remains unchanged:** one owner-approved acquisition invocation was consumed, exited 1, and published no bundle/import. No retry followed. Its failure stage, HTTP status, actual request count and quota/credit consumption remain UNKNOWN. New diagnostics cannot retroactively reveal any of those facts.

**Do not run another live Acquire until the owner separately authorizes a new bounded attempt. CLI flags and a configured credential are not authorization.** Plan now reports NEW_OWNER_PROVIDER_APPROVAL_REQUIRED; this follow-up does not renew the consumed authority.

### Operator failure contract (version 1)

An acquisition failure emits one JSON object on operator stderr. No arbitrary C# stderr/stdout, exception text, stack trace, URI, header, environment, stdin, private path, provider body or observation is forwarded. The browser receives no diagnostic fields. A synthetic HTTP example from offline tests:

```json
{"version":1,"event":"WP39_ACQUIRE_FAILED","failedStage":"http_response","instrument":"BTC/USD","httpStatus":429,"requestsStarted":1,"responsesReceived":1,"outcomeUnknown":false,"failureCode":"HTTP_RATE_LIMITED","bundlePublished":false}
```

Exactly ten fields, fixed event/version, allowlisted stages/codes and instrument null/BTC/USD/ETH/USD. C# counters are integers in the bounded two-request attempt; Python accepts only integers (not booleans), `0 <= responsesReceived <= requestsStarted <= 2`, observed numeric HTTP status 100..599 or null, fixed booleans, and a consistent unknown-outcome flag. Duplicate fields, extra fields, unsupported versions, invalid types/scalars, secret-bearing free text, inconsistent HTTP codes/counters, malformed UTF-8/JSON and oversized envelopes are replaced, never forwarded.

| Stage | Interpretation and principal fixed codes |
| --- | --- |
| preflight | MISSING_APPROVAL, MISSING_PRIVATE_CHANNEL (Python), VOLUME_UNCONFIRMED, INVALID_DESTINATION, DESTINATION_EXISTS, INVALID_PRIVATE_INPUT; zero only when established before child/send |
| child_launch | CHILD_EXECUTABLE_UNAVAILABLE or CHILD_LAUNCH_FAILED; no raw OS error/path |
| private_input | INVALID_PRIVATE_INPUT or VOLUME_UNCONFIRMED; malformed/oversized/private schema rejected before request |
| request_start | SendAsync invoked; TRANSPORT_ERROR, TIMEOUT or CANCELLED if no response was observed |
| http_response | Exact received status; HTTP_UNAUTHORIZED (401), HTTP_FORBIDDEN (403), HTTP_RATE_LIMITED (429), HTTP_SERVER_ERROR (5xx), HTTP_OTHER; INVALID_RESPONSE_HEADERS or post-header transport failure |
| response_parse | INVALID_PROVIDER_PAYLOAD (JSON/shape/encoding) or INVALID_PROVIDER_METADATA (symbol/interval/UTC metadata) |
| observation_validation | INVALID_OBSERVATION (UTC/order/precision/OHLCV/window), INVALID_VOLUME, ZERO_ACCEPTED_CANDLES |
| bundle_publication | BUNDLE_PUBLICATION_FAILED or cancellation; no automatic reset/retry |
| unknown | UNKNOWN, CHILD_NONZERO_EXIT, CHILD_TIMEOUT, CHILD_DIAGNOSTIC_INVALID or CHILD_OUTPUT_OVERSIZE where reliable child facts are unavailable |

Requests are counted immediately at the actual `HttpClient.SendAsync` invocation boundary, **not** on entering the instrument loop. That proves a send was initiated, not that a packet reached the provider or a credit was charged. Responses are counted only after an actual response status is observed, before validating headers/body. The last status is cleared before a new symbol request so a first-symbol 200 cannot masquerade as the second symbol's result. A body read failure after headers retains the actually observed status. No HTTP category is interpreted as proof of an invalid key, exhausted account quota or other unobserved account condition.

For a send with no observed response, `outcomeUnknown=true`, status null; a second-symbol transport failure after one successful response reports requestsStarted=2/responsesReceived=1. Cancellation before any send reports zero; in-flight caller cancellation or request deadline retains send counters. Missing approval does not read private input/channel. Invalid poll-request exception behavior and existing cancellation propagation are preserved.

If the parent has no trustworthy complete child diagnostic, it reports **null request/response counts**, status/instrument null, outcomeUnknown=true and **bundlePublished=null**. This is deliberately more conservative than asserting no bundle after a timeout/crash: publication might have occurred before the parent lost trustworthy output. Example of an offline fake child timeout:

```json
{"version":1,"event":"WP39_ACQUIRE_FAILED","failedStage":"unknown","instrument":null,"httpStatus":null,"requestsStarted":null,"responsesReceived":null,"outcomeUnknown":true,"failureCode":"CHILD_TIMEOUT","bundlePublished":null}
```

C# classified failures report bundlePublished=false for this attempted publication; they do not claim that an unrelated pre-existing directory was removed. No diagnostic code creates a receipt, SQLite database or qualification artifact. Destination-not-overwritten checks precede requests; second-symbol rejection creates no partial bundle; publication failure cleans only its fresh pending directory and preserves existing state. Unknown parent outcomes stop without recovery or import.

### Bounded subprocess handling and credential isolation

Acquisition uses the existing worker command/stdin channel, with a specialized parent I/O path. Private input is at most 4 KiB. Child stdout is discarded to DEVNULL. A bounded reader holds at most **2,049 stderr bytes** to detect the **2,048-byte** envelope limit; oversized output causes termination of only the owned subprocess and an unknown-count parent result. No output spool or new diagnostic artifact is written. Process wait uses the existing 40-second acquisition timeout, owned-child kill/wait and bounded reader joins; there is no request/process retry. Non-acquisition commands retain their existing behavior.

The parent reconstructs only validated fixed scalars; it never prints a prefix/suffix of malformed child output. Exceptions propagated to the acquisition CLI contain only fixed failure metadata, and the CLI prints exactly one sanitized JSON line instead of a traceback or a second generic error. An optional typed adapter callback carries only enum event, instrument enum and observed status, never provider content. RunAsync's injectable offline handler keeps successful test bundles explicitly SyntheticFixture; no fake response is described as authentic.

### Executed offline evidence

| Suite/check | Result |
| --- | --- |
| Focused C# acquisition, adapter and initializer suites | **84 passed**, 0 failed, 0 skipped, exit 0 |
| Python WP39 workflow/diagnostic suite | **22 passed**, exit 0 |
| Existing chart/client fixture regressions | **61 passed**, exit 0; no UI source change |
| Real provider, real-key Acquire, quota/DNS/capability probes | **NOT RUN**, zero in this follow-up |
| Docker/browser smoke in this follow-up | Optional; **NOT RUN**. Earlier fixture/browser evidence remains historical, not a new run |
| PowerShell policy/dependencies | No script execution or bypass, no installs/updates |

Commands actually run from the isolated PR branch:

```powershell
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -v
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
# Existing project virtual-environment interpreter; no dependency installation:
python -B -m unittest test_research_chart test_research_snapshot_client -q
git diff --check
```

New negative cases cover fake HTTP 401/403/429/500/301, transport failure, true short request deadline, in-flight and pre-send cancellation, body-read failure after status 200, missing approval/private channel, malformed/oversized stdin, invalid JSON/metadata/timezone, duplicate and unaligned UTC, invalid OHLC/decimal/volume, zero accepted candles, second-symbol HTTP/transport failures, publication filesystem failure and existing-destination preservation. Successful fake acquisition preserves private SyntheticFixture provenance and exactly two sends. Owned local fake subprocesses test missing executable, nonzero without a diagnostic, timeout/kill, malformed and overflowing secret-bearing stderr, discarded success stdout/stderr, strict schema/version/types/duplicates and exactly one CLI failure envelope. Canary assertions cover operator output, formatted exceptions, arguments, child environment and success artifacts. No fake key is substituted for the owner's key in a live request.

Only pre-existing analyzer warnings in SyntheticSnapshotQualificationTests (CA1711, SYSLIB1045, CA1861, CA2219) and bare-mode Streamlit ScriptRunContext notices appeared; the new cancellation-parameter analyzer warning encountered during implementation was resolved by retaining the existing public signature and using a private core. Final focused tests passed. Tests used bounded disposable fixture/stub directories, cleaned in finally; no secret-bearing private attempt artifacts were staged.

### Exact diagnostic follow-up mutation inventory

1. `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs` - narrow typed diagnostic events and existing validation/error-boundary instrumentation.
2. `src/AIQuantTradingResearch.Worker/ResearchLocalHistory.cs` - allowlisted trace, observed counters, fixed JSON failure and injectable offline-handler seam; existing acquisition/public signature preserved.
3. `eng/docker/release-1.15/wp39_local_history.py` - strict bounded child-envelope parsing, bounded subprocess I/O, parent failures and single CLI diagnostic; Plan requires fresh authority.
4. `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs` - stage/status/counter/publication and canary tests.
5. `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataMinuteCandleSourceTests.cs` - actual send/deadline/cancellation/body-read instrumentation tests and exception-compatibility regression.
6. `tests/runtime/release_1_15/test_wp39_local_history.py` - fake-only parent/subprocess/schema tests.
7. `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` - current failed historical checkpoint and offline follow-up.
8. This existing runbook - contract, scope, actual evidence and retained unknowns.

Implemented diagnostics and passing local fixture tests are not authentic-data import, local authentic acceptance or public qualification. Keep PR #427 OPEN/UNMERGED for review; no future live request is granted. `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
