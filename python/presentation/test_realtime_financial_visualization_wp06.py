import json
import tempfile
import unittest
from decimal import Decimal
from pathlib import Path

import realtime_financial_visualization as visualization
from realtime_financial_visualization import FrameIntegrityError, project_visualization_frame
from visualization_read_model import Envelope, ReadModelCache


def envelope(value=1, identity="a", kind="HistoricalPresentation", mode="Historical", state="Ready", points=None, observation_count=None, feature=None, pipeline=None, failure=None, stale_reason=None):
    points = [{"sourceTime": "2024-01-01T00:00:00+00:00", "price": Decimal("10.0")}] if points is None else points
    return {"contractVersion": "aiq-visualization-read-model-v1", "revision": {"kind": kind, "value": value, "identity": identity}, "sourceMode": mode, "sourceAuthority": 0 if mode == "Historical" else 1, "target": "SIMULATED-USD", "state": state, "window": points, "latest": points[-1] if points else None, "observationCount": len(points) if observation_count is None else observation_count, "feature": feature, "pipeline": pipeline, "failure": failure, "staleReason": stale_reason}


def wrapped(**kwargs): return Envelope(envelope(**kwargs))


class VisualizationFrameTests(unittest.TestCase):
    def test_rendering_receives_exact_frame_data_without_pixel_assertions(self):
        class FakeStreamlit:
            def __init__(self): self.calls = []
            def subheader(self, value): self.calls.append(("subheader", value))
            def warning(self, value): self.calls.append(("warning", value))
            def line_chart(self, value, **kwargs): self.calls.append(("line_chart", value, kwargs))
            def write(self, value): self.calls.append(("write", value))
            def error(self, value): self.calls.append(("error", value))
            def info(self, value): self.calls.append(("info", value))

        frame = project_visualization_frame(wrapped(value=3, identity="render"), "TransportRead")
        fake, original = FakeStreamlit(), visualization.st
        try:
            visualization.st = fake
            visualization.render_visualization_frame(frame)
        finally:
            visualization.st = original
        chart = next(call for call in fake.calls if call[0] == "line_chart")
        self.assertEqual([{"sourceTime": "2024-01-01T00:00:00+00:00", "price": Decimal("10.0")}], chart[1])
        self.assertEqual({"x": "sourceTime", "y": "price"}, chart[2])
        self.assertIn(("warning", "TransportRead"), fake.calls)
        self.assertIn(("write", {"observationCount": 1, "windowCount": 1, "windowCapacity": 64}), fake.calls)

    def test_exact_one_and_two_point_frames_preserve_values_and_metadata(self):
        first = project_visualization_frame(wrapped(feature={"identity": "simple-return-lag-1-v1", "value": None, "observationCount": 1, "requiredObservationCount": 2}, pipeline={"isSuccess": True}))
        points = [{"sourceTime": "2024-01-01T00:00:00+00:00", "price": Decimal("10.0")}, {"sourceTime": "2024-01-01T00:01:00+00:00", "price": Decimal("12.5")}]
        second = project_visualization_frame(wrapped(value=2, identity="b", points=points, feature={"identity": "simple-return-lag-1-v1", "value": Decimal("0.25"), "observationCount": 2, "requiredObservationCount": 2}, pipeline={"isSuccess": True}))
        self.assertEqual(("2024-01-01T00:00:00+00:00",), tuple(point.source_time for point in first.points))
        self.assertEqual((Decimal("10.0"), Decimal("12.5")), tuple(point.price for point in second.points))
        self.assertEqual(second.points[-1], second.latest)
        self.assertEqual((2, 2, 64), (second.observation_count, second.window_count, second.window_capacity))
        self.assertEqual(("simple-return-lag-1-v1", Decimal("0.25"), True), (second.feature_identity, second.feature_value, second.pipeline_success))

    def test_sequential_evolution_equivalence_and_wp05_older_conflict_retention(self):
        with tempfile.TemporaryDirectory() as directory:
            path, cache = Path(directory) / "frame.json", ReadModelCache()
            path.write_text(json.dumps(envelope(value=1, identity="one"), default=float), encoding="utf-8"); self.assertIsNone(cache.refresh(path)); frame_one = project_visualization_frame(cache.last_good)
            path.write_text(json.dumps(envelope(value=2, identity="two"), default=float), encoding="utf-8"); self.assertIsNone(cache.refresh(path)); frame_two = project_visualization_frame(cache.last_good)
            self.assertGreater(frame_two.revision_value, frame_one.revision_value)
            path.write_text(json.dumps(envelope(value=2, identity="two"), default=float), encoding="utf-8"); self.assertIsNone(cache.refresh(path)); self.assertEqual(frame_two, project_visualization_frame(cache.last_good))
            path.write_text(json.dumps(envelope(value=1, identity="one"), default=float), encoding="utf-8"); self.assertEqual("OlderRevision", cache.refresh(path)); self.assertEqual(frame_two, project_visualization_frame(cache.last_good))
            path.write_text(json.dumps(envelope(value=2, identity="conflict"), default=float), encoding="utf-8"); self.assertEqual("RevisionConflict", cache.refresh(path)); retained = project_visualization_frame(cache.last_good, "RevisionConflict")
            self.assertEqual((frame_two.revision_identity, "RevisionConflict"), (retained.revision_identity, retained.transport_warning))

    def test_historical_replay_bounded_states_and_transport_are_truthful(self):
        historical = project_visualization_frame(wrapped(value=7, identity="historical"))
        replay = project_visualization_frame(wrapped(value=0, identity="replay", kind="ReplayLogicalTick", mode="Replay"), "TransportRead")
        points = [{"sourceTime": f"2024-01-01T{1 + minute // 60:02d}:{minute % 60:02d}:00+00:00", "price": Decimal(minute)} for minute in range(64)]
        bounded = project_visualization_frame(wrapped(points=points, observation_count=65))
        warmup = project_visualization_frame(wrapped(state="WarmUp", feature={"identity": "simple-return-lag-1-v1", "value": None, "observationCount": 1, "requiredObservationCount": 2}))
        empty = project_visualization_frame(wrapped(state="Empty", points=[]))
        failed = project_visualization_frame(wrapped(state="Failed", failure={"category": "Safe", "message": "safe failure", "recoverable": True}))
        stale = project_visualization_frame(wrapped(state="Stale", stale_reason="Structural staleness."))
        self.assertEqual(("HistoricalPresentation", "Historical", 0), (historical.revision_kind, historical.source_mode, historical.source_authority))
        self.assertEqual(("ReplayLogicalTick", "Replay", 1, "TransportRead"), (replay.revision_kind, replay.source_mode, replay.source_authority, replay.transport_warning))
        self.assertEqual((64, 65, 64), (bounded.window_count, bounded.observation_count, bounded.window_capacity))
        self.assertEqual(("WarmUp", 2), (warmup.state, warmup.feature_required_observation_count))
        self.assertEqual(("Empty", 0, None), (empty.state, empty.window_count, empty.latest))
        self.assertEqual(("Failed", "Safe", True), (failed.state, failed.failure_category, failed.failure_recoverable))
        self.assertEqual(("Stale", "Structural staleness."), (stale.state, stale.stale_reason))

    def test_integrity_failures_are_rejected_without_repair(self):
        mismatch = envelope(); mismatch["latest"] = {"sourceTime": "2024-01-01T00:01:00+00:00", "price": Decimal("11")}
        oversized = envelope(points=[{"sourceTime": f"2024-01-01T{1 + minute // 60:02d}:{minute % 60:02d}:00+00:00", "price": Decimal(minute)} for minute in range(65)], observation_count=65)
        invalid_revision = envelope(); invalid_revision["revision"]["value"] = -1
        invalid_state = envelope(); invalid_state["state"] = "Invented"
        inconsistent_count = envelope(observation_count=3)
        for payload in (mismatch, oversized, invalid_revision, invalid_state, inconsistent_count):
            with self.subTest(payload=payload):
                with self.assertRaises(FrameIntegrityError): project_visualization_frame(Envelope(payload))

    def test_no_payload_is_transport_local(self):
        cache = ReadModelCache()
        with tempfile.TemporaryDirectory() as directory:
            self.assertEqual("ProducerUnavailable", cache.refresh(Path(directory) / "missing.json"))
        self.assertIsNone(cache.last_good)


if __name__ == "__main__": unittest.main()
