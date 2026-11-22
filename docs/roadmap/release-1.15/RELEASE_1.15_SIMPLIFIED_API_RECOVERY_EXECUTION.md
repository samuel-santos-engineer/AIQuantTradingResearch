# Release 1.15 simplified Research API recovery

| Gate | Status | Latest evidence | Blocker |
| --- | --- | --- | --- |
| G0 Baseline and reproducibility | PASS | [G0 attempt 1](#g0-attempt-1) | None |
| G1 Local API startup | PASS | [G1 attempt 5](#g1-attempt-5) | None |
| G2 Local REST and Streamlit | PASS | [G2 attempt 2](#g2-attempt-2) | None |
| G3 Source-bound images and rollback | PASS | [G3 attempt 4](#g3-attempt-4) | None |
| G4 Staging API-first acceptance | BLOCKED | [G4 attempt 4](#g4-attempt-4) | Verify the ten nonsecret staging API settings and key presence through the portal before another image update. |

API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.


## G0 attempt 1

PASS on 8db2f908e7a691b9b05ce825b4e5f5dd8c877f69 from 2026-11-22T14:48:20.351088Z to 2026-11-22T14:48:20.911800Z.

PR #479 is merged in current main. Source tree 3e3abdc2d85e78e43461c716019157dfde76d420. The original checkout contains owner changes and was preserved. .NET SDK 10.0.103, Python 3.13.15, Docker 29.7.2 Linux, Streamlit 1.61.1 and Plotly 7.1.0. Worker --research-snapshot-api starts through research-bootstrap.sh on 52731. Its local configuration names are AIQ_RESEARCH_API_KEY, AIQ_RESEARCH_PRIVATE_DIRECTORY, AIQ_RESEARCH_TLS_PFX, AIQ_RESEARCH_TLS_PASSWORD, AIQ_RESEARCH_BIND_ADDRESS and AIQ_RESEARCH_TRANSPORT. GET /health and authenticated POST /v1/research/snapshot are implemented in ResearchSnapshotEndpoint.cs. The separate Streamlit process uses AIQ_RESEARCH_CHART, AIQ_RESEARCH_BASE_URL, AIQ_RESEARCH_API_KEY and AIQ_RESEARCH_CA_FILE on 8501. The tracked research-api.Dockerfile and research-chart.Dockerfile pin cached Linux/amd64 bases. The last recorded rollback children are API sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3 and UI sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde. They are historical staging receipts. The old API source revision and current Azure configuration are unverified.

Next action. Run the local Worker startup and health gates.

Azure mutations. 0.

## G1 attempt 1

FAIL on 8db2f908e7a691b9b05ce825b4e5f5dd8c877f69 from 2026-11-22T14:48:20.911800Z to 2026-11-22T14:50:07.460076Z.

Five passed and two failed. STORAGE and LISTENER cases stopped earlier at TLS. xUnit stack frame was ResearchSnapshotEndpointTests.cs:56. The underlying certificate exception is intentionally suppressed by the Worker. No health request occurred. The start timestamp is the recorded lower bound from the preceding gate or attempt.

Next action. Isolate the TLS certificate load with a fake PFX before changing application code.

Azure mutations. 0.

## G1 attempt 2

FAIL on 8db2f908e7a691b9b05ce825b4e5f5dd8c877f69 from 2026-11-22T14:50:07.460076Z to 2026-11-22T14:51:35.355383Z.

Synthetic PFX was readable. Windows default key import failed inside the sandbox with CryptographicException 0x80070002 at ImportPfx; the same PFX imported successfully outside it. Six diagnostic cases passed outside the sandbox and the Worker published. The direct launch then exited before health. The smoke harness did not retain the fixed stage on this attempt. The start timestamp is the recorded lower bound from the preceding gate or attempt.

Next action. Record the direct Worker's fixed startup stage and check local port availability.

Azure mutations. 0.

## G1 attempt 3

FAIL on 8db2f908e7a691b9b05ce825b4e5f5dd8c877f69 from 2026-11-22T14:51:35.355383Z to 2026-11-22T14:53:11.991419Z.

Worker exited 1 at SERVING. A synthetic Kestrel StartAsync at 52731 raised SocketException 0x80004005 AccessDenied at Socket.DoBind and SocketTransportOptions.CreateDefaultBoundListenSocket. An OS-selected port started successfully in the same probe. Windows excludes 52678?52777. This reproduces a local bind failure and supplies no cause attribution for Azure. The start timestamp is the recorded lower bound from the preceding gate or attempt.

Next action. Make the local TLS listener port configurable while preserving Azure port 52731.

Azure mutations. 0.

## G1 attempt 4

FAIL on 8db2f908e7a691b9b05ce825b4e5f5dd8c877f69 from 2026-11-22T14:53:11.991419Z to 2026-11-22T14:54:49.560625Z.

Nine startup-stage tests passed. The direct Windows Worker answered 200 Alive on 55231 and passed the missing-key stage check. The first Linux-mapped health connection returned SSLEOFError before a successful handshake. The owned container was removed. No Linux startup conclusion on this attempt. The start timestamp is the recorded lower bound from the preceding gate or attempt.

Next action. Allow bounded local startup negotiation and retain the fixed container startup stage if the listener exits.

Azure mutations. 0.

## G1 attempt 5

PASS on 8db2f908e7a691b9b05ce825b4e5f5dd8c877f69 from 2026-11-22T14:54:49.872819Z to 2026-11-22T14:55:57.592482Z.

The pinned Linux runtime used the newly published Worker and repository bootstrap. It answered 200 Alive on a host loopback mapping from 55231 to container 52731 and stayed running until cleanup. The direct Windows smoke and nine startup diagnostic cases passed in the preceding attempt. The local-only port override preserves the Linux default and the staging listener port. The Azure startup cause remains unknown.

Next action. Run the fixture REST contract and Streamlit browser gate.

Azure mutations. 0.

## G2 attempt 1

FAIL on 8db2f908e7a691b9b05ce825b4e5f5dd8c877f69 from 2026-11-22T15:48:28.832596Z to 2026-11-22T15:50:34.836786Z.

33 .NET cases and 56 Python cases passed. Chrome showed BTC and ETH candlesticks, SMA, and ETH volume-unavailable disclosure. The browser harness then dereferenced a transient null graph after the disconnected status appeared during Streamlit remount, at wp07_browser_qualification.py:350. Remaining browser checks were not accepted.

Next action. Wait for the remounted graph before checking retained closes, then rerun the browser gate.

Azure mutations. 0.

## G2 attempt 2

PASS on 8db2f908e7a691b9b05ce825b4e5f5dd8c877f69 from 2026-11-22T15:50:35.007689Z to 2026-11-22T15:53:37.785138Z.

The browser rerun passed. Actual Chrome showed BTC to ETH to BTC, candlesticks and C# SMA20, RSI14 and MACD, numerical BTC volume, ETH volume-unavailable disclosure with no bars, pointer zoom and pan with refresh retention, Return to Live, and retained chart data with offline, 401 and malformed API responses followed by reconnects. The full harness reports PARTIAL for its broader qualification scope, including bounded resource observations. No authentic data or staging browser qualification occurred. Combined focused coverage is 34 .NET and 56 Python chart tests passed after the targeted rerun. Six receipt-schema cases also passed.

Next action. Commit the tested source and run the local Linux dual-container build and smoke.

Azure mutations. 0.

## G3 attempt 1

FAIL on 6179d8f44edab7d01bf65bd3f64dfe99a1598c40 from 2026-11-22T15:54:11.593151Z to 2026-11-22T15:55:03.452586Z.

Framework-dependent publication succeeded from the committed tree. The host PowerShell execution policy blocked loading the WP07 wrapper before it ran any Docker command.

Next action. Use the existing WP07 Python Docker driver directly with the same inputs and without changing PowerShell policy.

Azure mutations. 0.

## G3 attempt 2

BLOCKED on 6179d8f44edab7d01bf65bd3f64dfe99a1598c40 from 2026-11-22T15:55:03.588124Z to 2026-11-22T15:59:45.539458Z.

Both candidates built from the committed tree and passed the dual-container synthetic smoke. API auth, BTC/ETH, an actual Chrome browser, C# supplied analytics, outage/reconnect, writer exclusion and restart behavior passed. Local API staging-mode smoke returned 200 Alive on 52731 and authenticated 503 DATASET_NOT_READY for empty ephemeral state. This is a local fixture test and supplies no evidence about Azure's overlay, runtime image, dataset, or startup failure. Two OCI chains verified locally. Registry publication and fresh staging baseline were not run.

Next action. Approve exact candidate registry publication and digest readback.

Azure mutations. 0.

## G4 attempt 1

BLOCKED on 6179d8f44edab7d01bf65bd3f64dfe99a1598c40 from 2026-11-22T15:59:45.833735Z to 2026-11-22T15:59:45.834302Z.

No registry publication, ARM read, Azure mutation, staging health or staging data request occurred. Candidate child digests are local archive observations. Staging dataset presence remains unknown.

Next action. Complete G3 publication and baseline verification before an exact image-only apply checkpoint.

Azure mutations. 0.


## Candidate provenance and staging checkpoint

Validated source `6179d8f44edab7d01bf65bd3f64dfe99a1598c40` and tree `222917d520a3451788af37d6aa3d709f84f725fd`. Docker Engine 29.7.2 and Buildx v0.36.1-desktop.1 built with the pinned Linux/amd64 bases and local qualified Python dependency image, using `--pull=false --network=none`. The build context contained the published Worker, bootstrap and allowlisted presentation sources. Private fixture, TLS and state stayed outside the build context.

| Role | Cached candidate tag | Local OCI index and image ID | Local Linux/amd64 child | Child config |
| --- | --- | --- | --- | --- |
| API | aiq-wp07-bf45fb49d44f41b088522b3d59c1699f:research | sha256:fefdccc43d8dec7c7b290813967fd29c936e9d95ffa30ed155d2dadd1a768393 | sha256:aecb4586a30bf5be3f398bdcf1578127208e10198528a546324039da40c4c357 | sha256:6e0519660d51dbeb161a02925cc9448e80b082c4106b46635640bdac7387be3f |
| UI | aiq-wp07-bf45fb49d44f41b088522b3d59c1699f:ui | sha256:6bdc3136aa0c17db366561b9deb5730486cf6efea8decba86abcb0c5c8769b6e | sha256:d26e2ea9a15699b19124dc88e1a26e01e3dd7f8e9ea5fa0a777010d6574c5f14 | sha256:ac97152a84d6ed7d0c3a7780ebd7ed6c24dd7b59c92da43377eb1269fa86596d |

Publish manifest SHA-256 `sha256:03e3bfab4da5b9be7ccf79fb367348730a87c1028127c5271675606801b43018`. Context manifest SHA-256 `sha256:36921b54825be9558fe7a32332d5ffe78e82d5cc2f1fe734384194e3f9397c2f`. API Dockerfile `sha256:b1de21175c37120916ca31f2490405e7408c8cc719db595b3a8374272a6b03b3`, UI Dockerfile `sha256:c74bc6a5b1aebaf236d524246ab6983bdc2bf62bcfcceb14edaa98c709c921de`, bootstrap `sha256:b63b0a15d3c6d61652fc4f29d2954d34152bf19b40c2d493b2c18ae8f42fc100`. Hash-verified local OCI archive metadata established each mapping. Registry publication and fresh registry readback remain pending. Use verified registry child digests for any staging configuration.

| Order | Existing staging app and port | Historical rollback child | Proposed child pending registry readback |
| --- | --- | --- | --- |
| API first | aiqr115researchstagingwus27f5eabb5, 52731 | sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3 | sha256:aecb4586a30bf5be3f398bdcf1578127208e10198528a546324039da40c4c357 |
| UI after API acceptance | aiqr112wp05wus27f5eabb5, 8501 | sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde | sha256:d26e2ea9a15699b19124dc88e1a26e01e3dd7f8e9ea5fa0a777010d6574c5f14 |

Resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`. Subscription `7f5eabb5-80f5-4369-88be-b14a6de763f1`. Stop on drift from the historical rollback references or existing startup, routing or HTTPS contract. The proposed setting change is image-only. Preserve keys, startup override, ports, TLS, storage, slot metadata and plan. The proposed image reference is `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@<verified-child>` after registry verification.

After explicit apply approval, verify the API's configured digest, startup and exact GET /health 200 Alive, then authenticated POST /v1/research/snapshot for genuine staging BTC/USD and ETH/USD. Stop before UI if API acceptance fails and execute only the approved API rollback. After API acceptance, configure UI and run one bounded BTC to ETH to BTC browser check. Roll back a failed UI independently. Verify each configuration readback.

Proposed image-only PATCH and rollback command shape for Windows PowerShell 5.1. Use a reviewed UTF-8/LF JSON file. Apply uses the same shape with the approved registry candidate. Verify target, prior settings, rollback digest and approval before executing.

```powershell
# $app must match the approved staging role exactly.
$subscription = '7f5eabb5-80f5-4369-88be-b14a6de763f1'
$group = 'rg-aiq-r112-wp05-wus2-7f5eabb5'
$uri = "https://management.azure.com/subscriptions/$subscription/resourceGroups/$group/providers/Microsoft.Web/sites/$app/config/web?api-version=2023-12-01"
az rest --subscription $subscription --method patch --url $uri --body '@rollback-image.json' --output none --only-show-errors
```

- API rollback body `{"properties":{"linuxFxVersion":"DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3"}}`
- UI rollback body `{"properties":{"linuxFxVersion":"DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde"}}`

A fresh, complete rollback checkpoint and explicit apply authorization are required. No image was published and Azure did not change in this execution.


The immediate approval checkpoint is publication and registry readback of the exact local IDs already listed. Proposed PowerShell 5.1 commands for the existing GHCR repository follow. Run them only after approval and confirm the expected local ID before each push. Stop on any mismatch or partial publication. No Azure write is included.

```powershell
$repo = 'ghcr.io/samuel-santos-engineer/aiquanttradingresearch'
$apiId = 'sha256:fefdccc43d8dec7c7b290813967fd29c936e9d95ffa30ed155d2dadd1a768393'
$uiId = 'sha256:6bdc3136aa0c17db366561b9deb5730486cf6efea8decba86abcb0c5c8769b6e'
$apiTag = "$($repo):r115-recovery-api-6179d8f44eda"
$uiTag = "$($repo):r115-recovery-ui-6179d8f44eda"
docker image tag $apiId $apiTag
if ($LASTEXITCODE -ne 0) { throw 'API tag failed' }
docker image tag $uiId $uiTag
if ($LASTEXITCODE -ne 0) { throw 'UI tag failed' }
if ((docker image inspect --format '{{.Id}}' $apiTag) -cne $apiId) { throw 'API local identity drift' }
if ((docker image inspect --format '{{.Id}}' $uiTag) -cne $uiId) { throw 'UI local identity drift' }
docker image push $apiTag
if ($LASTEXITCODE -ne 0) { throw 'API publication failed' }
docker image push $uiTag
if ($LASTEXITCODE -ne 0) { throw 'UI publication failed' }
docker buildx imagetools inspect $apiTag
if ($LASTEXITCODE -ne 0) { throw 'API registry readback failed' }
docker buildx imagetools inspect $uiTag
if ($LASTEXITCODE -ne 0) { throw 'UI registry readback failed' }
```

Verify each manifest and config by digest during readback before creating a staging apply checkpoint. Seven strict receipt-schema tests passed, including positive PASS, FAIL and BLOCKED fixtures and rejected portable-path, key and artifact mutations.

## G3 attempt 3

BLOCKED on 6179d8f44edab7d01bf65bd3f64dfe99a1598c40 from 2026-11-22T16:18:13.961324Z to 2026-11-22T16:19:16.181113Z.

Both approved GHCR tags were published and verified. The raw registry index and Linux/amd64 manifest each matched the local SHA-256 byte hash, and their config descriptors matched the local verified configs. No Docker login, direct credential read or Azure call occurred. The historical staging rollback configuration remains unverified in this pass.

Next action. Approve projected read-only staging baseline verification for the two apps.

Azure mutations. 0.

Registry publication and readback completed at 2026-11-22T16:18:38.380284Z in 24.419 seconds. Two GHCR image tags were written.

| Role | Verified registry tag | OCI index | Linux/amd64 child | Child config descriptor |
| --- | --- | --- | --- | --- |
| API | ghcr.io/samuel-santos-engineer/aiquanttradingresearch:r115-recovery-api-6179d8f44eda | sha256:fefdccc43d8dec7c7b290813967fd29c936e9d95ffa30ed155d2dadd1a768393 | sha256:aecb4586a30bf5be3f398bdcf1578127208e10198528a546324039da40c4c357 | sha256:6e0519660d51dbeb161a02925cc9448e80b082c4106b46635640bdac7387be3f |
| UI | ghcr.io/samuel-santos-engineer/aiquanttradingresearch:r115-recovery-ui-6179d8f44eda | sha256:6bdc3136aa0c17db366561b9deb5730486cf6efea8decba86abcb0c5c8769b6e | sha256:d26e2ea9a15699b19124dc88e1a26e01e3dd7f8e9ea5fa0a777010d6574c5f14 | sha256:ac97152a84d6ed7d0c3a7780ebd7ed6c24dd7b59c92da43377eb1269fa86596d |

The next approval checkpoint is four projected ARM GETs, one site and one config/web read for each staging app. Confirm exact app and plan identity, HTTPS-only and configured image references, and an empty startup override. No app-settings list or credential read is proposed. Stop on missing evidence or drift from the historical rollback references. G4 remains blocked pending a complete baseline and explicit image-only apply approval.

## G3 attempt 4

PASS on 392fb8f3323318aa1a0a85e41d03a2f72fcc0e2e from 2026-11-22T16:40:56.326262Z to 2026-11-22T16:41:59.623253Z.

Four successful ARM metadata reads in 12.565 seconds. Both exact Linux West US 2 staging apps are enabled, Running, HTTPS-only, and on the expected shared plan. Site usage and availability both report Normal. Both startup overrides are empty and both configured image digests match the historical rollback references. The immutable candidate mappings were verified in the preceding G3 publication attempt. No runtime identity, health, quota counters, app settings or dataset content was read.

Next action. Obtain exact API-only image apply and conditional rollback approval.

Azure mutations. 0.

## G4 attempt 2

BLOCKED on 392fb8f3323318aa1a0a85e41d03a2f72fcc0e2e from 2026-11-22T16:41:08.891065Z to 2026-11-22T16:41:59.849460Z.

G3 image baseline is verified. This authority covered four ARM GETs only. No staging health, authentication, dataset, deployment, rollback or browser operation occurred.

Next action. Approve the exact API-only image update and conditional rollback checkpoint below.

Azure mutations. 0.


## Fresh staging baseline and API-only approval checkpoint

Observed 2026-11-22T16:40:56.326262Z to 2026-11-22T16:41:08.891065Z. Four ARM GETs, no retries, no Azure writes. Each read used a ten-second owned-worker request budget. Both staging site records identify Linux in West US 2 on the expected plan. Site records report Running, Enabled, HTTPS-only, usage Normal and availability Normal. These are management observations. Current F1 quota counters and actual running container identities are unverified.

| Role | Site GET seconds | Config GET seconds | Configured rollback child | Startup override |
| --- | ---: | ---: | --- | --- |
| API | 6.477 | 1.923 | sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3 | Empty |
| UI | 2.287 | 1.878 | sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde | Empty |

Target for the next separately approved operation is only `aiqr115researchstagingwus27f5eabb5` in `rg-aiq-r112-wp05-wus2-7f5eabb5`, subscription `7f5eabb5-80f5-4369-88be-b14a6de763f1`. The sole setting change would be `linuxFxVersion`. Keep the current startup, port 52731, HTTPS-only, authentication, ephemeral storage and plan. Allow configuration-triggered startup and zero explicit restarts.

- Apply body `{"properties":{"linuxFxVersion":"DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:aecb4586a30bf5be3f398bdcf1578127208e10198528a546324039da40c4c357"}}`
- Conditional rollback body `{"properties":{"linuxFxVersion":"DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3"}}`

Use the staging `az rest --method patch` command in the preceding checkpoint. Before applying, recheck the exact API image and startup baseline, quota and the owner-provided key channel without disclosing values. Stop before mutation on drift or a failed prerequisite. After apply, read back the API image and observe health for up to 180 seconds with at most three lightweight HTTPS requests, bounded to eight seconds each. Accept only literal 200 Alive. After health, make one authenticated snapshot POST per instrument, BTC first and ETH only after BTC passes. Require valid UTC-ordered v1.0 snapshots for actual staging BTC/USD and ETH/USD data. On failure, perform the single approved API image rollback and verify its configuration readback. The UI stays out of this checkpoint. API health and data must pass before any separate UI apply approval.

Three synthetic projection tests passed. No app settings were listed, no startup override values were emitted and no health, quota, data or browser request occurred in this baseline phase.

## G4 attempt 3

FAIL on 6179d8f44edab7d01bf65bd3f64dfe99a1598c40 from 2026-11-22T18:02:47.039399Z to 2026-11-22T18:07:20.575454Z.

Preflight passed with all three F1 counters below limit, expected API configuration and an available process key. Azure acknowledged the image-only apply, and candidate configuration readback matched. Three health observations yielded transport unverified with no HTTP status, HTTP 503 and HTTP 503. No later acceptance checks ran. The single conditional rollback was confirmed by config readback. UI, ports, keys, storage, app settings, plan and startup override were preserved. No explicit restart occurred.

Next action. Verify the Azure startup prerequisites against the Worker contract before another candidate attempt.

Azure image mutations. 2 for this attempt.


### API-only apply and rollback details

Target aiqr115researchstagingwus27f5eabb5 in rg-aiq-r112-wp05-wus2-7f5eabb5, subscription 7f5eabb5-80f5-4369-88be-b14a6de763f1. Preflight from 2026-11-22T18:02:47.039399Z to 2026-11-22T18:02:53.358982Z used three projected ARM GETs. It reported stop requests 0/15, daily CPU 0/3,600,000 ms and short-period CPU 0/180,000 ms. These are pre-change counters. Post-rollback counters are unknown. The key channel was present and passed shape validation without disclosing its value. Key equality and authentication were untested.

| Step | Configured API Linux/amd64 child | Result |
| --- | --- | --- |
| Pre-apply | sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3 | Verified rollback image and blank startup. |
| Apply and config readback | sha256:aecb4586a30bf5be3f398bdcf1578127208e10198528a546324039da40c4c357 | Image-only PATCH acknowledged. Candidate configured. |
| Health acceptance | sha256:aecb4586a30bf5be3f398bdcf1578127208e10198528a546324039da40c4c357 | First request captured no HTTP status. Second and third returned 503. Zero successful health responses. |
| Rollback and config readback | sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3 | Restored and verified. |

The apply and rollback controller ran from 2026-11-22T18:04:30.389995Z to 2026-11-22T18:06:40.007166Z in 129.617 seconds. It made four API config GETs and two image-only PATCHes. The health checks were tentative within the bounded watch. No authenticated POST /v1/research/snapshot, UI or post-rollback health request followed. Candidate startup and running image identity remain unverified. HTTP 503 alone does not identify the failure stage. Azure dataset presence and the startup root cause remain unknown. There was no additional deployment or restart.

The temporary controller passed seven synthetic preflight and sequencing cases, including quota exhaustion, key absence, baseline drift, conditional rollback, and stopping before ETH when BTC fails. API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## G4 attempt 4

BLOCKED on 6179d8f44edab7d01bf65bd3f64dfe99a1598c40 from 2026-11-22T18:18:06.088092Z to 2026-11-22T18:19:13.854618Z.

WP36 historically verified the ten declared app settings. WP80 later verified WEBSITES_PORT=52731. The recent API apply verified HTTPS-only, the configured rollback reference and an empty startup override. It did not reverify the app settings or effective runtime markers. Seven exact cached-image synthetic cases exited 1 at their expected fixed markers and all owned containers were removed. The Azure failure stage remains unknown.

Next action. Verify the ten nonsecret staging API settings and key presence through the portal before another image update.

Azure mutations. 0.


### Startup settings comparison

The [WP36 private post-save receipt](prompters/release-1.15-wp36-simplified-research-deploy.md) matched all ten declared settings and preserved the existing key. [WP80 routing evidence](RELEASE_1.15_WP80_API_STANDALONE_ACCEPTANCE.md) later matched port 52731. The G4 apply verified that the startup override remained empty. The bootstrap mode is an app setting independent of that startup override.

| Check | Expected value or condition | Evidence available |
| --- | --- | --- |
| Bootstrap | `AIQ_RESEARCH_BOOTSTRAP_MODE=serve`. An empty or absent value defaults to serve. | WP36 matched. Current app setting unknown. |
| Routing | `WEBSITES_PORT=52731` and `AIQ_RESEARCH_BIND_ADDRESS=0.0.0.0`. | WP80 matched port. Candidate image defaults the bind address. Effective injected values and ingress-to-listener delivery remain unverified. |
| Transport | `AIQ_RESEARCH_TRANSPORT=azure-app-service-http` and `AIQ_RESEARCH_PLATFORM_HTTPS_ONLY=1`. | WP36 matched both. ARM verified frontend HTTPS-only. Current process transport opt-in remains unverified. |
| Private storage | `AIQ_RESEARCH_PRIVATE_DIRECTORY=/tmp/aiq-research`, `AIQ_RESEARCH_STORAGE_SCOPE=azure-f1-staging-ephemeral`, `AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING=1`, `AIQ_RESEARCH_EXPECTED_MOUNT_PATH=/`, `WEBSITES_ENABLE_APP_SERVICE_STORAGE=false`. | WP36 matched all five. Effective values, UID, mount and directory checks were never verified in the candidate Azure container. |
| Key | `AIQ_RESEARCH_API_KEY` exists, with a Worker-compatible format. | Historically present. The G4 operator process key passed a local shape check. Azure runtime presence, format and equality remain unverified. Check only the setting name in the portal. |
| Managed process identity | Expected staging site and hostname, nonempty `WEBSITE_INSTANCE_ID` and `WEBSITE_ROLE_INSTANCE_ID=0`. | ARM identifies the site. The Worker requires the role literal, which the local smoke injected synthetically. No current main-container role value was observed. |

Microsoft identifies WEBSITE_ROLE_INSTANCE_ID as a read-only current instance ID. The reference gives no guarantee that the Linux custom container receives a value of zero. Treat a missing or nonzero value as a hypothesis for the Azure STORAGE rejection. Do not add or spoof a WEBSITE identity variable as an app setting. Microsoft's custom container routing reference documents WEBSITES_PORT as the platform routing input. [App settings reference](https://learn.microsoft.com/en-us/azure/app-service/reference-app-settings#language-specific-settings) ? [Custom container routing](https://learn.microsoft.com/en-us/azure/app-service/configure-custom-container#configure-port-number)

The local matrix used exact API image ID `sha256:fefdccc43d8dec7c7b290813967fd29c936e9d95ffa30ed155d2dadd1a768393`, tied to the published candidate by the prior OCI receipts. Each case changed one synthetic setting, used a network-isolated owned container, and produced exit 1 and the expected fixed marker.

| Synthetic variation | Observed fixed startup location |
| --- | --- |
| Unknown bootstrap mode | Shell bootstrap before the Worker |
| Missing key | CONFIGURATION |
| Platform HTTPS assertion set to zero | TRANSPORT |
| Missing transport mode with no PFX fixture | TLS |
| Missing managed role marker | STORAGE |
| Managed role marker set to one | STORAGE |
| Malformed key | LISTENER, before the listener starts |

The earlier clean candidate smoke returned health 200 with a valid empty store and snapshot 503 DATASET_NOT_READY. Missing candles therefore do not explain a health 503 in that verified local configuration. Azure-specific settings, storage, listener and ingress remain possible causes.

For the next check, use the exact research staging app's portal to compare only the declared nonsecret settings above. Return MATCH, MISSING or MISMATCH by name and a key-present boolean. Keep the key and unrelated settings private. The ARM settings-list endpoint returns a settings dictionary, and client-side projection still delivers the full response to the client. It was not used. A complete match of those declared settings would still leave platform-injected role and kernel conditions unresolved. [Web Apps List Application Settings](https://learn.microsoft.com/en-us/rest/api/appservice/web-apps/list-application-settings?view=rest-appservice-2025-03-01)

No Azure, SCM, registry, provider, deployment, or credential operation occurred in this pass. API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.
