"""Fake-only WP39 command tests. Never obtains credentials or contacts a provider."""
import importlib.util
import json
import os
from pathlib import Path
import tempfile
import unittest
from unittest.mock import patch

MODULE = Path(__file__).resolve().parents[3] / 'eng/docker/release-1.15/wp39_local_history.py'
spec = importlib.util.spec_from_file_location('wp39', MODULE)
workflow = importlib.util.module_from_spec(spec); spec.loader.exec_module(workflow)


class WorkflowTests(unittest.TestCase):
    def test_plan_is_offline_and_never_claims_import(self):
        with patch.object(workflow, 'run', side_effect=AssertionError('no subprocess')):
            plan = workflow.plan()
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


if __name__ == '__main__': unittest.main()
