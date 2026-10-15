# Codex Authority — Release 1.14 WP07-P3B

## Offline Native-Boundary Contracts and Negative Tests

**Owner authorization:** P3A investigation approved; this prompt authorizes **P3B only**, not native implementation, runtime packaging, or public activation.

## Mandatory starting conditions

1. Inspect and record full initial Git status, branch, HEAD, local refs, and relevant file hashes. The protected, intentionally dirty checkout must remain intact; do not checkout, reset, stash, clean, fetch, commit, push, create a PR, or merge.
2. Read the accepted static launch plan and tests, the frozen `aiq-admin-lifecycle-v1` protocol and tests, and the relevant existing runtime files **read-only** to establish compatibility. Do not promote legacy runtime behavior to the approved architecture.
3. Preserve `PUBLIC_RUNTIME_VERIFIED=False`, disabled Option A, and all production/Azure NO-GO gates.

## Exact change allowlist — two NEW files only

- `container/admin_lifecycle_native_boundary_contracts.py`
- `python/presentation/test_admin_lifecycle_native_boundary_contracts.py`

If either path already exists, **STOP** and report; do not overwrite. Do not modify any other path, dependencies, configuration, generated files, or Git metadata. Do not assign a native ABI, bootstrap language, compiler, real scratch FD numbers, or runtime authority.

## Frozen architectural invariants

- Secret-free finite root PID1 pre-fork; supervisor remains PID1 at UID/GID 1200/1200; guardian UID/GID 1300/1300; router UID/GID 1100/1100; agent/UI UID/GID 1000/1000, subject to future base-image collision qualification. Supervisor directly parents router and guardian; guardian directly parents agent; agent parents UI.
- Complete permanent identity drop (real/effective/saved IDs, empty supplementary groups and capabilities), distinct trusted/presentation sessions, no privileged managed startup, no secret-bearing parent memory at forks, and no cross-UID signaling dependency.
- Agent FD3 RA survives exec; agent FD4 metadata survives exec, validates once, then closes; agent-bootstrap FD5 is a **conditionally approved design slot only** for the temporary launch gate. Close FD5 and every gate alias explicitly after `A_EXEC_ACK` and before agent exec; CLOEXEC is an additional backstop. UI exec inherits no control/credential/metadata/gate FDs.
- Preserve frozen 64-byte big-endian protocol, launch/PID/deadline bindings, sequence continuity, one-time readiness and `R_EXPOSE` gate. One shared absolute `CLOCK_MONOTONIC` shutdown deadline of four seconds; Docker stop grace five seconds; privileged startup five seconds and total startup thirty seconds.
- D1 guardian requires its own positive, child-specific bounded kernel wait for agent exit zero; D2 supervisor independently requires positive child-specific bounded wait for guardian exit zero, and supervisor also independently waits router exit zero after host disposal. Adopted-child drain and terminal absence occur only after mandatory waits and never substitute for them. D3 teardown is failure containment only.

## Implementation scope — offline immutable contracts only

Implement standard-library-only, immutable, declarative contract types and pure validators driven by **injected synthetic observations**, with explicit, stable rejection classifications. Do not call `fork`, `exec`, `waitpid`, `pidfd`, `prctl`, `setuid`, `setgid`, `setsid`, `clock_gettime`, `kill`, `dup2`, `fcntl`, `os.pipe`, socket APIs, native libraries, subprocesses, or any equivalent native operation. Do not read secrets or access live processes.

Model and validate at least:

1. **Endpoint provenance/remapping:** endpoint creation and ownership, full inherited-copy inventory, collision-safe source/destination/scratch plans, live-destination rejection, duplication cycles, alias tracking, close-unused and pre-exec closure, CLOEXEC versus intentional exec survival, and no UI control-FD inheritance. Treat scratch FD numbers as unresolved symbols; never claim real vacancy.
2. **Fork/exec identity:** exact role/parent graph, positive child PID binding from injected fork outcomes, one exclusive status owner per required child, finite secret-free pre-fork stage, and sanitization/closure obligations before exec.
3. **Hardening and sessions:** real/effective/saved UID/GID equality at final target, empty supplementary groups/capabilities, explicit failure on partial drop, and trusted/presentation session isolation (including same-session signaling hazards).
4. **Absolute deadlines:** checked nonnegative monotonic nanoseconds, overflow-safe arithmetic, one propagated shutdown deadline without reset, bounded EINTR retries, and startup budget constraints.
5. **Child-specific wait evidence:** injected nonblocking outcomes distinguishing still-running, EINTR, normal exit zero, nonzero exit, signaled exit, ECHILD, wrong returned PID, and interop errors; sticky failure on anything other than valid positive child-specific exit-zero proof. EOF, notifier, pidfd readiness, disappearance, or ECHILD cannot prove exit zero.
6. **PID1 shutdown/drain and exposure:** mandatory wait/disposal ordering before adopted-child drain/terminal absence, no premature public listener or `R_EXPOSE`, and no recovery from a sticky failure through later synthetic success.

Keep this module separate from the existing launch-plan and protocol implementations. Do not duplicate or change frozen wire framing. The contracts describe requirements; they are not proof that Linux/kernel/.NET interop can meet them.

## Tests and validation

Write focused positive and adversarial tests in the single new test file. Explicitly reject occupied destinations, unsafe remapping/aliases, inherited FD5 conflicts, gate or UI leaks, wrong parent/returned PID, competing reaper/status owner, ECHILD-as-success, signaled/nonzero exit, false success from EOF/absence, EINTR deadline resets, deadline overflow, incomplete hardening, shared trusted/presentation sessions, premature exposure, and drain-before-waits. Assert specific rejection reasons where possible; avoid tests passing only through generic canonical mismatch.

Run only:

```powershell
python -B -m unittest python.presentation.test_admin_lifecycle_native_boundary_contracts -v
```

If validation fails, stop and report the exact failures. Do not broaden scope or alter the accepted P2 files to make tests pass.

## Stop conditions

Immediately STOP and request owner review if the work needs a third path, dependency, native execution, ABI/toolchain selection, real descriptor allocation, frozen protocol/FD/topology revision, identity provisioning, Docker/Azure operation, or a modification to the protected checkout outside the two-file allowlist.

## Final evidence and disposition

Report: exact created paths; contract invariants and explicit limitations; test names/counts/exit code; before/after hashes for the new files where applicable; unchanged hashes of pre-existing Git-listed files; initial/final full Git status, branch, HEAD and refs; any unverified ignored-file scope; and all unresolved native feasibility risks. No commit/PR/merge.

End with exactly one:

- **`WP07-P3B OFFLINE NATIVE-BOUNDARY CONTRACT COMPLETE — OWNER REVIEW REQUIRED`**
- **`WP07-P3B BLOCKED — OWNER REVIEW REQUIRED`**

`PUBLIC_RUNTIME_VERIFIED=False`; production, public-runtime and Azure qualification remain **NO-GO**.
