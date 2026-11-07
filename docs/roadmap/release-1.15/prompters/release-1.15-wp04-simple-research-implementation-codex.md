# Codex continuation — Release 1.15 WP04 simple private research implementation

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Owner authorization: implement the narrowly scoped WP04 private calculations on the merged WP03 foundation; **no merge or deployment authority**. Date: 2026-10-24.

## Confirm baseline and protect owner work

1. Verify `main` contains merged PR #389, squash `bf4e744330986eac8234cda7a73ce866aa092242`, and merged PR #388 `b066124713f6a6f25ae7fdb0f57300ed4a83c853`. Recheck open PR #386 and #387 and any newly opened PRs. Do not merge them, cherry-pick their unmerged work, or assume their code is on `main`.
2. Read `AGENTS.md`, `.gitattributes`, Release 1.15 D3/D4/D5 contracts, WP03/WP04 foundation preflight, merged WP03 source/evidence and issue #370. Inspect exact WP03 signatures and SQLite schema before implementing. Do not guess checkpoint or writer APIs.
3. Snapshot the original dirty/untracked checkout including **every untracked Codex prompt**: literal paths, byte counts and SHA-256. Preserve all originals byte-for-byte; use a separate worktree/checkout. The newly untracked `docs/roadmap/release-1.15/prompters/release-1.15-wp03-simple-implementation-codex.md` must be inventoried, not silently discarded. Include the current WP04 prompt too if it appears in the checkout. Archive only to an explicitly authorized, collision-free documentation path; otherwise record its pending archive disposition. Never overwrite an existing prompt archive.

## Exact WP04 mutation authority

The four **new** paths proposed in the merged WP03/WP04 foundation plan are approved for this implementation:

- `src/AIQuantTradingResearch.Application/MarketData/PrivateResearchCalculator.cs` — private deterministic D3 calculation and checkpoint projection.
- `tests/AIQuantTradingResearch.Application.Tests/PrivateResearchCalculatorTests.cs` — independent numerical, boundary and replay tests.
- `tests/AIQuantTradingResearch.Application.Tests/PrivateResearchReference.cs` — independent test-only reference, not a wrapper around production output.
- `tests/AIQuantTradingResearch.Infrastructure.Tests/CommittedResearchProjectionTests.cs` — actual WP03 store integration, transactional and restart tests.

Two **existing** WP03 paths may be modified only as needed to bind the same-transaction calculation and persisted calculated state, after inspecting the merged implementation:

- `src/AIQuantTradingResearch.Application/MarketData/ResearchStateContracts.cs` (A01) — minimal immutable calculated-state/projection contracts.
- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteResearchStateStore.cs` (A05) — minimal atomic persistence/read of calculated projection and checkpoint, preserving WP03 writer/recovery behavior.

These six paths are the complete source/test write scope. Do not change the SQLite schema file, ingestion service, Worker Program/DI, provider adapter, Dockerfiles, Python, dependencies, lockfiles or unrelated tests without returning for a literal-path amendment. If the merged schema cannot support required durable calculated state without editing `ResearchStateSchema.cs`, **stop that part and propose the smallest explicit amendment**; do not smuggle schema changes into another file or claim full integration. No second writer, database, service or cache authority.

Documentation writes, if needed, are limited to dated append-only evidence in the existing `docs/roadmap/release-1.15/RELEASE_1.15_WP03_WP04_FOUNDATION_PREFLIGHT.md` and `docs/roadmap/release-1.15/RELEASE_1.15_FILE_MANIFEST.md`. A new evidence or prompt archive file requires naming its exact path and owner approval first. Do not modify the pending #387 documents.

## Functional scope: smallest truthful implementation

- Consume only genuine committed BTC/USD and ETH/USD one-minute OHLCV from WP03. Never compute research from D5-transformed values, fabricate candles or label incomplete/unavailable results as Ready.
- Implement SMA(20), EMA(20), Bollinger Bands(20,2), RSI(14), MACD(12,26,9), ATR(14), trend score (-3..3), and regime classifications under the accepted D3 algorithm, including precise warmup/nulls, 100-observation warmup + 300 **prior** observations for calibration, first eligible regime evaluation on candle 401, percentile boundaries and precedence. Preserve UTC gap/segment boundaries, corrections and invalidation; only emit meaningful trend transition/SegmentStart events.
- Prefer readable deterministic `decimal`/integer-scale calculations using existing pinned .NET libraries. Do not add custom numerical frameworks or dependencies. Compare against independently derived numerical vectors and documented tolerances; no circular test expectations.
- Keep recursive state/checkpoints durable and aligned with the **same WP03 committed generation/version and transaction**. Do not let a separate calculator writer advance independently. Reject stale projections and mixed generations. On restart, correction, retention and replay, reconstruct deterministically from valid checkpoint/evidence; where evidence is insufficient, mark research unavailable/recalibrating or begin a new truthful segment rather than guessing.
- Make the calculated-state reader immutable, coherent and private; it is the future WP05 source of truth. No D4 HTTP endpoint, D5 transform, UI or provider activation in this PR.
- Be practical: implement the straightforward correct path first. No distributed leases, complex schedulers, elaborate abstractions or unnecessary security gates. Do not weaken existing WP03 fail-closed recovery or data integrity.

## Test and PR expectations

- Run focused new Application and Infrastructure xUnit suites **offline**, plus relevant existing WP03 ingestion/store/recovery tests and current numerical regressions; no `dotnet restore` or package install unless explicitly permitted. Include boundary vectors (warmup, 401st candle, exact threshold equality, zero denominators, gaps, corrected candle, restart/checkpoint, stale projection, failed transaction, event retention and 1,000-candle pruning).
- Distinguish calculation-only tests from true SQLite-integrated tests. If a required contract/schema change is outside the six-path scope, finish safe standalone tests, document the precise blocker and request a minimal amendment rather than faking integration success.
- Report exact commands, counts, SDK/runtime, failures, performance if measured, and source hashes. Existing #389 168 tests are historical evidence unless rerun; no CI/Linux/Azure claim from Windows local results.
- All changed text UTF-8 no BOM, LF and terminal newline. Independently scan CRLF, run `git diff --check`, verify links and unchanged owner-file hashes. Preserve all untracked prompt originals and explicitly report archival status.
- Open **one narrowly scoped WP04 implementation PR**, leave it **OPEN/UNMERGED** for owner review. Do not close WP03/WP04 issues or declare WP04 accepted automatically.

## Strict environment policy

**Production is completely off limits**, including read-only inspection, URLs, secrets, settings, Azure resources and registry tags. Do not publish or deploy anywhere under this prompt. Future publication and milestone assessment must use **staging only**, with separately approved WP07 authorization. No live Twelve Data calls, provider keys, registry push, Docker promotion or Azure commands. Keep two independent non-root Linux/amd64 Docker images as the target, one active service API key per environment, and the accepted standard synchronous HTTPS/native-DNS timeout limitation; do not implement DNS workers or key rotation.

`PUBLIC_RUNTIME_VERIFIED=False` until genuine staging qualification and owner acceptance.

## Return report

Give verified baseline and PR statuses, exact changed paths, implemented indicator/regime and checkpoint behavior, tests actually executed, blockers or requested exact-path amendments, original/untracked prompt inventory and archive dispositions, new PR URL/head SHA, and confirmation that no production/staging/provider/deployment operations occurred.
