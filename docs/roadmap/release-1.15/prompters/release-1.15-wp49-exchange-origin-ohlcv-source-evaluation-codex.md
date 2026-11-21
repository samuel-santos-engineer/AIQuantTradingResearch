# Codex execution prompt — Release 1.15 WP49: Exchange-origin OHLCV source evaluation

## Starting authority and baseline

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

PR #435 has been squash-merged into `main` at `7c8d242105fbc5e3fbed4159b07f94a90a7a271c`. Verify `origin/main` and the applicable `AGENTS.md`/repository instructions before starting. Work in a clean isolated branch/worktree; do not alter the owner's dirty/untracked checkout or private preparations. Preserve WP39–WP48 evidence. WP48 baseline: 234 offline tests (150 focused C#, 23 Python workflow, 61 chart/client), with no claim that a new source is operational.

**Scope: evaluation and a concrete implementation decision only.** Create one PR and leave it OPEN/UNMERGED for owner review. Do not integrate a new provider, run real acquisition, modify production parser/credentials, create new provider accounts, or deploy anything in WP49. No new governance/licensing prerequisite or approval gate. Focus on practical engineering feasibility, including source usage terms as a factual property rather than a blocker.

## Goal

Identify the best practical **exchange-origin** historical 1-minute OHLCV feed for the existing local Docker historical chart, preserving genuine BTC and ETH observations, explicit volume units, provenance and bounded reproducible acquisition. The desired chart labels are BTC/USD and ETH/USD; do not silently substitute BTC/USDT or ETH/USDT. If only stablecoin pairs are feasible, document the mismatch and ask for a separate owner decision rather than relabeling.

## Research work

1. Inspect existing instrument identifiers, UTC/1-minute/time-window/1000-candle request contract, base-asset volume requirement, atomic two-instrument publication, provenance, importer, UI, offline test seams, and Docker runtime. Cite exact code locations and describe integration touchpoints; do not change production code.
2. Evaluate at least two credible **first-party exchange** market-data offerings, prioritizing exchanges with documented BTC/USD and ETH/USD spot candles (e.g. Coinbase Exchange and Kraken if their current official documentation supports them). Add other exchanges only when they materially improve fit. Use official API docs and official public data documentation, not unsupported blog assertions. Verify pair IDs and distinctions between base/quote, spot/perpetual, USD/USDT, trading venue and any aggregated price. Do not assume either example offers the needed history without verifying docs.
3. Compare: exact USD spot pairs; authentic 1-minute OHLCV; whether volume is explicitly **base-asset traded volume**; candle timestamp interpretation; whether incomplete/current candles are included; oldest available history and 24-hour/1000-candle retrieval feasibility; per-request row limits and pagination; ordering and gaps; API key/account requirements; public endpoint restrictions/rate limits; reproducibility; API response schema; attribution/usage constraints relevant to a local research interface. Distinguish documented facts from assumptions/unknowns.
4. Assess whether a first-party downloadable historical dataset is preferable to an API for the initial local chart. If the endpoint cannot retrieve the intended window in a single request, calculate a bounded, documented request plan **without executing it**. Never propose unlimited crawling, automatic retries or unbounded backfill.
5. Produce a source decision matrix with links, evidence dates, explicit unknowns and a ranked recommendation. Select a primary source only if the evidence supports both **BTC/USD and ETH/USD with base-asset volume**. Otherwise state `NO_SOURCE_SELECTED` and identify the smallest unresolved owner decision. Preserve Twelve Data as the existing implementation until a reviewed migration.
6. Design a concrete WP50 implementation slice: adapter interface, exact endpoints/parameters, normalization, UTC minute alignment, closed-candle filtering, OHLC/volume decimal bounds, provenance (exchange, venue, native pair, request window, retrieval timestamp), no mixing venues, atomic two-pair publication, synthetic fake-handler tests, diagnostic codes, and import/UI compatibility. Include a staged offline-first test plan and explicit single-attempt owner authorization before any future real market-data request. Do not treat public unauthenticated endpoints as preauthorized: WP49 must make **zero** live market-data calls, even HEAD/DNS/account/credit probes.

## Verification and safety

- Public documentation research is allowed; do not call exchange market-data endpoints or use API examples that execute requests. No credentials, environment-secret presence/value reads, private response or historical artifacts, Azure, Docker runtime, registry, account operations or live data acquisition.
- Do not weaken mandatory authentic OHLCV, invent volume, convert quote volume into base volume without an evidence-backed contract, label USDT as USD, synthesize missing bars, or mark synthetic fixtures authentic.
- Keep production source unchanged. Run relevant cached-only offline checks if feasible; otherwise explicitly record which checks were not run and why. Validate UTF-8/LF/no BOM and `git diff --check`. Never claim tests passed without execution.
- If provider documentation conflicts or is insufficient, report the conflict and choose a cautious decision; do not add licensing or governance approval gates. Do not invent a definitive source contract.

## Deliverables and PR

Create:
- `docs/roadmap/release-1.15/prompters/release-1.15-wp49-exchange-origin-ohlcv-evaluation.md` with official citations, comparison matrix, source decision, unresolved issues, and WP50 implementation design.
- An append-only WP49 status entry in `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`.

If a concise supplemental contract/design file is genuinely necessary, explain why and keep the diff narrow. Do not create production adapter code in this WP.

Create a PR against `main` titled `WP49: Evaluate exchange-origin BTC/USD and ETH/USD OHLCV sources`; leave it **OPEN/UNMERGED**. In the PR body include baseline SHA, exact changed paths, research sources and dates, selected source or `NO_SOURCE_SELECTED`, evidence/unknowns, verification performed, and operational exclusions. Do not merge, deploy, or request live provider access.

Maintain status flags: `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.
