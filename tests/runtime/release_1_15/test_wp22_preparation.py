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

class DiagnosticTests(unittest.TestCase):
    setUp = PreparationTests.setUp
    inspect = PreparationTests.inspect
    update = PreparationTests.update
    def wire(self, b=None):
        b=copy.deepcopy(b or baseline())
        site=b["site"]
        return {("get","",R.SITE):{"id":site["id"],"kind":site["kind"],"location":site["location"],
                    "properties":{k:v for k,v in site.items() if k not in ("id","kind","location")}},
            ("get","",R.PLAN):{**b["plan"],"location":"West US 2"},
            **{(method,route,R.SITE):{"properties":b[key]} for key,method,route in (
                ("web","get","/config/web"),("settings","post","/config/appsettings/list"),
                ("connections","post","/config/connectionstrings/list"),("logs","get","/config/logs"),
                ("slotFlags","get","/config/slotConfigNames"))},
            ("get","/slots",R.SITE):{"value":b["slots"]}}

    def real_inspect(self, transport):
        with mock.patch.object(R.Azure,"request",side_effect=transport):
            return P.inspect(self.directory,P.ReadOnlyAzure(),self.gate)

    def test_all_sixteen_request_failures_exact_stage_no_retries(self):
        operations=list(P.REQUEST_NAMES)
        for index in range(16):
            calls=[];wire=self.wire()
            def transport(method,suffix="",body=None,resource=R.SITE,kind=None):
                op=(method,suffix,resource);calls.append(op)
                if len(calls)==index+1:raise R.Blocked("AZURE_OPERATION_FAILED")
                return copy.deepcopy(wire[op])
            result=self.real_inspect(transport)
            expected=("FIRST_CAPTURE_" if index<8 else "SECOND_CAPTURE_")+P.REQUEST_NAMES[operations[index%8]]
            with self.subTest(index=index):
                self.assertEqual(result["failedStage"],expected)
                self.assertEqual(result["failureCategory"],"AZURE_TRANSPORT_FAILURE")
                self.assertEqual(len(calls),index+1)
                self.assertFalse(result["serviceKeyChannelChecked"])

    def test_all_response_validation_stages(self):
        operations=list(P.REQUEST_NAMES)
        for index in range(16):
            calls=[];wire=self.wire()
            def transport(method,suffix="",body=None,resource=R.SITE,kind=None):
                op=(method,suffix,resource);calls.append(op)
                return [] if len(calls)==index+1 else copy.deepcopy(wire[op])
            result=self.real_inspect(transport)
            with self.subTest(index=index):
                self.assertEqual(result["failedStage"],("FIRST_CAPTURE_" if index<8 else "SECOND_CAPTURE_")+P.REQUEST_NAMES[operations[index%8]]+"_RESPONSE_VALIDATION")
                self.assertEqual(result["failureCategory"],"AZURE_RESOURCE_RESPONSE_INVALID")
                self.assertEqual(len(calls),index+1)

    def test_exact_success_inventory_and_validation_equality_with_wp21(self):
        wire=self.wire();calls=[]
        def transport(method,suffix="",body=None,resource=R.SITE,kind=None):
            calls.append((method,suffix,resource));return copy.deepcopy(wire[calls[-1]])
        result=self.real_inspect(transport)
        self.assertTrue(result["azureBaselineVerified"])
        self.assertTrue(result["serviceKeyChannelChecked"])
        self.assertIsNone(result["failedStage"]);self.assertIsNone(result["failureCategory"])
        self.assertEqual(calls,list(P.REQUEST_NAMES)*2)
        self.assertEqual(set(calls),P.READS)
        with mock.patch.object(R.Azure,"request",side_effect=transport):
            expected=R.Azure().capture();actual=P.ReadOnlyAzure().capture()
        self.assertEqual(actual,expected)
        self.assertFalse((self.directory/"baseline.json").exists())
        self.assertFalse((self.directory/"consumed.json").exists())

    def test_capture_validation_stages_and_missing_fields(self):
        for offset in (0,8):
            wire=self.wire();calls=[]
            def transport(method,suffix="",body=None,resource=R.SITE,kind=None):
                op=(method,suffix,resource);calls.append(op);value=copy.deepcopy(wire[op])
                if len(calls)==offset+3:value["properties"].pop("appCommandLine")
                return value
            result=self.real_inspect(transport)
            self.assertEqual(result["failedStage"],("FIRST_CAPTURE_" if offset==0 else "SECOND_CAPTURE_")+"VALIDATION")
            self.assertEqual(result["failureCategory"],"AZURE_BASELINE_INCOMPLETE")

    def test_missing_nested_response_is_schema_not_transport(self):
        wire=self.wire();wire[("get","/config/web",R.SITE)]={}
        result=self.real_inspect(lambda method,suffix="",resource=R.SITE:copy.deepcopy(wire[(method,suffix,resource)]))
        self.assertEqual(result["failedStage"],"FIRST_CAPTURE_WEB_RESPONSE_VALIDATION")
        self.assertEqual(result["failureCategory"],"AZURE_RESPONSE_SCHEMA_MISMATCH")

    def test_target_plan_region_mismatch(self):
        for op,field,value,stage in ((('get','',R.SITE),'id','other','SITE'),
                (('get','',R.SITE),'location','other','SITE'),
                (('get','',R.PLAN),'id','other','PLAN'),
                (('get','',R.PLAN),'location','other','PLAN'),
                (('get','',R.PLAN),'sku',{'name':'paid'},'PLAN')):
            wire=self.wire();wire[op][field]=value
            result=self.real_inspect(lambda method,suffix="",resource=R.SITE:copy.deepcopy(wire[(method,suffix,resource)]))
            self.assertEqual(result["failureCategory"],"AZURE_TARGET_OR_PLAN_MISMATCH")
            self.assertEqual(result["failedStage"],"FIRST_CAPTURE_"+stage+"_RESPONSE_VALIDATION")

    def test_drift_stage(self):
        changed=baseline();changed["settings"]["unrelated"]="changed"
        self.adapter.capture.side_effect=[baseline(),changed]
        result=self.inspect()
        self.assertEqual(result["failedStage"],"BASELINE_COMPARISON")
        self.assertEqual(result["failureCategory"],"AZURE_BASELINE_DRIFT")

    def test_template_preflight_and_post_template_stages(self):
        with mock.patch.object(P,"template",side_effect=R.Blocked("TEMPLATE_EXPIRED_OR_NOT_FRESH")):
            result=self.inspect()
        self.assertEqual(result["failedStage"],"PRIVATE_ATTEMPT_TEMPLATE")
        with mock.patch.object(P,"template",side_effect=[self.value,R.Blocked("TEMPLATE_EXPIRED_OR_NOT_FRESH")]):
            result=self.inspect()
        self.assertEqual(result["failedStage"],"POST_CAPTURE_TEMPLATE_RECHECK")
        self.gate.return_value={"result":"BLOCKED"}
        result=self.inspect();self.assertEqual(result["failedStage"],"OPERATOR_PREFLIGHT")

    def test_service_check_exception_stage(self):
        # Malformed external process channel access, not a real key operation.
        with mock.patch.object(P.os,"environ") as env:
            env.get.side_effect=RuntimeError("SECRET_CANARY")
            result=self.inspect()
        self.assertEqual(result["failedStage"],"SERVICE_KEY_CHANNEL_CHECK")
        self.assertFalse(result["serviceKeyChannelChecked"])

    def test_absent_key_checked_vs_prior_failure_unchecked(self):
        b=baseline();b["settings"]={};self.adapter.capture.side_effect=lambda:b
        result=self.inspect();self.assertTrue(result["serviceKeyChannelChecked"])
        self.assertFalse(result["serviceKeyChannelAvailable"])
        self.adapter.capture.side_effect=TimeoutError("SECRET_CANARY")
        result=self.inspect();self.assertFalse(result["serviceKeyChannelChecked"])
        self.assertEqual(result["failureCategory"],"AZURE_READ_TIMEOUT")

    def test_timeout_cli_and_unclassified_categories(self):
        for error,expected in ((P.subprocess.TimeoutExpired("SECRET_CANARY",45,stderr=b"SECRET_CANARY"),"AZURE_READ_TIMEOUT"),
            (R.Blocked("AZURE_CLI_UNAVAILABLE"),"AZURE_CLI_UNAVAILABLE"),
            (R.Blocked("AZURE_OPERATION_FAILED"),"AZURE_TRANSPORT_FAILURE"),
            (RuntimeError("401 SECRET_CANARY"),"UNCLASSIFIED_FAILURE"),
            (RuntimeError("403 SECRET_CANARY"),"UNCLASSIFIED_FAILURE"),
            (R.Blocked("SECRET_CANARY"),"UNCLASSIFIED_FAILURE")):
            result=self.real_inspect(mock.Mock(side_effect=error))
            self.assertEqual(result["failedStage"],"FIRST_CAPTURE_SITE")
            self.assertEqual(result["failureCategory"],expected)
            self.assertNotIn("SECRET_CANARY",json.dumps(result))
        # HTTP statuses are unavailable in the real transport. Text never classifies auth/RBAC.

    def test_observation_write_failure_overrides_success_and_prior_failure(self):
        for capture in (lambda:baseline(),mock.Mock(side_effect=RuntimeError("SECRET_CANARY"))):
            self.adapter.capture.side_effect=capture
            with mock.patch.object(R,"save_new",side_effect=OSError("SECRET_CANARY")) as save:
                result=self.inspect()
            self.assertEqual(result["failedStage"],"OBSERVATION_WRITE")
            self.assertEqual(result["failureCategory"],"OBSERVATION_PERSISTENCE_FAILED")
            self.assertFalse(result["azureBaselineVerified"])
            self.assertEqual(result["result"],"AZURE_BASELINE_UNVERIFIED")
            save.assert_called_once()

    def test_entry_failures_have_actual_entry_stage(self):
        import contextlib,io
        for argv,stage,target in (([],"OFFLINE_QUALIFICATION",P.subprocess),
                (["--prepare-private-attempt"],"PRIVATE_ATTEMPT_PREPARATION",P.C)):
            name="run" if target is P.subprocess else "prepare_private_attempt"
            output=io.StringIO()
            with mock.patch.object(target,name,side_effect=RuntimeError("SECRET_CANARY")),contextlib.redirect_stdout(output):
                self.assertEqual(P.main(argv),1)
            value=json.loads(output.getvalue())
            self.assertEqual(value["failedStage"],stage)
            self.assertIn(value["failureCategory"],P.CATEGORIES)
            self.assertNotIn("SECRET_CANARY",output.getvalue())

    def test_stdout_observations_redacted_closed_and_bounded(self):
        canary="UNIQUE_SECRET_CANARY /private/path token=settings-value"
        self.adapter.capture.side_effect=RuntimeError(canary)
        result=self.inspect()
        self.assertIn(result["failedStage"],P.STAGES)
        self.assertIn(result["failureCategory"],P.CATEGORIES)
        self.assertLess(len(R.encoded(result)),4096)
        for path in self.directory.glob("wp22-observation-*.json"):
            data=path.read_text();self.assertNotIn(canary,data);self.assertNotIn(str(self.directory),data)
        import contextlib,io
        output=io.StringIO()
        with mock.patch.dict(os.environ,{"AIQ_WP21_ATTEMPT_DIRECTORY":str(self.directory)}), \
             mock.patch.object(R.Azure,"request",side_effect=RuntimeError(canary)), \
             mock.patch.object(P.C.WP19,"offline_report",return_value={"result":"OFFLINE_PREFLIGHT_PASS"}), \
             contextlib.redirect_stdout(output):
            self.assertEqual(P.main(["--inspect-azure-read-only"]),1)
        self.assertNotIn(canary,output.getvalue());self.assertLess(len(output.getvalue()),4096)
        self.assertFalse(json.loads(output.getvalue())["liveControllerInvoked"])

if __name__=="__main__":unittest.main()
