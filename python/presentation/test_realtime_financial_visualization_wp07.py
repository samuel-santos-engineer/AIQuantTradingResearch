import unittest
from visualization_read_model import parse_envelope
from realtime_financial_visualization import project_visualization_frame, project_wp07_presentation_sections

def frame(state="Ready", feature=None, pipeline=None, failure=None, idem="NewlyPersisted", quality="Valid"):
    payload = {"contractVersion":"aiq-visualization-read-model-v1","revision":{"kind":"HistoricalPresentation","value":1,"identity":"a"},"sourceMode":"Historical","sourceAuthority":0,"target":"BTC","state":state,"window":[],"latest":None,"observationCount":0,"feature":feature,"pipeline":pipeline,"failure":failure,"staleReason":"retained" if state == "Stale" else None,"idempotencyStatus":idem,"dataQualityStatus":quality}
    return project_visualization_frame(parse_envelope(__import__("json").dumps(payload)))

class PresentationContractTests(unittest.TestCase):
    def test_exact_order_labels_and_available_values(self):
        sections = project_wp07_presentation_sections(frame(feature={"identity":"simple-return-lag-1-v1","value":0.25,"observationCount":2,"requiredObservationCount":2}, pipeline={"isSuccess":True}))
        self.assertEqual(("Feature","Snapshot","Data Quality","Pipeline","Idempotency"), tuple(item[0] for item in sections))
        self.assertEqual((("Feature identity","simple-return-lag-1-v1"),("Feature value","0.25"),("Observed / required","2 / 2")), sections[0][1])
        self.assertEqual((("Validation status","Valid"),), sections[2][1])
        self.assertEqual((("Pipeline status","Success"),("Backend state","Ready"),("Failure category","Unavailable")), sections[3][1])
        self.assertEqual((("Persistence disposition","NewlyPersisted"),), sections[4][1])

    def test_five_states_and_canonical_unavailable_values_are_deterministic(self):
        for state, idem, quality in (("WarmUp","EquivalentExisting","Valid"),("Empty","Unavailable","Unavailable"),("Failed","Unavailable","Invalid"),("Stale","EquivalentExisting","Valid")):
            value = frame(state, idem=idem, quality=quality, failure={"category":"Safe","message":"safe","recoverable":True} if state == "Failed" else None)
            first, second = project_wp07_presentation_sections(value), project_wp07_presentation_sections(value)
            self.assertEqual(first, second)
            self.assertEqual("Unavailable", dict(first[1][1])["Snapshot identity"])
            self.assertEqual(idem, dict(first[4][1])["Persistence disposition"])
            self.assertEqual(quality, dict(first[2][1])["Validation status"])

if __name__ == "__main__": unittest.main()
