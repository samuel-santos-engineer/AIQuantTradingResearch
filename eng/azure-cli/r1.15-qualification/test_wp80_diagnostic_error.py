"""Synthetic WP80 error-classification tests; no Azure adapter or real bodies."""
import contextlib
import importlib
import io
import json
import unittest
from unittest.mock import patch
import wp80_diagnostic_error as m


def encode(value):
    return json.dumps(value).encode("utf-8")


class Tests(unittest.TestCase):
    def test_bad_request_parameter_hints(self):
        result = m.classify(400, encode({"error": {"code": "BadRequest", "target": "startTime",
                                                  "message": "endTime must follow startTime"}}))
        self.assertEqual(["BAD_REQUEST"], result["serverCategories"])
        self.assertEqual(["END_TIME", "START_TIME"], result["parameterHints"])
        self.assertFalse(result["causeConfirmed"])
        self.assertFalse(result["retryAuthorized"])

    def test_known_nested_codes(self):
        r = m.classify(400, encode({"error": {"code": "BadRequest", "details": [
            {"code": "InvalidTimeRange"}], "innererror": {"code": "InvalidApiVersionParameter"}}}))
        self.assertEqual(["API_VERSION_REJECTED", "BAD_REQUEST", "TIME_RANGE_REJECTED"], r["serverCategories"])

    def test_fixed_access_service_and_throttle_categories(self):
        for code, status, category in [("Forbidden", 403, "ACCESS_DENIED"),
                                       ("ServiceUnavailable", 503, "SERVICE_FAILURE"),
                                       ("TooManyRequests", 429, "THROTTLED")]:
            self.assertEqual([category], m.classify(status, encode({"code": code}))["serverCategories"])

    def test_unknown_code_not_emitted(self):
        r = m.classify(400, encode({"error": {"code": "fake-private-value", "message": "fake-secret"}}))
        self.assertEqual("STRUCTURED_ERROR", r["responseClass"])
        self.assertEqual([], r["serverCategories"])
        self.assertNotIn("fake", json.dumps(r))

    def test_no_output_or_external_operations(self):
        out = io.StringIO()
        with patch("socket.socket", side_effect=AssertionError("network")), patch("subprocess.Popen", side_effect=AssertionError("process")), contextlib.redirect_stdout(out), contextlib.redirect_stderr(out):
            importlib.reload(m)
            m.classify(400, b'{"code":"BadRequest"}')
        self.assertEqual("", out.getvalue())

    def test_equal_output_for_different_private_values_and_order(self):
        a = {"error": {"code": "BadRequest", "message": "startTime fake-secret-A", "requestId": "fake-A"}}
        b = {"unrelated": "fake-B", "error": {"requestId": "fake-B", "message": "fake-secret-B startTime", "code": "BadRequest"}}
        self.assertEqual(m.classify(400, encode(a)), m.classify(400, encode(b)))

    def test_no_extra_output_keys(self):
        self.assertEqual({"responseClass", "serverCategories", "parameterHints", "causeConfirmed", "retryAuthorized"},
                         set(m.classify(400, b'{"code":"BadRequest"}')))

    def test_hint_boundaries_and_order(self):
        r = m.classify(400, encode({"code": "BadRequest", "message": "xstartTime startTimeSuffix xapi-version api-versionX"}))
        self.assertEqual([], r["parameterHints"])
        r = m.classify(400, encode({"code": "BadRequest", "message": "DetectorName timeGrain api-version"}))
        self.assertEqual(["API_VERSION", "DETECTOR", "TIME_GRAIN"], r["parameterHints"])

    def test_non_json_invalid_utf8_and_duplicate_keys(self):
        for body in (b'<html>fake-secret</html>', b'\xff', b'{"code":"BadRequest","code":"Forbidden"}', b'{"message":NaN}'):
            self.assertEqual("NONSTANDARD_BODY", m.classify(400, body)["responseClass"])

    def test_status_and_input_types_fail_closed(self):
        for status in (True, 200, 399, 600, "400"):
            self.assertEqual("INVALID_STATUS", m.classify(status, b'{}')["responseClass"])
        self.assertEqual("INVALID_BODY_TYPE", m.classify(400, "fake")["responseClass"])

    def test_size_cap_boundary(self):
        body = b'{"code":"BadRequest"}'
        self.assertEqual("STRUCTURED_ERROR", m.classify(400, body + b' ' * (m.MAX_BYTES - len(body)))["responseClass"])
        self.assertEqual("BODY_CAP_EXCEEDED", m.classify(400, b' ' * (m.MAX_BYTES + 1))["responseClass"])

    def test_schema_failures_discard_partial_hints(self):
        for value in ([], {"properties": {}}, {"error": None},
                      {"code": "BadRequest", "target": "startTime", "details": ["fake"]},
                      {"code": "BadRequest", "details": {}}):
            r = m.classify(400, encode(value))
            self.assertEqual("UNSUPPORTED_SCHEMA", r["responseClass"])
            self.assertEqual([], r["parameterHints"])
            self.assertEqual([], r["serverCategories"])

    def test_record_and_depth_caps(self):
        for count in (16, 17):
            r = m.classify(400, encode({"code": "BadRequest", "details": [{"code": "Forbidden"}] * count}))
            self.assertEqual("UNSUPPORTED_SCHEMA", r["responseClass"])
        node = {"code": "BadRequest"}
        for _ in range(4):
            node = {"code": "BadRequest", "innererror": node}
        self.assertEqual("UNSUPPORTED_SCHEMA", m.classify(400, encode(node))["responseClass"])

    def test_accepted_record_and_depth_boundaries(self):
        r = m.classify(400, encode({"code": "BadRequest", "details": [{"code": "Forbidden"}] * 15}))
        self.assertEqual("STRUCTURED_ERROR", r["responseClass"])
        node = {"code": "BadRequest"}
        for _ in range(3):
            node = {"code": "BadRequest", "innererror": node}
        self.assertEqual("STRUCTURED_ERROR", m.classify(400, encode(node))["responseClass"])

    def test_long_message_not_scanned(self):
        r = m.classify(400, encode({"code": "BadRequest", "message": "startTime " + "x" * 8192}))
        self.assertEqual([], r["parameterHints"])


if __name__ == "__main__":
    unittest.main()
