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


class Browser:
    def __init__(self, url):
        self.ws = connect(url, origin="http://localhost", max_size=32_000_000)
        self.sequence = 0

    def call(self, method, **params):
        self.sequence += 1
        ticket = self.sequence
        self.ws.send(json.dumps(dict(id=ticket, method=method, params=params)))
        deadline = time.monotonic() + 15
        while time.monotonic() < deadline:
            message = json.loads(self.ws.recv(timeout=max(.1, deadline - time.monotonic())))
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
        rect = self.js("""(()=>{const w=[...document.querySelectorAll('[data-testid="stSelectbox"]')].find(e=>e.textContent.includes(%s));const e=w?.querySelector('button');if(!e)return null;e.scrollIntoView({behavior:'instant',block:'center'});const b=e.getBoundingClientRect();return {x:b.x+b.width/2,y:b.y+b.height/2}})()""" % json.dumps(label))
        assert rect, "Selectbox button unavailable: " + label
        self.call('Input.dispatchMouseEvent', type='mouseMoved', **rect)
        self.call('Input.dispatchMouseEvent', type='mousePressed', **rect, button='left', clickCount=1)
        self.call('Input.dispatchMouseEvent', type='mouseReleased', **rect, button='left', clickCount=1)
        time.sleep(.3)
        option = self.js("""(()=>{const e=[...document.querySelectorAll('[role="option"]')].find(e=>e.textContent.trim()===%s);if(!e)return null;const b=e.getBoundingClientRect();return {x:b.x+b.width/2,y:b.y+b.height/2}})()""" % json.dumps(str(value)))
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
          modebar:!!g.querySelector('.modebar'),width:g.getBoundingClientRect().width}})()""")

    def click(self, label):
        found = self.js("""(()=>{const e=[...document.querySelectorAll('button,label')].find(e=>(e.innerText.trim()===%s || e.innerText.trim().startsWith(%s)));if(!e)return false;e.click();return true})()""" % (json.dumps(label),json.dumps(label)))
        assert found, "Control not found: " + label


def main():
    root = Path(__file__).resolve().parents[3]
    output = Path(tempfile.mkdtemp(prefix="aiq-wp07-browser-"))
    profile = output / "chrome-profile"
    processes = []
    browser = None
    report = {"PUBLIC_RUNTIME_VERIFIED": False, "cases": {}, "limitations": [
        "Constant 401-row offline fixture; no provider observations.",
        "Correction/epoch/date/generation, gaps, fault recovery, resource leaks and Linux NOT RUN."]}
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
    main()
