$rg = 'rg-aiq-wp04-wcus-c1e9a49dadf6'
$app = 'aiqwp04wcusc1e9a49dadf6'
$baseUrl = "https://$app.azurewebsites.net"

Write-Host '=== INJECT SYNTHETIC INVALID KEY ONLY ==='
az webapp config appsettings set --resource-group $rg --name $app `
  --settings 'TWELVE_DATA_API_KEY=invalid-wp04-probe-key' --output none
Write-Host "WP04_INVALID_SECRET_SETTING_INJECTION_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Synthetic invalid-key setting injection failed.' }

Write-Host '=== RESTART TO APPLY SYNTHETIC INVALID KEY ==='
az webapp restart --resource-group $rg --name $app
Write-Host "WP04_INVALID_SECRET_RESTART_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Restart after synthetic invalid-key injection failed.' }

Write-Host '=== POLL HEALTH / PRESENCE BOOLEAN ==='
$ready = $false
for ($attempt = 1; $attempt -le 30; $attempt++) {
    Write-Host "WP04_INVALID_SECRET_HEALTH_POLL_ATTEMPT=$attempt"
    try {
        $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 20
        $diagnostics = Invoke-RestMethod -Uri "$baseUrl/diagnostics" -TimeoutSec 20
        if ($health.status -eq 'ok' -and $diagnostics.secretPresent -eq $true) {
            $ready = $true
            break
        }
    } catch {
        Write-Host "WP04_INVALID_SECRET_HEALTH_POLL_ERROR=$($_.Exception.Message)"
    }
    Start-Sleep -Seconds 10
}
Write-Host "WP04_INVALID_SECRET_PROCESS_READY=$ready"
if (-not $ready) { throw 'Synthetic invalid-key process did not become ready.' }

Write-Host '=== ONE SYNTHETIC INVALID-KEY REQUEST ==='
$tempBody = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp04-invalid-$([guid]::NewGuid().ToString('N')).json"
try {
    $status = & curl.exe --silent --show-error --output $tempBody --write-out '%{http_code}' --max-time 45 "$baseUrl/probe/provider?mode=valid"
    $curlExit = $LASTEXITCODE
    $body = Get-Content -Raw -LiteralPath $tempBody
    Write-Host "WP04_INVALID_SECRET_HTTP_STATUS=$status"
    Write-Host "WP04_INVALID_SECRET_BODY=$body"
    Write-Host "WP04_INVALID_SECRET_CURL_EXIT_CODE=$curlExit"
    if ($curlExit -ne 0 -or $status -ne '502' -or $body -notmatch '"classification":"invalid_secret_or_provider_failure"' -or $body -notmatch '"providerRequestMade":true') {
        throw 'Invalid-secret isolation did not produce the expected bounded failure.'
    }
}
finally {
    Remove-Item -LiteralPath $tempBody -Force -ErrorAction SilentlyContinue
    Write-Host "WP04_INVALID_SECRET_TEMP_OUTPUT_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempBody)"
}