$ErrorActionPreference = 'Continue'

$resourceGroup = 'rg-aiq-wp02-wcus-fd81a595e4e2'
$appName = 'aiqwp02wcusfd81a595e4e2'
$baseUrl = "https://$appName.azurewebsites.net"
$expectedMarker = 'wp02-wcus-marker-20260830-r1'

Write-Host '=== STOP TARGET WEB APP ==='
az webapp stop --name $appName --resource-group $resourceGroup
Write-Host "WEBAPP_STOP_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== START TARGET WEB APP ==='
az webapp start --name $appName --resource-group $resourceGroup
Write-Host "WEBAPP_START_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== POLL HEALTH AFTER CONTAINER RECYCLE ==='
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

Write-Host '=== READ STATE AFTER CONTAINER RECYCLE ==='
try {
    $state = Invoke-WebRequest -Uri "$baseUrl/state" -UseBasicParsing -TimeoutSec 30
    Write-Host "STATE_STATUS_CODE=$($state.StatusCode)"
    Write-Host "STATE_BODY=$($state.Content)"
    Write-Host "MARKER_MATCH=$($state.Content -match [regex]::Escape($expectedMarker))"
} catch {
    Write-Host "STATE_ERROR=$($_.Exception.Message)"
}
Write-Host "STATE_READ_EXIT_CODE=$LASTEXITCODE"