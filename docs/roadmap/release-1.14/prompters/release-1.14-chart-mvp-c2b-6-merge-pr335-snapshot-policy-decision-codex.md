# Release 1.14 — Chart MVP C2B-6: Merge PR #335 and Resolve Snapshot Policy Decisions

## Authority and intent

Owner has **accepted PR #335 as a documentation-only C2B-5 planning milestone**. This authority permits Codex to perform normal safeguarded merge of PR #335, synchronize a clean `main`, and prepare **C2B-6 documentation-only** governance decisions for separate owner review. Acceptance of PR #335 **does not approve** its proposed policy amendments or authorize implementation.

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

PR: https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/335

Expected reviewed PR head (revalidate): `96fb734d90ab5630332d08f36e05f67737fa8699`

Previously reported post-#334 `main`: `fc86f4318b75ba0eb5c62dc0171ede7454148290` (verify current state; do not assume unchanged).

## Phase A — Safeguarded PR #335 merge

1. Inspect current PR state, head, base, file inventory, diff, reviews, conversations and required checks. Expected scope is exactly three documentation files: C2B-5 Codex authority, architecture evidence and **explicitly unapproved** policy proposal.
2. Confirm there are no unauthorized code, dataset, generated artifact, runtime or workflow changes. If head or scope differs materially, stop and request owner review. Respect all branch protections; do not bypass checks or reviews.
3. Merge using a merge commit only when safe and permitted. If blocked, report blockers and stop; do not force merge.
4. Fetch/synchronize the designated **clean separate checkout** onto `origin/main`; confirm zero ahead/behind, clean working tree, actual merge SHA and preservation of the original dirty WP07 checkout and archive. Do not modify or clean the original WP07 worktree.
5. Record the merged C2B-5 documents as **planning evidence, not effective policy**.

## Phase B — C2B-6 decision record and proposed governance amendment (documentation only)

Read the canonical public/private data strategy, Release 1.14 C2A/C2B governance, frozen WP07 rules, PR #333/#334 evidence and merged C2B-5 architecture proposal. Identify exact governing clauses and conflicts. Produce an explicit, narrow **proposed** amendment and decision matrix addressing:

- **Offline snapshot production:** whether and where the frozen deterministic synthetic generator may run; how an exception to cold-generation/export prohibitions is scoped; prohibit anonymous-request generation and startup generation unless separately authorized.
- **Data provenance and artifact trust:** repository/build ownership, trusted generation environment, manifest schema, deterministic version/seed/window/fingerprint verification, tamper detection, promotion and provenance chain. Never put private provider data or credentials into public artifacts.
- **Indicator strategy:** recommend precomputation only if semantics and reproducibility are verified; document exact supported indicators/parameters, calculation parity and handling of warm-up/null rows. Avoid silent behavioral changes to existing REST contracts.
- **Window semantics:** preserve the frozen fixed interval `[2025-10-26, 2026-10-01)` and existing generator fingerprints. No rolling 90-day behavior, regeneration algorithm changes or synthetic/live ambiguity.
- **API bounds:** per-endpoint maximum date span/rows/parameters/payload size, pagination or rejection behavior, stable sanitized error codes, GET-only contracts and cache semantics. Do not fabricate thresholds: justify proposed numbers with measurements or mark them as TBD requiring owner approval.
- **Isolation and admission:** public/private boundary, read-only serving, fail-closed verification, path traversal prevention, bounded concurrency and resource usage, durable abuse protection appropriate to Azure F1 and restart/recycle scenarios. Clearly distinguish verified protections from future controls.
- **Retention and recovery:** immutable version identifiers, retention and cleanup ownership, atomic promotion, startup verification, rollback to known-good artifact, corrupt/missing snapshot behavior and explicit `503 DATASET_NOT_READY` fallback.
- **Operational qualification:** reproducible local tests, adversarial HTTP negative paths, restart/readiness behavior, target Azure F1 constraints and real browser evidence required before any activation request.

Provide alternatives and trade-offs (immutable file snapshot, SQLite, request-time Worker), with a recommendation and concrete **go/no-go gates**. Separate decisions proposed for owner approval from already-frozen contracts; avoid language that treats a proposal as enacted policy. If exact governance source is missing or ambiguous, flag it rather than inventing permission.

## Required deliverables

Create documentation under `docs/roadmap/release-1.14/prompters/` following established lowercase hyphenated naming:

1. `release-1.14-chart-mvp-c2b-6-snapshot-policy-decision-evidence.md` — source references, conflict matrix, decision rationale, alternatives, measurable acceptance criteria, remaining open questions and validation evidence.
2. `release-1.14-chart-mvp-c2b-6-snapshot-governance-amendment-proposal.md` — proposed precise clause-level amendment, clearly stamped **NOT APPROVED / NOT EFFECTIVE**, with rollback/revocation conditions.
3. Preserve this authority file in the same prompters directory if repository convention requires it; do not duplicate or rewrite historical authorities.

Open a **documentation-only PR** against `main` for owner review. Provide PR URL, base/head SHA, exact changed-file inventory, test/document checks, unresolved decisions, and an explicit statement that policy approval and implementation are still pending. Do **not** merge the new C2B-6 PR without a later owner instruction.

## Hard boundaries and stop conditions

- `PUBLIC_RUNTIME_VERIFIED=False` remains unchanged. **No Azure deployment, production activation, or public synthetic generator enablement.**
- **No production code, tests, CI workflows, dependencies, schemas, synthetic datasets, snapshots, SQLite databases, exports, generator seed/version/algorithms/fingerprints, or REST contract changes** in this authority. Existing six GET endpoints remain frozen.
- Advanced WP07 supervisor/guardian/native ABI lifecycle work stays **ON HOLD**; preserve the original worktree and archive.
- Public Preview remains **anonymous, free, synthetic-only**, labeled exactly **“BTC-like Market Simulation — Synthetic Historical OHLCV”**. Twelve Data is private-only; Vike remains inactive.
- No policy amendment becomes effective merely by merging a planning PR. Stop for owner decision before any snapshot creation, cold-generation exception, export, implementation, deployment or public activation.
- Stop and report if PR #335 cannot safely merge, baseline is dirty/unreconciled, source governance conflicts cannot be accurately mapped, or documentation-only scope cannot be maintained.

## Completion report

Report: PR #335 final state and merge SHA; clean-main verification; C2B-6 PR URL and three-file-or-smaller scope as applicable; clause-level proposed decisions; tests/checks run versus inherited; residual blockers; and unchanged NO-GO status. Do not claim browser, Azure or runtime verification unless actually performed.
