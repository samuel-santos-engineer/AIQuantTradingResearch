"""WP11 fixed-target diagnostic controller. No UI/provider/import operation exists."""
import argparse
import datetime as dt
import json
import hashlib
import tarfile
import os
from pathlib import Path
import re
import subprocess
import sys
import time
import zipfile

APP='aiqr115researchstagingwus27f5eabb5'
GROUP='rg-aiq-r112-wp05-wus2-7f5eabb5'
AZ=[r'C:\Program Files\Microsoft SDKs\Azure\CLI2\python.exe','-IBm','azure.cli']
API='2023-12-01'

def save(root,name,value):
    (root/name).write_text(json.dumps(value,indent=2),encoding='utf-8',newline='\n')

def az(*args,timeout=20):
    p=subprocess.run([*AZ,*args,'--only-show-errors'],capture_output=True,timeout=timeout)
    if p.returncode:raise RuntimeError('Exact research-app operation failed')
    return json.loads(p.stdout) if p.stdout.strip() else None

def comparable(value):
    return {k:v for k,v in value.items() if k not in ('systemData','etag','eTag')}

def logging_config():return az('webapp','log','show','-g',GROUP,'-n',APP,'-o','json')

def config():return az('webapp','config','show','-g',GROUP,'-n',APP,'-o','json')
def settings():return az('webapp','config','appsettings','list','-g',GROUP,'-n',APP,'-o','json')
def site():return az('webapp','show','-g',GROUP,'-n',APP,'-o','json',timeout=15)
def start_app():return az('webapp','start','-g',GROUP,'-n',APP,'-o','none',timeout=12)

def stop_verified():
    acknowledged=None
    try:
        az('webapp','stop','-g',GROUP,'-n',APP,'-o','none',timeout=12)
        acknowledged=time.monotonic()
    except Exception:pass # Stop might have completed remotely; verification is still mandatory.
    for attempt in range(3):
        try:
            if site()['state']=='Stopped':return acknowledged if acknowledged is not None else time.monotonic()
        except Exception:pass # A short read failure must not abandon mandatory rollback.
        if attempt<2:time.sleep(1)
    raise RuntimeError('Research stop not confirmed')

def patch_config(root,resource,values):
    save(root,'config-body.json',{'properties':values})
    az('rest','--method','patch','--url',resource+'/config/web?api-version='+API,
       '--body','@'+str(root/'config-body.json'),'-o','none')

def put_settings(root,resource,values):
    save(root,'settings-body.json',{'properties':values})
    az('rest','--method','put','--url',resource+'/config/appsettings?api-version='+API,
       '--body','@'+str(root/'settings-body.json'),'-o','none')

def parse_archive(path):
    # Raw archives stay in protected state. Return only fixed booleans/numeric conclusions.
    found=None;uid=None;total=0;mounts={}
    with zipfile.ZipFile(path) as archive:
        for entry in archive.infolist():
            if entry.is_dir() or total>=10000000:continue
            with archive.open(entry) as f:data=f.read(min(2000000,10000000-total))
            total+=len(data)
            for line in data.decode('utf-8',errors='replace').splitlines():
                match=re.search(r'UID_FIELDS (\d+) (\d+) (\d+) (\d+)',line)
                if match:uid=all(x=='1654' for x in match.groups())
                mount=re.search(r'MOUNT (\d+):\d+ rootIsSlash=([01]) (/(?:tmp(?:/aiq-research)?)?) ([a-z,]+) ([A-Za-z0-9_.-]+)',line)
                if mount:
                    major,root_slash,path,options,kind=mount.groups()
                    mounts[path]={'filesystem':kind,'rootIsSlash':root_slash=='1','writable':'rw' in options.split(','),'deviceMajorZero':major=='0'}
                marker='WP11_STORAGE_PROBE '
                if marker in line:
                    text=line.split(marker,1)[1]
                    try:value=json.JSONDecoder().raw_decode(text)[0]
                    except (ValueError,TypeError):continue
                    keys={'linux','hostOptIn','kernel','privateWritable','accepted','probeCleanup'}
                    if type(value) is dict and set(value)==keys and all(type(x) is bool for x in value.values()):found=value
    return found,uid,mounts

