# WP44 - controlled authentic acquisition and local chart verification

Status: **ONE_APPROVED_ATTEMPT_FAILED_NO_BUNDLE**. Offline phase completed; one separately approved Acquire invocation executed on 2026-11-07. Conditional import/chart phase NOT RUN.

## Baseline and actual evidence

Verified PR #430 MERGED and fetched origin/main exactly `4a41bc49396d6a5bbd5db3c1afb373fbcd09e8d8`. Clean isolated WP44 worktree/branch; owner checkout and earlier private artifacts preserved. AGENTS, LF governance, merged WP39-WP43 runbooks, current diagnostic/launch workflow reviewed. No code/dependencies/contracts changed. No provider or DNS/HEAD/capability probe, credential read, Azure operation, registry publication or external data sharing.

Fresh existing Prepare exited 0, PREPARED_NOT_IMPORTED. New owner-private temporary destination, owner/SYSTEM-only grants with protected ACL. Cached pinned bases/dependency image verified by unchanged preparer; empty-feed cache-only restore, fresh C# publish and two Docker builds with --pull=false and --network=none. No old prepared images used as proof. Toolchain: .NET SDK 10.0.103; Python 3.13.15; Docker 29.7.2 Linux/x86_64.

- AIQuantTradingResearch.Worker SHA-256: `35e28a9f96d45c3ea41e0a0cb995604ee9aba10eb4454e3880859354f781b5ca`.
- AIQuantTradingResearch.Infrastructure SHA-256: `d00c7c882aa2e791052ed21d2728f24f1e8abd96a1a67ea74981640cac6f42bb`.
- research image: `sha256:195a178edd5697591cb5084f4cda3d074a1fb6cd09505d22e6eacda5d31c3513`.
- ui image: `sha256:4d8ebbf538d643e3e4421f1fa79b91da19f53b4b87f3b36679783b0eddd57856`.

Assembly hashes match the freshly compiled Release outputs. Owned image tags match private manifest. No conflicting session containers/state volumes. Manifest exists; bundle, import receipt, private SQLite and qualification are absent. No service/chart was started.

Actual public TLS certificate SAN wp07-research; valid **2026-11-07 21:26:11 UTC through 2026-11-08 21:27:11 UTC**, checked at 2026-11-07 21:28:12 UTC. Private PFX/key not inspected. Require a new private preparation if expired before a later approved operation; never overwrite old evidence.

Freshly published Worker through actual Python worker/strict parser, approval and provider process-channel markers removed from child environment, empty stdin, returned the expected bounded negative diagnostic:

```json
{"version":1,"event":"WP39_ACQUIRE_FAILED","failedStage":"preflight","instrument":null,"httpStatus":null,"requestsStarted":0,"responsesReceived":0,"outcomeUnknown":false,"failureCode":"MISSING_APPROVAL","bundlePublished":false}
```

This is an offline missing-approval probe, not an Acquire invocation or provider failure. Zero sends/responses. Eleven WP42 codes remain intact; actual fake-handler C# envelopes round-trip through Python in tests. No real key was needed, including for presence checks.

## Commands and tests

Executed from isolated source with existing installed tools. Commands are Windows PowerShell 5.1 compatible; actual chart/client invocation used the existing project virtual-environment Python. No policy bypass, downloads, restore network source or pulls.

```powershell
$prepared = Join-Path $env:TEMP 'aiq-wp44-prepared-20261107'
python -B eng/docker/release-1.15/wp39_local_history.py --action Prepare --directory $prepared
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source (Join-Path $prepared 'empty-feed') -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
# Use the existing project virtual-environment interpreter:
python -B -m unittest test_research_chart test_research_snapshot_client -q
git diff --check
```

Results: **101 C# passed**, 0 failed/skipped; **23 Python workflow passed**; **61 chart/client passed**; total **185 passed**, exit 0. Existing analyzer warnings CA1711, SYSLIB1045, CA1861, CA2219 and Streamlit bare-mode notices remain. No failed prerequisite, skipped runtime suite or broad/browser requalification claimed. Fake tests preserve synthetic provenance, no partial bundle, strict envelope/canary behavior and actual status/counters; they do not prove authentic availability.

## Mandatory new owner checkpoint

