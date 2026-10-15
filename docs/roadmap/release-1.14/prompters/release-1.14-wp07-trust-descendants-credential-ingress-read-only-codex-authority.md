# Codex Authority — Release 1.14 WP07
## Trust Boundary, Descendant Closure, and Credential Ingress Design

**Authority class:** READ-ONLY / DESIGN ONLY / NO IMPLEMENTATION  
**Decision gate:** STOP FOR OWNER REVIEW  
**Required verdict:** `DESIGN CLOSED (PLANNING ONLY)` or `DESIGN NOT CLOSED`

### 1. Mission

Investigate and specify the three unresolved blockers from the latest supervisor design review: (1) integrity of trusted supervisor/guardian status reporting in the presence of same-UID agent/UI processes, (2) evidence of clean descendant termination rather than merely named-child exits, and (3) secure credential ingress confined to the router. Compare retaining supervisor UID 1000 against a separate unprivileged supervisor identity. **Do not implement, test, deploy, or silently change policy.**

### 2. Binding baseline and preservation

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Existing dirty and untracked work is intentional. Record initial and final branch, HEAD, and full Git status; require exact status equality. Do not clean, stash, reset, checkout, commit, push, rebase, or change any file, including generated reports in the repository.
- If branch/HEAD differ, record the mismatch and STOP rather than attempting repair.
- Preservation evidence must be accurately labeled status-only unless independent content hashes were actually captured.

### 3. Binding owner decisions — do not reopen

