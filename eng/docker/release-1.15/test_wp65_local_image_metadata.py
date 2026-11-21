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
        self.assertEqual({"EXPECTED_REPOSITORY_DIGEST": 1, "OTHER_REPOSITORY_DIGEST": 1,
                          "MALFORMED_REFERENCE": 1, "UNEXPECTED_VALUE_TYPE": 1,
                          "UNSUPPORTED_REFERENCE_SHAPE": 0, "UNSUPPORTED_DIGEST_SHAPE": 0}, value["referenceCategories"])
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


class UnsupportedShapeTests(unittest.TestCase):
    def project(self, refs):
        return m.project_repodigests(json.dumps({"Id": m.WP61, "RepoDigests": refs}).encode())

    def test_explicit_port_shapes_are_withheld_not_publication(self):
        for port in [1, 443, 5000, 65535]:
            ref = "registry.example.org:" + str(port) + "/team/fake-image@sha256:" + "a" * 64
            value = self.project([ref])
            self.assertEqual(1, value["referenceCategories"]["UNSUPPORTED_REFERENCE_SHAPE"])
            self.assertEqual("UNVERIFIED", value["result"])
            self.assertEqual("UNVERIFIED", value["ociProvenance"])
            self.assertFalse(value["PUBLIC_RUNTIME_VERIFIED"])
            self.assertFalse(value["liveAttemptReady"])
            self.assertNotIn(ref, json.dumps(value))
            self.assertNotIn("safeRepoDigests", value)

    def test_ports_with_ambiguous_or_invalid_forms_remain_malformed(self):
        for authority in ["registry.example.org:0", "registry.example.org:65536",
                          "registry.example.org:0443", "registry.example.org:+443",
                          "registry.example.org:", "localhost:5000", "[::1]:5000",
                          "user:fake-sensitive@registry.example.org:443"]:
            ref = authority + "/team/repo@sha256:" + "a" * 64
            value = self.project([ref])
            self.assertEqual(1, value["referenceCategories"]["MALFORMED_REFERENCE"])
            self.assertNotIn(ref, json.dumps(value))
        base = "registry.example.org:443/team/repo@sha256:" + "a" * 64
        for ref in ["https://" + base, base + "\n", base.upper(), base[:-1],
                    base.replace("/team/", "//"), base.replace("@sha256:", ":tag@sha256:")]:
            self.assertEqual("MALFORMED_REFERENCE", m.reference_category(ref))

    def test_mixed_count_bounds_and_no_partial_values(self):
        expected = StructuralCategoryTests.expected
        refs = [expected, StructuralCategoryTests.other,
                "registry.example.org:5000/repo@sha256:" + "b" * 64,
                "fake-sensitive", None]
        value = self.project(refs)
        self.assertEqual(5, sum(value["referenceCategories"].values()))
        self.assertEqual(0, value["referenceCategories"]["UNSUPPORTED_DIGEST_SHAPE"])
        self.assertEqual(5, sum(count == 1 for count in value["referenceCategories"].values()))
        self.assertNotIn("safeRepoDigests", value)
        for ref in refs[:-1]: self.assertNotIn(ref, json.dumps(value))
        self.assertLess(len(json.dumps(value)), 1024)
        for count in [0, 1, m.MAX_REPODIGESTS]:
            value = self.project([refs[2]] * count)
            self.assertEqual(count, sum(value["referenceCategories"].values()))
        value = self.project([refs[2]] * (m.MAX_REPODIGESTS + 1))
        self.assertEqual("COUNT_LIMIT_STOP", value["repoDigestState"])
        self.assertNotIn("referenceCategories", value)


