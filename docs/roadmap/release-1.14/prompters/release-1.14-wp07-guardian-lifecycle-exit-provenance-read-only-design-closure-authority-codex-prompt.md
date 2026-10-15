# Codex Authority — Release 1.14 WP07: Guardian Lifecycle and Exit-Provenance Design Closure (READ-ONLY)

## Authority and objective
Owner authorizes **read-only design analysis only** to resolve whether an unprivileged exit guardian can provide trustworthy agent exit-status evidence without merely relocating the unverified-exit problem. **No implementation is authorized.** Produce a concrete, auditable state-machine and protocol proposal, or explicitly conclude that the design is not ready.

## Frozen baseline and mandatory preflight
- Repository: `C:\projects\github\AIQuantTradingResearch`.
- Required branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Required HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Working tree is intentionally dirty. Record branch, HEAD, and initial Git status; **never** reset, stash, checkout, clean, stage, commit, push, or change tracked/untracked files. Compare final Git status with initial status; report preservation limitations honestly.
- If branch/HEAD differs, evidence is missing, or a needed operation would mutate state, **STOP** and request owner direction.
- Use only read-only repository inspection and existing evidence. No Docker, builds, tests, network-dependent commands, Azure, package restoration, scripts, probes, deployment, or qualification reruns.

## Established incident evidence — do not overclaim
- First host run exited 64 after `AgentTerminationUnverified`; second host run also exited 64, without OOM or forced-stop evidence.
- Second report: `.NET` runtime `10.0.12`; `AIQ_ADMIN_WAIT=TARGET_ANY_RETURN_ERROR_ECHILD`, followed by `AgentTerminationUnverified`, `AIQ_ADMIN_SHUTDOWN_FINAL=AgentTerminationUnverified`, and `AIQ_ADMIN_FAILURE=Shutdown`.
- UI `Terminated`/`StopAcknowledged`, router `StopAcknowledged`, and channel EOF occurred before `ECHILD`. Those events **do not** prove the agent exited 0.
- A competing .NET PID 1 reaper is a **supported hypothesis**, not proven as the actual consumer of the status. Do not assert otherwise without evidence.

## Inviolable architectural and security constraints
- Enabled mode: .NET administrative router remains **PID 1, UID/GID 1100:1100**; presentation agent and Streamlit remain **UID/GID 1000:1000**. A finite root bootstrap is permitted only as previously approved; **no resident privileged helper**.
- No cross-UID signaling, new privileged operations, ambient capabilities, Docker socket, secret leakage, credential exposure in `docker inspect`/arguments/logs/host files, or public Worker/SQLite/private provider access.
- Preserve existing credential/FD confinement requirements, loopback-only UI, local unauthenticated administrative denial, and production TLS NO-GO.
- Preserve enabled shutdown's **shared four-second coordination deadline** and **five-second Docker stop grace**. No relaxation of agent exit-0 proof. `ECHILD`, EOF, PID disappearance, guardian self-report, and channel acknowledgment alone never establish exit 0.
- Disabled Option A remains **unchanged**: Streamlit as PID 1 on port 8501, UID/GID 1000:1000; generic `/admin` UI fallback permitted.
- `PUBLIC_RUNTIME_VERIFIED=False`; administrative API, Azure passive qualification, and public Worker activation remain NO-GO.

## Required design work

### A. Exact process and descriptor topology
Document the existing bootstrap, router, agent, and UI parent-child relationships, relevant FDs, UID transitions, protocol channels, and who is authorized to call `wait*`. Then draw a **candidate** guardian topology and distinguish OS parentage from logical supervision. Address the fact that the router as PID 1 could reap the guardian, even if it cannot reap the guardian's living child. Do not assume any runtime behavior that has not been verified.

