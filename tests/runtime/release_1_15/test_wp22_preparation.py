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
DIAGNOSTIC_CLI=P.diagnostic_cli

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

class TransportTests(unittest.TestCase):
    setUp = PreparationTests.setUp
    wire = DiagnosticTests.wire

    def worker(self, output=b'{}', code=0, raw=False):
        if not raw:
            try:
                payload=json.loads(output)
                if isinstance(payload,dict):
                    output=json.dumps({'protocol':P.WORKER_PROTOCOL,'ok':True,'workerStage':None,'payload':payload}).encode()
            except (ValueError,UnicodeError):pass
        child=mock.Mock();child.pid=321;child.returncode=code
        child.poll.return_value=code
        child.communicate.return_value=(output,b"SECRET_CANARY status=403 /private/path")
        return child

    def call(self, child, arguments=None):
        with mock.patch.dict(os.environ,{"AIQ_WP21_ATTEMPT_DIRECTORY":str(self.directory)}), \
             mock.patch.object(Path,"is_file",return_value=True), \
             mock.patch.object(P.subprocess,"Popen",return_value=child) as start, \
             mock.patch.object(R,"worker_identity",return_value={"pid":321,"created":42}):
            value=DIAGNOSTIC_CLI(arguments or ['rest','--method','get','--url','FIXED'])
        return value,start

    def failure(self,child,expected):
        with self.assertRaises(P.TransportFailure) as caught:self.call(child)
        self.assertEqual(caught.exception.transport_category,expected)
        self.assertIsNone(caught.exception.http_status)
        return caught.exception

    def test_executable_missing_and_launch_failure(self):
        with mock.patch.object(Path,"is_file",return_value=False), \
             mock.patch.object(P.subprocess,"Popen") as start,self.assertRaises(P.TransportFailure) as caught:
            P.diagnostic_cli([])
        self.assertEqual(caught.exception.transport_category,"CLI_EXECUTABLE_UNAVAILABLE");start.assert_not_called()
        with mock.patch.object(Path,"is_file",return_value=True), \
             mock.patch.dict(os.environ,{"AIQ_WP21_ATTEMPT_DIRECTORY":str(self.directory)}), \
             mock.patch.object(P.subprocess,"Popen",side_effect=OSError("SECRET_CANARY 401")), \
             self.assertRaises(P.TransportFailure) as caught:P.diagnostic_cli([])
        self.assertEqual(caught.exception.transport_category,"WORKER_LAUNCH_FAILURE")

    def test_nonzero_exit_including_ambiguous_watchdog(self):
        for code in (1,124,401,403,-1):
            with self.subTest(code=code):
                child=self.worker(b'{"httpStatus":403,"secret":"SECRET_CANARY"}',code)
                self.failure(child,"WORKER_NONZERO_EXIT")
                self.assertEqual(child.communicate.call_count,1)

    def test_invalid_empty_oversize_and_nonobject_output(self):
        for output in (b'',b'SECRET_CANARY status=401',b'null',b'[]',b'"403"',b'\xff',b'x'*(8*1024*1024+1)):
            self.failure(self.worker(output,raw=True),"INVALID_WORKER_OUTPUT")

    def test_timeout_terminates_only_owned_worker_without_retry(self):
        child=self.worker();child.poll.return_value=None
        child.communicate.side_effect=[P.subprocess.TimeoutExpired("SECRET_CANARY",45,output=b"401"), (b'',b'')]
        error=self.failure(child,"PARENT_TIMEOUT")
        child.kill.assert_called_once_with()
        self.assertEqual(child.communicate.call_count,2)
        self.assertEqual(child.communicate.call_args.kwargs,{"timeout":5})
        self.assertEqual(P.category(error,"FIRST_CAPTURE_SITE"),"AZURE_READ_TIMEOUT")

    def test_worker_io_failure_and_cleanup_uncertainty(self):
        child=self.worker();child.poll.return_value=None
        child.communicate.side_effect=[OSError("SECRET_CANARY"),(b'',b'')]
        self.failure(child,"WORKER_IO_FAILURE");child.kill.assert_called_once()
        child=self.worker();child.poll.return_value=None
        child.communicate.side_effect=P.subprocess.TimeoutExpired("SECRET_CANARY",45)
        self.failure(child,"UNKNOWN")

    def test_ownership_failure_no_request_dispatched(self):
        child=self.worker();child.poll.return_value=None
        with mock.patch.object(R,"save_new",side_effect=OSError("SECRET_CANARY")):
            self.failure(child,"UNKNOWN")
        child.kill.assert_called_once()
        self.assertEqual(child.communicate.call_args.kwargs,{"timeout":5})
        self.assertEqual(child.communicate.call_count,1)

    def test_success_request_body_stdin_and_original_timeout_watchdog(self):
        child=self.worker(b'{"properties":{}}')
        value,start=self.call(child,['rest','--method','post','--url','FIXED','--body','SECRET_CANARY'])
        self.assertEqual(value,{"properties":{}})
        self.assertNotIn('SECRET_CANARY',str(start.call_args))
        self.assertIn(b'SECRET_CANARY',child.communicate.call_args.kwargs['input'])
        self.assertEqual(child.communicate.call_args.kwargs['timeout'],45)
        self.assertEqual(start.call_args.args[0][-1],P.DIAGNOSTIC_BRIDGE)
        self.assertIn('threading.Timer(40',P.DIAGNOSTIC_BRIDGE)
        self.assertEqual(len(list(self.directory.glob('management-worker-*.json'))),1)

    def test_typed_metadata_propagated_stdout_and_observation_without_secret(self):
        import contextlib,io
        for name in P.TRANSPORT_CATEGORIES:
            output=io.StringIO()
            with mock.patch.dict(os.environ,{"AIQ_WP21_ATTEMPT_DIRECTORY":str(self.directory)}), \
                 mock.patch.object(P,"diagnostic_cli",side_effect=P.TransportFailure(name,"REQUEST" if name=="WORKER_REPORTED_FAILURE" else None)) as call, \
                 mock.patch.object(P.C.WP19,"offline_report",return_value={"result":"OFFLINE_PREFLIGHT_PASS"}), \
                 contextlib.redirect_stdout(output):
                self.assertEqual(P.main(["--inspect-azure-read-only"]),1)
            value=json.loads(output.getvalue())
            self.assertEqual(value['failedStage'],'FIRST_CAPTURE_SITE')
            self.assertEqual(value['transportFailureCategory'],name)
            self.assertIsNone(value['httpStatus'])
            self.assertFalse(value['serviceKeyChannelChecked']);self.assertFalse(value['approvalEnabled'])
            self.assertFalse(value['liveControllerInvoked']);self.assertFalse(value['liveAttemptReady'])
            self.assertLess(len(output.getvalue()),4096);self.assertEqual(call.call_count,1)
        for path in self.directory.glob('wp22-observation-*.json'):
            value=R.load(path);self.assertIn(value['transportFailureCategory'],P.TRANSPORT_CATEGORIES)
            self.assertIsNone(value['httpStatus']);self.assertNotIn('SECRET_CANARY',path.read_text())

    def test_spoofed_unsupported_statuses_never_become_metadata(self):
        for status in (401,403,200,0,999,True,'401',None):
            # Structured JSON from ARM is baseline data, not a transport envelope.
            child=self.worker(json.dumps({'httpStatus':status,'message':'SECRET_CANARY 403'}).encode())
            with mock.patch.object(P,'diagnostic_cli',side_effect=lambda args:self.call(child,args)[0]), \
                 mock.patch.object(P.C.WP19,'offline_report',return_value={'result':'OFFLINE_PREFLIGHT_PASS'}):
                result=P.inspect(self.directory,P.ReadOnlyAzure())
            self.assertIsNone(result['httpStatus'])
            self.assertIsNone(result['transportFailureCategory'])  # local schema validation
            self.assertEqual(result['failureCategory'],'AZURE_RESPONSE_SCHEMA_MISMATCH')
            self.assertNotIn('SECRET_CANARY',json.dumps(result))

    def test_live_default_transport_unchanged_and_readonly_inventory(self):
        with mock.patch.object(R,'cli',return_value={'old':'contract'}) as old, \
             mock.patch.object(P,'diagnostic_cli',return_value={'new':'contract'}) as diagnostic:
            self.assertEqual(R.Azure().request('get'),{'old':'contract'})
            old.assert_called_once();diagnostic.assert_not_called()
            self.assertEqual(P.ReadOnlyAzure().request('get'),{'new':'contract'})
            self.assertEqual(old.call_count,1);diagnostic.assert_called_once()
        wire=self.wire();calls=[]
        def transport(args):
            method=args[args.index('--method')+1];url=args[args.index('--url')+1]
            op=next(op for op in P.READS if op[0]==method and url=='https://management.azure.com'+op[2]+op[1]+'?api-version='+R.API)
            calls.append(op);return copy.deepcopy(wire[op])
        with mock.patch.object(P,'diagnostic_cli',side_effect=transport), \
             mock.patch.object(P.C.WP19,'offline_report',return_value={'result':'OFFLINE_PREFLIGHT_PASS'}):
            value=P.inspect(self.directory,P.ReadOnlyAzure())
        self.assertTrue(value['azureBaselineVerified']);self.assertIsNone(value['transportFailureCategory'])
        self.assertIsNone(value['httpStatus']);self.assertEqual(calls,list(P.REQUEST_NAMES)*2)
        self.assertFalse((self.directory/'baseline.json').exists());self.assertFalse((self.directory/'consumed.json').exists())

