"""Fake-only WP75 command tests; no executable adapter."""
import contextlib
import io
import importlib
import json
import unittest
from unittest.mock import patch
import wp75_staging_plan as m


class Tests(unittest.TestCase):
    def test_default_and_import_have_no_external_operations(self):
        with patch("subprocess.Popen", side_effect=AssertionError("process")), patch("socket.socket", side_effect=AssertionError("network")), contextlib.redirect_stdout(io.StringIO()) as out:
            importlib.reload(m)
            self.assertEqual(0, m.main([]))
        value = json.loads(out.getvalue())
        self.assertEqual("PLAN_ONLY", value["result"])
        self.assertEqual([], value["commandArgv"])
        self.assertFalse(value["executionImplemented"])

    def test_ordered_sequence_does_not_claim_acceptance(self):
        value = m.plan()
        self.assertLess(value["sequence"].index("API"), value["sequence"].index("UI"))
        self.assertLess(value["sequence"].index("API_AUTHENTIC_DATA_ACCEPTANCE"), value["sequence"].index("UI"))
        self.assertFalse(value["PUBLIC_RUNTIME_VERIFIED"])
        self.assertFalse(value["liveAttemptReady"])

    def test_publish_exact_bytes_no_rebuild_or_pull(self):
        argv = m.plan("Publish")["commandArgv"]
        self.assertEqual([["docker", "tag", m.UI_ID, m.TAG], ["docker", "push", m.TAG]], argv)

    def test_api_accepted_child_not_diagnostic(self):
        argv = m.plan("API")["commandArgv"]
        body = json.loads(argv[1][-1])["properties"]
        self.assertEqual("DOCKER|" + m.REPO + "@" + m.API_DIGEST, body["linuxFxVersion"])
        self.assertEqual("", body["appCommandLine"])
        self.assertNotIn("d33626", json.dumps(argv))
        self.assertTrue(all(m.API_APP in json.dumps(a) for a in argv))

    def test_ui_requires_sha256_mapping_not_local_id(self):
        for value in [None, "tag", m.UI_ID, "sha512:" + "a" * 128, "sha256:" + "A" * 64, "sha256:" + "a" * 63, "https://user:fake-secret@host"]:
            with self.assertRaises(ValueError): m.plan("UI", value)
        value = m.plan("UI", "sha256:" + "a" * 64)
        self.assertTrue(all(m.UI_APP in json.dumps(a) for a in value["commandArgv"]))
        self.assertFalse(value["PUBLIC_RUNTIME_VERIFIED"])

    def test_nonsecret_settings_merge_preserves_existing_contract(self):
        for role in ["API", "UI"]:
            argv = m.commands(role, "sha256:" + "a" * 64)[0]
            self.assertIn("set", argv)
            self.assertIn("--settings", argv)
            self.assertNotIn("--slot-settings", argv)
            self.assertEqual(["--output", "none"], argv[-2:])
            self.assertNotIn("AIQ_RESEARCH_API_KEY", json.dumps(argv))
            self.assertNotIn("AIQ_RESEARCH_CA_FILE", json.dumps(argv))
        self.assertEqual(m.API_URL, m.SETTINGS["UI"]["AIQ_RESEARCH_BASE_URL"])
        self.assertEqual("52731", m.SETTINGS["API"]["WEBSITES_PORT"])
        self.assertEqual("8501", m.SETTINGS["UI"]["WEBSITES_PORT"])

    def test_no_plan_resource_key_log_slot_or_restart_mutations(self):
        argv = m.commands("API") + m.commands("UI", "sha256:" + "a" * 64)
        for forbidden in ["/restart", "/config/logs", "slotConfigNames", "AIQ_RESEARCH_API_KEY=", "aiqr112wp035ec325382770"]:
            self.assertNotIn(forbidden, json.dumps(argv))
        self.assertEqual(0, m.plan("API")["azureOperations"])

    def test_rollback_requires_exact_safe_baseline_and_role(self):
        for role, ref in [(None, m.REPO + "@sha256:" + "a" * 64), ("API", "other.invalid/repo@sha256:" + "a" * 64), ("UI", m.REPO + ":latest")]:
            with self.assertRaises(ValueError): m.plan("Rollback", role=role, rollback_reference=ref)
        value = m.plan("Rollback", role="UI", rollback_reference=m.REPO + "@sha256:" + "b" * 64)
        self.assertEqual(1, len(value["commandArgv"]))
        self.assertNotIn("httpsOnly", json.dumps(value["commandArgv"]))

    def test_invalid_cli_input_does_not_echo_secretlike_value(self):
        with contextlib.redirect_stdout(io.StringIO()) as out:
            self.assertEqual(2, m.main(["--phase", "UI", "--ui-digest", "fake-sensitive"]))
        self.assertNotIn("fake-sensitive", out.getvalue())
        self.assertFalse(json.loads(out.getvalue())["liveAttemptReady"])

    def test_invalid_phase_and_role_fail_closed(self):
        with self.assertRaises(ValueError): m.plan("Apply")
        with self.assertRaises(ValueError): m.commands("Production")


if __name__ == "__main__": unittest.main()
