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

class CommandTests(unittest.TestCase):
    def test_command_preserves_viewport(self):
        v=ViewportTests().viewport();v.command();self.assertEqual(v.command_epoch,1);self.assertEqual(v.y,(90,120))
    def test_rebuild_changes_mount_without_reset(self):
        v=ViewportTests().viewport();v.command(rebuild=True);self.assertEqual(v.mount,2);self.assertFalse(v.live)
    def test_only_one_inflight_and_one_pending(self):
        v=Viewport();self.assertEqual(v.request(),1)
        for _ in range(20): self.assertIsNone(v.request())
        self.assertTrue(v.pending);self.assertEqual(v.request_epoch,1)
        self.assertTrue(v.finish(1));self.assertTrue(v.in_flight);self.assertFalse(v.pending);self.assertEqual(v.request_epoch,2)
        self.assertTrue(v.finish(2));self.assertFalse(v.in_flight)
    def test_old_request_cannot_finish_current(self):
        v=Viewport();v.request();self.assertFalse(v.finish(0));self.assertTrue(v.in_flight)
    def test_return_live_clears_all_axes_and_commands(self):
        v=ViewportTests().viewport();v.volume_y=(0,12);v.return_live();self.assertIsNone(v.volume_y);self.assertEqual(v.command_epoch,1)
    def test_command_epochs_are_per_instrument(self):
        a,b=Viewport(),Viewport();a.command();self.assertEqual(b.command_epoch,0)

if __name__ == "__main__": unittest.main()
