$ErrorActionPreference = 'Continue'

function Write-Step {
    param([string]$Message)
    Write-Host "[$(Get-Date -Format 'HH:mm:ss')] $Message" -ForegroundColor Cyan
}

$owner = 'samuel-santos-engineer'
$repository = 'AIQuantTradingResearch'
$runId = [guid]::NewGuid().ToString('N')
$revision = "wp02-r1-$runId"
$imageTag = "ghcr.io/$owner/aiq-azure-f1-wp02-probe:$revision"
$baseImage = 'python:3.13-alpine'
$tempRoot = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$probeRoot = Join-Path $tempRoot "aiq-azure-f1-distribution-$runId"
$buildCompleted = $false
$pushCompleted = $false
$scriptExit = 0

try {
    Write-Step 'Signing in to GHCR; enter the PAT only at Docker prompt'
    & docker login ghcr.io -u $owner
    $loginExit = $LASTEXITCODE
    Write-Host "GHCR_LOGIN_EXIT_CODE=$loginExit"
    if ($loginExit -ne 0) { throw 'GHCR login failed.' }

    $baseWasPresent = -not [string]::IsNullOrWhiteSpace((& docker image ls --quiet $baseImage))
    Write-Host "GHCR_DISTRIBUTION_BASE_IMAGE_PREEXISTING=$baseWasPresent"

    Write-Step 'Creating temporary probe source'
    New-Item -ItemType Directory -Path $probeRoot -ErrorAction Stop | Out-Null

    @'
import json, os, re
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from urllib.parse import parse_qs, urlparse

MARKER_PATH = "/home/aiq-wp02.marker"
REVISION = os.environ.get("PROBE_REVISION", "unset")

def state():
    marker = None
    if os.path.exists(MARKER_PATH):
        with open(MARKER_PATH, "r", encoding="utf-8") as f:
            marker = f.read().strip()
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
        if path == "/healthz": self.respond(200, {"status": "ok"})
        elif path == "/state": self.respond(200, state())
        else: self.respond(404, {"error": "not-found"})
    def do_POST(self):
        parsed = urlparse(self.path)
        marker = parse_qs(parsed.query).get("value", [""])[0]
        if parsed.path != "/marker" or not re.fullmatch(r"[A-Za-z0-9-]{1,128}", marker):
            self.respond(400, {"error": "invalid-marker"})
            return
        os.makedirs("/home", exist_ok=True)
        with open(MARKER_PATH, "w", encoding="utf-8") as f:
            f.write(marker)
        self.respond(200, state())
    def log_message(self, format, *args): pass

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

    Write-Step 'Building probe image; plain Docker progress follows'
    & docker build --progress=plain `
        --label "org.opencontainers.image.source=https://github.com/$owner/$repository" `
        --label 'org.opencontainers.image.description=Temporary Azure App Service F1 feasibility probe; no production workload or provider access.' `
        --tag $imageTag `
        $probeRoot
    $buildExit = $LASTEXITCODE
    Write-Host "GHCR_PROBE_BUILD_EXIT_CODE=$buildExit"
    if ($buildExit -ne 0) { throw 'GHCR probe image build failed.' }
    $buildCompleted = $true

    Write-Step 'Pushing probe image to GHCR; Docker layer progress follows'
    & docker push $imageTag
    $pushExit = $LASTEXITCODE
    Write-Host "GHCR_PROBE_PUSH_EXIT_CODE=$pushExit"
    if ($pushExit -ne 0) { throw 'GHCR probe image push failed.' }
    $pushCompleted = $true

    Write-Step 'Reading pushed image identity'
    $repoDigest = (& docker image inspect --format '{{index .RepoDigests 0}}' $imageTag)
    Write-Host "GHCR_PROBE_IMAGE_REFERENCE=$imageTag"
    Write-Host "GHCR_PROBE_IMAGE_DIGEST=$repoDigest"
}
catch {
    $scriptExit = 1
    Write-Host "GHCR_DISTRIBUTION_ERROR=$($_.Exception.Message)"
}
finally {
    Write-Step 'Cleaning local temporary image'
    $localImageId = (& docker image ls --quiet $imageTag)
    if (-not [string]::IsNullOrWhiteSpace($localImageId)) {
        & docker image rm --force $imageTag
        Write-Host "GHCR_PROBE_LOCAL_IMAGE_REMOVE_EXIT_CODE=$LASTEXITCODE"
    }
    else {
        Write-Host "GHCR_PROBE_LOCAL_IMAGE_REMOVE_EXIT_CODE=NOT_APPLICABLE"
    }

    Write-Step 'Cleaning newly pulled base image only when introduced by this run'
    if (-not $baseWasPresent) {
        $baseImageId = (& docker image ls --quiet $baseImage)
        if (-not [string]::IsNullOrWhiteSpace($baseImageId)) {
            & docker image rm --force $baseImage
            Write-Host "GHCR_PROBE_NEW_BASE_IMAGE_REMOVE_EXIT_CODE=$LASTEXITCODE"
        }
        else {
            Write-Host "GHCR_PROBE_NEW_BASE_IMAGE_REMOVE_EXIT_CODE=NOT_APPLICABLE"
        }
    }
    else {
        Write-Host "GHCR_PROBE_NEW_BASE_IMAGE_REMOVE_EXIT_CODE=NOT_APPLICABLE_PREEXISTING"
    }

    Write-Step 'Removing temporary source directory and verifying cleanup'
    Remove-Item -LiteralPath $probeRoot -Recurse -Force -ErrorAction SilentlyContinue
    $imageAfter = (& docker image ls --quiet $imageTag)
    $complete = $buildCompleted -and $pushCompleted -and ($scriptExit -eq 0)

    Write-Host "GHCR_PROBE_LOCAL_IMAGE_PRESENT_AFTER_CLEANUP=$(-not [string]::IsNullOrWhiteSpace($imageAfter))"
    Write-Host "GHCR_PROBE_TEMP_DIRECTORY_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $probeRoot)"
    Write-Host "GHCR_PROBE_BUILD_COMPLETED=$buildCompleted"
    Write-Host "GHCR_PROBE_PUSH_COMPLETED=$pushCompleted"
    Write-Host "GHCR_DISTRIBUTION_SCRIPT_EXIT_CODE=$(if ($complete) { 0 } else { 1 })"
}