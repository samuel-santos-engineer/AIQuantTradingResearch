#Requires -Version 5.1
<#
UTF-8/LF. Windows PowerShell 5.1 operator handoff. No policy changes.
One orchestrator using CLI compatibility fix, worker stages and fixed validation-rule diagnostics and WP21/WP22 helpers.
Use -RepositoryRoot for the clean merged-main checkout; no helper is copied.
Default: offline gates only. -PreparePrivateAttempt creates a disabled UUID.
-InspectAzureReadOnly explicitly selects preparation plus read-only ARM inspection.
No live approval, deployment, provider request or application HTTPS probe.
#>
[CmdletBinding()]
param(
    [string]$RepositoryRoot = (Join-Path $PSScriptRoot '../../..'),
    [switch]$PreparePrivateAttempt,
    [switch]$InspectAzureReadOnly
)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version 2.0
$gates = New-Object 'System.Collections.Generic.List[object]'
$report = [ordered]@{
    result = 'WP29_BLOCKED'
    gates = $gates
    attemptPrepared = $false
    attemptId = $null
    azureBaselineVerified = $false
    serviceKeyChannelChecked = $false
    failedStage = $null
    failureCategory = $null
    validationRule = $null
    transportFailureCategory = $null
    httpStatus = $null
    workerStage = $null
    serviceKeyChannelAvailable = $false
    approvalEnabled = $false
    liveControllerInvoked = $false
    liveAttemptReady = $false
    RESEARCH_PROCESS_ALIVE = 'UNVERIFIED'
    AUTHENTIC_DATA_NOT_IMPORTED = $true
    PUBLIC_RUNTIME_VERIFIED = $false
    failureReason = $null
}
$allowedStages = @('PRIVATE_ATTEMPT_TEMPLATE','PRIVATE_ATTEMPT_PREPARATION','OFFLINE_QUALIFICATION',
    'OPERATOR_PREFLIGHT','BASELINE_COMPARISON','POST_CAPTURE_TEMPLATE_RECHECK',
    'SERVICE_KEY_CHANNEL_CHECK','OBSERVATION_WRITE')
foreach ($prefix in @('FIRST_CAPTURE_','SECOND_CAPTURE_')) {
    $allowedStages += $prefix + 'VALIDATION'
    foreach ($name in @('SITE','PLAN','WEB','APP_SETTINGS','CONNECTION_STRINGS','LOGS','SLOT_FLAGS','SLOTS')) {
        $allowedStages += $prefix + $name
        $allowedStages += $prefix + $name + '_RESPONSE_VALIDATION'
    }
}
$allowedCategories = @('LOCAL_PREREQUISITE_UNVERIFIED','AZURE_CLI_UNAVAILABLE',
    'AZURE_TRANSPORT_FAILURE','AZURE_RESOURCE_RESPONSE_INVALID','AZURE_RESPONSE_SCHEMA_MISMATCH',
    'AZURE_TARGET_OR_PLAN_MISMATCH','AZURE_BASELINE_DRIFT','AZURE_READ_TIMEOUT',
    'AZURE_BASELINE_INCOMPLETE','OBSERVATION_PERSISTENCE_FAILED','UNCLASSIFIED_FAILURE')
$allowedTransportCategories = @('CLI_EXECUTABLE_UNAVAILABLE','WORKER_LAUNCH_FAILURE',
    'PARENT_TIMEOUT','WORKER_NONZERO_EXIT','INVALID_WORKER_OUTPUT','WORKER_IO_FAILURE','WORKER_REPORTED_FAILURE','UNKNOWN')
$allowedValidationRules = @('INCOMPLETE_BASELINE','BASELINE_TARGET_MISMATCH','BASELINE_STATE_INVALID',
    'BASELINE_STARTUP_MISSING','BASELINE_SETTINGS_INVALID','BASELINE_METADATA_MISSING',
    'BASELINE_CONNECTION_METADATA_UNVERIFIED','BASELINE_SLOT_FLAGS_MISSING','BASELINE_LOGGING_MISSING',
    'BASELINE_HOST_UNVERIFIED','BASELINE_STARTUP_UNVERIFIED','BASELINE_SLOT_METADATA_UNVERIFIED',
    'BASELINE_LOGGING_UNVERIFIED')
$allowedWorkerStages = @('INPUT','INITIALIZATION','REQUEST','RESPONSE_VALIDATION','RESPONSE_DECODING')
$activeGate = 'LOCAL_PREREQUISITES'
$exitCode = 1
$hadDirectory = Test-Path Env:AIQ_WP21_ATTEMPT_DIRECTORY
$oldDirectory = $env:AIQ_WP21_ATTEMPT_DIRECTORY

