"""Synthetic inspect fixtures only."""
import contextlib
import io
import importlib
import json
import unittest
from unittest.mock import patch
import wp65_local_image_metadata as m


def fixture():
    return {"Id": m.WP61, "Os": "linux", "Architecture": "amd64", "User": "12002:12002", "ExposedPorts": {"8501/tcp": {}},
            "Entrypoint": ["/opt/venv/bin/python", "-B", "-m", "streamlit", "run", "/app/python/presentation/realtime_financial_visualization.py", "--server.port=8501", "--server.address=0.0.0.0"], "Cmd": [], "RepoDigests": []}


class Tests(unittest.TestCase):
    def test_exact_id_command_and_no_rich_fields(self):
        command = m.command("desktop-linux")
        self.assertEqual(["docker", "--context", "desktop-linux", "image", "inspect", m.WP61], command[:6])
        for name in ["Env", "Labels", "History", "Mounts", "RootFS"]: self.assertNotIn(name, command[-1])
        with self.assertRaises(ValueError): m.command("unexpected-context")

    def test_matching_contract_leaves_digest_kinds_unknown(self):
        r = m.project(json.dumps(fixture()).encode())
        self.assertEqual("MATCH", r["result"])
        self.assertEqual("UNKNOWN", r["ociIndexDigest"])
        self.assertEqual("UNKNOWN", r["configDigest"])
        self.assertEqual("UNVERIFIED", r["publication"])

    def test_wrong_id_malformed_duplicate_and_cap(self):
        d = fixture(); d["Id"] = "sha256:" + "0" * 64
        for raw in [json.dumps(d).encode(), b"{", b"[]", b'{"Id":1,"Id":2}', b"x" * (m.LIMIT+1)]:
            self.assertEqual("UNVERIFIED", m.project(raw)["result"])

    def test_contract_mismatch_not_identity_equivalence(self):
        d = fixture(); d["User"] = "0"
        r = m.project(json.dumps(d).encode())
        self.assertEqual("MISMATCH", r["result"])
        self.assertEqual("UNKNOWN", r["runtimeManifestDigest"])

    def test_args_values_not_echoed(self):
        d = fixture(); d["Cmd"] = ["fake-sensitive-command-value"]
        r = json.dumps(m.project(json.dumps(d).encode()))
        self.assertNotIn("fake-sensitive", r)
        self.assertNotIn('"Entrypoint":', r)
        self.assertNotIn("realtime_financial_visualization.py", r)

    def test_safe_full_repodigest_and_unsafe_ref(self):
        d = fixture(); d["RepoDigests"] = ["ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:"+"1"*64]
        self.assertEqual(d["RepoDigests"], m.project(json.dumps(d).encode())["safeRepoDigests"])
        d["RepoDigests"] = ["https://fake-user:fake-secret@private.invalid/image"]
        r = m.project(json.dumps(d).encode()); self.assertEqual("UNVERIFIED", r["result"])
        self.assertNotIn("fake-secret", json.dumps(r))

    def test_default_no_daemon_or_network(self):
        with patch("subprocess.Popen", side_effect=AssertionError("no daemon")), patch("socket.socket", side_effect=AssertionError("no network")), contextlib.redirect_stdout(io.StringIO()) as output:
            self.assertEqual(0, m.main([]))
        self.assertEqual(0, json.loads(output.getvalue())["dockerOperations"])


