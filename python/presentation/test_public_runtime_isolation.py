"""Offline Linux lifecycle tests. No provider, network, fixture files or dependencies.

The emulated cgroup below tests policy transitions, not kernel accounting. The real
delegated-cgroup test is a separate gate and skips explicitly when delegation is absent.
"""
import json
import os
from pathlib import Path
import signal
import subprocess
import sys
import tempfile
import threading
import time
import unittest
from unittest.mock import patch

import historical_market_bridge as bridge


class PolicyGroup:
    def __init__(self, root):
        self.path = Path(root) / 'request'
        self.cost = 0
    def create(self): self.path.mkdir()
    def populated(self): return False
    def usage(self): return self.cost
    def kill(self): pass
    def remove(self): self.path.rmdir()


class ProcessGroup(PolicyGroup):
    def __init__(self, root, process):
        super().__init__(root); self.process = process; self.create()
    def populated(self):
        for path in Path('/proc').glob('[0-9]*/stat'):
            try:
                fields = path.read_text().rsplit(')', 1)[1].split()
                if int(fields[2]) == self.process.pid and fields[0] != 'Z': return True
            except (OSError, IndexError, ValueError): pass
        return False
    def kill(self):
        try: os.killpg(self.process.pid, signal.SIGKILL)
        except ProcessLookupError: pass


class EnvironmentTests(unittest.TestCase):
    def test_environment_is_fixed_and_never_inherits_secrets_or_paths(self):
        with patch.dict(os.environ, {'TwelveData__ApiKey': 'secret', 'Vike__ApiKey': 'secret', 'HTTPS_PROXY': 'private',
                                     'DOTNET_STARTUP_HOOKS': '/private/hook.dll', 'HOME': '/private', 'PATH': '/private'}):
            env = bridge.public_child_environment()
        self.assertEqual('1', env['DOTNET_PROCESSOR_COUNT'])
        self.assertEqual('HistoricalQuery', env['Worker__Mode'])
        self.assertNotIn('secret', str(env)); self.assertNotIn('/private', str(env))
        self.assertEqual({'PATH', 'DOTNET_ROOT', 'HOME', 'TMPDIR', 'LANG', 'LC_ALL', 'TZ', 'DOTNET_CLI_TELEMETRY_OPTOUT',
                          'DOTNET_NOLOGO', 'DOTNET_PROCESSOR_COUNT', 'Worker__Mode', 'HistoricalMarketQuery__CacheDirectory'}, set(env))

    def test_missing_containment_never_spawns_worker(self):
        with patch.object(bridge, '_verify_public_filesystem', side_effect=bridge.BridgeError('BridgeUnavailable')), \
                patch.object(bridge.subprocess, 'Popen', side_effect=AssertionError('spawn forbidden')):
            with self.assertRaises(bridge.BridgeError): bridge.invoke(*bridge.DEFAULT_SELECTION)

    def test_runtime_overrides_are_not_an_invoke_api(self):
        with self.assertRaises(TypeError): bridge.invoke(*bridge.DEFAULT_SELECTION, environ={'Vike__ApiKey': 'secret'})


