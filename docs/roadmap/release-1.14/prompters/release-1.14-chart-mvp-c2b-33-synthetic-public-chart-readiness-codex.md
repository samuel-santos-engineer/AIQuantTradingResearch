# C2B-33 — Synthetic public chart readiness (Docker-first, local-only)

## Authority and product boundary

Owner requests the next bounded Codex milestone following the merge of governance PR #352 (`ce7afdde4febea8abe30cd2be4873fef29aeb18e`). This document authorizes **local-only inspection, scoped implementation if necessary, qualification, and a review PR**. It does **not** authorize merging PRs, publishing another image, deploying to Azure, enabling public APIs, promoting datasets, or relaxing governance.

**Authoritative objective:** “A publicly accessible, educational synthetic crypto-chart application—not a live or real historical market-data platform.” Read and obey `docs/governance/SYNTHETIC_PUBLIC_PRODUCT_SCOPE.md` and `docs/governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md` from current `main` before changing anything. C2B-32 is ABORTED; C2B-31 staging is DEFERRED. **No web searches for quote providers, licensing investigations for real candles, live/exchange/Twelve Data APIs, or real historical data ingestion.** All public-facing output must be unmistakably labeled simulated/synthetic. BTC-like synthetic showcase only; do not invent or add ETH-like data without separate owner approval.

## Evidence baseline (verify rather than assume)

- C2B-29 PR #350 is an open/unmerged local Linux candidate with 12 synthetic OHLCV/SMA selections, a locally accepted 1D/30D Plotly chart, focused tests and read-only/non-root startup evidence. It cherry-picked #347; reconcile overlap before editing, do not silently merge either PR.
- C2B-30 PR #351 is open/unmerged evidence for the exact image published to public GHCR: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:55d142b57d0df9953f01350263369ee2ecd9a66019d1844d1f424ea91e0999d5`. This is an immutable **historical candidate identity**, not authority to deploy or modify it.
- #346, #347, #349, #350, #351 may remain open; verify statuses and exact changed-file overlap before proceeding. Owner files and original worktrees must be preserved.
- Local public APIs historically return `503 DATASET_NOT_READY` by design; deployed Azure API paths historically returned `200 HTML` SPA fallback. Neither fact authorizes activating APIs or changing their contracts.
- `PUBLIC_RUNTIME_VERIFIED=False`, Azure deployment/activation NO-GO, advanced WP07 ON HOLD. G-B0–G-B7 remain unchanged.

## Goal

Make the **existing synthetic chart experience** unambiguous, coherent, and ready for a future owner decision about public accessibility, without reopening the market-data source problem or changing the deployment state. Prefer the smallest viable code/documentation delta; do not create another service or rewrite the original Streamlit/Plotly chart.

## Execution sequence

### 1. Read-only inventory and gap assessment

1. Fetch latest `main` and relevant PR metadata/patches; identify which branch contains the currently qualified chart, immutable reader, gate, startup and labels. Record commit IDs, exact paths and dependencies. Avoid broad merges and accidental duplication of #347 within #350.
2. Inspect existing UI for explicit **SYNTHETIC / SIMULATED — NOT REAL MARKET DATA** labeling in chart title/subtitle, instrument selector, tooltips/hover, documentation and any exported/downloaded outputs. Check for misleading BTC/USD, ETH/USD, live/real-price language, timestamps or implied exchange attribution.
3. Inventory the actual synthetic user journey: chart loads, 1D/30D and other supported ranges/timeframes, volume, native TA popover, C# SMA20 off→on→off, hover, zoom/pan, toolbar, clear unavailable states. Preserve the owner-accepted zoom reset and do not add unsupported instruments.
4. Identify whether the existing local-only candidate eligibility gate already meets the goal. **Do not interpret “publicly accessible” as approval to remove gates or activate data APIs.** If an actual public-mode chart would require a new publication/promotion decision, document that as a separate future authority and keep it disabled.
5. Produce a short, concrete gap matrix: already satisfied / safe local-only change / blocked pending owner authority. If no code changes are needed, submit evidence-only PR and stop.

### 2. Narrow changes only where justified

- Permit only non-deceptive synthetic labels, chart copy, instrument naming, help text, accessibility text, and focused tests/docs that do not alter market-data provenance, release eligibility, security gates or API semantics.
- Preserve the existing Plotly candlesticks/volume, selectors, hover/modebar and offline C# SMA20 immutable values. Do not replace the chart or compute indicator values in Python/JavaScript.
- No live C# worker, no new runtime service, no provider calls, no new external dependencies, no private replay, no provider fallback, no new instrument series, no mutable runtime dataset downloads.
- Do not change the certified/published image bytes or assert its digest certifies a modified source tree. Any source changes require a new future image build/qualification under separate authority.
- No exposing private assets, warmup rows, source artifacts, manifest paths or secrets through UI, HTTP, logs or exports.

### 3. Local Linux Docker qualification

- Run against a local Linux Docker image built from the exact reviewed branch; identify it as a **new local screening image**, not the already-published digest if source differs. No Azure mutation, GHCR push, Windows-native certification or extra cloud resources.
- Exercise startup/health, default unavailable public API contract (`503` JSON for dataset/candles/indicators, `404` JSON for unknown API), no accidental synthetic-data activation, and non-root/read-only filesystem behavior where applicable.
- For explicitly authorized local candidate mode only, browser-check visible synthetic labels, 1D/30D SMA off→on→off, volume, selectors, hover, zoom/pan, toolbar; compare all displayed SMA values against pinned immutable C# artifacts. Reuse historical test evidence only when exact image/content and scope genuinely match; otherwise rerun.
- Run focused Python/.NET tests and negative integrity/ownership checks for any touched paths; record exact commands, hashes, image IDs, screenshots and resource observations. Treat any missing browser access as INCOMPLETE, not PASS. Clean only owned resources.

### 4. Review artifact and stopping conditions

Create a **scoped PR left OPEN/UNMERGED** against `main` containing only necessary changes and a concise evidence report under `docs/roadmap/release-1.14/prompters/`. Include changed-path inventory, provenance, labels screenshots, checks/tests, outstanding gates and follow-up decision. Preserve all other PRs and owner files. If new changes conflict with open PR #350, stop and propose a safe dependency/rebase plan rather than copying or overwriting unmerged code.

Stop immediately if the task requires real market data, expanding to ETH-like synthetic instruments, public dataset/API activation, export/promotion, Azure deployment, GHCR push, relaxing gate G-B0–G-B7, or merging another PR. Escalate those as **separate owner decisions**.

## Required final report

Report PR URL/head/status, exact changed files, local image identity, chart and test results, remaining gaps, cleanup, and explicitly: `PUBLIC_RUNTIME_VERIFIED=False`; Azure deployment/activation **NO-GO**; advanced WP07 **ON HOLD**. Do not imply the published C2B-30 digest includes any newly proposed UI changes.
