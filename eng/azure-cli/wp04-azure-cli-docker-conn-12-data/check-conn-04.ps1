$runId = '285950909c3b43b8be1b643f73b28b11'
$localImage = "aiq-wp04-twelve-probe:wp04-r1-$runId"
$remoteImage = "ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp04-r1-$runId"
$tempConfig = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp04-ghcr-$runId"

try {
    docker image inspect $localImage *> $null
    Write-Host "WP04_LOCAL_IMAGE_PRECHECK_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Expected local WP04 image is absent.' }

    docker tag $localImage $remoteImage
    Write-Host "WP04_GHCR_TAG_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'GHCR tagging failed.' }

    docker push $remoteImage
    Write-Host "WP04_GHCR_PUSH_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'GHCR push failed.' }

    New-Item -ItemType Directory -Path $tempConfig -Force | Out-Null
    docker --config $tempConfig manifest inspect --verbose $remoteImage *> $null
    Write-Host "WP04_GHCR_ANONYMOUS_MANIFEST_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Anonymous GHCR manifest check failed.' }

    Write-Host "WP04_REMOTE_IMAGE=$remoteImage"
}
finally {
    if (Test-Path -LiteralPath $tempConfig) {
        Remove-Item -LiteralPath $tempConfig -Recurse -Force
    }
    Write-Host "WP04_GHCR_TEMP_CONFIG_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempConfig)"
}