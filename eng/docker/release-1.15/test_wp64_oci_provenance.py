"""Synthetic OCI bytes and receipts only; no network, daemon, images or private paths."""
import contextlib
import copy
import io
import json
import unittest
from unittest.mock import patch
import wp64_oci_provenance as m


def raw(value): return json.dumps(value, sort_keys=True).encode()


def fixture(index=True, mutate=None):
    config = {"architecture": "amd64", "os": "linux", "config": {"User": "12002:12002", "ExposedPorts": {"8501/tcp": {}},
              "Entrypoint": ["/opt/venv/bin/python", "-B", "-m", "streamlit", "run", "/app/python/presentation/realtime_financial_visualization.py", "--server.port=8501", "--server.address=0.0.0.0"],
              "Env": ["AIQ_RESEARCH_CHART=1", "FAKE_SECRET=synthetic-do-not-echo"], "Labels": {"private-fake-label": "secret-label-value"}}}
    if mutate: mutate(config)
    c = raw(config); cd = m.hashed(c)
    manifest = {"schemaVersion": 2, "mediaType": "application/vnd.oci.image.manifest.v1+json", "layers": [],
                "config": {"mediaType": "application/vnd.oci.image.config.v1+json", "digest": cd, "size": len(c)}}
    child = raw(manifest); md = m.hashed(child); blobs = {cd: c, md: child}; root = md
    if index:
        i = raw({"schemaVersion": 2, "mediaType": "application/vnd.oci.image.index.v1+json", "manifests": [
            {"mediaType": manifest["mediaType"], "digest": md, "size": len(child), "platform": {"os": "linux", "architecture": "amd64"}}]})
        root = m.hashed(i); blobs[root] = i
    receipt = {"repository": m.REPO, "rootDigest": root, "runtimeDigest": md, "configDigest": cd, "evidenceKind": "RECORDED_REGISTRY_READBACK"}
    return root, blobs, cd, receipt


