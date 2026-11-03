"""Bounded cached-image investigation. No Azure/registry calls or real keys.

Run with the existing local Python interpreter providing cryptography; no installs.
Only localhost HTTPS is contacted. Creates and removes only named owned containers.
"""
import datetime
import ipaddress
import json
import os
from pathlib import Path
import ssl
import subprocess
import tempfile
import time
import urllib.request
import uuid

IMAGE = 'ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f'


def docker(*args, check=True):
    result = subprocess.run(['docker', *args], capture_output=True, text=True, timeout=30)
    if check and result.returncode:
        raise RuntimeError('Docker operation failed: ' + args[0])
    return result


def inspect(name):
    return json.loads(docker('inspect', name).stdout)[0]


def certificate(directory):
    from cryptography import x509
    from cryptography.hazmat.primitives import hashes, serialization
    from cryptography.hazmat.primitives.asymmetric import rsa
    from cryptography.hazmat.primitives.serialization import pkcs12
    from cryptography.x509.oid import NameOID
    key = rsa.generate_private_key(public_exponent=65537, key_size=2048)
    subject = x509.Name([x509.NameAttribute(NameOID.COMMON_NAME, 'WP37 disposable localhost')])
    now = datetime.datetime.now(datetime.timezone.utc)
    cert = (x509.CertificateBuilder().subject_name(subject).issuer_name(subject)
            .public_key(key.public_key()).serial_number(x509.random_serial_number())
            .not_valid_before(now - datetime.timedelta(minutes=1))
            .not_valid_after(now + datetime.timedelta(hours=1))
            .add_extension(x509.SubjectAlternativeName([x509.IPAddress(ipaddress.ip_address('127.0.0.1'))]), False)
            .sign(key, hashes.SHA256()))
    (directory / 'test.pfx').write_bytes(pkcs12.serialize_key_and_certificates(b'local', key, cert, None, serialization.NoEncryption()))
    (directory / 'test.pem').write_bytes(cert.public_bytes(serialization.Encoding.PEM))


