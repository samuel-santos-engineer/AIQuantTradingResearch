"""Offline planning tests; no Azure, registry, Docker or retained-state operations."""
import contextlib
import hashlib
import io
import json
import os
from pathlib import Path
import stat
import sys
import unittest
from unittest.mock import patch
import linux_deployment_preparation as m


class OfflineTests(unittest.TestCase):
    def test_default_never_spawns_cli_or_network(self):
        with patch.object(m.subprocess, "Popen", side_effect=AssertionError("no child")), contextlib.redirect_stdout(io.StringIO()) as output:
            self.assertEqual(0, m.main([]))
        value = json.loads(output.getvalue())
        self.assertFalse(value["liveAdapterImplemented"])
        self.assertFalse(value["PUBLIC_RUNTIME_VERIFIED"])
        self.assertEqual("OWNER_DECISION_REQUIRED", value["artifactSelection"])

    def test_no_apply_option(self):
        with contextlib.redirect_stderr(io.StringIO()), self.assertRaises(SystemExit) as error:
            m.main(["--apply"])
        self.assertEqual(2, error.exception.code)

    def test_allowlist_blocks_production_settings_and_restart(self):
        for method, resource in [("POST", m.API + "/restart"), ("POST", m.API + "/config/appsettings/list"), ("GET", m.ROOT + "sites/aiqr112wp035ec325382770"), ("PATCH", m.PLAN)]:
            with self.assertRaises(m.Blocked): m.validate_operation(method, resource)
        self.assertFalse(m.validate_operation("PATCH", m.API + "/config/web")["executed"])

    def snapshot(self):
        return {"resourceId": m.API, "planId": m.PLAN, "linux": True, "sku": "F1", "state": "Running", "usage": "Normal", "stopCurrent": 0, "stopLimit": 15, "configuredImage": m.ACCEPTED_API, "startup": ""}

    def test_below_quota_snapshot_does_not_prove_runtime(self):
        self.assertFalse(m.validate_snapshot(self.snapshot(), m.ACCEPTED_API)["runtimeVerified"])

    def test_quota_missing_exhausted_or_boolean_fails_closed(self):
        for value in [15, 57, None, True, -1]:
            snap = self.snapshot(); snap["stopCurrent"] = value
            with self.assertRaises(m.Blocked): m.validate_snapshot(snap, m.ACCEPTED_API)

    def test_drift_and_host_mismatch_fail_closed(self):
        for key, value in [("configuredImage", m.DIAGNOSTIC_API), ("startup", "unexpected"), ("sku", "UNKNOWN"), ("resourceId", m.UI)]:
            snap = self.snapshot(); snap[key] = value
            with self.assertRaises(m.Blocked): m.validate_snapshot(snap, m.ACCEPTED_API)

    def test_manifest_exact_bytes_and_platform(self):
        raw = json.dumps({"schemaVersion": 2, "layers": []}).encode()
        child = "sha256:" + hashlib.sha256(raw).hexdigest()
        index = json.dumps({"manifests": [{"digest": child, "platform": {"os": "linux", "architecture": "amd64"}}]}).encode()
        value = m.verify_platform(index, "sha256:" + hashlib.sha256(index).hexdigest(), raw)
        self.assertEqual(child, value["runtimeDigest"])
        self.assertFalse(value["configVerified"])
        with self.assertRaises(m.Blocked): m.verify_manifest(raw + b"\n", child)

    def test_ambiguous_platform_rejected(self):
        raw = json.dumps({"manifests": []}).encode()
        with self.assertRaises(m.Blocked): m.verify_platform(raw, "sha256:" + hashlib.sha256(raw).hexdigest(), b"{}")

    def test_recovery_uncertainty_never_claims_health(self):
        self.assertEqual("NO_MUTATION", m.recovery_decision(False, None, False, True))
        for safe, complete, drift in [(None, True, False), (False, True, False), (True, False, False), (True, True, True)]:
            self.assertEqual("MANUAL_RECOVERY_REQUIRED", m.recovery_decision(True, safe, complete, drift))
        self.assertEqual("PROPOSE_ONE_BASELINE_IMAGE_ROLLBACK", m.recovery_decision(True, True, True, False))

    def test_errors_do_not_echo_sensitive_input(self):
        with self.assertRaises(m.Blocked) as error: m.verify_manifest(b"fake-secret private-path", "sha256:" + "0" * 64)
        self.assertNotIn("fake-secret", str(error.exception))
        self.assertNotIn("private-path", str(error.exception))


@unittest.skipUnless(sys.platform == "linux", "LINUX_NOT_AVAILABLE: no Docker run authorized for this preparation")
class NativeLinuxTests(unittest.TestCase):
    def test_owner_modes_and_synthetic_file(self):
        with m.private_workspace() as (folder, descriptor):
            try:
                m.write_private(descriptor, "synthetic.json", {"synthetic": True})
                info = (folder / "synthetic.json").lstat()
                self.assertEqual(os.geteuid(), info.st_uid)
                self.assertEqual(0o600, stat.S_IMODE(info.st_mode))
                self.assertEqual(0o700, stat.S_IMODE(folder.lstat().st_mode))
            finally:
                (folder / "synthetic.json").unlink(missing_ok=True)
        self.assertFalse(folder.exists())

    def test_unsafe_directory_rejected_before_file_content(self):
        with m.private_workspace() as (folder, descriptor):
            os.chmod(folder, 0o755)
            try:
                with self.assertRaises(m.Blocked): m.write_private(descriptor, "synthetic.json", {"synthetic": True})
                self.assertFalse((folder / "synthetic.json").exists())
            finally: os.chmod(folder, 0o700)

    def test_normal_local_capture(self):
        self.assertEqual(b"SYNTHETIC\n", m.bounded_local([sys.executable, "-c", "print('SYNTHETIC')"]))

    def test_timeout_and_output_limit_fail_closed(self):
        for code, seconds, limit in [("import time;time.sleep(60)", 0.1, m.LIMIT), ("print('x'*10000)", 3, 32)]:
            with self.assertRaises(m.Blocked): m.bounded_local([sys.executable, "-c", code], seconds, limit)


if __name__ == "__main__":
    unittest.main()
