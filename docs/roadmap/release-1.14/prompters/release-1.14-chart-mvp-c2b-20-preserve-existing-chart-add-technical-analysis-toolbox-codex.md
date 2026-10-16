# C2B-20 — Revise PR #345: preserve existing chart and add one Technical Analysis toolbox control

**Owner direction (2026-10-16):** AIQuantTradingResearch is a showcase lab, not a production trading platform. Ship a useful demonstration without replacing working UI or introducing unnecessary infrastructure.

## Authorization and baseline

- Work on existing **open, unmerged PR #345**, baseline `3a4e6ef0e8a0ddf77776801cf87907927d8abc31`, reviewed head `1ff5c9515416edda196b89bc475829cd5972782c`. Recheck live refs before editing. **Do not merge** the PR.
- Owner requests a correction to C2B-19's user-interface direction: **preserve the existing Plotly/Streamlit chart, its design and functionality, and add one Technical Analysis icon to its toolbar**. Do not adopt the new standalone HTML/SVG page as the primary chart.
- Existing screenshot shows the range dropdown (30D), OHLC candles and volume, hover information, and Plotly modebar controls (zoom/pan/reset/export and related functions). Treat these as regression-protected behavior, not features to rebuild.

## First inspect; then make the smallest change

1. Locate the existing chart view, Plotly construction and modebar customization, frontend dependencies and existing local/deployed launch paths. Determine whether a **single new Plotly modebar button** can open a compact anchored popover/menu without disturbing default controls. Prefer native features and existing libraries; avoid new frameworks, custom chart renderers, new processes, complex state or architectural layers.
2. If an accessible, supportable anchored modebar popover cannot be added within the existing technology, use the closest minimal **toolbox-adjacent icon button and tooltip/popover**, visually associated with the Plotly toolbar. Do not silently replace the chart or remove any modebar controls. Explain the technical constraint in the PR.
3. Menu title: **Technical Analysis**. First and only enabled tool for this milestone: **SMA (20)**, as a checked/unchecked toggle. Initial visibility may be on or off according to existing chart behavior, but toggling must visibly show/hide exactly the SMA20 overlay without resetting the selected range, OHLC/volume display, axis, hover behavior or zoom/pan state. Include keyboard accessibility and readable label/title; dismiss menu by outside click or Escape where naturally supported.
4. Use **verified existing C# synthetic calculation** and its 250 hidden warmup candles for the SMA, not JS/Python recomputation. Preserve UTC alignment and overlay continuity for existing ranges. Do not leak the 250 warmup rows to the visible chart. If the existing UI cannot currently consume that C# result locally without invasive wiring, stop and report the smallest feasible integration change rather than fabricate values or expose public data routes.
5. **Do not remove other current capabilities**, including existing range selector, OHLC and volume rendering, zoom/pan/hover controls, layout, chart responsiveness or unrelated navigation. Keep the original visual style and controls. Preserve existing real/private provider boundaries and source provenance; no provider data should be copied to public synthetic routes.
6. The standalone `LocalSyntheticChart.cs` SVG/HTML route from the initial PR is a **replacement UI**, not the requested feature. Remove or refactor its replacement-only code as appropriate. Retain only lightweight pieces genuinely needed to demonstrate the SMA menu in the existing chart. Avoid duplicate chart implementations.

## Scope, verification and safety

- Favor edits inside the existing UI/chart files and the smallest necessary local synthetic data adapter/tests. Modify `Program.cs` only if necessary. No new chart framework, native containment, third-party data provider, paid resource, packages, schema changes, snapshots, exporter or production hosting rework.
- Perform a local-only focused build/test and **browser visual regression check**: previous chart remains recognizably the same; all existing controls still work; added TA icon opens a compact menu; SMA20 on/off changes only the overlay. Check 1D and 30D if both are supported by the existing UI; do not remove ranges to make this simpler.
- Do not claim exact C# SMA overlay verification unless the tested UI path truly uses the C# calculator. Record actual tested scope/results and any blockers. If wiring the proven C# indicator into the old UI requires a new boundary, flag the smallest alternative for owner review before expanding scope.
- Preserve all local/untracked owner documents, protected WP07 worktree/archive and unrelated changes. No destructive cleanup. Public `/api/v1/dataset`, `/candles`, `/indicators` must remain 503 `DATASET_NOT_READY`; `PUBLIC_RUNTIME_VERIFIED=False`; Azure activation/deployment **NO-GO**; advanced WP07 **ON HOLD**.
- Update the **same PR #345**, not a new PR, with focused change summary, old/new behavior, screenshots or browser evidence, commands and test results, any technical constraints and exact new head SHA. Keep PR **open and unmerged** pending owner acceptance. If integration cannot meet the preservation requirement safely, stop with a concise blocker and options; do not substitute a different interface.

## Acceptance criterion

**The existing chart looks and works as before, with just one additional Technical Analysis toolbox icon. Clicking it opens a tooltip/popover menu with SMA (20), and toggling SMA20 adds/removes the verified indicator overlay without altering the chart's other features.**
