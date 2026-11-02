# Release 1.15 WP36 - Simplified research API deployment

Status: settings-only amendment implemented and authorized save verified; image/HTTPS/restart Apply remains unapproved and unexecuted. The amendment section below supersedes the original portal-only/settings-API boundary. Baseline: merged PR #423, `3c0585fca53bd7ecd175eed0b9549186090a1155`. UTF-8/LF/no BOM required.

## Scope and practical settings constraint

No rebuild, repush, API/authentication/source change, UI change, provider acquisition or old controller invocation. The owner waived complex rollback: this helper creates only a minimal sanitized pre-change snapshot and offers manual recovery guidance. No full settings baseline, connection strings, two-capture workflow or private historical attempt access.

The documented [App Service settings operation](https://learn.microsoft.com/en-us/rest/api/appservice/web-apps/update-application-settings?view=rest-appservice-2025-03-01) replaces the collection with PUT. Microsoft's [deployment library](https://github.com/microsoft/pipelines-appservice-lib/blob/master/packages/appservice-rest/src/Arm/azure-app-service.ts) implements `patchApplicationSettings` by retrieving the collection and calling its PUT updater; its name is not evidence of a server-side partial update. An undocumented PATCH cannot be assumed to preserve unrelated settings. No such mutation was tested on Azure.

Originally the automated helper had no app-settings API route; the later explicitly approved settings-only amendment below adds a separate merge path. Proposed practical alternative: the owner privately confirms the existing key-setting name and edits only the ten nonsecret runtime settings in the portal, preserving unrelated settings and sticky flags. This operator-assisted adjustment was presented for owner agreement; it is not silently treated as completed. If the owner does not accept this path, stop at the settings API constraint rather than reading the key or issuing a full-settings PUT. No new vault or infrastructure is required.

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

The image-deployment action's ARM allowlist cannot access settings, connection strings, slot flags, publishing credentials, plan mutations or the UI. The separately authorized settings path below privately reads settings/flag metadata and uses one bounded CLI merge; it cannot invoke image deployment. CLI responses for writes are suppressed; failures expose only fixed stages and allowlisted Azure error codes. No secrets are CLI arguments. A timeout after an attempted write is treated as potentially changed state, never safe automatic retry.

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

## Amendment - authorized automated ten-setting save (2026-11-02)

This amendment supersedes only the original portal-only/no-settings-API boundary for the ten named nonsecret settings. The owner separately authorized their automated merge-style save and accepted a possible implicit recycle. It does **not** authorize image assignment, platform HTTPS-only changes, explicit restart, health probes, registry calls, provider work or merge of this PR. The owner had confirmed key-name presence; the implementation additionally verified it from actual private SDK data before saving rather than relying on that assertion alone.

### Installed CLI preservation evidence

Installed Azure CLI version: **2.89.1**, with compiled appservice handler. Reviewed its actual `update_app_settings` bytecode, signature and list/output helpers using the installed interpreter; no real settings were involved in this inspection. It retrieves the existing collection, sets each plain `--settings NAME=value` entry in that dictionary, and submits the merged collection. Its internal collection update is not a server-side partial PATCH. Only a nonempty `slot_result` invokes the slot configuration updater; plain key/value strings without `--slot-settings` or JSON slot metadata leave that branch unused. Returned data is redacted by the CLI, and our save additionally uses `--output none`.

A fake-only probe executes that **installed handler** with a fake key/unrelated value and all ten desired strings. The probe substitutes all relevant SDK operations with in-memory fakes; its fake client has no slot API at all, so an unexpected slot access fails. The captured update equals exactly the original fake dictionary merged with the ten entries. Probe PASS, no Azure or actual key. Version changes stop with `SETTINGS_PRESERVATION_UNVERIFIED` rather than inheriting this assurance blindly.

### Separate commands and private processing

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp36ResearchDeploy.ps1 -Action SettingsPlan
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp36ResearchDeploy.ps1 -Action SettingsApply -ApproveResearchSettingsSave
```

Default Plan remains nonmutating. SettingsPlan has no Azure/registry I/O. `-ApproveExactResearchDeploy` does not approve SettingsApply, and `-ApproveResearchSettingsSave` does not approve image Apply. No key argument or environment-channel handoff is required. PowerShell 5.1 AST parsing passed; runtime execution remains subject to the owner's policy, with no bypass. Actual agent execution used the reviewed Python implementation directly:

```text
python -B eng/azure-cli/r1.15-qualification/wp36_research_deploy.py --action SettingsApply --approve-research-settings-save
```

The helper locates the installed Azure CLI interpreter and starts a private worker. Private SDK reads use an explicit subscription ID, bounded network calls with retries disabled, and HTTP logging disabled. Key/unrelated values stay only in that worker's RAM: no JSON output pipe, log, fingerprint, archive or file contains them. Only sanitized result fields leave the worker. Existing cached Azure authentication is used implicitly; credentials are never printed or manually read by the helper. Clearing dictionary references is best effort, not a promise of cryptographic memory erasure. The CLI's internally authorized HTTP serialization/read/merge/write necessarily carries the existing values to Azure; no claim is made that it avoids reading or rewriting the collection internally.

After exact app/plan/region/mode metadata preflight, private reads establish key-name presence and collect original settings/slot flags without serializing them. An orphan sticky name for an absent desired setting stops before save, because creating that entry could otherwise make a new setting sticky. Existing flags (including null versus empty lists) are preserved; new settings must default nonsticky.

One bounded CLI save is invoked, equivalent to:

```text
az webapp config appsettings set --subscription <WP36 fixed subscription> --resource-group rg-aiq-r112-wp05-wus2-7f5eabb5 --name aiqr115researchstagingwus27f5eabb5 --settings <exact ten nonsecret NAME=value pairs from the table above> --only-show-errors --output none
```

There is no key in the argument vector, no `--slot-settings`, no settings JSON replacement file, no `--debug`/`--verbose` and no command echo. The installed Python `-IBm azure.cli` entrypoint is invoked directly (the same entrypoint used by az.cmd), so a timeout terminates that CLI process rather than leaving an intermediary command shell's child running. Save timeout is 90 seconds; the worker has a finite overall bound. Stdout/stderr are captured and never forwarded or persisted. Child-only environment options disable CLI file logging and telemetry; SDK logging is disabled in the worker. No user's global logging/policy/transcript settings are changed.

No retry or automatic rollback follows an uncertain save. Read/merge/write is not an atomic concurrency control: avoid concurrent configuration edits during the operation. Required post-save comparison detects drift instead of overwriting it again.

### Actual save and verification

Result: **`settings saved`**, exit 0, **one CLI save invocation**, Azure save confirmed. Private post-save checks all returned true:

| Check | Actual result |
| --- | --- |
| All ten exact nonsecret string values | PASS |
| Existing key-setting name present | PASS |
| Existing key value unchanged (in-process equality, no hash/output) | PASS |
| All unrelated entries unchanged | PASS |
| Final entry names exactly original union ten desired names | PASS |
| All app/connection-string/Azure-storage sticky-name metadata unchanged | PASS |
| Hosting mode, startup-empty state, HTTPS-only, target and plan unchanged | PASS |

Saved nonsecret names (no unrelated values disclosed): `WEBSITES_PORT`, `AIQ_RESEARCH_BOOTSTRAP_MODE`, `AIQ_RESEARCH_BIND_ADDRESS`, `AIQ_RESEARCH_TRANSPORT`, `AIQ_RESEARCH_PLATFORM_HTTPS_ONLY`, `AIQ_RESEARCH_PRIVATE_DIRECTORY`, `AIQ_RESEARCH_STORAGE_SCOPE`, `AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING`, `AIQ_RESEARCH_EXPECTED_MOUNT_PATH`, `WEBSITES_ENABLE_APP_SERVICE_STORAGE`.

An implicit recycle was accepted but not independently observed. No explicit restart, registry call, image/HTTPS update, connection-string value access, health probe or provider call occurred. Platform HTTPS-only remains false and hosting remains empty Code mode; the application flag `1` does not change the platform setting. UI/plan/logging/slots are not mutation targets. No new full baseline or historical private record was created/read.

If a later settings save is uncertain, do not rerun blindly. A failed/timeout save reports possible mutation and never emits `settings saved`; a successful CLI return with inconclusive preservation also remains blocked. Inspect only the safe stage/boolean summary and obtain targeted review; do not reconstruct a replacement collection, expose the key or run image Apply as a recovery measure.

### Amendment tests and review status

`python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp36_research_deploy.py -v`: **30 passed**, exit 0. Added separate/default approvals, exact subscription/target and ten arguments, fake key/unrelated preservation, sticky/null metadata preservation, orphan sticky rejection, before-save gates, changed-key/unrelated/flags/value/mode detection, no raw stdout/stderr disclosure, bounded/ambiguous save handling, no retry and no image/restart/health invocation. Installed CLI fake merge probe also PASS independently. Windows PowerShell 5.1 AST: zero errors; runtime not rerun through a policy bypass. UTF-8/LF/no BOM and diff checks run before commit.

Same four PR paths amended; no competing PR, application change or dependency installation. This is settings-only success. Image/HTTPS/restart Apply remains separately unapproved and unexecuted. PR #424 remains OPEN/UNMERGED.

`liveAttemptReady=false`; `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.
