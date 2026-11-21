# Codex implementation prompt — Release 1.15 WP53: Fresh local preparation qualification

## Authority and baseline

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
Start from clean `main` containing merged PR #439, squash commit `f7481558d8a2da69463a56a2392deef120e76f46`. Create a narrowly scoped WP53 branch and leave its pull request **OPEN/UNMERGED** for owner review. Preserve unrelated working-tree changes, private data and existing Docker resources. Do not merge or deploy.

Release 1.15 remains **Twelve Data only**, authentic BTC/USD and ETH/USD 1-minute OHLCV with base-asset volume. Keep strict volume validation, six WP50 subtypes, version-1 ten-field diagnostic envelope, UTC/ASC/window/grace/count constraints, BTC-first/ETH-second sequence, no retries, atomic two-symbol publication, and authentic-only provenance. No provider migration, synthetic fallback, licensing prerequisite or governance gate.

## Goal

Qualify the actual **fresh local preparation** and WP52 identity guard on the intended Docker/Linux environment, without invoking Twelve Data or reading provider credentials. This is operational qualification of the preparation path, not a live acquisition authorization. Favor execution and concise evidence over another documentation-only milestone.

## Allowed work

1. Inspect the existing `eng/docker/release-1.15/wp39_local_history.py` Prepare/Acquire path, WP52 identity code/tests and documented resource ownership before touching resources.
2. Run existing offline tests first. On a Linux environment, execute the real symlink/escape rejection test that was skipped on Windows, plus relevant identity and regression tests. If Linux is unavailable, report **NOT QUALIFIED** for that item; do not claim a pass or bypass it.
3. Subject to an explicit, separate owner approval for Docker/resource operations, perform a **fresh Prepare** in a new owned temporary directory using only already available local images/dependencies. Do not reuse, mutate or delete older preparations, Docker volumes, containers, credentials or owner assets. Do not pull images or packages silently. Record only nonsecret source revision, published Worker identity, workflow hash, manifest structure and relevant command exit statuses.
4. Verify the complete published Worker tree and copied `context/research` tree match the manifest; test the real guard against controlled changes in **disposable copies** (missing manifest, modified DLL/dependency/native file, modified workflow, symlink escape). Ensure a rejected preparation stops before credential presence/value access and before child/provider execution. Never insert a real key, inspect its presence or execute actual Acquire.
5. If Prepare is unavailable or fails due to prerequisites, stop safely, preserve evidence and identify the minimal actionable blocker. Do not fabricate successful Docker qualification or expand into unrelated refactoring.
6. Add focused automated regression tests or minimal fixes **only where a demonstrated qualification failure requires them**. Keep the existing Python/Worker compatibility and error handling intact. Update a concise WP53 evidence/runbook entry and append the execution plan with actual results, limitations and exact commands.

## Strict prohibitions

No Twelve Data requests (including DNS/HEAD/capability/account/credit queries), no provider credential or presence inspection, no raw/private historical payload inspection, no real Acquire/Import/Start/Verify/browser qualification, no Azure/deployment/registry actions. **This prompt itself does not authorize Docker execution**: obtain separate explicit owner authorization before invoking Docker, running Prepare, creating resources or changing local infrastructure. If authorization is unavailable, complete safe offline checks and leave Docker qualification pending. Do not create a fake approval gate for provider licensing.

## Acceptance and evidence

- Fresh identity generation and validation are demonstrated against real, matching prepared artifacts **only if** Docker operations are separately authorized and successfully executed.
- Real Linux symlink test passes when Linux is available; otherwise it is reported as unqualified.
- Invalid preparations are rejected with sanitized parent-only `PREPARATION_IDENTITY_INVALID` at preflight, zero requests/responses and no bundle publication; no credential access on initial rejection.
- No existing private resources are changed. All tests and commands have exact pass/fail/skip totals and logs scrubbed of secrets and local private paths.
- Maintain truthful flags: `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false` until actual later evidence establishes otherwise.
- PR contains only necessary implementation/tests and concise WP53 evidence. Leave it open and unmerged, with explicit blockers and any required next owner authorization.

## Finish

Return the PR URL/head SHA, changed paths, exact test results, whether Docker preparation was **AUTHORIZED / EXECUTED / QUALIFIED** (three separate facts), Linux symlink outcome, and next concrete step. Do not request or infer permission for a live provider attempt from PR approval.
