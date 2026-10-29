"""No-network WP22 tests; Azure transport and operator policy boundaries mocked."""
import copy
import datetime as dt
import json
import os
from pathlib import Path
import sys
import tempfile
import unittest
import uuid
from unittest import mock
sys.path.insert(0,str(Path(__file__).resolve().parents[3]/"eng/azure-cli/r1.15-qualification"))
import wp22_preparation as P
from test_wp21_controller import baseline
R=P.R

class PreparationTests(unittest.TestCase):
    def setUp(self):
        self.temp=tempfile.TemporaryDirectory();self.addCleanup(self.temp.cleanup)
        self.directory=Path(self.temp.name)/str(uuid.uuid4());self.directory.mkdir()
        now=R.utc()
        self.value=dict(attemptId=self.directory.name,target=R.SITE,plan=R.PLAN,image=R.IMAGE,
            issuedUtc=now.isoformat(),expiresUtc=(now+dt.timedelta(minutes=15)).isoformat(),
            explicitLiveApproval=False,allowRecovery=False,code=R.identities())
        R.save_new(self.directory/"approval.json",self.value)
        patch=mock.patch.object(R,"private_directory");patch.start();self.addCleanup(patch.stop)
        patch=mock.patch.dict(os.environ,{"AIQ_WP21_SERVICE_KEY":""});patch.start();self.addCleanup(patch.stop)
        self.adapter=mock.Mock();self.adapter.capture.side_effect=lambda:baseline()
        self.gate=mock.Mock(return_value={"result":"OFFLINE_PREFLIGHT_PASS"})

    def inspect(self):return P.inspect(self.directory,self.adapter,self.gate)
    def update(self,**changes):
        self.value.update(changes)
        (self.directory/"approval.json").write_text(json.dumps(self.value),encoding="utf-8")

    def test_verified_read_only_disabled_and_no_live_records(self):
        result=self.inspect()
        self.assertTrue(result["azureBaselineVerified"])
        self.assertTrue(result["serviceKeyChannelAvailable"])
        self.assertFalse(result["approvalEnabled"]);self.assertFalse(result["liveAttemptReady"])
        self.assertEqual(self.adapter.capture.call_count,2)
        self.adapter.deploy.assert_not_called()
        self.assertFalse((self.directory/"baseline.json").exists())
        self.assertFalse((self.directory/"consumed.json").exists())
        self.assertFalse(R.load(self.directory/"approval.json")["explicitLiveApproval"])

    def test_read_inventory_only_eight_calls_allowed(self):
        with mock.patch.object(R.Azure,"request",return_value={"location":"West US 2"}) as transport:
            for method,suffix,resource in P.READS:P.ReadOnlyAzure().request(method,suffix,resource=resource)
            self.assertEqual(transport.call_count,8)
            for method,suffix,resource in [("post","/start",R.SITE),("post","/stop",R.SITE),
                ("patch","",R.SITE),("put","/config/appsettings",R.SITE),("get","",R.SITE+"/other")]:
                with self.assertRaises(R.Blocked):P.ReadOnlyAzure().request(method,suffix,resource=resource)
            with self.assertRaises(R.Blocked):P.ReadOnlyAzure().request("get",body={})
            self.assertEqual(transport.call_count,8)

    def test_paginated_or_missing_plan_region_rejected(self):
        for value in ({"nextLink":"https://other"}, {"location":"other"}, {}):
            with mock.patch.object(R.Azure,"request",return_value=value):
                with self.assertRaises((R.Blocked,KeyError)):P.ReadOnlyAzure().request("get",resource=R.PLAN)

    def test_expiry_after_capture_blocks_qualification(self):
        def capture():
            self.update(expiresUtc=(R.utc()-dt.timedelta(seconds=1)).isoformat())
            return baseline()
        self.adapter.capture.side_effect=capture
        self.assertFalse(self.inspect()["azureBaselineVerified"])

    def test_changed_code_blocks_before_reads(self):
        self.update(code={});self.assertFalse(self.inspect()["azureBaselineVerified"])
        self.adapter.capture.assert_not_called()

    def test_expired_blocks_before_reads(self):
        self.update(expiresUtc=(R.utc()-dt.timedelta(seconds=1)).isoformat())
        self.assertFalse(self.inspect()["azureBaselineVerified"]);self.adapter.capture.assert_not_called()

    def test_enabled_template_blocks(self):
        self.update(explicitLiveApproval=True)
        self.assertFalse(self.inspect()["azureBaselineVerified"]);self.adapter.capture.assert_not_called()

    def test_missing_template_blocks(self):
        (self.directory/"approval.json").unlink()
        self.assertFalse(self.inspect()["azureBaselineVerified"]);self.adapter.capture.assert_not_called()

    def test_target_substitution_blocks(self):
        self.update(target=R.SITE+"/slots/other")
        self.assertFalse(self.inspect()["azureBaselineVerified"]);self.adapter.capture.assert_not_called()

    def test_consumed_blocks(self):
        R.save_new(self.directory/"consumed.json",{})
        self.assertFalse(self.inspect()["azureBaselineVerified"]);self.adapter.capture.assert_not_called()

    def test_policy_failure_blocks_reads(self):
        self.gate.return_value={"result":"BLOCKED"}
        self.assertFalse(self.inspect()["azureBaselineVerified"]);self.adapter.capture.assert_not_called()

    def test_auth_failure_redacted_and_bounded(self):
        self.adapter.capture.side_effect=RuntimeError("SECRET_CANARY"*10000)
        result=self.inspect();data=R.encoded(result)
        self.assertLess(len(data),4096);self.assertNotIn(b"SECRET_CANARY",data)
        self.assertFalse(result["azureBaselineVerified"])

    def test_drift_rejected(self):
        other=baseline();other["web"]["appCommandLine"]="changed"
        self.adapter.capture.side_effect=[baseline(),other]
        self.assertFalse(self.inspect()["azureBaselineVerified"])

    def test_missing_all_categories_rejected(self):
        for key in baseline():
            with self.subTest(key=key):
                b=baseline();del b[key];self.adapter.capture.side_effect=lambda:b
                self.assertFalse(self.inspect()["azureBaselineVerified"])

    def test_malformed_metadata_and_running_region_rejected(self):
        for key,field,value in [("site","state","Stopped"),("site","location","other"),
            ("site","publicNetworkAccess",{}),("web","appCommandLine",None),
            ("slotFlags","appSettingNames","bad"),("logs","httpLogs",None)]:
            with self.subTest(field=field):
                b=baseline();b[key][field]=value;self.adapter.capture.side_effect=lambda:b
                self.assertFalse(self.inspect()["azureBaselineVerified"])

    def test_key_unknown_false_without_echo(self):
        b=baseline();b["settings"]={};self.adapter.capture.side_effect=lambda:b
        result=self.inspect();self.assertTrue(result["azureBaselineVerified"])
        self.assertFalse(result["serviceKeyChannelAvailable"])

    def test_environment_channel_boolean_only(self):
        with mock.patch.dict(os.environ,{"AIQ_WP21_SERVICE_KEY":"IN_MEMORY_PRIVATE_CANARY_123456789012345"}):
            result=self.inspect()
        self.assertTrue(result["serviceKeyChannelAvailable"])
        self.assertNotIn("CANARY",json.dumps(result))

    def test_observations_new_only_and_redacted(self):
        self.inspect();self.inspect()
        paths=list(self.directory.glob("wp22-observation-*.json"));self.assertEqual(len(paths),2)
        for path in paths:
            self.assertNotIn("FAKE_PRIVATE",path.read_text())
            self.assertNotIn(str(self.directory),path.read_text())

    def test_preparation_uses_existing_safe_path(self):
        with mock.patch.object(P.C,"prepare_private_attempt",return_value={"attemptId":self.directory.name}) as prep, mock.patch("builtins.print") as output:
            self.assertEqual(P.main(["--prepare-private-attempt"]),0)
        prep.assert_called_once_with()
        value=json.loads(output.call_args.args[0]);self.assertFalse(value["approvalEnabled"])
        self.assertEqual(value["attemptId"],self.directory.name)

if __name__=="__main__":unittest.main()
