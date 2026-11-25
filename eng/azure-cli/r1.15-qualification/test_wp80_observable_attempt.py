import datetime as dt
import json
import unittest

import wp80_observable_attempt as attempt


class Response:
    def __init__(self, status, headers=()):
        self.status = status
        self.headers = headers

    def getheaders(self):
        return self.headers


class FakeAttempt:
    origin = dt.datetime(2026, 1, 1, tzinfo=dt.timezone.utc)
    marker = b"2026-01-01T00:00:15Z Research snapshot startup. stage=SERVING\n"

    def __init__(self, health, startup=None):
        self.health = iter(health)
        self.startup = startup or (Response(200, [("Content-Type", "text/plain")]), self.marker)
        self.events = []
        self.elapsed = 0
        self.startup_body_reads = 0
        self.out = {"health": [], "rollback": "NOT_TRIGGERED", "result": "BLOCKED"}

    def utc(self):
        return (self.origin + dt.timedelta(seconds=self.elapsed)).isoformat().replace("+00:00", "Z")

    def wait(self, delay):
        self.elapsed += delay

    def request(self, host, method, path, headers, *, seconds, cap, readable):
        self.events.append((host, method, path, self.elapsed, seconds, cap))
        if host == attempt.LAB_HOST and method == "GET" and path == "/health":
            planned = next(self.health)
            if isinstance(planned, Exception):
                raise planned
            status, body = planned
            response = Response(status)
        elif host == attempt.LAB_SCM and method == "GET" and path == attempt.STARTUP_PATH:
            if headers["Accept"] != "text/plain, application/json" or headers["Accept-Encoding"] != "identity":
                raise AssertionError("unexpected startup negotiation")
            if isinstance(self.startup, Exception):
                raise self.startup
            response, body = self.startup
            if readable(response):
                self.startup_body_reads += 1
                return response.status, body
            return response.status, None
        else:
            raise AssertionError("unexpected target")
        return response.status, body if readable(response) else None

    def run(self, *, apply=None, rollback=None, startup_token=lambda: "fake-bearer"):
        attempt.run_observable_attempt(
            self.out, apply=apply or (lambda: self.events.append("apply")),
            rollback=rollback or (lambda: self.events.append("rollback")),
            request=self.request, startup_token=startup_token,
            snapshot=lambda instrument: self.events.append(instrument),
            monotonic=lambda: self.elapsed, wait=self.wait, utc=self.utc,
        )
        return self.out