def window(root,record):
    started=time.monotonic();record['startedUtc']=dt.datetime.now(dt.timezone.utc).isoformat()
    save(root,'watchdog.json',{'stopAt':started+30})
    flags=subprocess.CREATE_NO_WINDOW if os.name=='nt' else 0
    watcher=subprocess.Popen([sys.executable,'-B',str(Path(__file__).resolve()),'--state',str(root),'--watchdog'],
                             stdout=subprocess.DEVNULL,stderr=subprocess.DEVNULL,creationflags=flags)
    record['_watcher']=watcher;record['_started']=started
    start_app()
    for attempt in range(4):
        remaining=started+25-time.monotonic()
        if remaining<=1:break
        path=root/('candidate-logs-'+str(attempt)+'.zip')
        try:
            az('webapp','log','download','-g',GROUP,'-n',APP,'--log-file',str(path),timeout=min(8,remaining))
            probe,uid,mounts=parse_archive(path)
            if probe is not None:
                record['guardAccepted']=probe['accepted'];record['linux']=probe['linux']
                record['exactHostOptIn']=probe['hostOptIn'];record['kernelEligible']=probe['kernel']
                record['privateWriteProbe']=probe['privateWritable'];record['probeCleanup']=probe['probeCleanup']
                record['allUidFields1654']=uid;record['mountConclusions']=mounts;record['runtimeObserved']=True;break
        except Exception:record['logReadFailures']=record.get('logReadFailures',0)+1
        time.sleep(min(1,max(0,started+25-time.monotonic())))

def restore(root,resource,baseline,record):
    stopped_at=stop_verified()
    if '_started' in record:
        record['candidateElapsedSeconds']=round((stopped_at if stopped_at is not None else time.monotonic())-record['_started'],3)
        record['elapsedBasis']='successful_stop_acknowledgment_then_verified_stopped'
        record['within60Seconds']=record['candidateElapsedSeconds']<=60
    (root/'watchdog.cancel').write_text('stop confirmed',encoding='utf-8')
    watcher=record.pop('_watcher',None)
    if watcher is not None:watcher.wait(timeout=15)
    patch_config(root,resource,{k:baseline['config'].get(k) for k in ('linuxFxVersion','appCommandLine')})
    if 'logs' in baseline:
        save(root,'logs-body.json',{'properties':{k:baseline['logs'][k] for k in ('applicationLogs','detailedErrorMessages','failedRequestsTracing','httpLogs')}})
        az('rest','--method','put','--url',resource+'/config/logs?api-version='+API,'--body','@'+str(root/'logs-body.json'),'-o','none')
    put_settings(root,resource,{x['name']:x['value'] for x in baseline['settings']})
    if baseline['site']['state']=='Running':start_app()
    current_config=config();current_settings=settings();current_site=site()
    save(root,'restored-config.json',current_config);save(root,'restored-settings.json',current_settings);save(root,'restored-site.json',current_site)
    record['configRestored']=comparable(current_config)==comparable(baseline['config'])
    record['settingsRestored']=current_settings==baseline['settings']
    keys=('state','kind','httpsOnly','serverFarmId','publicNetworkAccess')
    record['siteRestored']=all(current_site.get(k)==baseline['site'].get(k) for k in keys)
    record['loggingRestored']=True
    if 'logs' in baseline:
        current_logs=logging_config();save(root,'restored-logs.json',current_logs)
        record['loggingRestored']=comparable(current_logs)==comparable(baseline['logs'])
    record['rollbackVerified']=record['configRestored'] and record['settingsRestored'] and record['siteRestored'] and record['loggingRestored']
    if not record['rollbackVerified']:
        record['configDifferenceNames']=sorted(k for k in set(current_config)|set(baseline['config']) if current_config.get(k)!=baseline['config'].get(k))
        raise RuntimeError('Exact baseline rollback verification failed')

