# Codex Authority — Release 1.14 WP07: Strict Guardian Exit-Status Mechanism Feasibility

**Authority class:** READ-ONLY / PLANNING ONLY. **No implementation or runtime qualification.**

## Owner-approved policy baseline (binding)

- **D1 — CONDITIONAL YES:** A trusted guardian's direct kernel-wait attestation may establish the bound agent's exit code 0, only after guardian integrity, provenance, identity, and descriptor confinement are qualified.
- **D2 — STRICT:** Clean shutdown MUST independently establish the guardian's own **normal exit code 0**. A status report, channel EOF, pidfd readiness, disappearance, or an `ECHILD` result does not substitute.
- **D3 — CONDITIONAL YES:** After terminal shutdown failure, bounded PID 1 exit and container-runtime teardown may provide **failure containment only**, subject to qualification; they never establish clean PASS.

These decisions were explicitly made by the owner. Do not reopen or weaken them. They are policy approvals, **not** design closure or implementation permission.

## Fixed baseline and gates

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Existing dirty and untracked working-tree content MUST remain untouched. Record initial and final `git status --porcelain=v1` and HEAD; do not claim content-hash preservation unless actually measured.
- Router remains **PID 1, UID/GID 1100:1100**; agent, UI, and proposed guardian **UID/GID 1000:1000**; finite root bootstrap only. No persistent privileged helper, cross-UID signals, capability expansion, credential exposure, or broader process/mount permissions.
- Disabled Option A remains unchanged. Enabled-mode clean shutdown requires verified lifecycle ACK, EOF, **agent normal exit 0**, **guardian normal exit 0** if added, router/host drain and disposal, all within **one monotonic four-second coordinated deadline**. Docker stop grace remains five seconds.
- `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO**.

## Observed evidence and limits

- Owner-executed sanitized WP07 qualification recorded .NET runtime **10.0.12**, application `waitpid` returning **`ECHILD`**, `AgentTerminationUnverified`, container exit **64**, no OOM. Neither agent exit 0 nor the actual consumer of its status was proven.
- Public `dotnet/runtime` **v10.0.12** `pal_signal.c` PID 1 signal/reaping path can compete with application waiters; `ProcessWaitState.Unix.cs` has separate managed child-registration/reaping behavior. Public tagged source is not proven byte-identical to the observed floating-tag image runtime.
- Current bootstrap forks the agent before execing the .NET router; the current waiter calls `waitpid(-1, WNOHANG)` without binding the agent PID. The agent closes its lifecycle channel before final process exit. A guardian would be the agent's actual parent, but **the router would still be the guardian's parent** and subject to PID 1 runtime reaping interactions.
- Useful source anchors: `container/admin_diagnostics_bootstrap.py`, `container/presentation_lifecycle_agent.py`, `src/AIQuantTradingResearch.AdminDiagnostics/Program.cs`, `src/AIQuantTradingResearch.AdminDiagnostics/PresentationLifecycle.cs`, `Dockerfile`, `global.json`; official sources `https://github.com/dotnet/runtime/blob/v10.0.12/src/native/libs/System.Native/pal_signal.c` and `https://github.com/dotnet/runtime/blob/v10.0.12/src/libraries/System.Diagnostics.Process/src/System/Diagnostics/ProcessWaitState.Unix.cs`.

## Exact research question

**Can the .NET 10 router, running as Linux PID 1 under UID 1100, independently obtain and retain the actual kernel normal-exit-code-0 status of its UID 1000 guardian child, without a race against .NET runtime reaping, while preserving the fixed process/security contract?**

Answer **FEASIBLE** only when a specific supported mechanism and proof path are identified. Otherwise conclude **NOT ESTABLISHED**; do not confuse theoretical plausibility with demonstrated ownership.

## Authorized work

