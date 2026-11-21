# Codex Prompt — Release 1.15 WP50: Twelve Data Volume Diagnostics Implementation

## Authority and baseline

Work in `samuel-santos-engineer/AIQuantTradingResearch`. Confirm `origin/main` includes squash merge of PR #436, commit `52398b2ad31f75a4a098ca1c86bf414740e6140c`. Read applicable `AGENTS.md`, governance, Release 1.15 execution plan and WP39–WP49 runbooks before changes. Use a clean isolated worktree/branch; preserve the owner's working tree, untracked files, private preparations, credentials and Docker resources. Create a focused WP50 pull request and leave it **OPEN/UNMERGED** for owner review.

**Binding product decision:** Twelve Data is the sole Release 1.15 market-data provider. Instruments remain BTC/USD and ETH/USD; fixed one-minute UTC/ASC `time_series` acquisition, OHLCV integrity, BTC/ETH base-asset volume requirements, provenance, atomic two-symbol publication and the Docker-first/local research chart architecture remain intact. No Kraken/Coinbase adapters, source switching, migration architecture, licensing gates or Azure work.

## Objective

The one approved WP47 live attempt returned BTC/USD HTTP 200 then `observation_validation / INVALID_VOLUME`, requestsStarted/responsesReceived 1/1, no ETH request and no bundle. The exact subtype and consumed credits are unknown; approval was consumed. Implement **bounded, non-sensitive, subtype-specific diagnostics** so a *future separately authorized* attempt can distinguish volume representation failures without exposing values or changing the set of accepted provider observations.

## Required implementation

1. Trace the actual Twelve Data adapter's volume extraction and validation, the Worker diagnostic mapping, the Python child-event allowlist/strict parser, and existing C#/Python tests. Confirm the real code before selecting exact paths and diagnostic identifiers.
2. Introduce a **small, finite, static** diagnostic taxonomy, covering where distinguishable: `VOLUME_MISSING`, `VOLUME_NULL`, `VOLUME_WRONG_TYPE`, `VOLUME_INVALID_FORMAT`, `VOLUME_PRECISION_EXCEEDED`, and `VOLUME_OUT_OF_RANGE`. These are suggested names, not a mandate to duplicate categories: use repo-consistent `failureCode` values and preserve compatibility with the existing version-1 ten-field envelope. Preserve existing stage/instrument/HTTP/send/response/outcome/bundle semantics. Where an existing failure category must remain for compatibility, document exact mapping. Do not emit values, string lengths, JSON fragments, provider messages, raw bodies, headers, exception text, arbitrary paths or secrets. In particular, do not disclose a volume value even if invalid.
3. Preserve **exact acceptance behavior**: JSON numeric volume, missing/null volume, invalid lexical/precision/range cases still fail; valid decimal strings, including explicit `"0"`, retain exact values. No zero filling, numeric JSON relaxation, rounding, quote/base conversion, OHLC-only downgrade or inferred volume units. Distinguish constructor-level volume out-of-range from unrelated OHLC/observation errors only when the code can do so safely and deterministically.
4. Update every producer/consumer static allowlist in lockstep, including the Worker and Python workflow. Reject unknown, malformed, extra-field or oversized diagnostic envelopes. Keep request count, sequential BTC-then-ETH behavior, no retry, failure-first stop, and atomic publication unchanged.
5. Add offline synthetic tests for each new subtype in BTC-first and ETH-second positions, including valid-prefix-then-bad-row, malformed OHLC/timestamp precedence, forming rows, transport failures, no partial publication, exact counters, sanitized logs, and strict Python parser compatibility. Preserve authentic-only rejection of synthetic fixtures. Do not use real/captured provider responses.
6. Run the applicable existing regression suites, starting from the reported **237-test** baseline (153 focused C#, 23 workflow, 61 chart/client), and report actual new totals, command lines, exits, failures/skips and relevant warnings. Verify UTF-8/LF/no BOM and `git diff --check`.
7. Update a new `docs/roadmap/release-1.15/prompters/release-1.15-wp50-twelvedata-volume-diagnostics.md` runbook and append a concise WP50 entry to `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`. Document the exact taxonomy, precedence, compatibility, coverage and what remains unknown. Avoid rewriting historical WP47 evidence or implying that new diagnostics retroactively identify its defect.

## Hard operational boundaries

**Offline-only WP50.** Do not issue Twelve Data or other market-data requests, including GET/HEAD/DNS probes, account/credit/capability checks or support messages. Do not inspect credential values or presence, old private provider responses, private bundles or account data. Do not run Acquire/Import/Start/Verify/browser against real provider data, deploy, publish images, operate Azure, or modify unrelated infrastructure. Do not merge the PR. Use cached dependencies only when available; if an offline prerequisite is unavailable, report it rather than silently downloading.

No live request is approved by this prompt, by PR merge, or by successful offline tests. Any later attempt needs a **new, separate, explicit, single-attempt owner authorization** and fresh preparation/identity/TLS/ACL/destination checks. No retries.

## Deliverable and stop condition

Produce one narrowly scoped implementation PR with source, coordinated allowlists, tests, runbook and execution-plan update. In the final Codex report include PR URL/head, changed paths, precise test totals, diagnostic code table, unchanged acceptance guarantees, and operational non-actions. Leave the PR **OPEN/UNMERGED**. If a safe subtype distinction would require leaking provider data or changing acceptance, do not implement it; document the limitation and stop for review.

Keep these flags truthful until authentic import and public runtime verification actually occur:

`AUTHENTIC_DATA_NOT_IMPORTED=True`
`PUBLIC_RUNTIME_VERIFIED=False`
`liveAttemptReady=false`
