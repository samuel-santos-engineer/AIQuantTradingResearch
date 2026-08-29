import ast
import json
import unittest
from decimal import Decimal
from pathlib import Path

import realtime_financial_visualization as presentation
from visualization_read_model import Envelope, ReadModelError, parse_envelope


def payload(health=None):
    result = {
        "contractVersion": "aiq-visualization-read-model-v1",
        "revision": {"kind": "HistoricalPresentation", "value": 1, "identity": "wp06"},
        "sourceMode": "Historical", "sourceAuthority": 0, "target": "SIMULATED-USD", "state": "Ready",
        "window": [{"sourceTime": "2024-01-01T00:00:00+00:00", "price": Decimal("10")}],
        "latest": {"sourceTime": "2024-01-01T00:00:00+00:00", "price": Decimal("10")},
        "observationCount": 1,
    }
    if health is not None:
        result["systemHealth"] = health
    return result


class Release110NoBypassTests(unittest.TestCase):
    def test_import_and_ast_boundaries(self):
        tree = ast.parse(Path(presentation.__file__).read_text(encoding="utf-8"))
        imports = {node.names[0].name.split(".")[0] for node in ast.walk(tree) if isinstance(node, ast.Import)}
        imports |= {node.module.split(".")[0] for node in ast.walk(tree) if isinstance(node, ast.ImportFrom) and node.module}
        self.assertFalse({"sqlite3", "subprocess", "multiprocessing", "requests"} & imports)
        self.assertIn("visualization_read_model", imports)

    def test_health_projection_and_presentation_mapping(self):
        expected = {
            "ready": ("info", "System Health: Canonical evidence available."),
            "warmup": ("info", "System Health: Waiting for bounded canonical observations."),
            "empty": ("info", "System Health: Canonical pipeline completed with no observations."),
            "failed": ("error", "System Health: Canonical pipeline failed."),
            "stale": ("warning", "System Health: Canonical visualization evidence is structurally stale."),
            "unavailable": ("warning", "System Health: Health evidence is unavailable; visualization data may still be available."),
        }
        for state, projected in expected.items():
            reason = {"failed": "pipeline-failed", "stale": "structural-staleness", "unavailable": "required-health-evidence-unavailable"}.get(state)
            frame = presentation.project_visualization_frame(Envelope(payload({"state": state, "provenance": "historical", "reason": reason})))
            self.assertEqual(projected, presentation.project_system_health_presentation(frame))
        self.assertNotIn("degraded", expected)

    def test_absent_and_malformed_health_are_not_false_healthy(self):
        absent = parse_envelope(json.dumps(payload(), default=str))
        self.assertEqual("unavailable", absent.raw["systemHealth"]["state"])
        invalid = payload({"state": "degraded", "provenance": "historical", "reason": None})
        with self.assertRaises(ReadModelError):
            parse_envelope(json.dumps(invalid, default=str))

    def test_no_sensitive_or_unbounded_telemetry_inputs(self):
        source = Path(presentation.__file__).read_text(encoding="utf-8").lower()
        for forbidden in ("sqlite", "twelvedata", "popen", "os.system", "opentelemetry"):
            self.assertNotIn(forbidden, source)


if __name__ == "__main__":
    unittest.main()
