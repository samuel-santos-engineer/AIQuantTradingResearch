"""Exact research-only implementation. No operation at import; no provider or UI."""
import contextlib
import datetime as dt
import hashlib
import importlib.util
import json
import os
from pathlib import Path
import subprocess
import sys
import time
import uuid

HERE = Path(__file__).resolve().parent
ROOT = HERE.parents[2]
SUB = "7f5eabb5-80f5-4369-88be-b14a6de763f1"
GROUP = "rg-aiq-r112-wp05-wus2-7f5eabb5"
APP = "aiqr115researchstagingwus27f5eabb5"
BASE = "/subscriptions/" + SUB + "/resourceGroups/" + GROUP + "/providers/Microsoft.Web/"
SITE = BASE + "sites/" + APP
PLAN = BASE + "serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5"
DIGEST = "sha256:d3221dbe0a4ef6dfba37e0c61c06bb3bc9d9ee2b6f776d7738c35aece32be26f"
IMAGE = "ghcr.io/samuel-santos-engineer/aiquanttradingresearch@" + DIGEST
API = "2023-12-01"
FILES = {"runtime": HERE / "wp21_runtime.py", "controller": HERE / "wp21_controller.py", "recovery": HERE / "wp21_recovery.py",
         "collector": HERE / "wp16_research_readiness.py", "readiness": ROOT / "eng/docker/release-1.15/Test-Wp07StagingChartReadiness.ps1",
         "launcher": HERE / "wp18_research_launcher.py", "handoff": HERE / "wp19_operator_handoff.py",
         "snapshotClient": ROOT / "python/presentation/research_snapshot_client.py",
         "qualificationEntry": ROOT / "eng/docker/release-1.15/Test-Wp21ControllerQualification.ps1",
         "liveEntry": ROOT / "eng/docker/release-1.15/Invoke-Wp21ResearchAttempt.ps1",
         "recoveryEntry": ROOT / "eng/docker/release-1.15/Restore-Wp21ResearchBaseline.ps1",
         "tests": ROOT / "tests/runtime/release_1_15/test_wp21_controller.py"}


class Blocked(Exception):
    def __init__(self, reason, evidence=None):
        super().__init__(reason)
        self.evidence = evidence


def require(ok, reason):
    if not ok: raise Blocked(reason)


def utc(): return dt.datetime.now(dt.timezone.utc)
def sha(data): return hashlib.sha256(data).hexdigest()
def encoded(value): return json.dumps(value, sort_keys=True, separators=(",", ":")).encode("utf-8")
def identities(): return {k: sha(v.read_bytes()) for k, v in FILES.items()}


def save_new(path, value):
    data = encoded(value) + b"\n"
    require(len(data)<=8*1024*1024,"PRIVATE_RECORD_TOO_LARGE")
    with path.open("xb") as stream:
        stream.write(data);stream.flush();os.fsync(stream.fileno())


def load(path):
    data = path.read_bytes();require(len(data) <= 8 * 1024 * 1024, "PRIVATE_INPUT_TOO_LARGE")
    return json.loads(data)


def private_directory(path):
    require(sys.version_info >= (3,12), "PYTHON_312_OR_NEWER_REQUIRED")
    require(os.name == "nt" and path.is_absolute() and path.is_dir() and not path.drive.startswith("\\"), "PRIVATE_DIRECTORY_REQUIRED")
    expected = Path(os.environ["LOCALAPPDATA"]) / "AIQ-WP21" / path.name
    require(path == expected and not any(p.is_symlink() or p.is_junction() for p in (path, *path.parents)), "FIXED_LOCAL_PRIVATE_DIRECTORY_REQUIRED")
    # Owner/System only, including inherited ACEs; no ACL modification.
    literal = str(path).replace("'", "''")
    command = "$ErrorActionPreference='Stop';$me=[Security.Principal.WindowsIdentity]::GetCurrent().User.Value;$a=Get-Acl -LiteralPath '" + literal + "';$bad=@($a.GetAccessRules($true,$true,[Security.Principal.SecurityIdentifier])|Where-Object {$_.AccessControlType -eq 'Allow' -and $_.IdentityReference.Value -notin @($me,'S-1-5-18')});@{safe=($bad.Count -eq 0 -and $a.GetOwner([Security.Principal.SecurityIdentifier]).Value -in @($me,'S-1-5-18'))}|ConvertTo-Json"
    p = subprocess.run(["powershell.exe", "-NoProfile", "-NonInteractive", "-Command", command], capture_output=True, timeout=15)
    require(p.returncode == 0 and json.loads(p.stdout)["safe"] is True, "PRIVATE_ACL_UNVERIFIED")


