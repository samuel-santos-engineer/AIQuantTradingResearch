# Codex execution prompt — Release 1.15 WP58 Phase F: Minimal Azure F1 Staging Reproduction

## Mission
Reproduce the **already accepted local BTC/USD and ETH/USD historical candlestick chart** in the **existing Azure F1 staging** using the current **two application containers** (Python/Streamlit UI and C#/.NET research API) and **ephemeral SQLite inside the .NET container**. Prioritize a working public staging chart, not generalized Azure infrastructure investigation.

This prompt authorizes **offline repository inspection, targeted implementation and tests, and a reviewable PR**. It does **not** authorize Azure/registry mutations, Docker builds/pushes, private-data access or transfer, credentials, login, live probes, staging deployment, or PR merge. Present one narrowly bounded executable live-operation request when implementation is ready. Do not interpret this prompt as approval for that operation.

## Established evidence and constraints
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Existing documentation PR **#445**, open/unmerged at last verification, head `fdf84b81aff68f9aa54970fea88ebebebdae9c2d`. Verify fresh before editing; do not merge it.
- Historical WP57 local browser acceptance: BTC → ETH → BTC, **1,000 authentic candles per symbol**, SMA20 **981 non-null / 19 warmup**, volume unavailable/null with no fabricated bars, provenance/educational disclosures. `LOCAL_CHART_VERIFIED=True` (historical), `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.
- Dedicated Dockerfiles: `container/release_1_15/research-chart.Dockerfile` (Python/Streamlit, port 8501) and `container/release_1_15/research-api.Dockerfile` (.NET, port 52731, bootstrap, UID1654). Dockerfiles copy code/published output, **not a proven populated SQLite DB**. Local image IDs are not OCI registry digests.
- Existing Azure staging UI `aiqr112wp05wus27f5eabb5`, research API `aiqr115researchstagingwus27f5eabb5`, shared Linux F1 plan `asp-aiq-r112-wp05-wus2-7f5eabb5`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, West US 2. **Production `aiqr112wp035ec325382770` is excluded.**
- Azure research app last configured with an **older** artifact; observed `/health` HTTP 403 once and later timeout, cause **unknown**. Azure dataset presence and actual running artifact unverified. Do not assume image deployment fixes 403.
- Ephemeral SQLite may disappear when the .NET container is replaced. Accept manual reinitialization; no third tier, persistent database, new paid resources, new provider calls, SSH daemon, public data-upload endpoint, or secret/data baked into images.
- Prior Phase E found newer Linux app-container exec without sshd in Microsoft docs, but did **not** establish secure bulk transfer, stdin/completion or same-container lifecycle. Do not claim these are solved, and do not make broad CLI/exec research the milestone.

## Work plan — minimum path to parity
1. **Inspect the actual local working path:** locate precise source of retained validated BTC/ETH observations, local initializer invocation, SQLite ownership/path, keys/generation, startup sequencing, API endpoints, UI environment/config, and existing test helpers. Use code evidence rather than assumptions. Do not open private datasets or credentials without separate authorization; inspect source/tests/metadata only.
2. **Identify the smallest reproducible staging initialization method** that reuses the existing validated observations and initializer, runs within the **final .NET app container after image replacement**, and avoids exposing observations/keys in Docker layers, Git, URLs, arguments, environment settings, logs or browser. Favor an already-supported repository/runtime mechanism. If no proven secure same-container input/exec/lifecycle mechanism exists, clearly state the **single missing capability**, implement only safe offline portions, and propose one bounded verification/operation; do not invent a transport or pretend the chart will work.
3. **Implement only narrowly scoped, reusable code/scripts/tests** needed for a deterministic initialize → verify both datasets → serve sequence, where feasible offline. Preserve existing API and UI contracts, two-container topology, and ephemeral SQLite. Do not introduce an elaborate orchestration framework, automatic reinitialization, persistent state, recovery service, or general deployment abstraction. Use safe idempotency for identical observations; reject mismatched or partial datasets. Existing per-symbol commits are not evidence of pair atomicity: gate serving/acceptance on both symbols being valid.
4. **Prepare a compact staging runbook** with exact existing app targets, immutable image verification, required configuration without secret values, safe initialization preconditions, API-first then UI deployment, bounded authentic-data readiness checks, browser BTC → ETH → BTC acceptance, and simple stop/rollback instructions. Explicitly say rollback of image/config cannot recover ephemeral data. Do not turn the runbook into new licensing or governance prerequisites.
5. **Run targeted offline tests** and inspect diff for UTF-8/LF, no secret/raw-data leakage, source-only image contexts, and unrelated changes. Report what actually ran; never claim Azure or public readiness from offline tests.

## Staging acceptance criteria (future live phase, not authorized here)
- Exact compatible Linux/amd64 immutable images and intended configurations are running in the **two existing staging apps**.
- .NET final-container SQLite contains the **same validated authentic retained** BTC/USD and ETH/USD observations: exactly 1,000 per symbol, correct identities/provenance/gaps/null-volume semantics; no new provider acquisition or fabricated candles.
- API authenticated transformed snapshot succeeds for both symbols; SMA20 has 981 calculated values and 19 warmup values on the accepted contiguous dataset.
- Public staging UI browser BTC → ETH → BTC works, without JS failures, leaked credentials, fake volume, or ungrounded success claims.
- If data is absent or startup fails, display truthful unavailability; `PUBLIC_RUNTIME_VERIFIED` stays false. F1 cold-start/ephemeral-loss tradeoff accepted.

## GitHub workflow
- Inspect existing branch/PR #445 and repository instructions first. Avoid conflicting changes; prefer a **separate implementation PR** linked to #445, leaving #445 open/unmerged. Update documentation in #445 only if specifically necessary and safe; explain any dependency. Never merge without owner approval.
- Keep changes small, reviewable and limited to implementation/runbook/test paths necessary for this objective. Do not modify production configuration.

## Required final Codex report
Provide: (1) actual local initialization mechanism and why Azure images alone lack state; (2) files changed and PR URL; (3) tests/results; (4) what is genuinely ready versus blocked; (5) **one smallest next owner authorization request**, with exact commands/targets/timeouts, data/secret handling, failure stop and rollback boundaries. Do not ask a series of architecture questions. Do not perform live actions without explicit approval.

**Priority:** Deliver local-to-F1 functional equivalence with minimum moving parts. No speculative gates, no third layer, no persistence redesign.
