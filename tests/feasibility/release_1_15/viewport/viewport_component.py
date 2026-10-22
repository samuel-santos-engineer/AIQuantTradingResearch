"""Local trusted Plotly bundle via supported raw components-v2 JavaScript."""
from functools import lru_cache
from pathlib import Path
import hashlib
import json
import math
import re
from datetime import datetime, timezone

@lru_cache(maxsize=1)
def assets():
    from plotly.offline import get_plotlyjs
    folder = Path(__file__).parent
    bundle = get_plotlyjs()
    # The existing UMD package selects its local CommonJS branch. No eval,
    # window.Plotly dependency, CDN or page-level script injection.
    source = "const module={exports:{}}; const exports=module.exports;\n" + bundle
    source += "\nconst Plotly=module.exports;\n" + (folder/'viewport_component.js').read_text(encoding='utf-8')
    return source, (folder/'viewport_component.html').read_text(encoding='utf-8'), hashlib.sha256(bundle.encode()).hexdigest(), len(bundle.encode())

@lru_cache(maxsize=1)
def renderer():
    import streamlit as st
    source, html, _, _ = assets()
    return st.components.v2.component('wp01_owned_viewport', html=html, js=source, isolate_styles=False)

def render(data, key):
    return renderer()(data=data,key=key,height=640,default={'viewport':None},on_viewport_change=lambda:None)


def validate_event(event, instrument, identity, state):
    """Fail closed before accepting any component-owned browser state."""
    try:
        encoded=json.dumps(event,allow_nan=False,ensure_ascii=True,separators=(',',':')).encode('utf-8')
    except (TypeError,ValueError,OverflowError) as error:
        raise ValueError('invalid JSON value') from error
    if len(encoded)>4096 or not isinstance(event,dict):
        raise ValueError('event bound')
    required={'kind','instrument','identity','mount','revision','commandEpoch','x','y','y2'}
    if set(event)!=required or event['kind'] not in ('manual','ack'):
        raise ValueError('fields')
    if event['instrument'] not in ('BTC/USD','ETH/USD') or event['instrument']!=instrument:
        raise ValueError('instrument')
    if event['identity']!=identity or not isinstance(event['identity'],dict):
        raise ValueError('identity')
    for key in ['mount','revision','commandEpoch']:
        if type(event[key]) is not int or not 0<=event[key]<=2**31-1:
            raise ValueError('counter')
    if event['mount']!=state.mount or event['commandEpoch']!=state.command_epoch:
        raise ValueError('stale mount/command')
    if event['kind']=='manual' and event['revision']<=state.revision:
        raise ValueError('stale revision')
    if event['kind']=='ack' and event['revision']!=state.revision:
        raise ValueError('ack revision')
    x=event['x']
    if not isinstance(x,list) or len(x)!=2:
        raise ValueError('x shape')
    dates=[]
    for value in x:
        if not isinstance(value,str) or not re.fullmatch(r'20[0-9]{2}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}(?:\.[0-9]{1,6})?Z',value):
            raise ValueError('UTC')
        try: dates.append(datetime.fromisoformat(value.replace('Z','+00:00')))
        except ValueError as error: raise ValueError('UTC') from error
    if dates[0]>=dates[1] or (dates[1]-dates[0]).total_seconds()>31*86400:
        raise ValueError('x range')
    for key in ['y','y2']:
        values=event[key]
        if not isinstance(values,list) or len(values)!=2 or any(type(v) not in (int,float) or abs(v)>1e12 or not math.isfinite(v) for v in values) or values[0]>=values[1]:
            raise ValueError('y range')
    return event


def accept_event(event, instrument, identity, state):
    event=validate_event(event,instrument,identity,state)
    if event['kind']=='ack':
        state.acknowledged=event['commandEpoch']
        return False
    state.manual(event['x'],event['y'],event['revision'],event['mount'])
    state.volume_y=tuple(event['y2'])
    return True
