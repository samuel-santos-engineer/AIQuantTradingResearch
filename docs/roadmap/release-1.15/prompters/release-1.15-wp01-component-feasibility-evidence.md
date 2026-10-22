# Release 1.15 WP01 S1 - Components-v2 feasibility evidence

Date: 2026-10-22 (America/Sao_Paulo). Issue #367. Existing draft PR #379; pre-change head `7edfbee3124fa758c379677a7f760f8d2524de05`. Clean main remains at planning merge `455b80d0e50c17791653457a4ebd3001d3629a6e`.

**Verdict: scoped component feasibility SUPPORTED by the tested local browser matrix; OWNER REVIEW REQUIRED.** This is not WP01 acceptance, a production component, a contract freeze or permission to begin WP02. The prior native/S2 reports remain historical observations; this report adds evidence rather than rewriting their earlier incomplete results. `PUBLIC_RUNTIME_VERIFIED=False` remains unchanged.

## Authority and exact scope

Owner explicitly authorized the nine paths in the [component proposal](release-1.15-wp01-supported-viewport-component-proposal.md). The proposal's earlier NOT AUTHORIZED label records its prior status; the subsequent owner instruction supplies this experiment's authority. Only those nine repository paths change: eight source/test/Dockerfile paths hashed below, plus this evidence document. Four are modifications and five are additions relative to the prior PR head. No production application, fixture generator, S2 source/evidence, dependency pin, project file, README or existing governance was edited.

All repository and temporary scripts/text are UTF-8/LF. Windows orchestration used Windows PowerShell 5.1 APIs; process-scoped RemoteSigned enabled only the locally authored temporary helper. No machine-wide execution policy changed. No dependency, browser, package manifest, framework, CDN or external asset was installed/introduced. No new public API, provider call, registry push, Azure operation or deployment occurred.

## Loading proof before implementation

First inspected the installed Streamlit 1.61.1 component signature and Plotly Python 7.1.0 assets. The existing Plotly bundle is 4,815,814 bytes and reports Plotly.js 4.1.1. The first minimal component successfully rendered in actual Chrome before event implementation proceeded. No unlisted component path or dependency was required.

The loader reads `get_plotlyjs()` from the installed package, places that trusted existing UMD source in a local CommonJS lexical container (`module.exports`), and supplies the resulting raw module plus renderer through the supported components-v2 `js` argument. Fixed HTML and fixture data are separate. It neither relies on an existing global Plotly instance nor uses eval, arbitrary page-script injection, a CDN, bundler or native Streamlit chart internals. The bundle is assembled in memory once per process, not copied into a new repository asset.

The default shadow-root experiment loaded Plotly but left its existing styles outside the shadow boundary. Visual inspection caught the malformed modebar. The final implementation uses the documented `isolate_styles=False` option; the renderer still queries only its supplied parent and owned chart. Trusted component code is not a security sandbox. No application-page selector or DOM patch is used by the renderer. Its cleanup cancels timers/listeners and purges Plotly after actual owned-element detachment, preserving the graph on ordinary data updates.