class WorkerStageTests(unittest.TestCase):
    def execute_worker(self, case):
        import contextlib,io,types
        class Timer:
            def __init__(self,*args):pass
            def start(self):pass
        calls=[]
        core=types.ModuleType('azure.cli.core')
        def initialize():
            if case=='INITIALIZATION':raise RuntimeError('SECRET_CANARY /private/path')
            return types.SimpleNamespace(data={})
        core.get_default_cli=initialize
        util=types.ModuleType('azure.cli.core.util')
        def request(*args,**kwargs):
            self.assertEqual(kwargs['headers'],['Content-Type=application/json'])
            self.assertEqual(args[0].data['command'],'rest')
            calls.append(True)
            if case=='REQUEST':raise RuntimeError('SECRET_CANARY status=403')
            def decode():
                if case=='RESPONSE_DECODING':raise ValueError('SECRET_CANARY')
                return {'properties':{}}
            return types.SimpleNamespace(status_code=403 if case=='RESPONSE_VALIDATION' else 200,
                content=b'{}',json=decode)
        util.send_raw_request=request
        modules={'azure':types.ModuleType('azure'),'azure.cli':types.ModuleType('azure.cli'),
            'azure.cli.core':core,'azure.cli.core.util':util}
        source='INVALID_SECRET_CANARY' if case=='INPUT' else json.dumps(['rest','--method','get','--url','FIXED'])
        output=io.StringIO()
        with mock.patch.dict(sys.modules,modules),mock.patch('threading.Timer',Timer), \
             mock.patch('logging.disable'),mock.patch.object(sys,'stdin',io.StringIO(source)), \
             mock.patch('socket.socket.connect',side_effect=AssertionError('NETWORK_DISABLED')), \
             contextlib.redirect_stdout(output):
            exec(compile(P.DIAGNOSTIC_BRIDGE,'readonly-worker','exec'),{})
        self.assertNotIn('SECRET_CANARY',output.getvalue())
        self.assertLess(len(output.getvalue()),4096)
        return json.loads(output.getvalue()),calls

    def test_exact_worker_stages_with_fake_cli_and_network_disabled(self):
        for stage in P.WORKER_STAGES:
            value,calls=self.execute_worker(stage)
            self.assertEqual(value,{'protocol':P.WORKER_PROTOCOL,'ok':False,'workerStage':stage,'payload':None})
            self.assertEqual(len(calls),0 if stage in ('INPUT','INITIALIZATION') else 1)

    def test_success_envelope_preserves_payload(self):
        value,calls=self.execute_worker('SUCCESS')
        self.assertEqual(value,{'protocol':P.WORKER_PROTOCOL,'ok':True,'workerStage':None,'payload':{'properties':{}}})
        self.assertEqual(len(calls),1)


