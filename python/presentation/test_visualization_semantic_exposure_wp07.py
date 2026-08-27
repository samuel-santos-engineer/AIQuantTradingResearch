import unittest
from visualization_read_model import parse_envelope, ReadModelError
from realtime_financial_visualization import project_visualization_frame

def payload(idempotency="Unavailable", quality="Unavailable"):
    return '{"contractVersion":"aiq-visualization-read-model-v1","revision":{"kind":"HistoricalPresentation","value":1,"identity":"a"},"sourceMode":"Historical","sourceAuthority":0,"target":"BTC","state":"Empty","window":[],"latest":null,"observationCount":0,"feature":null,"pipeline":null,"failure":null,"staleReason":null,"idempotencyStatus":"' + idempotency + '","dataQualityStatus":"' + quality + '"}'

class SemanticExposureTests(unittest.TestCase):
    def test_all_canonical_values_propagate_without_changing_frame_shape(self):
        for status in ("NewlyPersisted", "EquivalentExisting", "Unavailable"):
            for quality in ("Valid", "Invalid", "Unavailable"):
                frame = project_visualization_frame(parse_envelope(payload(status, quality)))
                self.assertEqual((status, quality, 0, 64), (frame.idempotency_status, frame.data_quality_status, frame.window_count, frame.window_capacity))
    def test_missing_fields_are_backward_unavailable_and_unknown_values_rejected(self):
        legacy = payload().replace(',"idempotencyStatus":"Unavailable","dataQualityStatus":"Unavailable"', '')
        self.assertEqual(("Unavailable", "Unavailable"), (project_visualization_frame(parse_envelope(legacy)).idempotency_status, project_visualization_frame(parse_envelope(legacy)).data_quality_status))
        with self.assertRaises(ReadModelError): parse_envelope(payload("Unknown", "Valid"))

if __name__ == "__main__": unittest.main()
