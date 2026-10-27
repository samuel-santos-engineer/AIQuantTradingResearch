"""Offline assessment tests. Fixtures are metadata, never Azure acceptance evidence."""
import json
import importlib.util
import io
from contextlib import redirect_stdout
from unittest import mock
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
        self.assertEqual(r['researchHttp'],'NOT_RUN')
        self.assertFalse(r['PUBLIC_RUNTIME_VERIFIED'])

    def test_unapproved_probe_route_rejected_before_network(self):
        r=self.ps("try {Get-Wp07PublicUiProbe '/api/v1/anything'; throw 'unexpected'} catch { @{rejected=($_.Exception.Message -eq 'Unapproved route.')}|ConvertTo-Json }")
        self.assertTrue(r['rejected'])

    def test_ps51_parse(self):
        r=self.ps("$t=$null; $e=$null;[Management.Automation.Language.Parser]::ParseFile('"+str(SCRIPT).replace("'","''")+"',[ref]$t,[ref]$e)|Out-Null; @{errors=$e.Count}|ConvertTo-Json")
        self.assertEqual(r['errors'],0)

SOURCE = SCRIPT.parents[2] / 'azure-cli/r1.15-qualification/wp16_research_readiness.py'
SPEC = importlib.util.spec_from_file_location('wp16_readiness', SOURCE)
MODULE = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(MODULE)


class ResearchHealthWaitTests(unittest.TestCase):
    def run_wait(self, outcomes):
        elapsed = [0.0]
        limits = []
        def probe(timeout):
            limits.append(timeout)
            outcome, status, seconds = outcomes.pop(0)
            elapsed[0] += timeout if seconds is None else seconds
            return outcome, status
        def pause(seconds): elapsed[0] += seconds
        result = MODULE.wait_for_health(probe, lambda: elapsed[0], pause)
        return result, limits

    def test_cold_start_can_become_alive_after_initial_timeouts(self):
        result, limits = self.run_wait([('PENDING', None, None), ('PENDING', 503, None), ('ALIVE', 200, 11)])
        self.assertTrue(result['processAlive'])
        self.assertEqual(result['elapsedSeconds'], 135)
        self.assertEqual(limits, [60, 60, 56])
        self.assertEqual(result['datasetAcceptance'], 'NOT_RUN')
        self.assertFalse(result['PUBLIC_RUNTIME_VERIFIED'])

    def test_attempt_and_total_budget_are_bounded(self):
        result, limits = self.run_wait([('PENDING', None, None)] * 3)
        self.assertFalse(result['processAlive']);self.assertEqual(result['elapsedSeconds'], 180)
        self.assertEqual(len(limits), 3);self.assertEqual(limits, [60, 60, 56])

    def test_fatal_response_is_not_retried(self):
        result, limits = self.run_wait([('FAILED', 401, 1)])
        self.assertFalse(result['processAlive']);self.assertEqual(len(limits), 1)

    def test_late_alive_cannot_override_expired_budget(self):
        result, _ = self.run_wait([('ALIVE', 200, 181)])
        self.assertFalse(result['processAlive'])

    def response(self, status=200, body=b'Alive', encoding='identity'):
        connection = mock.MagicMock();response = connection.getresponse.return_value
        response.status = status;response.read.return_value = body;response.getheader.return_value = encoding
        return connection

    def test_exact_anonymous_https_health_request_and_tls_validation(self):
        connection = self.response()
        with mock.patch.object(MODULE.http.client, 'HTTPSConnection', return_value=connection) as factory:
            self.assertEqual(MODULE.probe_once(60), ('ALIVE', 200))
        self.assertEqual(factory.call_args.args, (MODULE.HOST,))
        self.assertTrue(factory.call_args.kwargs['context'].check_hostname)
        self.assertEqual(factory.call_args.kwargs['context'].verify_mode, MODULE.ssl.CERT_REQUIRED)
        self.assertEqual(connection.request.call_args.args, ('GET', '/health'))
        self.assertNotIn('Authorization', connection.request.call_args.kwargs['headers'])
        self.assertNotIn('X-API-Key', connection.request.call_args.kwargs['headers'])
        connection.getresponse.return_value.read.assert_called_once_with(6)
        connection.close.assert_called_once()

    def test_redirect_auth_and_wrong_route_status_fail_immediately(self):
        for status in (301, 302, 401, 404, 429):
            with self.subTest(status=status),mock.patch.object(MODULE.http.client, 'HTTPSConnection', return_value=self.response(status)):
                self.assertEqual(MODULE.probe_once(60), ('FAILED', status))

    def test_startup_gateway_statuses_can_remain_pending(self):
        for status in (502, 503, 504):
            with self.subTest(status=status),mock.patch.object(MODULE.http.client, 'HTTPSConnection', return_value=self.response(status)):
                self.assertEqual(MODULE.probe_once(60), ('PENDING', status))

    def test_html_partial_oversized_and_encoded_body_are_not_alive(self):
        for body, encoding in ((b'<html>', 'identity'), (b'Aliv', 'identity'), (b'Alive!', 'identity'), (b'Alive', 'gzip')):
            with self.subTest(body=body),mock.patch.object(MODULE.http.client, 'HTTPSConnection', return_value=self.response(body=body, encoding=encoding)):
                self.assertEqual(MODULE.probe_once(60), ('FAILED', 200))

    def test_tls_failure_is_not_pending_and_no_raw_error_escapes(self):
        connection = self.response();connection.connect.side_effect = MODULE.ssl.SSLError('PRIVATE_CANARY')
        with mock.patch.object(MODULE.http.client, 'HTTPSConnection', return_value=connection):
            result = MODULE.probe_once(60)
        self.assertEqual(result, ('FAILED', None));self.assertNotIn('PRIVATE_CANARY', str(result))
        connection.close.assert_called_once()

    def test_transport_timeout_is_pending_and_connection_closed(self):
        connection = self.response();connection.connect.side_effect = TimeoutError()
        with mock.patch.object(MODULE.http.client, 'HTTPSConnection', return_value=connection):
            self.assertEqual(MODULE.probe_once(60), ('PENDING', None))
        connection.close.assert_called_once()

    def test_expired_attempt_does_not_send_http_request(self):
        connection = self.response()
        with mock.patch.object(MODULE.http.client, 'HTTPSConnection', return_value=connection),mock.patch.object(MODULE.time, 'monotonic', side_effect=[0, 61]):
            self.assertEqual(MODULE.probe_once(60), ('PENDING', None))
        connection.request.assert_not_called()

    def test_python_default_never_probes(self):
        with mock.patch('sys.argv', ['readiness']),mock.patch.object(MODULE, 'wait_for_health') as wait,redirect_stdout(io.StringIO()) as output:
            self.assertEqual(MODULE.main(), 0)
        wait.assert_not_called();self.assertEqual(json.loads(output.getvalue())['result'], 'NOT_RUN')

if __name__ == '__main__':
    unittest.main()
