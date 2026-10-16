# C2B-23 — Final Browser Acceptance of Existing Chart + SMA(20)

## Owner authority and decision
The owner **approves accepting the known zoom/pan reset on SMA(20) toggle as a documented UX limitation** for this local-only Release 1.14 showcase. Do not pursue compatibility fixes, new frameworks, custom JavaScript/DOM workarounds, or Streamlit patches. This authority is **verification and documentation only**, except for the smallest directly necessary correction if a genuine SMA toggle regression is reproducibly found; report any such correction before expanding scope.

Work on existing **PR #345**, which was reported OPEN/UNMERGED at `4b9b357b3728f08f8d569eb35323b50a66253a1b`. Verify live head/base/status and file inventory before work. Do not merge or deploy. Preserve all owner untracked documents, protected WP07/archive, and existing unrelated processes. Do not alter frozen E4 generator/calculator or public readiness.

## Task
1. Run the documented local-only C# loopback data source and existing Streamlit preview on Windows/Development/loopback with `AIQ_LOCAL_CHART=1`, following the exact commands and safety gates in PR #345. Avoid ports already in use and stop only processes you own.
2. In a real browser, verify **both 1D and 30D** views: original Plotly candlesticks, volume, selectors, hover, modebar/zoom/pan, and native Technical Analysis popover. Explicitly prove SMA(20) **off → on → off** by observing a visible SMA trace appear and disappear; do not count a click as successful unless the resulting state changes. Capture evidence before/after, with selected range and trace state. If automation fails to click, retry via an accessible supported browser interaction or report INCOMPLETE; do not infer success.
3. Verify C#-provided SMA(20) data remains the only analytics source, with warmup hidden; do not recompute indicators in Python/JavaScript. Confirm public `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators` return **503 DATASET_NOT_READY**, accounting for existing rate limiting when sequencing requests. `PUBLIC_RUNTIME_VERIFIED=False` remains unchanged.
4. Record zoom/pan reset on SMA toggle as an **owner-accepted known UX limitation**, referencing earlier observed evidence; do not claim C2B-22's failed automated click reproduced the reset. No need to fix or independently re-prove zoom retention. Check that zoom/pan still work before/after toggle.
5. Run only proportionate focused checks if needed, record commands/results honestly; avoid broad requalification. Document browser evidence and any outstanding gaps in PR #345. If all functional smoke criteria pass, mark **local showcase acceptance recommended, with the documented zoom-reset limitation**. Do not imply public production readiness.

## Stop conditions
If the SMA toggle cannot be reliably demonstrated on both 1D and 30D, or the original chart controls regress, report **BLOCKED/INCOMPLETE** with exact steps/evidence. Do not silently waive functional failures. Do not introduce unsupported browser/library workarounds. Seek owner review before any meaningful implementation change or scope expansion.

## Deliverable
Update the existing PR #345 description/comment with a concise evidence table for 1D/30D, SMA off/on/off, original chart controls, public 503, known limitation, changed-file inventory and test scope. Include screenshot paths or attached evidence where practical. Leave PR **OPEN and UNMERGED**; return head SHA and acceptance recommendation. No Azure activation/deployment, provider integration or WP07 work; public activation remains **NO-GO**.
