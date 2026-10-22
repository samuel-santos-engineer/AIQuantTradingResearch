import unittest
from viewport_state import Viewport
from fixture_snapshots import snapshot


class ViewportTests(unittest.TestCase):
    def viewport(self):
        value = Viewport()
        self.assertTrue(value.manual(("2026-10-20T01:00:00Z", "2026-10-20T02:00:00Z"), (90, 120), 1, 1))
        return value

    def test_changed_refresh_keeps_both_ranges(self):
        v = self.viewport()
        self.assertEqual(v.reconcile("2026-10-20T00:00:00Z", "2026-10-21T00:00:00Z"),
                         (("2026-10-20T01:00:00Z", "2026-10-20T02:00:00Z"), (90, 120)))

    def test_partial_overlap_keeps_absolute_ranges(self):
        v = self.viewport(); v.reconcile("2026-10-20T01:30:00Z", "2026-10-21T00:00:00Z")
        self.assertFalse(v.expired); self.assertEqual(v.x[0], "2026-10-20T01:00:00Z")

    def test_expired_does_not_silently_return_live(self):
        v = self.viewport(); v.reconcile("2026-10-21T00:00:00Z", "2026-10-22T00:00:00Z")
        self.assertTrue(v.expired); self.assertFalse(v.live)

    def test_rebuild_rejects_old_mount(self):
        v = self.viewport(); v.reconcile("2026-10-20T00:00:00Z", "2026-10-21T00:00:00Z", rebuild=True)
        self.assertFalse(v.manual(("a", "b"), (1, 2), 10, 1)); self.assertEqual(v.y, (90, 120))

    def test_late_revision_cannot_overwrite(self):
        v = self.viewport(); self.assertFalse(v.manual(("a", "b"), (1, 2), 1, 1)); self.assertEqual(v.y, (90, 120))

    def test_invalid_ranges_rejected(self):
        for x,y in [(("b", "a"), (1, 2)), (("a", "b"), (2, 1))]:
            with self.assertRaises(ValueError): Viewport().manual(x, y, 1, 1)

    def test_return_live_is_explicit(self):
        v = self.viewport(); v.return_live(); self.assertTrue(v.live); self.assertIsNone(v.x); self.assertIsNone(v.y)

    def test_sessions_independent(self):
        a,b = self.viewport(), Viewport(); self.assertIsNone(b.x); a.return_live(); self.assertEqual(b.revision, 0)

    def test_fixture_counts_gaps_and_identity(self):
        a=snapshot(); b=snapshot("ETH/USD", gap=700)
        self.assertEqual(len(a["rows"]),1000); self.assertEqual(len(b["rows"]),999)
        self.assertNotEqual(a["instrument"],b["instrument"])
        self.assertEqual(a["rows"][0]["time"],"2026-10-20T00:00:00Z")

if __name__ == "__main__": unittest.main()
