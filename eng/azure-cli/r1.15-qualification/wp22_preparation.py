"""Offline by default; explicit read-only ARM inspection, never live execution."""
import argparse
import datetime as dt
import json
import os
import re
from pathlib import Path
import subprocess
import sys
import uuid
import wp21_runtime as R
import wp21_controller as C

READS = frozenset({("get", "", R.SITE), ("get", "", R.PLAN),
    ("get", "/config/web", R.SITE), ("post", "/config/appsettings/list", R.SITE),
    ("post", "/config/connectionstrings/list", R.SITE), ("get", "/config/logs", R.SITE),
    ("get", "/config/slotConfigNames", R.SITE), ("get", "/slots", R.SITE)})


# All externally visible diagnostics are fixed enums, never exception text.
REQUEST_NAMES = {
    ("get", "", R.SITE): "SITE", ("get", "", R.PLAN): "PLAN",
    ("get", "/config/web", R.SITE): "WEB",
    ("post", "/config/appsettings/list", R.SITE): "APP_SETTINGS",
    ("post", "/config/connectionstrings/list", R.SITE): "CONNECTION_STRINGS",
    ("get", "/config/logs", R.SITE): "LOGS",
    ("get", "/config/slotConfigNames", R.SITE): "SLOT_FLAGS",
    ("get", "/slots", R.SITE): "SLOTS"}
STAGES = frozenset({"PRIVATE_ATTEMPT_TEMPLATE", "PRIVATE_ATTEMPT_PREPARATION", "OFFLINE_QUALIFICATION",
    "OPERATOR_PREFLIGHT", "BASELINE_COMPARISON",
    "POST_CAPTURE_TEMPLATE_RECHECK", "SERVICE_KEY_CHANNEL_CHECK", "OBSERVATION_WRITE"} |
    {prefix+name+suffix for prefix in ("FIRST_CAPTURE_", "SECOND_CAPTURE_")
     for name in REQUEST_NAMES.values() for suffix in ("", "_RESPONSE_VALIDATION")} |
    {prefix+"VALIDATION" for prefix in ("FIRST_CAPTURE_", "SECOND_CAPTURE_")})
CATEGORIES = frozenset({"LOCAL_PREREQUISITE_UNVERIFIED", "AZURE_CLI_UNAVAILABLE",
    "AZURE_TRANSPORT_FAILURE", "AZURE_RESOURCE_RESPONSE_INVALID", "AZURE_RESPONSE_SCHEMA_MISMATCH",
    "AZURE_TARGET_OR_PLAN_MISMATCH", "AZURE_BASELINE_DRIFT", "AZURE_READ_TIMEOUT",
    "AZURE_BASELINE_INCOMPLETE", "OBSERVATION_PERSISTENCE_FAILED", "UNCLASSIFIED_FAILURE"})
# Match only known internal Blocked codes; never parse Azure/stderr/error bodies.
CODE_CATEGORIES = {
    "AZURE_CLI_UNAVAILABLE": "AZURE_CLI_UNAVAILABLE",
    "AZURE_OPERATION_FAILED": "AZURE_TRANSPORT_FAILURE",
    "COMPLETE_RESPONSE_REQUIRED": "AZURE_RESOURCE_RESPONSE_INVALID",
    "PLAN_REGION_DRIFT": "AZURE_TARGET_OR_PLAN_MISMATCH",
    "RESOURCE_DRIFT": "AZURE_TARGET_OR_PLAN_MISMATCH",
    "REGION_DRIFT": "AZURE_TARGET_OR_PLAN_MISMATCH",
    "EXISTING_F1_REQUIRED": "AZURE_TARGET_OR_PLAN_MISMATCH",
    "HOST_KIND_UNSUPPORTED": "AZURE_TARGET_OR_PLAN_MISMATCH",
    "BASELINE_TARGET_MISMATCH": "AZURE_TARGET_OR_PLAN_MISMATCH",
    "BASELINE_DRIFT": "AZURE_BASELINE_DRIFT",
    **{code: "AZURE_BASELINE_INCOMPLETE" for code in (
        "INCOMPLETE_BASELINE", "BASELINE_STATE_INVALID", "BASELINE_STARTUP_MISSING",
        "BASELINE_SETTINGS_INVALID", "BASELINE_METADATA_MISSING",
        "BASELINE_CONNECTION_METADATA_UNVERIFIED", "BASELINE_SLOT_FLAGS_MISSING",
        "BASELINE_LOGGING_MISSING", "BASELINE_HOST_UNVERIFIED", "BASELINE_STARTUP_UNVERIFIED",
        "BASELINE_SLOT_METADATA_UNVERIFIED", "BASELINE_LOGGING_UNVERIFIED")}}


