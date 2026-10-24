import copy
import datetime as dt
import hashlib
import hmac
import json
import pathlib
import unittest
from unittest.mock import patch

from python.presentation import research_snapshot_client as m

KEY = "offline-test-service-key-12345678"
G = "22222222-2222-4222-8222-222222222222"
SEG = "33333333-3333-4333-8333-333333333333"


def wire(value):
    return json.dumps(value, separators=(",", ":")).encode()


def full(empty=False):
    t = "2026-10-23T12:00:00Z"
    status = {"observedAt": "2026-10-23T12:01:15.000000Z", "research": "Unavailable",
              "freshness": "Unavailable" if empty else "Current", "integrity": "Protected", "classificationValidity": "Unavailable",
              "lastCompletedCandleAt": None if empty else t, "lastSuccessfulProviderPollAt": None if empty else "2026-10-23T12:01:00.000000Z",
              "lastCommitAt": None if empty else "2026-10-23T12:01:00.000000Z", "researchAsOfCandleAt": None}
    row = {"time": t, "segment": SEG, "segmentIndex": "1", "open": "91.602434113300", "high": "92.518458454433",
           "low": "90.686409772167", "close": "91.602434113300", "volume": "1.000000000000"}
    return {"schemaVersion": "1.0", "type": "full", "instrument": "BTC/USD", "requestEpoch": "1", "observedAt": status["observedAt"],
            "identity": {"generation": G, "version": "0" if empty else "1", "displayDate": "2026-10-23", "transformEpoch": "1"},
            "contract": {"researchAlgorithm": "aiq-r115-research-v1", "transformAlgorithm": "aiq-r115-transform-v1", "intervalSeconds": 60},
            "status": status, "candles": [] if empty else [row], "analytics": [] if empty else [{"time": t, **dict.fromkeys(m.INDICATORS)}],
            "classifications": [] if empty else [{"time": t, "segment": SEG, "validity": "Unavailable", "trendScore": None, "trend": None, "regime": None}],
            "events": [], "researchUnavailableIntervals": [] if empty else [{"start": t, "end": "2026-10-23T12:01:00Z"}], "marketDataGapIntervals": []}


