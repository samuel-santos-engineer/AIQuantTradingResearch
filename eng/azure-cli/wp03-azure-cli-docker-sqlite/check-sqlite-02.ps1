$ErrorActionPreference = 'Stop'

$runId = [guid]::NewGuid().ToString('N')
$tempRoot = Join-Path ([System.IO.Path]::GetTempPath()) "aiq-wp03-$runId"
$imageTag = "aiq-wp03-sqlite-probe:wp03-r1-$runId"
$containerName = "aiq-wp03-sqlite-$runId"
$port = 18083

New-Item -ItemType Directory -Path $tempRoot -Force | Out-Null
Write-Host "WP03_TEMP_ROOT=$tempRoot"
Write-Host "WP03_LOCAL_IMAGE=$imageTag"
Write-Host "WP03_CONTAINER=$containerName"
Write-Host "WP03_PORT=$port"

@'
import json
import os
import sqlite3
import threading
import time
from datetime import datetime, timezone
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from urllib.parse import parse_qs, urlparse

DB_PATH = os.getenv("SQLITE_PATH", "/home/aiq-wp03/qualification.sqlite3")
REVISION = os.getenv("PROBE_REVISION", "wp03-r1")
LOCKS = {}
LOCKS_GUARD = threading.Lock()

def now():
    return datetime.now(timezone.utc).isoformat()

def connect(timeout_ms=5000):
    con = sqlite3.connect(DB_PATH, timeout=timeout_ms / 1000, isolation_level=None)
    con.execute(f"PRAGMA busy_timeout={int(timeout_ms)}")
    return con

def initialize(requested_mode, name):
    os.makedirs(os.path.dirname(DB_PATH), exist_ok=True)
    con = connect()
    try:
        actual_mode = con.execute(f"PRAGMA journal_mode={requested_mode}").fetchone()[0]
        con.execute("BEGIN IMMEDIATE")
        con.execute("CREATE TABLE IF NOT EXISTS events (seq INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, committed_utc TEXT NOT NULL)")
        con.execute("INSERT INTO events(name, committed_utc) VALUES (?, ?)", (name, now()))
        con.execute("COMMIT")
        return actual_mode
    except Exception:
        con.execute("ROLLBACK")
        raise
    finally:
        con.close()

def state():
    os.makedirs(os.path.dirname(DB_PATH), exist_ok=True)
    con = connect()
    try:
        con.execute("CREATE TABLE IF NOT EXISTS events (seq INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, committed_utc TEXT NOT NULL)")
        rows = [{"seq": r[0], "name": r[1], "committedUtc": r[2]} for r in con.execute("SELECT seq, name, committed_utc FROM events ORDER BY seq")]
        journal = con.execute("PRAGMA journal_mode").fetchone()[0]
        integrity = con.execute("PRAGMA integrity_check").fetchone()[0]
        quick = con.execute("PRAGMA quick_check").fetchone()[0]
    finally:
        con.close()

    directory = os.path.dirname(DB_PATH)
    files = []
    if os.path.isdir(directory):
        for entry in sorted(os.listdir(directory)):
            path = os.path.join(directory, entry)
            if os.path.isfile(path):
                files.append({"name": entry, "size": os.path.getsize(path)})

    return {
        "service": "aiq-azure-f1-wp03",
        "revision": REVISION,
        "dbPath": DB_PATH,
        "sqliteVersion": sqlite3.sqlite_version,
        "journalMode": journal,
        "rows": rows,
        "integrityCheck": integrity,
        "quickCheck": quick,
        "files": files,
    }

def write(name, timeout_ms):
    con = connect(timeout_ms)
    try:
        con.execute("BEGIN IMMEDIATE")
        con.execute("INSERT INTO events(name, committed_utc) VALUES (?, ?)", (name, now()))
        con.execute("COMMIT")
        return {"result": "committed", "name": name}
    except sqlite3.OperationalError as ex:
        try:
            con.execute("ROLLBACK")
        except sqlite3.Error:
            pass
        return {"result": "blocked", "errorType": "sqlite_operational_error", "error": str(ex), "name": name}
    finally:
        con.close()

def hold_lock(lock_id, seconds):
    def worker():
        con = connect()
        try:
            con.execute("BEGIN IMMEDIATE")
            with LOCKS_GUARD:
                LOCKS[lock_id]["active"] = True
            time.sleep(seconds)
            con.execute("ROLLBACK")
            result = "released"
        except Exception as ex:
            result = "error:" + str(ex)
        finally:
            con.close()
            with LOCKS_GUARD:
                LOCKS[lock_id]["active"] = False
                LOCKS[lock_id]["result"] = result

    with LOCKS_GUARD:
        LOCKS[lock_id] = {"active": False, "result": "starting", "seconds": seconds}
    threading.Thread(target=worker, daemon=True).start()

