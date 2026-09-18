$rg='rg-aiq-r112-wp03-wcus-5ec325382770'; $plan='asp-aiq-r112-wp03-wcus-5ec325382770'; $app='aiqr112wp035ec325382770'
Write-Host '=== F1 PLAN ==='
az appservice plan show --resource-group $rg --name $plan --query '{name:name,sku:sku.name,tier:sku.tier,kind:kind,location:location}' --output json
Write-Host "WP04_RESET_PLAN_EXIT_CODE=$LASTEXITCODE"
Write-Host '=== APP STATE ==='
az webapp show --resource-group $rg --name $app --query '{name:name,state:state,availabilityState:availabilityState,location:location,httpsOnly:httpsOnly}' --output json
Write-Host "WP04_RESET_APP_EXIT_CODE=$LASTEXITCODE"
Write-Host '=== IMAGE ==='
az webapp config container show --resource-group $rg --name $app --query "[?name=='DOCKER_CUSTOM_IMAGE_NAME'].{name:name,value:value}" --output json
Write-Host "WP04_RESET_IMAGE_EXIT_CODE=$LASTEXITCODE"
Write-Host '=== PERSISTENCE SETTINGS ==='
az webapp config appsettings list --resource-group $rg --name $app --query "[?name=='Persistence__DatabasePath' || name=='Persistence__CreateParentDirectoryForInitialization' || name=='WEBSITES_ENABLE_APP_SERVICE_STORAGE'].{name:name,value:value}" --output json
Write-Host "WP04_RESET_PERSISTENCE_EXIT_CODE=$LASTEXITCODE"
Write-Host '=== REGISTRY CREDENTIAL PRESENCE ONLY ==='
az webapp config appsettings list `
  --resource-group 'rg-aiq-r112-wp03-wcus-5ec325382770' `
  --name 'aiqr112wp035ec325382770' `
  --output json |
  ConvertFrom-Json |
  Where-Object {
    $_.name -eq 'DOCKER_REGISTRY_SERVER_USERNAME' -or
    $_.name -eq 'DOCKER_REGISTRY_SERVER_PASSWORD'
  } |
  ForEach-Object {
    [pscustomobject]@{
      name = $_.name
      present = -not [string]::IsNullOrEmpty($_.value)
    }
  } |
  ConvertTo-Json

Write-Host "WP04_RESET_REGISTRY_CREDENTIALS_EXIT_CODE=$LASTEXITCODE"