def authorization(value, directory, recovery=False):
    require(set(value) == {"attemptId", "target", "plan", "image", "issuedUtc", "expiresUtc", "explicitLiveApproval", "allowRecovery", "code"}, "APPROVAL_FIELDS_INVALID")
    attempt = str(uuid.UUID(value["attemptId"]))
    require(attempt == value["attemptId"] and directory.name == attempt, "ATTEMPT_ID_MISMATCH")
    require(value["target"] == SITE and value["plan"] == PLAN and value["image"] == IMAGE, "EXACT_TARGET_IMAGE_REQUIRED")
    require(value["explicitLiveApproval"] is True and value["allowRecovery"] is True, "EXPLICIT_AUTHORITY_REQUIRED")
    if recovery:
        require(all(value["code"].get(k) == sha(FILES[k].read_bytes()) for k in ("runtime", "recovery", "recoveryEntry")), "RECOVERY_IDENTITY_MISMATCH")
    else:
        require(value["code"] == identities(), "REVIEWED_CODE_MISMATCH")
        issued = dt.datetime.fromisoformat(value["issuedUtc"]);expires = dt.datetime.fromisoformat(value["expiresUtc"])
        require(issued.tzinfo is not None and expires.tzinfo is not None and issued <= utc() <= expires and dt.timedelta(0) < expires-issued <= dt.timedelta(hours=1), "APPROVAL_EXPIRED_OR_NOT_FRESH")
    return attempt


# JSON containing secret bodies travels via stdin, never OS arguments or disk.
# Use the existing CLI's authenticated REST transport without invoke()/command
# history logging of secret body arguments. No token is returned to this process.
# API/signature inspected offline in the installed CLI; no auth call in WP21.
BRIDGE = """import sys,json,logging,threading,os
t=threading.Timer(40,lambda:os._exit(124));t.daemon=True;t.start()
logging.disable(logging.CRITICAL)
from azure.cli.core import get_default_cli
from azure.cli.core.util import send_raw_request
try:
 a=json.load(sys.stdin);method=a[a.index('--method')+1];url=a[a.index('--url')+1]
 body=a[a.index('--body')+1] if '--body' in a else None
 r=send_raw_request(get_default_cli(),method,url,headers={'Content-Type':'application/json'},body=body)
 if not 200<=r.status_code<300 or len(r.content)>8388608:sys.exit(1)
 json.dump(r.json() if r.content else None,sys.stdout)
except BaseException:sys.exit(1)
"""


def cli(arguments):
    interpreter = Path(os.environ.get("ProgramFiles", r"C:\Program Files")) / "Microsoft SDKs/Azure/CLI2/python.exe"
    require(interpreter.is_file(), "AZURE_CLI_UNAVAILABLE")
    directory = Path(os.environ["AIQ_WP21_ATTEMPT_DIRECTORY"])
    p = subprocess.Popen([str(interpreter), "-I", "-c", BRIDGE], stdin=subprocess.PIPE, stdout=subprocess.PIPE, stderr=subprocess.PIPE,
                         creationflags=getattr(subprocess, "CREATE_NO_WINDOW", 0))
    try:
        identity = worker_identity(p.pid)
        save_new(directory/("management-worker-"+str(uuid.uuid4())+".json"), identity)
        # No request is supplied until ownership is durably journaled.
        output, errors = p.communicate(input=encoded(arguments + ["--only-show-errors"]), timeout=45)
        require(p.returncode == 0 and len(output) <= 8*1024*1024, "AZURE_OPERATION_FAILED")
        return json.loads(output) if output.strip() else None
    except BaseException:
        if p.poll() is None:
            p.kill();p.communicate(timeout=5)
        raise


def _process_api():
    import ctypes as ct
    from ctypes import wintypes as w
    k = ct.WinDLL("kernel32", use_last_error=True)
    k.OpenProcess.argtypes = [w.DWORD,w.BOOL,w.DWORD];k.OpenProcess.restype = w.HANDLE
    k.GetProcessTimes.argtypes = [w.HANDLE]+[ct.POINTER(w.FILETIME)]*4;k.GetProcessTimes.restype = w.BOOL
    k.TerminateProcess.argtypes = [w.HANDLE,w.UINT];k.TerminateProcess.restype = w.BOOL
    k.WaitForSingleObject.argtypes = [w.HANDLE,w.DWORD];k.WaitForSingleObject.restype = w.DWORD
    k.CloseHandle.argtypes = [w.HANDLE];k.CloseHandle.restype = w.BOOL
    return k


