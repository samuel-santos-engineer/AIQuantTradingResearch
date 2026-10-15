# Codex Authority — Release 1.14 WP07-P3I
## Design-Only Native Interface and Lifecycle Composition Specification

**Owner decision:** WP07-P3H approved. This authority permits **only** the bounded WP07-P3I documentation increment described below. It does **not** approve a language, toolchain, ABI, native implementation, integration, qualification, deployment, or public activation.

## 1. Authorized output and immutable baseline

Create exactly one new Markdown file (and no other file):

`docs/roadmap/release-1.14/prompters/release-1.14-wp07-native-interface-composition-specification.md`

If this path already exists, **STOP** and request owner review rather than overwrite. All other tracked, untracked, ignored, and repository metadata must be preserved. Work in the current protected dirty checkout; do not clean, stash, reset, checkout, fetch, or otherwise normalize it.

Protected branch: `draft/release-1.14-wp07-partial-public-disabled`.
Protected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.

Previously accepted *offline* evidence:
- Frozen `aiq-admin-lifecycle-v1` 64-byte binary protocol and accepted static launch plan; protocol is not native-qualified.
- `container/admin_lifecycle_launch_plan.py`: accepted static topology, handoffs, metadata, FD and closure obligations.
- `container/admin_lifecycle_native_boundary_contracts.py` SHA-256 `5231BD87F3ECF828ACD13609EB3A7E56980B4A94C6E3E797F56728984CF65AD2`.
- `python/presentation/test_admin_lifecycle_native_boundary_contracts.py` SHA-256 `B1639A4DC14671B04F664CE556C60E0E037ACCA0BC4DF9A6E6631378F33D4FEA`.
- P3G accepted offline validators within their scope; P3H completed read-only feasibility analysis with **no language, ABI, or toolchain selected**.

Do not assume local untracked bootstrap/runtime alternatives are merged production evidence. Cached Git refs are not fresh remote verification. `PUBLIC_RUNTIME_VERIFIED=False`; production/public runtime/Azure **NO-GO**.

## 2. Read-only source inventory and provenance

Before authoring, inspect relevant existing files **without importing, executing, testing, building, installing, or network access**. Record precise local file/line evidence and whether each item is frozen, accepted offline, local-only, candidate, or unknown. Verify the two P3F hashes above; if mismatched, **STOP**. Inspect protocol codec, accepted launch plan, native-boundary contracts/tests, existing entrypoint/Dockerfiles, and relevant legacy lifecycle/bootstrap and .NET project files. Explicitly distinguish local HEAD from cached refs and untracked files.

Capture initial full `git status --porcelain=v1 -uall`, branch, HEAD, and local refs, and a reproducible inventory/hash of pre-existing Git-listed files. Capture equivalent final evidence and explain any coverage exclusions. Read-only Git inspection only.

## 3. Required specification content

Write a self-contained, reviewable design document with the following sections. Each normative obligation must identify its owner, prerequisites, success proof, failure behavior, and relationship to the accepted model. Separate **MUST/STOP security requirements** from **candidate Linux mechanisms** and **unqualified assumptions**.

### A. Trust graph, process lineage, and startup phases

Specify finite secret-free root PID1 bootstrap, router/guardian children, guardian→agent bootstrap, supervisor permanent UID/GID1200 as PID1, router UID/GID1100, guardian UID/GID1300, agent/UI UID/GID1000. Preserve direct-child ownership: guardian waits for agent; supervisor waits for guardian and router. No secret-bearing memory at fork; no public listener before `R_EXPOSE`. Identify phase boundaries, who owns each FD, and when privileged operations must finish. Identity allocations remain conditional on base-image collision checks.

### B. Language-neutral native interface catalog

For `pipe2`/`socketpair`, `fcntl`, `dup2`/`dup3`, `close`/optional `close_range`, `fork`, `execve`, `setgroups`, `setresgid`, `setresuid`, capabilities/`prctl`, `setsid`/`setpgid`, `sigaction`/`sigprocmask`, `waitpid` or carefully qualified `waitid`, `clock_gettime(CLOCK_MONOTONIC)`, and `poll`/`ppoll`: specify candidate signatures at the **type/semantic** level, inputs/outputs, errno/EINTR handling, ownership, blocking constraints, and portability questions. **Do not assert unverified ABI sizes, layouts, constants, syscall numbers, kernel features, libc compatibility, or compiler availability.** Note where libc headers/bindings and platform verification would be mandatory. Do not select C, Rust, Python FFI, ABI, or toolchain.

### C. Descriptor provenance and collision-safe mapping

Provide a role-by-role and phase-by-phase table of all original descriptors, aliases, destination slots, inheritors, explicit closes, CLOEXEC state, and required exec survivors. Account for the complete inherited parent table at each fork, not just desired descriptors. Describe safe cycle evacuation to *verified vacant* scratch slots, no overwriting live endpoints, all duplicate aliases, writer-copy closure for genuine EOF, and native enumeration/race limitations. Root→router/guardian/supervisor and guardian→agent must be reconciled. Agent bootstrap retains RA3 and metadata4 across exec, validates metadata once then closes it; guardian-metadata inherited FD5 is explicitly vacated before conditional gate FD5 assignment; gate and aliases close after `A_EXEC_ACK` and before agent exec; UI exec has **zero** control/credential/metadata/gate descriptors. FD5 remains a **conditional numeric design slot**, never a claimed native allocation proof.

