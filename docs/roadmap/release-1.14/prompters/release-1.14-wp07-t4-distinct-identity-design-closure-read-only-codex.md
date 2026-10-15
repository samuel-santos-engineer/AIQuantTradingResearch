# Codex Authority — Release 1.14 WP07: T4 Distinct Identity Design Closure

**Authority class:** Read-only architecture/design closure; planning only.  
**Owner decision:** T4 APPROVED for investigation only.  
**Implementation / qualification / deployment authority:** NONE.  
**Required final disposition:** `DESIGN CLOSED (PLANNING ONLY)` or `DESIGN NOT CLOSED`, followed by `STOP — OWNER REVIEW REQUIRED`.

## 1. Objective

Determine whether an enabled-mode architecture using **mutually distinct, unprivileged supervisor identity S and guardian identity G**, both different from router UID 1100 and presentation UID 1000, has a complete, defensible process-creation, privilege-transition, integrity, lifecycle and evidence design. Identify precise unresolved blockers and any additional owner approvals. Do not choose numeric identities or implement anything.

## 2. Binding owner decisions and non-negotiable constraints

- **A1:** An unprivileged supervisor may serve as enabled-mode namespace PID 1; router remains UID 1100.
- **A2:** Supervisor independently verifies guardian normal exit 0 and owns final container verdict.
- **A3:** No expanded privileged managed startup. Finite root bootstrap only; no resident privileged helper.
- **D1:** Guardian attestation of the agent's actual kernel-observed exit status is conditional on qualified trusted-guardian integrity. Self-report alone is insufficient.
- **Strict D2:** Supervisor must independently perform a specifically bound kernel wait establishing guardian normal exit 0. EOF, disappearance, `ECHILD` at a required wait, and guardian self-report are not substitutes.
- **D3:** Namespace/container teardown is failure containment after FAIL, never clean PASS evidence.
- **T1:** Read-only investigation of distinct supervisor identity was approved; no UID allocation or identity-contract amendment.
- **T2:** PID 1 adoption plus terminal child absence conditionally accepted as descendant-closure evidence **only after positive required statuses**, subject to kernel qualification and explicit namespace/no-injection assumptions. Terminal `ECHILD` proves absence only; `WNOHANG` zero does not.
- **T3:** Production administration remains disabled until a compliant credential-ingress mechanism is established. Do not reopen generic Azure credential research.
- **T4:** Read-only closure investigation of mutually distinct unprivileged supervisor and guardian identities S/G, different from 1000/1100. Analysis of finite unmanaged guardian bootstrap pre-forking UID 1000 agent before permanent guardian drop/exec is allowed. **No identity allocation, identity-contract amendment, retained privilege, cross-UID signaling, or implementation.**
- **Timing:** One shared `CLOCK_MONOTONIC` shutdown deadline of four seconds from shutdown initiation; Docker stop grace five seconds. No clock reset across stages.
- **Mode and release:** Disabled Option A unchanged. `PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO**.

## 3. Repository preservation / permitted activity

Expected baseline (verify read-only before and after):

- Branch: `draft/release-1.14-wp07-partial-public-disabled`
- HEAD: `67893f51931ee183f71050a83b81127e6e648b87`
- Existing dirty working tree is intentional. Preserve all tracked/untracked content, metadata and Git state.

**Allowed:** Inspect existing source, documents, recorded qualification evidence and read-only local Git metadata; reason from Linux semantics and already-available documentation; prepare findings **in the Codex response only**. Clearly distinguish CONFIRMED, SOURCE-SUPPORTED, INFERRED, UNPROVEN, UNAVAILABLE and OWNER DECISION REQUIRED. If baseline differs, report and stop rather than repair.

**Forbidden:** Any file creation/modification/deletion in repository; formatters, codegen, package installation, builds, tests, Docker/container runs, Azure calls or deployment, credential retrieval, secret inspection, numeric UID/GID selection, privilege or contract changes, commits, pushes, branches, rebases, resets, stashes, GitHub issues/PRs or Project changes. Do not alter the dirty tree. Do not claim runtime verification from source inspection.

## 4. Bounded design questions

### 4.1 Exact creation graph and permanent identity transitions

Specify an exact ordered sequence for finite root namespace PID 1 to fork the UID 1100 router and a guardian bootstrap branch; for that guardian branch to fork the UID 1000 agent **before** permanently dropping to G and execing guardian; and for original PID 1 to permanently drop to S and exec supervisor **without changing PID or direct-child relationships**. Define which branches own each wait, when supplementary groups/capabilities are removed, and why neither supervisor nor guardian needs privilege to create a differently credentialed child after dropping. Keep .NET router startup strictly after its drop. No privileged managed runtime or resident root process.

Address fork/exec failures, premature exits, readiness race windows, descriptor inheritance, parent death and lack of restart/re-exec recovery. State any unavoidable conflict with existing code or contracts; do not quietly reinterpret approved decisions.

### 4.2 Startup gating and session isolation

Design a bounded, authenticated readiness sequence that prevents agent/UI launch until supervisor and guardian have completed post-final-exec hardening, endpoint binding and expected-child registration. Router must not publish enabled readiness before all required role bindings succeed. Define exact fail-closed behavior for missing readiness, startup timeouts and partial launch.

Analyze Linux sessions/process groups, especially cross-UID same-session `SIGCONT`. Specify how agent/UI enter a session separate from trusted roles, with parentage and FD ownership intact. Explain what this separation does and does not prevent. No cross-UID signaling exception.

### 4.3 Trusted guardian D1 integrity

Assume agent/UI may be hostile. Analyze attempts to forge PASS through guardian memory modification, ptrace or `/proc`, same-session signals, FD duplication/leakage, writable code/interpreters/import paths, environment injection, re-exec dumpability resets and channel spoofing. Distinguish attacks that can only force FAIL from attacks that can forge PASS. Identify exact assumptions and controls needed to make guardian D1 truthful (not merely guardian exit 0). A compromised guardian remains a TCB failure; strict D2 does not prove honesty.

Describe immutable dependency ownership, post-final-exec nondumpability, isolated session, sanitized execution environment, absence of inherited privileged handles, per-launch authenticated channel binding, and no untrusted-process-accessible restart path. Identify which properties need empirical Linux/kernel qualification and which cannot be established under current constraints.

### 4.4 Descriptor / IPC / credential matrix

Provide a role-by-role table: S supervisor, router 1100, G guardian, agent/UI 1000; real/effective/saved UID/GID, supplementary groups/capabilities, kernel parent and wait owner, sessions, permitted inherited FDs, IPC direction and credentials exposure. Detail closing *all* unused endpoints and duplicates before untrusted exec. Distinguish bootstrap-time peer identity from post-drop peer identity; do not treat UID equality or channel possession alone as proof of report truthfulness.

Router alone may consume the private credential FD. Supervisor, guardian, agent and UI must not inherit credential material. T3 production-disabled restriction is binding; do not propose environment/app-settings, secret files, extra services or unsupported private-FD ingress as approved production solutions.

### 4.5 Shutdown and evidence state machine

Give a precise, auditable sequence for lifecycle ACK/EOF, qualified guardian wait of specifically bound agent exit 0 (D1), supervisor's independent bound guardian exit 0 (D2), supervisor's bound router exit 0 and host disposal, adopted-child draining, and final terminal child absence (T2). Explain the sole reaping authority, `SIGCHLD` disposition, `ECHILD` contexts, unknown orphans, intermediate subreapers, nested PID namespaces and external injection assumptions.

All required waits and cooperative cross-UID IPC must consume the same four-second monotonic deadline; no blocking beyond remaining budget or clock reset. Map failure scenarios (crash, hang, malformed/duplicate/stale frame, incorrect child identity, unknown descendants, cancellation, partial startup, supervisor death) to sticky FAIL. D3 is containment only. No invented measured timings or runtime results.

### 4.6 Closure decision and minimal next step

Compare the distinct S/G proposal to the previous shared-UID model only to the extent needed to justify the integrity boundary. Clearly identify whether the *design* is complete enough to request a **separate** owner identity-contract decision; do not treat T4 as that approval. If closure is impossible, isolate the smallest remaining technical or policy blocker and recommend one bounded next authority. Do not propose an implementation allowlist before closure.

## 5. Required response format

1. **Verdict:** `DESIGN CLOSED (PLANNING ONLY)` or `DESIGN NOT CLOSED`, with one-paragraph rationale.
2. **Baseline preservation:** Initial/final branch, HEAD and full status comparison; explicitly state whether content hashes were checked.
3. **Evidence ledger:** CONFIRMED / SOURCE-SUPPORTED / INFERRED / UNPROVEN / UNAVAILABLE / OWNER DECISION REQUIRED; cite inspected repository paths and relevant lines where available.
4. **Exact topology and startup sequence:** Process tree, ordered fork/drop/exec, readiness and session transition gates.
5. **Identity/FD/IPC matrix:** Per-role IDs, parentage, wait authority, session, inherited handles, control channels and secret exposure.
6. **Guardian integrity proof obligations:** Attack/defense table; forged PASS versus forced FAIL; remaining trusted assumptions.
7. **Shutdown proof:** D1, strict D2, router disposal, T2 absence, monotonic budget and sticky-failure table.
8. **Decision ledger:** Already-approved A1/A2/A3, D1/D2/D3, T1/T2/T3/T4 versus any *proposed, unapproved* amendment; no numeric identities.
9. **Smallest next authority:** Precisely bounded, with outstanding empirical qualifications and provenance requirements separated from design blockers.
10. **Release disposition:** Explicit `PUBLIC_RUNTIME_VERIFIED=False`, all NO-GO gates and `STOP — OWNER REVIEW REQUIRED`.

## 6. Stop conditions

Stop and report if baseline differs, an instruction would require prohibited execution, the topology violates a binding owner decision, guardian truthfulness cannot be defended, or any mandatory positive exit proof would be replaced by EOF, PID disappearance, self-report, generic `ECHILD`, or container teardown. Never label unqualified mechanisms PASS.

**NO IMPLEMENTATION. NO RUNTIME QUALIFICATION. NO GITHUB OR AZURE CHANGES.**
