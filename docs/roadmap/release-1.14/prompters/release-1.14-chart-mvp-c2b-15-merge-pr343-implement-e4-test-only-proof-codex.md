# Release 1.14 Chart MVP — C2B-15 Codex authority
## Guarded merge of approved PR #343 and narrow E4 test-only implementation

**Authority status:** The owner explicitly approved PR #343 and requested this next Codex authority. This authorizes (1) a guarded merge of the **exact approved PR #343** and (2) narrowly scoped **test-only source creation**. It does **not** authorize running tests, builds, restores, generators, the Python oracle, benchmarks, snapshots, exports, Azure experiments, deployment or public activation. Open a documentation/test-source PR and leave it unmerged for owner review.

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`.

**Accepted PR reference — verify afresh:** PR #343, base `614e5490e2aaf37c5264a0bf0afccbe6f462ea44`, accepted head `bf84a172bbb92615da6f223f4670a5ab294c724a`, four C2B-13 Markdown files; the last revision simplified the local E4 proof contract. If head, base, changed-file inventory, material patch, mergeability or protection state differs, STOP and request owner review.

## 1. Immutable project boundaries

1. This is a **showcase lab without production intention**. Prioritize the simplest correct path toward a working synthetic historical candlestick/indicator demo. Do not invent enterprise-grade governance, Job Objects, native helpers, kernel resource accounting, a custom runner or additional design phases.
2. `PUBLIC_RUNTIME_VERIFIED=False`. Public `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators` remain safely unavailable; Azure deployment and public activation are **NO-GO**; advanced WP07 is **ON HOLD**.
3. Do not inspect, switch, reset, stash, clean, modify or otherwise disturb the protected dirty WP07 worktree/archive or private/provider data. Work from an independently verified clean checkout.
4. Preserve `aiq-synthetic-ohlcv-v1`, seed 114, fixed UTC coverage `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, complete 1h/4h/1d datasets, twelve 1/7/30/90-day selections, 250 hidden calculation warmup rows, frozen fingerprints and exact `aiq-ta-v1` C# semantics. Public label: **BTC-like Market Simulation — Synthetic Historical OHLCV**.
5. No changes to production source, projects, packages, manifests, frozen expectations, CI, Docker, GitHub protections, Project metadata, credentials, Azure, public endpoints or historical owner registers. No new Python production port, exporter or persistent market-data fixture.

## 2. Merge approved PR #343 safely

1. Fetch fresh GitHub PR metadata, full changed-file inventory and patch, current reviews/threads/checks/protections and exact head/base. Require exact accepted head `bf84a172bbb92615da6f223f4670a5ab294c724a` and no material divergence.
2. Merge via normal GitHub protections with an exact-head guard. No bypass, force push, branch deletion or unrelated cleanup. Record final merge SHA and state.
3. Fetch `main` and verify `main == origin/main`, clean tracked worktree, 0/0 ahead/behind, and ancestry of #342 merge `614e5490e2aaf37c5264a0bf0afccbe6f462ea44`. Preserve all pre-existing untracked files byte-for-byte and exclude them from the new PR. STOP on divergence or uncertain ownership.
4. Create a fresh `planning/release-1.14-chart-mvp-c2b-15-e4-test-only` branch from verified merged `main`.

## 3. Exactly permitted new test-source files

Create only these two new files, provided neither already exists; if either exists or requires modifications to any other path, STOP and request a revised literal allowlist:

- `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`

Use the existing Infrastructure.Tests friend assembly, fixture-copy rule and public calculator contracts. No `.csproj` edits, new dependencies, scripts, helpers, runner projects or application code. If a necessary seam or compilation dependency cannot be satisfied within these two files, document the blocker rather than broadening scope.

## 4. Implementation contract: exact independent E4 proof

