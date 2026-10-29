"""WP21 default offline qualification. --execute requires private fresh authority."""
import argparse
import importlib.util
import json
import os
import re
import uuid
from pathlib import Path
import subprocess
import sys
import wp21_runtime as R

SPEC = importlib.util.spec_from_file_location("wp19", R.HERE / "wp19_operator_handoff.py")
WP19 = importlib.util.module_from_spec(SPEC);SPEC.loader.exec_module(WP19)
WP19.SCRIPTS.update({k: R.FILES[k] for k in ("qualificationEntry", "liveEntry", "recoveryEntry")})


def run(directory, approval, adapter, gate=WP19.WP18.preflight, observer=R.health, snapshot=R.unauthenticated_snapshot):
    R.authorization(approval, directory)
    R.require(not (directory/"baseline.json").exists() and not (directory/"consumed.json").exists(), "ATTEMPT_CONSUMED")
    R.require(gate().get("result") == "LOCAL_PREFLIGHT_PASS", "LOCAL_PREFLIGHT_BLOCKED")  # Real -File route, even for direct --execute.
    baseline = adapter.capture();R.validate_baseline(baseline)
    R.require(baseline["site"]["state"] == "Running", "ORIGINAL_RUNNING_STATE_REQUIRED")
    key = os.environ.get("AIQ_WP21_SERVICE_KEY") or baseline["settings"].get("AIQ_RESEARCH_API_KEY", "")
    R.require(32 <= len(key) <= 256 and all('!' <= c <= '~' for c in key), "PRIVATE_SERVICE_KEY_UNAVAILABLE")
    R.require(adapter.capture() == baseline, "PREWRITE_BASELINE_DRIFT")
    R.authorization(approval, directory)  # Refuse expiry/code changes during baseline reads.
    envelope = {"approval": approval, "controllerPid": os.getpid(), "baseline": baseline, "baselineHash": R.sha(R.encoded(baseline)), "approvalHash": R.sha(R.encoded(approval))}
    R.save_new(directory/"baseline.json", envelope)
    R.save_new(directory/"consumed.json", {"attemptId": approval["attemptId"], "baselineHash": envelope["baselineHash"], "approvalHash": envelope["approvalHash"], "controllerPid": os.getpid()})
    result = {"result": "INCOMPLETE", "RESEARCH_PROCESS_ALIVE": "UNVERIFIED", "AUTHENTIC_DATA_NOT_IMPORTED": True,
              "PUBLIC_RUNTIME_VERIFIED": False, "snapshots": {}, "attemptId": approval["attemptId"]}
    try:
        adapter.deploy(baseline)
        R.require(observer() is True, "HEALTH_NOT_ALIVE")  # Immediately after Start.
        result["RESEARCH_PROCESS_ALIVE"] = True
        result["healthObservedUtc"] = R.utc().isoformat()
        R.save_new(directory/"health-alive.json", {"attemptId": approval["attemptId"], "collectorHash": approval["code"]["collector"]})
        for instrument in ("BTC/USD", "ETH/USD"):
            snapshot(instrument)
            result["snapshots"][instrument] = "VALIDATED_UNAUTHENTICATED_401"
        result["result"] = "RESEARCH_HTTPS_CHECKS_COMPLETE"
    except BaseException as error:
        result["result"] = "QUALIFICATION_FAILED"  # KeyboardInterrupt/SystemExit also recover.
        result["failureReason"] = str(error) if isinstance(error,R.Blocked) else type(error).__name__
        if isinstance(error,R.Blocked) and error.evidence is not None: result["healthEvidence"] = error.evidence
    finally:
        try:
            recovery = R.recover(directory, adapter, controller_cleanup=True)
            result["rollbackVerified"] = recovery["rollbackVerified"]
            result["unrestoredFields"] = recovery["unrestoredFields"]
        except BaseException:
            result["rollbackVerified"] = False
        if not result["rollbackVerified"]: result["result"] = "CRITICAL_RECOVERY_UNVERIFIED"
        R.save_new(directory/"result.json", result)
    return result


