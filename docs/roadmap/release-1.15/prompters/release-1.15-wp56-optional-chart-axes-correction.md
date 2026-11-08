# Release 1.15 WP56 - optional chart axes correction

Date: 2026-11-08. Code baseline main `61a1f804526b145e8a3d47f1daf4795f3fbec375`; isolated branch `fix/release-1.15-wp56-optional-chart-axes`. PR #443 was independently verified OPEN/UNMERGED at expected `470561539372adde0ec0452cfc8a930f06b6e755`; its evidence/branch and retained runtime were not rewritten. This is a focused production-fix follow-up, not a new data acquisition or milestone acceptance.

## Observed browser evidence and limits

WP56 first authorized offline source/test work only. Owner separately authorized one bounded diagnostic session; a temporary-driver Python parse error launched no Chrome and was corrected offline. The ensuing session stopped during BTC readiness because its state probe raised before obtaining valid DOM state; Chrome closed, no ETH click and no new ETH evidence. The driver then gained a document-readiness guard and sanitized evaluation-error capture offline. A **separately owner-approved replacement session** performed one BTC visit then one ETH click, with observations at0,2,10,29 seconds afterward, and closed only its owned Chrome. Existing venv/Chrome used; no installation, direct API probes, private-file access, Docker/provider/runtime mutations or screenshot/price capture.

| Captured state | BTC before selection | ETH after selection, through29 seconds |
| --- | --- | --- |
| Radio checked | BTC true, ETH false | BTC false, ETH true |
| Connected, research caption,1000 retained candles | Present | Present |
| Fixed availability warnings | None observed | None observed |
| Component host / data-bearing graph | Present / BTC verified | Absent / no graph found |
| Shadow roots, iframes, custom elements | None; host was ordinary div | None |
| Candlestick/SMA and null-volume annotation | Present; no volume bars | Not rendered/verified |
| Page exception element | Absent | Absent |
| Failed same-origin asset responses | None captured | None captured |

Two console-error events mentioned component/module, undefined and range. Raw console text was not retained in published evidence. No Runtime.exceptionThrown event was observed; that does not rule out framework-handled errors. These observations distinguish the failure from a missing/invalid ETH snapshot at the UI level: connected/candle-count/research content rerendered, but the component vanished. They do not by themselves prove every cause or success of a correction.

## Reproduced defect and narrow fix

Offline real `component_data` fixtures show that OHLC-only with no oscillator has only xaxis/yaxis: secondary axes are genuinely absent. The actual component JavaScript unconditionally read `graph.layout.yaxis2.range` while constructing its acknowledgement, and wrote `layout.xaxis2.range` while restoring a viewport. The new executable lifecycle test against the unmodified component **failed with TypeError, exit1**; after correction it passes. This establishes a concrete component lifecycle defect consistent with the observed undefined-range console signals. The installed chart has not been replaced, so resolution of its ETH symptom remains unverified.

- JavaScript emits null for an absent secondary viewport, preserves null in pending intent, and only restores secondary ranges if the target layout contains the axis. Primary axes and existing ticket/revision/session/cleanup protections unchanged.
- Python validates secondary null **only when the exact rendered figure lacks yaxis2**; the default/present-axis path still requires finite ordered bounds. A non-null secondary range for a missing axis is rejected atomically. Primary axis, instrument/identity/mount/revision/epoch/size validation remain unchanged.
- In OHLC-only mode, switching oscillator panels clears the former secondary oscillator range so RSI/MACD cannot reuse another panel's scale. Numeric-volume mode retains its volume-axis range behavior. No fetch/retry, indicators or authoritative data computations added.

Volume remains in snapshots; null never becomes zero. Numeric zero/positive volume and oscillator figures are included in the lifecycle regression. Twelve Data remains sole upstream, BTC/USD/ETH/USD unchanged; C# indicators and transformation/provenance/null-volume disclosures preserved.

## Executed offline verification

Existing Python3.13.15/project venv; cached dependencies only. Existing installed editor Node-compatible runtime24.21.0 executed the JavaScript as a process with ELECTRON_RUN_AS_NODE=1 and a nonsecret environment allowlist: no GUI/browser/chart access or install. The lifecycle test uses the actual component source and actual offline figure payloads, a stubbed Plotly renderer supplying default ranges only for existing axes, in-memory session storage and timers. It exercises27 BTC/ETH/BTC mounts across null/zero/positive volume and None/RSI/MACD panels; manual debounce, pending-intent handoff, remount, restoration and cleanup. This is not a real Plotly browser integration test. A host without an existing compatible JS runtime honestly skips this single test rather than installing anything; no such skip occurred here. Existing editor crashpad/bare-mode Streamlit warnings are not application failures.

Streamlit AppTest separately proves null-volume BTC->ETH->BTC sends the correct instrument and component key/figure on every mount while fetching only BTC then ETH once. Its component renderer is mocked, so it does not substitute for browser acceptance. Viewport tests prove absent-axis null handling and atomic rejection of mismatched/invalid ranges. Existing chart/client tests cover numeric volume, identities, sessions, disclosure and supplied analytics.

```text
PYTHONPATH=python/presentation
<existing-venv-python> -B -m unittest test_research_chart test_research_snapshot_client -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
```

Before edits: chart/client63 pass, exit0; workflow38 pass1 Windows unprivileged-symlink skip/39, exit0. Added actual-component regression then fails on baseline with TypeError as noted above. Final chart/client **66 passed, exit0**, workflow **38 passed/1 skipped of39, exit0**; combined105 run104 passed1 skip. No C# touched or C# rerun/broad-suite claim; prior29 baseline-matched Infrastructure harness prerequisites remain open. UTF-8/LF/no BOM and git diff --check checked before commit. No application exceptions were caught-and-ignored to force these passes.

Exact changed paths: `python/presentation/research_chart.py`, `python/presentation/research_chart_component.js`, `python/presentation/test_research_chart.py`, and this evidence note. No dependency, schema, provider, frozen data, C#, Dockerfile, startup, API/authentication or public configuration change.

## Runtime boundary and acceptance

| Acceptance item | Status |
| --- | --- |
| Authentic BTC/ETH import and both-symbol API | Prior WP55 PASS; not rerun. |
| BTC actual browser, retained old runtime | PASS observed. |
| ETH actual browser, retained old runtime | FAIL/INCOMPLETE: rerendered status but component/graph absent. |
| New-code offline lifecycle/regressions | PASS. |
| Corrected code in real browser | NOT_RUN; retained image still embeds old JavaScript. |
| LOCAL_CHART_VERIFIED | False. |
| PUBLIC_RUNTIME_VERIFIED | False. |

A separately approved corrected UI artifact build and controlled runtime switch/browser requalification are required. Preserve existing authentic bundle/database/volume; **do not redo acquisition/import**. No retained preparation/context/image/manifest was edited or identity guard bypassed. If the normal fresh preparation path is selected, it needs its own resource authority and fresh validated identities; it must not silently mutate old artifacts. Do not infer rebuild/restart/private-data/browser permission from this source/test correction. No Docker inspection/build/restart, API probe, provider request, credential handling, Azure/registry/deployment or public activation occurred under WP56. Both PRs remain OPEN/UNMERGED pending owner review.

`AUTHENTIC_DATA_NOT_IMPORTED=False`; `LOCAL_CHART_VERIFIED=False`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
