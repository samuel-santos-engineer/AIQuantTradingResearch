#Requires -Version 5.1
# UTF-8/LF. Dot-source in the owner parent session. No automatic secret access.
if ($MyInvocation.InvocationName -ne '.') {
    Write-Output '{"result":"DOT_SOURCE_REQUIRED","processChannelValid":false}'
    return
}

function Read-Wp31MaskedServiceKey {
    # Mockable input boundary. No plaintext/key-valued parameter exists.
    Read-Host 'Enter the retained research service key using the approved private input method' -AsSecureString -ErrorAction Stop
}

function Test-Wp31ServiceKey {
    [CmdletBinding()]
    param()
    $valid = $false
    try {
        $value = [Environment]::GetEnvironmentVariable('AIQ_WP21_SERVICE_KEY', [EnvironmentVariableTarget]::Process)
        $valid = $null -ne $value -and $value -cmatch '\A[0-9a-f]{64}\z'
    } catch { $valid = $false }
    finally { $value = $null }
    [pscustomobject]@{ result = 'KEY_CHANNEL_CHECKED'; processChannelValid = [bool]$valid }
}

function Clear-Wp31ServiceKey {
    [CmdletBinding()]
    param()
    $cleared = $false
    try {
        [Environment]::SetEnvironmentVariable('AIQ_WP21_SERVICE_KEY', $null, [EnvironmentVariableTarget]::Process)
        $cleared = [string]::IsNullOrEmpty([Environment]::GetEnvironmentVariable('AIQ_WP21_SERVICE_KEY', [EnvironmentVariableTarget]::Process))
    } catch { $cleared = $false }
    [pscustomobject]@{
        result = $(if ($cleared) { 'KEY_CHANNEL_CLEARED' } else { 'KEY_CHANNEL_STATE_UNVERIFIED' })
        processChannelValid = $false
        processChannelCleared = [bool]$cleared
    }
}

function Import-Wp31ServiceKey {
    [CmdletBinding()]
    param([switch]$ApprovedRetentionAndTransfer, [switch]$SessionSafetyReviewed)
    $secure = $null
    $pointer = [IntPtr]::Zero
    $plain = $null
    $loaded = $false
    $clearConfirmed = $false
    try {
        # Never retain an unknown/stale value after a failed replacement attempt.
        $clear = Clear-Wp31ServiceKey
        if (-not $clear.processChannelCleared) { throw 'BLOCKED' }
        $clearConfirmed = $true
        if (-not $ApprovedRetentionAndTransfer -or -not $SessionSafetyReviewed) { throw 'BLOCKED' }
        $secure = Read-Wp31MaskedServiceKey
        if ($secure -isnot [Security.SecureString]) { throw 'BLOCKED' }
        $pointer = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secure)
        $plain = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($pointer)
        if ($null -eq $plain -or $plain -cnotmatch '\A[0-9a-f]{64}\z') { throw 'BLOCKED' }
        [Environment]::SetEnvironmentVariable('AIQ_WP21_SERVICE_KEY', $plain, [EnvironmentVariableTarget]::Process)
        $loaded = (Test-Wp31ServiceKey).processChannelValid
        if (-not $loaded) { throw 'BLOCKED' }
        $clearConfirmed = $false
    } catch {
        $loaded = $false
    } finally {
        try {
            if ($pointer -ne [IntPtr]::Zero) { [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($pointer) }
        } catch { $loaded = $false }
        $plain = $null
        try {
            if ($secure -is [Security.SecureString]) { $secure.Dispose() }
        } catch { $loaded = $false }
        $secure = $null
        if (-not $loaded) {
            $clear = Clear-Wp31ServiceKey
            $clearConfirmed = $clear.processChannelCleared
        }
    }
    [pscustomobject]@{
        result = $(if ($loaded) { 'KEY_IMPORTED_PROCESS_ONLY' } elseif ($clearConfirmed) { 'KEY_IMPORT_BLOCKED_CHANNEL_CLEARED' } else { 'KEY_CHANNEL_STATE_UNVERIFIED' })
        processChannelValid = [bool]$loaded
        processChannelCleared = [bool]$clearConfirmed
        approvalEnabled = $false
        liveControllerInvoked = $false
        liveAttemptReady = $false
        PUBLIC_RUNTIME_VERIFIED = $false
    }
}
