# Release 1.14 WP07-P3J — Independent Read-Only Native Interface and Lifecycle Composition Design Review

## Authority and objective
Owner authorizes **WP07-P3J only**: independently inspect the P3I design specification against the accepted static launch plan, offline native-boundary contracts, frozen lifecycle protocol, and source-backed runtime constraints. This is an **investigation/review**, not approval of native feasibility, implementation, packaging, toolchain, deployment, or public exposure.

**Mutation allowlist: EMPTY.** Do not create or edit any repository file, including reports. Return findings in the Codex response only. Do not run tests, imports, builds, installations, native experiments, containers, network calls, Azure operations, or Git mutations (including fetch, checkout, stash, reset, clean, commit, push, PR, merge). Do not execute project scripts. Read-only source inspection and Git/status/hash commands are allowed. If any requirement needs prohibited activity, STOP and report it.

## Protected baseline
- Branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Working tree is intentionally dirty/untracked: preserve **all** existing entries exactly. Do not normalize or reconcile local source against remote evidence.
- P3I document: `docs/roadmap/release-1.14/prompters/release-1.14-wp07-native-interface-composition-specification.md`, reported SHA-256 `58DEEEE0A481201E42BA2559AB7EE049DCA7F4F7DBE49BE16343030AAC3817B5`.
- Native-boundary model: `container/admin_lifecycle_native_boundary_contracts.py`, SHA-256 `5231BD87F3ECF828ACD13609EB3A7E56980B4A94C6E3E797F56728984CF65AD2`.
- Focused tests: `python/presentation/test_admin_lifecycle_native_boundary_contracts.py`, SHA-256 `B1639A4DC14671B04F664CE556C60E0E037ACCA0BC4DF9A6E6631378F33D4FEA`.
- Accepted static launch plan: `container/admin_lifecycle_launch_plan.py`; frozen 64-byte `aiq-admin-lifecycle-v1` protocol and existing codec are authoritative. Prior accepted offline models are **symbolic only**.
- `PUBLIC_RUNTIME_VERIFIED=False`. Production, public-runtime and Azure gates are **NO-GO**. Disabled Option A must not change.

## Preflight and evidence handling
1. Capture complete initial branch, HEAD, local refs, and `git status --porcelain=v1 -uall` (or equivalent complete status); identify and preserve all pre-existing dirty/untracked entries. Confirm the three SHA-256 baselines before substantive review; STOP on mismatch and report without repairing.
2. Read the **entire** P3I specification, sections A–H, and inspect relevant lines of the accepted launch plan, protocol codec, native-boundary contracts and tests. Read runtime/Docker/source packaging only to substantiate compatibility findings. Clearly label merged-HEAD evidence versus local untracked/dirty files and cached refs; do not claim remote freshness.
3. Give source file and line references for each PASS, GAP, BLOCK and counterexample. Distinguish normative frozen requirements, proposed interfaces, symbolic assertions and unqualified kernel/platform behavior. Do not turn suggested syscall signatures into a selected ABI.

## Independent review checklist
### A. Trust, topology and startup
- Confirm finite secret-free root bootstrap PID1, router and guardian creation before incompatible permanent drops, supervisor UID/GID1200 remaining PID1, router UID/GID1100, guardian UID/GID1300, agent/UI UID/GID1000, and role-parent/required-wait ownership.
- Confirm no secret-bearing pre-fork state, no privileged helper retained, correct session boundaries, startup deadlines (5-second privileged phase and 30-second total), and no public listener before `R_EXPOSE`.
- Identify any inconsistent or unstated parent, fork, exec, session or identity prerequisites.

### B. Native interface and ABI proposals
- Check that candidate `pipe2`/`socketpair`, `fcntl`, `dup2`/`dup3`, `close`/optional `close_range`, `fork`, `execve`, identity/capability, signal, wait and monotonic clock interfaces specify ownership, argument/return/error semantics and platform assumptions **without** asserting unqualified signatures or selecting C/Rust/Python, libc, ABI or toolchain.
- Check EINTR handling against the original absolute deadline; distinguish wait zero-return/pending from positive status; ECHILD/wrong PID/signal death/nonzero exit must fail.
- Assess fork-safety, thread/runtime initialization, signal disposition, SIGCHLD exclusivity, PID1 reaping, and bounded failure containment.

