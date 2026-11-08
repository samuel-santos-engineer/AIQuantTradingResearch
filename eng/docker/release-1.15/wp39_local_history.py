"""WP39 owner workflow. Plan is offline. Acquisition requires separate explicit approval.

No Azure/registry publication. Provider key enters only from a private process channel,
then bounded C# stdin, never Docker/image/browser/arguments or a configuration file.
"""
import argparse
import io
import json
import os
from pathlib import Path
import shutil
import subprocess
import sys
import tarfile
import tempfile
import uuid
import hashlib
import re
import threading

ROOT = Path(__file__).resolve().parents[3]
ENGINE = Path(__file__).with_name('wp07_local_historical_chart.py')
DEPENDENCIES = 'sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed'


DIAGNOSTIC_LIMIT = 2048
CHILD_STAGES = frozenset(('preflight', 'private_input', 'request_start', 'http_response', 'response_parse',
                          'observation_validation', 'bundle_publication', 'unknown'))
CHILD_CODES = frozenset(('MISSING_APPROVAL', 'INVALID_DESTINATION', 'DESTINATION_EXISTS', 'INVALID_PRIVATE_INPUT',
    'VOLUME_UNCONFIRMED', 'TIMEOUT', 'CANCELLED', 'TRANSPORT_ERROR', 'HTTP_UNAUTHORIZED', 'HTTP_FORBIDDEN',
    'HTTP_RATE_LIMITED', 'HTTP_SERVER_ERROR', 'HTTP_OTHER', 'INVALID_RESPONSE_HEADERS', 'INVALID_PROVIDER_PAYLOAD',
    'INVALID_PROVIDER_METADATA', 'INVALID_OBSERVATION', 'INVALID_VOLUME', 'ZERO_ACCEPTED_CANDLES',
    'BUNDLE_PUBLICATION_FAILED', 'INVALID_JSON_ENCODING', 'INVALID_PAYLOAD_SHAPE', 'PROVIDER_ERROR_ENVELOPE', 'INVALID_PROVIDER_STATUS', 'PROVIDER_SYMBOL_MISMATCH', 'PROVIDER_INTERVAL_MISMATCH', 'PROVIDER_TIMEZONE_MISMATCH', 'INVALID_CANDLE_COLLECTION', 'PAYLOAD_SIZE_LIMIT', 'INVALID_CANDLE_TIMESTAMP', 'INVALID_OHLC_ENCODING', 'INVALID_METADATA_OBJECT', 'INVALID_METADATA_SYMBOL_FIELD', 'INVALID_METADATA_INTERVAL_FIELD', 'INVALID_METADATA_TIMEZONE_FIELD', 'UNKNOWN'))

PARENT_CODES = CHILD_CODES | frozenset(('MISSING_PRIVATE_CHANNEL', 'CHILD_EXECUTABLE_UNAVAILABLE',
    'CHILD_LAUNCH_FAILED', 'CHILD_TIMEOUT', 'CHILD_NONZERO_EXIT', 'CHILD_DIAGNOSTIC_INVALID', 'CHILD_OUTPUT_OVERSIZE'))


class AcquisitionFailure(RuntimeError):
    def __init__(self, diagnostic):
        super().__init__('WP39_ACQUIRE_FAILED')
        self.diagnostic = diagnostic


def parent_failure(stage, code, known_zero=False):
    if not isinstance(stage,str) or stage not in CHILD_STAGES | {'child_launch'}: stage = 'unknown'
    if not isinstance(code,str) or code not in PARENT_CODES: code = 'UNKNOWN'
    return AcquisitionFailure(dict(version=1, event='WP39_ACQUIRE_FAILED', failedStage=stage, instrument=None,
        httpStatus=None, requestsStarted=0 if known_zero else None, responsesReceived=0 if known_zero else None,
        outcomeUnknown=not known_zero, failureCode=code, bundlePublished=False if known_zero else None))


