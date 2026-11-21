# Codex Prompt — Release 1.15 WP58: Public Runtime Qualification (BTC/USD + ETH/USD)

## Role and objective

Work in `samuel-santos-engineer/AIQuantTradingResearch`. Advance Release 1.15 from **locally verified historical BTC/USD and ETH/USD chart** to an evidence-based **public runtime qualification plan and, only with separate owner authorization, execution**. Prioritize the smallest change that makes the existing Azure web application display the working chart. Do not broaden scope or introduce new market-data providers.

## Immutable verified baseline

- PR #444 (WP56 optional-axis UI correction) **squash-merged first**, `cfe60efc4d8317dbf3c27df0ecd126117e19f31e`.
- PR #443 (WP55 authentic acquisition/import + WP57 local acceptance evidence) **squash-merged second**, `4a6bc2b901d6f6ff0081dad4597064017acb86f9`. Start by verifying `main` contains both commits; do not assume an old checkout or cached preparation is current.
- WP57 corrected local chart `http://127.0.0.1:18555`: **BTC → ETH → BTC browser PASS**, each 1,000 authentic-provider-derived completed minute candles, aligned SMA20 (981 non-null values), volume unavailable disclosure and no fabricated volume bars. Both-symbol authenticated local API PASS. No captured JavaScript errors.
- `AUTHENTIC_DATA_NOT_IMPORTED=False`; `LOCAL_CHART_VERIFIED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.
- Authentic observed-provider bundle and private SQLite already exist locally; no further acquisition or import is needed for this work package. Do not disclose or upload raw observations, private database, secrets, TLS private material, or private filesystem paths.
- Twelve Data is the sole upstream. Strict OHLCV remains default, explicit `--ohlc-only` is opt-in; volume remains nullable and never synthesized. C# analytics are authoritative. The UI labels transformed educational prices, not actual market quotes.

## Phase A — offline public-readiness investigation (authorized by this prompt)

1. Confirm repository branch/head, both merges, clean worktree, current governance and Release 1.15 execution plan. Identify actual Azure App Service target, deployment configuration, CI/CD workflow, runtime image/build arrangement, public URL, route/health checks, app startup process, network/authentication boundaries and any prior Azure F1 constraints **from repository files and existing public documentation only**. Do not inspect cloud accounts, credentials or private data.
2. Determine the minimal supported public architecture for the historical chart. Respect the established **Docker-first plus dual-process local runtime** policy: do not silently assume the loopback Docker setup can be exposed as-is. Assess whether the public Streamlit presentation can securely access a separately hosted authenticated research API, whether Azure F1 can support the proposed process/image arrangement, and what data persistence/lifecycle is necessary. Treat unanswered questions as findings, not presumed readiness.
3. Review the public-chart governance decisions. Do **not** create licensing approval gates or revisit provider selection. Keep the existing educational/provenance disclosures and ensure no secrets or private historical payloads are bundled into public artifacts.
4. Produce a short, prioritized gap matrix: current implementation, public requirement, evidence, minimal correction, and acceptance check. Distinguish deploy blocker from optional hardening. Prefer implementation-ready steps over speculative architecture or new documentation-only work.
5. If a clearly bounded code/configuration correction can be developed **entirely offline** without credentials, external resources, or operational side effects, implement it on an isolated branch with focused tests. Do not alter core acquisition, imported datasets, analytics, instrument catalog, or private runtime resources. Keep any resulting PR **OPEN/UNMERGED**.
6. Run focused offline tests using cached existing dependencies; report exact pass/fail/skip counts and commands. No invented CI or browser claims. Record relevant nonsecret evidence in `docs/roadmap/release-1.15/` and update the execution plan without rewriting historical results.

## Phase B — explicit authorization boundary (STOP before operations)

After Phase A, present a **concrete, minimal proposed public-runtime operation** for owner approval, including exact target, expected artifact/image identity, planned configuration (redacted), resource changes, rollback, cost/free-tier implications, test steps and stop conditions. Request authorization **before** any:

- Azure login, account/resource/plan/quota inspection, registry push, deployment, app setting change, remote restart, public URL probe, or cloud cost-incurring action;
- Docker build/pull/start/stop, local runtime switch, private state/secret inspection, browser automation against live runtime, or networked external check;
- Twelve Data requests, provider credentials, reacquisition, import, data migration or publication of private raw candles.

Do not interpret merge approval or earlier WP55/WP57 local operational approvals as permission for WP58. Do not demand approvals for ordinary offline code review and tests. Do not perform a public deployment under the authority of this prompt alone.

## Phase C — only if owner subsequently authorizes concrete operations

Execute **only** the approved bounded plan, with a clear rollback and stop-on-failure policy. Validate the actual public route and authenticated API-to-UI boundary, BTC → ETH → BTC browser rendering, candle counts and aligned indicators, null-volume presentation, educational/provenance labels, session isolation, safe secret handling, no unexpected network/provider calls, and basic health/restart behavior if within approved scope. Do not claim public verification based solely on local browser success or a healthy HTTP response. Capture sanitized outcomes and actual image/deployment identities; no screenshots or raw market data unless separately authorized.

## PR and reporting requirements

- Make one focused, reviewable PR if code or documentation changes are needed; do not merge it.
- Keep `LOCAL_CHART_VERIFIED=True` as the historical WP57 acceptance claim, but qualify whether later changes require new local testing. Keep `PUBLIC_RUNTIME_VERIFIED=False` until real public browser acceptance passes.
- Final response must identify: confirmed public blockers, recommended smallest viable deployment approach, changes and test counts, authorization needed next, PR link/head if created, and truthful status flags.
- If public hosting is not feasible under current Azure F1/runtime constraints, stop with a specific evidence-backed alternative **within existing architecture** and a narrow owner decision; do not invent a workaround or silently upgrade to paid resources.

## Definition of done

WP58 Phase A is done when there is a technically grounded, minimally scoped, implementation-ready public-runtime plan (and optional offline code PR) with explicit remaining authorization and acceptance criteria. **Release 1.15 public acceptance is done only after separately authorized live deployment and actual BTC/ETH public browser verification.**
