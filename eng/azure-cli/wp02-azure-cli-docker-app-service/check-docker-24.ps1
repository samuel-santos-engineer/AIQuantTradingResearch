$rg = 'rg-aiq-wp02-wcus-fd81a595e4e2'
$plan = 'asp-aiq-wp02-wcus-fd81a595e4e2'
$app = 'aiqwp02wcusfd81a595e4e2'

Write-Host '=== RESOURCE GROUP ==='
az group show --name $rg --query "{name:name,location:location,state:properties.provisioningState}" --output json
Write-Host "RG_READ_EXIT=$LASTEXITCODE"

Write-Host '=== PLAN ==='
az appservice plan show --name $plan --resource-group $rg --query "{name:name,location:location,sku:sku.name,tier:sku.tier,kind:kind,workers:numberOfWorkers,state:status}" --output json
Write-Host "PLAN_READ_EXIT=$LASTEXITCODE"

Write-Host '=== WEB APP ==='
az webapp show --name $app --resource-group $rg --query "{name:name,location:location,state:state,httpsOnly:httpsOnly,kind:kind,image:siteConfig.linuxFxVersion}" --output json
Write-Host "APP_READ_EXIT=$LASTEXITCODE"

Write-Host '=== SETTINGS ==='
az webapp config appsettings list --name $app --resource-group $rg --query "[?name=='WEBSITES_ENABLE_APP_SERVICE_STORAGE' || name=='WEBSITES_PORT'].{name:name,value:value}" --output json
Write-Host "SETTINGS_READ_EXIT=$LASTEXITCODE"

Write-Host '=== TARGET RESOURCE LIST ==='
az resource list --resource-group $rg --query "[].{type:type,name:name,location:location,sku:sku.name,kind:kind}" --output json
Write-Host "RESOURCE_LIST_EXIT=$LASTEXITCODE"