class OfflineTemplateTests(unittest.TestCase):
    def test_parser_optional_and_wrong_types_fail_closed(self):
        for name, values in {"Cmd": [False, "", {}, 1], "Entrypoint": [None, False, ""], "User": [None, False, 12002], "Os": [None, 1], "Architecture": [None, []], "ExposedPorts": [None, [], ""], "RepoDigests": [None, {}, ""]}.items():
            for value in values:
                data=fixture();data[name]=value
                self.assertEqual("UNVERIFIED", m.project(json.dumps(data).encode())["result"])
        data=fixture();data["Cmd"]=None
        self.assertEqual("MATCH", m.project(json.dumps(data).encode())["result"])

    def test_flat_config_is_not_silently_assumed(self):
        data=fixture()
        for name in ["User", "ExposedPorts", "Entrypoint", "Cmd"]:data[name]=None
        self.assertEqual("UNVERIFIED", m.project(json.dumps(data).encode())["result"])

    def test_invalid_port_names_do_not_escape(self):
        for name in ["0/tcp", "99999/tcp", "fake-sensitive-path"]:
            data=fixture();data["ExposedPorts"]={name:{}}
            result=m.project(json.dumps(data).encode())
            self.assertEqual("UNVERIFIED", result["result"])
            self.assertNotIn("fake-sensitive", json.dumps(result))

    def test_minimal_projection_only_requests_identity_platform(self):
        self.assertEqual('{"Id":{{json .Id}},"Os":{{json .Os}},"Architecture":{{json .Architecture}}}', m.FORMAT)
        self.assertNotIn("Config", m.FORMAT)
        self.assertNotIn("RepoDigests", m.FORMAT)
        data = {"Id": m.WP61, "Os": "linux", "Architecture": "amd64"}
        result = m.project(json.dumps(data).encode())
        self.assertEqual("UNVERIFIED", result["result"])
        self.assertTrue(result["contract"]["linuxAmd64"])
        self.assertEqual("UNVERIFIED", result["contract"]["user12002"])
        self.assertEqual("UNKNOWN", result["ociIndexDigest"])

    def test_missing_listen_flags_unverified_not_mismatch(self):
        data = fixture(); data["Entrypoint"] = data["Entrypoint"][:6]
        result = m.project(json.dumps(data).encode())
        self.assertEqual("UNVERIFIED", result["result"])
        data = fixture(); data["Entrypoint"][6] = "--server.port=9999"
        self.assertEqual("MISMATCH", m.project(json.dumps(data).encode())["result"])

    def test_missing_identity_or_empty_object_unverified(self):
        for data in [{}, {"Id": m.WP61}, {"Id": None}]:
            self.assertEqual("UNVERIFIED", m.project(json.dumps(data).encode())["result"])


class RepoDigestTests(unittest.TestCase):
    def test_fixed_two_field_command(self):
        command = m.repodigest_command(m.CONTEXT)
        self.assertEqual(["docker", "--context", "desktop-linux", "image", "inspect", m.WP61], command[:6])
        self.assertEqual('{"Id":{{json .Id}},"RepoDigests":{{json .RepoDigests}}}', command[-1])
        self.assertNotIn("Config", command[-1])
        with self.assertRaises(ValueError): m.repodigest_command("other")

    def test_null_empty_safe_reference_distinct(self):
        reference = "ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:" + "a" * 64
        for refs, expected in [(None, "NULL"), ([], "EMPTY"), ([reference], "SAFE_REFS")]:
            value = m.project_repodigests(json.dumps({"Id": m.WP61, "RepoDigests": refs}).encode())
            self.assertEqual("OBSERVED", value["result"])
            self.assertEqual(expected, value["repoDigestState"])
            self.assertEqual("UNVERIFIED", value["ociProvenance"])

    def test_untrusted_ref_never_disclosed_or_partially_returned(self):
        for refs in [["fake-sensitive-secret"], [None], ["ghcr.io/user/repo@sha256:" + "a" * 64], ["https://user:fake-sensitive-secret@host/repo"]]:
            value = m.project_repodigests(json.dumps({"Id": m.WP61, "RepoDigests": refs}).encode())
            self.assertEqual("UNVERIFIED", value["result"])
            self.assertEqual("UNSAFE_REFERENCE_WITHHELD", value["repoDigestState"])
            self.assertNotIn("fake-sensitive", json.dumps(value))
            self.assertNotIn("safeRepoDigests", value)

    def test_missing_wrong_id_schema_duplicates_caps(self):
        for raw in [b"{}", b"{", b"[]", b'{"Id":1,"Id":2}', json.dumps({"Id": "wrong", "RepoDigests": []}).encode(), json.dumps({"Id": m.WP61}).encode(), b"x" * (m.LIMIT + 1)]:
            self.assertEqual("UNVERIFIED", m.project_repodigests(raw)["result"])
        value = m.project_repodigests(json.dumps({"Id": m.WP61, "RepoDigests": ["x"] * 17}).encode())
        self.assertEqual("COUNT_LIMIT_STOP", value["repoDigestState"])