def child_failure(raw):
    # Do not forward a byte of child output. Only reconstruct validated fixed scalars.
    if not isinstance(raw, bytes):
        return parent_failure('unknown', 'CHILD_DIAGNOSTIC_INVALID')
    if len(raw) > DIAGNOSTIC_LIMIT:
        return parent_failure('unknown', 'CHILD_OUTPUT_OVERSIZE')
    def unique(pairs):
        result = {}
        for key, value in pairs:
            if key in result: raise ValueError()
            result[key] = value
        return result
    def reject_constant(value): raise ValueError()
    try:
        value = json.loads(raw.decode('utf-8'), object_pairs_hook=unique, parse_constant=reject_constant)
        if not isinstance(value, dict) or set(value) != {'version','event','failedStage','instrument','httpStatus',
                'requestsStarted','responsesReceived','outcomeUnknown','failureCode','bundlePublished'}: raise ValueError()
        if type(value['version']) is not int or value['version'] != 1 or value['event'] != 'WP39_ACQUIRE_FAILED': raise ValueError()
        if value['failedStage'] not in CHILD_STAGES or value['failureCode'] not in CHILD_CODES: raise ValueError()
        if value['instrument'] not in (None, 'BTC/USD', 'ETH/USD'): raise ValueError()
        started, received, status = value['requestsStarted'], value['responsesReceived'], value['httpStatus']
        if type(started) is not int or type(received) is not int or not 0 <= received <= started <= 2: raise ValueError()
        if status is not None and (type(status) is not int or not 100 <= status <= 599 or received == 0): raise ValueError()
        if type(value['outcomeUnknown']) is not bool or value['outcomeUnknown'] != (started > received): raise ValueError()
        if value['bundlePublished'] is not False or started and value['instrument'] is None: raise ValueError()
        http_codes = {401:'HTTP_UNAUTHORIZED',403:'HTTP_FORBIDDEN',429:'HTTP_RATE_LIMITED'}
        if value['failureCode'].startswith('HTTP_'):
            expected = http_codes.get(status, 'HTTP_SERVER_ERROR' if status is not None and 500 <= status <= 599 else 'HTTP_OTHER')
            if status is None or 200 <= status <= 299 or value['failedStage'] != 'http_response' or value['failureCode'] != expected: raise ValueError()
        if value['failedStage'] == 'http_response' and (status is None or received == 0): raise ValueError()
        return AcquisitionFailure(value)
    except (UnicodeError, ValueError, TypeError, RecursionError):
        return parent_failure('unknown', 'CHILD_DIAGNOSTIC_INVALID')


def run_acquisition(args, env, data, timeout):
    if not isinstance(data, bytes) or len(data) > 4096:
        raise parent_failure('private_input', 'INVALID_PRIVATE_INPUT', known_zero=True)
    try:
        child = subprocess.Popen(args, env=env, stdin=subprocess.PIPE, stdout=subprocess.DEVNULL, stderr=subprocess.PIPE)
    except FileNotFoundError:
        raise parent_failure('child_launch', 'CHILD_EXECUTABLE_UNAVAILABLE') from None
    except OSError:
        raise parent_failure('child_launch', 'CHILD_LAUNCH_FAILED') from None
    captured = []
    overflow = threading.Event()
    def read_diagnostic():
        try:
            raw = child.stderr.read(DIAGNOSTIC_LIMIT + 1)
            if len(raw) > DIAGNOSTIC_LIMIT:
                overflow.set()
                child.kill()  # Only this owned subprocess; outcome/counts remain unknown.
            else:
                captured.append(raw)
        except (OSError, ValueError):
            pass  # Missing trusted diagnostic becomes a fixed parent failure.
    reader = threading.Thread(target=read_diagnostic, daemon=True)
    reader.start()
    try:
        try:
            child.stdin.write(data)
            child.stdin.close()
        except BrokenPipeError:
            pass
        try:
            code = child.wait(timeout=timeout)
        except subprocess.TimeoutExpired:
            child.kill(); child.wait(timeout=5)
            raise parent_failure('unknown', 'CHILD_TIMEOUT') from None
        reader.join(timeout=5)
        if overflow.is_set(): raise parent_failure('unknown', 'CHILD_OUTPUT_OVERSIZE')
        if reader.is_alive(): raise parent_failure('unknown', 'CHILD_DIAGNOSTIC_INVALID')
        if code:
            if not captured or not captured[0]: raise parent_failure('unknown', 'CHILD_NONZERO_EXIT')
            raise child_failure(captured[0])
        return ''  # stdout is discarded, not retained or forwarded, even on success.
    except AcquisitionFailure:
        raise
    except (OSError, ValueError, subprocess.SubprocessError):
        raise parent_failure('unknown', 'UNKNOWN') from None
    finally:
        if child.poll() is None:
            child.kill(); child.wait(timeout=5)
        reader.join(timeout=5)
        # Do not block closing a buffered reader if an unexpected inherited pipe is still open.
        streams = (child.stdin,) if reader.is_alive() else (child.stdin, child.stderr)
        for stream in streams:
            try: stream.close()
            except OSError: pass


