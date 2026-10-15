# Codex Authority — Release 1.14 WP07
## T5–T7 Implementation Readiness and Qualification Planning (READ-ONLY)

**Authority class:** Planning and inspection only. **No implementation authority.**
**Owner decisions:** T5, T6 and T7 APPROVED in conversation. Preserve A1/A2/A3, D1/strict D2/D3 and T1–T4.

## Mission
Inspect the current repository and produce a precise, bounded, file-by-file implementation-readiness and empirical-qualification plan for the approved distinct-identity supervisor/guardian topology, startup protocol and qualification-only synthetic credential boundary. Identify blockers and separate every proposed change from any authorized action. **Do not modify anything.**

## Protected baseline and first checks
- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Working tree is intentionally dirty. Capture complete `git status --porcelain=v1` and branch/HEAD at start and end; compare. Do not assume cleanliness or alter tracked/untracked files.
- Use read-only commands only. If baseline differs, STOP and report; do not repair it.
- Report status-level preservation honestly; do not claim hash-level preservation unless actually checked.

## Binding architecture and owner decisions
- **T5:** Symbolic unprivileged supervisor S and guardian G, mutually distinct and distinct from router UID/GID 1100 and presentation UID/GID 1000. No numeric S/G assignment approved. Root PID 1 performs finite unmanaged pre-fork, then permanently drops to S and execs supervisor *in PID 1*. Router is supervisor's direct child; guardian is supervisor's direct child; agent is guardian's direct child; UI is agent's child. Guardian bootstrap forks agent before dropping to G. No resident privileged helper or privileged managed startup.
- **T6:** Bounded startup protocol: root bootstrap records `t0=CLOCK_MONOTONIC`; privileged phase deadline `t0+5s`; total startup deadline `t0+30s`; absolute deadlines, no reset/restart. Distinct agent-exec and UI-launch gates; hardened trusted roles and channel binding precede release. Fixed 64-byte network-order records, role/launch/sequence/PID/deadline validation, exact framing and bounded record counts. Use the prior design report as proposal-level specification; identify any inconsistencies rather than silently inventing resolution.
- **T7:** Disposable synthetic credential generated solely in isolated router bootstrap branch **after process separation**. Never in root/supervisor/guardian/agent/UI memory, environment, arguments, files, logs or inherited descriptors. Qualification scope limited to lifecycle and unauthenticated-denial checks. No authenticated-client qualification or production credential ingress.
- Router executes as UID/GID 1100 child of PID 1; current PID-1 router validation conflict is a **future planned change**, not permission to edit.
- D1: Guardian's bound positive kernel wait for actual agent normal exit 0, integrity-protected against hostile UID 1000. Strict D2: supervisor's independent bound positive kernel wait for guardian normal exit 0; never substitute EOF, ECHILD, PID disappearance or self-report. T2: adopted-descendant terminal absence is closure evidence **only after** mandatory positive status proofs; terminal ECHILD never proves exit 0. D3: teardown only failure containment.
- Preserve four-second shared `CLOCK_MONOTONIC` shutdown deadline, five-second Docker stop grace, disabled Option A, and sticky FAIL semantics. No clean PASS on timeout, unknown descendants, missing proof, compromised trust boundary or supervisor death.
- Session isolation includes agent and UI; guard against same-session SIGCONT exception. All roles have permanently dropped real/effective/saved UID/GID, empty supplementary groups and no capabilities; trusted dependencies immutable; role-specific FD allowlists; no unauthorized cross-UID signaling.
- T3 production administration disabled. `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation remain NO-GO.

## Read-only work packages
1. **Source inventory:** Identify exact paths, entry points, current behavior, and expected future deltas for root bootstrap, supervisor, guardian, agent/UI, router validation, configuration, container entrypoint and tests. Distinguish confirmed code from inferred plans. Provide line references where possible.
2. **Identity/TCB prerequisites:** Map roles, parent/wait owner, UID/GID symbolic assignments, session and capability policy, root-owned immutable interpreter/library/import provenance, nondumpability timing, post-exec hardening and host/kernel assumptions. List numeric identity allocation as a separate owner gate.
3. **Startup conformance matrix:** Specify states, actor, allowed incoming/outgoing 64-byte messages, launch binding, exact FD ownership/closure, readiness prerequisites, privileged and overall absolute deadline checks, invalid-transition sticky FAIL behavior, and partial startup cleanup. Flag unresolved ambiguities rather than implementing a guess.
4. **Synthetic credential plan:** Trace credential creation exclusively within the router branch, temporary pipe and FD 3 handoff, memory lifetime and sanitization, permitted unauthenticated-denial qualification, and prohibited external credential handoff. Explain how no earlier fork can inherit secret memory. No real secrets.
5. **Shutdown proof plan:** Map each D1, strict D2, router normal exit, host disposal, T2 adoption/drain, ACK/EOF and shared deadline proof to its owning process, kernel primitive, validation and failure condition. Explicitly distinguish positive wait status from terminal absence.
6. **Qualification plan, NOT execution:** Define isolated future tests for Linux process lineage, cross-UID tracing/signaling and same-session SIGCONT, FD confinement, credential inheritance, malformed/replayed records, startup deadline boundaries, failure stickiness, bound positive waits, orphan/adoption behavior, hostile UID 1000 actions, four-second shutdown and disabled-mode invariance. Include prerequisites and acceptance criteria; do not assert passes.
7. **Provenance and sequencing:** Identify missing observed image/runtime-stage digests, Linux/kernel/platform metadata and native build/source provenance. Propose minimal reviewable implementation work packages and STOP gates, with explicit dependency ordering and no authorization implied.

## Hard prohibitions
- NO source, test, documentation, configuration or generated-file modifications in the repository.
- NO commits, branch changes, checkout, stash, reset, clean, push, PRs, issues or other GitHub actions.
- NO builds, tests, Docker/container execution, Azure calls, deployment, networking or runtime qualification.
- NO numeric S/G identity allocation or identity-contract amendment.
- NO credential replacement beyond planning, no external authenticated-client test, no production ingress design reopening.
- NO activation of public Worker or administration; NO changes to disabled Option A.
- If an essential fact cannot be read without a prohibited action, label **UNAVAILABLE** and STOP on that dependency.

## Required report
Deliver a single owner-review report containing:
1. Baseline start/end and preservation evidence.
2. Evidence ledger: **CONFIRMED / SOURCE-SUPPORTED / INFERRED / UNPROVEN / UNAVAILABLE / OWNER DECISION REQUIRED**.
3. Exact file-by-file future change inventory with prerequisites and risk ranking.
4. Identity/privilege/session/FD matrix and startup state/record conformance matrix.
5. Synthetic credential data-flow and scope exclusions.
6. Shutdown proof ownership and failure matrix.
7. Planned qualification tests with acceptance criteria and explicit *NOT RUN* status.
8. Sequenced smallest implementation work packages and owner authorization gates; unresolved blockers.
9. One conclusion: **IMPLEMENTATION PLAN READY — OWNER AUTHORIZATION REQUIRED** or **IMPLEMENTATION PLAN BLOCKED**.
10. Final line: **STOP — OWNER REVIEW REQUIRED.**

This authority is for research and planning only. No implementation or qualification is approved.
