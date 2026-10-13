# Release 1.14 — WP07 Codex Preflight (READ-ONLY)

**Target:** issue #322, milestone #65 — Public Startup Isolation and Private Provider Preservation.
**Known merged predecessor evidence:** WP03 PR #327 (`6eeeecb515c6f8b179d0f02fb8f5595c4bc0b3c0`); WP02 PR #328 (`bc7c79edbf254276ccc56f789676d6b073af6620`); WP04 PR #329 (`dc5e6b4fcbd64865a5a1a71f398f04735ab961d9`). Confirm current remote/local baseline rather than assuming it.
**Authority:** inspection and optional offline run-only validation ONLY. Do not edit/create/delete/format any repository file, stage, commit, push, open PR, change issues, deploy, or call live market providers. The owner must separately approve an exhaustive literal-path implementation authority after reviewing this report.

## Controlling documents (inspect in this order)
1. `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md` — **Section 10 controls**, regardless of stale Proposed labels.
2. `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md` — AC110, AC114, AC117, AC118, AC120, AC121; G-3/G-4; exact provisional limits.
3. `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md` — WP07, common no-bypass rules, WP07→WP05/WP08 dependency.
4. `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md` — WP07 proposed paths, shared-file sequencing, preservation-only files.
5. Merged PRs #327/#328/#329 and issue #322.

## Proposed WP07 mutation candidates — NOT APPROVED
Exactly these nine **existing** paths are proposed in the manifest; check presence, responsibility, integration seams and whether meaningful tests can fit. These paths are **not** granted mutation authority by this preflight.

1. `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs`
2. `src/AIQuantTradingResearch.Worker/Program.cs`
3. `container/entrypoint.sh`
4. `python/presentation/historical_market_bridge.py`
5. `python/presentation/realtime_financial_visualization.py`
6. `tests/AIQuantTradingResearch.Infrastructure.Tests/DependencyInjectionTests.cs`
7. `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalProductionCompositionAcceptanceTests.cs`
8. `python/presentation/test_historical_market_bridge.py`
9. `python/presentation/test_market_research_ui.py`

**Important:** The manifest expressly warns that meaningful new startup/capture/global-admission tests may require **additional literal-path amendment**. If nine files cannot accommodate adequate independent test seams, list precise additional paths and STOP; never create them during preflight. WP04 has already modified the shared bridge/test files, so inspect current merged versions before proposing integration changes.

## Read-only investigation

### A. Baseline, preservation and dependency proof
- Record branch, HEAD, fetched `origin/main`, divergence, clean/dirty/staged/untracked status. Preserve every untracked prompter and unrelated change. Capture a before/after protected-file inventory/hash without writing repository artifacts.
- Confirm merged PRs and issue states. Confirm WP02 independent synthetic provider implements `IIndependentSyntheticHistoricalMarketDataProvider`; WP03 bounded `ExecuteAsync(Stream, …)` entry point exists and is not yet wired; WP04 response analytics projection is integrated.
- Inspect DI registration, Worker `Program.cs`, container startup, Streamlit startup/navigation/System Health, bridge subprocess execution, current cache/handoff/log/export boundaries, private Twelve Data daily-close modes and inactive Vike retention. Identify exact call graph and anonymous reachability.

### B. Public/private composition and environment
- Design synthetic-only anonymous composition with no private acquisition Worker, Twelve Data/Vike invocation, HTTP client creation, credential dependence or private fallback. Preserve private daily-close behavior without adding private OHLCV or failover.
- Specify a child environment **built from scratch**, allowed variable names/values and `DOTNET_PROCESSOR_COUNT=1`; reject inherited private secrets, cloud/provider credentials, proxy/provider override variables and private cache roots. Verify all public/private cache, handoff, log and export paths are isolated and cannot traverse into private roots.
- Verify the 4,096-byte Worker request cap is enforced **while reading** via the approved raw-byte entry point, not after `ReadToEndAsync`; inspect invalid UTF-8, duplicate/extra JSON keys, source/context overrides and error sanitization. Check no bypass on cache hits or derived analytics outputs.
- Inspect entrypoint startup modes: anonymous public boot must be secret-free and not start private acquisition. Private modes remain explicitly separate, with no new rights claims.

