# Release 1.14 Chart MVP — C2B-10 Codex authority

## Guarded merge of accepted PR #339; engineering decision design packet (documentation only)

**Owner authority:** The owner explicitly accepted PR #339 and requested the next Codex instruction. This authorizes a guarded merge of the **exact reviewed PR #339#339** followed by a separate, **documentation-only** C2B-10 engineering-design/decision PR. It does **not** approve an implementation mechanism, authorize active tests or production/export, or grant public activation/deployment.

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`.

## A. Preconditions and preservation

1. Re-fetch PR #339, `main`, protections, checks, reviews, unresolved conversations and exact diff. Expected reviewed head: `eedcb2660e011fdcccb2a9195f5020a2229fa2ad`; expected base predecessor: PR #338 merge `b1fad6255919936dc604888be57d87229a5d910e`. Expected scope: precisely seven Markdown paths (four canonical governance documents and three C2B-9 prompters). **If the head, content or scope differs materially, STOP for owner review.** Do not interpret acceptance as permission to bypass GitHub safeguards.
2. Use an independently verified clean checkout. **Do not touch** the original protected WP07 partial/dirty worktree, its hash archive, or private artifacts. Do not reset, clean, stash or change branches in a checkout with unrelated work.
3. Keep `PUBLIC_RUNTIME_VERIFIED=False`; Public Preview remains anonymous, free, synthetic-only and safe-unavailable for dataset/candles/indicators. Public activation and Azure deployment **NO-GO**; advanced WP07 lifecycle work **ON HOLD**. No public Twelve Data/Vike access or private fallback.
4. Preserve `aiq-synthetic-ohlcv-v1`, seed 114, fixed coverage `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, canonical dataset/window/visible fingerprints, 1h/4h/1d intervals, 1/7/30/90-day selections, 250 calculation-only warmup rows, frozen indicator semantics and exact public label **BTC-like Market Simulation — Synthetic Historical OHLCV**.
5. No source/tests/schema/package/lockfile/CI/Docker/UI/API/database/artifact/fixture/credential/cloud/Project metadata mutation, no generator or provider invocation, no load/fault/native/Azure/browser experiment, no branch-protection change, no force push or unrelated cleanup. Do not rewrite historical authority documents or the verbatim C2B-8 owner register.

## B. Guarded merge of PR #339

1. Verify that PR #337 and #338 are already merged with SHAs `a9d1ca05b7c5d097dd5cd094bf94fc70c4dfe4c2` and `b1fad6255919936dc604888be57d87229a5d910e`, respectively; verify PR #339's current exact head and seven-file Markdown-only patch against the owner-accepted review.
2. Check current mergeability, required reviews/checks, protection/rulesets and unresolved review conversations. Merge #339 only by an ordinary permitted GitHub method with an **exact-head guard**. If any uncertainty, changed head, conflict or required check/review failure occurs, **STOP** and report; no bypass.
3. Re-fetch merge result and record merge SHA, branch ancestry and tree; synchronize safe checkout and verify clean `main == origin/main`, ahead/behind `0/0`. Do not claim a clean checkout unless measured.
4. Only after a verified merge, create a fresh C2B-10 planning branch from that exact merged `main`. Leave the resulting PR **open and unmerged**.

## C. C2B-10 objective — make engineering choices reviewable, not executable

Produce a **decision-ready technical design packet** addressing C2B-9 backlog E1–E8, with alternatives, a clear recommended choice, assumptions, failure modes, dependencies, required evidence and explicit owner decision prompts. Recommendations are **NOT approvals**. Do not convert an unresolved `TBD` into an invented numeric limit or silently select a paid service.

### E1 — Immutable release trust and manifest

Compare a reviewed out-of-band content digest/pin against signed manifest options. Recommend the smallest viable first-party trust anchor for this repository, while stating its threat model, trusted revision and ownership. Specify proposed manifest fields, canonical encoding, member identities, integrity checks, unknown-field handling, release ID derivation and anti-TOCTOU requirements **as design only**. No keys, pins, hashes, manifest schema files or artifacts created.

### E2 — Ownership, promotion and release pinning

Compare image-bundled immutable assets and operator-controlled versioned storage. Address current public ownership of `/app`, actual target-UID read/write permissions, immutable ancestors, whole-release atomic promotion, concurrent reader pinning, rollback and revocation. State exactly which platform/image/mount facts require later verification. No Docker, UID, filesystem or Azure changes.

### E3 — Durable security-control backend and authoritative domain

Compare protected journal, SQLite and any already-available transactional mechanism, including whether each is viable under Azure F1 recycling, instance multiplicity and zero-incremental-cost constraints. Define security-only schema semantics conceptually: bounded atomic admission/reservation, global and client accounting, restart/clock rollback/unknown-state fail-closed behavior, storage protection, minimized identifiers, retention and operator recovery. Distinguish **proven facts** from assumptions. A process-local limiter is not durable. Do not install or select a backend as effective policy.

