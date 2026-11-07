# Codex Prompt — Release 1.15 WP25: Operator-Executed Azure Read-Only Diagnostic Qualification

## Authority and baseline

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
Work from current `main`, containing squash-merged PR #413 at commit `0ac12a38b034ab4bf64fdcb0d8ca3d566773d1b9`. Preserve any dirty checkout; use a separate worktree if needed. PR #413 added fixed `failedStage`, `failureCategory`, and `serviceKeyChannelChecked` to the WP22 read-only inspector. WP21 live controller and independent recovery remain unchanged.

**WP25 is an operator-executed, read-only diagnostic qualification, not a deployment milestone.** Do not run any Azure mutation, Start/Stop, container deployment, image publication, public application HTTPS probe, authenticated snapshot, provider call, data import, UI change, or credential acquisition. Do not grant or infer live approval. The existing research App Service and UI must not be changed.

Exact research target: `aiqr115researchstagingwus27f5eabb5`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, F1 plan `asp-aiq-r112-wp05-wus2-7f5eabb5` in West US 2. Preserve the reviewed immutable research image digest `sha256:d3221dbe0a4ef6dfba37e0c61c06bb3bc9d9ee2b6f776d7738c35aece32be26f` and all existing guardrails.

## Objective

Obtain exactly one controlled, real Azure **read-only** baseline inspection in the owner's authorized Windows PowerShell 5.1/Azure CLI session, using merged WP24 diagnostics. Return only a sanitized report indicating the exact failed stage and conservative failure category, or successful two-capture validation. Avoid unnecessary code changes. If existing code works, do not open a PR.

The historical disabled attempt `a6a6a450-6479-473b-8aa7-8d828b671058` and its private evidence are immutable historical records: do not inspect again, modify, reuse, reset, delete, rearm, or extend them. Create one fresh disabled UUID for WP25 only when the operator is ready to perform the read-only inspection within the template's freshness window. No live baseline or consumed marker may be written.

## Codex tasks

1. Inspect merged WP21/WP22/WP24 files and runbook, especially:
   - `eng/docker/release-1.15/Test-Wp22AttemptPreparation.ps1`
   - `eng/docker/release-1.15/Test-Wp21ControllerQualification.ps1`
   - `eng/azure-cli/r1.15-qualification/wp22_preparation.py`
   - `docs/roadmap/release-1.15/prompters/release-1.15-wp22-attempt-preparation-runbook.md`
2. Verify locally that WP24 diagnostic fields are present and that the read-only inspector retains its exact eight ARM operation allowlist per capture, strict equality, freshness checks, disabled approval, and sanitized output. Do not weaken any validation.
3. Run existing offline tests and static checks. Record actual results, distinguishing Codex-host execution from owner operator evidence. Do not attempt execution-policy bypass, change identities, run `Invoke-Expression`, change profile/policy/registry, or access historical private attempts.
4. Prepare a concise operator handoff. Use the existing reviewed PowerShell entry point rather than introducing a wrapper that suppresses diagnostic fields. Operator should execute commands below from a clean merged-main checkout and an already authorized Azure CLI session. **Codex must not run real Azure inspection on behalf of the operator.**
5. Ask the owner to return only the selected sanitized JSON fields. Interpret `failedStage`, `failureCategory`, `azureBaselineVerified`, and `serviceKeyChannelChecked` precisely. If a failure occurs, stop and recommend a *specific, narrow* correction supported by evidence. Do not automatically retry or create multiple attempts.
6. If a genuine code defect is demonstrated, implement the minimum correction with mocked regression tests, no altered Azure permissions/validation, and open a small PR for review, unmerged. If no defect is demonstrated, do not create a PR or extra governance artifacts.

## Operator handoff — execute only in owner's authorized session

Use reviewed merged `main` and Windows PowerShell 5.1. Do not paste secrets, print private paths or approval records, or use an unreviewed WP23 wrapper.

```powershell
# From repository root: offline qualification only.
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1
if ($LASTEXITCODE -ne 0) { throw 'WP22 offline qualification failed; stop.' }
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp21ControllerQualification.ps1
if ($LASTEXITCODE -ne 0) { throw 'WP21 operator preflight failed; stop.' }

# Only when ready for ONE read-only Azure inspection; approval stays disabled.
$prepared = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -PreparePrivateAttempt | ConvertFrom-Json
if ($LASTEXITCODE -ne 0 -or $prepared.attemptPrepared -ne $true -or $prepared.approvalEnabled -ne $false) { throw 'Disabled preparation failed; stop.' }
$env:AIQ_WP21_ATTEMPT_DIRECTORY = Join-Path (Join-Path $env:LOCALAPPDATA 'AIQ-WP21') $prepared.attemptId

# One explicit, read-only Azure management inspection, within the freshness window.
$inspection = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -InspectAzureReadOnly | ConvertFrom-Json
$inspectionExit = $LASTEXITCODE
$inspection | Select-Object result,attemptId,failedStage,failureCategory,azureBaselineVerified,serviceKeyChannelChecked,serviceKeyChannelAvailable,approvalEnabled,liveControllerInvoked,liveAttemptReady,RESEARCH_PROCESS_ALIVE,AUTHENTIC_DATA_NOT_IMPORTED,PUBLIC_RUNTIME_VERIFIED | ConvertTo-Json
if ($inspectionExit -ne 0) { throw 'Read-only inspection blocked; stop; share sanitized JSON only.' }
```

This command may read private Azure app settings and connection strings into the authorized process memory; never print them. It does not write Azure resources. Azure authentication and RBAC may be indistinguishable under the existing CLI transport: `AZURE_TRANSPORT_FAILURE` must not be relabeled as definitively 401 or 403. `serviceKeyChannelChecked=false` means unverified, not missing. A true availability flag verifies only presence/shape, not validity. Do not ask for key material or expose private files.

## Decision rules and reporting

- On `READ_ONLY_BASELINE_VERIFIED_NOT_AUTHORIZED` with `azureBaselineVerified=true`, record that the two read-only captures matched. This is **not** the immutable live baseline; WP21 must capture a fresh baseline immediately before any separately approved write. Do not arm the attempt or invoke the live controller.
- On failure, report `failedStage` and `failureCategory` exactly, with the safe boolean fields. Preserve evidence, stop. No automatic retry, fallback, Azure mutation or weakened check.
- If the execution policy blocks `-File`, stop and report; no policy workaround.
- Do not claim real Azure schema compatibility or service-key presence based solely on mocked tests.
- Any subsequent named live attempt needs a distinct explicit owner approval covering the UUID, exact resource/image, bounded health and unauthenticated checks, and unconditional independently verified rollback. WP25 supplies no such approval.

Final response to owner: exact offline results; whether owner read-only execution happened; sanitized Azure outcome if supplied; any narrowly scoped defect and PR URL if necessary; remaining prerequisites. Preserve `liveAttemptReady=false`, `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED`, `PUBLIC_RUNTIME_VERIFIED=False` until real, independently qualified evidence justifies a change.
