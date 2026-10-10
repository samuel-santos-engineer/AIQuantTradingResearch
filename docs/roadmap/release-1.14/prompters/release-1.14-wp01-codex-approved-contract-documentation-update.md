# Codex Authority — Release 1.14 WP01 Approved Engineering Contract Documentation Update

## Authority and status

The owner has approved the Release 1.14 engineering baseline described in the read-only **WP01 Engineering Contract Freeze Proposal** (482-line Codex report, supplied in the conversation). This authority permits a **documentation-only update** to the three existing, local, untracked WP01 planning documents. It does **not** authorize application implementation, new fixtures, tests, profiling, deployment, commits, branches, PRs, issues, or changes to controlling governance.

## Exact mutation allowlist

Edit **only** these existing files, in place:

1. `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
2. `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`

Do not create any additional files. Preserve existing untracked authority prompters, architecture image, root README, controlling governance, roadmap, dependencies, code, tests, deployment files and all unrelated files. **Do not `git add`, commit, push, open a PR, or create a branch.** Do not overwrite or discard owner edits. Stop and report if the files are absent, materially inconsistent, or no longer match the documented baseline.

## Preflight

- Confirm the current branch, HEAD, `origin/main`, ahead/behind and `git status --short`; record any deviation from the previously reported `893e54c058136a027bd9f8d2ad69019a2901aafc` without resetting anything.
- Read all three target files, the read-only engineering freeze proposal (request it from the owner if not available in Codex context), and `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md` Section 10, accepted via merged PR #316. The planning inspection PR #317 supplies historical context only. Section 10 supersedes conflicting Sections 1–9; do not modify the controlling document or silently repair its status field.
- Capture SHA-256 of protected/unrelated untracked files and root README before edits. Respect the existing local-only/uncommitted state.
- Treat the engineering proposal as **approved selections**, except where explicitly noted as a **pending evidence gate** below. Do not infer approval for future implementation.

## Accepted baseline — preserve exactly

- Independently authored **synthetic-only** anonymous public OHLCV, with visible label **“BTC-like Market Simulation — Synthetic Historical OHLCV”** and illustrative/not-investment-advice disclosure. No public Twelve Data or Vike calls, publication of licensed observations, provider-derived fitting, or real-market equivalence claim.
- Synthetic identity `btc-like-simulation`; generator `aiq-synthetic-ohlcv-v1`, seed `114`.
- Fixed UTC coverage `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)` = **340 days**, **8,160 1h**, **2,040 4h**, **340 1d** candles. Complete UTC-aligned 4h and 1d aggregation from hourly.
- Intervals `1h`, `4h`, `1d`; ranges `1D`, `7D`, `30D`, `90D`; **12 combinations**, default `1h`/`30D`; exactly **250 preceding calculation-only warmup candles** at selected interval, never published as visible candles. For 1d/90D the full 340-day dataset is required. Preserve the 1d/1D one-visible-candle behavior.
- Existing WP01–WP08 IDs, dependency graph, AC107–AC122, G-1–G-4, strict public/private boundaries, F1 zero-recurring-cost target, and separate authority per implementation work package.

## Incorporate the approved engineering contracts

### A. Generator, deterministic arithmetic, UTC grid and serialization

Transcribe the complete **Section 3** and **Section 4** normative generator/serialization specification from the supplied engineering freeze proposal into the Release definition, preserving precise formulas, bytes, types, constants, order and test vectors. In particular:

- SHA-256 counter input: `ASCII("aiq-synthetic-ohlcv-v1") || 0x00 || UInt64BE(114) || UInt64BE(i) || UInt8(l)`; `U(i,l)=UInt32BE(SHA256(input)[0:4])`; `Z(i,l,m)=floor(U(i,l)*m/4294967296)`.
- `r=i mod 480`; `triangle(i)=r-120` if `r<=240`, otherwise `360-r`; `P(i)=500000000+2500*triangle(i)+Z(i,0,200001)-100000` in 1/10000 price ticks. Boundary indices 0..8160, candle indices 0..8159. Open=P(i), Close=P(i+1), High=max(Open,Close)+1+Z(i,1,50000), Low=min(Open,Close)-1-Z(i,2,50000), Volume=100000+Z(i,3,1000000), with volume divided by 1000. Checked arithmetic, no silent repair.
- UTC origin and complete 4h/24h aggregation; window selection formula `totalIntervalCandles=8160/h`, `visibleCount=24*d/h`, `visibleStartIndex=totalIntervalCandles-visibleCount`, `calculationStartIndex=visibleStartIndex-250`.
- Canonical dataset UTF-8 without BOM, LF and final newline, exact fixed metadata header order, timestamp and fixed-scale row format, dataset/window fingerprints, and operational timestamp exclusions as specified in proposal Section 4. Include all three independently cross-runtime reproduced **row** vectors/hashes and the three proposed full-dataset byte counts/hashes **without silently promoting the latter to independently verified fixtures**.
- Generator/version/constants are immutable within version; modifications require a new version and owner authority. Generate complete base once per cold request, aggregate/select and cache bounded results; no background or startup generation.

### B. Indicators

Incorporate the complete approved **Section 5** analytical contract, including `aiq-ta-v1`, scale `10^12`, exact scaled-integer/rational ties-to-even rounding, rejection of excess input precision, bounded inputs, SMA20, SMA-seeded EMA20, Wilder RSI14, EMA12/26 MACD and EMA9 signal, Bollinger20 population standard deviation via integer square root and exact midpoint comparison, first-valid indices, null/invalid input rules, display-vs-canonical precision, and the reference cases. **Replace** the older Newton-iteration proposal; do not leave contradictory rounding algorithms. Explicitly state that recursive indicators may differ across overlapping ranges because each calculation window uses its own 250-candle warmup seed.

### C. Navigation and architecture presentation

Accept `?page=market-research`, `?page=ml-studies`, `?page=system-health`, `?page=data-governance` in the existing Streamlit 1.61.1 application; missing page defaults to Market Research; unknown or repeated page values fail safely. Same-tab static links, radio synchronization without loops, browser reload/back/forward tests, no new `/architecture/data-governance` deep-path route, no JavaScript or untrusted HTML. Include the exact governance keynote **“Quantitative Research Built with Responsible Data Governance”**, link **“Explore the Architecture & Data Governance”**, and Section 10 responsive accessible **SVG plus complete text equivalent**, visually distinguishing delivered vs planned, synthetic/Twelve Data/Vike roles and inactive future connections. No remote Mermaid renderer or provider call for architecture navigation.

### D. Public protocol, publication gate and private isolation

Accept `aiq-candles-v2`, `aiq-historical-query-v2`, `aiq-historical-market-data-cache-v2`, `aiq-ta-v1`. Public request is server-owned synthetic identity only; reject public v1 observed-market queries, unknown/duplicate/malformed fields, gaps, fingerprint mismatches, private identities and source overrides. Distinct public/private cache roots (`/home/aiq-public-synthetic-cache`, `/home/aiq-private-research-cache`), with public runtime unable to mount/read the private root. Explicit from-scratch child environment, no inherited credentials/provider settings/private paths; secret-free public startup; no private acquisition Worker; public-safe System Health, logs, handoffs, errors, exports and retries. Preserve Twelve Data daily-close observation integration and Vike private-only adapter/qualification. **New private canonical historical OHLCV and automatic failover are deferred** to separately authorized future work, not Release 1.14 release prerequisites. Do not imply Twelve Data ML/retention/publication rights have been established.

### E. Provisional F1 resource engineering targets

Copy the full **Section 7** budget table into the Release definition and trace its enforcement/evidence ownership through the Execution Plan and Manifest. Owner approves these as **provisional engineering limits**, **not observed Azure quotas or deployment readiness evidence**. Include: max 8,160 base/10,540 coexisting candles, 2,410 calculation candles, 2,160 public-visible candles, 4,096-byte bounded request read, 2,097,152-byte stdout enforced **during capture**, 16,384-byte stderr, 8,192-byte chunks, one globally admitted public child, 1s admission, 10s worker and 15s admission-to-reap deadlines, TERM then kill/reap, bounded retry, 12 cache entries at 2 MiB each, 32 MiB disk including temp files, 24h retention, 30-minute session retention, 64 KiB public health handoff, 160 MiB child RSS, 640 MiB container working set, 2 CPU-second cold operation, p95 5s cold/2s warm, aggregate CPU admission ≤60 CPU-seconds/15min and ≤240/day/instance. Ensure concurrent stdout/stderr draining, read-before-append limits, fail-closed overload, bounded locks and no lease-expiry overlap. Profiling and Azure F1 validation **remain pending, separately authorized gates**; no current measurements may be claimed.

## Required edits by file

**`RELEASE_1.14_DEFINITION.md`** — Make the above contracts unambiguous, fully specified and consistent with Section 10. Replace obsolete 'freeze pending' and Newton/route proposals with accepted contract language. Clearly distinguish owner-approved design from unverified candidate dataset fingerprints and unmeasured resource targets. Maintain acceptance traceability and a concise residual evidence/stop register.

**`RELEASE_1.14_EXECUTION_PLAN.md`** — Update WP01 to 'engineering contract owner-approved and incorporated' only after actual edits/checks. Preserve graph and WP execution authorization boundaries. Assign WP03 v2 identity/protocol/publication/cache/stdio; WP02 generator plus independently verified full-dataset goldens; WP04 analytics; WP07 secret-free public startup, global admission, public health and private preservation; WP05 public synthetic candlestick UI; WP06 query navigation and SVG/text architecture; WP08 F1 profiling, concurrency, browser accessibility, isolation and end-to-end evidence. Explicitly record stop conditions and independent release gates.

**`RELEASE_1.14_FILE_MANIFEST.md`** — Preserve existing present-vs-future exact path distinctions. Update contract/version and validation ownership; do not turn any proposed implementation, test, fixture, script or deployment path into a current authorized write. If additional future paths are needed, list them as **proposed only**, requiring separate owner approval.

## Required verification and final report

1. Recompute/validate date/grid arithmetic, all 12 visible selections and warmup start indices. Check accepted hash vectors **as documentary transcriptions**, not independent verification; explicitly report independent full-dataset hash verification as outstanding.
2. Search all three docs for conflicting 273-day / `2026-01-01` baseline, pending-owner contract language, obsolete Newton routing, real/public provider claims, and inconsistently named versions.
3. Validate markdown links, literal repository paths, AC107–AC122/G-1–G-4, dependency acyclicity, and `git diff --check` where applicable. Because the three files are untracked, inspect content directly and do not mistake empty `git diff` for proof they were unchanged.
4. Confirm only the three allowlisted files changed, no staging, no tracked edits, no new files, protected-file hashes unchanged. Preserve other untracked files and local owner work.
5. Report exact files changed, checks performed, remaining **verification** gates (independent full-dataset goldens, indicator independent tests, browser/accessibility, resource profiling/F1, isolation) and any contradictions requiring owner intervention. Do not claim builds, browser tests, provider calls, profiling or deployments unless actually run and separately authorized.

**End state: WP01 documentation updated locally and left uncommitted; no implementation, branch, commit, PR, issue or deployment authorized.**
