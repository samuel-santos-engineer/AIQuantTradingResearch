# Codex Authority — Release 1.14 WP07: Owner Trust Decisions and Platform Evidence

**Authority type:** Read-only, planning-only. **No implementation authority.**

## 1. Purpose and controlling baseline

Prepare an evidence-backed owner decision record for the remaining enabled-mode shutdown architecture blockers: trusted-component identity/integrity, PID 1 descendant-absence proof, and permissible production credential ingress on Azure F1. Investigate only the minimum platform evidence needed to frame these decisions. **Do not make owner decisions on their behalf.**

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

Expected local baseline (verify, do not repair):
- Branch: `draft/release-1.14-wp07-partial-public-disabled`
- HEAD: `67893f51931ee183f71050a83b81127e6e648b87`
- Working tree intentionally dirty, including untracked files. Capture initial and final `git status` and compare; never clean, reset, stash, checkout, rebase, commit, push or modify files. A matching status is not a content-hash guarantee; label evidence accurately.

The preceding read-only design report concluded **DESIGN NOT CLOSED**. Its candidate is a finite root bootstrap that forks a UID/GID 1100 router, then permanently drops and execs a UID/GID 1000 supervisor **as PID 1**; supervisor directly parents guardian, guardian directly parents agent, agent directly parents Streamlit (UID/GID 1000). The supervisor independently waits for guardian and router; guardian waits for agent; agent waits for UI. PID 1 adopted-descendant draining and a final child-absence observation are proposed, not qualified. The current synthetic credential wrapper is not proven to be a permissible production ingress mechanism.

## 2. Binding owner decisions — do not reopen

- **A1:** Enabled mode may use an unprivileged supervisor as PID 1; router stays UID/GID 1100:1100. Disabled Option A must remain unchanged.
- **A2:** Supervisor independently verifies guardian normal exit code 0 and owns the final container verdict.
- **A3:** Expanded privileged managed startup is excluded. Root bootstrap is finite only; no resident privileged helper.
- **D1:** Trusted guardian attestation is conditionally acceptable only when based on a direct, identity-bound, actual kernel wait establishing agent normal exit 0, with integrity qualified.
- **D2 (STRICT):** Guardian normal exit code 0 must be independently verified by the supervisor. No EOF, disappearance, self-report, pidfd readiness or `ECHILD` may substitute.
- **D3:** PID 1 exit/container teardown may provide **failure containment only**, subject to qualification; never a clean PASS.
- Shared monotonic shutdown deadline: **4 seconds** including host drain/disposal and required waits; Docker stop grace: **5 seconds**. No reset or additional time budget.
- No new privilege, cross-UID signaling, credential exposure, public recovery endpoints or Worker activation.

## 3. Read-only work packages

### WP-A — Trust and identity owner decision record

Compare precisely:
1. **Existing identity model:** supervisor, guardian, agent and UI UID 1000, router UID 1100; root-owned immutable executable/dependency paths, fixed import paths, sanitized environments, role-specific FD allowlists, post-final-exec nondumpability, and no trusted-role restart while untrusted same-UID processes remain.
2. **Distinct unprivileged supervisor identity:** finite root PID 1 must pre-fork both router UID 1100 and guardian UID 1000 **before** permanently dropping/execing the supervisor under a proposed third unprivileged UID. Preserve direct-child wait ownership. Identify exact identity-contract amendment required; do not assume the guardian gains isolation from same-UID agent/UI.
3. Any strictly narrower option supported by inspected source, without added privilege or changes to D1/D2.

Specify attacker capabilities separately for compromised UI, agent, guardian and router: same-UID signals, ptrace/proc access, FD inheritance/duplication, mutable imports/libraries, writable paths, credential access, spoofed IPC and restart/re-exec. Distinguish protections demonstrable from source from assumptions requiring kernel/container qualification. State what compromise of each trusted component does to evidence validity. **Do not claim nondumpability prevents same-UID signaling or creates a full security boundary.**

Draft **T1** owner decision (all options **UNDECIDED**): retain UID 1000 supervisor under enumerated trust assumptions, approve investigation of a distinct supervisor UID (requiring a later explicit identity amendment), or reject both pending stronger evidence. Recommend a conditional choice, with tradeoffs and exact approval wording, but do not select it.

### WP-B — PID 1 descendant-closure owner decision record

