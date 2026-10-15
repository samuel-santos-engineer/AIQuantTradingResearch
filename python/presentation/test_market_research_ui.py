import unittest
from unittest.mock import patch, MagicMock
from decimal import Decimal

import realtime_financial_visualization as ui
from historical_market_bridge import Candle, MarketResponse, DEFAULT_SELECTION, PUBLIC_UNAVAILABLE

class MarketResearchUiTests(unittest.TestCase):
    def response(self, state="Fresh", candles=True):
        rows = (Candle("2026-01-01T00:00:00+00:00", Decimal("10"), Decimal("12"), Decimal("9"), Decimal("11"), Decimal("100")),) if candles else ()
        return MarketResponse(state, "BTC/USD", "1h", "30D", rows, "Vike", "2026-01-01T00:00:00+00:00", "2026-01-01T00:00:00+00:00", state == "Stale", None)

    def test_defaults_and_selection_matrix_are_frozen(self):
        self.assertEqual(("btc-like-simulation", "1h", "30D"), DEFAULT_SELECTION)
        self.assertTrue(ui.market_selection_changed(None, DEFAULT_SELECTION)); self.assertFalse(ui.market_selection_changed(DEFAULT_SELECTION, DEFAULT_SELECTION))

    def test_candlestick_volume_hover_pan_and_responsive_figure(self):
        figure = ui.build_market_figure(self.response())
        self.assertEqual("candlestick", figure.data[0].type); self.assertEqual("bar", figure.data[1].type)
        self.assertIn("High", figure.data[0].hovertemplate); self.assertIn("Volume", figure.data[1].hovertemplate)
        self.assertEqual("pan", figure.layout.dragmode); self.assertFalse(figure.layout.xaxis.rangeslider.visible)

    def test_controlled_unavailable_text_is_exact(self):
        self.assertEqual("Historical market data is temporarily unavailable. Please try again later.", PUBLIC_UNAVAILABLE)

    def test_wp06_provenance_research_boundary_and_information_surface(self):
        response = self.response()
        self.assertEqual("Data source: Vike \u2022 Historical OHLCV \u2022 Last updated: 2026-01-01T00:00:00+00:00 (UTC)", ui.market_provenance_caption(response))
        non_utc = MarketResponse(response.state, response.symbol, response.interval, response.range, response.candles, response.provider, "2026-01-01T00:00:00+02:00", response.last_validated_utc, response.is_stale, response.failure)
        self.assertIsNone(ui.market_provenance_caption(non_utc))
        non_vike = MarketResponse(response.state, response.symbol, response.interval, response.range, response.candles, "Twelve Data", response.last_updated_utc, response.last_updated_utc, response.is_stale, response.failure)
        self.assertIsNone(ui.market_provenance_caption(non_vike))
        self.assertEqual("BTC-like Market Simulation — Synthetic Historical OHLCV", ui.VIKE_PUBLIC_MESSAGE)
        self.assertIn("Twelve Data is retained for private/internal research", ui.TWELVE_DATA_BOUNDARY_MESSAGE)
        self.assertEqual("Research and demonstration application \u2022 No trade execution", ui.RESEARCH_FOOTER)
        self.assertEqual(("Market Research", "Architecture & Data Governance", "ML & Automation Studies", "System Health"), ui.NAVIGATION_OPTIONS)
        content = "\n".join(ui.ML_AUTOMATION_STUDIES_CONTENT)
        for text in ("AI Quant Trading Research", "Release 2.0+", "Begins Release 2.0", "Automated trading", "Not enabled", "This environment does not execute trades."):
            self.assertIn(text, content)

    def test_wp06_controlled_stale_empty_and_system_health_messages(self):
        self.assertEqual("Showing the most recently validated historical data.", ui.STALE_HISTORICAL_MESSAGE)
        self.assertEqual("No historical market data is available for this selection.", ui.EMPTY_HISTORICAL_MESSAGE)
        self.assertEqual("System Health data is temporarily unavailable.", ui.SYSTEM_HEALTH_UNAVAILABLE_MESSAGE)

    def test_retry_cooldown_idle_expiry_and_no_rerun_acquisition(self):
        state = {}
        self.assertTrue(ui.market_session_request(state, DEFAULT_SELECTION, now=0))
        state['wp05_market_response'] = self.response()
        self.assertFalse(ui.market_session_request(state, DEFAULT_SELECTION, now=1))
        self.assertTrue(ui.market_session_request(state, DEFAULT_SELECTION, retry=True, now=2))
        self.assertFalse(ui.market_session_request(state, DEFAULT_SELECTION, retry=True, now=11))
        self.assertTrue(ui.market_session_request(state, DEFAULT_SELECTION, retry=True, now=12))
        state['wp05_market_response'] = self.response()
        self.assertFalse(ui.market_session_request(state, DEFAULT_SELECTION, now=1812))
        self.assertNotIn('wp05_market_response', state)

    def test_health_and_ml_routes_never_read_private_handoff_or_spawn(self):
        for route in ('System Health', 'ML & Automation Studies'):
            stub = MagicMock(); stub.radio.return_value = route; stub.session_state = {'wp05_cache': object()}
            with patch.object(ui, 'st', stub), patch.object(ui, 'invoke', side_effect=AssertionError('spawn forbidden')), \
                    patch.object(ui, 'resolve_handoff_path', side_effect=AssertionError('private read forbidden')), \
                    patch.object(ui.ReadModelCache, 'refresh', side_effect=AssertionError('private read forbidden')):
                ui.render()
            if route == 'System Health': self.assertNotIn('wp05_cache', stub.session_state)

    def test_cached_private_response_is_rejected_and_navigation_expires_idle_response(self):
        self.assertFalse(ui.is_public_market_response(self.response(), DEFAULT_SELECTION))
        stub = MagicMock(); stub.radio.return_value = 'System Health'
        stub.session_state = {'wp05_market_response': self.response(), 'wp07_last_interaction': 0}
        with patch.object(ui, 'st', stub), patch.object(ui.time, 'monotonic', return_value=1800), \
                patch.object(ui, 'invoke', side_effect=AssertionError('automatic acquisition forbidden')):
            ui.render()
        self.assertNotIn('wp05_market_response', stub.session_state)

if __name__ == "__main__": unittest.main()
