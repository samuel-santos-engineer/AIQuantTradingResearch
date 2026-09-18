Set-Location C:\projects\github\AIQuantTradingResearch
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass -Force

$resourceGroup = 'rg-aiq-r112-wp03-wcus-5ec325382770'
$webAppName = 'aiqr112wp035ec325382770'
$expectedImage = 'DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f'
$startTime = (Get-Date).ToUniversalTime().AddHours(-24).ToString('o')

Write-Host "WP04_INVESTIGATION_POWERSHELL=$($PSVersionTable.PSVersion)"

Write-Host '=== ACCOUNT ==='
az account show --query '{name:name,state:state,id:id}' --output json
Write-Host "WP04_INVESTIGATION_ACCOUNT_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== SITE / CONTAINER CONFIGURATION ==='
$site = az webapp show --resource-group $resourceGroup --name $webAppName `
  --query '{state:state,availabilityState:availabilityState,defaultHostName:defaultHostName,httpsOnly:httpsOnly,location:location,linuxFxVersion:siteConfig.linuxFxVersion,alwaysOn:siteConfig.alwaysOn,healthCheckPath:siteConfig.healthCheckPath,appCommandLine:siteConfig.appCommandLine}' `
  --output json | ConvertFrom-Json
$siteExit = $LASTEXITCODE
$site | ConvertTo-Json -Depth 4
Write-Host "WP04_INVESTIGATION_SITE_EXIT_CODE=$siteExit"

$imageIdentity = 'NOT_PROVEN'
if ($siteExit -eq 0 -and -not [string]::IsNullOrWhiteSpace($site.linuxFxVersion)) {
    if ([string]::Equals($site.linuxFxVersion, $expectedImage, [StringComparison]::Ordinal)) {
        $imageIdentity = 'MATCH'
    } else {
        $imageIdentity = 'MISMATCH'
    }
}
$startupOverride = if ($siteExit -ne 0) { 'NOT_PROVEN' } elseif ([string]::IsNullOrWhiteSpace($site.appCommandLine)) { 'NONE' } else { 'PRESENT' }
$appState = if ($siteExit -ne 0) { 'NOT_PROVEN' } elseif ($site.state -eq 'Running') { 'Running' } elseif ($site.state -eq 'Stopped') { 'Stopped' } else { 'Other' }
Write-Host "WP04_INVESTIGATION_IMAGE_IDENTITY=$imageIdentity"
Write-Host "WP04_INVESTIGATION_STARTUP_OVERRIDE=$startupOverride"
Write-Host "WP04_INVESTIGATION_APP_SERVICE_STATE=$appState"

Write-Host '=== SAFE APP-SETTING PRESENCE ==='
$settings = az webapp config appsettings list --resource-group $resourceGroup --name $webAppName --output json | ConvertFrom-Json
$settingsExit = $LASTEXITCODE
Write-Host "WP04_INVESTIGATION_SETTINGS_EXIT_CODE=$settingsExit"

if ($settingsExit -eq 0) {
    $portSetting = @($settings | Where-Object { $_.name -eq 'WEBSITES_PORT' }) | Select-Object -First 1
    $usernameSetting = @($settings | Where-Object { $_.name -eq 'DOCKER_REGISTRY_SERVER_USERNAME' }) | Select-Object -First 1
    $passwordSetting = @($settings | Where-Object { $_.name -eq 'DOCKER_REGISTRY_SERVER_PASSWORD' }) | Select-Object -First 1
    $urlSetting = @($settings | Where-Object { $_.name -eq 'DOCKER_REGISTRY_SERVER_URL' }) | Select-Object -First 1

    $portPresent = $null -ne $portSetting
    $portValue = if (-not $portPresent) { 'NONE' } elseif ($null -eq $portSetting.value) { 'NULL' } elseif ([string]::IsNullOrWhiteSpace([string]$portSetting.value)) { 'EMPTY' } else { [string]$portSetting.value }

    $usernamePresent = ($null -ne $usernameSetting -and -not [string]::IsNullOrWhiteSpace([string]$usernameSetting.value))
    $passwordPresent = ($null -ne $passwordSetting -and -not [string]::IsNullOrWhiteSpace([string]$passwordSetting.value))
    $urlPresent = ($null -ne $urlSetting -and -not [string]::IsNullOrWhiteSpace([string]$urlSetting.value))

    Write-Host "WP04_INVESTIGATION_WEBSITES_PORT_PRESENT=$portPresent"
    Write-Host "WP04_INVESTIGATION_WEBSITES_PORT_VALUE=$portValue"
    Write-Host "WP04_INVESTIGATION_REGISTRY_USERNAME_PRESENT=$usernamePresent"
    Write-Host "WP04_INVESTIGATION_REGISTRY_PASSWORD_PRESENT=$passwordPresent"
    Write-Host "WP04_INVESTIGATION_REGISTRY_URL_PRESENT=$urlPresent"
} else {
    Write-Host 'WP04_INVESTIGATION_WEBSITES_PORT_PRESENT=NOT_PROVEN'
    Write-Host 'WP04_INVESTIGATION_WEBSITES_PORT_VALUE=NOT_PROVEN'
    Write-Host 'WP04_INVESTIGATION_REGISTRY_USERNAME_PRESENT=NOT_PROVEN'
    Write-Host 'WP04_INVESTIGATION_REGISTRY_PASSWORD_PRESENT=NOT_PROVEN'
    Write-Host 'WP04_INVESTIGATION_REGISTRY_URL_PRESENT=NOT_PROVEN'
}