def snapshot_worker(instrument):
    import http.client
    import ssl
    import time
    sys.path.insert(0, str(R.ROOT / "python/presentation"))
    from research_snapshot_client import failure_reason
    R.require(instrument in ("BTC/USD", "ETH/USD"), "INSTRUMENT_NOT_ALLOWED")
    start = time.monotonic()
    connection = http.client.HTTPSConnection(R.APP+".azurewebsites.net", timeout=5, context=ssl.create_default_context())
    try:
        connection.connect()
        remaining = 5-(time.monotonic()-start);R.require(remaining>0, "SNAPSHOT_DEADLINE")
        connection.sock.settimeout(remaining)
        body = R.encoded({"schemaVersion": "1.0", "instrument": instrument, "requestEpoch": "1", "knownIdentity": None})
        connection.request("POST", "/v1/research/snapshot", body=body, headers={"Content-Type": "application/json", "Accept-Encoding": "identity"})
        response = connection.getresponse()
        R.require(response.status == 401 and response.getheader("Content-Encoding", "identity") == "identity", "UNAUTHENTICATED_CHECK_FAILED")
        R.require(response.getheader("Content-Type", "").lower().replace(" ", "") in ("application/json", "application/json;charset=utf-8"), "INVALID_ERROR_MEDIA")
        data = response.read(4097)
        R.require(len(data)<=4096 and time.monotonic()-start<=5 and failure_reason(data,401,instrument,1)=="AuthenticationRejected", "INVALID_401_ENVELOPE")
        print("VALIDATED_401")
    finally: connection.close()


def offline_report():
    R.require(sys.version_info >= (3,12), "PYTHON_312_OR_NEWER_REQUIRED")
    # Execute only the bounded offline suite; it mocks every Azure/HTTP boundary.
    tests = subprocess.run([sys.executable, "-B", "-m", "unittest", "discover", "-s", str(R.ROOT/"tests/runtime/release_1_15"),
                            "-p", "test_wp21_controller.py", "-q"], capture_output=True, timeout=90)
    summary = tests.stderr.decode("utf-8", errors="replace")
    count = re.search(r"Ran ([0-9]+) tests", summary)
    passed = tests.returncode == 0 and count is not None and int(count[1]) >= 32 and "\nOK" in summary
    local = WP19.offline_report()
    report = {"result": "OFFLINE_QUALIFICATION_COMPLETE" if passed else "OFFLINE_QUALIFICATION_FAILED",
              "privateControllerQualified": passed, "offlineTests": int(count[1]) if count else 0, "qualificationBasis": "MOCKED_AZURE_HTTP_AND_OWNED_LOCAL_WORKER_TEST",
              "operatorPreflightPassed": local["result"] == "OFFLINE_PREFLIGHT_PASS", "operatorPreflight": local, "liveAttemptReady": False,
              "blockers": ["FRESH_PRIVATE_APPROVAL_AND_ACL_DIRECTORY_REQUIRED", "FRESH_LIVE_BASELINE_NOT_CAPTURED", "SERVICE_KEY_CHANNEL_NOT_VERIFIED"],
              "pythonVersion": ".".join(str(v) for v in sys.version_info[:3]), "code": R.identities(), "approvedImageDigest": R.DIGEST, "target": R.APP, "RESEARCH_PROCESS_ALIVE": "UNVERIFIED", "AUTHENTIC_DATA_NOT_IMPORTED": True,
              "PUBLIC_RUNTIME_VERIFIED": False, "AZURE_DEPLOYMENT_NOT_RUN": True}
    if not report["operatorPreflightPassed"]: report["blockers"].append("OPERATOR_SESSION_PREFLIGHT_REQUIRED")
    return report



