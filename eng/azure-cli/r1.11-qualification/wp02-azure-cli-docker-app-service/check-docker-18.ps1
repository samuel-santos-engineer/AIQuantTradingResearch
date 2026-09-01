$ErrorActionPreference = 'Continue'

$resourceGroup = 'rg-aiq-wp02-wcus-fd81a595e4e2'
$appName = 'aiqwp02wcusfd81a595e4e2'
$baseUrl = "https://$appName.azurewebsites.net"

Write-Host "WP02_BASE_URL=$baseUrl"

Write-Host '=== WEB APP RUNTIME READ-BACK ==='
az webapp show `
  --name $appName `
  --resource-group $resourceGroup `
  --query "{state:state,availabilityState:availabilityState,httpsOnly:httpsOnly,publicNetworkAccess:publicNetworkAccess,linuxFxVersion:siteConfig.linuxFxVersion}" `
  --output json
Write-Host "WEBAPP_RUNTIME_READBACK_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== HEALTH ENDPOINT ==='
try {
    $health = Invoke-WebRequest -Uri "$baseUrl/healthz" -UseBasicParsing -TimeoutSec 120
    Write-Host "HEALTH_STATUS_CODE=$($health.StatusCode)"
    Write-Host "HEALTH_BODY=$($health.Content)"
} catch {
    Write-Host "HEALTH_ERROR=$($_.Exception.Message)"
}
Write-Host "HEALTH_REQUEST_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== INITIAL STATE ==='
try {
    $state = Invoke-WebRequest -Uri "$baseUrl/state" -UseBasicParsing -TimeoutSec 120
    Write-Host "STATE_STATUS_CODE=$($state.StatusCode)"
    Write-Host "STATE_BODY=$($state.Content)"
} catch {
    Write-Host "STATE_ERROR=$($_.Exception.Message)"
}
Write-Host "STATE_REQUEST_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== INITIAL RESOURCE INVENTORY ==='
az resource list `
  --resource-group $resourceGroup `
  --query "[].{type:type,name:name,location:location,sku:sku.name,kind:kind}" `
  --output json
Write-Host "TARGET_RESOURCE_INVENTORY_EXIT_CODE=$LASTEXITCODE"