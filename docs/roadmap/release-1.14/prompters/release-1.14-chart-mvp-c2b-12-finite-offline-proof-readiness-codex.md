# Release 1.14 Chart MVP — C2B-12 Codex authority
## E4 finite offline proof readiness and authorization proposal — documentation only

**Authority status:** Owner requested the next Codex file after PR #341. This document authorizes **planning and evidence review only**. It does **not** authorize running tests, generating candles, computing/exporting snapshots, changing production or test source, committing generated data, Azure experiments, deployment, or public activation. Any later executable proof needs a new explicit owner authority with literal paths and finite ceilings.

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`.

**Starting observation — verify afresh:** PR #341 merged as `fc3bd493009dc95ba0686b48733b4e3deecba4c2`, with three C2B-11 Markdown documents. The E1–E8 recommendations were conditionally approved as *design directions*, not qualified engineering mechanisms. T ceilings remain TBD.

## 1. Non-negotiable boundary

1. Preserve `PUBLIC_RUNTIME_VERIFIED=False`; `/api/v1/dataset`, `/api/v1/candles`, and `/api/v1/indicators` remain safely unavailable until separately qualified. Public activation/Azure deployment are **NO-GO**; advanced WP07 work remains **ON HOLD**.
2. Never modify, switch, clean, stash, reset, inspect protected private artifacts in, or otherwise disturb the dirty WP07 worktree/archive. Use an independently verified clean checkout.
3. No modifications to production code, tests, packages, schema, fixtures, golden outputs, snapshots, Docker, CI, Azure, credentials, private/provider data, Project metadata, branch protections, or historical authority/owner registers.
4. No generator/calculator execution, oracle execution, build, restore, test run, benchmark, fault injection, materialization, export, release promotion, or public read activation. Read-only source and document inspection is permitted in a safe checkout.
5. Preserve `aiq-synthetic-ohlcv-v1`, seed 114, coverage `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, 1h/4h/1d × 1/7/30/90 days, 250 calculation-only warmup rows, all frozen fingerprints and exact C# arithmetic/rounding/null/indicator semantics. No rolling/live/provider fallback; public identity remains **BTC-like Market Simulation — Synthetic Historical OHLCV**.
6. A planning PR merge or design approval never constitutes implementation, proof, export, deployment, or activation authority.

## 2. Preflight and provenance

- Verify PR #341 merged, exact merge SHA, files, review/protection state as relevant, and current remote `main`. Record any divergence; do not assume a historical baseline is still HEAD.
- Inspect C2B-10 engineering options, finite proof/qualification plan and C2B-11 owner register/evidence, plus current canonical Release 1.14 strategy/definition/execution/manifest and C2B-7 frozen specification.
- Identify the exact existing friend-assembly, generator, calculator, serialization and independent-reference test interfaces by **read-only** inspection. Record source paths and relevant method signatures without modifying them.
- Preserve the accepted C2B-11 register byte-for-byte. The four previously authorized Markdown hard breaks are historical and do not create a general whitespace exception.
- If the repository baseline, accepted contracts or trust assumptions conflict, **STOP** and record the conflict rather than selecting a workaround.

## 3. C2B-12 deliverables — proposed E4 proof, not execution

Produce a small, independently reviewable **documentation-only** packet addressing:

**A. Exact E4 proof matrix.** Define every comparison for all three complete datasets (expected 8160/2040/340 rows), twelve calculation/visible selections, existing independent dataset/window/visible fingerprints and canonical bytes. Map the independent test-only oracle to exact frozen SMA20, SMA-seeded EMA20, Wilder RSI14, MACD12/26/9, population Bollinger20×2, scale 10^12, ties-to-even, integer/rational arithmetic, square-root midpoint, first-valid/null positions, 250 warmup rows, gaps, order, provenance, identity and negative cases. Specify how independence is demonstrated; never use production outputs as the oracle's expected values or invent tolerances/derived hashes.

**B. Literal file and output proposal.** Evaluate the previously proposed candidate paths, without creating them:
- `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`

Document any exact additional *necessary* paths as separate requests, not implied permissions. Specify allowed test-only transport (in-memory preferred), temporary/output root, logs and sanitized evidence. Explicitly prohibit exporter seams, persistent candle/indicator fixtures, snapshot files and production Python code in this first proof.

**C. Finite resource and abort worksheet.** Propose measurable, bounded *candidate* values for wall time, CPU time, RSS, subprocess count, test repetitions, total output/temp/log bytes, disk usage, concurrency, cancellation/kill grace and cleanup. Distinguish **proposed safety ceilings** from unmeasured performance/acceptance targets. Do not fabricate measurements or present any candidate as owner-approved. If credible ceilings cannot be justified from static evidence, mark TBD and require owner selection before running anything. Explicitly account for Windows PowerShell 5.1, .NET and CPython execution, no implicit restore/network access, and runaway-process containment.

**D. Independent review and STOP protocol.** Require an independently authored oracle/spec review before production comparisons; enumerate mismatch, inconsistent hashes, unexpected file output, exceeded resource ceiling, insufficient independence, or ambiguous cleanup as hard STOP. Define exact owned-path cleanup, non-destructive failure evidence, no weakening of arithmetic rules, and no change to frozen constants to make tests pass.

**E. Stage separation and dependencies.** Distinguish (i) owner approval of this plan, (ii) separate narrow executable local E4 proof authority, (iii) independent evidence acceptance, (iv) later E1/E2/E3/E5–E8 design and qualification, (v) separate offline materialization/export, integration, Azure/browser qualification, deployment and activation. No stage inherits authority from a preceding stage.

## 4. Permitted changes and PR handling

Create a planning branch from freshly verified clean `main`. Only **new Markdown** files under `docs/roadmap/release-1.14/prompters/` with prefix `release-1.14-chart-mvp-c2b-12-` may be added:

1. `release-1.14-chart-mvp-c2b-12-e4-finite-offline-proof-readiness-plan.md`
2. `release-1.14-chart-mvp-c2b-12-e4-proof-coverage-and-resource-worksheet.md`
3. `release-1.14-chart-mvp-c2b-12-readiness-evidence.md`
4. `release-1.14-chart-mvp-c2b-12-finite-offline-proof-readiness-codex.md` (preserve this authority verbatim, if included).

Do not change any other path, including existing canonical policy and C2B-11 documents. If a necessary amendment lies outside this allowlist, STOP and request authority. No automatic merge or branch deletion. Open **one C2B-12 documentation-only PR**, leave it **open and unmerged** for owner review.

## 5. Validation and final report

- Verify exact path inventory, Markdown links/headings, frozen identifiers and numerical contract references, `git diff --check`, no unintended whitespace exceptions, and absence of source/data/test/output changes.
- State clearly that **no executable tests, builds, restores, oracle runs, generator calls, snapshot creation/export, Azure/browser probes or deployment** occurred. Inherited results must be labeled inherited, not fresh.
- Record base/head SHAs, changed files, source references, remaining TBD ceilings, unresolved proof obligations and any STOP findings.
- Explicitly preserve `PUBLIC_RUNTIME_VERIFIED=False`, dataset/indicator unavailability, NO-GO activation/deployment and advanced WP07 ON HOLD.

**Exit condition:** A reviewable, bounded E4 proof *proposal* and explicit next owner decisions; no executable work authorized and no claims of qualified parity, durability, isolation or Azure F1 feasibility.
