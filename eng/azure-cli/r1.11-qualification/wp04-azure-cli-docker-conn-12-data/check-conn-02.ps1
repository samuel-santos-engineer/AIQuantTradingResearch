Write-Host '=== F1 TARGET AVAILABILITY: POWERSHELL FILTER ==='
$f1Locations = @(az appservice list-locations --sku F1 --output tsv)
$f1Match = @($f1Locations | Where-Object {
    $_.Trim() -eq 'West Central US' -or $_.Trim() -eq 'westcentralus'
})
Write-Host "WP04_F1_TARGET_MATCH_COUNT=$($f1Match.Count)"
Write-Host "WP04_F1_TARGET_MATCH=$($f1Match -join ',')"
Write-Host "WP04_F1_AVAILABILITY_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== WP04 RESOURCE GROUPS: POWERSHELL FILTER ==='
$allGroups = @(az group list --output json | ConvertFrom-Json)
$ownedGroups = @($allGroups | Where-Object { $_.name -like 'rg-aiq-wp04-*' })
Write-Host "WP04_OWNED_RESOURCE_GROUP_COUNT=$($ownedGroups.Count)"
Write-Host "WP04_OWNED_RESOURCE_GROUP_PRESENT=$($ownedGroups.Count -gt 0)"
Write-Host "WP04_OWNED_RESOURCE_GROUP_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== WP04 APP SERVICE PLANS: POWERSHELL FILTER ==='
$allPlans = @(az appservice plan list --output json | ConvertFrom-Json)
$ownedPlans = @($allPlans | Where-Object { $_.name -like 'asp-aiq-wp04-*' })
Write-Host "WP04_OWNED_PLAN_COUNT=$($ownedPlans.Count)"
Write-Host "WP04_OWNED_PLAN_PRESENT=$($ownedPlans.Count -gt 0)"
Write-Host "WP04_OWNED_PLAN_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== PREVIOUS CREDENTIAL AVAILABILITY EVIDENCE ==='
Write-Host 'WP04_TWELVE_DATA_CREDENTIAL_AVAILABLE=True'