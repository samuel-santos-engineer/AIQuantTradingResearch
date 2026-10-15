# Codex Authority — Release 1.14 WP07-P2: Runtime Integration Architecture and Dependency Inventory

## Owner authorization and purpose
You are authorized to perform a **read-only, planning-only investigation** of the next safe runtime integration increment following merged PR #331. Do not implement the runtime, edit files, run builds/tests, allocate identities, deploy, commit, push, open PRs, or merge. Report findings and stop for owner review.

## Verified starting point
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- PR #331 merged; `main` merge commit `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8`.
- PR #331 added exactly six paths: `container/admin_lifecycle_protocol.py`, `python/presentation/test_admin_lifecycle_protocol.py`, `src/AIQuantTradingResearch.AdminDiagnostics/AdminLifecycleProtocol.cs`, `src/AIQuantTradingResearch.AdminDiagnostics/AIQuantTradingResearch.AdminDiagnostics.csproj`, `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminLifecycleProtocolTests.cs`, `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AIQuantTradingResearch.AdminDiagnostics.Tests.csproj`.
- P1F reported 24 Python and 50 .NET focused tests passing, zero build warnings/errors. These are offline conformance results, not live-kernel evidence.
- The original local checkout may be intentionally dirty, on `draft/release-1.14-wp07-partial-public-disabled` at `67893f51931ee183f71050a83b81127e6e648b87`. Preserve it without checkout, reset, stash, clean, file writes, or ref updates. Verify actual starting state rather than assuming.
- `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification, and public Worker activation remain **NO-GO**. Disabled Option A and T3 remain unchanged.

## Non-negotiable architectural contracts
- **D1:** guardian must positively obtain actual bounded kernel wait status for its direct agent child and require exit 0. **Strict D2:** supervisor independently obtains positive bounded kernel wait status for its direct guardian child and requires exit 0. Router exit 0 only after host disposal. **T2:** PID1 adopted-child drain and terminal child absence occur only after mandatory positive waits; `ECHILD` or EOF never substitute for clean-exit proof. **D3:** namespace/container teardown is failure containment only.
- **A1/A2/A3:** unprivileged supervisor remains PID1; router UID/GID 1100; supervisor independently validates guardian exit; privileged managed startup forbidden.
- **T1–T8:** finite root PID1 pre-fork; supervisor S=1200/1200 and guardian G=1300/1300 are distinct from agent/UI=1000 and router=1100, subject to base-image collision verification before allocation; all real/effective/saved IDs permanently dropped, supplementary groups empty, capabilities absent; no retained root helper or cross-UID signaling. Guardian is direct parent of agent, supervisor direct parent of guardian/router. Trusted/presentation sessions isolated, including SIGCONT same-session considerations. No secret-bearing parent memory at fork. Router generates disposable synthetic credential only locally after isolation (T7); no external authenticated-client or production credential ingress.
- **T6:** privileged bootstrap phase <=5 seconds; total startup <=30 seconds. Shutdown shared absolute `CLOCK_MONOTONIC` deadline <=4 seconds, Docker stop grace 5 seconds. Never confuse startup and shutdown clocks.
- **T9–T11 and C1:** frozen `aiq-admin-lifecycle-v1` 64-byte big-endian protocol, exact roles/messages/outcomes, sequence/launch/PID/deadline binding, descriptor lifetime; temporary agent metadata FD4 validated once and closed before agent exec; launch gate permits `A_EXEC` guardian role04 and `A_EXEC_ACK` agent-bootstrap role07 only on that bootstrap channel. ACK proves gate receipt, not exec or readiness. Bootstrap roles forbidden on runtime control channels. No protocol record, injected observation, EOF, or terminal absence qualifies an actual positive kernel wait.
- Expected FD roles: supervisor 3 SR, 4 SG, 5 metadata; guardian 3 SG, 4 launch gate, 5 metadata; router 3 credential read, 4 RA, 5 SR, 6 metadata; agent 3 RA, temporary 4 metadata; UI none. Review descriptor creation, close-on-exec, leaks and ownership without changing them.

## Authorized read-only tasks
1. Record branch, HEAD, remote `main` SHA, full `git status` and ref state before/after. Use non-mutating inspection only; do not fetch if it would update local refs. GitHub API reads are allowed. Report whether source being inspected is from current `main`, old dirty checkout, or both.
2. Inventory current entrypoint, Dockerfiles, scripts, Python process/lifecycle code, .NET router configuration/lifecycle, UI launch, tests and packaging. Cite **exact file paths and line numbers** for each relevant finding. Distinguish tracked code on merged `main` from untracked local files.
3. Construct a process tree and startup/shutdown state diagrams grounded in inspected source and approved topology. Identify present vs missing components, including root bootstrap, supervisor, guardian, router bootstrap, agent bootstrap, agent, UI, kernel-wait owners, adopted-child drain and terminal proof ledger.
4. Trace exact protocol consumers and integration seams: which process owns each channel/FD, record sequence, launch/PID/deadline binding, readiness gate, signal path, EOF policy, host disposal, and failure propagation. Call out existing ASCII framing, PID1 assumptions, and unbound waiter implementations if still present.
5. Analyze security and operational gaps: UID/GID collision checks; `setgroups`/`setresgid`/`setresuid`, capabilities and sessions; root pre-fork ordering; credential lifecycle; no public listener before `R_EXPOSE`; source/native build provenance; Linux clock and wait interop; cleanup within four seconds; disabled-mode regression; image/runtime provenance and qualification evidence.
6. Propose a **dependency-ordered, smallest-risk implementation plan** broken into bounded increments with exact proposed path allowlists, newly required files, tests, objective acceptance criteria, and explicit owner decisions. Separate pure offline/bootstrap scaffolding from runtime wiring, Docker identity wiring, and later live qualification. Do not treat proposed allowlists as authorization.
7. Identify unresolved contract ambiguities or blockers requiring owner decisions; do not invent protocol extensions, alter frozen wire contract, weaken D1/D2/T2, or quietly broaden scope.

## Required report
- Baseline/preservation table with source provenance, initial/final status and any limits of preservation evidence.
- File/line-based inventory and current-vs-approved architecture gap matrix.
- Detailed trust, parentage, session, FD, message and deadline ownership mapping.
- Ordered minimal next implementation increments; for the **first** increment give exact file allowlist and offline test plan, and clearly label it **proposal only**.
- Risk register with severity, evidence, and explicit stop/owner-decision triggers.
- Confirmation that no files, refs, tests/build outputs, GitHub state, Docker, Azure or production configuration were mutated.
- Final verdict exactly one of:
  - `WP07-P2 ARCHITECTURE INVENTORY COMPLETE — OWNER REVIEW REQUIRED`
  - `WP07-P2 BLOCKED — OWNER REVIEW REQUIRED`

**STOP after reporting. No runtime integration, PR or merge is authorized by this file.**
