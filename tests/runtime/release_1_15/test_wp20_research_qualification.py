"""Offline flow/contract tests, not qualification of a private Azure adapter."""
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
SOURCE = ROOT / "eng/azure-cli/r1.15-qualification/wp20_research_qualification.py"
SPEC = importlib.util.spec_from_file_location("wp20", SOURCE)
M = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(M)


class GuardTests(unittest.TestCase):
    def setUp(self):
        self.folder = tempfile.TemporaryDirectory();self.addCleanup(self.folder.cleanup)
        self.record = Path(self.folder.name) / "WP20-one-attempt.json"
        self.approval = {"explicitLiveApproval": True, "target": M.TARGET, "digest": M.DIGEST}
        self.ops = mock.Mock()
        self.ops.preflight.return_value = "LOCAL_PREFLIGHT_PASS"
        self.ops.compare_baseline.return_value = True
        self.ops.health.return_value = {"result": "PROCESS_ALIVE", "processAlive": True, "elapsedSeconds": 150}
        self.ops.unauthenticated_snapshot.return_value = "VALIDATED_401"
        self.ops.private_credentials_available.return_value = True
        self.ops.authenticated_snapshot.return_value = "VALIDATED_503_DATASET_NOT_READY"

    def run_attempt(self): return M.qualify(self.ops, self.record, self.approval)

    def test_no_approval_wrong_target_or_artifact_prevents_all_operations(self):
        for change in ({"explicitLiveApproval": False}, {"target": "other"}, {"digest": "other"}):
            with self.assertRaises(M.QualificationBlocked): M.qualify(self.ops, self.record, {**self.approval, **change})
        self.assertEqual(self.ops.mock_calls, []);self.assertFalse(self.record.exists())

    def test_local_preflight_failure_prevents_baseline_and_mutation(self):
        self.ops.preflight.return_value = "LOCAL_PREFLIGHT_BLOCKED"
        with self.assertRaises(M.QualificationBlocked): self.run_attempt()
        self.ops.capture_baseline.assert_not_called();self.ops.deploy.assert_not_called()

    def test_missing_baseline_query_and_comparison_fail_before_arming(self):
        self.ops.capture_baseline.side_effect = RuntimeError("PRIVATE_CANARY")
        with self.assertRaises(RuntimeError): self.run_attempt()
        self.ops.deploy.assert_not_called();self.assertFalse(self.record.exists())
        self.ops.capture_baseline.side_effect = None;self.ops.compare_baseline.return_value = False
        with self.assertRaises(M.QualificationBlocked): self.run_attempt()
        self.assertFalse(self.record.exists());self.ops.deploy.assert_not_called()

    def test_existing_record_is_never_reused_or_overwritten(self):
        self.record.write_text("WP17 consumed", encoding="utf-8")
        with self.assertRaises(M.QualificationBlocked): self.run_attempt()
        self.assertEqual(self.record.read_text(), "WP17 consumed");self.assertEqual(self.ops.mock_calls, [])

    def test_new_record_is_created_exclusively_after_verified_baseline(self):
        def deploy():
            value = json.loads(self.record.read_text());self.assertEqual(value["workPackage"], "WP20")
            self.assertEqual(value["digest"], M.DIGEST)
        self.ops.deploy.side_effect = deploy
        self.run_attempt()
        with self.assertRaises(M.QualificationBlocked): self.run_attempt()

    def test_exact_order_and_restore_even_after_health_success(self):
        result = self.run_attempt()
        self.assertEqual([v[0] for v in self.ops.mock_calls], ["preflight", "capture_baseline", "compare_baseline", "deploy", "health", "restore_baseline", "compare_baseline"])
        self.assertTrue(result["rollbackVerified"]);self.assertTrue(result["RESEARCH_PROCESS_ALIVE"])
        self.assertFalse(result["PUBLIC_RUNTIME_VERIFIED"])

    def test_partial_deploy_failure_requires_restoration_without_probes(self):
        self.ops.deploy.side_effect = RuntimeError("PRIVATE_CANARY")
        result = self.run_attempt()
        self.ops.health.assert_not_called();self.ops.restore_baseline.assert_called_once()
        self.assertTrue(result["rollbackVerified"]);self.assertNotIn("PRIVATE_CANARY", json.dumps(result))

    def test_existing_health_bounds_are_passed_unchanged(self):
        self.run_attempt();self.ops.health.assert_called_once_with(max_attempts=3, attempt_seconds=60, total_seconds=180, parent_seconds=190)

    def test_health_failure_prevents_all_snapshots_and_restores(self):
        self.approval.update(unauthenticatedSnapshot=True, authenticatedSnapshots=True)
        self.ops.health.return_value = {"result": "INCOMPLETE", "processAlive": False, "elapsedSeconds": 180}
        result = self.run_attempt()
        self.ops.unauthenticated_snapshot.assert_not_called();self.ops.authenticated_snapshot.assert_not_called()
        self.assertTrue(result["rollbackVerified"]);self.assertEqual(result["RESEARCH_PROCESS_ALIVE"], "UNVERIFIED")

    def test_late_health_cannot_establish_alive(self):
        self.ops.health.return_value["elapsedSeconds"] = 181
        self.assertEqual(self.run_attempt()["RESEARCH_PROCESS_ALIVE"], "UNVERIFIED")

    def test_snapshot_permissions_and_strict_response_outcomes(self):
        self.approval.update(unauthenticatedSnapshot=True, authenticatedSnapshots=True)
        result = self.run_attempt()
        self.ops.unauthenticated_snapshot.assert_called_once()
        self.assertEqual(self.ops.authenticated_snapshot.call_args_list, [mock.call("BTC/USD"), mock.call("ETH/USD")])
        self.assertEqual(set(result["authenticatedSnapshots"].values()), {"VALIDATED_503_DATASET_NOT_READY"})
        self.assertTrue(result["AUTHENTIC_DATA_NOT_IMPORTED"]);self.assertFalse(result["PUBLIC_RUNTIME_VERIFIED"])

    def test_missing_private_channel_skips_authenticated_requests(self):
        self.approval["authenticatedSnapshots"] = True;self.ops.private_credentials_available.return_value = False
        result = self.run_attempt();self.ops.authenticated_snapshot.assert_not_called()
        self.assertEqual(result["authenticatedSnapshots"], "SKIPPED_PRIVATE_CHANNEL_UNAVAILABLE")

    def test_invalid_401_prevents_authenticated_requests(self):
        self.approval.update(unauthenticatedSnapshot=True, authenticatedSnapshots=True)
        self.ops.unauthenticated_snapshot.return_value = "INVALID"
        self.assertEqual(self.run_attempt()["result"], "QUALIFICATION_FAILED")
        self.ops.authenticated_snapshot.assert_not_called()

    def test_snapshot_failure_stops_further_requests_and_restores(self):
        self.approval["authenticatedSnapshots"] = True
        self.ops.authenticated_snapshot.return_value = "PRIVATE_PAYLOAD"
        result = self.run_attempt();self.ops.authenticated_snapshot.assert_called_once_with("BTC/USD")
        self.assertTrue(result["rollbackVerified"]);self.assertNotIn("PRIVATE_PAYLOAD", json.dumps(result))

    def test_recovery_failure_is_never_reported_successfully(self):
        self.ops.restore_baseline.side_effect = RuntimeError("PRIVATE_CANARY")
        self.assertEqual(self.run_attempt()["result"], "CRITICAL_RECOVERY_UNVERIFIED")
        self.assertEqual(self.ops.compare_baseline.call_count, 2)

    def test_independent_comparison_failure_is_critical(self):
        self.ops.compare_baseline.side_effect = [True, False]
        self.assertEqual(self.run_attempt()["result"], "CRITICAL_RECOVERY_UNVERIFIED")

    def test_cli_is_offline_only_and_explicit_about_unqualified_controller(self):
        with mock.patch.object(M.WP19, "offline_report", return_value={"result": "OFFLINE_PREFLIGHT_PASS", "PUBLIC_RUNTIME_VERIFIED": False}), mock.patch.object(M, "qualify") as live, contextlib.redirect_stdout(io.StringIO()) as output:
            self.assertEqual(M.main([]), 0)
        live.assert_not_called();value = json.loads(output.getvalue())
        self.assertEqual(value["result"], "LOCAL_PREFLIGHT_PASS");self.assertFalse(value["liveAttemptReady"])
        self.assertFalse(value["privateControllerQualified"])

    def test_real_windows_wrapper_parse_and_safe_policy_refusal(self):
        self.assertEqual(os.name, "nt")
        script = M.WP19.SCRIPTS["wp20"]
        info = M.WP19.metadata();self.assertEqual(info["scripts"]["wp20"]["parseErrors"], 0)
        p = subprocess.run(["powershell.exe", "-NoProfile", "-NonInteractive", "-File", str(script)], capture_output=True, timeout=30)
        if info["effectivePolicy"] == "Restricted":
            self.assertNotEqual(p.returncode, 0)
            self.assertTrue(b"running scripts is disabled" in p.stderr, "Expected policy refusal, raw stderr withheld")
        elif not p.stderr:
            value = json.loads(p.stdout.decode("utf-8-sig"));self.assertFalse(value["liveAttemptReady"])
            self.assertFalse(value["PUBLIC_RUNTIME_VERIFIED"])


if __name__ == "__main__":
    unittest.main()
