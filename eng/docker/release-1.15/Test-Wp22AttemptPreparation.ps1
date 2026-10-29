#Requires -Version 5.1
# UTF-8/LF; no policy or identity changes. Offline unless explicitly selected.
[CmdletBinding()]
param([switch]$PreparePrivateAttempt,[switch]$InspectAzureReadOnly)
$ErrorActionPreference='Stop'
Set-StrictMode -Version 2.0
try {
    if($PreparePrivateAttempt -and $InspectAzureReadOnly){throw 'Choose one mode'}
    $python=Get-Command python -CommandType Application -ErrorAction Stop | Select-Object -First 1
    $entry=Join-Path $PSScriptRoot '../../azure-cli/r1.15-qualification/wp22_preparation.py'
    if($PreparePrivateAttempt){ & $python.Source -B $entry --prepare-private-attempt }
    elseif($InspectAzureReadOnly){ & $python.Source -B $entry --inspect-azure-read-only }
    else { & $python.Source -B $entry }
    exit $LASTEXITCODE
} catch {
    Write-Output '{"result":"PREPARATION_BLOCKED","failureReason":"LOCAL_PREREQUISITE_UNVERIFIED","PUBLIC_RUNTIME_VERIFIED":false,"liveAttemptReady":false}'
    exit 1
}
