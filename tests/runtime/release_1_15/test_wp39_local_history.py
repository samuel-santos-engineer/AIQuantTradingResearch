"""Fake-only WP39 command tests. Never obtains credentials or contacts a provider."""
import importlib.util
import json
import os
from pathlib import Path
import tempfile
import unittest
import subprocess
import sys
import traceback
from unittest.mock import patch

MODULE = Path(__file__).resolve().parents[3] / 'eng/docker/release-1.15/wp39_local_history.py'
spec = importlib.util.spec_from_file_location('wp39', MODULE)
workflow = importlib.util.module_from_spec(spec); spec.loader.exec_module(workflow)


class WorkflowTests(unittest.TestCase):
    def setUp(self):
        # These existing channel/envelope tests isolate identity validation; real guard tests below do not.
        guard = patch.object(workflow, 'validate_preparation_identity')
        guard.start(); self.addCleanup(guard.stop)

    def test_plan_is_offline_and_never_claims_import(self):
        with patch.object(workflow, 'run', side_effect=AssertionError('no subprocess')):
            plan = workflow.plan()
        self.assertEqual('NEW_OWNER_PROVIDER_APPROVAL_REQUIRED',plan['status'])
        self.assertEqual(2, plan['calls'])
        self.assertEqual(0, plan['retries'])
        self.assertTrue(plan['AUTHENTIC_DATA_NOT_IMPORTED'])
        self.assertFalse(plan['PUBLIC_RUNTIME_VERIFIED'])

    def test_missing_approval_never_reads_provider_channel(self):
        with patch.object(workflow.os, 'environ') as env, patch.object(workflow, 'worker') as child:
            with self.assertRaises(RuntimeError): workflow.acquire(Path('unused'), False, True)
            env.get.assert_not_called(); child.assert_not_called()

    def test_unknown_volume_capability_never_reads_provider_channel(self):
        with patch.object(workflow.os, 'environ') as env, patch.object(workflow, 'worker') as child:
            with self.assertRaises(RuntimeError): workflow.acquire(Path('unused'), True, False)
            env.get.assert_not_called(); child.assert_not_called()

    def test_missing_private_channel_never_launches_child(self):
        with patch.dict(os.environ, {}, clear=True), patch.object(workflow, 'worker') as child:
            with self.assertRaises(RuntimeError): workflow.acquire(Path('unused'), True, True)
            child.assert_not_called()

    def test_fake_key_uses_stdin_not_child_environment_or_arguments(self):
        canary = 'FAKE_LOCAL_PROVIDER_CANARY'
        with patch.dict(os.environ, {'AIQ_WP39_PROVIDER_KEY': canary}), patch.object(workflow, 'worker') as child, patch('builtins.print'):
            workflow.acquire(Path('unused'), True, True)
            args = child.call_args.args
            self.assertNotIn(canary, str(args[:2]))
            self.assertNotIn('AIQ_WP39_PROVIDER_KEY', args[2])
            self.assertEqual(canary, json.loads(args[3])['providerKey'])
            self.assertEqual(canary, os.environ['AIQ_WP39_PROVIDER_KEY'])

    def test_child_failure_does_not_clear_owner_channel(self):
        with patch.dict(os.environ, {'AIQ_WP39_PROVIDER_KEY': 'FAKE'}), patch.object(workflow, 'worker', side_effect=RuntimeError('fixed')):
            with self.assertRaises(RuntimeError): workflow.acquire(Path('unused'), True, True)
            self.assertEqual('FAKE', os.environ['AIQ_WP39_PROVIDER_KEY'])

    def test_fixture_is_never_imported_as_authentic(self):
        with patch.object(workflow, 'worker', return_value='SyntheticFixture') as child:
            with self.assertRaises(RuntimeError): workflow.import_bundle(Path('unused'))
            self.assertEqual(1, child.call_count)

    def test_prepare_never_overwrites_existing_directory(self):
        with tempfile.TemporaryDirectory() as temp, patch.object(workflow, 'run') as commands:
            with self.assertRaises(ValueError): workflow.prepare(Path(temp))
            commands.assert_not_called()


