"""WP62 bounded research-only observation. Plan by default; never prints raw diagnostics."""
import argparse
import copy
import json
import os
from pathlib import Path
import re
import shutil
import subprocess
import tempfile
import threading
import time

SUB = "7f5eabb5-80f5-4369-88be-b14a6de763f1"
GROUP = "rg-aiq-r112-wp05-wus2-7f5eabb5"
APP = "aiqr115researchstagingwus27f5eabb5"
BASE = f"/subscriptions/{SUB}/resourceGroups/{GROUP}/providers/Microsoft.Web/"
SITE = BASE + "sites/" + APP
PLAN = BASE + "serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5"
REPOSITORY = "ghcr.io/samuel-santos-engineer/aiquanttradingresearch@"
OLD = "DOCKER|" + REPOSITORY + "sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3"
NEW = "DOCKER|" + REPOSITORY + "sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c"
STAGES = {"CONFIGURATION", "TRANSPORT", "TLS", "STORAGE", "LISTENER", "SERVING"}
LIMIT = 1048576
LIVE_ADAPTER_QUALIFIED = False  # SCM authentication and Windows ACL remain unqualified.


class Blocked(Exception):
    pass


def require(ok, code):
    if not ok:
        raise Blocked(code)


class WindowsJob:
    """Own a suspended process before it can create descendants; kill and verify the entire job."""
    def __init__(self):
        require(os.name == "nt", "WINDOWS_JOB_REQUIRED")
        import ctypes
        from ctypes import wintypes as w
        self.c, self.w = ctypes, w
        k = self.k = ctypes.WinDLL("kernel32", use_last_error=True)
        class Limits(ctypes.Structure):
            _fields_ = [("user", ctypes.c_longlong), ("job", ctypes.c_longlong), ("flags", w.DWORD),
                        ("minimum", ctypes.c_size_t), ("maximum", ctypes.c_size_t), ("active", w.DWORD),
                        ("affinity", ctypes.c_size_t), ("priority", w.DWORD), ("scheduling", w.DWORD)]
        class IO(ctypes.Structure):
            _fields_ = [(name, ctypes.c_ulonglong) for name in ("read", "write", "other", "readbytes", "writebytes", "otherbytes")]
        class Extended(ctypes.Structure):
            _fields_ = [("basic", Limits), ("io", IO), ("processmemory", ctypes.c_size_t),
                        ("jobmemory", ctypes.c_size_t), ("peakprocess", ctypes.c_size_t), ("peakjob", ctypes.c_size_t)]
        class Accounting(ctypes.Structure):
            _fields_ = [("user", ctypes.c_longlong), ("kernel", ctypes.c_longlong),
                        ("perioduser", ctypes.c_longlong), ("periodkernel", ctypes.c_longlong),
                        ("faults", w.DWORD), ("total", w.DWORD), ("active", w.DWORD), ("terminated", w.DWORD)]
        self.Accounting = Accounting
        k.CreateJobObjectW.argtypes = [ctypes.c_void_p, w.LPCWSTR]; k.CreateJobObjectW.restype = w.HANDLE
        k.SetInformationJobObject.argtypes = [w.HANDLE, ctypes.c_int, ctypes.c_void_p, w.DWORD]; k.SetInformationJobObject.restype = w.BOOL
        k.AssignProcessToJobObject.argtypes = [w.HANDLE, w.HANDLE]; k.AssignProcessToJobObject.restype = w.BOOL
        k.TerminateJobObject.argtypes = [w.HANDLE, w.UINT]; k.TerminateJobObject.restype = w.BOOL
        k.QueryInformationJobObject.argtypes = [w.HANDLE, ctypes.c_int, ctypes.c_void_p, w.DWORD, ctypes.c_void_p]; k.QueryInformationJobObject.restype = w.BOOL
        k.CloseHandle.argtypes = [w.HANDLE]; k.CloseHandle.restype = w.BOOL
        self.handle = k.CreateJobObjectW(None, None)
        require(bool(self.handle), "JOB_CREATE_FAILED")
        limits = Extended(); limits.basic.flags = 0x2000  # JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE; no breakaway.
        if not k.SetInformationJobObject(self.handle, 9, ctypes.byref(limits), ctypes.sizeof(limits)):
            k.CloseHandle(self.handle)
            raise Blocked("JOB_LIMIT_FAILED")
        self.cleanup_verified = False

    def attach_and_resume(self, proc):
        c, w, k = self.c, self.w, self.k
        require(k.AssignProcessToJobObject(self.handle, int(proc._handle)), "JOB_ASSIGN_FAILED")
        class ThreadEntry(c.Structure):
            _fields_ = [("size", w.DWORD), ("usage", w.DWORD), ("tid", w.DWORD), ("pid", w.DWORD),
                        ("base", w.LONG), ("delta", w.LONG), ("flags", w.DWORD)]
        k.CreateToolhelp32Snapshot.argtypes = [w.DWORD, w.DWORD]; k.CreateToolhelp32Snapshot.restype = w.HANDLE
        k.Thread32First.argtypes = [w.HANDLE, c.POINTER(ThreadEntry)]; k.Thread32First.restype = w.BOOL
        k.Thread32Next.argtypes = [w.HANDLE, c.POINTER(ThreadEntry)]; k.Thread32Next.restype = w.BOOL
        k.OpenThread.argtypes = [w.DWORD, w.BOOL, w.DWORD]; k.OpenThread.restype = w.HANDLE
        k.ResumeThread.argtypes = [w.HANDLE]; k.ResumeThread.restype = w.DWORD
        snapshot = k.CreateToolhelp32Snapshot(4, 0)
        require(snapshot not in (None, c.c_void_p(-1).value), "THREAD_SNAPSHOT_FAILED")
        try:
            entry = ThreadEntry(); entry.size = c.sizeof(entry); threads = []
            more = k.Thread32First(snapshot, c.byref(entry))
            while more:
                if entry.pid == proc.pid:
                    threads.append(entry.tid)
                more = k.Thread32Next(snapshot, c.byref(entry))
            require(len(threads) == 1, "SUSPENDED_THREAD_UNVERIFIED")
            handle = k.OpenThread(2, False, threads[0])
            require(bool(handle), "THREAD_OPEN_FAILED")
            try:
                require(k.ResumeThread(handle) == 1, "THREAD_RESUME_FAILED")
            finally:
                k.CloseHandle(handle)
        finally:
            k.CloseHandle(snapshot)

    def finish(self):
        c, k = self.c, self.k
        try:
            require(k.TerminateJobObject(self.handle, 1), "JOB_TERMINATE_FAILED")
            deadline = time.monotonic() + 3
            while True:
                accounting = self.Accounting()
                require(k.QueryInformationJobObject(self.handle, 1, c.byref(accounting), c.sizeof(accounting), None), "JOB_QUERY_FAILED")
                if accounting.active == 0:
                    self.cleanup_verified = True
                    break
                require(time.monotonic() < deadline, "JOB_DESCENDANTS_REMAIN")
                time.sleep(0.01)
        finally:
            closed = k.CloseHandle(self.handle)
            self.handle = None
            require(closed, "JOB_CLOSE_FAILED")