No live approval has been received for WP44. Request **one** existing Acquire invocation, maximum two sequential HTTPS GETs to `https://api.twelvedata.com/time_series`: BTC/USD then ETH/USD, `interval=1min`, `outputsize=1000`, `timezone=UTC`, `order=ASC`; existing latest-24-hour completed-observation window and 15-second grace. No retry, pagination, fallback, additional endpoint, account/credit/capability probe, DNS/HEAD preflight or extra invocation. Actual credits and availability unknown. Owner previously confirmed BTC/ETH base-asset volume units; do not silently alter that assumption.

This prompt, PR merge, flags and historical consumed approvals do not grant live authority. Stop here until explicit new owner approval. Before a later approved invocation, recheck clean source/head, build/manifest identity, protected ACL, valid TLS, no conflicting session/state, absent destination and exact Windows User setting NAME TwelveData__ApiKey. Read value only inside separately approved Acquire, map temporarily to process-only AIQ_WP39_PROVIDER_KEY, restore prior channel in finally. Never expose key in arguments, files, URLs, Docker, Git, logs or browser.

On first failure/unknown, stop immediately with only bounded version-1 fixed diagnostic fields, child exit and artifact-presence evidence. Preserve private artifacts; no retry/import/start/browser. Unknown counts/status/credits remain unknown. No provider response/header/code/message/private paths copied to evidence.

Only a complete atomically published ObservedProvider bundle passing existing provenance/hash validation permits existing Import -> Start -> Verify -> Stop and actual local browser checks. Conditional evidence must cover both instruments, UTC/OHLCV/base volume/counts/gaps, receipt/generation, authenticated TLS, continuity/readiness, switching, 100/300/1000 presets, zoom/Refresh retention and observed-provider labels. No fixture substitution or importer weakening. These steps are NOT RUN now.

## Historical uncertainty, preservation and readiness

WP41 remains BTC/USD HTTP 200 / response_parse / INVALID_PROVIDER_PAYLOAD, one send/one response, ETH/USD not requested, no bundle/import; exact historical body defect and credits unknown. WP42 categories are prospective, not a retroactive explanation.

Retain owned preparation/images privately pending review. No session was started, so no process cleanup needed. Later cleanup must match private manifest IDs/tags and remove only confirmed owned resources; never prune or delete historical artifacts. Use existing Stop action for any later owned session, preserving state/volume. No Azure/public operations.

Exactly two changed paths: this new runbook and append-only `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`. UTF-8/LF/no BOM and independent CRLF/diff checks passed. New WP44 PR stays OPEN/UNMERGED.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Separately approved live outcome

After offline evidence and PR #431, owner explicitly approved: "approved. run one acquire invocation". Immediately before execution, clean source head `f0e57b2370526faa4653c16f3279f8715ca20900`, published assembly hashes, image/manifest identity, actual TLS validity, protected private ACL, zero conflicting session containers/state volumes, absent destinations and exact Windows User setting NAME were rechecked successfully. The setting value was read only inside the approved operation, transient process channel restored in finally. No key/raw response/header/provider code/message/private path was printed or copied into evidence.

Exactly one Acquire invocation, exit **1**. Observed bounded diagnostic:

```json
{"version":1,"event":"WP39_ACQUIRE_FAILED","failedStage":"response_parse","instrument":"BTC/USD","httpStatus":200,"requestsStarted":1,"responsesReceived":1,"outcomeUnknown":false,"failureCode":"INVALID_PROVIDER_METADATA","bundlePublished":false}
```

The updated parser distinguishes missing/wrong-typed metadata or its required fields under this code; the diagnostic does not identify which field or its content. Do not infer provider account/key state, response contents or charged credits. This is the new WP44 observation, not a retroactive explanation of WP41. One send/one response observed; ETH/USD was not requested. HTTP 200 is not valid-data acceptance.

Presence-only checks: bundle **absent**, import receipt **absent**, private SQLite **absent**, qualification **absent**. No Import/Start/Verify/browser; no retry, additional provider probe, Azure operation or registry publication. Owned private preparation/images retained, no session started. Approval consumed. Earlier offline zero-request/checkpoint sections remain historical; overall WP44 now includes this one approved send/response. No further attempt is authorized.

**STOPPED AFTER ACQUISITION FAILURE - OWNER REVIEW REQUIRED**. `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
