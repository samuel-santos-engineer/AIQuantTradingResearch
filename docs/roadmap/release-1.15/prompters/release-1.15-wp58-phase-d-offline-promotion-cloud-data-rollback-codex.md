# Codex execution prompt — Release 1.15 WP58 Phase D
## Offline artifact promotion, cloud dataset readiness, and rollback planning

You are working in `samuel-santos-engineer/AIQuantTradingResearch`. The owner approves **offline investigation, planning, and documentation only** under this prompt. Do not infer authorization for Azure/registry/HTTP/Docker/provider/secret/data operations, publication, deployment, PR merge, or cloud resource mutation.

### Verified starting point

- Main contains squash merges PR #444 (`cfe60efc4d8317dbf3c27df0ecd126117e19f31e`) then PR #443 (`4a6bc2b901d6f6ff0081dad4597064017acb86f9`).
- PR #445 (`docs/release-1.15-wp58-public-readiness`) was OPEN/UNMERGED at `aa169c42b550f791ea1e10f7906b2312e6f071f5` when this prompt was written. Recheck its current head before editing; never overwrite intervening changes.
- WP57 local browser BTC → ETH → BTC passed with authentic imported historical candles, 1,000 per symbol, SMA20 981 non-null values, unavailable volume correctly disclosed, and no new acquisition/import.
- Azure staging research site's configured classic-container digest was read as `sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f`, an older WP35/WP37 artifact. This is **configured-reference evidence**, not running-image or registry-content proof, nor an explanation for the earlier HTTP 403.
- Research `/health` previously returned HTTPS 403/text-html in one bounded probe; a later headers-only request timed out. HTTP 403 origin remains unknown. A later Azure F1 snapshot reported stop requests 0/15, CPU counters below limits, and research Running/Normal/Normal; those later counters do not explain the earlier response.
- Current status: `AUTHENTIC_DATA_NOT_IMPORTED=False` (local historical import), `LOCAL_CHART_VERIFIED=True` (historical WP57), `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.

### Goal

Prepare a **specific, implementable, evidence-based** offline plan to promote the accepted two-image local chart implementation to the **existing West US 2 staging pair**, while proving or explicitly identifying the blocker for **authentic cloud dataset availability** and documenting safe per-app rollback. Prioritize a functioning public BTC/USD and ETH/USD historical chart over additional speculative governance work. Do not introduce new provider or licensing approval gates.

### Exact staging topology and boundaries

- Staging UI: `aiqr112wp05wus27f5eabb5`.
- Staging research API: `aiqr115researchstagingwus27f5eabb5`.
- Existing shared Linux F1 plan: `asp-aiq-r112-wp05-wus2-7f5eabb5` in resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`.
- Existing dual-process/two-container policy: separate Streamlit research chart UI and C# research API, strict HTTPS server-to-server service-key authentication, no secret/provider key in browser or public image. No supervisor/combined-image shortcut, no tunnel, no paid-tier upgrade, no new Azure resources.
- Production app `aiqr112wp035ec325382770` is entirely excluded.
- No Twelve Data requests, new acquisition, reimport, database/raw-observation upload, state migration, private database mounting/baking, or synthetic-data substitution. Do not inspect private bundles, database bytes, keys, TLS materials or credentials.

### Workstream 1 — source and artifact identity (offline)

1. Verify the clean worktree, exact Git branch and latest PR #445 head; avoid touching owner checkout/untracked files. Use an isolated worktree if appropriate and already available without network side effects.
2. Trace the exact merged source for the accepted local UI and research API. Inspect `container/release_1_15/research-chart.Dockerfile`, `research-api.Dockerfile`, related entrypoints, allowlisted build contexts, startup and port contracts. Distinguish dedicated research UI from root synthetic `public_preview_app`.
3. Compare WP57 local Docker image IDs (`sha256:3270ce6ec33ba8cb7da27738ea6d9dc27a811a3270e7ecdef83f32e757a83ae1` UI and `sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028` API) with the old Azure configured digest. Explicitly state that local image IDs are **not** OCI registry manifest digests.
4. Propose deterministic, secret-free source-only builds and a **separately approved** publication/manifest-readback sequence to establish exact compatible immutable OCI digests for both images. Do not actually build/pull/push/login/contact registry without fresh owner approval. Include an image-content exclusion checklist for raw observations, SQLite/WAL, secrets, TLS and private mounts.
5. Check whether existing deployment scripts hardcode stale digests or single-image assumptions. Describe the minimal implementation correction needed, with exact file paths and tests, but do not modify runtime/deployment code in this planning-only phase unless the owner later authorizes implementation.

### Workstream 2 — cloud dataset readiness (priority blocker)

