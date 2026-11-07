# Codex assignment — Release 1.15 WP11: Azure F1 research diagnostic and rollback

## Objective
Turn the merged WP09/WP10 implementation into **actual, sanitized Azure F1 compatibility evidence** for the existing C# research service, then restore the existing research staging app. Focus on execution, not another planning-only milestone. Authentic BTC/USD and ETH/USD browser acceptance remains the ultimate Release 1.15 objective, but is **not authorized in this diagnostic**.

## Baseline and existing authority
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Start from `main` containing PR #404 squash commit `c300087aa250d86de24a64ed4bfe0805d3716c9d`; confirm ancestry and current state.
- PR #403 introduced exact-site, explicit non-durable F1 staging research mode. PR #404 introduced same-container private-stdin bootstrap and read-only diagnostics.
- The owner approved, and approval was recorded in a comment on PR #404, **only**: publish the research candidate image; run a **maximum 60-second diagnostic** on the existing research staging app; stop candidate and restore its exact baseline. This authorization survives merge, but do not expand it.
- Existing resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`.
- Research app ONLY: `aiqr115researchstagingwus27f5eabb5`.
- UI app `aiqr112wp05wus27f5eabb5` must remain entirely unchanged.
- Existing F1 plan only; no VM, disk, plan upgrade, additional paid service, production changes, or unrelated resource discovery.
- Preserve all existing owner dirty/untracked files. Use an isolated worktree. Never leak credentials, config values, provider keys, private data, or image-registry secrets.

## Required work
1. **Reconcile reality before mutation.** Verify merged implementation and current exact-app metadata. Determine whether a diagnostic already ran and whether the research app was restored. Do not repeat an already completed mutation merely to regenerate evidence. Review the existing WP09/WP10 scripts and runbook rather than inventing alternative deployment mechanisms.
2. **Record rollback state privately and safely.** Capture exact research-app image, relevant container settings, app configuration, and any values needed for restoration in protected local storage, without printing secrets or committing them. Verify that a reliable rollback is possible **before** making any change. If registry access, required rights, exact image digest, rollback credentials, or other prerequisites are absent, stop and report a specific blocker; do not substitute a different app or environment.
3. **Publish only the research candidate**, using the repository's existing approved build/publish path. Record immutable image digest and sanitized evidence. No UI image publication.
4. **Run a strictly time-bounded diagnostic** on the named research app: at most 60 seconds of candidate operation. Inspect actual Linux UID, `/proc/self/mountinfo` interpretation, container-root overlay, `/tmp/aiq-research` permissions/write-probe eligibility, exact App Service identity/opt-in conditions, and whether `ResearchStorageBoundary` accepts the staging-only mode. The diagnostic must not import data, start an uncontrolled persistent writer, acquire provider data, expose a public diagnostic endpoint, or pretend a health 200 proves chart readiness. Follow least-privilege and safe sanitized output rules.
5. **Mandatory rollback in a finally-style path** regardless of success, failure, or timeout. Stop the candidate, restore the original research-app image and settings, and verify the baseline. If rollback cannot be confirmed, report that as a critical unresolved state and do not claim success. Do not touch the UI app.
6. **Report evidence precisely.** Separate local tests, actual Azure observations, not-run checks and blockers. Include bounded elapsed time, candidate digest, sanitized mount/UID conclusions, and rollback verification. `PUBLIC_RUNTIME_VERIFIED=False` until authentic end-to-end browser acceptance. No fixtures may be represented as authentic candles.
7. **If code changes are needed**, add only narrowly scoped fixes/tests required for the diagnostic or its safety and open a focused PR for review; do not merge without owner authorization. If no code changes are needed, report the diagnostic outcome without creating a documentation-only PR.

## Strict exclusions
- No Azure UI changes, restart, configuration updates, or image changes.
- No Twelve Data/provider calls or credits; no BTC/ETH export/import/bootstrap in this WP.
- No extra VM, paid hosting, persistent volume, shared storage workaround, production deployment, or governance broadening.
- No research-service permanent deployment, no public browser acceptance claims, no automatic provider acquisition, no silent guard bypass or Azure identity stripping.
- Do not publish private exports, account credentials, keys, settings values, or raw diagnostic payloads.

## Acceptance for this assignment
The result is either **(A)** verified real F1 staging compatibility plus verified exact rollback, **(B)** verified incompatibility plus verified exact rollback, or **(C)** a clearly evidenced preflight blocker with no mutation. Report any incomplete rollback prominently. The next work package, after separate authorization and authentic data prerequisites, will address secure private bootstrap, actual service deployment, and BTC/ETH chart browser acceptance on the existing UI staging URL.
