$rg = 'rg-aiq-wp04-wcus-c1e9a49dadf6'
$plan = 'asp-aiq-wp04-wcus-c1e9a49dadf6'
$app = 'aiqwp04wcusc1e9a49dadf6'
$image = 'aiq-wp04-twelve-probe:wp04-r1-285950909c3b43b8be1b643f73b28b11'
$tempRoot = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp04-final-audit-$([guid]::NewGuid().ToString('N'))"
$logZip = Join-Path $tempRoot 'logs.zip'
$extractRoot = Join-Path $tempRoot 'logs'
$secureKey = $null
$bstr = [IntPtr]::Zero
$plainKey = $null

New-Item -ItemType Directory -Path $tempRoot -Force | Out-Null

try {
    Write-Host '=== APP-SETTING PRESENCE ONLY ==='
    $settings = @(az webapp config appsettings list --resource-group $rg --name $app --output json | ConvertFrom-Json)
    $secretSettingCount = @($settings | Where-Object { $_.name -eq 'TWELVE_DATA_API_KEY' }).Count
    Write-Host "WP04_FINAL_SECRET_SETTING_COUNT=$secretSettingCount"
    Write-Host "WP04_FINAL_SETTINGS_READ_EXIT_CODE=$LASTEXITCODE"
    if ($secretSettingCount -ne 1) { throw 'Expected one secret setting before owned-resource cleanup.' }

    $secureKey = Read-Host -AsSecureString 'Paste the real Twelve Data API key only for local non-printing disclosure scanning; do not share it in chat'
    $bstr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secureKey)
    $plainKey = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($bstr)
    if ([string]::IsNullOrWhiteSpace($plainKey)) { throw 'Empty API key was entered.' }

    Write-Host '=== LOCAL IMAGE HISTORY / METADATA: NON-PRINTING SCAN ==='
    $history = (& docker history --no-trunc $image 2>&1 | Out-String)
    $inspect = (& docker image inspect $image 2>&1 | Out-String)
    Write-Host "WP04_FINAL_IMAGE_HISTORY_EXIT_CODE=$LASTEXITCODE"
    $historyHasKey = $history.Contains($plainKey)
    $inspectHasKey = $inspect.Contains($plainKey)
    $historyHasSettingName = $history.Contains('TWELVE_DATA_API_KEY')
    $inspectHasSettingName = $inspect.Contains('TWELVE_DATA_API_KEY')
    Write-Host "WP04_FINAL_IMAGE_HISTORY_CONTAINS_SECRET=$historyHasKey"
    Write-Host "WP04_FINAL_IMAGE_METADATA_CONTAINS_SECRET=$inspectHasKey"
    Write-Host "WP04_FINAL_IMAGE_HISTORY_CONTAINS_SETTING_NAME=$historyHasSettingName"
    Write-Host "WP04_FINAL_IMAGE_METADATA_CONTAINS_SETTING_NAME=$inspectHasSettingName"
    if ($historyHasKey -or $inspectHasKey -or $historyHasSettingName -or $inspectHasSettingName) {
        throw 'Potential secret disclosure detected in local image evidence.'
    }

    Write-Host '=== APP SERVICE LOG DOWNLOAD: NON-PRINTING SCAN ==='
    az webapp log download --resource-group $rg --name $app --log-file $logZip --output none
    $logDownloadExit = $LASTEXITCODE
    Write-Host "WP04_FINAL_LOG_DOWNLOAD_EXIT_CODE=$logDownloadExit"
    if ($logDownloadExit -ne 0) { throw 'Unable to obtain App Service logs for secret scan.' }

    Expand-Archive -LiteralPath $logZip -DestinationPath $extractRoot -Force
    $logFiles = @(Get-ChildItem -LiteralPath $extractRoot -File -Recurse -ErrorAction SilentlyContinue)
    $secretHits = 0
    $settingNameHits = 0
    foreach ($file in $logFiles) {
        $content = [System.IO.File]::ReadAllText($file.FullName)
        if ($content.Contains($plainKey)) { $secretHits++ }
        if ($content.Contains('TWELVE_DATA_API_KEY')) { $settingNameHits++ }
    }
    Write-Host "WP04_FINAL_LOG_FILE_COUNT=$($logFiles.Count)"
    Write-Host "WP04_FINAL_LOG_SECRET_HIT_FILE_COUNT=$secretHits"
    Write-Host "WP04_FINAL_LOG_SETTING_NAME_HIT_FILE_COUNT=$settingNameHits"
    if ($secretHits -ne 0) { throw 'Potential secret disclosure detected in App Service logs.' }

    Write-Host '=== OWNED RESOURCE INVENTORY ==='
    az resource list --resource-group $rg `
      --query '[].{type:type,name:name,location:location,kind:kind,sku:sku.name}' `
      --output json
    Write-Host "WP04_FINAL_RESOURCE_INVENTORY_EXIT_CODE=$LASTEXITCODE"

    az appservice plan show --resource-group $rg --name $plan `
      --query '{sku:sku.name,tier:sku.tier,kind:kind,location:location}' --output json
    Write-Host "WP04_FINAL_PLAN_READ_EXIT_CODE=$LASTEXITCODE"

    az webapp show --resource-group $rg --name $app `
      --query '{state:state,httpsOnly:httpsOnly,kind:kind,location:location}' --output json
    Write-Host "WP04_FINAL_WEBAPP_READ_EXIT_CODE=$LASTEXITCODE"

    Write-Host 'WP04_FINAL_AUDIT_VALID=True'
}
finally {
    if ($bstr -ne [IntPtr]::Zero) {
        [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($bstr)
    }
    Remove-Variable plainKey -ErrorAction SilentlyContinue
    Remove-Variable secureKey -ErrorAction SilentlyContinue
    Remove-Item -LiteralPath $tempRoot -Recurse -Force -ErrorAction SilentlyContinue
    Write-Host "WP04_FINAL_AUDIT_TEMP_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempRoot)"
}