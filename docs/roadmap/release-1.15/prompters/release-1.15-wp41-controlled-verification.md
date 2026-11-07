# WP41 - controlled authentic acquisition and local verification

Status: **ONE_APPROVED_ATTEMPT_FAILED_NO_BUNDLE**. Phase A completed; Phase B executed once on 2026-11-07 after separate explicit owner approval. Phase C NOT RUN.

## Baseline and actual scope

PR #428 verified MERGED at `7c92eb98b8e9fe7d204375cf43c3e11f3f68a5e7`, identical to fetched origin/main. New isolated branch `docs/release-1.15-wp41-controlled-verification` began clean. Effective AGENTS, LF policy and WP39/WP40 runbooks reviewed. Owner dirty checkout, earlier private preparations and unrelated workloads preserved. No production code, dependencies or frozen contracts changed. All new/modified text is UTF-8/LF/no BOM.

WP40's earlier approval checkpoint remains historical evidence, not permission. Its certificate was not reused; a fresh owner-private preparation was created. This task does not reconstruct the consumed WP39 attempt or infer its cause/credits.

## Phase A evidence

| Check | Actual result |
| --- | --- |
| Fresh existing Python Prepare | Exit 0, PREPARED_NOT_IMPORTED; cached-only restore/publish and two local Docker builds |
| Toolchain | .NET SDK 10.0.103, Python 3.13.15, Docker 29.7.2 Linux/x86_64 |
| Inputs | Existing preparer verified pinned cached ASP.NET/Python images and dependency ID; empty-feed package-cache restore, no downloads/pulls |
| Ownership | Fresh temporary directory with inheritance removed and owner/SYSTEM full-control entries; identities/private absolute paths omitted |
| Channel | Exact Windows User registry setting NAME present, value never read; validity is unverified |
| Worker assembly SHA-256 | `8de33bd6d89645f6f72f68c2a918ff7d094d0bc1edb9d9990b0f8b6368b74679`; matches fresh Release build |
| Infrastructure assembly SHA-256 | `fb5626772944c86cb4c924173c708ebfce181d11534e808382dde94ccb930a42`; matches fresh Release build |
| Research image | `sha256:6f6e96216027adc023a5d8dfd08a0ad3d11a80ae790320d619fce0767dd2287a`; owned tag matches private manifest |
| UI image | `sha256:12379fc76fad4f08bcc46f7ce9a7ea11208864c6abb2a2f4b55711afb70d08e8`; owned tag matches private manifest |
| Actual public certificate | SAN wp07-research, validity 2026-11-07 19:40:22 UTC to 2026-11-08 19:41:22 UTC; valid at check 2026-11-07 19:42:15 UTC. Private PFX not read |
| Session conflicts | Zero session-labeled containers and zero owned state volumes |
| Artifact presence | Manifest present; bundle, import receipt, private SQLite and qualification absent |
| C# focused tests | 84 passed, 0 failed/skipped, exit 0 |
| Python diagnostic/workflow | 22 passed, exit 0 |
| Chart/client regressions | 61 passed, exit 0 using installed project virtual environment |
| Provider / Azure / registry publication | Zero operations; no DNS/HEAD/HTTP provider probe |
| Authentic import/start/verify/browser | NOT RUN; no authentic bundle |

Freshly published Worker was invoked with approval marker and provider process channel removed from its child environment, empty stdin, through the actual Python worker and strict bounded parser. The expected negative probe returned:

```json
{"version":1,"event":"WP39_ACQUIRE_FAILED","failedStage":"preflight","instrument":null,"httpStatus":null,"requestsStarted":0,"responsesReceived":0,"outcomeUnknown":false,"failureCode":"MISSING_APPROVAL","bundlePublished":false}
```

This is not an Acquire action or live provider failure. Diagnostic bounds/invalid schema, fake HTTP/transport/cancellation, publication/no-overwrite, secret canaries and synthetic provenance were requalified by existing tests. No real key or provider response was used. No additional fake end-to-end Docker/browser run was performed.

## Reproduction commands

Run from isolated source with existing installed tools; no PowerShell execution-policy bypass. `$prepared` denotes the new private temporary preparation, never an old destination.

```powershell
$prepared = Join-Path $env:TEMP 'aiq-wp41-prepared-20261107'
python -B eng/docker/release-1.15/wp39_local_history.py --action Prepare --directory $prepared
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source (Join-Path $prepared 'empty-feed') -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
# Use existing project virtual-environment Python (as executed):
python -B -m unittest test_research_chart test_research_snapshot_client -q
git diff --check
```

