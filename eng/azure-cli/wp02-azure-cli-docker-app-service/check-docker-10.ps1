$ErrorActionPreference = 'Continue'

$location = 'brazilsouth'
$runId = (Get-Date -Format 'yyyyMMddHHmmss') + (Get-Random -Minimum 1000 -Maximum 9999)
$resourceGroup = "rg-aiq-wp02-$runId"
$planName = "asp-aiq-wp02-$runId"
$webAppName = "aiqwp02$runId"

Write-Host "AZ_WP02_RESOURCE_GROUP=$resourceGroup"
Write-Host "AZ_WP02_PLAN_NAME=$planName"
Write-Host "AZ_WP02_WEB_APP_NAME=$webAppName"
Write-Host "AZ_WP02_LOCATION=$location"

az group create --name $resourceGroup --location $location --output json
Write-Host "AZ_WP02_RESOURCE_GROUP_CREATE_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Resource group creation failed.' }

az appservice plan create `
    --name $planName `
    --resource-group $resourceGroup `
    --location $location `
    --sku F1 `
    --is-linux `
    --output json
Write-Host "AZ_WP02_LINUX_F1_PLAN_CREATE_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Linux F1 App Service plan creation failed.' }

az appservice plan show `
    --name $planName `
    --resource-group $resourceGroup `
    --query "{Name:name,Location:location,Kind:kind,Reserved:reserved,Sku:sku.name,Tier:sku.tier,Capacity:sku.capacity,Status:status}" `
    --output json
Write-Host "AZ_WP02_LINUX_F1_PLAN_READBACK_EXIT_CODE=$LASTEXITCODE"

az resource list `
    --resource-group $resourceGroup `
    --query "[].{Type:type,Name:name,Location:location,Kind:kind,Sku:sku.name}" `
    --output table
Write-Host "AZ_WP02_RESOURCE_GROUP_INVENTORY_EXIT_CODE=$LASTEXITCODE"