def execute_azure(root,plan):
    baseline={k:json.loads((root/(k+'.json')).read_text()) for k in ('site','config','settings')}
    if (root/'logs.json').exists():baseline['logs']=json.loads((root/'logs.json').read_text())
    resource='https://management.azure.com'+baseline['site']['id']
    expected='/resourceGroups/'+GROUP+'/providers/Microsoft.Web/sites/'+APP
    if not resource.lower().endswith(expected.lower()):raise RuntimeError('Wrong baseline target')
    if comparable(config())!=comparable(baseline['config']) or settings()!=baseline['settings']:raise RuntimeError('Baseline changed before mutation')
    if site()['state']!=baseline['site']['state']:raise RuntimeError('Baseline state changed')
    record={'PUBLIC_RUNTIME_VERIFIED':False,'target':APP,'runtimeObserved':False,'rollbackVerified':False,
            'candidateDigest':plan['digest'],'providerRequests':0,'result':'INCOMPLETE'}
    save(root,'execution-armed.json',{'target':APP,'phase':'before-first-stop'})
    try:
        stop_verified()
        patch_config(root,resource,{'linuxFxVersion':'DOCKER|'+plan['immutable']})
        values={x['name']:x['value'] for x in baseline['settings']}
        values.update({'AIQ_RESEARCH_BOOTSTRAP_MODE':'diagnose','AIQ_RESEARCH_STORAGE_SCOPE':'azure-f1-staging-ephemeral',
                       'AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING':'1','AIQ_RESEARCH_EXPECTED_MOUNT_PATH':'/',
                       'WEBSITES_ENABLE_APP_SERVICE_STORAGE':'false'})
        put_settings(root,resource,values)
        if 'logs' in baseline:az('webapp','log','config','-g',GROUP,'-n',APP,'--docker-container-logging','filesystem','-o','none')
        window(root,record)
    except Exception:record['operationFailed']=True
    finally:
        try:restore(root,resource,baseline,record)
        except Exception:
            record['criticalRollbackUnconfirmed']=True
            save(root,'rollback-error.json',{'stopAndRestoreRequired':True})
        record.pop('_started',None);record.pop('_watcher',None)
        if record.get('rollbackVerified') and record.get('within60Seconds') and record.get('runtimeObserved') and record.get('probeCleanup'):
            record['result']='A_COMPATIBLE_ROLLED_BACK' if record['guardAccepted'] else 'B_REJECTED_ROLLED_BACK'
        save(root,'result.json',record)
    return record

def verify_published_identity(local,manifest,registry_index,archive_path):
    # Docker containerd exposes OCI index IDs; traditional stores expose config IDs.
    if local.get('Descriptor',{}).get('mediaType')=='application/vnd.oci.image.index.v1+json':
        if registry_index!=local['Id']:raise RuntimeError('Published OCI index differs from qualified local index')
        matches=[]
        with tarfile.open(archive_path) as archive:
            for entry in json.load(archive.extractfile('manifest.json')):
                data=archive.extractfile(entry['Config']).read();value=json.loads(data)
                if value.get('os')=='linux' and value.get('architecture')=='amd64':
                    if value['rootfs']['diff_ids']!=local['RootFS']['Layers'] or value['config']['User']!='1654:1654':
                        raise RuntimeError('Qualified platform/rootfs/user mismatch')
                    matches.append('sha256:'+hashlib.sha256(data).hexdigest())
        if len(matches)!=1 or matches[0]!=manifest['config']['digest']:raise RuntimeError('Published config differs from independent local archive')
    elif manifest['config']['digest']!=local['Id']:
        raise RuntimeError('Published config differs from qualified local config')