class StructuralMatrixTests(unittest.TestCase):
    # Synthetic candidates only; labels are not emitted by the production helper.
    digest = "@sha256:" + "c" * 64
    matrix = (
        ("localhost", "localhost/team/repo", "MALFORMED_REFERENCE"),
        ("single_label", "registry/team/repo", "MALFORMED_REFERENCE"),
        ("localhost_port", "localhost:5000/team/repo", "MALFORMED_REFERENCE"),
        ("ipv6", "[2001:db8::1]/team/repo", "MALFORMED_REFERENCE"),
        ("ipv6_port", "[2001:db8::1]:5000/team/repo", "MALFORMED_REFERENCE"),
        ("tag_plus_digest", "registry.example.org/team/repo:fake-tag", "MALFORMED_REFERENCE"),
        ("repeated_slash", "registry.example.org/team//repo", "MALFORMED_REFERENCE"),
        ("double_underscore", "registry.example.org/team/fake__repo", "MALFORMED_REFERENCE"),
        ("double_hyphen", "registry.example.org/team/fake--repo", "MALFORMED_REFERENCE"),
        ("uppercase_host", "REGISTRY.example.org/team/repo", "MALFORMED_REFERENCE"),
        ("uppercase_path", "registry.example.org/Team/repo", "MALFORMED_REFERENCE"),
        ("canonical_port", "registry.example.org:5000/team/repo", "UNSUPPORTED_REFERENCE_SHAPE"),
        ("leading_zero_port", "registry.example.org:05000/team/repo", "MALFORMED_REFERENCE"),
        ("scheme", "https://registry.example.org/team/repo", "MALFORMED_REFERENCE"),
        ("userinfo", "fake-user:fake-sensitive@registry.example.org/team/repo", "MALFORMED_REFERENCE"),
        ("other_repository", "registry.example.org/team/repo", "OTHER_REPOSITORY_DIGEST"),
    )

    def project(self, refs):
        return m.project_repodigests(json.dumps({"Id": m.WP61, "RepoDigests": refs}).encode())

    def test_candidate_matrix_retains_conservative_categories(self):
        for label, prefix, category in self.matrix:
            with self.subTest(case=label):
                ref = prefix + self.digest
                value = self.project([ref])
                self.assertEqual(category, m.reference_category(ref))
                self.assertEqual(1, value["referenceCategories"][category])
                self.assertEqual(1, sum(value["referenceCategories"].values()))
                self.assertEqual("UNVERIFIED", value["result"])
                self.assertNotIn(ref, json.dumps(value))
                self.assertNotIn("safeRepoDigests", value)

    def test_matrix_mixed_with_expected_withholds_all_values(self):
        expected = StructuralCategoryTests.expected
        for label, prefix, category in self.matrix:
            with self.subTest(case=label):
                ref = prefix + self.digest
                value = self.project([expected, ref])
                self.assertEqual(1, value["referenceCategories"]["EXPECTED_REPOSITORY_DIGEST"])
                self.assertEqual(1, value["referenceCategories"][category])
                self.assertNotIn(expected, json.dumps(value))
                self.assertNotIn(ref, json.dumps(value))
                self.assertNotIn("safeRepoDigests", value)

    def test_all_matrix_categories_bounded_and_cap_stops(self):
        refs = [prefix + self.digest for _, prefix, _ in self.matrix]
        self.assertEqual(m.MAX_REPODIGESTS, len(refs))
        value = self.project(refs)
        self.assertEqual(len(refs), sum(value["referenceCategories"].values()))
        self.assertTrue(all(type(n) is int and 0 <= n <= m.MAX_REPODIGESTS
                            for n in value["referenceCategories"].values()))
        self.assertLess(len(json.dumps(value)), 1024)
        for ref in refs: self.assertNotIn(ref, json.dumps(value))
        value = self.project(refs + [StructuralCategoryTests.expected])
        self.assertEqual("COUNT_LIMIT_STOP", value["repoDigestState"])
        self.assertNotIn("referenceCategories", value)
        self.assertNotIn("safeRepoDigests", value)

    def test_equal_categories_do_not_emit_shape_position_length_or_hash(self):
        # Different synthetic lengths, characters and order produce identical output.
        short = "localhost/repo" + self.digest
        long = "https://fake-sensitive.invalid/" + "x" * 200 + self.digest
        self.assertEqual(self.project([short]), self.project([long]))
        self.assertEqual(self.project([short, long]), self.project([long, short]))
        value = self.project([short])
        self.assertEqual({"result", "repoDigestState", "ociProvenance",
                          "PUBLIC_RUNTIME_VERIFIED", "liveAttemptReady",
                          "exactIdMatch", "referenceCategories"}, set(value))
        self.assertEqual({"EXPECTED_REPOSITORY_DIGEST", "OTHER_REPOSITORY_DIGEST",
                          "UNSUPPORTED_REFERENCE_SHAPE", "MALFORMED_REFERENCE",
                          "UNEXPECTED_VALUE_TYPE", "UNSUPPORTED_DIGEST_SHAPE"}, set(value["referenceCategories"]))


