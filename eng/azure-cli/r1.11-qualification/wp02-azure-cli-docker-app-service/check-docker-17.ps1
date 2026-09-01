$ErrorActionPreference = 'Continue'

$runId = [guid]::NewGuid().ToString('N').Substring(0,12)
$resourceGroup = "rg-aiq-wp02-wcus-$runId"
$planName = "asp-aiq-wp02-wcus-$runId"
$appName = "aiqwp02wcus$runId"
$location = "westcentralus"
$image = "ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe@sha256:572d402b138de1f4f70803329a45d4cc4177969059d2789ae4b8df90a2b52f43"

Write-Host "WP02_RESOURCE_GROUP=$resourceGroup"
Write-Host "WP02_PLAN_NAME=$planName"
Write-Host "WP02_WEB_APP_NAME=$appName"
Write-Host "WP02_LOCATION=$location"
Write-Host "WP02_IMAGE=$image"

Write-Host '=== CREATE RESOURCE GROUP ==='
az group create `
  --name $resourceGroup `
  --location $location `
  --tags "initiative=INIT-1.11" "workPackage=WP02" "purpose=feasibility-probe" "costCeiling=0"
Write-Host "RESOURCE_GROUP_CREATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== CREATE LINUX F1 PLAN ==='
az appservice plan create `
  --name $planName `
  --resource-group $resourceGroup `
  --location $location `
  --sku F1 `
  --is-linux
Write-Host "F1_PLAN_CREATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== CREATE WEB APP FROM PUBLIC GHCR IMAGE ==='
az webapp create `
  --name $appName `
  --resource-group $resourceGroup `
  --plan $planName `
  --deployment-container-image-name $image
Write-Host "WEB_APP_CREATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== CONFIGURE PERSISTENT HOME AND PORT ==='
az webapp config appsettings set `
  --name $appName `
  --resource-group $resourceGroup `
  --settings WEBSITES_ENABLE_APP_SERVICE_STORAGE=true WEBSITES_PORT=8080
Write-Host "APP_SETTINGS_UPDATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== ENFORCE HTTPS ==='
az webapp update `
  --name $appName `
  --resource-group $resourceGroup `
  --https-only true
Write-Host "HTTPS_ONLY_UPDATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== READ-BACK ==='
az appservice plan show `
  --name $planName `
  --resource-group $resourceGroup `
  --query "{name:name,location:location,sku:sku.name,tier:sku.tier,kind:kind,numberOfWorkers:numberOfWorkers}" `
  --output json
Write-Host "PLAN_READBACK_EXIT_CODE=$LASTEXITCODE"

az webapp show `
  --name $appName `
  --resource-group $resourceGroup `
  --query "{name:name,defaultHostName:defaultHostName,state:state,httpsOnly:httpsOnly,kind:kind}" `
  --output json
Write-Host "WEB_APP_READBACK_EXIT_CODE=$LASTEXITCODE"

az webapp config appsettings list `
  --name $appName `
  --resource-group $resourceGroup `
  --query "[?name=='WEBSITES_ENABLE_APP_SERVICE_STORAGE' || name=='WEBSITES_PORT'].{name:name,value:value}" `
  --output json
Write-Host "APP_SETTINGS_READBACK_EXIT_CODE=$LASTEXITCODE"