$ErrorActionPreference = 'Stop'

$r2TempRoot = 'C:\Users\sabsf\AppData\Local\Temp\aiq-wp03-r2-3f99757f018b46698e6c89d589867a03'
$runId = [guid]::NewGuid().ToString('N')
$r3TempRoot = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp03-r3-$runId"
$localImage = "aiq-wp03-sqlite-probe:wp03-r3-$runId"
$remoteImage = "ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp03-r3-$runId"
$tempConfig = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp03-ghcr-r3-$runId"

$sourcePath = Join-Path $r2TempRoot 'server.py'
if (-not (Test-Path -LiteralPath $sourcePath)) {
    throw "Required retained WP03 r2 source was not found: $sourcePath"
}

New-Item -ItemType Directory -Path $r3TempRoot -Force | Out-Null

$source = Get-Content -Raw -LiteralPath $sourcePath
$needle = '            elif parsed.path == "/transaction":'

$updateEndpoint = @'
            elif parsed.path == "/update":
                seq = int(query.get("seq", ["0"])[0])
                name = query.get("name", ["updated"])[0]
                con = connect()
                try:
                    con.execute("BEGIN IMMEDIATE")
                    changed = con.execute("UPDATE events SET name = ? WHERE seq = ?", (name, seq)).rowcount
                    if changed != 1:
                        con.execute("ROLLBACK")
                        self.send_json(404, {"result": "not_found", "seq": seq})
                        return
                    con.execute("COMMIT")
                    self.send_json(200, {"result": "updated", "seq": seq, "name": name})
                finally:
                    con.close()
'@

if (-not $source.Contains($needle)) {
    throw 'The exact insertion point for the isolated UPDATE endpoint was not found.'
}

$updatedSource = $source.Replace($needle, $updateEndpoint + $needle)
$updatedSource | Set-Content -LiteralPath (Join-Path $r3TempRoot 'server.py') -Encoding Ascii

@'
FROM python:3.13-alpine
LABEL aiq.probe.revision="wp03-r3"
WORKDIR /app
COPY server.py /app/server.py
EXPOSE 8080
CMD ["python", "/app/server.py"]
'@ | Set-Content -LiteralPath (Join-Path $r3TempRoot 'Dockerfile') -Encoding Ascii

Write-Host "WP03_R3_TEMP_ROOT=$r3TempRoot"
Write-Host "WP03_R3_LOCAL_IMAGE=$localImage"
Write-Host "WP03_R3_REMOTE_IMAGE=$remoteImage"

Write-Host '=== BUILD R3 UPDATE-CAPABLE IMAGE ==='
docker build --progress=plain --tag $localImage $r3TempRoot
Write-Host "WP03_R3_LOCAL_IMAGE_BUILD_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== TAG / PUSH R3 IMAGE ==='
docker tag $localImage $remoteImage
Write-Host "WP03_R3_REMOTE_TAG_EXIT_CODE=$LASTEXITCODE"

docker push $remoteImage
Write-Host "WP03_R3_GHCR_PUSH_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== ANONYMOUS R3 MANIFEST READ-BACK ==='
New-Item -ItemType Directory -Path $tempConfig -Force | Out-Null
try {
    docker --config $tempConfig manifest inspect $remoteImage --verbose
    Write-Host "WP03_R3_ANONYMOUS_MANIFEST_EXIT_CODE=$LASTEXITCODE"
}
finally {
    if (Test-Path -LiteralPath $tempConfig) {
        Remove-Item -LiteralPath $tempConfig -Recurse -Force
    }
    Write-Host "WP03_R3_TEMP_CONFIG_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempConfig)"
}