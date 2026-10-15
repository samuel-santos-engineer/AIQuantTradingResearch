# Codex Authority — Release 1.14 WP07-P3F
## Targeted Offline Contract Validation Corrections

**Owner authorization:** WP07-P3F authorized. This authority permits only the narrow offline correction and testing below. It does **not** authorize native implementation, integration, release, or acceptance of P3D.

### Context and immutable baseline
- Protected checkout: `draft/release-1.14-wp07-partial-public-disabled`; expected HEAD `67893f51931ee183f71050a83b81127e6e648b87`. The checkout is intentionally dirty. Preserve all pre-existing changes, refs, files and Git status entries.
- P3E read-only reassessment concluded `WP07-P3E BLOCKED — OWNER REVIEW REQUIRED` due to two static acceptance bypasses; its conclusions are review findings, not demonstrated native exploits.
- Expected pre-edit SHA-256:
  - `container/admin_lifecycle_native_boundary_contracts.py`: `0DDA0B31936AD4E96EBC665D804DBD84C5E3E6B7C2C01570FFE92D594061AA5A`
  - `python/presentation/test_admin_lifecycle_native_boundary_contracts.py`: `14012FEDF467E9144982C06CA125F81EB5AB823492ABB2589AB8DCA365ABB124`
- P3D reported 27 focused tests passing; P3E inspected but did not execute them. Do not treat the 27-pass report as independent native evidence.
- Frozen `aiq-admin-lifecycle-v1` binary protocol, accepted launch plan, mandatory positive child-specific exit-zero waits, secret-free fork boundaries, absolute monotonic deadlines, four-second shared shutdown budget, five-second Docker grace, and conditional agent-bootstrap FD5 assignment remain unchanged. `PUBLIC_RUNTIME_VERIFIED=False`; all production/public-runtime/Azure gates are **NO-GO**.

### Exact write allowlist
Modify **only** these two existing paths:
1. `container/admin_lifecycle_native_boundary_contracts.py`
2. `python/presentation/test_admin_lifecycle_native_boundary_contracts.py`

No new files or third-path modifications. No dependencies, generated artifacts, schema or frozen-protocol changes. Do not touch launch-plan or codec sources. No Git checkout/reset/stash/clean/fetch, commit, push, PR, merge, network, Docker, Azure, native syscalls, real fork/exec/wait, credential handling, ABI/toolchain selection, or production wiring. Keep work synthetic and standard-library-only.

### Required preflight
1. Record current branch, HEAD, complete `git status --porcelain=v1 -uall`, local refs, both target SHA-256 hashes, and a preservation baseline for all other Git-listed tracked/untracked files. Preserve the intentionally dirty state.
2. If branch/HEAD or either target hash differs from the expected baseline, **STOP** and report the discrepancy; do not guess or overwrite later work.
3. Inspect the two allowlisted sources, the P3E findings, and relevant accepted launch-plan/protocol declarations read-only. Identify precise functions and invariant boundaries before editing.

### P3F-1 — Gate endpoint provenance reconciliation
- Close the P3E counterexample where current `gate` and `gate_alias` endpoints are relabeled as `credential` while retaining their IDs, alias relation and closure, but gate history still describes them as gate endpoints.
- Reconcile **every shared endpoint ID** between current inventory and gate history, regardless of the current endpoint channel. Require consistent immutable provenance/identity attributes and alias relationships as applicable; do not restrict consistency checking to current `channel == "gate"`.
- Preserve mandatory A_EXEC/ACK history, gate/alias closure, conditional FD5, and the existing valid agent-exec fixture.
- Add reason-specific negative tests for contradictory shared-ID channel/provenance and aliases, including the exact P3E relabeling counterexample. Ensure a valid, consistent history remains accepted.

### P3F-2 — Startup ledger constructor and prefix integrity
- Close the P3E counterexample: constructing `startup_events=("bogus",) * 4` must fail, and it must not be possible to advance to `listener` using a malformed prefilled prefix.
- Validate every supplied startup-event prefix against the frozen/accepted startup sequence **at construction** and preserve that invariant across transitions; length/type-only checks are insufficient.
- Reject skipped, duplicated, reordered, unknown, or overlong prefixes with specific classifications. Preserve the existing readiness-before-exposure and launch/metadata/gate separation requirements.
- Add direct-constructor and transition-focused negatives; retain valid empty, partial and complete prefixes and ordinary successful progression.

### Regression and evidence
- Preserve P3D's accepted-offline checks: completed-wait constructor timing, sticky wait failures, distinct A/G/R PID bindings and exclusive status ownership, timestamped EOF→agent wait→guardian wait and disposal→router wait prerequisites, wait-before-drain rejection, endpoint closure/remapping, privilege/session declarations, and exposure ordering.
- Keep negative tests specific about rejection classifications, not merely generic exceptions.
- Run **only** the authorized focused validation:
  `python -B -m unittest python.presentation.test_admin_lifecycle_native_boundary_contracts -v`
- Report test count and exit code; distinguish tests from independent review and native qualification.

### Preservation and final report
- Record final complete Git status, branch/HEAD, refs, SHA-256 of both modified files and compare all non-allowlisted Git-listed file hashes against preflight. Report limitations (ignored files and Git metadata not exhaustively hashed, if applicable).
- Provide a concise finding-by-finding change summary with source locations, exact negative/positive test coverage, validation output summary and any unresolved gaps.
- Do **not** claim independent acceptance or actual kernel/FD/UID/session/deadline feasibility.
- Stop immediately if the correction needs a third path, changed frozen contract, native operations, credentials, ABI selection, dependencies or destructive Git actions. If blocked, leave the checkout intact and explain.

### Required terminal verdict
Use exactly one:
- `WP07-P3F OFFLINE CONTRACT VALIDATION CORRECTIONS COMPLETE — OWNER REVIEW REQUIRED`
- `WP07-P3F BLOCKED — OWNER REVIEW REQUIRED`

A **separately authorized independent read-only reassessment** is required before accepting these contracts or considering native implementation planning.
