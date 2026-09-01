[CmdletBinding()]
param(
    [Parameter(Mandatory)] [string] $ResourceGroup,
    [Parameter(Mandatory)] [string] $PlanName,
    [Parameter(Mandatory)] [string] $WebAppName
)

$ErrorActionPreference = 'Stop'
function Write-ExitCode([string] $Name) { Write-Host "$Name=$LASTEXITCODE" }

Write-Host '=== F1 PLAN ==='
& az appservice plan show --name $PlanName --resource-group $ResourceGroup --query '{name:name,location:location,sku:sku.name,tier:sku.tier,kind:kind}' --output json
Write-ExitCode 'WP03_VERIFY_PLAN_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Plan read-back failed.' }

Write-Host '=== WEB APP RUNTIME ==='
& az webapp show --name $WebAppName --resource-group $ResourceGroup --query '{name:name,location:location,httpsOnly:httpsOnly,state:state,kind:kind,publicNetworkAccess:publicNetworkAccess}' --output json
Write-ExitCode 'WP03_VERIFY_WEBAPP_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Web App read-back failed.' }

Write-Host '=== CONTAINER IMAGE ==='
& az webapp config container show --name $WebAppName --resource-group $ResourceGroup --output json
Write-ExitCode 'WP03_VERIFY_CONTAINER_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Container configuration read-back failed.' }

Write-Host '=== NON-SECRET DEPLOYMENT SETTINGS ==='
& az webapp config appsettings list --name $WebAppName --resource-group $ResourceGroup --query "[?name=='WEBSITES_ENABLE_APP_SERVICE_STORAGE' || name=='WEBSITES_PORT'].{name:name,value:value}" --output json
Write-ExitCode 'WP03_VERIFY_SETTINGS_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Deployment-settings read-back failed.' }

Write-Host '=== OWNED RESOURCE INVENTORY ==='
& az resource list --resource-group $ResourceGroup --query "[].{name:name,type:type,location:location,kind:kind,sku:sku.name}" --output json
Write-ExitCode 'WP03_VERIFY_RESOURCE_INVENTORY_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Resource inventory read-back failed.' }

Write-Host 'WP03_VERIFY_BOUNDARY=No secrets, SQLite initialization, provider automation, or public-health claims are validated by WP03.'
