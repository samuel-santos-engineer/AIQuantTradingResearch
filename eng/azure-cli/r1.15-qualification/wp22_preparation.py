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


class ReadOnlyAzure(R.Azure):
    def request(self, method, suffix="", body=None, resource=R.SITE, kind=None):
        R.require((method, suffix, resource) in READS and body is None and kind is None,
                  "READ_ONLY_OPERATION_REQUIRED")
        value=super().request(method, suffix, resource=resource)
        R.require(isinstance(value,dict) and not value.get("nextLink"), "COMPLETE_RESPONSE_REQUIRED")
        if resource==R.PLAN:
            R.require(''.join(c for c in value["location"].lower() if c.isalnum())=="westus2",
                      "PLAN_REGION_DRIFT")
        return value


def report():
    return dict(result="PREPARATION_INCOMPLETE", attemptPrepared=False,
        offlinePreflightPassed=False, azureBaselineVerified=False,
        serviceKeyChannelAvailable=False, approvalEnabled=False,
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
    try:
        template(directory)
        result.update(attemptPrepared=True,attemptId=directory.name)
        gate=gate or C.WP19.offline_report
        R.require(gate().get("result")=="OFFLINE_PREFLIGHT_PASS", "OPERATOR_PREFLIGHT_REQUIRED")
        result["offlinePreflightPassed"]=True
        adapter=adapter or ReadOnlyAzure()
        first=adapter.capture();complete(first)
        second=adapter.capture();complete(second)
        R.require(first==second,"BASELINE_DRIFT")
        template(directory)  # Recheck freshness/code/disabled flags after bounded reads.
        key=os.environ.get("AIQ_WP21_SERVICE_KEY") or first["settings"].get("AIQ_RESEARCH_API_KEY","")
        result["serviceKeyChannelAvailable"]=isinstance(key,str) and 32<=len(key)<=256 and all('!'<=c<='~' for c in key)
        result.update(result="READ_ONLY_BASELINE_VERIFIED_NOT_AUTHORIZED",azureBaselineVerified=True)
    except Exception:
        # Never publish exception messages, configuration values, settings names or paths.
        result.update(result="AZURE_BASELINE_UNVERIFIED",failureReason="PREREQUISITE_OR_AZURE_CAPTURE_UNVERIFIED")
    result["observedUtc"]=R.utc().isoformat()
    if result["attemptPrepared"]:
        # New-only sanitized observation; never a live rollback baseline or consumed marker.
        R.save_new(directory/("wp22-observation-"+str(uuid.uuid4())+".json"),result)
    return result


def main(argv=None):
    parser=argparse.ArgumentParser(description=__doc__)
    modes=parser.add_mutually_exclusive_group()
    modes.add_argument("--prepare-private-attempt",action="store_true")
    modes.add_argument("--inspect-azure-read-only",action="store_true")
    args=parser.parse_args(argv)
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
        value=report();value.update(result="PREPARATION_BLOCKED",failureReason="LOCAL_PREREQUISITE_UNVERIFIED")
        print(json.dumps(value));return 1


if __name__=="__main__":raise SystemExit(main())
