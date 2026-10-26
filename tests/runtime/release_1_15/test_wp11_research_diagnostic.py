"""Offline fault injection for fixed-target rollback controller, no Azure calls."""
import importlib.util
import json
import io
import tarfile
import hashlib
from pathlib import Path
import tempfile
import time
import unittest
from unittest import mock
import zipfile

SOURCE=Path(__file__).resolve().parents[3]/'eng/azure-cli/r1.15-qualification/wp11_research_diagnostic.py'
spec=importlib.util.spec_from_file_location('wp11',SOURCE);m=importlib.util.module_from_spec(spec);spec.loader.exec_module(m)

class RollbackTests(unittest.TestCase):
    def setUp(self):
        self.temp=tempfile.TemporaryDirectory();self.addCleanup(self.temp.cleanup);self.root=Path(self.temp.name)
        self.base={'site':{'id':'/subscriptions/test/resourceGroups/'+m.GROUP+'/providers/Microsoft.Web/sites/'+m.APP,
                           'state':'Running','kind':'app,linux','httpsOnly':False},
                   'config':{'linuxFxVersion':'','appCommandLine':''},'settings':[]}
        for k,v in self.base.items():m.save(self.root,k+'.json',v)
        self.current_config=self.base['config'].copy();self.current_settings=[];self.current_site=self.base['site'].copy();self.calls=[]
        def stop():self.calls.append('stop');self.current_site['state']='Stopped'
        def start():self.calls.append('start');self.current_site['state']='Running'
        def patch(root,resource,values):
            self.assertTrue(resource.endswith('/'+m.APP));self.calls.append('config');self.current_config.update(values)
        def put(root,resource,values):
            self.assertTrue(resource.endswith('/'+m.APP));self.calls.append('settings');self.current_settings[:]=[{'name':k,'value':v} for k,v in values.items()]
        for name,fn in {'stop_verified':stop,'start_app':start,'patch_config':patch,'put_settings':put,
                        'config':lambda:self.current_config.copy(),'settings':lambda:self.current_settings.copy(),
                        'site':lambda:self.current_site.copy()}.items():
            patcher=mock.patch.object(m,name,side_effect=fn);patcher.start();self.addCleanup(patcher.stop)
        self.plan={'digest':'sha256:'+'a'*64,'immutable':'approved.invalid/research@sha256:'+'a'*64}

    def run_case(self,accepted=None,fail=False):
        def window(root,record):
            record['_started']=time.monotonic()-1
            if fail:raise RuntimeError('simulated log/start failure')
            record.update(runtimeObserved=True,guardAccepted=accepted,probeCleanup=True)
        with mock.patch.object(m,'window',side_effect=window):return m.execute_azure(self.root,self.plan)

    def test_runtime_failure_still_restores_exact_baseline(self):
        r=self.run_case(fail=True)
        self.assertTrue(r['rollbackVerified']);self.assertTrue(r['operationFailed']);self.assertEqual(r['result'],'INCOMPLETE')
        self.assertEqual(self.current_config,self.base['config']);self.assertEqual(self.current_settings,[])
        self.assertEqual(self.current_site['state'],'Running');self.assertGreaterEqual(self.calls.count('stop'),2)

    def test_acceptance_requires_rollback_and_time_bound(self):
        r=self.run_case(accepted=True)
        self.assertEqual(r['result'],'A_COMPATIBLE_ROLLED_BACK');self.assertTrue(r['within60Seconds'])

    def test_rejection_is_reported_after_restoration(self):
        r=self.run_case(accepted=False)
        self.assertEqual(r['result'],'B_REJECTED_ROLLED_BACK');self.assertTrue(r['rollbackVerified'])

    def test_rollback_failure_is_critical_not_success(self):
        with mock.patch.object(m,'restore',side_effect=RuntimeError('simulated failure')):
            r=self.run_case(accepted=True)
        self.assertFalse(r['rollbackVerified']);self.assertTrue(r['criticalRollbackUnconfirmed']);self.assertEqual(r['result'],'INCOMPLETE')

    def test_preexisting_execution_cannot_publish_or_rerun(self):
        m.save(self.root,'publication-plan.json',{'imageId':'sha256:'+'a'*64});m.save(self.root,'execution-armed.json',{})
        with mock.patch.object(m.sys,'argv',['wp11','--state',str(self.root),'--execute']),mock.patch.object(m,'publish') as publish:
            with self.assertRaises(SystemExit):m.main()
            publish.assert_not_called()

    def test_server_generated_audit_metadata_is_not_a_restorable_setting(self):
        self.assertEqual(m.comparable({'linuxFxVersion':'','systemData':{'lastModifiedAt':'new'}}),
                         m.comparable({'linuxFxVersion':'','systemData':{'lastModifiedAt':'old'}}))

    def test_raw_logs_are_reduced_to_allowlisted_types(self):
        probe=dict(linux=True,hostOptIn=True,kernel=False,privateWritable=False,accepted=False,probeCleanup=True)
        archive=self.root/'logs.zip'
        with zipfile.ZipFile(archive,'w') as z:
            z.writestr('app.log','PRIVATE_CANARY\nUID_FIELDS 1654 1654 1654 1654\nMOUNT 0:1 rootIsSlash=1 / rw,relatime overlay\nWP11_STORAGE_PROBE '+json.dumps(probe))
        r,uid,mounts=m.parse_archive(archive)
        self.assertEqual(r,probe);self.assertTrue(uid);self.assertEqual(mounts['/']['filesystem'],'overlay')
        self.assertNotIn('PRIVATE_CANARY',json.dumps([r,uid,mounts]))

    def test_early_exit_without_evidence_stays_incomplete_after_rollback(self):
        with mock.patch.object(m,'window',return_value=None):r=m.execute_azure(self.root,self.plan)
        self.assertTrue(r['rollbackVerified']);self.assertFalse(r['runtimeObserved']);self.assertEqual(r['result'],'INCOMPLETE')

    def test_watchdog_requests_exact_target_stop(self):
        m.save(self.root,'watchdog.json',{'stopAt':0})
        with mock.patch.object(m.sys,'argv',['wp11','--state',str(self.root),'--watchdog']),mock.patch.object(m,'az',return_value=None) as az:
            m.main()
        self.assertEqual(az.call_args.args,('webapp','stop','-g',m.GROUP,'-n',m.APP,'-o','none'))
        self.assertTrue(json.loads((self.root/'watchdog-fired.json').read_text())['stopRequested'])

    def test_watchdog_stop_failure_is_not_reported_as_stopped(self):
        m.save(self.root,'watchdog.json',{'stopAt':0})
        with mock.patch.object(m.sys,'argv',['wp11','--state',str(self.root),'--watchdog']),mock.patch.object(m,'az',side_effect=TimeoutError()):m.main()
        self.assertFalse(json.loads((self.root/'watchdog-fired.json').read_text())['stopRequested'])