function Invoke-JsonEntry {
    param([string]$ScriptPath, [string[]]$EntryArguments = @())
    # Child -File honors the operator's policy. Never evaluate script text.
    $lines = @(& powershell.exe -NoProfile -NonInteractive -File $ScriptPath @EntryArguments 2>$null)
    $childExit = $LASTEXITCODE
    $text = $lines -join "`n"
    if ($text.Length -gt 65536) { throw 'REPORT_BOUND_EXCEEDED' }
    try { $value = $text | ConvertFrom-Json -ErrorAction Stop }
    catch { throw 'CHILD_REPORT_UNAVAILABLE' }
    if ($null -eq $value -or $value -is [array]) { throw 'CHILD_REPORT_INVALID' }
    return @{ ExitCode = $childExit; Value = $value }
}

function Add-Gate {
    param([string]$Name, [string]$State)
    $gates.Add([ordered]@{ gate = $Name; state = $State })
}

try {
    if ($PSVersionTable.PSVersion.Major -ne 5 -or $PSVersionTable.PSVersion.Minor -ne 1) {
        throw 'WINDOWS_POWERSHELL_51_REQUIRED'
    }
    $root = (Resolve-Path -LiteralPath $RepositoryRoot).Path
    $entries = Join-Path $root 'eng/docker/release-1.15'
    $wp22 = Join-Path $entries 'Test-Wp22AttemptPreparation.ps1'
    $wp21 = Join-Path $entries 'Test-Wp21ControllerQualification.ps1'
    if (-not (Test-Path -LiteralPath $wp22 -PathType Leaf) -or
        -not (Test-Path -LiteralPath $wp21 -PathType Leaf)) { throw 'MERGED_HELPERS_REQUIRED' }
    Add-Gate $activeGate 'PASS'

    $activeGate = 'WP22_OFFLINE_TESTS'
    $step = Invoke-JsonEntry $wp22
    if ($step.ExitCode -ne 0 -or $step.Value.result -ne 'OFFLINE_TESTS_PASS' -or
        $step.Value.offlineTests -lt 51) { throw 'OFFLINE_TESTS_FAILED' }
    Add-Gate $activeGate 'PASS'

    $activeGate = 'WP21_OFFLINE_AND_OPERATOR_PREFLIGHT'
    $step = Invoke-JsonEntry $wp21
    if ($step.ExitCode -ne 0 -or $step.Value.result -ne 'OFFLINE_QUALIFICATION_COMPLETE' -or
        $step.Value.privateControllerQualified -ne $true -or $step.Value.offlineTests -lt 38 -or
        $step.Value.operatorPreflightPassed -ne $true -or $step.Value.liveAttemptReady -ne $false -or
        $step.Value.PUBLIC_RUNTIME_VERIFIED -ne $false) { throw 'OPERATOR_PREFLIGHT_FAILED' }
    Add-Gate $activeGate 'PASS'

    if ($PreparePrivateAttempt -or $InspectAzureReadOnly) {
        $activeGate = 'FRESH_DISABLED_ATTEMPT'
        $step = Invoke-JsonEntry $wp22 @('-PreparePrivateAttempt')
        $value = $step.Value
        if ($step.ExitCode -ne 0 -or $value.result -ne 'PRIVATE_ATTEMPT_PREPARED_NOT_AUTHORIZED' -or
            $value.attemptPrepared -ne $true -or $value.approvalEnabled -ne $false -or
            $value.liveControllerInvoked -ne $false -or $value.liveAttemptReady -ne $false -or
            $value.PUBLIC_RUNTIME_VERIFIED -ne $false) { throw 'DISABLED_PREPARATION_FAILED' }
        $id = [guid]::Parse($value.attemptId).ToString('D')
        if ($id -cne $value.attemptId) { throw 'ATTEMPT_ID_INVALID' }
        $report.attemptPrepared = $true
        $report.attemptId = $id
        $env:AIQ_WP21_ATTEMPT_DIRECTORY = Join-Path (Join-Path $env:LOCALAPPDATA 'AIQ-WP21') $id
        Add-Gate $activeGate 'PASS'
    }

    if ($InspectAzureReadOnly) {
        $activeGate = 'AZURE_ARM_READ_ONLY_BASELINE'
        $step = Invoke-JsonEntry $wp22 @('-InspectAzureReadOnly')
        $value = $step.Value
        # WP26 has no trusted HTTP status envelope. Never infer or copy a status.
        if ($null -ne $value.httpStatus) { throw 'UNSUPPORTED_HTTP_STATUS' }
        if ($null -ne $value.transportFailureCategory) {
            if ($value.transportFailureCategory -isnot [string] -or
                $value.transportFailureCategory -cnotin $allowedTransportCategories) {
                throw 'INVALID_TRANSPORT_ENUM'
            }
            $report.transportFailureCategory = $value.transportFailureCategory
        }
        if ($null -ne $value.workerStage) {
            if ($value.workerStage -isnot [string] -or $value.workerStage -cnotin $allowedWorkerStages -or
                $value.transportFailureCategory -cne 'WORKER_REPORTED_FAILURE') {
                throw 'INVALID_WORKER_STAGE'
            }
            $report.workerStage = $value.workerStage
        } elseif ($value.transportFailureCategory -ceq 'WORKER_REPORTED_FAILURE') {
            throw 'MISSING_WORKER_STAGE'
        }
        if ($null -ne $value.validationRule) {
            if ($value.validationRule -isnot [string] -or $value.validationRule -cnotin $allowedValidationRules -or
                $value.failedStage -cnotin @('FIRST_CAPTURE_VALIDATION','SECOND_CAPTURE_VALIDATION')) {
                throw 'INVALID_VALIDATION_RULE'
            }
            $report.validationRule = $value.validationRule
        }
        # Accept only fixed diagnostic enums and typed booleans, including failure reports.
        if ($value.approvalEnabled -ne $false -or $value.liveControllerInvoked -ne $false -or
            $value.liveAttemptReady -ne $false -or $value.PUBLIC_RUNTIME_VERIFIED -ne $false -or
            $value.serviceKeyChannelChecked -isnot [bool] -or
            $value.serviceKeyChannelAvailable -isnot [bool]) { throw 'INVALID_SAFETY_REPORT' }
        if ($null -ne $value.failedStage) {
            if ($value.failedStage -isnot [string] -or $value.failedStage -cnotin $allowedStages -or
                $value.failureCategory -isnot [string] -or $value.failureCategory -cnotin $allowedCategories) {
                throw 'INVALID_DIAGNOSTIC_ENUM'
            }
            $report.failedStage = $value.failedStage
            $report.failureCategory = $value.failureCategory
        } elseif ($null -ne $value.failureCategory) { throw 'INVALID_DIAGNOSTIC_PAIR' }
        $report.serviceKeyChannelChecked = $value.serviceKeyChannelChecked
        $report.serviceKeyChannelAvailable = $value.serviceKeyChannelAvailable
        if ($step.ExitCode -ne 0 -or $value.result -ne 'READ_ONLY_BASELINE_VERIFIED_NOT_AUTHORIZED' -or
            $value.attemptId -cne $report.attemptId -or $value.azureBaselineVerified -ne $true -or
            $value.serviceKeyChannelChecked -ne $true -or $null -ne $value.failedStage -or
            $null -ne $value.transportFailureCategory -or $null -ne $value.workerStage -or
            $null -ne $value.validationRule -or
            $value.approvalEnabled -ne $false -or $value.liveControllerInvoked -ne $false -or
            $value.liveAttemptReady -ne $false -or $value.PUBLIC_RUNTIME_VERIFIED -ne $false) {
            throw 'READ_ONLY_BASELINE_UNVERIFIED'
        }
        $report.azureBaselineVerified = $true
        Add-Gate $activeGate 'PASS'
        $activeGate = 'SERVICE_KEY_CHANNEL_PRESENCE_ONLY'
        if ($value.serviceKeyChannelAvailable -isnot [bool]) { throw 'KEY_CHANNEL_UNKNOWN' }
        $report.serviceKeyChannelAvailable = $value.serviceKeyChannelAvailable
        if ($value.serviceKeyChannelAvailable) { Add-Gate $activeGate 'AVAILABLE_NOT_AUTHENTICATED' }
        else { Add-Gate $activeGate 'UNAVAILABLE' }
        $report.result = 'READ_ONLY_QUALIFICATION_COMPLETE_NOT_AUTHORIZED'
    } elseif ($PreparePrivateAttempt) {
        $report.result = 'DISABLED_ATTEMPT_PREPARED_AZURE_NOT_RUN'
    } else {
        $report.result = 'OFFLINE_GATES_PASSED_AZURE_NOT_RUN'
    }
    Add-Gate 'NAMED_LIVE_APPROVAL' 'NOT_GRANTED'
    Add-Gate 'HEALTH_AND_BROWSER' 'NOT_RUN'
    $exitCode = 0
} catch {
    # No raw exception, settings, token, private path or child output escapes.
    Add-Gate $activeGate 'BLOCKED'
    $report.failureReason = $activeGate + '_FAILED'
    if ($null -eq $report.failedStage) {
        $report.failureCategory = 'LOCAL_PREREQUISITE_UNVERIFIED'
    }
} finally {
    if ($hadDirectory) { $env:AIQ_WP21_ATTEMPT_DIRECTORY = $oldDirectory }
    else { Remove-Item Env:AIQ_WP21_ATTEMPT_DIRECTORY -ErrorAction SilentlyContinue }
    $report | ConvertTo-Json -Depth 6
}
exit $exitCode
