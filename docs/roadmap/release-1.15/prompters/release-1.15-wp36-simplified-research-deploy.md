# Release 1.15 WP36 - Simplified research API deployment

Status: implementation and read-only preflight complete; live Apply not approved or executed. Baseline: merged PR #423, `3c0585fca53bd7ecd175eed0b9549186090a1155`. UTF-8/LF/no BOM required.

## Scope and practical settings constraint

No rebuild, repush, API/authentication/source change, UI change, provider acquisition or old controller invocation. The owner waived complex rollback: this helper creates only a minimal sanitized pre-change snapshot and offers manual recovery guidance. No full settings baseline, connection strings, two-capture workflow or private historical attempt access.

The documented [App Service settings operation](https://learn.microsoft.com/en-us/rest/api/appservice/web-apps/update-application-settings?view=rest-appservice-2025-03-01) replaces the collection with PUT. Microsoft's [deployment library](https://github.com/microsoft/pipelines-appservice-lib/blob/master/packages/appservice-rest/src/Arm/azure-app-service.ts) implements `patchApplicationSettings` by retrieving the collection and calling its PUT updater; its name is not evidence of a server-side partial update. An undocumented PATCH cannot be assumed to preserve unrelated settings. No such mutation was tested on Azure.

Consequently the automated helper has **no app-settings API route at all**. Proposed practical alternative: the owner privately confirms the existing key-setting name and edits only the ten nonsecret runtime settings in the portal, preserving unrelated settings and sticky flags. This operator-assisted adjustment was presented for owner agreement; it is not silently treated as completed. If the owner does not accept this path, stop at the settings API constraint rather than reading the key or issuing a full-settings PUT. No new vault or infrastructure is required.

## Fixed image and unchanged serving contract

Repository `ghcr.io/samuel-santos-engineer/aiquanttradingresearch` with OCI index digest `sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f`; Linux/amd64 runtime manifest `sha256:fb9aab67753be84ca3300876fa4ed4d669ab0d897beb49f3fb8f2eae58b9b012`. Use the index by digest, never just the WP35 tag.

WP35 evidence: local image HTTP health 200 `Alive`, UID 1654 and exact bootstrap command `dotnet /app/AIQuantTradingResearch.Worker.dll --research-snapshot-api`. The unchanged Dockerfile entrypoint is `/bin/sh /app/research-bootstrap.sh`, bind `0.0.0.0`, port 52731. Default Worker mode is not the API.

Reviewed `ResearchSnapshotEndpoint.cs` and `ResearchStorageBoundary.cs` require the transport/storage guards below and genuine platform site/host/instance/role markers. The helper neither fabricates markers nor bypasses private writable-directory/kernel guards. Storage remains explicitly non-durable F1 staging. Health alone does not prove key validity, authentication or stored authentic data.

## Actual read-only preflight (2026-11-02)

Command: `python -B eng/azure-cli/r1.15-qualification/wp36_research_deploy.py --action Inspect`. Result `METADATA_VERIFIED_NO_MUTATION`, exit 0.

| Check | Actual evidence |
| --- | --- |
| App | Exact `aiqr115researchstagingwus27f5eabb5`, Running |
| Resource group/plan | Exact `rg-aiq-r112-wp05-wus2-7f5eabb5` / `asp-aiq-r112-wp05-wus2-7f5eabb5` |
| Platform | Linux, F1, West US 2 |
| Current hosting | Empty `linuxFxVersion`; no classic image or sidecar mode; startup empty |
| HTTPS-only | False; enabling is an explicit proposed delta |
| Registry | Anonymous token/manifest access succeeded; index and runtime bytes matched their SHA-256 digests; Linux/amd64 manifest confirmed |
| Existing key/runtime settings | Not queried; owner confirmation pending |
| Azure health | Not probed; UNVERIFIED |

Registry check uses only an anonymous GHCR pull token held transiently in memory, never an owner token, Docker configuration, publishing credential or key. It verifies manifest access and content digests, not all layer downloads or future Azure network reachability. A successful anonymous registry read is not proof Azure has already pulled the image.

F1 conversion support remains an actual compatibility-attempt question. Microsoft documents [classic image configuration versus sidecars](https://learn.microsoft.com/azure/app-service/configure-sidecar), but metadata cannot prove this resource will accept the update. A rejected apply stops without paid upgrade, new resource, sidecar conversion or automatic recovery.

## Owner portal prerequisite - not executed by Codex

Use only the exact research app in the existing group. Verify the setting **name** `AIQ_RESEARCH_API_KEY` is present without opening, displaying, copying, changing or screenshotting its value. If absent/unknown, stop for the already-agreed owner-only workflow. Do not generate a new key or paste any secret into this chat.

Privately add/edit only the following nonsecret settings. Preserve existing unrelated settings and each existing deployment-slot checkbox. Do not change the UI app, image, startup, plan, logging or secret. For newly added nonsecret settings, do not enable a sticky flag as part of this procedure. If the portal would expose/rewrite a secret or slot preservation cannot be established, stop and report that limitation. The helper makes no claim to independently verify these portal actions.

| Name | Value |
| --- | --- |
| `WEBSITES_PORT` | `52731` |
| `AIQ_RESEARCH_BOOTSTRAP_MODE` | `serve` |
| `AIQ_RESEARCH_BIND_ADDRESS` | `0.0.0.0` |
| `AIQ_RESEARCH_TRANSPORT` | `azure-app-service-http` |
| `AIQ_RESEARCH_PLATFORM_HTTPS_ONLY` | `1` |
| `AIQ_RESEARCH_PRIVATE_DIRECTORY` | `/tmp/aiq-research` |
| `AIQ_RESEARCH_STORAGE_SCOPE` | `azure-f1-staging-ephemeral` |
| `AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING` | `1` |
| `AIQ_RESEARCH_EXPECTED_MOUNT_PATH` | `/` |
| `WEBSITES_ENABLE_APP_SERVICE_STORAGE` | `false` |

A portal Save can recycle the research app. It requires owner approval too; this runbook does not itself execute or authorize the Save. The application flag `AIQ_RESEARCH_PLATFORM_HTTPS_ONLY=1` is not proof platform HTTPS-only is enabled; the helper's separately approved platform update must enable it. Save alone may not yield a working research app before image assignment.

## Single PowerShell entrypoint and exact proposed Apply

Use the reviewed WP36 checkout; older original checkouts do not acquire these files automatically. Windows PowerShell 5.1 only, no dependencies or policy bypass:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp36ResearchDeploy.ps1
```

Default Plan: no network, Azure, registry or key input. `-Action Inspect` runs bounded registry/metadata reads only, no health probe. Python implementation is independently executable for offline tests; this does not establish PowerShell runtime qualification.

After owner portal prerequisite confirmation and **separate explicit approval** of these exact operations, the Apply command is:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp36ResearchDeploy.ps1 -Action Apply -ApproveExactResearchDeploy -OwnerKeyPresent -OwnerRuntimeSettingsConfirmed
```

Do not set confirmation switches unless the corresponding facts/approval exist. The three mutation requests use existing Azure CLI authentication, API `2023-12-01`, fixed research resource only:

1. PATCH research `/config/web` with only `properties.linuxFxVersion=DOCKER|<fixed repository>@<fixed index digest>` and `properties.appCommandLine=""`. Preserve all other web configuration. Microsoft's deployment library uses PATCH `/config/web` for selected configuration properties; the helper does not use `az webapp config container set`, which may entail additional settings handling.
2. PATCH research site with only `properties.httpsOnly=true` (currently false).
3. POST research `/restart` once.

Before those writes, reverify target/plan/region/mode, anonymous registry manifest access and all confirmations. Reject sidecar, unknown image, nonempty startup, non-F1 plan, wrong identity, stopped/unknown state or ambiguous metadata. Capture only resource ID, mode classification, HTTPS boolean, startup-empty boolean, names of intended nonsecret settings and timestamp in a new local sanitized `before.json`. No secret or full configuration archive. Tool output does not reveal the local user's evidence root.

After the restart, read back mode/image/HTTPS and observe **only** anonymous HTTPS GET `/health` with normal TLS validation, no redirects, no authentication and no API key. At most 180 seconds, per-request timeout at most 8 seconds. Only HTTP 200 and literal five-byte `Alive` passes. Oversized/HTML/trailing-newline bodies do not pass. Do not claim key authentication, imported data, durable storage or public readiness from success.

The helper's ARM allowlist cannot access settings, connection strings, slot flags, publishing credentials, plan mutations or the UI. CLI responses for writes are suppressed; failures expose only fixed stages and allowlisted Azure error codes. No secrets are CLI arguments. A timeout after an attempted write is treated as potentially changed state, never safe automatic retry.

## Manual recovery - deliberately limited

On rejection/timeout/failed health, stop. The report identifies the failed stage, whether Azure may have changed and a safe current-state inspection if available. Do not rerun blindly. No automatic full-settings restore or key overwrite.

Use the sanitized snapshot and portal to review only the actual changed properties: previous mode was empty Code configuration, startup empty, HTTPS-only false and state Running. Reverting an accepted classic conversion is a separate owner action; do not claim it is proven or that an app stopped/started was restored. If the image update was rejected, inspect current state before deciding whether anything needs reversal. If HTTPS/image changed, owner reviews a targeted reversal of those properties; never remove unknown settings, secrets or original Code files.

The ten portal setting values were not archived, so this snapshot cannot restore their previous values. Owner reviews any desired reversal individually; leaving nonsecret settings is preferable to deleting unknown original configuration. No hidden automatic rollback promise. Logging, slots, plan and UI are untouched by the helper; configuration recovery cannot recover ephemeral data lost on a recycle.

## Qualification and inventory

- `python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp36_research_deploy.py -v`: **20 passed**, exit 0. Fake-only no-op, apply confirmations, exact target/F1/region, immutable delta, settings/key access rejection, preserved unrelated configuration by omission, failed mutation handling/manual recovery, anonymous registry error/digest mismatch, snapshot failure, exact health/body/anonymous request/timeout and redirect rejection.
- Default Python Plan: `PLAN_ONLY`, exit 0, no I/O beyond console output.
- Actual Inspect: PASS as above. No Azure mutations or live health requests.
- Windows PowerShell `5.1.26100.9711` AST parse: 0 errors. Exact older `5.1.26100.9444` unavailable. PowerShell runtime invocation not repeated under the known script-execution policy block; no bypass. No application build needed.
- `git diff --check` plus independent UTF-8/no BOM/LF checks required before publication.

Exact changed paths:

- `eng/azure-cli/r1.15-qualification/wp36_research_deploy.py`
- `eng/docker/release-1.15/Invoke-Wp36ResearchDeploy.ps1`
- `tests/runtime/release_1_15/test_wp36_research_deploy.py`
- `docs/roadmap/release-1.15/prompters/release-1.15-wp36-simplified-research-deploy.md`

Owner portal method/setting confirmations and exact live Apply approval are pending. This is a reviewable operator-assisted path, not a claim that the settings API restriction was solved through an unverified partial update. PR must remain OPEN/UNMERGED. `liveAttemptReady=false`; `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.
