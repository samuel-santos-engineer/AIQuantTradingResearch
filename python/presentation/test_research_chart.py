"""Offline public DTO fixtures only. Never seeds production, acquires prices or calls a provider."""
import copy
from datetime import datetime, timedelta, timezone
import json
import os
from pathlib import Path
import unittest
from unittest.mock import patch

import research_chart as chart
from research_snapshot_client import ResearchSnapshotClient, SnapshotUnavailable

TEST_KEY = "offline-ui-key-not-a-real-credential"
GENERATION = "22222222-2222-4222-8222-222222222222"
SEGMENT = "33333333-3333-4333-8333-333333333333"
SECOND_SEGMENT = "44444444-4444-4444-8444-444444444444"


def fixture(instrument="BTC/USD", count=401, gap=False):
    now = datetime(2026, 10, 23, 12, 1, 15, tzinfo=timezone.utc)
    end = datetime(2026, 10, 23, 12, 0, tzinfo=timezone.utc)
    rows, analytics, classes, events, unavailable, gaps = [], [], [], [], [], []
    for i in range(count):
        index = i + 1
        segment = SEGMENT
        t = end - timedelta(minutes=count - i - 1)
        if gap and count == 2:
            index = 1
            segment = SEGMENT if i == 0 else SECOND_SEGMENT
            if i == 0:
                t -= timedelta(minutes=1)
        timestamp = t.strftime("%Y-%m-%dT%H:%M:00Z")
        rows.append(dict(time=timestamp, segment=segment, segmentIndex=str(index), open="91.602434113300", high="92.518458454433",
                         low="90.686409772167", close="91.602434113300", volume="1.000000000000"))
        values = {"sma20": (20, "91.602434113300"), "ema20": (20, "91.602434113300"), "bollingerLower": (20, "91.602434113300"),
                  "bollingerMiddle": (20, "91.602434113300"), "bollingerUpper": (20, "91.602434113300"), "rsi14": (15, "50.000000000000"),
                  "macd": (26, "0.000000000000"), "macdSignal": (34, "0.000000000000"), "macdHistogram": (34, "0.000000000000"),
                  "atr14": (14, "1.832048682266"), "normalizedAtr14": (14, "0.020000000000"), "bollingerWidth": (20, "0.000000000000"), "emaSlope5": (25, "0.000000000000")}
        analytics.append(dict(time=timestamp, **{k: v if index >= first else None for k, (first, v) in values.items()}))
        valid = index >= 401
        classes.append(dict(time=timestamp, segment=segment, validity="Valid" if valid else "Unavailable", trendScore=0 if valid else None,
                            trend="Neutral" if valid else None, regime="Undetermined" if valid else None))
        if index == 1:
            events.append(dict(time=timestamp, segment=segment, type="SegmentStart", previousTrend=None, trend=None, regime=None))
        if index == 401:
            events.append(dict(time=timestamp, segment=segment, type="TrendTransition", previousTrend=None, trend="Neutral", regime="Undetermined"))
        if not valid:
            finish = (t + timedelta(minutes=1)).strftime("%Y-%m-%dT%H:%M:00Z")
            if unavailable and unavailable[-1]["end"] == timestamp:
                unavailable[-1]["end"] = finish
            else:
                unavailable.append(dict(start=timestamp, end=finish))
    if gap and count == 2:
        start = (datetime.fromisoformat(rows[0]["time"].replace("Z", "+00:00")) + timedelta(minutes=1)).strftime("%Y-%m-%dT%H:%M:00Z")
        gaps.append(dict(start=start, end=rows[1]["time"]))
    obs = now.strftime("%Y-%m-%dT%H:%M:%S.%fZ")
    return dict(schemaVersion="1.0", type="full", instrument=instrument, requestEpoch="1", observedAt=obs,
                identity=dict(generation=GENERATION, version="1" if rows else "0", displayDate="2026-10-23", transformEpoch="1"),
                contract=dict(researchAlgorithm="aiq-r115-research-v1", transformAlgorithm="aiq-r115-transform-v1", intervalSeconds=60),
                status=dict(observedAt=obs, research="Ready" if count >= 401 else "Recalibrating" if rows else "Unavailable", freshness="Current" if rows else "Unavailable",
                            integrity="Protected", classificationValidity="Valid" if count >= 401 else "Unavailable", lastCompletedCandleAt=rows[-1]["time"] if rows else None,
                            lastSuccessfulProviderPollAt=obs if rows else None, lastCommitAt=obs if rows else None, researchAsOfCandleAt=rows[-1]["time"] if count >= 401 else None),
                candles=rows, analytics=analytics, classifications=classes, events=events, researchUnavailableIntervals=unavailable, marketDataGapIntervals=gaps)


