Set-Location C:\projects\github\AIQuantTradingResearch

$resourceGroup = 'rg-aiq-r112-wp03-wcus-5ec325382770'
$planName = 'asp-aiq-r112-wp03-wcus-5ec325382770'
$webAppName = 'aiqr112wp035ec325382770'
$expectedCommit = 'ef4a5caf4768ab82c66f0e539d92c1631761b500'

Write-Host '=== WP04 CANDIDATE / GIT RECONCILIATION ==='
git rev-parse HEAD
Write-Host "WP04_AZURE_LOCAL_HEAD_EXIT_CODE=$LASTEXITCODE"
git rev-parse origin/release/1.12-wp04-persistent-sqlite
Write-Host "WP04_AZURE_REMOTE_BRANCH_HEAD_EXIT_CODE=$LASTEXITCODE"
git status --short
Write-Host "WP04_AZURE_GIT_STATUS_EXIT_CODE=$LASTEXITCODE"
gh pr list --head release/1.12-wp04-persistent-sqlite --state all `
  --json number,state,isDraft,headRefOid,baseRefName,title --limit 10
Write-Host "WP04_AZURE_PR_RECONCILIATION_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== ACCOUNT / PLAN / APP READ-BACK ==='
az account show --query '{name:name,state:state,isDefault:isDefault}' --output json
Write-Host "WP04_AZURE_ACCOUNT_READ_EXIT_CODE=$LASTEXITCODE"

.\eng\azure-cli\r1.12-deployment\wp03-ghcr-azure-f1\verify-f1-reference.ps1 `
  -ResourceGroup $resourceGroup `
  -PlanName $planName `
  -WebAppName $webAppName
Write-Host "WP04_AZURE_WP03_BOUNDARY_READ_EXIT_CODE=$LASTEXITCODE"

.\eng\azure-cli\r1.12-deployment\wp04-persistent-sqlite\verify-persistent-sqlite.ps1 `
  -ResourceGroup $resourceGroup `
  -WebAppName $webAppName
Write-Host "WP04_AZURE_WP04_PERSISTENCE_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== COST / DEPENDENCY INVENTORY ==='
az resource list --resource-group $resourceGroup `
  --query "[].{name:name,type:type,kind:kind,sku:sku.name,location:location}" `
  --output json
Write-Host "WP04_AZURE_RESOURCE_INVENTORY_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== DEPLOYED IMAGE REFERENCE ONLY ==='
$containerConfigurationJson = az webapp config container show `
  --resource-group $resourceGroup `
  --name $webAppName `
  --output json
$containerReadExitCode = $LASTEXITCODE
Write-Host "WP04_AZURE_IMAGE_REFERENCE_READ_EXIT_CODE=$containerReadExitCode"
if ($containerReadExitCode -ne 0) { throw 'Container configuration read failed.' }
$containerConfiguration = $containerConfigurationJson | ConvertFrom-Json
$imageReference = @(
    $containerConfiguration |
        Where-Object { $_.name -eq 'DOCKER_CUSTOM_IMAGE_NAME' } |
        Select-Object -First 1 -ExpandProperty value
)
Write-Host "WP04_AZURE_DEPLOYED_IMAGE=$imageReference"

Write-Host '=== GHCR ANONYMOUS DEPLOYED-IMAGE READ-BACK ==='
if ([string]::IsNullOrWhiteSpace($imageReference)) {
    throw 'No deployed image reference was returned.'
}

$temporaryDockerConfig = Join-Path ([System.IO.Path]::GetTempPath()) `
    ('aiq-wp04-preflight-' + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $temporaryDockerConfig | Out-Null
$priorDockerConfig = $env:DOCKER_CONFIG

try {
    $env:DOCKER_CONFIG = $temporaryDockerConfig
    $dockerImageReference = $imageReference -replace '^DOCKER\|', ''
    docker manifest inspect $dockerImageReference | Out-Null
    Write-Host "WP04_AZURE_GHCR_ANONYMOUS_MANIFEST_EXIT_CODE=$LASTEXITCODE"
}
finally {
    $env:DOCKER_CONFIG = $priorDockerConfig
    Remove-Item -LiteralPath $temporaryDockerConfig -Recurse -Force
    Write-Host "WP04_AZURE_GHCR_TEMP_CONFIG_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $temporaryDockerConfig)"
}

Write-Host '=== PRE-MUTATION SUMMARY ==='
Write-Host "WP04_AZURE_EXPECTED_CANDIDATE_COMMIT=$expectedCommit"
Write-Host "WP04_AZURE_PREMUTATION_COMPLETE=True"
