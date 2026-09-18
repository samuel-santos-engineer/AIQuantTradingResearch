Set-Location C:\projects\github\AIQuantTradingResearch

$resourceGroup = 'rg-aiq-r112-wp03-wcus-5ec325382770'
$webAppName = 'aiqr112wp035ec325382770'
$planName = 'asp-aiq-r112-wp03-wcus-5ec325382770'

$account = az account show --output json | ConvertFrom-Json
$subscriptionId = $account.id
$resourceId = "/subscriptions/$subscriptionId/resourceGroups/$resourceGroup/providers/Microsoft.Web/sites/$webAppName"

Write-Host "WP04_RO_POWERSHELL=$($PSVersionTable.PSVersion)"
Write-Host "=== ACCOUNT ==="
$account | Select-Object name,id,state,isDefault | ConvertTo-Json -Compress

Write-Host "=== WEB APP ==="
$webApp = az webapp show --resource-group $resourceGroup --name $webAppName `
  --query '{state:state,availabilityState:availabilityState,location:location,defaultHostName:defaultHostName,httpsOnly:httpsOnly,linuxFxVersion:siteConfig.linuxFxVersion,alwaysOn:siteConfig.alwaysOn,healthCheckPath:siteConfig.healthCheckPath}' `
  --output json
Write-Output $webApp
Write-Host "WP04_RO_WEBAPP_EXIT_CODE=$LASTEXITCODE"

Write-Host "=== PLAN ==="
az appservice plan show --resource-group $resourceGroup --name $planName `
  --query '{sku:sku.name,tier:sku.tier,location:location}' --output json
Write-Host "WP04_RO_PLAN_EXIT_CODE=$LASTEXITCODE"

Write-Host "=== NON-SECRET APP SETTINGS ==="
az webapp config appsettings list --resource-group $resourceGroup --name $webAppName `
  --query "[?name=='WEBSITES_PORT' || name=='WEBSITES_ENABLE_APP_SERVICE_STORAGE' || name=='DOCKER_REGISTRY_SERVER_USERNAME' || name=='DOCKER_REGISTRY_SERVER_PASSWORD'].{name:name,present:!contains(value,'')}" `
  --output json
Write-Host "WP04_RO_SETTINGS_EXIT_CODE=$LASTEXITCODE"

Write-Host "=== SCM / FTP BASIC AUTH ==="
az rest --method GET --url "https://management.azure.com$resourceId/basicPublishingCredentialsPolicies/scm?api-version=2024-04-01" `
  --query 'properties.allow' --output tsv
Write-Host "WP04_RO_SCM_AUTH_EXIT_CODE=$LASTEXITCODE"
az rest --method GET --url "https://management.azure.com$resourceId/basicPublishingCredentialsPolicies/ftp?api-version=2024-04-01" `
  --query 'properties.allow' --output tsv
Write-Host "WP04_RO_FTP_AUTH_EXIT_CODE=$LASTEXITCODE"

Write-Host "=== DIAGNOSTIC SETTINGS ==="
az monitor diagnostic-settings list --resource $resourceId --output json
Write-Host "WP04_RO_DIAGNOSTIC_SETTINGS_EXIT_CODE=$LASTEXITCODE"

Write-Host "=== WEB APP LOG CONFIGURATION ==="
az rest --method GET --url "https://management.azure.com$resourceId/config/logs?api-version=2024-04-01" --output json
Write-Host "WP04_RO_LOG_CONFIG_EXIT_CODE=$LASTEXITCODE"

Write-Host "=== ACTIVITY LOG: LAST 7 DAYS ==="
az monitor activity-log list --resource-id $resourceId --offset 7d --max-events 50 `
  --query "[].{time:eventTimestamp,operation:operationName.localizedValue,status:status.localizedValue,subStatus:subStatus.localizedValue}" `
  --output json
Write-Host "WP04_RO_ACTIVITY_LOG_EXIT_CODE=$LASTEXITCODE"

$hostName = az webapp show --resource-group $resourceGroup --name $webAppName --query defaultHostName --output tsv
if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($hostName)) {
    throw 'Could not resolve the normal public hostname.'
}

Write-Host "=== DNS ==="
Resolve-DnsName -Name $hostName -Type A | Select-Object Name,Type,IPAddress | ConvertTo-Json -Compress
Write-Host "WP04_RO_DNS_EXIT_CODE=$LASTEXITCODE"

Write-Host "=== TLS ==="
$tcpClient = New-Object System.Net.Sockets.TcpClient
try {
    $tcpClient.Connect($hostName, 443)
    $sslStream = New-Object System.Net.Security.SslStream($tcpClient.GetStream(), $false, ({ $true }))
    $sslStream.AuthenticateAsClient($hostName)
    $certificate = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2($sslStream.RemoteCertificate)
    [pscustomobject]@{
        Subject = $certificate.Subject
        Issuer = $certificate.Issuer
        NotAfter = $certificate.NotAfter
        Protocol = $sslStream.SslProtocol.ToString()
    } | ConvertTo-Json -Compress
    Write-Host 'WP04_RO_TLS_HANDSHAKE=PASS'
}
finally {
    if ($sslStream) { $sslStream.Dispose() }
    $tcpClient.Dispose()
}

Write-Host "=== NORMAL FRONT DOOR ROOT ==="
try {
    $response = Invoke-WebRequest -Uri "https://$hostName/" -UseBasicParsing -TimeoutSec 20
    Write-Host "WP04_RO_NORMAL_HTTP_STATUS=$([int]$response.StatusCode)"
    Write-Host 'WP04_RO_NORMAL_FRONT_DOOR=PASS'
}
catch {
    $status = $_.Exception.Response.StatusCode.value__
    if ($status) { Write-Host "WP04_RO_NORMAL_HTTP_STATUS=$status" }
    Write-Host 'WP04_RO_NORMAL_FRONT_DOOR=FAIL'
}