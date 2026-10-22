# Release 1.15 WP01 S1 - Components-v2 feasibility evidence

Date: 2026-10-22 (America/Sao_Paulo). Issue #367. Existing draft PR #379; pre-change head `7edfbee3124fa758c379677a7f760f8d2524de05`. Clean main remains at planning merge `455b80d0e50c17791653457a4ebd3001d3629a6e`.

**Owner decision (2026-10-22): components-v2 is the preferred Release 1.15 mechanism; no further viewport-technology search.**

**Latest residual verification: NOT READY TO FINALIZE - pending user intent is lost across refresh.** Stale-accepted-state guards passed the tested cases, and the bounded resource screen showed no accumulating component listener counts or large retained browser heap growth. This does not waive the reproduced input-loss defect. See the residual verification section below.

**Original settled-case verdict: scoped component feasibility SUPPORTED by the tested local browser matrix; OWNER REVIEW REQUIRED.** This is not WP01 acceptance, a production component, a contract freeze or permission to begin WP02. The prior native/S2 reports remain historical observations; this report adds evidence rather than rewriting their earlier incomplete results. `PUBLIC_RUNTIME_VERIFIED=False` remains unchanged.

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

## Residual verification - owner-selected mechanism, 2026-10-22

Scope: one narrowly bounded read-only verification of implementation commit `058da1c93304978db3a303c754a6023367b7d6d8`. Only this existing evidence document is updated; component, fixture, tests, Dockerfile and production source are unchanged. The owner accepted components-v2 as the preferred direction. No alternative viewport technology was investigated. Acceptance of that direction is distinct from finalizing this prototype or accepting WP01.

Rebuilt the unchanged pinned fixture using the previously recorded build command, with tag `aiq-r115-wp01-residual:local`. Local image ID `sha256:a0a9f6b5f6aadc0ccf78d0d2468bf910a70d12eedcdbc3137e1ea50ad6965cfd`; functional manifest `sha256:683046c3543f718d604eac067dc072c37dd3060b1870a621697dd750f4e616a2` and config `sha256:96598096fd856dd94ed31a75cde4f71dccdfe598832ba0e9046b3de77fd46000` match the earlier tested build. The different aggregate image ID reflects regenerated build attestation, not changed application bytes. Same Streamlit 1.61.1, Plotly Python 7.1.0, local Plotly.js 4.1.1 and Chrome 155.0.8059.86; no dependency installation or external asset. Same loopback/read-only/1 CPU/512 MiB/64-task/32 MiB temporary-filesystem screening profile.

### Debounce, refresh, acknowledgment and detachment results

Used actual Chrome Input mouse gestures and fixture buttons, with no sleeps between the raced gesture and refresh/remount input. A separate settled control used bounded acknowledgment/revision polling. Temporary PowerShell 5.1/.NET/CDP orchestration is outside the repository; no runtime instrumentation or source patch was introduced. Browser heap/DOM diagnostics use supported CDP calls. Read-only Plotly emitter inspection is test diagnostics, not a component dependency on private internals.

| Case | Actual observation | Verdict |
| --- | --- | --- |
| Two rapid zooms without refresh | Latest visual range was accepted once; interaction revision 1 -> 2, no intermediate overwrite | PASS for this coalescing case |
| Pending zoom plus changed refresh, twice | Latest visible gesture was canceled; refresh restored the prior accepted range and revision did not advance | FAIL: input-loss defect, 2/2 reproductions |
| Remount with pending callback | At 17.1 ms after the immediate post-gesture sample, remount canceled the old callback; held old graph became disconnected, emitter removed by purge | No stale overwrite; pending user intent still lost |
| Fresh gesture on the new mount, then 1.4 s idle | Revision 3 and its viewport remained stable; old callback did not overwrite it | PASS for tested detached-callback guard |
| Closely spaced refresh and correction inputs | Both commands observed; current command/ack reached 5/5, viewport unchanged | PASS for observed overlap; not a forced reversal of acknowledgment delivery |
| Late stale-event replay after commands | Rejected without changing accepted x/y ranges | PASS for tested stale event |
| Runtime exceptions during race block | 0 observed | No exception observed; not a substitute for correct intent preservation |