def capture(command, seconds=15, stream=False, popen=subprocess.Popen, owner_factory=None):
    """Combined bounded pipes and Windows Job ownership; no shell or raw-output persistence."""
    started = time.monotonic()
    real = popen is subprocess.Popen
    owner = owner_factory() if owner_factory else WindowsJob() if real else None
    kwargs = {"creationflags": 4} if owner else {}  # CREATE_SUSPENDED: no unowned descendant race.
    proc = None
    threads = []
    buffers = [bytearray(), bytearray()]
    lock = threading.Lock()
    overflow = threading.Event()
    expired = False

    def drain(pipe, target):
        while True:
            chunk = pipe.read(4096)
            if not chunk:
                return
            with lock:
                if sum(map(len, buffers)) + len(chunk) > LIMIT:
                    overflow.set()
                    return  # Main thread observes this and terminates the owned job.
                target.extend(chunk)

    try:
        proc = popen(command, stdout=subprocess.PIPE, stderr=subprocess.PIPE, **kwargs)
        if owner:
            owner.attach_and_resume(proc)
        threads = [threading.Thread(target=drain, args=(pipe, buf), daemon=True)
                   for pipe, buf in zip((proc.stdout, proc.stderr), buffers)]
        for thread in threads:
            thread.start()
        while True:
            if overflow.is_set():
                break
            remaining = seconds - (time.monotonic() - started)
            if remaining <= 0:
                expired = True
                break
            try:
                proc.wait(timeout=min(0.05, remaining))
                break
            except subprocess.TimeoutExpired:
                pass
    finally:
        try:
            if owner:
                owner.finish()  # Also removes descendants after normal parent completion.
        finally:
            if proc is not None:
                if proc.poll() is None:
                    proc.kill()  # Suspended assignment failure or fake process fallback only.
                proc.wait(timeout=5)
                for thread in threads:
                    thread.join(timeout=1)
                require(not any(t.is_alive() for t in threads), "PIPE_CLEANUP_UNVERIFIED")
                proc.stdout.close(); proc.stderr.close()
    require(not overflow.is_set(), "OUTPUT_LIMIT")
    require(not expired or stream, "TIMEOUT")
    require(expired or proc.returncode == 0, "CLI_FAILED")
    return bytes(buffers[0] + buffers[1]) if stream else bytes(buffers[0])


