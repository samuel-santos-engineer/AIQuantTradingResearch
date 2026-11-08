# Release 1.15 WP53 - fresh local preparation qualification

Date: 2026-11-08. Source baseline main/#439 squash `f7481558d8a2da69463a56a2392deef120e76f46`, verified merged. Clean isolated WP53 branch. Twelve Data remains the sole provider for authentic BTC/USD and ETH/USD minute OHLCV; no provider/request/acceptance changes.

## Disposition

| Fact | Result |
| --- | --- |
| Separate Docker/resource authority | **AUTHORIZED** by explicit owner reply after WP53 prompt. |
| Fresh Prepare | **EXECUTED**, once, exit0, using cached inputs and a new owned temporary directory. |
| Preparation identity and Linux guard | **QUALIFIED** for the tested preparation and cases below. |
| Actual acquisition/import/chart runtime | **NOT EXECUTED / NOT QUALIFIED**. No operational approval implied. |
| Linux real symlink rejection | **PASS**, real filesystem symlinks, not mocked. |

No production/test correction was needed. Repository changes are only this evidence and an append-only execution-plan entry. Prior private preparations, volumes, containers and owner files were not changed or deleted.

## Commands and offline baseline (executed first)

```powershell
$feed = Join-Path $env:TEMP 'aiq-wp53-empty-feed'
New-Item -ItemType Directory -Force -Path $feed | Out-Null
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source $feed -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
& '<existing-venv-python.exe>' -B -m unittest test_research_chart test_research_snapshot_client -q
```

Windows:163 C# passed;38 workflow run,37 passed/1 skipped (unprivileged symlink unavailable);61 chart/client passed. **262 run,261 passed,1 skipped**, all exits0. Restore used only cached packages/empty feed; no download. Existing owner-checkout venv used for chart/client; no installation. Existing CA1711, SYSLIB1045, CA1861, CA2219 analyzer and bare-mode Streamlit ScriptRunContext warnings recorded; no unrelated repair.

## Separately authorized Docker preflight and Linux regression

Executed `docker context show`, `docker info --format '{{.OSType}} {{.ServerVersion}}'`, and `docker image inspect <exact-input> --format '{{.Id}}'` for all three inputs below. Context desktop-linux; engine linux29.7.2. All commands exited0. No pull/registry publication.

| Required cached input | Observed image identity |
| --- | --- |
| aiq-c2b36:candidate | sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed |
| mcr.microsoft.com/dotnet/aspnet:10.0 pinned digest | sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f |
| python:3.13-slim pinned digest | sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024 |

Linux regression argv (placeholders replace local paths/unique owned name only):

```text
docker run --pull=never --rm --name <owned-linux-test> --network none --read-only --cap-drop ALL --security-opt no-new-privileges --pids-limit 64 --memory 256m --cpus 1 --tmpfs /tmp:rw,noexec,nosuid,size=64m --mount type=bind,source=<isolated-checkout>,target=/work,readonly --workdir /work python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024 python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
```

**38 Linux tests passed,0 failures,0 skips, exit0**, including test_linked_publication_or_manifest_fails_before_reads: actual Worker file symlink, manifest symlink and directory symlink rejected. Combined Windows+Linux suite executions: **300 run,299 passed,1 Windows skip**. Do not erase the host skip; the independent Linux run closes the requested Linux qualification item. Owned test container removed automatically by --rm; existing containers/volumes untouched.

## One fresh Prepare - executed and preserved

Invoked the existing command exactly once:

```text
python -B eng/docker/release-1.15/wp39_local_history.py --action Prepare --directory <new-owned-temporary-directory>
```

A bounded Python subprocess wrapper supplied an allowlist of nonsecret Windows tool/cache environment names only (PATH,SystemRoot,WINDIR,TEMP,TMP,LOCALAPPDATA,APPDATA,USERPROFILE,HOMEDRIVE,HOMEPATH,ProgramFiles variants,DOTNET_ROOT,COMSPEC,PATHEXT), plus DOTNET_CLI_TELEMETRY_OPTOUT=1. No provider/service credential channel was queried, copied or forwarded. Timeout240s; captured stdout was the fixed `PREPARED_NOT_IMPORTED; no provider calls or private state created`, stderr empty, **exit0**.

Prepare inspected only existing cached inputs, applied its owner-only ACL, restored/published Worker using empty-feed/cache-only settings, ran only its local preparation mode (not acquisition), copied publication into context/research and built two unique owned images with pull=false/network=none. It generated its normal fresh disposable service/TLS configuration privately; **no generated key, configuration contents or TLS private material was inspected or published**. No historical/private bundle read. New owned preparation and images are retained for owner review; no old preparation/resource deleted.

One initial verification from the restricted execution context returned the sanitized identity failure because the preparation's owner ACL denied access. Repeating under the same approved owner context used by Prepare passed. This is the expected ACL boundary, not a source defect; no permission weakening or code fix was applied.

