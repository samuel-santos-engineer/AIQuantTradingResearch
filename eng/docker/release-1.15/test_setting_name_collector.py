"""Synthetic byte streams only; no HTTP/CLI/authentication/private-file use."""
import contextlib
import io
import json
import unittest
from unittest.mock import patch
import setting_name_collector as m
import setting_name_provenance as p


class Clock:
    def __init__(self): self.now = 0
    def __call__(self): return self.now


class Response:
    status = 200
    redirected = False
    attempts = 1
    content_encoding = None
    upstream_buffering = "none"
    def __init__(self, raw, clock, chunk=7, length=True):
        self.raw, self.clock, self.chunk = raw, clock, chunk
        self.offset = 0; self.closed = False; self.reads = []; self.advance = 0
        self.declared_length = len(raw) if length else None
        self.close_fail = False; self.close_advance = 0
    def read(self, maximum, **kwargs):
        self.reads.append(maximum); self.clock.now += self.advance
        end = min(len(self.raw), self.offset + self.chunk, self.offset + maximum)
        out = self.raw[self.offset:end]; self.offset = end
        return out
    def close(self, **kwargs):
        self.closed = True
        self.clock.now += self.close_advance
        if self.close_fail: raise RuntimeError("fake-secret /private/file")


class Transport:
    qualification_only = True
    network_enabled = False
    def __init__(self, clock):
        self.clock = clock; self.calls = []; self.responses = []
    def open(self, request, **kwargs):
        self.calls.append((request, kwargs))
        return self.responses[len(self.calls)-1]


def payload(role, extra=None):
    return json.dumps({"id": p.BASE + p.APPS[role] + "/config/appsettings",
                       "properties": extra or {"AIQ_RESEARCH_API_KEY": "fake-sensitive-secret"}}).encode()


