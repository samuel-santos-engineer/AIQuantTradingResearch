# Codex prompt — Release 1.15 WP51: Twelve Data acquisition readiness and diagnostic rehearsal

You are working in `samuel-santos-engineer/AIQuantTradingResearch`. The owner explicitly chose **Twelve Data as the sole Release 1.15 market-data provider** for BTC/USD and ETH/USD. Do not propose or implement Kraken, Coinbase, alternative providers, or provider-agnostic migration. Focus on the existing local historical-candle research chart.

## Baseline and operating boundaries

- Start from merged PR #437, squash commit `2e6ff99f04ba68b3ade68093dd41931d9194b077`. Verify the repository baseline, branch status and clean/dirty working tree. Work in an isolated branch/worktree and preserve all owner changes, untracked files, credentials and private artifacts. Do not reset or clean the owner's checkout.
- WP50 introduced six finite diagnostic codes: `VOLUME_MISSING`, `VOLUME_NULL`, `VOLUME_WRONG_TYPE`, `VOLUME_INVALID_FORMAT`, `VOLUME_PRECISION_EXCEEDED`, `VOLUME_OUT_OF_RANGE`. Keep the old `INVALID_VOLUME` code recognized. Existing version-1 ten-field envelope, strict allowlists, authentication boundaries, OHLCV acceptance, 1,000-row/24h bounds, 15-second grace, first-failure stop, no retries, BTC-first/ETH-second sequencing, and atomic two-symbol publication must remain intact.
- **No live Twelve Data request, DNS/HEAD probe, account/credit check, support contact, credential value or presence inspection, or private response/bundle reading.** No Docker/Azure/registry/deployment, actual Acquire/Import/Start/Verify/browser operation, or owner messaging. No provider request is authorized by this prompt. WP47's prior single-attempt authorization is consumed.
- Maintain `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false` until real evidence proves otherwise. Do not claim WP47's unknown volume subtype or credit charge is known.

## WP51 objective

Prepare an **offline-only, reproducible end-to-end diagnostic rehearsal** of the existing Twelve Data acquisition workflow so the next separately owner-authorized attempt can provide actionable failure classification without leaking provider data or accidentally publishing partial history. Do not spend another work package repeating general provider research or adding governance/licensing prerequisites. Prefer narrow implementation/test improvements that directly remove demonstrated readiness defects.

## Required investigation and implementation

1. Inspect the actual WP50 code, tests and existing workflow: `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs`, `src/AIQuantTradingResearch.Worker/ResearchLocalHistory.cs`, `eng/docker/release-1.15/wp39_local_history.py`, relevant C# and Python tests, and the WP49/WP50 Twelve Data runbooks. Identify the exact executable command and owned-resource prechecks for the existing local acquisition path **without executing any live operation**.
2. Construct a fake-only rehearsal that exercises the *real* Worker diagnostic producer and the *real* Python strict consumer together, not merely independently mocked envelopes. Include BTC-first and ETH-second failures for all six WP50 volume subtypes; verify exact stage, code, instrument, HTTP status, send/response counts, unknown-outcome semantics, and no raw response/volume/credential leakage. Preserve tests for legacy `INVALID_VOLUME`, malformed/oversized/extra-field envelopes and a fake success that cannot be mislabeled authentic.
3. Exercise transport timeout/cancellation, response-without-valid-data, HTTP failure, and first-failure-stop paths where existing seams allow. Prove no second request after first-symbol failure, no retries, no bundle/receipt publication after either failure, and no mutation of an existing destination. Use entirely synthetic fixtures and fake HTTP/subprocess boundaries; never read a real private bundle or secret.
4. Verify that the strict Python consumer accepts the new codes while still rejecting unknown codes and malformed fields. Review whether any documented production packaging path could combine a WP50 Worker with an older Python consumer; if a genuine gap is demonstrated, make the smallest offline-testable packaging/compatibility correction within existing architecture. Do not change the envelope or broaden accepted observations.
5. Produce a concise, operator-useful **single-attempt readiness checklist** grounded in existing commands and code: offline build/test prechecks, identity and executable matching, approved target BTC/USD then ETH/USD, safe destination, expected diagnostic fields, stop conditions, no retry, unknown-credit reporting, and what to report to the owner. This is documentation, **not** authorization to execute it. Keep any unresolved Twelve Data minute-volume availability/base-unit questions explicitly unresolved; do not infer them from synthetic tests.
6. If the existing tests already establish a requirement, do not duplicate it merely to increase test count. Add only tests or implementation changes that close concrete coverage gaps. Avoid schema, analytics, UI, provider, dependency, infrastructure and unrelated refactors.

## Deliverables

- A focused WP51 runbook at `docs/roadmap/release-1.15/prompters/release-1.15-wp51-twelvedata-acquisition-readiness.md`, including actual file paths, exact fake-only commands, results, outstanding uncertainties, and a clear distinction between **rehearsed** and **not executed** operations.
- Append-only WP51 status in `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`.
- Only narrowly necessary synthetic tests or code changes, with a rationale and exact-path inventory. Keep source-of-truth Twelve Data behavior and diagnostic security intact.
- Run fresh cached/offline C# and Python workflow/chart regressions where possible; report counts, skips, warnings and any environment limitations accurately. Validate UTF-8/LF/no BOM and `git diff --check`.
- Create one focused GitHub PR against `main`, **leave it OPEN/UNMERGED** for owner review. In the PR description give baseline merge SHA, changed paths, tests, exact operational non-actions, and current status flags. Do not merge or perform provider calls.

## Acceptance criteria

The owner can inspect a precise, tested offline rehearsal and a ready-to-follow, bounded Twelve Data-only next-attempt checklist, while all authentic-data and public-runtime status flags remain false. No provider request or operational permission is implied. If a real blocker is discovered, document the exact blocker and minimal proposed remedy rather than fabricating success or switching providers.
