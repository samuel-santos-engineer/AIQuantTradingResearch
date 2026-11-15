"""Fake-only WP62 tests; never instantiate the real CLI/private-store adapters."""
import copy
import io
import json
import os
from pathlib import Path
import shutil
import sys
import tempfile
import subprocess
import unittest
from unittest.mock import patch
import wp62_staging_observation as m


class Store:
    def __init__(self, fail=None):
        self.files = {}
        self.fail = fail

    def write(self, name, value):
        if name == self.fail:
            raise OSError("fake-secret-private-path")
        self.files[name] = copy.deepcopy(value)


class Fake:
    def __init__(self):
        self.calls = []
        self.pair = {"linuxFxVersion": m.OLD, "appCommandLine": ""}
        self.logs = {"applicationLogs": {"fileSystem": {"level": "Off"}}, "httpLogs": {}, "failedRequestsTracing": {}, "detailedErrorMessages": {}}
        self.quota_values = [0]
        self.quota_calls = 0
        self.fail = None
        self.drift = False
        self.web_reads = 0
        self.mismatch_logs = False
        self.observation = {"stages": [], "listenerMarker": True, "candidateBound": False}

    def request(self, method, resource, body=None):
        self.calls.append((method, resource, copy.deepcopy(body)))
        if method == "get" and resource == m.PLAN + "/usages":
            n = self.quota_values[min(self.quota_calls, len(self.quota_values) - 1)]
            self.quota_calls += 1
            if n is None:
                raise m.Blocked("QUOTA_SCHEMA")
            return {"value": [{"name": {"value": "WPStopRequests"}, "currentValue": n, "limit": 15, "unit": "Count"}]}
        if method == "get" and resource == m.SITE:
            return {"kind": "app,linux,container", "properties": {"serverFarmId": m.PLAN, "state": "Running", "usageState": "Normal", "availabilityState": "Normal", "httpsOnly": True}}
        if resource.endswith("/config/web"):
            if method == "get":
                self.web_reads += 1
                return {"properties": dict(self.pair, appCommandLine="fake-drift" if self.drift and self.web_reads == 2 else self.pair["appCommandLine"])}
            self.pair = copy.deepcopy(body["properties"])
            if self.fail == "patch":
                self.fail = None
                raise m.Blocked("TIMEOUT")  # Simulate a late/uncertain applied write.
            return {}
        if resource.endswith("/config/logs"):
            if method == "put":
                self.logs = copy.deepcopy(body["properties"])
                if self.mismatch_logs:
                    self.logs["unexpected"] = True
                return {}
            return {"properties": copy.deepcopy(self.logs)}
        raise AssertionError("Unexpected fake operation")

    def enable_logging(self):
        self.calls.append(("logging", m.SITE, None))
        if self.fail == "enable":
            raise m.Blocked("TIMEOUT")
        self.logs["applicationLogs"]["fileSystem"]["level"] = "Verbose"

    def observe(self):
        self.calls.append(("observe", m.SITE, None))
        if self.fail == "observe":
            raise OSError("fake-secret-private-path")
        return self.observation


