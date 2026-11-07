# Codex prompt — Release 1.15 WP23: Operator-executed Azure F1 read-only qualification

## Authority and starting point

Work in `samuel-santos-engineer/AIQuantTradingResearch` from current `main`, after squash merge of PR #412 at `5cc708a9a2035e95988edf1a2ad4104460ba202b`. Preserve any dirty original checkout: use an isolated worktree if necessary. WP21 and WP22 are merged. This task is implementation, verification and operator handoff, not a governance or licensing exercise.

**No live deployment authority is granted.** Do not run Azure Start/Stop, PUT/PATCH, change settings, publish or rebuild images, run application HTTPS probes, send provider requests, import candles, run authenticated snapshots, change the UI, or invoke WP21 live entry. Do not enable the approval template. Do not alter execution policy or bypass a sandbox restriction. No Azure mutation is authorized.

## Objective

Advance from WP22 mocked read-only tests to **real operator-session evidence** about the existing Azure F1 research staging configuration, using the merged WP22 safe entry. Where Codex cannot execute Windows PowerShell or authenticate to Azure, prepare a precise operator handoff and offline checks instead; never report the Azure read as completed without the operator's sanitized output. Prefer minimal changes and avoid a new redundant controller or another planning-only layer.

Target only:

- Research App Service: `aiqr115researchstagingwus27f5eabb5`
- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`
- Existing F1 plan: `asp-aiq-r112-wp05-wus2-7f5eabb5`, West US 2
- Research HTTPS URL (NOT to probe in WP23): `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net`
- Approved research image digest: `sha256:d3221dbe0a4ef6dfba37e0c61c06bb3bc9d9ee2b6f776d7738c35aece32be26f`
- The existing UI App Service must remain untouched.

## Required work

1. Inspect merged WP21 and WP22 scripts, tests, code fingerprints and runbooks. Check the exact `Test-Wp22AttemptPreparation.ps1` modes, WP21 private directory preparation, WP22 read-only ARM allowlist, local PowerShell preflight, report redaction, and fresh-template expiry behavior. Identify real-world incompatibilities before proposing changes; do not invent a new authorization mechanism.
2. Provide a concise **operator-executable sequence** from repository root in an already authorized Windows PowerShell 5.1 session, with no policy changes:
   - Run default WP22 offline test entry.
   - Run WP21 controller qualification/preflight if needed.
   - Prepare one fresh private attempt via `-PreparePrivateAttempt`; preserve its returned UUID and leave `explicitLiveApproval=false` and `allowRecovery=false`.
   - Set `AIQ_WP21_ATTEMPT_DIRECTORY` to the new UUID under `%LOCALAPPDATA%\AIQ-WP21` without disclosing the absolute path.
   - Within the disabled template's freshness window, run `-InspectAzureReadOnly` using the already authenticated Azure CLI operator session.
   - Inspect and return only the sanitized JSON result, including `azureBaselineVerified`, `serviceKeyChannelAvailable`, `approvalEnabled`, `liveControllerInvoked`, `liveAttemptReady` and failure category if any. Do not print tokens, key values, environment dumps, raw Azure errors, `approval.json`, worker journals or any baseline settings.
3. Verify that read-only inspection can make **only** the eight allowlisted ARM operations per capture (16 total for two captures): GET research site, GET F1 plan, GET web config, POST appsettings/list, POST connectionstrings/list, GET logs, GET slotConfigNames, GET slots. POST list is a read operation. Fail closed on any drift, missing field, non-F1 plan, wrong region, unexpected slot, bad identity, malformed response, expired template, or insufficient Azure permissions. Do not add write fallbacks.
4. Pay special attention to schema compatibility and operator ergonomics. The real Azure REST schema was not verified in WP22; if the operator returns `AZURE_BASELINE_UNVERIFIED`, use only sanitized evidence to diagnose. Propose the smallest well-tested compatibility fix in a new PR if justified, never weaken validation merely to force a pass. Preserve exact recovery sufficiency and secret safety.
5. Verify that `serviceKeyChannelAvailable` is **boolean evidence only**. Do not create, display, rotate or store service keys, or assume a key is valid merely because it has the expected shape. A missing key must not be patched around.
6. Keep the WP21 live controller and independent recovery unchanged unless a specific reproducible safety defect requires a reviewed fix. Do not write WP21 `baseline.json` or `consumed.json` during WP23. The read-only observation is not the fresh live rollback baseline; a future authorized controller must capture that independently immediately before mutation.
7. Run relevant offline Python tests, PowerShell parsing where available, and UTF-8/LF and `git diff --check`. If PowerShell `-File` is blocked by Codex's host policy, report it and rely on operator execution. No `-ExecutionPolicy Bypass`, `Set-ExecutionPolicy`, alternate identity, script-text evaluation or other workaround.
8. If code changes are necessary, open a **small, reviewable, unmerged PR** with tests and sanitized runbook. If existing merged WP22 functionality is sufficient, do **not** create a no-op PR: return exact operator instructions and explicitly state that the operator's actual read-only Azure evidence is pending.

## Suggested operator commands (review against actual merged code before returning)

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp21ControllerQualification.ps1
$prepared = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -PreparePrivateAttempt | ConvertFrom-Json
if ($LASTEXITCODE -ne 0 -or -not $prepared.attemptPrepared -or $prepared.approvalEnabled) { throw 'Preparation failed or approval unexpectedly enabled' }
$env:AIQ_WP21_ATTEMPT_DIRECTORY = Join-Path (Join-Path $env:LOCALAPPDATA 'AIQ-WP21') $prepared.attemptId
$inspection = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -InspectAzureReadOnly | ConvertFrom-Json
$inspection | Select-Object result,attemptId,azureBaselineVerified,serviceKeyChannelAvailable,approvalEnabled,liveControllerInvoked,liveAttemptReady,RESEARCH_PROCESS_ALIVE,PUBLIC_RUNTIME_VERIFIED,failureReason | ConvertTo-Json
```

Run the read-only inspection only in the operator's authorized Azure session. If the entry is blocked or authentication fails, stop and report the sanitized failure. Do not rerun with policy or permission bypasses. The above commands are a proposed handoff, not evidence that they ran.

## Completion and report

State separately: offline tests, operator PowerShell execution, private UUID preparation, Azure ARM read-only inspection, key-channel boolean, named approval, live controller invocation, process health and browser verification. Never conflate these.

Until actual operator evidence is received, retain:

- `azureBaselineVerified=false` / unverified
- `liveAttemptReady=false`
- `RESEARCH_PROCESS_ALIVE=UNVERIFIED`
- `AUTHENTIC_DATA_NOT_IMPORTED=True`
- `PUBLIC_RUNTIME_VERIFIED=False`

A read-only Azure success **does not** authorize live deployment. After operator qualification, return the named attempt UUID and sanitized outcome for owner review, and request a **separate explicit approval** before any live entry. Do not execute it in WP23.
