# C2B-22 — PR #345: Minimal zoom/pan retention correction

Date: 2026-10-16. Owner-authorized narrow local-only correction. **AIQuantTradingResearch is a showcase lab, not a production trading system: favor the smallest working solution.**

## Baseline and scope guard

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`; existing PR [#345](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/345).
- Expected open/unmerged head before changes: `4b9b357b3728f08f8d569eb35323b50a66253a1b`; base: `3a4e6ef0e8a0ddf77776801cf87907927d8abc31`. Recheck both, worktree, diff and owner-file preservation; STOP if unexpected.
- Continue on the **same PR**, do not merge it or create another PR. Preserve all six existing PR paths and restrict edits to the minimum existing Streamlit/Plotly presentation files required for this fix. Any new file or dependency requires owner approval.
- Preserve original Plotly candles, volume subplot, interval/range selectors, UTC axis, hover, zoom, pan, modebar and native **Technical Analysis → SMA (20)** popover; verified C# analytics and local adapter remain intact.

## Authorized investigation and fix

1. Reproduce the observed failure in the actual installed Streamlit 1.61.1 local preview: pan/zoom the 30D chart, toggle SMA(20), observe viewport reset. Record initial and resulting x/y axis ranges without exposing private data.
2. Inspect the installed public Streamlit/Plotly APIs and current chart call. Prefer a **small supported fix** that keeps the rendered element identity stable when SMA visibility changes. Stable `uirevision`, `key` and trace UIDs alone have already failed because Streamlit includes the changing figure specification in element identity; do not repeat them as an unverified solution.
3. If stable identity cannot be achieved through supported APIs, try the smallest **supported** viewport capture/restore mechanism available in the current dependencies, with no custom component or new framework. Respect Streamlit rerun semantics and preserve independent user range selections.
4. Do **not** patch Streamlit internals, inject JavaScript, manipulate the DOM, introduce a custom modebar, replace Plotly, or add process/network infrastructure. Do not alter public routes, readiness flags, frozen datasets, indicators, provider integrations or Azure configuration.
5. Keep code minimal. If the supported options cannot preserve zoom/pan without disproportionate complexity, **STOP**, document the actual limitation and alternatives, and request owner disposition. Do not silently weaken the criterion or broaden scope.

## Focused local verification

- Use the already established Windows/Development/loopback-only host and Streamlit opt-in from PR #345; reuse installed offline dependencies and safe owned output paths. Do not change the local-only gate or expose a LAN/public endpoint.
- Verify in an actual browser, not just unit tests: select 30D, zoom and pan, record visible ranges, toggle SMA on then off, confirm x-axis **and applicable y-axis** viewport are preserved; repeat on 1D and after a normal range/interval selection. A user selection change may intentionally reset axes; an SMA toggle must not.
- Verify candles, volume, UTC labeling, hover, Plotly modebar, selectors and SMA toggle still work. Run only focused relevant .NET/Python tests if needed, using offline/no-restore constraints. Public `/api/v1/dataset`, `/candles`, `/indicators` must still return 503 (respect existing rate limits).
- Keep owned processes and temporary files bounded and clean up only confirmed owned resources. Preserve untracked owner documents, protected WP07/archive and unrelated running processes.
- Report exact changed files, commands, PASS/FAIL/BLOCKED, before/after viewport evidence, test results, any warnings and remaining limitations. Push minimal changes to PR #345 **only if a supported fix is verified**; otherwise report BLOCKED without speculative changes.

## Non-negotiable boundaries

`PUBLIC_RUNTIME_VERIFIED=False`; public activation/deployment **NO-GO**; advanced WP07 **ON HOLD**. No public data enablement, snapshots/export, package installation, provider calls, production launch, merge or deployment. This authority is for the existing local showcase chart's zoom/pan behavior only.

**Success:** Original chart remains unchanged except for the Technical Analysis popover and its SMA overlay; toggling SMA(20) does not reset the user's zoom/pan. If no proportionate supported solution exists, provide evidence and seek owner acceptance of the limitation rather than overengineering.
