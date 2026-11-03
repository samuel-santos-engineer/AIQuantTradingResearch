"""Local Docker-only API smoke; fixture manifests only, no host credentials."""
import argparse
import json
from pathlib import Path
import subprocess

PROBE = r'''
import os,sys,json,ssl,urllib.request,urllib.error
from decimal import Decimal
sys.path.insert(0,'/app/python/presentation')
from research_snapshot_client import ResearchSnapshotClient
base='https://wp07-research:52731';ctx=ssl.create_default_context(cafile='/ca.pem')
opener=urllib.request.build_opener(urllib.request.ProxyHandler({}),urllib.request.HTTPSHandler(context=ctx))
with opener.open(base+'/health',timeout=4) as r: assert r.status==200 and r.read(6)==b'Alive'
client=ResearchSnapshotClient(base,os.environ['AIQ_RESEARCH_API_KEY'],ca_file='/ca.pem')
counts={}
for instrument in ('BTC/USD','ETH/USD'):
 s=client.fetch(instrument);rows=s['candles'];assert len(rows)==1000
 times=[r['time'] for r in rows];assert times==sorted(set(times)) and all(t.endswith('Z') for t in times)
 for r in rows:
  o,h,l,c=map(Decimal,(r['open'],r['high'],r['low'],r['close']));assert l<=min(o,c)<=max(o,c)<=h
 counts[instrument]=len(rows)
body={'schemaVersion':'1.0','instrument':'BTC/USD','requestEpoch':'1','knownIdentity':None}
for key,payload,query,wanted in [(None,body,'',401),('wrong-test-only',body,'',401),(os.environ['AIQ_RESEARCH_API_KEY'],dict(body,instrument='DOGE/USD'),'',422),(os.environ['AIQ_RESEARCH_API_KEY'],body,'?range=-1',400)]:
 request=urllib.request.Request(base+'/v1/research/snapshot'+query,data=json.dumps(payload).encode(),headers={'Content-Type':'application/json',**({'X-API-Key':key} if key else {})})
 try:opener.open(request,timeout=4);raise AssertionError('invalid request accepted')
 except urllib.error.HTTPError as e:assert e.code==wanted,(e.code,wanted)
print(json.dumps(dict(health='200 Alive',rows=counts,orderedUniqueUtc=True,ohlc=True,negativeCases=4,PUBLIC_RUNTIME_VERIFIED=False)))
'''

if __name__ == '__main__':
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--directory', type=Path, required=True)
    args = parser.parse_args()
    manifest = json.loads((args.directory / 'local.json').read_text())
    assert manifest['testOnly'] is True
    subprocess.run(['docker', 'exec', manifest['session'] + '-ui', '/opt/venv/bin/python', '-B', '-c', PROBE], check=True, timeout=40)
