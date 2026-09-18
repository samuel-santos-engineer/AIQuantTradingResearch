Set-Location C:\projects\github\AIQuantTradingResearch
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass -Force

$ErrorActionPreference = 'Stop'
$branch = 'release/1.12-wp04-persistent-sqlite'
$expectedCommit = '579bbbe3f24de13f87c9e94c9b480e029e70e709'
$expectedDigest = 'sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03'
$resourceGroup = 'rg-aiq-r112-wp03-wcus-5ec325382770'
$webAppName = 'aiqr112wp035ec325382770'
$helper = Join-Path $PWD 'eng\azure-cli\r1.12-deployment\wp04-persistent-sqlite\verify-persistent-sqlite-webapp.ps1'
$tempSettingNames = @(
    'Worker__Mode',
    'PersistentSqliteQualification__Phase',
    'PersistentSqliteQualification__RunId',
    'PersistentSqliteQualification__EvidenceOutputPath',
    'PersistentSqliteQualification__HttpEvidenceEnabled',
    'PersistentSqliteQualification__HttpEvidenceToken'
)

if ($PSVersionTable.PSVersion.Major -ne 5) {
    throw 'Windows PowerShell 5.1 is required.'
}

$remoteTip = (git ls-remote --heads origin "refs/heads/$branch" | ForEach-Object { ($_ -split "`t")[0] })
if ($LASTEXITCODE -ne 0 -or $remoteTip -ne $expectedCommit) {
    throw 'Governed remote branch tip mismatch.'
}

$staged = @(git diff --cached --name-only)
if ($staged.Count -ne 0) {
    throw 'Staging must be empty.'
}

$helperContent = Get-Content -Raw -LiteralPath $helper
$tokens = $null
$parseErrors = $null
[void][System.Management.Automation.Language.Parser]::ParseFile(
    (Resolve-Path -LiteralPath $helper),
    [ref]$tokens,
    [ref]$parseErrors
)
if ($parseErrors.Count -ne 0 -or
    $helperContent -notmatch '\[System\.Diagnostics\.Stopwatch\]::StartNew\(\)' -or
    $helperContent -notmatch '\$pollBudgetSeconds\s*=\s*180' -or
    $helperContent -notmatch '\$statusCode -eq 404 -or \$statusCode -eq 503' -or
    $helperContent -match 'RandomNumberGenerator\]::Fill') {
    throw 'Committed helper contract precheck failed.'
}

$site = az webapp show --resource-group $resourceGroup --name $webAppName `
    --query '{state:state,availabilityState:availabilityState}' --output json | ConvertFrom-Json
$imageSetting = az webapp config show --resource-group $resourceGroup --name $webAppName `
    --query linuxFxVersion --output tsv