def category(error, stage):
    if stage == "OBSERVATION_WRITE": return "OBSERVATION_PERSISTENCE_FAILED"
    if not stage.startswith(("FIRST_CAPTURE_", "SECOND_CAPTURE_", "BASELINE_COMPARISON")):
        return "LOCAL_PREREQUISITE_UNVERIFIED"
    if isinstance(error, (subprocess.TimeoutExpired, TimeoutError)): return "AZURE_READ_TIMEOUT"
    if isinstance(error, TransportFailure) and error.transport_category=="PARENT_TIMEOUT":
        return "AZURE_READ_TIMEOUT"
    if isinstance(error, R.Blocked):
        code = error.args[0] if len(error.args) == 1 and type(error.args[0]) is str else None
        return CODE_CATEGORIES.get(code, "UNCLASSIFIED_FAILURE")
    if isinstance(error, (KeyError, TypeError, ValueError, AttributeError)):
        return "AZURE_RESPONSE_SCHEMA_MISMATCH"
    if isinstance(error, OSError): return "AZURE_TRANSPORT_FAILURE"
    return "UNCLASSIFIED_FAILURE"


TRANSPORT_CATEGORIES = frozenset({"CLI_EXECUTABLE_UNAVAILABLE", "WORKER_LAUNCH_FAILURE",
    "PARENT_TIMEOUT", "WORKER_NONZERO_EXIT", "INVALID_WORKER_OUTPUT", "WORKER_IO_FAILURE", "UNKNOWN"})


class TransportFailure(R.Blocked):
    def __init__(self, value):
        self.transport_category=value if type(value) is str and value in TRANSPORT_CATEGORIES else "UNKNOWN"
        # The unchanged worker protocol does not expose trustworthy HTTP status.
        self.http_status=None
        super().__init__("AZURE_CLI_UNAVAILABLE" if self.transport_category=="CLI_EXECUTABLE_UNAVAILABLE"
            else "AZURE_OPERATION_FAILED")


def diagnostic_cli(arguments):
    """Read-only caller only. Never decode stderr or infer status from worker text."""
    interpreter=Path(os.environ.get("ProgramFiles", r"C:\Program Files"))/"Microsoft SDKs/Azure/CLI2/python.exe"
    if not interpreter.is_file(): raise TransportFailure("CLI_EXECUTABLE_UNAVAILABLE")
    directory=Path(os.environ["AIQ_WP21_ATTEMPT_DIRECTORY"])
    try:
        child=subprocess.Popen([str(interpreter),"-I","-c",R.BRIDGE],stdin=subprocess.PIPE,
            stdout=subprocess.PIPE,stderr=subprocess.PIPE,
            creationflags=getattr(subprocess,"CREATE_NO_WINDOW",0))
    except OSError:
        raise TransportFailure("WORKER_LAUNCH_FAILURE") from None
    failure="UNKNOWN"
    try:
        identity=R.worker_identity(child.pid)
        R.save_new(directory/("management-worker-"+str(uuid.uuid4())+".json"),identity)
        # Ownership must be persisted before any request is supplied.
        try:
            output,unused_errors=child.communicate(input=R.encoded(arguments+["--only-show-errors"]),timeout=45)
        except subprocess.TimeoutExpired:
            failure="PARENT_TIMEOUT";raise
        except OSError:
            failure="WORKER_IO_FAILURE";raise
        if type(child.returncode) is not int or child.returncode != 0:
            failure="WORKER_NONZERO_EXIT";raise TransportFailure(failure)
        failure="INVALID_WORKER_OUTPUT"
        if not isinstance(output,bytes) or len(output)>8*1024*1024:
            raise TransportFailure(failure)
        value=json.loads(output)
        if not isinstance(value,dict): raise TransportFailure(failure)
        return value
    except Exception:
        try:
            if child.poll() is None:
                child.kill();child.communicate(timeout=5)
        except Exception:
            # Cleanup cannot be claimed from incomplete process API evidence.
            failure="UNKNOWN"
        raise TransportFailure(failure) from None
    except BaseException:
        # Preserve interruption semantics while still cleaning only our owned child.
        if child.poll() is None:
            child.kill();child.communicate(timeout=5)
        raise


