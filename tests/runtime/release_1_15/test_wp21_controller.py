"""Offline qualification: every Azure/HTTP boundary mocked, no live adapter call."""
import contextlib
import datetime as dt
import importlib.util
import io
import json
import os
from pathlib import Path
import subprocess
import sys
import tempfile
import unittest
import uuid
from unittest import mock

ROOT=Path(__file__).resolve().parents[3]
HERE=ROOT/'eng/azure-cli/r1.15-qualification'
sys.path.insert(0,str(HERE))
import wp21_runtime as R
import wp21_controller as C
import wp21_recovery as REC
QUIESCE=R.quiesce_workers


def baseline():
    return {'site':{'id':R.SITE,'kind':'app,linux,container','location':'West US 2','serverFarmId':R.PLAN,'state':'Running','httpsOnly':False,'publicNetworkAccess':'Enabled'},
            'plan':{'id':R.PLAN,'sku':{'name':'F1'}},'web':{'linuxFxVersion':'DOCKER|original','appCommandLine':'original-command'},
            'settings':{'AIQ_RESEARCH_API_KEY':'FAKE_PRIVATE_CANARY_FOR_OFFLINE_ONLY_12345','unrelated':'preserve'},
            'connections':{'private':{'value':'FAKE_CONNECTION_CANARY','type':'Custom'}},
            'logs':{k:{} for k in ('applicationLogs','httpLogs','detailedErrorMessages','failedRequestsTracing')},
            'slotFlags':{'appSettingNames':['unrelated'],'connectionStringNames':['private']},'slots':[]}


