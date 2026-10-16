# Release 1.14 — Chart MVP C2B-7: Merge PR #336 and Qualify Snapshot Implementation Readiness

## Authority and intent

The owner accepts PR #336 as the C2B-6 documentation milestone and authorizes its guarded merge. This authority covers **merge verification and a new documentation/evidence-only C2B-7 readiness PR**. Acceptance of PR #336 does **not** by itself enact the proposed governance amendment or authorize an exception to frozen cold-generation/export prohibitions. Any ambiguity about policy approval is a STOP, not implied permission.

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
Accepted PR: https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/336
Expected reviewed head: `77495399372b0814e128a7cce66acd434986ac0c` (verify live; if changed, inspect diff and report before merging).
Expected baseline from prior evidence: `b7eec6376c4a761adc4f547d65ae403608ce35b1` (do not assume still current).

## Phase A — Safely merge PR #336

1. Work only in the clean, separate development checkout; preserve the original dirty WP07 worktree, deferred files, archive and Codex-internal references.
2. Fetch current remote state and inspect PR #336: target `main`, head, three-file Markdown-only inventory, diff, conversations, reviews, required checks and mergeability. No bypass of branch protections.
3. If scope drift, unexpected conflicts, failing required checks, unresolved review comments, or protection obstacles arise, STOP and report; do not force merge.
4. Merge using a normal GitHub merge commit after safeguards pass. Record PR URL, exact merge SHA, final head and checks/reviews status. Do not infer CI checks or approvals that do not exist.
5. Synchronize clean `main` to `origin/main`, verify identical commit IDs and clean status; use a fresh branch for C2B-7.

## Phase B — C2B-7 readiness analysis (documentation only)

Read the controlling public/private data strategy and C2B-4/5/6 evidence and proposals in `docs/roadmap/release-1.14/prompters/`; cite exact file paths and sections in the dossier. Distinguish **existing binding policy**, **proposed but unapproved amendments**, **owner-accepted planning conclusions**, and **remaining unknowns**. Never silently promote proposal language into effective policy.

Produce an implementation-readiness specification covering:

- **Snapshot provenance and ownership:** deterministic offline build inputs, exact frozen generator/version/seed/window, manifest schema, schema and fingerprint checks, immutable artifact naming, trusted producer and reviewer, atomic promotion, provenance/audit trail, rollback and retention.
- **Frozen data contract:** `[2025-10-26, 2026-10-01)`; no rolling windows, algorithm/seed/fingerprint modifications, live feeds, Twelve Data/Vike exposure or public fallback.
- **Indicators:** recommend and document precomputed SMA/EMA/RSI/MACD/Bollinger behavior, warmup/null semantics, parameters, deterministic reproducibility and verification; do not implement or assert equivalence without evidence.
- **Public API/UI:** preserve all six GET-only routes and current error contracts; propose bounded candle/indicator pagination or limits, date filtering, payload-size limits, sanitization, cache semantics, safe 503 behavior when invalid/unavailable, and prominent exact synthetic labeling.
- **Security and isolation:** immutable read-only serving; no anonymous request-time generator, file export, privileged endpoint, private provider access or mutable shared state; document admission controls, abuse resistance, concurrent load, recovery, tamper detection and fail-closed behavior.
- **Runtime qualification:** propose reproducible measurements for Azure F1-compatible CPU, memory, cold start, restart, concurrency, payload and latency, disk/quota and log safety. Mark unmeasured thresholds **TBD — measurement required**; never fabricate passing limits or claim Azure evidence from localhost tests.
- **Decision register:** record each unresolved choice, alternatives, rationale, risk, owner decision required, acceptance test and explicit go/no-go condition.

## Deliverables

Create only documentation in `docs/roadmap/release-1.14/prompters/`, using the established lowercase hyphenated convention:

1. `release-1.14-chart-mvp-c2b-7-merge-pr336-snapshot-implementation-readiness-codex.md` — preserve this authority verbatim.
2. `release-1.14-chart-mvp-c2b-7-snapshot-implementation-readiness-evidence.md` — baseline, source references, measurements actually performed, gap/decision register, verification and STOP findings.
3. `release-1.14-chart-mvp-c2b-7-snapshot-implementation-specification.md` — reviewable design and staged test/rollback plan, explicitly **not implementation authorization**.

Run documentation checks and any fast relevant read-only tests where practical; report newly run results separately from inherited Python (162 passed, 11 skipped) and .NET (620 passed) evidence. Do not change production code, tests, CI, generator, data artifacts, API behavior, runtime configuration or Azure resources in this phase. Open a new **unmerged** documentation-only PR against `main`; report URL, base/head SHA, inventory, checks, decisions and blockers.

## Hard gates

- `PUBLIC_RUNTIME_VERIFIED=False` must remain unchanged.
- Advanced WP07 remains on hold; original worktree and archive untouched.
- No snapshot production, export, publication, runtime activation, Azure deployment, policy enactment or C2B-7 PR merge.
- Public Preview stays anonymous and synthetic-only; Private Research remains isolated.
- If the proposed amendment is necessary to execute a step, STOP at documentation and request explicit owner approval of its exact final text.

## Completion report

State PR #336 merge outcome and SHA; clean-main proof; C2B-7 PR URL and changed-file list; documentation/test evidence; resolved versus TBD decisions; exact approvals required before any implementation. Never describe a proposed amendment as already effective.