class ReadOnlyAzure(R.Azure):
    def transport(self, arguments):
        return diagnostic_cli(arguments)

    def __init__(self):
        self.capture_prefix = "FIRST_CAPTURE_"
        self.stage = lambda value: None

    def request(self, method, suffix="", body=None, resource=R.SITE, kind=None):
        operation=(method, suffix, resource)
        R.require(operation in READS and body is None and kind is None,
                  "READ_ONLY_OPERATION_REQUIRED")
        name=self.capture_prefix+REQUEST_NAMES[operation]
        self.stage(name)
        value=super().request(method, suffix, resource=resource)
        self.stage(name+"_RESPONSE_VALIDATION")
        R.require(isinstance(value,dict) and not value.get("nextLink"), "COMPLETE_RESPONSE_REQUIRED")
        if resource==R.PLAN:
            R.require(''.join(c for c in value["location"].lower() if c.isalnum())=="westus2",
                      "PLAN_REGION_DRIFT")
        return value

    def capture(self):
        # Same eight operations and validations as WP21. Only the read-only
        # subclass is instrumented; live deployment/recovery are untouched.
        site=self.request("get");p=site["properties"]
        R.require(site["id"].lower()==R.SITE.lower() and p["serverFarmId"].lower()==R.PLAN.lower(), "RESOURCE_DRIFT")
        R.require(site["kind"] in ("app,linux,container", "app,linux"), "HOST_KIND_UNSUPPORTED")
        R.require(''.join(c for c in site["location"].lower() if c.isalnum())=="westus2", "REGION_DRIFT")
        plan=self.request("get",resource=R.PLAN)
        R.require(plan["id"].lower()==R.PLAN.lower() and plan["sku"]["name"]=="F1", "EXISTING_F1_REQUIRED")
        # Site extraction is attributed to its response, not the preceding plan GET.
        self.stage(self.capture_prefix+"SITE_RESPONSE_VALIDATION")
        value={"site": {"id":site["id"], "kind":site["kind"], "location":site["location"],
            **{k:p[k] for k in ("serverFarmId","state","httpsOnly","publicNetworkAccess")}},
            "plan":{"id":plan["id"],"sku":plan["sku"]}}
        for key,method,route in (("web","get","/config/web"),
                ("settings","post","/config/appsettings/list"),
                ("connections","post","/config/connectionstrings/list"),
                ("logs","get","/config/logs"),("slotFlags","get","/config/slotConfigNames")):
            value[key]=self.request(method,route)["properties"]
        value["slots"]=self.request("get","/slots")["value"]
        self.stage(self.capture_prefix+"VALIDATION")
        R.validate_baseline(value)
        return value


def report():
    return dict(result="PREPARATION_INCOMPLETE", attemptPrepared=False,
        offlinePreflightPassed=False, azureBaselineVerified=False,
        serviceKeyChannelAvailable=False, serviceKeyChannelChecked=False,
        failedStage=None, failureCategory=None, transportFailureCategory=None, httpStatus=None,
        approvalEnabled=False,
        liveControllerInvoked=False, liveAttemptReady=False,
        RESEARCH_PROCESS_ALIVE="UNVERIFIED", AUTHENTIC_DATA_NOT_IMPORTED=True,
        PUBLIC_RUNTIME_VERIFIED=False, AZURE_DEPLOYMENT_NOT_RUN=True)


