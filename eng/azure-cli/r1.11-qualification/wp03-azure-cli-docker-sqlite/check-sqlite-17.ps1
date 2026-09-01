$runId = 'e361ba0a4ab546f3bdcf69f033dc419f'
$localImage = "aiq-wp03-sqlite-probe:wp03-r4-$runId"
$remoteImage = "ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r4-$runId"
$tempConfig = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp03-ghcr-r4-$runId"

try {
    docker image inspect $localImage *> $null
    Write-Host "WP03_R4_LOCAL_IMAGE_PRECHECK_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw "Missing local R4 image: $localImage" }

    docker tag $localImage $remoteImage
    Write-Host "WP03_R4_REMOTE_TAG_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw "Failed to tag R4 image." }

    docker push $remoteImage
    Write-Host "WP03_R4_GHCR_PUSH_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw "Failed to push R4 image." }

    New-Item -ItemType Directory -Path $tempConfig -Force | Out-Null
    docker --config $tempConfig manifest inspect --verbose $remoteImage *> $null
    Write-Host "WP03_R4_ANONYMOUS_MANIFEST_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw "Anonymous R4 manifest read-back failed." }

    Write-Host "WP03_R4_REMOTE_IMAGE=$remoteImage"
}
finally {
    if (Test-Path -LiteralPath $tempConfig) {
        Remove-Item -LiteralPath $tempConfig -Recurse -Force
    }
    Write-Host "WP03_R4_TEMP_CONFIG_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempConfig)"
}