### B. Guardian exit-status provenance
Specify precisely how a guardian binds to the intended agent PID and collects the **actual kernel-reported** wait status (including exit 0, nonzero exit, signal death, `ECHILD`, `EINTR`, and other errors). Define how the router authenticates the guardian's report using bootstrap-established FD/channel provenance without treating an unverified process statement as independent exit proof. Explain trust assumptions and whether they are sufficient under the project's threat model. Explicitly distinguish integrity/authenticity of a message from independently observed kernel exit status.

### C. Guardian lifecycle closure — central blocker
Resolve or clearly mark unresolved: guardian crashes before status; after reaping but before sending status; after sending status but before exiting; hangs; channel closes early; status duplicated, truncated, reordered, or stale; router cancellation; guardian orphaning; agent/UI orphaning; simultaneous failures; and deadline exhaustion. Specify who observes guardian termination, whether guardian exit code must be proven, how .NET PID 1 reaping affects that proof, and why the proposed closure does not recursively introduce another unverified helper. If no safe bounded solution exists, explicitly declare **DESIGN NOT CLOSED**.

### D. Protocol and state machine
Provide a finite state machine for normal shutdown and each failure branch. Define bounded messages and framing, per-message semantics, sender/receiver ownership, channel EOF meaning, validation of agent identity, and all STOP/FAIL/NOT_QUALIFIED outcomes. Show how a single shared monotonic deadline is enforced without unbounded waits or best-effort success. No new arbitrary logs or secret-bearing diagnostics.

### E. Failure cleanup and process-tree containment
Analyze how remaining agent/UI/guardian processes are contained and terminated without cross-UID signaling or a resident root helper. Distinguish Docker's container-wide stop behavior from the application proving a clean exit. Describe how orphan processes, PID reuse, zombie handling, and a guardian's premature death are treated. If reliable cleanup cannot be demonstrated within existing constraints, identify the exact blocker rather than proposing privilege expansion.

### F. Compare alternatives and choose conditionally
Compare at least:
1. Dedicated UID 1000 guardian parent of agent.
2. Existing agent as a direct child/status owner or restructured supervision without a new guardian (identify whose exit is actually being proven).
3. Agent-specific PID/pidfd evidence combined with existing parentage (explain why liveness is not exit-status proof).
4. Exclusive reaping inside .NET PID 1, if supported (do not assume managed process adoption).
Evaluate each for exit-0 proof, failure closure, security, complexity, compatibility with router PID 1, and deadline. Recommend an option **only if** its proof obligations close; otherwise recommend further investigation with exact missing evidence.

### G. Verification and implementation planning — proposals only
If and only if the design closes, propose a minimal literal-path file allowlist for a **future separately authorized** implementation; justify every path, including any new file. List deterministic offline tests and later separately approved Linux PID 1 tests covering real wait status, competing reaping, `ECHILD`, `EINTR`, nonzero/signal exit, EOF races, identity mismatch, malformed messages, guardian failure, orphan handling, FD/credential confinement, deadline, and disabled-mode invariance. No implementation or test execution now.

## Deliverable format
Return a concise owner-review report with:
1. Baseline and read-only preservation observations.
2. Evidence ledger: **CONFIRMED / SUPPORTED / UNPROVEN**.
3. Existing and candidate process/FD topology.
4. Explicit state machine and guardian-exit closure proof or blocker.
5. Failure-mode table and comparison matrix.
6. Security and timing analysis.
7. **DESIGN CLOSED** or **DESIGN NOT CLOSED**, with conditions and exact unanswered questions.
8. Proposed next *narrow* authority, if justified, without executing it.
9. Explicit release NO-GO gates and STOP for owner review.

## Absolute STOP conditions
STOP before any source edit, test/build execution, Docker command, Git mutation, privilege change, network operation, Azure operation, Worker activation, credential inspection, protocol weakening, or expansion of this read-only scope. Do not treat `ECHILD` or guardian reports alone as proof of agent exit 0. Do not invent runtime observations.

**STOP — owner review required.**