@unittest.skipUnless(sys.platform == 'linux', 'Linux OS-lock/process-group primitives required')
class LinuxRuntimeTests(unittest.TestCase):
    def test_actual_entrypoint_public_boot_without_secrets_never_launches_private_worker(self):
        script = Path(__file__).resolve().parents[2]/'container'/'entrypoint.sh'
        with tempfile.TemporaryDirectory() as root:
            for name, body in (('id', '#!/bin/sh\nprintf 1000\\n'), ('env', '#!/bin/sh\nprintf "%s\\n" "$@"')):
                path = Path(root)/name; path.write_text(body); path.chmod(0o700)
            process = subprocess.run(['/bin/sh', str(script)], env={'PATH': root+':/usr/bin:/bin', 'TwelveData__ApiKey': 'private-secret',
                                                                   'Vike__ApiKey': 'private-secret', 'HTTPS_PROXY': 'private-secret'},
                                     stdout=subprocess.PIPE, stderr=subprocess.PIPE, timeout=3)
            self.assertEqual(0, process.returncode, process.stderr)
            self.assertIn(b'-i', process.stdout); self.assertIn(b'streamlit', process.stdout)
            self.assertNotIn(b'private-secret', process.stdout); self.assertNotIn(b'Worker.dll', process.stdout)
            self.assertNotIn(b'TwelveData', process.stdout)
            private = subprocess.run(['/bin/sh', str(script)], env={'PATH': root+':/usr/bin:/bin', 'AIQ_RUNTIME_MODE': 'private'},
                                     stdout=subprocess.PIPE, stderr=subprocess.PIPE, timeout=3)
            self.assertEqual(64, private.returncode); self.assertIn(b'TwelveData__ApiKey', private.stderr)

    def test_real_separate_processes_share_one_admission_slot_and_wait_is_bounded(self):
        script = '''
import sys,time
sys.path.insert(0,sys.argv[1])
from test_public_runtime_isolation import PolicyGroup
from historical_market_bridge import _Admission,BridgeError
from pathlib import Path
root=Path(sys.argv[2]);group=PolicyGroup(root)
try:
 with _Admission(time.monotonic(),root=root,group=group):
  print('admitted',flush=True);time.sleep(1.4)
except BridgeError: print('busy',flush=True)
'''
        with tempfile.TemporaryDirectory() as root:
            args = [sys.executable, '-I', '-B', '-c', script, str(Path(__file__).parent), root]
            first = subprocess.Popen(args, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
            second = None
            try:
                self.assertEqual(b'admitted\n', first.stdout.readline())
                began = time.monotonic()
                second = subprocess.Popen(args, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
                out, err = second.communicate(timeout=3)
                self.assertEqual(b'busy\n', out); self.assertEqual(b'', err)
                self.assertLess(time.monotonic() - began, 1.3)
                first.wait(timeout=3)
            finally:
                for process in (first, second):
                    if process is not None:
                        if process.poll() is None: process.kill(); process.wait(timeout=2)
                        process.stdout.close(); process.stderr.close()

    def test_cpu_exhaustion_day_window_clock_rollback_and_corruption_fail_closed(self):
        for costs, expected in (([[1, 45000000]], 'Busy'), ([[1, 225000000]], 'Busy')):
            with tempfile.TemporaryDirectory() as root:
                group = PolicyGroup(root)
                bridge._atomic_ledger(Path(root)/'.public-runtime-ledger.json', {'last': 1, 'entries': costs, 'pending': None})
                with self.assertRaises(bridge.BridgeError):
                    with bridge._Admission(time.monotonic(), root=Path(root), group=group, wall=lambda: 2): pass
        with tempfile.TemporaryDirectory() as root:
            path = Path(root)/'.public-runtime-ledger.json'; group = PolicyGroup(root)
            for bad in ({'last': 100, 'entries': [], 'pending': None}, {'last': 1, 'entries': [[1, True]], 'pending': None}, {}):
                bridge._atomic_ledger(path, bad)
                with self.assertRaises(bridge.BridgeError):
                    with bridge._Admission(time.monotonic(), root=Path(root), group=group, wall=lambda: 2): pass

    def test_abandoned_reservation_is_not_expired_and_settlement_uses_cpu_evidence(self):
        with tempfile.TemporaryDirectory() as root:
            group = PolicyGroup(root); group.create()
            bridge._atomic_ledger(Path(root)/'.public-runtime-ledger.json',
                                 {'last': 1, 'entries': [], 'pending': {'token': 'a'*32, 'reserved': bridge.CPU_RESERVATION_US}})
            with bridge._Admission(time.monotonic(), root=Path(root), group=group, wall=lambda: 2) as admission:
                self.assertEqual([[2, bridge.CPU_RESERVATION_US]], admission.state['entries'])
                group.create(); group.cost = 12345; admission.settle()
                self.assertEqual([2, bridge.CPU_RESERVATION_US], admission.state['entries'][-1])
            state = json.loads((Path(root)/'.public-runtime-ledger.json').read_text())
            self.assertIsNone(state['pending'])

    def test_missing_ledger_after_initialization_cannot_reset_cpu_budget(self):
        with tempfile.TemporaryDirectory() as root:
            (Path(root)/'.public-runtime-initialized').touch()
            with self.assertRaises(bridge.BridgeError):
                with bridge._Admission(time.monotonic(), root=Path(root), group=PolicyGroup(root)): pass

    def test_cpu_history_retains_rolling_windows_and_prunes_only_old_completed_entries(self):
        with tempfile.TemporaryDirectory() as root:
            now = 100000 * 1000000000
            bridge._atomic_ledger(Path(root)/'.public-runtime-ledger.json', {'last': now-1, 'entries': [[now-86401*1000000000, 240000000]], 'pending': None})
            group = PolicyGroup(root)
            with bridge._Admission(time.monotonic(), root=Path(root), group=group, wall=lambda: now) as admission:
                self.assertEqual([], admission.state['entries']); group.create(); admission.settle()

    def test_symlink_lock_and_cgroup_refuse_unsafe_paths(self):
        with tempfile.TemporaryDirectory() as root:
            target = Path(root)/'target'; target.touch(); (Path(root)/'.public-runtime.lock').symlink_to(target)
            with self.assertRaises(OSError):
                with bridge._Admission(time.monotonic(), root=Path(root), group=PolicyGroup(root)): pass
            with self.assertRaises(bridge.BridgeError): bridge._Cgroup(Path(root)/'missing')

    def _child_capture(self, script, cancel=False, reads=None):
        with tempfile.TemporaryDirectory() as root:
            process = subprocess.Popen([sys.executable, '-I', '-B', '-c', script], stdin=subprocess.PIPE,
                                       stdout=subprocess.PIPE, stderr=subprocess.PIPE, start_new_session=True)
            group = ProcessGroup(root, process)
            event = threading.Event()
            if cancel: event.set()
            try:
                original = os.read
                def read(fd, size):
                    if reads is not None: reads.append(size)
                    return original(fd, size)
                with patch.object(bridge.os, 'read', side_effect=read):
                    return bridge._capture(process, b'{}', time.monotonic()+1, time.monotonic()+4, group, event)
            finally:
                bridge._terminate(process, group, time.monotonic()+3)
                self.assertIsNotNone(process.returncode); self.assertFalse(group.populated())
                for pipe in (process.stdin, process.stdout, process.stderr): pipe.close()

    def test_binary_stdout_boundary_chunk_size_and_utf8_rejection(self):
        reads = []
        raw = self._child_capture("import sys;sys.stdin.buffer.read();sys.stdout.buffer.write(b'x'*2097152)", reads=reads)
        self.assertEqual(2097152, len(raw)); self.assertTrue(all(size <= 8192 for size in reads))
        with self.assertRaises(bridge.BridgeError):
            self._child_capture("import sys;sys.stdin.buffer.read();sys.stdout.buffer.write(b'x'*2097153)")
        raw = self._child_capture("import sys;sys.stdin.buffer.read();sys.stdout.buffer.write(bytes([192,175]))")
        with self.assertRaises(bridge.BridgeError): bridge.parse_response(raw)

    def test_stderr_boundary_and_flood_drain_concurrently_without_public_exposure(self):
        raw = self._child_capture("import sys;sys.stdin.buffer.read();sys.stderr.buffer.write(b's'*16384);sys.stderr.flush();sys.stdout.buffer.write(b'{}')")
        self.assertEqual(b'{}', raw)
        with self.assertRaises(bridge.BridgeError):
            self._child_capture("import sys;sys.stdin.buffer.read();sys.stderr.buffer.write(b'private'*4000)")

    def test_timeout_cancellation_term_ignore_and_descendants_are_killed(self):
        with self.assertRaises(bridge.BridgeError): self._child_capture('import time;time.sleep(5)', cancel=True)
        script = "import signal,time,subprocess,sys;signal.signal(signal.SIGTERM,signal.SIG_IGN);subprocess.Popen([sys.executable,'-c','import signal,time;signal.signal(signal.SIGTERM,signal.SIG_IGN);time.sleep(20)']);time.sleep(20)"
        began = time.monotonic()
        with self.assertRaises(bridge.BridgeError): self._child_capture(script)
        self.assertLess(time.monotonic()-began, 4)

    def test_real_kernel_cgroup_accounting_is_a_distinct_required_gate(self):
        value = os.environ.get('AIQ_WP07_TEST_CGROUP')
        if not value:
            self.skipTest('BLOCKED: AIQ_WP07_TEST_CGROUP must name an isolated writable delegated test cgroup')
        root = Path(value)
        if root == bridge.PUBLIC_CGROUP: self.fail('Tests must not use the production cgroup')
        if not root.is_dir() or not os.access(root, os.W_OK):
            self.skipTest('BLOCKED: no writable delegated /sys/fs/cgroup/aiq-public; kernel CPU/tree proof pending')
        group = bridge._Cgroup(root)
        if group.path.exists(): self.fail('Active/stale production cgroup must not be touched by tests')
        group.create()
        try:
            self.assertEqual(0, group.usage()); self.assertFalse(group.populated())
            read_gate, write_gate = os.pipe()
            script = "import os,sys,signal,subprocess;os.read(int(sys.argv[1]),1);signal.signal(signal.SIGTERM,signal.SIG_IGN);subprocess.Popen([sys.executable,'-S','-c','while True: pass']);print('ready',flush=True);exec('while True: pass')"
            process = subprocess.Popen([sys.executable, '-I', '-S', '-B', '-c', script, str(read_gate)],
                                       pass_fds=(read_gate,), stdout=subprocess.PIPE, stderr=subprocess.PIPE, start_new_session=True)
            try:
                group.attach(process.pid); os.close(read_gate); read_gate = -1
                os.write(write_gate, b'G'); os.close(write_gate); write_gate = -1
                deadline = time.monotonic()+3
                while group.usage() < 10000 and time.monotonic() < deadline: time.sleep(0.01)
                self.assertGreaterEqual(group.usage(), 10000); self.assertTrue(group.populated())
                bridge._terminate(process, group, time.monotonic()+3)
                self.assertFalse(group.populated()); self.assertIsNotNone(process.returncode)
            finally:
                for fd in (read_gate, write_gate):
                    if fd >= 0: os.close(fd)
                if process.poll() is None: group.kill(); process.kill(); process.wait(timeout=2)
                process.stdout.close(); process.stderr.close()
        finally: group.remove()


if __name__ == '__main__': unittest.main()
