const models = new WeakMap();
function utc(value) {
    let text=String(value).replace(' ', 'T');
    if (/^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$/.test(text)) text+=':00';
    if (!text.endsWith('Z')) text+='Z';
    if (!Number.isFinite(Date.parse(text))) throw new Error('invalid UTC range');
    return text; // Preserve Plotly sub-millisecond digits; Date.toISOString would truncate.
}
export default async function ({data, parentElement, setStateValue}) {
    const graph=parentElement.querySelector('[data-viewport-chart]');
    let model=models.get(graph);
    if (!model) { model={token:null, renders:0, revision:data.revision, lastManual:null, replay:0, timer:null, suppress:false}; models.set(graph,model); }
    model.revision=Math.max(model.revision,data.revision);
    const describe=kind=>({kind,instrument:data.instrument,identity:data.identity,mount:data.mount,
        revision:kind==='manual'?++model.revision:data.revision,commandEpoch:data.commandEpoch,
        x:graph.layout.xaxis.range.map(utc),y:Array.from(graph.layout.yaxis.range),y2:Array.from(graph.layout.yaxis2.range)});
    const publish=()=>{
        const event=describe('manual'); model.lastManual=event;
        graph.dataset.lastEvent=JSON.stringify(event); setStateValue('viewport',event);
    };
    if (model.token!==data.renderToken || !graph.data) {
        model.suppress=true;
        if (model.timer) clearTimeout(model.timer);
        const layout=structuredClone(data.figure.layout);
        if (data.viewport.x) { layout.xaxis.range=data.viewport.x; layout.xaxis.autorange=false; layout.xaxis2.range=data.viewport.x; layout.xaxis2.autorange=false; }
        if (data.viewport.y) { layout.yaxis.range=data.viewport.y; layout.yaxis.autorange=false; }
        if (data.viewport.y2) { layout.yaxis2.range=data.viewport.y2; layout.yaxis2.autorange=false; }
        const start=performance.now();
        await Plotly.react(graph,data.figure.data,layout,{scrollZoom:true,responsive:true});
        model.token=data.renderToken; model.renders++;
        graph.dataset.renderMs=String(performance.now()-start);
        graph.dataset.loading='verified'; graph.dataset.plotlyVersion=Plotly.version;
        graph.dataset.renders=String(model.renders); graph.dataset.mount=String(data.mount);
        graph.dataset.identity=JSON.stringify(data.identity); graph.dataset.instrument=data.instrument;
        model.suppress=false;
        const ack=describe('ack'); graph.dataset.lastAck=JSON.stringify(ack); setStateValue('viewport',ack);
    }
    graph.removeAllListeners('plotly_relayout');
    graph.on('plotly_relayout',()=>{
        if (model.suppress) return;
        if (model.timer) clearTimeout(model.timer);
        model.timer=setTimeout(publish,250);
    });
    // Explicit negative fixture: resend an old revision through the real component channel.
    if (data.replay>model.replay && model.lastManual) {
        const old=structuredClone(model.lastManual);
        old.x[0]=new Date(Date.parse(old.x[0])+1).toISOString();
        setStateValue('viewport',old);
    }
    model.replay=data.replay;
    return ()=>{
        if (model.timer) clearTimeout(model.timer);
        graph.removeAllListeners('plotly_relayout');
        // Supported cleanup also runs during updates. Purge only after actual
        // detachment so Plotly can release its own resize handler/resources.
        setTimeout(()=>{
            if (!graph.isConnected) { Plotly.purge(graph); models.delete(graph); }
        },0);
    };
}