class Tests(unittest.TestCase):
    def test_plan_never_constructs_adapters(self):
        self.assertEqual("PLAN_ONLY", m.execute(None, None)["result"])
        with patch.object(m, "PrivateStore", side_effect=AssertionError("must not instantiate")):
            self.assertEqual(0, m.main([]))

    def test_apply_requires_both_approvals(self):
        with patch.object(m, "PrivateStore", side_effect=AssertionError("must not instantiate")):
            with self.assertRaises(m.Blocked):
                m.main(["--apply"])
            with self.assertRaises(m.Blocked):
                m.main(["--apply", "--approve-exact-live-apply"])

    def test_quota_pressure_stops_before_baseline(self):
        a, s = Fake(), Store()
        a.quota_values = [15]
        r = m.execute(a, s, True, True)
        self.assertEqual("QUOTA_EXCEEDED", r["result"])
        self.assertEqual(1, len(a.calls))
        self.assertFalse(s.files)

    def test_missing_quota_stops(self):
        a = Fake(); a.quota_values = [None]
        self.assertEqual("QUOTA_SCHEMA", m.execute(a, Store(), True, True)["result"])
        self.assertEqual(1, len(a.calls))

    def test_baseline_persistence_failure_prevents_writes(self):
        a = Fake()
        r = m.execute(a, Store("baseline.json"), True, True)
        self.assertEqual("UNEXPECTED_FAILURE", r["result"])
        self.assertTrue(all(c[0] == "get" for c in a.calls))
        self.assertNotIn("fake-secret", json.dumps(r))

    def test_drift_prevents_mutation(self):
        a = Fake(); a.drift = True
        self.assertEqual("PRE_APPLY_DRIFT", m.execute(a, Store(), True, True)["result"])
        self.assertTrue(all(c[0] == "get" for c in a.calls))

    def test_unbound_marker_rolls_back_and_restores_logging(self):
        a, s = Fake(), Store(); original = copy.deepcopy(a.logs)
        r = m.execute(a, s, True, True)
        self.assertEqual("ATTRIBUTABLE_STARTUP_NOT_ESTABLISHED", r["result"])
        self.assertTrue(r["imageRollbackVerified"] and r["loggingRestoreVerified"])
        self.assertEqual(a.logs, original)
        self.assertEqual(a.pair["linuxFxVersion"], m.OLD)
        self.assertIn("recovery.json", s.files)
        self.assertEqual(2, sum(c[0] == "patch" for c in a.calls))

    def test_uncertain_apply_restores_once(self):
        a = Fake(); a.fail = "patch"
        r = m.execute(a, Store(), True, True)
        self.assertTrue(r["imageRollbackVerified"] and r["loggingRestoreVerified"])
        self.assertEqual(2, sum(c[0] == "patch" for c in a.calls))

    def test_enable_failure_restores_logs_without_image_change(self):
        a = Fake(); a.fail = "enable"
        r = m.execute(a, Store(), True, True)
        self.assertTrue(r["loggingRestoreVerified"])
        self.assertFalse(any(c[0] == "patch" for c in a.calls))

    def test_quota_after_observation_blocks_rollback_without_retry(self):
        a = Fake(); a.quota_values = [0, 57]
        r = m.execute(a, Store(), True, True)
        self.assertFalse(r["imageRollbackVerified"])
        self.assertTrue(r["manualRecoveryRequired"] and r["loggingRestoreVerified"])
        self.assertEqual(2, a.quota_calls)
        self.assertEqual(1, sum(c[0] == "patch" for c in a.calls))

    def test_logging_restoration_mismatch_is_reported(self):
        a = Fake(); a.mismatch_logs = True
        r = m.execute(a, Store(), True, True)
        self.assertFalse(r["loggingRestoreVerified"])
        self.assertTrue(r["manualRecoveryRequired"])

    def test_observation_exception_does_not_leak(self):
        a = Fake(); a.fail = "observe"
        r = m.execute(a, Store(), True, True)
        self.assertEqual("UNEXPECTED_FAILURE", r["result"])
        self.assertNotIn("fake-secret", json.dumps(r))
        self.assertTrue(r["imageRollbackVerified"])

    def test_classifier_never_returns_private_text_or_attestation(self):
        raw = b"fake-secret /private/key exception token Research snapshot service unavailable. stage=TLS\nResearch snapshot listener started."
        r = m.classify(raw)
        self.assertEqual(["TLS"], r["stages"])
        self.assertTrue(r["listenerMarker"])
        self.assertFalse(r["candidateBound"])
        self.assertNotIn("private", json.dumps(r))

    def test_adapter_operation_and_delta_allowlist(self):
        a = object.__new__(m.Azure); a.store = Store(); a.entry = []; a.rollback = None; a.logging = None
        for method, resource, body in [("get", m.SITE + "/config/appsettings", None), ("post", m.SITE + "/restart", None), ("get", m.SITE, {"unexpected": True}), ("patch", m.SITE + "/config/web", None), ("patch", m.SITE + "/config/web", {"properties": {"linuxFxVersion": "wrong"}})]:
            with self.assertRaises(m.Blocked):
                a.request(method, resource, body)

    def test_capture_combined_limit(self):
        class Process:
            stdout = io.BytesIO(b"x" * (m.LIMIT + 1))
            stderr = io.BytesIO()
            returncode = 0
            def kill(self): pass
            def poll(self): return self.returncode
            def wait(self, timeout): return 0
        with self.assertRaisesRegex(m.Blocked, "OUTPUT_LIMIT"):
            m.capture([], popen=lambda *a, **kw: Process())

    def test_capture_timeout_kills_owned_process(self):
        class Process:
            stdout = io.BytesIO()
            stderr = io.BytesIO()
            returncode = -1
            killed = False
            def kill(self): self.killed = True
            def poll(self): return self.returncode if self.killed else None
            def wait(self, timeout):
                if not self.killed: raise subprocess.TimeoutExpired("fake", timeout)
                return -1
        p = Process()
        with self.assertRaisesRegex(m.Blocked, "TIMEOUT"):
            m.capture([], seconds=0.01, popen=lambda *a, **kw: p)
        self.assertTrue(p.killed)


    def test_cli_request_is_argument_array_for_exact_resource(self):
        adapter = object.__new__(m.Azure)
        adapter.store = Store(); adapter.entry = ["fake-python.exe", "-IBm", "azure.cli"]
        adapter.rollback = adapter.logging = None
        response = json.dumps({"id": m.SITE + "/config/web", "properties": {}}).encode()
        with patch.object(m, "capture", return_value=response) as runner:
            adapter.request("get", m.SITE + "/config/web")
        command = runner.call_args.args[0]
        self.assertIsInstance(command, list)
        self.assertEqual(["fake-python.exe", "-IBm", "azure.cli", "rest"], command[:4])
        self.assertEqual("https://management.azure.com" + m.SITE + "/config/web?api-version=2023-12-01", command[command.index("--url") + 1])
        self.assertNotIn("az.cmd", command)

    def test_live_apply_remains_blocked_even_with_flags(self):
        with self.assertRaisesRegex(m.Blocked, "LIVE_ADAPTER_BLOCKED"):
            m.Azure(Store())
        with patch.object(m, "PrivateStore", side_effect=AssertionError("must not construct")):
            with self.assertRaisesRegex(m.Blocked, "LIVE_APPLY_BLOCKED"):
                m.main(["--apply", "--approve-exact-live-apply", "--approve-conditional-rollback"])

    def test_job_cleanup_error_fails_closed(self):
        class Owner:
            def attach_and_resume(self, proc): pass
            def finish(self): raise m.Blocked("JOB_QUERY_FAILED")
        class Process:
            stdout = io.BytesIO()
            stderr = io.BytesIO()
            returncode = 0
            def wait(self, timeout): return 0
            def poll(self): return 0
        with self.assertRaisesRegex(m.Blocked, "JOB_QUERY_FAILED"):
            m.capture([], popen=lambda *a, **kw: Process(), owner_factory=Owner)

    def test_failed_rollback_retains_private_ledger(self):
        a, store = Fake(), Store()
        original = a.request
        def request(method, resource, body=None):
            if method == "patch" and body == a.rollback:
                raise m.Blocked("TIMEOUT")
            return original(method, resource, body)
        a.request = request
        result = m.execute(a, store, True, True)
        self.assertTrue(result["manualRecoveryRequired"])
        self.assertFalse(result["imageRollbackVerified"])
        self.assertTrue(result["loggingRestoreVerified"])
        self.assertTrue(store.files["ledger.json"]["imageMayHaveChanged"])
        self.assertIn("recovery.json", store.files)


