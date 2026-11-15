"""Fake-only WP62 tests; never instantiate the real CLI/private-store adapters."""
import copy
import io
import json
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
            def wait(self, timeout):
                if not self.killed: raise subprocess.TimeoutExpired("fake", timeout)
                return -1
        p = Process()
        with self.assertRaisesRegex(m.Blocked, "TIMEOUT"):
            m.capture([], popen=lambda *a, **kw: p)
        self.assertTrue(p.killed)


if __name__ == "__main__":
    unittest.main()