### E4 — B1 complete independent indicator parity

Design an independent verification approach for all three frozen datasets and twelve qualified windows, including exact canonical hashes, 250 hidden warmup rows, C# calculator parity, SMA20/EMA20/RSI14/MACD12-26-9/Bollinger20x2, exact decimal rounding/null/seed/gap semantics and separately authored oracle evidence. Do not derive expected hashes from production output, invent tolerances, run the generator or export snapshots under this authority.

### E5 — Ready REST/UI payload and full-response limits

Propose **illustrative** response shapes for six existing GET endpoints while preserving current parameter/default/error semantics and fixed selections; mark them **not accepted contracts**. Distinguish 4096-byte current unavailable/error behavior and 2 MiB bridge cap from unknown ready-response budgets. Define the future measurement procedure for full maximum payload, serialization, slow readers, cancellation, full-send admission, memory/CPU, health independence and UI rendering. No runtime implementation or active measurement.

### E6 — Retention, revocation, recovery and privacy

Define proposed whole-release lifecycle, minimum necessary security-control records, expiration safety, operator-only revocation/rollback and recovery from interrupted promotion, restart or corrupt assets. Compare alternatives and explicitly mark member/aggregate/temp/disk/log/retention/response ceilings **TBD — measurement required**. No automatic regeneration, older-file fallback, counter reset or private-source repair.

### E7 — Isolation and G-B0–G-B7 applicability

Provide a gate-by-gate matrix: unchanged requirement, snapshot-specific applicability, proposed evidence, owner acceptance condition and STOP outcome. Include path/mount/environment/FD/`/proc`/network/private-derived-data negatives, no public Worker/generator/provider capability, complete-read admission and durable abuse state. No blanket WP07 waiver or resumption of held advanced work.

### E8 — Exact future allowlist and finite proof plan

Propose **candidate** literal source/test/output paths and bounded sequential proof stages, but do not grant mutation rights. For each stage define preconditions, commands as examples (not to run), expected outputs, independent verification, cleanup, abort criteria, CPU/RSS/time/disk/log/request/concurrency ceilings as **TBD until separately measured and owner-approved**, and rollback to safe unavailable behavior. Separate offline feasibility proof from snapshot production/export, API integration, target Azure F1/browser qualification and activation.

## D. Required deliverables and path scope

On the C2B-10 planning branch, create **only** these Markdown files under `docs/roadmap/release-1.14/prompters/`:

1. `release-1.14-chart-mvp-c2b-10-merge-pr339-engineering-design-decisions-codex.md` — preserve this authority verbatim, apart from disclosed nonsemantic Markdown normalization.
2. `release-1.14-chart-mvp-c2b-10-engineering-options-and-owner-decisions.md` — E1–E8 alternatives, reasoned recommendations, open questions, dependencies, and a compact **approve / revise / defer** decision table. No default recommendation is recorded as owner approval.
3. `release-1.14-chart-mvp-c2b-10-finite-proof-and-qualification-plan.md` — staged, non-executable candidate architecture, literal-path proposal, trust/isolation/indicator parity evidence plan, gate matrix and TBD resource/response thresholds.
4. `release-1.14-chart-mvp-c2b-10-merge-and-design-evidence.md` — exact PR #339 merge/protection/head/base/SHA evidence, scope, sources inspected, distinction between observed facts and assumptions, fresh documentation checks, inherited tests, unresolved STOPs and explicit non-authorization.

If an essential design question cannot be answered without a prohibited experiment, mark it **UNRESOLVED** and describe the narrow later authority needed; do not run the experiment. Do not edit the canonical C2B-9 files again in this phase.

## E. Validation, review and exit

- Audit exactly four new Markdown files and no other diff; check Markdown links/headings, `git diff --check`, owner-register consistency, frozen identities and absence of new runtime authority.
- State clearly which evidence is fresh. Prior Python **162 passed / 11 skipped**, .NET **620 passed** and C2B-7 **16 focused local tests** are **inherited**, not rerun, unless separately and explicitly authorized. No target Azure F1, browser, B1 parity, immutable-ownership or durable-store proof is claimed.
- Open **one C2B-10 documentation-only PR** from verified merged main; leave it **open/unmerged** for owner review. Report PR URL, base/head SHA, exact four paths, merge SHA of #339, validations and all unresolved owner decisions.
- **STOP** if any merge safeguard fails, if policy text is ambiguous, or if the task requires code, snapshot bytes, tests, credentials, platform changes, new paid resources or unapproved budgets.

**Exit condition:** PR #339 safely merged; a complete, reviewable E1–E8 owner-decision packet proposed; no engineering mechanism accepted by implication; `PUBLIC_RUNTIME_VERIFIED=False`; implementation, snapshot production/export/promotion, public activation and Azure deployment remain **NO-GO**.