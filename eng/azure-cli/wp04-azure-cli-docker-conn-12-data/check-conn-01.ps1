$targetRegion = 'westcentralus'

Write-Host '=== AUTHENTICATED SUBSCRIPTION ==='
az account show --query '{name:name,state:state,isDefault:isDefault}' --output json
Write-Host "WP04_ACCOUNT_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== MICROSOFT.WEB PROVIDER ==='
az provider show --namespace Microsoft.Web --query '{namespace:namespace,registrationState:registrationState}' --output json
Write-Host "WP04_PROVIDER_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== WEST CENTRAL US F1 AVAILABILITY ==='
$locations = @(az appservice list-locations --sku F1 --query "[?name=='$targetRegion'].{name:name,displayName:displayName}" --output json | ConvertFrom-Json)
Write-Host "WP04_F1_TARGET_MATCH_COUNT=$($locations.Count)"
$locations | ConvertTo-Json -Compress
Write-Host "WP04_F1_AVAILABILITY_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== NO LEFTOVER WP04 RESOURCE GROUP ==='
$wp04Groups = @(az group list --query "[?starts_with(name,'rg-aiq-wp04-')].name" --output tsv)
Write-Host "WP04_OWNED_RESOURCE_GROUP_COUNT=$($wp04Groups.Count)"
Write-Host "WP04_OWNED_RESOURCE_GROUP_PRESENT=$($wp04Groups.Count -gt 0)"
Write-Host "WP04_OWNED_RESOURCE_GROUP_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== NO LEFTOVER WP04 APP SERVICE PLAN ==='
$wp04Plans = @(az appservice plan list --query "[?starts_with(name,'asp-aiq-wp04-')].name" --output tsv)
Write-Host "WP04_OWNED_PLAN_COUNT=$($wp04Plans.Count)"
Write-Host "WP04_OWNED_PLAN_PRESENT=$($wp04Plans.Count -gt 0)"
Write-Host "WP04_OWNED_PLAN_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== CREDENTIAL AVAILABILITY ONLY ==='
$answer = Read-Host 'Do you already hold a Twelve Data API key locally? Enter only True or False; do not paste it'
$available = $answer.Trim().ToLowerInvariant() -eq 'true'
Write-Host "WP04_TWELVE_DATA_CREDENTIAL_AVAILABLE=$available"