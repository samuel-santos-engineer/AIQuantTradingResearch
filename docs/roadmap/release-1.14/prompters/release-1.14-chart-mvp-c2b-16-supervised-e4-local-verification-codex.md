# C2B-16 — Supervised E4 local verification of PR #344

**Owner authority:** Perform a narrow, supervised local compile/test verification of the two test-only files in open PR #344. This is a showcase lab, not a production platform. Prefer the shortest safe path to working tests; do not introduce Job Objects, native containment, runners, CI, infrastructure, or additional governance layers.

## Fixed starting point and guardrails

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- PR #343 merged as `1b0e4da65537187f9f0b5f80d27c4948afd87551`.
- PR #344 expected **OPEN, UNMERGED**, head `0ddf8b9ba4c340755718d6582ff4fed454b6207a`, base `1b0e4da65537187f9f0b5f80d27c4948afd87551`. Recheck remotely before work; STOP if different or unexpectedly merged.
- Only in-scope source files:
  - `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs`
  - `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`
- Preserve all other tracked and untracked files, owner documents, protected WP07 worktree/archive, and private/provider data. Do not inspect or alter protected materials.
- `PUBLIC_RUNTIME_VERIFIED=False`; public activation/deployment **NO-GO**; advanced WP07 **ON HOLD**.

## Authorized verification workflow

1. Check working-tree status, branch, PR head/base, exact two-file diff, existing test project settings, and available local `.NET` SDK and CPython interpreter. Record **actual** absolute executable paths and versions; do not assume previously proposed paths. If tools, offline dependencies, or permissions are unavailable, STOP and report rather than install or fetch.
2. Choose a minimal reviewed local environment and safe owned build/test/temp output locations. Inventory existing contents before writing. Do not touch unrelated artifacts. Avoid restore, pip, package download, external network calls, private datasets or secrets. If the required build cannot proceed offline, STOP.
3. Under developer supervision, perform the minimal **offline compile/build** needed for the test project, then run only the E4 qualification tests with an explicit test filter, using `--no-restore` / `--no-build` where applicable. Do not run the entire suite or any provider/Azure/browser probes. The existing tests may invoke the approved test-only Python oracle and synthetic generator/calculator in memory solely for qualification.
4. Use ordinary finite safeguards: provisional outer build 120 seconds, test batch 120 seconds, session 300 seconds; sequential oracle children, each at most 30 seconds; 16 MiB stdout and 16 KiB stderr per oracle; supervised resource observation; best-effort process-tree kill with finite wait. These are pragmatic limits, **not** hard OS isolation. STOP on timeout, unexpected network/write, cap overrun, uncertain termination, or resource pressure. Do not automatically retry or silently increase caps.
5. Report compilation diagnostics, test discovery and pass/fail counts, elapsed times, oracle path/version, bounded output observations, exact mismatch categories (without raw rows/vectors), and any unmet coverage. Distinguish tests actually run from historical baselines and from claims in the PR description. Do not claim independent oracle acceptance without separate reviewer evidence.
6. If an ordinary compile/test defect is found, make **minimal corrections only within the two listed files**, preserving frozen specification, fingerprints, complete dataset/window/indicator comparisons and the independent reference. No changes to production code, packages, project files, scripts, fixture data, snapshots, or expected constants. Do not weaken assertions, suppress failures, skip cases, or copy production output into the oracle. Rerun the same narrow verification once after a correction if the environment is clean and limits permit; otherwise STOP and report.
7. Push any two-file fixes to the existing PR #344 branch only after confirming the branch still matches the expected remote head (or the new head created by this authority). Keep PR **OPEN and UNMERGED**. Provide new head SHA, exact diff, commands, results, limitations, and preservation checks. If no fix is needed, do not create a commit merely for evidence.

## Explicit exclusions

No merge of #344, no public snapshot export/materialization, no production/runtime integration, no provider/Twelve Data data, no Azure deployment, no WP07 work, no new runner/native helper/Job Object, no general CI change, no broad refactor, no package installation, no automatic retries, and no claim that local E4 parity establishes public readiness. Do not add targeted subprocess-fault-injection or exhaustive negative matrices solely to close noncritical disclosed gaps in this phase.

## Exit and owner decision

Finish with a concise factual report: **PASS**, **FAIL**, or **BLOCKED**; exact PR head and two-file scope; local toolchain; compile/test counts; resource observations; any corrections and remaining gaps. If blocked, explain the smallest next action. Request owner acceptance before merging PR #344 or advancing to public snapshot/runtime work.
