# Codex execution prompt — Release 1.15 / PR #442 verification-gate closure

## Mission

Continue **the existing OPEN, UNMERGED PR #442** (`feat/release-1.15-crypto-ohlc-mode`, last reviewed head `6a1e5df32db5829a26fb6e4a63d8c51973c39cdd`) in `samuel-santos-engineer/AIQuantTradingResearch`. Resolve the open verification gates of the owner-approved Twelve Data OHLC-only integration so the owner can make a well-evidenced merge decision. **Do not merge PR #442 or create an unrelated feature PR.** Check actual branch/head/base before changes and report divergence; do not overwrite other work. Baseline main was `0fcb75560e3841674316f1c666066e494550857a` when PR #442 was opened; verify current baseline rather than assuming it.

## Non-negotiable product contract

- **Twelve Data is the sole authoritative upstream market-data provider for this milestone.** Do not query, integrate, benchmark, recommend, or fall back to Kraken, Coinbase, Binance, Bitfinex or any other provider. Existing diagnostic venue-filter names in historical evidence do not authorize new calls or providers.
- BTC/USD and ETH/USD are the existing instruments. Authentic provider OHLC prices and price-based C# indicators must be preserved. The public educational chart may display transformed prices according to the existing research contract; never label transformed values as raw quotes.
- **Keep `volume` in all applicable internal types, persistence, bundle and API contracts.** When upstream volume is unavailable in explicit OHLC-only mode, represent it as **JSON null / nullable decimal**, never zero, inferred quantity, quote-to-base conversion, synthetic value or a fabricated unit. Keep the chart's “Volume unavailable” disclosure and omit volume bars where data is unavailable. Preserve real numeric volume (including actual zero) for present/future supported instruments/feeds.
- Keep strict OHLCV as the default; `--ohlc-only` is an explicit operator opt-in. Do not silently downgrade strict mode or loosen its six volume-failure diagnostics. Maintain completed UTC minutes, ordered/finite bounded acquisition, BTC-first then ETH, first-failure/no-retry and atomic two-symbol publication. Preserve fail-closed preparation identity and private-channel handling.
- No new licensing or provider approval gates, architecture rewrites, dependencies, schema migrations or extra providers. Focus on finishing Release 1.15 Twelve Data integration and the currently open PR.

## Authorization boundary

This prompt authorizes **repository inspection, offline code/test work, local nonsecret test evidence and updating PR #442 only**. It does **not** authorize live Twelve Data calls (even HEAD/DNS/account/credit checks), credential presence/value inspection, reading private historical responses, actual Acquire/Import/Start/Verify/browser operations, Docker builds/containers/resource changes, Azure deployment, registry activity, deletion of retained resources, or modifying owner private state. Obtain separate explicit owner approval before any such operation. Use mocked/local synthetic test fixtures, empty-feed/cache-only package restore, and existing offline dependencies where feasible. Never emit credentials, private paths, response bodies or raw prices.

## Step 1 — Establish exact baseline and test inventory

1. Confirm repository status, main SHA, PR #442 branch/head, clean working tree and 17-file change inventory; avoid touching unrelated work. Check GitHub PR comments/checks if available.
2. Read the PR #442 evidence: `docs/roadmap/release-1.15/prompters/release-1.15-twelvedata-crypto-ohlc-only.md`, the engineering-contract amendment and execution-plan entry. Verify the intended null-volume behavior and exact focused test commands.
3. Reproduce **the full Infrastructure suite** on the PR branch using the same .NET SDK, configuration (`Release` and `--no-restore` when possible), working directory, environment and test selection as the baseline comparison. Capture each failure's stable test identifier, failure category and a sanitized minimal diagnostic. Distinguish test failures from setup failures. Do not treat a broad namespace filter as a representative full-suite run.
4. Run the **identical** full Infrastructure command on a disposable, isolated checkout/worktree of the verified main/base commit, with the same owner execution context and dependency cache. Do not alter or delete the owner's checkout, preparation or resources. If an isolated baseline cannot be executed without prohibited operations, stop and document the limitation, rather than guessing.
5. Build a **test-by-test comparison matrix**: failing on PR only (candidate regression); failing on baseline and PR (pre-existing); failing on baseline only; environmental/intermittent/unknown. Include totals, exit codes and exact invocation details. The previous record was 656 tests / 625 pass / 31 fail on PR; this is a historical observation, not a target to force.

## Step 2 — Close actual regressions and avoid unrelated repair

- Investigate and fix any **PR-only** failure attributable to OHLC-only nullable-volume propagation, keeping changes minimal and within the existing PR. Add focused regression tests where necessary.
- For pre-existing failures, determine whether they arise from missing Debug Worker artifacts, lifecycle/interop/E4 harness setup, host ACL/certificate context or other reproducible causes. Prefer a **bounded test-harness or documented execution-environment correction** if clearly necessary to obtain an honest full-suite result; do not rewrite unrelated production components merely to turn the suite green.
- If full-suite failures cannot be fixed safely within scope, leave them **explicitly open** with precise baseline comparison evidence and a minimal follow-up proposal. Never assert a clean full suite without an actual clean run. Do not waive or hide a new regression.
- Verify nullable volume end-to-end with offline fixtures: `CompletedResearchCandle` nullable field/unit; adapter strict vs explicit OHLC-only; two-symbol bundle; importer and reopened SQLite; transformed snapshot; HTTPS test endpoint; strict Python consumer; Plotly chart and price indicators. Include both missing/null volume and valid numeric volume (including zero), proving backward compatibility and no fabricated volume.
- Confirm malformed price, missing time, symbol mismatch and stale preparation still fail closed. Keep exact existing identity checks; no manifest edits/guard bypass.

## Step 3 — Verification and evidence

Run, as supported offline, the full Application suite, the full Infrastructure suite (not just focused filter), the Release 1.15 Python workflow tests and the chart/client tests. Run relevant formatting and `git diff --check`; verify UTF-8/LF/no BOM on changed files. Record exact commands, environment assumptions, test counts, skips, exits and reproducible failures. If Linux symlink test cannot run on Windows, retain the honest skip and cite the previously qualified Linux result without claiming a new execution.

Update the existing PR #442 evidence/runbook with a compact baseline-vs-PR failure matrix, root-cause analysis, fixes, residual risks and precise merge-readiness conclusion. Amend execution plan only if needed, without duplicating a new milestone or changing upstream-provider policy. Keep source changes as small as possible; documentation-only outcome is acceptable if all 31 are proven baseline/environmental and the owner is given a clear decision.

## Deliverable and stop conditions

- **Update the existing PR #442 branch** (only if safe and under your permitted GitHub workflow), preserving its OPEN/UNMERGED status. If branch updates are blocked or require authority, provide an exact proposed patch/evidence and stop rather than creating another PR by default.
- Summarize: branch/head, changed paths, all suite counts, baseline-vs-PR failures, any remaining PR-only failures, null/numeric volume compatibility, absence of live/provider/resource operations, and whether PR #442 is actually merge-ready.
- **Do not merge, deploy, prepare Docker, or perform live acquisition.** A later fresh matching preparation and controlled Twelve Data acquisition remain separately authorized future actions, not part of this prompt.
- Preserve truthful flags until independently demonstrated otherwise: `liveAttemptReady=false`; `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.

## Definition of done

The owner can review a reproducible full-suite baseline comparison, no unaddressed OHLC-only regressions, explicit nullable-volume and numeric-volume preservation, and a candid merge recommendation for **the same PR #442**. Prioritize executable evidence over additional governance paperwork.
