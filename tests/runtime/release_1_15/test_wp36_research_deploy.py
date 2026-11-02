"""Fake-only WP36 tests. No Azure, registry credentials, real key or live HTTP."""
from contextlib import redirect_stdout
import importlib.util
import io
import copy
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
        with patch.object(wp36.urllib.request, "build_opener", return_value=opener), redirect_stdout(io.StringIO()) as output:
            exec(wp36.HEALTH_PROBE_CODE, {})
        self.assertEqual(json.loads(output.getvalue()), {"status": 200, "bodyMatches": True})
        request = opener.open.call_args.args[0]
        self.assertEqual(request.full_url, wp36.HEALTH)
        self.assertIsNone(request.get_header("X-api-key"))
        self.assertIsNone(request.get_header("Authorization"))
        self.assertLessEqual(opener.open.call_args.kwargs["timeout"], 8)

    def test_health_failure_is_bounded(self):
        now = [0.0]
        probe = Mock(return_value=(None, b""))
        with self.assertRaisesRegex(wp36.Blocked, "HEALTH_NOT_VERIFIED"):
            wp36.observe_health(budget=3, clock=lambda: now[0], sleep=lambda seconds: now.__setitem__(0, now[0] + seconds), probe=probe)
        self.assertEqual(now[0], 3)

    def test_redirects_are_not_followed(self):
        self.assertIsNone(wp36.NoRedirect().redirect_request(None, None, 302, None, None, "https://example.invalid"))

    def test_powershell_51_wrapper_has_no_policy_or_secret_input(self):
        s = (ROOT / "eng/docker/release-1.15/Invoke-Wp36ResearchDeploy.ps1").read_text()
        self.assertIn("#Requires -Version 5.1", s)
        self.assertNotIn("ExecutionPolicy", s)
        self.assertNotIn("AIQ_RESEARCH_API_KEY", s)


class Wp37DeadlineTests(unittest.TestCase):
    def test_drifted_mode_or_https_stops_before_write(self):
        for change in ({"mode": "EXPECTED_CLASSIC_IMAGE"}, {"httpsOnly": True}):
            azure = Mock(); snapshot = DeployTests().snapshot(); snapshot.update(change)
            azure.inspect.return_value = snapshot
            with self.subTest(change=change):
                result, code = wp36.execute("Apply", True, True, True, azure=azure, registry=lambda: {})
                self.assertEqual(code, 2)
                self.assertEqual(result["failureReason"], "PRE_APPLY_METADATA_DRIFT")
                azure.request.assert_not_called()

    def test_cli_uses_direct_interpreter_and_pinned_subscription(self):
        done = Mock(returncode=0)
        with patch.object(wp36, "cli_entrypoint", return_value=["owned-cli-python.exe", "-IBm", "azure.cli"]), patch.object(wp36.subprocess, "run", return_value=done) as run:
            wp36.Azure().request("patch", body={"properties": {"httpsOnly": True}})
        args = run.call_args.args[0]
        self.assertEqual(args[:4], ["owned-cli-python.exe", "-IBm", "azure.cli", "rest"])
        self.assertEqual(args[args.index("--subscription") + 1], wp36.SUB)
        self.assertFalse(run.call_args.kwargs.get("shell", False))
        self.assertEqual(run.call_args.kwargs["timeout"], 25)

    def test_whole_health_process_has_remaining_deadline(self):
        done = Mock(returncode=0, stdout=b'{"status":200,"bodyMatches":true}')
        with patch.object(wp36.subprocess, "run", return_value=done) as run:
            self.assertEqual(wp36.bounded_health_probe(.25), (200, b"Alive"))
        self.assertEqual(run.call_args.kwargs["timeout"], .25)
        self.assertEqual(run.call_args.args[0][-1], wp36.HEALTH_PROBE_CODE)
        self.assertFalse(run.call_args.kwargs.get("shell", False))

    def test_whole_request_timeout_is_pending_without_output_disclosure(self):
        with patch.object(wp36.subprocess, "run", side_effect=wp36.subprocess.TimeoutExpired("FAKE_PRIVATE_CANARY", 8)):
            self.assertEqual(wp36.bounded_health_probe(8), (None, b""))

    def test_late_alive_is_not_accepted(self):
        now = [0.0]
        def late_probe(timeout):
            self.assertEqual(timeout, 3)
            now[0] += 3.01
            return 200, b"Alive"
        with self.assertRaisesRegex(wp36.Blocked, "HEALTH_NOT_VERIFIED"):
            wp36.observe_health(budget=3, clock=lambda: now[0], probe=late_probe)

    def test_final_request_uses_remaining_budget_and_no_extra_request(self):
        now = [0.0]; timeouts = []
        def probe(timeout):
            timeouts.append(timeout); now[0] += timeout
            return None, b""
        with self.assertRaisesRegex(wp36.Blocked, "HEALTH_NOT_VERIFIED"):
            wp36.observe_health(budget=11, clock=lambda: now[0], sleep=lambda seconds: now.__setitem__(0, now[0] + seconds), probe=probe)
        self.assertEqual(timeouts, [8, 1]); self.assertEqual(now[0], 11)

    def test_malformed_worker_result_cannot_pass(self):
        for data in (b'{}', b'{"status":200,"bodyMatches":"true"}', b'{"status":200,"bodyMatches":true,"extra":1}', b'invalid'):
            with self.subTest(data=data), patch.object(wp36.subprocess, "run", return_value=Mock(returncode=0, stdout=data)):
                with self.assertRaises(wp36.Blocked):
                    wp36.bounded_health_probe(8)

    def test_redirect_or_auth_response_is_not_retried(self):
        for status in (301, 302, 307, 401, 403):
            probe = Mock(return_value=(status, b""))
            with self.subTest(status=status), self.assertRaisesRegex(wp36.Blocked, "HEALTH_REJECTED"):
                wp36.observe_health(probe=probe)
            probe.assert_called_once()


