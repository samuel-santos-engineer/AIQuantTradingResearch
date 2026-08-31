$rg = 'rg-aiq-wp04-wcus-c1e9a49dadf6'
$plan = 'asp-aiq-wp04-wcus-c1e9a49dadf6'
$app = 'aiqwp04wcusc1e9a49dadf6'
$image = 'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp04-r1-285950909c3b43b8be1b643f73b28b11'
$baseUrl = "https://$app.azurewebsites.net"
$secureKey = $null
$bstr = [IntPtr]::Zero
$plainKey = $null

Write-Host '=== CONFIRM OWNED PLAN BEFORE CONTINUATION ==='
az appservice plan show --resource-group $rg --name $plan `
  --query '{name:name,sku:sku.name,tier:sku.tier,kind:kind}' --output json
Write-Host "WP04_RECOVERY_PLAN_READ_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Expected owned F1 plan is absent.' }

try {
    Write-Host '=== CREATE CUSTOM-CONTAINER WEB APP ==='
    az webapp create --resource-group $rg --plan $plan --name $app `
      --container-image-name $image --output none
    Write-Host "WP04_RECOVERY_WEBAPP_CREATE_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'WP04 custom-container Web App creation failed.' }

    $secureKey = Read-Host -AsSecureString 'Paste the Twelve Data API key here only (hidden); do not share it in chat'
    $bstr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secureKey)
    $plainKey = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($bstr)
    if ([string]::IsNullOrWhiteSpace($plainKey)) { throw 'Empty API key was entered.' }

    Write-Host '=== INJECT SETTINGS WITHOUT PRINTING VALUES ==='
    az webapp config appsettings set --resource-group $rg --name $app `
      --settings `
        'WEBSITES_ENABLE_APP_SERVICE_STORAGE=true' `
        'WEBSITES_PORT=8080' `
        'PROBE_REVISION=wp04-r1' `
        "TWELVE_DATA_API_KEY=$plainKey" `
      --output none
    Write-Host "WP04_RECOVERY_SECRET_SETTINGS_INJECTION_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'WP04 secret-setting injection failed.' }

    Write-Host '=== ENFORCE HTTPS ONLY ==='
    az webapp update --resource-group $rg --name $app --https-only true --output none
    Write-Host "WP04_RECOVERY_HTTPS_ONLY_CONFIG_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'WP04 HTTPS-only configuration failed.' }
}
finally {
    if ($bstr -ne [IntPtr]::Zero) {
        [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($bstr)
    }
    Remove-Variable plainKey -ErrorAction SilentlyContinue
    Remove-Variable secureKey -ErrorAction SilentlyContinue
}

Write-Host '=== POLL PUBLIC HEALTH ==='
$healthy = $false
for ($attempt = 1; $attempt -le 30; $attempt++) {
    Write-Host "WP04_RECOVERY_HEALTH_POLL_ATTEMPT=$attempt"
    try {
        $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 20
        $health | ConvertTo-Json -Compress
        if ($health.status -eq 'ok' -and $health.revision -eq 'wp04-r1') {
            $healthy = $true
            break
        }
    } catch {
        Write-Host "WP04_RECOVERY_HEALTH_POLL_ERROR=$($_.Exception.Message)"
    }
    Start-Sleep -Seconds 10
}
Write-Host "WP04_RECOVERY_HEALTHY=$healthy"
if (-not $healthy) { throw 'WP04 probe did not become healthy.' }

Write-Host '=== SECRET-SAFE DIAGNOSTICS ==='
$diagnostics = Invoke-RestMethod -Uri "$baseUrl/diagnostics" -TimeoutSec 30
$diagnostics | ConvertTo-Json -Compress
Write-Host 'WP04_RECOVERY_DIAGNOSTICS_EXIT_CODE=0'