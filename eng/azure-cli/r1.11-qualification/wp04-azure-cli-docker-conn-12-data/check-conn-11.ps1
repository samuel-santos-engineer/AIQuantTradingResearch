$app = 'aiqwp04wcusc1e9a49dadf6'
$baseUrl = "https://$app.azurewebsites.net"
$tempBody = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp04-network-$([guid]::NewGuid().ToString('N')).json"

try {
    Write-Host '=== CONTROLLED SYNTHETIC NETWORK FAILURE ==='
    $status = & curl.exe --silent --show-error --output $tempBody --write-out '%{http_code}' --max-time 45 "$baseUrl/probe/provider?mode=network"
    $curlExit = $LASTEXITCODE
    $body = Get-Content -Raw -LiteralPath $tempBody
    Write-Host "WP04_NETWORK_FAILURE_HTTP_STATUS=$status"
    Write-Host "WP04_NETWORK_FAILURE_BODY=$body"
    Write-Host "WP04_NETWORK_FAILURE_CURL_EXIT_CODE=$curlExit"
    if ($curlExit -ne 0 -or $status -ne '504' -or $body -notmatch '"classification":"synthetic_network_failure"' -or $body -notmatch '"providerRequestMade":false') {
        throw 'Controlled network-failure isolation did not pass.'
    }

    Write-Host '=== HOST HEALTH AFTER NETWORK FAILURE ==='
    $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 30
    $health | ConvertTo-Json -Compress
    $healthy = $health.status -eq 'ok'
    Write-Host "WP04_NETWORK_FAILURE_HOST_HEALTHY=$healthy"
    if (-not $healthy) { throw 'Host did not remain healthy.' }
}
finally {
    Remove-Item -LiteralPath $tempBody -Force -ErrorAction SilentlyContinue
    Write-Host "WP04_NETWORK_FAILURE_TEMP_OUTPUT_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempBody)"
}