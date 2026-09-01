$app = 'aiqwp03wcus3d25217bd701'
$baseUrl = "https://$app.azurewebsites.net"

function Invoke-Wp03Post([string]$path) {
    $response = Invoke-RestMethod -Method Post -Uri "$baseUrl$path" -TimeoutSec 30
    $response | ConvertTo-Json -Compress
    return $response
}

Write-Host '=== SELECTED DELETE MODE ==='
$baseline = Invoke-Wp03Post '/baseline?mode=DELETE&name=wp03-selected-delete-baseline'
Write-Host 'WP03_SELECTED_DELETE_BASELINE_EXIT_CODE=0'

$write1 = Invoke-Wp03Post '/write?name=wp03-selected-delete-write-one'
Write-Host 'WP03_SELECTED_DELETE_WRITE_ONE_EXIT_CODE=0'

$write2 = Invoke-Wp03Post '/write?name=wp03-selected-delete-write-two'
Write-Host 'WP03_SELECTED_DELETE_WRITE_TWO_EXIT_CODE=0'

$update = Invoke-Wp03Post '/update?seq=10&name=wp03-selected-delete-baseline-updated'
Write-Host 'WP03_SELECTED_DELETE_UPDATE_EXIT_CODE=0'

$rollback = Invoke-Wp03Post '/transaction?name=wp03-selected-delete-rollback&commit=false'
Write-Host 'WP03_SELECTED_DELETE_ROLLBACK_EXIT_CODE=0'

Write-Host '=== SELECTED DELETE STATE / INTEGRITY ==='
$state = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 30
$state | ConvertTo-Json -Depth 8 -Compress

$names = @($state.rows | ForEach-Object name)
$valid = $state.journalMode -eq 'delete' -and
         $state.integrityCheck -eq 'ok' -and
         $state.quickCheck -eq 'ok' -and
         $names -contains 'wp03-selected-delete-baseline-updated' -and
         $names -contains 'wp03-selected-delete-write-one' -and
         $names -contains 'wp03-selected-delete-write-two' -and
         -not ($names -contains 'wp03-selected-delete-rollback')
Write-Host "WP03_SELECTED_DELETE_DATA_VALID=$valid"
Write-Host 'WP03_SELECTED_DELETE_STATE_EXIT_CODE=0'
if (-not $valid) { throw 'Selected DELETE-mode data proof failed.' }