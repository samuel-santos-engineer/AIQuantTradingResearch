"""Offline fault injection for fixed-target rollback controller, no Azure calls."""
import importlib.util
import json
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

if __name__=='__main__':unittest.main()
