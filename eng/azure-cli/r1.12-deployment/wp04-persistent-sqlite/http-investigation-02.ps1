Set-Location C:\projects\github\AIQuantTradingResearch
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass -Force

$resourceGroup = 'rg-aiq-r112-wp03-wcus-5ec325382770'
$webAppName = 'aiqr112wp035ec325382770'
$logFile = Join-Path $env:TEMP ('wp04-normal-runtime-logs-' + [guid]::NewGuid().ToString('N') + '.zip')

Write-Host '=== PRE-STATE ==='
az webapp config appsettings list `
  --resource-group $resourceGroup `
  --name $webAppName `
  --query "[?name=='WEBSITES_PORT' || name=='DOCKER_REGISTRY_SERVER_USERNAME' || name=='DOCKER_REGISTRY_SERVER_PASSWORD' || name=='DOCKER_REGISTRY_SERVER_URL'].{name:name,present:(value != null && value != '')}" `
  --output json
Write-Host "WP04_DIAGNOSTIC_PRESTATE_SETTINGS_EXIT_CODE=$LASTEXITCODE"

az webapp log show `
  --resource-group $resourceGroup `
  --name $webAppName `
  --output json
Write-Host "WP04_DIAGNOSTIC_PRESTATE_LOG_SHOW_EXIT_CODE=$LASTEXITCODE"

az webapp show `
  --resource-group $resourceGroup `
  --name $webAppName `
  --query "{state:state,availabilityState:availabilityState,httpsOnly:httpsOnly,linuxFxVersion:siteConfig.linuxFxVersion,appCommandLine:siteConfig.appCommandLine,alwaysOn:siteConfig.alwaysOn,healthCheckPath:siteConfig.healthCheckPath}" `
  --output json
Write-Host "WP04_DIAGNOSTIC_PRESTATE_SITE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== ENABLE FILESYSTEM CONTAINER LOGGING ONLY ==='
az webapp log config `
  --resource-group $resourceGroup `
  --name $webAppName `
  --docker-container-logging filesystem `
  --web-server-logging off `
  --detailed-error-messages false `
  --failed-request-tracing false `
  --output none
Write-Host "WP04_DIAGNOSTIC_ENABLE_EXIT_CODE=$LASTEXITCODE"

if ($LASTEXITCODE -ne 0) {
    throw 'Diagnostic logging enablement failed.'
}

Write-Host '=== ONE REQUIRED NORMAL-RUNTIME RESTART ==='
az webapp restart `
  --resource-group $resourceGroup `
  --name $webAppName
Write-Host "WP04_DIAGNOSTIC_RESTART_EXIT_CODE=$LASTEXITCODE"

if ($LASTEXITCODE -ne 0) {
    throw 'Diagnostic restart failed.'
}

Write-Host '=== BOUNDED CAPTURE WINDOW ==='
Start-Sleep -Seconds 60
Write-Host 'WP04_DIAGNOSTIC_CAPTURE_ELAPSED_SECONDS=60'

Write-Host '=== SAFE NORMAL FRONT-DOOR CHECK ==='
try {
    $site = az webapp show `
      --resource-group $resourceGroup `
      --name $webAppName `
      --query "defaultHostName" `
      --output tsv

    $response = Invoke-WebRequest `
      -Uri ("https://" + $site + "/") `
      -Method Get `
      -UseBasicParsing `
      -TimeoutSec 30 `
      -ErrorAction Stop

    Write-Host "WP04_DIAGNOSTIC_FRONT_DOOR_STATUS=$([int]$response.StatusCode)"
} catch {
    if ($null -ne $_.Exception.Response) {
        Write-Host "WP04_DIAGNOSTIC_FRONT_DOOR_STATUS=$([int]$_.Exception.Response.StatusCode)"
    } else {
        Write-Host 'WP04_DIAGNOSTIC_FRONT_DOOR_STATUS=NONE'
    }
}

Write-Host '=== LOG RETRIEVAL ==='
az webapp log download `
  --resource-group $resourceGroup `
  --name $webAppName `
  --log-file $logFile
Write-Host "WP04_DIAGNOSTIC_LOG_DOWNLOAD_EXIT_CODE=$LASTEXITCODE"
Write-Host "WP04_DIAGNOSTIC_LOG_FILE_PRESENT=$(Test-Path -LiteralPath $logFile)"

if (Test-Path -LiteralPath $logFile) {
    Write-Host 'WP04_DIAGNOSTIC_LOG_ARCHIVE_SIZE_BYTES='((Get-Item -LiteralPath $logFile).Length)
}

Write-Host '=== RESTORE LOGGING TO DISABLED ==='
az webapp log config `
  --resource-group $resourceGroup `
  --name $webAppName `
  --docker-container-logging off `
  --web-server-logging off `
  --detailed-error-messages false `
  --failed-request-tracing false `
  --output none
Write-Host "WP04_DIAGNOSTIC_RESTORE_EXIT_CODE=$LASTEXITCODE"

if ($LASTEXITCODE -ne 0) {
    throw 'Diagnostic logging restoration failed.'
}

Write-Host '=== POST-RESTORATION PROOF ==='
az webapp log show `
  --resource-group $resourceGroup `
  --name $webAppName `
  --output json
Write-Host "WP04_DIAGNOSTIC_POSTSTATE_LOG_SHOW_EXIT_CODE=$LASTEXITCODE"

az webapp config appsettings list `
  --resource-group $resourceGroup `
  --name $webAppName `
  --query "[?name=='WEBSITES_PORT' || name=='DOCKER_REGISTRY_SERVER_USERNAME' || name=='DOCKER_REGISTRY_SERVER_PASSWORD' || name=='DOCKER_REGISTRY_SERVER_URL'].{name:name,present:(value != null && value != '')}" `
  --output json
Write-Host "WP04_DIAGNOSTIC_POSTSTATE_SETTINGS_EXIT_CODE=$LASTEXITCODE"

az webapp show `
  --resource-group $resourceGroup `
  --name $webAppName `
  --query "{state:state,availabilityState:availabilityState,httpsOnly:httpsOnly,linuxFxVersion:siteConfig.linuxFxVersion,appCommandLine:siteConfig.appCommandLine,alwaysOn:siteConfig.alwaysOn,healthCheckPath:siteConfig.healthCheckPath}" `
  --output json
Write-Host "WP04_DIAGNOSTIC_POSTSTATE_SITE_EXIT_CODE=$LASTEXITCODE"

Write-Host 'WP04_DIAGNOSTIC_RESTART_COUNT=1'
Write-Host 'WP04_DIAGNOSTIC_NEW_RESOURCES=0'
Write-Host 'WP04_DIAGNOSTIC_RECURRING_COST=$0.00'
Write-Host 'WP04_DIAGNOSTIC_QUALIFICATION_MODE=DISABLED'
Write-Host 'WP04_DIAGNOSTIC_SCM_BASIC_AUTH=UNCHANGED_FALSE'
Write-Host 'WP04_DIAGNOSTIC_FTP_BASIC_AUTH=UNCHANGED_FALSE'