"""Offline wrapper preflight only; never starts the initializer or acquires data."""
import os
from pathlib import Path
import subprocess
import tempfile
import unittest

SCRIPT = Path(__file__).resolve().parents[3] / 'eng/docker/release-1.15/Invoke-Wp07ResearchImport.ps1'

@unittest.skipUnless(os.name == 'nt', 'Windows PowerShell 5.1 operator wrapper')
class ImportPreflightTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory(prefix='aiq-import-preflight-')
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        self.worker = self.root / 'AIQuantTradingResearch.Worker.dll'
        self.worker.write_bytes(b'not executable; preflight must not launch')
        self.config = self.root / 'config.json'
        self.config.write_bytes(b'{}')
        self.export = self.root / 'export.json'
        self.export.write_bytes(b'{}')
        self.state = self.root / 'absent-private'

    def invoke(self, *extra, published=None):
        env = os.environ.copy()
        for key in ('WEBSITE_INSTANCE_ID','WEBSITE_SITE_NAME','WEBSITE_HOSTNAME','WEBSITE_ROLE_INSTANCE_ID'):
            env.pop(key, None)
        p = subprocess.run(['powershell.exe','-NoProfile','-ExecutionPolicy','Bypass','-File',str(SCRIPT),
            '-PublishedResearchDirectory',str(published or self.root),'-ImportFile',str(self.export),
            '-ConfigurationFile',str(self.config),'-PrivateDirectory',str(self.state),*extra],
            capture_output=True,timeout=15,env=env)
        self.assertFalse(self.state.exists())
        self.assertNotIn(str(self.root).encode(),p.stdout+p.stderr)
        return p

    def test_default_is_nonmutating_even_with_unexecutable_worker(self):
        p=self.invoke(); self.assertEqual(p.returncode,0); self.assertIn(b'semantic validation NOT_RUN',p.stdout)

    def test_initialize_requires_execute(self):
        self.assertEqual(self.invoke('-Action','Initialize').returncode,1)

    def test_oversize_configuration(self):
        self.config.write_bytes(b'x'*4097); self.assertEqual(self.invoke().returncode,1)

    def test_oversize_export(self):
        self.export.write_bytes(b'x'*1048577); self.assertEqual(self.invoke().returncode,1)

    def test_invalid_utf8_and_no_echo(self):
        self.config.write_bytes(b'SECRET_CANARY\xff'); p=self.invoke()
        self.assertEqual(p.returncode,1); self.assertNotIn(b'SECRET_CANARY',p.stdout+p.stderr)

    def test_bom_rejected(self):
        self.export.write_bytes(b'\xef\xbb\xbf{}'); self.assertEqual(self.invoke().returncode,1)

    @unittest.skipUnless(os.environ.get('AIQ_WP07_IMPORT_PUBLISHED'), 'Opt-in existing published worker')
    def test_real_initializer_rejects_invalid_configuration_without_state(self):
        p=self.invoke('-Action','Initialize','-Execute', published=Path(os.environ['AIQ_WP07_IMPORT_PUBLISHED']))
        self.assertEqual(p.returncode,1)
        self.assertIn(b'stage=initializer',p.stdout)

    def test_missing_export(self):
        self.export.unlink(); self.assertEqual(self.invoke().returncode,1)

if __name__ == '__main__':
    unittest.main()
