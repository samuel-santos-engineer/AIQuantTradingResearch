# Release 1.14 — WP04 Codex Preflight Authority

**Work package:** WP04 — Shared Deterministic Technical Indicators  
**Tracking:** https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/321  
**Milestone:** #65  
**Mode:** READ-ONLY PREFLIGHT. NO IMPLEMENTATION AUTHORITY.  
**Expected predecessor:** WP03 PR #327 (`6eeeecb515c6f8b179d0f02fb8f5595c4bc0b3c0`), WP02 PR #328 (`bc7c79edbf254276ccc56f789676d6b073af6620`).

## 1. Authority and non-mutation boundary

You are Codex operating in the AIQuantTradingResearch repository. This instruction authorizes inspection and non-mutating offline validation **only**. Do not create, edit, stage, commit, push, delete or rename any repository file; do not open/merge PRs, mutate GitHub issues, modify Azure or deploy. Preserve every tracked and untracked file, including this prompter if present. Do not call Twelve Data, Vike, other market providers or external APIs.

Controlling source: Section 10 of `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`, then the merged WP01 engineering contract in:

- `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`

Do not treat the proposed manifest paths as mutation authority. Owner approval of a separate exhaustive literal-path WP04 implementation authority is required after this preflight.

## 2. Verify baseline and preservation

1. Record current branch, HEAD full SHA, fetched `origin/main` SHA, divergence, worktree status and untracked paths. Do not reset or clean.
2. Verify WP03 PR #327 and WP02 PR #328 are merged, issues #319 and #320 closed, issue #321 open, milestone #65 existing.
3. Inventory and hash tracked and untracked files before and after inspection; explicitly preserve any unrelated changes.
4. Identify any conflicts between current implementation and Section 10/WP01; do not resolve by editing during preflight.

## 3. Proposed WP04 literal mutation manifest — inspection only

**Proposed new Application files:**

1. `src/AIQuantTradingResearch.Application/MarketData/TechnicalIndicatorContracts.cs`
2. `src/AIQuantTradingResearch.Application/MarketData/TechnicalIndicatorCalculator.cs`
3. `tests/AIQuantTradingResearch.Application.Tests/TechnicalIndicatorCalculatorTests.cs`

**Proposed existing integration/test files (shared with accepted WP03; must be inspected for compatibility and sequencing):**

4. `src/AIQuantTradingResearch.Worker/HistoricalMarketDataQueryExecution.cs`
5. `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalMarketDataQueryExecutionTests.cs`
6. `python/presentation/historical_market_bridge.py`
7. `python/presentation/test_historical_market_bridge.py`

These seven paths are **candidates**, not approved edits. Check that the three proposed-new files are absent and four integration files exist; check implicit C# compile inclusion, project references, `InternalsVisibleTo`, protocol projection, and test seams. Identify whether all WP04 obligations can be met within exactly these seven paths, or whether an explicit owner-approved path amendment is needed. Do not create a new fixture or reference script path; embed independently derived vectors in the authorized test file if feasible.

## 4. Frozen analytical contract to inspect

Calculation version: `aiq-ta-v1`; scaled integer `S=10^12`, arbitrary-precision intermediate arithmetic and exact rational rounding **nearest integer, midpoint ties to even, symmetric for negative values**. Reject excess decimal precision rather than rounding input. Maximum calculation input 2,410 candles; finite positive closes no greater than 10^9. Preserve UTC alignment, strict order, source/context/window fingerprint, calculation identity and nullable outputs; no lookahead, zero substitution or forward fill. Exactly 250 calculation-only warmup candles precede the selected visible candles; return only visible indicator values. No binary floats until chart rendering. No packages.

- **SMA20:** rounded sum of 20 scaled closes / 20; first valid one-based sample 20.
- **EMA20:** rounded SMA20 seed; recurrence `R((2*X + 19*previous)/21)`; first valid sample 20.
- **RSI14 (Wilder):** rounded seed averages over 14 close differences, then `R((13*previous+current)/14)`; first valid sample 15. Both average gain/loss zero => 50; zero loss with positive gain => 100; zero gain with positive loss => 0.
- **MACD12/26/9:** SMA-seeded EMA12 and EMA26, MACD difference; SMA-seeded EMA9 of valid MACD; histogram difference. First valid MACD sample 26; signal and histogram sample 34.
- **Bollinger20×2:** population variance with `A=20*sum(X^2)-sum(X)^2`; `sigma=R(sqrt(A)/20)` computed by integer floor square root and exact midpoint comparison `4*A` versus `20^2*(2*k+1)^2`, ties to even; middle SMA20 and upper/lower middle ±2*sigma. First valid sample 20. Negative lower bands are valid.

