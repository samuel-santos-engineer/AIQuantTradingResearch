Write-Host '=== INITIATIVE-TAGGED AZURE RESOURCE IDENTITY ==='
az resource list --tag initiative=INIT-1.11 `
  --query '[].{name:name,type:type,resourceGroup:resourceGroup,location:location,kind:kind,sku:sku.name,tags:tags}' `
  --output json
Write-Host "WP05_INITIATIVE_TAGGED_RESOURCE_IDENTITY_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== SUPERSEDED R4 TAG AVAILABILITY ==='
$image = 'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r4-aa549fe26e4b464db2111df3fc0cf793'
$tempConfig = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp05-ghcr-one-$([guid]::NewGuid().ToString('N'))"
New-Item -ItemType Directory -Path $tempConfig -Force | Out-Null
try {
    docker --config $tempConfig manifest inspect $image *> $null
    Write-Host "WP05_SUPERSEDED_R4_MANIFEST_EXIT_CODE=$LASTEXITCODE"
}
finally {
    Remove-Item -LiteralPath $tempConfig -Recurse -Force -ErrorAction SilentlyContinue
    Write-Host "WP05_SUPERSEDED_R4_TEMP_CONFIG_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempConfig)"
}