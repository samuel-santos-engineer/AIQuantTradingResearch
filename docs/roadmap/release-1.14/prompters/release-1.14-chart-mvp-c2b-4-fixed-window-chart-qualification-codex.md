# Release 1.14 — Chart MVP C2B-4: Fixed-Window Synthetic Chart Qualification

## Authority and objective

Owner-authorized next Codex work package following merged PR #333. Continue from a verified clean `main` (reported merge commit `c9461b20dfcb3f7ec60ffffbcd6013f2aecde285`) toward a functional **local** public synthetic candlestick chart and indicators. Work continuously through permitted analysis, implementation, tests, and defect correction; stop only for a genuine boundary or evidence blocker. This document does **not** authorize Azure deployment or public generator activation.

## Frozen product and security contracts

- Anonymous, free Public Preview; **independently generated deterministic synthetic OHLCV only**, prominently labeled **“BTC-like Market Simulation — Synthetic Historical OHLCV”**. State clearly that prices/volumes are illustrative, the app is a research lab, and it is not investment advice.
- Preserve existing generator algorithm, seed/version, exact fixed coverage `[2025-10-26, 2026-10-01)`, qualified fingerprints, 12 existing selections, 250 hidden warm-up candles, gap semantics, canonical provenance and existing regression baselines. **No rolling 90-day claim or migration** in this work package.
- No public Twelve Data/Vike calls, credentials, observations, caches, private derived outputs, or fallback. Twelve Data remains private; Vike remains inactive.
- Preserve `PUBLIC_RUNTIME_VERIFIED=False` and the fail-closed publication gate. Advanced WP07 supervisor/guardian/native ABI implementation remains ON HOLD. Do not reactivate it, bypass its controls, or claim an alternative is qualified without evidence.
- Retain all six frozen GET endpoints: `/api/v1/health`, `/api/v1/status`, `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators`, `/api/v1/diagnostics`; keep `/_stcore/health`. Errors are bounded, truthful, sanitized, and stable; no public mutation/admin endpoints.
- Zero incremental recurring Azure F1 cost. No production configuration changes or new privileged infrastructure.

## Starting-point verification

1. Verify active checkout is clean `main` at `origin/main`, containing PR #333. If not, record exact state and stop rather than resetting, cleaning, stashing, or overwriting local work.
2. Confirm original WP07 checkout and archive remain untouched; do not operate on them. Inventory current chart, generator, bridge, HTTP API, entrypoint, tests, governance documents and deployment assumptions.
3. Establish current baseline: Python/.NET tests and local HTTP smoke results, including expected `503 DATASET_NOT_READY`; capture commands, counts, exit codes and redacted outputs.

## Execution stages

### A. Architecture and threat/compatibility decision

Trace the complete synthetic-data path (generation → canonical validation/fingerprint → safe publication → REST read → indicator calculation → Streamlit/Plotly). Compare:

- A precomputed **immutable, versioned, fingerprint-verified synthetic snapshot** served read-only;
- SQLite holding only independently generated synthetic candles;
- Existing generator/Worker on-demand execution.

Evaluate provenance, reproducibility, source independence, atomic publication, integrity failure, warm-up, intervals, UTC boundaries, read concurrency, startup/restart, Azure F1 CPU/memory, persistence, public/private isolation, abuse resistance and crash recovery. Explicitly determine whether immutable verified data can eliminate public-request generation and therefore avoid reopening WP07. **Do not assume that precomputation or SQLite automatically meets the frozen security controls.** Recommend the smallest compliant design with documented tradeoffs and unresolved gates.

### B. Bounded local implementation (only if safe)

Implement independently reviewable, fail-closed changes in a new feature branch from clean `main`. Favor read-only, immutable and verified synthetic fixtures/snapshots when evidence supports them. Do not change frozen generator output or fingerprints. Ensure visible chart range and API metadata reflect **fixed** actual coverage. No live exchange identity. Preserve all existing REST contracts and their unavailable behavior if data is not qualified.

If publication can be proven safe within existing boundaries, implement local-only synthetic candle reads, indicators and Plotly rendering with strict request validation, bounded sizes, interval selection, hidden warm-up, consistent provenance, safe gaps and cache/read limits. If safe publication cannot be established without changing frozen WP07 contracts, implement only separable safe components and leave candle/indicator endpoints unavailable. **Never flip the runtime verification gate as a shortcut.**

### C. Evidence-driven tests and failure handling

Run and iteratively fix authorized unit/integration/security tests. Verify: exact generator fingerprints and coverage; OHLCV invariants; UTC interval alignment; deterministic repeatability; correct indicator warm-up; no fake/filled gaps; snapshot/DB integrity and corrupt/missing-file failure; no private provider access or credential leakage; strict input limits; rate/concurrency bounds; stable status/diagnostic error codes; read-only endpoint semantics; browser-visible truthful labels; HTTP health independent of expensive work. Validate startup, restart and read-only recovery locally when possible. Do not claim Azure target-runtime or browser verification from localhost tests.

For each of the six endpoints, capture at least one normal or explicitly unavailable response and one relevant negative-case test; include status, schema, and safe failure behavior. Preserve `/_stcore/health` and Streamlit root. Test failures must be corrected within scope; unresolved mandatory gates remain NO-GO.

### D. Reviewable delivery

Write `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-4-fixed-window-chart-evidence.md` with:

- Baseline SHA, branch, changed-file inventory, commands and test counts;
- Architecture comparison and selected/nonselected options;
- Frozen contracts and fingerprint proof;
- Public/private isolation and admission/recovery/abuse threat assessment;
- API, chart, and local HTTP/browser evidence (distinguish observed from untested);
- Explicit GO/GAP/BLOCK matrix for local shell, local data publication, production public activation, Azure deployment;
- Unresolved owner decisions, precise next actions, rollback strategy.

Create a focused PR against `main` only after local verification; include evidence and non-activation limitations. **Do not merge the PR automatically** under this authority; return the PR for owner review. No Azure deployment, activation, WP07 resurrection, force-push, branch-protection bypass, or edits to preserved checkout.

## Stop conditions

Stop and report exact evidence if: clean baseline cannot be established; a protected local change would be overwritten; generator identity/fingerprint would change; private data might become publicly reachable; a mandatory isolation, integrity, admission or recovery requirement is unqualified; production activation would be needed to test; or an irreversible operation would be required. Do not substitute a green unit test for a missing security proof.

## Completion definition

The work package is complete when a reviewable PR/evidence dossier demonstrates the maximum safely achievable local chart/API functionality **or** clearly documents why data publication must remain disabled, with all frozen constraints intact. Public activation and Azure deployment remain **NO-GO** pending separately authorized target-runtime, security, resource, abuse and browser qualification.
