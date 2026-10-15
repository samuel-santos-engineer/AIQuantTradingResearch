# Codex Authority — Release 1.14 WP07
## Startup Protocol and Synthetic Credential Boundary Closure

**Authority class:** Read-only architecture/specification investigation. **No implementation.**
**Owner authorization:** This file authorizes analysis only, not adoption of a replacement credential technique or an identity-contract amendment.

### 1. Baseline and mandatory preservation

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

- Branch: `draft/release-1.14-wp07-partial-public-disabled`
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`
- Existing dirty working tree is intentional. Capture initial and final branch, HEAD, and full status. Do not change files, index, branches, commits, or GitHub state. If baseline differs, report and stop; do not repair it.
- Existing preservation evidence is status-level only; do not claim content-hash preservation without measuring it.

### 2. Binding owner decisions and gates

Preserve **A1** (unprivileged enabled-mode supervisor is PID 1; router remains UID 1100), **A2** (supervisor independently verifies guardian exit 0 and owns final verdict), **A3** (no expanded privileged managed startup); **D1** (qualified truthful guardian kernel-wait attestation for bound agent), **strict D2** (independent positive guardian normal exit 0), and **D3** (container teardown is failure containment only).

Preserve **T1** (distinct supervisor identity investigation only, no allocation/amendment), **T2** (conditional PID 1 adopted-child drain and terminal child absence after positive status proofs, subject to qualification), **T3** (production administration remains disabled until compliant ingress is established), and **T4** (read-only analysis of distinct unprivileged S/G identities and finite unmanaged root pre-fork; no allocation or contract amendment).

- S and G are **symbolic, mutually distinct**, and distinct from router UID 1100 and presentation UID 1000. Do not select numeric UIDs/GIDs.
- Preserve disabled Option A unchanged.
- Shutdown has **one shared CLOCK_MONOTONIC four-second deadline** from shutdown initiation; Docker grace remains five seconds. Startup is a separate, finite, bounded phase; do not silently consume or redefine shutdown timing.
- `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification, and public Worker activation remain **NO-GO**.

### 3. Fixed architectural premises (not implementation approval)

Analyze the following proposed creation graph:

```text
Finite unmanaged root bootstrap — PID 1
├── Router branch → permanent UID/GID 1100 drop → exec .NET
├── Guardian bootstrap branch
│   ├── Agent branch → permanent UID/GID 1000 drop → gated agent exec
│   │   └── Streamlit UID/GID 1000 after release
│   └── permanent drop to symbolic G → exec guardian
└── permanent drop to symbolic S → exec supervisor (same PID 1)

Supervisor S directly waits for router and guardian.
Guardian G directly waits for agent.
Agent directly waits for UI.
```

No role may create a differently credentialed child after its permanent drop. No resident root helper, retained capability, cross-UID signaling exception, or privileged .NET startup. Separate presentation session from trusted sessions, including the same-session `SIGCONT` exception. Require complete dependency integrity, sanitized exec, post-final-exec hardening, and exact descriptor confinement. Do not claim these protections are qualified.

### 4. Investigation A — executable-level startup *specification*, not code

Produce a complete proposed finite-state startup protocol with:

1. Exact creation order and the earliest point at which each role may exec, bind channels, claim ready, release the agent, launch UI, and expose router readiness. Prevent UI/agent execution before trusted roles' hardening and binding; distinguish gated process creation from execution.
2. Every startup state, transition, actor, guard, acknowledgment, and terminal failure. Specify an **absolute monotonic startup deadline** (propose a bounded value or identify the owner choice needed); no per-message resets, indefinite waits, restart, or privileged recovery.
3. Concrete bounded record layouts: version, role, per-launch binding, sequence, message type, numeric bounds, framing and EOF semantics. State which role validates each record, which failures are sticky, and how stale/duplicate/reordered/partial records are rejected. Do not confuse channel possession with trusted code honesty.
4. Ownership of startup metadata and per-launch identifiers, how each role learns bound child identity without PID reuse ambiguity, and which authority alone performs each wait. No competing waiter, `SIGCHLD` status discard, or arbitrary-child substitution.
5. Exact role/FD allowlist matrix for **each fork and exec boundary** (including duplicates, read/write ends, inheritance and closure). UI receives no privileged/control/result/credential handles.
6. Sessions, UID/GID and capability transitions; readiness failures, fork/exec failure, early child death, malformed messages, timeouts, premature launch and simultaneous failures. All fail closed.
7. Interface between successful startup and later shutdown proof (D1, strict D2, router zero/disposal, T2 child absence), preserving the separate four-second shutdown clock.

