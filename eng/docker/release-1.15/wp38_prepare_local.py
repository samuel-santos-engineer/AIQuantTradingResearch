"""Prepare disposable WP38 fixtures/images for the existing WP07 local launcher.

No Azure, provider or registry operations; uses only cached images/NuGet packages.
The generated key is test-only. Never pass an existing private dataset directory.
"""
import argparse
import io
import json
import os
from pathlib import Path
import shutil
import subprocess
import tarfile
import tempfile
import uuid

RESEARCH = 'sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f'
DEPENDENCIES = 'sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed'
BASE = 'python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024'
ROOT = Path(__file__).resolve().parents[3]


def run(args, timeout=180, data=None, env=None):
    result = subprocess.run(args, input=data, capture_output=True, timeout=timeout, env=env)
    if result.returncode:
        # Commands never carry credentials; do not expose raw build/application logs.
        raise RuntimeError('Local step failed: ' + args[0] + ' ' + args[1])
    return result.stdout.decode('utf-8').strip()


def prepare(output):
    output = output.resolve()
    if not output.is_relative_to(Path(tempfile.gettempdir()).resolve()) or output.exists():
        raise ValueError('Use a new directory under the local temporary directory')
    for image in (RESEARCH, DEPENDENCIES, BASE):
        run(['docker', 'image', 'inspect', image], timeout=30)
    assert json.loads(run(['docker', 'image', 'inspect', 'aiq-c2b36:candidate']))[0]['Id'] == DEPENDENCIES
    output.mkdir()
    owner = 'aiq-wp38-' + uuid.uuid4().hex[:12]
    volume, container, tag = owner + '-state', owner + '-seed', owner + ':ui'
    created_volume = created_container = built = False
    try:
        feed = output / 'empty-feed'; feed.mkdir()
        project = ROOT / 'tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj'
        run(['dotnet', 'restore', str(project), '--source', str(feed), '-p:NuGetAudit=false'])
        env = os.environ.copy()
        env['AIQ_WP07_EXPORT'] = str(output / 'fixture')
        run(['dotnet', 'test', str(project), '--no-restore', '-c', 'Release', '--filter',
             'FullyQualifiedName~ExportOwnedLinuxQualificationFixture'], env=env)
        fixture = output / 'fixture'
        assert json.loads((fixture / 'fixture.json').read_text())['testOnly'] is True
        context = output / 'context'; public = context / 'python/presentation'; public.mkdir(parents=True)
        for name in ('realtime_financial_visualization.py', 'research_chart.py', 'research_snapshot_client.py',
                     'research_chart_component.js', 'research_chart_component.html', 'visualization_read_model.py',
                     'historical_market_bridge.py', 'local_synthetic_chart.py', 'immutable_synthetic_chart.py', 'public_preview_api.py'):
            shutil.copyfile(ROOT / 'python/presentation' / name, public / name)
        shutil.copyfile(ROOT / 'favicon.ico', context / 'favicon.ico')
        run(['docker', 'build', '--pull=false', '--network=none', '--platform=linux/amd64',
             '--build-context', 'qualified_python=docker-image://aiq-c2b36:candidate',
             '-f', str(ROOT / 'container/release_1_15/research-chart.Dockerfile'), '-t', tag, str(context)])
        built = True
        ui = json.loads(run(['docker', 'image', 'inspect', tag]))[0]['Id']
        run(['docker', 'volume', 'create', '--label', 'aiq.wp38.owner=' + owner, volume]); created_volume = True
        run(['docker', 'create', '--name', container, '--network=none', '--pull=never',
             '--mount', 'type=volume,source=' + volume + ',target=/private', RESEARCH]); created_container = True
        archive = io.BytesIO()
        with tarfile.open(fileobj=archive, mode='w') as tar:
            source = fixture / 'private'
            for path in [source, *source.iterdir()]:
                assert not path.is_symlink()
                info = tar.gettarinfo(str(path), arcname='data' if path == source else 'data/' + path.name)
                info.uid = info.gid = 1654; info.uname = info.gname = ''
                info.mode = 0o700 if path.is_dir() else 0o600
                if path.is_file():
                    with path.open('rb') as stream:
                        tar.addfile(info, stream)
                else:
                    tar.addfile(info)
        run(['docker', 'cp', '-a', '-', container + ':/private'], data=archive.getvalue())
        run(['docker', 'rm', container]); created_container = False
        manifest = dict(testOnly=True, researchImage=RESEARCH, uiImage=ui, uiTag=tag, stateVolume=volume,
                        tlsDirectory=str(fixture / 'tls'), keyFile=str(fixture / 'service.key'), session=owner,
                        AUTHENTIC_DATA_NOT_IMPORTED=True, PUBLIC_RUNTIME_VERIFIED=False)
        (output / 'local.json').write_text(json.dumps(manifest, indent=2) + '\n', encoding='utf-8', newline='\n')
        print('WP38 fixture preparation complete. Use the existing local launcher through Invoke-Wp38LocalChart.ps1.')
    except Exception:
        if created_container:
            run(['docker', 'rm', '-f', container])
        if created_volume:
            run(['docker', 'volume', 'rm', volume])
        if built:
            run(['docker', 'image', 'rm', tag])
        raise


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--output', type=Path, required=True)
    parser.add_argument('--action', choices=('Prepare', 'Start', 'Stop'), default='Prepare')
    parser.add_argument('--port', type=int, default=8501)
    args = parser.parse_args()
    if args.action == 'Prepare':
        prepare(args.output)
    else:
        manifest = json.loads((args.output / 'local.json').read_text())
        if manifest.get('testOnly') is not True or not 1024 <= args.port <= 65535:
            raise ValueError('Disposable fixture manifest and valid port required')
        env = os.environ.copy()
        env.update(AIQ_CHART_ACTION=args.action, AIQ_CHART_SESSION=manifest['session'],
                   AIQ_CHART_PORT=str(args.port), AIQ_CHART_EXECUTE='True', AIQ_CHART_FIXTURE='True',
                   AIQ_CHART_RESEARCH=manifest['researchImage'], AIQ_CHART_UI=manifest['uiImage'],
                   AIQ_CHART_VOLUME=manifest['stateVolume'], AIQ_CHART_TLS=manifest['tlsDirectory'])
        # Only the fake key generated by the explicit fixture exporter is loaded.
        env['AIQ_RESEARCH_API_KEY'] = (args.output / 'fixture/service.key').read_text().strip()
        import sys
        try:
            subprocess.run([sys.executable, '-B', str(Path(__file__).with_name('wp07_local_historical_chart.py'))],
                           env=env, check=True, timeout=180)
        finally:
            env.pop('AIQ_RESEARCH_API_KEY', None)
