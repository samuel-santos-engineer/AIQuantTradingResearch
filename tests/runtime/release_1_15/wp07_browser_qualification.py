"""Opt-in, bounded actual-Chrome smoke against the real Streamlit/HTTPS stack.

Invoked by OwnedBrowserQualificationAgainstRealHttpsSnapshot; no provider access.
Only task-owned processes/profile are terminated. Artifacts stay outside the repo.
"""
import base64
import hashlib
import json
import os
from pathlib import Path
import socket
import subprocess
import sys
import tempfile
import time
import urllib.request
from websockets.sync.client import connect


def port():
    with socket.socket() as sock:
        sock.bind(("127.0.0.1", 0))
        return sock.getsockname()[1]


def wait_for(action, seconds=45):
    until = time.monotonic() + seconds
    last = None
    while time.monotonic() < until:
        try:
            value = action()
            if value:
                return value
        except (OSError, ValueError, KeyError) as error:
            last = error
        time.sleep(.2)
    raise AssertionError("Bounded wait expired: " + str(last))


def command(action):
    folder = Path(os.environ['AIQ_WP07_CONTROL'])
    sequence = getattr(command, 'sequence', 0) + 1
    command.sequence = sequence
    pending = folder / 'command.pending'
    pending.write_text(json.dumps(dict(sequence=sequence, action=action)), encoding='utf-8')
    pending.replace(folder / 'command.json')
    wait_for(lambda: (folder / 'ack.json').exists() and json.loads((folder / 'ack.json').read_text())['sequence'] == sequence)


