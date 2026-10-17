# C2B-24 — Azure-Compatible Synthetic Chart Runtime (Planning + Bounded Implementation)

## Owner authority and objective
Owner approved the **next phase** after PR #345 was squash-merged to `main` at `33b941c28b0158810b4f406db965bf83ff92244a`. Deliver the smallest **Azure-compatible** path for the existing Streamlit/Plotly synthetic market chart and verified C# analytics, targeting the two existing Azure App Service sites:

- `https://aiqr112wp035ec325382770.azurewebsites.net/`
- `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/`

**This is NOT authority to deploy, publish data publicly, change readiness flags, or merge a PR.** Build and test a bounded candidate, then request owner approval for the deployment gate.

## Existing accepted behavior to preserve
- Preserve the established white Streamlit/Plotly interface: candlesticks, volume panel, 1h/4h/1d interval and 1D/7D/30D/90D selections where qualified, hover, zoom/pan, Plotly modebar, and the native **Technical Analysis → SMA (20)** popover.
- SMA(20) must use the existing qualified C# calculation with 250 hidden warmup candles. Never recompute SMA in Python or browser JavaScript or reveal warmup rows.
- Keep the label **BTC-like Market Simulation — Synthetic Historical OHLCV** clearly visible. Do not imply live BTC prices, actual exchange data, or investment advice.
- Owner accepts the documented viewport reset when toggling SMA; do not reopen that issue or introduce a new UI framework.
- Preserve the deterministic dataset, frozen fingerprints, calculation version and existing qualified intervals/ranges. No Twelve Data, exchange feeds, external market-data licenses, or new market-data providers.

## Current boundary and problem
PR #345's local integration is intentionally Windows/Development/127.0.0.1-only: `python/presentation/local_synthetic_chart.py` and `src/AIQuantTradingResearch.Worker/LocalSyntheticChart.cs`. Azure App Service cannot use that mode as-is. `PUBLIC_RUNTIME_VERIFIED=False`; the public `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators` routes are 503 `DATASET_NOT_READY`; Azure public activation/deployment is **NO-GO**. Advanced WP07 is **ON HOLD**. Do not reinterpret the owner's successful local screenshot as proof of public readiness.

## Required workflow
1. **Preflight:** Fetch latest `origin/main`, confirm merge commit and inspect actual deployment/startup architecture, Python Streamlit hosting and C# runtime availability on BOTH Azure sites. Inspect existing readiness requirements, workflows, secrets/configuration references and Azure constraints without disclosing secrets. Preserve all untracked owner files, protected worktrees, WP07/archive and unrelated branches. If the repository or environment cannot support a minimal path, stop and report a concrete blocker; do not invent an architecture.
2. **Choose one minimal supported deployment design** that can run the **existing C# deterministic generator/analytics** and **existing Streamlit UI** in Azure's actual Linux/runtime constraints. Prefer existing repository patterns and available platform facilities. Avoid new services, databases, queues, frontend rewrites, new data providers, unsupported cgroup bridge, or large dependency changes. Explain how the UI obtains C#-computed visible OHLCV/SMA without relying on the local Windows-only opt-in path. Preserve existing API semantics and error boundaries.
3. **Implement only a bounded candidate** on a new branch/PR (not a direct push to `main`). Explicitly separate the existing Windows local adapter from any Azure-compatible path. Require a server-side, non-publicly addressable data path where possible, bounded timeouts/payload sizes, fixed allowed selections, strict deterministic provenance/row-count validation and no user-controlled arbitrary upstream URL. Preserve `PUBLIC_RUNTIME_VERIFIED=False` and public 503 routes until independently qualified through existing project readiness rules. Do not expose internal source ports, synthetic data routes, or production data just to make the chart appear.
4. **Test:** Run targeted C# and Python tests for all supported selections, warmup and SMA identity, adapter fail-closed behavior, default public 503 responses, absence of private/provider access and preservation of existing chart controls. Where feasible, run a Linux/container-like local smoke test and a browser check for 1D/30D SMA off → on → off. Report any test not run; do not claim a browser pass from unit tests or a screenshot showing SMA-on only.
5. **Deployment assessment (no deployment):** For EACH Azure URL, record required startup command/runtime/environment changes, hosting feasibility, permissions/credentials needed, rollback strategy and a checklist to qualify readiness. Confirm whether both sites are intended to host UI or if one is an API site; do not assume they have identical roles. Public route changes and `PUBLIC_RUNTIME_VERIFIED` require separate explicit owner authorization after evidence review. Do not use GitHub Actions, Azure CLI, portal, or deployment slots to publish during C2B-24.
6. **Deliver:** Open a PR with concise design rationale, exact changed-file inventory, tests and results, local demonstration instructions, Azure deployment plan for both sites, risks and explicit remaining owner decisions. Leave PR **OPEN/UNMERGED**. If a safe minimal candidate is not possible, produce a planning-only PR or blocker report instead of forcing implementation.

## Non-negotiable boundaries
- No deployment, Azure configuration mutation, production activation or merge in this authority.
- No bypass of `PUBLIC_RUNTIME_VERIFIED=False`, no change to public 503 contracts without separate authorization.
- No modification of qualified synthetic algorithms, snapshots, E4 frozen evidence, Twelve Data/provider integrations, or advanced WP07 lifecycle.
- No private secrets in source, logs, PR descriptions or test artifacts.
- No extra framework, chart replacement, unsupported DOM/Streamlit patches or unnecessary hardening infrastructure.

## Acceptance for C2B-24
A reviewable, tested Azure-compatible **candidate** and an evidence-backed, site-specific publication plan; the existing local showcase continues working. This phase is **not** accepted as public publication. Stop and ask the owner if any prerequisite requires loosening the above gates.
