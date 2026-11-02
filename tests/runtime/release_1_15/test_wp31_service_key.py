"""Offline PowerShell tests: no operator input, real key, Azure or policy bypass."""
import json
import os
from pathlib import Path
import shutil
import subprocess
import unittest

ROOT=Path(__file__).resolve().parents[3]
HELPER=ROOT/'eng/docker/release-1.15/Wp31-ServiceKey.ps1'
TEST=HELPER.with_name('Test-Wp31ServiceKey.ps1')

class ServiceKeyTests(unittest.TestCase):
    def test_static_secret_and_mutation_surface(self):
        source=HELPER.read_text(encoding='utf-8')
        self.assertIn('Read-Host',source);self.assertIn('-AsSecureString',source)
        self.assertIn('ZeroFreeBSTR',source);self.assertIn('$secure.Dispose()',source)
        self.assertIn('[EnvironmentVariableTarget]::Process',source)
        for forbidden in ('::User','::Machine','Set-Clipboard','Get-Clipboard','setx','Invoke-Expression',
                'Start-Process','Invoke-RestMethod','Invoke-WebRequest','Set-Content','Out-File','Write-Host'):
            self.assertNotIn(forbidden.casefold(),source.casefold())
        self.assertNotIn('RandomNumberGenerator',source)  # No real secret generation path.
        self.assertIn("$MyInvocation.InvocationName -ne '.'",source)

    def test_authored_scripts_utf8_lf(self):
        for path in (HELPER,TEST):
            data=path.read_bytes();data.decode('utf-8')
            self.assertNotIn(b'\r',data);self.assertFalse(data.startswith(b'\xef\xbb\xbf'))

    def test_powershell51_fake_input_execution(self):
        ps=shutil.which('powershell.exe')
        if not ps:self.skipTest('Windows PowerShell unavailable')
        # No secret is passed through arguments. Test refuses an inherited channel.
        env=os.environ.copy();env.pop('AIQ_WP21_SERVICE_KEY',None)
        result=subprocess.run([ps,'-NoProfile','-NonInteractive','-File',str(TEST)],
            capture_output=True,timeout=30,env=env)
        errors=result.stderr.decode('utf-8',errors='replace')
        if result.returncode and any(code in errors for code in ('running scripts is disabled','PSSecurityException','UnauthorizedAccess')):
            self.skipTest('Host execution policy blocked -File; no bypass attempted')
        self.assertEqual(result.returncode,0,'Helper execution failed; raw streams withheld')
        value=json.loads(result.stdout.decode('utf-8-sig'))
        self.assertEqual(value['result'],'OFFLINE_HELPER_TESTS_PASS')
        self.assertEqual(value['fakeInputCases'],13)
        self.assertFalse(value['actualKeyUsed']);self.assertFalse(value['azureInvoked'])

if __name__=='__main__':unittest.main()