class Browser:
    def __init__(self, url):
        self.ws = connect(url, origin="http://localhost", max_size=32_000_000)
        self.sequence = 0
        self.urls = []

    def call(self, method, **params):
        self.sequence += 1
        ticket = self.sequence
        self.ws.send(json.dumps(dict(id=ticket, method=method, params=params)))
        deadline = time.monotonic() + 15
        while time.monotonic() < deadline:
            message = json.loads(self.ws.recv(timeout=max(.1, deadline - time.monotonic())))
            if message.get('method') == 'Network.requestWillBeSent':
                self.urls.append(message['params']['request']['url'])
            if message.get("id") == ticket:
                if "error" in message:
                    raise AssertionError(message["error"])
                return message.get("result", {})
        raise TimeoutError("CDP command deadline: " + method)

    def js(self, expression):
        result = self.call("Runtime.evaluate", expression=expression, returnByValue=True)
        if "exceptionDetails" in result:
            raise AssertionError(result["exceptionDetails"])
        return result["result"].get("value")

    def key(self, name):
        self.call("Input.dispatchKeyEvent", type="keyDown", key=name)
        self.call("Input.dispatchKeyEvent", type="keyUp", key=name)

    def select(self, label, value):
        self.call('Page.bringToFront')
        rect = self.js("""(()=>{const w=[...document.querySelectorAll('[data-testid="stSelectbox"]')].find(e=>e.textContent.includes(%s));const e=w?.querySelector('button');if(!e)return null;e.scrollIntoView({behavior:'instant',block:'center'});const b=e.getBoundingClientRect();return {x:b.x+b.width/2,y:b.y+b.height/2}})()""" % json.dumps(label))
        time.sleep(.35)
        rect = self.js("""(()=>{const w=[...document.querySelectorAll('[data-testid="stSelectbox"]')].find(e=>e.textContent.includes(%s));const e=w?.querySelector('button[aria-label=\"Open\"]');if(!e)return null;const b=e.getBoundingClientRect();return {x:b.x+b.width/2,y:b.y+b.height/2}})()""" % json.dumps(label))
        assert rect, "Selectbox button unavailable: " + label
        self.call('Input.dispatchMouseEvent', type='mouseMoved', **rect)
        self.call('Input.dispatchMouseEvent', type='mousePressed', **rect, button='left', clickCount=1)
        self.call('Input.dispatchMouseEvent', type='mouseReleased', **rect, button='left', clickCount=1)
        time.sleep(.6)
        option = wait_for(lambda: self.js("""(()=>{const e=[...document.querySelectorAll('[role="option"]')].find(e=>e.textContent.trim()===%s);if(!e)return null;const b=e.getBoundingClientRect();return {x:b.x+b.width/2,y:b.y+b.height/2}})()""" % json.dumps(str(value))), seconds=5)
        assert option, "Option unavailable after bounded browser interaction: " + str(value)
        self.call('Input.dispatchMouseEvent', type='mousePressed', **option, button='left', clickCount=1)
        self.call('Input.dispatchMouseEvent', type='mouseReleased', **option, button='left', clickCount=1)
        time.sleep(.7)

    def graph(self):
        return self.js("""(()=>{const walk=r=>{let g=r.querySelector('[data-viewport-chart]');
          if(g&&g.data)return g;for(const e of r.querySelectorAll('*'))if(e.shadowRoot){g=walk(e.shadowRoot);if(g)return g}};
          const g=walk(document);if(!g)return null;return {instrument:g.dataset.instrument,
          traces:g.data.map(t=>({name:t.name,type:t.type,rows:t.x?.length})),
          x:g.layout.xaxis.range,y:g.layout.yaxis.range,loading:g.dataset.loading,
          identity:JSON.parse(g.dataset.identity),renders:g.dataset.renders,renderMs:g.dataset.renderMs,
          candleTimes:g.data[0].x,closes:g.data[0].close,
          overlays:g.data.filter(t=>t.type==='scatter').map(t=>({name:t.name,y:t.y,connectgaps:t.connectgaps})),
          shapes:g.layout.shapes||[],listeners:g._ev?._events?.plotly_relayout?.length||null,
          modebar:!!g.querySelector('.modebar'),width:g.getBoundingClientRect().width}})()""")

    def graph_js(self, expression):
        return self.js("""(()=>{const walk=r=>{let g=r.querySelector('[data-viewport-chart]');if(g&&g.data)return g;for(const e of r.querySelectorAll('*'))if(e.shadowRoot){g=walk(e.shadowRoot);if(g)return g}};const g=walk(document);return """ + expression + "})()")

    def drag(self):
        rect = self.graph_js("(()=>{g.scrollIntoView({behavior:'instant',block:'center'});const b=g.querySelector('.nsewdrag').getBoundingClientRect();return {x:b.x,y:b.y,w:b.width,h:b.height}})()")
        x, y = rect['x'] + rect['w']*.4, rect['y'] + rect['h']*.4
        self.call('Input.dispatchMouseEvent', type='mouseMoved', x=x, y=y)
        self.call('Input.dispatchMouseEvent', type='mousePressed', x=x, y=y, button='left', clickCount=1)
        self.call('Input.dispatchMouseEvent', type='mouseMoved', x=x+rect['w']*.15, y=y+rect['h']*.15, button='left', buttons=1)
        self.call('Input.dispatchMouseEvent', type='mouseReleased', x=x+rect['w']*.15, y=y+rect['h']*.15, button='left', clickCount=1)
        self.graph_js("(()=>{window.wp07OldIntent=g.dataset.pendingIntent;window.wp07Slot=Object.keys(sessionStorage).find(k=>k.startsWith('research-chart-intent-')&&k.endsWith('ETH/USD'));return true})()")

    def click(self, label):
        self.call('Page.bringToFront')
        rect = self.js("""(()=>{const es=[...document.querySelectorAll('button,label')];const e=es.find(e=>e.innerText.trim()===%s)||es.find(e=>e.innerText.trim().startsWith(%s));if(!e)return null;e.scrollIntoView({behavior:'instant',block:'center'});const b=e.getBoundingClientRect();return {x:b.x+b.width/2,y:b.y+b.height/2}})()""" % (json.dumps(label),json.dumps(label)))
        assert rect, "Control not found: " + label
        self.call('Input.dispatchMouseEvent', type='mouseMoved', **rect)
        self.call('Input.dispatchMouseEvent', type='mousePressed', **rect, button='left', clickCount=1)
        self.call('Input.dispatchMouseEvent', type='mouseReleased', **rect, button='left', clickCount=1)