def _creation(k, handle):
    import ctypes as ct
    from ctypes import wintypes as w
    times = [w.FILETIME() for _ in range(4)]
    require(k.GetProcessTimes(handle,*(ct.byref(t) for t in times)), "WORKER_IDENTITY_UNAVAILABLE")
    return (times[0].dwHighDateTime << 32) | times[0].dwLowDateTime


def worker_identity(pid):
    k = _process_api();handle = k.OpenProcess(0x1000,False,pid)
    require(handle, "WORKER_IDENTITY_UNAVAILABLE")
    try: return {"pid":pid,"created":_creation(k,handle)}
    finally: k.CloseHandle(handle)


def quiesce_workers(directory):
    # Stable kernel handles plus creation times avoid killing unrelated reused PIDs.
    import ctypes as ct
    k = _process_api()
    for path in directory.glob("management-worker-*.json"):
        value = load(path)
        require(set(value)=={"pid","created"} and type(value["pid"]) is int and value["pid"]>0 and type(value["created"]) is int and value["created"]>0, "WORKER_RECORD_INVALID")
        handle = k.OpenProcess(0x1000 | 0x0001 | 0x00100000,False,value["pid"])
        if not handle:
            require(ct.get_last_error()==87,"WORKER_STATE_UNVERIFIED")
            continue  # No such PID; the owned worker is gone.
        try:
            if _creation(k,handle) != value["created"]: continue  # Reused PID: never terminate.
            if k.WaitForSingleObject(handle,0) != 0:
                require(k.TerminateProcess(handle,125),"OWNED_WORKER_STOP_UNCONFIRMED")
                require(k.WaitForSingleObject(handle,5000)==0,"OWNED_WORKER_STOP_UNCONFIRMED")
        finally:k.CloseHandle(handle)



