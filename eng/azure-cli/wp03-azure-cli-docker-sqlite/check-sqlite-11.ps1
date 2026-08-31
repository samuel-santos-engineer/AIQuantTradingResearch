$ErrorActionPreference = 'Continue'

$appName = 'aiqwp03wcus3d25217bd701'
$baseUrl = "https://$appName.azurewebsites.net"

Write-Host '=== MATRIX: DELETE MODE ==='
try {
    $delete = Invoke-RestMethod `
      -Method Post `
      -Uri "$baseUrl/baseline?mode=DELETE&name=wp03-matrix-delete" `
      -TimeoutSec 60
    $delete | ConvertTo-Json -Compress
    Write-Host 'WP03_MATRIX_DELETE_REQUEST_EXIT_CODE=0'
} catch {
    Write-Host "WP03_MATRIX_DELETE_REQUEST_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_MATRIX_DELETE_REQUEST_EXIT_CODE=1'
}

try {
    $deleteState = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 60
    $deleteState | ConvertTo-Json -Depth 10 -Compress
    Write-Host 'WP03_MATRIX_DELETE_STATE_EXIT_CODE=0'
} catch {
    Write-Host "WP03_MATRIX_DELETE_STATE_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_MATRIX_DELETE_STATE_EXIT_CODE=1'
}

Write-Host '=== MATRIX: WAL MODE ==='
try {
    $wal = Invoke-RestMethod `
      -Method Post `
      -Uri "$baseUrl/baseline?mode=WAL&name=wp03-matrix-wal" `
      -TimeoutSec 60
    $wal | ConvertTo-Json -Compress
    Write-Host 'WP03_MATRIX_WAL_REQUEST_EXIT_CODE=0'
} catch {
    Write-Host "WP03_MATRIX_WAL_REQUEST_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_MATRIX_WAL_REQUEST_EXIT_CODE=1'
}

try {
    $walState = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 60
    $walState | ConvertTo-Json -Depth 10 -Compress
    Write-Host 'WP03_MATRIX_WAL_STATE_EXIT_CODE=0'
} catch {
    Write-Host "WP03_MATRIX_WAL_STATE_ERROR=$($_.Exception.Message)"
    Write-Host 'WP03_MATRIX_WAL_STATE_EXIT_CODE=1'
}