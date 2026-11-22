"""Synthetic WP80 error-classification tests; no Azure adapter or real bodies."""
import contextlib
import datetime as dt
import hashlib
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

    def test_legacy_generic_failure_is_not_a_stage_witness(self):
        legacy = b"2026-01-01T00:05:00Z Research snapshot service unavailable.\n"
        staged = b"2026-01-01T00:05:00Z Research snapshot service unavailable. stage=STORAGE\n"
        a, b = self.run_tail(legacy), self.run_tail(staged)
        self.assertEqual([], a["eventCategories"])
        self.assertEqual(["APP_STORAGE_FAILURE_MARKER"], b["eventCategories"])
        for result in (a, b):
            self.assertFalse(result["causeConfirmed"])
            self.assertEqual("UNVERIFIED", result["runtimeImageIdentity"])

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


class ScmMetadataTests(unittest.TestCase):
    def value(self, **changes):
        value = {"status": 200, "contentType": "text/plain", "transferEncoding": None,
                 "contentEncoding": None, "authChallengePresent": False,
                 "locationPresent": False, "upgradePresent": False, "connectionUpgrade": False}
        value.update(changes)
        return value

    def classify(self, **changes):
        return m.classify_scm_metadata(self.value(**changes))

    def test_candidate_is_never_capture_or_authentication_success(self):
        r = self.classify()
        self.assertEqual("HTTP_BODY_CANDIDATE_UNVERIFIED", r["responseClass"])
        self.assertEqual("UNVERIFIED", r["authentication"])
        for key in ("bodyReadAuthorized", "retryAuthorized", "logCaptureVerified"):
            self.assertFalse(r[key])

    def test_common_content_type_parameters(self):
        for content in (' Text/Plain; charset=utf-8 ', 'text/plain; charset="UTF-8"', 'text/plain; charset=utf-8; version=1'):
            self.assertEqual(self.classify(), self.classify(contentType=content))
        self.assertEqual("OCTET_STREAM", self.classify(contentType="application/octet-stream")["mediaClass"])

    def test_unsupported_200_media(self):
        for media, label in [("text/html", "HTML"), ("application/json", "JSON"), ("application/x-synthetic", "OTHER")]:
            r = self.classify(contentType=media)
            self.assertEqual("MEDIA_UNSUPPORTED", r["responseClass"])
            self.assertEqual(label, r["mediaClass"])

    def test_missing_or_empty_media(self):
        for value in (None, "", "   "):
            r = self.classify(contentType=value)
            self.assertEqual("MEDIA_MISSING_UNKNOWN", r["responseClass"])
            self.assertEqual("MISSING", r["mediaClass"])

    def test_sse_is_declared_not_established(self):
        r = self.classify(contentType="text/event-stream; charset=utf-8")
        self.assertEqual("SSE_UNSUPPORTED", r["responseClass"])
        self.assertFalse(r["bodyReadAuthorized"])

    def test_authentication_denials(self):
        self.assertEqual("CHALLENGE_PRESENT", self.classify(status=401, authChallengePresent=True)["authentication"])
        self.assertEqual("DENIED_NO_CHALLENGE", self.classify(status=401)["authentication"])
        self.assertEqual("ACCESS_DENIED", self.classify(status=403)["responseClass"])
        self.assertEqual("DENIED_REASON_UNKNOWN", self.classify(status=403)["authentication"])

    def test_redirects_never_followed(self):
        for status in (301, 302, 303, 307, 308):
            self.assertEqual("REDIRECT_BLOCKED", self.classify(status=status, locationPresent=True)["responseClass"])
            self.assertEqual("REDIRECT_WITHOUT_LOCATION", self.classify(status=status)["responseClass"])
            self.assertFalse(self.classify(status=status, locationPresent=True)["retryAuthorized"])

    def test_upgrade_indicators_and_contradictions(self):
        self.assertEqual("UPGRADE_UNSUPPORTED", self.classify(status=101, upgradePresent=True, connectionUpgrade=True)["responseClass"])
        for status, upgrade, connection in [(101, False, False), (101, True, False), (200, True, True), (200, False, True)]:
            self.assertEqual("CONTRADICTORY_UPGRADE", self.classify(status=status, upgradePresent=upgrade, connectionUpgrade=connection)["responseClass"])

    def test_200_with_auth_or_location_is_ambiguous(self):
        for changes in ({"authChallengePresent": True}, {"locationPresent": True}):
            self.assertEqual("AMBIGUOUS_AUTH_OR_LOCATION", self.classify(**changes)["responseClass"])

    def test_transfer_and_content_encoding(self):
        self.assertEqual("CHUNKED_DECLARED", self.classify(transferEncoding="Chunked")["transferClass"])
        self.assertEqual("IDENTITY_DECLARED", self.classify(transferEncoding="identity")["transferClass"])
        for changes in ({"transferEncoding": "gzip, chunked"}, {"contentEncoding": "gzip"}):
            self.assertEqual("UNSUPPORTED_ENCODING", self.classify(**changes)["responseClass"])

    def test_metadata_types_keys_and_caps(self):
        for value in (None, [], {}, self.value(extra="synthetic-only")):
            self.assertEqual("INVALID_METADATA", m.classify_scm_metadata(value)["responseClass"])
        for changes in ({"status": True}, {"status": "200"}, {"status": 600},
                        {"contentType": []}, {"contentType": "x" * 257},
                        {"transferEncoding": "x" * 65}, {"contentEncoding": "x" * 65},
                        {"upgradePresent": "websocket"}, {"authChallengePresent": None}):
            self.assertEqual("INVALID_METADATA", self.classify(**changes)["responseClass"])

    def test_size_boundaries(self):
        for field, cap in (("contentType", 256), ("transferEncoding", 64), ("contentEncoding", 64)):
            value = ("application/" + "x" * (cap - 12)) if field == "contentType" else "x" * cap
            self.assertNotEqual("INVALID_METADATA", self.classify(**{field: value})["responseClass"])
            self.assertEqual("INVALID_METADATA", self.classify(**{field: value + "x"})["responseClass"])

    def test_header_injection_and_malformed_media(self):
        for value in ("text/plain\r\nX-Test: synthetic", "text/plain\x00", "not-a-media", "text/plain/extra", "text/plain; charset", 'text/plain; charset="unterminated'):
            self.assertEqual("INVALID_METADATA", self.classify(contentType=value)["responseClass"])

    def test_no_arbitrary_values_and_equal_output(self):
        a = self.classify(contentType="application/x-synthetic-a")
        b = self.classify(contentType="application/x-synthetic-b")
        self.assertEqual(a, b)
        self.assertNotIn("synthetic", json.dumps(a))
        a = self.value();b = dict(reversed(list(a.items())))
        self.assertEqual(m.classify_scm_metadata(a), m.classify_scm_metadata(b))

    def test_no_network_process_file_or_output_side_effects(self):
        out = io.StringIO()
        with patch("socket.socket", side_effect=AssertionError("network")), patch("subprocess.Popen", side_effect=AssertionError("process")), patch("builtins.open", side_effect=AssertionError("file")), contextlib.redirect_stdout(out), contextlib.redirect_stderr(out):
            r = self.classify()
        self.assertEqual("", out.getvalue())
        self.assertEqual({"responseClass", "mediaClass", "transferClass", "authentication", "protocolClass",
                          "bodyReadAuthorized", "retryAuthorized", "logCaptureVerified"}, set(r))

    def test_other_statuses_never_accepted(self):
        for status in (100, 201, 204, 304, 400, 429, 500, 503):
            r = self.classify(status=status)
            self.assertIn(r["responseClass"], ("HTTP_ERROR", "UNSUPPORTED_STATUS"))
            self.assertFalse(r["logCaptureVerified"])


