"""Sanitized operator report; only the separately approved live mode calls WP18.

Never use this helper to evade a blocked PowerShell entry point. Run the documented
PowerShell script in the intended authorized operator session.
"""
import argparse
import datetime
import hashlib
import importlib.util
import json
import os
from pathlib import Path
import re
import shutil
import subprocess
import sys

HERE = Path(__file__).resolve().parent
SPEC = importlib.util.spec_from_file_location("wp18", HERE / "wp18_research_launcher.py")
WP18 = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(WP18)
SCRIPTS = {"offline": HERE.parents[1] / "docker/release-1.15/Test-Wp19OperatorPreflight.ps1",
           "live": HERE.parents[1] / "docker/release-1.15/Invoke-Wp19ResearchHandoff.ps1",
           "readiness": WP18.SCRIPT}
DIGEST = "sha256:d3221dbe0a4ef6dfba37e0c61c06bb3bc9d9ee2b6f776d7738c35aece32be26f"
SIGNATURES = {"Valid", "UnknownError", "NotSigned", "HashMismatch", "NotTrusted", "NotSupportedFileFormat", "Incompatible"}
REASONS = {"POLICY_UNAVAILABLE", "POLICY_INCONSISTENT", "LOCAL_SCRIPT_LAUNCH_BLOCKED", "LOCAL_EXECUTION_UNAVAILABLE", "WINDOWS_POWERSHELL_51_REQUIRED", "INTERPRETER_UNAVAILABLE", "POLICY_NOT_APPROVED", "INVALID_DRY_RUN"}


class HandoffBlocked(Exception):
    pass


def metadata():
    ps = shutil.which("powershell.exe")
    if os.name != "nt" or not ps:
        raise HandoffBlocked("WINDOWS_POWERSHELL_51_REQUIRED")
    policy = WP18.call([ps, "-NoProfile", "-NonInteractive", "-Command", WP18.QUERY], 15)
    effective = WP18.effective_policy(policy["scopes"])
    if policy["effective"] != effective or not re.fullmatch(r"5\.1\.[0-9.]+", policy["version"]):
        raise HandoffBlocked("POLICY_OR_VERSION_UNAVAILABLE")
    provenance = {}
    for label, path in SCRIPTS.items():
        literal = str(path).replace("'", "''")
        command = ("$ErrorActionPreference='Stop';$p='" + literal + "';$t=$null;$e=$null;"
                   "[Management.Automation.Language.Parser]::ParseFile($p,[ref]$t,[ref]$e)|Out-Null;"
                   "@{signature=(Get-AuthenticodeSignature -LiteralPath $p).Status.ToString();"
                   "internetMark=(Test-Path -LiteralPath ($p+':Zone.Identifier'));parseErrors=$e.Count}|ConvertTo-Json")
        value = WP18.call([ps, "-NoProfile", "-NonInteractive", "-Command", command], 15)
        if value.get("signature") not in SIGNATURES or type(value.get("internetMark")) is not bool or type(value.get("parseErrors")) is not int:
            raise HandoffBlocked("PROVENANCE_UNAVAILABLE")
        provenance[label] = {"signature": value["signature"], "internetMark": value["internetMark"],
                             "parseErrors": value["parseErrors"], "sha256": hashlib.sha256(path.read_bytes()).hexdigest()}
    return {"powerShellVersion": policy["version"], "effectivePolicy": effective,
            "policyScopes": policy["scopes"], "pythonAvailable": bool(shutil.which("python")), "scripts": provenance}


