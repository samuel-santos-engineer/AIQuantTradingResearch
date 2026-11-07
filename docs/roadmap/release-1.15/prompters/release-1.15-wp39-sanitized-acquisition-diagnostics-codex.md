# Codex task — Release 1.15 WP39: Offline-only sanitized acquisition diagnostics

## Mission

Continue the existing **OPEN / UNMERGED PR #427** in `samuel-santos-engineer/AIQuantTradingResearch`. Fix the diagnostic blind spot following the **single owner-approved Twelve Data acquisition attempt**, which exited 1 without producing an authentic bundle. Do not infer the cause. **Do not retry the provider.** Make the smallest well-tested change that distinguishes preflight, subprocess, transport, HTTP, validation and publication failures without disclosing credentials or provider response content.

Baseline PR #427 head observed at prompt preparation: `248704a408443237893a6d973b193f0445a6632c`. First inspect live GitHub/working-tree state; use the current PR #427 branch, do not reset others' work, do not create a competing PR. Leave #427 **open and unmerged** for owner review.

## Non-negotiable execution boundary

- **ZERO live provider requests**, including capability checks, quota checks, DNS/HTTP probes to Twelve Data, or acquisition retries. The prior single-attempt authority is **consumed**, not reusable. No `Acquire` with a real key.
- **ZERO Azure operations**, registry publication, image promotion, cloud deployment, or production modifications.
- Do not access, read, display, export, copy, rotate or persist the owner's real Twelve Data key or Azure API key. Do not inspect secret settings. All tests use clearly fake credentials and fake HTTP handlers. Do not print child environment, stdin, headers, raw response bodies, exception text, private directory paths or command arguments containing secrets.
- Preserve the existing two-container architecture, authenticated snapshot contract, SQLite state/import boundaries, fixture/observed distinction, bounded acquisition of at most one request per symbol, no retries/pagination, no destructive recovery, and existing UX. No licensing approval gate or architecture redesign.
- Do not claim an authentic import or local authentic/browser qualification. Keep `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.

## Inspect before editing

Review the current PR diff, tests and these paths (actual names may differ; verify):

- `eng/docker/release-1.15/wp39_local_history.py` — `run`, `worker`, `acquire`, and CLI catch-all `WP39_BLOCKED`.
- `src/AIQuantTradingResearch.Worker/ResearchLocalHistory.cs` — `RunAsync`, `AcquireAsync` and generic catch-all.
- `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/*` or the actual `TwelveDataMinuteCandleSource` implementation and its tests.
- `tests/runtime/release_1_15/test_wp39_local_history.py` and `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchLocalHistoryTests.cs`, `TwelveDataMinuteCandleSourceTests.cs`.
- `docs/roadmap/release-1.15/prompters/release-1.15-wp39-local-authentic-history.md` and `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`.

The currently observed Python `run()` discards child stderr on nonzero exit and raises a generic `RuntimeError`. The C# `RunAsync()` catches all exceptions and prints a generic acquisition failure. The adapter may also map HTTP/validation errors to a generic exception. Instrument these boundaries carefully instead of making a speculative provider fix.

## Implement a bounded, secret-safe diagnostic contract

Provide a **single structured, allowlisted diagnostic** for a failed acquisition, printed to stderr or another existing operator-only output (not to the browser), with no free-form exception details. Suggested schema (adapt only for stronger correctness):

```json
{
  "event": "WP39_ACQUIRE_FAILED",
  "failedStage": "http_response",
  "instrument": "BTC/USD",
  "httpStatus": 429,
  "requestsStarted": 1,
  "responsesReceived": 1,
  "outcomeUnknown": false,
  "failureCode": "HTTP_RATE_LIMITED",
  "bundlePublished": false
}
```

Use stable allowlisted values, e.g. `preflight`, `child_launch`, `private_input`, `request_start`, `http_response`, `response_parse`, `observation_validation`, `bundle_publication`, `unknown`; `instrument` must be `BTC/USD`, `ETH/USD`, or `null`; `httpStatus` must be an **actually observed** numeric status or `null`. Codes should distinguish missing approval/channel, invalid private input, child executable failure, timeout, transport error, HTTP 401/403/429/5xx/other, invalid provider payload, invalid UTC/OHLCV/volume, zero accepted candles, bundle filesystem/publication failure, and unknown errors where classification is impossible.

**Accuracy is more important than granularity:**

- Count `requestsStarted` at the actual outbound request boundary, not at symbol-loop entry. Count `responsesReceived` only after a response status is observed. If a send started and no response arrived, mark `outcomeUnknown=true`; never assert the provider received it or that zero credits were consumed. If the process exits before any instrument request, report zero only when the instrumentation proves it.
- If Python cannot receive a trustworthy child diagnostic (e.g. child launch failed, timed out, crashed, emitted malformed diagnostics), report the Python-observed stage and unknown/null counts rather than inventing a status or number of provider calls.
- Do not classify a 401/403 as a definitively invalid key, or a 429 as definitively exhausted account quota. Preserve the observed HTTP fact and generic category.
- Do not forward arbitrary C# stderr to Python stdout/stderr. Parse a strict size-bounded, versioned, fixed-schema machine diagnostic with allowlisted values; reject/replace malformed or oversized child output with a safe generic code.
- If the adapter currently suppresses status/transport details, expose only structured nonsecret classification through narrowly scoped internal types or callbacks. Do not change the public API or leak provider response bodies. Ensure exception handling and cancellation preserve real stage/counters, including failures on the second symbol.
- Do not log any provider key, authorization header, URL query with credentials, request/response body, stack trace, full exception message, stdin, environment dump, private path, or imported observations. Use fake-key canary assertions in tests.
- The diagnostic must not write a bundle, receipt, research.sqlite, or qualification artifact on failure. Existing private state is preserved. Never automatically retry.

## Offline tests (required)

Extend fake-only Python and C# tests for at least:

1. Missing approval, missing private channel, malformed private input: zero outbound requests, no key read when approval is absent.
2. Child executable missing/nonzero, timeout, malformed/oversized child diagnostic: sanitized parent failure, unknown request count when not provable.
3. Fake HTTP 401, 403, 429, 500: exact observed status, correct stage/instrument and counts; no inference of actual account state.
4. Fake transport failure before response and timeout/cancellation: outcome unknown if send began, status null.
5. Invalid JSON/metadata, invalid timezone, duplicate/unaligned UTC, invalid OHLCV, unsupported volume and empty valid rows: precise stage where available; no published bundle.
6. Second-symbol failure after first response: counts reflect both actual requests; no partial bundle.
7. Successful fake acquisition: original success behavior and private provenance, without implying authentic provider data. Retain synthetic fixture marking in tests.
8. Fake secret canary in every relevant boundary: never appears in diagnostic output, command arguments, child environment, traceback, artifacts or operator-facing text.
9. No-retry/no-pagination guarantee, destination-not-overwritten behavior and unchanged WP38 fixture chart tests.

Run relevant offline test suites and report exact commands, pass/fail counts and any environmental blockers. If Docker is already available, optional local fixture-only smoke is allowed; no provider calls. Do not bypass PowerShell execution policy.

## Documentation and PR update

Update the existing WP39 runbook with:

- Historical fact: **one approved live acquisition attempt was consumed; it failed exit 1; no bundle/import; no retry; failure stage/status/request count/credits remain unknown for that historical attempt**. New diagnostics **cannot retroactively reveal** them.
- Sanitized diagnostic schema, code/stage interpretation, safe example outputs using fake values, and exact offline tests/evidence.
- Clear operator instruction: **do not run another live Acquire** until the owner separately authorizes a new bounded attempt; command flags alone are not authorization.
- The execution plan's stale `AWAITING_OWNER_PROVIDER_APPROVAL` text should reflect `ONE_APPROVED_ACQUISITION_ATTEMPT_FAILED_NO_BUNDLE` and the offline diagnostic follow-up. Do not imply another request was approved.
- Maintain explicit distinction among implemented, locally fixture-qualified, authentic-data-imported, locally authentic-qualified, and publicly qualified.

Keep edits tightly scoped to the relevant workflow/adapter/tests/runbook/plan. Preserve UTF-8 LF without BOM; run `git diff --check` and check no secret-bearing files are staged. Commit changes to the existing PR #427 branch, update the PR description with truthful evidence, **leave it OPEN/UNMERGED**, and return the new head SHA and exact test results.

## Stop conditions

Stop and report, without guessing or requesting live data, if instrumentation would require exposing secrets, broadening the provider contract, unapproved network activity, modifying protected runtime contracts, overwriting private state, or if current branch ownership conflicts with PR #427.

## Final status

Unless a separate future owner-authorized live acquisition and complete import/snapshot verification actually succeed, retain:

`AUTHENTIC_DATA_NOT_IMPORTED=True` · `PUBLIC_RUNTIME_VERIFIED=False` · `liveAttemptReady=false`.
