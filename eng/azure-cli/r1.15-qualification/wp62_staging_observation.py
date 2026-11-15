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


class Blocked(Exception):
    pass


def require(ok, code):
    if not ok:
        raise Blocked(code)


def capture(command, seconds=15, stream=False, popen=subprocess.Popen):
    """Combined bounded pipes; direct executable, no batch shell or output persistence."""
    started = time.monotonic()
    proc = popen(command, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    buffers = [bytearray(), bytearray()]
    lock = threading.Lock()
    overflow = threading.Event()

    def drain(pipe, target):
        while True:
            chunk = pipe.read(4096)
            if not chunk:
                return
            with lock:
                if sum(map(len, buffers)) + len(chunk) > LIMIT:
                    overflow.set()
                    proc.kill()
                    return
                target.extend(chunk)

    threads = [threading.Thread(target=drain, args=(pipe, buf), daemon=True)
               for pipe, buf in zip((proc.stdout, proc.stderr), buffers)]
    for thread in threads:
        thread.start()
    expired = False
    try:
        proc.wait(timeout=max(0.001, seconds - (time.monotonic() - started)))
    except subprocess.TimeoutExpired:
        expired = True
        proc.kill()
        proc.wait(timeout=5)
    for thread in threads:
        thread.join(timeout=1)
    require(not any(t.is_alive() for t in threads), "PIPE_CLEANUP_UNVERIFIED")
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
        require(proc.returncode == 0 and proc.stdout.strip() == b"ACL_VERIFIED", "PRIVATE_ACL_UNVERIFIED")

    def write(self, name, value):
        require(name in {"baseline.json", "recovery.json", "ledger.json", "body.json"}, "PRIVATE_NAME_REJECTED")
        path = self.directory / name
        path.write_text(json.dumps(value) + "\n", encoding="utf-8", newline="\n")
        return path


class Azure:
    def __init__(self, store):
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