class Tests(unittest.TestCase):
    def test_index_child_config_and_receipt_match(self):
        root, blobs, local, receipt = fixture()
        value = m.verify(root, blobs, local, receipt)
        self.assertEqual("MATCH", value["status"])
        self.assertEqual("CONFIG", value["localIdKind"])
        self.assertEqual(m.REPO + "@" + receipt["runtimeDigest"], value["deployableReference"])
        self.assertFalse(value["layerContentVerified"])

    def test_direct_manifest(self):
        root, blobs, local, receipt = fixture(False)
        value = m.verify(root, blobs, local, receipt)
        self.assertEqual("MATCH", value["status"])
        self.assertEqual("RUNTIME_MANIFEST", value["rootKind"])

    def test_local_id_can_be_verified_index_not_assumed(self):
        root, blobs, _, receipt = fixture()
        value = m.verify(root, blobs, root, receipt)
        self.assertEqual("MATCH", value["status"])
        self.assertEqual("INDEX", value["localIdKind"])

    def test_misleading_local_id_is_unverified(self):
        root, blobs, _, receipt = fixture()
        value = m.verify(root, blobs, m.WP61, receipt)
        self.assertEqual("UNVERIFIED", value["status"])
        self.assertEqual("LOCAL_ID_NOT_MAPPED", value["reason"])

    def test_ambiguous_platform(self):
        root, blobs, local, receipt = fixture()
        index = json.loads(blobs[root]); index["manifests"] *= 2
        data = raw(index); root = m.hashed(data); blobs[root] = data
        self.assertEqual("PLATFORM_AMBIGUOUS", m.verify(root, blobs, local, receipt)["reason"])

    def test_tampered_root_child_and_config(self):
        for which in ["rootDigest", "runtimeDigest", "configDigest"]:
            root, blobs, local, receipt = fixture(); key = receipt[which]; blobs[key] += b" "
            self.assertEqual("UNVERIFIED", m.verify(root, blobs, local, receipt)["status"])

    def test_wrong_config_platform_user_exposed_port(self):
        for field, value in [("architecture", "arm64"), ("os", "windows"), ("User", "0"), ("ExposedPorts", {"9000/tcp": {}})]:
            def mutate(c):
                target = c if field in {"architecture", "os"} else c["config"]
                target[field] = value
            root, blobs, local, receipt = fixture(mutate=mutate)
            self.assertEqual("MISMATCH", m.verify(root, blobs, local, receipt)["status"])

    def test_wrong_listening_port_and_entrypoint(self):
        for args in [["--server.port=9000", "--server.address=0.0.0.0"], ["--server.port=8501", "--server.address=127.0.0.1"]]:
            root, blobs, local, receipt = fixture(mutate=lambda c: c["config"]["Entrypoint"].__setitem__(slice(6, None), args))
            self.assertEqual("MISMATCH", m.verify(root, blobs, local, receipt)["status"])
        root, blobs, local, receipt = fixture(mutate=lambda c: c["config"].__setitem__("Entrypoint", ["untrusted-script"]))
        self.assertEqual("MISMATCH", m.verify(root, blobs, local, receipt)["status"])

    def test_duplicate_port_evidence_unverified(self):
        root, blobs, local, receipt = fixture(mutate=lambda c: c["config"]["Entrypoint"].append("--server.port=9000"))
        self.assertEqual("UNVERIFIED", m.verify(root, blobs, local, receipt)["status"])

    def test_absent_port_evidence_unverified(self):
        root, blobs, local, receipt = fixture(mutate=lambda c: c["config"]["Entrypoint"].pop(6))
        self.assertEqual("UNVERIFIED", m.verify(root, blobs, local, receipt)["status"])

    def test_research_mode_and_optional_source_evidence(self):
        root, blobs, local, receipt = fixture(mutate=lambda c: c["config"].__setitem__("Env", []))
        self.assertEqual("UNVERIFIED", m.verify(root, blobs, local, receipt)["status"])
        root, blobs, local, receipt = fixture(mutate=lambda c: c["config"]["Labels"].__setitem__("org.opencontainers.image.revision", "private-invalid-source"))
        self.assertEqual("MISMATCH", m.verify(root, blobs, local, receipt)["status"])
        root, blobs, local, receipt = fixture()
        self.assertEqual("UNKNOWN", m.verify(root, blobs, local, receipt)["sourceRevision"])

    def test_incomplete_receipt_not_nonpublication(self):
        root, blobs, local, _ = fixture()
        for receipt in [None, {}, {"repository": m.REPO}]:
            value = m.verify(root, blobs, local, receipt)
            self.assertEqual("UNVERIFIED", value["status"])
            self.assertEqual("UNVERIFIED", value["publication"])
            self.assertIsNone(value["deployableReference"])

    def test_conflicting_receipt_mismatch(self):
        root, blobs, local, receipt = fixture(); receipt["runtimeDigest"] = "sha256:" + "0" * 64
        self.assertEqual("MISMATCH", m.verify(root, blobs, local, receipt)["status"])

    def test_malformed_duplicate_oversize_and_media_type(self):
        for data in [b"{", b'{"schemaVersion":2,"schemaVersion":2}', b"x" * (m.LIMIT + 1), raw({"schemaVersion": 2, "mediaType": "unknown"})]:
            root = m.hashed(data)
            self.assertEqual("UNVERIFIED", m.verify(root, {root: data})["status"])

    def test_descriptor_size_mismatch(self):
        root, blobs, local, receipt = fixture(False)
        doc = json.loads(blobs[root]); doc["config"]["size"] += 1
        data = raw(doc); root = m.hashed(data); blobs[root] = data
        self.assertEqual("DESCRIPTOR_SIZE", m.verify(root, blobs, local, receipt)["reason"])

    def test_redaction(self):
        root, blobs, local, receipt = fixture(); text = json.dumps(m.verify(root, blobs, local, receipt))
        for secret in ["synthetic-do-not-echo", "secret-label-value", "private-fake-label", "FAKE_SECRET", "Entrypoint"]:
            self.assertNotIn(secret, text)

    def test_default_no_network_docker_or_process(self):
        with patch("socket.socket", side_effect=AssertionError("no network")), patch("subprocess.Popen", side_effect=AssertionError("no Docker/process")), contextlib.redirect_stdout(io.StringIO()) as output:
            self.assertEqual(0, m.main([]))
        self.assertIsNone(json.loads(output.getvalue())["deployableReference"])

    def test_no_execute_mode(self):
        with contextlib.redirect_stderr(io.StringIO()), self.assertRaises(SystemExit): m.main(["--execute"])


if __name__ == "__main__":
    unittest.main()