Primary API references: [components-v2](https://docs.streamlit.io/develop/api-reference/custom-components/st.components.v2.component), [Plotly events](https://plotly.com/javascript/plotlyjs-events/), [Chrome input protocol](https://chromedevtools.github.io/devtools-protocol/tot/Input/). Online Streamlit docs currently describe 1.66.0; the installed 1.61.1 implementation and real browser results, not a presumed upgrade, establish this experiment's behavior.

## Environment and reproducible commands

Linux/amd64 Streamlit server in one label-owned Docker container; Docker 29.7.2. Existing local base ID `sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed`; version assertions retain Streamlit 1.61.1/Plotly 7.1.0. Final tested local image ID `sha256:1b62615dd74f070b898ee0cd1ac15521b90c347148db8b9e5fcbbbac2cb692b5`. No image was published. Build RUN networking disabled; registry metadata resolution is not an air-gap claim.

```powershell
python -B -m unittest discover -s tests/feasibility/release_1_15/viewport -p 'test_viewport*.py' -v
docker build --pull=false --network=none -f tests/feasibility/release_1_15/viewport/Dockerfile -t aiq-r115-wp01-component:local .
```

The owned container ran with `--read-only --cpus=1 --memory=512m --pids-limit=64 --tmpfs /tmp:rw,nosuid,nodev,size=33554432` and `-p 127.0.0.1:58933:8501`; no credential/private mount. The advertised browser address is also loopback and usage telemetry is disabled. This is the local screening profile, not asserted Azure F1 capacity.

Actual installed Chrome `155.0.8059.86` ran headless in a separate owned temporary profile, remote debugging bound to loopback. Built-in .NET ClientWebSocket in PowerShell 5.1 spoke the supported DevTools Protocol; no Playwright, Selenium, npm or browser installation. A second browser context provided separate storage/page/Streamlit websocket state. Windows browser client evidence is not a Windows application-runtime certification; the tested server image was Linux.

Mouse tests used `Input.dispatchMouseEvent`: press at plot fractions (0.20,0.20), six held-button moves, release at (0.75,0.75); pan selected the real modebar Pan control and dragged (0.50,0.30) to (0.57,0.35). No direct Plotly range-setting call simulated those gestures. Buttons used real input events. Checkbox/radio changes used DOM.focus plus real Space keyDown/keyUp after initial label clicks failed to change state. The failed clicks were not accepted as passes. Read-only browser DOM inspection located controls, read chart layout/data and captured evidence; it is test orchestration, not runtime DOM modification.

The final image was rebuilt after correcting sub-millisecond UTC preservation and detached-element cleanup. The complete 26-case matrix below was rerun against that final image. Each case set was bounded below 15 minutes. Earlier exploratory outputs, a temporary PowerShell JSON-array serialization error and a page-level browser-context request rejection are not substituted for final evidence; the driver used correct browser-level context creation and final structured JSON checks.

## Offline tests

Focused command above: **32 tests passed, exit 0** (0.012 seconds on final run): original nine state tests preserved; six command/request tests; seventeen independent event tests. Literal expected vectors are separately authored in the test file, not copied from browser/calculator outputs.

Negatives include missing/extra fields, wrong instrument/generation/version/date, bool/float/negative/overflow counters, stale revision/mount/command, wrong acknowledgment revision, malformed/local-time/nonexistent UTC dates, inverted/excessive x span, malformed arrays, NaN/infinity/inverted/equal/huge y values, invalid JSON values and serialized events over 4,096 bytes. Rejection leaves the prior state unchanged. The 4,096-byte check is on the parsed event's serialization, not production transport-size enforcement. One-in-flight/one-pending coalescing and stale request completion are pure-model tests; no real API scheduler is implemented by this fixture.

## Actual viewport evidence

Canonical UTC display below preserves all Plotly fractional digits (padding to six places only); no Date.toISOString truncation is used for accepted ranges. X preservation assertions compared UTC ticks exactly; price/volume y comparisons used tolerance 1e-9. Fake candles, volume, UTC axis, hover and modebar remain present. The overlay is explicitly a fixture trace, not SMA/research recomputation.

| Range | X start UTC | X end UTC | Price y range | Volume y range |
| --- | --- | --- | --- | --- |
| R1 | `2026-10-20T11:40:00.000000Z` | `2026-10-20T16:39:00.000000Z` | `96.88888888888889, 119.11111111111111` | `0, 10.526315789473685` |
| R2 | `2026-10-20T12:39:33.915600Z` | `2026-10-20T15:24:10.598600Z` | `102.45806100217865, 114.73772034066153` | `0, 10.526315789473685` |
| R3 | `2026-10-20T12:28:05.746300Z` | `2026-10-20T15:12:42.429300Z` | `103.07094952531149, 115.35060886379436` | `0, 10.526315789473685` |
| R4 | `2026-10-20T11:40:00.000000Z` | `2026-10-20T16:39:00.000000Z` | `17.444444444444443, 28.555555555555557` | `0, 10.526315789473685` |
| R5 | `2026-10-20T13:09:29.676200Z` | `2026-10-20T15:54:06.359200Z` | `19.67446524064171, 26.36886017033076` | `0, 10.526315789473685` |
| R6 | `2026-10-22T13:39:00.000000Z` | `2026-10-22T18:39:00.000000Z` | `97.85777777777778, 120.30222222222223` | `0, 10.526315789473685` |
| R7 | `2026-10-22T17:00:00.000000Z` | `2026-10-22T18:39:00.000000Z` | `97.85777777777778, 120.30222222222223` | `0, 10.526315789473685` |
| R8 | `2026-10-20T12:09:38.155100Z` | `2026-10-20T14:39:16.957800Z` | `101.34893048128342, 114.73772034066153` | `0, 10.526315789473685` |
| R9 | `2026-10-22T17:19:43.336600Z` | `2026-10-22T18:19:10.834200Z` | `103.48264161220044, 115.88509754406815` | `0, 10.526315789473685` |
| R10 | `2026-10-20T00:00:00.000000Z` | `2026-10-20T16:39:00.000000Z` | `96.88888888888889, 119.11111111111111` | `0, 10.526315789473685` |

Case evidence uses the range IDs above. Render counters are per mounted chart, so intentional remounts reset the counter to one. Query counters count local fixture reconciliation, not provider/HTTP requests. Revision/ack fields are the resulting server state.

| Actual browser case | Before -> after range | Render count | Fixture queries | Revision / ack | Result |
| --- | --- | --- | --- | --- | --- |
| physical mouse zoom | R1 -> R2 | 1 -> 1 | 1 -> 1 | 1 / 0 | PASS |
| physical mouse pan | R2 -> R3 | 1 -> 1 | 1 -> 1 | 2 / 0 | PASS |
| stale revision through component channel | R3 -> R3 | 1 -> 1 | 1 -> 1 | 2 / 0 | PASS |
| Refresh changed | R3 -> R3 | 1 -> 2 | 1 -> 2 | 2 / 1 | PASS |
| Refresh unchanged | R3 -> R3 | 2 -> 2 | 2 -> 3 | 2 / 1 | PASS |
| Correction | R3 -> R3 | 2 -> 3 | 3 -> 4 | 2 / 2 | PASS |
| visible overlay True | R3 -> R3 | 3 -> 4 | 4 -> 4 | 2 / 2 | PASS |
| visible overlay False | R3 -> R3 | 4 -> 5 | 4 -> 4 | 2 / 2 | PASS |
| Remount | R3 -> R3 | 5 -> 1 | 4 -> 4 | 2 / 3 | PASS |
| Date rollover | R3 -> R3 | 1 -> 1 | 4 -> 5 | 2 / 4 | PASS |
| Generation reset | R3 -> R3 | 1 -> 1 | 5 -> 6 | 2 / 5 | PASS |
| Gap fixture | R3 -> R3 | 1 -> 2 | 6 -> 7 | 2 / 6 | PASS |
| Retention partial | R3 -> R3 | 2 -> 3 | 7 -> 8 | 2 / 7 | PASS |
| ETH initial independent state | R3 -> R4 | 3 -> 1 | 8 -> 1 | 0 / 0 | PASS |
| ETH physical zoom | R4 -> R5 | 1 -> 1 | 1 -> 1 | 1 / 0 | PASS |
| BTC instrument round trip | R3 -> R3 | 3 -> 1 | 8 -> 8 | 2 / 7 | PASS |
| ETH instrument round trip | R5 -> R5 | 1 -> 1 | 1 -> 1 | 1 / 0 | PASS |
| fully expired retention | R3 -> R3 | 1 -> 2 | 8 -> 9 | 2 / 8 | PASS |
| explicit Return to Live | R3 -> R6 | 2 -> 3 | 9 -> 9 | 3 / 9 | PASS |
| explicit preset 100 resets intentionally | R6 -> R7 | 3 -> 4 | 9 -> 9 | 4 / 10 | PASS |
| session B independent physical zoom | R1 -> R8 | 1 -> 1 | 1 -> 1 | 1 / 0 | PASS |
| session B isolated overlay | R8 -> R8 | 1 -> 2 | 1 -> 1 | 1 / 0 | PASS |
| session A unaffected by B | R7 -> R7 | 4 -> 4 | 9 -> 9 | 4 / 10 | PASS |
| session A independent changed refresh and zoom | R7 -> R9 | 4 -> 5 | 9 -> 10 | 5 / 11 | PASS |
| session B unaffected by A | R8 -> R8 | 2 -> 2 | 1 -> 1 | 1 / 0 | PASS |
| explicit preset 1000 resets intentionally | R8 -> R10 | 2 -> 3 | 1 -> 1 | 2 / 1 | PASS |

Additional assertions behind these rows:

- Changed refresh increments the accepted version; unchanged keeps the same identity and render count. Correction changes the candle close at index -10 while preserving viewport.
- Overlay off/on/off was verified in the actual Plotly trace list and server checkbox state; query count stays unchanged. No click-only success.
- The explicit negative fixture replays an old revision through the real component state channel; server reports `stale revision` and retains accepted ranges. This is a deliberate replay, not a claim of naturally reordered network packets.
- Remount increments logical mount/key; date changes 2026-10-22 to 2026-10-23; generation changes fixture-generation-a to fixture-generation-b. Acknowledgments do not create manual interaction revisions.
- Gap fixture contains 999 actual fixture timestamps without rangebreak compression. Partial retention starts at 2026-10-20T13:00:00Z and preserves the partly overlapping viewport; complete expiry keeps the old viewport with an explicit warning/Return to Live action.
- Instrument round trips preserve each instrument's x/price-y/volume-y state. Two browser contexts retain independent generation/date/version, overlay and viewport. Mutating each session leaves the other unchanged.
- Explicit Return to Live and 100/1,000 preset selections intentionally reset; default 300 was verified at startup. Presets count retained rows. No user viewport gesture or overlay triggers fixture reconciliation.
- Actual pointer hover displayed OHLC at 2026-10-20T13:40:00Z: open 104, high 106, low 102, close 105. Eleven modebar entries were present; zoom and pan were exercised, while download/share/export were not invoked.

The raw final case file has 26 structured entries and SHA-256 `d737c9ba144ef53b4f7b0c15866159fd2290540deb9a275e005d112e1a363427`. It was validated to contain full before/after array/object data, not stringified/truncated summaries. The complete range and counter table above is the repository-reviewable evidence; machine-specific evidence roots are deliberately not published.

## Resource and loading observations

The component reads the existing 4,815,814-byte bundle into its trusted module; it is not a lightweight zero-cost wrapper. Resource timing in the component-only page showed no external asset origin. No native PlotlyChart chunk was required until the explicit native comparison was enabled. That comparison adds a 4,667,003-byte local Streamlit chunk; cached final timing was zero, while an earlier uncached local observation was about 79.7 ms. Neither is end-to-end usable-chart latency or a controlled performance benchmark.

Observed browser Plotly.react timings across final cases: 42.1-119.4 ms (includes cold chart creation/remount and changed data updates; unchanged reuses the prior diagnostic). The full module-loading/startup cost is not included in those values. Native versus component total startup/feedback and long-run memory are not yet controlled benchmarks; no provisional 5 s/2 s/200 ms target is declared passed.

Two-context sampled V8 heap used bytes: A 30650444, B 39113348. These are host-browser heaps, not container RAM and not comprehensive browser-process RSS. Container working-set sample with comparison enabled was 160 MiB/512 MiB; a cgroup sample reported memory.current 181,198,848 bytes and memory.peak 260,808,704 bytes, with 15 tasks. Final later idle/cleanup sample is recorded below; different sample times and Docker working-set/raw cgroup accounting must not be summed.

```json
{
  "memory.current": "181137408",
  "memory.peak": "260808704",
  "pids.current": "15",
  "cpu.stat": "usage_usec 11818332\nuser_usec 10061665\nsystem_usec 1756667\nnice_usec 0\nnr_periods 2826\nnr_throttled 11\nthrottled_usec 1172386\nnr_bursts 0\nburst_usec 0"
}
```

This is one Python fixture server with two browser contexts, not combined private C# research load or S2's planned 1/3/5-browser capacity study, and not Azure F1 memory/quota/durability qualification. Root-supervisor, storage, numerical/schema/HMAC and F1 obligations in S2 remain unresolved.

## Artifacts, preservation and limits

Final screenshot artifact `component-final-browser.png` (actual OHLC hover, volume, UTC labels and modebar) SHA-256 `82b244377e3f493f340072eaa3efc777443155b1ba97cb488a064fc6f7f08676`. Screenshots and temporary protocol transcripts remain outside the repository because the nine-path allowlist contains no binary artifact path; no private name or absolute machine path is published. The final screenshot was visually inspected. Reviewers can reproduce the fixture from the hashed sources and verify the reported ranges directly through the UI/component diagnostics.

Remaining limitations: no production API/analytics/cache integration; no hostile arbitrary descendant/security proof; no mobile/cross-browser or long-soak test; no stress test of near-simultaneous gesture debounce and server refresh/remount; no delivery acknowledgment/retry guarantee for an interaction canceled before its 250 ms settle timer; no full real-network scheduler test. Model tests and the settled browser cases do not prove every rapid-event race. These remain explicit integration/protocol proof obligations, not silently waived behavior. No new contract was frozen on the owner's behalf.

Automatic approval review rejected an initial combined cleanup command because older image ownership was not established; no action from it executed. Read-only checks showed those four older manifest IDs already absent. The safer cleanup verified the current image's build history/tag/reference, label-checked the container, closed only the owned browser, validated its absolute profile path stayed under the owned task directory before deletion, then removed the stopped container and unshared current tag. Container exit 0/no OOM. The pre-existing base image was rechecked unchanged; no global prune or unrelated process deletion. Temporary evidence is retained.

Before commit/push: independent staged/working-tree UTF-8/LF scan, `git diff --check`, nine-path allowlist and preserved-file hashes; these checks accompany publication. Draft PR #379 remains OPEN/UNMERGED; issue #367 remains OPEN. No WP01 acceptance, WP02 implementation, provider activity, publication or deployment. `PUBLIC_RUNTIME_VERIFIED=False`.

## Tested source SHA-256

| Exact authorized source/test path | SHA-256 |
| --- | --- |
| `tests/feasibility/release_1_15/viewport/viewport_component.py` | `c6c5ce812bc16427f0be7a447220ffc9a5f8265f6e2bcebf9307f67d0b2964eb` |
| `tests/feasibility/release_1_15/viewport/viewport_component.js` | `5e9e116a6b4eff64e3c4193a317d74eb964c1e2271905e2569a07a91af7c3dfa` |
| `tests/feasibility/release_1_15/viewport/viewport_component.html` | `862136b1662f47b958ce549aa32f45199a982ce595c3a93953f467fcf63eae4d` |
| `tests/feasibility/release_1_15/viewport/test_viewport_component_contract.py` | `c6d7c0fa342101b72ba577a77da6d8cec56a72051efde6852be377f1986ccd1a` |
| `tests/feasibility/release_1_15/viewport/app.py` | `002fd25c951cb17d12da58a6b7938a59a8539e8f35436c910bc811bacc557dd0` |
| `tests/feasibility/release_1_15/viewport/viewport_state.py` | `191d8f05b902808232346a760d0c90cb319646a06bfc9bbb163c74991e82147b` |
| `tests/feasibility/release_1_15/viewport/test_viewport_state.py` | `5d3d2594612c9e540cd883122b72f33cf32357172c56883844248c5384f2505b` |
| `tests/feasibility/release_1_15/viewport/Dockerfile` | `f141404f41d17a919c05f017519511a4d4613dab1a6ea113139b7b7d8f4ace7a` |