class Handler(BaseHTTPRequestHandler):
    def log_message(self, fmt, *args):
        return

    def send_json(self, status_code, payload):
        data = json.dumps(payload, separators=(",", ":")).encode()
        self.send_response(status_code)
        self.send_header("Content-Type", "application/json")
        self.send_header("Content-Length", str(len(data)))
        self.end_headers()
        self.wfile.write(data)

    def do_GET(self):
        parsed = urlparse(self.path)
        if parsed.path == "/healthz":
            self.send_json(200, {"status": "ok", "revision": REVISION})
        elif parsed.path == "/state":
            self.send_json(200, state())
        elif parsed.path == "/lock/status":
            query = parse_qs(parsed.query)
            lock_id = query.get("id", [""])[0]
            with LOCKS_GUARD:
                payload = LOCKS.get(lock_id, {"active": False, "result": "not_found"})
            self.send_json(200, payload)
        else:
            self.send_json(404, {"error": "not_found"})

    def do_POST(self):
        parsed = urlparse(self.path)
        query = parse_qs(parsed.query)
        try:
            if parsed.path == "/baseline":
                mode = query.get("mode", ["DELETE"])[0].upper()
                name = query.get("name", ["baseline"])[0]
                actual = initialize(mode, name)
                self.send_json(200, {"result": "initialized", "requestedJournalMode": mode, "actualJournalMode": actual})
            elif parsed.path == "/write":
                name = query.get("name", ["write"])[0]
                timeout_ms = int(query.get("timeoutMs", ["5000"])[0])
                payload = write(name, timeout_ms)
                self.send_json(200 if payload["result"] == "committed" else 409, payload)
            elif parsed.path == "/lock/hold":
                lock_id = query.get("id", ["lock"])[0]
                seconds = int(query.get("seconds", ["10"])[0])
                hold_lock(lock_id, seconds)
                self.send_json(202, {"result": "starting", "lockId": lock_id, "seconds": seconds})
            elif parsed.path == "/transaction":
                name = query.get("name", ["transaction"])[0]
                commit = query.get("commit", ["true"])[0].lower() == "true"
                con = connect()
                try:
                    con.execute("BEGIN IMMEDIATE")
                    con.execute("INSERT INTO events(name, committed_utc) VALUES (?, ?)", (name, now()))
                    if commit:
                        con.execute("COMMIT")
                        result = "committed"
                    else:
                        con.execute("ROLLBACK")
                        result = "rolled_back"
                finally:
                    con.close()
                self.send_json(200, {"result": result, "name": name})
            else:
                self.send_json(404, {"error": "not_found"})
        except Exception as ex:
            self.send_json(500, {"errorType": type(ex).__name__, "error": str(ex)})

with ThreadingHTTPServer(("0.0.0.0", 8080), Handler) as server:
    server.serve_forever()
'@ | Set-Content -LiteralPath (Join-Path $tempRoot 'server.py') -Encoding Ascii

@'
FROM python:3.13-alpine
WORKDIR /app
COPY server.py /app/server.py
EXPOSE 8080
CMD ["python", "/app/server.py"]
'@ | Set-Content -LiteralPath (Join-Path $tempRoot 'Dockerfile') -Encoding Ascii

Write-Host '=== BUILD SQLITE PROBE IMAGE ==='
docker build --progress=plain --tag $imageTag $tempRoot
Write-Host "WP03_LOCAL_IMAGE_BUILD_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== RUN LOCAL SQLITE PROBE ==='
docker run --detach --name $containerName `
  --publish "127.0.0.1:${port}:8080" `
  --env PROBE_REVISION=wp03-r1 `
  --env SQLITE_PATH=/home/aiq-wp03/qualification.sqlite3 `
  $imageTag
Write-Host "WP03_LOCAL_CONTAINER_RUN_EXIT_CODE=$LASTEXITCODE"

$baseUrl = "http://127.0.0.1:$port"
$healthy = $false
for ($attempt = 1; $attempt -le 24; $attempt++) {
    Write-Host "WP03_LOCAL_HEALTH_ATTEMPT=$attempt"
    try {
        $health = Invoke-RestMethod -Uri "$baseUrl/healthz" -TimeoutSec 10
        $health | ConvertTo-Json -Compress
        $healthy = $true
        break
    } catch {
        Write-Host "WP03_LOCAL_HEALTH_ERROR=$($_.Exception.Message)"
        Start-Sleep -Seconds 2
    }
}
Write-Host "WP03_LOCAL_HEALTHY=$healthy"

if (-not $healthy) {
    throw 'Local SQLite probe did not become healthy.'
}

Write-Host '=== LOCAL DELETE-MODE BASELINE ==='
$baseline = Invoke-RestMethod -Method Post -Uri "$baseUrl/baseline?mode=DELETE&name=wp03-local-baseline" -TimeoutSec 30
$baseline | ConvertTo-Json -Compress
Write-Host "WP03_LOCAL_BASELINE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== LOCAL TRANSACTION COMMIT / ROLLBACK ==='
$commit = Invoke-RestMethod -Method Post -Uri "$baseUrl/transaction?name=wp03-local-commit&commit=true" -TimeoutSec 30
$commit | ConvertTo-Json -Compress
Write-Host "WP03_LOCAL_COMMIT_EXIT_CODE=$LASTEXITCODE"

$rollback = Invoke-RestMethod -Method Post -Uri "$baseUrl/transaction?name=wp03-local-rollback&commit=false" -TimeoutSec 30
$rollback | ConvertTo-Json -Compress
Write-Host "WP03_LOCAL_ROLLBACK_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== LOCAL STATE / INTEGRITY ==='
$state = Invoke-RestMethod -Uri "$baseUrl/state" -TimeoutSec 30
$state | ConvertTo-Json -Depth 8 -Compress
Write-Host "WP03_LOCAL_STATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== REMOVE TEMPORARY LOCAL CONTAINER ONLY ==='
docker rm --force $containerName
Write-Host "WP03_LOCAL_CONTAINER_REMOVE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== RETAINED FOR NEXT BATCH ==='
docker image inspect $imageTag --format 'LOCAL_IMAGE_PRESENT={{.Id}}'
Write-Host "WP03_LOCAL_IMAGE_INSPECT_EXIT_CODE=$LASTEXITCODE"
Write-Host "WP03_TEMP_SOURCE_PRESENT=$(Test-Path -LiteralPath $tempRoot)"