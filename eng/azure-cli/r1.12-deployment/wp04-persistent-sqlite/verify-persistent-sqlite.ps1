[CmdletBinding()]
param(
    [Parameter(Mandatory)] [ValidatePattern('^[A-Za-z0-9][A-Za-z0-9-]{0,89}$')] [string] $ResourceGroup,
    [Parameter(Mandatory)] [ValidatePattern('^[a-z0-9][a-z0-9-]{1,58}[a-z0-9]$')] [string] $WebAppName
)

$ErrorActionPreference = 'Stop'
function Write-ExitCode([string] $Name) { Write-Host "$Name=$LASTEXITCODE" }

Write-Host '=== WEB APP RUNTIME ==='
& az webapp show --name $WebAppName --resource-group $ResourceGroup --query '{name:name,location:location,httpsOnly:httpsOnly,state:state,kind:kind}' --output json
Write-ExitCode 'WP04_VERIFY_WEBAPP_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Web App read-back failed.' }

Write-Host '=== NON-SECRET PERSISTENCE SETTINGS ==='
& az webapp config appsettings list --name $WebAppName --resource-group $ResourceGroup --query "[?name=='WEBSITES_ENABLE_APP_SERVICE_STORAGE' || name=='Persistence__DatabasePath' || name=='Persistence__CreateParentDirectoryForInitialization'].{name:name,value:value}" --output json
Write-ExitCode 'WP04_VERIFY_PERSISTENCE_SETTINGS_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Persistent SQLite settings read-back failed.' }

Write-Host '=== OWNED RESOURCE INVENTORY ==='
& az resource list --resource-group $ResourceGroup --query "[].{name:name,type:type,location:location,kind:kind,sku:sku.name}" --output json
Write-ExitCode 'WP04_VERIFY_RESOURCE_INVENTORY_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Resource inventory read-back failed.' }

Write-Host 'WP04_VERIFY_BOUNDARY=This script reads deployment configuration only; application-owned SQLite initialization, data update, recovery, and integrity require separate governed execution evidence.'