Define an exact ordering and proof ledger: mandatory positive statuses for UI policy, agent via qualified D1 guardian wait, guardian via strict D2 supervisor wait, router via supervisor wait and host disposal; then stop legitimate launches, drain adopted children under a **single** PID 1 wait authority, and require final kernel child absence before a clean verdict. Distinguish terminal `ECHILD` as **absence evidence only after** all mandatory positive statuses, never as successful agent/guardian exit. Distinguish WNOHANG zero (children may remain), unrelated adopted-child statuses, PID namespace adoption rules, signal dispositions, subreapers, nested PID namespaces and externally injected processes. Discuss races between absence observation and final exit, plus shutdown deadline exhaustion. Do not assume groups/sessions or `/proc` snapshots prove descendant absence.

Draft **T2** owner decision (**UNDECIDED**): conditional acceptance or rejection of the PID 1 adoption-and-child-absence design model, with explicit assumptions and required real-Linux qualification. Do not mark it qualified.

### WP-C — Production credential ingress and platform evidence

Inspect existing repository/bootstrap/runbook evidence for transient synthetic local key generation, environment-to-pipe handling, router FD 3, and role-specific descriptor isolation. Do not read, print, transmit or materialize any actual secret values.

Investigate only publicly documented, source-verifiable **Azure App Service Linux / F1** facilities that might deliver an authorized production credential through a private FD or equivalent permitted channel **without** exposing it through container configuration environment variables, command arguments, host files, forbidden mounts, public APIs or additional unapproved services. Record the precise source, applicability, platform constraints and any unresolved gaps. Do not confuse local synthetic-key generation with production authorized credential distribution. Do not invent an Azure private-FD ingress feature. If no compliant supported mechanism is established, explicitly conclude **NOT ESTABLISHED** and retain production administration disabled; consider whether a future separately approved policy decision would be necessary rather than relaxing secrecy silently.

Draft **T3** owner decision (**UNDECIDED**): keep production administration disabled pending evidence (default recommendation), or authorize a future narrowly scoped investigation of a specifically sourced permissible ingress mechanism. Do not approve production credentials or enable administration.

### WP-D — Evidence and design-closure boundary

Separate **CONFIRMED**, **SOURCE-SUPPORTED**, **INFERRED**, **UNPROVEN**, **UNAVAILABLE** and **OWNER DECISION REQUIRED**. Preserve distinction between the observed runtime 10.0.12 / `ECHILD` / exit 64 and the public v10.0.12 source; actual status consumer and exact image/runtime binary provenance remain unproven. List minimum owner-supplied existing image ID/digest, runtime-stage digest, platform and native build/source metadata without building/pulling images.

Provide an offline-test design and separately authorized Linux PID 1 qualification plan only, including same-UID attack attempts, descriptor confinement, agent/guardian/router failures, orphan adoption, wait races, four-second deadline and D3 container teardown. **Do not execute any tests.**

## 4. Hard execution limits

Allowed: read-only inspection of existing source, reports, git metadata and trustworthy public documentation/source where available; reasoning and a response-only proposed decision record. Any web/network use must be read-only and must not authenticate to Azure or GitHub or retrieve secrets. If network access is unavailable, label evidence unavailable; do not bypass restrictions.

Forbidden: source/doc/script edits, writing decision files into the repository, generating repository artifacts, package restores, builds, tests, Docker runs, image pulls/builds, qualification retries, Azure CLI or portal mutations, credential inspection, Worker activation, branch/commit/status changes, GitHub PRs/issues/comments/pushes, privilege changes and implementation. Do not run cleanup or reset commands. Preserve all existing dirty/untracked content.

If any necessary evidence requires a forbidden operation, **STOP** and identify the exact separate owner authority needed.

## 5. Required response format

1. **Baseline and preservation:** branch, HEAD, before/after status comparison and limitations.
2. **Evidence ledger:** file/line or versioned external-source citations; no claims beyond evidence.
3. **T1 trust/identity options:** threat matrix, integrity gaps, exact proposed owner decision wording; **UNDECIDED**.
4. **T2 descendant closure:** proof sequence, assumptions, failure semantics, proposed owner decision wording; **UNDECIDED**.
5. **T3 credential ingress:** supported Azure F1 evidence vs unsupported options, recommended production-disabled posture, proposed owner decision wording; **UNDECIDED**.
6. **Remaining design blockers:** separate owner-policy decisions, architectural gaps, and future qualification-only proof obligations.
7. **Next bounded authority:** recommend the smallest read-only step conditional on T1/T2/T3, or state why no further authority is justified. **No implementation allowlist until design closure.**
8. Explicit verdict **DESIGN CLOSED (PLANNING ONLY)** or **DESIGN NOT CLOSED**. Do not claim closure while a mandatory trust/ingress/cleanup mechanism is unsupported.
9. Release gates and STOP statement.

**Permanent gates:** `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability **NO-GO**, production ingress TLS **NO-GO**, Azure passive qualification **NO-GO**, public Worker activation **NO-GO**. All remain unchanged.

**STOP — owner review required.**