class WorkerEnvelopeTests(unittest.TestCase):
    setUp=PreparationTests.setUp
    worker=TransportTests.worker
    call=TransportTests.call
    failure=TransportTests.failure
    def test_parent_accepts_only_fixed_failure_stages(self):
        for stage in P.WORKER_STAGES:
            value={'protocol':P.WORKER_PROTOCOL,'ok':False,'workerStage':stage,'payload':None}
            error=self.failure(self.worker(json.dumps(value).encode(),raw=True),'WORKER_REPORTED_FAILURE')
            self.assertEqual(error.worker_stage,stage)
        bad=[{'protocol':P.WORKER_PROTOCOL,'ok':False,'workerStage':'SECRET_CANARY','payload':None},
            {'protocol':P.WORKER_PROTOCOL,'ok':False,'workerStage':'REQUEST','payload':{'secret':'SECRET_CANARY'}},
            {'protocol':'unknown','ok':False,'workerStage':'REQUEST','payload':None},
            {'protocol':P.WORKER_PROTOCOL,'ok':True,'workerStage':'REQUEST','payload':{}},
            {'protocol':P.WORKER_PROTOCOL,'ok':1,'workerStage':None,'payload':{}},
            {'protocol':P.WORKER_PROTOCOL,'ok':False,'workerStage':'REQUEST','payload':None,'extra':'SECRET_CANARY'}]
        for value in bad:self.failure(self.worker(json.dumps(value).encode(),raw=True),'INVALID_WORKER_OUTPUT')

    def test_worker_stage_reaches_sanitized_report_and_observation(self):
        import contextlib,io
        for stage in P.WORKER_STAGES:
            output=io.StringIO()
            with mock.patch.dict(os.environ,{'AIQ_WP21_ATTEMPT_DIRECTORY':str(self.directory)}), \
                 mock.patch.object(P,'diagnostic_cli',side_effect=P.TransportFailure('WORKER_REPORTED_FAILURE',stage)), \
                 mock.patch.object(P.C.WP19,'offline_report',return_value={'result':'OFFLINE_PREFLIGHT_PASS'}), \
                 contextlib.redirect_stdout(output):self.assertEqual(P.main(['--inspect-azure-read-only']),1)
            value=json.loads(output.getvalue());self.assertEqual(value['workerStage'],stage)
            self.assertEqual(value['transportFailureCategory'],'WORKER_REPORTED_FAILURE')
            self.assertIsNone(value['httpStatus']);self.assertFalse(value['serviceKeyChannelChecked'])
        self.assertEqual(len(list(self.directory.glob('wp22-observation-*.json'))),len(P.WORKER_STAGES))

