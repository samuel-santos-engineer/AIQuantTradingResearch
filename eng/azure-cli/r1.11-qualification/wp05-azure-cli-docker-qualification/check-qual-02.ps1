$monthStart = '2026-08-01'
$monthEnd = '2026-08-30'

$raw = az consumption usage list --start-date $monthStart --end-date $monthEnd --output json
$exitCode = $LASTEXITCODE
Write-Host "WP05_COST_DETAIL_RETRY_QUERY_EXIT_CODE=$exitCode"
if ($exitCode -ne 0) { throw 'Cost usage query failed.' }

$parsed = $raw | ConvertFrom-Json
$records = if ($null -ne $parsed.value) { @($parsed.value) } else { @($parsed) }
Write-Host "WP05_COST_DETAIL_RETRY_TOTAL_RECORD_COUNT=$($records.Count)"

$owned = @($records | Where-Object {
    (($_ | ConvertTo-Json -Depth 8 -Compress) -match 'aiq-wp0[2-5]') -or
    (($_ | ConvertTo-Json -Depth 8 -Compress) -match 'INIT-1\.11')
})
Write-Host "WP05_COST_DETAIL_RETRY_OWNED_RECORD_COUNT=$($owned.Count)"

$sanitized = @(
    foreach ($record in $owned) {
        [PSCustomObject]@{
            UsageStart       = $record.usageStart
            UsageEnd         = $record.usageEnd
            MeterCategory    = $record.meterDetails.meterCategory
            MeterSubCategory = $record.meterDetails.meterSubCategory
            MeterRegion      = $record.meterDetails.meterRegion
            UsageQuantity    = $record.usageQuantity
            Unit             = $record.meterDetails.unit
            PretaxCost       = $record.pretaxCost
            Currency         = $record.currency
            IsEstimated      = $record.isEstimated
        }
    }
)
$sanitized | ConvertTo-Json -Compress

$costKnown = $true
$costSum = [decimal]0
foreach ($record in $sanitized) {
    if ($null -eq $record.PretaxCost -or $record.PretaxCost -eq '') {
        $costKnown = $false
        break
    }
    $costSum += [decimal]$record.PretaxCost
}
Write-Host "WP05_OWNED_PRETAX_COST_KNOWN=$costKnown"
Write-Host "WP05_OWNED_PRETAX_COST_SUM=$(if ($costKnown) { $costSum } else { 'UNKNOWN' })"