"""Real Streamlit script runs; never invokes the Worker or public generation."""
from pathlib import Path
import unittest
from unittest.mock import patch

from streamlit.testing.v1 import AppTest

import historical_market_bridge as bridge


class PublicShellTests(unittest.TestCase):
    def app(self):
        return AppTest.from_file(str(Path(__file__).with_name("realtime_financial_visualization.py")), default_timeout=30)

    def test_unqualified_shell_renders_and_never_calls_bridge_or_retains_old_response(self):
        app = self.app()
        app.session_state["wp05_market_response"] = "untrusted old chart"
        with patch.object(bridge, "invoke", side_effect=AssertionError("Worker invocation forbidden")):
            app.run()
        self.assertEqual([], list(app.exception))
        self.assertTrue(any("BTC-like Market Simulation" in item.value for item in app.caption))
        self.assertTrue(any("not enabled" in item.value for item in app.info))
        self.assertTrue(any("chart unavailable" in item.value for item in app.warning))
        self.assertEqual(3, len(app.selectbox))
        self.assertTrue(all(item.disabled for item in app.selectbox))
        self.assertNotIn("wp05_market_response", app.session_state)
        self.assertFalse(bridge.PUBLIC_RUNTIME_VERIFIED)

    def test_architecture_navigation_and_other_pages_are_truthful_and_render_without_error(self):
        app = self.app().run()
        for page in ("Architecture & Data Governance", "ML & Automation Studies", "System Health", "Market Research"):
            app.radio[0].set_value(page).run()
            self.assertEqual([], list(app.exception), page)
            if page == "Architecture & Data Governance":
                self.assertEqual(page, app.title[0].value)
                self.assertTrue(any("private research only" in item.value for item in app.info))
                self.assertEqual(1, len(app.table))
            if page == "System Health":
                self.assertTrue(any("unavailable" in item.value for item in app.info))


if __name__ == "__main__":
    unittest.main()
