# Codex execution prompt — Release 1.15 WP07: Azure staging BTC/ETH historical chart

## Authority and baseline

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Begin from current `origin/main` including merged PR #399, squash commit `654274a3aca706008ea82bb2852b11d19200c092`. Preserve the owner's existing checkout, all dirty/untracked files and state byte-for-byte; use a clean isolated worktree. Create one focused PR for owner review, and leave it OPEN/UNMERGED. Do not close WP issues or change `PUBLIC_RUNTIME_VERIFIED=False` without actual staging acceptance evidence.

**Owner direction:** Every evaluation of Azure behavior, storage, routing, reachability, startup, capacity and browser operation MUST be performed against our **existing Azure staging environment**, not production, an unrelated Azure subscription/environment, or a hypothetical substitute. Local tests may be used for fast development and regression, but cannot be presented as Azure qualification. Do not broaden the milestone into another generic SQLite study.

## Goal

Deliver the shortest safe path to a **working historical BTC/USD and ETH/USD candle chart in existing Azure staging**, backed by authentic completed-minute observations, C# research snapshots, existing D4/D5 calculations and Streamlit/Plotly. Reuse accepted local results from PRs #395–#399. SQLite remains the only authoritative research datastore; one writer; controlled stop/verify/start. No pooling, distributed coordination, cross-host fencing, automatic failover, host-power-loss testing, schema replacement, or licensing/approval gates.

## Known staging targets and boundaries

- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`.
- Existing F1 plan: `asp-aiq-r112-wp05-wus2-7f5eabb5`.
- UI staging app: `aiqr112wp05wus27f5eabb5` (currently legacy image).
- Research staging app: `aiqr115researchstagingwus27f5eabb5` (currently shell).
- Both are staging only; do not touch production.
- PR #398 deliberately fails closed for App Service writable SQLite, because F1's currently identified storage is not a qualified durable local/block WAL volume. **Never bypass or strip Azure identity markers to force the writer to run.**
- PR #399 demonstrates the actual chart locally with fixture candles, strict internal HTTPS, and stop/start persistence; it does not establish a public Azure chart or authentic observations.

## Work plan — implementation first, bounded decisions

1. **Inspect current staging read-only.** Reconfirm the exact existing apps/plan, their assigned images, HTTPS-only, public ingress, port settings, persistent mounts and any already-owned suitable research host. Do not print secrets, provider keys, connection strings, registry credentials or private state. Identify the minimal missing deployment inputs and actionable next steps, rather than producing another broad planning report.
2. **Keep the Azure assessment anchored to staging.** Any Azure test must target the named staging resources or an owner-authorized research host explicitly assigned to serve those staging apps. No production testing and no substitute Azure test environment. If there is no suitable research host with persistent local/block filesystem, report the concrete blocker and present the smallest single-instance staging-host option and cost implications for owner decision. Do not provision or purchase anything without explicit approval.
3. **Real completed observations.** Determine whether an authorized private BTC/USD and ETH/USD completed 1-minute export is already available via approved operator inputs. Do not access provider credentials or make live provider calls without explicit owner authorization. Use the existing bounded private acquisition/import and production `--research-initialize` path; never use deterministic fixture rows as real data, fabricate candles, or leak HMAC/API keys. If missing, state exactly which operator-supplied export or bounded acquisition authorization is needed.
4. **Single-writer SQLite hosting.** Preserve the existing SQLite/WAL, D5 tuple, integrity witness and fail-closed storage boundary. Research must run with one authoritative SQLite volume on a supported persistent local/block filesystem. Restarts are sequential: stop old instance, verify stopped, start new instance with unchanged state. Do not add connection pooling, distributed leases or multiwriter support. Do not redo the PR #398 fault matrix or power-loss testing.
5. **Complete staging integration preparation.** Ensure the existing Streamlit client can use a public staging research HTTPS hostname with validated CA/hostname and server-side `X-API-Key`; do not relax TLS, place secrets in the browser, or claim that App Service's frontend TLS termination provides end-to-end encryption. Keep UI on the existing F1 staging app if feasible. Where changes can be safely implemented without cloud mutation, implement them and test locally. Produce exact reproducible staging configuration/deployment/rollback commands, but **do not execute mutations** until the owner separately authorizes the precise staging actions.
6. **Actual staging acceptance after authorization only.** Once the owner authorizes required provider/data and staging-host/deployment operations, evaluate only the existing staging environment: authentic BTC/ETH completed candles, indicators, gaps, stale/reconnect, strict HTTPS/auth negatives, chart selection and real Chrome behavior, persistence through ordinary stop/start, F1 cold start and basic 1/3/5-session behavior if practical. Record actual failures and scope limits. Never set `PUBLIC_RUNTIME_VERIFIED=True` based on local fixtures or read-only staging inventory.

## Deliverables

- A focused implementation PR (if code or operator automation changes are necessary) and concise append-only evidence/runbook updates, with exact changed-file inventory.
- A staging-specific deployment and acceptance procedure, showing the exact named resources and explicit authorization needed for any remaining mutation.
- A short decision table: `IMPLEMENTED_LOCAL`, `VERIFIED_AZURE_STAGING`, `BLOCKED_OWNER_INPUT`, `NOT_RUN`, without conflating them.
- Evidence of relevant executed checks, including failed attempts; Git diff/encoding verification; untouched owner checkout.
- If the only remaining blockers are owner-supplied real data and a suitable persistent staging research host, **stop there** and request those concrete decisions. Do not create speculative infrastructure, new governance gates or an endless sequence of research PRs.

## Strict exclusions

No Azure mutation, deployment, image push, registry publication, paid resource creation, live provider acquisition, secret retrieval, production change, PR merge or WP closure without explicit owner authorization. Do not change the SQLite storage policy to permit unsupported F1 WAL. No new licensing prerequisite. Keep `PUBLIC_RUNTIME_VERIFIED=False` until the actual staging acceptance criteria are met.
