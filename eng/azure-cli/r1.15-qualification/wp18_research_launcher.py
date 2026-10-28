"""Windows-only operator gate; no policy changes and no Azure composition.

Only --execute-controller can invoke a separately approved private controller.
That controller must own exact-target/baseline/rollback and single-attempt checks.
"""
import argparse
import json
import os
from pathlib import Path
import shutil
import subprocess
import sys

SCRIPT = Path(__file__).resolve().parents[2] / "docker/release-1.15/Test-Wp07StagingChartReadiness.ps1"
SCOPES = ("MachinePolicy", "UserPolicy", "Process", "CurrentUser", "LocalMachine")
POLICIES = {"Undefined", "Restricted", "AllSigned", "RemoteSigned", "Unrestricted", "Bypass"}
GUIDANCE = "Ask the owner/administrator to review effective Group Policy and script signing/provenance; use an approved signed script where required. No policy is changed automatically. Rerun the gate in the intended operator account."
QUERY = "$ErrorActionPreference='Stop'; $s=[ordered]@{}; Get-ExecutionPolicy -List | ForEach-Object {$s[$_.Scope.ToString()]=$_.ExecutionPolicy.ToString()}; @{version=$PSVersionTable.PSVersion.ToString();effective=(Get-ExecutionPolicy).ToString();scopes=$s}|ConvertTo-Json -Depth 3"


class PreflightBlocked(Exception):
    pass


def effective_policy(scopes):
    if set(scopes) != set(SCOPES) or any(v not in POLICIES for v in scopes.values()):
        raise PreflightBlocked("POLICY_UNAVAILABLE")
    return next((scopes[k] for k in SCOPES if scopes[k] != "Undefined"), "Restricted")


def call(args, timeout):
    try:
        p = subprocess.run(args, capture_output=True, timeout=timeout,
                           creationflags=getattr(subprocess, "CREATE_NO_WINDOW", 0))
        if p.returncode or len(p.stdout) > 4096:
            raise PreflightBlocked("LOCAL_SCRIPT_LAUNCH_BLOCKED")
        return json.loads(p.stdout.decode("utf-8-sig"))
    except (OSError, subprocess.TimeoutExpired, ValueError):
        raise PreflightBlocked("LOCAL_EXECUTION_UNAVAILABLE") from None


def preflight():
    if os.name != "nt":
        raise PreflightBlocked("WINDOWS_POWERSHELL_51_REQUIRED")
    ps = shutil.which("powershell.exe")
    if not ps or not shutil.which("python"):
        raise PreflightBlocked("INTERPRETER_UNAVAILABLE")
    policy = call([ps, "-NoProfile", "-NonInteractive", "-Command", QUERY], 15)
    try:
        if not policy["version"].startswith("5.1."):
            raise PreflightBlocked("WINDOWS_POWERSHELL_51_REQUIRED")
        effective = effective_policy(policy["scopes"])
        if effective != policy["effective"]:
            raise PreflightBlocked("POLICY_INCONSISTENT")
    except (KeyError, TypeError, AttributeError):
        raise PreflightBlocked("POLICY_UNAVAILABLE") from None
    # Real -File route, unchanged policy. Offline mode also launches the real Python
    # helper without --execute. No fabricated approval and no script-text evaluation.
    dry = call([ps, "-NoProfile", "-NonInteractive", "-File", str(SCRIPT), "-LocalPreflight"], 30)
    if effective not in {"RemoteSigned", "AllSigned", "Unrestricted"}:
        raise PreflightBlocked("POLICY_NOT_APPROVED")
    if dry != {"result": "LOCAL_PREFLIGHT_PASS", "PUBLIC_RUNTIME_VERIFIED": False}:
        raise PreflightBlocked("INVALID_DRY_RUN")
    return {"result": "LOCAL_PREFLIGHT_PASS", "effectivePolicy": effective,
            "scopes": policy["scopes"], "PUBLIC_RUNTIME_VERIFIED": False}


def guarded_deployment(deploy):
    result = preflight()  # Unconditional; no cached success or skip flag.
    deploy()
    return result


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--execute-controller", type=Path,
                        help="Requires separate live authority; exact private Python controller receives --execute.")
    args = parser.parse_args(argv)
    try:
        if args.execute_controller is None:
            result = preflight()
        else:
            controller = args.execute_controller
            if not controller.is_absolute() or controller.suffix != ".py" or not controller.is_file():
                raise PreflightBlocked("CONTROLLER_UNAVAILABLE")
            def deploy():
                # Inherit private controller output; this gate never reads service keys.
                completed = subprocess.run([sys.executable, "-B", str(controller), "--execute"], check=False)
                if completed.returncode:
                    raise PreflightBlocked("CONTROLLER_FAILED_REVIEW_RECOVERY_EVIDENCE")
            result = guarded_deployment(deploy)
        print(json.dumps(result))
        return 0
    except PreflightBlocked as error:
        print(json.dumps({"result": "PREFLIGHT_BLOCKED", "reason": str(error),
                          "guidance": GUIDANCE, "PUBLIC_RUNTIME_VERIFIED": False}))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