def template(directory):
    R.private_directory(directory)
    value=R.load(directory/"approval.json")
    R.require(set(value)=={"attemptId","target","plan","image","issuedUtc","expiresUtc",
        "explicitLiveApproval","allowRecovery","code"}, "DISABLED_TEMPLATE_REQUIRED")
    R.require(str(uuid.UUID(value["attemptId"]))==value["attemptId"]==directory.name,
              "ATTEMPT_ID_MISMATCH")
    R.require(value["target"]==R.SITE and value["plan"]==R.PLAN and value["image"]==R.IMAGE,
              "EXACT_TARGET_IMAGE_REQUIRED")
    R.require(value["explicitLiveApproval"] is False and value["allowRecovery"] is False,
              "DISABLED_TEMPLATE_REQUIRED")
    R.require(value["code"]==R.identities(), "REVIEWED_CODE_MISMATCH")
    issued=dt.datetime.fromisoformat(value["issuedUtc"])
    expires=dt.datetime.fromisoformat(value["expiresUtc"])
    R.require(issued.tzinfo is not None and expires.tzinfo is not None and
        issued<=R.utc()<=expires and dt.timedelta(0)<expires-issued<=dt.timedelta(hours=1),
        "TEMPLATE_EXPIRED_OR_NOT_FRESH")
    R.require(not (directory/"baseline.json").exists() and not (directory/"consumed.json").exists(),
              "ATTEMPT_ALREADY_ARMED_OR_CONSUMED")
    return value


def complete(b):
    R.validate_baseline(b)
    s=b["site"]
    R.require(s["state"]=="Running" and s["kind"] in ("app,linux","app,linux,container") and
        ''.join(c for c in s["location"].lower() if c.isalnum())=="westus2" and
        s["publicNetworkAccess"] in (None,"Enabled","Disabled"), "BASELINE_HOST_UNVERIFIED")
    R.require(all(isinstance(b["web"][k],str) for k in ("linuxFxVersion","appCommandLine")),
              "BASELINE_STARTUP_UNVERIFIED")
    R.require(all(isinstance(b["slotFlags"][k],list) and
        all(isinstance(x,str) for x in b["slotFlags"][k])
        for k in ("appSettingNames","connectionStringNames")), "BASELINE_SLOT_METADATA_UNVERIFIED")
    R.require(all(isinstance(b["logs"][k],dict) for k in
        ("applicationLogs","httpLogs","detailedErrorMessages","failedRequestsTracing")),
        "BASELINE_LOGGING_UNVERIFIED")


