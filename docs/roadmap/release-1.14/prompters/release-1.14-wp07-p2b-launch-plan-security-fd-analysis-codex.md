# Codex Authority — Release 1.14 WP07-P2B
## Independent Offline Launch-Plan Security Review and Descriptor Allocation Analysis

**Authority:** Owner requests WP07-P2B following reported completion of WP07-P2A. **READ-ONLY investigation only.** No edits, tests, builds, restore, commit, branch, push, PR, merge, runtime execution, Docker, Azure, or deployment.

## Baselines and preservation
- Verify remote `main` contains merged PR #331, merge commit `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8`. Do not assume remote `main` has not moved.
- Protected checkout: `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`. Intentionally dirty; do not fetch into or change refs, checkout, reset, clean, stash, stage or modify files.
- Record initial and final complete Git status and refs. Record hashes of the two P2A files and check pre-existing tracked-file preservation where feasible. Distinguish Git-listed files from ignored/untracked files; never claim stronger evidence than obtained.
- Review only; do not promote local untracked legacy runtime to approved architecture.

## P2A artifacts under review
- `container/admin_lifecycle_launch_plan.py`
- `python/presentation/test_admin_lifecycle_launch_plan.py`

P2A reported 15 passing offline tests and two-file-only creation; independently inspect the source and tests. This authority does not authorize rerunning tests. Compare against the merged protocol source and relevant existing specifications as read-only references.

## Binding security contracts
- A1–A3; D1, strict D2, D3; T1–T11; C1; owner-approved P2-C2; frozen `aiq-admin-lifecycle-v1` 64-byte protocol.
- Approved topology: finite secret-free root PID1 pre-fork; supervisor S UID/GID 1200/1200 remains PID1 and directly parents router R UID/GID 1100 and guardian G UID/GID 1300; guardian directly parents agent A UID/GID 1000; agent parents UI UID/GID 1000. Numeric S/G allocation remains conditional on base-image collision checks. Role/session confinement and permanent identity/capability drops are required; no cross-UID signals.
- Distinct startup budgets: five-second privileged bootstrap phase and 30-second total startup. One Linux `CLOCK_MONOTONIC` epoch; one shared four-second shutdown deadline, within five-second Docker stop grace. D1 and D2 require real, independent, positive bound kernel wait exit-zero observations; terminal absence is not a substitute.
- No credential in pre-fork ancestor or trusted roles; synthetic qualification credential only generated inside isolated router after separation. No public listener before `R_EXPOSE`; Option A disabled.
- **C1:** Guardian role `04` sends `A_EXEC` and agent-bootstrap role `07` replies `A_EXEC_ACK` only over bootstrap launch gate; bootstrap senders forbidden on runtime channels. ACK proves receipt, not exec/readiness.
- **P2-C2:** Agent metadata FD4 survives agent exec, is validated exactly once after exec, then closes. The separate guardian–agent bootstrap launch gate closes **before** agent exec. These are different descriptors/channels; guardian FD4 is its gate end, not the agent metadata FD4.
- Approved postexec role FD maps: supervisor FD3 SR, FD4 SG, FD5 metadata; guardian FD3 SG, FD4 gate, FD5 metadata; router FD3 credential, FD4 RA, FD5 SR, FD6 metadata; agent FD3 RA, temporary FD4 metadata; UI no control/credential FDs. Agent-bootstrap gate FD number is **not assigned** by prior owner decision. Do not silently assign or amend the approved maps.

## Required read-only analysis
1. **Source-backed conformance:** Identify exact file/line evidence for immutable role graph, direct parentage, distinct S/G/agent/router identities, session isolation, permanent hardening, credential secrecy, FD ownership and lifecycle, shared absolute deadlines, startup readiness/exposure order, and shutdown proof obligations. Distinguish executable validation from merely declared model data.
2. **Negative-case adequacy:** Review all 15 reported test cases and inspect for bypasses: duplicate or invalid IDs, wrong parent, session aliasing including same-session SIGCONT, missing capability drop, descriptor collisions/leaks, early credential generation, premature `R_EXPOSE`, startup deadline reset, shutdown deadline reset, missing bound positive wait, and absence-only success. Do not invent executed results.
3. **FD lifetime conflict resolution:** Confirm P2-C2 is modeled without contradiction: agent metadata FD4 must cross exec once, whereas launch gate must close before exec. Analyze agent-bootstrap gate allocation options (e.g., transient separate FD or explicit pre-exec remapping), including CLOEXEC, pipe endpoint inheritance, aliasing and FD3/FD4 conflicts. **Recommend** a collision-free plan and precise owner decision wording, but **do not assign** an FD number as binding authority.
4. **Protocol fit:** Cross-check P2A milestones and role/FD assumptions against merged `admin_lifecycle_protocol.py`, C1 channel exception, T9–T11 wire and sequencing, including one-time readiness and `A_READY` post-exec. Identify unmodeled runtime IPC and evidence boundaries.
5. **Integration hazard inventory:** Review relevant current `main` entrypoint/Docker packaging and local legacy bootstrap/router/agent only to identify incompatibilities; no code changes. In particular, distinguish router PID1 assumptions, pre-fork credentials, ASCII transport, `waitpid(-1)` and relative deadlines from the approved architecture.
6. **Next increment:** Propose a smallest bounded follow-up with exact files, prerequisites, offline validation and explicit STOP conditions. Separate any FD-number decision from implementation authority; do not authorize native execution, privilege changes, deployment or GitHub actions.

## STOP conditions
Stop and report if specifications genuinely conflict, source cannot be inspected, protected checkout/ref preservation cannot be maintained, descriptor allocation requires revising a frozen owner decision, or next-step scope cannot be bounded. Never resolve ambiguity by silently broadening authority.

## Deliverable format
- Verdict: `WP07-P2B REVIEW ACCEPTED — OWNER DECISION REQUIRED` or `WP07-P2B BLOCKED — OWNER REVIEW REQUIRED`.
- Baseline and preservation evidence, P2A file hashes, source/line citations, contract matrix with pass/gap/uncertain, negative-test assessment, descriptor options and preferred nonbinding recommendation, exact unresolved owner decisions, proposed next file allowlist and validation gates.
- Clearly separate static evidence from actual kernel/identity/FD/clock evidence, which remains unqualified.
- **No mutation and no automatic PR/merge under this authority.**

`PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO**. STOP for owner review.