def offline_report():
    result = {"result": "OFFLINE_PREFLIGHT_BLOCKED", "observedUtc": datetime.datetime.now(datetime.timezone.utc).isoformat(),
              "RESEARCH_PROCESS_ALIVE": "UNVERIFIED", "AUTHENTIC_DATA_NOT_IMPORTED": True,
              "PUBLIC_RUNTIME_VERIFIED": False, "liveControllerInvoked": False}
    try:
        result.update(metadata())
        if any(v["parseErrors"] for v in result["scripts"].values()):
            raise HandoffBlocked("SCRIPT_PARSE_FAILED")
        gate = WP18.preflight()  # Real default offline gate, no live flags.
        if gate.get("result") != "LOCAL_PREFLIGHT_PASS":
            raise HandoffBlocked("INVALID_DRY_RUN")
        result["result"] = "OFFLINE_PREFLIGHT_PASS"
    except (WP18.PreflightBlocked, HandoffBlocked) as error:
        reason = str(error)
        result["reason"] = reason if reason in REASONS | {"SCRIPT_PARSE_FAILED", "PROVENANCE_UNAVAILABLE", "POLICY_OR_VERSION_UNAVAILABLE"} else "LOCAL_PREFLIGHT_UNAVAILABLE"
        result["guidance"] = WP18.GUIDANCE
    except (OSError, KeyError, TypeError, ValueError):
        result["reason"] = "LOCAL_PREFLIGHT_UNAVAILABLE"
        result["guidance"] = WP18.GUIDANCE
    return result


def validate_live(args):
    requested = args.approve_live_attempt or args.controller is not None or args.approval_reference is not None or args.approved_image_digest is not None
    if not requested:
        return False
    if not args.approve_live_attempt or not args.approval_reference or not re.fullmatch(r"[A-Z0-9][A-Z0-9-]{3,63}", args.approval_reference):
        raise HandoffBlocked("FRESH_EXPLICIT_APPROVAL_REQUIRED")
    if args.approved_image_digest != DIGEST:
        raise HandoffBlocked("APPROVED_ARTIFACT_REQUIRED")
    if args.controller is None or not args.controller.is_absolute() or args.controller.suffix != ".py" or not args.controller.is_file():
        raise HandoffBlocked("PRIVATE_CONTROLLER_UNAVAILABLE")
    return True


def execute_live(args):
    # Existing WP18 rechecks its real gate before executing the private controller.
    # Do not echo controller stdout/stderr into the shareable report. The qualified
    # controller owns its private evidence, deadlines and independent recovery.
    p = subprocess.run([sys.executable, "-B", str(HERE / "wp18_research_launcher.py"),
                        "--execute-controller", str(args.controller)], stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
    return p.returncode == 0


def emit(report, path=None):
    text = json.dumps(report, indent=2) + "\n"
    if len(text.encode("utf-8")) > 8192:
        raise HandoffBlocked("REPORT_BOUND_EXCEEDED")
    if path is not None:
        # New operator-chosen file only; never overwrite operational evidence.
        try:
            with path.open("x", encoding="utf-8", newline="\n") as stream:
                stream.write(text)
        except OSError:
            raise HandoffBlocked("REPORT_NOT_WRITTEN") from None
    print(text, end="")


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--report-path", type=Path)
    parser.add_argument("--approve-live-attempt", action="store_true")
    parser.add_argument("--approval-reference")
    parser.add_argument("--approved-image-digest")
    parser.add_argument("--controller", type=Path)
    args = parser.parse_args(argv)
    try:
        live = validate_live(args)
        if args.report_path is not None and (args.report_path.exists() or not args.report_path.parent.is_dir()):
            raise HandoffBlocked("REPORT_NOT_WRITTEN")
        report = offline_report()
        if live and report["result"] == "OFFLINE_PREFLIGHT_PASS":
            report["liveControllerInvoked"] = True
            report["result"] = "CONTROLLER_RETURNED_REVIEW_PRIVATE_EVIDENCE" if execute_live(args) else "CONTROLLER_FAILED_REVIEW_PRIVATE_RECOVERY"
        emit(report, args.report_path)
        return 0 if report["result"] in {"OFFLINE_PREFLIGHT_PASS", "CONTROLLER_RETURNED_REVIEW_PRIVATE_EVIDENCE"} else 1
    except (HandoffBlocked, OSError) as error:
        reason = str(error) if isinstance(error, HandoffBlocked) else "LOCAL_EXECUTION_UNAVAILABLE"
        emit({"result": "HANDOFF_BLOCKED", "reason": reason, "PUBLIC_RUNTIME_VERIFIED": False})
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
