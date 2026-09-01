$images = @(
  'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp02-r1-93e7a0e5d0954256bf6b4740fc5af238',
  'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r1-86df07f8b09844ddbb8e0501df8a358a',
  'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r2-3f99757f018b46698e6c89d589867a03',
  'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r3-fc006171dbf241b78775e7dd6d5e8a73',
  'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r4-aa549fe26e4b464db2111df3fc0cf793',
  'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r4-e361ba0a4ab546f3bdcf69f033dc419f',
  'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp04-r1-285950909c3b43b8be1b643f73b28b11'
)

$tempConfig = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp05-ghcr-$([guid]::NewGuid().ToString('N'))"
New-Item -ItemType Directory -Path $tempConfig -Force | Out-Null

try {
    $available = 0
    foreach ($image in $images) {
        docker --config $tempConfig manifest inspect $image *> $null
        $exitCode = $LASTEXITCODE
        if ($exitCode -eq 0) { $available++ }
        Write-Host "WP05_GHCR_MANIFEST_EXIT_CODE=$exitCode"
    }
    Write-Host "WP05_GHCR_EXPECTED_ARTIFACT_COUNT=$($images.Count)"
    Write-Host "WP05_GHCR_ANONYMOUS_AVAILABLE_COUNT=$available"

    $groups = @(az group list --output json | ConvertFrom-Json)
    $initiativeGroups = @($groups | Where-Object { $_.name -like 'rg-aiq-wp0[2-5]-*' })
    Write-Host "WP05_FINAL_INITIATIVE_RESOURCE_GROUP_COUNT=$($initiativeGroups.Count)"
    Write-Host "WP05_FINAL_INITIATIVE_RESOURCE_GROUP_READ_EXIT_CODE=$LASTEXITCODE"

    $resources = @(az resource list --tag initiative=INIT-1.11 --output json | ConvertFrom-Json)
    Write-Host "WP05_FINAL_INITIATIVE_TAGGED_RESOURCE_COUNT=$($resources.Count)"
    Write-Host "WP05_FINAL_INITIATIVE_TAGGED_RESOURCE_READ_EXIT_CODE=$LASTEXITCODE"
}
finally {
    Remove-Item -LiteralPath $tempConfig -Recurse -Force -ErrorAction SilentlyContinue
    Write-Host "WP05_GHCR_TEMP_CONFIG_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempConfig)"
}