Explicitly reconcile the existing router's **PID 1 validation** with A1's child-router topology at the specification level. Identify the future validation invariant, but do not edit it.

### 5. Investigation B — synthetic qualification credential boundary

The inspected bootstrap currently reads synthetic key material **before fork**; closing a pipe or sanitizing an exec later does not undo inherited secret-bearing fork memory. Analyze and compare, without executing:

- **Existing synthetic-only wrapper/bootstrap mechanism:** identify exact trust and memory-copy limits; do not describe it as credential-free or production-compliant.
- **Router-branch-only synthetic generation after separation:** whether a disposable local qualification key can be created and consumed solely in the router branch, after all other branches have forked, with no secret-bearing parent memory, inherited secret descriptors, environment, arguments, host files, reports or presentation-role handles. Analyze whether the administrative client/authorized tester would need the key and how that affects the proposal.
- **No enabled synthetic qualification pending owner approval:** explicitly evaluate the cost of refusing to replace the existing technique.

For each candidate provide a threat/secret-flow diagram, fork-memory analysis, per-role exposure matrix, assumptions, provenance requirements, exact owner approvals needed, and a clear **PROPOSAL ONLY / NOT AUTHORIZED** label for any replacement. Do not reopen generic Azure production credential research; **T3 keeps production administration disabled**. No production credential retrieval, secret generation, exposure or inspection is authorized.

### 6. Proposed identity-contract wording

Draft narrow, **unapproved** contract text for S/G identity separation and finite root bootstrap, without numeric UID/GID allocation. Distinguish A1/T4's permission to investigate from any future permission to amend identity validation or deploy. Identify exact future source-level conflicts and candidate tests only as planning obligations.

### 7. Evidence and deliverable

Read only relevant repository source, prior WP07 reports/runbooks, and authoritative public Linux documentation if needed. Clearly separate **CONFIRMED**, **SOURCE-SUPPORTED**, **INFERRED**, **UNPROVEN**, **UNAVAILABLE**, and **OWNER DECISION REQUIRED**. Do not imply target Linux behavior, image provenance, timing or integrity were empirically verified.

Return, in order:

1. Baseline and preservation observations.
2. Evidence ledger with source paths/lines.
3. Proposed exact startup state machine and timeout policy.
4. Creation-order, sessions, identity, status-collector and **fork/exec FD matrices**.
5. Startup message schema, validation and failure table.
6. Synthetic-only credential alternatives and memory-exposure comparison.
7. Proposed S/G contract wording, with explicit unapproved decisions.
8. Remaining design blockers vs later Linux PID 1 qualification obligations, including image/runtime provenance.
9. **DESIGN CLOSED (PLANNING ONLY)** or **DESIGN NOT CLOSED**, justified; smallest next owner decision/authority.
10. `STOP — OWNER REVIEW REQUIRED`.

### 8. Absolute prohibitions

**Do not** modify or create repository files; change Git state; allocate identities; implement code; change the credential mechanism; inspect, create or use credentials; run builds/tests/Docker; run Azure commands; deploy; change GitHub; create issues/PRs; activate administration, TLS or Worker; or claim runtime qualification. No implementation allowlist is granted. If a requirement cannot be resolved read-only, document the blocker and stop.
