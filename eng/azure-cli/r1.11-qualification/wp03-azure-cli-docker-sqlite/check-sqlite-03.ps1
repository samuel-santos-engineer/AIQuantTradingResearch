$ErrorActionPreference = 'Stop'

$runId = '86df07f8b09844ddbb8e0501df8a358a'
$localImage = "aiq-wp03-sqlite-probe:wp03-r1-$runId"
$remoteImage = "ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r1-$runId"
$tempConfig = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp03-ghcr-$runId"

Write-Host "WP03_LOCAL_IMAGE=$localImage"
Write-Host "WP03_REMOTE_IMAGE=$remoteImage"

Write-Host '=== CONFIRM LOCAL IMAGE ==='
docker image inspect $localImage --format 'LOCAL_IMAGE_ID={{.Id}}'
Write-Host "WP03_LOCAL_IMAGE_INSPECT_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== TAG FOR EXISTING PUBLIC GHCR PACKAGE ==='
docker tag $localImage $remoteImage
Write-Host "WP03_REMOTE_TAG_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== PUSH WP03 IMAGE ==='
docker push $remoteImage
Write-Host "WP03_GHCR_PUSH_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== ANONYMOUS MANIFEST READ-BACK ==='
New-Item -ItemType Directory -Path $tempConfig -Force | Out-Null
try {
    docker --config $tempConfig manifest inspect $remoteImage --verbose
    Write-Host "WP03_GHCR_ANONYMOUS_MANIFEST_EXIT_CODE=$LASTEXITCODE"
}
finally {
    if (Test-Path -LiteralPath $tempConfig) {
        Remove-Item -LiteralPath $tempConfig -Recurse -Force
    }
    Write-Host "WP03_GHCR_TEMP_CONFIG_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempConfig)"
}