class FixtureClient(ResearchSnapshotClient):
    """Test transport only; whole-response acceptance is the real merged WP05 client."""
    def __init__(self, count=401, gap=False):
        super().__init__("https://offline.invalid", TEST_KEY)
        self.data = {instrument: fixture(instrument, count, gap) for instrument in ("BTC/USD", "ETH/USD")}
        self.calls = []
        self.fail = set()

    def fetch(self, instrument):
        self.calls.append(instrument)
        request = self.begin(instrument)
        epoch = int(request["requestEpoch"])
        if instrument in self.fail:
            return self.accept(b'{', instrument, epoch)
        data = copy.deepcopy(self.data[instrument])
        data["requestEpoch"] = str(epoch)
        if request["knownIdentity"] == data["identity"]:
            data["type"] = "unchanged"
            for name in ("candles", "analytics", "classifications", "events", "researchUnavailableIntervals", "marketDataGapIntervals"):
                del data[name]
        return self.accept(json.dumps(data).encode(), instrument, epoch)


class ProjectionTests(unittest.TestCase):
    def snapshot(self, count=401, gap=False, instrument="BTC/USD"):
        return FixtureClient(count, gap).fetch(instrument)

    def test_both_instruments_exact_candles_utc_volume_and_disclosure(self):
        for instrument in ("BTC/USD", "ETH/USD"):
            with self.subTest(instrument=instrument):
                data = self.snapshot(instrument=instrument)
                figure = chart.build_figure(data, chart.InstrumentView())
                candle = next(t for t in figure["data"] if t["type"] == "candlestick")
                self.assertEqual(401, len(candle["x"]))
                self.assertEqual(data["candles"][0]["time"], candle["x"][0])
                self.assertAlmostEqual(91.6024341133, candle["open"][0])
                self.assertIn("not actual quotes", candle["text"][0])
                self.assertIn("91.602434113300", candle["text"][0])
                self.assertIn(instrument, figure["layout"]["title"]["text"])
                self.assertIn("Volume", figure["data"][1]["name"])
                self.assertEqual("date", figure["layout"]["xaxis"]["type"])

    def test_overlays_and_panels_use_supplied_transformed_values_with_nulls(self):
        data = self.snapshot()
        state = chart.InstrumentView(overlays=chart.OVERLAYS, panel="MACD")
        fig = chart.build_figure(data, state)
        sma = next(t for t in fig["data"] if t["name"] == "SMA20 (transformed)")
        self.assertEqual([None] * 19, sma["y"][:19])
        self.assertAlmostEqual(float(data["analytics"][19]["sma20"]), sma["y"][19])
        macd = next(t for t in fig["data"] if t["name"] == "MACD (transformed)")
        self.assertEqual([None] * 25, macd["y"][:25])
        state.panel = "RSI14"
        rsi = next(t for t in chart.build_figure(data, state)["data"] if t["name"].startswith("RSI"))
        self.assertEqual([None] * 14, rsi["y"][:14])
        self.assertEqual(50, rsi["y"][14])

    def test_gap_is_utc_space_not_a_candle_or_connected_overlay(self):
        data = self.snapshot(2, True)
        fig = chart.build_figure(data, chart.InstrumentView())
        candle = next(t for t in fig["data"] if t["type"] == "candlestick")
        self.assertEqual(2, len(candle["x"]))
        sma = next(t for t in fig["data"] if t["name"].startswith("SMA"))
        self.assertEqual([None, None, None], sma["y"])
        self.assertFalse(sma["connectgaps"])
        self.assertEqual(data["marketDataGapIntervals"][0]["start"], fig["layout"]["shapes"][0]["x0"])

    def test_retained_presets_change_viewport_without_slicing_or_fetching(self):
        data = self.snapshot(1000)
        for preset in chart.PRESETS:
            fig = chart.build_figure(data, chart.InstrumentView(preset=preset))
            self.assertEqual(data["candles"][-preset]["time"], fig["layout"]["xaxis"]["range"][0])
            self.assertEqual(1000, len(fig["data"][0]["x"]))

    def test_live_reset_changes_plotly_ui_revision_but_refresh_retains_it(self):
        data = self.snapshot()
        state = chart.InstrumentView()
        initial = chart.component_data(data, state, "session")
        state.view.x = (data["candles"][200]["time"], data["candles"][300]["time"])
        state.view.y = (91.0, 92.0)
        state.view.live = False
        historical = chart.component_data(data, state, "session")
        self.assertEqual(initial["figure"]["layout"]["uirevision"], historical["figure"]["layout"]["uirevision"])
        state.view.command()  # Ordinary refresh must preserve Plotly's user viewport.
        refreshed = chart.component_data(data, state, "session")
        self.assertEqual(historical["figure"]["layout"]["uirevision"], refreshed["figure"]["layout"]["uirevision"])
        state.view.return_live()
        live = chart.component_data(data, state, "session")
        self.assertNotEqual(refreshed["figure"]["layout"]["uirevision"], live["figure"]["layout"]["uirevision"])
        self.assertIsNone(live["viewport"]["x"])
        self.assertIsNone(live["viewport"]["y"])
        self.assertEqual(initial["figure"]["layout"]["xaxis"]["range"], live["figure"]["layout"]["xaxis"]["range"])
        self.assertEqual(initial["figure"]["data"], live["figure"]["data"])

    def test_classification_400_401_and_only_transition_events(self):
        before, after = self.snapshot(400), self.snapshot(401)
        self.assertEqual("Unavailable", before["classifications"][-1]["validity"])
        self.assertEqual("Valid", after["classifications"][-1]["validity"])
        state = chart.InstrumentView(events=True)
        figure = chart.build_figure(after, state)
        markers = [t for t in figure["data"] if t.get("mode") == "markers"]
        self.assertEqual(2, sum(len(t["x"]) for t in markers))
        self.assertIn("Undetermined", str(markers))

    def test_empty_has_no_fabricated_candles(self):
        figure = chart.build_figure(self.snapshot(0), chart.InstrumentView())
        self.assertFalse(any(t["type"] == "candlestick" for t in figure["data"]))

    def test_render_payload_has_no_service_key_or_private_client(self):
        data = chart.component_data(self.snapshot(), chart.InstrumentView(), "public-session-nonce")
        serialized = json.dumps(data)
        for secret in (TEST_KEY, "_key", "checkpoint", "keyEpoch", "research.sqlite", "X-API-Key"):
            self.assertNotIn(secret, serialized)
        self.assertEqual(4, len(data["identity"]))

    def test_unchanged_status_does_not_change_render_token(self):
        client = FixtureClient()
        state = chart.InstrumentView()
        first = chart.acquire(client, "BTC/USD", state, now=0)
        a = chart.component_data(first, state, "session")
        second = chart.acquire(client, "BTC/USD", state, now=60)
        b = chart.component_data(second, state, "session")
        self.assertEqual("unchanged", second["type"])
        self.assertEqual(a["renderToken"], b["renderToken"])
        self.assertEqual(a["figure"], b["figure"])

    def test_failure_preserves_last_good_instrument_and_sync(self):
        client = FixtureClient()
        state = chart.InstrumentView()
        first = chart.acquire(client, "BTC/USD", state, now=0)
        synced = client.connection_state("BTC/USD")[1]
        client.fail.add("BTC/USD")
        last = chart.acquire(client, "BTC/USD", state, now=60)
        self.assertEqual(first, last)
        self.assertEqual(("Disconnected", synced), client.connection_state("BTC/USD"))
        other = chart.acquire(client, "ETH/USD", chart.InstrumentView(), now=61)
        self.assertEqual("ETH/USD", other["instrument"])
        self.assertEqual("BTC/USD", client.snapshot("BTC/USD")["instrument"])

    def test_normal_cadence_is_bounded_and_explicit_refresh_is_allowed(self):
        client = FixtureClient()
        state = chart.InstrumentView()
        chart.acquire(client, "BTC/USD", state, now=0)
        chart.acquire(client, "BTC/USD", state, now=59)
        self.assertEqual(1, len(client.calls))
        chart.acquire(client, "BTC/USD", state, now=60)
        self.assertEqual(2, len(client.calls))
        chart.acquire(client, "BTC/USD", state, now=61, force=True)
        self.assertEqual(3, len(client.calls))

    def test_component_uses_local_assets_and_hardened_lifecycle(self):
        source = Path(chart.__file__).with_name("research_chart_component.js").read_text(encoding="utf-8")
        for guard in ("model.ticket===ticket", "graph.isConnected", "clearTimeout", "resetEpoch", "Plotly.purge", "sessionStorage", "setStateValue", "commandEpoch"):
            self.assertIn(guard, source)
        for remote in ("fetch(", "XMLHttpRequest", "cdn.plot.ly", "document.querySelector", "eval("):
            self.assertNotIn(remote, source)


