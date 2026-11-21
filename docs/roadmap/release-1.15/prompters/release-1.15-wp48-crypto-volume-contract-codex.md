# Codex execution prompt — Release 1.15 / WP48: Crypto Volume Contract Compatibility

## Mission and authority

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

Start from freshly fetched `origin/main` and verify PR #434 squash merge commit `05dd8ba4abc95c28d8e8abf6df39661e42dc70dc`. Work on a clean isolated WP48 branch, preserving the owner's working tree, untracked files, private preparations, Docker resources and credentials. Read applicable `AGENTS.md`, Release 1.15 governance, execution plan, and WP39–WP47 evidence before changing code.

WP47 made **one** separately owner-approved live Acquire invocation, which failed on BTC/USD HTTP 200 with `observation_validation / INVALID_VOLUME`, requests/responses 1/1, exit 1, no bundle; ETH/USD not requested. The specific defect and credit usage are **unknown**. This approval is **consumed**. Do not infer the historical response content or retroactively assign a cause.

**This WP48 prompt authorizes offline research, implementation, synthetic tests and a review PR only.** No Twelve Data API requests, DNS/HEAD/account/credit/capability probes, Acquire/Import/Start/browser operations, credential value or presence inspection, inspection of historical private provider responses, Azure changes, registry publication or live provider authorization. Do not ask Codex to perform a real request. No licensing/governance approval gates.

## Investigation

1. Review the actual C# volume parsing and observation-validation path, Worker diagnostics, strict Python diagnostic allowlist, importer/provenance checks, and synthetic fixtures. Enumerate exactly which JSON representations lead to `INVALID_VOLUME` versus `INVALID_OBSERVATION`, including missing, null, JSON number, JSON string, empty, malformed, negative, nonfinite, precision/overflow and any row-level cases. Do not claim a historical cause from this enumeration.
2. Consult **public, authoritative Twelve Data documentation and official SDK models/source** for `time_series` OHLCV and cryptocurrency-specific BTC/USD and ETH/USD volume semantics. Establish what is explicitly required, optional, nullable, numeric, string-encoded, and what units are documented. Differentiate generic SDK schema from a guaranteed crypto endpoint payload. Public docs only; never execute API example links. Record exact source URLs, access dates, evidence and limits in a contract matrix.
3. Distinguish the essential product requirement—authentic, correctly attributed BTC/USD and ETH/USD minute OHLCV with defensible base-asset volume units—from incidental provider JSON formatting. Do **not** synthesize volume, replace missing volume with zero, treat quote volume as base volume without evidence, or silently change the product to OHLC-only.

## Implementation decision

- If official evidence supports an alternative **losslessly and safely interpretable** volume representation that preserves nonnegative finite numeric value, precision, units, UTC candle identity and provenance, implement the **smallest** narrowly justified compatibility correction. Keep bounds, decimal rules, atomic two-symbol publication, strict source identity and all other validations. Reject ambiguous/unsupported cases.
- If evidence supports optional or null volume but no legitimate way to obtain the mandatory base-asset volume, **do not relax acceptance**. Document that this data source may not satisfy the OHLCV requirement and identify concrete alternatives for a later owner decision. Do not invent volume or propose provider licensing as a prerequisite.
- If evidence is insufficient, make no speculative acceptance changes. You may improve fixed, bounded diagnostic categorization **only when it materially helps** and C# producer/Python consumer remain compatible; never leak raw provider data, arbitrary messages, keys, values, headers or paths. Preserve version-1 diagnostic semantics or explicitly justify and test any coordinated contract change.
- Consider a provider-independent synthetic test matrix for string-vs-number volume and malformed/absent/null/negative/overflow values, including second-symbol failure and zero partial publication. Verify that fake success remains `SyntheticFixture` and is rejected by authentic-only import.

## Offline validation

Use existing cached dependencies only; no network restores or downloads. Run focused C# adapter/Worker/acquisition tests, Python workflow/strict diagnostic tests, chart/client regressions and any new tests. WP47 reported **210 passing offline tests** (126 C#, 23 workflow, 61 chart/client); report actual WP48 counts and failures rather than asserting an unexecuted result. Validate UTF-8/LF/no BOM, `git diff --check`, no credential/private files staged, and no unauthorized changes to Docker, Azure, UI, schema, provider request shape or retry policy.

## Deliverables and stop condition

Create `docs/roadmap/release-1.15/prompters/release-1.15-wp48-crypto-volume-contract.md` with: source/contract matrix, parser decision tree, precise scope and rationale, synthetic test inventory and actual results, changed-file inventory, remaining uncertainty and next engineering recommendation. Append a concise WP48 entry to `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` without rewriting historical evidence. Create **one GitHub PR**, leave it **OPEN/UNMERGED** for owner review, and report its URL, full head SHA, changed paths and test results.

Explicitly retain: `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`. No further live provider attempt is approved by this prompt or by merging the WP48 PR. If later requested, a single live attempt requires a **new separate explicit owner authorization**, fresh source/build/manifest/TLS/ACL/destination checks, and no retries.
