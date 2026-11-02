"""Fake-only metadata triage; no Azure, secrets or process experiments."""
import importlib.util
import io
import json
from pathlib import Path
from contextlib import redirect_stdout
import unittest

PATH = Path(__file__).resolve().parents[3] / "eng/azure-cli/r1.15-qualification/wp34_runtime_diagnosis.py"
SPEC = importlib.util.spec_from_file_location("wp34", PATH)
wp34 = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(wp34)


class RuntimeDiagnosisTests(unittest.TestCase):
    def fixture(self):
        return {name: name != "sidecarConfigured" for name in wp34.FIELDS}

    def invoke(self, data, args=None):
        output = io.StringIO()
        with redirect_stdout(output):
            code = wp34.main(["--stdin"] if args is None else args, io.BytesIO(data))
        return code, json.loads(output.getvalue()), output.getvalue()

    def test_match_is_never_runtime_acceptance(self):
        result = wp34.diagnose(self.fixture())
        self.assertEqual(result["result"], "CURATED_METADATA_MATCH_RUNTIME_UNVERIFIED")
        for name in ("azureInspected", "healthVerified", "liveAttemptReady", "PUBLIC_RUNTIME_VERIFIED"):
            self.assertIs(result[name], False)
        self.assertIs(result["AUTHENTIC_DATA_NOT_IMPORTED"], True)

    def test_each_missing_or_false_fact_is_reported(self):
        for name in wp34.FIELDS:
            if name == "sidecarConfigured":
                continue
            for value in (False, None):
                with self.subTest(name=name, value=value):
                    data = self.fixture()
                    data[name] = value
                    self.assertTrue(wp34.diagnose(data)["findings"])

    def test_sidecar_requires_main_container_review(self):
        data = self.fixture()
        data.update(classicContainerConfigured=False, sidecarConfigured=True)
        self.assertIn("SIDECAR_MAIN_CONTAINER_REVIEW_REQUIRED", wp34.diagnose(data)["findings"])

    def test_unknown_mode_is_not_a_match(self):
        data = self.fixture()
        data["sidecarConfigured"] = None
        self.assertIn("HOSTING_MODE_UNVERIFIED", wp34.diagnose(data)["findings"])

    def test_contradictory_modes_rejected(self):
        data = self.fixture()
        data["sidecarConfigured"] = True
        with self.assertRaises(ValueError):
            wp34.diagnose(data)

    def test_closed_schema_and_strict_boolean_types(self):
        for data in ([], {}, dict(self.fixture(), extra=True)):
            with self.assertRaises(ValueError):
                wp34.diagnose(data)
        for value in (0, 1, "true", [], {}):
            data = self.fixture()
            data["siteRunning"] = value
            with self.assertRaises(ValueError):
                wp34.diagnose(data)

    def test_cli_valid_curated_input(self):
        code, result, _ = self.invoke(json.dumps(self.fixture()).encode())
        self.assertEqual(code, 0)
        self.assertFalse(result["healthVerified"])

    def test_invalid_input_is_fixed_and_never_echoed(self):
        canary = "FAKE_SECRET_MUST_NOT_BE_ECHOED"
        for raw in (b"", b"\xff", b"x" * 4097, b"[]", b'{"x":true,"x":false}',
                    json.dumps(dict(self.fixture(), secret=canary)).encode(),
                    b"[" * 1500):
            code, result, output = self.invoke(raw)
            self.assertEqual(code, 2)
            self.assertEqual(result["result"], "INVALID_OR_MISSING_CURATED_METADATA")
            self.assertNotIn(canary, output)

    def test_no_default_input_read(self):
        class Forbidden:
            def read(self, size):
                raise AssertionError("unexpected read")
        output = io.StringIO()
        with redirect_stdout(output):
            self.assertEqual(wp34.main([], Forbidden()), 2)


if __name__ == "__main__":
    unittest.main()
