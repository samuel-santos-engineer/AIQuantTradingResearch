# Release 1.15 — WP27 Codex Prompt: Operator Azure Read-Only Transport Qualification

## Authority and baseline

Work in `samuel-santos-engineer/AIQuantTradingResearch` from clean `main` including merged PR #414, squash commit `4c4fd398b07c932be00f6d076c664ad27d27cea8`. WP26's fixed transport failure categories are now available. This is a **diagnostic qualification only**, not permission to deploy, mutate Azure, acquire data, or probe a public runtime.

Historical private attempts, including `a6a6a450-6479-473b-8aa7-8d828b671058` and `098374d7-2bd7-46ae-af33-d9a125b2b418`, and all their records are immutable. Never inspect, reuse, reset, extend, arm, delete or modify them. Do not change execution policy, Azure identity/RBAC, CLI installation, subscriptions, resource configuration, service keys, credentials, or worker timeouts to make a check pass.

## Goal

Determine the *actual supported transport-failure category* for the first research-site ARM GET by performing at most one newly prepared disabled attempt and one read-only inspection **in the authorized owner's Windows PowerShell/Azure CLI session**. Codex's restricted sandbox is not an authorized substitute. Do not guess an HTTP status: the current bridge has no trustworthy HTTP status envelope, so `httpStatus` remains `null`.

## Tasks

1. Verify clean checkout and merge SHA. Read the existing WP22 runbook's WP26 section, `eng/azure-cli/r1.15-qualification/wp22_preparation.py`, `wp21_runtime.py`, and existing PowerShell entry points. Do not create new governance, gates or wrappers.
2. Run offline unit tests using the four existing commands documented in the WP26 runbook; report exact counts, command exit statuses and any host execution-policy limitation. Confirm that read-only operations, dual captures, disabled approval, strict baseline and bounded timeouts are unchanged.
3. Provide the owner the exact post-merge operator handoff from the runbook. In the already-authorized Windows session, owner first runs offline qualification and WP21 operator preflight, then prepares **one fresh disabled UUID** and performs **one** `-InspectAzureReadOnly` within the 15-minute validity window. Stop immediately if a prerequisite fails. Never retry an Azure inspection or attempt preparation to mask a failure.
4. Ask the owner to share **only sanitized selected JSON fields**: `result`, `attemptId`, `failedStage`, `failureCategory`, `transportFailureCategory`, `httpStatus`, `azureBaselineVerified`, `serviceKeyChannelChecked`, `serviceKeyChannelAvailable`, `approvalEnabled`, `liveControllerInvoked`, `liveAttemptReady`, `RESEARCH_PROCESS_ALIVE`, `AUTHENTIC_DATA_NOT_IMPORTED`, `PUBLIC_RUNTIME_VERIFIED`. Do not request private paths, raw CLI stderr/stdout, request/response bodies, secrets, worker journals, observation contents or environment dumps.
5. Interpret only the fixed enums. `WORKER_NONZERO_EXIT` is not proof of 401/403, RBAC failure, worker watchdog or an HTTP rejection. `UNKNOWN` remains unknown. `serviceKeyChannelChecked=false` means unchecked, not absent. A successful read-only baseline is not deployment approval and is not an immutable live rollback baseline.
6. If a *reproducible code defect* is demonstrated by safe, mocked evidence, propose the smallest corrective change and tests, and open a **reviewable, unmerged PR**. Do not broaden to live deployment or protocol redesign. If no code defect is evidenced, **do not create a PR**; return operator result and recommend the next narrowly scoped action based on the fixed failure category.

## Operator commands (repository root; owner-controlled authorized session)

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1
if ($LASTEXITCODE -ne 0) { throw 'Offline qualification failed; stop.' }
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp21ControllerQualification.ps1
if ($LASTEXITCODE -ne 0) { throw 'Operator preflight failed; stop.' }
$prepared = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -PreparePrivateAttempt | ConvertFrom-Json
if ($LASTEXITCODE -ne 0 -or $prepared.attemptPrepared -ne $true -or $prepared.approvalEnabled -ne $false) { throw 'Disabled preparation failed; stop.' }
$env:AIQ_WP21_ATTEMPT_DIRECTORY = Join-Path (Join-Path $env:LOCALAPPDATA 'AIQ-WP21') $prepared.attemptId
$inspection = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -InspectAzureReadOnly | ConvertFrom-Json
$inspectionExit = $LASTEXITCODE
$inspection | Select-Object result,attemptId,failedStage,failureCategory,transportFailureCategory,httpStatus,azureBaselineVerified,serviceKeyChannelChecked,serviceKeyChannelAvailable,approvalEnabled,liveControllerInvoked,liveAttemptReady,RESEARCH_PROCESS_ALIVE,AUTHENTIC_DATA_NOT_IMPORTED,PUBLIC_RUNTIME_VERIFIED | ConvertTo-Json
if ($inspectionExit -ne 0) { throw 'Read-only inspection blocked; stop; share sanitized JSON only.' }
```

These commands are for the owner to execute, not Codex to execute in its sandbox. The PowerShell throw after displaying the selected JSON is intentional. Do not repeat the inspection if it fails.

## Deliverable

A concise qualification report: merged baseline SHA, offline test evidence, whether the owner ran the commands, new disabled attempt ID if actually prepared, exact sanitized result if actually obtained, and the smallest evidence-grounded next step. Distinguish proposed commands from executed commands. State clearly if no operator run occurred. Do not fabricate an Azure result or mark any public runtime verified.

Mandatory invariants: `approvalEnabled=false`, `liveControllerInvoked=false`, `liveAttemptReady=false`, `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`. No live controller, Azure mutation, deployment, Twelve Data calls, public browser probes, or new infrastructure.
