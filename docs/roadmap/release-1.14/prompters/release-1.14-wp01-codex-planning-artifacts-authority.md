# Release 1.14 WP01 — Codex Documentation-Only Planning Artifact Authority

## Authorization and purpose

**Owner approval:** The owner accepted the proposed fixed synthetic dataset, the full twelve interval/range combinations, and the 250-candle warmup as the Release 1.14 **planning baseline**, and explicitly authorized creation of exactly three planning documents. This is a **documentation-only repository-write authority**, not an application implementation or deployment authority.

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`

**Authorized output paths — exhaustive allowlist:**

1. `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
2. `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`

Do not create, modify, stage, delete, rename, or format **any other file**, including the controlling governance document, `docs/project/ROADMAP.md`, older Release 1.13 artifacts, existing prompters, source, tests, lockfiles, root `README.md`, or the untracked architecture image. If any of the three target files already exists, inspect it and preserve/reconcile its content; never blindly overwrite. No GitHub issues, Project changes, branches, commits, PRs, merges, releases, cloud changes, live provider calls, or deployment under this authority. Leave the three files as local reviewable working-tree artifacts for the owner to inspect and separately authorize for commit/PR.

## Mandatory preflight

- Inspect current branch, `git status --short`, `git rev-parse HEAD`, and `origin/main` after fetching if safe; do not assume the prior report's SHA remains current. If the working tree has unrelated changes, preserve them exactly; never clean, stash, reset, or delete them.
- Confirm PRs #316 and #317 were merged and inspect the accepted policy at `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`, especially Section 10, which supersedes conflicting Sections 1–9. Its lingering “Proposed” status is a known editorial inconsistency; **record** a future correction, do not edit the policy in this work package.
- Inspect current Release 1.13 definition, execution plan, manifest, WP01 contract and relevant implementation evidence, and the prior Release 1.14 Codex planning response. Verify exact current source/test paths before putting them in a future allowlist.
- Inspect any of the three target files if present and stop on irreconcilable content conflicts. Do not assume the previous inspection report's absence findings remain true.

## Approved architecture decisions — preserve verbatim in substance

- **Anonymous public:** Independently authored, deterministic **synthetic OHLCV** only, not copied, sampled, fitted, perturbed, or trained from licensed real market data. No anonymous Twelve Data or Vike calls. Display **“BTC-like Market Simulation — Synthetic Historical OHLCV”** and a prominent explanation that prices and activity units are illustrative, not observed market prices or investment advice.
- **Private research:** Twelve Data is the primary external provider, subject to applicable rights and entitlements. The existing implementation currently handles daily close observations; **do not claim** it already supports canonical historical OHLCV.
- **Retained Vike:** Keep its existing adapter and qualification evidence for a future separately approved private backup. No public feed, automatic failover, or equivalence between BTC/USD spot and Hyperliquid BTC perpetual.
- **Common architecture:** Provider-independent canonical candle contract, provenance and market identity, shared SMA/EMA/RSI/MACD/Bollinger calculations, and fail-closed public/private publication isolation. Public process startup, child environment, System Health handoff, caches, logs, and exports are in scope for *future isolation design*, not mutation now.
- **Resources:** Preserve Azure Linux F1 / $0 recurring infrastructure target; no paid services, new packages, new microservices, trading, or ML training.

## Approved Release 1.14 planning baseline

The owner has accepted these for **planning**; do not silently reopen them or treat the planning approval as permission to implement:

| Dimension | Baseline |
|---|---|
| Public identity | `btc-like-simulation`; simulation, not BTC/USD spot or a perpetual |
| Dataset coverage | Fixed UTC base grid from `2026-01-01T00:00:00Z` through exclusive end `2026-10-01T00:00:00Z` |
| Generator | Proposed `aiq-synthetic-ohlcv-v1`, fixed public seed `114`; SHA-256 counter-based deterministic generation, with exact counter encoding/constants/rounding and golden vectors to be frozen in WP01 before implementation |
| Base and aggregation | Generate hourly candles, aggregate into UTC-aligned 4h/1d candles: first open, max high, min low, last close, summed simulated volume |
| Intervals | `1h`, `4h`, `1d` |
| Visible ranges | `1D`, `7D`, `30D`, `90D`, ending at fixed exclusive dataset end |
| Default | BTC-like simulation, `1h`, `30D` |
| Warmup | Exactly 250 prior candles at selected interval, excluded from visible display |
| Numeric contract | Decimal OHLCV, finite values, positive prices, nonnegative illustrative volume; deterministic decimal-safe serialization |
| Completeness | No gaps, duplicates or partial aggregate buckets; invalid data fails closed |
| Fingerprint | SHA-256 of versioned manifest plus ordered canonical rows, independent of wall-clock generated-at time |
| Request bound | At most 2,160 visible candles plus 250 warmup candles |
| 1d × 1D | Exactly one visible candle; indicators may use hidden warmup, never invented visible history |

