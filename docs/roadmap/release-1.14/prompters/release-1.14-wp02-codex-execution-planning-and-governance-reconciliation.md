# Release 1.14 — Codex Planning Authority: Synthetic Public Preview and Private Provider Architecture

## Selected execution role

**Codex — planning and repository inspection only.** This authority prepares a reviewable Release 1.14 execution contract; it does not implement, deploy, activate a provider, or approve future work packages. Preserve the owner's substantive acceptance and merge authority.

## Repository and canonical predecessor

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

Before any work, fetch and inspect current `origin/main`, the current branch and worktree. Verify merged PR #316 and the controlling governance file:

`docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`

Section 10 (2026-10-10 amendment) **supersedes conflicting public Vike/Hyperliquid assumptions in Sections 1–9**. Treat this as approved governance following PR #316, even though its status line still says “Proposed”; identify that status-line cleanup as a documentation reconciliation item, not a reason to reactivate the old policy. Do not assume a fixed predecessor SHA. Do not overwrite existing untracked or modified files.

Inspect the earlier Release 1.13 planning/contract artifacts, current roadmap, architecture/provider assessments, Twelve Data and Vike adapters, canonical market-data contracts, Streamlit UI/navigation, tests, package pins, Azure F1 deployment constraints, and any Release 1.14 artifacts actually present. The Release 1.14 definition and execution-plan files were absent when this prompter was drafted; recheck before proposing them.

## Mission

Produce an implementation-ready, **documentation-only Release 1.14 plan** that reconciles historical requirements with the accepted data strategy, identifies exact work-package boundaries and dependencies, and freezes acceptance and validation expectations before implementation authorization.

Do not silently rewrite Release 1.13 history. Where older requirements conflict, document the conflict and superseding authority explicitly.

## Non-negotiable product and data decisions

1. **Public preview:** Independently generated, deterministic **synthetic** OHLCV only. Never derive the public dataset by copying, perturbing, sampling, or training on Vike/Twelve Data market data. No external provider requests in anonymous public flows. Label: **“BTC-like Market Simulation — Synthetic Historical OHLCV”** and visibly disclose that it is illustrative, not actual prices or trading advice.
2. **Private research:** Twelve Data is the primary external provider, subject to applicable private-use, retention, derived-data and ML terms. Keep private credentials, caches, data, models, features, exports and logs isolated from public surfaces.
3. **Retained Vike:** Preserve its existing adapter and qualification evidence for a later provider-agnostic/private-resilience release. No public activation, automatic failover or implied BTC/USD spot ↔ Hyperliquid BTC perpetual equivalence.
4. **Shared core:** Provider-independent canonical candle contracts and deterministic SMA, EMA, RSI, MACD and Bollinger computations. Preserve source, simulated-versus-observed status, market type, venue, quote/settlement where applicable, UTC interval, units, provenance, generator seed/version, fingerprint and calculation version.
5. **Operating model:** Azure Linux F1 and zero recurring infrastructure subscription cost. No trading/order execution, new paid services or unbounded background polling.

## Required planning work

### A. Governance reconciliation

Produce a precise conflict register with file path, section, obsolete assertion, replacement decision, proposed documentation-only resolution, and authority. Include the inherited BTC/USD / ETH/USD public selectors, Vike attribution/public provider assumptions, interval/range matrix, public freshness wording, market identity and existing public UI claims. Do not infer that a BTC-like simulation is actual BTC/USD or a perpetual contract.

Preserve acceptance IDs **AC107–AC122** and gates **G-1–G-4**, with traceable revised interpretations. Recommend targeted status correction for Section 10 and clarify the distinction between the approved public synthetic baseline and future separately licensed real feeds.

### B. Freeze the public simulation contract

Specify deterministic seed/version, reproducible UTC timestamp grid, candle-open boundaries, intervals and range controls, generator independence from licensed data, finite numeric OHLC invariants, volume semantics, bounded warmup, gap policy, deterministic regeneration and dataset fingerprint. Evaluate the previously proposed `1h/4h/1d` × `1D/7D/30D/90D` matrix against the current code and chart requirements; freeze or explicitly flag decisions requiring owner approval. No synthetic values may be represented as market observations.

