# Release 1.15 — WP29 Codex Prompt: Post-Fix Azure Read-Only Qualification

## Authority and starting point

Work in `samuel-santos-engineer/AIQuantTradingResearch` from the clean merged `main` containing PR #416, squash commit `7cc0aa0dc256ceb8ee0cc8162f7f0957a3906f37`. PR #416 corrected both WP21 and read-only ARM workers to pass `headers=['Content-Type=application/json']` and set `cli.data['command']='rest'` before `send_raw_request`. This is a qualification and evidence task, **not** permission to deploy, alter Azure resources, arm an attempt, acquire market data, or run a live controller.

Prior WP28 evidence identified `FIRST_CAPTURE_SITE` / `AZURE_TRANSPORT_FAILURE` / `WORKER_REPORTED_FAILURE` / `REQUEST`, with `httpStatus=null`; offline reproduction identified a pre-network Azure CLI helper incompatibility, now fixed. Do not assume the live Azure request will succeed until operator evidence confirms it. `PUBLIC_RUNTIME_VERIFIED=False`.

## Objectives

1. Confirm the checkout includes merged PR #416 and inspect the existing operator handoff in `docs/roadmap/release-1.15/prompters/release-1.15-wp22-attempt-preparation-runbook.md`.
2. Run offline qualification on the existing WP22/WP26/WP28 and WP21/WP19/WP18 test suites. Verify the exact installed-CLI compatibility regression really executes on the operator host; distinguish an actual pass from a skipped test. Do not claim a real Azure check from mocked tests.
3. Prepare the operator to run **one** fresh disabled private attempt, followed by **one** read-only Azure inspection in its 15-minute window, using the merged existing PowerShell entry points and the current `workerStage` fields. Do not run Azure calls in an unauthorized Codex environment. If the operator environment is unavailable, return a copy-paste-ready handoff and explicitly state that live Azure qualification remains pending.
4. Analyze only the resulting sanitized selected JSON if the authorized operator supplies it. Confirm `failedStage`, `failureCategory`, `transportFailureCategory`, `workerStage`, `httpStatus`, `azureBaselineVerified`, `serviceKeyChannelChecked`, `serviceKeyChannelAvailable`, `approvalEnabled`, `liveControllerInvoked`, `liveAttemptReady`, `RESEARCH_PROCESS_ALIVE`, `AUTHENTIC_DATA_NOT_IMPORTED`, `PUBLIC_RUNTIME_VERIFIED`.
5. If read-only baseline succeeds, report that success **only for read-only qualification** and whether the service-key channel was actually checked. Do not infer live readiness, immutable rollback baseline, research health, authentic candle import, or browser chart verification.
6. If it fails, isolate the smallest evidence-backed next action. Do not infer auth/RBAC/HTTP status from `REQUEST`, stderr, exit codes or messages. If a code defect is proven, create a small reviewable PR and leave it **open and unmerged**. If no code defect is proven, do not create a speculative PR.

## Operator commands (after offline verification, authorized Windows PowerShell 5.1/Azure CLI session only)

Run from the clean merged repository root. Use the existing direct scripts; do not use older wrappers, policy bypass, alternative credentials or historical attempt directories.

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
$inspection | Select-Object result,attemptId,failedStage,failureCategory,transportFailureCategory,workerStage,httpStatus,azureBaselineVerified,serviceKeyChannelChecked,serviceKeyChannelAvailable,approvalEnabled,liveControllerInvoked,liveAttemptReady,RESEARCH_PROCESS_ALIVE,AUTHENTIC_DATA_NOT_IMPORTED,PUBLIC_RUNTIME_VERIFIED | ConvertTo-Json
if ($inspectionExit -ne 0) { throw 'Inspection blocked; stop; share sanitized JSON only.' }
```

**Stop on any failure.** A failed inspection consumes this attempt for purposes of this handoff: do not retry, reset, extend, inspect, arm or rewrite it. Preserve all WP23/WP25/WP27/WP28 and other historical private attempts unchanged. Return only the sanitized selected JSON; never disclose private paths, observation/approval records, credentials, configuration, raw worker streams, tokens or keys.

## Non-negotiable boundaries

- No changes to Azure resources, App Service configuration, identity/RBAC, startup, image, secrets, logging, slots, F1 plan, or deployed UI.
- No live WP21 controller/recovery execution, deployment, rollback, market-data acquisition, public health/browser probe or extra ARM requests.
- Preserve the eight read-only ARM operations per capture, two captures, strict equality, bounded timeouts, ownership-before-dispatch, redaction, fail-closed observation persistence, and disabled approvals.
- No authentication or HTTP status assertions without trustworthy typed evidence. `httpStatus=null` is expected with the current protocol.
- If code changes are necessary, use minimal scope, mocked offline regression tests, document the operator handoff, and open an unmerged PR for review. Do not merge your own PR.
- Never convert read-only success into live approval. Maintain `liveAttemptReady=false`, `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False` unless independently authorized and actually verified in a future milestone.

## Deliverable

Provide (a) merged-base verification, (b) exact tests and actual pass/skip counts, (c) whether an authorized real operator inspection was performed, (d) sanitized qualification JSON if available, (e) precise evidence-supported result and next action, and (f) PR URL only if a demonstrated defect warranted a code change. Do not invent Azure evidence.