def inspect(directory, adapter=None, gate=None):
    result=report()
    stage="PRIVATE_ATTEMPT_TEMPLATE"

    def set_stage(value):
        nonlocal stage
        R.require(value in STAGES, "INVALID_DIAGNOSTIC_STAGE")
        stage=value

    def fail(error):
        # No arbitrary error evidence, messages or attributes are copied.
        result["transportFailureCategory"]=(error.transport_category if isinstance(error,TransportFailure)
            and error.transport_category in TRANSPORT_CATEGORIES else
            "UNKNOWN" if stage.startswith(("FIRST_CAPTURE_","SECOND_CAPTURE_"))
            and not stage.endswith(("RESPONSE_VALIDATION","VALIDATION")) else None)
        result["httpStatus"]=None
        if stage=="OBSERVATION_WRITE": result["transportFailureCategory"]=None
        result.update(result="AZURE_BASELINE_UNVERIFIED",azureBaselineVerified=False,
            failedStage=stage,failureCategory=category(error,stage),
            failureReason="PREREQUISITE_OR_AZURE_CAPTURE_UNVERIFIED")

    try:
        template(directory)
        result.update(attemptPrepared=True,attemptId=directory.name)
        set_stage("OPERATOR_PREFLIGHT")
        gate=gate or C.WP19.offline_report
        R.require(gate().get("result")=="OFFLINE_PREFLIGHT_PASS", "OPERATOR_PREFLIGHT_REQUIRED")
        result["offlinePreflightPassed"]=True
        adapter=adapter or ReadOnlyAzure()
        if isinstance(adapter,ReadOnlyAzure): adapter.stage=set_stage
        captures=[]
        for prefix in ("FIRST_CAPTURE_", "SECOND_CAPTURE_"):
            # Injection seam for offline tests; real requests overwrite this stage.
            set_stage(prefix+"VALIDATION")
            if isinstance(adapter,ReadOnlyAzure): adapter.capture_prefix=prefix
            current=adapter.capture()
            set_stage(prefix+"VALIDATION");complete(current)
            captures.append(current)
        set_stage("BASELINE_COMPARISON")
        R.require(captures[0]==captures[1],"BASELINE_DRIFT")
        set_stage("POST_CAPTURE_TEMPLATE_RECHECK")
        template(directory)
        set_stage("SERVICE_KEY_CHANNEL_CHECK")
        key=os.environ.get("AIQ_WP21_SERVICE_KEY") or captures[0]["settings"].get("AIQ_RESEARCH_API_KEY","")
        result["serviceKeyChannelAvailable"]=isinstance(key,str) and 32<=len(key)<=256 and all('!'<=c<='~' for c in key)
        result["serviceKeyChannelChecked"]=True
        result.update(result="READ_ONLY_BASELINE_VERIFIED_NOT_AUTHORIZED",azureBaselineVerified=True)
    except Exception as error:
        fail(error)
    result["observedUtc"]=R.utc().isoformat()
    if result["attemptPrepared"]:
        set_stage("OBSERVATION_WRITE")
        try:
            R.require(len(R.encoded(result))<=4096,"REPORT_TOO_LARGE")
            R.save_new(directory/("wp22-observation-"+str(uuid.uuid4())+".json"),result)
        except Exception as error:
            # A failed write is not retried and cannot produce a success report.
            fail(error)
    return result


def main(argv=None):
    parser=argparse.ArgumentParser(description=__doc__)
    modes=parser.add_mutually_exclusive_group()
    modes.add_argument("--prepare-private-attempt",action="store_true")
    modes.add_argument("--inspect-azure-read-only",action="store_true")
    args=parser.parse_args(argv)
    entry_stage=("PRIVATE_ATTEMPT_PREPARATION" if args.prepare_private_attempt else
                 "PRIVATE_ATTEMPT_TEMPLATE" if args.inspect_azure_read_only else "OFFLINE_QUALIFICATION")
    try:
        if args.prepare_private_attempt:
            prepared=C.prepare_private_attempt()
            value=report();value.update(result="PRIVATE_ATTEMPT_PREPARED_NOT_AUTHORIZED",
                attemptPrepared=True,attemptId=prepared["attemptId"])
        elif args.inspect_azure_read_only:
            value=inspect(Path(os.environ["AIQ_WP21_ATTEMPT_DIRECTORY"]))
        else:
            tests=subprocess.run([sys.executable,"-B","-m","unittest","discover","-s",
                str(R.ROOT/"tests/runtime/release_1_15"),"-p","test_wp22_preparation.py","-q"],
                capture_output=True,timeout=90)
            summary=tests.stderr.decode("utf-8",errors="replace")
            count=re.search(r"Ran ([0-9]+) tests",summary)
            passed=tests.returncode==0 and count is not None and int(count[1])>=17 and "\nOK" in summary
            value=report();value.update(result="OFFLINE_TESTS_PASS" if passed else "OFFLINE_TESTS_FAILED",
                offlineTests=int(count[1]) if count else 0)
        data=R.encoded(value)
        R.require(len(data)<=4096,"REPORT_TOO_LARGE")
        print(data.decode("utf-8"))
        return 0 if value["result"] in ("OFFLINE_TESTS_PASS","PRIVATE_ATTEMPT_PREPARED_NOT_AUTHORIZED","READ_ONLY_BASELINE_VERIFIED_NOT_AUTHORIZED") else 1
    except Exception:
        value=report();value.update(result="PREPARATION_BLOCKED",failureReason="LOCAL_PREREQUISITE_UNVERIFIED",
            failedStage=entry_stage,failureCategory="LOCAL_PREREQUISITE_UNVERIFIED")
        print(json.dumps(value));return 1


if __name__=="__main__":raise SystemExit(main())