Visible candle counts: `1h` = 24/168/720/2160; `4h` = 6/42/180/540; `1d` = 1/7/30/90 for `1D`/`7D`/`30D`/`90D`, respectively. State that golden vectors and numeric precision are **still to be fully specified in WP01**; don't invent a verified fingerprint or imply an algorithm has been implemented.

**Proposed indicator contract to formalize in WP01:** SMA20; EMA20 with SMA seed and α=2/(N+1); Wilder RSI14; MACD 12/26/9; Bollinger20 with population standard deviation ×2. Document first-valid positions, null handling, constant/up/down series, deterministic decimal rounding and Bollinger square-root method as explicit design selections requiring validation. Suggested calculation version `aiq-ta-v1`. Do not claim untested numerical conventions are verified.

## Deliverable 1 — RELEASE_1.14_DEFINITION.md

Write a self-contained release definition with: purpose and non-goals; canonical governance precedence; public simulation UX and labeling; source roles and licensing boundaries; fixed dataset/interval/range/warmup baseline; canonical candle, provenance, publication and cache-context contracts; indicators and deterministic semantics; anonymous startup/System Health/environment isolation; public homepage keynote **“Quantitative Research Built with Responsible Data Governance”**; dedicated **“Explore the Architecture & Data Governance”** page with visually rendered Section 10 normative topology, responsive accessible graphic and complete text equivalent; truthful implemented/planned status; detailed conflict register linking obsolete Release 1.13 Vike/BTC/USD/ETH/USD assertions to superseding Release 1.14 requirements; acceptance traceability for **AC107–AC122** and **G-1–G-4**; risks, open engineering decisions, and explicit non-authorities.

## Deliverable 2 — RELEASE_1.14_EXECUTION_PLAN.md

Define bounded WP01–WP08 work packages and gates, including the dependency order proposed by Codex:

- WP01: governance reconciliation and implementation contract freeze.
- WP03: canonical contracts, provenance, publication policy, cache isolation and stdio boundaries (**before WP02**).
- WP02: independent deterministic synthetic generator (**after WP03**).
- WP04: shared deterministic indicators (**after WP03**).
- WP07: Twelve Data/private-boundary preservation, Vike retention and anonymous startup isolation (**after WP03**, before WP05).
- WP05: synthetic candlestick-first UI (**after WP02, WP04, WP07**).
- WP06: homepage keynote and dedicated accessible architecture page (**after WP05**).
- WP08: end-to-end release verification, accessibility/security, bounded child capture, F1 resource acceptance (**after WP06 and WP07**).

For every WP define exact prerequisites, proposed file ownership, tests, negative security cases, G/AC traceability, completion evidence, acceptance owner and stop rules. No WP automatically grants execution rights to the next. Separate **planning** from implementation; each later WP needs its own explicit owner-authorized literal mutation allowlist. Include Mermaid dependency graph, bounded resource/stdio capture concerns, no provider calls in public, and verification that Twelve Data credentials/private handoffs cannot be inherited by anonymous processes.

## Deliverable 3 — RELEASE_1.14_FILE_MANIFEST.md

Record the exhaustive three-file **current** mutation allowlist and protected paths; repository-observed **future proposed** per-WP source/test path lists, clearly distinguished from presently authorized edits; existing vs proposed-new path labels; shared-file sequencing; validation ownership; preservation-only Vike/Twelve Data adapter paths; and conditions requiring additional authority (Docker, routing, schema, packages, Azure, private OHLCV adapter, scripts, tests or UI assets). Do not use wildcard implementation permissions. Preserve root README and the pre-existing untracked image.

## Quality and review checks

- Ensure the three files are mutually consistent, reference Section 10 as governing, and preserve AC107–AC122 / G-1–G-4 without renumbering.
- Check that all twelve combinations and the one-candle case are covered; distinguish simulated activity units from exchange volume, dataset coverage from generated-at, and simulation identity from observed BTC/USD.
- Include exact planned SVG/page requirements but **do not create the graphic or page** under this authority.
- Verify no external public data use or rights assumptions; no private-data/public fallback; no claim of present Azure deployment readiness.
- Check `git status --short`, `git diff --check`, and changed-file paths at completion. Run only documentation-local checks; no network provider calls or app build/test execution is required.
- If required changes fall outside the three paths, document them as future owner decisions and **stop**, without expanding scope.

## Final response to owner

Report: current HEAD and status; the exact three paths created/updated; concise contents; approved baseline carried forward; any unresolved decisions; verification performed; unchanged/protected paths; and explicit statement **“DOCUMENTATION-ONLY WP01 COMPLETE — NO IMPLEMENTATION, COMMIT, PR OR DEPLOYMENT AUTHORIZED.”** Leave files uncommitted for owner review.