- **A1 YES:** Enabled mode may use an unprivileged supervisor as PID 1; router remains UID/GID 1100:1100. Disabled Option A is unchanged.
- **A2 YES:** Supervisor independently verifies guardian normal exit code 0 and owns final container verdict.
- **A3 NO:** No expanded privileged managed startup.
- **D1 conditional YES:** Trusted guardian attestation of the agent's actual, bound kernel-wait result is acceptable only after integrity and provenance qualification.
- **D2 strict:** Independently verified guardian normal exit code 0 is mandatory; neither a report nor EOF, liveness, pidfd readiness, or `ECHILD` substitutes.
- **D3 conditional YES:** PID 1 exit/container teardown may provide qualified failure containment only; never turn FAIL into clean PASS.
- No persistent root helper, privilege expansion, cross-UID signaling, credential exposure, unsupported .NET runtime hooks, or new public recovery surface.
- Four-second **single monotonic coordinated deadline** and five-second Docker stop grace remain unchanged. `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification, and public Worker activation remain NO-GO.

### 4. Architecture starting point (candidate, not approved implementation)

Finite root bootstrap is PID 1. It first forks a router child, permanently drops that child to UID/GID 1100 and execs .NET. Original PID 1 permanently drops to UID/GID 1000 and execs an unprivileged supervisor in the **same PID 1 process**, retaining parentage of router. Supervisor creates guardian; guardian creates agent; agent creates Streamlit, all at UID/GID 1000. Agent waits for UI, guardian waits for its bound agent and reports D1 evidence, supervisor independently waits for its bound guardian and router and emits final container exit code. This creation order resolves router parentage in principle, but not integrity, credentials, descendants, or timing.

### 5. Investigation A — trusted-component integrity

1. State exact trusted computing base, attacker capabilities, and assumptions for each of supervisor, guardian, agent, UI, router, finite bootstrap, container runtime, filesystem, and kernel. Distinguish accidental bugs, compromised UI, compromised agent, and compromised guardian. Never claim integrity against a compromised guardian merely because its own code reports success.
2. Analyze same-UID attacks: signals, ptrace and `/proc`, inherited or duplicated FDs, writable interpreter/library/dependency paths, environment and import-path poisoning, process replacement, PID reuse, and post-exec dumpability reset. Explain which controls are enforceable and which remain assumptions or require real-Linux qualification.
3. Specify immutable executable/interpreter/dependency checks, safe pre/post-exec sequencing, `PR_SET_DUMPABLE` timing, role-specific FD allowlists, and status-channel authenticity and truthfulness separately. Note that nondumpability is not general same-UID isolation or a defense against permitted signals.
4. Compare **UID 1000 supervisor** with **a distinct unprivileged supervisor UID**. For each, detail exact root-fork ordering, parentage, permanent UID/GID drops, guardian creation, cross-UID IPC, status ownership, and prohibited signaling. Do not assume a supervisor under a distinct UID can subsequently create a UID 1000 guardian; identify any pre-fork and reparenting/parentage constraints. Identify every additional owner approval needed for a new identity contract.
5. Provide an explicit integrity-verdict matrix: established by existing source, source-supported but unqualified, unproven, impossible under current policy, or requiring owner decision.

### 6. Investigation B — clean descendant closure

1. Define exactly what **clean shutdown** must prove for Streamlit and all its descendants, including grandchildren, orphaning, daemonization, fork/exec races, adopted children, PID reuse, process groups, and abnormal agent/guardian exits. Separate trusted-agent guarantees from independently observed kernel evidence.
2. Compare bounded mechanisms (specific-child waits, process groups/sessions, subreaper behavior, `/proc` inspection, cgroup-scoped enumeration/kill where applicable, namespace PID 1 semantics) for ability to prove **clean** descendant closure, not merely failure containment. State which require permissions, additional capabilities, unavailable container features, or new owner policy.
3. Specify success/failure state transitions, binding of status to the correct process, one wait authority per direct child, `EINTR`/`ECHILD` handling, early EOF, duplicate frames, guardian crash after reporting, router hang, supervisor death, simultaneous failures, and shared-deadline exhaustion.
4. Make the distinction explicit: PID 1 death and container teardown may qualify **D3 containment after FAIL**, but cannot establish missing D1/D2 evidence or clean descendant shutdown.
5. Identify the smallest possible trusted-agent assumption, if any, that would close the clean-descendant obligation; label it a proposed policy decision rather than silently adopting it.

### 7. Investigation C — credential ingress

1. Trace existing **synthetic local** credential delivery from owner qualification wrapper to bootstrap and router, based only on readable source/runbook. Distinguish existing practice from any proposed production ingress; do not inspect, print, decode, or record credential values.
2. Define a proposed private ingress and handoff from an authorized owner/source to **router-only** consumption, with no credential in Docker configuration, image, persistent host/container file, logs, reports, argv, inherited supervisor/guardian/agent/UI environment, or leaked FD. Explain lifecycle before/after fork and exec, role-specific FD maps, error/EOF paths, and secret-bearing memory cleanup assumptions.
3. Compare feasible approaches (e.g. transient inherited pipe/FD, owner-supplied runtime secret channel, or other bounded mechanism) with precise platform/host constraints and evidence. Do not assume Azure/App Service provides an unspecified secure FD injection mechanism. Distinguish synthetic local qualification from production feasibility and licensing/public activation gates.
4. If requirements cannot all be met, identify the exact conflict and the smallest owner decision needed. Do not relax secrecy or add an admin public recovery endpoint.

### 8. Evidence and verification plan

- Use existing repository source, runbooks, sanitized qualification reports, and trusted public official Linux/.NET references **read-only** if accessible. Label every assertion `CONFIRMED`, `SOURCE-SUPPORTED`, `INFERRED`, `UNPROVEN`, or `UNAVAILABLE`. Do not claim the observed container binaries match tagged runtime source without provenance.
- List missing image ID/digest, runtime-stage digest, platform, native runtime build IDs/hashes, and source provenance; **do not** build or pull images to obtain them.
- Propose deterministic offline tests (threat assumptions, status binding, malformed frames, failure stickiness, credentials/FD maps, descendant races and deadlines) and separately authorized real Linux PID 1 qualification (kernel parentage/waits, same-UID protections, credential confinement, orphan handling, namespace teardown and four-second completion). **Do not run any tests or Docker.**
- Present an exact decision matrix separating (a) design closure, (b) owner policy decisions, and (c) future empirical qualification. Do not label design closed merely because future tests could discover whether a mechanism works.

### 9. Strict authority limits

Allowed: read-only inspection; architectural diagrams; bounded protocol and threat-model specifications; evidence-based comparison; proposed owner decision wording; recommendation of next **separately authorized** phase.

Prohibited: repository/file edits, new code or test files, generated artifacts in repo, dependency installation/restoration, builds, test execution, Docker commands, Azure activity, Git mutations, credential inspection, privilege/ACL changes, implementation allowlist authorization, public Worker/data activation. Do not run an owner qualification script.

### 10. Required response format

Return, in order:

1. **Verdict:** `DESIGN CLOSED (PLANNING ONLY)` or `DESIGN NOT CLOSED`, with one-sentence rationale.
2. Baseline and preservation evidence, including whether hashes were checked.
3. Evidence ledger with source locations and confidence classifications.
4. Exact candidate topology and identity/descriptor ownership table.
5. Trusted-component and attacker-capability matrix, including UID 1000 vs distinct supervisor UID comparison.
6. Clean-descendant proof model, failure matrix and D3 boundary.
7. Credential ingress/handoff comparison and exact unresolved production assumptions.
8. Shared-deadline and verification obligations, clearly distinguishing design from empirical tests.
9. Any proposed owner decisions (each **UNAPPROVED**), unresolved blockers and smallest justified next authority.
10. Explicit statement: **NO IMPLEMENTATION. NO RUNTIME QUALIFICATION. NO GITHUB CHANGES. STOP — OWNER REVIEW REQUIRED.**

If any essential trust, descendant, or credential requirement remains unsupported, verdict **DESIGN NOT CLOSED**. Preserve all NO-GO gates.
