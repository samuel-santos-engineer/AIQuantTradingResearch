# Codex Authority — Release 1.14 WP07-P3K
## Read-Only Native ABI and Toolchain Evidence Dossier

**Owner authorization:** P3J approved; P3K is authorized solely as the next read-only evidence investigation. **This is not authority to select a technology or implement native code.**

## Mission and decision boundary
Independently assemble a source-grounded evidence dossier comparing minimal C/Linux, minimal Rust/Linux, Python/native FFI, and unchanged legacy Python/.NET reuse (the latter is blocked for direct reuse). Assess whether each candidate could plausibly satisfy the accepted native interface and lifecycle composition specification. Separate observed repository evidence, documented external/platform capabilities, assumptions, unresolved gaps, and proposed qualification steps. Do not claim feasibility from an interface proposal or static contract.

## Protected baseline
- Protected checkout: `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`. Working tree intentionally dirty/untracked. Do not disturb it.
- P3I specification: `docs/roadmap/release-1.14/prompters/release-1.14-wp07-native-interface-composition-specification.md`, SHA-256 `58DEEEE0A481201E42BA2559AB7EE049DCA7F4F7DBE49BE16343030AAC3817B5`.
- Offline contracts: `container/admin_lifecycle_native_boundary_contracts.py`, SHA-256 `5231BD87F3ECF828ACD13609EB3A7E56980B4A94C6E3E797F56728984CF65AD2`.
- Focused tests: `python/presentation/test_admin_lifecycle_native_boundary_contracts.py`, SHA-256 `B1639A4DC14671B04F664CE556C60E0E037ACCA0BC4DF9A6E6631378F33D4FEA`.
- Accepted static launch plan: `container/admin_lifecycle_launch_plan.py`; frozen `aiq-admin-lifecycle-v1` protocol in `container/admin_lifecycle_protocol.py` (64-byte big-endian records; direction sequence 1..32 continuous across startup/shutdown).
- Last reviewed local merged-main evidence is cached, not a fresh remote verification. Local dirty/untracked runtime alternatives are not merged production evidence.
- `PUBLIC_RUNTIME_VERIFIED=False`; production, public-runtime, and Azure qualification are **NO-GO**.

## Absolute operating restrictions
**Empty mutation allowlist.** Read-only inspection only: inspect source, existing documentation, local manifests, existing lockfiles, cached metadata, and Git status/refs/hashes using non-mutating commands. Do not edit/create/delete any repository file or output artifact in the repository. Do not run tests, import project modules, execute project scripts, install/restore packages, invoke compilers/linkers, build, execute native code, launch containers, inspect live processes, use credentials, contact network endpoints, fetch, checkout, stash, reset, clean, commit, push, open PRs, merge, modify Azure or GitHub state, or select a language/ABI/toolchain. Do not invoke a tool with side effects to discover capabilities. Stop if evidence requires any prohibited operation.

## Required investigation
1. **Inventory and provenance.** Record read-only preflight branch/HEAD, complete Git status and local refs; verify the three exact SHA-256 baselines. Identify which sources are tracked at protected HEAD versus untracked/local alternatives and cached refs. Cite precise file:line evidence; do not infer current remote state.
2. **Candidate matrix.** Compare C/Linux, Rust/Linux and Python/native FFI on pre-fork runtime state, direct process/descriptor control, libc/FFI correctness, signal and reaper ownership, memory safety, failure containment, runtime footprint, reproducible packaging, dependency provenance, and audit surface. Explain why unchanged legacy Python/.NET cannot be reused directly. No winner or toolchain selection.
3. **ABI evidence and unknowns.** Identify required Linux interfaces (`pipe2`, `socketpair`, `fcntl`, `dup2`/`dup3`, `close`, optional `close_range`, `fork`, `execve`, `setgroups`, `setresgid`, `setresuid`, `prctl`, capability interfaces, `setsid`/`setpgid`, `sigaction`/`sigprocmask`, `waitpid` or `waitid`, `clock_gettime`, `poll`/`ppoll`). For each relevant cluster document required types/headers, errno/status semantics, platform-specific layouts and kernel/libc/version assumptions; distinguish verified local evidence from assumptions. Never invent ABI constants, syscall numbers or layouts.
4. **Image and packaging provenance.** Inspect Dockerfiles, runtime/project manifests and available lockfiles without building. Identify floating tags, missing immutable digests, target architecture, glibc/musl and dynamic loader uncertainty, runtime publication constraints, executable/library separation, and conditional UID/GID 1000/1100/1200/1300 collision checks. No external image pull or UID probing.
5. **Security obligations and composition.** Map candidate mechanisms to the accepted topology: supervisor PID1 UID1200, direct router UID1100 and guardian UID1300 children, guardian-owned agent UID1000 wait; finite secret-free root bootstrap; permanent identity/capability drops; trusted/presentation session separation; full inherited FD provenance, collision-safe scratch, conditional agent-bootstrap FD5 vacancy, C1 A_EXEC/A_EXEC_ACK gate closure before agent exec, RA3/metadata4 survival, metadata validate-once/close, descriptor-free UI exec, and explicit RA/SR/SG closure. Distinguish launch-plan and contract evidence from actual native proof.
6. **Status, clock and shutdown.** Preserve D1 guardian child-specific normal exit-zero agent wait, D2 supervisor child-specific normal exit-zero guardian wait, supervisor router wait after host disposal; no competing SIGCHLD reapers, auto-reap, ECHILD/EOF/absence/notifier as proof; bounded EINTR with original absolute `CLOCK_MONOTONIC` deadlines; shared four-second shutdown and external five-second Docker stop grace; adopted-child drain only after required waits. Assess candidate-specific qualification questions, not success.
7. **Evidence dossier and staged recommendation.** Produce a compact table with each candidate's strengths, blockers, source-backed observations, unknowns and minimum evidence needed before selection. Rank *risks and qualification questions*, not technologies. Recommend the smallest separately authorized next **design/evidence** increment, its exact allowlist and STOP conditions. Explicitly reserve language/toolchain choice, native prototype, Linux qualification, runtime wiring, Azure feasibility and public activation for later owner decisions.

## Validation and deliverable
- **No tests or execution.** Validation is read-only source reconciliation and exact hash/status comparison only.
- Return the dossier in the Codex response, not as a newly created repository document.
- Include a source/evidence matrix with file:line citations; clear PASS/GAP/BLOCK judgments with narrow meanings; discrepancies and unsupported claims; native/ABI/image unknowns; staged decision gates; and a recommended next authority boundary.
- Repeat complete final branch/HEAD/status/refs and preservation checks. Report any scope violation immediately. Preserve existing untracked paths and all other files.
- Do not convert a design-only PASS into a native-feasibility PASS.

## Mandatory STOP conditions
STOP and request owner review if the baseline hashes differ, protected state cannot be preserved, evidence requires a network call or execution, an additional file/change is necessary, frozen contracts appear to require amendment, or any native/runtime action becomes necessary. No speculative repairs.

## Final verdict (use exactly one)
**WP07-P3K EVIDENCE DOSSIER COMPLETE — OWNER DECISION REQUIRED**

or

**WP07-P3K BLOCKED — OWNER REVIEW REQUIRED**

Always state: `PUBLIC_RUNTIME_VERIFIED=False`; production, public-runtime and Azure qualification remain **NO-GO**.