@unittest.skipUnless(os.name == "nt" and os.environ.get("AIQ_WP62_WINDOWS_OFFLINE_TESTS") == "1", "genuine Windows synthetic qualification opt-in required")
class WindowsOfflineTests(unittest.TestCase):
    def own_capture(self, code, seconds=5):
        owners = []
        def factory():
            owner = m.WindowsJob(); owners.append(owner); return owner
        try:
            return m.capture([sys.executable, "-B", "-c", code], seconds=seconds, owner_factory=factory)
        finally:
            self.assertEqual(1, len(owners))
            self.assertTrue(owners[0].cleanup_verified)

    def test_normal_parent_exit_cleans_descendant_and_pipes(self):
        code = "import subprocess,sys; subprocess.Popen([sys.executable,'-c','import time; time.sleep(60)']); print('PARENT_COMPLETE',flush=True)"
        self.assertIn(b"PARENT_COMPLETE", self.own_capture(code))

    def test_timeout_cleans_owned_tree(self):
        code = "import subprocess,sys,time; subprocess.Popen([sys.executable,'-c','import time; time.sleep(60)']); print('READY',flush=True); time.sleep(60)"
        with self.assertRaisesRegex(m.Blocked, "TIMEOUT"):
            self.own_capture(code, 1)

    def test_overflow_cleans_owned_tree(self):
        code = "import subprocess,sys,time; subprocess.Popen([sys.executable,'-c','import time; time.sleep(60)']); sys.stdout.write('x'*2097152); sys.stdout.flush(); time.sleep(60)"
        with self.assertRaisesRegex(m.Blocked, "OUTPUT_LIMIT"):
            self.own_capture(code)

    def test_real_private_acl_and_synthetic_file_inheritance(self):
        try:
            store = m.PrivateStore()
        except m.Blocked as error:
            if str(error) == "WINDOWS_ACL_PRIVILEGE_NOT_HELD":
                self.skipTest("WINDOWS_ACL_NOT_QUALIFIED: genuine host denied required ACL privilege before any baseline write")
            raise
        try:
            store.write("recovery.json", {"synthetic": True})
            environment = dict(os.environ)
            environment["AIQ_WP62_SYNTHETIC_FILE"] = str(store.directory / "recovery.json")
            script = ("$ErrorActionPreference='Stop'; $a=Get-Acl -LiteralPath $env:AIQ_WP62_SYNTHETIC_FILE; "
                      "$sid=[System.Security.Principal.WindowsIdentity]::GetCurrent().User.Value; "
                      "if($a.GetOwner([System.Security.Principal.SecurityIdentifier]).Value -ne $sid){exit 2}; "
                      "$rules=@($a.GetAccessRules($true,$true,[System.Security.Principal.SecurityIdentifier])); "
                      "if($rules.Count -ne 2 -or $a.AreAccessRulesProtected){exit 3}; "
                      "foreach($r in $rules){if(-not $r.IsInherited -or $r.AccessControlType -ne 'Allow' -or "
                      "$r.IdentityReference.Value -notin @($sid,'S-1-5-18') -or $r.FileSystemRights -ne 'FullControl'){exit 4}}; 'SYNTHETIC_FILE_ACL_PASS'")
            proc = subprocess.run(["powershell.exe", "-NoProfile", "-NonInteractive", "-Command", script], env=environment, capture_output=True, timeout=15)
            self.assertEqual(0, proc.returncode, "synthetic ACL check failed; raw output withheld")
            self.assertEqual(b"SYNTHETIC_FILE_ACL_PASS", proc.stdout.strip())
        finally:
            resolved = store.directory.resolve()
            self.assertEqual(Path(tempfile.gettempdir()).resolve(), resolved.parent)
            self.assertTrue(resolved.name.startswith("aiq-wp62-recovery-"))
            shutil.rmtree(resolved)
            self.assertFalse(resolved.exists())


if __name__ == "__main__":
    unittest.main()
