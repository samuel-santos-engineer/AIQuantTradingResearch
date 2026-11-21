# Codex Prompt — Release 1.15 WP45: Twelve Data Metadata Contract Compatibility (Offline)

## Mission
Continue `samuel-santos-engineer/AIQuantTradingResearch` Release 1.15 from the verified squash merge of PR #431, commit `37aeb51a181fe28fa08d293acbe274c38e357f66`. The last approved Acquire failed for BTC/USD with HTTP 200, `response_parse`, `INVALID_PROVIDER_METADATA`, one request/one response, no bundle. The exact missing or wrong-typed metadata field and provider credit consumption are unknown. The approval is consumed.

**Goal:** establish a defensible, documented Twelve Data `/time_series` metadata contract and correct any overly strict assumptions in the adapter *without weakening candle correctness, provenance, confidentiality, or publication guarantees*. Do this **entirely offline**. Favor an implementation change that advances toward authentic BTC/USD and ETH/USD local charts over another preparation-only milestone.

## Hard boundaries
- No live provider request, DNS/HEAD/capability/account probe, credential read or presence inspection, provider response/body inspection, retries, or Azure operations. No import/start/browser claim using authentic data. Do not use old private acquisition artifacts.
- Do not silently substitute synthetic fixtures for authentic observations or mark the local/public chart verified.
- Preserve the existing maximum two sequential provider requests, `1min`, `outputsize=1000`, UTC/ASC, bounded latest-24-hour completed-candle window, 15-second grace, no retries, no pagination, no fallback, no partial publication, strict private input, bounded diagnostics and version-1 envelope.
- Respect repository `AGENTS.md`, governance, existing WP39–WP44 workflows, local working-tree changes and private resources. No licensing or architecture gate; no Azure action.
- Never expose API keys, private paths, provider response bodies/headers or arbitrary provider messages in logs, tests, PRs, or diagnostic envelopes.

## Phase 1 — Ground the provider contract
1. Verify PR #431 is merged and `origin/main` includes exact commit `37aeb51a181fe28fa08d293acbe274c38e357f66`; start a clean isolated branch/worktree without altering owner dirty/untracked files.
2. Inspect `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs`, Worker mapping, Python diagnostic validator, fake tests, and WP42/WP44 runbooks.
3. Review publicly documented Twelve Data `/time_series` success/error response examples and authoritative API documentation, **without calling the API endpoint or using credentials**. Cite exact documentation URLs and access dates in the WP45 runbook; if internet/documentation access is unavailable, state that clearly and do not invent a contract.
4. Produce a field-by-field table: required for security/data correctness, documented but optional, undocumented assumptions, and safe handling. Pay particular attention to whether `status` is always present on success, whether `meta.exchange_timezone` is guaranteed, and whether `meta.symbol` and `meta.interval` have stable types. **Do not presume any of these are optional until evidence supports it.**

## Phase 2 — Implement the smallest safe correction
1. Make only contract-supported changes to success response parsing. Where documented optional metadata is absent, accept it only if independent request context and parsed candle validation retain all essential guarantees. Do not invent missing fields or claim provider-supplied facts that were not present.
2. Reject explicit contradictory metadata, malformed/wrong-typed present fields, structural provider error envelopes, malformed/oversized JSON, non-UTC/invalid timestamps, bad OHLCV/volume, duplicates/order/window violations, and any ambiguous case that cannot be validated safely.
3. Keep `ObservedProvider` provenance conditional on authentic successful acquisition and existing atomic bundle hash/receipt checks; never relabel synthetic data.
4. If an absent field is now permitted, add precise comments explaining the documentation evidence and how equivalent validation is guaranteed. If the documented schema does not justify relaxation, **do not change parsing behavior**; instead improve fixed, allowlisted metadata diagnostics (e.g. a small code per required-field category) only if necessary, preserving version-1 envelope and updating C# producer/Python validator atomically.
5. Avoid a broad refactor, new dependencies, new endpoints, or unapproved provider calls.

## Phase 3 — Offline qualification
- Add synthetic JSON success and rejection fixtures reflecting documented response variations for BTC/USD and ETH/USD, with metadata missing/present/contradictory/wrong-typed and provider `status=error` scenarios.
- Test strict timestamps, UTC semantics, OHLCV/base-asset volume, bounded JSON/body, error-code mapping, no secret/body leakage, second-symbol failure/no partial bundle, and C# Worker diagnostic compatibility through actual Python parser.
- Run existing focused C#, Python workflow, and chart/client suites; record actual passed/failed/skipped counts, commands, warning baselines, and any unavailable tests. Run `git diff --check` and UTF-8/LF/no BOM checks.
- **Do not** run a new live Acquire, read credentials, reuse old private responses, or claim authentic-data verification.

## Phase 4 — Documentation and PR
- Add `docs/roadmap/release-1.15/prompters/release-1.15-wp45-metadata-contract-compatibility.md` documenting source URLs, field contract, changes, safety rationale, test evidence, unresolved uncertainties, and exact changed-path inventory.
- Append concise WP45 status to `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` without rewriting historical WP41/WP44 evidence.
- Create **one new WP45 PR** against `main`; leave **OPEN/UNMERGED** for owner review. Do not merge automatically. Provide PR URL, full head SHA, changed paths, test results, and whether code changes were justified by documented provider behavior.
- If no safe contract change is supportable, still open a documentation/test-only PR with explicit unresolved questions and a concrete next engineering option, rather than weakening validation.

## Final report and authority checkpoint
The historical WP44 observation remains BTC/USD HTTP 200 / `INVALID_PROVIDER_METADATA` with 1/1 sends/responses and no bundle; no retroactive root-cause claim. Any later real provider invocation requires a **new, separate, explicit owner authorization** and fresh build/manifest/TLS/ACL/destination checks. Never infer approval from this prompt or from a PR merge. Keep flags:

`AUTHENTIC_DATA_NOT_IMPORTED=True`
`PUBLIC_RUNTIME_VERIFIED=False`
`liveAttemptReady=false`
