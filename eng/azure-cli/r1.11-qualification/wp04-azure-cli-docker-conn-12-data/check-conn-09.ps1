$rg = 'rg-aiq-wp04-wcus-c1e9a49dadf6'
$app = 'aiqwp04wcusc1e9a49dadf6'
$baseUrl = "https://$app.azurewebsites.net"

Write-Host '=== RESTART TO APPLY SECRET REMOVAL ==='
az webapp restart --resource-group $rg --name $app
Write-Host "WP04_MISSING_SECRET_RESTART_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Restart after secret removal failed.' }

Write-Host '=== POLL FOR SECRET-ABSENT PROCESS ==='
$absent = $false
for ($attempt = 1; $attempt -le 30; $attempt++) {
    Write-Host "WP04_MISSING_SECRET_ABSENCE_POLL_ATTEMPT=$attempt"
    try {
        $diagnostics = Invoke-RestMethod -Uri "$baseUrl/diagnostics" -TimeoutSec 20
        $diagnostics | ConvertTo-Json -Compress
        if ($diagnostics.secretPresent -eq $false) {
            $absent = $true
            break
        }
    } catch {
        Write-Host "WP04_MISSING_SECRET_ABSENCE_POLL_ERROR=$($_.Exception.Message)"
    }
    Start-Sleep -Seconds 10
}
Write-Host "WP04_MISSING_SECRET_PROCESS_ABSENT=$absent"
if (-not $absent) { throw 'Secret remained in the restarted process.' }

Write-Host '=== EXERCISE MISSING-SECRET PATH ==='
$tempBody = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp04-missing-$([guid]::NewGuid().ToString('N')).json"
try {
    $status = & curl.exe --silent --show-error --output $tempBody --write-out '%{http_code}' --max-time 30 "$baseUrl/probe/provider?mode=valid"
    $curlExit = $LASTEXITCODE
    $body = Get-Content -Raw -LiteralPath $tempBody
    Write-Host "WP04_MISSING_SECRET_HTTP_STATUS=$status"
    Write-Host "WP04_MISSING_SECRET_BODY=$body"
    Write-Host "WP04_MISSING_SECRET_CURL_EXIT_CODE=$curlExit"
    if ($curlExit -ne 0 -or $status -ne '424' -or $body -notmatch '"classification":"missing_secret"' -or $body -notmatch '"providerRequestMade":false') {
        throw 'Missing-secret path did not fail closed.'
    }
}
finally {
    Remove-Item -LiteralPath $tempBody -Force -ErrorAction SilentlyContinue
    Write-Host "WP04_MISSING_SECRET_TEMP_OUTPUT_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempBody)"
}