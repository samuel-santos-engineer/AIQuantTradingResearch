"""Root PID1 feasibility only. Retained root trust is NOT an accepted D1 decision."""
import json
import os
from pathlib import Path
import signal
import subprocess
import time
from urllib.request import urlopen

RESEARCH_UID = 12001
PUBLIC_UID = 12002


class Backoff:
    def __init__(self): self.failures = 0
    def failure(self):
        delay = min(2 ** min(self.failures, 5), 30)
        self.failures += 1
        return delay
    def healthy(self): self.failures = 0


def main():
    if os.getpid() != 1 or os.geteuid() != 0:
        raise RuntimeError("fixture_requires_owned_container_root_pid1")
    os.umask(0o077)
    folder=Path('/private'); folder.mkdir(exist_ok=True); os.chown(folder,RESEARCH_UID,RESEARCH_UID); folder.chmod(0o700)
    for name in ['canary-key','checkpoint']:
        path=folder/name
        if not path.exists():
            path.write_text('fixture-only' if name=='canary-key' else '0',encoding='utf-8')
            os.chown(path,RESEARCH_UID,RESEARCH_UID); path.chmod(0o600)
    stopping=False
    def stop(*_):
        nonlocal stopping
        stopping=True
    signal.signal(signal.SIGTERM,stop); signal.signal(signal.SIGINT,stop)
    base={'PATH':'/opt/venv/bin:/usr/local/bin:/usr/bin:/bin','HOME':'/tmp','PYTHONDONTWRITEBYTECODE':'1','PYTHONUNBUFFERED':'1'}
    commands={
      'research':(['/usr/share/dotnet/dotnet','/stub/ResearchStub.dll'],RESEARCH_UID,dict(base,DOTNET_ROOT='/usr/share/dotnet',DOTNET_SYSTEM_GLOBALIZATION_INVARIANT='1',DOTNET_PROCESSOR_COUNT='1',FIXTURE_PROVIDER_KEY='fake-canary-never-provider')),
      'public':(['/opt/venv/bin/python','-B','-m','streamlit','run','/spike/public_app.py','--server.address=0.0.0.0','--server.port=8501','--browser.gatherUsageStats=false'],PUBLIC_UID,dict(base,STREAMLIT_BROWSER_GATHER_USAGE_STATS='false'))}
    children={}; policies={r:Backoff() for r in commands}; next_start={r:0.0 for r in commands}; failed={r:0 for r in commands}; started={}
    def terminate(role):
        child=children[role]
        # Only fixed-role children created here; never scan or signal unrelated host processes.
        if child.poll() is None:
            os.killpg(child.pid,signal.SIGTERM)
            try: child.wait(timeout=5)
            except subprocess.TimeoutExpired:
                os.killpg(child.pid,signal.SIGKILL); child.wait(timeout=5)
        print(json.dumps({'event':'reaped','role':role,'pid':child.pid,'exit':child.returncode}),flush=True)
    try:
        while not stopping:
            now=time.monotonic()
            for role,(command,uid,environment) in commands.items():
                child=children.get(role)
                if child is not None and child.poll() is not None:
                    terminate(role); del children[role]; next_start[role]=now+policies[role].failure(); child=None
                if child is None and now>=next_start[role]:
                    child=subprocess.Popen(command,user=uid,group=uid,extra_groups=(),umask=0o077,env=environment,start_new_session=True)
                    children[role]=child; started[role]=now; failed[role]=0
                    print(json.dumps({'event':'started','role':role,'pid':child.pid,'uid':uid,'fixtureOnly':True}),flush=True)
                if child is not None and now-started[role]>90:
                    endpoint='http://127.0.0.1:52731/health' if role=='research' else 'http://127.0.0.1:8501/_stcore/health'
                    try:
                        with urlopen(endpoint,timeout=1) as response: assert response.status==200
                        failed[role]=0; policies[role].healthy()
                    except Exception:
                        failed[role]+=1
                        if failed[role]>=3:
                            terminate(role); del children[role]; next_start[role]=now+policies[role].failure()
                    started[role]=now-60  # Next health check in approximately 30 seconds.
            time.sleep(0.2)
    finally:
        for role in list(children): terminate(role)
        # Reap adopted exited children without claiming containment of an escaping process tree.
        try:
            while os.waitpid(-1,os.WNOHANG)[0]: pass
        except ChildProcessError: pass

if __name__=='__main__': main()
