# Release 1.14 — WP02 Codex Preflight (Read-Only)

**Target:** Issue #320, milestone #65 — Independent Deterministic Synthetic Historical OHLCV Generator.
**Accepted predecessor:** WP03 issue #319 closed; PR #327 merged at `6eeeecb515c6f8b179d0f02fb8f5595c4bc0b3c0`.
**Authority:** Read-only preflight only. **Do not implement, edit, stage, commit, push, create a PR, mutate GitHub issues, or deploy.** Request separate explicit owner approval of a literal-path implementation allowlist after reporting findings.

## Controlling references

1. `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md` Section 10 (supersedes conflicts).
2. `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md` — exact algorithm, grid, serialization, hashes, windows, acceptance.
3. `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md` — WP02 prerequisites, tests, evidence and stop conditions.
4. `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md` — exact proposed WP02 paths.
5. Merged PR #327 — WP03 canonical v2 interfaces and validation behavior.

## Read-only inspection steps

1. Fetch and compare `main` / `origin/main`; confirm merge SHA and divergence. Capture branch, clean/dirty status, tracked modifications and untracked files without altering them. Hash existing files where useful. **Never clean/reset/stash/remove an untracked prompter.**
2. Read the controlling docs and actual merged WP03 contracts/interfaces; verify the provider seam (`IHistoricalMarketDataProvider`), canonical model, source/context/provenance/fingerprint requirements and compatibility constraints. Report mismatches rather than changing WP03 files.
3. Inspect the Infrastructure project file and existing tests to determine whether the two *proposed new* WP02 files are sufficient for compilation, integration and meaningful tests. Do not assume a new source file is automatically included or that DI is authorized. Do not add packages, fixtures or project-file edits.
4. Establish independent golden-verification methodology and available runtimes (for example, independent C# implementation and separately authored PowerShell or Python calculation). **Do not claim cross-runtime independence by copying generator output into test expectations.** Preflight may assess feasibility, but must not create fixture files or modify the repo.
5. Run only existing offline baseline tests if safe and available, and distinguish these from WP02 implementation evidence. Preserve local signing configuration; `-p:AutoSignTestBinaries=false` is permitted as a command-line-only workaround if needed. Do not call real market providers.
6. Identify every proposed mutation path and any required extra path. If extra paths are needed, stop and request explicit allowlist amendment before implementation.

## Proposed implementation allowlist — NOT YET APPROVED

Exactly two proposed-new files from the manifest:

- `src/AIQuantTradingResearch.Infrastructure/MarketData/Synthetic/SyntheticHistoricalMarketDataProvider.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticHistoricalMarketDataProviderTests.cs`

No other path is authorized for mutation by this preflight. No changes to WP03, DI/composition, `Program.cs`, Python bridge/UI, existing providers, project files, package manifests, configuration, fixtures, documentation, or deployment. Any prompter is an external handoff artifact, not an implied repository mutation.

## Frozen implementation specification to validate during preflight

- Identity: `btc-like-simulation`, source `independent-synthetic`, status `simulated`, generator `aiq-synthetic-ohlcv-v1`, seed `114`; canonical `aiq-candles-v2`.
- UTC grid: `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, 340 days, exactly 8,160 hourly / 2,040 four-hour / 340 daily candles; full groups aligned to origin.
- SHA input: ASCII generator version, NUL, UInt64BE(seed), UInt64BE(hourly boundary index), UInt8(lane); `U` = first SHA-256 4 bytes as UInt32BE; `Z(i,l,m)=floor(U*m/2^32)`.
- `r=i mod 480`; triangle = `r-120` if `r<=240`, else `360-r`. `P(i)=500000000+2500*triangle+Z(i,0,200001)-100000` ticks; open=P(i), close=P(i+1), high=max(open,close)+1+Z(i,1,50000), low=min(open,close)-1-Z(i,2,50000), volume=100000+Z(i,3,1000000). Price scale 4; activity scale 3. Compute P(0)..P(8160), candles 0..8159.
- Aggregation: first open, maximum high, minimum low, last close, summed volume; 4-hour and daily complete UTC buckets only.
- Twelve interval/range combinations: `1h|4h|1d` × `1D|7D|30D|90D`; exactly 250 preceding hidden warmup candles, with one visible candle for `1d/1D`. 90D visible counts: 2160/540/90; calculation totals 2410/790/340.
- Exact canonical dataset/window UTF-8 without BOM, LF including final newline; fixed header/field order and decimal/timestamp spelling from the definition; dataset SHA-256 over complete interval serialization; window SHA-256 over the entire warmup+visible calculation window, excluding operational timestamps.
- Candidate full-dataset byte counts and hashes **unverified until independently reproduced**: 1h 596926 / `5b187d5bea1fd202077c33713855f9c0518446ce6a8864a5249daa0e7e45dc29`; 4h 151407 / `d713767c7afeb5d3f535e8276e1cecb905b3f2d6c096aac5a62214f62ccc22ec`; 1d 25957 / `0fc878f11193073aa88f71b026825c9f7cbb4cb18d5c87fec2d66a9e44291c81`.
- Immutable generator/version/constants; checked integer arithmetic, bounded cancellation, fail closed on malformed requests, gaps, partial groups, overflow, invalid identity/provenance. No provider calls, licensed fixtures, startup/background generation, Python generator or external randomness.

## Future implementation exit evidence (not established by preflight)

Independent full-byte/count/hash reproduction across runtimes; independently authored row and window vectors; all twelve windows and UTC boundaries; overlap/aggregation invariants; reproducibility across locale, clock and request order; changed seed/version detection; cancellation and rejection cases; exact changed paths and preserved-provider proof. AC113/119/122, G-3/G-4. No public runtime activation before WP07 or release acceptance before WP08.

## Required preflight report

Return: (1) baseline SHA/status, (2) actual WP03 interface/contract fit, (3) exact path existence and two-file feasibility, (4) proposed independent golden strategy and any uncertainties, (5) optional offline baseline test commands/results, (6) protected/untracked-file preservation, (7) blockers or requested literal-path amendments, and (8) explicit statement **“No implementation edits made; owner implementation approval pending.”** Stop at the scope gate and ask the owner to authorize the exact two-file list or approve a justified amendment. Do not self-authorize.
