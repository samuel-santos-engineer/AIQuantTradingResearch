$ErrorActionPreference = 'Continue'

$resourceGroup = 'rg-aiq-wp02-wcus-fd81a595e4e2'
$appName = 'aiqwp02wcusfd81a595e4e2'
$baseUrl = "https://$appName.azurewebsites.net"
$expectedMarker = 'wp02-wcus-marker-20260830-r1'
$image = 'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe@sha256:572d402b138de1f4f70803329a45d4cc4177969059d2789ae4b8df90a2b52f43'

Write-Host '=== REAPPLY IMMUTABLE IMAGE DIGEST ==='
az webapp config container set `
  --name $appName `
  --resource-group $resourceGroup `
  --docker-custom-image-name $image `
  --docker-registry-server-url https://ghcr.io
Write-Host "IMAGE_REDEPLOY_CONFIG_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== RESTART AFTER IMAGE REDEPLOYMENT ==='
az webapp restart --name $appName --resource-group $resourceGroup
Write-Host "IMAGE_REDEPLOY_RESTART_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== VERIFY CONFIGURED IMAGE ==='
az webapp show `
  --name $appName `
  --resource-group $resourceGroup `
  --query "siteConfig.linuxFxVersion" `
  --output tsv
Write-Host "IMAGE_CONFIG_READBACK_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== POLL HEALTH ==='
$healthy = $false
for ($i = 1; $i -le 24; $i++) {
    Write-Host "POLL_ATTEMPT=$i"
    try {
        $health = Invoke-WebRequest -Uri "$baseUrl/healthz" -UseBasicParsing -TimeoutSec 15
        Write-Host "HEALTH_STATUS_CODE=$($health.StatusCode)"
        Write-Host "HEALTH_BODY=$($health.Content)"
        if ($health.StatusCode -eq 200) {
            $healthy = $true
            break
        }
    } catch {
        Write-Host "HEALTH_ERROR=$($_.Exception.Message)"
    }
    Start-Sleep -Seconds 5
}
Write-Host "HEALTH_RECOVERED=$healthy"

Write-Host '=== READ STATE AFTER IMAGE REDEPLOYMENT ==='
try {
    $state = Invoke-WebRequest -Uri "$baseUrl/state" -UseBasicParsing -TimeoutSec 30
    Write-Host "STATE_STATUS_CODE=$($state.StatusCode)"
    Write-Host "STATE_BODY=$($state.Content)"
    Write-Host "MARKER_MATCH=$($state.Content -match [regex]::Escape($expectedMarker))"
} catch {
    Write-Host "STATE_ERROR=$($_.Exception.Message)"
}
Write-Host "STATE_READ_EXIT_CODE=$LASTEXITCODE"