def external_main():
    """Actual container UI only; never start a Windows replacement UI/backend."""
    url = os.environ['AIQ_WP07_EXTERNAL_UI_URL']
    assert url.startswith('http://127.0.0.1:')
    output = Path(tempfile.mkdtemp(prefix='aiq-wp07-linux-browser-'))
    result = dict(PUBLIC_RUNTIME_VERIFIED=False, mode='actual-linux-container-ui', cases={})
    process = browser = None
    def lifecycle(action):
        folder = Path(os.environ['AIQ_WP07_CONTAINER_CONTROL'])
        pending = folder / 'browser-command.pending'
        pending.write_text(json.dumps(dict(action=action)), encoding='utf-8')
        pending.replace(folder / 'browser-command.json')
        wait_for(lambda: (folder / 'browser-ack.json').exists() and json.loads((folder / 'browser-ack.json').read_text())['action'] == action)
    try:
        chrome_port = port()
        chrome = Path(os.environ.get('PROGRAMFILES', 'C:/Program Files')) / 'Google/Chrome/Application/chrome.exe'
        process = subprocess.Popen([str(chrome), '--headless=new', '--disable-gpu', '--no-first-run',
            '--disable-background-networking', '--disable-component-update', '--disable-sync',
            '--remote-allow-origins=http://localhost', '--user-data-dir='+str(output/'profile'),
            '--remote-debugging-address=127.0.0.1', '--remote-debugging-port='+str(chrome_port), 'about:blank'],
            stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL, creationflags=subprocess.CREATE_NO_WINDOW)
        opener = urllib.request.build_opener(urllib.request.ProxyHandler({}))
        targets = wait_for(lambda: json.load(opener.open(f'http://127.0.0.1:{chrome_port}/json', timeout=2)))
        browser = Browser(next(t['webSocketDebuggerUrl'] for t in targets if t['type']=='page'))
        browser.call('Page.enable'); browser.call('Network.enable')
        result['browser'] = browser.call('Browser.getVersion')['product']
        browser.call('Page.navigate', url=url)
        graph = wait_for(browser.graph)
        assert graph['instrument']=='BTC/USD' and len(graph['candleTimes'])==1000
        assert any('SMA20' in (t['name'] or '') for t in graph['traces'])
        assert graph['loading']=='verified' and graph['modebar']
        result['cases']['BTC_actual_component'] = 'PASS'
        browser.select('Retained candles', 1000)
        assert len(browser.graph()['candleTimes'])==1000 and browser.graph()['shapes']
        result['cases']['bounded_retention_and_gap'] = 'PASS'
        browser.click('Technical Analysis'); time.sleep(.5); browser.select('Research panel', 'RSI14'); browser.key('Escape')
        wait_for(lambda: any('RSI14' in (t['name'] or '') for t in browser.graph()['traces']))
        result['cases']['supplied_RSI'] = 'PASS'
        browser.click('ETH/USD')
        eth = wait_for(lambda: (g if (g:=browser.graph()) and g['instrument']=='ETH/USD' else None))
        assert len(eth['candleTimes'])==1000
        result['cases']['ETH_actual_component'] = 'PASS'
        lifecycle('stop')
        browser.click('Refresh')
        wait_for(lambda: 'Disconnected - showing last-known' in browser.js('document.body.innerText'))
        assert browser.graph()['closes']==eth['closes']
        result['cases']['research_outage_last_good'] = 'PASS'
        lifecycle('start'); time.sleep(2); browser.click('Refresh')
        wait_for(lambda: 'Connected |' in browser.js('document.body.innerText'))
        assert browser.graph()['closes']==eth['closes']
        result['cases']['restart_reconnect'] = 'PASS'
        text = browser.js('document.body.innerText')
        assert 'Transformed educational values' in text and os.environ['AIQ_RESEARCH_API_KEY'] not in text+json.dumps(browser.graph())
        from urllib.parse import urlparse
        unexpected = [u for u in browser.urls if urlparse(u).scheme in ('http','https') and urlparse(u).hostname not in ('127.0.0.1','localhost')]
        assert not unexpected, unexpected
        result['cases']['no_CDN_or_browser_upstream'] = 'PASS'
        result['plotlyJs'] = browser.graph_js('g.dataset.plotlyVersion')
        browser.graph_js("(()=>{g.scrollIntoView({behavior:'instant',block:'center'});return true})()")
        raw = base64.b64decode(browser.call('Page.captureScreenshot', format='png')['data'])
        (output/'chart.png').write_bytes(raw); result['screenshotSha256']=hashlib.sha256(raw).hexdigest()
        result['result']='PASS'
    except Exception as error:
        result['result']='FAIL';result['error']=str(error)
        raise
    finally:
        if browser:
            try: browser.call('Browser.close')
            except Exception: pass
            browser.ws.close()
        if process and process.poll() is None:
            process.terminate()
            try: process.wait(timeout=10)
            except subprocess.TimeoutExpired: process.kill();process.wait(timeout=5)
        (output/'result.json').write_text(json.dumps(result,indent=2)+'\n',encoding='utf-8',newline='\n')
        print(json.dumps(result),flush=True)