class InstalledCliCompatibilityTests(unittest.TestCase):
    def test_both_exact_workers_against_installed_cli_with_fake_auth_and_http(self):
        interpreter=Path(os.environ.get('ProgramFiles',r'C:\Program Files'))/'Microsoft SDKs/Azure/CLI2/python.exe'
        if not interpreter.is_file():self.skipTest('Installed Windows Azure CLI interpreter unavailable')
        # Execute actual installed send_raw_request, never actual login/profile/token/HTTP.
        probe = r"""
import contextlib,io,json,logging,socket,sys,types
from unittest import mock
logging.disable(logging.CRITICAL)
bridges=json.load(sys.stdin)
from azure.cli.core.util import send_raw_request
import requests
class Timer:
 def __init__(self,*args):pass
 def start(self):pass
class FakeProfile:
 def __init__(self,*args,**kwargs):pass
 def get_raw_token(self,*args,**kwargs):return (('Bearer','OFFLINE_FAKE_TOKEN',None),None,None)
results=[]
for bridge in bridges:
 ctx=types.SimpleNamespace(data={},cloud=types.SimpleNamespace(endpoints=types.SimpleNamespace(
  resource_manager='https://management.azure.com/',active_directory_resource_id='https://management.azure.com/')))
 response=requests.Response();response.status_code=200;response._content=b'{"properties":{}}'
 response.reason='OK';response.headers['Content-Type']='application/json'
 output=io.StringIO();code=0
 with mock.patch('azure.cli.core.get_default_cli',return_value=ctx), \
      mock.patch('azure.cli.core._profile.Profile',FakeProfile), \
      mock.patch('azure.cli.core.util.get_az_rest_user_agent',return_value='offline-test'), \
      mock.patch('azure.cli.core.telemetry.set_user_agent'), \
      mock.patch('azure.cli.core.util._log_request'),mock.patch('azure.cli.core.util._log_response'), \
      mock.patch('requests.Session.send',return_value=response) as send, \
      mock.patch('socket.socket.connect',side_effect=AssertionError('NETWORK_FORBIDDEN')), \
      mock.patch('socket.getaddrinfo',side_effect=AssertionError('DNS_FORBIDDEN')), \
      mock.patch('threading.Timer',Timer), \
      mock.patch.object(sys,'stdin',io.StringIO(json.dumps(['rest','--method','get','--url',
       'https://management.azure.com/subscriptions/00000000-0000-0000-0000-000000000000/FIXED']))), \
      contextlib.redirect_stdout(output):
  try:exec(compile(bridge,'exact-worker','exec'),{})
  except SystemExit as error:code=error.code
 value=json.loads(output.getvalue())
 payload=value['payload'] if 'protocol' in value else value
 assert code==0 and payload=={'properties':{}} and send.call_count==1
 assert ctx.data['command']=='rest'
 assert send.call_args.args[0].headers['Content-Type']=='application/json'
 assert 'OFFLINE_FAKE_TOKEN' not in output.getvalue()
 results.append(True)
print(json.dumps({'workersPassed':len(results),'networkMocked':True,'profileMocked':True}))
"""
        result=P.subprocess.run([str(interpreter),'-I','-B','-c',probe],
            input=json.dumps([R.BRIDGE,P.DIAGNOSTIC_BRIDGE]).encode(),capture_output=True,timeout=30)
        # Never echo raw stdout/stderr on assertion failure.
        self.assertEqual(result.returncode,0,'Installed helper compatibility failed; raw streams withheld')
        self.assertEqual(json.loads(result.stdout),{'workersPassed':2,'networkMocked':True,'profileMocked':True})

