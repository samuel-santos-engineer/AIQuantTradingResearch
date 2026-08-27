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
if __name__ == "__main__": unittest.main()