def main():
    root = Path(__file__).resolve().parents[3]
    output = Path(tempfile.mkdtemp(prefix="aiq-wp07-browser-"))
    profile = output / "chrome-profile"
    processes = []
    browser = None
    report = {"PUBLIC_RUNTIME_VERIFIED": False, "cases": {}, "limitations": [
        "Initial 401-row deterministic fixtures with committed correction/gap/generation scenarios; no provider observations.",
        "Exhaustive stale-event races and Linux NOT RUN. Resource counters are bounded observations only."]}
    try:
        ui_port, chrome_port = port(), port()
        env = dict(os.environ, AIQ_RESEARCH_CHART="1", STREAMLIT_BROWSER_GATHER_USAGE_STATS="false")
        log = (output / "streamlit.log").open("wb")
        try:
            processes.append(subprocess.Popen([sys.executable, "-B", "-m", "streamlit", "run",
                str(root / "python/presentation/realtime_financial_visualization.py"),
                "--server.address=127.0.0.1", "--server.port=" + str(ui_port),
                "--server.headless=true", "--browser.gatherUsageStats=false"],
                cwd=root, env=env, stdout=log, stderr=log, creationflags=subprocess.CREATE_NO_WINDOW))
        finally:
            log.close()
        opener = urllib.request.build_opener(urllib.request.ProxyHandler({}))
        wait_for(lambda: opener.open(f"http://127.0.0.1:{ui_port}/_stcore/health", timeout=2).status == 200)
        chrome = Path(os.environ.get("PROGRAMFILES", "C:/Program Files")) / "Google/Chrome/Application/chrome.exe"
        processes.append(subprocess.Popen([str(chrome), "--headless=new", "--disable-gpu",
            "--no-first-run", "--disable-background-networking", "--disable-component-update",
            "--disable-sync", "--remote-allow-origins=http://localhost",
            "--user-data-dir=" + str(profile), "--remote-debugging-address=127.0.0.1",
            "--remote-debugging-port=" + str(chrome_port), "about:blank"],
            stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL, creationflags=subprocess.CREATE_NO_WINDOW))
        targets = wait_for(lambda: json.load(opener.open(f"http://127.0.0.1:{chrome_port}/json", timeout=2)))
        target = next(t for t in targets if t['type'] == 'page')
        browser = Browser(target['webSocketDebuggerUrl'])
        report['browser'] = browser.call('Browser.getVersion')['product']
        browser.call('Page.enable')
        browser.call('Page.navigate', url=f"http://127.0.0.1:{ui_port}")
        graph = wait_for(browser.graph)
        assert graph['instrument'] == 'BTC/USD', graph
        assert any(t['type'] == 'candlestick' for t in graph['traces']), graph
        assert any(t['type'] == 'bar' for t in graph['traces']), graph
        assert graph['modebar'], graph
        report['cases']['BTC_initial'] = dict(result='PASS', graph=graph)
        assert 'Transformed educational values' in browser.js('document.body.innerText')
        report['cases']['disclosure'] = 'PASS'
        browser.click('Refresh')
        time.sleep(2)
        refreshed = browser.graph()
        assert refreshed['x'] == graph['x'] and refreshed['instrument'] == 'BTC/USD'
        report['cases']['refresh_unchanged'] = dict(result='PASS', graph=refreshed)
        browser.click('ETH/USD')
        eth = wait_for(lambda: (g if (g := browser.graph()) and g['instrument'] == 'ETH/USD' else None))
        report['cases']['ETH_switch'] = dict(result='PASS', graph=eth)
        time.sleep(2)
        for count in (100, 1000, 300):
            browser.select('Retained candles', count)
            current = browser.graph()
            from datetime import datetime
            width = (datetime.fromisoformat(current['x'][1]) - datetime.fromisoformat(current['x'][0])).total_seconds() / 60
            assert width == min(count, 401), current
            report['cases']['preset_' + str(count)] = dict(result='PASS', graph=current)
        browser.click('Technical Analysis')
        for panel, expected in [('RSI14', 'RSI14'), ('MACD', 'MACD')]:
            browser.select('Research panel', panel)
            current = wait_for(lambda: (g if (g := browser.graph()) and any(expected in (t['name'] or '') for t in g['traces']) else None))
            report['cases']['panel_' + panel] = dict(result='PASS', graph=current)
        browser.select('Research panel', 'None')
        browser.js("document.querySelector('input[aria-label=\"Historical transition markers\"]').click()")
        transitions = wait_for(lambda: (g if (g := browser.graph()) and any(t['name'] == 'Trend transition' and t['rows'] > 0 for t in g['traces']) else None))
        report['cases']['nonempty_transition_marker'] = dict(result='PASS', graph=transitions)
        browser.js("document.querySelector('input[aria-label=\"Historical transition markers\"]').click()")
        time.sleep(.5)
        browser.key('Escape')
        # Genuine pointer drag through the Plotly plotting rectangle, not a relayout injection.
        rect = browser.js("""(()=>{const walk=r=>{let g=r.querySelector('[data-viewport-chart]');if(g)return g;for(const e of r.querySelectorAll('*'))if(e.shadowRoot){g=walk(e.shadowRoot);if(g)return g}};const g=walk(document);g.scrollIntoView({behavior:'instant',block:'center'});const b=g.querySelector('.nsewdrag').getBoundingClientRect();return {x:b.x,y:b.y,w:b.width,h:b.height}})()""")
        before = browser.graph()
        x1, y1 = rect['x'] + rect['w'] * .25, rect['y'] + rect['h'] * .25
        x2, y2 = rect['x'] + rect['w'] * .75, rect['y'] + rect['h'] * .75
        browser.call('Input.dispatchMouseEvent', type='mouseMoved', x=x1, y=y1)
        browser.call('Input.dispatchMouseEvent', type='mousePressed', x=x1, y=y1, button='left', clickCount=1)
        browser.call('Input.dispatchMouseEvent', type='mouseMoved', x=x2, y=y2, button='left', buttons=1)
        browser.call('Input.dispatchMouseEvent', type='mouseReleased', x=x2, y=y2, button='left', clickCount=1)
        zoomed = wait_for(lambda: (g if (g := browser.graph()) and g['x'] != before['x'] else None))
        time.sleep(1)
        browser.click('Refresh')
        time.sleep(1)
        retained = browser.graph()
        assert retained['x'] == zoomed['x'] and retained['y'] == zoomed['y'], (zoomed, retained)
        report['cases']['pointer_zoom_refresh_retention'] = dict(result='PASS', before=before, zoomed=zoomed, after=retained)
        browser.click('Return to Live')
        live = wait_for(lambda: (g if (g := browser.graph()) and g['x'] != zoomed['x'] else None))
        report['cases']['return_live'] = dict(result='PASS', graph=live)
        # Actual modebar Pan tool followed by a genuine pointer drag.
        pan = browser.graph_js("(()=>{const e=g.querySelector('[data-title=\"Pan\"]');if(!e)return false;e.click();return true})()")
        assert pan, 'Pan modebar control missing'
        before_pan = browser.graph()
        browser.drag()
        panned = wait_for(lambda: (g if (g := browser.graph()) and g['x'] != before_pan['x'] else None))
        time.sleep(1)
        browser.click('Refresh'); time.sleep(1)
        assert browser.graph()['x'] == panned['x'] and browser.graph()['y'] == panned['y']
        report['cases']['pointer_pan_refresh'] = dict(result='PASS', before=before_pan, after=panned)
        browser.click('Return to Live'); time.sleep(1)
        assert browser.graph()['x'] == live['x'] and browser.graph()['y'] == live['y']

        for action, identity_key in [('correction', 'version'), ('day', 'displayDate'), ('epoch', 'transformEpoch')]:
            old = browser.graph()
            command(action)
            browser.click('Refresh')
            changed = wait_for(lambda: (g if (g := browser.graph()) and g['identity'][identity_key] != old['identity'][identity_key] else None))
            assert changed['closes'] != old['closes'], action
            assert changed['x'] == old['x'], (action, 'live UTC window changed unexpectedly')
            report['cases'][action] = dict(result='PASS', before=old, after=changed)

        command('gap'); browser.click('Refresh')
        gap = wait_for(lambda: (g if (g := browser.graph()) and len(g['candleTimes']) == 402 else None))
        assert gap['candleTimes'][-1] == '2026-10-01T06:45:00Z'
        assert gap['candleTimes'][-2] == '2026-10-01T06:40:00Z'
        assert len(gap['shapes']) > 0
        assert any(t['y'][-1] is None and t['connectgaps'] is False for t in gap['overlays'])
        browser.click('Technical Analysis'); time.sleep(.3)
        checked = browser.js("""(()=>{const w=[...document.querySelectorAll('[data-testid="stCheckbox"]')].find(e=>e.innerText.includes('Historical transition markers'));const input=w?.querySelector('input');if(!input)return null;input.click();return input.checked})()""")
        wait_for(lambda: browser.js("""(()=>{const w=[...document.querySelectorAll('[data-testid="stCheckbox"]')].find(e=>e.innerText.includes('Historical transition markers'));return w?.querySelector('label')?.dataset.selected==='true'})()"""))
        time.sleep(1)
        markers = wait_for(lambda: (g if (g := browser.graph()) and any('transition' in str(t['name']).lower() or 'segment' in str(t['name']).lower() for t in g['traces']) else None))
        report['cases']['gaps_nulls_markers'] = dict(result='PASS', graph=markers)
        browser.key('Escape')

        old = browser.graph()
        command('generation'); browser.click('Refresh')
        replaced = wait_for(lambda: (g if (g := browser.graph()) and g['identity']['generation'] != old['identity']['generation'] else None))
        assert len(replaced['candleTimes']) == 401 and replaced['closes'] != old['closes']
        report['cases']['generation'] = dict(result='PASS', before=old, after=replaced)

        for action in ('offline', 'unauthorized', 'malformed'):
            old = browser.graph()
            command(action); browser.click('Refresh')
            wait_for(lambda: 'Disconnected - showing last-known' in browser.js('document.body.innerText'))
            assert browser.graph()['closes'] == old['closes']
            report['cases'][action] = dict(result='PASS', retainedIdentity=old['identity'])
            command('online'); browser.click('Refresh')
            wait_for(lambda: 'Connected |' in browser.js('document.body.innerText'))
            report['cases'][action + '_reconnect'] = 'PASS'

        # A new browser context has a separate Streamlit session and sessionStorage.
        browser_control = Browser(json.load(opener.open(f"http://127.0.0.1:{chrome_port}/json/version", timeout=2))['webSocketDebuggerUrl'])
        context = browser_control.call('Target.createBrowserContext')['browserContextId']
        target2 = browser_control.call('Target.createTarget', url=f"http://127.0.0.1:{ui_port}", browserContextId=context)['targetId']
        target_list = json.load(opener.open(f"http://127.0.0.1:{chrome_port}/json", timeout=2))
        second = Browser(next(t['webSocketDebuggerUrl'] for t in target_list if t['id'] == target2))
        try:
            fresh = wait_for(second.graph)
            assert fresh['instrument'] == 'BTC/USD'
            first = browser.graph()
            second.select('Retained candles', 100)
            assert browser.graph()['x'] == first['x']
            second.click('Technical Analysis'); time.sleep(1); second.select('Research panel', 'RSI14'); second.key('Escape')
            wait_for(lambda: any('RSI14' in (t['name'] or '') for t in second.graph()['traces']))
            assert not any('RSI14' in (t['name'] or '') for t in browser.graph()['traces'])
            browser.click('BTC/USD'); time.sleep(1); browser.click('ETH/USD'); time.sleep(1)
            assert second.graph()['instrument'] == 'BTC/USD'
            assert browser.graph()['x'] == first['x']
            assert any('RSI14' in (t['name'] or '') for t in second.graph()['traces'])
            report['cases']['two_browser_sessions'] = dict(result='PASS', first=first, second=second.graph())
        finally:
            second.ws.close()
            browser_control.call('Target.disposeBrowserContext', browserContextId=context)
            browser_control.ws.close()

        browser.call('HeapProfiler.collectGarbage')
        memory = []
        for repeat in range(8):
            browser.click('Refresh'); time.sleep(.5)
            browser.call('HeapProfiler.collectGarbage')
            sample = browser.call('Memory.getDOMCounters')
            sample['heap'] = browser.call('Runtime.getHeapUsage')
            sample['renderMs'] = browser.graph()['renderMs']
            memory.append(sample)
        report['cases']['bounded_refresh_resources'] = dict(result='OBSERVED', samples=memory,
            note='Eight refreshes; DOM counters are bounded observations, not proof of absence of leaks.')
        # Replay a saved intent from before an explicit reset, then force a panel remount.
        # This tests the actual component's stale-intent gate, not arbitrary forged callbacks.
        browser.click('Return to Live'); time.sleep(1)
        live_before_remount = browser.graph()
        replayed = browser.js("""(()=>{const k=window.wp07Slot;if(!k||!window.wp07OldIntent)return false;sessionStorage.setItem(k,window.wp07OldIntent);return true})()""")
        if replayed:
            browser.click('Technical Analysis')
            for panel in ('RSI14', 'MACD', 'None'):
                browser.select('Research panel', panel)
                time.sleep(.4)
                assert browser.graph()['x'] == live_before_remount['x']
            browser.key('Escape')
            report['cases']['stale_intent_remount'] = 'PASS: old intent did not overwrite live x range through three panel remounts'
        else:
            report['cases']['stale_intent_remount'] = 'NOT RUN: no saved manual intent remained for replay'



        # Observe the real 60-second Streamlit fragment, without shortening its timer.
        browser.js("document.querySelector('input[aria-label=\"Refresh every 60 seconds while this page is open\"]').click()")
        wait_for(lambda: browser.js("document.querySelector('input[aria-label=\"Refresh every 60 seconds while this page is open\"]').closest('label').dataset.selected==='true'"))
        time.sleep(1)
        sync_expression = "document.body.innerText.split(String.fromCharCode(10)).find(s=>s.startsWith('Transformation date:'))"
        sync = browser.js(sync_expression)
        assert sync and 'Last successful sync:' in sync
        began = time.monotonic()
        wait_for(lambda: browser.js(sync_expression) != sync, seconds=135)
        elapsed = time.monotonic()-began
        report['cases']['real_auto_refresh'] = dict(result='PASS' if elapsed <= 75 else 'FAIL', elapsedSeconds=elapsed,
            note='Real enabled 60-second fragment; a second-tick delay is reported as FAIL, not waived.')
        assert elapsed <= 75, 'Real 60-second fragment missed its first expected tick'
        browser.js("document.querySelector('input[aria-label=\"Refresh every 60 seconds while this page is open\"]').click()")
        visible = browser.js('document.body.innerText') + json.dumps(browser.graph())
        assert os.environ['AIQ_RESEARCH_API_KEY'] not in visible
        report['cases']['visible_secret_negative'] = 'PASS'

        browser.click('BTC/USD')
        wait_for(lambda: (g if (g := browser.graph()) and g['instrument'] == 'BTC/USD' else None))
        browser.call('Emulation.setDeviceMetricsOverride', width=390, height=844, deviceScaleFactor=1, mobile=False)
        time.sleep(1)
        narrow = browser.graph()
        assert narrow['width'] <= 390, narrow
        report['cases']['narrow_layout'] = dict(result='PASS', graph=narrow)
        shot = browser.call('Page.captureScreenshot', format='png')
        raw = base64.b64decode(shot['data'])
        (output / 'narrow-BTC.png').write_bytes(raw)
        report['screenshot_sha256'] = hashlib.sha256(raw).hexdigest()
        report['result'] = 'PARTIAL'
    except Exception as error:
        report['result'] = 'FAIL'
        report['error'] = str(error)
        if browser:
            report['failure_controls'] = browser.js("[...document.querySelectorAll('[role=option],[role=listbox],input[role=combobox]')].map(e=>e.outerHTML.slice(0,1800))")
            report['failure_graph'] = browser.graph()
            report['failure_visible_text'] = browser.js('document.body.innerText')
        raise
    finally:
        if browser:
            try:
                browser.call('Browser.close')
            except Exception:
                pass  # Browser.close may close the transport before replying.
            browser.ws.close()
        for process in reversed(processes):
            if process.poll() is None:
                # The fresh Chrome profile owns its browser tree; Browser.close is unnecessary
                # for user browsers, which this harness never opens or attaches to.
                process.terminate()
                try:
                    process.wait(timeout=10)
                except subprocess.TimeoutExpired:
                    process.kill()
                    process.wait(timeout=5)
        (output / 'result.json').write_text(json.dumps(report, indent=2) + "\n", encoding='utf-8', newline='\n')
        print('Owned local evidence:', output)
        print(json.dumps(report, indent=2))


if __name__ == '__main__':
    if os.environ.get('AIQ_WP07_EXTERNAL_UI_URL'):
        external_main()
    else:
        main()