def cli_entry():
    launcher = shutil.which("az.cmd")
    require(bool(launcher), "CLI_MISSING")
    folder = Path(launcher).resolve().parent
    interpreter = next((p for p in (folder / "python.exe", folder.parent / "python.exe") if p.is_file()), None)
    require(interpreter is not None, "CLI_INTERPRETER_MISSING")
    return [str(interpreter), "-IBm", "azure.cli"]


def classify(raw):
    # Values and exceptions are deliberately excluded; these literal markers are not attestation.
    text = raw.decode("utf-8", errors="replace")
    return {"stages": sorted(set(re.findall(r"Research snapshot service unavailable[.] stage=(CONFIGURATION|TRANSPORT|TLS|STORAGE|LISTENER|SERVING)(?![A-Z_])", text))),
            "listenerMarker": "Research snapshot listener started." in text,
            "candidateBound": False}


class PrivateStore:
    """New owner/SYSTEM-only Windows directory, ACL verified before baseline writes."""
    def __init__(self):
        require(os.name == "nt", "WINDOWS_PRIVATE_STORE_REQUIRED")
        self.directory = Path(tempfile.mkdtemp(prefix="aiq-wp62-recovery-"))
        # Resolve identity and verify ACL using SID-based comparisons, never localized account labels.
        environment = dict(os.environ)
        environment["AIQ_WP62_ACL_DIRECTORY"] = str(self.directory)
        script = ("$ErrorActionPreference='Stop'; $p=$env:AIQ_WP62_ACL_DIRECTORY; "
                  "$sid=[System.Security.Principal.WindowsIdentity]::GetCurrent().User; "
                  "$acl=New-Object System.Security.AccessControl.DirectorySecurity; "
                  "$acl.SetOwner($sid); $acl.SetAccessRuleProtection($true,$false); "
                  "foreach($s in @($sid.Value,'S-1-5-18')) { "
                  "$id=New-Object System.Security.Principal.SecurityIdentifier($s); "
                  "$rule=New-Object System.Security.AccessControl.FileSystemAccessRule($id,'FullControl','ContainerInherit,ObjectInherit','None','Allow'); "
                  "$acl.AddAccessRule($rule) }; Set-Acl -LiteralPath $p -AclObject $acl; "
                  "$a=Get-Acl -LiteralPath $p; "
                  "if(-not $a.AreAccessRulesProtected){exit 2}; "
                  "$rules=@($a.GetAccessRules($true,$true,[System.Security.Principal.SecurityIdentifier])); "
                  "if($rules.Count -ne 2){exit 3}; foreach($r in $rules){ "
                  "if($r.IsInherited -or $r.AccessControlType -ne 'Allow' -or "
                  "$r.IdentityReference.Value -notin @($sid.Value,'S-1-5-18') -or $r.FileSystemRights -ne 'FullControl'){exit 4} }; "
                  "if($a.GetOwner([System.Security.Principal.SecurityIdentifier]).Value -ne $sid.Value){exit 5}; 'ACL_VERIFIED'")
        proc = subprocess.run(["powershell.exe", "-NoProfile", "-NonInteractive", "-Command", script],
                              env=environment, capture_output=True, timeout=15)
        if proc.returncode != 0 or proc.stdout.strip() != b"ACL_VERIFIED":
            # No baseline was written; remove only our known new empty directory.
            self.directory.rmdir()
            if b"PrivilegeNotHeldException" in proc.stderr:
                raise Blocked("WINDOWS_ACL_PRIVILEGE_NOT_HELD")
            raise Blocked("PRIVATE_ACL_UNVERIFIED")

    def write(self, name, value):
        require(name in {"baseline.json", "recovery.json", "ledger.json", "body.json"}, "PRIVATE_NAME_REJECTED")
        path = self.directory / name
        path.write_text(json.dumps(value) + "\n", encoding="utf-8", newline="\n")
        return path


