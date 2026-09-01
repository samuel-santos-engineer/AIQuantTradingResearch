$app = 'aiqwp04wcusc1e9a49dadf6'
$baseUrl = "https://$app.azurewebsites.net"
$tempDns = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp04-dns-$([guid]::NewGuid().ToString('N')).json"
$tempAuth = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp04-auth-$([guid]::NewGuid().ToString('N')).json"

try {
    Write-Host '=== PROBE 1: DNS / TLS / HTTPS ==='
    $dnsStatus = & curl.exe --silent --show-error --output $tempDns --write-out '%{http_code}' --max-time 45 "$baseUrl/probe/dns-tls"
    $dnsExit = $LASTEXITCODE
    $dnsBody = Get-Content -Raw -LiteralPath $tempDns
    Write-Host "WP04_DNS_TLS_HTTP_STATUS=$dnsStatus"
    Write-Host "WP04_DNS_TLS_BODY=$dnsBody"
    Write-Host "WP04_DNS_TLS_CURL_EXIT_CODE=$dnsExit"
    if ($dnsExit -ne 0 -or $dnsStatus -ne '200' -or $dnsBody -notmatch '"classification":"dns_tls_success"') {
        throw 'DNS/TLS probe did not pass.'
    }

    Write-Host '=== PROBE 3: ONE AUTHENTICATED TWELVE DATA REQUEST ==='
    $authStatus = & curl.exe --silent --show-error --output $tempAuth --write-out '%{http_code}' --max-time 45 "$baseUrl/probe/provider?mode=valid"
    $authExit = $LASTEXITCODE
    $authBody = Get-Content -Raw -LiteralPath $tempAuth
    Write-Host "WP04_AUTH_PROVIDER_HTTP_STATUS=$authStatus"
    Write-Host "WP04_AUTH_PROVIDER_BODY=$authBody"
    Write-Host "WP04_AUTH_PROVIDER_CURL_EXIT_CODE=$authExit"
    if ($authExit -ne 0 -or $authStatus -ne '200' -or $authBody -notmatch '"classification":"authenticated_success"') {
        throw 'Authenticated Twelve Data connectivity did not pass.'
    }
}
finally {
    Remove-Item -LiteralPath $tempDns,$tempAuth -Force -ErrorAction SilentlyContinue
    Write-Host "WP04_PROVIDER_TEMP_OUTPUTS_PRESENT_AFTER_CLEANUP=$((Test-Path -LiteralPath $tempDns) -or (Test-Path -LiteralPath $tempAuth))"
}