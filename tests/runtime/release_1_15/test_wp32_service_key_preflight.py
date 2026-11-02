"""Fake-only preflight checks; actual Azure/secret operations are absent."""
import json
import os
from pathlib import Path
import shutil
import subprocess
import unittest

ROOT=Path(__file__).resolve().parents[3]
ENTRY=ROOT/'eng/docker/release-1.15/Test-Wp32ServiceKeyPreflight.ps1'

class StaticPreflightTests(unittest.TestCase):
    def test_no_secret_or_mutation_entry_surface(self):
        text=ENTRY.read_text(encoding='utf-8')
        for forbidden in ('Import-Wp31ServiceKey','Clear-Wp31ServiceKey','Read-Host','SetEnvironmentVariable',
                'Invoke-RestMethod','Invoke-WebRequest','Start-Process','Set-Clipboard','Get-Clipboard',
                'RandomNumberGenerator','--settings','Set-Content','Out-File','Set-ExecutionPolicy'):
            self.assertNotIn(forbidden.casefold(),text.casefold())
        self.assertIn('Test-Wp31ServiceKey',text)
        self.assertNotIn('$env:AIQ_WP21_SERVICE_KEY',text)  # Read only via reviewed WP31 boundary.

    def test_utf8_lf(self):
        data=ENTRY.read_bytes();data.decode('utf-8')
        self.assertNotIn(b'\r',data);self.assertFalse(data.startswith(b'\xef\xbb\xbf'))

class RuntimePreflightTests(unittest.TestCase):
    def run_entry(self,args=(),fake=None):
        ps=shutil.which('powershell.exe')
        if not ps:self.skipTest('Windows PowerShell unavailable')
        env=os.environ.copy();env.pop('AIQ_WP21_SERVICE_KEY',None)
        if fake is not None:env['AIQ_WP21_SERVICE_KEY']=fake
        result=subprocess.run([ps,'-NoProfile','-NonInteractive','-File',str(ENTRY),*args],
            capture_output=True,timeout=20,env=env)
        errors=result.stderr.decode('utf-8',errors='replace')
        if result.returncode and any(x in errors for x in ('running scripts is disabled','PSSecurityException','UnauthorizedAccess')):
            self.skipTest('Host policy blocks -File; no bypass')
        self.assertFalse(bool(errors),'Runtime emitted stderr; raw stream withheld')
        value=json.loads(result.stdout.decode('utf-8-sig'))
        for field in ('azureInvoked','configurationChangeAllowed','azureIdentityVerified','azureSettingAbsenceVerified',
                'azureSettingPresenceVerified','channelsEqualVerified','authenticatedApiVerified',
                'approvalEnabled','liveControllerInvoked','liveAttemptReady','PUBLIC_RUNTIME_VERIFIED'):
            self.assertIs(value[field],False)
        if fake:self.assertFalse(fake in result.stdout.decode('utf-8'),'Fake secret appeared; raw stream withheld')
        self.assertLess(len(result.stdout),4096)
        return result.returncode,value

    def test_default_never_reads_inherited_channel(self):
        code,value=self.run_entry(fake='a'*64)
        self.assertEqual(code,0);self.assertFalse(value['localChannelChecked'])

    def test_confirmations_missing_blocks_before_channel_check(self):
        code,value=self.run_entry(['-CheckLocalChannel'],fake='a'*64)
        self.assertEqual(code,1);self.assertFalse(value['localChannelChecked'])

    def test_valid_fake_channel_is_not_configuration_permission(self):
        code,value=self.run_entry(['-CheckLocalChannel','-ApprovedRetentionAndTransfer','-SessionSafetyReviewed'],fake='a'*64)
        self.assertEqual(code,0);self.assertTrue(value['localChannelChecked']);self.assertTrue(value['localChannelShapeValid'])

    def test_missing_and_invalid_fail_closed(self):
        for fake in (None,'INVALID_FAKE_CANARY','a'*63):
            code,value=self.run_entry(['-CheckLocalChannel','-ApprovedRetentionAndTransfer','-SessionSafetyReviewed'],fake=fake)
            self.assertEqual(code,1);self.assertTrue(value['localChannelChecked']);self.assertFalse(value['localChannelShapeValid'])

if __name__=='__main__':unittest.main()
