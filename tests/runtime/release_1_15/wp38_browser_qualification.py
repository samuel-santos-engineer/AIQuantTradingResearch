"""Bounded Chrome acceptance of the WP38 fixture demo; only loopback browser I/O."""
import argparse
import base64
import json
import os
from pathlib import Path
import subprocess
import tempfile
import time
import urllib.request
from wp07_browser_qualification import Browser, port, wait_for


def main(directory, url):
    assert url.startswith('http://127.0.0.1:')
    manifest = json.loads((directory / 'local.json').read_text())
    assert manifest['testOnly'] is True
    output = Path(tempfile.mkdtemp(prefix='aiq-wp38-browser-'))
    browser = process = None
    report = dict(testOnly=True, PUBLIC_RUNTIME_VERIFIED=False, cases={})
    try:
        debug = port()
        chrome = Path(os.environ['PROGRAMFILES']) / 'Google/Chrome/Application/chrome.exe'
        process = subprocess.Popen([str(chrome), '--headless=new', '--disable-gpu', '--no-first-run',
            '--disable-background-networking', '--disable-component-update', '--disable-sync',
            '--remote-allow-origins=http://localhost', '--user-data-dir=' + str(output / 'profile'),
            '--remote-debugging-address=127.0.0.1', '--remote-debugging-port=' + str(debug), 'about:blank'],
            stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL, creationflags=subprocess.CREATE_NO_WINDOW)
        opener = urllib.request.build_opener(urllib.request.ProxyHandler({}))
        targets = wait_for(lambda: json.load(opener.open(f'http://127.0.0.1:{debug}/json', timeout=2)))
        browser = Browser(next(t['webSocketDebuggerUrl'] for t in targets if t['type'] == 'page'))
        browser.call('Page.enable'); browser.call('Network.enable')
        browser.call('Emulation.setDeviceMetricsOverride', width=1200, height=1400, deviceScaleFactor=1, mobile=False)
        browser.call('Page.navigate', url=url)
        graph = wait_for(browser.graph)
        assert graph['instrument'] == 'BTC/USD' and len(graph['candleTimes']) == 1000
        assert graph['modebar'] and graph['loading'] == 'verified'
        assert any(t['type'] == 'candlestick' for t in graph['traces'])
        assert any(t['type'] == 'bar' for t in graph['traces'])
        assert 'SYNTHETIC TEST FIXTURE' in browser.js('document.body.innerText')
        assert 'SYNTHETIC TEST FIXTURE' in browser.graph_js('g.layout.title.text')
        report['cases']['BTC_candles_volume_fixture_disclosure'] = 'PASS'
        report['initialRange'] = graph['x']
        browser.select('Retained candles', 100)
        small = wait_for(lambda: (g if (g := browser.graph()) and g['x'] != graph['x'] else None))
        report['range100'] = small['x']
        browser.select('Retained candles', 1000)
        large = wait_for(lambda: (g if (g := browser.graph()) and g['x'] != small['x'] else None))
        report['range1000'] = large['x']
        report['cases']['retained_one_minute_range_presets'] = 'PASS'
        browser.click('ETH/USD')
        eth = wait_for(lambda: (g if (g := browser.graph()) and g['instrument'] == 'ETH/USD' else None))
        assert len(eth['candleTimes']) == 1000 and eth['modebar']
        report['cases']['ETH_switch'] = 'PASS'
        before = eth['x']; browser.drag()
        zoom = wait_for(lambda: (g if (g := browser.graph()) and g['x'] != before else None))
        report['manualRange'] = zoom['x']
        report['cases']['actual_pointer_zoom'] = 'PASS'
        browser.click('Refresh')
        wait_for(lambda: browser.graph()['x'] == zoom['x'])
        report['cases']['refresh_retains_viewport'] = 'PASS'
        text = browser.js('document.body.innerText')
        fake = (directory / 'fixture/service.key').read_text().strip()
        assert fake not in text + json.dumps(browser.graph()) + json.dumps(browser.urls)
        from urllib.parse import urlparse
        assert all(urlparse(u).hostname in ('127.0.0.1', 'localhost') for u in browser.urls if urlparse(u).scheme in ('http', 'https'))
        report['cases']['no_browser_upstream_or_key_exposure'] = 'PASS'
        browser.js('window.scrollTo(0,0)')
        time.sleep(0.5)
        (output / 'chart.png').write_bytes(base64.b64decode(browser.call('Page.captureScreenshot', format='png')['data']))
        report['result'] = 'PASS'
    finally:
        if browser:
            try: browser.call('Browser.close')
            except Exception: pass
            browser.ws.close()
        if process and process.poll() is None:
            process.terminate()
            try: process.wait(timeout=10)
            except subprocess.TimeoutExpired: process.kill(); process.wait(timeout=5)
        (output / 'result.json').write_text(json.dumps(report, indent=2) + '\n', encoding='utf-8', newline='\n')
        print(json.dumps(report), flush=True)
        print('Local browser evidence: ' + str(output), flush=True)


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--directory', type=Path, required=True)
    parser.add_argument('--url', default='http://127.0.0.1:18538')
    args = parser.parse_args()
    main(args.directory, args.url)
