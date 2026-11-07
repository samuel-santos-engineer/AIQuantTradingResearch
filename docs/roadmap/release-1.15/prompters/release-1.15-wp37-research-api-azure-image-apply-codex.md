# Release 1.15 — WP37 Codex prompt: Azure research API image deployment

## Context and baseline

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Begin from current `main` containing merged PR #424, squash commit `9196ba26893a80f79111318a88f3d7ad509f3ca1`. Review the merged WP36 helper and runbook before changing anything. Ten nonsecret settings have already been saved once and privately verified, including preservation of the existing `AIQ_RESEARCH_API_KEY`, unrelated settings and slot flags. **Do not repeat the settings save.**

Target only:
- Subscription: `7f5eabb5-80f5-4369-88be-b14a6de763f1`
- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`
- Research App Service: `aiqr115researchstagingwus27f5eabb5`
- Existing plan: `asp-aiq-r112-wp05-wus2-7f5eabb5` (Linux F1, West US 2)
- Immutable image: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f`
- Expected health URL: `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net/health`, HTTP 200 and exact body `Alive`.

The separate UI App Service `aiqr112wp05wus27f5eabb5` must not be changed. No provider acquisition, authentic-data import, authentication probe, paid plan, new resource, image rebuild/repush, or complex automatic rollback.

## Goal

Complete a narrowly scoped, owner-approved compatibility attempt to run the already-qualified research API image on the existing Linux F1 research app. Prefer the merged WP36 `Apply` implementation and make only necessary targeted fixes, with fake-only regression tests. The Code-mode to classic-container transition on this exact F1 resource remains unproven until Azure accepts it.

## Approval boundary

**This prompt authorizes preparation, review, tests, and read-only preflight only. It does NOT itself authorize live `Apply`.** Before any Azure PATCH/POST, present the exact proposed mutations and current sanitized preflight to the owner, and obtain explicit separate approval to execute them. Do not infer approval from the prior settings-save authorization, PR merge, or this prompt. Do not run Apply just to test compatibility. If no approval is available, stop with a reviewable PR and the exact proposed command.

After separate owner approval, the only permitted live mutations are:
1. PATCH the exact research app `/config/web` setting `properties.linuxFxVersion` to `DOCKER|` plus the immutable image reference and `properties.appCommandLine` to empty string, leaving unrelated web configuration untouched.
2. PATCH only the exact research app `properties.httpsOnly=true`.
3. POST restart of the exact research app once.

A configuration update may itself recycle the app; no additional restart loops. No settings API calls, key reads/rewrites, slot flag changes, logging changes, plan changes or UI mutations. The ten settings were already saved; do not invoke `SettingsApply`.

## Implementation and verification

1. Verify checkout/main baseline and read the merged `eng/azure-cli/r1.15-qualification/wp36_research_deploy.py`, `eng/docker/release-1.15/Invoke-Wp36ResearchDeploy.ps1`, WP36 runbook and tests. Avoid creating a redundant controller. Make minimal improvements only if a concrete correctness issue exists.
2. Run default Plan and read-only Inspect. Confirm exact subscription/resource/plan/region, Running Linux F1, empty current `linuxFxVersion` and startup, no sidecar, current HTTPS-only false, and anonymous registry manifest digest/platform access. If metadata has drifted, stop rather than guessing. Never print the key, full settings, connection strings or credentials.
3. Verify the three `Apply` calls are allowlisted to this resource and do not use `az webapp config appsettings set`, full settings PUT, or the old WP21 controller. Preserve strict separate approval switches: `-ApproveExactResearchDeploy -OwnerKeyPresent -OwnerRuntimeSettingsConfirmed`. Prior owner report satisfies factual confirmations but **not live Apply approval**.
4. Before any authorized mutation, record only sanitized original hosting mode/startup-empty/HTTPS-only/plan/state metadata, with no secrets. No full baseline or automatic rollback framework is required. Note that manual recovery may be necessary and Code-mode restoration is not proven.
5. On separate explicit approval, execute the exact three operations once, bounded and without blind retries. Read back the assigned immutable image and HTTPS-only. Perform anonymous HTTPS GET `/health` with normal TLS validation, no redirects, no API key, max 180 seconds total, max 8 seconds/request. Pass only HTTP 200 with exact `Alive` bytes. On rejection/timeout/failed health, stop, report stage and sanitized current state; do not automatically recover, rerun or upgrade the plan.
6. Report separately: Azure mutation outcome, image readback, platform HTTPS-only, anonymous health outcome, any F1 incompatibility evidence, and whether manual owner action is needed. Health success does not prove authenticated snapshot access, authentic imported data or public readiness. Keep `liveAttemptReady=false`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False` unless independently established by later authorized work.
7. Run focused fake-only tests, lint/diff checks, and document exact observations. Open a PR for owner review, leaving it unmerged. If Apply was not separately authorized, clearly state **Azure deployment NOT performed** and **Azure health UNVERIFIED**.

## Preferred commands

Read-only:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp36ResearchDeploy.ps1 -Action Inspect
```

**Do not execute the following until the owner separately approves the exact live Apply:**

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp36ResearchDeploy.ps1 -Action Apply -ApproveExactResearchDeploy -OwnerKeyPresent -OwnerRuntimeSettingsConfirmed
```

If local PowerShell policy blocks execution, do not bypass policy. Use the reviewed Python entrypoint for permitted Plan/Inspect, and disclose any runtime qualification gap.