All commands completed exit 0. Actual chart/client invocation used the existing project virtual-environment interpreter rather than default Python. Existing SyntheticSnapshotQualificationTests warnings CA1711, SYSLIB1045, CA1861, CA2219 and Streamlit bare-mode ScriptRunContext notices appeared; no new failures/skips. Separate changed-text byte checks verify LF/no BOM. No new wrapper or dependencies were needed.

## Owner approval checkpoint - no live authority yet

Request authorization for exactly **one** existing WP39 Acquire invocation: at most two sequential HTTPS GETs to `https://api.twelvedata.com/time_series`, BTC/USD then ETH/USD, `interval=1min`, `outputsize=1000`, `timezone=UTC`, `order=ASC`. Existing completed-observation latest-24-hour window and 15-second grace remain. No retry, pagination, fallback, extra endpoint/account/capability probe, Azure call or registry publication. Actual credits and availability remain unknown. BTC/ETH base-asset volume units remain owner-confirmed, not independently measured.

Before that separately approved operation, recheck build/manifest identity, private ACL, certificate validity, absent bundle and conflicting session. If expired, prepare a new private destination rather than overwriting artifacts. Only inside approved Acquire may the existing Windows User provider setting be read and temporarily mapped to process AIQ_WP39_PROVIDER_KEY, restoring the prior channel in finally. Never put the value into args, output, evidence, Docker, URLs or browser. No CLI flag or prior approval is sufficient authority.

If the single attempt fails/has unknown outcome, stop immediately, retain private artifacts and report only fixed diagnostic fields (failedStage, instrument, httpStatus, requestsStarted, responsesReceived, outcomeUnknown, failureCode, bundlePublished), child exit and artifact presence. No raw output/body/header/exception/private paths; no retry or import. Started sends do not establish provider receipt or consumed credits; unknown counts/publication stay null.

Only complete ObservedProvider acquisition passing existing hash/provenance validation permits existing Import -> Start -> Verify -> Stop. Conditional browser evidence must cover BTC/ETH UTC/OHLCV/volume/counts/gaps, authenticated TLS snapshot generation/receipt identity, continuity/readiness, switching, 100/300/1000 presets, zoom/Refresh retention and observed-provider labeling. No fixture substitution or importer modification. These conditional steps are not current evidence.

## Stop, preservation and readiness

No session was started; no processes require stopping. Retain owned private preparation and matching local images for review. Later cleanup must match manifest tag/image identity and remove only confirmed owned resources, never prune or delete historical state. If a subsequent approved session starts, use existing Python Stop action on that preparation; preserve state/volume. No private artifacts enter Git.

Mutation inventory: this new WP41 runbook and append-only WP41 status in `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`. Leave new PR OPEN/UNMERGED.

Implemented/offline-qualified is not authentic local or public acceptance. `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Separately approved Phase B outcome

After Phase A and PR #429, owner explicitly replied "approved.continue" to the exact single-invocation checkpoint. Immediately before execution, clean PR head `e810503d5d17180556bca4801403f70f4b23c73d`, published assembly hashes, image/manifest identities, protected directory ACL, actual TLS validity, absent bundle/receipt/SQLite/qualification and absence of session containers were rechecked successfully.

Exactly one existing Acquire invocation executed with the established Windows User provider setting mapped temporarily to the process channel; finally restored its previous value. No credential or raw provider body/header/exception was printed or retained as review evidence. The only reported failure envelope was:

```json
{"version":1,"event":"WP39_ACQUIRE_FAILED","failedStage":"response_parse","instrument":"BTC/USD","httpStatus":200,"requestsStarted":1,"responsesReceived":1,"outcomeUnknown":false,"failureCode":"INVALID_PROVIDER_PAYLOAD","bundlePublished":false}
```

Acquire exit: **1**. Invocations: **1**. One send and one HTTP response observed for BTC/USD; ETH/USD was not requested. The HTTP 200 response did not pass the existing provider-payload parser. These fields do not identify the raw content, exact payload defect, key/account condition or credits consumed; no further diagnosis or response reconstruction was attempted. HTTP success is not acquisition success.

Presence-only checks after failure: bundle **absent**, import receipt **absent**, private SQLite **absent**, qualification **absent**. Import, Start, Verify and browser checks **NOT RUN**. No retry, provider probe, Azure operation, registry publication or production-code change. Preparation/private evidence and owned images preserved; no session was started. This approval is consumed.

The Phase A table above is historical offline evidence; its zero-provider row applies only to Phase A. Overall WP41 now includes the one approved send/response reported here. The earlier approval proposal remains a record, not permission for another attempt. New investigation or a new provider attempt requires further owner direction; no automatic retry is authorized.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. **STOPPED AFTER ACQUISITION FAILURE - OWNER REVIEW REQUIRED**.
