# Release 1.14 WP07-P3D — Offline Native-Boundary Contract Corrections

## Authority and objective

Owner has authorized **WP07-P3D only**: repair the four P3C static-model acceptance findings and add focused regression tests. This is a **local, offline, two-file correction**, not native implementation, runtime integration, or acceptance of P3B. P3C is blocked pending independent re-review.

## Protected baseline

- Protected branch: `draft/release-1.14-wp07-partial-public-disabled`; expected HEAD `67893f51931ee183f71050a83b81127e6e648b87`.
- Preserve the intentionally dirty checkout, every pre-existing status entry, all refs, and all unrelated file contents. Do not checkout, reset, clean, stash, fetch, commit, push, or open PRs.
- P3C-reported starting hashes (verify before editing, STOP if mismatched unless owner resolves):
  - `container/admin_lifecycle_native_boundary_contracts.py`: `F43BE3359C0C8C79B5428F2061500D2C2E439D08D0DF2F5AEFE3DE745D83DA40`
  - `python/presentation/test_admin_lifecycle_native_boundary_contracts.py`: `5465744E17CC05A5662E411BA4EF3430E89D5811115CD334A813C00E7F08D477`
- P3B tests were reported 23/23 passing; P3C inspected without running and identified static acceptance failures.
- `PUBLIC_RUNTIME_VERIFIED=False`; production, public-runtime and Azure qualification remain **NO-GO**.

## Exact edit allowlist

1. `container/admin_lifecycle_native_boundary_contracts.py`
2. `python/presentation/test_admin_lifecycle_native_boundary_contracts.py`

**No third file.** Standard-library-only, immutable offline contracts and injected synthetic observations. Keep existing frozen `aiq-admin-lifecycle-v1` 64-byte wire protocol, accepted launch-plan topology and obligations, P2-C5 conditional agent-bootstrap FD5 slot, and disabled Option A unchanged. No runtime/native calls, process creation, credential processing, dependencies, image changes, .NET changes, or public exposure.

## Required corrections (all four)

### P3C-1 — Gate provenance and ACK must not disappear with inventory omission

Inspect the agent-bootstrap/agent-exec boundary and distinguish **mandatory protocol history** from the optional set of currently inherited handles. Make C1 gate provenance, guardian `A_EXEC` and agent-bootstrap `A_EXEC_ACK` receipt, and post-ACK/pre-exec explicit gate-and-alias closure mandatory prerequisites to an accepted agent-exec boundary. An input listing only RA3 and metadata4 **must not** bypass these obligations. Do not treat ACK as exec success or readiness. Preserve agent post-exec metadata validation/closure and the conditional nature of FD5. Add a reason-specific negative for omitted history/ACK even when no gate handle is listed; retain existing gate-leak negatives and a valid positive.

### P3C-2 — Completed-wait construction must be deadline-safe

A completed required-child wait state must be validated **at construction and on every state transition**, not only through `observe`. Enforce nonnegative, monotonic and in-budget observation times, with `last_now` within the supplied shared absolute deadline (and relevant start/bounds), and preserve sticky failure. Reject direct construction of a completed state whose completion is after the deadline; reject invalid backward/reset/overflow conditions. Preserve child-specific direct-parent, normal-exit-zero proof; `ECHILD`, zero-return, EOF, absence, pidfd readiness, signal death, nonzero exit, wrong PID and competing reapers are not positive proof.

### P3C-3 — Disposal/wait chronology must use timestamps

Represent and validate observed event times, not merely event labels or equal deadlines. Enforce the required shutdown prerequisites: router `HOST_DISPOSED` must precede supervisor's **positive router exit-zero wait**; required guardian/agent wait ordering must respect their role-specific shutdown obligations; mandatory waits must precede adopted-child drain/terminal absence. Keep the single shared four-second `CLOCK_MONOTONIC` shutdown deadline and Docker stop grace five seconds as external qualification obligations; do not simulate kernel proof. Fix the currently reported positive fixture (wait at time 11, `HOST_DISPOSED` at 12) so it is valid **only** when disposal precedes the corresponding wait. Add specific rejection for wait-before-disposal, equal/invalid timestamps where causal order requires strict precedence, and any deadline overrun. Do not invent protocol messages or loosen shutdown ordering.

### P3C-4 — Global required-child PID consistency

Validate the **complete required-child binding set**, not only each individual positive PID. Distinct mandatory child roles must not claim the same PID; parent/child ownership must match the approved topology (supervisor directly owns router and guardian; guardian directly owns agent). Reject duplicate PID across distinct required roles, wrong direct parent, inconsistent returned PID, or competing wait ownership. Add a reason-specific duplicate-role PID negative and a valid distinct-PID positive.

## Test and validation requirements

- Preserve the existing focused tests unless changing a fixture is necessary to correct an invalid accepted chronology; explain any changed expectation.
- Add reason-specific negative tests for each of the four findings and positive coverage for the corrected valid paths. Test constructor bypass explicitly, not solely `observe`.
- Do not weaken validations, change error classes solely to pass tests, or substitute declaration-only evidence for actual native proof.
- Run only the authorized offline command:

```powershell
python -B -m unittest python.presentation.test_admin_lifecycle_native_boundary_contracts -v
```

- Record test count, pass/fail and exit code. Avoid additional builds, imports outside the test invocation, full-suite runs, network access, native operations, Docker or Azure.

## Preservation and reporting

1. Before edits, record exact branch, HEAD, local refs, complete `git status --porcelain=v1 -uall`, both SHA-256 hashes and pre-existing tracked-file integrity baseline. Stop if expected baseline cannot be established.
2. Modify **only** the two allowlisted paths; preserve all other tracked/untracked content and existing Git state.
3. After validation, repeat branch/HEAD/refs/status and integrity checks. Distinguish the two authorized modifications from every pre-existing dirty entry. State coverage limitations for ignored files and Git metadata.
4. Report each P3C finding as FIXED / STILL BLOCKED with file:line evidence, concrete invariants, tests, command and exit code, final SHA-256 for both files, and residual limitations (symbolic scratch/FD5, native waits, SIGCHLD, image UID collisions, libc/ABI, sessions, actual clock/deadline feasibility).
5. Recommend **separate independent read-only review** after P3D; do not self-accept P3B/P3D or authorize toolchain/ABI selection.

## STOP conditions

STOP and request owner review if any correction needs a third path, a frozen-protocol or accepted-launch-plan change, an unsupported ordering assumption, native execution/syscalls, a dependency, ABI/toolchain selection, credential handling, Docker/Azure, Git mutation, or a change outside the exact allowlist. Do not conceal unresolved cases behind passing tests.

## Terminal verdict (choose exactly one)

- `WP07-P3D OFFLINE CONTRACT CORRECTIONS COMPLETE — OWNER REVIEW REQUIRED`
- `WP07-P3D BLOCKED — OWNER REVIEW REQUIRED`

**No public runtime or production activation is authorized.**
