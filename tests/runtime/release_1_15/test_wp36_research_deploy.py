"""Fake-only WP36 tests. No Azure, registry credentials, real key or live HTTP."""
from contextlib import redirect_stdout
import importlib.util
import io
import json
from pathlib import Path
import tempfile
import unittest
from unittest.mock import Mock, patch
import urllib.error

ROOT = Path(__file__).resolve().parents[3]
SPEC = importlib.util.spec_from_file_location("wp36", ROOT / "eng/azure-cli/r1.15-qualification/wp36_research_deploy.py")
wp36 = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(wp36)


def metadata():
    return (dict(id=wp36.SITE, plan=wp36.PLAN, kind="app,linux", location="West US 2", state="Running", httpsOnly=False),
            dict(id=wp36.PLAN, linux=True, sku="F1", location="westus2"),
            dict(empty=True, classic=False, sidecar=False, candidate=False, startupEmpty=True))


class DeployTests(unittest.TestCase):
    def snapshot(self, deployed=False):
        s, p, w = metadata()
        if deployed:
            s["httpsOnly"] = True
            w.update(empty=False, classic=True, candidate=True)
        return wp36.validate_metadata(s, p, w)

    def fake_azure(self):
        azure = Mock()
        azure.inspect.side_effect = [self.snapshot(), self.snapshot(True)]
        return azure

    def test_default_plan_does_no_io(self):
        azure, registry, health = Mock(), Mock(), Mock()
        result, code = wp36.execute("Plan", azure=azure, registry=registry, health=health)
        self.assertEqual(code, 0)
        azure.inspect.assert_not_called(); registry.assert_not_called(); health.assert_not_called()
        self.assertFalse(result["settingsApiWrites"])

    def test_apply_requires_every_explicit_confirmation_before_io(self):
        for approval, key, settings in ((False, True, True), (True, False, True), (True, True, False)):
            with self.subTest(approval=approval, key=key, settings=settings):
                azure, registry, health = Mock(), Mock(), Mock()
                result, code = wp36.execute("Apply", approval, key, settings, azure, health, registry)
                self.assertEqual(code, 2)
                self.assertEqual(result["failedStage"], "APPROVAL")
                azure.inspect.assert_not_called(); registry.assert_not_called(); health.assert_not_called()

    def test_exact_identity_plan_region_f1_and_state(self):
        for part, name, value in ((0, "id", wp36.SITE.replace(wp36.APP, "ui")), (0, "plan", "wrong"),
                                  (1, "id", "wrong"), (1, "sku", "B1"), (1, "linux", None),
                                  (0, "location", "eastus"), (1, "location", "eastus"),
                                  (0, "state", "Stopped"), (0, "httpsOnly", None)):
            values = list(metadata()); values[part][name] = value
            with self.subTest(name=name, value=value), self.assertRaises(wp36.Blocked):
                wp36.validate_metadata(*values)

    def test_sidecar_unknown_image_and_nonempty_startup_rejected(self):
        for change in (dict(sidecar=True), dict(empty=False), dict(empty=None), dict(startupEmpty=False),
                       dict(empty=False, classic=True), dict(empty=True, classic=True, candidate=True)):
            s, p, w = metadata(); w.update(change)
            with self.subTest(change=change), self.assertRaises(wp36.Blocked):
                wp36.validate_metadata(s, p, w)

    def test_inspect_never_mutates_or_probes_health(self):
        azure, health = self.fake_azure(), Mock()
        result, code = wp36.execute("Inspect", azure=azure, health=health, registry=lambda: {"verified": True})
        self.assertEqual(code, 0); self.assertEqual(result["result"], "METADATA_VERIFIED_NO_MUTATION")
        azure.request.assert_not_called(); health.assert_not_called()

    def test_registry_failure_prevents_azure_access(self):
        azure = Mock()
        result, code = wp36.execute("Apply", True, True, True, azure=azure, registry=Mock(side_effect=wp36.Blocked("ANONYMOUS_PULL_UNAVAILABLE")))
        self.assertEqual(code, 2); azure.inspect.assert_not_called(); azure.request.assert_not_called()

    def test_apply_exact_three_mutations_and_sanitized_snapshot(self):
        azure = self.fake_azure(); health = Mock(return_value={"status": 200, "body": "Alive"})
        with tempfile.TemporaryDirectory() as owned, patch.object(wp36.tempfile, "mkdtemp", return_value=owned):
            result, code = wp36.execute("Apply", True, True, True, azure, health, lambda: {"verified": True})
            saved = json.loads(Path(owned, "before.json").read_text())
        self.assertEqual(code, 0); self.assertEqual(result["azureHealth"], "PASS")
        self.assertFalse(result["PUBLIC_RUNTIME_VERIFIED"]); self.assertFalse(result["liveAttemptReady"])
        calls = azure.request.call_args_list
        self.assertEqual(len(calls), 3)
        self.assertEqual(calls[0].kwargs, {"suffix": "/config/web", "body": {"properties": {"linuxFxVersion": "DOCKER|" + wp36.IMAGE, "appCommandLine": ""}}})
        self.assertEqual(calls[1].kwargs, {"body": {"properties": {"httpsOnly": True}}})
        self.assertEqual(calls[2].kwargs, {"suffix": "/restart"})
        self.assertEqual(saved["mode"], "EMPTY_CODE_MODE"); self.assertFalse(saved["httpsOnly"])
        self.assertNotIn("AIQ_RESEARCH_API_KEY", json.dumps(calls, default=str))
        self.assertEqual(saved["nonsecretSettingNames"], sorted(wp36.SETTINGS))

    def test_delta_has_exact_ten_nonsecret_settings(self):
        self.assertEqual(len(wp36.SETTINGS), 10)
        self.assertEqual(wp36.SETTINGS["WEBSITES_PORT"], "52731")
        self.assertEqual(wp36.SETTINGS["AIQ_RESEARCH_PLATFORM_HTTPS_ONLY"], "1")
        self.assertNotIn("AIQ_RESEARCH_API_KEY", wp36.SETTINGS)
        self.assertFalse(any(k.startswith("WEBSITE_") for k in wp36.SETTINGS))

    def test_azure_rejection_stops_without_automatic_recovery(self):
        azure = self.fake_azure(); azure.request.side_effect = wp36.Blocked("AZURE_REQUEST_REJECTED", "BadRequest")
        with tempfile.TemporaryDirectory() as owned, patch.object(wp36.tempfile, "mkdtemp", return_value=owned):
            result, code = wp36.execute("Apply", True, True, True, azure=azure, health=Mock(), registry=lambda: {})
        self.assertEqual(code, 2); self.assertEqual(result["failedStage"], "IMAGE_CONFIGURATION")
        self.assertTrue(result["azureMayHaveChanged"]); self.assertTrue(result["manualRecoveryRequired"])
        self.assertEqual(azure.request.call_count, 1)

    def test_snapshot_failure_prevents_mutations(self):
        azure = self.fake_azure()
        with patch.object(wp36.tempfile, "mkdtemp", side_effect=OSError("FAKE_PRIVATE_DETAILS")):
            result, code = wp36.execute("Apply", True, True, True, azure=azure, registry=lambda: {})
        self.assertEqual(code, 2); azure.request.assert_not_called()
        self.assertNotIn("FAKE_PRIVATE_DETAILS", json.dumps(result))

    def test_no_settings_or_secret_operations_possible(self):
        azure = wp36.Azure()
        for method, suffix in (("get", "/config/appsettings/list"), ("post", "/config/appsettings/list"),
                               ("put", "/config/appsettings"), ("patch", "/config/appsettings"),
                               ("post", "/config/connectionstrings/list"), ("get", "/config/slotConfigNames")):
            with self.subTest(method=method, suffix=suffix), self.assertRaises(wp36.Blocked):
                azure.request(method, suffix=suffix)
        with self.assertRaises(wp36.Blocked):
            azure.request("patch", body={"properties": {"httpsOnly": False}})
        with self.assertRaises(wp36.Blocked):
            azure.request("patch", resource=wp36.PLAN, body={"sku": "B1"})

    def test_cli_failure_does_not_echo_raw_error(self):
        done = Mock(returncode=1, stderr=b"(FAKE_PRIVATE_CANARY) raw-details", stdout=b"")
        with patch.object(wp36.subprocess, "run", return_value=done):
            with self.assertRaises(wp36.Blocked) as caught:
                wp36.Azure().request("get", query="{id:id}")
        self.assertIsNone(caught.exception.code)
        self.assertNotIn("FAKE_PRIVATE", str(caught.exception))

    def test_cli_fixed_patch_suppresses_response_and_is_bounded(self):
        with patch.object(wp36.subprocess, "run", return_value=Mock(returncode=0)) as run:
            wp36.Azure().request("patch", body={"properties": {"httpsOnly": True}})
        args = run.call_args.args[0]
        self.assertEqual(args[args.index("--output") + 1], "none")
        self.assertEqual(run.call_args.kwargs["timeout"], 25)

    def test_anonymous_registry_failure_is_sanitized(self):
        opener = Mock(); opener.open.side_effect = OSError("FAKE_PRIVATE_TRANSPORT")
        with patch.object(wp36.urllib.request, "build_opener", return_value=opener):
            with self.assertRaisesRegex(wp36.Blocked, "^ANONYMOUS_PULL_UNAVAILABLE$"):
                wp36.verify_registry()

    def test_anonymous_registry_rejects_digest_mismatch(self):
        opener = Mock()
        responses = []
        for raw in (b'{"token":"FAKE_ANONYMOUS_TOKEN"}', b'{"manifests":[]}'):
            response = Mock(status=200); response.read.return_value = raw
            response.__enter__ = Mock(return_value=response); response.__exit__ = Mock(return_value=False)
            responses.append(response)
        opener.open.side_effect = responses
        with patch.object(wp36.urllib.request, "build_opener", return_value=opener):
            with self.assertRaisesRegex(wp36.Blocked, "^REGISTRY_DIGEST_MISMATCH$"):
                wp36.verify_registry()
        self.assertEqual(opener.open.call_count, 2)
        self.assertIsNone(opener.open.call_args_list[0].args[0].get_header("Authorization"))

    def test_health_exact_contract(self):
        for status, body in ((200, b"Alive\n"), (200, b"<html>"), (200, b"Alive!"), (302, b"Alive"), (401, b"Alive"), (503, b"Alive")):
            self.assertFalse(wp36.valid_health(status, body))
        self.assertTrue(wp36.valid_health(200, b"Alive"))

    def test_health_request_is_anonymous_fixed_https(self):
        opener = Mock(); response = Mock(status=200)
        response.read.return_value = b"Alive"
        opener.open.return_value.__enter__ = Mock(return_value=response)
        opener.open.return_value.__exit__ = Mock(return_value=False)
        self.assertEqual(wp36.observe_health(opener=opener), {"status": 200, "body": "Alive"})
        request = opener.open.call_args.args[0]
        self.assertEqual(request.full_url, wp36.HEALTH)
        self.assertIsNone(request.get_header("X-api-key"))
        self.assertIsNone(request.get_header("Authorization"))
        self.assertLessEqual(opener.open.call_args.kwargs["timeout"], 8)

    def test_health_failure_is_bounded(self):
        now = [0.0]
        opener = Mock(); opener.open.side_effect = OSError("FAKE_PRIVATE_DETAILS")
        with self.assertRaisesRegex(wp36.Blocked, "HEALTH_NOT_VERIFIED"):
            wp36.observe_health(budget=3, clock=lambda: now[0], sleep=lambda seconds: now.__setitem__(0, now[0] + seconds), opener=opener)
        self.assertEqual(now[0], 3)

    def test_redirects_are_not_followed(self):
        self.assertIsNone(wp36.NoRedirect().redirect_request(None, None, 302, None, None, "https://example.invalid"))

    def test_powershell_51_wrapper_has_no_policy_or_secret_input(self):
        s = (ROOT / "eng/docker/release-1.15/Invoke-Wp36ResearchDeploy.ps1").read_text()
        self.assertIn("#Requires -Version 5.1", s)
        self.assertNotIn("ExecutionPolicy", s)
        self.assertNotIn("AIQ_RESEARCH_API_KEY", s)


if __name__ == "__main__":
    unittest.main()