def run(args, env=None, data=None, timeout=180, acquisition=False):
    if acquisition:
        return run_acquisition(args, env, data, timeout)
    try:
        result = subprocess.run(args, env=env, input=data, capture_output=True, timeout=timeout)
    except FileNotFoundError:
        raise RuntimeError('Local executable unavailable') from None
    except subprocess.TimeoutExpired:
        raise RuntimeError('Local step timed out') from None
    except OSError:
        raise RuntimeError('Local step unavailable') from None
    if result.returncode:
        raise RuntimeError('Local step rejected: ' + args[0] + ' ' + args[1])
    return result.stdout.decode('utf-8').strip()


def private_directory(path):
    path.mkdir()
    if os.name == 'nt':
        sid = re.search(r'S-1-5-21-[0-9-]+', run(['whoami', '/user', '/fo', 'csv']))
        if not sid:
            raise RuntimeError('Owner identity unavailable')
        run(['icacls', str(path), '/inheritance:r', '/grant:r', '*' + sid.group() + ':(OI)(CI)F', '*S-1-5-18:(OI)(CI)F'])
    else:
        path.chmod(0o700)


def worker(directory, mode, env=None, data=None):
    settings = os.environ.copy() if env is None else env.copy()
    settings['AIQ_WP39_LOCAL_DIRECTORY'] = str(directory / 'runtime')
    settings['AIQ_WP39_BUNDLE_DIRECTORY'] = str(directory / 'bundle')
    settings['AIQ_RESEARCH_PRIVATE_DIRECTORY'] = str(directory / 'private')
    return run(['dotnet', str(directory / 'publish/AIQuantTradingResearch.Worker.dll'), mode], env=settings, data=data, timeout=40, acquisition=mode == '--research-acquire-local')


def plan():
    return dict(status='NEW_OWNER_PROVIDER_APPROVAL_REQUIRED', provider='Twelve Data',
                endpoint='https://api.twelvedata.com/time_series', symbols=['BTC/USD', 'ETH/USD'],
                parameters=dict(interval='1min', outputsize=1000, timezone='UTC', order='ASC'),
                window='latest up to 1000 completed minutes per symbol; reject rows older than 24h at execution',
                completionGraceSeconds=15, calls=2, retries=0, pagination=0,
                expectedCredits=2, accountCapabilities='unverified; errors/unknown volume capability fail closed',
                secretChannel='AIQ_WP39_PROVIDER_KEY process environment -> bounded C# stdin -> Authorization header',
                PUBLIC_RUNTIME_VERIFIED=False, AUTHENTIC_DATA_NOT_IMPORTED=True, liveAttemptReady=False)


