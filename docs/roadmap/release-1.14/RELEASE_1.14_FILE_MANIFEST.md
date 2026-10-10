# Release 1.14 File Manifest — Current Authority and Future Proposals

## Current exhaustive mutation allowlist

Under the owner-issued approved-contract documentation update, edit only these existing local untracked files in place; create no additional files:

1. `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
2. `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`

All three were absent at the original creation preflight and present as untracked/uncommitted artifacts at the approved-contract update preflight on 2026-10-10. They remain local review artifacts. No staging, branches, commits, PRs or lifecycle actions are authorized. The [definition](RELEASE_1.14_DEFINITION.md) records Section 10 precedence, owner-approved engineering contract, candidate hashes and provisional unmeasured limits; the [execution plan](RELEASE_1.14_EXECUTION_PLAN.md) owns dependencies/tests/acceptance and pending independent evidence. Neither enlarges this allowlist.

Baseline: `main` and fetched `origin/main` at `893e54c058136a027bd9f8d2ad69019a2901aafc`, with merged PR #316/#317. Recheck future baselines; do not fix this SHA as an implementation predecessor.

## Protected paths and preserved worktree

Everything outside the three current paths is protected in this WP, including:

- `README.md`
- `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`
- `docs/project/ROADMAP.md`
- all older Release 1.13 records and existing prompters
- source, tests, UI assets, package/lockfiles, schemas and configuration
- `Dockerfile` and `container/entrypoint.sh`
- pre-existing untracked `imgs/showcase/Release 1.14 Governance Amendment.png`
- pre-existing untracked `docs/roadmap/release-1.14/prompters/release-1.14-wp01-codex-planning-artifacts-authority.md`

Do not clean, reset, stash, delete, rename, format or stage unrelated work. The policy status correction and roadmap reconciliation are future owner decisions, not current edits. No GitHub issues/Projects, provider requests, credentials, cloud changes or deployment.

## Future proposed ownership — no present execution permission

Each future WP needs its own owner-authorized exhaustive literal allowlist. `Existing` means located during repository inspection; `Proposed new` names a design candidate, not an existing or authorized artifact. Exact new names and any additional required files require approval. No wildcard/directory permission or implied permission for associated tests exists.

### WP03 — Canonical/provenance/publication/cache/stdio

Existing proposed mutation paths:

- `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataContracts.cs`
- `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataCacheContracts.cs`
- `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataReadService.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/Cache/AtomicFileHistoricalMarketDataCache.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/Cache/AtomicFileHistoricalMarketDataQueryLock.cs`
- `src/AIQuantTradingResearch.Worker/HistoricalMarketDataQueryExecution.cs`
- `python/presentation/historical_market_bridge.py`
- `tests/AIQuantTradingResearch.Application.Tests/HistoricalMarketDataContractsTests.cs`
- `tests/AIQuantTradingResearch.Application.Tests/HistoricalMarketDataReadServiceTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/AtomicFileHistoricalMarketDataCacheTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/AtomicFileHistoricalMarketDataQueryLockTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalMarketDataQueryExecutionTests.cs`
- `python/presentation/test_historical_market_bridge.py`

Owns `aiq-candles-v2`, `aiq-historical-query-v2` and `aiq-historical-market-data-cache-v2` identity/provenance, fail-closed policy and negative cache/protocol tests. Enforce definition request/grid/count/cache/retention/disk/lock bounds and approved separate cache roots. Reject public v1/observed/private/source overrides, duplicates/malformed fields and fingerprint mismatches. Preserve private legacy compatibility without accepting observed data publicly. No schema/adapter changes or public activation.

### WP02 — Synthetic generator

Proposed new paths:

- `src/AIQuantTradingResearch.Infrastructure/MarketData/Synthetic/SyntheticHistoricalMarketDataProvider.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticHistoricalMarketDataProviderTests.cs`

Owns the approved `aiq-synthetic-ohlcv-v1` seed-114 generator/aggregation, exact UTF-8/LF serialization and dataset/window fingerprints, generation bounds and independent golden/invariant/count tests. Three row vectors are transcribed from prior cross-runtime reference checks; full-dataset byte counts/hashes remain candidates requiring independent verification before release acceptance. Place independently authored fixtures in the authorized test file unless a separate fixture path is explicitly approved. No fixture-directory permissions. Requires accepted WP03, incorporated approved WP01 engineering contract and individual execution authority.

### WP04 — Shared indicators and projection

Proposed new paths:

- `src/AIQuantTradingResearch.Application/MarketData/TechnicalIndicatorContracts.cs`
- `src/AIQuantTradingResearch.Application/MarketData/TechnicalIndicatorCalculator.cs`
- `tests/AIQuantTradingResearch.Application.Tests/TechnicalIndicatorCalculatorTests.cs`

Existing proposed integration/test paths:

- `src/AIQuantTradingResearch.Worker/HistoricalMarketDataQueryExecution.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalMarketDataQueryExecutionTests.cs`
- `python/presentation/historical_market_bridge.py`
- `python/presentation/test_historical_market_bridge.py`

Owns `aiq-ta-v1` exact scale-10^12 integer/rational ties-to-even arithmetic and integer square-root midpoint rules, canonical/display precision, first-valid/null/warmup semantics, independent expected vectors and safe visible-only projection. Definition reference cases are not independent implementation test results. No source-specific calculations or packages.

### WP07 — Composition, startup and private preservation

Existing proposed mutation paths:

- `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `container/entrypoint.sh`
- `python/presentation/historical_market_bridge.py`
- `python/presentation/realtime_financial_visualization.py`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/DependencyInjectionTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalProductionCompositionAcceptanceTests.cs`
- `python/presentation/test_historical_market_bridge.py`
- `python/presentation/test_market_research_ui.py`

Owns secret-free public composition/startup without private acquisition Worker, from-scratch child environment, global admission and anonymous System Health isolation. Enforce definition child/capture/chunk/deadline/cancellation/retry/session/health/CPU limits and no lease-expiry overlap. Existing tests are candidate seams; meaningful new startup/capture/global-admission tests need an explicit literal amendment. Preserve provider sources and private close-observation behavior; new private OHLCV/failover is deferred. WP07 runtime acceptance also consumes accepted WP02; design work may start after WP03.

### WP05 — Synthetic public research UI

Existing proposed mutation paths:

- `python/presentation/realtime_financial_visualization.py`
- `python/presentation/test_market_research_ui.py`

Owns controls, candlesticks/activity volume, overlays/panels, UTC hover, labels/timestamps, safe states and accessible alternative. Existing Plotly/Streamlit pins are reused unchanged. Dependencies: accepted WP02/WP04/WP07.

### WP06 — Keynote and dedicated visual governance page

Existing proposed mutation paths:

- `python/presentation/realtime_financial_visualization.py`
- `python/presentation/test_market_research_ui.py`

Proposed new paths requiring explicit UI/asset/test authorization:

- `python/presentation/data_governance_page.py`
- `python/presentation/test_data_governance_page.py`
- `python/presentation/assets/data_governance.svg`

The SVG must render Section 10 topology with scalable geometry, readable 320/768/1280px layouts, accessible name/title/description, complete adjacent text equivalent and delivered/planned labels. Do not create assets now or substitute the untracked infographic. Approved existing-app navigation is `?page=market-research|ml-studies|system-health|data-governance`, same-tab static links, safe missing/unknown/repeated values and loop-free radio synchronization. No new `/architecture/data-governance` route, JavaScript or remote renderer. WP06 owns fresh-session/direct-link/reload/Back/Forward/keyboard/screen-reader tests; any extra path needs separate authority.

### WP08 — Verification and acceptance evidence

Existing test paths above are run-only unless separately approved for correction. Proposed new documentary path:

- `docs/roadmap/release-1.14/RELEASE_1.14_ACCEPTANCE.md`

This path is not currently authorized. WP08 owns integrated AC/gate reconciliation, independent-gate results, manual accessibility/security/history review and separately authorized CPU/RSS/container/latency/concurrency/cache profiling/current F1 evidence against every provisional definition limit. Targets are not measured Azure quotas or readiness. No implied script/test/deployment-file mutation. An offline-only record must disclose pending runtime/cloud evidence.

## Shared-file sequencing

| Shared path | Sequence / ownership |
| --- | --- |
| `src/AIQuantTradingResearch.Worker/HistoricalMarketDataQueryExecution.cs` | WP03 protocol/policy → WP04 analytics projection |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalMarketDataQueryExecutionTests.cs` | WP03 protocol negatives → WP04 indicator projection |
| `python/presentation/historical_market_bridge.py` | WP03 protocol → WP04 outputs → WP07 environment/capture integration |
| `python/presentation/test_historical_market_bridge.py` | Same ordered validation ownership |
| `python/presentation/realtime_financial_visualization.py` | WP07 safe System Health → WP05 synthetic research → WP06 keynote/page navigation |
| `python/presentation/test_market_research_ui.py` | Same ordered UI/negative-test ownership |

