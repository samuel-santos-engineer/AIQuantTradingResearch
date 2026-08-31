$ErrorActionPreference = 'Continue'

$appName = 'aiqwp03wcus3d25217bd701'
$baseUrl = "https://$appName.azurewebsites.net"

Write-Host "WP03_BASE_URL=$baseUrl"

Write-Host '=== POLL HTTPS HEALTH ==='
$healthy = $false
for ($attempt = 1; $attempt -le 24; $attempt++) {
    Write-Host "HEALTH_POLL_ATTEMPT=$attempt"
    try {
        $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 15
        $health | ConvertTo-Json -Compress
        $healthy = $true
        break
    } catch {
        Write-Host "HEALTH_POLL_ERROR=$($_.Exception.Message)"
        Start-Sleep -Seconds 5
    }
}
Write-Host "WP03_HEALTHY=$healthy"

if (-not $healthy) {
    Write-Host 'WP03_BASELINE_EXIT_CODE=NOT_RUN'
    exit 1
}

Write-Host '=== INITIALIZE DELETE JOURNAL MODE ==='
try {
    $baseline = Invoke-RestMethod `
      -Method Post `
      -Uri "$baseUrl/baseline?mode=DELETE&name=wp03-azure-baseline" `
      -TimeoutSec 60
    $baseline | ConvertTo-Json -Compress
    Write-Host 'WP03_BASELINE_EXIT_CODE=0'
} catch {
    Write-Host "WP03_BASELINE_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_BASELINE_EXIT_CODE=1'
}

Write-Host '=== COMMITTED TRANSACTION ==='
try {
    $commit = Invoke-RestMethod `
      -Method Post `
      -Uri "$baseUrl/transaction?name=wp03-azure-commit&commit=true" `
      -TimeoutSec 60
    $commit | ConvertTo-Json -Compress
    Write-Host 'WP03_COMMIT_EXIT_CODE=0'
} catch {
    Write-Host "WP03_COMMIT_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_COMMIT_EXIT_CODE=1'
}

Write-Host '=== ROLLED-BACK TRANSACTION ==='
try {
    $rollback = Invoke-RestMethod `
      -Method Post `
      -Uri "$baseUrl/transaction?name=wp03-azure-rollback&commit=false" `
      -TimeoutSec 60
    $rollback | ConvertTo-Json -Compress
    Write-Host 'WP03_ROLLBACK_EXIT_CODE=0'
} catch {
    Write-Host "WP03_ROLLBACK_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_ROLLBACK_EXIT_CODE=1'
}

Write-Host '=== STATE / INTEGRITY / FILES ==='
try {
    $state = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 60
    $state | ConvertTo-Json -Depth 10 -Compress
    Write-Host 'WP03_STATE_EXIT_CODE=0'
} catch {
    Write-Host "WP03_STATE_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_STATE_EXIT_CODE=1'
}