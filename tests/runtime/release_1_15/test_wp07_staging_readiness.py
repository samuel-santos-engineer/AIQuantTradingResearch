"""Offline assessment tests. Fixtures are metadata, never Azure acceptance evidence."""
import json
import os
from pathlib import Path
import subprocess
import unittest

SCRIPT = Path(__file__).resolve().parents[3] / 'eng/docker/release-1.15/Test-Wp07StagingChartReadiness.ps1'

@unittest.skipUnless(os.name == 'nt', 'Windows PowerShell5.1')
class StagingReadinessTests(unittest.TestCase):
    def ps(self, code):
        p=subprocess.run(['powershell.exe','-NoProfile','-ExecutionPolicy','Bypass','-Command',
            "$ErrorActionPreference='Stop'; . '"+str(SCRIPT).replace("'","''")+"'; "+code],
            capture_output=True,text=True,timeout=15)
        self.assertEqual(p.returncode,0,p.stderr)
        return json.loads(p.stdout)

    def assessment(self, state='Running', https=True, image=True, names=None):
        ui=json.dumps(dict(state=state,httpsOnly=https,imageAssigned=image))
        research=json.dumps(dict(state='Running',httpsOnly=False,imageAssigned=False))
        names=names if names is not None else ['AIQ_RESEARCH_CHART','AIQ_RESEARCH_BASE_URL','AIQ_RESEARCH_API_KEY']
        return self.ps("$u='"+ui+"'|ConvertFrom-Json; $r='"+research+"'|ConvertFrom-Json; $n=@('"+"','".join(names)+"'); Get-Wp07ConfigurationAssessment $u $r $n | ConvertTo-Json")

    def test_config_presence_never_proves_runtime_or_storage(self):
        r=self.assessment()
        self.assertTrue(r['uiConfigurationPresent'])
        self.assertFalse(r['appServiceDurableWriterSupported'])
        self.assertFalse(r['compatibleResearchEndpointVerified'])
        self.assertEqual(r['chartAcceptance'],'NOT_RUN')
        self.assertFalse(r['PUBLIC_RUNTIME_VERIFIED'])

    def test_running_shell_and_legacy_ui_are_not_ready(self):
        r=self.assessment(names=['WEBSITES_PORT'])
        self.assertTrue(r['researchShellRunning'])
        self.assertFalse(r['researchImageAssigned'])
        self.assertEqual(r['missingUiResearchSettingCount'],3)
        self.assertFalse(r['uiConfigurationPresent'])

    def test_stopped_ui(self):
        self.assertFalse(self.assessment(state='Stopped')['uiConfigurationPresent'])

    def test_https_disabled(self):
        self.assertFalse(self.assessment(https=False)['uiConfigurationPresent'])

    def test_missing_image(self):
        self.assertFalse(self.assessment(image=False)['uiConfigurationPresent'])

    def test_default_no_network(self):
        p=subprocess.run(['powershell.exe','-NoProfile','-ExecutionPolicy','Bypass','-File',str(SCRIPT)],
            capture_output=True,text=True,timeout=15)
        self.assertEqual(p.returncode,0,p.stderr)
        r=json.loads(p.stdout)
        self.assertEqual(r['management'],'NOT_RUN')
        self.assertEqual(r['publicHttp'],'NOT_RUN')
        self.assertFalse(r['PUBLIC_RUNTIME_VERIFIED'])

    def test_unapproved_probe_route_rejected_before_network(self):
        r=self.ps("try {Get-Wp07PublicUiProbe '/api/v1/anything'; throw 'unexpected'} catch { @{rejected=($_.Exception.Message -eq 'Unapproved route.')}|ConvertTo-Json }")
        self.assertTrue(r['rejected'])

    def test_ps51_parse(self):
        r=self.ps("$t=$null; $e=$null;[Management.Automation.Language.Parser]::ParseFile('"+str(SCRIPT).replace("'","''")+"',[ref]$t,[ref]$e)|Out-Null; @{errors=$e.Count}|ConvertTo-Json")
        self.assertEqual(r['errors'],0)

if __name__ == '__main__':
    unittest.main()