def prepare(directory):
    if directory.exists() or not directory.is_relative_to(Path(tempfile.gettempdir()).resolve()):
        raise ValueError('Use a new local temporary directory')
    for image in ('aiq-c2b36:candidate', 'mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f', 'python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024'):
        run(['docker', 'image', 'inspect', image])
    if json.loads(run(['docker', 'image', 'inspect', 'aiq-c2b36:candidate']))[0]['Id'] != DEPENDENCIES:
        raise RuntimeError('Cached dependency mismatch')
    private_directory(directory)
    feed = directory / 'empty-feed'; feed.mkdir()
    project = ROOT / 'src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj'
    run(['dotnet', 'restore', str(project), '--source', str(feed), '-p:NuGetAudit=false'])
    run(['dotnet', 'publish', str(project), '-c', 'Release', '--no-restore', '--self-contained', 'false', '-p:UseAppHost=false', '-o', str(directory / 'publish')])
    worker(directory, '--research-prepare-local')
    owner = 'aiq-wp39-' + uuid.uuid4().hex[:12]
    context = directory / 'context'; public = context / 'python/presentation'; public.mkdir(parents=True)
    shutil.copytree(directory / 'publish', context / 'research')
    bootstrap = context / 'container/release_1_15'; bootstrap.mkdir(parents=True)
    shutil.copyfile(ROOT / 'container/release_1_15/research-bootstrap.sh', bootstrap / 'research-bootstrap.sh')
    for name in ('realtime_financial_visualization.py', 'research_chart.py', 'research_snapshot_client.py', 'research_chart_component.js', 'research_chart_component.html', 'visualization_read_model.py', 'historical_market_bridge.py', 'local_synthetic_chart.py', 'immutable_synthetic_chart.py', 'public_preview_api.py'):
        shutil.copyfile(ROOT / 'python/presentation' / name, public / name)
    shutil.copyfile(ROOT / 'favicon.ico', context / 'favicon.ico')
    images = {}
    for role, file in (('research', 'research-api.Dockerfile'), ('ui', 'research-chart.Dockerfile')):
        tag = owner + ':' + role
        run(['docker', 'build', '--pull=false', '--network=none', '--platform=linux/amd64', '--build-context', 'qualified_python=docker-image://aiq-c2b36:candidate', '-f', str(ROOT / 'container/release_1_15' / file), '-t', tag, str(context)])
        images[role] = json.loads(run(['docker', 'image', 'inspect', tag]))[0]['Id']
    manifest = dict(session=owner, researchImage=images['research'], uiImage=images['ui'], stateVolume=owner + '-state', tlsDirectory=str(directory / 'runtime/tls'))
    (directory / 'local.json').write_text(json.dumps(manifest, indent=2) + '\n', encoding='utf-8', newline='\n')
    print('PREPARED_NOT_IMPORTED; no provider calls or private state created')


def acquire(directory, approved, volume_confirmed):
    if not approved:
        raise parent_failure('preflight', 'MISSING_APPROVAL', known_zero=True)
    if not volume_confirmed:
        raise parent_failure('preflight', 'VOLUME_UNCONFIRMED', known_zero=True)
    # Do not access this channel at all in Plan/Prepare/offline tests.
    key = os.environ.get('AIQ_WP39_PROVIDER_KEY')
    if not key:
        raise parent_failure('preflight', 'MISSING_PRIVATE_CHANNEL', known_zero=True)
    env = os.environ.copy(); env.pop('AIQ_WP39_PROVIDER_KEY', None)
    env['AIQ_WP39_PROVIDER_APPROVED'] = '1'
    try:
        worker(directory, '--research-acquire-local', env, json.dumps(dict(providerKey=key, baseAssetVolumeConfirmed=True)).encode())
    finally:
        key = None; env.pop('AIQ_WP39_PROVIDER_APPROVED', None)
    print('ACQUIRED_NOT_IMPORTED; verify/import/snapshot consumption still required')


