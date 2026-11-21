# Codex execution prompt — Release 1.15 / WP47

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Baseline: PR #433 squash-merged into `main` at `45e9250ff658f9dc4f9fef8963370bc6fd30d244`.

## Objective

Deliver a fresh, reproducible local build of the WP46-compatible Twelve Data adapter and, **only after a new, separate, explicit owner approval**, execute at most one bounded authentic acquisition. If a complete authentic BTC/USD and ETH/USD bundle is published, import it and verify the local Docker candlestick interface. Preserve strict provenance, security, no-retry behavior, and owner artifacts. Create a WP47 PR for review; do not merge it.

## Non-negotiable boundaries

- No Azure operations, public deployment, registry publication, licensing gates, or unrelated architecture changes.
- Do not read provider credentials, check their value/presence, contact provider/API endpoints, perform DNS/HEAD/capability probes, or access historical private responses during offline preparation.
- Prior WP41/WP44 authorizations are consumed. This prompt, PR merge, or successful prechecks **do not authorize** another acquisition.
- No retry, pagination, fallback, extra symbols, additional endpoints, or automatic second invocation. Stop on first failure or ambiguous outcome.
- Preserve owner dirty/untracked files, old private evidence, and resource ownership. Use isolated worktree and new owner-private preparation directory; never overwrite or prune historical artifacts.
- Use existing governance and WP39–WP46 contracts; avoid new approval/licensing gates beyond the existing explicit per-attempt owner authorization.

## Phase A — offline, authorized now

1. Verify `origin/main` contains exact merge SHA above; review applicable `AGENTS.md`, governance, WP39–WP46 runbooks and actual source. Create a clean isolated WP47 branch/worktree; do not disturb owner's checkout.
2. Inspect WP46 change: `meta.exchange_timezone` may be absent for the fixed UTC BTC/ETH path; if present, its strict checks remain. Confirm required status, symbol, interval, UTC output timestamps, OHLCV, volume, bounds, provenance and atomic publication remain unchanged. Do not infer WP44's failed field.
3. Prepare a **new** owner-private directory with existing `eng/docker/release-1.15/wp39_local_history.py --action Prepare`; use only cached/pinned inputs, offline/empty-feed .NET restore, fresh C# publish and Docker builds (`--pull=false`, `--network=none`). Verify assembly hashes against build outputs, both owned image IDs against the new manifest, ACL protection, actual TLS certificate SAN/validity, destination absence, and zero conflicting owned sessions/volumes. If any prerequisite fails, stop; do not work around it by accessing the network.
4. Run focused C# acquisition/adapter/initializer tests, Python WP39 workflow tests, and chart/client tests; expected baseline **210 passed** (126 C#, 23 Python workflow, 61 chart/client). Record actual counts, warnings and failures. Exercise the freshly published Worker through the actual Python strict validator using the **missing-approval** negative probe, proving zero requests/responses. Run UTF-8/LF/no BOM checks and `git diff --check`.
5. Document sanitized offline evidence and precise readiness state in `docs/roadmap/release-1.15/prompters/release-1.15-wp47-controlled-acquisition.md` and append to `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`. No private paths, secrets, payloads, headers, provider messages, or credentials in Git or logs.
6. **Stop and ask the owner for a fresh, explicit approval for exactly one Acquire invocation.** If approval is not received, finish with `AWAITING_NEW_OWNER_PROVIDER_APPROVAL`, create a docs-only PR and leave it OPEN/UNMERGED. Do not claim liveAttemptReady=true.

## Phase B — only after separate explicit owner approval

1. Recheck exact clean source/head, fresh assembly/image/manifest identities, protected private ACL, unexpired actual TLS, no conflicting session/state resources, and absence of destination artifacts. Check only the exact Windows User setting **name** `TwelveData__ApiKey` during this authorized preflight. Read its value solely inside the approved Acquire process, map temporarily to process-only `AIQ_WP39_PROVIDER_KEY`, and restore previous process channel in `finally`. Never expose the key in command lines, URLs, files, Docker, logs, Git, or browser.
2. Invoke the **existing Acquire action once only**, permitting at most two sequential HTTPS GETs to `https://api.twelvedata.com/time_series`: BTC/USD then ETH/USD; `interval=1min`, `outputsize=1000`, `timezone=UTC`, `order=ASC`, existing latest-24h completed-observation window and 15-second grace. Use existing owner-confirmed base-asset volume assumption. Do not retry even if the first attempt fails with a precise code.
3. On any failed/unknown outcome: stop immediately. Record only the existing bounded version-1 ten-field diagnostic, exit code, request/response counters and presence-only artifact facts. No response inspection, further provider probes, import, start, verify, browser or additional request. Mark approval consumed; historical WP44 metadata defect and credit usage remain unknown.
4. **Only if** the complete authentic two-symbol bundle was atomically published and passes existing provenance/hash checks: run existing Import → Start → Verify → local browser checks → Stop. Validate authentic BTC/USD and ETH/USD minute candles, UTC, OHLCV/base volume, counts/gaps, receipt/generation, authenticated TLS, switching, 100/300/1000 presets, zoom and Refresh retention, labels, and readiness. Never substitute synthetic fixtures or weaken importer checks. Preserve owned state and private evidence on Stop. Record actual outcomes; do not claim public runtime verification from local-only tests.
5. Update WP47 runbook and execution-plan append with sanitized factual evidence, test totals, actual flags and precise stop reason. Commit only intended documentation or narrowly justified code/tests if a defect is independently demonstrated offline; open a WP47 PR against `main`, leave it **OPEN/UNMERGED** for owner review. Do not merge or deploy.

## Required final report

Report baseline/head and PR URL, changed paths, actual offline test counts, source/image identity evidence, whether explicit live approval was granted, exact number of Acquire invocations and provider requests, sanitized diagnostic or verified import/chart evidence, artifacts present/absent, and flags `AUTHENTIC_DATA_NOT_IMPORTED`, `PUBLIC_RUNTIME_VERIFIED`, `liveAttemptReady`. Clearly distinguish local Docker verification from public/Azure deployment. Never infer the cause of previous responses or provider credits from diagnostics alone.