class AcquisitionDiagnosticTests(unittest.TestCase):
    def setUp(self):
        # These existing channel/envelope tests isolate identity validation; real guard tests below do not.
        guard = patch.object(workflow, 'validate_preparation_identity')
        guard.start(); self.addCleanup(guard.stop)

    CANARY = 'FAKE-STDIN-SECRET-CANARY-ONLY'

    @staticmethod
    def valid():
        return dict(version=1, event='WP39_ACQUIRE_FAILED', failedStage='http_response', instrument='BTC/USD',
                    httpStatus=429, requestsStarted=1, responsesReceived=1, outcomeUnknown=False,
                    failureCode='HTTP_RATE_LIMITED', bundlePublished=False)

    def check_unknown(self, failure, code):
        value=failure.diagnostic
        self.assertEqual(code,value['failureCode'])
        self.assertIsNone(value['httpStatus']);self.assertIsNone(value['requestsStarted']);self.assertIsNone(value['responsesReceived'])
        self.assertTrue(value['outcomeUnknown']);self.assertIsNone(value['bundlePublished'])
        self.assertNotIn(self.CANARY,json.dumps(value))

    def test_strict_child_schema_accepts_observed_facts(self):
        value=self.valid()
        self.assertEqual(value,workflow.child_failure(json.dumps(value).encode()).diagnostic)

    def test_volume_subtypes_and_legacy_code_round_trip_without_schema_widening(self):
        codes = ('VOLUME_MISSING', 'VOLUME_NULL', 'VOLUME_WRONG_TYPE',
                 'VOLUME_INVALID_FORMAT', 'VOLUME_PRECISION_EXCEEDED', 'VOLUME_OUT_OF_RANGE', 'INVALID_VOLUME')
        for code in codes:
            for count, symbol in ((1, 'BTC/USD'), (2, 'ETH/USD')):
                value = dict(self.valid(), failedStage='observation_validation', failureCode=code,
                             instrument=symbol, httpStatus=200, requestsStarted=count, responsesReceived=count)
                with self.subTest(code=code, symbol=symbol):
                    self.assertEqual(value, workflow.child_failure(json.dumps(value).encode()).diagnostic)
                    for mutation in ({'extra': self.CANARY}, {'failureCode': code + self.CANARY},
                                     {'requestsStarted': True}, {'bundlePublished': True}):
                        self.check_unknown(workflow.child_failure(json.dumps(dict(value, **mutation)).encode()),
                                           'CHILD_DIAGNOSTIC_INVALID')

    def test_malformed_forged_or_secret_bearing_diagnostics_are_replaced(self):
        cases=[b'',b'not-json',self.CANARY.encode(),b'{}',b'[]',b'null',b'\xff',
               json.dumps(self.valid()).encode()+b'\n'+self.CANARY.encode()]
        for key,value in [('version',True),('version',2),('instrument',self.CANARY),('failureCode',self.CANARY),
                          ('failedStage',self.CANARY),('httpStatus',True),('httpStatus',None),('httpStatus',200),
                          ('requestsStarted',True),('requestsStarted',3),('responsesReceived',2),
                          ('outcomeUnknown',True),('bundlePublished',True),('extra',self.CANARY)]:
            cases.append(json.dumps(dict(self.valid(),**{key:value})).encode())
        cases.append(json.dumps(self.valid()).replace('"version": 1','"version": 1,"version": 1').encode())
        for raw in cases:
            with self.subTest(length=len(raw)):
                self.check_unknown(workflow.child_failure(raw),'CHILD_DIAGNOSTIC_INVALID')

    def test_oversized_diagnostic_is_not_parsed_or_forwarded(self):
        self.check_unknown(workflow.parent_failure(self.CANARY,self.CANARY),'UNKNOWN')
        self.check_unknown(workflow.child_failure(self.CANARY.encode()*100),'CHILD_OUTPUT_OVERSIZE')

    def test_child_launch_failure_never_infers_provider_counts_or_echoes_exception(self):
        for error,code in [(FileNotFoundError(self.CANARY),'CHILD_EXECUTABLE_UNAVAILABLE'),(OSError(self.CANARY),'CHILD_LAUNCH_FAILED')]:
            with self.subTest(code=code),patch.object(workflow.subprocess,'Popen',side_effect=error):
                try: workflow.run(['absent-fake-worker'],data=b'{}',acquisition=True)
                except workflow.AcquisitionFailure as failure:
                    self.check_unknown(failure,code)
                    self.assertNotIn(self.CANARY,''.join(traceback.format_exception(failure)))
                else: self.fail('Launch unexpectedly succeeded')

    def stub(self, mode, timeout=5, diagnostic=None):
        # Child source has no embedded key. Fake canary arrives only via bounded stdin.
        source = """import json,sys,time
payload=json.load(sys.stdin)
mode=sys.argv[1]
if mode=='timeout':time.sleep(60)
if mode=='valid':print(json.dumps(payload['diagnostic']),file=sys.stderr)
if mode=='malformed':print(payload['canary'],file=sys.stderr)
if mode=='oversize':print(payload['canary']*10000,file=sys.stderr,flush=True)
if mode=='success':
 print(payload['canary'])
 print(payload['canary'],file=sys.stderr)
if mode=='nonzero':raise SystemExit(42)
raise SystemExit(0 if mode=='success' else 1)
"""
        with tempfile.TemporaryDirectory(prefix='wp39-fake-child-') as temp:
            path=Path(temp)/'fake_child.py';path.write_text(source,encoding='utf-8',newline='\n')
            args=[sys.executable,'-IB',str(path),mode]
            self.assertNotIn(self.CANARY,str(args))
            return workflow.run(args, data=json.dumps(dict(canary=self.CANARY,diagnostic=diagnostic or self.valid())).encode(), acquisition=True,timeout=timeout)

    def test_oversized_private_input_never_launches_a_child(self):
        with patch.object(workflow.subprocess,'Popen') as launch:
            with self.assertRaises(workflow.AcquisitionFailure) as caught:
                workflow.run(['unlaunched-fake-child'],data=b'x'*4097,acquisition=True)
            launch.assert_not_called()
            self.assertEqual('INVALID_PRIVATE_INPUT',caught.exception.diagnostic['failureCode'])
            self.assertEqual(0,caught.exception.diagnostic['requestsStarted'])
            self.assertFalse(caught.exception.diagnostic['outcomeUnknown'])

    def test_wp42_fixed_codes_round_trip_and_reject_external_text(self):
        codes = ('INVALID_JSON_ENCODING','INVALID_PAYLOAD_SHAPE','PROVIDER_ERROR_ENVELOPE',
                 'INVALID_PROVIDER_STATUS','PROVIDER_SYMBOL_MISMATCH','PROVIDER_INTERVAL_MISMATCH',
                 'PROVIDER_TIMEZONE_MISMATCH','INVALID_CANDLE_COLLECTION','PAYLOAD_SIZE_LIMIT',
                 'INVALID_CANDLE_TIMESTAMP','INVALID_OHLC_ENCODING','INVALID_METADATA_OBJECT',
                 'INVALID_METADATA_SYMBOL_FIELD','INVALID_METADATA_INTERVAL_FIELD','INVALID_METADATA_TIMEZONE_FIELD')
        for code in codes:
            with self.subTest(code=code):
                value=self.valid(); value.update(failureCode=code,failedStage='response_parse')
                raw=json.dumps(value).encode()
                self.assertEqual(value,workflow.child_failure(raw).diagnostic)
                with self.assertRaises(workflow.AcquisitionFailure) as caught:self.stub('valid',diagnostic=value)
                self.assertEqual(value,caught.exception.diagnostic)
                self.assertNotIn(self.CANARY,''.join(traceback.format_exception(caught.exception)))
                value['failureCode']=code+self.CANARY
                self.check_unknown(workflow.child_failure(json.dumps(value).encode()),'CHILD_DIAGNOSTIC_INVALID')

    def test_owned_fake_child_diagnostic_survives_parent_without_freeform_output(self):
        with self.assertRaises(workflow.AcquisitionFailure) as caught:self.stub('valid')
        self.assertEqual(self.valid(),caught.exception.diagnostic)

    def test_owned_fake_child_nonzero_without_diagnostic_is_unknown(self):
        with self.assertRaises(workflow.AcquisitionFailure) as caught:self.stub('nonzero')
        self.check_unknown(caught.exception,'CHILD_NONZERO_EXIT')

    def test_owned_fake_child_timeout_is_killed_and_remains_unknown(self):
        with self.assertRaises(workflow.AcquisitionFailure) as caught:self.stub('timeout',timeout=.1)
        self.check_unknown(caught.exception,'CHILD_TIMEOUT')

    def test_owned_fake_child_secret_stderr_is_rejected_not_forwarded(self):
        with self.assertRaises(workflow.AcquisitionFailure) as caught:self.stub('malformed')
        self.check_unknown(caught.exception,'CHILD_DIAGNOSTIC_INVALID')

    def test_owned_fake_child_overflow_is_bounded_and_killed(self):
        with self.assertRaises(workflow.AcquisitionFailure) as caught:self.stub('oversize')
        self.check_unknown(caught.exception,'CHILD_OUTPUT_OVERSIZE')

    def test_owned_fake_child_success_discards_both_outputs(self):
        self.assertEqual('',self.stub('success'))

    def test_acquire_parent_diagnostic_is_single_and_contains_no_secret(self):
        result=subprocess.run([sys.executable,'-IB',str(MODULE),'--action','Acquire','--directory','unused-local-target'],
                              env={},capture_output=True,timeout=10)
        self.assertEqual(1,result.returncode);self.assertEqual(b'',result.stdout)
        self.assertEqual(1,len(result.stderr.splitlines()))
        value=json.loads(result.stderr)
        self.assertEqual('MISSING_APPROVAL',value['failureCode']);self.assertEqual(0,value['requestsStarted'])
        self.assertFalse(value['outcomeUnknown']);self.assertFalse(value['bundlePublished'])

    def test_fake_key_is_private_stdin_even_through_worker_boundary(self):
        failure=workflow.child_failure(json.dumps(self.valid()).encode())
        with patch.dict(os.environ,{'AIQ_WP39_PROVIDER_KEY':self.CANARY},clear=True),patch.object(workflow,'run',side_effect=failure) as runner:
            with self.assertRaises(workflow.AcquisitionFailure):workflow.acquire(Path('unused'),True,True)
            args=runner.call_args.args[0];kwargs=runner.call_args.kwargs
            self.assertNotIn(self.CANARY,str(args));self.assertTrue(kwargs['acquisition'])
            self.assertNotIn('AIQ_WP39_PROVIDER_KEY',kwargs['env'])
            self.assertEqual(self.CANARY,json.loads(kwargs['data'])['providerKey'])
            self.assertEqual(self.CANARY,os.environ['AIQ_WP39_PROVIDER_KEY'])
            self.assertNotIn(self.CANARY,str(failure))

    def test_parent_preflight_codes_have_proven_zero_requests(self):
        for approval,volume,code in [(False,True,'MISSING_APPROVAL'),(True,False,'VOLUME_UNCONFIRMED'),(True,True,'MISSING_PRIVATE_CHANNEL')]:
            with self.subTest(code=code),patch.dict(os.environ,{},clear=True):
                with self.assertRaises(workflow.AcquisitionFailure) as caught:workflow.acquire(Path('unused'),approval,volume)
                value=caught.exception.diagnostic
                self.assertEqual(code,value['failureCode']);self.assertEqual(0,value['requestsStarted']);self.assertEqual(0,value['responsesReceived'])
                self.assertFalse(value['outcomeUnknown']);self.assertFalse(value['bundlePublished'])


class PreparationIdentityTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name) / 'owned'
        for name in ('publish', 'context/research'):
            target = self.root / name
            target.mkdir(parents=True)
            for file, data in {'AIQuantTradingResearch.Worker.dll': b'FAKE-WORKER',
                               'AIQuantTradingResearch.Worker.deps.json': b'{}',
                               'AIQuantTradingResearch.Worker.runtimeconfig.json': b'{}',
                               'runtimes/linux-x64/native/fake.so': b'FAKE-NATIVE'}.items():
                path = target / file; path.parent.mkdir(parents=True, exist_ok=True); path.write_bytes(data)
        workflow.write_preparation_identity(self.root)
        self.manifest = self.root / workflow.IDENTITY_FILE

    def blocked(self):
        before = {p.relative_to(self.root): p.read_bytes() for p in self.root.rglob('*')
                  if p.is_file() and not p.is_symlink() and not p.is_junction()}
        with patch.object(workflow.os, 'environ') as env, patch.object(workflow, 'worker') as child, \
             patch.object(workflow.subprocess, 'Popen') as process:
            with self.assertRaises(workflow.AcquisitionFailure) as caught:
                workflow.acquire(self.root, True, True)
            env.get.assert_not_called(); env.copy.assert_not_called(); child.assert_not_called(); process.assert_not_called()
        value = caught.exception.diagnostic
        self.assertEqual(dict(version=1,event='WP39_ACQUIRE_FAILED',failedStage='preflight',instrument=None,
            httpStatus=None,requestsStarted=0,responsesReceived=0,outcomeUnknown=False,
            failureCode='PREPARATION_IDENTITY_INVALID',bundlePublished=False), value)
        self.assertNotIn(str(self.root), json.dumps(value)); self.assertNotIn('FAKE-WORKER',json.dumps(value))
        for relative, data in before.items(): self.assertEqual(data, (self.root / relative).read_bytes())
        self.assertFalse((self.root / 'bundle').exists()); self.assertFalse((self.root / 'import-receipt.json').exists())

    def test_fresh_pair_is_deterministic_portable_and_reaches_channel_boundary(self):
        value = json.loads(self.manifest.read_bytes())
        self.assertEqual(value, workflow.preparation_identity(self.root))
        self.assertNotIn(str(self.root), self.manifest.read_text())
        self.assertIn('runtimes/linux-x64/native/fake.so',value['publication'])
        self.assertTrue(self.manifest.read_bytes().endswith(b'\n'))
        self.assertNotIn(b'\r',self.manifest.read_bytes())
        with patch.object(workflow.os,'environ') as env, patch.object(workflow,'worker') as child:
            env.get.return_value = None
            with self.assertRaises(workflow.AcquisitionFailure) as caught:workflow.acquire(self.root,True,True)
            self.assertEqual('MISSING_PRIVATE_CHANNEL',caught.exception.diagnostic['failureCode'])
            env.get.assert_called_once_with('AIQ_WP39_PROVIDER_KEY');child.assert_not_called()

    def test_stale_swapped_or_missing_actual_publication_is_rejected(self):
        for relative in ('publish/AIQuantTradingResearch.Worker.dll','context/research/AIQuantTradingResearch.Worker.dll',
                         'publish/AIQuantTradingResearch.Worker.deps.json','publish/runtimes/linux-x64/native/fake.so'):
            path=self.root/relative;original=path.read_bytes()
            with self.subTest(relative=relative):
                path.write_bytes(b'FAKE-STALE');self.blocked();path.write_bytes(original)
        worker=self.root/'publish/AIQuantTradingResearch.Worker.dll'
        worker.unlink();self.blocked()

    def test_changed_executing_workflow_is_rejected(self):
        other=Path(self.temp.name)/'different.py';other.write_bytes(b'# fake changed workflow\n')
        with patch.object(workflow,'__file__',str(other)):self.blocked()

    def test_loaded_workflow_and_disk_source_must_agree(self):
        with patch.object(workflow,'_LOADED_WORKFLOW_HASH','0'*64):self.blocked()

    def test_dispatch_recheck_stops_change_after_initial_validation(self):
        calls=[]
        path=self.root/'publish/AIQuantTradingResearch.Worker.dll'
        class FakeEnvironment(dict):
            def get(self,key,default=None):
                calls.append(key);path.write_bytes(b'FAKE-CHANGED-AFTER-CHECK')
                return super().get(key,default)
        fake=FakeEnvironment(AIQ_WP39_PROVIDER_KEY='FAKE-ONLY-KEY')
        with patch.object(workflow.os,'environ',fake),patch.object(workflow,'worker') as child:
            with self.assertRaises(workflow.AcquisitionFailure) as caught:workflow.acquire(self.root,True,True)
            self.assertEqual('PREPARATION_IDENTITY_INVALID',caught.exception.diagnostic['failureCode'])
            self.assertEqual(0,caught.exception.diagnostic['requestsStarted']);child.assert_not_called()
        self.assertEqual(['AIQ_WP39_PROVIDER_KEY'],calls)
        self.assertEqual('FAKE-ONLY-KEY',fake['AIQ_WP39_PROVIDER_KEY'])
        self.assertNotIn('FAKE-ONLY-KEY',json.dumps(caught.exception.diagnostic))

    def test_old_preparation_without_manifest_fails_without_upgrade(self):
        self.manifest.unlink();self.blocked();self.assertFalse(self.manifest.exists())

    def test_manifest_strict_schema_hash_size_and_path_aliases(self):
        original=self.manifest.read_bytes();value=json.loads(original)
        cases=[b'{}',b'[]',b'null',b'\xff',b'{' ,b' '* (workflow.IDENTITY_LIMIT+1),
               original.replace(b'"version":1',b'"version":1,"version":1')]
        for mutation in ({'version':True},{'version':2},{'extra':'FAKE-SECRET'},
                         {'workflowSha256':'0'*64},{'workflowSha256':'bad'},
                         {'publication':{'../outside.dll':'0'*64}},
                         {'publication':{'C:\\outside.dll':'0'*64}},
                         {'publication':{**value['publication'],'../outside.dll':'0'*64}},
                         {'publication':{**value['publication'],'runtimes\\linux-x64\\fake.so':'0'*64}}):
            cases.append(json.dumps(dict(value,**mutation)).encode())
        for raw in cases:
            with self.subTest(size=len(raw)):
                self.manifest.write_bytes(raw);self.blocked()
        self.manifest.write_bytes(original)

    def test_added_removed_and_case_alias_files_fail(self):
        extra=self.root/'publish/extra.dll';extra.write_bytes(b'fake');self.blocked();extra.unlink()
        value=json.loads(self.manifest.read_bytes())
        value['publication']['aiquanttradingresearch.worker.dll']=value['publication']['AIQuantTradingResearch.Worker.dll']
        self.manifest.write_text(json.dumps(value),encoding='utf-8',newline='\n');self.blocked()

    def test_existing_destination_is_preserved_by_identity_rejection(self):
        bundle=self.root/'bundle';bundle.mkdir();(bundle/'sentinel').write_bytes(b'preserve')
        self.manifest.unlink()
        with patch.object(workflow.os,'environ') as env,patch.object(workflow,'worker') as child:
            with self.assertRaises(workflow.AcquisitionFailure):workflow.acquire(self.root,True,True)
            env.get.assert_not_called();child.assert_not_called()
        self.assertEqual(b'preserve',(bundle/'sentinel').read_bytes())

    def test_linked_publication_or_manifest_fails_before_reads(self):
        target=Path(self.temp.name)/'outside';target.write_bytes(b'FAKE-OUTSIDE')
        path=self.root/'publish/AIQuantTradingResearch.Worker.dll';original=path.read_bytes();path.unlink()
        try:path.symlink_to(target)
        except OSError:
            path.write_bytes(original)
            self.skipTest('Host does not permit unprivileged symlink creation')
        with patch.object(workflow.os,'environ') as env,patch.object(workflow,'worker') as child:
            with self.assertRaises(workflow.AcquisitionFailure):workflow.acquire(self.root,True,True)
            env.get.assert_not_called();child.assert_not_called()
        path.unlink();path.write_bytes(original)
        original_manifest=self.manifest.read_bytes();self.manifest.unlink();self.manifest.symlink_to(target)
        with self.assertRaises(workflow.AcquisitionFailure):workflow.validate_preparation_identity(self.root)
        self.manifest.unlink();self.manifest.write_bytes(original_manifest)
        context=self.root/'context/research';moved=self.root/'copied';context.rename(moved);context.symlink_to(moved,target_is_directory=True)
        with self.assertRaises(workflow.AcquisitionFailure):workflow.validate_preparation_identity(self.root)
        context.unlink();moved.rename(context)

    def test_windows_reparse_attribute_is_rejected_on_any_host(self):
        info=workflow.os.stat_result((0o100644,0,0,1,0,0,1,0,0,0))
        from types import SimpleNamespace
        with patch.object(Path,'lstat',return_value=SimpleNamespace(st_mode=info.st_mode,st_file_attributes=0x400)):
            with self.assertRaises(ValueError):workflow.identity_path(self.root)

    def test_bounded_publication_and_exclusive_manifest_write(self):
        with self.assertRaises(FileExistsError):workflow.write_preparation_identity(self.root)
        with patch.object(workflow,'PUBLICATION_FILES',2):self.blocked()
        with patch.object(workflow,'PUBLICATION_BYTES',1):self.blocked()

    def test_copy_must_match_before_manifest_can_be_created(self):
        self.manifest.unlink();(self.root/'context/research/AIQuantTradingResearch.Worker.dll').write_bytes(b'bad copy')
        with self.assertRaises(ValueError):workflow.write_preparation_identity(self.root)
        self.assertFalse(self.manifest.exists())

    def test_prepare_writes_guard_only_at_successful_end_without_docker(self):
        # Existing Prepare orchestrator runs entirely against fixture/mocked process boundaries.
        self.manifest.unlink()
        with patch.object(workflow,'run') as run,patch.object(workflow,'private_directory',side_effect=lambda path:path.mkdir()),patch.object(workflow,'worker'), \
             patch.object(workflow.shutil,'copytree'),patch.object(workflow.shutil,'copyfile'), \
             patch.object(workflow,'write_preparation_identity') as write:
            new=Path(self.temp.name)/'new'
            run.side_effect=lambda args,**kw: json.dumps([{'Id':workflow.DEPENDENCIES}]) if args[:3]==['docker','image','inspect'] else ''
            with patch('builtins.print'):workflow.prepare(new)
            write.assert_called_once_with(new)
        with patch.object(workflow,'run',side_effect=RuntimeError('fake build failure')), \
             patch.object(workflow,'write_preparation_identity') as write:
            with self.assertRaises(RuntimeError):workflow.prepare(Path(self.temp.name)/'failed')
            write.assert_not_called()


if __name__ == '__main__': unittest.main()
