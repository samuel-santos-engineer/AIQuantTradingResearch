"""Offline Linux-first deployment planning primitives. No live adapter or network execution."""
import argparse
from contextlib import contextmanager
import hashlib
import json
import os
from pathlib import Path
import re
import signal
import stat
import subprocess
import sys
import tempfile
import threading
import time

SUB = "7f5eabb5-80f5-4369-88be-b14a6de763f1"
GROUP = "rg-aiq-r112-wp05-wus2-7f5eabb5"
ROOT = f"/subscriptions/{SUB}/resourceGroups/{GROUP}/providers/Microsoft.Web/"
PLAN = ROOT + "serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5"
API = ROOT + "sites/aiqr115researchstagingwus27f5eabb5"
UI = ROOT + "sites/aiqr112wp05wus27f5eabb5"
REPOSITORY = "ghcr.io/samuel-santos-engineer/aiquanttradingresearch"
ACCEPTED_API = "sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3"
DIAGNOSTIC_API = "sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c"
FIRST_URL = "https://management.azure.com" + PLAN + "/usages?api-version=2023-12-01"
LIMIT = 1048576


class Blocked(Exception):
    pass


def require(ok, code):
    if not ok:
        raise Blocked(code)


def validate_operation(method, resource):
    # This validates a proposal only. It never executes the returned action.
    require((method, resource) in {("GET", PLAN + "/usages"), ("GET", PLAN), ("GET", API), ("GET", UI),
                                  ("GET", API + "/config/web"), ("GET", UI + "/config/web"),
                                  ("GET", API + "/config/logs"), ("GET", UI + "/config/logs"),
                                  ("PATCH", API + "/config/web"), ("PATCH", UI + "/config/web")}, "OPERATION_NOT_ALLOWED")
    return {"method": method, "resource": resource, "executed": False}


def validate_snapshot(value, baseline_image):
    require(value.get("resourceId", "").lower() == API.lower(), "RESOURCE_IDENTITY")
    require(value.get("planId", "").lower() == PLAN.lower(), "PLAN_IDENTITY")
    require(value.get("linux") is True and value.get("sku") == "F1", "HOSTING_UNKNOWN")
    require(value.get("state") == "Running" and value.get("usage") == "Normal", "APP_STATE_UNKNOWN")
    require(type(value.get("stopCurrent")) is int and value["stopCurrent"] >= 0 and
            type(value.get("stopLimit")) is int and value["stopLimit"] > 0, "QUOTA_SCHEMA")
    require(value["stopCurrent"] < value["stopLimit"], "QUOTA_PRESSURE")
    require(value.get("configuredImage") == baseline_image and value.get("startup") == "", "DRIFT")
    return {"quotaBelowLimit": True, "configuredOnly": True, "runtimeVerified": False}


def verify_manifest(raw, digest):
    require(re.fullmatch(r"sha256:[a-f0-9]{64}", digest) is not None, "DIGEST_SCHEMA")
    require("sha256:" + hashlib.sha256(raw).hexdigest() == digest, "DIGEST_MISMATCH")
    try:
        document = json.loads(raw)
    except (ValueError, UnicodeError):
        raise Blocked("MANIFEST_SCHEMA") from None
    return document


def verify_platform(index_raw, index_digest, manifest_raw):
    index = verify_manifest(index_raw, index_digest)
    children = [x for x in index.get("manifests", []) if x.get("platform", {}).get("os") == "linux" and
                x.get("platform", {}).get("architecture") == "amd64"]
    require(len(children) == 1, "PLATFORM_AMBIGUOUS")
    manifest = verify_manifest(manifest_raw, children[0]["digest"])
    require(manifest.get("schemaVersion") == 2 and isinstance(manifest.get("layers"), list), "MANIFEST_SCHEMA")
    return {"platform": "linux/amd64", "runtimeDigest": children[0]["digest"], "configVerified": False}


def recovery_decision(write_attempted, quota_safe, baseline_complete, drift):
    if not write_attempted:
        return "NO_MUTATION"
    if quota_safe is not True or baseline_complete is not True or drift is not False:
        return "MANUAL_RECOVERY_REQUIRED"
    return "PROPOSE_ONE_BASELINE_IMAGE_ROLLBACK"


