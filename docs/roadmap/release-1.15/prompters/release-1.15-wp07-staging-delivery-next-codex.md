# Codex execution prompt — Release 1.15 WP07: unblock Azure staging BTC/ETH chart

## Authority and baseline

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
Start from freshly fetched `main` containing merged PR #400, squash commit `2957c01d03fb48c306bd8d343bdcc424c44878ba`. Verify ancestry and current repository state. Preserve the owner's checkout and every dirty/untracked file byte-for-byte; use an isolated worktree. Open one focused PR for owner review. Do not merge your PR.

The owner requires **all evaluation of Azure runtime behavior to take place exclusively in the existing Azure staging environment**. Do not evaluate against production, unrelated Azure resource groups, subscriptions, or a newly invented staging environment. Local static/unit checks are permitted, but do not label local results Azure-verified.

The product goal is a working **historical BTC/USD and ETH/USD chart** on the existing staging UI, using authentic completed-minute candles, the existing C# research snapshot service, server-side authenticated HTTPS, Streamlit/Plotly, and one authoritative SQLite store.

## Accepted engineering decisions

- Reuse merged PRs #395–#400 and their qualified local chart, SQLite WAL, recovery, D5 witness, and single-volume writer evidence. No repeat general SQLite durability testing, pooling, distributed coordination, multiwriter/failover design, cross-host fencing, or host power-loss study.
- Single research writer; controlled stop, verify stopped, start; brief interruption acceptable. Preserve authoritative database, watermark, D5 tuple, and key epochs across ordinary restarts.
- Keep `ResearchStorageBoundary` fail-closed for unsupported Azure App Service SQLite writer storage. Do not remove Azure identity markers, claim F1 ephemeral storage is durable, or silently move SQLite to an unsupported network share.
- Keep the existing staging UI and F1 plan unless an exact owner-approved change is necessary. Never touch production.
- No new licensing prerequisite, approval framework, broad governance rewrite, speculative provider or storage abstraction, or documentation-only PR if implementation can move forward.
- `PUBLIC_RUNTIME_VERIFIED=False` until actual authorized staging deployment and browser acceptance succeed.

## Known staging inventory (reverify read-only, scoped)

- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`, West US 2.
- Existing staging UI: `aiqr112wp05wus27f5eabb5`, public `https://aiqr112wp05wus27f5eabb5.azurewebsites.net`, Linux App Service on F1, existing container.
- Existing staging research shell: `aiqr115researchstagingwus27f5eabb5`, no qualified image or persistent SQLite mount.
- Plan: `asp-aiq-r112-wp05-wus2-7f5eabb5` (F1).
- No separate persistent research compute/disk was identified **within this resource group**. This does not prove none exists elsewhere; do not search unrelated environments without authorization.
- No authorized authentic BTC/ETH completed-minute export was supplied. Existing fixtures are not real market observations.

## Deliverable: eliminate avoidable blockers and present one executable next step

1. **Reconcile existing implementation:** Inspect the merged runbook, initializer, `TwelveDataMinuteCandleSource`, ingestion service, research container and staging UI settings. Identify what is already implemented versus the smallest missing code/configuration. Do not repeat tests unrelated to changes.
2. **Real candle path:** Prepare a bounded, executable **operator-controlled** import/acquisition workflow using the existing source and initializer for completed UTC one-minute BTC/USD and ETH/USD observations, maximum 1,000 per instrument, preserving gaps and source provenance. Never fabricate or relabel fixtures. Do not access secrets or make live provider calls unless the owner has explicitly authorized the exact acquisition and credential channel. If blocked, produce a minimal exact approval request (symbols, maximum calls/credits, source, secret handling), not a new general licensing gate.
3. **Research host:** Determine the smallest feasible **single-instance** persistent local/block-backed Linux research host for the *existing staging environment*. First use only explicitly designated staging resources; do not assume the F1 research shell can host SQLite. If no qualifying host has been designated, provide **one concrete, region-specific** Azure staging VM + managed-disk proposal with SKU, approximate recurring cost, network/TLS/DNS requirements, and stop/start/rollback plan, clearly marked proposed/not provisioned. Do not create or purchase resources without the owner's explicit approval. Avoid broad comparisons and HA architectures.
4. **Implementation-first increment:** Implement only useful, reviewable repository changes that make the eventual staging deployment executable once the two inputs are approved: bounded operator scripts, safe staging config templates, preflight checks, precise commands, and targeted tests. Do not modify application code without a demonstrated defect. Do not add a second research writer or provider polling by default. Ensure scripts are nonmutating by default and fail closed; never log secret values, export rows, full container environments, or private configuration.
5. **Azure evaluation boundary:** Read-only management-plane checks may be performed only for the named staging resources. No Azure mutation, registry push, live data acquisition, public endpoint changes, secret retrieval, or deployment without explicit authorization. If authorization is already available in the active task, execute only the specifically authorized staging operations and record actual browser/TLS/restart results; otherwise mark them NOT_RUN and give the exact command sequence for later approval.
6. **Acceptance criteria for a future authorized staging run:** Both symbols show authentic completed candles, retained/gapped intervals and existing C# indicators; HTTPS hostname/certificate and API key validation work server-side; no browser provider/API-key exposure; restart retains the same SQLite/D5 state; real Chrome chart selectors, interactions, stale/reconnect behavior pass against the **existing staging URL**. Test only what has actually been deployed; record failures honestly.

## Work hygiene and reporting

- Start by verifying PR #400 is merged at `2957c01d03fb48c306bd8d343bdcc424c44878ba` and preserve all owner files.
- Reuse pinned dependencies, current Docker images and accepted local evidence. Do not add packages, change schema, or repeat the SQLite fault matrix.
- Validate modified scripts and focused tests; enforce UTF-8 without BOM, LF endings, final newline, `git diff --check`.
- Open **one focused PR** with actual code/operational improvement, exact changed files, test commands/results, staging read-only findings, and clearly separated PASS_LOCAL / VERIFIED_AZURE_STAGING_MANAGEMENT / NOT_RUN statuses. Leave it **OPEN and UNMERGED**.
- Finish with **at most two precise owner decisions**: (A) authorize bounded real BTC/ETH acquisition or provide export; (B) designate an existing persistent staging research host or approve the exact proposed single-instance staging host/cost. Do not multiply gates.
- Never claim the public chart works until it has been actually tested in Azure staging. Until then report `PUBLIC_RUNTIME_VERIFIED=False`.

## Stop condition

If the only possible next action would require unapproved provider calls or Azure provisioning/deployment, stop before those actions. Provide the concrete implementation-ready proposal and explicit approval request. Do not spend another iteration expanding SQLite infrastructure research or producing speculative architecture documentation.
