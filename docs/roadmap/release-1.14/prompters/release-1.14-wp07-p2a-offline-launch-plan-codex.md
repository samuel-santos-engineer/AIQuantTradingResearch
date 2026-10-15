# Codex Authority — Release 1.14 WP07-P2A: Offline Launch-Plan Model

## Owner authorization and binding clarification
The owner explicitly APPROVED **P2-C2**. Preserve T11/C1 without altering the frozen `aiq-admin-lifecycle-v1` protocol: **agent metadata FD4 survives agent exec, is validated exactly once by the executed agent, then closes**; the **guardian–agent bootstrap launch gate closes before agent exec** after its permitted `A_EXEC` (guardian role 04) / `A_EXEC_ACK` (agent-bootstrap role 07) exchange. These are separate channels. Do not confuse the guardian's FD4 launch-gate endpoint with the agent's FD4 metadata endpoint; FD numbering is per-process. `A_EXEC_ACK` is not evidence of exec or `A_READY`.

## Scope and starting conditions
Implement a **pure, non-executing, offline launch-plan model**, and nothing else. Before editing, verify remote `main` contains PR #331 merge `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8`, inspect current branch/HEAD and full Git status, and record the baseline. The historical protected dirty checkout is `draft/release-1.14-wp07-partial-public-disabled` at `67893f51931ee183f71050a83b81127e6e648b87`; do not reset, clean, stash, checkout or otherwise disturb it. If isolated work is necessary, use a separate worktree/temporary copy with explicit preservation checks. Do not assume stale local `main`/`origin/main` reflects remote state. Stop if the baseline cannot be established without violating preservation.

**Exact mutation allowlist — create only these two NEW files:**
1. `container/admin_lifecycle_launch_plan.py`
2. `python/presentation/test_admin_lifecycle_launch_plan.py`

Do not edit existing protocol modules, tests, csproj files, solution, Dockerfile, entrypoint, runtime, docs or GitHub state. If either allowlisted file already exists, STOP for owner review instead of overwriting it. No commits, pushes, PRs, merges or GitHub mutations under this authority.

## Required modeled architecture
Model the approved target as immutable declarative roles, relations, channel ownership, ordered milestones and validation rules, without invoking OS/runtime actions:
- Finite root PID1 secret-free pre-fork; root becomes permanent supervisor UID/GID **1200/1200**, remains PID1.
- Supervisor direct children: router UID/GID **1100/1100** and guardian UID/GID **1300/1300**. Guardian directly parents agent UID/GID **1000/1000**; agent parents UI UID/GID **1000/1000**. Numeric UID/GID allocations are **conditional on future base-image collision verification**; no account creation now. Do not assume UI and agent must have distinct numeric identities; distinguish role/process identity from UID.
- Router, guardian, supervisor and presentation sessions must be explicitly isolated to address same-session `SIGCONT`; document required session boundaries without calling `setsid` or sending signals.
- Privileged root pre-fork must contain no credential or secret-bearing parent memory. Router-local disposable synthetic qualification credential is generated only after verified separation; no production credential ingress or external authenticated-client testing.
- Permanent identity hardening: all real/effective/saved UID/GID values, supplementary groups and capabilities; no privileged managed startup, no root helper, no cross-UID signaling.
- Startup: a single absolute Linux `CLOCK_MONOTONIC` startup deadline, **5s** privileged phase and **30s** total; no per-stage resets. Startup gates: `G_BOUND`, `R_PREPARE`, `R_BOUND`, `G_RELEASE`, `A_EXEC`, `A_EXEC_ACK`, agent exec and metadata validation, `A_BIND`, `A_READY`, `START_UI`, `UI_READY`, `R_UI_READY`, `R_EXPOSE`, `R_EXPOSED`. No public listener before `R_EXPOSE`.
- Shutdown: one shared **4s** `CLOCK_MONOTONIC` deadline inside **5s** Docker stop grace; `STOP_ROUTER`/`STOP_GUARDIAN`, `STOP_UI`, `UI_STOPPED` and permitted EOF, `R_PROTOCOL_DONE`, `HOST_DISPOSED`; guardian **actual positive bound wait** for agent exit0 (D1); supervisor **independent actual positive bound waits** for guardian exit0 (strict D2) and router exit0; then PID1 adopted-child drain and terminal absence (T2). `ECHILD`, EOF, reports and injected observations cannot substitute for positive kernel evidence. D3 teardown is failure containment only.
- Preserve the merged protocol's frozen message/role catalog, strict per-direction sequences, binding, outcome ranges, C1 exception and report-vs-wait distinction. A launch plan describes obligations, not proofs that they occurred.

## Descriptor lifetime contract (must be explicit and testable)
- Supervisor: FD3 SR, FD4 SG, FD5 metadata.
- Guardian: FD3 SG, FD4 **bootstrap launch gate**, FD5 metadata.
- Router: FD3 credential read, FD4 RA, FD5 SR, FD6 metadata.
- Agent after exec: FD3 RA, **temporary FD4 metadata**; validate once then close. Agent bootstrap receives the separate launch-gate endpoint before exec, which must close before exec; the exact bootstrap FD number/duplication must not be invented where the frozen contract does not specify it.
- UI: no inherited control, credential or metadata FDs.
- Model descriptor ownership by **process and channel identity**, not global FD number. Reject leaks, collisions within one process, premature closure, metadata closed before agent exec, gate retained across exec, unvalidated metadata retention, UI inheritance and unapproved descriptor passing.

## Implementation and tests
Use simple Python standard-library-only immutable data structures and pure validation functions. Prefer a small explicit model rather than an executable orchestration framework. Tests must cover a valid canonical plan and negative cases: wrong parentage, duplicate role/process identity, invalid UID/GID or role assignments, missing session isolation, hardening prerequisite omission, credential-before-fork, role/FD collisions, C2 metadata/gate lifetime confusion, missing or reordered readiness, premature exposure, deadline reset/overflow, forbidden EOF/wait proof substitution and UI FD leakage. Explicitly distinguish validated static obligations from runtime attestation.

You may run **focused offline Python tests** only (`python -B -m unittest python.presentation.test_admin_lifecycle_launch_plan -v` or equivalent), plus read-only Git/status/hash checks. No builds of runtime, .NET changes, dependency installs, Docker, networking, Azure, credentials, real fork/exec/UID changes/signals/waits, or live qualification. Do not introduce new dependencies. If an unapproved file must be changed or the model cannot honor C2, STOP and explain.

## Evidence and stop gates
Record initial/final branch, HEAD, full status and preservation evidence; enumerate exact changed paths; report tests and any limitations. Do not claim that offline tests prove Linux kernel behavior, FD closure, PID parentage, privilege confinement, runtime deadline performance or production readiness. Leave `PUBLIC_RUNTIME_VERIFIED=False`, administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation **NO-GO**. Disabled Option A remains unchanged.

Conclude with exactly one verdict:
- `WP07-P2A OFFLINE LAUNCH PLAN COMPLETE — OWNER REVIEW REQUIRED`
- `WP07-P2A BLOCKED — OWNER REVIEW REQUIRED`

**STOP after the report. No follow-on authority is implied.**