class SseTests(unittest.TestCase):
    start = dt.datetime(2026, 1, 1, tzinfo=dt.timezone.utc)
    end = start + dt.timedelta(minutes=15)
    line = b"2026-01-01T00:05:00Z process exited"

    def parse(self, body):
        return m.sanitize_sse(body, self.start, self.end)

    def test_source_style_raw_lines_are_not_sse_event_evidence(self):
        body = b"2026-01-01T00:05:00Z process exited\n"
        framed = self.parse(body)
        plain = m.sanitize_log_tail(200, "application/octet-stream", body, self.start, self.end)
        self.assertEqual("SSE_FRAMING_ONLY", framed["responseClass"])
        self.assertEqual([], framed["eventCategories"])
        self.assertEqual(["EXIT_MENTION"], plain["eventCategories"])
        for result in (framed, plain):
            self.assertFalse(result["causeConfirmed"])
            self.assertEqual("UNVERIFIED", result["runtimeImageIdentity"])

    def test_complete_event_fixed_categories(self):
        r = self.parse(b"data: " + self.line + b"\n\n")
        self.assertEqual("SSE_DATA_CLASSIFIED", r["responseClass"])
        self.assertEqual(["EXIT_MENTION"], r["eventCategories"])
        self.assertEqual("HAS_IN_WINDOW_RECORD", r["freshness"])
        for key in ("runtimeImageIdentity", "mainProcessLiveness", "listener"):
            self.assertEqual("UNVERIFIED", r[key])
        self.assertFalse(r["causeConfirmed"])
        self.assertFalse(r["retryAuthorized"])

    def test_multiline_data_join(self):
        r = self.parse(b"data:" + self.line + b"\ndata:2026-01-01T00:06:00Z image pull failed\n\n")
        self.assertEqual(["EXIT_MENTION", "PULL_FAILURE_MENTION"], r["eventCategories"])

    def test_ids_retry_event_names_comments_are_opaque(self):
        base = b"data:" + self.line + b"\n\n"
        a = b": heartbeat fake-private-A\nid:fake-id-A\nretry:1\nevent:fake-A\n" + base
        b = b": heartbeat fake-private-B\nid:fake-id-B\nretry:999999999999999\nevent:fake-B\n" + base
        self.assertEqual(self.parse(base), self.parse(a))
        self.assertEqual(self.parse(a), self.parse(b))
        self.assertNotIn("fake", json.dumps(self.parse(a)))

    def test_heartbeat_only_is_not_capture(self):
        r = self.parse(b": heartbeat\n\nid:fake-id\nretry:1000\n\n")
        self.assertEqual("SSE_FRAMING_ONLY", r["responseClass"])
        self.assertEqual("UNKNOWN", r["freshness"])
        self.assertEqual([], r["eventCategories"])

    def test_incomplete_event_not_dispatched(self):
        for suffix in (b"", b"\n"):
            r = self.parse(b"data:" + self.line + suffix)
            self.assertEqual("SSE_INCOMPLETE_EVENT", r["responseClass"])
            self.assertEqual([], r["eventCategories"])

    def test_complete_then_incomplete_withholds_all(self):
        r = self.parse(b"data:" + self.line + b"\n\ndata:" + self.line)
        self.assertEqual("SSE_INCOMPLETE_EVENT", r["responseClass"])
        self.assertEqual([], r["eventCategories"])

    def test_unknown_payload_is_not_invented_json_schema(self):
        for value in (b'{"message":"synthetic"}', b'<html>synthetic</html>', b'synthetic text', b''):
            r = self.parse(b"data:" + value + b"\n\n")
            self.assertEqual("SSE_UNSUPPORTED_PAYLOAD", r["responseClass"])
            self.assertEqual([], r["eventCategories"])

    def test_line_endings_and_utf8_bom(self):
        expected = self.parse(b"data:" + self.line + b"\n\n")
        for body in (b"data:" + self.line + b"\r\n\r\n", b"data:" + self.line + b"\r\r", b"\xef\xbb\xbfdata:" + self.line + b"\n\n"):
            self.assertEqual(expected, self.parse(body))

    def test_data_field_case_and_optional_space(self):
        self.assertEqual("SSE_FRAMING_ONLY", self.parse(b"Data:" + self.line + b"\n\n")["responseClass"])
        self.assertEqual(self.parse(b"data:" + self.line + b"\n\n"), self.parse(b"data: " + self.line + b"\n\n"))
        self.assertEqual("SSE_UNSUPPORTED_PAYLOAD", self.parse(b"data:  " + self.line + b"\n\n")["responseClass"])

    def test_stale_and_future_events_do_not_match(self):
        for stamp in (b"2025-12-31T23:59:59Z", b"2026-01-01T00:15:01Z"):
            r = self.parse(b"data:" + stamp + b" process exited\n\n")
            self.assertEqual("NO_IN_WINDOW_RECORD", r["freshness"])
            self.assertEqual([], r["eventCategories"])

    def test_total_line_and_record_caps(self):
        self.assertEqual("BODY_CAP_EXCEEDED", self.parse(b"x" * (m.MAX_BYTES + 1))["responseClass"])
        self.assertEqual("SSE_FRAMING_ONLY", self.parse(b":" + b"x" * 2047 + b"\n")["responseClass"])
        self.assertEqual("SSE_RECORD_CAP_EXCEEDED", self.parse(b":" + b"x" * 2048 + b"\n")["responseClass"])
        self.assertEqual("SSE_FRAMING_ONLY", self.parse(b":\n" * 512)["responseClass"])
        self.assertEqual("SSE_RECORD_CAP_EXCEEDED", self.parse(b":\n" * 513)["responseClass"])

    def test_event_count_boundaries(self):
        event = b"data:" + self.line + b"\n\n"
        self.assertEqual("SSE_DATA_CLASSIFIED", self.parse(event * 32)["responseClass"])
        r = self.parse(event * 33)
        self.assertEqual("SSE_EVENT_CAP_EXCEEDED", r["responseClass"])
        self.assertEqual([], r["eventCategories"])

    def test_event_size_cap(self):
        line = b"data:2026-01-01T00:05:00Z " + b"x" * 2000 + b"\n"
        self.assertEqual("SSE_EVENT_CAP_EXCEEDED", self.parse(line * 5 + b"\n")["responseClass"])

    def test_exact_total_and_event_byte_boundaries(self):
        body = (b":" + b"x" * 2046 + b"\n") * 32
        self.assertEqual(m.MAX_BYTES, len(body))
        self.assertEqual("SSE_FRAMING_ONLY", self.parse(body)["responseClass"])
        prefix = b"2026-01-01T00:05:00Z "
        records = [prefix + b"x" * (size - len(prefix)) for size in (1638, 1638, 1638, 1638, 1636)]
        self.assertEqual(8192, len(b"\n".join(records)))
        event = b"".join(b"data:" + record + b"\n" for record in records) + b"\n"
        self.assertEqual("SSE_DATA_CLASSIFIED", self.parse(event)["responseClass"])
        records[-1] += b"x"
        event = b"".join(b"data:" + record + b"\n" for record in records) + b"\n"
        self.assertEqual("SSE_EVENT_CAP_EXCEEDED", self.parse(event)["responseClass"])

    def test_later_unknown_payload_withholds_prior_markers(self):
        r = self.parse(b"data:" + self.line + b"\n\ndata:synthetic-unsupported\n\n")
        self.assertEqual("SSE_UNSUPPORTED_PAYLOAD", r["responseClass"])
        self.assertEqual([], r["eventCategories"])
        self.assertEqual("UNKNOWN", r["freshness"])

    def test_invalid_types_encoding_and_window(self):
        self.assertEqual("INVALID_BODY_TYPE", self.parse("synthetic")["responseClass"])
        for value in (b"\xff", b"data:fake\x00value\n\n", b"PK\x03\x04synthetic"):
            self.assertEqual("UNSUPPORTED_ENCODING", self.parse(value)["responseClass"])
        self.assertEqual("INVALID_WINDOW", m.sanitize_sse(b"", self.end, self.start)["responseClass"])
        self.assertEqual("SSE_EMPTY", self.parse(b"")["responseClass"])

    def test_equal_output_across_order_and_unknown_fields(self):
        a = b"data:" + self.line + b"\n\n"
        b = b"data:2026-01-01T00:06:00Z image pull failed\n\n"
        self.assertEqual(self.parse(a + b), self.parse(b"synthetic-ignored-field:value\n" + b + a))

    def test_exact_keys_and_no_network_process_file_output(self):
        out = io.StringIO()
        with patch("socket.socket", side_effect=AssertionError("network")), patch("subprocess.Popen", side_effect=AssertionError("process")), patch("builtins.open", side_effect=AssertionError("file")), contextlib.redirect_stdout(out), contextlib.redirect_stderr(out):
            r = self.parse(b"data:" + self.line + b"\n\n")
        self.assertEqual("", out.getvalue())
        self.assertEqual({"responseClass", "freshness", "eventCategories", "runtimeImageIdentity",
                          "mainProcessLiveness", "listener", "causeConfirmed", "retryAuthorized"}, set(r))


