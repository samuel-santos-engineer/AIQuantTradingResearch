# WP47 - controlled acquisition checkpoint

Status **AWAITING_NEW_OWNER_PROVIDER_APPROVAL**. Phase A complete; Phase B NOT RUN. Local date 2026-11-07; UTC observation 2026-11-08 02:11:04.

## Baseline and implementation reconciliation

Verified #433 MERGED, fetched origin/main exactly `45e9250ff658f9dc4f9fef8963370bc6fd30d244`; clean new isolated branch. AGENTS/LF governance and WP39-WP46 workflow/evidence decisions reviewed. Owner dirty/untracked checkout, earlier private artifacts and unrelated resources untouched.

Fresh source includes WP46 compatibility: exchange_timezone may be absent for the fixed UTC BTC/ETH path; present values still require string UTC. Status, meta/symbol/interval, strict timestamp/UTC/minute/sequence/OHLCV/base-volume constraints, 24h window/grace, bounded inputs, atomic two-symbol publication and provenance remain unchanged. Historical WP44 exact failed field and consumed credits remain unknown; no inference from prospective compatibility.

## Fresh offline preparation and identities

Existing Python Prepare exited 0, PREPARED_NOT_IMPORTED. New private temporary destination; owner/SYSTEM grants and protected ACL. Cached pinned bases/dependency image verified; cache-only empty-feed restore, fresh source publication and two local Docker builds with --pull=false/--network=none. No downloads/pulls or older prepared binaries reused. Toolchain .NET SDK10.0.103, Python3.13.15, Docker29.7.2 Linux/x86_64.

- AIQuantTradingResearch.Worker SHA-256: `43fe842b4a2d815b859cd1bd53775e8d65075963cb9dae11e75446c2acffdeb6`.
- AIQuantTradingResearch.Infrastructure SHA-256: `049db4a23be77542940abe3b6da4db5fa8ebebb079014f9facf59dea87c4ac83`.
- research image ID: `sha256:468e02dbfe35405ada3147c32012c48022f86532ad8411fe7e5d7cf4c115ebfd`.
- ui image ID: `sha256:4586acfebc8ac00c90611a584dd938148b8491e6573818990a5dbc0a7a5a1731`.

Published assemblies equal fresh Release build outputs; both owned image tags match new private manifest. Zero conflicting session containers and zero owned state volumes. Manifest present; bundle, import receipt, private SQLite and qualification absent. Public TLS certificate SAN wp07-research, valid **2026-11-08 02:09:02 UTC through 2026-11-09 02:10:02 UTC**, valid at actual check 2026-11-08 02:11:04 UTC. No private PFX/key inspected. Expiry requires a new owned preparation, never overwrite historical outputs.

Fresh published Worker through actual Python worker/strict child parser, with approval/provider process channel removed from child environment and empty stdin:

```json
{"version":1,"event":"WP39_ACQUIRE_FAILED","failedStage":"preflight","instrument":null,"httpStatus":null,"requestsStarted":0,"responsesReceived":0,"outcomeUnknown":false,"failureCode":"MISSING_APPROVAL","bundlePublished":false}
```

Expected negative probe, not an Acquire invocation. Zero sends/responses; no real credential value or presence inspection.

## Executed commands and fake tests

From isolated source, Windows PowerShell5.1-compatible commands; existing installed dependencies only:

```powershell
$prepared = Join-Path $env:TEMP 'aiq-wp47-prepared-20261107'
python -B eng/docker/release-1.15/wp39_local_history.py --action Prepare --directory $prepared
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source (Join-Path $prepared 'empty-feed') -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
# Actual chart/client invocation used existing project virtual-environment Python:
python -B -m unittest test_research_chart test_research_snapshot_client -q
git diff --check
```

**210 passed:** 126 focused C# (0 failed/skipped), 23 workflow, 61 chart/client; all exits0. Existing CA1711/SYSLIB1045/CA1861/CA2219 warnings and Streamlit bare-mode notices remain. No failed prerequisite, skipped runtime suite or policy bypass. Fake tests cover optional timezone absence, strict identity/present-field/row bounds, structural errors, real Worker envelope/Python compatibility, unknown outcomes/canaries, second-symbol failures/no partial bundle and SyntheticFixture-only safeguards. These are not authentic observations.

## Mandatory fresh approval checkpoint

No new live approval received. Prior WP41/WP44 approvals consumed. This task/merge/flags/prechecks grant no Acquire authority. Request **exactly one** existing Acquire invocation: at most two sequential HTTPS GETs to `https://api.twelvedata.com/time_series`, BTC/USD then ETH/USD, `interval=1min`, `outputsize=1000`, `timezone=UTC`, `order=ASC`; existing latest24h completed-observation window and 15-second grace. No retry/pagination/fallback/additional symbols/endpoints/account/credit/capability/DNS/HEAD probe. Actual credits and availability unknown; base volume units remain owner-confirmed BTC/ETH.

Only after explicit new owner approval: recheck clean head/source, fresh assembly/image/manifest, protected ACL, valid TLS, no session/state conflict or destination artifacts; inspect exact Windows User setting NAME TwelveData__ApiKey. Read value solely inside approved Acquire, transiently map process AIQ_WP39_PROVIDER_KEY and restore previous value in finally. No key in args/URLs/files/Docker/logs/Git/browser. Never access earlier private responses.

First failure/unknown stops immediately: only bounded version1 ten-field diagnostic, exit and artifact-presence report; no retry/import/start/verify/browser or extra request. Approval consumed regardless of precise failure category. Retain unknown status/count/credit facts as unknown; no raw response/message/header/path or exception disclosure.

Only complete atomically published ObservedProvider bundle passing existing validation permits Import -> Start -> Verify -> actual local browser checks -> Stop. Conditional checks: BTC/ETH minute UTC/OHLCV/base volume/counts/gaps, receipt/generation, TLS/authentication, readiness/continuity, switching, 100/300/1000 presets, zoom/Refresh retention and labels. Never relabel fixtures or weaken importer. None of those operational phases ran here.

## Actual stage, preservation and review

Acquire invocations **0**, real provider requests **0**, explicit live approval **not received**, credential presence/value checks **0**, Azure/registry/public operations **0**. Bundle/import/database/qualification absent; local chart/browser NOT RUN. No public/local acceptance inferred.

Owned preparation/images retained privately. No session started; no process cleanup necessary. Any later cleanup must match ownership/manifest IDs and target only confirmed owned resources; no prune or historical deletion. Existing Stop preserves state/volume for a later owned session. No new licensing/architecture gate.

Exactly two paths changed: this new runbook and append-only `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`. UTF-8/LF/no BOM, independent CRLF check and git diff --check passed; no private artifacts staged. New WP47 PR OPEN/UNMERGED. Stop at **AWAITING_NEW_OWNER_PROVIDER_APPROVAL**.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