The timing measurements below run from the immediate post-gesture browser sample through completion of the refresh input sequence. They are not claimed as exact input-release-to-server latencies; both samples still showed the old accepted revision before refresh, inside the pending 250 ms debounce window. All dates below are UTC, with string formatting normalized only for readability.

| Reproduction | Refresh input interval | Prior accepted x range / revision | Latest pending visual x range | Result after refresh |
| --- | --- | --- | --- | --- |
| 1 | 36.4 ms | `2026-10-20T12:54:31.7959Z to 2026-10-20T15:39:08.4789Z` / 1 | `2026-10-20T13:27:19.3785Z to 2026-10-20T15:14:25.5226Z` | `2026-10-20T12:54:31.7959Z to 2026-10-20T15:39:08.4789Z` / revision 1; pending event not delivered |
| 2 | 26.6 ms | `2026-10-20T13:48:39.5623Z to 2026-10-20T14:58:20.655Z` / 2 | `2026-10-20T14:02:32.4983Z to 2026-10-20T14:47:52.8756Z` | `2026-10-20T13:48:39.5623Z to 2026-10-20T14:58:20.655Z` / revision 2; pending event not delivered |

| Reproduction | Prior accepted price y | Pending visual price y | Result price y |
| --- | --- | --- | --- |
| 1 | `101.34893048128342, 113.62858981976629` | `103.20059257070372, 111.21192112308292` | `101.34893048128342, 113.62858981976629` |
| 2 | `104.40862880016509, 109.63527095733227` | `105.19675938836983, 108.60665427165537` | `104.40862880016509, 109.63527095733227` |

Volume y stayed `[0, 10.526315789473685]` in both reproductions. Price y, as well as x, returned to the prior accepted range.

The guards did **not** allow an older event to replace a newer accepted viewport in these cases. The separate failure is loss of the newest *pending* user intent: server state is still older when the refresh cancels the settle timer. Do not report this as a successful refresh-retention test merely because the prior accepted range survives.

Static source reconciliation explains the reproduction: relayout stores a 250 ms timer but does not durably reconcile pending intent; component cleanup/data replacement clears that timer, and refresh applies `data.viewport` from the prior accepted server state. No correction was made in this verification-only pass.

A focused follow-up should retain/reconcile pending gesture intent across data refresh and logical remount, distinguish explicit Return to Live/preset reset from ordinary reconciliation, and ensure old render acknowledgments cannot defeat a newer interaction. It should remain within the existing component/state/test boundary, preserve the preferred mechanism and add regression cases for these exact timings. This is a proposed correction, not unrequested implementation or an architectural search. PR finalization should wait for the correction and a rerun of the failing cases.

### Bounded resource-stability control

Predeclared envelope: at most five minutes, 30 refresh/real-pan cycles (20 changed, 10 unchanged), six logical remounts, post-GC samples at 0/10/20/30 and one final idle sample. The diagnostic heap flag was set before the run: final retained heap above baseline by both 20% and 5 MiB. It is a screening heuristic, not an approved product budget or acceptance SLA. Component resize listener count should remain stable and relayout emitter count should remain one.

The first attempt stopped at its first interaction because a fixed 450 ms read had not yet observed the accepted revision. A later observation showed delivery, and a separately settled pan succeeded. That early attempt is INCOMPLETE, not a pass or evidence of a leak. The controlled run retained the same actual-interaction criterion, replacing the timing assumption with at most three-second polling for the current acknowledgment/accepted revision. This isolates resource stability from the already recorded debounce/refresh failure; it does not cure or waive that race.