class StopVerificationTests(unittest.TestCase):
    def test_transient_read_failure_does_not_abandon_rollback(self):
        with mock.patch.object(m,'az',return_value=None),mock.patch.object(m,'site',side_effect=[TimeoutError(),{'state':'Stopped'}]),mock.patch.object(m.time,'sleep'):
            self.assertIsInstance(m.stop_verified(),float)
    def test_stop_transport_error_still_verifies_remote_state(self):
        with mock.patch.object(m,'az',side_effect=TimeoutError()),mock.patch.object(m,'site',return_value={'state':'Stopped'}):
            self.assertIsInstance(m.stop_verified(),float)
    def test_never_claims_stop_when_running(self):
        with mock.patch.object(m,'az',return_value=None),mock.patch.object(m,'site',return_value={'state':'Running'}),mock.patch.object(m.time,'sleep'):
            with self.assertRaises(RuntimeError):m.stop_verified()

class ImageIdentityTests(unittest.TestCase):
    def setUp(self):
        self.temp=tempfile.TemporaryDirectory();self.addCleanup(self.temp.cleanup);self.path=Path(self.temp.name)/'image.tar'
        value={'os':'linux','architecture':'amd64','rootfs':{'diff_ids':['sha256:layer']},'config':{'User':'1654:1654'}}
        data=json.dumps(value).encode();self.digest='sha256:'+hashlib.sha256(data).hexdigest()
        with tarfile.open(self.path,'w') as tar:
            for name,content in [('manifest.json',json.dumps([{'Config':'config.json'}]).encode()),('config.json',data)]:
                info=tarfile.TarInfo(name);info.size=len(content);tar.addfile(info,io.BytesIO(content))
        self.local={'Id':'sha256:index','Descriptor':{'mediaType':'application/vnd.oci.image.index.v1+json'},'RootFS':{'Layers':['sha256:layer']}}
        self.manifest={'config':{'digest':self.digest}}
    def test_containerd_index_and_independent_config_both_match(self):
        m.verify_published_identity(self.local,self.manifest,'sha256:index',self.path)
    def test_different_index_rejected_even_if_config_matches(self):
        with self.assertRaises(RuntimeError):m.verify_published_identity(self.local,self.manifest,'sha256:wrong',self.path)
    def test_different_config_rejected(self):
        with self.assertRaises(RuntimeError):m.verify_published_identity(self.local,{'config':{'digest':'sha256:wrong'}},'sha256:index',self.path)
    def test_rootfs_mismatch_rejected(self):
        self.local['RootFS']['Layers']=['different']
        with self.assertRaises(RuntimeError):m.verify_published_identity(self.local,self.manifest,'sha256:index',self.path)

