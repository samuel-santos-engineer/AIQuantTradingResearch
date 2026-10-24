#Requires -Version 5.1
[CmdletBinding()]
param([string] $OutputParent = $env:TEMP, [switch] $KeepImages)
$ErrorActionPreference = 'Stop'
$env:AIQ_WP02_REPO = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../../..'))
$env:AIQ_WP02_OUTPUT_PARENT = [System.IO.Path]::GetFullPath($OutputParent)
$env:AIQ_WP02_KEEP_IMAGES = [string]([bool]$KeepImages)
$pythonCode = @"
import json
import hashlib
import os
from pathlib import Path
import subprocess
import sys
import time
import uuid
root = Path(os.environ['AIQ_WP02_REPO']).resolve()
run_id = uuid.uuid4().hex
output = Path(os.environ['AIQ_WP02_OUTPUT_PARENT']).resolve() / ('aiq-wp02-pair-' + run_id)
output.mkdir(parents=True, exist_ok=False)
research = 'aiq-wp02-research:' + run_id
ui = 'aiq-wp02-ui:' + run_id
fixtures = root / 'tests/runtime/release_1_15/dual_docker'
record = {'fixtureOnly': True, 'authPolicy': 'single active key; no overlap', 'timeoutModel': 'standard socket timeouts; native DNS may exceed budget', 'PUBLIC_RUNTIME_VERIFIED': False, 'runId': run_id, 'commands': [], 'result': 'FAIL'}
images = []
started = time.monotonic()
print('Local evidence: ' + str(output), flush=True)
def run(args, timeout, name, cwd=root):
    record['commands'].append(args)
    try:
        result = subprocess.run(args, cwd=cwd, capture_output=True, text=True, encoding='utf-8', errors='replace', timeout=timeout)
    except subprocess.TimeoutExpired as error:
        captured = (error.stdout or b'') + (error.stderr or b'')
        (output / (name + '.log')).write_bytes(captured if isinstance(captured, bytes) else captured.encode('utf-8'))
        raise RuntimeError(name + ' bounded timeout') from None
    (output / (name + '.log')).write_bytes((result.stdout + result.stderr).encode('utf-8'))
    if result.returncode:
        raise RuntimeError(name + ' exit ' + str(result.returncode) + '; inspect owned local log')
    return result.stdout.strip()
try:
    record['inputSha256'] = {p.relative_to(root).as_posix(): hashlib.sha256(p.read_bytes()).hexdigest() for p in list(fixtures.glob('*')) + list((root / 'container/release_1_15').glob('*.Dockerfile')) + [root / 'eng/docker/release-1.15/Invoke-Wp02DualDockerQualification.ps1'] if p.is_file()}
    record['source'] = run(['git', 'rev-parse', 'HEAD'], 10, 'source')
    record['docker'] = run(['docker', 'info', '--format', '{{.ServerVersion}} {{.OSType}} {{.Architecture}}'], 15, 'docker')
    dependency = json.loads(run(['docker', 'image', 'inspect', 'aiq-c2b36:candidate'], 15, 'python-base'))[0]
    if dependency['Id'] != 'sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed':
        raise RuntimeError('Existing Python dependency image differs; no fallback')
    record['pythonDependencyImageId'] = dependency['Id']
    run(['docker', 'build', '--pull=false', '--network=none', '--platform=linux/amd64', '--label', 'aiq.wp02=' + run_id, '-f', 'container/release_1_15/research.Dockerfile', '-t', research, '.'], 600, 'research-build')
    images.append(research)
    run(['docker', 'build', '--pull=false', '--network=none', '--platform=linux/amd64', '--label', 'aiq.wp02=' + run_id, '--build-context', 'qualified_python=docker-image://aiq-c2b36:candidate', '-f', 'container/release_1_15/ui.Dockerfile', '-t', ui, '.'], 600, 'ui-build')
    images.append(ui)
    for name, tag in [('research', research), ('ui', ui)]:
        record[name + 'Image'] = json.loads(run(['docker', 'image', 'inspect', tag], 15, name + '-image'))[0]
    os.environ.update(AIQ_WP02_RUN_ID=run_id, AIQ_WP02_OUTPUT=str(output), AIQ_WP02_RESEARCH_IMAGE=research, AIQ_WP02_UI_IMAGE=ui)
    run([sys.executable, '-B', '-m', 'unittest', 'test_api_key_client_fixture', '-v'], 30, 'client-tests', fixtures)
    run([sys.executable, '-B', '-m', 'unittest', 'test_pair_boundary', '-v'], 180, 'pair-tests', fixtures)
    record['result'] = 'PASS'
