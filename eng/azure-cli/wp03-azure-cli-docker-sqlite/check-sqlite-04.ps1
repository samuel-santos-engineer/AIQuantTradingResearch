$ErrorActionPreference = 'Stop'

$runId = [guid]::NewGuid().ToString('N').Substring(0,12)
$resourceGroup = "rg-aiq-wp03-wcus-$runId"
$planName = "asp-aiq-wp03-wcus-$runId"
$appName = "aiqwp03wcus$runId"
$location = 'westcentralus'
$image = 'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r1-86df07f8b09844ddbb8e0501df8a358a'

Write-Host "WP03_RESOURCE_GROUP=$resourceGroup"
Write-Host "WP03_PLAN_NAME=$planName"
Write-Host "WP03_WEB_APP_NAME=$appName"
Write-Host "WP03_LOCATION=$location"
Write-Host "WP03_IMAGE=$image"

Write-Host '=== CREATE RESOURCE GROUP ==='
az group create `
  --name $resourceGroup `
  --location $location `
  --tags "initiative=INIT-1.11" "workPackage=WP03" "purpose=sqlite-qualification" "costCeiling=0"
Write-Host "WP03_RESOURCE_GROUP_CREATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== CREATE LINUX F1 PLAN ==='
az appservice plan create `
  --name $planName `
  --resource-group $resourceGroup `
  --location $location `
  --sku F1 `
  --is-linux
Write-Host "WP03_F1_PLAN_CREATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== CREATE WEB APP ==='
az webapp create `
  --name $appName `
  --resource-group $resourceGroup `
  --plan $planName `
  --deployment-container-image-name $image
Write-Host "WP03_WEB_APP_CREATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== CONFIGURE PERSISTENT HOME / SQLITE PATH / PORT ==='
az webapp config appsettings set `
  --name $appName `
  --resource-group $resourceGroup `
  --settings `
    WEBSITES_ENABLE_APP_SERVICE_STORAGE=true `
    WEBSITES_PORT=8080 `
    SQLITE_PATH=/home/aiq-wp03/qualification.sqlite3 `
    PROBE_REVISION=wp03-r1
Write-Host "WP03_APP_SETTINGS_UPDATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== ENFORCE HTTPS ==='
az webapp update `
  --name $appName `
  --resource-group $resourceGroup `
  --https-only true
Write-Host "WP03_HTTPS_ONLY_UPDATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== READ-BACK ==='
az appservice plan show `
  --name $planName `
  --resource-group $resourceGroup `
  --query "{name:name,location:location,sku:sku.name,tier:sku.tier,kind:kind}" `
  --output json
Write-Host "WP03_PLAN_READBACK_EXIT_CODE=$LASTEXITCODE"

az webapp show `
  --name $appName `
  --resource-group $resourceGroup `
  --query "{name:name,defaultHostName:defaultHostName,state:state,httpsOnly:httpsOnly,kind:kind,image:siteConfig.linuxFxVersion}" `
  --output json
Write-Host "WP03_WEB_APP_READBACK_EXIT_CODE=$LASTEXITCODE"

az webapp config appsettings list `
  --name $appName `
  --resource-group $resourceGroup `
  --query "[?name=='WEBSITES_ENABLE_APP_SERVICE_STORAGE' || name=='WEBSITES_PORT' || name=='SQLITE_PATH' || name=='PROBE_REVISION'].{name:name,value:value}" `
  --output json
Write-Host "WP03_APP_SETTINGS_READBACK_EXIT_CODE=$LASTEXITCODE"