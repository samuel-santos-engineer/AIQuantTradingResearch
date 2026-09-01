[CmdletBinding()]
param(
    [Parameter(Mandatory)] [ValidatePattern('^[A-Za-z0-9][A-Za-z0-9-]{0,89}$')] [string] $ResourceGroup,
    [Parameter(Mandatory)] [ValidatePattern('^[A-Za-z0-9-]{1,60}$')] [string] $PlanName,
    [Parameter(Mandatory)] [ValidatePattern('^[a-z0-9][a-z0-9-]{1,58}[a-z0-9]$')] [string] $WebAppName,
    [Parameter(Mandatory)] [ValidatePattern('^ghcr\.io/.+@sha256:[a-f0-9]{64}$')] [string] $Image,
    [string] $Location = 'westcentralus'
)

$ErrorActionPreference = 'Stop'
if ($Location -ne 'westcentralus') { throw 'WP03 is limited to West Central US.' }
function Write-ExitCode([string] $Name) { Write-Host "$Name=$LASTEXITCODE" }

Write-Host '=== ACCOUNT / TARGET PRECHECK ==='
& az account show --query '{name:name,state:state,isDefault:isDefault}' --output json
Write-ExitCode 'WP03_AZ_ACCOUNT_READ_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Azure authentication is required in the interactive operator session.' }

Write-Host '=== CREATE OR RECONCILE OWNED RESOURCE GROUP ==='
& az group create --name $ResourceGroup --location $Location --tags initiative='Release-1.12' wp='WP03' owner='Terra' recurringCost='0' --output none
Write-ExitCode 'WP03_RESOURCE_GROUP_CREATE_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Resource group operation failed.' }

Write-Host '=== CREATE OR RECONCILE LINUX F1 PLAN ==='
& az appservice plan create --name $PlanName --resource-group $ResourceGroup --location $Location --sku F1 --is-linux --output none
Write-ExitCode 'WP03_F1_PLAN_CREATE_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'F1 Linux App Service plan operation failed.' }

Write-Host '=== CREATE OR RECONCILE PUBLIC GHCR WEB APP ==='
& az webapp create --name $WebAppName --resource-group $ResourceGroup --plan $PlanName --container-image-name $Image --https-only true --output none
Write-ExitCode 'WP03_WEBAPP_CREATE_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Web App create operation failed.' }

Write-Host '=== APPLY WP03 DEPLOYMENT SETTINGS ONLY ==='
& az webapp config appsettings set --name $WebAppName --resource-group $ResourceGroup --settings WEBSITES_ENABLE_APP_SERVICE_STORAGE=true WEBSITES_PORT=8501 --output none
Write-ExitCode 'WP03_DEPLOYMENT_SETTINGS_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'WP03 deployment settings operation failed.' }

& az webapp config container set --name $WebAppName --resource-group $ResourceGroup --container-image-name $Image --container-registry-url https://ghcr.io --output none
Write-ExitCode 'WP03_CONTAINER_CONFIGURATION_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Container configuration operation failed.' }

Write-Host 'WP03_DEPLOYMENT_SCOPE=F1_Free_WestCentralUS_PublicGHCR_PersistentHome'
Write-Host 'WP03_SECRET_CONFIGURATION=NOT_IMPLEMENTED_WP05_OWNED'
Write-Host 'WP03_SQLITE_INITIALIZATION=NOT_IMPLEMENTED_WP04_OWNED'
