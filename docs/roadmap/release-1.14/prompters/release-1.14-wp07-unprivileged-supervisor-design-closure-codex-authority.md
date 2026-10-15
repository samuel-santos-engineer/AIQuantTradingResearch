# Codex Authority — Release 1.14 WP07: Unprivileged Supervisor Design Closure

**Authority type:** READ-ONLY DESIGN / PLANNING ONLY. **No implementation, testing, builds, Docker, Azure, or Git mutations.**

## 1. Owner-approved policy (binding)

The owner explicitly approved:

- **A1 YES:** Enabled-mode PID 1 may be an **unprivileged supervisor**; the .NET router remains UID/GID **1100:1100**. This amendment applies to enabled mode **only**. Disabled Option A remains unchanged.
- **A2 YES:** The supervisor independently obtains the guardian's actual **normal exit code 0** and owns the final container verdict.
- **A3 NO:** No expanded privileged managed startup.
- **D1 conditional YES:** A trusted guardian's attestation is acceptable evidence of **agent exit 0** only if the guardian actually performs a kernel wait for its bound agent and its integrity/provenance are qualified.
- **D2 strict:** Independently prove guardian normal exit code **0**; EOF, report, disappearance, pidfd readiness, and `ECHILD` do not substitute.
- **D3 conditional YES:** PID 1 exit and container teardown may contain failures if qualified, but **never** upgrade application shutdown FAIL to PASS.

Do not reinterpret these approvals as implementation authorization or relax their conditions.

## 2. Protected baseline and preservation

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- The working tree is intentionally dirty, with existing untracked files. Preserve **every** preexisting modification and untracked item. Do not clean, reset, checkout, stash, amend, commit, push, or create files.
- Read-only initial/final `git status --porcelain=v1`, branch, and HEAD checks are permitted. Record that matching status is **not** a before/after content-hash proof. Stop if the expected baseline differs or any operation would change the repository.

## 3. Current evidence and non-assumptions

Prior owner-run qualification: container runtime reported **10.0.12**, `waitpid` returned **ECHILD**, `AgentTerminationUnverified`, container exit **64**, no OOM; **agent exit 0 was not established**. Public .NET `v10.0.12` source supports a PID 1 reaping race, but neither the actual consumer of that status nor exact observed runtime-binary/source provenance is established. Floating `aspnet:10.0` and SDK `10.0.103` are not sufficient provenance.

Current enabled-mode architecture has a finite root bootstrap that forks an agent UID 1000 and execs a router UID 1100 as PID 1. This must **not** be assumed to be the approved future topology. Current `waitpid(-1, WNOHANG)` is not bound to an expected child PID. Acknowledgment, EOF, or Streamlit termination cannot establish agent exit 0.

## 4. Narrow design task

Produce a **concrete, evidence-classified, testable design specification**, without modifying files, for the following *candidate* enabled-mode topology:

```text
finite root bootstrap (PID 1, exits by exec after permanent drop)
  -> unprivileged supervisor (PID 1, proposed UID/GID 1000:1000)
       |-- router (direct child, UID/GID 1100:1100; .NET)
       `-- guardian (direct child, UID/GID 1000:1000)
             `-- agent (UID/GID 1000:1000)
                   `-- Streamlit (UID/GID 1000:1000)
