# C2B-17 — Execute merged E4 local verification (Codex authority)

Date: 2026-10-16
Project: `samuel-santos-engineer/AIQuantTradingResearch` — Release 1.14 showcase lab, **not a production trading platform**.

## Owner intent and baseline

The owner reports PR #344 merged at `3a4e6ef0e8a0ddf77776801cf87907927d8abc31`. Verify the remote merge commit and that the working branch is based on the expected main before doing anything. PR #344's two test-only files are the execution target:

- `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`

The files were previously unexecuted. **Do not report E4 parity or passing tests until observed.** `PUBLIC_RUNTIME_VERIFIED=False`; public dataset/candles/indicators remain unavailable; Azure deployment/public activation NO-GO; advanced WP07 ON HOLD.

## Authorized work — one supervised, finite local verification

1. Inspect `git status`, current HEAD, and remote `main`. Preserve all existing tracked changes, untracked owner files, private material and protected WP07 worktree/archive. Do not reset, clean, stash, overwrite or delete unrelated work. If baseline is ambiguous, STOP.
2. Inspect the two merged test files and existing test-project configuration. Resolve actual installed .NET SDK and CPython executable paths and versions. Confirm Python environment variable `AIQ_E4_REFERENCE_PYTHON` is set to an **existing absolute interpreter path**, without PATH fallback. Report toolchain mismatch rather than installing or changing tools.
3. Confirm local offline build dependencies are already available. Do not restore packages, run pip, download dependencies, call providers or make external network requests. Use existing project facilities; no new harness, Job Object, native helper, watchdog or kernel resource controls.
4. Establish an explicit, safe local build/test output location or existing reviewed project output pattern. Ensure no collision with protected/untracked files; log only sanitized command/status/count/timing evidence. Avoid raw candle rows, expected vectors, base64 payloads, secrets or private inputs in logs. Do not run if output ownership or available offline dependencies cannot be established.
5. Perform **one bounded build**, then the focused E4 test run only, with no restore and no unrelated suite expansion. Prefer an exact test filter for `SyntheticSnapshotQualificationTests`. Record exact commands, executable paths, versions, exit codes, elapsed time and test counts. Do not silently fall back to all tests or another Python executable.
6. Apply the simple safeguards already proposed in C2B-13 revision: supervised outer commands, approximately 120 seconds for build and 120 seconds for focused test batch (300 seconds total including cleanup), at most two sequential oracle invocations with 30-second per-child deadline, byte-bounded stdout/stderr and finite cancellation/cleanup. These are **best-effort local safeguards**, not hard OS guarantees. If the existing test implementation enforces its own bounds, use those; do not build additional infrastructure to reproduce them. If the workload exceeds a proposed bound, STOP and report actual measurements before changing it.
7. If compilation or assertions fail, collect **sanitized, specific** diagnostics: failing test, compiler code/location, mismatch category, exception type, and whether oracle subprocess exited cleanly. Do not alter frozen generator constants, fingerprint expectations, canonical formats, indicator semantics or test expectations to manufacture a pass. Do not automatically retry, expand resource caps, or change production code.
8. Inspect only files and processes created by this run for cleanup. If a subprocess may still be alive, STOP further execution and request manual inspection. Never use process-name-wide termination or unchecked recursive cleanup. Preserve useful bounded failure diagnostics for owner review.

## Changes and PR policy

This authority is **execution and reporting first**. Do not modify source or open a PR automatically. If a clear test-only defect is discovered, explain the smallest proposed correction limited to the two E4 test files and request owner authorization before editing. Production files, packages, CI, schema, runtime, Azure configuration, generator, exporter, snapshots and governance documents are out of scope.

## Report to owner

Return a concise factual report containing:

- Verified merge baseline, worktree preservation and exact toolchain versions/paths.
- Exact offline build and focused test commands; test count, PASS/FAIL/BLOCKED, exit codes and elapsed times.
- Whether the complete three-dataset, twelve-window, candle-byte and indicator comparisons actually executed twice, and whether the Python oracle independently completed.
- Any numerical mismatch or build defect with sanitized locations/categories; explicitly distinguish untested supplemental gaps from failures.
- Observed output/resource/cleanup status and any uncertainty.
- The **single smallest next action** to advance the showcase.

If the test run passes, report **local E4 proof passed subject to independent oracle/spec review**, not production readiness or public activation. If it fails or is blocked, stop and report; do not invent success. No merge or deployment authority is granted.

## Scope principle

**Ship the showcase, not a production platform.** Prefer straightforward compilation, tests and diagnosis. Do not introduce extra planning phases, enterprise containment, hard CPU/RSS qualification, native helpers or unrelated negative-test matrices. Preserve only essential correctness, data licensing, privacy separation and basic security boundaries.
