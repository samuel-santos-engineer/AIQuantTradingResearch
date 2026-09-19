import json
import subprocess
import unittest

from historical_market_bridge import *


def response(state="Fresh", candles=None):
    return json.dumps({"contractVersion": CONTRACT_VERSION, "state": state, "symbol": "BTC/USD" if state != "Unavailable" else None, "interval": "1h" if state != "Unavailable" else None, "range": "30D" if state != "Unavailable" else None, "candles": candles if candles is not None else ([] if state == "Unavailable" else [{"openTimeUtc":"2026-01-01T00:00:00+00:00","open":"10","high":"12","low":"9","close":"11","volume":"20"}]), "provider":None if state == "Unavailable" else "Vike", "lastUpdatedUtc":None if state == "Unavailable" else "2026-01-01T00:00:00+00:00", "lastValidatedUtc":None if state == "Unavailable" else "2026-01-01T00:00:00+00:00", "isStale":state == "Stale", "failure":"Unavailable" if state == "Unavailable" else None})


class HistoricalBridgeTests(unittest.TestCase):
    def test_matrix_and_defaults_are_canonical(self):
        self.assertEqual(("BTC/USD", "1h", "30D"), DEFAULT_SELECTION)
        self.assertEqual(2, len(SYMBOLS)); self.assertEqual(3, len(INTERVALS)); self.assertEqual(4, len(RANGES))
        self.assertIn('"symbol":"BTC/USD"', build_request(*DEFAULT_SELECTION))
        with self.assertRaises(BridgeError): build_request("BTC", "1h", "30D")

    def test_parses_precision_stale_empty_and_unavailable(self):
        fresh = parse_response(response()); self.assertEqual("11", str(fresh.candles[0].close))
        stale = parse_response(response("Stale")); self.assertTrue(stale.is_stale)
        empty = parse_response(response("Fresh", [])); self.assertEqual((), empty.candles)
        unavailable = parse_response(response("Unavailable", [])); self.assertEqual("Unavailable", unavailable.state)

    def test_rejects_malformed_protocol(self):
        with self.assertRaises(BridgeError): parse_response("not-json")
        with self.assertRaises(BridgeError): parse_response(json.dumps({"contractVersion":"wrong"}))

    def test_rejects_inconsistent_order_timestamps_and_state(self):
        payload = json.loads(response())
        payload["candles"].append(payload["candles"][0])
        with self.assertRaises(BridgeError): parse_response(json.dumps(payload))
        payload = json.loads(response("Stale")); payload["isStale"] = False
        with self.assertRaises(BridgeError): parse_response(json.dumps(payload))
        payload = json.loads(response()); payload["lastUpdatedUtc"] = "2026-01-01T00:00:00"
        with self.assertRaises(BridgeError): parse_response(json.dumps(payload))

    def test_invocation_is_fixed_and_shell_free(self):
        calls = []
        def runner(*args, **kwargs):
            calls.append((args, kwargs)); return subprocess.CompletedProcess(args[0], 0, response(), "safe diagnostic")
        result = invoke(*DEFAULT_SELECTION, {"HistoricalMarketQuery__WorkerAssemblyPath":"/app/worker/AIQuantTradingResearch.Worker.dll"}, runner)
        self.assertEqual("Fresh", result.state); self.assertEqual(["dotnet", "/app/worker/AIQuantTradingResearch.Worker.dll"], calls[0][0][0]); self.assertFalse(calls[0][1]["shell"]); self.assertEqual(15, calls[0][1]["timeout"])


if __name__ == "__main__": unittest.main()