**Controlled result: 30/30 cycles completed in 45.154 seconds**, six remounts, no browser runtime exceptions. The full run fit the predeclared five-minute bound. Twenty changed refreshes produced twenty `Plotly.react` calls; ten unchanged refreshes produced none; six remounts created their new charts as expected. Direct pan rendering is not counted as a `Plotly.react` call. No excess application-driven redraw count was observed under this settled control.

| Completed cycles | Post-GC V8 used bytes | DOM nodes | DOM event listeners | Window resize listeners | Component relayout handlers |
| --- | --- | --- | --- | --- | --- |
| 0 | 24,791,780 | 4,239 | 454 | 2 | 1 |
| 10 | 24,899,820 | 4,165 | 455 | 2 | 1 |
| 20 | 25,410,480 | 4,166 | 455 | 2 | 1 |
| 30 | 25,595,448 | 4,166 | 455 | 2 | 1 |
| Final idle | 25,578,884 | 4,165 | 455 | 2 | 1 |

Final minus baseline V8 used heap: **787,104 bytes (about 0.75 MiB / 3.17%)**. This did not trigger the predeclared growth screen. One additional DOM listener appeared during warm interaction, then remained stable; resize and relayout counts did not accumulate across remounts. DOM nodes returned below baseline. Forced-GC observations distinguish retained heap from ordinary allocation peaks, but a finite 45-second control cannot prove absence of long-run leaks.

Twenty changed-refresh `Plotly.react` browser timings: min 40.9 ms, median 48.8 ms, nearest-rank p95 66.8 ms, max 74.6 ms. These are renderer-call durations, not gesture-to-visible-feedback or full request/ack latency. No production performance threshold is marked passed. After 2.2 seconds quiescence, accepted revision remained 40 with identical ranges; no accumulating late callbacks were observed. There is no direct active-timer census, so this is bounded behavioral/listener evidence, not proof of zero dormant timers.

Container working-set sample: 179.2 MiB/512 MiB. A late sample reported raw cgroup memory.current 254,935,040 bytes and peak 389,898,240 bytes. Final later observation:

```json
{
  "memory.current": "254799872",
  "memory.peak": "389898240",
  "pids.current": "20",
  "cpu.stat": "usage_usec 24240944\nuser_usec 21150890\nsystem_usec 3090054\nnice_usec 0\nnr_periods 3604\nnr_throttled 42\nthrottled_usec 1141597\nnr_bursts 0\nburst_usec 0"
}
```

Container shutdown reported exit 0/no OOM. The raw peak is not browser heap and must not be added to Docker working set. Container trend samples were not taken at every browser checkpoint; this pass does not establish server long-soak stability. Nor does it qualify combined C# research load, multiple-session capacity or Azure F1 headroom.

### Review handoff and preservation

Recorded 47 structured residual-pass observations, including both failures, the aborted fixed-delay attempt and the completed control. Temporary raw evidence SHA-256: `12e745b2f36ebcdd4ee50fd1eb19f3c5762e6fbb1d5a03c719df8ac7a37fb7c7`. The exact failing viewport vectors and resource table are published above, without machine-specific evidence roots or personal identifiers. Source and dependency hashes remain those already recorded for commit `058da1c`; no application/test correction or new asset was written. Original 32 offline tests were not rerun in this documentation-only update; their prior result is preserved, not recounted as new tests.

Only this existing evidence Markdown is changed for PR #379. UTF-8/LF and `git diff --check` apply before commit/push. The label-verified owned container, unique owned Chrome/profile and unshared experiment tag were removed after path/provenance checks; temporary observations retained, pre-existing resources untouched. No global prune, provider/Azure activity, deployment, issue closure or WP02 work.

**Disposition:** components-v2 remains the owner-selected preferred direction. Resource screen is encouraging but bounded. The reproducible pending-intent loss must be resolved before finalizing this implementation. Keep PR #379 DRAFT/OPEN and issue #367 OPEN. `PUBLIC_RUNTIME_VERIFIED=False`.

## Pending-intent correction - final review, 2026-10-22

