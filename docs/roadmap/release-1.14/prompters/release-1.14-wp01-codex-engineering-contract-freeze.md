# Release 1.14 WP01 — Codex Engineering Contract Freeze Authority

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`

**Authority class:** Read-only engineering analysis and owner-review proposal.
**Status:** Proposed technical freeze; **not** permission to implement or commit.

## 1. Mission

Inspect the actual repository and the three existing, uncommitted Release 1.14 WP01 planning artifacts. Produce a precise, reproducible engineering contract proposal that resolves or explicitly escalates the remaining technical choices before any WP03 implementation authority. Do not treat proposed technical choices as approved merely because they appear in this authority.

The approved public architecture is **independently generated synthetic OHLCV for anonymous preview; Twelve Data as primary private research provider; Vike retained as inactive future qualified private backup**. Preserve provider-independent canonical data/analytics and fail-closed publication boundaries.

## 2. Required preflight

1. Fetch `origin/main`; record branch, HEAD, worktree status and divergence. Do not rely on the previously reported SHA `893e54c058136a027bd9f8d2ad69019a2901aafc` without verification.
2. Read the controlling `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`, especially Section 10 (approved by merged PR #316), and the existing Release 1.14 WP01 prompters.
3. Read, without overwriting, these **untracked/uncommitted** files:
   - `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
   - `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
   - `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`
4. Inspect relevant existing .NET candle contracts, historical provider/read service, cache/locking, query/stdio, Python bridge, Streamlit UI/navigation, Twelve Data observation source, Vike adapter, container entrypoint, System Health, package pins, tests and F1 evidence. Verify paths from the worktree; do not invent files.
5. Record pre-existing changes and hashes for protected/unrelated files. Never stage, clean, overwrite or delete them. In particular preserve root `README.md` and any existing untracked architecture image.

## 3. Accepted baseline — do not reopen without a demonstrated contradiction

- Dataset UTC start: **2025-10-26T00:00:00Z**; exclusive end: **2026-10-01T00:00:00Z**; **340 days**.
- Complete base grid: **8,160 1h candles**, aggregated **2,040 4h** and **340 1d** candles.
- Twelve supported selections: `1h`, `4h`, `1d` × `1D`, `7D`, `30D`, `90D`; default synthetic instrument, `1h`, `30D`.
- Exactly **250 preceding candles at the selected interval** as indicator warmup; do not expose them as visible-range observations.
- Proposed public seed `114` and generator version `aiq-synthetic-ohlcv-v1` should be evaluated and frozen only through owner acceptance.
- All public values are synthetic and independent of licensed real data; no public Twelve Data/Vike calls, caches, credentials, artifacts or automatic fallback.
- Preserve AC107–AC122 and gates G-1–G-4; zero-recurring-cost Azure F1 objective; no trading functionality.

## 4. Engineering decisions to freeze

### A. Synthetic generation and golden vectors

Specify an **implementable, cross-runtime deterministic** SHA-256 counter-based generation algorithm, including exact domain separation, seed representation, counter byte order, hashing input, uniform-to-decimal conversion, price/volume initialization, drift/volatility bounds, integer/decimal scaling, rounding, and overflow rules. Ensure OHLC invariants, finite positive prices, nonnegative arbitrary-unit volume, complete aligned hourly UTC grid and reproducible 4h/1d aggregation. No fitted/sampled parameters or observations from any market-data provider.

Specify exact canonical serialization (UTF-8, key order or length framing, decimal formatting, timestamps, newline rules, metadata, stable dataset fingerprint) and distinguish **dataset identity/fingerprint** from **operational generated-at**. Provide at least three independently checkable golden-vector cases: first hourly candle, an aggregation boundary, and the final visible-range candle; give expected hashes or a deterministic reference computation that can generate expected hashes. If you cannot independently validate expected numbers, mark them **TO BE GENERATED/VERIFIED**, not fabricated. Specify overlap consistency and clock/locale independence tests.

Clarify whether the full 340-day base dataset is generated once or lazily and how a 250-candle warmup at each interval remains available within bounds. Explicitly address the earliest possible visible range and fixed exclusive end.

### B. Numerical analytics contract

Specify decimal precision, intermediate rounding, final display precision, deterministic standard deviation/square root method, null/invalid handling, alignment, reproducibility and versioning. Evaluate and recommend precise conventions for SMA20, EMA20 (SMA seed), Wilder RSI14, MACD12/26/9, and Bollinger20 ±2 population standard deviations. Include first-valid-index semantics, constant/rising/falling sequences, RSI zero-gain/loss cases, warmup effects, calculation window identity, and independent test vectors. Avoid implementation-dependent floating-point assumptions without a justified explicit boundary.

### C. Streamlit navigation and architecture visualization

Inspect the actual Streamlit version, existing radio-based navigation and deployment routing. Recommend the **smallest compatible** way to expose a dedicated `Architecture & Data Governance` page and homepage keynote/link. Distinguish stable query-parameter navigation from a claimed deep URL route; do not promise `/architecture/data-governance` without evidence. Specify browser/back behavior, keyboard/screen-reader accessibility, responsive design and direct-link tests. Require a visible SVG or equivalently accessible visual derived from the normative Mermaid in governance Section 10, plus complete textual equivalent, implemented/planned labels and synthetic disclosure. Do not silently promote an untracked infographic to normative source.

### D. Azure F1 resource and process boundaries

From existing runtime and historical F1 evidence, propose **explicit measurable budgets** for CPU/memory, max generated candles, serialized response bytes, stdout/stderr capture, child-process concurrency, timeouts, retries, cache entries/bytes/TTL, disk use and cancellation. Identify which numbers are evidenced and which require profiling or owner acceptance. Address Python `capture_output` buffering before size checks; specify hard-bounded capture/termination rather than post-hoc checks. Require zero external provider calls from anonymous UI and System Health; ensure child environment allowlisting and separate public/private handoffs, namespaces and startup requirements. Do not assume the current Azure app is healthy or quotas are unchanged.

### E. Private OHLCV scope

Make an explicit release-scope recommendation: **preserve existing Twelve Data daily-close observation integration and Vike adapter, but defer any new private canonical historical OHLCV adapter and automatic fallback to a separately authorized future release**, unless repository evidence demonstrates a hard Release 1.14 dependency. Distinguish provider preservation from implementation of new private historical candles; identify licensing and rights questions as unresolved where appropriate. No credentials or live provider requests.

## 5. Expected output (no repository writes)

Return a self-contained owner-review report with:

1. Repository baseline, inspected file paths and worktree protection evidence.
2. A **decision register**: accepted baseline, exact proposed technical freeze, evidence, alternatives/tradeoffs, risks and owner-approval status.
3. Deterministic generator specification, canonical serialization and golden-vector methodology, with verified values only when actually computed.
4. Indicator numerical contract and independent validation cases.
5. Navigation/page and architecture-diagram contract.
6. F1 resource budget proposal with measured-vs-assumed labels and required profiling gates.
7. Private provider scope and public/private isolation requirements.
8. Exact **suggested** edits to the three WP01 documents (section names and replacement text or concise diffs), without applying them.
9. AC107–AC122/G-1–G-4 traceability and WP03-before-WP02 dependency verification.
10. Explicit unresolved decisions and **owner approval checklist** before any documentation write authority or implementation.

## 6. Strict mutation and stop boundaries

**READ-ONLY.** Do not create, modify, delete, stage, commit, push, open a PR, create issues/Project items, install packages, change secrets, query live providers, run deployment, or alter Azure resources. In particular **do not edit** the three WP01 planning documents under this authority: prepare reviewable exact changes only. Existing untracked files are user work, not disposable artifacts. Read-only local checks may run if bounded and non-mutating; distinguish checks performed from checks proposed.

If a decision needs empirical profiling, a new dependency, private credentials, live service access, or file mutation, mark it **REQUIRES SEPARATE AUTHORITY** and stop that action. Do not infer implementation authorization from the acceptance of this planning report.

**Final status line:** `WP01 ENGINEERING CONTRACT FREEZE PROPOSAL COMPLETE — OWNER ACCEPTANCE PENDING — NO FILE MUTATIONS OR IMPLEMENTATION AUTHORIZED`.
