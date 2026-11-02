#Requires -Version 5.1
# UTF-8/LF. Disposable test process; fake repeated-character values only.
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version 2.0
$count = 0
try {
    if (Test-Path Env:AIQ_WP21_SERVICE_KEY) { throw 'RUN_ONLY_IN_DISPOSABLE_KEY_FREE_TEST_PROCESS' }
    . (Join-Path $PSScriptRoot 'Wp31-ServiceKey.ps1')
    function Assert-Wp31Test { param([bool]$Condition) if (-not $Condition) { throw 'OFFLINE_ASSERTION_FAILED' } }
    $script:fakeText = 'a' * 64
    $script:inputCalls = 0
    $script:lastSecure = $null
    function Read-Wp31MaskedServiceKey {
        $script:inputCalls++
        if ($script:fakeText -eq 'THROW_FAKE') { throw 'FAKE_SECRET_CANARY' }
        $script:lastSecure = New-Object Security.SecureString
        foreach ($character in $script:fakeText.ToCharArray()) { $script:lastSecure.AppendChar($character) }
        return $script:lastSecure
    }
    $beforePid = $PID
    $result = @(Import-Wp31ServiceKey -ApprovedRetentionAndTransfer -SessionSafetyReviewed *>&1)
    Assert-Wp31Test ($result.Count -eq 1 -and $result[0].result -eq 'KEY_IMPORTED_PROCESS_ONLY')
    Assert-Wp31Test ((Test-Wp31ServiceKey).processChannelValid -and $beforePid -eq $PID)
    Assert-Wp31Test ([Environment]::GetEnvironmentVariable('AIQ_WP21_SERVICE_KEY',[EnvironmentVariableTarget]::Process) -ceq $script:fakeText)
    Assert-Wp31Test (($result | ConvertTo-Json -Compress) -notlike ('*' + $script:fakeText + '*'))
    $disposed = $false
    try { $unused = $script:lastSecure.Length } catch { $disposed = $true }
    Assert-Wp31Test $disposed
    $count++
    Assert-Wp31Test ((Clear-Wp31ServiceKey).processChannelCleared -and -not (Test-Wp31ServiceKey).processChannelValid)
    $count++
    foreach ($invalid in @('',('a' * 63),('a' * 65),('g' * 64),('A' * 64),(('a' * 64) + "`n"),'THROW_FAKE')) {
        [Environment]::SetEnvironmentVariable('AIQ_WP21_SERVICE_KEY',('b' * 64),[EnvironmentVariableTarget]::Process)
        $script:fakeText = $invalid
        $result = @(Import-Wp31ServiceKey -ApprovedRetentionAndTransfer -SessionSafetyReviewed *>&1)
        Assert-Wp31Test ($result.Count -eq 1 -and $result[0].result -eq 'KEY_IMPORT_BLOCKED_CHANNEL_CLEARED')
        Assert-Wp31Test (-not (Test-Path Env:AIQ_WP21_SERVICE_KEY))
        Assert-Wp31Test (($result | ConvertTo-Json -Compress) -notmatch 'FAKE_SECRET_CANARY')
        $count++
    }
    $script:inputCalls = 0
    [Environment]::SetEnvironmentVariable('AIQ_WP21_SERVICE_KEY',('b' * 64),[EnvironmentVariableTarget]::Process)
    $result = Import-Wp31ServiceKey
    Assert-Wp31Test ($result.result -eq 'KEY_IMPORT_BLOCKED_CHANNEL_CLEARED' -and $script:inputCalls -eq 0)
    Assert-Wp31Test (-not (Test-Path Env:AIQ_WP21_SERVICE_KEY))
    $count++
    # Replacement uses the same current process and only the named variable.
    [Environment]::SetEnvironmentVariable('AIQ_WP31_TEST_SENTINEL','preserve',[EnvironmentVariableTarget]::Process)
    foreach ($replacement in @(('c' * 64),('d' * 64))) {
        $script:fakeText = $replacement
        $result = Import-Wp31ServiceKey -ApprovedRetentionAndTransfer -SessionSafetyReviewed
        Assert-Wp31Test $result.processChannelValid
        Assert-Wp31Test ([Environment]::GetEnvironmentVariable('AIQ_WP21_SERVICE_KEY',[EnvironmentVariableTarget]::Process) -ceq $replacement)
        $count++
    }
    $null = Clear-Wp31ServiceKey
    Assert-Wp31Test ($env:AIQ_WP31_TEST_SENTINEL -eq 'preserve')
    $count++
    @{ result='OFFLINE_HELPER_TESTS_PASS'; fakeInputCases=$count; actualKeyUsed=$false; azureInvoked=$false; PUBLIC_RUNTIME_VERIFIED=$false } | ConvertTo-Json -Compress
    exit 0
} catch {
    Write-Output '{"result":"OFFLINE_HELPER_TESTS_FAILED","actualKeyUsed":false,"PUBLIC_RUNTIME_VERIFIED":false}'
    exit 1
} finally {
    [Environment]::SetEnvironmentVariable('AIQ_WP21_SERVICE_KEY',$null,[EnvironmentVariableTarget]::Process)
    [Environment]::SetEnvironmentVariable('AIQ_WP31_TEST_SENTINEL',$null,[EnvironmentVariableTarget]::Process)
}