Authority: [owner review comment](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/379#issuecomment-6333075971), followed by the owner instruction to implement, commit regression evidence and return PR #379 for final review. Starting worktree/remote head `7a28d617d8deb57589c18fa425f47cd1756e6de3`; base `455b80d0e50c17791653457a4ebd3001d3629a6e`. Both worktrees initially clean. Prior failures above remain historical FAIL observations; this section records a separate corrected-source run.

### Correction and ordering contract

- Capture UTC x, price y and volume y2, together with a monotonically increasing interaction revision, at the manual relayout event rather than at timer delivery. Ignore mode changes and suppress programmatic restores. Keep the 250 ms debounce for ordinary gestures.
- Hand the newest unacknowledged intent across updates/remounts using browser sessionStorage, with a per-Streamlit-session random nonce and instrument-specific slot (maximum two current-session slots). This is tab-local viewport data, not a provider/storage/transport service. A stored intent remains until server acceptance or an explicit reset fence; a disconnected component retains no publishing callback. A new Streamlit session receives a new namespace. Browser storage denial/corruption and abandoned-session storage reclamation are not qualified here.
- Ordinary refresh, correction, retention, display-date rollover, generation replacement and remount retain absolute manual ranges. Rebind that existing revision to the current instrument/snapshot identity, mount and command epoch; do not invent another gesture. The unchanged-refresh path does not redraw. Retention expiry keeps the chosen historical range with the existing explicit expired warning; overlap does not silently reset it.
- Return to Live and preset selection increment a separate reset epoch. An older buffered gesture cannot defeat that reset. Component invocation/mount/command/accepted-revision guards and per-invocation tickets reject obsolete callbacks; cleanup only clears the handler/timer it owns. Actual detached graphs are purged. An eligible intent is committed once by the existing strict server validator; duplicate revision, old identity, mount, command or acknowledgment revision remain rejected.
- A pending gesture is published before the render acknowledgment, preventing the single component state channel from replacing it with an acknowledgment. After server acceptance the buffer is removed and the current acknowledgment can follow. Synthetic fixtures only; no analytics, provider or production code changed.

### Exact changed paths

- `tests/feasibility/release_1_15/viewport/app.py`
- `tests/feasibility/release_1_15/viewport/viewport_state.py`
- `tests/feasibility/release_1_15/viewport/viewport_component.js`
- `tests/feasibility/release_1_15/viewport/test_viewport_component_contract.py`
- `docs/roadmap/release-1.15/prompters/release-1.15-wp01-component-feasibility-evidence.md`

No new repository file, dependency, production edit or fixture snapshot change. All five paths are within the existing nine-path authority. The fixture nonce uses Python standard-library uuid, not a dependency.

### Final build and focused tests

Final local image ID `sha256:a2e12364de976c336b7efa5150f4d0853a98639206d7941606b72f52d7ab65c9`; functional manifest `sha256:fdf91aa91834e006f52f70655b6349bc89d55d8e8f812647dde7158d9568df34`; config `sha256:105e5a24d84ed8a2eb60be9d347ea4fdf0e59bb3cd93298c0ece7c43f2c99cc5`. Same pinned local base, Streamlit 1.61.1, Plotly Python 7.1.0, Plotly.js 4.1.1 and installed Chrome 155.0.8059.86. Built with no network/pull; no packages installed. Container: read-only, loopback binding, 1 CPU, 512 MiB, 64 tasks, 32 MiB temporary filesystem.

```powershell
docker build --pull=false --network=none -f tests/feasibility/release_1_15/viewport/Dockerfile -t aiq-r115-wp01-fix:local .
python -B -m unittest discover -s tests/feasibility/release_1_15/viewport -p 'test_viewport*.py' -v
docker exec aiq-r115-wp01-fix /opt/venv/bin/python -B -m unittest discover -s /spike -p test_viewport*.py -v
```

Host: **36/36 PASS, exit 0**. Final Linux image: **36/36 PASS, exit 0**. These are the same 36 tests on two runtimes, not 72 distinct tests. All original 32 are retained. Four added tests contain literal pending vectors from both published failures, once-only acceptance, acknowledgment-before/after ordering, refresh/remount reset-fence preservation, retention expiry and rejection of intent delayed past an explicit reset. The second literal is the pending 14:02:32.4983Z..14:47:52.8756Z range, not its older accepted range. Intermediate builds/checks are superseded by this final-image run.

Host hashes below were compared with the actual final container files before cleanup:

| Path within viewport folder | SHA-256 |
| --- | --- |
| `app.py` | `5c6dd7fd5bccf71aa0eb04125e2d72e44b11ba6064d40060046b8b6b6d8e0193` |
| `viewport_state.py` | `07c9349922b30b767a5acc8e25f79bc03cf2f381d692e0053f3a3881d498a2c6` |
| `viewport_component.js` | `2455ed900e35ca316b2212b0422ff4c16324be41e6bfc2c446db53fd89174054` |
| `test_viewport_component_contract.py` | `2f9f8de7cc95e0d92cdbc615301d71b1037f6dbfbb8143059a72eab557f721f8` |
| `fixture_snapshots.py` | `248964c3f6172db13d3d450ac9361dd1242efb688918c062b2a3442778159ad9` |

### Final-image browser matrix

Actual Chrome mouse/keyboard interaction through CDP, using temporary Windows PowerShell 5.1/.NET orchestration outside the repository. No browser package installation, component framework replacement, Streamlit patch or application DOM workaround. Read-only browser layout/heap/listener inspection supplies test observations. Two isolated browser contexts have independent Streamlit sessions.

**Cold-load warning:** the first matrix attempt completed cases 1-20, but the second context had no chart. Its server traceback reported `AttributeError: partially initialized module pandas ... has no attribute Series` during Plotly layout construction. A session reload recovered it; cases 21-26 then passed on the same final image. This is a real observed fixture initialization failure, not merely a missed automation click. Simultaneous cold initialization/import ordering is a suspected cause, not a proven diagnosis. No dependency/startup fix was attempted under this narrow correction. Cold-start concurrency remains unresolved for final owner review. The completed functional cases below do not erase that failed attempt.

| Case | Final functional result |
| --- | --- |
| 01 physical zoom | PASS |
| 02 physical pan | PASS |
| 03 stale replay | PASS |
| 04 changed refresh | PASS |
| 05 unchanged refresh | PASS |
| 06 correction | PASS |
| 07 overlay on | PASS |
| 08 overlay off | PASS |
| 09 remount | PASS |
| 10 date rollover | PASS |
| 11 generation replacement | PASS |
| 12 gap | PASS |
| 13 partial retention | PASS |
| 14 ETH independent live | PASS |
| 15 ETH zoom | PASS |
| 16 BTC return | PASS |
| 17 ETH return | PASS |
| 18 expired retention | PASS |
| 19 Return to Live | PASS |
| 20 preset 100 | PASS |
| 21 second session zoom | PASS |
| 22 B overlay | PASS |
| 23 A unaffected by B | PASS |
| 24 A refresh and zoom | PASS |
| 25 B unaffected by A | PASS |
| 26 B preset 1000 | PASS |

### Pending gesture races and exact ranges

Nine actual gestures were followed by command input inside the 250 ms debounce window. The recorded upper bound starts **before dispatching mouse release** and ends after the real button input sequence, including sampling/CDP overhead. Repeated changed refresh, unchanged refresh, correction, remount, date rollover, generation replacement and retention all preserved pending x/y/y2. No late duplicate revision was observed after the additional 500 ms settle check.

| Command | Release/input upper bound ms | Revision before -> pending -> accepted | Command epoch before -> after | Queries before -> after | Graph renders before -> after | WS sent/received |
| --- | --- | --- | --- | --- | --- | --- |
| Refresh changed | 106.477 | 8 -> 9 -> 9 | 14 -> 15 | 11 -> 12 | 7 -> 8 | 3/114 |
| Refresh changed | 92.761 | 9 -> 10 -> 10 | 15 -> 16 | 12 -> 13 | 8 -> 9 | 3/114 |
| Refresh unchanged | 72.443 | 10 -> 11 -> 11 | 16 -> 16 | 13 -> 14 | 9 -> 9 | 3/114 |
| Correction | 73.367 | 11 -> 12 -> 12 | 16 -> 17 | 14 -> 15 | 9 -> 10 | 3/114 |
| Remount | 74.288 | 12 -> 13 -> 13 | 17 -> 18 | 15 -> 15 | 10 -> 1 | 3/114 |
| Date rollover | 88.956 | 13 -> 14 -> 14 | 18 -> 19 | 15 -> 16 | 1 -> 1 | 3/114 |
| Generation reset | 74.386 | 14 -> 15 -> 15 | 19 -> 20 | 16 -> 17 | 1 -> 1 | 3/114 |
| Retention partial | 73.927 | 15 -> 16 -> 16 | 20 -> 21 | 17 -> 18 | 1 -> 2 | 3/114 |
| Retention expired | 94.231 | 18 -> 19 -> 19 | 23 -> 24 | 19 -> 20 | 3 -> 4 | 3/117 |

A render count of 1 after remount denotes a new graph, not a negative redraw count. Changed refresh/correction add one redraw, unchanged refresh adds none. Acknowledgment/intent reruns produced the observed WebSocket frames above; these are actual Streamlit protocol frames, not provider queries or an efficiency SLA. No external resource origin was observed (resource origin set: loopback only).

Exact recorded vectors follow. Each tuple contains x (UTC), price y, volume y2. Pending and after were compared using exact UTC ticks and absolute y/y2 tolerance 1e-9. Different UTC string formatting is normalized only in this published copy.

```json
{"case":"race pending then Refresh changed","before":{"x":["2026-10-20T12:39:33.9156Z","2026-10-20T15:24:10.5986Z"],"y":[106.59085513924326,113.44426216041794],"y2":[0,10.526315789473685]},"pending":{"x":["2026-10-20T13:20:35.817Z","2026-10-20T14:51:13.3235Z"],"y":[107.96634675562541,111.75343441438504],"y2":[0,10.526315789473685]},"after":{"x":["2026-10-20T13:20:35.817Z","2026-10-20T14:51:13.3235Z"],"y":[107.96634675562541,111.75343441438504],"y2":[0,10.526315789473685]}}
{"case":"race pending then Refresh changed","before":{"x":["2026-10-20T13:20:35.817Z","2026-10-20T14:51:13.3235Z"],"y":[107.96634675562541,111.75343441438504],"y2":[0,10.526315789473685]},"pending":{"x":["2026-10-20T13:43:11.1915Z","2026-10-20T14:33:04.755Z"],"y":[108.72642233687044,110.8191089219248],"y2":[0,10.526315789473685]},"after":{"x":["2026-10-20T13:43:11.1915Z","2026-10-20T14:33:04.755Z"],"y":[108.72642233687044,110.8191089219248],"y2":[0,10.526315789473685]}}
{"case":"race pending then Refresh unchanged","before":{"x":["2026-10-20T13:43:11.1915Z","2026-10-20T14:33:04.755Z"],"y":[108.72642233687044,110.8191089219248],"y2":[0,10.526315789473685]},"pending":{"x":["2026-10-20T13:55:37.3791Z","2026-10-20T14:23:05.4548Z"],"y":[109.14642845128392,110.30281497778455],"y2":[0,10.526315789473685]},"after":{"x":["2026-10-20T13:55:37.3791Z","2026-10-20T14:23:05.4548Z"],"y":[109.14642845128392,110.30281497778455],"y2":[0,10.526315789473685]}}
{"case":"race pending then Correction","before":{"x":["2026-10-20T13:55:37.3791Z","2026-10-20T14:23:05.4548Z"],"y":[109.14642845128392,110.30281497778455],"y2":[0,10.526315789473685]},"pending":{"x":["2026-10-20T14:02:28.185Z","2026-10-20T14:17:35.5162Z"],"y":[109.3785173915124,110.01751885891917],"y2":[0,10.526315789473685]},"after":{"x":["2026-10-20T14:02:28.185Z","2026-10-20T14:17:35.5162Z"],"y":[109.3785173915124,110.01751885891917],"y2":[0,10.526315789473685]}}
{"case":"race pending then Remount","before":{"x":["2026-10-20T14:02:28.185Z","2026-10-20T14:17:35.5162Z"],"y":[109.3785173915124,110.01751885891917],"y2":[0,10.526315789473685]},"pending":{"x":["2026-10-20T14:06:14.35Z","2026-10-20T14:14:33.8719Z"],"y":[109.506766182013,109.85986859715756],"y2":[0,10.526315789473685]},"after":{"x":["2026-10-20T14:06:14.35Z","2026-10-20T14:14:33.8719Z"],"y":[109.506766182013,109.85986859715756],"y2":[0,10.526315789473685]}}
{"case":"race pending then Date rollover","before":{"x":["2026-10-20T14:06:14.35Z","2026-10-20T14:14:33.8719Z"],"y":[109.506766182013,109.85986859715756],"y2":[0,10.526315789473685]},"pending":{"x":["2026-10-20T14:08:18.8628Z","2026-10-20T14:12:53.8695Z"],"y":[109.57763449761939,109.77275347925007],"y2":[0,10.526315789473685]},"after":{"x":["2026-10-20T14:08:18.8628Z","2026-10-20T14:12:53.8695Z"],"y":[109.57763449761939,109.77275347925007],"y2":[0,10.526315789473685]}}
{"case":"race pending then Generation reset","before":{"x":["2026-10-20T14:08:18.8628Z","2026-10-20T14:12:53.8695Z"],"y":[109.57763449761939,109.77275347925007],"y2":[0,10.526315789473685]},"pending":{"x":["2026-10-20T14:09:27.4121Z","2026-10-20T14:11:58.8142Z"],"y":[109.61679524242862,109.72461500054897],"y2":[0,10.526315789473685]},"after":{"x":["2026-10-20T14:09:27.4121Z","2026-10-20T14:11:58.8142Z"],"y":[109.61679524242862,109.72461500054897],"y2":[0,10.526315789473685]}}
{"case":"race pending then Retention partial","before":{"x":["2026-10-20T14:09:27.4121Z","2026-10-20T14:11:58.8142Z"],"y":[109.61679524242862,109.72461500054897],"y2":[0,10.526315789473685]},"pending":{"x":["2026-10-20T14:10:05.1512Z","2026-10-20T14:11:28.5041Z"],"y":[109.63843486968506,109.69801441517046],"y2":[0,10.526315789473685]},"after":{"x":["2026-10-20T14:10:05.1512Z","2026-10-20T14:11:28.5041Z"],"y":[109.63843486968506,109.69801441517046],"y2":[0,10.526315789473685]}}
{"case":"race pending then Retention expired","before":{"x":["2026-10-20T12:39:33.9156Z","2026-10-20T15:24:10.5986Z"],"y":[102.45806100217865,114.73772034066153],"y2":[0,10.526315789473685]},"pending":{"x":["2026-10-20T13:20:35.817Z","2026-10-20T14:51:13.3235Z"],"y":[104.92261161473178,111.70816312084531],"y2":[0,10.526315789473685]},"after":{"x":["2026-10-20T13:20:35.817Z","2026-10-20T14:51:13.3235Z"],"y":[104.92261161473178,111.70816312084531],"y2":[0,10.526315789473685]}}
```

Additional actual-browser schedules passed: refresh input before the gesture (three rapid repetitions), pending Return to Live, pending preset reset, pending instrument switch away/back, and two rapid gestures selecting the latest range. Runtime exceptions during the final race block: 0. Both browser sessions had zero pending-intent slots after quiescence.

The deterministic server tests explicitly force old acknowledgment processing both before and after the newer intent, including rejection of the delayed old revision. The browser tests reverse input scheduling and observe current acknowledgment convergence; they do not claim arbitrary network acknowledgment delivery was intercepted/reordered. Browser transport scheduling is not exhaustively proven.

### Final bounded resource screen

Completed **30/30 refresh/pan cycles**, 20 changed/10 unchanged, six remounts, in **45.122 seconds**. Same predeclared screening flag as the earlier control: retained heap growth above both 20% and 5 MiB; maximum three minutes for this final run. Twenty changed refreshes caused exactly twenty Plotly.react calls; ten unchanged refreshes caused none; six remounts created six graphs. No extra application-driven redraw was counted.

| Sample | Post-GC used heap bytes | DOM nodes | DOM listeners | Resize listeners | Relayout handlers |
| --- | --- | --- | --- | --- | --- |
| resource baseline | 29253864 | 4311 | 456 | 2 | 1 |
| resource sample 10 | 29260872 | 4185 | 456 | 2 | 1 |
| resource sample 20 | 29443448 | 4184 | 456 | 2 | 1 |
| resource sample 30 | 29567320 | 4185 | 456 | 2 | 1 |
| Final idle | 29550700 | 4184 | 456 | 2 | 1 |

Retained heap growth **296836 bytes (0.283 MiB / 1.01%)**; screening flag false. Changed-refresh Plotly.react durations: min 39.5 ms, median 45.05 ms, nearest-rank p95 61.1 ms, max 61.4 ms. These are renderer-call durations, not end-to-end latency. After 2.2 seconds idle, revision/ranges did not drift; runtime exceptions 0. No direct active-timer census or long-soak/capacity claim.

Container working-set sample 184.8 MiB/512 MiB; raw cgroup memory.current 203,079,680 bytes, peak 210,206,720 bytes, pids.current 21, OOM/OOM-kill counters 0. These are different accounting measures, not additive budgets, and do not qualify Azure F1 headroom or combined research load.

### Evidence custody, limitations and review handoff

- Final raw observation file `correction-final-browser.json`: 78 records, SHA-256 `2cd252fda1ffe91195600edf0444813babc750a66463e9dd78e70b6a70ccab4b`. Exact race vectors and resource observations are committed above; raw observations remain in the owned temporary artifact directory without publishing personal paths.
- Supporting screenshot `correction-final-browser.png`: SHA-256 `d8fa92096b05bf6bf3f5538c6724c0fedf370d69eddfc3af29b3e004c2d29ae1`. Screenshot inspected: fake candles/volume, UTC axes and native Plotly controls visible; the exact narrow viewport/hover is a gesture result, not a default range.
- Existing settled and residual evidence files, including the two original failures, were not overwritten. Primary checkout remains clean at its baseline; only the five listed experiment/evidence paths changed. UTF-8/LF, separate CRLF/UTF-8 scan and git diff --check are required before committing.
- Confirmed owned Chrome/profile, label-verified container and unshared task image/tag removed; container exit 0, OOMKilled false. Prior intermediate image IDs were already absent. Pre-existing Docker base and unrelated processes/resources preserved.
- Remaining limits: unresolved observed cold-load pandas/Plotly initialization error; storage denial/corruption and abandoned-session slot reclamation; finite browser schedule/resource coverage; no native transport freshness/capacity or production integration qualification. These are review obligations, not silently accepted gates.

**Disposition:** pending-intent correction and focused regression evidence submitted for final owner review. PR #379 remains DRAFT/OPEN; issue #367 remains OPEN. No merge, WP01 acceptance/closure, WP02, provider operation, Azure change or deployment. `PUBLIC_RUNTIME_VERIFIED=False`.
