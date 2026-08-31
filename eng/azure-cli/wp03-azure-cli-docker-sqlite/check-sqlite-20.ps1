$rg = 'rg-aiq-wp03-wcus-3d25217bd701'
$app = 'aiqwp03wcus3d25217bd701'
$baseUrl = "https://$app.azurewebsites.net"

Write-Host '=== WEB APP DEPLOYMENT STATE ==='
az webapp show `
  --resource-group $rg `
  --name $app `
  --query '{state:state,availabilityState:availabilityState,enabled:enabled,defaultHostName:defaultHostName,httpsOnly:httpsOnly,linuxFxVersion:siteConfig.linuxFxVersion}' `
  --output json
Write-Host "WP03_R4_APP_SHOW_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== CONTAINER CONFIGURATION ==='
az webapp config container show `
  --resource-group $rg `
  --name $app `
  --output json
Write-Host "WP03_R4_CONTAINER_SHOW_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== RELEVANT APP SETTINGS ONLY ==='
az webapp config appsettings list `
  --resource-group $rg `
  --name $app `
  --query "[?name=='WEBSITES_ENABLE_APP_SERVICE_STORAGE' || name=='WEBSITES_PORT' || name=='SQLITE_PATH' || name=='PROBE_REVISION'].{name:name,value:value}" `
  --output json
Write-Host "WP03_R4_SETTINGS_READBACK_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== HTTP RESPONSE HEADERS ==='
curl.exe --silent --show-error --include --max-time 30 "$baseUrl/health"
Write-Host "WP03_R4_HEALTH_CURL_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== DEPLOYMENT HISTORY ==='
az webapp log deployment list `
  --resource-group $rg `
  --name $app `
  --output json
Write-Host "WP03_R4_DEPLOYMENT_HISTORY_EXIT_CODE=$LASTEXITCODE"