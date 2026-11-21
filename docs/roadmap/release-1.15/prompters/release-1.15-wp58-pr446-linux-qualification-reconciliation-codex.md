# Release 1.15 — WP58 PR #446 Linux qualification and reconciliation

## Mission

Investigate and resolve the outstanding local Linux qualification failure for [PR #446](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/446), reconcile its changes with the latest `main`, and leave the PR open for owner review. **Do not merge.** Keep WP59/WP60/WP61 and their accepted evidence intact.

## Authoritative GitHub checkpoint

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Latest merged `main` checkpoint: `ca874bb7afb4a79011f621cbf5ca314af0f8018a` (PR #451 documentation-only squash merge). Fetch and verify the live remote before work; stop if unexpected changes affect the plan.
- PR #450 merged: `cca6a81429fcbc6906ee5e77fe5dd0a5a19d8b4b`.
- PR #451 merged: `ca874bb7afb4a79011f621cbf5ca314af0f8018a`.
- PR #446 last observed OPEN, head `187e6584571ed4d978511f0abfc7caa3021ea481`, based on old `main` (`4a6bc2b901d6f6ff0081dad4597064017acb86f9`) and reported not mergeable. Recheck all live refs/status before edits.
- #446 implements `--research-initialize-chart`: preflight both BTC/USD and ETH/USD, require 1,000 contiguous OHLC-only candles per symbol, verify SMA20 19-null/981-calculated, then permit API serving; preserve existing initializer/default serve behavior and documented non-pair-atomic semantics.
- Existing Windows fake-only focused test result: 133 passed, historical evidence only.
- Existing Linux SDK image identity verification passed, but two bounded Linux qualification attempts exited 1 without a usable test summary. Latest failure reported `System.IO.IOException` (eight output lines). **Cause and assertion result remain UNKNOWN.** Do not call this an application test failure or a passing qualification without evidence.

## Scope and procedure

1. **Read-only inventory first.** Inspect PR #446 diff, comments, evidence/runbook, changed paths, base/head and merge conflicts; compare against latest `main`. Review the exact Linux test command, SDK image digest, mounts, workdir, output directory, `--rm` behavior, filesystem permissions, stdout/stderr and container cleanup evidence. Preserve the operator's current checkout and untracked/private files by using an isolated clean worktree or equivalent.
2. **Diagnose before retrying.** Reconstruct the failure mechanism from available logs and offline inspection. Specifically distinguish Docker/container setup, read-only filesystem and test-result paths, .NET first-run<owner-home>/NuGet caches, VSTest output, permissions/UID, and genuine test assertions. Do not assume any one cause. If an approved, bounded fake-only Linux run is needed, state exact preconditions, resource identities, command, writable temporary paths, expected output, cleanup, and stop conditions before execution. No open-ended retries.
3. **Bounded local qualification.** Only when preconditions and existing local authorization permit: use the already verified pinned Linux SDK image, `--network=none`, no pull/build, no private mounts, no retained observations/SQLite/credentials/TLS, and disposable fake-only tests. Provide a writable isolated test-result/temp/home area if required, with minimum permissions and explicit cleanup verification. Capture bounded logs and an actual test-count summary. Stop on unexpected failure. If authority is insufficient for a new Docker run, do not run it: complete the offline diagnosis, propose the exact one-run plan, and mark Linux qualification BLOCKED/NOT_RUN.
4. **Reconcile against current `main`.** Preserve merged Release 1.15 evidence, especially PRs #445, #447, #449, #450 and #451. Resolve genuine conflicts narrowly. Keep the initializer feature isolated; do not overwrite WP59/WP60/WP61 chart code or documentation. Prefer normal merge of `origin/main` into the PR branch, not rebase/force-push, after verifying current head and branch ownership. If unsafe, stop and explain.
5. **Regression tests.** Run available focused Windows/local .NET initializer tests using existing cached dependencies and no network, plus Linux fake-only qualification only if safely authorized. Verify original CLI behavior, dual-symbol preflight, compatible partial resume, incompatible ETH rejection before BTC commit, complete pair verification, bootstrap exit/serve gate, and no implicit pair-atomicity claim. Do not fabricate counts or infer Linux pass from Windows results.
6. **Evidence and handoff.** Update only the PR's existing relevant evidence/runbook and necessary narrow code/tests. Record exact commands, exit codes, test counts, verified hashes, observed IOException diagnosis, Docker cleanup results, main reconciliation and limitations. Keep UTF-8/LF/no BOM, run `git diff --check`, verify changed paths and GitHub mergeability/checks. Publish changes to the existing PR #446 by a normal push if safe; leave OPEN/UNMERGED for owner review.

## Non-negotiable boundaries

- **No Azure/F1 quota, production, registry, provider acquisition, upload/import, network pull, dependency upgrade, or public deployment operations.** No changes to retained local BTC/ETH imported data, named volume, API/UI runtime, existing key/TLS, or rollback images.
- No private data, keys, raw observations, paths, screenshots or credentials in GitHub evidence or logs.
- Do not modify or reopen merged PRs. Do not create new governance/licensing gates or broaden architecture.
- Do not merge #446 automatically, even if tests pass. If qualification fails, preserve truthful `NOT_PASSED` and provide the smallest actionable next step.
- Maintain `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false`.

## Acceptance / required final report

Report (a) live PR/head/base/main status, (b) exact root cause if proven or UNKNOWN with supporting evidence, (c) which tests actually ran and their exit codes/counts, (d) whether Linux initializer qualification PASSED, FAILED or NOT_RUN/BLOCKED, (e) whether reconciliation was completed and PR is mergeable, (f) exact changed files/commits, (g) preserved runtime/secret boundaries, and (h) a clear **MERGE RECOMMENDED** or **DO NOT MERGE** decision with outstanding risks. No unsupported claim of public Azure readiness.