def run_case(directory, label, memory, cpu, negative=None):
    name = 'aiq-wp37-local-' + uuid.uuid4().hex[:12]
    env = os.environ.copy()
    env['AIQ_RESEARCH_API_KEY'] = '0' * 64  # Fake only; omitted from all evidence.
    settings = {
        'AIQ_RESEARCH_BOOTSTRAP_MODE': 'serve',
        'AIQ_RESEARCH_PRIVATE_DIRECTORY': '/tmp/aiq-research',
        'AIQ_RESEARCH_STORAGE_SCOPE': 'local-qualification',
        'AIQ_RESEARCH_EXPECTED_MOUNT_PATH': '/',
        'AIQ_RESEARCH_TRANSPORT': 'local-https',
        'AIQ_RESEARCH_BIND_ADDRESS': '0.0.0.0',
        'AIQ_RESEARCH_TLS_PFX': '/test/test.pfx',
    }
    if negative == 'mount':
        settings['AIQ_RESEARCH_EXPECTED_MOUNT_PATH'] = '/not-the-root-mount'
    if negative == 'certificate':
        settings['AIQ_RESEARCH_TLS_PFX'] = '/test/absent.pfx'
    if negative == 'partial-staging':
        settings['AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING'] = '1'
    if negative == 'mode':
        settings['AIQ_RESEARCH_BOOTSTRAP_MODE'] = 'invalid'
    args = ['docker', 'run', '-d', '--pull=never', '--name', name,
            '--memory', memory, '--cpus', cpu, '--restart=no',
            '-p', '127.0.0.1::52731', '--mount', 'type=bind,source=' + str(directory) + ',target=/test,readonly',
            '-e', 'AIQ_RESEARCH_API_KEY']
    for key, value in settings.items():
        args += ['-e', key + '=' + value]
    args.append(IMAGE)
    started = time.monotonic()
    try:
        result = subprocess.run(args, env=env, capture_output=True, text=True, timeout=30)
        if result.returncode:
            raise RuntimeError('Container creation failed')
        state = inspect(name)
        ports = state['NetworkSettings']['Ports'].get('52731/tcp')
        port = ports[0] if ports else None
        if port:
            assert port['HostIp'] == '127.0.0.1'
        elif not negative:
            raise AssertionError('No loopback publication')
        context = ssl.create_default_context(cafile=str(directory / 'test.pem'))
        opener = urllib.request.build_opener(urllib.request.ProxyHandler({}), urllib.request.HTTPSHandler(context=context))
        ready = None
        while time.monotonic() - started < 25:
            state = inspect(name)
            if not state['State']['Running']:
                break
            try:
                with opener.open('https://127.0.0.1:' + port['HostPort'] + '/health', timeout=1) as response:
                    assert response.status == 200 and response.read(6) == b'Alive'
                ready = round(time.monotonic() - started, 3)
                break
            except (OSError, urllib.error.URLError):
                time.sleep(0.25)
        evidence = {'case': label, 'memoryLimit': memory, 'cpuLimit': cpu, 'readySeconds': ready}
        if negative:
            state = inspect(name)
            assert ready is None and not state['State']['Running'] and state['State']['ExitCode'] == 1
            evidence['fixedFailureMessage'] = docker('logs', name).stderr.strip()
            assert evidence['fixedFailureMessage'] in ('Research bootstrap unavailable', 'Research snapshot service unavailable.')
        else:
            assert ready is not None, 'Readiness not reached'
            evidence['identity'] = docker('exec', name, 'id', '-u').stdout.strip() + ':' + docker('exec', name, 'id', '-g').stdout.strip()
            assert evidence['identity'] == '1654:1654'
            command = docker('exec', name, 'cat', '/proc/1/cmdline').stdout.replace('\x00', ' ').strip()
            assert command == 'dotnet /app/AIQuantTradingResearch.Worker.dll --research-snapshot-api'
            evidence['pid1Command'] = command
            docker('exec', name, '/bin/sh', '-c', 'mkdir -m 700 /tmp/aiq-research && touch /tmp/aiq-research/probe && rm /tmp/aiq-research/probe')
            evidence['privateDirectoryWritable'] = True
            evidence['resources'] = json.loads(docker('stats', name, '--no-stream', '--format', '{{json .}}').stdout)
            # Two later health samples independently establish continued serving/process lifetime.
            for _ in range(2):
                time.sleep(2)
                assert inspect(name)['State']['Running']
                with opener.open('https://127.0.0.1:' + port['HostPort'] + '/health', timeout=2) as response:
                    assert response.status == 200 and response.read(6) == b'Alive'
            stop = time.monotonic()
            docker('stop', '--time', '10', name)
            evidence['shutdownSeconds'] = round(time.monotonic() - stop, 3)
            state = inspect(name)
            assert state['State']['ExitCode'] == 0
        evidence.update(exitCode=state['State']['ExitCode'], oomKilled=state['State']['OOMKilled'], restarts=state['RestartCount'])
        assert not evidence['oomKilled'] and evidence['restarts'] == 0
        return evidence
    finally:
        # Exact random name belongs to this invocation; never enumerate/delete other containers.
        docker('rm', '-f', name, check=False)


def main():
    metadata = json.loads(docker('image', 'inspect', IMAGE).stdout)[0]
    assert metadata['Architecture'] == 'amd64' and metadata['Config']['User'] == '1654:1654'
    assert metadata['Config']['Entrypoint'] == ['/bin/sh', '/app/research-bootstrap.sh']
    report = {'image': IMAGE, 'azureRequests': 0, 'registryRequests': 0, 'cases': [], 'rootCause': 'UNRESOLVED', 'PUBLIC_RUNTIME_VERIFIED': False}
    with tempfile.TemporaryDirectory(prefix='aiq-wp37-local-') as temp:
        directory = Path(temp)
        certificate(directory)
        for index in range(3):
            report['cases'].append(run_case(directory, 'baseline-' + str(index + 1), '512m', '1'))
            print(json.dumps(report['cases'][-1]), flush=True)
        for index in range(2):
            report['cases'].append(run_case(directory, 'constrained-' + str(index + 1), '256m', '0.25'))
            print(json.dumps(report['cases'][-1]), flush=True)
        for negative in ('mount', 'certificate', 'partial-staging', 'mode'):
            report['cases'].append(run_case(directory, negative, '256m', '0.25', negative))
            print(json.dumps(report['cases'][-1]), flush=True)
    print(json.dumps(report, indent=2))


if __name__ == '__main__':
    main()
