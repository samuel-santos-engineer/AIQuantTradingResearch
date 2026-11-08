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


if __name__ == '__main__': unittest.main()
