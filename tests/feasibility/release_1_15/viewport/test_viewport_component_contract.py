"""Independent literal event vectors; no browser or Plotly-derived expectations."""
import copy
import unittest
from viewport_state import Viewport
from viewport_component import validate_event, accept_event

class EventTests(unittest.TestCase):
    def setUp(self):
        self.state=Viewport()
        self.identity={'generation':'fixture-generation-a','version':1,'displayDate':'2026-10-22'}
        self.event={'kind':'manual','instrument':'BTC/USD','identity':copy.deepcopy(self.identity),'mount':1,'revision':1,'commandEpoch':0,
                    'x':['2026-10-20T12:00:00.000Z','2026-10-20T13:00:00.000Z'],'y':[90,120],'y2':[0,12]}
    def reject(self,event,reason=None):
        before=copy.deepcopy(self.state)
        if reason:
            with self.assertRaisesRegex(ValueError,reason): validate_event(event,'BTC/USD',self.identity,self.state)
        else:
            with self.assertRaises(ValueError): validate_event(event,'BTC/USD',self.identity,self.state)
        self.assertEqual(before,self.state)
    def test_accept_manual_exact_ranges(self):
        self.assertTrue(accept_event(self.event,'BTC/USD',self.identity,self.state))
        self.assertEqual(self.state.x,('2026-10-20T12:00:00.000Z','2026-10-20T13:00:00.000Z'))
        self.assertEqual(self.state.y,(90,120));self.assertEqual(self.state.volume_y,(0,12));self.assertFalse(self.state.live)
    def test_ack_only_does_not_invent_manual_viewport(self):
        self.event.update(kind='ack',revision=0)
        self.assertFalse(accept_event(self.event,'BTC/USD',self.identity,self.state))
        self.assertEqual(self.state.acknowledged,0);self.assertTrue(self.state.live);self.assertIsNone(self.state.x)
    def test_missing_and_extra_fields(self):
        for key in self.event:
            e=copy.deepcopy(self.event);del e[key];self.reject(e)
        self.reject(dict(self.event,unexpected='x'))
    def test_wrong_instrument(self): self.reject(dict(self.event,instrument='ETH/USD'),'instrument')
    def test_identity_mismatch(self):
        for key,value in [('generation','other'),('version',2),('displayDate','2026-10-23')]:
            e=copy.deepcopy(self.event);e['identity'][key]=value;self.reject(e,'identity')
    def test_counter_types_bounds(self):
        for key in ['mount','revision','commandEpoch']:
            for value in [True,1.0,-1,2**31,'1',None]:
                e=dict(self.event);e[key]=value;self.reject(e)
    def test_old_mount_and_command(self):
        self.state.command(rebuild=True);self.reject(self.event,'stale mount/command')
    def test_duplicate_revision_rejected(self):
        accept_event(self.event,'BTC/USD',self.identity,self.state);self.reject(self.event,'stale revision')
    def test_old_revision_rejected(self):
        self.state.revision=7;self.reject(self.event,'stale revision')
    def test_wrong_ack_revision(self): self.reject(dict(self.event,kind='ack'),'ack revision')
    def test_invalid_utc_and_inverted_dates(self):
        for values in [['2026-10-20 12:00:00','2026-10-20T13:00:00Z'],['2026-02-30T12:00:00Z','2026-10-20T13:00:00Z'],['2026-10-20T13:00:00Z','2026-10-20T12:00:00Z'],['2026-10-20T12:00:00+01:00','2026-10-20T13:00:00Z']]: self.reject(dict(self.event,x=values))
    def test_excessive_date_span(self): self.reject(dict(self.event,x=['2026-01-01T00:00:00Z','2026-03-01T00:00:00Z']),'x range')
    def test_nonfinite_inverted_and_huge_y(self):
        for key in ['y','y2']:
            for values in [[float('nan'),2],[0,float('inf')],[120,90],[1,1],[False,2],[0,10**500]]:
                e=dict(self.event);e[key]=values;self.reject(e)
    def test_malformed_arrays(self):
        for key in ['x','y','y2']:
            for values in [[],[1],[1,2,3],None,'x']:
                e=dict(self.event);e[key]=values;self.reject(e)
    def test_oversized_event(self): self.reject(dict(self.event,instrument='x'*5000),'event bound')
    def test_invalid_json_value(self): self.reject(dict(self.event,instrument=object()),'invalid JSON')
    def test_invalid_kind_and_top_level(self):
        self.reject(dict(self.event,kind='provider'));self.reject([])


