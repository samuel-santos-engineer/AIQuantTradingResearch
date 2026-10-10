# Release 1.14 — WP03 Codex Execution Authority: Canonical OHLCV Contracts and Public/Private Isolation

**Status:** Owner-requested WP03 document; implementation is **NOT yet authorized** by this document alone. Owner must explicitly approve execution against a current repository preflight. **No commit, PR, merge, deployment, live provider request, or GitHub issue mutation** is delegated by this document. This file is delivered for download and local Codex use; do not publish this prompter to the repository.

## 1. Preconditions and authority precedence

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Predecessor: merged PR [#318](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/318), merge SHA `20f7a8cada5fa84b8b99397051c09f9e1dc5ba3e` (verify current `main`/`origin/main` rather than assuming the historical SHA remains HEAD).
- Tracking milestone: [Release 1.14 milestone #65](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/65); WP03 tracking issue [#319](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/319). Creating the issue **does not authorize implementation**.
- Normative governance: `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`, accepted Section 10 superseding conflicts in Sections 1–9. WP01 approved baseline in `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`, `RELEASE_1.14_EXECUTION_PLAN.md` and `RELEASE_1.14_FILE_MANIFEST.md`.
- WP03 is before WP02, WP04 and WP07. **WP03 cannot claim running public isolation completed:** secret-free public startup, process environment, child stdout/stderr capture, global admission and System Health activation are WP07-owned, and final profiling is WP08-owned.
- Preflight: fetch/reconcile `origin/main`, record HEAD, branch, ahead/behind, clean/dirty status, PR #318 presence, all existing untracked assets, file hashes and repository governance. Never reset, clean, stash or overwrite unrelated changes. STOP if baseline or scope cannot be established.

## 2. Mission and explicit non-goals

Define and (only after separately approved execution) implement **provider-independent canonical historical OHLCV v2 contracts, provenance, server-owned public/private publication policy, strict public query/protocol validation, bounded isolated caching/locking and focused negative tests** while preserving the existing private Twelve Data daily-close path and Vike adapter. Legacy observed historical workflows may remain accessible only behind a demonstrably private boundary. No public market data is observed or fetched in this WP.

Out of scope: synthetic candle generation (WP02), indicator calculation (WP04), DI/Worker startup/container/public process environment/global admission and health (WP07), Streamlit chart and governance page (WP05/06), cloud/F1 profiling (WP08), real historical private OHLCV support, Vike failover, external publication-rights claims, provider calls, credentials, trading or ML, migrations/new dependencies/services.

## 3. WP03 proposed mutation allowlist — exhaustive, pending owner's execution approval

**Existing source paths:**

1. `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataContracts.cs`
2. `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataCacheContracts.cs`
3. `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataReadService.cs`
4. `src/AIQuantTradingResearch.Infrastructure/MarketData/Cache/AtomicFileHistoricalMarketDataCache.cs`
5. `src/AIQuantTradingResearch.Infrastructure/MarketData/Cache/AtomicFileHistoricalMarketDataQueryLock.cs`
6. `src/AIQuantTradingResearch.Worker/HistoricalMarketDataQueryExecution.cs`
7. `python/presentation/historical_market_bridge.py`

**Existing test paths:**

8. `tests/AIQuantTradingResearch.Application.Tests/HistoricalMarketDataContractsTests.cs`
9. `tests/AIQuantTradingResearch.Application.Tests/HistoricalMarketDataReadServiceTests.cs`
10. `tests/AIQuantTradingResearch.Infrastructure.Tests/AtomicFileHistoricalMarketDataCacheTests.cs`
11. `tests/AIQuantTradingResearch.Infrastructure.Tests/AtomicFileHistoricalMarketDataQueryLockTests.cs`
12. `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalMarketDataQueryExecutionTests.cs`
13. `python/presentation/test_historical_market_bridge.py`

These are **planning-proposed** exact paths, not blanket permission to edit any file. No directory globs or implied additional fixtures/scripts. When owner approves executing WP03, only these 13 may mutate unless a new literal-file amendment is approved. Never edit source or tests during read-only planning/review. If a required cross-cutting change cannot fit, STOP and report why. Preserve README, governance docs, WP01 docs, roadmap, packages/lockfiles, Dockerfile, entrypoint, Twelve Data and Vike adapters, DI/Worker `Program.cs`, Streamlit app, all other tests and untracked assets.

## 4. Frozen data contracts and identity

Canonical version `aiq-candles-v2`; public query `aiq-historical-query-v2`; public cache `aiq-historical-market-data-cache-v2`. Preserve fields sufficient to validate every data boundary: instrument ID, market type, source, observed/simulated status, venue, base/quote/settlement identities, UTC opening time, full OHLCV decimal units, volume unit, generator/version/seed as applicable, coverage bounds, interval, range, dataset/window fingerprints, and calculation-version identity when analytics are later added. Distinguish request-visible timeframe from fixed dataset coverage and operational generated-at. Never mislabel synthetic price units as USD or its activity volume as BTC/exchange turnover.

Approved public identity is exactly `btc-like-simulation`, market type `simulation`, source `independent-synthetic`, status `simulated`, no venue/base/quote/settlement, price `illustrative-price-unit`, volume `illustrative-activity-unit`; no BTC/USD spot or Hyperliquid perpetual identity. Public context/source identity must be **server-owned**, not accepted from caller input. Reject unknown, missing, conflicting, duplicate and malformed fields, invalid Unicode/UTF-8, nonfinite/overflow decimals, gaps/duplicates, count/grid misalignment, fingerprint mismatch and unapproved v1 observed-market requests. **No fallback** to Vike/Twelve Data/private roots on a public error. Public response failures are controlled and must not expose provider metadata, secrets, paths or private data.

Fixed simulation coverage (contract validation, not generator implementation): inclusive `2025-10-26T00:00:00Z`, exclusive `2026-10-01T00:00:00Z` = 340 UTC days. `1h` 8160, `4h` 2040, `1d` 340 complete aggregate rows; 12 selections `1h|4h|1d × 1D|7D|30D|90D`; exactly 250 prior selected-interval calculation candles, omitted from public visible candles. Visible matrix: 1h 24/168/720/2160; 4h 6/42/180/540; 1d 1/7/30/90. Default 1h/30D. Up to 2410 calculation rows, 2160 visible rows. Query window end is fixed exclusive coverage end. UTC-aligned 4h starts 00/04/08/12/16/20 and daily at 00; no partial buckets. `1d × 1D` has one visible candle.

Validate OHLCV constraints: `0 < Low <= min(Open, Close) <= max(Open, Close) <= High`, finite prices and nonnegative volume, monotonic unique UTC timestamps, correct count/interval/range, no silent repair/forward fill. Normalize none of the provider private content into a publicly publishable form.

## 5. Protocol and serialization boundaries

Approved generator canonical serialization is UTF-8 without BOM, LF-only with final newline, ordered manifest and rows, price scale 4, volume scale 3, fixed coverage and generator/version/seed; see WP01 definition for **authoritative exact byte layout**. WP03 should validate/transport versioned fingerprints without implementing synthetic generation or prematurely claiming independently verified goldens. Dataset hash excludes operational timestamps; window hash covers hidden calculation window. WP02 later supplies independent full-dataset golden proof. Future `aiq-ta-v1` analytics are WP04-owned, not to be fabricated in WP03.

Public request v2 contains only server-allowed version, instrument `btc-like-simulation`, one of three intervals, one of four ranges, and bounded deadline. Context/source cannot be overridden by callers. Strict JSON parse: reject duplicate properties, extra fields, multiple JSON values, invalid UTF-8, malformed or oversized input; count **input bytes before buffering/deserialization**, not after `ReadToEndAsync`. Maximum request payload **4096 bytes**. Establish typed public failure states (invalid/unavailable/busy/timeout as relevant) without exposing internals or market-staleness claims. Worker/Python bridge must enforce the same policy, including negatives before any cache hit or provider selection. Bridge stdout hard-capture/security changes beyond protocol validation remain WP07-owned; flag the boundary and leave it for WP07 rather than extending WP03 scope silently.

## 6. Cache and concurrency requirements

Cache keys include context and source, instrument/market identity, synthetic generator/version/seed, fixed coverage, interval/range, canonical/query/cache/calculation versions and other fingerprint-sensitive identity. Public synthetic and private observed responses cannot collide. Validate **every cache hit** against active publication rules and provenance; never trust a file merely because its key matches. Reject fingerprint mismatches, corrupted/mixed-version entries, traversal and private-root references. Approved distinct planned roots are `/home/aiq-public-synthetic-cache` and `/home/aiq-private-research-cache`; physical public/private mounting/startup isolation is WP07-owned. No public read of private cache root.

Bound public cache to **12 selection entries**, at most **2 MiB per entry**, **32 MiB disk including bounded temporary files**, **24h write-based TTL** with opportunistic cleanup (no background poller). Atomic replacement, safe temp cleanup, bounded per-key lock and cancellation must be tested. Existing 35-second lock lease must not silently allow concurrent live children; final process-global admission/lease-overlap proof is WP07-owned. Preserve safe failure on cache lock/IO/permission/cancellation and never permit observed fallback.

## 7. Focused validation and evidence

On separately authorized implementation, run focused existing .NET and Python test suites. Tests must prove: exact version/identity/source/provenance rules; UTC alignment and all 12 windows; earliest 1d/90D warmup index 0 and one-visible-daily-candle case; bounded streamed request before allocation; malformed/duplicate/extra JSON/invalid UTF-8; forbidden source/context overrides; observed/v1/private/legacy rejection for public; cache collision/TTL/corruption/fingerprint mismatch/traversal/replacement/partial writes; cancellable lock and lease races; no private/public contamination on cached failures or mixed data; exact public visible count and hidden warmup. Prove private legacy regression compatibility without any live Twelve Data/Vike API call. Verify exact changed paths and preservation hashes. Run broader tests when feasible; do not change build/package/CI scripts without an explicit amendment.

Trace acceptance criteria **AC114, AC117, AC118, AC119, AC120, AC122** and **G-2/G-4** to test names/results. Distinguish WP03 code/test completion from actual public runtime safety, which requires WP02/WP07, and final release validation WP08. No public deployment, runtime-provider activation, license assertion or F1-readiness claim on WP03 completion.

## 8. Stop conditions and Codex report format

STOP, do not silently expand scope, for: any missing listed path, need for new contract/fixture/script/config file or schema; change to approved numerical/generator/serialization contract; inability to preserve private legacy behavior; provider API/credentials; need for DI/startup/entrypoint change, new dependencies/services, Azure changes, conflicts with Section 10, tests requiring unlisted paths, uncaught public-data exposure, or unresolved lock/capture boundary. Record blockers with exact required file paths and request separate owner review.

Return a report with: current branch/HEAD vs origin, verified PR #318 and milestone/issue IDs, document/Section 10 contract precedence, existing path inventory, implementation changes **only if subsequently authorized**, test commands/results, AC/gate matrix, security negative evidence, unchanged-provider/protected-file proof, remaining WP02/WP04/WP07/WP08 dependencies, unresolved risks and explicit declaration that no GitHub PR/deployment was created.

**Owner-approval gate: this document is a proposed WP03 execution contract. Do not modify repository code until the owner explicitly authorizes its execution and exact path allowlist.**