def publish(root,plan):
    if plan.get('digest'):return plan
    ref=plan['repository']+':'+plan['tag']
    if not plan.get('pushed'):
        for args in (['tag',plan['imageId'],ref],['push',ref]):
            p=subprocess.run(['docker',*args],capture_output=True,timeout=180)
            if p.returncode:raise RuntimeError('Research publication blocked; no Azure mutation')
        plan['pushed']=True;save(root,'publication-plan.json',plan) # Never repeat a completed upload for readback.
    p=subprocess.run(['docker','image','inspect',plan['imageId']],capture_output=True,timeout=15)
    if p.returncode:raise RuntimeError('Qualified local artifact unavailable')
    local=json.loads(p.stdout)[0]
    p=subprocess.run(['docker','buildx','imagetools','inspect',ref,'--format','{{json .Manifest.Digest}}'],capture_output=True,timeout=30)
    if p.returncode:raise RuntimeError('Registry index readback failed')
    index_digest=json.loads(p.stdout)
    def manifest(ref):
        p=subprocess.run(['docker','buildx','imagetools','inspect',ref,'--raw'],capture_output=True,timeout=30)
        if p.returncode:raise RuntimeError('Registry readback failed')
        return json.loads(p.stdout)
    m=manifest(ref)
    if 'manifests' in m:
        candidates=[x for x in m['manifests'] if x.get('platform',{}).get('os')=='linux' and x.get('platform',{}).get('architecture')=='amd64']
        if len(candidates)!=1:raise RuntimeError('Ambiguous research platform')
        digest=candidates[0]['digest'];m=manifest(plan['repository']+'@'+digest)
    else:
        p=subprocess.run(['docker','buildx','imagetools','inspect',ref,'--format','{{json .Manifest.Digest}}'],capture_output=True,timeout=30)
        if p.returncode:raise RuntimeError('Digest readback failed')
        digest=json.loads(p.stdout)
    archive=root/'qualified-image.tar'
    if local.get('Descriptor',{}).get('mediaType')=='application/vnd.oci.image.index.v1+json' and not archive.exists():
        p=subprocess.run(['docker','image','save','-o',str(archive),plan['imageId']],capture_output=True,timeout=90)
        if p.returncode:raise RuntimeError('Independent local artifact export failed')
    verify_published_identity(local,m,index_digest,archive)
    immutable=plan['repository']+'@'+digest
    p=subprocess.run(['docker','--config',str(root/'anonymous-docker'),'manifest','inspect',immutable],capture_output=True,timeout=30)
    if p.returncode:raise RuntimeError('Anonymous Azure pull prerequisite failed')
    plan.update({'digest':digest,'immutable':immutable});save(root,'publication-plan.json',plan)
    print('Research publication/config-match/anonymous readback verified; digest='+digest,flush=True)
    return plan

def main():
    parser=argparse.ArgumentParser();parser.add_argument('--state',type=Path,required=True);parser.add_argument('--execute',action='store_true');parser.add_argument('--watchdog',action='store_true')
    args=parser.parse_args();root=args.state
    if args.watchdog:
        stop_at=json.loads((root/'watchdog.json').read_text())['stopAt']
        while time.monotonic()<stop_at:
            if (root/'watchdog.cancel').exists():return
            time.sleep(.2)
        if not (root/'watchdog.cancel').exists():
            try:az('webapp','stop','-g',GROUP,'-n',APP,'-o','none',timeout=12);save(root,'watchdog-fired.json',{'stopRequested':True})
            except Exception:save(root,'watchdog-fired.json',{'stopRequested':False})
        return
    plan=json.loads((root/'publication-plan.json').read_text())
    if not re.fullmatch(r'sha256:[0-9a-f]{64}',plan.get('imageId','')):raise SystemExit('Qualified candidate missing')
    if (root/'execution-armed.json').exists():raise SystemExit('Prior execution exists; reconcile without rerunning')
    if not args.execute:print('PREFLIGHT_ONLY; publication/Azure mutations NOT_RUN');return
    plan=publish(root,plan)
    result=execute_azure(root,plan);print(json.dumps(result))
    if not result.get('rollbackVerified'):raise SystemExit(2)
    if result['result']=='INCOMPLETE':raise SystemExit(1)

if __name__=='__main__':main()
