$rg = 'rg-aiq-wp04-wcus-c1e9a49dadf6'
$app = 'aiqwp04wcusc1e9a49dadf6'
$baseUrl = "https://$app.azurewebsites.net"
$secureKey = $null
$bstr = [IntPtr]::Zero
$plainKey = $null

try {
    $secureKey = Read-Host -AsSecureString 'Paste the Twelve Data API key here only (hidden); do not share it in chat'
    $bstr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secureKey)
    $plainKey = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($bstr)
    if ([string]::IsNullOrWhiteSpace($plainKey)) { throw 'Empty API key was entered.' }

    Write-Host '=== RESTORE REAL SECRET WITHOUT PRINTING IT ==='
    az webapp config appsettings set --resource-group $rg --name $app `
      --settings "TWELVE_DATA_API_KEY=$plainKey" --output none
    Write-Host "WP04_RECOVERY_SECRET_RESTORE_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Real secret restoration failed.' }
}
finally {
    if ($bstr -ne [IntPtr]::Zero) {
        [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($bstr)
    }
    Remove-Variable plainKey -ErrorAction SilentlyContinue
    Remove-Variable secureKey -ErrorAction SilentlyContinue
}

Write-Host '=== RESTART TO APPLY RESTORED SECRET ==='
az webapp restart --resource-group $rg --name $app
Write-Host "WP04_RECOVERY_RESTART_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Recovery restart failed.' }

Write-Host '=== POLL HEALTH / SECRET-PRESENCE BOOLEAN ==='
$ready = $false
for ($attempt = 1; $attempt -le 30; $attempt++) {
    Write-Host "WP04_RECOVERY_HEALTH_POLL_ATTEMPT=$attempt"
    try {
        $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 20
        $diagnostics = Invoke-RestMethod -Uri "$baseUrl/diagnostics" -TimeoutSec 20
        if ($health.status -eq 'ok' -and $diagnostics.secretPresent -eq $true) {
            $ready = $true
            break
        }
    } catch {
        Write-Host "WP04_RECOVERY_HEALTH_POLL_ERROR=$($_.Exception.Message)"
    }
    Start-Sleep -Seconds 10
}
Write-Host "WP04_RECOVERY_PROCESS_READY=$ready"
if (-not $ready) { throw 'Recovered process did not become ready.' }

Write-Host '=== ONE AUTHENTICATED RECOVERY REQUEST ==='
$tempBody = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp04-recovery-$([guid]::NewGuid().ToString('N')).json"
try {
    $status = & curl.exe --silent --show-error --output $tempBody --write-out '%{http_code}' --max-time 45 "$baseUrl/probe/provider?mode=valid"
    $curlExit = $LASTEXITCODE
    $body = Get-Content -Raw -LiteralPath $tempBody
    Write-Host "WP04_RECOVERY_PROVIDER_HTTP_STATUS=$status"
    Write-Host "WP04_RECOVERY_PROVIDER_BODY=$body"
    Write-Host "WP04_RECOVERY_PROVIDER_CURL_EXIT_CODE=$curlExit"
    if ($curlExit -ne 0 -or $status -ne '200' -or $body -notmatch '"classification":"authenticated_success"' -or $body -notmatch '"providerRequestMade":true') {
        throw 'Provider recovery did not pass.'
    }
}
finally {
    Remove-Item -LiteralPath $tempBody -Force -ErrorAction SilentlyContinue
    Write-Host "WP04_RECOVERY_TEMP_OUTPUT_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempBody)"
}