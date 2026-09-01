$targetRegion = 'westcentralus'
$monthStart = '2026-08-01'
$monthEnd = '2026-08-30'

Write-Host '=== SUBSCRIPTION / PROVIDER ==='
az account show --query '{name:name,state:state,isDefault:isDefault}' --output json
Write-Host "WP05_ACCOUNT_READ_EXIT_CODE=$LASTEXITCODE"

az provider show --namespace Microsoft.Web --query '{namespace:namespace,registrationState:registrationState}' --output json
Write-Host "WP05_PROVIDER_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== WEST CENTRAL US F1 AVAILABILITY ==='
$f1Locations = @(az appservice list-locations --sku F1 --output tsv)
$f1Match = @($f1Locations | Where-Object { $_.Trim() -eq 'West Central US' -or $_.Trim() -eq $targetRegion })
Write-Host "WP05_F1_TARGET_MATCH_COUNT=$($f1Match.Count)"
Write-Host "WP05_F1_TARGET_MATCH=$($f1Match -join ',')"
Write-Host "WP05_F1_AVAILABILITY_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== NO LEFTOVER INITIATIVE RESOURCE GROUPS ==='
$groups = @(az group list --output json | ConvertFrom-Json)
$initiativeGroups = @($groups | Where-Object { $_.name -like 'rg-aiq-wp0[2-5]-*' })
Write-Host "WP05_INITIATIVE_RESOURCE_GROUP_COUNT=$($initiativeGroups.Count)"
Write-Host "WP05_INITIATIVE_RESOURCE_GROUP_PRESENT=$($initiativeGroups.Count -gt 0)"
Write-Host "WP05_INITIATIVE_RESOURCE_GROUP_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== AZURE COST-USAGE COMMAND AVAILABILITY ==='
az consumption usage list --help *> $null
Write-Host "WP05_COST_USAGE_HELP_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== SANITIZED AUGUST USAGE QUERY ==='
$usage = @()
$usageError = $null
try {
    $usage = @(az consumption usage list --start-date $monthStart --end-date $monthEnd --output json | ConvertFrom-Json)
    $usageError = $LASTEXITCODE
} catch {
    $usageError = 1
}
Write-Host "WP05_COST_USAGE_QUERY_EXIT_CODE=$usageError"
Write-Host "WP05_COST_USAGE_RECORD_COUNT=$($usage.Count)"

# Do not print records. Only determine whether any record text references owned AIQ probes.
$ownedUsageRecords = @($usage | Where-Object {
    (($_ | ConvertTo-Json -Depth 8 -Compress) -match 'aiq-wp0[2-5]') -or
    (($_ | ConvertTo-Json -Depth 8 -Compress) -match 'INIT-1\.11')
})
Write-Host "WP05_OWNED_USAGE_RECORD_COUNT=$($ownedUsageRecords.Count)"