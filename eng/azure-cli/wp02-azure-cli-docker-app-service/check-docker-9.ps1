$imageReference = 'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp02-r1-93e7a0e5d0954256bf6b4740fc5af238'
$tempRoot = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$dockerProbeConfig = Join-Path $tempRoot ("aiq-ghcr-anonymous-" + [guid]::NewGuid().ToString('N'))

try {
    New-Item -ItemType Directory -Path $dockerProbeConfig | Out-Null
    & docker --config $dockerProbeConfig manifest inspect $imageReference *> $null
    Write-Host "GHCR_ANONYMOUS_MANIFEST_INSPECT_EXIT_CODE=$LASTEXITCODE"
}
finally {
    Remove-Item -LiteralPath $dockerProbeConfig -Recurse -Force -ErrorAction SilentlyContinue
    Write-Host "GHCR_ANONYMOUS_TEMP_CONFIG_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $dockerProbeConfig)"
}