def prepare_private_attempt():
    # Fresh operator-owned directory only; never modifies historical WP17 evidence.
    parent=Path(os.environ["LOCALAPPDATA"])/"AIQ-WP21"
    parent.mkdir(parents=True,exist_ok=True)
    directory=parent/str(uuid.uuid4());directory.mkdir()
    literal=str(directory).replace("'","''")
    command="$ErrorActionPreference='Stop';$acl=New-Object Security.AccessControl.DirectorySecurity;$acl.SetAccessRuleProtection($true,$false);$me=[Security.Principal.WindowsIdentity]::GetCurrent().User;$acl.SetOwner($me);foreach($sid in @($me,([Security.Principal.SecurityIdentifier]::new('S-1-5-18')))){$acl.AddAccessRule([Security.AccessControl.FileSystemAccessRule]::new($sid,'FullControl','ContainerInherit,ObjectInherit','None','Allow'))};Set-Acl -LiteralPath '"+literal+"' -AclObject $acl"
    p=subprocess.run(["powershell.exe","-NoProfile","-NonInteractive","-Command",command],capture_output=True,timeout=15)
    R.require(p.returncode==0,"PRIVATE_ACL_PREPARATION_FAILED")
    R.private_directory(directory)
    value={"attemptId":directory.name,"target":R.SITE,"plan":R.PLAN,"image":R.IMAGE,
           "issuedUtc":R.utc().isoformat(),"expiresUtc":(R.utc()+R.dt.timedelta(minutes=15)).isoformat(),
           "explicitLiveApproval":False,"allowRecovery":False,"code":R.identities()}
    R.save_new(directory/"approval.json",value)
    return {"result":"PRIVATE_ATTEMPT_PREPARED_NOT_AUTHORIZED","attemptId":directory.name,
            "PUBLIC_RUNTIME_VERIFIED":False,"liveAttemptReady":False}


def main(argv=None):
    parser=argparse.ArgumentParser(description=__doc__)
    modes=parser.add_mutually_exclusive_group()
    modes.add_argument("--execute",action="store_true")
    modes.add_argument("--prepare-private-attempt",action="store_true")
    parser.add_argument("--snapshot-worker",choices=("BTC/USD","ETH/USD"),help=argparse.SUPPRESS)
    args=parser.parse_args(argv)
    try:
        if args.prepare_private_attempt:
            print(json.dumps(prepare_private_attempt()));return 0
        if args.snapshot_worker:
            # Private worker route requires the armed immutable baseline, not a generic flag.
            directory=Path(os.environ["AIQ_WP21_ATTEMPT_DIRECTORY"])
            R.private_directory(directory);value=R.envelope(directory)
            R.authorization(value["approval"],directory)
            R.require(R.load(directory/"health-alive.json")=={"attemptId":value["approval"]["attemptId"],"collectorHash":value["approval"]["code"]["collector"]},"HEALTH_NOT_ESTABLISHED")
            R.save_new(directory/("snapshot-"+args.snapshot_worker[:3]+".json"),{"attemptId":value["approval"]["attemptId"]})
            R.require(args.execute is False,"INVALID_MODE")
            snapshot_worker(args.snapshot_worker);return 0
        if not args.execute:
            value=offline_report();WP19.emit(value);return 0 if value["privateControllerQualified"] else 1
        directory=Path(os.environ["AIQ_WP21_ATTEMPT_DIRECTORY"])
        R.private_directory(directory)
        approval=R.load(directory/"approval.json")
        value=run(directory,approval,R.Azure())
        print(json.dumps(value));return 0 if value["result"]=="RESEARCH_HTTPS_CHECKS_COMPLETE" and value["rollbackVerified"] else 1
    except BaseException as error:
        reason=str(error) if isinstance(error,R.Blocked) else "LOCAL_OR_OPERATION_FAILURE_REVIEW_RECOVERY"
        print(json.dumps({"result":"CONTROLLER_BLOCKED_REVIEW_PRIVATE_RECOVERY","reason":reason,"PUBLIC_RUNTIME_VERIFIED":False}));return 1


if __name__=="__main__":raise SystemExit(main())