class EvidenceTests(unittest.TestCase):
    def setUp(self):
        self.nonce='b'*32;self.digest='sha256:'+'a'*64
        self.value={'version':1,'nonce':self.nonce,'digest':self.digest,'observedUtc':'2026-10-25T12:00:01Z',
                    'entrypoint':'research-bootstrap/diagnose','uid':[1654]*4,'rootOverlay':True,'rootWritable':True,
                    'probe':{k:True for k in m.PROBE_KEYS}}
        self.started=m.dt.datetime(2026,10,25,12,tzinfo=m.dt.timezone.utc).timestamp()
    def check(self,data=None,delay=2):
        return m.validate_evidence(json.dumps(self.value).encode() if data is None else data,self.nonce,self.digest,self.started,self.started+delay)
    def test_complete(self):self.assertTrue(self.check()['probe']['accepted'])
    def test_bad_frames(self):
        for data in (b'',b'{',b'\xff',b' '*4097):
            with self.subTest(data_length=len(data)),self.assertRaises(ValueError):self.check(data)
    def test_replay_mismatch_startup_and_inconsistency(self):
        for key,value in [('nonce','c'*32),('digest','sha256:'+'c'*64),('observedUtc','2026-10-25T11:59:59Z'),('entrypoint',''),('uid',[0]*4),('rootWritable',False),('secret','forbidden')]:
            original=self.value.copy();self.value[key]=value
            with self.subTest(field=key),self.assertRaises(ValueError):self.check()
            self.value=original
    def test_logging_delay_and_future(self):
        for delay in (0,61):
            with self.subTest(delay=delay),self.assertRaises(ValueError):self.check(delay=delay)
    def test_duplicate_fields(self):
        with self.assertRaises(ValueError):self.check(json.dumps(self.value).replace('"version": 1','"version": 1, "version": 1').encode())
    def test_preflight_blocks_before_publication_and_azure(self):
        with tempfile.TemporaryDirectory() as directory:
            root=Path(directory);m.save(root,'publication-plan.json',{'imageId':self.digest})
            with mock.patch.object(m.sys,'argv',['wp11','--state',directory,'--execute']),mock.patch.object(m,'publish') as publish,mock.patch.object(m,'execute_azure') as execute:
                with self.assertRaisesRegex(RuntimeError,'prerequisite unqualified'):m.main()
                publish.assert_not_called();execute.assert_not_called()
    def test_watchdog_is_armed_before_startup_failure(self):
        with tempfile.TemporaryDirectory() as directory:
            root=Path(directory);record={};watcher=mock.Mock()
            with mock.patch.object(m.subprocess,'Popen',return_value=watcher),mock.patch.object(m,'start_app',side_effect=TimeoutError()):
                with self.assertRaises(TimeoutError):m.window(root,record)
            self.assertEqual(json.loads((root/'watchdog.json').read_text())['stopAt'],record['_started']+30)
            self.assertIs(record['_watcher'],watcher)

if __name__=='__main__':unittest.main()
