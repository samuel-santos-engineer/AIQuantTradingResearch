$ErrorActionPreference = 'Continue'

Write-Host 'ACCOUNT_NAME='(az account show --query name --output tsv)
Write-Host 'ACCOUNT_STATE='(az account show --query state --output tsv)
Write-Host 'ACCOUNT_DEFAULT='(az account show --query isDefault --output tsv)
Write-Host 'ACCOUNT_EXIT=' $LASTEXITCODE

$groups = @(az group list --query "[].name" --output tsv)
Write-Host 'RESOURCE_GROUP_COUNT=' $groups.Count
$groups | ForEach-Object { Write-Host "RESOURCE_GROUP=$_"
}
Write-Host 'GROUPS_EXIT=' $LASTEXITCODE

$resources = @(az resource list --query "[].{type:type,name:name,resourceGroup:resourceGroup,location:location,sku:sku.name,kind:kind}" --output json | ConvertFrom-Json)
Write-Host 'RESOURCE_COUNT=' $resources.Count
$resources | ForEach-Object {
    Write-Host ("RESOURCE type={0};name={1};group={2};location={3};sku={4};kind={5}" -f $_.type,$_.name,$_.resourceGroup,$_.location,$_.sku,$_.kind)
}
Write-Host 'RESOURCES_EXIT=' $LASTEXITCODE

$plans = @(az appservice plan list --query "[].{name:name,resourceGroup:resourceGroup,location:location,sku:sku.name,tier:sku.tier,workers:numberOfWorkers,kind:kind}" --output json | ConvertFrom-Json)
Write-Host 'PLAN_COUNT=' $plans.Count
$plans | ForEach-Object {
    Write-Host ("PLAN name={0};group={1};location={2};sku={3};tier={4};workers={5};kind={6}" -f $_.name,$_.resourceGroup,$_.location,$_.sku,$_.tier,$_.workers,$_.kind)
}
Write-Host 'PLANS_EXIT=' $LASTEXITCODE

$apps = @(az webapp list --query "[].{name:name,resourceGroup:resourceGroup,location:location,state:state,kind:kind,httpsOnly:httpsOnly}" --output json | ConvertFrom-Json)
Write-Host 'WEBAPP_COUNT=' $apps.Count
$apps | ForEach-Object {
    Write-Host ("WEBAPP name={0};group={1};location={2};state={3};kind={4};httpsOnly={5}" -f $_.name,$_.resourceGroup,$_.location,$_.state,$_.kind,$_.httpsOnly)
}
Write-Host 'WEBAPPS_EXIT=' $LASTEXITCODE

$subscriptionId = az account show --query id --output tsv
$usage = @(az rest --method get `
  --url "https://management.azure.com/subscriptions/$subscriptionId/providers/Microsoft.Web/locations/westcentralus/usages?api-version=2025-05-01" `
  --output json | ConvertFrom-Json).value

$f1 = $usage | Where-Object { $_.name.value -eq 'F1 VMs' }
Write-Host "F1_CURRENT=$($f1.currentValue)"
Write-Host "F1_LIMIT=$($f1.limit)"
Write-Host "F1_UNIT=$($f1.unit)"
Write-Host 'QUOTA_EXIT=' $LASTEXITCODE

$failedRgExists = az group exists --name 'rg-aiq-wp02-202608300657414941'
Write-Host "FAILED_RG_EXISTS=$failedRgExists"
Write-Host 'FAILED_RG_EXIT=' $LASTEXITCODE