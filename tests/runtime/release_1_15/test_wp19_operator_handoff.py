"""Offline human-handoff tests; no policy override, Azure or live controller."""
import argparse
import contextlib
import importlib.util
import io
import json
import os
from pathlib import Path
import subprocess
import tempfile
import unittest
from unittest import mock

ROOT = Path(__file__).resolve().parents[3]
SOURCE = ROOT / "eng/azure-cli/r1.15-qualification/wp19_operator_handoff.py"
SPEC = importlib.util.spec_from_file_location("wp19", SOURCE)
M = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(M)


def args(**changes):
    values = dict(approve_live_attempt=False, approval_reference=None, approved_image_digest=None, controller=None)
    values.update(changes)
    return argparse.Namespace(**values)


def good_metadata():
    return {"powerShellVersion": "5.1.26100.9444", "effectivePolicy": "RemoteSigned", "policyScopes": dict.fromkeys(M.WP18.SCOPES, "Undefined"), "pythonAvailable": True,
            "scripts": {"readiness": {"parseErrors": 0}}}


class OperatorHandoffTests(unittest.TestCase):
    def test_default_is_offline_only(self):
        self.assertFalse(M.validate_live(args()))
        with mock.patch.object(M, "offline_report", return_value={"result": "OFFLINE_PREFLIGHT_PASS"}), mock.patch.object(M, "execute_live") as live, contextlib.redirect_stdout(io.StringIO()):
            self.assertEqual(M.main([]), 0)
        live.assert_not_called()

    def test_partial_approval_is_rejected(self):
        for change in ({"controller": SOURCE}, {"approve_live_attempt": True}, {"approval_reference": "WP20-OWNER-APPROVED"}):
            with self.assertRaises(M.HandoffBlocked): M.validate_live(args(**change))

    def test_artifact_must_match_specific_reviewed_image(self):
        with self.assertRaises(M.HandoffBlocked): M.validate_live(args(approve_live_attempt=True, approval_reference="WP20-OWNER-APPROVED", approved_image_digest="sha256:" + "0"*64, controller=SOURCE))

    def test_missing_relative_and_wrong_type_controller_rejected(self):
        for controller in (None, Path("relative.py"), SOURCE.with_suffix(".missing"), SOURCE.parent / "missing.py"):
            with self.assertRaises(M.HandoffBlocked): M.validate_live(args(approve_live_attempt=True, approval_reference="WP20-OWNER-APPROVED", approved_image_digest=M.DIGEST, controller=controller))

    def test_explicit_complete_arguments_validate_without_executing(self):
        self.assertTrue(M.validate_live(args(approve_live_attempt=True, approval_reference="WP20-OWNER-APPROVED", approved_image_digest=M.DIGEST, controller=SOURCE)))

    def test_policy_failure_returns_redacted_safe_refusal(self):
        with mock.patch.object(M, "metadata", return_value=good_metadata()), mock.patch.object(M.WP18, "preflight", side_effect=M.WP18.PreflightBlocked("LOCAL_SCRIPT_LAUNCH_BLOCKED")):
            value = M.offline_report()
        self.assertEqual(value["result"], "OFFLINE_PREFLIGHT_BLOCKED")
        self.assertFalse(value["liveControllerInvoked"]);self.assertFalse(value["PUBLIC_RUNTIME_VERIFIED"])
        self.assertEqual(value["reason"], "LOCAL_SCRIPT_LAUNCH_BLOCKED")

    def test_all_blocked_offline_results_prevent_live_controller(self):
        for reason in ("LOCAL_SCRIPT_LAUNCH_BLOCKED", "INTERPRETER_UNAVAILABLE", "SCRIPT_PARSE_FAILED"):
            with mock.patch.object(M, "validate_live", return_value=True), mock.patch.object(M, "offline_report", return_value={"result": "OFFLINE_PREFLIGHT_BLOCKED", "reason": reason}), mock.patch.object(M, "execute_live") as live, contextlib.redirect_stdout(io.StringIO()):
                self.assertEqual(M.main([]), 1)
            live.assert_not_called()

    def test_parse_failure_prevents_wp18_gate(self):
        value = good_metadata();value["scripts"]["readiness"]["parseErrors"] = 1
        with mock.patch.object(M, "metadata", return_value=value), mock.patch.object(M.WP18, "preflight") as gate:
            self.assertEqual(M.offline_report()["reason"], "SCRIPT_PARSE_FAILED")
        gate.assert_not_called()

    def test_success_is_only_offline_acceptance(self):
        with mock.patch.object(M, "metadata", return_value=good_metadata()), mock.patch.object(M.WP18, "preflight", return_value={"result": "LOCAL_PREFLIGHT_PASS"}):
            result = M.offline_report()
        self.assertEqual(result["result"], "OFFLINE_PREFLIGHT_PASS")
        self.assertEqual(result["RESEARCH_PROCESS_ALIVE"], "UNVERIFIED")
        self.assertTrue(result["AUTHENTIC_DATA_NOT_IMPORTED"]);self.assertFalse(result["PUBLIC_RUNTIME_VERIFIED"])

    def test_metadata_errors_do_not_expose_private_values(self):
        for error in (OSError("PRIVATE_CANARY"), M.WP18.PreflightBlocked("PRIVATE_CANARY")):
            with mock.patch.object(M, "metadata", side_effect=error): value = M.offline_report()
            self.assertNotIn("PRIVATE_CANARY", json.dumps(value))

    def test_missing_powershell_is_fixed_failure(self):
        with mock.patch.object(M.shutil, "which", return_value=None), self.assertRaises(M.HandoffBlocked): M.metadata()

    def test_metadata_allowlists_and_discards_raw_fields(self):
        policy = {"version": "5.1.26100.9711", "effective": "Restricted", "scopes": dict.fromkeys(M.WP18.SCOPES, "Undefined")}
        entry = {"signature": "NotSigned", "internetMark": False, "parseErrors": 0, "private": "PRIVATE_CANARY"}
        with mock.patch.object(M.os, "name", "nt"), mock.patch.object(M.shutil, "which", return_value="fixed"), mock.patch.object(M.WP18, "call", side_effect=[policy, entry, entry, entry]):
            value = M.metadata()
        self.assertNotIn("PRIVATE_CANARY", json.dumps(value));self.assertEqual(len(value["scripts"]), 3)

    def test_live_route_uses_wp18_and_suppresses_raw_controller_output(self):
        with mock.patch.object(M.subprocess, "run", return_value=subprocess.CompletedProcess([], 0)) as run:
            self.assertTrue(M.execute_live(args(controller=SOURCE)))
        command = run.call_args.args[0]
        self.assertEqual(command[-2:], ["--execute-controller", str(SOURCE)])
        self.assertEqual(Path(command[2]).name, "wp18_research_launcher.py")
        self.assertEqual(run.call_args.kwargs["stdout"], subprocess.DEVNULL)
        self.assertEqual(run.call_args.kwargs["stderr"], subprocess.DEVNULL)

    def test_existing_report_refuses_before_any_live_call(self):
        with mock.patch.object(M, "validate_live", return_value=True), mock.patch.object(M, "offline_report") as offline, mock.patch.object(M, "execute_live") as live, contextlib.redirect_stdout(io.StringIO()):
            self.assertEqual(M.main(["--report-path", str(SOURCE)]), 1)
        live.assert_not_called();offline.assert_not_called()

    def test_missing_python_from_wp18_fails_offline(self):
        value = good_metadata();value["pythonAvailable"] = False
        with mock.patch.object(M, "metadata", return_value=value), mock.patch.object(M.WP18, "preflight", side_effect=M.WP18.PreflightBlocked("INTERPRETER_UNAVAILABLE")):
            result = M.offline_report()
        self.assertEqual(result["reason"], "INTERPRETER_UNAVAILABLE")
        self.assertFalse(result["liveControllerInvoked"])

    def test_report_is_bounded_utf8_lf_and_never_overwrites(self):
        with tempfile.TemporaryDirectory() as folder:
            path = Path(folder) / "report.json"
            with contextlib.redirect_stdout(io.StringIO()): M.emit({"result": "OFFLINE_PREFLIGHT_BLOCKED"}, path)
            before = path.read_bytes();self.assertNotIn(b"\r", before);self.assertFalse(before.startswith(b"\xef\xbb\xbf"))
            with self.assertRaises(M.HandoffBlocked): M.emit({"new": True}, path)
            self.assertEqual(path.read_bytes(), before)
            with self.assertRaises(M.HandoffBlocked): M.emit({"oversized": "x" * 8192})

    def test_wrappers_have_separate_default_and_live_argument_surfaces(self):
        offline = M.SCRIPTS["offline"].read_text(); live = M.SCRIPTS["live"].read_text()
        self.assertNotIn("ApproveLiveAttempt", offline);self.assertNotIn("--controller", offline)
        for required in ("-not $ApproveLiveAttempt", "$ApprovalReference", "$ApprovedImageDigest", "$ControllerPath"):
            self.assertIn(required, live)
        for source in (offline, live, SOURCE.read_text()):
            for forbidden in ("Set-ExecutionPolicy", "Invoke-Expression", "-ExecutionPolicy", "az webapp", "http.client", "urllib"):
                self.assertNotIn(forbidden, source)

    def test_multiple_python_matches_select_one_path_without_override(self):
        command = "$all=@(Get-Command python -CommandType Application -ErrorAction Stop);$selected=Get-Command python -CommandType Application -ErrorAction Stop | Select-Object -First 1;@{selectedCount=@($selected).Count;sourceIsString=($selected.Source -is [string]);firstPath=($selected.Source -eq $all[0].Source)}|ConvertTo-Json"
        p = subprocess.run(["powershell.exe", "-NoProfile", "-NonInteractive", "-Command", command], capture_output=True, text=True, timeout=15)
        self.assertEqual(p.returncode, 0)
        value = json.loads(p.stdout)
        self.assertEqual(value, {"selectedCount": 1, "sourceIsString": True, "firstPath": True})
        for label in ("offline", "live"):
            self.assertIn("Get-Command python -CommandType Application -ErrorAction Stop | Select-Object -First 1", M.SCRIPTS[label].read_text())

    def test_real_ps51_parse_metadata_and_blocked_file_route(self):
        self.assertEqual(os.name, "nt", "Windows operator qualification required")
        value = M.metadata()  # Read-only inspection, not execution of script contents.
        self.assertEqual(value["effectivePolicy"], M.WP18.effective_policy(value["policyScopes"]))
        self.assertTrue(value["powerShellVersion"].startswith("5.1."))
        self.assertTrue(all(v["parseErrors"] == 0 for v in value["scripts"].values()))
        for label in ("offline", "live"):
            p = subprocess.run(["powershell.exe", "-NoProfile", "-NonInteractive", "-File", str(M.SCRIPTS[label])], capture_output=True, timeout=15)
            if value["effectivePolicy"] == "Restricted":
                self.assertNotEqual(p.returncode, 0)
                self.assertTrue(b"running scripts is disabled" in p.stderr and b"UnauthorizedAccess" in p.stderr, "Expected execution-policy refusal; raw stderr withheld")
            elif p.stderr:
                self.assertNotEqual(p.returncode, 0)  # E.g. AllSigned rejects unsigned entry.
            else:
                result = json.loads(p.stdout.decode("utf-8-sig"))
                self.assertIn(result["result"], ("OFFLINE_PREFLIGHT_PASS", "OFFLINE_PREFLIGHT_BLOCKED", "HANDOFF_BLOCKED"))
                self.assertFalse(result["PUBLIC_RUNTIME_VERIFIED"])
                if label == "live": self.assertEqual(result["result"], "HANDOFF_BLOCKED")


if __name__ == "__main__":
    unittest.main()
