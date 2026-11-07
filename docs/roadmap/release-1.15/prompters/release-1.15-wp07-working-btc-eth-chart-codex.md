# Codex execution prompt — Release 1.15 WP07: working BTC/ETH historical chart

## Authority and objective

Baseline: merged PR #398, main squash commit `00e8b52bb93edfb8f418808705e414a61d9105f2`. Deliver the shortest safe implementation path to a **working user-visible historical BTC/USD and ETH/USD candle chart** using the existing Streamlit UI, C# research snapshot service, and authoritative SQLite research state. Create one focused PR for owner review; do not merge it.

Owner decisions are final for this increment:
- SQLite remains the sole authoritative datastore. Accept the completed local SQLite/WAL, process-crash recovery, D5 witness, and same-volume writer-exclusion qualification from PR #398. Do not rerun or expand general SQLite durability research without a specific regression.
- Exactly one research writer/process is needed. No pooling workstream, distributed coordination, multiwriter support, cross-host fencing, automatic failover, or host-power-loss testing.
- A brief research-service interruption during controlled stop-before-start replacement is acceptable for this milestone; the UI should recover when the research service returns.
- Focus on delivering the chart. No new governance, licensing or approval gates.

## Required approach

1. **Inspect actual main and current architecture first.** Confirm merged #398 behavior, WP07 runbook, existing Azure staging resources, C# research API and initializer, Streamlit HTTPS snapshot client, Docker qualification, and any current blockers. Preserve the owner's dirty/untracked checkout byte-for-byte using an isolated worktree. Do not import unrelated open PRs #386/#387.
2. **Choose the smallest viable operational topology.** Prefer existing owned infrastructure only where its filesystem genuinely preserves the SQLite DB, WAL, SHM, lock, and D5 witness across ordinary container/app restarts. Do not assume Azure App Service F1 `/home` network storage is compatible with SQLite WAL; do not bypass `ResearchStorageBoundary` or strip Azure markers to make an unsupported setup appear to work. If no existing suitable storage is available, identify a concrete minimal single-instance SQLite-compatible hosting arrangement (for example an already-owned Linux host with persistent local/block storage), with factual costs/changes and the precise owner authorization needed. Do not provision a paid VM/disk or silently change hosting topology.
3. **Implement only the chart-delivery critical path.** Wire the existing completed-minute BTC/ETH ingestion/operational initializer, single authoritative SQLite state, authenticated research snapshot endpoint, and Streamlit historical candle/indicator UI. Preserve server-side HTTPS hostname/CA verification and `X-API-Key` handling. Keep secrets, provider credentials, HMAC key, raw DB, and raw observations off the browser. Avoid test/synthetic candles in public staging. The source of real completed observations and its acquisition authorization must be stated factually; do not invent or fetch provider data without explicit authority.
4. **Keep one writer and simple lifecycle.** Define an operator-controlled stop-old / verify-stopped / start-new procedure, persistence checks across normal restarts, and a rollback that never overwrites the authoritative SQLite lineage. No distributed lease or pooling implementation. Preserve fail-closed storage checks where hosting is unsupported.
5. **Re-use existing qualification.** Run focused regression tests for changed code and the existing local dual-container/browser acceptance path as appropriate. Verify BTC and ETH selection, historical completed candles, indicators, gaps, unavailable/reconnect behavior, and absence of browser-to-provider traffic. Do not repeat the PR #398 fault matrix or add host-power-loss tests. Distinguish local evidence from actual Azure staging evidence.
6. **If deployment is technically blocked, still deliver useful progress.** Make the smallest reviewable code/runbook changes, provide an executable operator checklist and exact remaining blocker/decision, and stop. Do not claim a live working Azure chart without actually observing it in staging.

## Existing staging context

Existing UI App Service `aiqr112wp05wus27f5eabb5` on shared Linux F1 plan `asp-aiq-r112-wp05-wus2-7f5eabb5` (resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, West US 2); research App Service shell `aiqr115researchstagingwus27f5eabb5`. Earlier read-only inventory showed legacy UI image, empty research shell, no research persistent mount, and research `httpsOnly=false`. Reinspect read-only before relying on these observations. The PR #398 storage boundary intentionally rejects writable research startup on unsupported App Service hosting. Do not undo that safety behavior merely to demonstrate a chart.

## Explicit exclusions / authorization boundary

No Azure mutations, deployment, image registry push, paid resource creation, secret retrieval, provider activation/live provider API calls, production changes, or issue closure without the owner's separate explicit authorization. Do not alter the D5 algorithm, SQLite schema/journal mode, pinned packages, established two-service interface, or default local HTTPS unnecessarily. Do not set `PUBLIC_RUNTIME_VERIFIED=True` unless actual authorized public staging/browser qualification succeeds; otherwise keep it `False`.

## Deliverables

- One narrowly scoped PR based on the merged #398 baseline, OPEN and UNMERGED, with exact changed-file inventory.
- Working local BTC/ETH historical chart acceptance evidence using the real C# research service and Streamlit browser, clearly identifying disposable local fixtures versus real public data.
- Minimal, explicit operator path to obtain authorized real completed observations, initialize/persist SQLite, configure HTTPS research access, launch the chart, restart safely, and verify both instruments.
- If current hosting remains incompatible, a concise decision note giving the **smallest viable single-instance persistent SQLite host** and exact deployment authorization needed, not another generic durability investigation.
- Tests/results, known limitations, preserved checkout evidence, and accurate `PUBLIC_RUNTIME_VERIFIED=False` status until actual staging verification.

Do not ask for more planning questions unless a truly irreversible choice requires owner authorization. Execute the safe local work, open the PR, and report the next concrete action needed to get the chart online.
