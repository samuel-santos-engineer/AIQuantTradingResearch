"""WP33 fake-only same-session handoff tests; no owner key or generator access."""
import os
import json
from pathlib import Path
import shutil
import subprocess
import unittest

ROOT=Path(__file__).resolve().parents[3]
HELPER=ROOT/'eng/docker/release-1.15/Wp33-OwnerKeyHandoff.ps1'
TEST=HELPER.with_name('Test-Wp33OwnerKeyHandoff.ps1')

class OwnerHandoffTests(unittest.TestCase):
    def test_no_secret_argument_or_external_side_effect_surface(self):
        text=HELPER.read_text(encoding='utf-8')
        self.assertIn('Get-Variable -Name key -Scope 1 -ValueOnly -ErrorAction Stop',text)
        self.assertIn('Clear-Wp31ServiceKey',text)
        self.assertIn('Test-Wp31ServiceKey',text)
        self.assertIn("$MyInvocation.InvocationName -ne '.'",text)
        for forbidden in ('Read-Host','RandomNumberGenerator','generate-key.ps1','::User','::Machine',
                'Set-Clipboard','Get-Clipboard','Start-Process','Invoke-Expression','Invoke-RestMethod',
                'Invoke-WebRequest','Set-Content','Out-File','Write-Host','Set-ExecutionPolicy'):
            self.assertNotIn(forbidden.casefold(),text.casefold())
        self.assertNotIn('$Key,',text);self.assertNotIn('[string]$Key',text)

    def test_utf8_lf(self):
        for path in (HELPER,TEST):
            data=path.read_bytes();data.decode('utf-8')
            self.assertNotIn(b'\r',data);self.assertFalse(data.startswith(b'\xef\xbb\xbf'))

    def test_powershell51_fake_handoff(self):
        ps=shutil.which('powershell.exe')
        if not ps:self.skipTest('Windows PowerShell unavailable')
        env=os.environ.copy();env.pop('AIQ_WP21_SERVICE_KEY',None)
        result=subprocess.run([ps,'-NoProfile','-NonInteractive','-File',str(TEST)],
            capture_output=True,timeout=30,env=env)
        errors=result.stderr.decode('utf-8',errors='replace')
        if result.returncode and any(x in errors for x in ('running scripts is disabled','PSSecurityException','UnauthorizedAccess')):
            self.skipTest('Host policy blocks -File; no bypass')
        self.assertEqual(result.returncode,0,'Fake handoff failed; raw streams withheld')
        self.assertFalse(bool(errors),'Unexpected stderr withheld')
        value=json.loads(result.stdout.decode('utf-8-sig'))
        self.assertEqual(value['result'],'OFFLINE_OWNER_HANDOFF_TESTS_PASS')
        self.assertEqual(value['fakeCases'],14)
        self.assertFalse(value['actualKeyUsed']);self.assertFalse(value['azureInvoked'])
        self.assertFalse('a'*64 in result.stdout.decode('utf-8'),'Fake canary appeared; withheld')

if __name__=='__main__':unittest.main()
