import unittest
from decimal import Decimal
import inspect

import realtime_financial_visualization as ui
from historical_market_bridge import Candle, MarketResponse, DEFAULT_SELECTION, PUBLIC_UNAVAILABLE


class MarketResearchUiTests(unittest.TestCase):
    def response(self, state="Fresh", candles=True):
        rows = (Candle("2026-01-01T00:00:00+00:00", Decimal("10"), Decimal("12"), Decimal("9"), Decimal("11"), Decimal("100")),) if candles else ()
        return MarketResponse(state, "BTC/USD", "1h", "30D", rows, "Vike", "2026-01-01T00:00:00+00:00", "2026-01-01T00:00:00+00:00", state == "Stale", None)

    def test_defaults_and_selection_matrix_are_frozen(self):
        self.assertEqual(("BTC/USD", "1h", "30D"), DEFAULT_SELECTION)
        self.assertTrue(ui.market_selection_changed(None, DEFAULT_SELECTION)); self.assertFalse(ui.market_selection_changed(DEFAULT_SELECTION, DEFAULT_SELECTION))

    def test_candlestick_volume_hover_pan_and_responsive_figure(self):
        figure = ui.build_market_figure(self.response())
        self.assertEqual("candlestick", figure.data[0].type); self.assertEqual("bar", figure.data[1].type)
        self.assertIn("High", figure.data[0].hovertemplate); self.assertIn("Volume", figure.data[1].hovertemplate)
        self.assertEqual("pan", figure.layout.dragmode); self.assertFalse(figure.layout.xaxis.rangeslider.visible)

    def test_controlled_unavailable_text_is_exact(self):
        self.assertEqual("Historical market data is temporarily unavailable. Please try again later.", PUBLIC_UNAVAILABLE)

    def test_wp06_provenance_research_boundary_and_information_surface(self):
        source = inspect.getsource(ui)
        self.assertEqual("Data source: Vike • Historical OHLCV • Last updated: 2026-01-01T00:00:00+00:00 (UTC)", ui.market_provenance_caption(self.response()))
        self.assertEqual("Public historical visualization uses Vike market data.", ui.VIKE_PUBLIC_MESSAGE)
        self.assertIn("Twelve Data is retained for private/internal research", ui.TWELVE_DATA_BOUNDARY_MESSAGE)
        self.assertEqual("Research and demonstration application • No trade execution", ui.RESEARCH_FOOTER)
        for text in ("AI Quant Trading Research", "ML & Automation Studies", "Release 2.0+", "Begins Release 2.0", "Automated trading", "Not enabled", "This environment does not execute trades."):
            self.assertIn(text, source)
        self.assertNotIn("requests.", source)
        self.assertNotIn("httpx", source)

    def test_wp06_controlled_stale_empty_and_system_health_messages(self):
        self.assertEqual("Showing the most recently validated historical data.", ui.STALE_HISTORICAL_MESSAGE)
        self.assertEqual("No historical market data is available for this selection.", ui.EMPTY_HISTORICAL_MESSAGE)
        self.assertEqual("System Health data is temporarily unavailable.", ui.SYSTEM_HEALTH_UNAVAILABLE_MESSAGE)


if __name__ == "__main__": unittest.main()