class Azure:
    def __init__(self, store):
        require(LIVE_ADAPTER_QUALIFIED, "LIVE_ADAPTER_BLOCKED_SCM_AND_ACL_UNQUALIFIED")
        self.store, self.entry = store, cli_entry()
        self.rollback = None
        self.logging = None

    def request(self, method, resource, body=None):
        allowed = {("get", PLAN + "/usages"), ("get", SITE), ("get", SITE + "/config/web"),
                   ("get", SITE + "/config/logs"), ("patch", SITE + "/config/web"), ("put", SITE + "/config/logs")}
        require((method, resource) in allowed, "OPERATION_NOT_ALLOWED")
        if method == "get":
            require(body is None, "GET_BODY_REJECTED")
        if method == "patch":
            require(isinstance(body, dict) and body in ({"properties": {"linuxFxVersion": NEW, "appCommandLine": ""}}, self.rollback), "IMAGE_DELTA_REJECTED")
        if method == "put":
            require(self.logging is not None and body == {"properties": self.logging}, "LOGGING_DELTA_REJECTED")
        args = self.entry + ["rest", "--method", method, "--url", "https://management.azure.com" + resource + "?api-version=2023-12-01"]
        if body is not None:
            args += ["--body", "@" + str(self.store.write("body.json", body))]
        raw = capture(args + ["--only-show-errors", "-o", "json"])
        try:
            result = json.loads(raw)
        except (ValueError, UnicodeError):
            raise Blocked("RESPONSE_SCHEMA") from None
        if method == "get" and resource != PLAN + "/usages":
            require(result.get("id", "").lower() == resource.lower(), "RESOURCE_IDENTITY")
        return result

    def enable_logging(self):
        capture(self.entry + ["webapp", "log", "config", "--subscription", SUB, "-g", GROUP,
                              "-n", APP, "--docker-container-logging", "filesystem", "--only-show-errors", "-o", "none"], 20)

    def observe(self):
        raw = capture(self.entry + ["webapp", "log", "tail", "--subscription", SUB, "-g", GROUP,
                                    "-n", APP, "--only-show-errors"], 30, stream=True)
        return classify(raw)


def quota(adapter):
    values = adapter.request("get", PLAN + "/usages").get("value")
    require(isinstance(values, list), "QUOTA_SCHEMA")
    stops = [v for v in values if v.get("name", {}).get("value") == "WPStopRequests"]
    require(len(stops) == 1, "QUOTA_SCHEMA")
    x = stops[0]
    require(type(x.get("currentValue")) is int and x["currentValue"] >= 0 and type(x.get("limit")) is int and x["limit"] > 0 and x.get("unit") == "Count", "QUOTA_SCHEMA")
    require(x["currentValue"] < x["limit"], "QUOTA_EXCEEDED")


def web_pair(value):
    p = value.get("properties", {})
    require(all(k in p for k in ("linuxFxVersion", "appCommandLine")), "WEB_INCOMPLETE")
    return {k: p[k] for k in ("linuxFxVersion", "appCommandLine")}