class FakeSettings:
    def __init__(self):
        self.values = {"AIQ_RESEARCH_API_KEY": "FAKE_SECRET_CANARY", "privateUnrelated": "FAKE_UNRELATED_CANARY", "WEBSITES_PORT": "old"}
        self.sticky = {"app_setting_names": ["AIQ_RESEARCH_API_KEY", "privateUnrelated", "WEBSITES_PORT"],
                       "connection_string_names": None, "azure_storage_config_names": []}
        self.saved = 0
        self.snapshot = DeployTests().snapshot()

    def metadata(self):
        return copy.deepcopy(self.snapshot)

    def private_settings(self):
        return dict(self.values)

    def flags(self):
        return copy.deepcopy(self.sticky)

    def save(self):
        self.saved += 1
        self.values.update(wp36.SETTINGS)


class SettingsTests(unittest.TestCase):
    def run_settings(self, adapter, approved=True):
        stdout, stderr = io.StringIO(), io.StringIO()
        from contextlib import redirect_stderr
        with redirect_stdout(stdout), redirect_stderr(stderr):
            result, code = wp36.execute_settings(approved, adapter, lambda: "2.89.1")
            print(json.dumps(result))
        self.assertNotIn("FAKE_SECRET_CANARY", stdout.getvalue() + stderr.getvalue())
        self.assertNotIn("FAKE_UNRELATED_CANARY", stdout.getvalue() + stderr.getvalue())
        return result, code

    def test_settings_plan_no_io_and_approval_is_separate(self):
        with patch.object(wp36, "launch_settings_worker") as worker, redirect_stdout(io.StringIO()):
            self.assertEqual(wp36.main(["--action", "SettingsPlan"]), 0)
            self.assertEqual(wp36.main(["--action", "SettingsApply", "--approve-exact-research-deploy"]), 2)
            worker.assert_not_called()
        with patch.object(wp36, "execute") as deploy, patch.object(wp36, "launch_settings_worker", return_value=(wp36.settings_plan(), 0)), redirect_stdout(io.StringIO()):
            self.assertEqual(wp36.main(["--action", "SettingsApply", "--approve-research-settings-save"]), 0)
            deploy.assert_not_called()

    def test_merge_preserves_key_unrelated_and_all_flags(self):
        adapter = FakeSettings(); flags = copy.deepcopy(adapter.sticky)
        result, code = self.run_settings(adapter)
        self.assertEqual(code, 0); self.assertEqual(result["result"], "settings saved")
        self.assertTrue(all(result["checks"].values()))
        self.assertEqual(adapter.saved, 1)
        self.assertEqual(adapter.values["AIQ_RESEARCH_API_KEY"], "FAKE_SECRET_CANARY")
        self.assertEqual(adapter.values["privateUnrelated"], "FAKE_UNRELATED_CANARY")
        self.assertEqual(adapter.sticky, flags)
        self.assertTrue(all(adapter.values[k] == v for k, v in wp36.SETTINGS.items()))
        self.assertFalse(result["imageDeployInvoked"]); self.assertFalse(result["explicitRestartInvoked"])
        self.assertFalse(result["healthProbed"])

    def test_missing_key_stops_before_save(self):
        adapter = FakeSettings(); del adapter.values["AIQ_RESEARCH_API_KEY"]
        result, code = self.run_settings(adapter)
        self.assertEqual(code, 2); self.assertEqual(adapter.saved, 0)
        self.assertEqual(result["failureReason"], "KEY_NAME_ABSENT_OR_UNVERIFIED")

    def test_orphan_sticky_new_name_stops_before_save(self):
        adapter = FakeSettings(); adapter.sticky["app_setting_names"].append("AIQ_RESEARCH_BOOTSTRAP_MODE")
        result, code = self.run_settings(adapter)
        self.assertEqual(code, 2); self.assertEqual(adapter.saved, 0)
        self.assertEqual(result["failureReason"], "SETTINGS_PRESERVATION_UNVERIFIED")

    def test_probe_failure_prevents_any_azure_access(self):
        adapter = Mock()
        result, code = wp36.execute_settings(True, adapter, Mock(side_effect=wp36.Blocked("SETTINGS_PRESERVATION_UNVERIFIED")))
        self.assertEqual(code, 2); adapter.metadata.assert_not_called(); adapter.save.assert_not_called()

    def test_no_settings_read_before_approval(self):
        adapter = Mock()
        result, code = wp36.execute_settings(False, adapter, Mock())
        self.assertEqual(code, 2); adapter.private_settings.assert_not_called(); adapter.metadata.assert_not_called()

    def test_post_save_mismatch_never_claims_saved(self):
        for defect in ("key", "unrelated", "flag", "value", "mode", "extra"):
            adapter = FakeSettings()
            original = adapter.save
            def bad_save():
                original()
                if defect == "key": adapter.values["AIQ_RESEARCH_API_KEY"] = "changed"
                if defect == "unrelated": adapter.values["privateUnrelated"] = "changed"
                if defect == "flag": adapter.sticky["app_setting_names"] = []
                if defect == "value": adapter.values["WEBSITES_PORT"] = "wrong"
                if defect == "mode": adapter.snapshot["httpsOnly"] = True
                if defect == "extra": adapter.values["unexpected"] = "new"
            adapter.save = bad_save
            with self.subTest(defect=defect):
                result, code = self.run_settings(adapter)
                self.assertEqual(code, 2); self.assertFalse(result["settingsSaved"])
                self.assertEqual(adapter.saved, 1); self.assertTrue(result["azureMayHaveChanged"])

    def test_timeout_and_error_do_not_retry_or_echo(self):
        for error in (wp36.Blocked("SETTINGS_SAVE_TIMEOUT_UNCERTAIN"), ValueError("FAKE_SECRET_CANARY")):
            adapter = FakeSettings(); adapter.save = Mock(side_effect=error)
            result, code = self.run_settings(adapter)
            self.assertEqual(code, 2); adapter.save.assert_called_once()
            self.assertTrue(result["azureMayHaveChanged"]); self.assertFalse(result["automaticRetry"])

    def test_save_command_exact_scope_values_and_output_none(self):
        adapter = object.__new__(wp36.SettingsAdapter)
        with patch.object(wp36.subprocess, "run", return_value=Mock(returncode=0)) as run:
            adapter.save()
        args = run.call_args.args[0]
        self.assertEqual(args[1:7], ["-IBm", "azure.cli", "webapp", "config", "appsettings", "set"])
        for name, value in (("--subscription", wp36.SUB), ("--resource-group", wp36.GROUP), ("--name", wp36.APP), ("--output", "none")):
            self.assertEqual(args[args.index(name) + 1], value)
        start = args.index("--settings") + 1
        self.assertEqual(args[start:start+10], [k + "=" + v for k, v in wp36.SETTINGS.items()])
        for forbidden in ("--slot-settings", "--debug", "--verbose", "AIQ_RESEARCH_API_KEY", "FAKE_SECRET_CANARY"):
            self.assertNotIn(forbidden, args)
        self.assertEqual(run.call_args.kwargs["timeout"], 90)
        self.assertEqual(run.call_args.kwargs["env"]["AZURE_LOGGING_ENABLE_LOG_FILE"], "false")

    def test_native_cli_timeout_is_sanitized(self):
        adapter = object.__new__(wp36.SettingsAdapter)
        with patch.object(wp36.subprocess, "run", side_effect=wp36.subprocess.TimeoutExpired("FAKE_SECRET_CANARY", 90)):
            with self.assertRaisesRegex(wp36.Blocked, "^SETTINGS_SAVE_TIMEOUT_UNCERTAIN$"):
                adapter.save()


if __name__ == "__main__":
    unittest.main()
