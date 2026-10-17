# Release 2.0 ? Proposed phases and literal path proposals

This planning PR adds only four new Markdown files in this folder. Nothing below authorizes implementation. No issue/dependency links or milestone metadata are created. Each phase needs explicit owner authority and approved literal paths; extra path/dependency requirements stop the phase.

| Phase | Dependency | Exit gate |
|---|---|---|
| WP01 Contract freeze |Owner approves planning|Approve hypothesis, model/features, label/split/metrics/tolerance, report contract, limits and paths; no training|
| WP02 Offline evaluation |WP01 accepted|One candidate + trivial baselines; provenance/leakage/independent metric tests; bounded reproducible report and measured Linux fit budget|
| WP03 Report/image |WP02 accepted|Immutable educational panel, preserved chart; full Linux-image tests/browser acceptance|
| WP04 Development qualification |WP03 accepted + separate registry/deployment authority|Exact tested digest promoted to existing development only; browser/API/health/startup/digest evidence; rollback on failure|

Four proposed future phases, not four approved PRs or scheduled commitments. No production phase.

## Literal candidate mutation allowlists ? require later approval

### WP01 new documents only

- docs/roadmap/release-2.0/planning/EVALUATION_CONTRACT.md
- docs/roadmap/release-2.0/planning/IMPLEMENTATION_AUTHORITY.md

Freeze exact feature columns/units, target availability/purge, metric/rounding/one-class rules, report schema/size/hash policy, offline invocation, failures/UI and evidence. Explicitly select or reject the milestone-preferred model.

### WP02 bounded offline evaluator and independent tests

- python/evaluation/__init__.py
- python/evaluation/synthetic_ml_evaluation.py
- python/evaluation/test_synthetic_ml_evaluation.py
- docs/roadmap/release-2.0/planning/WP02_EVIDENCE.md

Read the existing generated1h-90D.json plus checked manifest, not private/provider data. No generator/C#/indicator/public bridge/dependency changes. Inline small independent vectors in tests; generated reports are Docker build output, not committed datasets/fixtures. Reject unknown source/root/identity; no network acquisition. If another package/import path is required, stop for amended authority.

### WP03 bounded UI/image integration

- Dockerfile
- python/presentation/ml_evaluation.py
- python/presentation/test_ml_evaluation.py
- python/presentation/realtime_financial_visualization.py
- python/presentation/test_local_synthetic_chart.py
- docs/roadmap/release-2.0/planning/WP03_EVIDENCE.md

Isolated offline report build stage, immutable final JSON asset and separately approved report digest/schema, preserving existing C# handoff and six indicators. No entrypoint/environment/runtime server/API/dependency changes. No serialized model loading or runtime fit. Invalid/missing report hides ML only; chart remains available. Any additional reader/source/test path requires new authority.

### WP04 Linux harness and evidence

- eng/qualification/release-2.0/qualify-local.sh
- eng/qualification/release-2.0/browser_acceptance.py
- docs/roadmap/release-2.0/planning/WP04_EVIDENCE.md

Linux shell/native browser only; workstation is a Docker host. Browser/CDP tooling availability is unverified; reuse approved existing tools or request a separate dependency decision. No silent browser-framework install or native compatibility work. Registry/development promotion is an operator action under separate authority, never a main-merge trigger. Deployment qualification cannot make incidental code fixes outside its allowlist.

## Dependencies, risks and sequence

Existing scikit-learn/numpy/scipy/threadpoolctl and Streamlit/Plotly pins are reused. No GPU/distributed service, new project/package or cloud ML resource. SDK build already produces C# immutable inputs; evaluate actual generated artifacts in a Linux build stage with evaluation network disabled. Final image reads results only. Explicit synthetic-candidate target is required, not an accidental default target.

Existing C# formula contracts stay untouched, but Linux regression tests remain mandatory. Scientific-stack smoke alone is not an ML result. One synthetic series cannot establish real-market skill. Target overlap, global scaling, test peeking, changed indicator seeds and per-selector retraining are explicit failure modes.

F1 headroom/full matrix duration can block WP04. Do not pay for compute, weaken coverage or change governance: return measured evidence to owner. A new production trigger blocks merge; this authority cannot disable it. Every source change requires a new accepted image/digest.

Owner reviews this planning deliverable, then grants WP01 authority and later scoped implementation phases. Separate approval is required for registry publication/development promotion. Planning merge grants no model selection, implementation, issue creation, deployment or milestone closure. Stop after planning.
