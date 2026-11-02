#Requires -Version 5.1
# UTF-8/LF. Offline by default. No generation, import, Azure access or mutation.
[CmdletBinding()]
param([switch]$CheckLocalChannel, [switch]$ApprovedRetentionAndTransfer, [switch]$SessionSafetyReviewed)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version 2.0
$report = [ordered]@{
    result = 'OFFLINE_PREFLIGHT_COMPLETE_CONFIGURATION_BLOCKED'
    localChannelChecked = $false
    localChannelShapeValid = $false
    retentionAndTransferConfirmedByOperator = [bool]$ApprovedRetentionAndTransfer
    azureIdentityVerified = $false
    azureSettingAbsenceVerified = $false
    azureSettingPresenceVerified = $false
    channelsEqualVerified = $false
    authenticatedApiVerified = $false
    azureInvoked = $false
    configurationChangeAllowed = $false
    approvalEnabled = $false
    liveControllerInvoked = $false
    liveAttemptReady = $false
    RESEARCH_PROCESS_ALIVE = 'UNVERIFIED'
    AUTHENTIC_DATA_NOT_IMPORTED = $true
    PUBLIC_RUNTIME_VERIFIED = $false
    failureReason = $null
}
$exitCode = 0
try {
    if ($PSVersionTable.PSVersion.Major -ne 5 -or $PSVersionTable.PSVersion.Minor -ne 1) { throw 'BLOCKED' }
    $helper = Join-Path $PSScriptRoot 'Wp31-ServiceKey.ps1'
    if (-not (Test-Path -LiteralPath $helper -PathType Leaf)) { throw 'BLOCKED' }
    if ($CheckLocalChannel) {
        if (-not $ApprovedRetentionAndTransfer -or -not $SessionSafetyReviewed) { throw 'BLOCKED' }
        # Definitions only; never call import/clear or prompt for a secret here.
        . $helper
        $local = Test-Wp31ServiceKey
        if ($local.result -ne 'KEY_CHANNEL_CHECKED' -or $local.processChannelValid -isnot [bool]) { throw 'BLOCKED' }
        $report.localChannelChecked = $true
        $report.localChannelShapeValid = $local.processChannelValid
        $report.result = 'LOCAL_CHECK_COMPLETE_CONFIGURATION_BLOCKED'
        if (-not $local.processChannelValid) {
            $report.result = 'LOCAL_CHECK_BLOCKED'
            $report.failureReason = 'LOCAL_CHANNEL_UNAVAILABLE_OR_INVALID'
            $exitCode = 1
        }
    }
} catch {
    $report.result = 'PREFLIGHT_BLOCKED'
    $report.failureReason = 'LOCAL_PREREQUISITE_UNVERIFIED'
    $exitCode = 1
}
$report | ConvertTo-Json -Compress
exit $exitCode
