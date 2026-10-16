# Release 1.14 — Chart MVP C2B-5: Merge PR #334 and Qualify Immutable Synthetic Snapshot Architecture

**Authority:** Owner acceptance of PR #334 and authorization to prepare the next Codex execution step.
**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`
**PR:** https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/334
**Intended base:** `main`
**Mode:** Merge verified C2B-4 evidence milestone; then documentation-only C2B-5 architecture assessment.

## 1. Outcomes

1. Verify and merge PR #334 without bypassing repository protections; verify the merge SHA and a clean, up-to-date `main` baseline.
2. Produce a reviewable C2B-5 architecture decision packet on whether immutable, prevalidated synthetic snapshots can safely replace anonymous request-time generation while honoring frozen public/private isolation and Azure F1 constraints.
3. Identify the exact governing cold-generation, export, pre-generation and cache-publication provisions that would require explicit amendment. **Do not treat this authority as approving those amendments.**
4. Report GO/GAP/BLOCK for any later implementation, target-runtime qualification and activation; default to NO-GO until separately approved.

## 2. First stage — PR #334 merge

- Confirm PR #334 still targets `main`, contains only the reviewed evidence, prompt and negative-test changes, and has no unexpected commits, conflicts, unresolved conversations, or required failing/pending checks.
- Confirm existing test evidence (reported: 162 Python passed, 11 skipped; 620 .NET passed); rerun relevant checks when environment permits and accurately distinguish fresh from inherited evidence.
- Do not bypass branch protection, reviews or required checks. If prerequisites fail, stop the merge and report a specific blocker.
- Merge with a merge commit if permitted; record PR URL, reviewed head, merge SHA, checks and resulting `main` SHA.
- Update the **active clean checkout** to `main` using safe operations only. Never reset, stash, clean, checkout over, or modify the archived/original dirty WP07 checkout. Stop if the active checkout is unexpectedly dirty.

## 3. Second stage — C2B-5 documentation-only decision packet

Create a dedicated C2B-5 planning branch from the verified clean `main`. Inspect current source, tests and controlling architecture/business policy; do not assume an older proposal is effective merely because it exists in documentation.

Compare at least:

- **A. Existing request-time frozen generator / Worker path:** security, admission, cleanup, resource, cold-generation and availability constraints.
- **B. Offline-generated, immutable, integrity-verified synthetic snapshot:** generation authority and environment, provenance, no derivation from observed markets, artifact creation and promotion, read-only publication, startup validation, corruption handling, rollback, refresh/retention, and prevention of public request-time generation.
- **C. SQLite synthetic-only store:** additional writable-state, locking, corruption, durability, publication and deployment concerns versus immutable files.

For each option assess: confidentiality and absence of Twelve Data/Vike calls; isolation; no unauthorized cross-context fallback; deterministic fingerprints; integrity verification; resource and abuse controls; rate/concurrency limits for API reads; Azure F1 storage/restart behavior; zero incremental recurring cost; REST readiness and failure codes; browser chart behavior; operational recovery and evidence needed.

Explicitly map every proposed deviation to its existing governing requirement, required policy change, verification gate, owner decision and rollback. The current frozen public preview scope remains synthetic-only. Preserve existing fixed coverage `[2025-10-26, 2026-10-01)`, generator identity/version/seed, fingerprints, canonical contracts and indicator warm-up unless a later authority expressly changes them. Do not advertise a rolling 90-day window.

Produce a concise architecture recommendation and an acceptance matrix covering snapshot provenance, deterministic repeatability, immutable artifact validation, deployment atomicity, dataset readiness, API and UI integration, negative isolation tests, rate limiting, crash/restart recovery, Azure F1 headroom and honest failure states.

## 4. Allowed and prohibited actions

**Allowed:** Read-only repository analysis; bounded local test runs; merging PR #334 after verified safeguards; a new planning branch; editing only C2B-5 documentation and evidence; committing/pushing that planning packet; opening a new **unmerged** documentation-only PR for owner review. Continue ordinary documentation/test-evidence corrections without repeated approval.

**Not authorized:** Changes to application/runtime code, Dockerfile, entrypoint, generator, fingerprint fixtures, database schema, credentials or Azure; generating/exporting/publishing snapshots; enabling public candles; altering `PUBLIC_RUNTIME_VERIFIED=False`; reviving advanced WP07; Twelve Data/Vike public access; changing existing security gates; merging the new C2B-5 PR; production deployment.

Preserve the original WP07 checkout and its archive without modification. If branch state, policy authority, provenance, or preservation is ambiguous, stop that affected action and report evidence rather than forcing progress.

## 5. Deliverables and naming

Use established lowercase, hyphenated Release 1.14 Chart MVP naming:

- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-5-immutable-snapshot-architecture-evidence.md`
- Any additional proposed amendment: `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-5-immutable-snapshot-policy-proposal.md`

The evidence dossier must include: PR #334 merge evidence, clean-main verification, exact changed-file inventory, governing-policy references, alternatives matrix, conflicts and amendment text proposals, tests executed (and not executed), explicit remaining gates, next-step authority needed, and a clear **public activation / Azure deployment NO-GO** disposition.

## 6. Stop and report conditions

Stop the affected stage for: merge protections not satisfied; unexpectedly dirty active checkout; risk to preserved WP07 state; unclear effective policy; proposed change requiring implementation or runtime activation; unexpected security or fingerprint regression; unavailable evidence that cannot be honestly obtained. Report completed safe work and precise next decision. Do not claim that an architecture recommendation constitutes approval or qualification.
