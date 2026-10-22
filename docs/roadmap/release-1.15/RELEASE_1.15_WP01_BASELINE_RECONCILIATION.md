# Release 1.15 WP01 - Baseline reconciliation

Date: 2026-10-21. Issue: #367. Milestone: #66.
Status: planning proposal for owner review; no implementation or feasibility acceptance.

## Authority and preserved baseline

Inspected baseline: `137bb3a8cb1e48dbdfd2650ed3992d45f269cb0e`, merged README freeze PR #377. The primary checkout was clean on `main`; planning uses a separate branch. Issue #367 is OPEN in milestone #66. Existing worktrees and owner files are preserved. GitHub reads establish issue/baseline state, not Azure runtime state.

The [WP01 prompt](prompters/release-1.15-wp01-codex-baseline-contract-and-feasibility.md) authorizes reconciliation and a planning PR. It requires separate exact-path authority for source changes and feasibility execution. This PR does not close #367, change milestone state, accept a contract on the owner's behalf, or authorize merge, provider access, deployment or public activation.

Controlling references:

- [Historical crypto architecture](../../governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md): proposed engineering contract, with remaining decisions requiring review.
- [Definition](RELEASE_1.15_DEFINITION.md), [execution plan](RELEASE_1.15_EXECUTION_PLAN.md), [file manifest](RELEASE_1.15_FILE_MANIFEST.md), [acceptance](RELEASE_1.15_ACCEPTANCE.md).
- [Release 1.14 closeout](../release-1.14/RELEASE_1.14_MILESTONE_65_CLOSEOUT.md): accepted synthetic showcase, not evidence for Release 1.15 historical ingestion, continuity or resource capacity.
- [LF policy](../../governance/REPOSITORY_LF_LINE_ENDINGS_POLICY.md) and root `AGENTS.md`/`.gitattributes`.

The Docker-first policy permits the future dual-process profile. Permission for that architecture is not proof that its isolation, supervision or resource use works. No licensing-specific engineering gate is added. Non-trading/educational scope and existing public readiness gates remain in force. Advanced Release 1.14 WP07 is not reactivated.

## Current implementation versus required behavior

Paths below are read-only observations relative to the repository root, not mutation grants.

| Inspected path | Observed baseline | Release 1.15 gap / owner |
| --- | --- | --- |
| `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataContracts.cs` | Hourly, four-hour and daily intervals; synthetic publication policy in the same file | One-minute historical contracts need a separate design; preserve synthetic identities (WP03) |
| `src/AIQuantTradingResearch.Application/MarketData/TechnicalIndicatorCalculator.cs` | Bounded fixed-point calculator, synthetic-public validation and hour-based grid checks | Arithmetic may be reusable; minute-grid compatibility is not established (WP04) |
| `src/AIQuantTradingResearch.Application/MarketData/WilderAverageTrueRangeCalculator.cs` | Existing ATR calculator also uses the interval-hour mapping | Same minute-contract gap; no claim of unchanged compatibility (WP04) |
| `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataClient.cs` | Hard-coded `1day`; whole-content string read | Completed 1m requests, bounded decoding, quota scheduling and OHLCV handling are future work (WP03) |
| `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataTimeSeriesNormalizer.cs` | Daily date format and close-price observations | Cannot obtain correct minute OHLCV by merely changing the request interval (WP03) |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs` | Existing factory forces DELETE journal mode | Existing database is not the new atomic research snapshot/checkpoint/watermark contract (WP03) |
| `src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj` | Already references `Microsoft.AspNetCore.App` | A localhost API can share the C# research process; a third API process is unnecessary (WP02/WP05) |
| `Dockerfile` | Final synthetic candidate is a Python presentation runtime with offline C# handoff | Not the proposed ongoing private ingestion plus public UI runtime (WP02) |
| `container/entrypoint.sh` | Synthetic mode executes Streamlit; legacy private branch does not implement distinct research/UI identities | New lifecycle and secret ownership need proof; do not reuse archived containment as certification (WP02/WP07) |
| `python/presentation/realtime_financial_visualization.py` | Plotly candles/volume and indicators; key/uirevision, no complete UTC viewport ownership protocol | Preserve chart while proving refresh, retention and session behavior (WP06) |
| `python/presentation/historical_market_bridge.py` | Older synthetic/Worker bridge and strict older contract | Not the Release 1.15 localhost full/unchanged client (WP05) |

The inspected installed packages are Streamlit 1.61.1 and Plotly 7.1.0. In that Streamlit implementation, `streamlit/elements/plotly_chart.py` includes the figure specification in element identity and sets `key_as_main_identity=False`. Stable key, trace IDs and `uirevision` alone therefore are not sufficient evidence. This is static inspection of the installed Windows packages, not Linux browser qualification. Current online Streamlit documentation can describe a later version; do not infer an upgrade or backport.

Release 1.14 accepted an indicator-toggle zoom reset. That historical concession does not waive Release 1.15 viewport preservation. Earlier failed automated clicks are not proof of a successful toggle or a reset.

## Dependency reconciliation

WP01 precedes WP02. WP03 depends on WP01 and the WP02 runtime contract; WP04 depends on WP03. WP05 depends on WP02-WP04; WP06 depends on WP05. WP07 qualifies WP02-WP06; WP08 records acceptance after WP07. Contract questions should be decided before dependent source work, not hidden in integration changes.

The accompanying [engineering contract](RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md) separates inherited invariants from proposed closures. The [feasibility plan](RELEASE_1.15_WP01_FEASIBILITY_PLAN.md) names literal future paths. The [acceptance/risk ledger](RELEASE_1.15_WP01_ACCEPTANCE_RISKS.md) records what remains unproven.

## This PR's exact scope

Only four new UTF-8/LF Markdown files:

1. `docs/roadmap/release-1.15/RELEASE_1.15_WP01_BASELINE_RECONCILIATION.md`
2. `docs/roadmap/release-1.15/RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md`
3. `docs/roadmap/release-1.15/RELEASE_1.15_WP01_FEASIBILITY_PLAN.md`
4. `docs/roadmap/release-1.15/RELEASE_1.15_WP01_ACCEPTANCE_RISKS.md`

No existing file is revised, including the frozen README, governing documents, source, tests, dependency pins, schemas and historical evidence. This supplements the roadmap without silently promoting its proposed contract to effective policy.

`PUBLIC_RUNTIME_VERIFIED=False`. Public activation/deployment remains NO-GO for Release 1.15 pending separate authority and evidence; this does not change the accepted Release 1.14 showcase.