def import_bundle(directory):
    if worker(directory, '--research-verify-local') != 'ObservedProvider':
        raise RuntimeError('Observed-provider evidence required; fixtures cannot use this path')
    manifest = json.loads((directory / 'local.json').read_text())
    if run(['docker', 'ps', '--filter', 'label=aiq.chart.session=' + manifest['session'], '--format', '{{.ID}}']):
        raise RuntimeError('Stop the owned chart session before import')
    env = os.environ.copy(); env.pop('AIQ_WP39_PROVIDER_KEY', None)
    env['AIQ_RESEARCH_IMPORT_FILE'] = str(directory / 'bundle/observations.json')
    worker(directory, '--research-initialize', env, (directory / 'runtime/initialize.json').read_bytes())
    evidence = json.loads((directory / 'bundle/provenance.json').read_text())
    receipt = dict(status='ImportedAwaitingSnapshotVerification', kind='ObservedProvider', generation=json.loads((directory / 'runtime/initialize.json').read_text())['generation'],
                   importSha256=evidence['importSha256'], importedAtUtc=__import__('datetime').datetime.now(__import__('datetime').timezone.utc).isoformat())
    (directory / 'import-receipt.json').write_text(json.dumps(receipt, indent=2) + '\n', encoding='utf-8', newline='\n')
    print('IMPORTED_AWAITING_SNAPSHOT_VERIFICATION; public readiness unchanged')


def launch(directory, action, port):
    m = json.loads((directory / 'local.json').read_text())
    if action == 'Start':
        if worker(directory, '--research-verify-local') != 'ObservedProvider':
            raise RuntimeError('Verified provider bundle required')
        receipt = json.loads((directory / 'import-receipt.json').read_text())
        provenance = json.loads((directory / 'bundle/provenance.json').read_text())
        if receipt['kind'] != 'ObservedProvider' or receipt['importSha256'] != provenance['importSha256']:
            raise RuntimeError('Import receipt mismatch')
        existing = subprocess.run(['docker', 'volume', 'inspect', m['stateVolume']], capture_output=True, timeout=30)
        if existing.returncode:
            run(['docker', 'volume', 'create', '--label', 'aiq.wp39.owner=' + m['session'], '--label', 'aiq.wp39.import=' + receipt['importSha256'], m['stateVolume']])
            name = m['session'] + '-seed'
            try:
                run(['docker', 'create', '--name', name, '--network=none', '--pull=never', '--mount', 'type=volume,source=' + m['stateVolume'] + ',target=/private', m['researchImage']])
                stream = io.BytesIO()
                with tarfile.open(fileobj=stream, mode='w') as tar:
                    source = directory / 'private'
                    for path in [source, *source.iterdir()]:
                        if path.is_symlink(): raise RuntimeError('Private symlink rejected')
                        info = tar.gettarinfo(str(path), arcname='data' if path == source else 'data/' + path.name)
                        info.uid = info.gid = 1654; info.uname = info.gname = ''; info.mode = 0o700 if path.is_dir() else 0o600
                        if path.is_file():
                            with path.open('rb') as file: tar.addfile(info, file)
                        else: tar.addfile(info)
                run(['docker', 'cp', '-a', '-', name + ':/private'], data=stream.getvalue())
            finally:
                run(['docker', 'rm', '-f', name])
        elif (json.loads(existing.stdout)[0]['Labels'].get('aiq.wp39.owner') != m['session'] or json.loads(existing.stdout)[0]['Labels'].get('aiq.wp39.import') != receipt['importSha256']):
            raise RuntimeError('Unowned state volume')
    env = os.environ.copy(); env.pop('AIQ_WP39_PROVIDER_KEY', None)
    env.update(AIQ_CHART_ACTION=action, AIQ_CHART_SESSION=m['session'], AIQ_CHART_PORT=str(port), AIQ_CHART_EXECUTE='True', AIQ_CHART_FIXTURE='False',
               AIQ_CHART_RESEARCH=m['researchImage'], AIQ_CHART_UI=m['uiImage'], AIQ_CHART_VOLUME=m['stateVolume'], AIQ_CHART_TLS=m['tlsDirectory'])
    env['AIQ_RESEARCH_API_KEY'] = (directory / 'runtime/service.key').read_text()
    env['AIQ_CHART_PROVENANCE'] = 'ObservedProvider'
    subprocess.run([sys.executable, '-B', str(ENGINE)], env=env, timeout=180, check=True)


