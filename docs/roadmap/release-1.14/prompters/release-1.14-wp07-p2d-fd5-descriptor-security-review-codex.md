# Codex Authority — Release 1.14 WP07-P2D
## Independent P2C Security Review and Proposed FD5 Allocation

**Authority type:** READ-ONLY investigation. **STOP — OWNER REVIEW REQUIRED.**

### Owner direction and decision boundary
The owner directs that **FD5 remain the proposed agent-bootstrap launch-gate descriptor**. FD5 is a candidate for independent validation, **not a frozen P2-C4 numeric allocation** and not authority to implement it. Do not substitute FD3 or FD4, silently assign a different number, or modify any frozen descriptor map. If FD5 conflicts, stop and explain the conflict and options; do not self-approve a replacement.

### Baseline and preservation
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Verify remote `main` includes PR #331 merge `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8`; record any drift rather than assuming it is current.
- Protected checkout: `draft/release-1.14-wp07-partial-public-disabled` at `67893f51931ee183f71050a83b81127e6e648b87`, intentionally dirty. Preserve branch, HEAD, refs, tracked/untracked/ignored files, and full Git status. No fetch or ref update in the protected checkout.
- P2C model expected SHA-256: `835BF3E4D39D2E97C53A6FB98DC7A2AC91CB8C64CA1B58112E2940A4D0884E2A`.
- P2C tests expected SHA-256: `9DDCC4351D3638A2AF24B99867323E983CAEC9D07E328ACC8DAA778590DD9705`.
- Record pre/post hashes and status. Distinguish verified content preservation from status-only evidence; never claim ignored files were hashed if they were not.

### Frozen decisions and constraints
Preserve A1–A3, D1/strict D2/D3, T1–T11, C1, P2-C2 and P2-C3, and `aiq-admin-lifecycle-v1` exactly. Supervisor PID1 UID/GID1200; guardian UID/GID1300; router UID/GID1100; agent/UI UID/GID1000. Actual image-specific identity collision checks remain outstanding. Finite root secret-free pre-fork, permanent hardening, isolated sessions, router-local synthetic credential generation after separation, and no public exposure before `R_EXPOSE` remain mandatory. Five-second privileged startup, 30-second total startup, shared four-second monotonic shutdown, five-second Docker stop grace. Positive bound kernel waits (guardian→agent; supervisor→guardian/router) cannot be replaced by reports, EOF, `ECHILD`, injected observations or terminal absence.

### Required review tasks
1. Read both P2C files fully; inspect all 23 test methods without executing them. Independently assess whether immutable plan validation rejects invalid parentage, duplicate/conflicting identities, session aliasing, hardening omissions, premature `R_EXPOSE`, deadline reset/overflow, false proof and credential-order violations. Report any missing test or bypass with precise file/line evidence.
2. Trace **every process and phase** of the descriptor model: root pre-fork; guardian bootstrap; agent bootstrap before/after ACK; agent exec; postexec metadata validation; agent→UI spawn; shutdown. Enumerate FD numbers, ownership, pipe/socket ends, duplicates, CLOEXEC flags, explicit closes, inherited aliases, and permitted survivors. Distinguish symbolic modeled obligations from actual kernel-verified operations.
3. Test the **proposed agent-bootstrap FD5** on paper against agent FD3 (RA runtime) and FD4 (temporary metadata surviving exec); guardian FD4 (launch gate); and all other relevant parent/inherited endpoints. Validate that process-local reuse is legal only where lifetime/ownership rules allow it. Require closure of agent FD5 after `A_EXEC_ACK` and before agent exec, with CLOEXEC as backstop, and no surviving aliases. Guardian-side launch gate FD4 has its own lifecycle. No UI control/credential FD inheritance.
4. Check descriptor setup order and remapping hazards (`dup2`/`dup3` conceptual collisions, close-on-exec toggles, alias propagation, pipe-end leaks), including the possibility that the inherited FD5 slot is occupied before remapping. Do not perform native operations.
5. Verify that C1 launch-gate `A_EXEC`/`A_EXEC_ACK` semantics and P2-C2 metadata FD4 postexec validation remain independent; gate ACK is not proof of exec or `A_READY`. Ensure no frozen wire-role or sequence rules are changed.
6. Recommend exact wording for **P2-C4 owner approval or rejection** of FD5, identifying all conditions and unresolved evidence. Propose the smallest next offline implementation scope and exact file allowlist, but do not grant implementation authority.

### Forbidden actions
No file edits/creation in the repository; no test or build execution; no dependency restore; no fork/exec, FD operations, signals, native waits, identity changes or credentials; no Docker, Azure, network probing, deploy, Git commits/branches/pushes, PRs or merges. Read-only GitHub/API queries permitted solely for baseline verification; do not mutate refs. If the review requires any forbidden action or a changed contract, stop.

### Required report
- Baseline, SHA-256 comparison, full initial/final status and preservation limitations.
- Contract-by-contract verdict and source/line evidence, including test coverage limitations.
- Phase-by-phase FD ownership/lifetime table and explicit FD5 collision/alias analysis.
- Exact P2-C4 decision proposal, unresolved questions, next bounded offline increment.
- State clearly: static model ≠ real kernel FD closure, confinement, lineage, bound waits or four-second runtime proof.
- One final verdict only: **`WP07-P2D REVIEW ACCEPTED — P2-C4 OWNER DECISION REQUIRED`** or **`WP07-P2D BLOCKED — OWNER REVIEW REQUIRED`**.

`PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO**. Stop for owner review; do not implement or merge.