Verified nonsecret identity (40 publication files, manifest version1):

| Identity | Value |
| --- | --- |
| Workflow SHA-256 | 9a63181918d35653e03e6c91d58d322f09de5de170580ac46eab35ba70450e8f |
| Published Worker.dll SHA-256 | 5e67728a906d8f85b7b074fc1c0e45a6682401f38b8658ece2e65a76825bba14 |
| Publication mapping digest | 9f486cb9ca575e9856f8f40fa4f04477d84c6938d5c7da3437a9f0b056b6217e |
| Research image | sha256:f845723bc624a98a0416e5aea6059c350e87bce30d6561f22008dd28b30d1c07 |
| UI image | sha256:48517846c81a12ccfd148b936bc3a89a7d9b249d1534fdfc3a6a099b613d1bc4 |

Mapping digest is SHA-256 of UTF-8 json.dumps(publication,sort_keys=True,separators=(',',':')). Manifest structure is exactly version/workflowSha256/publication; no private location is recorded here. Full actual publish and context/research trees were recomputed by validate_preparation_identity and matched, not merely the entry DLL. Bundle, import receipt and private database directory were absent. These existence checks concern only newly owned outputs, not credential presence.

## Linux validation of real artifacts and controlled rejection

Second owned cached Python container used --pull=never/--rm/--network none/--read-only/cap-drop ALL/no-new-privileges, pids64,512MiB,cpus1 and256MiB noexec/nosuid tmpfs. Read-only mounts were limited to isolated source at /work and the fresh **publish tree, context/research tree and preparation-identity.json individually** at /input. Service key, TLS files, initialization configuration and private state were not mounted. No Worker/application container was started.

Executed Python -B -c qualification logic:

1. Import the real current workflow, validate_preparation_identity(Path('/input')) and parse only its nonsecret identity manifest.
2. Copy only publish, context/research and identity into an owned /tmp disposable copy; validate the copy positively.
3. For each controlled mutation below, run the existing acquire function only behind mocked environment, Worker and subprocess boundaries; assert the exact static ten-field preflight failure, env.get/env.copy not called, Worker/Popen not called. This is a fake-only rejection rehearsal, **not actual Acquire CLI or a provider attempt**.
4. Restore only each changed disposable file, validate the copy again and revalidate the original read-only source. Container /tmp disappears with its owned container; source artifacts remain unchanged.

| Controlled disposable-copy case | Result |
| --- | --- |
| Missing manifest | PASS |
| Modified real Worker DLL | PASS |
| Modified real .deps.json | PASS |
| Modified real native .so file | PASS |
| Modified context/research Worker copy | PASS |
| Changed workflow path/bytes | PASS |
| Worker symlink escaping to disposable outside sentinel | PASS |
| Existing fake destination sentinel with invalid manifest | PASS, sentinel preserved |

**Eight checks passed, exit0**; these are additional qualification checks, not extra unittest counts. Final fixed summary: realIdentity=PASS,disposableCopyIdentity=PASS,negativeCount=8,sourcePreserved=true,providerSends=0,credentialReads=0.

Each rejection reconstructed exactly version1,event WP39_ACQUIRE_FAILED,failedStage preflight,instrument/httpStatus null,requestsStarted/responsesReceived0/0,outcomeUnknown=false,failureCode PREPARATION_IDENTITY_INVALID,bundlePublished=false. No raw publication bytes, invalid values, credentials, local private paths or provider response text emitted. No second request/retry possible at the mocked dispatch boundary. No bundle/receipt created.

## Limits, next step and status

Docker preparation was AUTHORIZED / EXECUTED / QUALIFIED for creation and identity validation of these matching artifacts. This does not qualify Linux Worker startup, HTTPS authentication, actual provider availability/base-volume units, import, analytics/chart or browser behavior. The two fresh images were built, not launched as the application. WP52's owner-writable trust/last-check race limitations remain: no signed build attestation or protection against an attacker rewriting all owned files. Existing UTC/ASC/1000/24h/15s, six volume subtypes, mandatory authentic OHLCV/base units, first-failure/no-retry and two-pair atomic publication rules are unchanged.

Next concrete step is owner review of this evidence/PR, then a **separate explicit single-attempt authorization** if the owner elects to acquire, with immediate fresh identity/TLS/ACL/destination checks. WP47 approval remains consumed; its subtype/credits and provider minute-volume availability remain unknown. No live provider permission requested or inferred here. No new licensing/governance gate.

Only changed repository paths: this `docs/roadmap/release-1.15/prompters/release-1.15-wp53-fresh-preparation-qualification.md` and append-only `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`. No source/test changes needed. UTF-8/LF/no BOM and git diff --check verified before commit/PR. No Azure/registry/deployment/provider/DNS/HEAD/account/credit/support operations, credential/presence inspection, private historical payload inspection or actual Acquire/Import/Start/Verify/browser operation. PR remains OPEN/UNMERGED.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
