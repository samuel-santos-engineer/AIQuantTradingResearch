# Codex Authority — Release 1.14 WP07-P3C
## Independent Native-Boundary Contract Security Review (READ ONLY)

**Owner authorization:** WP07-P3C is authorized for independent, read-only review only. This document is not authority to implement, repair, execute tests, build, commit, fetch, push, open PRs, merge, or deploy.

### Protected baseline
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Protected local branch: `draft/release-1.14-wp07-partial-public-disabled`; expected HEAD `67893f51931ee183f71050a83b81127e6e648b87`.
- Preserve the intentionally dirty checkout, all existing Git status entries, local refs, and pre-existing files. Record full before/after `git status` and refs; inspect and hash without modifying. No checkout, reset, clean, stash, fetch, commit, or PR.
- Last reported merged remote-main baseline: `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8` (do not refresh remotely).
- P3B files and reported SHA-256 baselines:
  - `container/admin_lifecycle_native_boundary_contracts.py`: `F43BE3359C0C8C79B5428F2061500D2C2E439D08D0DF2F5AEFE3DE745D83DA40`
  - `python/presentation/test_admin_lifecycle_native_boundary_contracts.py`: `5465744E17CC05A5662E411BA4EF3430E89D5811115CD334A813C00E7F08D477`
- P3B reported `23 passed, exit 0`; **do not rerun** under this authority.
- `PUBLIC_RUNTIME_VERIFIED=False`; production, public runtime, and Azure gates remain NO-GO.

### Read-only source scope
Inspect the two P3B files in full. Compare against `container/admin_lifecycle_launch_plan.py`, `container/admin_lifecycle_protocol.py`, their relevant offline tests, and existing legacy runtime files solely as reference. No file edits, imports, test execution, builds, package restore, native syscalls, Docker, Azure, or network calls.

### Mandatory independent review
1. **Descriptor provenance and remapping:** Trace endpoint creation, complete fork inheritance, alias inventory, scratch-slot constraints, occupied destination and live-endpoint overwrite rejection, duplication cycles, CLOEXEC versus explicit closure, agent FD3 RA / FD4 metadata / conditional FD5 launch gate, guardian FD4 gate, and descriptor-free UI exec. Distinguish symbolic checks from native feasibility.
2. **Child-specific status ownership:** Verify direct parentage and exactly one exclusive reaper per mandatory child; required wait evidence must identify the exact child PID and prove normal exit zero. Reject ECHILD, wrong returned PID, `waitpid(-1)`, zero-return/polling as success, EOF, pidfd readiness, notifier reports, disappearance, signal death, and nonzero exit. Verify D1 guardian→agent, D2 supervisor→guardian, and supervisor→router obligations independently; T2 adopted-child drain only after positive waits.
3. **Identity, secrecy, and sessions:** Check real/effective/saved UID/GID and empty supplementary groups/capabilities, distinct S1200/G1300/R1100/A+UI1000 roles, secret-free root pre-fork, no cross-UID signaling dependence, trusted versus presentation sessions, and no privilege-retaining helper. Identify any contract that only asserts a label rather than verifying a meaningful obligation.
4. **Clock, failures, and shutdown:** Confirm absolute `CLOCK_MONOTONIC` deadline propagation, checked arithmetic, five-second privileged startup and 30-second overall startup, one shared four-second shutdown budget and five-second Docker grace. Check bounded EINTR retry without resetting deadline, sticky failure, and ordered disposal, mandatory waits, drain, terminal absence. D3 teardown must remain failure containment only.
5. **Protocol compatibility:** Check frozen `aiq-admin-lifecycle-v1` 64-byte records, positive PID-bound metadata emitted after fork binding, sender/role/order and per-direction sequence, C1 `A_EXEC`/`A_EXEC_ACK` bootstrap-only gate, agent exec before `A_READY`, metadata FD4 validation once after exec then closure, and no `R_EXPOSE` before full readiness. Do not authorize wire/protocol changes.
6. **Test effectiveness:** Inspect each of the 23 test methods, including rejection classifications and negative mutations. Identify acceptance bypasses, missing cases, false-positive tests, and checks that rely only on canonical equality or synthetic assertions. Do not claim runtime or kernel proof.
7. **Implementation feasibility questions:** Identify unresolved language/toolchain, ABI, libc/runtime, image digest and UID/GID collision, descriptor enumeration/race, SIGCHLD/reaping, PID1/session, and native clock issues. Recommend an investigation or design-only next increment with exact file allowlist and STOP conditions; do not select or implement a toolchain.

### STOP conditions
Stop and report BLOCKED if review needs any mutation, test execution, native experiment, network access, extra authority, frozen topology/map/protocol change, or cannot establish source-backed contract consistency. Do not repair findings in this authority.

### Required report
- Lead with exactly one verdict: **`WP07-P3C REVIEW ACCEPTED — OWNER DECISION REQUIRED`** or **`WP07-P3C BLOCKED — OWNER REVIEW REQUIRED`**.
- Provide a source-and-line-backed findings table with PASS / GAP / BLOCK, severity, exact security consequences, and explicit distinction between declared/symbolic obligations and verified native behavior.
- Include 23-test inspection findings, gaps, next bounded proposal and precise owner decision wording; do not automatically promote to implementation.
- Report initial/final complete Git status and refs, checked hashes (including P3B baselines), any limits on ignored-file coverage, and whether the checkout remained unchanged.
- Preserve `PUBLIC_RUNTIME_VERIFIED=False` and production/public/Azure NO-GO.
