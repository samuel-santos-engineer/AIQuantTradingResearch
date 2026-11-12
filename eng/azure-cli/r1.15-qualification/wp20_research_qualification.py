"""Offline WP20 handoff and reusable single-attempt guard, not an Azure adapter.

The CLI never invokes qualify(). A separately reviewed private controller must
supply the real operations and independent recovery before live authority is used.
"""
import argparse
import datetime
import importlib.util
import json
from pathlib import Path

HERE = Path(__file__).resolve().parent
SPEC = importlib.util.spec_from_file_location("wp19", HERE / "wp19_operator_handoff.py")
WP19 = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(WP19)
WP19.SCRIPTS["wp20"] = HERE.parents[1] / "docker/release-1.15/Test-Wp20ResearchQualification.ps1"
TARGET = "aiqr115researchstagingwus27f5eabb5"
DIGEST = WP19.DIGEST


class QualificationBlocked(Exception):
    pass


def qualify(ops, record, approval):
    """Testable sequencing for a future qualified private adapter, never a CLI mode.

    ops must own exact-resource/plan/image checks, restricted baseline capture,
    current comparisons, actual collector invocation, strict DTO validation,
    private credential delivery and independently runnable recovery. No Azure
    implementation or assertion of those properties is supplied by this library.
    """
    if approval.get("explicitLiveApproval") is not True or approval.get("target") != TARGET or approval.get("digest") != DIGEST:
        raise QualificationBlocked("EXACT_LIVE_AUTHORITY_REQUIRED")
    if record.exists():
        raise QualificationBlocked("ATTEMPT_ALREADY_EXISTS")
    if ops.preflight() != "LOCAL_PREFLIGHT_PASS":
        raise QualificationBlocked("LOCAL_PREFLIGHT_BLOCKED")
    ops.capture_baseline()  # Private image/startup/settings/logs/HTTPS/slots/state.
    if ops.compare_baseline() is not True:
        raise QualificationBlocked("BASELINE_UNVERIFIED")
    # Exclusive create: no deletion, overwrite, replacement or reuse of WP17 state.
    with record.open("x", encoding="utf-8", newline="\n") as stream:
        json.dump({"workPackage": "WP20", "target": TARGET, "digest": DIGEST,
                   "armedUtc": datetime.datetime.now(datetime.timezone.utc).isoformat()}, stream)
    result = {"result": "INCOMPLETE", "RESEARCH_PROCESS_ALIVE": "UNVERIFIED",
              "AUTHENTIC_DATA_NOT_IMPORTED": True, "PUBLIC_RUNTIME_VERIFIED": False,
              "unauthenticatedSnapshot": "NOT_RUN", "authenticatedSnapshots": "NOT_RUN"}
    try:
        ops.deploy()  # Must include Start; any partial failure still requires recovery.
        health = ops.health(max_attempts=3, attempt_seconds=60, total_seconds=180, parent_seconds=190)
        if not (health.get("result") == "PROCESS_ALIVE" and health.get("processAlive") is True and
                type(health.get("elapsedSeconds")) in (int, float) and 0 <= health["elapsedSeconds"] <= 180):
            raise QualificationBlocked("HEALTH_NOT_ESTABLISHED")
        result["RESEARCH_PROCESS_ALIVE"] = True
        if approval.get("unauthenticatedSnapshot") is True:
            if ops.unauthenticated_snapshot() != "VALIDATED_401":
                raise QualificationBlocked("UNAUTHENTICATED_CHECK_FAILED")
            result["unauthenticatedSnapshot"] = "VALIDATED_401"
        if approval.get("authenticatedSnapshots") is True:
            if ops.private_credentials_available() is not True:
                result["authenticatedSnapshots"] = "SKIPPED_PRIVATE_CHANNEL_UNAVAILABLE"
            else:
                result["authenticatedSnapshots"] = {}
                for instrument in ("BTC/USD", "ETH/USD"):
                    outcome = ops.authenticated_snapshot(instrument)
                    if outcome not in ("VALIDATED_RESPONSE", "VALIDATED_503_DATASET_NOT_READY"):
                        raise QualificationBlocked("AUTHENTICATED_CHECK_FAILED")
                    result["authenticatedSnapshots"][instrument] = outcome
        result["result"] = "RESEARCH_ONLY_CHECKS_COMPLETE"
    except Exception:
        result["result"] = "QUALIFICATION_FAILED"  # Never serialize exception/body/settings.
    finally:
        restored = False
        try:
            ops.restore_baseline()
            restored = True
        except Exception:
            pass
        try:
            result["independentBaselineMatch"] = ops.compare_baseline() is True
        except Exception:
            result["independentBaselineMatch"] = False
        result["rollbackVerified"] = restored and result["independentBaselineMatch"]
        if not result["rollbackVerified"]:
            result["result"] = "CRITICAL_RECOVERY_UNVERIFIED"
    return result


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--report-path", type=Path)
    args = parser.parse_args(argv)
    report = WP19.offline_report()
    report["result"] = "LOCAL_PREFLIGHT_PASS" if report["result"] == "OFFLINE_PREFLIGHT_PASS" else "LOCAL_PREFLIGHT_BLOCKED"
    report.update({"workPackage": "WP20", "privateControllerQualified": False,
                   "liveAttemptReady": False, "controllerRequirement": "FRESH_PRIVATE_CONTROLLER_AND_INDEPENDENT_RECOVERY_REVIEW_REQUIRED"})
    try:
        WP19.emit(report, args.report_path)
    except WP19.HandoffBlocked:
        WP19.emit({"result": "LOCAL_PREFLIGHT_BLOCKED", "reason": "REPORT_NOT_WRITTEN", "PUBLIC_RUNTIME_VERIFIED": False})
        return 1
    return 0 if report["result"] == "LOCAL_PREFLIGHT_PASS" else 1


if __name__ == "__main__":
    raise SystemExit(main())
