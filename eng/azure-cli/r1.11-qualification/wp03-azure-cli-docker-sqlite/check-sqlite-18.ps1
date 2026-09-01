$rg = 'rg-aiq-wp03-wcus-3d25217bd701'
$app = 'aiqwp03wcus3d25217bd701'
$image = 'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r4-e361ba0a4ab546f3bdcf69f033dc419f'
$baseUrl = "https://$app.azurewebsites.net"

Write-Host '=== APPLY R4 IMAGE ==='
az webapp config container set `
  --resource-group $rg `
  --name $app `
  --container-image-name $image `
  --container-registry-url 'https://ghcr.io' `
  --output none
Write-Host "WP03_R4_IMAGE_CONFIG_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'R4 image configuration failed.' }

Write-Host '=== SET R4 REVISION ==='
az webapp config appsettings set `
  --resource-group $rg `
  --name $app `
  --settings 'PROBE_REVISION=wp03-r4' `
  --output none
Write-Host "WP03_R4_REVISION_SETTINGS_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'R4 revision setting failed.' }

Write-Host '=== RESTART R4 ==='
az webapp restart --resource-group $rg --name $app
Write-Host "WP03_R4_RESTART_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'R4 restart failed.' }

Write-Host '=== POLL R4 HEALTH ==='
$healthy = $false
for ($attempt = 1; $attempt -le 30; $attempt++) {
    Write-Host "R4_HEALTH_POLL_ATTEMPT=$attempt"
    try {
        $health = Invoke-RestMethod -Uri "$baseUrl/health" -TimeoutSec 20
        $health | ConvertTo-Json -Compress
        if ($health.status -eq 'ok' -and $health.revision -eq 'wp03-r4') {
            $healthy = $true
            break
        }
    } catch {
        Write-Host "R4_HEALTH_POLL_ERROR=$($_.Exception.Message)"
    }
    Start-Sleep -Seconds 10
}
Write-Host "WP03_R4_HEALTHY=$healthy"
if (-not $healthy) { throw 'R4 did not become healthy.' }

Write-Host '=== VERIFY PERSISTED SQLITE STATE ==='
try {
    $state = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 30
    $state | ConvertTo-Json -Depth 8 -Compress
    $valid = $state.journalMode -eq 'wal' -and
             $state.integrityCheck -eq 'ok' -and
             $state.quickCheck -eq 'ok' -and
             $state.rows.Count -ge 9
    Write-Host "WP03_R4_PERSISTED_STATE_VALID=$valid"
    if (-not $valid) { throw 'R4 did not preserve the expected WAL SQLite state.' }
    Write-Host 'WP03_R4_STATE_EXIT_CODE=0'
} catch {
    Write-Host "WP03_R4_STATE_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_R4_STATE_EXIT_CODE=1'
    throw
}

Write-Host '=== VERIFY ACTIVE R4 CONFIGURATION ==='
az webapp config container show `
  --resource-group $rg `
  --name $app `
  --query 'linuxFxVersion' `
  --output tsv
Write-Host "WP03_R4_CONFIG_READBACK_EXIT_CODE=$LASTEXITCODE"