class SsePrefixTests(unittest.TestCase):
    start = dt.datetime(2026, 1, 1, tzinfo=dt.timezone.utc)
    end = start + dt.timedelta(minutes=15)
    event = b"data:2026-01-01T00:05:00Z process exited\n\n"

    def parse(self, body, **kwargs):
        return m.sanitize_sse_prefix(body, self.start, self.end, **kwargs)

    def test_first_complete_event_stops_acquisition(self):
        r = self.parse(self.event)
        self.assertTrue(r["captureStopRequired"])
        self.assertEqual("SSE_DATA_CLASSIFIED", r["responseClass"])
        self.assertEqual(["EXIT_MENTION"], r["eventCategories"])
        self.assertEqual("UNVERIFIED", r["listener"])

    def test_every_byte_boundary_waits_for_complete_event(self):
        for i in range(len(self.event)):
            r = self.parse(self.event[:i])
            self.assertFalse(r["captureStopRequired"])
            self.assertEqual([], r["eventCategories"])
        self.assertTrue(self.parse(self.event)["captureStopRequired"])

    def test_split_crlf_and_multibyte_sequences(self):
        event = b": synthetic-" + "\u00e9".encode() + b"\r\ndata:2026-01-01T00:05:00Z process exited\r\n\r\n"
        for i in range(len(event)):
            self.assertFalse(self.parse(event[:i])["captureStopRequired"])
        self.assertEqual("SSE_DATA_CLASSIFIED", self.parse(event)["responseClass"])

    def test_heartbeat_frames_do_not_trigger_acceptance(self):
        prefix = b": heartbeat\n\nid:synthetic\nretry:1\n\n"
        self.assertFalse(self.parse(prefix)["captureStopRequired"])
        self.assertEqual(["EXIT_MENTION"], self.parse(prefix + self.event)["eventCategories"])
        self.assertEqual("SSE_FRAMING_ONLY", self.parse(prefix, final=True)["responseClass"])

    def test_unknown_first_event_stops_without_searching_later(self):
        r = self.parse(b'data:{"synthetic":"unsupported"}\n\n' + self.event)
        self.assertTrue(r["captureStopRequired"])
        self.assertEqual("SSE_UNSUPPORTED_PAYLOAD", r["responseClass"])
        self.assertEqual([], r["eventCategories"])

    def test_one_event_scope_not_whole_stream_acceptance(self):
        a = self.parse(self.event + b'data:{"synthetic":"later"}\n\n')
        b = self.parse(self.event)
        self.assertEqual(a, b)
        self.assertFalse(a["causeConfirmed"])
        self.assertFalse(a["retryAuthorized"])
        self.assertEqual("UNVERIFIED", a["runtimeImageIdentity"])

    def test_final_incomplete_event_fails_closed(self):
        r = self.parse(self.event[:-1], final=True)
        self.assertTrue(r["captureStopRequired"])
        self.assertEqual("SSE_INCOMPLETE_EVENT", r["responseClass"])
        self.assertEqual([], r["eventCategories"])

    def test_final_cr_delimiter_is_handled(self):
        value = self.event.replace(b"\n", b"\r")
        self.assertFalse(self.parse(value)["captureStopRequired"])
        self.assertEqual("SSE_DATA_CLASSIFIED", self.parse(value, final=True)["responseClass"])

    def test_exact_total_cap_stops_even_for_heartbeats(self):
        body = (b":" + b"x" * 2046 + b"\n") * 32
        self.assertEqual(m.MAX_BYTES, len(body))
        self.assertTrue(self.parse(body)["captureStopRequired"])
        r = self.parse(body + b"x")
        self.assertTrue(r["captureStopRequired"])
        self.assertEqual("BODY_CAP_EXCEEDED", r["responseClass"])

    def test_caps_invalid_input_and_window(self):
        self.assertEqual("SSE_RECORD_CAP_EXCEEDED", self.parse(b"x" * 2049)["responseClass"])
        self.assertEqual("INVALID_PREFIX_INPUT", self.parse("synthetic")["responseClass"])
        self.assertEqual("INVALID_PREFIX_INPUT", self.parse(b"", final="yes")["responseClass"])
        self.assertEqual("INVALID_WINDOW", m.sanitize_sse_prefix(b"", self.end, self.start)["responseClass"])

    def test_fake_capture_stops_before_next_chunk(self):
        def chunks():
            yield b": heartbeat\n\n"
            yield self.event[:7]
            yield self.event[7:]
            raise AssertionError("read past first complete event")
        captured = bytearray()
        for chunk in chunks():
            self.assertLessEqual(len(captured) + len(chunk), m.MAX_BYTES)
            captured.extend(chunk)
            result = self.parse(bytes(captured))
            if result["captureStopRequired"]:
                break
        self.assertEqual("SSE_DATA_CLASSIFIED", result["responseClass"])
        self.assertLess(len(captured), m.MAX_BYTES)

    def test_no_network_process_file_or_output_and_fixed_keys(self):
        out = io.StringIO()
        with patch("socket.socket", side_effect=AssertionError("network")), patch("subprocess.Popen", side_effect=AssertionError("process")), patch("builtins.open", side_effect=AssertionError("file")), contextlib.redirect_stdout(out), contextlib.redirect_stderr(out):
            r = self.parse(self.event)
        self.assertEqual("", out.getvalue())
        self.assertEqual({"responseClass", "freshness", "eventCategories", "runtimeImageIdentity", "mainProcessLiveness",
                          "listener", "causeConfirmed", "retryAuthorized", "captureStopRequired"}, set(r))


