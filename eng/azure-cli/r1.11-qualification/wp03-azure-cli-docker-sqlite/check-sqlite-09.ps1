$ErrorActionPreference = 'Continue'

$resourceGroup = 'rg-aiq-wp03-wcus-3d25217bd701'
$appName = 'aiqwp03wcus3d25217bd701'
$baseUrl = "https://$appName.azurewebsites.net"
$r2Image = 'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r2-3f99757f018b46698e6c89d589867a03'

Write-Host "WP03_R2_IMAGE=$r2Image"

Write-Host '=== APPLY DISTINCT R2 IMAGE ==='
az webapp config container set `
  --name $appName `
  --resource-group $resourceGroup `
  --docker-custom-image-name $r2Image `
  --docker-registry-server-url https://ghcr.io
Write-Host "WP03_R2_IMAGE_CONFIG_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== SET R2 PROBE REVISION ==='
az webapp config appsettings set `
  --name $appName `
  --resource-group $resourceGroup `
  --settings PROBE_REVISION=wp03-r2
Write-Host "WP03_R2_REVISION_SETTINGS_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== RESTART AFTER REDEPLOYMENT ==='
az webapp restart --name $appName --resource-group $resourceGroup
Write-Host "WP03_R2_RESTART_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== POLL R2 HEALTH ==='
$healthy = $false
for ($attempt = 1; $attempt -le 24; $attempt++) {
    Write-Host "R2_HEALTH_POLL_ATTEMPT=$attempt"
    try {
        $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 15
        $health | ConvertTo-Json -Compress
        if ($health.revision -eq 'wp03-r2') {
            $healthy = $true
            break
        }
    } catch {
        Write-Host "R2_HEALTH_POLL_ERROR=$($_.Exception.Message)"
    }
    Start-Sleep -Seconds 5
}
Write-Host "WP03_R2_HEALTHY=$healthy"

Write-Host '=== STATE BEFORE POST-REDEPLOYMENT WRITE ==='
try {
    $before = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 60
    $before | ConvertTo-Json -Depth 10 -Compress
    Write-Host 'WP03_R2_STATE_BEFORE_EXIT_CODE=0'
} catch {
    Write-Host "WP03_R2_STATE_BEFORE_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_R2_STATE_BEFORE_EXIT_CODE=1'
}

Write-Host '=== POST-REDEPLOYMENT COMMITTED WRITE ==='
try {
    $write = Invoke-RestMethod `
      -Method Post `
      -Uri "$baseUrl/write?name=wp03-after-image-redeployment&timeoutMs=5000" `
      -TimeoutSec 60
    $write | ConvertTo-Json -Compress
    Write-Host 'WP03_R2_WRITE_EXIT_CODE=0'
} catch {
    Write-Host "WP03_R2_WRITE_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_R2_WRITE_EXIT_CODE=1'
}

Write-Host '=== FINAL R2 STATE / INTEGRITY ==='
try {
    $after = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 60
    $after | ConvertTo-Json -Depth 10 -Compress
    Write-Host 'WP03_R2_STATE_AFTER_EXIT_CODE=0'
} catch {
    Write-Host "WP03_R2_STATE_AFTER_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_R2_STATE_AFTER_EXIT_CODE=1'
}