class ValidationRuleTests(unittest.TestCase):
    setUp=PreparationTests.setUp
    inspect=PreparationTests.inspect

    def test_real_validation_failures_both_capture_passes(self):
        cases=[
            ('INCOMPLETE_BASELINE',lambda b:b.pop('web')),
            ('BASELINE_TARGET_MISMATCH',lambda b:b['site'].update(id='other')),
            ('BASELINE_STATE_INVALID',lambda b:b['site'].update(httpsOnly=None)),
            ('BASELINE_STARTUP_MISSING',lambda b:b['web'].pop('appCommandLine')),
            ('BASELINE_SETTINGS_INVALID',lambda b:b.update(settings=None)),
            ('BASELINE_METADATA_MISSING',lambda b:b.update(slots=[{}])),
            ('BASELINE_CONNECTION_METADATA_UNVERIFIED',lambda b:b.update(connections={'private':{'value':None,'type':'Custom'}})),
            ('BASELINE_SLOT_FLAGS_MISSING',lambda b:b['slotFlags'].pop('appSettingNames')),
            ('BASELINE_LOGGING_MISSING',lambda b:b['logs'].pop('httpLogs')),
            ('BASELINE_HOST_UNVERIFIED',lambda b:b['site'].update(state='Stopped')),
            ('BASELINE_STARTUP_UNVERIFIED',lambda b:b['web'].update(appCommandLine=None)),
            ('BASELINE_SLOT_METADATA_UNVERIFIED',lambda b:b['slotFlags'].update(appSettingNames='malformed')),
            ('BASELINE_LOGGING_UNVERIFIED',lambda b:b['logs'].update(httpLogs=None))]
        self.assertEqual({code for code,_ in cases},P.VALIDATION_RULES)
        for second in (False,True):
            for code,mutate in cases:
                b=baseline();mutate(b)
                self.adapter.capture.side_effect=[baseline(),b] if second else [b]
                with self.subTest(second=second,rule=code):
                    result=self.inspect()
                    self.assertEqual(result['validationRule'],code)
                    self.assertEqual(result['failedStage'],'SECOND_CAPTURE_VALIDATION' if second else 'FIRST_CAPTURE_VALIDATION')
                    self.assertFalse(result['azureBaselineVerified']);self.assertFalse(result['serviceKeyChannelChecked'])
                    self.assertFalse(result['approvalEnabled']);self.assertFalse(result['liveControllerInvoked'])
                    self.assertEqual(self.adapter.capture.call_count,2 if second else 1)
                self.adapter.capture.reset_mock()
        self.assertFalse((self.directory/'baseline.json').exists());self.assertFalse((self.directory/'consumed.json').exists())

    def test_unknown_exception_and_nonvalidation_codes_never_disclosed(self):
        for error in (R.Blocked('SECRET_CANARY /private/path'),RuntimeError('BASELINE_SETTINGS_INVALID SECRET_CANARY')):
            self.adapter.capture.side_effect=error
            result=self.inspect();self.assertIsNone(result['validationRule'])
            self.assertNotIn('SECRET_CANARY',json.dumps(result))
        with mock.patch.object(P,'template',side_effect=R.Blocked('BASELINE_SETTINGS_INVALID')):
            result=self.inspect();self.assertIsNone(result['validationRule'])

    def test_rule_observation_and_success_null(self):
        b=baseline();b['settings']={'SECRET_SETTING_CANARY':None};self.adapter.capture.side_effect=[b]
        result=self.inspect();self.assertEqual(result['validationRule'],'BASELINE_SETTINGS_INVALID')
        path=next(self.directory.glob('wp22-observation-*.json'))
        self.assertEqual(R.load(path)['validationRule'],'BASELINE_SETTINGS_INVALID')
        self.assertNotIn('SECRET_SETTING_CANARY',path.read_text())
        self.adapter.capture.side_effect=lambda:baseline()
        self.assertIsNone(self.inspect()['validationRule'])
        with mock.patch.object(R,'save_new',side_effect=OSError('SECRET_CANARY')):
            result=self.inspect()
        self.assertEqual(result['failedStage'],'OBSERVATION_WRITE');self.assertIsNone(result['validationRule'])

if __name__=="__main__":unittest.main()
