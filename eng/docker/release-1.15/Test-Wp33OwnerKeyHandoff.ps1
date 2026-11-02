#Requires -Version 5.1
# UTF-8/LF. Run only in a disposable child, fake values only.
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version 2.0
$count = 0
$stage = 'PREREQUISITES'
try {
    if (Test-Path Env:AIQ_WP21_SERVICE_KEY) { throw 'KEY_FREE_TEST_PROCESS_REQUIRED' }
    . (Join-Path $PSScriptRoot 'Wp33-OwnerKeyHandoff.ps1')
    function Assert-Wp33 { param([bool]$Condition) if (-not $Condition) { throw 'OFFLINE_ASSERTION_FAILED' } }
    $stage = 'VALID_CALLER_HANDOFF'
    $key = 'a' * 64
    $beforePid = $PID
    $result = @(Import-Wp33OwnerKey -SessionSafetyReviewed *>&1)
    Assert-Wp33 ($result.Count -eq 1 -and $result[0].result -eq 'OWNER_KEY_IMPORTED_PROCESS_ONLY')
    Assert-Wp33 ($result[0].processChannelValid -and $PID -eq $beforePid)
    Assert-Wp33 ([Environment]::GetEnvironmentVariable('AIQ_WP21_SERVICE_KEY',[EnvironmentVariableTarget]::Process) -ceq $key)
    Assert-Wp33 (($result | ConvertTo-Json -Compress) -notlike ('*' + $key + '*'))
    Assert-Wp33 ($key -ceq ('a' * 64))
    $count++
    $stage = 'UNRELATED_VARIABLE_AND_CLEAR'
    $env:AIQ_WP33_SENTINEL = 'preserve'
    Assert-Wp33 ((Clear-Wp31ServiceKey).processChannelCleared)
    Assert-Wp33 ($env:AIQ_WP33_SENTINEL -eq 'preserve')
    $count++
    $stage = 'ABSENT_SOURCE_CLEARS_STALE'
    Remove-Variable key
    $env:AIQ_WP21_SERVICE_KEY = 'b' * 64
    $result = @(Import-Wp33OwnerKey -SessionSafetyReviewed *>&1)
    Assert-Wp33 ($result.Count -eq 1 -and $result[0].result -eq 'OWNER_KEY_IMPORT_BLOCKED_CHANNEL_CLEARED')
    Assert-Wp33 (-not (Test-Path Env:AIQ_WP21_SERVICE_KEY))
    $count++
    $index = 0
    foreach ($invalid in @($null,'',('a' * 63),('a' * 65),('A' * 64),('g' * 64),(('a' * 64) + "`n"),42,@('a','b'))) {
        $index++
        $stage = 'INVALID_SOURCE_' + $index
        $key = $invalid
        $env:AIQ_WP21_SERVICE_KEY = 'b' * 64
        $result = @(Import-Wp33OwnerKey -SessionSafetyReviewed *>&1)
        Assert-Wp33 ($result.Count -eq 1 -and $result[0].result -eq 'OWNER_KEY_IMPORT_BLOCKED_CHANNEL_CLEARED')
        Assert-Wp33 (-not (Test-Path Env:AIQ_WP21_SERVICE_KEY))
        $count++
    }
    $stage = 'MISSING_SAFETY_CONFIRMATION'
    $key = 'c' * 64
    $env:AIQ_WP21_SERVICE_KEY = 'b' * 64
    $result = Import-Wp33OwnerKey
    Assert-Wp33 ($result.result -eq 'OWNER_KEY_IMPORT_BLOCKED_CHANNEL_CLEARED')
    Assert-Wp33 (-not (Test-Path Env:AIQ_WP21_SERVICE_KEY))
    $count++
    $stage = 'REPLACEMENT_AND_FINALLY_CLEAR'
    $expectedFailure = $false
    try {
        $key = 'd' * 64
        $result = Import-Wp33OwnerKey -SessionSafetyReviewed
        Assert-Wp33 $result.processChannelValid
        Assert-Wp33 ([Environment]::GetEnvironmentVariable('AIQ_WP21_SERVICE_KEY',[EnvironmentVariableTarget]::Process) -ceq $key)
        throw 'FAKE_FAILURE_AFTER_HANDOFF'
    } catch { $expectedFailure = $_.Exception.Message -ceq 'FAKE_FAILURE_AFTER_HANDOFF' } finally { $null = Clear-Wp31ServiceKey }
    Assert-Wp33 $expectedFailure
    Assert-Wp33 (-not (Test-Path Env:AIQ_WP21_SERVICE_KEY))
    $count++
    @{ result='OFFLINE_OWNER_HANDOFF_TESTS_PASS'; fakeCases=$count; actualKeyUsed=$false; azureInvoked=$false; PUBLIC_RUNTIME_VERIFIED=$false } | ConvertTo-Json -Compress
    exit 0
} catch {
    @{ result='OFFLINE_OWNER_HANDOFF_TESTS_FAILED'; failedTestStage=$stage; completedCases=$count; actualKeyUsed=$false; PUBLIC_RUNTIME_VERIFIED=$false } | ConvertTo-Json -Compress
    exit 1
} finally {
    [Environment]::SetEnvironmentVariable('AIQ_WP21_SERVICE_KEY',$null,[EnvironmentVariableTarget]::Process)
    [Environment]::SetEnvironmentVariable('AIQ_WP33_SENTINEL',$null,[EnvironmentVariableTarget]::Process)
    Remove-Variable key -ErrorAction SilentlyContinue
}
