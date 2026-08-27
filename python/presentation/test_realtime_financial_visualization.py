"""WP09 permanent deterministic parser-to-frame-to-presentation coverage."""
import json
import unittest

from realtime_financial_visualization import project_visualization_frame, project_wp07_presentation_sections
from visualization_read_model import parse_envelope


def envelope(state, points, *, feature=None, failure=None):
    latest = points[-1] if points else None
    return {
        "contractVersion": "aiq-visualization-read-model-v1",
        "revision": {"kind": "HistoricalPresentation", "value": 1, "identity": "a" * 64},
        "sourceMode": "Historical", "sourceAuthority": 0, "target": "BTC", "state": state,
        "window": points, "latest": latest, "observationCount": len(points),
        "feature": feature, "pipeline": {"isSuccess": state != "Failed"},
        "idempotencyStatus": "Unavailable", "dataQualityStatus": "Unavailable", "failure": failure,
        "staleReason": None,
    }


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


if __name__ == "__main__":
    unittest.main()