class SnapshotTests(unittest.TestCase):
    def setUp(self):
        self.client = m.ResearchSnapshotClient("https://localhost:52731", KEY)
        self.client.begin("BTC/USD")

    def reject(self, value):
        with self.assertRaises(m.SnapshotUnavailable):
            self.client.accept(wire(value), "BTC/USD", 1)
        self.assertIsNone(self.client.snapshot("BTC/USD"))

    def test_full_is_validated_and_deeply_immutable(self):
        data = self.client.accept(wire(full()), "BTC/USD", 1)
        self.assertTrue(self.client.connected)
        with self.assertRaises(TypeError):
            data["candles"][0]["open"] = "raw"
        self.assertEqual(1, len(data["candles"]))

    def test_verified_empty_state_is_not_ready(self):
        self.assertEqual("Unavailable", self.client.accept(wire(full(True)), "BTC/USD", 1)["status"]["research"])

    def test_unchanged_requires_complete_identity_and_has_no_arrays(self):
        data = full()
        self.client.accept(wire(data), "BTC/USD", 1)
        self.client.begin("BTC/USD")
        data["requestEpoch"] = "2"
        data["type"] = "unchanged"
        for field in m.COLLECTIONS:
            del data[field]
        self.assertEqual("unchanged", self.client.accept(wire(data), "BTC/USD", 2)["type"])
        data["candles"] = None
        with self.assertRaises(m.SnapshotUnavailable):
            self.client.accept(wire(data), "BTC/USD", 2)

    def test_malformed_failure_preserves_last_good_and_last_sync(self):
        self.client.accept(wire(full()), "BTC/USD", 1)
        before, synced = self.client.snapshot("BTC/USD"), self.client.last_successful_sync
        with self.assertRaises(m.SnapshotUnavailable):
            self.client.accept(b'{', "BTC/USD", 1)
        self.assertFalse(self.client.connected)
        self.assertEqual(before, self.client.snapshot("BTC/USD"))
        self.assertEqual(synced, self.client.last_successful_sync)

    def test_late_response_does_not_replace_latest_snapshot(self):
        self.client.begin("BTC/USD")
        with self.assertRaises(m.SnapshotUnavailable):
            self.client.accept(wire(full()), "BTC/USD", 1)

    def test_identity_collision_is_rejected(self):
        data = full()
        self.client.accept(wire(data), "BTC/USD", 1)
        self.client.begin("BTC/USD")
        data["requestEpoch"] = "2"
        data["candles"][0]["volume"] = "2.000000000000"
        with self.assertRaises(m.SnapshotUnavailable):
            self.client.accept(wire(data), "BTC/USD", 2)

    def test_wrong_instrument_and_epoch_types(self):
        for field, value in (("instrument", "ETH/USD"), ("requestEpoch", 1), ("requestEpoch", "01"), ("requestEpoch", "9223372036854775808")):
            with self.subTest(field=field, value=value):
                obj = full()
                obj[field] = value
                self.reject(obj)

    def test_identity_canonical_forms(self):
        for field, value in (("generation", G.upper()), ("generation", "00000000-0000-0000-0000-000000000000"), ("transformEpoch", "0"), ("version", True), ("displayDate", "2026-02-30")):
            # G contains only digits; use an alphabetic UUID to exercise uppercase rejection.
            if field == "generation" and value == G.upper():
                value = "AAAAAAAA-AAAA-4AAA-8AAA-AAAAAAAAAAAA"
            with self.subTest(field=field, value=value):
                obj = full()
                obj["identity"][field] = value
                self.reject(obj)

    def test_known_integer_is_not_bool_fraction_or_exponent(self):
        for lexeme in ("true", "60.0", "6e1", "-0"):
            payload = wire(full()).replace(b'"intervalSeconds":60', b'"intervalSeconds":' + lexeme.encode())
            with self.subTest(lexeme=lexeme), self.assertRaises(m.SnapshotUnavailable):
                self.client.accept(payload, "BTC/USD", 1)

    def test_decimal_canonical_forms_and_price_order(self):
        for value in (91, "91.0", "NaN", "+91.000000000000", "-0.000000000000", "0.000000000000", "4000000000.000000000001", "999.000000000000"):
            with self.subTest(value=value):
                data = full()
                data["candles"][0]["open"] = value
                self.reject(data)

    def test_bounds_utf8_duplicates_surrogates_trailing_and_depth(self):
        payloads = [b'\xff', b'\xef\xbb\xbf{}', b'{}{}', b'{"x":1,"x":2}', b'{"x":"\\ud800"}',
                    b'{"x":NaN}', b'[' * 9 + b'0' + b']' * 9, b' ' * (m.LIMIT + 1),
                    b'{"x":1e25}', b'{"x":1e-25}', b'{"x":1e99999999999999}', b'{"x":"' + b'a' * 257 + b'"}',
                    wire({str(i): 0 for i in range(33)}), wire({"x": [0] * 2049}), wire({"a" * 65: 1}),
                    b'{"x":' + b'1' * 65 + b'}', wire({"x": "\u00e9" * 129}),
                    wire({"a": [{str(i): 0 for i in range(32)}] * 2048, "b": [{str(i): 0 for i in range(32)}] * 2048})]
        for payload in payloads:
            with self.subTest(payload=payload[:30]), self.assertRaises((m.SnapshotUnavailable, ValueError, UnicodeError)):
                m.parse(payload)

    def test_compatible_unknown_optional_fields_are_bounded_and_accepted(self):
        data = full()
        data["schemaVersion"] = "1.99"
        data["extra"] = {"exactNumber": 0.125, "text": "\u00e9" * 128}
        self.client.accept(wire(data), "BTC/USD", 1)

    def test_unsupported_schema_or_algorithm(self):
        for kind in ("schema", "research", "transform"):
            data = full()
            if kind == "schema":
                data["schemaVersion"] = "2.0"
            else:
                data["contract"][kind + "Algorithm"] = "wrong"
            self.reject(data)

    def test_row_alignment_order_and_segment_identity(self):
        for kind in ("time", "segment", "index", "missing", "duplicate"):
            data = full()
            if kind == "time":
                data["analytics"][0]["time"] = "2026-10-23T12:00:00+00:00"
            elif kind == "segment":
                data["classifications"][0]["segment"] = G
            elif kind == "index":
                data["candles"][0]["segmentIndex"] = "0"
            elif kind == "missing":
                data["analytics"] = []
            else:
                for name in ("candles", "analytics", "classifications"):
                    data[name] *= 2
            self.reject(data)

    def test_gap_and_unavailable_intervals_are_exact(self):
        for name in ("marketDataGapIntervals", "researchUnavailableIntervals"):
            data = full()
            data[name] = [] if data[name] else [{"start": "2026-10-23T12:00:00Z", "end": "2026-10-23T12:01:00Z"}]
            self.reject(data)

    def test_future_poll_commit_or_candle_is_rejected(self):
        for field in ("lastCommitAt", "lastSuccessfulProviderPollAt"):
            data = full()
            data["status"][field] = "2026-10-24T00:00:00.000000Z"
            self.reject(data)
        data = full()
        data["observedAt"] = data["status"]["observedAt"] = "2026-10-23T12:00:59.000000Z"
        self.reject(data)

    def test_readiness_and_classification_are_not_inferred_from_candles(self):
        data = full()
        data["status"]["research"] = "Ready"
        self.reject(data)
        data = full()
        data["classifications"][0].update(validity="Valid", trendScore=0, trend="Neutral", regime="Undetermined")
        self.reject(data)

    def test_freshness_boundary_is_close_plus_120_seconds(self):
        data = full()
        data["observedAt"] = data["status"]["observedAt"] = "2026-10-23T12:03:00.000000Z"
        self.client.accept(wire(data), "BTC/USD", 1)
        self.client.begin("BTC/USD")
        data["requestEpoch"] = "2"
        data["observedAt"] = data["status"]["observedAt"] = "2026-10-23T12:03:00.000001Z"
        data["status"]["freshness"] = "Stale"
        self.client.accept(wire(data), "BTC/USD", 2)

    def test_first_analytics_seed_cannot_appear_early(self):
        data = full()
        data["analytics"][0]["sma20"] = "91.602434113300"
        self.reject(data)

    def test_events_require_membership_type_alignment_and_uniqueness(self):
        data = full()
        event = {"time": data["candles"][0]["time"], "segment": SEG, "type": "SegmentStart", "previousTrend": None, "trend": None, "regime": None}
        data["events"] = [event, event]
        self.reject(data)
        data["events"] = [dict(event, type="TrendTransition")]
        self.reject(data)

    def test_two_sessions_and_instruments_do_not_share_state(self):
        self.client.accept(wire(full()), "BTC/USD", 1)
        other = m.ResearchSnapshotClient("https://localhost", KEY)
        self.assertIsNone(other.snapshot("BTC/USD"))
        self.assertIsNone(self.client.snapshot("ETH/USD"))
        self.assertEqual("1", self.client.begin("ETH/USD")["requestEpoch"])
        self.assertEqual("1", other.begin("BTC/USD")["requestEpoch"])

    def test_epoch_and_day_changes_force_full_and_monotonic_acceptance(self):
        self.client.accept(wire(full()), "BTC/USD", 1)
        self.client.begin("BTC/USD")
        data = full()
        data["requestEpoch"] = "2"
        data["identity"]["transformEpoch"] = "2"
        self.client.accept(wire(data), "BTC/USD", 2)
        self.client.begin("BTC/USD")
        data["requestEpoch"] = "3"
        data["identity"]["transformEpoch"] = "1"
        with self.assertRaises(m.SnapshotUnavailable):
            self.client.accept(wire(data), "BTC/USD", 3)

    def test_new_generation_requires_full_not_unchanged(self):
        self.client.accept(wire(full()), "BTC/USD", 1)
        self.client.begin("BTC/USD")
        data = full(True)
        data["requestEpoch"] = "2"
        data["identity"]["generation"] = SEG
        self.client.accept(wire(data), "BTC/USD", 2)

    def test_client_rejects_plaintext_userinfo_query_and_bad_configuration(self):
        for url in ("http://localhost", "https://u:p@localhost", "https://localhost/path", "https://localhost?q=1", "https://localhost/#x"):
            with self.subTest(url=url), self.assertRaises(m.SnapshotUnavailable):
                m.ResearchSnapshotClient(url, KEY)
        for key in ("", "short", "a" * 257, "a" * 16 + "\n"):
            with self.assertRaises(m.SnapshotUnavailable):
                m.ResearchSnapshotClient("https://localhost", key)

    def test_tls_context_requires_host_and_certificate_validation(self):
        self.assertTrue(self.client._ssl.check_hostname)
        self.assertEqual(m.ssl.CERT_REQUIRED, self.client._ssl.verify_mode)

    def test_independent_hmac_corpus_matches_exact_bytes_digest_and_rounding(self):
        path = pathlib.Path(__file__).resolve().parents[2] / "tests/runtime/release_1_15/research_snapshot_contract_vectors.json"
        corpus = json.loads(path.read_text(encoding="utf-8"))
        for vector in corpus["vectors"]:
            message = (corpus["algorithm"] + "\n" + vector["day"] + "\n" + corpus["privateTestKeyEpoch"] + "\n").encode("ascii")
            self.assertEqual(vector["messageHex"], message.hex())
            self.assertEqual(vector["byteCount"], len(message))
            digest = hmac.digest(bytes.fromhex(corpus["testKeyHex"]), message, "sha256")
            self.assertEqual(vector["digest"], digest.hex())
            factor = 900000000000 + int.from_bytes(digest, "big") * 200000000001 // 2**256
            self.assertEqual(vector["factor"], str(factor))
            for n, key in ((100 * 10**12, "price100Units"), (-7 * 10**12, "negative7Units")):
                q, r = divmod(abs(n) * factor, 10**12)
                q += r * 2 > 10**12 or r * 2 == 10**12 and q % 2 == 1
                self.assertEqual(vector[key], str(-q if n < 0 else q))

    def test_large_decimal_ceiling_is_exact_without_context_rounding(self):
        self.assertEqual(m.D("1000000000000000000000000.000000000000"), m.quantity("1000000000000000000000000.000000000000", "1e24"))
        with self.assertRaises(m.SnapshotUnavailable):
            m.quantity("1000000000000000000000000.000000000001", "1e24")

    def test_per_instrument_connection_observations_are_separate(self):
        self.client.accept(wire(full()), "BTC/USD", 1)
        self.assertEqual("Connected", self.client.connection_state("BTC/USD")[0])
        self.assertEqual(("Disconnected", None), self.client.connection_state("ETH/USD"))

    def test_transport_rejects_non200_media_encoding_oversize_and_preserves_cache(self):
        self.client.accept(wire(full()), "BTC/USD", 1)
        before = self.client.snapshot("BTC/USD")
        class Sock:
            def settimeout(self, _): pass
        class Connection:
            sock = Sock()
            def __init__(self, *args, **kwargs): pass
            def connect(self): pass
            def request(self, *args, **kwargs): pass
            def getresponse(self): return response
            def close(self): pass
        class Response:
            status = 200
            headers = {"Content-Type": "application/json"}
            def getheaders(self): return list(self.headers.items())
            def getheader(self, key, default=None): return self.headers.get(key, default)
            def read1(self, n): return b""
        for status, headers in ((302, {}), (503, {}), (200, {"Content-Type": "text/plain"}),
                                (200, {"Content-Type": "application/json", "Content-Encoding": "gzip"}),
                                (200, {"Content-Type": "application/json", "Content-Length": str(m.LIMIT + 1)})):
            response = Response()
            response.status, response.headers = status, headers
            with patch.object(m.http.client, "HTTPSConnection", Connection), self.assertRaises(m.SnapshotUnavailable):
                self.client.fetch("BTC/USD")
            self.assertFalse(self.client.connected)
            self.assertEqual(before, self.client.snapshot("BTC/USD"))


if __name__ == "__main__":
    unittest.main()