WP04 and WP07 cannot concurrently edit shared bridge files; explicitly sequence WP04 projection before final WP07 bridge integration. WP02/04/07 dependencies never grant each other's paths. Re-run relevant integration checks when shared files change; reject stale acceptance evidence.

## Preservation-only provider files and regression evidence

The following existing source paths were observed and are read-only/preservation-only under the proposed release scope. Directory membership grants no mutation permission.

Vike:

- `src/AIQuantTradingResearch.Infrastructure/MarketData/Vike/VikeConfiguration.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/Vike/VikeHistoricalMarketDataProvider.cs`

Twelve Data:

- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataClient.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataConfiguration.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataErrorResponse.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataNormalizationFailure.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataNormalizationResult.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataObservationSource.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataTimeSeriesMetadata.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataTimeSeriesNormalizer.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataTimeSeriesResponse.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataTimeSeriesValue.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataTransportResult.cs`

Existing run-only offline regression paths:

- `tests/AIQuantTradingResearch.Infrastructure.Tests/VikeHistoricalMarketDataProviderTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataClientTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataConfigurationTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataObservationSourceTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataTimeSeriesNormalizerTests.cs`

Preserve qualification/history evidence, including `docs/roadmap/release-1.13/RELEASE_1.13_ACCEPTANCE.md`, `docs/roadmap/release-1.13/WP07_AZURE_F1_VALIDATION.md` and `docs/roadmap/release-1.14/prompters/release-1.14-wp01-codex-vike-hyperliquid-data-source-qualification.md`. Conditional qualification is not current public-rights clearance. Existing Twelve Data close-observation implementation is not canonical OHLCV support.

## Additional-authority conditions

Stop before any unlisted file edit, including new tests, fixtures, scripts, UI assets, route files or project manifests. Require separate literal authorization for `Dockerfile`, routing/configuration, schemas/migrations, package/lockfiles, Azure/cloud changes, private canonical OHLCV adapters, credential use or live provider validation. New packages, services and paid infrastructure remain outside release policy unless separately governed by an explicit policy revision.

Owner approval on 2026-10-10 resolves the dataset-coverage blocker: inclusive start `2025-10-26T00:00:00Z` through exclusive end `2026-10-01T00:00:00Z` gives 340 complete UTC daily buckets, 8,160 hourly candles and 2,040 four-hour aggregates. Daily 90D plus 250 preceding warmup totals 340; hourly/four-hour 90D requests total 2,410/790. All twelve combinations and the one-visible-daily-candle case remain unchanged. These are planning arithmetic assertions, not generated fixtures or implementation permission.

The definition incorporates owner-approved generator/serialization/numerical contracts, v2 canonical/protocol/cache, query navigation/SVG, isolation and provisional unmeasured resource limits. Remaining independent evidence gates are full-dataset goldens (WP02), analytical implementation tests (WP04), isolation/capture/admission (WP03/WP07/WP08), browser/accessibility (WP06/WP08) and profiling/current F1 acceptance (WP08 under separate authority). Private rights remain conditional; private canonical OHLCV/failover is deferred and not a release prerequisite. No candidate hash is silently promoted to an independently verified fixture. Existing/proposed-new path lists and dependency graph are unchanged. File proposals never authorize execution.

Validation ownership follows the execution-plan table: WP03 request/cache/lock bounds; WP02 complete-base/aggregate bounds and independent hash verification; WP04 numerical/calculation bounds; WP07 capture/process/environment/health controls; WP05 visible/display semantics; WP06 navigation/SVG/text; WP08 every integrated provisional resource limit and current F1 evidence. Extra test/fixture/script/deployment paths remain proposed only and require separate literal authority before creation.

## Current documentation-local validation ownership

Codex checks the three artifacts for mutually consistent baseline/AC/gate/dependency/path scope, relative links and existing/new labels; all twelve counts and one-candle semantics; dataset/warmup feasibility; synthetic units/coverage/generated-at; no provider rights/readiness claims; and no unintended secret content. Review whitespace of untracked artifacts directly in addition to `git diff --check`.

Final proof must show unchanged HEAD/branch, no tracked-file diff or staged changes, only these three new files plus preserved pre-existing untracked entries, and unchanged protected untracked file hashes. Leave all artifacts uncommitted for owner review. Do not execute app builds/tests or install packages under this documentation authority.

**DOCUMENTATION-ONLY WP01 — NO IMPLEMENTATION, COMMIT, PR OR DEPLOYMENT AUTHORIZED.**

## Subsequent owner-authorized planning PR publication set

On 2026-10-10 the owner explicitly authorized publishing all existing untracked/updated files in a documentation-only GitHub PR and merging it to close WP01 planning. This later authority supersedes the earlier local-only restrictions solely for branch/stage/commit/push/PR/merge lifecycle. The exhaustive observed eight-file publication set is:

- `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-wp01-codex-approved-contract-documentation-update.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-wp01-codex-engineering-contract-freeze.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-wp01-codex-planning-artifacts-authority.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-wp01-codex-synthetic-coverage-reconciliation.md`
- `imgs/showcase/Release 1.14 Governance Amendment.png`

Preserve authority prompter substance and image bytes exactly. The engineering-freeze prompter receives only whitespace normalization of its two initial Markdown hard breaks for clean Git whitespace validation. The image is an illustrative historical artifact, not normative architecture or verified compliance evidence; the definition records that distinction. Preserve README, controlling governance, roadmap, source/tests/dependencies and deployment files. Future proposed implementation paths remain unauthorized. Required independent goldens, numerical tests, isolation, accessibility and profiling gates remain pending after planning publication. No issue/Project mutation, implementation, provider activation or deployment follows from merge.
