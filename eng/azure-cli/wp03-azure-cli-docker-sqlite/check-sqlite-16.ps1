$ErrorActionPreference = 'Stop'

$r3TempRoot = 'C:\Users\sabsf\AppData\Local\Temp\aiq-wp03-r3-fc006171dbf241b78775e7dd6d5e8a73'
$runId = [guid]::NewGuid().ToString('N')
$r4TempRoot = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp03-r4-$runId"
$localImage = "aiq-wp03-sqlite-probe:wp03-r4-$runId"
$containerName = "aiq-wp03-sqlite-r4-$runId"
$port = 18084

$sourcePath = Join-Path $r3TempRoot 'server.py'
if (-not (Test-Path -LiteralPath $sourcePath)) {
    throw "Required retained WP03 r3 source was not found: $sourcePath"
}

New-Item -ItemType Directory -Path $r4TempRoot -Force | Out-Null

$source = Get-Content -Raw -LiteralPath $sourcePath
$bad = '                    con.close()            elif parsed.path == "/transaction":'
$good = "                    con.close()`n            elif parsed.path == `"/transaction`":"

if (-not $source.Contains($bad)) {
    throw 'Expected r3 newline defect was not found; refusing to apply an unverified repair.'
}

$fixed = $source.Replace($bad, $good)

if ($fixed.Contains('con.close()            elif parsed.path')) {
    throw 'The r4 source still contains the known syntax defect.'
}

$fixed | Set-Content -LiteralPath (Join-Path $r4TempRoot 'server.py') -Encoding Ascii

@'
FROM python:3.13-alpine
LABEL aiq.probe.revision="wp03-r4"
WORKDIR /app
COPY server.py /app/server.py
EXPOSE 8080
CMD ["python", "/app/server.py"]
'@ | Set-Content -LiteralPath (Join-Path $r4TempRoot 'Dockerfile') -Encoding Ascii

Write-Host "WP03_R4_TEMP_ROOT=$r4TempRoot"
Write-Host "WP03_R4_LOCAL_IMAGE=$localImage"
Write-Host "WP03_R4_CONTAINER=$containerName"

Write-Host '=== BUILD R4 REPAIRED IMAGE ==='
docker build --progress=plain --tag $localImage $r4TempRoot
Write-Host "WP03_R4_LOCAL_IMAGE_BUILD_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== RUN R4 LOCAL PROBE ==='
docker run --detach --name $containerName `
  --publish "127.0.0.1:${port}:8080" `
  --env PROBE_REVISION=wp03-r4 `
  --env SQLITE_PATH=/home/aiq-wp03/qualification.sqlite3 `
  $localImage
Write-Host "WP03_R4_LOCAL_CONTAINER_RUN_EXIT_CODE=$LASTEXITCODE"

try {
    $baseUrl = "http://127.0.0.1:$port"
    $healthy = $false
    for ($attempt = 1; $attempt -le 24; $attempt++) {
        Write-Host "R4_LOCAL_HEALTH_ATTEMPT=$attempt"
        try {
            $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 10
            $health | ConvertTo-Json -Compress
            if ($health.revision -eq 'wp03-r4') {
                $healthy = $true
                break
            }
        } catch {
            Write-Host "R4_LOCAL_HEALTH_ERROR=$($_.Exception.Message)"
        }
        Start-Sleep -Seconds 2
    }
    Write-Host "WP03_R4_LOCAL_HEALTHY=$healthy"
    if (-not $healthy) { throw 'R4 local health did not succeed.' }

    Write-Host '=== LOCAL BASELINE / UPDATE PROOF ==='
    $baseline = Invoke-RestMethod -Method Post -Uri "$baseUrl/baseline?mode=DELETE&name=wp03-r4-local-baseline" -TimeoutSec 30
    $baseline | ConvertTo-Json -Compress
    Write-Host 'WP03_R4_LOCAL_BASELINE_EXIT_CODE=0'

    $update = Invoke-RestMethod -Method Post -Uri "$baseUrl/update?seq=1&name=wp03-r4-local-updated" -TimeoutSec 30
    $update | ConvertTo-Json -Compress
    Write-Host 'WP03_R4_LOCAL_UPDATE_EXIT_CODE=0'

    $state = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 30
    $state | ConvertTo-Json -Depth 10 -Compress
    Write-Host 'WP03_R4_LOCAL_STATE_EXIT_CODE=0'
}
finally {
    docker rm --force $containerName
    Write-Host "WP03_R4_LOCAL_CONTAINER_REMOVE_EXIT_CODE=$LASTEXITCODE"
}

docker image inspect $localImage --format 'LOCAL_IMAGE_PRESENT={{.Id}}'
Write-Host "WP03_R4_LOCAL_IMAGE_INSPECT_EXIT_CODE=$LASTEXITCODE"
Write-Host "WP03_R4_TEMP_SOURCE_PRESENT=$(Test-Path -LiteralPath $r4TempRoot)"