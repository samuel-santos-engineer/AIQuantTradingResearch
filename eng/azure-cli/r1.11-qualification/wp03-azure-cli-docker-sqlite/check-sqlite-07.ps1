$ErrorActionPreference = 'Continue'

$resourceGroup = 'rg-aiq-wp03-wcus-3d25217bd701'
$appName = 'aiqwp03wcus3d25217bd701'
$baseUrl = "https://$appName.azurewebsites.net"

Write-Host '=== STOP WEB APP ==='
az webapp stop --name $appName --resource-group $resourceGroup
Write-Host "WP03_APP_STOP_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== START WEB APP ==='
az webapp start --name $appName --resource-group $resourceGroup
Write-Host "WP03_APP_START_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== POLL HTTPS RECOVERY ==='
$healthy = $false
for ($attempt = 1; $attempt -le 24; $attempt++) {
    Write-Host "RECYCLE_HEALTH_POLL_ATTEMPT=$attempt"
    try {
        $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 15
        $health | ConvertTo-Json -Compress
        $healthy = $true
        break
    } catch {
        Write-Host "RECYCLE_HEALTH_POLL_ERROR=$($_.Exception.Message)"
        Start-Sleep -Seconds 5
    }
}
Write-Host "WP03_RECYCLE_HEALTHY=$healthy"

Write-Host '=== STATE BEFORE POST-RECYCLE WRITE ==='
try {
    $before = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 60
    $before | ConvertTo-Json -Depth 10 -Compress
    Write-Host 'WP03_RECYCLE_STATE_BEFORE_EXIT_CODE=0'
} catch {
    Write-Host "WP03_RECYCLE_STATE_BEFORE_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_RECYCLE_STATE_BEFORE_EXIT_CODE=1'
}

Write-Host '=== POST-RECYCLE COMMITTED WRITE ==='
try {
    $write = Invoke-RestMethod `
      -Method Post `
      -Uri "$baseUrl/write?name=wp03-after-container-recycle&timeoutMs=5000" `
      -TimeoutSec 60
    $write | ConvertTo-Json -Compress
    Write-Host 'WP03_RECYCLE_WRITE_EXIT_CODE=0'
} catch {
    Write-Host "WP03_RECYCLE_WRITE_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_RECYCLE_WRITE_EXIT_CODE=1'
}

Write-Host '=== FINAL STATE / INTEGRITY ==='
try {
    $after = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 60
    $after | ConvertTo-Json -Depth 10 -Compress
    Write-Host 'WP03_RECYCLE_STATE_AFTER_EXIT_CODE=0'
} catch {
    Write-Host "WP03_RECYCLE_STATE_AFTER_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_RECYCLE_STATE_AFTER_EXIT_CODE=1'
}