Write-Host '=== RESOURCE DETAILS ==='
az resource list --query "[].{type:type,name:name,resourceGroup:resourceGroup,location:location,sku:sku.name,kind:kind}" --output json
Write-Host "RESOURCE_DETAILS_EXIT=$LASTEXITCODE"

Write-Host '=== APP SERVICE PLAN DETAILS ==='
az appservice plan list --query "[].{name:name,resourceGroup:resourceGroup,location:location,sku:sku.name,tier:sku.tier,workers:numberOfWorkers,kind:kind}" --output json
Write-Host "PLAN_DETAILS_EXIT=$LASTEXITCODE"

Write-Host '=== WEB APP DETAILS ==='
az webapp list --query "[].{name:name,resourceGroup:resourceGroup,location:location,state:state,kind:kind,httpsOnly:httpsOnly}" --output json
Write-Host "WEBAPP_DETAILS_EXIT=$LASTEXITCODE"

Write-Host '=== WEST CENTRAL US F1 QUOTA ==='
az rest --method get `
  --url "https://management.azure.com/subscriptions/$(az account show --query id --output tsv)/providers/Microsoft.Web/locations/westcentralus/usages?api-version=2025-05-01" `
  --query "value[?name.value=='F1 VMs']" `
  --output json
Write-Host "F1_QUOTA_DETAILS_EXIT=$LASTEXITCODE"