class ObservableAttemptTests(unittest.TestCase):
    def test_both_formats_allow_one_read_and_same_sanitized_evidence(self):
        results = []
        for media, candidate in (
            ("text/plain", FakeAttempt.marker),
            ("application/json", json.dumps({"content": FakeAttempt.marker.decode(),
                                             "instance": "never-emit-instance"}).encode()),
        ):
            with self.subTest(media=media):
                fake = FakeAttempt([(200, b"Alive")], (Response(200, [("Content-Type", media)]), candidate))
                out = fake.run()
                self.assertEqual("PASS", out["result"])
                self.assertEqual(1, fake.startup_body_reads)
                self.assertEqual([
                    "apply", (attempt.LAB_HOST, "GET", "/health", 15, 8, 16),
                    (attempt.LAB_SCM, "GET", attempt.STARTUP_PATH, 15, 8, 16383),
                    "BTC/USD", "ETH/USD",
                ], fake.events)
                self.assertNotIn("never-emit-instance", json.dumps(out))
                self.assertNotIn("fake-bearer", json.dumps(out))
                results.append(out["startupRecords"])
        self.assertEqual(results[0], results[1])

    def test_failed_health_uses_three_probes_and_one_image_rollback(self):
        fake = FakeAttempt([TimeoutError(), (200, b"Alive?"), (503, b"ignored")])
        out = fake.run()
        self.assertEqual("HEALTH_FAILED", out["result"])
        self.assertEqual("IMAGE_RESTORED", out["rollback"])
        self.assertEqual([None, 200, 503], [h["status"] for h in out["health"]])
        self.assertEqual([
            "apply", (attempt.LAB_HOST, "GET", "/health", 15, 8, 16),
            (attempt.LAB_HOST, "GET", "/health", 60, 8, 16),
            (attempt.LAB_HOST, "GET", "/health", 120, 8, 16),
            (attempt.LAB_SCM, "GET", attempt.STARTUP_PATH, 120, 8, 16383), "rollback",
        ], fake.events)

    def test_header_denials_read_zero_body_and_keep_alive_separate(self):
        cases = (
            (302, [("Location", "fake-target")], "REDIRECT_BLOCKED"),
            (403, [], "ACCESS_DENIED"),
            (200, [("Content-Type", "text/html")], "UNSUPPORTED_MEDIA"),
            (200, [("Content-Type", "application/json"), ("Content-Encoding", "gzip")], "UNSUPPORTED_ENCODING"),
            (200, [("Content-Type", "text/plain"), ("content-type", "application/json")], "AMBIGUOUS_HEADERS"),
            (200, [("Content-Type", "text/plain"), ("Transfer-Encoding", "chunked"),
                   ("Content-Length", "4")], "AMBIGUOUS_HEADERS"),
        )
        for status, headers, expected in cases:
            with self.subTest(expected=expected):
                fake = FakeAttempt([(200, b"Alive")], (Response(status, headers), b"never-consumed"))
                out = fake.run()
                self.assertEqual(expected, out["startupResponseClass"])
                self.assertEqual("BLOCKED_STARTUP_EVIDENCE", out["result"])
                self.assertTrue(out["healthAccepted"])
                self.assertEqual(0, fake.startup_body_reads)
                self.assertEqual(["BTC/USD", "ETH/USD"], fake.events[-2:])

    def test_startup_read_error_and_auth_error_leave_rollback_available(self):
        for startup, token, request_count in (
            (TimeoutError("private"), lambda: "fake", 1),
            (None, lambda: (_ for _ in ()).throw(RuntimeError("private")), 0),
        ):
            with self.subTest(request_count=request_count):
                fake = FakeAttempt([(429, b"")], startup)
                out = fake.run(startup_token=token)
                self.assertEqual("IMAGE_RESTORED", out["rollback"])
                self.assertEqual(request_count, sum(isinstance(e, tuple) and e[0] == attempt.LAB_SCM
                                                    for e in fake.events))
                self.assertEqual(1, len(out["health"]))
                self.assertNotIn("private", json.dumps(out))

    def test_deadline_skips_probes_and_partial_budget_limits_timeout(self):
        def finish_apply(fake, elapsed):
            fake.events.append("apply")
            fake.elapsed = elapsed

        fake = FakeAttempt([(503, b""), (503, b""), (503, b"")])
        late = fake.run(apply=lambda: finish_apply(fake, 176))
        self.assertEqual((attempt.LAB_HOST, "GET", "/health", 176, 4, 16), fake.events[1])
        self.assertEqual(1, len(late["health"]))
        for elapsed in (178, 180):
            with self.subTest(elapsed=elapsed):
                bounded = FakeAttempt([])
                out = bounded.run(apply=lambda: finish_apply(bounded, elapsed))
                self.assertEqual([], out["health"])
                self.assertFalse(out["healthAccepted"])

    def test_apply_and_rollback_safety_errors_propagate(self):
        fake = FakeAttempt([])
        def blocked():
            raise RuntimeError("DRIFT")
        with self.assertRaisesRegex(RuntimeError, "^DRIFT$"):
            fake.run(apply=blocked)
        self.assertEqual([], fake.events)

        fake = FakeAttempt([(503, b""), (503, b""), (503, b"")])
        with self.assertRaisesRegex(RuntimeError, "^DRIFT$"):
            fake.run(rollback=blocked)
        self.assertEqual("RECHECK_PENDING", fake.out["rollback"])
        self.assertEqual("BLOCKED", fake.out["result"])
        self.assertNotIn("BTC/USD", fake.events)