class ViewportTests(unittest.TestCase):
    def setUp(self):
        self.data = FixtureClient().fetch("BTC/USD")
        self.state = chart.InstrumentView()
        self.state.reconcile(self.data)

    def event(self, **changes):
        event = dict(kind="manual", instrument="BTC/USD", identity=dict(self.data["identity"]), mount=self.state.view.mount,
                    revision=1, commandEpoch=self.state.view.command_epoch, x=["2026-10-23T10:00:00Z", "2026-10-23T11:00:00Z"],
                    y=[90, 92], y2=[0, 5], y3=None)
        event.update(changes)
        return event

    def test_manual_ranges_preserved_during_correction_and_unchanged(self):
        event = self.event()
        self.assertTrue(self.state.view.accept(event, "BTC/USD", self.data["identity"]))
        view = copy.deepcopy(self.state.view)
        self.state.reconcile(self.data)
        self.assertEqual(view, self.state.view)
        corrected = dict(self.data, identity=dict(self.data["identity"], version="2"))
        self.state.reconcile(corrected)
        self.assertEqual(view.x, self.state.view.x)
        self.assertEqual(view.y, self.state.view.y)
        self.assertFalse(self.state.view.live)
        self.assertGreater(self.state.view.command_epoch, view.command_epoch)

    def test_day_epoch_and_generation_rebuild_preserve_eligible_ranges(self):
        self.state.view.accept(self.event(), "BTC/USD", self.data["identity"])
        expected = self.state.view.x
        mount = self.state.view.mount
        changed = dict(self.data, identity=dict(self.data["identity"], displayDate="2026-10-24", transformEpoch="2"))
        self.state.reconcile(changed)
        self.assertEqual(expected, self.state.view.x)
        self.assertGreater(self.state.view.mount, mount)
        changed["identity"]["generation"] = SECOND_SEGMENT
        self.state.reconcile(changed)
        self.assertIn("generation changed", self.state.notice)

    def test_retention_partial_and_expired_require_explicit_live_reset(self):
        self.state.view.accept(self.event(), "BTC/USD", self.data["identity"])
        self.state.view.reconcile([{ "time": "2026-10-23T10:30:00Z"}, {"time": "2026-10-23T11:10:00Z"}])
        self.assertTrue(self.state.view.partial)
        self.state.view.reconcile([{ "time": "2026-10-23T11:01:00Z"}, {"time": "2026-10-23T12:00:00Z"}])
        self.assertTrue(self.state.view.expired)
        self.assertEqual("2026-10-23T10:00:00Z", self.state.view.x[0])
        self.state.view.return_live()
        self.assertTrue(self.state.view.live)
        self.assertIsNone(self.state.view.x)

    def test_stale_instrument_identity_mount_command_revision_and_nonfinite_are_atomic_rejections(self):
        for key, value in (("instrument", "ETH/USD"), ("identity", {}), ("mount", 0), ("commandEpoch", 1), ("revision", 0), ("y", [0, float("nan")]), ("x", ["bad", "bad"])):
            event = self.event()
            event[key] = value
            before = copy.deepcopy(self.state.view)
            with self.subTest(key=key), self.assertRaises(ValueError):
                self.state.view.accept(event, "BTC/USD", self.data["identity"])
            self.assertEqual(before, self.state.view)

    def test_ack_is_not_a_manual_gesture_and_counters_are_strict(self):
        event = self.event()
        event.update(kind="ack", revision=0)
        self.assertFalse(self.state.view.accept(event, "BTC/USD", self.data["identity"]))
        self.assertTrue(self.state.view.live)
        event["revision"] = True
        with self.assertRaises(ValueError):
            self.state.view.accept(event, "BTC/USD", self.data["identity"])

    def test_instrument_and_session_views_do_not_share_ranges_or_preferences(self):
        other, second_session = chart.InstrumentView(), chart.InstrumentView()
        self.state.view.accept(self.event(), "BTC/USD", self.data["identity"])
        self.state.preset = 100
        self.assertIsNone(other.view.x)
        self.assertIsNone(second_session.view.x)
        self.assertEqual(300, other.preset)


