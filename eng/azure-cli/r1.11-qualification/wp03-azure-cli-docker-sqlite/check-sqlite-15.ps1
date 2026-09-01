$ErrorActionPreference = 'Continue'

$resourceGroup = 'rg-aiq-wp03-wcus-3d25217bd701'
$appName = 'aiqwp03wcus3d25217bd701'
$baseUrl = "https://$appName.azurewebsites.net"
$r3Image = 'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r3-fc006171dbf241b78775e7dd6d5e8a73'

Write-Host "WP03_R3_IMAGE=$r3Image"

Write-Host '=== APPLY R3 IMAGE ==='
az webapp config container set `
  --name $appName `
  --resource-group $resourceGroup `
  --docker-custom-image-name $r3Image `
  --docker-registry-server-url https://ghcr.io
Write-Host "WP03_R3_IMAGE_CONFIG_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== SET R3 PROBE REVISION ==='
az webapp config appsettings set `
  --name $appName `
  --resource-group $resourceGroup `
  --settings PROBE_REVISION=wp03-r3
Write-Host "WP03_R3_REVISION_SETTINGS_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== RESTART R3 ==='
az webapp restart --name $appName --resource-group $resourceGroup
Write-Host "WP03_R3_RESTART_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== POLL R3 HEALTH ==='
$healthy = $false
for ($attempt = 1; $attempt -le 24; $attempt++) {
    Write-Host "R3_HEALTH_POLL_ATTEMPT=$attempt"
    try {
        $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 15
        $health | ConvertTo-Json -Compress
        if ($health.revision -eq 'wp03-r3') {
            $healthy = $true
            break
        }
    } catch {
        Write-Host "R3_HEALTH_POLL_ERROR=$($_.Exception.Message)"
    }
    Start-Sleep -Seconds 5
}
Write-Host "WP03_R3_HEALTHY=$healthy"

Write-Host '=== R3 STATE / PERSISTED ROWS ==='
try {
    $state = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 60
    $state | ConvertTo-Json -Depth 10 -Compress
    Write-Host 'WP03_R3_STATE_EXIT_CODE=0'
} catch {
    Write-Host "WP03_R3_STATE_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_R3_STATE_EXIT_CODE=1'
}