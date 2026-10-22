import unittest
from supervisor import Backoff, RESEARCH_UID, PUBLIC_UID

class SupervisorPolicyTests(unittest.TestCase):
    def test_distinct_fixed_nonroot_children(self):
        self.assertNotEqual(RESEARCH_UID,PUBLIC_UID); self.assertGreater(RESEARCH_UID,0); self.assertGreater(PUBLIC_UID,0)
    def test_restart_delay_grows_and_is_bounded(self):
        b=Backoff(); self.assertEqual([b.failure() for _ in range(8)],[1,2,4,8,16,30,30,30])
    def test_healthy_resets_delay(self):
        b=Backoff(); b.failure(); b.failure(); b.healthy(); self.assertEqual(b.failure(),1)
    def test_failure_loop_remains_bounded(self):
        b=Backoff(); self.assertTrue(all(1<=b.failure()<=30 for _ in range(1000)))

if __name__=='__main__': unittest.main()
