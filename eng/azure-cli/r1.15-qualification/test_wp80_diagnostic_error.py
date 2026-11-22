"""Synthetic WP80 error-classification tests; no Azure adapter or real bodies."""
import contextlib
import datetime as dt
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

    def test_documented_string_innererror_is_opaque(self):
        a = {"error": {"code": "BadRequest", "target": "startTime", "innererror": "fake-private-A endTime"}}
        b = {"error": {"code": "BadRequest", "target": "startTime", "innererror": "fake-private-B api-version"}}
        result = m.classify(400, encode(a))
        self.assertEqual("STRUCTURED_ERROR", result["responseClass"])
        self.assertEqual(["BAD_REQUEST"], result["serverCategories"])
        self.assertEqual(["START_TIME"], result["parameterHints"])
        self.assertEqual(result, m.classify(400, encode(b)))
        for invalid in (None, [], 42, True):
            value = {"code": "BadRequest", "innererror": invalid}
            self.assertEqual("UNSUPPORTED_SCHEMA", m.classify(400, encode(value))["responseClass"])

    def test_long_message_not_scanned(self):
        r = m.classify(400, encode({"code": "BadRequest", "message": "startTime " + "x" * 8192}))
        self.assertEqual([], r["parameterHints"])


class LogTailTests(unittest.TestCase):
    start = dt.datetime(2026, 1, 1, tzinfo=dt.timezone.utc)
    end = start + dt.timedelta(minutes=15)

    def run_tail(self, body, status=200, media="application/octet-stream", start=None, end=None):
        return m.sanitize_log_tail(status, media, body, start or self.start, end or self.end)

    def test_recent_fixed_mentions_and_no_acceptance(self):
        body = b"2026-01-01T00:05:00Z image pull failed\n2026-01-01T00:06:00Z Process exited with exit code 1"
        r = self.run_tail(body)
        self.assertEqual(["EXIT_MENTION", "PULL_FAILURE_MENTION"], r["eventCategories"])
        self.assertEqual("HAS_IN_WINDOW_RECORD", r["freshness"])
        for key in ("runtimeImageIdentity", "mainProcessLiveness", "listener"):
            self.assertEqual("UNVERIFIED", r[key])
        self.assertFalse(r["causeConfirmed"])
        self.assertFalse(r["retryAuthorized"])

    def test_exact_application_markers(self):
        r = self.run_tail(b"2026-01-01T00:05:00Z Research snapshot listener started.")
        self.assertEqual(["APP_LISTENER_MARKER"], r["eventCategories"])
        r = self.run_tail(b"2026-01-01T00:05:00Z fake Research snapshot listener started.")
        self.assertEqual([], r["eventCategories"])

    def test_timeout_and_ping_mentions(self):
        r = self.run_tail(b"2026-01-01T00:05:00Z startup timed out; did not respond to HTTP pings")
        self.assertEqual(["PORT_READINESS_FAILURE_MENTION", "STARTUP_TIMEOUT_MENTION"], r["eventCategories"])

    def test_stale_future_and_undated_records_do_not_count(self):
        for body in (b"2025-12-31T23:59:59Z image pull failed", b"2026-01-01T00:15:01Z image pull failed", b"image pull failed"):
            r = self.run_tail(body)
            self.assertEqual([], r["eventCategories"])
            self.assertNotEqual("HAS_IN_WINDOW_RECORD", r["freshness"])

    def test_invalid_timestamp_and_window(self):
        self.assertEqual("UNKNOWN", self.run_tail(b"2026-99-99T00:05:00Z image pull failed")["freshness"])
        for start, end in [(self.start, self.start), (self.end, self.start),
                           (self.start, self.end + dt.timedelta(seconds=1)),
                           (self.start.replace(tzinfo=None), self.end)]:
            self.assertEqual("INVALID_WINDOW", self.run_tail(b"", start=start, end=end)["responseClass"])

    def test_no_content_is_not_healthy(self):
        r = self.run_tail(b"", status=204)
        self.assertEqual("NO_CONTENT", r["responseClass"])
        self.assertEqual("UNKNOWN", r["freshness"])
        self.assertEqual("UNSUPPORTED_SCHEMA", self.run_tail(b"fake", status=204)["responseClass"])

    def test_json_string_only_no_invented_object_schema(self):
        line = "2026-01-01T00:05:00Z image pull failed"
        self.assertEqual(self.run_tail(line.encode()), self.run_tail(encode(line), media="application/json"))
        for value in ({"logs": line}, [line], 123):
            self.assertEqual("UNSUPPORTED_SCHEMA", self.run_tail(encode(value), media="application/json")["responseClass"])

    def test_media_status_and_body_type_fail_closed(self):
        for media in ("text/html", "application/zip", "text/plain", None):
            self.assertEqual("UNSUPPORTED_MEDIA", self.run_tail(b"fake", media=media)["responseClass"])
        for status in (True, 201, 302, 400, 503):
            self.assertEqual("UNEXPECTED_STATUS", self.run_tail(b"", status=status)["responseClass"])
        self.assertEqual("INVALID_BODY_TYPE", self.run_tail("fake")["responseClass"])

    def test_invalid_encoding_archive_and_controls(self):
        for body in (b"\xff", b"PK\x03\x04fake", b"fake\x00secret", b"\x1b[31msecret"):
            self.assertEqual("UNSUPPORTED_ENCODING", self.run_tail(body)["responseClass"])

    def test_byte_line_and_record_caps(self):
        self.assertEqual("BODY_CAP_EXCEEDED", self.run_tail(b"x" * (m.MAX_BYTES + 1))["responseClass"])
        self.assertEqual("BOUNDED_TAIL", self.run_tail(b"x" * 2048)["responseClass"])
        self.assertEqual("RECORD_CAP_EXCEEDED", self.run_tail(b"x" * 2049)["responseClass"])
        self.assertEqual("BOUNDED_TAIL", self.run_tail(b"x\n" * 256)["responseClass"])
        self.assertEqual("RECORD_CAP_EXCEEDED", self.run_tail(b"x\n" * 257)["responseClass"])

    def test_noninterference_order_and_private_values(self):
        a = b"2026-01-01T00:05:00Z fake-private-A process exited\n2026-01-01T00:06:00Z fake-unrelated-A"
        b = b"2026-01-01T00:06:00Z fake-unrelated-B\n2026-01-01T00:05:00Z fake-private-B process exited"
        self.assertEqual(self.run_tail(a), self.run_tail(b))
        self.assertNotIn("fake", json.dumps(self.run_tail(a)))

    def test_window_endpoints_and_exact_body_cap(self):
        for timestamp in (b"2026-01-01T00:00:00Z", b"2026-01-01T00:15:00Z"):
            self.assertEqual("HAS_IN_WINDOW_RECORD", self.run_tail(timestamp + b" process exited")["freshness"])
        body = (b"x" * 2047 + b"\n") * 32
        self.assertEqual(m.MAX_BYTES, len(body))
        self.assertEqual("BOUNDED_TAIL", self.run_tail(body)["responseClass"])

    def test_source_failure_stage_markers(self):
        for stage in ("CONFIGURATION", "STORAGE"):
            body = ("2026-01-01T00:05:00Z Research snapshot service unavailable. stage=" + stage).encode()
            self.assertEqual(["APP_" + stage + "_FAILURE_MARKER"], self.run_tail(body)["eventCategories"])

    def test_exact_output_keys_and_no_side_effects(self):
        out = io.StringIO()
        with patch("socket.socket", side_effect=AssertionError("network")), patch("subprocess.Popen", side_effect=AssertionError("process")), contextlib.redirect_stdout(out), contextlib.redirect_stderr(out):
            r = self.run_tail(b"2026-01-01T00:05:00Z image pull failed")
        self.assertEqual("", out.getvalue())
        self.assertEqual({"responseClass", "freshness", "eventCategories", "runtimeImageIdentity",
                          "mainProcessLiveness", "listener", "causeConfirmed", "retryAuthorized"}, set(r))


if __name__ == "__main__":
    unittest.main()