class InventoryTests(unittest.TestCase):
    observed = dt.datetime(2026, 1, 2, tzinfo=dt.timezone.utc)

    def row(self, source="synthetic-source", stamp="2026-01-01T12:00:00Z"):
        file = "2026_01_01_" + source + "_docker.log"
        return {"machineName": source, "lastUpdated": stamp, "size": 123,
                "href": "https://" + m._INVENTORY_HOST + "/api/vfs/LogFiles/" + file,
                "path": "/home/LogFiles/" + file}

    def project(self, rows):
        return m.classify_log_inventory(encode(rows), self.observed)

    def test_documented_row_projects_no_identity_or_selection(self):
        r = self.project([self.row()])
        self.assertEqual("INVENTORY_PROJECTED", r["responseClass"])
        self.assertTrue(r["schemaRecognized"])
        self.assertEqual("NONEMPTY", r["inventoryState"])
        self.assertEqual("WITHIN_24H_PRESENT", r["fileTimeClass"])
        self.assertEqual("UNVERIFIED", r["mainContainerRole"])
        self.assertEqual("UNVERIFIED", r["runtimeImageIdentity"])
        for key in ("fileSelectionAuthorized", "logBodyReadAuthorized", "retryAuthorized"):
            self.assertFalse(r[key])

    def test_empty_does_not_prove_no_main_logs(self):
        r = self.project([])
        self.assertEqual("EMPTY", r["inventoryState"])
        self.assertEqual("UNKNOWN", r["fileTimeClass"])
        self.assertEqual("UNVERIFIED", r["mainContainerRole"])

    def test_freshness_boundary_old_and_mixed(self):
        self.assertEqual("WITHIN_24H_PRESENT", self.project([self.row(stamp="2026-01-01T00:00:00Z")])["fileTimeClass"])
        old = self.row(stamp="2025-12-31T23:59:59Z")
        self.assertEqual("OLDER_ONLY", self.project([old])["fileTimeClass"])
        self.assertEqual("WITHIN_24H_PRESENT", self.project([old, self.row()])["fileTimeClass"])

    def test_future_invalid_and_non_utc_time_rejected(self):
        for stamp in ("2026-01-01", "2026-99-01T00:00:00Z", "2026-01-01T00:00:00-03:00"):
            self.assertEqual("INVALID_FILE_TIME", self.project([self.row(stamp=stamp)])["responseClass"])
        self.assertEqual("FUTURE_FILE_METADATA", self.project([self.row(stamp="2026-01-02T00:00:01Z")])["responseClass"])
        self.assertEqual("INVENTORY_PROJECTED", self.project([self.row(stamp="2026-01-01T12:00:00.1234567Z")])["responseClass"])

    def test_known_fields_only_and_typed_rows(self):
        row = self.row();row["extra"] = "synthetic"
        for rows in ([{}], [row], ["synthetic"], {"value": []}):
            self.assertEqual("UNSUPPORTED_SCHEMA", self.project(rows)["responseClass"])
        row = self.row();row.pop("path")
        self.assertEqual("UNSUPPORTED_SCHEMA", self.project([row])["responseClass"])

    def test_size_and_string_types_and_caps(self):
        for field, value in [("size", True), ("size", -1), ("size", 2**63), ("machineName", []),
                             ("machineName", "x" * 257), ("href", "x" * 2049),
                             ("path", "x" * 1025), ("lastUpdated", "x" * 41), ("machineName", "x\n")]:
            row = self.row();row[field] = value
            self.assertEqual("UNSUPPORTED_SCHEMA", self.project([row])["responseClass"])

    def test_cross_origin_credentials_query_fragment_and_scheme(self):
        row = self.row();valid = row["href"]
        for href in [valid.replace(m._INVENTORY_HOST, "synthetic.invalid"), valid.replace("https://", "http://"),
                     valid.replace("https://", "https://synthetic:synthetic@"), valid + "?synthetic=value", valid + "#fragment",
                     valid + "?", valid + "#", " " + valid, valid.replace(m._INVENTORY_HOST, m._INVENTORY_HOST + ":444")]:
            row = self.row();row["href"] = href
            self.assertEqual("UNSAFE_REFERENCE", self.project([row])["responseClass"])

    def test_traversal_double_encoding_and_mismatched_path(self):
        for suffix in ("../synthetic.log", "%2e%2e/synthetic.log", "%252e%252e/synthetic.log", "synthetic%5c.log"):
            row = self.row();row["href"] = "https://" + m._INVENTORY_HOST + "/api/vfs/LogFiles/" + suffix
            self.assertEqual("UNSAFE_REFERENCE", self.project([row])["responseClass"])
        row = self.row();row["path"] = "/home/LogFiles/different.log"
        self.assertEqual("UNSAFE_REFERENCE", self.project([row])["responseClass"])

    def test_explicit_https_port_and_encoded_filename(self):
        row = self.row("synthetic space")
        row["href"] = row["href"].replace("synthetic space", "synthetic%20space")
        self.assertEqual("INVENTORY_PROJECTED", self.project([row])["responseClass"])
        row["href"] = row["href"].replace(m._INVENTORY_HOST, m._INVENTORY_HOST + ":443")
        self.assertEqual("INVENTORY_PROJECTED", self.project([row])["responseClass"])

    def test_no_role_inference_from_kudu_or_default_suffix(self):
        a = self.project([self.row("synthetic_kudu")])
        b = self.project([self.row("synthetic_default")])
        self.assertEqual(a, b)
        self.assertEqual("UNVERIFIED", a["mainContainerRole"])

    def test_noninterference_source_sizes_links_paths_and_order(self):
        a = self.row("synthetic-a");b = self.row("synthetic-b");b["size"] = 987
        self.assertEqual(self.project([a]), self.project([b]))
        self.assertEqual(self.project([a,b]), self.project([b,a]))
        r = json.dumps(self.project([a]))
        for fragment in ("synthetic", "2026-", "https://", "LogFiles"):
            self.assertNotIn(fragment, r)

    def test_row_count_and_byte_caps(self):
        self.assertEqual("INVENTORY_PROJECTED", self.project([self.row()] * 32)["responseClass"])
        self.assertEqual("ROW_CAP_EXCEEDED", self.project([self.row()] * 33)["responseClass"])
        body = encode([self.row()]);body += b" " * (32768 - len(body))
        self.assertEqual("INVENTORY_PROJECTED", m.classify_log_inventory(body, self.observed)["responseClass"])
        self.assertEqual("BODY_CAP_EXCEEDED", m.classify_log_inventory(body + b" ", self.observed)["responseClass"])

    def test_duplicate_invalid_json_and_encoding(self):
        for body in (b'[{"size":1,"size":2}]', b'NaN', b'\xff', b'<html>synthetic</html>'):
            self.assertEqual("NONSTANDARD_BODY", m.classify_log_inventory(body, self.observed)["responseClass"])

    def test_invalid_observation_time_or_body(self):
        for observed in (None, self.observed.replace(tzinfo=None), self.observed.astimezone(dt.timezone(dt.timedelta(hours=-3)))):
            self.assertEqual("INVALID_OBSERVATION_TIME", m.classify_log_inventory(b"[]", observed)["responseClass"])
        self.assertEqual("INVALID_BODY_TYPE", m.classify_log_inventory("[]", self.observed)["responseClass"])

    def test_later_bad_row_discards_prior_partial_evidence(self):
        r = self.project([self.row(), {}])
        self.assertFalse(r["schemaRecognized"])
        self.assertEqual("UNKNOWN", r["fileTimeClass"])
        self.assertEqual("UNKNOWN", r["inventoryState"])

    def test_no_network_process_file_output_and_exact_keys(self):
        body = encode([self.row()]);out = io.StringIO()
        with patch("socket.socket", side_effect=AssertionError("network")), patch("subprocess.Popen", side_effect=AssertionError("process")), patch("builtins.open", side_effect=AssertionError("file")), contextlib.redirect_stdout(out), contextlib.redirect_stderr(out):
            r = m.classify_log_inventory(body, self.observed)
        self.assertEqual("", out.getvalue())
        self.assertEqual({"responseClass", "schemaRecognized", "inventoryState", "fileTimeClass", "mainContainerRole",
                          "runtimeImageIdentity", "fileSelectionAuthorized", "logBodyReadAuthorized", "retryAuthorized"}, set(r))


