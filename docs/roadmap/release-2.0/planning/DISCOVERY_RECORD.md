# Release 2.0 ? Read-only discovery

Observed2026-10-17. Refreshed origin/main3b29e5119345a10ba98f6c686d88976143025003. No model fit/build/runtime/browser test, Docker operation, registry write, Azure mutation, issue or milestone update performed. Work is isolated; existing owner documents, README files and archives remain untouched.

## Milestone60

Title: **Phase 5 - Release 2.0: Lightweight Machine Learning Evaluation**. State=open; due_on=null; open_issues=0; closed_issues=0. REST linked-issue query state=all returned[]. Description: one narrow deterministic ML hypothesis, temporal evaluation, reproducible metrics/baseline comparison; governed scikit-learn Logistic Regression preferred unless blocked; not a full ML platform/backtesting. No invented issues/deadlines. Selected new documentation folder: docs/roadmap/release-2.0/planning; preserve existing untracked owner authority in its parent.

## Source inventory

| Path | Observed role |
|---|---|
| Dockerfile |Pinned SDK/Python3.13 Linux bases; build, immutable-handoff, synthetic-candidate; explicit release target needed|
| requirements.txt; requirements-linux.lock |Existing scikit-learn1.9.0,numpy2.5.1,scipy1.18.1,threadpoolctl3.7.0; no upgrades proposed|
| container/entrypoint.sh |Public-synthetic sanitized environment, no runtime provider/Worker; final bootstrap permanently drops identity|
| src/AIQuantTradingResearch.Worker/ImmutableSyntheticChartHandoff.cs |Offline12-member C# export; candidate-v4,1MiB/member and4MiB total bounds|
| src/AIQuantTradingResearch.Worker/LocalSyntheticChart.cs |Visible projections from full250-warmup C# calculations; separate ATR version|
| src/AIQuantTradingResearch.Application/MarketData/TechnicalIndicatorCalculator.cs |Frozen SMA/EMA/RSI/MACD/population Bollinger|
| src/AIQuantTradingResearch.Application/MarketData/WilderAverageTrueRangeCalculator.cs |Frozen C# Wilder ATR|
| python/presentation/immutable_synthetic_chart.py |Pinned finite reader, nonqueuing lock/deadline; manifest0c7b3a415d551dde753382c741f581d8aab4683880228bcfd904e53a0823cf84|
| python/presentation/local_synthetic_chart.py |Strict canonical transport validation|
| python/presentation/realtime_financial_visualization.py |Plotly chart, six toggles, System Health and bounded busy-only retry|
| python/presentation/test_local_synthetic_chart.py; test_immutable_synthetic_chart.py; test_public_preview_api.py |Focused UI/reader/API regressions|
| tests/AIQuantTradingResearch.Application.Tests/TechnicalIndicatorCalculatorTests.cs; WilderAverageTrueRangeCalculatorTests.cs |Independent formula vectors|
| tests/AIQuantTradingResearch.Infrastructure.Tests/LocalSyntheticChartTests.cs; ImmutableSyntheticChartHandoffTests.cs |All12 projections/export|
| python/validation/scientific_stack_validation.py |Legacy scientific-stack smoke, not an ML experiment or Release2.0 native-host certificate|
| eng/build.sh |Existing helper, not a substitute for actual image qualification|

No checked-in Release2.0 Linux native-browser harness was found; future harness availability is a decision/proof obligation. Legacy host harnesses/narratives are references only. No existing roadmap/release definitions modified.

## Read-only Azure/deployment-isolation observations

Development: aiqr112wp05wus27f5eabb5; rg-aiq-r112-wp05-wus2-7f5eabb5; West US2; Running. Existing plan asp-aiq-r112-wp05-wus2-7f5eabb5, skuF1/tierFree. CLI reserved property returned null, not independent OS proof; Linux Docker is the current configured image path and future hard target.

Production: aiqr112wp035ec325382770; rg-aiq-r112-wp03-wcus-5ec325382770; West Central US; immutable under this authority. Only read-only configuration inspection, no production HTTP/browser test.

Both configured images: sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07. Both scmType=None, empty startup override; source repoUrl/branch null. Safe setting subset: AIQ_RUNTIME_MODE=public-synthetic, WEBSITES_ENABLE_APP_SERVICE_STORAGE=false; DOCKER_ENABLE_CI absent. No secrets inspected. Workflow REST count0, repository hooks[]. Main protections enforce admins/conversation resolution, required approvals0, no required status-check context returned.

No configured merge-to-production deployment path was observed, permitting this documentation-only merge without invoking a known production trigger. This does not exclude unknown out-of-band automation. Recheck workflows/hooks/source/CD/immutable production digest before each future merge; if a trigger appears, stop for owner decision, never disable it under this authority. No webhook invoked/generated. Configured image readback is not a new runtime/capacity certificate; earlier deployment behavior remains historical owner-reported evidence.

## Primary references checked2026-10-17

- [Leakage/pipelines](https://scikit-learn.org/stable/common_pitfalls.html): fit learned preprocessing only on training data.
- [TimeSeriesSplit](https://scikit-learn.org/stable/modules/generated/sklearn.model_selection.TimeSeriesSplit.html): ordered splits/gap; still require explicit target-boundary checks.
- [LogisticRegression](https://scikit-learn.org/stable/modules/generated/sklearn.linear_model.LogisticRegression.html): numerical variability requires pinned qualified environment; current online documentation is not permission to upgrade existing1.9.0.
- [Azure quotas](https://learn.microsoft.com/en-us/azure/app-service/web-sites-monitor): Free/shared CPU quota exhaustion can stop apps until reset.
- [Shared capacity](https://learn.microsoft.com/en-us/azure/app-service/overview-hosting-plans): no dedicated-core/continuous-availability assumption for F1.

No current remaining quota, metric freshness, cold-start distribution or evaluation/report capacity measured. Numeric limits in the plan are proposals, not platform guarantees or observed results. No issues, due dates or production commitments added.