class CanonicalReviewPolicyTests(unittest.TestCase):
    # distribution/reference v0.6.0 syntax is broader than local policy.
    # These assert local withholding, not execution of the upstream Go parser.
    def project(self, refs):
        return m.project_repodigests(json.dumps({"Id": m.WP61, "RepoDigests": refs}).encode())

    def test_upstream_recognizable_shapes_do_not_expand_local_emission(self):
        for prefix in ["localhost/team/repo", "registry/team/repo",
                       "[2001:db8::1]:5000/team/repo",
                       "registry.example.org/team/repo:fake-tag",
                       "registry.example.org/team/fake__repo",
                       "registry.example.org/team/fake--repo",
                       "REGISTRY.example.org/team/repo",
                       "registry.example.org:05000/team/repo"]:
            ref = prefix + "@sha256:" + "d" * 64
            value = self.project([StructuralCategoryTests.expected, ref])
            self.assertEqual("UNVERIFIED", value["result"])
            self.assertEqual(1, value["referenceCategories"]["MALFORMED_REFERENCE"])
            self.assertNotIn("safeRepoDigests", value)
            self.assertNotIn(ref, json.dumps(value))
            self.assertNotIn(StructuralCategoryTests.expected, json.dumps(value))

    def test_digest_regex_is_not_algorithm_validation_or_emission_policy(self):
        base = "ghcr.io/samuel-santos-engineer/aiquanttradingresearch@"
        for suffix in ["sha256:" + "A" * 64, "sha256:" + "a" * 32,
                       "sha256:" + "a" * 65, "sha512:" + "a" * 128,
                       "SHA256:" + "a" * 64, "fakealgo:" + "a" * 64]:
            ref = base + suffix
            value = self.project([ref])
            self.assertEqual("UNVERIFIED", value["result"])
            self.assertEqual("UNVERIFIED", value["ociProvenance"])
            self.assertEqual(1, sum(value["referenceCategories"].values()))
            self.assertNotIn("safeRepoDigests", value)
            self.assertNotIn(ref, json.dumps(value))