### C. Canonical analytics and isolation

Identify existing repository-native adapters/interfaces and the smallest viable extension for a synthetic source without a parallel uncontrolled pipeline. Define fail-closed publication policy, distinct public/private cache namespaces, source provenance and prevention of accidental private fallback. Preserve Twelve Data and Vike source files. Freeze indicator input/output, warmup, null handling and deterministic test expectations without installing packages.

### D. Public UI and architecture page

Plan a candlestick-first public research surface with clearly synthetic labeling, interval/range controls, OHLCV hover, volume, indicator overlays, accessible responsive states and truthful generated-at versus market-as-of timestamps. Plan the homepage keynote **“Quantitative Research Built with Responsible Data Governance”** and working navigation **“Explore the Architecture & Data Governance”** to a dedicated page (proposed `/architecture/data-governance`, subject to existing routing).

The dedicated page must **visually render** the normative architecture from Section 10, not merely link to the Markdown. Require a responsive, accessible graphic (SVG or equivalently accessible rendering) and a textual equivalent showing public synthetic data, private Twelve Data, retained future Vike, canonical contracts, shared analytics and the publication gate. Distinguish delivered versus planned capabilities. Treat the diagram in Section 10 as the source of truth; any prior infographic is an illustration, not normative implementation authority.

### E. Work-package execution plan

Evaluate this candidate sequence against actual repository dependencies, adjusting identifiers only with documented rationale:

- **WP01:** Governance reconciliation and implementation contract.
- **WP02:** Deterministic synthetic OHLCV generator and fixture contract.
- **WP03:** Canonical contracts, provenance and public/private isolation.
- **WP04:** Deterministic technical indicators.
- **WP05:** Public candlestick UI and synthetic disclosures.
- **WP06:** Homepage keynote and public architecture/data-governance page with diagram.
- **WP07:** Private Twelve Data preservation and retained Vike boundaries (no automatic failover).
- **WP08:** End-to-end verification, accessibility, security, Azure F1 resource assessment and release acceptance.

For each WP provide purpose, prerequisites, exact anticipated files (from inspection, not guessed), mutation allowlist proposal, tests, AC/gate traceability, exit criteria, reviewer decision and no-bypass rules. Separate the plan from later executable implementation authorities. Do not create issues or Project items.

## Planning deliverables

After inspecting existing repository patterns, prepare the minimum necessary **documentation-only** Release 1.14 planning artifacts, subject to explicit literal path allowlisting before editing. Candidates include:

- `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`
- a concise requirements/acceptance reconciliation record if repository conventions require it.

The above paths are **candidates, not an unconditional mutation allowlist**. Verify absence/presence and exact project conventions first. Do not mutate the controlling governance document automatically; propose any Section 10 status correction and legacy text consolidation for owner review within the planning PR if separately authorized.

Report the exact proposed path allowlist and produce reviewable drafts only if operating under a subsequent explicit repository-write authorization. Under this prompter alone, **inspect and report the complete proposed plan; do not write or commit files**.

## Validation and stop conditions

Check proposed acceptance coverage, dependency acyclicity, data-publication safety, provider identity semantics, indicator warmup, accessible diagram requirements, Azure F1 cost envelope, and unchanged root README. Flag unknowns as decisions, not assumptions. Do not claim deployment readiness from planning evidence.

Stop and report `GOVERNANCE RESTRICTION — STOPPED` if the next action requires source changes, credentials, live provider calls, new packages, schema changes, Azure changes, paid services, GitHub issues/Project changes, commits, PRs or merges. Do not erase unrelated worktree changes.

## Final Codex response format

Return: (1) repository truth and inspected files; (2) conflict register; (3) frozen decisions versus open owner decisions; (4) proposed WP dependency graph; (5) exact candidate document paths and future implementation allowlists; (6) AC107–AC122/G-1–G-4 traceability; (7) diagram/public-page requirements; (8) validation plan and risks; (9) explicit statement **NO IMPLEMENTATION OR DEPLOYMENT AUTHORIZED**.

Stop and await owner review.