if ($LASTEXITCODE -ne 0) {
    throw 'Unable to read the App Service Linux container image reference.'
}
$settings = az webapp config appsettings list --resource-group $resourceGroup --name $webAppName `
    --output json | ConvertFrom-Json
$subscriptionId = az account show --query id --output tsv
if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($subscriptionId)) {
    throw 'Unable to resolve the active Azure subscription.'
}

$publishingPolicies = az rest --method GET --url "https://management.azure.com/subscriptions/$subscriptionId/resourceGroups/$resourceGroup/providers/Microsoft.Web/sites/$webAppName/basicPublishingCredentialsPolicies?api-version=2024-04-01" --output json | ConvertFrom-Json
if ($LASTEXITCODE -ne 0 -or $null -eq $publishingPolicies.value) {
    throw 'Unable to read App Service basic publishing credential policies.'
}

$scmPolicy = @($publishingPolicies.value | Where-Object name -eq 'scm' | Select-Object -First 1)
$ftpPolicy = @($publishingPolicies.value | Where-Object name -eq 'ftp' | Select-Object -First 1)
if ($scmPolicy.Count -ne 1 -or $ftpPolicy.Count -ne 1) {
    throw 'App Service basic publishing credential policy collection was incomplete.'
}

$scmAllow = ([bool]$scmPolicy[0].properties.allow).ToString().ToLowerInvariant()
$ftpAllow = ([bool]$ftpPolicy[0].properties.allow).ToString().ToLowerInvariant()

$tempSettingCount = @($settings | Where-Object Name -in $tempSettingNames).Count
$registryCredentialCount = @($settings | Where-Object {
    $_.Name -in @('DOCKER_REGISTRY_SERVER_USERNAME', 'DOCKER_REGISTRY_SERVER_PASSWORD')
}).Count
$imageDigestMatches = -not [string]::IsNullOrWhiteSpace($imageSetting) -and $imageSetting -match [regex]::Escape($expectedDigest)

Write-Host "WP04_D3_PRECHECK_RUNTIME_STATE=$($site.state)/$($site.availabilityState)"
Write-Host "WP04_D3_PRECHECK_CONTAINER_IMAGE_REFERENCE=$imageSetting"
Write-Host "WP04_D3_PRECHECK_IMAGE_DIGEST_MATCH=$imageDigestMatches"
Write-Host "WP04_D3_PRECHECK_SCM_BASIC_AUTH_ALLOW=$scmAllow"
Write-Host "WP04_D3_PRECHECK_FTP_BASIC_AUTH_ALLOW=$ftpAllow"
Write-Host "WP04_D3_PRECHECK_TEMPORARY_SETTING_COUNT=$tempSettingCount"
Write-Host "WP04_D3_PRECHECK_REGISTRY_CREDENTIAL_COUNT=$registryCredentialCount"

if ($site.state -ne 'Running' -or $site.availabilityState -ne 'Normal' -or
    -not $imageDigestMatches -or
    $scmAllow -ne 'false' -or $ftpAllow -ne 'false' -or
    $tempSettingCount -ne 0 -or $registryCredentialCount -ne 0) {
    throw 'Azure precheck drift detected.'
}

$runId = 'initialize-' + [guid]::NewGuid().ToString('N')
$forbiddenRunIds = @(
    'initialize-dd7679f16cef4765a9c9d12f4b72bf76',
    'initialize-3dab96f04cb84902b64df269ebae3459',
    'initialize-50a6837f6cb649ed98481a3c26831961'
)
if ($runId -in $forbiddenRunIds) {
    throw 'Fresh RunId generation failed.'
}

Write-Host 'RELEASE 1.12 WP04 — AZURE HARD-DEADLINE INITIALIZE PRECHECK: PASS'
Write-Host 'RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 HARD-DEADLINE QUALIFICATION RUNTIME: PASS'
Write-Host "WP04_D3_INITIALIZE_RUN_ID=$runId"

$stdout = Join-Path $env:TEMP ('wp04-initialize-' + [guid]::NewGuid().ToString('N') + '.out')
$stderr = Join-Path $env:TEMP ('wp04-initialize-' + [guid]::NewGuid().ToString('N') + '.err')
$pollStopwatch = [System.Diagnostics.Stopwatch]::StartNew()

try {
    $process = Start-Process -FilePath powershell.exe -WindowStyle Hidden -PassThru -Wait `
        -RedirectStandardOutput $stdout -RedirectStandardError $stderr `
        -ArgumentList @(
            '-NoProfile', '-ExecutionPolicy', 'Bypass', '-File', $helper,
            '-ResourceGroup', $resourceGroup,
            '-WebAppName', $webAppName,
            '-Phase', 'initialize',
            '-RunId', $runId,
            '-LifecycleAction', 'Restart'
        )

    $safeLines = Get-Content -LiteralPath $stdout -ErrorAction SilentlyContinue |
        Where-Object { $_ -match '^(WP04_|{)' }
    $safeLines | ForEach-Object { Write-Host $_ }

    Write-Host "WP04_D3_INITIALIZE_HELPER_PROCESS_EXIT_CODE=$($process.ExitCode)"
}
finally {
    $pollStopwatch.Stop()
    Remove-Item -LiteralPath $stdout, $stderr -Force -ErrorAction SilentlyContinue
}

$finalSite = az webapp show --resource-group $resourceGroup --name $webAppName `
    --query '{state:state,availabilityState:availabilityState}' --output json | ConvertFrom-Json
$finalSettings = az webapp config appsettings list --resource-group $resourceGroup --name $webAppName `
    --output json | ConvertFrom-Json
$finalImage = az webapp config show --resource-group $resourceGroup --name $webAppName `
    --query linuxFxVersion --output tsv
if ($LASTEXITCODE -ne 0) {
    throw 'Unable to read the final App Service Linux container image reference.'
}
$finalTempCount = @($finalSettings | Where-Object Name -in $tempSettingNames).Count
$finalRegistryCredentialCount = @($finalSettings | Where-Object {
    $_.Name -in @('DOCKER_REGISTRY_SERVER_USERNAME', 'DOCKER_REGISTRY_SERVER_PASSWORD')
}).Count

Write-Host "WP04_D3_POLL_ELAPSED_SECONDS=$([math]::Round($pollStopwatch.Elapsed.TotalSeconds, 3))"
Write-Host "WP04_D3_FINAL_TEMPORARY_SETTING_COUNT=$finalTempCount"
Write-Host "WP04_D3_FINAL_REGISTRY_CREDENTIAL_COUNT=$finalRegistryCredentialCount"
Write-Host "WP04_D3_FINAL_RUNTIME_STATE=$($finalSite.state)/$($finalSite.availabilityState)"
Write-Host "WP04_D3_FINAL_IMAGE_MATCH=$($finalImage -match [regex]::Escape($expectedDigest))"
Write-Host "WP04_D3_FINAL_SCM_BASIC_AUTH_ALLOW=$scmAllow"
Write-Host "WP04_D3_FINAL_FTP_BASIC_AUTH_ALLOW=$ftpAllow"
