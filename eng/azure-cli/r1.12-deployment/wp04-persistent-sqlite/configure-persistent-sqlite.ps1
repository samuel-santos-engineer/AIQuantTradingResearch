[CmdletBinding()]
param(
    [Parameter(Mandatory)] [ValidatePattern('^[A-Za-z0-9][A-Za-z0-9-]{0,89}$')] [string] $ResourceGroup,
    [Parameter(Mandatory)] [ValidatePattern('^[a-z0-9][a-z0-9-]{1,58}[a-z0-9]$')] [string] $WebAppName,
    [ValidatePattern('^/home/data/aiquant\.db$')] [string] $DatabasePath = '/home/data/aiquant.db'
)

$ErrorActionPreference = 'Stop'
function Write-ExitCode([string] $Name) { Write-Host "$Name=$LASTEXITCODE" }

Write-Host '=== ACCOUNT PRECHECK ==='
& az account show --query '{name:name,state:state,isDefault:isDefault}' --output json
Write-ExitCode 'WP04_AZ_ACCOUNT_READ_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Azure authentication is required in the interactive operator session.' }

Write-Host '=== APPLY PERSISTENT SQLITE INITIALIZATION CONFIGURATION ONLY ==='
& az webapp config appsettings set --name $WebAppName --resource-group $ResourceGroup --settings "Persistence__DatabasePath=$DatabasePath" 'Persistence__CreateParentDirectoryForInitialization=true' --output none
Write-ExitCode 'WP04_PERSISTENCE_INITIALIZATION_SETTINGS_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Persistent SQLite path configuration failed.' }

Write-Host '=== RESTART FOR CONFIGURATION APPLICATION ==='
& az webapp restart --name $WebAppName --resource-group $ResourceGroup --output none
Write-ExitCode 'WP04_PERSISTENCE_PATH_RESTART_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Web App restart after persistent SQLite path configuration failed.' }

Write-Host 'WP04_PERSISTENCE_DATABASE_PATH=/home/data/aiquant.db'
Write-Host 'WP04_PERSISTENCE_CREATE_PARENT_DIRECTORY_FOR_INITIALIZATION=True'
Write-Host 'WP04_BOUNDARY=No secrets, provider requests, schema changes, or domain-row writes are performed by this script.'
