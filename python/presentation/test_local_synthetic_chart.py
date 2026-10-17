import unittest
from decimal import Decimal
from unittest.mock import Mock, patch

import local_synthetic_chart as adapter
import realtime_financial_visualization as ui
from historical_market_bridge import Analytics, BridgeError, Candle, IndicatorRow, MarketResponse


class LocalChartTests(unittest.TestCase):
    def test_gate_requires_explicit_windows_loopback_development_and_rejects_azure(self):
        env = {"AIQ_LOCAL_CHART": "1", "DOTNET_ENVIRONMENT": "Development"}
        self.assertTrue(adapter.enabled("127.0.0.1", environment=env, platform="nt"))
        self.assertFalse(adapter.enabled("0.0.0.0", environment=env, platform="nt"))
        self.assertFalse(adapter.enabled("127.0.0.1", environment=env, platform="posix"))
        for changed in ({}, dict(env, DOTNET_ENVIRONMENT="Production"), dict(env, WEBSITE_INSTANCE_ID="azure")):
            self.assertFalse(adapter.enabled("127.0.0.1", environment=changed, platform="nt"))

    def test_disabled_adapter_never_opens_a_connection(self):
        with patch.object(adapter, "enabled", return_value=False), patch.object(adapter, "build_opener") as opener:
            with self.assertRaises(BridgeError):
                adapter.load("btc-like-simulation", "1h", "30D", server_address="127.0.0.1")
            opener.assert_not_called()

    def test_parser_rejects_missing_fields_and_malformed_json(self):
        for payload in (b"{}", b'{"label":1,"label":2}', b"not-json"):
            with self.subTest(payload=payload), self.assertRaises(BridgeError):
                adapter.parse(payload, "1h", "30D")

    def test_overlays_use_only_supplied_csharp_values_and_aligned_timestamps(self):
        times = ("2026-09-30T00:00:00Z", "2026-09-30T01:00:00Z")
        candles = tuple(Candle(t, Decimal(10), Decimal(12), Decimal(9), Decimal(11), Decimal(100)) for t in times)
        rows = tuple(IndicatorRow(t, Decimal(10), Decimal("10.25"), None, None, None, None, Decimal(-1), Decimal(10), Decimal(21)) for t in times)
        response = MarketResponse("Available", "btc-like-simulation", "1h", "1D", candles, "independent-synthetic", None, None, False, None, {}, Analytics({}, rows))
        off = ui.build_market_figure(response)
        on = ui.build_market_figure(response, True, True, True)
        self.assertEqual(off.data[0].to_plotly_json(), on.data[0].to_plotly_json())
        self.assertEqual(off.data[1].to_plotly_json(), on.data[1].to_plotly_json())
        for index, field in ((2, "sma20"), (3, "ema20"), (4, "bollinger_lower"), (5, "bollinger_middle"), (6, "bollinger_upper")):
            self.assertFalse(off.data[index].visible)
            self.assertTrue(on.data[index].visible)
            self.assertEqual(list(times), list(on.data[index].x))
            self.assertEqual([float(getattr(r, field)) for r in rows], list(on.data[index].y))

    def test_signed_indicator_transport_rejects_missing_nonfinite_and_wrong_scale(self):
        self.assertEqual(Decimal("-1.000000000000"), adapter.indicator_decimal("-1.000000000000"))
        for value in (None, "NaN", "Infinity", "1.0", 1.0, "3000000001.000000000000"):
            with self.subTest(value=value), self.assertRaises(BridgeError):
                adapter.indicator_decimal(value)

    def test_health_validates_current_bundle_without_private_handoff(self):
        streamlit = Mock(session_state={"wp05_cache": "stale"})
        streamlit.get_option.return_value = "0.0.0.0"
        with patch.object(ui, "st", streamlit), patch.object(ui, "immutable_chart_enabled", return_value=True), \
             patch.object(ui, "load_immutable_chart") as load, patch.object(ui, "resolve_handoff_path") as private:
            ui.render_system_health()
            load.assert_called_once_with("btc-like-simulation", "1h", "1D", server_address="0.0.0.0")
            private.assert_not_called()
        streamlit.success.assert_called_once()
        streamlit.warning.assert_not_called()
        self.assertNotIn("wp05_cache", streamlit.session_state)

    def test_health_reports_failed_validation_without_claiming_success(self):
        streamlit = Mock(session_state={})
        with patch.object(ui, "st", streamlit), patch.object(ui, "immutable_chart_enabled", return_value=True), \
             patch.object(ui, "load_immutable_chart", side_effect=BridgeError("ImmutableCandidateUnavailable")):
            ui.render_system_health()
        streamlit.warning.assert_called_once()
        streamlit.success.assert_not_called()

    def test_disabled_health_does_not_read_dataset_or_claim_success(self):
        streamlit = Mock(session_state={})
        with patch.object(ui, "st", streamlit), patch.object(ui, "immutable_chart_enabled", return_value=False), \
             patch.object(ui, "load_immutable_chart") as load:
            ui.render_system_health()
            load.assert_not_called()
        streamlit.success.assert_not_called()
        streamlit.warning.assert_not_called()

    def test_toggle_changes_only_supplied_sma_visibility_with_stable_chart_identity(self):
        # UI-only contract test; actual C# values and complete selections are tested in .NET/HTTP.
        times = ("2026-09-30T00:00:00Z", "2026-09-30T01:00:00Z")
        candles = tuple(Candle(t, Decimal(10), Decimal(12), Decimal(9), Decimal(11), Decimal(100)) for t in times)
        indicators = tuple(IndicatorRow(t, Decimal("10.123456789012"), *([None] * 8)) for t in times)
        response = MarketResponse("Available", "btc-like-simulation", "1h", "30D", candles,
                                  "independent-synthetic", None, None, False, None, {}, Analytics({}, indicators))
        off, on = ui.build_market_figure(response, False), ui.build_market_figure(response, True)
        self.assertEqual(off.data[0].to_plotly_json(), on.data[0].to_plotly_json())
        self.assertEqual(off.data[1].to_plotly_json(), on.data[1].to_plotly_json())
        self.assertEqual(("candlestick", "bar", "scatter", "scatter", "scatter", "scatter", "scatter"), tuple(t.type for t in on.data))
        self.assertFalse(off.data[2].visible); self.assertTrue(on.data[2].visible)
        self.assertEqual([float(r.sma20) for r in indicators], list(on.data[2].y))
        self.assertEqual(off.layout.uirevision, on.layout.uirevision)
        self.assertEqual("pan", on.layout.dragmode)
        self.assertFalse(on.layout.xaxis.rangeslider.visible)
        self.assertIn("High", on.data[0].hovertemplate)
        self.assertEqual("Volume", on.layout.yaxis2.title.text)
        # Figure-owned labels also survive Plotly's native PNG export, unlike page captions.
        self.assertIn(ui.SYNTHETIC_DISCLOSURE, on.layout.title.text)
        self.assertIn("BTC-like simulation", on.layout.title.text)
        self.assertEqual(off.layout.title.text, on.layout.title.text)
        for trace in on.data:
            self.assertIn(ui.SYNTHETIC_DISCLOSURE, trace.hovertemplate)
            self.assertIn("Simulation time (UTC)", trace.hovertemplate)
        self.assertEqual([float(c.open) for c in candles], list(on.data[0].open))
        self.assertEqual([float(c.volume) for c in candles], list(on.data[1].y))



if __name__ == "__main__":
    unittest.main()
