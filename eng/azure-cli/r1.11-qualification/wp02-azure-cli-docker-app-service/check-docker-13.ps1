$ErrorActionPreference = 'Continue'

Write-Host '=== SUBSCRIPTION COST CONTEXT ==='
az account show --query "{name:name,state:state,isDefault:isDefault,tenantId:tenantId,user:user.name}" --output json
Write-Host "AZ_WP02_SUBSCRIPTION_CONTEXT_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== ALL RESOURCE GROUPS ==='
az group list --query "[].{name:name,location:location,provisioningState:properties.provisioningState}" --output json
Write-Host "AZ_WP02_RESOURCE_GROUP_INVENTORY_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== ALL SUBSCRIPTION RESOURCES ==='
az resource list --query "[].{type:type,name:name,resourceGroup:resourceGroup,location:location,sku:sku.name,kind:kind}" --output json
Write-Host "AZ_WP02_RESOURCE_INVENTORY_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== APP SERVICE PLANS ==='
az appservice plan list --query "[].{name:name,resourceGroup:resourceGroup,location:location,sku:sku.name,tier:sku.tier,numberOfWorkers:numberOfWorkers,kind:kind}" --output json
Write-Host "AZ_WP02_PLAN_INVENTORY_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== WEB APPS ==='
az webapp list --query "[].{name:name,resourceGroup:resourceGroup,location:location,state:state,kind:kind,httpsOnly:httpsOnly}" --output json
Write-Host "AZ_WP02_WEBAPP_INVENTORY_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== TARGET REGION QUOTA ==='
$subscriptionId = az account show --query id --output tsv
$subscriptionExit = $LASTEXITCODE

if ($subscriptionExit -eq 0 -and $subscriptionId) {
    az rest --method get `
      --url "https://management.azure.com/subscriptions/$subscriptionId/providers/Microsoft.Web/locations/westcentralus/usages?api-version=2025-05-01" `
      --query "value[?name.value=='F1 VMs'].{name:name.localizedValue,current:currentValue,limit:limit,unit:unit}" `
      --output json
    Write-Host "AZ_WP02_TARGET_F1_QUOTA_EXIT_CODE=$LASTEXITCODE"
} else {
    Write-Host 'AZ_WP02_TARGET_F1_QUOTA_EXIT_CODE=NOT_RUN'
}

Write-Host '=== FAILED PROBE RESOURCE GROUP ==='
az group exists --name 'rg-aiq-wp02-202608300657414941'
Write-Host "AZ_WP02_FAILED_RG_EXISTS_EXIT_CODE=$LASTEXITCODE"