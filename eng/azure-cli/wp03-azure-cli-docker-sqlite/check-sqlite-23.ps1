$app = 'aiqwp03wcus3d25217bd701'
$baseUrl = "https://$app.azurewebsites.net"
$lockId = 'wp03-selected-delete-lock-r1'
$tempBody = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp03-lock-$([guid]::NewGuid().ToString('N')).json"

try {
    Write-Host '=== START SELECTED DELETE HELD WRITE ==='
    $hold = Invoke-RestMethod -Method Post -Uri "$baseUrl/lock/hold?id=$lockId&seconds=10" -TimeoutSec 30
    $hold | ConvertTo-Json -Compress
    Write-Host 'WP03_SELECTED_DELETE_LOCK_HOLD_EXIT_CODE=0'

    Write-Host '=== WAIT FOR LOCK ACTIVE ==='
    $active = $false
    for ($attempt = 1; $attempt -le 10; $attempt++) {
        $status = Invoke-RestMethod -Uri "$baseUrl/lock/status?id=$lockId" -TimeoutSec 30
        Write-Host "WP03_SELECTED_DELETE_LOCK_STATUS_ATTEMPT=$attempt"
        $status | ConvertTo-Json -Compress
        if ($status.active -eq $true) { $active = $true; break }
        Start-Sleep -Seconds 1
    }
    Write-Host "WP03_SELECTED_DELETE_LOCK_ACTIVE=$active"
    if (-not $active) { throw 'DELETE lock never became active.' }

    Write-Host '=== READER WHILE LOCKED ==='
    $reader = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 30
    $reader | ConvertTo-Json -Depth 8 -Compress
    $readerOk = $reader.journalMode -eq 'delete' -and $reader.integrityCheck -eq 'ok' -and $reader.quickCheck -eq 'ok'
    Write-Host "WP03_SELECTED_DELETE_LOCKED_READER_VALID=$readerOk"
    Write-Host 'WP03_SELECTED_DELETE_LOCKED_READER_EXIT_CODE=0'
    if (-not $readerOk) { throw 'DELETE-mode reader proof failed.' }

    Write-Host '=== COMPETING WRITE: 1000ms BUSY TIMEOUT ==='
    $httpCode = & curl.exe --silent --show-error --output $tempBody --write-out '%{http_code}' `
      --request POST "$baseUrl/write?name=wp03-selected-delete-competing-blocked&timeoutMs=1000"
    $curlExit = $LASTEXITCODE
    $body = if (Test-Path -LiteralPath $tempBody) { Get-Content -Raw -LiteralPath $tempBody } else { '' }
    Write-Host "WP03_SELECTED_DELETE_COMPETING_WRITE_HTTP_STATUS=$httpCode"
    Write-Host "WP03_SELECTED_DELETE_COMPETING_WRITE_BODY=$body"
    Write-Host "WP03_SELECTED_DELETE_COMPETING_WRITE_CURL_EXIT_CODE=$curlExit"
    if ($curlExit -ne 0 -or $httpCode -ne '409' -or $body -notmatch '"result":"blocked"') {
        throw 'Competing DELETE-mode write was not deterministically blocked.'
    }

    Write-Host '=== WAIT FOR LOCK RELEASE ==='
    $released = $false
    for ($attempt = 1; $attempt -le 15; $attempt++) {
        Start-Sleep -Seconds 1
        $status = Invoke-RestMethod -Uri "$baseUrl/lock/status?id=$lockId" -TimeoutSec 30
        Write-Host "WP03_SELECTED_DELETE_LOCK_RELEASE_ATTEMPT=$attempt"
        $status | ConvertTo-Json -Compress
        if ($status.active -eq $false -and $status.result -eq 'released') { $released = $true; break }
    }
    Write-Host "WP03_SELECTED_DELETE_LOCK_RELEASED=$released"
    if (-not $released) { throw 'DELETE lock did not release normally.' }

    Write-Host '=== WRITE AFTER RELEASE ==='
    $post = Invoke-RestMethod -Method Post -Uri "$baseUrl/write?name=wp03-selected-delete-post-lock-commit" -TimeoutSec 30
    $post | ConvertTo-Json -Compress
    $postOk = $post.result -eq 'committed'
    Write-Host "WP03_SELECTED_DELETE_POST_LOCK_WRITE_VALID=$postOk"
    Write-Host 'WP03_SELECTED_DELETE_POST_LOCK_WRITE_EXIT_CODE=0'
    if (-not $postOk) { throw 'Post-lock write did not commit.' }

    Write-Host '=== FINAL SELECTED DELETE STATE ==='
    $final = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 30
    $final | ConvertTo-Json -Depth 8 -Compress
    $names = @($final.rows | ForEach-Object name)
    $finalOk = $final.journalMode -eq 'delete' -and
               $final.integrityCheck -eq 'ok' -and
               $final.quickCheck -eq 'ok' -and
               $names -contains 'wp03-selected-delete-post-lock-commit' -and
               -not ($names -contains 'wp03-selected-delete-competing-blocked')
    Write-Host "WP03_SELECTED_DELETE_FINAL_VALID=$finalOk"
    Write-Host 'WP03_SELECTED_DELETE_FINAL_STATE_EXIT_CODE=0'
    if (-not $finalOk) { throw 'Final selected DELETE state failed validation.' }
}
finally {
    Remove-Item -LiteralPath $tempBody -Force -ErrorAction SilentlyContinue
    Write-Host "WP03_SELECTED_DELETE_TEMP_OUTPUT_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempBody)"
}