except Exception as error:
    record['failure'] = type(error).__name__ + ': ' + str(error)
finally:
    # Backstop even if the unittest process times out before its own tearDownClass.
    try:
        owned = run(['docker', 'ps', '-aq', '--filter', 'label=aiq.wp02=' + run_id], 15, 'cleanup-owned-list').splitlines()
        for identifier in owned:
            info = json.loads(run(['docker', 'inspect', identifier], 10, 'cleanup-owned-inspect-' + identifier))[0]
            if info['Config']['Labels'].get('aiq.wp02') != run_id:
                raise RuntimeError('Container ownership mismatch')
            run(['docker', 'stop', '--time', '8', identifier], 15, 'cleanup-owned-stop-' + identifier)
            run(['docker', 'rm', identifier], 10, 'cleanup-owned-rm-' + identifier)
        networks = run(['docker', 'network', 'ls', '-q', '--filter', 'label=aiq.wp02=' + run_id], 10, 'cleanup-network-list').splitlines()
        for identifier in networks:
            info = json.loads(run(['docker', 'network', 'inspect', identifier], 10, 'cleanup-network-inspect'))[0]
            if info['Labels'].get('aiq.wp02') != run_id:
                raise RuntimeError('Network ownership mismatch')
            run(['docker', 'network', 'rm', identifier], 10, 'cleanup-network-rm')
    except Exception as error:
        record['result'] = 'FAIL'
        record.setdefault('cleanupFailures', []).append(type(error).__name__)
    certs = (output / 'certs').resolve()
    if certs.parent == output.resolve() and certs.name == 'certs':
        for name in ['server.pfx', 'trust.pem']:
            path = certs / name
            if path.is_file() and not path.is_symlink():
                path.unlink()
        if certs.exists() and not any(certs.iterdir()):
            certs.rmdir()
    if os.environ['AIQ_WP02_KEEP_IMAGES'].lower() != 'true':
        for tag in reversed(images):
            try:
                info = json.loads(run(['docker', 'image', 'inspect', tag], 15, 'cleanup-inspect-' + tag.split(':')[0]))[0]
                if info['Config']['Labels'].get('aiq.wp02') != run_id:
                    raise RuntimeError('Image ownership mismatch')
                run(['docker', 'image', 'rm', tag], 30, 'cleanup-image-' + tag.split(':')[0])
            except Exception as error:
                record['result'] = 'FAIL'
                record.setdefault('cleanupFailures', []).append(type(error).__name__)
    record['seconds'] = round(time.monotonic() - started, 3)
    (output / 'result.json').write_bytes((json.dumps(record, indent=2) + '\n').encode('utf-8'))
    print('Result=' + record['result'] + '; PUBLIC_RUNTIME_VERIFIED=False', flush=True)
sys.exit(0 if record['result'] == 'PASS' else 1)
"@
try {
    $pythonCode | & python -B -
    if ($LASTEXITCODE -ne 0) { throw 'WP02 qualification failed; inspect the owned local report.' }
}
finally {
    Remove-Item Env:AIQ_WP02_REPO -ErrorAction SilentlyContinue
    Remove-Item Env:AIQ_WP02_OUTPUT_PARENT -ErrorAction SilentlyContinue
    Remove-Item Env:AIQ_WP02_KEEP_IMAGES -ErrorAction SilentlyContinue
}
