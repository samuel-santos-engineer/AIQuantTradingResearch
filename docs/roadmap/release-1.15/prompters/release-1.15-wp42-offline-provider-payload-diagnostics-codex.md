# Codex Prompt — Release 1.15 WP42: Offline Twelve Data Payload-Parse Diagnosis

## Objective and authority

Continue `samuel-santos-engineer/AIQuantTradingResearch` from the current **OPEN/UNMERGED PR #429**, head `75055bb73710f34a205bda6849963269d6d10674`, without merging it. Improve the precision of **sanitized, offline-only** failure diagnostics for the existing Twelve Data BTC/USD one-minute candle parser. The immediate evidence is one separately approved, now-consumed acquisition invocation: BTC/USD HTTP 200, `failedStage=response_parse`, `failureCode=INVALID_PROVIDER_PAYLOAD`, `requestsStarted=1`, `responsesReceived=1`, `outcomeUnknown=false`, `bundlePublished=false`; ETH/USD was not requested; exit 1; no bundle, import, database, or chart startup. The exact payload defect and charged credits are unknown. **Do not assert a root cause without evidence.**

**This prompt authorizes code inspection, local changes, synthetic fixtures, and offline tests only. It does NOT authorize another Twelve Data request, DNS/HEAD/capability probe, reading a real credential, historical private evidence inspection, Azure operation, registry publication, Docker cloud pull, or PR merge.** Prior provider approval was consumed. Do not create a new licensing/approval gate; preserve the existing separate approval boundary for any future real acquisition.

## First steps

1. Verify PR #429 is still OPEN, unmerged, and its head/base match the expected state. Read repository `AGENTS.md` and effective local instructions. If the head has changed, inspect the changes and rebase the task on the actual current head without overwriting anyone's work; report the divergence.
2. Use an isolated clean worktree or existing PR branch as appropriate, protecting the owner's dirty checkout. **Update PR #429 itself** rather than opening a competing PR, unless repository permissions make this impossible. Never force-push or rewrite reviewed commits.
3. Inspect the actual implementation and tests before editing, especially `TwelveDataMinuteCandleSource`, its JSON/metadata/UTC/OHLCV/volume validators, `ResearchLocalHistory.AcquisitionTrace`, the Worker `RunAsync` diagnostic envelope, Python `wp39_local_history.py` strict child parser, and relevant tests/runbooks.
4. Write down the exact current exception/diagnostic paths: which parse or validation conditions collapse to `INVALID_PROVIDER_PAYLOAD`, and which failures are classified earlier/later. Separate demonstrated behavior from hypotheses about the historical HTTP 200 body.

## Implementation requirements

- Introduce a **small, fixed, allowlisted taxonomy** of precise failure codes/stages for meaningful parser and validation boundaries. Candidate categories (adapt to actual code): invalid JSON/encoding, unexpected top-level shape, provider error envelope under HTTP 200, missing/invalid metadata, symbol or interval mismatch, non-UTC/timezone mismatch, missing/invalid candle collection, invalid candle timestamp/order/duplicate/alignment, invalid OHLC, invalid base-asset volume, payload size limit, and empty/no eligible completed-candle window. Only report categories that the implementation can reliably distinguish. Avoid misleading classification of provider account/key state or credit consumption.
- Decide whether an HTTP-200 provider error envelope can be recognized **structurally** without logging or reproducing its contents. Preserve fail-closed behavior; never accept it as candle data. Do not add permissive fallback mappings merely to make a payload pass.
- Keep diagnostic records bounded, versioned, single-event, machine-parseable, and compatible with the existing Python strict allowlist. If extending failure-code or stage enums, update both C# producer and Python validator atomically. Prefer the smallest backward-compatible change; if schema evolution is unavoidable, document the compatibility strategy and test it.
- Never include raw response body, JSON fragments, provider message strings, secrets, authorization headers, URL query with secrets, exception messages, private paths, candle values, or arbitrary external text in logs, diagnostics, PR comments, or fixtures. Fake fixtures must be clearly synthetic, with canary strings to test leakage. Never capture, reconstruct, or read the actual historical response.
- Preserve existing source endpoint/query shape, sequential BTC/USD then ETH/USD, max two requests per approved invocation, timeouts, no retries/pagination/probes, UTC/ASC 1-minute and 1,000-row bounds, 15-second completed-candle grace, base-asset volume requirement, atomic bundle publication, import/provenance contracts, and fixture-versus-observed safeguards. Do not change UI, public runtime, schema, licensing policy, or Azure architecture.
- Preserve diagnostic semantics: request-start counts mean entering send boundary, response-received counts mean HTTP response observed, HTTP status is only observed status, and unknown outcomes remain explicitly unknown. No inferred provider receipt or credits.
- For unexpected errors, fail closed with a generic fixed category, no raw exception propagation to the CLI. Maintain one sanitized diagnostic per failure.

