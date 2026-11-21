"""Fake-only image promotion/preservation tests. No Azure adapter."""
import copy
import contextlib
import importlib
import io
import json
import unittest
from unittest.mock import patch
import wp77_ui_promotion as m


class Fake:
    def __init__(self):
        self.calls = []
        self.site = {"serverFarmId": m.PLAN, "state": "Running", "usageState": "Normal",
                     "availabilityState": "Normal", "httpsOnly": True}
        self.web = {"linuxFxVersion": m.OLD, "appCommandLine": "", "minTlsVersion": "1.2",
                    "ipSecurityRestrictions": [{"fake": "unchanged"}]}
        self.settings = {"WEBSITES_PORT": "8501", "unrelated": "fake-sensitive", "AIQ_RESEARCH_API_KEY": "FAKE_RETAINED_KEY"}
        self.slots = {"appSettingNames": ["unrelated"], "connectionStringNames": None, "azureStorageConfigNames": []}
        self.fail_patch = False
        self.drift_settings = False

    def __call__(self, method, path, body=None):
        self.calls.append((method, path, copy.deepcopy(body)))
        if method == "PATCH":
            if self.fail_patch: raise OSError("fake-sensitive")
            assert path == m.SITE + "/config/web"
            assert body == {"properties": {"linuxFxVersion": m.TARGET, "appCommandLine": ""}}
            self.web.update(body["properties"])
            if self.drift_settings: self.settings["unrelated"] = "changed"
            return {}
        if path == m.SITE:
            return {"id": path, "kind": "app,linux,container", "location": "West US 2", "properties": copy.deepcopy(self.site)}
        if path.endswith("/config/web"): return {"id": path, "properties": copy.deepcopy(self.web)}
        if path.endswith("/config/appsettings/list"):
            return {"id": path[:-5], "properties": copy.deepcopy(self.settings)}
        if path.endswith("/config/slotConfigNames"):return {"id": path, "properties": copy.deepcopy(self.slots)}
        raise AssertionError("scope")


class Tests(unittest.TestCase):
    def test_default_import_no_process_network(self):
        with patch("subprocess.Popen", side_effect=AssertionError()), patch("socket.socket", side_effect=AssertionError()), contextlib.redirect_stdout(io.StringIO()) as out:
            importlib.reload(m); self.assertEqual(0, m.main())
        self.assertEqual("PLAN_ONLY", json.loads(out.getvalue())["result"])

    def test_single_targeted_patch_with_preserved_settings(self):
        f = Fake(); settings = copy.deepcopy(f.settings); slots = copy.deepcopy(f.slots)
        r = m.execute(f)
        self.assertEqual("IMAGE_DEPLOYMENT_CONFIGURED_INTEGRATION_PENDING", r["result"])
        self.assertTrue(r["UI_IMAGE_CONFIGURED"])
        self.assertEqual(1, r["patchAttempts"])
        self.assertEqual(1, sum(c[0] == "PATCH" for c in f.calls))
        self.assertEqual(settings, f.settings); self.assertEqual(slots, f.slots)
        self.assertTrue(all(c[1].startswith(m.SITE) for c in f.calls))
        for c in f.calls:
            if c[0] == "POST":self.assertTrue(c[1].endswith("/appsettings/list")); self.assertIsNone(c[2])

    def test_missing_key_does_not_block_image_or_claim_integration(self):
        f = Fake(); del f.settings["AIQ_RESEARCH_API_KEY"]
        r = m.execute(f)
        self.assertTrue(r["UI_IMAGE_CONFIGURED"]); self.assertFalse(r["uiKeyPresent"])
        for key in ["SAME_KEY_PRESENT", "API_HEALTH_ACCEPTED", "UI_RUNNING_IMAGE_VERIFIED", "BTC_ETH_BROWSER_ACCEPTED", "PUBLIC_RUNTIME_VERIFIED", "liveAttemptReady"]:self.assertFalse(r[key])

    def test_idempotent_target_no_repatch(self):
        f = Fake(); f.web["linuxFxVersion"] = m.TARGET
        r = m.execute(f); self.assertTrue(r["UI_IMAGE_CONFIGURED"]); self.assertEqual(0, r["patchAttempts"])

    def test_baseline_drift_stops_before_write(self):
        for change in [lambda f:f.site.update(httpsOnly=False), lambda f:f.site.update(serverFarmId="wrong"),
                       lambda f:f.web.update(linuxFxVersion="wrong"), lambda f:f.web.update(appCommandLine="wrong"),
                       lambda f:f.settings.update(WEBSITES_PORT="80")]:
            f = Fake(); change(f); r = m.execute(f)
            self.assertEqual(0, r["patchAttempts"]); self.assertFalse(any(c[0] == "PATCH" for c in f.calls))
            if r.get("failedStage") == "UI_SITE_BASELINE":
                self.assertIn("siteChecks", r)
                self.assertTrue(any(value is False for value in r["siteChecks"].values()))

    def test_ambiguous_patch_stops_no_retry_or_rollback(self):
        f = Fake(); f.fail_patch = True; r = m.execute(f)
        self.assertEqual("UI_IMAGE_PATCH", r["failedStage"])
        self.assertTrue(r["azureMayHaveChanged"])
        self.assertEqual(1, sum(c[0] == "PATCH" for c in f.calls))
        self.assertFalse(r["UI_IMAGE_CONFIGURED"])
        self.assertNotIn("fake-sensitive", json.dumps(r))

    def test_preservation_failure_does_not_rollback(self):
        f = Fake(); f.drift_settings = True; r = m.execute(f)
        self.assertTrue(r["UI_IMAGE_CONFIGURED"])
        self.assertEqual("SETTINGS_PRESERVATION_UNVERIFIED", r["failureCategory"])
        self.assertEqual(1, sum(c[0] == "PATCH" for c in f.calls))

    def test_no_private_values_in_report(self):
        f = Fake(); r = m.execute(f)
        for value in ["fake-sensitive", "FAKE_RETAINED_KEY", "ipSecurityRestrictions"]:self.assertNotIn(value, json.dumps(r))
        self.assertEqual(9, len(f.calls))


if __name__ == "__main__": unittest.main()
