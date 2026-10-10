import json
import subprocess
import unittest
import hashlib
from datetime import datetime, timedelta, timezone
from historical_market_bridge import *

QUALIFIED = {i: "a" * 64 for i in INTERVALS}  # arbitrary test identity, not release goldens

def response(interval="1d", range_value="1D"):
    hours = {"1h": 1, "4h": 4, "1d": 24}[interval]
    start = datetime(2026, 10, 1, tzinfo=timezone.utc) - timedelta(days=int(range_value[:-1]))
    identity = {"canonicalVersion": "aiq-candles-v2", "instrument": SYMBOLS[0], "marketType": "simulation", "source": "independent-synthetic", "status": "simulated", "venue": None, "base": None, "quote": None, "settlement": None, "priceUnit": "illustrative-price-unit", "volumeUnit": "illustrative-activity-unit", "generatorVersion": "aiq-synthetic-ohlcv-v1", "seed": 114, "coverageStartUtc": "2025-10-26T00:00:00Z", "coverageEndExclusiveUtc": "2026-10-01T00:00:00Z", "datasetFingerprint": "a" * 64, "windowFingerprint": "b" * 64, "calculationVersion": "aiq-ta-v1"}
    candles = [{"openTimeUtc": (start + timedelta(hours=i * hours)).strftime("%Y-%m-%dT%H:%M:%SZ"), "open": "10.0000", "high": "12.0000", "low": "9.0000", "close": "11.0000", "volume": "20.000"} for i in range(24 * int(range_value[:-1]) // hours)]
    # Independently known constant-close 11: SMA/EMA/bands 11, RSI 50, MACD/signal/histogram 0.
    parameters = {"version": "aiq-ta-v1", "scale": 12, "smaPeriod": 20, "emaPeriod": 20, "rsiPeriod": 14,
                  "macdFastPeriod": 12, "macdSlowPeriod": 26, "macdSignalPeriod": 9, "bollingerPeriod": 20, "bollingerMultiplier": 2}
    rows = [{"openTimeUtc": c["openTimeUtc"], "sma20": "11.000000000000", "ema20": "11.000000000000", "rsi14": "50.000000000000",
             "macd": "0.000000000000", "macdSignal": "0.000000000000", "macdHistogram": "0.000000000000", "bollingerLower": "11.000000000000",
             "bollingerMiddle": "11.000000000000", "bollingerUpper": "11.000000000000"} for c in candles]
    analytics = {"calculationVersion": "aiq-ta-v1", "parameters": parameters, "context": "public-synthetic", "source": "independent-synthetic",
                 "instrument": SYMBOLS[0], "interval": interval, "range": range_value, "canonicalVersion": "aiq-candles-v2",
                 "datasetFingerprint": "a" * 64, "windowFingerprint": "b" * 64,
                 "calculationStartUtc": (start-timedelta(hours=250*hours)).strftime("%Y-%m-%dT%H:%M:%SZ"),
                 "visibleStartUtc": start.strftime("%Y-%m-%dT%H:%M:%SZ"), "warmup": 250, "calculationCount": len(candles)+250, "rows": rows}
    return {"contractVersion": CONTRACT_VERSION, "state": "Available", "instrument": SYMBOLS[0], "interval": interval, "range": range_value, "candles": candles, "provenance": identity, "generatedAtUtc": "2026-10-10T00:00:00Z", "failure": None, "analytics": analytics}

class HistoricalBridgeTests(unittest.TestCase):
    def parse(self, payload):
        return parse_response(json.dumps(payload), **qualification())

    def test_all_twelve_windows_and_one_visible_daily_candle(self):
        counts = (24, 168, 720, 2160, 6, 42, 180, 540, 1, 7, 30, 90)
        index = 0
        for interval in INTERVALS:
            for range_value in RANGES:
                with self.subTest(interval=interval, range=range_value):
                    value = self.parse(response(interval, range_value))
                    self.assertEqual(counts[index], len(value.candles)); index += 1
                    self.assertEqual(len(value.candles), len(value.analytics.rows))
                    self.assertEqual(Decimal('11'), value.analytics.rows[0].sma20)
                    self.assertEqual(Decimal('50'), value.analytics.rows[0].rsi14)
                    self.assertEqual(value.candles[0].open_time_utc, value.analytics.rows[0].open_time_utc)
                    self.assertFalse(value.is_stale)
        self.assertEqual(("btc-like-simulation", "1h", "30D"), DEFAULT_SELECTION)

    def test_requests_reject_private_unknown_duplicate_multiple_values_and_utf8(self):
        valid = build_request(*DEFAULT_SELECTION)
        self.assertEqual(SYMBOLS[0], validate_request(valid)["instrument"])
        for bad in (valid[:-1] + ',"source":"Vike"}', valid[:-1] + ',"context":"private-research"}', valid[:-1] + ',"deadlineSeconds":10}', valid + '{}', b'\xc0\xaf', b'\xed\xa0\x80', valid.replace('btc-like-simulation', 'BTC/USD'), valid.replace('aiq-historical-query-v2', 'aiq-historical-query-v1')):
            with self.subTest(bad=bad), self.assertRaises(BridgeError): validate_request(bad)
        with self.assertRaises(BridgeError): build_request(SYMBOLS[0], '1h', '30D', True)
        with self.assertRaises(BridgeError): build_request('BTC/USD', '1h', '30D')

    def test_request_exact_byte_boundary_and_flood(self):
        valid = build_request(*DEFAULT_SELECTION)
        validate_request(valid.ljust(4096))
        with self.assertRaises(BridgeError): validate_request(valid.ljust(4097))
        with self.assertRaises(BridgeError): validate_request(b' ' * 1000000)

    def test_metadata_and_fingerprints_fail_closed(self):
        for field, bad in {'source': 'Vike', 'status': 'observed', 'marketType': 'spot', 'instrument': 'BTC/USD', 'venue': 'exchange', 'base': 'BTC', 'quote': 'USD', 'settlement': 'USD', 'priceUnit': 'USD', 'volumeUnit': 'BTC', 'seed': True, 'generatorVersion': 'other', 'canonicalVersion': 'v1', 'calculationVersion': None, 'datasetFingerprint': 'b' * 64, 'windowFingerprint': 'bad'}.items():
            payload = response(); payload['provenance'][field] = bad
            with self.subTest(field=field), self.assertRaises(BridgeError): self.parse(payload)
        with self.assertRaises(BridgeError): parse_response(json.dumps(response()))

    def test_decimal_nonfinite_overflow_precision_and_grid_fail_closed(self):
        for bad in ('NaN', 'Infinity', '1e4', '-1.0000', '1.00001', '79228162514264337593543950336.0000'):
            payload = response(); payload['candles'][0]['open'] = bad
            with self.subTest(value=bad), self.assertRaises(BridgeError): self.parse(payload)
        for mutate in (lambda p: p['candles'].append(p['candles'][0]), lambda p: p['candles'].clear(), lambda p: p['candles'][0].update(openTimeUtc='2026-09-30T01:00:00Z'), lambda p: p['candles'][0].update(low='13.0000'), lambda p: p['candles'][0].update(provider='Vike')):
            payload = response(); mutate(payload)
            with self.assertRaises(BridgeError): self.parse(payload)

    def test_response_duplicate_unknown_multiple_values_and_legacy_rejected(self):
        valid = json.dumps(response())
        for bad in (valid[:-1] + ',"state":"Available"}', valid + '{}', valid.replace('aiq-historical-query-v2', 'aiq-historical-query-v1'), valid[:-1] + ',"provider":"Vike"}', b'\xf4\x90\x80\x80', json.dumps({'secret': 'private'})):
            with self.assertRaises(BridgeError): parse_response(bad, qualified_dataset_fingerprints=QUALIFIED)

    def test_controlled_failure_has_no_private_metadata(self):
        payload = {key: None for key in ('instrument', 'interval', 'range', 'candles', 'provenance', 'generatedAtUtc', 'analytics')}
        payload.update(contractVersion=CONTRACT_VERSION, state='Unavailable', failure='Timeout')
        self.assertEqual('Timeout', self.parse(payload).failure)
        payload['failure'] = '/private/provider/secret'
        with self.assertRaises(BridgeError): self.parse(payload)

    def test_invocation_rejects_selection_before_spawn_and_checks_response_binding(self):
        calls = []
        def runner(*args, **kwargs):
            calls.append((args, kwargs)); return subprocess.CompletedProcess(args[0], 0, json.dumps(response()), '')
        env = {'HistoricalMarketQuery__WorkerAssemblyPath': '/app/worker/AIQuantTradingResearch.Worker.dll'}
        value = invoke(SYMBOLS[0], '1d', '1D', env, runner, **qualification())
        self.assertEqual(1, len(value.candles)); self.assertFalse(calls[0][1]['shell'])
        with self.assertRaises(BridgeError): invoke('BTC/USD', '1h', '30D', env, runner)
        self.assertEqual(1, len(calls))
        with self.assertRaises(BridgeError): invoke(SYMBOLS[0], '1h', '30D', env, runner, **qualification())

    def test_rehashed_or_tampered_visible_data_cannot_self_qualify(self):
        payload = response(); payload['candles'][0]['close'] = '11.5000'
        with self.assertRaises(BridgeError): self.parse(payload)
        payload = response(); payload['provenance']['windowFingerprint'] = 'c' * 64
        with self.assertRaises(BridgeError): self.parse(payload)

    def test_analytics_identity_and_parameters_fail_closed(self):
        bad_metadata = {'context': 'private-research', 'source': 'Vike', 'instrument': 'BTC/USD', 'interval': '4h', 'range': '90D',
                        'calculationVersion': 'v2', 'canonicalVersion': 'v1', 'datasetFingerprint': 'c'*64, 'windowFingerprint': 'c'*64,
                        'warmup': True, 'calculationCount': 250, 'calculationStartUtc': '2026-01-01T00:00:00Z',
                        'visibleStartUtc': '2026-01-01T00:00:00Z'}
        for field, bad in bad_metadata.items():
            payload = response(); payload['analytics'][field] = bad
            with self.subTest(field=field), self.assertRaises(BridgeError): self.parse(payload)
        for field in response()['analytics']['parameters']:
            payload = response(); payload['analytics']['parameters'][field] = True
            with self.subTest(parameter=field), self.assertRaises(BridgeError): self.parse(payload)
        for target in ('analytics', 'parameters', 'row'):
            payload = response(); obj = payload['analytics'] if target == 'analytics' else payload['analytics']['parameters'] if target == 'parameters' else payload['analytics']['rows'][0]
            obj['unknown'] = 'private'
            with self.assertRaises(BridgeError): self.parse(payload)
        payload = response(); payload.pop('analytics')
        with self.assertRaises(BridgeError): self.parse(payload)

    def test_analytics_decimal_alignment_and_semantics_fail_closed(self):
        for field in ('sma20', 'ema20', 'rsi14', 'macd', 'macdSignal', 'macdHistogram', 'bollingerLower', 'bollingerMiddle', 'bollingerUpper'):
            for bad in (None, True, 11.0, 'NaN', 'Infinity', '1e1', '1.000', '01.000000000000', '-0.000000000000', '3000000001.000000000000'):
                payload = response(); payload['analytics']['rows'][0][field] = bad
                with self.subTest(field=field, bad=bad), self.assertRaises(BridgeError): self.parse(payload)
        for field, bad in {'rsi14': '100.000000000001', 'sma20': '-1.000000000000', 'macdHistogram': '1.000000000000',
                           'bollingerLower': '12.000000000000', 'bollingerMiddle': '10.000000000000', 'bollingerUpper': '12.000000000000',
                           'openTimeUtc': '2026-01-01T00:00:00Z'}.items():
            payload = response(); payload['analytics']['rows'][0][field] = bad
            with self.assertRaises(BridgeError): self.parse(payload)
        for bad_rows in ([], response()['analytics']['rows'] * 251, {}, None):
            payload = response(); payload['analytics']['rows'] = bad_rows
            with self.assertRaises(BridgeError): self.parse(payload)

    def test_signed_bands_and_macd_are_decimal_and_immutable(self):
        payload = response(); row = payload['analytics']['rows'][0]
        row.update(bollingerLower='-1.000000000000', bollingerUpper='23.000000000000', macd='-7.000000000000',
                   macdSignal='-6.500000000000', macdHistogram='-0.500000000000')
        value = self.parse(payload)
        self.assertEqual(Decimal('-1'), value.analytics.rows[0].bollinger_lower)
        self.assertEqual(Decimal('-0.5'), value.analytics.rows[0].macd_histogram)
        with self.assertRaises(TypeError): value.analytics.metadata['context'] = 'private-research'
        with self.assertRaises(TypeError): value.analytics.metadata['parameters']['scale'] = 4

    def test_duplicate_analytics_fields_and_failure_payload_are_rejected(self):
        text = json.dumps(response()).replace('"sma20": "11.000000000000"', '"sma20": "11.000000000000", "sma20": "11.000000000000"')
        with self.assertRaises(BridgeError): parse_response(text, **qualification())
        payload = {key: None for key in ('instrument', 'interval', 'range', 'candles', 'provenance', 'generatedAtUtc', 'analytics')}
        payload.update(contractVersion=CONTRACT_VERSION, state='Unavailable', failure='Timeout', analytics=response()['analytics'])
        with self.assertRaises(BridgeError): self.parse(payload)

def qualification():
    # Independent expected constant-row transport bytes; no synthetic algorithm/goldens.
    visible = {}
    windows = {}
    for interval in INTERVALS:
        for range_value in RANGES:
            data = response(interval, range_value)
            rows = ''.join(row['openTimeUtc'] + ',10.0000,12.0000,9.0000,11.0000,20.000\n' for row in data['candles'])
            visible[(interval, range_value)] = hashlib.sha256(rows.encode('ascii')).hexdigest()
            windows[(interval, range_value)] = 'b' * 64
    return dict(qualified_dataset_fingerprints=QUALIFIED, qualified_window_fingerprints=windows, qualified_visible_fingerprints=visible)

if __name__ == '__main__': unittest.main()
