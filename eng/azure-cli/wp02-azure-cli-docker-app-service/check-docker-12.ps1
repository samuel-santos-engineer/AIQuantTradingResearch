$ErrorActionPreference = 'Continue'
$region = 'westcentralus'
$failedRg = 'rg-aiq-wp02-202608300657414941'

Write-Host '=== ACCOUNT ==='
$account = az account show --query "{name:name,state:state,isDefault:isDefault}" --output json
Write-Host $account
Write-Host "ACCOUNT_EXIT=$LASTEXITCODE"

Write-Host '=== PROVIDER ==='
$provider = az provider show --namespace Microsoft.Web --query "{namespace:namespace,registrationState:registrationState}" --output json
Write-Host $provider
Write-Host "PROVIDER_EXIT=$LASTEXITCODE"

Write-Host '=== REGION ==='
$location = az account list-locations --query "[?name=='$region'].{name:name,displayName:displayName}" --output json
Write-Host $location
Write-Host "REGION_EXIT=$LASTEXITCODE"

Write-Host '=== F1 AVAILABILITY ==='
$f1 = @(az appservice list-locations --sku F1 --linux-workers-enabled --output tsv)
$f1Exit = $LASTEXITCODE
$match = @($f1 | Where-Object { $_ -eq 'West Central US' })
Write-Host "F1_MATCH_COUNT=$($match.Count)"
Write-Host "F1_MATCH=$($match -join ',')"
Write-Host "F1_EXIT=$f1Exit"

Write-Host '=== WEB USAGE ==='
$subId = az account show --query id --output tsv
$subExit = $LASTEXITCODE
if ($subExit -eq 0 -and $subId) {
    $usage = az rest --method get `
      --url "https://management.azure.com/subscriptions/$subId/providers/Microsoft.Web/locations/$region/usages?api-version=2025-05-01" `
      --query "value[].{name:name.localizedValue,current:currentValue,limit:limit,unit:unit}" `
      --output json
    Write-Host $usage
    Write-Host "WEB_USAGE_EXIT=$LASTEXITCODE"
} else {
    Write-Host 'WEB_USAGE_EXIT=NOT_RUN'
}

Write-Host '=== EXISTING PLANS ==='
az appservice plan list `
  --query "[].{location:location,sku:sku.name,tier:sku.tier,kind:kind}" `
  --output json
Write-Host "PLANS_EXIT=$LASTEXITCODE"

Write-Host '=== EXISTING APPS ==='
az webapp list `
  --query "[].{location:location,kind:kind,state:state}" `
  --output json
Write-Host "APPS_EXIT=$LASTEXITCODE"

Write-Host '=== PRIOR FAILED RESOURCE GROUP ==='
$exists = az group exists --name $failedRg
$existsExit = $LASTEXITCODE
Write-Host "FAILED_RG_EXISTS=$exists"
Write-Host "FAILED_RG_EXIT=$existsExit"