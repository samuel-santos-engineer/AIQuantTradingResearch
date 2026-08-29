import json, tempfile, unittest
from pathlib import Path
from visualization_read_model import *

def payload(value=1, identity="a", kind="HistoricalPresentation", mode="Historical"):
    return json.dumps({"contractVersion": CONTRACT_VERSION, "revision":{"kind":kind,"value":value,"identity":identity}, "sourceMode":mode,"sourceAuthority":0 if mode=="Historical" else 1,"target":"BTC","state":"Empty","window":[],"observationCount":0})

class ReadModelTests(unittest.TestCase):
    def test_configuration(self):
        env={"LOCALAPPDATA":"C:/runtime"}; self.assertEqual(2, refresh_interval_seconds(env)); self.assertEqual(60, refresh_interval_seconds({**env,"Visualization__RefreshIntervalSeconds":"60"})); self.assertTrue(str(resolve_handoff_path(env)).endswith("visualization-read-model.json"))
        for value in ("0","61","bad"): self.assertRaises(ConfigurationError, refresh_interval_seconds, {**env,"Visualization__RefreshIntervalSeconds":value})
        self.assertRaises(ConfigurationError, resolve_handoff_path, {**env,"Visualization__HandoffPath":"relative.json"})
    def test_cache_revision_and_retry(self):
        with tempfile.TemporaryDirectory() as directory:
            path=Path(directory)/"model.json"; cache=ReadModelCache(); self.assertEqual("ProducerUnavailable",cache.refresh(path))
            path.write_text("pending", encoding="utf-8")
            calls=[]
            def reader(_): calls.append(1); return "{" if len(calls)==1 else payload()
            self.assertIsNone(cache.refresh(path, reader, lambda seconds:self.assertEqual(.05,seconds))); self.assertEqual(2,len(calls)); first=cache.last_good
            path.write_text(payload(0),encoding="utf-8"); self.assertEqual("OlderRevision",cache.refresh(path)); self.assertIs(cache.last_good,first)
            path.write_text(payload(1,"conflict"),encoding="utf-8"); self.assertEqual("RevisionConflict",cache.refresh(path)); self.assertIs(cache.last_good,first)
            path.write_text(payload(1,"r", "ReplayLogicalTick","Replay"),encoding="utf-8"); self.assertIsNone(cache.refresh(path)); self.assertEqual("Replay",cache.last_good.raw["sourceMode"])
    def test_version_rejected(self): self.assertRaises(ReadModelError, parse_envelope, '{"contractVersion":"wrong"}')

    def test_system_health_is_optional_for_old_v1_and_strict_when_present(self):
        legacy = parse_envelope(payload())
        self.assertEqual({"state":"unavailable","provenance":"historical","reason":"required-health-evidence-unavailable"}, legacy.raw["systemHealth"])
        current = json.loads(payload())
        current["systemHealth"] = {"state":"failed","provenance":"historical","reason":"pipeline-failed"}
        self.assertEqual("failed", parse_envelope(json.dumps(current)).raw["systemHealth"]["state"])
        for health in (None, {}, {"state":"ready","provenance":"historical","reason":"pipeline-failed"}, {"state":"unknown","provenance":"historical","reason":None}):
            malformed = json.loads(payload()); malformed["systemHealth"] = health
            with self.assertRaisesRegex(ReadModelError, "HealthIntegrity"):
                parse_envelope(json.dumps(malformed))

    def test_malformed_system_health_retains_last_good_envelope(self):
        with tempfile.TemporaryDirectory() as directory:
            path = Path(directory) / "model.json"; cache = ReadModelCache()
            valid = json.loads(payload()); valid["systemHealth"] = {"state":"ready","provenance":"historical","reason":None}
            path.write_text(json.dumps(valid), encoding="utf-8")
            self.assertIsNone(cache.refresh(path)); last_good = cache.last_good
            malformed = json.loads(payload()); malformed["systemHealth"] = {"state":"ready","provenance":"historical","reason":"unexpected"}
            path.write_text(json.dumps(malformed), encoding="utf-8")
            self.assertEqual("HealthIntegrity", cache.refresh(path)); self.assertIs(last_good, cache.last_good)
if __name__ == "__main__": unittest.main()
