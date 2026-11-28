# Release 1.15 simplified Research API recovery

| Gate | Status | Latest evidence | Blocker |
| --- | --- | --- | --- |
| G0 Baseline and reproducibility | PASS | [G0 attempt 1](#g0-attempt-1) | None |
| G1 Local API startup | PASS | [G1 attempt 5](#g1-attempt-5) | None |
| G2 Local REST and Streamlit | PASS | [G2 attempt 2](#g2-attempt-2) | None |
| G3 Source-bound images and rollback | PASS | [G3 attempt 4](#g3-attempt-4) | None |
| G4 Staging API-first acceptance | BLOCKED | [G4 attempt 12](#g4-attempt-12) | Verify a way to capture the main container's launch or fixed Worker stage before another image apply. |

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

## G4 attempt 5

BLOCKED on dcd9976ef49754bd0a590b13e8c43dc26d6d0ae0 from 2026-11-22T18:28:11.351245Z to 2026-11-22T18:28:42.214057Z.

One read-only Azure CLI settings request succeeded in 1.471 seconds. All ten declared startup settings exactly matched and AIQ_RESEARCH_API_KEY was present. Azure CLI processed the settings response internally; its output contained only fixed projections. Azure runtime-injected markers, key format and equality, storage, ingress and startup failure location remain unverified. No settings or image changed.

Next action. Check the effective main-container startup inputs or obtain a fixed failure stage before another deployment.

Azure mutations. 0.


### Declared staging settings verified through Azure CLI

Owner-authorized CLI read at 2026-11-22T18:28:11.351245Z to 2026-11-22T18:28:12.822282Z, one request, 1.471 seconds, no retry. Target was only `aiqr115researchstagingwus27f5eabb5`. The App Service list operation used HTTP POST for a read-only request. The projected resource ID matched the exact API app. All ten values in the preceding comparison matched, including explicit bootstrap `serve`. `AIQ_RESEARCH_API_KEY` was present. No setting value, unrelated setting name or credential was emitted. Azure CLI received the full settings response internally before projecting booleans.

The successful comparison narrows the problem to evidence beyond the declared setting values. The Worker process inputs, Azure-injected instance and role markers, key validity, container storage boundary and listener start still require runtime evidence. Neither an app-setting mismatch nor a particular startup failure stage was established for the failed deployment. No health request, authenticated snapshot, UI operation, Azure write, restart or deployment occurred in this pass.

API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## G4 attempt 6

BLOCKED on f5fa4163dabb80817e7798175cdb56243b831136 from 2026-11-22T18:37:01.970326Z to 2026-11-22T18:41:15.614220Z.

G4-A PASS: the earlier candidate exits at STORAGE when the platform role identifier is absent or opaque. G4-B PASS: the exact staging exception no longer assumes the role ordinal equals zero. G4-C PASS: targeted suites and seven network-isolated Docker cases passed. The corrected candidate serves 200 Alive, rejects unauthenticated snapshots with 401 and answers authenticated empty-state snapshots with 503 DATASET_NOT_READY. G4-D READY_FOR_SEPARATE_APPROVAL. Azure root cause is unverified and G4 live acceptance remains blocked.

Next action. Approve publication and digest readback of the exact corrected API image.

Azure mutations. 0.


### Startup compatibility correction and verification

Startup runs shell bootstrap `serve`, then Worker configuration, transport/TLS, storage validation, listener and `GET /health`. The ten declared staging app settings matched the prior ARM read. The shell's empty/absent mode defaults to serve. Configuration requires the private directory and API key. Platform HTTP bypasses the local PFX only after exact app/hostname, a nonempty injected instance ID, explicit transport consent and bind address 0.0.0.0 pass. Storage separately requires explicit ephemeral opt-in, the fixed private path, Linux UID 1654, writable private root-overlay permissions and the same app identity. Listeners use port 52731 for platform HTTP. The key format is checked before listener startup.

Microsoft defines WEBSITE_ROLE_INSTANCE_ID as a current instance identifier without guaranteeing a zero ordinal or its presence in this container. WEBSITE_INSTANCE_ID is separately described as a VM instance identifier. The fix removes only the exact-zero role comparison. It retains the nonempty instance check and keeps role presence as a fail-closed Azure marker outside the exact staging exception. No Azure marker is fabricated in deployment. [Microsoft App Service settings](https://learn.microsoft.com/en-us/azure/app-service/reference-app-settings#language-specific-settings) ? [Custom container port routing](https://learn.microsoft.com/en-us/azure/app-service/configure-custom-container#configure-port-number)

| Image and fixture variation | Fixed startup stage | Local result |
| --- | --- | --- |
| Earlier candidate, role absent | STORAGE | Exit 1 |
| Earlier candidate, opaque role | STORAGE | Exit 1 |
| Corrected candidate, role absent | SERVING | Health 200, unauthenticated 401, authenticated empty state 503 DATASET_NOT_READY |
| Corrected candidate, opaque role | SERVING | Health 200, unauthenticated 401, authenticated empty state 503 DATASET_NOT_READY |
| Corrected candidate, instance marker missing | TRANSPORT | Exit 1 |
| Corrected candidate, read-only storage | STORAGE | Exit 1 |
| Corrected candidate, wrong UID | STORAGE | Exit 1 |

Earlier exact local index `sha256:fefdccc43d8dec7c7b290813967fd29c936e9d95ffa30ed155d2dadd1a768393` was verified against the earlier OCI receipt. Each case used an owned network-isolated container. Health and REST used only the container namespace's loopback. All seven fixtures passed and their owned containers were removed. The empty-state response came from a running process. Azure startup, effective identity, key and filesystem were not observed in this pass. ROOT_CAUSE_UNVERIFIED.

Automation passed 87 .NET tests, 56 Python chart tests, and seven receipt tests. Seven Docker fixture cases passed. Browser and fixture-export opt-ins were excluded from the .NET run. UI source remained unchanged. The Python chart suite includes BTC volume, ETH OHLC-only and BTC to ETH to BTC state. No new real-browser or authentic-dataset acceptance is claimed.

### Corrected API immutable image and approval plan

| Item | Value |
| --- | --- |
| Source commit | f5fa4163dabb80817e7798175cdb56243b831136 |
| Source tree | 6c6926284ddec759386aefb001a8ea4eab43d02b |
| Build-context file hash manifest SHA-256 | 38adeedc7346d9d58d7e8aa19872fe9e99f9f6ee40235c6ecafee2a051518f96 |
| Retained local tag | aiq-r115-compat:api-f5fa4163dabb |
| Local OCI index and image ID | sha256:68073490abb07a4e13e7f498b9ce6105b2b1de3370fc0125287443c73a3236ea |
| Local Linux/amd64 child | sha256:47b9bc01743b05790daf489d22da467db61a042791e4cd72d07c89fedd812be0 |
| Local config | sha256:e6b1e1aed024a2b8c4169059acdadc0c759a2e91126123d06aeec0aad12f173b |
| Proposed GHCR tag | ghcr.io/samuel-santos-engineer/aiquanttradingresearch:r115-recovery-api-f5fa4163dabb |
| Previously verified staging rollback child | sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3 |

The build used the tracked API Dockerfile and bootstrap, cached pinned ASP.NET base, framework-dependent publish and source revision OCI label. Archive verification hashed exact index, Linux/amd64 manifest and config bytes. The context had only published output and bootstrap. Registry operations require separate approval. Verify the approved tag and exact registry index, Linux/amd64 child and config mapping before proposing an API-only image update. Recheck staging configuration and quota under the future approval, preserve the rollback child and use the image-only rollback procedure above on failed health or data acceptance. The UI remains excluded from API apply. No Azure, SCM, image pushes or deployment occurred.

G4-A PASS. G4-B PASS. G4-C PASS. G4-D READY_FOR_SEPARATE_APPROVAL. G4 remains blocked for live acceptance. API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## G4 attempt 7

BLOCKED on f5fa4163dabb80817e7798175cdb56243b831136 from 2026-11-22T19:17:49.996326Z to 2026-11-22T19:18:41.025111Z.

The single approved API tag was published. Raw registry OCI index and Linux/amd64 child bytes hash-matched the verified local objects. The child referenced the exact verified local config digest. G4-D READY_FOR_SEPARATE_APPROVAL. No Azure operation occurred.

Next action. Approve the exact API-only staging apply, acceptance checks, and conditional rollback.

Azure mutations. 0.


### Corrected API image publication and exact staging checkpoint

The single owner-approved GHCR tag `ghcr.io/samuel-santos-engineer/aiquanttradingresearch:r115-recovery-api-f5fa4163dabb` was published and read back by 2026-11-22T19:18:01.678672Z in 11.682 seconds. The OCI index `sha256:68073490abb07a4e13e7f498b9ce6105b2b1de3370fc0125287443c73a3236ea` and Linux/amd64 child `sha256:47b9bc01743b05790daf489d22da467db61a042791e4cd72d07c89fedd812be0` matched the local raw SHA-256 hashes. The child manifest references config digest `sha256:e6b1e1aed024a2b8c4169059acdadc0c759a2e91126123d06aeec0aad12f173b`, previously verified in the local OCI archive. No Docker login was invoked. No UI image tag, Azure read, Azure write, or deployment occurred.

The next separately approved write may change only linuxFxVersion for `aiqr115researchstagingwus27f5eabb5`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, subscription `7f5eabb5-80f5-4369-88be-b14a6de763f1`. Use the API-only image PATCH procedure above, and preserve startup mode, HTTPS-only, port 52731, authentication, storage, UI and plan.

- Candidate `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:47b9bc01743b05790daf489d22da467db61a042791e4cd72d07c89fedd812be0`
- Conditional rollback `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`

Before applying, verify quota, key channel, exact expected image and blank startup override. Confirm the configured candidate digest after applying. Run the previous bounded health gate with at most three HTTPS probes and up to 180 seconds. After verified 200 Alive, send one authenticated BTC/USD snapshot and then one ETH/USD snapshot only if BTC passes. If required acceptance fails, restore the previous image once and verify its configuration readback. No explicit restart or automatic redeployment. G4 remains blocked until that separate approval and live qualification.

API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## G4 attempt 8

BLOCKED on f5fa4163dabb80817e7798175cdb56243b831136 from 2026-11-22T19:20:04.281369Z to 2026-11-22T19:23:39.478183Z.

All three quotas were below limit, API site metadata reported Running and Normal, and the process key was available. The bounded API config read timed out, so no Azure write was sent in this attempt.

Next action. Retry only the bounded API config/web read before any mutation.

Azure mutations. 0.

## G4 attempt 9

FAIL on f5fa4163dabb80817e7798175cdb56243b831136 from 2026-11-22T19:20:41.525072Z to 2026-11-22T19:23:39.643883Z.

Config retry verified the rollback image and empty startup override. API apply acknowledged: True. Health: TRANSPORT_UNVERIFIED (no status), 503 HTTP_REJECTED, 403 HTTP_REJECTED. Snapshot requests: none. Result: API_HEALTH_FAILED. Final configured child: sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3. Rollback: RESTORED_AND_VERIFIED. UI unchanged.

Next action. Establish the candidate's Azure startup stage before another image attempt.

Confirmed Azure image mutations. 2.


### Corrected API candidate apply and outcome

The quota read showed stop requests 0/15, daily CPU 0/3,600,000 ms and short-period CPU 0/180,000 ms before apply. A single retry of the timed-out configuration read succeeded at 2026-11-22T19:20:42.991346Z. The controller rechecked the API baseline before applying. No settings, UI or production operation occurred.

| Item | Evidence |
| --- | --- |
| Target | aiqr115researchstagingwus27f5eabb5 |
| Candidate | `sha256:47b9bc01743b05790daf489d22da467db61a042791e4cd72d07c89fedd812be0` |
| Prior and conditional rollback | `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3` |
| Health | TRANSPORT_UNVERIFIED (no status), 503 HTTP_REJECTED, 403 HTTP_REJECTED |
| Authenticated snapshots | none |
| Rollback | RESTORED_AND_VERIFIED |
| Last configured API image | `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3` |

The ownership-bounded controller ran from 2026-11-22T19:21:13.161929Z to 2026-11-22T19:23:22.222931Z in 129.061 seconds, made 4 API config reads and attempted 2 image PATCHes. The controller did not explicitly restart the app. Configuration evidence alone does not establish the running image or the cause of an unsuccessful startup. No Azure dataset or public UI acceptance is inferred.

API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## G4 attempt 10

BLOCKED on 4f6a31b2b22ab504c7ed7b1ed0985c9e814ef598 from 2026-11-22T19:34:12.184615Z to 2026-11-22T19:34:39.960742Z.

The one authorized SCM inventory GET returned HTTP 403 and access denied with reason unknown. The collector stopped at headers. No inventory or log body was read, no file was selected and no stage was recovered. The first local launcher failed during import before authentication or an SCM request.

Next action. Obtain an existing main-container stage record from the exact window through the owner portal if available.

Azure mutations. 0.


### Historical startup evidence via Azure CLI

As requested, the collection used the installed Azure CLI App Service bearer session for one SCM inventory GET to the research staging app. Historical event window was 2026-11-22 19:21:13.161929 to 19:23:22.222931 UTC. The UTC completion stamp was 2026-11-22T19:34:13.801615+00:00. The owned worker reported 1.617 seconds and HTTP 403 ACCESS_DENIED at the inventory response header gate. Application body bytes consumed were zero. The conditional application file request was not sent. This SCM access denial does not establish the cause of the earlier API health 403.

Six offline checks exercised exact UTC boundaries, strict startup marker matching and unique/ambiguous/Kudu-only file selection. The local launcher initially could not import the tracked helper in the installed CLI's isolated interpreter. A local import check confirmed the issue, and the same scoped worker was launched with the required import path. Only one live SCM GET occurred. No Azure write, ARM query, setting read, restart, UI, health, or alternate log request occurred. No raw headers, log content, or token was emitted.

Startup stage and running image remain unverified. API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## G4 attempt 11

BLOCKED on c363879b7f85082405135974de28689a85bf4cc3 from 2026-11-22T20:19:54.935966Z to 2026-11-22T20:20:31.264500Z.

The quota snapshot showed stop requests 0/15 and the operator independently reported the app Running before this retry. The single SCM request failed at the transport stage in 10.585 seconds. No HTTP status, file inventory, log body or startup marker was captured.

Next action. Use an existing retained main-container record if the portal can access the exact historical window.

Azure mutations. 0.

Retry completed at 2026-11-22T20:20:05.520966+00:00 for the unchanged November 22 19:21:13?19:23:22 UTC evidence window. No VFS request or additional retry was sent. No ARM read, health probe, Azure write or restart occurred. The transport exception was classified generically. API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## G4 attempt 12

BLOCKED on a04aba10bedc4fc51cb6cef7ee9af0dfae533311 from 2026-11-22T20:38:06.123504Z to 2026-11-22T20:38:06.124534Z.

The owner-provided Portal report aligned the 2026-11-22 19:21:13 to 19:23:22 UTC impact window with a site configuration update and UpdateWebSite operation. It reported no Resource Health availability transition, no alert, no chartable series and no populated App Service Log Analytics table for the window. No direct main-container record established a Worker stage. A pre-start failure and a launch-time logging gap both remain possible.

Next action. Verify a way to capture the main container's launch or fixed Worker stage before another image apply.

Azure mutations. 0.


### Owner-provided Azure Portal incident report

The report describes the same research API target and the November 22, 2026 19:21:13?19:23:22 UTC window as the corrected-image attempt. A site configuration update and UpdateWebSite completed just before the reported impact. The tracked controller recorded an image-only PATCH, the candidate configuration readback, unsuccessful health checks and a verified rollback. The Portal event timing corroborates the configuration transition. It supplies no running-image attestation or failing Worker stage.

The report identifies no Azure platform outage for the resource, no Resource Health availability transition, and no alert. No chartable runtime series or populated App Service Log Analytics table was available for the window. The separately supplied Kudu-sidecar excerpt was outside it. The combined evidence leaves pre-start failure and a main-container logging gap unresolved. The owner-reported current Running state is recovery-state management evidence. API health remains unverified.

Before another separately authorized apply, confirm a capture path capable of distinguishing a main-container launch event from a Worker startup marker. The previous image was already restored and verified. This review made no Azure or SCM request and no alert, logging or configuration change. API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## N0 attempt 1

BLOCKED on f5fa4163dabb80817e7798175cdb56243b831136 from 2026-11-22T20:49:13.803949Z to 2026-11-22T20:51:17.493640Z.

Subscription and existing plan identity verified. West US 2 advertised Linux F1; watched existing plan quotas were all zero. Existing API Running, HTTPS-only and empty startup, with rollback image matched. The exact corrected Linux candidate passed health locally with the existing identity, then exited 1 at TRANSPORT with only the proposed new app name and hostname substituted. New plan admission and resource names remain unverified because the candidate cannot run on the proposed identity.

Next action. Review a scoped image change allowing only the exact parallel lab identity.

Azure mutations. 0.

## N1 attempt 1

BLOCKED on f5fa4163dabb80817e7798175cdb56243b831136 from 2026-11-22T20:51:17.573157Z to 2026-11-22T20:51:17.573438Z.

Provisioning stopped before writes because the pinned image rejects the new App Service identity. No resource group, plan, or app was created.

Next action. Resolve N0 image compatibility before creating resources.

Azure mutations. 0.

## N2 attempt 1

BLOCKED on f5fa4163dabb80817e7798175cdb56243b831136 from 2026-11-22T20:51:17.651193Z to 2026-11-22T20:51:17.651472Z.

No new app, diagnostic setup, image application, launch or startup capture.

Next action. Revisit the built-in log capture path after N0 resolution.

Azure mutations. 0.

## N3 attempt 1

BLOCKED on f5fa4163dabb80817e7798175cdb56243b831136 from 2026-11-22T20:51:17.729468Z to 2026-11-22T20:51:17.729747Z.

No new app or staging URL served. No HTTPS probe sent.

Next action. Run bounded health probes only after N2.

Azure mutations. 0.

## N4 attempt 1

BLOCKED on f5fa4163dabb80817e7798175cdb56243b831136 from 2026-11-22T20:51:17.806654Z to 2026-11-22T20:51:17.806925Z.

No authenticated request or dataset copy occurred.

Next action. Run the approved instrument checks only after N3.

Azure mutations. 0.

## N5 attempt 1

PASS on f5fa4163dabb80817e7798175cdb56243b831136 from 2026-11-22T20:51:17.883750Z to 2026-11-22T20:51:17.884007Z.

The local reproduction isolates a deterministic incompatibility of the pinned image with the fresh identity. Azure allocation and startup for the fresh app were not attempted. The old staging startup cause remains unknown.

Next action. Approve a narrowly scoped new image retaining the original staging and storage safeguards.

Azure mutations. 0.


### Parallel experiment decision matrix

| Check | Original research staging | Proposed parallel experiment |
| --- | --- | --- |
| Region and plan | West US 2, original Linux plan and app association verified | West US 2, separate Linux F1 advertised for the subscription. Allocation and create permissions remain unverified. |
| Image | Currently configured rollback child sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3. The earlier corrected candidate failed the health gate before rollback. | Exact intended child sha256:47b9bc01743b05790daf489d22da467db61a042791e4cd72d07c89fedd812be0. Previous registry mapping and cached local index sha256:68073490abb07a4e13e7f498b9ce6105b2b1de3370fc0125287443c73a3236ea were matched to source f5fa4163dabb80817e7798175cdb56243b831136. |
| Identity and transport | The image contains the existing site and hostname literals. The local control served HTTP 200 Alive on port 52731. | The image rejects the new site/hostname pair at TRANSPORT before the listener, reproduced with otherwise identical settings. Storage also requires the original pair. |
| Configuration and storage | Fresh HTTPS-only and empty startup readback. The ten app settings were verified in the earlier owner-authorized comparison. | Intended to retain the same nonsecret transport and port settings and use private ephemeral storage, with a separately supplied key. No runtime values were copied. |
| Startup, health and snapshots | Last corrected-image attempt showed no status, 503, then 403. Startup stage and current health remain unknown. | No Azure container, startup marker, health request or snapshot result. The local control returned authenticated 503 DATASET_NOT_READY with empty storage. |
| Quotas and costs | Existing-plan observation began at 2026-11-22T20:49:37.691744Z. Stop requests 0/15, daily CPU 0/3,600,000 ms, short CPU 0/180,000 ms. | No new plan or resource was created. No new ongoing cost. F1 admission has not been tested. |

Proposed names are `rg-aiq-r115-parallel-lab-wus2-7f5eabb5`, `asp-aiq-r115-parallel-lab-wus2-7f5eabb5`, and `aiqr115-parallel-lab-wus2-7f5eabb5`. They are provisional and were not reserved or checked for name collisions. Azure F1 uses shared compute. Separate plan placement cannot establish physical-host isolation. [Azure CLI F1 location command](https://learn.microsoft.com/en-us/cli/azure/appservice?view=azure-cli-latest#az-appservice-list-locations) ? [App Service plans](https://learn.microsoft.com/en-us/azure/app-service/overview-hosting-plans)

The exact Git source shows two allowlist comparisons for the old identity in transport and storage. Azure supplies the app identity for its actual container. Reusing the old app identity in the new container would invalidate the experiment. A new image should add only the exact proposed lab app name and matching hostname, preserving the existing checks and rejecting other identities. The provision-and-deploy authority specified the published child above. It did not authorize an image change. No paid tier was needed or selected. [App Service environment reference](https://learn.microsoft.com/en-us/azure/app-service/reference-app-settings#app-environment)

Proposed settings checklist for a future compatible image: preserve the ten declared nonsecret values from the staging comparison and set platform HTTPS-only. Supply a dedicated lab API key privately. No real key was inspected or transferred. Let Azure populate site, hostname, and instance markers, and use the new app's own private ephemeral path without reusing volumes, data, identity, or credentials. Existing built-in Linux filesystem logs are the first capture option to evaluate after the new app exists. Retention, availability, and bounded retrieval on the new app remain untested. [App Service diagnostics](https://learn.microsoft.com/en-us/azure/app-service/troubleshoot-diagnostic-logs#enable-application-logging-linux-or-container)

Azure mutations **0**. No resource IDs, endpoint, or continuing costs were created. The baseline receipt tests ran after the read-only checks and before the schema extension. The updated schema and append-only evidence were checked before commit. No Azure write phase occurred. API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## N0 attempt 2

BLOCKED on fb6d9a5e6567e5ab78cd026c78ea9bd278997305 from 2026-11-22T20:55:05.397570Z to 2026-11-22T20:58:07.447008Z.

The exact two staging site/hostname pairs now share one checked allowlist in transport and storage. Both served health 200 on port 52731 and preserved snapshot 401/503 behavior in the isolated empty-store fixture. Mixed and unknown identities exited 1 at TRANSPORT. Read-only storage and wrong UID exited 1 at STORAGE. Local OCI SHA-256 checks passed on the source-bound candidate. N0 still awaits registry publication of the new candidate and a fresh Azure provisioning preflight.

Next action. Approve publication and registry verification of the exact parallel API candidate.

Azure mutations. 0.


### Parallel identity correction and locally qualified image

Transport and storage share an exact site/hostname-pair check. The allowlist accepts `aiqr115researchstagingwus27f5eabb5` with its existing Azure hostname and `aiqr115-parallel-lab-wus2-7f5eabb5` with `aiqr115-parallel-lab-wus2-7f5eabb5.azurewebsites.net`. The valid pair checks remain case-sensitive. The nonempty platform instance marker, transport opt-in, private ephemeral storage constraints, UID, HTTPS assertions, authentication, and port 52731 still apply. Neither experiment sets Azure's managed identity markers.

| Network-isolated Docker cases | Result |
| --- | --- |
| Original identity and exact parallel identity with absent role marker | Both reached SERVING, health 200, unauthorized snapshot 401, valid-key empty-store snapshot 503 |
| Both mixed identity pairs and a completely unlisted pair | All exited 1 at TRANSPORT |
| Exact parallel identity with read-only root or wrong UID | Both exited 1 at STORAGE |

All seven containers were removed after verification. Scoped .NET suites passed 93 selected tests, and all eight receipt tests passed. Browser and fixture-export opt-ins were excluded. The synthetic Docker tests do not establish a real Azure hostname, capacity, health or data.

| Image identity | Verified local value |
| --- | --- |
| Source commit | fb6d9a5e6567e5ab78cd026c78ea9bd278997305 |
| Git tree | 6c076ce5b4aa4fad4cc145a5852defeee84624b2 |
| Build-context file manifest SHA-256 | 6a59de3687bc88bc9a96d593157343723008a59ab38cd57839943ff5bc9428e2 |
| Local tag | aiq-r115-parallel:api-fb6d9a5e6567 |
| Local index and image ID | sha256:60ad871ee2b74225ef9bf24e3432f9ad64f3ac4e93862de145c9ad21e7e8ac41 |
| Linux/amd64 child | sha256:a972845fd52ad0487116c67d022770421abfa4242ca5c94928a51e4eaa89d914 |
| Config digest | sha256:4bdc4b6abf70d3cc76a739a69f1f61840e1b681f8aa61c0c43160459d64140dd |
| Proposed GHCR tag | ghcr.io/samuel-santos-engineer/aiquanttradingresearch:r115-parallel-api-fb6d9a5e6567 |

The verified image uses the pinned cached ASP.NET base, tracked bootstrap and exact revision label. Next approval is for GHCR publication and digest readback of that image. Before any separate creation or apply, verify actual default hostname and exact app identity against the allowed pair, F1 admission and the private dedicated-lab key channel. If Azure assigns a different default hostname, stop at that mismatch. N1?N4 remain blocked. No Azure or GHCR operations, new resources, or incremental cloud cost occurred in this pass.

API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## N0 attempt 3

BLOCKED on fb6d9a5e6567e5ab78cd026c78ea9bd278997305 from 2026-11-23T01:36:52.229595Z to 2026-11-23T01:37:39.603893Z.

One parallel API GHCR tag was published. The registry index and child bytes matched the local hashes, and the child manifest referenced the expected locally verified config. No Azure operation, UI push, or credential disclosure. N0 still needs a fresh provisioning preflight, private lab key channel and explicit approval for the replacement child.

Next action. Approve fresh N0 preflight and F1-only provisioning using the verified parallel candidate.

Azure mutations. 0.


### Exact parallel image publication

Published one tag, `ghcr.io/samuel-santos-engineer/aiquanttradingresearch:r115-parallel-api-fb6d9a5e6567`. Registry verification finished at 2026-11-23T01:37:08.565572Z in 16.336 seconds. OCI index `sha256:60ad871ee2b74225ef9bf24e3432f9ad64f3ac4e93862de145c9ad21e7e8ac41` and Linux/amd64 child `sha256:a972845fd52ad0487116c67d022770421abfa4242ca5c94928a51e4eaa89d914` matched the previously verified local byte hashes. The child manifest referenced config digest `sha256:4bdc4b6abf70d3cc76a739a69f1f61840e1b681f8aa61c0c43160459d64140dd`. No other image was published.

The next approval is for the corrected child on the exact proposed parallel app and independent F1 plan in West US 2. Reverify subscription, F1 admission, names, actual app hostname, and a dedicated private key channel before use. The app name and expected hostname remain `aiqr115-parallel-lab-wus2-7f5eabb5` and `aiqr115-parallel-lab-wus2-7f5eabb5.azurewebsites.net`. Keep Azure-managed identity values intact. The new app starts with its own empty ephemeral storage. Old staging, Streamlit and production remain outside the scope.

N0 and N1?N4 remain blocked for live execution pending that scope, with N5 comparison recorded. There were zero Azure reads or mutations in this publication pass, no resources were created and no incremental Azure cost was incurred. API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## N0 attempt 4

BLOCKED on 599a6ce3dd7518631926a3bbdab2831aab68dff3 from 2026-11-23T01:44:27.984553Z to 2026-11-23T01:44:27.985094Z.

No dedicated lab API key was present in the available process channels. The required stop condition prevented any Azure check or provisioning. The existing staging API key was not consulted or reused. The exact published candidate is retained.

Next action. Approve generation and private in-memory transfer of one new dedicated lab key for the parallel experiment.

Azure mutations. 0.


### Parallel provisioning credential stop

Owner approved fresh preflight and F1-only provisioning using the published parallel Linux/amd64 child `sha256:a972845fd52ad0487116c67d022770421abfa4242ca5c94928a51e4eaa89d914`. The initial dedicated process key presence/format gate returned unavailable. For the approved scope, there was no pre-established private transfer channel for the new lab. The previous staging key was outside the experiment and was not inspected.

N0?N4 remain blocked. The prior N5 comparison stands. No Azure read or write, resource creation, authentication probe, or new Azure cost occurred. Fresh quota and F1 admission were not queried after the stop. The next proposed action is explicit authorization to generate a separate lab-only key in memory, privately configure the new app on the existing HTTPS management channel, and reuse that key only in the same owned activation/probe session. Keep it out of argv, files, console output, receipts and Git. No old credentials are needed.

API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## N0 attempt 5

PASS on fb6d9a5e6567e5ab78cd026c78ea9bd278997305 from 2026-11-23T01:50:35.190751Z to 2026-11-23T02:01:15.900188Z.

West US 2 advertised Linux F1. Existing regional free plans 1 against documented 10. Regional free VM counter 1/31. Proposed group absent and app name available. Old API rollback config matched. Dedicated lab-only memory channel generated for new settings and probes.

Next action. Provision the bounded new F1 lab resources.

Confirmed Azure mutations. 0.

## N1 attempt 2

BLOCKED on fb6d9a5e6567e5ab78cd026c78ea9bd278997305 from 2026-11-23T01:55:10.524934Z to 2026-11-23T02:01:16.070708Z.

A prior local controller launch failed before HTTPS because a function shadowed the HTTP module; the group was confirmed absent and the offline binding case passed. The corrected controller created the group and F1 plan. The app PUT timed out at its 16-second request bound before a response, leaving the app outcome initially unknown.

Next action. Reconcile the exact app by readback without resending the create.

Confirmed Azure mutations. 2.

## N1 attempt 3

PASS on fb6d9a5e6567e5ab78cd026c78ea9bd278997305 from 2026-11-23T01:56:04.642046Z to 2026-11-23T02:01:16.256310Z.

Readback confirmed all three lab resources, exact default hostname, blank initial image/startup and no configured storage mounts. The app create was not repeated. Settings verified: True. The generated lab key was transmitted only in the authenticated HTTPS request and stayed out of local files, process arguments and output.

Next action. Enable best-effort filesystem diagnostics and apply the exact parallel image.

Confirmed Azure mutations. 2.

## N2 attempt 2

PASS on fb6d9a5e6567e5ab78cd026c78ea9bd278997305 from 2026-11-23T01:56:45.891334Z to 2026-11-23T02:01:16.430792Z.

Filesystem logging: UNVERIFIED. Candidate configuration verified: True. Startup classification: STARTUP_MARKER_UNOBSERVED_LOGGING_UNVERIFIED. Startup HTTP status: None. Fixed stage: None. A running digest was not attested.

Next action. Evaluate the new app's bounded health results.

Confirmed Azure mutations. 1.

## N3 attempt 2

FAIL on fb6d9a5e6567e5ab78cd026c78ea9bd278997305 from 2026-11-23T01:57:24.497284Z to 2026-11-23T02:01:16.599049Z.

no status TRANSPORT_OR_RESPONSE_UNVERIFIED, 503 HEALTH_HTTP_REJECTED, 503 HEALTH_HTTP_REJECTED

Next action. Collect existing main-container startup evidence before any new apply.

Confirmed Azure mutations. 0.

## N4 attempt 2

BLOCKED on fb6d9a5e6567e5ab78cd026c78ea9bd278997305 from 2026-11-23T01:56:45.891334Z to 2026-11-23T02:01:16.781420Z.

Not run. No authenticated dataset accepted. No private dataset transfer was performed.

Next action. Resolve new app startup before authenticated probes.

Confirmed Azure mutations. 0.

## N5 attempt 2

PASS on fb6d9a5e6567e5ab78cd026c78ea9bd278997305 from 2026-11-23T01:59:10.634619Z to 2026-11-23T02:01:16.980845Z.

Original staging health remains unverified after rollback. New lab health: no status TRANSPORT_OR_RESPONSE_UNVERIFIED, 503 HEALTH_HTTP_REJECTED, 503 HEALTH_HTTP_REJECTED. New lab snapshots: Not run. Confirmed Azure writes confined to the new lab: 5.

Next action. Collect existing main-container startup evidence before any new apply.

Confirmed Azure mutations. 0.


### Parallel staging live decision matrix

| Observation | Old staging | New parallel lab |
| --- | --- | --- |
| Resource group and plan | Original West US 2 shared staging plan, outside write scope | West US 2 Linux F1, separate group and verified distinct plan |
| Image | Last verified original rollback reference | sha256:a972845fd52ad0487116c67d022770421abfa4242ca5c94928a51e4eaa89d914 configured=True. Actual running digest unverified. |
| Site and settings | Last rollback, HTTPS-only, and blank startup readback | Exact aiqr115-parallel-lab-wus2-7f5eabb5.azurewebsites.net, HTTPS-only, exact lab settings verified=True. Dedicated key stored only by the new app. |
| Storage | Historical staging ephemeral contract, current contents unknown | No configured mounts or import. New app has isolated ephemeral storage. |
| Startup marker | Unobserved in failed corrected-image window | STARTUP_MARKER_UNOBSERVED_LOGGING_UNVERIFIED; HTTP None; stage None |
| Health and snapshots | No current acceptance | no status TRANSPORT_OR_RESPONSE_UNVERIFIED, 503 HEALTH_HTTP_REJECTED, 503 HEALTH_HTTP_REJECTED. Not run. |

| Azure operation | Attempt UTC | HTTP status | Disposition |
| --- | --- | --- | --- |
| CREATE_GROUP | 2026-11-23T01:55:12.542643Z | 201 | Confirmed |
| CREATE_LINUX_F1_PLAN | 2026-11-23T01:55:18.722365Z | 200 | Confirmed |
| CREATE_LINUX_APP | 2026-11-23T01:55:24.571082Z | No response | Confirmed by fresh app readback |
| PRIVATE_LAB_SETTINGS | 2026-11-23T01:56:49.981110Z | 200 | Confirmed |
| ENABLE_FILESYSTEM_LOGGING | 2026-11-23T01:56:51.341522Z | No response | Unverified, may have applied |
| APPLY_PINNED_IMAGE | 2026-11-23T01:57:07.354445Z | 200 | Confirmed |

The logging write reached its request bound before an HTTP response. The controller continued under the best-effort logging scope. It did not collect startup files because logging was unverified. No root cause or Azure-side I/O exception was established.

Health request start times (UTC) and outcomes were 2026-11-23T01:57:24.497284Z (no HTTP status), 2026-11-23T01:58:09.497833Z (503), 2026-11-23T01:59:09.497554Z (503).

Exact verified new resource IDs:

- `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r115-parallel-lab-wus2-7f5eabb5`
- `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r115-parallel-lab-wus2-7f5eabb5/providers/Microsoft.Web/serverfarms/asp-aiq-r115-parallel-lab-wus2-7f5eabb5`
- `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r115-parallel-lab-wus2-7f5eabb5/providers/Microsoft.Web/sites/aiqr115-parallel-lab-wus2-7f5eabb5`

New site URL `https://aiqr115-parallel-lab-wus2-7f5eabb5.azurewebsites.net/`. The release health gate remains separate from this experiment. No paid supporting resources, custom domain, UI, data import, explicit restart or deletion were requested. F1 is the free shared-compute tier, and the separate plan does not prove physical-host isolation. No ongoing paid service was selected or observed in this experiment. [App Service hosting plans](https://learn.microsoft.com/en-us/azure/app-service/overview-hosting-plans)

The controller returned from configuration and observation at 2026-11-23T01:59:10.634619Z with 7 management read requests in the resumed phase. The initial app PUT had no HTTP status; fresh readback proved it had taken effect. In total, 5 confirmed Azure writes were recorded, with none against the old API, UI or production. No credentials, raw logs, or dataset content are included in the receipt. Filesystem log retention was not exposed by the bounded logging readback.

API_HEALTH_ACCEPTED=false for original staging. PARALLEL_API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

## N2 attempt 3

PASS on e8cf11f18e92995102085f81083c246b2ffc28ae from 2026-11-23T02:13:19.877045Z to 2026-11-23T02:13:44.002047Z.

At 2026-11-23T02:13:24.488697Z the read-only worker completed. ARM returned 200 and verified filesystem application logging at Information. SCM returned 403 at the inventory header gate. Zero SCM body bytes were read and no file was requested. The 403 reason, main-container role, running digest and Azure-side exception status remain unknown. The prior pinned image configuration readback and failed N3 health window stand. There were no new health or snapshot requests.

Next action. Use the parallel app's existing Azure portal log view to seek a main-container record from 2026-11-23 01:57:07?01:59:10 UTC. Capture only timestamp, role, startup stage or exception type. If unavailable, retain startup cause as unknown.

Azure mutations. 0.

## N5 attempt 3

PASS on b9a3498d44bd5cbb6b80c19241aa570d11a7d130 from 2026-11-23T02:17:13.413624Z to 2026-11-23T02:17:37.690602Z.

At 2026-11-23T02:17:19.688758Z the bounded Azure CLI-authenticated read completed with HTTP 200. The exact detector identity matched. One response row was inspected in memory and yielded zero records with both an in-window UTC timestamp and an explicit main-container role. No raw row, startup stage, exception type or image identity was emitted. Startup cause and Azure-side exception status remain unknown. The previous SCM 403 and N3 failure remain historical evidence. This pass completed the comparison only. No portal browser session, new health request, snapshot or Azure write occurred. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

Next action. If the signed-in Azure portal has retained main-container evidence for the same window, capture only its timestamp, role and fixed failure stage or exception type. Otherwise, a new startup capture needs its own plan and approval.

Azure mutations. 0.

## O0 attempt 1

BLOCKED on 3e8b67cd1ebde8a8499c724508a671e2b4c33c68 from 2026-11-23T02:48:55.366200Z to 2026-11-23T02:48:55.366805Z.

Secure channel BLOCKED. The prior controller generated a dedicated key in its own memory, verified its Azure settings write and cleared its local references on exit. No retained dedicated channel was established by this run. An operator environment variable exists, but its value and association with the parallel lab were not verified. No key value or Azure settings were read. The current cloud baseline, quota and concurrency checks were not run after this stop condition. The historical parallel configured image and potential image-only rollback target are sha256:a972845fd52ad0487116c67d022770421abfa4242ca5c94928a51e4eaa89d914, pending fresh verification. Running digest UNKNOWN. Microsoft documents the preview Linux startup log command. Bounded live access on this app is unverified. Earlier SCM 403 and detector attribution gaps were not retested.

Next action. Establish an owner-controlled in-memory channel for the exact dedicated parallel-lab key. If the key cannot be recovered through an existing approved channel, obtain separate authority for a parallel-app-only replacement and private handoff.

Azure mutations. 0.

## O1 attempt 1

BLOCKED on 3e8b67cd1ebde8a8499c724508a671e2b4c33c68 from 2026-11-23T02:48:55.454992Z to 2026-11-23T02:48:55.455419Z.

NOT RUN because O0 blocked. Offline review located the explicit parallel hostname in the existing storage and transport boundary. The parallel app is already in that allowlist and no evidence ties that check to the previous 503s. No source change, build, local runtime, OCI hash or publication.

Next action. Establish an owner-controlled in-memory channel for the exact dedicated parallel-lab key. If the key cannot be recovered through an existing approved channel, obtain separate authority for a parallel-app-only replacement and private handoff.

Azure mutations. 0.

## O2 attempt 1

BLOCKED on 3e8b67cd1ebde8a8499c724508a671e2b4c33c68 from 2026-11-23T02:48:55.535017Z to 2026-11-23T02:48:55.535655Z.

NOT RUN because O0 blocked. No image apply, launch, capture, health GET or BTC/ETH snapshot. HTTP status and startup stage UNKNOWN.

Next action. Establish an owner-controlled in-memory channel for the exact dedicated parallel-lab key. If the key cannot be recovered through an existing approved channel, obtain separate authority for a parallel-app-only replacement and private handoff.

Azure mutations. 0.

## O3 attempt 1

BLOCKED on 3e8b67cd1ebde8a8499c724508a671e2b4c33c68 from 2026-11-23T02:48:55.615221Z to 2026-11-23T02:48:55.615589Z.

NOT RUN because O0 blocked. No rollback triggered and no new evidence distinguishes container failure from a platform hypothesis. API_HEALTH_ACCEPTED=false. PARALLEL_API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

Next action. Establish an owner-controlled in-memory channel for the exact dedicated parallel-lab key. If the key cannot be recovered through an existing approved channel, obtain separate authority for a parallel-app-only replacement and private handoff.

Azure mutations. 0.

## O0 attempt 2

BLOCKED on 93950868841471a6a313aa0c181a4a5ca9cbd7a5 from 2026-11-25T02:21:47.760134Z to 2026-11-25T02:35:49.337278Z.

At 2026-11-25T02:21:52.273848Z UTC the first read returned HTTP 400 with no writes. The local controller used the Web Apps API version for the Resource Groups provider.

Next action. Repeat only the read-only baseline with the established Resource Groups API version.

Confirmed Azure mutations. 0.

## O0 attempt 3

PASS on 93950868841471a6a313aa0c181a4a5ca9cbd7a5 from 2026-11-25T02:22:23.117641Z to 2026-11-25T02:35:49.700528Z.

Group, plan, quota, app and config/web returned 200. The first config/logs read produced no HTTP status. One isolated GET confirmed Information logging at 2026-11-25T02:22:58.586996Z. Plan F1 Linux West US 2. Stop requests 0/15, daily CPU 0/3600000 ms, short CPU 0/180000 ms. App Running, enabled, usage and availability Normal, HTTPS-only, port 52731 and blank startup. Exact-app Activity Log returned 200 and zero rows in the 30-minute window at 2026-11-25T02:24:39.795657Z. This is a bounded observation. The prior key name was present, and its value was discarded during private parsing. Preview CLI startup inventory returned 200 with 1 metadata record at 2026-11-25T02:25:27.833071Z, capped at 32768 bytes. No historical body read. The previously configured image and image-only rollback target were verified as sha256:a972845fd52ad0487116c67d022770421abfa4242ca5c94928a51e4eaa89d914. The owner authorized a new dedicated key. Its generation and use are confined to the same O2 worker.

Next action. Locally qualify observable startup and publish the exact tested API candidate.

Confirmed Azure mutations. 0.

## O1 attempt 2

PASS on 93950868841471a6a313aa0c181a4a5ca9cbd7a5 from 2026-11-25T02:25:27.833071Z to 2026-11-25T02:35:49.942741Z.

101 .NET tests passed, zero skipped. Seven Linux cases passed and their owned containers were removed. Positive case used a synthetic consistent lab hostname with explicit opt-in and emitted BOOTSTRAP_ENTRY followed by six fixed Worker phases. Local GET /health returned 200 Alive, unauthenticated snapshot 401, fake-key empty-store snapshot 503 DATASET_NOT_READY. Invalid bootstrap mode, absent key, transport assertion, missing TLS, read-only storage and wrong UID were rejected. Source 93950868841471a6a313aa0c181a4a5ca9cbd7a5. Index sha256:a24c6ba6386d217884a221d7217dadbe168cb3d869e38c452df9604fe1c30bf0. Linux child sha256:7d3afdd26b4f95e0d58089ea5770706b59a2cead0951b6c5124ba062d2b10972. Config sha256:3b589fef96f73ff405947e3f0d9c1b55e3651fc6c8800e6b9a22322204306a06. Build context SHA-256 5affd6ffd3a8dbcda9dc5f4835115746cc60f077806de6592aae64b1a0cbbfc4. Registry readback verified all image identities at ghcr.io/samuel-santos-engineer/aiquanttradingresearch:r115-observable-api-939508688414. No Azure writes in this gate. No local Streamlit run.

Next action. Recheck cloud preconditions and apply one candidate configuration.

Confirmed Azure mutations. 0.

## O2 attempt 2

FAIL on 93950868841471a6a313aa0c181a4a5ca9cbd7a5 from 2026-11-25T02:33:08.669687Z to 2026-11-25T02:35:50.157066Z.

Controller concluded 2026-11-25T02:35:22.981065Z. Result HEALTH_FAILED. Last control stage ROLLBACK_IMAGE_READBACK. Operations IMAGE_AND_KEY_SINGLE_PATCH at 2026-11-25T02:33:14.350258Z 200, ROLLBACK_IMAGE_ONLY at 2026-11-25T02:35:20.840612Z 200. Candidate config verified True. Replacement verification True. Other settings verification True. The replacement was used only in process memory and Azure configuration. No local copy was retained. Health 2026-11-25T02:33:29.350745Z no HTTP status TRANSPORT_OR_RESPONSE_UNVERIFIED, 2026-11-25T02:34:14.350901Z 503 HEALTH_REJECTED, 2026-11-25T02:35:14.350515Z 503 HEALTH_REJECTED. Snapshots not run. Startup LOGGING_UNAVAILABLE. HTTP 200 failed the text response gate, so the body was not read and no startup records were recovered. Running digest remains unverified. No credential persisted to local artifacts or reports.

Next action. Evaluate acceptance and the conditional image-only rollback outcome.

Confirmed Azure mutations. 1.

## O3 attempt 2

FAIL on 93950868841471a6a313aa0c181a4a5ca9cbd7a5 from 2026-11-25T02:35:22.981065Z to 2026-11-25T02:35:50.422085Z.

Health accepted False. O3 accepted False. Startup classification LOGGING_UNAVAILABLE. Rollback status IMAGE_RESTORED. Azure readback matched the pre-experiment image. The rollback request changed the image field only. Pre-experiment parallel child sha256:a972845fd52ad0487116c67d022770421abfa4242ca5c94928a51e4eaa89d914. Candidate child sha256:7d3afdd26b4f95e0d58089ea5770706b59a2cead0951b6c5124ba062d2b10972. No post-rollback health request. The original staging API and UI and production were outside scope. No deployment outcome identifies an Azure platform defect by itself. API_HEALTH_ACCEPTED=false. PARALLEL_API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

Next action. Offline, qualify a bounded collector for both documented Azure CLI startup-log response forms before any new launch.

Confirmed Azure mutations. 1.

### Observable startup configuration decision

The existing ephemeral-storage scope and explicit opt-in now gate both portable Azure transport and the storage boundary. The app must supply a consistent platform site-name and default hostname pair, instance identity, HTTPS and bind configuration. The new checks retain the original Azure port and kernel/private-directory requirements. This configuration check is not platform attestation.

The combined config update was selected because changing settings separately can recycle the app before the candidate is configured. The one request was constrained to the lab and used a memory-only replacement. [App Service update configuration](https://learn.microsoft.com/en-us/rest/api/appservice/web-apps/update-configuration) [App Service CLI startup logs](https://learn.microsoft.com/en-us/cli/azure/webapp/log/startup)

## O1 attempt 3

PASS on 9fd2d00ae923f4b98dc237d23ca8902d946453f1 from 2026-11-25T02:42:32.295302Z to 2026-11-25T02:42:32.296707Z.

124 diagnostic tests passed, including six new startup-show tests. Eight receipt tests passed. The header gate admits HTTP 200 plaintext or UTF-8 JSON with a top-level string content field and optional allowlisted string metadata. It rejects redirects, authentication challenges, upgrades, unsupported media and compressed bodies before consuming chunks. Both formats share the under-16-KiB wire cap and require complete UTC-prefixed records in a caller-supplied window of at most 180 seconds. Up to 24 fixed observations are returned. File metadata and raw messages are discarded. Main-container role, startup attempt and runtime image remain unverified. The prior HTTP 200 response body was never read and its format remains unknown. This pass used synthetic fixtures only. Zero Azure requests and writes.

Next action. Wire the header gate and bounded iterator into the one-shot runner before seeking approval for another live launch.

Azure mutations. 0.

The Azure CLI client handles text/plain and otherwise delegates to JSON decoding. The collector accepts a narrower JSON object with a string content field. Azure has not verified this envelope for the parallel lab. The eventual caller must verify the exact SCM route and TLS, reject ambiguous headers, enforce the request deadline and input cap before reading, and avoid redirects, retries and automatic decompression. [Azure CLI startup-log reference](https://learn.microsoft.com/en-us/cli/azure/webapp/log/startup) [Versioned Azure CLI implementation](https://github.com/Azure/azure-cli/blob/azure-cli-2.87.0/src/azure-cli/azure/cli/command_modules/appservice/custom.py#L5807)

## O1 attempt 4

PASS on 33d51542d062662e67537365e46f1f501452565d from 2026-11-25T02:56:29.849214Z to 2026-11-25T02:56:29.850264Z.

130 diagnostic and runner tests passed, including six runner tests. Eight receipt tests passed. One additional fake full-controller integration passed with a combined apply, one startup JSON response and one image-only rollback readback. The rollback preserved the fake replacement key and unrelated settings. The startup request targets the existing parallel SCM host and latest startup-log path, with a read deadline of eight seconds and a 16383-byte accepted body cap. Duplicate relevant headers and conflicting transfer framing block body consumption. Health allows up to three probes in 180 seconds. Missed probe slots are skipped and no connection starts without at least three seconds remaining. Snapshots require the exact HTTP 200 Alive response. Apply readback or rollback drift blocks further action. The local wrapper has no live command entry point. No Azure, registry or Docker operations occurred.

Next action. Obtain separate authorization for any new live startup attempt and identify its approved private key channel before using a live transport.

Azure mutations. 0.

## O0 attempt 4

BLOCKED on 908aa887bbdf1a7ec11e73b0f3e80712a92a4fb4 from 2026-11-25T03:16:20.689145Z to 2026-11-25T03:20:25.622391Z.

The app, plan and quota returned HTTP 200. The logging request produced no HTTP status by 2026-11-25T03:16:36.799471Z. The worker did not read settings or send an apply.

Next action. Retry the logging read once with a 15-second request bound and fresh app, plan and quota rechecks.

Confirmed Azure mutations. 0.

## O0 attempt 5

PASS on 908aa887bbdf1a7ec11e73b0f3e80712a92a4fb4 from 2026-11-25T03:16:15.808783Z to 2026-11-25T03:20:25.759914Z.

Activity HTTP 200. Matching event rows 0. Baseline reads SITE_RECHECK 200, PLAN_RECHECK 200, QUOTA_RECHECK 200, LOGGING_RECHECK 200, IMAGE_RECHECK 200, EXISTING_KEY_PRIVATE_READ 200. Preflight quota CpuTime 0/3600000 Milliseconds, CpuTimeShortPeriod 0/180000 Milliseconds, WPStopRequests 0/15 Count. Existing key channel verified True. The runner cleared its references on exit. No key or unrelated setting values were emitted or saved. The preflight used the prior rollback image as the required baseline. The bounded logging retry was the only retry and the preflight caused zero Azure writes.

Next action. Apply once with a verified baseline.

Confirmed Azure mutations. 0.

## O2 attempt 3

FAIL on 908aa887bbdf1a7ec11e73b0f3e80712a92a4fb4 from 2026-11-25T03:17:59.089783Z to 2026-11-25T03:20:25.942520Z.

Result HEALTH_FAILED. Final controller stage ROLLBACK_IMAGE_READBACK. Reason none. End 2026-11-25T03:20:13.590331Z. Operations APPLY_IMAGE_ONLY 200 at 2026-11-25T03:18:06.377348Z, ROLLBACK_IMAGE_ONLY 200 at 2026-11-25T03:20:10.709484Z. Candidate sha256:7d3afdd26b4f95e0d58089ea5770706b59a2cead0951b6c5124ba062d2b10972 configured readback True. Registry index sha256:a24c6ba6386d217884a221d7217dadbe168cb3d869e38c452df9604fe1c30bf0 and config sha256:3b589fef96f73ff405947e3f0d9c1b55e3651fc6c8800e6b9a22322204306a06 were qualified in the prior O1. Health 2026-11-25T03:18:21.377762Z no HTTP status TRANSPORT_OR_RESPONSE_UNVERIFIED, 2026-11-25T03:19:06.377931Z 503 HEALTH_REJECTED, 2026-11-25T03:20:06.377759Z 503 HEALTH_REJECTED. Snapshot results not run. Startup HTTP 403. Header classification ACCESS_DENIED. The runner read zero SCM body bytes. No startup marker or Azure exception was recovered. Any marker remains unattributed to main-container role, attempt ID or running digest. No settings write occurred.

Next action. Compare health and the conditional rollback readback.

Confirmed Azure mutations. 1.

## O3 attempt 3

FAIL on 908aa887bbdf1a7ec11e73b0f3e80712a92a4fb4 from 2026-11-25T03:20:13.590331Z to 2026-11-25T03:20:26.105234Z.

Health accepted False. Startup interpretable False. Restore IMAGE_RESTORED. Last configuration state ROLLBACK_IMAGE_CONFIGURED. Prior parallel digest sha256:a972845fd52ad0487116c67d022770421abfa4242ca5c94928a51e4eaa89d914. Candidate child sha256:7d3afdd26b4f95e0d58089ea5770706b59a2cead0951b6c5124ba062d2b10972. No post-rollback health request and no verified running image. No other apps were targeted. API_HEALTH_ACCEPTED=false. PARALLEL_API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

Next action. Check the existing parallel app portal startup records for the 2026-11-25 03:18:06 to 03:20:13 UTC window to see whether a main-container launch or failure was recorded.

Confirmed Azure mutations. 1.

## O3 attempt 4

BLOCKED on 7235e2f959a1ae0b5224fd18169de782b2f3ec71 from 2026-11-25T03:23:48.346668Z to 2026-11-25T03:24:15.508235Z.

The portal-backed detector request finished at 2026-11-25T03:23:54.147946Z with HTTP 200 and exact detector identity. One row was projected in memory. It yielded zero records with an in-window timestamp and explicit main-container role. Startup cause, Azure-side exception and running digest remain unknown. The previous health failures and verified image rollback remain the live result. No browser session, SCM read, settings read, new health request or Azure write occurred.

Next action. If no main-container event is available in the signed-in portal for this same window, review a known-good container control on the same F1 app before approving another apply.

Azure mutations. 0.

## O1 attempt 5

PASS on 06583e8f35a311f811cb6e6336b748c7ff847015 from 2026-11-25T03:48:45.893717Z to 2026-11-25T03:48:45.894442Z.

Two Linux/amd64 starts passed, one with a read-only container root. Each returned 200 Alive on GET /health and 404 on GET /, POST /health and POST /v1/research/snapshot. Both emitted only the fixed bootstrap and listener-ready markers in order. Both owned containers were removed. Local control tag aiq-r115-f1-control:06583e8f35a3. Source 06583e8f35a311f811cb6e6336b748c7ff847015. Index sha256:5ef92a2179efc57af3512924a4933b307b6b24eed3b5065c92067f9cd1ba35f8. Linux child sha256:00c7e7212ae3bab7d3e4f80353e478949a365d38b5a677ab039eca6bec8bda23. Config sha256:09360b09d2ed079ef851fd4fe56bdce4b530c7fc3b689761b5bc01dd6953f036. All three digests verified from the local OCI archive. The control serves no dataset, requires no API key, and uses no SQLite or provider. Startup collector tests distinguish control markers from Research API markers and retain unverified role and image flags. 131 WP80 tests and eight receipt tests passed. There was no registry or Azure operation.

Next action. Authorize publication of the exact tested control to the existing GHCR repository before any separately authorized Azure comparison.

Azure mutations. 0.

### What the offline control can distinguish

On the same parallel F1 app, a control 200 Alive would show that this App Service target can pull, launch and route to the tested class of container. The Research API startup path would remain the next investigation target. Failure of both images would justify investigating the common image launch or routing path, and would leave attribution open.

A future comparison would confirm a fresh exact-app F1 baseline, quota, port 52731 and the previous configured image. Apply the verified control child once with an image-only change, run the same bounded health and startup capture, and restore the verified prior image after either outcome. The control has no authenticated snapshot. Original staging, UI and production are outside scope. No publication or Azure comparison was authorized by this offline pass.

## O1 attempt 6

PASS on e96751af2d8f0a12d8bb8ce80ccbf9aa2b33496b from 2026-11-25T03:50:53.676084Z to 2026-11-25T03:50:53.677468Z.

Registry tag ghcr.io/samuel-santos-engineer/aiquanttradingresearch:r115-f1-control-06583e8f35a3. Source 06583e8f35a311f811cb6e6336b748c7ff847015. OCI index sha256:5ef92a2179efc57af3512924a4933b307b6b24eed3b5065c92067f9cd1ba35f8. Linux/amd64 child sha256:00c7e7212ae3bab7d3e4f80353e478949a365d38b5a677ab039eca6bec8bda23. Config sha256:09360b09d2ed079ef851fd4fe56bdce4b530c7fc3b689761b5bc01dd6953f036. Push attempted True. Qualified registry readback True. Registry index and Linux manifest bytes matched their local SHA-256 digests. The registry Linux manifest references the config digest verified byte-for-byte in the local OCI archive. No config blob was separately fetched. No Azure request, configuration update, restart, health probe or key access occurred. The existing image rollback stands.

Next action. Authorize a separate comparison limited to one control apply on the parallel F1 app, bounded health/startup observation, and restoration of the previous image after either outcome.

Azure mutations. 0.

## O0 attempt 6

BLOCKED on 572da3af3f5b888bc5d4bcad9dccc51fa5a2cac9 from 2026-11-25T03:59:38.501311Z to 2026-11-25T04:03:44.281596Z.

Activity returned HTTP 200 and zero rows. App metadata returned HTTP 200 but the baseline check failed at 2026-11-25T03:59:40.714745Z. No apply, SCM or health request occurred. One projected follow-up at 2026-11-25T04:00:16.711532Z verified all expected site fields. The original mismatch was not retained in the first worker's projection. No write occurred.

Next action. Resume the same approved comparison with a fresh, complete baseline.

Confirmed Azure mutations. 0.

## O0 attempt 7

BLOCKED on 572da3af3f5b888bc5d4bcad9dccc51fa5a2cac9 from 2026-11-25T04:00:40.804313Z to 2026-11-25T04:03:44.537336Z.

Site, plan and quota returned HTTP 200. The logging GET returned no HTTP status by 2026-11-25T04:00:59.837583Z. No settings, apply, startup capture or health request followed.

Next action. Try one isolated logging read before aborting the comparison.

Confirmed Azure mutations. 0.

## O0 attempt 8

PASS on 572da3af3f5b888bc5d4bcad9dccc51fa5a2cac9 from 2026-11-25T04:00:38.162144Z to 2026-11-25T04:03:44.780694Z.

Activity HTTP 200 with 0 rows. Reads SITE_RECHECK 200, PLAN_RECHECK 200, QUOTA_RECHECK 200, LOGGING_RECHECK_FROM_ISOLATED_RECOVERY 200, IMAGE_RECHECK 200, ROUTING_PORT_PROJECTION 200. Isolated logging retry status 200, verified True. Quota CpuTime 0/3600000 Milliseconds, CpuTimeShortPeriod 0/180000 Milliseconds, WPStopRequests 0/15 Count. Port verified True. Baseline verified True. The settings response was reduced to identity and port in memory. No key value was selected, used or emitted. 131 WP80 tests, eight receipt tests and four fake control-orchestration checks passed.

Next action. Apply the immutable control once when all gates pass.

Confirmed Azure mutations. 0.

## O2 attempt 4

PASS on 572da3af3f5b888bc5d4bcad9dccc51fa5a2cac9 from 2026-11-25T04:02:15.162271Z to 2026-11-25T04:03:44.984740Z.

Image index sha256:5ef92a2179efc57af3512924a4933b307b6b24eed3b5065c92067f9cd1ba35f8. Linux child sha256:00c7e7212ae3bab7d3e4f80353e478949a365d38b5a677ab039eca6bec8bda23. Config sha256:09360b09d2ed079ef851fd4fe56bdce4b530c7fc3b689761b5bc01dd6953f036. Source 06583e8f35a311f811cb6e6336b748c7ff847015. Controller ended 2026-11-25T04:03:29.984343Z. Result CONTROL_HEALTH_PASS. Reason none. Operations APPLY_CONTROL_IMAGE 200 at 2026-11-25T04:02:20.512213Z, RESTORE_PRIOR_IMAGE 200 at 2026-11-25T04:03:27.086090Z. Control config verified True. Health 2026-11-25T04:02:35.512560Z no HTTP status TRANSPORT_OR_RESPONSE_UNVERIFIED, 2026-11-25T04:03:20.513090Z 200 ALIVE. Startup HTTP 200. Startup classification UNSUPPORTED_JSON_ENVELOPE. Fixed observations none. Runtime digest and main-container role remain unverified. No snapshot, key update or app settings mutation.

Next action. Verify the prior image restoration and compare outcomes.

Confirmed Azure mutations. 1.

## O3 attempt 5

PASS on 572da3af3f5b888bc5d4bcad9dccc51fa5a2cac9 from 2026-11-25T04:03:29.984343Z to 2026-11-25T04:03:45.139414Z.

Restore status IMAGE_RESTORED. Restore reason none. Prior configured digest sha256:a972845fd52ad0487116c67d022770421abfa4242ca5c94928a51e4eaa89d914. The same F1 target returned literal 200 Alive after the control image was configured, following the 503 Research API candidate result. This narrows the next offline review to differences in the Research API image and startup path. It does not identify a particular failing guard. Post-restore health was not checked. No original staging, Streamlit or production changes. API_HEALTH_ACCEPTED=false. PARALLEL_API_HEALTH_ACCEPTED=false. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false.

Next action. Compare the qualified Research API image and control startup inputs offline before any new Azure experiment.

Confirmed Azure mutations. 1.

## O1 attempt 7

PASS on 96de9a21064b11c92c1a46b9c7a6c6c29cbacd43 from 2026-11-25T04:25:17.399367Z to 2026-11-25T04:25:17.400383Z.

Eight synthetic Linux container runs passed their expected outcomes. Both images ran on the selected ASP.NET base with Linux/amd64, user 1654:1654 and IPv4 port 52731. The baseline control and Research both returned 200 Alive. Research returned 401 without authentication and 503 DATASET_NOT_READY with the fake key on the empty store. The other three fixtures left the control healthy and made Research exit 1 at TRANSPORT or STORAGE. All owned test containers were removed. 101 .NET, 131 WP80 and eight receipt tests passed. No application change, build, registry operation or Azure request occurred in this pass. No actual Azure runtime environment or launch-stage evidence was recovered.

Next action. Prototype an offline, explicit split between API liveness and authenticated storage availability, retaining the current guard and stressing real storage failures before any new Azure experiment.

Azure mutations. 0.

### Control and Research API startup differences

The paired runs used the locally qualified control index sha256:5ef92a2179efc57af3512924a4933b307b6b24eed3b5065c92067f9cd1ba35f8 and Research API index sha256:a24c6ba6386d217884a221d7217dadbe168cb3d869e38c452df9604fe1c30bf0. Each role ran with identical synthetic site, transport and storage settings within each pair. The test client shared the owned container's isolated loopback namespace. No host port was published.

| Synthetic change from baseline | F1 control | Research API |
| --- | --- | --- |
| None | HTTP 200 Alive | HTTP 200 Alive. Empty authenticated snapshot returned 503 DATASET_NOT_READY. |
| Remove WEBSITE_HOSTNAME | HTTP 200 Alive | Exit 1 at TRANSPORT before listener creation. |
| Set WEBSITES_ENABLE_APP_SERVICE_STORAGE=true | HTTP 200 Alive | Exit 1 at STORAGE before listener creation. |
| Add a writable tmpfs directly at the private research directory | HTTP 200 Alive | Exit 1 at STORAGE before listener creation. |

Both images use a shell entrypoint and the same ASP.NET base, UID and IPv4 listener. The control goes directly from shell to Kestrel. The Research Worker dispatches the API mode before composing provider services and checks transport and storage before Kestrel starts. The Azure HTTP transport expects consistent platform-provided site metadata and explicit opt-in. The storage branch requires UID 1654, a writable private container-root overlay and directory permissions, even when SQLite is absent. The Research API's in-process 503 DATASET_NOT_READY requires a listener and authentication. The earlier Azure 503 at /health remains of unknown origin.

Azure control health makes a blanket port, base-image or registry incompatibility less likely on this app. The fixtures do not identify the actual Azure environment values, mount topology or failed startup branch. The next offline candidate should keep transport, authentication, TLS, UID and storage checks fail-closed. A narrow liveness split could let an already validated listener return Alive when storage is unavailable while denying authenticated snapshots without an approved store. It would not fix failures that occur before transport and listener validation.

## O1 attempt 8

PASS on 6c853d05094adcba7c06968a1c0dc48210bb588a from 2026-11-25T04:53:39.184340Z to 2026-11-25T04:53:39.185465Z.

101 .NET tests, 132 WP80 tests, eight receipt tests and eight Linux smoke cases passed. Source 6c853d05094adcba7c06968a1c0dc48210bb588a. Local image aiq-r115-liveness:api-6c853d05094a. OCI index sha256:460cc90afdc97613bc7e47a18db76ebcbc2e8f5232069eab98332490416f9853. Linux child sha256:a6c05c989ebfff7c6a616ca8e6a598308bf9bf8d0bb49fb8c11af7f866e12b38. Config sha256:bc10a4b08217314c0cbb563c2754dad7bea89c3a2a9a0b83c2acd32d3b959373. With a conflicting storage setting, separate private-directory mount, read-only root or corrupt synthetic SQLite file, the Worker emitted the fixed STORAGE_UNAVAILABLE marker, returned 200 Alive, denied unauthenticated snapshots with 401, and returned 503 DATASET_NOT_READY to the fake authenticated client. The valid empty-store fixture preserved its existing behavior. Wrong UID, missing platform hostname and malformed key still exited before serving. No raw settings, paths or exception text were logged. Owned containers were removed. No registry publication or Azure operation occurred.

Next action. Review the exact local prototype and separately authorize publication before any Azure comparison.

Azure mutations. 0.

This prototype applies only after explicit Azure lab transport validation. The existing store guard remains mandatory for any snapshot source. A partial store is disposed on validation failure, and the fallback rechecks the original Linux UID requirement before listening. Liveness-only mode keeps the source unavailable for that process. Local HTTPS retains its existing startup failure behavior, and known storage errors are handled only after they return. The prior Azure 503 cause is still unknown. No new monitoring, provider request, state import or deployment.

## O1 attempt 9

PASS on 0d222fe8aee46299a4d3ec5ec12eb6806ca8db17 from 2026-11-25T05:01:24Z to 2026-11-25T05:02:16.049395Z.

Registry tag ghcr.io/samuel-santos-engineer/aiquanttradingresearch:r115-liveness-api-6c853d05094a. Source 6c853d05094adcba7c06968a1c0dc48210bb588a. OCI index sha256:460cc90afdc97613bc7e47a18db76ebcbc2e8f5232069eab98332490416f9853. Linux/amd64 child sha256:a6c05c989ebfff7c6a616ca8e6a598308bf9bf8d0bb49fb8c11af7f866e12b38. Config sha256:bc10a4b08217314c0cbb563c2754dad7bea89c3a2a9a0b83c2acd32d3b959373. Push attempted True. Qualified registry readback True. Registry index and Linux manifest bytes matched their local SHA-256 digests. The registry Linux manifest references the config digest verified byte-for-byte in the local OCI archive. No config blob was separately fetched. No Azure request, configuration update, restart, health probe or key access occurred. The existing parallel image rollback stands. Research API runtime readiness is still unverified.

Next action. Authorize a separate parallel-lab-only image apply with the existing approved memory-only key channel, bounded health and startup capture, and image-only rollback on failed health.

Azure mutations. 0.

## O0 attempt 9

BLOCKED on 603fad3a2eccdd3e9f5e8601c418fd5595e215ae from 2026-11-26T01:00:19.924631Z to 2026-11-26T01:03:09.397563Z.

The first logging read returned no HTTP status by 2026-11-26T01:00:36.188046Z. No settings or image mutation was attempted.

Next action. Retry that isolated read once before advancing.

Confirmed Azure mutations. 0.

## O0 attempt 10

PASS on 603fad3a2eccdd3e9f5e8601c418fd5595e215ae from 2026-11-26T01:00:13.335594Z to 2026-11-26T01:03:09.592173Z.

Activity HTTP 200 with 0 matching rows. Logging HTTP 200 verified True. Reads SITE_RECHECK 200, PLAN_RECHECK 200, QUOTA_RECHECK 200, LOGGING_RECHECK_FROM_ISOLATED_RECOVERY 200, IMAGE_RECHECK 200, ROUTING_PORT_PROJECTION 200. Quota CpuTime 0/3600000 Milliseconds, CpuTimeShortPeriod 0/180000 Milliseconds, WPStopRequests 0/15 Count. Port verified True. Baseline verified True. No key value was selected, used or emitted. 132 WP80 tests, eight receipt tests and four fake controller checks passed.

Next action. Apply the published candidate once.

Confirmed Azure mutations. 0.

## O2 attempt 5

PASS on 603fad3a2eccdd3e9f5e8601c418fd5595e215ae from 2026-11-26T01:01:46.796684Z to 2026-11-26T01:03:09.739450Z.

Source 6c853d05094adcba7c06968a1c0dc48210bb588a. Index sha256:460cc90afdc97613bc7e47a18db76ebcbc2e8f5232069eab98332490416f9853. Linux child sha256:a6c05c989ebfff7c6a616ca8e6a598308bf9bf8d0bb49fb8c11af7f866e12b38. Config sha256:bc10a4b08217314c0cbb563c2754dad7bea89c3a2a9a0b83c2acd32d3b959373. Worker ended 2026-11-26T01:02:57.818801Z. Result LIVENESS_HEALTH_PASS. Last control stage LIVENESS_STARTUP. Reason none. Writes APPLY_LIVENESS_IMAGE 200 at 2026-11-26T01:01:51.722305Z. Control-plane candidate verified True. Health 2026-11-26T01:02:06.723206Z no HTTP status TRANSPORT_OR_RESPONSE_UNVERIFIED, 2026-11-26T01:02:51.722850Z 200 ALIVE. Startup HTTP 200. Startup response class UNSUPPORTED_JSON_ENVELOPE. Markers none. Running digest and marker attribution are unverified. No setting mutation, restart command, key use or snapshot.

Next action. Report API liveness independently from startup attribution and snapshot availability.

Confirmed Azure mutations. 1.

## O3 attempt 6

PASS on 603fad3a2eccdd3e9f5e8601c418fd5595e215ae from 2026-11-26T01:02:57.818801Z to 2026-11-26T01:03:09.885125Z.

The app returned literal 200 Alive after the liveness image was configured. The approved rule retains that image. Restore state NOT_TRIGGERED. Restore reason none. Prior digest sha256:a972845fd52ad0487116c67d022770421abfa4242ca5c94928a51e4eaa89d914. PARALLEL_API_HEALTH_ACCEPTED=true. PUBLIC_RUNTIME_VERIFIED=False. liveAttemptReady=false. Post-rollback health and data readiness remain unverified. Original staging, UI and production were outside scope.

Next action. Authorize a separate authenticated BTC/USD and ETH/USD snapshot check on the exact parallel API before claiming dataset or browser readiness.

Confirmed Azure mutations. 0.

## N4 attempt 3

BLOCKED on afdacbb09bebd76c84d168e9a72c36ee38b482df from 2026-11-28T17:02:06.521047Z to 2026-11-28T17:02:48.940507Z.

Finished at 2026-11-28T17:02:20.588314Z. All four management responses were HTTP 200. The exact parallel site was Running with normal usage and availability. Pre-probe plan counters were stop requests 0/15, daily CPU 0/3600000 ms and short CPU 0/180000 ms. The published liveness child sha256:a6c05c989ebfff7c6a616ca8e6a598308bf9bf8d0bb49fb8c11af7f866e12b38 and blank startup matched the expected configuration. The dedicated key shape was validated in memory. The BTC/USD POST started at 2026-11-28T17:02:12.575675Z and returned no HTTP status within its eight-second bound. ETH/USD was skipped after the unverified BTC/USD response. No authorized or rejected API response, snapshot data, or exact transport cause was observed. There were no Azure writes or health probes. Post-probe quota and current serving state remain unknown. The prior health pass is historical and does not establish current snapshot or public readiness.

Next action. Run one separately authorized anonymous health check on the exact parallel API to establish current reachability before another authenticated attempt.

Azure mutations. 0.

## N3 attempt 3

PASS on 5b7f180cae0f85c65bdcd3c256b8db5559d7c7dd from 2026-11-28T17:16:16.275679Z to 2026-11-28T17:16:38.704956Z.

At 2026-11-28T17:16:17.241215Z the single anonymous request returned HTTP 200 with literal Alive in 0.966 seconds. TLS verification passed. No redirects, retries, credentials, management requests, snapshots or Azure writes occurred. This is a fresh serving observation for the parallel API. It does not classify the earlier BTC/USD timeout. Authenticated data readiness and running image identity were not established. Post-probe quota was not queried.

Next action. Authorize one bounded BTC/USD snapshot check and one conditional ETH/USD check, with no retry and the same private key only in memory.

Azure mutations. 0.
