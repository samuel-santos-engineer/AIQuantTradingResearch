$rg = 'rg-aiq-wp04-wcus-c1e9a49dadf6'
$app = 'aiqwp04wcusc1e9a49dadf6'
$baseUrl = "https://$app.azurewebsites.net"
$secureKey = $null
$bstr = [IntPtr]::Zero
$plainKey = $null

try {
    $secureKey = Read-Host -AsSecureString 'Paste the known-valid Twelve Data API key here only (hidden); do not share it in chat'
    $bstr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secureKey)
    $plainKey = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($bstr)
    if ([string]::IsNullOrWhiteSpace($plainKey)) { throw 'Empty API key was entered.' }

    az webapp config appsettings set --resource-group $rg --name $app `
      --settings "TWELVE_DATA_API_KEY=$plainKey" --output none
    Write-Host "WP04_RECOVERY_RETRY_SECRET_RESTORE_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Secret restoration retry failed.' }
}
finally {
    if ($bstr -ne [IntPtr]::Zero) {
        [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($bstr)
    }
    Remove-Variable plainKey -ErrorAction SilentlyContinue
    Remove-Variable secureKey -ErrorAction SilentlyContinue
}

az webapp restart --resource-group $rg --name $app
Write-Host "WP04_RECOVERY_RETRY_RESTART_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Recovery retry restart failed.' }

$ready = $false
for ($attempt = 1; $attempt -le 30; $attempt++) {
    try {
        $diagnostics = Invoke-RestMethod -Uri "$baseUrl/diagnostics" -TimeoutSec 20
        if ($diagnostics.secretPresent -eq $true) { $ready = $true; break }
    } catch { }
    Start-Sleep -Seconds 10
}
Write-Host "WP04_RECOVERY_RETRY_PROCESS_READY=$ready"
if (-not $ready) { throw 'Recovered process did not become ready.' }

$tempBody = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp04-recovery-retry-$([guid]::NewGuid().ToString('N')).json"
try {
    $status = & curl.exe --silent --show-error --output $tempBody --write-out '%{http_code}' --max-time 45 "$baseUrl/probe/provider?mode=valid"
    $curlExit = $LASTEXITCODE
    $body = Get-Content -Raw -LiteralPath $tempBody
    Write-Host "WP04_RECOVERY_RETRY_PROVIDER_HTTP_STATUS=$status"
    Write-Host "WP04_RECOVERY_RETRY_PROVIDER_BODY=$body"
    Write-Host "WP04_RECOVERY_RETRY_PROVIDER_CURL_EXIT_CODE=$curlExit"
    if ($curlExit -ne 0 -or $status -ne '200' -or $body -notmatch '"classification":"authenticated_success"') {
        throw 'WP04 recovery retry failed; do not repeat it.'
    }
}
finally {
    Remove-Item -LiteralPath $tempBody -Force -ErrorAction SilentlyContinue
    Write-Host "WP04_RECOVERY_RETRY_TEMP_OUTPUT_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempBody)"
}