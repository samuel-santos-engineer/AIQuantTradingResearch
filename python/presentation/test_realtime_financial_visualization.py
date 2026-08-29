"""WP09 permanent deterministic parser-to-frame-to-presentation coverage."""
import json
import unittest

import realtime_financial_visualization as visualization
from realtime_financial_visualization import project_system_health_presentation, project_visualization_frame, project_wp07_presentation_sections, render_visualization_frame
from visualization_read_model import parse_envelope


def envelope(state, points, *, feature=None, failure=None, system_health=None):
    latest = points[-1] if points else None
    result = {
        "contractVersion": "aiq-visualization-read-model-v1",
        "revision": {"kind": "HistoricalPresentation", "value": 1, "identity": "a" * 64},
        "sourceMode": "Historical", "sourceAuthority": 0, "target": "BTC", "state": state,
        "window": points, "latest": latest, "observationCount": len(points),
        "feature": feature, "pipeline": {"isSuccess": state != "Failed"},
        "idempotencyStatus": "Unavailable", "dataQualityStatus": "Unavailable", "failure": failure,
        "staleReason": None,
    }
    if system_health is not None:
        result["systemHealth"] = system_health
    return result


class PermanentVisualizationTests(unittest.TestCase):
    def project(self, payload):
        return project_visualization_frame(parse_envelope(json.dumps(payload)))

    def assert_sections(self, frame, state):
        sections = project_wp07_presentation_sections(frame)
        self.assertEqual(("Feature", "Snapshot", "Data Quality", "Pipeline", "Idempotency"), tuple(section for section, _ in sections))
        self.assertEqual(("Backend state", state), sections[3][1][1])

    def test_pi_ready_preserves_exact_latest_feature_and_sections(self):
        frame = self.project(envelope("Ready", [{"sourceTime": "2024-01-01T00:00:00+00:00", "price": 10}, {"sourceTime": "2024-01-01T00:01:00+00:00", "price": 12.5}], feature={"identity": "simple-return-lag-1-v1", "value": 0.25, "observationCount": 2, "requiredObservationCount": 2}))
        self.assertEqual("12.5", format(frame.latest.price, "f")); self.assertEqual(2, frame.window_count); self.assert_sections(frame, "Ready")

    def test_pi_warmup_preserves_required_count_without_value(self):
        frame = self.project(envelope("WarmUp", [{"sourceTime": "2024-01-01T00:00:00+00:00", "price": 10}], feature={"identity": "simple-return-lag-1-v1", "value": None, "observationCount": 1, "requiredObservationCount": 2}))
        self.assertIsNone(frame.feature_value); self.assertEqual(1, frame.feature_observation_count); self.assert_sections(frame, "WarmUp")

    def test_pi_empty_preserves_zero_window_and_unavailable_rows(self):
        frame = self.project(envelope("Empty", []))
        self.assertIsNone(frame.latest); self.assertEqual(0, frame.window_count); self.assert_sections(frame, "Empty")

    def test_pi_failed_preserves_safe_failure_and_sections(self):
        frame = self.project(envelope("Failed", [], failure={"category": "DependencyUnavailable", "message": "safe failure", "recoverable": True}))
        self.assertEqual("DependencyUnavailable", frame.failure_category); self.assertEqual("safe failure", frame.failure_message); self.assert_sections(frame, "Failed")

    def test_wp05_health_frame_and_fixed_presentation_mapping(self):
        expected = {
            "ready": ("info", "System Health: Canonical evidence available."),
            "warmup": ("info", "System Health: Waiting for bounded canonical observations."),
            "empty": ("info", "System Health: Canonical pipeline completed with no observations."),
            "failed": ("error", "System Health: Canonical pipeline failed."),
            "stale": ("warning", "System Health: Canonical visualization evidence is structurally stale."),
            "unavailable": ("warning", "System Health: Health evidence is unavailable; visualization data may still be available."),
        }
        for health_state, mapping in expected.items():
            reason = {"ready": None, "warmup": None, "empty": None, "failed": "pipeline-failed", "stale": "structural-staleness", "unavailable": "required-health-evidence-unavailable"}[health_state]
            frame = self.project(envelope("Ready", [], system_health={"state":health_state,"provenance":"historical","reason":reason}))
            self.assertEqual(health_state, frame.system_health_state); self.assertEqual(mapping, project_system_health_presentation(frame))

    def test_wp05_health_message_follows_target_state_heading(self):
        class FakeStreamlit:
            def __init__(self): self.calls=[]
            def subheader(self, value): self.calls.append(("subheader", value))
            def info(self, value): self.calls.append(("info", value))
            def warning(self, value): self.calls.append(("warning", value))
            def error(self, value): self.calls.append(("error", value))
            def write(self, value): self.calls.append(("write", value))
            def line_chart(self, *args, **kwargs): self.calls.append(("line_chart", None))
        fake = FakeStreamlit(); original = visualization.st; visualization.st = fake
        try:
            render_visualization_frame(self.project(envelope("Ready", [], system_health={"state":"ready","provenance":"historical","reason":None})))
        finally:
            visualization.st = original
        self.assertEqual([("subheader", "BTC - Ready"), ("subheader", "System Health"), ("info", "System Health: Canonical evidence available.")], fake.calls[:3])


if __name__ == "__main__":
    unittest.main()
