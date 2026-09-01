$runId = [guid]::NewGuid().ToString('N')
$tempRoot = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp04-$runId"
$image = "aiq-wp04-twelve-probe:wp04-r1-$runId"
$container = "aiq-wp04-twelve-$runId"
$port = 18084

New-Item -ItemType Directory -Path $tempRoot -Force | Out-Null
Write-Host "WP04_TEMP_ROOT=$tempRoot"
Write-Host "WP04_LOCAL_IMAGE=$image"
Write-Host "WP04_CONTAINER=$container"

$dockerfile = @'
FROM python:3.13-alpine
WORKDIR /app
COPY server.py /app/server.py
EXPOSE 8080
CMD ["python", "/app/server.py"]
'@

$server = @'
import json
import os
import socket
import ssl
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from urllib.error import HTTPError, URLError
from urllib.parse import parse_qs, urlparse
from urllib.request import Request, urlopen

HOST = "api.twelvedata.com"
API_URL = "https://api.twelvedata.com/price?symbol=AAPL"
KEY_NAME = "TWELVE_DATA_API_KEY"
TIMEOUT_SECONDS = 10

def send(handler, status, payload):
    data = json.dumps(payload, separators=(",", ":")).encode("utf-8")
    handler.send_response(status)
    handler.send_header("Content-Type", "application/json")
    handler.send_header("Content-Length", str(len(data)))
    handler.end_headers()
    handler.wfile.write(data)

def dns_tls_probe():
    try:
        addresses = socket.getaddrinfo(HOST, 443, type=socket.SOCK_STREAM)
        raw = socket.create_connection((HOST, 443), timeout=TIMEOUT_SECONDS)
        context = ssl.create_default_context()
        with context.wrap_socket(raw, server_hostname=HOST) as tls:
            tls_version = tls.version()
            verified = True
        return 200, {"classification":"dns_tls_success","host":HOST,"resolved":bool(addresses),"tlsVerified":verified,"tlsVersion":tls_version}
    except Exception as ex:
        return 502, {"classification":"dns_tls_failure","host":HOST,"errorType":type(ex).__name__}

def provider_probe(mode):
    if mode == "missing":
        return 424, {"classification":"missing_secret","providerRequestMade":False,"secretPresent":False}
    if mode == "network":
        try:
            socket.getaddrinfo("wp04-controlled-unreachable.invalid", 443, type=socket.SOCK_STREAM)
            return 502, {"classification":"synthetic_network_failure_unexpected_resolution","providerRequestMade":False}
        except socket.gaierror:
            return 504, {"classification":"synthetic_network_failure","providerRequestMade":False,"timeoutBoundSeconds":TIMEOUT_SECONDS}
    if mode == "invalid":
        key = "invalid-wp04-probe-key"
        secret_present = False
    else:
        key = os.getenv(KEY_NAME, "")
        secret_present = bool(key)
        if not secret_present:
            return 424, {"classification":"missing_secret","providerRequestMade":False,"secretPresent":False}

    try:
        request = Request(API_URL, headers={"Authorization":"apikey " + key, "Accept":"application/json"})
        with urlopen(request, timeout=TIMEOUT_SECONDS) as response:
            status = response.status
            payload = json.loads(response.read().decode("utf-8"))
        if status == 200 and "price" in payload:
            return 200, {"classification":"authenticated_success","providerRequestMade":True,"httpStatus":status,"secretPresent":secret_present}
        code = str(payload.get("code", ""))
        classification = "invalid_secret_or_provider_failure" if mode == "invalid" or code in ("401", "403") else "provider_response_failure"
        return 502, {"classification":classification,"providerRequestMade":True,"httpStatus":status,"secretPresent":secret_present}
    except HTTPError as ex:
        classification = "invalid_secret_or_provider_failure" if mode == "invalid" or ex.code in (401, 403) else "provider_http_failure"
        return 502, {"classification":classification,"providerRequestMade":True,"httpStatus":ex.code,"secretPresent":secret_present}
    except (URLError, TimeoutError, socket.timeout) as ex:
        return 504, {"classification":"provider_transport_failure","providerRequestMade":True,"errorType":type(ex).__name__,"secretPresent":secret_present}
    except Exception as ex:
        return 502, {"classification":"provider_probe_failure","providerRequestMade":True,"errorType":type(ex).__name__,"secretPresent":secret_present}

