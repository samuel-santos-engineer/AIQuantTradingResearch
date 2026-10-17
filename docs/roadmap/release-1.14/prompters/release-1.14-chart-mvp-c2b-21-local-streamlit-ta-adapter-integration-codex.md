# C2B-21 — Revise PR #345: preserve Plotly chart, integrate local synthetic TA

**Owner authority:** 2026-10-16. **Scope:** implementation, focused local tests and browser verification only. This is a showcase lab, **not a production trading platform**. Prefer the smallest working change; do not add governance, infrastructure, new frameworks or production-hardening layers.

## Baseline and guard

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Target: existing **open, unmerged PR #345**. Expected pre-change head `1ff5c9515416edda196b89bc475829cd5972782c`; base `3a4e6ef0e8a0ddf77776801cf87907927d8abc31`.
- Fetch and verify live PR state/head/base before modifying. If changed, STOP and report rather than overwrite work. Preserve all owner/untracked files, protected WP07/archive and private-provider data. Do not merge PR #345.
- The screenshot of the existing Azure Streamlit/Plotly chart is the visual/functionality reference, **not permission to deploy or activate public data**.

## Required result

Keep the **existing Streamlit/Plotly UI** and its current layout/behavior: OHLC candlestick trace, volume subplot, range selector (including 30D), hover OHLC values, UTC time axis, camera/zoom/pan/reset/fullscreen tools, and other existing controls. **Do not replace it with an SVG or a separate chart application.** Add only one toolbox-adjacent Streamlit-native **Technical Analysis** popover, with an **SMA (20)** checkbox/toggle. Toggling displays/hides a line overlay calculated by existing verified C# indicator code. Preserve zoom/pan across toggle rerenders via stable Plotly `uirevision` and stable trace/axis identity as appropriate.

## Explicitly authorized local-only integration

1. Inspect the existing Streamlit chart composition, launch path, data bridge and tests. Reuse existing code and visual structure; keep changes minimal.
2. Refactor the C2B-19 C# **local-only** endpoint to return visible synthetic **OHLCV** and C#-calculated **SMA(20)** for the existing chart selections that the frozen synthetic contract actually supports (1h/4h/1d × 1/7/30/90 days). Preserve 250 hidden warmup rows for calculations; do not expose warmup rows in visible responses. Reject unsupported selections clearly rather than inventing data. Keep deterministic fixed UTC historical coverage, synthetic labeling and provenance.
3. Implement a narrowly gated **development-only Streamlit adapter** that obtains these values from the loopback C# endpoint. Reuse the existing chart rendering and volume logic. Explicit local launch/opt-in only; never silently switch the deployed/public chart to this source. Avoid the held Linux/cgroup execution bridge for this local demonstration; do not weaken or bypass its restrictions.
4. Remove the replacement standalone SVG chart page from PR #345. Retain only the minimal local endpoint/launch code genuinely needed by the Streamlit adapter; do not introduce an additional frontend, worker service, process supervisor or dependency unless already available and necessary.
5. Place the native Streamlit TA popover adjacent to the existing chart toolbox as closely as supported by Streamlit **without fragile DOM injection or custom Plotly modebar JavaScript**. If embedding a true icon inside Plotly's modebar is unsupported without invasive changes, use an immediately adjacent icon/popover and document that limitation. Default SMA visibility should preserve the existing chart appearance until enabled unless existing behavior dictates otherwise.
6. Keep public `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators` at their existing `503 DATASET_NOT_READY` boundary. No changes to `PUBLIC_RUNTIME_VERIFIED=False`, private Twelve Data pathways, licensing, Azure configuration, deployed Streamlit startup, production runtime or WP07.

## Verification permitted

- Run only focused builds, tests and local browser checks needed to prove: local gate rejects production/Azure-marked launches; loopback adapter is not reachable as a public source; synthetic OHLCV/range/SMA values align with C# calculator; all existing chart controls, volume and hover remain functional; toggling SMA does not reset user zoom/pan; public routes remain 503.
- Use installed tooling and existing test facilities. No new Job Objects, native harness, package downloads, paid resources, cloud deployment, broad test matrix or unbounded experiments. Keep temporary outputs owned and clean up safely; do not touch unrelated files.
- If the Streamlit local adapter cannot be integrated without violating a public/private or held-bridge boundary, STOP and report the exact blocker with the smallest alternative. Do not silently widen authority.

## Delivery

- Update **the existing PR #345**, leaving it open/unmerged. Keep the diff minimal and limited to the local endpoint, Streamlit chart/adapter and focused tests actually needed; list every changed path.
- Provide the new head SHA, concise description, exact local Windows PowerShell launch steps for **both** C# loopback source and Streamlit UI, test/build results, screenshot or browser evidence, public-route status and any limitations.
- Success = the **original Plotly chart still looks and behaves as before**, with a single additional Technical Analysis popover whose SMA(20) toggle overlays the verified C# line. No public activation is implied.

**Non-negotiable state:** `PUBLIC_RUNTIME_VERIFIED=False`; Azure deployment/public activation **NO-GO**; advanced WP07 **ON HOLD**.