class ControllerTests(unittest.TestCase):
    def setUp(self):
        self.temp=tempfile.TemporaryDirectory();self.addCleanup(self.temp.cleanup)
        self.ident=str(uuid.uuid4());self.directory=Path(self.temp.name)/self.ident;self.directory.mkdir()
        now=R.utc();self.approval={'attemptId':self.ident,'target':R.SITE,'plan':R.PLAN,'image':R.IMAGE,
          'issuedUtc':(now-dt.timedelta(seconds=1)).isoformat(),'expiresUtc':(now+dt.timedelta(minutes=15)).isoformat(),
          'explicitLiveApproval':True,'allowRecovery':True,'code':R.identities()}
        self.adapter=mock.Mock();self.adapter.capture.side_effect=lambda:baseline()
        self.gate=mock.Mock(return_value={'result':'LOCAL_PREFLIGHT_PASS'})
        self.health=mock.Mock(return_value=True);self.snapshot=mock.Mock()
        self.events=[]
        patch=mock.patch.object(R,'quiesce_workers');patch.start();self.addCleanup(patch.stop)

    def run_attempt(self):return C.run(self.directory,self.approval,self.adapter,self.gate,self.health,self.snapshot)

    def test_exact_authority_target_plan_image_code_and_freshness(self):
        for k,v in (('target',R.SITE+'/slots/other'),('plan','paid'),('image',R.IMAGE+':tag'),('explicitLiveApproval',False),('allowRecovery',False),('attemptId',str(uuid.uuid4())),('code',{}),('expiresUtc',(R.utc()-dt.timedelta(seconds=1)).isoformat())):
            approval={**self.approval,k:v}
            with self.subTest(k=k),self.assertRaises((R.Blocked,ValueError)):C.run(self.directory,approval,self.adapter,self.gate,self.health,self.snapshot)
        self.adapter.deploy.assert_not_called();self.gate.assert_not_called()

    def test_failed_real_gate_prevents_azure_reads_and_writes(self):
        self.gate.side_effect=RuntimeError('LOCAL_BLOCK')
        with self.assertRaises(RuntimeError):self.run_attempt()
        self.assertEqual(self.adapter.mock_calls,[])

    def test_malformed_preflight_success_cannot_authorize_azure(self):
        self.gate.return_value={'result':'INCOMPLETE'}
        with self.assertRaises(R.Blocked):self.run_attempt()
        self.assertEqual(self.adapter.mock_calls,[])

    def test_authority_expiring_during_baseline_reads_prevents_mutation(self):
        now=R.utc()
        with mock.patch.object(R,'utc',side_effect=[now,now+dt.timedelta(hours=2)]),self.assertRaises(R.Blocked):self.run_attempt()
        self.adapter.deploy.assert_not_called();self.assertFalse((self.directory/'baseline.json').exists())

    def test_incomplete_baseline_fails_without_write(self):
        for field in baseline():
            b=baseline();del b[field];self.adapter.capture.side_effect=lambda:b
            with self.subTest(field=field),self.assertRaises(R.Blocked):self.run_attempt()
        self.adapter.deploy.assert_not_called();self.assertFalse((self.directory/'consumed.json').exists())

    def test_drift_before_mutation_fails_closed(self):
        b=baseline();b['logs']['httpLogs']={'changed':True};self.adapter.capture.side_effect=[baseline(),b]
        with self.assertRaises(R.Blocked):self.run_attempt()
        self.adapter.deploy.assert_not_called();self.assertFalse((self.directory/'consumed.json').exists())

    def test_unavailable_query_fails_before_arming(self):
        self.adapter.capture.side_effect=TimeoutError('PRIVATE_CANARY')
        with self.assertRaises(TimeoutError):self.run_attempt()
        self.adapter.deploy.assert_not_called()

    def test_no_service_key_stops_before_any_mutation(self):
        b=baseline();b['settings']={};self.adapter.capture.side_effect=lambda:b
        with mock.patch.dict(os.environ,{'AIQ_WP21_SERVICE_KEY':''}),self.assertRaises(R.Blocked):self.run_attempt()
        self.adapter.deploy.assert_not_called()

    def test_missing_recovery_identity_fails_before_azure(self):
        self.approval['code'].pop('recovery')
        with self.assertRaises(R.Blocked):self.run_attempt()
        self.adapter.deploy.assert_not_called()

    def test_success_order_single_deployment_and_unconditional_recovery(self):
        self.adapter.deploy.side_effect=lambda b:self.events.append('deploy')
        self.health.side_effect=lambda:self.events.append('health') or True
        self.snapshot.side_effect=lambda x:self.events.append(x)
        self.adapter.restore.side_effect=lambda b:self.events.append('restore')
        result=self.run_attempt()
        self.assertEqual(self.events,['deploy','health','BTC/USD','ETH/USD','restore'])
        self.adapter.deploy.assert_called_once();self.assertEqual(self.adapter.capture.call_count,3)
        self.assertTrue(result['rollbackVerified']);self.assertTrue(result['RESEARCH_PROCESS_ALIVE'])
        self.assertFalse(result['PUBLIC_RUNTIME_VERIFIED']);self.assertNotIn('FAKE_PRIVATE_CANARY',json.dumps(result))
        self.assertNotIn('FAKE_CONNECTION_CANARY',json.dumps(result))

    def test_consumed_marker_refuses_replay_and_preserves_files(self):
        self.run_attempt();before=(self.directory/'baseline.json').read_bytes()
        with self.assertRaises(R.Blocked):self.run_attempt()
        self.assertEqual((self.directory/'baseline.json').read_bytes(),before);self.adapter.deploy.assert_called_once()

    def test_partial_deploy_errors_and_interrupts_all_recover(self):
        for error in (TimeoutError(),KeyboardInterrupt(),SystemExit()):
            with self.subTest(error=type(error).__name__):
                self.setUp();self.adapter.deploy.side_effect=error
                result=self.run_attempt();self.assertTrue(result['rollbackVerified']);self.health.assert_not_called();self.snapshot.assert_not_called()

    def test_health_failure_or_timeout_prevents_snapshots(self):
        for failure in (False,TimeoutError(),ValueError('HTML_PRIVATE')):
            with self.subTest(failure=type(failure).__name__):
                self.setUp()
                if isinstance(failure,Exception):self.health.side_effect=failure
                else:self.health.return_value=failure
                result=self.run_attempt();self.snapshot.assert_not_called();self.assertTrue(result['rollbackVerified'])
                self.assertEqual(result['RESEARCH_PROCESS_ALIVE'],'UNVERIFIED')

    def test_snapshot_failure_stops_second_instrument_and_restores(self):
        self.snapshot.side_effect=RuntimeError('PRIVATE_PAYLOAD')
        result=self.run_attempt();self.snapshot.assert_called_once_with('BTC/USD')
        self.assertTrue(result['rollbackVerified']);self.assertNotIn('PRIVATE_PAYLOAD',json.dumps(result))

    def test_failed_restore_prominently_overrides_health_success(self):
        self.adapter.restore.side_effect=RuntimeError('PRIVATE')
        result=self.run_attempt();self.assertEqual(result['result'],'CRITICAL_RECOVERY_UNVERIFIED');self.assertFalse(result['rollbackVerified'])

    def test_failed_independent_comparison_is_critical(self):
        b=baseline();b['site']['httpsOnly']=True
        self.adapter.capture.side_effect=[baseline(),baseline(),b]
        self.assertEqual(self.run_attempt()['result'],'CRITICAL_RECOVERY_UNVERIFIED')

    def test_standalone_recovery_after_crash_and_repeat_is_safe(self):
        b=baseline();h=R.sha(R.encoded(b))
        R.save_new(self.directory/'baseline.json',{'approval':self.approval,'controllerPid':12345,'baseline':b,'baselineHash':h,'approvalHash':R.sha(R.encoded(self.approval))})
        R.save_new(self.directory/'consumed.json',{'attemptId':self.ident,'baselineHash':h,'approvalHash':R.sha(R.encoded(self.approval)),'controllerPid':12345})
        # Recovery does not require controller source to exist.
        with mock.patch.dict(R.FILES,{'controller':Path(self.temp.name)/'missing-controller.py'}),mock.patch.object(R,'controller_inactive',return_value=True):
            for _ in range(2):self.assertTrue(R.recover(self.directory,self.adapter)['rollbackVerified'])
        self.assertEqual(len(list(self.directory.glob('recovery-*.json'))),2)
        self.assertEqual(self.adapter.deploy.call_count,0)

    def test_recovery_refuses_tampered_baseline_or_wrong_attempt(self):
        self.run_attempt();path=self.directory/'baseline.json';value=R.load(path)
        value['baseline']['site']['id']=R.SITE+'/slots/UI';path.write_text(json.dumps(value))
        with self.assertRaises(R.Blocked):R.recover(self.directory,self.adapter)

    def test_external_recovery_refuses_active_controller_or_reused_pid(self):
        self.run_attempt()
        self.adapter.reset_mock()
        with mock.patch.object(R,'controller_inactive',return_value=False),self.assertRaises(R.Blocked):R.recover(self.directory,self.adapter)
        self.adapter.restore.assert_not_called()

    def test_snapshot_401_validation_no_auth_no_redirect_and_503_not_success(self):
        import http.client
        sys.path.insert(0,str(ROOT/'python/presentation'))
        from research_snapshot_client import SnapshotUnavailable
        payload={'schemaVersion':'1.0','type':'error','instrument':'BTC/USD','requestEpoch':'1',
          'observedAt':R.utc().strftime('%Y-%m-%dT%H:%M:%S.%fZ'),'error':{'code':'UNAUTHORIZED','message':'Request unavailable.','retryAfterSeconds':None}}
        connection=mock.MagicMock();response=connection.getresponse.return_value;response.status=401
        response.getheader.side_effect=lambda name,default=None: {'Content-Encoding':'identity','Content-Type':'application/json'}.get(name,default)
        response.read.return_value=json.dumps(payload).encode()
        with mock.patch.object(http.client,'HTTPSConnection',return_value=connection) as create,contextlib.redirect_stdout(io.StringIO()) as output:C.snapshot_worker('BTC/USD')
        self.assertEqual(output.getvalue().strip(),'VALIDATED_401')
        self.assertTrue(create.call_args.kwargs['context'].check_hostname)
        self.assertNotIn('X-API-Key',connection.request.call_args.kwargs['headers'])
        self.assertEqual(connection.request.call_args.args[:2],('POST','/v1/research/snapshot'))
        for status,body in ((503,b'{}'),(302,b''),(401,b'<html>'),(401,b'x'*4097)):
            response.status=status;response.read.return_value=body
            with mock.patch.object(http.client,'HTTPSConnection',return_value=connection),self.assertRaises((R.Blocked,ValueError,SnapshotUnavailable)):C.snapshot_worker('BTC/USD')

    def test_recovery_default_has_no_azure_operations(self):
        with mock.patch.object(R,'Azure') as az,contextlib.redirect_stdout(io.StringIO()) as output:
            self.assertEqual(REC.main([]),0)
        az.assert_not_called();self.assertEqual(json.loads(output.getvalue())['result'],'RECOVERY_NOT_RUN')

    def test_real_adapter_cannot_access_ui_slots_or_paid_plan(self):
        az=R.Azure()
        with mock.patch.object(R,'cli') as cli:
            for method,route,resource in (('get','',R.SITE+'-ui'),('delete','',R.SITE),('post','/slots/new',R.SITE),('patch','',R.PLAN)):
                with self.assertRaises(R.Blocked):az.request(method,route,resource=resource)
        cli.assert_not_called()

    def test_real_capture_collects_all_baseline_categories_using_read_operations(self):
        b=baseline();site={'id':R.SITE,'kind':b['site']['kind'],'location':b['site']['location'],'properties':{k:v for k,v in b['site'].items() if k not in ('id','kind','location')}}
        values=[site,b['plan']]+[{'properties':b[k]} for k in ('web','settings','connections','logs','slotFlags')]+[{'value':[]}]
        with mock.patch.object(R,'cli',side_effect=values) as cli:self.assertEqual(R.Azure().capture(),b)
        self.assertEqual(cli.call_count,8)
        for call in cli.call_args_list:
            args=call.args[0];method=args[args.index('--method')+1];url=args[args.index('--url')+1]
            self.assertTrue(method=='get' or method=='post' and '/list?' in url)
            self.assertNotIn('--body',args)

    def test_capture_rejects_paid_plan_and_slot_inventory(self):
        b=baseline();site={'id':R.SITE,'kind':b['site']['kind'],'location':b['site']['location'],'properties':{k:v for k,v in b['site'].items() if k not in ('id','kind','location')}}
        with mock.patch.object(R,'cli',side_effect=[site,{'id':R.PLAN,'sku':{'name':'P1'}}]),self.assertRaises(R.Blocked):R.Azure().capture()
        b['slots']=[{'name':'unexpected'}]
        with self.assertRaises(R.Blocked):R.validate_baseline(b)

    def test_candidate_guard_settings_and_recovery_only_restore_mutated_fields(self):
        az=R.Azure();seen=[]
        with mock.patch.object(az,'request',side_effect=lambda *a,**k:seen.append((a,k))),mock.patch.object(az,'stop'):
            az.deploy(baseline())
        self.assertEqual(seen[-1][0],('post','/start'))
        settings=next(a[2] for a,k in seen if a[:2]==('put','/config/appsettings'))
        self.assertEqual(settings['AIQ_RESEARCH_PRIVATE_DIRECTORY'],'/tmp/aiq-research')
        self.assertEqual(settings['AIQ_RESEARCH_TRANSPORT'],'azure-app-service-http')
        self.assertEqual(settings['AIQ_RESEARCH_STORAGE_SCOPE'],'azure-f1-staging-ephemeral')
        self.assertEqual(settings['unrelated'],'preserve')
        self.assertNotIn('AIQ_RESEARCH_IMPORT_FILE',settings)
        self.assertEqual(settings['WEBSITES_PORT'],'52731')
        seen.clear()
        with mock.patch.object(az,'request',side_effect=lambda *a,**k: (seen.append((a,k)) or {'properties':baseline()['logs' if a[1]=='/config/logs' else 'slotFlags']}) if a[0]=='get' else seen.append((a,k))),mock.patch.object(az,'stop'):az.restore(baseline())
        self.assertEqual(next(a[2] for a,k in seen if a[:2]==('patch','/config/web')),baseline()['web'])
        self.assertEqual(seen[-1][0],('post','/start'))
        self.assertTrue(any(k.get('kind')==baseline()['site']['kind'] for a,k in seen))

    def test_management_bridge_sends_secrets_only_on_stdin_not_command_arguments(self):
        child=mock.Mock();child.pid=123;child.returncode=0;child.communicate.return_value=(b'null',b'')
        with mock.patch.dict(os.environ,{'AIQ_WP21_ATTEMPT_DIRECTORY':str(self.directory)}),mock.patch.object(Path,'is_file',return_value=True),mock.patch.object(R.subprocess,'Popen',return_value=child) as start,mock.patch.object(R,'worker_identity',return_value={'pid':123,'created':42}):
            R.cli(['rest','--method','put','--url','FIXED','--body','PRIVATE_CANARY'])
        self.assertNotIn('PRIVATE_CANARY',str(start.call_args.args));self.assertIn(b'PRIVATE_CANARY',child.communicate.call_args.kwargs['input'])
        self.assertEqual(child.communicate.call_args.kwargs['timeout'],45)
        self.assertEqual(len(list(self.directory.glob('management-worker-*.json'))),1)
        self.assertNotIn('.invoke(',R.BRIDGE);compile(R.BRIDGE,'bridge','exec')


    def test_worker_quiescence_uses_stable_birth_identity_and_never_kills_reused_pid(self):
        R.save_new(self.directory/'management-worker-one.json',{'pid':123,'created':42})
        api=mock.Mock();api.OpenProcess.return_value=100;api.TerminateProcess.return_value=True
        with mock.patch.object(R,'_process_api',return_value=api),mock.patch.object(R,'_creation',return_value=43):QUIESCE(self.directory)
        api.TerminateProcess.assert_not_called();api.CloseHandle.assert_called_with(100)
        api.reset_mock();api.OpenProcess.return_value=100;api.TerminateProcess.return_value=True;api.WaitForSingleObject.side_effect=[258,0]
        with mock.patch.object(R,'_process_api',return_value=api),mock.patch.object(R,'_creation',return_value=42):QUIESCE(self.directory)
        api.TerminateProcess.assert_called_once_with(100,125)
        api.WaitForSingleObject.assert_any_call(100,5000)

    def test_owned_harmless_local_worker_identity_and_termination(self):
        # No Azure, HTTP, PowerShell, controller or credentials; exact owned child only.
        child=subprocess.Popen([sys.executable,'-I','-c','import time; time.sleep(30)'],stdin=subprocess.DEVNULL,stdout=subprocess.DEVNULL,stderr=subprocess.DEVNULL,creationflags=subprocess.CREATE_NO_WINDOW)
        try:
            value=R.worker_identity(child.pid)
            self.assertEqual(value['pid'],child.pid);self.assertGreater(value['created'],0)
            R.save_new(self.directory/'management-worker-owned.json',value)
            QUIESCE(self.directory)
            child.wait(timeout=5)
            self.assertIsNotNone(child.returncode)
        finally:
            if child.poll() is None:child.kill();child.wait(timeout=5)

    def test_unjournaled_worker_never_receives_secret_or_management_request(self):
        child=mock.Mock();child.pid=123;child.poll.return_value=None
        with mock.patch.dict(os.environ,{'AIQ_WP21_ATTEMPT_DIRECTORY':str(self.directory)}),mock.patch.object(Path,'is_file',return_value=True),mock.patch.object(R.subprocess,'Popen',return_value=child),mock.patch.object(R,'worker_identity',return_value={'pid':123,'created':42}),mock.patch.object(R,'save_new',side_effect=OSError('PRIVATE_DISK_ERROR')),self.assertRaises(OSError):
            R.cli(['rest','--method','put','--url','FIXED','--body','PRIVATE_CANARY'])
        child.kill.assert_called_once()
        self.assertEqual(child.communicate.call_args.kwargs,{'timeout':5})

    def test_health_uses_existing_script_and_owned_parent(self):
        health={'result':'PROCESS_ALIVE','processAlive':True,'elapsedSeconds':179,'attempts':[{'outcome':'ALIVE','status':200}]}
        p=subprocess.CompletedProcess([],0,json.dumps({'researchHttp':health}).encode(),b'')
        with mock.patch.object(R.subprocess,'run',return_value=p) as run:self.assertTrue(R.health())
        self.assertEqual(run.call_args.kwargs['timeout'],200)
        self.assertEqual(run.call_args.args[0][-1],'-ProbeResearch')
        for change in ({'processAlive':False},{'elapsedSeconds':181},{'attempts':[{}]*4}):
            value={'researchHttp':{**health,**change}}
            with mock.patch.object(R.subprocess,'run',return_value=subprocess.CompletedProcess([],0,json.dumps(value).encode(),b'')),self.assertRaises(R.Blocked):R.health()

    def test_snapshot_worker_requires_health_record_before_any_http(self):
        self.run_attempt();(self.directory/'health-alive.json').unlink()
        with mock.patch.dict(os.environ,{'AIQ_WP21_ATTEMPT_DIRECTORY':str(self.directory)}),mock.patch.object(R,'private_directory'),mock.patch.object(C,'snapshot_worker') as worker,contextlib.redirect_stdout(io.StringIO()):
            self.assertEqual(C.main(['--snapshot-worker','BTC/USD']),1)
        worker.assert_not_called()

    def test_snapshot_worker_claim_is_single_use(self):
        self.run_attempt()
        with mock.patch.dict(os.environ,{'AIQ_WP21_ATTEMPT_DIRECTORY':str(self.directory)}),mock.patch.object(R,'private_directory'),mock.patch.object(C,'snapshot_worker') as worker,contextlib.redirect_stdout(io.StringIO()):
            self.assertEqual(C.main(['--snapshot-worker','BTC/USD']),0)
            self.assertEqual(C.main(['--snapshot-worker','BTC/USD']),1)
        worker.assert_called_once_with('BTC/USD')

    def test_offline_qualification_never_sets_live_ready_from_mocks(self):
        with mock.patch.object(C.subprocess,'run',return_value=subprocess.CompletedProcess([],0,b'',b'Ran 32 tests\n\nOK\n')),mock.patch.object(C.WP19,'offline_report',return_value={'result':'OFFLINE_PREFLIGHT_PASS'}),mock.patch.object(R,'Azure') as az:
            result=C.offline_report()
        self.assertTrue(result['privateControllerQualified']);self.assertFalse(result['liveAttemptReady']);az.assert_not_called()

    def test_no_tests_or_failed_suite_cannot_qualify_controller(self):
        for summary in (b'Ran 0 tests\nOK', b'Ran 32 tests\nFAILED', b''):
            with mock.patch.object(C.subprocess,'run',return_value=subprocess.CompletedProcess([],0,b'',summary)),mock.patch.object(C.WP19,'offline_report',return_value={'result':'OFFLINE_PREFLIGHT_PASS'}):
                self.assertFalse(C.offline_report()['privateControllerQualified'])

    def test_prepare_mode_never_calls_azure_or_live_mode(self):
        with mock.patch.object(C,'prepare_private_attempt',return_value={'result':'PRIVATE_ATTEMPT_PREPARED_NOT_AUTHORIZED','liveAttemptReady':False}),mock.patch.object(R,'Azure') as az,mock.patch.object(C,'run') as deploy,contextlib.redirect_stdout(io.StringIO()):
            self.assertEqual(C.main(['--prepare-private-attempt']),0)
        az.assert_not_called();deploy.assert_not_called()

    def test_private_acl_failure_blocks_live_before_azure(self):
        with mock.patch.dict(os.environ,{'AIQ_WP21_ATTEMPT_DIRECTORY':str(self.directory)}),mock.patch.object(R,'private_directory',side_effect=RuntimeError('SECRET_CANARY')),mock.patch.object(R,'Azure') as az,contextlib.redirect_stdout(io.StringIO()) as output:
            self.assertEqual(C.main(['--execute']),1)
        az.assert_not_called();self.assertNotIn('SECRET_CANARY',output.getvalue())

    def test_ps51_scripts_parse_without_policy_changes(self):
        self.assertEqual(os.name,'nt')
        for filename in ('Test-Wp21ControllerQualification.ps1','Invoke-Wp21ResearchAttempt.ps1','Restore-Wp21ResearchBaseline.ps1'):
            path=ROOT/'eng/docker/release-1.15'/filename
            command="$t=$null;$e=$null;[Management.Automation.Language.Parser]::ParseFile('"+str(path).replace("'","''")+"',[ref]$t,[ref]$e)|Out-Null;@{errors=$e.Count}|ConvertTo-Json"
            p=subprocess.run(['powershell.exe','-NoProfile','-NonInteractive','-Command',command],capture_output=True,text=True,timeout=15)
            self.assertEqual(p.returncode,0);self.assertEqual(json.loads(p.stdout)['errors'],0)
            for forbidden in ('Set-ExecutionPolicy','-ExecutionPolicy','Invoke-Expression'):self.assertNotIn(forbidden,path.read_text())


if __name__=='__main__':unittest.main()