### C. Hard subprocess bounds and global admission
Determine concrete implementation and testing seams for every requirement below; distinguish already enforced from missing:
- **One globally admitted public child per app instance across sessions/processes**; at most **1 second** admission wait. Global means not merely Streamlit session state or a per-key cache lock.
- Worker operation deadline **10 seconds**; admission-to-cleanup/reap total **15 seconds**. Cancellation: TERM, ≤**1 second** grace, then kill/reap, including descendants as applicable. No live-child overlap merely because the existing 35-second lock lease expires; prove owner token and process lifecycle safety.
- Concurrent stdout/stderr draining with raw-byte checks **during capture**; stdout ≤**2,097,152 bytes**, stderr ≤**16,384 bytes**, read chunks ≤**8,192 bytes**. No `capture_output`/`communicate`/`ReadToEndAsync` unbounded buffering or post-hoc length check. Overflow, timeout, cancellation and stderr flood must terminate and reap; never display raw stderr.
- Aggregate child CPU admission ≤**60 CPU-seconds per 15 minutes** and ≤**240 CPU-seconds per day per instance**, fail closed when exhausted; identify trustworthy accounting and cross-process coordination, clock/rollover/abandoned-state treatment. **Do not claim these targets measured or feasible on Azure F1**; profiling is WP08.
- No automatic retry; explicit retry at most once per session per **10 seconds**. One current session response, expire after **30 idle minutes** on next interaction. Public System Health handoff ≤**64 KiB**, only sanitized operational metadata; no provider credentials, URLs, private paths, raw payloads, stacks, artifacts or internal errors.
- Consider all anonymous routes and reruns: market research, ML studies, System Health, navigation/retry/error states. Assert **zero private provider/HTTP calls**.

### D. Test feasibility and preservation
- Map each negative scenario to an **existing candidate test path** and specific seam: concurrent sessions and **separate processes**, admission overload, deadline/TERM/kill/reap, stdout/stderr flood, chunk-size, invalid UTF-8, request cap, session cooldown/TTL, CPU accounting, abandoned lease/live child, malicious source/environment, cache traversal, System Health leaks, startup with absent secrets, public-only synthetic qualification, private mode preservation.
- Assess whether reliable cross-process/process-tree/capture tests require a new Python test/helper or other file. Propose exact paths as **amendment candidates**, not permissions. Do not dilute requirements to fit nine paths.
- Run existing **offline-only** Application, Infrastructure and Python bridge/UI regression tests if safe and feasible. Report exact commands, passed/failed/skipped and known WP05 BTC/USD versus `btc-like-simulation` UI failure separately. Do not run tests that make live provider calls or modify protected artifacts.
- Inspect preservation-only Twelve Data/Vike files and regression tests read-only; confirm no changes needed to adapters. Confirm no packages, csproj, Dockerfile, schema, credentials, Azure config or deployment files needed; if needed, STOP and report.

## Report back (no edits)
1. Baseline/branch/working tree and protected-file before/after inventory.
2. Nine-path matrix: exists, present responsibility, exact proposed change, available test seam, any conflict with WP04.
3. Anonymous startup and provider-call graph, with current violations/unknowns; private preservation assessment.
4. Public child environment allowlist and source/private-root isolation matrix.
5. Requirement-by-requirement hard-bound matrix: current enforcement, proposed mechanism, test evidence needed, risk.
6. Exact test plan for cross-process global admission, CPU accounting, bounded capture, timeout/cancellation/reap and no lease-overlap. Flag whether an explicit path amendment is essential.
7. Offline baseline commands/results and pre-existing WP05 UI failure.
8. Any blockers, additional **literal** paths proposed for owner review, and stop conditions.
9. Finish with: **“No implementation edits made; owner WP07 implementation approval pending.”**

**Stop condition:** Any missing safe test seam, unexpected dependency, new file requirement, security ambiguity, unverified global coordination, inability to enforce hard bounds, private-provider invocation, scope conflict or baseline mismatch must be surfaced for owner decision—not worked around or silently implemented. No production activation or Azure deployment is authorized by this preflight.