class Tests(unittest.TestCase):
    def setup_streams(self):
        clock = Clock(); transport = Transport(clock)
        transport.responses = [Response(payload(role), clock) for role in p.APPS]
        return clock, transport

    def check_unknown(self, result, role="api"):
        self.assertEqual("STOPPED", result["result"])
        self.assertEqual({"UNKNOWN"}, set(result["roles"][role]["presence"].values()))
        self.assertNotIn("fake-sensitive", json.dumps(result))

    def test_exact_two_role_order_targets_allowlists(self):
        clock, transport = self.setup_streams()
        result = m.collect_offline(transport, clock)
        self.assertEqual(["api", "ui"], [x[0].role for x in transport.calls])
        for request, _ in transport.calls:
            self.assertEqual("POST", request.method)
            self.assertEqual("https://management.azure.com" + p.BASE + p.APPS[request.role] + "/config/appsettings/list?api-version=2023-12-01", request.url)
            self.assertEqual(set(p.NAMES[request.role]), set(result["roles"][request.role]["presence"]))
        self.assertTrue(all(r.closed for r in transport.responses))
        self.assertNotIn("fake-sensitive", json.dumps(result))

    def test_chunked_unknown_length_body(self):
        clock, transport = self.setup_streams()
        for response in transport.responses: response.declared_length = None; response.chunk = 3
        self.assertEqual("OFFLINE_QUALIFICATION", m.collect_offline(transport, clock)["result"])

    def test_declared_oversize_rejected_without_reading(self):
        clock, transport = self.setup_streams(); transport.responses[0].declared_length = m.LIMIT + 1
        result = m.collect_offline(transport, clock); self.check_unknown(result)
        self.assertEqual([], transport.responses[0].reads)
        self.assertEqual(1, len(transport.calls))

    def test_unknown_length_cap_stops_before_extra_byte(self):
        clock, transport = self.setup_streams(); r = transport.responses[0]
        r.raw = b"x" * (m.LIMIT + 99); r.declared_length = None; r.chunk = m.CHUNK
        self.check_unknown(m.collect_offline(transport, clock))
        self.assertEqual(m.LIMIT, r.offset)
        self.assertLessEqual(max(r.reads), m.CHUNK)

    def test_truncation(self):
        clock, transport = self.setup_streams(); transport.responses[0].declared_length += 1
        result = m.collect_offline(transport, clock); self.check_unknown(result)
        self.assertEqual("TRUNCATED", result["roles"]["api"]["reason"])

    def test_json_duplicate_and_wrong_identity(self):
        for raw in [b"{", b'{"id":"one","id":"two"}', payload("ui")]:
            clock, transport = self.setup_streams(); transport.responses[0] = Response(raw, clock)
            self.check_unknown(m.collect_offline(transport, clock))
            self.assertEqual(1, len(transport.calls))

    def test_absent_not_unknown_when_valid(self):
        clock, transport = self.setup_streams()
        result = m.collect_offline(transport, clock)
        self.assertEqual("ABSENT", result["roles"]["api"]["presence"]["WEBSITES_PORT"])

    def test_redirect_retry_compression_buffering_rejected(self):
        for field, value in [("redirected", True), ("attempts", 2), ("content_encoding", "gzip"), ("upstream_buffering", "unknown"), ("status", 401)]:
            clock, transport = self.setup_streams(); setattr(transport.responses[0], field, value)
            self.check_unknown(m.collect_offline(transport, clock))
            self.assertFalse(transport.responses[0].reads)
            self.assertEqual(1, len(transport.calls))

    def test_deadline_early_termination_and_cleanup(self):
        clock, transport = self.setup_streams(); transport.responses[0].advance = 16
        result = m.collect_offline(transport, clock); self.check_unknown(result)
        self.assertEqual("DEADLINE", result["roles"]["api"]["reason"])
        self.assertTrue(transport.responses[0].closed)
        self.assertEqual(1, len(transport.calls))

    def test_per_request_deadline_preserves_previous_elapsed_time(self):
        clock, transport = self.setup_streams()
        for response in transport.responses: response.chunk = m.CHUNK; response.advance = 14
        result = m.collect_offline(transport, clock)
        self.assertEqual("OFFLINE_QUALIFICATION", result["result"])
        self.assertEqual(29, transport.calls[1][1]["deadline"])

    def test_total_budget_caps_second_deadline(self):
        clock, transport = self.setup_streams()
        for response in transport.responses: response.chunk = m.CHUNK
        transport.responses[0].advance = 14.5
        transport.responses[0].close_advance = 0.75
        transport.responses[1].advance = 14.8
        result = m.collect_offline(transport, clock)
        self.check_unknown(result, "ui")
        self.assertEqual(30, transport.calls[1][1]["deadline"])
        self.assertEqual("COLLECTED", result["roles"]["api"]["state"])
        self.assertEqual("DEADLINE", result["roles"]["ui"]["reason"])

    def test_cleanup_deadline_violation_fails_closed(self):
        clock, transport = self.setup_streams(); transport.responses[0].close_advance = 2
        result = m.collect_offline(transport, clock); self.check_unknown(result)
        self.assertEqual("CLEANUP_ERROR", result["roles"]["api"]["reason"])
        self.assertEqual(1, len(transport.calls))

    def test_stream_contract_oversize_chunk_rejected(self):
        clock, transport = self.setup_streams()
        with patch.object(transport.responses[0], "read", return_value=b"x" * (m.CHUNK + 1)):
            result = m.collect_offline(transport, clock)
        self.check_unknown(result)
        self.assertEqual("STREAM_CONTRACT", result["roles"]["api"]["reason"])


    def test_initial_cancellation_opens_nothing(self):
        clock, transport = self.setup_streams()
        self.check_unknown(m.collect_offline(transport, clock, cancelled=lambda: True))
        self.assertEqual([], transport.calls)

    def test_cancel_between_chunks(self):
        clock, transport = self.setup_streams()
        self.check_unknown(m.collect_offline(transport, clock, cancelled=lambda: transport.responses[0].offset > 0))
        self.assertEqual(1, len(transport.calls))
        self.assertTrue(transport.responses[0].closed)

    def test_open_error_no_echo(self):
        clock, transport = self.setup_streams()
        with patch.object(transport, "open", side_effect=RuntimeError("fake-sensitive-secret /private/path")):
            result = m.collect_offline(transport, clock)
        self.check_unknown(result)
        self.assertEqual("TRANSPORT_ERROR", result["roles"]["api"]["reason"])

    def test_cleanup_error_changes_presence_to_unknown(self):
        clock, transport = self.setup_streams(); transport.responses[0].close_fail = True
        result = m.collect_offline(transport, clock); self.check_unknown(result)
        self.assertEqual("CLEANUP_ERROR", result["roles"]["api"]["reason"])
        self.assertEqual(1, len(transport.calls))

    def test_default_no_network_or_cli_and_live_block(self):
        with patch("subprocess.Popen", side_effect=AssertionError("no CLI")), patch("socket.socket", side_effect=AssertionError("no network")), contextlib.redirect_stdout(io.StringIO()) as output:
            self.assertEqual(0, m.main([]))
            for flag in ["--apply", "--execute"]:
                with self.assertRaisesRegex(m.Stop, "LIVE_TRANSPORT_UNQUALIFIED"): m.main([flag])
        self.assertEqual(0, json.loads(output.getvalue())["azureCalls"])

    def test_fake_transport_contract_required(self):
        clock, transport = self.setup_streams(); transport.network_enabled = True
        with self.assertRaises(m.Stop): m.collect_offline(transport, clock)
        self.assertEqual([], transport.calls)


if __name__ == "__main__":
    unittest.main()
