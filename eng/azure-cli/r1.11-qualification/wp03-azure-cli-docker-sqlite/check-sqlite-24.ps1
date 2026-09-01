$rg = 'rg-aiq-wp03-wcus-3d25217bd701'
$plan = 'asp-aiq-wp03-wcus-3d25217bd701'
$app = 'aiqwp03wcus3d25217bd701'

Write-Host '=== RESOURCE GROUP ==='
az group show --name $rg --query '{name:name,location:location,provisioningState:properties.provisioningState}' --output json
Write-Host "WP03_FINAL_RG_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== APP SERVICE PLAN: STRICT-$0 EVIDENCE ==='
az appservice plan show --resource-group $rg --name $plan `
  --query '{name:name,location:location,kind:kind,sku:sku.name,tier:sku.tier,workers:sku.capacity}' `
  --output json
Write-Host "WP03_FINAL_PLAN_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== WEB APP ==='
az webapp show --resource-group $rg --name $app `
  --query '{name:name,location:location,state:state,kind:kind,httpsOnly:httpsOnly,image:siteConfig.linuxFxVersion}' `
  --output json
Write-Host "WP03_FINAL_WEBAPP_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== REQUIRED SETTINGS ==='
az webapp config appsettings list --resource-group $rg --name $app `
  --query "[?name=='WEBSITES_ENABLE_APP_SERVICE_STORAGE' || name=='WEBSITES_PORT' || name=='SQLITE_PATH' || name=='PROBE_REVISION'].{name:name,value:value}" `
  --output json
Write-Host "WP03_FINAL_SETTINGS_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== COMPLETE TARGET RESOURCE INVENTORY ==='
az resource list --resource-group $rg `
  --query '[].{type:type,name:name,location:location,kind:kind,sku:sku.name}' `
  --output json
Write-Host "WP03_FINAL_RESOURCE_INVENTORY_EXIT_CODE=$LASTEXITCODE"

$subscriptionId = az account show --query id --output tsv
Write-Host "WP03_FINAL_SUBSCRIPTION_LOOKUP_EXIT_CODE=$LASTEXITCODE"
Write-Host '=== WEST CENTRAL US F1 QUOTA ==='
az rest --method get `
  --url "https://management.azure.com/subscriptions/$subscriptionId/providers/Microsoft.Web/locations/westcentralus/usages?api-version=2023-12-01" `
  --query "value[?name.value=='Free VMs' || name.value=='F1 VMs'].{name:name.localizedValue,current:currentValue,limit:limit,unit:unit}" `
  --output json
Write-Host "WP03_FINAL_F1_QUOTA_READ_EXIT_CODE=$LASTEXITCODE"