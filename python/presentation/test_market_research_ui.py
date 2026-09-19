import unittest
from decimal import Decimal

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


if __name__ == "__main__": unittest.main()
