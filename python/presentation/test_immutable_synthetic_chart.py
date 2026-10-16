import hashlib
import json
import os
import shutil
from pathlib import Path
import tempfile
import unittest
from unittest.mock import patch

import immutable_synthetic_chart as reader
from historical_market_bridge import BridgeError


def encode(value):
    return (json.dumps(value, separators=(",", ":")) + "\n").encode()


class ImmutableChartTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory(prefix="aiq-reader-test-")
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name) / "candidate"
        self.root.mkdir(mode=0o700)
        self.manifest = dict(schema="aiq-chart-handoff-candidate-v1", generatorVersion="aiq-synthetic-ohlcv-v1", seed=114, files=[])
        # Synthetic structure-only inputs. parse is mocked ONLY for filesystem/manifest tests;
        # independent full C# artifact integration is a separate explicit offline check.
        for name in reader.NAMES:
            raw = encode({"candles": []})
            (self.root / name).write_bytes(raw)
            self.manifest["files"].append(dict(name=name, bytes=len(raw), sha256=hashlib.sha256(raw).hexdigest()))
        self.pin = self.repin()

    def repin(self):
        raw = encode(self.manifest)
        (self.root / "manifest.json").write_bytes(raw)
        return hashlib.sha256(raw).hexdigest()

    def load(self, pin=None):
        return reader._load_candidate(self.root, self.pin if pin is None else pin, "1h", "1D")

    def test_complete_inventory_and_selection_uses_only_supplied_bytes(self):
        with patch.object(reader, "parse", side_effect=lambda raw, i, r: (i, r)) as parse:
            self.assertEqual(self.load(), ("1h", "1D"))
            self.assertEqual(parse.call_count, 12)

    def test_gate_is_loopback_development_only_and_rejects_azure_and_conflicting_modes(self):
        env = dict(DOTNET_ENVIRONMENT="Development", AIQ_IMMUTABLE_CHART_CANDIDATE="1")
        self.assertTrue(reader.enabled("127.0.0.1", environment=env))
        for address, changed in [("0.0.0.0", env), ("127.0.0.1", {}), ("127.0.0.1", dict(env, WEBSITE_INSTANCE_ID="x")),
                                 ("127.0.0.1", dict(env, AIQ_LOCAL_CHART="1")), ("127.0.0.1", dict(env, DOTNET_ENVIRONMENT="Production"))]:
            self.assertFalse(reader.enabled(address, environment=changed))
        for key in ("Vike__ApiKey", "TwelveData__ApiKey", "Persistence__DatabasePath", "Worker__Replay__Target", "Visualization__HandoffPath"):
            self.assertFalse(reader.enabled("127.0.0.1", environment=dict(env, **{key: "not-a-credential"})))

    def test_image_qualification_is_explicit_and_rejects_inherited_markers(self):
        env = dict(AIQ_RUNTIME_MODE="local-candidate", AIQ_LOCAL_IMAGE_QUALIFICATION="1")
        with patch.object(reader.os, "name", "posix"):
            self.assertTrue(reader.enabled("0.0.0.0", environment=env))
            for extra in ({"WEBSITE_INSTANCE_ID": "test"}, {"DOTNET_ENVIRONMENT": "Production"},
                          {"ASPNETCORE_ENVIRONMENT": "Staging"}, {"ENVIRONMENT": "production"},
                          {"TwelveData__ApiKey": "not-a-credential"}, {"AIQ_LOCAL_CHART": "1"},
                          {"AIQ_IMMUTABLE_CHART_CANDIDATE": "1"}, {"AIQ_LOCAL_IMAGE_QUALIFICATION": "0"}):
                self.assertFalse(reader.enabled("0.0.0.0", environment=dict(env, **extra)))
            self.assertFalse(reader.enabled("127.0.0.1", environment=env))
            self.assertFalse(reader.enabled("0.0.0.0", environment={}))

    def test_image_asset_trust_rejects_runtime_ownership_and_owner_write(self):
        from types import SimpleNamespace
        fake = SimpleNamespace(st_uid=1000, st_mode=0o100444)
        with patch.object(Path, "lstat", return_value=fake):
            with self.assertRaisesRegex(ValueError, "ownership"):
                reader._check_path(self.root / "member", image_owned=True)
            fake.st_uid = 0
            reader._check_path(self.root / "member", image_owned=True)
            fake.st_mode = 0o100644
            with self.assertRaisesRegex(ValueError, "ownership"):
                reader._check_path(self.root / "member", image_owned=True)

    def test_disabled_path_does_not_touch_files(self):
        with patch.object(reader, "enabled", return_value=False), patch.object(reader, "_load_candidate") as load:
            with self.assertRaises(BridgeError): reader.load("btc-like-simulation", "1h", "1D", server_address="127.0.0.1")
            load.assert_not_called()

    def test_missing_artifact_and_unknown_selection_fail_closed(self):
        with patch.object(reader, "enabled", return_value=True), patch.object(reader, "ROOT", self.root / "missing"):
            for i, r in [("1h", "1D"), ("../1h", "1D"), ("1h", "live")]:
                with self.assertRaisesRegex(BridgeError, "ImmutableCandidateUnavailable"):
                    reader.load("btc-like-simulation", i, r, server_address="127.0.0.1")

    def test_tampered_bytes_and_external_anchor_rejected(self):
        with self.assertRaises(ValueError): self.load("0" * 64)
        (self.root / reader.NAMES[0]).write_bytes(b"changed\n")
        with self.assertRaises(ValueError): self.load()

    def test_extra_missing_and_traversal_members_rejected(self):
        (self.root / "extra").write_bytes(b"x")
        with self.assertRaises(ValueError): self.load()
        (self.root / "extra").unlink()
        self.manifest["files"][0]["name"] = "../outside.json"
        with self.assertRaises(ValueError): self.load(self.repin())
        (self.root / reader.NAMES[0]).unlink()
        with self.assertRaises(ValueError): self.load()

    def test_oversized_manifest_member_and_wrong_scalar_types(self):
        (self.root / "manifest.json").write_bytes(b"x" * 8193)
        with self.assertRaises(ValueError): self.load()
        self.pin = self.repin()
        (self.root / reader.NAMES[0]).write_bytes(b"x" * 1048577)
        with self.assertRaises(ValueError): self.load()
        self.manifest["seed"] = True
        with self.assertRaises(ValueError): self.load(self.repin())

    def test_duplicate_extra_malformed_utf8_and_nonfinite_json(self):
        for raw in (b'{"seed":114,"seed":114}\n', b'{"x":NaN}\n', b'\xff\n', b'{}\r\n'):
            (self.root / "manifest.json").write_bytes(raw)
            with self.subTest(raw=raw), self.assertRaises(ValueError): self.load(hashlib.sha256(raw).hexdigest())
        self.manifest["unexpected"] = 1
        with self.assertRaises(ValueError): self.load(self.repin())

    def test_real_payload_validator_rejects_structure_only_rows(self):
        with self.assertRaises(BridgeError): self.load()

    def test_concurrent_acquisition_denied_without_queue(self):
        with patch.object(reader, "enabled", return_value=True):
            self.assertTrue(reader._LOCK.acquire(False))
            try:
                with self.assertRaises(BridgeError): reader.load("btc-like-simulation", "1h", "1D", server_address="127.0.0.1")
            finally: reader._LOCK.release()

    def test_link_or_unsafe_permissions_rejected(self):
        # Windows unprivileged symlink creation may be unavailable; reparse-bit rejection
        # is tested portably, and actual Linux permissions are exercised when on Linux.
        with patch.object(reader.stat, "S_ISLNK", return_value=True):
            with self.assertRaises(ValueError): self.load()
        if os.name == "posix":
            self.root.chmod(0o777)
            with self.assertRaises(ValueError): self.load()
            self.root.chmod(0o700)

    def test_budget_is_fail_closed(self):
        with patch.object(reader.time, "monotonic", side_effect=[0, 3]):
            with self.assertRaises(ValueError): self.load()


