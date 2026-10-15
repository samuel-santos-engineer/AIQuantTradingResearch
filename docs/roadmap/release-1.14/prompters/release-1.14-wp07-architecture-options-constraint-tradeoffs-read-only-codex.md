# Codex Authority — Release 1.14 WP07
## Read-Only Architecture Options and Constraint-Tradeoff Review

**Authority class:** Planning and read-only evidence analysis ONLY. **No implementation.**

### 1. Binding baseline and preservation

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

Expected branch: `draft/release-1.14-wp07-partial-public-disabled`

Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`

Before doing anything, inspect branch, HEAD and complete Git status read-only. If the expected branch/HEAD do not match, STOP and report; do not switch branches or attempt repairs. The working tree is intentionally dirty and includes untracked files. Preserve all files, status, and Git state exactly. Record initial/final status and explicitly distinguish status-level evidence from independently verified content hashes. Do not run commands that generate artifacts, modify caches, or mutate the repository.

### 2. Established incident and binding decisions

The enabled-mode shutdown qualification remains FAILED: the sanitized owner report records .NET runtime `10.0.12`, `ECHILD`, `AgentTerminationUnverified`, container exit `64`, no OOM, and no verified agent exit code 0. Public .NET `v10.0.12` source contains PID 1 child reaping capable of racing application `waitpid(-1, WNOHANG)`; actual status consumer and exact observed runtime binary provenance remain unproven.

**Owner-approved security contract (do not reopen or reinterpret):**

- **D1:** Conditional YES to a trusted guardian's *actual kernel-wait* attestation of bound agent exit 0, subject to integrity, provenance, identity, and descriptor qualification.
- **D2:** STRICT: independently prove the guardian's own **normal exit code 0**. Report, EOF, disappearance, pidfd readiness, or `ECHILD` are not substitutes.
- **D3:** Conditional YES to PID 1 exit/container teardown for **failure containment only**, subject to qualification; never upgrade application shutdown FAIL to PASS.

Existing protected contract: disabled Option A unchanged; router is PID 1 and UID/GID 1100:1100 in enabled mode; presentation agent and UI UID/GID 1000:1000; finite root bootstrap only, no resident privileged helper; no unauthorized cross-UID signaling, added capabilities, privilege escalation, credential exposure, or public administrative activation. One monotonic 4-second coordinated shutdown deadline and unchanged Docker 5-second grace. `PUBLIC_RUNTIME_VERIFIED=False`; admin availability, production ingress TLS, Azure passive qualification and public Worker activation remain NO-GO.

### 3. Narrow objective

Produce an **architecture decision analysis**, not a design or code patch, addressing:

> Which *minimal and explicit* process-creation, supervision, or identity-contract change, if any, could provide independently proven guardian exit 0 (D2) and qualified agent status attestation (D1), without weakening the other approved controls?

Do not assume a feasible solution exists. Identify every frozen constraint that each option preserves or would require the owner to amend. The current result is **NOT ESTABLISHED / DESIGN NOT CLOSED** until supported evidence justifies otherwise.

### 4. Required options and proof analysis

Evaluate, at minimum, the following as distinct candidates, including reasons for rejection:

1. **Unchanged bootstrap-forked UID 1000 guardian:** kernel parentage, exec boundary, .NET PID 1 competing reaping, impossibility of retroactive managed-child registration absent a supported mechanism.
2. **Router-managed `Process.Start` guardian:** registered wait state and actual exit-code retention; why UID 1100 with no capabilities cannot launch UID 1000; assess supported user/group launch semantics without proposing implicit privilege changes.
3. **Change guardian identity to UID 1100:** whether same-UID guardian could be directly managed by the router while spawning/controlling UID 1000 agent; prove or refute the latter privilege boundary, status trust, descriptor isolation and signaling.
4. **Pre-created bootstrap launcher / one-shot privileged creation:** determine whether a finite, nonresident launcher can both satisfy managed status registration and drop privileges safely; explicitly identify any unavoidable root, saved UID, capability, setuid executable, or authority changes.
5. **Change PID 1/supervision ownership:** assess whether a different init/supervisor could exclusively reap both statuses; mark any change to frozen router-PID-1 architecture as requiring separate owner approval.
6. **Supported .NET runtime configuration or process APIs:** evaluate only documented/version-supported mechanisms for registered status ownership or exclusive reaping; do not use unsupported internals, signal hacks, or inferred adoption.
7. **Any materially simpler architecture:** only if supported by source and an explicit end-to-end process/status ownership graph. Reject recursive status reporters that merely move the D2 problem.

For every option, supply: process tree (before/after exec), UID/GID/capability timeline, exact parent of each child, who exclusively owns each kernel wait status, status provenance and D1/D2 satisfaction, same-UID attack surface, FD/credential confinement, failure/orphan containment under D3, deadline feasibility, required contract amendments, available evidence, and unresolved proof obligations. Distinguish source-confirmed, Linux-semantics-supported, inferred, and unproven claims. Treat pidfds as identity/termination tools, **not** automatic exit-code recovery after reaping.

### 5. Source and evidence discipline

Read existing repository source, tests, runbooks, owner-provided reports and locally available trusted documentation without modification. Consult official versioned .NET and Linux sources only as needed for narrow factual verification; cite exact URL/tag/line or local path/line. Do not imply that public tagged source proves byte identity with the recorded container. Explicitly list missing runtime image ID/digest, native component provenance and other necessary evidence; do not fetch/build/pull images or download runtime binaries. Do not claim direct access to files or reports not actually inspected.

### 6. Deliverables (in final Codex response only)

1. **Baseline/preservation ledger** with initial/final branch, HEAD and Git status comparison.
2. **Evidence ledger** separating confirmed observations, supported mechanisms, inference and unknowns.
3. **Architecture comparison matrix** with each candidate's D1/D2/D3 result and exact frozen constraint affected.
4. **Two or three least-disruptive options** (or explicitly none), ranked by security-contract compatibility, simplicity and proof burden; include a complete ownership and privilege-transition sketch for each.
5. **Owner decision boundary:** enumerate proposed changes to currently frozen constraints as separate **UNAPPROVED** decisions, with risks and explicit YES/NO consequences. Do **not** treat recommendations as owner consent; do not reopen D1/D2/D3.
6. **Binary verdict:** `ARCHITECTURE PATH IDENTIFIED (UNAPPROVED)` only if a source-supported candidate closes the logical status-ownership chain subject to stated tests; otherwise `NO DEFENSIBLE PATH ESTABLISHED`. In either case implementation remains **DESIGN NOT CLOSED**.
7. **Smallest next authority recommendation:** further read-only evidence/design if necessary, or an owner architecture-decision record. No implementation allowlist, implementation steps, or qualification authorization until owner decisions and design closure.

### 7. Absolute prohibitions / STOP conditions

No edits, patches, generated files in the repository, builds, tests, package restore, Docker invocation, host qualification, Azure commands, commits, branch changes, pushes, PRs, releases or deploys. No runtime credentials, keys, live admin endpoint activation, real market-data integration or Worker activation. Do not change security policy by assumption. STOP on baseline mismatch, inaccessible indispensable evidence, required privilege expansion, unsupported runtime hooks, unverifiable exit code, or any proposed bypass of D1/D2/D3; report the blocker rather than working around it.

**Final disposition must retain:** `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation **NO-GO**. **STOP — owner review required.**