1. **Read-only baseline check.** Inspect only relevant source, prior sanitized evidence, official versioned runtime documentation/source already accessible through permitted read-only channels, and local metadata if present. Distinguish observed binaries, public tag source, and hypotheses. Do not require internet access; mark unavailable evidence clearly.
2. **Map guardian creation and status ownership.** For each candidate, identify *who creates guardian*, *who owns waitable kernel status*, whether .NET PID 1 native or managed reapers can consume it, and exactly which actor observes its **normal exit code 0**. Include ordering around bootstrap fork/exec, runtime startup, SIGCHLD, and PID reuse.
3. **Compare mechanisms:**
   - Guardian created through supported .NET `System.Diagnostics.Process` APIs after router startup, including whether its registered wait state captures/caches its status under PID 1 and whether the guardian can run as UID 1000 without prohibited privilege escalation or new persistent root privileges.
   - Guardian forked by finite root bootstrap and handed off through PID, pidfd, FD, or other documented mechanism; explicitly test the claim that later `Process.GetProcessById` can adopt status ownership (do not assume it can).
   - Child-specific `waitpid` / `waitid(P_PIDFD)` / `WNOWAIT`, and pidfd polling: identify races with competing reapers and distinguish identity/termination readiness from exit-code proof.
   - Supported runtime configuration or signal ownership mechanisms, if any; do not propose unsupported internal hooks, monkey-patching, signal-handler overrides, or runtime forks as acceptable without separate owner approval.
   - Any *simpler* topology preserving router PID 1 and fixed UIDs, with a complete independent guardian-status proof; reject moving PID 1 or silently relaxing D2.
4. **Threat and failure review.** For each viable-looking mechanism, analyze reaper races, `ECHILD`, `EINTR`, nonzero/signal exits, guardian crash before/after agent report, simultaneous shutdown/cancellation, hangs, orphan adoption, credential/FD inheritance, UID boundaries, and remaining-time exhaustion. Do not treat `waitpid` success for an arbitrary child as bound guardian proof.
5. **Provenance requirements.** Identify the minimum owner-supplied image digest/runtime binary metadata needed to tie actual container behavior to inspected source; do not pull images or run Docker.
6. **Narrow next phase.** If FEASIBLE, propose a **design-closure-only** follow-up specifying exact proof obligations and proposed test strategy, without authorizing edits. If NOT ESTABLISHED, identify the smallest missing evidence or explicit contract conflict and stop. No implementation allowlist is authorized here.

## Required deliverable (response only; no repository artifacts)

Produce:

1. Baseline and preservation report (initial/final status comparison; accurate limitations).
2. Evidence ledger: **CONFIRMED / SOURCE-SUPPORTED / INFERRED / UNPROVEN / UNAVAILABLE**, with exact source paths/line anchors where available.
3. Process/parent/wait-ownership topology and timeline.
4. Mechanism comparison table covering actual guardian exit-0 evidence, competing reapers, UID/PID1 compatibility, and failure cases.
5. D1/D2/D3 compliance assessment, clearly identifying D2's strict requirement.
6. Minimal image/runtime provenance requirements and any owner-only prerequisites.
7. Explicit **FEASIBLE** or **NOT ESTABLISHED** verdict with evidence threshold, unresolved blockers, and next bounded authority recommendation.
8. Reaffirm NO-GO release gates and **STOP — owner review required**.

## Prohibited actions / STOP conditions

- **No file edits** (including reports in the repository), code generation into repo, build, tests, Docker, Azure, network restore, qualification run, deployment, branch creation, commit, push, rebase, reset, or cleanup of dirty files.
- No automatic owner decisions, no acceptance of guardian disappearance/EOF/pidfd readiness as exit 0, no weakened agent exit proof, no additional root residency, no cross-UID signaling, no privileged process supervisor, no public Worker activation.
- Stop if expected baseline differs, required inspection would mutate state, mechanism relies on unsupported internals, or evidence cannot substantiate independent guardian exit-code ownership. Record uncertainty rather than fabricating a solution.

**This authority ends with analysis only. Implementation remains DESIGN NOT CLOSED and NO-GO.**
