// At most two pending intents per Streamlit session, isolated by session nonce.
// sessionStorage hands intent across component-key remounts in this tab only.
const models = new WeakMap();
function utc(value) {
    let text=String(value).replace(' ', 'T');
    if (/^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$/.test(text)) text+=':00';
    if (!text.endsWith('Z')) text+='Z';
    if (!Number.isFinite(Date.parse(text))) throw new Error('invalid UTC range');
    return text; // Keep Plotly sub-millisecond precision.
}
export default async function ({data, parentElement, setStateValue}) {
    const graph=parentElement.querySelector('[data-viewport-chart]');
    const slot='research-chart-intent-'+data.session+'-'+data.instrument;
    let model=models.get(graph);
    if (!model) { model={token:null,renders:0,revision:data.revision,lastManual:null,replay:0,timer:null,ticket:0,suppress:false};models.set(graph,model); }
    // An obsolete invocation cannot redraw over a newer accepted envelope.
    if (data.mount<(model.mount||0) || data.commandEpoch<(model.epoch||0) || data.revision<(model.acceptedRevision||0)) return;
    model.mount=data.mount;model.epoch=data.commandEpoch;model.acceptedRevision=data.revision;
    const ticket=++model.ticket;
    let active=true;
    let pending=null;
    try {
        const saved=sessionStorage.getItem(slot);
        if (saved && saved.length<=4096) pending=JSON.parse(saved);
        if (pending && (!Array.isArray(pending.x) || !Array.isArray(pending.y) || !Array.isArray(pending.y2) ||
            !Number.isInteger(pending.revision) || !Number.isInteger(pending.resetEpoch))) pending=null;
        if (saved && !pending) sessionStorage.removeItem(slot);
    } catch { sessionStorage.removeItem(slot); }

    if (pending && pending.resetEpoch>data.resetEpoch) return;
    if (pending && (pending.resetEpoch!==data.resetEpoch || pending.revision<=data.revision)) {
        sessionStorage.removeItem(slot);pending=null;
    }
    model.revision=Math.max(model.revision,data.revision,pending?.revision||0);
    const current=()=>active && model.ticket===ticket && graph.isConnected;
    const ranges=()=>({x:graph.layout.xaxis.range.map(utc),y:Array.from(graph.layout.yaxis.range),y2:Array.from(graph.layout.yaxis2.range),y3:graph.layout.yaxis3?Array.from(graph.layout.yaxis3.range):null});
    const event=(kind,intent)=>({kind,instrument:data.instrument,identity:data.identity,mount:data.mount,
        revision:intent?intent.revision:data.revision,commandEpoch:data.commandEpoch,...(intent||ranges())});
    const publish=()=>{
        model.timer=null;
        if (!current() || !pending) return;
        // Rebind existing intent to the current render; never mint a gesture on restore.
        const {x,y,y2,y3,revision}=pending;
        const manual=event('manual',{x,y,y2,y3,revision});model.lastManual=manual;
        graph.dataset.lastEvent=JSON.stringify(manual);setStateValue('viewport',manual);
    };
    if (model.token!==data.renderToken || !graph.data) {
        model.suppress=true;
        const layout=structuredClone(data.figure.layout);
        const viewport=pending||data.viewport;
        if (viewport.x) { layout.xaxis.range=viewport.x;layout.xaxis.autorange=false;layout.xaxis2.range=viewport.x;layout.xaxis2.autorange=false;if (layout.xaxis3) {layout.xaxis3.range=viewport.x;layout.xaxis3.autorange=false;} }
        if (viewport.y) { layout.yaxis.range=viewport.y;layout.yaxis.autorange=false; }
        if (viewport.y2) { layout.yaxis2.range=viewport.y2;layout.yaxis2.autorange=false; }
        if (viewport.y3 && layout.yaxis3) {layout.yaxis3.range=viewport.y3;layout.yaxis3.autorange=false;}
        const start=performance.now();
        await Plotly.react(graph,data.figure.data,layout,{scrollZoom:true,responsive:true});
        if (!current()) return;
        model.token=data.renderToken;model.renders++;
        graph.dataset.renderMs=String(performance.now()-start);
        graph.dataset.loading='verified';graph.dataset.plotlyVersion=Plotly.version;
        graph.dataset.renders=String(model.renders);graph.dataset.mount=String(data.mount);
        graph.dataset.identity=JSON.stringify(data.identity);graph.dataset.instrument=data.instrument;
        model.suppress=false;
    }
    graph.removeAllListeners('plotly_relayout');
    graph.on('plotly_relayout',change=>{
        if (!current() || model.suppress || !Object.keys(change).some(k=>/^[xy]axis[23]?\.(range|autorange)/.test(k))) return;
        if (model.timer) clearTimeout(model.timer);
        pending={...ranges(),revision:++model.revision,resetEpoch:data.resetEpoch};
        sessionStorage.setItem(slot,JSON.stringify(pending));
        graph.dataset.pendingIntent=JSON.stringify(pending);
        model.timer=setTimeout(publish,250);
    });
    if (pending) {
        // Refresh before debounce delivery must flush intent, rather than ack over it.
        publish();
    } else {
        const ack=event('ack');graph.dataset.lastAck=JSON.stringify(ack);setStateValue('viewport',ack);
    }
    return ()=>{
        active=false;
        if (model.ticket===ticket) {
            if (model.timer) {clearTimeout(model.timer);model.timer=null;}
            graph.removeAllListeners('plotly_relayout');
        }
        // Pending intent is bounded and already handed off, not a live callback.
        setTimeout(()=>{if (!graph.isConnected) {Plotly.purge(graph);models.delete(graph);}},0);
    };
}