class StreamlitSmokeTests(unittest.TestCase):
    def app(self, client):
        from streamlit.testing.v1 import AppTest
        path = Path(chart.__file__).with_name("realtime_financial_visualization.py")
        app = AppTest.from_file(str(path), default_timeout=30)
        return app

    def test_real_streamlit_instrument_controls_full_unchanged_and_no_key(self):
        client = FixtureClient()
        with patch.dict(os.environ, {"AIQ_RESEARCH_CHART": "1", "AIQ_RESEARCH_BASE_URL": "https://offline.invalid", "AIQ_RESEARCH_API_KEY": TEST_KEY}), patch.object(chart, "ResearchSnapshotClient", return_value=client):
            app = self.app(client).run()
            self.assertEqual([], list(app.exception))
            self.assertTrue(any("Transformed educational" in c.value for c in app.caption))
            self.assertEqual(["BTC/USD"], client.calls)
            app.radio(key="r115_instrument").set_value("ETH/USD").run()
            self.assertEqual([], list(app.exception))
            self.assertEqual(["BTC/USD", "ETH/USD"], client.calls)
            app.multiselect(key="r115_overlays_ETH/USD").set_value(["EMA20", "Bollinger bands"]).run()
            self.assertEqual(2, len(client.calls))
            app.button(key="r115_refresh_ETH/USD").click().run()
            self.assertEqual(3, len(client.calls))
            self.assertEqual("unchanged", client.snapshot("ETH/USD")["type"])
            rendered = str([x.value for x in app.caption]) + str([x.value for x in app.info])
            self.assertNotIn(TEST_KEY, rendered)

    def test_empty_unavailable_and_disconnected_last_good_are_truthful(self):
        client = FixtureClient(0)
        with patch.dict(os.environ, {"AIQ_RESEARCH_CHART": "1", "AIQ_RESEARCH_BASE_URL": "https://offline.invalid", "AIQ_RESEARCH_API_KEY": TEST_KEY}), patch.object(chart, "ResearchSnapshotClient", return_value=client):
            app = self.app(client).run()
            self.assertEqual([], list(app.exception))
            self.assertTrue(any("No completed" in x.value for x in app.info))
        client = FixtureClient()
        with patch.dict(os.environ, {"AIQ_RESEARCH_CHART": "1", "AIQ_RESEARCH_BASE_URL": "https://offline.invalid", "AIQ_RESEARCH_API_KEY": TEST_KEY}), patch.object(chart, "ResearchSnapshotClient", return_value=client):
            app = self.app(client).run()
            client.fail.add("BTC/USD")
            app.button(key="r115_refresh_BTC/USD").click().run()
            self.assertEqual([], list(app.exception))
            self.assertTrue(any("Disconnected - showing last-known" in x.value for x in app.warning))
            self.assertEqual(401, len(client.snapshot("BTC/USD")["candles"]))
            client.fail.clear()
            app.button(key="r115_refresh_BTC/USD").click().run()
            self.assertEqual([], list(app.exception))
            self.assertEqual("Connected", client.connection_state("BTC/USD")[0])
            self.assertEqual("unchanged", client.snapshot("BTC/USD")["type"])
            self.assertFalse(any("Disconnected - showing last-known" in x.value for x in app.warning))


    def test_missing_configuration_fails_closed_without_displaying_key(self):
        with patch.dict(os.environ, {"AIQ_RESEARCH_CHART": "1", "AIQ_RESEARCH_BASE_URL": "https://offline.invalid", "AIQ_RESEARCH_API_KEY": TEST_KEY}), patch.object(chart, "ResearchSnapshotClient", side_effect=SnapshotUnavailable()):
            app = self.app(None).run()
            self.assertEqual([], list(app.exception))
            self.assertTrue(any("No validated historical data" in x.value for x in app.warning))
            self.assertNotIn(TEST_KEY, str([x.value for x in app.warning]))

    def test_stale_snapshot_is_labeled_as_an_observation_not_current_quotes(self):
        client = FixtureClient()
        data = client.data["BTC/USD"]
        data["observedAt"] = data["status"]["observedAt"] = "2026-10-23T12:04:15.000000Z"
        data["status"]["freshness"] = "Stale"
        with patch.dict(os.environ, {"AIQ_RESEARCH_CHART": "1", "AIQ_RESEARCH_BASE_URL": "https://offline.invalid", "AIQ_RESEARCH_API_KEY": TEST_KEY}), patch.object(chart, "ResearchSnapshotClient", return_value=client):
            app = self.app(client).run()
            self.assertEqual([], list(app.exception))
            self.assertTrue(any("not current market quotes" in x.value for x in app.warning))
            self.assertTrue(any("Freshness at observation: Stale" in x.value for x in app.caption))

    def test_two_cold_streamlit_runtimes_keep_clients_and_preferences_separate(self):
        first, second = FixtureClient(), FixtureClient()
        with patch.dict(os.environ, {"AIQ_RESEARCH_CHART": "1", "AIQ_RESEARCH_BASE_URL": "https://offline.invalid", "AIQ_RESEARCH_API_KEY": TEST_KEY}), patch.object(chart, "ResearchSnapshotClient", side_effect=[first, second]):
            a = self.app(first).run()
            b = self.app(second).run()
            a.selectbox(key="r115_preset_BTC/USD").set_value(100).run()
            a.radio(key="r115_instrument").set_value("ETH/USD").run()
            self.assertEqual([], list(a.exception))
            self.assertEqual([], list(b.exception))
            self.assertEqual(300, b.selectbox(key="r115_preset_BTC/USD").value)
            self.assertEqual(["BTC/USD", "ETH/USD"], first.calls)
            self.assertEqual(["BTC/USD"], second.calls)


if __name__ == "__main__":
    unittest.main()
