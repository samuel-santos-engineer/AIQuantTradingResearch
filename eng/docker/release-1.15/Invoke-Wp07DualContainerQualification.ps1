#requires -Version 5.1
[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)][string]$PublishedResearchDirectory,
    [Parameter(Mandatory=$true)][string]$FixtureDirectory,
    [string]$Python='python',
    [switch]$Execute,
    [switch]$KeepImages
)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
# Windows PowerShell 5.1; all orchestration uses existing Python standard library.
$names=@('AIQ_WP07_REPO','AIQ_WP07_PUBLISH','AIQ_WP07_FIXTURE','AIQ_WP07_EXECUTE','AIQ_WP07_KEEP')
$saved=@{}
foreach ($name in $names) { $saved[$name]=[Environment]::GetEnvironmentVariable($name,'Process') }
try {
    $env:AIQ_WP07_REPO=[IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../../..'))
    $env:AIQ_WP07_PUBLISH=[IO.Path]::GetFullPath($PublishedResearchDirectory)
    $env:AIQ_WP07_FIXTURE=[IO.Path]::GetFullPath($FixtureDirectory)
    $env:AIQ_WP07_EXECUTE=[string][bool]$Execute
    $env:AIQ_WP07_KEEP=[string][bool]$KeepImages
    $code=@'
import os,sys,json,time,uuid,subprocess,hashlib,shutil,tarfile,io,tempfile
from pathlib import Path
root=Path(os.environ['AIQ_WP07_REPO']).resolve()
published=Path(os.environ['AIQ_WP07_PUBLISH']).resolve()
fixture=Path(os.environ['AIQ_WP07_FIXTURE']).resolve()
execute=os.environ['AIQ_WP07_EXECUTE']=='True'
keep=os.environ['AIQ_WP07_KEEP']=='True'
owner='aiq-wp07-'+uuid.uuid4().hex
out=Path(tempfile.gettempdir())/owner;out.mkdir()
record={'PUBLIC_RUNTIME_VERIFIED':False,'owner':owner,'result':'BLOCKED','cases':{},'commands':[]}
owned=[];env=dict(os.environ);browser=None
bases=['mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f','python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024']
dependency='sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed'
def run(args,timeout=30,data=None):
    record['commands'].append(args)
    r=subprocess.run(args,input=data,capture_output=True,timeout=timeout,env=env)
    (out/(str(len(record['commands']))+'.log')).write_bytes(r.stdout+r.stderr)
    if r.returncode:raise RuntimeError(args[0]+' '+args[1]+' failed; see owned local log '+str(len(record['commands'])))
    return r.stdout.decode('utf-8',errors='replace').strip()
def docker(*args,timeout=30,data=None):return run(['docker',*args],timeout,data)
def archive(folder,uid,prefix=None):
    b=io.BytesIO()
    with tarfile.open(fileobj=b,mode='w') as t:
        for p in [folder,*folder.iterdir()]:
            assert not p.is_symlink()
            info=t.gettarinfo(str(p),arcname=(prefix or '.') if p==folder else ((prefix+'/') if prefix else '')+p.name)
            info.uid=info.gid=uid;info.uname=info.gname='';info.mode=0o700 if p.is_dir() else 0o600
            if p.is_file():
                with p.open('rb') as f:t.addfile(info,f)
            else:t.addfile(info)
    return b.getvalue()
research=owner+'-research';ui=owner+'-ui'
probe=r"""import os,json,urllib.request,urllib.error,ssl,time,sys
sys.path.insert(0,"/app/python/presentation")
from research_snapshot_client import ResearchSnapshotClient,SnapshotUnavailable
base='https://wp07-research:52731';key=os.environ['AIQ_RESEARCH_API_KEY'];ca='/ca.pem'
client=ResearchSnapshotClient(base,key,ca_file=ca)
results={};latency={}
assert os.getuid()==12002 and not any("TWELVE" in k.upper() or "HMAC" in k.upper() for k in os.environ)
for instrument in ('BTC/USD','ETH/USD'):
 began=time.perf_counter();s=client.fetch(instrument);full=time.perf_counter()-began;assert len(s['candles'])==1000 and s['status']['classificationValidity']=='Valid'
 assert s['marketDataGapIntervals'] and s['analytics'][-1]['sma20'] is not None
 assert s['candles'][0]['open']!='100.000000000000'
 identity=dict(s['identity']);began=time.perf_counter();n=client.fetch(instrument);latency[instrument]=dict(fullSeconds=full,unchangedSeconds=time.perf_counter()-began);assert n['type']=='unchanged' and dict(n['identity'])==identity
 results[instrument]=dict(identity=identity,rows=len(s['candles']),lastClose=s['candles'][-1]['close'])
for invalid in (None,'deliberately-wrong-test-key'):
 request=urllib.request.Request(base+'/v1/research/snapshot',data=b'{"schemaVersion":"1.0","instrument":"BTC/USD","requestEpoch":"1","knownIdentity":null}',headers={'Content-Type':'application/json',**({'X-API-Key':invalid} if invalid else {})})
 try:urllib.request.urlopen(request,context=ssl.create_default_context(cafile=ca),timeout=5);raise AssertionError('unauthorized accepted')
 except urllib.error.HTTPError as e:assert e.code==401
for bad in ('https://wp07-wrong-host:52731','https://wp07-research:52731'):
 try:
  broken=ResearchSnapshotClient(bad,key,ca_file=ca if 'wrong-host' in bad else None);broken.fetch('BTC/USD');raise AssertionError('untrusted TLS accepted')
 except SnapshotUnavailable:pass
print(json.dumps(dict(result='PASS',instruments=results,latency=latency,uid=os.getuid(),negative='missing/wrong key; wrong hostname; untrusted CA')))
"""
def probe_api():
    value=docker('exec',ui,'/opt/venv/bin/python','-B','-c',probe,timeout=30)
    return json.loads(value)
def ready():
    for _ in range(30):
        try:return probe_api()
        except RuntimeError:time.sleep(.5)
    raise RuntimeError('Actual research HTTPS readiness not achieved')
try:
    endpoint=docker('context','inspect','--format','{{.Endpoints.docker.Host}}')
    assert endpoint.startswith('npipe://'),'Local Windows Docker context required'
    record['docker']=docker('info','--format','{{.ServerVersion}} {{.OSType}} {{.Architecture}}')
    for image in bases+[dependency]:
        meta=json.loads(docker('image','inspect',image))[0]
        assert meta['Os']=='linux' and meta['Architecture']=='amd64'
        record.setdefault('baseImages',[]).append(dict(id=meta['Id'],reference=image))
    assert (published/'AIQuantTradingResearch.Worker.dll').is_file()
    assert (published/'runtimes/linux-x64/native/libe_sqlite3.so').is_file(),'Portable publish missing Linux SQLite native library'
    assert json.loads((fixture/'fixture.json').read_text())==dict(testOnly=True,rowsPerInstrument=1000,readiness400=False,readiness401=True)
    for p in ['private/research.sqlite','private/watermark.json','tls/server.pfx','tls/ca.pem','service.key']:assert (fixture/p).is_file()
    if not execute:
        record['result']='PREFLIGHT_READY';print('Preflight only; runtime NOT RUN; use explicit -Execute');sys.exit(0)
    env['AIQ_RESEARCH_API_KEY']=(fixture/'service.key').read_text().strip()
    assert len(env['AIQ_RESEARCH_API_KEY'])>=32
    record['publishManifestSha256']=hashlib.sha256(json.dumps({p.relative_to(published).as_posix():hashlib.sha256(p.read_bytes()).hexdigest() for p in sorted(published.rglob('*')) if p.is_file()},sort_keys=True).encode()).hexdigest()
    context=out/'context';(context/'research').mkdir(parents=True)
    shutil.copytree(published,context/'research',dirs_exist_ok=True)
    presentation=context/'python/presentation';presentation.mkdir(parents=True)
    modules=['realtime_financial_visualization.py','research_chart.py','research_snapshot_client.py','research_chart_component.js','research_chart_component.html','visualization_read_model.py','historical_market_bridge.py','local_synthetic_chart.py','immutable_synthetic_chart.py','public_preview_api.py']
    record['sourceHashes']={}
    for name in modules:
        p=root/'python/presentation'/name;shutil.copyfile(p,presentation/name);record['sourceHashes'][name]=hashlib.sha256(p.read_bytes()).hexdigest()
    shutil.copyfile(root/'favicon.ico',context/'favicon.ico')
    docker('tag',dependency,owner+':dependencies');owned.append(('dependency',owner+':dependencies'))
    for role,file in [('research','research-api.Dockerfile'),('ui','research-chart.Dockerfile')]:
        tag=owner+':'+role
        docker('build','--platform','linux/amd64','--pull=false','--network=none','--label','aiq.owner='+owner,'--build-context','qualified_python=docker-image://'+owner+':dependencies','-f',str(root/'container/release_1_15'/file),'-t',tag,str(context),timeout=180)
        owned.append(('image',tag))
        meta=json.loads(docker('image','inspect',tag))[0]
        record[role+'Image']=dict(id=meta['Id'],os=meta['Os'],architecture=meta['Architecture'],user=meta['Config']['User'],localManifestDigest=meta.get('Descriptor',{}).get('digest'))
        assert meta['Config']['User'] not in ('','0','root')
    docker('network','create','--internal','--label','aiq.owner='+owner,owner);owned.append(('network',owner))
    docker('network','create','--label','aiq.owner='+owner,owner+'-ingress');owned.append(('network',owner+'-ingress'))
    for volume in ('private','tls'):
        name=owner+'-'+volume;docker('volume','create','--label','aiq.owner='+owner,name);owned.append(('volume',name))
    research_create=['create','--name',research,'--network',owner,'--network-alias','wp07-research','--network-alias','wp07-wrong-host','--label','aiq.owner='+owner,
        '--read-only','--tmpfs','/tmp:rw,nosuid,noexec,size=32m','--cap-drop=ALL','--security-opt=no-new-privileges','--memory=256m','--pids-limit=128',
        '--mount','type=volume,source='+owner+'-private,target=/private','--mount','type=volume,source='+owner+'-tls,target=/tls',
        '--env','AIQ_RESEARCH_API_KEY','--env','DOTNET_PROCESSOR_COUNT=1','--env','AIQ_RESEARCH_PRIVATE_DIRECTORY=/private/data','--env','AIQ_RESEARCH_TLS_PFX=/tls/server.pfx',owner+':research']
    docker(*research_create)
    owned.append(('container',research))
    docker('cp','-a','-',research+':/private',data=archive(fixture/'private',1654,'data'))
    # Populate while stopped, then recreate with a read-only TLS mount before startup.
    # No root application process or privileged helper service is launched.
    docker('cp','-a','-',research+':/tls',data=archive(fixture/'tls',1654))
    assert docker('container','inspect','--format','{{index .Config.Labels "aiq.owner"}}',research)==owner
    docker('container','rm',research);owned.pop()
    index=research_create.index('type=volume,source='+owner+'-tls,target=/tls')
    research_create[index]+=',readonly'
    docker(*research_create);owned.append(('container',research))
    docker('create','--name',ui,'--network',owner+'-ingress','--label','aiq.owner='+owner,'--publish','127.0.0.1::8501',
        '--read-only','--tmpfs','/tmp:rw,nosuid,noexec,size=64m','--cap-drop=ALL','--security-opt=no-new-privileges','--memory=512m','--pids-limit=128',
        '--mount','type=bind,source='+str(fixture/'tls/ca.pem')+',target=/ca.pem,readonly','--env','AIQ_RESEARCH_API_KEY',
        '--env','AIQ_RESEARCH_BASE_URL=https://wp07-research:52731','--env','AIQ_RESEARCH_CA_FILE=/ca.pem',owner+':ui')
    owned.append(('container',ui))
    docker('network','connect',owner,ui)
    began=time.monotonic();docker('start',research);docker('start',ui)
    record['apiBefore']=ready();record['startupToApiProbeSeconds']=time.monotonic()-began
    record['cases']['real_https_contract_and_negatives']='PASS'
    for role,name in [('research',research),('ui',ui)]:
        top=docker('top',name,'-eo','pid,user,args');record[role+'Processes']=top
        assert len(top.splitlines())==2,'Expected one principal application process'
        assert ('dotnet' if role=='research' else 'streamlit') in top
    permissions=docker('exec',research,'/bin/sh','-c','id; stat -c "%u:%g %a %n" /private/data /private/data/research.sqlite /private/data/watermark.json')
    assert '1654:1654 700 /private/data' in permissions and '1654:1654 600 /private/data/research.sqlite' in permissions
    record['permissions']=permissions;record['cases']['private_modes']='PASS'
    assert docker('exec',ui,'/bin/sh','-c','test ! -e /private && test ! -e /tls/server.pfx && echo isolated')=='isolated'
    record['cases']['ui_no_private_mount']='PASS'
    record['uiState']=json.loads(docker('inspect','--format','{{json .State}}',ui))
    assert record['uiState']['Running'],'Actual UI principal exited before browser readiness'
    host=docker('port',ui,'8501/tcp');assert host.startswith('127.0.0.1:')
    browser_env=dict(env,AIQ_WP07_EXTERNAL_UI_URL='http://'+host,AIQ_WP07_CONTAINER_CONTROL=str(out))
    with (out/'browser.log').open('wb') as log:
        browser=subprocess.Popen([sys.executable,'-B',str(root/'tests/runtime/release_1_15/wp07_browser_qualification.py')],env=browser_env,stdout=log,stderr=log,creationflags=subprocess.CREATE_NO_WINDOW)
        deadline=time.monotonic()+180;handled=None
        while browser.poll() is None:
            if time.monotonic()>deadline:raise RuntimeError('Browser deadline')
            command=out/'browser-command.json'
            if command.exists():
                action=json.loads(command.read_text())['action']
                if action!=handled:
                    if action=='stop':docker('stop','--time','5',research)
                    elif action=='start':docker('start',research)
                    else:raise RuntimeError('Unexpected owned browser command')
                    handled=action
                    (out/'browser-ack.pending').write_text(json.dumps(dict(action=action)),encoding='utf-8')
                    (out/'browser-ack.pending').replace(out/'browser-ack.json')
            time.sleep(.1)
        assert browser.returncode==0,'Actual Linux UI browser failed; inspect owned browser.log'
    record['browser']=json.loads((out/'browser.log').read_text().splitlines()[-1])
    record['apiAfter']=ready();assert record['apiAfter']['instruments']==record['apiBefore']['instruments']
    record['cases']['restart_persistence']='PASS'
    docker('kill','--signal','KILL',research)
    docker('start',research)
    record['apiAfterKill']=ready()
    assert record['apiAfterKill']['instruments']==record['apiBefore']['instruments']
    record['cases']['abrupt_process_restart_persistence']='PASS'
    contender=owner+'-writer-contender'
    docker('create','--name',contender,'--label','aiq.owner='+owner,'--network','none','--read-only',
        '--tmpfs','/tmp:rw,nosuid,noexec,size=32m','--cap-drop=ALL','--security-opt=no-new-privileges',
        '--memory=256m','--pids-limit=128','--env','AIQ_RESEARCH_API_KEY','--env','DOTNET_PROCESSOR_COUNT=1',
        '--env','AIQ_RESEARCH_PRIVATE_DIRECTORY=/private/data','--env','AIQ_RESEARCH_TLS_PFX=/tls/server.pfx',
        '--mount','type=volume,source='+owner+'-private,target=/private',
        '--mount','type=volume,source='+owner+'-tls,target=/tls,readonly',owner+':research')
    owned.append(('container',contender))
    try:docker('start','-a',contender,timeout=15)
    except RuntimeError:pass  # Expected only if the contender itself exits nonzero; inspect below.
    assert docker('inspect','--format','{{.State.ExitCode}}',contender)=='1'
    assert docker('inspect','--format','{{.State.Running}}',contender)=='false'
    assert ready()['instruments']==record['apiBefore']['instruments']
    record['cases']['same_volume_writer_exclusion']='PASS'

    record['stats']=[json.loads(line) for line in docker('stats','--no-stream','--format','{{json .}}',research,ui).splitlines()]
    for name in (research,ui):
        logs=docker('logs',name);assert env['AIQ_RESEARCH_API_KEY'] not in logs
    record['cases']['logs_no_service_key']='PASS'
    record['result']='PASS_LOCAL_ONLY'
except Exception as error:
    record['error']=str(error)
    record['result']='FAIL' if execute else 'BLOCKED'
finally:
    if browser and browser.poll() is None:
        # The PID is our still-running Popen child; its fresh-profile browser tree is owned.
        stopped=subprocess.run(['taskkill','/PID',str(browser.pid),'/T','/F'],capture_output=True,timeout=15)
        try:browser.wait(timeout=5)
        except subprocess.TimeoutExpired:
            record.setdefault('cleanupErrors',[]).append('Owned browser tree did not terminate')
            browser.kill();browser.wait(timeout=5)
    for kind,name in owned:
        if kind=='container':
            try:docker('logs',name)
            except Exception:pass
    for kind,name in reversed(owned):
        if kind=='image' and keep:continue
        try:
            if kind=='dependency':docker('image','rm',name);continue
            label=docker(kind,'inspect','--format','{{index .Config.Labels "aiq.owner"}}' if kind in ('container','image') else '{{index .Labels "aiq.owner"}}',name)
            assert label==owner,'Ownership mismatch; cleanup refused'
            docker(kind,'rm',*(['-f'] if kind in ('container','image') else []),name)
        except Exception as error:record.setdefault('cleanupErrors',[]).append(str(error))
    if record.get('cleanupErrors'):record['result']='FAIL_CLEANUP'
    (out/'result.json').write_text(json.dumps(record,indent=2)+'\n',encoding='utf-8',newline='\n')
    print('Owned local report: '+str(out))
    print('Result='+record['result']+'; PUBLIC_RUNTIME_VERIFIED=False')
if record['result'] not in ('PASS_LOCAL_ONLY','PREFLIGHT_READY'):sys.exit(1)

'@
    $code | & $Python -B -
    if ($LASTEXITCODE -ne 0) { throw 'Local Linux qualification failed or blocked; inspect the owned report. No deployment is authorized.' }
}
finally { foreach ($name in $names) { [Environment]::SetEnvironmentVariable($name,$saved[$name],'Process') } }
