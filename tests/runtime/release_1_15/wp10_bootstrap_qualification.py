"""Bounded local packaging/bootstrap tests; no Azure/provider calls or real input."""
import argparse
import datetime as dt
import io
import json
import os
from pathlib import Path
import shutil
import socket
import subprocess
import tarfile
import tempfile
import time
import urllib.error
import urllib.request
import uuid

ROOT=Path(__file__).resolve().parents[3]
BASE='mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f'
PYTHON='python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024'
DEPENDENCY='sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed'

def main():
    parser=argparse.ArgumentParser();parser.add_argument('--publish',type=Path,required=True);parser.add_argument('--execute',action='store_true')
    options=parser.parse_args();publish=options.publish
    owner='aiq-wp10-'+uuid.uuid4().hex;owned=[];client=None
    result={'PUBLIC_RUNTIME_VERIFIED':False,'azure':'NOT_RUN','providerRequests':0,'cases':{}}
    def docker(*args, data=None, timeout=45, ok=True):
        p=subprocess.run(['docker',*args],input=data,capture_output=True,timeout=timeout)
        if ok and p.returncode:raise RuntimeError('Owned Docker operation failed: '+args[0])
        return p
    assert json.loads(docker('context','inspect').stdout)[0]['Endpoints']['docker']['Host'].startswith('npipe://')
    for image in (BASE,PYTHON,DEPENDENCY):
        info=json.loads(docker('image','inspect',image).stdout)[0]
        assert info['Os']=='linux' and info['Architecture']=='amd64'
    assert (publish/'AIQuantTradingResearch.Worker.dll').is_file()
    assert (publish/'runtimes/linux-x64/native/libe_sqlite3.so').is_file()
    if not options.execute:
        print('PREFLIGHT_ONLY; no build/container/data operation; PUBLIC_RUNTIME_VERIFIED=False');return
    out=Path(tempfile.mkdtemp(prefix=owner+'-'));context=out/'context';context.mkdir()
    try:
        shutil.copytree(publish,context/'research')
        bootstrap=context/'container/release_1_15';bootstrap.mkdir(parents=True)
        shutil.copyfile(ROOT/'container/release_1_15/research-bootstrap.sh',bootstrap/'research-bootstrap.sh')
        presentation=context/'python/presentation';presentation.mkdir(parents=True)
        for name in ('realtime_financial_visualization.py','research_chart.py','research_snapshot_client.py','research_chart_component.js','research_chart_component.html','visualization_read_model.py','historical_market_bridge.py','local_synthetic_chart.py','immutable_synthetic_chart.py','public_preview_api.py'):
            shutil.copyfile(ROOT/'python/presentation'/name,presentation/name)
        shutil.copyfile(ROOT/'favicon.ico',context/'favicon.ico')
        docker('tag',DEPENDENCY,owner+':dependencies')
        for role,file in (('research','research-api.Dockerfile'),('ui','research-chart.Dockerfile')):
            tag=owner+':'+role
            docker('build','--platform','linux/amd64','--pull=false','--network=none','--label','aiq.owner='+owner,
                   '--build-context','qualified_python=docker-image://'+owner+':dependencies',
                   '-f',str(ROOT/'container/release_1_15'/file),'-t',tag,str(context),timeout=180)
            info=json.loads(docker('image','inspect',tag).stdout)[0]
            assert info['Config']['User']==('1654:1654' if role=='research' else '12002:12002')
            result[role+'ImageId']=info['Id']
            result[role+'RegistryDigest']='NOT_PUBLISHED'
        research=result['researchImageId']
        def simple(name,mode,expected):
            c=owner+'-'+name;owned.append(c)
            p=docker('run','--name',c,'--label','aiq.owner='+owner,'--network','none','--cap-drop','ALL',
                     '--security-opt','no-new-privileges','--memory','256m','--pids-limit','64',
                     '--env','AIQ_RESEARCH_BOOTSTRAP_MODE='+mode,'--env','AIQ_RESEARCH_PRIVATE_DIRECTORY=/tmp/aiq-research',
                     *(['--read-only','--env','AIQ_WP11_ATTEMPT_NONCE='+'b'*32,
                        '--env','AIQ_WP11_CANDIDATE_DIGEST=sha256:'+'a'*64] if mode=='diagnose' else []),research,ok=False)
            assert p.returncode==expected,name
            if mode=='diagnose':
                frame=json.loads(p.stdout)
                assert frame['entrypoint']=='research-bootstrap/diagnose' and frame['uid']==[1654]*4
                assert frame['nonce']=='b'*32 and frame['digest']=='sha256:'+'a'*64
                assert frame['probe']['accepted'] is False and frame['probe']['probeCleanup'] is True
                result['localDiagnostic']=p.stdout.decode().splitlines()
            result['cases'][name]='PASS_LOCAL'
        simple('diagnostic','diagnose',0)
        simple('unknown-mode','unknown',1)
        simple('missing-private-export','operator-stdin',1)
        simple('lost-state','serve',1)
        # ALL following observations are fabricated OFFLINE TEST FIXTURES, not real acquisition.
        key='offline-wp10-canary-service-key-12345678'
        config=json.dumps({'generation':str(uuid.uuid4()),'keyEpoch':str(uuid.uuid4()),'transformEpoch':1,
                           'hmacKeyBase64':'AAECAwQFBgcICQoLDA0ODxAREhMUFRYXGBkaGxwdHh8=','apiKey':key}).encode()
        rows=[{'openTimeUtc':(dt.datetime(2026,10,1,tzinfo=dt.timezone.utc)+dt.timedelta(minutes=i)).strftime('%Y-%m-%dT%H:%M:%SZ'),
               'open':'100','high':'101','low':'99','close':'100','volume':'1'} for i in range(401)]
        export=json.dumps({'source':'twelvedata','observedAtUtc':'2026-10-01T07:00:00Z','BTC/USD':rows,'ETH/USD':rows}).encode()
        env=out/'offline-test.env'
        env.write_text('\n'.join(k+'='+v for k,v in {
            'AIQ_RESEARCH_BOOTSTRAP_MODE':'operator-stdin','AIQ_RESEARCH_IMPORT_FILE':'/tmp/wp10-offline-input.json',
            'AIQ_RESEARCH_PRIVATE_DIRECTORY':'/tmp/aiq-research','AIQ_RESEARCH_STORAGE_SCOPE':'azure-f1-staging-ephemeral',
            'AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING':'1','AIQ_RESEARCH_EXPECTED_MOUNT_PATH':'/',
            'WEBSITES_ENABLE_APP_SERVICE_STORAGE':'false','WEBSITE_SITE_NAME':'aiqr115researchstagingwus27f5eabb5',
            'WEBSITE_HOSTNAME':'aiqr115researchstagingwus27f5eabb5.azurewebsites.net','WEBSITE_INSTANCE_ID':'SIMULATED_LOCAL_ONLY',
            'WEBSITE_ROLE_INSTANCE_ID':'0','AIQ_RESEARCH_TRANSPORT':'azure-app-service-http',
            'AIQ_RESEARCH_PLATFORM_HTTPS_ONLY':'1','AIQ_RESEARCH_API_KEY':key}.items()),encoding='utf-8',newline='\n')
        with socket.socket() as sock:sock.bind(('127.0.0.1',0));port=sock.getsockname()[1]
        c=owner+'-bootstrap';owned.append(c)
        docker('create','--interactive','--name',c,'--label','aiq.owner='+owner,'--env-file',str(env),
               '--cap-drop','ALL','--security-opt','no-new-privileges','--memory','256m','--pids-limit','64',
               '--publish','127.0.0.1:'+str(port)+':52731',research)
        archive=io.BytesIO()
        with tarfile.open(fileobj=archive,mode='w') as tar:
            info=tarfile.TarInfo('wp10-offline-input.json');info.size=len(export);info.uid=info.gid=1654;info.mode=0o600
            tar.addfile(info,io.BytesIO(export))
        docker('cp','-',c+':/tmp',data=archive.getvalue())
        bad=owner+'-invalid-input';owned.append(bad)
        docker('create','--interactive','--name',bad,'--label','aiq.owner='+owner,'--env-file',str(env),
               '--network','none','--cap-drop','ALL','--security-opt','no-new-privileges','--memory','256m','--pids-limit','64',research)
        docker('cp','-',bad+':/tmp',data=archive.getvalue())
        rejected=docker('start','--attach','--interactive',bad,data=b'{}',ok=False)
        assert rejected.returncode==1 and b'REIMPORT_REQUIRED' in rejected.stdout+rejected.stderr
        assert key.encode() not in rejected.stdout+rejected.stderr
        result['cases']['invalid_stdin_never_starts_endpoint']='PASS_LOCAL'
        client=subprocess.Popen(['docker','start','--attach','--interactive',c],stdin=subprocess.PIPE,stdout=subprocess.PIPE,stderr=subprocess.PIPE)
        client.stdin.write(config);client.stdin.close();client.stdin=None
        opener=urllib.request.build_opener(urllib.request.ProxyHandler({}))
        base='http://127.0.0.1:'+str(port)
        for attempt in range(40):
            try:
                with opener.open(base+'/health',timeout=1) as response:assert response.status==200
                break
            except (OSError,urllib.error.URLError):time.sleep(.25)
        else:raise RuntimeError('Same-container bootstrap did not reach endpoint')
        result['cases']['initializer_then_endpoint_same_container']='PASS_LOCAL'
        request={'schemaVersion':'1.0','instrument':'BTC/USD','requestEpoch':'1','knownIdentity':None}
        try:opener.open(urllib.request.Request(base+'/v1/research/snapshot',data=json.dumps(request).encode(),headers={'Content-Type':'application/json'}),timeout=3)
        except urllib.error.HTTPError as error:assert error.code==401
        else:raise RuntimeError('Missing API key was accepted')
        result['cases']['server_authentication']='PASS_LOCAL'
        for instrument in ('BTC/USD','ETH/USD'):
            request['instrument']=instrument
            with opener.open(urllib.request.Request(base+'/v1/research/snapshot',data=json.dumps(request).encode(),headers={'Content-Type':'application/json','X-API-Key':key}),timeout=4) as response:
                body=json.load(response)
            assert body['instrument']==instrument and body['type']=='full', 'Snapshot not full'
            result['cases'][instrument+'_offline_snapshot']='PASS_LOCAL'
        docker('stop','--time','5',c)
        stdout,stderr=client.communicate(timeout=8);client=None
        assert key.encode() not in stdout+stderr and b'hmacKeyBase64' not in stdout+stderr
        result['cases']['no_secret_diagnostics']='PASS_LOCAL'
        ui=owner+'-ui-health';owned.append(ui)
        docker('run','--detach','--name',ui,'--label','aiq.owner='+owner,'--network','none','--read-only',
               '--tmpfs','/tmp:rw,size=64m','--cap-drop','ALL','--security-opt','no-new-privileges',
               '--memory','512m','--pids-limit','64',result['uiImageId'])
        for attempt in range(40):
            probe=docker('exec',ui,'/opt/venv/bin/python','-B','-c',
                         'import urllib.request; print(urllib.request.urlopen("http://127.0.0.1:8501/_stcore/health",timeout=1).status)',ok=False)
            if probe.returncode==0 and probe.stdout.strip()==b'200':break
            time.sleep(.25)
        else:raise RuntimeError('UI image health unavailable')
        result['cases']['ui_image_health_without_credentials_network']='PASS_LOCAL'
        result['result']='PASS_LOCAL'
    except Exception:
        result['result']='FAIL_LOCAL'
        raise
    finally:
        for c in reversed(owned):
            p=docker('inspect','--format','{{ index .Config.Labels "aiq.owner" }}',c,ok=False)
            if p.returncode==0:
                assert p.stdout.decode().strip()==owner;docker('rm','--force',c)
        if client is not None:
            try:client.communicate(timeout=8)
            except subprocess.TimeoutExpired:client.kill();client.communicate(timeout=5)
        docker('image','rm',owner+':dependencies')
        # Only immutable local images retained. Private test input/context removed from exact owned root.
        assert out.resolve().parent==Path(tempfile.gettempdir()).resolve() and out.name.startswith(owner)
        shutil.rmtree(out)
        print(json.dumps(result))

if __name__=='__main__':main()
