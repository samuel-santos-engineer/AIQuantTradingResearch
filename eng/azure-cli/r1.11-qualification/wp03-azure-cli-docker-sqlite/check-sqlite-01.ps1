$ErrorActionPreference = 'Continue'

$region = 'westcentralus'
$wp02ResourceGroup = 'rg-aiq-wp02-wcus-fd81a595e4e2'
$probeImage = 'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe@sha256:572d402b138de1f4f70803329a45d4cc4177969059d2789ae4b8df90a2b52f43'

Write-Host '=== USER / DOCKER RUNTIME ==='
whoami
Write-Host "WHOAMI_EXIT_CODE=$LASTEXITCODE"
docker info --format 'Server={{.ServerVersion}};OSType={{.OSType}};Architecture={{.Architecture}};Context={{.Name}}'
Write-Host "DOCKER_INFO_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== AZURE ACCOUNT / PROVIDER ==='
az account show --query "{name:name,state:state,isDefault:isDefault}" --output json
Write-Host "AZ_ACCOUNT_EXIT_CODE=$LASTEXITCODE"
az provider show --namespace Microsoft.Web --query "{namespace:namespace,registrationState:registrationState}" --output json
Write-Host "AZ_MICROSOFT_WEB_PROVIDER_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== WEST CENTRAL US F1 QUOTA ==='
$subscriptionId = az account show --query id --output tsv
Write-Host "AZ_SUBSCRIPTION_LOOKUP_EXIT_CODE=$LASTEXITCODE"
az rest --method get `
  --url "https://management.azure.com/subscriptions/$subscriptionId/providers/Microsoft.Web/locations/$region/usages?api-version=2025-05-01" `
  --query "value[?name.value=='F1'].{name:name.localizedValue,current:currentValue,limit:limit,unit:unit}" `
  --output json
Write-Host "AZ_TARGET_F1_QUOTA_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== NO LEFTOVER WP02 RESOURCE GROUP ==='
az group exists --name $wp02ResourceGroup
Write-Host "AZ_WP02_GROUP_EXISTS_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== EXISTING INITIATIVE-TAGGED RESOURCES ==='
az resource list --tag initiative=INIT-1.11 `
  --query "[].{type:type,name:name,resourceGroup:resourceGroup,location:location,sku:sku.name,kind:kind}" `
  --output json
Write-Host "AZ_INITIATIVE_RESOURCE_INVENTORY_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== PUBLIC GHCR PROBE IMAGE AVAILABILITY ==='
docker manifest inspect $probeImage --verbose
Write-Host "GHCR_WP02_IMAGE_MANIFEST_EXIT_CODE=$LASTEXITCODE"