### C. Descriptor provenance and exec boundaries
- Trace root→router/guardian/supervisor and guardian→agent inherited descriptor inventories, including all aliases and parent-held copies.
- Review occupied destinations, remapping cycles, scratch allocation/vacancy, explicit closure, CLOEXEC backstop, EOF writer-copy closure and enumeration races. **FD5 is conditional design only**: prove the specification demands actual vacancy before agent gate assignment; no symbolic `None` is native proof.
- Confirm router/guardian/supervisor FD maps, agent RA3/metadata4 survival, inherited guardian metadata5 closure, C1 `A_EXEC`/`A_EXEC_ACK` gate history and closure before agent exec, postexec metadata validate-once-and-close, and descriptor-free UI exec.

### D. Protocol and cross-validator composition
- Compare against the frozen 64-byte big-endian codec: launch ID, roles, types, per-direction sequence 1..32 continuous across startup/shutdown, PID bindings, absolute monotonic deadlines, reserved zeros, framing and error classifications.
- Verify ordering from actual fork/PID binding through metadata, gate ACK, A_READY, UI readiness, `R_EXPOSE`, and shutdown. ACK is not readiness or exec proof; EOF/notifier/absence are not required child-exit proof.
- Explicitly map which obligations are enforced by launch-plan validator versus native-boundary contracts versus **still-unimplemented composition**. Do not claim `Ledger.complete_model` alone proves metadata, hardening or RA/SR/SG closures.

### E. Shutdown and proof obligations
- Confirm one shared 4-second `CLOCK_MONOTONIC` shutdown deadline and external 5-second Docker stop grace, without resetting deadlines.
- D1: guardian must obtain positive bounded direct-child agent normal exit-zero; D2: supervisor must obtain positive bounded guardian normal exit-zero and separately router exit-zero after `HOST_DISPOSED`; T2 adopted-child drain/absence only after required waits; D3 teardown is containment only.
- Check endpoint closure/EOF ordering, sticky failures, timestamped prerequisites, status-owner exclusivity and cross-UID cooperative shutdown.

### F. Qualification matrix and negative cases
- Assess explicit negative scenarios for FD collision/alias escape, contradictory gate provenance, malformed startup prefix, competing reaper, wrong parent/PID, ECHILD-as-success, deadline overflow/reset, incomplete privilege drop, session collision, metadata omission, premature exposure, wait-before-disposal and drain-before-waits.
- Confirm unqualified items remain GAPs: libc/ABI/loader, image digests and UID/GID collisions, kernel descriptor operations, SIGCHLD and direct waits, sessions, capability enforcement, clock/deadline feasibility, Docker grace and Azure behavior.
- Identify the smallest safe next separately authorized increment, its exact allowlist and STOP conditions. **Do not select a language/toolchain or begin native prototypes.**

## Deliverable and decision standard
Return a concise but source-backed report containing: (1) preservation/preflight evidence; (2) section-by-section PASS/GAP/BLOCK table; (3) material contradictions or acceptance bypasses with exact line citations and reasoned counterexamples; (4) cross-validator obligation-to-evidence matrix; (5) unresolved native qualification gates; (6) recommendation for smallest next bounded phase, with explicit owner decision required.

**ACCEPT** only if the design document faithfully preserves all frozen/accepted obligations, marks assumptions and platform behaviors unqualified, and contains no material internal contradiction within its declared design-only scope. Do not treat missing native evidence as an automatic design BLOCK when it is accurately marked GAP; do BLOCK if the document claims it as established or omits a required security obligation. If evidence is insufficient, mark GAP/BLOCK rather than inventing proof.

At finish, recapture branch, HEAD, refs, complete Git status and pre-existing-file preservation evidence. Note limitations of hashing ignored files or internal Git metadata. No source mutations are permitted.

Finish with **exactly one** verdict:
- `WP07-P3J INDEPENDENT DESIGN REVIEW ACCEPTED — OWNER DECISION REQUIRED`
- `WP07-P3J BLOCKED — OWNER REVIEW REQUIRED`

`PUBLIC_RUNTIME_VERIFIED=False`; production, public-runtime and Azure qualification remain **NO-GO**.
