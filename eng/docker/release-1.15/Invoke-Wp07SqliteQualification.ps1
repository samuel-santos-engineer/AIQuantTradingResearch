#requires -Version 5.1
[CmdletBinding()]
param(
 [Parameter(Mandatory=$true)][string]$ResearchImage,
 [Parameter(Mandatory=$true)][string]$PublishedResearchDirectory,
 [string]$Python='python', [switch]$Execute
)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$names=@('AIQ_SQLITE_ROOT','AIQ_SQLITE_IMAGE','AIQ_SQLITE_PUBLISH','AIQ_SQLITE_EXECUTE')
$saved=@{}
foreach($name in $names){$saved[$name]=[Environment]::GetEnvironmentVariable($name,'Process')}
try {
 $env:AIQ_SQLITE_ROOT=[IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../../..'))
 $env:AIQ_SQLITE_IMAGE=$ResearchImage
 $env:AIQ_SQLITE_PUBLISH=[IO.Path]::GetFullPath($PublishedResearchDirectory)
 $env:AIQ_SQLITE_EXECUTE=[string][bool]$Execute
 $code=@'
import os,sys,json,subprocess,uuid,tempfile,hashlib,io,tarfile,time,shutil,re
from pathlib import Path
root=Path(os.environ['AIQ_SQLITE_ROOT']);publish=Path(os.environ['AIQ_SQLITE_PUBLISH']);image=os.environ['AIQ_SQLITE_IMAGE']
owner='aiq-wp07-sqlite-'+uuid.uuid4().hex;out=Path(tempfile.gettempdir())/owner;out.mkdir();owned=[]
record={'PUBLIC_RUNTIME_VERIFIED':False,'result':'BLOCKED','cases':{},'power_loss':'NOT_RUN','cross_host_fencing':'BLOCKED','azure_durability':'BLOCKED'}
canaries=['AAECAwQFBgcICQoLDA0ODxAREhMUFRYXGBkaGxwdHh8=','KioqKioqKioqKioqKioqKioqKioqKioqKioqKioqKioqKio=']
def run(args,data=None,ok=True,timeout=40):
 p=subprocess.run(args,input=data,capture_output=True,timeout=timeout)
 assert not any(s.encode()in p.stdout+p.stderr for s in canaries),'Secret in diagnostic output'
 if ok and p.returncode:raise RuntimeError(args[0]+' '+args[1]+' failed: '+p.stderr.decode(errors='replace')[:200])
 return p
def d(*a,**kw):return run(['docker',*a],**kw)
def mount(v):return 'type=volume,source='+v+',target=/private'
def args(v,extra=()):
 return ['--label','aiq.owner='+owner,'--network','none','--cap-drop','ALL','--security-opt','no-new-privileges','--read-only','--memory','256m','--pids-limit','64','--mount',mount(v),'--mount','type=bind,source='+str(probe)+',target=/app/AIQuantTradingResearch.Infrastructure.Tests.dll,readonly','--env','AIQ_RESEARCH_EXPECTED_MOUNT_PATH=/private',*extra,'--entrypoint','dotnet',image,'exec','--runtimeconfig','/app/AIQuantTradingResearch.Worker.runtimeconfig.json','--depsfile','/app/AIQuantTradingResearch.Worker.deps.json','/app/AIQuantTradingResearch.Infrastructure.Tests.dll']
def command(v,mode,*rest,extra=(),expected=0,readonly=False):
 launch=args(v,extra)
 if readonly:launch=[x+',readonly'if x==mount(v)else x for x in launch]
 p=d('run','--rm',*launch,mode,*rest,ok=False);assert p.returncode==expected,(mode,p.returncode);return p
def volume(suffix):
 v=owner+'-'+suffix;d('volume','create','--label','aiq.owner='+owner,v);owned.append(('volume',v))
 c=v+'-seed';d('create','--name',c,*args(v),'seed');owned.append(('container',c));b=io.BytesIO()
 with tarfile.open(fileobj=b,mode='w')as t:
  for name in ('data','control'):
   q=tarfile.TarInfo(name);q.type=tarfile.DIRTYPE;q.uid=q.gid=1654;q.mode=0o700;t.addfile(q)
 d('cp','-',c+':/private',data=b.getvalue());d('start','--attach',c);return v
def background(v,mode,*rest):
 c=owner+'-'+uuid.uuid4().hex[:8];d('run','--detach','--name',c,*args(v),mode,*rest);owned.append(('container',c));end=time.monotonic()+15
 while time.monotonic()<end:
  if d('exec',c,'test','-f','/private/control/ready',ok=False).returncode==0:return c
  if d('inspect','--format','{{.State.Running}}',c).stdout.strip()!=b'true':raise RuntimeError('Probe exited before deterministic marker')
  time.sleep(.1)
 raise RuntimeError('Fault marker deadline exceeded')
try:
 assert re.fullmatch(r'sha256:[0-9a-f]{64}',image)
 context=json.loads(d('context','inspect').stdout)[0];assert context['Endpoints']['docker']['Host'].startswith('npipe://')
 info=json.loads(d('image','inspect',image).stdout)[0];assert info['Os']=='linux'and info['Architecture']=='amd64'and info['Config']['User']=='1654:1654'
 record['imageId']=info['Id'];record['docker']=d('version','--format','{{.Server.Version}}').stdout.decode().strip();assert(publish/'AIQuantTradingResearch.Infrastructure.dll').is_file()
 record['sourceHashes']={p:hashlib.sha256((root/p).read_bytes()).hexdigest()for p in ['tests/runtime/release_1_15/wp07_sqlite_fault_probe.cs','src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStorageBoundary.cs']}
 if os.environ['AIQ_SQLITE_EXECUTE']!='True':record['result']='PREFLIGHT_ONLY'
 else:
  binary=hashlib.sha256((publish/'AIQuantTradingResearch.Infrastructure.dll').read_bytes()).hexdigest()
  observed=d('run','--rm','--label','aiq.owner='+owner,'--network','none','--read-only','--entrypoint','/bin/sh',image,'-c','sha256sum /app/AIQuantTradingResearch.Infrastructure.dll').stdout.decode().split()[0]
  assert observed==binary,'Published dependencies do not match qualified image'
  record['infrastructureBinarySha256']=binary
  dotnet=Path(shutil.which('dotnet'));sdk=run(['dotnet','--version']).stdout.decode().strip();assert sdk.startswith('10.0.');csc=dotnet.parent/'sdk'/sdk/'Roslyn/bincore/csc.dll'
  refs=sorted((dotnet.parent/'packs/Microsoft.NETCore.App.Ref').glob('10.*/ref/net10.0'),key=lambda p:tuple(map(int,p.parts[-3].split('.'))))[-1]
  probe=out/'AIQuantTradingResearch.Infrastructure.Tests.dll';rsp=out/'compile.rsp'
  lines=['/noconfig','/nostdlib+','/nullable:enable','/target:exe','/out:"'+str(probe)+'"']+['/r:"'+str(p)+'"'for p in refs.glob('*.dll')]
  lines+=['/r:"'+str(publish/name)+'"'for name in ['AIQuantTradingResearch.Domain.dll','AIQuantTradingResearch.Application.dll','AIQuantTradingResearch.Infrastructure.dll','Microsoft.Data.Sqlite.dll']]
  lines+=['"'+str(root/'tests/runtime/release_1_15/wp07_sqlite_fault_probe.cs')+'"'];rsp.write_text('\n'.join(lines),encoding='utf-8',newline='\n')
  run(['dotnet',str(csc),'@'+str(rsp)]);record['probeSha256']=hashlib.sha256(probe.read_bytes()).hexdigest();record['sdk']=sdk
  v=volume('overlap');holder=background(v,'hold');record['localFilesystemStat']=d('exec',holder,'stat','-f','-c','%T','/private/data').stdout.decode().strip();command(v,'recover','1',expected=1);d('kill','--signal','SIGKILL',holder);d('wait',holder);command(v,'recover','1');record['cases']['same_volume_distinct_container_denial_and_takeover']='PASS_LOCAL'
  a=volume('local-a');b=volume('local-b');ca=background(a,'hold');cb=background(b,'hold');record['cases']['separate_volume_writers_both_run_negative_control']='PASS_LOCAL_LIMITATION'
  for c in(ca,cb):d('kill','--signal','SIGKILL',c);d('wait',c)
  for i,point in enumerate(('before-commit','watermark-replace','transform-before-commit','transform-after-commit')):
   v=volume('fault'+str(i));c=background(v,'crash',point);assert d('exec',c,'cat','/private/control/ready').stdout.decode()==point;d('kill','--signal','SIGKILL',c);d('wait',c)
   if point=='transform-after-commit':command(v,'expect-recovery')
   else:command(v,'recover','2'if point=='watermark-replace'else'1')
   record['cases'][point+'_interrupted']='PASS_LOCAL'
  v=volume('negatives');command(v,'recover','1',readonly=True,expected=1);record['cases']['read_only_mount']='PASS_LOCAL';command(v,'recover','1',extra=('--env','AIQ_RESEARCH_EXPECTED_MOUNT_PATH=/wrong'),expected=1);record['cases']['mount_mismatch']='PASS_LOCAL'
  command(v,'recover','1',extra=('--user','12002:12002'),expected=1);record['cases']['wrong_uid']='PASS_LOCAL'
  for instance in('instance-a','instance-b'):
   command(v,'new-store',extra=('--env','WEBSITE_INSTANCE_ID='+instance,'--env','AIQ_RESEARCH_STORAGE_SCOPE=local-qualification'),expected=1)
   d('run','--rm','--label','aiq.owner='+owner,'--network','none','--read-only','--mount',mount(v),'--entrypoint','/bin/sh',image,'-c','test ! -e /private/control/never-created')
  command(v,'recover','1');record['cases']['both_azure_instances_blocked_before_write']='PASS_LOCAL';command(v,'tamper');command(v,'expect-recovery');record['cases']['tampered_witness_rejected']='PASS_LOCAL'
  modes=d('run','--rm','--label','aiq.owner='+owner,'--network','none','--read-only','--mount',mount(v),'--entrypoint','/bin/sh',image,'-c','stat -c "%u:%g %a" /private/data /private/data/*').stdout.decode().splitlines()
  assert modes[0]=='1654:1654 700'and all(x=='1654:1654 600'for x in modes[1:]);record['cases']['private_modes_and_no_secret_output']='PASS_LOCAL';record['result']='PASS_LOCAL_ONLY_AZURE_BLOCKED'
except Exception as e:record['failure']=str(e);record['result']='FAIL'
finally:
 errors=[]
 for kind,name in reversed(owned):
  try:
   fmt='{{ index .Config.Labels "aiq.owner" }}'if kind=='container'else'{{ index .Labels "aiq.owner" }}';assert d(kind,'inspect','--format',fmt,name).stdout.decode().strip()==owner
   d('rm','--force',name)if kind=='container'else d('volume','rm',name)
  except Exception:errors.append(kind+' cleanup failed')
 record['cleanupErrors']=errors
 if errors:record['result']='FAIL_CLEANUP'
 (out/'result.json').write_text(json.dumps(record,indent=2),encoding='utf-8',newline='\n');print('Owned local report: '+str(out));print('Result='+record['result']+'; PUBLIC_RUNTIME_VERIFIED=False')
 sys.exit(0 if record['result']in('PREFLIGHT_ONLY','PASS_LOCAL_ONLY_AZURE_BLOCKED')else 1)
'@
 $code | & $Python -B -
 if($LASTEXITCODE -ne 0){throw 'SQLite qualification failed; inspect the owned local report.'}
}
finally {foreach($name in $names){[Environment]::SetEnvironmentVariable($name,$saved[$name],'Process')}}