Use the *complete definition* as normative for all arithmetic, metadata and protocol details. Overlapping range selections need not have identical recursive EMA/RSI/MACD outputs because the 250-candle seed windows differ. Do not mistakenly assert otherwise.

## 5. Preflight checks and independent evidence plan

Inspect the accepted WP03 canonical/provenance and query response contracts and WP02 generator only as **read-only** inputs. Determine:

- Where canonical decimal closes and 250 hidden candles are supplied and how to reject gaps, duplicates, invalid ordering, identity/provenance mismatch, or malformed values.
- Where to place immutable calculation contracts, parameter/version identity, nullable timestamp-aligned results and visible-only projection without leaking warmup data.
- How the Worker and Python bridge currently validate v2 responses and how an analytics payload can be added without bypassing WP03 publication validation, size bounds, fingerprint checks, public-safe failures or private isolation.
- Whether existing public response contracts or bridge validation force changes outside the seven candidate paths. Stop and propose an exact amendment if so.
- Whether the new C# Application files compile automatically, whether current tests can exercise internals, and whether integration tests can be written within the listed test files.
- A truly independent test-oracle plan: manually/independently derived constant, rising, falling, alternating and mixed series; signed ties-to-even; integer square-root midpoint and overflow; first-valid/null positions; no-lookahead; warmup/visible projection; provenance and calculation identity; deterministic results regardless of adapter. Avoid generating expected values from the production implementation.

Approved reference checks (documentary, not implementation evidence): constant 100 series => SMA/EMA 100, RSI 50, MACD 0; increasing 1..40 => SMA20 at 20 = 10.5, EMA20 at 21 = 11.5, RSI at 15 = 100, MACD at 26 = 7 and signal at 34 = 7; decreasing 40..1 => SMA20 at 20 = 30.5, EMA20 at 21 = 29.5, RSI at 15 = 0, MACD at 26 = -7 and signal at 34 = -7. For 1..20, population sigma `5.766281297335`, bands `-1.032562594670 / 10.500000000000 / 22.032562594670`. Alternating 100,101 yields RSI sample 15 = 50 and sample 16 = `53.571428571400`. Rounding: `R(1/2)=0`, `R(3/2)=2`, `R(-1/2)=0`, `R(-3/2)=-2`.

## 6. Read-only validation

Run only relevant existing **offline** baseline tests, with exact commands, pass/fail counts, runtime versions and environmental caveats. Prefer `--no-restore -p:AutoSignTestBinaries=false` where required by the local certificate; use `xUnit.MaxParallelThreads=1` if needed to avoid infrastructure contention. Do not edit test files or suppress failures. Record the existing known UI default mismatch (BTC/USD versus `btc-like-simulation`) as WP05-owned; distinguish it from WP04 regressions. Do not execute new generator code against providers or modify build configurations.

## 7. Explicit exclusions and stop conditions

No WP02 generator changes; no WP03 contract/read-service/cache changes unless a new owner-approved amendment is obtained; no WP07 DI, Program.cs, container, startup, credentials, runtime environment or global admission; no WP05 UI/chart changes; no WP06 pages/assets; no WP08 profiling/deployment. No provider adapters, database, schemas, packages, project files, new scripts or fixtures, CI configuration or cloud mutations.

Stop if arithmetic requirements contradict the accepted specification; if canonical or Worker/Python protocol constraints require an eighth path; if an independent oracle cannot be established; or if source/provenance validation would be weakened. Report blockers and proposed **literal** path amendments, not silent workarounds.

## 8. Required preflight report

Return a concise but evidence-based report containing:

1. Full baseline SHA, PR/issue/milestone verification, working-tree state and preserved untracked files.
2. Seven-path existence/feasibility matrix, compilation/test seams and precise integration touchpoints.
3. Frozen numerical contract and independent golden-vector/test plan, including first-valid indices and square-root rounding.
4. Protocol/bridge/publication-gate compatibility and any path amendment needed.
5. Offline baseline test commands/results and any pre-existing failures.
6. Before/after preservation hash counts and zero-edit confirmation.
7. A clear decision: **two-phase WP04 implementation feasible within seven paths / explicit scope amendment required**.
8. Explicit statement: **No implementation edits; owner implementation approval pending.**

**Do not begin implementation after a passing preflight. Wait for the repository owner's separate, explicit WP04 implementation approval and exact path allowlist.**
