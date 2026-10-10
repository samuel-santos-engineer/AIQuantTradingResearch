# Release 1.14 WP01 — Codex Synthetic Dataset Coverage Reconciliation Authority

## Status and purpose

**Owner-approved documentation-only correction.** The owner accepted the correction to the Release 1.14 synthetic dataset's start date on 2026-10-10. This instruction authorizes only the narrow planning-document reconciliation described below. It does not grant implementation authority.

## Repository and preflight

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

Before writing, inspect the current branch, `origin/main`, working-tree status, and the three target documents. Do not assume an earlier SHA remains current. Confirm the three documents already exist as uncommitted, untracked files from the preceding WP01 documentation-only work. Preserve all other existing tracked and untracked changes, including pre-existing image assets. If the target files are missing, have unexpected content, or are tracked/modified in an unexpected way, stop and report the discrepancy rather than overwriting them.

Controlling governance: `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`, especially its merged Release 1.14 Section 10. The synthetic-public / Twelve Data-private / Vike-retained architecture is unchanged.

## Literal write allowlist — exactly three files

1. `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
2. `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`

No other file may be created, modified, deleted, staged, committed, or renamed. In particular, do not modify root `README.md`, the controlling governance document, Release 1.13 history, source code, tests, schemas, configuration, provider adapters, GitHub issues, Project metadata, or Azure resources.

## Approved correction — normative

- **Synthetic dataset inclusive start:** `2025-10-26T00:00:00Z`.
- **Synthetic dataset exclusive end:** `2026-10-01T00:00:00Z`.
- **Dataset coverage:** exactly **340 complete UTC daily buckets**.
- **Daily 90D request:** 90 visible daily candles plus exactly 250 preceding daily warmup candles, totaling 340.
- Preserve the complete interval set `1h`, `4h`, `1d` and range set `1D`, `7D`, `30D`, `90D` (12 combinations), including the single visible daily candle for `1d × 1D`.
- Preserve the fixed public seed `114`, proposed generator identity `aiq-synthetic-ohlcv-v1`, hourly base-grid and aligned aggregation contract, and the fixed exclusive end. The changed start date must propagate consistently to any documented grid, dataset length, coverage calculation, examples, validation and acceptance language.
- Public candles remain independently generated **synthetic** OHLCV, never copied, sampled, fitted or derived from Vike, Twelve Data or other restricted real market-data feeds. Maintain the exact synthetic disclosure and public/private isolation boundaries.

### Calculation checks

Verify using UTC calendar arithmetic, not inclusive-end assumptions:

- `2025-10-26T00:00:00Z` to `2026-10-01T00:00:00Z` is 340 days.
- At a complete hourly base grid, 340 × 24 = **8,160 hourly candles**.
- Aligned four-hour aggregates total **2,040** candles; aligned daily aggregates total **340**.
- Visible 90D ranges: **2,160** hourly, **540** four-hour, **90** daily.
- Warmup at each selected interval: exactly **250** preceding candles. The maximum 90D request uses 2,410 hourly, 790 four-hour or 340 daily candles, respectively.

These are planning-level arithmetic assertions, not permission to generate data or golden fixtures. If an existing document uses a different aggregation or warmup convention, report it and reconcile only where it is a direct consequence of the approved correction; do not silently invent additional contract changes.

## Required edits

**Definition:** Replace the former `2026-01-01T00:00:00Z` dataset start and 273-day/67-day shortfall with the approved 2025-10-26 start and 340-day coverage. Mark the specific dataset-coverage blocker resolved by owner approval. Preserve all other open engineering decisions (counter encoding, exact constants/golden vectors, decimal/rounding, routing, resource limits, private OHLCV deferral) as unresolved unless already explicitly accepted elsewhere.

**Execution plan:** Update the dataset contract, WP02/WP03 prerequisites, coverage validation and acceptance expectations wherever the old dates or shortfall appear. The dependency order, owner gates, work-package boundaries, and no-bypass rules remain unchanged. Do not advance implementation status.

**File manifest:** Update any date/coverage references and the decision/stop-rule description consistently. Keep the exact existing and future path boundaries intact; do not turn proposed implementation paths into approved mutation allowlists.

## Validation and report

After editing, perform read-only/documentary checks: search all three documents for obsolete start-date and 273-day/67-day blocker statements; verify calendar arithmetic and twelve combinations; check relative links, whitespace, acceptance IDs AC107–AC122, gates G-1–G-4, and dependency acyclicity where applicable. Run `git diff --check` for tracked changes, and independently inspect the three untracked files because ordinary `git diff` does not show them. Confirm hashes or equivalent checks that protected files and unrelated pre-existing untracked assets are unchanged. Do not run provider API calls, application tests, builds, deployments or credential-dependent operations.

Return: repository baseline; exact three modified files; before/after date and count; documentary checks and any failures; remaining unresolved decisions; unchanged protected files; and explicit statement **DOCUMENTATION-ONLY RECONCILIATION COMPLETE — NO IMPLEMENTATION, COMMIT, PR OR DEPLOYMENT AUTHORIZED**. If the preflight or scope cannot be met, stop and report rather than expanding authority.