@contextmanager
def private_workspace(parent=None):
    """Linux-only synthetic/private-file primitive; directory verified before any file write."""
    require(sys.platform == "linux" and hasattr(os, "geteuid"), "LINUX_PERMISSIONS_NOT_QUALIFIED")
    folder = Path(tempfile.mkdtemp(prefix="aiq-linux-baseline-", dir=parent))
    descriptor = None
    try:
        info = folder.lstat()
        require(stat.S_ISDIR(info.st_mode) and info.st_uid == os.geteuid() and stat.S_IMODE(info.st_mode) == 0o700,
                "PRIVATE_DIRECTORY_UNSAFE")
        descriptor = os.open(folder, os.O_RDONLY | os.O_DIRECTORY | os.O_NOFOLLOW)
        yield folder, descriptor
    finally:
        if descriptor is not None:
            os.close(descriptor)
        # Caller controls retention. Never recursively delete a private baseline.
        if folder.exists() and not any(folder.iterdir()):
            folder.rmdir()


def write_private(descriptor, name, value):
    require(name in {"synthetic.json", "baseline.json", "recovery.json", "ledger.json"}, "PRIVATE_NAME_REJECTED")
    info = os.fstat(descriptor)
    require(info.st_uid == os.geteuid() and stat.S_IMODE(info.st_mode) == 0o700, "PRIVATE_DIRECTORY_UNSAFE")
    fd = os.open(name, os.O_WRONLY | os.O_CREAT | os.O_EXCL | os.O_NOFOLLOW, 0o600, dir_fd=descriptor)
    try:
        info = os.fstat(fd)
        require(info.st_uid == os.geteuid() and stat.S_IMODE(info.st_mode) == 0o600 and info.st_nlink == 1, "PRIVATE_FILE_UNSAFE")
        data = (json.dumps(value) + "\n").encode("utf-8")
        with os.fdopen(fd, "wb", closefd=False) as file:
            file.write(data); file.flush(); os.fsync(file.fileno())
    finally:
        os.close(fd)


def bounded_local(command, seconds=3, limit=LIMIT):
    """Future Linux local subprocess primitive, not a live CLI/network adapter."""
    require(sys.platform == "linux", "LINUX_PROCESS_CLEANUP_NOT_QUALIFIED")
    require(0 < seconds <= 15 and 0 < limit <= LIMIT, "CAP_INVALID")
    proc = subprocess.Popen(command, stdout=subprocess.PIPE, stderr=subprocess.PIPE, start_new_session=True)
    buffers = [bytearray(), bytearray()]; lock = threading.Lock(); overflow = threading.Event()
    def drain(pipe, buffer):
        while True:
            chunk = pipe.read(4096)
            if not chunk: return
            with lock:
                if sum(map(len, buffers)) + len(chunk) > limit:
                    overflow.set(); return
                buffer.extend(chunk)
    threads = [threading.Thread(target=drain, args=(pipe, buf), daemon=True)
               for pipe, buf in zip((proc.stdout, proc.stderr), buffers)]
    for thread in threads: thread.start()
    deadline = time.monotonic() + seconds
    failed = None
    try:
        while proc.poll() is None and not overflow.is_set() and time.monotonic() < deadline:
            time.sleep(0.01)
        if overflow.is_set(): failed = "OUTPUT_LIMIT"
        elif proc.poll() is None: failed = "TIMEOUT"
    finally:
        try: os.killpg(proc.pid, signal.SIGKILL)
        except ProcessLookupError: pass
        proc.wait(timeout=3)
        for thread in threads: thread.join(timeout=1)
        require(not any(thread.is_alive() for thread in threads), "PIPE_CLEANUP_UNKNOWN")
        proc.stdout.close(); proc.stderr.close()
        try: os.killpg(proc.pid, 0)
        except ProcessLookupError: pass
        else: raise Blocked("PROCESS_GROUP_CLEANUP_UNKNOWN")  # Includes unreaped descendant zombies; never claim success.
    if failed: raise Blocked(failed)
    require(proc.returncode == 0, "LOCAL_COMMAND_FAILED")
    return bytes(buffers[0])


def plan():
    return {"result": "OFFLINE_PLAN_ONLY", "liveAdapterImplemented": False,
            "topology": "TWO_EXISTING_LINUX_WEB_APPS", "artifactSelection": "OWNER_DECISION_REQUIRED",
            "firstProposedOperation": validate_operation("GET", PLAN + "/usages"),
            "firstProposedUrl": FIRST_URL, "timeoutSeconds": 15, "responseCapBytes": LIMIT,
            "currentQuota": "UNKNOWN", "currentAzureConfiguration": "UNKNOWN", "uiRegistryDigest": "UNKNOWN",
            "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.parse_args(argv)  # No apply flag, credentials, target overrides or live execution route.
    print(json.dumps(plan(), sort_keys=True))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