class StructuralCategoryTests(unittest.TestCase):
    expected = "ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:" + "a" * 64
    other = "registry.example.org/team/fake-image@sha256:" + "b" * 64

    def project(self, refs):
        return m.project_repodigests(json.dumps({"Id": m.WP61, "RepoDigests": refs}).encode())

    def test_expected_and_other_are_syntax_only(self):
        for ref, category in [(self.expected, "EXPECTED_REPOSITORY_DIGEST"), (self.other, "OTHER_REPOSITORY_DIGEST")]:
            value = self.project([ref])
            self.assertEqual(1, value["referenceCategories"][category])
            self.assertEqual("UNVERIFIED", value["ociProvenance"])
            self.assertFalse(value["PUBLIC_RUNTIME_VERIFIED"])
            self.assertFalse(value["liveAttemptReady"])
            if ref == self.expected:
                self.assertEqual([ref], value["safeRepoDigests"])
            else:
                self.assertEqual("UNVERIFIED", value["result"])
                self.assertNotIn(ref, json.dumps(value))

    def test_mixed_collection_withholds_every_reference(self):
        value = self.project([self.expected, self.other, "fake-sensitive", None])
        self.assertEqual({name: 1 for name in (
            "EXPECTED_REPOSITORY_DIGEST", "OTHER_REPOSITORY_DIGEST",
            "MALFORMED_REFERENCE", "UNEXPECTED_VALUE_TYPE")}, value["referenceCategories"])
        self.assertEqual("UNVERIFIED", value["result"])
        self.assertNotIn("safeRepoDigests", value)
        rendered = json.dumps(value)
        for ref in [self.expected, self.other, "fake-sensitive"]:
            self.assertNotIn(ref, rendered)
        self.assertLess(len(rendered), 1024)

    def test_malformed_ambiguous_and_uppercase_withheld(self):
        for ref in ["https://" + self.other, "user:fake-sensitive@" + self.other,
                    self.other + "\n", " " + self.other, self.other.upper(),
                    self.other[:-1], self.other.replace("@sha256:", ":tag@sha256:"),
                    "registry/repo@sha256:" + "a" * 64,
                    "registry.example.org:443/repo@sha256:" + "a" * 64,
                    self.other.replace("/team/", "/../"), self.other.replace("/team/", "//"),
                    "registry.example.org/repo:tag", "x" * 2048]:
            with self.subTest(length=len(ref)):
                value = self.project([ref])
                self.assertEqual(1, value["referenceCategories"]["MALFORMED_REFERENCE"])
                self.assertNotIn(ref, json.dumps(value))
                self.assertNotIn("safeRepoDigests", value)

    def test_nonstring_values_and_collection_type(self):
        for ref in [None, True, 1, {}, [], {"fake-sensitive": "value"}]:
            value = self.project([ref])
            self.assertEqual(1, value["referenceCategories"]["UNEXPECTED_VALUE_TYPE"])
            self.assertNotIn("fake-sensitive", json.dumps(value))
        for refs in [True, 1, {}, "fake-sensitive"]:
            value = self.project(refs)
            self.assertEqual("UNKNOWN", value["repoDigestState"])
            self.assertNotIn("referenceCategories", value)

    def test_guards_precede_classification(self):
        for raw in [json.dumps({"Id": "wrong", "RepoDigests": [self.other]}).encode(),
                    ('{"Id":"' + m.WP61 + '","RepoDigests":[],"RepoDigests":[]}').encode(),
                    json.dumps({"Id": m.WP61}).encode(), b"x" * (m.LIMIT + 1)]:
            value = m.project_repodigests(raw)
            self.assertEqual("UNKNOWN", value["repoDigestState"])
            self.assertNotIn("referenceCategories", value)
        value = self.project([self.expected] * m.MAX_REPODIGESTS)
        self.assertEqual(m.MAX_REPODIGESTS, value["safeRepoDigestCount"])
        value = self.project([self.other] * (m.MAX_REPODIGESTS + 1))
        self.assertEqual("COUNT_LIMIT_STOP", value["repoDigestState"])
        self.assertNotIn("referenceCategories", value)

    def test_import_and_default_plan_have_no_external_operations(self):
        with patch("subprocess.Popen", side_effect=AssertionError("process forbidden")), patch("socket.socket", side_effect=AssertionError("network forbidden")), contextlib.redirect_stdout(io.StringIO()) as output:
            importlib.reload(m)
            self.assertEqual(0, m.main([]))
        self.assertEqual("PLAN_ONLY", json.loads(output.getvalue())["result"])


if __name__ == "__main__": unittest.main()
