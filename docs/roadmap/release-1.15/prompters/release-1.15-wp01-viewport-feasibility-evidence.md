# Release 1.15 WP01 S1 - Native viewport feasibility evidence

Date: 2026-10-22. Issue #367. Base: `455b80d0e50c17791653457a4ebd3001d3629a6e` (merged planning PR #378).
Owner authorized the exact-path S1 experiment after planning merge. Subsequent owner reply authorizes a separate component proposal only, not component implementation.

**Verdict: BLOCKED / browser acceptance INCOMPLETE.** Native state fixtures and server rendering work; a supported native relayout channel was not established. No zoom-retention waiver is applied. WP01 remains OPEN; WP02 is not advanced.

## Exact changed paths

Five new experiment files, no application changes:

- `tests/feasibility/release_1_15/viewport/fixture_snapshots.py`
- `tests/feasibility/release_1_15/viewport/viewport_state.py`
- `tests/feasibility/release_1_15/viewport/test_viewport_state.py`
- `tests/feasibility/release_1_15/viewport/app.py`
- `tests/feasibility/release_1_15/viewport/Dockerfile`

This evidence file is the sixth S1 path. The separately requested [component proposal](release-1.15-wp01-supported-viewport-component-proposal.md) is documentation only. Existing chart, production Dockerfile, dependencies and frozen contracts are unchanged.

## Commands and observations

`python -B -m unittest discover -s tests/feasibility/release_1_15/viewport -p test_viewport_state.py -v`: **9 passed**, exit 0. Cases: absolute x/y preservation on reconciliation; partial retention overlap; fully expired range without silent Return to Live; stale mount; stale revision; inverted ranges; explicit Return to Live; session independence; 1,000-row/gap fixture identity. These tests exercise only the small pure model, not browser event capture or the full Release 1.15 client.

Build: `docker build --pull=false --network=none --progress=plain -f tests/feasibility/release_1_15/viewport/Dockerfile -t aiq-r115-wp01-viewport:local .`: exit 0. Existing local base image resolved to `sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed`. The Dockerfile uses a local tag, so reproduce only after checking that exact ID. No package installation or source-provider call occurred. Docker engine 29.7.2; Linux/amd64; installed assertions confirm Streamlit 1.61.1 and Plotly 7.1.0.

Built local image ID: `sha256:4517bc58849c5129c9ea635d0967c62cd7f93507eabdc28f0dc60a615d1a4f95`. This is a local experiment, not a registry publication or release candidate.

Started one label-owned container with read-only root, 1 CPU, 512 MiB, 64 PIDs, 32 MiB temporary filesystem and dynamically allocated loopback host port. `/_stcore/health` returned 200. No credentials, private mounts or real data were supplied; telemetry disabled. The fixture exposes 100/300/1,000 presets and baseline/change/correction/gap/pruning/expiry/generation/date choices.

Installed Streamlit AppTest executed the app inside that Linux image and set its fixture-overlay checkbox false/true/false. Actual Plotly trace collections were:

| State | Server-generated traces | Result |
| --- | --- | --- |
| Off | Fake OHLC, Fake volume | PASS |
| On | Fake OHLC, Fake volume, Fixture overlay | PASS |
| Off again | Fake OHLC, Fake volume | PASS |

The overlay is deliberately a fixture trace, not calculated SMA research. No indicator was recomputed or implemented. AppTest is supported server-side testing; it is **not a real browser**. Its expected bare-mode ScriptRunContext warning does not establish browser rendering. No screenshot or x/y browser range was captured.

## Native API finding and unexecuted cases

The inspected pinned `st.plotly_chart` signature exposes selection callbacks, not `on_relayout`. Installed implementation includes figure specification in element identity (`key_as_main_identity=False`). The returned native event is selection state. Plotly.js documents relayout events for an owned chart, but that does not prove native Streamlit forwards them. Stable key/uirevision alone is not accepted as a solution.

No installed Playwright/Selenium package or connected browser-control tool was available. No package/browser was installed. Therefore actual x/y preservation, user pan/zoom/hover/modebar, two real sessions, remount/correction/date/generation behavior and independent visible toggles remain **NOT RUN / INCOMPLETE**. Request coalescing, programmatic echoes and the complete command-acknowledgment protocol are not implemented by this small state model. Do not turn nine model tests into those missing proofs.

This is a native API feasibility gap plus incomplete browser evidence, not a claim that a failed automated click reproduced a viewport reset. The supported alternative is documented separately for owner review. No custom component, page JavaScript/DOM workaround or Streamlit patch was implemented.

## Preservation and cleanup

The label-verified container stopped with exit 0 and no OOM, then was removed. Its experiment image tag was removed after recording its ID. Pre-existing images/containers and owner worktrees were not cleaned or modified. All new text is UTF-8/LF; independent CR scan and `git diff --check` are required before publication.

`PUBLIC_RUNTIME_VERIFIED=False`. No deployment/public activation; no WP01 acceptance or issue closure.
