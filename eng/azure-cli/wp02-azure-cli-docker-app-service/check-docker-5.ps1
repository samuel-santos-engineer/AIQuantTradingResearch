$ErrorActionPreference = 'Continue'
$targetLocation = 'brazilsouth'

az account list-locations --query "[?name=='brazilsouth'] | [0].{Name:name,DisplayName:displayName,RegionalDisplayName:regionalDisplayName}" --output json
Write-Host "AZ_TARGET_REGION_METADATA_EXIT_CODE=$LASTEXITCODE"

$f1LinuxRegions = @(
    az appservice list-locations --sku F1 --linux-workers-enabled --output tsv
)
$f1LinuxExit = $LASTEXITCODE
Write-Host "AZ_LINUX_F1_REGION_DISCOVERY_EXIT_CODE=$f1LinuxExit"
Write-Host "AZ_BRAZIL_SOUTH_LINUX_F1_AVAILABLE=$($f1LinuxRegions -contains 'Brazil South')"
$f1LinuxRegions

az appservice plan list --query "[].{Name:name,ResourceGroup:resourceGroup,Location:location,Sku:sku.name,Tier:sku.tier,Kind:kind}" --output table
Write-Host "AZ_APP_SERVICE_PLAN_INVENTORY_EXIT_CODE=$LASTEXITCODE"

az resource list --resource-type Microsoft.Web/sites --query "[].{Name:name,ResourceGroup:resourceGroup,Location:location,Kind:kind}" --output table
Write-Host "AZ_APP_SERVICE_SITE_INVENTORY_EXIT_CODE=$LASTEXITCODE"