class PendingIntentRegressionTests(unittest.TestCase):
    # Literal ranges from the two published failing browser records. These are
    # expectations, not values calculated by the component under test.
    vectors = [
        (["2026-10-20T13:27:19.3785Z", "2026-10-20T15:14:25.5226Z"],
         [103.20059257070372, 111.21192112308292]),
        (["2026-10-20T14:02:32.4983Z", "2026-10-20T14:47:52.8756Z"],
         [105.19675938836983, 108.60665427165537]),
    ]

    def make_event(self, state, revision, vector):
        x,y=self.vectors[vector]
        return dict(kind='manual', instrument='BTC/USD',
                    identity=dict(generation='fixture-generation-a',version=2,displayDate='2026-10-22'),
                    mount=state.mount, revision=revision, commandEpoch=state.command_epoch,
                    x=x.copy(),y=y.copy(),y2=[0,10.526315789473685])

    def test_published_vectors_commit_once_after_refresh(self):
        for index in range(2):
            with self.subTest(vector=index):
                state=Viewport(revision=index+1);state.command()
                event=self.make_event(state,index+2,index)
                self.assertTrue(accept_event(event,'BTC/USD',event['identity'],state))
                self.assertEqual(state.x,tuple(self.vectors[index][0]))
                self.assertEqual(state.y,tuple(self.vectors[index][1]))
                self.assertEqual(state.volume_y,(0,10.526315789473685))
                with self.assertRaisesRegex(ValueError,'stale revision'):
                    accept_event(event,'BTC/USD',event['identity'],state)

    def test_ack_before_and_after_newest_intent(self):
        for ack_first in [True,False]:
            with self.subTest(ack_first=ack_first):
                state=Viewport();state.command()
                event=self.make_event(state,2,0)
                old_ack=dict(event,kind='ack',revision=0)
                if ack_first: self.assertFalse(accept_event(old_ack,'BTC/USD',event['identity'],state))
                self.assertTrue(accept_event(event,'BTC/USD',event['identity'],state))
                with self.assertRaisesRegex(ValueError,'ack revision'):
                    accept_event(old_ack,'BTC/USD',event['identity'],state)
                new_ack=dict(event,kind='ack')
                self.assertFalse(accept_event(new_ack,'BTC/USD',event['identity'],state))
                self.assertEqual(state.x,tuple(self.vectors[0][0]))

    def test_refresh_and_remount_do_not_advance_reset_fence(self):
        state=Viewport();state.command();state.command(rebuild=True)
        self.assertEqual(state.reset_epoch,0)
        event=self.make_event(state,1,0)
        self.assertTrue(accept_event(event,'BTC/USD',event['identity'],state))
        state.reconcile('2026-10-22T00:00:00Z','2026-10-23T00:00:00Z')
        self.assertTrue(state.expired);self.assertFalse(state.live)
        self.assertEqual(state.x,tuple(self.vectors[0][0]))

    def test_explicit_reset_fences_delayed_intent(self):
        state=Viewport();event=self.make_event(state,1,0)
        state.return_live()
        self.assertEqual(state.reset_epoch,1)
        with self.assertRaisesRegex(ValueError,'stale mount/command'):
            accept_event(event,'BTC/USD',event['identity'],state)
        self.assertTrue(state.live);self.assertIsNone(state.x)

if __name__=='__main__': unittest.main()
