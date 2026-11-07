# Release 1.15 — WP28 Codex Prompt: Operator Azure Read-Only Worker-Stage Qualification

## Authority and baseline

PR #415 was squash-merged into `main` at commit `a58806f520df31be02d6a8c8460d1816172f8fa0`. Work only from a clean checkout containing that commit. WP28 is an operator qualification and evidence handoff, **not** a deployment, import, or authorization work package.

Objective: run the already-implemented WP21/WP22/WP24/WP26/#415 offline qualification and, only in the owner's existing authorized Windows PowerShell 5.1/Azure CLI session, perform **one** read-only ARM baseline inspection using **one new disabled private attempt**. Determine the actual `workerStage` and fixed transport category if the first capture fails. Do not infer an HTTP status or an authentication/RBAC root cause from the stage alone.

## Hard constraints

- Preserve every historical private attempt, including `a6a6a450-6479-473b-8aa7-8d828b671058` and `098374d7-2bd7-46ae-af33-d9a125b2b418`, and the WP27 attempt. Never read, reuse, retry, extend, re-arm, modify or clean up their evidence.
- No Azure mutation, deployment, live controller, recovery execution, image rebuild/push, service-key disclosure, Twelve Data acquisition, browser/public runtime probe or change to the existing F1 resource.
- Do not alter execution policy, Azure identity, roles, credentials, quotas, runtime limits, worker protocol or validation rules. Do not use policy bypasses.
- Retain exactly eight allowed read-only ARM operations per capture, two captures, strict equality, the existing 40-second worker watchdog, 45-second parent timeout, five-second owned-child cleanup, disabled approval, private template freshness and fail-closed observation writes.
- `httpStatus` remains `null` unless a separately reviewed protocol reliably supplies it; this WP does not change that protocol.
- Never print raw CLI stderr/stdout, exception messages, ARM bodies, settings, paths to private records, worker journals, tokens, service keys, or environment dumps. Return only the sanitized selected JSON fields.
- `approvalEnabled=false`, `liveControllerInvoked=false`, `liveAttemptReady=false`, `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False` throughout.

## Codex tasks

1. Confirm `main` contains merge commit `a58806f520df31be02d6a8c8460d1816172f8fa0`; review the existing runbook at `docs/roadmap/release-1.15/prompters/release-1.15-wp22-attempt-preparation-runbook.md`, and inspect the exact entry points `eng/docker/release-1.15/Test-Wp22AttemptPreparation.ps1` and `Test-Wp21ControllerQualification.ps1`. Do not create a new wrapper that drops `workerStage`.
2. Run the existing offline qualification tests in the Codex environment, if supported. Report the exact test counts and any environment limitations without claiming operator or Azure success.
3. Hand the following commands to the owner for execution from the **clean merged checkout** in the already authorized Windows PowerShell/Azure CLI session. Codex must not execute Azure calls or access historical private attempts.
4. Require one new disabled UUID and one inspection within the 15-minute freshness window. Stop on any failure; no automatic or manual retries. If the session or script execution is blocked, report the blocker without changing policy or identity.
5. Interpret only the sanitized returned fields. If `workerStage=INITIALIZATION`, examine only non-secret local CLI installation compatibility through offline evidence; if `REQUEST`, do not claim auth/RBAC/HTTP cause; if `INPUT`, `RESPONSE_VALIDATION`, or `RESPONSE_DECODING`, distinguish the execution boundary but do not relax checks. If a non-stage worker failure occurs, use its fixed category. If baseline succeeds, mark only read-only baseline verified—not live readiness.
6. Create **no PR** if the merged code works. If concrete operator evidence demonstrates a code defect, propose the smallest correction with mocked regression tests and an open, unmerged PR; obtain owner review before another Azure attempt. Do not use repeated attempts as a diagnostic technique.

## Owner commands (run only after offline review)

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1
if ($LASTEXITCODE -ne 0) { throw 'Offline qualification failed; stop.' }

powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp21ControllerQualification.ps1
if ($LASTEXITCODE -ne 0) { throw 'Operator preflight failed; stop.' }

$prepared = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -PreparePrivateAttempt | ConvertFrom-Json
if ($LASTEXITCODE -ne 0 -or $prepared.attemptPrepared -ne $true -or $prepared.approvalEnabled -ne $false) { throw 'Disabled attempt preparation failed; stop.' }

$env:AIQ_WP21_ATTEMPT_DIRECTORY = Join-Path (Join-Path $env:LOCALAPPDATA 'AIQ-WP21') $prepared.attemptId
$inspection = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -InspectAzureReadOnly | ConvertFrom-Json
$inspectionExit = $LASTEXITCODE
$inspection | Select-Object result,attemptId,failedStage,failureCategory,transportFailureCategory,workerStage,httpStatus,azureBaselineVerified,serviceKeyChannelChecked,serviceKeyChannelAvailable,approvalEnabled,liveControllerInvoked,liveAttemptReady,RESEARCH_PROCESS_ALIVE,AUTHENTIC_DATA_NOT_IMPORTED,PUBLIC_RUNTIME_VERIFIED | ConvertTo-Json
if ($inspectionExit -ne 0) { throw 'Read-only inspection blocked; stop and share sanitized JSON only.' }
```

Do not run preparation until ready for the single immediate inspection. The selected JSON is the only permitted operator handoff; redact anything unexpected before sharing.

## Acceptance and report

Provide: verified source commit, offline test results, whether operator execution was actually performed (by whom), the new attempt ID if created, the sanitized JSON fields, exact observed stage/category if failure, and the smallest evidence-grounded next step. Explicitly distinguish a real operator observation from mocked test output. Do not claim an Azure baseline, service-key check, or runtime verification without evidence.

**No live deployment authority.** `liveAttemptReady=false`; `PUBLIC_RUNTIME_VERIFIED=False`.