- Implement a test-only CPython reference independently derived from the frozen specification, not translated/copied from the C# implementation, existing production output or embedded reference. Use Python standard-library exact integer/rational arithmetic; no floating-point tolerance, network, pip or external provider access. Annotate specification-to-formula mapping and independent derivations in comments without claiming independent reviewer sign-off.
- Cover all three complete datasets (8160 hourly, 2040 four-hour, 340 daily), full canonical bytes/OHLCV rows and existing immutable dataset fingerprints; all twelve fixed calculation and visible windows, full canonical bytes and frozen window/visible fingerprints; exact UTC boundaries, aggregation, 250 hidden warmup and selection identity.
- Compare every aligned nullable output of SMA20, SMA-seeded EMA20, Wilder RSI14, MACD12/26/9 and population Bollinger20×2, including first-valid/null alignment, scale `10^12`, symmetric ties-to-even, integer square-root midpoint and fixed parameters. Calculate over full calculation windows; project only visible rows. Never change frozen constants or expectations to make a mismatch pass.
- Implement finite, explicit arithmetic edge and negative/provenance/grid/cancellation/transport cases from the revised C2B-13 worksheet, insofar as accessible through the approved test seams. Assert actual public validation behavior rather than treating arbitrary helper exceptions as successful rejection. Where a case cannot be implemented honestly within these two files, mark it as an explicit unimplemented coverage gap in the PR report; do not silently skip or add production seams.
- Prefer a single bounded JSON stdout oracle invocation per pass with exact selectors only as input, independently produced results, strict UTF-8/JSON parsing, explicit nulls, exact scaled-integer strings, no raw market-data files and no persistent vectors. If a full result cannot fit finite capture, STOP and request revised transport scope instead of truncating results.
- Design ordinary local safeguards within the tests: exact interpreter resolution (no PATH fallback), finite child timeout/cancellation, concurrent stdout/stderr byte-bounded drains, finite final wait, best-effort `Kill(entireProcessTree: true)` where available, and explicit failure on uncertain termination. No Job Object, native containment, custom framing, independent watchdog or hard CPU/RSS guarantee. Do not claim these safeguards were executed or verified.
- Keep test code deterministic, straightforward, reviewable and focused on showcase correctness. Avoid gratuitous abstraction or extensive framework-building. Preserve full comparisons without sample-only shortcuts.

## 5. Explicit non-execution and review boundaries

- **Do not run** `dotnet build`, `dotnet test`, Python, generators, calculators, fixtures, restores, package installation, smoke tests, native probes, snapshot export or Azure/browser requests. Static source inspection and ordinary Git/document checks only.
- Candidate local-run safeguards from revised C2B-13 (120-second build, 120-second test batch, 30-second oracle invocation, 300-second supervised total, 16 MiB stdout/invocation, bounded stderr/logs, best-effort cleanup) remain **proposals**, not tested facts or implicit execution permission.
- Do not require Job Objects, a native helper, exhaustive process accounting or a named reviewer merely to create the test files. Independent numerical/spec review is still required **before accepting parity evidence**, not as a precondition to writing the oracle.
- No claim of passing tests, parity, qualified snapshots, production isolation, durable state or Azure F1 feasibility. The eventual local run needs a separate explicit owner authorization specifying exact executable paths/versions, test command/filter, offline/no-restore behavior, safe output locations and supervised finite limits.

## 6. Evidence and PR procedure

1. Static-review the new source for path allowlist, syntax by inspection, intended method signatures, source independence, bounded subprocess logic, frozen identifiers, full coverage and no imports of private/provider data. **Do not misrepresent static inspection as compilation or execution.**
2. Validate `git diff --check`, no unintended modifications or generated files, and preserved unrelated/untracked content. If the new files contain unresolved TODOs or unimplemented test coverage, identify each gap and its impact in the PR description; no fabricated pass results.
3. Commit only the two permitted test-source files and open **one C2B-15 PR**, **open and unmerged**, against verified `main`. Do not add an evidence Markdown file or modify prior authority documents without further owner permission; place evidence in the PR description.
4. Report PR URL, base/head, exact file inventory, #343 merge SHA, independent-oracle design, coverage achieved versus blocked, all static checks, absence of execution and next narrow local-run prerequisites.
5. End with `PUBLIC_RUNTIME_VERIFIED=False`, activation/deployment **NO-GO**, advanced WP07 **ON HOLD**.

**Exit condition:** Approved PR #343 merged with exact-head guard and a reviewable two-file E4 test-only implementation PR open/unmerged. No executable proof, snapshot generation, public runtime change or activation authority is granted.