class Azure:
    def request(self, method, suffix="", body=None, resource=SITE, kind=None):
        require(resource in (SITE, PLAN), "RESOURCE_NOT_ALLOWED")
        require((method, suffix, resource) in {
            ("get", "", SITE), ("get", "", PLAN), ("get", "/config/web", SITE),
            ("post", "/config/appsettings/list", SITE), ("post", "/config/connectionstrings/list", SITE),
            ("get", "/config/logs", SITE), ("get", "/config/slotConfigNames", SITE), ("get", "/slots", SITE),
            ("post", "/stop", SITE), ("post", "/start", SITE), ("patch", "/config/web", SITE),
            ("put", "/config/appsettings", SITE), ("put", "/config/logs", SITE), ("put", "/config/slotConfigNames", SITE), ("patch", "", SITE)}, "OPERATION_NOT_ALLOWED")
        args = ["rest", "--method", method, "--url", "https://management.azure.com"+resource+suffix+"?api-version="+API]
        if body is not None:
            payload={"properties":body}
            if kind is not None:
                require((method,suffix,resource)==("patch","",SITE) and kind in ("app,linux","app,linux,container"),"HOST_KIND_NOT_ALLOWED")
                payload["kind"]=kind
            args += ["--body",json.dumps(payload)]
        args += ["-o", "none" if method in ("patch", "put") or suffix in ("/stop", "/start") else "json"]
        return cli(args)

    def capture(self):
        site = self.request("get");p = site["properties"]
        require(site["id"].lower() == SITE.lower() and p["serverFarmId"].lower() == PLAN.lower(), "RESOURCE_DRIFT")
        require(site["kind"] == "app,linux,container" or site["kind"] == "app,linux", "HOST_KIND_UNSUPPORTED")
        require("".join(c for c in site["location"].lower() if c.isalnum()) == "westus2", "REGION_DRIFT")
        plan = self.request("get", resource=PLAN)
        require(plan["id"].lower() == PLAN.lower() and plan["sku"]["name"] == "F1", "EXISTING_F1_REQUIRED")
        value = {"site": {"id": site["id"], "kind": site["kind"], "location": site["location"], **{k: p[k] for k in ("serverFarmId", "state", "httpsOnly", "publicNetworkAccess")}},
                 "plan": {"id": plan["id"], "sku": plan["sku"]}}
        for key, method, route in (("web", "get", "/config/web"), ("settings", "post", "/config/appsettings/list"),
                                   ("connections", "post", "/config/connectionstrings/list"), ("logs", "get", "/config/logs"), ("slotFlags", "get", "/config/slotConfigNames")):
            value[key] = self.request(method, route)["properties"]
        value["slots"] = self.request("get", "/slots")["value"]
        validate_baseline(value)
        return value

    def stop(self):
        self.request("post", "/stop")
        for _ in range(3):
            if self.request("get")["properties"]["state"] == "Stopped": return
            time.sleep(1)
        raise Blocked("STOP_UNCONFIRMED")

    def deploy(self, baseline):
        key = os.environ.get("AIQ_WP21_SERVICE_KEY") or baseline["settings"].get("AIQ_RESEARCH_API_KEY", "")
        require(32 <= len(key) <= 256 and all('!' <= c <= '~' for c in key), "PRIVATE_SERVICE_KEY_UNAVAILABLE")
        values = dict(baseline["settings"])
        values.update({"AIQ_RESEARCH_API_KEY": key, "WEBSITES_PORT": "52731", "AIQ_RESEARCH_BOOTSTRAP_MODE": "serve",
                       "AIQ_RESEARCH_BIND_ADDRESS": "0.0.0.0", "AIQ_RESEARCH_TRANSPORT": "azure-app-service-http",
                       "AIQ_RESEARCH_PLATFORM_HTTPS_ONLY": "1", "AIQ_RESEARCH_PRIVATE_DIRECTORY": "/tmp/aiq-research",
                       "AIQ_RESEARCH_STORAGE_SCOPE": "azure-f1-staging-ephemeral", "AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING": "1",
                       "AIQ_RESEARCH_EXPECTED_MOUNT_PATH": "/", "WEBSITES_ENABLE_APP_SERVICE_STORAGE": "false"})
        self.stop()
        self.request("patch", "/config/web", {"linuxFxVersion": "DOCKER|"+IMAGE, "appCommandLine": ""})
        self.request("put", "/config/appsettings", values)
        self.request("patch", body={"httpsOnly": True})
        self.request("post", "/start")  # Controller calls health immediately, no settle/read first.

    def restore(self, baseline):
        validate_baseline(baseline)
        self.stop()
        self.request("patch", "/config/web", {k: baseline["web"][k] for k in ("linuxFxVersion", "appCommandLine")})
        self.request("put", "/config/appsettings", baseline["settings"])
        self.request("patch", body={"httpsOnly": baseline["site"]["httpsOnly"]}, kind=baseline["site"]["kind"])
        for key,route in (("logs","/config/logs"),("slotFlags","/config/slotConfigNames")):
            if self.request("get",route)["properties"] != baseline[key]:
                self.request("put",route,baseline[key])
        if baseline["site"]["state"] == "Running": self.request("post", "/start")
        # Connections/inventory are never mutated; strict comparison refuses
        # success on drift rather than deleting unrelated resources.


def validate_baseline(b):
    require(set(b) == {"site", "plan", "web", "settings", "connections", "logs", "slotFlags", "slots"}, "INCOMPLETE_BASELINE")
    require(b["site"]["id"].lower() == SITE.lower() and b["site"]["serverFarmId"].lower() == PLAN.lower() and b["plan"]["id"].lower() == PLAN.lower(), "BASELINE_TARGET_MISMATCH")
    require(b["site"]["state"] in ("Running", "Stopped") and type(b["site"]["httpsOnly"]) is bool and b["plan"]["sku"]["name"] == "F1", "BASELINE_STATE_INVALID")
    require({"linuxFxVersion", "appCommandLine"} <= set(b["web"]), "BASELINE_STARTUP_MISSING")
    require(isinstance(b["settings"], dict) and all(isinstance(v, str) for v in b["settings"].values()), "BASELINE_SETTINGS_INVALID")
    require(isinstance(b["connections"], dict) and b["slots"] == [] and isinstance(b["slotFlags"], dict), "BASELINE_METADATA_MISSING")
    require(all(isinstance(v,dict) and isinstance(v.get("value"),str) and isinstance(v.get("type"),str) for v in b["connections"].values()), "BASELINE_CONNECTION_METADATA_UNVERIFIED")
    require({"appSettingNames", "connectionStringNames"} <= set(b["slotFlags"]), "BASELINE_SLOT_FLAGS_MISSING")
    require({"applicationLogs", "httpLogs", "detailedErrorMessages", "failedRequestsTracing"} <= set(b["logs"]), "BASELINE_LOGGING_MISSING")