class StartupAttributionTests(unittest.TestCase):
    window = ("2026-01-01T00:00:00Z", "2026-01-01T00:01:00Z")

    def setUp(self):
        self.record = b"Research snapshot service unavailable. stage=STORAGE"
        ids = dict(instanceId="synthetic-worker", containerId="synthetic-container",
                   attemptId="synthetic-attempt")
        self.attempt = dict(ids, role="MAIN", imageDigest=m._DIAGNOSTIC_CHILD,
                            startedUtc="2026-01-01T00:00:00Z",
                            observedThroughUtc="2026-01-01T00:01:00Z")
        self.envelope = dict(ids, recordUtc="2026-01-01T00:00:30Z", clockErrorMs=100,
                             sha256=hashlib.sha256(self.record).hexdigest())

    def evaluate(self):
        return m.evaluate_startup_attribution(self.record, trusted_attempts=encode([self.attempt]),
                                              trusted_record=encode(self.envelope),
                                              observation_window=self.window)

    def test_single_attempt_and_exact_vocabulary(self):
        for category, record in {**{stage: m._STAGE_PREFIX + stage.encode()
                                   for stage in m._STARTUP_STAGES},
                                 "LISTENER_STARTED": b"Research snapshot listener started."}.items():
            with self.subTest(category=category):
                self.record = record
                self.envelope["sha256"] = hashlib.sha256(record).hexdigest()
                self.assertEqual(self.evaluate(), dict(mainContainerBound=True,
                    diagnosticImageBound=True, startupAttemptBound=True, recordWithinAttempt=True,
                    diagnosticCaptureReady=True, diagnosticCategory=category,
                    reason="CONSISTENT_TRUSTED_EVIDENCE"))

    def test_legitimate_stage_without_independent_evidence(self):
        result = m.evaluate_startup_attribution(self.record)
        self.assertFalse(result["diagnosticCaptureReady"])
        self.assertEqual("TRUSTED_BINDING_MISSING", result["reason"])
        self.assertEqual("UNKNOWN", result["diagnosticCategory"])

    def test_main_role_and_immutable_image_are_separate_requirements(self):
        cases = [("role", None, "mainContainerBound"), ("role", "SIDECAR", "mainContainerBound"),
                 ("imageDigest", "sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3",
                  "diagnosticImageBound"), ("imageDigest", "synthetic-tag", "diagnosticImageBound"),
                 ("imageDigest", None, "diagnosticImageBound")]
        for key, value, binding in cases:
            with self.subTest(key=key, value=value):
                original = self.attempt[key]
                self.attempt[key] = value
                result = self.evaluate()
                self.attempt[key] = original
                self.assertFalse(result[binding])
                self.assertTrue(result["startupAttemptBound"])
                self.assertFalse(result["diagnosticCaptureReady"])

    def test_reused_file_and_previous_attempt_cannot_match_current_attempt(self):
        self.envelope["attemptId"] = "synthetic-previous"
        result = self.evaluate()
        self.assertTrue(result["recordWithinAttempt"])
        self.assertFalse(result["startupAttemptBound"])
        self.assertFalse(result["diagnosticCaptureReady"])

    def test_conflicting_container_or_instance_identity(self):
        for key in ("instanceId", "containerId"):
            with self.subTest(key=key):
                self.envelope[key] = "synthetic-other"
                result = self.evaluate()
                self.envelope[key] = self.attempt[key]
                self.assertEqual("CONTAINER_IDENTITY_MISMATCH", result["reason"])
                self.assertFalse(result["diagnosticCaptureReady"])

    def test_multiple_instances_containers_or_overlapping_attempts_fail(self):
        for variant in (dict(instanceId="synthetic-other"), dict(containerId="synthetic-other"),
                        dict(attemptId="synthetic-other"), {}):
            with self.subTest(variant=variant):
                roster = [self.attempt, dict(self.attempt, **variant)]
                result = m.evaluate_startup_attribution(self.record, trusted_attempts=encode(roster),
                                                        trusted_record=encode(self.envelope),
                                              observation_window=self.window)
                self.assertEqual("ATTEMPT_ROSTER_NOT_UNIQUE", result["reason"])
                self.assertFalse(result["diagnosticCaptureReady"])

    def test_stale_future_and_clock_boundary_uncertainty(self):
        for stamp in ("2025-12-31T23:59:59Z", "2026-01-01T00:01:01Z",
                      "2026-01-01T00:00:00.050Z", "2026-01-01T00:00:59.950Z"):
            with self.subTest(stamp=stamp):
                self.envelope["recordUtc"] = stamp
                result = self.evaluate()
                self.assertTrue(result["startupAttemptBound"])
                self.assertFalse(result["recordWithinAttempt"])
                self.assertFalse(result["diagnosticCaptureReady"])

    def test_consistent_old_or_future_witness_is_outside_current_observation(self):
        for day in ("2025-12-31", "2026-01-02"):
            self.window = (day + "T00:00:00Z", day + "T00:01:00Z")
            result = self.evaluate()
            self.assertEqual("INVALID_TIME_EVIDENCE", result["reason"])
            self.assertFalse(result["diagnosticCaptureReady"])

    def test_invalid_utc_or_clock_uncertainty(self):
        original = dict(self.envelope)
        for changes in ({"recordUtc": "2026-02-30T00:00:00Z"},
                        {"recordUtc": "2026-01-01T00:00:30+00:00"},
                        {"recordUtc": "2026-01-01T00:00:30.1234567Z"},
                        {"clockErrorMs": 2001}, {"clockErrorMs": True}):
            with self.subTest(changes=changes):
                self.envelope = dict(original, **changes)
                result = self.evaluate()
                self.assertEqual("INVALID_TIME_EVIDENCE", result["reason"])
                self.assertFalse(result["diagnosticCaptureReady"])

    def test_missing_attempt_or_role_field_is_rejected(self):
        for key in ("attemptId", "role"):
            with self.subTest(key=key):
                old = self.attempt.pop(key)
                self.assertFalse(self.evaluate()["diagnosticCaptureReady"])
                self.attempt[key] = old

    def test_malformed_oversized_and_duplicate_metadata(self):
        for metadata in (b"{", b" " * 4097, b'[{"attemptId":"a","attemptId":"b"}]',
                         b'{"machineName":"synthetic_default"}', b"\xff"):
            with self.subTest(size=len(metadata)):
                result = m.evaluate_startup_attribution(self.record, trusted_attempts=metadata,
                                                        trusted_record=encode(self.envelope),
                                              observation_window=self.window)
                self.assertFalse(result["diagnosticCaptureReady"])

    def test_rotated_or_forged_record_breaks_integrity(self):
        self.record = b"Research snapshot service unavailable. stage=TLS"
        result = self.evaluate()
        self.assertEqual("RECORD_INTEGRITY_UNVERIFIED", result["reason"])
        self.assertFalse(result["diagnosticCaptureReady"])

    def test_marker_inside_authenticated_but_unsupported_text_fails(self):
        self.record = b"synthetic noise " + self.record
        self.envelope["sha256"] = hashlib.sha256(self.record).hexdigest()
        result = self.evaluate()
        self.assertEqual("UNSUPPORTED_RECORD", result["reason"])
        self.assertFalse(result["diagnosticCaptureReady"])
        self.assertNotIn("synthetic", json.dumps(result))


if __name__ == "__main__":
    unittest.main()
