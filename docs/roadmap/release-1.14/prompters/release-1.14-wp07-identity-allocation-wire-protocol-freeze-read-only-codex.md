# Codex Authority — Release 1.14 WP07
## Numeric Identity Proposal and Wire Protocol Freeze — READ-ONLY

**Authority type:** bounded investigation and specification only. **No implementation, tests, builds, Docker, networking, GitHub or Azure actions.**

### Owner-approved context

A1/A2/A3, D1/strict D2/D3, and T1–T7 remain binding. T5 approves *symbolic* mutually distinct unprivileged supervisor S and guardian G identities, separate from router UID/GID 1100 and presentation UID/GID 1000, **not** numeric allocation. T6 approves the bounded startup design: absolute `CLOCK_MONOTONIC` deadlines of `t0+5s` for privileged phase and `t0+30s` for complete startup, fixed records and readiness gates. T7 approves router-local disposable synthetic credential generation only after verified process separation, strictly for lifecycle and unauthenticated-denial qualification; no authenticated-client testing or production credential ingress.

Preserve the shared four-second monotonic shutdown deadline, five-second Docker stop grace, disabled Option A, D1 trusted guardian positive bound agent exit-0 wait, strict D2 supervisor positive bound guardian exit-0 wait, router positive exit-0/disposal, and T2 adopted-child terminal absence **only after** positive proofs. D3 is failure containment, not proof.

Protected reported baseline: branch `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`, intentional dirty working tree. Verify and record initial/final branch, HEAD, full status; do not alter them. Do not reset, clean, checkout, stash, commit or push. If baseline differs, STOP and report.

`PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation remain NO-GO. Production administration stays disabled under T3.

### Task 1 — Propose numeric S/G identity assignments (do not allocate)

Read existing Dockerfile, bootstrap, identity checks and relevant documented contracts. Inventory all existing UID/GID assignments and potential conflicts, including group memberships and file ownership. Recommend **candidate** numeric UID/GID values for S and G that are mutually distinct and distinct from 1000/1100 and existing image accounts. Explain selection rationale, group IDs, required immutable trusted-path ownership, no supplementary groups, all real/effective/saved IDs, no capabilities and no post-drop privilege recovery. Clearly mark **PROPOSED — OWNER APPROVAL REQUIRED**. Do not edit Dockerfile, create accounts, allocate IDs or amend the identity contract.

### Task 2 — Freeze a complete proposed protocol, not just a sketch

Specify a versioned, cross-language Python/.NET wire contract for **every** bootstrap metadata, startup, control, lifecycle, shutdown and result message required by the approved graph. The prior 64-byte network-byte-order candidate had: magic (4), version/sender role/message type/flags (4), launch ID (16), sequence (4), sender PID (4), related child PID (4), startup deadline nanoseconds (8), shutdown deadline nanoseconds (8), reserved zero (12). Retain or explicitly justify proposed refinements; do not silently change approved T6 behavior. Freeze:

- Magic bytes, version, exact field offsets, signedness, endian, flags, reserved-byte rules, valid ranges, clock epoch and overflow rules; ensure byte counts and sample vectors reconcile.
- Enumerated numeric roles, message types and legal sender/receiver pairs; bootstrap metadata types/count/order binding both router and guardian child PIDs, guardian's agent PID, launch identifier and direct-parent provenance.
- Sequence-number scope per direction, allowed counts, one-time transitions, partial-read and EOF handling, replay/duplicate/reorder detection, invalid records and sticky failure semantics. Clarify what can and cannot be authenticated by private FD possession.
- Exact startup FSM and gates: root pre-fork, agent blocked before exec, S/G permanent drops and post-exec hardening, bound guardian/router readiness, router-local credential generation **only after all required forks/separation**, agent exec, agent ready, UI ready, listener exposure and RUNNING. No premature readiness or restart.
- Exact launch-gate acknowledgment semantics and metadata ownership; exact role-specific FD numbers/ownership at each fork/exec, closure of duplicate endpoints, and no supervisory or credential FD inherited by UI.
- Monotonic-clock interoperability across Python and .NET, deadline encoding and propagation; privileged `t0+5s`, startup `t0+30s`, and a **new** shared shutdown `now+4s` only on defined shutdown initiation. Never reset a phase deadline.
- Shutdown record types and exact encoding for agent/guardian/router positive normal exit 0, nonzero, signal death, wait errors, EOF and lifecycle ACK. Distinguish self-reports from kernel wait evidence; required-child `ECHILD` fails, terminal child absence is T2-only.
- Explicit canonical byte-vector examples and malformed/negative vectors suitable for future offline cross-language tests (do **not** execute tests).

If a protocol ambiguity cannot be resolved from approved owner intent without a new policy choice, mark it **OWNER DECISION REQUIRED**, offer bounded alternatives and do not present it as frozen.

### Task 3 — Credential and security invariants

Describe router-only disposable synthetic credential generation after separation, including exact guard before generation, permanent router UID 1100 drop, router-local pipe/read FD, writer closure, transient memory cleanup, digest sensitivity and absence of credential in root/S/G/agent/UI, arguments, environment, files and diagnostics. No external handoff, authenticated-client testing or production ingress. Specify trusted immutable dependencies, separate presentation session (agent and UI), nondumpability, signal/ptrace restrictions, FD confinement and fail-closed behavior. Separate design arguments from target-kernel empirical proof.

### Task 4 — Implementation prerequisites and owner decision sheet

Produce a concise evidence ledger with **CONFIRMED / SOURCE-SUPPORTED / INFERRED / UNPROVEN / UNAVAILABLE / OWNER DECISION REQUIRED**. List exact proposed source/test paths only as **future change inventory, not an allowlist**. Identify unresolved image/runtime-stage digests, platform/kernel and native build provenance. Produce a decision sheet with proposed S/G numeric assignments and proposed wire specification version, clearly distinguishing approved T5–T7 principles from unapproved numeric constants or protocol refinements. Specify smallest subsequent authority; do not grant it.

### Hard STOP conditions

No source/document mutation, identity allocation, package installation, builds, tests, Docker, runtime qualification, network access, deployment, GitHub issues/PRs/branches/commits, Azure changes, credentials or privileged managed startup. No production administration or public activation. No arbitrary wait evidence, no cross-UID signaling, no secret-bearing pre-fork root memory. If a necessary design change conflicts with A1/A2/A3, D1/D2/D3 or T1–T7, STOP for owner decision.

### Required output

1. Baseline and preservation report (status-level evidence caveat).
2. Identity inventory and candidate S/G UID/GID matrix, conflicts and owner approval needed.
3. Complete proposed wire protocol with byte layout, constants, record catalog, transitions, canonical/negative vectors and unresolved decisions.
4. Process/session/FD/credential invariants and failure matrix.
5. Evidence ledger, qualification prerequisites and explicit owner decision sheet.
6. Final verdict: **`SPECIFICATION READY FOR OWNER FREEZE`** only if complete and internally consistent, otherwise **`SPECIFICATION BLOCKED`**, listing precise gaps.

**STOP — OWNER REVIEW REQUIRED.** No implementation authorization is implied by either verdict.
