# Codex authority — Release 1.14 production evidence publication and milestone closure

## Owner instruction and objective

Complete **documentation-only** Release 1.14 closeout in `AIQuantTradingResearch`. Publish the **original existing local** production-publication evidence file via a narrowly scoped GitHub PR, merge that PR after checks, and close GitHub milestone **#65** (`Phase 4 - Release 1.14: Technical Analysis & Quantitative Feature Foundation`). The owner expressly authorizes this scoped PR, merge and milestone closure without further approval if all gates pass.

**Absolute prohibition: Do not touch, edit, stage, reformat, rename, or update any README file.** Do not change application code, tests, deployment configuration, runtime, Azure, Docker images, GitHub Actions, governance flags, release definitions, existing evidence files, or other issues/PRs. No build, publication, redeployment, provider search or live-data work.

## Authoritative source file

The original file should exist in the owner's local checkout:

`docs/roadmap/release-1.14/prompters/release-1.14-c2b-37-production-publication-evidence.md`

Repository-relative destination (exact):

`docs/roadmap/release-1.14/prompters/release-1.14-c2b-37-production-publication-evidence.md`

**Use the original bytes and contents. Never reconstruct, paraphrase, fabricate, or replace the evidence from conversation summaries.** If the file is missing, unreadable, already tracked with conflicting content, or contains secrets, tokens, connection strings, credentials, personal identifiers or other inappropriate publication material, **STOP** and report the blocker; do not silently redact or generate a substitute. Check the file is Markdown and appropriate for a public GitHub repository. Report its SHA-256 before and after publication; the committed blob must preserve the original content (Git text encoding/newline conversions must not silently change it).

## Existing release evidence and boundaries

- Merged indicator PRs: #355 EMA20/Bollinger, #356 RSI14/MACD, #357 ATR14; SMA20 retained.
- Consolidated chart PR #354; governance PR #352: **synthetic public outputs only**.
- Documentation closeout PR #358 is already merged; `docs/roadmap/release-1.14/RELEASE_1.14_MILESTONE_65_CLOSEOUT.md` records owner-certified production publication and notes that the original local evidence was uncommitted at that time. **Do not modify that historical statement**: it was accurate when written.
- The owner reports successful production publication of exact staging-tested OCI digest `sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07` and successful six-indicator/twelve-selection/health/API browser checks, with staging unchanged. Treat these as **owner-reported**, not independent certification beyond the original evidence.
- Issues #323, #324, #325 and all other milestone #65 issues have already been closed. Do not reopen, rewrite or relabel them. Check current milestone state and open issue count afresh.
- `PUBLIC_RUNTIME_VERIFIED=False`, raw public data APIs disabled, advanced WP07 ON HOLD: do not change or imply legacy WP07/F1 security certification. No real historical or live market data.

## Execution gates

1. In the owner's local checkout, record `git status --porcelain=v1`, current branch, remote and current `origin/main` SHA. Preserve all untracked files, especially owner documents. Never clean, reset, stash or overwrite unrelated work. If a clean isolated worktree/clone is needed, use one without deleting the original source evidence.
2. Verify the source file exists, is untracked or otherwise eligible for an additive commit, and is appropriate for public release. Compute SHA-256 and inspect content for secrets and inaccurate claims. Do not alter the original.
3. Fetch latest `origin/main`; create a new short-lived branch from that exact commit. Add **only** `docs/roadmap/release-1.14/prompters/release-1.14-c2b-37-production-publication-evidence.md` with unchanged content. If that path is already present on main, compare bytes; if identical, **do not create an empty PR**—report and proceed to milestone closure only after verifying it was previously published. If different, stop for owner reconciliation.
4. Run `git diff --check`, inspect `git diff --name-status origin/main...HEAD` and verify the changed-path allowlist contains **exactly one file**, the evidence path. Verify committed evidence hash matches original. Explicitly check **zero README changes**, zero code/config/test/infra changes, zero deployment changes.
5. Open a documentation-only PR targeting `main`, with a clear title such as `docs(release-1.14): publish original production deployment evidence`. Explain the provenance, original file hash, PR #358 closeout linkage, and no README/code/deployment changes. Review PR file inventory and mergeability. If required repository checks exist, wait for them to pass; do not bypass branch protection or required checks. Merge using the repository's established method (prefer squash) only if all gates pass; otherwise stop with the PR open and report the blocker.
6. Verify merged PR and `origin/main` contains the identical original evidence content. Do not modify any other file or PR.
7. Inspect GitHub milestone **#65** via GitHub REST or `gh api`: `GET /repos/{repository}/milestones/65`; verify `open_issues == 0` and all expected issues closed. If milestone is still open, close the **milestone object itself** using `PATCH /repos/{repository}/milestones/65` with `{"state":"closed"}`. If already closed, record that fact; do not toggle. **Do not close a milestone with outstanding open issues**; stop and report unexpected issues for owner review. Do not mutate milestone title, description, due date or issue state.
8. Verify with a fresh milestone GET: `state == closed`, `open_issues == 0`; confirm merged PR, final main SHA, original evidence file hash, and that no README/code/deployment changes occurred. Restore/leave owner's original local checkout and untracked materials intact. No Azure calls or deployments.

## Required final report

Report: PR URL/number and merged commit SHA (or exact blocker); original and published evidence SHA-256; the exact changed-file inventory; explicit README untouched confirmation; milestone #65 URL/state/open/closed issue counts; latest main SHA; no runtime/Azure changes; any limitations. Do not claim success for an unverified operation.

**Stop condition:** Missing or unsuitable original evidence, hash mismatch, dirty/ambiguous overlap, unexpected changed paths, failing checks, insufficient GitHub permission, or milestone with open issues => stop safely and request owner decision rather than expanding authority.
