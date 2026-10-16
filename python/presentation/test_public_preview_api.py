"""Offline ASGI negatives plus exact-path, real Streamlit HTTP smoke tests."""
import asyncio
import ast
import builtins
from datetime import timedelta
import http.client
import json
import os
from pathlib import Path
import socket
import subprocess
import sys
import tempfile
import time
import unittest
from unittest.mock import patch

import public_preview_api as api


async def request(app, path="/api/v1/candles", query=b"", method="GET", headers=(), body=b"", receive=None):
    messages = []
    async def empty_receive():
        return {"type": "http.request", "body": body, "more_body": False}
    await app({"type": "http", "path": path, "method": method, "query_string": query, "headers": headers},
              receive or empty_receive, _sender(messages))
    return messages[0]["status"], dict(messages[0]["headers"]), json.loads(messages[1]["body"])


def _sender(messages):
    async def send(message):
        messages.append(message)
    return send


class PublicApiTests(unittest.IsolatedAsyncioTestCase):
    async def test_six_exact_routes_and_liveness_readiness_are_distinct(self):
        for route in api.ROUTES:
            with self.subTest(route=route):
                status, headers, body = await request(api.PublicReadOnlyAPI(), "/api/v1/" + route)
                self.assertEqual(503 if route in ("dataset", "candles", "indicators") else 200, status)
                self.assertEqual(b"application/json; charset=utf-8", headers[b"content-type"])
                self.assertEqual(b"no-store", headers[b"cache-control"])
                self.assertEqual(api.VERSION, body["apiVersion"])
                self.assertLessEqual(len(json.dumps(body).encode()), api.MAX_RESPONSE_BYTES)
                if route in ("dataset", "candles", "indicators"):
                    self.assertEqual("DATASET_NOT_READY", body["code"])
                    self.assertIsNone(body["data"])

    async def test_private_sources_overrides_duplicates_unknown_and_bad_encoding_rejected(self):
        for query, code in ((b"instrument=BTC%2FUSD", "INVALID_INSTRUMENT"),
                            (b"instrument=ETH%2FUSD", "INVALID_INSTRUMENT"),
                            (b"source=TwelveData", "INVALID_QUERY"), (b"context=private", "INVALID_QUERY"),
                            (b"cache=/private", "INVALID_QUERY"), (b"interval=1h&interval=1d", "INVALID_QUERY"),
                            (b"interval=", "INVALID_QUERY"), (b"interval=%FF", "INVALID_QUERY"),
                            (b"interval=%ZZ", "INVALID_QUERY"), (b"interval=1m", "INVALID_INTERVAL"),
                            (b"indicator=custom", "INVALID_INDICATOR"), (b"period=999999", "INVALID_QUERY")):
            status, _, body = await request(api.PublicReadOnlyAPI(), "/api/v1/indicators", query)
            self.assertEqual(400, status, query)
            self.assertEqual(code, body["code"], query)
            self.assertNotIn("private", json.dumps(body))

    async def test_fixed_utc_ninety_day_edges_alignment_and_record_bounds(self):
        for interval in api.INTERVALS:
            query = ("interval=" + interval + "&from=2026-07-03T00:00:00Z&to=2026-10-01T00:00:00Z").encode()
            self.assertEqual(503, (await request(api.PublicReadOnlyAPI(), query=query))[0])
        for query, code in ((b"from=2026-07-02T00:00:00Z", "INVALID_RANGE"),
                            (b"to=2026-10-02T00:00:00Z", "INVALID_RANGE"),
                            (b"from=2026-10-01T00:00:00Z", "INVALID_RANGE"),
                            (b"from=2026-09-01T00:00:01Z", "INVALID_RANGE"),
                            (b"from=2026-09-01T00:00:00%2B00:00", "INVALID_RANGE"),
                            (b"from=2026-02-30T00:00:00Z", "INVALID_RANGE"),
                            (b"limit=0", "INVALID_LIMIT"), (b"limit=2161", "INVALID_LIMIT"),
                            (b"limit=1", "ROW_LIMIT_EXCEEDED")):
            status, _, body = await request(api.PublicReadOnlyAPI(), query=query)
            self.assertEqual(400, status)
            self.assertEqual(code, body["code"])

    async def test_all_twelve_selections_validate_without_exposing_warmup(self):
        for interval in api.INTERVALS:
            for days in (1, 7, 30, 90):
                start = (api.FIXED_END - timedelta(days=days)).strftime("%Y-%m-%dT%H:%M:%SZ")
                status, _, body = await request(api.PublicReadOnlyAPI(), query=f"interval={interval}&from={start}".encode())
                self.assertEqual(503, status)
                self.assertIsNone(body["data"])
                self.assertNotIn("rows", body)

    async def test_bounded_errors_methods_bodies_headers_and_unknown_routes(self):
        cases = (({"method": "POST"}, 405, "METHOD_NOT_ALLOWED"),
                 ({"query": b"x" * 4097}, 414, "REQUEST_TOO_LARGE"),
                 ({"headers": [(b"x", b"a" * 16385)]}, 431, "REQUEST_TOO_LARGE"),
                 ({"headers": [(b"content-length", b"1")], "body": b"x"}, 400, "INVALID_REQUEST"),
                 ({"headers": [(b"transfer-encoding", b"chunked")]}, 400, "INVALID_REQUEST"),
                 ({"headers": [(b"content-length", b"0")] * 2}, 400, "INVALID_REQUEST"),
                 ({"body": b"unexpected"}, 400, "INVALID_REQUEST"),
                 ({"path": "/api/v1/regenerate"}, 404, "NOT_FOUND"))
        for kwargs, expected, code in cases:
            status, _, body = await request(api.PublicReadOnlyAPI(), **kwargs)
            self.assertEqual((expected, code), (status, body["code"]))

    async def test_rate_limits_are_global_bounded_and_liveness_has_reserved_budget(self):
        clock = [100.0]
        app = api.PublicReadOnlyAPI(clock=lambda: clock[0])
        self.assertEqual(503, (await request(app))[0])
        self.assertEqual(503, (await request(app))[0])
        self.assertEqual(429, (await request(app))[0])
        self.assertEqual(200, (await request(app, "/api/v1/health"))[0])
        clock[0] = 99.0
        self.assertEqual(429, (await request(app))[0])
        clock[0] = 101.0
        self.assertEqual(429, (await request(app))[0])  # rollback remains sticky
        clock = [0.0]
        limiter = api.WindowLimit(clock=lambda: clock[0])
        for second in range(15):
            clock[0] = float(second * 2)
            self.assertTrue(limiter.accept()); self.assertTrue(limiter.accept())
        clock[0] = 31.0
        self.assertFalse(limiter.accept())
        self.assertEqual(30, len(limiter.history))
        clock[0] = 60.0
        self.assertTrue(limiter.accept())

    async def test_no_queue_concurrency_and_body_timeout_release_admission(self):
        app = api.PublicReadOnlyAPI()
        entered, release = asyncio.Event(), asyncio.Event()
        async def slow_receive():
            entered.set()
            await release.wait()
            return {"type": "http.request", "body": b""}
        first = asyncio.create_task(request(app, receive=slow_receive))
        await entered.wait()
        self.assertEqual("SERVICE_BUSY", (await request(app))[2]["code"])
        self.assertEqual(200, (await request(app, "/api/v1/health"))[0])
        self.assertEqual("INVALID_REQUEST", (await first)[2]["code"])
        self.assertEqual(0, app.active)

    async def test_no_files_processes_or_provider_calls_and_no_state_promotes_readiness(self):
        with patch.object(builtins, "open", side_effect=AssertionError("file access forbidden")), \
                patch.object(subprocess, "Popen", side_effect=AssertionError("spawn forbidden")):
            for route in ("dataset", "candles", "indicators"):
                status, _, body = await request(api.PublicReadOnlyAPI(), "/api/v1/" + route)
                self.assertEqual((503, "DATASET_NOT_READY"), (status, body["code"]))
        tree = ast.parse(Path(api.__file__).read_text(encoding="utf-8"))
        imports = {node.module.split(".")[0] for node in ast.walk(tree) if isinstance(node, ast.ImportFrom)}
        imports.update(alias.name.split(".")[0] for node in ast.walk(tree) if isinstance(node, ast.Import) for alias in node.names)
        self.assertEqual({"__future__", "asyncio", "collections", "datetime", "json", "re", "time", "urllib"}, imports)

    async def test_unexpected_failure_is_sanitized_and_oversized_output_is_replaced(self):
        with patch.object(api, "payload", side_effect=RuntimeError("private credential /secret/path")):
            status, _, body = await request(api.PublicReadOnlyAPI())
            self.assertEqual((503, api.failure("SERVICE_UNAVAILABLE")), (status, body))
        with patch.object(api, "payload", return_value=(200, {"sensitive": "x" * 4097})):
            status, _, body = await request(api.PublicReadOnlyAPI())
            self.assertEqual((503, api.failure("SERVICE_UNAVAILABLE")), (status, body))

    async def test_transport_failure_does_not_send_a_second_response_or_keep_admission(self):
        app, messages = api.PublicReadOnlyAPI(), []
        async def receive():
            return {"type": "http.request", "body": b""}
        async def broken_send(message):
            messages.append(message)
            raise OSError("connection closed")
        with self.assertRaises(OSError):
            await app({"type": "http", "path": "/api/v1/candles", "method": "GET"}, receive, broken_send)
        self.assertEqual(1, len(messages))
        self.assertEqual(0, app.active)

    async def test_boundary_reserves_api_and_admin_without_spa_or_websocket_bypass(self):
        async def forbidden(*args):
            self.fail("reserved path reached Streamlit")
        for path in ("/admin", "/admin/recovery", "/api/v2/status", "/API/v1/health", "/api/v1/unknown"):
            self.assertEqual(404, (await request(api.PublicBoundary(forbidden), path))[0])
        messages = []
        await api.PublicBoundary(forbidden)({"type": "websocket", "path": "/api/v1/health"}, None, _sender(messages))
        self.assertEqual([{"type": "websocket.close", "code": 1008}], messages)


class RealStreamlitHttpTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.temp = tempfile.TemporaryDirectory(prefix="aiq-c2b-http-")
        cls.log = open(Path(cls.temp.name) / "server.log", "w+b")
        with socket.socket() as reservation:
            reservation.bind(("127.0.0.1", 0))
            cls.port = reservation.getsockname()[1]
        env = {name: os.environ[name] for name in ("SYSTEMROOT", "WINDIR", "PATH") if name in os.environ}
        env.update(HOME=cls.temp.name, USERPROFILE=cls.temp.name, TEMP=cls.temp.name, TMP=cls.temp.name,
                   PYTHONDONTWRITEBYTECODE="1", STREAMLIT_BROWSER_GATHER_USAGE_STATS="false")
        # Windows virtualenv launchers can leave the real interpreter behind when
        # terminated. Own the interpreter directly, with the same pinned packages.
        import streamlit
        env["PYTHONPATH"] = str(Path(streamlit.__file__).parents[1])
        script = Path(__file__).with_name("public_preview_app.py")
        cls.process = subprocess.Popen([sys._base_executable, "-B", "-m", "streamlit", "run", str(script),
            "--server.address", "127.0.0.1", "--server.port", str(cls.port), "--server.headless", "true",
            "--browser.gatherUsageStats", "false", "--server.fileWatcherType", "none",
            "--client.showErrorDetails", "none", "--secrets.files", "[]"],
            cwd=str(script.parents[2]), env=env, stdout=cls.log, stderr=subprocess.STDOUT)
        try:
            deadline = time.monotonic() + 30
            while time.monotonic() < deadline:
                if cls.process.poll() is not None:
                    cls.log.seek(0)
                    raise AssertionError("Streamlit startup failed: " + cls.log.read(5000).decode(errors="replace"))
                try:
                    if cls.fetch("/_stcore/health")[0] == 200:
                        return
                except OSError:
                    pass
                time.sleep(0.1)
            raise AssertionError("Streamlit did not become healthy within 30 seconds")
        except BaseException:
            cls.tearDownClass()
            raise

    @classmethod
    def tearDownClass(cls):
        if cls.process.poll() is None:
            cls.process.terminate()
            try:
                cls.process.wait(timeout=5)
            except subprocess.TimeoutExpired:
                cls.process.kill(); cls.process.wait(timeout=5)
        cls.log.close()
        cls.temp.cleanup()

    @classmethod
    def fetch(cls, path, method="GET", body=None):
        connection = http.client.HTTPConnection("127.0.0.1", cls.port, timeout=3)
        try:
            connection.request(method, path, body=body)
            response = connection.getresponse()
            return response.status, dict(response.getheaders()), response.read(8192)
        finally:
            connection.close()

    def test_real_server_root_health_six_routes_and_reserved_admin(self):
        status, headers, body = self.fetch("/")
        self.assertEqual(200, status)
        self.assertIn("text/html", headers["content-type"])
        self.assertIn(b"<html", body.lower())
        self.assertEqual(200, self.fetch("/_stcore/health")[0])
        for route in ("health", "status", "dataset", "candles", "indicators", "diagnostics"):
            time.sleep(0.55)
            status, headers, raw = self.fetch("/api/v1/" + route)
            self.assertEqual(503 if route in ("dataset", "candles", "indicators") else 200, status, route)
            self.assertIn("application/json", headers["content-type"])
            self.assertEqual(api.VERSION, json.loads(raw)["apiVersion"])
            self.assertLessEqual(len(raw), api.MAX_RESPONSE_BYTES)
            for forbidden in (b"Traceback", b"ApiKey", b"/home/", b"C:\\", b"processId"):
                self.assertNotIn(forbidden, raw)
        self.assertEqual(404, self.fetch("/admin")[0])
        time.sleep(0.55)
        self.assertEqual(405, self.fetch("/api/v1/dataset", "POST", b"{}")[0])
        self.assertEqual(200, self.fetch("/_stcore/health")[0])

    def test_each_real_endpoint_rejects_unknown_query_without_disclosing_input(self):
        for route in ("health", "status", "dataset", "candles", "indicators", "diagnostics"):
            with self.subTest(route=route):
                time.sleep(0.55)
                status, headers, raw = self.fetch("/api/v1/" + route + "?privatePath=%2Fprivate%2Fsecret")
                self.assertEqual(400, status)
                self.assertEqual("application/json; charset=utf-8", headers["content-type"])
                self.assertEqual("no-store", headers["cache-control"])
                self.assertEqual(api.failure("INVALID_QUERY"), json.loads(raw))
                self.assertLessEqual(len(raw), api.MAX_RESPONSE_BYTES)
                self.assertNotIn(b"secret", raw)
                self.assertNotIn(b"privatePath", raw)
        self.assertEqual(200, self.fetch("/_stcore/health")[0])

    def test_clean_server_restart_does_not_promote_dataset_or_indicator_readiness(self):
        # Ordinary localhost server restart only: no Worker, guardian, generation,
        # snapshot publication, fault injection or admission recovery experiment.
        type(self).tearDownClass()
        type(self).setUpClass()
        for route in ("health", "status", "dataset", "candles", "indicators", "diagnostics"):
            with self.subTest(route=route):
                time.sleep(0.55)
                status, headers, raw = self.fetch("/api/v1/" + route)
                body = json.loads(raw)
                self.assertEqual("application/json; charset=utf-8", headers["content-type"])
                self.assertEqual(503 if route in ("dataset", "candles", "indicators") else 200, status)
                self.assertEqual("OK" if route == "health" else "DATASET_NOT_READY", body["code"])
                if route == "status":
                    self.assertEqual({"web": "ready", "dataset": "unavailable", "indicators": "unavailable",
                                      "generationEnabled": False}, body["data"])
                if route == "dataset":
                    self.assertEqual("2025-10-26T00:00:00Z", body["contract"]["qualifiedReferenceStartUtc"])
                    self.assertEqual("2026-10-01T00:00:00Z", body["contract"]["qualifiedReferenceEndExclusiveUtc"])
                    self.assertIsNone(body["contract"]["actualCoverage"])
                    self.assertIsNone(body["contract"]["datasetFingerprint"])
                    self.assertFalse(body["contract"]["rollingRefreshEnabled"])
                if route in ("dataset", "candles", "indicators"):
                    self.assertIsNone(body["data"])
        self.assertEqual(200, self.fetch("/")[0])
        self.assertEqual(200, self.fetch("/_stcore/health")[0])


if __name__ == "__main__":
    unittest.main()