def execute(adapter, store, approved=False, rollback_approved=False):
    result = {"result": "PLAN_ONLY", "target": APP, "candidate": NEW[7:],
              "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}
    if not approved:
        return result
    require(rollback_approved, "CONDITIONAL_ROLLBACK_APPROVAL_REQUIRED")
    baseline = None
    logging_touched = image_touched = False
    post_quota_checked = post_quota_ok = False
    try:
        quota(adapter)
        site_doc = adapter.request("get", SITE)
        require("linux" in site_doc.get("kind", "").lower(), "SITE_OS_UNVERIFIED")
        site = site_doc.get("properties", {})
        require(site.get("serverFarmId", "").lower() == PLAN.lower() and site.get("state") == "Running" and site.get("usageState") == "Normal" and site.get("availabilityState") == "Normal" and type(site.get("httpsOnly")) is bool, "SITE_PRECONDITION")
        web = adapter.request("get", SITE + "/config/web")
        pair = web_pair(web)
        require(pair == {"linuxFxVersion": OLD, "appCommandLine": ""}, "BASELINE_DRIFT")
        logs = adapter.request("get", SITE + "/config/logs").get("properties")
        require(isinstance(logs, dict) and all(k in logs for k in ("applicationLogs", "httpLogs", "failedRequestsTracing", "detailedErrorMessages")), "LOGGING_BASELINE_INCOMPLETE")
        baseline = {"site": copy.deepcopy(site), "web": pair, "logs": copy.deepcopy(logs)}
        store.write("baseline.json", baseline)
        adapter.rollback = {"properties": pair}
        adapter.logging = copy.deepcopy(logs)
        store.write("recovery.json", {"resource": SITE, "imageRollback": adapter.rollback, "loggingRollback": {"properties": logs}, "noRestart": True})
        require(web_pair(adapter.request("get", SITE + "/config/web")) == pair, "PRE_APPLY_DRIFT")
        logging_touched = True
        store.write("ledger.json", {"loggingMayHaveChanged": True, "imageMayHaveChanged": False})
        adapter.enable_logging()
        image_touched = True
        store.write("ledger.json", {"loggingMayHaveChanged": True, "imageMayHaveChanged": True})
        adapter.request("patch", SITE + "/config/web", {"properties": {"linuxFxVersion": NEW, "appCommandLine": ""}})
        require(web_pair(adapter.request("get", SITE + "/config/web")) == {"linuxFxVersion": NEW, "appCommandLine": ""}, "CANDIDATE_READBACK")
        observation = adapter.observe()
        result["observation"] = observation
        post_quota_checked = True
        quota(adapter)
        post_quota_ok = True
        # Generic SCM log output has no trusted running-image binding. Never retain a candidate on that evidence alone.
        require(observation.get("candidateBound") is True and observation.get("listenerMarker") is True, "ATTRIBUTABLE_STARTUP_NOT_ESTABLISHED")
        result["result"] = "DIAGNOSTIC_OBSERVATION_ONLY"
    except Exception as error:
        result["result"] = str(error) if isinstance(error, Blocked) else "UNEXPECTED_FAILURE"
        if image_touched and baseline:
            try:
                if post_quota_checked:
                    require(post_quota_ok, "POST_QUOTA_UNSAFE")
                else:
                    quota(adapter)  # One recovery snapshot if observation never reached its quota check.
                adapter.request("patch", SITE + "/config/web", adapter.rollback)
                require(web_pair(adapter.request("get", SITE + "/config/web")) == baseline["web"], "ROLLBACK_READBACK")
                result["imageRollbackVerified"] = True
            except Exception:
                result["imageRollbackVerified"] = False
                result["manualRecoveryRequired"] = True
    finally:
        if logging_touched and baseline:
            try:
                adapter.request("put", SITE + "/config/logs", {"properties": baseline["logs"]})
                require(adapter.request("get", SITE + "/config/logs").get("properties") == baseline["logs"], "LOGGING_RESTORE_MISMATCH")
                result["loggingRestoreVerified"] = True
            except Exception:
                result["loggingRestoreVerified"] = False
                result["manualRecoveryRequired"] = True
    return result


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--apply", action="store_true")
    parser.add_argument("--approve-exact-live-apply", action="store_true")
    parser.add_argument("--approve-conditional-rollback", action="store_true")
    args = parser.parse_args(argv)
    if not args.apply:
        result = execute(None, None)
    else:
        require(LIVE_ADAPTER_QUALIFIED, "LIVE_APPLY_BLOCKED_SCM_AND_ACL_UNQUALIFIED")
        require(args.approve_exact_live_apply, "EXACT_LIVE_APPROVAL_REQUIRED")
        require(args.approve_conditional_rollback, "CONDITIONAL_ROLLBACK_APPROVAL_REQUIRED")
        store = PrivateStore()
        result = execute(Azure(store), store, True, True)
    print(json.dumps(result, sort_keys=True))
    return 0 if result["result"] == "PLAN_ONLY" else 2


if __name__ == "__main__":
    try:
        raise SystemExit(main())
    except Blocked as error:
        print(json.dumps({"result": str(error), "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}))
        raise SystemExit(2)
