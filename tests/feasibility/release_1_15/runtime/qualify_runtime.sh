#!/bin/sh
# Read-only fixture observations. Host owns start/fault/cleanup and bounded deadlines.
set -eu
python -B - <<'PY'
import json,os,sqlite3,subprocess,time
from pathlib import Path
from urllib.request import urlopen
limit=time.monotonic()+30
while True:
    try:
        with urlopen('http://127.0.0.1:52731/health',timeout=1) as response: health=json.load(response)
        break
    except Exception:
        if time.monotonic()>limit: raise
        time.sleep(.2)
children=[]
for path in Path('/proc').glob('[0-9]*/status'):
    try:
        data=path.read_text(); pid=int(path.parent.name)
        ppid=int(next(line.split()[1] for line in data.splitlines() if line.startswith('PPid:')))
        if ppid==1:
            children.append({'pid':pid,'uid':next(line.split()[1:] for line in data.splitlines() if line.startswith('Uid:'))})
    except (FileNotFoundError,ProcessLookupError): pass
print(json.dumps({'health':health,'pid1_uid':os.stat('/proc/1').st_uid,'children':children}))
for instrument in ['BTC/USD','ETH/USD']:
    with urlopen('http://127.0.0.1:52731/snapshot?instrument='+instrument,timeout=5) as response: snapshot=json.load(response)
    assert snapshot['fixtureOnly'] and len(snapshot['rows'])==1000 and 'raw_fixture' not in str(snapshot)
    print(json.dumps({'instrument':instrument,'rows':len(snapshot['rows']),'version':snapshot['version']}))
probe="""import os,json
from pathlib import Path
results={}
for name in ['fixture.sqlite','fixture.sqlite-journal','checkpoint','watermark','canary-key']:
 try:
  Path('/private',name).read_bytes(); results[name]='EXPOSED'
 except PermissionError: results[name]='DENIED'
 except FileNotFoundError: results[name]='ABSENT'
assert all(value=='DENIED' for value in results.values()),results
assert 'FIXTURE_PROVIDER_KEY' not in os.environ
print(json.dumps({'public_file_probe':results,'uid':os.getuid(),'groups':os.getgroups()}))
"""
subprocess.run(['/usr/local/bin/python','-B','-c',probe],user=12002,group=12002,extra_groups=(),env={'PATH':'/usr/local/bin:/usr/bin:/bin','HOME':'/tmp'},check=True)
db=sqlite3.connect('/private/fixture.sqlite')
assert db.execute('pragma integrity_check').fetchone()[0]=='ok'
assert db.execute('select count(*) from observations').fetchone()[0]==2000
print(json.dumps({'sqlite_integrity':'ok','total_rows':2000,'watermark':Path('/private/watermark').read_text(),'fixture_checkpoint':Path('/private/checkpoint').read_text()}))
for name in ['memory.current','memory.peak','cpu.stat','pids.current','pids.max']:
    path=Path('/sys/fs/cgroup',name)
    print(json.dumps({'cgroup':name,'value':path.read_text().strip() if path.exists() else 'UNAVAILABLE'}))
PY