```

**Do not silently assume this topology is realizable.** In particular, analyze how a supervisor already running UID 1000 can start a UID 1100 router **without** retained root/capabilities, a resident privileged helper, cross-UID signaling, or A3-prohibited managed privileged startup. Consider bootstrap creation order and Linux parentage/exec semantics. A pre-forked router does not automatically become the supervisor's direct child; a sibling cannot wait for it. If no valid creation/parentage arrangement exists, explicitly mark **DESIGN NOT CLOSED**. Do not solve this by changing A1/A2/A3 implicitly.

### Required analyses

1. **Creation/identity/parentage:** Provide an exact chronological process-creation and permanent-privilege-drop sequence. Show who forks/execs whom, UID/GID and supplementary groups, capabilities, PID 1 continuity, and who can `waitpid` on each process. Address the UID 1000 -> UID 1100 creation conflict explicitly. Distinguish kernel parentage from logical supervision; prove that any claimed direct-child wait is valid.
2. **Status provenance and final verdict:** Agent waits for Streamlit; guardian waits for the *specific* agent and attests actual terminal kernel status (D1); supervisor waits for the *specific* guardian and independently verifies normal exit 0 (D2). Define router status ownership and whether its exit code is mandatory. No `waitpid(-1)` status misattribution, no success from `ECHILD`, EOF, signal/kill, pidfd readiness or unverified report. Define a complete PASS/FAIL decision table and exit-code mapping.
3. **Supervisor/guardian integrity:** Analyze same-UID threats (agent/UI compromise, `ptrace`, `/proc`, FD duplication, signals, writable executables/dependencies, inherited environment, socket spoofing). Specify enforceable defenses, assumptions and residual risks, including whether separate identity or kernel protections would be required and whether those would need new owner approval. Do not claim channel possession proves truthful kernel waiting.
4. **IPC and credential boundaries:** Detail bounded framing, identity and per-launch binding, FD creation/inheritance/closure, router-controlled graceful stop without cross-UID signals, administrative credential FD isolation (router only), agent/guardian/supervisor access limitations, malformed/stale/duplicate/partial frames, EOF and timeout behavior. Preserve no secrets in env/argv/files/logs/reports/`docker inspect`.
5. **Failure and containment:** Analyze guardian/router/agent/UI crashes or hangs, status obtained then reporter crash, cancellation, simultaneous failures, orphan adoption, supervisor PID 1 death, and containment under D3. Explicitly separate *clean application shutdown* from *failed shutdown followed by container teardown*. Do not assume an unprivileged supervisor can signal a different-UID router; explain the cooperative shutdown path and failure fallback.
6. **Timing:** Specify one monotonic deadline starting at shutdown initiation, **four seconds total** including host drain/disposal, all IPC/waits and cleanup. Docker stop grace remains **five seconds**. No retries may reset the clock. Analyze whether the full chain is plausible; do not assert measured performance.
7. **Mode isolation and provenance:** Disabled Option A must retain its current PID 1 Streamlit behavior. Enabled-mode changes must not activate admin diagnostics by default, Worker, real market-data feeds, or Azure operations. Identify required owner-supplied existing image ID/digest, runtime-stage digest, platform and component build/source metadata without downloading or building images.
8. **Alternatives within approvals:** If the candidate topology fails on router identity/parentage or same-UID integrity, compare at most two narrowly scoped alternatives. Identify **exactly** which owner decision/constraint would require amendment, without recommending an implicit relaxation of strict D2 or D3. No speculative chain of extra helpers with unverified exit codes.
9. **Verification plan only:** Propose deterministic offline tests for status binding, wait errors, protocol frames, failures and deadline accounting; and *separately authorized* real Linux PID 1 tests for UID transitions, direct-child waits, credentials, integrity, orphan containment and timing. No tests may be run under this authority.

## 5. Permitted and forbidden actions

**Permitted:** Read existing repository source, previously captured sanitized reports, existing runbooks, and publicly available official .NET/Linux source or documentation where accessible without creating local artifacts. Read-only Git inspection. Reason and draft the final response in chat.

**Forbidden:** Any repository edits or new files, code generation into the repo, builds, test execution, package installation/restoration, Docker commands, host qualification, Azure activity, GitHub mutations, branch operations, commits, pushes, deployments, privilege/ACL/capability changes, credential exposure, or running the application. No implementation file allowlist until design closure.

Do not access or alter the owner’s Docker environment. The Codex sandbox identity has no Docker entitlement; do not seek it.

## 6. Required final report (in chat only)

1. Baseline and initial/final read-only status comparison; limitations of preservation proof.
2. Evidence ledger: **CONFIRMED / SOURCE-SUPPORTED / INFERRED / UNPROVEN / UNAVAILABLE**, with exact local source references and authoritative external links if used.
3. Creation-order/UID/PID/parentage table and process/FD diagram. **Explicit verdict on whether UID 1000 PID 1 can directly own a UID 1100 router under A1/A2/A3.**
4. Status-ownership proof chain for UI, agent, guardian and router, with a final-verdict truth table.
5. IPC, credentials, threat model, failure-state machine and monotonic-deadline design.
6. Unresolved blockers, required owner decisions (if any), evidence and future qualification plan.
7. One of **DESIGN CLOSED (PLANNING ONLY)** or **DESIGN NOT CLOSED**, with reasons. Design closure does not itself authorize implementation.
8. If and only if design is closed, recommend the smallest **separately approved** next planning/implementation authority; otherwise recommend the smallest bounded next evidence/owner-decision step.
9. Explicit **STOP — owner review required**.

## 7. Immutable release gates

`PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability **NO-GO**; production ingress TLS **NO-GO**; Azure passive qualification **NO-GO**; public Worker activation **NO-GO**. No real BTC/ETH feed, licensing assumption, public API exposure or deployment is authorized.

**No implementation. No runtime qualification. No GitHub changes. STOP for owner review.**
