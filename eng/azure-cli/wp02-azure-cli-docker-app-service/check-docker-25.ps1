$ErrorActionPreference = 'Continue'

$resourceGroup = 'rg-aiq-wp02-wcus-fd81a595e4e2'

Write-Host '=== FINAL PRE-DELETE INVENTORY ==='
az resource list `
  --resource-group $resourceGroup `
  --query "[].{type:type,name:name,location:location,sku:sku.name,kind:kind}" `
  --output json
Write-Host "PRE_DELETE_INVENTORY_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== DELETE ONLY WP02 RESOURCE GROUP ==='
az group delete --name $resourceGroup --yes --no-wait
Write-Host "RESOURCE_GROUP_DELETE_REQUEST_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== WAIT FOR RESOURCE GROUP DELETION ==='
$deleted = $false
for ($i = 1; $i -le 36; $i++) {
    Write-Host "DELETE_POLL_ATTEMPT=$i"
    $exists = az group exists --name $resourceGroup
    $pollExit = $LASTEXITCODE
    Write-Host "DELETE_POLL_EXISTS=$exists"
    Write-Host "DELETE_POLL_EXIT_CODE=$pollExit"

    if ($exists -eq 'false') {
        $deleted = $true
        break
    }

    Start-Sleep -Seconds 5
}
Write-Host "RESOURCE_GROUP_DELETED=$deleted"

Write-Host '=== CLEANUP READ-BACK ==='
az group exists --name $resourceGroup
Write-Host "RESOURCE_GROUP_EXISTS_READBACK_EXIT_CODE=$LASTEXITCODE"

az resource list --resource-group $resourceGroup --output json
Write-Host "POST_DELETE_RESOURCE_LIST_EXIT_CODE=$LASTEXITCODE"