# Codex task — Release 1.15 WP11: amend PR #405 for attributable F1 diagnostic evidence

## Mission
Amend **existing OPEN PR #405** (`task/release-1.15-wp11-diagnostic`) in `samuel-santos-engineer/AIQuantTradingResearch`; do **not** open another PR unless technically unavoidable. Address the review concern that the one authorized Azure F1 research diagnostic yielded no attributable UID/mount/storage-guard output. Deliver a bounded, independently attributable evidence-capture mechanism and offline failure-path tests **without conducting another Azure diagnostic**.

## Verified baseline and facts
- `main` includes PR #404 squash `c300087aa250d86de24a64ed4bfe0805d3716c9d`.
- PR #405 head at review: `b1a8ab1f4f023c6084b990c261ef6184f2de8cd7`; re-fetch latest before editing.
- Research staging app: `aiqr115researchstagingwus27f5eabb5`; UI: `aiqr112wp05wus27f5eabb5` (do not touch).
- One candidate was published; Linux/amd64 leaf digest `sha256:5b48f5d703acd7dca7e722ab9bc0028cbe8b80a3526c7a60800699785f6abed3`.
- Prior candidate start/stop control-plane window: 24.125 seconds. No attributable runtime output was recovered; compatibility remains UNVERIFIED.
- Automatic rollback initially failed at stop confirmation; manual recovery restored exact configurable baseline and original shell Running state, independently verified. Subsequent controller fixes are local-only.
- PR #405 adds `--research-storage-probe`, `container/release_1_15/research-bootstrap.sh` diagnostic mode, and `eng/azure-cli/r1.15-qualification/wp11_research_diagnostic.py` plus PowerShell wrapper and tests.
- `PUBLIC_RUNTIME_VERIFIED=False`. Provider requests and credits remain zero.

## Required implementation
1. Inspect PR #405 diff, the diagnostic controller, shell entrypoint, probe, tests, and existing runbook. Identify why Azure App Service logging may not capture a short-lived container's stdout/stderr and why existing collection did not prove candidate execution. Clearly distinguish established cause from hypothesis.
2. Implement the **smallest reliable attributable evidence path** suitable for the **existing** Azure F1 research app and its current allowed capabilities. It must bind observations to the exact candidate immutable digest and a unique per-attempt nonce/correlation ID, with explicit startup/entrypoint confirmation and fixed-format results for UID, selected mount facts and `ResearchStorageBoundary.ProbeStaging()` acceptance. Validate freshness and source; do not infer execution from successful Azure start, HTTP 200, old log markers or config alone.
3. Prefer a mechanism that works despite delayed or empty log streams. If evidence requires a platform facility or private channel that cannot be demonstrated without Azure mutation, implement a fail-closed preflight and document the precise missing prerequisite. **Do not claim an untested channel works**. No public endpoint containing diagnostics or secrets; no new infrastructure, registry, storage account, disk or VM.
4. Preserve the existing maximum 60-second candidate run bound for any future separately approved diagnostic. Preserve independent watchdog, immutable publication identity, exact baseline capture and restoration, retry-bounded stop confirmation, and fail-closed semantics. If evidence is absent, stale, nonce-mismatched, digest-unattributable, truncated or inconsistent, return `INCOMPLETE` rather than accepted/rejected.
5. Keep diagnostic read/write scope strictly to the existing guard's bounded empty-directory write probe, when fully opted in. Never open SQLite, start research API, import candles, use provider credentials, access UI, or emit private config/mount sources/credentials. Fixed allowlisted booleans/metadata only. No broadening of `ResearchStorageBoundary` production denial or the WP09 exact-site policy.
6. Add targeted tests for evidence presence, absent output, stale replay, mismatched nonce/digest, partial or malformed payload, early process exit, startup failure, logging delay, timeout, watchdog stop, stop-confirmation failure and rollback. Exercise real Linux behavior locally where feasible; use mocked Azure calls offline. Keep PS5.1 compatibility and UTF-8 LF/no BOM.
7. Append a concise evidence and operational section to the existing Release 1.15 runbook/evidence docs: explain why the first attempt is inconclusive, how the new mechanism is verified locally, the exact requirements for a later authorized retry, and remaining blockers. Keep documents append-only where appropriate.
8. Preserve the owner's local dirty/untracked files: inventory/hash them first, use an isolated worktree or equivalent, and verify unchanged afterward. Do not overwrite unrelated changes. Make focused commits to **PR #405's branch** and leave the PR OPEN/UNMERGED.

## Authority boundaries — strict
- **NO Azure mutations or second diagnostic run.** The prior owner approval was for a single run and is exhausted. No `az webapp config set`, stop/start/restart, logging change, deployment, slot operation or other live write.
- **NO image publication**, even of a corrected candidate. Existing candidate digest is historical evidence, not blanket permission to republish.
- **NO provider calls**, data import, credential discovery, new paid resource, UI changes, production changes, or PR merge.
- Read-only Azure inspection only if available and truly needed; avoid retrieving private settings or raw secrets. No reliance on local environment containing Azure credentials.
- A future retry requires the owner to explicitly authorize its exact image publication (if needed), target, maximum duration, rollback and evidence path.

## Acceptance / deliverables
- Updated PR #405 with focused implementation, tests and appended runbook/evidence; no separate planning-only PR.
- Local test commands and exact pass/fail counts; distinguish offline simulation from actual Azure proof.
- A clear evidence contract: source, attempt nonce, immutable image identity, timestamp/freshness, bounded payload schema, rejection of replay/mismatch, independent retrieval, failure behavior.
- Demonstrate rollback logic still executes if evidence is missing or collection fails; report any unresolved risk.
- Report `AZURE_DIAGNOSTIC_NOT_RUN`, `F1_COMPATIBILITY_UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED`, and `PUBLIC_RUNTIME_VERIFIED=False` unless genuinely superseded by independently authorized real evidence (not permitted by this task).
- Summarize exact changed paths, PR link, head SHA, checks and what approval would be required next.

## Priorities
Deliver a genuinely testable implementation rather than another planning-only artifact. If independent attributable capture is impossible with currently verified F1 capabilities, fail closed and explain the smallest concrete capability needed; do not invent Azure behavior or weaken rollback to obtain a positive result.