1. Trace the C# research API bootstrap, SQLite persistence boundary, state initialization and authenticated snapshot flow **from repository source and already recorded evidence only**. Identify what `/health` can and cannot prove; distinguish process liveness, authenticated service readiness, and 1,000 authentic BTC/ETH observations available to chart users.
2. Construct a decision table for cloud state: **verified initialized**, **verified missing**, **unverified**, and **lost after restart**. For each, give an honest user-visible outcome, next separately approvable verification, and stop condition. No claim that local imported state exists in Azure.
3. Explain F1 ephemeral overlay implications and whether a restart can erase state. Do not silently propose raw-data upload, reacquisition/import/migration, provider calls or image-baked datasets as workarounds. If those restrictions make an actually working public historical chart impossible, say so clearly and present the owner with the exact architectural decision needed rather than creating a fictional readiness gate.
4. Design a future narrowly scoped authenticated readiness check (proposal only): two symbols, count/provenance/generation/SMA/volume semantics, strict no raw-price/secret logging, timeout and stop conditions, explicit service-key handling by owner-approved secure channel. No call now.
5. Evaluate whether the plan can achieve reliable public chart operation on the existing F1 plan without persistent data; distinguish temporary demo success from restart-safe availability. Do not claim zero cost or guaranteed quota headroom.

### Workstream 3 — exact staging promotion and rollback (offline)

1. Define prerequisites in dependency order: cloud data feasibility; artifact identity and safe publication; authenticated service configuration; bounded health; exact rollback baseline; then API-first/UI-second staging promotion; then public browser acceptance. Do not assume the unexplained HTTP 403 is resolved by replacing an old digest.
2. Specify the **minimum per-app** settings/images/startup values to inspect and later change. Include UI `WEBSITES_PORT=8501`, research `WEBSITES_PORT=52731` as proposals requiring source verification; HTTPS base URL and server-only API key; no local CA/Docker DNS. Preserve all unrelated app settings and secrets without exposing them.
3. Design a **read-only, private rollback-baseline capture** as a future separately authorized action, with exact previous image digest/reference, startup, settings-preserving diff, identity and redaction. No full-settings replacement. State how to stop if a reversible baseline cannot be established.
4. Propose one bounded apply window for the two existing staging apps, explicit owner approval for each write and restart, one preapproved rollback on failure, no retry/restart loops, no production promotion. Include verification of actual running image, immutable manifest identity, app readiness and authenticated data; configured-reference readback alone is insufficient.
5. Define public acceptance: real HTTPS UI; BTC→ETH→BTC with 1,000 authentic candles per symbol and SMA20 981 non-null values where contiguous; volume-unavailable disclosure/no fabricated bars; C# authoritative analytics and provenance; two isolated browser sessions; no secrets in browser; no failed assets or JS errors; bounded F1 resource observations; and a separately approved restart/loss behavior check. Do not mark public verified on HTTP 200 alone.

### Deliverables and repository workflow

- Update `docs/roadmap/release-1.15/RELEASE_1.15_WP58_PUBLIC_RUNTIME_QUALIFICATION_PLAN.md` with a clearly dated **Phase D — OFFLINE ONLY** section: exact source/architecture findings, artifact-pair mapping, dataset decision table, proposed immutable-digest promotion sequence, reversible per-app rollback, blockers, acceptance criteria and precisely scoped next owner decision.
- Append a concise Phase D checkpoint to `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` without rewriting historical entries.
- Keep PR #445 **OPEN/UNMERGED**; update its existing branch and description only if its head and worktree can be safely reconciled. Do not create a duplicate PR or merge. If the PR has moved, inspect/reconcile before writing.
- Documentation-only changes preferred. Run `git diff --check`, UTF-8/LF/no BOM validation, and targeted **offline** tests only if relevant source/code changes justify them. Report exact executed commands, results and skips; do not claim tests/CI/cloud checks not run.
- Record a precise outcome matrix: `SOURCE_PAIR_IDENTIFIED`, `REGISTRY_DIGEST_PAIR_VERIFIED`, `CLOUD_DATASET_READY`, `ROLLBACK_BASELINE_VERIFIED`, `HTTP_403_ORIGIN_KNOWN`, `PUBLIC_RUNTIME_VERIFIED`, `liveAttemptReady`. Unknowns must remain UNKNOWN/FALSE, never inferred from plans.
- End with **one prioritized, minimal, separately approvable next operation** (or a concrete architectural decision if the data boundary blocks progress). Include target, method, projected fields, timeouts, number of calls, secrets/redaction, stop rules, rollback relevance, and what remains prohibited. Do not perform that operation in this phase.

### Non-negotiable execution rule

**Offline only.** Do not run Azure CLI/ARM reads, app URL requests, registry/network calls, Docker builds or runtime operations, browser tests, credential discovery, data access, provider requests, deployment, configuration changes or restart. GitHub PR publication of the documentation evidence is allowed within the existing PR workflow, but do not merge. When evidence is insufficient, report the gap and request the next exact approval rather than improvising live access.
