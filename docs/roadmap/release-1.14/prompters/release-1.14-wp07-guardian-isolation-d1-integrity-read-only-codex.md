# Codex Authority — Release 1.14 WP07
## Guardian Isolation Mechanisms and D1 Integrity Boundary

**Authority class:** READ-ONLY architecture investigation; planning only.  
**Owner authorization:** This document authorizes analysis, not implementation, identity allocation, qualification, or GitHub/Azure modification.  
**Mandatory terminal state:** STOP — OWNER REVIEW REQUIRED.

## 1. Mission

Determine whether a defensible, enforceable integrity boundary can protect the trusted guardian's D1 attestation against hostile or compromised agent/Streamlit processes. Compare narrowly bounded candidate mechanisms, specify exact Linux process ancestry and UID/GID transitions, identify proof obligations and any further owner decisions. Do **not** treat a guardian's normal exit 0 as proof of attestation honesty.

The most recent read-only report concluded **DESIGN NOT CLOSED**: a distinct unprivileged supervisor PID 1 is structurally defensible, but the guardian sharing UID 1000 with agent/UI leaves D1 integrity unresolved. A distinct supervisor identity is not yet adopted or assigned.

## 2. Binding owner decisions — do not reinterpret

- **A1 YES:** enabled-mode PID 1 may be an unprivileged supervisor; router remains UID 1100. Disabled Option A unchanged.
- **A2 YES:** supervisor independently verifies guardian's actual normal exit code 0 and owns final container verdict.
- **A3 NO:** no expanded privileged managed startup.
- **D1:** guardian attestation must be based on its real kernel wait for the specifically bound agent and must be trustworthy under an explicitly qualified integrity boundary.
- **Strict D2:** supervisor must independently obtain the guardian's real normal exit code 0 by a positive, specifically bound kernel wait; EOF, disappearance, ECHILD, or self-report cannot substitute.
- **D3:** PID 1/container teardown is failure containment only, never clean PASS evidence.
- **T1 YES:** read-only investigation of a distinct unprivileged supervisor identity only; **no UID allocation or identity-contract amendment**.
- **T2 YES:** conditional PID 1 adoption plus terminal child-absence evidence, subject to Linux qualification and only **after** positive D1/D2 and other mandatory status proofs. Terminal ECHILD may prove absence, not exit 0.
- **T3 YES:** production administration remains disabled until compliant credential ingress is established; do not reopen generic Azure credential research.
- One shared **CLOCK_MONOTONIC four-second** shutdown deadline; **five-second Docker** stop grace; no deadline reset.
- `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO**.

## 3. Protected baseline and permitted inspection

Expected baseline from previous Codex report:

- Branch: `draft/release-1.14-wp07-partial-public-disabled`
- HEAD: `67893f51931ee183f71050a83b81127e6e648b87`
- Working tree is intentionally dirty. Preserve **all** tracked/untracked content exactly, including pre-existing changes. Record initial/final branch, HEAD and complete status; distinguish status-only preservation from content-hash evidence.

Permitted: read existing local source, runbooks, prior reports and existing documentation; inspect local Git metadata with read-only commands; consult authoritative public Linux/kernel documentation if available; reason and report in the Codex response. If a prerequisite is unavailable, label it UNAVAILABLE rather than creating or fetching resources.

**Prohibited:** source/docs/test edits or repository artifact creation; code generation in the working tree; package installation; builds/tests/experiments; Docker execution; Azure access or deployment; credential reads or secret retrieval; UID/GID allocation; changing permissions; Git checkout/reset/stash/commit/rebase/push; PRs/issues/projects; any GitHub changes; runtime qualification. No new implementation allowlist.

## 4. Questions to resolve

### Q1 — Existing UID 1000 guardian with hardening

Evaluate whether the following can constitute an enforceable D1 integrity boundary against compromised UID 1000 agent/UI (not merely defense in depth): immutable/root-owned executable, interpreter, dependency and import paths; sanitized environment; post-final-exec nondumpability; no trusted-role re-exec/restart with hostile same-UID processes alive; restricted `/proc` and ptrace access; strict role-specific FD closure, no inherited or duplicated attestation/control endpoints; per-launch binding; signals including SIGSTOP/SIGKILL and permitted same-UID signaling; status-consumer exclusivity. Distinguish availability attacks that cause sticky FAIL from integrity attacks that could forge a clean PASS. Identify assumptions about trusted guardian/agent behavior and the limitations of each control.

### Q2 — Distinct unprivileged guardian identity

Explore whether a **finite root PID 1** can create a topology in which the guardian is distinct from agent/UI while preserving the guardian's direct-child kernel wait for the agent and the supervisor's direct-child wait for the guardian. In particular, test the logic of *pre-forking the UID 1000 agent from the guardian before the guardian drops to a distinct unprivileged UID*, while preserving guardian parent identity through exec. Analyze startup synchronization so no untrusted agent/UI begins before guardian post-exec hardening and all channels are bound. Do not assume a non-root guardian can create a UID 1000 agent after dropping credentials.

For each candidate, specify parentage at every fork/exec, real/effective/saved UID/GID, supplementary groups, capabilities, credential FD ownership, result/control channel ownership, exact wait owner, signals, privilege-drop irreversibility and failure behavior. Explain whether the finite root bootstrap could safely coordinate supervisor, router, guardian and agent without retaining a privileged helper. **Do not select numeric IDs or approve identity changes.** Identify any new owner identity-contract decision needed.

### Q3 — Other narrowly feasible kernel isolation

Compare only mechanisms plausibly available within the existing container/security/resource constraints, such as kernel credential separation, descriptor confinement, dumpability/ptrace restrictions, namespace properties or other explicitly documented controls. Do not assume writable cgroup delegation, additional services, privileged resident helper, broad container capability changes, cross-UID signal authority, or an Azure feature not established by evidence. Mark unsupported candidates blocked rather than inventing availability.

### Q4 — D1/D2/T2 and bounded shutdown compatibility

For each viable candidate, trace:

1. Agent's specifically bound actual normal exit status collected by guardian (D1), with an integrity basis for the report.
2. Supervisor's independent positive guardian normal exit 0 (strict D2).
3. Supervisor's positive router normal exit 0 and host disposal completion.
4. PID 1's exclusive adopted-descendant drain and terminal child-absence observation under T2 assumptions, with no competing reaper or discarded SIGCHLD statuses.
5. Sticky FAIL behavior for missing, duplicate, stale or malformed records, guardian/agent/UI compromise or crashes, early EOF, ECHILD in required waits, signal attacks, unknown orphans, incomplete drain, router hang and deadline expiry.
6. One shared four-second monotonic shutdown budget and five-second Docker grace; no invented timing results.

Explain which behaviors are logical design arguments and which require later separately authorized target-kernel/Linux PID 1 tests. Preserve D3 as failure containment only.

## 5. Required deliverable (response only)

Provide a structured report with:

1. **Headline:** `DESIGN CLOSED (PLANNING ONLY)` or `DESIGN NOT CLOSED`, with a concise reason. Do not declare closure based on unqualified kernel assumptions or an unresolved D1 integrity boundary.
2. **Baseline preservation:** initial/final branch, HEAD, full status; report exact evidence level.
3. **Evidence ledger:** CONFIRMED, SOURCE-SUPPORTED, INFERRED, UNPROVEN, UNAVAILABLE, OWNER DECISION REQUIRED; identify inspected files/line locations and external references where relevant.
4. **Threat model:** trusted components, compromised UI/agent/guardian/supervisor, attacker actions, forged PASS versus forced FAIL.
5. **Candidate comparison:** UID 1000 guardian hardened; distinct guardian identity with creation order; at most a few actually feasible kernel alternatives. Include exact ancestry and UID/GID/FD/wait matrices.
6. **D1 proof argument:** why the guardian's agent status attestation can or cannot be trusted; explicit failure paths and limits of strict D2.
7. **T2/deadline integration:** proof order, descendant-absence assumptions, negative cases and qualification obligations.
8. **Decision ledger:** separate already-approved decisions from **proposed/unapproved** identity or trust changes; supply minimal exact owner decision wording if needed.
9. **Smallest next authority:** only the narrowest justified read-only owner decision or design-closure step. No implementation authorization.
10. **Final release posture:** `PUBLIC_RUNTIME_VERIFIED=False`; all existing NO-GO gates; **STOP — OWNER REVIEW REQUIRED**.

## 6. Stop conditions

Stop immediately and report if a proposed approach requires an unapproved UID/identity amendment, expanded privilege, production credential path, cross-UID signaling, altered shutdown contract, new service, GitHub/Azure modification or empirical execution. You may document the blocked option as a proposal but must not perform it. If D1 cannot be defensibly closed, report **DESIGN NOT CLOSED** and the precise unresolved owner choice or evidence requirement.

**NO IMPLEMENTATION. NO RUNTIME QUALIFICATION. NO GITHUB OR AZURE CHANGES.**
