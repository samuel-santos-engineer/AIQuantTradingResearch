# Release 1.14 — Baseline Consolidation — Codex Authority

**Authority:** Owner-requested preservation-first PR preparation and conditional merge.  
**Purpose:** Establish a clean, verified `main` baseline before resuming Chart MVP C2B.  
**Scope:** Local repository review, recovery snapshot, selective consolidation, tests, GitHub PR, conditional merge, and post-merge verification. **No Azure deployment or runtime activation.**

## 1. Starting assumptions — verify, do not presume

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Reported protected checkout: branch `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`.
- Reported dirty overlap: `Dockerfile`, `container/entrypoint.sh`, `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`; discover *all* other tracked/untracked/ignored changes.
- Existing synthetic generator has fixed coverage `[2025-10-26, 2026-10-01)` and qualified fingerprints. Do not change coverage, generator identity, seed, outputs, or fingerprint fixtures.
- Public Preview: anonymous, free, **independently generated synthetic OHLCV only**; truthful label `BTC-like Market Simulation — Synthetic Historical OHLCV`; no public Twelve Data/Vike access, credentials, caches or derived private artifacts.
- Twelve Data remains private; Vike public feed inactive; advanced WP07 is **ON HOLD**; `PUBLIC_RUNTIME_VERIFIED=False` must remain false.
- C2B REST API requirements are documentation scope only unless implementation is separately authorized: six read-only endpoints (`/api/v1/health`, `/status`, `/dataset`, `/candles`, `/indicators`, `/diagnostics`).
- The local strategy document may differ from the previously discussed C2B amendment. Verify actual bytes and authority status; do not silently claim an unmerged document is controlling.

## 2. Preservation checkpoint — before any Git mutation

1. Verify repository root, current branch, HEAD, upstream, status including untracked, and remote URLs (redact credentials). Check for in-progress Git operations.
2. Create a **complete recovery snapshot outside the repository** of the current working tree and relevant Git state, including tracked diffs, staged diffs, untracked files and file metadata. Exclude secrets from shared reports; store sensitive backup locally with restrictive permissions. Do not publish the backup.
3. Verify the snapshot is readable and covers every intended local change. Record a manifest and cryptographic hashes; retain the original branch and HEAD as recovery anchors.
4. Classify every changed/untracked file: (a) intended completed work, (b) unfinished disabled WP07 work, (c) C2B governance/documentation, (d) unrelated or sensitive material. Compare with `origin/main` without overwriting local content.
5. **STOP** if the snapshot is incomplete, files cannot be classified, secrets would enter Git history, or the source branch has unexpected changes.

## 3. Consolidation strategy

- Never `reset --hard`, `clean -fd`, discard, overwrite, force-push, or auto-stash the protected checkout. Do not use a linked worktree that changes protected Git metadata.
- Prefer an **independent full clone or standalone repository copy** in a separate directory as the integration workspace. Preserve the original checkout byte-for-byte. Copy only reviewed intended changes from the verified snapshot, with an explicit manifest. Do not copy credentials, private market data, runtime caches, or secrets.
- Create a uniquely named consolidation branch from freshly verified `origin/main` in that independent repository. Do not reuse or rewrite existing branches.
- Include only reviewed, authorized changes. Unfinished WP07 code may be included **only if inert**, security-gated, tests preserved, and the PR explains its disabled status; otherwise exclude and document the reason. Do not merge unsafe or unreviewable code merely to make the checkout clean.
- Reconcile the C2B REST API documentation only to the extent its owner-approved frozen requirements are accurately represented. Do not silently replace historical governance sections or assert unresolved amendments are effective.
- Keep the fixed-window synthetic generator and fingerprint fixtures unchanged. The deferred rolling-90-day proposal must not appear as implemented/current coverage.
- Record file-by-file disposition and before/after hashes for sensitive overlaps (`Dockerfile`, entrypoint, strategy document, generator, security gate).

## 4. Continuous verification loop

Within this authority, continue correcting ordinary, scoped integration defects and rerun tests until evidence passes or a stop condition is reached. Do not stop at the first failing test if a safe correction is available.

Evidence required:

1. **Preservation:** original checkout remains unchanged; recovery snapshot verified; every original change accounted for (included, excluded with reason, or explicitly deferred).
2. **Git hygiene:** PR branch based on verified `main`; clean commit history; no secrets, generated runtime caches, databases, binaries or accidental artifacts.
3. **Generator:** fixed coverage `[2025-10-26, 2026-10-01)`, version/seed/fingerprints and deterministic regression results unchanged.
4. **Security:** `PUBLIC_RUNTIME_VERIFIED=False`; no public provider fallback or Twelve Data/Vike calls; private credentials and datasets inaccessible from public path; admin diagnostics remain disabled.
5. **Application:** run applicable Python/.NET/unit/integration tests and build checks available in the repository. Report exact commands, counts, failures, skips, and environmental limitations. Do not invent passing evidence.
6. **Docs:** frozen synthetic-only scope and C2B REST API requirements accurately reflected; no claim that endpoints or chart are live unless actually verified.
7. **PR:** summarize included/excluded files, risks, tests, no-activation boundary, and rollback procedure. Check required GitHub CI and reviews.

## 5. Conditional PR and merge authority

- You **may** commit reviewed changes, push the isolated consolidation branch, and create a PR against `main` after the preservation checkpoint passes.
- You **may merge** the PR **only if** all required repository checks and review requirements pass, the PR diff is fully classified, no security/fingerprint regression exists, there are no unresolved conflicts, and branch protection permits the merge. Never bypass branch protection or required approvals.
- If any gate fails or review is required but absent, leave the PR open and report **MERGE BLOCKED** with exact evidence. Do not use admin overrides or force merges.
- After merge, verify PR merge state, merge commit SHA, `origin/main`, and tests/check status. Do not modify the original protected checkout merely to claim it is clean. Establish a **new clean development checkout** of merged `main` in a separate directory, or report that manual migration of the protected checkout remains pending.
- No Azure config changes, deployments, public generator activation, credentials, admin enablement, or WP07 lifecycle advancement.

## 6. Mandatory stop conditions

Stop and ask the owner for a narrow decision if: backup cannot be verified; changes are ambiguous or sensitive; intended changes cannot be integrated without altering frozen generator/fingerprints; security gate would need weakening; WP07 must be activated; branch protection/reviews are unmet; tests reveal unresolvable security regressions; or consolidation requires rewriting shared history.

## 7. Final report format

Provide:

- **Disposition:** `MERGED / PR OPEN — BLOCKED / NO PR — BLOCKED`.
- Original branch/HEAD and preservation snapshot location (local path only), manifest/hash verification, confirmation original checkout unchanged.
- Included/excluded/deferred file manifest, especially the three overlapping files.
- Exact commands and summarized test/build/CI results, fixed-window fingerprint comparison, public/private isolation negatives.
- PR URL, branch, commit(s), merge SHA if merged, `main` status, and clean new development checkout path.
- Remaining C2B work, with **production deployment NO-GO**, `PUBLIC_RUNTIME_VERIFIED=False`, and advanced WP07 **ON HOLD**.

**Success means a reviewable, verified consolidated baseline and (if all gates pass) a merged PR plus a clean new checkout. It does not mean public chart recovery or Azure activation.**