class DigestDiagnosticTests(unittest.TestCase):
    def project(self, refs):
        return m.project_repodigests(json.dumps({"Id": m.WP61, "RepoDigests": refs}).encode())

    def test_supported_subset_digest_boundaries(self):
        for algorithm, length in [("sha384", 96), ("sha512", 128)]:
            for name in ["registry.example.org/team/repo",
                         "ghcr.io/samuel-santos-engineer/aiquanttradingresearch"]:
                ref = name + "@" + algorithm + ":" + "a" * length
                self.assertEqual("UNSUPPORTED_DIGEST_SHAPE", m.reference_category(ref))
                for wrong in [length - 1, length + 1]:
                    self.assertEqual("MALFORMED_REFERENCE", m.reference_category(
                        name + "@" + algorithm + ":" + "a" * wrong))
                self.assertEqual("MALFORMED_REFERENCE", m.reference_category(
                    name + "@" + algorithm + ":" + "A" * length))

    def test_name_constraints_and_existing_precedence(self):
        for name in ["localhost/repo", "registry/repo", "[::1]/repo",
                     "registry.example.org:5000/repo", "registry.example.org/repo:tag",
                     "registry.example.org/team//repo", "REGISTRY.example.org/repo",
                     "https://registry.example.org/repo", "user:fake@registry.example.org/repo"]:
            self.assertEqual("MALFORMED_REFERENCE", m.reference_category(name + "@sha512:" + "a" * 128))
        samples = [(StructuralCategoryTests.expected, "EXPECTED_REPOSITORY_DIGEST"),
                   (StructuralCategoryTests.other, "OTHER_REPOSITORY_DIGEST"),
                   ("registry.example.org:5000/repo@sha256:" + "a" * 64, "UNSUPPORTED_REFERENCE_SHAPE"),
                   ("fake-sensitive", "MALFORMED_REFERENCE"), (None, "UNEXPECTED_VALUE_TYPE")]
        for ref, category in samples: self.assertEqual(category, m.reference_category(ref))

    def test_aggregate_only_mixed_withholding_and_noninterference(self):
        a = "registry.example.org/repo@sha384:" + "a" * 96
        b = "other.example.net/longer/fake-name@sha512:" + "b" * 128
        self.assertEqual(self.project([a]), self.project([b]))
        refs = [StructuralCategoryTests.expected, StructuralCategoryTests.other,
                "registry.example.org:5000/repo@sha256:" + "c" * 64,
                "fake-sensitive", None, a]
        value = self.project(refs)
        self.assertEqual(value, self.project(list(reversed(refs))))
        self.assertTrue(all(n == 1 for n in value["referenceCategories"].values()))
        self.assertEqual("UNVERIFIED", value["result"])
        self.assertEqual("UNVERIFIED", value["ociProvenance"])
        self.assertFalse(value["PUBLIC_RUNTIME_VERIFIED"])
        self.assertFalse(value["liveAttemptReady"])
        self.assertEqual({"result", "repoDigestState", "ociProvenance",
                          "PUBLIC_RUNTIME_VERIFIED", "liveAttemptReady",
                          "exactIdMatch", "referenceCategories"}, set(value))
        for ref in refs:
            if isinstance(ref, str): self.assertNotIn(ref, json.dumps(value))
        self.assertNotIn("safeRepoDigests", value)
        self.assertLess(len(json.dumps(value)), 1024)

    def test_count_and_raw_caps_and_guards_precede_diagnostic(self):
        ref = "registry.example.org/repo@sha512:" + "a" * 128
        value = self.project([ref] * m.MAX_REPODIGESTS)
        self.assertEqual(m.MAX_REPODIGESTS, value["referenceCategories"]["UNSUPPORTED_DIGEST_SHAPE"])
        value = self.project([ref] * (m.MAX_REPODIGESTS + 1))
        self.assertEqual("COUNT_LIMIT_STOP", value["repoDigestState"])
        self.assertNotIn("referenceCategories", value)
        raw = json.dumps({"Id": m.WP61, "RepoDigests": [ref]}).encode()
        self.assertEqual("UNSUPPORTED_DIGEST_SHAPE", m.reference_category(ref))
        value = m.project_repodigests(raw + b" " * (m.LIMIT - len(raw)))
        self.assertEqual(1, value["referenceCategories"]["UNSUPPORTED_DIGEST_SHAPE"])
        self.assertNotIn("referenceCategories", m.project_repodigests(raw + b" " * (m.LIMIT + 1 - len(raw))))
        for raw in [json.dumps({"Id": "wrong", "RepoDigests": [ref]}).encode(),
                    ('{"Id":"' + m.WP61 + '","RepoDigests":[],"RepoDigests":[]}').encode()]:
            self.assertNotIn("referenceCategories", m.project_repodigests(raw))


if __name__ == "__main__": unittest.main()
