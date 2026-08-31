$ErrorActionPreference = 'Continue'

$appName = 'aiqwp03wcus3d25217bd701'
$baseUrl = "https://$appName.azurewebsites.net"
$lockId = 'wp03-lock-20260830-r1'
$blockedBodyPath = Join-Path ([System.IO.Path]::GetTempPath()) "$lockId-blocked.json"
$releasedBodyPath = Join-Path ([System.IO.Path]::GetTempPath()) "$lockId-released.json"

try {
    Write-Host "WP03_LOCK_ID=$lockId"

    Write-Host '=== START HELD WRITE TRANSACTION ==='
    $hold = Invoke-RestMethod `
      -Method Post `
      -Uri "$baseUrl/lock/hold?id=$lockId&seconds=10" `
      -TimeoutSec 30
    $hold | ConvertTo-Json -Compress
    Write-Host 'WP03_LOCK_HOLD_START_EXIT_CODE=0'

    Write-Host '=== WAIT FOR LOCK TO BECOME ACTIVE ==='
    $active = $false
    for ($attempt = 1; $attempt -le 20; $attempt++) {
        Start-Sleep -Milliseconds 500
        $status = Invoke-RestMethod -Uri "$baseUrl/lock/status?id=$lockId" -TimeoutSec 15
        Write-Host "LOCK_STATUS_ATTEMPT=$attempt"
        $status | ConvertTo-Json -Compress
        if ($status.active -eq $true) {
            $active = $true
            break
        }
    }
    Write-Host "WP03_LOCK_ACTIVE=$active"

    Write-Host '=== CONCURRENT READ WHILE WRITE LOCK HELD ==='
    try {
        $reader = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 30
        $reader | ConvertTo-Json -Depth 10 -Compress
        Write-Host 'WP03_LOCKED_READER_EXIT_CODE=0'
    } catch {
        Write-Host "WP03_LOCKED_READER_ERROR=$($_.Exception.Message)"
        Write-Host 'WP03_LOCKED_READER_EXIT_CODE=1'
    }

    Write-Host '=== COMPETING WRITE WITH 1000ms BUSY TIMEOUT ==='
    $blockedStatus = & curl.exe --silent --show-error `
      --output $blockedBodyPath `
      --write-out '%{http_code}' `
      --request POST `
      "$baseUrl/write?name=wp03-competing-write-blocked&timeoutMs=1000"
    $blockedCurlExit = $LASTEXITCODE
    Write-Host "WP03_COMPETING_WRITE_HTTP_STATUS=$blockedStatus"
    if (Test-Path -LiteralPath $blockedBodyPath) {
        Write-Host "WP03_COMPETING_WRITE_BODY=$(Get-Content -Raw -LiteralPath $blockedBodyPath)"
    }
    Write-Host "WP03_COMPETING_WRITE_CURL_EXIT_CODE=$blockedCurlExit"

    Write-Host '=== WAIT FOR LOCK RELEASE ==='
    $released = $false
    for ($attempt = 1; $attempt -le 24; $attempt++) {
        Start-Sleep -Seconds 1
        $status = Invoke-RestMethod -Uri "$baseUrl/lock/status?id=$lockId" -TimeoutSec 15
        Write-Host "LOCK_RELEASE_ATTEMPT=$attempt"
        $status | ConvertTo-Json -Compress
        if ($status.active -eq $false -and $status.result -eq 'released') {
            $released = $true
            break
        }
    }
    Write-Host "WP03_LOCK_RELEASED=$released"

    Write-Host '=== COMPETING WRITE AFTER RELEASE ==='
    $releasedStatus = & curl.exe --silent --show-error `
      --output $releasedBodyPath `
      --write-out '%{http_code}' `
      --request POST `
      "$baseUrl/write?name=wp03-competing-write-after-release&timeoutMs=5000"
    $releasedCurlExit = $LASTEXITCODE
    Write-Host "WP03_POST_RELEASE_WRITE_HTTP_STATUS=$releasedStatus"
    if (Test-Path -LiteralPath $releasedBodyPath) {
        Write-Host "WP03_POST_RELEASE_WRITE_BODY=$(Get-Content -Raw -LiteralPath $releasedBodyPath)"
    }
    Write-Host "WP03_POST_RELEASE_WRITE_CURL_EXIT_CODE=$releasedCurlExit"

    Write-Host '=== FINAL STATE / INTEGRITY ==='
    $final = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 60
    $final | ConvertTo-Json -Depth 10 -Compress
    Write-Host 'WP03_LOCK_FINAL_STATE_EXIT_CODE=0'
}
finally {
    Remove-Item -LiteralPath $blockedBodyPath, $releasedBodyPath -Force -ErrorAction SilentlyContinue
    Write-Host "WP03_LOCK_TEMP_OUTPUTS_PRESENT_AFTER_CLEANUP=$((Test-Path -LiteralPath $blockedBodyPath) -or (Test-Path -LiteralPath $releasedBodyPath))"
}