def verify_snapshot(directory):
    if worker(directory, '--research-verify-local') != 'ObservedProvider':
        raise RuntimeError('Observed provider provenance required')
    m=json.loads((directory/'local.json').read_text())
    evidence=json.loads((directory/'bundle/provenance.json').read_text())
    receipt=json.loads((directory/'import-receipt.json').read_text())
    if receipt['kind']!='ObservedProvider' or receipt['importSha256']!=evidence['importSha256']:
        raise RuntimeError('Import receipt mismatch')
    probe = """import os,sys,json
sys.path.insert(0,'/app/python/presentation')
from research_snapshot_client import ResearchSnapshotClient
c=ResearchSnapshotClient('https://wp07-research:52731',os.environ['AIQ_RESEARCH_API_KEY'],ca_file='/ca.pem')
results={}
for instrument in ('BTC/USD','ETH/USD'):
 s=c.fetch(instrument);r=s['candles'];assert r
 results[instrument]=dict(rows=len(r),firstUtc=r[0]['time'],lastUtc=r[-1]['time'],generation=s['identity']['generation'])
print(json.dumps(results))
"""
    actual=json.loads(run(['docker','exec',m['session']+'-ui','/opt/venv/bin/python','-B','-c',probe],timeout=30))
    for symbol in ('BTC/USD','ETH/USD'):
        summary=evidence['symbols'][symbol]
        if actual[symbol]['generation']!=receipt['generation'] or any(actual[symbol][key]!=summary[key] for key in ('rows','firstUtc','lastUtc')):
            raise RuntimeError('Imported snapshot does not match acquisition bounds/generation')
    result=dict(status='LOCAL_AUTHENTIC_SNAPSHOT_VERIFIED',AUTHENTIC_DATA_NOT_IMPORTED=False,PUBLIC_RUNTIME_VERIFIED=False,liveAttemptReady=False)
    (directory/'qualification.json').write_text(json.dumps(result,indent=2)+'\n',encoding='utf-8',newline='\n')
    print(json.dumps(result))


if __name__ == '__main__':
    p = argparse.ArgumentParser(description=__doc__)
    p.add_argument('--action', choices=('Plan','Prepare','Acquire','Import','Start','Stop','Verify'), default='Plan')
    p.add_argument('--directory', type=Path)
    p.add_argument('--port', type=int, default=8501)
    p.add_argument('--execute-provider-approved', action='store_true')
    p.add_argument('--base-asset-volume-confirmed', action='store_true')
    args = p.parse_args()
    try:
        if args.action == 'Plan': print(json.dumps(plan(), indent=2))
        else:
            if args.directory is None or not 1024 <= args.port <= 65535: raise ValueError('Local directory/port required')
            directory = args.directory.resolve()
            if args.action == 'Prepare': prepare(directory)
            elif args.action == 'Acquire': acquire(directory,args.execute_provider_approved,args.base_asset_volume_confirmed)
            elif args.action == 'Import': import_bundle(directory)
            elif args.action == 'Verify': verify_snapshot(directory)
            else: launch(directory,args.action,args.port)
    except AcquisitionFailure as failure:
        print(json.dumps(failure.diagnostic, separators=(',',':')), file=sys.stderr)
        raise SystemExit(1)
    except Exception:
        if args.action == 'Acquire':
            print(json.dumps(parent_failure('unknown', 'UNKNOWN').diagnostic, separators=(',',':')), file=sys.stderr)
        else:
            print('WP39_BLOCKED; preserve local state for review; no automatic retry', file=sys.stderr)
        raise SystemExit(1)
