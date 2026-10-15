# Codex Authority — Release 1.14 WP07-P3A
## Read-only Native Boundary Design Investigation

**Owner authorization:** P2-C5 approved. FD5 is the **conditional agent-bootstrap launch-gate design slot**, not permission for native allocation or implementation. This authority permits **read-only investigation and a proposal only**.

## Mandatory safety boundary

- Preserve the intentionally dirty protected checkout, its current branch/HEAD, local refs, full Git status and every file. Record before/after evidence. Do not checkout, reset, clean, stash, fetch, commit, push, open PRs or merge.
- **No file edits or generated repository artifacts.** No tests, imports that execute project code, builds, restores, installs, scripts, containers, Azure actions, process launches, privilege changes, network calls or GitHub mutations. Read source and inspect metadata only.
- Do not treat legacy bootstrap, router PID1 assumptions, ASCII lifecycle transport, `waitpid(-1)`, or relative startup budgets as approved implementation.
- Preserve `PUBLIC_RUNTIME_VERIFIED=False`; production, public runtime and Azure qualification remain **NO-GO**.

## Frozen architecture to investigate (do not revise)

Finite secret-free root PID1 pre-fork; supervisor stays PID1 as UID/GID 1200/1200, router UID/GID 1100/1100, guardian UID/GID 1300/1300, agent/UI UID/GID 1000/1000, conditional on image collision checks. Supervisor directly parents router and guardian; guardian directly parents agent; agent parents UI. Distinct trusted/presentation sessions, permanent identity drop (real/effective/saved IDs), empty supplementary groups and capabilities, no cross-UID signaling. Router-local disposable synthetic credential only after separation, qualification-only. No public listener before `R_EXPOSE`.

Preserve frozen `aiq-admin-lifecycle-v1` 64-byte binary protocol, roles, messages, sequence and PID bindings; C1 `A_EXEC`/`A_EXEC_ACK` gate before exec and `A_READY` after exec; agent FD4 metadata survives exec, validates once and closes. Process-local maps: S FD3 SR/FD4 SG/FD5 metadata; G FD3 SG/FD4 gate/FD5 metadata; R FD3 credential/FD4 RA/FD5 SR/FD6 metadata; agent FD3 RA/FD4 metadata; **agent-bootstrap transient FD5 gate**, explicitly closed with every alias after ACK and before exec, CLOEXEC backstop. UI exec inherits no control/credential/metadata/gate endpoints. FD5 is conditionally approved for design only; unknown scratch occupancy and alias numbers remain unqualified.

Absolute CLOCK_MONOTONIC startup deadlines: privileged bootstrap 5 seconds, total 30 seconds; shared shutdown deadline 4 seconds, Docker stop grace 5 seconds. D1 guardian independently obtains positive bounded kernel wait of agent exit 0; D2 supervisor independently obtains positive bounded kernel wait of guardian exit 0; supervisor independently waits router exit 0 after host disposal; adopted-child drain/terminal absence only after mandatory positive waits. EOF/ECHILD/absence/reports cannot substitute for positive waits.

## Investigation tasks

1. **Source inventory with paths and line evidence:** Identify existing native/runtime entrypoints, .NET project packaging, Docker entrypoint/base image, protocol implementations, lifecycle models, identity/session/wait/clock adapters, and local legacy conflicts. Distinguish merged main evidence from uncommitted protected-worktree evidence; do not fetch.
2. **Native syscall boundary design:** Propose minimal OS interfaces for pipe/socketpair creation, `dup2`/close/CLOEXEC and alias enumeration, fork/exec handoffs, UID/GID/group/capability drop and verification, setsid/session isolation, monotonic absolute clock/deadline propagation, signal restrictions, direct-child `waitpid`/`waitid` bounded positive exit proofs, reaping/drain and PID1 shutdown. Describe error and timeout handling without performing operations.
3. **Descriptor provenance table:** For each root/guardian/router/agent/UI fork, trace creation, parent possession, child inherited copies, FD slot occupancy (especially agent-bootstrap FD5), remapping constraints, explicit unused-end closures, pre-exec survivors, and UI-exec emptiness. Identify where the accepted symbolic model is insufficient for real native safety.
4. **Protocol integration:** Determine how frozen binary records, metadata PID binding, one-shot validation, endpoint directions, continuous per-direction sequences, C1 gate, readiness/exposure and shutdown proofs could connect to a native adapter without changing protocol semantics.
5. **Deployment and feasibility blockers:** Inspect base-image UID/GID collisions, .NET executable vs library boundaries, PID1 entrypoint assumptions, runtime availability and credential-generation placement. Mark any unverified claim as unverified.
6. **Next-step proposal:** Recommend a small, separately owner-authorized **design-only or offline adapter** increment, with exact path allowlist, acceptance/negative tests, dependency policy, validation command and STOP conditions. Do not implement it. If a frozen contract, descriptor map, identity, protocol or additional file must change, stop and present an owner decision rather than assuming approval.

## Reporting requirements

Provide a source-backed findings table (PASS/GAP/BLOCK), explicit native-vs-symbolic evidence distinction, risks ordered by severity, a collision/alias-aware FD5 design assessment, and a staged implementation/qualification sequence. Include initial/final branch, HEAD, refs, complete Git status, and read-only preservation evidence feasible without altering the checkout. Do not claim a hash sweep if not performed. Report the exact next owner decision and scope.

Conclude with exactly one verdict:

- `WP07-P3A INVESTIGATION COMPLETE — OWNER DECISION REQUIRED`
- `WP07-P3A BLOCKED — OWNER REVIEW REQUIRED`

No investigation finding constitutes implementation, native qualification, runtime approval, or authorization to merge/deploy.
