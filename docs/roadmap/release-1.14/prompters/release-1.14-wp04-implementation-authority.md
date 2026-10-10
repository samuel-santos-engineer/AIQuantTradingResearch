# Release 1.14 — WP04 Implementation Authority (Owner Approval Required)

**Status:** Proposed authority, not yet approved by repository owner.  
**Issue:** #321; milestone #65.  
**Baseline:** `bc7c79edbf254276ccc56f789676d6b073af6620`.  
**Controlling source:** Section 10 of `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md` and Release 1.14 WP04 contracts.

## Exact mutation allowlist (seven paths, no others)

1. `src/AIQuantTradingResearch.Application/MarketData/TechnicalIndicatorContracts.cs` (new)
2. `src/AIQuantTradingResearch.Application/MarketData/TechnicalIndicatorCalculator.cs` (new)
3. `tests/AIQuantTradingResearch.Application.Tests/TechnicalIndicatorCalculatorTests.cs` (new)
4. `src/AIQuantTradingResearch.Worker/HistoricalMarketDataQueryExecution.cs` (existing)
5. `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalMarketDataQueryExecutionTests.cs` (existing)
6. `python/presentation/historical_market_bridge.py` (existing)
7. `python/presentation/test_historical_market_bridge.py` (existing)

Do not edit any other path. Preserve the untracked WP04 preflight prompter and all unrelated files. No packages, project files, fixtures, generated artifacts, startup/DI wiring, provider adapters, UI changes or deployment.

## Phase A — calculator and independent proof

Implement immutable identity/parameter contracts and a bounded shared deterministic `aiq-ta-v1` calculator for SMA20, EMA20, RSI14, MACD12/26/9 and Bollinger Bands20×2. Respect frozen scale 10^12, BigInteger intermediates, symmetric ties-to-even rounding, positive closes <= 10^9 and maximum 2,410 inputs. First valid sample positions: SMA/EMA/Bollinger 20, RSI 15, MACD 26, MACD signal/histogram 34. Preserve nullable alignment, no lookahead, exact deterministic seeding and frozen parameter semantics. For Bollinger midpoint rounding compare `4A` against `400(2k+1)^2` after integer binary-search square root; negative lower bands are valid.

Embed independently derived vectors in the authorized test file, including constant/rising/falling, alternating/mixed series, signed midpoint ties, square-root midpoint boundaries, large intermediates and rejection of malformed inputs, gaps, duplicates, order errors, precision overflow and provenance mismatch. Do not derive expected values by copying calculator outputs. Report Phase A evidence before Phase B if any contract ambiguity arises.

## Phase B — Worker projection and Python validation

Preserve WP03 read-service input/cache contract (`CalculationVersion == null`). Validate the full 250-warmup calculation window and canonical provenance, compute analytics from that full window, then project only visible rows and validated `aiq-ta-v1` analytics metadata into the response. Include fixed parameters, source/context/window identity and final fail-closed publication checks. Do not expose hidden warmup values. Calculate per response; no analytics cache modification.

Update the Python bridge validator and its tests in the same phase as the Worker response shape. Strictly validate analytics metadata, decimal values, alignment, provenance, malformed and extra fields. Preserve private-provider boundaries and no external network activity. Do not assume recursively seeded indicators are identical across distinct range windows.

## Verification and stop conditions

Run focused and full Application tests, focused and full Infrastructure tests, and Python bridge tests. Record commands, test counts, expected WP05-owned UI failure separately, and changed-file inventory. Verify all twelve interval/range projections, visible-only behavior, negative cases and source isolation. Stop on contract mismatch, additional path requirement, golden-vector disagreement or unexpected regression; request new owner authority before changing scope.

**Lifecycle boundary:** Implementation authority, once explicitly approved by owner, does not authorize staging, committing, pushing, opening/merging a PR, closing issue #321, changing Azure, deploying or activating WP07. Report findings and stop for review.
