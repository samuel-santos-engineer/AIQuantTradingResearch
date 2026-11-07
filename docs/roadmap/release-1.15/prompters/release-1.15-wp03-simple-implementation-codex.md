# Codex continuation — Release 1.15 WP03 simple functional implementation

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Date: 2026-10-24
Target: WP03 #369 — real completed one-minute BTC/USD and ETH/USD private research storage and ingestion, offline first.

## Binding owner decisions

- **Prefer the simplest sound lab implementation.** Deliver real functionality, not another planning-only or fixture-only PR. Avoid speculative abstractions, distributed leases, background orchestration services, expensive infrastructure, and elaborate recovery machinery beyond the existing D3 contracts. Record a real limitation instead of silently claiming it solved.
- **Production is completely off limits, including read-only inspection.** Eventual publishing, milestone assessment and Azure qualification are **staging only**, under separate future explicit deployment authority. This prompt authorizes no Azure, registry, provider, credential or deployment operation.
- Two independent non-root, single-process Docker images remain the runtime target. One active service API key; no rotation. Standard synchronous HTTPS/native DNS timeout limitation is accepted; do not redesign it.
- No fabricated candles, synthetic production history, fake successful D4 snapshot or premature WP04/WP05/WP06 work. No licensing approval gate or paid dependency.
- `PUBLIC_RUNTIME_VERIFIED=False` until actual accepted staging qualification.

## 0. Verify baseline and preserve all owner work

1. Read `AGENTS.md`, `.gitattributes`, LF governance, D1-B amendments, Release 1.15 execution plan, D3/D4/D5 canonical contracts, and merged `docs/roadmap/release-1.15/RELEASE_1.15_WP03_WP04_FOUNDATION_PREFLIGHT.md` plus `RELEASE_1.15_WP03_WP04_PROMPT_PROVENANCE.md`.
2. Verify PR #388 merged at `b066124713f6a6f25ae7fdb0f57300ed4a83c853`. Check PRs #386 and #387 live; they were OPEN/UNMERGED at last verification. **Do not merge either without separate approval.** Rebase new work on verified current `main`, not unmerged PR content; report conflicts.
3. Before work, inventory **every dirty/untracked file including all untracked Codex `.md` prompts**: exact path, bytes and SHA-256. Preserve original content, names, status and hashes; no clean/reset/stash/overwrite. Work in a fresh isolated worktree or checkout. Compare against already committed prompt archives from #388 and **include any newly discovered unarchived prompt** in a separately enumerated documentation path if permitted; otherwise record a proposed archive path and retain the original. Do not duplicate or overwrite archived prompts.
4. Inspect exact current C# project, SQLite, Twelve Data and test conventions; verify proposed paths are still available and no concurrent ownership conflict exists.

## 1. Authorized WP03 implementation scope — literal paths only

The owner approves proceeding with the **11 WP03 candidate paths** enumerated in merged `RELEASE_1.15_WP03_WP04_FOUNDATION_PREFLIGHT.md`, subject to verifying their exact names and ownership there. **Treat that merged document as the authoritative literal path list; do not infer paths from this prompt, wildcard directories, or silently expand it.** If any essential code requires an additional path (e.g. composition root, project file, or deployment config), do not modify it: implement the coherent subset within the approved paths, document the exact missing path and request narrow amendment in the PR.

Allowed supporting documentation: append dated actual test/limitations evidence to the existing WP03/WP04 foundation preflight and existing manifest only if needed; any **new** evidence or prompt archive path must be named exactly and approved before writing. No edits to WP02 fixtures, WP04 source, WP05 endpoints, WP06 UI, Azure configuration, Dockerfiles or old Release 1.14 code.

## 2. Minimal functional vertical slice

- Define private, immutable completed-candle and committed-state reader contracts for **BTC/USD and ETH/USD**, one-minute UTC openings, validated OHLCV and provenance. Enforce actual completed timestamps, positive prices, high/low consistency, nonnegative volume, scale/precision bounds, max 1,000 retained actual candles per instrument. Reject invalid data rather than clamp or fill missing candles.
- Implement a **separate research-only SQLite database** using the already pinned `Microsoft.Data.Sqlite`; preserve historical observation tables and factory behavior. One local writer, transactional per-instrument version/generation updates, atomic committed-state read, deterministic duplicate handling, correction and gap/segment bookkeeping. Keep calculated research values explicitly unavailable until WP04; never claim Ready.
- Prefer straightforward SQLite transactions and existing SQLite locking. WAL and `synchronous=FULL` may be used where the actual local filesystem supports them; validate configuration. A simple process-lifetime local lock is enough for the offline lab slice. Do **not** invent a cross-App-Service writer-exclusion guarantee or add distributed locking. Record staging filesystem/exclusivity as a later WP07 qualification item.
- Build a bounded Twelve Data **one-minute OHLCV adapter** using injected offline HTTP fixtures. Default tests must make **zero live provider requests** and use no real API keys. Respect configured quota/admission/fairness, completed-minute grace and bounded response handling; do not assume provider plan entitlement from fixtures.
- Implement the simplest correct checkpoint/watermark/recovery behavior feasible under D3. Never claim cross-file atomicity. If commit succeeds but outside watermark update fails, report degraded protection, not a failed commit. Validate restart, WAL/rollback/corruption, correction and lineage conflicts. If a D3 recovery obligation cannot be met within the exact paths and simple design, **fail closed**, report the precise limitation and keep readiness false; do not fake acceptance or invent a new service.
- Provide one coherent immutable read API that future WP04 projection and WP05 transformation can consume. No raw/private state is accessible to public UI. Do not build D4 response or D5 factor here.

## 3. Deterministic qualification

Implement the WP03 tests in the 11 exact approved paths. Cover: BTC/ETH separation, UTC boundary/completion, valid/invalid OHLCV, gaps, idempotent duplicates, correction and monotonic versions, bounded 1,000 retention, atomic rollback on injected fault, coherent read snapshots, restart persistence, watermark ahead/behind, corrupted checkpoint and recovery fail-closed behavior, single local writer exclusion, quota fairness and offline-only provider handling. Verify no fabricated candles or private secret leakage.

Run focused .NET tests **without restoring/upgrading dependencies if local assets suffice**. Record actual commands, counts, failures, runtime and source hashes. Do not claim tests that were not executed. Check UTF-8 without BOM, LF-only, final newline, `git diff --check`, and preservation of owner-file hashes. If environment prerequisites are missing, report them accurately rather than installing or claiming success.

## 4. Delivery

- Create **one narrowly scoped WP03 implementation PR**, leave **OPEN/UNMERGED** for owner review. Report all literal changed paths and why, what genuinely works, test evidence, unresolved D3 obligations, and the immutable reader contract needed by WP04.
- Keep #369/#370/#371 open and `PUBLIC_RUNTIME_VERIFIED=False`; do not claim WP03 acceptance, WP04 readiness, actual staging/browser/F1 proof or production readiness.
- Do not deploy or publish images, use Azure CLI, read staging/production settings, call live Twelve Data, or touch production. Staging publishing will occur later under separate explicit authorization.

**Primary goal: build a real, small, testable WP03 foundation now, with honest limitations, so WP04 can follow immediately.**
