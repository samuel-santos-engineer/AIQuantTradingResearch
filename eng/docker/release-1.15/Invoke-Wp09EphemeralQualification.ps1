#Requires -Version 5.1
[CmdletBinding()]
param([Parameter(Mandatory=$true)][string]$ResearchImage,
      [Parameter(Mandatory=$true)][string]$PublishedResearchDirectory,
      [switch]$Execute)
$ErrorActionPreference='Stop'
$names=@('AIQ_WP09_IMAGE','AIQ_WP09_PUBLISH','AIQ_WP09_ROOT','AIQ_WP09_EXECUTE')
$saved=@{};foreach($name in $names){$saved[$name]=[Environment]::GetEnvironmentVariable($name,'Process')}
$env:AIQ_WP09_IMAGE=$ResearchImage
$env:AIQ_WP09_PUBLISH=[IO.Path]::GetFullPath($PublishedResearchDirectory)
$env:AIQ_WP09_ROOT=[IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../../..'))
$env:AIQ_WP09_EXECUTE=[string][bool]$Execute
try {
@'
import os,json,re,subprocess,tempfile,uuid,shutil
from pathlib import Path
image=os.environ['AIQ_WP09_IMAGE'];publish=Path(os.environ['AIQ_WP09_PUBLISH'])
root=Path(os.environ['AIQ_WP09_ROOT']);owner='aiq-wp09-'+uuid.uuid4().hex
owned=[];results={}
def run(a,timeout=35):return subprocess.run(a,capture_output=True,timeout=timeout)
def docker(*a):
 p=run(['docker',*a]);assert p.returncode==0,'Docker operation failed';return p.stdout
assert re.fullmatch(r'sha256:[0-9a-f]{64}',image)
assert json.loads(docker('context','inspect'))[0]['Endpoints']['docker']['Host'].startswith('npipe://')
meta=json.loads(docker('image','inspect',image))[0]
assert meta['Os']=='linux' and meta['Architecture']=='amd64' and meta['Config']['User']=='1654:1654'
assert (publish/'AIQuantTradingResearch.Worker.dll').is_file()
if os.environ['AIQ_WP09_EXECUTE']!='True':
 print('PREFLIGHT_ONLY; no compilation/container/state operation; PUBLIC_RUNTIME_VERIFIED=False');raise SystemExit(0)
out=Path(tempfile.mkdtemp(prefix=owner+'-'));publish=Path(shutil.copytree(publish,out/'publish'));assembly=publish/'AIQuantTradingResearch.Infrastructure.Tests.dll'
try:
 dotnet=Path(shutil.which('dotnet'));sdk=run(['dotnet','--version']).stdout.decode().strip()
 refs=sorted((dotnet.parent/'packs/Microsoft.NETCore.App.Ref').glob('10.*/ref/net10.0'),key=lambda p:tuple(map(int,p.parts[-3].split('.'))))[-1]
 lines=['/noconfig','/nostdlib+','/nullable:enable','/target:exe','/out:"'+str(assembly)+'"']
 lines+=['/r:"'+str(p)+'"'for p in [*refs.glob('*.dll'),*publish.glob('*.dll')]]
 lines+=['"'+str(root/'tests/runtime/release_1_15/wp09_ephemeral_probe.cs')+'"']
 rsp=out/'compile.rsp';rsp.write_text('\n'.join(lines),encoding='utf-8')
 compiled=run(['dotnet',str(dotnet.parent/'sdk'/sdk/'Roslyn/bincore/csc.dll'),'@'+str(rsp)])
 assert compiled.returncode==0,'Offline probe compilation failed'
 env={'AIQ_RESEARCH_STORAGE_SCOPE':'azure-f1-staging-ephemeral','AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING':'1',
 'WEBSITE_SITE_NAME':'aiqr115researchstagingwus27f5eabb5','WEBSITE_HOSTNAME':'aiqr115researchstagingwus27f5eabb5.azurewebsites.net',
 'WEBSITE_INSTANCE_ID':'simulated-local-not-azure','WEBSITE_ROLE_INSTANCE_ID':'0',
 'WEBSITES_ENABLE_APP_SERVICE_STORAGE':'false','AIQ_RESEARCH_EXPECTED_MOUNT_PATH':'/'}
 def case(name,mode='reject',change=None,extra=(),startup=False):
  settings=env.copy();settings.update(change or {})
  container=owner+'-'+name;owned.append(container)
  args=['docker','run','--name',container,'--label','aiq.owner='+owner,'--network','none','--cap-drop','ALL',
  '--security-opt','no-new-privileges','--memory','256m','--pids-limit','64',
  '--mount','type=bind,source='+str(publish)+',target=/app,readonly',*extra]
  for k,v in settings.items():
   if v is not None:args+=['--env',k+'='+v]
  args+=['--entrypoint','dotnet',image]
  if startup:args+=['AIQuantTradingResearch.Worker.dll','--research-snapshot-api']
  else:args+=['exec','--runtimeconfig','/app/AIQuantTradingResearch.Worker.runtimeconfig.json',
  '--depsfile','/app/AIQuantTradingResearch.Worker.deps.json','/app/AIQuantTradingResearch.Infrastructure.Tests.dll',mode]
  p=run(args)
  assert p.returncode==(1 if startup else 0),name+' failed; status='+str(p.returncode)+'; probe='+p.stdout.decode(errors='replace')[:160]
  if not startup:assert (b'PASS' in p.stdout if mode in ('pass','probe') else b'REJECTED' in p.stdout),name+' unexpected result'
  results[name]='PASS_LOCAL'
 case('probe','probe')
 case('probe-denied','probe-reject',change={'AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING':None})
 case('overlay','pass')
 case('default-denial',change={'AIQ_RESEARCH_STORAGE_SCOPE':None,'AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING':None})
 case('wrong-site',change={'WEBSITE_SITE_NAME':'production'})
 case('missing-instance',change={'WEBSITE_INSTANCE_ID':None})
 case('wrong-uid',extra=('--user','12002:12002'))
 case('readonly',extra=('--read-only',))
 case('tmpfs',extra=('--tmpfs','/tmp:rw,size=16m'))
 case('symlink','symlink')
 case('permissions','permissions')
 case('invalid-db','invalid-db')
 case('missing-db',startup=True,change={'AIQ_RESEARCH_PRIVATE_DIRECTORY':'/tmp/aiq-research'})
 print(json.dumps({'result':'PASS_LOCAL','cases':results,'azure':'NOT_RUN','PUBLIC_RUNTIME_VERIFIED':False}))
finally:
 for name in reversed(owned):
  p=run(['docker','inspect','--format','{{ index .Config.Labels "aiq.owner" }}',name])
  if p.returncode==0:
   assert p.stdout.decode().strip()==owner
   docker('rm','--force',name)
 # Retain only owned temporary compiler inputs; no export, service or provider keys.
'@ | python -B -
if($LASTEXITCODE){throw 'WP09 local smoke failed.'}
} finally {foreach($name in $names){[Environment]::SetEnvironmentVariable($name,$saved[$name],'Process')}}
