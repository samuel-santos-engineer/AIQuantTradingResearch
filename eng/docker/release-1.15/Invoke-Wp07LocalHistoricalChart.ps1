#requires -Version 5.1
[CmdletBinding()]
param(
 [ValidateSet('Validate','Start','Stop')][string]$Action='Validate',
 [string]$ResearchImage, [string]$UiImage, [string]$StateVolume, [string]$TlsDirectory,
 [ValidateRange(1024,65535)][int]$Port=8501,
 [ValidatePattern('^[a-z][a-z0-9-]{2,40}$')][string]$Session='aiq-r115-local-chart',
 [string]$Python='python', [switch]$Execute
)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$names=@('AIQ_CHART_ACTION','AIQ_CHART_RESEARCH','AIQ_CHART_UI','AIQ_CHART_VOLUME','AIQ_CHART_TLS','AIQ_CHART_PORT','AIQ_CHART_SESSION','AIQ_CHART_EXECUTE')
$saved=@{}
foreach($name in $names){$saved[$name]=[Environment]::GetEnvironmentVariable($name,'Process')}
try {
 $env:AIQ_CHART_ACTION=$Action; $env:AIQ_CHART_RESEARCH=$ResearchImage; $env:AIQ_CHART_UI=$UiImage
 $env:AIQ_CHART_VOLUME=$StateVolume; $env:AIQ_CHART_TLS=$TlsDirectory; $env:AIQ_CHART_PORT=[string]$Port
 $env:AIQ_CHART_SESSION=$Session; $env:AIQ_CHART_EXECUTE=[string][bool]$Execute
 $code=@'
import os,sys,json,subprocess,re,socket,time,uuid,tempfile,urllib.request
from pathlib import Path
action=os.environ['AIQ_CHART_ACTION'];session=os.environ['AIQ_CHART_SESSION'];port=int(os.environ['AIQ_CHART_PORT']);execute=os.environ['AIQ_CHART_EXECUTE']=='True'
research=session+'-research';ui=session+'-ui';private=session+'-private';ingress=session+'-ingress';created=[];token=uuid.uuid4().hex
record={'PUBLIC_RUNTIME_VERIFIED':False,'result':'BLOCKED','action':action,'session':session,'stateVolumeRemoved':False}
def d(*args,ok=True):
 p=subprocess.run(['docker',*args],capture_output=True,timeout=40)
 key=os.environ.get('AIQ_RESEARCH_API_KEY','')
 if key and key.encode()in p.stdout+p.stderr:raise RuntimeError('Sensitive diagnostic blocked')
 if ok and p.returncode:raise RuntimeError('Docker '+args[0]+' failed')
 return p

def owned(kind,name):
 fmt='{{json .Config.Labels}}'if kind=='container'else'{{json .Labels}}'
 p=d(kind,'inspect','--format',fmt,name,ok=False)
 if p.returncode:return None
 labels=json.loads(p.stdout)or{}
 if labels.get('aiq.chart.session')!=session:raise RuntimeError('Existing resource is not owned by this session')
 if kind=='container':return {'Config':{'Labels':labels},'State':{'Running':json.loads(d('container','inspect','--format','{{json .State.Running}}',name).stdout)}}
 return {'Labels':labels}

try:
 context=json.loads(d('context','inspect').stdout)[0];assert context['Endpoints']['docker']['Host'].startswith('npipe://'),'Only the owned local Docker endpoint is supported'
 if action=='Stop':
  for name in(ui,research):
   obj=owned('container',name)
   if obj and execute:
    if obj['State']['Running']:d('stop','--time','20',name)
    assert not owned('container',name)['State']['Running'],'Stop must complete before replacement'
  record['result']='STOPPED_STATE_PRESERVED'if execute else'STOP_PLAN_ONLY'
 else:
  images=[os.environ.get('AIQ_CHART_RESEARCH',''),os.environ.get('AIQ_CHART_UI','')]
  for image,user in zip(images,('1654:1654','12002:12002')):
   assert re.fullmatch(r'sha256:[0-9a-f]{64}',image),'Use the qualified immutable local image IDs'
   observed=d('image','inspect','--format','{{.Os}} {{.Architecture}} {{.Config.User}}',image).stdout.decode().split();assert observed==['linux','amd64',user]
  volume=os.environ.get('AIQ_CHART_VOLUME','');assert re.fullmatch(r'[a-zA-Z0-9][a-zA-Z0-9_.-]{0,127}',volume),'Existing named state volume required';assert d('volume','inspect','--format','{{.Driver}}',volume).stdout.strip()==b'local';assert d('volume','inspect','--format','{{len .Options}}',volume).stdout.strip()==b'0','Use an existing default local Docker volume'
  tls=Path(os.environ.get('AIQ_CHART_TLS','')).resolve();assert tls.is_dir()and(tls/'server.pfx').is_file()and(tls/'ca.pem').is_file(),'Existing private PFX/public CA required'
  key=os.environ.get('AIQ_RESEARCH_API_KEY','');assert 32<=len(key)<=256 and all(33<=ord(c)<=126 for c in key),'Existing server-side API key environment required'
  for name in(research,ui):
   obj=owned('container',name)
   if obj and obj['State']['Running']:raise RuntimeError('Stop this session and verify stopped before Start/Validate')
  assert not d('ps','--filter','volume='+volume,'--format','{{.ID}}').stdout.strip(),'State volume has a running container; stop its writer first'
  with socket.socket()as sock:sock.bind(('127.0.0.1',port))
  record.update(researchImage=images[0],uiImage=images[1],url='http://127.0.0.1:'+str(port),result='PREFLIGHT_ONLY')
  if action=='Start'and execute:
   for name in(ui,research):
    if owned('container',name):d('rm',name)
   for name in(ingress,private):
    if owned('network',name):d('network','rm',name)
   labels=['--label','aiq.chart.session='+session,'--label','aiq.chart.launch='+token]
   d('network','create','--internal',*labels,private);created.append(('network',private));d('network','create',*labels,ingress);created.append(('network',ingress))
   limits=['--cap-drop','ALL','--security-opt','no-new-privileges','--read-only','--pids-limit','128']
   d('create','--name',research,*labels,*limits,'--network',private,'--network-alias','wp07-research','--memory','256m','--mount','type=volume,source='+volume+',target=/private','--mount','type=bind,source='+str(tls)+',target=/tls,readonly','--env','AIQ_RESEARCH_PRIVATE_DIRECTORY=/private/data','--env','AIQ_RESEARCH_EXPECTED_MOUNT_PATH=/private','--env','AIQ_RESEARCH_TRANSPORT=local-https','--env','AIQ_RESEARCH_API_KEY','--env','AIQ_RESEARCH_TLS_PFX=/tls/server.pfx',images[0]);created.append(('container',research));d('start',research)
   d('create','--name',ui,*labels,*limits,'--network',ingress,'--publish','127.0.0.1:'+str(port)+':8501','--memory','512m','--tmpfs','/tmp:rw,nosuid,nodev,size=64m','--mount','type=bind,source='+str(tls/'ca.pem')+',target=/ca.pem,readonly','--env','AIQ_RESEARCH_BASE_URL=https://wp07-research:52731','--env','AIQ_RESEARCH_API_KEY','--env','AIQ_RESEARCH_CA_FILE=/ca.pem',images[1]);created.append(('container',ui));d('network','connect',private,ui);d('start',ui)
   probe="""import sys,os,time,json
sys.path.insert(0,'/app/python/presentation')
from research_snapshot_client import ResearchSnapshotClient
client=ResearchSnapshotClient('https://wp07-research:52731',os.environ['AIQ_RESEARCH_API_KEY'],ca_file='/ca.pem')
for attempt in range(30):
 try:
  snapshots={i:client.fetch(i)for i in ('BTC/USD','ETH/USD')}
  counts={i:len(s['candles'])for i,s in snapshots.items()}
  assert all(n>0 for n in counts.values());print(json.dumps(dict(rows=counts,identities={i:dict(s['identity'])for i,s in snapshots.items()})));break
 except Exception as error:
  if attempt==29:print(type(error).__name__,file=sys.stderr);raise SystemExit(1)
  time.sleep(.3)
"""
   checked=d('exec',ui,'/opt/venv/bin/python','-B','-c',probe,ok=False)
   if checked.returncode:
    last=checked.stderr.decode(errors='replace').splitlines()[-1:]
    category=(last[0].split(':',1)[0]if last else'ProbeFailed')
    record['probeFailureCategory']=category if re.fullmatch(r'[A-Za-z_]+',category)else'ProbeFailed'
    raise RuntimeError('Authenticated HTTPS snapshot probe failed')
   record.update(json.loads(checked.stdout))
   end=time.monotonic()+30
   while True:
    try:
     with urllib.request.urlopen(record['url']+'/_stcore/health',timeout=2)as response:assert response.status==200
     break
    except Exception:
     if time.monotonic()>end:raise RuntimeError('UI readiness deadline exceeded')
     time.sleep(.2)
   record['result']='RUNNING_LOCAL_ONLY';record['stopCommand']='Invoke-Wp07LocalHistoricalChart.ps1 -Action Stop -Session '+session+' -Execute'
except Exception as error:
 record['failure']=str(error);record['result']='FAIL'
 record['containerStates']={}
 for name in(research,ui):
  try:
   state=d('container','inspect','--format','{{.State.Status}} {{.State.ExitCode}}',name,ok=False)
   if state.returncode==0:record['containerStates'][name]=state.stdout.decode().strip()
  except Exception:pass
finally:
 errors=[]
 if record['result']=='FAIL':
  for kind,name in reversed(created):
   try:
    obj=owned(kind,name);labels=obj.get('Config',{}).get('Labels',{})if kind=='container'else obj.get('Labels',{})
    assert labels['aiq.chart.launch']==token
    d('rm','--force',name)if kind=='container'else d('network','rm',name)
   except Exception:errors.append('Owned '+kind+' cleanup failed')
 record['cleanupErrors']=errors
 if errors:record['result']='FAIL_CLEANUP'
 folder=Path(tempfile.gettempdir())/('aiq-chart-launch-'+token);folder.mkdir();(folder/'result.json').write_text(json.dumps(record,indent=2),encoding='utf-8',newline='\n')
 print('Owned local report: '+str(folder));print('Result='+record['result']+'; PUBLIC_RUNTIME_VERIFIED=False')
 if record['result']=='RUNNING_LOCAL_ONLY':print('Local chart: '+record['url'])
 sys.exit(1 if record['result'].startswith('FAIL')else 0)
'@
 $code | & $Python -B -
 if($LASTEXITCODE -ne 0){throw 'Local chart operation failed; inspect the owned local report. State volume is preserved.'}
}
finally {foreach($name in $names){[Environment]::SetEnvironmentVariable($name,$saved[$name],'Process')}}
