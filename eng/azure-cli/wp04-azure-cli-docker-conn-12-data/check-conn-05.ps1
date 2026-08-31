$stamp = [guid]::NewGuid().ToString('N').Substring(0, 12)
$rg = "rg-aiq-wp04-wcus-$stamp"
$plan = "asp-aiq-wp04-wcus-$stamp"
$app = "aiqwp04wcus$stamp"
$image = 'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp04-r1-285950909c3b43b8be1b643f73b28b11'
$location = 'westcentralus'
$baseUrl = "https://$app.azurewebsites.net"
$secureKey = $null
$bstr = [IntPtr]::Zero
$plainKey = $null

try {
    Write-Host "WP04_RESOURCE_GROUP=$rg"
    Write-Host "WP04_PLAN_NAME=$plan"
    Write-Host "WP04_WEB_APP_NAME=$app"
    Write-Host "WP04_BASE_URL=$baseUrl"

    Write-Host '=== CREATE TAGGED WP04 RESOURCE GROUP ==='
    az group create --name $rg --location $location `
      --tags initiative=INIT-1.11 wp=WP04 owner=Terra recurringCost=0 `
      --output none
    Write-Host "WP04_RESOURCE_GROUP_CREATE_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'WP04 resource-group creation failed.' }

    Write-Host '=== CREATE LINUX F1 PLAN ==='
    az appservice plan create --resource-group $rg --name $plan --location $location --is-linux --sku F1 --output none
    Write-Host "WP04_LINUX_F1_PLAN_CREATE_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'WP04 Linux F1 plan creation failed.' }

    Write-Host '=== CREATE WEB APP ==='
    az webapp create --resource-group $rg --plan $plan --name $app --output none
    Write-Host "WP04_WEBAPP_CREATE_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'WP04 Web App creation failed.' }

    Write-Host '=== CONFIGURE PUBLIC GHCR CONTAINER ==='
    az webapp config container set --resource-group $rg --name $app `
      --container-image-name $image --container-registry-url 'https://ghcr.io' --output none
    Write-Host "WP04_CONTAINER_CONFIG_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'WP04 container configuration failed.' }

    $secureKey = Read-Host -AsSecureString 'Paste the Twelve Data API key here only (hidden); do not share it in chat'
    $bstr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secureKey)
    $plainKey = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($bstr)
    if ([string]::IsNullOrWhiteSpace($plainKey)) { throw 'Empty API key was entered.' }

    Write-Host '=== INJECT APP SETTINGS WITHOUT PRINTING VALUES ==='
    az webapp config appsettings set --resource-group $rg --name $app `
      --settings `
        'WEBSITES_ENABLE_APP_SERVICE_STORAGE=true' `
        'WEBSITES_PORT=8080' `
        'PROBE_REVISION=wp04-r1' `
        "TWELVE_DATA_API_KEY=$plainKey" `
      --output none
    Write-Host "WP04_SECRET_SETTINGS_INJECTION_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'WP04 secret-setting injection failed.' }

    Write-Host '=== ENFORCE HTTPS ONLY ==='
    az webapp update --resource-group $rg --name $app --https-only true --output none
    Write-Host "WP04_HTTPS_ONLY_CONFIG_EXIT_CODE=$LASTEXITCODE"
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
    Write-Host "WP04_HEALTH_POLL_ATTEMPT=$attempt"
    try {
        $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 20
        $health | ConvertTo-Json -Compress
        if ($health.status -eq 'ok' -and $health.revision -eq 'wp04-r1') {
            $healthy = $true
            break
        }
    } catch {
        Write-Host "WP04_HEALTH_POLL_ERROR=$($_.Exception.Message)"
    }
    Start-Sleep -Seconds 10
}
Write-Host "WP04_HEALTHY=$healthy"
if (-not $healthy) { throw 'WP04 probe did not become healthy.' }

Write-Host '=== SECRET-SAFE DIAGNOSTICS ==='
$diagnostics = Invoke-RestMethod -Uri "$baseUrl/diagnostics" -TimeoutSec 30
$diagnostics | ConvertTo-Json -Compress
Write-Host 'WP04_DIAGNOSTICS_EXIT_CODE=0'

Write-Host '=== SANITIZED RUNTIME CONFIGURATION ==='
az webapp show --resource-group $rg --name $app `
  --query '{state:state,httpsOnly:httpsOnly,kind:kind,location:location,image:siteConfig.linuxFxVersion}' `
  --output json
Write-Host "WP04_RUNTIME_READBACK_EXIT_CODE=$LASTEXITCODE"

$settings = @(az webapp config appsettings list --resource-group $rg --name $app --output json | ConvertFrom-Json)
$secretPresent = @($settings | Where-Object { $_.name -eq 'TWELVE_DATA_API_KEY' }).Count -eq 1
$storagePresent = @($settings | Where-Object { $_.name -eq 'WEBSITES_ENABLE_APP_SERVICE_STORAGE' -and $_.value -eq 'true' }).Count -eq 1
$portPresent = @($settings | Where-Object { $_.name -eq 'WEBSITES_PORT' -and $_.value -eq '8080' }).Count -eq 1
Write-Host "WP04_SECRET_SETTING_NAME_PRESENT=$secretPresent"
Write-Host "WP04_STORAGE_SETTING_VALID=$storagePresent"
Write-Host "WP04_PORT_SETTING_VALID=$portPresent"
Write-Host 'WP04_SETTINGS_SANITIZED_READBACK_EXIT_CODE=0'