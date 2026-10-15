# Codex Authority — Release 1.14 WP07-P2E
## Offline Endpoint Handoff, FD5 Occupancy and Shutdown Ownership Model

**Owner authorization:** P2-C4 approved conditionally. FD5 is the *proposed* transient agent-bootstrap launch-gate slot, **not** authorization to allocate it in a live process. This authority permits only the offline modeling and tests below. STOP for owner review on any conflict.

### Protected baseline and preservation
- Remote `main` reference at last review: `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8` (PR #331). Verify current remote state without updating local refs; do not assume it remains current.
- Protected checkout: `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`; working tree intentionally dirty.
- Initial expected SHA-256:
  - `container/admin_lifecycle_launch_plan.py`: `835BF3E4D39D2E97C53A6FB98DC7A2AC91CB8C64CA1B58112E2940A4D0884E2A`
  - `python/presentation/test_admin_lifecycle_launch_plan.py`: `9DDCC4351D3638A2AF24B99867323E983CAEC9D07E328ACC8DAA778590DD9705`
- Verify branch/HEAD, complete Git status, expected hashes before editing. Inventory and hash non-allowlisted Git-listed files before/after; report limits for ignored files and Git metadata. Preserve all unrelated changes. No checkout, reset, stash, clean, commit, push, PR or merge.

### Exact mutation allowlist (only these two existing files)
1. `container/admin_lifecycle_launch_plan.py`
2. `python/presentation/test_admin_lifecycle_launch_plan.py`

No other file edits, new files, dependency changes, project/solution changes, generated tracked artifacts or ref updates. Python bytecode must not be written (`python -B`). If a necessary correction requires another path, STOP.

### Frozen contracts
Preserve A1–A3, D1/strict D2/D3, T1–T11, C1, P2-C2, P2-C3 and conditional P2-C4. Immutable topology: root finite secret-free pre-fork → supervisor UID/GID1200 PID1, direct router UID/GID1100 and guardian UID/GID1300; guardian direct agent UID/GID1000; agent owns UI UID/GID1000. Distinct trusted/presentation sessions, permanent hardening, no cross-UID signaling. Router-local disposable synthetic credential only after separation; no pre-fork secret or public credential ingress. Frozen `aiq-admin-lifecycle-v1` framing, roles, messages, deadlines and proof separation unchanged.

**Process-local FD maps:** supervisor FD3 SR, FD4 SG, FD5 metadata; guardian FD3 SG, FD4 bootstrap gate, FD5 metadata; router FD3 credential, FD4 RA, FD5 SR, FD6 metadata; agent postexec FD3 RA, temporary FD4 metadata; UI no inherited control/credential FDs. Proposed **agent-bootstrap FD5** launch gate, distinct from agent FD3/FD4. Guardian FD4 is the guardian-side gate. Gate closes explicitly after `A_EXEC_ACK` and before agent exec, CLOEXEC as backstop; metadata FD4 survives agent exec, is validated once then closed. The gate's numerical use is *conditional* on proving collision-free static handoff. Do not change maps to force a pass.

Timing: privileged startup ≤5s; total startup ≤30s; shared absolute Linux `CLOCK_MONOTONIC` shutdown deadline ≤4s; Docker grace 5s. No resetting deadlines. Positive specifically bound guardian wait for agent exit0 and supervisor waits for guardian/router exit0 remain mandatory; EOF, reports, injected observations, ECHILD and terminal absence never substitute for kernel proof. Offline declarations are not kernel evidence.

### Implementation obligations
Extend the **pure, non-executing** immutable launch-plan model with explicit phase transitions and endpoint lineage:
1. **Creation/acquisition:** represent which bootstrap parent creates or acquires each logical pipe/socket endpoint before each fork; distinguish an endpoint from its per-process FD number and aliases. A child cannot inherit an endpoint absent from its immediate parent's preceding snapshot. Model root→guardian bootstrap, guardian bootstrap→agent bootstrap, root→router bootstrap and root→supervisor handoffs coherently, without inventing a privileged resident helper or violating finite secret-free pre-fork.
2. **Fork and slot occupancy:** account for every inherited endpoint, unused end and duplicate alias at each boundary. Model agent-bootstrap FD5's *pre-assignment occupant* and reject assigning/remapping the gate into occupied FD5 unless the previous occupant has been safely closed and is proven no longer required. Reject overwriting live FD3 RA or FD4 metadata. Do not silently assume FD5 is free.
3. **C1/C2/C3/C4 lifetimes:** require gate ACK, explicit close of agent FD5 and all gate aliases **before** exec; require CLOEXEC separately; ensure guardian-side FD4 ownership/closure is represented without conflating agent metadata FD4. Preserve RA FD3 and metadata FD4 across agent exec; metadata validate-once-and-close; UI inherits none.
4. **Shutdown snapshots:** extend ownership/closure modeling past UI spawn through STOP_UI, UI_STOPPED + permitted EOF, R_PROTOCOL_DONE, HOST_DISPOSED, guardian bound agent observation, supervisor bound guardian/router observations, adopted-child drain and terminal absence. Reject leaked or improperly closed control, gate, metadata or credential endpoints; do not treat closure or EOF as exit-zero proof.
5. **State integrity:** reject nonexistent source endpoints, wrong parent, impossible transfers, retained unused ends, alias escape, occupied-slot overwrite, duplicate ownership/closure, session aliasing, premature R_EXPOSE, deadline resets and missing hardening/readiness. Use deterministic, testable immutable data; no actual FDs, clocks, processes or credentials.
6. If any exact endpoint creation/transfer order cannot be reconciled with the frozen topology and existing protocol, **STOP and document the ambiguity**, rather than fabricate a valid trace or change the contract.

### Focused offline validation
Run `python -B -m unittest python.presentation.test_admin_lifecycle_launch_plan -v` and report exact count/exit code. Preserve all 23 existing test intentions. Add explicit negative tests for unavailable parent→child handoffs, occupied inherited agent FD5, overwritten live endpoints, retained fork/exec aliases, guardian-side gate confusion, premature R_EXPOSE, session aliasing, and shutdown endpoint leaks/incorrect closure. Include a coherent canonical positive trace covering creation, fork, exec, UI and shutdown. No test may claim native enforcement.

### Stop gates and final report
STOP on unexpected initial hashes/baseline, need for additional files, incompatible frozen maps, source ambiguity, test failure, preservation mismatch or any request for native operations. No fork/exec, `dup2`, `fcntl`, `setuid`, signals, `waitpid`, credential generation, network listener, Docker, Azure, GitHub mutations or deployment. No PR/merge authority.

Report: exact files/diff, before/after SHA-256, test command/count, phase-by-phase ownership trace, explicit FD5 occupancy finding, shutdown endpoint ledger, unresolved gaps, initial/final branch/HEAD/status, unrelated hash preservation and its limits. End with exactly one:

- `WP07-P2E OFFLINE HANDOFF MODEL COMPLETE — OWNER REVIEW REQUIRED`
- `WP07-P2E BLOCKED — OWNER REVIEW REQUIRED`

`PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO**. Do not claim runtime qualification.
