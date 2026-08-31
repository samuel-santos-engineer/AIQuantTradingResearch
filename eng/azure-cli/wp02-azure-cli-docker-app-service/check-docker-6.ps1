$ErrorActionPreference = 'Continue'

$runId = [guid]::NewGuid().ToString('N')
$tempRoot = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$probeRoot = Join-Path $tempRoot "aiq-azure-f1-wp02-$runId"
$imageTag = "aiq-azure-f1-probe:wp02-r1-$runId"
$containerName = "aiq-azure-f1-wp02-$runId"
$baseImage = 'python:3.13-alpine'
$marker = "wp02-$runId"
$scriptExit = 0

try {
    $baseImageId = (& docker image ls --quiet $baseImage)
    $baseWasPresent = -not [string]::IsNullOrWhiteSpace($baseImageId)
    Write-Host "DOCKER_BASE_IMAGE_PREEXISTING=$baseWasPresent"

    New-Item -ItemType Directory -Path $probeRoot -ErrorAction Stop | Out-Null

    @'
import json
import os
import re
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from urllib.parse import parse_qs, urlparse

MARKER_PATH = "/home/aiq-wp02.marker"
REVISION = os.environ.get("PROBE_REVISION", "unset")

def state():
    marker = None
    if os.path.exists(MARKER_PATH):
        with open(MARKER_PATH, "r", encoding="utf-8") as marker_file:
            marker = marker_file.read().strip()
    return {"service": "aiq-azure-f1-wp02", "revision": REVISION, "marker": marker}

class Handler(BaseHTTPRequestHandler):
    def respond(self, code, payload):
        body = json.dumps(payload, sort_keys=True).encode("utf-8")
        self.send_response(code)
        self.send_header("Content-Type", "application/json")
        self.send_header("Content-Length", str(len(body)))
        self.end_headers()
        self.wfile.write(body)

    def do_GET(self):
        path = urlparse(self.path).path
        if path == "/healthz":
            self.respond(200, {"status": "ok"})
        elif path == "/state":
            self.respond(200, state())
        else:
            self.respond(404, {"error": "not-found"})

    def do_POST(self):
        parsed = urlparse(self.path)
        if parsed.path != "/marker":
            self.respond(404, {"error": "not-found"})
            return
        marker = parse_qs(parsed.query).get("value", [""])[0]
        if not re.fullmatch(r"[A-Za-z0-9-]{1,128}", marker):
            self.respond(400, {"error": "invalid-marker"})
            return
        os.makedirs("/home", exist_ok=True)
        with open(MARKER_PATH, "w", encoding="utf-8") as marker_file:
            marker_file.write(marker)
        self.respond(200, state())

    def log_message(self, format, *args):
        pass

ThreadingHTTPServer(("0.0.0.0", 8080), Handler).serve_forever()
'@ | Set-Content -LiteralPath (Join-Path $probeRoot 'server.py') -Encoding utf8 -ErrorAction Stop

    @'
FROM python:3.13-alpine
WORKDIR /app
COPY server.py /app/server.py
ENV PROBE_REVISION=wp02-r1
EXPOSE 8080
CMD ["python", "/app/server.py"]
'@ | Set-Content -LiteralPath (Join-Path $probeRoot 'Dockerfile') -Encoding ascii -ErrorAction Stop

    & docker build --tag $imageTag $probeRoot
    Write-Host "DOCKER_WP02_PROBE_BUILD_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'WP02 probe image build failed.' }

    & docker image inspect --format '{{.Os}}|{{.Architecture}}|{{.Config.Env}}' $imageTag
    Write-Host "DOCKER_WP02_PROBE_IMAGE_INSPECT_EXIT_CODE=$LASTEXITCODE"

    & docker run -d --name $containerName -p 127.0.0.1:18080:8080 -e PROBE_REVISION=wp02-r1 $imageTag
    Write-Host "DOCKER_WP02_PROBE_RUN_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'WP02 probe container start failed.' }

    Start-Sleep -Seconds 2

    $health = Invoke-WebRequest -UseBasicParsing 'http://127.0.0.1:18080/healthz'
    Write-Host "LOCAL_PROBE_HEALTH_STATUS_CODE=$($health.StatusCode)"
    Write-Output $health.Content

    $written = Invoke-RestMethod -Method Post -Uri "http://127.0.0.1:18080/marker?value=$marker"
    Write-Host "LOCAL_PROBE_MARKER_WRITE_RESULT=$($written | ConvertTo-Json -Compress)"

    $state = Invoke-RestMethod 'http://127.0.0.1:18080/state'
    Write-Host "LOCAL_PROBE_STATE_RESULT=$($state | ConvertTo-Json -Compress)"
}
catch {
    $scriptExit = 1
    Write-Host "WP02_LOCAL_PROBE_ERROR=$($_.Exception.Message)"
}
finally {
    & docker rm --force $containerName 2>&1
    Write-Host "DOCKER_WP02_PROBE_CONTAINER_REMOVE_EXIT_CODE=$LASTEXITCODE"

    & docker image rm --force $imageTag 2>&1
    Write-Host "DOCKER_WP02_PROBE_IMAGE_REMOVE_EXIT_CODE=$LASTEXITCODE"

    if (-not $baseWasPresent) {
        & docker image rm --force $baseImage 2>&1
        Write-Host "DOCKER_WP02_NEW_BASE_IMAGE_REMOVE_EXIT_CODE=$LASTEXITCODE"
    }
    else {
        Write-Host "DOCKER_WP02_NEW_BASE_IMAGE_REMOVE_EXIT_CODE=NOT_APPLICABLE_PREEXISTING"
    }

    Remove-Item -LiteralPath $probeRoot -Recurse -Force -ErrorAction SilentlyContinue

    $imageAfter = (& docker image ls --quiet $imageTag)
    $containerAfter = (& docker container ls --all --quiet --filter "name=^/$containerName$")
    Write-Host "DOCKER_WP02_PROBE_IMAGE_PRESENT_AFTER_CLEANUP=$(-not [string]::IsNullOrWhiteSpace($imageAfter))"
    Write-Host "DOCKER_WP02_PROBE_CONTAINER_PRESENT_AFTER_CLEANUP=$(-not [string]::IsNullOrWhiteSpace($containerAfter))"
    Write-Host "DOCKER_WP02_TEMP_DIRECTORY_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $probeRoot)"
    Write-Host "WP02_LOCAL_PROBE_SCRIPT_EXIT_CODE=$scriptExit"
}