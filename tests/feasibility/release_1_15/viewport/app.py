"""Components-v2 fixture only. No production/provider/research integration."""
import json
import uuid
from dataclasses import asdict
import streamlit as st
import plotly.graph_objects as go
from plotly.subplots import make_subplots
from fixture_snapshots import snapshot
from viewport_state import Viewport
from viewport_component import render, assets, accept_event

st.set_page_config(layout='wide')
st.title('WP01 owned viewport - FAKE DATA')
if 'viewport_session' not in st.session_state: st.session_state.viewport_session=uuid.uuid4().hex
instrument=st.radio('Instrument',['BTC/USD','ETH/USD'],horizontal=True)
if 'instruments' not in st.session_state: st.session_state.instruments={}
if instrument not in st.session_state.instruments:
    st.session_state.instruments[instrument]={'view':Viewport(),'version':1,'day':'2026-10-22','generation':'fixture-generation-a','start':0,'gap':False,'correction':False,'queries':1,'preset':300,'replay':0,'last':None,'rejected':0}
s=st.session_state.instruments[instrument];v=s['view']
cols=st.columns(4)
actions=['Refresh changed','Refresh unchanged','Correction','Remount','Retention partial','Retention expired','Date rollover','Generation reset','Return to Live','Replay stale event','Gap fixture','Reset fixture']
clicked=[cols[index%4].button(label) for index,label in enumerate(actions)]
action=next((label for label,pressed in zip(actions,clicked) if pressed),None)
if action in ('Refresh changed','Correction','Retention partial','Retention expired','Date rollover','Generation reset','Gap fixture','Reset fixture'):
    epoch=v.request(); assert v.finish(epoch);s['queries']+=1
    if action in ('Refresh changed','Correction'): s['version']+=1
    if action=='Correction': s['correction']=True
    if action=='Retention partial': s['start']=780
    if action=='Retention expired': s['start']=3000
    if action=='Date rollover': s['day']='2026-10-23' if s['day']=='2026-10-22' else '2026-10-22'
    if action=='Generation reset': s['generation']='fixture-generation-b' if s['generation']=='fixture-generation-a' else 'fixture-generation-a'
    if action=='Gap fixture': s['gap']=not s['gap'];s['version']+=1
    if action=='Reset fixture': s.update(start=0,gap=False,correction=False,day='2026-10-22',generation='fixture-generation-a');v.return_live()
    v.command(rebuild=action in ('Date rollover','Generation reset'))
if action=='Refresh unchanged':
    epoch=v.request();assert v.finish(epoch);s['queries']+=1
if action=='Remount': v.command(rebuild=True)
if action=='Return to Live': v.return_live()
if action=='Replay stale event': s['replay']+=1
preset=st.radio('Preset',[100,300,1000],index=[100,300,1000].index(s['preset']),horizontal=True,key='preset_'+instrument)
if preset!=s['preset']: s['preset']=preset;v.return_live()
overlay=st.checkbox('Fixture overlay',key='overlay_'+instrument)
native=st.checkbox('Show native comparison',value=False)
data=snapshot(instrument,s['version'],s['generation'],s['day'],s['start'],s['start']+700 if s['gap'] else None)
rows=data['rows'];identity={k:data[k] for k in ('generation','version','displayDate')}
if s['correction']: rows[-10]=dict(rows[-10],close=rows[-10]['close']+.5)
if s['day']=='2026-10-23': rows=[dict(row,**{k:row[k]*1.01 for k in ('open','high','low','close')}) for row in rows]
v.reconcile(rows[0]['time'],rows[-1]['time'])
fig=make_subplots(rows=2,cols=1,shared_xaxes=True,row_heights=[.75,.25])
x=[r['time'] for r in rows]
fig.add_trace(go.Candlestick(x=x,open=[r['open'] for r in rows],high=[r['high'] for r in rows],low=[r['low'] for r in rows],close=[r['close'] for r in rows],name='Fake OHLC'),row=1,col=1)
fig.add_trace(go.Bar(x=x,y=[r['volume'] for r in rows],name='Fake volume'),row=2,col=1)
if overlay: fig.add_trace(go.Scatter(x=x,y=[r['close'] for r in rows],name='Fixture overlay'),row=1,col=1)
fig.update_layout(height=620,uirevision=instrument+'_'+str(v.mount),xaxis_rangeslider_visible=False,title='FAKE DATA - not market observations',dragmode='zoom')
fig.update_xaxes(title_text='UTC',range=[rows[-min(preset,len(rows))]['time'],rows[-1]['time']],autorange=False)
figure=json.loads(fig.to_json())
render_token=json.dumps([instrument,identity,s['start'],s['gap'],s['correction'],preset,overlay,v.mount,v.command_epoch],sort_keys=True)
result=render({'figure':figure,'instrument':instrument,'identity':identity,'mount':v.mount,'revision':v.revision,'commandEpoch':v.command_epoch,'resetEpoch':v.reset_epoch,'session':st.session_state.viewport_session,
               'viewport':{'x':v.x,'y':v.y,'y2':v.volume_y},'renderToken':render_token,'replay':s['replay']},key='owned_'+instrument+'_'+str(v.mount))
event=result.viewport
if event is not None:
    signature=json.dumps(event,sort_keys=True)
    if signature!=s['last']:
        s['last']=signature
        try:
            changed=accept_event(event,instrument,identity,v)
            if changed: st.rerun()
        except ValueError as error: s['rejected']+=1;s['rejection']=str(error)
if v.expired: st.warning('Historical viewport expired. Choose Return to Live; no automatic reset.')
if native: st.plotly_chart(fig,key='native_'+instrument,config={'scrollZoom':True})
st.code(json.dumps({'viewportEvidence':True,'instrument':instrument,'identity':identity,'state':asdict(v),'queries':s['queries'],'rejected':s['rejected'],'rejection':s.get('rejection'),'rows':len(rows),'overlay':overlay,'bundleBytes':assets()[3]},sort_keys=True))
