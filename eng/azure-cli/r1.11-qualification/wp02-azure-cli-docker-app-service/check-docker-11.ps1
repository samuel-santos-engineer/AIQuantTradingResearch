$resourceGroup = 'rg-aiq-wp02-202608300657414941'
$location = 'brazilsouth'

az resource list --resource-group $resourceGroup --output table
Write-Host "AZ_WP02_FAILED_RESOURCE_GROUP_INVENTORY_EXIT_CODE=$LASTEXITCODE"

az group delete --name $resourceGroup --yes
Write-Host "AZ_WP02_FAILED_RESOURCE_GROUP_DELETE_EXIT_CODE=$LASTEXITCODE"

az group exists --name $resourceGroup --output tsv
Write-Host "AZ_WP02_FAILED_RESOURCE_GROUP_EXISTS_READBACK_EXIT_CODE=$LASTEXITCODE"

az vm list-usage --location $location `
    --query "[?contains(name.localizedValue, 'Total')].{Name:name.localizedValue,Current:currentValue,Limit:limit}" `
    --output table
Write-Host "AZ_WP02_BRAZIL_SOUTH_QUOTA_READ_EXIT_CODE=$LASTEXITCODE"