"""Synthetic-only presence/provenance tests. No credential/CLI/network imports."""
import contextlib
import hashlib
import io
import json
import unittest
from unittest.mock import patch
import setting_name_provenance as m


class Tests(unittest.TestCase):
    def payload(self, role="api", properties=None):
        return json.dumps({"id": m.BASE + m.APPS[role] + "/config/appsettings", "properties": properties if properties is not None else {}}).encode()

    def test_present_absent_without_echo_values_or_unknown_names(self):
        raw = self.payload(properties={"AIQ_RESEARCH_API_KEY": "fake-secret-DO-NOT-ECHO", "PRIVATE_UNKNOWN_NAME": "private-path"})
        value = m.parse_presence(raw, "api")
        self.assertEqual("PRESENT", value["AIQ_RESEARCH_API_KEY"])
        self.assertEqual("ABSENT", value["WEBSITES_PORT"])
        result = json.dumps(value)
        for secret in ["fake-secret", "PRIVATE_UNKNOWN_NAME", "private-path"]:
            self.assertNotIn(secret, result)

    def test_empty_null_and_blank_values_are_presence_only(self):
        for value in [None, "", "  "]:
            self.assertEqual("PRESENT", m.parse_presence(self.payload(properties={"AIQ_RESEARCH_API_KEY": value}), "api")["AIQ_RESEARCH_API_KEY"])

    def test_missing_keys_are_absent(self):
        self.assertEqual({"ABSENT"}, set(m.parse_presence(self.payload("ui"), "ui").values()))

    def test_malformed_response_unknown_not_absent(self):
        for raw in [b"{", b"null", b"[]", b"fake-secret", self.payload().replace(b'"properties": {}', b'"properties": null')]:
            self.assertEqual({"UNKNOWN"}, set(m.parse_presence(raw, "api").values()))

    def test_wrong_resource_identity_unknown(self):
        self.assertEqual({"UNKNOWN"}, set(m.parse_presence(self.payload("ui"), "api").values()))

    def test_duplicate_fields_and_oversize_unknown(self):
        self.assertEqual({"UNKNOWN"}, set(m.parse_presence(b'{"id":"one","id":"two"}', "api").values()))
        self.assertEqual({"UNKNOWN"}, set(m.parse_presence(b"x" * (m.LIMIT + 1), "api").values()))

    def test_incomplete_digest_unverified(self):
        self.assertEqual("UNVERIFIED", m.compare_digest("sha256:a06baf21", m.UI_LOCAL))

    def test_unknown_provenance_not_mismatch_of_digest_kinds(self):
        self.assertEqual("UNVERIFIED", m.compare_digest(m.UI_CONFIGURED))

    def test_known_platform_digest_comparison(self):
        self.assertEqual("MATCH", m.compare_digest(m.UI_CONFIGURED, m.UI_CONFIGURED))
        self.assertEqual("MISMATCH", m.compare_digest(m.UI_CONFIGURED, "sha256:" + "0" * 64))

    def test_index_child_mapping_exact_hashes(self):
        child = json.dumps({"schemaVersion": 2, "layers": []}).encode()
        digest = "sha256:" + hashlib.sha256(child).hexdigest()
        index = json.dumps({"manifests": [{"digest": digest, "platform": {"os": "linux", "architecture": "amd64"}}]}).encode()
        idx = "sha256:" + hashlib.sha256(index).hexdigest()
        self.assertNotEqual(idx, digest)
        self.assertEqual(digest, m.map_index_child(index, idx, child))
        self.assertEqual("UNVERIFIED", m.map_index_child(index, idx, child + b"\n"))

    def test_plan_only_no_network_or_cli(self):
        with patch("subprocess.Popen", side_effect=AssertionError("no child")), patch("socket.socket", side_effect=AssertionError("no network")), contextlib.redirect_stdout(io.StringIO()) as output:
            self.assertEqual(0, m.main([]))
        value = json.loads(output.getvalue())
        self.assertEqual(0, value["azureCalls"])
        self.assertFalse(value["liveAdapterImplemented"])
        self.assertEqual(2, len(value["proposedReads"]))

    def test_no_live_mode_or_target_override(self):
        with contextlib.redirect_stderr(io.StringIO()), self.assertRaises(SystemExit):
            m.main(["--execute"])
        with self.assertRaisesRegex(ValueError, "ROLE_NOT_ALLOWED"):
            m.parse_presence(b"{}", "production")


if __name__ == "__main__":
    unittest.main()
