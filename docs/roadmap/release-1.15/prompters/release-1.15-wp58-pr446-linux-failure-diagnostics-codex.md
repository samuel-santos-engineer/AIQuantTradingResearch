# Release 1.15 — WP58 PR #446: bounded Linux failure diagnostics (Codex)

## Objective

Identify the **11 failing Linux test cases** and their sanitized failure categories from the existing direct-VSTest path. This is a diagnostic-only continuation; **DO NOT MERGE PR #446**. Do not fix code, alter test assertions, or infer a root cause before obtaining evidence.

## Verified baseline

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- PR: https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/446 ; expected head `be28a28a89b3ba25f6a3e59ce0446db656eb163b` (recheck before work).
- Current merged `main` at review: `ca874bb7afb4a79011f621cbf5ca314af0f8018a`; PR was 0 behind, open/unmerged, GitHub mergeable=false.
- Historical fresh Windows fake-only suite: 133 passed. Latest direct VSTest Linux: **25 executed, 14 passed, 11 failed, 0 skipped**, exit 1. Test names/assertions unknown because capture retained only counts.
- Earlier `dotnet vstest` CLI startup raised `System.IO.IOException` with named-mutex/NuGet migration markers. Direct VSTest DLL bypassed startup and reached tests; do not equate the two failure categories.
- Latest Linux runner used cached pinned `mcr.microsoft.com/dotnet/sdk:10.0@sha256:3a48280a9a7c6276c3eeaba1efa6a08820f18c84fb9e6274c187719ecfaf9fbc`, Linux/amd64, `--pull=never`, `--network=none`, read-only root, UID/GID 1654, only isolated fake-test output mounted read-only at `/qualification`, disposable `/tmp` tmpfs, disposable HOME/DOTNET_CLI_HOME/results. Its owned container cleanup passed.
- Keep `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false`.

## Required procedure

1. **Preflight, no mutations:** Fetch and verify PR head/status and latest `main`, ensure isolated clean worktree and no concurrent conflicting operator changes. Review the existing Phase F runbook and prior direct-VSTest evidence. If head changed or permissions/ownership cannot be established, stop and report. Do not overwrite other work.
2. **Prepare the sanitizer offline:** Design a bounded in-memory capture that records each failing test's *fully qualified test name* (or uniquely identifying sanitized name), failure category (assertion, exception, native-load, permissions, missing file, environment, other), and short sanitized assertion/exception-type details. Strip paths, usernames, machine identifiers, environment values, tokens, keys, dataset content, stack paths and raw output. Keep enough expected-vs-actual assertion semantics to diagnose failures, using redaction rather than suppressing the entire message. Never persist raw VSTest output or TRX containing unredacted data. Validate the sanitizer with synthetic examples for assertions, stack traces, paths and sensitive strings before running Docker.
3. **One bounded Linux invocation only:** After verifying the exact cached SDK digest and Linux/amd64 identity, invoke the already established **direct VSTest DLL**, not `dotnet vstest`, for `FullyQualifiedName~ResearchStateInitializationTests`. Reuse the previous safe image, UID, network, read-only, tmpfs, disposable home/results and fake-only mount restrictions. Confirm the exact owned container name/label and mount before starting. Allow at most **one** run, **90 seconds**, no retries, pulls, builds, restores, package installation, extra mounts or fallback commands. Use an appropriately detailed console logger captured in memory and sanitized before display; do not disclose raw logs.
4. **Cleanup:** Inspect only the known owned container and verify its label before removing it; confirm removal. Never delete unknown/shared resources. Report cleanup as VERIFIED, UNVERIFIED or FAILED, without guessing.
5. **Analyze:** Report executed/passed/failed/skipped counts, exit status, **each failing test name**, sanitized failure summary, and whether multiple failures share a plausible cause. Distinguish observed evidence from hypotheses. If failures are caused by a harness/fixture issue, identify the smallest proposed correction but **do not implement it**. If a test run cannot complete or the sanitizer is inadequate, stop and report that limitation; do not rerun.
6. **Documentation only:** Append a dated, clearly delimited diagnostic section to `docs/roadmap/release-1.15/prompters/release-1.15-wp58-phase-f-staging-initialization-runbook.md`. Preserve all previous evidence and merged WP59–WP61 content. Validate UTF-8/LF/no BOM and `git diff --check`. If safe, commit and push **only** the authorized runbook update to the existing PR #446 branch (normal push, no force push). Otherwise report why publication was blocked.
7. **Final handoff:** Give PR URL/head/status, exact test counts, concise table of all failures with sanitized details, root-cause confidence, cleanup verification, changed files, and a narrowly scoped recommendation for the next step. State explicitly that Linux qualification remains **FAILED** until a separate successful run and that PR #446 is **OPEN/UNMERGED — DO NOT MERGE**.

## Strict boundaries

No application/source/test-code changes, no initializer execution against authentic data, no retained SQLite/observations/volume/container/key/TLS/rollback-image access or mutation, no Azure/public deployment, provider requests, registry actions, storage migration, dependency changes, credential rotation or GitHub PR merge. No broad Linux test reruns. The isolated test-output mount must contain fake fixtures only. If any required safeguard fails, stop and request guidance rather than expanding scope.
