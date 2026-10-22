# Release 1.15 WP01 - Supported viewport component proposal

Date: 2026-10-22. Status: PROPOSAL ONLY; implementation NOT AUTHORIZED. Owner requested this separate exact-path proposal after the [S1 native finding](release-1.15-wp01-viewport-feasibility-evidence.md). No component was implemented. Issue #367 remains OPEN.

## Decision requested

Authorize a small, local-only components-v2 experiment on the existing S1 fixture, using installed Streamlit 1.61.1 and Plotly 7.1.0. Its purpose is to own one Plotly chart and exchange bounded viewport events through the supported component channel. It must not locate/manipulate the native Streamlit chart or application DOM, replace a production chart, acquire data or perform research. This is a candidate to prove, not an accepted solution or dependency upgrade.

Read-only inspection confirms the installed `streamlit/components/v2/__init__.py` exposes `component`, a component-owned parent element and supported state/trigger callbacks. [Official component API](https://docs.streamlit.io/develop/api-reference/custom-components/st.components.v2.component) describes that boundary; the current online page is 1.66.0, so the installed 1.61.1 implementation governs this experiment. [Plotly event documentation](https://plotly.com/javascript/plotlyjs-events/) supports relayout handlers on a chart the component owns. Neither document proves end-to-end state retention on the pinned stack.

## Exact proposed mutation allowlist

| Literal path | Action / purpose |
| --- | --- |
| `tests/feasibility/release_1_15/viewport/viewport_component.py` | NEW: register component, load fixed trusted local assets and validate bounded event payloads |
| `tests/feasibility/release_1_15/viewport/viewport_component.js` | NEW: component-owned Plotly rendering, relayout/restore acknowledgments, cleanup |
| `tests/feasibility/release_1_15/viewport/viewport_component.html` | NEW: fixed component-owned chart root only |
| `tests/feasibility/release_1_15/viewport/test_viewport_component_contract.py` | NEW: negative event/identity/epoch/range validation tests |
| `tests/feasibility/release_1_15/viewport/app.py` | MODIFY: fixture-only native/component selection and explicit state evidence |
| `tests/feasibility/release_1_15/viewport/viewport_state.py` | MODIFY: add bounded command/acknowledgment and coalesced-refresh state after specification review |
| `tests/feasibility/release_1_15/viewport/test_viewport_state.py` | MODIFY: preserve nine tests, add independent request/command/remount ordering negatives |
| `tests/feasibility/release_1_15/viewport/Dockerfile` | MODIFY: package existing local assets only, assert unchanged dependency versions |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp01-component-feasibility-evidence.md` | NEW: actual browser ranges, results, limitations, source/image provenance and cleanup |

No production presentation/source, dependency/project file, framework fork, generated repository Plotly bundle, npm installation, CDN, package download, new server or public route. No generic directory grant. If supported local Plotly loading needs a package manifest, extra asset or another path, stop and present that exact amendment first.

## Narrow contract to prove

Python owns accepted fixture identity, authoritative data and per-session/per-instrument viewport. Component owns only the chart element it is given. Fixed trusted HTML/JS is separate from data; fixture labels/values never become executable HTML. No page-wide selectors, parent-page traversal, arbitrary script evaluation or native Streamlit internals.

Use the Plotly.js bundle already supplied by installed Plotly, through a documented supported local loading path; no network fetch or new dependency. This loading path is a proof obligation: do not assume a global `window.Plotly` from Streamlit internals, use page injection or introduce a bundler as a fallback. Large runtime bundle size, duplicate loading, component remount and memory cost must be measured. Failure to load proportionately is BLOCKED, not grounds for unreviewed packaging changes.

Events contain only instrument, accepted snapshot identity, mount token, strictly ordered interaction revision, command epoch/acknowledgment and finite x/y ranges. Bound strings, payload bytes and array lengths; reject malformed UTC, reversed/unbounded ranges, wrong identity, stale mount/epoch/revision and non-finite values. Never accept component events as new market data or indicator values.

Manual pan/zoom preserves immediate browser feedback; settle state around 200-300 ms. Programmatic restore must acknowledge its command and suppress a false manual-event echo. Return to Live resets immediately. At most one active request and one pending refresh per instrument; chart interaction does not call a provider/API. Generation/date/structural changes rebuild with the eligible absolute UTC viewport. Sessions and instruments remain independent.

## Proposed bounded verification

First run pure contract/state tests against independent expected ranges. Then one owned Linux container under the S1 limits, loopback only, fake fixtures and no secrets; at most 15 minutes per browser case set. Reuse installed browser tooling or owner-assisted browser verification; installing automation is outside this allowlist. Stop before browser acceptance if no reliable interaction/evidence channel is available.

Real browser acceptance must record x/y ranges before/after pan/zoom, changed refresh, unchanged/no-redraw, overlay off/on/off, preset and instrument round trip; partial/complete retention expiry; correction/gap; date/generation rebuild; remount and stale event rejection; two sessions. Explicitly distinguish intentional Return to Live/preset resets. Include trace-state proof, request counts, command/ack/mount identity and screenshot paths where available. No click-only passes, server AppTest substitutions or invented timing results.

Compare native and component startup/interaction/memory costs, with actual browser and server preparation measured separately. Do not turn provisional targets into passed SLAs. Any mismatch or need to expand scope remains an owner decision; source and execution authority must explicitly approve this list and bounded procedure before work starts.

All proposed text/source/scripts must be UTF-8/LF. Independently check CRLF/lone CR and `git diff --check`; any Windows host script must support Windows PowerShell 5.1.26100.9444. Preserve existing owner files and production behavior. No provider calls, Azure activity, deployment, issue closure or automatic merge. `PUBLIC_RUNTIME_VERIFIED=False`.