## Required offline tests

Use fake `HttpMessageHandler` and synthetic JSON only, with no live network. Cover at minimum:

- Valid synthetic BTC/USD and ETH/USD parsing with existing strict constraints (fake data must never be misrepresented as authentic imported observations).
- HTTP 200 + malformed JSON; valid JSON but wrong top-level shape; recognizable provider error envelope; absent/wrong metadata; symbol/interval/timezone mismatch; malformed/missing candles; invalid timestamps/order/duplicates; invalid OHLC; missing/invalid volume; no eligible completed candles; oversized body.
- HTTP 401/403/429/500 remain classified as observed HTTP facts, not parser failures. Transport failure, timeout, cancellation, response-body read failure, and second-symbol failure retain accurate status/counters and no partial bundle.
- End-to-end C# Worker sanitized envelope -> Python strict parser for each newly added code; malformed/oversized/forged child diagnostic rejection; secret-canary non-disclosure in stdout/stderr/exception representations; no automatic retry.
- Existing WP39/WP40/WP41 tests and chart/client regressions. If local tools or caches are unavailable, report exactly what did not run rather than silently substituting a narrower test set.

Run offline build/test commands in the repo's documented environment (cached dependencies only); record exact commands, pass/fail/skip counts, and limitations. Do not use real provider credentials, real provider responses, live network, Azure, or external data fetching.

## Documentation and PR deliverables

Update the **existing PR #429** with implementation, tests, and concise documentation in `docs/roadmap/release-1.15/prompters/` (WP42 offline parser diagnosis) plus an execution-plan amendment. Clearly distinguish:

1. **Historical observed facts:** BTC/USD HTTP 200, one send/response, generic parser failure, no ETH request, no bundle/import; exact historical defect and credits unknown.
2. **New offline capability:** what the synthetic tests demonstrate about future classification; do **not** retroactively assign a specific failure code to the earlier response.
3. **Future operational checkpoint:** a new real attempt would require separate explicit owner approval; do not request it as part of this execution or perform it automatically.

Keep PR #429 OPEN/UNMERGED for owner review. Report changed paths, commit/head, test evidence, new diagnostic taxonomy, any unresolved risks, and next recommended decision. Enforce UTF-8/LF/no BOM, `git diff --check`, no secrets or private artifacts staged, and no unrelated changes. Avoid changing historical runbook text in ways that imply the previous attempt did not happen.

## Mandatory stop conditions

Stop and report if the current PR head/authority is ambiguous, the owner branch cannot be safely updated, the change would require live provider data to verify, or a requested step would expose a credential/private response. Never work around these limits by issuing a probe. **No live acquisition, no retries, no Azure, no merge.**

Final status unless a later separately authorized and successful workflow actually changes it:

`AUTHENTIC_DATA_NOT_IMPORTED=True`
`PUBLIC_RUNTIME_VERIFIED=False`
`liveAttemptReady=false`
