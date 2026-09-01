$ErrorActionPreference = 'Stop'

$r1TempRoot = 'C:\Users\sabsf\AppData\Local\Temp\aiq-wp03-86df07f8b09844ddbb8e0501df8a358a'
$runId = [guid]::NewGuid().ToString('N')
$r2TempRoot = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp03-r2-$runId"
$localImage = "aiq-wp03-sqlite-probe:wp03-r2-$runId"
$remoteImage = "ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r2-$runId"
$tempConfig = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp03-ghcr-r2-$runId"

if (-not (Test-Path -LiteralPath (Join-Path $r1TempRoot 'server.py'))) {
    throw "Required retained WP03 r1 source was not found: $r1TempRoot"
}

New-Item -ItemType Directory -Path $r2TempRoot -Force | Out-Null
Copy-Item -LiteralPath (Join-Path $r1TempRoot 'server.py') -Destination (Join-Path $r2TempRoot 'server.py')

@'
FROM python:3.13-alpine
LABEL aiq.probe.revision="wp03-r2"
WORKDIR /app
COPY server.py /app/server.py
EXPOSE 8080
CMD ["python", "/app/server.py"]
'@ | Set-Content -LiteralPath (Join-Path $r2TempRoot 'Dockerfile') -Encoding Ascii

Write-Host "WP03_R2_TEMP_ROOT=$r2TempRoot"
Write-Host "WP03_R2_LOCAL_IMAGE=$localImage"
Write-Host "WP03_R2_REMOTE_IMAGE=$remoteImage"

Write-Host '=== BUILD DISTINCT R2 IMAGE ==='
docker build --progress=plain --tag $localImage $r2TempRoot
Write-Host "WP03_R2_LOCAL_IMAGE_BUILD_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== TAG / PUSH R2 IMAGE ==='
docker tag $localImage $remoteImage
Write-Host "WP03_R2_REMOTE_TAG_EXIT_CODE=$LASTEXITCODE"

docker push $remoteImage
Write-Host "WP03_R2_GHCR_PUSH_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== ANONYMOUS R2 MANIFEST READ-BACK ==='
New-Item -ItemType Directory -Path $tempConfig -Force | Out-Null
try {
    docker --config $tempConfig manifest inspect $remoteImage --verbose
    Write-Host "WP03_R2_ANONYMOUS_MANIFEST_EXIT_CODE=$LASTEXITCODE"
}
finally {
    if (Test-Path -LiteralPath $tempConfig) {
        Remove-Item -LiteralPath $tempConfig -Recurse -Force
    }
    Write-Host "WP03_R2_TEMP_CONFIG_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempConfig)"
}