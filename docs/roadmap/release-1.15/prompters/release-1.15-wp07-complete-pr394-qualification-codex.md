# Codex prompt — Release 1.15 WP07: complete PR #394 qualification

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

## Authority and starting point

Continue **existing draft PR #394**, head `db34c2395017af11003b2e48a0da2c22a9b8afad`, based on merged PR #393 (`3a068341509c78af548b734ace1de737cc8dc613`). First fetch and verify current remote head/base; if changed, inspect before proceeding. Do not create a competing PR. Keep PR #394 **draft, open, unmerged** for owner review. Do not merge or close issues. Use an isolated worktree; preserve the original dirty checkout, all owner files and prompts byte-for-byte. No reset/stash/clean of owner work.

This is a focused **local offline WP07 qualification continuation**, not a new planning exercise. The one-line Plotly `uirevision` fix already in #394 has real-browser evidence: Return to Live restores both x/y axes and Refresh retains zoom. Preserve that behavior and regression coverage. The reported focused Python tests are 52/52; Application 43/43 and Infrastructure 83/83. The unrelated pre-existing legacy synthetic hover assertion is reproduced on unchanged baseline and is not permission to alter its test or legacy UI.

## Deliverable

Extend #394 with reproducible evidence and, only if tests expose genuine defects, minimal tightly scoped fixes. Prioritize the missing browser cases before Linux. Update the existing append-only foundation evidence document with exact commands, environment, results, source hashes, remaining gaps and preservation checks. Do not claim acceptance for unrun scenarios.

### A. Complete actual-browser acceptance

Use the existing real Chrome/CDP -> actual Streamlit UI -> server-side HTTPS -> C# WP05 endpoint -> disposable committed WP03/WP04/D5 fixture harness already in #394. Extend the fixture/coordinator only as necessary to exercise, observe and assert:

1. Explicit pointer **pan** as distinct from zoom; x/y persistence across Refresh and Return to Live.
2. Gap/continuity rendering, null overlays and transition markers; no fabricated candles or accidental line bridges.
3. Real committed-state **correction**, generation, UTC-day, and transform-epoch changes: correct full/unchanged behavior, rebuilt public view, no stale transformed values, and viewport retention/reset according to governance.
4. Disconnect, malformed/unauthorized response, recovery/reconnect, and last-good UI/status behavior without secret leakage.
5. Two actual independent browser sessions and BTC/ETH switching: viewport/preset/panel state isolation; stale component events and remount/disposal safety.
6. Narrow viewport, Plotly hover, modebar, repeated rerenders and bounded resource stability; 60-second refresh if testable within reasonable time (do not replace with invented timing proof).

Record real observed DOM/Plotly state, event sequence, screenshots/hashes where useful, and clear PASS/FAIL/NOT RUN. Prefer deterministic offline fixtures and bounded tests. No browser framework installation if existing Chrome/CDP works. Avoid brittle page-DOM hacks in product code; test harness may interact through browser protocol.

### B. Linux dual-container qualification

Inspect actual merged Docker/runtime governance and WP02 #368, WP03 #369, WP07 #373 acceptance obligations. First inventory local Docker daemon/platform, pinned base image digests and required publish/fixture inputs **read-only**. Do not pull or substitute images, install SDKs, acquire data, or deploy merely to make the test pass. If all prerequisites already exist and the permitted local offline runner can safely execute with disposable fixtures, run bounded Linux/amd64 two-image qualification: non-root single-process roles, no shared filesystem, HTTPS certificate/hostname/API-key boundary, volume permissions and SQLite restart/recovery, health, bounded response, memory/CPU/startup/latency evidence. The WP05 `snapshot-client` Dockerfile is a test driver, **not** the final Streamlit image; do not misrepresent it as the production UI container. If exact pinned bases or a real Streamlit image are missing, mark the corresponding cases BLOCKED with the precise missing prerequisite and provide a minimal separately reviewable implementation proposal rather than inventing a substitute or claiming PASS. No Azure operations.

### C. Scope, discipline and completion

- Stay within #394's five current paths wherever possible: `python/presentation/research_chart.py`, `python/presentation/test_research_chart.py`, `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchSnapshotEndpointTests.cs`, `tests/runtime/release_1_15/wp07_browser_qualification.py`, and append-only `docs/roadmap/release-1.15/RELEASE_1.15_WP03_WP04_FOUNDATION_PREFLIGHT.md`. If a truly necessary new path or dependency is identified, **stop and report the exact path, justification and smallest change proposal** before altering it.
- Preserve the existing WP03/04/05 contracts, private/public separation, D5 day transform, bounded snapshots, existing app default mode and Release 1.14 behavior. No fake provider observations, automatic D5 provisioning, secret disclosure or new runtime services.
- No cloud/Azure/staging/production operations, registry pushes, provider activation, live credentials, upgrades, image pulls, branch deletion, issue closure or merge.
- Keep original checkout byte-for-byte intact, including untracked prompt files. Verify UTF-8/no BOM/LF/final newline and `git diff --check` for changed text. No archives without authorization.
- Re-run focused suites and the explicit opt-in browser harness; distinguish baseline failures, genuine new failures and blocked tests. Do not silently skip tests or report an opt-in browser test as passed when it was not enabled.
- Push updates to **PR #394** and retain **draft/open/unmerged**. In the final report provide head SHA, exact changed files, passing/failing/blocked matrix, test commands, source hashes, owner-checkout preservation, and specific next prerequisites. Keep `PUBLIC_RUNTIME_VERIFIED=False` until actual staging qualification is complete.

Primary goal: finish as much **real browser qualification** as possible now, without turning missing Linux prerequisites into an unrelated redesign or a release gate for completed application work.