class Handler(BaseHTTPRequestHandler):
    def log_message(self, fmt, *args):
        return
    def do_GET(self):
        parsed = urlparse(self.path)
        if parsed.path == "/healthz":
            send(self, 200, {"status":"ok","service":"aiq-azure-f1-wp04","revision":os.getenv("PROBE_REVISION","wp04-r1")})
        elif parsed.path == "/diagnostics":
            send(self, 200, {"secretSettingName":KEY_NAME,"secretPresent":bool(os.getenv(KEY_NAME,"")),"providerHost":HOST,"secretValueDisclosed":False})
        elif parsed.path == "/probe/dns-tls":
            status, payload = dns_tls_probe()
            send(self, status, payload)
        elif parsed.path == "/probe/provider":
            mode = parse_qs(parsed.query).get("mode", ["valid"])[0]
            if mode not in ("valid", "missing", "invalid", "network"):
                send(self, 400, {"classification":"invalid_probe_mode","providerRequestMade":False})
            else:
                status, payload = provider_probe(mode)
                send(self, status, payload)
        else:
            send(self, 404, {"error":"not_found"})

with ThreadingHTTPServer(("0.0.0.0", 8080), Handler) as server:
    server.serve_forever()
'@

[System.IO.File]::WriteAllText((Join-Path $tempRoot 'Dockerfile'), $dockerfile, [System.Text.UTF8Encoding]::new($false))
[System.IO.File]::WriteAllText((Join-Path $tempRoot 'server.py'), $server, [System.Text.UTF8Encoding]::new($false))

Write-Host '=== BUILD SECRET-FREE PROBE IMAGE ==='
docker build --progress plain --tag $image $tempRoot
Write-Host "WP04_LOCAL_IMAGE_BUILD_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Probe image build failed.' }

Write-Host '=== RUN LOCAL PROBE WITHOUT SECRET ==='
docker run --detach --rm --name $container --publish "${port}:8080" $image
Write-Host "WP04_LOCAL_CONTAINER_RUN_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Probe container start failed.' }

$baseUrl = "http://127.0.0.1:$port"
$healthy = $false
for ($attempt = 1; $attempt -le 15; $attempt++) {
    try {
        $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 10
        if ($health.status -eq 'ok') { $healthy = $true; break }
    } catch { Start-Sleep -Seconds 1 }
}
Write-Host "WP04_LOCAL_HEALTHY=$healthy"
if (-not $healthy) { throw 'Local probe did not become healthy.' }

Write-Host '=== SECRET-FREE DIAGNOSTICS ==='
$diagnostics = Invoke-RestMethod -Uri "$baseUrl/diagnostics" -TimeoutSec 10
$diagnostics | ConvertTo-Json -Compress
Write-Host 'WP04_LOCAL_DIAGNOSTICS_EXIT_CODE=0'

Write-Host '=== LOCAL DNS/TLS PROBE ==='
$dnsTls = Invoke-RestMethod -Uri "$baseUrl/probe/dns-tls" -TimeoutSec 30
$dnsTls | ConvertTo-Json -Compress
Write-Host 'WP04_LOCAL_DNS_TLS_EXIT_CODE=0'

Write-Host '=== LOCAL MISSING-SECRET ISOLATION ==='
try {
    Invoke-RestMethod -Uri "$baseUrl/probe/provider?mode=missing" -TimeoutSec 30
} catch {
    $missing = $_.ErrorDetails.Message | ConvertFrom-Json
    $missing | ConvertTo-Json -Compress
}
Write-Host 'WP04_LOCAL_MISSING_SECRET_EXIT_CODE=0'

docker rm --force $container *> $null
Write-Host "WP04_LOCAL_CONTAINER_REMOVE_EXIT_CODE=$LASTEXITCODE"
Write-Host "WP04_LOCAL_IMAGE_RETAINED=$image"
Write-Host "WP04_TEMP_SOURCE_RETAINED=$tempRoot"