### D. Frozen protocol and launch-plan composition

Map each critical protocol phase and proof to the native interface and accepted static launch trace: actual positive PID binding before metadata emission; C1 guardian→agent-bootstrap `A_EXEC`/`A_EXEC_ACK` gate distinct from post-exec `A_READY`; metadata once-only validation/closure; continuous per-direction sequencing, launch ID, role/PID and deadline checks; bounded binary framing; readiness, `R_EXPOSE`, and exposure prohibition before all prerequisites. Show exactly how separate offline validators must compose so neither standalone readiness/shutdown ledger can falsely establish metadata, hardening, RA/SR/SG closure, or kernel exit evidence. Do not alter frozen message layout, roles, FD maps, or protocol semantics.

### E. Identity, sessions, signals, and waits

Specify irreversible real/effective/saved UID/GID changes, empty supplementary groups and capabilities, post-drop verification, and distinct trusted/presentation sessions, including PID1/session-leader caveats. Specify SIGCHLD disposition and mask constraints; prohibit automatic reaping or competing handlers. Define a single exclusive owner for each direct child; require bounded child-specific positive kernel **normal exit zero** for D1 agent, D2 guardian, and router disposal wait. `ECHILD`, wrong PID, signaled/nonzero exit, EOF, notifier, pidfd readiness, absence, or namespace teardown never count as positive proof. Describe adopted-child drain only after required waits and failure containment D3 as non-proof.

### F. Shared absolute clocks, shutdown, and failures

Specify checked `CLOCK_MONOTONIC` arithmetic, a shared absolute four-second shutdown deadline, bounded retries without deadline reset, five-second external Docker stop grace, and five-second privileged / thirty-second total startup deadlines where applicable. Show ordered STOP/EOF/UI completion, `HOST_DISPOSED`, RA/SR/SG closure, required waits, then adopted-child drain/terminal absence. Distinguish disposal from positive router wait. Define fixed error classifications and sticky failure rules; identify operations whose blocking semantics remain to be qualified.

### G. Candidate comparison and qualification matrix

Compare minimal C/Linux, minimal Rust/Linux, and Python/native FFI **without choosing**. List exact evidence still needed for headers/bindings, ABI/libc/loader, architecture, reproducible toolchain and dependencies, immutable image digests, UID/GID1000/1100/1200/1300 availability, native FD enumeration/remapping, fork safety, SIGCHLD/reaping, session behavior, monotonic clock and four-second feasibility, .NET executable/router packaging and Python agent handoff. Flag unchanged legacy Python/.NET topology as blocked for direct reuse.

### H. Negative-case matrix, traceability, and staged gates

Include concrete counterexamples and rejection/proof expectations for occupied FD5, live destination overwrite, scratch collision, alias leakage, missing gate/ACK, contradictory endpoint provenance, malformed startup prefix, pre-fork secrets, partial identity drop, session collision, incorrect parent/duplicate PID, competing reaper, `ECHILD`-as-success, deadline reset/overflow, wait-before-disposal, early exposure, omitted RA/SR/SG closure, and drain-before-required-waits. Cross-reference accepted model/contract rules and identify any composition gaps. End with explicit staged decisions: independent design review → separate owner decision on language/toolchain/ABI → separately authorized offline/native prototype → local Linux qualification → packaging/image qualification → Azure/public gates. No stage is automatically authorized.

## 4. Execution and validation restrictions

**Allowed:** read-only local source inspection and writing the one authorized Markdown file; read-only final source review of that document. No test execution, imports, scripts, build/restore, compiler checks, package installation, native experiments/syscalls, credential processing, Docker, Azure, web/network, GitHub operations, commit, PR, merge, fetch, or modifications outside the single allowlisted file. Do not execute generated snippets. Do not edit frozen contracts or existing tests. Do not choose a language, toolchain, ABI or implementation approach.

If the task requires a second path, a frozen-contract amendment, uncertain existing-file overwrite, unverified native claim presented as fact, or any prohibited action: **STOP** and report the blocker. Do not silently broaden scope.

## 5. Required final report

Report: exact created path; concise document section index; source-backed PASS/GAP/BLOCK matrix; unresolved ABI/native and composition prerequisites; whether a language/toolchain was selected (**must be NO**); initial/final branch, HEAD, complete status/refs and pre-existing file integrity comparison; the new document's SHA-256; all operations performed and explicitly not performed. Distinguish Codex-reported inspection from actual native proof. Request independent read-only design review as the next possible owner decision.

Final verdict exactly one of:

**`WP07-P3I DESIGN SPECIFICATION COMPLETE — OWNER REVIEW REQUIRED`**

**`WP07-P3I BLOCKED — OWNER REVIEW REQUIRED`**

Always reiterate `PUBLIC_RUNTIME_VERIFIED=False` and production/public-runtime/Azure **NO-GO**.
