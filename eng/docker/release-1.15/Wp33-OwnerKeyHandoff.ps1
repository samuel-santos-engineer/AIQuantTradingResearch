#Requires -Version 5.1
# UTF-8/LF. Dot-source in the owner's existing session. No generator or Azure calls.
if ($MyInvocation.InvocationName -ne '.') {
    Write-Output '{"result":"DOT_SOURCE_REQUIRED","processChannelValid":false}'
    return
}
. (Join-Path $PSScriptRoot 'Wp31-ServiceKey.ps1')

function Import-Wp33OwnerKey {
    [CmdletBinding()]
    param([switch]$SessionSafetyReviewed)
    $candidate = $null
    $loaded = $false
    $clearConfirmed = $false
    try {
        # Fail closed on missing/malformed source; never retain a stale channel.
        $clear = Clear-Wp31ServiceKey
        if (-not $clear.processChannelCleared) { throw 'BLOCKED' }
        $clearConfirmed = $true
        if (-not $SessionSafetyReviewed) { throw 'BLOCKED' }
        # Exact caller scope only. No key-valued command parameter or output.
        $candidate = Get-Variable -Name key -Scope 1 -ValueOnly -ErrorAction Stop
        if ($candidate -isnot [string] -or $candidate -cnotmatch '\A[0-9a-f]{64}\z') { throw 'BLOCKED' }
        [Environment]::SetEnvironmentVariable('AIQ_WP21_SERVICE_KEY', $candidate, [EnvironmentVariableTarget]::Process)
        $loaded = (Test-Wp31ServiceKey).processChannelValid -and [string]::Equals(
            $candidate, [Environment]::GetEnvironmentVariable('AIQ_WP21_SERVICE_KEY', [EnvironmentVariableTarget]::Process), [StringComparison]::Ordinal)
        if (-not $loaded) { throw 'BLOCKED' }
        $clearConfirmed = $false
    } catch {
        $loaded = $false
    } finally {
        $candidate = $null
        if (-not $loaded) {
            $clear = Clear-Wp31ServiceKey
            $clearConfirmed = $clear.processChannelCleared
        }
    }
    [pscustomobject]@{
        result = $(if ($loaded) { 'OWNER_KEY_IMPORTED_PROCESS_ONLY' } elseif ($clearConfirmed) { 'OWNER_KEY_IMPORT_BLOCKED_CHANNEL_CLEARED' } else { 'KEY_CHANNEL_STATE_UNVERIFIED' })
        processChannelValid = [bool]$loaded
        processChannelCleared = [bool]$clearConfirmed
        azureInvoked = $false
        approvalEnabled = $false
        liveControllerInvoked = $false
        liveAttemptReady = $false
        RESEARCH_PROCESS_ALIVE = 'UNVERIFIED'
        AUTHENTIC_DATA_NOT_IMPORTED = $true
        PUBLIC_RUNTIME_VERIFIED = $false
    }
}
