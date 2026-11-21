#Requires -Version 5.1
<#
UTF-8/LF. Windows PowerShell 5.1 operator handoff. No policy changes.
One orchestrator using the reviewed WP21/WP22 helpers; not a standalone controller.
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
    result = 'WP23_BLOCKED'
    gates = $gates
    attemptPrepared = $false
    attemptId = $null
    azureBaselineVerified = $false
    serviceKeyChannelAvailable = $false
    approvalEnabled = $false
    liveControllerInvoked = $false
    liveAttemptReady = $false
    RESEARCH_PROCESS_ALIVE = 'UNVERIFIED'
    AUTHENTIC_DATA_NOT_IMPORTED = $true
    PUBLIC_RUNTIME_VERIFIED = $false
    failureReason = $null
}
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
        $step.Value.offlineTests -lt 19) { throw 'OFFLINE_TESTS_FAILED' }
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
        if ($step.ExitCode -ne 0 -or $value.result -ne 'READ_ONLY_BASELINE_VERIFIED_NOT_AUTHORIZED' -or
            $value.attemptId -cne $report.attemptId -or $value.azureBaselineVerified -ne $true -or
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
} finally {
    if ($hadDirectory) { $env:AIQ_WP21_ATTEMPT_DIRECTORY = $oldDirectory }
    else { Remove-Item Env:AIQ_WP21_ATTEMPT_DIRECTORY -ErrorAction SilentlyContinue }
    $report | ConvertTo-Json -Depth 6
}
exit $exitCode
