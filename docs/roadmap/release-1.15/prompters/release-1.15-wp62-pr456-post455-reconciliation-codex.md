# Codex task — Release 1.15 WP62: reconcile PR #456 after #455 merge

## Verified starting point
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- PR #455 was squash-merged into `main` as `2ab325e9e0a1fbdb8b3a8a4266de6ee30f0a6fa0`.
- PR #456 (`feat/release-1.15-wp62-observation-helper`) was stacked on the former #455 branch. Last reviewed head: `400de691d825b0359aef82f666ff865d5fbb2c47`. Refresh all refs before work; do not assume unchanged.
- #456 contains only `eng/azure-cli/r1.15-qualification/wp62_staging_observation.py`, `eng/azure-cli/r1.15-qualification/test_wp62_staging_observation.py`, and `docs/roadmap/release-1.15/prompters/release-1.15-wp62b-bounded-quota-event-observation.md`.

## Authority and objective
Reconcile #456 onto the now-merged `main` without losing any WP62-B evidence or the latest offline hardening. Qualify the final PR diff, tests and default fail-closed behavior. Leave PR #456 OPEN and UNMERGED for owner review. No authority to execute Azure operations, activate the live adapter, or merge.

## Procedure
1. Inspect fresh remote `main`, #456 base/head, PR metadata and exact diff. Preserve unrelated local changes; use an isolated worktree or clean disposable checkout if necessary. Do not force-push or rewrite shared history unnecessarily.
2. Rebase or otherwise reconcile #456 onto current `main` while preserving exactly the intended three-file delta. In particular, retain the entire merged WP62-B evidence chronology from #455, adding only #456's helper-hardening section; do not duplicate or erase historical evidence. Update the PR base to `main` if needed, using normal supported GitHub operations. If history rewrite is unavoidable, use a carefully verified lease and explain it; stop if ownership or remote drift is uncertain.
3. Preserve `LIVE_ADAPTER_QUALIFIED=False` and the unconditional live-apply block, including with approval flags. Preserve plan-only default, exact Azure target/digest allowlists, no retry loops, bounded output/process-tree cleanup, quota/drift checks, private-baseline fail-closed behavior, and conservative `candidateBound=False` classification.
4. Run offline tests only. On genuine Windows, run the opt-in test suite and verify process-tree cleanup. Windows ACL test may remain SKIPPED only for the documented `PrivilegeNotHeldException`; do not treat skip as success. On other OSes, report limits honestly. Test default plan-only execution and blocked live-apply flags without instantiating real Azure adapters. Validate syntax, UTF-8/LF/no BOM, Markdown links/tables and `git diff --check`.
5. Inspect whether the PR is cleanly mergeable against `main`, and verify its changed-path inventory is exactly the three approved paths. Update PR description with final tests, skips, limits and base/head. Do not merge.

## Hard prohibitions
- **ZERO Azure operations:** no Azure CLI commands that authenticate or call cloud services; no ARM/SCM calls, quota reads, health requests, log streams, restarts, configuration changes, deployment or imports.
- No registry operations, Docker builds/pulls/runs, provider calls, key/TLS/retained DB access or private-baseline access.
- No enabling of the live adapter, no Windows privilege elevation, ACL weakening, policy bypass, or new runtime dependencies.
- No changes outside the three approved paths unless a separate explicit approval is obtained.

## Acceptance report
Return: PR URL, verified `main` SHA, PR base/head SHAs, changed-file list, mergeability, test counts (including skips), Windows process cleanup status, ACL and SCM limitations, and an explicit declaration of zero Azure operations. Preserve `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false`. If reconciliation is blocked, leave #456 open and explain the smallest safe next step.