Write-Host '=== PLAN ==='
az appservice plan show --resource-group $resourceGroup --name 'asp-aiq-r112-wp03-wcus-5ec325382770' `
  --query '{name:name,sku:sku.name,tier:sku.tier,location:location,kind:kind}' --output json
Write-Host "WP04_INVESTIGATION_PLAN_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== RESOURCE HEALTH ==='
$subscriptionId = az account show --query id --output tsv
$subscriptionExit = $LASTEXITCODE
if ($subscriptionExit -eq 0 -and -not [string]::IsNullOrWhiteSpace($subscriptionId)) {
    az rest --method get --url "https://management.azure.com/subscriptions/$subscriptionId/resourceGroups/$resourceGroup/providers/Microsoft.Web/sites/$webAppName/providers/Microsoft.ResourceHealth/availabilityStatuses/current?api-version=2023-07-01-preview" --output json
    Write-Host "WP04_INVESTIGATION_RESOURCE_HEALTH_EXIT_CODE=$LASTEXITCODE"
} else {
    Write-Host 'WP04_INVESTIGATION_RESOURCE_HEALTH_EXIT_CODE=NOT_PROVEN'
}

Write-Host '=== RECENT ACTIVITY ==='
az monitor activity-log list --resource-group $resourceGroup --start-time $startTime `
  --query "[?contains(resourceId, 'Microsoft.Web/sites/$webAppName') || contains(resourceId, 'Microsoft.Web/serverfarms/asp-aiq-r112-wp03-wcus-5ec325382770')].{time:eventTimestamp,operation:operationName.value,status:status.value,subStatus:subStatus.value,level:level,correlationId:correlationId}" `
  --output json
Write-Host "WP04_INVESTIGATION_ACTIVITY_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== NORMAL FRONT-DOOR METADATA ==='
if ($siteExit -eq 0 -and -not [string]::IsNullOrWhiteSpace($site.defaultHostName)) {
    $uri = "https://$($site.defaultHostName)/"
    $stopwatch = [Diagnostics.Stopwatch]::StartNew()
    try {
        $response = Invoke-WebRequest -Uri $uri -Method Get -UseBasicParsing -MaximumRedirection 0 -TimeoutSec 30 -ErrorAction Stop
        $status = [int]$response.StatusCode
        $headers = $response.Headers
    } catch {
        $response = $_.Exception.Response
        if ($null -ne $response) {
            $status = [int]$response.StatusCode
            $headers = $response.Headers
        } else {
            $status = 'NONE'
            $headers = $null
        }
    }
    $stopwatch.Stop()

    Write-Host "WP04_INVESTIGATION_HTTP_STATUS=$status"
    Write-Host "WP04_INVESTIGATION_HTTP_DURATION_MS=$($stopwatch.ElapsedMilliseconds)"
    foreach ($headerName in @('Server','Date','Retry-After','x-ms-request-id','x-ms-routing-name','x-arr-log-id','ARRAffinity','ARRAffinitySameSite','Location')) {
        if ($null -ne $headers -and $null -ne $headers[$headerName]) {
            if ($headerName -notin @('ARRAffinity','ARRAffinitySameSite')) {
                Write-Host "WP04_INVESTIGATION_HTTP_HEADER_$($headerName.Replace('-','_'))=$($headers[$headerName])"
            } else {
                Write-Host "WP04_INVESTIGATION_HTTP_HEADER_$($headerName)=PRESENT"
            }
        }
    }
} else {
    Write-Host 'WP04_INVESTIGATION_HTTP_STATUS=NOT_PROVEN'
}