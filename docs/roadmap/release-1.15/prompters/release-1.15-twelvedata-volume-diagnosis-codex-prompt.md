# Codex execution prompt — Release 1.15: Twelve Data volume diagnosis and source-direction correction

## Owner decision — binding scope

The owner explicitly reaffirmed: **Twelve Data remains the sole market-data provider for Release 1.15**, with **BTC/USD and ETH/USD**. Do not migrate to Kraken, Coinbase, exchange archives, or a provider-agnostic architecture. Do not introduce licensing, approval, or governance gates unrelated to the existing one-attempt operational authorization rule. Preserve the existing Docker-first and dual-process design, chart, workflow, and authentic OHLCV integrity requirements.

**PR #436 is OPEN and UNMERGED. Do not merge it.** Its recommendation to move to Kraken conflicts with the owner's decision. First reconcile PR #436 in place if authorized GitHub access and branch ownership permit: revise the WP49 evaluation and appended execution-plan text to make alternatives *not selected/out of scope*, retain the research as historical comparative evidence, and state unequivocally that Twelve Data remains the implementation target. Do not erase accurate historical evidence or imply WP49 evaluated live endpoints. If editing the existing PR is impractical, report the blocker and create a narrowly scoped replacement PR against main, explicitly marking #436 superseded; do not close #436 without owner direction. Leave any PR OPEN/UNMERGED for review.

## Baseline and observed facts

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

- Latest known merged PR #435 squash: `7c8d242105fbc5e3fbed4159b07f94a90a7a271c`.
- PR #436 head at last review: `9d36bbae61e8d4c35eec6e4ba4b7c17f545490f9` (verify current state and SHA; never force-push or overwrite changed owner work).
- WP47 authorized single Acquire: BTC/USD HTTP 200; `failedStage=observation_validation`, `failureCode=INVALID_VOLUME`, `requestsStarted=1`, `responsesReceived=1`, `outcomeUnknown=false`, `bundlePublished=false`. ETH/USD not requested; no bundle, import, database or chart. Approval consumed. Specific volume defect and credits unknown.
- WP48: official Twelve Data Java SDK `TimeSeriesItem.md` marks `volume` optional `String`, but does not prove exact BTC/USD/ETH/USD one-minute volume availability or units. Existing production parser acceptance unchanged; reported offline baseline 234 tests (150 C#, 23 workflow, 61 chart/client).
- `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

Verify current repository state, instructions (`AGENTS.md`), governance, and actual implementation before making changes. Preserve owner checkout, private preparations, credentials and unrelated resources.

## Objective

Produce an **implementation-oriented, Twelve Data-only** resolution path for `INVALID_VOLUME`, not a new source evaluation. Investigate what the existing parser accepts, what the official provider contract establishes, and what information is still missing. Improve offline diagnosability only where it can distinguish failure categories **without disclosing response bodies or private values**. Implement a narrowly justified parser correction only if reliable official evidence supports it and authentic base-asset volume semantics remain demonstrably valid. Otherwise keep acceptance strict and document the precise unresolved question and next safe action.

## Execution steps

1. **Reconcile source direction:** update the WP49 report and execution plan in PR #436 (or propose a replacement as described above). Clearly distinguish historical research from an approved implementation direction. Explicitly prohibit Kraken/Coinbase implementation for Release 1.15. Do not alter the provider selection, request budget, instrument pair, or Twelve Data provenance.
2. **Trace the exact Twelve Data volume path** from HTTP response JSON through `TwelveDataMinuteCandleSource`, completed-candle validation, Worker diagnostic mapping, Python strict diagnostic allowlist, atomic publication, importer and chart. Produce a decision table for absent, null, JSON number, string zero, malformed string, precision overflow, negative, out-of-range, and missing/partial rows; distinguish `INVALID_VOLUME` from `INVALID_OBSERVATION` and earlier parse failures. Do not infer which case WP47 actually encountered.
3. **Research authoritative public provider material only:** official Twelve Data time-series REST documentation, official SDK schema/models and provider support articles. Confirm or explicitly leave unknown: whether BTC/USD and ETH/USD `1min` data include volume, whether field is optional, JSON encoding, base-versus-quote units, venue/aggregation semantics, and subscription/product availability. Document URLs, access dates and exact limits. Do not make live API, DNS/HEAD, account, credit, or capability probes; do not contact support or submit tickets without explicit owner instruction.
4. **Implement offline-safe, bounded diagnostic specificity** only if appropriate: e.g. classify volume presence/type/format/range using finite static codes within the existing version-1 envelope, never raw response text, exception messages, headers, paths, credentials, arbitrary strings or payload excerpts. Preserve counters, unknown-outcome semantics and failure-first stop. Update all strict C#/Worker/Python allowlists and tests together if any code changes. If changing the envelope would create incompatibility, prefer keeping it unchanged and document the diagnostic limitation; do not silently widen its contract.
5. **Parser acceptance remains unchanged by default.** A change to accept JSON numeric volume or another encoding requires official evidence for that exact representation, lossless bounded decimal conversion, and credible base-volume unit semantics. Never synthesize zero, convert quote to base by division, invent trades, mix providers, accept OHLC-only as OHLCV, or relabel synthetic data as authentic. Preserve strict UTC timestamps, interval, symbol identity, 24h window, 15-second grace, 1,000-row upper bound, provenance and atomic two-instrument publication.
6. **Offline tests:** extend synthetic fake-handler coverage for both first-symbol and second-symbol failure, correct bounded code/stage/counters, no partial bundle, no secret or provider payload disclosure, and existing authentic-only verification. Run cached-only C#, Python workflow and chart/client regressions. Report exact counts and commands; 234 is a historical baseline, not a claim about newly run tests. No dependency downloads, Docker operations or Azure actions unless separately requested (none are authorized here).
7. **Evidence and PR:** write a concise Twelve Data-specific runbook under `docs/roadmap/release-1.15/prompters/` and append an execution-plan entry. Include what is known, unknown, any actual code changes, test results, and a concrete next decision. Prefer updating PR #436 to correct the direction, with a clearly separated follow-up implementation PR if code/test changes warrant it. Keep PR(s) OPEN/UNMERGED for owner review. Do not merge or close PRs autonomously.

## Operational safety — non-negotiable

- **Zero live Twelve Data requests** in this task, including test requests and discovery probes. No retries or fallback. Existing WP47 approval was consumed; no new request is authorized by this prompt, PR update, or owner agreement to stay with Twelve Data.
- No credential value/presence inspection; no private historical response/bundle inspection; no secret logging. Preserve private artifacts and prior evidence.
- No Azure, registry publication, deployment, live Docker workflow, Import, Start, Verify or browser qualification.
- Do not claim successful authentic acquisition, chart verification, volume availability, exact historical defect or credit consumption.
- Do not introduce a new licensing prerequisite or provider-switch approval process.

## Deliverables / acceptance

- PR #436 direction corrected in place if feasible, or a clearly explained replacement PR; no merge.
- Twelve Data-only volume contract matrix, evidence-backed unknowns and explicit owner-aligned next implementation step.
- If justified, minimal offline diagnostic/parser patch plus exhaustive synthetic tests; otherwise documentation and tests only.
- Accurate offline test report, `git diff --check`, UTF-8/LF/no-BOM checks, exact changed paths and open PR link(s).
- Final status: `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false` unless independently verified through a separately authorized future workflow.

**Stop and ask the owner** before any real provider request, provider migration, change to mandatory authentic OHLCV/base-volume requirements, or merge.
