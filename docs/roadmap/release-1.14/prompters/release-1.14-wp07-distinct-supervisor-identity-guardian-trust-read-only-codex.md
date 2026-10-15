# Codex Authority — Release 1.14 WP07
## Distinct Supervisor Identity and Guardian Trust Boundary Reconciliation

**Authority class:** READ-ONLY DESIGN INVESTIGATION — PLANNING ONLY  
**Status:** OWNER-AUTHORIZED INVESTIGATION; NO IMPLEMENTATION AUTHORITY  
**Stop condition:** Return findings and stop for owner review.

### 1. Owner decisions (binding)

- **A1 YES:** An unprivileged supervisor may serve as enabled-mode PID 1; router remains UID/GID 1100:1100.
- **A2 YES:** Supervisor independently collects and verifies guardian's actual normal exit code 0 and owns the final container verdict.
- **A3 NO:** Expanded privileged managed startup is excluded.
- **D1:** Guardian's kernel-wait attestation for the specifically bound agent is conditionally admissible only when its integrity and evidence chain are qualified.
- **Strict D2:** Supervisor must independently obtain the guardian's normal exit code 0 through a specifically bound kernel wait. EOF, disappearance, `ECHILD`, self-report or namespace teardown are not substitutes.
- **D3:** Container teardown can contain failure but cannot prove clean shutdown or justify PASS.
- **T1 YES:** Investigate a distinct unprivileged supervisor identity, **read-only**. This is **not** approval to allocate a UID, change an identity contract or select a final topology.
- **T2 YES:** Conditionally accept PID 1 adoption plus terminal kernel child-absence evidence for descendant closure, subject to explicit assumptions and separate Linux qualification. Terminal `ECHILD` proves absence **only after** all mandatory positive D1/D2 and router status evidence; it cannot prove any required exit 0.
- **T3 YES:** Production administration remains disabled until compliant credential ingress is established. Do not reopen generic Azure credential research.

Keep the shared **four-second CLOCK_MONOTONIC shutdown deadline**, **five-second Docker stop grace**, disabled Option A and all existing release NO-GO gates unchanged. `PUBLIC_RUNTIME_VERIFIED=False`.

### 2. Baseline and preservation

Repository: `samuel-santos-engineer/AIQuantTradingResearch`  
Expected branch: `draft/release-1.14-wp07-partial-public-disabled`  
Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`

The working tree may be intentionally dirty. **Preserve all existing files, untracked files, branches and Git state.** At start and end, capture read-only branch, HEAD and complete status; distinguish status comparison from actual content-hash verification. If baseline differs, report the difference and STOP rather than repairing it.

### 3. Permitted work

Read existing repository source, specifications, runbooks, previous WP07 evidence and relevant local documentation. Read narrowly relevant public Linux semantics documentation if needed, distinguishing source evidence, inference and unqualified assumptions. Prepare a textual architecture comparison and owner-decision proposal **in the final response only**; do not create or edit repository files or external records.

### 4. Investigation questions — bounded scope

**Q1 — Exact creation order and direct-child ownership.** Trace finite root PID 1 bootstrap pre-forking router UID 1100 and guardian UID 1000, then permanently dropping all credentials/capabilities and execing a supervisor under a **hypothetical distinct unprivileged identity** in the **same PID 1**. Determine whether both direct-child wait relationships survive exec and identity transition; whether guardian can create and wait for agent UID 1000 and agent for UI UID 1000; whether existing bootstrap/guardian startup dependencies require supervisor-mediated handshakes; and which proposed operations are impossible without privileges. Do not select or allocate a numeric UID.

**Q2 — Compare two explicit identity models.** Compare (i) supervisor UID 1000, with guardian created after supervisor exec, versus (ii) distinct supervisor UID, with guardian and router both pre-forked before supervisor's permanent drop. Provide a parent/child, real/effective/saved UID/GID, capability, FD and kernel-wait owner matrix. Cover credential and control descriptor inheritance, cross-UID IPC, readiness, shutdown initiation, routing of failure reports, signal permissions and terminal failure behavior. No privileged resident helper or cross-UID signal bypass is allowed.

**Q3 — Guardian integrity is a separate blocker.** Analyze threats from compromised same-UID agent/UI to guardian and D1 attestation, including signals, ptrace/dumpability, `/proc`, writable code/interpreters/import paths, inherited FDs, protocol spoofing, re-exec/restart, and adversarial descendants. Explicitly separate protections for a distinct-UID supervisor from protections for a UID 1000 guardian. State the smallest enforceable trust assumptions or additional architecture options needed for D1 without assuming that guardian exit 0 establishes guardian honesty. If a further owner policy amendment is necessary, specify its exact text as a **proposal**, not an approval.

**Q4 — T2 proof compatibility and shutdown deadline.** Check whether pre-fork guardian topology remains compatible with sole PID 1 reaping, positive named-child waits, adopted-child draining and terminal child-absence check. Treat `ECHILD` as absence only, never status success. Discuss competing waiters, `SIGCHLD` dispositions, intermediate subreapers, nested PID namespaces, injected processes and failure stickiness. Map every required action to the single shared four-second monotonic budget; do not claim measured timing.

**Q5 — Decide what can actually be closed.** Identify exact design blockers versus later test/provenance obligations. Compare security benefit, complexity, attack surface and feasibility of distinct supervisor identity against the UID 1000 baseline. Recommend the **smallest** next owner decision or read-only authority, if any. Do not issue implementation instructions or an implementation allowlist.

### 5. Mandatory non-authorizations

- **NO source edits, repository documents, generated artifacts, patches or code changes.**
- **NO builds, unit/integration tests, process launches, Docker, Linux PID 1 runtime qualification or Azure execution.**
- **NO Git branch operations, checkout, reset, stash, clean, commit, push, PR, issue or project modifications.**
- **NO UID allocation, identity-contract amendment, credential retrieval, secret inspection, production activation or new services.**
- **NO changes to disabled Option A, shutdown timing, D1/D2/D3, T1/T2/T3 or release gates.**
- Do not treat hypothetical mechanisms as proven, or make a clean PASS from EOF, `ECHILD`, PID disappearance, guardian self-report or Docker teardown.

### 6. Required final response structure

1. **Verdict:** `DESIGN CLOSED (PLANNING ONLY)` or `DESIGN NOT CLOSED`, with one-sentence rationale. Closure requires defensible guardian integrity, not merely supervisor separation.
2. **Baseline preservation:** initial/final branch, HEAD, complete status comparison; any limitations in evidence.
3. **Evidence ledger:** `CONFIRMED`, `SOURCE-SUPPORTED`, `INFERRED`, `UNPROVEN`, `UNAVAILABLE`, `OWNER DECISION REQUIRED`, with exact source references where available.
4. **Creation-order sequence and topology diagrams** for both candidate identities, including kernel wait ownership.
5. **Identity/descriptor/control matrix** and cross-UID constraints.
6. **Threat analysis:** distinct-supervisor benefits versus unresolved same-UID guardian D1 integrity.
7. **T2/strict D2 compatibility and four-second deadline/failure matrix.**
8. **Precise remaining blockers, proposed owner decision text (if needed), and smallest next bounded authority.**
9. **Disposition:** no implementation, no runtime qualification, no GitHub/Azure changes; `PUBLIC_RUNTIME_VERIFIED=False`; all administrative API, ingress TLS, Azure passive qualification and public Worker gates **NO-GO**.
10. **STOP — OWNER REVIEW REQUIRED.**

**Do not infer approval from the existence of this authority.** T1 permits investigation only; the owner must separately approve any identity-contract amendment or implementation.