def qualify_generated_candidate(root):
    """Explicit offline integration proof against operator-generated C# files, no mock parse."""
    for name in reader.NAMES:
        interval, range_value = name[:-5].split("-")
        response = reader._load_candidate(root, reader.MANIFEST_SHA256, interval, range_value, image_owned=root == reader.ROOT)
        expected_count = int(range_value[:-1]) * 24 // {"1h": 1, "4h": 4, "1d": 24}[interval]
        assert len(response.candles) == len(response.analytics.rows) == expected_count
        assert all(row.sma20 is not None for row in response.analytics.rows)
        print(f"PASS {name}: {expected_count} aligned visible C# SMA rows")
    mutations = {
        "wrong-selection": lambda p: p.update(interval="4h"),
        "missing-row": lambda p: p["candles"].pop(),
        "wrong-time": lambda p: p["candles"][0].update(timeUtc="2026-09-30T00:01:00Z"),
        "negative-price": lambda p: p["candles"][0].update(open="-1.0000"),
        "invalid-ohlc": lambda p: p["candles"][0].update(high="1.0000"),
        "negative-volume": lambda p: p["candles"][0].update(volume="-1.000"),
        "null-sma": lambda p: p["candles"][0].update(sma20=None),
        "wrong-scale": lambda p: p["candles"][0].update(sma20="1.1"),
        "wrong-warmup": lambda p: p.update(warmup=249),
        "wrong-calculator": lambda p: p.update(calculationVersion="unknown"),
        "wrong-provenance": lambda p: p.update(datasetFingerprint="0" * 64),
        "extra-member": lambda p: p.update(unexpected=True),
    }
    for name, mutate in mutations.items():
        with tempfile.TemporaryDirectory(prefix="aiq-handoff-negative-") as temporary:
            copy = Path(temporary) / "candidate"
            shutil.copytree(root, copy)
            path = copy / "1h-1D.json"
            path.chmod(0o600)  # Only this owned fault-injection copy, never the immutable source.
            payload = json.loads(path.read_bytes())
            mutate(payload)
            raw = encode(payload)
            path.write_bytes(raw)
            manifest = json.loads((copy / "manifest.json").read_bytes())
            manifest["files"][0].update(bytes=len(raw), sha256=hashlib.sha256(raw).hexdigest())
            manifest_bytes = encode(manifest)
            (copy / "manifest.json").chmod(0o600)
            (copy / "manifest.json").write_bytes(manifest_bytes)
            # Test-only trust rebind reaches semantic checks; public load never accepts a pin.
            try:
                reader._load_candidate(copy, hashlib.sha256(manifest_bytes).hexdigest(), "1h", "1D")
            except (ValueError, BridgeError):
                print(f"PASS reject {name}")
            else:
                raise AssertionError(f"Accepted {name}")


if __name__ == "__main__":
    import sys
    if len(sys.argv) == 3 and sys.argv[1] == "--candidate":
        qualify_generated_candidate(Path(sys.argv[2]))
    else:
        unittest.main()