def health():
    script = FILES["readiness"]
    p = subprocess.run(["powershell.exe", "-NoProfile", "-NonInteractive", "-File", str(script), "-ProbeResearch"], capture_output=True, timeout=200)
    require(len(p.stdout) <= 8192, "HEALTH_REPORT_TOO_LARGE")
    try: value = json.loads(p.stdout.decode("utf-8-sig"))["researchHttp"]
    except (ValueError,KeyError,TypeError):
        raise Blocked("HEALTH_OR_LOCAL_LAUNCH_FAILED", {"httpResponseObserved":False,"collectorReportAvailable":False}) from None
    require(isinstance(value,dict) and isinstance(value.get("attempts"),list) and 1 <= len(value["attempts"]) <= 3, "HEALTH_REPORT_INVALID")
    attempts=[]
    for item in value["attempts"]:
        status=item.get("status")
        require(item.get("outcome") in ("ALIVE","PENDING","FAILED") and (status is None or type(status) is int and 100<=status<=599), "HEALTH_REPORT_INVALID")
        attempts.append({"outcome":item["outcome"],"status":status})
    evidence={"attempts":attempts,"httpResponseObserved":any(x["status"] is not None for x in attempts),"collectorReportAvailable":True}
    if not (p.returncode == 0 and value["result"] == "PROCESS_ALIVE" and value["processAlive"] is True and type(value["elapsedSeconds"]) in (int,float) and 0 <= value["elapsedSeconds"] <= 180 and attempts[-1] == {"outcome":"ALIVE","status":200}):
        raise Blocked("BOUNDED_COLLECTOR_DID_NOT_ESTABLISH_HEALTH",evidence)
    return True



def unauthenticated_snapshot(instrument):
    # Owned worker bounds native DNS; no key, no authenticated dataset request.
    p = subprocess.run([sys.executable, "-B", str(HERE / "wp21_controller.py"), "--snapshot-worker", instrument], capture_output=True, timeout=10)
    require(p.returncode == 0 and p.stdout.strip() == b"VALIDATED_401", "SNAPSHOT_CHECK_FAILED")


def envelope(directory):
    value = load(directory/"baseline.json");approval = value["approval"]
    authorization(approval, directory, recovery=True)
    require(value["baselineHash"] == sha(encoded(value["baseline"])) and value["approvalHash"] == sha(encoded(approval)), "BASELINE_OR_APPROVAL_HASH_MISMATCH")
    marker = load(directory/"consumed.json")
    require(set(marker) == {"attemptId", "baselineHash", "approvalHash", "controllerPid"} and marker["approvalHash"] == value["approvalHash"] and marker["attemptId"] == approval["attemptId"] and marker["baselineHash"] == value["baselineHash"] and type(marker["controllerPid"]) is int and 0 < marker["controllerPid"] <= 2147483647 and marker["controllerPid"] == value["controllerPid"], "ATTEMPT_MARKER_MISMATCH")
    validate_baseline(value["baseline"])
    return value


def controller_inactive(marker):
    command = "@{inactive=($null -eq (Get-Process -Id " + str(marker["controllerPid"]) + " -ErrorAction SilentlyContinue))}|ConvertTo-Json"
    p = subprocess.run(["powershell.exe", "-NoProfile", "-NonInteractive", "-Command", command], capture_output=True, timeout=15)
    return p.returncode == 0 and json.loads(p.stdout).get("inactive") is True


def recover(directory, adapter, controller_cleanup=False):
    value = envelope(directory)
    marker = load(directory/"consumed.json")
    require(marker["controllerPid"] == os.getpid() if controller_cleanup else controller_inactive(marker), "ACTIVE_CONTROLLER_OR_PID_REUSE_REQUIRES_REVIEW")
    restored = False
    try:
        quiesce_workers(directory)
        adapter.restore(value["baseline"]);restored = True
    except BaseException:
        restored = False
    finally:
        try:
            current = adapter.capture()
            differences = sorted(k for k in value["baseline"] if current.get(k) != value["baseline"][k])
            matched = not differences
        except Exception:
            matched = False;differences = ["CAPTURE_UNAVAILABLE_OR_TARGET_DRIFT"]
        report = {"unrestoredFields": differences, "result": "BASELINE_RESTORED" if restored and matched else "CRITICAL_RECOVERY_UNVERIFIED",
                  "rollbackVerified": restored and matched, "observedUtc": utc().isoformat(), "PUBLIC_RUNTIME_VERIFIED": False}
        save_new(directory/("recovery-"+str(uuid.uuid4())+".json"), report)
    return report
