$ErrorActionPreference = 'Continue'

az account show --query "{Name:name,State:state,Default:isDefault}" --output json
Write-Host "AZ_ACCOUNT_SHOW_EXIT_CODE=$LASTEXITCODE"

az provider show --namespace Microsoft.Web --query "{Namespace:namespace,RegistrationState:registrationState}" --output json
Write-Host "AZ_MICROSOFT_WEB_PROVIDER_EXIT_CODE=$LASTEXITCODE"

az appservice list-locations --sku F1 --linux-workers-enabled --query "[].{Name:name,DisplayName:displayName}" --output table
Write-Host "AZ_LINUX_F1_LOCATIONS_EXIT_CODE=$LASTEXITCODE"

az resource list --query "[?type=='Microsoft.Web/serverfarms' || type=='Microsoft.Web/sites'].{Type:type,Name:name,ResourceGroup:resourceGroup,Location:location,Kind:kind}" --output table
Write-Host "AZ_EXISTING_APP_SERVICE_RESOURCES_EXIT_CODE=$LASTEXITCODE"
